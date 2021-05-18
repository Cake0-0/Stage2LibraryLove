using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrototypeDatabase.Models;
using System.Data.SqlClient;

namespace PrototypeDatabase.Pages.LibraryMembers
{
    public class CreateUserModel : PageModel
    {
        [BindProperty]
        public LibraryMember NewUser { get; set; }

        public string Message { get; set; } 

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PrototypeDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
  
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();



            using (SqlCommand command = new SqlCommand())

            {
                command.Connection = conn;

                

                //Creates a row of the table with the following information, all fields are required in html so will be filled out, email is specified to be formatted with a @
                command.CommandText = @"INSERT INTO LibraryMember (Email, Role, FirstName, LastName, Username, UserPass, FirstQuestion, FirstAnswer, SecondQuestion, SecondAnswer) VALUES (@Em, @R, @UFN, @ULN, @UN, @UP, @FQ, @FA, @SQ, @SA)";

                command.Parameters.AddWithValue("@Em", NewUser.Email);
                command.Parameters.AddWithValue("@R", NewUser.Role);
                command.Parameters.AddWithValue("@UFN", NewUser.FirstName);
                command.Parameters.AddWithValue("@ULN", NewUser.LastName);
                command.Parameters.AddWithValue("@UN", NewUser.Username);
                command.Parameters.AddWithValue("@UP", NewUser.UserPass);
                command.Parameters.AddWithValue("@FQ", NewUser.FirstQuestion);
                command.Parameters.AddWithValue("@FA", NewUser.FirstAnswer);
                command.Parameters.AddWithValue("@SQ", NewUser.SecondQuestion);
                command.Parameters.AddWithValue("@SA", NewUser.SecondAnswer);

                Console.WriteLine(NewUser.Id);
                Console.WriteLine(NewUser.Email);
                Console.WriteLine(NewUser.FirstName);
                Console.WriteLine(NewUser.LastName);
                Console.WriteLine(NewUser.Username);
                Console.WriteLine(NewUser.UserPass);
                Console.WriteLine(NewUser.FirstQuestion);
                Console.WriteLine(NewUser.FirstAnswer);
                Console.WriteLine(NewUser.SecondQuestion);
                Console.WriteLine(NewUser.SecondAnswer);


                command.ExecuteNonQuery();
            }
            
            return RedirectToPage("/Index");
        }
        
    }
}
