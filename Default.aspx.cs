using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;

namespace InspectIT
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            if (Request.QueryString["msg"] != null)
            {
                //Global DLdb = new Global();
                //string msg = DLdb.Decrypt(Request.QueryString["msg"].ToString());
                //successbox.InnerHtml = msg;
                //successbox.Visible = true;
            }


            //Response.Write(DLdb.Decrypt("xq|olNBtpucMax5dR2yJgKGQoyZxdXn5G2vwDGC3W8WullJeFOZOAw~~"));
            //Response.End();
        }

        protected void SignIN_Click(object sender, EventArgs e)
        {
            // SET LOGIN VALUE = FALSE
            string Loggedin = "false";

            Global DLdb = new Global();
            DLdb.DB_Connect();

            DateTime expireDate;
            DateTime now = DateTime.Now;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from Users where (email = @username and password = @password and isActive = '1' and isSuspended = '0') or (regno = @username and password = @password and isActive = '1' and isSuspended = '0')";
            DLdb.SQLST.Parameters.AddWithValue("username", username.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(password.Text.ToString()));
            //DLdb.SQLST.Parameters.AddWithValue("password", password.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                if (theSqlDataReader["RegistrationEnd"].ToString() != "" && theSqlDataReader["RegistrationEnd"] != DBNull.Value)
                {
                    expireDate = Convert.ToDateTime(theSqlDataReader["RegistrationEnd"].ToString());
                }
                else
                {
                    expireDate = Convert.ToDateTime("1900-01-01");
                }
                
                // SET SESSION OBJECTS
                Session["IIT_UID"] = theSqlDataReader["UserID"].ToString();
                Session["IIT_RegNo"] = theSqlDataReader["RegNo"].ToString();
                Session["IIT_isSuspended"] = theSqlDataReader["isSuspended"].ToString();
                if (expireDate < now)
                {
                    Session["IIT_isSuspended"] = "Expired";
                }
                else
                {
                    Session["IIT_isSuspended"] = "NotExpired";
                }
                Session["IIT_UName"] = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                Session["IIT_UfName"] = theSqlDataReader["fname"].ToString();
                Session["IIT_UsName"] = theSqlDataReader["lname"].ToString();
                Session["IIT_EmailAddress"] = theSqlDataReader["email"].ToString();
                if (theSqlDataReader["role"].ToString() == "AdminRights")
                {
                    Session["IIT_Role"] = "Administrator";
                }
                else
                {
                    Session["IIT_Role"] = theSqlDataReader["role"].ToString();
                }
                Session["IIT_Rights"] = theSqlDataReader["rights"].ToString();
                Session["IIT_tempID"] = DLdb.CreatePassword(6);
                // SET LOGIN VALUE = TRUE
                Loggedin = "True";
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "insert into LoggedInRecords (UserID) values (@UserID)";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                
                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            else
            {
                // SHOW ERROR MESSAGE
                errorbox.InnerHtml = "Email address / Password invalid.";
                errorbox.Visible = true;
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            //Response.Write("ROLE: " + Session["IIT_Role"].ToString());
            //Response.End();


            // CHECK LOGIN VALUE
            if (Loggedin == "True")
            {
                if (Session["IIT_Role"].ToString() == "Administrator")
                {
                    Response.Redirect("HomeCOCAdmin");
                }
                else if (Session["IIT_Role"].ToString() == "Supplier")
                {
                    
                    Response.Redirect("PurchasePlumbingCOCSupplier");
                }
                else if (Session["IIT_Role"].ToString() == "Staff")
                {
                    Response.Redirect("userDashboard");
                }
                else if (Session["IIT_Role"].ToString() == "Inspector")
                {
                    Response.Redirect("ViewCOCStatementInspector");
                }
                

            }
        }
        
    }
}