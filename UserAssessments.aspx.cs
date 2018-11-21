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
    public partial class UserAssessments : System.Web.UI.Page
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
            DLdb.SQLST.CommandText = "select sum(nopoints) as tot from assessments where userid=@userid and isApproved='1'";
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
            DLdb.SQLST.CommandText = "SELECT * FROM Assessments where UserID=@UserID and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string category = "";
                    string Activity = "";
                    string approved = "";
                    string editBtn = "";
                    string dltBtn = "";

                    if (theSqlDataReader["Category"].ToString() == "1")
                    {
                        category = "Category 1: Developmental Activities";
                    } 
                    else if (theSqlDataReader["Category"].ToString() == "2")
                    {
                        category = "Category 2: Work-based Activities";
                    }
                    else
                    {
                        category = "Category 3: Individual Activities";
                    }

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM CPDActivities where CPDActivityID=@CPDActivityID";
                    DLdb.SQLST2.Parameters.AddWithValue("CPDActivityID", theSqlDataReader["CPDActivityID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            Activity = theSqlDataReader2["Activity"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    if (theSqlDataReader["isApproved"].ToString() == "True")
                    {
                        approved = "<span class=\"label label-success\">Approved</span>";
                        editBtn = "<div class=\"btn btn-sm btn-primary disabled\"><i class=\"fa fa-pencil\"></i></div>";
                        dltBtn = "<div class=\"btn btn-sm btn-danger disabled\"><i class=\"fa fa-trash\"></i></div>";
                    }
                    else
                    {
                        approved = "<span class=\"label label-danger\">Not Approved</span>";
                        editBtn = "<a href=\"AddAssessment.aspx?aid=" + theSqlDataReader["AssessmentID"].ToString() + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-eye\"></i></div></a>";
                        dltBtn = "<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeleteAssessment.aspx?op=del&aid=" + theSqlDataReader["AssessmentID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div>";
                    }
                    displayusers.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["CertificateDate"].ToString() + "</td>" +
                                                       "<td>" + category + "</td>" +
                                                       "<td>" + Activity + "</td>" +
                                                       "<td>" + theSqlDataReader["NoPoints"].ToString() + "</td>" +
                                                       "<td>" + approved + "</td>" +
                                                       "<td>" + editBtn +
                                                       dltBtn + "</td>" +
                                                   "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Assessments where UserID=@UserID and isActive='0'";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string category = "";
                    string Activity = "";
                    string approved = "";
                    string editBtn = "";
                    string dltBtn = "";

                    if (theSqlDataReader["Category"].ToString() == "1")
                    {
                        category = "Category 1: Developmental Activities";
                    }
                    else if (theSqlDataReader["Category"].ToString() == "2")
                    {
                        category = "Category 2: Work-based Activities";
                    }
                    else
                    {
                        category = "Category 3: Individual Activities";
                    }

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM CPDActivities where CPDActivityID=@CPDActivityID";
                    DLdb.SQLST2.Parameters.AddWithValue("CPDActivityID", theSqlDataReader["CPDActivityID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            Activity = theSqlDataReader2["Activity"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    if (theSqlDataReader["isApproved"].ToString() == "True")
                    {
                        approved = "<span class=\"label label-success\">Approved</span>";
                        editBtn = "<div class=\"btn btn-sm btn-primary disabled\"><i class=\"fa fa-pencil\"></i></div>";
                        dltBtn = "<div class=\"btn btn-sm btn-danger disabled\"><i class=\"fa fa-trash\"></i></div>";
                    }
                    else
                    {
                        approved = "<span class=\"label label-danger\">Not Approved</span>";
                        editBtn = "<a href=\"AddAssessment.aspx?aid=" + theSqlDataReader["AssessmentID"].ToString() + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-eye\"></i></div></a>";
                        dltBtn = "<div class=\"btn btn-sm btn-success\" onclick=\"deleteconf('DeleteAssessment.aspx?op=undel&aid=" + theSqlDataReader["AssessmentID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div>";
                    }
                    Tbody1.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["CertificateDate"].ToString() + "</td>" +
                                                       "<td>" + category + "</td>" +
                                                       "<td>" + Activity + "</td>" +
                                                       "<td>" + theSqlDataReader["NoPoints"].ToString() + "</td>" +
                                                       "<td>" + approved + "</td>" +
                                                       "<td>" + editBtn + dltBtn + "</td>" +
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