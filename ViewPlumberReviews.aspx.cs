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
    public partial class ViewPlumberReviews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();

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

            displayauditors.InnerHtml = "";
            
            DLdb.DB_Connect();

            string cocid = "";
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "SELECT * FROM COCStatements where isactive = '1' and AuditorID <> '0'";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM Auditor where isactive = '1' and AuditorID=@AuditorID";
                    DLdb.SQLST.Parameters.AddWithValue("AuditorID", theSqlDataReader2["AuditorID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            cocid = theSqlDataReader2["COCStatementID"].ToString();
                            //<a href=\"adminGenerateInvoice.aspx?UserID=" + DLdb.Encrypt(theSqlDataReader["AuditorID"].ToString()) + "\"><input type=\"button\" value=\"Create Invoice\" class=\"btn btn-primary\"/></a>
                            displayauditors.InnerHtml += "<tr>" +
                                 "<td>" + cocid + "</td>" +
                                                               "<td>" + theSqlDataReader["regNo"].ToString() + "</td>" +
                                                               "<td>" + theSqlDataReader["fName"].ToString() + "</td>" +
                                                               "<td>" + theSqlDataReader["lName"].ToString() + "</td>" +
                                                               "<td>" + theSqlDataReader["phoneWork"].ToString() + "</td>" +
                                                               "<td>" + theSqlDataReader["phoneHome"].ToString() + "</td>" +
                                                               "<td>" + theSqlDataReader["phoneMobile"].ToString() + "</td>" +
                                                               "<td>" + theSqlDataReader["fax"].ToString() + "</td>" +
                                                               "<td>" + theSqlDataReader["isactive"].ToString() + "</td>" +
                                                                "<td>" +
                                                                "<a href=\"EditCOCStatementInspector.aspx?cocid=" + DLdb.Encrypt(cocid) + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                               //"<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeleteAuditor.aspx?AuditorID=" + theSqlDataReader["AuditorID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div>" +
                                                               "</td>" +
                                                           "</tr>";
                        }
                    }
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }
            }
            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

           

            DLdb.DB_Close();
        }                
    }
}