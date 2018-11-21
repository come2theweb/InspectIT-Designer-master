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
    public partial class AssessmentsQueueEdit : System.Web.UI.Page
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
                string userid = "";
                Span1.Visible = false;
                if (Request.QueryString["aid"]!=null)
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from AssessmentDetails where AssessID=@AssessID and isActive='1'";
                    DLdb.SQLST.Parameters.AddWithValue("AssessID", DLdb.Decrypt(Request.QueryString["aid"].ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        userid = theSqlDataReader["UserID"].ToString();
                        locations.SelectedValue = theSqlDataReader["location"].ToString();
                        Date.Text = theSqlDataReader["Date"].ToString();
                        Time.Text = theSqlDataReader["Time"].ToString();
                        OTP.Text = theSqlDataReader["OTP"].ToString();
                        imgName.Text = theSqlDataReader["Attachment"].ToString();
                        Result.SelectedValue = theSqlDataReader["Result"].ToString();
                        AssessType.SelectedValue = theSqlDataReader["Type"].ToString();
                        ResultPercent.Text = theSqlDataReader["ResultPercentage"].ToString();
                        
                        Image1.ImageUrl = "AssessmentImgs/"+ theSqlDataReader["Attachment"].ToString();
                        if (theSqlDataReader["NewCard"].ToString() == "True")
                        {
                            CheckBox1.Checked = true;
                        }
                        string commss = "";
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "select * from AssessmentDetailsComments where AssessCommID = @AssessCommID";
                        DLdb.SQLST2.Parameters.AddWithValue("AssessCommID", theSqlDataReader["AssessID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                commss += theSqlDataReader2["Comment"].ToString() + "<br/>";
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                        dispCommsAssess.InnerHtml = commss;
                    }
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    AssessType.Items.Add(new ListItem("", ""));
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from AssessmentTypes where isactive='1'";
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            AssessType.Items.Add(new ListItem(theSqlDataReader["Type"].ToString(), theSqlDataReader["Type"].ToString()));
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.RS.Close();

                    locations.Items.Add(new ListItem("", ""));
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from LocationTypes where isactive='1'";
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            locations.Items.Add(new ListItem(theSqlDataReader["Location"].ToString(), theSqlDataReader["Location"].ToString()));
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", userid.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        regno.Text = theSqlDataReader["regno"].ToString();
                        name.Text = theSqlDataReader["fname"].ToString();
                        surname.Text = theSqlDataReader["lname"].ToString();
                        cell.Text = theSqlDataReader["contact"].ToString();
                        email.Text = theSqlDataReader["email"].ToString();
                    }
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                    btn_add.Visible = true;
                    Button1.Visible = false;
                }
                else
                {
                    btn_add.Visible = false;
                    Button1.Visible = true;
                }
                checkBxErr.Visible = false;
                
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
           
            Global DLdb = new Global();
            DLdb.DB_Connect();
            if (img.HasFiles)
            {
                foreach (HttpPostedFile File in img.PostedFiles)
                {
                    string filename = File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AssessmentImgs/"), filename));
                }
            }
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into AssessmentDetails (UserID,Location,Type,Date,Time,Result,Attachment,OTP,ResultPercentage) values (@UserID,@Location,@Type,@Date,@Time,@Result,@Attachment,@OTP,@ResultPercentage)";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uids.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Location", locations.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Type", AssessType.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Date", Date.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Time", Time.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Result", Result.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("OTP", OTP.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ResultPercentage", ResultPercent.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Attachment", img.FileName);
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
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("AssessmentsQueue?msg=" + DLdb.Encrypt("Assessment Added"));
        }
        

        protected void btn2_add_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string ImgOld = imgName.Text.ToString();
            if (img.HasFiles)
            {
                ImgOld = img.FileName;
                foreach (HttpPostedFile File in img.PostedFiles)
                {
                    string filename = File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AssessmentImgs/"), filename));
                }
            }
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update AssessmentDetails set UserID=@UserID,Location=@Location,Type=@Type,Date=@Date,Time=@Time,Result=@Result,Attachment=@Attachment,OTP=@OTP,ResultPercentage=@ResultPercentage where AssessID=@AssessID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uids.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Location", locations.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Type", AssessType.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Date", Date.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Time", Time.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Result", Result.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("OTP", OTP.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ResultPercentage", ResultPercent.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AssessID", DLdb.Decrypt(Request.QueryString["aid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("Attachment", img.FileName);
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
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("AssessmentsQueue?msg=" + DLdb.Encrypt("Assessment updated"));
        }

        protected void searchuid_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where RegNo=@RegNo and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("RegNo", regno.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                name.Text = theSqlDataReader["fname"].ToString();
                surname.Text = theSqlDataReader["lname"].ToString();
                uids.Text = theSqlDataReader["UserID"].ToString();
                email.Text = theSqlDataReader["email"].ToString();
                cell.Text = theSqlDataReader["contact"].ToString();
            }
            else
            {
                Span1.Visible = true;
                Span1.InnerHtml = "Unable to find registration number entered";
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        protected void add_comm_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into AssessmentDetailsComments (AssessID,Comment) values (@AssessID,@Comment)";
            DLdb.SQLST.Parameters.AddWithValue("AssessID", DLdb.Decrypt(Request.QueryString["aid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("Comment", CommentsActivity.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            DLdb.DB_Close();
        }
    }
}