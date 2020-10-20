using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC1.Models
{
    public class LoginMemberModel
    {
        [Required(ErrorMessage ="Nhập Email")]
        public string Customer_Email { set; get; }
        [Required(ErrorMessage = "Nhập Mật Khẩu")]
        public string Customer_Pass { set; get; }
        public string Last_name { set; get; }

    }
}