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
using System.IO;

namespace InspectIT
{
    public partial class AddCertificate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            // ADMIN CHECK
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }

            if (Request.QueryString["msg"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
                successmsg.InnerHtml = msg;
                successmsg.Visible = true;
            }

            if (Request.QueryString["err"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
                errormsg.InnerHtml = msg;
                errormsg.Visible = true;
            }

            DLdb.DB_Connect();
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select top 1 (COCStatementID + 1) as nCOCStatementID from COCStatements order by COCStatementID desc";
            //DLdb.SQLST.Parameters.AddWithValue("Param", "val");
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    StartRange.Text = theSqlDataReader["nCOCStatementID"].ToString();
                }
            }
            else
            {
                StartRange.Text = "0";
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            DLdb.DB_Close();
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();

            DLdb.DB_Connect();
            string OrderID = "";

            // CREATE ORDER
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO Orders (UserID,Description,TotalNoItems,SubTotal,Vat,Delivery,Total,COCType,Method,isPaid) values (@UserID,@Description,@TotalNoItems,@SubTotal,@Vat,@Delivery,@Total,@COCType,@Method,'1'); Select Scope_Identity() as OrderID";

            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("Description", "COC Certificate Stock");
            DLdb.SQLST.Parameters.AddWithValue("TotalNoItems", CetificateNumber.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubTotal", "0");
            DLdb.SQLST.Parameters.AddWithValue("Vat", "0");
            DLdb.SQLST.Parameters.AddWithValue("Delivery", "0");
            DLdb.SQLST.Parameters.AddWithValue("Total", "0");
            DLdb.SQLST.Parameters.AddWithValue("COCType", "Paper");
            DLdb.SQLST.Parameters.AddWithValue("Method", "Printer");

            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                OrderID = theSqlDataReader["OrderID"].ToString();
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            int start = Convert.ToInt32(StartRange.Text.ToString());
            // LOOP THROUGHT NUMBER AND CREATE STATEMENTS
            int cnt = 0;
            int pamount = Convert.ToInt32(CetificateNumber.Text);
            while (cnt <= pamount)
            {
                if (cnt==0)
                {
                    if (start == 0)
                    {
                        start++;
                    }
                }
                else
                {
                    // add the cocstatements to the user
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into COCStatements (OrderID,UserID,Type,DatePurchased,Status, isPaid, isStock,SupplierID,COCNumber) values (@OrderID,@UserID,@Type,getdate(),'', '1', '1',@SupplierID,@COCNumber)";
                    DLdb.SQLST2.Parameters.AddWithValue("OrderID", OrderID);
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", "0");
                    DLdb.SQLST2.Parameters.AddWithValue("SupplierID", "0");
                    DLdb.SQLST2.Parameters.AddWithValue("Type", "Paper");
                    DLdb.SQLST2.Parameters.AddWithValue("COCNumber", start.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                    DLdb.SQLST2.Parameters.AddWithValue("Message", "Paper Certificate Generated");
                    DLdb.SQLST2.Parameters.AddWithValue("Username", "0");
                    DLdb.SQLST2.Parameters.AddWithValue("TrackingTypeID", "0");
                    DLdb.SQLST2.Parameters.AddWithValue("CertificateID", start.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    start++;
                }
                
              
                cnt++;
            }
            
            // CREATE STOCK
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO CertificateStock (UserID,TransactionType,Amount,StartRange,EndRange) values (@UserID,@TransactionType,@Amount,@StartRange,@EndRange);";

            int eRange = Convert.ToInt32(StartRange.Text) + Convert.ToInt32(CetificateNumber.Text);

            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("TransactionType", "OrderID: " + OrderID);
            DLdb.SQLST.Parameters.AddWithValue("Amount", CetificateNumber.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("StartRange", StartRange.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("EndRange", eRange.ToString());

            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();

            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);

            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("ViewCertificate?msg=" + DLdb.Encrypt("Certificates have been generated."));

        }
    }
}