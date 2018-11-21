using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InspectIT.API
{
    public partial class srv_AddComment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string refixNoticeId = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCRefixes where COCStatementID=@COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", Request.QueryString["cocid"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    refixNoticeId = theSqlDataReader["RefixNoticeID"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into COCRefixesComments (RefixNoticeID,COCStatementID,UserID,Comments) values (@RefixNoticeID,@COCStatementID,@UserID,@Comments)";
            DLdb.SQLST.Parameters.AddWithValue("RefixNoticeID", refixNoticeId);
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", Request.QueryString["cocid"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", Request.QueryString["uid"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comments", Request.QueryString["comm"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }
    }
}