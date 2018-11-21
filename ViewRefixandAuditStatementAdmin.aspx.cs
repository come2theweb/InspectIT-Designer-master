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
    public partial class ViewRefixandAuditStatementAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            // ADMIN CHECK
            //if (Session["IIT_Role"].ToString() != "Administrator")
            //{
            //    Response.Redirect("Default");
            //}
            
            COCStatement.InnerHtml = "";
            //COCStatementarchived.InnerHtml = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where (isRefix='1' and isActive = '1' and Status = 'Auditing') or (isRefix='0' and isActive = '1' and Status = 'Auditing')";//
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string customerName = "";
                    string customerAddress = "";
                    string AuditorName = "";
                    string ScDate = "";
                    if (theSqlDataReader["AssignedDate"] != DBNull.Value)
                    {
                        DateTime cDate = Convert.ToDateTime(theSqlDataReader["AssignedDate"].ToString());
                        ScDate = cDate.ToString("MM/dd/yyyy");
                    }

                    string SrefixDate = "";
                    string SrefixDatePlusFive = "";
                    if (theSqlDataReader["DateRefix"] != DBNull.Value)
                    {
                        DateTime refixDate = Convert.ToDateTime(theSqlDataReader["DateRefix"].ToString());
                        SrefixDate = refixDate.ToString("MM/dd/yyyy");
                        DateTime refixDatePlusFive = refixDate.AddDays(5);
                        SrefixDatePlusFive = refixDatePlusFive.ToString("MM/dd/yyyy");
                    }

                    string SauditedDate = "";
                    if (theSqlDataReader["DateAudited"] != DBNull.Value)
                    {
                        DateTime auditedDate = Convert.ToDateTime(theSqlDataReader["DateAudited"].ToString());
                        SauditedDate = auditedDate.ToString("MM/dd/yyyy");
                    }
                    
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Customers where CustomerID=@CustomerID";
                    DLdb.SQLST2.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            customerName = theSqlDataReader2["CustomerName"].ToString() + " " + theSqlDataReader2["CustomerSurname"].ToString();
                            customerAddress = theSqlDataReader2["AddressStreet"].ToString() + " " + theSqlDataReader2["AddressSuburb"].ToString() + " " + theSqlDataReader2["AddressCity"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Auditor where AuditorID=@AuditorID";
                    DLdb.SQLST2.Parameters.AddWithValue("AuditorID", theSqlDataReader["AuditorID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            AuditorName = theSqlDataReader2["fName"].ToString() + " " + theSqlDataReader2["lName"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    Boolean isComplete = false;
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM COCInspectors where COCStatementID=@COCStatementID";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            if (theSqlDataReader2["isComplete"].ToString() == "True")
                            {
                                isComplete = true;
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string installTypes = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from COCInstallations ci inner join InstallationTypes it on ci.TypeID=it.InstallationTypeID where COCStatementID=@COCStatementID";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            installTypes += theSqlDataReader2["InstallationType"].ToString() + "<br/>";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string status = "";
                    if (theSqlDataReader["Status"].ToString() == "Logged")
                    {
                        status = "<span class=\"label label-success\">Logged</span>";
                    }
                    else if (theSqlDataReader["Status"].ToString() == "Allocated")
                    {
                        status = "<span class=\"label label-warning\">Allocated</span>";
                    }
                    else if (theSqlDataReader["Status"].ToString() == "Non-Logged")
                    {
                        status = "<span class=\"label label-danger\">Non-Logged</span>";
                    }
                    else if (theSqlDataReader["Status"].ToString() == "Completed")
                    {
                        status = "<span class=\"label label-success\">Complete</span>";
                    }
                    else if (theSqlDataReader["Status"].ToString() == "Auditing")
                    {
                        status = "<span class=\"label label-default\">Auditing</span>";
                    }

                    string isCOCRefix = "false";
                    if (theSqlDataReader["isRefix"].ToString() == "True")
                    {
                        isCOCRefix = "true";
                    }


                    // GET THE REVIEW STATUS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from COCReviews where COCStatementID = @COCStatementID and isclosed = '0'";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            if (theSqlDataReader2["isFixed"].ToString() == "True" && isCOCRefix == "true")
                            {
                                status = "<div class=\"alert alert-warning\">Refix Complete</div>";
                            }
                            else if (theSqlDataReader2["isFixed"].ToString() == "False" && isCOCRefix == "true")
                            {
                                status = "<div class=\"alert alert-danger\">Refix Required</div>";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string archiveBtn = "";
                    if (isComplete == true)
                    {
                        archiveBtn = "<a href=\"DeleteItems.aspx?id=" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "&op=adminCocArch&pid=" + "\"><div class=\"btn btn-sm btn-danger\"><i class=\"fa fa-trash\"></i></div></a>";
                    }
                    
                    COCStatement.InnerHtml += "<tr>" +
                                                "<td>" + theSqlDataReader["COCNumber"].ToString() + "</td>" +
                                                "<td>" + status + "</td>" +
                                                "<td>" + installTypes + "</td>" +
                                                "<td>" + ScDate + "</td>" +
                                                "<td>" + SauditedDate + "</td>" +
                                                "<td>" + AuditorName + "</td>" +
                                                 "<td>" + SrefixDatePlusFive + "</td>" +
                                               // "<td>" + customerAddress + "</td>" +
                                                "<td width=\"100px\"><div class=\"btn-group\"><a href=\"EditCOCStatementAdmin.aspx?cocid=" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>"+ archiveBtn + "</div></td>" +
                                            "</tr>";

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            



            DLdb.DB_Close();
        }
    }
}