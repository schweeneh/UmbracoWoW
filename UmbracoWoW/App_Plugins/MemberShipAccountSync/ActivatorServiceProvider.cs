using System;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync
{
    internal class ActivatorServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            return Activator.CreateInstance(serviceType);
        }
    }
}