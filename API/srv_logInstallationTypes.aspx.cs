using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace InspectIT.API
{
    public partial class srv_logInstallationTypes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string COCNumber = Request.QueryString["cocnumber"].ToString();
            string ischecked = Request.QueryString["chk"].ToString();
            string TypeID = Request.QueryString["type"].ToString();

            if (ischecked=="true")
            {
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "select * from COCInstallations where isActive='1' and COCStatementID=@COCStatementID and TypeID=@TypeID";
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", COCNumber);
                DLdb.SQLST2.Parameters.AddWithValue("TypeID", TypeID);
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.HasRows)
                {
                    while (theSqlDataReader2.Read())
                    {

                    }
                }
                else
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into COCInstallations (COCStatementID,TypeID) values(@COCStatementID,@TypeID)";
                    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", COCNumber);
                    DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeID);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            else if(ischecked == "false")
            {
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "select * from COCInstallations where COCStatementID=@COCStatementID and TypeID=@TypeID";
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", COCNumber);
                DLdb.SQLST2.Parameters.AddWithValue("TypeID", TypeID);
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.HasRows)
                {
                    while (theSqlDataReader2.Read())
                    {
                        DLdb.RS.Open();
                        DLdb.SQLST.CommandText = "update COCInstallations set Isactive='0' where COCStatementID=@COCStatementID and TypeID=@TypeID";
                        DLdb.SQLST.Parameters.AddWithValue("COCStatementID", COCNumber);
                        DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeID);
                        DLdb.SQLST.CommandType = CommandType.Text;
                        DLdb.SQLST.Connection = DLdb.RS;
                        SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                        if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.RS.Close();
                    }
                }
                else
                {

                }

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }

            
            

            DLdb.DB_Close();
        }
    }
}