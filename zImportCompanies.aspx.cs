using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace InspectIT
{
    public partial class zImportCompanies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_PIRB_Connect();

            // LOAD PLUMBER INFORMATION
            DLdb.pirbRS.Open();
            DLdb.pirbSQLST.CommandText = "select * from companys";
            //DLdb.SQLST.Parameters.AddWithValue("ID", theSqlDataReader["ID"].ToString());
            DLdb.pirbSQLST.CommandType = CommandType.Text;
            DLdb.pirbSQLST.Connection = DLdb.pirbRS;
            SqlDataReader theSqlDataReader = DLdb.pirbSQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    //string PlumbersList = "<tr>" +
            //"<td>" + theSqlDataReader["ID"].ToString() + "</td>" +
            //                            "<td>" + theSqlDataReader["companycode"].ToString() + "</td>" +
            //                            "<td>" + theSqlDataReader["name"].ToString() + "</td>" +
            //                            "<td>" + theSqlDataReader["regno"].ToString() + "</td>" +
            //                          "<td>" + theSqlDataReader["ResidentialStreet"].ToString() + "</td>" +
            //                          "<td>" + theSqlDataReader["ResidentialSuburb"].ToString() + "</td>" +
            //                          "<td>" + theSqlDataReader["ResidentialCity"].ToString() + "</td>" +
            //                          "<td>" + theSqlDataReader["ResidentialCode"].ToString() + "</td>" +
            //                          "<td>" + theSqlDataReader["PostalAddress"].ToString() + "</td>" +
            //                        "<td>" + theSqlDataReader["PostalCity"].ToString() + "</td>" +
            //                          "<td>" + theSqlDataReader["PostalCode"].ToString() + "</td>" +
            //                          "<td>" + theSqlDataReader["primarycontact"].ToString() + "</td>" +
            //                          "<td>" + theSqlDataReader["BusinessPhone"].ToString() + "</td>" +
            //                          "<td>" + theSqlDataReader["Fax"].ToString() + "</td>" +
            //                          "<td>" + theSqlDataReader["Email"].ToString() + "</td>" +
            //                          "<td>" + theSqlDataReader["InsurancePolicyNo"].ToString() + "</td>" +
            //                          "<td>" + theSqlDataReader["insurancecompany"].ToString() + "</td>" +
            //                          "<td>" + theSqlDataReader["InsuranceStartDate"].ToString() + "</td>" +
            //                      "</tr>";


                    DLdb.DB_Connect();
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "insert into Companies (CompanyID,CompanyCode,CompanyName,Mobile,CompanyRegNo,AddressLine1,Suburb,City,Province,AreaCode,PostalAddress,PostalCity,PostalCode,PrimaryContact,CompanyContactNo,Fax,CompanyEmail,InsurancePolicyNo,InsuranceCompany,InsuranceStartDate)" +
                        "values (@CompanyID,@CompanyCode,@CompanyName,@Mobile,@CompanyRegNo,@AddressLine1,@Suburb,@City,@Province,@AreaCode,@PostalAddress,@PostalCity,@PostalCode,@PrimaryContact,@CompanyContactNo,@Fax,@CompanyEmail,@InsurancePolicyNo,@InsuranceCompany,@InsuranceStartDate)";
                    DLdb.SQLST3.Parameters.AddWithValue("CompanyID", theSqlDataReader["CompanyID"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("CompanyCode", theSqlDataReader["companycode"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("CompanyName", theSqlDataReader["Name"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("CompanyRegNo", theSqlDataReader["RegNo"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("AddressLine1", theSqlDataReader["ResidentialStreet"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("Suburb", theSqlDataReader["ResidentialSuburb"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("City", theSqlDataReader["ResidentialCity"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("Province", theSqlDataReader["ResidentialProvince"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("AreaCode", theSqlDataReader["ResidentialCode"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("PostalAddress", theSqlDataReader["PostalAddress"].ToString());
                    // DLdb.SQLST3.Parameters.AddWithValue("PostalSuburb", theSqlDataReader["PostalSuburb"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("PostalCity", theSqlDataReader["PostalCity"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("PostalCode", theSqlDataReader["PostalCode"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("PrimaryContact", theSqlDataReader["PrimaryContact"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("CompanyContactNo", theSqlDataReader["BusinessPhone"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("Fax", theSqlDataReader["Fax"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("CompanyEmail", theSqlDataReader["Email"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("Mobile", theSqlDataReader["Mobile"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("InsuranceCompany", theSqlDataReader["InsuranceCompany"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("InsurancePolicyNo", theSqlDataReader["InsurancePolicyNo"].ToString());
                    //DLdb.SQLST3.Parameters.AddWithValue("InsurancePolicyHolder", theSqlDataReader["InsurancePolicyHolder"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("InsuranceStartDate", theSqlDataReader["InsuranceStartDate"].ToString());
                    //DLdb.SQLST3.Parameters.AddWithValue("InsuranceEndDate", theSqlDataReader["InsuranceEndDate"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();


                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();
                    DLdb.DB_Close();

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.pirbRS.Close();

            DLdb.DB_PIRB_Close();
        }
    }
}