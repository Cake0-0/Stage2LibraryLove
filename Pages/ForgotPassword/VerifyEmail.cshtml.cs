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
    public class VerifyEmailModel : PageModel
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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Database connection
            string DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PrototypeDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection conn = new SqlConnection(DbConnection);
            Console.WriteLine(DbConnection);
            conn.Open();

            Console.WriteLine(NewUser.Username);
            Console.WriteLine(NewUser.UserPass);

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT  Email, UserPass, FirstQuestion, FirstAnswer, SecondQuestion, SecondAnswer FROM LibraryMember WHERE Email = @Em";
                //Checks to see if an account exists with the given email address
                command.Parameters.AddWithValue("@Em", NewUser.Email);
                

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    NewUser.Email = reader.GetString(0);
                    NewUser.UserPass = reader.GetString(1);
                    NewUser.FirstQuestion = reader.GetString(2);
                    NewUser.FirstAnswer = reader.GetString(3);
                    NewUser.SecondQuestion = reader.GetString(4);
                    NewUser.SecondAnswer = reader.GetString(5);

                }

                if (!string.IsNullOrEmpty(NewUser.FirstQuestion))
                {
                    SessionID = HttpContext.Session.Id;
                    HttpContext.Session.SetString("sessionID", SessionID);
                    HttpContext.Session.SetString("email", NewUser.Email);
                    HttpContext.Session.SetString("pass", NewUser.UserPass);
                    HttpContext.Session.SetString("fquestion", NewUser.FirstQuestion);
                    HttpContext.Session.SetString("fanswer", NewUser.FirstAnswer);
                    HttpContext.Session.SetString("squestion", NewUser.SecondQuestion);
                    HttpContext.Session.SetString("sanswer", NewUser.SecondAnswer);
                    //If an account exists with this email address then it creates a session and saves the above variables for future use
                    return RedirectToPage("/ForgotPassword/SecurityQuestions");


                }
                else
                {
                    //If an account does not exist with the given email address then it informs the user
                    Message = "Invalid Email Address";
                    return Page();
                }
            }

        }
    }
}
