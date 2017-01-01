using System;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Core.Logging;
using UmbracoWoW.App_Plugins.MembershipAccountSync;
using UmbracoWoW.App_Plugins.MemberShipAccountSync.MangosAccountManagement.MangosSOAPClient;
using System.Threading;
using System.Web;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync.MangosAccountManagement
{
    public class MangosMemberAccountSynchronizer : MembershipAccountSynchronizerBase
    {
        private ILogger _logger;

        public MangosMemberAccountSynchronizer(ILogger logger)
        {
            _logger = logger;
        }

        protected override void MemberService_Created(IMemberService sender, NewEventArgs<IMember> e)
        {
            
            //throw new NotImplementedException();
            
        }

        protected override void MemberService_Deleted(IMemberService sender, DeleteEventArgs<IMember> e)
        {
            //CommandLine.RunCommand("php", "phpSoapClient.php server info", HttpContext.Current.Server.MapPath("~\\App_Plugins\\MemberShipAccountSync\\MangosAccountManagement\\MangosSOAPClient"));
            var ea = e;
        }

        protected override void MemberService_Saved(IMemberService sender, SaveEventArgs<IMember> e)
        {
            var ea = e;
            //CommandLine.RunCommand("php", "phpSoapClient.php server info", HttpContext.Current.Server.MapPath("~\\App_Plugins\\MemberShipAccountSync\\MangosAccountManagement\\MangosSOAPClient"));
        }

        protected override void MemberService_Saving(IMemberService sender, SaveEventArgs<IMember> e)
        {
            var ea = e;
            //throw new NotImplementedException();
        }
    }
}