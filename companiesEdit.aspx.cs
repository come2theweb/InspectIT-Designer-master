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
    public partial class companiesEdit : System.Web.UI.Page
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

            if (Request.QueryString["err"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
                Div1.InnerHtml = msg;
                Div1.Visible = true;
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from Companies where CompanyID=@CompanyID";
                    DLdb.SQLST.Parameters.AddWithValue("CompanyID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            CompanyName.Text = theSqlDataReader["CompanyName"].ToString();
                            CompanyRegNo.Text = theSqlDataReader["CompanyRegNo"].ToString();
                            VatNo.Text = theSqlDataReader["VatNo"].ToString();
                            CompanyContactPerson.Text = theSqlDataReader["PrimaryContact"].ToString();
                            CompanyPostalAddress.Text = theSqlDataReader["PostalAddress"].ToString();
                            CompanyPhysicalAddress.Text = theSqlDataReader["AddressLine1"].ToString();
                            CompanyPostalCode.Text = theSqlDataReader["PostalCode"].ToString();
                            CompanyPhysicalCode.Text = theSqlDataReader["AreaCode"].ToString();
                            CompanyMobilePhone.Text = theSqlDataReader["Mobile"].ToString();
                            CompanyEmailAddress.Text = theSqlDataReader["CompanyEmail"].ToString();
                            CompanyWorkPhone.Text = theSqlDataReader["CompanyContactNo"].ToString();
                           
                            if (theSqlDataReader["Construction"].ToString()=="True")
                            {
                                Construction.Checked = true;
                            }
                            if (theSqlDataReader["Maintenance"].ToString() == "True")
                            {
                                Maintenance.Checked = true;
                            }

                            //DLdb.SQLST.Parameters.AddWithValue("PostalProvince", DropDownList4.Text.ToString());
                            //DLdb.SQLST.Parameters.AddWithValue("PostalCity", postalCities.Text.ToString());
                            //DLdb.SQLST.Parameters.AddWithValue("PostalSuburb", postalSuburb.Text.ToString());
                            //DLdb.SQLST.Parameters.AddWithValue("Province", DropDownList5.Text.ToString());
                            //DLdb.SQLST.Parameters.AddWithValue("City", physicalCities.Text.ToString());
                            //DLdb.SQLST.Parameters.AddWithValue("Suburb", physicalSuburb.Text.ToString());
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM Users where Company=@Company";
                    DLdb.SQLST.Parameters.AddWithValue("Company", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            string totPerformanceStatus = "";
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "select sum(cast(performancePointAllocation as int)) as tots from PerformanceStatus where UserID=@UserID and isApproved='1' and isActive='1'";
                            DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                theSqlDataReader2.Read();
                                totPerformanceStatus = theSqlDataReader2["tots"].ToString();
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            Tbody1.InnerHtml += "<tr>" +
                                                               "<td>" + theSqlDataReader["Regno"].ToString() + "</td>" +
                                                               "<td>" + theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString() + "</td>" +
                                                               "<td>"+ totPerformanceStatus + "</td>" +
                                                               "<td></td>" +
                                                           "</tr>";
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM PerformanceStatus where isActive='1' and CompanyID=@CompanyID";
                    DLdb.SQLST.Parameters.AddWithValue("CompanyID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
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
                                                               "<td><div class=\"btn btn-sm btn-primary\" title=\"Edit\" onclick='editPerformanceStatus(" + theSqlDataReader["PerformanceStatusID"].ToString() + ")'><i class=\"fa fa-pencil\"></i></div>" +
                                                               "<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeletePerformanceStatus.aspx?op=delcomp&uid=" + Request.QueryString["id"].ToString() + "&id=" + theSqlDataReader["PerformanceStatusID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                           "</tr>";
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM PerformanceStatus where isActive='0' and CompanyID=@CompanyID";
                    DLdb.SQLST.Parameters.AddWithValue("CompanyID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
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
                                                               "<div class=\"btn btn-sm btn-success\" onclick=\"deleteconf('DeletePerformanceStatus.aspx?op=undelcomp&uid=" + Request.QueryString["id"].ToString() + "&id=" + theSqlDataReader["PerformanceStatusID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                           "</tr>";
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    performanceType.Items.Add(new ListItem("", ""));
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from performancetypes where isactive='1' and isCompany='1'";
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            performanceType.Items.Add(new ListItem(theSqlDataReader["Type"].ToString(), theSqlDataReader["PerformanceID"].ToString()));
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM ApprenticeMentorShips where isActive='1' and CompanyID=@CompanyID";
                    DLdb.SQLST.Parameters.AddWithValue("CompanyID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            string name = "";
                            string regno = "";
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID";
                            DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                theSqlDataReader2.Read();
                                regno = theSqlDataReader2["regno"].ToString();
                                name = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();
                            Tbody2.InnerHtml += "<tr>" +
                                                               "<td>regno</td>" +
                                                               "<td>"+ name + "</td>" +
                                                               "<td>" + theSqlDataReader["EndDate"].ToString() + "</td>" +
                                                               "<td><div class=\"btn btn-sm btn-primary\" title=\"Edit\" onclick='editApprenticeMentorShip(" + theSqlDataReader["ApprenticeID"].ToString() + ")'><i class=\"fa fa-pencil\"></i></div>" +
                                                               "<div class=\"btn btn-sm btn-success\" onclick=\"deleteconf('DeleteApprenticeMentorShip.aspx?op=undelcomp&compid=" + Request.QueryString["id"].ToString() + "&id=" + theSqlDataReader["ApprenticeID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                           "</tr>";
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM ApprenticeMentorShips where isActive='0' and CompanyID=@CompanyID";
                    DLdb.SQLST.Parameters.AddWithValue("CompanyID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            string name = "";
                            string regno = "";
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID";
                            DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                theSqlDataReader2.Read();
                                regno = theSqlDataReader2["regno"].ToString();
                                name = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();
                            Tbody3.InnerHtml += "<tr>" +
                                                               "<td>regno</td>" +
                                                               "<td>" + name + "</td>" +
                                                               "<td>" + theSqlDataReader["EndDate"].ToString() + "</td>" +
                                                               "<td><div class=\"btn btn-sm btn-primary\" title=\"Edit\" onclick='editApprenticeMentorShip(" + theSqlDataReader["ApprenticeID"].ToString() + ")'><i class=\"fa fa-pencil\"></i></div>" +
                                                               "<div class=\"btn btn-sm btn-success\" onclick=\"deleteconf('DeleteApprenticeMentorShip.aspx?op=undelcomp&compid=" + Request.QueryString["id"].ToString() + "&id=" + theSqlDataReader["ApprenticeID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                           "</tr>";
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }
                else
                {

                    btnUpdate.Visible = false;
                    btnSave.Visible = true;
                }


            }


            DLdb.DB_Close();
        }
        protected void myListDropDown_Change(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();


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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into Companies (SARSCertificate,IOPSAMembership,PIRBDeclaration,EmployeeMedicals,BBBEEStatus,PublicLiabilityInsurance,CompanyLiabilityInsurance,COIDLetter,Maintenance,Construction,CompanyName,CompanyRegNo,VatNo,CompanyEmail,CompanyContactNo,AddressLine1,Province,City,Suburb,AreaCode,PostalAddress,PostalCity,PostalSuburb,PostalProvince,PostalCode,PrimaryContact,Mobile) values (@SARSCertificate,@IOPSAMembership,@PIRBDeclaration,@EmployeeMedicals,@BBBEEStatus,@PublicLiabilityInsurance,@CompanyLiabilityInsurance,@COIDLetter,@Maintenance,@Construction,@CompanyName,@CompanyRegNo,@VatNo,@CompanyEmail,@CompanyContactNo,@AddressLine1,@Province,@City,@Suburb,@AreaCode,@PostalAddress,@PostalCity,@PostalSuburb,@PostalProvince,@PostalCode,@PrimaryContact,@Mobile)";
            DLdb.SQLST.Parameters.AddWithValue("CompanyName", CompanyName.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyRegNo", CompanyRegNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("VatNo", VatNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PrimaryContact", CompanyContactPerson.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalAddress", CompanyPostalAddress.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalProvince", DropDownList4.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalCity", postalCities.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalSuburb", postalSuburb.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressLine1", CompanyPhysicalAddress.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalCode", CompanyPostalCode.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Province", DropDownList5.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("City", physicalCities.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Suburb", physicalSuburb.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AreaCode", CompanyPhysicalCode.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Mobile", CompanyMobilePhone.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyEmail", CompanyEmailAddress.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyContactNo", CompanyWorkPhone.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Maintenance", Maintenance.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("Construction", Construction.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("SARSCertificate", SARSCertificate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("COIDLetter", COIDLetter.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PublicLiabilityInsurance", PublicLiabilityInsurance.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyLiabilityInsurance", CompanyLiabilityInsurance.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("BBBEEStatus", BBBEEStatus.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("IOPSAMembership", IOPSAMembership.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("EmployeeMedicals", EmployeeMedicals.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PIRBDeclaration", PIRBDeclaration.Text.ToString());
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
            DLdb.RS.Close();

            decimal TotalPoints = 0;
            decimal sars = 0;
            decimal coid = 0;
            decimal publicliability = 0;
            decimal companyliability = 0;
            decimal iopsa = 0;
            decimal bbbee = 0;
            decimal aprenticeship = 0;
            decimal mentor = 0;
            decimal medical = 0;
            decimal pirbDeclaration = 0;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from companypoints";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["Item"].ToString()== "SARS Clearance Certificate  ")
                    {
                        sars = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "COID Letter of Good Standing  ")
                    {
                        coid = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "Public Liability Insurance")
                    {
                        publicliability = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "Company Liability Insurance   ")
                    {
                        companyliability = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "BBEE Status ")
                    {
                        bbbee = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "IOPSA Membership")
                    {
                        iopsa = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "Employee Medicals  ")
                    {
                        medical = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "PIRB Company Declaration  ")
                    {
                        pirbDeclaration = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "Apprenticeships in Company")
                    {
                        aprenticeship = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "Mentoring in Company  ")
                    {
                        mentor = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            if (SARSCertificate.Text != "")
            {
                TotalPoints += sars;
            }
            if (COIDLetter.Text != "")
            {
                TotalPoints += coid;
            }
            if (PublicLiabilityInsurance.Text != "")
            {
                TotalPoints += publicliability;
            }
            if (CompanyLiabilityInsurance.Text != "")
            {
                TotalPoints += companyliability;
            }
            if (BBBEEStatus.Text != "")
            {
                TotalPoints += bbbee;
            }
            if (IOPSAMembership.Text != "")
            {
                TotalPoints += iopsa;
            }
            if (EmployeeMedicals.Text != "")
            {
                TotalPoints += medical;
            }
            if (PIRBDeclaration.Text != "")
            {
                TotalPoints += pirbDeclaration;
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update Companies set TotalPoints=@TotalPoints where CompanyID=@CompanyID";
            DLdb.SQLST.Parameters.AddWithValue("TotalPoints", TotalPoints.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("companies?msg=" + DLdb.Encrypt("Successfully added a new company"));
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string compid = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update Companies set Maintenance=@Maintenance,Construction=@Construction,CompanyName=@CompanyName,CompanyRegNo=@CompanyRegNo,VatNo=@VatNo,CompanyEmail=@CompanyEmail,CompanyContactNo=@CompanyContactNo,AddressLine1=@AddressLine1,Province=@Province,City=@City,Suburb=@Suburb,AreaCode=@AreaCode,PostalAddress=@PostalAddress,PostalCity=@PostalCity,PostalSuburb=@PostalSuburb,PostalProvince=@PostalProvince,PostalCode=@PostalCode,PrimaryContact=@PrimaryContact,Mobile=@Mobile,SARSCertificate=@SARSCertificate,COIDLetter=@COIDLetter,PublicLiabilityInsurance=@PublicLiabilityInsurance,CompanyLiabilityInsurance=@CompanyLiabilityInsurance,BBBEEStatus=@BBBEEStatus,IOPSAMembership=@IOPSAMembership,EmployeeMedicals=@EmployeeMedicals,PIRBDeclaration=@PIRBDeclaration where CompanyID=@CompanyID;select scope_identity() as compid;";
            DLdb.SQLST.Parameters.AddWithValue("CompanyName", CompanyName.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyRegNo", CompanyRegNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("VatNo", VatNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PrimaryContact", CompanyContactPerson.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalAddress", CompanyPostalAddress.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalProvince", DropDownList4.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalCity", postalCities.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalSuburb", postalSuburb.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressLine1", CompanyPhysicalAddress.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalCode", CompanyPostalCode.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Province", DropDownList5.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("City", physicalCities.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Suburb", physicalSuburb.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AreaCode", CompanyPhysicalCode.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Mobile", CompanyMobilePhone.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyEmail", CompanyEmailAddress.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyContactNo", CompanyWorkPhone.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Maintenance", Maintenance.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("Construction", Construction.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("SARSCertificate", SARSCertificate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("COIDLetter", COIDLetter.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PublicLiabilityInsurance", PublicLiabilityInsurance.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyLiabilityInsurance", CompanyLiabilityInsurance.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("BBBEEStatus", BBBEEStatus.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("IOPSAMembership", IOPSAMembership.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("EmployeeMedicals", EmployeeMedicals.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PIRBDeclaration", PIRBDeclaration.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                compid = theSqlDataReader["compid"].ToString();
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
            DLdb.RS.Close();

            decimal TotalPoints = 0;
            decimal sars = 0;
            decimal coid = 0;
            decimal publicliability = 0;
            decimal companyliability = 0;
            decimal iopsa = 0;
            decimal bbbee = 0;
            decimal aprenticeship = 0;
            decimal mentor = 0;
            decimal medical = 0;
            decimal pirbDeclaration = 0;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from companypoints";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["Item"].ToString() == "SARS Clearance Certificate  ")
                    {
                        sars = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "COID Letter of Good Standing  ")
                    {
                        coid = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "Public Liability Insurance")
                    {
                        publicliability = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "Company Liability Insurance   ")
                    {
                        companyliability = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "BBEE Status ")
                    {
                        bbbee = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "IOPSA Membership")
                    {
                        iopsa = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "Employee Medicals  ")
                    {
                        medical = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "PIRB Company Declaration  ")
                    {
                        pirbDeclaration = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "Apprenticeships in Company")
                    {
                        aprenticeship = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                    else if (theSqlDataReader["Item"].ToString() == "Mentoring in Company  ")
                    {
                        mentor = Convert.ToDecimal(theSqlDataReader["Points"].ToString());
                    }
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            if (SARSCertificate.Text != "")
            {
                TotalPoints += sars;
            }
            if (COIDLetter.Text != "")
            {
                TotalPoints += coid;
            }
            if (PublicLiabilityInsurance.Text != "")
            {
                TotalPoints += publicliability;
            }
            if (CompanyLiabilityInsurance.Text != "")
            {
                TotalPoints += companyliability;
            }
            if (BBBEEStatus.Text != "")
            {
                TotalPoints += bbbee;
            }
            if (IOPSAMembership.Text != "")
            {
                TotalPoints += iopsa;
            }
            if (EmployeeMedicals.Text != "")
            {
                TotalPoints += medical;
            }
            if (PIRBDeclaration.Text != "")
            {
                TotalPoints += pirbDeclaration;
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update Companies set TotalPoints=@TotalPoints where CompanyID=@CompanyID";
            DLdb.SQLST.Parameters.AddWithValue("TotalPoints", TotalPoints.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("companies?msg=" + DLdb.Encrypt("Successfully updated company"));
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
            DLdb.SQLST.CommandText = "insert into PerformanceStatus (hasEndDate,endDate,CompanyID,Date,PerformanceType,Details,PerformancePointAllocation,Attachment) values (@hasEndDate,@endDate,@CompanyID,@Date,@PerformanceType,@Details,@PerformancePointAllocation,@Attachment)";
            DLdb.SQLST.Parameters.AddWithValue("CompanyID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("Date", date.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PerformanceType", performanceType.SelectedItem.ToString());
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

            string ImgOld = TextBox1.Text.ToString();
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
            DLdb.SQLST.Parameters.AddWithValue("PerformanceType", performanceType.SelectedItem.ToString());
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


        protected void saveBtnApprentice_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (FileUpload2.HasFiles)
            {
                foreach (HttpPostedFile File in FileUpload2.PostedFiles)
                {
                    string filename = "Apprentice_"+ File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/performanceImgs/"), filename));
                }
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into ApprenticeMentorShips (StartDate,endDate,CompanyID,Type,UserID,Attachment) values (@StartDate,@endDate,@CompanyID,@Type,@UserID,@Attachment)";
            DLdb.SQLST.Parameters.AddWithValue("CompanyID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("UserID", appMenID.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Attachment", "Apprentice_" + FileUpload2.FileName);
            DLdb.SQLST.Parameters.AddWithValue("Type", selAppMenType.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("endDate", endDateAppMen.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("StartDate", StartDateAppMen.Text.ToString());
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
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void updateBtnApprentice_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            
            string filename = imgUploaded.Text.ToString();
            if (FileUpload2.HasFiles)
            {
                filename = "Apprentice_" + FileUpload2.FileName;
                foreach (HttpPostedFile File in FileUpload2.PostedFiles)
                {
                    filename = "Apprentice_" + File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/performanceImgs/"), filename));
                }
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update ApprenticeMentorShips set endDate=@endDate,StartDate=@StartDate,Type=@Type,UserID=@UserID,Attachment=@Attachment where ApprenticeID=@ApprenticeID";
            DLdb.SQLST.Parameters.AddWithValue("ApprenticeID", ApprenticeID.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", appMenID.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Attachment", filename.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Type", selAppMenType.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("endDate", endDateAppMen.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("StartDate", StartDateAppMen.Text.ToString());
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
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}