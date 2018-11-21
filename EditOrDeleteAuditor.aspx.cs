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

namespace InspectIT
{
    public partial class EditOrDeleteAuditor : System.Web.UI.Page
    {
        public string previousLatitude = "";
        public string previousLongitude = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                btn_update.Visible = false;
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {

            }

            // ADMIN CHECK
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }
            showSearchAreas.Visible = false;
            
            if (!IsPostBack)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Area order by name asc";
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
                DLdb.SQLST.CommandText = "select * from AreaSuburbs order by name asc";
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

                string UserID = "";

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Auditor where AuditorID=@AuditorID";
                DLdb.SQLST.Parameters.AddWithValue("AuditorID", DLdb.Decrypt(Request.QueryString["AuditorID"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        // General Details
                        //previousLatitude = theSqlDataReader["lat"].ToString();
                        //previousLongitude = theSqlDataReader["lng"].ToString();
                        regNo.Text = theSqlDataReader["regNo"].ToString();
                        fName.Text = theSqlDataReader["fName"].ToString();
                        lName.Text = theSqlDataReader["lName"].ToString();
                        idNo.Text = theSqlDataReader["idNo"].ToString();
                        string isActive = theSqlDataReader["isActive"].ToString();
                        if (isActive == "True")
                        {
                            active.Checked = true;
                        }
                        startInactiveDate.Text = theSqlDataReader["startInactiveDate"].ToString();
                        endInactiveDate.Text = theSqlDataReader["endInactiveDate"].ToString();
                        phoneWork.Text = theSqlDataReader["phoneWork"].ToString();
                        phoneHome.Text = theSqlDataReader["phoneHome"].ToString();
                        phoneMobile.Text = theSqlDataReader["phoneMobile"].ToString();
                        fax.Text = theSqlDataReader["fax"].ToString();
                        email.Text = theSqlDataReader["email"].ToString();
                        pin.Text = theSqlDataReader["pin"].ToString();
                        currentImg.Text = theSqlDataReader["photoFile"].ToString();
                        TextBox7.Text = theSqlDataReader["CompanyLogo"].ToString();
                        Image1.ImageUrl = "AuditorImgs/" + theSqlDataReader["photoFile"].ToString();
                        Image2.ImageUrl = "AuditorImgs/" + theSqlDataReader["CompanyLogo"].ToString();
                        addressLine1.Text = theSqlDataReader["addressLine1"].ToString();
                        addressLine2.Text = theSqlDataReader["addressLine2"].ToString();
                        Province.Text = theSqlDataReader["province"].ToString();
                        postalprovince.Text = theSqlDataReader["postalprovince"].ToString();
                        DropDownList4.SelectedValue = theSqlDataReader["city"].ToString();
                        DropDownList5.SelectedValue = theSqlDataReader["Suburb"].ToString();
                        areaCode.Text = theSqlDataReader["areaCode"].ToString();
                        postalAddress.Text = theSqlDataReader["postalAddress"].ToString();
                        postalCity.Text = theSqlDataReader["postalCity"].ToString();
                        postalCode.Text = theSqlDataReader["postalCode"].ToString();
                        companyRegNo.Text = theSqlDataReader["companyRegNo"].ToString();
                        companyName.Text = theSqlDataReader["companyName"].ToString();
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
                        UserID = theSqlDataReader["UserID"].ToString();
                    }
                }
                
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();


                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from users where userid = @userid";
                DLdb.SQLST.Parameters.AddWithValue("userid", UserID);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        supPassword.Text = DLdb.Decrypt(theSqlDataReader["password"].ToString());
                        supPasswordConfirm.Text = DLdb.Decrypt(theSqlDataReader["password"].ToString());
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string areaProvince = "";
                string areaName = "";
                string areasCode = "";
                string areaSuburb = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from AuditorAreas where AuditorID = @AuditorID";
                DLdb.SQLST.Parameters.AddWithValue("AuditorID", DLdb.Decrypt(Request.QueryString["AuditorID"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "Select * from Area where ID = @ID";
                        DLdb.SQLST2.Parameters.AddWithValue("ID", theSqlDataReader["AreaID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                areaName = theSqlDataReader2["Name"].ToString();
                                areasCode = theSqlDataReader2["Code"].ToString();

                                DLdb.RS3.Open();
                                DLdb.SQLST3.CommandText = "Select * from Province where ID = @ID";
                                DLdb.SQLST3.Parameters.AddWithValue("ID", theSqlDataReader2["ProvinceID"].ToString());
                                DLdb.SQLST3.CommandType = CommandType.Text;
                                DLdb.SQLST3.Connection = DLdb.RS3;
                                SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();
                                if (theSqlDataReader3.HasRows)
                                {
                                    while (theSqlDataReader3.Read())
                                    {
                                        areaProvince = theSqlDataReader3["Name"].ToString();
                                    }
                                }

                                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                                DLdb.SQLST3.Parameters.RemoveAt(0);
                                DLdb.RS3.Close();
                                
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "Select * from areasuburbs where SuburbID = @SuburbID";
                        DLdb.SQLST2.Parameters.AddWithValue("SuburbID", theSqlDataReader["SuburbID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                areaSuburb = theSqlDataReader2["Name"].ToString();
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        displayauditorsareas.InnerHtml += "<tr>" +
                            "<td>" + areaProvince + "</td>" +
                            "<td>" + areaName + "</td>" +
                            "<td>" + areaSuburb + "</td>" +
                            "<td><a href=\"DeleteItems.aspx?id="+ theSqlDataReader["ID"].ToString() + "&op=delaudArea&pid="+Request.QueryString["AuditorID"].ToString()+"\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-trash\"></i></div></a></td>" +
                            //"<td>" + areasCode + "</td>" +
                            "</tr>";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.DB_Close();
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (supPassword.Text.ToString() == supPasswordConfirm.Text.ToString())
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

                //decimal latitude = 0;
                //decimal longitude = 0;
                //try
                //{
                //    string addressToLongLat = addressLine1.Text.ToString() + ", " + citySuburb.Text.ToString() + ", " + Province.SelectedValue.ToString();
                //    var address = addressToLongLat;
                //    var locationService = new GoogleLocationService();
                //    var point = locationService.GetLatLongFromAddress(address);
                //    latitude = Convert.ToDecimal(point.Latitude);
                //    longitude = Convert.ToDecimal(point.Longitude);
                //}
                //catch (Exception err)
                //{
                //    errormsg.Visible = true;
                //    errormsg.InnerHtml = err + " <br/>Please try clicking the update button again, if the problem persists contact the IT department";
                //    latitude = Convert.ToDecimal(previousLatitude);
                //    longitude = Convert.ToDecimal(previousLongitude);
                //}

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "UPDATE Auditor SET regNo=@regNo, fName=@fName, lName=@lName," +
                                            "idNo=@idNo, isActive=@isActive, startInactiveDate=@startInactiveDate, endInactiveDate=@endInactiveDate," +
                                            "phoneWork=@phoneWork,companyName=@companyName, phoneHome=@phoneHome, phoneMobile=@phoneMobile, fax=@fax, email=@email, pin=@pin, photoFile=@photoFile," +
                                            "addressLine1=@addressLine1, addressLine2=@addressLine2, province=@province,city=@city, suburb=@suburb, areaCode=@areaCode," +
                                            "postalAddress=@postalAddress, postalCity=@postalCity, postalCode=@postalCode, companyRegNo=@companyRegNo, vatRegNo=@vatRegNo," +
                                            "pastelAccount=@pastelAccount, invoiceEmail=@invoiceEmail, stopPayments=@stopPayments, bankName=@bankName, accName=@accName, accNumber=@accNumber," +
                                            "branchCode=@branchCode,CompanyLogo=@CompanyLogo, accType=@accType,postalprovince=@postalprovince,Range=@Range " +
                                            "WHERE AuditorID=@AuditorID";
                DLdb.SQLST.Parameters.AddWithValue("AuditorID", DLdb.Decrypt(Request.QueryString["AuditorID"].ToString()));
                DLdb.SQLST.Parameters.AddWithValue("regNo", regNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("fName", fName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("lName", lName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("idNo", idNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("isActive", this.active.Checked ? "1" : "0");
                DLdb.SQLST.Parameters.AddWithValue("startInactiveDate", startInactiveDate.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("endInactiveDate", endInactiveDate.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("phoneWork", phoneWork.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("phoneHome", phoneHome.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("companyName", companyName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("phoneMobile", phoneMobile.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("fax", fax.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("email", email.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("pin", pin.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("photoFile", ImgOld);
                DLdb.SQLST.Parameters.AddWithValue("addressLine1", addressLine1.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("addressLine2", addressLine2.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("province", Province.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("postalprovince", postalprovince.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Suburb", DropDownList5.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("city", DropDownList4.SelectedValue.ToString());
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
                DLdb.SQLST.Parameters.AddWithValue("CompanyLogo", ImgOldCL);
                //DLdb.SQLST.Parameters.AddWithValue("lat", latitude);
                //DLdb.SQLST.Parameters.AddWithValue("lng", longitude);
                DLdb.SQLST.Parameters.AddWithValue("Range", Range.Text.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

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

                foreach (ListItem listItems in filterAreasDisp.Items)
                {
                    if (listItems.Selected)
                    {
                        DLdb.SQLST.Parameters.Clear();
                        DLdb.RS.Open();
                        DLdb.SQLST.CommandText = "insert into AuditorAreas (AuditorID,AreaID) values (@AuditorID,@AreaID)";
                        DLdb.SQLST.Parameters.AddWithValue("AreaID", listItems.Value.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("AuditorID", DLdb.Decrypt(Request.QueryString["AuditorID"].ToString()));
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
                        DLdb.SQLST.Parameters.Clear();
                        DLdb.RS.Open();
                        DLdb.SQLST.CommandText = "update AuditorAreas set isActive='0' where  AuditorID=@AuditorID and AreaID=@AreaID";
                        DLdb.SQLST.Parameters.AddWithValue("AreaID", listItems.Value.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("AuditorID", DLdb.Decrypt(Request.QueryString["AuditorID"].ToString()));
                        DLdb.SQLST.CommandType = CommandType.Text;
                        DLdb.SQLST.Connection = DLdb.RS;
                        theSqlDataReader = DLdb.SQLST.ExecuteReader();

                        if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.RS.Close();
                    }
                }

                DLdb.DB_Close();
                Response.Redirect("ViewAuditor.aspx?msg=" + DLdb.Encrypt("Auditor updated successfuly"));
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
            DLdb.SQLST.CommandText = "select * from Area where Name like '%" + searchAreas.Text.ToString() + "%'";
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


            foreach (ListItem listItemDisp in filterAreasDisp.Items)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from AuditorAreas where AuditorID=@AuditorID and AreaID=@AreaID and isActive='1'";
                DLdb.SQLST.Parameters.AddWithValue("AuditorID", DLdb.Decrypt(Request.QueryString["AuditorID"].ToString()));
                DLdb.SQLST.Parameters.AddWithValue("AreaID", listItemDisp.Value.ToString());
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

        protected void manageAreas_Click(object sender, EventArgs e)
        {
            //showSearchAreas.Visible = true;
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from AuditorAreas where SuburbID = @SuburbID and AuditorID=@AuditorID and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("SuburbID", DropDownList3.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AuditorID", DLdb.Decrypt(Request.QueryString["AuditorID"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();

            }
            else
            {
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "insert into AuditorAreas (AuditorID,AreaID,SuburbID) values (@AuditorID,@AreaID,@SuburbID)";
                DLdb.SQLST2.Parameters.AddWithValue("AreaID", DropDownList2.SelectedValue.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("AuditorID", DLdb.Decrypt(Request.QueryString["AuditorID"].ToString()));
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

            DLdb.DB_Close();

            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void myListDropDown_Change(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DropDownList2.Items.Clear();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Area where ProvinceID=@ProvinceID order by name asc";
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
            DLdb.SQLST.CommandText = "select * from AreaSuburbs where CityID=@CityID order by name asc";
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