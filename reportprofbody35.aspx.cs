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
    public partial class reportprofbody35 : System.Web.UI.Page
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
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from Users where isActive = '1'";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        string equity = theSqlDataReader["equity"].ToString();
                        string nationality = theSqlDataReader["nationality"].ToString();
                        string language = theSqlDataReader["language"].ToString();
                        string gender = theSqlDataReader["gender"].ToString();
                        string CitizenResidentStatus = theSqlDataReader["CitizenResidentStatus"].ToString();
                        

                        string socioStatus = "";
                        if (theSqlDataReader["SocioeconomicStatus"].ToString() == "Employed")
                        {
                            socioStatus = "01";
                        }
                        else if (theSqlDataReader["SocioeconomicStatus"].ToString() == "Unemployed")
                        {
                            socioStatus = "02";
                        }
                        else
                        {
                            socioStatus = theSqlDataReader["SocioeconomicStatus"].ToString();
                        }

                        string disability = theSqlDataReader["disabilityStatus"].ToString();
                        
                        string province = theSqlDataReader["province"].ToString();

                        displayauditors.InnerHtml += "<tr>" +
                                                    "<td>" + theSqlDataReader["IdNo"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["Alternate"].ToString() + "</td>" +
                                                    //"<td>" + theSqlDataReader["AlternativeIDType"].ToString() + "</td>" +
                                                    "<td>" + equity + "</td>" +
                                                    "<td>" + nationality + "</td>" +
                                                    "<td>" + language + "</td>" +
                                                    "<td>" + gender + "</td>" +
                                                    "<td>" + CitizenResidentStatus + "</td>" +
                                                    "<td>" + socioStatus + "</td>" +
                                                    "<td>" + disability + "</td>" +
                                                    "<td>" + theSqlDataReader["lname"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["fname"].ToString() + "</td>" +
                                                    //"<td>" + theSqlDataReader["SecondName"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["Title"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["DateofBirth"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["ResidentialStreet"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["ResidentialSuburb"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["ResidentialCity"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["PostalAddress"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["PostalCity"].ToString() + "</td>" +
                                                    "<td></td>" +
                                                    "<td>" + theSqlDataReader["ResidentialCode"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["PostalCode"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["HomePhone"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["Contact"].ToString() + "</td>" +
                                                   // "<td>" + theSqlDataReader["Fax"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["Email"].ToString() + "</td>" +
                                                    "<td>" + province + "</td>" +
                                                    "<td></td>" +
                                                    "<td></td>" +
                                                    //"<td>" + theSqlDataReader["PreviousSurname"].ToString() + "</td>" +
                                                    //"<td>" + theSqlDataReader["PreviousAlternate"].ToString() + "</td>" +
                                                    //"<td>" + theSqlDataReader["PreviousAlternativeIDType"].ToString() + "</td>" +
                                                    "<td></td>" +
                                                    "<td></td>" +
                                                    "<td></td>" +
                                                    "<td></td>" +
                                                    "<td></td>" +
                                                    "<td></td>" +
                                                    "<td></td>" +
                                                    "<td></td>" +
                                                    "<td>" + theSqlDataReader["CreateDate"].ToString() + "</td>" +
                                                  "</tr>";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                DLdb.DB_Close();
            }
        }                
    }
}