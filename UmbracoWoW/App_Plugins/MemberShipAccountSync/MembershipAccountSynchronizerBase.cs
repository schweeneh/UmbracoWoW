using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UmbracoWoW.App_Plugins.MembershipAccountSync;
using Umbraco.Core.Services;
using Umbraco.Core.Events;
using Umbraco.Core.Models;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync
{
    public abstract class MembershipAccountSynchronizerBase : IMemberAccountSynchronizer
    {
        public void RegisterEvents()
        {
            MemberService.Deleted += MemberService_Deleted;
            MemberService.Saved += MemberService_Saved;
            MemberService.Saving += MemberService_Saving;
            MemberService.Created += MemberService_Created;
        }

        protected abstract void MemberService_Created(IMemberService sender, NewEventArgs<IMember> e);

        protected abstract void MemberService_Saved(IMemberService sender, Umbraco.Core.Events.SaveEventArgs<Umbraco.Core.Models.IMember> e);        

        protected abstract void MemberService_Deleted(IMemberService sender, Umbraco.Core.Events.DeleteEventArgs<Umbraco.Core.Models.IMember> e);

        protected abstract void MemberService_Saving(IMemberService sender, Umbraco.Core.Events.SaveEventArgs<Umbraco.Core.Models.IMember> e);
    }
}