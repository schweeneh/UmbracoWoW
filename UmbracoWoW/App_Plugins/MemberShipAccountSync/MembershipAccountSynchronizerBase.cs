﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UmbracoWoW.App_Plugins.MembershipAccountSync;
using Umbraco.Core.Services;
using System.Web.Security;
using UmbracoWoW.App_Plugins.MemberShipAccountSync.MangosAccountManagement;
using Umbraco.Core;
using Umbraco.Core.Logging;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync
{
    public abstract class MembershipAccountSynchronizerBase : IMemberAccountSynchronizer
    {
        protected ILogger _logger;

        public MembershipAccountSynchronizerBase(ILogger logger)
        {
            _logger = logger;
        }

        public void RegisterEvents()
        {
            MemberService.Deleted += MemberService_Deleted;
            MemberService.Saved += MemberService_Saved;
            AccountSyncMembershipProvider.Created += AccountSyncMembershipProvider_Created;
            AccountSyncMembershipProvider.PasswordChanged += AccountSyncMembershipProvider_PasswordChanged;
        }

        protected abstract void AccountSyncMembershipProvider_PasswordChanged(Umbraco.Web.Security.Providers.MembersMembershipProvider sender, AccountSyncMembershipProvider.PasswordChangedEventArgs e);
        
        protected abstract void AccountSyncMembershipProvider_Created(Umbraco.Web.Security.Providers.MembersMembershipProvider sender, AccountSyncMembershipProvider.NewMemberEventArgs e);
        
        protected abstract void MemberService_Saved(IMemberService sender, Umbraco.Core.Events.SaveEventArgs<Umbraco.Core.Models.IMember> e);        

        protected abstract void MemberService_Deleted(IMemberService sender, Umbraco.Core.Events.DeleteEventArgs<Umbraco.Core.Models.IMember> e);
    }

    public class MembershipAccountSyncServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            var normalArgs = new[] { typeof(ILogger) };
            var found = serviceType.GetConstructor(normalArgs);
            if (found != null)
                return found.Invoke(new object[]
                {
                    ApplicationContext.Current.ProfilingLogger.Logger
                });
            //use normal ctor
            return Activator.CreateInstance(serviceType);
        }
    }
}
