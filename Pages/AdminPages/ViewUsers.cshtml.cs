using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using PrototypeDatabase.Models;
using PrototypeDatabase.Pages.DatabaseConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PrototypeDatabase.Pages.AdminPages
{
    public class ViewUsersModel : PageModel
    {
        [BindProperty]
        public List<LibraryMember> User { get; set; }

        public List<string> URole { get; set; } = new List<string> { "User", "Admin" };
        public string UserName;
        public const string SessionKeyName1 = "username";


        public string FirstName;
        public const string SessionKeyName2 = "fname";

        public string SessionID;
        public const string SessionKeyName3 = "sessionID";
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                HttpContext.Session.Clear();
                return RedirectToPage("/Login/Login");
            }
            return Page();
            //get the session first!
            UserName = HttpContext.Session.GetString(SessionKeyName1);
            FirstName = HttpContext.Session.GetString(SessionKeyName2);
            SessionID = HttpContext.Session.GetString(SessionKeyName3);


            DatabaseConnect dbstring = new DatabaseConnect(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                //Shows all users in table
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM LibraryMember";

                var reader = command.ExecuteReader();

                User = new List<LibraryMember>();
                while (reader.Read())
                {
                    LibraryMember Row = new LibraryMember(); //each record found from the table
                    Row.Id = reader.GetInt32(0);
                    Row.FirstName = reader.GetString(1);
                    Row.Username = reader.GetString(3);
                    Row.Role = reader.GetString(6); // We dont get the password. The role field is in the 5th position
                    User.Add(Row);
                }
                
            }
            return Page();

        }
    }
}
