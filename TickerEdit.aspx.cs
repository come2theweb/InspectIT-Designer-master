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
    public partial class TickerEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

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

            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                btnUpdatePlumber.Visible = false;
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {

            }

            if (Request.QueryString["msg"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
                successmsg.InnerHtml = msg;
                successmsg.Visible = true;
            }

            if (Request.QueryString["err"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
                errormsg.InnerHtml = msg;
                errormsg.Visible = true;
            }


            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM Ticker where TickerID=@TickerID";
                    DLdb.SQLST.Parameters.AddWithValue("TickerID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        Notice.Text = theSqlDataReader["Notice"].ToString();
                        StartDate.Text = theSqlDataReader["StartDate"].ToString();
                        EndDate.Text = theSqlDataReader["EndDate"].ToString();
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                    btnUpdatePlumber.Visible = true;
                    Button1.Visible = false;
                }
                else
                {
                    Button1.Visible = true;
                    btnUpdatePlumber.Visible = false;
                }
                

                DLdb.DB_Close();
            }
        }

        protected void btnUpdatePlumber_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "UPDATE Ticker SET Notice=@Notice,StartDate=@StartDate,EndDate=@EndDate WHERE TickerID=@TickerID";
            DLdb.SQLST.Parameters.AddWithValue("TickerID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("Notice", Notice.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("StartDate", StartDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("EndDate", EndDate.Text.ToString());
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
            Response.Redirect("GlobalSettings.aspx?msg=" + DLdb.Encrypt("Notice has been updated"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "insert into Ticker (Notice,StartDate,EndDate) values (@Notice,@StartDate,@EndDate)";
            DLdb.SQLST.Parameters.AddWithValue("Notice", Notice.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("StartDate", StartDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("EndDate", EndDate.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("GlobalSettings.aspx?msg=" + DLdb.Encrypt("Notice has been added"));
        }
    }
}