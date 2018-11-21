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

namespace InspectIT
{
    public partial class NewPlumberRegistrationUpdateAdmin : System.Web.UI.Page
    {
        public string userID = "";
        public string userIDaa = "";
        public string ApplicationID = "";
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
            string newAppID = "";
            string ComPID = "";
            if (!IsPostBack)
            {
                if (Request.QueryString["npid"] != null)
                {
                    ApplicationID = DLdb.Decrypt(Request.QueryString["npid"].ToString());
                    newAppID = DLdb.Decrypt(Request.QueryString["npid"].ToString());

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from NewApplicationsUpdate where ApplicationID=@ApplicationID";
                    DLdb.SQLST.Parameters.AddWithValue("ApplicationID", newAppID.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            userIDaa = theSqlDataReader["UserID"].ToString();
                            userID = theSqlDataReader["UserID"].ToString();
                            if (theSqlDataReader["ProcedureRegistration"].ToString() == "True")
                            {
                                CheckBox1.Checked = true;
                            }
                            if (theSqlDataReader["CodeConduct"].ToString() == "True")
                            {
                                CheckBox2.Checked = true;
                            }
                            if (theSqlDataReader["Acknowledgement"].ToString() == "True")
                            {
                                CheckBox3.Checked = true;
                            }
                            if (theSqlDataReader["Declaration"].ToString() == "True")
                            {
                                CheckBox4.Checked = true;
                            }
                            if (theSqlDataReader["isApproved"].ToString() == "True")
                            {
                                isApproved.Checked = true;
                            }
                            if (theSqlDataReader["isRejected"].ToString() == "True")
                            {
                                isRejected.Checked = true;
                            }

                            if (theSqlDataReader["QualificationVerified"].ToString() == "True")
                            {
                                QualificationVerified.Checked = true;
                            }

                            if (theSqlDataReader["ProofExperience"].ToString() == "True")
                            {
                                ProofExperience.Checked = true;
                            }

                            if (theSqlDataReader["AssessmentComplete"].ToString() == "True")
                            {
                                AssessmentComplete.Checked = true;
                            }
                            if (theSqlDataReader["PaymentRecieved"].ToString() == "True")
                            {
                                PaymentRecieved.Checked = true;
                            }
                            if (theSqlDataReader["Declaration"].ToString() == "True")
                            {
                                Declaration.Checked = true;
                            }
                            DropDownList1.SelectedValue = theSqlDataReader["RegistrationCard"].ToString();
                            DropDownList2.SelectedValue = theSqlDataReader["DeliveryMethod"].ToString();
                            declareName.Text = theSqlDataReader["DeclarationName"].ToString();
                            declareIDnum.Text = theSqlDataReader["DeclarationIDNumber"].ToString();

                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID";
                            DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                while (theSqlDataReader2.Read())
                                {
                                    title.Text = theSqlDataReader2["Title"].ToString();
                                    Name.Text = theSqlDataReader2["fname"].ToString();
                                    Surname.Text = theSqlDataReader2["lname"].ToString();
                                    ComPID = theSqlDataReader2["company"].ToString();
                                }
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    if (QualificationVerified.Checked && ProofExperience.Checked && Declaration.Checked && PaymentRecieved.Checked && AssessmentComplete.Checked)
                    {
                        isApproved.Enabled = true;
                        isRejected.Enabled = true;
                    }
                    else
                    {
                        isApproved.Enabled = false;
                        isRejected.Enabled = false;
                    }

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from PlumberQualifications where PlumberID=@PlumberID";
                    DLdb.SQLST.Parameters.AddWithValue("PlumberID", userID);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            userQualifications.InnerHtml += "<tr>" +
                                "<td>" + theSqlDataReader["CertificationNo"].ToString() + "</td>" +
                                "<td>" + theSqlDataReader["CourseYear"].ToString() + "</td>" +
                                "<td>" + theSqlDataReader["QualifiedThrough"].ToString() + "</td>" +
                                "<td>" + theSqlDataReader["TrainingProvider"].ToString() + "</td>" +
                                "<td></td>" +
                                "</tr>";
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from NewApplicationComments where UserID=@UserID and isActive='1'";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", userIDaa.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            commsHis.InnerHtml += "<p>" + theSqlDataReader["Comment"].ToString() + "</p><hr/>";
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from NewApplicationCertificates where UserID=@UserID and isActive='1'";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", userIDaa.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            attachs.InnerHtml += "<a href='Assessments/" + theSqlDataReader["Certificate"].ToString() + "' taregt='_blank'>" + theSqlDataReader["Certificate"].ToString() + "</a>";
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                    ERRMSGsub.Visible = false;

                    routeQualification.Items.Clear();
                    routeQualification.Items.Add(new ListItem("", ""));

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from QualificationRoute where isActive = '1' order by Route";
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            routeQualification.Items.Add(new ListItem(theSqlDataReader["Route"].ToString(), theSqlDataReader["QualificationID"].ToString()));
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from PlumberDesignations where PlumberID = @PlumberID";
                    DLdb.SQLST.Parameters.AddWithValue("PlumberID", userID.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            if (theSqlDataReader["Designation"].ToString() == "Director Plumber")
                            {
                                DirectorPlumber.Checked = true;
                            }
                            else if (theSqlDataReader["Designation"].ToString() == "Master Plumber")
                            {
                                MasterPlumber.Checked = true;
                            }
                            else if (theSqlDataReader["Designation"].ToString() == "Licensed Plumber")
                            {
                                LicensedPlumber.Checked = true;
                            }
                            else if (theSqlDataReader["Designation"].ToString() == "Technical Operator Practitioner")
                            {
                                TechnicalOperatorPractitioner.Checked = true;
                            }
                            else if (theSqlDataReader["Designation"].ToString() == "Technical Assistant Practitioner")
                            {
                                TechnicalAssistantPractitioner.Checked = true;
                            }
                            else if (theSqlDataReader["Designation"].ToString() == "Learner Plumber")
                            {
                                LearnerPlumber.Checked = true;
                            }
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from PlumberSpecialisations where PlumberID = @PlumberID";
                    DLdb.SQLST.Parameters.AddWithValue("PlumberID", userID.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            if (theSqlDataReader["Specialisation"].ToString() == "Training Assesor")
                            {
                                MasterPlumberTrainingAssesor.Checked = true;
                            }
                            else if (theSqlDataReader["Specialisation"].ToString() == "Estimator")
                            {
                                MasterPlumberEstimator.Checked = true;
                            }
                            else if (theSqlDataReader["Specialisation"].ToString() == "Arbitrator")
                            {
                                MasterPlumberArbitrator.Checked = true;
                            }
                            else if (theSqlDataReader["Specialisation"].ToString() == "Solar")
                            {
                                LicensedPlumberSolar.Checked = true;
                            }
                            else if (theSqlDataReader["Specialisation"].ToString() == "Heat Pump")
                            {
                                LicensedPlumberHeatPump.Checked = true;
                            }
                            else if (theSqlDataReader["Specialisation"].ToString() == "Gas")
                            {
                                LicensedPlumberGas.Checked = true;
                            }
                            else if (theSqlDataReader["Specialisation"].ToString() == "Drainage")
                            {
                                TechnicalOperatorPractitionerDrainage.Checked = true;
                            }
                            else if (theSqlDataReader["Specialisation"].ToString() == "Cold Water")
                            {
                                TechnicalOperatorPractitionerColdWater.Checked = true;
                            }
                            else if (theSqlDataReader["Specialisation"].ToString() == "Hot Water")
                            {
                                TechnicalOperatorPractitionerHotWater.Checked = true;
                            }
                            else if (theSqlDataReader["Specialisation"].ToString() == "Water Energy Efficiency")
                            {
                                TechnicalOperatorPractitionerWaterEnergyEfficiency.Checked = true;
                            }
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }

            }
            DLdb.DB_Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into NewApplicationComments (UserID,ApplicationID,Comment,isUpdate) values (@UserID,@ApplicationID,@Comment,'1')";
            DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("UserID", userID);
            DLdb.SQLST.Parameters.AddWithValue("Comment", TextBox3.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            TextBox3.Text = "";
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        
        protected void Button3_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (FileUpload4.HasFiles)
            {
                foreach (HttpPostedFile File in FileUpload4.PostedFiles)
                {
                    string filename = File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Assessments/"), filename));
                }
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into NewApplicationCertificates (UserID,ApplicationID,Certificate,isUpdate) values (@UserID,@ApplicationID,@Certificate,'1')";
            DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("UserID", userID);
            DLdb.SQLST.Parameters.AddWithValue("Certificate", FileUpload4.FileName);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void saveAppDetails_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update NewApplicationsUpdate set QualificationVerified=@QualificationVerified,AssessmentComplete=@AssessmentComplete," +
                "ProofExperience=@ProofExperience,PaymentRecieved=@PaymentRecieved,isApproved=@isApproved,isRejected=@isRejected where ApplicationID=@ApplicationID";
            DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("QualificationVerified", QualificationVerified.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("ProofExperience", ProofExperience.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("PaymentRecieved", PaymentRecieved.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("isApproved", isApproved.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("isRejected", isRejected.Checked ? 1 : 0);
            DLdb.SQLST.Parameters.AddWithValue("AssessmentComplete", AssessmentComplete.Checked ? 1 : 0);
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
            DLdb.RS.Close();

            string plumberName = "";
            string plumberEmail = "";
            string plumberContact = "";
            string UserID = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from NewApplicationsUpdate where ApplicationID=@ApplicationID";
            DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "select * from Users where UserID=@UserID";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                if (theSqlDataReader2.HasRows)
                {
                    theSqlDataReader2.Read();
                    plumberName = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                    plumberEmail = theSqlDataReader2["email"].ToString();
                    plumberContact = theSqlDataReader2["contact"].ToString();
                    UserID = theSqlDataReader2["UserID"].ToString();
                }
                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            if (isRejected.Checked)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update NewApplicationsUpdate set isActive='0' where ApplicationID=@ApplicationID";
                DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "delete from PlumberDesignations where ApplicationID=@ApplicationID";
                DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "delete from PlumberSpecialisations where ApplicationID=@ApplicationID";
                DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string HTMLSubject = "PIRB UPDATE REGISTRATION ";
                string HTMLBody = "Good Day " + plumberName +
                    "<br/>" +
                    "Thank you for updating your profile with the Plumbing Industry Registration Board.  Your application has unfortunately been rejected" +
                    "<br/><br/>" +
                    "For further information as to why your application was rejected, please contact the PIRB on 0861 747 275 or email info@pirb.co.za." +
                      "<br/><br/>" +
                    "Best Regards" +
                    "<br/><br/>" +
                    "Lea Smith" +
                    "<br/>" +
                    "Chairman od the PIRB";
                DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumberEmail.ToString(), "");

                DLdb.sendSMS(UserID.ToString(), plumberContact.ToString(), "Thank you for update with the PIRB.  Unfortunately, your application has been rejected.  Please contact the PIRB for further details.");


            }

            if (isApproved.Checked)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from PlumberDesignations where PlumberID=@PlumberID";
                DLdb.SQLST.Parameters.AddWithValue("PlumberID", UserID.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM badges where Item=@Item";
                        DLdb.SQLST2.Parameters.AddWithValue("Item", theSqlDataReader["Designation"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                DLdb.RS3.Open();
                                DLdb.SQLST3.CommandText = "SELECT * FROM AssignedBadges where BadgeID=@BadgeID and UserID=@UserID";
                                DLdb.SQLST3.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("UserID", UserID.ToString());
                                DLdb.SQLST3.CommandType = CommandType.Text;
                                DLdb.SQLST3.Connection = DLdb.RS3;
                                SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                                if (theSqlDataReader3.HasRows)
                                {
                                    while (theSqlDataReader3.Read())
                                    {

                                    }
                                }
                                else
                                {
                                    DLdb.RS4.Open();
                                    DLdb.SQLST4.CommandText = "insert into AssignedBadges (UserID,BadgeID) values (@UserID,@BadgeID)";
                                    DLdb.SQLST4.Parameters.AddWithValue("UserID", UserID.ToString());
                                    DLdb.SQLST4.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                    DLdb.SQLST4.CommandType = CommandType.Text;
                                    DLdb.SQLST4.Connection = DLdb.RS4;
                                    SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                                    if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                                    DLdb.SQLST4.Parameters.RemoveAt(0);
                                    DLdb.SQLST4.Parameters.RemoveAt(0);
                                    DLdb.RS4.Close();

                                }

                                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                                DLdb.SQLST3.Parameters.RemoveAt(0);
                                DLdb.SQLST3.Parameters.RemoveAt(0);
                                DLdb.RS3.Close();
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "select * from DesignationSpecialisationPoints where Item=@Item";
                        DLdb.SQLST2.Parameters.AddWithValue("Item", theSqlDataReader["Designation"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                        if (theSqlDataReader2.HasRows)
                        {
                            theSqlDataReader2.Read();

                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "SELECT * FROM AssignedDesignationSpecialisationPoints where PointID=@PointID and UserID=@UserID";
                            DLdb.SQLST3.Parameters.AddWithValue("PointID", theSqlDataReader2["PointID"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("UserID", UserID.ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {

                                }
                            }
                            else
                            {
                                DLdb.RS4.Open();
                                DLdb.SQLST4.CommandText = "insert into AssignedDesignationSpecialisationPoints (UserID,PointID,Points) values (@UserID,@PointID,@Points)";
                                DLdb.SQLST4.Parameters.AddWithValue("UserID", UserID.ToString());
                                DLdb.SQLST4.Parameters.AddWithValue("PointID", theSqlDataReader["PointID"].ToString());
                                DLdb.SQLST4.Parameters.AddWithValue("Points", theSqlDataReader["Points"].ToString());
                                DLdb.SQLST4.CommandType = CommandType.Text;
                                DLdb.SQLST4.Connection = DLdb.RS4;
                                SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                                if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                                DLdb.SQLST4.Parameters.RemoveAt(0);
                                DLdb.SQLST4.Parameters.RemoveAt(0);
                                DLdb.SQLST4.Parameters.RemoveAt(0);
                                DLdb.RS4.Close();

                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();
                        }
                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                        
                    }
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from PlumberSpecialisations where PlumberID=@PlumberID";
                DLdb.SQLST.Parameters.AddWithValue("PlumberID", userID.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM badges where Item=@Item";
                        DLdb.SQLST2.Parameters.AddWithValue("Item", theSqlDataReader["Specialisations"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                DLdb.RS3.Open();
                                DLdb.SQLST3.CommandText = "SELECT * FROM AssignedBadges where BadgeID=@BadgeID and UserID=@UserID";
                                DLdb.SQLST3.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("UserID", userID.ToString());
                                DLdb.SQLST3.CommandType = CommandType.Text;
                                DLdb.SQLST3.Connection = DLdb.RS3;
                                SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                                if (theSqlDataReader3.HasRows)
                                {
                                    while (theSqlDataReader3.Read())
                                    {

                                    }
                                }
                                else
                                {
                                    DLdb.RS4.Open();
                                    DLdb.SQLST4.CommandText = "insert into AssignedBadges (UserID,BadgeID) values (@UserID,@BadgeID)";
                                    DLdb.SQLST4.Parameters.AddWithValue("UserID", userID.ToString());
                                    DLdb.SQLST4.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                    DLdb.SQLST4.CommandType = CommandType.Text;
                                    DLdb.SQLST4.Connection = DLdb.RS4;
                                    SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                                    if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                                    DLdb.SQLST4.Parameters.RemoveAt(0);
                                    DLdb.SQLST4.Parameters.RemoveAt(0);
                                    DLdb.RS4.Close();

                                }

                                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                                DLdb.SQLST3.Parameters.RemoveAt(0);
                                DLdb.SQLST3.Parameters.RemoveAt(0);
                                DLdb.RS3.Close();
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "select * from DesignationSpecialisationPoints where Item=@Item";
                        DLdb.SQLST2.Parameters.AddWithValue("Item", theSqlDataReader["Specialisations"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                        if (theSqlDataReader2.HasRows)
                        {
                            theSqlDataReader2.Read();

                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "SELECT * FROM AssignedDesignationSpecialisationPoints where PointID=@PointID and UserID=@UserID";
                            DLdb.SQLST3.Parameters.AddWithValue("PointID", theSqlDataReader2["PointID"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("UserID", UserID.ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {

                                }
                            }
                            else
                            {
                                DLdb.RS4.Open();
                                DLdb.SQLST4.CommandText = "insert into AssignedDesignationSpecialisationPoints (UserID,PointID,Points) values (@UserID,@PointID,@Points)";
                                DLdb.SQLST4.Parameters.AddWithValue("UserID", UserID.ToString());
                                DLdb.SQLST4.Parameters.AddWithValue("PointID", theSqlDataReader["PointID"].ToString());
                                DLdb.SQLST4.Parameters.AddWithValue("Points", theSqlDataReader["Points"].ToString());
                                DLdb.SQLST4.CommandType = CommandType.Text;
                                DLdb.SQLST4.Connection = DLdb.RS4;
                                SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                                if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                                DLdb.SQLST4.Parameters.RemoveAt(0);
                                DLdb.SQLST4.Parameters.RemoveAt(0);
                                DLdb.SQLST4.Parameters.RemoveAt(0);
                                DLdb.RS4.Close();

                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();
                        }
                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                    }
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                
                string HTMLSubject = "PIRB UPDATE REGISTRATION ";
                string HTMLBody = "Good Day " + plumberName +
                    "<br/>" +
                    "Thank you for updating your profile with the Plumbing Industry Registration Board.  Your application has been approved and your PIRB profile has been updated" +
                    "<br/><br/>" +
                    "To view your updated profile login to your profile. To access your profile, visit www.pirb.co.za, select the Plumber Login option and enter your username and password." +
                    "<br/><br/>" +
                    "It is your responsibility to keep your information on the PIRB registrar current. All PIRB notices will be communicated to the contacted details as reflected on the PIRB registrar at the time of the sending out the notices." +
                    "<br/><br/>" +
                    "If applicable your registration card will be ready for collection/postage/courier shortly.  " +
                     "<br/><br/>" +
                     "Thank you and we wish you luck in you PIRB registration career.  If you require any further information, please contact the PIRB on 0861 747 275 or email info@pirb.co.za." +
                      "<br/><br/>" +
                    "Best Regards" +
                    "<br/><br/>" +
                    "Lea Smith" +
                    "<br/>" +
                    "Chairman od the PIRB";
                DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumberEmail.ToString(), "");

                DLdb.sendSMS(UserID.ToString(), plumberContact.ToString(), "Thank you for registering with the PIRB.  Your application has been approved and your profile has been update.  Details sent to " + plumberEmail);

            }

            DLdb.DB_Close();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {

        }

        protected void btnQualificationAdd_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string PlumberID = "";
            string Name = "";
            string RegNo = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from NewApplicationsUpdate where ApplicationID=@ApplicationID";
            DLdb.SQLST.Parameters.AddWithValue("ApplicationID", DLdb.Decrypt(Request.QueryString["npid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "select * from Users where UserID=@UserID";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                if (theSqlDataReader2.HasRows)
                {
                    theSqlDataReader2.Read();
                    PlumberID = theSqlDataReader2["UserID"].ToString();
                    RegNo = theSqlDataReader2["RegNo"].ToString();
                    Name = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                }
                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into PlumberQualifications (PlumberID,CourseYear,TrainingProvider,QualifiedThrough,CertificationNo,NameQualifiedPlumber,RegNoQualifiedPlumber) values (@PlumberID,@CourseYear,@TrainingProvider,@QualifiedThrough,@CertificationNo,@NameQualifiedPlumber,@RegNoQualifiedPlumber)";
            DLdb.SQLST.Parameters.AddWithValue("PlumberID", PlumberID.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CourseYear", certificateYear.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TrainingProvider", TrainingProvider.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("QualifiedThrough", routeQualification.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CertificationNo", CertificationNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("NameQualifiedPlumber", Name.ToString());
            DLdb.SQLST.Parameters.AddWithValue("RegNoQualifiedPlumber", RegNo.ToString());
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
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}