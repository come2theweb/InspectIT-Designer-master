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
    public partial class NewPlumberRegistrationAdmin : System.Web.UI.Page
    {
        public string userID = "";
        public string userIDaa = "";
        public string ApplicationID = "";
        public string disabledEnabledRadios = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }
            string newAppID = "";
            string ComPID = "";
            if (!IsPostBack)
            {
                if (Request.QueryString["npid"] != null)
                {
                    ApplicationID = DLdb.Decrypt(Request.QueryString["npid"].ToString());
                    newAppID = DLdb.Decrypt(Request.QueryString["npid"].ToString());
                    FileUpload1.Visible = false;
                    postalCities.Visible = false;
                    postalSuburb.Visible = false;
                    physicalCities.Visible = false;
                    physicalSuburb.Visible = false;

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from NewApplications where ApplicationID=@ApplicationID";
                    DLdb.SQLST.Parameters.AddWithValue("ApplicationID", newAppID.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            userIDaa = theSqlDataReader["UserID"].ToString();
                            userID = theSqlDataReader["UserID"].ToString();
                            if (theSqlDataReader["ProcedureRegistration"].ToString() == "True")
                            {
                                CheckBox1.Checked = true;
                            }
                            if (theSqlDataReader["CodeConduct"].ToString() == "True")
                            {
                                CheckBox2.Checked = true;
                            }
                            if (theSqlDataReader["Acknowledgement"].ToString() == "True")
                            {
                                CheckBox3.Checked = true;
                            }
                            if (theSqlDataReader["Declaration"].ToString() == "True")
                            {
                                CheckBox4.Checked = true;
                            }
                            if (theSqlDataReader["isApproved"].ToString() == "True")
                            {
                                isApproved.Checked = true;
                            }
                            if (theSqlDataReader["isRejected"].ToString() == "True")
                            {
                                isRejected.Checked = true;
                            }
                            if (theSqlDataReader["IDAttached"].ToString() == "True")
                            {
                                IDAttached.Checked = true;
                            }
                            if (theSqlDataReader["InitialEachPage"].ToString() == "True")
                            {
                                InitialEachPage.Checked = true;
                            }
                            if (theSqlDataReader["QualificationVerified"].ToString() == "True")
                            {
                                QualificationVerified.Checked = true;
                            }
                            if (theSqlDataReader["CompanyDetailsCorrect"].ToString() == "True")
                            {
                                CompanyDetailsCorrect.Checked = true;
                            }
                            if (theSqlDataReader["ProofExperience"].ToString() == "True")
                            {
                                ProofExperience.Checked = true;
                            }
                            if (theSqlDataReader["PhotoCorrect"].ToString() == "True")
                            {
                                PhotoCorrect.Checked = true;
                            }
                            if (theSqlDataReader["InductionDone"].ToString() == "True")
                            {
                                InductionDone.Checked = true;
                            }
                            if (theSqlDataReader["PaymentRecieved"].ToString() == "True")
                            {
                                PaymentRecieved.Checked = true;
                            }
                            if (theSqlDataReader["Declaration"].ToString() == "True")
                            {
                                Declaration.Checked = true;
                            }
                            RadioButtonList2.SelectedValue = theSqlDataReader["Designation"].ToString();
                            DropDownList1.SelectedValue = theSqlDataReader["RegistrationCard"].ToString();
                            DropDownList2.SelectedValue = theSqlDataReader["DeliveryMethod"].ToString();
                            RadioButtonList1.SelectedValue = theSqlDataReader["Designation"].ToString();
                            declareName.Text = theSqlDataReader["DeclarationName"].ToString();
                            declareIDnum.Text = theSqlDataReader["DeclarationIDNumber"].ToString();

                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID";
                            DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                while (theSqlDataReader2.Read())
                                {
                                    title.Text = theSqlDataReader2["Title"].ToString();
                                    Name.Text = theSqlDataReader2["fname"].ToString();
                                    Surname.Text = theSqlDataReader2["lname"].ToString();
                                    AlternateID.Text = theSqlDataReader2["Alternate"].ToString();
                                    Gender.SelectedValue = theSqlDataReader2["Gender"].ToString();
                                    RacialStatus.SelectedValue = theSqlDataReader2["Equity"].ToString();
                                    ResidentStatus.Text = theSqlDataReader2["CitizenResidentStatus"].ToString();
                                    if (theSqlDataReader2["CitizenResidentStatus"].ToString() == "South Africa")
                                    {
                                        DropDownList3.SelectedValue = "1";
                                    }
                                    IDNumber.Text = theSqlDataReader2["IdNo"].ToString();
                                    Image1.ImageUrl = "Photos/" + theSqlDataReader2["IDPhoto"].ToString();
                                    dob.Text = theSqlDataReader2["DateofBirth"].ToString();
                                    HomeLanguage.SelectedValue = theSqlDataReader2["Language"].ToString();
                                    Nationality.SelectedValue = theSqlDataReader2["Nationality"].ToString();
                                    Disability.SelectedValue = theSqlDataReader2["DisabilityStatus"].ToString();
                                    postalAddress.Text = theSqlDataReader2["PostalAddress"].ToString();
                                    DropDownList4.SelectedValue = theSqlDataReader2["PostalProvince"].ToString();
                                    adminpostalCities.Text = theSqlDataReader2["PostalCity"].ToString();
                                    adminpostalSuburb.Text = theSqlDataReader2["PostalSuburb"].ToString();
                                    PostalCode.Text = theSqlDataReader2["PostalCode"].ToString();
                                    PhysicalAddress.Text = theSqlDataReader2["ResidentialStreet"].ToString();
                                    DropDownList5.SelectedValue = theSqlDataReader2["Province"].ToString();
                                    adminphysicalSuburb.Text = theSqlDataReader2["ResidentialSuburb"].ToString();
                                    adminphysicalCities.Text = theSqlDataReader2["ResidentialCity"].ToString();
                                    homePhone.Text = theSqlDataReader2["HomePhone"].ToString();
                                    Mobile.Text = theSqlDataReader2["contact"].ToString();
                                    emailAddress.Text = theSqlDataReader2["email"].ToString();
                                    secondEmailAddress.Text = theSqlDataReader2["SecondEmail"].ToString();
                                    WorkPhone.Text = theSqlDataReader2["BusinessPhone"].ToString();
                                    EmploymentStatus.SelectedValue = theSqlDataReader2["SocioeconomicStatus"].ToString();
                                    CompanyID.SelectedValue = theSqlDataReader2["company"].ToString();
                                    ComPID = theSqlDataReader2["company"].ToString();
                                }
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "SELECT * FROM Companies where CompanyID=@CompanyID";
                            DLdb.SQLST2.Parameters.AddWithValue("CompanyID", ComPID.ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                while (theSqlDataReader2.Read())
                                {
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
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    if (QualificationVerified.Checked && ProofExperience.Checked && Declaration.Checked && PaymentRecieved.Checked && IDAttached.Checked && InitialEachPage.Checked && PhotoCorrect.Checked && CompanyDetailsCorrect.Checked && InductionDone.Checked)
                    {
                        //isApproved.Enabled = true;
                        //isRejected.Enabled = true;
                        disabledEnabledRadios = "false";
                        isApproved.Attributes.Remove("disabled");
                        isRejected.Attributes.Remove("disabled");
                    }
                    else
                    {
                        //isApproved.Enabled = false;
                        //isRejected.Enabled = false;
                        disabledEnabledRadios = "true";
                        isApproved.Attributes.Add("disabled", "disabled");
                        isRejected.Attributes.Add("disabled", "disabled");
                    }

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from PlumberQualifications where PlumberID=@PlumberID";
                    DLdb.SQLST.Parameters.AddWithValue("PlumberID", userID);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            userQualifications.InnerHtml += "<tr>" +
                                "<td>" + theSqlDataReader["CertificationNo"].ToString() + "</td>" +
                                "<td>" + theSqlDataReader["CourseYear"].ToString() + "</td>" +
                                "<td>" + theSqlDataReader["QualifiedThrough"].ToString() + "</td>" +
                                "<td>" + theSqlDataReader["TrainingProvider"].ToString() + "</td>" +
                                "<td></td>" +
                                "</tr>";
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from NewApplicationComments where ApplicationID=@ApplicationID";
                    DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            commsHis.InnerHtml += "<p>" + theSqlDataReader["Comment"].ToString() + "</p><hr/>";
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from NewApplicationCertificates where ApplicationID=@ApplicationID";
                    DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            attachs.InnerHtml += "<a href='Assessments/" + theSqlDataReader["Certificate"].ToString() + "' taregt='_blank'>" + theSqlDataReader["Certificate"].ToString() + "</a>";
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                    ERRMSGsub.Visible = false;
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

                    routeQualification.Items.Clear();
                    routeQualification.Items.Add(new ListItem("", ""));

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from QualificationRoute where isActive = '1' order by Route";
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            routeQualification.Items.Add(new ListItem(theSqlDataReader["Route"].ToString(), theSqlDataReader["QualificationID"].ToString()));
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.RS.Close();


                }
                else
                {
                    adminpostalCities.Visible = false;
                    adminpostalSuburb.Visible = false;
                    adminphysicalCities.Visible = false;
                    adminphysicalSuburb.Visible = false;
                }


            }
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

        protected void submitNewApplication_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string oldPhoto = "";
            string oldPhotoID = "";

            if (!CheckBox1.Checked)
            {
                ERRMSGsub.Visible = true;
                ERRMSGsub.InnerHtml = "Please make sure you have checked the procedure of registration checkbox";
            }
            else if (!CheckBox2.Checked)
            {
                ERRMSGsub.Visible = true;
                ERRMSGsub.InnerHtml = "Please make sure you have checked the Code of Conduct checkbox";
            }
            else if (!CheckBox3.Checked)
            {
                ERRMSGsub.Visible = true;
                ERRMSGsub.InnerHtml = "Please make sure you have checked the Acknowledgement checkbox";
            }
            else if (!CheckBox4.Checked)
            {
                ERRMSGsub.Visible = true;
                ERRMSGsub.InnerHtml = "Please make sure you have checked the declaration checkbox";
            }
            else
            {
                if (userID == "" && ApplicationID == "")
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into NewApplications (RegistrationCard,DeliveryMethod,ProcedureRegistration,Designation,CodeConduct,Acknowledgement,Declaration,DeclarationName,DeclarationIDNumber) values (@RegistrationCard,@DeliveryMethod,@ProcedureRegistration,@Designation,@CodeConduct,@Acknowledgement,@Declaration,@DeclarationName,@DeclarationIDNumber); select scope_identity() as ApplicationID;";
                    DLdb.SQLST.Parameters.AddWithValue("RegistrationCard", DropDownList1.SelectedValue);
                    DLdb.SQLST.Parameters.AddWithValue("DeliveryMethod", DropDownList2.SelectedValue);
                    DLdb.SQLST.Parameters.AddWithValue("ProcedureRegistration", CheckBox1.Checked ? 1 : 0);
                    DLdb.SQLST.Parameters.AddWithValue("Designation", RadioButtonList1.SelectedValue);
                    DLdb.SQLST.Parameters.AddWithValue("CodeConduct", CheckBox2.Checked ? 1 : 0);
                    DLdb.SQLST.Parameters.AddWithValue("Acknowledgement", CheckBox3.Checked ? 1 : 0);
                    DLdb.SQLST.Parameters.AddWithValue("Declaration", CheckBox4.Checked ? 1 : 0);
                    DLdb.SQLST.Parameters.AddWithValue("DeclarationName", declareName.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("DeclarationIDNumber", declareIDnum.Text.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        ApplicationID = theSqlDataReader["ApplicationID"].ToString();
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

                    string gRegNo = "";
                    int RegNo = 0;
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select RegNo from Users order by RegNo desc";
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        gRegNo = theSqlDataReader["RegNo"].ToString();
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.RS.Close();

                    // SET REG NUMBER
                    gRegNo = gRegNo.Substring(0, 4);
                    RegNo = (Convert.ToInt32(gRegNo) + 1);

                    // CREATE PIN
                    Random random = new Random();
                    int PIN = random.Next(0, 9999);

                    if (FileUpload2.HasFiles)
                    {
                        oldPhoto = FileUpload2.FileName;
                        foreach (HttpPostedFile File in FileUpload2.PostedFiles)
                        {
                            string filename = File.FileName;
                            File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Photos/"), filename));
                        }
                    }

                    if (FileUpload1.HasFiles)
                    {
                        oldPhotoID = FileUpload1.FileName;
                        foreach (HttpPostedFile File in FileUpload1.PostedFiles)
                        {
                            string filename = "ID_" + File.FileName;
                            File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Photos/"), filename));
                        }
                    }

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into Users (RegNo,IDPhoto,password,company,Title,fName,lname,DateofBirth,IDNo,Photo,ResidentialStreet,ResidentialSuburb,ResidentialCity,PostalAddress,PostalSuburb,PostalCity,PostalCode,BusinessPhone,HomePhone,contact,Email,SecondEmail,Province,PostalProvince,Nationality,Language,Gender,Equity,DisabilityStatus,SocioeconomicStatus,CitizenResidentStatus,Alternate,ApplicationProcess,role,NoCOCpurchases) values" +
                        " (@RegNo,@IDPhoto,@password,@company,@Title,@fName,@lname,@DateofBirth,@IDNo,@Photo,@ResidentialStreet,@ResidentialSuburb,@ResidentialCity,@PostalAddress,@PostalSuburb,@PostalCity,@PostalCode,@BusinessPhone,@HomePhone,@contact,@Email,@SecondEmail,@Province,@PostalProvince,@Nationality,@Language,@Gender,@Equity,@DisabilityStatus,@SocioeconomicStatus,@CitizenResidentStatus,@Alternate,'New Application','Staff','10'); select scope_identity() as uid;";
                    DLdb.SQLST.Parameters.AddWithValue("company", CompanyID.SelectedValue);
                    DLdb.SQLST.Parameters.AddWithValue("Title", title.SelectedValue);
                    DLdb.SQLST.Parameters.AddWithValue("fName", Name.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("lname", Surname.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("DateofBirth", dob.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("IDNo", IDNumber.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Photo", FileUpload2.FileName);
                    DLdb.SQLST.Parameters.AddWithValue("IDPhoto", "ID_" + FileUpload1.FileName);
                    DLdb.SQLST.Parameters.AddWithValue("ResidentialStreet", PhysicalAddress.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("ResidentialSuburb", physicalSuburb.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("ResidentialCity", physicalCities.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Province", DropDownList5.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalAddress", postalAddress.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalSuburb", postalSuburb.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalCity", postalCities.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalCode", PostalCode.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalProvince", DropDownList4.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("BusinessPhone", WorkPhone.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("HomePhone", homePhone.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("contact", Mobile.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Email", emailAddress.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("SecondEmail", secondEmailAddress.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Nationality", Nationality.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Gender", Gender.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Language", HomeLanguage.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Equity", RacialStatus.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("DisabilityStatus", Disability.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("SocioeconomicStatus", EmploymentStatus.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("CitizenResidentStatus", ResidentStatus.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Alternate", AlternateID.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("RegNo", RegNo.ToString() + "/" + DateTime.Now.ToString("MM"));
                    DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(PIN.ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        userID = theSqlDataReader["uid"].ToString();
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
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update NewApplications set UserID=@UserID where ApplicationID=@ApplicationID";
                    DLdb.SQLST.Parameters.AddWithValue("ApplicationID", ApplicationID);
                    DLdb.SQLST.Parameters.AddWithValue("UserID", userID);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }
                else
                {
                    //update
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update NewApplications set RegistrationCard=@RegistrationCard,DeliveryMethod=@DeliveryMethod,ProcedureRegistration=@ProcedureRegistration,Designation=@Designation,CodeConduct=@CodeConduct,Acknowledgement=@Acknowledgement,Declaration=@Declaration,DeclarationName=@DeclarationName,DeclarationIDNumber=@DeclarationIDNumber where ApplicationID=@ApplicationID";
                    DLdb.SQLST.Parameters.AddWithValue("RegistrationCard", DropDownList1.SelectedValue);
                    DLdb.SQLST.Parameters.AddWithValue("DeliveryMethod", DropDownList2.SelectedValue);
                    DLdb.SQLST.Parameters.AddWithValue("ProcedureRegistration", CheckBox1.Checked ? 1 : 0);
                    DLdb.SQLST.Parameters.AddWithValue("Designation", RadioButtonList1.SelectedValue);
                    DLdb.SQLST.Parameters.AddWithValue("CodeConduct", CheckBox2.Checked ? 1 : 0);
                    DLdb.SQLST.Parameters.AddWithValue("Acknowledgement", CheckBox3.Checked ? 1 : 0);
                    DLdb.SQLST.Parameters.AddWithValue("Declaration", CheckBox4.Checked ? 1 : 0);
                    DLdb.SQLST.Parameters.AddWithValue("DeclarationName", declareName.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("DeclarationIDNumber", declareIDnum.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
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
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update Users set company=@company,Title=@Title,IDPhoto=@IDPhoto,fName=@fName,lname=@lname,DateofBirth=@DateofBirth,IDNo=@IDNo,Photo=@Photo,ResidentialStreet=@ResidentialStreet,ResidentialSuburb=@ResidentialSuburb,ResidentialCity=@ResidentialCity,PostalAddress=@PostalAddress,PostalSuburb=@PostalSuburb,PostalCity=@PostalCity,PostalCode=@PostalCode,BusinessPhone=@BusinessPhone,HomePhone=@HomePhone,contact=@contact,Email=@Email,SecondEmail=@SecondEmail,Province=@Province,PostalProvince=@PostalProvince,Nationality=@Nationality,Language=@Language,Gender=@Gender,Equity=@Equity,DisabilityStatus=@DisabilityStatus,SocioeconomicStatus=@SocioeconomicStatus,CitizenResidentStatus=@CitizenResidentStatus,Alternate=@Alternate where UserID=@UserID";
                    DLdb.SQLST.Parameters.AddWithValue("company", CompanyID.SelectedValue);
                    DLdb.SQLST.Parameters.AddWithValue("Title", title.SelectedValue);
                    DLdb.SQLST.Parameters.AddWithValue("fName", Name.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("lname", Surname.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("DateofBirth", dob.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("IDNo", IDNumber.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("ResidentialStreet", PhysicalAddress.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("ResidentialSuburb", physicalSuburb.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("ResidentialCity", physicalCities.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Province", DropDownList5.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalAddress", postalAddress.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalSuburb", postalSuburb.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalCity", postalCities.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalCode", PostalCode.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalProvince", DropDownList4.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("BusinessPhone", WorkPhone.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("HomePhone", homePhone.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("contact", Mobile.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Email", emailAddress.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("SecondEmail", secondEmailAddress.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Nationality", Nationality.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Gender", Gender.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Language", HomeLanguage.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Equity", RacialStatus.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("DisabilityStatus", Disability.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("SocioeconomicStatus", EmploymentStatus.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("CitizenResidentStatus", ResidentStatus.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Alternate", AlternateID.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("UserID", userID.ToString());
                    if (FileUpload2.HasFiles)
                    {
                        DLdb.SQLST.Parameters.AddWithValue("Photo", FileUpload2.FileName);
                        foreach (HttpPostedFile File in FileUpload2.PostedFiles)
                        {
                            string filename = File.FileName;
                            File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Photos/"), filename));
                        }
                    }
                    else
                    {
                        DLdb.SQLST.Parameters.AddWithValue("Photo", oldPhoto.ToString());
                    }
                    if (FileUpload1.HasFiles)
                    {
                        DLdb.SQLST.Parameters.AddWithValue("IDPhoto", "ID_" + FileUpload1.FileName);
                        foreach (HttpPostedFile File in FileUpload1.PostedFiles)
                        {
                            string filename = "ID_" + File.FileName;
                            File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Photos/"), filename));
                        }
                    }
                    else
                    {
                        DLdb.SQLST.Parameters.AddWithValue("IDPhoto", oldPhotoID.ToString());
                    }
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
                }
            }
            DLdb.DB_Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            submitNewApplication_Click(sender, e);

            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (FileUpload3.HasFiles)
            {
                foreach (HttpPostedFile File in FileUpload3.PostedFiles)
                {
                    string filename = File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Assessments/"), filename));
                }
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into NewApplicationCertificates (UserID,ApplicationID,Certificate) values (@UserID,@ApplicationID,@Certificate)";
            DLdb.SQLST.Parameters.AddWithValue("ApplicationID", ApplicationID);
            DLdb.SQLST.Parameters.AddWithValue("UserID", userID);
            DLdb.SQLST.Parameters.AddWithValue("Certificate", FileUpload3.FileName);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into NewApplicationComments (UserID,ApplicationID,Comment) values (@UserID,@ApplicationID,@Comment)";
            DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("UserID", userID);
            DLdb.SQLST.Parameters.AddWithValue("Comment", TextBox3.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            TextBox3.Text = "";
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (FileUpload4.HasFiles)
            {
                foreach (HttpPostedFile File in FileUpload4.PostedFiles)
                {
                    string filename = File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Assessments/"), filename));
                }
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into NewApplicationCertificates (UserID,ApplicationID,Certificate) values (@UserID,@ApplicationID,@Certificate)";
            DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("UserID", userID);
            DLdb.SQLST.Parameters.AddWithValue("Certificate", FileUpload4.FileName);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void saveAppDetails_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string uid = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from NewApplications where ApplicationID=@ApplicationID";
            DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                uid = theSqlDataReader["UserID"].ToString();
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update NewApplications set IDAttached=@IDAttached,Designation=@Designation,InitialEachPage=@InitialEachPage,QualificationVerified=@QualificationVerified,CompanyDetailsCorrect=@CompanyDetailsCorrect," +
                "ProofExperience=@ProofExperience,PhotoCorrect=@PhotoCorrect,InductionDone=@InductionDone,PaymentRecieved=@PaymentRecieved,isApproved=@isApproved,isRejected=@isRejected where ApplicationID=@ApplicationID";
            DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("IDAttached", IDAttached.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("InitialEachPage", InitialEachPage.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("QualificationVerified", QualificationVerified.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("CompanyDetailsCorrect", CompanyDetailsCorrect.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("ProofExperience", ProofExperience.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("PhotoCorrect", PhotoCorrect.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("InductionDone", InductionDone.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("PaymentRecieved", PaymentRecieved.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("isApproved", isApproved.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("isRejected", isRejected.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("Designation", RadioButtonList2.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

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

            string plumberName = "";
            string plumberEmail = "";
            string plumberContact = "";
            string plumberPassword = "";
            string UserID = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from NewApplications where ApplicationID=@ApplicationID";
            DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "select * from Users where UserID=@UserID";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                if (theSqlDataReader2.HasRows)
                {
                    theSqlDataReader2.Read();
                    plumberName = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                    plumberEmail = theSqlDataReader2["email"].ToString();
                    plumberContact = theSqlDataReader2["contact"].ToString();
                    UserID = theSqlDataReader2["UserID"].ToString();
                    plumberPassword = DLdb.Decrypt(theSqlDataReader2["Password"].ToString());
                }
                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            if (isApproved.Checked)
            {
                string EndDate = "";
                string StartDate = "";
                string points = "";
                string pointID = "";

                if (RadioButtonList2.SelectedValue == "Licensed Plumber")
                {
                    //DESIGNATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberDesignations (PlumberID,Designation,ApplicationID) VALUES (@PlumberID,@Designation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", uid.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
                    DLdb.SQLST2.Parameters.AddWithValue("Designation", "Licensed Plumber");
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();


                }
                else if (RadioButtonList2.SelectedValue == "Technical Operating Practitioner")
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberDesignations (PlumberID,Designation,ApplicationID) VALUES (@PlumberID,@Designation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", uid.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
                    DLdb.SQLST2.Parameters.AddWithValue("Designation", "Technical Operating Practitioner");
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                else if (RadioButtonList2.SelectedValue == "Technical Assistance Practitioner")
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberDesignations (PlumberID,Designation,ApplicationID) VALUES (@PlumberID,@Designation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", uid.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
                    DLdb.SQLST2.Parameters.AddWithValue("Designation", "Technical Assistance Practitioner");
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                else if (RadioButtonList2.SelectedValue == "Learner")
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberDesignations (PlumberID,Designation,ApplicationID) VALUES (@PlumberID,@Designation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", uid.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
                    DLdb.SQLST2.Parameters.AddWithValue("Designation", "Learner");
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }


                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from DesignationSpecialisationPoints where Item=@Item";
                DLdb.SQLST.Parameters.AddWithValue("Item", RadioButtonList2.SelectedValue.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    points = theSqlDataReader["Points"].ToString();
                    pointID = theSqlDataReader["pointID"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "INSERT INTO AssignedDesignationSpecialisationPoints (UserID,PointID,Points) VALUES (@UserID,@PointID,@Points)";
                DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PointID", pointID);
                DLdb.SQLST.Parameters.AddWithValue("Points", points);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from PlumberDesignations where PlumberID=@PlumberID";
                DLdb.SQLST.Parameters.AddWithValue("PlumberID", uid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM badges where Item=@Item";
                        DLdb.SQLST2.Parameters.AddWithValue("Item", theSqlDataReader["Designation"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                DLdb.RS3.Open();
                                DLdb.SQLST3.CommandText = "SELECT * FROM AssignedBadges where BadgeID=@BadgeID and UserID=@UserID";
                                DLdb.SQLST3.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("UserID", uid.ToString());
                                DLdb.SQLST3.CommandType = CommandType.Text;
                                DLdb.SQLST3.Connection = DLdb.RS3;
                                SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                                if (theSqlDataReader3.HasRows)
                                {
                                    while (theSqlDataReader3.Read())
                                    {

                                    }
                                }
                                else
                                {
                                    DLdb.RS4.Open();
                                    DLdb.SQLST4.CommandText = "insert into AssignedBadges (UserID,BadgeID) values (@UserID,@BadgeID)";
                                    DLdb.SQLST4.Parameters.AddWithValue("UserID", uid.ToString());
                                    DLdb.SQLST4.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                    DLdb.SQLST4.CommandType = CommandType.Text;
                                    DLdb.SQLST4.Connection = DLdb.RS4;
                                    SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                                    if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                                    DLdb.SQLST4.Parameters.RemoveAt(0);
                                    DLdb.SQLST4.Parameters.RemoveAt(0);
                                    DLdb.RS4.Close();

                                }

                                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                                DLdb.SQLST3.Parameters.RemoveAt(0);
                                DLdb.SQLST3.Parameters.RemoveAt(0);
                                DLdb.RS3.Close();
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

                DateTime dEndDate = DateTime.Now.AddMonths(12);
                EndDate = dEndDate.ToString();
                StartDate = DateTime.Now.ToShortDateString();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update users set ApplicationProcess='Approved',RegistrationStart=@RegistrationStart,RegistrationEnd=@RegistrationEnd where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", uid);
                DLdb.SQLST.Parameters.AddWithValue("RegistrationStart", StartDate);
                DLdb.SQLST.Parameters.AddWithValue("RegistrationEnd", EndDate);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into ActiveRegistrationLog (UserID,Description,DateActive,DateInactive) values (@UserID,@Description,@DateActive,@DateInactive)";
                DLdb.SQLST.Parameters.AddWithValue("UserID", uid);
                DLdb.SQLST.Parameters.AddWithValue("DateActive", StartDate);
                DLdb.SQLST.Parameters.AddWithValue("DateInactive", EndDate);
                DLdb.SQLST.Parameters.AddWithValue("Description", "New plumber registration approved");
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string HTMLSubject = "PIRB REGISTRATION ";
                string HTMLBody = "Good Day " + plumberName +
                    "<br/>" +
                    "Thank you for registering with the Plumbing Industry Registration Board.  Your application has been approved and your PIRB profile has been created." +
                    "<br/><br/>" +
                    "You are now part of the family of plumbing professionals and as part of this family it is expected among other things that you conduct yourself and your business in a professional manner which shall be seen by others as being honourable, transparent and fair.   It is further expected of you to proactively perform, work and act to promote plumbing practices that protect the health and safety of the community and the integrity of the water supply and wastewater systems.   " +
                      "<br/><br/>" +
                      "An important part of registration is your registration profile.  Your profile can be used to manage your profile, place orders, if applicable manage your certificates of compliance, refix requirements and much more.  To access your profile, visit www.pirb.co.za, select the Plumber Login option and enter use the following username & password below." +

                       "<br/><br/>" +
                      "User Name : " + plumberEmail +
                       "<br/>" +
                       "Password : " + plumberPassword+
                       "<br/><br/>" +
                       "We would urge that once you have login into your profile, change your password.   Going forward, please do not give your username and password to anyone as you will be held accountable for any activity related to your profile. " +
                        "<br/><br/>" +
                        "It is your responsibility to keep your information on the PIRB registrar current. All PIRB notices will be communicated to the contacted details as reflected on the PIRB registrar at the time of the sending out the notices" +
                         "<br/><br/>" +
                         "If applicable your registration card will be ready for collection/postage/courier shortly" +
                          "<br/><br/>" +
                          "We wish you luck in you PIRB registration career, and if you require any further information, please contact the PIRB on 0861 747 275 or email info@pirb.co.za." +
                    "<br/><br/>" +
                          "Best Regards" +
                    "<br/><br/>" +
                    "Lea Smith" +
                    "<br/>" +
                    "Chairman od the PIRB";
                DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumberEmail.ToString(), "");

                DLdb.sendSMS(uid.ToString(), plumberContact.ToString(), "Thank you for registering with the PIRB.  Your application has been approved and your profile has been created.  Details sent to " + plumberEmail);

            }

            if (isRejected.Checked)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update NewApplications set isActive='0' where ApplicationID=@ApplicationID";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update users set ApplicationProcess='Rejected' where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", uid);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                
                string HTMLSubject = "PIRB REGISTRATION ";
                string HTMLBody = "Good Day " + plumberName +
                    "<br/>" +
                    "Thank you for registering with the Plumbing Industry Registration Board.  Your application has unfortunately been rejected." +
                    "<br/><br/>" +
                    "For further information as to why your application was rejected, please contact the PIRB on 0861 747 275 or email info@pirb.co.za. " +
                      "<br/><br/>" +
                    "<br/><br/>" +
                    "Best Regards" +
                    "<br/><br/>" +
                    "Lea Smith" +
                    "<br/>" +
                    "Chairman od the PIRB";
                DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumberEmail.ToString(), "");

                DLdb.sendSMS(uid.ToString(), plumberContact.ToString(), "Thank you for registering with the PIRB.  Unfortunately, your application has been rejected.  Please contact the PIRB for further details.");

            }

            DLdb.DB_Close();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {

        }

        protected void btnQualificationAdd_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string PlumberID = "";
            string Name = "";
            string RegNo = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from NewApplications where ApplicationID=@ApplicationID";
            DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "select * from Users where UserID=@UserID";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                if (theSqlDataReader2.HasRows)
                {
                    theSqlDataReader2.Read();
                    PlumberID = theSqlDataReader2["UserID"].ToString();
                    RegNo = theSqlDataReader2["RegNo"].ToString();
                    Name = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                }
                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into PlumberQualifications (PlumberID,CourseYear,TrainingProvider,QualifiedThrough,CertificationNo,NameQualifiedPlumber,RegNoQualifiedPlumber) values (@PlumberID,@CourseYear,@TrainingProvider,@QualifiedThrough,@CertificationNo,@NameQualifiedPlumber,@RegNoQualifiedPlumber)";
            DLdb.SQLST.Parameters.AddWithValue("PlumberID", PlumberID.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CourseYear", certificateYear.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TrainingProvider", TrainingProvider.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("QualifiedThrough", routeQualification.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CertificationNo", CertificationNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("NameQualifiedPlumber", Name.ToString());
            DLdb.SQLST.Parameters.AddWithValue("RegNoQualifiedPlumber", RegNo.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
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
    }
}