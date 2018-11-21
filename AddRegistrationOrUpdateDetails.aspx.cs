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
    public partial class AddRegistrationOrUpdateDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();

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

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        protected void btn_AddDetails_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO RegistrationDetails (RegistrationNumber, TitleStatus, FirstName, LastName,IDNumber," +
                                     "RegistrationRenewalDate, LicensedPlumber, Solar, HeatPump, Assessor, Notice," +
                                     "NonLoggedCOCs, NonLoggedCOCsAllocated, NumberCOCsLogged, NumberCOCsAudited, RefixNotices, AddressStreet, AddressSuburb," +
                                     "AddressCity, AddressProvince, AddressAreaCode, PostalAddress, PostalCity, PostalCode, HomeNo," +
                                     "MobileNo, EmploymentDate, Email, InsuranceCompany, PolicyHolder,PolicyNumber,PeriodOfInsuranceFrom,PeriodOfInsuranceTo)" +

                                     "VALUES (@RegistrationNumber, @TitleStatus, @FirstName, @LastName,@IDNumber," +
                                     "@RegistrationRenewalDate, @LicensedPlumber, @Solar, @HeatPump, @Assessor, @Notice," +
                                     "@NonLoggedCOCs, @NonLoggedCOCsAllocated,@NumberCOCsLogged, @NumberCOCsAudited, @RefixNotices, @AddressStreet, @AddressSuburb," +
                                     "@AddressCity, @AddressProvince, @AddressAreaCode, @PostalAddress, @PostalCity, @PostalCode, @HomeNo," +
                                     "@MobileNo, @EmploymentDate, @Email, @InsuranceCompany, @PolicyHolder,@PolicyNumber,@PeriodOfInsuranceFrom,@PeriodOfInsuranceTo)";
            
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
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("");
        }
    }
}