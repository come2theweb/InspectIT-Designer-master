using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InspectIT.API
{
    public partial class sendOTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string smsNumber = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from users where userid=@userid";
            DLdb.SQLST.Parameters.AddWithValue("userid", DLdb.Decrypt(Request.QueryString["uid"].ToString()));
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
            Session["IIT_OTPCodeSubAudit"] = OTPCode;
            DLdb.sendSMS(Request.QueryString["uid"].ToString(), smsNumber.ToString(), "Inspect-It: You have requested to Log COC "+DLdb.Decrypt(Request.QueryString["cocid"].ToString()) +". OTP Code: "+ OTPCode + ".  Report any fraudulent activity to PIRB Immediately.");


            DLdb.DB_Close();
        }
    }
}