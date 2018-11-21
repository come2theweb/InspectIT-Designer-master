using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace InspectIT.API
{
    public partial class srv_getCOCDetails : System.Web.UI.Page
    {
        public class details
        {
            public string COCSattementID { get; set; }
            public string CompletedDate { get; set; }
            public string COCType { get; set; }
            public string InsuranceCompany { get; set; }
            public string PolicyHolder { get; set; }
            public string PolicyNumber { get; set; }
            public string isBank { get; set; }
            public string PeriodOfInsuranceFrom { get; set; }
            public string PeriodOfInsuranceTo { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string COCNumber = Request.QueryString["ccid"].ToString();
            string json = "";
            // load Customers
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatementDetails where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", COCNumber);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    var Page = new details();
                    Page.COCSattementID = theSqlDataReader["COCStatementID"].ToString();
                    Page.CompletedDate = theSqlDataReader["CompletedDate"].ToString();
                    Page.COCType = theSqlDataReader["COCType"].ToString();
                    Page.InsuranceCompany = theSqlDataReader["InsuranceCompany"].ToString();
                    Page.PolicyHolder = theSqlDataReader["PolicyHolder"].ToString();
                    Page.PolicyNumber = theSqlDataReader["PolicyNumber"].ToString();
                    Page.isBank = theSqlDataReader["isBank"].ToString();
                    Page.PeriodOfInsuranceFrom = theSqlDataReader["PeriodOfInsuranceFrom"].ToString();
                    Page.PeriodOfInsuranceTo = theSqlDataReader["PeriodOfInsuranceTo"].ToString();
                    json = JsonConvert.SerializeObject(Page);
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
                



            DLdb.DB_Close();

            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();
        }
    }
}