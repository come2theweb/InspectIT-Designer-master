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
    public partial class EditReview : System.Web.UI.Page
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

            //// ADMIN CHECK
            //if (Session["IIT_Role"].ToString() != "Inspector")
            //{
            //    Response.Redirect("Default");
            //}

            string commentTempID = "";
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

                // LOAD TEMPLATES
                ReviewTemplate.Items.Clear();
                ReviewTemplate.Items.Add(new ListItem("", ""));

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from InspectorCommentTemplate where UserID = @UserID and isActive = '1'";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        ReviewTemplate.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["CommentID"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                // LOAD THE REVIEW
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM COCReviews where COCStatementID = @COCStatementID and ReviewID = @ReviewID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["rid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["status"].ToString() == "Failure")
                        {
                            rdFail.Checked = true;
                        }
                        else if (theSqlDataReader["status"].ToString() == "Cautionary")
                        {
                            rdWarning.Checked = true;
                        }
                        else if (theSqlDataReader["status"].ToString() == "Compliment")
                        {
                            rdSuccess.Checked = true;
                        }

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

                        commentTempID = theSqlDataReader["commentTemplateID"].ToString();
                        Name.Text = theSqlDataReader["Name"].ToString();
                        Comment.Text = theSqlDataReader["Comment"].ToString();
                        Reference.Text = theSqlDataReader["Reference"].ToString();
                        subTypes.SelectedValue = theSqlDataReader["SubID"].ToString();
                        string lTypeOfInstallation = theSqlDataReader["TypeID"].ToString();
                        TypeOfInstallation.SelectedIndex = TypeOfInstallation.Items.IndexOf(TypeOfInstallation.Items.FindByValue(lTypeOfInstallation));

                        if (theSqlDataReader["FieldID"].ToString() == "" || theSqlDataReader["FieldID"] == DBNull.Value)
                        {
                            question.Text = theSqlDataReader["Question"].ToString();
                        }
                        else
                        {
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "SELECT * FROM formfields where isActive = '1' and FieldID=@FieldID";
                            DLdb.SQLST2.Parameters.AddWithValue("FieldID", theSqlDataReader["FieldID"].ToString());
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
                        }

                        string lFormFields = theSqlDataReader["FieldID"].ToString();
                        FormFields.SelectedIndex = FormFields.Items.IndexOf(FormFields.Items.FindByValue(lFormFields));

                        if (theSqlDataReader["CommentTemplateID"] != DBNull.Value)
                        {
                            string lCommentTemplateID = theSqlDataReader["CommentTemplateID"].ToString();
                            ReviewTemplate.SelectedIndex = ReviewTemplate.Items.IndexOf(ReviewTemplate.Items.FindByValue(lCommentTemplateID));
                        }

                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "Select * from FormImg where ReviewID = @ReviewID and isReference='1' and isActive='1'";
                        DLdb.SQLST2.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["rid"].ToString()));
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                string filename = theSqlDataReader2["imgsrc"].ToString(); 
                                Div1.InnerHtml += "<div class=\"col-md-3 img-thumbnail\"><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "Select * from FormImg where ReviewID = @ReviewID and isReference='0' and isActive='1'";
                        DLdb.SQLST2.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["rid"].ToString()));
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                string filename = theSqlDataReader2["imgsrc"].ToString(); 
                                CurrentMedia.InnerHtml += "<div class=\"col-md-3 img-thumbnail\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader2["imgid"].ToString() + "','" + Request.QueryString["cocid"] + "','" + Request.QueryString["rid"] + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                    }
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.DB_Close();
            }
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
                    DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,ReviewID,isReference) values (@imgsrc,@UserID,@FieldID,@tempID,'1',@ReviewID,'0'); Select Scope_Identity() as ImgID";
                    DLdb.SQLST.Parameters.AddWithValue("imgsrc", filename);
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("tempID", Session["IIT_tempID"]);
                    DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["rid"].ToString()));
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

                    CurrentMedia.InnerHtml += "<div class=\"col-md-3 img-thumbnail\" id=\"show_img_" + ImgID + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + ImgID + "','" + Request.QueryString["cocid"] + "','" + Request.QueryString["rid"] + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive\" /></a></div>";

                    DLdb.DB_Close();
                }
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string CommentID = "";
            string status = "";

            if (rdFail.Checked == true)
            {
                status = "Failure";
            }
            else if (rdWarning.Checked == true)
            {
                status = "Cautionary";
            }
            else if (rdSuccess.Checked == true)
            {
                status = "Compliment";
            }

            if (status != "")
            {
                if (img.HasFiles)
                {
                    foreach (HttpPostedFile File in img.PostedFiles)
                    {
                        string filename = File.FileName;
                        File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/assets/img/"), filename));
                    }
                }

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Update COCReviews set isactive='1',COCStatementID = @COCStatementID,question=@question,SubID=@SubID,UserID = @UserID,Name = @Name,Comment = @Comment,TypeID = @TypeID,status = @status where ReviewID = @ReviewID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("Name", Name.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Comment", Comment.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
                //DLdb.SQLST.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("status", status);
                // DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", ReviewTemplate.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["rid"].ToString()));
                //DLdb.SQLST.Parameters.AddWithValue("Reference", Reference.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("question", question.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("ReferenceImage", img.FileName);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
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
                DLdb.SQLST.CommandText = "update FormImg set ReviewID = @ReviewID, tempID = null where tempID =@tempID and UserID = @UserID";
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["rid"].ToString()));
                DLdb.SQLST.Parameters.AddWithValue("tempID", Session["IIT_tempID"]);
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

                Response.Redirect("EditCOCStatementInspector?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("Review updated successfully"));
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "Update COCReviews set isActive='0' where ReviewID = @ReviewID";
            //DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["rid"].ToString()));
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.RS.Close();

            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "update FormImg set isActive='0' where ReviewID = @ReviewID";
            //DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["rid"].ToString()));
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementInspector?cocid=" + Request.QueryString["cocid"]);
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

        protected void ReviewTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            // LOAD MEDIA
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormImg where CommentTemplateID = @CommentID";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", ReviewTemplate.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string filename = theSqlDataReader["imgsrc"].ToString(); ;
                    CurrentMedia.InnerHtml += "<div class=\"col-md-3\" id=\"show_img_" + theSqlDataReader["imgid"].ToString() + "\"><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" class=\"img img-responsive\" /></a></div>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM InspectorCommentTemplate where commentid = @commentid";
            DLdb.SQLST.Parameters.AddWithValue("commentid", ReviewTemplate.SelectedValue.ToString());
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

                    FormFields.Items.Clear();
                    FormFields.Items.Add(new ListItem("", ""));

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from FormLinks l inner join formfields f on l.FormID = f.FormID where TypeID = @TypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("TypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            FormFields.Items.Add(new ListItem(theSqlDataReader2["Label"].ToString(), theSqlDataReader2["FieldID"].ToString()));
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string lFormFields = theSqlDataReader["FieldID"].ToString();
                    FormFields.SelectedIndex = FormFields.Items.IndexOf(FormFields.Items.FindByValue(lFormFields));
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        protected void btnRefixComplete_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            // LOAD MEDIA
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCReviews set isClosed = '1' where ReviewID = @ReviewID";
            DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["rid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCReviews where COCStatementID = @COCStatementID and isfixed = '0'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "Update COCStatements set isRefix = '1' where COCStatementID = @COCStatementID";
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();

                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "insert into COCRefixes (UserID,COCStatementID,CompletionDate,isFixed) values (@UserID,@COCStatementID,@CompletionDate,'0')";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST2.Parameters.AddWithValue("CompletionDate", DateTime.Now.ToString("dd/MM/yyyy"));
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            else
            {
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "Update COCStatements set isRefix = '0' where COCStatementID = @COCStatementID";
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementInspector?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("Review maked as fixed"));
        }
    }
}