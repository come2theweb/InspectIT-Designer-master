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
    public partial class AddCompany : System.Web.UI.Page
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

        protected void btn_add_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO Companies (CompanyName, CompanyRegNo, CompanyWebsite, CompanyEmail, CompanyContactNo," +
                "AddressLine1, AddressLine2, Province, CitySuburb, AreaCode, PostalAddress, PostalCity, PostalCode)" +

                "VALUES (@CompanyName, @CompanyRegNo, @CompanyWebsite, @CompanyEmail, @CompanyContactNo," +
                "@AddressLine1, @AddressLine2, @Province, @CitySuburb, @AreaCode, @PostalAddress, @PostalCity, @PostalCode)";

            // General Details
            DLdb.SQLST.Parameters.AddWithValue("CompanyName", CompanyName.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyRegNo", CompanyRegNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyWebsite", CompanyWebsite.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyEmail", CompanyEmail.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CompanyContactNo", CompanyContactNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressLine1", AddressLine1.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressLine2", AddressLine2.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Province", Province.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CitySuburb", CitySuburb.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AreaCode", AreaCode.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalAddress", PostalAddress.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalCity", PostalCity.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PostalCode", PostalCode.Text.ToString());

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
            Response.Redirect("ViewCompany.aspx");
        }
    }
}