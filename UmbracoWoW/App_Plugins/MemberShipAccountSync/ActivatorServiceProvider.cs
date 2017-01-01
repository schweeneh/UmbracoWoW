using Umbraco.Core.Logging;
using System;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync
{
    internal class ActivatorServiceProvider : IServiceProvider
    {
        private ILogger _logger;

        public ActivatorServiceProvider(ILogger logger)
        {
            _logger = logger;   
        }

        public ActivatorServiceProvider() { }

        public object GetService(Type serviceType)
        {
            if(_logger != null)
            {
                var loggerConstructor = serviceType.GetConstructor(new[] { typeof(ILogger) });

                if(loggerConstructor != null)
                {
                    return loggerConstructor.Invoke(new object[] { _logger });
                }
            }

            return Activator.CreateInstance(serviceType);
        }
    }
}