using System;
using Umbraco.Core;
using UmbracoWoW.App_Plugins.MembershipAccountSync;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync
{
    public class MembershipResolverApplicationEventHandler : ApplicationEventHandler
    {
        private IServiceProvider _serviceProvider = new ActivatorServiceProvider();

        protected override void ApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //Register the resolvers for the membership account sychronization plugin.
            MembershipAccountSynchronizersResolver.Current = new MembershipAccountSynchronizersResolver(_serviceProvider, applicationContext.ProfilingLogger.Logger, PluginManager.Current.ResolveMembershipSynchronizers());
        }

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //Register the events that run when a user is deleted, modified or added.
            var synchronizers = MembershipAccountSynchronizersResolver.Current.AccountSynchronizers;
            foreach(IMemberAccountSynchronizer synchronizer in synchronizers)
            {
                //Register the event handlers.
                synchronizer.RegisterEvents();
            }
        }
    }
}