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
    public partial class installationTypes : System.Web.UI.Page
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
            
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }

            string delBtnClass = "";
            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                addITBtn.Visible = false;
                delBtnClass = "hide";
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {
                addITBtn.Visible = true;
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
            DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    

                    displayusers.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["InstallationType"].ToString() + "</td>" +
                                                       "<td><a href=\"installationTypesAdd.aspx?id=" + DLdb.Encrypt(theSqlDataReader["InstallationTypeID"].ToString()) + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                       "<div class=\"btn "+ delBtnClass + " btn-sm btn-danger\" onclick=\"deleteconf('DeleteItems.aspx?op=delInstallType&pid=0&id=" + theSqlDataReader["InstallationTypeID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                   "</tr>";
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive='0'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    
                    Tbody2.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["InstallationType"].ToString() + "</td>" +
                                                       "<td><a href=\"installationTypesAdd.aspx?id=" + DLdb.Encrypt(theSqlDataReader["InstallationTypeID"].ToString()) + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                       "<div class=\"btn " + delBtnClass + " btn-sm btn-danger\" onclick=\"deleteconf('DeleteItems.aspx?op=undelInstallType&pid=0&id=" + theSqlDataReader["InstallationTypeID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                   "</tr>";
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();
        }
    }
}