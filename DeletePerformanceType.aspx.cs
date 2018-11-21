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
    public partial class DeletePerformanceType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (Request.QueryString["op"].ToString() == "del")
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
                Response.Redirect("PerformanceTypes.aspx?msg=" + DLdb.Encrypt("Performance Type has been deleted"));
            }
            else
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
                Response.Redirect("PerformanceTypes.aspx?msg=" + DLdb.Encrypt("Performance Type has been added"));
            }
           
            DLdb.DB_Close();
        }
        
    }
}