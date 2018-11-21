using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace InspectIT
{
    public partial class VerifyPurchaseSupplier : System.Web.UI.Page
    {
        public string UserID = "";
        public string Price = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }
            
            //if (Request.QueryString["msg"] != null)
            //{
            //    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
            //    successmsg.InnerHtml = msg;
            //    successmsg.Visible = true;
            //}

            //if (Request.QueryString["err"] != null)
            //{
            //    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
            //    errormsg.InnerHtml = msg;
            //    errormsg.Visible = true;
            //}

            if (!IsPostBack)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from Orders where OrderID = @OrderID";
                DLdb.SQLST.Parameters.AddWithValue("OrderID", DLdb.Decrypt(Request.QueryString["oid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        UserID = theSqlDataReader["UserID"].ToString();
                        Price = theSqlDataReader["Total"].ToString();
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "Select * from users where UserID = @UserID";
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                NameofPlumber.InnerHtml = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                                NumberTo.InnerHtml = theSqlDataReader2["contact"].ToString();
                                btn_buy.Enabled = true;
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
            }
        }

        protected void resendOTP_Click(object sender, EventArgs e)
        {
            if(NumberTo.InnerHtml.ToString() != "")
            {
                Global DLdb = new Global();
                string OTPCode = Session["IIT_OTPCodePlumber"].ToString();
                DLdb.sendSMS("11", NumberTo.InnerHtml, "Inspect-It: OTP Resend, please use OTP Code: " + OTPCode);
            }
        }

        protected void btn_buy_Click(object sender, EventArgs e)
        {
            if (OTPCode.Text != "")
            {
                if (Session["IIT_OTPCodePlumber"].ToString() == OTPCode.Text.ToString())
                {
                    Global DLdb = new Global();
                    DLdb.DB_Connect();
                    
                    string Order = DLdb.Decrypt(Request.QueryString["oid"].ToString());

                    // SEND TO PAYMENT GATEWAY
                    string uid = "";
                    string amount = Request.QueryString["cost"].ToString();
                    string orderId = Order.ToString();
                    string name = "";
                    string description = "";
                    string site = "";
                    string merchant_id = "10140624";
                    string merchant_key = "mbmxqajjukwkb";
                    string ReturnURL = "https://197.242.82.242/inspectit/PF_returnURL";
                    string CancelURL = "https://197.242.82.242/inspectit/PF_cancelURL";
                    string NotifyURL = "https://197.242.82.242/inspectit/PF_notifyURL";
                    
                    StringBuilder str = new StringBuilder();

                    uid = UserID;
                    name = "Inspect IT: " + orderId;
                    description = "Purchased Paper COC";

                    // Check if we are using the test or live system
                    string paymentMode = "test";

                    if (paymentMode == "test")
                    {
                        site = "https://sandbox.payfast.co.za/eng/process?";
                        merchant_id = "10009492";
                        merchant_key = "b28g8dznu91dl";
                    }
                    else if (paymentMode == "live")
                    {
                        site = "https://www.payfast.co.za/eng/process?";
                        merchant_id = "10140624";
                        merchant_key = "mbmxqajjukwkb";
                    }

                    str.Append("merchant_id=" + merchant_id);
                    str.Append("&merchant_key=" + merchant_key);
                    str.Append("&return_url=" + ReturnURL);
                    str.Append("&cancel_url=" + CancelURL);
                    str.Append("&notify_url=" + NotifyURL);

                    // SEND ORDER DETAILS

                    str.Append("&m_payment_id=" + HttpUtility.UrlEncode(orderId));
                    str.Append("&amount=" + HttpUtility.UrlEncode(amount));
                    str.Append("&item_name=" + HttpUtility.UrlEncode(name));
                    str.Append("&item_description=" + HttpUtility.UrlEncode(description));

                    Response.Redirect(site + str.ToString());

                    //Response.Redirect("PF_notifyURLtest.aspx?oid=" + orderId);
                    
                    DLdb.DB_Close();
                }
                else
                {
                    errormsg.InnerHtml = "Invalid OTP Code";
                    errormsg.Visible = true;
                }
            }
            else
            {
                errormsg.InnerHtml = "Please enter OTP Code";
                errormsg.Visible = true;
            }
            

        }
    }
}