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
    public partial class srv_getCustomerDetails : System.Web.UI.Page
    {
        public class details
        {
            public string CompanyID { get; set; }
            public string CustomerID { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string PhysicalAddress { get; set; }
            public string PhysicalSuburb { get; set; }
            public string PhysicalCity { get; set; }
            public string PhysicalProvince { get; set; }
            public string PhysicalCode { get; set; }
            public string EmailAddress { get; set; }
            public string ContactNumber { get; set; }
            public string MobileNumber { get; set; }
            public string IDNumber { get; set; }
            public string Notes { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string COCNumber = Request.QueryString["ccid"].ToString();
            string json = "";
            // load Customers
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID and isActive = '1'";
            DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", COCNumber);
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from Customers where CustomerID = @CustomerID and isActive = '1'";
                    DLdb.SQLST.Parameters.AddWithValue("CustomerID", theSqlDataReader2["CustomerID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            var Page = new details();
                            Page.CustomerID = theSqlDataReader["CustomerID"].ToString();
                            Page.Name = theSqlDataReader["CustomerName"].ToString();
                            Page.Surname = theSqlDataReader["CustomerSurname"].ToString();
                            Page.PhysicalAddress = theSqlDataReader["AddressStreet"].ToString();
                            Page.PhysicalSuburb = theSqlDataReader["AddressSuburb"].ToString();
                            Page.PhysicalCity = theSqlDataReader["AddressCity"].ToString();
                            Page.PhysicalProvince = theSqlDataReader["Province"].ToString();
                            Page.PhysicalCode = theSqlDataReader["AddressAreaCode"].ToString();
                            Page.EmailAddress = theSqlDataReader["CustomerEmail"].ToString();
                            Page.ContactNumber = theSqlDataReader["CustomerCellNo"].ToString();
                            Page.MobileNumber = theSqlDataReader["CustomerCellNoAlt"].ToString();
                            json = JsonConvert.SerializeObject(Page);
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            

            DLdb.DB_Close();

            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();
        }
    }
}