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
    public partial class reportQuestionsAdd : System.Web.UI.Page
    {
        public string tempImgID="";
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }
            
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }

            tempImgID = DLdb.CreatePassword(6);
            

            if (!IsPostBack)
            {
                TypeOfInstallation.Items.Clear();
                TypeOfInstallation.Items.Add(new ListItem("", ""));

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive = '1'";
                //DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        TypeOfInstallation.Items.Add(new ListItem(theSqlDataReader["InstallationType"].ToString(), theSqlDataReader["InstallationTypeID"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                

                DLdb.DB_Close();
            }

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            //if (tempID != "0")
            //{
            string CommentID = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO ReportQuestionList (refixPointsNotComplete,isActive,complimentaryPoints,cautionaryPoints,refixPoints,UserID,Name, Comment,TypeID,FieldID,Reference,SubID) VALUES (@refixPointsNotComplete,@isActive,@complimentaryPoints,@cautionaryPoints,@refixPoints,@UserID,@Name, @Comment,@TypeID,@FieldID,@Reference,@SubID); Select Scope_Identity() as CommentID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("Name", Name.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comment", Comment.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Reference", TextBox1.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("FieldID", FormFields.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("refixPoints", refixPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("refixPointsNotComplete", refixPointsNotComplete.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("cautionaryPoints", cautionaryPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("complimentaryPoints", complimentaryPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("isActive", isActive.Checked ? 1 : 0);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                CommentID = theSqlDataReader["CommentID"].ToString();
            }

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
            DLdb.RS.Close();

            int orderby = 0;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select top 1 * from formfields order by orderby desc";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                orderby = Convert.ToInt32(theSqlDataReader["orderby"].ToString()) + 1;
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO formfields (FormID,name, label,required,type,options,SubID,class,orderby,CommentTemplateID) VALUES (@FormID,@name, @label,@required,@type,@options,@SubID,@class,@orderby,@CommentTemplateID)";
            DLdb.SQLST.Parameters.AddWithValue("name", "option" + orderby);
            DLdb.SQLST.Parameters.AddWithValue("orderby", orderby);
            DLdb.SQLST.Parameters.AddWithValue("label", question.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("required", "1");
            DLdb.SQLST.Parameters.AddWithValue("type", "radio");
            DLdb.SQLST.Parameters.AddWithValue("options", "Yes,No");
            DLdb.SQLST.Parameters.AddWithValue("class", "padding-top");
            DLdb.SQLST.Parameters.AddWithValue("FormID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", CommentID);
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
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

          

            if (FileUpload2.HasFiles)
            {
                foreach (HttpPostedFile File in FileUpload2.PostedFiles)
                {
                    string filename = File.FileName;
                    string ImgID = "";
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AuditorImgs/"), filename));

                    // SAVE IMAGE WITH TEMPID
                   
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile) values (@imgsrc,@UserID,@FieldID,@tempID,'1'); Select Scope_Identity() as ImgID";
                    DLdb.SQLST.Parameters.AddWithValue("imgsrc", filename);
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("FieldID", "");
                    DLdb.SQLST.Parameters.AddWithValue("tempID", Session["IIT_tempID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                     theSqlDataReader = DLdb.SQLST.ExecuteReader();

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

                    Div1.InnerHtml += "<div class=\"col-md-3 img-thumbnail\" id=\"show_img_" + ImgID + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + ImgID + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";

                }
                // ADD MEDIA COMMENTTEMPLATEID
              
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update FormImg set CommentID = @CommentID, tempID = null where tempID = @tempid and UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", CommentID);
            DLdb.SQLST.Parameters.AddWithValue("tempID", Session["IIT_tempID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("reportQuestionsEdit.aspx?id=" + CommentID);
            //}
        }

        protected void btnUploads_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string CommentID = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO ReportQuestionList (refixPointsNotComplete,isActive,complimentaryPoints,cautionaryPoints,refixPoints,UserID,Name, Comment,TypeID,FieldID,Reference,SubID) VALUES (@refixPointsNotComplete,@isActive,@complimentaryPoints,@cautionaryPoints,@refixPoints,@UserID,@Name, @Comment,@TypeID,@FieldID,@Reference,@SubID); Select Scope_Identity() as CommentID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("Name", Name.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comment", Comment.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Reference", TextBox1.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("FieldID", FormFields.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("refixPointsNotComplete", refixPointsNotComplete.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("refixPoints", refixPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("cautionaryPoints", cautionaryPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("complimentaryPoints", complimentaryPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("isActive", isActive.Checked ? 1 : 0);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                CommentID = theSqlDataReader["CommentID"].ToString();
            }

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
            DLdb.RS.Close();

            int orderby = 0;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select top 1 * from formfields order by orderby desc";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                orderby = Convert.ToInt32(theSqlDataReader["orderby"].ToString()) + 1;
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO formfields (FormID,name, label,required,type,options,SubID,class,orderby,CommentTemplateID) VALUES (@FormID,@name, @label,@required,@type,@options,@SubID,@class,@orderby,@CommentTemplateID)";
            DLdb.SQLST.Parameters.AddWithValue("name", "option" + orderby);
            DLdb.SQLST.Parameters.AddWithValue("orderby", orderby);
            DLdb.SQLST.Parameters.AddWithValue("label", question.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("required", "1");
            DLdb.SQLST.Parameters.AddWithValue("type", "radio");
            DLdb.SQLST.Parameters.AddWithValue("options", "Yes,No");
            DLdb.SQLST.Parameters.AddWithValue("class", "padding-top");
            DLdb.SQLST.Parameters.AddWithValue("FormID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", CommentID);
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
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            //if (tempID != "0")
            //{
            if (FileUpload1.HasFiles)
            {
                foreach (HttpPostedFile File in FileUpload1.PostedFiles)
                {
                    string filename = File.FileName;
                    string ImgID = "";
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AuditorImgs/"), filename));
                    
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference) values (@imgsrc,@UserID,@FieldID,@tempID,'1','1'); Select Scope_Identity() as ImgID";
                    DLdb.SQLST.Parameters.AddWithValue("imgsrc", filename);
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("FieldID", "");
                    DLdb.SQLST.Parameters.AddWithValue("tempID", Session["IIT_tempID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

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

                    CurrentMedia.InnerHtml += "<div class=\"col-md-3 img-thumbnail\" id=\"show_img_" + ImgID + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImagea('" + ImgID + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";

                }
            }
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update FormImg set CommentID = @CommentID, tempID = null where tempID = @tempid and UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", CommentID);
            DLdb.SQLST.Parameters.AddWithValue("tempID", Session["IIT_tempID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            //}
            DLdb.DB_Close();
            Response.Redirect("reportQuestionsEdit.aspx?id=" + CommentID);
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string CommentID = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO ReportQuestionList (refixPointsNotComplete,isActive,complimentaryPoints,cautionaryPoints,refixPoints,UserID,Name, Comment,TypeID,FieldID,Reference,SubID) VALUES (@refixPointsNotComplete,@isActive,@complimentaryPoints,@cautionaryPoints,@refixPoints,@UserID,@Name, @Comment,@TypeID,@FieldID,@Reference,@SubID); Select Scope_Identity() as CommentID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("Name", Name.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comment", Comment.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Reference", TextBox1.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("refixPointsNotComplete", refixPointsNotComplete.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("FieldID", FormFields.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("refixPoints", refixPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("cautionaryPoints", cautionaryPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("complimentaryPoints", complimentaryPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("isActive", isActive.Checked ? 1 : 0);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                CommentID = theSqlDataReader["CommentID"].ToString();
            }

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
            DLdb.RS.Close();

            int orderby = 0;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select top 1 * from formfields order by orderby desc";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
             theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                orderby = Convert.ToInt32(theSqlDataReader["orderby"].ToString()) + 1;
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO formfields (FormID,name, label,required,type,options,SubID,class,orderby) VALUES (@FormID,@name, @label,@required,@type,@options,@SubID,@class,@orderby)";
            DLdb.SQLST.Parameters.AddWithValue("name", "option" + orderby);
            DLdb.SQLST.Parameters.AddWithValue("orderby", orderby);
            DLdb.SQLST.Parameters.AddWithValue("label", question.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("required", "1");
            DLdb.SQLST.Parameters.AddWithValue("type", "radio");
            DLdb.SQLST.Parameters.AddWithValue("options", "Yes,No");
            DLdb.SQLST.Parameters.AddWithValue("class", "padding-top");
            DLdb.SQLST.Parameters.AddWithValue("FormID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", CommentID);
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
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // ADD MEDIA COMMENTTEMPLATEID
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update FormImg set CommentID = @CommentID, tempID = null where tempID = @tempid and UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", CommentID);
            DLdb.SQLST.Parameters.AddWithValue("tempID", Session["IIT_tempID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            //string commNewID = "";
            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "select * from users where [Role]='Inspector' and isActive='1'";
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //theSqlDataReader = DLdb.SQLST.ExecuteReader();
            //if (theSqlDataReader.HasRows)
            //{
            //    while (theSqlDataReader.Read())
            //    {
            //        DLdb.RS2.Open();
            //        DLdb.SQLST2.CommandText = "INSERT INTO InspectorCommentTemplate (CommentTemplateID,UserID,Name, Comment,TypeID,FieldID) VALUES (@CommentTemplateID,@UserID,@Name, @Comment,@TypeID,@FieldID); select scope_identity() as commNewID;";
            //        DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
            //        DLdb.SQLST2.Parameters.AddWithValue("Name", Name.Text.ToString());
            //        DLdb.SQLST2.Parameters.AddWithValue("Comment", Comment.Text.ToString());
            //        DLdb.SQLST2.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            //        DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.Text.ToString());
            //        DLdb.SQLST2.Parameters.AddWithValue("CommentTemplateID", CommentID.ToString());
            //        DLdb.SQLST2.CommandType = CommandType.Text;
            //        DLdb.SQLST2.Connection = DLdb.RS2;
            //        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
            //        if (theSqlDataReader2.HasRows)
            //        {
            //            theSqlDataReader2.Read();
            //            commNewID = theSqlDataReader2["commNewID"].ToString();
            //        }
            //        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            //        DLdb.SQLST2.Parameters.RemoveAt(0);
            //        DLdb.SQLST2.Parameters.RemoveAt(0);
            //        DLdb.SQLST2.Parameters.RemoveAt(0);
            //        DLdb.SQLST2.Parameters.RemoveAt(0);
            //        DLdb.SQLST2.Parameters.RemoveAt(0);
            //        DLdb.SQLST2.Parameters.RemoveAt(0);
            //        DLdb.RS2.Close();

            //        DLdb.RS2.Open();
            //        DLdb.SQLST2.CommandText = "select * from formImg where CommentTemplateID=@CommentTemplateID and tempID is null";
            //        DLdb.SQLST2.Parameters.AddWithValue("CommentTemplateID", CommentID.ToString());
            //        DLdb.SQLST2.CommandType = CommandType.Text;
            //        DLdb.SQLST2.Connection = DLdb.RS2;
            //        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
            //        if (theSqlDataReader2.HasRows)
            //        {
            //            while (theSqlDataReader2.Read())
            //            {
            //                DLdb.RS3.Open();
            //                DLdb.SQLST3.CommandText = "insert into formImg (imgsrc,CommentTemplateID,UserID,isFile,tempID) values (@imgsrc,@CommentTemplateID,@UserID,'1',@tempID)";
            //                DLdb.SQLST3.Parameters.AddWithValue("imgsrc", theSqlDataReader2["imgsrc"].ToString());
            //                DLdb.SQLST3.Parameters.AddWithValue("CommentTemplateID", commNewID);
            //                DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
            //                DLdb.SQLST3.Parameters.AddWithValue("tempID", "0");
            //                DLdb.SQLST3.CommandType = CommandType.Text;
            //                DLdb.SQLST3.Connection = DLdb.RS3;
            //                SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();
                            
            //                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            //                DLdb.SQLST3.Parameters.RemoveAt(0);
            //                DLdb.SQLST3.Parameters.RemoveAt(0);
            //                DLdb.SQLST3.Parameters.RemoveAt(0);
            //                DLdb.SQLST3.Parameters.RemoveAt(0);
            //                DLdb.RS3.Close();
            //            }
            //        }
            //        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            //        DLdb.SQLST2.Parameters.RemoveAt(0);
            //        DLdb.RS2.Close();

            //    }
            //}
            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.RS.Close();
            
            DLdb.DB_Close();

            Response.Redirect("reportQuestions.aspx");
        }

        protected void TypeOfInstallation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            //FormFields.Items.Clear();
            //FormFields.Items.Add(new ListItem("", ""));

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from FormLinks l inner join formfields f on l.FormID = f.FormID where TypeID = @TypeID";
            DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    //FormFields.Items.Add(new ListItem(theSqlDataReader["Label"].ToString(), theSqlDataReader["FieldID"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            subTypes.Items.Clear();
            subTypes.Items.Add(new ListItem("", ""));

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypessub where isActive = '1' and InstallationTypeID=@InstallationTypeID";
            DLdb.SQLST.Parameters.AddWithValue("InstallationTypeID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    subTypes.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["subID"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }
    }
}