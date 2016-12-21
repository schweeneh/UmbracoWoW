using System;
using System.Collections.Generic;
using Umbraco.Core;
using UmbracoWoW.App_Plugins.MembershipAccountSync;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync
{
    public static class PluginManagerExtensions
    {
        internal static IEnumerable<Type> ResolveMembershipSynchronizers(this PluginManager resolver)
        {
            return resolver.ResolveTypes<IMemberAccountSynchronizer>();
        }
    }
}