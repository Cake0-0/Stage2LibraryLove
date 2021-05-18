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
    public class SecurityQuestionsModel : PageModel
    {
        //Calls the variables from the Session 
        public string Email;
        public const string SessionKeyName1 = "email";


        public string FirstQuestion;
        public const string SessionKeyName2 = "fquestion";

        public string FirstAnswer;
        public const string SessionKeyName3 = "fanswer";

        public string SecondQuestion;
        public const string SessionKeyName4 = "squestion";

        public string SecondAnswer;
        public const string SessionKeyName5 = "sanswer";

        public string SessionID;
        public const string SessionKeyName6 = "sessionID";

        public string Message { get; set; }

        [BindProperty]
        public LibraryMember NewUser { get; set; }

        public IActionResult OnGet()
        {
            //Calls the needed variables from the Model
            Email = HttpContext.Session.GetString(SessionKeyName1);
            FirstQuestion = HttpContext.Session.GetString(SessionKeyName2);
            FirstAnswer = HttpContext.Session.GetString(SessionKeyName3);
            SecondQuestion = HttpContext.Session.GetString(SessionKeyName4);
            SecondAnswer = HttpContext.Session.GetString(SessionKeyName5);
            SessionID = HttpContext.Session.GetString(SessionKeyName6);
            return Page();

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
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
                //Checks to see if the given security answers match the tables 
                Email = HttpContext.Session.GetString(SessionKeyName1);
                command.Connection = conn;
                command.CommandText = @"SELECT FirstName, FirstQuestion, FirstAnswer, SecondQuestion, SecondAnswer, Email FROM LibraryMember WHERE Email = @Em AND FirstAnswer = @FA AND SecondAnswer = @SA";

                command.Parameters.AddWithValue("@FA", NewUser.FirstAnswer);
                command.Parameters.AddWithValue("@SA", NewUser.SecondAnswer);
                command.Parameters.AddWithValue("@Em", NewUser.Email);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    NewUser.FirstName = reader.GetString(0);
                    NewUser.FirstQuestion = reader.GetString(1);
                    NewUser.FirstAnswer = reader.GetString(2);
                    NewUser.SecondQuestion = reader.GetString(3);
                    NewUser.SecondAnswer = reader.GetString(4);
                    NewUser.Email = reader.GetString(5);
                }
                reader.Close();
                //Checks to see if a user with matching security answers exists if it exists goes to next step
                if (!string.IsNullOrEmpty(NewUser.FirstName))
                {
                    return RedirectToPage("/ForgotPassword/ResetPassword");

                }


                // If it doesnt exist redirects to wrong answer page
                else
                {
                    return RedirectToPage("/ForgotPassword/IncorrectAnswers");
                }
            }

        }
    }
}