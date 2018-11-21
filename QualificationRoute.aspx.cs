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
    public partial class QualificationRoute : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string delBtnClass = "";

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }
            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                addBtn.Visible = false;
                delBtnClass = "hide";
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {
                addBtn.Visible = true;
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
            displayusers_del.InnerHtml = "";
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM qualificationroute where isActive='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    displayusers.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["Route"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["isActive"].ToString() + "</td>" +
                                                       "<td><a href='QualificationRouteEdit?id=" + theSqlDataReader["QualificationID"].ToString() + "'><div class=\"btn btn-sm btn-primary\" title=\"Edit\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                       "<div class=\"btn " + delBtnClass + " btn-sm btn-danger\" onclick=\"deleteconf('DeleteQualificationRoute.aspx?op=del&id=" + theSqlDataReader["QualificationID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                   "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM qualificationroute where isActive='0'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    displayusers_del.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["Route"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["isActive"].ToString() + "</td>" +
                                                       "<td><a href='QualificationRouteEdit?id=" + theSqlDataReader["QualificationID"].ToString() + "'><div class=\"btn btn-sm btn-primary\" title=\"Edit\"><i class=\"fa fa-pencil\"></i></div></a>" +

                                                       "<div class=\"btn " + delBtnClass + " btn-sm btn-success\" onclick=\"deleteconf('DeleteQualificationRoute.aspx?op=undel&id=" + theSqlDataReader["QualificationID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                   "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("QualificationRouteEdit");
        }
    }
}