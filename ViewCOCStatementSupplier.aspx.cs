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
    public partial class ViewCOCStatementSupplier : System.Web.UI.Page
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
            if (Session["IIT_Role"].ToString() != "Supplier")
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

            string supid = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Suppliers where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    supid = theSqlDataReader["SupplierID"].ToString();

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatements where supplierid=@supplierid";
            DLdb.SQLST.Parameters.AddWithValue("supplierid", supid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string username = "";
                    string regno = "";
                    DateTime cDate = Convert.ToDateTime(theSqlDataReader["DatePurchased"].ToString());
                    
                    // GET CUSTOMER NAME AND ADDRESS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID = @UserID";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            username = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                            regno = theSqlDataReader2["regno"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string desc = "";
                    if (theSqlDataReader["UserID"].ToString() == "0")
                    {
                        desc = "<label class=\"label label-primary\">In Stock</label>";
                    }
                    else
                    {
                        desc = "<label class=\"label label-success\">COC Allocated</label>";
                    }
                    
                    COCStatement.InnerHtml += "<tr>" +
                                                "<td>" + theSqlDataReader["OrderID"].ToString() + "</td>" +
                                                "<td>" + desc + "</td>" +
                                                "<td>" + theSqlDataReader["COCNumber"].ToString() + "</td>" +
                                                "<td>" + cDate.ToString("MM/dd/yyyy") + "</td>" +
                                                //"<td>" + theSqlDataReader["COCType"].ToString() + "</td>" +
                                                "<td>" + username + "</td>" +
                                                "<td>" + regno + "</td>" +
                                                //"<td width=\"100px\"></td>" +
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