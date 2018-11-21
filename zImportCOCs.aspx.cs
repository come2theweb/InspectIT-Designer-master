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
    public partial class zImportCOCs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_IITOld_Connect();
            DLdb.DB_Connect();

            // *************************** COC's ****************************************************
            // *************************** COC's ****************************************************

            Response.Write("Starting Import COC's...<br />");

            string COCIStatementID = "";
            Boolean doesExist = false;
                        
            string completedDatte = "";
            string insuranceClaimNumber = "";
            string meOrSupervisor = "";
            string isComp = "";
            string cocNumberTemp = "";
            // LOAD SUPPLIER INFORMATION
            DLdb.iitRS5.Open();
            DLdb.iitSQLST5.CommandText = "select * from tempCOCRange";
            DLdb.iitSQLST5.CommandType = CommandType.Text;
            DLdb.iitSQLST5.Connection = DLdb.iitRS5;
            SqlDataReader iittheSqlDataReader5 = DLdb.iitSQLST5.ExecuteReader();

            if (iittheSqlDataReader5.HasRows)
            {
                while (iittheSqlDataReader5.Read())
                {
                    cocNumberTemp = iittheSqlDataReader5["COCNumber"].ToString();

                    DLdb.iitRS.Open();
                    // DLdb.iitSQLST.CommandText = "select top 1 * from Certificate where CertificateNo not in (select COCNumber from COCStatements)";
                    DLdb.iitSQLST.CommandText = "select * from Certificate where CertificateNo=@CertificateNo";
                    DLdb.iitSQLST.Parameters.AddWithValue("CertificateNo", iittheSqlDataReader5["COCNumber"].ToString());
                    DLdb.iitSQLST.CommandType = CommandType.Text;
                    DLdb.iitSQLST.Connection = DLdb.iitRS;
                    SqlDataReader theSqlDataReader = DLdb.iitSQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            if (theSqlDataReader["WorkCompletedDate"].ToString() != "" || theSqlDataReader["WorkCompletedDate"] != DBNull.Value)
                            {
                                completedDatte = theSqlDataReader["WorkCompletedDate"].ToString();
                                isComp = "1";
                            }
                            else
                            {
                                completedDatte = "";
                                isComp = "0";
                            }

                            insuranceClaimNumber = theSqlDataReader["insuranceClaimNo"].ToString();
                            meOrSupervisor = theSqlDataReader["StartedWorkAndCompleted"].ToString();
                            // ************* GET USER INFO *****************************
                            string PlumberRegNo = "";
                            //// GET REGNO
                            //DLdb.iitRS2.Open();
                            //DLdb.iitSQLST2.CommandText = "Select * from Plumbers where PlumberID = @PlumberID";
                            //DLdb.iitSQLST2.Parameters.AddWithValue("PlumberID", theSqlDataReader["PlumberID"].ToString());
                            //DLdb.iitSQLST2.CommandType = CommandType.Text;
                            //DLdb.iitSQLST2.Connection = DLdb.iitRS2;
                            //SqlDataReader iittheSqlDataReader2 = DLdb.iitSQLST2.ExecuteReader();

                            //if (iittheSqlDataReader2.HasRows)
                            //{
                            //    iittheSqlDataReader2.Read();
                            //    PlumberRegNo = iittheSqlDataReader2["regno"].ToString();
                            //}

                            //if (iittheSqlDataReader2.IsClosed) iittheSqlDataReader2.Close();
                            //DLdb.iitSQLST2.Parameters.RemoveAt(0);
                            //DLdb.iitRS2.Close();

                            string UserID = "";
                            // GET USERID
                            DLdb.RS2.Open();
                            //DLdb.SQLST2.CommandText = "Select * from users where regno = @regno";
                            DLdb.SQLST2.CommandText = "Select * from users where PIRBID = @PIRBID";
                            DLdb.SQLST2.Parameters.AddWithValue("PIRBID", theSqlDataReader["PlumberID"].ToString());
                            //DLdb.SQLST2.Parameters.AddWithValue("regno", PlumberRegNo);
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                theSqlDataReader2.Read();
                                UserID = theSqlDataReader2["UserID"].ToString();
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            // ************* GET SUPPLIER INFO *****************************
                            string SupplierID = "0";
                            // GET SUPPLIERID
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "Select * from suppliers where SIDOLD = @SIDOLD";
                            DLdb.SQLST2.Parameters.AddWithValue("SIDOLD", theSqlDataReader["ResellerID"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                theSqlDataReader2.Read();
                                SupplierID = theSqlDataReader2["SupplierID"].ToString();
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            // ************* GET INSPECTOR INFO *****************************
                            // GET INSPECTORID
                            string InspectorID = "0";
                            string inspectorUserID = "";
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "Select * from Auditor where IIDOld = @IIDOld";
                            DLdb.SQLST2.Parameters.AddWithValue("IIDOld", theSqlDataReader["InspectorID"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                theSqlDataReader2.Read();
                                InspectorID = theSqlDataReader2["AuditorID"].ToString();
                                inspectorUserID = theSqlDataReader2["UserID"].ToString();
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            // ************* ADD/CHECK CUSTOMER *****************************
                            string CustomerID = null;
                            // ADD CUSTOMER IF NOT CUTOMER EXISTS
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "select * from Customers where CustomerName = @CustomerName";
                            DLdb.SQLST2.Parameters.AddWithValue("CustomerName", theSqlDataReader["ConsumerName"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (!theSqlDataReader2.HasRows)
                            {
                                DLdb.RS3.Open();
                                DLdb.SQLST3.CommandText = "insert into Customers (CustomerName,CustomerSurname,CustomerCellNo,CustomerCellNoAlt,CustomerEmail,AddressStreet,AddressSuburb,AddressCity,Province,AddressAreaCode,CustomerPassword,lat,lng) values (@CustomerName,@CustomerSurname,@CustomerCellNo,@CustomerCellNoAlt,@CustomerEmail,@AddressStreet,@AddressSuburb,@AddressCity,@Province,@AddressAreaCode,@CustomerPassword,@lat,@lng); Select Scope_Identity() as CustomerID;";
                                DLdb.SQLST3.Parameters.AddWithValue("CustomerName", theSqlDataReader["ConsumerName"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("CustomerSurname", "");
                                DLdb.SQLST3.Parameters.AddWithValue("CustomerCellNo", theSqlDataReader["TelNo"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("CustomerCellNoAlt", "");
                                DLdb.SQLST3.Parameters.AddWithValue("CustomerEmail", "");
                                DLdb.SQLST3.Parameters.AddWithValue("AddressStreet", theSqlDataReader["ComplexName"].ToString() + " " + theSqlDataReader["PropertyNo"].ToString() + " " + theSqlDataReader["Street"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("AddressSuburb", theSqlDataReader["Suburb"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("AddressCity", theSqlDataReader["City"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("Province", "");
                                DLdb.SQLST3.Parameters.AddWithValue("AddressAreaCode", theSqlDataReader["AreaCode"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("CustomerPassword", DLdb.Encrypt(DLdb.CreatePassword(8)));
                                DLdb.SQLST3.Parameters.AddWithValue("lat", "");
                                DLdb.SQLST3.Parameters.AddWithValue("lng", "");

                                DLdb.SQLST3.CommandType = CommandType.Text;
                                DLdb.SQLST3.Connection = DLdb.RS3;
                                SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                                if (theSqlDataReader3.HasRows)
                                {
                                    theSqlDataReader3.Read();
                                    CustomerID = theSqlDataReader3["CustomerID"].ToString();
                                }

                                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
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
                            else
                            {

                                theSqlDataReader2.Read();

                                CustomerID = theSqlDataReader2["CustomerID"].ToString();

                                DLdb.RS3.Open();
                                DLdb.SQLST3.CommandText = "update Customers set CustomerName=@CustomerName,CustomerSurname=@CustomerSurname,CustomerCellNo=@CustomerCellNo,CustomerCellNoAlt=@CustomerCellNoAlt,CustomerEmail=@CustomerEmail,AddressStreet=@AddressStreet,AddressSuburb=@AddressSuburb,AddressCity=@AddressCity,Province=@Province,AddressAreaCode=@AddressAreaCode,lat=@lat,lng=@lng where CustomerID=@CustomerID";
                                DLdb.SQLST3.Parameters.AddWithValue("CustomerID", theSqlDataReader2["CustomerID"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("CustomerName", theSqlDataReader["ConsumerName"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("CustomerSurname", "");
                                DLdb.SQLST3.Parameters.AddWithValue("CustomerCellNo", theSqlDataReader["TelNo"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("CustomerCellNoAlt", "");
                                DLdb.SQLST3.Parameters.AddWithValue("CustomerEmail", "");
                                DLdb.SQLST3.Parameters.AddWithValue("AddressStreet", theSqlDataReader["ComplexName"].ToString() + " " + theSqlDataReader["PropertyNo"].ToString() + " " + theSqlDataReader["Street"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("AddressSuburb", theSqlDataReader["Suburb"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("AddressCity", theSqlDataReader["City"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("Province", "");
                                DLdb.SQLST3.Parameters.AddWithValue("AddressAreaCode", theSqlDataReader["AreaCode"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("CustomerPassword", DLdb.Encrypt(DLdb.CreatePassword(8)));
                                DLdb.SQLST3.Parameters.AddWithValue("lat", "");
                                DLdb.SQLST3.Parameters.AddWithValue("lng", "");

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
                                DLdb.RS3.Close();

                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            // ************* GET CETIFICATE INFORMATION *****************************
                            string CertType = "";
                            // GET CETIFICATE TYPE
                            DLdb.iitRS2.Open();
                            DLdb.iitSQLST2.CommandText = "select * from CertificateType where ID = @ID";
                            DLdb.iitSQLST2.Parameters.AddWithValue("ID", theSqlDataReader["CertificateTypeID"].ToString());
                            DLdb.iitSQLST2.CommandType = CommandType.Text;
                            DLdb.iitSQLST2.Connection = DLdb.iitRS2;
                            SqlDataReader iittheSqlDataReader2 = DLdb.iitSQLST2.ExecuteReader();

                            if (iittheSqlDataReader2.HasRows)
                            {
                                iittheSqlDataReader2.Read();
                                CertType = iittheSqlDataReader2["Description"].ToString();
                            }

                            if (iittheSqlDataReader2.IsClosed) iittheSqlDataReader2.Close();
                            DLdb.iitSQLST2.Parameters.RemoveAt(0);
                            DLdb.iitRS2.Close();

                            string CertStatus = "";
                            // GET STATUS TYPE
                            DLdb.iitRS2.Open();
                            DLdb.iitSQLST2.CommandText = "select * from CertificateStatus where ID = @ID";
                            DLdb.iitSQLST2.Parameters.AddWithValue("ID", theSqlDataReader["CertificateStatusID"].ToString());
                            DLdb.iitSQLST2.CommandType = CommandType.Text;
                            DLdb.iitSQLST2.Connection = DLdb.iitRS2;
                            iittheSqlDataReader2 = DLdb.iitSQLST2.ExecuteReader();

                            if (iittheSqlDataReader2.HasRows)
                            {
                                iittheSqlDataReader2.Read();
                                CertStatus = iittheSqlDataReader2["Name"].ToString();
                            }

                            if (iittheSqlDataReader2.IsClosed) iittheSqlDataReader2.Close();
                            DLdb.iitSQLST2.Parameters.RemoveAt(0);
                            DLdb.iitRS2.Close();

                            // MAP CODES
                            string InstallationTypeIDs = "";
                            if (theSqlDataReader["isCode1"].ToString() == "True")
                            {
                                if (InstallationTypeIDs == "")
                                {
                                    InstallationTypeIDs = "1";
                                }
                                else
                                {
                                    InstallationTypeIDs += ",1";
                                }
                            }
                            if (theSqlDataReader["isCode2"].ToString() == "True")
                            {
                                if (InstallationTypeIDs == "")
                                {
                                    InstallationTypeIDs = "2";
                                }
                                else
                                {
                                    InstallationTypeIDs += ",2";
                                }
                            }
                            if (theSqlDataReader["isCode3"].ToString() == "True")
                            {
                                if (InstallationTypeIDs == "")
                                {
                                    InstallationTypeIDs = "3";
                                }
                                else
                                {
                                    InstallationTypeIDs += ",3";
                                }
                            }
                            if (theSqlDataReader["isCode4"].ToString() == "True")
                            {
                                if (InstallationTypeIDs == "")
                                {
                                    InstallationTypeIDs = "4";
                                }
                                else
                                {
                                    InstallationTypeIDs += ",4";
                                }
                            }
                            if (theSqlDataReader["isCode5"].ToString() == "True")
                            {
                                if (InstallationTypeIDs == "")
                                {
                                    InstallationTypeIDs = "5";
                                }
                                else
                                {
                                    InstallationTypeIDs += ",5";
                                }
                            }
                            if (theSqlDataReader["isCode6"].ToString() == "True")
                            {
                                if (InstallationTypeIDs == "")
                                {
                                    InstallationTypeIDs = "6";
                                }
                                else
                                {
                                    InstallationTypeIDs += ",6";
                                }
                            }
                            if (theSqlDataReader["isCode7"].ToString() == "True")
                            {
                                if (InstallationTypeIDs == "")
                                {
                                    InstallationTypeIDs = "7";
                                }
                                else
                                {
                                    InstallationTypeIDs += ",7";
                                }
                            }
                            if (theSqlDataReader["isCode8"].ToString() == "True")
                            {
                                if (InstallationTypeIDs == "")
                                {
                                    InstallationTypeIDs = "8";
                                }
                                else
                                {
                                    InstallationTypeIDs += ",8";
                                }
                            }

                            // INSPECTOR REPORT
                            string isaRefix = "0";
                            string status = "";
                            string inspectionDate = "";
                            string refixDate = "";
                            //DLdb.iitRS2.Open();
                            //DLdb.iitSQLST2.CommandText = "select * from InspectionReport where ID = @ID";
                            //DLdb.iitSQLST2.Parameters.AddWithValue("ID", theSqlDataReader["InspectionReportID"].ToString());
                            //DLdb.iitSQLST2.CommandType = CommandType.Text;
                            //DLdb.iitSQLST2.Connection = DLdb.iitRS2;
                            //iittheSqlDataReader2 = DLdb.iitSQLST2.ExecuteReader();

                            //if (iittheSqlDataReader2.HasRows)
                            //{
                            //    iittheSqlDataReader2.Read();
                            //    isaRefix = iittheSqlDataReader2["isRefix"].ToString();
                            //    inspectionDate = iittheSqlDataReader2["InspectionSubmittionDate"].ToString();
                            //    refixDate = iittheSqlDataReader2["RefixByDate"].ToString();

                            //    DLdb.iitRS3.Open();
                            //    DLdb.iitSQLST3.CommandText = "select * from InspectionStatus where ID = @ID";
                            //    DLdb.iitSQLST3.Parameters.AddWithValue("ID", iittheSqlDataReader2["InspectionStatusID"].ToString());
                            //    DLdb.iitSQLST3.CommandType = CommandType.Text;
                            //    DLdb.iitSQLST3.Connection = DLdb.iitRS3;
                            //    SqlDataReader iittheSqlDataReader3 = DLdb.iitSQLST3.ExecuteReader();

                            //    if (iittheSqlDataReader3.HasRows)
                            //    {
                            //        iittheSqlDataReader3.Read();
                            //        status = iittheSqlDataReader3["Description"].ToString();
                            //    }

                            //    if (iittheSqlDataReader3.IsClosed) iittheSqlDataReader3.Close();
                            //    DLdb.iitSQLST3.Parameters.RemoveAt(0);
                            //    DLdb.iitRS3.Close();
                            //}

                            //if (iittheSqlDataReader2.IsClosed) iittheSqlDataReader2.Close();
                            //DLdb.iitSQLST2.Parameters.RemoveAt(0);
                            //DLdb.iitRS2.Close();

                            // CREATE COC
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "insert into COCStatements (COCStatementID,UserID,CustomerID,COCNumber,AuditorID,Type,DatePurchased,DateLogged,DateAudited,DateRefix,DateInspection,DateComplete,Status,isRefix,isPaid,SupplierID,NonComplianceDetails,CreateDate) values (@COCStatementID,@UserID,@CustomerID,@COCNumber,@AuditorID,@Type,@DatePurchased,@DateLogged,@DateAudited,@DateRefix,@DateInspection,@DateComplete,@Status,@isRefix,@isPaid,@SupplierID,@NonComplianceDetails,@CreateDate); Select Scope_Identity() as COCStatementID";
                            DLdb.SQLST2.Parameters.AddWithValue("UserID", UserID);
                            DLdb.SQLST2.Parameters.AddWithValue("CustomerID", CustomerID);
                            DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", theSqlDataReader["CertificateNo"].ToString());
                            DLdb.SQLST2.Parameters.AddWithValue("COCNumber", theSqlDataReader["CertificateNo"].ToString());
                            DLdb.SQLST2.Parameters.AddWithValue("AuditorID", InspectorID);
                            DLdb.SQLST2.Parameters.AddWithValue("Type", CertType);
                            DLdb.SQLST2.Parameters.AddWithValue("DatePurchased", theSqlDataReader["DateCreated"].ToString());
                            DLdb.SQLST2.Parameters.AddWithValue("DateAudited", "");
                            DLdb.SQLST2.Parameters.AddWithValue("DateLogged", theSqlDataReader["LoggedDate"].ToString());
                            DLdb.SQLST2.Parameters.AddWithValue("DateRefix", refixDate);
                            DLdb.SQLST2.Parameters.AddWithValue("DateInspection", inspectionDate);
                            DLdb.SQLST2.Parameters.AddWithValue("DateComplete", "");
                            DLdb.SQLST2.Parameters.AddWithValue("Status", CertStatus);
                            DLdb.SQLST2.Parameters.AddWithValue("isRefix", isaRefix);
                            DLdb.SQLST2.Parameters.AddWithValue("isPaid", "1");

                            DLdb.SQLST2.Parameters.AddWithValue("SupplierID", SupplierID);
                            DLdb.SQLST2.Parameters.AddWithValue("NonComplianceDetails", theSqlDataReader["NonComplianceDetails"].ToString());
                            DLdb.SQLST2.Parameters.AddWithValue("CreateDate", theSqlDataReader["DateCreated"].ToString());

                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                theSqlDataReader2.Read();
                                COCIStatementID = theSqlDataReader2["COCStatementID"].ToString();
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);

                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);

                            DLdb.RS2.Close();

                            // CREATE INSTALLATION TYPES
                            foreach (string id in InstallationTypeIDs.Split(','))
                            {
                                // CHECK IF EXISTS AND UPDATE OR ADD NEW
                                DLdb.RS2.Open();
                                DLdb.SQLST2.CommandText = "select * from COCInstallations where TypeID = @TypeID and COCStatementID = @COCStatementID";
                                DLdb.SQLST2.Parameters.AddWithValue("TypeID", id);
                                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", COCIStatementID);
                                DLdb.SQLST2.CommandType = CommandType.Text;
                                DLdb.SQLST2.Connection = DLdb.RS2;
                                theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                                if (theSqlDataReader2.HasRows)
                                {
                                    DLdb.RS3.Open();
                                    DLdb.SQLST3.CommandText = "update COCInstallations set isActive = '1' where TypeID = @TypeID and COCStatementID = @COCStatementID";
                                    DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", COCIStatementID);
                                    DLdb.SQLST3.Parameters.AddWithValue("TypeID", id);
                                    DLdb.SQLST3.CommandType = CommandType.Text;
                                    DLdb.SQLST3.Connection = DLdb.RS3;
                                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                                    DLdb.SQLST3.Parameters.RemoveAt(0);
                                    DLdb.SQLST3.Parameters.RemoveAt(0);
                                    DLdb.RS3.Close();
                                }
                                else
                                {
                                    DLdb.RS3.Open();
                                    DLdb.SQLST3.CommandText = "insert into COCInstallations (TypeID,COCStatementID) values (@TypeID,@COCStatementID)";
                                    DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", COCIStatementID);
                                    DLdb.SQLST3.Parameters.AddWithValue("TypeID", id);
                                    DLdb.SQLST3.CommandType = CommandType.Text;
                                    DLdb.SQLST3.Connection = DLdb.RS3;
                                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                                    DLdb.SQLST3.Parameters.RemoveAt(0);
                                    DLdb.SQLST3.Parameters.RemoveAt(0);
                                    DLdb.RS3.Close();

                                }

                                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.RS2.Close();
                            }

                            //select * from [dbo].[ReportItems]
                            //select * from [dbo].[ReportItemInspectorPhotos]
                            string reportTypeIDs = "";
                            string reportStatusID = "";
                            string reportNote = "";
                            string questionID = "";
                            string reportisFixed = "";
                            string reportItemID = "";
                            string reportImages = "";
                            string Question = "";
                            string CommentID = "";

                            //DLdb.iitRS2.Open();
                            //DLdb.iitSQLST2.CommandText = "select * from report where InspectionReportID = @InspectionReportID";
                            //DLdb.iitSQLST2.Parameters.AddWithValue("InspectionReportID", theSqlDataReader["InspectionReportID"].ToString());
                            //DLdb.iitSQLST2.CommandType = CommandType.Text;
                            //DLdb.iitSQLST2.Connection = DLdb.iitRS2;
                            //iittheSqlDataReader2 = DLdb.iitSQLST2.ExecuteReader();

                            //if (iittheSqlDataReader2.HasRows)
                            //{
                            //    while (iittheSqlDataReader2.Read())
                            //    {
                            //        reportStatusID = iittheSqlDataReader2["reportStatusID"].ToString();
                            //        reportNote = iittheSqlDataReader2["Note"].ToString();

                            //        DLdb.iitRS3.Open();
                            //        DLdb.iitSQLST3.CommandText = "select * from ReportItems where reportID = @reportID and AnswerID='1'";
                            //        DLdb.iitSQLST3.Parameters.AddWithValue("reportID", iittheSqlDataReader2["ID"].ToString());
                            //        DLdb.iitSQLST3.CommandType = CommandType.Text;
                            //        DLdb.iitSQLST3.Connection = DLdb.iitRS3;
                            //        SqlDataReader iittheSqlDataReader3 = DLdb.iitSQLST3.ExecuteReader();

                            //        if (iittheSqlDataReader3.HasRows)
                            //        {
                            //            while (iittheSqlDataReader3.Read())
                            //            {

                            //                questionID = iittheSqlDataReader3["QuestionID"].ToString();
                            //                reportisFixed = iittheSqlDataReader3["IsFixed"].ToString();
                            //                reportItemID = iittheSqlDataReader3["ID"].ToString();

                            //                DLdb.iitRS4.Open();
                            //                DLdb.iitSQLST4.CommandText = "select * from Questions where ID = @ID";
                            //                DLdb.iitSQLST4.Parameters.AddWithValue("ID", questionID);
                            //                DLdb.iitSQLST4.CommandType = CommandType.Text;
                            //                DLdb.iitSQLST4.Connection = DLdb.iitRS4;
                            //                SqlDataReader iittheSqlDataReader4 = DLdb.iitSQLST4.ExecuteReader();

                            //                if (iittheSqlDataReader4.HasRows)
                            //                {
                            //                    while (iittheSqlDataReader4.Read())
                            //                    {
                            //                        Question = iittheSqlDataReader4["Question"].ToString();
                            //                    }
                            //                }

                            //                if (iittheSqlDataReader4.IsClosed) iittheSqlDataReader4.Close();
                            //                DLdb.iitSQLST4.Parameters.RemoveAt(0);
                            //                DLdb.iitRS4.Close();

                            //                // GET REPORT STATUS
                            //                //1   New
                            //                //2   Incomplete
                            //                //3   Failed
                            //                //4   Complete
                            //                string reportStatus = "";
                            //                if (reportStatusID.ToString() == "1")
                            //                {
                            //                    reportStatus = "New";
                            //                }
                            //                else if (reportStatusID.ToString() == "2")
                            //                {
                            //                    reportStatus = "Incomplete";
                            //                }
                            //                else if (reportStatusID.ToString() == "3")
                            //                {
                            //                    reportStatus = "Failed";
                            //                }
                            //                else if (reportStatusID.ToString() == "4")
                            //                {
                            //                    reportStatus = "Complete";
                            //                }

                            //                // ADD COC REVIEW DETAILS
                            //                DLdb.RS2.Open();
                            //                DLdb.SQLST2.CommandText = "INSERT INTO COCReviews (COCStatementID,UserID,Name,Reference,ReferenceImage, Comment,TypeID,FieldID,status,CommentTemplateID) VALUES (@COCStatementID,@UserID,@Name,@Reference,@ReferenceImage,@Comment,@TypeID,@FieldID,@status,@CommentTemplateID); Select Scope_Identity() as CommentID";
                            //                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", COCIStatementID);
                            //                DLdb.SQLST2.Parameters.AddWithValue("UserID", UserID);
                            //                DLdb.SQLST2.Parameters.AddWithValue("Name", Question);
                            //                DLdb.SQLST2.Parameters.AddWithValue("Comment", reportNote);
                            //                DLdb.SQLST2.Parameters.AddWithValue("TypeID", "0");
                            //                DLdb.SQLST2.Parameters.AddWithValue("FieldID", "0");
                            //                DLdb.SQLST2.Parameters.AddWithValue("status", reportStatus);
                            //                DLdb.SQLST2.Parameters.AddWithValue("CommentTemplateID", "0");
                            //                DLdb.SQLST2.Parameters.AddWithValue("Reference", "");
                            //                DLdb.SQLST2.Parameters.AddWithValue("ReferenceImage", "");

                            //                DLdb.SQLST2.CommandType = CommandType.Text;
                            //                DLdb.SQLST2.Connection = DLdb.RS2;
                            //                theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            //                if (theSqlDataReader2.HasRows)
                            //                {
                            //                    theSqlDataReader2.Read();
                            //                    CommentID = theSqlDataReader2["CommentID"].ToString();
                            //                }

                            //                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            //                DLdb.SQLST2.Parameters.RemoveAt(0);
                            //                DLdb.SQLST2.Parameters.RemoveAt(0);
                            //                DLdb.SQLST2.Parameters.RemoveAt(0);
                            //                DLdb.SQLST2.Parameters.RemoveAt(0);
                            //                DLdb.SQLST2.Parameters.RemoveAt(0);
                            //                DLdb.SQLST2.Parameters.RemoveAt(0);
                            //                DLdb.SQLST2.Parameters.RemoveAt(0);
                            //                DLdb.SQLST2.Parameters.RemoveAt(0);
                            //                DLdb.SQLST2.Parameters.RemoveAt(0);
                            //                DLdb.SQLST2.Parameters.RemoveAt(0);
                            //                DLdb.RS2.Close();

                            //            }
                            //        }

                            //        if (iittheSqlDataReader3.IsClosed) iittheSqlDataReader3.Close();
                            //        DLdb.iitSQLST3.Parameters.RemoveAt(0);
                            //        DLdb.iitRS3.Close();


                            //        // GET IMAGES
                            //        DLdb.iitRS3.Open();
                            //        DLdb.iitSQLST3.CommandText = "select * from ReportItemInspectorPhotos where ReportItemID = @ReportItemID";
                            //        DLdb.iitSQLST3.Parameters.AddWithValue("ReportItemID", iittheSqlDataReader2["ID"].ToString());
                            //        DLdb.iitSQLST3.CommandType = CommandType.Text;
                            //        DLdb.iitSQLST3.Connection = DLdb.iitRS3;
                            //        iittheSqlDataReader3 = DLdb.iitSQLST3.ExecuteReader();

                            //        if (iittheSqlDataReader3.HasRows)
                            //        {
                            //            while (iittheSqlDataReader3.Read())
                            //            {
                            //                string idName = iittheSqlDataReader3["ID"].ToString() + iittheSqlDataReader3["ReportItemID"].ToString();
                            //                var imageBytes = (byte[])iittheSqlDataReader3["Photo"];
                            //                if (imageBytes.Length > 0)
                            //                {
                            //                    using (var convertedImage = new Bitmap(new MemoryStream(imageBytes)))
                            //                    {
                            //                        var fileName = Server.MapPath("~/AuditorImgs/") + idName + ".bmp";
                            //                        if (File.Exists(fileName))
                            //                        {
                            //                            File.Delete(fileName);
                            //                        }
                            //                        convertedImage.Save(fileName);

                            //                        // SAVE IMAGES
                            //                        DLdb.RS2.Open();
                            //                        DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,CommetID,isFile) values (@imgsrc,@UserID,@FieldID,@CommentID,'1')";
                            //                        DLdb.SQLST2.Parameters.AddWithValue("imgsrc", fileName);
                            //                        DLdb.SQLST2.Parameters.AddWithValue("UserID", UserID);
                            //                        DLdb.SQLST2.Parameters.AddWithValue("FieldID", "0");
                            //                        DLdb.SQLST2.Parameters.AddWithValue("CommentID", CommentID);
                            //                        DLdb.SQLST2.CommandType = CommandType.Text;
                            //                        DLdb.SQLST2.Connection = DLdb.RS2;
                            //                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            //                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            //                        DLdb.SQLST2.Parameters.RemoveAt(0);
                            //                        DLdb.SQLST2.Parameters.RemoveAt(0);
                            //                        DLdb.SQLST2.Parameters.RemoveAt(0);
                            //                        DLdb.SQLST2.Parameters.RemoveAt(0);
                            //                        DLdb.RS2.Close();

                            //                    }
                            //                }

                            //            }
                            //        }

                            //        if (iittheSqlDataReader3.IsClosed) iittheSqlDataReader3.Close();
                            //        DLdb.iitSQLST3.Parameters.RemoveAt(0);
                            //        DLdb.iitRS3.Close();
                            //    }
                            //}

                            //if (iittheSqlDataReader2.IsClosed) iittheSqlDataReader2.Close();
                            //DLdb.iitSQLST2.Parameters.RemoveAt(0);
                            //DLdb.iitRS2.Close();


                            // POPULATE COC DETAILS
                            string workcompletedby = "";
                            if (meOrSupervisor == "True")
                            {
                                workcompletedby = "Me";
                            }
                            else if (meOrSupervisor == "False")
                            {
                                workcompletedby = "Supervised";
                            }
                            else
                            {
                                workcompletedby = "";
                            }
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "insert into COCStatementDetails (COCStatementID,CompletedDate,COCType,InsuranceCompany,PolicyHolder,PolicyNumber,isBank,Periodofinsurancefrom,periodofinsuranceto,descriptionofwork,WorkCompleteby) values (@COCStatementID,@CompletedDate,@COCType,@InsuranceCompany,@PolicyHolder,@PolicyNumber,@isBank,@Periodofinsurancefrom,@periodofinsuranceto,@descriptionofwork,@WorkCompleteby)";
                            DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", COCIStatementID);
                            DLdb.SQLST2.Parameters.AddWithValue("CompletedDate", completedDatte);
                            DLdb.SQLST2.Parameters.AddWithValue("COCType", "Normal");
                            DLdb.SQLST2.Parameters.AddWithValue("InsuranceCompany", "");
                            DLdb.SQLST2.Parameters.AddWithValue("PolicyHolder", "");
                            DLdb.SQLST2.Parameters.AddWithValue("PolicyNumber", insuranceClaimNumber);
                            DLdb.SQLST2.Parameters.AddWithValue("isBank", "");
                            DLdb.SQLST2.Parameters.AddWithValue("Periodofinsurancefrom", "");
                            DLdb.SQLST2.Parameters.AddWithValue("periodofinsuranceto", "");
                            DLdb.SQLST2.Parameters.AddWithValue("descriptionofwork", theSqlDataReader["InstallationDetails"].ToString());
                            DLdb.SQLST2.Parameters.AddWithValue("WorkCompleteby", workcompletedby);
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            if (theSqlDataReader["InspectionReportID"] != DBNull.Value)
                            {
                                string cocInspecStatus = "Auditing";
                                if (isComp == "1")
                                {
                                    cocInspecStatus = "Completed";
                                }
                                // ADD AUDIT IF NEEDED
                                DLdb.RS2.Open();
                                DLdb.SQLST2.CommandText = "insert into COCInspectors (COCStatementID,UserID,isComplete,CompletedOn,Invoice,TotalAmount,Description,InspectionDate,Status,RefixComments,Picture,Quality,isReconned) values (@COCStatementID,@UserID,@isComplete,@CompletedOn,@Invoice,@TotalAmount,@Description,@InspectionDate,@Status,@RefixComments,@Picture,@Quality,@isReconned)";
                                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", COCIStatementID);
                                DLdb.SQLST2.Parameters.AddWithValue("UserID", inspectorUserID);
                                DLdb.SQLST2.Parameters.AddWithValue("isComplete", isComp);
                                DLdb.SQLST2.Parameters.AddWithValue("CompletedOn", completedDatte);
                                DLdb.SQLST2.Parameters.AddWithValue("Invoice", "");
                                DLdb.SQLST2.Parameters.AddWithValue("TotalAmount", "");
                                DLdb.SQLST2.Parameters.AddWithValue("Description", CertType);
                                DLdb.SQLST2.Parameters.AddWithValue("InspectionDate", inspectionDate);
                                DLdb.SQLST2.Parameters.AddWithValue("Status", cocInspecStatus);
                                DLdb.SQLST2.Parameters.AddWithValue("RefixComments", "");
                                DLdb.SQLST2.Parameters.AddWithValue("Picture", "");
                                DLdb.SQLST2.Parameters.AddWithValue("Quality", "");
                                DLdb.SQLST2.Parameters.AddWithValue("isReconned", "0");
                                DLdb.SQLST2.CommandType = CommandType.Text;
                                DLdb.SQLST2.Connection = DLdb.RS2;
                                theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.RS2.Close();

                                // UPDATE COC with audit
                                DLdb.RS2.Open();
                                DLdb.SQLST2.CommandText = "Update COCStatements set isAudit = '1', [status] = @status where COCStatementID = @COCStatementID";
                                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", COCIStatementID);
                                DLdb.SQLST2.Parameters.AddWithValue("status", cocInspecStatus);
                                DLdb.SQLST2.CommandType = CommandType.Text;
                                DLdb.SQLST2.Connection = DLdb.RS2;
                                theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.RS2.Close();


                            }

                            // Response.Flush();
                            Server.ScriptTimeout = 10000000;

                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.iitSQLST.Parameters.RemoveAt(0);
                    DLdb.iitRS.Close();

                    DLdb.iitRS.Open();
                    DLdb.iitSQLST.CommandText = "delete from tempCOCRange where COCNumber='" + cocNumberTemp + "'";
                    DLdb.iitSQLST.CommandType = CommandType.Text;
                    DLdb.iitSQLST.Connection = DLdb.iitRS;
                    theSqlDataReader = DLdb.iitSQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.iitRS.Close();
                }
            }

            if (iittheSqlDataReader5.IsClosed) iittheSqlDataReader5.Close();
            DLdb.iitRS5.Close();

          


            DLdb.DB_IITOld_Close();
            

            Response.Write("Completed Import COC's...<br />");

        }
    }
}