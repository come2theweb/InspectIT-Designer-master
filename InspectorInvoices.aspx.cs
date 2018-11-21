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
    public partial class InspectorInvoices : System.Web.UI.Page
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

            DLdb.DB_Connect();

            //// CREATE INVOICE PER USER

            //// GET SITES
            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "select * from users";
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //if (theSqlDataReader.HasRows)
            //{
            //    while (theSqlDataReader.Read())
            //    {

            //        // GET THE Principle Contractor
            //        string PContractor = "";
            //        string PEmail = "";
            //        string PAddress = "";

            //        //select * from managers where userid = '1' and managementtype = 'Construction Manager'
            //        DLdb.RS3.Open();
            //        DLdb.SQLST3.CommandText = "select * from users where userid = @UserID and type = 'Principle Contractor'";
            //        DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["userid"].ToString());
            //        DLdb.SQLST3.CommandType = CommandType.Text;
            //        DLdb.SQLST3.Connection = DLdb.RS3;
            //        SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            //        if (theSqlDataReader3.HasRows)
            //        {
            //            theSqlDataReader3.Read();
            //            PContractor = theSqlDataReader3["Name"].ToString() + " " + theSqlDataReader3["Surname"].ToString();
            //            PEmail = theSqlDataReader3["emailaddress"].ToString();
            //            PAddress = theSqlDataReader3["PhysicalAddress"].ToString() + ", " + theSqlDataReader3["PhysicalSuburb"].ToString() + ", " + theSqlDataReader3["PhysicalCity"].ToString() + ", " + theSqlDataReader3["PhysicalProvince"].ToString();
            //        }

            //        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            //        DLdb.SQLST3.Parameters.RemoveAt(0);
            //        DLdb.RS3.Close();

            //        // GET THE PERIOD YY AND MM
            //        string srtMM = DateTime.Now.Month.ToString();
            //        string srtYY = DateTime.Now.Year.ToString();
            //        float TotalAmount = 0;

            //        // GET THE TRANSACTION MONTHLY TOTAL
            //        DLdb.RS3.Open();
            //        DLdb.SQLST3.CommandText = "select sum(Amount) as TotalAmount from Transactions where UserID = @UserID and isactive = '1'";
            //        DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["userid"].ToString());
            //        DLdb.SQLST3.CommandType = CommandType.Text;
            //        DLdb.SQLST3.Connection = DLdb.RS3;
            //        theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            //        if (theSqlDataReader3.HasRows)
            //        {
            //            theSqlDataReader3.Read();
            //            if (theSqlDataReader3["TotalAmount"] != DBNull.Value)
            //            {
            //                TotalAmount = Convert.ToInt32(theSqlDataReader3["TotalAmount"].ToString());
            //            }
            //        }

            //        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            //        DLdb.SQLST3.Parameters.RemoveAt(0);
            //        DLdb.RS3.Close();

            //        // CREATE THE INVOICE 
            //        int INVID = 0;
            //        DLdb.RS3.Open();
            //        DLdb.SQLST3.CommandText = "insert into Invoices (MM,YYYY,Amount,UserID,Username,Useraddress) values (@MM,@YYYY,@Amount,@UserID,@Username,@Useraddress);Select Scope_Identity() as INVID;";
            //        DLdb.SQLST3.Parameters.AddWithValue("MM", srtMM.ToString());
            //        DLdb.SQLST3.Parameters.AddWithValue("YYYY", srtYY.ToString());
            //        DLdb.SQLST3.Parameters.AddWithValue("Amount", TotalAmount.ToString());
            //        DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["userid"].ToString());
            //        DLdb.SQLST3.Parameters.AddWithValue("Username", PContractor.ToString());
            //        DLdb.SQLST3.Parameters.AddWithValue("Useraddress", PAddress.ToString());
            //        DLdb.SQLST3.CommandType = CommandType.Text;
            //        DLdb.SQLST3.Connection = DLdb.RS3;
            //        theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            //        if (theSqlDataReader3.HasRows)
            //        {
            //            theSqlDataReader3.Read();
            //            INVID = Convert.ToInt32(theSqlDataReader3["INVID"].ToString());
            //        }

            //        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            //        DLdb.SQLST3.Parameters.RemoveAt(0);
            //        DLdb.SQLST3.Parameters.RemoveAt(0);
            //        DLdb.SQLST3.Parameters.RemoveAt(0);
            //        DLdb.SQLST3.Parameters.RemoveAt(0);
            //        DLdb.SQLST3.Parameters.RemoveAt(0);
            //        DLdb.SQLST3.Parameters.RemoveAt(0);
            //        DLdb.RS3.Close();

            //        // GET ALL THE TRANSACTIONS
            //        DLdb.RS2.Open();
            //        DLdb.SQLST2.CommandText = "select * from Transactions where UserID = @UserID and isactive = '1'";
            //        DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["userid"].ToString());
            //        DLdb.SQLST2.CommandType = CommandType.Text;
            //        DLdb.SQLST2.Connection = DLdb.RS2;
            //        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            //        if (theSqlDataReader2.HasRows)
            //        {
            //            while (theSqlDataReader2.Read())
            //            {
            //                // ADD INVOICE ITEMS
            //                DLdb.RS3.Open();
            //                DLdb.SQLST3.CommandText = "INSERT INTO InvoiceItems (InvoiceID,Name,Cost) values (@InvoiceID,@Name,@Cost)";
            //                DLdb.SQLST3.Parameters.AddWithValue("InvoiceID", INVID);
            //                DLdb.SQLST3.Parameters.AddWithValue("Name", theSqlDataReader2["descrofgoods"].ToString());
            //                DLdb.SQLST3.Parameters.AddWithValue("Cost", theSqlDataReader2["Amount"].ToString());
            //                DLdb.SQLST3.CommandType = CommandType.Text;
            //                DLdb.SQLST3.Connection = DLdb.RS3;
            //                theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            //                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            //                DLdb.SQLST3.Parameters.RemoveAt(0);
            //                DLdb.SQLST3.Parameters.RemoveAt(0);
            //                DLdb.SQLST3.Parameters.RemoveAt(0);
            //                DLdb.RS3.Close();


            //            }

            //        }

            //        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            //        DLdb.SQLST2.Parameters.RemoveAt(0);
            //        DLdb.RS2.Close();

            //        string Items = "";
            //        // GET ALL THE TRANSACTIONS
            //        DLdb.RS2.Open();
            //        DLdb.SQLST2.CommandText = "select * from InvoiceItems where InvoiceID = @InvoiceID and isactive = '1'";
            //        DLdb.SQLST2.Parameters.AddWithValue("InvoiceID", INVID.ToString());
            //        DLdb.SQLST2.CommandType = CommandType.Text;
            //        DLdb.SQLST2.Connection = DLdb.RS2;
            //        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            //        if (theSqlDataReader2.HasRows)
            //        {
            //            while (theSqlDataReader2.Read())
            //            {
            //                Items += "<tr>" +
            //                          "<td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='left'>" + theSqlDataReader2["ItemID"].ToString() + "</td>" +
            //                          "<td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='left'>" + theSqlDataReader2["Name"].ToString() + "</td>" +
            //                          "<td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>" + theSqlDataReader2["Cost"].ToString() + "</b></td>" +
            //                         "</tr>";

            //            }

            //        }

            //        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            //        DLdb.SQLST2.Parameters.RemoveAt(0);
            //        DLdb.RS2.Close();

            //        // CREATE THE PDF INVOICE
            //        htmlContent = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
            //                                    "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
            //                                    "        <tr>" +
            //                                    "            <td>" +
            //                                    "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
            //                                    "                    <tr>" +
            //                                    "                        <td align='left' valign='top'><div style='width:100%;padding:10px;padding-top:10px'><br />To: " + PContractor + "<br />Address: " + PAddress + "</div></td>" +
            //                                    "                        <td align='right'><img src='http://www.chsitapp.co.za/assets/img/logo.png'><br /><br /><font style='font-size:26px;'><b>INVOICE No.: IVN00" + INVID + "</b><br />Period: </b>" + srtMM + "-" + srtYY + "</font></td>" +
            //                                    "                    </tr>" +
            //                                    "                    <tr>" +
            //                                    "                        <td align='left' colspan='2' height='800px' valign='top'>" +
            //                                    "                            <table border='0' cellpadding='5px' cellspacing='0' width='100%'>" +
            //                                    "                               <tr>" +
            //                                    "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>Item No.</td>" +
            //                                    "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>Description</td>" +
            //                                    "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>Amount</b></td>" +
            //                                    "                                </tr>" +
            //                                    "                                " + Items + "" +
            //                                    "                               <tr>" +
            //                                    "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
            //                                    "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>Total Amount<b></td>" +
            //                                    "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + TotalAmount + "</b></td>" +
            //                                    "                                </tr>" +
            //                                    "                            </table>" +
            //                                    "                        </td>" +
            //                                    "                    </tr>" +
            //                                    "                    <tr>" +
            //                                    "                        <td colspan='2'><br /><br /><table border='0' cellpadding='3px' cellspacing='0' width='100%'><tr><td align='left'><img src='http://www.chsitapp.co.za/assets/img/mbalogo.png'></td><td valign='middle' align='right'><b>Powered by WAD Management Systems</b></td></tr></table></td>" +
            //                                    "                    </tr>" +
            //                                    "                </table>" +
            //                                    "            </td>" +
            //                                    "        </tr>" +
            //                                    "    </table>" +
            //                                    "</body>");

            //        string filename = "invoice_" + INVID + "_" + srtMM + "-" + srtYY + ".pdf";
            //        var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(htmlContent);
            //        string path = Server.MapPath("~/invoices/") + filename;
            //        File.WriteAllBytes(path, pdfBytes);


            //    }
            //}

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.RS.Close();


            //DLdb.DB_Close();
        }
    }
}