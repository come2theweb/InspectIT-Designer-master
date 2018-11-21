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
    public partial class srv_Username : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // SET THE SCORES FROM THE QUESTIONS FORM.
            if (Request.QueryString["uid"] != null)
            {
                Global DLdb = new Global();
                DLdb.DB_Connect();

                string UserID = Request.QueryString["uid"].ToString();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from Users where UserID = @UserID and isActive = '1'";
                DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    Response.Write(theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString());
                }
                else
                {
                    Response.Write("False");
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.DB_Close();
            }
            else
            {
                Response.Write("Error");
            }

            Response.End();

        }
    }
}