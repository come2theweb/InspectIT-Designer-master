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
    public partial class UserSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Global DLdb = new Global();

            //// CHECK SESSION
            //if (Session["IIT_UID"] == null)
            //{
            //    Response.Redirect("Default");
            //}

            //// ADMIN CHECK
            //if (Session["IIT_Role"].ToString() != "Administrator")
            //{
            //    Response.Redirect("Default");
            //}

            ////if (Request.QueryString["msg"] != null)
            ////{
            ////    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
            ////    successmsg.InnerHtml = msg;
            ////    successmsg.Visible = true;
            ////}

            ////if (Request.QueryString["err"] != null)
            ////{
            ////    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
            ////    errormsg.InnerHtml = msg;
            ////    errormsg.Visible = true;
            ////}

            ////if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.RS.Close();

            //DLdb.DB_Close();
           
        }
                
        protected void btn_update_Click1(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "UPDATE UserSettings SET UserFirstName=@UserFirstName, UserPassword=@UserPassword, UserEmail=@UserEmail, WHERE UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Request.QueryString["UserID"]);
            DLdb.SQLST.Parameters.AddWithValue("UserFirstName", UserFirstName.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserPassword", UserPassword.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserEmail", UserEmail.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("");
        }
    }
}