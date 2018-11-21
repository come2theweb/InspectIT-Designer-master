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
    public partial class ViewPlumbers : System.Web.UI.Page
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
            

            //displayusers.InnerHtml = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT top 1 * FROM Users where Role = 'Staff' and isActive='1'";
            // DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string designation = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from PlumberDesignations where PlumberID=@PlumberID";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", theSqlDataReader["PIRBID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        designation += theSqlDataReader2["Designation"].ToString();
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    //displayusers.InnerHtml += "<tr>" +
                    //                                   "<td>" + theSqlDataReader["Regno"].ToString() + "</td>" +
                    //                                   "<td>" + theSqlDataReader["fname"].ToString() + "</td>" +
                    //                                   "<td>" + theSqlDataReader["lname"].ToString() + "</td>" +
                    //                                   "<td>" + designation + "</td>" +
                    //                                   "<td>" + theSqlDataReader["email"].ToString() + "</td>" +
                    //                                   "<td>" + DLdb.Decrypt(theSqlDataReader["password"].ToString()) + "</td>" +
                    //                                   "<td>" + theSqlDataReader["isActive"].ToString() + "</td>" +
                    //                                   "<td><a href=\"EditOrDeleteUser.aspx?UserID=" + theSqlDataReader["UserID"].ToString() + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a></td>" +
                    //                                   //"<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeleteUser.aspx?UserID=" + theSqlDataReader["UserID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div>" +
                    //                               "</tr>";
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

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            //string text = TextBox1.Text.ToString();

            DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "SELECT top 10 * FROM Users where (fname like '%"+ text + "%') or (lname like '%" + text + "%') or (email like '%" + text + "%') or (regno like '%" + text + "%')";
            //DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string designation = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from PlumberDesignations where PlumberID=@PlumberID";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", theSqlDataReader["PIRBID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        designation += theSqlDataReader2["Designation"].ToString();
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    //displayusers.InnerHtml += "<tr>" +
                    //                                   "<td>" + theSqlDataReader["Regno"].ToString() + "</td>" +
                    //                                   "<td>" + theSqlDataReader["fname"].ToString() + "</td>" +
                    //                                   "<td>" + theSqlDataReader["lname"].ToString() + "</td>" +
                    //                                   "<td>" + designation + "</td>" +
                    //                                   "<td>" + theSqlDataReader["email"].ToString() + "</td>" +
                    //                                   "<td>" + DLdb.Decrypt(theSqlDataReader["password"].ToString()) + "</td>" +
                    //                                   "<td>" + theSqlDataReader["isActive"].ToString() + "</td>" +
                    //                                   "<td><a href=\"EditOrDeleteUser.aspx?UserID=" + theSqlDataReader["UserID"].ToString() + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a></td>" +
                    //                               //"<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeleteUser.aspx?UserID=" + theSqlDataReader["UserID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div>" +
                    //                               "</tr>";
                }
            }
            else
            {
                // Display any errors
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

        }
    }
}