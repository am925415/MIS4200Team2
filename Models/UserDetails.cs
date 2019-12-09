using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIS4200Team2.Models
{
    public class UserDetails
    {
        public Guid ID { get; set; }


        [Display(Name = "First Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use only letters A - Z")]
        [StringLength(30)]
        public string firstName { get; set; }



        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use only letters A - Z")]
        [StringLength(30)]
        public string lastName { get; set; }

        [Display(Name = "Name")]
        public string fullName => firstName + " " + lastName;

        [Display(Name = "Birth Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime birthDate { get; set; }



        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Display(Name = "Phone Number")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]{3})-([0-9]{3})-([0-9]{4})$",
                   ErrorMessage = "Please enter phone number in format xxx-xxx-xxxx.")]
        //[DisplayFormat(DataFormatString = "\\d{3}-\\d{3}-\\d{4}", ApplyFormatInEditMode = true)]
        public string PhoneNumber { get; set; }


        [Display(Name = "Start Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime startDate { get; set; }


        [Display(Name = "Job Title")]
        [Required]
        public jobTitle JobTitle { get; set; }


        [Display(Name = "Business Unit")]
        [Required]
        public operatingGroup operatingGroups { get; set; }


        [Display(Name = "Location")]
        [Required]
        public location locations { get; set; }


        [Display(Name = "Profile Picture (optional)")]

        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only JPG, GIF, or PNG are accepted.")]
        public string photo { get; set; }



        public enum jobTitle
        {
            Consultant = 0,
            [Display(Name = "Consultant")]
            Senior_Consultant = 1,
            Manager = 2,
            Architect = 3,
            [Display(Name = "Manager")]
            Senior_Manager = 4,
            [Display(Name = "Architect")]
            Senior_Architect = 5,
            [Display(Name = "Devloper")]
            Principal_Architect = 6,
            Partner = 7,
        }
        public enum operatingGroup
        {
            [Display(Name = "Software Delivery")]
            Modern_Software_Delivery = 0,
            [Display(Name = "Governance Analytics")]
            Data_and_Analytics = 1,
            [Display(Name = "Application Support")]
            Mobile_App_Development = 2,
            [Display(Name = "Management Collaborations")]
            Enterprise_Collaboration = 3,
            [Display(Name = "IT Consulting")]
            Technology_Solution_Services = 4,
            [Display(Name = "Enterprise Applications & Solutions")]
            Enterprise_Applications_and_Solutions = 5,
            [Display(Name = "Marketing")]
            Marketing_Operations_and_CRM = 6,
            [Display(Name = "Human Resources")]
            Talent_Management = 7,
            Digital = 8,        
            [Display(Name = "Finance")]
            Financial_Services = 16,
        }
        public enum location
        {
            Columbus = 0,
            Seattle = 1,
            India = 2,
            Cincinnati = 3,
            Cleveland = 4,
            [Display(Name = "St. Louis")]
            St_Louis = 5,
            Indianapolis = 6,
            Chicago = 7,
            Charlotte = 8,
            Boston = 9,
            Louisville = 10,
            Miami = 11,
            Tampa = 12,
        }
        public ICollection<Corevalueleaderboard> Users { get; set; }
    }
}