using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MIS4200Team2.Models
{
    public class EmployeeFullDetail
    {
        public int employeeFullDetailID { get; set; }
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Display(Name = "Business Unit")]
        public string businessUnit { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime hireDate { get; set; }
        [Display(Name = "Title")]
        public string title { get; set; }

        public ICollection<Profile> Profiles { get; set; }
    }
}