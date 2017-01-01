using System.Web.Security;
using Umbraco.Core.Events;
using Umbraco.Web.Security.Providers;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync
{
    public class AccountSyncMembershipProvider : MembersMembershipProvider
    {
        public static event TypedEventHandler<MembersMembershipProvider, NewMemberEventArgs> Created;

        public static event TypedEventHandler<MembersMembershipProvider, PasswordChangedEventArgs> PasswordChanged;

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var result = base.ChangePassword(username, oldPassword, newPassword);
            var args = new PasswordChangedEventArgs
            {
                Username = username,
                OldPassword = oldPassword,
                NewPassword = newPassword
            };
            PasswordChanged.RaiseEvent(args, this);
            return result;
        }

        protected override MembershipUser PerformCreateUser(string memberTypeAlias, string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var result = base.PerformCreateUser(memberTypeAlias, username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
            var args = new NewMemberEventArgs
            {
                MemberTypeAlias = memberTypeAlias,
                UserName = username,
                Password = password,
                Email = email,
                PasswordQuestion = passwordQuestion,
                PasswordAnswer = passwordAnswer,
                IsApproved = isApproved,
                ProviderUserKey = providerUserKey,
                Status = status
            };
            Created.RaiseEvent(args, this);
            return result;
        }
        
        public override void UpdateUser(MembershipUser user)
        {
            base.UpdateUser(user);
        }

        public class NewMemberEventArgs : System.EventArgs
        {
            public string MemberTypeAlias { get; set; }

            public string UserName { get; set; }

            public string Password { get; set; }

            public string Email { get; set; }

            public string PasswordQuestion { get; set; }

            public string PasswordAnswer { get; set; }

            public bool IsApproved { get; set; }

            public object ProviderUserKey { get; set; }

            public MembershipCreateStatus Status { get; set; }
        }

        public class PasswordChangedEventArgs : System.EventArgs
        {
            public string Username { get; set; }

            public string OldPassword { get; set; }

            public string NewPassword { get; set; }
        }
    }
}