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
    public partial class adminCpdPoints : System.Web.UI.Page
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
            DLdb.SQLST.CommandText = "SELECT * FROM Assessments where isActive='1'";
            // DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string name = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            name = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    string category = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Categories where CategoryID=@CategoryID";
                    DLdb.SQLST2.Parameters.AddWithValue("CategoryID", theSqlDataReader["CategoryID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            category = theSqlDataReader2["categoryname"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    string subcategory = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM CategoriesSub where SubCategoryID=@SubCategoryID";
                    DLdb.SQLST2.Parameters.AddWithValue("SubCategoryID", theSqlDataReader["SubCategoryID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            subcategory = theSqlDataReader2["SubCategoryName"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    string approved = "";

                    if (theSqlDataReader["isApproved"].ToString() == "True")
                    {
                        approved = "<span class=\"label label-success\">Approved</span>";
                    }
                    else
                    {
                        approved = "<span class=\"label label-danger\">Not Approved</span>";
                    }

                    displayusers.InnerHtml += "<tr>" +
                                                       "<td>" + name + "</td>" +
                                                       "<td>" + category + "</td>" +
                                                       "<td>" + subcategory + "</td>" +
                                                       "<td><a href='Assessments/" + theSqlDataReader["Certificate"].ToString() + "'><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-download\"></i></div></a></td>" +
                                                       "<td>" + theSqlDataReader["NoPoints"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["CertificateDate"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["InstitutionName"].ToString() + "</td>" +
                                                       "<td>" + approved + "</td>" +
                                                       "<td><a href=\"approvePoints.aspx?aid=" + theSqlDataReader["AssessmentID"].ToString() + "&ap=approved" + "\"><div class=\"btn btn-sm btn-success\"><i class=\"fa fa-check\"></i></div></a>" +
                                                       "<a href=\"approvePoints.aspx?aid=" + theSqlDataReader["AssessmentID"].ToString() + "&ap=disapproved" + "\"><div class=\"btn btn-sm btn-danger\"><i class=\"fa fa-times\"></i></div></a></td>" +
                                                   "</tr>";
                }
            }
            else
            {
                // Display any errors
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            DLdb.DB_Close();
        }
    }
}