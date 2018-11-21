using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Collections;
using System.Web;

namespace InspectIT
{
    public partial class PF_notifyURL : System.Web.UI.Page
    {
        string orderId = "";
        string processorOrderId = "";
        string strPostedVariables = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // Can't have a postback on this page, it is called
            // once by the payment processor.
            if (Page.IsPostBack) return;

            try
            {
                // Get the posted variables. Exclude the signature (it must be excluded when we hash and also when we validate).
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                NameValueCollection arrPostedVariables = new NameValueCollection(); // We will use later to post data
                NameValueCollection req = Request.Form;
                string key = "";
                string value = "";
                for (int i = 0; i < req.Count; i++)
                {
                    key = req.Keys[i];
                    value = req[i];

                    if (key != "signature")
                    {
                        strPostedVariables += key + "=" + value + "&";
                        arrPostedVariables.Add(key, value);
                    }
                }

                // Remove the last &
                strPostedVariables = strPostedVariables.TrimEnd(new char[] { '&' });

                // Also get the Ids early. They are used to log errors to the orders table.
                orderId = Request.Form["m_payment_id"];
                processorOrderId = Request.Form["pf_payment_id"];

                // Are we testing or making live payments
                string site = "";
                string merchant_id = "";
                string paymentMode = "test"; 

                if (paymentMode == "test")
                {
                    site = "https://sandbox.payfast.co.za/eng/query/validate";
                    merchant_id = "10009492";
                }
                else if (paymentMode == "live")
                {
                    site = "https://www.payfast.co.za/eng/query/validate";
                    merchant_id = "10140624";
                }
                else
                {
                    throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
                }

                // Get the posted signature from the form.
                string postedSignature = Request.Form["signature"];

                if (string.IsNullOrEmpty(postedSignature))
                    throw new Exception("Signature parameter cannot be null");

                // Check if this is a legitimate request from the payment processor
                PerformSecurityChecks(arrPostedVariables, merchant_id);

                // The request is legitimate. Post back to payment processor to validate the data received
                ValidateData(site, arrPostedVariables);

                // All is valid, process the order
                ProcessOrder(arrPostedVariables);
            }
            catch (Exception ex)
            {
                // An error occurred
                Global DLdb = new Global();
                DLdb.ERR_log("0010110", "0", "Payfast", ex.ToString());
            }
        }

        private void PerformSecurityChecks(NameValueCollection arrPostedVariables, string merchant_id)
        {
            // Verify that we are the intended merchant
            string receivedMerchant = arrPostedVariables["merchant_id"];

            if (receivedMerchant != merchant_id)
                throw new Exception("Mechant ID mismatch");

            // Verify that the request comes from the payment processor's servers.

            // Get all valid websites from payment processor
            string[] validSites = new string[] { "www.payfast.co.za", "sandbox.payfast.co.za", "w1w.payfast.co.za", "w2w.payfast.co.za" };

            // Get the requesting ip address
            string requestIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(requestIp))
                throw new Exception("IP address cannot be null");

