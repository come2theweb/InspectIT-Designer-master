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
    public partial class ViewRefixandAuditStatement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            
            // CHECK SESSION
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
            
            DLdb.DB_Connect();
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where isRefix='1' and UserID=@UserID";
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
                    DateTime cDate = Convert.ToDateTime(theSqlDataReader["DatePurchased"].ToString());
                    DateTime rDate;
                    if (theSqlDataReader["DateRefix"].ToString() != "" && theSqlDataReader["DateRefix"] != DBNull.Value)
                    {
                        rDate = Convert.ToDateTime(theSqlDataReader["DateRefix"].ToString());
                    }
                    else
                    {
                        rDate = Convert.ToDateTime("1900-01-01");
                    }
                    //REQUIRED: LOAD CUSTOMER NAME AND ADDRESS
                    // GET CUSTOMER NAME AND ADDRESS
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
                    //REQUIRED: ADD COC TYPE AND STATUS COLUMNS

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
                            if (theSqlDataReader2["isFixed"].ToString() == "True")
                            {
                                status = "<div class=\"alert alert-warning\">Refix Complete</div>";
                            }
                            else
                            {
                                status = "<div class=\"alert alert-danger\">Refix Required</div>";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    COCStatement.InnerHtml += "<tr>" +
                                                "<td>" + theSqlDataReader["COCStatementID"].ToString() + "</td>" +
                                                "<td>" + status + "</td>" +
                                                "<td>" + customerName + "</td>" +
                                                "<td>" + customerAddress + "</td>" +
                                                "<td>" + rDate.ToString("dd/MM/yyyy") + "</td>" +
                                                "<td width=\"100px\"><div class=\"btn-group\"><a href=\"EditCOCStatement.aspx?cocid=" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a></div></td>" +
                                            "</tr>";
                }
            }
            else
            {
                // Display any errors
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            DLdb.DB_Close();
        }
    }
}