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
    public partial class ViewCertificates : System.Web.UI.Page
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

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();
            displaySuppliers.InnerHtml = "";

            //Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "SELECT * FROM Suppliers where isactive = '1'";
            // DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    //    <th>Avaliable Balance</th>
                    //    <th>Transaction Type</th>
                    //    <th>Amount</th>
                    //    <th>Start Range</th>
                    //    <th>End Range</th>
                    //    <th>Date</th>

                    displaySuppliers.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["SupplierName"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["StartRange"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["EndRange"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["InvoiceNumber"].ToString() + "</td>" +
                                                       "<td><a href=\"EditOrDeleteCertificate.aspx?SupplierID=" + theSqlDataReader["SupplierID"].ToString() + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                      // "<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeleteSupplier.aspx?SupplierID=" + theSqlDataReader["SupplierID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                   "</tr>";
                }
            }
            else
            {
                // Display any errors
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "SELECT * FROM Suppliers where isactive = '0'";
            // DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
             theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    displayarchivedsupp.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["SupplierName"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["StartRange"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["EndRange"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["InvoiceNumber"].ToString() + "</td>" +
                                                       "<td><a href=\"EditOrDeleteCertificate.aspx?SupplierID=" + theSqlDataReader["SupplierID"].ToString() + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                   // "<div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('DeleteSupplier.aspx?SupplierID=" + theSqlDataReader["SupplierID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                   "</tr>";
                }
            }
            else
            {
                // Display any errors
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();


            DLdb.DB_Close();
        }
    }
}