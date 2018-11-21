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
    public partial class ViewCertificate : System.Web.UI.Page
    {
        public int MaxNoCert = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                hideBtn.Visible = false;
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {
               
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
            
            displaySuppliers.InnerHtml = "";
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM CertificateStock";
            // DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    displaySuppliers.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["StockID"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["Amount"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["StartRange"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["EndRange"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["CreateDate"].ToString() + "</td>" +
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


            int NoStockCertificates = 0;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select count(*) as Total from COCStatements where isstock = '1' and userid = '0' and SupplierID = '0'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["Total"] != DBNull.Value && theSqlDataReader["Total"].ToString() != "0")
                    {
                        NoStockCertificates = Convert.ToInt32(theSqlDataReader["Total"]);
                        MaxNoCert = NoStockCertificates;
                        successmsg.InnerHtml = "There are " + MaxNoCert.ToString() + " available stock certificates to assign.";
                        successmsg.Visible = true;
                    }
                    else
                    {
                        NoStockCertificates = 0;
                        errormsg.InnerHtml = "There is no stock avaliable to supply paper certificates. please create stock first.";
                        errormsg.Visible = true;
                        //btn_add.Enabled = false;
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.DB_Close();
        }
    }
}