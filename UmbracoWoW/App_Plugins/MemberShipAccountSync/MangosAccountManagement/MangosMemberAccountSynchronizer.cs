using System;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using UmbracoWoW.App_Plugins.MembershipAccountSync;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync.MangosAccountManagement
{
    public class MangosMemberAccountSynchronizer : MembershipAccountSynchronizerBase
    {
        protected override void MemberService_Deleted(IMemberService sender, DeleteEventArgs<IMember> e)
        {

            //Find the Mangos user using the SOAP interface and delete the user.
            //throw new NotImplementedException();
        }

        protected override void MemberService_Saved(IMemberService sender, SaveEventArgs<IMember> e)
        {
            //Find the Mangos user using the SOAP interface and insert/update the user.
            //throw new NotImplementedException();
        }
    }
}