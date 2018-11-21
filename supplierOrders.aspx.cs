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
    public partial class supplierOrders : System.Web.UI.Page
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

            //// ADMIN CHECK
            //if (Session["IIT_Role"].ToString() != "Administrator")
            //{
            //    Response.Redirect("Default");
            //}

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
            
            UserOrdersDisplay.InnerHtml = "";
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Orders where UserID=@UserID and isactive = '1'";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string TotalCost = theSqlDataReader["Total"].ToString();
                    int TotalNoItems = Convert.ToInt32(theSqlDataReader["TotalNoItems"]);
                    string COCType = theSqlDataReader["COCType"].ToString();
                    string Method = theSqlDataReader["Method"].ToString();
                    
                    string isPaid = "";
                    if (theSqlDataReader["isPaid"].ToString() == "True")
                    {
                        isPaid = "<span class=\"label label-success\">Paid</span>";
                    }
                    else
                    {
                        isPaid = "<span class=\"label label-danger\">Not Paid</span>";
                    }

                    string PlumberInvoice = "";
                    if (theSqlDataReader["PDFName"] != DBNull.Value)
                    {
                        PlumberInvoice = "<a href=\"invoices/" + theSqlDataReader["PDFName"].ToString() + "\" target=\"_blank\"><div class=\"btn btn-sm btn-success\" title=\"View Invoice\"><i class=\"fa fa-download\"></i></div></a>";
                    }

                    UserOrdersDisplay.InnerHtml += "<tr>" +
                                                "<td>" + theSqlDataReader["OrderID"].ToString() + "</td>" +                               
                                                "<td>" + theSqlDataReader["Description"].ToString() + "</td>" +
                                                "<td>" + TotalNoItems + "</td>" +
                                                "<td>" + COCType + "</td>" +
                                                "<td>" + Method + "</td>" +
                                                "<td>" + isPaid + "</td>" +
                                                "<td>" + TotalCost + "</td>" +
                                                "<td width=\"100px\">"+ PlumberInvoice + "</td>" +
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