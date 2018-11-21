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
    public partial class DeleteSubCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "update CategoriesSub set isActive='0' WHERE SubCategoryID=@SubCategoryID";
            DLdb.SQLST.Parameters.AddWithValue("SubCategoryID", Request.QueryString["scid"]);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            DLdb.DB_Close();
            Response.Redirect("ViewAssessmentSubCategories.aspx?msg=" + DLdb.Encrypt("Category has been deleted") + "&cid=" + Request.QueryString["cid"].ToString());
        }
    }
}