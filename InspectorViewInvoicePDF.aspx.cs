using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.Net;

namespace InspectIT
{
    public partial class InspectorViewInvoicePDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();


            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            // INSPECTOR CHECK
            if (Session["IIT_Role"].ToString() != "Inspector")
            {
                Response.Redirect("Default");
            }
            

            DLdb.DB_Connect();

            UserInvoice.InnerHtml = "";

            // GET PDF
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCInspectors where AuditID=@AuditID";
            DLdb.SQLST.Parameters.AddWithValue("AuditID", DLdb.Decrypt(Request.QueryString["aid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {

                    UserInvoice.InnerHtml = "<embed src=\"https://197.242.82.242/inspectit/Inspectorinvoices/" + theSqlDataReader["Invoice"].ToString()+"\" type=\"application/pdf\" width=\"100%\" height=\"900\" />";
                    
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            DLdb.DB_Close();
        }
    }
}