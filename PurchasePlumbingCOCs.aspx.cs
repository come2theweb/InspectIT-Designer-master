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
    public partial class PurchasePlumbingCOCs : System.Web.UI.Page
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

            // ADMIN CHECK
            //if (Session["IIT_Role"].ToString() != "Administrator")
            //{
            //    Response.Redirect("Default");
            //}

            //if (Request.QueryString["msg"] != null)
            //{
            //    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
            //    successmsg.InnerHtml = msg;
            //    successmsg.Visible = true;
            //}

            if (Request.QueryString["err"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
                Div1.InnerHtml = msg;
                Div1.Visible = true;
            }
            if (!IsPostBack)
            {
               
            }


            dispErr.Visible = false;
            if (Session["IIT_isSuspended"].ToString() == "True" || Session["IIT_isSuspended"].ToString() == "Expired")
            {
                errormsg.InnerHtml = "You can't purchase a COC because you're either suspended or your registration has expired";
                errormsg.Visible = true;
                btn_buy.Visible = false;
            }
            else
            {
                // GET AVALIABLE
                int NoBought = 0;
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select count(*) as Total from [dbo].[COCStatements] where UserID = @UserID and DateLogged is null";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        NoBought = Convert.ToInt32(theSqlDataReader["Total"]);
                        NonLoggedCOCsPurchased.Text = NoBought.ToString();
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();


                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID = @UserID and NoCOCpurchases <> '0'";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
                        NumberOfPermittedCOCs.Text = theSqlDataReader["NoCOCpurchases"].ToString();
                        COCsAbleToPurchase.Text = (Convert.ToInt32(theSqlDataReader["NoCOCpurchases"]) - NoBought).ToString();
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
            if (NoCOCsPurchase.Text != "")
            {
                // check number avaliable and current
                int NoAv = Convert.ToInt32(COCsAbleToPurchase.Text);
                int CurNo = Convert.ToInt32(NoCOCsPurchase.Text);

                if (CurNo > NoAv)
                {
                    errormsg.InnerHtml = "Can't purchase more than 'Number of Permitted COC's that you are able to purchase'";
                    errormsg.Visible = true;
                }
                else
                {
                    // check disclaimer
                    if (DisclaimerAgreementPurchaseCOC.Checked == false)
                    {
                        errormsg.InnerHtml = "You need to accept the disclaimer to continue.";
                        errormsg.Visible = true;
                    }
                    else
                    {
                        if (signatureImg == false && ElectronicCOC.Checked == true)
                        {
                            errormsg.InnerHtml = "You may not purchase an electronic COC as your signature is not on the system.";
                            errormsg.Visible = true;
                        }
                        else
                        {
                            errormsg.Visible = false;
                            string OrderID = "";
                            string Type = "Paper";
                            string Method = "Collect at PIRB Offices";
                            if (ElectronicCOC.Checked == true)
                            {
                                Type = "Electronic";
                                Method = "";
                            }

                            if (RegisteredPostPurchaseCOC.Checked == true)
                            {
                                Method = "Registered Post";
                            }
                            if (CourierPurchaseCOC.Checked == true)
                            {
                                Method = "Courier";
                            }


                            // CREATE ORDER
                            decimal StartRange = 0;
                            DLdb.RS.Open();
                            DLdb.SQLST.CommandText = "Select top 1 (COCStatementID + 1) as nCOCStatementID from COCStatements order by COCStatementID desc";
                            DLdb.SQLST.CommandType = CommandType.Text;
                            DLdb.SQLST.Connection = DLdb.RS;
                            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
                            if (theSqlDataReader.HasRows)
                            {
                                while (theSqlDataReader.Read())
                                {
                                    StartRange = Convert.ToDecimal(theSqlDataReader["nCOCStatementID"].ToString());
                                }
                            }
                            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                            DLdb.RS.Close();

                            decimal eRange = StartRange + Convert.ToDecimal(NoCOCsPurchase.Text);

                            DLdb.RS.Open();
                            DLdb.SQLST.CommandText = "INSERT INTO Orders (StartRange,EndRange,UserID,Description,TotalNoItems,SubTotal,Vat,Delivery,Total,COCType,Method,isPaid) values (@StartRange,@EndRange,@UserID,@Description,@TotalNoItems,@SubTotal,@Vat,@Delivery,@Total,@COCType,@Method,@isPaid); Select Scope_Identity() as OrderID";
                            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                            DLdb.SQLST.Parameters.AddWithValue("Description", "COC Purchase");
                            DLdb.SQLST.Parameters.AddWithValue("TotalNoItems", NoCOCsPurchase.Text.ToString());
                            DLdb.SQLST.Parameters.AddWithValue("SubTotal", CertificateCosts.Text.ToString());
                            DLdb.SQLST.Parameters.AddWithValue("Vat", VATCOCPurchases.Text.ToString());
                            DLdb.SQLST.Parameters.AddWithValue("Delivery", DeliveryCostCOCPurchase.Text.ToString());
                            DLdb.SQLST.Parameters.AddWithValue("Total", totalAmountdue.Text.ToString());
                            DLdb.SQLST.Parameters.AddWithValue("COCType", Type);
                            DLdb.SQLST.Parameters.AddWithValue("Method", Method);
                            DLdb.SQLST.Parameters.AddWithValue("StartRange", StartRange.ToString());
                            DLdb.SQLST.Parameters.AddWithValue("EndRange", eRange.ToString());
                            DLdb.SQLST.Parameters.AddWithValue("isPaid", "0");
                            DLdb.SQLST.CommandType = CommandType.Text;
                            DLdb.SQLST.Connection = DLdb.RS;
                            theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.RS.Close();

                            
                            //// LOOP THROUGHT NUMBER AND CREATE STATEMENTS
                            //int cnt = 1;
                            //int pamount = Convert.ToInt32(NoCOCsPurchase.Text);
                            //while (cnt <= pamount)
                            //{
                            //    // add the cocstatements to the user
                            //    DLdb.RS.Open();
                            //    DLdb.SQLST.CommandText = "insert into COCStatements (OrderID,UserID,Type,DatePurchased,Status, isPaid,COCNumber) values (@OrderID,@UserID,@Type,getdate(),'Non-Logged', '1',@COCNumber)";
                            //    DLdb.SQLST.Parameters.AddWithValue("OrderID", OrderID);
                            //    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                            //    DLdb.SQLST.Parameters.AddWithValue("Type", Type);
                            //    DLdb.SQLST.Parameters.AddWithValue("COCNumber", cnt.ToString());
                            //    DLdb.SQLST.CommandType = CommandType.Text;
                            //    DLdb.SQLST.Connection = DLdb.RS;
                            //    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                            //    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                            //    DLdb.SQLST.Parameters.RemoveAt(0);
                            //    DLdb.SQLST.Parameters.RemoveAt(0);
                            //    DLdb.SQLST.Parameters.RemoveAt(0);
                            //    DLdb.SQLST.Parameters.RemoveAt(0);
                            //    DLdb.RS.Close();

                            //    DLdb.RS2.Open();
                            //    DLdb.SQLST2.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                            //    DLdb.SQLST2.Parameters.AddWithValue("Message", "Paper Certificate Generated");
                            //    DLdb.SQLST2.Parameters.AddWithValue("Username", "0");
                            //    DLdb.SQLST2.Parameters.AddWithValue("TrackingTypeID", "0");
                            //    DLdb.SQLST2.Parameters.AddWithValue("CertificateID", cnt.ToString());
                            //    DLdb.SQLST2.CommandType = CommandType.Text;
                            //    DLdb.SQLST2.Connection = DLdb.RS2;
                            //    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            //    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            //    DLdb.SQLST2.Parameters.RemoveAt(0);
                            //    DLdb.SQLST2.Parameters.RemoveAt(0);
                            //    DLdb.SQLST2.Parameters.RemoveAt(0);
                            //    DLdb.SQLST2.Parameters.RemoveAt(0);
                            //    DLdb.RS2.Close();

                            //    cnt++;
                            //}

                            //// remove from users table avaliable
                            ////int NoLeft = (NoAv - CurNo); 
                            ////DLdb.RS.Open();
                            ////DLdb.SQLST.CommandText = "update users set NoCOCPurchases = @NoCOCPurchases where UserID = @UserID";
                            ////DLdb.SQLST.Parameters.AddWithValue("NoCOCPurchases", NoLeft);
                            ////DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                            ////DLdb.SQLST.CommandType = CommandType.Text;
                            ////DLdb.SQLST.Connection = DLdb.RS;
                            ////theSqlDataReader = DLdb.SQLST.ExecuteReader();

                            ////if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                            ////DLdb.SQLST.Parameters.RemoveAt(0);
                            ////DLdb.SQLST.Parameters.RemoveAt(0);
                            ////DLdb.RS.Close();

                            //// ####################################################################################################################################################
                            //// CREATE PDF AND SAVE PDF NAME TO THE ORDERS TABLE

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

                            if (Type=="Paper")
                            {
                                string OTPCode = DLdb.CreateNumber(5);
                                Session["IIT_OTPCodePlumber"] = OTPCode;
                                // Response.Redirect("ViewCOCStatement?msg=" + DLdb.Encrypt("Your purchase was complete, thank you."));
                                DLdb.sendSMS(Session["IIT_UID"].ToString(), NumberTo.ToString(), "Inspect-It: You would like to purchase " + NoCOCsPurchase.Text.ToString() + " " + Type + " C.O.C for " + Method + ". Amount: R" + totalAmountdue.Text.ToString() + ". OTP Code: " + OTPCode);

                            }
                            else
                            {
                                string OTPCode = DLdb.CreateNumber(5);
                                Session["IIT_OTPCodePlumber"] = OTPCode;
                                // Response.Redirect("ViewCOCStatement?msg=" + DLdb.Encrypt("Your purchase was complete, thank you."));
                                DLdb.sendSMS(Session["IIT_UID"].ToString(), NumberTo.ToString(), "Inspect-It: You would like to purchase " + NoCOCsPurchase.Text.ToString() + " " + Type + " C.O.C. Amount: R" + totalAmountdue.Text.ToString() + ". OTP Code: " + OTPCode);

                            }
                            Response.Redirect("VerifyPurchasePlumber?oid=" + DLdb.Encrypt(OrderID) + "&cost=" + totalAmountdue.Text.ToString());
                        }
                        
                    }
                }
            }
            else
            {
                Response.Redirect("PurchasePlumbingCOCs?err=" + DLdb.Decrypt("Please enter an amount into 'Number of COC's You wish to Purchase"));
                //errormsg.InnerHtml = "Please enter an amount into 'Number of COC's You wish to Purchase'";
                //errormsg.Visible = true;
            }
            
        }


        protected void Group1_CheckedChanged(Object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            decimal COCElectronic = 0;
            decimal COCPaper = 0;
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
                    if (theSqlDataReader["ID"].ToString() == "40")
                    {
                        COCElectronic = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                    else if (theSqlDataReader["ID"].ToString() == "36")
                    {
                        COCPaper = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                    else if (theSqlDataReader["ID"].ToString() == "24")
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

                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            int NoCocsPurchase = 0;
            if (NoCOCsPurchase.Text.ToString() == "")
            {
                NoCocsPurchase = 0;
            }
            else
            {
                NoCocsPurchase = Convert.ToInt32(NoCOCsPurchase.Text.ToString());
            }

            
            int COCsAbleToPurchases = Convert.ToInt32(COCsAbleToPurchase.Text.ToString());

            if (NoCocsPurchase > COCsAbleToPurchases)
            {
                dispErr.InnerHtml = "Can't purchase more than 'Number of COC's I am able to Purchase";
                dispErr.Visible = true;
            }

            decimal totalCost = 0;
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

            if (ElectronicCOC.Checked == true)
            {
                totalCost = NoCocsPurchase * COCElectronic;
                CertificateCost.Text = totalCost.ToString("0.##");
                CertificateCosts.Text = totalCost.ToString("0.##");

                decimal vatElec = totalCost * vat;
                VATCOCPurchase.Text = vatElec.ToString("0.##");
                VATCOCPurchases.Text = vatElec.ToString("0.##");

                decimal totalAmount = totalCost + vatElec;
                TotalDueCOCPurchase.Text = totalAmount.ToString("0.##");
                totalAmountdue.Text = totalAmount.ToString("0.##");
                //paperdel.Visible = false;
            }
            else
            {
                paperdel.Attributes.Remove("class");
                totalCost = NoCocsPurchase * COCPaper;
                CertificateCost.Text = totalCost.ToString("0.##");
                CertificateCosts.Text = totalCost.ToString("0.##");

                decimal vatPaper = totalCost * vat;
                VATCOCPurchase.Text = vatPaper.ToString("0.##");
                VATCOCPurchases.Text = vatPaper.ToString("0.##");

                decimal totalAmount = totalCost + vatPaper;


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
                TotalDueCOCPurchase.Text = totalAmount.ToString("0.##");
                totalAmountdue.Text = totalAmount.ToString("0.##");
            }

            //Response.Write(totalCost);
            //Response.End();

            DLdb.DB_Close();
        }

        protected void NoCOCsPurchase_TextChanged(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (Convert.ToInt32(NoCOCsPurchase.Text.ToString()) < 1)
            {
                NoCOCsPurchase.Text = "1";
            }
            else
            {
                decimal COCElectronic = 0;
                decimal COCPaper = 0;
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
                        if (theSqlDataReader["ID"].ToString() == "40")
                        {
                            COCElectronic = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                        }
                        else if (theSqlDataReader["ID"].ToString() == "36")
                        {
                            COCPaper = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                        }
                        else if (theSqlDataReader["ID"].ToString() == "24")
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

                    }
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                int NoCocsPurchase = Convert.ToInt32(NoCOCsPurchase.Text.ToString());
                int COCsAbleToPurchases = Convert.ToInt32(COCsAbleToPurchase.Text.ToString());

                if (NoCocsPurchase > COCsAbleToPurchases)
                {
                    dispErr.InnerHtml = "Can't purchase more than 'Number of COC's I am able to Purchase";
                    dispErr.Visible = true;
                }

                decimal totalCost = 0;
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

                if (ElectronicCOC.Checked == true)
                {
                    totalCost = NoCocsPurchase * COCElectronic;
                    CertificateCost.Text = totalCost.ToString("0.##");
                    CertificateCosts.Text = totalCost.ToString("0.##");

                    decimal vatElec = totalCost * vat;
                    VATCOCPurchase.Text = vatElec.ToString("0.##");
                    VATCOCPurchases.Text = vatElec.ToString("0.##");

                    decimal totalAmount = totalCost + vatElec;
                    TotalDueCOCPurchase.Text = totalAmount.ToString("0.##");
                    totalAmountdue.Text = totalAmount.ToString("0.##");
                    //paperdel.Visible = false;
                }
                else
                {
                    paperdel.Attributes.Remove("class");
                    totalCost = NoCocsPurchase * COCPaper;
                    CertificateCost.Text = totalCost.ToString("0.##");
                    CertificateCosts.Text = totalCost.ToString("0.##");

                    decimal vatPaper = totalCost * vat;
                    VATCOCPurchase.Text = vatPaper.ToString("0.##");
                    VATCOCPurchases.Text = vatPaper.ToString("0.##");

                    decimal totalAmount = totalCost + vatPaper;


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
                    TotalDueCOCPurchase.Text = totalAmount.ToString("0.##");
                    totalAmountdue.Text = totalAmount.ToString("0.##");
                }

                //Response.Write(totalCost);
                //Response.End();

                DLdb.DB_Close();
            }
        }
    }
}