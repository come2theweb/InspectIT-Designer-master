using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InspectIT
{
    public partial class markRefixFixed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string cocid = "";
            string op = Request.QueryString["op"].ToString();
            if (op == "plumbFix")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCReviews set isfixed='1' where ReviewID = @ReviewID";
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCReviews where ReviewID = @ReviewID";
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    cocid = theSqlDataReader["COCStatementID"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                DLdb.SQLST.Parameters.AddWithValue("Message", "Plumber marked the refix as fixed");
                DLdb.SQLST.Parameters.AddWithValue("Username", Session["IIT_UName"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
                DLdb.SQLST.Parameters.AddWithValue("CertificateID", cocid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string audid = "";
                string auduid = "";
                string plumemail = "";
                string plumname = "";
                string plumnumber = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCStatements where cocstatementid=@cocstatementid";
                DLdb.SQLST.Parameters.AddWithValue("cocstatementid", cocid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    audid = theSqlDataReader["AuditorID"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Auditor where AuditorID=@AuditorID";
                DLdb.SQLST.Parameters.AddWithValue("AuditorID", audid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    auduid = theSqlDataReader["UserID"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", auduid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    plumemail = theSqlDataReader["email"].ToString();
                    plumnumber = theSqlDataReader["contact"].ToString();
                    plumname = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string HTMLSubject = "Inspect IT - C.O.C Refix Completed";
                string HTMLBody = "Dear " + plumname.ToString() + "<br /><br />COC Number " + cocid + " has been marked as fixed by the plumber.<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Team";
                DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumemail.ToString(), "");

                DLdb.sendSMS(auduid.ToString(), plumnumber.ToString(), "COC Number " + cocid + " has been marked as fixed by the plumber. ");

            }
            else if (op == "insFixNot")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCReviews set isfixed='0' where ReviewID = @ReviewID";
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string plumid = "";
                string plumemail = "";
                string plumname = "";
                string plumnumber = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCStatements where cocstatementid=@cocstatementid";
                DLdb.SQLST.Parameters.AddWithValue("cocstatementid", DLdb.Decrypt(Request.QueryString["coc"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    plumid = theSqlDataReader["UserID"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", plumid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    plumemail = theSqlDataReader["email"].ToString();
                    plumnumber = theSqlDataReader["contact"].ToString();
                    plumname = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                DLdb.SQLST.Parameters.AddWithValue("Message", "Auditor marked the plumbers fix as not fixed");
                DLdb.SQLST.Parameters.AddWithValue("Username", Session["IIT_UName"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
                DLdb.SQLST.Parameters.AddWithValue("CertificateID", DLdb.Decrypt(Request.QueryString["coc"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string HTMLSubject = "Inspect IT - C.O.C Refix Required";
                string HTMLBody = "Dear " + plumname.ToString() + "<br /><br />COC Number " + DLdb.Decrypt(Request.QueryString["coc"].ToString()) + " has been audited and a refix is required.<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Team";
                DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumemail.ToString(), "");

                DLdb.sendSMS(plumid.ToString(), plumnumber.ToString(), "COC Number " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " has been audited and a refix is required. ");
                
            }
            else
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCReviews set isclosed='1', isfixed='1' where ReviewID = @ReviewID";
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                //DLdb.RS.Open();
                //DLdb.SQLST.CommandText = "update COCStatements set isRefix='0' where ReviewID = @ReviewID";
                //DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                //DLdb.SQLST.CommandType = CommandType.Text;
                //DLdb.SQLST.Connection = DLdb.RS;
                //theSqlDataReader = DLdb.SQLST.ExecuteReader();

                //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCReviews where ReviewID = @ReviewID";
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    cocid = theSqlDataReader["COCStatementID"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                DLdb.SQLST.Parameters.AddWithValue("Message", "Auditor marked the refix as Fixed and closed the problem");
                DLdb.SQLST.Parameters.AddWithValue("Username", Session["IIT_UName"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
                DLdb.SQLST.Parameters.AddWithValue("CertificateID", cocid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                //DLdb.RS.Open();
                //DLdb.SQLST.CommandText = "update cocstatements set isrefix='0' where COCStatementID = @COCStatementID";
                //DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["coc"].ToString()));
                //DLdb.SQLST.CommandType = CommandType.Text;
                //DLdb.SQLST.Connection = DLdb.RS;
                //theSqlDataReader = DLdb.SQLST.ExecuteReader();

                //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.RS.Close();
            }
            

            DLdb.DB_Close();
        }
    }
}