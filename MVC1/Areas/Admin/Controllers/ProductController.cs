using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC1.Areas.Admin.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MVC1.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        DataAccess da = new DataAccess();
        string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        // GET: Admin/Product
        public ActionResult Index()
        {
            List<ProductModel> listProduct = new List<ProductModel>();
            DataTable dt = da.GetTable("EXEC dbo.Get_Product");
            da.com.CommandType = CommandType.StoredProcedure;
            for(int i = 0;i<dt.Rows.Count;i++)
            {
                var product = new ProductModel();
                product.Product_id = Convert.ToInt32(dt.Rows[i]["Product_id"].ToString());
                product.Catalogs_name = dt.Rows[i]["Catalogs_name"].ToString();
                product.Supplier_name = dt.Rows[i]["Supplier_name"].ToString();
                product.Product_name = dt.Rows[i]["Product_name"].ToString();
                product.Product_Price = Convert.ToDecimal(dt.Rows[i]["Product_Price"].ToString());
                product.Product_Discount = Convert.ToInt32(dt.Rows[i]["Product_Discount"].ToString());
                product.Product_image = dt.Rows[i]["Product_image"].ToString();
                product.Product_listimage = dt.Rows[i]["Product_listimage"].ToString();
                product.Product_number = Convert.ToInt32(dt.Rows[i]["Product_number"].ToString());
                product.Product_content = dt.Rows[i]["Product_content"].ToString();
                product.Product_Status = Convert.ToBoolean(dt.Rows[i]["Product_Status"].ToString());
                listProduct.Add(product);
            }
            return View(listProduct);
        }
        [HttpPost]
        public int Add(ProductModel product)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("dbo.Insert_Product", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Catalogs_id", product.Catalogs_id);
                com.Parameters.AddWithValue("@Supplier_id", product.Supplier_id);
                com.Parameters.AddWithValue("@Product_name", product.Product_name);
                com.Parameters.AddWithValue("@Product_Price", product.Product_Price);
                com.Parameters.AddWithValue("@Product_Discount", product.Product_Discount);
                com.Parameters.AddWithValue("@Product_image", product.Product_image);
                com.Parameters.AddWithValue("@Product_number", product.Product_number);
                com.Parameters.AddWithValue("@Product_content", product.Product_content);
                com.Parameters.AddWithValue("@Product_Status", product.Product_Status);
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
            List<ProductModel> listProduct = new List<ProductModel>();
            DataTable dt = da.GetTable("EXEC dbo.Get_ProductById @Product_id = " + ID);
            da.com.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var product = new ProductModel();
                product.Product_id = Convert.ToInt32(dt.Rows[i]["Product_id"].ToString());
                product.Catalogs_id = Convert.ToInt32(dt.Rows[i]["Catalogs_id"].ToString());
                product.Supplier_id = Convert.ToInt32(dt.Rows[i]["Supplier_id"].ToString());
                product.Product_name = dt.Rows[i]["Product_name"].ToString();
                product.Product_Price = Convert.ToDecimal(dt.Rows[i]["Product_Price"].ToString());
                product.Product_Discount = Convert.ToInt32(dt.Rows[i]["Product_Discount"].ToString());
                product.Product_image = dt.Rows[i]["Product_image"].ToString();
                product.Product_listimage = dt.Rows[i]["Product_listimage"].ToString();
                product.Product_number = Convert.ToInt32(dt.Rows[i]["Product_number"].ToString());
                product.Product_content = dt.Rows[i]["Product_content"].ToString();
                product.Product_Status = Convert.ToBoolean(dt.Rows[i]["Product_Status"].ToString());
                listProduct.Add(product);
            }
            JsonResult json = Json(listProduct[0], JsonRequestBehavior.AllowGet);
            return json;
        }
        [HttpPost]
        public int Edit(ProductModel product)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                int i;
                con.Open();
                SqlCommand com = new SqlCommand("[dbo].[Edit_Product]", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Product_id", product.Product_id);
                com.Parameters.AddWithValue("@Catalogs_id", product.Catalogs_id);
                com.Parameters.AddWithValue("@Supplier_id", product.Supplier_id);
                com.Parameters.AddWithValue("@Product_name", product.Product_name);
                com.Parameters.AddWithValue("@Product_Price", product.Product_Price);
                com.Parameters.AddWithValue("@Product_Discount", product.Product_Discount);
                com.Parameters.AddWithValue("@Product_image", product.Product_image);
                //com.Parameters.AddWithValue("@Product_listimage", product.Product_listimage);
                com.Parameters.AddWithValue("@Product_number", product.Product_number);
                com.Parameters.AddWithValue("@Product_content", product.Product_content);
                com.Parameters.AddWithValue("@Product_Status", product.Product_Status);
                i = com.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex)
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
                SqlCommand com = new SqlCommand("dbo.Delete_Product", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Product_id", ID);
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