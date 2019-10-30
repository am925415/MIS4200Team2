using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MIS4200Team2.Models
{
    public class Profile
    {
        public int profileID { get; set; }
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Display(Name = "Bio")]
        public string bio { get; set; }
        [Display(Name = "Email")]

        public string email { get; set; }
        [Display(Name = "Phone Number")]
        public string phoneNumber { get; set; }
        [Display(Name = "Employee ID")]
        public   int employeeID { get; set; }
        public virtual EmployeeFullDetail EmployeeFullDetail { get; set; }




}
}