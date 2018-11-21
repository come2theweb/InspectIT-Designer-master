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
    public partial class PF_notifyURLOtherTest : System.Web.UI.Page
    {
        string orderId = "";
        string processorOrderId = "";
        string strPostedVariables = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // Determine from payment status if we are supposed to credit or not
            string paymentStatus = "COMPLETE";
            //string paymentStatus = "COMPLETE";

            Global DLdb = new Global();
            DLdb.DB_Connect();
            try
            {
                string uid = "";
                int numcoc_purchase = 0;
                //string amount = "";
                string orderId = Request.QueryString["id"].ToString();
                //string orderId = "7";
                string name = "";
                string description = "";

                string userName = "";
                string UserSurname = "";
                string htmlContent = "";
                string userEmailAddress = "";
                string EmailText = "";

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
                    //decimal CocCost = 0;
                    string Plumbname = "";

                    DateTime now = DateTime.Now;
                    string Today = now.ToString("dd/MM/yyyy");
                    decimal vats = 0;
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from settings where ID='1'";
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        vats = Convert.ToDecimal(theSqlDataReader2["VatPercentage"].ToString());
                    }
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.RS2.Close();
                    decimal vat = Convert.ToDecimal(vats);

                    string plumberName = "";
                    string PlumberAddress = "";
                    string plumberEmail = "";
                    string plumberContact = "";
                    string registrationItems = "";
                    decimal lateRenewal = 0;
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
                    decimal AssessmentFee = 0;
                    decimal totalRegisFees = 0;
                    string newID = "";
                    string delivery = "";

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from Rates";
                    DLdb.SQLST.Parameters.AddWithValue("PlumberID", UserID.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            if (theSqlDataReader["ID"].ToString() == "62")
                            {
                                lateRenewal = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "63")
                            {
                                AssessmentFee = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "61")
                            {
                                Learner = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "60")
                            {
                                WaterEnergyEfficiency = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "59")
                            {
                                HoTWater = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "58")
                            {
                                ColdWater = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "57")
                            {
                                Drainage = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "56")
                            {
                                TechOperPractitioner = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "55")
                            {
                                Gas = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "54")
                            {
                                HeatPump = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "53")
                            {
                                Solar = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "52")
                            {
                                Licensed = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "49")
                            {
                                TrainingAssessor = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "47")
                            {
                                Master = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "37")
                            {
                                Card = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            else if (theSqlDataReader["ID"].ToString() == "46")
                            {
                                Director = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                            }
                            // veronike below doesn't have field in rates table
                            TechOperAssistant = 0;
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from orders where orderid = @orderid and isactive = '1'";
                    DLdb.SQLST.Parameters.AddWithValue("orderid", orderId);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
                        delivery = theSqlDataReader["Delivery"].ToString();
                        registrationItems += "                               <tr>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>"+ Method + "</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>1</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + delivery + "</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + delivery + "</th>" +
                                                        "                                </tr>";
                        if (theSqlDataReader["Description"].ToString() == "Order A New Card")
                        {
                            EmailText = "You have placed an order to recieve a new card. Please see attached your invoice";

                            totalRegisFees += totalRegisFees + Card;
                            registrationItems += "                               <tr>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>Order A New Card</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>1</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + Card + "</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + Card + "</th>" +
                                                        "                                </tr>";
                        }
                        else if (theSqlDataReader["Description"].ToString() == "Registration Payment")
                        {
                            EmailText = "You have paid for your renewal of your registration. Please see attached your invoice";
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "Select * from PlumberDesignations where PlumberID = @PlumberID";
                            DLdb.SQLST2.Parameters.AddWithValue("PlumberID", UserID.ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                while (theSqlDataReader2.Read())
                                {
                                    string amount = "";
                                    if (theSqlDataReader2["Designation"].ToString() == "Director Plumber")
                                    {
                                        amount = Director.ToString();
                                        totalRegisFees += totalRegisFees + Director;
                                    }
                                    else if (theSqlDataReader2["Designation"].ToString() == "Master Plumber")
                                    {
                                        amount = Master.ToString();
                                        totalRegisFees += totalRegisFees + Master;
                                    }
                                    else if (theSqlDataReader2["Designation"].ToString() == "Licensed Plumber")
                                    {
                                        amount = Licensed.ToString();
                                        totalRegisFees += totalRegisFees + Licensed;
                                    }
                                    else if (theSqlDataReader2["Designation"].ToString() == "Technical Operator Practitioner")
                                    {
                                        amount = TechOperPractitioner.ToString();
                                        totalRegisFees += totalRegisFees + TechOperPractitioner;
                                    }
                                    else if (theSqlDataReader2["Designation"].ToString() == "Technical Assistant Practitioner")
                                    {
                                        amount = TechOperAssistant.ToString();
                                        totalRegisFees += totalRegisFees + TechOperAssistant;
                                    }
                                    else if (theSqlDataReader2["Designation"].ToString() == "Learner Plumber")
                                    {
                                        amount = Learner.ToString();
                                        totalRegisFees += totalRegisFees + Learner;
                                    }

                                    registrationItems += "                               <tr>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + theSqlDataReader2["Designation"].ToString() + "</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>1</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + amount + "</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + amount + "</th>" +
                                                        "                                </tr>";
                                }
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "select * from PlumberSpecialisations where PlumberID = @PlumberID";
                            DLdb.SQLST2.Parameters.AddWithValue("PlumberID", UserID.ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                while (theSqlDataReader2.Read())
                                {
                                    string amount = "";
                                    if (theSqlDataReader2["Specialisation"].ToString() == "Training Assesor")
                                    {
                                        amount = TrainingAssessor.ToString();
                                        totalRegisFees += totalRegisFees + TrainingAssessor;
                                    }
                                    else if (theSqlDataReader2["Specialisation"].ToString() == "Estimator")
                                    {
                                        amount = Estimator.ToString();
                                        totalRegisFees += totalRegisFees + Estimator;
                                    }
                                    else if (theSqlDataReader2["Specialisation"].ToString() == "Arbitrator")
                                    {
                                        amount = Arbitrator.ToString();
                                        totalRegisFees += totalRegisFees + Arbitrator;
                                    }
                                    else if (theSqlDataReader2["Specialisation"].ToString() == "Solar")
                                    {
                                        amount = Solar.ToString();
                                        totalRegisFees += totalRegisFees + Solar;
                                    }
                                    else if (theSqlDataReader2["Specialisation"].ToString() == "Heat Pump")
                                    {
                                        amount = HeatPump.ToString();
                                        totalRegisFees += totalRegisFees + HeatPump;
                                    }
                                    else if (theSqlDataReader2["Specialisation"].ToString() == "Gas")
                                    {
                                        amount = Gas.ToString();
                                        totalRegisFees += totalRegisFees + Gas;
                                    }
                                    else if (theSqlDataReader2["Specialisation"].ToString() == "Drainage")
                                    {
                                        amount = Drainage.ToString();
                                        totalRegisFees += totalRegisFees + Drainage;
                                    }
                                    else if (theSqlDataReader2["Specialisation"].ToString() == "Cold Water")
                                    {
                                        amount = ColdWater.ToString();
                                        totalRegisFees += totalRegisFees + ColdWater;
                                    }
                                    else if (theSqlDataReader2["Specialisation"].ToString() == "Hot Water")
                                    {
                                        amount = HoTWater.ToString();
                                        totalRegisFees += totalRegisFees + HoTWater;
                                    }
                                    else if (theSqlDataReader2["Specialisation"].ToString() == "Water Energy Efficiency")
                                    {
                                        amount = WaterEnergyEfficiency.ToString();
                                        totalRegisFees += totalRegisFees + WaterEnergyEfficiency;
                                    }
                                    registrationItems += "                               <tr>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + theSqlDataReader2["Specialisation"].ToString() + "</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>1</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + amount + "</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + amount + "</th>" +
                                                        "                                </tr>";
                                }
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            if (theSqlDataReader["WantCard"].ToString() == "True")
                            {
                                totalRegisFees += totalRegisFees + Card;
                                registrationItems += "                               <tr>" +
                                                            "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>Order A New Card</th>" +
                                                            "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>1</th>" +
                                                            "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + Card + "</th>" +
                                                            "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + Card + "</th>" +
                                                            "                                </tr>";
                            }
                            

                        }
                        else
                        {
                            EmailText = "You have paid your assessment fee. Please see attached your invoice";
                            totalRegisFees += totalRegisFees + AssessmentFee;
                            registrationItems += "                               <tr>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>Assessment Fee</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>1</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + AssessmentFee + "</th>" +
                                                        "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + AssessmentFee + "</th>" +
                                                        "                                </tr>";
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Update Orders set isPaid = '1' where orderid = @orderid and isactive = '1'";
                    DLdb.SQLST.Parameters.AddWithValue("orderid", orderId);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
                        plumberName = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                        plumberEmail = theSqlDataReader["email"].ToString();
                        plumberContact = theSqlDataReader["contact"].ToString();
                        UserID = theSqlDataReader["UserID"].ToString();
                        PlumberAddress = theSqlDataReader["ResidentialStreet"].ToString() + "<br/> " + theSqlDataReader["ResidentialSuburb"].ToString() + "<br/> " + theSqlDataReader["ResidentialCity"].ToString() + "<br/> " + theSqlDataReader["Province"].ToString();
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    decimal vatElec = totalRegisFees * vat;
                    decimal totalCost = vatElec + totalRegisFees;

                    htmlContent = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
                                                    "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                    "        <tr>" +
                                                    "            <td>" +
                                                    "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                    "                    <tr><td>Plumbing Industry Registration Board<br/> P.O Box 680 <br/> Wired Park <br/> Centurion, 0149</td>" +
                                                    "                        <td align='center'><b>Pro Forma Invoice</b> <br/> " + newID + "</td>" +
                                                    "                    <td><img src='http://197.242.82.242/inspectit/assets/img/cardlogo.jpg' style='height:110px;'/></td></tr>" +
                                                    "                    <tr>" +
                                                    "                        <td align='left'><div style='width:100%;padding:10px;padding-top:10px'><br />" +
                                                     "                           <table border='1' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                    "                               <tr><th>Name / Address</th></tr>" +
                                                    "                               <tr><td>" + plumberName + "<br/> " + PlumberAddress + "</td></tr>" +
                                                    "                           </table>" +
                                                    "                           </div></td>" +
                                                    "                        <td align='right' colspan='2'><div style='width:100%;padding:10px;padding-top:10px'><br />" +
                                                    "                           <table border='1' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                    "                               <tr><th>Cust VAT Reg</th><th>Company VAT Reg</th><th>Inv Date</th></tr>" +
                                                    "                               <tr><td></td><td>4230255327</td><td>" + Today + "</td></tr>" +
                                                    "                           </table>" +
                                                    "                       </div></td>" +
                                                    "                    </tr>" +
                                                    "                    <tr>" +
                                                    "                        <td align='left'><div style='width:100%;padding:10px;'><br />" +
                                                     "                           <table border='1' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                    "                               <tr><th>Terms</th><th>Due Date</th></tr>" +
                                                    "                               <tr><td>COD</td><td>" + Today + "</td></tr>" +
                                                    "                           </table>" +
                                                    "                           </div></td><td></td><td></td>" +
                                                    "                    </tr>" +
                                                    "                    <tr>" +
                                                    "                        <td align='left' colspan='3' height='800px' valign='top'>" +
                                                    "                            <table border='0' cellpadding='5px' cellspacing='0' width='100%'>" +
                                                    "                               <tr>" +
                                                    "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>Description</th>" +
                                                    "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>Qty</th>" +
                                                    "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>Rate</th>" +
                                                    "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>Amount</th>" +
                                                    "                                </tr>" + registrationItems +
                                                    "                               <tr>" +
                                                    "                                   <td style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
                                                    "                                   <td style=\"border: 1px solid #E5E5E5;\" colspan='3' valign='middle'>" +
                                                    "                                       <table border='1' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                    "                                               <tr><td>Subtotal</td><td>R " + subTotal+ "</td></tr>" +
                                                    "                                               <tr><td>VAT Total</td><td>" + VATCost + "</td></tr>" +
                                                    "                                               <tr><td>Total</td><td>" + totalAmountdue + "</td></tr>" +
                                                    "                                       </table>" +
                                                    "                                   </td>" +
                                                    "                                </tr>" +
                                                    "                               <tr>" +
                                                    "                                   <td style=\"border: 1px solid #E5E5E5;background-color:green;\" valign='middle'>Bank Details:<br/>Bank - First National Bank <br/> Account Number: 62244182987 <br/> Branch: Eldoraigne <br/> Branch Code: 251145 <br/> Please use Inv number as Reference or Nam &amp; Surname</td>" +
                                                    "                                   <td style=\"border: 1px solid #E5E5E5;\" colspan='3' valign='middle'>" +
                                                    "                                       <table border='1' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                    "                                               <tr><td>Phone No</td><td>Email</td></tr>" +
                                                    "                                               <tr><td>012-653-0648</td><td>Admin@pirb.co.za</td></tr>" +
                                                    "                                               <tr><td>Fax No</td><td>Web Site</td></tr>" +
                                                    "                                               <tr><td>086692723</td><td>www.pirb.co.za</td></tr>" +
                                                    "                                       </table>" +
                                                    "                                   </td>" +
                                                    "                                </tr>" +
                                                    "                            </table>" +
                                                    "                        </td>" +
                                                    "                    </tr>" +
                                                    "                </table>" +
                                                    "            </td>" +
                                                    "        </tr>" +
                                                    "    </table>" +
                                                    "</body>");
                    string UserName = "";
                    string UserEmail = "";
                    string NumberTo = "";

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


                    string filename = "invoice_" + orderId + "_" + srtMM + "-" + srtYY + ".pdf";
                    var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(htmlContent);
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

                    // DLdb.sendSMS(UserID, NumberTo.ToString(), "Inspect-It: You have purchased " + NoCOCsPurchase + " " + Type + " C.O.C for " + Method + ". Amount: R" + totalAmountdue + ".");

                    string eHTMLBody = "Dear " + UserName + "<br /><br />" + EmailText + "<br /><br />Regards<br />Inspect-It Administrator";

                    string eSubject = "Inspect-IT C.O.C Statement Purchase";
                    DLdb.sendEmail(eHTMLBody, eSubject, "mathewpayne@gmail.com", UserEmail, path);


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