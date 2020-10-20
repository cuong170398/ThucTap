using MVC1.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace MVC1.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        DataAccess da = new DataAccess();
        // GET: Admin/Customer
        public ActionResult Index()
        {
            List<CustomerModel> listCustomer = new List<CustomerModel>();
            DataTable dt = da.GetTable("EXEC dbo.Get_Customer");
            da.com.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var customer = new CustomerModel();
                customer.Customer_id = Convert.ToInt32(dt.Rows[i]["Customer_id"].ToString());
                customer.Customer_name = dt.Rows[i]["Customer_name"].ToString();
                customer.Customer_phone = dt.Rows[i]["Customer_phone"].ToString();
                customer.Customer_Address = dt.Rows[i]["Customer_Address"].ToString();
                customer.Customer_Email = dt.Rows[i]["Customer_Email"].ToString();
                customer.Customer_Pass = dt.Rows[i]["Customer_Pass"].ToString();
                customer.Customer_Birthday = Convert.ToDateTime(dt.Rows[i]["Customer_Birthday"].ToString());
                customer.Customer_Gender = dt.Rows[i]["Customer_Gender"].ToString();
                customer.Customer_Status = Boolean.Parse(dt.Rows[i]["Customer_Status"].ToString());
                listCustomer.Add(customer);
            }
            return View(listCustomer);
        }
        public int Add(CustomerModel customer)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("dbo.Insert_Customer", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Customer_name", customer.Customer_name);
                com.Parameters.AddWithValue("@Customer_phone", customer.Customer_phone);
                com.Parameters.AddWithValue("@Customer_Address", customer.Customer_Address);
                com.Parameters.AddWithValue("@Customer_Email", customer.Customer_Email);
                com.Parameters.AddWithValue("@Customer_Pass", customer.Customer_Pass);
                com.Parameters.AddWithValue("@Customer_Birthday", customer.Customer_Birthday);
                com.Parameters.AddWithValue("@Customer_Gender", customer.Customer_Gender);
                com.Parameters.AddWithValue("@Customer_Status", customer.Customer_Status);
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
            List<CustomerModel> ListCustomer = new List<CustomerModel>();
            DataTable dt = da.GetTable("EXEC dbo.Get_CustomerId @Customer_id='" + ID + "'");
            da.com.CommandType = CommandType.StoredProcedure;
            for(int i=0;i<dt.Rows.Count;i++)
            {
                var Customer = new CustomerModel();
                Customer.Customer_id = Convert.ToInt32(dt.Rows[i]["Customer_id"].ToString());
                Customer.Customer_name = dt.Rows[i]["Customer_name"].ToString();
                Customer.Customer_phone = dt.Rows[i]["Customer_phone"].ToString();
                Customer.Customer_Address = dt.Rows[i]["Customer_Address"].ToString();
                Customer.Customer_Email = dt.Rows[i]["Customer_Email"].ToString();
                Customer.Customer_Pass = dt.Rows[i]["Customer_Pass"].ToString();
                Customer.Customer_Birthday = Convert.ToDateTime(dt.Rows[i]["Customer_Birthday"].ToString());
                Customer.Customer_Gender = dt.Rows[i]["Customer_Gender"].ToString();
                Customer.Customer_Status = Convert.ToBoolean(dt.Rows[i]["Customer_Status"].ToString());
                ListCustomer.Add(Customer);
            }
            JsonResult json = Json(ListCustomer[0], JsonRequestBehavior.AllowGet);
            return json;
        }
        [HttpGet]
        public int Edit(CustomerModel customer)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("dbo.Update_Customer", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Customer_id", customer.Customer_id);
                com.Parameters.AddWithValue("@Customer_name", customer.Customer_name);
                com.Parameters.AddWithValue("@Customer_phone", customer.Customer_phone);
                com.Parameters.AddWithValue("@Customer_Address", customer.Customer_Address);
                com.Parameters.AddWithValue("@Customer_Email", customer.Customer_Email);
                com.Parameters.AddWithValue("@Customer_Pass", customer.Customer_Pass);
                com.Parameters.AddWithValue("@Customer_Birthday", customer.Customer_Birthday);
                com.Parameters.AddWithValue("@Customer_Gender", customer.Customer_Gender);
                com.Parameters.AddWithValue("@Customer_Status", customer.Customer_Status);
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
        [HttpPost]
        public int Delete(int ID)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("dbo.Delete_Customer", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Customer_id", ID);
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
    }
}