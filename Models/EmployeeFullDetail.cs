﻿using System;
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
        [Display(Name = "Employee ID")]
        public string firstName { get; set; }
        [Display(Name = "First Name")]
        public string lastName { get; set; }
        [Display(Name = "Last Name")]
        public string businessUnit { get; set; }
        [Display(Name = "Business Unit")]
        public DateTime hireDate { get; set; }
        [Display(Name = "Hiring Date")]
        public string title { get; set; }
        [Display(Name = "Title")]

        public ICollection<EmployeeFullDetail> EmployeeFullDetails { get; set; }
    }
}