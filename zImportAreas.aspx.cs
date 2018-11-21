using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace InspectIT
{
    public partial class zImportAreas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string areaID = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into area (Name,Code,ProvinceID) select area,boxcode,'9' from  [dbo].[PCODES$] where province='Western Cape'";
            //DLdb.SQLST.Parameters.AddWithValue("Email", email.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "WITH CTE AS( SELECT [Name], RN = ROW_NUMBER()OVER(PARTITION BY [Name] ORDER BY [Name]) FROM area ) DELETE FROM CTE WHERE RN > 1";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Area where provinceid='9'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from [dbo].[PCODES$] where Area=@Area";
                    DLdb.SQLST2.Parameters.AddWithValue("Area", theSqlDataReader["Name"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "insert into AreaSuburbs (CityID,Name,ProvinceID) values (@CityID,@Name,@ProvinceID)";
                            DLdb.SQLST3.Parameters.AddWithValue("CityID", theSqlDataReader["ID"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("Name", theSqlDataReader2["Suburb"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("ProvinceID", theSqlDataReader["ProvinceID"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();
                            
                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();
                        }
                    }
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();
        }
    }
}