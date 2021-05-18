using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PrototypeDatabase.Models
{ // Variables used throughout the login session and account details/foregot password pages
    public class LibraryMember
    {
        public int Id { get; set; }

        [Display(Name = "Library ID")] //Not used
        public int LibraryId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        public string UserPass { get; set; }
        [Display(Name = "Role")]  //Defaults to user 
        public string Role { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Display(Name = "Account Balance")] //Not used
        public int Balance { get; set; }

        [Display(Name = "Card Number")]  //Not used
        public int CardNumber { get; set; }

        [Display(Name = "First Security Question")]
        public string FirstQuestion { get; set; }

        [Display(Name = "First Answer")]
        public string FirstAnswer { get; set; }

        [Display(Name = "Second Question")]
        public string SecondQuestion { get; set; }

        [Display(Name = "Second Answer")]
        public string SecondAnswer { get; set; }
    }


}

