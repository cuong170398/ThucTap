using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC1.Areas.Admin.Models
{
    public class AdminModel
    {
        public int Admin_id { set; get; }
        public int Permission_id { set; get; }
        public int Quyen { set; get; }
        public string Frist_name { set; get; }
        public string Last_name { set; get; }
        public string Email { set; get; }
        public string PassW { set; get; }
        public string Addres { set; get; }
        [DataType(DataType.Date)]
        public DateTime birthday { set; get; }
        public string Gender { set; get; }
        public decimal Salary { set; get; }
        public Boolean Admin_Status { set; get; }
        public int Tong { set; get; }
    }
}