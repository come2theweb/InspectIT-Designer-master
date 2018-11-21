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
using System.Security.Cryptography;

namespace InspectIT
{
    public partial class DismissReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }
                        
            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Update COCReviews set isFixed = @isFixed where ReviewID = @ReviewID";
            DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(Request.QueryString["rid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("isFixed", "1");

            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            DLdb.DB_Close();

            Response.Redirect("EditCOCStatement?cocid=" + Request.QueryString["cocid"] + "&msg=" + DLdb.Encrypt("Review dismissed successfully"));


        }


    }
}