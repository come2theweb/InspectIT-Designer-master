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
    public partial class PerformanceTypesEdit : System.Web.UI.Page
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
                btnUpdate.Visible = false;
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {

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
            
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM performancetypes where PerformanceID=@PerformanceID";
                    DLdb.SQLST.Parameters.AddWithValue("PerformanceID", DLdb.Decrypt(Request.QueryString["id"]));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            performanceType.Text = theSqlDataReader["Type"].ToString();
                            pointAllocation.Text = theSqlDataReader["Points"].ToString();
                            if (theSqlDataReader["isCompany"].ToString() == "True")
                            {
                                CheckBox1.Checked = true;
                            }
                        }
                    }
               
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                
                    DLdb.DB_Close();
                    btnSave.Visible = false;
                }
                else
                {
                    btnUpdate.Visible = false;

                }
            }
        }
        protected void btn_update_Click1(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update performancetypes set Type=@Type,Points=@Points,isCompany=@isCompany where PerformanceID=@PerformanceID";
            DLdb.SQLST.Parameters.AddWithValue("PerformanceID", DLdb.Decrypt(Request.QueryString["id"]));
            DLdb.SQLST.Parameters.AddWithValue("Type", performanceType.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Points", pointAllocation.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("isCompany", CheckBox1.Checked ? 1:0);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("PerformanceTypes");
        }

        protected void btnsave_Click1(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into performancetypes (Type,Points,isCompany) values (@Type,@Points,@isCompany)";
            DLdb.SQLST.Parameters.AddWithValue("Type", performanceType.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Points", pointAllocation.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("isCompany", CheckBox1.Checked ? 1 : 0);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("PerformanceTypes");
        }
    }
}