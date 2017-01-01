using System;
using Umbraco.Core;
using UmbracoWoW.App_Plugins.MembershipAccountSync;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync
{
    public class MembershipResolverApplicationEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //Register the resolvers for the membership account sychronization plugin.
            MembershipAccountSynchronizersResolver.Current = new MembershipAccountSynchronizersResolver(new ActivatorServiceProvider(applicationContext.ProfilingLogger.Logger), applicationContext.ProfilingLogger.Logger, PluginManager.Current.ResolveMembershipSynchronizers());
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