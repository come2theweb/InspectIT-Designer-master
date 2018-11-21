using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using GoogleMaps.LocationServices;

namespace InspectIT.API
{
    public partial class srv_updateCOCDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["uid"] == null)
            {

            }
            else
            {
                string UID = Request.QueryString["uid"];
                string Results = "";

                string COCNumber = Request.QueryString["cocnumber"].ToString();
                string Description = Request.QueryString["noncomp"].ToString();
                string WorkCompletedBy = Request.QueryString["TickSel"].ToString();
                

                Global DLdb = new Global();
                DLdb.DB_Connect();

                string WorkCompleteby = "";
                if (WorkCompletedBy == "A")
                {
                    WorkCompleteby = "Me";
                }
                else if (WorkCompletedBy == "B")
                {
                    WorkCompleteby = "Supervised";
                }


                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCStatementDetails set DescriptionofWork=@DescriptionofWork,WorkCompleteby=@WorkCompleteby where  COCStatementID=@COCStatementID"; 
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", COCNumber);

                DLdb.SQLST.Parameters.AddWithValue("DescriptionofWork", Description);
                DLdb.SQLST.Parameters.AddWithValue("WorkCompleteby", WorkCompleteby);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Results = "Done";

                Response.Write(Results);
                Response.End();
                
            }
        }
    }
}