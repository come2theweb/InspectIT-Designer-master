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
    public partial class newApplicationsUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string delBtnClass = "";
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }
            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                //addBtn.Visible = false;
                delBtnClass = "hide";
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {
                //addBtn.Visible = true;
            }


            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }
            if (Session["IIT_Role"].ToString() != "Administrator")
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
            

            displayusers.InnerHtml = "";
            displayusers_del.InnerHtml = "";
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM NewApplicationsUpdate where isActive='1' and isRejected='0' and isApproved='0'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DateTime submitted = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                    DateTime created = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                    DateTime today = DateTime.Now;

                    string checkImg = "<img src='assets/chk.png' style='height:20px;'/>";
                    string QualificationVerified = "";
                    string ProofExperience = "";
                    string plumberName = "";
                    string PaymentRecieved = "";
                    string Declaration = "";
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
                            plumberName = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                            
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    if (theSqlDataReader["QualificationVerified"].ToString() == "True")
                    {
                        QualificationVerified = checkImg;
                    }

                    if (theSqlDataReader["ProofExperience"].ToString() == "True")
                    {
                        ProofExperience = checkImg;
                    }

                    if (theSqlDataReader["Declaration"].ToString() == "True")
                    {
                        Declaration = checkImg;
                    }

                    if (theSqlDataReader["PaymentRecieved"].ToString() == "True")
                    {
                        PaymentRecieved = checkImg;
                    }
                    
                    displayusers.InnerHtml += "<tr>" +
                                                       "<td>" + submitted.ToString("dd/MM/yyyy") + "</td>" +
                                                       "<td>" + plumberName + "</td>" +
                                                       "<td>" + (today - created).Days + "</td>" +
                                                       "<td>" + QualificationVerified + "</td>" +
                                                       "<td>" + ProofExperience + "</td>" +
                                                       "<td>" + Declaration + "</td>" +
                                                       "<td>" + PaymentRecieved + "</td>" +
                                                       "<td><a href='NewPlumberRegistrationUpdateAdmin?npid=" + DLdb.Encrypt(theSqlDataReader["ApplicationID"].ToString()) + "'><div class=\"btn btn-sm btn-primary\" title=\"Edit\"><i class=\"fa fa-pencil\"></i></div></a></td>" +
                                                   "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM performancetypes where isActive='0'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    //displayusers_del.InnerHtml += "<tr>" +
                    //                                   "<td>" + theSqlDataReader["Type"].ToString() + "</td>" +
                    //                                   "<td>" + theSqlDataReader["Points"].ToString() + "</td>" +
                    //                                   "<td>" + theSqlDataReader["isActive"].ToString() + "</td>" +
                    //                                   "<td><a href='PerformanceTypesEdit?id=" + theSqlDataReader["PerformanceID"].ToString() + "'><div class=\"btn btn-sm btn-primary\" title=\"Edit\"><i class=\"fa fa-pencil\"></i></div></a>" +

                    //                                   "<div class=\"btn " + delBtnClass + " btn-sm btn-success\" onclick=\"deleteconf('DeletePerformanceType.aspx?op=undel&id=" + theSqlDataReader["PerformanceID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                    //                               "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();
        }
        
    }
}