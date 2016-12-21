using System;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using UmbracoWoW.App_Plugins.MembershipAccountSync;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync.PHPBBAccountManagement
{
    public class PHPBBMemberAccountSynchronizer : MembershipAccountSynchronizerBase
    {
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