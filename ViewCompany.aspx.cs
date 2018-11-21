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
    public partial class ViewCompany : System.Web.UI.Page
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
            displayCompanies.InnerHtml = "";

           // Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "SELECT * FROM Companies";
            // DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    displayCompanies.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["CompanyName"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["CompanyRegNo"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["CompanyEmail"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["CompanyContactNo"].ToString() + "</td>" +
                                                       "<td><a href=\"EditOrDeleteCompany.aspx?CompanyID=" + theSqlDataReader["CompanyID"].ToString() + "\"><input type=\"button\" value=\"Edit\" class=\"btn btn-primary\"/></a><a href=\"DeleteCompany.aspx?CompanyID=" + theSqlDataReader["CompanyID"].ToString() + "\"><input type=\"button\" value=\"Delete\" class=\"btn btn-danger\"/></a></td>" +
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