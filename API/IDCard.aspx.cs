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
    public partial class IDCard : System.Web.UI.Page
    {
        public class RegDetails
        {
            //public string IDCardFront { get; set; }
            //public string IDCardBack { get; set; }
            public string DisclaimerAgreementIDCard { get; set; }
            public string CostOfNewCard { get; set; }
            public string DeliveryCost { get; set; }
            public string VAT { get; set; }
            public string TotalDue { get; set; }
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

            DLdb.SQLST.CommandText = "SELECT * FROM IDCards";
            DLdb.SQLST.Parameters.AddWithValue("NewCardID", Request.QueryString["NewCardID"].ToString());
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
                    Details.DisclaimerAgreementIDCard = theSqlDataReader["DisclaimerAgreementIDCard"].ToString();
                    Details.CostOfNewCard = theSqlDataReader["CostOfNewCard"].ToString();
                    Details.DeliveryCost = theSqlDataReader["DeliveryCost"].ToString();
                    Details.VAT = theSqlDataReader["VAT"].ToString();
                    Details.TotalDue = theSqlDataReader["TotalDue"].ToString();
                   
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