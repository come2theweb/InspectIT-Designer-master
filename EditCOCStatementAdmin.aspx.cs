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
using System.IO;

namespace InspectIT
{
    public partial class EditCOCStatementAdmin : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();

            //// CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            if (Session["IIT_Role"].ToString() != "Inspector" && Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }

            if (Request.QueryString["msg"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
                successmsg.InnerHtml = msg;
                successmsg.Visible = true;
            }

            if (Request.QueryString["err"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
                errormsg.InnerHtml = msg;
                errormsg.Visible = true;
            }
            
            if (!IsPostBack)
            {
                
                DLdb.DB_Connect();

                string CustomerID = "";
                string UserID = "";
                string name = "";
                string surname = "";
                BlankFormWarning.Visible = false;
                //REQUIRED: DISABLE SAVE AND SUBMIT
                btnSave.Enabled = false;
                btnSubmit.Enabled = false;
                //REQUIRED: HIDE REFIX UNTILL isRefix = '1'
                DisplayRefixNotice.Visible = false;

                // SELECT LATEST COMMENT POSTED FROM COC REFIX COMMENTS
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM COCRefixesComments where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        latestCommentPosted.InnerHtml = theSqlDataReader["Comments"].ToString();
                        
                        // GET USER INSPECTOR DETAILS FOR RIGHT HAND SIDE COMMENTS
                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "SELECT * FROM Users where UserID=@UserID and Role='Inspector'";
                        DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                        DLdb.SQLST3.CommandType = CommandType.Text;
                        DLdb.SQLST3.Connection = DLdb.RS3;
                        SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                        if (theSqlDataReader3.HasRows)
                        {
                            while (theSqlDataReader3.Read())
                            {
                                DateTime dateCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                                inspectorDatePosted.InnerHtml = dateCreated.ToString("dd/MM/yyyy");
                                name = theSqlDataReader3["fname"].ToString();
                                surname = theSqlDataReader3["lname"].ToString();

                                inspectorName.InnerHtml = name + " " + surname;

                                inspectorComments.InnerHtml += "<p>" + theSqlDataReader["Comments"].ToString() + "</p>";

                               // inspectorDatePosted.InnerHtml = theSqlDataReader3["CreateDate"].ToString("dd/MM/yyyy");
                            }
                        }

                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.RS3.Close();
                        
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string compImg = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from FormImg where COCID = @COCID and isReference='0' and isCompleteImg='1' and isActive='1'";
                DLdb.SQLST.Parameters.AddWithValue("COCID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        string filename = theSqlDataReader["imgsrc"].ToString(); ;
                        if (theSqlDataReader["UserID"].ToString() == Session["IIT_UID"].ToString())
                        {
                            compImg += "<div class=\"col-md-3\" id=\"show_img_" + theSqlDataReader["ImgID"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader["imgid"].ToString() + "','" + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                        }
                        else
                        {
                            compImg += "<div class=\"col-md-3\"><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"AuditorImgs/" + filename + "\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                        }
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                dispCompltImgs.InnerHtml = compImg;
                // GET INSPECTOR DETAILS
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCInspectors I inner join Auditor A on I.UserID = A.UserID where I.COCStatementID = @COCStatementID and I.isactive = '1'";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        string photo = "<img src=\"assets/img/profiles/avatar_small2x.jpg\" />";
                        InspectorFullName.Text = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                        InspectorContact.Text = theSqlDataReader["phonemobile"].ToString();
                        InspectorEmail.Text = theSqlDataReader["email"].ToString();
                        InspectorRegNo.Text = theSqlDataReader["regno"].ToString();
                        InspectorBusContact.Text = theSqlDataReader["phonework"].ToString();
                        if (theSqlDataReader["photofile"] != DBNull.Value && theSqlDataReader["photofile"].ToString() != "")
                        {
                            photo = "<img src=\"AuditorImgs/" + theSqlDataReader["photofile"].ToString() + "\" style=\"height:170px;\" />";
                        }
                        InspectorImage.InnerHtml = photo;

                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM AuditHistory where COCStatementID = @COCStatementID and isAdmin='0'";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        auidtHistoryCommentsPosted.InnerHtml += "<br/>" + theSqlDataReader["AuditComment"].ToString() + "<hr/>";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM PlumberHistory where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        P2.InnerHtml += "<br/>" + theSqlDataReader["Comment"].ToString() + "<hr/>";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM AuditHistory where COCStatementID = @COCStatementID and isAdmin='1'";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        P1.InnerHtml += "<br/>" + theSqlDataReader["AuditComment"].ToString() + "<hr/>";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();


                //*********************************************************************************************************************
                //PRIVATE COMMENTS BETWEEN ADMIN AND INSPECTOR
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM COCPrivateComments where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        string newComm = "";
                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "SELECT * FROM Users where UserID=@UserID";
                        DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                        DLdb.SQLST3.CommandType = CommandType.Text;
                        DLdb.SQLST3.Connection = DLdb.RS3;
                        SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                        if (theSqlDataReader3.HasRows)
                        {
                            theSqlDataReader3.Read();
                            newComm = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
                        }

                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.RS3.Close();

                        //lastprivatecomm.InnerHtml = theSqlDataReader["Comments"].ToString();
                        DateTime dateCreatedView = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                        lastprivatecomm.InnerHtml = theSqlDataReader["Comments"].ToString() + "<br/> <small>" + newComm + " - " + dateCreatedView.ToString("dd/MM/yyyy") + "</small>";


                        // GET USER INSPECTOR DETAILS FOR RIGHT HAND SIDE COMMENTS
                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "SELECT * FROM Users where UserID=@UserID and Role='Inspector'";
                        DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                        DLdb.SQLST3.CommandType = CommandType.Text;
                        DLdb.SQLST3.Connection = DLdb.RS3;
                        theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                        if (theSqlDataReader3.HasRows)
                        {
                            while (theSqlDataReader3.Read())
                            {
                                DateTime dateCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                                inspectorDatePostedpriv.InnerHtml = dateCreated.ToString("dd/MM/yyyy");

                                inspecnamepriv.InnerHtml = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();

                                inspectorCommentspriv.InnerHtml += "<p>" + theSqlDataReader["Comments"].ToString() + "</p>";
                            }
                        }

                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.RS3.Close();

                        // GET USER DETAILS FOR LEFT HAND SIDE COMMENTS
                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "SELECT * FROM Users where UserID=@UserID and Role!='Inspector'";
                        DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                        DLdb.SQLST3.CommandType = CommandType.Text;
                        DLdb.SQLST3.Connection = DLdb.RS3;
                        theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                        if (theSqlDataReader3.HasRows)
                        {
                            while (theSqlDataReader3.Read())
                            {
                                DateTime dateCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                                admindatepost.InnerHtml = dateCreated.ToString("dd/MM/yyyy");
                                
                                adnames.InnerHtml = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
                                adminscomm.InnerHtml += "<p>" + theSqlDataReader["Comments"].ToString() + "</p>";
                            }
                        }

                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.RS3.Close();
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();


                // SELECT LATEST DATE POSTED FROM COC REFIX 
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM COCRefixes where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        RefixDate.Text = theSqlDataReader["CompletionDate"].ToString();
                        string isFixed = theSqlDataReader["isFixed"].ToString();
                        if (isFixed == "True")
                        {
                            RefixCompleted.Checked = true;
                            refixdetails.InnerHtml = theSqlDataReader["CompletionDate"].ToString();
                        } else
                        {
                            refixdetails.InnerHtml = theSqlDataReader["CompletionDate"].ToString();
                        }
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                // LOAD COC DETAILS
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM COCStatementDetails where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        
                        

                        if (theSqlDataReader["COCType"].ToString() == "Normal")
                        {
                            NormalCOC.Checked = true;
                            COCType.InnerHtml = "Type: Normal";
                        }
                        else if (theSqlDataReader["COCType"].ToString() == "Sales")
                        {
                            SalesCOC.Checked = true;
                            COCType.InnerHtml = "Type: Sales";
                        }
                        else if (theSqlDataReader["COCType"].ToString() == "PreInstall")
                        {
                            PreInstallCOC.Checked = true;
                            COCType.InnerHtml = "Type: Pre-Install";
                        }


                        NormalCOC.Enabled = false;
                        SalesCOC.Enabled = false;
                        PreInstallCOC.Enabled = false;
                        
                        CompletedDate.Text = theSqlDataReader["CompletedDate"].ToString();
                        InsuranceCompany.Text = theSqlDataReader["InsuranceCompany"].ToString();
                        PolicyHolder.Text = theSqlDataReader["PolicyHolder"].ToString();
                        PolicyNumber.Text = theSqlDataReader["PolicyNumber"].ToString();
                        PeriodOfInsuranceFrom.Text = theSqlDataReader["PeriodOfInsuranceFrom"].ToString();
                        PeriodOfInsuranceTo.Text = theSqlDataReader["PeriodOfInsuranceTo"].ToString();
                        CompletionofWork.InnerHtml = theSqlDataReader["WorkCompleteby"].ToString();
                        DescriptionofWork.Text = theSqlDataReader["DescriptionofWork"].ToString();

                        string misBank = theSqlDataReader["isBank"].ToString();
                        isBank.SelectedIndex = isBank.Items.IndexOf(isBank.Items.FindByValue(misBank));

                        //REQUIRED: IF TRUE ADD BANK FORM BY BANK NAME
                        if (misBank == "True")
                        {

                        }
                        plumbercontactedStatus.SelectedValue = theSqlDataReader["PlumberContacted"].ToString();
                        clientContactedStatus.SelectedValue = theSqlDataReader["ClientContacted"].ToString();
                        TextBox3.Text = theSqlDataReader["ScheduledDate"].ToString();
                        // GET STATUS


                    }
                }
                else
                {
                    BlankFormWarning.Visible = true;
                    btnSave.Enabled = false;
                    btnSubmit.Enabled = false;
                }
                
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string COCStatusName = "";

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        string cocDisp = "";
                        if (theSqlDataReader["COCFileName"].ToString() != "" && theSqlDataReader["COCFileName"] != DBNull.Value)
                        {
                            cocDisp = "<embed src=\"https://197.242.82.242/inspectit/pdf/" + theSqlDataReader["COCFileName"].ToString() + "\" width=\"700\" height=\"375\" type='application/pdf'>";
                        }
                        else if (theSqlDataReader["PaperBasedCOC"].ToString() != "" && theSqlDataReader["PaperBasedCOC"] != DBNull.Value)
                        {
                            cocDisp = "<img src=\"https://197.242.82.242/inspectit/pdf/" + theSqlDataReader["PaperBasedCOC"].ToString() + "\" style=\"height:375px;\" class=\"img-responsive\"/>";
                        }

                        nonCompTxtbx.Text = theSqlDataReader["NonComplianceDetails"].ToString();
                        pdfDisp.InnerHtml = cocDisp;

                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "Select * from COCReviews where COCStatementID = @COCStatementID and isclosed = '0' and isFixed='0' and Status='Failure'";
                        DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
                        DLdb.SQLST3.CommandType = CommandType.Text;
                        DLdb.SQLST3.Connection = DLdb.RS3;
                        SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                        if (theSqlDataReader3.HasRows)
                        {
                            while (theSqlDataReader3.Read())
                            {
                                cocStatusUpdate.SelectedValue = "Refix Required";
                            }
                        }
                        else
                        {
                            cocStatusUpdate.SelectedValue = theSqlDataReader["Status"].ToString();
                        }

                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.RS3.Close();
                        
                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "select * from CertificateTracking where Certificateid = @Certificateid";
                        DLdb.SQLST3.Parameters.AddWithValue("Certificateid", theSqlDataReader["COCStatementID"].ToString());
                        DLdb.SQLST3.CommandType = CommandType.Text;
                        DLdb.SQLST3.Connection = DLdb.RS3;
                        theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                        if (theSqlDataReader3.HasRows)
                        {
                            while (theSqlDataReader3.Read())
                            {
                                DateTime createDte = Convert.ToDateTime(theSqlDataReader3["DateCreated"].ToString());
                                logdetails.InnerHtml += "User: " + theSqlDataReader3["username"].ToString() + " message: " + theSqlDataReader3["message"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;" + createDte.ToString("dd/MM/yyyy HH:mm") + "<br />";
                            }
                        }

                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.RS3.Close();
                        
                        COCStatusName = theSqlDataReader["Status"].ToString();
                        COCNumber.InnerHtml = "COC Number: " + theSqlDataReader["COCNumber"].ToString();
                        invoiceDisp.InnerHtml = "<embed src=\"Inspectorinvoices"+theSqlDataReader["COCFileName"].ToString()+"\" width=\"500\" height=\"375\" type='application/pdf'>";
                        CustomerID = theSqlDataReader["CustomerID"].ToString();
                        UserID = theSqlDataReader["UserID"].ToString();
                        string isRefix = theSqlDataReader["isRefix"].ToString();
                        if (isRefix == "True")
                        {
                            DisplayRefixNotice.Visible = true;
                        }
                        if (theSqlDataReader["DateRefix"] != DBNull.Value)
                        {
                            NoDaysToComplete.Text = Convert.ToDateTime(theSqlDataReader["DateRefix"]).ToString("dd/MM/yyyy");
                        }
                        if (theSqlDataReader["AorB"].ToString() == "A")
                        {
                            CheckBoxList1.SelectedValue = "A";
                        }
                        else
                        {
                            CheckBoxList1.SelectedValue = "B";
                        }
                        if (theSqlDataReader["COCFilename"] != DBNull.Value)
                        {
                            showreportbtn.InnerHtml = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\"><button type=\"button\" ID=\"btnViewPDF\" class=\"btn btn-default\">View Report</button></a>";
                        }
                        else
                        {
                            showreportbtn.InnerHtml = "<a href=\"zCreateOlderPDF.aspx?cocid=" + theSqlDataReader["COCStatementID"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"Create COC\">Create Report</div></a>";
                        }
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                // GET USER DETAILS FOR LEFT HAND SIDE COMMENTS
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID and Role <> 'Inspector'";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", UserID);
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.HasRows)
                {
                    while (theSqlDataReader2.Read())
                    {
                        DateTime dateCreated = Convert.ToDateTime(theSqlDataReader2["CreateDate"].ToString());
                        plumberDatePosted.InnerHtml = dateCreated.ToString("dd/MM/yyyy");

                        name = theSqlDataReader2["fname"].ToString();
                        surname = theSqlDataReader2["lname"].ToString();

                        plumberName.InnerHtml = name + " " + surname;
                        PlumberFullName.Text = name + " " + surname;
                        PlumberRegNo.Text = theSqlDataReader2["regno"].ToString();
                        PlumberEmail.Text = theSqlDataReader2["email"].ToString();
                        PlumberContact.Text = theSqlDataReader2["contact"].ToString();
                        PlumberBusContact.Text = theSqlDataReader2["contact"].ToString();

                        Image1.ImageUrl = "Photos/" + theSqlDataReader2["Photo"].ToString();
                        plumname.InnerHtml= name + " " + surname;
                        plumregNo.InnerHtml= theSqlDataReader2["regno"].ToString();
                        // PlumberImage.InnerHtml += "<img src=\"photos/" + theSqlDataReader2["photo"].ToString() + "\" />";
                        //plumberComments.InnerHtml += "<p>" + theSqlDataReader2["Comments"].ToString() + "</p>";
                    }
                }

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();

                // LOAD CUSTOMER
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Customers where CustomerID = @CustomerID";
                DLdb.SQLST.Parameters.AddWithValue("CustomerID", CustomerID);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {

                        CustomerName.Text = theSqlDataReader["CustomerName"].ToString();
                        CustomerSurname.Text = theSqlDataReader["CustomerSurname"].ToString();
                        CustomerCellNo.Text = theSqlDataReader["CustomerCellNo"].ToString();
                        CustomerCellNoAlt.Text = theSqlDataReader["CustomerCellNoAlt"].ToString();
                        CustomerEmail.Text = theSqlDataReader["CustomerEmail"].ToString();
                        AddressStreet.Text = theSqlDataReader["AddressStreet"].ToString();
                        AddressSuburb.Text = theSqlDataReader["AddressSuburb"].ToString();
                        AddressCity.Text = theSqlDataReader["AddressCity"].ToString();
                        AddressAreaCode.Text = theSqlDataReader["AddressAreaCode"].ToString();

                        CustomerFullAddress.Text = theSqlDataReader["AddressStreet"].ToString() + "," + theSqlDataReader["AddressSuburb"].ToString() + "," + theSqlDataReader["AddressCity"].ToString() + "," + theSqlDataReader["AddressAreaCode"].ToString();

                        CustomerMap.InnerHtml = "<a href=\"https://www.google.co.za/maps/place/" + CustomerFullAddress.Text + "\" title=\"Click to view on map\" target=\"_blank\"><div class=\"btn btn-info\"><i class=\"fa fa-map-marker\"></i></div></a>";

                        string mProvince = theSqlDataReader["Province"].ToString();
                        Province.SelectedIndex = Province.Items.IndexOf(Province.Items.FindByValue(mProvince));

                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                // LOAD TYPE OF INSTALLATIONS
                TypeOfInstallation.Items.Clear();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive = '1'";
                //DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        TypeOfInstallation.Items.Add(new ListItem(theSqlDataReader["InstallationType"].ToString(), theSqlDataReader["InstallationTypeID"].ToString()));
                        
                        foreach (ListItem listItem in cocStatusUpdate.Items)
                        {
                            if (listItem.ToString() == COCStatusName.ToString())
                            {
                                listItem.Selected = true;
                            }
                        }
                           
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                inspectorList.Items.Clear();
                inspectorList.Items.Add(new ListItem("",""));
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Auditor where isactive = '1'";
                //DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        inspectorList.Items.Add(new ListItem(theSqlDataReader["fName"].ToString() + " " + theSqlDataReader["lName"].ToString(), theSqlDataReader["AuditorID"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();


                string LinksHTML = "<label class=\"btn btn-tag btn-danger btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=NON&tid=7&cocid=" + Request.QueryString["cocid"] + "&did=1'\">Non Compliance Notice</label><br />";
                int cnt = 0;
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM COCInstallations where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        foreach (ListItem li in TypeOfInstallation.Items)
                        {
                            if (li.Value.ToString() == theSqlDataReader["TypeID"].ToString())
                            {
                                li.Selected = true;

                                if (theSqlDataReader["isRefix"].ToString() == "True")
                                {
                                    completeForms.InnerHtml += "<label class=\"btn btn-tag btn-danger btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["cocid"] + "&did=" + theSqlDataReader["DataID"].ToString() + "&refix=1'\">" + li.Text.ToString() + "</label><br />";
                                }

                                if (theSqlDataReader["DataID"] == DBNull.Value)
                                {
                                    LinksHTML += "<div class=\"row\"><div class=\"col-md-12\"><label class=\"btn btn-tag btn-tag-light btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["cocid"] + "&did=" + theSqlDataReader["DataID"].ToString() + "'\">" + li.Text.ToString() + "</label></div></div><hr />";
                                }
                                else 
                                {
                                    cnt++;
                                    LinksHTML += "<div class=\"row\"><div class=\"col-md-12\"><label class=\"btn btn-tag btn-success btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["cocid"] + "'\">" + li.Text.ToString() + "</label></div></div><hr />";
                                }

                            }
                        }
                        
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();


                // GET AUDIT DETAILS
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM COCInspectors where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        InspectionDate.Text = theSqlDataReader["InspectionDate"].ToString();
                        string lQuality = theSqlDataReader["Quality"].ToString();
                        Quality.SelectedIndex = Quality.Items.IndexOf(Quality.Items.FindByValue(lQuality));
                        if (theSqlDataReader["Picture"] != DBNull.Value)
                        {
                            AuditPicture.InnerHtml = "<img src=\"noticeimages/" + theSqlDataReader["Picture"].ToString() + "\" class=\"img-responsive\" style=\"height:200px;\" />";
                        }
                        
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                // LOAD REVIEWS                
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM COCReviews where COCStatementID = @COCStatementID  and isClosed = '0' and isActive = '1' order by createdate desc";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        string StatusCol = "";
                        if (theSqlDataReader["status"].ToString() == "Failure")
                        {
                            StatusCol = "danger";
                        } 
                        else if (theSqlDataReader["status"].ToString() == "Cautionary")
                        {
                            StatusCol = "warning";
                        }
                        else if (theSqlDataReader["status"].ToString() == "Compliment")
                        {
                            StatusCol = "success";
                        }

                        string InstallationType = "";

                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "Select * from InstallationTypes where InstallationTypeID = @InstallationTypeID";
                        DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["TypeID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                InstallationType = theSqlDataReader2["InstallationType"].ToString();
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        string Media = "";
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "Select * from FormImg where CommentID = @CommentID";
                        DLdb.SQLST2.Parameters.AddWithValue("CommentID", theSqlDataReader["ReviewID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                string filename = theSqlDataReader2["imgsrc"].ToString(); ;
                                if (theSqlDataReader2["UserID"].ToString() == Session["IIT_UID"].ToString())
                                {
                                    Media += "<div class=\"col-md-3\" id=\"show_img_" + theSqlDataReader2["ImgID"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader2["imgid"].ToString() + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" class=\"img img-responsive\" /></a></div>";
                                }
                                else
                                {
                                    Media += "<div class=\"col-md-3\"><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" class=\"img img-responsive\" /></a></div>";
                                }
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        string isFixed = "";
                        string isFixedComments = "";
                        string isFixedImgs = "";

                        if (theSqlDataReader["isFixed"].ToString() == "True" && theSqlDataReader["status"].ToString() == "Failure")
                        {
                            isFixed = "This has been fixed";
                            isFixedComments = theSqlDataReader["RefixComments"].ToString();

                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "Select * from FormImg where ReviewID = @CommentID";
                            DLdb.SQLST2.Parameters.AddWithValue("CommentID", theSqlDataReader["ReviewID"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                while (theSqlDataReader2.Read())
                                {
                                    string filename = theSqlDataReader2["imgsrc"].ToString(); ;
                                    isFixedImgs += "<div class=\"col-md-3\"><div class=\"col-md-3\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader2["imgid"].ToString() + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" class=\"img img-responsive\" /></a></div>";
                                }
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            CurrentReview.InnerHtml += "<div class=\"row alert-success\" style=\"padding: 5px; \">" +
                                                   "       <div class=\"col-md-6 text-left\">" +
                                                   "         <b>Instalation Type:</b> " + InstallationType + "<br />" +
                                                   "         <b>Refix Status:</b> " + theSqlDataReader["status"].ToString() + " - Complete" +
                                                   "     </div>" +
                                                   "     <div class=\"col-md-6 text-left\">" +
                                                   "        <b>Media:</b><br />" + isFixedImgs + "" +
                                                   "     </div>" +
                                                   "     <div class=\"col-md-12 text-left\"><b>Comments:</b> " + isFixedComments + "</div>" +
                                                   "     <div class=\"col-md-12 text-right\"><div class=\"btn btn-primary\" onclick=\"document.location.href='EditReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "'\">Edit</div></div>" +
                                                   " </div><hr />";

                        }
                        else
                        {
                            CurrentReview.InnerHtml += "<div class=\"row alert-" + StatusCol + "\" style=\"padding: 5px; \">" +
                                                   "       <div class=\"col-md-6 text-left\">" +
                                                   "         <b>Instalation Type:</b> " + InstallationType + "<br />" +
                                                   "         <b>Audit Status:</b> " + theSqlDataReader["status"].ToString() + "" +
                                                   "     </div>" +
                                                   "     <div class=\"col-md-6 text-left\">" +
                                                   "        <b>Reference:</b> " + theSqlDataReader["Reference"].ToString() + "" +
                                                   "     </div>" +
                                                   "     <div class=\"col-md-12 text-left\"><b>Comments:</b> " + theSqlDataReader["comment"].ToString() + "</div>" +
                                                   "     <div class=\"col-md-12 text-left\"><b>Media:</b><br />" + Media + "</div>" +
                                                   "     <div class=\"col-md-12 text-right\"><div class=\"btn btn-primary\" onclick=\"document.location.href='EditReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "'\">Edit</div></div>" +
                                                   " </div><hr />";
                        }

                        

                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                //REQUIRED: SUCCESS BUTTON AND UPDATING PROGRESS BAR, IF ALL FORMS COMPLETE ACTIVATE SAVE AND SUBMIT
                if (cnt == 2)
                {
                    progressBarStatus.InnerHtml = "<div class=\"progress-bar progress-bar-primary\" data-percentage=\"100%\"></div>";
                }
                else if (cnt == 1)
                {
                    progressBarStatus.InnerHtml = "<div class=\"progress-bar progress-bar-primary\" data-percentage=\"50%\"></div>";
                }
                else
                {
                    progressBarStatus.InnerHtml = "<div class=\"progress-bar progress-bar-primary\" data-percentage=\"0%\"></div>";
                }

                // ADD THE CONTROLS
                FormLinks.InnerHtml = LinksHTML;

                btnSave.Enabled = true;
                btnSubmit.Enabled = true;

                TypeOfInstallation.Items[0].Enabled = false;
                TypeOfInstallation.Items[1].Enabled = false;
                TypeOfInstallation.Items[2].Enabled = false;
                TypeOfInstallation.Items[3].Enabled = false;
                TypeOfInstallation.Items[4].Enabled = false;
                TypeOfInstallation.Items[5].Enabled = false;
                TypeOfInstallation.Items[6].Enabled = false;
                TypeOfInstallation.Items[7].Enabled = false;
                CheckBoxList1.Items[0].Enabled = false;
                CheckBoxList1.Items[1].Enabled = false;

                DLdb.DB_Close();
            }
        }

        protected void btn_updateDetails_Click(object sender, EventArgs e)
        {
            string COCType = "";
            string CustomerID = "";

            if (NormalCOC.Checked == true)
            {
                COCType = "Normal";
            }
            else if (SalesCOC.Checked == true)
            {
                COCType = "Sales";
            }
            else if (PreInstallCOC.Checked == true)
            {
                COCType = "PreInstall";
            }

            Global DLdb = new Global();
            DLdb.DB_Connect();

            // ADD COC STATEMENT DETAILS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into COCStatementDetails (COCStatementID,CompletedDate,COCType,InsuranceCompany,PolicyHolder,PolicyNumber,isBank,PeriodOfInsuranceFrom,PeriodOfInsuranceTo) values (@COCStatementID,@CompletedDate,@COCType,@InsuranceCompany,@PolicyHolder,@PolicyNumber,@isBank,@PeriodOfInsuranceFrom,@PeriodOfInsuranceTo)";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID",DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.Parameters.AddWithValue("CompletedDate", CompletedDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("COCType", COCType.ToString());
            DLdb.SQLST.Parameters.AddWithValue("InsuranceCompany", InsuranceCompany.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PolicyHolder", PolicyHolder.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PolicyNumber", PolicyNumber.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("isBank", isBank.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PeriodOfInsuranceFrom", PeriodOfInsuranceFrom.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PeriodOfInsuranceTo", PeriodOfInsuranceTo.Text.ToString());
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
            DLdb.RS.Close();

            // ADD CUSTOMER IF NOT CUTOMER EXISTS
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select * from Customers where CustomerEmail = @CustomerEmail";
            DLdb.SQLST2.Parameters.AddWithValue("CustomerEmail", CustomerEmail.Text.ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (!theSqlDataReader2.HasRows)
            {
                //REQUIRED: Customer NEEDS PASSWORD....
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into Customers (CustomerName,CustomerSurname,CustomerCellNo,CustomerCellNoAlt,CustomerEmail,AddressStreet,AddressSuburb,AddressCity,Province,AddressAreaCode,CustomerPassword) values (@CustomerName,@CustomerSurname,@CustomerCellNo,@CustomerCellNoAlt,@CustomerEmail,@AddressStreet,@AddressSuburb,@AddressCity,@Province,@AddressAreaCode,@CustomerPassword); Select Scope_Identity() as CustomerID";
                DLdb.SQLST.Parameters.AddWithValue("CustomerName", CustomerName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerSurname", CustomerSurname.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerCellNo", CustomerCellNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerCellNoAlt", CustomerCellNoAlt.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerEmail", CustomerEmail.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressStreet", AddressStreet.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressSuburb", AddressSuburb.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressCity", AddressCity.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Province", Province.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressAreaCode", AddressAreaCode.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerPassword", DLdb.CreatePassword(8));

                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    CustomerID = theSqlDataReader["CustomerID"].ToString();
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

                // UPDATE THE CUSTOMER ID
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCStatements set CustomerID = @CustomerID where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.Parameters.AddWithValue("CustomerID", CustomerID.ToString());

                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }
            else
            {
                theSqlDataReader2.Read();

                // UPDATE THE CUSTOMER ID
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCStatements set CustomerID = @CustomerID where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.Parameters.AddWithValue("CustomerID", theSqlDataReader2["CustomerID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();
            
            // INSPECTION TYPES
            foreach (ListItem item in TypeOfInstallation.Items)
            {
                if (item.Selected)
                {
                    //REQUIRED: CHECK IF EXISTS AND UPDATE OR ADD NEW
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from COCInstallations where InstallationTypeID = @InstallationTypeID and COCStatementID = @COCStatementID";
                    DLdb.SQLST.Parameters.AddWithValue("InstallationTypeID", item.Value.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();

                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "update COCInstallations set TypeID=@TypeID,COCStatementID=@COCStatementID where InstallationTypeID=@InstallationTypeID";
                        DLdb.SQLST3.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["InstallationTypeID"].ToString());
                        DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                        DLdb.SQLST3.Parameters.AddWithValue("TypeID", item.Value.ToString());
                        DLdb.SQLST3.CommandType = CommandType.Text;
                        DLdb.SQLST3.Connection = DLdb.RS3;
                        SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.RS3.Close();
                    }
                    else
                    {
                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "insert into COCInstallations (TypeID,COCStatementID) values (@TypeID,@COCStatementID)";
                        DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                        DLdb.SQLST3.Parameters.AddWithValue("TypeID", item.Value.ToString());
                        DLdb.SQLST3.CommandType = CommandType.Text;
                        DLdb.SQLST3.Connection = DLdb.RS3;
                        SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.RS3.Close();
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                }
            }
            
            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementAdmin.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("COC Statement has been updated"));
        }

        protected void saveOrUpdate_Click(object sender, EventArgs e)
        {
            string COCType = "";
            string CustomerID = "";

            if (NormalCOC.Checked == true)
            {
                COCType = "Normal";
            }
            else if (SalesCOC.Checked == true)
            {
                COCType = "Sales";
            }
            else if (PreInstallCOC.Checked == true)
            {
                COCType = "PreInstall";
            }

            Global DLdb = new Global();
            DLdb.DB_Connect();

            // ADD COC STATEMENT DETAILS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatementDetails set PlumberContacted=@PlumberContacted,ClientContacted=@ClientContacted,ScheduledDate=@ScheduledDate where cocstatementid=@cocstatementid ";
            DLdb.SQLST.Parameters.AddWithValue("PlumberContacted", plumbercontactedStatus.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ClientContacted", clientContactedStatus.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ScheduledDate", TextBox3.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatements set Status=@Status where cocstatementid=@cocstatementid";
            DLdb.SQLST.Parameters.AddWithValue("Status", cocStatusUpdate.SelectedValue);
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementAdmin.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("COC Statement has been updated"));
        }

        protected void updateRefix_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string uid = Session["IIT_UID"].ToString();

            // ADD COC REFIX DETAILS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into COCRefixes (UserID,COCStatementID,CompletionDate,isFixed) values (@UserID,@COCStatementID,@CompletionDate,@isFixed)";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid);
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.Parameters.AddWithValue("CompletionDate", RefixDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("isFixed", this.RefixCompleted.Checked ? "1" : "0");
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementAdmin.aspx?cocid=" + Request.QueryString["cocid"]);
        }

        protected void addComments_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string uid = Session["IIT_UID"].ToString();
            string refixNoticeId = "";

            // GET LATEST REFIX NOTICE ID TO INSERT A COMMENT SPECIFIC TO THAT ID
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCRefixes where COCStatementID=@COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    refixNoticeId = theSqlDataReader["RefixNoticeID"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // ADD COC REFIX COMMENTS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into COCRefixesComments (RefixNoticeID,COCStatementID,UserID,Comments) values (@RefixNoticeID,@COCStatementID,@UserID,@Comments)";
            DLdb.SQLST.Parameters.AddWithValue("RefixNoticeID", refixNoticeId);
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid);
            DLdb.SQLST.Parameters.AddWithValue("Comments", RefixComments.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementAdmin.aspx?cocid=" + Request.QueryString["cocid"]);
        }

        protected void addAudHis_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string uid = Session["IIT_UID"].ToString();
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into AuditHistory (COCStatementID,UserID,AuditComment) values (@COCStatementID,@UserID,@AuditComment)";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid);
            DLdb.SQLST.Parameters.AddWithValue("AuditComment", TextBox2.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementAdmin.aspx?cocid=" + Request.QueryString["cocid"]);
        }

        protected void addplumberHis_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string plumberID = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatements where COCStatementID=@COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                plumberID = theSqlDataReader["UserID"].ToString();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into PlumberHistory (COCStatementID,PlumberID,Comment,isAdmin) values (@COCStatementID,@PlumberID,@Comment,'1')";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.Parameters.AddWithValue("PlumberID", plumberID);
            DLdb.SQLST.Parameters.AddWithValue("Comment", TextBox9.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementAdmin.aspx?cocid=" + Request.QueryString["cocid"]);
        }

        protected void addAdminHis_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string uid = Session["IIT_UID"].ToString();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into AuditHistory (COCStatementID,UserID,AuditComment,isAdmin) values (@COCStatementID,@UserID,@AuditComment,'1')";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid);
            DLdb.SQLST.Parameters.AddWithValue("AuditComment", TextBox7.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementAdmin.aspx?cocid=" + Request.QueryString["cocid"]);
        }

        protected void addPrivateComments_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string uid = Session["IIT_UID"].ToString();
            
            // ADD COC REFIX COMMENTS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into COCPrivateComments (COCStatementID,UserID,Comments) values (@COCStatementID,@UserID,@Comments)";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid);
            DLdb.SQLST.Parameters.AddWithValue("Comments", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementAdmin.aspx?cocid=" + Request.QueryString["cocid"]);
        }


        // GET FORM TYPE FOR NON COMPLIANCE AND LOAD CORRECT FORM
        protected void addNonCompliance_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCInstallations where COCStatementID=@COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "select * from FormLinks where TypeID=@TypeID";
                    DLdb.SQLST3.Parameters.AddWithValue("TypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        while (theSqlDataReader3.Read())
                        {
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "select * from Forms where FormID=@FormID";
                            DLdb.SQLST2.Parameters.AddWithValue("FormID", theSqlDataReader3["FormID"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();
                        }
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        protected void TemplateSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (TemplateSelection.SelectedValue.ToString() != "")
            //{
            //    AuditorComments.Text = TemplateSelection.SelectedValue.ToString();

            //}
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string InspectionStatus = "";
            //if (chkFailure.Checked == true)
            //{
            //    InspectionStatus = "Failure";
            //}
            //else if (chkCompleted.Checked == true)
            //{
            //    InspectionStatus = "Completed";
            //}
            //else if (chkCautionary.Checked == true)
            //{
            //    InspectionStatus = "Cautionary";
            //}
            //else if (chkComplement.Checked == true)
            //{
            //    InspectionStatus = "Complement";
            //}
            //else if (chkRefixed.Checked == true)
            //{
            //    InspectionStatus = "Refix";
            //}

            // CHECK THE PICTURE
            if (FileUpload1.HasFile)
            {
                try
                {

                    //// UPLOAD THE PICTURE
                    string filename = Path.GetFileName(FileUpload1.FileName);
                    filename = DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + filename;
                    FileUpload1.SaveAs(Server.MapPath("~/noticeimages/") + filename);

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update COCInspectors set InspectionDate = @InspectionDate, Status = @Status, Quality = @Quality, Picture = @Picture where COCStatementID = @COCStatementID and UserID = @UserID and isactive='1'";
                    DLdb.SQLST.Parameters.AddWithValue("InspectionDate", InspectionDate.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Status", InspectionStatus);
                    DLdb.SQLST.Parameters.AddWithValue("Quality", Quality.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Picture", filename);
                    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
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
                    DLdb.RS.Close();

                }
                catch (Exception err)
                {
                    errormsg.InnerHtml = "Error: " + err.Message;
                    errormsg.Visible = true;
                }

            }
            else
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCInspectors set InspectionDate = @InspectionDate, Status = @Status, Quality = @Quality where COCStatementID = @COCStatementID and UserID = @UserID and isactive='1'";
                DLdb.SQLST.Parameters.AddWithValue("InspectionDate", InspectionDate.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Status", InspectionStatus);
                DLdb.SQLST.Parameters.AddWithValue("Quality", Quality.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }
            
            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementAdmin.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("COC Audit Statement Saved"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string InspectionStatus = "";
            string isReFix = "0";


            //if (chkFailure.Checked == true)
            //{
            //    InspectionStatus = "Failure";
            //    isReFix = "1";
            //}
            //else if (chkCompleted.Checked == true)
            //{
            //    InspectionStatus = "Completed";
            //}
            //else if (chkCautionary.Checked == true)
            //{
            //    InspectionStatus = "Cautionary";
            //}
            //else if (chkComplement.Checked == true)
            //{
            //    InspectionStatus = "Complement";
            //}
            //else if (chkRefixed.Checked == true)
            //{
            //    InspectionStatus = "Refix";
            //    isReFix = "1";
            //}

            // CHECK THE PICTURE
            if (FileUpload1.HasFile)
            {
                try
                {

                    // UPLOAD THE PICTURE
                    string filename = Path.GetFileName(FileUpload1.FileName);
                    filename = DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + filename;
                    FileUpload1.SaveAs(Server.MapPath("~/noticeimages/") + filename);

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update COCInspectors set isComplete = '1',CompletedOn = getdate(),InspectionDate = @InspectionDate, Status = @Status, Quality = @Quality, Picture = @Picture where COCStatementID = @COCStatementID and UserID = @UserID and isactive='1'";
                    DLdb.SQLST.Parameters.AddWithValue("InspectionDate", InspectionDate.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Status", InspectionStatus);
                    DLdb.SQLST.Parameters.AddWithValue("Quality", Quality.SelectedValue.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Picture", filename);
                    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
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
                    DLdb.RS.Close();

                    

                }
                catch (Exception err)
                {
                    errormsg.InnerHtml = "Error: " + err.Message;
                    errormsg.Visible = true;
                }

            }
            else
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCInspectors set isComplete = '1',CompletedOn = getdate(),InspectionDate = @InspectionDate, Status = @Status, Quality = @Quality where COCStatementID = @COCStatementID and UserID = @UserID and isactive='1'";
                DLdb.SQLST.Parameters.AddWithValue("InspectionDate", InspectionDate.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Status", InspectionStatus);
                DLdb.SQLST.Parameters.AddWithValue("Quality", Quality.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                
            }

            if (isReFix == "1")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCStatements set isRefix = '1', DateRefix = @RefixDate where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.Parameters.AddWithValue("RefixDate", NoDaysToComplete.Text.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }
            else
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCStatements set DateRefix = @RefixDate where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.Parameters.AddWithValue("RefixDate", NoDaysToComplete.Text.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }

            string CustomerID = "";
            string Reviews = "";
            var html_FIELD_Content = "";
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "SELECT * FROM COCReviews where COCStatementID = @COCStatementID  and isFixed = '0' and isActive = '1' order by createdate desc";
            DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    string StatusCol = "";
                    string backCol = "";
                    string btnFix = "";

                    if (theSqlDataReader2["status"].ToString() == "Failure")
                    {
                        backCol = "#F8D7DA";
                        StatusCol = "danger";
                        btnFix = "<div class=\"btn btn-primary\" onclick=\"document.location.href='FixReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader2["ReviewID"].ToString()) + "'\">Refix Complete</div>";
                    }
                    else if (theSqlDataReader2["status"].ToString() == "Cautionary")
                    {
                        backCol = "#FFF3CD";
                        StatusCol = "warning";
                        btnFix = "<div class=\"btn btn-primary\" onclick=\"document.location.href='DismissReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader2["ReviewID"].ToString()) + "'\">Dismiss</div>";
                    }
                    else if (theSqlDataReader2["status"].ToString() == "Compliment")
                    {
                        backCol = "#D4EDDA";
                        StatusCol = "success";
                        btnFix = "<div class=\"btn btn-primary\" onclick=\"document.location.href='DismissReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader2["ReviewID"].ToString()) + "'\">Dismiss</div>";
                    }

                    string InstallationType = "";

                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "Select * from InstallationTypes where InstallationTypeID = @InstallationTypeID";
                    DLdb.SQLST3.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader2["TypeID"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        while (theSqlDataReader3.Read())
                        {
                            InstallationType = theSqlDataReader3["InstallationType"].ToString();
                        }
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();

                    string Media = "";
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "Select * from FormImg where CommentID = @CommentID";
                    DLdb.SQLST3.Parameters.AddWithValue("CommentID", theSqlDataReader2["ReviewID"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        while (theSqlDataReader3.Read())
                        {
                            if (theSqlDataReader3["imgsrc"].ToString() != "" || theSqlDataReader3["imgsrc"] != DBNull.Value)
                            {
                                string filename = theSqlDataReader3["imgsrc"].ToString();
                                Media += "<div class=\"col-md-3\"><img src=\"https://197.242.82.242/inspectit/AuditorImgs/" + filename + "\" class=\"img img-responsive\" style=\"height:100px;\" /></div>";

                            }
                            else
                            {
                                Media = "";
                            }
                        }
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();

                    string refImgsw = "";
                    if (theSqlDataReader2["ReferenceImage"].ToString() != "" || theSqlDataReader2["ReferenceImage"] != DBNull.Value)
                    {
                        string filename = theSqlDataReader2["ReferenceImage"].ToString();
                        refImgsw += "<img src=\"https://197.242.82.242/inspectit/assets/img/" + filename + "\" class=\"img img-responsive\" style=\"height:100px;\" />";

                    }
                    else
                    {
                        refImgsw = "";
                    }
                    html_FIELD_Content = "<table border='0' cellpadding='10px' cellspacing='0' width='80%'>" +
                                            "<tr>" +
                                            "  <td align='left' width='10%'>" +
                                            "      <b>Installation Type:</b>" +
                                            "  </td>" +
                                            "  <td align='left' width='40%'>" +
                                                    InstallationType +
                                            "  </td>" +
                                            "  <td align='left' width='10%'>" +
                                            "      <b>Reference:</b>" +
                                            "  </td>" +
                                            "  <td align='right' width='40%'>" +
                                            "      " + theSqlDataReader2["Reference"].ToString() + "<br/>" +
                                                        refImgsw +
                                            "  </td>" +
                                            "</tr>" +
                                            "<tr>" +
                                            "  <td align='left' width='10%'>" +
                                            "      <b>Audit Status:</b>" +
                                            "  </td>" +
                                            "  <td align='left' width='40%'>" +
                                                    theSqlDataReader2["status"].ToString() +
                                            "  </td>" +
                                            "  <td align='left' width='10%'>" +
                                            "      <b>Comments:</b>" +
                                            "  </td>" +
                                            "  <td align='right' width='40%'>" +
                                            "      " + theSqlDataReader2["comment"].ToString() + "<br/>" +
                                                        Media +
                                            "  </td>" +
                                            "</tr>" +
                                            "</table>";

                    Reviews += "<div class=\"row alert-" + StatusCol + "\" style=\"padding: 5px;background-color:" + backCol + "; \">" +
                                               html_FIELD_Content +
                                               " </div><hr />";

                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            // GET USER DETAILS
            string uname = "";
            string uemail = "";
            string ucompany = "";
            string usignature = "";
            string uaddress = "";
            string ucontact = "";
            string uregno = "";

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select top 1 * from Users where UserID = @UserID";
            DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2= DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                theSqlDataReader2.Read();
                uname = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                uemail = theSqlDataReader2["email"].ToString();
                ucompany = theSqlDataReader2["company"].ToString();
                string sigImg = "";
                if (theSqlDataReader2["signature"].ToString() != "" || theSqlDataReader2["signature"] != DBNull.Value)
                {
                    string filename = theSqlDataReader2["signature"].ToString();
                    sigImg += "<img src=\"http://197.242.82.242/pirbreg/signatures/" + sigImg + "\" class=\"img img-responsive\" style=\"max-height:100px;\" />";

                }
                else
                {
                    sigImg = "";
                }
                // usignature = "<img src=\"http://197.242.82.242/pirbreg/signatures/" + theSqlDataReader2["signature"].ToString() + "\" />";
                usignature = sigImg;
                uaddress = theSqlDataReader2["ResidentialStreet"].ToString() + " " + theSqlDataReader2["ResidentialSuburb"].ToString() + " " + theSqlDataReader2["ResidentialCity"].ToString() + "  " + theSqlDataReader2["Province"].ToString() + " " + theSqlDataReader2["ResidentialCode"].ToString();
                ucontact = theSqlDataReader2["fname"].ToString();
                uregno = theSqlDataReader2["regno"].ToString();
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "Select * from COCStatements where COCStatementID = @COCStatementID";
            DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    CustomerID = theSqlDataReader2["CustomerID"].ToString();
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            string clientname = "";
            string clientaddress = "";
            string Clientemail = "";
            string ClientTel = "";

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select * from Customers where CustomerID = @CustomerID";
            DLdb.SQLST2.Parameters.AddWithValue("CustomerID", CustomerID);
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
            
            if (theSqlDataReader2.HasRows)
            {
                theSqlDataReader2.Read();
                clientname = theSqlDataReader2["CustomerName"].ToString() + ' ' + theSqlDataReader2["CustomerSurname"].ToString();
                clientaddress = theSqlDataReader2["AddressStreet"].ToString() + "<br />" + theSqlDataReader2["AddressSuburb"].ToString() + "<br />" + theSqlDataReader2["AddressCity"].ToString() + "<br />" + theSqlDataReader2["Province"].ToString();
                Clientemail = theSqlDataReader2["CustomerEmail"].ToString();
                ClientTel = theSqlDataReader2["CustomerCellNo"].ToString();

            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            string clientdetails = "<div style='border:1px solid #EFEFEF;padding:10px;width:100%;'><b>Customer Name: </b>" + clientname + "<br />";
            clientdetails += "<b>Email Address: </b>" + Clientemail + "<br />";
            clientdetails += "<b>Tel No.: </b>" + ClientTel + "<br />";
            clientdetails += "<b>Address: </b>" + clientaddress + "<br /></div>";

            // BUILD THE PDF FILENAME
            DateTime cDate = DateTime.Now;
            string filenames = cDate.ToString("ddMMyyyy") + "_" + Session["IIT_UID"].ToString() + "_InspectorCOC_" + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + ".pdf";
            
            string seltype = "";

            if (CheckBoxList1.SelectedValue.ToString() == "A")
            {
                seltype = "                       <tr>" +
                            "                           <td width='10%'>" +
                            "                           <b>A</b>" +
                            "                          </td>" +
                            "                           <td width='90%'>" +
                            "                           <p>The above plumbing work was carried out by me or under my supervision, and that it complies in all respects to the plumbing regulations, laws, National Compulsory Standards and Local by laws.</p>" +
                            "                          </td></tr>" +
                            "                       <tr>";
            }
            else
            {
                seltype = "                       <tr>" +
                            "                           <td width='10%'>" +
                            "                           <b>B</b>" +
                            "                          </td>" +
                            "                           <td width='90%'>" +
                            "                           <p>I have fully inspected and tested the work started but not completed by another Licensed plumber. I further certify that the inspected and tested work and the necessary completion work was carried out by me or under my supervision - complies in all respects to the plumbing regulations, laws, National Compulsory Standards and Local by laws.</p>" +
                            "                          </td></tr>";
            }

            string hotwatersystemstick = "";
            string coldwatersystemstick = "";
            string sanitaryware = "";
            string belowground = "";
            string aboveground = "";
            string rainwater = "";
            foreach (ListItem item in TypeOfInstallation.Items)
            {
                if (item.Selected)
                {
                    // item.Value.ToString()
                    if (item.Value.ToString() == "1")
                    {
                        hotwatersystemstick = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }

                    if (item.Value.ToString() == "2")
                    {
                        belowground = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }

                    if (item.Value.ToString() == "3")
                    {
                        coldwatersystemstick = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }
                    if (item.Value.ToString() == "4")
                    {
                        aboveground = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }
                    if (item.Value.ToString() == "7")
                    {
                        sanitaryware = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }
                    if (item.Value.ToString() == "8")
                    {
                        rainwater = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }
                }
            }

            var htmlContent = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
                                                "<h2 style='text-align:center;font-size:50px;color:#735b41;'>PLUMBING CERTIFICATE OF COMPLIANCE</h2>" +
                                                "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                "        <tr>" +
                                                "            <td>" +
                                                "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'><tr>" +
                                                "                           <td>" +
                                                "                               <img src='https://197.242.82.242/inspectit/assets/img/cardlogo.jpg' />" +
                                                "                           </td><td>" +
                                                "                               <div style='width:100%;'><div style='width:80%;background-color:#ccc;top:10;float:right;border: 1px solid #E5E5E5;padding:10px'> COC Number: " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " </div ></div ><br /><br /><br />" +
                                                "                               <div style='width:100%;'><div style='width:80%;background-color:pink;top:10;float:right;border: 1px solid #E5E5E5;padding:10px'> ONLY PIRB REGISTERED LICENSED PLUMBERS ARE AUTHORISED TO ISSUE THIS PLUMBING CERTIFICATE OF COMPLIANCE </div></div><br/><br /><br /><br /> " +
                                                "                               <div style='width:100%;'><div style='width:80%;background-color:red;color:white;top:10;float:right;float:right;border: 1px solid #E5E5E5;padding:10px'> TO VERIFY AND AUTHENTICATE THIS CERTIFICATE OF COMPLIANCE VISIT PIRB.CO.ZA AND CLICK ON VERIFY / AUTHENTICATE LINK </div></div> " +
                                                "                           </td></tr></table>" +
                                                "                            <br /><br />" +
                                                //"                        <div>" + clientdetails + "</div>" +
                                                // "                        <div style='width:100%;'><div style='background-color:#ccc;position:absolute;top:10;float:right;border: 1px solid #E5E5E5;padding:10px'>COC Number: " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + "</div></div></td>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                    <td bgcolor='lightgreen'>" +
                                                "                       <h4 style='background-color:lightgreen;text-align:center;'>Physical Address Details of Installation</h4>" +
                                                //"                        <div>" + clientdetails + "</div>" +
                                                "                    </td>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                    <td>" +
                                                "                        " + clientdetails + "" +
                                                "                    </td>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'><tr>" +
                                                "                           <td bgcolor='lightgreen' width='80%'>" +
                                                "                               <h4 style='background-color:lightgreen;text-align:center;'>Type of Installation Carried Out by Licensed Plumber <br /><span>(Clearly tick the appropriate Installation Category Code and complete the installation details below)</span></h4>" +
                                                "                               " +
                                                "                           </td><td width='10%'>" +
                                                "                               <h4 style='text-align:center;'>Code</h4>" +
                                                "                           </td><td width='10%'>" +
                                                "                               <h4 style='text-align:center;'>Tick</h4>" +
                                                "                           </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:red;'> Hot Water System </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" + hotwatersystemstick + "" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:lightblue;'> Cold Water System </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" + coldwatersystemstick + "" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:blue;'> Sanitary-ware and Sanitary-fittings </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" + sanitaryware + "" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:brown;'> Below-ground Drainage System </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" + belowground + "" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:green;'> Above-ground Drainage System </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" + aboveground + "" +
                                                "                          </td></tr>" +
                                                 "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:darkblue;'> Rain Water Disposal System </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" + rainwater + "" +
                                                "                          </td></tr>" +
                                                "                       </table>" +

                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'><tr>" +
                                                "                           <td bgcolor='lightgreen' width='80%'>" +
                                                "                               <h4 style='background-color:lightgreen;text-align:center;'>Specialisations: To be Carried Out by Licensed Plumber Only Registered to do the Specialised Word <br /><span>(To Verify and authenticate Licensed Plumbers specialisations visit pirb.co.za)</span></h4>" +
                                                "                               " +
                                                "                           </td><td width='10%'>" +
                                                "                               <h4 style='text-align:center;'>Code</h4>" +
                                                "                           </td><td width='10%'>" +
                                                "                               <h4 style='text-align:center;'>Tick</h4>" +
                                                "                           </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:orange;'> Solar Water Heating System </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:maroon;'> Heat Pump </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                          </td></tr>" +
                                                "                       </table>" +
                                                "                    </tr>" +
                                                "                       <tr><i>See explanations of the above on the reverse of this certificate</i></tr>" +

                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'><tr>" +
                                                "                           <td bgcolor='lightgreen'>" +
                                                "                               <h4 style='background-color:lightgreen;text-align:center;'>Installation Details<br /><span>(Details of the work undertaken or scope of work for which the COC is being issued for)</span></h4>" +
                                                "                           </td></tr>" +
                                                 "                    <tr>" +
                                                "                        <td align='middle'>" +
                                                                            //"                               " + html_FIELD_Content + " " +
                                                                            Reviews +
                                                "                        </td>" +
                                                "                    </tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                          </td></tr>" +
                                                "                       </table>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'><tr>" +
                                                "                           <td bgcolor='lightgreen'>" +
                                                "                               <h4 style='background-color:lightgreen;text-align:center;'>Pre-Existing Non Compliance* Conditions<br /><span>(Details of any non-compliance of the pre-existing plumbing installation on which work was done that needs to be brought to the attention of owner/user)</span></h4>" +
                                                "                           </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                          </td></tr>" +
                                                "                       </table>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'>" +
                                                "                       <tr> " +
                                                "                           <td width='10%'>" +
                                                "                          </td>" +
                                                "                           <td width='90%'>" +
                                                "                           <p>I " + uname + " (Licensed Plumber's Name and Surname), Licensed registration number " + uregno + ", certify that, " +
                                                "                                   the above compliance certificate details are true and correct and will be logged in accordance with the prescribed requirements as defined by the PIRB." +
                                                "                                   I further certify that; " +
                                                "                                   <br />Delete either <b>A</b> or <b>B</b> as appropriate</p>" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                       " + seltype + "    " +
                                                "                       <tr>" +
                                                "                           <td width='10%'></td> <td width='90%'>" +
                                                "                               Signed (Licensed Plumber): " + usignature + "" +
                                                "                          </td></tr>" +
                                                "                       </table>" +
                                                "                    </tr><br/><br/><br/><br/><br/><br/><br/><br/><br/>" +
                                                "                       </table>" +
                                                "                    </tr><br/><br/><br/><br/><br/><br/><br/><br/><br/>" +
                                                "                </table>" +
                                                "            </td>" +
                                                "        </tr>" +
                                                "    </table>" +
                                                "</body>");

            var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(htmlContent);
            string path = Server.MapPath("~/Inspectorinvoices/") + filenames;
            File.WriteAllBytes(path, pdfBytes);

            //if (isReFix == "1")
            //{
            //    DLdb.RS.Open();
            //    DLdb.SQLST.CommandText = "update COCStatements set isRefix = '1', DateRefix = @RefixDate where COCStatementID = @COCStatementID";
            //    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            //    DLdb.SQLST.Parameters.AddWithValue("RefixDate", NoDaysToComplete.Text.ToString());

            //    DLdb.SQLST.CommandType = CommandType.Text;
            //    DLdb.SQLST.Connection = DLdb.RS;
            //    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.RS.Close();

            //    // CREATE INVOICE
            //    var htmlContent = "";
            //    string invID = "";
            //    string total = "";
            //    string UserName = "";
            //    string UserEmail = "";
            //    string cocType = "";
            //    string filename = "";
            //    string staffname = "";
            //    string staffid = "";
            //    string cocStatementNo = "";
            //    string staffCell = "";
            //    string staffEmail = "";

            //    // GET SITES
            //    DLdb.RS.Open();
            //    DLdb.SQLST.CommandText = "select * from COCInspectors where UserID=@UserID";
            //    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            //    DLdb.SQLST.CommandType = CommandType.Text;
            //    DLdb.SQLST.Connection = DLdb.RS;
            //    theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //    if (theSqlDataReader.HasRows)
            //    {
            //        while (theSqlDataReader.Read())
            //        {

            //            DLdb.RS3.Open();
            //            DLdb.SQLST3.CommandText = "select * from users where userid = @UserID";
            //            DLdb.SQLST3.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            //            DLdb.SQLST3.CommandType = CommandType.Text;
            //            DLdb.SQLST3.Connection = DLdb.RS3;
            //            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            //            if (theSqlDataReader3.HasRows)
            //            {
            //                theSqlDataReader3.Read();
            //                UserName = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
            //                UserEmail = theSqlDataReader3["email"].ToString();

            //            }

            //            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            //            DLdb.SQLST3.Parameters.RemoveAt(0);
            //            DLdb.RS3.Close();

            //            DLdb.RS3.Open();
            //            DLdb.SQLST3.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID";
            //            DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
            //            DLdb.SQLST3.CommandType = CommandType.Text;
            //            DLdb.SQLST3.Connection = DLdb.RS3;
            //            theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            //            if (theSqlDataReader3.HasRows)
            //            {
            //                theSqlDataReader3.Read();
            //                cocType = theSqlDataReader3["Type"].ToString();
            //                cocStatementNo = theSqlDataReader3["COCStatementID"].ToString();

            //                DLdb.RS4.Open();
            //                DLdb.SQLST4.CommandText = "select * from users where UserID = @UserID";
            //                DLdb.SQLST4.Parameters.AddWithValue("UserID", theSqlDataReader3["UserID"].ToString());
            //                DLdb.SQLST4.CommandType = CommandType.Text;
            //                DLdb.SQLST4.Connection = DLdb.RS4;
            //                SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

            //                if (theSqlDataReader4.HasRows)
            //                {
            //                    theSqlDataReader4.Read();
            //                    staffname = theSqlDataReader4["fname"].ToString() + " " + theSqlDataReader4["lname"].ToString();
            //                    staffid = theSqlDataReader4["UserID"].ToString();
            //                    staffEmail = theSqlDataReader4["email"].ToString();
            //                    staffCell = theSqlDataReader4["Contact"].ToString();
            //                }

            //                if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
            //                DLdb.SQLST4.Parameters.RemoveAt(0);
            //                DLdb.RS4.Close();
            //            }

            //            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            //            DLdb.SQLST3.Parameters.RemoveAt(0);
            //            DLdb.RS3.Close();

            //            string regno = "";
            //            string name = "";
            //            string workphone = "";
            //            string email = "";
            //            string address = "";
            //            DLdb.RS3.Open();
            //            DLdb.SQLST3.CommandText = "select * from Auditor where UserID = @UserID";
            //            DLdb.SQLST3.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
            //            DLdb.SQLST3.CommandType = CommandType.Text;
            //            DLdb.SQLST3.Connection = DLdb.RS3;
            //            theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            //            if (theSqlDataReader3.HasRows)
            //            {
            //                theSqlDataReader3.Read();
            //                regno = theSqlDataReader3["regno"].ToString();
            //                name = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
            //                workphone = theSqlDataReader3["phonework"].ToString();
            //                email = theSqlDataReader3["email"].ToString();
            //                address = theSqlDataReader3["addressline1"].ToString() + "<br/> " + theSqlDataReader3["addressline2"].ToString() + "<br/> " + theSqlDataReader3["citysuburb"].ToString() + "<br/> " + theSqlDataReader3["province"].ToString() + "<br/> " + theSqlDataReader3["areacode"].ToString();
            //            }

            //            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            //            DLdb.SQLST3.Parameters.RemoveAt(0);
            //            DLdb.RS3.Close();

            //            // GET THE PERIOD YY AND MM
            //            string srtMM = DateTime.Now.Month.ToString();
            //            string srtYY = DateTime.Now.Year.ToString();

            //            invID = theSqlDataReader["AuditID"].ToString();
            //            total = "850"; // theSqlDataReader["TotalAmount"].ToString();

            //            // CREATE THE PDF INVOICE
            //            htmlContent = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
            //                                            "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
            //                                            "        <tr>" +
            //                                            "            <td>" +
            //                                            "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
            //                                            "                    <tr>" +
            //                                            // "                        <td align='left' valign='top'><div style='width:100%;padding:10px;padding-top:10px'><br />To: " + UserName + "<br /></div></td>" +
            //                                            "                    <td align='center' colspan='2' width='60%'><img src='https://197.242.82.242/inspectit/assets/img/cardlogo.jpg'/></td>" +
            //                                            "                    <td align='left' width='40%'><font style='font-size:26px;'><b>INVOICE No. : IVN00" + invID + "</b><br />Period: </b>" + srtMM + "-" + srtYY + "</font></td></tr>" +
            //                                            "                    <tr>" +
            //                                            //"                        <td align='left' width='15%' valign='top'><div style='width:100%;padding:10px;padding-top:10px'><br />To: <br /></div></td>" +
            //                                            "                        <td align='left' width='70%'><br /><h4>To :</h4>" + name + "<br/>"+ workphone + "<br/>" + email + "<br/>" + regno + "</td>" +
            //                                            "                        <td align='left' width='15%' colspan='2'><br /><h4>Address :</h4>" + address + "</td>" +
            //                                            "                    </tr>" +
            //                                            "                    <tr>" +
            //                                            "                        <td align='left' colspan='3' valign='top'>" +
            //                                            "                            <table border='0' cellpadding='5px' cellspacing='0' width='100%'>" +
            //                                            "                               <tr>" +
            //                                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>Audit No.</td>" +
            //                                            "                                   <td width='15%' colspan='2' style=\"border: 1px solid #E5E5E5;\" valign='middle'>COC Type</td>" +
            //                                            "                                </tr>" +
            //                                            "                               <tr>" +
            //                                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + invID + "</td>" +
            //                                            "                                   <td width='15% colspan='2' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + cocType + "</td>" +
            //                                            "                                </tr>" +
            //                                            "                               <tr>" +
            //                                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>Total Amount<b></td>" +
            //                                            "                                   <td width='15%' colspan='2' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + total + "</b></td>" +
            //                                            "                                </tr>" +
            //                                            "                            </table>" +
            //                                            "                        </td>" +
            //                                            "                    </tr>" +
            //                                            "                    <tr>" +
            //                                            "                       <td align='middle'><b>Powered By InspectIT</b></td>" +
            //                                            "                        <td><td align='left'><img src='https://197.242.82.242/inspectit/assets/img/logo.png'/></td>"+
            //                                            "                    </tr>" +
            //                                            "                </table>" +
            //                                            "            </td>" +
            //                                            "        </tr>" +
            //                                            "    </table>" +
            //                                            "</body>");

            //            filename = "invoice_" + invID + "_" + srtMM + "-" + srtYY + ".pdf";
            //            var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(htmlContent);
            //            string path = Server.MapPath("~/inspectorCOCDocs/") + filename;
            //            File.WriteAllBytes(path, pdfBytes);


            //        }
            //    }

            //    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.RS.Close();


            //    DLdb.RS.Open();
            //    DLdb.SQLST.CommandText = "update COCInspectors set Invoice=@Invoice,Description=@Description where AuditID=@AuditID";
            //    DLdb.SQLST.Parameters.AddWithValue("Invoice", filename);
            //    DLdb.SQLST.Parameters.AddWithValue("Description", cocType);
            //    DLdb.SQLST.Parameters.AddWithValue("AuditID", invID);
            //    DLdb.SQLST.CommandType = CommandType.Text;
            //    DLdb.SQLST.Connection = DLdb.RS;
            //    theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.RS.Close();

            //    if (CustomerCellNo.ToString() != "")
            //    {
            //        DLdb.sendSMS(staffid, staffCell, "Inspect-It: COC Statement Number: " + cocStatementNo + " Has been audited and requires your attention");
            //    }

            //    // EMAIL
            //    if (CustomerEmail.ToString() != "")
            //    {
            //        string eHTMLBody = "Dear " + staffname + "<br /><br />COC Statement Number: " + cocStatementNo + " Has been audited and requires your attention, <br/><br/> Please <a href='https://197.242.82.242/inspectit/'>login</a> to view your reifx.<br /><br />Regards<br />Inspect-It Administrator";
            //        string eSubject = "Inspect-IT C.O.C Statement Refix";
            //        DLdb.sendEmail(eHTMLBody, eSubject, "mathewpayne@gmail.com", staffEmail, "");
            //    }
            //}
            //else
            //{
            //    // CREATE INVOICE
            //    var htmlContent = "";
            //    string invID = "";
            //    string total = "";
            //    string UserName = "";
            //    string UserEmail = "";
            //    string cocType = "";
            //    string filename = "";

            //    DLdb.RS.Open();
            //    DLdb.SQLST.CommandText = "update COCStatements set DateRefix = @RefixDate where COCStatementID = @COCStatementID";
            //    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            //    DLdb.SQLST.Parameters.AddWithValue("RefixDate", NoDaysToComplete.Text.ToString());

            //    DLdb.SQLST.CommandType = CommandType.Text;
            //    DLdb.SQLST.Connection = DLdb.RS;
            //    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.RS.Close();

            //    // GET SITES
            //    DLdb.RS.Open();
            //    DLdb.SQLST.CommandText = "select * from COCInspectors where UserID=@UserID";
            //    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            //    DLdb.SQLST.CommandType = CommandType.Text;
            //    DLdb.SQLST.Connection = DLdb.RS;
            //    theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //    if (theSqlDataReader.HasRows)
            //    {
            //        while (theSqlDataReader.Read())
            //        {

            //            DLdb.RS3.Open();
            //            DLdb.SQLST3.CommandText = "select * from users where userid = @UserID";
            //            DLdb.SQLST3.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            //            DLdb.SQLST3.CommandType = CommandType.Text;
            //            DLdb.SQLST3.Connection = DLdb.RS3;
            //            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            //            if (theSqlDataReader3.HasRows)
            //            {
            //                theSqlDataReader3.Read();
            //                UserName = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
            //                UserEmail = theSqlDataReader3["email"].ToString();
            //            }

            //            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            //            DLdb.SQLST3.Parameters.RemoveAt(0);
            //            DLdb.RS3.Close();

            //            DLdb.RS3.Open();
            //            DLdb.SQLST3.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID";
            //            DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
            //            DLdb.SQLST3.CommandType = CommandType.Text;
            //            DLdb.SQLST3.Connection = DLdb.RS3;
            //            theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            //            if (theSqlDataReader3.HasRows)
            //            {
            //                theSqlDataReader3.Read();
            //                cocType = theSqlDataReader3["Type"].ToString();
            //            }

            //            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            //            DLdb.SQLST3.Parameters.RemoveAt(0);
            //            DLdb.RS3.Close();

            //            // GET THE PERIOD YY AND MM
            //            string srtMM = DateTime.Now.Month.ToString();
            //            string srtYY = DateTime.Now.Year.ToString();

            //            invID = theSqlDataReader["AuditID"].ToString();
            //            total = theSqlDataReader["TotalAmount"].ToString();

            //            // CREATE THE PDF INVOICE
            //            htmlContent = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
            //                                            "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
            //                                            "        <tr>" +
            //                                            "            <td>" +
            //                                            "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
            //                                            "                    <tr><td></td>" +
            //                                            // "                        <td align='left' valign='top'><div style='width:100%;padding:10px;padding-top:10px'><br />To: " + UserName + "<br /></div></td>" +
            //                                            "                        <td align='center'><img src='https://197.242.82.242/inspectit/assets/img/logo_2x.png'/></td>" +
            //                                            "                    <td></td></tr>" +
            //                                            "                    <tr>" +
            //                                            "                        <td align='left' valign='top'><div style='width:100%;padding:10px;padding-top:10px'><br />To: " + UserName + "<br /></div></td>" +
            //                                            "                        <td align='right'><br /><font style='font-size:26px;'><b>INVOICE No. : IVN00" + invID + "</b><br />Period: </b>" + srtMM + "-" + srtYY + "</font></td>" +
            //                                            "                    </tr>" +
            //                                            "                    <tr>" +
            //                                            "                        <td align='left' colspan='2' height='800px' valign='top'>" +
            //                                            "                            <table border='0' cellpadding='5px' cellspacing='0' width='100%'>" +
            //                                            "                               <tr>" +
            //                                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>Audit No.</td>" +
            //                                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>COC Type</td>" +
            //                                            "                                </tr>" +
            //                                            "                               <tr>" +
            //                                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + invID + "</td>" +
            //                                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + cocType + "</td>" +
            //                                            "                                </tr>" +
            //                                            "                               <tr>" +
            //                                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>Total Amount<b></td>" +
            //                                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + total + "</b></td>" +
            //                                            "                                </tr>" +
            //                                            "                            </table>" +
            //                                            "                        </td>" +
            //                                            "                    </tr>" +
            //                                            "                    <tr>" +
            //                                            "                        <td colspan='2'><br /><br /><table border='0' cellpadding='3px' cellspacing='0' width='100%'><tr><td align='left'><img src='https://197.242.82.242/inspectit/assets/img/logo.png'/></td><td valign='middle' align='right'><b>InspectIT Team</b></td></tr></table></td>" +
            //                                            "                    </tr>" +
            //                                            "                </table>" +
            //                                            "            </td>" +
            //                                            "        </tr>" +
            //                                            "    </table>" +
            //                                            "</body>");

            //            filename = "invoice_" + invID + "_" + srtMM + "-" + srtYY + ".pdf";
            //            var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(htmlContent);
            //            string path = Server.MapPath("~/inspectorCOCDocs/") + filename;
            //            File.WriteAllBytes(path, pdfBytes);


            //        }
            //    }

            //    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.RS.Close();


            //    DLdb.RS.Open();
            //    DLdb.SQLST.CommandText = "update COCInspectors set Invoice = @Invoice, Description=@Description where AuditID=@AuditID";
            //    DLdb.SQLST.Parameters.AddWithValue("Invoice", filename);
            //    DLdb.SQLST.Parameters.AddWithValue("Description", cocType);
            //    DLdb.SQLST.Parameters.AddWithValue("AuditID", invID);
            //    DLdb.SQLST.CommandType = CommandType.Text;
            //    DLdb.SQLST.Connection = DLdb.RS;
            //    theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.RS.Close();
            //}



            //REQUIRED: Email the COC User notification




            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("COC Audit Statement Submitted"));
        }

        

        protected void btnAddReview_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddReview?cocid=" + Request.QueryString["cocid"].ToString());
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string NumberTo = "";
            string EmailAddress = "";
            string FullName = "";
            string AuditorID = "";
            string uid = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Auditor where AuditorID = @AuditorID";
            DLdb.SQLST.Parameters.AddWithValue("AuditorID", inspectorList.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    uid = theSqlDataReader["UserID"].ToString();
                    NumberTo = theSqlDataReader["phoneMobile"].ToString();
                    AuditorID = theSqlDataReader["AuditorID"].ToString();
                    EmailAddress = theSqlDataReader["email"].ToString();
                    FullName = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCInspectors where COCStatementID=@COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "update COCInspectors set UserID=@UserID,Status = 'Auditing',isActive='1' where COCStatementID=@COCStatementID";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", uid.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();

            }
            else
            {
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "insert into COCInspectors (UserID, COCStatementID, Status) values (@UserID, @COCStatementID, 'Auditing')";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", uid.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatements set isAudit = '1',status='Auditing',AuditorID=@AuditorID where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("AuditorID", AuditorID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
            DLdb.SQLST.Parameters.AddWithValue("Message", "Certificate re-assigned to auditor: " + FullName);
            DLdb.SQLST.Parameters.AddWithValue("Username", "Admin");
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

            // SMS
            if (NumberTo.ToString() != "")
            {
                DLdb.sendSMS(uid.ToString(), NumberTo, "Inspect-It: You have been assigned to audit a C.O.C, please login to Inspect-It to view.");
            }

            // EMAIL
            if (EmailAddress.ToString() != "")
            {
                string eHTMLBody = "Dear " + FullName + "<br /><br />You have been assigned to a C.O.C Statement on the Inspect-It system, please <a href='https://197.242.82.242/inspectit/'>login</a> to view.<br /><br />Regards<br />Inspect-It Administrator";
                string eSubject = "Inspect-IT New C.O.C Statement Audit";
                DLdb.sendEmail(eHTMLBody, eSubject, "mathewpayne@gmail.com", EmailAddress, "");
            }

            DLdb.DB_Close();
            Response.Redirect("EditCOCStatementAdmin.aspx?cocid=" + Request.QueryString["cocid"].ToString() + "&msg=COC has been reassigned");
        }

        protected void myListDropDown_Change(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Auditor where AuditorID = @AuditorID";
            DLdb.SQLST.Parameters.AddWithValue("AuditorID", inspectorList.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    TextBox4.Text = theSqlDataReader["fName"].ToString() + " " + theSqlDataReader["lName"].ToString();
                    TextBox5.Text = theSqlDataReader["phoneMobile"].ToString();
                    TextBox6.Text = theSqlDataReader["email"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

    }
}