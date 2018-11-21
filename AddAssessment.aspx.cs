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
    public partial class AddAssessment : System.Web.UI.Page
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
            if (Session["IIT_Role"].ToString() != "Staff")
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
                if (Request.QueryString["aid"]!=null)
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from Assessments where AssessmentID=@AssessmentID and isActive='1'";
                    DLdb.SQLST.Parameters.AddWithValue("AssessmentID", Request.QueryString["aid"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        Category.SelectedValue = theSqlDataReader["Category"].ToString();
                        Activity.Text = theSqlDataReader["Activity"].ToString();
                        Points.Text = theSqlDataReader["NoPoints"].ToString();
                        productCode.Text = theSqlDataReader["productCode"].ToString();
                        activityDate.Text = theSqlDataReader["CertificateDate"].ToString();
                        CPDActivityID.Text = theSqlDataReader["CPDActivityID"].ToString();
                        CommentsActivity.Text = theSqlDataReader["comment"].ToString();
                        if (theSqlDataReader["Declaration"].ToString()=="True")
                        {
                            CheckBox1.Checked = true;
                        }
                        imageDisp.ImageUrl = "AssessmentImgs/" + theSqlDataReader["Attachment"].ToString();
                        img.Visible = false;
                    }
                    else
                    {
                        errMsgProdCode.Visible = true;
                        errMsgProdCode.InnerHtml = "Unable to find product code entered";
                    }
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                    btn_add.Visible = false;
                }
                else
                {
                    imageDisp.Visible = false;
                    //btn_add.Visible = false;
                }
                errMsgProdCode.Visible = false;
                checkBxErr.Visible = false;
                regno.Text = Session["IIT_RegNo"].ToString();
                name.Text = Session["IIT_UfName"].ToString();
                surname.Text = Session["IIT_UsName"].ToString();
                
            }

            

        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
           
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (!CheckBox1.Checked)
            {
                checkBxErr.Visible = true;
                checkBxErr.InnerHtml = "Please check the declaration before submitting this CPD Activity";
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
                DLdb.SQLST.CommandText = "INSERT INTO Assessments (UserID,Attachment,Comment,Category,Activity,ProductCode,CPDActivityID,NoPoints,CertificateDate,Declaration)" +
                                         "VALUES (@UserID,@Attachment,@Comment,@Category,@Activity,@ProductCode,@CPDActivityID,@NoPoints,@CertificateDate,@Declaration)";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("Category", Category.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("ProductCode", productCode.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CPDActivityID", CPDActivityID.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("NoPoints", Points.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CertificateDate", activityDate.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Activity", Activity.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Comment", CommentsActivity.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Attachment", img.FileName);
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
                DLdb.RS.Close();

                DLdb.DB_Close();
                Response.Redirect("UserAssessments.aspx?msg=" + DLdb.Encrypt("Assessment added successfuly"));
            }
            
            

            // EMAIL THE USER DETAILS
            //string HTMLSubject = "Welcome to Inspect IT.";
            //string HTMLBody = "Dear " + fName.Text.ToString() + "<br /><br />Welcome to Inspect IT<br /><br />Your login details are;<br />Email Address: " + email.Text.ToString() + "<br />Password: " + pass.ToString() + "<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Team";
           // DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", email.Text.ToString(), "");

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
    }
}