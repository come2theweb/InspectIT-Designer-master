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
    public partial class VerifyPurchasePlumber : System.Web.UI.Page
    {
        public string UserID = "";
        public string Price = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
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
                        UserID = theSqlDataReader["UserID"].ToString();
                        Price = theSqlDataReader["Total"].ToString();
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
                                NameofPlumber.InnerHtml = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                                NumberTo.InnerHtml = theSqlDataReader2["contact"].ToString();
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
                DLdb.DB_Connect();
                string OTPCode = Session["IIT_OTPCodePlumber"].ToString();
                DLdb.sendSMS(Session["IIT_UID"].ToString(), NumberTo.InnerHtml, "Inspect-It: OTP Resend, please use OTP Code: " + OTPCode);

                DLdb.DB_Close();
            }
        }

        protected void btn_buy_Click(object sender, EventArgs e)
        {
            if (OTPCode.Text != "")
            {
                if (Session["IIT_OTPCodePlumber"].ToString() == OTPCode.Text.ToString())
                {
                    Global DLdb = new Global();
                    DLdb.DB_Connect();
                    
                    string Order = DLdb.Decrypt(Request.QueryString["oid"].ToString());

                    // SEND TO PAYMENT GATEWAY
                    string uid = "";
                    string amount = Request.QueryString["cost"].ToString();
                    string orderId = Order.ToString();
                    string name = "";
                    string description = "";
                    string site = "";
                    string merchant_id = "10140624";
                    string merchant_key = "mbmxqajjukwkb";
                    string ReturnURL = "https://197.242.82.242/inspectit/PF_returnURL";
                    string CancelURL = "https://197.242.82.242/inspectit/PF_cancelURL";
                    string NotifyURL = "https://197.242.82.242/inspectit/PF_notifyURL";
                    
                    StringBuilder str = new StringBuilder();

                    uid = UserID;
                    name = "Inspect IT: " + orderId;
                    description = "Purchased Electronic / Paper COC";

                    // Check if we are using the test or live system
                    string paymentMode = "test";

                    if (paymentMode == "test")
                    {
                        site = "https://sandbox.payfast.co.za/eng/process?";
                        merchant_id = "10009492";
                        merchant_key = "b28g8dznu91dl";
                    }
                    else if (paymentMode == "live")
                    {
                        site = "https://www.payfast.co.za/eng/process?";
                        merchant_id = "10140624";
                        merchant_key = "mbmxqajjukwkb";
                    }

                    str.Append("merchant_id=" + merchant_id);
                    str.Append("&merchant_key=" + merchant_key);
                    str.Append("&return_url=" + ReturnURL);
                    str.Append("&cancel_url=" + CancelURL);
                    str.Append("&notify_url=" + NotifyURL);

                    // SEND ORDER DETAILS

                    str.Append("&m_payment_id=" + HttpUtility.UrlEncode(orderId));
                    str.Append("&amount=" + HttpUtility.UrlEncode(amount));
                    str.Append("&item_name=" + HttpUtility.UrlEncode(name));
                    str.Append("&item_description=" + HttpUtility.UrlEncode(description));

                    Response.Redirect(site + str.ToString());

                    //Response.Redirect("PF_notifyURLtest.aspx?oid=" + orderId);

                    //string SupName = "";
                    //string SupEmail = "";
                    //string supAddress = "";
                    //string OrderID = DLdb.Decrypt(Request.QueryString["oid"].ToString());
                    //string NoCOCsPurchase = "";
                    //string Type = "";
                    //string Method = "";
                    //string totalAmountdue = "";
                    //string VATCost = "";

                    //DLdb.RS.Open();
                    //DLdb.SQLST.CommandText = "Select * from Orders where OrderID = @OrderID";
                    //DLdb.SQLST.Parameters.AddWithValue("OrderID", DLdb.Decrypt(Request.QueryString["oid"].ToString()));
                    //DLdb.SQLST.CommandType = CommandType.Text;
                    //DLdb.SQLST.Connection = DLdb.RS;
                    //SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    //if (theSqlDataReader.HasRows)
                    //{
                    //    while (theSqlDataReader.Read())
                    //    {
                    //        //int sRange = Convert.ToInt32(theSqlDataReader["StartRange"].ToString());
                    //        //int eRange = Convert.ToInt32(theSqlDataReader["EndRange"].ToString());
                    //        NoCOCsPurchase = theSqlDataReader["TotalNoItems"].ToString();
                    //        Type = theSqlDataReader["COCType"].ToString();
                    //        Method = theSqlDataReader["Method"].ToString();
                    //        totalAmountdue = theSqlDataReader["Total"].ToString();
                    //        VATCost = theSqlDataReader["Vat"].ToString();

                    //        //for (int i = sRange; i < eRange; i++)
                    //        //{
                    //        //    DLdb.RS2.Open();
                    //        //    DLdb.SQLST2.CommandText = "Update COCStatements set UserID = @UserID, isStock = '0',SupplierID = null where COCStatementID = @COCStatementID";
                    //        //    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"]);
                    //        //    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", i);
                    //        //    DLdb.SQLST2.CommandType = CommandType.Text;
                    //        //    DLdb.SQLST2.Connection = DLdb.RS2;
                    //        //    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    //        //    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    //        //    DLdb.SQLST2.Parameters.RemoveAt(0);
                    //        //    DLdb.SQLST2.Parameters.RemoveAt(0);
                    //        //    DLdb.RS2.Close();
                    //        //}
                    //    }
                    //}

                    //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    //DLdb.SQLST.Parameters.RemoveAt(0);
                    //DLdb.RS.Close();

                    ////REQUIRED: CREATE SUPPLIER INVOICE *-* [USE SUPPLIERID IN ORDER TABLE]
                    //string UserName = "";
                    //string UserEmail = "";
                    //string NumberTo = "";
                    //string useridss = Session["IIT_UID"].ToString();

                    ////GET THE USERS DETAILS
                    //DLdb.RS3.Open();
                    //DLdb.SQLST3.CommandText = "select * from users where userid = @UserID";
                    //DLdb.SQLST3.Parameters.AddWithValue("UserID", Session["IIT_UID"]);
                    //DLdb.SQLST3.CommandType = CommandType.Text;
                    //DLdb.SQLST3.Connection = DLdb.RS3;
                    //SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    //if (theSqlDataReader3.HasRows)
                    //{
                    //    theSqlDataReader3.Read();
                    //    UserName = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
                    //    if (theSqlDataReader3["CompanyIsBillingInfo"].ToString() == "True")
                    //    {
                    //        DLdb.RS4.Open();
                    //        DLdb.SQLST4.CommandText = "select * from Companies where CompanyID = @CompanyID";
                    //        DLdb.SQLST4.Parameters.AddWithValue("CompanyID", theSqlDataReader3["company"].ToString());
                    //        DLdb.SQLST4.CommandType = CommandType.Text;
                    //        DLdb.SQLST4.Connection = DLdb.RS4;
                    //        SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                    //        if (theSqlDataReader4.HasRows)
                    //        {
                    //            theSqlDataReader4.Read();
                    //            UserName = UserName + "<br /> " + theSqlDataReader4["CompanyName"].ToString();
                    //            UserEmail = theSqlDataReader4["companyemail"].ToString();
                    //            NumberTo = theSqlDataReader4["companycontactno"].ToString();

                    //        }

                    //        if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                    //        DLdb.SQLST4.Parameters.RemoveAt(0);
                    //        DLdb.RS4.Close();
                    //    }
                    //    else
                    //    {

                    //        UserEmail = theSqlDataReader3["email"].ToString();
                    //        if (theSqlDataReader3["contact"].ToString() != "" && theSqlDataReader3["contact"] != DBNull.Value)
                    //        {
                    //            NumberTo = theSqlDataReader3["contact"].ToString();
                    //        }
                    //    }
                    //}

                    //if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    //DLdb.SQLST3.Parameters.RemoveAt(0);
                    //DLdb.RS3.Close();

                    //var createPDF = "";
                    //// GET THE PERIOD YY AND MM
                    //string srtMM = DateTime.Now.Month.ToString();
                    //string srtYY = DateTime.Now.Year.ToString();

                    //// CREATE THE PDF INVOICE
                    //createPDF = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
                    //                            "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                    //                            "        <tr>" +
                    //                            "            <td>" +
                    //                            "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                    //                            "                    <tr>" +
                    //                            "                    <td align='center' colspan='2' width='60%'><img src='https://197.242.82.242/inspectit/assets/img/cardlogo.jpg'/></td>" +
                    //                            "                    <td align='left' width='40%'><font style='font-size:26px;'><b>INVOICE No. : IVN00" + OrderID + "</b><br />Period: </b>" + srtMM + "-" + srtYY + "</font></td></tr>" +
                    //                            "                    </tr>" +
                    //                            "                    <tr>" +
                    //                            "                        <td align='left' width='70%'><br /><h4>To :</h4>" + UserName + "<br/>" + NumberTo + "<br/>" + UserEmail + "</td>" +
                    //                            //"                        <td align='left' width='15%' colspan='2'><br /><h4>Address :</h4>" + address + "</td>" +
                    //                            "                    </tr>" +
                    //                            "                    <tr>" +
                    //                            "                        <td align='left' colspan='3' valign='top'>" +
                    //                            "                            <table border='0' cellpadding='5px' cellspacing='0' width='100%'>" +
                    //                            "                               <tr>" +
                    //                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>COC Type</b></td>" +
                    //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>Collection Method</b></td>" +
                    //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>Number of COC's</b></td>" +
                    //                            "                                </tr>" +
                    //                            "                               <tr>" +
                    //                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + Type + "</td>" +
                    //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + Method + "</td>" +
                    //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + NoCOCsPurchase + "</td>" +
                    //                            "                                </tr>" +
                    //                            "                               <tr>" +
                    //                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
                    //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>VAT @15%<b></td>" +
                    //                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + VATCost + "</b></td>" +
                    //                            "                                </tr>" +
                    //                            "                               <tr>" +
                    //                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
                    //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>Total Amount<b></td>" +
                    //                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + totalAmountdue + "</b></td>" +
                    //                            "                                </tr>" +
                    //                            "                            </table>" +
                    //                            "                        </td>" +
                    //                            "                    </tr>" +
                    //                            "                    <tr>" +
                    //                            "                       <td align='middle'><b>Powered By InspectIT</b></td>" +
                    //                            "                        <td><td align='left'><img src='https://197.242.82.242/inspectit/assets/img/logo.png'/></td>" +
                    //                            "                    </tr>" +
                    //                            "                </table>" +
                    //                            "            </td>" +
                    //                            "        </tr>" +
                    //                            "    </table>" +
                    //                            "</body>");

                    //string filename = "invoice_" + OrderID + "_" + srtMM + "-" + srtYY + ".pdf";
                    //var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(createPDF);
                    //string path = Server.MapPath("~/invoices/") + filename;
                    //File.WriteAllBytes(path, pdfBytes);

                    //DLdb.RS.Open();
                    //DLdb.SQLST.CommandText = "update Orders set PDFName=@PDFName where OrderID=@OrderID";
                    //DLdb.SQLST.Parameters.AddWithValue("OrderID", OrderID);
                    //DLdb.SQLST.Parameters.AddWithValue("PDFName", filename);
                    //DLdb.SQLST.CommandType = CommandType.Text;
                    //DLdb.SQLST.Connection = DLdb.RS;
                    //theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    //DLdb.SQLST.Parameters.RemoveAt(0);
                    //DLdb.SQLST.Parameters.RemoveAt(0);
                    //DLdb.RS.Close();

                    //DLdb.DB_Close();

                    //// SMS
                    //if (NumberTo.ToString() != "")
                    //{
                    //    DLdb.sendSMS(useridss, NumberTo.ToString(), "Inspect-It: You have purchased " + NoCOCsPurchase + " " + Type + " C.O.C for " + Method + ". Amount: R" + totalAmountdue + ".");
                    //}

                    //// EMAIL
                    //if (UserEmail.ToString() != "")
                    //{
                    //    string eHTMLBody = "Dear " + UserName + "<br /><br />You have purchased " + NoCOCsPurchase + " " + Type + " C.O.C for " + Method + ". At the cost of R" + totalAmountdue + "., please <a href='https://197.242.82.242/inspectit/'>login</a> to view invoice.<br /><br />Regards<br />Inspect-It Administrator";
                    //    string eSubject = "Inspect-IT C.O.C Statement Purchase";
                    //    DLdb.sendEmail(eHTMLBody, eSubject, "mathewpayne@gmail.com", UserEmail, "");
                    //}

                    DLdb.DB_Close();

                    //Response.Redirect("ViewCOCStatement?msg=" + DLdb.Encrypt("Your purchase was complete, thank you."));

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