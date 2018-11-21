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
    public partial class AddReview : System.Web.UI.Page
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
                TypeOfInstallation.Items.Clear();
                TypeOfInstallation.Items.Add(new ListItem("", ""));
                ReviewTemplate.Items.Clear();
                ReviewTemplate.Items.Add(new ListItem("", ""));

                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "SELECT * FROM COCInstallations where isActive = '1' and COCStatementID=@COCStatementID";
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.HasRows)
                {
                    while (theSqlDataReader2.Read())
                    {
                        DLdb.RS.Open();
                        DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive = '1' and InstallationTypeID=@InstallationTypeID";
                        DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                        DLdb.SQLST.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader2["TypeID"].ToString());
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
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.RS.Close();

                        DLdb.RS.Open();
                        DLdb.SQLST.CommandText = "select * from InspectorCommentTemplate where UserID = @UserID and isActive = '1' and TypeID=@TypeID";
                        DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                        DLdb.SQLST.Parameters.AddWithValue("TypeID", theSqlDataReader2["TypeID"].ToString());
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
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.RS.Close();
                    }
                }

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();

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
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AuditorImgs/"), filename));

                    // SAVE IMAGE WITH TEMPID
                    Global DLdb = new Global();
                    DLdb.DB_Connect();

                    string ImgID = "";
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile) values (@imgsrc,@UserID,@FieldID,@tempID,'1'); Select Scope_Identity() as ImgID";
                    DLdb.SQLST.Parameters.AddWithValue("imgsrc", filename);
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("tempID", Session["IIT_tempID"]);
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

                    CurrentMedia.InnerHtml += "<div class=\"col-md-3\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImagea('" + ImgID + "','" + DLdb.Decrypt(Request.QueryString["cocid"]) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"AuditorImgs/" + filename + "\" class=\"img-thumbnail img img-responsive\" /></a></div>";

                    DLdb.DB_Close();
                }


            }
            //}
        }

        protected void btnUploads_Click(object sender, EventArgs e)
        {
            //if (tempID != "0")
            //{
            if (img.HasFiles)
            {
                foreach (HttpPostedFile File in img.PostedFiles)
                {
                    string filename = File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AuditorImgs/"), filename));

                    // SAVE IMAGE WITH TEMPID
                    Global DLdb = new Global();
                    DLdb.DB_Connect();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile) values (@imgsrc,@UserID,@FieldID,@tempID,'1')";
                    DLdb.SQLST.Parameters.AddWithValue("imgsrc", filename);
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("tempID", "0");
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    Div1.InnerHtml += "<div class=\"col-md-3\"><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"AuditorImgs/" + filename + "\" class=\"img-thumbnail img img-responsive\" /></a></div>";

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
            string status = "";
            string comment = "";

            if (rdFail.Checked == true)
            {
                status = "Failure";
                comment = Comment.Text.ToString();
            }
            else if (rdWarning.Checked == true)
            {
                status = "Cautionary";
                comment = Comment.Text.ToString();
            }
            else if (rdSuccess.Checked == true)
            {
                status = "Compliment";
                comment = TextBox1.Text.ToString();
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
                DLdb.SQLST.CommandText = "INSERT INTO COCReviews (COCStatementID,UserID,Name,Reference,ReferenceImage,SubID, Comment,TypeID,question,status,CommentTemplateID) VALUES (@COCStatementID,@UserID,@Name,@Reference,@ReferenceImage,@SubID,@Comment,@TypeID,@question,@status,@CommentTemplateID); Select Scope_Identity() as CommentID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("Name", Name.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Comment", comment.ToString());
                DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("question", question.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("status", status);
                DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", ReviewTemplate.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Reference", Reference.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("ReferenceImage", img.FileName);
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
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentTemplateID=@CommentTemplateID and isreference='0'";
                DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", ReviewTemplate.SelectedValue.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        //DLdb.RS2.Open();
                        //DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,ReviewID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','0',@ReviewID)";
                        //DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                        //DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                        //DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                        //DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                        //DLdb.SQLST2.Parameters.AddWithValue("ReviewID", CommentID);
                        //DLdb.SQLST2.CommandType = CommandType.Text;
                        //DLdb.SQLST2.Connection = DLdb.RS2;
                        //SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        //if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        //DLdb.SQLST2.Parameters.RemoveAt(0);
                        //DLdb.SQLST2.Parameters.RemoveAt(0);
                        //DLdb.SQLST2.Parameters.RemoveAt(0);
                        //DLdb.SQLST2.Parameters.RemoveAt(0);
                        //DLdb.SQLST2.Parameters.RemoveAt(0);
                        //DLdb.RS2.Close();
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentTemplateID=@CommentTemplateID and isreference='1'";
                DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", ReviewTemplate.SelectedValue.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        //DLdb.RS2.Open();
                        //DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,ReviewID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','1',@ReviewID)";
                        //DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                        //DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                        //DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                        //DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                        //DLdb.SQLST2.Parameters.AddWithValue("ReviewID", CommentID);
                        //DLdb.SQLST2.CommandType = CommandType.Text;
                        //DLdb.SQLST2.Connection = DLdb.RS2;
                        //SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        //if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        //DLdb.SQLST2.Parameters.RemoveAt(0);
                        //DLdb.SQLST2.Parameters.RemoveAt(0);
                        //DLdb.SQLST2.Parameters.RemoveAt(0);
                        //DLdb.SQLST2.Parameters.RemoveAt(0);
                        //DLdb.SQLST2.Parameters.RemoveAt(0);
                        //DLdb.RS2.Close();
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                // ADD MEDIA COMMENTTEMPLATEID
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update FormImg set ReviewID = @ReviewID, tempID = null where tempID = @tempid and UserID = @UserID";
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", CommentID);
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

                string plumid = "";
                string plumemail = "";
                string plumname = "";
                string plumnumber = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCStatements where cocstatementid=@cocstatementid";
                DLdb.SQLST.Parameters.AddWithValue("cocstatementid", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    plumid = theSqlDataReader["UserID"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", plumid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    plumemail = theSqlDataReader["email"].ToString();
                    plumnumber = theSqlDataReader["contact"].ToString();
                    plumname = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DateTime now = DateTime.Now;

                string newPerformanceStatusID = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into PerformanceStatus (UserID,Date,PerformanceType,PerformancePointAllocation,Details) values (UserID,Date,PerformanceType,PerformancePointAllocation,Details); select scope_identity() as PerformanceStatusID;";
                DLdb.SQLST.Parameters.AddWithValue("UserID", plumid.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Date", now.ToString("MM/dd/yyyy"));
                DLdb.SQLST.Parameters.AddWithValue("PerformanceType", status.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PerformancePointAllocation", pointAllocation.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Details", CommentID.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    newPerformanceStatusID = theSqlDataReader["PerformanceStatusID"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string designation = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM PlumberDesignations where isActive = '1' and PlumberID=@PlumberID";
                DLdb.SQLST.Parameters.AddWithValue("PlumberID", plumid.ToString());
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
                DLdb.SQLST.CommandText = "SELECT * FROM Weighting where isActive = '1' and WeightingID='3'";
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

                decimal totalPointsAssigned = weightingPercentage / 100 * Convert.ToDecimal(pointAllocation.Text);
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into  AssignedWeighting (UserID,WeightingID,Points,PerformanceType,PerformancePointAllocation,PerformanceStatusID,Type,weightingValue,TypePlumber) values (@UserID,@WeightingID,@Points,@PerformanceType,@PerformancePointAllocation,@PerformanceStatusID,'Performance',@weightingValue,@TypePlumber)";
                DLdb.SQLST.Parameters.AddWithValue("UserID", DLdb.Decrypt(Request.QueryString["UserID"].ToString()));
                DLdb.SQLST.Parameters.AddWithValue("WeightingID", weightingID.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Points", totalPointsAssigned.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PerformanceType", status.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PerformancePointAllocation", pointAllocation.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PerformanceStatusID", newPerformanceStatusID.ToString());
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
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                if (status == "Failure")
                {
                    //REQUIRED: SMS PLUMBER

                    // SET TO REFIX
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Update COCStatements set isRefix = '1' where COCStatementID = @COCStatementID";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    //string HTMLSubject = "Inspect IT - C.O.C Refix Required";
                    //string HTMLBody = "Dear " + plumname.ToString() + "<br /><br />COC Number " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " has been audited and a refix is required.<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Team";
                    //DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumemail.ToString(), "");

                    //DLdb.sendSMS(plumid.ToString(), plumnumber.ToString(), "COC Number " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " has been audited and a refix is required.");
                }

                DLdb.DB_Close();

                Response.Redirect("EditCOCStatementInspector?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("Review added successfully"));
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
                    //FormFields.Items.Add(new ListItem(theSqlDataReader["Label"].ToString(), theSqlDataReader["FieldID"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            FormFields.Visible = true;
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

        protected void subTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            FormFields.Items.Clear();
            FormFields.Items.Add(new ListItem("", ""));

            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "select * from FormLinks l inner join formfields f on l.FormID = f.FormID where TypeID = @TypeID and f.SubID=@SubID";
            //DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            //DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //if (theSqlDataReader.HasRows)
            //{
            //    while (theSqlDataReader.Read())
            //    {
            //        FormFields.Items.Add(new ListItem(theSqlDataReader["Label"].ToString(), theSqlDataReader["FieldID"].ToString()));
            //    }
            //}

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.RS.Close();

            //reviewDropdown.Items.Clear();
            //reviewDropdown.Items.Add(new ListItem("", ""));
            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "select * from ReportQuestionList where TypeID = @TypeID and SubID=@SubID";
            //DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
            //DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //if (theSqlDataReader.HasRows)
            //{
            //    while (theSqlDataReader.Read())
            //    {
            //        reviewDropdown.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["ID"].ToString()));
            //    }
            //}

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from formfields where SubID = @SubID";
            DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    FormFields.Items.Add(new ListItem(theSqlDataReader["Label"].ToString(), theSqlDataReader["CommentTemplateID"].ToString()));
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

            question.Visible = true;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update FormImg set isActive='0' where TempID = @TempID";
            DLdb.SQLST.Parameters.AddWithValue("TempID", Session["IIT_tempID"]);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // LOAD MEDIA
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormImg where CommentTemplateID = @CommentID and UserID=@UserID and isReference='1' and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", ReviewTemplate.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    //string filename = theSqlDataReader["imgsrc"].ToString(); ;

                    //DLdb.RS2.Open();
                    //DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,ReviewID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','1',@ReviewID)";
                    //DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                    //DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    //DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                    //DLdb.SQLST2.Parameters.AddWithValue("tempID", Session["IIT_tempID"]);
                    //DLdb.SQLST2.Parameters.AddWithValue("ReviewID", "");
                    //DLdb.SQLST2.CommandType = CommandType.Text;
                    //DLdb.SQLST2.Connection = DLdb.RS2;
                    //SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    //if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    //DLdb.SQLST2.Parameters.RemoveAt(0);
                    //DLdb.SQLST2.Parameters.RemoveAt(0);
                    //DLdb.SQLST2.Parameters.RemoveAt(0);
                    //DLdb.SQLST2.Parameters.RemoveAt(0);
                    //DLdb.SQLST2.Parameters.RemoveAt(0);
                    //DLdb.RS2.Close();
                    //Div1.InnerHtml += "<div class=\"col-md-3 img-thumnail\"><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"AuditorImgs/" + filename + "\" class=\"img-thumbnail img img-responsive\" /></a></div>";

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormImg where CommentTemplateID = @CommentID and UserID=@UserID and isReference='0' and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", ReviewTemplate.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    //string filename = theSqlDataReader["imgsrc"].ToString(); ;
                    //string iid = "";

                    //DLdb.RS2.Open();
                    //DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,ReviewID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','0',@ReviewID); select scope_identity() as iid";
                    //DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                    //DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    //DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                    //DLdb.SQLST2.Parameters.AddWithValue("tempID", Session["IIT_tempID"]);
                    //DLdb.SQLST2.Parameters.AddWithValue("ReviewID", "");
                    //DLdb.SQLST2.CommandType = CommandType.Text;
                    //DLdb.SQLST2.Connection = DLdb.RS2;
                    //SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    //if (theSqlDataReader2.HasRows)
                    //{
                    //    theSqlDataReader2.Read();
                    //    iid = theSqlDataReader2["iid"].ToString();
                    //}
                    //if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    //DLdb.SQLST2.Parameters.RemoveAt(0);
                    //DLdb.SQLST2.Parameters.RemoveAt(0);
                    //DLdb.SQLST2.Parameters.RemoveAt(0);
                    //DLdb.SQLST2.Parameters.RemoveAt(0);
                    //DLdb.SQLST2.Parameters.RemoveAt(0);
                    //DLdb.RS2.Close();

                    //CurrentMedia.InnerHtml += "<div class=\"col-md-3 img-thumnail\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImagea('" + iid + "','" + DLdb.Decrypt(Request.QueryString["cocid"]) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"AuditorImgs/" + filename + "\" class=\"img-thumbnail img img-responsive\" /></a></div>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
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
                    Reference.Text = theSqlDataReader["Reference"].ToString();
                    string lTypeOfInstallation = theSqlDataReader["TypeID"].ToString();
                    TypeOfInstallation.SelectedIndex = TypeOfInstallation.Items.IndexOf(TypeOfInstallation.Items.FindByValue(lTypeOfInstallation));
                    if (rdFail.Checked)
                    {
                        pointAllocation.Text = theSqlDataReader["refixPoints"].ToString();
                    }
                    else if (rdWarning.Checked)
                    {
                        pointAllocation.Text = theSqlDataReader["cautionaryPoints"].ToString();
                    }
                    else if (rdSuccess.Checked)
                    {
                        pointAllocation.Text = theSqlDataReader["complimentaryPoints"].ToString();
                    }
                    
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

                    subTypes.Items.Clear();
                    subTypes.Items.Add(new ListItem("", ""));

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from InstallationTypessub where isActive = '1' and InstallationTypeID=@InstallationTypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            subTypes.Items.Add(new ListItem(theSqlDataReader2["Name"].ToString(), theSqlDataReader2["SubID"].ToString()));
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

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
                    subTypes.SelectedValue = theSqlDataReader["SubID"].ToString();
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

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
                DLdb.SQLST.CommandText = "INSERT INTO COCReviews (COCStatementID,UserID,Name,Reference,ReferenceImage,SubID, Comment,TypeID,question,status,CommentTemplateID) VALUES (@COCStatementID,@UserID,@Name,@Reference,@ReferenceImage,@SubID,@Comment,@TypeID,@question,@status,@CommentTemplateID); Select Scope_Identity() as CommentID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("Name", Name.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Comment", Comment.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("question", question.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("status", status);
                DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", ReviewTemplate.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Reference", Reference.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("ReferenceImage", img.FileName);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentTemplateID=@CommentTemplateID and isreference='0'";
                DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", ReviewTemplate.SelectedValue.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,ReviewID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','0',@ReviewID)";
                        DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                        DLdb.SQLST2.Parameters.AddWithValue("ReviewID", CommentID);
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
                DLdb.SQLST.CommandText = "SELECT * FROM formimg where isActive = '1' and CommentTemplateID=@CommentTemplateID and isreference='1'";
                DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", ReviewTemplate.SelectedValue.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,ReviewID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','1',@ReviewID)";
                        DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                        DLdb.SQLST2.Parameters.AddWithValue("ReviewID", CommentID);
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
                DLdb.SQLST.CommandText = "update FormImg set ReviewID = @ReviewID, tempID = null where tempID = @tempid and UserID = @UserID";
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", CommentID);
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

                string plumid = "";
                string plumemail = "";
                string plumname = "";
                string plumnumber = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCStatements where cocstatementid=@cocstatementid";
                DLdb.SQLST.Parameters.AddWithValue("cocstatementid", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    plumid = theSqlDataReader["UserID"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", plumid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    plumemail = theSqlDataReader["email"].ToString();
                    plumnumber = theSqlDataReader["contact"].ToString();
                    plumname = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string auditorName = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    auditorName = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DateTime now = DateTime.Now;

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into PerformanceStatus (UserID,Date,PerformanceType,PerformancePointAllocation,Details) values (UserID,Date,PerformanceType,PerformancePointAllocation,Details)";
                DLdb.SQLST.Parameters.AddWithValue("UserID", plumid.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Date", now.ToString("MM/dd/yyyy"));
                DLdb.SQLST.Parameters.AddWithValue("PerformanceType", status.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PerformancePointAllocation", pointAllocation.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Details", CommentID.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                if (status == "Failure")
                {
                    //REQUIRED: SMS PLUMBER

                    // SET TO REFIX
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Update COCStatements set isRefix = '1' where COCStatementID = @COCStatementID";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                    DLdb.SQLST.Parameters.AddWithValue("Message", "Certificate has been marked for a refix");
                    DLdb.SQLST.Parameters.AddWithValue("Username", auditorName.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
                    DLdb.SQLST.Parameters.AddWithValue("CertificateID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    //string HTMLSubject = "Inspect IT - C.O.C Refix Required";
                    //string HTMLBody = "Dear " + plumname.ToString() + "<br /><br />COC Number " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " has been audited and a refix is required.<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Team";
                    //DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumemail.ToString(), "");

                    //DLdb.sendSMS(plumid.ToString(), plumnumber.ToString(), "COC Number " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " has been audited and a refix is required.");
                }

                Response.Redirect("EditReview?cocid=" + Request.QueryString["cocid"] + "&rid=" + DLdb.Encrypt(CommentID));
            }
            DLdb.DB_Close();
        }

        protected void FormFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            string val = FormFields.SelectedValue.ToString();
            Global DLdb = new Global();
            DLdb.DB_Connect();


            CurrentMedia.InnerHtml = "";
            Div1.InnerHtml = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from ReportQuestionList where ID = @ID";
            DLdb.SQLST.Parameters.AddWithValue("ID", val.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Comment.Text = theSqlDataReader["Comment"].ToString();
                    Reference.Text = theSqlDataReader["Reference"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormImg where CommentID = @CommentID and isReference='1' and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", val.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string filename = theSqlDataReader["imgsrc"].ToString(); ;

                    Div1.InnerHtml += "<div class=\"col-md-3 img-thumnail\"><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"AuditorImgs/" + filename + "\" class=\"img-thumbnail img img-responsive\" /></a></div>";

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormImg where CommentID = @CommentID and isReference='0' and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", val.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string filename = theSqlDataReader["imgsrc"].ToString(); ;
                    string iid = "";

                    CurrentMedia.InnerHtml += "<div class=\"col-md-3 img-thumnail\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImagea('" + iid + "','" + DLdb.Decrypt(Request.QueryString["cocid"]) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"AuditorImgs/" + filename + "\" class=\"img-thumbnail img img-responsive\" /></a></div>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                DLdb.SQLST.CommandText = "INSERT INTO COCReviews (COCStatementID,UserID,Name,Reference,ReferenceImage,SubID, Comment,TypeID,question,status,CommentTemplateID) VALUES (@COCStatementID,@UserID,@Name,@Reference,@ReferenceImage,@SubID,@Comment,@TypeID,@question,@status,@CommentTemplateID); Select Scope_Identity() as CommentID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("Name", Name.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Comment", Comment.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("TypeID", TypeOfInstallation.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SubID", subTypes.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("question", FormFields.SelectedItem.ToString());
                DLdb.SQLST.Parameters.AddWithValue("status", status);
                DLdb.SQLST.Parameters.AddWithValue("CommentTemplateID", "0");
                DLdb.SQLST.Parameters.AddWithValue("Reference", Reference.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("ReferenceImage", img.FileName);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
                        DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,ReviewID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','0',@ReviewID)";
                        DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                        DLdb.SQLST2.Parameters.AddWithValue("ReviewID", CommentID);
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
                        DLdb.SQLST2.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,isReference,ReviewID) values (@imgsrc,@UserID,@FieldID,@tempID,'1','1',@ReviewID)";
                        DLdb.SQLST2.Parameters.AddWithValue("imgsrc", theSqlDataReader["imgsrc"].ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("FieldID", FormFields.SelectedValue.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("tempID", "");
                        DLdb.SQLST2.Parameters.AddWithValue("ReviewID", CommentID);
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
                DLdb.SQLST.CommandText = "update FormImg set ReviewID = @ReviewID, tempID = null where tempID = @tempid and UserID = @UserID";
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", CommentID);
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

                string plumid = "";
                string plumemail = "";
                string plumname = "";
                string plumnumber = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCStatements where cocstatementid=@cocstatementid";
                DLdb.SQLST.Parameters.AddWithValue("cocstatementid", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    plumid = theSqlDataReader["UserID"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", plumid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    plumemail = theSqlDataReader["email"].ToString();
                    plumnumber = theSqlDataReader["contact"].ToString();
                    plumname = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                DateTime now = DateTime.Now;

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into PerformanceStatus (UserID,Date,PerformanceType,PerformancePointAllocation,Details) values (UserID,Date,PerformanceType,PerformancePointAllocation,Details)";
                DLdb.SQLST.Parameters.AddWithValue("UserID", plumid.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Date", now.ToString("MM/dd/yyyy"));
                DLdb.SQLST.Parameters.AddWithValue("PerformanceType", status.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PerformancePointAllocation", pointAllocation.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Details", CommentID.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string auditorName = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    auditorName = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                if (status == "Failure")
                {
                    //REQUIRED: SMS PLUMBER

                    // SET TO REFIX
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Update COCStatements set isRefix = '1' where COCStatementID = @COCStatementID";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                    DLdb.SQLST.Parameters.AddWithValue("Message", "Certificate has been marked for a refix");
                    DLdb.SQLST.Parameters.AddWithValue("Username", auditorName.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
                    DLdb.SQLST.Parameters.AddWithValue("CertificateID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    //string HTMLSubject = "Inspect IT - C.O.C Refix Required";
                    //string HTMLBody = "Dear " + plumname.ToString() + "<br /><br />COC Number " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " has been audited and a refix is required.<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Team";
                    //DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumemail.ToString(), "");

                    //DLdb.sendSMS(plumid.ToString(), plumnumber.ToString(), "COC Number " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " has been audited and a refix is required.");
                }


                Response.Redirect("EditReview?cocid=" + Request.QueryString["cocid"] + "&rid=" + DLdb.Encrypt(CommentID));
            }

            DLdb.DB_Close();
        }
         
        protected void rdFail_CheckedChanged1(object sender, EventArgs e)
        {
            if (rdFail.Checked || rdWarning.Checked)
            {
                hidePanel.Visible = true;
                commentPanel.Visible = false;
            }
            else if (rdSuccess.Checked)
            {
                commentPanel.Visible = true;
                hidePanel.Visible = false;
            }
            else
            {
                hidePanel.Visible = false;
                commentPanel.Visible = false;
            }
        }
    }
}