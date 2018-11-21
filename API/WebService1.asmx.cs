using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace InspectIT.API
{
    public class details
    {
        public string error { get; set; }
        public string nonLogged { get; set; }
        public string permitted { get; set; }
        public string permittedPurchase { get; set; }
        public string signature { get; set; }
    }
    public class CustomUser
    {
        //public int UserId { get; set; }
        //public string Name { get; set; }
        //public string Address { get; set; }
        //public int Age { get; set; }
        public string companyID { get; set; }
        public string companyName { get; set; }
        public string licensedCount { get; set; }
        public string nonLicensedCount { get; set; }
        public string Edit { get; set; }
        public string delete { get; set; }
    }

    public class TableUser
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string surname { get; set; }
        public string regno { get; set; }
        public string passwords { get; set; }
        public string active { get; set; }
        public string designation { get; set; }
        public string edit { get; set; }
        
    }

    public class comapnyDatatableLoading
    {
        public string companyID { get; set; }
        public string companyName { get; set; }
        public string licensedCount { get; set; }
        public string nonLicensedCount { get; set; }
        public string Edit { get; set; }
        public string delete { get; set; }
    }
    

        public class AssessDetails
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Result { get; set; }
        public string Attachment { get; set; }
        public string comms { get; set; }
    }

    public class performanceStatusEdit
    {
        public string Date { get; set; }
        public string PerformanceType { get; set; }
        public string Details { get; set; }
        public string PerformancePointAllocation { get; set; }
        public string Attachment { get; set; }
        public string endDate { get; set; }
        public string hasEndDate { get; set; }
    }

    public class apprenticeMentorshipEdit
    {
        public string regno { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string type { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string attachment { get; set; }
    }


    public class userDetails
    {
        public string regno { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string userid { get; set; }
    }

    public class documentEdit
    {
        public string DocumentAttached { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }

    public class cpdActivityView
    {
        public string Attachment { get; set; }
        public string comment { get; set; }
        public string CertificateDate { get; set; }
        public string productCode { get; set; }
        public string NoPoints { get; set; }
        public string Activity { get; set; }
        public string Category { get; set; }
    }


    public class CPDActivities
    {
        public string startDate { get; set; }
        public string Points { get; set; }
        public string Activity { get; set; }
        public string Category { get; set; }
        public string CPDActivityID { get; set; }
        public string regno { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }

    public class companyDetailsNewRegistration
    {
        public string primaryContact { get; set; }
        public string CompanyVAt { get; set; }
        public string empPostalAddress { get; set; }
        public string empPostalProvince { get; set; }
        public string empPostalCity { get; set; }
        public string empPostalSuburb { get; set; }
        public string empPostalCode { get; set; }
        public string empAddress { get; set; }
        public string empCity { get; set; }
        public string empProvince { get; set; }
        public string empSuburb { get; set; }
        public string empWorkPhone { get; set; }
        public string empMobile { get; set; }
        public string empEmailaddress { get; set; }
    }

    public class PlumberDetails
    {
        public string plumberFullName { get; set; }
        public string plumberRegNo { get; set; }
        public string plumberEmail { get; set; }
        public string plumberContact { get; set; }
    }

    public class order
    {
        public string otp { get; set; }
        public string oid { get; set; }
    }

    public class editReviewDetail
    {
        public string status { get; set; }
        public string type { get; set; }
        public string subtype { get; set; }
        public string question { get; set; }
        public string comment { get; set; }
        public string reference { get; set; }
        public string referenceMedia { get; set; }
        public string commentMedia { get; set; }
        public string installTypeDrop { get; set; }
        public string subTypeDrop { get; set; }
    }

    public class addReviewTemplate
    {
        public string installTypes { get; set; }
        public string reviewTemplates { get; set; }
    }

    public class PurchaseCOCAmountChanged
    {
        public string error { get; set; }
        public string vatValue { get; set; }
        public string total { get; set; }
        public string costCoc { get; set; }
        public string deliveryCost { get; set; }
    }

    public class ReviewTemplateDetails
    {
        public string comment { get; set; }
        public string reference { get; set; }
        public string installationType { get; set; }
        public string subtypes { get; set; }
        public string subtypesselected { get; set; }
        public string question { get; set; }
        public string referenceImgs { get; set; }
        public string commentImgs { get; set; }
    }

    public class CocDetails
    {
        public string cocNumber { get; set; }
        public string completeDate { get; set; }
        public string telNumber { get; set; }
        public string street { get; set; }
        public string suburb { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string customerName { get; set; }
        public string customerSurname { get; set; }
        public string customerEmail { get; set; }
        public string altTelNumber { get; set; }
        public string descriptionOfWork { get; set; }
        public string nonComplianceDetails { get; set; }
        public string AorB { get; set; }
        public string auditDate { get; set; }
        public string refixDate { get; set; }
        public string quality { get; set; }
        public string inspecImg { get; set; }
        public string completedImgs { get; set; }
        public string isSuspended { get; set; }
        public string paperCOC { get; set; }
        public string isRefix { get; set; }
        public string isPlumberSubmitted { get; set; }
        public string isPlumberLogged { get; set; }
        public string isPaper { get; set; }
        public string Status { get; set; }
        public string hideAudit { get; set; }
        public string installationTypes { get; set; }
        public string plumberName { get; set; }
        public string plumberRegisNumber { get; set; }
        public string refixViews { get; set; }
        public string inspecName { get; set; }
        public string inspecEmail { get; set; }
        public string inspecContact { get; set; }
        public string inspecImage { get; set; }
        public string plumberComments { get; set; }
        public string plumberDatePosted { get; set; }
        public string lastComment { get; set; }
        public string inspecComments { get; set; }
        public string inspecDatePosted { get; set; }
        public string noMoreFixs { get; set; }
    }

    public class CocDetailsInspecSide
    {
        public string cocNumber { get; set; }
        public string completeDate { get; set; }
        public string telNumber { get; set; }
        public string street { get; set; }
        public string suburb { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string customerName { get; set; }
        public string customerSurname { get; set; }
        public string customerEmail { get; set; }
        public string altTelNumber { get; set; }
        public string descriptionOfWork { get; set; }
        public string nonComplianceDetails { get; set; }
        public string AorB { get; set; }
        public string auditDate { get; set; }
        public string refixDate { get; set; }
        public string quality { get; set; }
        public string inspecImg { get; set; }
        public string completedImgs { get; set; }
        public string isSuspended { get; set; }
        public string paperCOC { get; set; }
        public string isRefix { get; set; }
        public string isPlumberSubmitted { get; set; }
        public string isPlumberLogged { get; set; }
        public string isPaper { get; set; }
        public string Status { get; set; }
        public string hideAudit { get; set; }
        public string installationTypes { get; set; }
        public string plumberName { get; set; }
        public string plumberRegisNumber { get; set; }
        public string refixViews { get; set; }
        public string inspecName { get; set; }
        public string inspecEmail { get; set; }
        public string inspecContact { get; set; }
        public string inspecImage { get; set; }
        public string plumberComments { get; set; }
        public string plumberDatePosted { get; set; }
        public string lastComment { get; set; }
        public string inspecComments { get; set; }
        public string inspecDatePosted { get; set; }
        public string noMoreFixs { get; set; }
        public string plumberEmail { get; set; }
        public string plumberContact { get; set; }
        public string subInvBtn { get; set; }
        public string countRefixesFixed { get; set; }
        public string countRefixes { get; set; }
        public string isInvoiceSubmitted { get; set; }
        public string isInspectorSubmitted { get; set; }
        public string isRefixs { get; set; }
        public string auditorID { get; set; }
        public string invAmountDisp { get; set; }
        public string plumberImg { get; set; }
        public string auidtHistoryCommentsPosted { get; set; }
        public string lastprivatecomm { get; set; }
        public string admindatepost { get; set; }
        public string inspectorDatePostedpriv { get; set; }
        public string adminscomm { get; set; }
        public string inspectorCommentspriv { get; set; }
        public string inspecnamepriv { get; set; }
        public string adnames { get; set; }
        public string scheduledTime { get; set; }
        public string scheduledDate { get; set; }
        public string clientContactedStatus { get; set; }
        public string plumbercontactedStatus { get; set; }
        public string pdf { get; set; }
        public string showreportbtn { get; set; }
        public string isClosedRefix { get; set; }
    }
    
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public void GetTableDataUsersSearched(string roleId)
        {
            // At this point you can call to your database to get the data
            // but I will just populate a sample collection in code
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DateTime now = DateTime.Now;
            // var Page = new comapnyDatatableLoading();
            // IList<CustomUser> studentList = new List<CustomUser>();
            IList<TableUser> userList = new List<TableUser>();
            //int length = Convert.ToInt32(displaystart) + Convert.ToInt32(displayLength);
            string sb = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT top 10 * FROM Users where (fname like '%"+ roleId + "%') or (lname like '%" + roleId + "%') or (email like '%" + roleId + "%') or (regno like '%" + roleId + "%')";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string designations = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from PlumberDesignations where PlumberID=@PlumberID";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        designations += theSqlDataReader2["Designation"].ToString();
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    sb += "<tr>" +
                    "<td>" + theSqlDataReader["regno"].ToString() + "</td>" +
                    "<td>" + theSqlDataReader["fname"].ToString() + "</td>" +
                    "<td>" + theSqlDataReader["lname"].ToString() + "</td>" +
                    "<td>" + designations + "</td>" +
                    "<td>" + theSqlDataReader["email"].ToString() + "</td>" +
                    "<td>" + DLdb.Decrypt(theSqlDataReader["password"].ToString()) + "</td>" +
                    "<td>" + theSqlDataReader["isactive"].ToString() + "</td>" +
                    "<td><a href='EditOrDeleteUser.aspx?UserID=" + theSqlDataReader["UserID"].ToString() + "'><div class='btn btn-sm btn-primary'><i class='fa fa-pencil'></i></div></a></td>" +
                    "</tr>";
                    // userList.Add(new TableUser() { UserId = theSqlDataReader["UserId"].ToString(), designation = designations, Name = theSqlDataReader["fname"].ToString(), email = theSqlDataReader["email"].ToString(), surname = theSqlDataReader["lname"].ToString(), passwords = DLdb.Decrypt(theSqlDataReader["password"].ToString()), regno = theSqlDataReader["regno"].ToString(), active = theSqlDataReader["isactive"].ToString(), edit = "<a href='EditOrDeleteUser.aspx?UserID=" + theSqlDataReader["UserID"].ToString() + "'><div class='btn btn-sm btn-primary'><i class='fa fa-pencil'></i></div></a>" });
                    //

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            // return userList;
            Context.Response.Write(sb);
            Context.Response.End();
        }

        [WebMethod]
        public void GetTableDataCompaniesSearched(string roleId)
        {
            // At this point you can call to your database to get the data
            // but I will just populate a sample collection in code
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DateTime now = DateTime.Now;
            // var Page = new comapnyDatatableLoading();
            // IList<CustomUser> studentList = new List<CustomUser>();
            IList<TableUser> userList = new List<TableUser>();
            //int length = Convert.ToInt32(displaystart) + Convert.ToInt32(displayLength);
            string sb = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT top 10 * FROM companies where CompanyName like '%" + roleId + "%'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    int LicensedCount = 0;
                    int NonLicensedCOunt = 0;

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from users where company=@company";
                    DLdb.SQLST2.Parameters.AddWithValue("company", theSqlDataReader["companyid"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "select * from PlumberDesignations where PlumberID=@PlumberID";
                            DLdb.SQLST3.Parameters.AddWithValue("PlumberID", theSqlDataReader2["UserID"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {
                                    if (theSqlDataReader3["Designation"].ToString() == "Licensed Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Director Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Master Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Qualified Plumber")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Learner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Technical Operator Practitioner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Technical Assistant Practitioner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                }
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    sb += "<tr>" +
                    "<td>" + theSqlDataReader["CompanyID"].ToString() + "</td>" +
                    "<td>" + theSqlDataReader["CompanyName"].ToString() + "</td>" +
                    "<td>" + LicensedCount + "</td>" +
                    "<td>" + NonLicensedCOunt + "</td>" +
                    "<td><a href='companiesEdit?id=" + DLdb.Encrypt(theSqlDataReader["CompanyID"].ToString()) + "'><div class='btn btn-sm btn-primary' title='Edit'><i class='fa fa-pencil'></i></div></a>" +
                    "<div class='btn  btn-sm btn-danger' onclick='deleteconf('DeleteCompanies.aspx?op=del&id=" + theSqlDataReader["CompanyID"].ToString() + "')'><i class='fa fa-trash'></i></div></td>" +
                    "</tr>";
                    // userList.Add(new TableUser() { UserId = theSqlDataReader["UserId"].ToString(), designation = designations, Name = theSqlDataReader["fname"].ToString(), email = theSqlDataReader["email"].ToString(), surname = theSqlDataReader["lname"].ToString(), passwords = DLdb.Decrypt(theSqlDataReader["password"].ToString()), regno = theSqlDataReader["regno"].ToString(), active = theSqlDataReader["isactive"].ToString(), edit = "<a href='EditOrDeleteUser.aspx?UserID=" + theSqlDataReader["UserID"].ToString() + "'><div class='btn btn-sm btn-primary'><i class='fa fa-pencil'></i></div></a>" });
                    //

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            // return userList;
            Context.Response.Write(sb);
            Context.Response.End();
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string GetTableDataUsers()
        {
            var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
            var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
            var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);
            var sortOrder = HttpContext.Current.Request.Params["sSortDir_0"].ToString(CultureInfo.CurrentCulture);
            var roleId = HttpContext.Current.Request.Params["roleId"].ToString(CultureInfo.CurrentCulture);
          //  var searched = HttpContext.Current.Request.Params["search"].ToString(CultureInfo.CurrentCulture);

            var records = GetRecordsFromDatabaseWithFilterUsers(displayStart.ToString(), displayLength.ToString()).ToList();
            if (records == null)
            {
                return string.Empty;
            }

            var orderedResults = sortOrder == "asc"
                            ? records.OrderBy(o => o.UserId)
                            : records.OrderByDescending(o => o.UserId);
            //var itemsToSkip = displayStart == 0
            //                  ? 0
            //                  : displayStart + 1;
            var itemsToSkip = 0;
            var pagedResults = orderedResults.Skip(itemsToSkip).Take(displayLength).ToList();
            var hasMoreRecords = false;


            var sb = new StringBuilder();
            sb.Append(@"{" + "\"sEcho\": " + echo.ToString() + ",");
            sb.Append("\"recordsTotal\":4620,");
            sb.Append("\"recordsFiltered\": 4620,");
            sb.Append("\"iTotalRecords\": 4620,");
            sb.Append("\"iTotalDisplayRecords\": 4620,");
            sb.Append("\"aaData\": [");
            foreach (var result in pagedResults)
            {
                if (hasMoreRecords)
                {
                    sb.Append(",");
                }

                sb.Append("[");
                sb.Append("\"" + result.regno + "\",");
                sb.Append("\"" + result.Name + "\",");
                sb.Append("\"" + result.surname + "\",");
                sb.Append("\"" + result.designation + "\",");
                sb.Append("\"" + result.email + "\",");
                sb.Append("\"" + result.passwords + "\",");
                sb.Append("\"" + result.active + "\",");
                sb.Append("\"" + result.edit + "\"");
                sb.Append("]");
                hasMoreRecords = true;
            }
            sb.Append("]}");

            return sb.ToString();
        }

        private static IEnumerable<TableUser> GetRecordsFromDatabaseWithFilterUsers(string displaystart, string displayLength)
        {
            // At this point you can call to your database to get the data
            // but I will just populate a sample collection in code
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DateTime now = DateTime.Now;
            // var Page = new comapnyDatatableLoading();
            // IList<CustomUser> studentList = new List<CustomUser>();
            IList<TableUser> userList = new List<TableUser>();
            int length = Convert.ToInt32(displaystart) + Convert.ToInt32(displayLength);

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "exec getUserData @startRowIndex,@maximumRows";
            DLdb.SQLST.Parameters.AddWithValue("startRowIndex", displaystart);
            DLdb.SQLST.Parameters.AddWithValue("maximumRows", length);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string designations = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from PlumberDesignations where PlumberID=@PlumberID";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        designations += theSqlDataReader2["Designation"].ToString();
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    userList.Add(new TableUser() { UserId = theSqlDataReader["UserId"].ToString(), designation= designations, Name = theSqlDataReader["fname"].ToString(), email = theSqlDataReader["email"].ToString(), surname = theSqlDataReader["lname"].ToString(), passwords = DLdb.Decrypt(theSqlDataReader["password"].ToString()), regno = theSqlDataReader["regno"].ToString(), active = theSqlDataReader["isactive"].ToString(), edit= "<a href='EditOrDeleteUser.aspx?UserID=" + theSqlDataReader["UserID"].ToString() + "'><div class='btn btn-sm btn-primary'><i class='fa fa-pencil'></i></div></a>" });
                    //

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            return userList;

        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string GetTableData()
        {
            var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
            var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
            var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);
            var sortOrder = HttpContext.Current.Request.Params["sSortDir_0"].ToString(CultureInfo.CurrentCulture);
            var roleId = HttpContext.Current.Request.Params["roleId"].ToString(CultureInfo.CurrentCulture);

            var records = GetRecordsFromDatabaseWithFilter(displayStart.ToString(), displayLength.ToString()).ToList();
            if (records == null)
            {
                return string.Empty;
            }

            var orderedResults = sortOrder == "asc"
                            ? records.OrderBy(o => o.companyID)
                            : records.OrderByDescending(o => o.companyID);
            //var itemsToSkip = displayStart == 0
            //                  ? 0
            //                  : displayStart + 1;
            var itemsToSkip = 0;
            var pagedResults = orderedResults.Skip(itemsToSkip).Take(displayLength).ToList();
            var hasMoreRecords = false;

          
            var sb = new StringBuilder();
            sb.Append(@"{" + "\"sEcho\": " + echo.ToString() + ",");
            sb.Append("\"recordsTotal\":4686,");
            sb.Append("\"recordsFiltered\": 4686,");
            sb.Append("\"iTotalRecords\": 4686,");
            sb.Append("\"iTotalDisplayRecords\": 4686,");
            sb.Append("\"aaData\": [");
            foreach (var result in pagedResults)
            {
                if (hasMoreRecords)
                {
                    sb.Append(",");
                }

                sb.Append("[");
                sb.Append("\"" + result.companyID + "\",");
                sb.Append("\"" + result.companyName + "\",");
                sb.Append("\"" + result.licensedCount + "\",");
                sb.Append("\"" + result.nonLicensedCount + "\",");
                sb.Append("\"" + result.Edit + result.delete + "\"");
               // sb.Append("\"<img class='image-details' src='images/details-icon.png' runat='server' height='16' width='16' alt='View Details'/>\"");
                sb.Append("]");
                hasMoreRecords = true;
            }
            sb.Append("]}");
           
            return sb.ToString();
        }

        private static IEnumerable<CustomUser> GetRecordsFromDatabaseWithFilter(string displaystart, string displayLength)
        {
            // At this point you can call to your database to get the data
            // but I will just populate a sample collection in code
            Global DLdb = new Global();
            DLdb.DB_Connect();
            
            DateTime now = DateTime.Now;
           // var Page = new comapnyDatatableLoading();
           // IList<CustomUser> studentList = new List<CustomUser>();
            IList<CustomUser> compList = new List<CustomUser>();
            int length = Convert.ToInt32(displaystart) + Convert.ToInt32(displayLength);
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "exec getCompanyData @startRowIndex,@maximumRows";
            DLdb.SQLST.Parameters.AddWithValue("startRowIndex", displaystart);
            DLdb.SQLST.Parameters.AddWithValue("maximumRows", length);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    int LicensedCount = 0;
                    int NonLicensedCOunt = 0;

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from users where company=@company";
                    DLdb.SQLST2.Parameters.AddWithValue("company", theSqlDataReader["companyid"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "select * from PlumberDesignations where PlumberID=@PlumberID";
                            DLdb.SQLST3.Parameters.AddWithValue("PlumberID", theSqlDataReader2["UserID"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {
                                    if (theSqlDataReader3["Designation"].ToString() == "Licensed Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Director Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Master Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Qualified Plumber")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Learner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Technical Operator Practitioner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Technical Assistant Practitioner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                }
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    
                    compList.Add(new CustomUser() { companyID= theSqlDataReader["CompanyID"].ToString(), companyName= theSqlDataReader["CompanyName"].ToString(), licensedCount= LicensedCount.ToString(), nonLicensedCount= NonLicensedCOunt.ToString(), Edit= "<a href='companiesEdit?id=" + DLdb.Encrypt(theSqlDataReader["CompanyID"].ToString()) + "'><div class='btn btn-sm btn-primary' title='Edit'><i class='fa fa-pencil'></i></div></a>", delete= "<div class='btn  btn-sm btn-danger' onclick='deleteconf('DeleteCompanies.aspx?op=del&id=" + theSqlDataReader["CompanyID"].ToString() + "')'><i class='fa fa-trash'></i></div>" });
                    
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            return compList;
            
        }
        

        [WebMethod]
        public void getCOCStatementsPlumber(string uid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string COCStatement = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where isactive = '1' and userid = @userid";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string customerName = "";
                    string customerAddress = "";
                    string customerContact = "";
                    DateTime rDate;
                    DateTime cDate = Convert.ToDateTime(theSqlDataReader["DatePurchased"].ToString());
                    if (theSqlDataReader["DateRefix"].ToString() != "" && theSqlDataReader["DateRefix"] != DBNull.Value)
                    {
                        rDate = Convert.ToDateTime(theSqlDataReader["DateRefix"].ToString());
                    }
                    else
                    {
                        rDate = Convert.ToDateTime("1900-01-01");
                    }

                    //string createPdfFromOldSystem = "";
                    //if (theSqlDataReader["COCFilename"] == DBNull.Value || theSqlDataReader["COCFilename"].ToString() == "")
                    //{
                    //    if (theSqlDataReader["Type"].ToString() == "Electronic")
                    //    {
                    //        if (theSqlDataReader["Status"].ToString() == "Logged" || theSqlDataReader["Status"].ToString() == "Completed")
                    //        {
                    //            //createPdfFromOldSystem = "<label class=\"btn btn-success btn-sm\"><i class=\"fa fa-plus\"></i></label>";
                    //            createPdfFromOldSystem = "<a href=\"zCreateOlderPDF.aspx?cocid=" + theSqlDataReader["COCStatementID"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"Create COC\"><i class=\"fa fa-plus\"></i></div></a>";
                    //        }
                    //        else
                    //        {
                    //            createPdfFromOldSystem = "";
                    //        }
                    //    }
                    //}

                    string cocType = "";
                    if (theSqlDataReader["type"].ToString() == "Electronic")
                    {
                        cocType = "<span class=\"label label-warning\">E</span>";
                    }
                    else if (theSqlDataReader["type"].ToString() == "Paper")
                    {
                        cocType = "<span class=\"label label-default\">P</span>";
                    }

                    //REQUIRED: LOAD CUSTOMER NAME AND ADDRESS
                    // GET CUSTOMER NAME AND ADDRESS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Customers where CustomerID=@CustomerID";
                    DLdb.SQLST2.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            customerName = theSqlDataReader2["CustomerName"].ToString() + " " + theSqlDataReader2["CustomerSurname"].ToString();
                            customerAddress = theSqlDataReader2["AddressStreet"].ToString() + " " + theSqlDataReader2["AddressSuburb"].ToString() + " " + theSqlDataReader2["AddressCity"].ToString();
                            customerContact = theSqlDataReader2["CustomerCellNo"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    string panelColor = "info";
                    string status = theSqlDataReader["Status"].ToString();
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM COCInspectors where COCStatementID=@COCStatementID";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DateTime comDate = DateTime.Now;
                            if (theSqlDataReader2["isComplete"].ToString() == "True")
                            {
                                if (theSqlDataReader2["CompletedOn"] != DBNull.Value)
                                {
                                    comDate = Convert.ToDateTime(theSqlDataReader2["CompletedOn"].ToString());
                                }
                                status = "Complete";
                                panelColor = "success";
                            }
                            if (theSqlDataReader["isRefix"].ToString() == "True")
                            {
                                if (theSqlDataReader2["CompletedOn"] != DBNull.Value)
                                {
                                    comDate = Convert.ToDateTime(theSqlDataReader2["CompletedOn"].ToString());
                                }
                                status = "Refix Required";
                                panelColor = "danger";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    // GET THE REVIEW STATUS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from COCReviews where COCStatementID = @COCStatementID and isclosed = '0'";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            if (theSqlDataReader2["isFixed"].ToString() == "True")
                            {
                                status = "Refix Complete";
                                panelColor = "warning";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string COCPDF = "";
                    string COCNumber = "";
                    if (theSqlDataReader["COCFilename"] != DBNull.Value)
                    {
                        COCPDF = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\" download><div class=\"btn btn-sm btn-success\" title=\"View COC\"><i class=\"fa fa-download\"></i></div></a>";
                        COCNumber = "<a style='float:right;' href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\">" + theSqlDataReader["COCStatementID"].ToString() + "</a>";
                    }
                    else
                    {
                        COCNumber = "<a href=\"#\" style='float:right;'>" + theSqlDataReader["COCStatementID"].ToString() + "</a>";
                    }

                    COCStatement += "<a href='editCOCStatement.html?cocid=" + theSqlDataReader["COCStatementID"].ToString() + "'><div class=\"col-md-6 col-sm-6 col-xs-6\">" +
                                    "   <div class=\"panel panel-" + panelColor + " panel-bordered\">" +
                                    "       <div class=\"panel-heading\">" +
                                    "           <h6 class=\"panel-title\">" + status + "<span style='text-align:right;float:right;'>" + COCNumber + "&nbsp;</span></h6>" +
                                    "       </div>" +
                                    "       <div class=\"panel-body\" style='height:100px;padding-left:10px;'>" +
                                    "<i class=\"fa fa-user\"></i>&nbsp;&nbsp;" +
                                                customerName + "<br/>" + customerAddress +
                                    "          <div class=\"btn-group\" style='float:right;'>" + COCPDF + "<div class=\"btn btn-sm btn-primary\" onclick=\"document.location.href='editCOCStatement.html?cocid=" + theSqlDataReader["COCStatementID"].ToString() + "'\"><i class=\"fa fa-pencil\"></i></div></div>" +
                                    //<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('Archive.aspx?&cocid=" + theSqlDataReader["COCStatementID"].ToString() + "')\"><i class=\"fa fa-archive\"></i></div>
                                    "       </div>" +
                                    "   </div>" +
                                    "</div></a>";


                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Context.Response.Write(COCStatement);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getCOCStatementsInspector(string uid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string COCStatement = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements c inner join COCInspectors i on c.COCStatementID = i.COCStatementID where c.isAudit = '1' and c.isactive = '1' and i.isComplete = '0' and i.isactive = '1' and i.userid = @userid";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string customerName = "";
                    string customerAddress = "";
                    string customerContact = "";
                    string username = "";
                    string usercontact = "";

                    DateTime cDate = DateTime.Now;
                    if (theSqlDataReader["DateLogged"] != DBNull.Value && theSqlDataReader["DateRefix"].ToString() != "")
                    {
                        cDate = Convert.ToDateTime(theSqlDataReader["DateLogged"].ToString());
                    }
                    DateTime refixDate = Convert.ToDateTime("01/01/1900");
                    string dateRefixed = "";
                    if (theSqlDataReader["DateRefix"] != DBNull.Value && theSqlDataReader["DateRefix"].ToString() != "")
                    {
                        refixDate = Convert.ToDateTime(theSqlDataReader["DateRefix"]);
                        dateRefixed = refixDate.ToString("dd/MM/yyyy");
                    }
                    //REQUIRED: LOAD CUSTOMER NAME AND ADDRESS
                    // GET CUSTOMER NAME AND ADDRESS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Customers where CustomerID=@CustomerID";
                    DLdb.SQLST2.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            customerName = theSqlDataReader2["CustomerName"].ToString() + " " + theSqlDataReader2["CustomerSurname"].ToString();
                            customerAddress = theSqlDataReader2["AddressStreet"].ToString() + " " + theSqlDataReader2["AddressSuburb"].ToString() + " " + theSqlDataReader2["AddressCity"].ToString();
                            customerContact = theSqlDataReader2["CustomerCellNo"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    // GET CUSTOMER NAME AND ADDRESS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            username = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                            usercontact = theSqlDataReader2["contact"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    // GET INSTALLATION TYPES
                    string InstallTypes = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM COCInstallations C inner join installationTypes I on C.TypeID=I.InstallationTypeID where C.isActive = '1' and C.COCStatementID= @COCStatementID";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            InstallTypes += theSqlDataReader2["InstallationType"].ToString() + "<br />";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    string panelColor = "info";
                    DateTime comDate = DateTime.Now;
                    string status = "Auditing";
                    if (theSqlDataReader["isComplete"].ToString() == "True")
                    {
                        if (theSqlDataReader["CompletedOn"] != DBNull.Value)
                        {
                            comDate = Convert.ToDateTime(theSqlDataReader["CompletedOn"].ToString());
                        }
                        status = "Complete";
                        panelColor = "success";
                    }
                    if (theSqlDataReader["isRefix"].ToString() == "True")
                    {
                        if (theSqlDataReader["CompletedOn"] != DBNull.Value)
                        {
                            comDate = Convert.ToDateTime(theSqlDataReader["CompletedOn"].ToString());
                        }
                        //status = "<div class=\"alert alert-danger\">Refix Required</div>";
                    }

                    string isCOCRefix = "false";
                    if (theSqlDataReader["isRefix"].ToString() == "True")
                    {
                        isCOCRefix = "true";
                    }

                    // GET THE REVIEW STATUS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from COCReviews where COCStatementID = @COCStatementID and isclosed = '0'";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            if (theSqlDataReader2["isFixed"].ToString() == "True" && isCOCRefix == "true")
                            {
                                panelColor = "warning";
                                status = "Refix Complete";
                            }
                            else if (theSqlDataReader2["isFixed"].ToString() == "False" && isCOCRefix == "true")
                            {
                                status = "Refix Required";
                                panelColor = "danger";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();


                    string stat = "";
                    if (theSqlDataReader["isInspectorSubmitted"].ToString() == "True")
                    {
                        stat = "<div class=\"alert alert-warning\">Submitted</div>";
                    }
                    else
                    {
                        stat = "<div class=\"alert alert-warning\">Not Submitted</div>";
                    }

                    string COCPDF = "";
                    string COCNumber = "";
                    if (theSqlDataReader["COCFilename"] != DBNull.Value)
                    {
                        COCPDF = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"View COC\"><i class=\"fa fa-download\"></i></div></a>";
                        COCNumber = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\">" + theSqlDataReader["COCStatementID"].ToString() + "</a>";
                    }
                    else
                    {
                        COCNumber = theSqlDataReader["COCStatementID"].ToString();
                    }

                    COCStatement += "<div class=\"col-md-6\">" +
                                    "   <div class=\"panel panel-" + panelColor + " \" style='margin-top:10px;'>" +
                                    "       <div class=\"panel-heading\">" +
                                    "           <h6 class=\"panel-title\">" + status + "<span style='text-align:right;float:right;'>" + COCNumber + "&nbsp;</span></h6>" +
                                    "       </div>" +
                                    "       <div class=\"panel-body\" style='height:100px;padding:10px;'>" +
                                    "<i class=\"fa fa-user\"></i>&nbsp;&nbsp;" +
                                                customerName + "<br/><a href=\"https://www.google.co.za/maps/place/" + customerAddress + "\" target=\"_blank\">" + customerAddress + "</a>" +
                                    "          <div class=\"btn-group\" style='float:right;'>" + COCPDF + "<div class=\"btn btn-sm btn-primary\" onclick=\"document.location.href='editCOCStatementInspec.html?cocid=" + theSqlDataReader["COCStatementID"].ToString() + "'\"><i class=\"fa fa-pencil\"></i>E</div></div>" +
                                    //<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('Archive.aspx?&cocid=" + theSqlDataReader["COCStatementID"].ToString() + "')\"><i class=\"fa fa-archive\"></i></div>
                                    "<br/>" +
                                    InstallTypes.Substring(0, 50) +
                                    "<br/>" +
                                    "<label class='label label-danger'>" + dateRefixed + "</label>" +
                                    "       </div>" +
                                    "   </div>" +
                                    "</div>";

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();



            Context.Response.Write(COCStatement);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getCOCStatementsPlumberArchived(string uid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string COCStatement = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where isactive = '0' and userid = @userid";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string customerName = "";
                    string customerAddress = "";
                    string customerContact = "";
                    DateTime rDate;
                    DateTime cDate = Convert.ToDateTime(theSqlDataReader["DatePurchased"].ToString());
                    if (theSqlDataReader["DateRefix"].ToString() != "" && theSqlDataReader["DateRefix"] != DBNull.Value)
                    {
                        rDate = Convert.ToDateTime(theSqlDataReader["DateRefix"].ToString());
                    }
                    else
                    {
                        rDate = Convert.ToDateTime("1900-01-01");
                    }


                    string createPdfFromOldSystem = "";
                    if (theSqlDataReader["COCFilename"] == DBNull.Value || theSqlDataReader["COCFilename"].ToString() == "")
                    {
                        if (theSqlDataReader["Type"].ToString() == "Electronic")
                        {
                            if (theSqlDataReader["Status"].ToString() == "Logged" || theSqlDataReader["Status"].ToString() == "Completed")
                            {
                                //createPdfFromOldSystem = "<label class=\"btn btn-success btn-sm\"><i class=\"fa fa-plus\"></i></label>";
                                createPdfFromOldSystem = "<a href=\"zCreateOlderPDF.aspx?cocid=" + theSqlDataReader["COCStatementID"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"Create COC\"><i class=\"fa fa-plus\"></i></div></a>";
                            }
                            else
                            {
                                createPdfFromOldSystem = "";
                            }
                        }
                    }

                    //REQUIRED: LOAD CUSTOMER NAME AND ADDRESS
                    // GET CUSTOMER NAME AND ADDRESS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Customers where CustomerID=@CustomerID";
                    DLdb.SQLST2.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            customerName = theSqlDataReader2["CustomerName"].ToString() + " " + theSqlDataReader2["CustomerSurname"].ToString();
                            customerAddress = theSqlDataReader2["AddressStreet"].ToString() + " " + theSqlDataReader2["AddressSuburb"].ToString() + " " + theSqlDataReader2["AddressCity"].ToString();
                            customerContact = theSqlDataReader2["CustomerCellNo"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    string panelColor = "info";
                    string status = theSqlDataReader["Status"].ToString();
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM COCInspectors where COCStatementID=@COCStatementID";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DateTime comDate = DateTime.Now;
                            if (theSqlDataReader2["isComplete"].ToString() == "True")
                            {
                                if (theSqlDataReader2["CompletedOn"] != DBNull.Value)
                                {
                                    comDate = Convert.ToDateTime(theSqlDataReader2["CompletedOn"].ToString());
                                }
                                status = "Complete";
                                panelColor = "success";
                            }
                            if (theSqlDataReader["isRefix"].ToString() == "True")
                            {
                                if (theSqlDataReader2["CompletedOn"] != DBNull.Value)
                                {
                                    comDate = Convert.ToDateTime(theSqlDataReader2["CompletedOn"].ToString());
                                }
                                status = "Refix Required";
                                panelColor = "danger";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    // GET THE REVIEW STATUS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from COCReviews where COCStatementID = @COCStatementID and isclosed = '0'";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            if (theSqlDataReader2["isFixed"].ToString() == "True")
                            {
                                status = "Refix Complete";
                                panelColor = "warning";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string COCPDF = "";
                    string COCNumber = "";
                    if (theSqlDataReader["COCFilename"] != DBNull.Value)
                    {
                        COCPDF = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\" download><div class=\"btn btn-sm btn-success\" title=\"View COC\"><i class=\"fa fa-download\"></i></div></a>";
                        COCNumber = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\" download style=\"color:red;\">" + theSqlDataReader["COCStatementID"].ToString() + "</a>";

                    }
                    else
                    {
                        COCNumber = theSqlDataReader["COCStatementID"].ToString();
                    }


                    COCStatement +=
                                            "" +
                                             "<div class=\"col-md-6\">" +
                                             "   <div class=\"panel panel-" + panelColor + " panel-bordered\">" +
                                             "       <div class=\"panel-heading\">" +
                                             "           <h6 class=\"panel-title\">" + status + "<span style='text-align:right;float:right;'>" + COCNumber + "</span></h6>" +
                                             "       </div>" +
                                             "       <div class=\"panel-body\">" +
                                                         customerAddress +
                                             "          <div class=\"btn-group\">" + COCPDF + createPdfFromOldSystem + "<div class=\"btn btn-sm btn-primary\" onclick=\"document.location.href='editCOCStatement.html?cocid=" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "'\"><i class=\"fa fa-pencil\"></i></div><div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('Archive.aspx?&cocid=" + theSqlDataReader["COCStatementID"].ToString() + "')\"><i class=\"fa fa-archive\"></i></div></div>" +
                                             "       </div>" +
                                             "   </div>" +
                                             "</div>";


                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Context.Response.Write(COCStatement);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getPurchaseCOCDetails(string uid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";
            string errorMsg = "";
            string suspended = "False";
            DateTime expireDate;
            DateTime now = DateTime.Now;
            string signatureImg = "False";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["RegistrationEnd"].ToString() != "" && theSqlDataReader["RegistrationEnd"] != DBNull.Value)
                    {
                        expireDate = Convert.ToDateTime(theSqlDataReader["RegistrationEnd"].ToString());
                    }
                    else
                    {
                        expireDate = Convert.ToDateTime("1900-01-01");
                    }

                    suspended = theSqlDataReader["isSuspended"].ToString();
                    if (expireDate < now)
                    {
                        suspended = "Expired";
                    }
                    else
                    {
                        suspended = "NotExpired";
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            var Page = new details();

            if (suspended == "True" || suspended == "Expired")
            {
                errorMsg = "You can't purchase a COC because you're either suspended or your registration has expired";
            }
            else
            {
                // GET AVALIABLE
                int NoBought = 0;
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select count(*) as Total from [dbo].[COCStatements] where UserID = @UserID and DateLogged is null";
                DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        NoBought = Convert.ToInt32(theSqlDataReader["Total"]);
                        Page.nonLogged = NoBought.ToString();
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID = @UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["Signature"] == DBNull.Value || theSqlDataReader["Signature"].ToString() == "")
                        {
                            signatureImg = "False";
                        }
                        else
                        {
                            signatureImg = "True";
                        }
                        Page.signature = signatureImg;
                        Page.permitted = theSqlDataReader["NoCOCpurchases"].ToString();
                        Page.permittedPurchase = (Convert.ToInt32(theSqlDataReader["NoCOCpurchases"]) - NoBought).ToString();
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }

            Page.error = errorMsg;

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }


        [WebMethod]
        public void getCompanyDataTableLoading()
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";
            DateTime now = DateTime.Now;

            var Page = new comapnyDatatableLoading();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Companies where isActive='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    int LicensedCount = 0;
                    int NonLicensedCOunt = 0;

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from users where company=@company";
                    DLdb.SQLST2.Parameters.AddWithValue("company", theSqlDataReader["companyid"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "select * from PlumberDesignations where PlumberID=@PlumberID";
                            DLdb.SQLST3.Parameters.AddWithValue("PlumberID", theSqlDataReader2["UserID"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {
                                    if (theSqlDataReader3["Designation"].ToString() == "Licensed Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Director Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Master Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Qualified Plumber")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Learner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Technical Operator Practitioner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Technical Assistant Practitioner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                }
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    Page.companyID = theSqlDataReader["CompanyID"].ToString();
                    Page.companyName = theSqlDataReader["CompanyName"].ToString();
                    Page.licensedCount = LicensedCount.ToString();
                    Page.nonLicensedCount = NonLicensedCOunt.ToString();
                    Page.companyID = "<a href='companiesEdit?id=" + DLdb.Encrypt(theSqlDataReader["CompanyID"].ToString()) + "'><div class=\"btn btn-sm btn-primary\" title=\"Edit\"><i class=\"fa fa-pencil\"></i></div></a>";
                    Page.companyID = "<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeleteCompanies.aspx?op=del&id=" + theSqlDataReader["CompanyID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div>";

                    //displayusers.InnerHtml += "<tr>" +
                    //                                   "<td>" + theSqlDataReader["CompanyID"].ToString() + "</td>" +
                    //                                   "<td>" + theSqlDataReader["CompanyName"].ToString() + "</td>" +
                    //                                   "<td>" + LicensedCount + "</td>" +
                    //                                   "<td>" + NonLicensedCOunt + "</td>" +
                    //                                   "<td><a href='companiesEdit?id=" + DLdb.Encrypt(theSqlDataReader["CompanyID"].ToString()) + "'><div class=\"btn btn-sm btn-primary\" title=\"Edit\"><i class=\"fa fa-pencil\"></i></div></a>" +
                    //                                   "<div class=\"btn " + delBtnClass + " btn-sm btn-danger\" onclick=\"deleteconf('DeleteCompanies.aspx?op=del&id=" + theSqlDataReader["CompanyID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                    //                               "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void updateCOCPurchaseAmount(string uid, string type, string delMethod, string AmountToPurchase, string permittedPurchase)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";
            decimal COCElectronic = 0;
            decimal COCPaper = 0;
            decimal CourierDelivery = 0;
            decimal RegisteredPostDelivery = 0;
            decimal Collection = 0;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Rates";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["ID"].ToString() == "40")
                    {
                        COCElectronic = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                    else if (theSqlDataReader["ID"].ToString() == "36")
                    {
                        COCPaper = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                    else if (theSqlDataReader["ID"].ToString() == "24")
                    {
                        CourierDelivery = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                    else if (theSqlDataReader["ID"].ToString() == "25")
                    {
                        RegisteredPostDelivery = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                    else if (theSqlDataReader["ID"].ToString() == "26")
                    {
                        Collection = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }

                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            int NoCocsPurchase = Convert.ToInt32(AmountToPurchase.ToString());
            int COCsAbleToPurchases = Convert.ToInt32(permittedPurchase.ToString());

            var Page = new PurchaseCOCAmountChanged();
            if (NoCocsPurchase > COCsAbleToPurchases)
            {
                Page.error = "Can't purchase more than 'Number of COC's I am able to Purchase";
            }

            decimal totalCost = 0;
            decimal vats = 0;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from settings where ID='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                vats = Convert.ToDecimal(theSqlDataReader["VatPercentage"].ToString());
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();
            decimal vat = Convert.ToDecimal(vats);
            Page.vatValue = vat.ToString();

            if (type == "Electronic")
            {
                totalCost = NoCocsPurchase * COCElectronic;
                Page.costCoc = totalCost.ToString("0.##");
                decimal vatElec = totalCost * vat;
                Page.vatValue = vatElec.ToString("0.##");
                decimal totalAmount = totalCost + vatElec;
                Page.total = totalAmount.ToString("0.##");

            }
            else
            {
                totalCost = NoCocsPurchase * COCPaper;
                Page.costCoc = totalCost.ToString("0.##");
                decimal vatPaper = totalCost * vat;
                Page.vatValue = vatPaper.ToString("0.##");
                decimal totalAmount = totalCost + vatPaper;

                if (delMethod == "Courier")
                {
                    Page.deliveryCost = CourierDelivery.ToString();
                    totalAmount = totalAmount + CourierDelivery;
                }
                else if (delMethod == "Registered Post")
                {
                    Page.deliveryCost = RegisteredPostDelivery.ToString();
                    totalAmount = totalAmount + RegisteredPostDelivery;
                }
                else if (delMethod == "Collect")
                {
                    Page.deliveryCost = Collection.ToString();
                    totalAmount = totalAmount + Collection;
                }
                Page.total = totalAmount.ToString("0.##");
            }

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void PurchaseCOCCreateOrder(string uid, string delivery, string costCOC, string total, string vat, string type, string delMethod, string AmountToPurchase, string permittedPurchase)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string OrderID = "";
            string Type = "Paper";
            string Method = "Collect at PIRB Offices";
            if (type == "Electronic")
            {
                Type = "Electronic";
                Method = "";
            }

            if (delMethod == "Registered Post")
            {
                Method = "Registered Post";
            }
            if (delMethod == "Courier")
            {
                Method = "Courier";
            }

            decimal StartRange = 0;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select top 1 (COCStatementID + 1) as nCOCStatementID from COCStatements order by COCStatementID desc";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    StartRange = Convert.ToDecimal(theSqlDataReader["nCOCStatementID"].ToString());
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            decimal eRange = StartRange + Convert.ToDecimal(AmountToPurchase);

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO Orders (StartRange,EndRange,UserID,Description,TotalNoItems,SubTotal,Vat,Delivery,Total,COCType,Method,isPaid) values (@StartRange,@EndRange,@UserID,@Description,@TotalNoItems,@SubTotal,@Vat,@Delivery,@Total,@COCType,@Method,@isPaid); Select Scope_Identity() as OrderID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Description", "COC Purchase");
            DLdb.SQLST.Parameters.AddWithValue("TotalNoItems", AmountToPurchase.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubTotal", costCOC.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Vat", vat.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Delivery", delivery.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Total", total.ToString());
            DLdb.SQLST.Parameters.AddWithValue("COCType", Type);
            DLdb.SQLST.Parameters.AddWithValue("Method", Method);
            DLdb.SQLST.Parameters.AddWithValue("StartRange", StartRange.ToString());
            DLdb.SQLST.Parameters.AddWithValue("EndRange", eRange.ToString());
            DLdb.SQLST.Parameters.AddWithValue("isPaid", "0");
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                OrderID = theSqlDataReader["OrderID"].ToString();
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
            DLdb.RS.Close();

            string UserName = "";
            string UserEmail = "";
            string NumberTo = "";

            DLdb.RS3.Open();
            DLdb.SQLST3.CommandText = "select * from users where userid = @UserID";
            DLdb.SQLST3.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST3.CommandType = CommandType.Text;
            DLdb.SQLST3.Connection = DLdb.RS3;
            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            if (theSqlDataReader3.HasRows)
            {
                theSqlDataReader3.Read();
                UserName = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
                UserEmail = theSqlDataReader3["email"].ToString();
                if (theSqlDataReader3["contact"].ToString() != "" && theSqlDataReader3["contact"] != DBNull.Value)
                {
                    NumberTo = theSqlDataReader3["contact"].ToString();
                }
            }

            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            DLdb.SQLST3.Parameters.RemoveAt(0);
            DLdb.RS3.Close();

            string OTPCode = DLdb.CreateNumber(5);
            if (Type == "Paper")
            {
                DLdb.sendSMS(uid.ToString(), NumberTo.ToString(), "Inspect-It: You would like to purchase " + AmountToPurchase.ToString() + " " + Type + " C.O.C for " + Method + ". Amount: R" + total.ToString() + ". OTP Code: " + OTPCode);
            }
            else
            {
                DLdb.sendSMS(uid.ToString(), NumberTo.ToString(), "Inspect-It: You would like to purchase " + AmountToPurchase.ToString() + " " + Type + " C.O.C. Amount: R" + total.ToString() + ". OTP Code: " + OTPCode);
            }

            string json = "";
            var Page = new order();
            Page.otp = OTPCode;
            Page.oid = OrderID;

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getPlumberDetails(string uid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";
            string fullname = "";
            string contact = "";
            string regno = "";
            string email = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    fullname = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                    email = theSqlDataReader["email"].ToString();
                    contact = theSqlDataReader["contact"].ToString();
                    regno = theSqlDataReader["regno"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            var Page = new PlumberDetails();

            Page.plumberFullName = fullname;
            Page.plumberEmail = email;
            Page.plumberContact = contact;
            Page.plumberRegNo = regno;

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getCOCDetails(string uid, string cocid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";
            string customerid = "";
            string auditorid = "";
            string pirbid = "";

            var Page = new CocDetails();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.cocNumber = theSqlDataReader["cocNumber"].ToString();
                    customerid = theSqlDataReader["customerid"].ToString();
                    auditorid = theSqlDataReader["auditorid"].ToString();
                    Page.auditDate = theSqlDataReader["DateAudited"].ToString();
                    Page.refixDate = theSqlDataReader["DateRefix"].ToString();
                    Page.AorB = theSqlDataReader["AorB"].ToString();
                    Page.nonComplianceDetails = theSqlDataReader["NonComplianceDetails"].ToString();
                    Page.isRefix = theSqlDataReader["isRefix"].ToString();
                    Page.isPlumberSubmitted = theSqlDataReader["isPlumberSubmitted"].ToString();
                    Page.isPlumberLogged = theSqlDataReader["isLogged"].ToString();
                    Page.isPaper = theSqlDataReader["Type"].ToString();
                    Page.Status = theSqlDataReader["Status"].ToString();
                    if (theSqlDataReader["PaperBasedCOC"].ToString() != "" || theSqlDataReader["PaperBasedCOC"] != DBNull.Value)
                    {
                        Page.paperCOC = "<span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImagePDF('" + cocid.ToString() + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"https://197.242.82.242/inspectit/pdf/" + theSqlDataReader["PaperBasedCOC"].ToString() + "\"><img src=\"https://197.242.82.242/inspectit/pdf/" + theSqlDataReader["PaperBasedCOC"].ToString() + "\" style=\"height:200px;\"/></a>";
                    }
                    else
                    {
                        Page.paperCOC = "";
                    }

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatementDetails where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.completeDate = theSqlDataReader["CompletedDate"].ToString();
                    Page.descriptionOfWork = theSqlDataReader["DescriptionofWork"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.isSuspended = theSqlDataReader["isSuspended"].ToString();
                    Page.plumberRegisNumber = theSqlDataReader["regno"].ToString();
                    Page.plumberName = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                    pirbid = theSqlDataReader["pirbid"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from auditor where AuditorID = @AuditorID";
            DLdb.SQLST.Parameters.AddWithValue("AuditorID", auditorid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.inspecEmail = theSqlDataReader["email"].ToString();
                    Page.inspecContact = theSqlDataReader["phonemobile"].ToString();
                    Page.inspecName = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                    if (theSqlDataReader["photofile"] != DBNull.Value && theSqlDataReader["photofile"].ToString() != "")
                    {
                        //photo = "<img src=\"AuditorImgs/" + theSqlDataReader["photofile"].ToString() + "\" />";
                        Page.inspecImage = "http://localhost:58752/AuditorImgs/" + theSqlDataReader["PhotoFile"].ToString();
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Customers where CustomerID = @CustomerID";
            DLdb.SQLST.Parameters.AddWithValue("CustomerID", customerid);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.customerName = theSqlDataReader["CustomerName"].ToString();
                    Page.customerSurname = theSqlDataReader["CustomerSurname"].ToString();
                    Page.telNumber = theSqlDataReader["CustomerCellNo"].ToString();
                    Page.altTelNumber = theSqlDataReader["CustomerCellNoAlt"].ToString();
                    Page.customerEmail = theSqlDataReader["CustomerEmail"].ToString();
                    Page.street = theSqlDataReader["AddressStreet"].ToString();
                    Page.suburb = theSqlDataReader["AddressSuburb"].ToString();
                    Page.city = theSqlDataReader["AddressCity"].ToString();
                    Page.province = theSqlDataReader["Province"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Boolean showLP = false;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM PlumberDesignations where isActive = '1' and PlumberID=@PlumberID";
            DLdb.SQLST.Parameters.AddWithValue("PlumberID", pirbid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["Designation"].ToString() == "Licensed Plumber")
                    {
                        showLP = true;
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            Boolean showSP = false;
            Boolean showHPP = false;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM PlumberSpecialisations where isActive = '1' and PlumberID=@PlumberID";
            DLdb.SQLST.Parameters.AddWithValue("PlumberID", pirbid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["Specialisation"].ToString() == "Solar")
                    {
                        showSP = true;
                    }
                    if (theSqlDataReader["Specialisation"].ToString() == "Heat Pump")
                    {
                        showHPP = true;
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            if (showLP == true && showSP == false && showHPP == false)
            {
                DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where (isActive = '1' and InstallationTypeID != '8') and (isActive = '1' and InstallationTypeID != '4')";
            }
            else if (showLP == true && showSP == true && showHPP == false)
            {
                DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive = '1' and InstallationTypeID != '8'";
            }
            else if (showLP == true && showSP == true && showHPP == true)
            {
                DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive = '1'";
            }
            else if (showLP == true && showSP == false && showHPP == true)
            {
                DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive = '1' and InstallationTypeID != '4'";
            }
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string checkeds = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM COCInstallations where COCStatementID = @COCStatementID and isActive = '1' and TypeID=@TypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("TypeID", theSqlDataReader["InstallationTypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            checkeds = "checked";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    Page.installationTypes += "<br/><input name=\"TypeOfInstallation\" type='checkbox' " + checkeds + " value='" + theSqlDataReader["InstallationTypeID"].ToString() + "'/>&nbsp;" + theSqlDataReader["InstallationType"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS3.Open();
            DLdb.SQLST3.CommandText = "select * from COCInspectors where COCStatementID = @COCStatementID and isactive = '1'";
            DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST3.CommandType = CommandType.Text;
            DLdb.SQLST3.Connection = DLdb.RS3;
            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            if (theSqlDataReader3.HasRows)
            {
                while (theSqlDataReader3.Read())
                {
                    if (theSqlDataReader3["InspectionDate"] == DBNull.Value)
                    {
                        Page.hideAudit = "False";
                    }
                    else
                    {
                        Page.hideAudit = "True";
                        Page.auditDate = theSqlDataReader3["InspectionDate"].ToString();
                        Page.quality = theSqlDataReader3["Quality"].ToString();
                        if (theSqlDataReader3["Picture"] != DBNull.Value)
                        {
                            Page.inspecImg = "<img src=\"http://localhost:58752/noticeimages/" + theSqlDataReader3["Picture"].ToString() + "\" class=\"img-responsive\" style='max-height:300px;' />";
                        }
                    }
                }
            }
            else
            {
                Page.hideAudit = "False";
            }

            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            DLdb.SQLST3.Parameters.RemoveAt(0);
            DLdb.RS3.Close();

            string CurrentReview = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCReviews where COCStatementID = @COCStatementID and isActive = '1' order by createdate desc";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string StatusCol = "";
                    string btnFix = "";
                    string uploadBtn = "";

                    if (theSqlDataReader["status"].ToString() == "Failure" && theSqlDataReader["isFixed"].ToString() == "False")
                    {
                        StatusCol = "danger";
                        uploadBtn = "<div class=\"row text-left\"><b style='margin-left:20px;'>Upload your refix images:</b><br /><div class=\"col-md-6 col-sm-6 col-xs-6\" style='margin-top:0px;'><label class=\"btn btn-success\" onclick=\"uploadImg('" + theSqlDataReader["reviewid"].ToString() + "')\">Upload&nbsp;&nbsp;<i class='fa fa-camera'></i></label></div><div class=\"col-md-6 col-sm-6 col-xs-6\" style='margin-top:0px;'><label class=\"btn btn-success\" onclick=\"getPhotoReview('navigator.camera.PictureSourceType.PHOTOLIBRARY','" + theSqlDataReader["reviewid"].ToString() + "')\">Upload&nbsp;&nbsp;<i class='fa fa-image'></i></label></div></div>";
                        btnFix = "<input class=\"fixChkbx\" id=\"" + theSqlDataReader["ReviewID"].ToString() + "\" name=\"" + theSqlDataReader["ReviewID"].ToString() + "\" value=\"" + theSqlDataReader["ReviewID"].ToString() + "\" onclick=\"markasFixed('" + theSqlDataReader["ReviewID"].ToString() + "')\" type=\"checkbox\"/> Mark As Fixed";
                        //btnFix = "<label class=\"btn btn-primary\" onclick=\"markasFixed('" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "')\">This job has been Refixed</label>";
                    }
                    else if (theSqlDataReader["status"].ToString() == "Cautionary")
                    {
                        StatusCol = "warning";
                        //btnFix = "<div class=\"btn btn-primary\" onclick=\"document.location.href='DismissReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "'\">Dismiss</div>";
                    }
                    else if (theSqlDataReader["status"].ToString() == "Compliment")
                    {
                        StatusCol = "success";
                        //btnFix = "<div class=\"btn btn-primary\" onclick=\"document.location.href='DismissReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "'\">Dismiss</div>";
                    }
                    else if (theSqlDataReader["status"].ToString() == "Failure" && theSqlDataReader["isFixed"].ToString() == "True" && theSqlDataReader["isClosed"].ToString() == "False")
                    {
                        StatusCol = "danger";
                        //btnFix = "<label class=\"label label-success\">You have marked this as fixed</label>";
                        btnFix = "<input class=\"fixChkbx\" id=\"" + theSqlDataReader["ReviewID"].ToString() + "\" value=\"" + theSqlDataReader["ReviewID"].ToString() + "\" name=\"" + theSqlDataReader["ReviewID"].ToString() + "\" checked type=\"checkbox\" /> Mark As Fixed";

                    }
                    else if (theSqlDataReader["status"].ToString() == "Failure" && theSqlDataReader["isFixed"].ToString() == "True" && theSqlDataReader["isClosed"].ToString() == "True")
                    {
                        StatusCol = "danger";
                        btnFix = "<label class=\"label label-success\">Refix Complete</label>";
                    }

                    string InstallationType = "";

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from InstallationTypes where InstallationTypeID = @InstallationTypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            InstallationType = theSqlDataReader2["InstallationType"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string Media = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from FormImg where ReviewID = @ReviewID and isReference='0' and isActive='1'";
                    DLdb.SQLST2.Parameters.AddWithValue("ReviewID", theSqlDataReader["ReviewID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            string filename = theSqlDataReader2["imgsrc"].ToString(); ;
                            if (theSqlDataReader2["UserID"].ToString() == uid.ToString())
                            {
                                Media += "<div class=\"col-md-3\" id=\"show_img_" + theSqlDataReader2["ImgID"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader2["imgid"].ToString() + "','" + cocid.ToString() + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"http://localhost:58752/AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"http://localhost:58752/AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                            }
                            else
                            {
                                Media += "<div class=\"col-md-3\"><a href=\"http://localhost:58752/AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"http://localhost:58752/AuditorImgs/" + filename + "\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string referenceMedia = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from FormImg where ReviewID = @ReviewID and isReference='1' and isActive='1'";
                    DLdb.SQLST2.Parameters.AddWithValue("ReviewID", theSqlDataReader["ReviewID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            string filename = theSqlDataReader2["imgsrc"].ToString(); ;
                            if (theSqlDataReader2["UserID"].ToString() == uid.ToString())
                            {
                                referenceMedia += "<div class=\"col-md-3\"><a href=\"http://localhost:58752/AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"http://localhost:58752/AuditorImgs/" + filename + "\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                                //referenceMedia += "<div class=\"col-md-3\" id=\"show_img_" + theSqlDataReader2["ImgID"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader2["imgid"].ToString() + "','"+DLdb.Decrypt(Request.QueryString["cocid"].ToString())+"')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                            }
                            else
                            {
                                referenceMedia += "<div class=\"col-md-3\"><a href=\"http://localhost:58752/AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"http://localhost:58752/AuditorImgs/" + filename + "\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                            }

                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    CurrentReview += "<div class=\"row alert-" + StatusCol + "\" style=\"padding: 5px; \">" +
                         "       <div class=\"col-md-6 text-right\">" +
                                                            btnFix +
                                               "     </div>" +
                                               "       <div class=\"col-md-6 text-left\">" +
                                               "         <b>Instalation Type:</b> " + InstallationType + "<br />" +
                                               "         <b>Audit Status:</b> " + theSqlDataReader["status"].ToString() + "" +
                                               "     </div>" +

                                               //"     <div class=\"col-md-12 text-left\">" +
                                               //"        <b>Reference:</b> " + theSqlDataReader["name"].ToString() + "" +
                                               //"     </div>" +
                                               "     <div class=\"col-md-12 text-left\"><b>Comments:</b> " + theSqlDataReader["comment"].ToString() + "</div>" +
                                               "     <div class=\"col-md-12 text-left\"><b>Media:</b><br />" + Media + "</div>" +
                                               "     <div class=\"col-md-12 text-left\"><b>Reference:</b> " + theSqlDataReader["Reference"].ToString() + "</div>" +
                                               "     <div class=\"col-md-12 text-left\"><b>Media:</b><br />" + referenceMedia + "</div>" +
                                               uploadBtn +
                                               // "     <div class=\"col-md-12 text-right\"><div class=\"btn btn-primary\" onclick=\"document.location.href='EditReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "'\">Edit</div></div>" +
                                               " </div><hr />";

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Page.refixViews = CurrentReview;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCReviews where COCStatementID = @COCStatementID and isActive = '1' and isFixed='0'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.noMoreFixs = "False";
                }
            }
            else
            {
                Page.noMoreFixs = "True";
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string compImg = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormImg where COCID = @COCID and isReference='0' and isCompleteImg='1' and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("COCID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string filename = theSqlDataReader["imgsrc"].ToString(); ;
                    if (theSqlDataReader["UserID"].ToString() == uid.ToString())
                    {
                        compImg += "<div class=\"col-md-3 col-sm-6 col-xs-6\" id=\"show_img_" + theSqlDataReader["ImgID"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader["imgid"].ToString() + "','" + cocid.ToString() + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"http://localhost:58752/AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"http://localhost:58752/AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                    }
                    else
                    {
                        compImg += "<div class=\"col-md-3 col-sm-6 col-xs-6\"><a href=\"http://localhost:58752/AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"http://localhost:58752/AuditorImgs/" + filename + "\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Page.completedImgs = compImg;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCRefixesComments where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string newComma = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        newComma = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DateTime dateCreatedViewa = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                    Page.lastComment = theSqlDataReader["Comments"].ToString() + "<br/> <small>" + newComma + " - " + dateCreatedViewa.ToString("dd/MM/yyyy") + "</small>";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID and Role='Inspector'";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DateTime dateCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                            Page.inspecDatePosted = dateCreated.ToString("dd/MM/yyyy");
                            Page.inspecComments += "<p>" + theSqlDataReader["Comments"].ToString() + "</p>";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID and Role!='Inspector'";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DateTime dateCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                            Page.plumberDatePosted = dateCreated.ToString("dd/MM/yyyy");
                            Page.plumberComments += "<p>" + theSqlDataReader["Comments"].ToString() + "</p>";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getCOCDetailsInspec(string uid, string cocid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";
            var Page = new CocDetailsInspecSide();
            string CustomerID = "";
            string plumberUserID = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCRefixesComments where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string newComma = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        newComma = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DateTime dateCreatedViewa = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                    Page.lastComment = theSqlDataReader["Comments"].ToString() + "<br/> <small>" + newComma + " - " + dateCreatedViewa.ToString("dd/MM/yyyy") + "</small>";

                    string inspecID = "";
                    // GET USER INSPECTOR DETAILS FOR RIGHT HAND SIDE COMMENTS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID and Role='Inspector'";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            inspecID = theSqlDataReader2["UserID"].ToString();
                            DateTime dateCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                            Page.inspecDatePosted = dateCreated.ToString("dd/MM/yyyy");

                            Page.inspecName = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                            Page.inspecComments += "<p>" + theSqlDataReader["Comments"].ToString() + "</p>";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    // GET USER DETAILS FOR LEFT HAND SIDE COMMENTS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID and Role!='Inspector'";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DateTime dateCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                            Page.plumberDatePosted = dateCreated.ToString("dd/MM/yyyy");

                            Page.plumberName = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                            Page.plumberRegisNumber = theSqlDataReader2["regno"].ToString();
                            Page.plumberEmail = theSqlDataReader2["email"].ToString();
                            Page.plumberContact = theSqlDataReader2["contact"].ToString();
                            //PlumberBusContact.Text = theSqlDataReader2["contact"].ToString();
                            Page.plumberComments += "<p>" + theSqlDataReader["Comments"].ToString() + "</p>";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select count(*) as totalNumber from COCReviews where COCStatementID=@COCStatementID and isClosed='0'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.countRefixes = theSqlDataReader["totalNumber"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select count(*) as totalNumber from COCReviews where COCStatementID=@COCStatementID and isFixed='1' and isClosed='0'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.countRefixesFixed = theSqlDataReader["totalNumber"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string inspectorsID = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.isRefixs = theSqlDataReader["isRefix"].ToString();
                    inspectorsID = theSqlDataReader["AuditorID"].ToString();
                    plumberUserID = theSqlDataReader["UserID"].ToString();
                    Page.auditorID = theSqlDataReader["AuditorID"].ToString();
                    Page.isInspectorSubmitted = theSqlDataReader["isInspectorSubmitted"].ToString();
                    Page.isInvoiceSubmitted = theSqlDataReader["isInvoiceSubmitted"].ToString();
                    Page.refixDate = theSqlDataReader["DateRefix"].ToString();

                    Page.Status = theSqlDataReader["Status"].ToString();
                    CustomerID = theSqlDataReader["CustomerID"].ToString();
                    Page.isRefix = theSqlDataReader["isRefix"].ToString();
                    //string isRefix = theSqlDataReader["isRefix"].ToString();
                    //if (isRefix == "True")
                    //{
                    //    DisplayRefixNotice.Visible = true;
                    //}
                    //if (theSqlDataReader["DateRefix"] != DBNull.Value)
                    //{
                    //    NoDaysToComplete.Text = Convert.ToDateTime(theSqlDataReader["DateRefix"]).ToString("MM/dd/yyyy");
                    //}

                    Page.AorB = theSqlDataReader["AorB"].ToString();

                    string cocDisp = "";
                    if (theSqlDataReader["COCFileName"].ToString() != "" && theSqlDataReader["COCFileName"] != DBNull.Value)
                    {
                        cocDisp = "<embed src=\"https://197.242.82.242/inspectit/pdf/" + theSqlDataReader["COCFileName"].ToString() + "\" height=\"375\" type='application/pdf'>";
                    }
                    else if (theSqlDataReader["PaperBasedCOC"].ToString() != "" && theSqlDataReader["PaperBasedCOC"] != DBNull.Value)
                    {
                        cocDisp = "<img src=\"https://197.242.82.242/inspectit/pdf/" + theSqlDataReader["PaperBasedCOC"].ToString() + "\" style=\"height:375px;\" class=\"img-responsive\"/>";
                    }
                    Page.pdf = cocDisp;
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string compImg = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormImg where COCID = @COCID and isReference='0' and isCompleteImg='1' and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("COCID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string filename = theSqlDataReader["imgsrc"].ToString(); ;
                    if (theSqlDataReader["UserID"].ToString() == uid.ToString())
                    {
                        compImg += "<div class=\"col-md-3\" id=\"show_img_" + theSqlDataReader["ImgID"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader["imgid"].ToString() + "','" + cocid.ToString() + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                    }
                    else
                    {
                        compImg += "<div class=\"col-md-3\"><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"AuditorImgs/" + filename + "\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            Page.completedImgs = compImg;

            string vatIncl = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Auditor where AuditorID=@AuditorID";
            DLdb.SQLST.Parameters.AddWithValue("AuditorID", inspectorsID.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["vatregno"].ToString() != "" && theSqlDataReader["vatregno"] != DBNull.Value)
                    {
                        vatIncl = "True";
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            decimal inspectorRate = 0;
            decimal vats = 0;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Rates where ID = '39'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                inspectorRate = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from settings where ID='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                vats = Convert.ToDecimal(theSqlDataReader["VatPercentage"].ToString());
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            decimal vat = inspectorRate * Convert.ToDecimal(vats);
            if (vatIncl == "True")
            {
                decimal vatElec = inspectorRate + vat;
                Page.invAmountDisp = "Your invoice amount is R" + vatElec;
            }
            else
            {
                Page.invAmountDisp = "Your invoice amount is R" + inspectorRate;
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", plumberUserID.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.plumberName = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                    Page.plumberRegisNumber = theSqlDataReader["regno"].ToString();
                    Page.plumberEmail = theSqlDataReader["email"].ToString();
                    Page.plumberContact = theSqlDataReader["contact"].ToString();
                    Page.plumberImg = "https://197.242.82.242/inspectit/Photos/" + theSqlDataReader["Photo"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM AuditHistory where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.auidtHistoryCommentsPosted += "<br/>" + theSqlDataReader["AuditComment"].ToString() + "<hr/>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCPrivateComments where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string newComm = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        newComm = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DateTime dateCreatedView = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                    Page.lastprivatecomm = theSqlDataReader["Comments"].ToString() + "<br/> <small>" + newComm + " - " + dateCreatedView.ToString("dd/MM/yyyy") + "</small>";

                    // GET USER INSPECTOR DETAILS FOR RIGHT HAND SIDE COMMENTS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID and Role='Inspector'";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DateTime dateCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                            Page.inspectorDatePostedpriv = dateCreated.ToString("dd/MM/yyyy");
                            Page.inspecnamepriv = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                            Page.inspectorCommentspriv += "<p>" + theSqlDataReader["Comments"].ToString() + "</p>";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    // GET USER DETAILS FOR LEFT HAND SIDE COMMENTS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID and Role!='Inspector'";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DateTime dateCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                            Page.admindatepost = dateCreated.ToString("dd/MM/yyyy");
                            Page.adnames = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                            Page.adminscomm += "<p>" + theSqlDataReader["Comments"].ToString() + "</p>";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatementDetails where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.completeDate = theSqlDataReader["CompletedDate"].ToString();
                    Page.descriptionOfWork = theSqlDataReader["DescriptionofWork"].ToString();
                    Page.plumbercontactedStatus = theSqlDataReader["PlumberContacted"].ToString();
                    Page.clientContactedStatus = theSqlDataReader["ClientContacted"].ToString();
                    if (theSqlDataReader["ScheduledDate"] != DBNull.Value && theSqlDataReader["ScheduledDate"].ToString() != "")
                    {
                        DateTime scheduledDate = Convert.ToDateTime(theSqlDataReader["ScheduledDate"].ToString());
                        Page.scheduledDate = scheduledDate.ToString("MM/dd/yyyy");
                        Page.scheduledTime = scheduledDate.ToString("HH:mm tt");
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Customers where CustomerID = @CustomerID";
            DLdb.SQLST.Parameters.AddWithValue("CustomerID", CustomerID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.customerName = theSqlDataReader["CustomerName"].ToString();
                    Page.customerSurname = theSqlDataReader["CustomerSurname"].ToString();
                    Page.telNumber = theSqlDataReader["CustomerCellNo"].ToString();
                    Page.altTelNumber = theSqlDataReader["CustomerCellNoAlt"].ToString();
                    Page.customerEmail = theSqlDataReader["CustomerEmail"].ToString();
                    Page.street = theSqlDataReader["AddressStreet"].ToString();
                    Page.suburb = theSqlDataReader["AddressSuburb"].ToString();
                    Page.city = theSqlDataReader["AddressCity"].ToString();
                    Page.province = theSqlDataReader["Province"].ToString();

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive = '1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string checkeds = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM COCInstallations where COCStatementID = @COCStatementID and isActive = '1' and TypeID=@TypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("TypeID", theSqlDataReader["InstallationTypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            checkeds = "checked";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    Page.installationTypes += "<br/><input type='checkbox' disabled " + checkeds + " value='" + theSqlDataReader["InstallationTypeID"].ToString() + "'/>&nbsp;" + theSqlDataReader["InstallationType"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCInspectors where COCStatementID = @COCStatementID and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["InspectionDate"] == DBNull.Value)
                    {
                        //Page.hideAudit = "False";
                    }
                    else
                    {
                        //Page.hideAudit = "True";
                        Page.auditDate = theSqlDataReader["InspectionDate"].ToString();
                        Page.quality = theSqlDataReader["Quality"].ToString();
                        if (theSqlDataReader["Picture"] != DBNull.Value)
                        {
                            Page.inspecImg = "<img src=\"http://localhost:58752/noticeimages/" + theSqlDataReader["Picture"].ToString() + "\" class=\"img-responsive\" style='max-height:300px;' />";
                        }
                    }

                    if (theSqlDataReader["Invoice"].ToString() != "" || theSqlDataReader["Invoice"] != DBNull.Value)
                    {
                        Page.showreportbtn = "<a href=\"http://localhost:58752/Inspectorinvoices/" + theSqlDataReader["Invoice"].ToString() + "\" target=\"_blank\"><button type=\"button\" ID=\"btnViewPDF\" class=\"btn btn-default\">View Report</button></a>";
                    }

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string CurrentReview = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCReviews where COCStatementID = @COCStatementID and isActive = '1' order by createdate desc";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string StatusCol = "";
                    if (theSqlDataReader["status"].ToString() == "Failure")
                    {
                        StatusCol = "danger";
                    }
                    else if (theSqlDataReader["status"].ToString() == "Cautionary")
                    {
                        StatusCol = "warning";
                    }
                    else if (theSqlDataReader["status"].ToString() == "Compliment")
                    {
                        StatusCol = "success";
                    }

                    string InstallationType = "";

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from InstallationTypes where InstallationTypeID = @InstallationTypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            InstallationType = theSqlDataReader2["InstallationType"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string Media = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from FormImg where ReviewID = @ReviewID and isReference='0' and isActive='1'";
                    DLdb.SQLST2.Parameters.AddWithValue("ReviewID", theSqlDataReader["ReviewID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            string filename = theSqlDataReader2["imgsrc"].ToString(); ;
                            if (theSqlDataReader2["UserID"].ToString() == uid.ToString())
                            {
                                Media += "<div class=\"col-md-3\" id=\"show_img_" + theSqlDataReader2["ImgID"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader2["imgid"].ToString() + "','" + cocid.ToString() + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"http://localhost:58752/AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"http://localhost:58752/AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                            }
                            else
                            {
                                Media += "<div class=\"col-md-3\"><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"http://localhost:58752/AuditorImgs/" + filename + "\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string referenceMedia = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from FormImg where ReviewID = @ReviewID and isReference='1' and isActive='1'";
                    DLdb.SQLST2.Parameters.AddWithValue("ReviewID", theSqlDataReader["ReviewID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            string filename = theSqlDataReader2["imgsrc"].ToString(); ;
                            if (theSqlDataReader2["UserID"].ToString() == uid.ToString())
                            {
                                referenceMedia += "<div class=\"col-md-3\"><a href=\"http://localhost:58752/AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"http://localhost:58752/AuditorImgs/" + filename + "\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                                //referenceMedia += "<div class=\"col-md-3\" id=\"show_img_" + theSqlDataReader2["ImgID"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader2["imgid"].ToString() + "','"+DLdb.Decrypt(Request.QueryString["cocid"].ToString())+"')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                            }
                            else
                            {
                                referenceMedia += "<div class=\"col-md-3\"><a href=\"http://localhost:58752/AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"http://localhost:58752/AuditorImgs/" + filename + "\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string btnFix = "";
                    string btnDelete = "";
                    string btnCompleteRefix = "";
                    string btnNotFix = "";
                    string btnEdit = "";

                    // moved the below out of the 1st if
                    btnEdit = "<div class=\"col-md-2 text-right\"><div class=\"btn btn-primary\" onclick=\"document.location.href='EditReview.html?cocid=" + cocid.ToString() + "&rid=" + theSqlDataReader["ReviewID"].ToString() + "'\">Edit</div></div>";
                    btnDelete = "<div class=\"col-md-2 text-right\"><div class=\"btn btn-danger\" onclick=\"deleteReview(" + cocid.ToString() + ",'delReviewFromCOC'," + theSqlDataReader["ReviewID"].ToString() + ")\">Delete</div></div>";
                    if (theSqlDataReader["isFixed"].ToString() == "True" && theSqlDataReader["status"].ToString() == "Failure" && theSqlDataReader["isClosed"].ToString() == "False")
                    {
                        btnFix = "<input id=\"" + theSqlDataReader["ReviewID"].ToString() + "\" checked name=\"" + theSqlDataReader["ReviewID"].ToString() + "\" value=\"" + theSqlDataReader["ReviewID"].ToString() + "\" onclick=\"markCompleted('" + theSqlDataReader["ReviewID"].ToString() + "','" + theSqlDataReader["COCStatementID"].ToString() + "')\" type=\"checkbox\"/> Mark As Fixed";
                        btnDelete = "";
                        btnCompleteRefix = "<div class=\"col-md-2 text-right\"><label class=\"btn btn-primary\" onclick=\"markCompleteded('" + theSqlDataReader["ReviewID"].ToString() + "','" + theSqlDataReader["COCStatementID"].ToString() + "')\">Complete</label></div>";
                        // btnNotFix = "<label class=\"btn btn-danger\" onclick=\"markNotCompleted('" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "','" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "')\">Mark as Refix</label>";
                    }
                    else if (theSqlDataReader["isFixed"].ToString() == "True" && theSqlDataReader["status"].ToString() == "Failure" && theSqlDataReader["isClosed"].ToString() == "True")
                    {
                        btnEdit = "";
                        btnDelete = "";
                        btnFix = "<label class=\"label label-success\">Refix Complete</label>";
                    }
                    else if (theSqlDataReader["isFixed"].ToString() == "True" && theSqlDataReader["status"].ToString() == "Failure")
                    {
                        btnFix = "<input id=\"" + theSqlDataReader["ReviewID"].ToString() + "\" name=\"" + theSqlDataReader["ReviewID"].ToString() + "\" value=\"" + theSqlDataReader["ReviewID"].ToString() + "\" onclick=\"markCompleted('" + theSqlDataReader["ReviewID"].ToString() + "','" + theSqlDataReader["COCStatementID"].ToString() + "')\" type=\"checkbox\"/> Mark As Fixed";
                        //btnEdit = "<div class=\"col-md-12 text-right\"><div class=\"btn btn-primary\" onclick=\"document.location.href='EditReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "'\">Edit</div></div>";
                    }

                    CurrentReview += "<div class=\"row alert-" + StatusCol + "\" style=\"padding: 5px; \">" +
                                              "       <div class=\"col-md-12 text-right\">" +
                                                           btnFix + btnNotFix +
                                              "     </div>" +
                                              "       <div class=\"col-md-6 text-left\">" +
                                              "         <b>Instalation Type:</b> " + InstallationType + "<br />" +
                                              "         <b>Audit Status:</b> " + theSqlDataReader["status"].ToString() + "" +
                                              "     </div>" +
                                              "     <div class=\"col-md-6 text-left\">" +
                                              //"        <b>Reference:</b> " + theSqlDataReader["name"].ToString() + "" +
                                              "     </div>" +
                                              "     <div class=\"col-md-12 text-left\"><b>Comments:</b> " + theSqlDataReader["comment"].ToString() + "</div>" +
                                              "     <div class=\"col-md-12 text-left\"><b>Media:</b><br />" + Media + "</div>" +
                                              "     <div class=\"col-md-12 text-left\"><b>Reference:</b> " + theSqlDataReader["Reference"].ToString() + "</div>" +
                                              "     <div class=\"col-md-12 text-left\"><b>Media:</b><br />" + referenceMedia + "</div>" +
                                              "     <div class=\"col-md-12 text-right\"><div class=\"col-md-8\"></div>" + btnEdit + btnDelete + btnCompleteRefix + "</div>" +

                                              " </div><hr />";

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            Page.refixViews = CurrentReview;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCReviews where COCStatementID = @COCStatementID and isActive = '1' and isClosed='0'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.isClosedRefix = "False";
                }
            }
            else
            {
                Page.isClosedRefix = "True";
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();





            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getCities(string pid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string areas = "";

            DLdb.RS.Open();
            if (pid == "0")
            {
                DLdb.SQLST.CommandText = "select * from Area order by Name";
            }
            else
            {
                DLdb.SQLST.CommandText = "select * from Area where ProvinceID=@ProvinceID order by Name";
                DLdb.SQLST.Parameters.AddWithValue("ProvinceID", pid.ToString());
            }
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    areas += "<option value='" + theSqlDataReader["ID"].ToString() + "'>" + theSqlDataReader["Name"].ToString() + "</option>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            if (pid == "0")
            {
               
            }
            else
            {
                DLdb.SQLST.Parameters.RemoveAt(0);
            }
            DLdb.RS.Close();

            Context.Response.Write(areas);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getSuburbs(string sid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string areas = "";

            DLdb.RS.Open();
            if (sid == "0")
            {
                DLdb.SQLST.CommandText = "select * from areasuburbs order by Name";
            }
            else
            {
                DLdb.SQLST.CommandText = "select * from areasuburbs where CityID=@CityID order by Name";
                DLdb.SQLST.Parameters.AddWithValue("CityID", sid.ToString());
            }
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    areas += "<option value='" + theSqlDataReader["SuburbID"].ToString() + "'>" + theSqlDataReader["Name"].ToString() + "</option>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            if (sid == "0")
            {

            }
            else
            {
                DLdb.SQLST.Parameters.RemoveAt(0);
            }
            DLdb.RS.Close();

            Context.Response.Write(areas);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void addCocCommentPlumber(string uid, string cocid, string comment)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string refixNoticeId = "";

            // GET LATEST REFIX NOTICE ID TO INSERT A COMMENT SPECIFIC TO THAT ID
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCRefixes where COCStatementID=@COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    refixNoticeId = theSqlDataReader["RefixNoticeID"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // ADD COC REFIX COMMENTS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into COCRefixesComments (RefixNoticeID,COCStatementID,UserID,Comments) values (@RefixNoticeID,@COCStatementID,@UserID,@Comments)";
            DLdb.SQLST.Parameters.AddWithValue("RefixNoticeID", refixNoticeId);
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid);
            DLdb.SQLST.Parameters.AddWithValue("Comments", comment.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void saveCOCPlumber(string uid, string cocid, string installTypes, string tickab, string nonCompDetails, string descWork, string cusName, string cusSurname, string altTelNum, string suburb, string city, string province, string streetAddy, string email, string completeDate, string telNum)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string AuditorID = "";
            string AuditorIDUSerID = "";
            string NumberTo = "";
            string EmailAddress = "";
            string FullName = "";
            string CustomerID = "";
            string WorkCompleteby = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatementDetails where COCStatementID=@COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "update COCStatementDetails set CompletedDate=@CompletedDate,DescriptionofWork=@DescriptionofWork,WorkCompleteby=@WorkCompleteby where  COCStatementID=@COCStatementID";
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("CompletedDate", completeDate.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("DescriptionofWork", descWork.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("WorkCompleteby", WorkCompleteby);
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            else
            {
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "insert into COCStatementDetails (COCStatementID,CompletedDate,COCType,InsuranceCompany,PolicyHolder,PolicyNumber,isBank,PeriodOfInsuranceFrom,PeriodOfInsuranceTo,DescriptionofWork,WorkCompleteby) values (@COCStatementID,@CompletedDate,@COCType,@InsuranceCompany,@PolicyHolder,@PolicyNumber,@isBank,@PeriodOfInsuranceFrom,@PeriodOfInsuranceTo,@DescriptionofWork,@WorkCompleteby)";
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("CompletedDate", completeDate.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("COCType", "");
                DLdb.SQLST2.Parameters.AddWithValue("InsuranceCompany", "");
                DLdb.SQLST2.Parameters.AddWithValue("PolicyHolder", "");
                DLdb.SQLST2.Parameters.AddWithValue("PolicyNumber", "");
                DLdb.SQLST2.Parameters.AddWithValue("isBank", "");
                DLdb.SQLST2.Parameters.AddWithValue("PeriodOfInsuranceFrom", "");
                DLdb.SQLST2.Parameters.AddWithValue("PeriodOfInsuranceTo", "");
                DLdb.SQLST2.Parameters.AddWithValue("DescriptionofWork", descWork.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("WorkCompleteby", WorkCompleteby);
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

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
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from AreaSuburbs where SuburbID=@SuburbID";
            DLdb.SQLST.Parameters.AddWithValue("SuburbID", suburb.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                suburb = theSqlDataReader["Name"].ToString();
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from area where ID=@ID";
            DLdb.SQLST.Parameters.AddWithValue("ID", city.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                city = theSqlDataReader["Name"].ToString();
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from province where ID=@ID";
            DLdb.SQLST.Parameters.AddWithValue("ID", province.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                province = theSqlDataReader["Name"].ToString();
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into Customers (CustomerName,CustomerSurname,CustomerCellNo,CustomerCellNoAlt,CustomerEmail,AddressStreet,AddressSuburb,AddressCity,Province,AddressAreaCode,CustomerPassword,lat,lng) values (@CustomerName,@CustomerSurname,@CustomerCellNo,@CustomerCellNoAlt,@CustomerEmail,@AddressStreet,@AddressSuburb,@AddressCity,@Province,@AddressAreaCode,@CustomerPassword,@lat,@lng); Select Scope_Identity() as CustomerID";
            DLdb.SQLST.Parameters.AddWithValue("CustomerName", cusName.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CustomerSurname", cusSurname.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CustomerCellNo", telNum.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CustomerCellNoAlt", altTelNum.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CustomerEmail", email.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressStreet", streetAddy.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressSuburb", suburb.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressCity", city.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Province", province.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressAreaCode", "");
            DLdb.SQLST.Parameters.AddWithValue("CustomerPassword", DLdb.CreatePassword(8));
            DLdb.SQLST.Parameters.AddWithValue("lat", "");
            DLdb.SQLST.Parameters.AddWithValue("lng", "");
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
             theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
            DLdb.SQLST.CommandText = "update COCStatements set CustomerID = @CustomerID, Status = 'Non-logged Allocated', NonComplianceDetails = @NonComplianceDetails,COCNumber=COCStatementID where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CustomerID", CustomerID.ToString());
            DLdb.SQLST.Parameters.AddWithValue("NonComplianceDetails", nonCompDetails.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCInstallations set isactive='0' where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            List<string> ParamsListNew = installTypes.Split(',').ToList<string>();
            foreach (string types in ParamsListNew)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCInstallations where TypeID = @TypeID and COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("TypeID", types.ToString());
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "update COCInstallations set isActive = '1' where TypeID = @TypeID and COCStatementID = @COCStatementID";
                    DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("TypeID", types.ToString());
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
                    DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("TypeID", types.ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();

                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatements Set Status = 'Non-logged Allocated',AorB=@AorB,NonComplianceDetails=@NonComplianceDetails,AuditorID=@AuditorID,COCNumber=COCStatementID where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AuditorID", "");
            DLdb.SQLST.Parameters.AddWithValue("NonComplianceDetails", nonCompDetails.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AorB", tickab.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
            DLdb.SQLST.Parameters.AddWithValue("Message", "Plumber has saved the COC");
            DLdb.SQLST.Parameters.AddWithValue("Username", uid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
            DLdb.SQLST.Parameters.AddWithValue("CertificateID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void paperCocExists(string uid, string cocid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string paperBasedCOCs = "";
            string typeOfCOC = "";
            string paperCOCName = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID and Type='Paper'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(cocid.ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                typeOfCOC = theSqlDataReader["Type"].ToString();
                paperCOCName = theSqlDataReader["PaperBasedCOC"].ToString();
                if (theSqlDataReader["paperBasedCOC"].ToString() == "" || theSqlDataReader["paperBasedCOC"] == DBNull.Value)
                {
                    paperBasedCOCs = "false";

                }
                else
                {
                    paperBasedCOCs = "true";
                }
            }
            else
            {
                paperBasedCOCs = "true";
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Context.Response.Write(paperBasedCOCs);
        }

        [WebMethod]
        public void submitRefixPlumber(string uid, string cocid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.Parameters.Clear();
            DLdb.SQLST.CommandText = "update COCStatements Set isPlumberSubmitted='1',isInspectorSubmitted='0' where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

        }

        [WebMethod]
        public void addPrivateCommentAdmin(string uid, string comment, string cocid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into COCPrivateComments (COCStatementID,UserID,Comments) values (@COCStatementID,@UserID,@Comments)";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comments", comment.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            DLdb.DB_Close();
        }

        [WebMethod]
        public void addAuditHistory(string uid, string comment, string cocid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into AuditHistory (COCStatementID,UserID,AuditComment) values (@COCStatementID,@UserID,@AuditComment)";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AuditComment", comment.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            DLdb.DB_Close();
        }

        [WebMethod]
        public void markRefix(string rid, string op, string cocid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (op == "plumbFix")
            {
                string cocids = "";
                string plumberName = "";
                string audid = "";
                string auduid = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCReviews set isfixed='1' where ReviewID = @ReviewID";
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", rid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCReviews where ReviewID = @ReviewID";
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", rid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    cocids = theSqlDataReader["COCStatementID"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocids.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    audid = theSqlDataReader["AuditorID"].ToString();
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from Users where UserID = @UserID";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        plumberName = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                    }
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                DLdb.SQLST.Parameters.AddWithValue("Message", "Plumber marked the refix as fixed");
                DLdb.SQLST.Parameters.AddWithValue("Username", plumberName.ToString());
                DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
                DLdb.SQLST.Parameters.AddWithValue("CertificateID", cocid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string plumemail = "";
                string plumname = "";
                string plumnumber = "";

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Auditor where AuditorID=@AuditorID";
                DLdb.SQLST.Parameters.AddWithValue("AuditorID", audid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    auduid = theSqlDataReader["UserID"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", auduid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    plumemail = theSqlDataReader["email"].ToString();
                    plumnumber = theSqlDataReader["contact"].ToString();
                    plumname = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string HTMLSubject = "Inspect IT - C.O.C Refix Completed";
                string HTMLBody = "Dear " + plumname.ToString() + "<br /><br />COC Number " + cocid + " has been marked as fixed by the plumber.<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Team";
                DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumemail.ToString(), "");

                DLdb.sendSMS(auduid.ToString(), plumnumber.ToString(), "COC Number " + cocid + " has been marked as fixed by the plumber. ");

            }
            else if (op == "insFixNot")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCReviews set isfixed='0' where ReviewID = @ReviewID";
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", rid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string plumid = "";
                string plumemail = "";
                string plumname = "";
                string plumnumber = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCStatements where cocstatementid=@cocstatementid";
                DLdb.SQLST.Parameters.AddWithValue("cocstatementid", cocid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    plumid = theSqlDataReader["UserID"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", plumid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    plumemail = theSqlDataReader["email"].ToString();
                    plumnumber = theSqlDataReader["contact"].ToString();
                    plumname = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                DLdb.SQLST.Parameters.AddWithValue("Message", "Auditor marked the plumbers fix as not fixed");
                DLdb.SQLST.Parameters.AddWithValue("Username", Session["IIT_UName"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
                DLdb.SQLST.Parameters.AddWithValue("CertificateID", cocid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string HTMLSubject = "Inspect IT - C.O.C Refix Required";
                string HTMLBody = "Dear " + plumname.ToString() + "<br /><br />COC Number " + cocid.ToString() + " has been audited and a refix is required.<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Team";
                DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumemail.ToString(), "");

                DLdb.sendSMS(plumid.ToString(), plumnumber.ToString(), "COC Number " + cocid.ToString() + " has been audited and a refix is required. ");

            }
            else
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCReviews set isclosed='1', isfixed='1' where ReviewID = @ReviewID";
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", rid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCReviews where ReviewID = @ReviewID";
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", rid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    cocid = theSqlDataReader["COCStatementID"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string auduid = "";
                string audname = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from Auditor where AuditorID = @AuditorID";
                    DLdb.SQLST2.Parameters.AddWithValue("AuditorID", theSqlDataReader["AuditorID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        auduid = theSqlDataReader["UserID"].ToString();
                    }
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", auduid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    audname = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                DLdb.SQLST.Parameters.AddWithValue("Message", "Auditor marked the refix as Fixed and closed the problem");
                DLdb.SQLST.Parameters.AddWithValue("Username", audname.ToString());
                DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
                DLdb.SQLST.Parameters.AddWithValue("CertificateID", cocid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

            }

            DLdb.DB_Close();
        }

        [WebMethod]
        public void saveProgressManagement(string plumStat, string clientStat, string time, string date, string cocid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatementDetails set PlumberContacted=@PlumberContacted,ClientContacted=@ClientContacted,ScheduledDate=@ScheduledDate where cocstatementid=@cocstatementid ";
            DLdb.SQLST.Parameters.AddWithValue("PlumberContacted", plumStat.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ClientContacted", clientStat.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ScheduledDate", date.ToString() + " " + time.ToString());
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void saveAuditDate(string uid, string refixDate, string quality, string date, string cocid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCInspectors set InspectionDate = @InspectionDate, Status = @Status, Quality = @Quality where COCStatementID = @COCStatementID and UserID = @UserID and isactive='1'";
            DLdb.SQLST.Parameters.AddWithValue("InspectionDate", date.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Status", "");
            DLdb.SQLST.Parameters.AddWithValue("Quality", quality.ToString());
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "update COCStatements set DateRefix = @DateRefix where COCStatementID = @COCStatementID";
            DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST2.Parameters.AddWithValue("DateRefix", refixDate.ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getInspectorReviewTemplates(string uid, string cocid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";
            var Page = new addReviewTemplate();

            string instalTypes = "<option value=''></option>";
            string templates = "<option value=''></option>";

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "SELECT * FROM COCInstallations where isActive = '1' and COCStatementID=@COCStatementID";
            DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive = '1' and InstallationTypeID=@InstallationTypeID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader2["TypeID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            instalTypes += "<option value='" + theSqlDataReader["InstallationTypeID"].ToString() + "'>" + theSqlDataReader["InstallationType"].ToString() + "</option>";
                            // TypeOfInstallation.Items.Add(new ListItem(theSqlDataReader["InstallationType"].ToString(), theSqlDataReader["InstallationTypeID"].ToString()));
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from InspectorCommentTemplate where UserID = @UserID and isActive = '1' and TypeID=@TypeID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("TypeID", theSqlDataReader2["TypeID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            templates += "<option value='" + theSqlDataReader["CommentID"].ToString() + "'>" + theSqlDataReader["Name"].ToString() + "</option>";
                            // ReviewTemplate.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["CommentID"].ToString()));
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            Page.installTypes = instalTypes;
            Page.reviewTemplates = templates;

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getReviewTemplateDetails(string uid, string cocid, string templateid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";
            var Page = new ReviewTemplateDetails();

            string instalTypes = "";
            string templates = "";
            string subsTypes = "";
            string refImg = "";
            string commImg = "";
            //    public string comment { get; set; }
            //public string reference { get; set; }
            //public string installationType { get; set; }
            //public string subtypes { get; set; }
            //public string question { get; set; }
            //     public string referenceImgs { get; set; }
            //public string commentImgs { get; set; }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM InspectorCommentTemplate where commentid = @commentid";
            DLdb.SQLST.Parameters.AddWithValue("commentid", templateid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.comment = theSqlDataReader["Comment"].ToString();
                    Page.reference = theSqlDataReader["Reference"].ToString();
                    Page.installationType = theSqlDataReader["TypeID"].ToString();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from FormLinks l inner join formfields f on l.FormID = f.FormID where TypeID = @TypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("TypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            //FormFields.Items.Add(new ListItem(theSqlDataReader2["Label"].ToString(), theSqlDataReader2["FieldID"].ToString()));
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from InstallationTypessub where isActive = '1' and InstallationTypeID=@InstallationTypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            subsTypes += "<option value='" + theSqlDataReader["SubID"].ToString() + "'>" + theSqlDataReader["Name"].ToString() + "</option>";
                            //subTypes.Items.Add(new ListItem(theSqlDataReader2["Name"].ToString(), theSqlDataReader2["SubID"].ToString()));
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    Page.subtypes = subsTypes;

                    if (theSqlDataReader["FieldID"].ToString() == "" || theSqlDataReader["FieldID"] == DBNull.Value)
                    {
                        Page.question = theSqlDataReader["Question"].ToString();
                    }
                    else
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM formfields where isActive = '1' and FieldID=@FieldID";
                        DLdb.SQLST2.Parameters.AddWithValue("FieldID", theSqlDataReader["FieldID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                Page.question = theSqlDataReader2["label"].ToString();
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                    }

                    //string lFormFields = theSqlDataReader["FieldID"].ToString();
                    //FormFields.SelectedIndex = FormFields.Items.IndexOf(FormFields.Items.FindByValue(lFormFields));
                    Page.subtypesselected = theSqlDataReader["SubID"].ToString();
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormImg where CommentTemplateID = @CommentID and UserID=@UserID and isReference='1' and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", templateid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string filename = theSqlDataReader["imgsrc"].ToString(); ;

                    refImg += "<div class=\"col-md-3 img-thumnail\"><a href=\"http://localhost:58752/AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"http://localhost:58752/AuditorImgs/" + filename + "\" class=\"img-thumbnail img img-responsive\" /></a></div>";

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormImg where CommentTemplateID = @CommentID and UserID=@UserID and isReference='0' and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", templateid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string filename = theSqlDataReader["imgsrc"].ToString(); ;
                    string iid = "";
                    commImg += "<div class=\"col-md-3 img-thumnail\" style='margin-bottom:40px;'><a href=\"http://localhost:58752/AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"http://localhost:58752/AuditorImgs/" + filename + "\" class=\"img-thumbnail img img-responsive\" /></a><br/><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImagea('" + iid + "','" + cocid.ToString() + "')\"><i class=\"fa fa-trash\"></i></span></div>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            Page.referenceImgs = refImg;
            Page.commentImgs = commImg;

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);
            Context.Response.End();
            DLdb.DB_Close();
        }

        [WebMethod]
        public void installationTypeChange(string uid, string cocid, string installID)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string subsTypes = "<option value=''></option>";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypessub where isActive = '1' and InstallationTypeID=@InstallationTypeID";
            DLdb.SQLST.Parameters.AddWithValue("InstallationTypeID", installID.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    subsTypes += "<option value='" + theSqlDataReader["subID"].ToString() + "'>" + theSqlDataReader["Name"].ToString() + "</option>";
                    // subTypes.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["subID"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Context.Response.Write(subsTypes);
            Context.Response.End();
            DLdb.DB_Close();
        }

        [WebMethod]
        public void subTypeChange(string uid, string cocid, string subID)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string formfields = "<option value=''></option>";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from formfields where SubID = @SubID";
            DLdb.SQLST.Parameters.AddWithValue("SubID", subID.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    formfields += "<option value='" + theSqlDataReader["CommentTemplateID"].ToString() + "'>" + theSqlDataReader["Label"].ToString() + "</option>";
                    //FormFields.Items.Add(new ListItem(theSqlDataReader["Label"].ToString(), theSqlDataReader["CommentTemplateID"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Context.Response.Write(formfields);
            Context.Response.End();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void addReviewCompliment(string uid, string cocid, string comment)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO COCReviews (COCStatementID,UserID,Name,Reference,ReferenceImage,SubID, Comment,TypeID,question,status,CommentTemplateID) VALUES (@COCStatementID,@UserID,@Name,@Reference,@ReferenceImage,@SubID,@Comment,@TypeID,@question,@status,@CommentTemplateID); Select Scope_Identity() as CommentID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Name", "");
            DLdb.SQLST.Parameters.AddWithValue("Comment", comment.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TypeID", "0");
            DLdb.SQLST.Parameters.AddWithValue("SubID", "0");
            DLdb.SQLST.Parameters.AddWithValue("question", "");
            DLdb.SQLST.Parameters.AddWithValue("status", "Compliment");
            DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", "0");
            DLdb.SQLST.Parameters.AddWithValue("Reference", "");
            DLdb.SQLST.Parameters.AddWithValue("ReferenceImage", "");
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
            DLdb.RS.Close();

            Context.Response.Write("");
            Context.Response.End();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void updateReviewCompliment(string uid, string rid, string comment)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCReviews set isactive='1',Comment=@Comment where ReviewID=@ReviewID";
            DLdb.SQLST.Parameters.AddWithValue("ReviewID", rid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comment", comment.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Context.Response.Write("");
            Context.Response.End();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void ReviewTemplateChanged(string uid, string cocid, string templateid, string status)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string name = "";
            string comment = "";
            string reference = "";
            string question = "";
            string subid = "";
            string typeid = "";
            string formselected = "";
            string formfeilds = "";
            string subTypes = "";


            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM InspectorCommentTemplate where commentid = @commentid";
            DLdb.SQLST.Parameters.AddWithValue("commentid", templateid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    name = theSqlDataReader["Name"].ToString();
                    comment = theSqlDataReader["Comment"].ToString();
                    reference = theSqlDataReader["Reference"].ToString();
                    typeid = theSqlDataReader["TypeID"].ToString();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from FormLinks l inner join formfields f on l.FormID = f.FormID where TypeID = @TypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("TypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            formfeilds += "<option value='" + theSqlDataReader2["FieldID"].ToString() + "'>" + theSqlDataReader2["Label"].ToString() + "</option>";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from InstallationTypessub where isActive = '1' and InstallationTypeID=@InstallationTypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            subTypes += "<option value='" + theSqlDataReader2["SubID"].ToString() + "'>" + theSqlDataReader2["Name"].ToString() + "</option>";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    if (theSqlDataReader["FieldID"].ToString() == "" || theSqlDataReader["FieldID"] == DBNull.Value)
                    {
                        question = theSqlDataReader["Question"].ToString();
                    }
                    else
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM formfields where isActive = '1' and FieldID=@FieldID";
                        DLdb.SQLST2.Parameters.AddWithValue("FieldID", theSqlDataReader["FieldID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                question = theSqlDataReader2["label"].ToString();
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                    }

                    formselected = theSqlDataReader["FieldID"].ToString();
                    subid = theSqlDataReader["SubID"].ToString();
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string CommentID = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO COCReviews (COCStatementID,UserID,Name,Reference,ReferenceImage,SubID, Comment,TypeID,question,status,CommentTemplateID) VALUES (@COCStatementID,@UserID,@Name,@Reference,@ReferenceImage,@SubID,@Comment,@TypeID,@question,@status,@CommentTemplateID); Select Scope_Identity() as CommentID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Name", name.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comment", comment.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TypeID", typeid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("question", question.ToString());
            DLdb.SQLST.Parameters.AddWithValue("status", status);
            DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", templateid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Reference", reference.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ReferenceImage", "");
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                CommentID = theSqlDataReader["CommentID"].ToString();
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
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentTemplateID=@CommentTemplateID and isreference='0'";
            DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", templateid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,ReviewID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','0',@ReviewID)";
                    DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", uid.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("FieldID", "");
                    DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                    DLdb.SQLST2.Parameters.AddWithValue("ReviewID", CommentID);
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentTemplateID=@CommentTemplateID and isreference='1'";
            DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", templateid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,ReviewID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','1',@ReviewID)";
                    DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", uid.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("FieldID", "");
                    DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                    DLdb.SQLST2.Parameters.AddWithValue("ReviewID", CommentID);
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            //// ADD MEDIA COMMENTTEMPLATEID
            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "update FormImg set ReviewID = @ReviewID, tempID = null where tempID = @tempid and UserID = @UserID";
            //DLdb.SQLST.Parameters.AddWithValue("ReviewID", CommentID);
            //DLdb.SQLST.Parameters.AddWithValue("tempID", Session["IIT_tempID"]);
            //DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.RS.Close();

            string plumid = "";
            string plumemail = "";
            string plumname = "";
            string plumnumber = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatements where cocstatementid=@cocstatementid";
            DLdb.SQLST.Parameters.AddWithValue("cocstatementid", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                plumid = theSqlDataReader["UserID"].ToString();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", plumid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                plumemail = theSqlDataReader["email"].ToString();
                plumnumber = theSqlDataReader["contact"].ToString();
                plumname = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string auditorName = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                auditorName = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            if (status == "Failure")
            {
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "Update COCStatements set isRefix = '1' where COCStatementID = @COCStatementID";
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                DLdb.SQLST.Parameters.AddWithValue("Message", "Certificate has been marked for a refix");
                DLdb.SQLST.Parameters.AddWithValue("Username", auditorName.ToString());
                DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
                DLdb.SQLST.Parameters.AddWithValue("CertificateID", cocid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }

            DLdb.DB_Close();
            Context.Response.Write(CommentID);
            Context.Response.End();
        }

        [WebMethod]
        public void adminQuestionTemplateChanged(string uid, string cocid, string templateid, string status, string subid, string typid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string comment = "";
            string reference = "";
            string ques = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from ReportQuestionList where ID = @ID";
            DLdb.SQLST.Parameters.AddWithValue("ID", templateid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    comment = theSqlDataReader["Comment"].ToString();
                    reference = theSqlDataReader["Reference"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from FormFields where CommentTemplateID = @CommentTemplateID";
            DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", templateid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    ques = theSqlDataReader["label"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormImg where CommentID = @CommentID and isReference='1' and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", templateid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string filename = theSqlDataReader["imgsrc"].ToString(); ;

                    //Div1.InnerHtml += "<div class=\"col-md-3 img-thumnail\"><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"AuditorImgs/" + filename + "\" class=\"img-thumbnail img img-responsive\" /></a></div>";

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormImg where CommentID = @CommentID and isReference='0' and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", templateid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string filename = theSqlDataReader["imgsrc"].ToString(); ;
                    string iid = "";

                    // CurrentMedia.InnerHtml += "<div class=\"col-md-3 img-thumnail\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImagea('" + iid + "','" + DLdb.Decrypt(Request.QueryString["cocid"]) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"AuditorImgs/" + filename + "\" class=\"img-thumbnail img img-responsive\" /></a></div>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string CommentID = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO COCReviews (COCStatementID,UserID,Name,Reference,ReferenceImage,SubID, Comment,TypeID,question,status,CommentTemplateID) VALUES (@COCStatementID,@UserID,@Name,@Reference,@ReferenceImage,@SubID,@Comment,@TypeID,@question,@status,@CommentTemplateID); Select Scope_Identity() as CommentID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Name", "");
            DLdb.SQLST.Parameters.AddWithValue("Comment", comment.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TypeID", typid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("question", ques.ToString());
            DLdb.SQLST.Parameters.AddWithValue("status", status);
            DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", "0");
            DLdb.SQLST.Parameters.AddWithValue("Reference", reference.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ReferenceImage", "");
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                CommentID = theSqlDataReader["CommentID"].ToString();
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
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentID=@CommentID and isreference='0'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", templateid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,ReviewID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','0',@ReviewID)";
                    DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", uid.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("FieldID", templateid.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                    DLdb.SQLST2.Parameters.AddWithValue("ReviewID", CommentID);
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentID=@CommentID and isreference='1'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", templateid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,ReviewID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','1',@ReviewID)";
                    DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", uid.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("FieldID", templateid.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                    DLdb.SQLST2.Parameters.AddWithValue("ReviewID", CommentID);
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            //// ADD MEDIA COMMENTTEMPLATEID
            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "update FormImg set ReviewID = @ReviewID, tempID = null where tempID = @tempid and UserID = @UserID";
            //DLdb.SQLST.Parameters.AddWithValue("ReviewID", CommentID);
            //DLdb.SQLST.Parameters.AddWithValue("tempID", Session["IIT_tempID"]);
            //DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.RS.Close();

            string plumid = "";
            string plumemail = "";
            string plumname = "";
            string plumnumber = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatements where cocstatementid=@cocstatementid";
            DLdb.SQLST.Parameters.AddWithValue("cocstatementid", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                plumid = theSqlDataReader["UserID"].ToString();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", plumid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                plumemail = theSqlDataReader["email"].ToString();
                plumnumber = theSqlDataReader["contact"].ToString();
                plumname = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string auditorName = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                auditorName = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            if (status == "Failure")
            {
                //REQUIRED: SMS PLUMBER

                // SET TO REFIX
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "Update COCStatements set isRefix = '1' where COCStatementID = @COCStatementID";
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                DLdb.SQLST.Parameters.AddWithValue("Message", "Certificate has been marked for a refix");
                DLdb.SQLST.Parameters.AddWithValue("Username", auditorName.ToString());
                DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
                DLdb.SQLST.Parameters.AddWithValue("CertificateID", cocid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                //string HTMLSubject = "Inspect IT - C.O.C Refix Required";
                //string HTMLBody = "Dear " + plumname.ToString() + "<br /><br />COC Number " + cocid.ToString() + " has been audited and a refix is required.<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Team";
                //DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumemail.ToString(), "");

                //DLdb.sendSMS(plumid.ToString(), plumnumber.ToString(), "COC Number " + cocid.ToString() + " has been audited and a refix is required.");
            }

            Context.Response.Write("");
            Context.Response.End();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void editReviewDetails(string uid, string cocid, string rid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";
            
            var Page = new editReviewDetail();
            
            string commentTempID = "";
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive = '1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.installTypeDrop += "<option value='" + theSqlDataReader["InstallationTypeID"].ToString() + "'>" + theSqlDataReader["InstallationType"].ToString() + "</option>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCReviews where COCStatementID = @COCStatementID and ReviewID = @ReviewID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ReviewID", rid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.status = theSqlDataReader["status"].ToString();


                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM InstallationTypessub where isActive = '1' and InstallationTypeID=@InstallationTypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            Page.subTypeDrop += "<option value='" + theSqlDataReader["subID"].ToString() + "'>" + theSqlDataReader["Name"].ToString() + "</option>";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    commentTempID = theSqlDataReader["commentTemplateID"].ToString();
                    Page.comment = theSqlDataReader["Comment"].ToString();
                    Page.reference = theSqlDataReader["Reference"].ToString();
                    Page.subtype = theSqlDataReader["SubID"].ToString();
                    Page.type = theSqlDataReader["TypeID"].ToString();

                    if (theSqlDataReader["FieldID"].ToString() == "" || theSqlDataReader["FieldID"] == DBNull.Value)
                    {
                        Page.question = theSqlDataReader["Question"].ToString();
                    }
                    else
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM formfields where isActive = '1' and FieldID=@FieldID";
                        DLdb.SQLST2.Parameters.AddWithValue("FieldID", theSqlDataReader["FieldID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                Page.question = theSqlDataReader2["label"].ToString();
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                    }

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from FormImg where ReviewID = @ReviewID and isReference='1' and isActive='1'";
                    DLdb.SQLST2.Parameters.AddWithValue("ReviewID", rid.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            string filename = theSqlDataReader2["imgsrc"].ToString();
                            Page.referenceMedia += "<div class=\"col-md-3 img-thumbnail\"><a href=\"https://197.242.82.242/inspectit/AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"https://197.242.82.242/inspectit/AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from FormImg where ReviewID = @ReviewID and isReference='0' and isActive='1'";
                    DLdb.SQLST2.Parameters.AddWithValue("ReviewID", rid.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            string filename = theSqlDataReader2["imgsrc"].ToString();
                            Page.commentMedia += "<div class=\"col-md-3 img-thumbnail\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader2["imgid"].ToString() + "','" + cocid.ToString() + "','" + rid.ToString() + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"https://197.242.82.242/inspectit/AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"https://197.242.82.242/inspectit/AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            Context.Response.End();

            DLdb.DB_Close();
        }
        
        [WebMethod]
        public void cancelReview(string uid, string cocid, string rid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Update COCReviews set isActive='0' where ReviewID = @ReviewID";
            DLdb.SQLST.Parameters.AddWithValue("ReviewID", rid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update FormImg set isActive='0' where ReviewID = @ReviewID";
            DLdb.SQLST.Parameters.AddWithValue("ReviewID", rid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Context.Response.Write("");
            Context.Response.End();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void updateReview(string uid, string cocid, string rid, string status, string comment, string type, string subid, string question)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Update COCReviews set isactive='1',question=@question,SubID=@SubID,Comment = @Comment,TypeID = @TypeID,status = @status where ReviewID = @ReviewID and COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comment", comment.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TypeID", type.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("status", status);
            DLdb.SQLST.Parameters.AddWithValue("ReviewID", rid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("question", question.ToString());
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
            DLdb.RS.Close();
            
            Context.Response.Write("");
            Context.Response.End();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void deleteReviewCOC(string rid, string cocid, string op)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Update COCReviews set isActive='0' where ReviewID = @ReviewID and COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ReviewID", rid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Context.Response.Write("");
            Context.Response.End();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void sendOTP(string uid, string cocid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string smsNumber = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from users where userid=@userid";
            DLdb.SQLST.Parameters.AddWithValue("userid", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    smsNumber = theSqlDataReader["contact"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string OTPCode = DLdb.CreateNumber(5);
            //Session["IIT_OTPCodeSubAudit"] = OTPCode;
            DLdb.sendSMS(uid.ToString(), smsNumber.ToString(), "Inspect-It: You have requested to Log COC " + cocid.ToString() + ". OTP Code: " + OTPCode + ".  Report any fraudulent activity to PIRB Immediately.");


            Context.Response.Write(OTPCode);
            Context.Response.End();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void sendOTPChangePasswords(string uid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string smsNumber = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from users where userid=@userid";
            DLdb.SQLST.Parameters.AddWithValue("userid", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    smsNumber = theSqlDataReader["contact"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string OTPCode = DLdb.CreateNumber(5);
            //Session["IIT_OTPCodeSubAudit"] = OTPCode;
            DLdb.sendSMS(uid.ToString(), smsNumber.ToString(), "Inspect-It: You have requested to change your password. OTP Code: " + OTPCode + ".  Report any fraudulent activity to PIRB Immediately.");


            Context.Response.Write(OTPCode);
            Context.Response.End();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void updatePassword(string uid, string newPass,string oldPass)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string oldPasswordChange = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    oldPasswordChange = DLdb.Decrypt(theSqlDataReader["Password"].ToString());
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            if (oldPasswordChange== oldPass)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update Users set password=@password where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
                DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(newPass.ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Context.Response.Write("done");
                Context.Response.End();
            }
            else
            {
                Context.Response.Write("Old password is incorrect");
                Context.Response.End();
            }


            

            DLdb.DB_Close();
        }

        [WebMethod]
        public void submitCOCPlumber(string uid, string cocid, string installTypes, string tickab, string nonCompDetails, string descWork, string cusName, string cusSurname, string altTelNum, string suburb, string city, string province, string streetAddy, string email, string completeDate, string telNum)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string AuditorID = "";
            string AuditorIDUSerID = "";
            string NumberTo = "";
            string EmailAddress = "";
            string FullName = "";
            string CustomerID = "";
            string WorkCompleteby = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatementDetails where COCStatementID=@COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "update COCStatementDetails set CompletedDate=@CompletedDate,DescriptionofWork=@DescriptionofWork,WorkCompleteby=@WorkCompleteby where  COCStatementID=@COCStatementID";
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("CompletedDate", completeDate.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("DescriptionofWork", descWork.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("WorkCompleteby", WorkCompleteby);
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            else
            {
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "insert into COCStatementDetails (COCStatementID,CompletedDate,COCType,InsuranceCompany,PolicyHolder,PolicyNumber,isBank,PeriodOfInsuranceFrom,PeriodOfInsuranceTo,DescriptionofWork,WorkCompleteby) values (@COCStatementID,@CompletedDate,@COCType,@InsuranceCompany,@PolicyHolder,@PolicyNumber,@isBank,@PeriodOfInsuranceFrom,@PeriodOfInsuranceTo,@DescriptionofWork,@WorkCompleteby)";
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("CompletedDate", completeDate.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("COCType", "");
                DLdb.SQLST2.Parameters.AddWithValue("InsuranceCompany", "");
                DLdb.SQLST2.Parameters.AddWithValue("PolicyHolder", "");
                DLdb.SQLST2.Parameters.AddWithValue("PolicyNumber", "");
                DLdb.SQLST2.Parameters.AddWithValue("isBank", "");
                DLdb.SQLST2.Parameters.AddWithValue("PeriodOfInsuranceFrom", "");
                DLdb.SQLST2.Parameters.AddWithValue("PeriodOfInsuranceTo", "");
                DLdb.SQLST2.Parameters.AddWithValue("DescriptionofWork", descWork.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("WorkCompleteby", WorkCompleteby);
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

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
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into Customers (CustomerName,CustomerSurname,CustomerCellNo,CustomerCellNoAlt,CustomerEmail,AddressStreet,AddressSuburb,AddressCity,Province,AddressAreaCode,CustomerPassword,lat,lng) values (@CustomerName,@CustomerSurname,@CustomerCellNo,@CustomerCellNoAlt,@CustomerEmail,@AddressStreet,@AddressSuburb,@AddressCity,@Province,@AddressAreaCode,@CustomerPassword,@lat,@lng); Select Scope_Identity() as CustomerID";
            DLdb.SQLST.Parameters.AddWithValue("CustomerName", cusName.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CustomerSurname", cusSurname.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CustomerCellNo", telNum.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CustomerCellNoAlt", altTelNum.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CustomerEmail", email.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressStreet", streetAddy.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressSuburb", suburb.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressCity", city.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Province", province.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AddressAreaCode", "");
            DLdb.SQLST.Parameters.AddWithValue("CustomerPassword", DLdb.CreatePassword(8));
            DLdb.SQLST.Parameters.AddWithValue("lat", "");
            DLdb.SQLST.Parameters.AddWithValue("lng", "");
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
            DLdb.SQLST.CommandText = "update COCStatements set CustomerID = @CustomerID, Status = 'Non-logged Allocated', NonComplianceDetails = @NonComplianceDetails,COCNumber=COCStatementID where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CustomerID", CustomerID.ToString());
            DLdb.SQLST.Parameters.AddWithValue("NonComplianceDetails", nonCompDetails.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCInstallations set isactive='0' where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            List<string> ParamsListNew = installTypes.Split(',').ToList<string>();
            foreach (string types in ParamsListNew)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCInstallations where TypeID = @TypeID and COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("TypeID", types.ToString());
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "update COCInstallations set isActive = '1' where TypeID = @TypeID and COCStatementID = @COCStatementID";
                    DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("TypeID", types.ToString());
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
                    DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", cocid.ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("TypeID", types.ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();

                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }

            // veronike this isnt here in submit
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatements Set Status = 'Non-logged Allocated',AorB=@AorB,NonComplianceDetails=@NonComplianceDetails,AuditorID=@AuditorID,COCNumber=COCStatementID where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AuditorID", "");
            DLdb.SQLST.Parameters.AddWithValue("NonComplianceDetails", nonCompDetails.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AorB", tickab.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // veronike this isnt here in submit
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
            DLdb.SQLST.Parameters.AddWithValue("Message", "Plumber has saved the COC");
            DLdb.SQLST.Parameters.AddWithValue("Username", uid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
            DLdb.SQLST.Parameters.AddWithValue("CertificateID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();



            DLdb.DB_Close();
        }

        [WebMethod]
        public void deleteImagePDF(string uid, string cocid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatements set PaperBasedCOC='' WHERE COCStatementID=@COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Context.Response.Write("");
            Context.Response.End();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void deleteCompImage(string uid, string cocid, string imgid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update formimg set isActive='0' WHERE imgid=@imgid";
            DLdb.SQLST.Parameters.AddWithValue("imgid", imgid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Context.Response.Write("");
            Context.Response.End();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void invoiceNumberExists(string uid, string invID)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string err = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCInspectors where InvoiceNumber = @InvoiceNumber and UserID = @UserID and isactive='1'";
            DLdb.SQLST.Parameters.AddWithValue("InvoiceNumber", invID.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                err = "You have already used this invoice number, please generate another one.";
            }
            else
            {
                err = "done";
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            Context.Response.Write(err);
            Context.Response.End();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void inspectorSaveAudit(string uid, string cocid, string inspecDate, string quality, string refixDate)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCInspectors set InspectionDate = @InspectionDate, Status = @Status, Quality = @Quality where COCStatementID = @COCStatementID and UserID = @UserID and isactive='1'";
            DLdb.SQLST.Parameters.AddWithValue("InspectionDate", inspecDate.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Status", "");
            DLdb.SQLST.Parameters.AddWithValue("Quality", quality.ToString());
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "update COCStatements set DateRefix = @DateRefix where COCStatementID = @COCStatementID";
            DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST2.Parameters.AddWithValue("DateRefix", refixDate.ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            Context.Response.Write("");
            Context.Response.End();

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getPerformanceStatusDetails(string id)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";

            var Page = new performanceStatusEdit();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from PerformanceStatus where PerformanceStatusID = @PerformanceStatusID";
            DLdb.SQLST.Parameters.AddWithValue("PerformanceStatusID", id.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.Date = theSqlDataReader["Date"].ToString();
                    Page.PerformanceType = theSqlDataReader["PerformanceType"].ToString();
                    Page.PerformancePointAllocation = theSqlDataReader["PerformancePointAllocation"].ToString();
                    Page.Details = theSqlDataReader["Details"].ToString();
                    Page.Attachment = theSqlDataReader["Attachment"].ToString();
                    Page.endDate = theSqlDataReader["endDate"].ToString();
                    Page.hasEndDate = theSqlDataReader["hasEndDate"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getAssessmentDetails(string id)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";

            var Page = new AssessDetails();
            
        DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from AssessmentDetails where AssessID = @AssessID";
            DLdb.SQLST.Parameters.AddWithValue("AssessID", id.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.Date = theSqlDataReader["Date"].ToString();
                    Page.Time = theSqlDataReader["Time"].ToString();
                    Page.Type = theSqlDataReader["Type"].ToString();
                    Page.Location = theSqlDataReader["Location"].ToString();
                    Page.Attachment = theSqlDataReader["Attachment"].ToString();
                    Page.Result = theSqlDataReader["Result"].ToString();
                    string commss = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from AssessmentDetailsComments where AssessCommID = @AssessCommID";
                    DLdb.SQLST2.Parameters.AddWithValue("AssessCommID", id.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            commss += theSqlDataReader2["Comment"].ToString() + "<br/>";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    Page.comms = commss;
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void addAssessComment(string id, string comm)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into AssessmentDetailsComments (AssessID,Comment) values (@AssessID,@Comment)";
            DLdb.SQLST.Parameters.AddWithValue("AssessID", id.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comment", comm.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getApprenticeMentorshipDetails(string id)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";

            var Page = new apprenticeMentorshipEdit();
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from ApprenticeMentorShips where ApprenticeID = @ApprenticeID";
            DLdb.SQLST.Parameters.AddWithValue("ApprenticeID", id.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        Page.regno = theSqlDataReader2["regno"].ToString();
                        Page.surname = theSqlDataReader2["lname"].ToString();
                        Page.name = theSqlDataReader2["fname"].ToString();
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    Page.type = theSqlDataReader["type"].ToString();
                    Page.startDate = theSqlDataReader["startDate"].ToString();
                    Page.endDate = theSqlDataReader["endDate"].ToString();
                    Page.attachment = theSqlDataReader["attachment"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void searchUsersRegno(string id)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";

            var Page = new userDetails();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where RegNo = @RegNo";
            DLdb.SQLST.Parameters.AddWithValue("RegNo", id.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.regno = theSqlDataReader["regno"].ToString();
                    Page.surname = theSqlDataReader["lname"].ToString();
                    Page.name = theSqlDataReader["fname"].ToString();
                    Page.userid = theSqlDataReader["userid"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getDocumentDetails(string id)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";

            var Page = new documentEdit();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Documents where DocumentID = @DocumentID";
            DLdb.SQLST.Parameters.AddWithValue("DocumentID", id.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.DocumentAttached = "AssessmentImgs/" + theSqlDataReader["DocumentAttached"].ToString();
                    Page.Description = theSqlDataReader["Description"].ToString();
                    Page.Image = theSqlDataReader["DocumentAttached"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getCPDActivityDetails(string id)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";

            var Page = new cpdActivityView();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Assessments where AssessmentID = @AssessmentID";
            DLdb.SQLST.Parameters.AddWithValue("AssessmentID", id.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.Category = theSqlDataReader["Category"].ToString();
                    Page.Activity = theSqlDataReader["Activity"].ToString();
                    Page.NoPoints = theSqlDataReader["NoPoints"].ToString();
                    Page.productCode = theSqlDataReader["productCode"].ToString();
                    Page.CertificateDate = theSqlDataReader["CertificateDate"].ToString();
                    Page.comment = theSqlDataReader["comment"].ToString();
                    Page.Attachment = "AssessmentImgs/" + theSqlDataReader["Attachment"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getPerformancePoints(string id)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";
            

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from performancetypes where PerformanceID = @PerformanceID";
            DLdb.SQLST.Parameters.AddWithValue("PerformanceID", id.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    json = theSqlDataReader["Points"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Context.Response.Write(json);
        }

        [WebMethod]
        public void searchProductCodePerformanceActivity(string prodCode, string uid)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";

            var Page = new CPDActivities();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from CPDActivities where ProductCode = @ProductCode and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("ProductCode", prodCode.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.startDate = theSqlDataReader["startDate"].ToString();
                    Page.Points = theSqlDataReader["Points"].ToString();
                    Page.Activity = theSqlDataReader["Activity"].ToString();
                    Page.CPDActivityID = theSqlDataReader["CPDActivityID"].ToString();
                    Page.Category = theSqlDataReader["Category"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID = @UserID and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.regno = theSqlDataReader["regno"].ToString();
                    Page.name  = theSqlDataReader["fname"].ToString();
                    Page.surname = theSqlDataReader["lname"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void savePerformanceActivity(string prodCode, string uid, string comm, string cat, string CpdActId, string act, string point, string date, string dec)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";
            string declaration = "";
            if (dec=="on")
            {
                declaration = "1";
            }
            else
            {
                declaration = "0";
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into Assessments (UserID,Category,CPDActivityID,Activity,ProductCode,NoPoints,CertificateDate,Comment,Declaration) values (@UserID,@Category,@CPDActivityID,@Activity,@ProductCode,@NoPoints,@CertificateDate,@Comment,@Declaration)";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Category", cat.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CPDActivityID", CpdActId.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Activity", act.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ProductCode", prodCode.ToString());
            DLdb.SQLST.Parameters.AddWithValue("NoPoints", point.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CertificateDate", date.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comment", comm.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Declaration", declaration.ToString());
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
            DLdb.RS.Close();
            
            DLdb.DB_Close();
            Context.Response.Write(json);
        }

        [WebMethod]
        public void getCompanyInfo(string id)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";

            var Page = new companyDetailsNewRegistration();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Companies where CompanyID = @CompanyID";
            DLdb.SQLST.Parameters.AddWithValue("CompanyID", id.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.CompanyVAt = theSqlDataReader["VatNo"].ToString();
                    Page.empEmailaddress = theSqlDataReader["CompanyEmail"].ToString();
                    Page.empWorkPhone = theSqlDataReader["CompanyContactNo"].ToString();
                    Page.empAddress = theSqlDataReader["AddressLine1"].ToString();
                    Page.empCity = theSqlDataReader["City"].ToString();
                    Page.empProvince = theSqlDataReader["Province"].ToString();
                    Page.empSuburb = theSqlDataReader["Suburb"].ToString();
                    Page.empPostalAddress = theSqlDataReader["PostalAddress"].ToString();
                    Page.empPostalCity = theSqlDataReader["PostalCity"].ToString();
                    Page.empPostalCode = theSqlDataReader["PostalCode"].ToString();
                    Page.primaryContact = theSqlDataReader["PrimaryContact"].ToString();
                    Page.empMobile = theSqlDataReader["Mobile"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            

            json = JsonConvert.SerializeObject(Page);

            Context.Response.ContentType = "application/json";
            Context.Response.Write(json);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getCityFromProvincePostal(string id)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string options = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Area where ProvinceID=@ProvinceID order by name asc";
            DLdb.SQLST.Parameters.AddWithValue("ProvinceID", id);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    options += "<option value='"+theSqlDataReader["id"].ToString() + "'>" + theSqlDataReader["Name"].ToString() + "</option>";
                    //postalCities.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["id"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Context.Response.Write(options);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getSuburbFromCityPostal(string id)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string options = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from AreaSuburbs where CityID=@CityID order by name asc";
            DLdb.SQLST.Parameters.AddWithValue("CityID", id);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    options += "<option value='" + theSqlDataReader["suburbid"].ToString() + "'>" + theSqlDataReader["Name"].ToString() + "</option>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Context.Response.Write(options);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getCityFromProvincePhys(string id)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string options = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Area where ProvinceID=@ProvinceID order by name asc";
            DLdb.SQLST.Parameters.AddWithValue("ProvinceID", id);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    options += "<option value='" + theSqlDataReader["id"].ToString() + "'>" + theSqlDataReader["Name"].ToString() + "</option>";
                    //postalCities.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["id"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Context.Response.Write(options);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getSuburbFromCityPhys(string id)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string options = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from AreaSuburbs where CityID=@CityID order by name asc";
            DLdb.SQLST.Parameters.AddWithValue("CityID", id);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    options += "<option value='" + theSqlDataReader["suburbid"].ToString() + "'>" + theSqlDataReader["Name"].ToString() + "</option>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            Context.Response.Write(options);

            DLdb.DB_Close();
        }
        
        public class PerData
        {
            public string UserId { get; set; }
            public string Name { get; set; }
            public string Img { get; set; }
            public string Points { get; set; }
            public string Badges { get; set; }
        }

        [WebMethod]
        public void getGamificationPersonalChart(string UserID)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            IList<PerData> datalist = new List<PerData>();

            // FRIENDS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Friends f inner join users u on f.friendUserID = u.UserID where f.UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select sum(cast(Points as decimal(18,0))) as PTotal from AssignedWeighting where UserID = @UserID and isactive = '1' and createdate BETWEEN DATEADD(yy,DATEDIFF(yy,0,getdate()),0) AND DATEADD(ms,-3,DATEADD(yy, 0, DATEADD(yy, DATEDIFF(yy, 0, getdate()) + 1, 0)))";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["friendUserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            string Points = theSqlDataReader2["PTotal"].ToString();
                            if (theSqlDataReader2["PTotal"] == DBNull.Value)
                            {
                                Points = "0";
                            }
                            // GET BADGES
                            datalist.Add(new PerData() { UserId = theSqlDataReader["friendUserID"].ToString(), Name = theSqlDataReader["fName"].ToString() + " " + theSqlDataReader["lname"].ToString(), Img = theSqlDataReader["photo"].ToString(), Points = Points });
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // OWN
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select sum(cast(Points as decimal(18,0))) as PTotal from AssignedWeighting where UserID = @UserID and isactive = '1' and createdate BETWEEN DATEADD(yy,DATEDIFF(yy,0,getdate()),0) AND DATEADD(ms,-3,DATEADD(yy, 0, DATEADD(yy, DATEDIFF(yy, 0, getdate()) + 1, 0)))";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            string Points = theSqlDataReader2["PTotal"].ToString();
                            if (theSqlDataReader2["PTotal"] == DBNull.Value)
                            {
                                Points = "0";
                            }
                            // GET BADGES
                            datalist.Add(new PerData() { UserId = theSqlDataReader["UserID"].ToString(), Name = theSqlDataReader["fName"].ToString() + " " + theSqlDataReader["lname"].ToString(), Img = theSqlDataReader["photo"].ToString(), Points = Points });
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // WORK LIST
            //List<PerData> SortedList = datalist.OrderBy(o => o.Points).ToList();
            var SortedList = datalist.OrderByDescending(o => o.Points).ToList();
            string results = "<table border='0'><tr>";
            int cnt = 0;
            foreach (var item in SortedList.ToList())
            {
                string uid = item.Name;
                cnt++;
                string col = "red";
                if (Convert.ToInt32(item.UserId) == Convert.ToInt32(UserID))
                {
                    col = "blue";
                }

                int p = Convert.ToInt32(item.Points);
                float bh = 0;
                float ah = 300;
                if (p != 0)
                {
                    bh = p;
                    ah = 300 - bh;
                }

                results += "<td valign='bottom' style='padding:1px;'><table border='0' style='height:300px;'><tr><td valign='bottom' style='height:" + ah.ToString() + "px;'><img src='photos/" + item.Img + "' title='" + item.UserId + " - " + item.Name + "' style='width:30px;height:auto;border-radius:10px;' / ></td></tr><tr><td valign='top' style='background-color:" + col + ";height:" + bh.ToString() + "px;'>" + item.Points + "</td></tr></table></td>";
            }

            results += "</tr></table>";

            Context.Response.Write(results);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getGamificationPersonalboard(string UserID)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            IList<PerData> datalist = new List<PerData>();

            // FRIENDS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Friends f inner join users u on f.friendUserID = u.UserID where f.UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select sum(cast(Points as decimal(18,0))) as PTotal from AssignedWeighting where UserID = @UserID and isactive = '1' and createdate BETWEEN DATEADD(yy,DATEDIFF(yy,0,getdate()),0) AND DATEADD(ms,-3,DATEADD(yy, 0, DATEADD(yy, DATEDIFF(yy, 0, getdate()) + 1, 0)))";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["friendUserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            string Points = theSqlDataReader2["PTotal"].ToString();
                            if (theSqlDataReader2["PTotal"] == DBNull.Value)
                            {
                                Points = "0";
                            }
                            string badgesDisp = "";
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "select * from AssignedBadges where UserID = @UserID and isactive = '1'";
                            DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["friendUserID"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {
                                    DLdb.RS4.Open();
                                    DLdb.SQLST4.CommandText = "SELECT * FROM badges where BadgeID=@BadgeID";
                                    DLdb.SQLST4.Parameters.AddWithValue("BadgeID", theSqlDataReader3["BadgeID"].ToString());
                                    DLdb.SQLST4.CommandType = CommandType.Text;
                                    DLdb.SQLST4.Connection = DLdb.RS4;
                                    SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                                    if (theSqlDataReader4.HasRows)
                                    {
                                        while (theSqlDataReader4.Read())
                                        {
                                            badgesDisp += "<img src='Badges/" + theSqlDataReader4["Badge"].ToString() + "' style='height:20px;width:20px;'  title=\"" + theSqlDataReader4["Item"].ToString() + "\"/>";
                                        }
                                    }

                                    if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                                    DLdb.SQLST4.Parameters.RemoveAt(0);
                                    DLdb.RS4.Close();
                                }
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();
                            // GET BADGES
                            datalist.Add(new PerData() { UserId = theSqlDataReader["friendUserID"].ToString(), Name = theSqlDataReader["fName"].ToString() + " " + theSqlDataReader["lname"].ToString(), Img = theSqlDataReader["photo"].ToString(), Points = Points, Badges = badgesDisp });
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // OWN
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select sum(cast(Points as decimal(18,0))) as PTotal from AssignedWeighting where UserID = @UserID and isactive = '1' and createdate BETWEEN DATEADD(yy,DATEDIFF(yy,0,getdate()),0) AND DATEADD(ms,-3,DATEADD(yy, 0, DATEADD(yy, DATEDIFF(yy, 0, getdate()) + 1, 0)))";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            string Points = theSqlDataReader2["PTotal"].ToString();
                            if (theSqlDataReader2["PTotal"] == DBNull.Value)
                            {
                                Points = "0";
                            }
                            string badgesDisp = "";
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "select * from AssignedBadges where UserID = @UserID and isactive = '1'";
                            DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {
                                    DLdb.RS4.Open();
                                    DLdb.SQLST4.CommandText = "SELECT * FROM badges where BadgeID=@BadgeID";
                                    DLdb.SQLST4.Parameters.AddWithValue("BadgeID", theSqlDataReader3["BadgeID"].ToString());
                                    DLdb.SQLST4.CommandType = CommandType.Text;
                                    DLdb.SQLST4.Connection = DLdb.RS4;
                                    SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                                    if (theSqlDataReader4.HasRows)
                                    {
                                        while (theSqlDataReader4.Read())
                                        {
                                            badgesDisp += "<img src='Badges/" + theSqlDataReader4["Badge"].ToString() + "' style='height:20px;width:20px;'  title=\"" + theSqlDataReader4["Item"].ToString() + "\"/>";
                                        }
                                    }

                                    if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                                    DLdb.SQLST4.Parameters.RemoveAt(0);
                                    DLdb.RS4.Close();
                                }
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();

                            // GET BADGES
                            datalist.Add(new PerData() { UserId = theSqlDataReader["UserID"].ToString(), Name = theSqlDataReader["fName"].ToString() + " " + theSqlDataReader["lname"].ToString(), Img = theSqlDataReader["photo"].ToString(), Points = Points,Badges= badgesDisp });
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // WORK LIST
            //List<PerData> SortedList = datalist.OrderBy(o => o.Points).ToList();
            var SortedList = datalist.OrderByDescending(o => o.Points).ToList();
            string results = "";
            int cnt = 0;
            foreach (var item in SortedList.ToList())
            {
                string uid = item.Name;
                cnt++;
                string col = "red";
                if (Convert.ToInt32(item.UserId) == Convert.ToInt32(UserID))
                {
                    col = "blue";
                }

                int p = Convert.ToInt32(item.Points);
                float bh = 0;
                float ah = 300;
                if (p != 0)
                {
                    bh = p;
                    ah = 300 - bh;
                }

                results += "<tr>" +
                    "<td>" + item.Name + "</td>" +
                    "<td>" + item.Points + "</td>" +
                    "<td>" + item.Badges + "</td>" +
                    "<td><div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeleteFriend.aspx?uid=" + item.UserId + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                    "</tr>";

            }
            
            Context.Response.Write(results);

            DLdb.DB_Close();
        }
        
        [WebMethod]
        public void getGamificationleaderBoardChart(string UserID)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            IList<PerData> datalist = new List<PerData>();

            // FRIENDS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select top 10 ROW_NUMBER() OVER(ORDER BY sum(cast(Points as decimal)) desc) AS Row,sum(cast(Points as decimal)) as tot,u.UserID,u.fname,u.lname,u.Photo from users u inner join AssignedWeighting a on u.UserID=a.UserID group by u.UserID,u.fname,u.lname,u.Photo";
            DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    datalist.Add(new PerData() { UserId = theSqlDataReader["UserID"].ToString(), Name = theSqlDataReader["fName"].ToString() + " " + theSqlDataReader["lname"].ToString(), Img = theSqlDataReader["photo"].ToString(), Points = theSqlDataReader["tot"].ToString() });
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            

            // WORK LIST
            //List<PerData> SortedList = datalist.OrderBy(o => o.Points).ToList();
            var SortedList = datalist.OrderByDescending(o => o.Points).ToList();
            string results = "<table border='0'><tr>";
            int cnt = 0;
            foreach (var item in SortedList.ToList())
            {
                string uid = item.Name;
                cnt++;
                string col = "red";
                if (Convert.ToInt32(item.UserId) == Convert.ToInt32(UserID))
                {
                    col = "blue";
                }

                int p = Convert.ToInt32(item.Points);
                float bh = 0;
                float ah = 300;
                if (p != 0)
                {
                    bh = p;
                    ah = 300 - bh;
                }

                results += "<td valign='bottom' style='padding:1px;'><table border='0' style='height:300px;'><tr><td valign='bottom' style='height:" + ah.ToString() + "px;'><img src='photos/" + item.Img + "' title='" + item.UserId + " - " + item.Name + "' style='width:30px;height:auto;border-radius:10px;' / ></td></tr><tr><td valign='top' style='background-color:" + col + ";height:" + bh.ToString() + "px;'>" + item.Points + "</td></tr></table></td>";
            }

            results += "</tr></table>";

            Context.Response.Write(results);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getGamificationLeaderboard(string UserID)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            IList<PerData> datalist = new List<PerData>();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select top 50 ROW_NUMBER() OVER(ORDER BY sum(cast(Points as decimal)) desc) AS Row,sum(cast(Points as decimal)) as tot,u.UserID,u.fname,u.lname,u.Photo from users u inner join AssignedWeighting a on u.UserID=a.UserID group by u.UserID,u.fname,u.lname,u.Photo";
            DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string badgesDisp = "";
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "select * from AssignedBadges where UserID = @UserID and isactive = '1'";
                    DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        while (theSqlDataReader3.Read())
                        {
                            DLdb.RS4.Open();
                            DLdb.SQLST4.CommandText = "SELECT * FROM badges where BadgeID=@BadgeID";
                            DLdb.SQLST4.Parameters.AddWithValue("BadgeID", theSqlDataReader3["BadgeID"].ToString());
                            DLdb.SQLST4.CommandType = CommandType.Text;
                            DLdb.SQLST4.Connection = DLdb.RS4;
                            SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                            if (theSqlDataReader4.HasRows)
                            {
                                while (theSqlDataReader4.Read())
                                {
                                    badgesDisp += "<img src='Badges/" + theSqlDataReader4["Badge"].ToString() + "' style='height:20px;width:20px;'  title=\"" + theSqlDataReader4["Item"].ToString() + "\"/>";
                                }
                            }

                            if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                            DLdb.SQLST4.Parameters.RemoveAt(0);
                            DLdb.RS4.Close();
                        }
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();

                    datalist.Add(new PerData() { UserId = theSqlDataReader["UserID"].ToString(), Name = theSqlDataReader["fName"].ToString() + " " + theSqlDataReader["lname"].ToString(), Img = theSqlDataReader["photo"].ToString(), Points = theSqlDataReader["tot"].ToString(), Badges = badgesDisp });
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // WORK LIST
            //List<PerData> SortedList = datalist.OrderBy(o => o.Points).ToList();
            var SortedList = datalist.OrderByDescending(o => o.Points).ToList();
            string results = "";
            int cnt = 0;
            foreach (var item in SortedList.ToList())
            {
                string uid = item.Name;
                cnt++;
                string col = "red";
                if (Convert.ToInt32(item.UserId) == Convert.ToInt32(UserID))
                {
                    col = "blue";
                }

                int p = Convert.ToInt32(item.Points);
                float bh = 0;
                float ah = 300;
                if (p != 0)
                {
                    bh = p;
                    ah = 300 - bh;
                }

                results += "<tr>" +
                    "<td>" + item.Name + "</td>" +
                    "<td>" + item.Points + "</td>" +
                    "<td>" + item.Badges + "</td>" +
                    "<td></td>" +
                    "</tr>";

            }

            Context.Response.Write(results);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getGamificationRegionalChart(string UserID, string sel)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            IList<PerData> datalist = new List<PerData>();
            int totalItems = 0;
            int totalPoints = 0;
            int average = 0;
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Province";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select avg(cast(Points as decimal(18,0))) as PTotal from AssignedWeighting where userid in (select UserID from users where Province = @Province) and isactive = '1' and [type] = 'CPD'";
                    DLdb.SQLST2.Parameters.AddWithValue("Province", theSqlDataReader["Name"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            if (theSqlDataReader2["PTotal"] != DBNull.Value)
                            {
                                datalist.Add(new PerData() { UserId = theSqlDataReader["ID"].ToString(), Name = theSqlDataReader["Name"].ToString(), Img = "", Points = theSqlDataReader2["PTotal"].ToString() });
                            }
                            else
                            {
                                datalist.Add(new PerData() { UserId = theSqlDataReader["ID"].ToString(), Name = theSqlDataReader["Name"].ToString(), Img = "", Points = "0" });
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();
            
            // WORK LIST
            //List<PerData> SortedList = datalist.OrderBy(o => o.Points).ToList();
            var SortedList = datalist.OrderByDescending(o => o.Points).ToList();
            string results = "<table border='0'><tr>";
            int cnt = 0;
            foreach (var item in SortedList.ToList())
            {
                string uid = item.Name;
                cnt++;
                string col = "red";
                if (Convert.ToInt32(item.UserId) == Convert.ToInt32(UserID))
                {
                    col = "blue";
                }

                int p = Convert.ToInt32(item.Points);
                float bh = 0;
                float ah = 300;
                if (p != 0)
                {
                    bh = p;
                    ah = 300 - bh;
                }

                results += "<td valign='bottom' style='padding:1px;'><table border='0' style='height:300px;'><tr><td valign='bottom' style='height:" + ah.ToString() + "px;'><img src='photos/" + item.Img + "' style='width:30px;height:auto;border-radius:10px;' title='" + item.UserId + " - " + item.Name + "' / ></td></tr><tr><td valign='top' style='background-color:" + col + ";height:" + bh.ToString() + "px;'>" + item.Points + "</td></tr></table></td>";
            }

            results += "</tr></table>";

            Context.Response.Write(results);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getGamificationRegionalboard(string UserID, string sel)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            IList<PerData> datalist = new List<PerData>();
            if (sel == "Total Average")
            {

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select ROW_NUMBER() OVER(ORDER BY sum(cast(Points as decimal)) desc) AS Row,sum(cast(Points as decimal)) as tot,u.Province,p.ID from users u inner join AssignedWeighting a on u.UserID=a.UserID inner join Province p on u.Province=p.Name group by u.Province ,p.ID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        datalist.Add(new PerData() { UserId = theSqlDataReader["ID"].ToString(), Name = theSqlDataReader["Province"].ToString(), Img = "", Points = theSqlDataReader["tot"].ToString() });
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }
            else if (sel == "Total Average CPD")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select ROW_NUMBER() OVER(ORDER BY sum(cast(NoPoints as decimal)) desc) AS Row,sum(cast(NoPoints as decimal)) as tot,u.Province,p.ID from users u inner join Assessments a on u.UserID=a.UserID inner join Province p on u.Province=p.Name group by u.Province ,p.ID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        datalist.Add(new PerData() { UserId = theSqlDataReader["ID"].ToString(), Name = theSqlDataReader["Province"].ToString(), Img = "", Points = theSqlDataReader["tot"].ToString() });
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }
            else if (sel == "Total Average Performance Status")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select ROW_NUMBER() OVER(ORDER BY sum(cast(PerformancePointAllocation as decimal)) desc) AS Row,sum(cast(PerformancePointAllocation as decimal)) as tot,u.Province,p.ID from users u inner join PerformanceStatus a on u.UserID=a.UserID inner join Province p on u.Province=p.Name group by u.Province ,p.ID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        datalist.Add(new PerData() { UserId = theSqlDataReader["ID"].ToString(), Name = theSqlDataReader["Province"].ToString(), Img = "", Points = theSqlDataReader["tot"].ToString() });
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }

                // WORK LIST
                //List<PerData> SortedList = datalist.OrderBy(o => o.Points).ToList();
                var SortedList = datalist.OrderByDescending(o => o.Points).ToList();
            string results = "";
            int cnt = 0;
            foreach (var item in SortedList.ToList())
            {
                string uid = item.Name;
                cnt++;
                string col = "red";
                if (Convert.ToInt32(item.UserId) == Convert.ToInt32(UserID))
                {
                    col = "blue";
                }

                int p = Convert.ToInt32(item.Points);
                float bh = 0;
                float ah = 300;
                if (p != 0)
                {
                    bh = p;
                    ah = 300 - bh;
                }

                results += "<tr>" +
                    "<td>" + item.Name + "</td>" +
                    "<td>" + item.Points + "</td>" +
                    "<td></td>" +
                    "<td></td>" +
                    "</tr>";

            }

            Context.Response.Write(results);

            DLdb.DB_Close();
        }

        [WebMethod]
        public void getGamificationIntermediateChart(string UserID)
        {
            Global DLdb = new Global();

            DLdb.DB_Connect();

            string UserPoints = "0";
            IList<PerData> datalist = new List<PerData>();

            // OWN
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select sum(cast(Points as decimal(18,0))) as PTotal from AssignedWeighting where UserID = @UserID and isactive = '1' and createdate BETWEEN DATEADD(yy,DATEDIFF(yy,0,getdate()),0) AND DATEADD(ms,-3,DATEADD(yy, 0, DATEADD(yy, DATEDIFF(yy, 0, getdate()) + 1, 0)))";
            DLdb.SQLST2.Parameters.AddWithValue("UserID", UserID);
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    UserPoints = theSqlDataReader2["PTotal"].ToString();
                    if (theSqlDataReader2["PTotal"] == DBNull.Value)
                    {
                        UserPoints = "0";
                    }
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            // GET TOP 5
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select TOP 5 * from (select sum(cast(Points as decimal(18,0))) as PTotal from AssignedWeighting where isactive = '1' and UserID <> @UserID and createdate BETWEEN DATEADD(yy,DATEDIFF(yy,0,getdate()),0) AND DATEADD(ms,-3,DATEADD(yy, 0, DATEADD(yy, DATEDIFF(yy, 0, getdate()) + 1, 0)))) as M where M.PTotal >= @UserPoints";
            DLdb.SQLST2.Parameters.AddWithValue("UserPoints", UserPoints);
            DLdb.SQLST2.Parameters.AddWithValue("UserID", UserID);
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    string Points = theSqlDataReader2["PTotal"].ToString();
                    if (theSqlDataReader2["PTotal"] == DBNull.Value)
                    {
                        Points = "0";
                    }

                    DLdb.RS4.Open();
                    DLdb.SQLST4.CommandText = "Select * from Users where UserID = @UserID";
                    DLdb.SQLST4.Parameters.AddWithValue("UserID", UserID);
                    DLdb.SQLST4.CommandType = CommandType.Text;
                    DLdb.SQLST4.Connection = DLdb.RS4;
                    SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                    if (theSqlDataReader4.HasRows)
                    {
                        while (theSqlDataReader4.Read())
                        {
                            datalist.Add(new PerData() { UserId = theSqlDataReader4["UserID"].ToString(), Name = theSqlDataReader4["fName"].ToString() + " " + theSqlDataReader4["lname"].ToString(), Img = theSqlDataReader4["photo"].ToString(), Points = Points });
                        }
                    }

                    if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                    DLdb.SQLST4.Parameters.RemoveAt(0);
                    DLdb.RS4.Close();

                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            // OWN
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select sum(cast(Points as decimal(18,0))) as PTotal from AssignedWeighting where UserID = @UserID and isactive = '1' and createdate BETWEEN DATEADD(yy,DATEDIFF(yy,0,getdate()),0) AND DATEADD(ms,-3,DATEADD(yy, 0, DATEADD(yy, DATEDIFF(yy, 0, getdate()) + 1, 0)))";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlReader3 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlReader3.HasRows)
                    {
                        while (theSqlReader3.Read())
                        {
                            string Points = theSqlReader3["PTotal"].ToString();
                            if (theSqlReader3["PTotal"] == DBNull.Value)
                            {
                                Points = "0";
                            }
                            // GET BADGES
                            datalist.Add(new PerData() { UserId = theSqlDataReader["UserID"].ToString(), Name = theSqlDataReader["fName"].ToString() + " " + theSqlDataReader["lname"].ToString(), Img = theSqlDataReader["photo"].ToString(), Points = Points });
                        }
                    }

                    if (theSqlReader3.IsClosed) theSqlReader3.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            //GET BOTTOM 5
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select TOP 5 * from (select sum(cast(Points as decimal(18,0))) as PTotal from AssignedWeighting where isactive = '1' and UserID <> @UserID and createdate BETWEEN DATEADD(yy,DATEDIFF(yy,0,getdate()),0) AND DATEADD(ms,-3,DATEADD(yy, 0, DATEADD(yy, DATEDIFF(yy, 0, getdate()) + 1, 0)))) as M where M.PTotal <= @UserPoints";
            DLdb.SQLST2.Parameters.AddWithValue("UserPoints", UserPoints);
            DLdb.SQLST2.Parameters.AddWithValue("UserID", UserID);
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    string Points = theSqlDataReader2["PTotal"].ToString();
                    if (theSqlDataReader2["PTotal"] == DBNull.Value)
                    {
                        Points = "0";
                    }

                    DLdb.RS4.Open();
                    DLdb.SQLST4.CommandText = "Select * from Users where UserID = @UserID";
                    DLdb.SQLST4.Parameters.AddWithValue("UserID", UserID);
                    DLdb.SQLST4.CommandType = CommandType.Text;
                    DLdb.SQLST4.Connection = DLdb.RS4;
                    SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                    if (theSqlDataReader4.HasRows)
                    {
                        while (theSqlDataReader4.Read())
                        {
                            datalist.Add(new PerData() { UserId = theSqlDataReader4["UserID"].ToString(), Name = theSqlDataReader4["fName"].ToString() + " " + theSqlDataReader4["lname"].ToString(), Img = theSqlDataReader4["photo"].ToString(), Points = Points });
                        }
                    }

                    if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                    DLdb.SQLST4.Parameters.RemoveAt(0);
                    DLdb.RS4.Close();
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();


            string results = "<table border='0'><tr>";
            int cnt = 0;
            foreach (var item in datalist.ToList())
            {
                string uid = item.Name;
                cnt++;
                string col = "red";
                if (Convert.ToInt32(item.UserId) == Convert.ToInt32(UserID))
                {
                    col = "blue";
                }

                int p = Convert.ToInt32(item.Points);
                float bh = 0;
                float ah = 300;
                if (p != 0)
                {
                    bh = p;
                    ah = 300 - bh;
                }

                results += "<td valign='bottom' style='padding:1px;'><table border='0' style='height:300px;'><tr><td valign='bottom' style='height:" + ah.ToString() + "px;'><img src='photos/" + item.Img + "' style='width:30px;height:auto;border-radius:10px;' title='" + item.UserId + " - " + item.Name + "' / ></td></tr><tr><td valign='top' style='background-color:" + col + ";height:" + bh.ToString() + "px;'>" + item.Points + "</td></tr></table></td>";
            }

            results += "</tr></table>";

            Context.Response.Write(results);

            DLdb.DB_Close();
        }

    }
}
