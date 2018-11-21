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
    public partial class adminViewCOCInspector : System.Web.UI.Page
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

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();
            displayusers.InnerHtml = "";

            DLdb.DB_Connect();
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "SELECT * FROM COCInspectors";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    displayusers.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["AuditID"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["COCStatementID"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["UserID"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["IsComplete"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["CompletedOn"].ToString() + "</td>" +
                                                       "<td width=\"100px\"><div class=\"btn-group\"><div class=\"btn btn-sm btn-primary\" onclick=\"document.location.href='adminViewInvoicePDF.aspx?aid=" + DLdb.Encrypt(theSqlDataReader["AuditID"].ToString()) + "'\"><i class=\"fa fa-eye\"></i></div></div></td>" +
                                                       "<td>" + theSqlDataReader["TotalAmount"].ToString() + "</td>" +
                                                       "<td><a href=\"adminGenerateInvoice.aspx?UserID=" + DLdb.Encrypt(theSqlDataReader["UserID"].ToString()) + "\"><input type=\"button\" value=\"Create Invoice\" class=\"btn btn-primary\"/></a></td>" +
                                                   "</tr>";
                }
            }
            else
            {
                // Display any errors
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();
            DLdb.DB_Close();
        }
    }
}