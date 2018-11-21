using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InspectIT.API
{
    
    public partial class srv_AuditReport : System.Web.UI.Page
    {
        public class details
        {
            public string inspectionDate { get; set; }
            public string Quality { get; set; }
            public string AuditPictures { get; set; }
            public string latestComment { get; set; }
            public string descriptionofwork { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string json = "";
            string lastComm = "";
            var Page = new details();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCRefixesComments where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", Request.QueryString["cocid"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    lastComm = theSqlDataReader["Comments"].ToString();
                    Page.latestComment = lastComm;
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatementDetails where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", Request.QueryString["cocid"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    Page.descriptionofwork = theSqlDataReader["DescriptionOfWork"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCInspectors where COCStatementID = @COCStatementID and isComplete = '1'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    
                    Page.inspectionDate = theSqlDataReader["InspectionDate"].ToString();
                    Page.Quality = theSqlDataReader["Quality"].ToString();
                   

                    if (theSqlDataReader["Picture"] != DBNull.Value)
                    {
                        Page.AuditPictures = "<img src=\"https://197.242.82.242/noticeimages/" + theSqlDataReader["Picture"].ToString() + "\" class=\"img-responsive\" />";
                    }
                    else
                    {
                        Page.AuditPictures = "";
                    }

                    
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            json = JsonConvert.SerializeObject(Page);

            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();

            DLdb.DB_Close();
        }
    }
}