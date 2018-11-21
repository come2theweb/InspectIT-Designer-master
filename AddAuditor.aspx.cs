using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GoogleMaps.LocationServices;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.IO;

namespace InspectIT
{
    public partial class AddAuditor : System.Web.UI.Page
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

            // ADMIN CHECK
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }

            //if (Request.QueryString["msg"] != null)
            //{
            //    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
            //    successmsg.InnerHtml = msg;
            //    successmsg.Visible = true;
            //}

            //if (Request.QueryString["err"] != null)
            //{
            //    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
            //    errormsg.InnerHtml = msg;
            //    errormsg.Visible = true;
            //}

            showSearchAreas.Visible = false;

            if (!IsPostBack)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Area";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        DropDownList4.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["Name"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from AreaSuburbs";
                //DLdb.SQLST.Parameters.AddWithValue("CityID", DropDownList2.SelectedValue);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        DropDownList5.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["Name"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }

            DLdb.DB_Close();
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (supPassword.Text.ToString() == supPasswordConfirm.Text.ToString())
            {
                if (photoFile.HasFiles)
                {
                    foreach (HttpPostedFile File in photoFile.PostedFiles)
                    {
                        string filename = File.FileName;
                        File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AuditorImgs/"), filename));
                    }
                }

                if (FileUpload1.HasFiles)
                {
                    foreach (HttpPostedFile File in FileUpload1.PostedFiles)
                    {
                        string filename = File.FileName;
                        File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AuditorImgs/"), filename));
                    }
                }

                //REQUIRED: CHECK IF EXISTS AND GET LAT/LNG
                string AuditorID = "";


                Global DLdb = new Global();
                DLdb.DB_Connect();
                string addressToLongLat = addressLine1.Text.ToString() + ", " + Suburb.Text.ToString() + ", " + city.Text.ToString() + ", " + Province.SelectedValue.ToString();

                //var address = addressToLongLat;
                //var locationService = new GoogleLocationService();
                //var point = locationService.GetLatLongFromAddress(address);
                //var latitude = point.Latitude;
                //var longitude = point.Longitude;

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "INSERT INTO Auditor (companyName,CompanyLogo,regNo, fName, lName, idNo, isActive," +
                                         "startInactiveDate, endInactiveDate, phoneWork, phoneHome, phoneMobile, fax," +
                                         "email, pin, photoFile, addressLine1, addressLine2, province, city,Suburb, areaCode," +
                                         "postalAddress, postalCity, postalCode, companyRegNo, vatRegNo, pastelAccount, invoiceEmail," +
                                         "stopPayments, bankName, accName, accNumber, branchCode, accType,Range,postalProvince)" +

                                         "VALUES (@companyName,@CompanyLogo,@regNo, @fName, @lName, @idNo, @isActive," +
                                         "@startInactiveDate, @endInactiveDate, @phoneWork, @phoneHome, @phoneMobile, @fax," +
                                         "@email, @pin, @photoFile, @addressLine1, @addressLine2, @province, @city,@Suburb, @areaCode," +
                                         "@postalAddress, @postalCity, @postalCode, @companyRegNo, @vatRegNo, @pastelAccount, @invoiceEmail," +
                                         "@stopPayments, @bankName, @accName, @accNumber, @branchCode, @accType,@Range,@postalProvince); Select Scope_Identity() as AuditorID";

                // General Details
                DLdb.SQLST.Parameters.AddWithValue("regNo", regNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("fName", fName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("lName", lName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("idNo", idNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("isActive", this.active.Checked ? "1" : "0"); // Active Checkbox
                DLdb.SQLST.Parameters.AddWithValue("startInactiveDate", startInactiveDate.Text.ToString()); // Start inactive date calendar
                DLdb.SQLST.Parameters.AddWithValue("endInactiveDate", endInactiveDate.Text.ToString()); // End inactive date calendar
                DLdb.SQLST.Parameters.AddWithValue("phoneWork", phoneWork.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("phoneHome", phoneHome.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("phoneMobile", phoneMobile.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("fax", fax.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("email", email.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("pin", pin.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("photoFile", photoFile.FileName); // File upload of photo 
                DLdb.SQLST.Parameters.AddWithValue("CompanyLogo", FileUpload1.FileName);

                // Address
                DLdb.SQLST.Parameters.AddWithValue("addressLine1", addressLine1.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("addressLine2", addressLine2.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("province", Province.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("postalProvince", postalprovince.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("city", DropDownList4.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Suburb", DropDownList5.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("areaCode", areaCode.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("postalAddress", postalAddress.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("postalCity", postalCity.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("postalCode", postalCode.Text.ToString());

                // Business
                DLdb.SQLST.Parameters.AddWithValue("companyRegNo", companyRegNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("companyName", companyName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("vatRegNo", vatRegNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("pastelAccount", pastelAccount.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("invoiceEmail", invoiceEmail.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("stopPayments", this.stopPayments.Checked ? "1" : "0"); // check box to stop payments
                DLdb.SQLST.Parameters.AddWithValue("bankName", DLdb.Encrypt(bankName.Text.ToString()));
                DLdb.SQLST.Parameters.AddWithValue("accName", DLdb.Encrypt(accName.Text.ToString()));
                DLdb.SQLST.Parameters.AddWithValue("accNumber", DLdb.Encrypt(accNumber.Text.ToString()));
                DLdb.SQLST.Parameters.AddWithValue("branchCode", DLdb.Encrypt(branchCode.Text.ToString()));
                DLdb.SQLST.Parameters.AddWithValue("accType", DLdb.Encrypt(accType.SelectedValue)); // Drop down of savings or cheque
                //DLdb.SQLST.Parameters.AddWithValue("lat", latitude);
                //DLdb.SQLST.Parameters.AddWithValue("lng", longitude);
                DLdb.SQLST.Parameters.AddWithValue("Range", Range.Text.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    AuditorID = theSqlDataReader["AuditorID"].ToString();
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
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
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string UserID = "";
                string pass = DLdb.CreatePassword(8);
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "INSERT INTO Users (fname,lname, password, role, email) VALUES (@fname, @lname, @password, @role, @email); Select Scope_Identity() as UserID";
                DLdb.SQLST.Parameters.AddWithValue("fname", fName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("lname", lName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(supPassword.Text.ToString()));
                DLdb.SQLST.Parameters.AddWithValue("role", "Inspector");
                DLdb.SQLST.Parameters.AddWithValue("email", email.Text.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    UserID = theSqlDataReader["UserID"].ToString();
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                //Update userid in auditors table
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from AuditorAreas where SuburbID = @SuburbID and AuditorID=@AuditorID and isActive='1'";
                DLdb.SQLST.Parameters.AddWithValue("SuburbID", DropDownList3.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AuditorID", AuditorID);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();

                }
                else
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into AuditorAreas (AuditorID,AreaID,SuburbID) values (@AuditorID,@AreaID,@SuburbID)";
                    DLdb.SQLST2.Parameters.AddWithValue("AreaID", DropDownList2.SelectedValue.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("AuditorID", AuditorID.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("SuburbID", DropDownList3.SelectedValue.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                //DLdb.RS.Open();
                //DLdb.SQLST.CommandText = "update Auditor set UserID = @UserID where AuditorID = @AuditorID";
                //DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                //DLdb.SQLST.Parameters.AddWithValue("AuditorID", AuditorID);
                //DLdb.SQLST.CommandType = CommandType.Text;
                //DLdb.SQLST.Connection = DLdb.RS;
                //theSqlDataReader = DLdb.SQLST.ExecuteReader();

                //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.RS.Close();

                //foreach (ListItem listItem in filterAreasDisp.Items)
                //{
                //    if (listItem.Selected)
                //    {
                //        DLdb.RS.Open();
                //        DLdb.SQLST.CommandText = "insert into AuditorAreas (AuditorID,AreaID) values (@AuditorID,@AreaID)";
                //        DLdb.SQLST.Parameters.AddWithValue("AreaID", listItem.Value.ToString());
                //        DLdb.SQLST.Parameters.AddWithValue("AuditorID", AuditorID.ToString());
                //        DLdb.SQLST.CommandType = CommandType.Text;
                //        DLdb.SQLST.Connection = DLdb.RS;
                //        theSqlDataReader = DLdb.SQLST.ExecuteReader();

                //        if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //        DLdb.SQLST.Parameters.RemoveAt(0);
                //        DLdb.SQLST.Parameters.RemoveAt(0);
                //        DLdb.RS.Close();
                //    }
                //    else
                //    {
                //        DLdb.RS.Open();
                //        DLdb.SQLST.CommandText = "update AuditorAreas set isActive='0' where  AuditorID=@AuditorID and AreaID=@AreaID";
                //        DLdb.SQLST.Parameters.AddWithValue("AreaID", listItem.Value.ToString());
                //        DLdb.SQLST.Parameters.AddWithValue("AuditorID", AuditorID.ToString());
                //        DLdb.SQLST.CommandType = CommandType.Text;
                //        DLdb.SQLST.Connection = DLdb.RS;
                //        theSqlDataReader = DLdb.SQLST.ExecuteReader();

                //        if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //        DLdb.SQLST.Parameters.RemoveAt(0);
                //        DLdb.SQLST.Parameters.RemoveAt(0);
                //        DLdb.RS.Close();
                //    }
                //}

                // EMAIL THE USER DETAILS
                string HTMLSubject = "Welcome to Inspect IT.";
                string HTMLBody = "Dear " + fName.Text.ToString() + "<br /><br />Welcome to Inspect IT<br /><br />Your login details are;<br />Email Address: " + email.Text.ToString() + "<br />Password: " + pass.ToString() + "<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Team";
                DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", email.Text.ToString(), "");

                DLdb.DB_Close();
                Response.Redirect("ViewAuditor.aspx?msg=" + DLdb.Encrypt("Auditor added successfuly"));
            }
            else
            {
                errormsg.InnerHtml = "Your password does not match";
                errormsg.Visible = true;
            }
        }

        protected void searchareasclick_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            showSearchAreas.Visible = true;
            filterAreasDisp.Items.Clear();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Area where Name like '%"+ searchAreas.Text.ToString() + "%'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {

                    filterAreasDisp.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["ID"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        protected void manageAreas_Click(object sender, EventArgs e)
        {
            showSearchAreas.Visible = true;
        }

        protected void myListDropDown_Change(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DropDownList2.Items.Clear();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Area where ProvinceID=@ProvinceID";
            DLdb.SQLST.Parameters.AddWithValue("ProvinceID", DropDownList1.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DropDownList2.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["id"].ToString()));
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
            DLdb.SQLST.CommandText = "select * from AreaSuburbs where CityID=@CityID";
            DLdb.SQLST.Parameters.AddWithValue("CityID", DropDownList2.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DropDownList3.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["suburbid"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            DLdb.DB_Close();
        }
    }
}