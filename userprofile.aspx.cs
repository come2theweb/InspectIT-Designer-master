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
using System.IO;
using GoogleMaps.LocationServices;

namespace InspectIT
{
    public partial class userprofile : System.Web.UI.Page
    {
        public Boolean canSavePlumber = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            //// CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }
            otpChangePass.Visible = false;
            //// ADMIN CHECK
            if (Session["IIT_Role"].ToString() == "Administrator")
            {
                Admin.Visible = true;
                Supplier.Visible = false;
                Inspector.Visible = false;
                plumber.Visible = false;
                if (!IsPostBack)
                {
                    DLdb.DB_Connect();
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID=@UserID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();


                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        // General Details
                        adminName.Text = theSqlDataReader["fname"].ToString();
                        password.Text = theSqlDataReader["password"].ToString();
                        role.Text = theSqlDataReader["role"].ToString();
                        adminEmail.Text = theSqlDataReader["email"].ToString();
                        string active = theSqlDataReader["isActive"].ToString();
                        if (active == "True")
                        {
                            isActive.Checked = true;
                        }
                    }


                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                    DLdb.DB_Close();
                }
            }
            else if (Session["IIT_Role"].ToString() == "Inspector")
            {
                Admin.Visible = false;
                Supplier.Visible = false;
                Inspector.Visible = true;
                plumber.Visible = false;
                if (!IsPostBack)
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM Auditor where UserID=@UserID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        // General Details
                        regNo.Text = theSqlDataReader["regNo"].ToString();
                        InspecName.Text = theSqlDataReader["fName"].ToString();
                        InspecSurname.Text = theSqlDataReader["lName"].ToString();
                        idNo.Text = theSqlDataReader["idNo"].ToString();
                        string isActive = theSqlDataReader["isActive"].ToString();
                        if (isActive == "True")
                        {
                            InspecActive.Checked = true;
                        }
                        startInactiveDate.Text = theSqlDataReader["startInactiveDate"].ToString();
                        endInactiveDate.Text = theSqlDataReader["endInactiveDate"].ToString();
                        phoneWork.Text = theSqlDataReader["phoneWork"].ToString();
                        phoneHome.Text = theSqlDataReader["phoneHome"].ToString();
                        phoneMobile.Text = theSqlDataReader["phoneMobile"].ToString();
                        fax.Text = theSqlDataReader["fax"].ToString();
                        InspecEmail.Text = theSqlDataReader["email"].ToString();
                        pin.Text = theSqlDataReader["pin"].ToString();
                        currentImg.Text = theSqlDataReader["photoFile"].ToString();
                        TextBox7.Text = theSqlDataReader["CompanyLogo"].ToString();
                        Image1.ImageUrl = "AuditorImgs" + theSqlDataReader["photoFile"].ToString();
                        Image2.ImageUrl = "AuditorImgs" + theSqlDataReader["CompanyLogo"].ToString();
                        addressLine1.Text = theSqlDataReader["addressLine1"].ToString();
                        addressLine2.Text = theSqlDataReader["addressLine2"].ToString();
                        province.Text = theSqlDataReader["province"].ToString();
                        auditCity.Text = theSqlDataReader["city"].ToString();
                        auditSuburb.Text = theSqlDataReader["Suburb"].ToString();
                        areaCode.Text = theSqlDataReader["areaCode"].ToString();
                        postalAddress.Text = theSqlDataReader["postalAddress"].ToString();
                        postalCity.Text = theSqlDataReader["postalCity"].ToString();
                        postalCode.Text = theSqlDataReader["postalCode"].ToString();
                        companyRegNo.Text = theSqlDataReader["companyRegNo"].ToString();
                        vatRegNo.Text = theSqlDataReader["vatRegNo"].ToString();
                        pastelAccount.Text = theSqlDataReader["pastelAccount"].ToString();
                        invoiceEmail.Text = theSqlDataReader["invoiceEmail"].ToString();
                        string isActive1 = theSqlDataReader["stopPayments"].ToString();
                        if (isActive1 == "True")
                        {
                            stopPayments.Checked = true;
                        }
                        bankName.Text = DLdb.Decrypt(theSqlDataReader["bankName"].ToString());
                        accName.Text = DLdb.Decrypt(theSqlDataReader["accName"].ToString());
                        accNumber.Text = DLdb.Decrypt(theSqlDataReader["accNumber"].ToString());
                        branchCode.Text = DLdb.Decrypt(theSqlDataReader["branchCode"].ToString());
                        accType.Text = DLdb.Decrypt(theSqlDataReader["accType"].ToString());
                        Range.Text = theSqlDataReader["Range"].ToString();
                    }


                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);

                    DLdb.RS.Close();
                    DLdb.DB_Close();
                }
            }
            else if (Session["IIT_Role"].ToString() == "Supplier")
            {
                Admin.Visible = false;
                Supplier.Visible = true;
                Inspector.Visible = false;
                plumber.Visible = false;
                if (!IsPostBack)
                {
                    DLdb.DB_Connect();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM Suppliers where UserID=@UserID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        // General Details
                        SupplierName.Text = theSqlDataReader["SupplierName"].ToString();
                        SupplierRegNo.Text = theSqlDataReader["SupplierRegNo"].ToString();
                        SupplierWebsite.Text = theSqlDataReader["SupplierWebsite"].ToString();
                        SupplierEmail.Text = theSqlDataReader["SupplierEmail"].ToString();
                        SupplierContactNo.Text = theSqlDataReader["SupplierContactNo"].ToString();
                        SupplierAddressLine1.Text = theSqlDataReader["AddressLine1"].ToString();
                        SupplierAddressLine2.Text = theSqlDataReader["AddressLine2"].ToString();
                        SupplierProvince.Text = theSqlDataReader["Province"].ToString();
                        SupplierCitySuburb.Text = theSqlDataReader["CitySuburb"].ToString();
                        SupplierAreaCode.Text = theSqlDataReader["AreaCode"].ToString();
                        SupplierPostalAddress.Text = theSqlDataReader["PostalAddress"].ToString();
                        SupplierCity.Text = theSqlDataReader["PostalCity"].ToString();
                        SupplierPostalCode.Text = theSqlDataReader["PostalCode"].ToString();

                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID=@UserID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        // General Details
                        supPassword.Text = DLdb.Decrypt(theSqlDataReader["Password"].ToString());
                        supPasswordConfirm.Text = DLdb.Decrypt(theSqlDataReader["Password"].ToString());

                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.DB_Close();
                }
            }
            else if (Session["IIT_Role"].ToString() == "Staff")
            {
                Admin.Visible = false;
                Supplier.Visible = false;
                Inspector.Visible = false;
                plumber.Visible = true;
                //btnUpdatePlumber.Visible = false;

                if (!IsPostBack)
                {
                    DLdb.DB_Connect();
                    string pirbid = "";

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID=@UserID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        regnoplumber.Text = theSqlDataReader["regno"].ToString();
                        Name.Text = theSqlDataReader["fname"].ToString();
                        Surname.Text = theSqlDataReader["lname"].ToString();
                        IDNum.Text = theSqlDataReader["IDNo"].ToString();
                        postalAddressPlumber.Text = theSqlDataReader["postalAddress"].ToString();
                        postalCityPlumber.Text = theSqlDataReader["postalCity"].ToString();
                        postalCodePlumber.Text = theSqlDataReader["postalCode"].ToString();
                        resstreetaddyplumber.Text = theSqlDataReader["ResidentialStreet"].ToString();
                        ressuburbplumber.Text = theSqlDataReader["ResidentialSuburb"].ToString();
                        rescityplumber.Text = theSqlDataReader["ResidentialCity"].ToString();
                        respostalcodeplumber.Text = theSqlDataReader["ResidentialCode"].ToString();
                        resprovinceplumber.Text = theSqlDataReader["Province"].ToString();
                        plumbermodilenum.Text = theSqlDataReader["contact"].ToString();
                        plumberemail.Text = theSqlDataReader["email"].ToString();
                        notice.Text = theSqlDataReader["notice"].ToString();
                        plumberPassword.Text = DLdb.Decrypt(theSqlDataReader["password"].ToString());
                        plumberinscompany.Text = theSqlDataReader["InsuranceCompany"].ToString();
                        plumberpolicynumber.Text = theSqlDataReader["InsurancePolicyNo"].ToString();
                        plumberpolicyholder.Text = theSqlDataReader["InsurancePolicyHolder"].ToString();
                        plumberperiodinsfrom.Text = theSqlDataReader["InsuranceStartDate"].ToString();
                        plumberperiodinsto.Text = theSqlDataReader["InsuranceEndDate"].ToString();
                        pirbid = theSqlDataReader["PIRBID"].ToString();
                        plumberSignature.ImageUrl = "http://197.242.82.242/pirbreg/signatures/" + theSqlDataReader["Signature"].ToString();
                        nonloggedcocallocated.Text = theSqlDataReader["NoCOCPurchases"].ToString();
                        if (theSqlDataReader["CompanyIsBillingInfo"].ToString() == "True")
                        {
                            CheckBox1.Checked = true;

                        }

                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM Companies where CompanyID=@CompanyID";
                        DLdb.SQLST2.Parameters.AddWithValue("CompanyID", theSqlDataReader["company"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            theSqlDataReader2.Read();
                            plumberempcompany.Text = theSqlDataReader2["CompanyName"].ToString();
                            plumberempcontact.Text = theSqlDataReader2["CompanyContactNo"].ToString();
                            plumberempemail.Text = theSqlDataReader2["CompanyEmail"].ToString();

                            TextBox1.Text = theSqlDataReader2["CompanyName"].ToString();
                            TextBox4.Text = theSqlDataReader2["CompanyContactNo"].ToString();
                            TextBox3.Text = theSqlDataReader2["CompanyEmail"].ToString();
                            TextBox2.Text = theSqlDataReader2["CompanyRegNo"].ToString();
                            TextBox6.Text = theSqlDataReader2["VatNo"].ToString();
                            TextBox5.Text = theSqlDataReader2["AddressLine1"].ToString() + ", " + theSqlDataReader2["City"].ToString() + ", " + theSqlDataReader2["Suburb"].ToString() + ", " + theSqlDataReader2["Province"].ToString();
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

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
                    //DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
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
                    

                    DLdb.DB_Close();
                }
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

            }
        }


        protected void btnUpdateInspector_Click(object sender, EventArgs e)
        {
            string ImgOld = currentImg.Text.ToString();
            string ImgOldCL = TextBox7.Text.ToString();

            if (photoFile.HasFiles)
            {
                ImgOld = photoFile.FileName;
                foreach (HttpPostedFile File in photoFile.PostedFiles)
                {
                    string filename = File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AuditorImgs/"), filename));
                }
            }

            if (FileUpload1.HasFiles)
            {
                ImgOldCL = FileUpload1.FileName;
                foreach (HttpPostedFile File in FileUpload1.PostedFiles)
                {
                    string filename = File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AuditorImgs/"), filename));
                }
            }

          

            Global DLdb = new Global();
            DLdb.DB_Connect();
            //string addressToLongLat = addressLine1.Text.ToString() + ", " + citySuburb.Text.ToString() + ", " + province.Text.ToString();
            
            //var address = addressToLongLat;
            //var locationService = new GoogleLocationService();
            //var point = locationService.GetLatLongFromAddress(address);
            //var latitude = point.Latitude;
            //var longitude = point.Longitude;
            
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "UPDATE Auditor SET regNo=@regNo, fName=@fName, lName=@lName,ComapnyLogo=@ComapnyLogo," +
                                        "idNo=@idNo, isActive=@isActive, startInactiveDate=@startInactiveDate, endInactiveDate=@endInactiveDate," +
                                        "phoneWork=@phoneWork, phoneHome=@phoneHome, phoneMobile=@phoneMobile, fax=@fax, email=@email, pin=@pin, photoFile=@photoFile," +
                                        "addressLine1=@addressLine1, addressLine2=@addressLine2, province=@province, citySuburb=@citySuburb, areaCode=@areaCode," +
                                        "postalAddress=@postalAddress, postalCity=@postalCity, postalCode=@postalCode, companyRegNo=@companyRegNo, vatRegNo=@vatRegNo," +
                                        "pastelAccount=@pastelAccount, invoiceEmail=@invoiceEmail, stopPayments=@stopPayments, bankName=@bankName, accName=@accName, accNumber=@accNumber," +
                                        "branchCode=@branchCode, accType=@accType,lat=@lat,lng=@lng,Range=@Range " +
                                        "WHERE UserID=@UserID";

            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("regNo", regNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("fName", InspecName.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("lName", InspecSurname.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("idNo", idNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("isActive", this.InspecActive.Checked ? "1" : "0");
            DLdb.SQLST.Parameters.AddWithValue("startInactiveDate", startInactiveDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("endInactiveDate", endInactiveDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("phoneWork", phoneWork.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("phoneHome", phoneHome.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("phoneMobile", phoneMobile.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("fax", fax.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("email", InspecEmail.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("pin", pin.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("photoFile", ImgOld);
            DLdb.SQLST.Parameters.AddWithValue("ComapnyLogo", ImgOldCL);
            DLdb.SQLST.Parameters.AddWithValue("addressLine1", addressLine1.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("addressLine2", addressLine2.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("province", province.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Suburb", auditSuburb.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("city", auditCity.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("areaCode", areaCode.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("postalAddress", postalAddress.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("postalCity", postalCity.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("postalCode", postalCode.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("companyRegNo", companyRegNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("vatRegNo", vatRegNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("pastelAccount", pastelAccount.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("invoiceEmail", invoiceEmail.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("stopPayments", this.stopPayments.Checked ? "1" : "0");
            DLdb.SQLST.Parameters.AddWithValue("bankName", DLdb.Encrypt(bankName.Text.ToString()));
            DLdb.SQLST.Parameters.AddWithValue("accName", DLdb.Encrypt(accName.Text.ToString()));
            DLdb.SQLST.Parameters.AddWithValue("accNumber", DLdb.Encrypt(accNumber.Text.ToString()));
            DLdb.SQLST.Parameters.AddWithValue("branchCode", DLdb.Encrypt(branchCode.Text.ToString()));
            DLdb.SQLST.Parameters.AddWithValue("accType", DLdb.Encrypt(accType.SelectedValue));
            DLdb.SQLST.Parameters.AddWithValue("lat", "");
            DLdb.SQLST.Parameters.AddWithValue("lng", "");
            DLdb.SQLST.Parameters.AddWithValue("Range", Range.Text.ToString());
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

            DLdb.DB_Close();
            Response.Redirect("userprofile.aspx?msg=You Profile Has Been Updated!");
        }

        protected void btnUpdateSupplier_Click(object sender, EventArgs e)
        {
            if (supPassword.Text.ToString() == "")
            {
                
                Global DLdb = new Global();
                DLdb.DB_Connect();
                DLdb.RS.Open();

                DLdb.SQLST.CommandText = "UPDATE Suppliers SET SupplierName=@SupplierName, SupplierRegNo=@SupplierRegNo, SupplierWebsite=@SupplierWebsite," +
                    "SupplierEmail=@SupplierEmail, SupplierContactNo=@SupplierContactNo, AddressLine1=@AddressLine1, AddressLine2=@AddressLine2," +
                    "Province=@Province, CitySuburb=@CitySuburb, AreaCode=@AreaCode, PostalAddress=@PostalAddress, PostalCity=@PostalCity, PostalCode=@PostalCode WHERE UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());

                DLdb.SQLST.Parameters.AddWithValue("SupplierName", SupplierName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SupplierRegNo", SupplierRegNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SupplierWebsite", SupplierWebsite.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SupplierEmail", SupplierEmail.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SupplierContactNo", SupplierContactNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressLine1", SupplierAddressLine1.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressLine2", SupplierAddressLine2.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Province", SupplierProvince.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CitySuburb", SupplierCitySuburb.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AreaCode", SupplierAreaCode.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PostalAddress", SupplierPostalAddress.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PostalCity", SupplierCity.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PostalCode", SupplierPostalCode.Text.ToString());


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
                DLdb.RS.Close();
                
                DLdb.DB_Close();
                Response.Redirect("userprofile.aspx?msg=You Profile Has Been Updated!");
                
            } else
            {
                if (supPassword.Text.ToString() == supPasswordConfirm.Text.ToString())
                {
                    Global DLdb = new Global();
                    DLdb.DB_Connect();
                    DLdb.RS.Open();

                    DLdb.SQLST.CommandText = "UPDATE Suppliers SET SupplierName=@SupplierName, SupplierRegNo=@SupplierRegNo, SupplierWebsite=@SupplierWebsite," +
                        "SupplierEmail=@SupplierEmail, SupplierContactNo=@SupplierContactNo, AddressLine1=@AddressLine1, AddressLine2=@AddressLine2," +
                        "Province=@Province, CitySuburb=@CitySuburb, AreaCode=@AreaCode, PostalAddress=@PostalAddress, PostalCity=@PostalCity, PostalCode=@PostalCode WHERE UserID=@UserID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());

                    DLdb.SQLST.Parameters.AddWithValue("SupplierName", SupplierName.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("SupplierRegNo", SupplierRegNo.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("SupplierWebsite", SupplierWebsite.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("SupplierEmail", SupplierEmail.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("SupplierContactNo", SupplierContactNo.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("AddressLine1", SupplierAddressLine1.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("AddressLine2", SupplierAddressLine2.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Province", SupplierProvince.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("CitySuburb", SupplierCitySuburb.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("AreaCode", SupplierAreaCode.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalAddress", SupplierPostalAddress.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalCity", SupplierCity.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalCode", SupplierPostalCode.Text.ToString());


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
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update users set password = @password where UserID = @UserID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(supPassword.Text.ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.DB_Close();
                    Response.Redirect("userprofile.aspx?msg=Your ptofile has been updated");
                }
                else
                {
                    errormsg.InnerHtml = "Your password does not match";
                    errormsg.Visible = true;
                }
            }
        }

        protected void btnUpdateAdmin_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "UPDATE Users SET fname=@fname, password=@password, role=@role, email=@email, isActive=@isActive WHERE UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("fname", adminName.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(password.Text.ToString()));
            DLdb.SQLST.Parameters.AddWithValue("role", role.SelectedValue);
            DLdb.SQLST.Parameters.AddWithValue("email", adminEmail.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("isActive", this.isActive.Checked ? "1" : "0");
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
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("userprofile.aspx?msg=Your ptofile has been updated");
        }

        protected void btnUpdatePlumber_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            
            string pass = "";
            string smsNumber = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users WHERE UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                pass = theSqlDataReader["Password"].ToString();
                smsNumber = theSqlDataReader["contact"].ToString();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            //if (plumberPassword.Text.ToString() !="")
            //{
            //    plumber.Visible = false;
            //    otpChangePass.Visible = true;
            //    canSavePlumber = false;
            //    string OTPCode = DLdb.CreateNumber(5);
            //    Session["IIT_OTPCodeChangePassPlumber"] = OTPCode;
            //    DLdb.sendSMS(Session["IIT_UID"].ToString(), smsNumber.ToString(), "Inspect-It: You've requested to change your password. OTP Code: " + OTPCode);

            //}
            //else
            //{
            canSavePlumber = true;
            //}

            if (canSavePlumber == true)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "UPDATE Users SET CompanyIsBillingInfo=@CompanyIsBillingInfo,InsuranceEndDate=@InsuranceEndDate,InsuranceStartDate=@InsuranceStartDate,InsuranceCompany=@InsuranceCompany,InsurancePolicyNo=@InsurancePolicyNo,InsurancePolicyHolder=@InsurancePolicyHolder WHERE UserID=@UserID";
                //DLdb.SQLST.CommandText = "UPDATE Users SET PostalAddress=@PostalAddress,CompanyIsBillingInfo=@CompanyIsBillingInfo,PostalCity=@PostalCity,PostalCode=@PostalCode,ResidentialStreet=@ResidentialStreet," +
                //    "ResidentialSuburb=@ResidentialSuburb,ResidentialCity=@ResidentialCity,InsuranceStartDate=@InsuranceStartDate,InsuranceEndDate=@InsuranceEndDate,Province=@Province,InsurancePolicyHolder=@InsurancePolicyHolder,InsurancePolicyNo=@InsurancePolicyNo,InsuranceCompany=@InsuranceCompany,ResidentialCode=@ResidentialCode,email=@email,contact=@contact WHERE UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                //DLdb.SQLST.Parameters.AddWithValue("email", plumberemail.Text.ToString());
                //DLdb.SQLST.Parameters.AddWithValue("PostalAddress", postalAddressPlumber.Text.ToString());
                //DLdb.SQLST.Parameters.AddWithValue("PostalCity", postalCityPlumber.Text.ToString());
                //DLdb.SQLST.Parameters.AddWithValue("PostalCode", postalCodePlumber.Text.ToString());
                //DLdb.SQLST.Parameters.AddWithValue("ResidentialStreet", resstreetaddyplumber.Text.ToString());
                //DLdb.SQLST.Parameters.AddWithValue("ResidentialSuburb", ressuburbplumber.Text.ToString());
                //DLdb.SQLST.Parameters.AddWithValue("ResidentialCity", rescityplumber.Text.ToString());
                //DLdb.SQLST.Parameters.AddWithValue("Province", resprovinceplumber.Text.ToString());
                //DLdb.SQLST.Parameters.AddWithValue("ResidentialCode", respostalcodeplumber.Text.ToString());
                //DLdb.SQLST.Parameters.AddWithValue("contact", plumbermodilenum.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("InsuranceCompany", plumberinscompany.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("InsurancePolicyNo", plumberpolicynumber.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("InsurancePolicyHolder", plumberpolicyholder.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("InsuranceStartDate", plumberperiodinsfrom.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("InsuranceEndDate", plumberperiodinsto.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CompanyIsBillingInfo", CheckBox1.Checked);
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
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.DB_Close();
                Response.Redirect("userprofile.aspx?msg=Your profile has been updated");
            }
            
        }

        protected void SubCoc_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (otpchangePasseword.Text.ToString() == Session["IIT_OTPCodeChangePassPlumber"].ToString())
            {
                canSavePlumber = true;
            }
            else
            {
                plumber.Visible = false;
                otpChangePass.Visible = true;
                canSavePlumber = false;
                errormsg.Visible = true;
                errormsg.InnerHtml = "OTP is incorrect";
            }

            if (canSavePlumber == true)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "UPDATE Users SET PostalAddress=@PostalAddress,Password=@Password,CompanyIsBillingInfo=@CompanyIsBillingInfo,PostalCity=@PostalCity,PostalCode=@PostalCode,ResidentialStreet=@ResidentialStreet," +
                    "ResidentialSuburb=@ResidentialSuburb,ResidentialCity=@ResidentialCity,InsuranceStartDate=@InsuranceStartDate,InsuranceEndDate=@InsuranceEndDate,Province=@Province,InsurancePolicyHolder=@InsurancePolicyHolder,InsurancePolicyNo=@InsurancePolicyNo,InsuranceCompany=@InsuranceCompany,ResidentialCode=@ResidentialCode,email=@email,contact=@contact WHERE UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("email", plumberemail.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PostalAddress", postalAddressPlumber.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PostalCity", postalCityPlumber.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PostalCode", postalCodePlumber.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("ResidentialStreet", resstreetaddyplumber.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("ResidentialSuburb", ressuburbplumber.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("ResidentialCity", rescityplumber.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Province", resprovinceplumber.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("ResidentialCode", respostalcodeplumber.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("contact", plumbermodilenum.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("InsuranceCompany", plumberinscompany.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("InsurancePolicyNo", plumberpolicynumber.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("InsurancePolicyHolder", plumberpolicyholder.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("InsuranceStartDate", plumberperiodinsfrom.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("InsuranceEndDate", plumberperiodinsto.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Password", DLdb.Encrypt(plumberPassword.Text.ToString()));
                DLdb.SQLST.Parameters.AddWithValue("CompanyIsBillingInfo", CheckBox1.Checked);
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
                DLdb.RS.Close();

                DLdb.DB_Close();
                Response.Redirect("userprofile.aspx?msg=Your ptofile has been updated");
            }

        }
    }
}