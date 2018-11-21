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
    public partial class Forgotpass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ResetPass_Click(object sender, EventArgs e)
        {
            // HIDE MESSAGES
            errorbox.Visible = false;
            successbox.Visible = false;

            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from users where emailaddress = @emailaddress and isactive = '1'";
            DLdb.SQLST.Parameters.AddWithValue("emailaddress", username.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                // EMAIL RESET LINK
                
                // SHOW SUCCESS MESSAGE
                successbox.InnerHtml = "We have sent you a reset password link to your email address. <a href='Default'>Click here</a> to login.";
                successbox.Visible = true;
                // CLEAR TEXTBOX
                username.Text = "";
            }
            else
            {
                // SHOW ERROR MESSAGE
                errorbox.InnerHtml = "Your email address was not found in the system.";
                errorbox.Visible = true;
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

        }
    }
}