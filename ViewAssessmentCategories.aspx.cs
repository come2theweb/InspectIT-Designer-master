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
    public partial class ViewAssessmentCategories : System.Web.UI.Page
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
            
            displayusers.InnerHtml = "";

            DLdb.DB_Connect();
            if (!IsPostBack)
            {
                DLdb.RS.Open();

                DLdb.SQLST.CommandText = "SELECT * FROM Categories where isActive='1'";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        displayusers.InnerHtml += "<tr>" +
                                                           "<td>" + theSqlDataReader["CategoryID"].ToString() + "</td>" +
                                                           "<td>" + theSqlDataReader["CategoryName"].ToString() + "</td>" +
                                                           "<td><a href=\"#\" onclick=\"editrole('" + theSqlDataReader["CategoryID"].ToString() + "','" + theSqlDataReader["CategoryName"].ToString() + "')\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                           "<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeleteCategory.aspx?cid=" + theSqlDataReader["CategoryID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div>" +
                                                           "<a href='ViewAssessmentSubCategories.aspx?cid="+ theSqlDataReader["CategoryID"].ToString() + "'><div class=\"btn btn-sm btn-success\"><i class=\"fa fa-eye\"></i></div></td>" +
                                                       "</tr>";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();
            }
            
            DLdb.DB_Close();
        }

        protected void addCategory_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "INSERT INTO Categories (CategoryName) values (@CategoryName)";
            DLdb.SQLST2.Parameters.AddWithValue("CategoryName", CategoryName.Text.ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            DLdb.DB_Close();

            Response.Redirect("ViewAssessmentCategories.aspx?msg=" + DLdb.Encrypt("Category Added"));
        }

        protected void editCategory_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "update Categories set CategoryName=@CategoryName where CategoryID=@CategoryID";
            DLdb.SQLST2.Parameters.AddWithValue("CategoryName", EditCategoryName.Text.ToString());
            DLdb.SQLST2.Parameters.AddWithValue("CategoryID", EditCategoryID.Text.ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            DLdb.DB_Close();

            Response.Redirect("ViewAssessmentCategories.aspx?msg=" + DLdb.Encrypt("Category Updated"));
        }
        
    }
}