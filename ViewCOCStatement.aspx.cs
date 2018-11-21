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
    public partial class ViewCOCStatement : System.Web.UI.Page
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
            
            COCStatement.InnerHtml = "";

            DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "SELECT * FROM COCStatements c inner join COCInspectors i on c.COCStatementID = i.COCStatementID where c.isactive = '1' and c.userid = @userid";
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where isactive = '1' and userid = @userid";
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
                    string customerContact = "";
                    DateTime rDate;
                    DateTime cDate = Convert.ToDateTime(theSqlDataReader["DatePurchased"].ToString());
                    if (theSqlDataReader["DateRefix"].ToString() != "" && theSqlDataReader["DateRefix"] != DBNull.Value)
                    {
                        rDate = Convert.ToDateTime(theSqlDataReader["DateRefix"].ToString());
                    }
                    else
                    {
                        rDate = Convert.ToDateTime("1900-01-01");
                    }
                    

                    string createPdfFromOldSystem = "";
                    if (theSqlDataReader["COCFilename"] == DBNull.Value || theSqlDataReader["COCFilename"].ToString() == "")
                    {
                        if (theSqlDataReader["Type"].ToString() == "Electronic")
                        {
                            if (theSqlDataReader["Status"].ToString() == "Logged" || theSqlDataReader["Status"].ToString() == "Completed")
                            {
                                //createPdfFromOldSystem = "<label class=\"btn btn-success btn-sm\"><i class=\"fa fa-plus\"></i></label>";
                                createPdfFromOldSystem = "<a href=\"zCreateOlderPDF.aspx?cocid=" + theSqlDataReader["COCStatementID"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"Create COC\"><i class=\"fa fa-plus\"></i></div></a>";
                            }
                            else
                            {
                                createPdfFromOldSystem = "";
                            }
                        }
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
                            customerContact = theSqlDataReader2["CustomerCellNo"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string status = "<div class=\"alert alert-info\">" + theSqlDataReader["Status"].ToString() + "</div>";
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
                            DateTime comDate = DateTime.Now;
                            if (theSqlDataReader2["isComplete"].ToString() == "True")
                            {
                                if (theSqlDataReader2["CompletedOn"] != DBNull.Value)
                                {
                                    comDate = Convert.ToDateTime(theSqlDataReader2["CompletedOn"].ToString());
                                }
                                status = "<div class=\"alert alert-success\">Complete</div>";
                            }
                            if (theSqlDataReader["isRefix"].ToString() == "True")
                            {
                                if (theSqlDataReader2["CompletedOn"] != DBNull.Value)
                                {
                                    comDate = Convert.ToDateTime(theSqlDataReader2["CompletedOn"].ToString());
                                }
                                status = "<div class=\"alert alert-danger\">Refix Required</div>";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    
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
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string COCPDF = "";
                    string COCNumber = "";
                    if (theSqlDataReader["COCFilename"] != DBNull.Value && theSqlDataReader["COCFilename"].ToString() != "")
                    {
                        COCPDF = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\" download><div class=\"btn btn-sm btn-success\" title=\"View COC\"><i class=\"fa fa-download\"></i></div></a>";
                        COCNumber = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\" download style=\"color:red;\">" + theSqlDataReader["COCStatementID"].ToString() + "</a>";

                    }
                    else
                    {
                        COCNumber = "<a href=\"#\" style=\"color:red;\">" + theSqlDataReader["COCStatementID"].ToString() + "</a>";
                    }

                    

                    // GET INSTALLATION TYPES
                    string InstallTypes = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "exec getInstallationTypes @COCStatementID";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            InstallTypes += theSqlDataReader2["name"].ToString() + "<br />";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    
                    COCStatement.InnerHtml += "<tr>" +
                                                "<td>" + COCNumber + "</td>" +
                                                "<td>" + status + "</td>" +
                                                "<td>" + cDate.ToString("MM/dd/yyyy") + "</td>" +
                                                "<td>" + theSqlDataReader["Type"].ToString() + "</td>" +
                                                "<td>" + customerName + "</td>" +
                                                "<td>" + customerAddress + "</td>" +
                                                //"<td>" + rDate.ToString("MM/dd/yyyy") + "</td>" +
                                                "<td width=\"180px\"><div class=\"btn-group\">" + COCPDF + createPdfFromOldSystem + "<div class=\"btn btn-sm btn-primary\" onclick=\"document.location.href='EditCOCStatement.aspx?cocid=" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "'\"><i class=\"fa fa-pencil\"></i></div><div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('Archive.aspx?&cocid=" + theSqlDataReader["COCStatementID"].ToString() + "')\"><i class=\"fa fa-archive\"></i></div></div></td>" +
                                            "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // Show Archived
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where UserID=@UserID and isactive = '0'";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string COCPDF = "";
                    string COCNumber = "";
                    if (theSqlDataReader["COCFilename"] != DBNull.Value && theSqlDataReader["COCFilename"].ToString() != "")
                    {
                        COCPDF = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\" download><div class=\"btn btn-sm btn-success\" title=\"View COC\"><i class=\"fa fa-download\"></i></div></a>";
                        COCNumber = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\" download style=\"color:red;\">" + theSqlDataReader["COCStatementID"].ToString() + "</a>";

                    }
                    else
                    {
                        COCNumber = "<a href=\"#\" style=\"color:red;\">" + theSqlDataReader["COCStatementID"].ToString() + "</a>";
                    }
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

                    COCStatement_del.InnerHtml += "<tr>" +
                                                "<td>" + theSqlDataReader["COCStatementID"].ToString() + "</td>" +
                                                "<td>" + theSqlDataReader["Status"].ToString() + "</td>" +
                                                "<td>" + cDate.ToString("MM/dd/yyyy") + "</td>" +
                                                "<td>" + theSqlDataReader["Type"].ToString() + "</td>" +
                                                "<td>" + customerName + "</td>" +
                                                "<td>" + customerAddress + "</td>" +
                                                //"<td>" + rDate.ToString("MM/dd/yyyy") + "</td>" +
                                                "<td width=\"100px\"><div class=\"btn-group\"><div class=\"btn btn-sm btn-primary\" onclick=\"document.location.href='EditCOCStatement.aspx?cocid=" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "'\"><i class=\"fa fa-pencil\"></i></div>"+ COCPDF + "</td>" +
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