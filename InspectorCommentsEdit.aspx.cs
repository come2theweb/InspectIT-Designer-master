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
    public partial class InspectorCommentsEdit : System.Web.UI.Page
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
                // LOAD TYPE OF INSTALLATIONS
                TypeOfInstallation.Items.Clear();
                TypeOfInstallation.Items.Add(new ListItem("", ""));

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive = '1'";
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
                DLdb.RS.Close();

                string cid = DLdb.Decrypt(Request.QueryString["cid"].ToString());
                string useidi = Session["IIT_UID"].ToString();
                string commtempid = "";

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM InspectorCommentTemplate where commentid = @commentid";
                DLdb.SQLST.Parameters.AddWithValue("commentid", DLdb.Decrypt(Request.QueryString["cid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        Name.Text = theSqlDataReader["Name"].ToString();
                        Comment.Text = theSqlDataReader["Comment"].ToString();
                        string lTypeOfInstallation = theSqlDataReader["TypeID"].ToString();
                        TypeOfInstallation.SelectedIndex = TypeOfInstallation.Items.IndexOf(TypeOfInstallation.Items.FindByValue(lTypeOfInstallation));
                        commtempid = theSqlDataReader["CommentTemplateID"].ToString();

                        complimentaryPoints.Text = theSqlDataReader["complimentaryPoints"].ToString();
                        cautionaryPoints.Text = theSqlDataReader["cautionaryPoints"].ToString();
                        refixPointsNotComplete.Text = theSqlDataReader["refixPointsNotComplete"].ToString();
                        refixPoints.Text = theSqlDataReader["refixPoints"].ToString();
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

                            question.Text = theSqlDataReader["Question"].ToString();
                        //if (theSqlDataReader["FieldID"].ToString() == "" || theSqlDataReader["FieldID"] == DBNull.Value)
                        //{

                        //}
                        //else
                        //{

                        //    DLdb.RS2.Open();
                        //    DLdb.SQLST2.CommandText = "SELECT * FROM formfields where isActive = '1' and FieldID=@FieldID";
                        //    DLdb.SQLST2.Parameters.AddWithValue("FieldID", theSqlDataReader["FieldID"].ToString());
                        //    DLdb.SQLST2.CommandType = CommandType.Text;
                        //    DLdb.SQLST2.Connection = DLdb.RS2;
                        //    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        //    if (theSqlDataReader2.HasRows)
                        //    {
                        //        while (theSqlDataReader2.Read())
                        //        {
                        //            question.Text = theSqlDataReader2["label"].ToString();
                        //        }
                        //    }

                        //    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        //    DLdb.SQLST2.Parameters.RemoveAt(0);
                        //    DLdb.RS2.Close();

                        //}

                        string lFormFields = theSqlDataReader["FieldID"].ToString();
                        FormFields.SelectedIndex = FormFields.Items.IndexOf(FormFields.Items.FindByValue(lFormFields));
                        subTypes.SelectedValue = theSqlDataReader["SubID"].ToString();
                        TextBox1.Text = theSqlDataReader["Reference"].ToString();
                    }
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                // LOAD MEDIA
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from FormImg where CommentTemplateID = @CommentTemplateID and userid=@userid and isReference='0' and isActive='1'";
                DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", DLdb.Decrypt(Request.QueryString["cid"].ToString()));
                DLdb.SQLST.Parameters.AddWithValue("userid", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        string filename = theSqlDataReader["imgsrc"].ToString();
                        Div1.InnerHtml += "<div class=\"col-md-3 img-thumbnail\" id=\"show_img_" + theSqlDataReader["ImgID"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader["imgid"].ToString() + "','" + DLdb.Decrypt(Request.QueryString["cid"].ToString()) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from FormImg where CommentTemplateID = @CommentTemplateID and userid=@userid and isReference='1' and isActive='1'";
                DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", DLdb.Decrypt(Request.QueryString["cid"].ToString()));
                DLdb.SQLST.Parameters.AddWithValue("userid", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        string filename = theSqlDataReader["imgsrc"].ToString();
                        CurrentMedia.InnerHtml += "<div class=\"col-md-3 img-thumbnail\" id=\"show_img_" + theSqlDataReader["ImgID"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImagea('" + theSqlDataReader["imgid"].ToString() + "','" + DLdb.Decrypt(Request.QueryString["cid"].ToString()) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";

                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.DB_Close();
            }

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
                    FormFields.Items.Add(new ListItem(theSqlDataReader["Label"].ToString(), theSqlDataReader["FieldID"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
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
                    DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,CommentTemplateID) values (@imgsrc,@UserID,@FieldID,@tempID,'1',@CommentTemplateID); Select Scope_Identity() as ImgID";
                    DLdb.SQLST.Parameters.AddWithValue("imgsrc", filename);
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("tempID", "0");
                    DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", DLdb.Decrypt(Request.QueryString["cid"].ToString()));

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
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    Div1.InnerHtml += "<div class=\"col-md-3 img-thumbnail\" id=\"show_img_" + ImgID + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + ImgID + "','" + DLdb.Decrypt(Request.QueryString["cid"].ToString()) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" class=\"img img-responsive\" /></a></div>";

                    DLdb.DB_Close();
                }
            }
        }


        protected void TypeOfInstallation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            FormFields.Items.Clear();
            FormFields.Items.Add(new ListItem("", ""));

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
                    FormFields.Items.Add(new ListItem(theSqlDataReader["Label"].ToString(), theSqlDataReader["FieldID"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update InspectorCommentTemplate set UserID=@UserID,Name=@Name,Question=@Question,Reference=@Reference, Comment=@Comment,TypeID=@TypeID,FieldID = @FieldID where commentid=@commentid";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", DLdb.Decrypt(Request.QueryString["cid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("Name", Name.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Comment", Comment.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Question", question.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Reference", TextBox1.Text.ToString());
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

            DLdb.DB_Close();

            Response.Redirect("InspectorComments.aspx?msg=" + DLdb.Encrypt("Comment successfully edited"));
        }

        protected void Button1_Click(object sender, EventArgs e)
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

                    Global DLdb = new Global();
                    DLdb.DB_Connect();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,CommentTemplateID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','1',@CommentTemplateID); Select Scope_Identity() as ImgID";
                    DLdb.SQLST.Parameters.AddWithValue("imgsrc", filename);
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("FieldID", "");
                    DLdb.SQLST.Parameters.AddWithValue("tempID", "0");
                    DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", DLdb.Decrypt(Request.QueryString["cid"].ToString()));
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
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    CurrentMedia.InnerHtml += "<div class=\"col-md-3 img-thumbnail\" id=\"show_img_" + ImgID + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImagea('" + ImgID + "','" + DLdb.Decrypt(Request.QueryString["cid"].ToString()) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";

                    DLdb.DB_Close();
                }
            }
        }
    }
}