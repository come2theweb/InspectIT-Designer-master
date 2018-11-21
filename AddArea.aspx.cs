using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GoogleMaps.LocationServices;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.IO;

namespace InspectIT
{
    public partial class AddArea : System.Web.UI.Page
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
                if (Request.QueryString["id"] != null)
                {
                    btn_add.Visible = false;
                    btn_update.Visible = true;

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from AreaSuburbs where suburbid=@suburbid";
                    DLdb.SQLST.Parameters.AddWithValue("suburbid", DLdb.Decrypt(Request.QueryString["id"]));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "select * from Area where ProvinceID=@ProvinceID";
                            DLdb.SQLST2.Parameters.AddWithValue("ProvinceID",theSqlDataReader["ProvinceID"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                while (theSqlDataReader2.Read())
                                {
                                    DropDownList2.Items.Add(new ListItem(theSqlDataReader2["Name"].ToString(), theSqlDataReader2["ID"].ToString()));
                                }
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            DropDownList1.SelectedValue = theSqlDataReader["ProvinceID"].ToString();
                            DropDownList2.SelectedValue = theSqlDataReader["CityID"].ToString();
                            suburbname.Text = theSqlDataReader["Name"].ToString();
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }
                else
                {
                    btn_add.Visible = true;
                    btn_update.Visible = false;
                }
                
            }

            DLdb.DB_Close();
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO AreaSuburbs (cityid,Name,ProvinceID) VALUES (@cityid,@Name,@ProvinceID)";
            DLdb.SQLST.Parameters.AddWithValue("cityid", DropDownList2.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Name", suburbname.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ProvinceID", DropDownList1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("ViewAreas.aspx?msg=" + DLdb.Encrypt("Area added successfuly"));
        }
        

        protected void myListDropDown_Change(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DropDownList2.Items.Clear();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Area where ProvinceID=@ProvinceID";
            DLdb.SQLST.Parameters.AddWithValue("ProvinceID", DropDownList1.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DropDownList2.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["ID"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            DLdb.DB_Close();
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update AreaSuburbs set cityid=@cityid,name=@name,ProvinceID=@ProvinceID where SuburbID=@SuburbID";
            DLdb.SQLST.Parameters.AddWithValue("SuburbID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("cityid", DropDownList2.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("name", suburbname.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ProvinceID", DropDownList1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            DLdb.DB_Close();
            Response.Redirect("ViewAreas.aspx?msg=" + DLdb.Encrypt("Area updated successfuly"));
        }
    }
}