using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace UmbracoWoW.App_Plugins.MemberShipAccountSync.MangosAccountManagement.MangosCommandLine
{
    public class MangosAccountManagement : IAccountManagement
    {
        public void CreateAccount(string userName, string password)
        {
            
        }

        public void DeleteAccount(string userName)
        {
            throw new NotImplementedException();
        }

        public void UpdateAccountPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}