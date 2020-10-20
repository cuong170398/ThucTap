using MVC1.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace MVC1.Areas.Admin.Controllers
{
    public class PermissionController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        DataAccess da = new DataAccess();
        // GET: Admin/Permission
        public ActionResult Index()
        {
            List<PermissionModel> listPermiss = new List<PermissionModel>();
            DataTable dt = da.GetTable("EXEC dbo.Get_Permission");
            da.com.CommandType = CommandType.StoredProcedure;
            for(int i=0;i<dt.Rows.Count;i++)
            {
                var Permission = new PermissionModel();
                Permission.Permission_id = Convert.ToInt32(dt.Rows[i]["Permission_id"].ToString());
                Permission.Permision_name = dt.Rows[i]["Permision_name"].ToString();
                Permission.Permission_Status = Convert.ToBoolean(dt.Rows[i]["Permission_Status"].ToString());
                listPermiss.Add(Permission);
            }
            return View(listPermiss);
        }
        [HttpGet]
        public int Add(PermissionModel permission)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("dbo.InserPermission", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Permision_name", permission.Permision_name);
                com.Parameters.AddWithValue("@Permission_Status", permission.Permission_Status);
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
        [HttpGet]
        public JsonResult ShowEdit(int ID)
        {
            List<PermissionModel> ListPermissions = new List<PermissionModel>();
            DataTable dt = da.GetTable("EXEC dbo.Get_PermissionId @Permission_id='"+ID+"'");
            da.com.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var permission = new PermissionModel();
                permission.Permission_id = Convert.ToInt32(dt.Rows[i]["Permission_id"].ToString());
                permission.Permision_name = dt.Rows[i]["Permision_name"].ToString();
                permission.Permission_Status = Convert.ToBoolean(dt.Rows[i]["Permission_Status"].ToString());
                ListPermissions.Add(permission);
            }
            JsonResult json = Json(ListPermissions[0], JsonRequestBehavior.AllowGet);
            return json;
        }
        [HttpGet]
        public int Edit(PermissionModel permission)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("dbo.Update_Permission", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Permission_id", permission.Permission_id);
                com.Parameters.AddWithValue("@Permision_name", permission.Permision_name);
                com.Parameters.AddWithValue("@Permission_Status", permission.Permission_Status);
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
        public int Delete(int ID)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("dbo.DeletePermission", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Permission_id",ID);
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