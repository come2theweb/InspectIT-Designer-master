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
    public partial class ViewCOCStatementAdmin : System.Web.UI.Page
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

            DateTime Now = DateTime.Now;
            DateTime startWeek = DateTime.Now.AddDays(-7);
            COCStatement.InnerHtml = "";
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where isactive = '1' and [status] = 'Logged' and DateLogged between '" + startWeek.ToString("yyyy-MM-dd") + " 00:00:01' and '" + Now.ToString("yyyy-MM-dd") + " 23:59:59' order by CreateDate desc";
            //DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string customerName = "";
                    string customerAddress = "";
                    string customerSuburb = "";
                    string customerCity = "";
                    string customerProvince = "";
                    string plumberName = "";
                    string plumberMobile = "";
                    int NumAuditedCOC = 0;
                    int NumLoggedCOC = 0;
                    int calculation = 0;

                    string scDate = "", spDate = "", sLDate = "";

                    DateTime cDate = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                    DateTime pDate = Convert.ToDateTime(theSqlDataReader["DatePurchased"].ToString());
                    
                    if (theSqlDataReader["DateLogged"] != DBNull.Value)
                    {
                        DateTime lDate = Convert.ToDateTime(theSqlDataReader["DateLogged"].ToString());
                        sLDate = lDate.ToString("MM/dd/yyyy");
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
                            customerSuburb = theSqlDataReader2["AddressSuburb"].ToString();
                            customerCity = theSqlDataReader2["AddressCity"].ToString();
                            customerProvince = theSqlDataReader2["Province"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

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
                            plumberName = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                            plumberMobile= theSqlDataReader2["contact"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select count(*) as NumLogged from COCStatements where [status]='Logged' and UserID=@UserID and isActive='1'";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            NumLoggedCOC = Convert.ToInt32(theSqlDataReader2["NumLogged"].ToString());
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select count(*) as NumAudited from COCStatements where isAudit='1' and UserID=@UserID and isActive='1'";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            NumAuditedCOC = Convert.ToInt32(theSqlDataReader2["NumAudited"].ToString());
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    if (NumLoggedCOC != 0 && NumAuditedCOC != 0)
                    {
                        calculation =  NumLoggedCOC/NumAuditedCOC  * 100;
                    }
                    else
                    {
                        calculation = 0;
                    }
                    

                    string COCPDF = "";
                    if (theSqlDataReader["COCFilename"] != DBNull.Value)
                    {
                        COCPDF = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"View COC\"><i class=\"fa fa-download\"></i></div></a>";
                    }

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
                    else if (theSqlDataReader["Status"].ToString() == "Complete")
                    {
                        status = "<span class=\"label label-success\">Complete</span>";
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
                            } else
                            {
                                status = "<div class=\"alert alert-danger\">Refix Required</div>";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string isAudit = "";
                    if (theSqlDataReader["isAudit"].ToString() == "True")
                    {
                        isAudit = "<span class=\"label label-info\">Audit</span>";
                    }
                    else
                    {
                        isAudit = "<span class=\"label label-default\">False</span>";
                    }
                    string isRefix = "";
                    if (theSqlDataReader["isRefix"].ToString() == "True")
                    {
                        isRefix = "<span class=\"label label-info\">Refix</span>";
                    }
                    else
                    {
                        isRefix = "<span class=\"label label-default\">False</span>";
                    }
                    string isPaid = "";
                    if (theSqlDataReader["isPaid"].ToString() == "True")
                    {
                        isPaid = "<span class=\"label label-info\">Paid</span>";
                    }
                    else
                    {
                        isPaid = "<span class=\"label label-default\">False</span>";
                    }
                    string isStock = "";
                    if (theSqlDataReader["isStock"].ToString() == "True")
                    {
                        isStock = "<span class=\"label label-info\">Stock</span>";
                    }
                    else
                    {
                        isStock = "<span class=\"label label-default\">False</span>";
                    }

                    //<th>COC Number</th>
                    //    <th>Date Purchase</th>
                    //    <th>Date Logged</th>
                    //    <th>Plumber</th>
                    //    <th>Plumber Mobile</th>
                    //    <th>City</th>
                    //    <th>%</th> <%-- the number coc's the plumbers has log divided by the number of audits that have been done on the plumber--%>
                    //    <th style="width:320px;"></th>

                    COCStatement.InnerHtml += "<tr>" +
                                                "<td>" + theSqlDataReader["COCNumber"].ToString() + "</td>" +
                                                "<td>" + pDate.ToString("MM/dd/yyyy") + "</td>" +
                                                "<td>" + sLDate + "</td>" +
                                                "<td>" + plumberName + "</td>" +
                                                "<td>" + plumberMobile + "</td>" +
                                                "<td>" + customerCity + "</td>" +
                                                "<td>" + calculation + "%</td>" +

                                                "<td style==\"width:320px;\"><div class=\"btn-group\"><div class=\"btn btn-sm btn-primary\" onclick=\"document.location.href='AssignInspector.aspx?cocid=" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "'\" title=\"Assign Inspector\"><i class=\"fa fa-user\"></i></div><div class=\"btn btn-sm btn-primary\" onclick=\"document.location.href='EditCOCStatementAdmin.aspx?cocid=" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "'\" title=\"Edit\"><i class=\"fa fa-pencil\"></i></div>" + COCPDF + createPdfFromOldSystem + "</div></td>" +
                                            //<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('Archive.aspx?cocid=" + theSqlDataReader["COCStatementID"].ToString() + "')\" title=\"Archive\"><i class=\"fa fa-trash\"></i></div>
                                            "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            

            DLdb.DB_Close();
        }

        protected void filtDates_Click(object sender, EventArgs e)
        {
            COCStatement.InnerHtml = "";
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DateTime start = Convert.ToDateTime(startDateSend.Text);
            DateTime end = Convert.ToDateTime(endDateSend.Text);

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where isactive = '1' and [status] = 'Logged' and DatePurchased between '" + start.ToString("yyyy-MM-dd") + " 00:00:01' and '" + end.ToString("yyyy-MM-dd") + " 23:59:59'";
            //DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string customerName = "";
                    string customerAddress = "";
                    string customerSuburb = "";
                    string customerCity = "";
                    string customerProvince = "";
                    string plumberName = "";
                    string plumberMobile = "";
                    int NumAuditedCOC = 0;
                    int NumLoggedCOC = 0;
                    int calculation = 0;

                    DateTime cDate = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                    DateTime pDate = Convert.ToDateTime(theSqlDataReader["DatePurchased"].ToString());
                    DateTime lDate = Convert.ToDateTime(theSqlDataReader["DateLogged"].ToString());

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
                            customerSuburb = theSqlDataReader2["AddressSuburb"].ToString();
                            customerCity = theSqlDataReader2["AddressCity"].ToString();
                            customerProvince = theSqlDataReader2["Province"].ToString();

                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    
                    string COCPDF = "";
                    if (theSqlDataReader["COCFilename"] != DBNull.Value)
                    {
                        COCPDF = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"View COC\"><i class=\"fa fa-download\"></i></div></a>";
                    }

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

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select count(*) as NumLogged from COCStatements where [status]='Logged' and UserID=@UserID and isActive='1'";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            NumLoggedCOC = Convert.ToInt32(theSqlDataReader2["NumLogged"].ToString());
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select count(*) as NumAudited from COCStatements where isAudit='1' and UserID=@UserID and isActive='1'";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            NumAuditedCOC = Convert.ToInt32(theSqlDataReader2["NumAudited"].ToString());
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

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
                            plumberName = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                            plumberMobile = theSqlDataReader2["contact"].ToString();

                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    if (NumLoggedCOC != 0 && NumAuditedCOC != 0)
                    {
                        calculation =  NumAuditedCOC/NumLoggedCOC  * 100;
                    }
                    else
                    {
                        calculation = 0;
                    }

                    string isAudit = "";
                    if (theSqlDataReader["isAudit"].ToString() == "True")
                    {
                        isAudit = "<span class=\"label label-info\">Audit</span>";
                    }
                    else
                    {
                        isAudit = "<span class=\"label label-default\">False</span>";
                    }
                    string isRefix = "";
                    if (theSqlDataReader["isRefix"].ToString() == "True")
                    {
                        isRefix = "<span class=\"label label-info\">Refix</span>";
                    }
                    else
                    {
                        isRefix = "<span class=\"label label-default\">False</span>";
                    }
                    string isPaid = "";
                    if (theSqlDataReader["isPaid"].ToString() == "True")
                    {
                        isPaid = "<span class=\"label label-info\">Paid</span>";
                    }
                    else
                    {
                        isPaid = "<span class=\"label label-default\">False</span>";
                    }
                    string isStock = "";
                    if (theSqlDataReader["isStock"].ToString() == "True")
                    {
                        isStock = "<span class=\"label label-info\">Stock</span>";
                    }
                    else
                    {
                        isStock = "<span class=\"label label-default\">False</span>";
                    }

                    COCStatement.InnerHtml += "<tr>" +
                                                "<td>" + theSqlDataReader["COCNumber"].ToString() + "</td>" +
                                                "<td>" + pDate.ToString("MM/dd/yyyy") + "</td>" +
                                                "<td>" + lDate.ToString("MM/dd/yyyy") + "</td>" +
                                                "<td>" + plumberName + "</td>" +
                                                "<td>" + plumberMobile + "</td>" +
                                                "<td>" + customerCity + "</td>" +
                                                "<td>" + calculation + "</td>" +
                                                "<td style==\"width:320px;\"><div class=\"btn-group\"><div class=\"btn btn-sm btn-primary\" onclick=\"document.location.href='AssignInspector.aspx?cocid=" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "'\" title=\"Assign Inspector\"><i class=\"fa fa-user\"></i></div><div class=\"btn btn-sm btn-primary\" onclick=\"document.location.href='EditCOCStatementAdmin.aspx?cocid=" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "'\" title=\"Edit\"><i class=\"fa fa-pencil\"></i></div>" + COCPDF + createPdfFromOldSystem + "<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('Archive.aspx?cocid=" + theSqlDataReader["COCStatementID"].ToString() + "')\" title=\"Archive\"><i class=\"fa fa-trash\"></i></div></div></td>" +
                                            "</tr>";
                  
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            DLdb.DB_Close();
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            COCStatement.InnerHtml = "";

            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where COCNumber ='"+ COCNumber.Text.ToString()+ "' and Status='Logged'";
            //DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
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
                    
                    string customerName = "";
                    string customerAddress = "";
                    string customerSuburb = "";
                    string customerCity = "";
                    string customerProvince = "";
                    string plumberName = "";
                    string plumberMobile = "";
                    int NumAuditedCOC = 0;
                    int NumLoggedCOC = 0;
                    int calculation = 0;
                    DateTime cDate = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                    DateTime pDate = Convert.ToDateTime(theSqlDataReader["DatePurchased"].ToString());
                    DateTime lDate = Convert.ToDateTime(theSqlDataReader["DateLogged"].ToString());

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
                            customerSuburb = theSqlDataReader2["AddressSuburb"].ToString();
                            customerCity = theSqlDataReader2["AddressCity"].ToString();
                            customerProvince = theSqlDataReader2["Province"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select count(*) as NumLogged from COCStatements where [status]='Logged' and UserID=@UserID and isActive='1'";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            NumLoggedCOC = Convert.ToInt32(theSqlDataReader2["NumLogged"].ToString());
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select count(*) as NumAudited from COCStatements where isAudit='1' and UserID=@UserID and isActive='1'";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            NumAuditedCOC = Convert.ToInt32(theSqlDataReader2["NumAudited"].ToString());
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

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
                            plumberName = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();

                            plumberMobile = theSqlDataReader2["contact"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    if (NumLoggedCOC != 0 && NumAuditedCOC != 0)
                    {
                        calculation = NumLoggedCOC / NumAuditedCOC * 100;
                    }
                    else
                    {
                        calculation = 0;
                    }

                    string COCPDF = "";
                    if (theSqlDataReader["COCFilename"] != DBNull.Value)
                    {
                        COCPDF = "<a href=\"pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"View COC\"><i class=\"fa fa-download\"></i></div></a>";
                    }

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

                    string isAudit = "";
                    if (theSqlDataReader["isAudit"].ToString() == "True")
                    {
                        isAudit = "<span class=\"label label-info\">Audit</span>";
                    }
                    else
                    {
                        isAudit = "<span class=\"label label-default\">False</span>";
                    }
                    string isRefix = "";
                    if (theSqlDataReader["isRefix"].ToString() == "True")
                    {
                        isRefix = "<span class=\"label label-info\">Refix</span>";
                    }
                    else
                    {
                        isRefix = "<span class=\"label label-default\">False</span>";
                    }
                    string isPaid = "";
                    if (theSqlDataReader["isPaid"].ToString() == "True")
                    {
                        isPaid = "<span class=\"label label-info\">Paid</span>";
                    }
                    else
                    {
                        isPaid = "<span class=\"label label-default\">False</span>";
                    }
                    string isStock = "";
                    if (theSqlDataReader["isStock"].ToString() == "True")
                    {
                        isStock = "<span class=\"label label-info\">Stock</span>";
                    }
                    else
                    {
                        isStock = "<span class=\"label label-default\">False</span>";
                    }

                    //<th>COC Number</th>
                    //<th>Status</th>
                    //<th>Date Purchase</th>
                    //<th>Date Logged</th>
                    //<th>Plumber</th>
                    //<th>Suburb</th>
                    //<th>City</th>
                    //<th>Progive</th>
                    //<th>Issued</th>
                    //<th>Logged</th>
                    //<th>Audit</th>
                    //<th>%</th>                       
                    //<th>UnRead Notice</th>
                    //<th></th>

                    COCStatement.InnerHtml += "<tr>" +
                                                "<td>" + theSqlDataReader["COCNumber"].ToString() + "</td>" +
                                                "<td>" + pDate.ToString("MM/dd/yyyy") + "</td>" +
                                                "<td>" + lDate.ToString("MM/dd/yyyy") + "</td>" +
                                                "<td>" + plumberName + "</td>" +
                                                "<td>" + plumberMobile + "</td>" +
                                                "<td>" + customerCity + "</td>" +
                                                "<td>" + calculation + "%</td>" +
                                                "<td style==\"width:320px;\"><div class=\"btn-group\"><div class=\"btn btn-sm btn-primary\" onclick=\"document.location.href='AssignInspector.aspx?cocid=" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "'\" title=\"Assign Inspector\"><i class=\"fa fa-user\"></i></div><div class=\"btn btn-sm btn-primary\" onclick=\"document.location.href='EditCOCStatementAdmin.aspx?cocid=" + DLdb.Encrypt(theSqlDataReader["COCStatementID"].ToString()) + "'\" title=\"Edit\"><i class=\"fa fa-pencil\"></i></div>" + COCPDF + createPdfFromOldSystem + "</div></td>" +
                                            //<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('Archive.aspx?cocid=" + theSqlDataReader["COCStatementID"].ToString() + "')\" title=\"Archive\"><i class=\"fa fa-trash\"></i></div>
                                            "</tr>";

                   
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
        }
    }
}