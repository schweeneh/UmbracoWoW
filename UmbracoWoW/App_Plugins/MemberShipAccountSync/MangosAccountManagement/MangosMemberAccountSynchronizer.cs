using System;
using System.Web.Security;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Security.Providers;
using UmbracoWoW.App_Plugins.MembershipAccountSync;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync.MangosAccountManagement
{
    public class MangosMemberAccountSynchronizer : MembershipAccountSynchronizerBase
    {
        protected override void AccountSyncMembershipProvider_Created(MembersMembershipProvider sender, AccountSyncMembershipProvider.NewMemberEventArgs e)
        {
            
        }

        protected override void AccountSyncMembershipProvider_PasswordChanged(MembersMembershipProvider sender, AccountSyncMembershipProvider.PasswordChangedEventArgs e)
        {
            
        }

        protected override void MemberService_Deleted(IMemberService sender, DeleteEventArgs<IMember> e)
        {
            
        }

        protected override void MemberService_Saved(IMemberService sender, SaveEventArgs<IMember> e)
        {
            
        }
    }
}