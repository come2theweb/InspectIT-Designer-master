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
    public partial class ViewAuditor : System.Web.UI.Page
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

            string delBtnClass = "";

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

            displayauditors.InnerHtml = "";
            
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "SELECT * FROM Auditor where isactive = '1'";
            // DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    double comp=0;
                    double allCount =0;
                    double percentcomp = 0;
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select count(AuditID) as countedCompleted from COCInspectors where userid=@userid and IsComplete='1'";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            comp = Convert.ToDouble(theSqlDataReader2["countedCompleted"]);
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select count(AuditID) as allCount from COCInspectors where userid=@userid";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            allCount = Convert.ToDouble(theSqlDataReader2["allCount"]);
                            
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    if (comp == 0 || allCount == 0)
                    {
                        percentcomp = 0;
                    }
                    else
                    {
                        percentcomp = comp / allCount * 100;
                    }
                    

                    //<a href=\"adminGenerateInvoice.aspx?UserID=" + DLdb.Encrypt(theSqlDataReader["AuditorID"].ToString()) + "\"><input type=\"button\" value=\"Create Invoice\" class=\"btn btn-primary\"/></a>
                    displayauditors.InnerHtml +=   "<tr>" +
                                                       "<td>" + theSqlDataReader["regNo"].ToString() + "</td>" +
                                                       "<td>" + Math.Round(percentcomp) + "%</td>" +
                                                       "<td>" + theSqlDataReader["fName"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["lName"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["phoneWork"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["phoneHome"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["phoneMobile"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["fax"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["isactive"].ToString() + "</td>" +
                                                        "<td><a href=\"EditOrDeleteAuditor.aspx?AuditorID=" + DLdb.Encrypt(theSqlDataReader["AuditorID"].ToString()) + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                       "<div class=\"btn "+ delBtnClass + " btn-sm btn-danger\" onclick=\"deleteconf('DeleteAuditor.aspx?AuditorID=" + theSqlDataReader["AuditorID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
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

            

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Auditor where isactive = '0'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
             theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    dispInactiveAuditr.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["regNo"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["fName"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["lName"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["phoneWork"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["phoneHome"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["phoneMobile"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["fax"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["isactive"].ToString() + "</td>" +
                                                        "<td><a href=\"EditOrDeleteAuditor.aspx?AuditorID=" + DLdb.Encrypt(theSqlDataReader["AuditorID"].ToString()) + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                       "<div class=\"btn " + delBtnClass + " btn-sm btn-danger\" onclick=\"deleteconf('DeleteAuditor.aspx?AuditorID=" + theSqlDataReader["AuditorID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                   "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();
            DLdb.DB_Close();
        }                
    }
}