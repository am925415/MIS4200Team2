﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIS4200Team2.Models
{
    public class Profile
    {
        public int employeeID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string businessUnit { get; set; }
        public DateTime hireDate { get; set; }
        public string title { get; set; }


    }
}