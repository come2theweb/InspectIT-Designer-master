using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InspectIT
{
    public partial class NewPlumberRegistrationUpdate : System.Web.UI.Page
    {
        public string userID = "";
        public string ApplicationID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string newAppID = "";
            string ComPID = "";
            if (Request.QueryString["npid"] != null)
            {
                newAppID = DLdb.Decrypt(Request.QueryString["npid"].ToString());
                //FileUpload1.Visible = false;
                //postalCities.Visible = false;
                //postalSuburb.Visible = false;
                //physicalCities.Visible = false;
                //physicalSuburb.Visible = false;

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from NewApplications where ApplicationID=@ApplicationID";
                DLdb.SQLST.Parameters.AddWithValue("ApplicationID", newAppID.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
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
                        DropDownList1.SelectedValue = theSqlDataReader["RegistrationCard"].ToString();
                        DropDownList2.SelectedValue = theSqlDataReader["DeliveryMethod"].ToString();
                        declareName.Text= theSqlDataReader["DeclarationName"].ToString();
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
            }
            

            if (!IsPostBack)
            {
                ERRMSGsub.Visible = false;
               
            }
            DLdb.DB_Close();
        }
        

        protected void submitNewApplication_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            
            if (!CheckBox1.Checked)
            {
                ERRMSGsub.Visible = true;
                ERRMSGsub.InnerHtml = "Please make sure you have checked the procedure of registration checkbox";
            }
            else if (!CheckBox2.Checked)
            {
                ERRMSGsub.Visible = true;
                ERRMSGsub.InnerHtml = "Please make sure you have checked the Code of Conduct checkbox";
            }
            else if (!CheckBox3.Checked)
            {
                ERRMSGsub.Visible = true;
                ERRMSGsub.InnerHtml = "Please make sure you have checked the Acknowledgement checkbox";
            }
            else if (!CheckBox4.Checked)
            {
                ERRMSGsub.Visible = true;
                ERRMSGsub.InnerHtml = "Please make sure you have checked the declaration checkbox";
            }
            else
            {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into NewApplicationsUpdate (RegNo,UserID,ProcedureRegistration,CodeConduct,Acknowledgement,Declaration,DeclarationName,DeclarationIDNumber,RegistrationCard,DeliveryMethod) values (@RegNo,@UserID,@ProcedureRegistration,@CodeConduct,@Acknowledgement,@Declaration,@DeclarationName,@DeclarationIDNumber,@RegistrationCard,@DeliveryMethod); select scope_identity() as ApplicationID;";
                    DLdb.SQLST.Parameters.AddWithValue("RegistrationCard", DropDownList1.SelectedValue);
                    DLdb.SQLST.Parameters.AddWithValue("DeliveryMethod", DropDownList2.SelectedValue);
                    DLdb.SQLST.Parameters.AddWithValue("ProcedureRegistration", CheckBox1.Checked ? 1 : 0);
                    DLdb.SQLST.Parameters.AddWithValue("CodeConduct", CheckBox2.Checked ? 1 : 0);
                    DLdb.SQLST.Parameters.AddWithValue("Acknowledgement", CheckBox3.Checked ? 1 : 0);
                    DLdb.SQLST.Parameters.AddWithValue("Declaration", CheckBox4.Checked ? 1 : 0);
                    DLdb.SQLST.Parameters.AddWithValue("DeclarationName", declareName.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("DeclarationIDNumber", declareIDnum.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("RegNo", regno.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("UserID", TxtUdi.Text.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();
                        ApplicationID = theSqlDataReader["ApplicationID"].ToString();
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
                    DLdb.RS.Close();

                if (DirectorPlumber.Checked == true)
                {
                    //DESIGNATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberDesignations (PlumberID,Designation,ApplicationID) VALUES (@PlumberID,@Designation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Designation", "Director Plumber");
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    
                }
                if (MasterPlumber.Checked == true)
                {
                    //DESIGNATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberDesignations (PlumberID,Designation,ApplicationID) VALUES (@PlumberID,@Designation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Designation", "Master Plumber");
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    
                }
                if (MasterPlumberTrainingAssesor.Checked == true)
                {
                    //SPECIALISATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberSpecialisations (PlumberID,Specialisation,ApplicationID) VALUES (@PlumberID,@Specialisation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Specialisation", "Training Assesor");
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                if (MasterPlumberEstimator.Checked == true)
                {
                    //SPECIALISATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberSpecialisations (PlumberID,Specialisation,ApplicationID) VALUES (@PlumberID,@Specialisation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Specialisation", "Estimator");
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                if (MasterPlumberArbitrator.Checked == true)
                {
                    //SPECIALISATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberSpecialisations (PlumberID,Specialisation,ApplicationID) VALUES (@PlumberID,@Specialisation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Specialisation", "Arbitrator");
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                if (LicensedPlumber.Checked == true)
                {
                    //DESIGNATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberDesignations (PlumberID,Designation,ApplicationID) VALUES (@PlumberID,@Designation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Designation", "Licensed Plumber");
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                   
                    
                }
                if (LicensedPlumberSolar.Checked == true)
                {
                    //SPECIALISATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberSpecialisations (PlumberID,Specialisation,ApplicationID) VALUES (@PlumberID,@Specialisation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Specialisation", "Solar");
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    
                }
                if (LicensedPlumberHeatPump.Checked == true)
                {
                    //SPECIALISATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberSpecialisations (PlumberID,Specialisation,ApplicationID) VALUES (@PlumberID,@Specialisation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Specialisation", "Heat Pump");
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                   
                }
                if (LicensedPlumberGas.Checked == true)
                {
                    //SPECIALISATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberSpecialisations (PlumberID,Specialisation,ApplicationID) VALUES (@PlumberID,@Specialisation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Specialisation", "Gas");
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                if (TechnicalOperatorPractitioner.Checked == true)
                {
                    // DESIGNATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberDesignations (PlumberID,Designation,ApplicationID) VALUES (@PlumberID,@Designation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Designation", "Technical Operator Practitioner");
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                if (TechnicalOperatorPractitionerDrainage.Checked == true)
                {
                    //SPECIALISATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberSpecialisations (PlumberID,Specialisation,ApplicationID) VALUES (@PlumberID,@Specialisation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Specialisation", "Drainage");
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                if (TechnicalOperatorPractitionerColdWater.Checked == true)
                {
                    //SPECIALISATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberSpecialisations (PlumberID,Specialisation,ApplicationID) VALUES (@PlumberID,@Specialisation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Specialisation", "Cold Water");
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                if (TechnicalOperatorPractitionerHotWater.Checked == true)
                {
                    //SPECIALISATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberSpecialisations (PlumberID,Specialisation,ApplicationID) VALUES (@PlumberID,@Specialisation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Specialisation", "Hot Water");
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                if (TechnicalOperatorPractitionerWaterEnergyEfficiency.Checked == true)
                {
                    //SPECIALISATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberSpecialisations (PlumberID,Specialisation,ApplicationID) VALUES (@PlumberID,@Specialisation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Specialisation", "Water Energy Efficiency");
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                if (TechnicalAssistantPractitioner.Checked == true)
                {
                    //DESIGNATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberDesignations (PlumberID,Designation,ApplicationID) VALUES (@PlumberID,@Designation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Designation", "Technical Assistant Practitioner");
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
                if (LearnerPlumber.Checked == true)
                {
                    //DESIGNATION
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "INSERT INTO PlumberDesignations (PlumberID,Designation,ApplicationID) VALUES (@PlumberID,@Designation,@ApplicationID)";
                    DLdb.SQLST2.Parameters.AddWithValue("PlumberID", TxtUdi.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("ApplicationID", ApplicationID.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Designation", "Learner Plumber");
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                   
                }


            }
            DLdb.DB_Close();

            Response.Redirect("Default");
        }
         
        protected void searchRegNo_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from users where regno=@regno";
            DLdb.SQLST.Parameters.AddWithValue("regno", regno.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                Name.Text = theSqlDataReader["fname"].ToString();
                title.Text = theSqlDataReader["title"].ToString();
                Surname.Text = theSqlDataReader["lname"].ToString();
                TxtUdi.Text = theSqlDataReader["UserID"].ToString();
                pirbid.Text = theSqlDataReader["PIRBID"].ToString();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            DLdb.DB_Close();
        }
    }
}