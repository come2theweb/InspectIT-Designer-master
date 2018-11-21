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
    public partial class DeleteAssessmentAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (Request.QueryString["op"].ToString() == "del")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update Assessments set isActive='0' WHERE AssessmentID=@AssessmentID";
                DLdb.SQLST.Parameters.AddWithValue("AssessmentID", Request.QueryString["aid"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                DLdb.DB_Close();
                Response.Redirect("EditOrDeleteUser.aspx?UserID="+ Request.QueryString["uid"].ToString() + "&msg=" + DLdb.Encrypt("Assessment has been deleted"));
            }
            else
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update Assessments set isActive='1' WHERE AssessmentID=@AssessmentID";
                DLdb.SQLST.Parameters.AddWithValue("AssessmentID", Request.QueryString["aid"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                DLdb.DB_Close();
                Response.Redirect("EditOrDeleteUser.aspx?UserID=" + Request.QueryString["uid"].ToString() + "&msg=" + DLdb.Encrypt("Assessment has been added"));
            }
            
        }
    }
}