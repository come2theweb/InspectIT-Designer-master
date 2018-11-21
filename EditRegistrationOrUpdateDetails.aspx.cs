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

namespace InspectIT
{
    public partial class EditRegistrationOrUpdateDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();

            //// CHECK SESSION
            //if (Session["IIT_UID"] == null)
            //{
            //    Response.Redirect("Default");
            //}

            //// ADMIN CHECK
            //if (Session["IIT_Role"].ToString() != "Administrator")
            //{
            //    Response.Redirect("Default");
            //}

            ////if (Request.QueryString["msg"] != null)
            ////{
            ////    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
            ////    successmsg.InnerHtml = msg;
            ////    successmsg.Visible = true;
            ////}

            ////if (Request.QueryString["err"] != null)
            ////{
            ////    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
            ////    errormsg.InnerHtml = msg;
            ////    errormsg.Visible = true;
            ////}

            ////if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.RS.Close();

            //DLdb.DB_Close();


            if (!IsPostBack)
            {
                DLdb.DB_Connect();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM RegistrationDetails where RegistrationID=@RegistrationID";
                DLdb.SQLST.Parameters.AddWithValue("RegistrationID", Request.QueryString["RegistrationID"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();


                while (theSqlDataReader.Read())
                {
                    // General Details
                    RegistrationNumber.Text = theSqlDataReader["RegistrationNumber"].ToString();
                    TitleStatus.Text = theSqlDataReader["TitleStatus"].ToString();
                    FirstName.Text = theSqlDataReader["FirstName"].ToString();
                    LastName.Text = theSqlDataReader["LastName"].ToString();
                    IDNumber.Text = theSqlDataReader["IDNumber"].ToString();
                    RegistrationRenewalDate.Text = theSqlDataReader["RegistrationRenewalDate"].ToString();
                    string licencePlumber = theSqlDataReader["LicensedPlumber"].ToString();
                    if (licencePlumber == "True")
                    {
                        LicensedPlumber.Checked = true;
                    }
                    string solars = theSqlDataReader["Solar"].ToString();
                    if (solars == "True")
                    {
                        Solar.Checked = true;
                    }
                    string heatPumps = theSqlDataReader["HeatPump"].ToString();
                    if (heatPumps == "True")
                    {
                        HeatPump.Checked = true;
                    }
                    string assessors = theSqlDataReader["Assessor"].ToString();
                    if (assessors == "True")
                    {
                        Assessor.Checked = true;
                    }
                    Notice.Text = theSqlDataReader["Notice"].ToString();
                    NonLoggedCOCs.Text = theSqlDataReader["NonLoggedCOCs"].ToString();
                    NonLoggedCOCsAllocated.Text = theSqlDataReader["NonLoggedCOCsAllocated"].ToString();
                    NumberCOCsLogged.Text = theSqlDataReader["NumberCOCsLogged"].ToString();
                    NumberCOCsAudited.Text = theSqlDataReader["NumberCOCsAudited"].ToString();
                    RefixNotices.Text = theSqlDataReader["RefixNotices"].ToString();
                    AddressStreet.Text = theSqlDataReader["AddressStreet"].ToString();
                    AddressSuburb.Text = theSqlDataReader["AddressSuburb"].ToString();
                    AddressCity.Text = theSqlDataReader["AddressCity"].ToString();
                    AddressProvince.Text = theSqlDataReader["AddressProvince"].ToString();
                    AddressAreaCode.Text = theSqlDataReader["AddressAreaCode"].ToString();
                    PostalAddress.Text = theSqlDataReader["PostalAddress"].ToString();
                    PostalCity.Text = theSqlDataReader["PostalCity"].ToString();
                    PostalCode.Text = theSqlDataReader["PostalCode"].ToString();
                    HomeNo.Text = theSqlDataReader["HomeNo"].ToString();
                    MobileNo.Text = theSqlDataReader["MobileNo"].ToString();
                    EmploymentDate.Text = theSqlDataReader["EmploymentDate"].ToString();
                    Email.Text = theSqlDataReader["Email"].ToString();
                    InsuranceCompany.Text = theSqlDataReader["InsuranceCompany"].ToString();
                    PolicyHolder.Text = theSqlDataReader["PolicyHolder"].ToString();
                    PolicyNumber.Text = theSqlDataReader["PolicyNumber"].ToString();
                    PeriodOfInsuranceFrom.Text = theSqlDataReader["PeriodOfInsuranceFrom"].ToString();
                    PeriodOfInsuranceTo.Text = theSqlDataReader["PeriodOfInsuranceTo"].ToString();
                }
                
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);

                DLdb.RS.Close();
                DLdb.DB_Close();
            }
        }

