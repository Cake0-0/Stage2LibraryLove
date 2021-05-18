using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PrototypeDatabase.Pages.LibrarianPages
{
    public class LibrarianIndexModel : PageModel
    {
        public string UserName;
        public const string SessionKeyName1 = "username";


        public string FirstName;
        public const string SessionKeyName2 = "fname";

        public string SessionID;
        public const string SessionKeyName3 = "sessionID";


        public IActionResult OnGet()
        {
            UserName = HttpContext.Session.GetString(SessionKeyName1);
            FirstName = HttpContext.Session.GetString(SessionKeyName2);
            SessionID = HttpContext.Session.GetString(SessionKeyName3);
            //checks to see if session exists. Librarian Default page
            if (string.IsNullOrEmpty(UserName))
            {
                HttpContext.Session.Clear();
                return RedirectToPage("/Login/Login");
            }
            return Page();

        }
    }
}
