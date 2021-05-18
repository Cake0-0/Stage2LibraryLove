using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using PrototypeDatabase.Models;
using System.Data.SqlClient;


namespace PrototypeDatabase.Pages.ForgotPassword
{
    public class ResetPasswordModel : PageModel
    {
        public string Email;
        public const string SessionKeyName1 = "email";

        public string Message { get; set; }

        [BindProperty]
        public LibraryMember NewUser { get; set; }

        public IActionResult OnGet()
        {
            Email = HttpContext.Session.GetString(SessionKeyName1);
            return Page();

        }
        public IActionResult OnPost()
        {

            string DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PrototypeDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())

            {
                //Updates the password of the user with the new password
                command.Connection = conn;
                command.CommandText = @"UPDATE LibraryMember SET UserPass = @UP WHERE Email = @Em";

                command.Parameters.AddWithValue("@Em", NewUser.Email);
                command.Parameters.AddWithValue("@UP", NewUser.UserPass);


                Console.WriteLine(NewUser.UserPass);

                command.ExecuteNonQuery();

                return RedirectToPage("/AccountDetails/UpdatedDetails");

            }
        }

    }
}
