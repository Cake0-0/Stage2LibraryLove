using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using PrototypeDatabase.Models;
using System.Data.SqlClient;


namespace PrototypeDatabase.Pages.ChangePassword
{
    public class ChangePasswordModel : PageModel
    {
        public string UserName;
        public const string SessionKeyName1 = "username";


        public string Message { get; set; } 

        [BindProperty]
        public LibraryMember NewUser { get; set; }

        public IActionResult OnGet()
        {
            UserName = HttpContext.Session.GetString(SessionKeyName1);
            // Checks to see if session type is correct
            if (string.IsNullOrEmpty(UserName))
            {
                HttpContext.Session.Clear();
                return RedirectToPage("/Login/Login");
            }
            return Page();
        }

        public IActionResult OnPost()
        {   // Checks if model is valid
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Database Connection
            string DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PrototypeDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //Sql Query
            SqlConnection conn = new SqlConnection(DbConnection);
            Console.WriteLine(DbConnection);
            conn.Open();

            Console.WriteLine(NewUser.Username);
            Console.WriteLine(NewUser.UserPass);

            using (SqlCommand command = new SqlCommand())
            {
                UserName = HttpContext.Session.GetString(SessionKeyName1); 
                command.Connection = conn;
                command.CommandText = @"SELECT FirstName, Username, Role, LastName, Email, UserPass FROM LibraryMember WHERE Username = @UN AND UserPass = @UP";
                //checks to see if the password enters matches any in the database with that password
                command.Parameters.AddWithValue("@UP", NewUser.UserPass);
                command.Parameters.AddWithValue("@UN", NewUser.Username);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    NewUser.FirstName = reader.GetString(0);
                    NewUser.Username = reader.GetString(1);
                    NewUser.Role = reader.GetString(2);
                    NewUser.LastName = reader.GetString(3);
                    NewUser.Email = reader.GetString(4);
                    NewUser.UserPass = reader.GetString(5);
                }
                reader.Close();
                //Checks to see if the user exists, if it does it'll redirect to the next step
                if (!string.IsNullOrEmpty(NewUser.FirstName))
                {
                    return RedirectToPage("/AccountDetails/NewPassword");

                }


                // If it doesn't then the password doesn't exist
                else
                {
                    Message = "Invalid Password!";
                    return Page();
                }
            }

        }
    }
}