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

namespace InspectIT
{
    public partial class EditOrDeleteUserAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                btnUpdatePlumber.Visible = false;
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {

            }

            // ADMIN CHECK
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");

            }

            //if (Request.QueryString["msg"] != null)
            //{
            //    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
            //    successmsg.InnerHtml = msg;
            //    successmsg.Visible = true;
            //}

            //if (Request.QueryString["err"] != null)
            //{
            //    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
            //    errormsg.InnerHtml = msg;
            //    errormsg.Visible = true;
            //}

            if (!IsPostBack)
            {

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["UserID"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    Name.Text = theSqlDataReader["fname"].ToString();
                    Surname.Text = theSqlDataReader["lname"].ToString();
                    Email.Text = theSqlDataReader["email"].ToString();
                    Password.Text = DLdb.Decrypt(theSqlDataReader["Password"].ToString());
                    PasswordConfirm.Text = DLdb.Decrypt(theSqlDataReader["Password"].ToString());
                    role.SelectedValue = theSqlDataReader["role"].ToString();
                    rights.SelectedValue = theSqlDataReader["rights"].ToString();
                    if (theSqlDataReader["isActive"].ToString() == "True")
                    {
                        isActive.Checked = true;
                    }
                    else
                    {
                        isActive.Checked = false;
                    }

                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                foreach (ListItem checkedItem in CheckBoxList1.Items)
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM UserRights where UserID=@UserID and Menu=@Menu and isActive='1'";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["UserID"].ToString()));
                    DLdb.SQLST.Parameters.AddWithValue("Menu", checkedItem.Value.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            checkedItem.Selected = true;
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }

                DLdb.DB_Close();
            }
        }

        protected void btnUpdatePlumber_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (rights.SelectedValue == "")
            {
                errormsg.Visible = true;
                errormsg.InnerHtml = "Please select the appropriate rights";
            }
            else
            {
                if (PasswordConfirm.Text.ToString() == "")
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "UPDATE Users SET fname = @Name,rights=@rights,lname = @Surname,email = @email,role=@role,isActive=@isActive WHERE UserID=@UserID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["UserID"].ToString()));
                    DLdb.SQLST.Parameters.AddWithValue("Name", Name.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Surname", Surname.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("email", Email.Text.ToString());
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
                }
                else
                {
                    if (Password.Text.ToString() == PasswordConfirm.Text.ToString() || Password.Text.ToString() != "")
                    {
                        DLdb.RS.Open();
                        DLdb.SQLST.CommandText = "UPDATE Users SET fName = @Name,rights=@rights,lname = @Surname,email = @email,password = @password,role=@role,isActive=@isActive WHERE UserID=@UserID";
                        DLdb.SQLST.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["UserID"].ToString()));
                        DLdb.SQLST.Parameters.AddWithValue("Name", Name.Text.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("Surname", Surname.Text.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("email", Email.Text.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(Password.Text.ToString()));
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
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.RS.Close();

                    }
                    else
                    {
                        errormsg.InnerHtml = "Your password does not match";
                        errormsg.Visible = true;
                    }
                }

                foreach (ListItem checkedItem in CheckBoxList1.Items)
                {
                    if (checkedItem.Selected)
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "select * from UserRights WHERE UserID=@UserID and Menu=@Menu and isActive='1'";
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["UserID"].ToString()));
                        DLdb.SQLST2.Parameters.AddWithValue("Menu", checkedItem.Value.ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {

                            }
                        }
                        else
                        {
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "insert into UserRights (UserID,Menu) values (@UserID,@Menu)";
                            DLdb.SQLST3.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["UserID"].ToString()));
                            DLdb.SQLST3.Parameters.AddWithValue("Menu", checkedItem.Value.ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();
                        }
                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                    }
                    else
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "select * from UserRights WHERE UserID=@UserID and Menu=@Menu and isActive='1'";
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["UserID"].ToString()));
                        DLdb.SQLST2.Parameters.AddWithValue("Menu", checkedItem.Value.ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                DLdb.RS3.Open();
                                DLdb.SQLST3.CommandText = "update UserRights set isActive='0' WHERE UserID=@UserID and Menu=@Menu";
                                DLdb.SQLST3.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["UserID"].ToString()));
                                DLdb.SQLST3.Parameters.AddWithValue("Menu", checkedItem.Value.ToString());
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
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                    }
                }


                Response.Redirect("viewuser?msg=Profile has been updated");

                DLdb.DB_Close();
            }
           
        }

    }
}