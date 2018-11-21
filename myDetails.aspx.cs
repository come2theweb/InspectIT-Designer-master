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
    public partial class myDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }
            
           
            string pirbID = "";
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

            if (!IsPostBack)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from PlumberDesignations where isActive='1' and plumberid=@plumberid";
                DLdb.SQLST.Parameters.AddWithValue("plumberid", Session["IIT_UID"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["Designation"].ToString() != "Licensed Plumber" && theSqlDataReader["Designation"].ToString() != "Director Plumber" & theSqlDataReader["Designation"].ToString() != "Master Plumber")
                        {
                            hideCoCOverview.Visible = false;
                        }
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                CompanyID.Items.Clear();
                CompanyID.Items.Add(new ListItem("", ""));

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from Companies where isActive = '1' order by CompanyName";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
               theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        CompanyID.Items.Add(new ListItem(theSqlDataReader["CompanyName"].ToString(), theSqlDataReader["CompanyID"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                string pirbid = "";

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    Image2.ImageUrl = "Photos/" + theSqlDataReader["Photo"].ToString();
                    statusTxt.Text = theSqlDataReader["status"].ToString();
                    regnoplumber.Text = theSqlDataReader["regno"].ToString();
                    Name.Text = theSqlDataReader["fname"].ToString();
                    Surname.Text = theSqlDataReader["lname"].ToString();
                    pirbID = theSqlDataReader["PIRBID"].ToString();
                    EmploymentStatus.SelectedValue = theSqlDataReader["SocioeconomicStatus"].ToString();
                    CompanyID.SelectedValue = theSqlDataReader["Company"].ToString();
                    regno.Text = theSqlDataReader["regno"].ToString();
                    TextBox2.Text = theSqlDataReader["fname"].ToString();
                    TextBox3.Text = theSqlDataReader["lname"].ToString();
                    RegistrationStart.Text = theSqlDataReader["RegistrationStart"].ToString();
                    if (theSqlDataReader["RegistrationStart"].ToString() != "" && theSqlDataReader["RegistrationStart"] != DBNull.Value)
                    {
                        DateTime renewalDate = Convert.ToDateTime(theSqlDataReader["RegistrationStart"].ToString());
                        renewalDateTxt.Text = renewalDate.ToString("dd MMM");
                    }
                    if (theSqlDataReader["RegistrationEnd"].ToString() != "" && theSqlDataReader["RegistrationEnd"] != DBNull.Value)
                    {
                        DateTime renewalDateYear = Convert.ToDateTime(theSqlDataReader["RegistrationEnd"].ToString());
                        Year.Text = renewalDateYear.ToString("yyyy");
                    }
                    IDNum.Text = theSqlDataReader["IDNo"].ToString();
                    notice.Text = theSqlDataReader["notice"].ToString();
                    Password.Text = DLdb.Decrypt(theSqlDataReader["Password"].ToString());
                    postalAddressPlumber.Text = theSqlDataReader["postalAddress"].ToString();
                    plumberemail.Text = theSqlDataReader["email"].ToString();
                    pirbid = theSqlDataReader["PIRBID"].ToString();
                    plumberSignature.ImageUrl = "http://197.242.82.242/pirbreg/signatures/" + theSqlDataReader["Signature"].ToString();
                    nonloggedcocallocated.Text = theSqlDataReader["NoCOCPurchases"].ToString();
                    
                    title.Text = theSqlDataReader["Title"].ToString();
                    AlternateID.Text = theSqlDataReader["Alternate"].ToString();
                    Gender.SelectedValue = theSqlDataReader["Gender"].ToString();
                    RacialStatus.SelectedValue = theSqlDataReader["Equity"].ToString();
                    ResidentStatus.Text = theSqlDataReader["CitizenResidentStatus"].ToString();
                    if (theSqlDataReader["CitizenResidentStatus"].ToString() == "South Africa")
                    {
                        DropDownList3.SelectedValue = "1";
                    }
                    Image1.ImageUrl = "Photos/" + theSqlDataReader["IDPhoto"].ToString();
                    dob.Text = theSqlDataReader["DateofBirth"].ToString();
                    HomeLanguage.SelectedValue = theSqlDataReader["Language"].ToString();
                    Nationality.SelectedValue = theSqlDataReader["Nationality"].ToString();
                    Disability.SelectedValue = theSqlDataReader["DisabilityStatus"].ToString();
                    //DropDownList4.SelectedValue = theSqlDataReader["PostalProvince"].ToString();
                    //DropDownList5.SelectedValue = theSqlDataReader["Province"].ToString();
                    adminPysProv.Text = theSqlDataReader["Province"].ToString();
                    adminPostProv.Text = theSqlDataReader["PostalProvince"].ToString();
                    adminpostalCities.Text = theSqlDataReader["PostalCity"].ToString();
                    adminpostalSuburb.Text = theSqlDataReader["PostalSuburb"].ToString();
                    PostalCode.Text = theSqlDataReader["PostalCode"].ToString();
                    PhysicalAddress.Text = theSqlDataReader["ResidentialStreet"].ToString();
                    adminphysicalSuburb.Text = theSqlDataReader["ResidentialSuburb"].ToString();
                    adminphysicalCities.Text = theSqlDataReader["ResidentialCity"].ToString();
                    SecondEmail.Text = theSqlDataReader["SecondEmail"].ToString();
                    homePhone.Text = theSqlDataReader["HomePhone"].ToString();
                    Mobile.Text = theSqlDataReader["contact"].ToString();
                    workphone.Text = theSqlDataReader["BusinessPhone"].ToString();
                    
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Companies where CompanyID=@CompanyID";
                    DLdb.SQLST2.Parameters.AddWithValue("CompanyID", theSqlDataReader["company"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        CompanyVAt.Text = theSqlDataReader2["VatNo"].ToString();
                        empEmailaddress.Text = theSqlDataReader2["CompanyEmail"].ToString();
                        empWorkPhone.Text = theSqlDataReader2["CompanyContactNo"].ToString();
                        empAddress.Text = theSqlDataReader2["AddressLine1"].ToString();
                        empCity.Text = theSqlDataReader2["City"].ToString();
                        empProvince.Text = theSqlDataReader2["Province"].ToString();
                        empSuburb.Text = theSqlDataReader2["Suburb"].ToString();
                        empPostalAddress.Text = theSqlDataReader2["PostalAddress"].ToString();
                        empPostalCity.Text = theSqlDataReader2["PostalCity"].ToString();
                        empPostalCode.Text = theSqlDataReader2["PostalCode"].ToString();
                        primaryContact.Text = theSqlDataReader2["PrimaryContact"].ToString();
                        empMobile.Text = theSqlDataReader2["Mobile"].ToString();
                        empPostalProvince.Text = theSqlDataReader2["PostalProvince"].ToString();
                        empPostalSuburb.Text = theSqlDataReader2["PostalSuburb"].ToString();
                    }
                    

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from PlumberDesignations where PlumberID = @PlumberID";
                DLdb.SQLST.Parameters.AddWithValue("PlumberID", pirbID);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        // GET THE COLOR AND ICON
                        string CardIcon = "https://197.242.82.242/inspectit/assets/icons/" + theSqlDataReader["designation"].ToString() + " Icon.png";
                        string CardColor = "";

                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "select * from LUDesignations where Designation = @Designation";
                        DLdb.SQLST2.Parameters.AddWithValue("Designation", theSqlDataReader["designation"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                CardColor = theSqlDataReader2["CardColor"].ToString();
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        // GET USER DETAILS TO POPULATE
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "select * from Users where PIRBID = @PIRBID";
                        DLdb.SQLST2.Parameters.AddWithValue("PIRBID", pirbID);
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                string CompanyName = "";
                                string BusinessPhone = "";
                                string BusinessEmail = "";
                                string PlumberPhoto = "Photos/" + theSqlDataReader2["Photo"].ToString();

                                DLdb.RS3.Open();
                                DLdb.SQLST3.CommandText = "Select * from Companies where CompanyID = @CompanyID";
                                DLdb.SQLST3.Parameters.AddWithValue("CompanyID", theSqlDataReader2["Company"].ToString());
                                DLdb.SQLST3.CommandType = CommandType.Text;
                                DLdb.SQLST3.Connection = DLdb.RS3;
                                SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                                if (theSqlDataReader3.HasRows)
                                {
                                    while (theSqlDataReader3.Read())
                                    {
                                        CompanyName = theSqlDataReader3["CompanyName"].ToString();
                                        BusinessPhone = theSqlDataReader3["CompanyContactNo"].ToString();
                                        BusinessEmail = theSqlDataReader3["CompanyEmail"].ToString();
                                    }
                                }

                                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                                DLdb.SQLST3.Parameters.RemoveAt(0);
                                DLdb.RS3.Close();

                                // GET Specialisations
                                string specifications = "";
                                DLdb.RS3.Open();
                                DLdb.SQLST3.CommandText = "Select * from PlumberSpecialisations where PlumberID = @PlumberID and isActive='1'";
                                DLdb.SQLST3.Parameters.AddWithValue("PlumberID", theSqlDataReader2["PIRBID"].ToString());
                                DLdb.SQLST3.CommandType = CommandType.Text;
                                DLdb.SQLST3.Connection = DLdb.RS3;
                                theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                                if (theSqlDataReader3.HasRows)
                                {
                                    while (theSqlDataReader3.Read())
                                    {
                                        specifications += "<br/>" + theSqlDataReader3["Specialisation"].ToString();
                                    }
                                }

                                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                                DLdb.SQLST3.Parameters.RemoveAt(0);
                                DLdb.RS3.Close();

                                DateTime regisEnd = Convert.ToDateTime(theSqlDataReader2["RegistrationEnd"].ToString());

                                string licensedPlumberIcons = "";
                                string licensedPlumberIconstwo = "";
                                if (theSqlDataReader["designation"].ToString() == "Licensed Plumber")
                                {
                                    licensedPlumberIcons = "<div style=\"font-size:10px;\"><img src=\"https://197.242.82.242/inspectit/assets/icons/Above Ground Drainage Icon.png\" style='height:20px;'/>Above Ground Drainage</div><div style=\"font-size:10px;\"><img src=\"https://197.242.82.242/inspectit/assets/icons/Below Ground Drainage Icon.png\" style='height:20px;'/>Below Ground Drainage</div><div style=\"font-size:10px;\"><img src=\"https://197.242.82.242/inspectit/assets/icons/Rainwater Disposal Icon.png\" style='height:20px;'/>Rain Water Drainage</div>";
                                    //    "<div style=\"font-size:10px;\"><img src=\"https://197.242.82.242/inspectit/assets/icons/Above Ground Drainage Icon.png\" style=\"height:20px;\" />&nbsp;Above Ground Drainage</div><br />" +
                                    //    "<div style=\"font-size:10px;\"><img src=\"https://197.242.82.242/inspectit/assets/icons/Below Ground Drainage Icon.png\" style=\"height:20px;/>&nbsp;Below Ground Drainage</div><br />" +
                                    //    "<div style=\"font-size:10px;\"><img src=\"https://197.242.82.242/inspectit/assets/icons/Rainwater Disposal Icon.png\" style=\"height:20px;/>&nbsp;Rain Water Drainage</div>" +

                                    //                        "";

                                    licensedPlumberIconstwo = "<div style=\"font-size:10px;\"><img src=\"https://197.242.82.242/inspectit/assets/icons/Cold Water Icon.png\" style='height:20px;'/>Cold Water</div><div style=\"font-size:10px;\"><img src=\"https://197.242.82.242/inspectit/assets/icons/Hot Water Icon.png\" style='height:20px;'/>Hot Water</div>";
                                    //<div style=\"font-size:10px;\"><img src=\"https://197.242.82.242/inspectit/assets/icons/Cold Water Icon.png\" style=\"height:20px;/>&nbsp;Cold Water</div><br />" +
                                    //    "<div style=\"font-size:10px;\"><img src=\"https://197.242.82.242/inspectit/assets/icons/Hot Water Icon.png\" style=\"height:20px;/>&nbsp;Hot Water</div><br />" ;
                                }

                                // Build FRONT
                                CardFront.InnerHtml = "<table border=\"0\" class=\"shadow\">" +
                                                   "<tr>" +
                                                   "     <td>" +
                                                   "         <table border=\"0\">" +
                                                   "             <tr>" +
                                                   "                 <td><img src=\"assets/img/cardlogo.jpg\" class=\"img-responsive\" /></td>" +
                                                   "             </tr>" +
                                                   "             <tr>" +
                                                   "                 <td style=\"padding:10px;\">" +
                                                   "                     <div style=\"padding-top:10px;\"><b>Reg No: " + theSqlDataReader2["RegNo"].ToString() + "</b></div>" +
                                                   "                     <div><b>Renewal Date: " + regisEnd.ToString("dd/MM/yyyy") + "</b></div>" +
                                                   "                 </td>" +
                                                   "             </tr>" +
                                                   "         </table>" +
                                                   "     </td>" +
                                                   "     <td style=\"padding:10px;\">" +
                                                   "         <img src=\"" + PlumberPhoto + "\" style=\"width:160px;height:160px;border:2px solid " + CardColor + ";\" />" +
                                                   "            " + theSqlDataReader2["fName"].ToString() + " " + theSqlDataReader2["lName"].ToString() + "" +
                                                   "     </td>" +
                                                   " </tr>" +
                                                   " <tr>" +
                                                   "     <td style=\"padding:10px;\">&nbsp;</td>" +
                                                   " </tr>" +
                                                   " <tr>" +
                                                   "     <td colspan=\"2\" style=\"background-color:" + CardColor + ";padding:10px;font-size:20px;color:white;font-weight:bold;\">" +
                                                   "         <div style=\"top:200px;left:50px;position:absolute;\"><img src=\"" + CardIcon + "\" style=\"width:65px;height:auto;\" /></div>" +
                                                   "         <div style=\"margin-left:150px;\" id=\"CardDesignation\" runat=\"server\">" + theSqlDataReader["designation"].ToString() + "</div>" +
                                                   "     </td>" +
                                                   " </tr>" +
                                                   "</table>";

                                //BUILD BACK
                                CardBack.InnerHtml = "<table border=\"0\">" +
                                                  "  <tr>" +
                                                   "     <td width=\"90%\">" +
                                                   "         <table border=\"0\" style=\"height:260px;\">" +
                                                   "             <tr>" +
                                                   "                 <td colspan=\"2\" style=\"padding:10px;font-size:10px;\">This card holder is only entitled to purchase and issue Plumbing COC’s for the following categories of plumbing and plumbing specialisations:</td>" +
                                                   "             </tr>" +
                                                   "             <tr>" +
                                                   // "                " + licensedPlumberIcons +
                                                   //"                 <td style=\"padding:10px;\"><img src=\"" + CardIcon + "\" style=\"width:28px;height:auto;\" /> <span style=\"padding-top:5px;\">" + theSqlDataReader["designation"].ToString() + "</span></td>" +
                                                   //"                 <td style=\"padding:10px;\">" + specifications + "</td>" +
                                                   "                 <td style=\"padding:10px;\">" + licensedPlumberIcons + "</td>" +
                                                   "                 <td style=\"padding:10px;\">" + licensedPlumberIconstwo + "</td>" +
                                                   "             </tr>" +
                                                   "             <tr style=\"padding:10px;border-top:1px solid black;\">" +
                                                   "                 <td style=\"padding:10px;border-top:1px solid black;border-right:1px solid black;\">" +
                                                   "                     <div><b>Current Employer:</b></div>" +
                                                   "                     <div style=\"width:100%;text-align:center;\">" + CompanyName + "</div>" +
                                                   "                 </td>" +
                                                   "                 <td style=\"padding:10px;border-top:1px solid black;\">" +
                                                   "                    <div><b>Specialisations:</b></div>" + specifications + "</td>" +
                                                   "             </tr>" +
                                                   "         </table>" +
                                                   "     </td>" +
                                                   "     <td width=\"10px\" style=\"background-color:" + CardColor + ";padding:10px;font-size:20px;color:white;\">" +
                                                   "         <div style=\"transform: rotate(270deg);white-space:nowrap;width:30px;transform-origin: right bottom 10%;margin-top:110px;font-weight:bold;\">" + theSqlDataReader["designation"].ToString() + "</div>" +
                                                   "     </td>" +
                                                   " </tr>" +
                                                   "</table>";
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

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from PlumberDesignations where PlumberID = @PlumberID";
                DLdb.SQLST.Parameters.AddWithValue("PlumberID", pirbID.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
                DLdb.SQLST.Parameters.AddWithValue("PlumberID", pirbID.ToString());
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
                
                string logged = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select count(*) as Total from [dbo].[COCStatements] where UserID = @UserID and [Status] = 'Logged'";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        logged = theSqlDataReader["Total"].ToString();
                        loggedcoc.Text = logged;
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string nonlogged = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select count(*) as Total from [dbo].[COCStatements] where UserID = @UserID and [Status] = 'Non-Logged'";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        nonlogged = theSqlDataReader["Total"].ToString();
                        nonloggedcoc.Text = nonlogged;
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                //string nonloggedalloc = "";
                //DLdb.RS.Open();
                //DLdb.SQLST.CommandText = "select count(*) as Total from [dbo].[COCStatements] where UserID = @UserID and [Status] = 'Non-Logged' and AuditorID <> '0'";
                //DLdb.SQLST.Parameters.AddWithValue("UserID",  DLdb.Decrypt(Request.QueryString["UserID"].ToString()));
                //DLdb.SQLST.CommandType = CommandType.Text;
                //DLdb.SQLST.Connection = DLdb.RS;
                //theSqlDataReader = DLdb.SQLST.ExecuteReader();

                //if (theSqlDataReader.HasRows)
                //{
                //    while (theSqlDataReader.Read())
                //    {
                //        nonloggedalloc = theSqlDataReader["Total"].ToString();
                //        nonloggedcocallocated.Text = nonloggedalloc;
                //    }
                //}

                //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.RS.Close();

                string auditedCoc = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select count(*) as Total from [dbo].[COCStatements] where UserID = @UserID and isAudit='1'";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        auditedCoc = theSqlDataReader["Total"].ToString();
                        numcocaudited.Text = auditedCoc;
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                foreach (ListItem listItemDisp in regDetails.Items) 
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from PlumberSpecialisations where PlumberID = @PlumberID and Specialisation = @Specialisation and isactive = '1'";
                    DLdb.SQLST.Parameters.AddWithValue("PlumberID", pirbid);
                    DLdb.SQLST.Parameters.AddWithValue("Specialisation", listItemDisp.Value.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            listItemDisp.Selected = true;
                        }
                    }
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from PlumberDesignations where PlumberID = @PlumberID and Designation = @Designation and isactive = '1'";
                    DLdb.SQLST.Parameters.AddWithValue("PlumberID", pirbid);
                    DLdb.SQLST.Parameters.AddWithValue("Designation", listItemDisp.Value.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            listItemDisp.Selected = true;
                        }
                    }
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM PerformanceStatus where isActive='1' and UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        displayusers.InnerHtml += "<tr>" +
                                                           "<td>" + theSqlDataReader["Date"].ToString() + "</td>" +
                                                           "<td>" + theSqlDataReader["PerformanceType"].ToString() + "</td>" +
                                                           "<td>" + theSqlDataReader["Details"].ToString() + "</td>" +
                                                           "<td>" + theSqlDataReader["PerformancePointAllocation"].ToString() + "</td>" +
                                                           "<td>" + theSqlDataReader["Attachment"].ToString() + "</td>" +
                                                           "<td><div class=\"btn btn-sm btn-primary\" title=\"Edit\" onclick='editPerformanceStatus("+ theSqlDataReader["PerformanceStatusID"].ToString() + ")'><i class=\"fa fa-pencil\"></i></div>" +
                                                           "<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeletePerformanceStatus.aspx?op=del&uid=" + Session["IIT_UID"].ToString() + "&id=" + theSqlDataReader["PerformanceStatusID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                       "</tr>";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM PerformanceStatus where isActive='0' and UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        displayusers_del.InnerHtml += "<tr>" +
                                                           "<td>" + theSqlDataReader["Date"].ToString() + "</td>" +
                                                           "<td>" + theSqlDataReader["PerformanceType"].ToString() + "</td>" +
                                                           "<td>" + theSqlDataReader["Details"].ToString() + "</td>" +
                                                           "<td>" + theSqlDataReader["PerformancePointAllocation"].ToString() + "</td>" +
                                                           "<td>" + theSqlDataReader["Attachment"].ToString() + "</td>" +
                                                           "<td><div class=\"btn btn-sm btn-primary\" title=\"Edit\" onclick='editPerformanceStatus(" + theSqlDataReader["PerformanceStatusID"].ToString() + ")'><i class=\"fa fa-pencil\"></i></div>" +
                                                           "<div class=\"btn btn-sm btn-success\" onclick=\"deleteconf('DeletePerformanceStatus.aspx?op=undel&uid=" + Session["IIT_UID"].ToString()+"&id=" + theSqlDataReader["PerformanceStatusID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                       "</tr>";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                performanceType.Items.Add(new ListItem("", ""));
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from performancetypes where isactive='1' and isCompany='0'";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        performanceType.Items.Add(new ListItem(theSqlDataReader["Type"].ToString(), theSqlDataReader["Type"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Assessments where UserID=@UserID and isActive='1'";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        string category = "";
                        string Activity = "";
                        string approved = "";
                        string editBtn = "";
                        string dltBtn = "";

                        if (theSqlDataReader["Category"].ToString() == "1")
                        {
                            category = "Category 1: Developmental Activities";
                        }
                        else if (theSqlDataReader["Category"].ToString() == "2")
                        {
                            category = "Category 2: Work-based Activities";
                        }
                        else
                        {
                            category = "Category 3: Individual Activities";
                        }

                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM CPDActivities where CPDActivityID=@CPDActivityID";
                        DLdb.SQLST2.Parameters.AddWithValue("CPDActivityID", theSqlDataReader["CPDActivityID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                Activity = theSqlDataReader2["Activity"].ToString();
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        if (theSqlDataReader["isApproved"].ToString() == "True")
                        {
                            approved = "<span class=\"label label-success\">Approved</span>";
                            editBtn = "<div class=\"btn btn-sm btn-primary\" onclick=\"editCpdActivity(" + theSqlDataReader["AssessmentID"].ToString() + ")\"><i class=\"fa fa-eye\"></i></div>";
                            dltBtn = "<div class=\"btn btn-sm btn-danger disabled\"><i class=\"fa fa-trash\"></i></div>";
                        }
                        else
                        {
                            approved = "<span class=\"label label-danger\">Not Approved</span>";
                            //  editBtn = "<a href=\"EditOrDeleteAssessment.aspx?aid=" + theSqlDataReader["AssessmentID"].ToString() + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>";
                            editBtn = "<div class=\"btn btn-sm btn-primary\" onclick=\"editCpdActivity(" + theSqlDataReader["AssessmentID"].ToString() + ")\"><i class=\"fa fa-eye\"></i></div>";
                            dltBtn = "<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeleteAssessmentAdmin.aspx?op=del&uid=" + Session["IIT_UID"].ToString()+"&aid=" + theSqlDataReader["AssessmentID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div>";
                        }
                        Tbody1.InnerHtml += "<tr>" +
                                                           "<td>" + theSqlDataReader["CertificateDate"].ToString() + "</td>" +
                                                           "<td>" + category + "</td>" +
                                                           "<td>" + Activity + "</td>" +
                                                           "<td>" + theSqlDataReader["NoPoints"].ToString() + "</td>" +
                                                           "<td>" + approved + "</td>" +
                                                           "<td>" + editBtn +
                                                           dltBtn + "</td>" +
                                                       "</tr>";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Assessments where UserID=@UserID and isActive='0'";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        string category = "";
                        string Activity = "";
                        string approved = "";
                        string editBtn = "";
                        string dltBtn = "";

                        if (theSqlDataReader["Category"].ToString() == "1")
                        {
                            category = "Category 1: Developmental Activities";
                        }
                        else if (theSqlDataReader["Category"].ToString() == "2")
                        {
                            category = "Category 2: Work-based Activities";
                        }
                        else
                        {
                            category = "Category 3: Individual Activities";
                        }

                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM CPDActivities where CPDActivityID=@CPDActivityID";
                        DLdb.SQLST2.Parameters.AddWithValue("CPDActivityID", theSqlDataReader["CPDActivityID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                Activity = theSqlDataReader2["Activity"].ToString();
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        if (theSqlDataReader["isApproved"].ToString() == "True")
                        {
                            approved = "<span class=\"label label-success\">Approved</span>";
                            editBtn = "<div class=\"btn btn-sm btn-primary\" onclick=\"editCpdActivity(" + theSqlDataReader["AssessmentID"].ToString() + ")\"><i class=\"fa fa-eye\"></i></div>";
                            dltBtn = "<div class=\"btn btn-sm btn-danger disabled\"><i class=\"fa fa-trash\"></i></div>";
                        }
                        else
                        {
                            approved = "<span class=\"label label-danger\">Not Approved</span>";
                            editBtn = "<div class=\"btn btn-sm btn-primary\" onclick=\"editCpdActivity("+ theSqlDataReader["AssessmentID"].ToString() + ")\"><i class=\"fa fa-eye\"></i></div>";
                            dltBtn = "<div class=\"btn btn-sm btn-success\" onclick=\"deleteconf('DeleteAssessmentAdmin.aspx?op=undel&uid=" + Session["IIT_UID"].ToString() + "&aid=" + theSqlDataReader["AssessmentID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div>";
                        }
                        Tbody2.InnerHtml += "<tr>" +
                                                           "<td>" + theSqlDataReader["CertificateDate"].ToString() + "</td>" +
                                                           "<td>" + category + "</td>" +
                                                           "<td>" + Activity + "</td>" +
                                                           "<td>" + theSqlDataReader["NoPoints"].ToString() + "</td>" +
                                                           "<td>" + approved + "</td>" +
                                                           "<td>" + editBtn + dltBtn + "</td>" +
                                                       "</tr>";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.DB_Close();
            }
        }
        
        protected void btnUpdatePlumber_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "UPDATE Users SET HomePhone=@HomePhone,SecondEmail=@SecondEmail,ResidentialStreet=@ResidentialStreet,ResidentialSuburb=@ResidentialSuburb,ResidentialCity=@ResidentialCity," +
                "Province=@Province,PostalAddress=@PostalAddress,PostalSuburb=@PostalSuburb,PostalCity=@PostalCity,PostalCode=@PostalCode,PostalProvince=@PostalProvince,company=@company,SocioeconomicStatus=@SocioeconomicStatus," +
                "DeliveryMethod=@DeliveryMethod,RegistrationCard=@RegistrationCard WHERE UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("HomePhone", homePhone.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SecondEmail", SecondEmail.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ResidentialStreet", PhysicalAddress.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ResidentialSuburb", physicalSuburb.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ResidentialCity", physicalCities.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Province", DropDownList5.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalAddress", postalAddressPlumber.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalSuburb", postalSuburb.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalCity", postalCities.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalCode", PostalCode.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalProvince", DropDownList4.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("company", CompanyID.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SocioeconomicStatus", EmploymentStatus.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("DeliveryMethod", DropDownList2.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("RegistrationCard", DropDownList1.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("myDetails.aspx?msg=" + DLdb.Encrypt("Profile has been updated"));
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (FileUpload1.HasFiles)
            {
                foreach (HttpPostedFile File in FileUpload1.PostedFiles)
                {
                    string filename = File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/performanceImgs/"), filename));
                }
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into PerformanceStatus (hasEndDate,endDate,UserID,Date,PerformanceType,Details,PerformancePointAllocation,Attachment) values (@hasEndDate,@endDate,@UserID,@Date,@PerformanceType,@Details,@PerformancePointAllocation,@Attachment)";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("Date", date.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PerformanceType", performanceType.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Details", details.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PerformancePointAllocation", points.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Attachment", FileUpload1.FileName);
            DLdb.SQLST.Parameters.AddWithValue("endDate", endDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("hasEndDate", hasEndDate.Checked ? 1 : 0);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string ImgOld = TextBox1aa.Text.ToString();
            if (FileUpload1.HasFiles)
            {
                ImgOld = FileUpload1.FileName;
                foreach (HttpPostedFile File in FileUpload1.PostedFiles)
                {
                    string filename = File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/performanceImgs/"), filename));
                }
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update PerformanceStatus set endDate=@endDate,hasEndDate=@hasEndDate,Date=@Date,PerformanceType=@PerformanceType,Details=@Details,PerformancePointAllocation=@PerformancePointAllocation,Attachment=@Attachment where PerformanceStatusID=@PerformanceStatusID";
            DLdb.SQLST.Parameters.AddWithValue("Date", date.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PerformanceType", performanceType.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Details", details.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PerformancePointAllocation", points.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Attachment", ImgOld.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PerformanceStatusID", PerformanceStatusID.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("endDate", endDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("hasEndDate", hasEndDate.Checked ? 1 : 0);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {

            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (!CheckBox1.Checked)
            {
                //checkBxErr.Visible = true;
                //checkBxErr.InnerHtml = "Please check the declaration before submitting this CPD Activity";
            }
            else
            {
                if (img.HasFiles)
                {
                    foreach (HttpPostedFile File in img.PostedFiles)
                    {
                        string filename = File.FileName;
                        File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AssessmentImgs/"), filename));
                    }
                }

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "INSERT INTO Assessments (UserID,Attachment,Category,Activity,ProductCode,CPDActivityID,NoPoints,CertificateDate,Declaration)" +
                                         "VALUES (@UserID,@Attachment,@Category,@Activity,@ProductCode,@CPDActivityID,@NoPoints,@CertificateDate,@Declaration)";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("Category", Category.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("ProductCode", productCode.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CPDActivityID", CPDActivityID.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("NoPoints", pointsActivity.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CertificateDate", activityDate.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Activity", Activity.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Declaration", CheckBox1.Checked ? 1 : 0);
                DLdb.SQLST.Parameters.AddWithValue("Attachment", img.FileName);

                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

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

                DLdb.DB_Close();
                Response.Redirect("UserAssessments.aspx?msg=" + DLdb.Encrypt("Assessment added successfuly"));
            }



            // EMAIL THE USER DETAILS
            //string HTMLSubject = "Welcome to Inspect IT.";
            //string HTMLBody = "Dear " + fName.Text.ToString() + "<br /><br />Welcome to Inspect IT<br /><br />Your login details are;<br />Email Address: " + email.Text.ToString() + "<br />Password: " + pass.ToString() + "<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Team";
            // DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", email.Text.ToString(), "");

        }

        protected void searchProdCode_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from CPDActivities where ProductCode=@ProductCode and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("ProductCode", productCode.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                Category.SelectedValue = theSqlDataReader["Category"].ToString();
                Activity.Text = theSqlDataReader["Activity"].ToString();
                pointsActivity.Text = theSqlDataReader["Points"].ToString();
                activityDate.Text = theSqlDataReader["StartDate"].ToString();
                CPDActivityID.Text = theSqlDataReader["CPDActivityID"].ToString();
            }
            else
            {
                errMsgProdCode.Visible = true;
                errMsgProdCode.InnerHtml = "Unable to find product code entered";
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        protected void updateBtnActivity_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            

            DLdb.DB_Close();
        }

        protected void updateBtnDocument_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();



            DLdb.DB_Close();
        }

        protected void myListDropDown_Change(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DropDownList2.Items.Clear();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Area where ProvinceID=@ProvinceID order by name asc";
            DLdb.SQLST.Parameters.AddWithValue("ProvinceID", DropDownList4.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    postalCities.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["id"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            DLdb.DB_Close();
        }

        protected void myListDropDown2_Change(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DropDownList2.Items.Clear();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Area where ProvinceID=@ProvinceID order by name asc";
            DLdb.SQLST.Parameters.AddWithValue("ProvinceID", DropDownList5.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    physicalCities.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["id"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            DLdb.DB_Close();
        }

        protected void myListDropDownCity_Change(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from AreaSuburbs where CityID=@CityID order by name asc";
            DLdb.SQLST.Parameters.AddWithValue("CityID", postalCities.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    postalSuburb.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["suburbid"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            DLdb.DB_Close();
        }

        protected void myListDropDownCity2_Change(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from AreaSuburbs where CityID=@CityID order by name asc";
            DLdb.SQLST.Parameters.AddWithValue("CityID", physicalCities.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    physicalSuburb.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["suburbid"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            DLdb.DB_Close();
        }


    }
}