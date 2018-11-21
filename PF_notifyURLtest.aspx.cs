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
    public partial class PF_notifyURLtest : System.Web.UI.Page
    {
        string orderId = "";
        string processorOrderId = "";
        string strPostedVariables = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string paymentStatus = "COMPLETE";

            Global DLdb = new Global();
            DLdb.DB_Connect();
            try
            {
                string uid = "";
                int numcoc_purchase = 0;
                string amount = "";
                string orderId = Request.QueryString["oid"].ToString();
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
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from Users where UserID = @UserID and isactive = '1'";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", orderId);
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
                    int pamount = Convert.ToInt32(NoCOCsPurchase);
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

                    string UserName = "";
                    string UserEmail = "";
                    string NumberTo = "";
                    string useridss = Session["IIT_UID"].ToString();

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
                    string srtMM = DateTime.Now.Month.ToString();
                    string srtYY = DateTime.Now.Year.ToString();

                    // CREATE THE PDF INVOICE
                    createPDF = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
                                                "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                "        <tr>" +
                                                "            <td>" +
                                                "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                "                    <tr>" +
                                                "                    <td align='center' colspan='2' width='60%'><img src='https://197.242.82.242/inspectit/assets/img/cardlogo.jpg'/></td>" +
                                                "                    <td align='left' width='40%'><font style='font-size:26px;'><b>INVOICE No. : IVN00" + orderId + "</b><br />Period: </b>" + srtMM + "-" + srtYY + "</font></td></tr>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <td align='left' width='70%'><br /><h4>To :</h4>" + UserName + "<br/>" + NumberTo + "<br/>" + UserEmail + "</td>" +
                                                //"                        <td align='left' width='15%' colspan='2'><br /><h4>Address :</h4>" + address + "</td>" +
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
                                                "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + Type + "</td>" +
                                                "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + Method + "</td>" +
                                                "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + NoCOCsPurchase + "</td>" +
                                                "                                </tr>" +
                                                "                               <tr>" +
                                                "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
                                                "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>VAT @15%<b></td>" +
                                                "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + VATCost + "</b></td>" +
                                                "                                </tr>" +
                                                "                               <tr>" +
                                                "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
                                                "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>Total Amount<b></td>" +
                                                "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + totalAmountdue + "</b></td>" +
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

                    // SMS
                    if (NumberTo.ToString() != "")
                    {
                        DLdb.sendSMS(useridss, NumberTo.ToString(), "Inspect-It: You have purchased " + NoCOCsPurchase + " " + Type + " C.O.C for " + Method + ". Amount: R" + totalAmountdue + ".");
                    }

                    // EMAIL
                    if (UserEmail.ToString() != "")
                    {
                        string eHTMLBody = "Dear " + UserName + "<br /><br />You have purchased " + NoCOCsPurchase + " " + Type + " C.O.C for " + Method + ". At the cost of R" + totalAmountdue + "., please <a href='https://197.242.82.242/inspectit/'>login</a> to view invoice.<br /><br />Regards<br />Inspect-It Administrator";
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