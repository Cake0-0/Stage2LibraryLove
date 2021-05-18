using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using PrototypeDatabase.Models;
using System.Data.SqlClient;


namespace PrototypeDatabase.Pages.EditAccount
{
    public class EditAccountModel : PageModel
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

        [BindProperty] 
        public LibraryMember NewUser { get; set; }

        public IActionResult OnGet()
        {
            //Takes context strings of all needed variables to display current values for each in the form box
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
            //Checks to see if session is correct
            if (string.IsNullOrEmpty(UserName))
            {
                HttpContext.Session.Clear();
                return RedirectToPage("/Login/Login");
            }
            return Page();
  
             
        }
        public IActionResult OnPostChange(int sessionCount) //On press of the submit changes button 
        {
            //Database Connection
            string DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PrototypeDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())

            {
                //Updates each variable accessible, unable to update username as account is tied to that
                command.Connection = conn;
                command.CommandText = "UPDATE LibraryMember SET Email = @Em, FirstName = @UFN, LastName = @ULN, FirstQuestion = @FQ, FirstAnswer = @FA, SecondQuestion = @SQ, SecondAnswer = @SA WHERE Username = @UN";

                command.Parameters.AddWithValue("@Em", NewUser.Email);
                command.Parameters.AddWithValue("@UFN", NewUser.FirstName);
                command.Parameters.AddWithValue("@ULN", NewUser.LastName);
                command.Parameters.AddWithValue("@UN", NewUser.Username);
                command.Parameters.AddWithValue("@FQ", NewUser.FirstQuestion);
                command.Parameters.AddWithValue("@FA", NewUser.FirstAnswer);
                command.Parameters.AddWithValue("@SQ", NewUser.SecondQuestion);
                command.Parameters.AddWithValue("@SA", NewUser.SecondAnswer);

                Console.WriteLine(NewUser.Email);
                Console.WriteLine(NewUser.FirstName);
                Console.WriteLine(NewUser.LastName);
                Console.WriteLine(NewUser.FirstQuestion);
                Console.WriteLine(NewUser.FirstAnswer);
                Console.WriteLine(NewUser.SecondQuestion);
                Console.WriteLine(NewUser.SecondAnswer);


                command.ExecuteNonQuery();

                return RedirectToPage("/AccountDetails/UpdatedDetails");

            }
        
        }
        public IActionResult OnPostDelete(int sessionCount) // On press of the delete account button
        {
            string DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PrototypeDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())

            {
                // Deletes the row that matches the username
                command.Connection = conn;
                command.CommandText = "DELETE FROM LibraryMember WHERE Username = @UN";

                command.Parameters.AddWithValue("@UN", NewUser.Username);

                command.ExecuteNonQuery();

                return RedirectToPage("/AccountDetails/DeletedAccount"); 

            }


            

        }
    }
}