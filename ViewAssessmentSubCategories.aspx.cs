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
    public partial class ViewAssessmentSubCategories : System.Web.UI.Page
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

                DLdb.SQLST.CommandText = "SELECT * FROM CategoriesSub where CategoryID=@CategoryID and isActive='1'";
                DLdb.SQLST.Parameters.AddWithValue("CategoryID", Request.QueryString["cid"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        displayusers.InnerHtml += "<tr>" +
                                                           "<td>" + theSqlDataReader["SubCategoryID"].ToString() + "</td>" +
                                                           "<td>" + theSqlDataReader["SubCategoryName"].ToString() + "</td>" +
                                                           "<td><a href=\"#\" onclick=\"editrole('" + theSqlDataReader["SubCategoryID"].ToString() + "','" + theSqlDataReader["SubCategoryName"].ToString() + "')\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                           "<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeleteSubCategory.aspx?scid=" + theSqlDataReader["SubCategoryID"].ToString() + "&cid=" + theSqlDataReader["CategoryID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                       "</tr>";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }

            
            DLdb.DB_Close();
        }
        

        protected void editSubCategory_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "update CategoriesSub set SubCategoryName=@SubCategoryName where SubCategoryID=@SubCategoryID";
            DLdb.SQLST2.Parameters.AddWithValue("SubCategoryName", EditSubCategoryName.Text.ToString());
            DLdb.SQLST2.Parameters.AddWithValue("SubCategoryID", EditSubCategoryID.Text.ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            DLdb.DB_Close();

            Response.Redirect("ViewAssessmentSubCategories.aspx?msg=" + DLdb.Encrypt("Sub Category Updated") + "&cid=" + Request.QueryString["cid"].ToString());
        }

        protected void addSubCategory_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "INSERT INTO CategoriesSub (CategoryID,SubCategoryName) values (@CategoryID,@SubCategoryName)";
            DLdb.SQLST2.Parameters.AddWithValue("CategoryID", Request.QueryString["cid"].ToString());
            DLdb.SQLST2.Parameters.AddWithValue("SubCategoryName", SubCategoryName.Text.ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            DLdb.DB_Close();
            Response.Redirect("ViewAssessmentSubCategories.aspx?msg=" + DLdb.Encrypt("Sub Category Added") + "&cid=" + Request.QueryString["cid"].ToString());
        }
    }
}