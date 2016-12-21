using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace UmbracoWoW.App_Plugins.MembershipAccountSync
{
    internal interface IMemberAccountSynchronizer
    {
        //void Save(IMemberService sender, SaveEventArgs<IMember> e);
        //void Delete(IMemberService sender, DeleteEventArgs<IMember> e);
        void RegisterEvents();
    }
}
