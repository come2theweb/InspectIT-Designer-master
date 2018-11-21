using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InspectIT
{
    public partial class zBgPerformanceActivity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from PerformanceStatus where hasEndDate='1' and isactive='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DateTime enddate = Convert.ToDateTime(theSqlDataReader["endDate"].ToString());
                    DateTime now = DateTime.Now;

                    if (now > enddate)
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "update PerformanceStatus set isActive='0' where PerformanceStatusID=@PerformanceStatusID";
                        DLdb.SQLST2.Parameters.AddWithValue("PerformanceStatusID", theSqlDataReader["PerformanceStatusID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                    }
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();
        }
    }
}