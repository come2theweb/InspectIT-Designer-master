using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using GoogleMaps.LocationServices;

namespace InspectIT.API
{
    public partial class srv_logCOCCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["uid"] == null)
            {

            }
            else
            {
                string UID = Request.QueryString["uid"];
                string Results = "";

                string COCNumber = Request.QueryString["cocnumber"].ToString();
                string ConsumerName = Request.QueryString["customername"].ToString();
                string ConsumerSurname = Request.QueryString["customersurname"].ToString();
                string TelNo = Request.QueryString["telno"].ToString();
                string AlternateTelNo = Request.QueryString["alttelno"].ToString();
                string EmailAddress = Request.QueryString["emalAddress"].ToString();
                string Street = Request.QueryString["street"].ToString();
                string Suburb = Request.QueryString["suburb"].ToString();
                string City = Request.QueryString["city"].ToString();
                string AreaCode = Request.QueryString["areacode"].ToString();
                string Province = Request.QueryString["province"].ToString();
                string addressToLongLat = Street + ", " + Suburb + ", " + City + ", " + Province;
                string CustomerID = "";

                var latitude = "";
                var longitude = "";
                try
                {
                    var address = addressToLongLat;
                    var locationService = new GoogleLocationService();
                    var point = locationService.GetLatLongFromAddress(address);
                    latitude = point.Latitude.ToString();
                    longitude = point.Longitude.ToString();
                }
                catch (Exception err)
                {

                }

                Global DLdb = new Global();
                DLdb.DB_Connect();

                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "select * from Customers where CustomerEmail = @CustomerEmail";
                DLdb.SQLST2.Parameters.AddWithValue("CustomerEmail", EmailAddress);
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (!theSqlDataReader2.HasRows)
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into Customers (CustomerName,CustomerSurname,CustomerCellNo,CustomerCellNoAlt,CustomerEmail,AddressStreet,AddressSuburb,AddressCity,Province,AddressAreaCode,CustomerPassword,lat,lng) values (@CustomerName,@CustomerSurname,@CustomerCellNo,@CustomerCellNoAlt,@CustomerEmail,@AddressStreet,@AddressSuburb,@AddressCity,@Province,@AddressAreaCode,@CustomerPassword,@lat,@lng); Select Scope_Identity() as CustomerID";
                    DLdb.SQLST.Parameters.AddWithValue("CustomerName", ConsumerName);
                    DLdb.SQLST.Parameters.AddWithValue("CustomerSurname", ConsumerSurname);
                    DLdb.SQLST.Parameters.AddWithValue("CustomerCellNo", TelNo);
                    DLdb.SQLST.Parameters.AddWithValue("CustomerCellNoAlt", AlternateTelNo);
                    DLdb.SQLST.Parameters.AddWithValue("CustomerEmail", EmailAddress);
                    DLdb.SQLST.Parameters.AddWithValue("AddressStreet", Street);
                    DLdb.SQLST.Parameters.AddWithValue("AddressSuburb", Suburb);
                    DLdb.SQLST.Parameters.AddWithValue("AddressCity", City);
                    DLdb.SQLST.Parameters.AddWithValue("Province", Province);
                    DLdb.SQLST.Parameters.AddWithValue("AddressAreaCode", AreaCode);
                    DLdb.SQLST.Parameters.AddWithValue("CustomerPassword", DLdb.CreatePassword(8));
                    DLdb.SQLST.Parameters.AddWithValue("lat", latitude);
                    DLdb.SQLST.Parameters.AddWithValue("lng", longitude);

                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        CustomerID = theSqlDataReader["CustomerID"].ToString();
                    }

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

                    // UPDATE THE CUSTOMER ID
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update COCStatements set CustomerID = @CustomerID, Status = 'Allocated' where COCStatementID = @COCStatementID";
                    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", COCNumber);
                    DLdb.SQLST.Parameters.AddWithValue("CustomerID", CustomerID.ToString());

                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }
                else
                {
                    theSqlDataReader2.Read();
                    
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update Customers set CustomerName = @CustomerName,CustomerSurname = @CustomerSurname,CustomerCellNo = @CustomerCellNo,CustomerCellNoAlt = @CustomerCellNoAlt,CustomerEmail = @CustomerEmail,AddressStreet = @AddressStreet,AddressSuburb = @AddressSuburb,AddressCity = @AddressCity,Province = @Province,AddressAreaCode = @AddressAreaCode,CustomerPassword = @CustomerPassword,lat = @lat,lng = @lng where CustomerID = @CustomerID";
                    DLdb.SQLST.Parameters.AddWithValue("CustomerName", ConsumerName);
                    DLdb.SQLST.Parameters.AddWithValue("CustomerSurname", ConsumerSurname);
                    DLdb.SQLST.Parameters.AddWithValue("CustomerCellNo", TelNo);
                    DLdb.SQLST.Parameters.AddWithValue("CustomerCellNoAlt", AlternateTelNo);
                    DLdb.SQLST.Parameters.AddWithValue("CustomerEmail", EmailAddress);
                    DLdb.SQLST.Parameters.AddWithValue("AddressStreet", Street);
                    DLdb.SQLST.Parameters.AddWithValue("AddressSuburb", Suburb);
                    DLdb.SQLST.Parameters.AddWithValue("AddressCity", City);
                    DLdb.SQLST.Parameters.AddWithValue("Province", Province);
                    DLdb.SQLST.Parameters.AddWithValue("AddressAreaCode", AreaCode);
                    DLdb.SQLST.Parameters.AddWithValue("CustomerPassword", DLdb.CreatePassword(8));
                    DLdb.SQLST.Parameters.AddWithValue("lat", latitude);
                    DLdb.SQLST.Parameters.AddWithValue("lng", longitude);
                    DLdb.SQLST.Parameters.AddWithValue("CustomerID", theSqlDataReader2["CustomerID"].ToString());

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

                    DLdb.RS.Close();
                    

                }

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();

                //string WorkCompletedDate = Request.QueryString["CompletionDate"].ToString();
                //string TypeCOC = Request.QueryString["radio-choice-v-2"].ToString();


                //string InsuranceClaimNo = Request.QueryString["orderno"].ToString();
                //string PropertyNo = Request.QueryString["number"].ToString();
                //string ComplexName = Request.QueryString["complex"].ToString();
               
                //string InstallationDetails = Request.QueryString["details"].ToString();
                //string NonComplianceDetails = Request.QueryString["noncomp"].ToString();
                //string WorkType = Request.QueryString["TickSel"].ToString();

                //string SQL_ST = "update Certificate set CertificateNo = '" + COCNumber + "',InsuranceClaimNo = '" + InsuranceClaimNo + "',WorkCompletedDate = '" + WorkCompletedDate + "',PropertyNo = '" + PropertyNo + "',ComplexName = '" + ComplexName + "',ConsumerName = '" + ConsumerName + "',Street = '" + Street + "',Suburb = '" + Suburb + "',City = '" + City + "',AreaCode = '" + AreaCode + "',TelNo = '" + TelNo + "',AlternateTelNo = '" + AlternateTelNo + "',InstallationDetails = '" + InstallationDetails + "',NonComplianceDetails = '" + NonComplianceDetails + "'";

                //string isCode1 = "0";
                //if (Request.QueryString["isCode1"] != null)
                //{
                //    isCode1 = "1";
                //}
                //string isCode2 = "0";
                //if (Request.QueryString["isCode2"] != null)
                //{
                //    isCode2 = "1";
                //}
                //string isCode3 = "0";
                //if (Request.QueryString["isCode3"] != null)
                //{
                //    isCode3 = "1";
                //}
                //string isCode4 = "0";
                //if (Request.QueryString["isCode4"] != null)
                //{
                //    isCode4 = "1";
                //}
                //string isCode5 = "0";
                //if (Request.QueryString["isCode5"] != null)
                //{
                //    isCode5 = "1";
                //}
                //string isCode6 = "0";
                //if (Request.QueryString["isCode6"] != null)
                //{
                //    isCode6 = "1";
                //}
                //string isCode7 = "0";
                //if (Request.QueryString["isCode7"] != null)
                //{
                //    isCode7 = "1";
                //}
                //string isCode8 = "0";
                //if (Request.QueryString["isCode8"] != null)
                //{
                //    isCode8 = "1";
                //}


                //SQL_ST = SQL_ST + ",IsCode1 = '" + isCode1 + "',IsCode2 = '" + isCode2 + "',IsCode3 = '" + isCode3 + "',IsCode4 = '" + isCode4 + "',IsCode5 = '" + isCode5 + "',IsCode6 = '" + isCode6 + "',IsCode7 = '" + isCode7 + "',IsCode8 = '" + isCode8 + "'";


                
                //// #######
                //if (TypeCOC == "Normal C.O.C")
                //{
                //    SQL_ST = SQL_ST + ",StartedWorkAndCompleted = '1', InspectedWorkAndCompleted = '0'";
                //}
                //else if (TypeCOC == "Sales C.O.C")
                //{
                //    SQL_ST = SQL_ST + ",StartedWorkAndCompleted = '0', InspectedWorkAndCompleted = '1'";
                //}
                //else if (TypeCOC == "Pre-Install C.O.C")
                //{
                //    SQL_ST = SQL_ST + ",StartedWorkAndCompleted = '0', InspectedWorkAndCompleted = '1'";
                //}
                ////#########
                //if (WorkType == "A")
                //{
                //    SQL_ST = SQL_ST + ",StartedWorkAndCompleted = '1', InspectedWorkAndCompleted = '0'";
                //}
                //else if (WorkType == "B")
                //{
                //    SQL_ST = SQL_ST + ",StartedWorkAndCompleted = '0', InspectedWorkAndCompleted = '1'";
                //}

                //SQL_ST = SQL_ST + ",LoggedDate = getdate(), certificatestatusid = '3' where CertificateNo = '" + COCNumber + "' and PlumberID = '" + UID + "'";



                

                //DLdb.RS.Open();
                //DLdb.SQLST.CommandText = SQL_ST;
                //DLdb.SQLST.CommandType = CommandType.Text;
                //DLdb.SQLST.Connection = DLdb.RS;
                //SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.RS.Close();

                Results = "Done";

                Response.Write(Results);
                Response.End();

            }
        }
    }
}