using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace InspectIT
{
    public partial class ViewRegistrationOrUpdateDetails : System.Web.UI.Page
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
            //displayusers.InnerHtml = "";

            //Global DLdb = new Global();
            //DLdb.DB_Connect();
            //DLdb.RS.Open();


            //DLdb.SQLST.CommandText = "SELECT * FROM RegistrationDetails where RegistrationID=@RegistrationID";
            //DLdb.SQLST.Parameters.AddWithValue("RegistrationID", Request.QueryString["RegistrationID"].ToString());
            //// DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //if (theSqlDataReader.HasRows)
            //{
            //    while (theSqlDataReader.Read())
            //    {
            //        RegistrationNumber.InnerHtml = theSqlDataReader["RegistrationNumber"].ToString();
            //        TitleStatus.InnerHtml = theSqlDataReader["TitleStatus"].ToString();
            //        FirstName.InnerHtml = theSqlDataReader["FirstName"].ToString();
            //        LastName.InnerHtml = theSqlDataReader["LastName"].ToString();
            //        IDNumber.InnerHtml = theSqlDataReader["IDNumber"].ToString();
            //        RegistrationRenewalDate.InnerHtml = theSqlDataReader["RegistrationRenewalDate"].ToString();
            //        // Checkboxes
            //        Notice.InnerHtml = theSqlDataReader["Notice"].ToString();
            //        NonLoggedCOCs.InnerHtml = theSqlDataReader["NonLoggedCOCs"].ToString();
            //        NonLoggedCOCsAllocated.InnerHtml = theSqlDataReader["NonLoggedCOCsAllocated"].ToString();
            //        NumberCOCsLogged.InnerHtml = theSqlDataReader["NumberCOCsLogged"].ToString();
            //        NumberCOCsAudited.InnerHtml = theSqlDataReader["NumberCOCsAudited"].ToString();
            //        RefixNotices.InnerHtml = theSqlDataReader["RefixNotices"].ToString();
            //        AddressStreet.InnerHtml = theSqlDataReader["AddressStreet"].ToString();
            //        AddressSuburb.InnerHtml = theSqlDataReader["AddressSuburb"].ToString();
            //        AddressCity.InnerHtml = theSqlDataReader["AddressCity"].ToString();
            //        AddressProvince.InnerHtml = theSqlDataReader["AddressProvince"].ToString();
            //        AddressAreaCode.InnerHtml = theSqlDataReader["AddressAreaCode"].ToString();
            //        PostalAddress.InnerHtml = theSqlDataReader["PostalAddress"].ToString();
            //        PostalCity.InnerHtml = theSqlDataReader["PostalCity"].ToString();
            //        PostalCode.InnerHtml = theSqlDataReader["PostalCode"].ToString();
            //        HomeNo.InnerHtml = theSqlDataReader["HomeNo"].ToString();
            //        MobileNo.InnerHtml = theSqlDataReader["MobileNo"].ToString();
            //        EmploymentDate.InnerHtml = theSqlDataReader["EmploymentDate"].ToString();
            //        Email.InnerHtml = theSqlDataReader["Email"].ToString();
            //        InsuranceCompany.InnerHtml = theSqlDataReader["InsuranceCompany"].ToString();
            //        PolicyHolder.InnerHtml = theSqlDataReader["PolicyHolder"].ToString();
            //        PolicyNumber.InnerHtml = theSqlDataReader["PolicyNumber"].ToString();
            //        PeriodOfInsuranceFrom.InnerHtml = theSqlDataReader["PeriodOfInsuranceFrom"].ToString();
            //        PeriodOfInsuranceTo.InnerHtml = theSqlDataReader["PeriodOfInsuranceTo"].ToString();
            //    }
            //}
            //else
            //{
            //    // Display any errors
            //}

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.RS.Close();
            //DLdb.DB_Close();

            //Response.End();
        }
    }
}