using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MVC1.Areas.Admin.Models;

namespace MVC1.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        DataAccess da = new DataAccess();
        string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        // GET: Admin/Order
        public ActionResult Index()
        {
            List<OrderModel> listOrder = new List<OrderModel>();
            DataTable dt = da.GetTable("EXEC dbo.Get_Order");
            da.com.CommandType = CommandType.StoredProcedure;
            for(int i=0;i<dt.Rows.Count;i++)
            {
                var Order = new OrderModel();
                Order.Orders_id = Convert.ToInt32(dt.Rows[i]["Orders_id"].ToString());
                Order.Ten = dt.Rows[i]["Ten"].ToString();
                Order.Customer_name = dt.Rows[i]["Customer_name"].ToString();
                Order.Order_date = Convert.ToDateTime(dt.Rows[i]["Order_date"].ToString());
                //Order.Delivery_date = Convert.ToDateTime(dt.Rows[i]["Delivery_date"].ToString());
                Order.Adress = dt.Rows[i]["Adress"].ToString();
                Order.Order_Status = Convert.ToBoolean(dt.Rows[i]["Order_Status"].ToString());
                listOrder.Add(Order);
            }
            return View(listOrder);
        }
        [HttpGet]
        public int Add(OrderModel orderModel)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("dbo.Add_Bill", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Customer_id",orderModel.Customer_id);
                com.Parameters.AddWithValue("@Admin_id", orderModel.Admin_id);
                com.Parameters.AddWithValue("@Order_date", orderModel.Order_date);
                //com.Parameters.AddWithValue("@Delivery_date", orderModel.Delivery_date);
                com.Parameters.AddWithValue("@Adress", orderModel.Adress);
                com.Parameters.AddWithValue("@Product_id", orderModel.Product_id);
                com.Parameters.AddWithValue("@Product_number", orderModel.Product_number);
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
        public int Access(int ID)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("EXEC dbo.Action_Bill @Oders_id='" + ID + "'", con);
                //com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Oders_id", ID);
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
        public int Delete(int ID)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("EXEC dbo.Delete_Bill @Orders_id='"+ID+"'",con);
                com.Parameters.AddWithValue("@Orders_id", ID);
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