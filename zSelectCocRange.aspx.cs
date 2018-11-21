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
    public partial class zSelectCocRange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_IITOld_Connect();
            DLdb.DB_Connect();
            // *************************** COC's ****************************************************
            // *************************** COC's ****************************************************

            Response.Write("Starting Import COC's...<br />");
            
            // LOAD SUPPLIER INFORMATION
            DLdb.iitRS.Open();
            DLdb.iitSQLST.CommandText = "select top 10000  * from [dbo].[Certificate] order by datecreated desc";
            //DLdb.iitSQLST.Parameters.AddWithValue("ID", theSqlDataReader["ID"].ToString());
            DLdb.iitSQLST.CommandType = CommandType.Text;
            DLdb.iitSQLST.Connection = DLdb.iitRS;
            SqlDataReader theSqlDataReader = DLdb.iitSQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.iitRS2.Open();
                    DLdb.iitSQLST2.CommandText = "insert into tempCOCRange (COCNumber) values (@COCNumber)";
                    DLdb.iitSQLST2.Parameters.AddWithValue("COCNumber", theSqlDataReader["CertificateNo"].ToString());
                    DLdb.iitSQLST2.CommandType = CommandType.Text;
                    DLdb.iitSQLST2.Connection = DLdb.iitRS2;
                    SqlDataReader theSqlDataReader2 = DLdb.iitSQLST2.ExecuteReader();
                    
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.iitSQLST2.Parameters.RemoveAt(0);
                    DLdb.iitRS2.Close();
                    
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.iitSQLST.Parameters.RemoveAt(0);
            DLdb.iitRS.Close();

            DLdb.DB_IITOld_Close();


            Response.Write("Completed Import COC's...<br />");
        }
    }
}