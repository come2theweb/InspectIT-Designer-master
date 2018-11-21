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
    public partial class PurchasePlumbingCOCSupplier : System.Web.UI.Page
    {
        public int NoAvailable = 0;
        public int MaxCount = 0;
        public string SupplierID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //displayfrm.Visible = false;
            Global DLdb = new Global();
            DLdb.DB_Connect();

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
                DLdb.SQLST.CommandText = "SELECT * FROM Suppliers where UserID = @UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["isBlocked"].ToString() == "True")
                        {
                            errormsg.InnerHtml = "You have been blocked from allocating COCs";
                            errormsg.Visible = true;
                            SearchPlumber.Enabled = false;
                        }
                    }
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from Suppliers where UserID = @UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["TotalNumber"].ToString() == "0")
                        {
                            errormsg.InnerHtml = "You have no stock assigned to you";
                            errormsg.Visible = true;
                            SearchPlumber.Enabled = false;
                        }
                        else
                        {
                            NoStock.Text = theSqlDataReader["TotalNumber"].ToString();
                            MaxCount = Convert.ToInt32(theSqlDataReader["TotalNumber"]);
                            NoUsed.Text = theSqlDataReader["TotalUsed"].ToString();
                            //countCertis.Text = theSqlDataReader["TotalNumber"].ToString();
                        }
                        SupplierID = theSqlDataReader["SupplierID"].ToString();
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                StartRange.Items.Clear();
                StartRange.Items.Add(new ListItem("", ""));

                EndRange.Items.Clear();
                EndRange.Items.Add(new ListItem("", ""));

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCStatements where SupplierID = @SupplierID and UserID='0'";
                DLdb.SQLST.Parameters.AddWithValue("SupplierID", SupplierID);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        StartRange.Items.Add(new ListItem(theSqlDataReader["COCStatementID"].ToString(), theSqlDataReader["COCStatementID"].ToString()));
                        EndRange.Items.Add(new ListItem(theSqlDataReader["COCStatementID"].ToString(), theSqlDataReader["COCStatementID"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select count(*) as countedCoc from COCStatements where SupplierID = @SupplierID and UserID='0'";
                DLdb.SQLST.Parameters.AddWithValue("SupplierID", SupplierID);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        countCertis.Text = theSqlDataReader["countedCoc"].ToString();
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.DB_Close();
            }
        }

        protected void SearchPlumber_Click(object sender, EventArgs e)
        {
            if (RegNo.Text.ToString() != "")
            {

                Global DLdb = new Global();
                DLdb.DB_Connect();
                Boolean runCode = false;
                Boolean suspendedd = false;
                string UserID = "0";
                string pirbID = "";
                // Search Plumbers
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from users where RegNo = @RegNo";
                DLdb.SQLST.Parameters.AddWithValue("RegNo", RegNo.Text.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    UserID = theSqlDataReader["UserID"].ToString();
                    PlumberRegNo.Text = theSqlDataReader["regno"].ToString();
                    PlumberContact.Text = theSqlDataReader["contact"].ToString();
                    PlumberEmail.Text = theSqlDataReader["email"].ToString();
                    pirbID = theSqlDataReader["PIRBID"].ToString();
                    notice.Text = theSqlDataReader["Notice"].ToString();
                    PlumberFullName.Text = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                    if (theSqlDataReader["isSuspended"].ToString() == "True")
                    {
                        suspendedd = true;
                        runCode = false;
                        err.Visible = true;
                        displayfrm.Visible = false;
                        err.InnerHtml = "Plumber is suspended and may not purchase COC's";
                    }
                    else
                    {

                        displayfrm.Visible = true;
                        runCode = true;
                        err.Visible = false;
                    }
                    //displayfrm.Visible = true;

                }
                else
                {
                    runCode = false;
                    err.Visible = true;
                    displayfrm.Visible = false;
                    err.InnerHtml = "Registration No is invaild - Please try again or advise the Licensed Plumber to Contact the PIRB";
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();



                if (runCode == true)
                {
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
                                            PlumberBusContact.Text = theSqlDataReader3["CompanyContactNo"].ToString();
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
                                    if (theSqlDataReader["designation"].ToString()== "Licensed Plumber")
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
                    // GET BATCH NUMBERS AND TOTAL AVALAIBLE

                    int NoBought = 0;
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select count(*) as Total from [dbo].[COCStatements] where UserID = @UserID and DateLogged is null";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

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

                    int permittedCOCs = 0;
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID = @UserID and NoCOCpurchases <> '0'";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            permittedCOCs = Convert.ToInt32(theSqlDataReader["NoCOCpurchases"].ToString());
                            NameofPlumber.InnerHtml = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                            NumberOfPermittedCOCs.Text = theSqlDataReader["NoCOCpurchases"].ToString();
                            COCsAbleToPurchase.Text = (Convert.ToInt32(theSqlDataReader["NoCOCpurchases"]) - NoBought).ToString();
                            NumberTo.InnerHtml = theSqlDataReader["contact"].ToString();
                            NoAvailable = Convert.ToInt32(theSqlDataReader["NoCOCpurchases"].ToString());
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

                    if (NoBought == permittedCOCs)
                    {
                        errormsg.Visible = true;
                        errormsg.InnerHtml = "Too many Non Logged COC's, can't purchase more.";
                    }

                    //Response.Write(NoBought+":"+ permittedCOCs);
                    //Response.End();
                }

                DLdb.DB_Close();
            }
            else
            {
                err.Visible = true;
                displayfrm.Visible = false;
                err.InnerHtml = "Please enter a Reg Number";
            }
        }

        protected void btn_buy_Click(object sender, EventArgs e)
        {
            if (NoCOCsPurchase.Text != "")
            {
                int CurNo = Convert.ToInt32(NoCOCsPurchase.Text);
                //if (CurNo > MaxCount)
                //{
                //    errormsg.InnerHtml = "You do not have enough stock to continue";
                //    errormsg.Visible = true;
                //}
                //else
                //{
                // check number avaliable and current
                int NoAv = Convert.ToInt32(COCsAbleToPurchase.Text);
                
                if (CurNo > NoAv)
                {
                    errormsg.InnerHtml = "Can't purchase more than 'Number of COC's I am able to Purchase'";
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
                        errormsg.Visible = false;
                        string OrderID = "";
                        string Type = "Paper";
                        string Method = "Collect at PIRB Supplier";

                        Global DLdb = new Global();
                        DLdb.DB_Connect();

                        // GET UISERID
                        string userid = "0";
                        string addy = "";
                        string name = "";
                        string cont = "";
                        string email = "";
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "Select * from users where RegNo = @RegNo";
                        DLdb.SQLST2.Parameters.AddWithValue("RegNo", RegNo.Text.ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            theSqlDataReader2.Read();
                            userid = theSqlDataReader2["UserID"].ToString();
                            notice.Text = theSqlDataReader2["Notice"].ToString();
                            name = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                            cont = theSqlDataReader2["contact"].ToString();
                            email = theSqlDataReader2["email"].ToString();
                            addy = theSqlDataReader2["ResidentialStreet"].ToString() + ",<br/> " + theSqlDataReader2["ResidentialSuburb"].ToString() + ",<br/> " + theSqlDataReader2["ResidentialCity"].ToString() + ",<br/> " + theSqlDataReader2["ResidentialCode"].ToString() + ",<br/> " + theSqlDataReader2["Province"].ToString();
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        // CREATE ORDER
                        DLdb.RS.Open();
                        DLdb.SQLST.CommandText = "INSERT INTO Orders (SupplierID,UserID,Description,TotalNoItems,SubTotal,Vat,Delivery,Total,COCType,Method,isPaid,StartRange,EndRange) values (@SupplierID,@UserID,@Description,@TotalNoItems,@SubTotal,@Vat,@Delivery,@Total,@COCType,@Method,'0',@StartRange,@EndRange); Select Scope_Identity() as OrderID";
                        DLdb.SQLST.Parameters.AddWithValue("UserID", userid);
                        DLdb.SQLST.Parameters.AddWithValue("SupplierID", Session["IIT_UID"].ToString());
                        DLdb.SQLST.Parameters.AddWithValue("Description", "COC Purchase");
                        DLdb.SQLST.Parameters.AddWithValue("TotalNoItems", NoCOCsPurchase.Text.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("SubTotal", CertificateCost.Text.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("Vat", VATCOCPurchase.Text.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("Delivery", DeliveryCostCOCPurchase.Text.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("Total", totalAmountdue.Text.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("COCType", Type);
                        DLdb.SQLST.Parameters.AddWithValue("Method", Method);
                        DLdb.SQLST.Parameters.AddWithValue("StartRange", StartRange.SelectedValue);
                        DLdb.SQLST.Parameters.AddWithValue("EndRange", EndRange.SelectedValue);
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
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.RS.Close();

                        // CREATE SUPPLIER STOCK
                        int sLeft = MaxCount - CurNo;
                        int sUsed = (Convert.ToInt32(NoUsed.Text) + CurNo);

                        //DLdb.RS.Open();
                        //DLdb.SQLST.CommandText = "update Suppliers set TotalNumber = @TotalNumber, TotalUsed = @TotalUsed where UserID = @UserID";
                        //DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                        //DLdb.SQLST.Parameters.AddWithValue("TotalUsed", sUsed);

                        //DLdb.SQLST.CommandType = CommandType.Text;
                        //DLdb.SQLST.Connection = DLdb.RS;
                        //theSqlDataReader = DLdb.SQLST.ExecuteReader();

                        ////if (theSqlDataReader.HasRows)
                        ////{
                        ////    theSqlDataReader.Read();
                        ////    OrderID = theSqlDataReader["OrderID"].ToString();
                        ////}

                        //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                        //DLdb.SQLST.Parameters.RemoveAt(0);
                        //DLdb.SQLST.Parameters.RemoveAt(0);
                        //DLdb.RS.Close();


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
                        //                            "                    <td align='left' width='40%'><font style='font-size:26px;'><b>INVOICE No. : " + OrderID.ToString() + "</b><br />Period: </b>" + srtMM + "-" + srtYY + "</font></td></tr>" +
                        //                            "                    </tr>" +
                        //                            "                    <tr>" +
                        //                            "                        <td align='left' width='70%'><br /><h4></h4>" + name.ToString() + "<br/>" + cont.ToString() + "<br/>" + email.ToString() + "</td>" +
                        //                            "                        <td align='left' width='30%' colspan='2'><br /><h4>Address :</h4>" + addy.ToString() + "</td>" +
                        //                            "                    </tr>" +
                        //                            "                    <tr>" +
                        //                            "                        <td align='left' width='70%'><br /><h4>Paper bases COC Statements</h4></td>" +
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
                        //                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>Paper</td>" +
                        //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>Pick Up</td>" +
                        //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + NoCOCsPurchase.Text.ToString() + "</td>" +
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

                        //string filename = userid + name + "_invoice_" + OrderID + "_" + srtMM + "-" + srtYY + ".pdf";
                        //var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(createPDF);
                        //string path = Server.MapPath("~/invoices/") + filename;
                        //File.WriteAllBytes(path, pdfBytes);

                        //DLdb.RS3.Open();
                        //DLdb.SQLST3.CommandText = "update Orders set PDFName=@PDFName where OrderID=@OrderID";
                        //DLdb.SQLST3.Parameters.AddWithValue("OrderID", OrderID);
                        //DLdb.SQLST3.Parameters.AddWithValue("PDFName", filename);
                        //DLdb.SQLST3.CommandType = CommandType.Text;
                        //DLdb.SQLST3.Connection = DLdb.RS3;
                        //SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                        //if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        //DLdb.SQLST3.Parameters.RemoveAt(0);
                        //DLdb.SQLST3.Parameters.RemoveAt(0);
                        //DLdb.RS3.Close();

                        // SMS OTP CODE
                        if (NumberTo.InnerHtml.ToString() != "")
                        {

                            string OTPCode = DLdb.CreateNumber(5);
                            Session["IIT_OTPCode"] = OTPCode;
                            DLdb.sendSMS(PID.Value.ToString(), NumberTo.InnerHtml, "Inspect-It: You would like to purchase " + NoCOCsPurchase.Text.ToString() + " COC's for Collection from PIRB Reseller. OTP Code: " + OTPCode + ". Report any fraudulent activity to PIRB Immediately.");
                        }

                        Response.Redirect("VerifyPurchase?oid=" + DLdb.Encrypt(OrderID));
                        DLdb.DB_Close();
                    }
                }
                // }
            }
            else
            {
                errormsg.InnerHtml = "Please enter an ammount into 'Number of COC's You wish to Purchase'";
                errormsg.Visible = true;
            }
        }

        protected void EndRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StartRange.SelectedValue.ToString() != "")
            {
                int sRange = Convert.ToInt32(StartRange.SelectedValue);
                int eRange = Convert.ToInt32(EndRange.SelectedValue);
                eRange = eRange + 1;
                int curAmount = 0;

                for (int i = sRange; i < eRange; i++)
                {
                    curAmount++;
                }

                errormsg.Visible = false;
                NoCOCsPurchase.Text = curAmount.ToString();

                double TotalCost = (curAmount * 108.77);

                CertificateCost.Text = TotalCost.ToString();

                double VAT = (TotalCost * 0.15);

                VATCOCPurchase.Text = VAT.ToString();

                double TOTCost = (TotalCost + VAT);

                TotalDueCOCPurchase.Text = Math.Round(TOTCost, 2).ToString();
                totalAmountdue.Text = Math.Round(TOTCost, 2).ToString();
            }
        }
    }
}