using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using PrototypeDatabase.Models;
using System.Data.SqlClient;


namespace PrototypeDatabase.Pages.NewPassword
{
    public class NewPasswordModel : PageModel
    {
        public string UserName;
        public const string SessionKeyName1 = "username";

        public string Message { get; set; } 

        [BindProperty]
        public LibraryMember NewUser { get; set; }

        public IActionResult OnGet()
        {
            UserName = HttpContext.Session.GetString(SessionKeyName1);
            // Checks to see if the session is correct 
            if (string.IsNullOrEmpty(UserName))
            {
                HttpContext.Session.Clear();
                return RedirectToPage("/Login/Login");
            }
            return Page();


        }
        public IActionResult OnPost()
        {
            //Database connection
            string DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PrototypeDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())

            {
                //Updates the databases password with the new password
                command.Connection = conn;
                command.CommandText = @"UPDATE LibraryMember SET UserPass = @UP WHERE Username = @UN";

                command.Parameters.AddWithValue("@UN", NewUser.Username);
                command.Parameters.AddWithValue("@UP", NewUser.UserPass);
                

                Console.WriteLine(NewUser.UserPass);

                command.ExecuteNonQuery();
                //Redirects to page to update information
                return RedirectToPage("/AccountDetails/UpdatedDetails");

            }
        }

    }
}
