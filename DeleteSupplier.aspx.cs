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
    public partial class DeleteSupplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (Request.QueryString["del"].ToString() == "undel")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update Suppliers set isActive = '1' WHERE SupplierID=@SupplierID";
                DLdb.SQLST.Parameters.AddWithValue("SupplierID", Request.QueryString["SupplierID"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }
            else
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update Suppliers set isActive = '0' WHERE SupplierID=@SupplierID";
                DLdb.SQLST.Parameters.AddWithValue("SupplierID", Request.QueryString["SupplierID"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }
            

            DLdb.DB_Close();
            Response.Redirect("ViewSupplier.aspx?msg=" + DLdb.Encrypt("Supplier has been deleted"));
        }
    }
}