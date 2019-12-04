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
        [Key]public int employeeID { get; set; }
        [Display(Name = "Employee ID")]
        public string firstName { get; set; }
        [Display(Name = "First Name")]
        public string lastName { get; set; }
        [Display(Name = "Last Name")]
        public string businessUnit { get; set; }
        [Display(Name = "Business Unit")]
        public string recognition { get; set; }
        [Display(Name = "Employee Recognition Nomination Description")]

        public ICollection<EmployeeRecognitionNomination> EmployeeRecognitionNominations { get; set; }
    } 
}