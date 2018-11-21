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
    public partial class EditOrDeleteCustomer : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                DLdb.DB_Connect();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Customers where CustomerID=@CustomerID";
                DLdb.SQLST.Parameters.AddWithValue("CustomerID", Request.QueryString["CustomerID"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();


                while (theSqlDataReader.Read())
                {
                    // General Details
                    CustomerName.Text = theSqlDataReader["CustomerName"].ToString();
                    CustomerSurname.Text = theSqlDataReader["CustomerSurname"].ToString();
                    CustomerCellNo.Text = theSqlDataReader["CustomerCellNo"].ToString();
                    CustomerEmail.Text = theSqlDataReader["CustomerEmail"].ToString();
                    CustomerPassword.Text = theSqlDataReader["CustomerPassword"].ToString();
                }


                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);

                DLdb.RS.Close();
                DLdb.DB_Close();
            }
        }
        
        protected void btn_update_Click1(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "UPDATE Customers SET CustomerName=@CustomerName, CustomerSurname=@CustomerSurname, CustomerCellNo=@CustomerCellNo, CustomerEmail=@CustomerEmail, CustomerPassword=@CustomerPassword WHERE CustomerID=@CustomerID";
            DLdb.SQLST.Parameters.AddWithValue("CustomerID", Request.QueryString["CustomerID"]);
            DLdb.SQLST.Parameters.AddWithValue("CustomerName", CustomerName.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CustomerSurname", CustomerSurname.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CustomerCellNo", CustomerCellNo.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CustomerEmail", CustomerEmail.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("CustomerPassword", CustomerPassword.Text.ToString());
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
            Response.Redirect("ViewCustomer.aspx?msg=" + DLdb.Encrypt("Customer updated successfuly"));
        }
    }
}