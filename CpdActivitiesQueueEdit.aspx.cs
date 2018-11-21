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
    public partial class CpdActivitiesQueueEdit : System.Web.UI.Page
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
                selectChk.Visible = false;
                Span1.Visible = false;
                if (Request.QueryString["aid"]!=null)
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from Assessments where AssessmentID=@AssessmentID and isActive='1'";
                    DLdb.SQLST.Parameters.AddWithValue("AssessmentID", DLdb.Decrypt(Request.QueryString["aid"].ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        userid = theSqlDataReader["UserID"].ToString();
                        Category.SelectedValue = theSqlDataReader["Category"].ToString();
                        Activity.Text = theSqlDataReader["Activity"].ToString();
                        Points.Text = theSqlDataReader["NoPoints"].ToString();
                        productCode.Text = theSqlDataReader["productCode"].ToString();
                        activityDate.Text = theSqlDataReader["CertificateDate"].ToString();
                        CPDActivityID.Text = theSqlDataReader["CPDActivityID"].ToString();
                        CommentsActivity.Text = theSqlDataReader["Comment"].ToString();
                        Image1.ImageUrl = "AssessmentImgs/"+ theSqlDataReader["Attachment"].ToString();
                        if (theSqlDataReader["Declaration"].ToString() == "True")
                        {
                            CheckBox1.Checked = true;
                        }
                    }
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
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
                errMsgProdCode.Visible = false;
                checkBxErr.Visible = false;
                
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
           
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string uid = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Assessments where AssessmentID=@AssessmentID and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("AssessmentID", DLdb.Decrypt(Request.QueryString["aid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                uid = theSqlDataReader["UserID"].ToString();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            if (RadioButton1.Checked)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update Assessments set isApproved='1',AdminComment=@AdminComment where AssessmentID=@AssessmentID";
                DLdb.SQLST.Parameters.AddWithValue("AssessmentID", DLdb.Decrypt(Request.QueryString["aid"].ToString()));
                DLdb.SQLST.Parameters.AddWithValue("AdminComment", TextBox1.Text.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string designation = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM PlumberDesignations where isActive = '1' and PlumberID=@PlumberID";
                DLdb.SQLST.Parameters.AddWithValue("PlumberID", uid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        designation = theSqlDataReader["Designation"].ToString();
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string weightingID = "";
                decimal weightingPercentage = 0;
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Weighting where isActive = '1' and WeightingID='1'";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    weightingID = theSqlDataReader["weightingID"].ToString();
                    if (designation == "Qualified Plumber  ")
                    {
                        weightingPercentage = Convert.ToDecimal(theSqlDataReader["Qualified"].ToString());
                    }
                    else if (designation == "Licensed Plumber")
                    {
                        weightingPercentage = Convert.ToDecimal(theSqlDataReader["Licensed"].ToString());
                    }
                    else if (designation == "Master Plumber")
                    {
                        weightingPercentage = Convert.ToDecimal(theSqlDataReader["Master"].ToString());
                    }
                    else if (designation == "Director Plumber")
                    {
                        weightingPercentage = Convert.ToDecimal(theSqlDataReader["Director"].ToString());
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                decimal totalPointsAssigned = weightingPercentage / 100 * Convert.ToDecimal(Points.Text);
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into  AssignedWeighting (UserID,WeightingID,Points,CPDActivityID,CPDPoints,Type,weightingValue,TypePlumber) values (@UserID,@WeightingID,@Points,@CPDActivityID,@CPDPoints,'CPD',@weightingValue,@TypePlumber)";
                DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
                DLdb.SQLST.Parameters.AddWithValue("WeightingID", weightingID.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Points", totalPointsAssigned.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CPDActivityID", CPDActivityID.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CPDPoints", Points.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("weightingValue", weightingPercentage.ToString());
                DLdb.SQLST.Parameters.AddWithValue("TypePlumber", designation.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
                Response.Redirect("CpdActivitiesQueue.aspx?msg=" + DLdb.Encrypt("Assessment Approved"));
            }
            else if (RadioButton2.Checked)
            {
                if (TextBox1.Text == "")
                {
                    selectChk.Visible = true;
                    selectChk.InnerHtml = "If you are rejecting the activity please add a comment";
                }
                else
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update Assessments set isRejected='1',AdminComment=@AdminComment where AssessmentID=@AssessmentID";
                    DLdb.SQLST.Parameters.AddWithValue("AssessmentID", DLdb.Decrypt(Request.QueryString["aid"].ToString()));
                    DLdb.SQLST.Parameters.AddWithValue("AdminComment", TextBox1.Text.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.DB_Close();
                    Response.Redirect("CpdActivitiesQueue.aspx?msg=" + DLdb.Encrypt("Assessment Rejected"));
                }
                
            }
            else
            {
                selectChk.Visible = true;
                selectChk.InnerHtml = "Please Approve or Reject CPD Activity";
            }
            
            
        }

        protected void searchProdCode_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from CPDActivities where ProductCode=@ProductCode and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("ProductCode", productCode.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                Category.SelectedValue = theSqlDataReader["Category"].ToString();
                Activity.Text = theSqlDataReader["Activity"].ToString();
                Points.Text = theSqlDataReader["Points"].ToString();
                activityDate.Text = theSqlDataReader["StartDate"].ToString();
                CPDActivityID.Text = theSqlDataReader["CPDActivityID"].ToString();
            }
            else
            {
                errMsgProdCode.Visible = true;
                errMsgProdCode.InnerHtml = "Unable to find product code entered";
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            DLdb.DB_Close();
        }

        protected void btn2_add_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            
            if (RadioButton2.Checked && TextBox1.Text == "")
            {
                selectChk.Visible = true;
                selectChk.InnerHtml = "If you are rejecting the activity please add a comment";
            }
            else
            {
               

            if (img.HasFiles)
            {
                foreach (HttpPostedFile File in img.PostedFiles)
                {
                    string filename = File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AssessmentImgs/"), filename));
                }
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into Assessments (isApproved,AdminComment,UserID,Category,CPDActivityID,Activity,ProductCode,NoPoints,CertificateDate,Comment,Attachment,isRejected,Declaration) values (@isApproved,@AdminComment,@UserID,@Category,@CPDActivityID,@Activity,@ProductCode,@NoPoints,@CertificateDate,@Comment,@Attachment,@isRejected,@Declaration)";
            DLdb.SQLST.Parameters.AddWithValue("isApproved", RadioButton1.Checked ? 1:0);
            DLdb.SQLST.Parameters.AddWithValue("AdminComment", TextBox1.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", uids.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Category", Category.SelectedValue);
            DLdb.SQLST.Parameters.AddWithValue("CPDActivityID", CPDActivityID.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Activity", Activity.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ProductCode", productCode.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("NoPoints", Points.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CertificateDate", activityDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comment", CommentsActivity.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Attachment", img.FileName);
            DLdb.SQLST.Parameters.AddWithValue("isRejected", RadioButton2.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("Declaration", CheckBox1.Checked ? 1 : 0);
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
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

                Response.Redirect("CpdActivitiesQueue.aspx?msg=" + DLdb.Encrypt("Assessment Added"));
            }
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
    }
}