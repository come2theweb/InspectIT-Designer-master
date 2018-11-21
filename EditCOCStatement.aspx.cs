using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoogleMaps.LocationServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

namespace InspectIT
{
    public partial class EditCOCStatement : System.Web.UI.Page
    {
        public string addressExists = "false";
        public string latitudeFromDB = "";
        public string longitudeFromDB = "";
        public Boolean isPaper = false;
        public string radioSelected = "";
        protected void Page_init(object sender, EventArgs e)
        {
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            Global DLdb = new Global();
            DLdb.DB_Connect();
            string CustomerIDs = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {

                    CustomerIDs = theSqlDataReader["CustomerID"].ToString();
                    subbtnhideorshow.Visible = true;
                    btnSave.Enabled = true;
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // LOAD CUSTOMER
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Customers where CustomerID = @CustomerID";
            DLdb.SQLST.Parameters.AddWithValue("CustomerID", CustomerIDs);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["AddressStreet"].ToString() != "" || theSqlDataReader["AddressStreet"] != DBNull.Value)
                    {
                        addressExists = "true";

                    }

                    latitudeFromDB = theSqlDataReader["lat"].ToString();
                    longitudeFromDB = theSqlDataReader["lng"].ToString();

                    populateLatitudeValue.Text = theSqlDataReader["lat"].ToString();
                    populateLongitudeValue.Text = theSqlDataReader["lng"].ToString();
                    CustomerName.Text = theSqlDataReader["CustomerName"].ToString();
                    CustomerSurname.Text = theSqlDataReader["CustomerSurname"].ToString();
                    CustomerCellNo.Text = theSqlDataReader["CustomerCellNo"].ToString();
                    CustomerCellNoAlt.Text = theSqlDataReader["CustomerCellNoAlt"].ToString();
                    CustomerEmail.Text = theSqlDataReader["CustomerEmail"].ToString();
                    AddressStreet.Text = theSqlDataReader["AddressStreet"].ToString();
                    //selSuburb.SelectedValue = theSqlDataReader["AddressSuburb"].ToString();
                    selSuburb.Text = theSqlDataReader["AddressSuburb"].ToString();
                    AddressCity.Text = theSqlDataReader["AddressCity"].ToString();
                    AddressAreaCode.Text = theSqlDataReader["AddressAreaCode"].ToString();

                    string mProvince = theSqlDataReader["Province"].ToString();
                    Province.SelectedIndex = Province.Items.IndexOf(Province.Items.FindByValue(mProvince));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
        }

        private void DisableControls(System.Web.UI.Control control)
        {
            foreach (System.Web.UI.Control c in control.Controls)
            {
                // Get the Enabled property by reflection.
                Type type = c.GetType();
                PropertyInfo prop = type.GetProperty("Enabled");

                // Set it to False to disable the control.
                if (prop != null)
                {
                    prop.SetValue(c, false, null);
                }

                // Recurse into child controls.
                if (c.Controls.Count > 0)
                {
                    this.DisableControls(c);
                }
            }
        }

        protected void DisableControlsTwo(Control parent, bool State)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is DropDownList)
                {
                    ((DropDownList)(c)).Enabled = State;
                }

                if (c is TextBox)
                {
                    ((TextBox)(c)).Enabled = State;
                }

                if (c is CheckBoxList)
                {
                    ((CheckBoxList)(c)).Enabled = State;
                }

                if (c is RadioButtonList)
                {
                    ((RadioButtonList)(c)).Enabled = State;
                }

                DisableControlsTwo(c, State);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            //// CHECK SESSION
            if (Session["IIT_UID"] == null)
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
                COCNumber.InnerHtml = "<p><b>COC Number: " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + "</b></p>";
                string btnNoticeShow = "";
                string CustomerID = "";
                string name = "";
                string pirbid = "";
                string surname = "";
                BlankFormWarning.Visible = false;
                Boolean isLogged = false;
                //REQUIRED: DISABLE SAVE AND SUBMIT
                //btnSave.Enabled = false;
                //btnSubmit.Enabled = false;
                submitAudPin.Text = "false";
                //REQUIRED: HIDE REFIX UNTILL isRefix = '1'
                DisplayRefixNotice.Visible = false;
                inspectorDetails.Visible = false;
                paperBase.Visible = false;

                if (Session["IIT_isSuspended"].ToString() == "True")
                {
                    errormsg.InnerHtml = "You can't submit a COC because you're suspended";
                    errormsg.Visible = true;
                    subbtnhideorshow.Visible = false;
                    btnSave.Enabled = false;
                    btnSubmitCompleteRifxes.Visible = false;
                }

                btnSubmitCompleteRifxes.Visible = false;

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

                        //latestCommentPosted.InnerHtml = theSqlDataReader["Comments"].ToString();

                        DateTime dateCreatedViewa = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                        latestCommentPosted.InnerHtml = theSqlDataReader["Comments"].ToString() + "<br/> <small>" + newComma + " - " + dateCreatedViewa.ToString("dd/MM/yyyy") + "</small>";
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
                                inspectorDatePosted.InnerHtml = dateCreated.ToString("dd/MM/yyyy");
                                name = theSqlDataReader2["fname"].ToString();
                                surname = theSqlDataReader2["lname"].ToString();

                                inspectorName.InnerHtml = name + " " + surname;

                                inspectorComments.InnerHtml += "<p>" + theSqlDataReader["Comments"].ToString() + "</p>";

                                // inspectorDatePosted.InnerHtml = theSqlDataReader2["CreateDate"].ToString("dd/MM/yyyy");
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

                AddressCity.Items.Add(new ListItem("", ""));
                selSuburb.Items.Add(new ListItem("", ""));
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Area order by Name";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        AddressCity.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["Name"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from areasuburbs order by Name";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        selSuburb.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["Name"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
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

                        }
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

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
                        //string photo = "<img src=\"assets/img/profiles/avatar_small2x.jpg\" />";

