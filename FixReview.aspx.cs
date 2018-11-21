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
    public partial class FixReview : System.Web.UI.Page
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
            
            
            if (!IsPostBack)
            {

                

                DLdb.DB_Close();
            }

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //if (tempID != "0")
            //{
                if (FileUpload1.HasFiles)
                {
                    foreach (HttpPostedFile File in FileUpload1.PostedFiles)
                    {
                        string filename = File.FileName;
                        string ImgID = "";
                        File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AuditorImgs/"), filename));

                        // SAVE IMAGE WITH TEMPID
                        Global DLdb = new Global();
                        DLdb.DB_Connect();

                        DLdb.RS.Open();
                        DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile) values (@imgsrc,@UserID,@FieldID,@tempID,'1'); Select Scope_Identity() as ImgID";
                        DLdb.SQLST.Parameters.AddWithValue("imgsrc", filename);
                        DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                        DLdb.SQLST.Parameters.AddWithValue("FieldID", "");
                        DLdb.SQLST.Parameters.AddWithValue("tempID", "0");
                        DLdb.SQLST.CommandType = CommandType.Text;
                        DLdb.SQLST.Connection = DLdb.RS;
                        SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
                        
                        if (theSqlDataReader.HasRows)
                        {
                            theSqlDataReader.Read();
                            ImgID = theSqlDataReader["ImgID"].ToString();
                        }

                        if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.RS.Close();

                        CurrentMedia.InnerHtml += "<div class=\"col-md-3\" id=\"show_img_" + ImgID + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + ImgID + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" class=\"img img-responsive\" /></a></div>";

                        DLdb.DB_Close();
                    }

                    
                }
            //}
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            string isFixed = "0";
            if (rdYes.Checked == true)
            {
                isFixed = "1";
            } 

            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Update COCReviews set isFixed = @isFixed, RefixComments = @Comments where ReviewID = @ReviewID";
            DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["rid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("isFixed", isFixed);
            DLdb.SQLST.Parameters.AddWithValue("Comments", ReviewComments.Text.ToString());

            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // ADD MEDIA COMMENT TEMPLATEID
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update FormImg set ReviewID = @ReviewID, tempID = null where tempID = '0' and UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["rid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("tempID", "0");
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());

            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            //REQUIRED: SMS AUDITOR

            DLdb.DB_Close();

            Response.Redirect("EditCOCStatement?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("Review updated successfully"));

        }
        

        
    }
}