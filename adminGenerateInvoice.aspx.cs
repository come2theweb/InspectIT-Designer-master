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
using System.IO;

namespace InspectIT
{
    public partial class adminGenerateInvoice : System.Web.UI.Page
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

            // CREATE INVOICE PER USER
            var htmlContent = "";
            string invID = "";
            string total = "";
            string UserName = "";
            string UserEmail = "";
            string cocType = "";
            string filename = "";

            // GET SITES
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCInspectors where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["UserID"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "select * from users where userid = @UserID";
                    DLdb.SQLST3.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["UserID"].ToString()));
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        theSqlDataReader3.Read();
                        UserName = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
                        UserEmail = theSqlDataReader3["email"].ToString();
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();

                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID";
                    DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        theSqlDataReader3.Read();
                        cocType = theSqlDataReader3["Type"].ToString();
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();

                    // GET THE PERIOD YY AND MM
                    string srtMM = DateTime.Now.Month.ToString();
                    string srtYY = DateTime.Now.Year.ToString();

                    invID = theSqlDataReader["AuditID"].ToString();
                    total = theSqlDataReader["TotalAmount"].ToString();

                    // CREATE THE PDF INVOICE
                    htmlContent = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
                                                    "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                    "        <tr>" +
                                                    "            <td>" +
                                                    "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                    "                    <tr><td></td>" +
                                                    // "                        <td align='left' valign='top'><div style='width:100%;padding:10px;padding-top:10px'><br />To: " + UserName + "<br /></div></td>" +
                                                    "                        <td align='center'><img src='https://197.242.82.242/inspectit/assets/img/logo_2x.png'/></td>" +
                                                    "                    <td></td></tr>" +
                                                    "                    <tr>" +
                                                    "                        <td align='left' valign='top'><div style='width:100%;padding:10px;padding-top:10px'><br />To: " + UserName + "<br /></div></td>" +
                                                    "                        <td align='right'><br /><font style='font-size:26px;'><b>INVOICE No. : IVN00" + invID + "</b><br />Period: </b>" + srtMM + "-" + srtYY + "</font></td>" +
                                                    "                    </tr>" +
                                                    "                    <tr>" +
                                                    "                        <td align='left' colspan='2' height='800px' valign='top'>" +
                                                    "                            <table border='0' cellpadding='5px' cellspacing='0' width='100%'>" +
                                                    "                               <tr>" +
                                                    "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>Audit No.</td>" +
                                                    "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>COC Type</td>" +
                                                    "                                </tr>" +
                                                    "                               <tr>" +
                                                    "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + invID + "</td>" +
                                                    "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + cocType + "</td>" +
                                                    "                                </tr>" +
                                                    "                               <tr>" +
                                                    "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>Total Amount<b></td>" +
                                                    "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + total + "</b></td>" +
                                                    "                                </tr>" +
                                                    "                            </table>" +
                                                    "                        </td>" +
                                                    "                    </tr>" +
                                                    "                    <tr>" +
                                                    "                        <td colspan='2'><br /><br /><table border='0' cellpadding='3px' cellspacing='0' width='100%'><tr><td align='left'><img src='https://197.242.82.242/inspectit/assets/img/logo.png'/></td><td valign='middle' align='right'><b>InspectIT Team</b></td></tr></table></td>" +
                                                    "                    </tr>" +
                                                    "                </table>" +
                                                    "            </td>" +
                                                    "        </tr>" +
                                                    "    </table>" +
                                                    "</body>");

                    filename = "invoice_" + invID + "_" + srtMM + "-" + srtYY + ".pdf";
                    var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(htmlContent);
                    string path = Server.MapPath("~/Inspectorinvoices/") + filename;
                    File.WriteAllBytes(path, pdfBytes);
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCInspectors set Invoice=@Invoice,Description=@Description where AuditID=@AuditID";
            DLdb.SQLST.Parameters.AddWithValue("Report", filename);
            DLdb.SQLST.Parameters.AddWithValue("Description", cocType);
            DLdb.SQLST.Parameters.AddWithValue("AuditID", invID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Response.Redirect("adminViewCOCInspector.aspx?msg=Invoice has been created");

            DLdb.DB_Close();
        }
    }
}