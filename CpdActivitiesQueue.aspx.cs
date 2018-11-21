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
    public partial class CpdActivitiesQueue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string delBtnClass = "";
            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }

            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                addBtn.Visible = false;
                delBtnClass = "hide";
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {
                addBtn.Visible = true;
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
            DLdb.SQLST.CommandText = "SELECT * FROM Assessments where isActive='1' and isRejected='0' and isApproved='0'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string name = "";
                    string regnno = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from Users WHERE UserID=@UserID";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        regnno = theSqlDataReader2["regno"].ToString();
                        name = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                    }
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    displayusers.InnerHtml += "<tr>" +
                                                    "<td>" + theSqlDataReader["CertificateDate"].ToString() + "</td>" +
                                                    "<td>" + name + "</td>" +
                                                    "<td>" + regnno + "</td>" +
                                                    "<td>" + theSqlDataReader["Category"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["Activity"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["NoPoints"].ToString() + "</td>" +
                                                    "<td><a href='CpdActivitiesQueueEdit?aid="+ DLdb.Encrypt(theSqlDataReader["AssessmentID"].ToString()) + "'><div class=\"btn btn-sm btn-primary\" title=\"Edit\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                    "</td>" +
                                                "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();
            

            DLdb.DB_Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("CpdActivitiesQueueEdit");
        }
    }
}