        protected void btn_updateDetails_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Update RegistrationDetails set RegistrationNumber=@RegistrationNumber, TitleStatus=@TitleStatus, FirstName=@FirstName, LastName=@LastName,IDNumber=@IDNumber," +
                                     "RegistrationRenewalDate=@RegistrationRenewalDate, LicensedPlumber=@LicensedPlumber, Solar=@Solar, HeatPump=@HeatPump, Assessor=@Assessor, Notice=@Notice," +
                                     "NonLoggedCOCs=@NonLoggedCOCs, NonLoggedCOCsAllocated=@NonLoggedCOCsAllocated, NumberCOCsLogged=@NumberCOCsLogged, NumberCOCsAudited=@NumberCOCsAudited, RefixNotices=@RefixNotices, AddressStreet=@AddressStreet, AddressSuburb=@AddressSuburb," +
                                     "AddressCity=@AddressCity, AddressProvince=@AddressProvince, AddressAreaCode=@AddressAreaCode, PostalAddress=@PostalAddress, PostalCity=@PostalCity, PostalCode=@PostalCode, HomeNo=@HomeNo," +
                                     "MobileNo=@MobileNo, EmploymentDate=@EmploymentDate, Email=@Email, InsuranceCompany=@InsuranceCompany, PolicyHolder=@PolicyHolder,PolicyNumber=@PolicyNumber,PeriodOfInsuranceFrom=@PeriodOfInsuranceFrom,PeriodOfInsuranceTo=@PeriodOfInsuranceTo " +
                                     " where RegistrationID=@RegistrationID";
            DLdb.SQLST.Parameters.AddWithValue("RegistrationID", Request.QueryString["RegistrationID"]);
            DLdb.SQLST.Parameters.AddWithValue("RegistrationNumber", RegistrationNumber.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TitleStatus", TitleStatus.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("FirstName", FirstName.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("LastName", LastName.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("IDNumber", IDNumber.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("RegistrationRenewalDate", RegistrationRenewalDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("LicensedPlumber", this.LicensedPlumber.Checked ? "1" : "0"); 
            DLdb.SQLST.Parameters.AddWithValue("Solar", this.Solar.Checked ? "1" : "0");
            DLdb.SQLST.Parameters.AddWithValue("HeatPump", this.HeatPump.Checked ? "1" : "0"); 
            DLdb.SQLST.Parameters.AddWithValue("Assessor", this.Assessor.Checked ? "1" : "0"); 
            DLdb.SQLST.Parameters.AddWithValue("Notice", Notice.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("NonLoggedCOCs", NonLoggedCOCs.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("NonLoggedCOCsAllocated", NonLoggedCOCsAllocated.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("NumberCOCsLogged", NumberCOCsLogged.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("NumberCOCsAudited", NumberCOCsAudited.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("RefixNotices", RefixNotices.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressStreet", AddressStreet.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressSuburb", AddressSuburb.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressCity", AddressCity.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressProvince", AddressProvince.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressAreaCode", AddressAreaCode.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalAddress", PostalAddress.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalCity", PostalCity.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalCode", PostalCode.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("HomeNo", HomeNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("MobileNo", MobileNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("EmploymentDate", EmploymentDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Email", Email.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("InsuranceCompany", InsuranceCompany.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PolicyHolder", PolicyHolder.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PolicyNumber", PolicyNumber.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PeriodOfInsuranceFrom", PeriodOfInsuranceFrom.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PeriodOfInsuranceTo", PeriodOfInsuranceTo.Text.ToString());

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
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("");
        }
    }
}