using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Web.Services;
using System.Web.Script.Services;

namespace InspectIT
{
    public partial class getFormData : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
                        
            //Global DLdb = new Global();
            DLdb.DB_Connect();
            
            string json = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormUserData where DataID = '" + Request.QueryString["did"] + "'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            DataTable schemaTable = theSqlDataReader.GetSchemaTable();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                json = theSqlDataReader["json"].ToString();//.Replace("[", "").Replace("]", "");

            }
            else
            {
                json = "{}";
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.ContentType = "application/json";
            Response.Write(json.Replace("[","").Replace("]",""));
            Response.End();
        }
    }
}