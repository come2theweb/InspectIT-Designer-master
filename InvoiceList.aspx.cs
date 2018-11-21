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
    public partial class InvoiceList : System.Web.UI.Page
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
            DLdb.SQLST.CommandText = "SELECT * FROM COCInspectors where isActive='1' and isReconned = '0'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string UserName = "";

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
                            UserName = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    
                    string InspecInvoice = "";
                    if (theSqlDataReader["Report"] != DBNull.Value && theSqlDataReader["Report"].ToString() != "")
                    {
                        InspecInvoice = "<a href=\"Inspectorinvoices/" + theSqlDataReader["Report"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"View Invoice\"><i class=\"fa fa-download\"></i></div></a>";
                    }

                    string isReconned = "";
                    if (theSqlDataReader["isReconned"].ToString() == "True")
                    {
                        isReconned = "<button type=\"button\" class=\"btn btn-success\" onclick=\"InspecChangeIsReconned(" + theSqlDataReader["AuditID"].ToString() + ")\">Reconned </button> ";
                    }
                    else
                    {
                        isReconned = "<button type=\"button\" class=\"btn btn-danger\" onclick=\"InspecChangeIsNotReconned(" + theSqlDataReader["AuditID"].ToString() + ")\">Not Reconned </button> ";
                    }

                    displayusers.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["AuditID"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["COCStatementID"].ToString() + "</td>" +
                                                       "<td>" + UserName + "</td>" +
                                                       "<td>" + theSqlDataReader["Description"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["CompletedOn"].ToString() + "</td>" +
                                                       "<td>" + InspecInvoice + "</td>" +
                                                       "<td>" + theSqlDataReader["TotalAmount"].ToString() + "</td>" +
                                                       "<td><span class=\"label label-primary\">Inspector Invoice</span></td>" +
                                                       "<td>" + isReconned + "</td>" +
                                                   "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCInspectors where isActive='1' and isReconned = '1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string UserName = "";

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
                            UserName = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    
                    string InspecInvoice = "";
                    if (theSqlDataReader["Report"] != DBNull.Value && theSqlDataReader["Report"].ToString() != "")
                    {
                        InspecInvoice = "<a href=\"Inspectorinvoices/" + theSqlDataReader["Report"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"View Invoice\"><i class=\"fa fa-download\"></i></div></a>";
                    }

                    string isReconned = "";
                    if (theSqlDataReader["isReconned"].ToString() == "True")
                    {
                        isReconned = "<button type=\"button\" class=\"btn btn-success\" onclick=\"InspecChangeIsReconned(" + theSqlDataReader["AuditID"].ToString() + ")\">Reconned </button> ";
                    }
                    else
                    {
                        isReconned = "<button type=\"button\" class=\"btn btn-danger\" onclick=\"InspecChangeIsNotReconned(" + theSqlDataReader["AuditID"].ToString() + ")\">Not Reconned </button> ";
                    }

                    displayusers_del.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["AuditID"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["COCStatementID"].ToString() + "</td>" +
                                                       "<td>" + UserName + "</td>" +
                                                       "<td>" + theSqlDataReader["Description"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["CompletedOn"].ToString() + "</td>" +
                                                       "<td>" + InspecInvoice + "</td>" +
                                                       "<td>" + theSqlDataReader["TotalAmount"].ToString() + "</td>" +
                                                       "<td><span class=\"label label-primary\">Inspector Invoice</span></td>" +
                                                       "<td>" + isReconned + "</td>" +
                                                   "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Orders where isActive='1' and isReconned = '0'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string UsersName = "";
                    string roole = "";
                    string label = "";

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
                            UsersName = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                            roole = theSqlDataReader2["role"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    if (theSqlDataReader["Description"].ToString() == "COC Certificate Stock")
                    {
                        label = "<span class=\"label label-success\">Admin Created Stock</span>";
                    }
                    else
                    {
                    if (roole == "Supplier")
                    {

                        label = "<span class=\"label label-warning\">Supplier Invoice</span>";
                    }
                    else
                    {
                        label = "<span class=\"label label-info\">Plumber Invoice</span>";
                    }
                    }
                    string PlumberInvoice = "";
                    if (theSqlDataReader["PDFName"] != DBNull.Value && theSqlDataReader["PDFName"].ToString() != "")
                    {
                        PlumberInvoice = "<a href=\"invoices/" + theSqlDataReader["PDFName"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"View Invoice\"><i class=\"fa fa-download\"></i></div></a>";
                    }

                    string ispReconned = "";
                    if (theSqlDataReader["isReconned"].ToString() == "True")
                    {
                        ispReconned = "<button type=\"button\" class=\"btn btn-success\" onclick=\"PlumberChangeIsReconned(" + theSqlDataReader["OrderID"].ToString() + ")\">Reconned </button> ";
                    }
                    else
                    {
                        ispReconned = "<button type=\"button\" class=\"btn btn-danger\" onclick=\"PlumberChangeIsNotReconned(" + theSqlDataReader["OrderID"].ToString() + ")\">Not Reconned </button> ";
                    }

                    displayusers.InnerHtml += "<tr>" +
                                                    "<td>" + theSqlDataReader["OrderID"].ToString() + "</td>" +
                                                    "<td></td>" +
                                                    "<td>" + UsersName + "</td>" +
                                                    "<td>" + theSqlDataReader["COCType"].ToString() + "</td>" +
                                                    "<td></td>" +
                                                    "<td>" + PlumberInvoice + "</td>" +
                                                    "<td>" + theSqlDataReader["Total"].ToString() + "</td>" +
                                                    "<td>"+label+"</td>" +
                                                    "<td>" + ispReconned + "</td>" +
                                                "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Orders where isActive='1' and isReconned = '1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string UsersName = "";

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
                            UsersName = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string PlumberInvoice = "";
                    if (theSqlDataReader["PDFName"] != DBNull.Value)
                    {
                        PlumberInvoice = "<a href=\"invoices/" + theSqlDataReader["PDFName"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"View Invoice\"><i class=\"fa fa-download\"></i></div></a>";
                    }

                    string ispReconned = "";
                    if (theSqlDataReader["isReconned"].ToString() == "True")
                    {
                        ispReconned = "<button type=\"button\" class=\"btn btn-success\" onclick=\"PlumberChangeIsReconned(" + theSqlDataReader["OrderID"].ToString() + ")\">Reconned </button> ";
                    }
                    else
                    {
                        ispReconned = "<button type=\"button\" class=\"btn btn-danger\" onclick=\"PlumberChangeIsNotReconned(" + theSqlDataReader["OrderID"].ToString() + ")\">Not Reconned </button> ";
                    }

                    displayusers_del.InnerHtml += "<tr>" +
                                                    "<td>" + theSqlDataReader["OrderID"].ToString() + "</td>" +
                                                    "<td></td>" +
                                                    "<td>" + UsersName + "</td>" +
                                                    "<td>" + theSqlDataReader["COCType"].ToString() + "</td>" +
                                                    "<td></td>" +
                                                    "<td>" + PlumberInvoice + "</td>" +
                                                    "<td>" + theSqlDataReader["Total"].ToString() + "</td>" +
                                                    "<td><span class=\"label label-info\">Plumber Invoice</span></td>" +
                                                    "<td>" + ispReconned + "</td>" +
                                                "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();
        }
    }
}