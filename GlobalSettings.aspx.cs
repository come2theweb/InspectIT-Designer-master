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
    public partial class GlobalSettings : System.Web.UI.Page
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
            
            displayrates.InnerHtml = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Ticker where isActive='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    displayrates.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["Notice"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["StartDate"].ToString() + "</td>"+
                                                       "<td>" + theSqlDataReader["EndDate"].ToString() + "</td>" +
                                                       "<td><a href=\"TickerEdit.aspx?id=" + DLdb.Encrypt(theSqlDataReader["TickerID"].ToString()) + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                       //"<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeleteUser.aspx?UserID=" + theSqlDataReader["UserID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                   "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM GlobalSettings where GlobalID='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    one.Text = theSqlDataReader["one"].ToString();
                    two.Text = theSqlDataReader["two"].ToString();
                    three.Text = theSqlDataReader["three"].ToString();
                    four.Text = theSqlDataReader["four"].ToString();
                    five.Text = theSqlDataReader["five"].ToString();
                    six.Text = theSqlDataReader["six"].ToString();
                    seven.Text = theSqlDataReader["seven"].ToString();
                    eight.Text = theSqlDataReader["eight"].ToString();
                    nine.Text = theSqlDataReader["nine"].ToString();
                    ten.Text = theSqlDataReader["ten"].ToString();
                    eleven.Text = theSqlDataReader["eleven"].ToString();
                    twelve.Text = theSqlDataReader["twelve"].ToString();
                    thirteen.Text = theSqlDataReader["thirteen"].ToString();
                    fourteen.Text = theSqlDataReader["fourteen"].ToString();
                    fifteen.Text = theSqlDataReader["fifteen"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update GlobalSettings set one=@one,two=@two,three=@three,four=@four,five=@five,six=@six,seven=@seven,eight=@eight,nine=@nine,ten=@ten,eleven=@eleven,twelve=@twelve,thirteen=@thirteen,fourteen=@fourteen,fifteen=@fifteen where GlobalID='1'";
            DLdb.SQLST.Parameters.AddWithValue("one", one.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("two", two.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("three", three.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("four", four.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("five", five.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("six", six.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("seven", seven.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("eight", eight.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("nine", nine.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ten", ten.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("eleven", eleven.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("twelve", twelve.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("thirteen", thirteen.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("fourteen", fourteen.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("fifteen", fifteen.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            DLdb.DB_Close();
        }
    }
}