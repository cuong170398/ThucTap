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
    public class SupplierController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        DataAccess da = new DataAccess();
        // GET: Admin/Supplier
        public ActionResult Index()
        {
            List<SupplierModel> listSupplier = new List<SupplierModel>();
            DataTable dt = da.GetTable("EXEC dbo.Get_Supplier");
            da.com.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var Supplier = new SupplierModel();
                Supplier.Supplier_id = Convert.ToInt32(dt.Rows[i]["Supplier_id"].ToString());
                Supplier.Supplier_name = dt.Rows[i]["Supplier_name"].ToString();
                Supplier.Supplier_address = dt.Rows[i]["Supplier_address"].ToString();
                Supplier.Supplier_phone = dt.Rows[i]["Supplier_phone"].ToString();
                Supplier.Supplier_fax = dt.Rows[i]["Supplier_fax"].ToString();
                Supplier.Supplier_email = dt.Rows[i]["Supplier_email"].ToString();
                Supplier.Supplier_Status = Convert.ToBoolean(dt.Rows[i]["Supplier_Status"].ToString());
                listSupplier.Add(Supplier);
            }
            return View(listSupplier);
        }
        [HttpGet]
        public int Add(SupplierModel supplier)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("dbo.Insert_Supplier", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("Supplier_name", supplier.Supplier_name);
                com.Parameters.AddWithValue("Supplier_address", supplier.Supplier_address);
                com.Parameters.AddWithValue("Supplier_phone", supplier.Supplier_phone);
                com.Parameters.AddWithValue("Supplier_fax", supplier.Supplier_fax);
                com.Parameters.AddWithValue("Supplier_email", supplier.Supplier_email);
                com.Parameters.AddWithValue("Supplier_Status", supplier.Supplier_Status);
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
            List<SupplierModel> ListSupplier = new List<SupplierModel>();
            DataTable dt = da.GetTable("dbo.Get_SupplierId @Supplier_id='" + ID + "'");
            da.com.CommandType = CommandType.StoredProcedure;
            for(int i=0;i<dt.Rows.Count;i++)
            {
                var Supplier = new SupplierModel();
                Supplier.Supplier_id = Convert.ToInt32(dt.Rows[i]["Supplier_id"].ToString());
                Supplier.Supplier_name = dt.Rows[i]["Supplier_name"].ToString();
                Supplier.Supplier_address = dt.Rows[i]["Supplier_address"].ToString();
                Supplier.Supplier_phone = dt.Rows[i]["Supplier_phone"].ToString();
                Supplier.Supplier_fax = dt.Rows[i]["Supplier_fax"].ToString();
                Supplier.Supplier_email = dt.Rows[i]["Supplier_email"].ToString();
                Supplier.Supplier_Status = Convert.ToBoolean(dt.Rows[i]["Supplier_Status"].ToString());
                ListSupplier.Add(Supplier);
            }
            JsonResult json = Json(ListSupplier[0], JsonRequestBehavior.AllowGet);
            return json;
        }
        [HttpGet]
        public int Edit(SupplierModel supplier)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("dbo.Update_Supplier",con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Supplier_id", supplier.Supplier_id);
                com.Parameters.AddWithValue("@Supplier_name", supplier.Supplier_name);
                com.Parameters.AddWithValue("@Supplier_phone", supplier.Supplier_phone);
                com.Parameters.AddWithValue("@Supplier_fax", supplier.Supplier_fax);
                com.Parameters.AddWithValue("@Supplier_address", supplier.Supplier_address);
                com.Parameters.AddWithValue("@Supplier_email", supplier.Supplier_email);
                com.Parameters.AddWithValue("@Supplier_Status", supplier.Supplier_Status);
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
                SqlCommand com = new SqlCommand("dbo.Delete_Supplier", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Supplier_id", ID);
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