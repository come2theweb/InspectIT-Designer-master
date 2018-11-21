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

namespace InspectIT.API
{
    public partial class PlumberReconned : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "SELECT * FROM Orders where OrderID=@OrderID";
            DLdb.SQLST2.Parameters.AddWithValue("OrderID", Request.QueryString["oid"].ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                theSqlDataReader2.Read();

                if (theSqlDataReader2["isReconned"].ToString() == "True")
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update Orders set isReconned='False' where OrderID=@OrderID";
                    DLdb.SQLST.Parameters.AddWithValue("OrderID", theSqlDataReader2["OrderID"].ToString());
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
                    DLdb.SQLST.CommandText = "update Orders set isReconned='True' where OrderID=@OrderID";
                    DLdb.SQLST.Parameters.AddWithValue("OrderID", theSqlDataReader2["OrderID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            DLdb.DB_Close();

            Response.Redirect("../InvoiceList.aspx");
        }
    }
}