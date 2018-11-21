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
using System.Security.Cryptography;
using System.IO;

namespace InspectIT
{
    public partial class VerifyPurchase : System.Web.UI.Page
    {
        public string plumName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            // ADMIN CHECK
            if (Session["IIT_Role"].ToString() != "Supplier")
            {
                Response.Redirect("Default");
            }

            //if (Request.QueryString["msg"] != null)
            //{
            //    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
            //    successmsg.InnerHtml = msg;
            //    successmsg.Visible = true;
            //}

            //if (Request.QueryString["err"] != null)
            //{
            //    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
            //    errormsg.InnerHtml = msg;
            //    errormsg.Visible = true;
            //}

            if (!IsPostBack)
            {
                DLdb.DB_Connect();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from Orders where OrderID = @OrderID";
                DLdb.SQLST.Parameters.AddWithValue("OrderID", DLdb.Decrypt(Request.QueryString["oid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "Select * from users where UserID = @UserID";
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                NumberTo.InnerHtml = theSqlDataReader2["contact"].ToString();
                                plumName = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                                NameofPlumber.InnerHtml = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                                btn_buy.Enabled = true;
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                        
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

            }
            
        }

        protected void resendOTP_Click(object sender, EventArgs e)
        {
            if(NumberTo.InnerHtml.ToString() != "")
            {
                Global DLdb = new Global();
                string OTPCode = Session["IIT_OTPCode"].ToString();
                DLdb.sendSMS("11", NumberTo.InnerHtml, "Inspect-It: OTP Resend, please use OTP Code: " + OTPCode);
            }
        }

        protected void btn_buy_Click(object sender, EventArgs e)
        {
            if (OTPCode.Text != "")
            {
                if (Session["IIT_OTPCode"].ToString() == OTPCode.Text.ToString())
                {
                    string SupName = "";
                    string SupEmail = "";
                    string NumberTo = "";
                    string supAddress = "";
                    string plumNamed = "";

                    Global DLdb = new Global();
                    DLdb.DB_Connect();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from Orders where OrderID = @OrderID";
                    DLdb.SQLST.Parameters.AddWithValue("OrderID", DLdb.Decrypt(Request.QueryString["oid"].ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            int used = 0; 
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "select * from Suppliers where UserID = @UserID";
                            DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["SupplierID"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {
                                    used = Convert.ToInt32(theSqlDataReader3["TotalUsed"].ToString());
                                    SupName = theSqlDataReader3["SupplierName"].ToString();
                                    SupEmail = theSqlDataReader3["SupplierEmail"].ToString();
                                    NumberTo = theSqlDataReader3["suppliercontactno"].ToString();
                                    supAddress = theSqlDataReader3["AddressLine1"].ToString() + "<br/> " + theSqlDataReader3["AddressLine2"].ToString() + "<br/> " + theSqlDataReader3["CitySuburb"].ToString() + "<br/> " + theSqlDataReader3["Province"].ToString();
                                }
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();

                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "Select * from users where UserID = @UserID";
                            DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {
                                    plumNamed = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
                                }
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();

                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "update Suppliers set TotalUsed = @TotalUsed where UserID = @UserID";
                            DLdb.SQLST3.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("TotalUsed", used + Convert.ToInt32(theSqlDataReader["TotalNoItems"]));
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();
                            
                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();

                            int sRange = Convert.ToInt32(theSqlDataReader["StartRange"].ToString());
                            int eRange = Convert.ToInt32(theSqlDataReader["EndRange"].ToString());

                            // ASSIGN CERTIFICATES
                            for (int i = sRange; i < eRange; i++)
                            {
                                DLdb.RS2.Open();
                                DLdb.SQLST2.CommandText = "Update COCStatements set UserID = @UserID, isStock = '0',DatePurchased=getdate(),status='Non-Logged' where COCStatementID = @COCStatementID";
                                DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"]);
                                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", i);
                                DLdb.SQLST2.CommandType = CommandType.Text;
                                DLdb.SQLST2.Connection = DLdb.RS2;
                                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.RS2.Close();

                                DLdb.RS2.Open();
                                DLdb.SQLST2.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                                DLdb.SQLST2.Parameters.AddWithValue("Message", "Certificate Sold to " + plumNamed);
                                DLdb.SQLST2.Parameters.AddWithValue("Username", SupName.ToString());
                                DLdb.SQLST2.Parameters.AddWithValue("TrackingTypeID", "0");
                                DLdb.SQLST2.Parameters.AddWithValue("CertificateID", i.ToString());
                                DLdb.SQLST2.CommandType = CommandType.Text;
                                DLdb.SQLST2.Connection = DLdb.RS2;
                                theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.RS2.Close();
                            }
                            // ADD LAST
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "Update COCStatements set UserID = @UserID, isStock = '0',DatePurchased=getdate(),status='Non-Logged' where COCStatementID = @COCStatementID";
                            DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"]);
                            DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", theSqlDataReader["EndRange"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();

                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                            DLdb.SQLST3.Parameters.AddWithValue("Message", "Certificate Sold to " + plumNamed);
                            DLdb.SQLST3.Parameters.AddWithValue("Username", "0");
                            DLdb.SQLST3.Parameters.AddWithValue("TrackingTypeID", "0");
                            DLdb.SQLST3.Parameters.AddWithValue("CertificateID", theSqlDataReader["EndRange"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();

                            string userid = "0";
                            string addy = "";
                            string name = "";
                            string cont = "";
                            string email = "";
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "Select * from users where UserID = @UserID";
                            DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                theSqlDataReader3.Read();
                                userid = theSqlDataReader3["UserID"].ToString();
                                name = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
                                cont = theSqlDataReader3["contact"].ToString();
                                email = theSqlDataReader3["email"].ToString();
                                addy = theSqlDataReader3["ResidentialStreet"].ToString() + ",<br/> " + theSqlDataReader3["ResidentialSuburb"].ToString() + ",<br/> " + theSqlDataReader3["ResidentialCity"].ToString() + ",<br/> " + theSqlDataReader3["ResidentialCode"].ToString() + ",<br/> " + theSqlDataReader3["Province"].ToString();
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();

                            string OrderID = theSqlDataReader["OrderID"].ToString();
                            var createPDF = "";
                            // GET THE PERIOD YY AND MM
                            string srtDD = DateTime.Now.Day.ToString();
                            string srtMM = DateTime.Now.Month.ToString();
                            string srtYY = DateTime.Now.Year.ToString();

                            // CREATE THE PDF INVOICE
                            createPDF = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
                                                        "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                        "        <tr>" +
                                                        "            <td>" +
                                                        "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                         "                    <tr>" +
                                                        "                    <td align='left' colspan='2' width='60%'><b>Plumbing Industry Registration Board (PIRB)</b></td>" +
                                                        "                    <td align='left' width='40%'><img src='https://197.242.82.242/inspectit/assets/img/cardlogo.jpg' style=\"height:100px;\"/></td></tr>" +
                                                        "                    </tr>" +
                                                          //"                    <tr>" +
                                                          //"                    <td align='center' colspan='2' width='60%'><img src='https://197.242.82.242/inspectit/assets/img/cardlogo.jpg'/></td>" +
                                                          //"                    <td align='left' width='40%'><font style='font-size:26px;'><b>INVOICE No. : " + theSqlDataReader["OrderID"].ToString() + "</b><br />Period: </b>" + srtMM + "-" + srtYY + "</font></td></tr>" +

                                                          //"                    </tr>" +
                                                          "                    <tr>" +
                                                "                        <td align='left' colspan='2' width='70%'>PO Box 411<br /> Wierdapark<br /> Centurion <br /> 0149 <br /> 0861 747 275 <br /> info@pirb.co.za <br /> www.pirb.co.za <br /><br /> VAT No: 4230255327</td>" +
                                                "                        <td align='left' width='30%'><br /><h3 style='color:red;'><b>PAID</b></h3></td>" +
                                                //"                        <td align='left' width='15%' colspan='2'><br /><h4>Address :</h4>" + address + "</td>" +
                                                "                    </tr>" +

                                                "                    <tr>" +
                                                "                        <td align='left' width='70%'><br /><h4>INVOICE TO :</h4>" + name + "<br/>" + cont + "<br/>" + email + "</td>" +
                                                "                        <td align='left' width='15%' colspan='2'><br /><h4>TAX INVOICE :</h4>" + theSqlDataReader["OrderID"].ToString() + "<br />Date: </b>" + srtDD + "-" + srtMM + "-" + srtYY + "</td>" +
                                                "                    </tr>" +
                                                        //"                    <tr>" +
                                                        //"                        <td align='left' width='70%'><br /><h4></h4>" + name.ToString() + "<br/>" + cont.ToString() + "<br/>" + email.ToString() + "</td>" +
                                                        //"                        <td align='left' width='30%' colspan='2'><br /><h4>Address :</h4>" + addy.ToString() + "</td>" +
                                                        //"                    </tr>" +
                                                        "                    <tr>" +
                                                        "                        <td align='left' width='70%'><br /><h4>Paper Based COC Statements</h4></td>" +
                                                        "                    </tr>" +
                                                        "                    <tr>" +
                                                        "                        <td align='left' colspan='3' valign='top'>" +
                                                        "                            <table border='0' cellpadding='5px' cellspacing='0' width='100%'>" +
                                                        "                               <tr>" +
                                                        "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>COC Type</b></td>" +
                                                        "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>Collection Method</b></td>" +
                                                        "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>Number of COC's</b></td>" +
                                                        "                                </tr>" +
                                                        "                               <tr>" +
                                                        "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>Paper</td>" +
                                                        "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>Pick Up</td>" +
                                                        "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + theSqlDataReader["TotalNoItems"].ToString() + "</td>" +
                                                        "                                </tr>" +
                                                        "                               <tr>" +
                                                        "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
                                                        "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>Sub Total<b></td>" +
                                                        "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + Convert.ToDecimal(theSqlDataReader["SubTotal"]).ToString("0.##") + "</b></td>" +
                                                        "                                </tr>" +
                                                        "                               <tr>" +
                                                        "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
                                                        "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>VAT @15%<b></td>" +
                                                        "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + Convert.ToDecimal(theSqlDataReader["Vat"]).ToString("0.##") + "</b></td>" +
                                                        "                                </tr>" +
                                                        "                               <tr>" +
                                                        "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
                                                        "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>Total Amount<b></td>" +
                                                        "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + Convert.ToDecimal(theSqlDataReader["Total"]).ToString("0.##") + "</b></td>" +
                                                        "                                </tr>" +
                                                        "                            </table>" +
                                                        "                        </td>" +
                                                        "                    </tr>" +
                                                        "                    <tr>" +
                                                        "                       <td align='middle'><b>Powered By InspectIT</b></td>" +
                                                        "                        <td><td align='left'><img src='https://197.242.82.242/inspectit/assets/img/logo.png'/></td>" +
                                                        "                    </tr>" +
                                                        "                </table>" +
                                                        "            </td>" +
                                                        "        </tr>" +
                                                        "    </table>" +
                                                        "</body>");

                            string filename = userid + name + "_invoice_" + OrderID + "_" + srtMM + "-" + srtYY + ".pdf";
                            var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(createPDF);
                            string path = Server.MapPath("~/invoices/") + filename;
                            File.WriteAllBytes(path, pdfBytes);

                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "Update Orders set isPaid = '1',PDFName=@PDFName where OrderID=@OrderID ";
                            DLdb.SQLST3.Parameters.AddWithValue("OrderID", DLdb.Decrypt(Request.QueryString["oid"].ToString()));
                            DLdb.SQLST3.Parameters.AddWithValue("PDFName", filename);
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();

                            string eHTMLBody = "Dear " + name + "<br /><br />You have purchased " + theSqlDataReader["TotalNoItems"].ToString() + " Paper C.O.C for Pick up. At the cost of R" + Convert.ToDecimal(theSqlDataReader["Total"]).ToString("0.##") + ", please <a href='https://197.242.82.242/inspectit/'>login</a> to view invoice.<br /><br />Regards<br />Inspect-It Administrator";
                            string eSubject = "Inspect-IT C.O.C Statement Purchase";
                            DLdb.sendEmail(eHTMLBody, eSubject, "mathewpayne@gmail.com", email, path);
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    //REQUIRED: CREATE SUPPLIER INVOICE *-* [USE SUPPLIERID IN ORDER TABLE]
                   
                    DLdb.DB_Close();

                    Response.Redirect("ViewCOCStatementSupplier?msg=" + DLdb.Encrypt("Thank you, Purchase successful"));

                } else
                {
                    errormsg.InnerHtml = "Invalid OTP Code";
                    errormsg.Visible = true;
                }
            }
            else
            {
                errormsg.InnerHtml = "Please enter OTP Code";
                errormsg.Visible = true;
            }
            

        }
    }
}