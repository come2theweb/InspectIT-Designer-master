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
    public partial class AssignInspectorProcess : System.Web.UI.Page
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

            // ADMIN CHECK
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCInspectors where COCStatementID=@COCStatementID";
            //DLdb.SQLST.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["iid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "update COCInspectors set UserID=@UserID,Status = 'Auditing',isActive='1' where COCStatementID=@COCStatementID";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["iid"].ToString()));
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
                
            }
            else
            {
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "insert into COCInspectors (UserID, COCStatementID, Status) values (@UserID, @COCStatementID, 'Auditing')";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["iid"].ToString()));
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            string NumberTo = "";
            string EmailAddress = "";
            string FullName = "";
            string AuditorID = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Auditor where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["iid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                NumberTo = theSqlDataReader["phoneMobile"].ToString();
                AuditorID = theSqlDataReader["AuditorID"].ToString();
                EmailAddress = theSqlDataReader["email"].ToString();
                FullName = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string plumberid = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatements where CocstatementID = @CocstatementID";
            DLdb.SQLST.Parameters.AddWithValue("CocstatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                plumberid = theSqlDataReader["userid"].ToString();
                
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string plumberEmail = "";
            string plumberContact = "";
            string plumberName = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", plumberid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                plumberEmail = theSqlDataReader["Email"].ToString();
                plumberContact = theSqlDataReader["contact"].ToString();
                plumberName = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();

            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatements set isAudit = '1',status='Auditing',AuditorID=@AuditorID,AssignedDate=getdate() where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("AuditorID", AuditorID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
            DLdb.SQLST.Parameters.AddWithValue("Message", "Certificate assigned to auditor: " + FullName);
            DLdb.SQLST.Parameters.AddWithValue("Username", "Admin");
            DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
            DLdb.SQLST.Parameters.AddWithValue("CertificateID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            // SMS
            if (NumberTo.ToString() != "")
            {
                DLdb.sendSMS(plumberid, plumberContact, "Inspect-It: COC "+ DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " has been selected for an Audit you will be contacted shortly.");
                DLdb.sendSMS(DLdb.Decrypt(Request.QueryString["iid"].ToString()), NumberTo, "Inspect-It: You have been assigned to audit a C.O.C, please login to Inspect-It to view.");
            }

            // EMAIL
            if (EmailAddress.ToString() != "")
            {
                string eHTMLBody = "Dear " + FullName + "<br /><br />You have been assigned to a C.O.C Statement on the Inspect-It system, please <a href='https://197.242.82.242/inspectit/'>login</a> to view.<br /><br />Regards<br />Inspect-It Administrator";
                string eSubject = "Inspect-IT New C.O.C Statement Audit";
                DLdb.sendEmail(eHTMLBody, eSubject, "mathewpayne@gmail.com", EmailAddress,"");
            }

            DLdb.DB_Close();
            Response.Redirect("ViewCOCStatementAdmin?cocid=" + Request.QueryString["cocid"].ToString() + "&msg=" + DLdb.Encrypt("Auditor assigned to C.O.C Statement"));
        }

        
    }
}