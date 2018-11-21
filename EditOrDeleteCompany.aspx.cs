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
    public partial class EditOrDeleteCompany : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                DLdb.DB_Connect();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Companies where CompanyID=@CompanyID";
                DLdb.SQLST.Parameters.AddWithValue("CompanyID", Request.QueryString["CompanyID"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();


                while (theSqlDataReader.Read())
                {
                    // General Details
                    CompanyName.Text = theSqlDataReader["CompanyName"].ToString();
                    CompanyRegNo.Text = theSqlDataReader["CompanyRegNo"].ToString();
                    CompanyWebsite.Text = theSqlDataReader["CompanyWebsite"].ToString();
                    CompanyEmail.Text = theSqlDataReader["CompanyEmail"].ToString();
                    CompanyContactNo.Text = theSqlDataReader["CompanyContactNo"].ToString();
                    AddressLine1.Text = theSqlDataReader["AddressLine1"].ToString();
                    AddressLine2.Text = theSqlDataReader["AddressLine2"].ToString();
                    Province.Text = theSqlDataReader["Province"].ToString();
                    CitySuburb.Text = theSqlDataReader["CitySuburb"].ToString();
                    AreaCode.Text = theSqlDataReader["AreaCode"].ToString();
                    PostalAddress.Text = theSqlDataReader["PostalAddress"].ToString();
                    PostalCity.Text = theSqlDataReader["PostalCity"].ToString();
                    PostalCode.Text = theSqlDataReader["PostalCode"].ToString();

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
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "UPDATE Companies SET CompanyName=@CompanyName, CompanyRegNo=@CompanyRegNo, CompanyWebsite=@CompanyWebsite," +
                "CompanyEmail=@CompanyEmail, CompanyContactNo=@CompanyContactNo, AddressLine1=@AddressLine1, AddressLine2=@AddressLine2," +
                "Province=@Province, CitySuburb=@CitySuburb, AreaCode=@AreaCode, PostalAddress=@PostalAddress, PostalCity=@PostalCity, PostalCode=@PostalCode WHERE CompanyID=@CompanyID";
            DLdb.SQLST.Parameters.AddWithValue("CompanyID", Request.QueryString["CompanyID"]);

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