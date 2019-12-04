using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MIS4200Team2.Models
{
    public class Profile
    {
        public int profileID { get; set; }
        public string FirstName { get; set; }
        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "First Name is required")]
        public string LastName { get; set; }
        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Last Name is required")]
        public string email { get; set; }
        [Display(Name = "Email Address:")]
        [Required(ErrorMessage = "The email address is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Phone Number:")]
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string BusinessUnit { get; set; }
        [Display(Name = "Business Unit:")]
        [Required(ErrorMessage = "Business Unit is required")]
        public DateTime HireDate { get; set; }
        [Display(Name = "Hire Date:")]
        [Required(ErrorMessage = "Hire Date is required")]
        public string State { get; set; }
        [Display(Name = "State:")]
        [Required(ErrorMessage = "Please select your state")]
        public string Country { get; set; }
        [Display(Name = "Country:")]
        [Required(ErrorMessage = "Country is required")]


    }


    public class EmployeeProfile : DbContext
        {
            public DbSet<Profile> Profiles { get; set; }
        }
    
}