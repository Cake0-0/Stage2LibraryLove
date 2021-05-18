using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PrototypeDatabase.Pages.AccountDetails

{
    public class DeletedAccount : PageModel
    {
        /*
        public string UserName;
        public const string SessionKeyName1 = "username";

        public string FirstName;
        public const string SessionKeyName2 = "fname";

        public string SessionID;
        public const string SessionKeyName3 = "sessionID";
        */

        public void OnGet()
        {
           //In order for the session to reset and information to update on user end clears session and forces relog
            HttpContext.Session.Clear();


        }
    }
}
