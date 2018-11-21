using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;

namespace InspectIT
{
    public partial class zImportPlumbersPhotos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_PIRB_Connect();

            // CHECK IF EXISTS
            DLdb.DB_Connect();
                        
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "SELECT * FROM users where password > '' AND not password like '%[^0-9]%' and [Role] = 'Staff'";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "update users set password = @password where UserID = @UserID";
                    DLdb.SQLST3.Parameters.AddWithValue("password", DLdb.Encrypt(theSqlDataReader2["password"].ToString()));
                    DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader2["UserID"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();
                    

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();
                    
                }                        
            }
                    
            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();
                    

            DLdb.DB_Close();
        }
    }
}