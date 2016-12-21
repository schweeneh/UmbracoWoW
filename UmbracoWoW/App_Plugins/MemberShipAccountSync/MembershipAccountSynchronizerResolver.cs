using Umbraco.Core.Logging;
using System;
using System.Collections.Generic;
using Umbraco.Core.ObjectResolution;
using UmbracoWoW.App_Plugins.MembershipAccountSync;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync
{
    internal class MembershipAccountSynchronizersResolver : ManyObjectsResolverBase<MembershipAccountSynchronizersResolver, IMemberAccountSynchronizer>
    {
        internal MembershipAccountSynchronizersResolver(IServiceProvider serviceProvider, ILogger logger, IEnumerable<Type> synchronizers)
            : base(serviceProvider, logger, synchronizers, ObjectLifetimeScope.Application)
		{
        }

        public IEnumerable<IMemberAccountSynchronizer> AccountSynchronizers
        {
            get
            {
                return Values;
            }
        }
    }
}