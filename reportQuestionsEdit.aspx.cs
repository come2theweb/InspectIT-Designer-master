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
    public partial class reportQuestionsEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }
            
            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                btn_add.Visible = false;
                btnUpload.Visible = false;
                Button1.Visible = false;
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {
                
            }

            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }

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

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentID=@CommentID and isreference='0'";
                DLdb.SQLST.Parameters.AddWithValue("CommentID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        Div1.InnerHtml += "<div class=\"col-md-3 img-thumbnail\" id=\"show_img_" + theSqlDataReader["imgid"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader["imgid"].ToString() + "','"+ DLdb.Decrypt(Request.QueryString["id"].ToString()) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + theSqlDataReader["imgSrc"].ToString() + "\" target=\"_blank\"><img src=\"AuditorImgs/" + theSqlDataReader["imgsrc"].ToString() + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";

                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentID=@CommentID and isreference='1'";
                DLdb.SQLST.Parameters.AddWithValue("CommentID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        CurrentMedia.InnerHtml += "<div class=\"col-md-3 img-thumbnail\" id=\"show_img_" + theSqlDataReader["imgid"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImagea('" + theSqlDataReader["imgid"].ToString() + "','" + DLdb.Decrypt(Request.QueryString["id"].ToString()) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + theSqlDataReader["imgSrc"].ToString() + "\" target=\"_blank\"><img src=\"AuditorImgs/" + theSqlDataReader["imgsrc"].ToString() + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";

                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM ReportQuestionList where ID = @ID";
                DLdb.SQLST.Parameters.AddWithValue("ID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        subTypes.Items.Clear();
                        subTypes.Items.Add(new ListItem("", ""));

                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM InstallationTypessub where isActive = '1' and InstallationTypeID=@InstallationTypeID";
                        DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["TypeID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                subTypes.Items.Add(new ListItem(theSqlDataReader2["Name"].ToString(), theSqlDataReader2["subID"].ToString()));
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM formfields where isActive = '1' and CommentTemplateID=@CommentTemplateID";
                        DLdb.SQLST2.Parameters.AddWithValue("CommentTemplateID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                question.Text = theSqlDataReader2["label"].ToString();
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        string mTypeOfInstallation = theSqlDataReader["TypeID"].ToString();
                        TypeOfInstallation.SelectedIndex = TypeOfInstallation.Items.IndexOf(TypeOfInstallation.Items.FindByValue(mTypeOfInstallation));
                        FormFields.Text = theSqlDataReader["FieldID"].ToString();
                        Name.Text = theSqlDataReader["Name"].ToString();
                        Comment.Text = theSqlDataReader["Comment"].ToString();
                        TextBox1.Text = theSqlDataReader["Reference"].ToString();
                        subTypes.SelectedValue= theSqlDataReader["SubID"].ToString();
                        complimentaryPoints.Text = theSqlDataReader["complimentaryPoints"].ToString();
                        cautionaryPoints.Text = theSqlDataReader["cautionaryPoints"].ToString();
                        refixPointsNotComplete.Text = theSqlDataReader["refixPointsNotComplete"].ToString();
                        refixPoints.Text = theSqlDataReader["refixPoints"].ToString();
                        if (theSqlDataReader["isActive"].ToString() == "True")
                        {
                            isActive.Checked = true;
                        }
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();


                DLdb.DB_Close();
            }

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //if (tempID != "0")
            //{
            if (FileUpload2.HasFiles)
            {
                foreach (HttpPostedFile File in FileUpload2.PostedFiles)
                {
                    string filename = File.FileName;
                    string ImgID = "";
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AuditorImgs/"), filename));

                    // SAVE IMAGE WITH TEMPID
                    Global DLdb = new Global();
                    DLdb.DB_Connect();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,CommentID,isFile,isReference) values (@imgsrc,@UserID,@FieldID,@CommentID,'1','0'); Select Scope_Identity() as ImgID";
                    DLdb.SQLST.Parameters.AddWithValue("imgsrc", filename);
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("FieldID", "");
                    DLdb.SQLST.Parameters.AddWithValue("CommentID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
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

                    Div1.InnerHtml += "<div class=\"col-md-3 img-thumbnail\" id=\"show_img_" + ImgID + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + ImgID + "','" + DLdb.Decrypt(Request.QueryString["id"].ToString()) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";

                    DLdb.DB_Close();
                }


            }
            //}
        }

        protected void btnUploads_Click(object sender, EventArgs e)
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
                    DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,CommentID,isFile,isReference) values (@imgsrc,@UserID,@FieldID,@CommentID,'1','1'); Select Scope_Identity() as ImgID";
                    DLdb.SQLST.Parameters.AddWithValue("imgsrc", filename);
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("FieldID", "");
                    DLdb.SQLST.Parameters.AddWithValue("CommentID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
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

                    CurrentMedia.InnerHtml += "<div class=\"col-md-3 img-thumbnail\" id=\"show_img_" + ImgID + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImagea('" + ImgID + "','" + DLdb.Decrypt(Request.QueryString["id"].ToString()) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";

                    DLdb.DB_Close();
                }


            }
            //}
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string CommentID = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update ReportQuestionList set complimentaryPoints=@complimentaryPoints,refixPointsNotComplete=@refixPointsNotComplete,isActive=@isActive,cautionaryPoints=@cautionaryPoints,refixPoints=@refixPoints,UserID = @UserID,Reference=@Reference,SubID=@SubID,Name = @Name, Comment = @Comment,TypeID = @TypeID,FieldID = @FieldID where ID = @ID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("Name", Name.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comment", Comment.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("FieldID", FormFields.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Reference", TextBox1.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("refixPointsNotComplete", refixPointsNotComplete.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("refixPoints", refixPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("cautionaryPoints", cautionaryPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("complimentaryPoints", complimentaryPoints.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("isActive", isActive.Checked ? 1 : 0);
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

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update formfields set label = @label,FormID=@FormID,SubID=@SubID where CommentTemplateID = @CommentTemplateID";
            DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("label", question.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("FormID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


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
            //        DLdb.SQLST2.CommandText = "INSERT INTO InspectorCommentTemplate (UserID,Name, Comment,TypeID,FieldID) VALUES (@UserID,@Name, @Comment,@TypeID,@FieldID)";
            //        DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
            //        DLdb.SQLST2.Parameters.AddWithValue("Name", Name.Text.ToString());
            //        DLdb.SQLST2.Parameters.AddWithValue("Comment", Comment.Text.ToString());
            //        DLdb.SQLST2.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            //        DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.Text.ToString());
            //        DLdb.SQLST2.CommandType = CommandType.Text;
            //        DLdb.SQLST2.Connection = DLdb.RS2;
            //        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            //        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            //        DLdb.SQLST2.Parameters.RemoveAt(0);
            //        DLdb.SQLST2.Parameters.RemoveAt(0);
            //        DLdb.SQLST2.Parameters.RemoveAt(0);
            //        DLdb.SQLST2.Parameters.RemoveAt(0);
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

        protected void btnUpload_Click1(object sender, EventArgs e)
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
                    DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,CommentID,isFile,isReference) values (@imgsrc,@UserID,@FieldID,@CommentID,'1','1'); Select Scope_Identity() as ImgID";
                    DLdb.SQLST.Parameters.AddWithValue("imgsrc", filename);
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("FieldID", "");
                    DLdb.SQLST.Parameters.AddWithValue("CommentID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
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

                    CurrentMedia.InnerHtml += "<div class=\"col-md-3 img-thumbnail\" id=\"show_img_" + ImgID + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + ImgID + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";

                    DLdb.DB_Close();
                }


            }
            //}
        }
    }
}