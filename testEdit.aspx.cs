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
    public partial class testEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global DLdb = new Global();
                DLdb.DB_Connect();

                DLdb.RS.Open();
                if (Request.QueryString["ID"] != null)
                    Response.Write("select * from companyDetails where ID=" + Request.QueryString["ID"]);
                DLdb.SQLST.CommandText = "SELECT * FROM companyDetails where ID=@ID";
                DLdb.SQLST.Parameters.AddWithValue("ID", Request.QueryString["ID"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();


                while (theSqlDataReader.Read())
                {
                    // SET SESSION OBJECTS
                    companyName.Text = (theSqlDataReader["companyName"].ToString());
                    companyAddress.Text = (theSqlDataReader["companyAddress"].ToString());
                    companyContact.Text = (theSqlDataReader["companyContact"].ToString());
                    companyProduct.Text = (theSqlDataReader["companyProduct"].ToString());

                }


                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);

                DLdb.RS.Close();
                DLdb.DB_Close();
            }
        }

        protected void btn_dltt(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            

            DLdb.SQLST.CommandText = "DELETE FROM companyDetails where ID = @ID";
            DLdb.SQLST.Parameters.AddWithValue("ID", Request.QueryString["ID"]);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    // SET SESSION OBJECTS

                    displayData.InnerHtml += "<div class=\"row\">" +
                                            "     <div class=\"col-md-2\">" + theSqlDataReader["companyName"].ToString() + "</div>" +
                                            "     <div class=\"col-md-2\">" + theSqlDataReader["companyAddress"].ToString() + "</div>" +
                                            "     <div class=\"col-md-2\">" + theSqlDataReader["companyContact"].ToString() + "</div>" +
                                            "     <div class=\"col-md-2\">" + theSqlDataReader["companyProduct"].ToString() + "</div>" +
                                            " </div>";
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
            DLdb.RS.Close();
            DLdb.DB_Close();
            Response.Redirect("test.aspx");

        }

        protected void btn_updatee(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "UPDATE companyDetails SET companyName=@companyName, companyAddress=@companyAddress, companyContact=@companyContact, companyProduct=@companyProduct WHERE ID=@ID";
            DLdb.SQLST.Parameters.AddWithValue("ID", Request.QueryString["ID"]);
            DLdb.SQLST.Parameters.AddWithValue("companyName", companyName.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("companyAddress", companyAddress.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("companyContact", companyContact.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("companyProduct", companyProduct.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

           

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            //Response.Redirect("test.aspx");
        }
    }
}