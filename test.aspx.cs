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
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string htmlcontent = "";
            string htmlHeader = "";
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "SELECT * FROM companyDetails";
            DLdb.SQLST.Parameters.AddWithValue("companyName", companyName.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                htmlHeader = "<table class=\"table table-hover\">" + "<tbody>" +
                                               "<thead>" + "<tr>" + "<th>Name</th>" + "<th>Address</th>" + "<th>Contact</th>" + "<th>Product</th>" + "</tr>" + "<thead>";
                while (theSqlDataReader.Read())
                {
                    // SET SESSION OBJECTS

                    htmlcontent +=
                                            "<tr>" +
                                            "     <td>" + theSqlDataReader["companyName"].ToString() + "</td>" +
                                            "     <td>" + theSqlDataReader["companyAddress"].ToString() + "</td>" +
                                            "     <td>" + theSqlDataReader["companyContact"].ToString() + "</td>" +
                                            "     <td>" + theSqlDataReader["companyProduct"].ToString() + "</td>" +
                                            "     <td><a href=\"testEdit.aspx\"><input type=\"button\" value=\"Edit\" class=\"btn btn-primary\"/></a></td>" +
                                            " </tr>";

                    //Label1.Text = theSqlDataReader["companyName"].ToString();
                    //Label2.Text = theSqlDataReader["companyAddress"].ToString();
                    //Label3.Text = theSqlDataReader["companyContact"].ToString();
                    //Label4.Text = theSqlDataReader["companyProduct"].ToString();
                }
            }
            else
            {
                // SHOW ERROR MESSAGE
                dispError.InnerHtml = "No Data";
                dispError.Visible = true;
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            DLdb.DB_Close();
            displayData.InnerHtml = htmlHeader + htmlcontent + "</tbody>" + "</table>"; 
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO companyDetails (companyName, companyAddress, companyContact, companyProduct) VALUES (@companyName, @companyAddress, @companyContact, @companyProduct)";
            
            DLdb.SQLST.Parameters.AddWithValue("companyName", companyName.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("companyAddress", companyAddress.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("companyContact", companyContact.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("companyProduct", companyProduct.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //if (theSqlDataReader.HasRows)
            //{
            //    theSqlDataReader.Read();
            //    // SET SESSION OBJECTS
            //    companyName.Text = theSqlDataReader["companyName"].ToString();
            //    companyAddress.Text = theSqlDataReader["companyAddress"].ToString();
            //    companyContact.Text = theSqlDataReader["companyContact"].ToString();
            //    companyProduct.Text = theSqlDataReader["companyProduct"].ToString();

            //}
            //else
            //{
            //    // SHOW ERROR MESSAGE
            //    dispError.InnerHtml = "No Data";
            //    dispError.Visible = true;
            //}

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("test.aspx");
            
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            // Moved the view button code into the page load
        }
    }
}