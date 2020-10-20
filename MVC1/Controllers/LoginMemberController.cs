using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC1.Areas.Admin.Models;
using MVC1.Models;

namespace MVC1.Controllers
{
    public class LoginMemberController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        DataAccess da = new DataAccess();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult LoginMember(LoginMemberModel Member)
        //{
        //    using (SqlConnection con = new SqlConnection(cs))
        //    {

        //    }
        //}
        public ActionResult LoginMember(LoginMemberModel member, string Customer_Email, string Customer_Pass)
        {
            string maicon = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(maicon);
            sqlcon.Open();
            SqlCommand com = new SqlCommand("EXEC dbo.Login_Member", sqlcon);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Customer_Email", member.Customer_Email);
            com.Parameters.AddWithValue("@Customer_Pass", member.Customer_Pass);
            sqlcon.Close();
            return View();
        }
    }
}