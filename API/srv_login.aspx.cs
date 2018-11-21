using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace InspectIT.srvAPI
{
    public partial class srv_login : System.Web.UI.Page
    {
        public class UserDetails
        {
            public string UserID { get; set; }
            public string AuditorID { get; set; }
            public string Username { get; set; }
            public string Role { get; set; }
            public string ErrorMsg { get; set; }
            public string Email { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string json = "";
            var Page = new UserDetails();
            // SET THE SCORES FROM THE QUESTIONS FORM.
            if (Request.QueryString["uname"] != null && Request.QueryString["pass"] != null)
            {
                
                string Uname = Request.QueryString["uname"].ToString();
                string Pass = Request.QueryString["pass"].ToString();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from Users where email = @username and password = @password and isActive = '1'";
                DLdb.SQLST.Parameters.AddWithValue("username", Uname.ToString());
                DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(Pass.ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    Page.UserID = theSqlDataReader["UserID"].ToString();
                    Page.Role = theSqlDataReader["Role"].ToString();
                    Page.Email = theSqlDataReader["email"].ToString();
                    Page.Username = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                    
                    //Response.Write(theSqlDataReader["UserID"].ToString());
                }
                else
                {
                    Page.ErrorMsg = "False";
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

            }
            else
            {
                Page.ErrorMsg = "Error";
                //Response.Write("Error");
            }
            
            json = JsonConvert.SerializeObject(Page);

            Response.ContentType = "application/json";
            Response.Write(json);

            Response.End();
            DLdb.DB_Close();

        }
    }
}