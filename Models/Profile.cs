using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIS4200Team2.Models
{
    public class Profile
    {
        public int profileID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string bio { get; set; }
        public string email { get; set; }
       
        public string phoneNumber { get; set; }

        public   int employeeID { get; set; }
        public virtual EmployeeFullDetail EmployeeFullDetail { get; set; }




}
}