                        InspectorFullName.Text = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                        InspectorContact.Text = theSqlDataReader["phonemobile"].ToString();
                        InspectorEmail.Text = theSqlDataReader["email"].ToString();
                        InspectorRegNo.Text = theSqlDataReader["regno"].ToString();
                        InspectorBusContact.Text = theSqlDataReader["phonework"].ToString();
                        if (theSqlDataReader["photofile"] != DBNull.Value && theSqlDataReader["photofile"].ToString() != "")
                        {
                            //photo = "<img src=\"AuditorImgs/" + theSqlDataReader["photofile"].ToString() + "\" />";
                            Image1.ImageUrl = "AuditorImgs/" + theSqlDataReader["PhotoFile"].ToString();
                        }
                        if (theSqlDataReader["Invoice"].ToString() != "" || theSqlDataReader["Invoice"] != DBNull.Value)
                        {

                            showreportbtn.InnerHtml = "<a href=\"Inspectorinvoices/" + theSqlDataReader["Invoice"].ToString() + "\" target=\"_blank\"><button type=\"button\" ID=\"btnViewPDF\" class=\"btn btn-default\">View Report</button></a>";
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
                        DescriptionofWork.Text = theSqlDataReader["DescriptionofWork"].ToString();

                        string misBank = theSqlDataReader["isBank"].ToString();
                        isBank.SelectedIndex = isBank.Items.IndexOf(isBank.Items.FindByValue(misBank));

                        //REQUIRED: IF TRUE ADD BANK FORM BY BANK NAME
                        if (misBank == "True")
                        {

                        }

                    }
                }
                else
                {
                    BlankFormWarning.Visible = true;
                    //btnSave.Enabled = false;
                    //btnSubmit.Enabled = false;
                    submitAudPin.Text = "false";
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string isPlumberLogged = "";
                string isPlumberSubmitted = "";
                string uids = "";
                // GET CUSTOMERID
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
                        uids = theSqlDataReader["UserID"].ToString();
                        nonCompTxtbx.Text = theSqlDataReader["NonComplianceDetails"].ToString();
                        CustomerID = theSqlDataReader["CustomerID"].ToString();
                        if (theSqlDataReader["PaperBasedCOC"].ToString() != "" && theSqlDataReader["PaperBasedCOC"] != DBNull.Value)
                        {
                            PaperCOCDisp.InnerHtml = "<span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImagePDF('" + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"pdf/" + theSqlDataReader["PaperBasedCOC"].ToString() + "\"><img src=\"pdf/" + theSqlDataReader["PaperBasedCOC"].ToString() + "\" style=\"height:200px;\"/></a>";
                        }
                        else
                        {
                            PaperCOCDisp.InnerHtml = "";

                        }
                        // SHOW OR HIDE REFIX BLOCK
                        string isRefix = theSqlDataReader["isRefix"].ToString();
                        if (isRefix == "True")
                        {
                            //DisplayRefixNotice.Visible = true;
                            inspectorDetails.Visible = true;
                        }

                        isPlumberSubmitted = theSqlDataReader["isPlumberSubmitted"].ToString();
                        if (isPlumberSubmitted == "True")
                        {
                            //DisplayRefixNotice.Visible = true;
                            subbtnhideorshow.Visible = false;
                            btnSave.Visible = false;
                            btnSubmitCompleteRifxes.Visible = false;
                        }

                        isPlumberLogged = theSqlDataReader["isLogged"].ToString();
                        if (isPlumberLogged == "True")
                        {
                            subbtnhideorshow.Visible = false;
                            btnSave.Visible = false;

                            DisableControlsTwo(Page, false);

                            TextBox1.Enabled = true;
                        }

                        if (theSqlDataReader["Type"].ToString() == "Paper")
                        {
                            paperBase.Visible = true;
                            isPaper = true;
                        }

                        radioSelected = theSqlDataReader["AorB"].ToString();
                        if (theSqlDataReader["AorB"].ToString() == "A")
                        {
                            RadioButtonList1.SelectedValue = "A";
                        }
                        else if (theSqlDataReader["AorB"].ToString() == "B")
                        {
                            RadioButtonList1.SelectedValue = "B";
                        }
                        // CHECK IF LOGGED
                        if (theSqlDataReader["DateLogged"] != DBNull.Value && theSqlDataReader["status"].ToString() == "Logged")
                        {
                            //btnSubmit.Enabled = false;
                            isLogged = true;
                        }
                        if (theSqlDataReader["DateRefix"] != DBNull.Value)
                        {
                            refixdetails.InnerHtml = Convert.ToDateTime(theSqlDataReader["DateRefix"]).ToString("dd/MM/yyyy");
                        }

                        //if (theSqlDataReader["COCFilename"] != DBNull.Value)
                        //{
                        //    showreportbtn.InnerHtml = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\"><button type=\"button\" ID=\"btnViewPDF\" class=\"btn btn-default\">View COC</button></a>";
                        //}
                        //else
                        //{
                        //    if (theSqlDataReader["OLDCOCID"] != DBNull.Value)
                        //    {
                        //        showreportbtn.InnerHtml = "<a href=\"zCreateOlderPDF.aspx?cocid=" + theSqlDataReader["COCStatementID"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"Create COC\">Create Report</div></a>";
                        //    }
                        //}

                        if (theSqlDataReader["Status"].ToString() == "Logged")
                        {
                            btn_updateDetails.Visible = false;
                            btnSave.Visible = false;
                            subbtnhideorshow.Visible = false;
                        }
                        else
                        {

                        }

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
                        selSuburb.SelectedValue = theSqlDataReader["AddressSuburb"].ToString();
                        AddressCity.SelectedValue = theSqlDataReader["AddressCity"].ToString();
                        AddressAreaCode.Text = theSqlDataReader["AddressAreaCode"].ToString();

                        CustomerMap.InnerHtml = "<a href=\"https://www.google.co.za/maps/place/" + CustomerFullAddress.Text + "\" title=\"Click to view on map\" target=\"_blank\"><div class=\"btn btn-info\"><i class=\"fa fa-map-marker\"></i></div></a>";
                        CustomerFullAddress.Text = theSqlDataReader["AddressStreet"].ToString() + "," + theSqlDataReader["AddressSuburb"].ToString() + "," + theSqlDataReader["AddressCity"].ToString() + "," + theSqlDataReader["AddressAreaCode"].ToString();

                        if (theSqlDataReader["AddressStreet"].ToString() == "")
                        {

                        }
                        else
                        {

                        }

                        string mProvince = theSqlDataReader["Province"].ToString();
                        Province.SelectedIndex = Province.Items.IndexOf(Province.Items.FindByValue(mProvince));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID = @UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", uids);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        pirbid = theSqlDataReader["pirbid"].ToString();
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string designation = "";
                Boolean showLP = false;
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM PlumberDesignations where isActive = '1' and PlumberID=@PlumberID";
                DLdb.SQLST.Parameters.AddWithValue("PlumberID", pirbid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["Designation"].ToString() == "Licensed Plumber")
                        {
                            showLP = true;
                        }
                        designation = theSqlDataReader["Designation"].ToString();
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                Boolean showSP = false;
                Boolean showHPP = false;
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM PlumberSpecialisations where isActive = '1' and PlumberID=@PlumberID";
                DLdb.SQLST.Parameters.AddWithValue("PlumberID", pirbid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["Specialisation"].ToString() == "Solar")
                        {
                            showSP = true;
                        }
                        if (theSqlDataReader["Specialisation"].ToString() == "Heat Pump")
                        {
                            showHPP = true;
                        }
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                // LOAD TYPE OF INSTALLATIONS
                //pirbid
                TypeOfInstallation.Items.Clear();

                DLdb.RS.Open();
                if (showLP == true && showSP == false && showHPP == false)
                {
                    DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where (isActive = '1' and InstallationTypeID != '8') and (isActive = '1' and InstallationTypeID != '4')";

                }
                else if (showLP == true && showSP == true && showHPP == false)
                {
                    DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive = '1' and InstallationTypeID != '8'";
                }
                else if (showLP == true && showSP == true && showHPP == true)
                {
                    DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive = '1'";
                }
                else if (showLP == true && showSP == false && showHPP == true)
                {
                    DLdb.SQLST.CommandText = "SELECT * FROM InstallationTypes where isActive = '1' and InstallationTypeID != '4'";
                }
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

                // LOAD TYPE OF INSTALLATIONS
                string LinksHTML = "<label class=\"btn btn-tag btn-danger btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=NON&tid=7&cocid=" + Request.QueryString["cocid"] + "&did=1'\">Non Compliance Notice</label><br />";
                decimal cnt = 0;
                decimal totcnt = 0;
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM COCInstallations where COCStatementID = @COCStatementID and isActive = '1'";
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

                                //if (theSqlDataReader["isRefix"].ToString() == "True")
                                //{

                                //    // GET ALL FIELD COMMENTS
                                //    DLdb.RS4.Open();
                                //    DLdb.SQLST4.CommandText = "exec getRefixFieldNoticesByCOC @COCStatementID, @TypeID";
                                //    DLdb.SQLST4.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                                //    DLdb.SQLST4.Parameters.AddWithValue("TypeID", theSqlDataReader["TypeID"].ToString());
                                //    DLdb.SQLST4.CommandType = CommandType.Text;
                                //    DLdb.SQLST4.Connection = DLdb.RS4;
                                //    SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                                //    if (theSqlDataReader4.HasRows)
                                //    {
                                //        while (theSqlDataReader4.Read())
                                //        {
                                //            string alertcol = "info";
                                //            if (theSqlDataReader4["NoticeType"].ToString() == "Failure")
                                //            {
                                //                alertcol = "danger";
                                //            }
                                //            else if (theSqlDataReader4["NoticeType"].ToString() == "Cautionary")
                                //            {
                                //                alertcol = "warning";
                                //            }
                                //            else if (theSqlDataReader4["NoticeType"].ToString() == "Complement")
                                //            {
                                //                alertcol = "success";
                                //            }

                                //            if (theSqlDataReader4["picture"] != DBNull.Value)
                                //            {
                                //                btnNoticeShow += "<div class=\"row\"><div class=\"col-md-12\"><div class=\"alert alert-" + alertcol + "\"><h5><b>" + theSqlDataReader4["NoticeType"].ToString() + "</b></h5>" + theSqlDataReader4["Comments"].ToString() + "<br /><br /><img src=\"noticeimages/" + theSqlDataReader4["picture"].ToString() + "\" class=\"img-responsive\" /><br /><br />" + theSqlDataReader4["Createdate"].ToString() + "</div></div></div>";
                                //            }
                                //            else
                                //            {
                                //                btnNoticeShow += "<div class=\"row\"><div class=\"col-md-12\"><div class=\"alert alert-" + alertcol + "\"><h5><b>" + theSqlDataReader4["NoticeType"].ToString() + "</b></h5>" + theSqlDataReader4["Comments"].ToString() + "<br /><br />" + theSqlDataReader4["Createdate"].ToString() + "</div></div></div>";
                                //            }

                                //        }
                                //    }

                                //    if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                                //    DLdb.SQLST4.Parameters.RemoveAt(0);
                                //    DLdb.SQLST4.Parameters.RemoveAt(0);
                                //    DLdb.RS4.Close();

                                //    completeForms.InnerHtml += "<div class=\"row\"><div class=\"col-md-12\"><label class=\"btn btn-tag btn-danger btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["cocid"] + "&did=" + theSqlDataReader["DataID"].ToString() + "&refix=1'\">View " + li.Text.ToString() + " Form</label></div></div>";
                                //    completeForms.InnerHtml += "<h5>Auditor Comments</h5>" + btnNoticeShow;
                                //}

                                if (theSqlDataReader["DataID"] == DBNull.Value)
                                {
                                    LinksHTML += "<label class=\"btn btn-tag btn-tag-light btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["cocid"] + "&did=" + theSqlDataReader["DataID"].ToString() + "'\">" + li.Text.ToString() + "</label><br />";
                                }
                                else
                                {
                                    cnt++;
                                    LinksHTML += "<label class=\"btn btn-tag btn-success btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["cocid"] + "'\">" + li.Text.ToString() + "</label><br />";
                                }

                            }
                        }
                        totcnt++;
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string noMoreFixes = "False";
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
                        string StatusCol = "";
                        string btnFix = "";
                        string uploadBtn = "";

                        if (theSqlDataReader["status"].ToString() == "Failure" && theSqlDataReader["isFixed"].ToString() == "False")
                        {
                            StatusCol = "danger";
                            uploadBtn = "<div class=\"col-md-12 text-left\"><b>Upload your refix images:</b><br /><label class=\"btn btn-success\" onclick=\"uploadImg('" + theSqlDataReader["reviewid"].ToString() + "','" + DLdb.Decrypt(Request.QueryString["cocid"]) + "')\">Upload</label></div>";
                            btnFix = "<input id=\"" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "\" name=\"" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "\" value=\"" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "\" onclick=\"markasFixed('" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "')\" type=\"checkbox\"/> Mark As Fixed";
                            //btnFix = "<label class=\"btn btn-primary\" onclick=\"markasFixed('" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "')\">This job has been Refixed</label>";
                        }
                        else if (theSqlDataReader["status"].ToString() == "Cautionary")
                        {
                            StatusCol = "warning";
                            //btnFix = "<div class=\"btn btn-primary\" onclick=\"document.location.href='DismissReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "'\">Dismiss</div>";
                        }
                        else if (theSqlDataReader["status"].ToString() == "Compliment")
                        {
                            StatusCol = "success";
                            //btnFix = "<div class=\"btn btn-primary\" onclick=\"document.location.href='DismissReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "'\">Dismiss</div>";
                        }
                        else if (theSqlDataReader["status"].ToString() == "Failure" && theSqlDataReader["isFixed"].ToString() == "True" && theSqlDataReader["isClosed"].ToString() == "False")
                        {
                            StatusCol = "danger";
                            //btnFix = "<label class=\"label label-success\">You have marked this as fixed</label>";
                            btnFix = "<input id=\"" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "\" value=\"" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "\" name=\"" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "\" checked type=\"checkbox\" /> Mark As Fixed";

                        }
                        else if (theSqlDataReader["status"].ToString() == "Failure" && theSqlDataReader["isFixed"].ToString() == "True" && theSqlDataReader["isClosed"].ToString() == "True")
                        {
                            StatusCol = "danger";
                            btnFix = "<label class=\"label label-success\">Refix Complete</label>";
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

                        CurrentReview.InnerHtml += "<div class=\"row alert-" + StatusCol + "\" style=\"padding: 5px; \">" +
                                                   "       <div class=\"col-md-6 text-left\">" +
                                                   "         <b>Instalation Type:</b> " + InstallationType + "<br />" +
                                                   "         <b>Audit Status:</b> " + theSqlDataReader["status"].ToString() + "" +
                                                   "     </div>" +
                                                   "       <div class=\"col-md-6 text-right\">" +
                                                                btnFix +
                                                   "     </div>" +
                                                   "     <div class=\"col-md-12 text-left\">" +
                                                   //"        <b>Reference:</b> " + theSqlDataReader["name"].ToString() + "" +
                                                   "     </div>" +
                                                   "     <div class=\"col-md-12 text-left\"><b>Comments:</b> " + theSqlDataReader["comment"].ToString() + "</div>" +
                                                   "     <div class=\"col-md-12 text-left\"><b>Media:</b><br />" + Media + "</div>" +
                                                   "     <div class=\"col-md-12 text-left\"><b>Reference:</b> " + theSqlDataReader["Reference"].ToString() + "</div>" +
                                                   "     <div class=\"col-md-12 text-left\"><b>Media:</b><br />" + referenceMedia + "</div>" +
                                                   uploadBtn +
                                                   // "     <div class=\"col-md-12 text-right\"><div class=\"btn btn-primary\" onclick=\"document.location.href='EditReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "'\">Edit</div></div>" +
                                                   " </div><hr />";

                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                //dispCompltImgs
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
                DLdb.SQLST.CommandText = "SELECT * FROM COCReviews where COCStatementID = @COCStatementID and isActive = '1' and isFixed='0'";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {

                        noMoreFixes = "False";
                    }
                }
                else
                {
                    noMoreFixes = "True";
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                if (noMoreFixes == "True")
                {
                    if (isPlumberLogged == "True" && isPlumberSubmitted == "False")
                    {
                        btnSubmitCompleteRifxes.Visible = true;
                    }
                }

                //REQUIRED: SUCCESS BUTTON AND UPDATING PROGRESS BAR, IF ALL FORMS COMPLETE ACTIVATE SAVE AND SUBMIT
                if (totcnt == cnt && cnt != 0)
                {
                    if (isLogged == false)
                    {
                        //btnSave.Enabled = true;
                        //btnSubmit.Enabled = true;
                        submitAudPin.Text = "true";
                    }
                    progressBarStatus.InnerHtml = "<div class=\"progress-bar progress-bar-primary\" data-percentage=\"100%\"></div>";

                }
                else
                {
                    if (cnt != 0)
                    {
                        decimal gPercent = (cnt / totcnt) * 100m;
                        if (isLogged == false)
                        {
                            //btnSave.Enabled = false;
                            //btnSubmit.Enabled = false;
                            submitAudPin.Text = "false";
                        }
                        progressBarStatus.InnerHtml = "<div class=\"progress-bar progress-bar-primary\" data-percentage=\"" + gPercent.ToString().Replace(",0", "") + "%\"></div>";
                    }
                    else
                    {
                        if (isLogged == false)
                        {
                            //btnSave.Enabled = false;
                            //btnSubmit.Enabled = false;
                            submitAudPin.Text = "false";
                        }
                        progressBarStatus.InnerHtml = "<div class=\"progress-bar progress-bar-primary\" data-percentage=\"0%\"></div>";
                    }

                }

                // ADD THE CONTROLS
                FormLinks.InnerHtml = LinksHTML;

                // GET GENERAL COMMENTS
                DLdb.RS3.Open();
                DLdb.SQLST3.CommandText = "select * from COCInspectors where COCStatementID = @COCStatementID and isComplete = '1'";
                DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST3.CommandType = CommandType.Text;
                DLdb.SQLST3.Connection = DLdb.RS3;
                SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                if (theSqlDataReader3.HasRows)
                {
                    while (theSqlDataReader3.Read())
                    {
                        InspectionDate.Text = theSqlDataReader3["InspectionDate"].ToString();
                        string lQuality = theSqlDataReader3["Quality"].ToString();
                        Quality.SelectedIndex = Quality.Items.IndexOf(Quality.Items.FindByValue(lQuality));
                        if (theSqlDataReader3["Picture"] != DBNull.Value)
                        {
                            AuditPicture.InnerHtml = "<img src=\"noticeimages/" + theSqlDataReader3["Picture"].ToString() + "\" class=\"img-responsive\" />";
                        }

                    }
                }

                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                DLdb.SQLST3.Parameters.RemoveAt(0);
                DLdb.RS3.Close();

                DLdb.RS3.Open();
                DLdb.SQLST3.CommandText = "select * from COCInspectors where COCStatementID = @COCStatementID and isactive = '1'";
                DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST3.CommandType = CommandType.Text;
                DLdb.SQLST3.Connection = DLdb.RS3;
                theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                if (theSqlDataReader3.HasRows)
                {
                    while (theSqlDataReader3.Read())
                    {
                        if (theSqlDataReader3["InspectionDate"] == DBNull.Value)
                        {
                            hideAudit.Visible = false;
                            hideAudita.Visible = false;
                        }
                        else
                        {
                            InspectionDate.Text = theSqlDataReader3["InspectionDate"].ToString();
                            string lQuality = theSqlDataReader3["Quality"].ToString();
                            Quality.SelectedIndex = Quality.Items.IndexOf(Quality.Items.FindByValue(lQuality));
                            if (theSqlDataReader3["Picture"] != DBNull.Value)
                            {
                                AuditPicture.InnerHtml = "<img src=\"noticeimages/" + theSqlDataReader3["Picture"].ToString() + "\" class=\"img-responsive\" />";
                            }
                        }

                    }
                }
                else
                {
                    hideAudit.Visible = false;
                    hideAudita.Visible = false;
                }

                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                DLdb.SQLST3.Parameters.RemoveAt(0);
                DLdb.RS3.Close();

                completeForms.InnerHtml += "<h5>Auditor Comments</h5>" + btnNoticeShow;

                DLdb.DB_Close();
            }
        }

        protected void btn_updateDetails_Click(object sender, EventArgs e)
        {

            Global DLdb = new Global();
            DLdb.DB_Connect();

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

            string WorkCompleteby = "";

            // ADD COC STATEMENT DETAILS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into COCStatementDetails (COCStatementID,CompletedDate,COCType,InsuranceCompany,PolicyHolder,PolicyNumber,isBank,PeriodOfInsuranceFrom,PeriodOfInsuranceTo,DescriptionofWork,WorkCompleteby) values (@COCStatementID,@CompletedDate,@COCType,@InsuranceCompany,@PolicyHolder,@PolicyNumber,@isBank,@PeriodOfInsuranceFrom,@PeriodOfInsuranceTo,@DescriptionofWork,@WorkCompleteby)";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.Parameters.AddWithValue("CompletedDate", CompletedDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("COCType", COCType.ToString());
            DLdb.SQLST.Parameters.AddWithValue("InsuranceCompany", InsuranceCompany.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PolicyHolder", PolicyHolder.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PolicyNumber", PolicyNumber.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("isBank", isBank.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PeriodOfInsuranceFrom", PeriodOfInsuranceFrom.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("PeriodOfInsuranceTo", PeriodOfInsuranceTo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("DescriptionofWork", DescriptionofWork.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("WorkCompleteby", WorkCompleteby);
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
            DLdb.RS.Close();

            string addressToLongLat = AddressStreet.Text.ToString() + ", " + selSuburb.Text + ", " + AddressCity.Text.ToString() + ", " + Province.Text.ToString();

            //string lat, lng;
            //try {
            //    var address = addressToLongLat;
            //    var locationService = new GoogleLocationService();
            //    var point = locationService.GetLatLongFromAddress(address);
            //    var latitude = point.Latitude;
            //    var longitude = point.Longitude;
            //    lat = latitude.ToString();
            //    lng = longitude.ToString();
            //} catch (Exception err)
            //{
            //    lat = "err";
            //    lng = "err";
            //}


            //// ADD CUSTOMER IF NOT CUTOMER EXISTS
            //DLdb.RS2.Open();
            //DLdb.SQLST2.CommandText = "select * from Customers where CustomerEmail = @CustomerEmail";
            //DLdb.SQLST2.Parameters.AddWithValue("CustomerEmail", CustomerEmail.Text.ToString());
            //DLdb.SQLST2.CommandType = CommandType.Text;
            //DLdb.SQLST2.Connection = DLdb.RS2;
            //SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            //if (!theSqlDataReader2.HasRows)
            //{

                //REQUIRED: Customer NEEDS PASSWORD....
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into Customers (CustomerName,CustomerSurname,CustomerCellNo,CustomerCellNoAlt,CustomerEmail,AddressStreet,AddressSuburb,AddressCity,Province,AddressAreaCode,CustomerPassword,lat,lng) values (@CustomerName,@CustomerSurname,@CustomerCellNo,@CustomerCellNoAlt,@CustomerEmail,@AddressStreet,@AddressSuburb,@AddressCity,@Province,@AddressAreaCode,@CustomerPassword,@lat,@lng); Select Scope_Identity() as CustomerID";
                DLdb.SQLST.Parameters.AddWithValue("CustomerName", CustomerName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerSurname", CustomerSurname.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerCellNo", CustomerCellNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerCellNoAlt", CustomerCellNoAlt.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerEmail", CustomerEmail.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressStreet", AddressStreet.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressSuburb", selSuburb.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressCity", AddressCity.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Province", Province.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressAreaCode", AddressAreaCode.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerPassword", DLdb.CreatePassword(8));
                DLdb.SQLST.Parameters.AddWithValue("lat", latitudeHidden.ToString());
                DLdb.SQLST.Parameters.AddWithValue("lng", longitudeHidden.ToString());

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
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                // UPDATE THE CUSTOMER ID
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCStatements set CustomerID = @CustomerID, Status = 'Non-logged Allocated', NonComplianceDetails = @NonComplianceDetails,COCNumber=COCStatementID where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.Parameters.AddWithValue("CustomerID", CustomerID.ToString());
                DLdb.SQLST.Parameters.AddWithValue("NonComplianceDetails", nonCompTxtbx.Text.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            //}
            //else
            //{

            //    theSqlDataReader2.Read();
            //    DLdb.RS.Open();
            //    DLdb.SQLST.CommandText = "update Customers set CustomerName=@CustomerName,CustomerSurname=@CustomerSurname,CustomerCellNo=@CustomerCellNo,CustomerCellNoAlt=@CustomerCellNoAlt,CustomerEmail=@CustomerEmail,AddressStreet=@AddressStreet,AddressSuburb=@AddressSuburb,AddressCity=@AddressCity,Province=@Province,AddressAreaCode=@AddressAreaCode,lat=@lat,lng=@lng where CustomerID=@CustomerID";
            //    DLdb.SQLST.Parameters.AddWithValue("CustomerID", theSqlDataReader2["CustomerID"].ToString());
            //    DLdb.SQLST.Parameters.AddWithValue("CustomerName", CustomerName.Text.ToString());
            //    DLdb.SQLST.Parameters.AddWithValue("CustomerSurname", CustomerSurname.Text.ToString());
            //    DLdb.SQLST.Parameters.AddWithValue("CustomerCellNo", CustomerCellNo.Text.ToString());
            //    DLdb.SQLST.Parameters.AddWithValue("CustomerCellNoAlt", CustomerCellNoAlt.Text.ToString());
            //    DLdb.SQLST.Parameters.AddWithValue("CustomerEmail", CustomerEmail.Text.ToString());
            //    DLdb.SQLST.Parameters.AddWithValue("AddressStreet", AddressStreet.Text.ToString());
            //    DLdb.SQLST.Parameters.AddWithValue("AddressSuburb", selSuburb.Text.ToString());
            //    DLdb.SQLST.Parameters.AddWithValue("AddressCity", AddressCity.Text.ToString());
            //    DLdb.SQLST.Parameters.AddWithValue("Province", Province.SelectedValue.ToString());
            //    DLdb.SQLST.Parameters.AddWithValue("AddressAreaCode", AddressAreaCode.Text.ToString());
            //    DLdb.SQLST.Parameters.AddWithValue("lat", latitudeHidden.ToString());
            //    DLdb.SQLST.Parameters.AddWithValue("lng", longitudeHidden.ToString());

            //    DLdb.SQLST.CommandType = CommandType.Text;
            //    DLdb.SQLST.Connection = DLdb.RS;
            //    theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.RS.Close();

            //    // UPDATE THE CUSTOMER ID
            //    DLdb.RS.Open();
            //    DLdb.SQLST.CommandText = "update COCStatements set CustomerID = @CustomerID, Status = 'Allocated', NonComplianceDetails = @NonComplianceDetails,COCNumber=COCStatementID where COCStatementID = @COCStatementID";
            //    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            //    DLdb.SQLST.Parameters.AddWithValue("CustomerID", theSqlDataReader2["CustomerID"].ToString());
            //    DLdb.SQLST.Parameters.AddWithValue("NonComplianceDetails", nonCompTxtbx.Text.ToString());
            //    DLdb.SQLST.CommandType = CommandType.Text;
            //    DLdb.SQLST.Connection = DLdb.RS;
            //    theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.SQLST.Parameters.RemoveAt(0);
            //    DLdb.RS.Close();

            //}

            //if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            //DLdb.SQLST2.Parameters.RemoveAt(0);
            //DLdb.RS2.Close();

            // INSPECTION TYPES
            foreach (ListItem item in TypeOfInstallation.Items)
            {
                if (item.Selected)
                {
                    // CHECK IF EXISTS AND UPDATE OR ADD NEW
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from COCInstallations where TypeID = @TypeID and COCStatementID = @COCStatementID";
                    DLdb.SQLST.Parameters.AddWithValue("TypeID", item.Value.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "update COCInstallations set isActive = '1' where TypeID = @TypeID and COCStatementID = @COCStatementID";
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
                else
                {
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "update COCInstallations set isActive = '0' where TypeID = @TypeID and COCStatementID = @COCStatementID";
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
            }


            DLdb.DB_Close();

            Response.Redirect("EditCOCStatement.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("COC Statement has been updated"));

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

            // UPDATE COC REFIX
            if (RefixCompleted.Checked == true)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCStatements set isRefix = '0' where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }

            DLdb.DB_Close();

            Response.Redirect("EditCOCStatement.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("Refix updated successfully"));

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

            Response.Redirect("EditCOCStatement.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("Discussion successfully"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string AuditorID = "";
            string AuditorIDUSerID = "";
            string NumberTo = "";
            string EmailAddress = "";
            string FullName = "";
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

            string WorkCompleteby = "";

            // ADD COC STATEMENT DETAILS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatementDetails where COCStatementID=@COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();

            }
            else
            {
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "insert into COCStatementDetails (COCStatementID,CompletedDate,COCType,InsuranceCompany,PolicyHolder,PolicyNumber,isBank,PeriodOfInsuranceFrom,PeriodOfInsuranceTo,DescriptionofWork,WorkCompleteby) values (@COCStatementID,@CompletedDate,@COCType,@InsuranceCompany,@PolicyHolder,@PolicyNumber,@isBank,@PeriodOfInsuranceFrom,@PeriodOfInsuranceTo,@DescriptionofWork,@WorkCompleteby)";
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST2.Parameters.AddWithValue("CompletedDate", CompletedDate.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("COCType", COCType.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("InsuranceCompany", InsuranceCompany.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("PolicyHolder", PolicyHolder.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("PolicyNumber", PolicyNumber.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("isBank", isBank.SelectedValue.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("PeriodOfInsuranceFrom", PeriodOfInsuranceFrom.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("PeriodOfInsuranceTo", PeriodOfInsuranceTo.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("DescriptionofWork", DescriptionofWork.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("WorkCompleteby", WorkCompleteby);
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string addressToLongLat = AddressStreet.Text.ToString() + ", " + selSuburb.Text + ", " + AddressCity.Text.ToString() + ", " + Province.Text.ToString();

            //// ADD CUSTOMER IF NOT CUTOMER EXISTS
            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "select * from Customers where CustomerEmail = @CustomerEmail";
            //DLdb.SQLST.Parameters.AddWithValue("CustomerEmail", CustomerEmail.Text.ToString());
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //if (!theSqlDataReader.HasRows)
            //{
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into Customers (CustomerName,CustomerSurname,CustomerCellNo,CustomerCellNoAlt,CustomerEmail,AddressStreet,AddressSuburb,AddressCity,Province,AddressAreaCode,CustomerPassword,lat,lng) values (@CustomerName,@CustomerSurname,@CustomerCellNo,@CustomerCellNoAlt,@CustomerEmail,@AddressStreet,@AddressSuburb,@AddressCity,@Province,@AddressAreaCode,@CustomerPassword,@lat,@lng); Select Scope_Identity() as CustomerID";
                DLdb.SQLST.Parameters.AddWithValue("CustomerName", CustomerName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerSurname", CustomerSurname.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerCellNo", CustomerCellNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerCellNoAlt", CustomerCellNoAlt.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerEmail", CustomerEmail.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressStreet", AddressStreet.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressSuburb", selSuburb.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressCity", AddressCity.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Province", Province.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressAreaCode", AddressAreaCode.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerPassword", DLdb.CreatePassword(8));
                DLdb.SQLST.Parameters.AddWithValue("lat", latitudeHidden.ToString());
                DLdb.SQLST.Parameters.AddWithValue("lng", longitudeHidden.ToString());

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
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                // UPDATE THE CUSTOMER ID
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCStatements set CustomerID = @CustomerID, Status = 'Non-logged Allocated', NonComplianceDetails = @NonComplianceDetails,COCNumber=COCStatementID where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.Parameters.AddWithValue("CustomerID", CustomerID.ToString());
                DLdb.SQLST.Parameters.AddWithValue("NonComplianceDetails", nonCompTxtbx.Text.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            //}
            //else
            //{

            //    theSqlDataReader.Read();
            //    DLdb.RS2.Open();
            //    DLdb.SQLST2.CommandText = "update Customers set CustomerName=@CustomerName,CustomerSurname=@CustomerSurname,CustomerCellNo=@CustomerCellNo,CustomerCellNoAlt=@CustomerCellNoAlt,CustomerEmail=@CustomerEmail,AddressStreet=@AddressStreet,AddressSuburb=@AddressSuburb,AddressCity=@AddressCity,Province=@Province,AddressAreaCode=@AddressAreaCode,lat=@lat,lng=@lng where CustomerID=@CustomerID";
            //    DLdb.SQLST2.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("CustomerName", CustomerName.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("CustomerSurname", CustomerSurname.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("CustomerCellNo", CustomerCellNo.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("CustomerCellNoAlt", CustomerCellNoAlt.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("CustomerEmail", CustomerEmail.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("AddressStreet", AddressStreet.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("AddressSuburb", selSuburb.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("AddressCity", AddressCity.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("Province", Province.SelectedValue.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("AddressAreaCode", AddressAreaCode.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("lat", latitudeHidden.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("lng", longitudeHidden.ToString());

            //    DLdb.SQLST2.CommandType = CommandType.Text;
            //    DLdb.SQLST2.Connection = DLdb.RS2;
            //    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            //    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.RS2.Close();

            //    // UPDATE THE CUSTOMER ID
            //    DLdb.RS2.Open();
            //    DLdb.SQLST2.CommandText = "update COCStatements set CustomerID = @CustomerID, Status = 'Allocated', NonComplianceDetails = @NonComplianceDetails,COCNumber=COCStatementID where COCStatementID = @COCStatementID";
            //    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            //    DLdb.SQLST2.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("NonComplianceDetails", nonCompTxtbx.Text.ToString());
            //    DLdb.SQLST2.CommandType = CommandType.Text;
            //    DLdb.SQLST2.Connection = DLdb.RS2;
            //    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            //    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.RS2.Close();

            //}

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.RS.Close();

            // INSPECTION TYPES
            foreach (ListItem item in TypeOfInstallation.Items)
            {
                if (item.Selected)
                {
                    // CHECK IF EXISTS AND UPDATE OR ADD NEW
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from COCInstallations where TypeID = @TypeID and COCStatementID = @COCStatementID";
                    DLdb.SQLST.Parameters.AddWithValue("TypeID", item.Value.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "update COCInstallations set isActive = '1' where TypeID = @TypeID and COCStatementID = @COCStatementID";
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
                else
                {
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "update COCInstallations set isActive = '0' where TypeID = @TypeID and COCStatementID = @COCStatementID";
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
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                //theSqlDataReader.Read();
                //DLdb.RS2.Open();
                //DLdb.SQLST2.CommandText = "select * from Customers where CustomerID = @CustomerID";
                //DLdb.SQLST2.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
                //DLdb.SQLST2.CommandType = CommandType.Text;
                //DLdb.SQLST2.Connection = DLdb.RS2;
                //SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                //if (theSqlDataReader2.HasRows)
                //{
                //    theSqlDataReader2.Read();
                //    DLdb.RS3.Open();
                //    DLdb.SQLST3.CommandText = "select * from Area where Name = @Name";
                //    DLdb.SQLST3.Parameters.AddWithValue("Name", theSqlDataReader2["AddressSuburb"].ToString());
                //    DLdb.SQLST3.CommandType = CommandType.Text;
                //    DLdb.SQLST3.Connection = DLdb.RS3;
                //    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();
                //    if (theSqlDataReader3.HasRows)
                //    {
                //        theSqlDataReader3.Read();
                //        DLdb.RS4.Open();
                //        DLdb.SQLST4.CommandText = "select * from AuditorAreas where AreaID = @AreaID";
                //        DLdb.SQLST4.Parameters.AddWithValue("AreaID", theSqlDataReader3["ID"].ToString());
                //        DLdb.SQLST4.CommandType = CommandType.Text;
                //        DLdb.SQLST4.Connection = DLdb.RS4;
                //        SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();
                //        if (theSqlDataReader4.HasRows)
                //        {
                //            theSqlDataReader4.Read();
                //            AuditorID = theSqlDataReader4["AuditorID"].ToString();
                //            DLdb.RS5.Open();
                //            DLdb.SQLST5.CommandText = "select * from Auditor where AuditorID = @AuditorID";
                //            DLdb.SQLST5.Parameters.AddWithValue("AuditorID", theSqlDataReader4["AuditorID"].ToString());
                //            DLdb.SQLST5.CommandType = CommandType.Text;
                //            DLdb.SQLST5.Connection = DLdb.RS5;
                //            SqlDataReader theSqlDataReader5 = DLdb.SQLST5.ExecuteReader();
                //            if (theSqlDataReader5.HasRows)
                //            {
                //                theSqlDataReader5.Read();
                //                AuditorIDUSerID = theSqlDataReader5["UserID"].ToString();
                //                NumberTo = theSqlDataReader5["phoneMobile"].ToString();
                //                EmailAddress = theSqlDataReader5["email"].ToString();
                //                FullName = theSqlDataReader5["fname"].ToString() + " " + theSqlDataReader5["lname"].ToString();
                //            }
                //            if (theSqlDataReader5.IsClosed) theSqlDataReader5.Close();
                //            DLdb.SQLST5.Parameters.RemoveAt(0);
                //            DLdb.RS5.Close();
                //        }
                //        if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                //        DLdb.SQLST4.Parameters.RemoveAt(0);
                //        DLdb.RS4.Close();
                //    }
                //    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                //    DLdb.SQLST3.Parameters.RemoveAt(0);
                //    DLdb.RS3.Close();
                //}
                //if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                //DLdb.SQLST2.Parameters.RemoveAt(0);
                //DLdb.RS2.Close();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // UPDATE THE COC
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatements Set Status = 'Non-logged Allocated',AorB=@AorB,NonComplianceDetails=@NonComplianceDetails,AuditorID=@AuditorID,COCNumber=COCStatementID where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.Parameters.AddWithValue("AuditorID", AuditorID);
            DLdb.SQLST.Parameters.AddWithValue("NonComplianceDetails", nonCompTxtbx.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("AorB", RadioButtonList1.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCInspectors where UserID=@UserID and COCStatementID=@COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", AuditorIDUSerID);
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "update COCInspectors set UserID=@UserID,isActive='1' where COCStatementID=@COCStatementID";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", AuditorIDUSerID);
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
                DLdb.SQLST2.CommandText = "insert into COCInspectors (UserID, COCStatementID) values (@UserID, @COCStatementID)";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", AuditorIDUSerID);
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
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
            DLdb.SQLST.Parameters.AddWithValue("Message", "Plumber has saved the COC");
            DLdb.SQLST.Parameters.AddWithValue("Username", Session["IIT_UName"].ToString());
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

            string WorkCompletedby = "";

            // UPDATE THE COC DETAILS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatementDetails Set DescriptionofWork = @DescriptionofWork, WorkCompleteby = @WorkCompletedby where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("DescriptionofWork", DescriptionofWork.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("WorkCompletedby", WorkCompletedby);
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            if (NumberTo.ToString() != "")
            {
                DLdb.sendSMS(AuditorIDUSerID, NumberTo, "Inspect-It: You have been assigned to audit a C.O.C, please login to Inspect-It to view.");
            }

            // EMAIL
            if (EmailAddress.ToString() != "")
            {
                string eHTMLBody = "Dear " + FullName + "<br /><br />You have neen assigned to a C.O.C Statement on the Inspect-It system, please <a href='https://197.242.82.242/inspectit/'>login</a> to view.<br /><br />Regards<br />Inspect-It Administrator";
                string eSubject = "Inspect-IT New C.O.C Statement Audit";
                DLdb.sendEmail(eHTMLBody, eSubject, "mathewpayne@gmail.com", EmailAddress, "");
            }

            Response.Redirect("EditCOCStatement.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("C.O.C Saved successfully"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

            string WorkCompleteby = "";

            // ADD COC STATEMENT DETAILS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatementDetails where COCStatementID=@COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();

            }
            else
            {
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "insert into COCStatementDetails (COCStatementID,CompletedDate,COCType,InsuranceCompany,PolicyHolder,PolicyNumber,isBank,PeriodOfInsuranceFrom,PeriodOfInsuranceTo,DescriptionofWork,WorkCompleteby) values (@COCStatementID,@CompletedDate,@COCType,@InsuranceCompany,@PolicyHolder,@PolicyNumber,@isBank,@PeriodOfInsuranceFrom,@PeriodOfInsuranceTo,@DescriptionofWork,@WorkCompleteby)";
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST2.Parameters.AddWithValue("CompletedDate", CompletedDate.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("COCType", COCType.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("InsuranceCompany", InsuranceCompany.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("PolicyHolder", PolicyHolder.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("PolicyNumber", PolicyNumber.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("isBank", isBank.SelectedValue.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("PeriodOfInsuranceFrom", PeriodOfInsuranceFrom.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("PeriodOfInsuranceTo", PeriodOfInsuranceTo.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("DescriptionofWork", DescriptionofWork.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("WorkCompleteby", WorkCompleteby);
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string addressToLongLat = AddressStreet.Text.ToString() + ", " + selSuburb.Text + ", " + AddressCity.Text.ToString() + ", " + Province.Text.ToString();

            //// ADD CUSTOMER IF NOT CUTOMER EXISTS
            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "select * from Customers where CustomerEmail = @CustomerEmail";
            //DLdb.SQLST.Parameters.AddWithValue("CustomerEmail", CustomerEmail.Text.ToString());
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //if (!theSqlDataReader.HasRows)
            //{
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into Customers (CustomerName,CustomerSurname,CustomerCellNo,CustomerCellNoAlt,CustomerEmail,AddressStreet,AddressSuburb,AddressCity,Province,AddressAreaCode,CustomerPassword,lat,lng) values (@CustomerName,@CustomerSurname,@CustomerCellNo,@CustomerCellNoAlt,@CustomerEmail,@AddressStreet,@AddressSuburb,@AddressCity,@Province,@AddressAreaCode,@CustomerPassword,@lat,@lng); Select Scope_Identity() as CustomerID";
                DLdb.SQLST.Parameters.AddWithValue("CustomerName", CustomerName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerSurname", CustomerSurname.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerCellNo", CustomerCellNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerCellNoAlt", CustomerCellNoAlt.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerEmail", CustomerEmail.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressStreet", AddressStreet.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressSuburb", selSuburb.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressCity", AddressCity.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Province", Province.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressAreaCode", AddressAreaCode.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CustomerPassword", DLdb.CreatePassword(8));
                DLdb.SQLST.Parameters.AddWithValue("lat", latitudeHidden.ToString());
                DLdb.SQLST.Parameters.AddWithValue("lng", longitudeHidden.ToString());

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
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                // UPDATE THE CUSTOMER ID
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCStatements set CustomerID = @CustomerID, Status = 'Non-logged Allocated', NonComplianceDetails = @NonComplianceDetails,COCNumber=COCStatementID where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                DLdb.SQLST.Parameters.AddWithValue("CustomerID", CustomerID.ToString());
                DLdb.SQLST.Parameters.AddWithValue("NonComplianceDetails", nonCompTxtbx.Text.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            //}
            //else
            //{

            //    theSqlDataReader.Read();
            //    DLdb.RS2.Open();
            //    DLdb.SQLST2.CommandText = "update Customers set CustomerName=@CustomerName,CustomerSurname=@CustomerSurname,CustomerCellNo=@CustomerCellNo,CustomerCellNoAlt=@CustomerCellNoAlt,CustomerEmail=@CustomerEmail,AddressStreet=@AddressStreet,AddressSuburb=@AddressSuburb,AddressCity=@AddressCity,Province=@Province,AddressAreaCode=@AddressAreaCode,lat=@lat,lng=@lng where CustomerID=@CustomerID";
            //    DLdb.SQLST2.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("CustomerName", CustomerName.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("CustomerSurname", CustomerSurname.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("CustomerCellNo", CustomerCellNo.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("CustomerCellNoAlt", CustomerCellNoAlt.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("CustomerEmail", CustomerEmail.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("AddressStreet", AddressStreet.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("AddressSuburb", selSuburb.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("AddressCity", AddressCity.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("Province", Province.SelectedValue.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("AddressAreaCode", AddressAreaCode.Text.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("lat", latitudeHidden.ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("lng", longitudeHidden.ToString());

            //    DLdb.SQLST2.CommandType = CommandType.Text;
            //    DLdb.SQLST2.Connection = DLdb.RS2;
            //    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            //    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.RS2.Close();

            //    // UPDATE THE CUSTOMER ID
            //    DLdb.RS2.Open();
            //    DLdb.SQLST2.CommandText = "update COCStatements set CustomerID = @CustomerID, Status = 'Allocated', NonComplianceDetails = @NonComplianceDetails,COCNumber=COCStatementID where COCStatementID = @COCStatementID";
            //    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            //    DLdb.SQLST2.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
            //    DLdb.SQLST2.Parameters.AddWithValue("NonComplianceDetails", nonCompTxtbx.Text.ToString());
            //    DLdb.SQLST2.CommandType = CommandType.Text;
            //    DLdb.SQLST2.Connection = DLdb.RS2;
            //    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            //    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.SQLST2.Parameters.RemoveAt(0);
            //    DLdb.RS2.Close();

            //}

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.RS.Close();

            // INSPECTION TYPES
            foreach (ListItem item in TypeOfInstallation.Items)
            {
                if (item.Selected)
                {
                    // CHECK IF EXISTS AND UPDATE OR ADD NEW
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from COCInstallations where TypeID = @TypeID and COCStatementID = @COCStatementID";
                    DLdb.SQLST.Parameters.AddWithValue("TypeID", item.Value.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "update COCInstallations set isActive = '1' where TypeID = @TypeID and COCStatementID = @COCStatementID";
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
                else
                {
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "update COCInstallations set isActive = '0' where TypeID = @TypeID and COCStatementID = @COCStatementID";
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
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            string paperBasedCOCs = "";
            string typeOfCOC = "";
            string paperCOCName = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID and Type='Paper'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                typeOfCOC = theSqlDataReader["Type"].ToString();
                paperCOCName = theSqlDataReader["PaperBasedCOC"].ToString();
                if (theSqlDataReader["paperBasedCOC"].ToString() == "" || theSqlDataReader["paperBasedCOC"] == DBNull.Value)
                {
                    paperBasedCOCs = "false";

                }
                else
                {
                    paperBasedCOCs = "true";
                }
            }
            else
            {
                paperBasedCOCs = "true";
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            if (paperBasedCOCs == "false")
            {
                errormsg.Visible = true;
                errormsg.InnerHtml = "You can't log the COC until you upload your paper based COC";
            }
            else
            {


                if (submitOTPpin.Text.ToString() == Session["IIT_OTPCodeSubAudit"].ToString())
                {
                    string AuditorID = "";
                    string AuditorIDUSerID = "";
                    string NumberTo = "";
                    string EmailAddress = "";
                    string FullName = "";
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID";
                    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        //DLdb.RS2.Open();
                        //DLdb.SQLST2.CommandText = "select * from Customers where CustomerID = @CustomerID";
                        //DLdb.SQLST2.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
                        //DLdb.SQLST2.CommandType = CommandType.Text;
                        //DLdb.SQLST2.Connection = DLdb.RS2;
                        //SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                        //if (theSqlDataReader2.HasRows)
                        //{
                        //    theSqlDataReader2.Read();
                        //    DLdb.RS3.Open();
                        //    DLdb.SQLST3.CommandText = "select * from Area where Name = @Name";
                        //    DLdb.SQLST3.Parameters.AddWithValue("Name", theSqlDataReader2["AddressSuburb"].ToString());
                        //    DLdb.SQLST3.CommandType = CommandType.Text;
                        //    DLdb.SQLST3.Connection = DLdb.RS3;
                        //    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();
                        //    if (theSqlDataReader3.HasRows)
                        //    {
                        //        theSqlDataReader3.Read();
                        //        DLdb.RS4.Open();
                        //        DLdb.SQLST4.CommandText = "select * from AuditorAreas where AreaID = @AreaID";
                        //        DLdb.SQLST4.Parameters.AddWithValue("AreaID", theSqlDataReader3["ID"].ToString());
                        //        DLdb.SQLST4.CommandType = CommandType.Text;
                        //        DLdb.SQLST4.Connection = DLdb.RS4;
                        //        SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();
                        //        if (theSqlDataReader4.HasRows)
                        //        {
                        //            theSqlDataReader4.Read();
                        //            AuditorID = theSqlDataReader4["AuditorID"].ToString();
                        //            DLdb.RS5.Open();
                        //            DLdb.SQLST5.CommandText = "select * from Auditor where AuditorID = @AuditorID";
                        //            DLdb.SQLST5.Parameters.AddWithValue("AuditorID", theSqlDataReader4["AuditorID"].ToString());
                        //            DLdb.SQLST5.CommandType = CommandType.Text;
                        //            DLdb.SQLST5.Connection = DLdb.RS5;
                        //            SqlDataReader theSqlDataReader5 = DLdb.SQLST5.ExecuteReader();
                        //            if (theSqlDataReader5.HasRows)
                        //            {
                        //                theSqlDataReader5.Read();
                        //                AuditorIDUSerID = theSqlDataReader5["UserID"].ToString();
                        //                NumberTo = theSqlDataReader5["phoneMobile"].ToString();
                        //                EmailAddress = theSqlDataReader5["email"].ToString();
                        //                FullName = theSqlDataReader5["fname"].ToString() + " " + theSqlDataReader5["lname"].ToString();
                        //            }
                        //            if (theSqlDataReader5.IsClosed) theSqlDataReader5.Close();
                        //            DLdb.SQLST5.Parameters.RemoveAt(0);
                        //            DLdb.RS5.Close();
                        //        }
                        //        if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                        //        DLdb.SQLST4.Parameters.RemoveAt(0);
                        //        DLdb.RS4.Close();
                        //    }
                        //    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        //    DLdb.SQLST3.Parameters.RemoveAt(0);
                        //    DLdb.RS3.Close();
                        //}
                        //if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        //DLdb.SQLST2.Parameters.RemoveAt(0);
                        //DLdb.RS2.Close();
                    }
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                    // UPDATE THE COC

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update COCStatements Set Status = 'Logged',AorB=@AorB,NonComplianceDetails=@NonComplianceDetails, DateLogged = getdate(),AuditorID=@AuditorID,COCNumber=COCStatementID where COCStatementID = @COCStatementID";
                    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                    DLdb.SQLST.Parameters.AddWithValue("AuditorID", "");
                    DLdb.SQLST.Parameters.AddWithValue("NonComplianceDetails", nonCompTxtbx.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("AorB", RadioButtonList1.SelectedValue);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    string designation = "";
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM PlumberDesignations where isActive = '1' and PlumberID=@PlumberID";
                    DLdb.SQLST.Parameters.AddWithValue("PlumberID", Session["IIT_UID"].ToString());
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
                    string weightingPoints = "";
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM Weighting where isActive = '1' and WeightingID='2'";
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        weightingID = theSqlDataReader["weightingID"].ToString();
                        if (designation == "Qualified Plumber  ")
                        {
                            weightingPoints = theSqlDataReader["Qualified"].ToString();
                        }
                        else if (designation == "Licensed Plumber")
                        {
                            weightingPoints = theSqlDataReader["Licensed"].ToString();
                        }
                        else if (designation == "Master Plumber")
                        {
                            weightingPoints = theSqlDataReader["Master"].ToString();
                        }
                        else if (designation == "Director Plumber")
                        {
                            weightingPoints = theSqlDataReader["Director"].ToString();
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.RS.Close();

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into  AssignedWeighting (UserID,WeightingID,Points,Type,weightingValue,TypePlumber) values (@UserID,@WeightingID,@Points,'Log COC',@weightingValue,@TypePlumber)";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("WeightingID", weightingID.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("Points", weightingPoints.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("weightingValue", weightingPoints.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("designation", designation.ToString());
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

                    string WorkCompletedby = "";

                    // UPDATE THE COC DETAILS
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update COCStatementDetails Set DescriptionofWork = @DescriptionofWork, WorkCompleteby = @WorkCompletedby where COCStatementID = @COCStatementID";
                    DLdb.SQLST.Parameters.AddWithValue("DescriptionofWork", DescriptionofWork.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("WorkCompletedby", WorkCompletedby);
                    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    // ****************************************************************************************
                    // BUILD THE PDF COC
                    // ****************************************************************************************

                    // CUSTOMERID
                    string CustomerIDs = "";
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from COCStatements where COCStatementID = @COCStatementID";
                    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            CustomerIDs = theSqlDataReader["CustomerID"].ToString();
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    // GET USER DETAILS
                    string gUID = Session["IIT_UID"].ToString();
                    string emailaddress = Session["IIT_EmailAddress"].ToString();
                    string name = Session["IIT_UName"].ToString();

                    // GET CLIENT DETAILS
                    string clientname = "";
                    string clientaddress = "";
                    string Clientemail = "";
                    string ClientTel = "";

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from Customers where CustomerID = @CustomerID";
                    DLdb.SQLST.Parameters.AddWithValue("CustomerID", CustomerIDs);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        clientname = theSqlDataReader["CustomerName"].ToString() + ' ' + theSqlDataReader["CustomerSurname"].ToString();
                        clientaddress = theSqlDataReader["AddressStreet"].ToString() + "<br />" + theSqlDataReader["AddressSuburb"].ToString() + "<br />" + theSqlDataReader["AddressCity"].ToString() + "<br />" + theSqlDataReader["Province"].ToString();
                        Clientemail = theSqlDataReader["CustomerEmail"].ToString();
                        ClientTel = theSqlDataReader["CustomerCellNo"].ToString();

                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.RS.Close();

                    // GET USER DETAILS
                    string uname = "";
                    string uemail = "";
                    string ucompany = "";
                    string usignature = "";
                    string uaddress = "";
                    string ucontact = "";
                    string uregno = "";

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select top 1 * from Users where UserID = @UserID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        uname = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                        uemail = theSqlDataReader["email"].ToString();
                        ucompany = theSqlDataReader["company"].ToString();
                        if (theSqlDataReader["signature"].ToString() != "" && theSqlDataReader["signature"] != DBNull.Value)
                        {
                            usignature = "<img src=\"http://197.242.82.242/pirbreg/signatures/" + theSqlDataReader["signature"].ToString() + "\" />";
                        }
                        else
                        {
                            usignature = "<img src=\"http://197.242.82.242/pirbreg/signatures/NoSignature.jpg\" />";
                        }
                        uaddress = theSqlDataReader["ResidentialStreet"].ToString() + " " + theSqlDataReader["ResidentialSuburb"].ToString() + " " + theSqlDataReader["ResidentialCity"].ToString() + "  " + theSqlDataReader["Province"].ToString() + " " + theSqlDataReader["ResidentialCode"].ToString();
                        ucontact = theSqlDataReader["fname"].ToString();
                        uregno = theSqlDataReader["regno"].ToString();
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.RS.Close();

                    // GET ALL ELEMENTS
                    string EmailHTML = "";
                    string EmailSub = "";
                    var html_FIELD_Content = "";

                    // BUILD CLIENT DETAILS
                    string clientdetails = "<div style='border:1px solid #EFEFEF;padding:10px;width:100%;'><b>Customer Name: </b>" + clientname + "<br />";
                    clientdetails += "<b>Email Address: </b>" + Clientemail + "<br />";
                    clientdetails += "<b>Tel No.: </b>" + ClientTel + "<br />";
                    clientdetails += "<b>Address: </b>" + clientaddress + "<br /></div>";

                    // BUILD THE PDF FILENAME
                    DateTime cDate = DateTime.Now;
                    string filename = cDate.ToString("ddMMyyyy") + "_" + gUID + "_COC_" + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + ".pdf";

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM COCInstallations where COCStatementID = @COCStatementID and isActive = '1'";
                    DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            // JSON //
                            string json = "";
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "Select TOP 1 * from FormUserData where DataID = @DataID order by createdate desc";
                            DLdb.SQLST2.Parameters.AddWithValue("DataID", theSqlDataReader["DataID"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                while (theSqlDataReader2.Read())
                                {
                                    json = theSqlDataReader2["json"].ToString();
                                }
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            // GET FORM ID
                            string frmid = "";
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "select* from[dbo].[FormLinks] where TypeID = @TypeID and FormType = 'COC'";
                            DLdb.SQLST2.Parameters.AddWithValue("TypeID", theSqlDataReader["TypeID"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                while (theSqlDataReader2.Read())
                                {
                                    frmid = theSqlDataReader2["FormID"].ToString();
                                }
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();

                            //FORM DETAILS
                            string frmName = "";
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "select * from Forms where FormID = @FormID";
                            DLdb.SQLST2.Parameters.AddWithValue("FormID", frmid.ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                while (theSqlDataReader2.Read())
                                {
                                    frmName = theSqlDataReader2["Name"].ToString();
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

                    string seltype = "";

                    if (RadioButtonList1.SelectedValue.ToString() == "A")
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
                                                        "                           </td><td style='border-bottom: solid 1px black;'>" + solarwaterHeating + "" +
                                                        "                          </td></tr>" +
                                                        "                       <tr>" +
                                                        "                           <td style='border-bottom: solid 1px black;'>" +
                                                        "                               Installation, Replacement and / or Repair of a<span style='color:maroon;'> Heat Pump </span>" +
                                                        "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                        "                           </td><td style='border-bottom: solid 1px black;'>" + heatpump + "" +
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
                                                                                    DescriptionofWork.Text.ToString() +
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
                                                        "                    <tr>" +
                                                        "                        <table border='1' width='100%'><tr>" +
                                                        "                           <td bgcolor='#FF0000'>" +
                                                        "                               <h4 style='background-color:red;text-align:center;color:white;'>IMPORTANT NOTICE</h4>" +
                                                        "                           </td></tr>" +
                                                        "                       <tr>" +
                                                        "                           <td>" +
                                                        "                               <ul>" +
                                                        "                                   <li>An incorrect statement of fact, including an omission, is an offence in terms of the PIRB Code of conduct, and will be subjected to PIRB disciplinary procedures.</li>" +
                                                        "                                   <li>A completed Certificate of Compliance must be provided to the owner/consumer within 5 days of the completion of the plumbing works and the details of the Certificate of Compliance must be logged electronically with the PIRB within that period.</li>" +
                                                        "                                   <li>The relevant plumbing work that was certified as complaint through the issuing of this certificate may be possibly be audited by a PIRB Auditor for compliance to the regulations, workmanship and health and safety of the plumbing.</li>" +
                                                        "                                   <li>If this Certificate of Compliance has been chosen for an audit you must cooperated fully with the PIRB Auditor in allowing them to carry out the relevant audit.</li>" +
                                                        "                                   <li>See reverse side of this Certificate of Compliance for further details</li>" +
                                                        "                               </ul>" +
                                                        "                          </td></tr>" +
                                                        "                       </table>" +
                                                        "                    </tr>" +
                                                        "                    <tr>" +
                                                        "                        <table border='0' width='100%'><tr>" +
                                                        "                           <td bgcolor='lightblue'>" +
                                                        "                               <h4 style='background-color:lightblue;text-align:center;color:white;'>OWNERS COPY</h4>" +
                                                        "                           </td></tr>" +
                                                        "                       </table>" +
                                                        "                    </tr>" +
                                                        "                    <tr>" +
                                                        "                        <table border='0' width='100%'><tr>" +
                                                        "                           <td bgcolor='lightgreen'>" +
                                                        "                               <h4 style='background-color:lightgreen;text-align:center;'>TERMS & CONDITIONS</h4>" +
                                                        "                           </td></tr>" +
                                                        "                       </table>" +
                                                        "                    </tr>" +
                                                        "                    <tr>" +
                                                        "                        <table border='0' width='100%'><tr>" +
                                                        "                           <td width='50%'>" +
                                                        "                           <h4>WHAT IS A PlUMBING CERTIFICATE Of COMPLIANCE (COC)?</h4>" +
                                                        "                               <p>A Plumbing COC is a means by which the Plumbing Industry Registration Board (PIRB)" +
                                                        "                                    licensed plumber self certifies that their work complies with all the current plumbing regulations" +
                                                        "                                    and laws as define by the National Compulsory Standards and Local Bylaws.COC's may" +
                                                        "                                    only be purchased, and used by a registered and approved PIRB licensed persons and at" +
                                                        "                                    the time of purchase, the COC is captured against the PIRB licensed plumber, and becomes" +
                                                        "                                    their responsibility.Upon issuing of a COC the PIRB licensed plumber has to log the relevant" +
                                                        "                                    COC into the PIRB's Plumbing audit / data management system within five days.Each day, a" +
                                                        "                                    computer random selection of jobs for which a COC has be logged with the PIRB, is selected for" +
                                                        "                                    an audit. Upon which a PIRB auditor will be sent out to carry out the audit.If the installation is" +
                                                        "                                    found to be incorrect or not up to standard the PIRB licensed plumber will be sent a rectification" +
                                                        "                                    notice on which the licensed plumber will have to react within the specified period as by the" +
                                                        "                                    auditor.This is usually 5 days.</p>" +
                                                        "                          </td>" +
                                                        "                           <td width='50%'>" +
                                                        "                           <h4>How compliance certificates may be purchased</h4>" +
                                                        "                           <p>Compliance Certificates may be purchased by licensed persons or authorized persons through any of the following methods:</p>" +
                                                        "                               <ul>" +
                                                        "                                   <li><b>Over the counter at the Plumbing Industry Registration Board offices.</b> Purchasers will need to present their current license card.Compliance certificates may only be given on-the spot where payment is by cash, credit card, bank transfer(confirmation required) and or bank cheque.</li>" +
                                                        "                                   <li><b>Online: </b>Purchasers should log on to www.pirb.co.za, click on Order COC and follow the prompts.</li>" +
                                                        "                                   <li><b>Resellers (merchant): </b>The PIRB Licensed Plumber will need to present his/her current licensed card upon purchasing a compliance certificate from a participating reseller(merchant) outlet. No third parties may purchase from a reseller unless preapproved and verified by the PIRB first.</li>" +
                                                        "                               </ul>" +
                                                        "                          </td></tr>" +
                                                        "                       <tr>" +
                                                        "                           <td width='50%'>" +
                                                        "                           <h4>JOBS WHICH REQUIRE A COC</h4>" +
                                                        "                               <p>COC must be provided to the consumer for all plumbing jobs which fall into one or more of the following categories:</p>" +
                                                        "                               <ul>" +
                                                        "                                   <li>Where the total value of the work, including materials, labour and VAT, is more than the prescribe value as defined by the PIRB(material costs must be included, regardless of whether the materials were supplied by another person) a certificate must be issued for the following:</li>" +
                                                        "                                   <ul>" +
                                                        "                                       <li>When an Installation, Replacement and/or Repair of Hot Water Systems and/ or Components is carried out</li>" +
                                                        "                                       <li>When an Installation, Replacement and/or Repair of Cold Water Systems and/ or Components is carried out</li>" +
                                                        "                                       <li>Installation, Replacement and/or Repair of Sanitary-ware and Sanitary-fittings is carried out.</li>" +
                                                        "                                       <li>Installation, Replacement and/or Repair of a Solar Water Heating System</li>" +
                                                        "                                       <li>Installation, Replacement and/or Repair of a Below-ground Drainage System</li>" +
                                                        "                                       <li>Installation, Replacement and/or Repair of an Above-ground Drainage System</li>" +
                                                        "                                       <li>Installation, Replacement and/or Repair of a Rain Water Disposal System</li>" +
                                                        "                                       <li>Installation, Replacement and/or Repair of a Heat Pump Water Heating System</li>" +
                                                        "                                   </ul>" +
                                                        "                                   <li>Any work that requires the installation, replacement and/or repair of any of an electrical / solar hot water cylinder valves or components must have a COC issued to the consumer regardless of the cost.</li>" +
                                                        "                               </ul>" +
                                                        "                          </td>" +
                                                        "                           <td width='50%'>" +
                                                        "                           <h4>DISPOSAL OF COMPLIANCE CERTIFICATES</h4>" +
                                                        "                           <p>If for any reason, a licensed person does not intend to use a compliance certificate for its intended purpose they should return it to the PIRB office and, if all is found to be in order, a refund could be arranged.If a licensed person has a compliance certificate stolen or loses a compliance certificate, he should report it immediately to the PIRB in the form of a statutory declaration.</p>" +
                                                        "                           <h4>THE PURPOSE OF AN AUDIT</h4>" +
                                                        "                               <p>Audits are conducted to provide a measure of the standard of the plumbing work being performed across the country.The aim is to ensure a correct and consistent application of the standards is reflected in the work done.</p>" +
                                                        "                           <h4>AUDIT PROCESS</h4>" +
                                                        "                               <p>A computer random selection of COC for which a compliance certificate has be lodged with the PIRB, is selected for an audit. Audits are conducted by qualified experienced trained plumbers and experts authorized by the PIRB to perform the function.PIRB Plumbing Auditors are registered with the PIRB and carry identification cards.When one of your COC has been selected for an audit you will be contacted by the PIRB Auditor.You will be asked for details of where the work was performed and arrangements will be made by the Auditor with the relevant consumer.You will be requested by the Auditor to attend the audit.</p>" +
                                                        "                          </td></tr>" +
                                                        "                       <tr>" +
                                                        "                           <td width='50%'>" +
                                                        "                           <h4>STAGE AT WHICH COC MUST BE COMPLTED</h4>" +
                                                        "                               <p>A completed COC must be provided to the consumer within <b>5 days</b> of the completion of the plumbing work and the details of the COC must be logged electronically with the PIRB within that" +
                                                        "                                   period. A job is considered to be completed when the plumbing work is practically completed or when plumbing work is capable of being used within an existing system - whichever comes first.</p>" +
                                                        "                           <h4>LOGGING COC THROUGH THE SMS SERVICE</h4>" +
                                                        "                               <p>Details are to be SMS to 082 934 9334 in the following format:<br /><br /> Your License Registration Number; Compliance Certificate Number; Numeric Code(s) of what work was undertaken; Area code where the work was carried out/ installed Example: 00001 / 75; 123456; 01; 0149.<br /><br /> Incorrectly formatted sms's will be rejected and you will be required to resubmit the details.</p>" +
                                                        "                          </td>" +
                                                        "                           <td width='50%'>" +
                                                        "                           <h4>WHAT HAPPENS IF MY WORK DOES NOT PASS AN AUDIT?</h4>" +
                                                        "                           <p>If the audited work is found not to comply, you will be advised of the work requiring attention in the form of a Rectification Notice.You are required to rectify the work in the time period specified by the auditor.This is usually 5 days.The work may then be re-audited.Failure to respond, act or co-operate will result in disciplinary procedures.</p>" +
                                                        "                           <h4>IF YOU DISAGREE WITH AN AUDIT RESULT</h4>" +
                                                        "                           <p>If you believe that the rectification notice is incorrect, you may contact the PIRB and your objection will be reviewed.Objections must be submitted in writing on the relevant PIRB form, obtainable from PIRB office.</p>" +
                                                        "                          </td></tr>" +
                                                        //  "                       <tr>" +
                                                        "                       </table>" +
                                                        "                    </tr><br/><br/><br/><br/><br/><br/><br/><br/><br/>" +

                                                        "                    <tr>" +
                                                        "                        <table border='1' width='100%'>" +
                                                        "                       <tr>" +
                                                        "                           <td>" +
                                                        "                               <h4 style='text-align:center;'>Code</h4>" +
                                                        "                           </td>" +
                                                        "                           <td bgcolor='lightgreen'>" +
                                                        "                               <h4 style='text-align:center;'>Type of Installation Carried Out:</h4>" +
                                                        "                           </td>" +
                                                        "                       </tr>" +
                                                        "                       <tr>" +
                                                        "                           <td>" +
                                                        "                               <h4 style='text-align:center;color:red;'>01</h4>" +
                                                        "                           </td>" +
                                                        "                           <td bgcolor='rgba(255,0,0,0.2)'>" +
                                                        "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:red;'>Hot Water System and /or Components </span></b><br />(A Certificate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing work carried out on the hot water reticulation system upstream of the pressure regulating valve, which shall include but not be limited to: the pressure regulating valve; an electrical hot water cylinder; all relevant valves and components and all hot water pipe and fittings, and shall end at any of the hot water terminal fittings; but shall exclude any sanitary fittings, solar and heat pump installations. The scope of work and non-compliance on pre-existing installations by others must be clearly noted in the installation details provided overleaf.)</p>" +
                                                        "                           </td>" +
                                                        "                       </tr>" +
                                                        "                       <tr>" +
                                                        "                           <td>" +
                                                        "                               <h4 style='text-align:center;color:lightblue;'>02</h4>" +
                                                        "                           </td>" +
                                                        "                           <td bgcolor='rgba(177,226,243,0.2)'>" +
                                                        "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:lightblue;'>Cold Water System and /or Components </span></b><br />(A Certificate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing works where work has been carried out on the cold water reticulation system upstream of the municipal metering valve, which shall include but not be limited to: all relevant valves and components relating to the cold water system and all cold water pipe and fittings, and shall end at any of the relevant cold water terminal fittings; but shall exclude any sanitary fittings. The scope of work and any non-compliance pre-existing installations by others must be clearly noted in the installation details provided overleaf.)</p>" +
                                                        "                           </td>" +
                                                        "                       </tr>" +
                                                        "                       <tr>" +
                                                        "                           <td>" +
                                                        "                               <h4 style='text-align:center;color:blue;'>03</h4>" +
                                                        "                           </td>" +
                                                        "                           <td bgcolor='rgba(0,0,255,0.2)'>" +
                                                        "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:blue;'>Sanitary-ware and Sanitary-fittings </span></b><br />(A Certificate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing works r where work has been carried out on the Sanitary-ware and Sanitary-fittings.The scope of work and any non-compliance pre-existing installations by others must be clearly noted in the installation details provided overleaf.) </p>" +
                                                        "                           </td>" +
                                                        "                       </tr>" +
                                                        "                       <tr>" +
                                                        "                           <td>" +
                                                        "                               <h4 style='text-align:center;color:orange;'>04</h4>" +
                                                        "                           </td>" +
                                                        "                           <td bgcolor='rgba(255,164,0,0.2)'>" +
                                                        "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:orange;'>Solar Water Heating System</span></b><br />(A Certificate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing works where work has been carried out on the Solar Water Heating System which shall include but not be limited to: the hot water reticulation system upstream of the pressure regulating valve; the pressure regulating valve; if applicable the electrical hot water cylinder; a solar (electrical)hot water cylinder; all relevant valves and components and all hot water pipe and fittings, and shall end at any of the relevant hot water terminal fittings; but shall exclude any sanitary fittings. The scope of work and non-compliance pre-existing installations by others must be clearly noted in the installation details provided overleaf.) <span style='color:red;'>Work can only be undertaken by a Licensed Plumber registered to do this specialised work.</span></p>" +
                                                        "                           </td>" +
                                                        "                       </tr>" +
                                                        "                       <tr>" +
                                                        "                           <td>" +
                                                        "                               <h4 style='text-align:center;color:brown;'>05</h4>" +
                                                        "                           </td>" +
                                                        "                           <td bgcolor='rgba(183,41,41,0.2)'>" +
                                                        "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:brown;'>Below-ground Drainage System</span></b><br />(A Certificate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing works where work has been carried out on a below- ground drainage system, which shall include but not be limited to: septic tank and French drain installations.The scope of work and any non-compliance pre-existing installations by others must be clearly noted in the installation details provided overleaf.) </p>" +
                                                        "                           </td>" +
                                                        "                       </tr>" +
                                                        "                       <tr>" +
                                                        "                           <td>" +
                                                        "                               <h4 style='text-align:center;color:green;'>06</h4>" +
                                                        "                           </td>" +
                                                        "                           <td bgcolor='rgba(0,127,0,0.2)'>" +
                                                        "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:green;'>Above-ground Drainage System</span></b><br />(A Certificate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing works where work has been carried out on an above-ground drainage system, which shall include but not be limited to: all internal and external waste water and soil drainage but shall excluded any sanitary ware fixtures.The scope of work and any non-compliance pre-existing installations by others must be clearly noted in the installation details provided overleaf.)</p>" +
                                                        "                           </td>" +
                                                        "                       </tr>" +
                                                        "                       <tr>" +
                                                        "                           <td>" +
                                                        "                               <h4 style='text-align:center;color:darkblue;'>07</h4>" +
                                                        "                           </td>" +
                                                        "                           <td bgcolor='rgba(0,0,165,0.2)'>" +
                                                        "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:darkblue;'>Rain Water Disposal System</span></b><br />(A Certificate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing works i or where work has been carried out on a rain water disposal system, which shall include but not be limited to: storm water drainage, guttering and flashing.The scope of work and any non-compliance pre-existing installations by others must be clearly noted in the installation details provided overleaf.) </p>" +
                                                        "                           </td>" +
                                                        "                       </tr>" +
                                                        "                       <tr>" +
                                                        "                           <td>" +
                                                        "                               <h4 style='text-align:center;color:maroon;'>08</h4>" +
                                                        "                           </td>" +
                                                        "                           <td bgcolor='rgba(130,0,0,0.2)'>" +
                                                        "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:maroon;'>Heat Pump</span></b><br />(A Certifcate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing works where work has been carried out on the Heat Pump Water Heating System which shall include but not be limited to: the hot water reticulation system upstream of the pressure regulating valve; the pressure regulating valve; if applicable the electrical hot water cylinder; a heat pump unit; all relevant valves and components and all hot water pipe and fttings, and shall end at any of the relevant hot water terminal fittings; but shall exclude any sanitary fttings. The scope of work and non-compliance pre-existing installations by others must be clearly noted in the installation details provided overleaf.) <span style='color:red;'>Work can only be undertaken by a Licensed Plumber registered to do this specialised work.</span></p>" +
                                                        "                           </td>" +
                                                        "                       </tr>" +
                                                        "                       </table>" +
                                                        "                    </tr>" +
                                                        //"                    <tr>" +
                                                        //"                    <tr>" +
                                                        //"                        <td align='bottom'>" +
                                                        //"                             <hr><table border='0' width='100%'><tr><td width='50%'><b>Logged by: </b>" + uname + "<br />" +
                                                        //"                             <b>Email Address: </b>" + uemail + "<br />" +
                                                        //"                             <b>Company: </b>" + ucompany + "<br />" +
                                                        //"                             <b>Address: </b>" + uaddress + "<br />" +
                                                        //"                             <b>Contact Number: </b>" + ucontact + "<br />" +
                                                        //"                             <b>PIRB Reg No.: </b>" + uregno + "</td><td width='50%' valign='top'><b>Signature</b><br />" + usignature + "<br /></td></tr></table>" +
                                                        //"                        </td>" +
                                                        //"                    </tr>" +
                                                        "                </table>" +
                                                        "            </td>" +
                                                        "        </tr>" +
                                                        "    </table>" +
                                                        "</body>");



                    // EMAIL THE NEW USERS

                    if (typeOfCOC == "Paper")
                    {
                        string path = Server.MapPath("~/pdf/") + paperCOCName;

                        string HTMLBody = "Dear " + Session["IIT_UName"].ToString() + "<br /><br />Thank you for completing your C.O.C.<br /><br /><br />Regards<br />System Administrator";
                        string HTMLSubject = "C.O.C Certificate";
                        string targetAddress = Session["IIT_EmailAddress"].ToString();
                        DLdb.sendEmail(HTMLBody, HTMLSubject, "veronike@slugg.co.za", targetAddress, path);

                        DLdb.RS.Open();
                        DLdb.SQLST.Parameters.Clear();
                        DLdb.SQLST.CommandText = "update COCStatements Set COCFilename = @COCFilename,isPlumberSubmitted='1',isLogged='1',isInspectorSubmitted='0' where COCStatementID = @COCStatementID";
                        DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                        DLdb.SQLST.Parameters.AddWithValue("COCFilename", paperCOCName);
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
                        var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(htmlContent);
                        string path = Server.MapPath("~/pdf/") + filename;
                        File.WriteAllBytes(path, pdfBytes);

                        string HTMLBody = "Dear " + Session["IIT_UName"].ToString() + "<br /><br />Thank you for completing your C.O.C.<br /><br />Please see the attached certificate<br /><br />Regards<br />System Administrator";
                        string HTMLSubject = "C.O.C Certificate";
                        string targetAddress = Session["IIT_EmailAddress"].ToString();
                        DLdb.sendEmail(HTMLBody, HTMLSubject, "veronike@slugg.co.za", targetAddress, path);

                        DLdb.RS.Open();
                        DLdb.SQLST.Parameters.Clear();
                        DLdb.SQLST.CommandText = "update COCStatements Set COCFilename = @COCFilename,isPlumberSubmitted='1',isLogged='1',isInspectorSubmitted='0' where COCStatementID = @COCStatementID";
                        DLdb.SQLST.Parameters.AddWithValue("COCFilename", filename);
                        DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
                        DLdb.SQLST.CommandType = CommandType.Text;
                        DLdb.SQLST.Connection = DLdb.RS;
                        theSqlDataReader = DLdb.SQLST.ExecuteReader();

                        if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.RS.Close();
                    }

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                    DLdb.SQLST.Parameters.AddWithValue("Message", "Plumber has submitted the COC");
                    DLdb.SQLST.Parameters.AddWithValue("Username", Session["IIT_UName"].ToString());
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

                    DLdb.DB_Close();

                    Response.Redirect("EditCOCStatement.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("C.O.C Submitted successfully"));
                }
                else
                {
                    Response.Redirect("EditCOCStatement.aspx?cocid=" + Request.QueryString["cocid"] + "&err=" + DLdb.Encrypt("OTP is incorrect"));
                    //errormsg.Visible = true;
                    //errormsg.InnerHtml = "OTP is incorrect";
                }
            }
        }
         
        protected void btnSubRefix_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.Parameters.Clear();
            DLdb.SQLST.CommandText = "update COCStatements Set isPlumberSubmitted='1',isInspectorSubmitted='0' where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            DLdb.DB_Close();

            Response.Redirect("EditCOCStatement.aspx?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("Refix Submitted successfully"));


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

        protected void Button1_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();


            if (paperBasedCOC.HasFiles)
            {
                foreach (HttpPostedFile File in paperBasedCOC.PostedFiles)
                {
                    string filenames = File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/pdf/"), filenames));
                }
            }

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatements Set paperBasedCOC=@paperBasedCOC where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.Parameters.AddWithValue("paperBasedCOC", paperBasedCOC.FileName);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
            DLdb.SQLST.Parameters.AddWithValue("Message", "New image of paper COC uploaded");
            DLdb.SQLST.Parameters.AddWithValue("Username", Session["IIT_UName"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
            DLdb.SQLST.Parameters.AddWithValue("CertificateID", DLdb.Decrypt(Request.QueryString["cocid"]).ToString());
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

            btnSave_Click(sender, e);

            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (FileUpload2.HasFiles)
            {
                foreach (HttpPostedFile File in FileUpload2.PostedFiles)
                {
                    string filenames = File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AuditorImgs/"), filenames));

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,ReviewID) values (@imgsrc,@UserID,@FieldID,@tempID,'1',@ReviewID); Select Scope_Identity() as ImgID";
                    DLdb.SQLST.Parameters.AddWithValue("imgsrc", filenames);
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("FieldID", "");
                    DLdb.SQLST.Parameters.AddWithValue("tempID", "");
                    DLdb.SQLST.Parameters.AddWithValue("ReviewID", TextBox1.Text.ToString());
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
            }



            DLdb.DB_Close();
            // Page.Response.Redirect(Page.Request.Url.ToString(), true);
            Response.Redirect("EditCOCStatement?cocid=" + Request.QueryString["cocid"].ToString());
        }

        protected void Button3_Click1(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (FileUpload3.HasFiles)
            {
                foreach (HttpPostedFile File in FileUpload3.PostedFiles)
                {
                    string filenames = File.FileName;
                    File.SaveAs(System.IO.Path.Combine(Server.MapPath("~/AuditorImgs/"), filenames));

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,tempID,isFile,ReviewID,isCompleteImg,COCID) values (@imgsrc,@UserID,@FieldID,@tempID,'1',@ReviewID,'1',@COCID); Select Scope_Identity() as ImgID";
                    DLdb.SQLST.Parameters.AddWithValue("imgsrc", filenames);
                    DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("FieldID", "");
                    DLdb.SQLST.Parameters.AddWithValue("tempID", "");
                    DLdb.SQLST.Parameters.AddWithValue("ReviewID", "");
                    DLdb.SQLST.Parameters.AddWithValue("COCID", DLdb.Decrypt(Request.QueryString["cocid"]));
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
            }



            DLdb.DB_Close();
            btnSave_Click(sender, e);
            Response.Redirect("EditCOCStatement?cocid=" + Request.QueryString["cocid"].ToString());
        }

        protected void selSuburb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from areasuburbs where Name=@Name";
            DLdb.SQLST.Parameters.AddWithValue("Name", selSuburb.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "select * from area where ID=@ID";
                DLdb.SQLST2.Parameters.AddWithValue("ID", theSqlDataReader["CityID"].ToString());
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                if (theSqlDataReader2.HasRows)
                {
                    theSqlDataReader2.Read();
                    AddressCity.SelectedValue = theSqlDataReader2["Name"].ToString();
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "select * from province where ID=@ID";
                    DLdb.SQLST3.Parameters.AddWithValue("ID", theSqlDataReader2["ProvinceID"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();
                    if (theSqlDataReader3.HasRows)
                    {
                        theSqlDataReader3.Read();
                        Province.SelectedValue = theSqlDataReader3["Name"].ToString();
                    }
                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();
                }
                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }
    }
}