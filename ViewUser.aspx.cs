﻿using System;
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
    public partial class ViewUser : System.Web.UI.Page
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

            string delBtnClass = "";

            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                addBtn.Visible = false;
                delBtnClass = "hide";
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {
                addBtn.Visible = true;
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
            displayusers.InnerHtml = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Users where (Role = 'Administrator' and isActive='1') or (role ='AdminRights' and isActive='1')";
            // DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    displayusers.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["fname"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["lname"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["role"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["email"].ToString() + "</td>" +
                                                       //"<td>" + theSqlDataReader["Regno"].ToString() + "</td>" +
                                                       "<td>" + DLdb.Decrypt(theSqlDataReader["password"].ToString()) + "</td>" +
                                                       "<td>" + theSqlDataReader["isActive"].ToString() + "</td>" +
                                                       "<td><a href=\"EditOrDeleteUserAdmin.aspx?UserID=" + DLdb.Encrypt(theSqlDataReader["UserID"].ToString()) + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                       "<div class=\"btn "+ delBtnClass + " btn-sm btn-danger\" onclick=\"deleteconf('DeleteUser.aspx?UserID=" + theSqlDataReader["UserID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
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

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Users where (Role = 'Administrator' and isActive='1') or (role ='AdminRights' and isActive='1')";
            // DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Tbody1.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["fname"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["lname"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["role"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["email"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["Regno"].ToString() + "</td>" +
                                                       "<td>" + DLdb.Decrypt(theSqlDataReader["password"].ToString()) + "</td>" +
                                                       "<td>" + theSqlDataReader["isActive"].ToString() + "</td>" +
                                                       "<td><a href=\"EditOrDeleteUserAdmin.aspx?UserID=" + DLdb.Encrypt(theSqlDataReader["UserID"].ToString()) + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                       "<div class=\"btn " + delBtnClass + " btn-sm btn-success\" onclick=\"deleteconfa('unDeleteUser.aspx?UserID=" + theSqlDataReader["UserID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
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
        }
    }
}