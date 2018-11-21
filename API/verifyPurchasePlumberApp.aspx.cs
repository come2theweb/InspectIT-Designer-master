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

namespace InspectIT.API
{
    public partial class verifyPurchasePlumberApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

            uid = Request.QueryString["uid"].ToString();
            name = "Inspect IT: " + orderId;
            description = "Purchased Electronic / Paper COC";

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

           

            DLdb.DB_Close();
        }
    }
}