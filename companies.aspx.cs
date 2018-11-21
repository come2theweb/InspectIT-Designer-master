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
using System.Web.Services;
using System.Web.Script.Services;

namespace InspectIT
{
    public partial class companies : System.Web.UI.Page
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
            
            displayusers.InnerHtml = "";
            displayusers_del.InnerHtml = "";
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT top 10 * FROM Companies where isActive='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string counted = "";
                    int LicensedCount = 0;
                    int NonLicensedCOunt = 0;
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select count(*) as counted from users where company=@company";
                    DLdb.SQLST2.Parameters.AddWithValue("company", theSqlDataReader["companyid"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                       theSqlDataReader2.Read();
                        counted = theSqlDataReader2["counted"].ToString();
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from users where company=@company";
                    DLdb.SQLST2.Parameters.AddWithValue("company", theSqlDataReader["companyid"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "select * from PlumberDesignations where PlumberID=@PlumberID";
                            DLdb.SQLST3.Parameters.AddWithValue("PlumberID", theSqlDataReader2["UserID"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {
                                    if (theSqlDataReader3["Designation"].ToString() == "Licensed Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Director Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Master Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Qualified Plumber")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Learner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Technical Operator Practitioner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Technical Assistant Practitioner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                }
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();


                    //displayusers.InnerHtml += "<tr>" +
                    //                                   "<td>" + theSqlDataReader["CompanyID"].ToString() + "</td>" +
                    //                                   "<td>" + theSqlDataReader["CompanyName"].ToString() + "</td>" +
                    //                                   "<td>" + LicensedCount + "</td>" +
                    //                                   "<td>" + NonLicensedCOunt + "</td>" +
                    //                                   "<td><a href='companiesEdit?id=" + DLdb.Encrypt(theSqlDataReader["CompanyID"].ToString()) + "'><div class=\"btn btn-sm btn-primary\" title=\"Edit\"><i class=\"fa fa-pencil\"></i></div></a>" +
                    //                                   "<div class=\"btn " + delBtnClass + " btn-sm btn-danger\" onclick=\"deleteconf('DeleteCompanies.aspx?op=del&id=" + theSqlDataReader["CompanyID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                    //                               "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT top 10 * FROM Companies where isActive='0'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string counted = "";
                    int LicensedCount = 0;
                    int NonLicensedCOunt = 0;
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select count(*) as counted from users where company=@company";
                    DLdb.SQLST2.Parameters.AddWithValue("company", theSqlDataReader["companyid"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        counted = theSqlDataReader2["counted"].ToString();
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from users where company=@company";
                    DLdb.SQLST2.Parameters.AddWithValue("company", theSqlDataReader["companyid"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "select * from PlumberDesignations where PlumberID=@PlumberID";
                            DLdb.SQLST3.Parameters.AddWithValue("PlumberID", theSqlDataReader2["UserID"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {
                                    if (theSqlDataReader3["Designation"].ToString() == "Licensed Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Director Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Master Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Qualified Plumber")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Learner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Technical Operator Practitioner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Technical Assistant Practitioner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                }
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    //displayusers_del.InnerHtml += "<tr>" +
                    //                                  "<td>" + theSqlDataReader["CompanyID"].ToString() + "</td>" +
                    //                                   "<td>" + theSqlDataReader["CompanyName"].ToString() + "</td>" +
                    //                                   "<td>" + LicensedCount + "</td>" +
                    //                                   "<td>" + NonLicensedCOunt + "</td>" +
                    //                                   "<td><a href='companiesEdit?id=" + DLdb.Encrypt(theSqlDataReader["CompanyID"].ToString()) + "'><div class=\"btn btn-sm btn-primary\" title=\"Edit\"><i class=\"fa fa-pencil\"></i></div></a>" +
                    //                                   "<div class=\"btn " + delBtnClass + " btn-sm btn-danger\" onclick=\"deleteconf('DeleteCompanies.aspx?op=undel&id=" + theSqlDataReader["CompanyID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                    //                               "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("companiesEdit");
        }
    }
}