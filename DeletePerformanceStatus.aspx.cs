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
    public partial class DeletePerformanceStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (Request.QueryString["op"].ToString() == "del")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update PerformanceStatus set isActive='0' WHERE PerformanceStatusID=@PerformanceStatusID";
                DLdb.SQLST.Parameters.AddWithValue("PerformanceStatusID", Request.QueryString["id"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                Response.Redirect("EditOrDeleteUser.aspx?UserID="+ Request.QueryString["uid"].ToString() + "&msg=" + DLdb.Encrypt("Performance Status has been archived"));
            }
            else if (Request.QueryString["op"].ToString() == "undel")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update performancetypes set isActive='1' WHERE PerformanceID=@PerformanceID";
                DLdb.SQLST.Parameters.AddWithValue("PerformanceID", Request.QueryString["id"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                Response.Redirect("EditOrDeleteUser.aspx?UserID=" + Request.QueryString["uid"].ToString() + "&msg=" + DLdb.Encrypt("Performance Status has been added"));
            }
            else if (Request.QueryString["op"].ToString() == "delcomp")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update performancetypes set isActive='0' WHERE PerformanceID=@PerformanceID";
                DLdb.SQLST.Parameters.AddWithValue("PerformanceID", Request.QueryString["id"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                Response.Redirect("companiesEdit.aspx?id=" + Request.QueryString["uid"].ToString() + "&msg=" + DLdb.Encrypt("Performance Status has been added"));
            }
            else if (Request.QueryString["op"].ToString() == "undelcomp")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update performancetypes set isActive='1' WHERE PerformanceID=@PerformanceID";
                DLdb.SQLST.Parameters.AddWithValue("PerformanceID", Request.QueryString["id"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                Response.Redirect("EditOrDeleteUser.aspx?id=" + Request.QueryString["uid"].ToString() + "&msg=" + DLdb.Encrypt("Performance Status has been added"));
            }

            DLdb.DB_Close();
        }
        
    }
}