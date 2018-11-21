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

namespace InspectIT
{
    public partial class RequestNewIDCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();
            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            // ADMIN CHECK
            if (Session["IIT_Role"].ToString() != "Administrator")
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

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();

            DLdb.SQLST.CommandText = "SELECT * FROM IDCards";
            //DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.SQLST.Parameters.AddWithValue("CostOfNewCard", CostOfNewCard.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("DeliveryCost", DeliveryCost.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("VAT", VAT.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("TotalDue", TotalDue.ToString());
                    
                }
            }
            else
            {
                // Display any errors
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            DLdb.DB_Close();

        }

        int collectCard = 0;

        protected void btn_buy_click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO IDCards (CollectIDCard,RegisteredPostIDCard, CourierIDCard,DisclaimerAgreementIDCard) VALUES (@CollectIDCard, @RegisteredPostIDCard, @CourierIDCard,@DisclaimerAgreementIDCard)";

            // General Details
            //DLdb.SQLST.Parameters.AddWithValue("IDCardFront", IDCardFront.Filename);
            //DLdb.SQLST.Parameters.AddWithValue("IDCardBack", IDCardBack.Filename);
            DLdb.SQLST.Parameters.AddWithValue("CollectIDCard", this.CollectIDCard.Checked ? "1" : "0");
            DLdb.SQLST.Parameters.AddWithValue("RegisteredPostIDCard", this.RegisteredPostIDCard.Checked ? "1" : "0");
            DLdb.SQLST.Parameters.AddWithValue("CourierIDCard", this.CourierIDCard.Checked ? "1" : "0");
            DLdb.SQLST.Parameters.AddWithValue("DisclaimerAgreementIDCard", this.DisclaimerAgreementIDCard.Checked ? "1" : "0");

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
            Response.Redirect("");
        }

        }
}