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
    public partial class ViewCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();

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

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();
            displayusers.InnerHtml = "";

           // Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "SELECT * FROM Customers";
            // DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    displayusers.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["CustomerName"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["CustomerSurname"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["CustomerCellNo"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["CustomerEmail"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["CustomerPassword"].ToString() + "</td>" +
                                                       "<td><a href=\"EditOrDeleteCustomer.aspx?CustomerID=" + theSqlDataReader["CustomerID"].ToString() + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                       "<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeleteCustomer.aspx?CustomerID=" + theSqlDataReader["CustomerID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                   "</tr>";
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
            // string htmlcontent = "";
            // Global DLdb = new Global();
            // DLdb.DB_Connect();
            // DLdb.RS.Open();

            // DLdb.SQLST.CommandText = "SELECT * FROM DBName";
            //// DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            // DLdb.SQLST.CommandType = CommandType.Text;
            // DLdb.SQLST.Connection = DLdb.RS;
            // SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            // if (theSqlDataReader.HasRows)
            // {
            //     while (theSqlDataReader.Read())
            //     {
            //         htmlcontent += "<tr>" +
            //                        "<td>" + theSqlDataReader["field1"].ToString() + "</td>" +
            //                        "<td>" + theSqlDataReader["field2"].ToString() + "</td>" +
            //                        "<td>" + theSqlDataReader["field3"].ToString() + "</td>" +
            //                        "<td>" + theSqlDataReader["field4"].ToString() + "</td>" +
            //                        "<td>" + theSqlDataReader["field5"].ToString() + "</td>" +
            //                        "<td><a href=\"EditOrDeleteUser.aspx\"><input type=\"button\" value=\"Edit\" class=\"btn btn-primary\"/></a></td>" +
            //                        "</tr>";
            //     }
            // }
            // else
            // {
            //     // Display any errors
            // }

            // if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            // DLdb.SQLST.Parameters.RemoveAt(0);
            // DLdb.RS.Close();
            // DLdb.DB_Close();
            // //displayData.InnerHtml = htmlcontent;
        }
    }
}