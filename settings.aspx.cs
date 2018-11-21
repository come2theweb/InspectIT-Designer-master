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

namespace InspectIT
{
    public partial class settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }
            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                Button1.Visible = false;
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {
                
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

            if (!IsPostBack)
            {

           
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM settings";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    AuditPercentage.Text = theSqlDataReader["AuditPercentage"].ToString();
                    AditRefixNumber.Text = theSqlDataReader["AditRefixNumber"].ToString();
                    CompanyName.Text = theSqlDataReader["CompanyName"].ToString();
                    CompanyTelephoneNumber.Text = theSqlDataReader["CompanyTelephoneNumber"].ToString();
                    CompanyVatNumber.Text = theSqlDataReader["CompanyVatNumber"].ToString();
                    EskomRebateAuditPercentage.Text = theSqlDataReader["EskomRebateAuditPercentage"].ToString();
                    NextRandomAuditRunDate.Text = theSqlDataReader["NextRandomAuditRunDate"].ToString();
                    OfficeHours.Text = theSqlDataReader["OfficeHours"].ToString();
                    PirbOrderEmail.Text = theSqlDataReader["PirbOrderEmail"].ToString();
                    PirbOrderFax.Text = theSqlDataReader["PirbOrderFax"].ToString();
                    PlumberMaxNonLoggedCertificates.Text = theSqlDataReader["PlumberMaxNonLoggedCertificates"].ToString();
                    RefixPeriod.Text = theSqlDataReader["RefixPeriod"].ToString();
                    SystemEmailAddress.Text = theSqlDataReader["SystemEmailAddress"].ToString();
                    VatPercentage.Text = theSqlDataReader["VatPercentage"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();
            }
            DLdb.DB_Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update settings set AuditPercentage=@AuditPercentage,AditRefixNumber=@AditRefixNumber,CompanyName=@CompanyName,CompanyTelephoneNumber=@CompanyTelephoneNumber,CompanyVatNumber=@CompanyVatNumber," +
                "EskomRebateAuditPercentage=@EskomRebateAuditPercentage,NextRandomAuditRunDate=@NextRandomAuditRunDate,OfficeHours=@OfficeHours,PirbOrderEmail=@PirbOrderEmail," +
                "PirbOrderFax=@PirbOrderFax,PlumberMaxNonLoggedCertificates=@PlumberMaxNonLoggedCertificates,RefixPeriod=@RefixPeriod,SystemEmailAddress=@SystemEmailAddress,VatPercentage=@VatPercentage where ID='1'";
            DLdb.SQLST.Parameters.AddWithValue("AuditPercentage", AuditPercentage.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AditRefixNumber", AditRefixNumber.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyName", CompanyName.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyTelephoneNumber", CompanyTelephoneNumber.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyVatNumber", CompanyVatNumber.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("EskomRebateAuditPercentage", EskomRebateAuditPercentage.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("NextRandomAuditRunDate", NextRandomAuditRunDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("OfficeHours", OfficeHours.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PirbOrderEmail", PirbOrderEmail.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PirbOrderFax", PirbOrderFax.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PlumberMaxNonLoggedCertificates", PlumberMaxNonLoggedCertificates.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("RefixPeriod", RefixPeriod.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SystemEmailAddress", SystemEmailAddress.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("VatPercentage", VatPercentage.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            
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
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            DLdb.DB_Close();
        }
    }
}