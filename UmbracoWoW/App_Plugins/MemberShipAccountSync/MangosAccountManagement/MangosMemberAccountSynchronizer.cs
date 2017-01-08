using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Security.Providers;
using System.Configuration;
using System;
using System.Web;
using System.Linq;
using Umbraco.Core.Logging;

using UmbracoWoW.MemberShipAccountSync.MangosAccountManagement;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync.MangosAccountManagement
{
    public class MangosMemberAccountSynchronizer : MembershipAccountSynchronizerBase
    {
        private MangosConfig _config;

        private string _workingDir = HttpContext.Current.Server.MapPath("/App_Plugins/MemberShipAccountSync/MangosAccountManagement/MangosSOAPClient");

        public MangosMemberAccountSynchronizer(ILogger logger) : base(logger)
        {
            try
            {
                _config = ConfigurationManager.GetSection("mangos") as MangosConfig;
            }catch(Exception ex)
            {
                throw new Exception("Could not load mangos configuration file. Check to make sure the mangos.config file exists and is properly configured.", ex); 
            }
        }

        protected override void AccountSyncMembershipProvider_Created(MembersMembershipProvider sender, AccountSyncMembershipProvider.NewMemberEventArgs e)
        {
            EnsureConfigLoaded();

            try
            {
                CommandLine.RunCommand("php", string.Format("{0} {1} {2} {3} {4} \"account create {5} {6}\"", "phpMangosSoapClient.php", _config.UserName, _config.Password, _config.HostName, _config.HostPort, e.UserName, e.Password), _workingDir);
            }catch(Exception ex)
            {
                //swallow this exception and log it. We don't want to cause an error in Umbraco if the WoW server isn't up and running.
                //A user should be able to sync their umbraco account with mangos once the server is up.
                _logger.WarnWithException(typeof(MangosMemberAccountSynchronizer), "Error creating mangos account: " + ex.Message, ex);
                e.Status = System.Web.Security.MembershipCreateStatus.ProviderError;
            }
        }

        protected override void AccountSyncMembershipProvider_PasswordChanged(MembersMembershipProvider sender, AccountSyncMembershipProvider.PasswordChangedEventArgs e)
        {
            EnsureConfigLoaded();

            CommandLine.RunCommand("php", string.Format("{0} {1} {2} {3} {4} \"account set password {5} {6} {7}\"", "phpMangosSoapClient.php", _config.UserName, _config.Password, _config.HostName, _config.HostPort, e.Username, e.OldPassword, e.NewPassword), _workingDir);
        }

        protected override void MemberService_Deleted(IMemberService sender, DeleteEventArgs<IMember> e)
        {
            var member = e.DeletedEntities.FirstOrDefault();

            if(member != null)
            {
                EnsureConfigLoaded();

                try
                {
                    CommandLine.RunCommand("php", string.Format("{0} {1} {2} {3} {4} \"account delete {5}\"", "phpMangosSoapClient.php", _config.UserName, _config.Password, _config.HostName, _config.HostPort, member.Name), _workingDir);
                }
                catch (Exception ex)
                {
                    //swallow this exception and log it. We don't want to cause an error in Umbraco if the WoW server isn't up and running.
                    //An admin should be able to delete orphan accounts at any time.
                    _logger.WarnWithException(typeof(MangosMemberAccountSynchronizer), "Error deleting mangos account: " + ex.Message, ex);
                }
            }
        }

        protected override void MemberService_Saved(IMemberService sender, SaveEventArgs<IMember> e)
        {
            
        }

        private void EnsureConfigLoaded()
        {
            if (_config == null)
            {
                throw new Exception("Mangos config was not setup propertly, cannot create account on Mangos server.");
            }
        }
    }
}
