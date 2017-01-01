using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using umbraco.cms.businesslogic.member;
using Umbraco.Core;
using Umbraco.Web.Models;
using Umbraco.Web.Security;

namespace UmbracoWoW.App_Plugins.RecaptchaRegistration.Models
{
    public class RecaptchaRegisterModel : PostRedirectModel
    {
        [Required]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
            ErrorMessage = "Please enter a valid e-mail address")]
        public string Email { get; set; }

        /// <summary>
        /// Returns the member properties
        /// </summary>
        public List<UmbracoProperty> MemberProperties { get; set; }

        /// <summary>
        /// The member type alias to use to register the member
        /// </summary>
        [Editable(false)]
        public string MemberTypeAlias { get; set; }

        /// <summary>
        /// The members real name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The members password
        /// </summary>
        [Required]
        public string Password { get; set; }

        [ReadOnly(true)]
        [Obsolete("This is no longer used and will be removed from the codebase in future versions")]
        public bool RedirectOnSucces { get; set; }

        /// <summary>
        /// The username of the model, if UsernameIsEmail is true then this is ignored.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Flag to determine if the username should be the email address, if true then the Username property is ignored
        /// </summary>
        public bool UsernameIsEmail { get; set; }

        /// <summary>
        /// Specifies if the member should be logged in if they are succesfully created
        /// </summary>
        public bool LoginOnSuccess { get; set; }

        /// <summary>
        /// Default is true to create a persistent cookie if LoginOnSuccess is true
        /// </summary>
        public bool CreatePersistentLoginCookie { get; set; }

        public RecaptchaRegisterModel(bool doLookup)
        {
            MemberTypeAlias = Constants.Conventions.MemberTypes.DefaultAlias;
            RedirectOnSucces = false;
            UsernameIsEmail = true;
            MemberProperties = new List<UmbracoProperty>();
            LoginOnSuccess = true;
            CreatePersistentLoginCookie = true;
            if (doLookup && HttpContext.Current != null && ApplicationContext.Current != null)
            {
                var helper = new MembershipHelper(ApplicationContext.Current, new HttpContextWrapper(HttpContext.Current));
                var model = helper.CreateRegistrationModel(MemberTypeAlias);
                MemberProperties = model.MemberProperties;
            }
        }

        public static RecaptchaRegisterModel CreateModel()
        {
            var model = new RecaptchaRegisterModel(false);
            return model;
        }

        internal class RecaptchaRegisterModelBinder : DefaultModelBinder
        {
            protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
            {
                return RecaptchaRegisterModel.CreateModel();
            }
        }
    }
}