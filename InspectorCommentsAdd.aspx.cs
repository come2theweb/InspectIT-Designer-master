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
    public partial class InspectorCommentsAdd : System.Web.UI.Page
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
            if (Session["IIT_Role"].ToString() != "Inspector")
            {
                Response.Redirect("Default");
            }
            
            if (!IsPostBack)
            {

                reviewShow.Visible = false;
                FormFields.Visible = false;
                question.Visible = true;
                // LOAD TYPE OF INSTALLATIONS
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
            string CommentID = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO InspectorCommentTemplate (refixPointsNotComplete,refixPoints,cautionaryPoints,complimentaryPoints,UserID,Name, Comment,TypeID,FieldID,Question,SubID,Reference) VALUES (@refixPointsNotComplete,@refixPoints,@cautionaryPoints,@complimentaryPoints,@UserID,@Name, @Comment,@TypeID,@FieldID,@Question,@SubID,@Reference); Select Scope_Identity() as CommentID";
            DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("Name", Name.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comment", Comment.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Question", FormFields.SelectedItem.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Reference", TextBox1.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("refixPointsNotComplete", refixPointsNotComplete.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("refixPoints", refixPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("cautionaryPoints", cautionaryPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("complimentaryPoints", complimentaryPoints.Text.ToString());

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

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentID=@CommentID and isreference='0'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", FormFields.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,CommentTemplateID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','0',@CommentTemplateID)";
                    DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                    DLdb.SQLST2.Parameters.AddWithValue("CommentTemplateID", CommentID);
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentID=@CommentID and isreference='1'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", FormFields.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,CommentTemplateID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','1',@CommentTemplateID)";
                    DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                    DLdb.SQLST2.Parameters.AddWithValue("CommentTemplateID", CommentID);
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
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
                        DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference) values (@imgsrc,@UserID,@FieldID,@tempID,'1','0'); Select Scope_Identity() as ImgID";
                        DLdb.SQLST.Parameters.AddWithValue("imgsrc", filename);
                        DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                        DLdb.SQLST.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
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

                        DLdb.DB_Close();
                    }

                    
                }
            //}
            // ADD MEDIA COMMENTTEMPLATEID
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update FormImg set CommentTemplateID = @CommentTemplateID, tempID = null where tempID = @tempID and UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", CommentID);
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

            Response.Redirect("InspectorCommentsEdit.aspx?cid=" + DLdb.Encrypt(CommentID));
        }

        protected void btnUploads_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string CommentID = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO InspectorCommentTemplate (refixPointsNotComplete,refixPoints,cautionaryPoints,complimentaryPoints,UserID,Name, Comment,TypeID,FieldID,Question,SubID,Reference) VALUES (@refixPointsNotComplete,@refixPoints,@cautionaryPoints,@complimentaryPoints,@UserID,@Name, @Comment,@TypeID,@FieldID,@Question,@SubID,@Reference); Select Scope_Identity() as CommentID";
            DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("Name", Name.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comment", Comment.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Question", FormFields.SelectedItem.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Reference", TextBox1.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("refixPointsNotComplete", refixPointsNotComplete.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("refixPoints", refixPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("cautionaryPoints", cautionaryPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("complimentaryPoints", complimentaryPoints.Text.ToString());

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

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentID=@CommentID and isreference='0'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", FormFields.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,CommentTemplateID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','0',@CommentTemplateID)";
                    DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                    DLdb.SQLST2.Parameters.AddWithValue("CommentTemplateID", CommentID);
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentID=@CommentID and isreference='1'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", FormFields.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,CommentTemplateID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','1',@CommentTemplateID)";
                    DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                    DLdb.SQLST2.Parameters.AddWithValue("CommentTemplateID", CommentID);
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

           
            //if (tempID != "0")
            //{
            if (FileUpload2.HasFiles)
            {
                foreach (HttpPostedFile File in FileUpload2.PostedFiles)
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

                    DLdb.DB_Close();
                }


            }

            // ADD MEDIA COMMENTTEMPLATEID
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update FormImg set CommentTemplateID = @CommentTemplateID, tempID = null where tempID = @tempID and UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", CommentID);
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

            Response.Redirect("InspectorCommentsEdit.aspx?cid=" + DLdb.Encrypt(CommentID));
            //}
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string CommentID = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO InspectorCommentTemplate (refixPointsNotComplete,refixPoints,cautionaryPoints,complimentaryPoints,UserID,Name, Comment,TypeID,FieldID,Question,SubID,Reference) VALUES (@refixPointsNotComplete,@refixPoints,@cautionaryPoints,@complimentaryPoints,@UserID,@Name, @Comment,@TypeID,@FieldID,@Question,@SubID,@Reference); Select Scope_Identity() as CommentID";
            DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("Name", Name.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comment", Comment.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Question", FormFields.SelectedItem.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Reference", TextBox1.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());//
            DLdb.SQLST.Parameters.AddWithValue("refixPointsNotComplete", refixPointsNotComplete.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("refixPoints", refixPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("cautionaryPoints", cautionaryPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("complimentaryPoints", complimentaryPoints.Text.ToString());
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

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentID=@CommentID and isreference='0'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", FormFields.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,CommentTemplateID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','0',@CommentTemplateID)";
                    DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                    DLdb.SQLST2.Parameters.AddWithValue("CommentTemplateID", CommentID);
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentID=@CommentID and isreference='1'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", FormFields.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,CommentTemplateID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','1',@CommentTemplateID)";
                    DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                    DLdb.SQLST2.Parameters.AddWithValue("CommentTemplateID", CommentID);
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // ADD MEDIA COMMENTTEMPLATEID
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update FormImg set CommentTemplateID = @CommentTemplateID, tempID = null where tempID = @tempID and UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", CommentID);
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

            Response.Redirect("InspectorComments.aspx");
        }

        protected void TypeOfInstallation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            
            subTypes.Items.Clear();
            subTypes.Items.Add(new ListItem("", ""));

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypessub where isActive = '1' and InstallationTypeID=@InstallationTypeID";
            DLdb.SQLST.Parameters.AddWithValue("InstallationTypeID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

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

        protected void subTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            FormFields.Items.Clear();
            FormFields.Items.Add(new ListItem("", ""));

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from FormLinks l inner join formfields f on l.FormID = f.FormID where TypeID = @TypeID and f.SubID=@SubID";
            DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                { 
                    // FormFields.Items.Add(new ListItem(theSqlDataReader["Label"].ToString(), theSqlDataReader["FieldID"].ToString()));
                    FormFields.Items.Add(new ListItem(theSqlDataReader["Label"].ToString(), theSqlDataReader["CommentTemplateID"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            reviewDropdown.Items.Clear();
            reviewDropdown.Items.Add(new ListItem("", ""));
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from ReportQuestionList where TypeID = @TypeID and SubID=@SubID";
            DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    reviewDropdown.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["ID"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked==true)
            {
                reviewShow.Visible = true;
                FormFields.Visible = true;
                question.Visible = false;
            }
            else
            {
                reviewShow.Visible = false;
                FormFields.Visible = false;
                question.Visible = true;
            }
        }

        protected void reviewDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from ReportQuestionList where ID = @ID";
            DLdb.SQLST.Parameters.AddWithValue("ID", FormFields.SelectedValue.ToString());
            //DLdb.SQLST.Parameters.AddWithValue("ID", reviewDropdown.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Name.Text = theSqlDataReader["Name"].ToString();
                    Comment.Text = theSqlDataReader["Comment"].ToString();
                    TextBox1.Text = theSqlDataReader["Reference"].ToString();
                    refixPoints.Text = theSqlDataReader["refixPoints"].ToString();
                    refixPointsNotComplete.Text = theSqlDataReader["refixPointsNotComplete"].ToString();
                    cautionaryPoints.Text = theSqlDataReader["cautionaryPoints"].ToString();
                    complimentaryPoints.Text = theSqlDataReader["complimentaryPoints"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from formfields where CommentTemplateID = @CommentTemplateID";
            DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", reviewDropdown.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    FormFields.SelectedValue = theSqlDataReader["FieldID"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentID=@CommentID and isreference='0'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", FormFields.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Div1.InnerHtml += "<div class=\"col-md-3 img-thumbnail\" id=\"show_img_" + theSqlDataReader["imgid"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader["imgid"].ToString() + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + theSqlDataReader["imgSrc"].ToString() + "\" target=\"_blank\"><img src=\"AuditorImgs/" + theSqlDataReader["imgsrc"].ToString() + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentID=@CommentID and isreference='1'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", FormFields.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    CurrentMedia.InnerHtml += "<div class=\"col-md-3 img-thumbnail\" id=\"show_img_" + theSqlDataReader["imgid"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader["imgid"].ToString() + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + theSqlDataReader["imgSrc"].ToString() + "\" target=\"_blank\"><img src=\"AuditorImgs/" + theSqlDataReader["imgsrc"].ToString() + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }
    }
}