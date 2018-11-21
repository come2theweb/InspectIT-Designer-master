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
using System.Security.Cryptography;

namespace InspectIT
{
    public partial class installationTypesAdd : System.Web.UI.Page
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

            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                btn_add.Visible = false;
                btn_update.Visible = false;
                hideBtn.Visible = false;
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {

            }

            // ADMIN CHECK
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    btn_add.Visible = false;
                    btn_update.Visible = true;

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from InstallationTypes where InstallationTypeID=@InstallationTypeID";
                    DLdb.SQLST.Parameters.AddWithValue("InstallationTypeID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        InstallationType.Text = theSqlDataReader["InstallationType"].ToString();
                    }
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }
                else
                {
                    btn_add.Visible = true;
                    btn_update.Visible = false;
                }
            }
            
            DLdb.DB_Close();
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO InstallationTypes (InstallationType) VALUES (@InstallationType)";
            DLdb.SQLST.Parameters.AddWithValue("InstallationType", InstallationType.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            
            Response.Redirect("installationTypes.aspx?msg=" + DLdb.Encrypt("Installation Type has been added successfully"));
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update InstallationTypes set InstallationType=@InstallationType where InstallationTypeID=@InstallationTypeID";
            DLdb.SQLST.Parameters.AddWithValue("InstallationType", InstallationType.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("InstallationTypeID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("installationTypes.aspx?msg=" + DLdb.Encrypt("Installation Type has been updated successfully"));
        }
    }
}