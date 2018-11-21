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

namespace InspectIT.API
{
    public partial class inspectorSubmitInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();


            // CREATE INVOICE
            var htmlContent = "";
            string UserName = "";
            string UserEmail = "";
            string cocType = "";
            string filename = "";
            string uid = "";
            string Bank = "";
            string UserContact = "";
            string registrationNumber = "";

            //isComplete = '1',CompletedOn = getdate(),
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCInspectors set isComplete = '1',CompletedOn = getdate() where COCStatementID = @COCStatementID and UserID = @UserID and isactive='1'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", Request.QueryString["cocid"]);
            DLdb.SQLST.Parameters.AddWithValue("UserID", Request.QueryString["uid"]);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update cocstatements set status = 'Completed',isInvoiceSubmitted='1',isInspectorSubmitted='1' where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", Request.QueryString["cocid"]);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS3.Open();
            DLdb.SQLST3.CommandText = "select * from users where userid = @UserID";
            DLdb.SQLST3.Parameters.AddWithValue("UserID", Request.QueryString["uid"]);
            DLdb.SQLST3.CommandType = CommandType.Text;
            DLdb.SQLST3.Connection = DLdb.RS3;
            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            if (theSqlDataReader3.HasRows)
            {
                theSqlDataReader3.Read();
                UserName = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
                UserEmail = theSqlDataReader3["email"].ToString();
                UserContact = theSqlDataReader3["contact"].ToString();
                uid = theSqlDataReader3["UserID"].ToString();
                registrationNumber = theSqlDataReader3["regno"].ToString();
            }

            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            DLdb.SQLST3.Parameters.RemoveAt(0);
            DLdb.RS3.Close();

            string pic = "";
            string vatregno = "";
            DLdb.RS3.Open();
            DLdb.SQLST3.CommandText = "select * from Auditor where userid = @UserID";
            DLdb.SQLST3.Parameters.AddWithValue("UserID", uid);
            DLdb.SQLST3.CommandType = CommandType.Text;
            DLdb.SQLST3.Connection = DLdb.RS3;
            theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            if (theSqlDataReader3.HasRows)
            {
                theSqlDataReader3.Read();
                if (theSqlDataReader3["PhotoFile"].ToString() != "" && theSqlDataReader3["PhotoFile"] != DBNull.Value)
                {
                    pic = "<img src='https://197.242.82.242/inspectit/AuditorImgs/" + theSqlDataReader3["CompanyLogo"].ToString() + "' style=\"height:200px;\"/>";
                }
                vatregno = theSqlDataReader3["vatregno"].ToString();
                Bank = "Bank Name: " + DLdb.Decrypt(theSqlDataReader3["BankName"].ToString()) + "<br/> Account Name: " + DLdb.Decrypt(theSqlDataReader3["AccName"].ToString()) + "<br/> Account Number: " + DLdb.Decrypt(theSqlDataReader3["AccNumber"].ToString()) + "<br/> Branch Code: " + DLdb.Decrypt(theSqlDataReader3["branchcode"].ToString()) + "<br/> Account Type: " + DLdb.Decrypt(theSqlDataReader3["AccType"].ToString());
            }

            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            DLdb.SQLST3.Parameters.RemoveAt(0);
            DLdb.RS3.Close();

            DLdb.RS3.Open();
            DLdb.SQLST3.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID";
            DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", Request.QueryString["cocid"].ToString());
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

            decimal inspectorRate = 0;
            DLdb.RS3.Open();
            DLdb.SQLST3.CommandText = "select * from Rates where ID = '39'";
            //DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST3.CommandType = CommandType.Text;
            DLdb.SQLST3.Connection = DLdb.RS3;
            theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            if (theSqlDataReader3.HasRows)
            {
                theSqlDataReader3.Read();
                inspectorRate = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
            }

            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            //DLdb.SQLST3.Parameters.RemoveAt(0);
            DLdb.RS3.Close();

            decimal vats = 0;
            DLdb.RS3.Open();
            DLdb.SQLST3.CommandText = "select * from settings where ID='1'";
            DLdb.SQLST3.CommandType = CommandType.Text;
            DLdb.SQLST3.Connection = DLdb.RS3;
            theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();
            if (theSqlDataReader3.HasRows)
            {
                theSqlDataReader3.Read();
                vats = Convert.ToDecimal(theSqlDataReader3["VatPercentage"].ToString());
            }
            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            DLdb.RS3.Close();

