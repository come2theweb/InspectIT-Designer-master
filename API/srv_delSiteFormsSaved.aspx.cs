using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;

namespace InspectIT.srvAPI
{
    public partial class srv_delSiteFormsSaved : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string result = "true";
            // SET THE SCORES FROM THE QUESTIONS FORM
            if (Request.Form["key"] != null && Request.Form["did"] != null)
            {
                Global DLdb = new Global();
                DLdb.DB_Connect();

                string did = Request.Form["did"].ToString();

                // LOAD FORMS
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "delete from FormUserData where DataID = @DataID";
                DLdb.SQLST.Parameters.AddWithValue("DataID", did);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                                
                DLdb.DB_Close();

            }
            else
            {
                result = "error";
            }

            Response.Write(result);
            Response.End();
        }
    }
}