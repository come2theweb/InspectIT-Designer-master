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

namespace InspectIT.API
{
    public partial class srv_getSuburbs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();

            string dropdown = "";
            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Area order by Name";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    dropdown += "<option value=\""+theSqlDataReader["Name"].ToString()+ "\">" + theSqlDataReader["Name"].ToString() + "</option>";
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Write(dropdown);
            Response.End();

        }
    }
}