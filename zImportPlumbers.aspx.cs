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
    public partial class zImportPlumbers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_PIRB_Connect();

            // LOAD PLUMBER INFORMATION
            DLdb.pirbRS.Open();
            DLdb.pirbSQLST.CommandText = "select * from plumbers where RegNo not in (select regno from users where regno is not null)";
            //DLdb.SQLST.Parameters.AddWithValue("ID", theSqlDataReader["ID"].ToString());
            DLdb.pirbSQLST.CommandType = CommandType.Text;
            DLdb.pirbSQLST.Connection = DLdb.pirbRS;
            SqlDataReader theSqlDataReader = DLdb.pirbSQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    // CHECK IF EXISTS
                    DLdb.DB_Connect();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from users where regno = @regno";
                    DLdb.SQLST2.Parameters.AddWithValue("regno", theSqlDataReader["RegNo"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        //    // UPDATE
                        //    DLdb.RS3.Open();
                        //    DLdb.SQLST3.CommandText = "update Users set fname = @fname,isSuspended=@isSuspended,InsuranceEndDate=@InsuranceEndDate,Notice=@Notice,InsuranceStartDate=@InsuranceStartDate,InsurancePolicyHolder=@InsurancePolicyHolder,InsurancePolicyNo=@InsurancePolicyNo,InsuranceCompany=@InsuranceCompany,IdNo = @IdNo,lname = @lname,password = @password,role = @role,email = @email,contact = @contact,ResidentialStreet = @ResidentialStreet,ResidentialSuburb = @ResidentialSuburb,ResidentialCity = @ResidentialCity,ResidentialCode = @ResidentialCode,Province = @Province,PostalAddress = @PostalAddress,PostalCity = @PostalCity,PostalCode = @PostalCode,company = @company,regno = @regno,PIRBID = @PIRBID,NoCOCPurchases = @NoCOCPurchases,RegistrationEnd = @RegistrationEnd where RegNo = @RegNo";
                        //    DLdb.SQLST3.Parameters.AddWithValue("PIRBID", theSqlDataReader["PlumberID"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("role", "Staff");
                        //    DLdb.SQLST3.Parameters.AddWithValue("company", theSqlDataReader["CompanyID"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("RegNo", theSqlDataReader["RegNo"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("password", DLdb.Encrypt(theSqlDataReader["PIN"].ToString()));
                        //    DLdb.SQLST3.Parameters.AddWithValue("fname", theSqlDataReader["Name"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("lname", theSqlDataReader["Surname"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("IdNo", theSqlDataReader["IdNo"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("ResidentialStreet", theSqlDataReader["Res_Street"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("ResidentialSuburb", theSqlDataReader["Res_Suburb"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("ResidentialCity", theSqlDataReader["Res_City"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("ResidentialCode", theSqlDataReader["Res_Code"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("PostalAddress", theSqlDataReader["Post_Add"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("PostalCity", theSqlDataReader["Post_City"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("PostalCode", theSqlDataReader["Post_Code"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("contact", theSqlDataReader["Mob_Phone"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("Email", theSqlDataReader["Email"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("Province", theSqlDataReader["Res_Province"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("NoCOCPurchases", "10");
                        //    DLdb.SQLST3.Parameters.AddWithValue("RegistrationEnd", theSqlDataReader["Reg_end"].ToString());

                        //    DLdb.SQLST3.Parameters.AddWithValue("InsuranceCompany", theSqlDataReader["InsuranceCompany"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("InsurancePolicyNo", theSqlDataReader["InsurancePolicyNo"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("InsurancePolicyHolder", theSqlDataReader["InsurancePolicyHolder"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("InsuranceStartDate", theSqlDataReader["InsuranceStartDate"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("InsuranceEndDate", theSqlDataReader["InsuranceEndDate"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("Notice", theSqlDataReader["Notes"].ToString());
                        //    DLdb.SQLST3.Parameters.AddWithValue("isSuspended", theSqlDataReader["RegistrationSuspended"].ToString());

                        //    DLdb.SQLST3.CommandType = CommandType.Text;
                        //    DLdb.SQLST3.Connection = DLdb.RS3;
                        //    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();


                        //    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.RS3.Close();
                    }
                    else
                    {
                        try
                        {
                            DLdb.RS3.Open();
                            // DLdb.SQLST3.CommandText = "insert into users (PIRBID,CompanyID,RegNo,PIN,Title,Name,Surname,IdNo,Photo,ResidentialStreet,ResidentialSuburb,ResidentialCity,ResidentialCode,PostalAddress,PostalCity,PostalCode,BusinessPhone,HomePhone,MobilePhone,Fax,Email,TradeTestNo,TradeVenue,Documents,Notes,RegistrationStart,RegistrationEnd,DateCreated,RegistrationSuspended,SANS10254,SANS10252A,SANS10252B,SANS10400,LABLaws,InsuranceCompany,InsurancePolicyNo,InsurancePolicyHolder,InsuranceStartDate,InsuranceEndDate,SANS10106,Province,LoggedCertificates,ExpiryNotificationDate,Nationality,Language,Gender,Equity,AlternativeIDType,DisabilityStatus,SocioeconomicStatus,CitizenResidentStatus,Alternate,PreviousSurname,PreviousAlternate,PreviousAlternativeIDType,DateUpdated,IsDeceasedOrResigned)" + //,ApplicationProcess,PostalSuburb,DateofBirth
                            //     "values (@PIRBID,@CompanyID,@RegNo,@PIN,@Title,@Name,@Surname,@IdNo,@Photo,@ResidentialStreet,@ResidentialSuburb,@ResidentialCity,@ResidentialCode,@PostalAddress,@PostalCity,@PostalCode,@BusinessPhone,@HomePhone,@MobilePhone,@Fax,@Email,@TradeTestNo,@TradeVenue,@Documents,@Notes,@RegistrationStart,@RegistrationEnd,@DateCreated,@RegistrationSuspended,@SANS10254,@SANS10252A,@SANS10252B,@SANS10400,@LABLaws,@InsuranceCompany,@InsurancePolicyNo,@InsurancePolicyHolder,@InsuranceStartDate,@InsuranceEndDate,@SANS10106,@Province,@LoggedCertificates,@ExpiryNotificationDate,@Nationality,@Language,@Gender,@Equity,@AlternativeIDType,@DisabilityStatus,@SocioeconomicStatus,@CitizenResidentStatus,@Alternate,@PreviousSurname,@PreviousAlternate,@PreviousAlternativeIDType,@DateUpdated,@IsDeceasedOrResigned)"; //,@PostalSuburb,@ApplicationProcess,@DateofBirth
                            DLdb.SQLST3.CommandText = "insert into Users (UserID,isSuspended,Notice,InsuranceCompany,InsuranceEndDate,InsuranceStartDate,InsurancePolicyHolder,InsurancePolicyNo,fname,IdNo,lname,password,role,email,contact,ResidentialStreet,ResidentialSuburb,ResidentialCity,ResidentialCode,Province,PostalAddress,PostalCity,PostalCode,company,regno,PIRBID,NoCOCPurchases,RegistrationEnd) " +
                                "values (@UserID,@isSuspended,@Notice,@InsuranceCompany,@InsuranceEndDate,@InsuranceStartDate,@InsurancePolicyHolder,@InsurancePolicyNo,@fname,@IdNo,@lname,@password,@role,@email,@contact,@ResidentialStreet,@ResidentialSuburb,@ResidentialCity,@ResidentialCode,@Province,@PostalAddress,@PostalCity,@PostalCode,@company,@regno,@PIRBID,@NoCOCPurchases,@RegistrationEnd)";
                            DLdb.SQLST3.Parameters.AddWithValue("PIRBID", theSqlDataReader["PlumberID"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("role", "Staff");
                            DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["PlumberID"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("company", theSqlDataReader["CompanyID"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("RegNo", theSqlDataReader["RegNo"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("password", DLdb.Encrypt(theSqlDataReader["PIN"].ToString()));
                            DLdb.SQLST3.Parameters.AddWithValue("fname", theSqlDataReader["Name"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("lname", theSqlDataReader["Surname"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("IdNo", theSqlDataReader["IdNo"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("ResidentialStreet", theSqlDataReader["ResidentialStreet"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("ResidentialSuburb", theSqlDataReader["ResidentialSuburb"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("ResidentialCity", theSqlDataReader["ResidentialCity"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("ResidentialCode", theSqlDataReader["ResidentialCode"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("PostalAddress", theSqlDataReader["PostalAddress"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("PostalCity", theSqlDataReader["PostalCity"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("PostalCode", theSqlDataReader["PostalCode"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("contact", theSqlDataReader["MobilePhone"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("Email", theSqlDataReader["Email"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("Province", theSqlDataReader["Province"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("NoCOCPurchases", "10");
                            DLdb.SQLST3.Parameters.AddWithValue("RegistrationEnd", theSqlDataReader["RegistrationEnd"].ToString());

                            DLdb.SQLST3.Parameters.AddWithValue("InsuranceCompany", theSqlDataReader["InsuranceCompany"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("InsurancePolicyNo", theSqlDataReader["InsurancePolicyNo"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("InsurancePolicyHolder", theSqlDataReader["InsurancePolicyHolder"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("InsuranceStartDate", theSqlDataReader["InsuranceStartDate"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("InsuranceEndDate", theSqlDataReader["InsuranceEndDate"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("Notice", theSqlDataReader["Notes"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("isSuspended", theSqlDataReader["RegistrationSuspended"].ToString());
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
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                        // ADD
                        
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    Response.Flush();
                    Server.ScriptTimeout = 10000000;

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