            // Is address in list of websites
            if (!IsIpAddressInList(validSites, requestIp))
                throw new Exception("IP address invalid");
        }

        private bool IsIpAddressInList(string[] validSites, string ipAddress)
        {
            // Get the ip addresses of the websites
            ArrayList validIps = new ArrayList();

            for (int i = 0; i < validSites.Length; i++)
            {
                validIps.AddRange(System.Net.Dns.GetHostAddresses(validSites[i]));
            }

            IPAddress ipObject = IPAddress.Parse(ipAddress);

            if (validIps.Contains(ipObject))
                return true;
            else
                return false;
        }

        private void ValidateData(string site, NameValueCollection arrPostedVariables)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            WebClient webClient = null;

            try
            {
                webClient = new WebClient();
                byte[] responseArray = webClient.UploadValues(site, arrPostedVariables);

                // Get the response and replace the line breaks with spaces
                string result = Encoding.ASCII.GetString(responseArray);
                result = result.Replace("\r\n", " ").Replace("\r", "").Replace("\n", " ");

                // Was the data valid?
                if (result == null || !result.StartsWith("VALID"))
                    throw new Exception("Data validation failed");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (webClient != null)
                    webClient.Dispose();
            }
        }

        private void ProcessOrder(NameValueCollection arrPostedVariables)
        {
            // Determine from payment status if we are supposed to credit or not
            string paymentStatus = arrPostedVariables["payment_status"];
            //string paymentStatus = "COMPLETE";

            Global DLdb = new Global();
            DLdb.DB_Connect();
            try
            {
                string uid = "";
                int numcoc_purchase = 0;
                string amount = "";
                string orderId = arrPostedVariables["m_payment_id"];
                //string orderId = "7";
                string name = "";
                string description = "";

                string userName = "";
                string UserSurname = "";
                string userEmailAddress = "";
                
                if (paymentStatus == "COMPLETE")
                {
                    // GET USERID
                    string UserID = "";
                    string SupName = "";
                    string SupEmail = "";
                    string supAddress = "";
                    string NoCOCsPurchase = "";
                    string Type = "";
                    string Method = "";
                    string totalAmountdue = "";
                    string VATCost = "";
                    string subTotal = "";
                    decimal CocCost = 0;
                    string Plumbname = "";

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from orders where orderid = @orderid and isactive = '1'";
                    DLdb.SQLST.Parameters.AddWithValue("orderid", orderId);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        UserID = theSqlDataReader["UserID"].ToString();
                        NoCOCsPurchase = theSqlDataReader["TotalNoItems"].ToString();
                        Type = theSqlDataReader["COCType"].ToString();
                        Method = theSqlDataReader["Method"].ToString();
                        totalAmountdue = theSqlDataReader["Total"].ToString();
                        VATCost = theSqlDataReader["Vat"].ToString();
                        subTotal = theSqlDataReader["subTotal"].ToString();
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from Users where UserID = @UserID and isactive = '1'";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                     theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        Plumbname = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    if (Type == "Paper")
                    {
                        DLdb.SQLST.CommandText = "select * from rates where ID = '36'";
                    }
                    else
                    {
                        DLdb.SQLST.CommandText = "select * from rates where ID = '40'";
                    }
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        CocCost = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.RS.Close();

                    // UPDATE ORDERS
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Update Orders set isPaid = '1' where orderid = @orderid and isactive = '1'";
                    DLdb.SQLST.Parameters.AddWithValue("orderid", orderId);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    string ccid = "";
                    int cnt = 1;
                    decimal pamount = Convert.ToInt32(NoCOCsPurchase);
                    if (Type == "Paper")
                    {

                    }
                    else
                    {
                        while (cnt <= pamount)
                        {
                            // add the cocstatements to the user
                            DLdb.RS.Open();
                            DLdb.SQLST.CommandText = "insert into COCStatements (OrderID,UserID,Type,DatePurchased,Status, isPaid,COCNumber) values (@OrderID,@UserID,@Type,getdate(),'Non-Logged', '1',@COCNumber); Select scope_identity() as cocid;";
                            DLdb.SQLST.Parameters.AddWithValue("OrderID", orderId);
                            DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                            DLdb.SQLST.Parameters.AddWithValue("Type", Type);
                            DLdb.SQLST.Parameters.AddWithValue("COCNumber", "");
                            DLdb.SQLST.CommandType = CommandType.Text;
                            DLdb.SQLST.Connection = DLdb.RS;
                            theSqlDataReader = DLdb.SQLST.ExecuteReader();
                            if (theSqlDataReader.HasRows)
                            {
                                theSqlDataReader.Read();
                                ccid = theSqlDataReader["cocid"].ToString();
                            }
                            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.RS.Close();

                            DLdb.RS.Open();
                            DLdb.SQLST.CommandText = "update COCStatements set COCNumber=@COCNumber where COCstatementid=@COCstatementid";
                            DLdb.SQLST.Parameters.AddWithValue("COCstatementid", ccid);
                            DLdb.SQLST.Parameters.AddWithValue("COCNumber", ccid);
                            DLdb.SQLST.CommandType = CommandType.Text;
                            DLdb.SQLST.Connection = DLdb.RS;
                            theSqlDataReader = DLdb.SQLST.ExecuteReader();

                            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.RS.Close();

                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                            DLdb.SQLST2.Parameters.AddWithValue("Message", Type + " Certificate Purchased and Generated");
                            DLdb.SQLST2.Parameters.AddWithValue("Username", Plumbname);
                            DLdb.SQLST2.Parameters.AddWithValue("TrackingTypeID", "0");
                            DLdb.SQLST2.Parameters.AddWithValue("CertificateID", ccid.ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            cnt++;
                        }
                    }
                    

                    string UserName = "";
                    string UserEmail = "";
                    string NumberTo = "";
                    decimal startAmount = 0;

                    if (Type == "Paper")
                    {
                        
                    }
                    else
                    {
                        startAmount = Convert.ToDecimal(ccid) - pamount;
                        Method = (startAmount + 1) + " - " + ccid;
                    }

                    //GET THE USERS DETAILS
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "select * from users where userid = @UserID";
                    DLdb.SQLST3.Parameters.AddWithValue("UserID", UserID);
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        theSqlDataReader3.Read();
                        UserName = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
                        if (theSqlDataReader3["CompanyIsBillingInfo"].ToString() == "True")
                        {
                            DLdb.RS4.Open();
                            DLdb.SQLST4.CommandText = "select * from Companies where CompanyID = @CompanyID";
                            DLdb.SQLST4.Parameters.AddWithValue("CompanyID", theSqlDataReader3["company"].ToString());
                            DLdb.SQLST4.CommandType = CommandType.Text;
                            DLdb.SQLST4.Connection = DLdb.RS4;
                            SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                            if (theSqlDataReader4.HasRows)
                            {
                                theSqlDataReader4.Read();
                                UserName = UserName + "<br /> " + theSqlDataReader4["CompanyName"].ToString();
                                UserEmail = theSqlDataReader4["companyemail"].ToString();
                                NumberTo = theSqlDataReader4["companycontactno"].ToString();

                            }

                            if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                            DLdb.SQLST4.Parameters.RemoveAt(0);
                            DLdb.RS4.Close();
                        }
                        else
                        {
                            UserEmail = theSqlDataReader3["email"].ToString();
                            if (theSqlDataReader3["contact"].ToString() != "" && theSqlDataReader3["contact"] != DBNull.Value)
                            {
                                NumberTo = theSqlDataReader3["contact"].ToString();
                            }
                        }

                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();

                    var createPDF = "";
                    // GET THE PERIOD YY AND MM
                    string srtDD = DateTime.Now.Day.ToString();
                    string srtMM = DateTime.Now.Month.ToString();
                    string srtYY = DateTime.Now.Year.ToString();
                    decimal qtyAmount = Convert.ToDecimal(NoCOCsPurchase) * CocCost;

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
                                                 //"                    <td align='left' width='40%'><font style='font-size:26px;'><b>INVOICE No. : IVN00" + orderId + "</b><br />Period: </b>" + srtDD + "-" + srtMM + "-" + srtYY + "</font></td></tr>" +
                                                 //"                    </tr>" +
                                                 "                    <tr>" +
                                                "                        <td align='left' colspan='2' width='70%'>PO Box 411<br /> Wierdapark<br /> Centurion <br /> 0149 <br /> 0861 747 275 <br /> info@pirb.co.za <br /> www.pirb.co.za <br /><br /> VAT No: 4230255327</td>" +
                                                "                        <td align='left' width='30%'><br /><h3 style='color:red;'><b>PAID</b></h3></td>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <td align='left' width='70%'><br /><h4>INVOICE TO :</h4>" + UserName + "<br/>" + NumberTo + "<br/>" + UserEmail + "</td>" +
                                                "                        <td align='left' width='15%' colspan='2'><br /><h4>TAX INVOICE : " + orderId + "</h4><br />Date: </b>" + srtDD + "/" + srtMM + "/" + srtYY + "</td>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <td align='left' colspan='3' valign='top'>" +
                                                "                            <table border='0' cellpadding='5px' cellspacing='0' width='100%'>" +
                                                "                               <tr>" +
                                                "                                   <td style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>Activity</b></td>" +
                                                "                                   <td style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>Description</b></td>" +
                                                "                                   <td style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>Qty</b></td>" +
                                                "                                   <td style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>Rate</b></td>" +
                                                "                                   <td style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>Amount</b></td>" +
                                                "                                </tr>" +
                                                "                               <tr>" +
                                                "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + Type + "</td>" +
                                                "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + Method + "</td>" +
                                                "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + NoCOCsPurchase + "</td>" +
                                                "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + CocCost + "</td>" +
                                                "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + qtyAmount + "</td>" +
                                                "                                </tr>" +
                                                "                               <tr>" +
                                                "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
                                                "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>Sub Total<b></td>" +
                                                "                                   <td width='15%' colspan=\"3\" style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + subTotal + "</b></td>" +
                                                "                                </tr>" +
                                                "                               <tr>" +
                                                "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
                                                "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>VAT @15%<b></td>" +
                                                "                                   <td width='15%' colspan=\"3\" style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + VATCost + "</b></td>" +
                                                "                                </tr>" +
                                                "                               <tr>" +
                                                "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
                                                "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>Total Amount<b></td>" +
                                                "                                   <td width='15%' colspan=\"3\" style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + totalAmountdue + "</b></td>" +
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

                    string filename = "invoice_" + orderId + "_" + srtMM + "-" + srtYY + ".pdf";
                    var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(createPDF);
                    string path = Server.MapPath("~/invoices/") + filename;
                    File.WriteAllBytes(path, pdfBytes);

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update Orders set PDFName=@PDFName where OrderID=@OrderID";
                    DLdb.SQLST.Parameters.AddWithValue("OrderID", orderId);
                    DLdb.SQLST.Parameters.AddWithValue("PDFName", filename);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                    
                    if (NumberTo.ToString() != "")
                    {
                        if (Type == "Paper")
                        {
                            DLdb.sendSMS(UserID, NumberTo.ToString(), "Inspect-It: You have purchased " + NoCOCsPurchase + " " + Type + " C.O.C for " + Method + ". Amount: R" + totalAmountdue + ".");
                        }
                        else
                        {
                            DLdb.sendSMS(UserID, NumberTo.ToString(), "Inspect-It: You have purchased " + NoCOCsPurchase + " " + Type + " C.O.C. Amount: R" + totalAmountdue + ".");
                        }
                    }

                    // EMAIL
                    if (UserEmail.ToString() != "")
                    {
                        string eHTMLBody = "";
                        if (Type == "Paper")
                        {
                             eHTMLBody = "Dear " + UserName + "<br /><br />You have purchased " + NoCOCsPurchase + " " + Type + " C.O.C for " + Method + ". At the cost of R" + totalAmountdue + "., please <a href='https://197.242.82.242/inspectit/'>login</a> to view invoice.<br /><br />Regards<br />Inspect-It Administrator";
                        }
                        else
                        {
                             eHTMLBody = "Dear " + UserName + "<br /><br />You have purchased " + NoCOCsPurchase + " " + Type + " C.O.C. At the cost of R" + totalAmountdue + ", please <a href='https://197.242.82.242/inspectit/'>login</a> to view invoice.<br /><br />Regards<br />Inspect-It Administrator";
                        }
                        string eSubject = "Inspect-IT C.O.C Statement Purchase";
                        DLdb.sendEmail(eHTMLBody, eSubject, "mathewpayne@gmail.com", UserEmail, path);
                    }

                    DLdb.DB_Close();

                }
                else if (paymentStatus == "FAILED")
                {
                    DLdb.ERR_log("0010110", "0", "Payfast", "FAILED: " + orderId);
                }
                else
                {
                    DLdb.ERR_log("0010110", "0", "Payfast", "Investigate: " + orderId);
                }

            }
            catch (Exception ex)
            {
                DLdb.ERR_log("0010110", "0", "Payfast", ex.ToString());
            }

        }
    }
}