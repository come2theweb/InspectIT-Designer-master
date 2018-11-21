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
    public partial class reportprofbody36 : System.Web.UI.Page
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
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }

            string delBtnClass = "";

            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                addBtn.Visible = false;
                delBtnClass = "hide";
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {
                addBtn.Visible = true;
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

          displayauditors.InnerHtml = "";

            if (!IsPostBack)
            {
                string Designation = "";
                string DesignationID = "";
                string DesignationCode = "";

                DLdb.RS3.Open();
                DLdb.SQLST3.CommandText = "Select * from PlumberDesignations where isActive = '1'";
                DLdb.SQLST3.CommandType = CommandType.Text;
                DLdb.SQLST3.Connection = DLdb.RS3;
                SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                if (theSqlDataReader3.HasRows)
                {
                    while (theSqlDataReader3.Read())
                    {
                        Designation = theSqlDataReader3["Designation"].ToString();
                        DesignationID = theSqlDataReader3["DesignationID"].ToString();
                        DesignationCode = theSqlDataReader3["DesignationCode"].ToString();

                        // LOAD PLUMBER INFORMATION
                        DLdb.RS.Open();
                        DLdb.SQLST.CommandText = "Select * from Users where isActive = '1' and UserID=@UserID";
                        DLdb.SQLST.Parameters.AddWithValue("UserID", theSqlDataReader3["PlumberID"].ToString());
                        DLdb.SQLST.CommandType = CommandType.Text;
                        DLdb.SQLST.Connection = DLdb.RS;
                        SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                        if (theSqlDataReader.HasRows)
                        {
                            while (theSqlDataReader.Read())
                            {
                                DateTime start = Convert.ToDateTime(theSqlDataReader["RegistrationStart"].ToString());
                                DateTime end = Convert.ToDateTime(theSqlDataReader["RegistrationEnd"].ToString());
                                string startDate = start.ToString("yyyyMMdd");
                                string endDate = end.ToString("yyyyMMdd");
                                DateTime today = DateTime.Today;

                                string strtuctureStatus = "";
                                if (theSqlDataReader["isSuspended"].ToString() == "True")
                                // if (theSqlDataReader["RegistrationSuspended"].ToString() == "True") //|| today >= Convert.ToDateTime(theSqlDataReader["ExpiryNotificationDate"])
                                {
                                    strtuctureStatus = "579";
                                }
                                else
                                {
                                    strtuctureStatus = "578";
                                }

                                displayauditors.InnerHtml += "<tr>" +
                                                            "<td>" + theSqlDataReader["IdNo"].ToString() + "</td>" +
                                                            "<td>" + theSqlDataReader["Alternate"].ToString() + "</td>" +
                                                            //"<td>" + theSqlDataReader["AlternativeIDType"].ToString() + "</td>" +
                                                            "<td>" + DesignationCode + "</td>" + // 413 licensed plumber, 414 qualified plumber, 415 hot water system installer
                                                            "<td>" + theSqlDataReader["RegNo"].ToString() + "</td>" +// make this their reg no
                                                            "<td>831</td>" +// plumbers need no 831
                                                            "<td>" + startDate + "</td>" +
                                                            "<td>" + endDate + "</td>" +
                                                            "<td>" + strtuctureStatus + "</td>" + // 578 active, 579 expired and suspended
                                                            "<td></td>" +
                                                            "<td></td>" +
                                                            "<td></td>" +
                                                            "<td>" + theSqlDataReader["CreateDate"].ToString() + "</td>" +
                                                          "</tr>";
                            }
                        }

                        if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                        DLdb.SQLST.Parameters.RemoveAt(0);
                        DLdb.RS.Close();
                    }
                }

                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                DLdb.RS3.Close();

                DLdb.DB_Close();
            }
        }                
    }
}