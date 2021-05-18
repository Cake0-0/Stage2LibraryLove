using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrototypeDatabase.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace PrototypeDatabase.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LibraryMember NewUser { get; set; }

        public string Message { get; set; }
        public string SessionID;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            string DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PrototypeDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection conn = new SqlConnection(DbConnection);
            Console.WriteLine(DbConnection); 
            conn.Open();

            Console.WriteLine(NewUser.Username);
            Console.WriteLine(NewUser.UserPass);

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT FirstName, Username, Role, LastName, Email, UserPass, FirstQuestion, FirstAnswer, SecondQuestion, SecondAnswer FROM LibraryMember WHERE Username = @UN AND UserPass = @UP";

                command.Parameters.AddWithValue("@UN", NewUser.Username);
                command.Parameters.AddWithValue("@UP", NewUser.UserPass);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    NewUser.FirstName = reader.GetString(0);
                    NewUser.Username = reader.GetString(1);
                    NewUser.Role = reader.GetString(2);
                    NewUser.LastName = reader.GetString(3);
                    NewUser.Email = reader.GetString(4);
                    NewUser.UserPass = reader.GetString(5);
                    NewUser.FirstQuestion = reader.GetString(6);
                    NewUser.FirstAnswer = reader.GetString(7);
                    NewUser.SecondQuestion = reader.GetString(8);
                    NewUser.SecondAnswer = reader.GetString(9);
                }

                if (!string.IsNullOrEmpty(NewUser.FirstName))
                {
                    SessionID = HttpContext.Session.Id;
                    HttpContext.Session.SetString("sessionID", SessionID);
                    HttpContext.Session.SetString("username", NewUser.Username);
                    HttpContext.Session.SetString("fname", NewUser.FirstName);
                    HttpContext.Session.SetString("lname", NewUser.LastName);
                    HttpContext.Session.SetString("email", NewUser.Email);
                    HttpContext.Session.SetString("role", NewUser.Role);
                    HttpContext.Session.SetString("pass", NewUser.UserPass);
                    HttpContext.Session.SetString("fquestion", NewUser.FirstQuestion);
                    HttpContext.Session.SetString("fanswer", NewUser.FirstAnswer);
                    HttpContext.Session.SetString("squestion", NewUser.SecondQuestion);
                    HttpContext.Session.SetString("sanswer", NewUser.SecondAnswer);

                    if (NewUser.Role == "User")
                    {
                        return RedirectToPage("/UserPages/UserIndex");
                    }
                    else if (NewUser.Role == "Librarian")
                    {
                        return RedirectToPage("/LibrarianPages/LibrarianIndex"); 
                    }
                    else 
                    {
                        return RedirectToPage("/AdminPages/AdminIndex");
                    }


                }
                else
                {
                    Message = "Invalid Username and Password!";
                    return Page();
                }
            }

        }
    }
}
