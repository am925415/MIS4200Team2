using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MIS4200Team2.Models
{
    public class EmployeeRecognitionNomination
    {
       
        [Display(Name = "Employee ID:")]
         [Key] public int employeeID { get; set; }

        
        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        public DateTime HireDate { get; set; }

        [Display(Name = "Email Address:")]
        [Required(ErrorMessage = "The email address is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }

        [Display(Name = "Phone Number:")]
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }
      
        [Display(Name = "Business Unit")]
        [Required(ErrorMessage = "Business Unit Required")]
        public businessUnit BusinessLocation { get; set; }
        [Display(Name = "Recognition Description:")]
        public string recognitionDescription { get; set; }

        [Display(Name = "Core Value:")]
        public centricCoreValue CentricCoreValue { get; set; }
        public enum businessUnit
        {
            Boston,
            Charlotte,
            Chicago,
            Cincinnati,
            Cleveland,
            Columbus,
            India,
            Indianapolis,
            Louisville,
            Miami,
            Seattle,
            [Display(Name = "St. Louis")]
            StLouis,
            Tampa
        }

        public enum title
        {
            Consultant,
            [Display(Name = "Senior Consultant")]
            SeniorConsultant,
            Manager,
            Architect,
            [Display(Name = "Senior Manager/Senior Architect")]
            SeniorManager,
            Director,
            VP
        }

        [Display(Name = "Values")]
        public centricCoreValue values { get; set; }

        public enum centricCoreValue
        {
            Stewardship,
            Culture,
            [Display(Name = "Delivery Excellance")]
            DeliveryExcellance,
            Innovation,
            [Display(Name = "Greater Good")]
            GreaterGood,
            Integrity,
            Balance
        }
        
        public ICollection<EmployeeRecognitionNomination> EmployeeRecognitionNominations { get; set; }
        
        public int profileID { get; set; }
        public virtual Profile Profile  { get; set; }
    } 
    
}