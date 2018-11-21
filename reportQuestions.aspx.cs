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
    public partial class reportQuestions : System.Web.UI.Page
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
            
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }

            string delBtnClass = "";
            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                addQBtn.Visible = false;
                delBtnClass = "hide";
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {
                addQBtn.Visible = true;
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
            DLdb.SQLST.CommandText = "SELECT * FROM ReportQuestionList where isActive='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string reportType = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM InstallationTypes where InstallationTypeID=@InstallationTypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            reportType = theSqlDataReader2["InstallationType"].ToString();
                        }
                    }
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string subType = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM InstallationTypesSub where subID=@subID";
                    DLdb.SQLST2.Parameters.AddWithValue("subID", theSqlDataReader["subID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            subType = theSqlDataReader2["Name"].ToString();
                        }
                    }
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string formField = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from FormLinks l inner join formfields f on l.FormID = f.FormID where FieldID = @FieldID";
                    DLdb.SQLST2.Parameters.AddWithValue("FieldID", theSqlDataReader["FieldID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            formField = theSqlDataReader2["Label"].ToString();
                        }
                    }
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    displayusers.InnerHtml += "<tr>" +
                                                       "<td>" + reportType + "</td>" +
                                                       "<td>" + subType + "</td>" +
                                                       "<td>" + theSqlDataReader["Name"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["isActive"].ToString() + "</td>" +
                                                       "<td><a href=\"reportQuestionsEdit.aspx?id=" + DLdb.Encrypt(theSqlDataReader["ID"].ToString()) + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                       "<div class=\"btn "+ delBtnClass + " btn-sm btn-danger\" onclick=\"deleteconf('DeletereportQuestions.aspx?id=" + theSqlDataReader["ID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                   "</tr>";
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM ReportQuestionList where isActive='0'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string reportType = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM InstallationTypes where InstallationTypeID=@InstallationTypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            reportType = theSqlDataReader2["InstallationType"].ToString();
                        }
                    }
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string subType = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM InstallationTypesSub where subID=@subID";
                    DLdb.SQLST2.Parameters.AddWithValue("subID", theSqlDataReader["subID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            subType = theSqlDataReader2["Name"].ToString();
                        }
                    }
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string formField = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from FormLinks l inner join formfields f on l.FormID = f.FormID where FieldID = @FieldID";
                    DLdb.SQLST2.Parameters.AddWithValue("FieldID", theSqlDataReader["FieldID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            formField = theSqlDataReader2["Label"].ToString();
                        }
                    }
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    Tbody1.InnerHtml += "<tr>" +
                                                       "<td>" + reportType + "</td>" +
                                                       "<td>" + subType + "</td>" +
                                                       "<td>" + theSqlDataReader["Name"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["isActive"].ToString() + "</td>" +
                                                       "<td><a href=\"reportQuestionsEdit.aspx?id=" + DLdb.Encrypt(theSqlDataReader["ID"].ToString()) + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                       "<div class=\"btn " + delBtnClass + " btn-sm btn-danger\" onclick=\"deleteconf('DeletereportQuestions.aspx?id=" + theSqlDataReader["ID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                   "</tr>";
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();
            DLdb.DB_Close();
        }
    }
}