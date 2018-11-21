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
    public partial class ViewCOCStatementInspector : System.Web.UI.Page
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
            if (Session["IIT_Role"].ToString() != "Inspector")
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
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements c inner join COCInspectors i on c.COCStatementID = i.COCStatementID where c.isAudit = '1' and c.isactive = '1' and i.isComplete = '0' and i.isactive = '1' and i.userid = @userid";
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
                    string username = "";
                    string usercontact = "";

                    DateTime cDate = DateTime.Now;
                    if (theSqlDataReader["DateLogged"] != DBNull.Value && theSqlDataReader["DateRefix"].ToString() != "")
                    {
                        cDate = Convert.ToDateTime(theSqlDataReader["DateLogged"].ToString());
                    }
                    DateTime refixDate = Convert.ToDateTime("01/01/1900");
                    string dateRefixed = "";
                    if (theSqlDataReader["DateRefix"] != DBNull.Value && theSqlDataReader["DateRefix"].ToString() != "")
                    {
                        refixDate = Convert.ToDateTime(theSqlDataReader["DateRefix"]);
                        dateRefixed = refixDate.ToString("dd/MM/yyyy");
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

                    // GET CUSTOMER NAME AND ADDRESS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID=@UserID";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            username = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                            usercontact = theSqlDataReader2["contact"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    // GET INSTALLATION TYPES
                    string InstallTypes = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM COCInstallations C inner join installationTypes I on C.TypeID=I.InstallationTypeID where C.isActive = '1' and C.COCStatementID= @COCStatementID";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", theSqlDataReader["COCStatementID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            InstallTypes += theSqlDataReader2["InstallationType"].ToString() + "<br />";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DateTime comDate = DateTime.Now;
                    string status = "<div class=\"alert alert-info\">Auditing</div>";
                    if (theSqlDataReader["isComplete"].ToString() == "True")
                    {
                        if (theSqlDataReader["CompletedOn"] != DBNull.Value)
                        {
                            comDate = Convert.ToDateTime(theSqlDataReader["CompletedOn"].ToString());
                        }
                        status = "<div class=\"alert alert-success\">Complete</div>";
                    }
                    if (theSqlDataReader["isRefix"].ToString() == "True")
                    {
                        if (theSqlDataReader["CompletedOn"] != DBNull.Value)
                        {
                            comDate = Convert.ToDateTime(theSqlDataReader["CompletedOn"].ToString());
                        }
                        //status = "<div class=\"alert alert-danger\">Refix Required</div>";
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


                    string stat = "";
                    if (theSqlDataReader["isInspectorSubmitted"].ToString() == "True")
                    {
                        stat = "<div class=\"alert alert-warning\">Submitted</div>";
                    }
                    else
                    {
                        stat = "<div class=\"alert alert-warning\">Not Submitted</div>";
                    }

                    string COCPDF = "";
                    string COCNumber = "";
                    if (theSqlDataReader["COCFilename"] != DBNull.Value)
                    {
                        COCPDF = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"View COC\"><i class=\"fa fa-download\"></i></div></a>";
                        COCNumber = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\">" + theSqlDataReader["COCStatementID"].ToString() + "</a>";
                    }
                    else
                    {
                        COCNumber = theSqlDataReader["COCStatementID"].ToString();
                    }

                    COCStatement.InnerHtml += "<tr>" +
                                                "<td>" + COCNumber + "</td>" +
                                                "<td>" + status + stat + "</td>" +
                                                "<td>" + InstallTypes + "</td>" +
                                                "<td>" + username + "</td>" +
                                                "<td>" + usercontact + "</td>" +
                                                "<td>" + customerName + "</td>" +
                                                "<td width=\"250px\"><a href=\"https://www.google.co.za/maps/place/" + customerAddress + "\" target=\"_blank\">" + customerAddress + "</a></td>" +
                                                "<td>" + customerContact + "</td>" +
                                                "<td>" + dateRefixed + "</td>" +
                                                "<td width=\"100px\"><div class=\"btn-group\"><div class=\"btn btn-sm btn-primary\" onclick=\"document.location.href='EditCOCStatementInspector.aspx?cocid=" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "'\"><i class=\"fa fa-pencil\"></i></td>" +
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