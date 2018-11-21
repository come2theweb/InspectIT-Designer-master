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
    public partial class PayRegistration : System.Web.UI.Page
    {
        //public decimal COCElectronic = 0;
        //public decimal COCPaper = 0;
        //public decimal CourierDelivery = 0;
        //public decimal RegisteredPostDelivery = 0;
        public string totalRegFeesComp = "";
        public Boolean signatureImg = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            dispErr.Visible = false;
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
            btn_buy.Enabled = false;

            if (!IsPostBack)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from PlumberDesignations where PlumberID = @PlumberID";
                DLdb.SQLST.Parameters.AddWithValue("PlumberID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["Designation"].ToString() == "Director Plumber")
                        {
                            DirectorPlumber.Checked = true;
                        }
                        else if (theSqlDataReader["Designation"].ToString() == "Master Plumber")
                        {
                            MasterPlumber.Checked = true;
                        }
                        else if (theSqlDataReader["Designation"].ToString() == "Licensed Plumber")
                        {
                            LicensedPlumber.Checked = true;
                        }
                        else if (theSqlDataReader["Designation"].ToString() == "Technical Operator Practitioner")
                        {
                            TechnicalOperatorPractitioner.Checked = true;
                        }
                        else if (theSqlDataReader["Designation"].ToString() == "Technical Assistant Practitioner")
                        {
                            TechnicalAssistantPractitioner.Checked = true;
                        }
                        else if (theSqlDataReader["Designation"].ToString() == "Learner Plumber")
                        {
                            LearnerPlumber.Checked = true;
                        }
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from PlumberSpecialisations where PlumberID = @PlumberID";
                DLdb.SQLST.Parameters.AddWithValue("PlumberID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["Specialisation"].ToString() == "Training Assesor")
                        {
                            MasterPlumberTrainingAssesor.Checked = true;
                        }
                        else if (theSqlDataReader["Specialisation"].ToString() == "Estimator")
                        {
                            MasterPlumberEstimator.Checked = true;
                        }
                        else if (theSqlDataReader["Specialisation"].ToString() == "Arbitrator")
                        {
                            MasterPlumberArbitrator.Checked = true;
                        }
                        else if (theSqlDataReader["Specialisation"].ToString() == "Solar")
                        {
                            LicensedPlumberSolar.Checked = true;
                        }
                        else if (theSqlDataReader["Specialisation"].ToString() == "Heat Pump")
                        {
                            LicensedPlumberHeatPump.Checked = true;
                        }
                        else if (theSqlDataReader["Specialisation"].ToString() == "Gas")
                        {
                            LicensedPlumberGas.Checked = true;
                        }
                        else if (theSqlDataReader["Specialisation"].ToString() == "Drainage")
                        {
                            TechnicalOperatorPractitionerDrainage.Checked = true;
                        }
                        else if (theSqlDataReader["Specialisation"].ToString() == "Cold Water")
                        {
                            TechnicalOperatorPractitionerColdWater.Checked = true;
                        }
                        else if (theSqlDataReader["Specialisation"].ToString() == "Hot Water")
                        {
                            TechnicalOperatorPractitionerHotWater.Checked = true;
                        }
                        else if (theSqlDataReader["Specialisation"].ToString() == "Water Energy Efficiency")
                        {
                            TechnicalOperatorPractitionerWaterEnergyEfficiency.Checked = true;
                        }
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM NewApplications where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DropDownList1.SelectedValue = theSqlDataReader["RegistrationCard"].ToString();
                       // RadioGroup2.SelectedValue = theSqlDataReader["DeliveryMethod"].ToString();
                }
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
            //string useridss = Session["IIT_UID"].ToString();

            //GET THE USERS DETAILS
           

            DLdb.RS.Open();
                DLdb.SQLST.CommandText = "INSERT INTO Orders (UserID,Description,TotalNoItems,SubTotal,Vat,Delivery,Total,Method,isPaid,WantCard) values (@UserID,@Description,@TotalNoItems,@SubTotal,@Vat,@Delivery,@Total,@Method,@isPaid,@WantCard); Select Scope_Identity() as OrderID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("Description", "Registration Payment");
                DLdb.SQLST.Parameters.AddWithValue("TotalNoItems", "1");
                DLdb.SQLST.Parameters.AddWithValue("SubTotal", totalRegFeesComp.ToString()); 
            DLdb.SQLST.Parameters.AddWithValue("Vat", VATCOCPurchase.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Delivery", DeliveryCostCOCPurchase.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Total", TotalDueCOCPurchase.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Method", Method);
                DLdb.SQLST.Parameters.AddWithValue("isPaid", "0");
            DLdb.SQLST.Parameters.AddWithValue("WantCard", DropDownList1.SelectedValue);
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
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                            
                

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
            //Response.Redirect("PF_notifyURLOtherTest.aspx?id=" + orderId);


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
        decimal totalRegisFees = 0;
        decimal lateRenewal = 0;
        decimal CardCost = 0;
            decimal CourierDelivery = 0;
            decimal RegisteredPostDelivery = 0;
            decimal Collection = 0;
            decimal regisFee = 0;
            decimal lateregisFee = 0;
            string UserName = "";
            string UserEmail = "";
            string NumberTo = "";
            string pirbID = Session["IIT_UID"].ToString();
            decimal Learner = 0;
            decimal WaterEnergyEfficiency = 0;
            decimal HoTWater = 0;
            decimal ColdWater = 0;
            decimal Drainage = 0;
            decimal TechOperPractitioner = 0;
            decimal TechOperAssistant = 0;
            decimal Gas = 0;
            decimal HeatPump = 0;
            decimal Solar = 0;
            decimal Licensed = 0;
            decimal TrainingAssessor = 0;
            decimal Master = 0;
            decimal Director = 0;
            decimal Card = 0;
            decimal Estimator = 0;
            decimal Arbitrator = 0;
            DateTime now = DateTime.Now;
            DateTime RegisEnddate;

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
                NumberTo = theSqlDataReader3["contact"].ToString();
                RegisEnddate = Convert.ToDateTime(theSqlDataReader3["RegistrationEnd"].ToString());

            }
            else
            {
                RegisEnddate = Convert.ToDateTime("1900-01-01");
            }

            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            DLdb.SQLST3.Parameters.RemoveAt(0);
            DLdb.RS3.Close();

            DLdb.RS3.Open();
            DLdb.SQLST3.CommandText = "select * from Rates";
            DLdb.SQLST3.CommandType = CommandType.Text;
            DLdb.SQLST3.Connection = DLdb.RS3;
            theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            if (theSqlDataReader3.HasRows)
            {
                while (theSqlDataReader3.Read())
                {
                    if (theSqlDataReader3["ID"].ToString() == "62")
                    {
                        lateRenewal = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "61")
                    {
                        Learner = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "60")
                    {
                        WaterEnergyEfficiency = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "59")
                    {
                        HoTWater = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "58")
                    {
                        ColdWater = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "57")
                    {
                        Drainage = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "56")
                    {
                        TechOperPractitioner = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "55")
                    {
                        Gas = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "54")
                    {
                        HeatPump = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "53")
                    {
                        Solar = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "52")
                    {
                        Licensed = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "49")
                    {
                        TrainingAssessor = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "47")
                    {
                        Master = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "37")
                    {
                        Card = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "46")
                    {
                        Director = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "24")
                    {
                        CourierDelivery = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "25")
                    {
                        RegisteredPostDelivery = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    else if (theSqlDataReader3["ID"].ToString() == "26")
                    {
                        Collection = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                    }
                    // veronike below doesn't have field in rates table
                    TechOperAssistant = 0;
                   
                }
            }

            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            DLdb.RS3.Close();

            if (DropDownList1.SelectedValue == "1")
            {
                cardCost.Text = Card.ToString();
            }
            else
            {
                cardCost.Text = "0";
                Card = 0;
            }
            if (now > RegisEnddate)
            {
                totalRegisFees += totalRegisFees + lateRenewal;
            }
            else
            {
                lateRenewal = 0;
            }

            lateRegistrationFee.Text = lateRenewal.ToString();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from PlumberDesignations where PlumberID = @PlumberID";
            DLdb.SQLST.Parameters.AddWithValue("PlumberID", pirbID.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string amount = "";
                    if (theSqlDataReader["Designation"].ToString() == "Director Plumber")
                    {
                        amount = Director.ToString();
                        totalRegisFees += totalRegisFees + Director;
                        DirectorPlumber.Checked = true;
                    }
                    else if (theSqlDataReader["Designation"].ToString() == "Master Plumber")
                    {
                        amount = Master.ToString();
                        totalRegisFees += totalRegisFees + Master;
                        MasterPlumber.Checked = true;
                    }
                    else if (theSqlDataReader["Designation"].ToString() == "Licensed Plumber")
                    {
                        amount = Licensed.ToString();
                        totalRegisFees += totalRegisFees + Licensed;
                        LicensedPlumber.Checked = true;
                    }
                    else if (theSqlDataReader["Designation"].ToString() == "Technical Operator Practitioner")
                    {
                        amount = TechOperPractitioner.ToString();
                        totalRegisFees += totalRegisFees + TechOperPractitioner;
                        TechnicalOperatorPractitioner.Checked = true;
                    }
                    else if (theSqlDataReader["Designation"].ToString() == "Technical Assistant Practitioner")
                    {
                        amount = TechOperAssistant.ToString();
                        totalRegisFees += totalRegisFees + TechOperAssistant;
                        TechnicalAssistantPractitioner.Checked = true;
                    }
                    else if (theSqlDataReader["Designation"].ToString() == "Learner Plumber")
                    {
                        amount = Learner.ToString();
                        totalRegisFees += totalRegisFees + Learner;
                        LearnerPlumber.Checked = true;
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from PlumberSpecialisations where PlumberID = @PlumberID";
            DLdb.SQLST.Parameters.AddWithValue("PlumberID", pirbID.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string amount = "";
                    if (theSqlDataReader["Specialisation"].ToString() == "Training Assesor")
                    {
                        amount = TrainingAssessor.ToString();
                        totalRegisFees += totalRegisFees + TrainingAssessor;
                        MasterPlumberTrainingAssesor.Checked = true;
                    }
                    else if (theSqlDataReader["Specialisation"].ToString() == "Estimator")
                    {
                        amount = Estimator.ToString();
                        totalRegisFees += totalRegisFees + Estimator;
                        MasterPlumberEstimator.Checked = true;
                    }
                    else if (theSqlDataReader["Specialisation"].ToString() == "Arbitrator")
                    {
                        amount = Arbitrator.ToString();
                        totalRegisFees += totalRegisFees + Arbitrator;
                        MasterPlumberArbitrator.Checked = true;
                    }
                    else if (theSqlDataReader["Specialisation"].ToString() == "Solar")
                    {
                        amount = Solar.ToString();
                        totalRegisFees += totalRegisFees + Solar;
                        LicensedPlumberSolar.Checked = true;
                    }
                    else if (theSqlDataReader["Specialisation"].ToString() == "Heat Pump")
                    {
                        amount = HeatPump.ToString();
                        totalRegisFees += totalRegisFees + HeatPump;
                        LicensedPlumberHeatPump.Checked = true;
                    }
                    else if (theSqlDataReader["Specialisation"].ToString() == "Gas")
                    {
                        amount = Gas.ToString();
                        totalRegisFees += totalRegisFees + Gas;
                        LicensedPlumberGas.Checked = true;
                    }
                    else if (theSqlDataReader["Specialisation"].ToString() == "Drainage")
                    {
                        amount = Drainage.ToString();
                        totalRegisFees += totalRegisFees + Drainage;
                        TechnicalOperatorPractitionerDrainage.Checked = true;
                    }
                    else if (theSqlDataReader["Specialisation"].ToString() == "Cold Water")
                    {
                        amount = ColdWater.ToString();
                        totalRegisFees += totalRegisFees + ColdWater;
                        TechnicalOperatorPractitionerColdWater.Checked = true;
                    }
                    else if (theSqlDataReader["Specialisation"].ToString() == "Hot Water")
                    {
                        amount = HoTWater.ToString();
                        totalRegisFees += totalRegisFees + HoTWater;
                        TechnicalOperatorPractitionerHotWater.Checked = true;
                    }
                    else if (theSqlDataReader["Specialisation"].ToString() == "Water Energy Efficiency")
                    {
                        amount = WaterEnergyEfficiency.ToString();
                        totalRegisFees += totalRegisFees + WaterEnergyEfficiency;
                        TechnicalOperatorPractitionerWaterEnergyEfficiency.Checked = true;
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "select * from Rates";
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //theSqlDataReader = DLdb.SQLST.ExecuteReader();
            //if (theSqlDataReader.HasRows)
            //{
            //    while (theSqlDataReader.Read())
            //    {
            //        if (theSqlDataReader["ID"].ToString() == "24")
            //        {
            //            CourierDelivery = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
            //        }
            //        else if (theSqlDataReader["ID"].ToString() == "25")
            //        {
            //            RegisteredPostDelivery = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
            //        }
            //        else if (theSqlDataReader["ID"].ToString() == "26")
            //        {
            //            Collection = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
            //        }
            //        else if (theSqlDataReader["ID"].ToString() == "37")
            //        {
            //            CardCost = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
            //        }
            //        else if (theSqlDataReader["ID"].ToString() == "62")
            //        {
            //            lateregisFee = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
            //        }
            //        RegistrationFee.Text = totalRegisFees.ToString();
            //        if (DropDownList1.SelectedValue == "1")
            //        {
            //            cardCost.Text = CardCost.ToString();
            //        }
            //        else
            //        {
            //            cardCost.Text = "0";
            //            CardCost = 0;
            //        }

            //        lateRegistrationFee.Text = lateRenewal.ToString();
            //    }
            //}
            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.RS.Close();

            RegistrationFee.Text = totalRegisFees.ToString();
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

            decimal allCosts = Card + totalRegisFees;
            totalRegFeesComp = totalRegisFees.ToString();


            if (CourierPurchaseCOC.Checked == true)
            {
                DeliveryCostCOCPurchase.Text = CourierDelivery.ToString();
                allCosts = allCosts + CourierDelivery;
            }
            else if (RegisteredPostPurchaseCOC.Checked == true)
            {
                DeliveryCostCOCPurchase.Text = RegisteredPostDelivery.ToString();
                allCosts = allCosts + RegisteredPostDelivery;
            }
            else if (CollectPurchaseCOC.Checked == true)
            {
                DeliveryCostCOCPurchase.Text = Collection.ToString();
                allCosts = allCosts + Collection;
            }
            TotalDueCOCPurchase.Text = allCosts.ToString(a);

            decimal vatC = allCosts * vat;
            VATCOCPurchase.Text = vatC.ToString();
            decimal totalAmount = allCosts + vatC;
            //Response.Write(totalCost);
            //Response.End();

            DLdb.DB_Close();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Group1_CheckedChanged(sender,e);
        }
    }
}