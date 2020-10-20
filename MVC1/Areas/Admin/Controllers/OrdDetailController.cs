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
    public class OrdDetailController : Controller
    {
        DataAccess da = new DataAccess();
        string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        // GET: Admin/OrdDetail
        public ActionResult Index()
        {
            List<OrdDetailModel> ListordDetail = new List<OrdDetailModel>();
            DataTable dt = da.GetTable("dbo.Get_Ordertail");
            da.com.CommandType = CommandType.StoredProcedure;
            for(int i = 0;i<dt.Rows.Count;i++)
            {
                var ordDetail = new OrdDetailModel();
                ordDetail.Oders_id = Convert.ToInt32(dt.Rows[i]["Oders_id"].ToString());
                ordDetail.Product_name = dt.Rows[i]["Product_name"].ToString();
                ordDetail.Product_image = dt.Rows[i]["Product_image"].ToString();
                ordDetail.Price = dt.Rows[i]["Price"].ToString();
                //ordDetail.Product_Discount = Convert.ToInt32(dt.Rows[i]["Product_Discount"].ToString());
                ordDetail.Product_number = Convert.ToInt32(dt.Rows[i]["Product_number"].ToString());
                ordDetail.Amount = dt.Rows[i]["Amount"].ToString();
                ordDetail.Ordetais_Status = Convert.ToBoolean(dt.Rows[i]["Ordetais_Status"].ToString());
                ListordDetail.Add(ordDetail);
            }
            return View(ListordDetail);
        }
    }
}