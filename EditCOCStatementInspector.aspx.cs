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
    public partial class EditCOCStatementInspector : System.Web.UI.Page
    {
        public string GlobalPlumberRegNo = "";
        public string GlobalPlumberName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

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

            //if (InspectionDate.Text.ToString() != "")
            //{
                
            //}
            //else
            //{
            //    btnSubmit.Visible = false;
            //    btnSave.Visible = false;
            //    subbtnhideorshow.Visible = false;
            //}

            if (!IsPostBack)
            {
                //DateTime now = DateTime.Now;
                //DateTime soon = DateTime.Now.AddDays(5);
                //InspectionDate.Text = now.ToString("MM/dd/yyyy");
                //NoDaysToComplete.Text = soon.ToString("MM/dd/yyyy");

                string CustomerID = "";
                string name = "";
                string surname = "";
                string plumberUserID = "";
                BlankFormWarning.Visible = false;
                ////REQUIRED: DISABLE SAVE AND SUBMIT
                //btnSave.Enabled = false;
                //btnSubmit.Enabled = false;
                ////REQUIRED: HIDE REFIX UNTIL isRefix = '1'
                DisplayRefixNotice.Visible = false;
                Boolean isClosedRefix = false;
                Boolean isRefixs = false;
                Boolean vatIncl = false;

                

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
                        string newComma = "";
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID";
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            theSqlDataReader2.Read();
                            newComma = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        DateTime dateCreatedViewa = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                        latestCommentPosted.InnerHtml = theSqlDataReader["Comments"].ToString() + "<br/> <small>" + newComma + " - " + dateCreatedViewa.ToString("dd/MM/yyyy") + "</small>";

                        string inspecID = "";
                        // GET USER INSPECTOR DETAILS FOR RIGHT HAND SIDE COMMENTS
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID and Role='Inspector'";
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                inspecID = theSqlDataReader2["UserID"].ToString();
                                DateTime dateCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                                inspectorDatePosted.InnerHtml = dateCreated.ToString("dd/MM/yyyy");
                                name = theSqlDataReader2["fname"].ToString();
                                surname = theSqlDataReader2["lname"].ToString();
                                inspectorName.InnerHtml = name + " " + surname;
                                inspectorComments.InnerHtml += "<p>" + theSqlDataReader["Comments"].ToString() + "</p>";
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        // GET USER DETAILS FOR LEFT HAND SIDE COMMENTS
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID and Role!='Inspector'";
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                DateTime dateCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                                plumberDatePosted.InnerHtml = dateCreated.ToString("dd/MM/yyyy");

                                name = theSqlDataReader2["fname"].ToString();
                                surname = theSqlDataReader2["lname"].ToString();
                                plumberName.InnerHtml = name + " " + surname;
                                PlumberFullName.Text = name + " " + surname;
                                PlumberRegNo.Text = theSqlDataReader2["regno"].ToString();
                                PlumberEmail.Text = theSqlDataReader2["email"].ToString();
                                PlumberContact.Text = theSqlDataReader2["contact"].ToString();
                                PlumberBusContact.Text = theSqlDataReader2["contact"].ToString();
                                plumberComments.InnerHtml += "<p>" + theSqlDataReader["Comments"].ToString() + "</p>";
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

                int countRefixes = 0;
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select count(*) as totalNumber from COCReviews where COCStatementID=@COCStatementID and isClosed='0'";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        countRefixes = Convert.ToInt32(theSqlDataReader["totalNumber"].ToString());
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                int countRefixesFixed = 0;
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select count(*) as totalNumber from COCReviews where COCStatementID=@COCStatementID and isFixed='1' and isClosed='0'";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        countRefixesFixed = Convert.ToInt32(theSqlDataReader["totalNumber"].ToString());
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                //Response.Write(countRefixes);
                //Response.End();

                if (countRefixes == 0 && InspectionDate.Text.ToString() != "")
                {
                    //btnSubmit.Visible = true;
                    subbtnhideorshow.Visible = true;
                }
                else
                {
                    //btnSubmit.Enabled = true;
                }

                if (countRefixesFixed == 0)
                {
                    //btnSubmit.Visible = true;
                    btnSubmit.Enabled = true;
                }
                else
                {
                    btnSubmit.Enabled = false;
                }

                string inspectorsID = "";
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
                        if (theSqlDataReader["isRefix"].ToString() == "True")
                        {
                            isRefixs = true;
                        }
                        plumberUserID = theSqlDataReader["UserID"].ToString();
                        inspectorsID = theSqlDataReader["AuditorID"].ToString();
                        string isInspectorSubmitted = theSqlDataReader["isInspectorSubmitted"].ToString();
                        string isThereRefix = theSqlDataReader["isRefix"].ToString();
                        if (isInspectorSubmitted == "True")
                        {
                            btnSubmit.Visible = false;
                            btnSave.Visible = false;
                        }
                        else
                        {
                            //subbtnhideorshow.Visible = false;

                        }

                        if (theSqlDataReader["isInvoiceSubmitted"].ToString() == "True")
                        {
                            subbtnhideorshow.Visible = false;
                            btnSubmit.Visible = false;
                            btnSave.Visible = false;
                        }

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

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Auditor where AuditorID=@AuditorID";
                DLdb.SQLST.Parameters.AddWithValue("AuditorID", inspectorsID.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["vatregno"].ToString() != "" && theSqlDataReader["vatregno"] != DBNull.Value)
                        {
                            vatIncl = true;
                        }
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                decimal inspectorRate = 0;
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Rates where ID = '39'";
                //DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    inspectorRate = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                decimal vats = 0;
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from settings where ID='1'";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    vats = Convert.ToDecimal(theSqlDataReader["VatPercentage"].ToString());
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                decimal vat = inspectorRate * Convert.ToDecimal(vats);
                if (vatIncl == true)
                {
                    decimal vatElec = inspectorRate + vat;
                    invAmountDisp.InnerHtml = "Your invoice amount is R" + vatElec;
                }
                else
                {
                    invAmountDisp.InnerHtml = "Your invoice amount is R" + inspectorRate;
                }

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", plumberUserID.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        name = theSqlDataReader["fname"].ToString();
                        surname = theSqlDataReader["lname"].ToString();
                        plumberName.InnerHtml = name + " " + surname;
                        GlobalPlumberName = name + " " + surname;
                        GlobalPlumberRegNo = theSqlDataReader["regno"].ToString();
                        PlumberFullName.Text = name + " " + surname;
                        PlumberRegNo.Text = theSqlDataReader["regno"].ToString();
                        PlumberEmail.Text = theSqlDataReader["email"].ToString();
                        PlumberContact.Text = theSqlDataReader["contact"].ToString();
                        PlumberBusContact.Text = theSqlDataReader["contact"].ToString();
                        Image1.ImageUrl = "Photos/" + theSqlDataReader["Photo"].ToString();
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM AuditHistory where COCStatementID = @COCStatementID";
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
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID";
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            theSqlDataReader2.Read();
                            newComm = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        DateTime dateCreatedView = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                        lastprivatecomm.InnerHtml = theSqlDataReader["Comments"].ToString() + "<br/> <small>" + newComm + " - " + dateCreatedView.ToString("dd/MM/yyyy") + "</small>";

                        // GET USER INSPECTOR DETAILS FOR RIGHT HAND SIDE COMMENTS
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID and Role='Inspector'";
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                DateTime dateCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                                inspectorDatePostedpriv.InnerHtml = dateCreated.ToString("dd/MM/yyyy");

                                inspecnamepriv.InnerHtml = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();

                                inspectorCommentspriv.InnerHtml += "<p>" + theSqlDataReader["Comments"].ToString() + "</p>";
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        // GET USER DETAILS FOR LEFT HAND SIDE COMMENTS
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID and Role!='Inspector'";
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                DateTime dateCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                                admindatepost.InnerHtml = dateCreated.ToString("dd/MM/yyyy");

                                adnames.InnerHtml = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                                adminscomm.InnerHtml += "<p>" + theSqlDataReader["Comments"].ToString() + "</p>";
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
                        }
                        else
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
                        COCNumber.InnerHtml = "COC Number: " + theSqlDataReader["COCStatementID"].ToString();

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
                        if (theSqlDataReader["ScheduledDate"] != DBNull.Value && theSqlDataReader["ScheduledDate"].ToString() != "")
                        {
                            DateTime scheduledDate = Convert.ToDateTime(theSqlDataReader["ScheduledDate"].ToString());
                            TextBox3.Text = scheduledDate.ToString("MM/dd/yyyy");
                            TextBox5.Text = scheduledDate.ToString("HH:mm tt");
                        }
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
                        cocStatusUpdate.SelectedValue = theSqlDataReader["Status"].ToString();
                        CustomerID = theSqlDataReader["CustomerID"].ToString();
                        string isRefix = theSqlDataReader["isRefix"].ToString();
                        if (isRefix == "True")
                        {
                            DisplayRefixNotice.Visible = true;
                        }
                        if (theSqlDataReader["DateRefix"] != DBNull.Value)
                        {
                            NoDaysToComplete.Text = Convert.ToDateTime(theSqlDataReader["DateRefix"]).ToString("MM/dd/yyyy");
                        }
                        //if (theSqlDataReader["COCFilename"] != DBNull.Value)
                        //{
                        //    showreportbtn.InnerHtml = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\"><button type=\"button\" ID=\"btnViewPDF\" class=\"btn btn-default\">View Report</button></a>";
                        //}

                        if (theSqlDataReader["AorB"].ToString() == "A")
                        {
                            CheckBoxList1.SelectedValue = "A";
                        }
                        else
                        {
                            CheckBoxList1.SelectedValue = "B";
                        }

                        string cocDisp = "";
                        if (theSqlDataReader["COCFileName"].ToString() != "" && theSqlDataReader["COCFileName"] != DBNull.Value)
                        {
                            cocDisp = "<embed src=\"https://197.242.82.242/inspectit/pdf/" + theSqlDataReader["COCFileName"].ToString() + "\" width=\"700\" height=\"375\" type='application/pdf'>";
                        }
                        else if (theSqlDataReader["PaperBasedCOC"].ToString() != "" && theSqlDataReader["PaperBasedCOC"] != DBNull.Value)
                        {
                            cocDisp = "<img src=\"https://197.242.82.242/inspectit/pdf/" + theSqlDataReader["PaperBasedCOC"].ToString() + "\" style=\"height:375px;\" class=\"img-responsive\"/>";
                        }

                        pdfDisp.InnerHtml = cocDisp;
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

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
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

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

                string LinksHTML = "<label class=\"btn btn-tag btn-danger btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=NON&tid=7&cocid=" + Request.QueryString["cocid"] + "&did=1'\">Non Compliance Notice</label><br />";
                int cnt = 0;
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM COCInstallations where COCStatementID = @COCStatementID and isActive='1'";
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

                // veronike carry on from here
                // GET AUDIT DETAILS
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM COCInspectors where COCStatementID = @COCStatementID and isActive='1'";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        //string InspectionStatus = theSqlDataReader["Status"].ToString();
                        //if (InspectionStatus == "Failure")
                        //{
                        //    chkFailure.Checked = true;
                        //}
                        //else if (InspectionStatus == "Completed")
                        //{
                        //    chkCompleted.Checked = true;
                        //}
                        //else if (InspectionStatus == "Cautionary")
                        //{
                        //    chkCautionary.Checked = true;
                        //}
                        //else if (InspectionStatus == "Complement")
                        //{
                        //    chkComplement.Checked = true;
                        //}
                        //else if (InspectionStatus == "Refixed")
                        //{
                        //    chkRefixed.Checked = true;
                        //}
                        //else
                        //{
                        //    chkRefixed.Checked = true;
                        //}

                        //refixdetails.Text = theSqlDataReader["RefixComments"].ToString();
                        if (theSqlDataReader["InspectionDate"].ToString() == "" || theSqlDataReader["InspectionDate"] == DBNull.Value)
                        {
                            btnSubmit.Visible = false;
                            btnSave.Visible = false;
                            subbtnhideorshow.Visible = false;
                        }
                        InspectionDate.Text = theSqlDataReader["InspectionDate"].ToString();
                        string lQuality = theSqlDataReader["Quality"].ToString();
                        Quality.SelectedIndex = Quality.Items.IndexOf(Quality.Items.FindByValue(lQuality));
                        if (theSqlDataReader["Picture"] != DBNull.Value)
                        {
                            AuditPicture.InnerHtml = "<img src=\"noticeimages/" + theSqlDataReader["Picture"].ToString() + "\" class=\"img-responsive\" style=\"height:200px;\" />";
                        }
                        if (theSqlDataReader["Invoice"].ToString() != "" || theSqlDataReader["Invoice"] != DBNull.Value)
                        {

                            showreportbtn.InnerHtml = "<a href=\"Inspectorinvoices/" + theSqlDataReader["Invoice"].ToString() + "\" target=\"_blank\"><button type=\"button\" ID=\"btnViewPDF\" class=\"btn btn-default\">View Report</button></a>";
                        }

                    }
                }
                else
                {
                    //chkRefixed.Checked = true;
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                // LOAD REVIEWS                
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM COCReviews where COCStatementID = @COCStatementID and isActive = '1' order by createdate desc";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        //if (theSqlDataReader["isClosed"].ToString() == "True")
                        //{
                        //    isClosedRefix = true;
                        //}
                        //else
                        //{
                        //    isClosedRefix = false;
                        //}
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
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

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
                        DLdb.SQLST2.CommandText = "Select * from FormImg where ReviewID = @ReviewID and isReference='0' and isActive='1'";
                        DLdb.SQLST2.Parameters.AddWithValue("ReviewID", theSqlDataReader["ReviewID"].ToString());
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
                                    Media += "<div class=\"col-md-3\" id=\"show_img_" + theSqlDataReader2["ImgID"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader2["imgid"].ToString() + "','" + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                                }
                                else
                                {
                                    Media += "<div class=\"col-md-3\"><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"AuditorImgs/" + filename + "\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                                }
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        string referenceMedia = "";
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "Select * from FormImg where ReviewID = @ReviewID and isReference='1' and isActive='1'";
                        DLdb.SQLST2.Parameters.AddWithValue("ReviewID", theSqlDataReader["ReviewID"].ToString());
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
                                    referenceMedia += "<div class=\"col-md-3\"><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"AuditorImgs/" + filename + "\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                                    //referenceMedia += "<div class=\"col-md-3\" id=\"show_img_" + theSqlDataReader2["ImgID"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader2["imgid"].ToString() + "','"+DLdb.Decrypt(Request.QueryString["cocid"].ToString())+"')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                                }
                                else
                                {
                                    referenceMedia += "<div class=\"col-md-3\"><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img style=\"height:130px;\" src=\"AuditorImgs/" + filename + "\" class=\"img img-responsive img-thumbnail\" /></a></div>";
                                }
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        string btnFix = "";
                        string btnDelete = "";
                        string btnCompleteRefix = "";
                        string btnNotFix = "";
                        string btnEdit = "";

                        // moved the below out of the 1st if
                            btnEdit = "<div class=\"col-md-2 text-right\"><div class=\"btn btn-primary\" onclick=\"document.location.href='EditReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + theSqlDataReader["ReviewID"].ToString() + "'\">Edit</div></div>";
                        btnDelete = "<div class=\"col-md-2 text-right\"><div class=\"btn btn-danger\" onclick=\"document.location.href='DeleteItems?pid=" + Request.QueryString["cocid"].ToString() + "&op=delReviewFromCOC&id=" + theSqlDataReader["ReviewID"].ToString() + "'\">Delete</div></div>";
                        if (theSqlDataReader["isFixed"].ToString() == "True" && theSqlDataReader["status"].ToString() == "Failure" && theSqlDataReader["isClosed"].ToString() == "False")
                        {
                            btnFix = "<input id=\"" + theSqlDataReader["ReviewID"].ToString() + "\" checked name=\"" + theSqlDataReader["ReviewID"].ToString() + "\" value=\"" + theSqlDataReader["ReviewID"].ToString() + "\" onclick=\"markCompleted('" + theSqlDataReader["ReviewID"].ToString() + "','" + theSqlDataReader["COCStatementID"].ToString() + "')\" type=\"checkbox\"/> Mark As Fixed";
                            btnDelete = "";
                            btnCompleteRefix = "<div class=\"col-md-2 text-right\"><label class=\"btn btn-primary\" onclick=\"markCompleteded('" + theSqlDataReader["ReviewID"].ToString() + "','" + theSqlDataReader["COCStatementID"].ToString() + "')\">Complete</label></div>";
                            // btnNotFix = "<label class=\"btn btn-danger\" onclick=\"markNotCompleted('" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "','" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "')\">Mark as Refix</label>";
                        }
                        else if (theSqlDataReader["isFixed"].ToString() == "True" && theSqlDataReader["status"].ToString() == "Failure" && theSqlDataReader["isClosed"].ToString() == "True")
                        {
                            btnEdit = "";
                            btnDelete = "";
                            btnFix = "<label class=\"label label-success\">Refix Complete</label>";
                        }
                        else if (theSqlDataReader["isFixed"].ToString() == "True" && theSqlDataReader["status"].ToString() == "Failure")
                        {
                            btnFix = "<input id=\"" + theSqlDataReader["ReviewID"].ToString() + "\" name=\"" + theSqlDataReader["ReviewID"].ToString() + "\" value=\"" + theSqlDataReader["ReviewID"].ToString() + "\" onclick=\"markCompleted('" + theSqlDataReader["ReviewID"].ToString() + "','" + theSqlDataReader["COCStatementID"].ToString() + "')\" type=\"checkbox\"/> Mark As Fixed";
                            //btnEdit = "<div class=\"col-md-12 text-right\"><div class=\"btn btn-primary\" onclick=\"document.location.href='EditReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "'\">Edit</div></div>";
                        }

                        CurrentReview.InnerHtml += "<div class=\"row alert-" + StatusCol + "\" style=\"padding: 5px; \">" +
                                                  "       <div class=\"col-md-12 text-right\">" +
                                                               btnFix + btnNotFix +
                                                  "     </div>" +
                                                  "       <div class=\"col-md-6 text-left\">" +
                                                  "         <b>Instalation Type:</b> " + InstallationType + "<br />" +
                                                  "         <b>Audit Status:</b> " + theSqlDataReader["status"].ToString() + "" +
                                                  "     </div>" +
                                                  "     <div class=\"col-md-6 text-left\">" +
                                                  //"        <b>Reference:</b> " + theSqlDataReader["name"].ToString() + "" +
                                                  "     </div>" +
                                                  "     <div class=\"col-md-12 text-left\"><b>Comments:</b> " + theSqlDataReader["comment"].ToString() + "</div>" +
                                                  "     <div class=\"col-md-12 text-left\"><b>Media:</b><br />" + Media + "</div>" +
                                                  "     <div class=\"col-md-12 text-left\"><b>Reference:</b> " + theSqlDataReader["Reference"].ToString() + "</div>" +
                                                  "     <div class=\"col-md-12 text-left\"><b>Media:</b><br />" + referenceMedia + "</div>" +
                                                  "     <div class=\"col-md-12 text-right\"><div class=\"col-md-8\"></div>" + btnEdit + btnDelete + btnCompleteRefix + "</div>" +

                                                  " </div><hr />";

                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM COCReviews where COCStatementID = @COCStatementID and isActive = '1' and isClosed='0'";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        isClosedRefix = false;
                    }
                }
                else
                {
                    isClosedRefix = true;
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                if (isRefixs == true && isClosedRefix == false)
                {
                    //btnAddReview.Visible = false;
                    subbtnhideorshow.Visible = false;
                    //NoDaysToComplete.Enabled = false;
                    //NoDaysToComplete.Text = "";
                }

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

              //  btnSave.Enabled = true;
              //  btnSubmit.Enabled = true;

                DLdb.DB_Close();
            }
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
            DLdb.SQLST.Parameters.AddWithValue("ScheduledDate", TextBox3.Text.ToString() + " " + TextBox5.Text.ToString());
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

            Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("COC Statement has been updated"));
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
            DLdb.SQLST.CommandText = "insert into COCStatementDetails (PlumberContacted,ScheduledDate,ClientContacted,COCStatementID,CompletedDate,COCType,InsuranceCompany,PolicyHolder,PolicyNumber,isBank,PeriodOfInsuranceFrom,PeriodOfInsuranceTo) values (@PlumberContacted,@ScheduledDate,@ClientContacted,@COCStatementID,@CompletedDate,@COCType,@InsuranceCompany,@PolicyHolder,@PolicyNumber,@isBank,@PeriodOfInsuranceFrom,@PeriodOfInsuranceTo)";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.Parameters.AddWithValue("CompletedDate", CompletedDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("COCType", COCType.ToString());
            DLdb.SQLST.Parameters.AddWithValue("InsuranceCompany", InsuranceCompany.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PolicyHolder", PolicyHolder.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PolicyNumber", PolicyNumber.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("isBank", isBank.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PeriodOfInsuranceFrom", PeriodOfInsuranceFrom.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PeriodOfInsuranceTo", PeriodOfInsuranceTo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PlumberContacted", plumbercontactedStatus.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ClientContacted", clientContactedStatus.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ScheduledDate", TextBox3.Text.ToString() + " " + TextBox5.Text.ToString());
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

            Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("COC Statement has been updated"));
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

            Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + Request.QueryString["cocid"]);

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

            Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + Request.QueryString["cocid"]);
        }

        protected void addAudHis_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string uid = Session["IIT_UID"].ToString();
            if (TextBox2.Text.ToString() != "")
            {
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
            }


            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + Request.QueryString["cocid"]);
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

            Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + Request.QueryString["cocid"]);
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
            // veronike remove the update of isclosed='1' and add it to submit invoice
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

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "update COCStatements set DateRefix = @DateRefix where COCStatementID = @COCStatementID";
            DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST2.Parameters.AddWithValue("DateRefix", NoDaysToComplete.Text.ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("COC Audit Statement Saved"));
        }

        protected void btnSaveAuditDate_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            // veronike remove the update of isclosed='1' and add it to submit invoice
            string InspectionStatus = "";

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

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "update COCStatements set DateRefix = @RefixDate where COCStatementID = @COCStatementID";
            DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST2.Parameters.AddWithValue("RefixDate", NoDaysToComplete.Text.ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("COC Audit Statement Saved"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string InspectionStatus = "";
            string isReFix = "0";

            
            // CHECK THE PICTURE
            if (FileUpload1.HasFile)
            {
                try
                {

                    // UPLOAD THE PICTURE
                    string filename = Path.GetFileName(FileUpload1.FileName);
                    filename = DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + filename;
                    FileUpload1.SaveAs(Server.MapPath("~/noticeimages/") + filename);
                    //isComplete = '1',CompletedOn = getdate(),
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

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update COCStatements set DateAudited = getdate(),isInspectorSubmitted='1',isPlumberSubmitted='0' where COCStatementID = @COCStatementID";
                    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
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
                //isComplete = '1',CompletedOn = getdate(),
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

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCStatements set DateAudited = getdate(),isInspectorSubmitted='1',isPlumberSubmitted='0' where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }

            if (isReFix == "1")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCStatements set isRefix = '1',isInspectorSubmitted='1',isPlumberSubmitted='0', DateRefix = @RefixDate where COCStatementID = @COCStatementID";
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
                DLdb.SQLST.CommandText = "update COCStatements set DateRefix = @DateRefix,isInspectorSubmitted='1',isPlumberSubmitted='0' where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.Parameters.AddWithValue("DateRefix", NoDaysToComplete.Text.ToString());
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

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "SELECT * FROM COCReviews where COCStatementID = @COCStatementID and isActive = '1' order by createdate desc";
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
                    var html_FIELD_Content = "";

                    if (theSqlDataReader2["status"].ToString() == "Failure")
                    {
                        backCol = "#F8D7DA";
                        StatusCol = "danger";
                    }
                    else if (theSqlDataReader2["status"].ToString() == "Cautionary")
                    {
                        backCol = "#FFF3CD";
                        StatusCol = "warning";
                    }
                    else if (theSqlDataReader2["status"].ToString() == "Compliment")
                    {
                        backCol = "#D4EDDA";
                        StatusCol = "success";
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

                    int countImga = 0;
                    string Media = "";

                    string HTMLContentImgs = "<table border='0' cellpadding='10px' cellspacing='0' width='100%'><tr>";

                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "Select * from FormImg where ReviewID = @ReviewID and UserID=@UserID and isReference='0'";
                    DLdb.SQLST3.Parameters.AddWithValue("ReviewID", theSqlDataReader2["ReviewID"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        while (theSqlDataReader3.Read())
                        {

                            if (theSqlDataReader3["imgsrc"].ToString() != "" && theSqlDataReader3["imgsrc"] != DBNull.Value)
                            {
                                string filename = theSqlDataReader3["imgsrc"].ToString();

                                if (countImga == 3)
                                {
                                    HTMLContentImgs += "</tr><tr>";
                                    countImga = 0;
                                }

                                HTMLContentImgs += "<td>" +
                                   "<img src=\"https://197.242.82.242/inspectit/AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img-thumbnail img img-responsive\" style=\"height:100px;\" />" +
                                   "</td>";

                                countImga++;

                            }
                            else
                            {
                                Media += "";
                            }

                        }
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();

                    string refImgsw = "";
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "Select * from FormImg where ReviewID = @ReviewID and UserID=@UserID and isReference='1'";
                    DLdb.SQLST3.Parameters.AddWithValue("ReviewID", theSqlDataReader2["ReviewID"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        while (theSqlDataReader3.Read())
                        {

                            if (theSqlDataReader3["imgsrc"].ToString() != "" && theSqlDataReader3["imgsrc"] != DBNull.Value)
                            {
                                string filename = theSqlDataReader3["imgsrc"].ToString();

                                if (countImga == 3)
                                {
                                    HTMLContentImgs += "</tr><tr>";
                                    countImga = 0;
                                }

                                refImgsw += "<td>" +
                                   "<img src=\"https://197.242.82.242/inspectit/AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img-thumbnail img img-responsive\" style=\"height:100px;\" />" +
                                   "</td>";

                                countImga++;

                            }
                            else
                            {
                                Media += "";
                            }

                        }
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();
                    
                    html_FIELD_Content = "<table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                        "<tr>" +
                        "   <td colspan='2' style=\"text-align:center;\"><b>" + InstallationType + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                        "   <td><b>Audit Status:</b></td>" +
                        "   <td>" + theSqlDataReader2["status"].ToString() + "</td>" +
                        "</tr>" +

                        "<tr>" +
                        "   <td><b>Reference:</b></td>" +
                        "   <td>" + theSqlDataReader2["Reference"].ToString() + "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "   <td colspan='2'>" + refImgsw + "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "   <td><b>Comments:</b></td>" +
                        "   <td>" + theSqlDataReader2["comment"].ToString() + "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "   <td colspan='2'>" + HTMLContentImgs + "</tr></table></td>" +
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
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

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
                // usignature = "<img src=\"https://197.242.82.242/inspectit/signatures/" + theSqlDataReader2["signature"].ToString() + "\" />";
                usignature = sigImg;
                uaddress = theSqlDataReader2["ResidentialStreet"].ToString() + " " + theSqlDataReader2["ResidentialSuburb"].ToString() + " " + theSqlDataReader2["ResidentialCity"].ToString() + "  " + theSqlDataReader2["Province"].ToString() + " " + theSqlDataReader2["ResidentialCode"].ToString();
                ucontact = theSqlDataReader2["fname"].ToString();
                uregno = theSqlDataReader2["regno"].ToString();
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            string plumberid = "";
            string refixTrue = "false";
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
                    if (theSqlDataReader2["isRefix"].ToString() == "True")
                    {
                        refixTrue = "true";
                    }
                    plumberid = theSqlDataReader2["UserID"].ToString();
                    CustomerID = theSqlDataReader2["CustomerID"].ToString();
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            string plumberemail = "";
            string plumbername = "";
            string plumbercontact = "";
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select * from Users where UserID = @UserID";
            DLdb.SQLST2.Parameters.AddWithValue("UserID", plumberid);
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                theSqlDataReader2.Read();
                plumbername = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                plumberemail = theSqlDataReader2["email"].ToString();
                plumbercontact = theSqlDataReader2["contact"].ToString();
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
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
            string heatpump = "";
            string solarwaterHeating = "";
            foreach (ListItem item in TypeOfInstallation.Items)
            {
                if (item.Selected)
                {
                    // item.Value.ToString()
                    if (item.Value.ToString() == "1")
                    {
                        hotwatersystemstick = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }

                    if (item.Value.ToString() == "5")
                    {
                        belowground = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }

                    if (item.Value.ToString() == "2")
                    {
                        coldwatersystemstick = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }
                    if (item.Value.ToString() == "6")
                    {
                        aboveground = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }
                    if (item.Value.ToString() == "3")
                    {
                        sanitaryware = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }
                    if (item.Value.ToString() == "7")
                    {
                        rainwater = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }
                    if (item.Value.ToString() == "4")
                    {
                        solarwaterHeating = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }
                    if (item.Value.ToString() == "8")
                    {
                        heatpump = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }
                }
            }

            //var htmlContent = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
            //                                    "<h2 style='text-align:center;font-size:50px;color:#735b41;'>PLUMBING CERTIFICATE OF COMPLIANCE</h2>" +
            //                                    "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
            //                                    "        <tr>" +
            //                                    "            <td>" +
            //                                    "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
            //                                    "                    <tr>" +
            //                                    "                        <table border='0' width='100%'><tr>" +
            //                                    "                           <td>" +
            //                                    "                               <img src='https://197.242.82.242/inspectit/assets/img/cardlogo.jpg' />" +
            //                                    "                           </td><td>" +
            //                                    "                               <div style='width:100%;'><div style='width:80%;background-color:#ccc;top:10;float:right;border: 1px solid #E5E5E5;padding:10px'> COC Number: " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " </div ></div ><br /><br /><br />" +
            //                                    "                               <div style='width:100%;'><div style='width:80%;background-color:pink;top:10;float:right;border: 1px solid #E5E5E5;padding:10px'> ONLY PIRB REGISTERED LICENSED PLUMBERS ARE AUTHORISED TO ISSUE THIS PLUMBING CERTIFICATE OF COMPLIANCE </div></div><br/><br /><br /><br /> " +
            //                                    "                               <div style='width:100%;'><div style='width:80%;background-color:red;color:white;top:10;float:right;float:right;border: 1px solid #E5E5E5;padding:10px'> TO VERIFY AND AUTHENTICATE THIS CERTIFICATE OF COMPLIANCE VISIT PIRB.CO.ZA AND CLICK ON VERIFY / AUTHENTICATE LINK </div></div> " +
            //                                    "                           </td></tr></table>" +
            //                                    "                            <br /><br />" +
            //                                    //"                        <div>" + clientdetails + "</div>" +
            //                                    // "                        <div style='width:100%;'><div style='background-color:#ccc;position:absolute;top:10;float:right;border: 1px solid #E5E5E5;padding:10px'>COC Number: " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + "</div></div></td>" +
            //                                    "                    </tr>" +
            //                                    "                    <tr>" +
            //                                    "                    <td bgcolor='lightgreen'>" +
            //                                    "                       <h4 style='background-color:lightgreen;text-align:center;'>Physical Address Details of Installation</h4>" +
            //                                    //"                        <div>" + clientdetails + "</div>" +
            //                                    "                    </td>" +
            //                                    "                    </tr>" +
            //                                    "                    <tr>" +
            //                                    "                    <td>" +
            //                                    "                        " + clientdetails + "" +
            //                                    "                    </td>" +
            //                                    "                    </tr>" +
            //                                    "                    <tr>" +
            //                                    "                        <table border='0' width='100%'><tr>" +
            //                                    "                           <td bgcolor='lightgreen' width='80%'>" +
            //                                    "                               <h4 style='background-color:lightgreen;text-align:center;'>Type of Installation Carried Out by Licensed Plumber <br /><span>(Clearly tick the appropriate Installation Category Code and complete the installation details below)</span></h4>" +
            //                                    "                               " +
            //                                    "                           </td><td width='10%'>" +
            //                                    "                               <h4 style='text-align:center;'>Code</h4>" +
            //                                    "                           </td><td width='10%'>" +
            //                                    "                               <h4 style='text-align:center;'>Tick</h4>" +
            //                                    "                           </td></tr>" +
            //                                    "                       <tr>" +
            //                                    "                           <td style='border-bottom: solid 1px black;'>" +
            //                                    "                               Installation, Replacement and / or Repair of a<span style='color:red;'> Hot Water System </span>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" + hotwatersystemstick + "" +
            //                                    "                          </td></tr>" +
            //                                    "                       <tr>" +
            //                                    "                           <td style='border-bottom: solid 1px black;'>" +
            //                                    "                               Installation, Replacement and / or Repair of a<span style='color:lightblue;'> Cold Water System </span>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" + coldwatersystemstick + "" +
            //                                    "                          </td></tr>" +
            //                                    "                       <tr>" +
            //                                    "                           <td style='border-bottom: solid 1px black;'>" +
            //                                    "                               Installation, Replacement and / or Repair of a<span style='color:blue;'> Sanitary-ware and Sanitary-fittings </span>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" + sanitaryware + "" +
            //                                    "                          </td></tr>" +
            //                                    "                       <tr>" +
            //                                    "                           <td style='border-bottom: solid 1px black;'>" +
            //                                    "                               Installation, Replacement and / or Repair of a<span style='color:brown;'> Below-ground Drainage System </span>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" + belowground + "" +
            //                                    "                          </td></tr>" +
            //                                    "                       <tr>" +
            //                                    "                           <td style='border-bottom: solid 1px black;'>" +
            //                                    "                               Installation, Replacement and / or Repair of a<span style='color:green;'> Above-ground Drainage System </span>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" + aboveground + "" +
            //                                    "                          </td></tr>" +
            //                                     "                       <tr>" +
            //                                    "                           <td style='border-bottom: solid 1px black;'>" +
            //                                    "                               Installation, Replacement and / or Repair of a<span style='color:darkblue;'> Rain Water Disposal System </span>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" + rainwater + "" +
            //                                    "                          </td></tr>" +
            //                                    "                       </table>" +

            //                                    "                    </tr>" +
            //                                    "                    <tr>" +
            //                                    "                        <table border='0' width='100%'><tr>" +
            //                                    "                           <td bgcolor='lightgreen' width='80%'>" +
            //                                    "                               <h4 style='background-color:lightgreen;text-align:center;'>Specialisations: To be Carried Out by Licensed Plumber Only Registered to do the Specialised Word <br /><span>(To Verify and authenticate Licensed Plumbers specialisations visit pirb.co.za)</span></h4>" +
            //                                    "                               " +
            //                                    "                           </td><td width='10%'>" +
            //                                    "                               <h4 style='text-align:center;'>Code</h4>" +
            //                                    "                           </td><td width='10%'>" +
            //                                    "                               <h4 style='text-align:center;'>Tick</h4>" +
            //                                    "                           </td></tr>" +
            //                                    "                       <tr>" +
            //                                    "                           <td style='border-bottom: solid 1px black;'>" +
            //                                    "                               Installation, Replacement and / or Repair of a<span style='color:orange;'> Solar Water Heating System </span>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" +
            //                                    "                          </td></tr>" +
            //                                    "                       <tr>" +
            //                                    "                           <td style='border-bottom: solid 1px black;'>" +
            //                                    "                               Installation, Replacement and / or Repair of a<span style='color:maroon;'> Heat Pump </span>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" +
            //                                    "                           </td><td style='border-bottom: solid 1px black;'>" +
            //                                    "                          </td></tr>" +
            //                                    "                       </table>" +
            //                                    "                    </tr>" +
            //                                    "                       <tr><i>See explanations of the above on the reverse of this certificate</i></tr>" +

            //                                    "                    <tr>" +
            //                                    "                        <table border='0' width='100%'><tr>" +
            //                                    "                           <td bgcolor='lightgreen' colspan='2'>" +
            //                                    "                               <h4 style='background-color:lightgreen;text-align:center;'>Installation Details<br /><span>(Details of the work undertaken or scope of work for which the COC is being issued for)</span></h4>" +
            //                                    "                           </td></tr>" +
            //                                     "                    <tr>" +
            //                                    "                        <td align='middle'>" +
            //                                                                //"                               " + html_FIELD_Content + " " +
            //                                                                Reviews +


            //                                    "                        </td>" +
            //                                    "                    </tr>" +
            //                                    "                       <tr>" +
            //                                    "                           <td>" +
            //                                    "                          </td></tr>" +
            //                                    "                       </table>" +
            //                                    "                    </tr>" +
            //                                    "                    <tr>" +
            //                                    "                        <table border='0' width='100%'><tr>" +
            //                                    "                           <td bgcolor='lightgreen'>" +
            //                                    "                               <h4 style='background-color:lightgreen;text-align:center;'>Pre-Existing Non Compliance* Conditions<br /><span>(Details of any non-compliance of the pre-existing plumbing installation on which work was done that needs to be brought to the attention of owner/user)</span></h4>" +
            //                                    "                           </td></tr>" +
            //                                    "                       <tr>" +
            //                                    "                           <td>" +
            //                                    "                          </td></tr>" +
            //                                    "                       <tr>" +
            //                                    "                           <td>" +
            //                                    "                          </td></tr>" +
            //                                    "                       </table>" +
            //                                    "                    </tr>" +
            //                                    "                    <tr>" +
            //                                    "                        <table border='0' width='100%'>" +
            //                                    "                       <tr> " +
            //                                    "                           <td width='10%'>" +
            //                                    "                          </td>" +
            //                                    "                           <td width='90%'>" +
            //                                    "                           <p>I " + uname + " (Licensed Plumber's Name and Surname), Licensed registration number " + uregno + ", certify that, " +
            //                                    "                                   the above compliance certificate details are true and correct and will be logged in accordance with the prescribed requirements as defined by the PIRB." +
            //                                    "                                   I further certify that; " +
            //                                    "                                   <br />Delete either <b>A</b> or <b>B</b> as appropriate</p>" +
            //                                    "                          </td></tr>" +
            //                                    "                       <tr>" +
            //                                    "                       " + seltype + "    " +
            //                                    "                       <tr>" +
            //                                    "                           <td width='10%'></td> <td width='90%'>" +
            //                                    "                               Signed (Licensed Plumber): " + usignature + "" +
            //                                    "                          </td></tr>" +
            //                                    "                       </table>" +
            //                                    "                    </tr><br/><br/><br/><br/><br/><br/><br/><br/><br/>" +
            //                                    "                       </table>" +
            //                                    "                    </tr><br/><br/><br/><br/><br/><br/><br/><br/><br/>" +
            //                                    "                </table>" +
            //                                    "            </td>" +
            //                                    "        </tr>" +
            //                                    "    </table>" +
            //                                    "</body>");

            var htmlContent = String.Format("" +
                "<body style='font-family:Calibri;font-size:11pt;color:black; border: 4px solid #00AEEF; border-radius: 15px; padding: 20px;'><h2 style='text-align:center;font-size:60px;color:#735b41; font-weight: 300;'>PLUMBING CERTIFICATE OF COMPLIANCE</h2>    " +

    "<table border='0' cellpadding='10px' cellspacing='0' width='100%'>        <tr>            <td>     " +
    "<table border='0' cellpadding='10px' cellspacing='0' width='100%'>                    <tr>             " +
    "<table border='0' width='100%'><tr>                           <td>                               <img src='http://pirb.co.za/wp-content/uploads/2018/09/cardlogo.jpg' />                           </td>" +
    "	<td>                              	 <div style='width:100%;'><div style='width:80%;background-color:#D1D3D4;top:10;float:right;border: 1px solid #E5E5E5;padding:10px'> COC Number: "+ DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + "</div ></div >" +
    "	<br /><br /><br />          " +
    "	<div style='width:100%;'><div style='width:80%;background-color:#F3CFC1;top:10;float:right;border: 1px solid #E5E5E5;padding:10px'><center> ONLY PIRB REGISTERED LICENSED PLUMBERS ARE AUTHORISED TO ISSUE THIS PLUMBING CERTIFICATE OF COMPLIANCE </center></div></div>" +

    "	<br/><br /><br /><br />      " +
    "	<div style='width:100%;'><div style='width:80%;background-color:#D2232A;color:white;top:10;float:right;float:right;border: 1px solid #E5E5E5;padding:10px'><center> TO VERIFY AND AUTHENTICATE THIS CERTIFICATE OF COMPLIANCE VISIT PIRB.CO.ZA AND CLICK ON VERIFY / AUTHENTICATE LINK </center></div></div>                            </td></tr></table>    " +
    "	<br /><br /></tr><tr><td bgcolor='#D1E28D' style=\"border: 1px solid #939598;\"><h4 style='background-color:#D1E28D;text-align:center; margin-top: 8px; margin-bottom: 5px; font-size: 20px;'>Physical Address Details of Installation</h4>                    </td>                    </tr>        " +
    "	<tr><td style=\"border: 1px solid #939598;\">"+ clientdetails + "</td>                    </tr>     " +
    "	<tr><table border='0' width='100%' style=\"margin-top: 10px;\"><tr><td bgcolor='#D1E28D' width='80%' style=\"border: 1px solid #939598;\"><h4 style='background-color:#D1E28D;text-align:center; margin-top: 8px; margin-bottom: 5px; font-size: 20px;'>Type of Installation Carried Out by Licensed Plumber <br /><span style=\"font-size: 15px;\">(Clearly tick the appropriate Installation Category Code and complete the installation details below)</span></h4>                                                          </td><td width='10%' style=\"border: 1px solid #939598;\">                               <h4 style='text-align:center; font-size: 20px; margin-top: 8px; margin-bottom: 5px;'>Code</h4>                           </td><td width='10%' style=\"border: 1px solid #939598;\">                               <h4 style='text-align:center; font-size: 20px; margin-top: 8px; margin-bottom: 5px;'>Tick</h4>                           </td></tr>                       <tr>    " +
    "	<td style='border: 1px solid #939598; padding: 5px;'>Installation, Replacement and / or Repair of a<span style='color:red;'> Hot Water System </span></td><td style='border: 1px solid #939598; padding: 5px;'></td><td style='border: 1px solid #939598; padding: 5px;'>"+ hotwatersystemstick + "</td></tr>     " +
    "	<tr><td style='border: 1px solid #939598; padding: 5px;'>Installation, Replacement and / or Repair of a<span style='color:lightblue;'> Cold Water System </span></td><td style='border: 1px solid #939598; padding: 5px;'></td><td style='border: 1px solid #939598; padding: 5px;'>"+coldwatersystemstick+"</td></tr>   " +
    "	<tr><td style='border: 1px solid #939598; padding: 5px;'>Installation, Replacement and / or Repair of a<span style='color:blue;'> Sanitary-ware and Sanitary-fittings </span></td><td style='border: 1px solid #939598; padding: 5px;'></td><td style='border: 1px solid #939598; padding: 5px;'>"+sanitaryware+"</td></tr>              " +
    "	<tr><td style='border: 1px solid #939598; padding: 5px;'>Installation, Replacement and / or Repair of a<span style='color:brown;'> Below-ground Drainage System </span></td><td style='border: 1px solid #939598; padding: 5px;'></td><td style='border: 1px solid #939598; padding: 5px;'>"+belowground+"</td></tr>" +
    "	<tr><td style='border: 1px solid #939598; padding: 5px;'>Installation, Replacement and / or Repair of a<span style='color:green;'> Above-ground Drainage System </span></td><td style='border: 1px solid #939598; padding: 5px;'></td><td style='border: 1px solid #939598; padding: 5px;'>"+aboveground+"</td></tr>" +
    "	<tr><td style='border: 1px solid #939598; padding: 5px;'>Installation, Replacement and / or Repair of a<span style='color:darkblue;'> Rain Water Disposal System </span></td><td style='border: 1px solid #939598; padding: 5px;'></td><td style='border: 1px solid #939598; padding: 5px;'>"+rainwater+"</td></tr>                       </table>                    </tr>       " +

    "	<tr><table border='0' width='100%' style=\"margin-top: 10px;\"><tr><td bgcolor='#D1E28D' width='80%' style=\"border: 1px solid #939598;\"><h4 style='background-color:#D1E28D;text-align:center; margin-top: 8px; margin-bottom: 5px; font-size: 20px;'>Specialisations: To be Carried Out by Licensed Plumber Only Registered to do the Specialised Word <br /><span style=\"font-size: 15px;\">(To Verify and authenticate Licensed Plumbers specialisations visit pirb.co.za)</span></h4>                                                          </td><td width='10%' style=\"border: 1px solid #939598;\">                               <h4 style='text-align:center; font-size: 20px; margin-top: 8px; margin-bottom: 5px;'>Code</h4>                           </td><td width='10%' style=\"border: 1px solid #939598;\">                               <h4 style='text-align:center; font-size: 20px; margin-top: 8px; margin-bottom: 5px;'>Tick</h4>                           </td></tr>        " +
    "	<tr><td style='border: 1px solid #939598; padding: 5px;'>Installation, Replacement and / or Repair of a<span style='color:orange;'> Solar Water Heating System </span></td><td style='border: 1px solid #939598; padding: 5px;'></td><td style='border: 1px solid #939598; padding: 5px;'>"+ solarwaterHeating + "</td></tr>  " +
    "	<tr><td style='border: 1px solid #939598; padding: 5px;'>Installation, Replacement and / or Repair of a<span style='color:maroon;'> Heat Pump </span></td><td style='border: 1px solid #939598; padding: 5px;'></td><td style='border: 1px solid #939598; padding: 5px;'>"+ heatpump + "</td></tr>                       </table>                    </tr>                       <tr><i>See explanations of the above on the reverse of this certificate</i></tr>     " +
    "	<tr><table border='0' width='100%' style=\"margin-top: 10px;\"><tr><td bgcolor='#D1E28D' colspan='2' style=\"border: 1px solid #939598;\"><h4 style='background-color:#D1E28D;text-align:center; margin-top: 8px; margin-bottom: 5px; font-size: 20px;'>Installation Details<br /><span style=\"font-size: 15px;\">Details of the work undertaken or scope of work for which the COC is being issued for)</span></h4>                           </td></tr>                    " +
    "	<tr><td align='middle'>" +
    "" +
    "" +
    "" +
    Reviews + "</td></tr><tr><td></td></tr></table></tr>    " +
    "	<tr><table border='0' width='100%' style=\"margin-top: 10px;\"><tr><td bgcolor='#D1E28D' style=\"border: 1px solid #939598;\"><h4 style='background-color:#D1E28D;text-align:center; margin-top: 8px; margin-bottom: 5px; font-size: 20px;'>Pre-Existing Non Compliance* Conditions<br /><span style=\"font-size: 15px;\">(Details of any non-compliance of the pre-existing plumbing installation on which work was done that needs to be brought to the attention of owner/user)</span></h4>                           </td></tr>                       <tr>                           <td>                          </td></tr>                       <tr>                           <td>                          </td></tr>                       </table>                    </tr>                    <tr>                        <table border='0' width='100%'>                       <tr>                            <td width='10%'>                          </td>                           <td width='90%'>                           <p>I "+ uname + " (Licensed Plumber's Name and Surname), Licensed registration number "+uregno+", certify that,                                    the above compliance certificate details are true and correct and will be logged in accordance with the prescribed requirements as defined by the PIRB.                                   I further certify that;                                    <br />Delete either <b>A</b> or <b>B</b> as appropriate</p></td></tr>" +
    "<tr>"+ seltype + "<tr><tr><td width='10%'></td> <td width='90%'>Signed (Licensed Plumber):" + usignature + "</td></tr></table></tr><br/><br/><br/><br/><br/><br/><br/><br/><br/>                       </table>                    </tr><br/><br/><br/><br/><br/><br/><br/><br/><br/>                </table>  </body>" +
                "");

            //Response.Write(htmlContent);
            //Response.End();

            var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(htmlContent);
            string path = Server.MapPath("~/Inspectorinvoices/") + filenames;
            File.WriteAllBytes(path, pdfBytes);

            DLdb.RS5.Open();
            DLdb.SQLST5.CommandText = "update COCInspectors set Invoice=@Invoice where COCStatementID=@COCStatementID";
            DLdb.SQLST5.Parameters.AddWithValue("Invoice", filenames);
            DLdb.SQLST5.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST5.CommandType = CommandType.Text;
            DLdb.SQLST5.Connection = DLdb.RS5;
            SqlDataReader theSqlDataReader5 = DLdb.SQLST5.ExecuteReader();

            if (theSqlDataReader5.IsClosed) theSqlDataReader5.Close();
            DLdb.SQLST5.Parameters.RemoveAt(0);
            DLdb.SQLST5.Parameters.RemoveAt(0);
            DLdb.RS5.Close();

            DLdb.sendSMS(plumberid, plumbercontact, "Inspect-It: COC Statement Number: " + DLdb.Decrypt(Request.QueryString["cocid"]) + " Has been audited and requires your attention");

            string eHTMLBody = "Dear Auditor<br /><br />COC Statement Number: " + DLdb.Decrypt(Request.QueryString["cocid"]) + "<br/> Please see attached your report <br/><br /><br />Regards<br />Inspect-It Administrator";
            string eSubject = "Inspect-IT - C.O.C Statement Report";
            DLdb.sendEmail(eHTMLBody, eSubject, "mathewpayne@gmail.com", Session["IIT_EmailAddress"].ToString(), path);

            string aHTMLBody = "Dear " + plumbername + "<br /><br />COC Statement Number: " + DLdb.Decrypt(Request.QueryString["cocid"]) + "<br/> has been audited. See attached.<br/><br /><br />Regards<br />Inspect-It Administrator";
            string aSubject = "Inspect-IT - C.O.C Statement Report";
            DLdb.sendEmail(aHTMLBody, aSubject, "mathewpayne@gmail.com", plumberemail.ToString(), path);

            if (refixTrue == "true")
            {
                string HTMLSubject = "Inspect IT - C.O.C Refix Required";
                string HTMLBody = "Dear " + plumbername.ToString() + "<br /><br />COC Number " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " has been audited and a refix is required.<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Team";
                DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumberemail.ToString(), "");

                DLdb.sendSMS(plumberid.ToString(), plumbercontact.ToString(), "COC Number " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " has been audited and a refix is required.");

            }


            DLdb.DB_Close();

            Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("COC Audit Statement Submitted"));
        }



        protected void btnAddReview_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddReview?cocid=" + Request.QueryString["cocid"].ToString());
        }

        protected void InspectionDate_TextChanged(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            int days = 0;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from settings where ID='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                days = Convert.ToInt32(theSqlDataReader["RefixPeriod"].ToString());
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DateTime dateSelected = Convert.ToDateTime(InspectionDate.Text.ToString());
            NoDaysToComplete.Text = dateSelected.AddDays(days).ToString("MM/dd/yyyy");

            DLdb.DB_Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string invID = invoiceNumber.Text.ToString();
            Boolean submit = false;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCInspectors where InvoiceNumber = @InvoiceNumber and UserID = @UserID and isactive='1'";
            DLdb.SQLST.Parameters.AddWithValue("InvoiceNumber", invID);
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                submit = false;
            }
            else
            {
                submit = true;
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            if (submit == false)
            {
                errormsg.Visible = true;
                errormsg.InnerHtml = "You have already used this invoice number, please generate another one.";
            }
            else
            {
                // CREATE INVOICE
                var htmlContent = "";
                string UserName = "";
                string UserEmail = "";
                string cocType = "";
                string filename = "";
                string uid = "";
                string Bank = "";
                string UserContact = "";
                string registrationNumber = "";

                //isComplete = '1',CompletedOn = getdate(),
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCInspectors set isComplete = '1',CompletedOn = getdate() where COCStatementID = @COCStatementID and UserID = @UserID and isactive='1'";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update cocstatements set status = 'Completed',isInvoiceSubmitted='1',isInspectorSubmitted='1' where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS3.Open();
                DLdb.SQLST3.CommandText = "select * from users where userid = @UserID";
                DLdb.SQLST3.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST3.CommandType = CommandType.Text;
                DLdb.SQLST3.Connection = DLdb.RS3;
                SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                if (theSqlDataReader3.HasRows)
                {
                    theSqlDataReader3.Read();
                    UserName = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
                    UserEmail = theSqlDataReader3["email"].ToString();
                    UserContact = theSqlDataReader3["contact"].ToString();
                    uid = theSqlDataReader3["UserID"].ToString();
                    registrationNumber = theSqlDataReader3["regno"].ToString();
                }

                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                DLdb.SQLST3.Parameters.RemoveAt(0);
                DLdb.RS3.Close();

                string pic = "";
                string vatregno = "";
                DLdb.RS3.Open();
                DLdb.SQLST3.CommandText = "select * from Auditor where userid = @UserID";
                DLdb.SQLST3.Parameters.AddWithValue("UserID", uid);
                DLdb.SQLST3.CommandType = CommandType.Text;
                DLdb.SQLST3.Connection = DLdb.RS3;
                theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                if (theSqlDataReader3.HasRows)
                {
                    theSqlDataReader3.Read();
                    if (theSqlDataReader3["PhotoFile"].ToString() != "" && theSqlDataReader3["PhotoFile"] != DBNull.Value)
                    {
                        pic = "<img src='https://197.242.82.242/inspectit/AuditorImgs/" + theSqlDataReader3["CompanyLogo"].ToString() + "' style=\"height:200px;\"/>";
                    }
                    vatregno = theSqlDataReader3["vatregno"].ToString();
                    Bank = "Bank Name: " + DLdb.Decrypt(theSqlDataReader3["BankName"].ToString()) + "<br/> Account Name: " + DLdb.Decrypt(theSqlDataReader3["AccName"].ToString()) + "<br/> Account Number: " + DLdb.Decrypt(theSqlDataReader3["AccNumber"].ToString()) + "<br/> Branch Code: " + DLdb.Decrypt(theSqlDataReader3["branchcode"].ToString()) + "<br/> Account Type: " + DLdb.Decrypt(theSqlDataReader3["AccType"].ToString());
                }

                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                DLdb.SQLST3.Parameters.RemoveAt(0);
                DLdb.RS3.Close();

                DLdb.RS3.Open();
                DLdb.SQLST3.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID";
                DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST3.CommandType = CommandType.Text;
                DLdb.SQLST3.Connection = DLdb.RS3;
                theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                if (theSqlDataReader3.HasRows)
                {
                    theSqlDataReader3.Read();
                    cocType = theSqlDataReader3["Type"].ToString();
                }

                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                DLdb.SQLST3.Parameters.RemoveAt(0);
                DLdb.RS3.Close();

                decimal inspectorRate = 0;
                DLdb.RS3.Open();
                DLdb.SQLST3.CommandText = "select * from Rates where ID = '39'";
                //DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST3.CommandType = CommandType.Text;
                DLdb.SQLST3.Connection = DLdb.RS3;
                theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                if (theSqlDataReader3.HasRows)
                {
                    theSqlDataReader3.Read();
                    inspectorRate = Convert.ToDecimal(theSqlDataReader3["Amount"].ToString());
                }

                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                //DLdb.SQLST3.Parameters.RemoveAt(0);
                DLdb.RS3.Close();

                decimal vats = 0;
                DLdb.RS3.Open();
                DLdb.SQLST3.CommandText = "select * from settings where ID='1'";
                DLdb.SQLST3.CommandType = CommandType.Text;
                DLdb.SQLST3.Connection = DLdb.RS3;
                theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();
                if (theSqlDataReader3.HasRows)
                {
                    theSqlDataReader3.Read();
                    vats = Convert.ToDecimal(theSqlDataReader3["VatPercentage"].ToString());
                }
                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                DLdb.RS3.Close();

                decimal vat = Convert.ToDecimal(vats);
                decimal vatElec = inspectorRate * vat;
                decimal totalWithvat = inspectorRate + vatElec;

                // GET THE PERIOD YY AND MM
                string srtDD = DateTime.Now.Day.ToString();
                string srtMM = DateTime.Now.Month.ToString();
                string srtYY = DateTime.Now.Year.ToString();

                DateTime InvoiceDate = Convert.ToDateTime(InvDate.Text.ToString());

                // CREATE THE PDF INVOICE
                htmlContent = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
                                                "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                "        <tr>" +
                                                "            <td>" +
                                                "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                "                    <tr>" +
                                                 "                        <td align='left' valign='top'><div style='width:100%;padding:10px;padding-top:10px'><br /> " + UserName + "<br />" + UserContact + "<br />" + UserEmail + "<br />" + vatregno + "</div></td>" +
                                                "                        <td align='center'>" + pic + "</td>" +
                                                "                    </tr>" +
                                                    "                    <tr>" +
                                                    "                        <td align='left'><br /><h4>INVOICE TO :</h4><b>Plumbing Industry Registration Board (PIRB)</b><br />PO Box 411<br /> Wierdapark<br /> Centurion <br /> 0149 <br /> 0861 747 275 <br /> info@pirb.co.za <br /> www.pirb.co.za <br /><br /> VAT No: 4230255327</td>" +
                                                    "                        <td align='left'><br /><h4>TAX INVOICE: " + invID + "</h4><br />Date: " + InvoiceDate.ToString("dd/MM/yyyy") + "</td>" +
                                                    "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <td align='left' colspan=\"2\" valign='top'>" +
                                                "                            <table border='0' cellpadding='5px' cellspacing='0' width='100%'>" +
                                                "                               <tr>" +
                                                "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle'>Activitiy</td>" +
                                                "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle'>Description</td>" +
                                                "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle'>Qty</td>" +
                                                "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle'>Rate</td>" +
                                                "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle'>Amount</td>" +
                                                "                                </tr>" +
                                                "                               <tr>" +
                                                "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle'>Audit</td>" +
                                                "                                   <td style=\"border: 1px solid #E5E5E5;\" valign='middle'>Audit undertaken of COC" + DLdb.Decrypt(Request.QueryString["cocid"]) + "</td>" +
                                                "                                   <td style=\"border: 1px solid #E5E5E5;\" valign='middle'>1</td>" +
                                                "                                   <td style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + inspectorRate + "</td>" +
                                                "                                   <td style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + inspectorRate + "</td>" +
                                                "                                </tr>" +
                                                    "                               <tr>" +
                                                    "                                   <td style=\"border: 1px solid #E5E5E5;\" colspan=\"2\" valign='middle' align='right'><b>VAT @15%<b></td>" +
                                                    "                                   <td  style=\"border: 1px solid #E5E5E5;\" colspan=\"3\" align='right' valign='middle'><b>R" + vatElec + "</b></td>" +
                                                    "                                </tr>" +
                                                "                               <tr>" +
                                                "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle' colspan=\"2\" align='right'><b>Total Amount Excl VAT<b></td>" +
                                                "                                   <td  style=\"border: 1px solid #E5E5E5;\" align='right' colspan=\"3\" valign='middle'><b>R" + inspectorRate + "</b></td>" +
                                                "                                </tr>" +
                                                "                               <tr>" +
                                                "                                   <td  style=\"border: 1px solid #E5E5E5;\" valign='middle' colspan=\"2\" align='right'><b>Total Amount Incl VAT<b></td>" +
                                                "                                   <td  style=\"border: 1px solid #E5E5E5;\" align='right' colspan=\"3\" valign='middle'><b>R" + totalWithvat + "</b></td>" +
                                                "                                </tr>" +
                                                "                            </table>" +
                                                "                        </td>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                 "                        <td align='left' valign='top'><div style='width:100%;padding:10px;padding-top:10px'><br /> " + Bank + "</div></td>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <td colspan='2'><br /><br /><table border='0' cellpadding='3px' cellspacing='0' width='100%'><tr><td align='left'><img src='https://197.242.82.242/inspectit/assets/img/logo.png'/></td><td valign='middle' align='right'><b>InspectIT Team</b></td></tr></table></td>" +
                                                "                    </tr>" +
                                                "                </table>" +
                                                "            </td>" +
                                                "        </tr>" +
                                                "    </table>" +
                                                "</body>");

                filename = "invoice_" + invID + "_" + srtMM + "-" + srtYY + ".pdf";
                var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(htmlContent);
                string path = Server.MapPath("~/Inspectorinvoices/") + filename;
                File.WriteAllBytes(path, pdfBytes);

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCInspectors set Report = @Report,InvoiceNumber=@InvoiceNumber,DateInvoiceSubmitted=getdate() where cocstatementid=@cocstatementid";
                DLdb.SQLST.Parameters.AddWithValue("Report", filename);
                DLdb.SQLST.Parameters.AddWithValue("InvoiceNumber", invoiceNumber.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("cocstatementid", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.DB_Close();
                Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("COC Audit Statement Submitted"));
            }
        }
    }
}