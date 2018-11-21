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

namespace InspectIT
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            // ADMIN CHECK
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO Users (fname,rights,lname, password, role, email, isActive, NoCOCpurchases) VALUES (@fname,@rights,@lname, @password, @role, @email, @isActive, '10')";
            DLdb.SQLST.Parameters.AddWithValue("fname", firstname.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("lname", Surname.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(passwordnew.Text.ToString())); 
            DLdb.SQLST.Parameters.AddWithValue("email", email.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("role", role.SelectedValue);
            DLdb.SQLST.Parameters.AddWithValue("isActive", this.isActive.Checked ? "1" : "0");
            DLdb.SQLST.Parameters.AddWithValue("rights", rights.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            string HTMLSubject = "Welcome to Inspect IT.";
            string HTMLBody = "Dear " + firstname.Text.ToString() + "<br /><br />Welcome to Inspect IT<br /><br />Your login details are;<br />Email Address: " + email.Text.ToString() + "<br />Password: " + passwordnew.ToString() + "<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Team";
            DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", email.Text.ToString(), "");
            
            Response.Redirect("ViewUser.aspx?msg=" + DLdb.Encrypt("User has been added successfully"));
        }
    }
}