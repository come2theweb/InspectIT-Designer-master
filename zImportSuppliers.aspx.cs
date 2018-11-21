using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;

namespace InspectIT
{
    public partial class zImportSuppliers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_IITOld_Connect();
            DLdb.DB_Connect();

            // *************************** SUPPLIERS ****************************************************
            // *************************** SUPPLIERS ****************************************************

            Response.Write("Adding Suppliers...<br />");

            // Clear the Suppliers table
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "truncate table suppliers";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReaderIIT = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReaderIIT.IsClosed) theSqlDataReaderIIT.Close();
            DLdb.RS.Close();

            // LOAD SUPPLIER INFORMATION
            DLdb.iitRS.Open();
            DLdb.iitSQLST.CommandText = "select * from Reseller";
            //DLdb.iitSQLST.Parameters.AddWithValue("ID", theSqlDataReader["ID"].ToString());
            DLdb.iitSQLST.CommandType = CommandType.Text;
            DLdb.iitSQLST.Connection = DLdb.iitRS;
            SqlDataReader theSqlDataReader = DLdb.iitSQLST.ExecuteReader();
            
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {

                    Response.Flush();
                    Server.ScriptTimeout = 10000000;
                    string SupplierID = "";

                    string isActive = "0";
                    if (theSqlDataReader["Active"].ToString() == "True")
                    {
                        isActive = "1";
                    }
                    
                    // INSERT THE SUPPLIERS
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "INSERT INTO Suppliers (SupplierName, SupplierRegNo, SupplierVatRegNo, SupplierWebsite, SupplierEmail, SupplierContactNo," +
                        "AddressLine1, AddressLine2, Province, CitySuburb, AreaCode, PostalAddress, PostalCity, PostalCode,TotalNumber,StartRange,EndRange,InvoiceNumber,PostalProvince,SIDOLD,isActive)" +

                        "VALUES (@SupplierName, @SupplierRegNo, @SupplierVatRegNo, @SupplierWebsite, @SupplierEmail, @SupplierContactNo," +
                        "@AddressLine1, @AddressLine2, @Province, @CitySuburb, @AreaCode, @PostalAddress, @PostalCity, @PostalCode,@TotalNumber,@StartRange,@EndRange,@InvoiceNumber,@PostalProvince,@SIDOLD,@isActive) Select Scope_Identity() as SupplierID";

                    DLdb.SQLST.Parameters.AddWithValue("SupplierName", theSqlDataReader["CompanyName"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("SupplierRegNo", theSqlDataReader["CompanyRegNo"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("SupplierVatRegNo", theSqlDataReader["VatRegNo"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("SupplierWebsite", "");
                    DLdb.SQLST.Parameters.AddWithValue("SupplierEmail", theSqlDataReader["Email"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("SupplierContactNo", theSqlDataReader["ContactMobilePhone"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("AddressLine1", theSqlDataReader["BusinessAddressLine1"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("AddressLine2", theSqlDataReader["BusinessAddressLine2"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Province", theSqlDataReader["ProvinceID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("CitySuburb", theSqlDataReader["BusinessCity"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("AreaCode", theSqlDataReader["BusinessCode"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalAddress", theSqlDataReader["PostalAddress"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalCity", theSqlDataReader["PostalCity"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalCode", theSqlDataReader["PostalCode"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("TotalNumber", theSqlDataReader["MinCertificates"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("StartRange", "0");
                    DLdb.SQLST.Parameters.AddWithValue("EndRange", "0");
                    DLdb.SQLST.Parameters.AddWithValue("InvoiceNumber", theSqlDataReader["PastelAccount"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("PostalProvince", theSqlDataReader["ProvinceID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("SIDOLD", theSqlDataReader["ID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("isActive", isActive);
                    
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReaderIIT = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReaderIIT.HasRows)
                    {
                        theSqlDataReaderIIT.Read();
                        SupplierID = theSqlDataReaderIIT["SupplierID"].ToString();
                    }

                    if (theSqlDataReaderIIT.IsClosed) theSqlDataReaderIIT.Close();
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

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "INSERT INTO Users (fname,lname, password, role, email) VALUES (@fname, @lname, @password, @role, @email); Select Scope_Identity() as UserID";

                    string pass = theSqlDataReader["Password"].ToString();

                    // General Details
                    DLdb.SQLST.Parameters.AddWithValue("fname", theSqlDataReader["ContactName"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("lname", theSqlDataReader["ContactSurname"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(pass));
                    DLdb.SQLST.Parameters.AddWithValue("role", "Supplier");
                    DLdb.SQLST.Parameters.AddWithValue("email", theSqlDataReader["Email"].ToString());

                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReaderIIT = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReaderIIT.HasRows)
                    {
                        theSqlDataReaderIIT.Read();
                        UserID = theSqlDataReaderIIT["UserID"].ToString();
                    }

                    if (theSqlDataReaderIIT.IsClosed) theSqlDataReaderIIT.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    //Update userid in auditors table
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update Suppliers set UserID = @UserID where SupplierID = @SupplierID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                    DLdb.SQLST.Parameters.AddWithValue("SupplierID", SupplierID);

                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReaderIIT = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReaderIIT.IsClosed) theSqlDataReaderIIT.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                    
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.iitSQLST.Parameters.RemoveAt(0);
            DLdb.iitRS.Close();

            


            DLdb.DB_IITOld_Close();
        }
    }
}