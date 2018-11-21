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
    public partial class UserPerformanceStatus : System.Web.UI.Page
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
            if (Session["IIT_Role"].ToString() != "Staff")
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
            displayusers.InnerHtml = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select sum(cast(PerformancePointAllocation as int)) as tot from PerformanceStatus where userid=@userid and isApproved='1' and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("userid", Session["IIT_UID"]);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["tot"].ToString() == null)
                    {
                        cpdpoints.InnerHtml = "0";
                    }
                    else
                    {
                        cpdpoints.InnerHtml = theSqlDataReader["tot"].ToString();
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM PerformanceStatus where UserID=@UserID and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string editBtn = "";
                    
                        editBtn = "<a onclick=\"openPerformance(" + theSqlDataReader["PerformanceStatusID"].ToString() + ")\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-eye\"></i></div></a>";
                    displayusers.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["Date"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["PerformanceType"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["Details"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["PerformancePointAllocation"].ToString() + "</td>" +
                                                       "<td>" + editBtn + "</td>" +
                                                   "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM PerformanceStatus where UserID=@UserID and isActive='0'";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string editBtn = "";
                    
                        editBtn = "<a onclick=\"openPerformance(" + theSqlDataReader["PerformanceStatusID"].ToString() + ")\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-eye\"></i></div></a>";
                    Tbody1.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["Date"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["PerformanceType"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["Details"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["PerformancePointAllocation"].ToString() + "</td>" +
                                                       "<td>" + editBtn + "</td>" +
                                                   "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }
    }
}