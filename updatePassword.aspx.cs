using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace InspectIT
{
    public partial class updatePassword : System.Web.UI.Page
    {
        public string oldPasswordChange = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
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
                Div1.InnerHtml = msg;
                Div1.Visible = true;
            }
            if (!IsPostBack)
            {


            }
            //string OTPCode = DLdb.CreateNumber(5);
            //Session["otpCodeChangPass"] = OTPCode;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    oldPasswordChange = DLdb.Decrypt(theSqlDataReader["Password"].ToString());
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            DLdb.DB_Close();
        }

        protected void btn_buy_Click(object sender, EventArgs e)
        {

            Global DLdb = new Global();
            DLdb.DB_Connect();

            string sessionPass = Session["otpCodeChangPass"].ToString();

            if (oldPasswordChange.ToString() == oldPassword.Text.ToString())
            {
                if (otpCode.Text.ToString() == sessionPass)
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update Users set password=@password where UserID=@UserID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(newPassword.Text.ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }
                else
                {
                    errormsg.Visible = true;
                    errormsg.InnerHtml = "OTP Incorrect";
                }
            }
            else
            {
                errormsg.Visible = true;
                errormsg.InnerHtml = "The Old password you entered is incorrect";
            }



            DLdb.DB_Close();

        }

    }
}