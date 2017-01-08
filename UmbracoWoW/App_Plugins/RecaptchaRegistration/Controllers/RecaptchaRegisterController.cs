using System;
using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Core;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web;

namespace UmbracoWoW.App_Plugins.RecaptchaRegistration.Controllers
{
    public class RecaptchaRegisterController : SurfaceController
    {
        [HttpPost]
        public ActionResult HandleRegisterMember([Bind(Prefix = "registerModel")]RegisterModel model)
        {
            var verifyPassword = Request.Form["VerifyPassword"];

            var recaptchaCode = Request.Form["g-recaptcha-response"];

            var secret = CurrentPage.GetPropertyValue<string>("secret");

            if(verifyPassword != model.Password)
            {
                ModelState.AddModelError("registerModel.Password", "Passwords don't match.");
            }

            if (!RecaptchaValidation.Validate(recaptchaCode, secret))
            {
                ModelState.AddModelError("RecaptchaError", "Recaptcha was incorrect.");
            }

            if (ModelState.IsValid == false)
            {
                return CurrentUmbracoPage();
            }

            MembershipCreateStatus status;

            var usernameIsEmail = CurrentPage.GetPropertyValue<bool>("useEmailForUsername");
            model.UsernameIsEmail = usernameIsEmail;
            model.Name = model.Username;

            var member = Members.RegisterMember(model, out status, model.LoginOnSuccess);

            switch (status)
            {
                case MembershipCreateStatus.Success:

                    TempData["FormSuccess"] = true;

                    //if there is a specified path to redirect to then use it
                    if (model.RedirectUrl.IsNullOrWhiteSpace() == false)
                    {
                        return Redirect(model.RedirectUrl);
                    }
                    //redirect to current page by default

                    return RedirectToCurrentUmbracoPage();
                case MembershipCreateStatus.InvalidUserName:
                    ModelState.AddModelError((model.UsernameIsEmail || model.Username == null)
                        ? "registerModel.Email"
                        : "registerModel.Username",
                        "Username is not valid");
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    ModelState.AddModelError("registerModel.Password", "The password is not strong enough");
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                case MembershipCreateStatus.InvalidAnswer:
                    //TODO: Support q/a http://issues.umbraco.org/issue/U4-3213
                    throw new NotImplementedException(status.ToString());
                case MembershipCreateStatus.InvalidEmail:
                    ModelState.AddModelError("registerModel.Email", "Email is invalid");
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    ModelState.AddModelError((model.UsernameIsEmail || model.Username == null)
                        ? "registerModel.Email"
                        : "registerModel.Username",
                        "A member with this username already exists.");
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    ModelState.AddModelError("registerModel.Email", "A member with this e-mail address already exists");
                    break;
                case MembershipCreateStatus.UserRejected:
                case MembershipCreateStatus.InvalidProviderUserKey:
                case MembershipCreateStatus.DuplicateProviderUserKey:
                case MembershipCreateStatus.ProviderError:
                    //don't add a field level error, just model level
                    ModelState.AddModelError("registerModel", "An error occurred creating the member: " + status);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return CurrentUmbracoPage();
        }

    }
}