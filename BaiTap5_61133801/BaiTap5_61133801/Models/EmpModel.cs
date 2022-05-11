using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiTap5_61133801.Models
{
    public class EmpModel
    {
        public string EmpID { get; set; }
        public string Name { get; set; }
        public DateTime BirthOfDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }
        public string Avatar { get; set; }
    }
}