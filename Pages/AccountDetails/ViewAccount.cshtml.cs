using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace PrototypeDatabase.Pages.AccountDetails
{
    public class ViewAccountModel : PageModel
    {
        public string UserName;
        public const string SessionKeyName1 = "username";


        public string FirstName;
        public const string SessionKeyName2 = "fname";

        public string SessionID;
        public const string SessionKeyName3 = "sessionID";

        public string LastName;
        public const string SessionKeyName4 = "lname";

        public string Email;
        public const string SessionKeyName5 = "email";

        public string Role;
        public const string SessionKeyName6 = "role";

        public string FirstQuestion;
        public const string SessionKeyName7 = "fquestion";

        public string FirstAnswer;
        public const string SessionKeyName8 = "fanswer";

        public string SecondQuestion;
        public const string SessionKeyName9 = "squestion";

        public string SecondAnswer;
        public const string SessionKeyName10 = "sanswer";

        public IActionResult OnGet()
        {
            //calls variables for the model for the webpage to view
            UserName = HttpContext.Session.GetString(SessionKeyName1);
            FirstName = HttpContext.Session.GetString(SessionKeyName2);
            SessionID = HttpContext.Session.GetString(SessionKeyName3);
            LastName = HttpContext.Session.GetString(SessionKeyName4);
            Email = HttpContext.Session.GetString(SessionKeyName5);
            Role = HttpContext.Session.GetString(SessionKeyName6);
            FirstQuestion = HttpContext.Session.GetString(SessionKeyName7);
            FirstAnswer = HttpContext.Session.GetString(SessionKeyName8);
            SecondQuestion = HttpContext.Session.GetString(SessionKeyName9);
            SecondAnswer = HttpContext.Session.GetString(SessionKeyName10);

            //checks if session is correct
            if (string.IsNullOrEmpty(UserName))
            {
                HttpContext.Session.Clear();
                return RedirectToPage("/Login/Login");
            }
            return Page();

        }
       
    }
}


