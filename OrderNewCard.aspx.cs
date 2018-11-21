using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace InspectIT
{
    public partial class OrderNewCard : System.Web.UI.Page
    {
        //public decimal COCElectronic = 0;
        //public decimal COCPaper = 0;
        //public decimal CourierDelivery = 0;
        //public decimal RegisteredPostDelivery = 0;
        //public decimal Collection = 0;
        public Boolean signatureImg = false;
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
                Div1.InnerHtml = msg;
                Div1.Visible = true;
            }
            if (!IsPostBack)
            {
               
            }
            btn_buy.Enabled = false;

            dispErr.Visible = false;
            if (Session["IIT_isSuspended"].ToString() == "True" || Session["IIT_isSuspended"].ToString() == "Expired")
            {
                errormsg.InnerHtml = "You can't order a new card because you are either suspended or your registration has expired. Please pay your registration renewal fee";
                errormsg.Visible = true;
                btn_buy.Enabled = false;
            }
            else
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID = @UserID and NoCOCpurchases <> '0'";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["Signature"] == DBNull.Value || theSqlDataReader["Signature"].ToString() == "")
                        {
                            signatureImg = false;
                        }
                        else
                        {
                            signatureImg = true;
                        }
                    }
                }
                else
                {
                    errormsg.Visible = true;
                    btn_buy.Visible = false;
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }
            
            DLdb.DB_Close();
        }

        protected void btn_buy_Click(object sender, EventArgs e)
        {

            Global DLdb = new Global();
            DLdb.DB_Connect();
            
                errormsg.Visible = false;
                string OrderID = "";

                string Method = "Collect at PIRB Offices";
                            

                if (RegisteredPostPurchaseCOC.Checked == true)
                {
                    Method = "Registered Post";
                }
                if (CourierPurchaseCOC.Checked == true)
                {
                    Method = "Courier";
                }
                            
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "INSERT INTO Orders (UserID,Description,TotalNoItems,SubTotal,Vat,Delivery,Total,Method,isPaid) values (@UserID,@Description,@TotalNoItems,@SubTotal,@Vat,@Delivery,@Total,@Method,@isPaid); Select Scope_Identity() as OrderID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("Description", "Order A New Card");
                DLdb.SQLST.Parameters.AddWithValue("TotalNoItems", "1");
                DLdb.SQLST.Parameters.AddWithValue("SubTotal", costOfCard.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Vat", VATCOCPurchase.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Delivery", DeliveryCostCOCPurchase.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Total", TotalDueCOCPurchase.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Method", Method);
                DLdb.SQLST.Parameters.AddWithValue("isPaid", "0");
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    OrderID = theSqlDataReader["OrderID"].ToString();
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                            
                string UserName = "";
                string UserEmail = "";
                string NumberTo = "";
                //string useridss = Session["IIT_UID"].ToString();

                //GET THE USERS DETAILS
                DLdb.RS3.Open();
                DLdb.SQLST3.CommandText = "select * from users where userid = @UserID";
                DLdb.SQLST3.Parameters.AddWithValue("UserID", Session["IIT_UID"]);
                DLdb.SQLST3.CommandType = CommandType.Text;
                DLdb.SQLST3.Connection = DLdb.RS3;
                SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                if (theSqlDataReader3.HasRows)
                {
                    theSqlDataReader3.Read();
                    UserName = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
                    UserEmail = theSqlDataReader3["email"].ToString();
                    if (theSqlDataReader3["contact"].ToString() != "" && theSqlDataReader3["contact"] != DBNull.Value)
                    {
                        NumberTo = theSqlDataReader3["contact"].ToString();
                    }
                }

                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                DLdb.SQLST3.Parameters.RemoveAt(0);
                DLdb.RS3.Close();

            string Order = OrderID;

            // SEND TO PAYMENT GATEWAY
            string uid = "";
            string amount = TotalDueCOCPurchase.Text.ToString();
            string orderId = Order.ToString();
            string name = "";
            string description = "";
            string site = "";
            string merchant_id = "10140624";
            string merchant_key = "mbmxqajjukwkb";
            string ReturnURL = "https://197.242.82.242/inspectit/PF_returnURL";
            string CancelURL = "https://197.242.82.242/inspectit/PF_cancelURL";
            string NotifyURL = "https://197.242.82.242/inspectit/PF_notifyURLOther";

            StringBuilder str = new StringBuilder();

            uid = Session["IIT_UID"].ToString();
            name = "Inspect IT: " + orderId;
            description = "Order New Card";

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
            //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>"+ NoCOCsPurchase.Text.ToString() + "</td>" +
            //                            "                                </tr>" +

            //                            "                               <tr>" +
            //                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
            //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>Total Amount<b></td>" +
            //                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + totalAmountdue.Text.ToString() + "</b></td>" +
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
            //    DLdb.sendSMS(useridss, NumberTo.ToString(), "Inspect-It: You have purchased " + NoCOCsPurchase.Text.ToString() + " " + Type + " C.O.C for " + Method + ". Amount: R" + totalAmountdue.Text.ToString() + ".");
            //}

            //// EMAIL
            //if (UserEmail.ToString() != "")
            //{
            //    string eHTMLBody = "Dear " + UserName + "<br /><br />You have purchased " + NoCOCsPurchase.Text.ToString() + " " + Type + " C.O.C for " + Method + ". At the cost of R" + totalAmountdue.Text.ToString() + "., please <a href='https://197.242.82.242/inspectit/'>login</a> to view invoice.<br /><br />Regards<br />Inspect-It Administrator";
            //    string eSubject = "Inspect-IT C.O.C Statement Purchase";
            //    DLdb.sendEmail(eHTMLBody, eSubject, "mathewpayne@gmail.com", UserEmail, "");
            //}

            //string OTPCode = DLdb.CreateNumber(5);
            //Session["IIT_OTPCodePlumber"] = OTPCode;
            //// Response.Redirect("ViewCOCStatement?msg=" + DLdb.Encrypt("Your purchase was complete, thank you."));
            //DLdb.sendSMS(Session["IIT_UID"].ToString(), NumberTo.ToString(), "Inspect-It: You would like to purchase " + NoCOCsPurchase.Text.ToString() + " " + Type + " C.O.C for " + Method + ". Amount: R" + totalAmountdue.Text.ToString() + ". OTP Code: " + OTPCode);

            //Response.Redirect("VerifyPurchasePlumber?oid=" + DLdb.Encrypt(OrderID) + "&cost=" + totalAmountdue.Text.ToString());


        }


        protected void Group1_CheckedChanged(Object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            btn_buy.Enabled = true;
            decimal CardCost = 0;
            decimal CourierDelivery = 0;
            decimal RegisteredPostDelivery = 0;
            decimal Collection = 0;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Rates";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["ID"].ToString() == "24")
                    {
                        CourierDelivery = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                    else if (theSqlDataReader["ID"].ToString() == "25")
                    {
                        RegisteredPostDelivery = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                    else if (theSqlDataReader["ID"].ToString() == "26")
                    {
                        Collection = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                    else if (theSqlDataReader["ID"].ToString() == "37")
                    {
                        CardCost = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                    costOfCard.Text = CardCost.ToString();
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();
            
            decimal vats = 0;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from settings where ID='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                vats = Convert.ToDecimal(theSqlDataReader["VatPercentage"].ToString());
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();
            decimal vat = Convert.ToDecimal(vats);
            string a = "0.##";


            decimal vatC = CardCost * vat;
            VATCOCPurchase.Text = vatC.ToString();
            decimal totalAmount = CardCost + vatC;

            if (CourierPurchaseCOC.Checked == true)
            {
                DeliveryCostCOCPurchase.Text = CourierDelivery.ToString();
                totalAmount = totalAmount + CourierDelivery;
            }
            else if (RegisteredPostPurchaseCOC.Checked == true)
            {
                DeliveryCostCOCPurchase.Text = RegisteredPostDelivery.ToString();
                totalAmount = totalAmount + RegisteredPostDelivery;
            }
            else if (CollectPurchaseCOC.Checked == true)
            {
                DeliveryCostCOCPurchase.Text = Collection.ToString();
                totalAmount = totalAmount + Collection;
            }
            TotalDueCOCPurchase.Text = totalAmount.ToString(a);

            //Response.Write(totalCost);
            //Response.End();

            DLdb.DB_Close();
        }
        
    }
}