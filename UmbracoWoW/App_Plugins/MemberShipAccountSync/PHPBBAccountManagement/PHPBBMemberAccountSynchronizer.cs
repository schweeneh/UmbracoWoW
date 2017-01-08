using System;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Security.Providers;
using UmbracoWoW.App_Plugins.MembershipAccountSync;
using UmbracoWoW.App_Plugins.MemberShipAccountSync.MangosAccountManagement;
using Umbraco.Core.Logging;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync.PHPBBAccountManagement
{
    public class PHPBBMemberAccountSynchronizer : MembershipAccountSynchronizerBase
    {
        public PHPBBMemberAccountSynchronizer(ILogger logger) : base(logger) { }
        
        protected override void AccountSyncMembershipProvider_Created(MembersMembershipProvider sender, AccountSyncMembershipProvider.NewMemberEventArgs e)
        {
            //throw new NotImplementedException();
        }

        protected override void AccountSyncMembershipProvider_PasswordChanged(MembersMembershipProvider sender, AccountSyncMembershipProvider.PasswordChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        protected override void MemberService_Deleted(IMemberService sender, DeleteEventArgs<IMember> e)
        {
            //throw new NotImplementedException();
        }

        protected override void MemberService_Saved(IMemberService sender, SaveEventArgs<IMember> e)
        {
            //throw new NotImplementedException();
        }
    }
}
