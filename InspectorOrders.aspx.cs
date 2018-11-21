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
    public partial class InspectorOrders : System.Web.UI.Page
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
            DLdb.SQLST.CommandText = "SELECT * FROM COCInspectors where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string invDisp = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from COCInspectors where AuditID=@AuditID";
                    DLdb.SQLST2.Parameters.AddWithValue("AuditID", theSqlDataReader["AuditID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            if (theSqlDataReader["Report"].ToString() != "" && theSqlDataReader["Report"] != DBNull.Value)
                            {
                                invDisp = "<a href=\"Inspectorinvoices/"+ theSqlDataReader["Report"].ToString() + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-eye\"></i></div></a>";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    displayusers.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["AuditID"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["COCStatementID"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["UserID"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["IsComplete"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["CompletedOn"].ToString() + "</td>" +
                                                       "<td>"+ invDisp + "</td>" +//InspectorViewInvoicePDF.aspx?aid=" + DLdb.Encrypt(theSqlDataReader["AuditID"].ToString()) + "
                                                       "<td>" + theSqlDataReader["TotalAmount"].ToString() + "</td>" +
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