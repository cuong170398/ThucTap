using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using MVC1.Areas.Admin.Models;
using System.Data;
using System.Data.SqlClient;

namespace MVC1.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        DataAccess da = new DataAccess();
        // GET: Admin/Admin
        public ActionResult Index(string Searching)
        {
            if(Searching == null || Searching =="")
            {
                List<AdminModel> listAdmin = new List<AdminModel>();
                DataTable dt = da.GetTable("EXEC dbo.Get_Admin");
                da.com.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var Admin = new AdminModel();
                    Admin.Admin_id = Convert.ToInt32(dt.Rows[i]["Admin_id"].ToString());
                    Admin.Permission_id = Convert.ToInt32(dt.Rows[i]["Permission_id"].ToString());
                    Admin.Frist_name = dt.Rows[i]["Frist_name"].ToString();
                    Admin.Last_name = dt.Rows[i]["Last_name"].ToString();
                    Admin.Email = dt.Rows[i]["Email"].ToString();
                    Admin.PassW = dt.Rows[i]["PassW"].ToString();
                    Admin.Addres = dt.Rows[i]["Addres"].ToString();
                    Admin.birthday = Convert.ToDateTime(dt.Rows[i]["birthday"].ToString());
                    Admin.Gender = dt.Rows[i]["Gender"].ToString();
                    Admin.Salary = Convert.ToDecimal(dt.Rows[i]["Salary"].ToString());
                    Admin.Admin_Status = Convert.ToBoolean(dt.Rows[i]["Admin_Status"].ToString());
                    listAdmin.Add(Admin);
                }
                return View(listAdmin);
            }
            else
            {
                List<AdminModel> listAdmin = new List<AdminModel>();
                DataTable dt = da.GetTable("EXEC dbo.GetBySearch @search = '" + Searching + "'");
                da.com.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var Admin = new AdminModel();
                    Admin.Admin_id = Convert.ToInt32(dt.Rows[i]["Admin_id"].ToString());
                    Admin.Permission_id = Convert.ToInt32(dt.Rows[i]["Permission_id"].ToString());
                    Admin.Frist_name = dt.Rows[i]["Frist_name"].ToString();
                    Admin.Last_name = dt.Rows[i]["Last_name"].ToString();
                    Admin.Email = dt.Rows[i]["Email"].ToString();
                    Admin.PassW = dt.Rows[i]["PassW"].ToString();
                    Admin.Addres = dt.Rows[i]["Addres"].ToString();
                    Admin.birthday = Convert.ToDateTime(dt.Rows[i]["birthday"].ToString());
                    Admin.Gender = dt.Rows[i]["Gender"].ToString();
                    Admin.Salary = Convert.ToDecimal(dt.Rows[i]["Salary"].ToString());
                    Admin.Admin_Status = Convert.ToBoolean(dt.Rows[i]["Admin_Status"].ToString());
                    listAdmin.Add(Admin);
                }
                return View(listAdmin);
            }
        }
        //public ActionResult Index(string Searching,int? currPage)
        //{
        //    var pageNumber = currPage ?? 1;
        //    int recodperpage = 3;
        //    List<AdminModel> listAdmin = new List<AdminModel>();
        //    DataTable dt = da.GetTable("EXEC dbo.PT_TK @search = '" + Searching + "',@currPage='" + pageNumber + "',@recodperpage='" + recodperpage + "'");
        //    da.com.CommandType = CommandType.StoredProcedure;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        var Admin = new AdminModel();
        //        Admin.Admin_id = Convert.ToInt32(dt.Rows[i]["Admin_id"].ToString());
        //        Admin.Permission_id = Convert.ToInt32(dt.Rows[i]["Permission_id"].ToString());
        //        Admin.Frist_name = dt.Rows[i]["Frist_name"].ToString();
        //        Admin.Last_name = dt.Rows[i]["Last_name"].ToString();
        //        Admin.Email = dt.Rows[i]["Email"].ToString();
        //        Admin.PassW = dt.Rows[i]["PassW"].ToString();
        //        Admin.Addres = dt.Rows[i]["Addres"].ToString();
        //        Admin.birthday = Convert.ToDateTime(dt.Rows[i]["birthday"].ToString());
        //        Admin.Gender = dt.Rows[i]["Gender"].ToString();
        //        Admin.Salary = Convert.ToDecimal(dt.Rows[i]["Salary"].ToString());
        //        Admin.Admin_Status = Convert.ToBoolean(dt.Rows[i]["Admin_Status"].ToString());
        //        int totalAdmin = Convert.ToInt32(dt.Rows[i]["SL"].ToString());
        //        if (totalAdmin % recodperpage == 0)
        //            Admin.Tong = totalAdmin / recodperpage;
        //        else
        //            Admin.Tong = totalAdmin / recodperpage + 1;
        //        listAdmin.Add(Admin);
        //    }
        //    return View(listAdmin);
        //}
        [HttpGet]
        public int Add(AdminModel admin)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("dbo.Insert_Adminstrator", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Permission_id", admin.Permission_id);
                com.Parameters.AddWithValue("@Frist_name", admin.Frist_name);
                com.Parameters.AddWithValue("@Last_name", admin.Last_name);
                com.Parameters.AddWithValue("@Email", admin.Email);
                com.Parameters.AddWithValue("@PassW", admin.PassW);
                com.Parameters.AddWithValue("@Addres", admin.Addres);
                com.Parameters.AddWithValue("@birthday", admin.birthday.ToString());
                com.Parameters.AddWithValue("@Gender", admin.Gender);
                com.Parameters.AddWithValue("@Salary", admin.Salary);
                com.Parameters.AddWithValue("@Admin_Status", admin.Admin_Status);
                i = com.ExecuteNonQuery();
                return i;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        [HttpGet]
        public JsonResult ShowEdit(int ID)
        {
            List<AdminModel> listAdmin = new List<AdminModel>();
            DataTable dt = da.GetTable("EXEC dbo.getAdminID @id = "+ID);
            da.com.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var Admin = new AdminModel();
                Admin.Admin_id = Convert.ToInt32(dt.Rows[i]["Admin_id"].ToString());
                Admin.Permission_id = Convert.ToInt32(dt.Rows[i]["Permission_id"].ToString());
                Admin.Frist_name = dt.Rows[i]["Frist_name"].ToString();
                Admin.Last_name = dt.Rows[i]["Last_name"].ToString();
                Admin.Email = dt.Rows[i]["Email"].ToString();
                Admin.PassW = dt.Rows[i]["PassW"].ToString();
                Admin.Addres = dt.Rows[i]["Addres"].ToString();
                Admin.birthday = Convert.ToDateTime(dt.Rows[i]["birthday"].ToString());
                Admin.Gender = dt.Rows[i]["Gender"].ToString();
                Admin.Salary = Convert.ToDecimal(dt.Rows[i]["Salary"].ToString());
                Admin.Admin_Status = Convert.ToBoolean(dt.Rows[i]["Admin_Status"].ToString());
                listAdmin.Add(Admin);
            }
            JsonResult json = Json(listAdmin[0], JsonRequestBehavior.AllowGet);
            return json;
        }
        [HttpGet]
        public int Edit(AdminModel admin)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("dbo.Update_Admin",con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Admin_id", admin.Admin_id);
                com.Parameters.AddWithValue("@Permission_id", admin.Permission_id);
                com.Parameters.AddWithValue("@Frist_name", admin.Frist_name);
                com.Parameters.AddWithValue("@Last_name", admin.Last_name);
                com.Parameters.AddWithValue("@Email", admin.Email);
                com.Parameters.AddWithValue("@PassW", admin.PassW);
                com.Parameters.AddWithValue("@Addres", admin.Addres);
                com.Parameters.AddWithValue("@birthday", admin.birthday.ToString());
                com.Parameters.AddWithValue("@Gender", admin.Gender);
                com.Parameters.AddWithValue("@Salary", admin.Salary);
                com.Parameters.AddWithValue("@Admin_Status", admin.Admin_Status);
                i = com.ExecuteNonQuery();
                return i;
            }
            catch(Exception ex)
            {
                string mes = ex.Message;
            }
            return -1;
        }
        public int Delete(int ID)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                    int i;
                    con.Open();
                    SqlCommand com = new SqlCommand("dbo.Delete_AdminStrator", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Admin_id", ID);
                    i = com.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
    }
}