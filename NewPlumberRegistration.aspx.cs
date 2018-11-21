using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InspectIT
{
    public partial class NewPlumberRegistration : System.Web.UI.Page
    { 
        public string userID = null;
        public string ApplicationID = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            //listOfAttachments.InnerHtml = listAttach;

            //if (Session["IIT_tempUidRegister"] == null)
            //{
            //    ApplicationID = "";
            //    userID = "";
            //}
            //else
            //{
            //    userID = Session["IIT_tempUidRegister"].ToString();
            //    ApplicationID = Session["IIT_tempAppidRegister"].ToString();
            //}
            string listAttach="";
            Session["IIT_subRegistration"] = "false";

            if (Session["IIT_NewAppTemp"] == null)
            {
                Session["IIT_NewAppTemp"] = DLdb.CreateNumber(9);
            }
            else
            {
                Session["IIT_NewAppTemp"] = Session["IIT_NewAppTemp"];
            }

            string newAppID = "";
            string ComPID = "";
            if (Session["IIT_NewApplistDocs"] != null)
            {
                listOfAttachments.InnerHtml = Session["IIT_NewApplistDocs"].ToString();
            }

            

            if (Request.QueryString["npid"] != null)
            {
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
                        DropDownList1.SelectedValue = theSqlDataReader["RegistrationCard"].ToString();
                        DropDownList2.SelectedValue = theSqlDataReader["DeliveryMethod"].ToString();
                        RadioButtonList1.SelectedValue = theSqlDataReader["Designation"].ToString();
                        declareName.Text= theSqlDataReader["DeclarationName"].ToString();
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
                                if (theSqlDataReader2["CitizenResidentStatus"].ToString()== "South Africa")
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
            }
            else
            {
                adminpostalCities.Visible = false;
                adminpostalSuburb.Visible = false;
                adminphysicalCities.Visible = false;
                adminphysicalSuburb.Visible = false;
            }
            

            if (!IsPostBack)
            {
            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "Select * from NewApplicationCertificates where TempID=@TempID";
            //DLdb.SQLST.Parameters.AddWithValue("TempID", Session["IIT_NewAppTemp"].ToString());
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //if (theSqlDataReader.HasRows)
            //{
            //    while (theSqlDataReader.Read())
            //    {
            //        listAttach += "<a href='Assessments/" + theSqlDataReader["Certificate"].ToString() + "' download>" + theSqlDataReader["Certificate"].ToString() + "</a><br/>";
                    
            //    }
            //}

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.RS.Close();

            //listOfAttachments.InnerHtml = listAttach;

                ERRMSGsub.Visible = false;
                CompanyID.Items.Clear();
                CompanyID.Items.Add(new ListItem("", ""));

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from Companies where isActive = '1' order by CompanyName";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
               SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        CompanyID.Items.Add(new ListItem(theSqlDataReader["CompanyName"].ToString(), theSqlDataReader["CompanyID"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();
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

           // Session["IIT_subRegistration"] = "true";
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
                if (Session["IIT_tempAppidRegister"] == null)
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
                        Session["IIT_tempAppidRegister"] = ApplicationID;
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
                    DLdb.SQLST.CommandText = "insert into Users (RegistrationCard,DeliveryMethod,RegNo,IDPhoto,password,company,Title,fName,lname,DateofBirth,IDNo,Photo,ResidentialStreet,ResidentialSuburb,ResidentialCity,PostalAddress,PostalSuburb,PostalCity,PostalCode,BusinessPhone,HomePhone,contact,Email,SecondEmail,Province,PostalProvince,Nationality,Language,Gender,Equity,DisabilityStatus,SocioeconomicStatus,CitizenResidentStatus,Alternate,ApplicationProcess,role,NoCOCpurchases) values" +
                        " (@RegistrationCard,@DeliveryMethod,@RegNo,@IDPhoto,@password,@company,@Title,@fName,@lname,@DateofBirth,@IDNo,@Photo,@ResidentialStreet,@ResidentialSuburb,@ResidentialCity,@PostalAddress,@PostalSuburb,@PostalCity,@PostalCode,@BusinessPhone,@HomePhone,@contact,@Email,@SecondEmail,@Province,@PostalProvince,@Nationality,@Language,@Gender,@Equity,@DisabilityStatus,@SocioeconomicStatus,@CitizenResidentStatus,@Alternate,'New Application','Staff','10'); select scope_identity() as uid;";
                    DLdb.SQLST.Parameters.AddWithValue("RegistrationCard", DropDownList1.SelectedValue);
                    DLdb.SQLST.Parameters.AddWithValue("DeliveryMethod", DropDownList2.SelectedValue);
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
                    if (DropDownList3.SelectedValue == "1")
                    {
                        DLdb.SQLST.Parameters.AddWithValue("CitizenResidentStatus", "South Africa");

                    }
                    else
                    {
                        DLdb.SQLST.Parameters.AddWithValue("CitizenResidentStatus", ResidentStatus.SelectedValue.ToString());
                    }
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
                        Session["IIT_tempUidRegister"] = userID;
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

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update Users set pirbid=@pirbid where userID=@userID";
                    DLdb.SQLST.Parameters.AddWithValue("userID", userID);
                    DLdb.SQLST.Parameters.AddWithValue("pirbid", userID);
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
                    DLdb.SQLST.Parameters.AddWithValue("ApplicationID", Session["IIT_tempAppidRegister"].ToString());
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
                    if (DropDownList3.SelectedValue == "1")
                    {
                        DLdb.SQLST.Parameters.AddWithValue("CitizenResidentStatus", "South Africa");

                    }
                    else
                    {
                        DLdb.SQLST.Parameters.AddWithValue("CitizenResidentStatus", ResidentStatus.SelectedValue.ToString());
                    }
                    DLdb.SQLST.Parameters.AddWithValue("Alternate", AlternateID.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_tempUidRegister"].ToString());
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

                //DLdb.RS2.Open();
                //DLdb.SQLST2.CommandText = "update NewApplicationCertificates set UserID=@UserID,ApplicationID=@ApplicationID where TempID=@TempID";
                //DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", Session["IIT_tempAppidRegister"].ToString());
                //DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_tempUidRegister"].ToString());
                //DLdb.SQLST2.Parameters.AddWithValue("TempID", Session["IIT_NewAppTemp"].ToString());
                //DLdb.SQLST2.CommandType = CommandType.Text;
                //DLdb.SQLST2.Connection = DLdb.RS2;
                //SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                //if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                //DLdb.SQLST2.Parameters.RemoveAt(0);
                //DLdb.SQLST2.Parameters.RemoveAt(0);
                //DLdb.RS2.Close();
            }
            DLdb.DB_Close();

            if (Session["IIT_subRegistration"].ToString() == "true")
            {
                Response.Redirect("Default");
            }

        }
          
        protected void Button1_Click(object sender, EventArgs e)
        {
            //submitNewApplication_Click(sender, e);

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
            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "insert into NewApplicationCertificates (TempID,Certificate) values (@TempID,@Certificate)";
            //DLdb.SQLST.Parameters.AddWithValue("TempID", Session["IIT_NewAppTemp"].ToString());
            //DLdb.SQLST.Parameters.AddWithValue("Certificate", FileUpload3.FileName);
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.RS.Close();
           // listAttach += "<a href='Assessments/" + FileUpload3.FileName + "' download>" + FileUpload3.FileName + "</a><br/>";

            string oldPhoto = "";
            string oldPhotoID = "";
            if (Session["IIT_tempAppidRegister"] == null)
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
                    Session["IIT_tempAppidRegister"] = ApplicationID;
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
                DLdb.SQLST.CommandText = "insert into Users (RegistrationCard,DeliveryMethod,RegNo,IDPhoto,password,company,Title,fName,lname,DateofBirth,IDNo,Photo,ResidentialStreet,ResidentialSuburb,ResidentialCity,PostalAddress,PostalSuburb,PostalCity,PostalCode,BusinessPhone,HomePhone,contact,Email,SecondEmail,Province,PostalProvince,Nationality,Language,Gender,Equity,DisabilityStatus,SocioeconomicStatus,CitizenResidentStatus,Alternate,ApplicationProcess,role,NoCOCpurchases) values" +
                    " (@RegistrationCard,@DeliveryMethod,@RegNo,@IDPhoto,@password,@company,@Title,@fName,@lname,@DateofBirth,@IDNo,@Photo,@ResidentialStreet,@ResidentialSuburb,@ResidentialCity,@PostalAddress,@PostalSuburb,@PostalCity,@PostalCode,@BusinessPhone,@HomePhone,@contact,@Email,@SecondEmail,@Province,@PostalProvince,@Nationality,@Language,@Gender,@Equity,@DisabilityStatus,@SocioeconomicStatus,@CitizenResidentStatus,@Alternate,'New Application','Staff','10'); select scope_identity() as uid;";
                DLdb.SQLST.Parameters.AddWithValue("RegistrationCard", DropDownList1.SelectedValue);
                DLdb.SQLST.Parameters.AddWithValue("DeliveryMethod", DropDownList2.SelectedValue);
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
                if (DropDownList3.SelectedValue == "1")
                {
                    DLdb.SQLST.Parameters.AddWithValue("CitizenResidentStatus", "South Africa");

                }
                else
                {
                    DLdb.SQLST.Parameters.AddWithValue("CitizenResidentStatus", ResidentStatus.SelectedValue.ToString());
                }
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
                    Session["IIT_tempUidRegister"] = userID;
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

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update Users set pirbid=@pirbid where userID=@userID";
                DLdb.SQLST.Parameters.AddWithValue("userID", userID);
                DLdb.SQLST.Parameters.AddWithValue("pirbid", userID);
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
                DLdb.SQLST.Parameters.AddWithValue("ApplicationID", Session["IIT_tempAppidRegister"].ToString());
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
                if (DropDownList3.SelectedValue == "1")
                {
                    DLdb.SQLST.Parameters.AddWithValue("CitizenResidentStatus", "South Africa");

                }
                else
                {
                    DLdb.SQLST.Parameters.AddWithValue("CitizenResidentStatus", ResidentStatus.SelectedValue.ToString());
                }
                DLdb.SQLST.Parameters.AddWithValue("Alternate", AlternateID.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_tempUidRegister"].ToString());
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

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "insert into NewApplicationCertificates (UserID,ApplicationID,Certificate,TempID) values (@UserID,@ApplicationID,@Certificate,@TempID)";
            DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", Session["IIT_tempAppidRegister"].ToString());
            DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_tempUidRegister"].ToString());
            DLdb.SQLST2.Parameters.AddWithValue("Certificate", FileUpload3.FileName);
            DLdb.SQLST2.Parameters.AddWithValue("TempID", Session["IIT_NewAppTemp"].ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            Session["IIT_NewApplistDocs"] += "<a href='Assessments/" + FileUpload3.FileName + "' download>" + FileUpload3.FileName + "</a><br/>";
            listOfAttachments.InnerHtml = Session["IIT_NewApplistDocs"].ToString();
            DLdb.DB_Close();
        }
        
    }
}