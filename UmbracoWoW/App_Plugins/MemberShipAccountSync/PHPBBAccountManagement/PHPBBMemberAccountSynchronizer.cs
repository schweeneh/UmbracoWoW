using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Core.Logging;
using System;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync.PHPBBAccountManagement
{
    public class PHPBBMemberAccountSynchronizer : MembershipAccountSynchronizerBase
    {
        private ILogger _logger;

        public PHPBBMemberAccountSynchronizer(ILogger logger)
        {
            _logger = logger;
        }

        protected override void MemberService_Created(IMemberService sender, NewEventArgs<IMember> e)
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

        protected override void MemberService_Saving(IMemberService sender, SaveEventArgs<IMember> e)
        {
            //throw new NotImplementedException();
        }
    }
}