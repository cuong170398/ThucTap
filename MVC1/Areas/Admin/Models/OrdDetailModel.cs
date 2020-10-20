using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC1.Areas.Admin.Models
{
    public class OrdDetailModel
    {
        public int Ordetail_id { set; get; }
        public int Oders_id { set; get; }
        public int Product_id { set; get; }
        public int Product_number { set; get; }
        public string Amount { set; get; }
        public Boolean Ordetais_Status { set; get; }
        public string Product_name { set; get; }
        public string Product_image { set; get; }
        public decimal Product_Price { set; get; }
        public float Product_Discount { set; get; }
        public String TongTien { set; get; }
        public string Price { set; get; }


    }
}