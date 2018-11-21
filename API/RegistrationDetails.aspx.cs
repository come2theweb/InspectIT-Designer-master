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
    public partial class RegistrationDetails : System.Web.UI.Page
    {
        public class RegDetails
        {
            public string RegistrationNumber { get; set; }
            public string TitleStatus { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string IDNumber { get; set; }
            public string RegistrationRenewalDate { get; set; }
            public string Notice { get; set; }
            public string NonLoggedCOCs { get; set; }
            public string NonLoggedCOCsAllocated { get; set; }
            public string NumberCOCsLogged { get; set; }
            public string NumberCOCsAudited { get; set; }
            public string RefixNotices { get; set; }
            public string AddressStreet { get; set; }
            public string AddressSuburb { get; set; }
            public string AddressCity { get; set; }
            public string AddressProvince { get; set; }
            public string AddressAreaCode { get; set; }
            public string PostalAddress { get; set; }
            public string PostalCity { get; set; }
            public string PostalCode { get; set; }
            public string HomeNo { get; set; }
            public string MobileNo { get; set; }
            public string EmploymentDate { get; set; }
            public string Email { get; set; }
            public string InsuranceCompany { get; set; }
            public string PolicyHolder { get; set; }
            public string PolicyNumber { get; set; }
            public string PeriodOfInsuranceFrom { get; set; }
            public string PeriodOfInsuranceTo { get; set; }
           
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();

            //// CHECK SESSION
            //if (Session["IIT_UID"] == null)
            //{
            //    Response.Redirect("Default");
            //}

            //// ADMIN CHECK
            //if (Session["IIT_Role"].ToString() != "Administrator")
            //{
            //    Response.Redirect("Default");
            //}

            ////if (Request.QueryString["msg"] != null)
            ////{
            ////    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
            ////    successmsg.InnerHtml = msg;
            ////    successmsg.Visible = true;
            ////}

            ////if (Request.QueryString["err"] != null)
            ////{
            ////    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
            ////    errormsg.InnerHtml = msg;
            ////    errormsg.Visible = true;
            ////}

            ////if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.RS.Close();

            //DLdb.DB_Close();
            //displayusers.InnerHtml = "";

            //Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();

            string json = "";

            DLdb.SQLST.CommandText = "SELECT * FROM RegistrationDetails";
            DLdb.SQLST.Parameters.AddWithValue("RegistrationID", Request.QueryString["RegistrationID"].ToString());
            // DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                var list = new List<RegDetails>();
                var Details = new RegDetails();
                while (theSqlDataReader.Read())
                {
                    Details.RegistrationNumber = theSqlDataReader["RegistrationNumber"].ToString();
                    Details.TitleStatus = theSqlDataReader["TitleStatus"].ToString();
                    Details.FirstName = theSqlDataReader["FirstName"].ToString();
                    Details.LastName = theSqlDataReader["LastName"].ToString();
                    Details.IDNumber = theSqlDataReader["IDNumber"].ToString();
                    Details.RegistrationRenewalDate = theSqlDataReader["RegistrationRenewalDate"].ToString();
                    Details.Notice = theSqlDataReader["Notice"].ToString();
                    Details.NonLoggedCOCs = theSqlDataReader["NonLoggedCOCs"].ToString();
                    Details.NonLoggedCOCsAllocated = theSqlDataReader["NonLoggedCOCsAllocated"].ToString();
                    Details.NumberCOCsLogged = theSqlDataReader["NumberCOCsLogged"].ToString();
                    Details.NumberCOCsAudited = theSqlDataReader["NumberCOCsAudited"].ToString();
                    Details.RefixNotices = theSqlDataReader["RefixNotices"].ToString();
                    Details.AddressStreet = theSqlDataReader["AddressStreet"].ToString();
                    Details.AddressSuburb = theSqlDataReader["AddressSuburb"].ToString();
                    Details.AddressCity = theSqlDataReader["AddressCity"].ToString();
                    Details.AddressProvince = theSqlDataReader["AddressProvince"].ToString();
                    Details.AddressAreaCode = theSqlDataReader["AddressAreaCode"].ToString();
                    Details.PostalAddress = theSqlDataReader["PostalAddress"].ToString();
                    Details.PostalCity = theSqlDataReader["PostalCity"].ToString();
                    Details.PostalCode = theSqlDataReader["PostalCode"].ToString();
                    Details.HomeNo = theSqlDataReader["HomeNo"].ToString();
                    Details.MobileNo = theSqlDataReader["MobileNo"].ToString();
                    Details.EmploymentDate = theSqlDataReader["EmploymentDate"].ToString();
                    Details.Email = theSqlDataReader["Email"].ToString();
                    Details.InsuranceCompany = theSqlDataReader["InsuranceCompany"].ToString();
                    Details.PolicyHolder = theSqlDataReader["PolicyHolder"].ToString();
                    Details.PolicyNumber = theSqlDataReader["PolicyNumber"].ToString();
                    Details.PeriodOfInsuranceFrom = theSqlDataReader["PeriodOfInsuranceFrom"].ToString();
                    Details.PeriodOfInsuranceTo = theSqlDataReader["PeriodOfInsuranceTo"].ToString();
                    list.Add(Details);
                }
                json = JsonConvert.SerializeObject(list);
            }
            else
            {
                // Display any errors
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            DLdb.DB_Close();

            Response.ContentType = "application/json";
            Response.Write(json.ToString());
            Response.End();
        }
    }
}