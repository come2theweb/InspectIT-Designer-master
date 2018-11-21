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
    public partial class AddPlumberCertificate : System.Web.UI.Page
    {
        public int MaxNoCert = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

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

            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                btn_add.Visible = false;
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {

            }
            if (!IsPostBack)
            {
                
                int NoStockCertificates = 0;

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select count(*) as Total from COCStatements where isstock = '1' and userid = '0' and SupplierID = '0'";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["Total"] != DBNull.Value && theSqlDataReader["Total"].ToString() != "0")
                        {
                            NoStockCertificates = Convert.ToInt32(theSqlDataReader["Total"]);
                            MaxNoCert = NoStockCertificates;
                            successmsg.InnerHtml = "There are " + MaxNoCert.ToString() + " available stock certificates to assign.";
                            successmsg.Visible = true;
                        }
                        else
                        {
                            NoStockCertificates = 0;
                            errormsg.InnerHtml = "There is no stock avaliable to supply paper certificates. please create stock first.";
                            errormsg.Visible = true;
                            //btn_add.Enabled = false;
                        }
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                StartRange.Items.Clear();
                StartRange.Items.Add(new ListItem("", ""));

                EndRange.Items.Clear();
                EndRange.Items.Add(new ListItem("", ""));

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCStatements where isstock = '1' and userid = '0' and SupplierID = '0'";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        StartRange.Items.Add(new ListItem(theSqlDataReader["COCNumber"].ToString(), theSqlDataReader["COCStatementID"].ToString()));
                        EndRange.Items.Add(new ListItem(theSqlDataReader["COCNumber"].ToString(), theSqlDataReader["COCStatementID"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                // LOAD SUPPLIERS
                SupplierID.Items.Clear();
                SupplierID.Items.Add(new ListItem("", ""));

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Users where isActive = '1' and Role='Staff'";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        SupplierID.Items.Add(new ListItem(theSqlDataReader["fName"].ToString() + " " + theSqlDataReader["lName"].ToString(), theSqlDataReader["UserID"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                DLdb.DB_Close();
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            errormsg.Visible = false;

            //string SupplierID = DLdb.Decrypt(Request.QueryString["sid"]);
            int sRange = Convert.ToInt32(StartRange.SelectedValue);
            int eRange = Convert.ToInt32(EndRange.SelectedValue);
            int curAmount = 1;

            for (int i = sRange; i < eRange; i++)
            {
                curAmount++;
            }

            NoCertificates.Text = curAmount.ToString();
            string UserID = "";
            string AddressLine2 = "";
            string AddressLine1 = "";
            string Province = "";
            string CitySuburb = "";
            string AreaCode = "";
            string SupplierName = "";
            string SupplierContactNo = "";
            string SupplierEmail = "";
            string fName = "";
            string email = "";
            int supplierStart =0;
            int supplierEnd = 0;
            int supplierTotal = 0;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", SupplierID.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                UserID = theSqlDataReader["UserID"].ToString();
                AddressLine1 = theSqlDataReader["ResidentialStreet"].ToString();
                AddressLine2 = "";
                Province = theSqlDataReader["Province"].ToString();
                CitySuburb = theSqlDataReader["ResidentialSuburb"].ToString();
                AreaCode = theSqlDataReader["ResidentialCode"].ToString();
                SupplierName = theSqlDataReader["fName"].ToString() + " " + theSqlDataReader["lName"].ToString();
                SupplierContactNo = theSqlDataReader["Contact"].ToString();
                SupplierEmail = theSqlDataReader["Email"].ToString();
                //supplierTotal = Convert.ToInt32(theSqlDataReader["TotalNumber"].ToString());
                //supplierEnd = Convert.ToInt32(theSqlDataReader["StartRange"].ToString());
                //supplierStart = Convert.ToInt32(theSqlDataReader["EndRange"].ToString());
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            supplierTotal = supplierTotal + Convert.ToInt32(NoCertificates.Text.ToString());
            supplierStart = supplierStart + Convert.ToInt32(StartRange.Text.ToString());
            supplierEnd = supplierEnd + Convert.ToInt32(EndRange.Text.ToString());
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                fName = theSqlDataReader["fname"].ToString();
                email = theSqlDataReader["email"].ToString();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            for (int i = sRange; i < eRange; i++)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Update COCStatements set UserID = @UserID,status='Non-Logged',DatePurchased=getdate(), isStock = '0' where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", UserID.ToString());
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", i);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                DLdb.SQLST.Parameters.AddWithValue("Message", "Certificate assigned to plumber: " + SupplierName);
                DLdb.SQLST.Parameters.AddWithValue("Username", Session["IIT_UName"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
                DLdb.SQLST.Parameters.AddWithValue("CertificateID", i.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }

            // ADD LAST
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Update COCStatements set UserID = @UserID,status='Non-Logged',DatePurchased=getdate(), isStock = '0' where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", UserID.ToString());
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", EndRange.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
            DLdb.SQLST.Parameters.AddWithValue("Message", "Certificate assigned to plumber: " + SupplierName);
            DLdb.SQLST.Parameters.AddWithValue("Username", Session["IIT_UName"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
            DLdb.SQLST.Parameters.AddWithValue("CertificateID", EndRange.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            string HTMLSubject = "Inspect IT Certificates.";
            string HTMLBody = "Dear " + fName.ToString() + "<br /><br />Paper certificates have been allocated to you" +
            "<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Administrator";
            DLdb.sendEmail(HTMLBody, HTMLSubject, "veronike@slugg.co.za", email.ToString(), "");
            
            DLdb.DB_Close();
            Response.Redirect("ViewPlumbers.aspx?msg=" + DLdb.Encrypt("Certificate has been added successfully"));
        }
        
    }
}