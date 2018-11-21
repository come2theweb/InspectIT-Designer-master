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
    public partial class zImportInspector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_IITOld_Connect();
            DLdb.DB_Connect();
            
            // *************************** AUDITORS ****************************************************
            // *************************** AUDITORS ****************************************************


            Response.Write("Adding Auditors...<br />");

            // Clear the Suppliers table
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "truncate table Auditor";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReaderIIT = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReaderIIT.IsClosed) theSqlDataReaderIIT.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "truncate table AuditorAreas";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReaderIIT = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReaderIIT.IsClosed) theSqlDataReaderIIT.Close();
            DLdb.RS.Close();


            // LOAD SUPPLIER INFORMATION
            DLdb.iitRS.Open();
            DLdb.iitSQLST.CommandText = "select * from Inspector";
            //DLdb.iitSQLST.Parameters.AddWithValue("ID", theSqlDataReader["ID"].ToString());
            DLdb.iitSQLST.CommandType = CommandType.Text;
            DLdb.iitSQLST.Connection = DLdb.iitRS;
            SqlDataReader theSqlDataReader = DLdb.iitSQLST.ExecuteReader();
            
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string AuditorID = "";
                    string idName = "";

                    string isActive = "0";
                    if (theSqlDataReader["Active"].ToString() == "True")
                    {
                        isActive = "1";
                    }

                    string stopPayments = "0";
                    if (theSqlDataReader["StopPayments"].ToString() == "True")
                    {
                        stopPayments = "1";
                    }

                    string accType = "";
                    if (theSqlDataReader["AccountTypeID"].ToString() == "1")
                    {
                        accType = "Cheque";
                    }
                    else
                    {
                        accType = "Savings";
                    }

                    string resProvince = "";
                    string picName = "";
                    DLdb.iitRS2.Open();
                    DLdb.iitSQLST2.CommandText = "select * from Province where ID=@ID";
                    DLdb.iitSQLST2.Parameters.AddWithValue("ID", theSqlDataReader["ResidentialProvinceID"].ToString());
                    DLdb.iitSQLST2.CommandType = CommandType.Text;
                    DLdb.iitSQLST2.Connection = DLdb.iitRS2;
                    SqlDataReader theSqlDataReader2 = DLdb.iitSQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            resProvince = theSqlDataReader2["Name"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.iitSQLST2.Parameters.RemoveAt(0);
                    DLdb.iitRS2.Close();

                    try
                    {
                        if (theSqlDataReader["Photo"].ToString() != "" || theSqlDataReader["Photo"] != DBNull.Value)
                        {
                            idName = theSqlDataReader["ID"].ToString();
                            picName = "__" +idName + ".bmp";
                            var imageBytes = (byte[])theSqlDataReader["Photo"];
                            if (imageBytes.Length > 0)
                            {
                                using (var convertedImage = new Bitmap(new MemoryStream(imageBytes)))
                                {
                                    var fileName = Server.MapPath("~/AuditorImgs/") + "__" + idName + ".bmp";
                                    if (File.Exists(fileName))
                                    {
                                        File.Delete(fileName);
                                    }
                                    convertedImage.Save(fileName);
                                }
                            }
                        }
                        else
                        {
                            picName = "";
                        }
                        
                    }
                    catch (Exception err)
                    {
                        
                    }
                    
                    // INSERT THE AUDITOR
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "INSERT INTO Auditor (regNo, fName, lName, idNo, isActive," +
                                             "startInactiveDate, endInactiveDate, phoneWork, phoneHome, phoneMobile, fax," +
                                             "email, pin, photoFile, addressLine1, addressLine2, province, city,Suburb, areaCode," +
                                             "postalAddress, postalCity, postalCode, companyRegNo, vatRegNo, pastelAccount, invoiceEmail," +
                                             "stopPayments, bankName, accName, accNumber, branchCode, accType,lat,lng,Range,postalProvince,Iidold)" +

                                             "VALUES (@regNo, @fName, @lName, @idNo, @isActive," +
                                             "@startInactiveDate, @endInactiveDate, @phoneWork, @phoneHome, @phoneMobile, @fax," +
                                             "@email, @pin, @photoFile, @addressLine1, @addressLine2, @province, @city,@Suburb, @areaCode," +
                                             "@postalAddress, @postalCity, @postalCode, @companyRegNo, @vatRegNo, @pastelAccount, @invoiceEmail," +
                                             "@stopPayments, @bankName, @accName, @accNumber, @branchCode, @accType,@lat,@lng,@Range,@postalProvince,@Iidold); Select Scope_Identity() as AuditorID";

                    // General Details
                    string oldid = theSqlDataReader["ID"].ToString();
                    DLdb.SQLST.Parameters.AddWithValue("Iidold", oldid);
                    DLdb.SQLST.Parameters.AddWithValue("regNo", theSqlDataReader["RegNo"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("fName", theSqlDataReader["Name"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("lName", theSqlDataReader["Surname"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("idNo", theSqlDataReader["IdNumber"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("isActive", isActive); // Active Checkbox
                    DLdb.SQLST.Parameters.AddWithValue("startInactiveDate", theSqlDataReader["startInactiveDate"].ToString()); // Start inactive date calendar
                    DLdb.SQLST.Parameters.AddWithValue("endInactiveDate", theSqlDataReader["endInactiveDate"].ToString()); // End inactive date calendar
                    DLdb.SQLST.Parameters.AddWithValue("phoneWork", theSqlDataReader["BusinessPhone"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("phoneHome", theSqlDataReader["HomePhone"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("phoneMobile", theSqlDataReader["MobilePhone"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("fax", theSqlDataReader["Fax"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("email", theSqlDataReader["Email"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("pin", theSqlDataReader["PIN"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("photoFile", picName); // File upload of photo

                    // Address
                    DLdb.SQLST.Parameters.AddWithValue("addressLine1", theSqlDataReader["ResidentialAddressLine1"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("addressLine2", theSqlDataReader["ResidentialAddressLine2"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("province", resProvince);
                    DLdb.SQLST.Parameters.AddWithValue("postalProvince", "");
                    DLdb.SQLST.Parameters.AddWithValue("Suburb", theSqlDataReader["ResidentialAddressLine2"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("city", theSqlDataReader["ResidentialCity"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("areaCode", theSqlDataReader["ResidentialCode"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("postalAddress", theSqlDataReader["postalAddress"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("postalCity", theSqlDataReader["postalCity"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("postalCode", theSqlDataReader["postalCode"].ToString());

                    // Business
                    DLdb.SQLST.Parameters.AddWithValue("companyRegNo", theSqlDataReader["companyRegNo"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("vatRegNo", theSqlDataReader["vatRegNo"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("pastelAccount", theSqlDataReader["pastelAccount"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("invoiceEmail", theSqlDataReader["InvoiceEmail"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("stopPayments", stopPayments); // check box to stop payments
                    DLdb.SQLST.Parameters.AddWithValue("bankName", "");
                    DLdb.SQLST.Parameters.AddWithValue("accName", DLdb.Encrypt(theSqlDataReader["accountName"].ToString()));
                    DLdb.SQLST.Parameters.AddWithValue("accNumber", DLdb.Encrypt(theSqlDataReader["accountNumber"].ToString()));
                    DLdb.SQLST.Parameters.AddWithValue("branchCode", DLdb.Encrypt(theSqlDataReader["branchCode"].ToString()));
                    DLdb.SQLST.Parameters.AddWithValue("accType", DLdb.Encrypt(accType)); // Drop down of savings or cheque
                    DLdb.SQLST.Parameters.AddWithValue("lat", "");
                    DLdb.SQLST.Parameters.AddWithValue("lng", "");
                    DLdb.SQLST.Parameters.AddWithValue("Range", "");
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReaderIIT = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReaderIIT.HasRows)
                    {
                        theSqlDataReaderIIT.Read();
                        AuditorID = theSqlDataReaderIIT["AuditorID"].ToString();
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

                    string pass = theSqlDataReader["Pin"].ToString();

                    // General Details
                    DLdb.SQLST.Parameters.AddWithValue("fname", theSqlDataReader["Name"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("lname", theSqlDataReader["Surname"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(pass));
                    DLdb.SQLST.Parameters.AddWithValue("role", "Inspector");
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
                    DLdb.SQLST.CommandText = "update Auditor set UserID = @UserID where AuditorID = @AuditorID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                    DLdb.SQLST.Parameters.AddWithValue("AuditorID", AuditorID);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReaderIIT = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReaderIIT.IsClosed) theSqlDataReaderIIT.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    string AreaID = "";
                    string SubID = "";
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from Area where Name=@Name";
                    DLdb.SQLST.Parameters.AddWithValue("Name", theSqlDataReader["ResidentialCity"].ToString().ToUpper());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReaderIIT = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReaderIIT.HasRows)
                    {
                        while (theSqlDataReaderIIT.Read())
                        {
                            AreaID = theSqlDataReaderIIT["ID"].ToString();
                        }
                    }
                    if (theSqlDataReaderIIT.IsClosed) theSqlDataReaderIIT.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from AreaSuburbs where Name=@Name";
                    DLdb.SQLST.Parameters.AddWithValue("Name", theSqlDataReader["ResidentialAddressLine2"].ToString().ToUpper());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReaderIIT = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReaderIIT.HasRows)
                    {
                        while (theSqlDataReaderIIT.Read())
                        {
                            SubID = theSqlDataReaderIIT["SuburbID"].ToString();
                        }
                    }
                    if (theSqlDataReaderIIT.IsClosed) theSqlDataReaderIIT.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into AuditorAreas (AuditorID,AreaID,SuburbID) values (@AuditorID,@AreaID,@SuburbID)";
                    DLdb.SQLST.Parameters.AddWithValue("AreaID", AreaID);
                    DLdb.SQLST.Parameters.AddWithValue("AuditorID", AuditorID);
                    DLdb.SQLST.Parameters.AddWithValue("SuburbID", SubID);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReaderIIT = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReaderIIT.IsClosed) theSqlDataReaderIIT.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
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