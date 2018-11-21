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
    public partial class srv_logCOCDetails : System.Web.UI.Page
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
                

                string WorkCompletedDate = Request.QueryString["CompletionDate"].ToString();
                string TypeCOC = Request.QueryString["radio-choice-v-2"].ToString();
                string InstallationType = Request.QueryString["isCode"].ToString();
                string insComapny = Request.QueryString["insuranceCompany"].ToString();
                string policyHolder = Request.QueryString["policyHolder"].ToString();
                string policyNum = Request.QueryString["policyNumber"].ToString();
                string isBank = Request.QueryString["bankIns"].ToString();
                string InsFrom = Request.QueryString["InsFrom"].ToString();
                string InsTo = Request.QueryString["InsTo"].ToString();

                Global DLdb = new Global();
                DLdb.DB_Connect();


                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into COCStatementDetails (COCStatementID,CompletedDate,COCType,InsuranceCompany,PolicyHolder,PolicyNumber,isBank,PeriodOfInsuranceFrom,PeriodOfInsuranceTo) values (@COCStatementID,@CompletedDate,@COCType,@InsuranceCompany,@PolicyHolder,@PolicyNumber,@isBank,@PeriodOfInsuranceFrom,@PeriodOfInsuranceTo)"; //,DescriptionofWork,WorkCompleteby ,@DescriptionofWork,@WorkCompleteby
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", COCNumber);
                DLdb.SQLST.Parameters.AddWithValue("CompletedDate", WorkCompletedDate);
                DLdb.SQLST.Parameters.AddWithValue("COCType", TypeCOC);
                DLdb.SQLST.Parameters.AddWithValue("InsuranceCompany", insComapny);
                DLdb.SQLST.Parameters.AddWithValue("PolicyHolder", policyHolder);
                DLdb.SQLST.Parameters.AddWithValue("PolicyNumber", policyNum);
                DLdb.SQLST.Parameters.AddWithValue("isBank", isBank);
                DLdb.SQLST.Parameters.AddWithValue("PeriodOfInsuranceFrom", InsFrom);
                DLdb.SQLST.Parameters.AddWithValue("PeriodOfInsuranceTo", InsTo);
                //DLdb.SQLST.Parameters.AddWithValue("DescriptionofWork", DescriptionofWork.Text.ToString());
                //DLdb.SQLST.Parameters.AddWithValue("WorkCompleteby", WorkCompleteby);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
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