            decimal vat = Convert.ToDecimal(vats);
            decimal vatElec = inspectorRate * vat;
            decimal totalWithvat = inspectorRate + vatElec;

            // GET THE PERIOD YY AND MM
            string srtDD = DateTime.Now.Day.ToString();
            string srtMM = DateTime.Now.Month.ToString();
            string srtYY = DateTime.Now.Year.ToString();

            DateTime InvoiceDate = Convert.ToDateTime(Request.QueryString["date"].ToString());
            string invID = Request.QueryString["invID"];

            // CREATE THE PDF INVOICE
            htmlContent = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
                                            "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                            "        <tr>" +
                                            "            <td>" +
                                            "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                            "                    <tr>" +
                                             "                        <td align='left' valign='top'><div style='width:100%;padding:10px;padding-top:10px'><br /> " + UserName + "<br />" + UserContact + "<br />" + UserEmail + "<br />" + vatregno + "</div></td>" +
                                            "                        <td align='center'>" + pic + "</td>" +
                                            "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <td align='left'><br /><h4>INVOICE TO :</h4><b>Plumbing Industry Registration Board (PIRB)</b><br />PO Box 411<br /> Wierdapark<br /> Centurion <br /> 0149 <br /> 0861 747 275 <br /> info@pirb.co.za <br /> www.pirb.co.za <br /><br /> VAT No: 4230255327</td>" +
                                                "                        <td align='left'><br /><h4>TAX INVOICE: " + invID + "</h4><br />Date: " + InvoiceDate.ToString("dd/MM/yyyy") + "</td>" +
                                                "                    </tr>" +
                                            "                    <tr>" +
                                            "                        <td align='left' colspan=\"2\" valign='top'>" +
                                            "                            <table border='0' cellpadding='5px' cellspacing='0' width='100%'>" +
                                            "                               <tr>" +
                                            "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle'>Activitiy</td>" +
                                            "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle'>Description</td>" +
                                            "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle'>Qty</td>" +
                                            "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle'>Rate</td>" +
                                            "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle'>Amount</td>" +
                                            "                                </tr>" +
                                            "                               <tr>" +
                                            "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle'>Audit</td>" +
                                            "                                   <td style=\"border: 1px solid #E5E5E5;\" valign='middle'>Audit undertaken of COC" + Request.QueryString["cocid"] + "</td>" +
                                            "                                   <td style=\"border: 1px solid #E5E5E5;\" valign='middle'>1</td>" +
                                            "                                   <td style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + inspectorRate + "</td>" +
                                            "                                   <td style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + inspectorRate + "</td>" +
                                            "                                </tr>" +
                                                "                               <tr>" +
                                                "                                   <td style=\"border: 1px solid #E5E5E5;\" colspan=\"2\" valign='middle' align='right'><b>VAT @15%<b></td>" +
                                                "                                   <td  style=\"border: 1px solid #E5E5E5;\" colspan=\"3\" align='right' valign='middle'><b>R" + vatElec + "</b></td>" +
                                                "                                </tr>" +
                                            "                               <tr>" +
                                            "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle' colspan=\"2\" align='right'><b>Total Amount Excl VAT<b></td>" +
                                            "                                   <td  style=\"border: 1px solid #E5E5E5;\" align='right' colspan=\"3\" valign='middle'><b>R" + inspectorRate + "</b></td>" +
                                            "                                </tr>" +
                                            "                               <tr>" +
                                            "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle' colspan=\"2\" align='right'><b>Total Amount Incl VAT<b></td>" +
                                            "                                   <td  style=\"border: 1px solid #E5E5E5;\" align='right' colspan=\"3\" valign='middle'><b>R" + totalWithvat + "</b></td>" +
                                            "                                </tr>" +
                                            "                            </table>" +
                                            "                        </td>" +
                                            "                    </tr>" +
                                            "                    <tr>" +
                                             "                        <td align='left' valign='top'><div style='width:100%;padding:10px;padding-top:10px'><br /> " + Bank + "</div></td>" +
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

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCInspectors set Report = @Report,InvoiceNumber=@InvoiceNumber,DateInvoiceSubmitted=getdate() where cocstatementid=@cocstatementid";
            DLdb.SQLST.Parameters.AddWithValue("Report", filename);
            DLdb.SQLST.Parameters.AddWithValue("InvoiceNumber", invID.ToString());
            DLdb.SQLST.Parameters.AddWithValue("cocstatementid", Request.QueryString["cocid"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
           // Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("COC Audit Statement Submitted"));
        }
    }
}