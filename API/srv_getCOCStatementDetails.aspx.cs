using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace InspectIT.API
{
    public partial class srv_getCOCStatementDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string getCocStatementInfo = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where COCStatementID=@COCStatementID and isactive = '1'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", Request.QueryString["cocid"].ToString());
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

                    string COCPDF = "";
                    if (theSqlDataReader["COCFilename"] != DBNull.Value)
                    {
                        COCPDF = "<a href=\"https://197.242.82.242/inspectit/pdf/" + theSqlDataReader["COCFilename"].ToString() + "\" target=\"_blank\"><button class=\"btn btn-success btn-sm\">COC Certificate</button></a>";
                    }

                    string status = "";
                    if (theSqlDataReader["Status"].ToString() == "Logged")
                    {
                        status = "<span class=\"label label-success\">Logged</span>";
                    }
                    else if (theSqlDataReader["Status"].ToString() == "Non-Logged")
                    {
                        status = "<span class=\"label label-warning\">Non-Logged</span>";
                    }

                    string cocType = "";
                    if (theSqlDataReader["type"].ToString() == "Electronic")
                    {
                        cocType = "<span class=\"label label-warning\">E</span>";
                    }
                    else if (theSqlDataReader["type"].ToString() == "Paper")
                    {
                        cocType = "<span class=\"label label-default\">P</span>";
                    }

                    getCocStatementInfo += "<div class=\"col-sm-10\"><b>C.O.C Number - " + theSqlDataReader["COCStatementID"].ToString() + "</b><br /><i class=\"fa fa-user\"></i> " + customerName + "</div><div class=\"col-sm-2 text-right\">" + cocType + "</div>" +
                                           " <div class=\"col-sm-12\"><i class=\"fa fa-clock-o\"></i> <small>" + cDate.ToString("dd/MM/yyyy") + "</small></div>" +
                                           " <div class=\"col-sm-6\">" +
                                           "  " + status +
                                           " </div>" +
                                           " <div class=\"col-sm-6\">" +
                                           "  <div class=\"btn-group-sm text-right\">" + 
                                                        COCPDF +
                                           " </div></div>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            DLdb.DB_Close();

            Response.Write(getCocStatementInfo);
            Response.End();
        }
    }
}