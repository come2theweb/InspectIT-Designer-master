using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InspectIT
{
    public partial class zBgRegistrationDate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string filename = "";
            string Renwewal = "";
            string htmlContent = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where isactive='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["RegistrationEnd"].ToString() != "" && theSqlDataReader["RegistrationEnd"] != DBNull.Value)
                    {
                        DateTime RegisEnddate = Convert.ToDateTime(theSqlDataReader["RegistrationEnd"].ToString());
                        DateTime nowThirt = DateTime.Now.AddDays(30);
                        DateTime now = DateTime.Now;
                        DateTime OneDayLate = Convert.ToDateTime(theSqlDataReader["RegistrationEnd"]).AddDays(1);
                        DateTime FifteenDayLate = Convert.ToDateTime(theSqlDataReader["RegistrationEnd"]).AddDays(15);
                        string dateWithoutTime = RegisEnddate.ToString("dd/MM/yyyy");
                        string dateWithoutTimeDayOne = OneDayLate.ToString("dd/MM/yyyy");
                        string dateWithoutTimeDayFifteen = FifteenDayLate.ToString("dd/MM/yyyy");
                        string nowWithoutTimeThirty = nowThirt.ToString("dd/MM/yyyy");
                        string Today = now.ToString("dd/MM/yyyy");

                        string plumberName = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                        string plumberEmail = theSqlDataReader["email"].ToString();
                        string plumberContact = theSqlDataReader["contact"].ToString();
                        string UserID = theSqlDataReader["UserID"].ToString();
                        string PlumberAddress = theSqlDataReader["ResidentialStreet"].ToString() + "<br/> " + theSqlDataReader["ResidentialSuburb"].ToString() + "<br/> " + theSqlDataReader["ResidentialCity"].ToString() + "<br/> " + theSqlDataReader["Province"].ToString();

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
                        decimal totalRegisFees = 0;
                        string newID = "";

                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "select * from Rates";
                        DLdb.SQLST3.Parameters.AddWithValue("PlumberID", UserID.ToString());
                        DLdb.SQLST3.CommandType = CommandType.Text;
                        DLdb.SQLST3.Connection = DLdb.RS3;
                        SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

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
                                // veronike below doesn't have field in rates table
                                TechOperAssistant = 0;
                            }
                        }

                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.RS3.Close();

                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "insert into Documents (UserID,Description) values (@UserID,@Description); select scope_identity() as id;";
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", UserID.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("Description", "");
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                        if (theSqlDataReader2.HasRows)
                        {
                            theSqlDataReader2.Read();
                            newID = theSqlDataReader2["id"].ToString();
                        }
                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                        
                        decimal vats = 0;
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "select * from settings where ID='1'";
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                        if (theSqlDataReader2.HasRows)
                        {
                            theSqlDataReader2.Read();
                            vats = Convert.ToDecimal(theSqlDataReader2["VatPercentage"].ToString());
                        }
                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.RS2.Close();
                        decimal vat = Convert.ToDecimal(vats);

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
                                                    "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>"+ theSqlDataReader2["Designation"].ToString() + "</th>" +
                                                    "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>1</th>" +
                                                    "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>"+ amount + "</th>" +
                                                    "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>"+ amount + "</th>" +
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

                        decimal vatElec = totalRegisFees * vat;
                        decimal totalCost = vatElec + totalRegisFees;
                        if (Today == dateWithoutTimeDayOne || Today == dateWithoutTimeDayFifteen)
                        {
                            totalRegisFees += totalRegisFees + lateRenewal;
                            totalCost = vatElec + totalRegisFees + lateRenewal;
                            registrationItems += "                               <tr>" +
                                                   "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>Late Renewal Registration Fee</th>" +
                                                   "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>1</th>" +
                                                   "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + lateRenewal.ToString() + "</th>" +
                                                   "                                   <th style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + lateRenewal.ToString() + "</th>" +
                                                   "                                </tr>";
                        }

                        htmlContent = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
                                                    "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                    "        <tr>" +
                                                    "            <td>" +
                                                    "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                    "                    <tr><td>Plumbing Industry Registration Board<br/> P.O Box 680 <br/> Wired Park <br/> Centurion, 0149</td>" +
                                                    "                        <td align='center'><b>Pro Forma Invoice</b> <br/> "+newID+"</td>" +
                                                    "                    <td><img src='http://197.242.82.242/inspectit/assets/img/cardlogo.jpg' style='height:110px;'/></td></tr>" +
                                                    "                    <tr>" +
                                                    "                        <td align='left'><div style='width:100%;padding:10px;padding-top:10px'><br />"+
                                                     "                           <table border='1' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                    "                               <tr><th>Name / Address</th></tr>" +
                                                    "                               <tr><td>"+plumberName+"<br/> "+ PlumberAddress + "</td></tr>" +
                                                    "                           </table>" +
                                                    "                           </div></td>" +
                                                    "                        <td align='right' colspan='2'><div style='width:100%;padding:10px;padding-top:10px'><br />" +
                                                    "                           <table border='1' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                    "                               <tr><th>Cust VAT Reg</th><th>Company VAT Reg</th><th>Inv Date</th></tr>" +
                                                    "                               <tr><td></td><td>4230255327</td><td>"+ Today + "</td></tr>"+
                                                    "                           </table>" +
                                                    "                       </div></td>" +
                                                    "                    </tr>" +
                                                    "                    <tr>" +
                                                    "                        <td align='left'><div style='width:100%;padding:10px;'><br />" +
                                                     "                           <table border='1' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                    "                               <tr><th>Terms</th><th>Due Date</th></tr>" +
                                                    "                               <tr><td>COD</td><td>"+ Today + "</td></tr>" +
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
                                                    "                                               <tr><td>Subtotal</td><td>R "+ totalRegisFees.ToString("0.##") + "</td></tr>" +
                                                    "                                               <tr><td>VAT Total</td><td>"+ vatElec.ToString("0.##") + "</td></tr>" +
                                                    "                                               <tr><td>Total</td><td>"+ totalCost.ToString("0.##") + "</td></tr>" +
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

                      
                        if (nowWithoutTimeThirty == dateWithoutTime)
                        {
                            Renwewal = "Renewal-1";
                            string HTMLSubject = "PIRB Renewal";
                            string HTMLBody = "Good Day " + plumberName + 
                                "<br/>" +
                                "This is a reminded that your PIRB registration is coming up for renewal in the next 30 days.  Please verify that your registration details are correct by logging into your registration profile, as it is important that you this information is keep current." +
                                "<br/><br/>" +
                                "To login <a href='www.pirb.co.za'>www.pirb.co.za</a>" +
                                "<br/><br/>" +
                                "Find attached your yearly registration fee, proforma invoice.  To avoid late registration fees and/or to earn your registration CPD points we encourage you make the necessary payment by no later than your renewal date of " + dateWithoutTime+
                                "<br/><br/>" +
                                "If you require any further information, please contact the PIRB on 0861 747 275 or email <a href='mailto:info@pirb.co.za'>info@pirb.co.za</a>" +
                                "<br/><br/>" +
                                "Best Regards" +
                                "<br/><br/" +
                                "The PIRB Team" +
                                "<br/><br/" +
                                "<b>Please do not respond to this email as it will not be answered.</b>";
                            DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumberEmail.ToString(), "");

                            DLdb.sendSMS(UserID.ToString(), plumberContact.ToString(), "This is reminder that your PIRB registration is coming up for renewal in the next 30 days. To avoid disappointment please renew. Contact PIRB - 0861 747 275");

                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "update Documents set Description=@Description where DocumentID=@DocumentID ";
                            DLdb.SQLST2.Parameters.AddWithValue("DocumentAttached", filename);
                            DLdb.SQLST2.Parameters.AddWithValue("Description", "Email Sent: Renewal Email 1");
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();
                        }

                        if (Today == dateWithoutTimeDayOne)
                        {
                            Renwewal = "Renewal-2";
                            string HTMLSubject = "PIRB Renewal";
                            string HTMLBody = "Good Day " + plumberName +
                                "<br/>" +
                                "This is a reminded that your PIRB registration has lapsed. To avoid late registration fees and/or your registration status being change to expired, we encourage you make your registration payment immediately. " +
                                "<br/><br/>" +
                                "Find attached your yearly registration fee, proforma invoice. " + 
                                "<br/><br/>" +
                                "If you require any further information, please contact the PIRB on 0861 747 275 or email <a href='mailto:info@pirb.co.za'>info@pirb.co.za</a>" +
                                "<br/><br/>" +
                                "Best Regards" +
                                "<br/><br/" +
                                "The PIRB Team" +
                                "<br/><br/" +
                                "<b>Please do not respond to this email as it will not be answered.</b>";
                            DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumberEmail.ToString(), "");

                            DLdb.sendSMS(UserID.ToString(), plumberContact.ToString(), "This is reminder that your PIRB registration has lapsed. To avoid disappointment and possible expiry status, pls renew. Contact PIRB - 0861 747 275");

                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "update Documents set Description=@Description where DocumentID=@DocumentID ";
                            DLdb.SQLST2.Parameters.AddWithValue("DocumentAttached", filename);
                            DLdb.SQLST2.Parameters.AddWithValue("Description", "Email Sent: Renewal Email 2");
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "update Users set Status=@Status where UserID=@UserID ";
                            DLdb.SQLST2.Parameters.AddWithValue("Status", "Expired");
                            DLdb.SQLST2.Parameters.AddWithValue("UserID", UserID.ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();
                            
                            DLdb.RS.Open();
                            DLdb.SQLST.CommandText = "insert into ActiveRegistrationLog (UserID,Description,DateActive,DateInactive) values (@UserID,@Description,@DateActive,@DateInactive)";
                            DLdb.SQLST.Parameters.AddWithValue("UserID", UserID.ToString());
                            DLdb.SQLST.Parameters.AddWithValue("DateActive", DateTime.Now);
                            DLdb.SQLST.Parameters.AddWithValue("DateInactive", DateTime.Now);
                            DLdb.SQLST.Parameters.AddWithValue("Description", "Users registration has expired");
                            DLdb.SQLST.CommandType = CommandType.Text;
                            DLdb.SQLST.Connection = DLdb.RS;
                            theSqlDataReader = DLdb.SQLST.ExecuteReader();

                            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.RS.Close();
                        }

                        

                        if (Today == dateWithoutTimeDayFifteen)
                        {
                            Renwewal = "Renewal-3";
                            string HTMLSubject = "PIRB Renewal";
                            string HTMLBody = "Good Day " + plumberName +
                                "<br/>" +
                                "This is a reminded that your PIRB registration renewal has passed and your registration has unfortunately been change to expired status.  " +
                                "<br/><br/>" +
                                "Find attached your yearly registration fee, proforma invoice. We encourage that you make the necessary payment as soon as possible so that we are able to make your registration active again." +
                                "<br/><br/>" +
                                "If you require any further information, please contact the PIRB on 0861 747 275 or email <a href='mailto:info@pirb.co.za'>info@pirb.co.za</a>" +
                                "<br/><br/>" +
                                "Best Regards" +
                                "<br/><br/" +
                                "The PIRB Team" +
                                "<br/><br/" +
                                "<b>Please do not respond to this email as it will not be answered.</b>";
                            DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumberEmail.ToString(), "");

                            DLdb.sendSMS(UserID.ToString(), plumberContact.ToString(), "This is notice that your PIRB registration renewal has passed and your registration is unfortunately been expired.  Contact PIRB - 0861 747 275");
                            
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "update Documents set Description=@Description where DocumentID=@DocumentID ";
                            DLdb.SQLST2.Parameters.AddWithValue("DocumentAttached", filename);
                            DLdb.SQLST2.Parameters.AddWithValue("Description", "Email Sent: Renewal Email 3");
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();
                        }
                        
                        filename = "Registration_" + Renwewal + "_" + newID + ".pdf";
                        var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(htmlContent);
                        string path = Server.MapPath("~/Inspectorinvoices/") + filename;
                        File.WriteAllBytes(path, pdfBytes);

                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "update Documents set DocumentAttached=@DocumentAttached where DocumentID=@DocumentID ";
                        DLdb.SQLST2.Parameters.AddWithValue("DocumentAttached", filename);
                        DLdb.SQLST2.Parameters.AddWithValue("DocumentID", newID);
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                        
                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                    }
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();

        }
    }
}