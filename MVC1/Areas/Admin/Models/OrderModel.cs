using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC1.Areas.Admin.Models
{
    public class OrderModel
    {
        public int Orders_id { set; get; }
        public int Customer_id { set; get; }
        public int Admin_id { set; get; }
        [DataType(DataType.Date)]
        public DateTime Order_date { set; get; }
        [DataType(DataType.Date)]
        public DateTime Delivery_date { set; get; }
        public string Adress { set; get; }
        public Boolean Order_Status { set; get; }
        public string Ten { set; get; }
        public string Customer_name { set; get; }
        public int Product_id { set; get; }
        public string Product_number { set; get; }
    }
}