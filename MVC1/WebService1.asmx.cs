using MVC1.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Xml;

namespace MVC1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://pragimtech.com/webservices")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["myConnectionString"].ToString();
            con = new SqlConnection(constring);
        }
        DataAccess da = new DataAccess();

        [WebMethod]
        [ScriptMethod(ResponseFormat=ResponseFormat.Json)]
        public void GetCatalog()
        {
            connection();
            CatalogModel[] studentlist = null;
            SqlCommand cmd = new SqlCommand("dbo.select_Catalog", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sd.Fill(dt);
            con.Close();
            studentlist = new CatalogModel[dt.Rows.Count];
            int Count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                studentlist[Count] = new CatalogModel();
                studentlist[Count] = new CatalogModel();
                studentlist[Count].Catalogs_id = Convert.ToInt32(dt.Rows[i]["Catalogs_id"].ToString());
                studentlist[Count].Catalogs_name = dt.Rows[i]["Catalogs_name"].ToString();
                studentlist[Count].Catalogs_Status = Convert.ToBoolean(dt.Rows[i]["Catalogs_Status"].ToString());
                Count++;
            }
            dt.Clear();
        }
        [WebMethod]
        public void GetList()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            CatalogModel[] CatalogList = null;
            DataTable dt = da.GetTable("EXEC dbo.select_Catalog");
            da.com.CommandType = CommandType.StoredProcedure;
            CatalogList = new CatalogModel[dt.Rows.Count];
            int Count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CatalogList[Count] = new CatalogModel();
                CatalogList[Count].Catalogs_id = Convert.ToInt32(dt.Rows[i]["Catalogs_id"].ToString());
                CatalogList[Count].Catalogs_name = dt.Rows[i]["Catalogs_name"].ToString();
                CatalogList[Count].Catalogs_Status = Convert.ToBoolean(dt.Rows[i]["Catalogs_Status"].ToString());
                Count++;
            }
            dt.Clear();
            var json = new
            {
                //Tên mảng
                CatalogList = CatalogList
            };
            HttpContext.Current.Response.Write(ser.Serialize(json));
        }
        [WebMethod]
        public void Get2()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            CatalogModel[] CatalogList = null;
            DataTable dt = da.GetTable("EXEC dbo.select_Catalog");
            da.com.CommandType = CommandType.StoredProcedure;
            CatalogList = new CatalogModel[dt.Rows.Count];
            int Count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CatalogList[Count] = new CatalogModel();
                CatalogList[Count].Catalogs_id = Convert.ToInt32(dt.Rows[i]["Catalogs_id"].ToString());
                CatalogList[Count].Catalogs_name = dt.Rows[i]["Catalogs_name"].ToString();
                CatalogList[Count].Catalogs_Status = Convert.ToBoolean(dt.Rows[i]["Catalogs_Status"].ToString());
                Count++;
            }
            dt.Clear();
            var json = new
            {
                CatalogList = CatalogList
            };
            HttpContext.Current.Response.Write(ser.Serialize(json));
            string objActivityDetails = JsonConvert.SerializeObject(json);
            XmlDocument xmlObject = JsonConvert.DeserializeXmlNode("{\"CatalogList\":" + objActivityDetails.ToString() + "}");

        }
        [WebMethod]
        public List<CatalogModel> GetID(int id)
        {
            List<CatalogModel> CatalogList = new List<CatalogModel>();
            DataTable dt = da.GetTable("EXEC Select_CatalogId @Catalogs_id=" + id);
            da.com.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var Catalog = new CatalogModel();
                Catalog.Catalogs_id = Convert.ToInt32(dt.Rows[i]["Catalogs_id"].ToString());
                Catalog.Catalogs_name = dt.Rows[i]["Catalogs_name"].ToString();
                //Catalog.Catalogs_image = dt.Rows[i]["Catalogs_image"].ToString();
                Catalog.Catalogs_Status = Convert.ToBoolean(dt.Rows[i]["Catalogs_Status"].ToString());
                CatalogList.Add(Catalog);
            }
            return CatalogList;
        }
        [WebMethod]
        public bool Add(string Catalogs_name,string Catalogs_image,string Catalogs_Status)
        {
            connection();
            SqlCommand com = new SqlCommand("dbo.Insert_Catalogs", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Catalogs_name", Catalogs_name);
            com.Parameters.AddWithValue("@Catalogs_image", Catalogs_image);
            com.Parameters.AddWithValue("@Catalogs_Status", Catalogs_Status);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        [WebMethod]
        public bool Edit(string Catalogs_id, string Catalogs_name, string Catalogs_image, string Catalogs_Status)
        {
            connection();
            SqlCommand com = new SqlCommand("dbo.Update_Catalos", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Catalogs_id",Catalogs_id);
            com.Parameters.AddWithValue("@Catalogs_name", Catalogs_name);
            com.Parameters.AddWithValue("@Catalogs_image",Catalogs_image);
            com.Parameters.AddWithValue("@Catalogs_Status", Catalogs_Status);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        [WebMethod]
        public bool Delete(int id)
        {
            //return "tại sao không được";
            connection();
            SqlCommand com = new SqlCommand("dbo.Delete_Api", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Catalogs_id", id);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}
