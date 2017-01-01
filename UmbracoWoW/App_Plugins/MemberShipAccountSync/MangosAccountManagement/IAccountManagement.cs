using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoWoW.App_Plugins.MemberShipAccountSync.MangosAccountManagement
{
    interface IAccountManagement
    {
        //Add an account.
        void CreateAccount(string userName, string password);

        //Delete an account.
        void DeleteAccount(string userName);

        //Update an account password.
        void UpdateAccountPassword(string userName, string password);
    }
}
