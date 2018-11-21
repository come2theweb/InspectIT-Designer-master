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
    public partial class EditorDeleteAssessment : System.Web.UI.Page
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
               

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Assessments where isActive = '1' and AssessmentID=@AssessmentID";
                DLdb.SQLST.Parameters.AddWithValue("AssessmentID", Request.QueryString["aid"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        Category.SelectedValue = theSqlDataReader["Category"].ToString();
                        Activity.Text = theSqlDataReader["Activity"].ToString();
                        Points.Text = theSqlDataReader["NoPoints"].ToString();
                        productCode.Text = theSqlDataReader["productCode"].ToString();
                        activityDate.Text = theSqlDataReader["CertificateDate"].ToString();
                        CPDActivityID.Text = theSqlDataReader["CPDActivityID"].ToString();
                        CommentsActivity.Text = theSqlDataReader["comment"].ToString();
                        imageDisp.ImageUrl = "AssessmentImgs/" + theSqlDataReader["Attachment"].ToString();
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                regno.Text = Session["IIT_RegNo"].ToString();
                name.Text = Session["IIT_UfName"].ToString();
                surname.Text = Session["IIT_UsName"].ToString();
            }

            

        }
        
    }
}