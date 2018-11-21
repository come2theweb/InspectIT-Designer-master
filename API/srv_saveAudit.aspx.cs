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
    public partial class srv_saveAudit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string AuditorID = "";
            string AuditorIDUSerID = "";
            string NumberTo = "";
            string EmailAddress = "";
            string FullName = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "select * from Customers where CustomerID = @CustomerID";
                DLdb.SQLST2.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                if (theSqlDataReader2.HasRows)
                {
                    theSqlDataReader2.Read();
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "select * from Area where Name = @Name";
                    DLdb.SQLST3.Parameters.AddWithValue("Name", theSqlDataReader2["AddressSuburb"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();
                    if (theSqlDataReader3.HasRows)
                    {
                        theSqlDataReader3.Read();
                        DLdb.RS4.Open();
                        DLdb.SQLST4.CommandText = "select * from AuditorAreas where AreaID = @AreaID";
                        DLdb.SQLST4.Parameters.AddWithValue("AreaID", theSqlDataReader3["ID"].ToString());
                        DLdb.SQLST4.CommandType = CommandType.Text;
                        DLdb.SQLST4.Connection = DLdb.RS4;
                        SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();
                        if (theSqlDataReader4.HasRows)
                        {
                            theSqlDataReader4.Read();
                            AuditorID = theSqlDataReader4["AuditorID"].ToString();
                            DLdb.RS5.Open();
                            DLdb.SQLST5.CommandText = "select * from Auditor where AuditorID = @AuditorID";
                            DLdb.SQLST5.Parameters.AddWithValue("AuditorID", theSqlDataReader4["AuditorID"].ToString());
                            DLdb.SQLST5.CommandType = CommandType.Text;
                            DLdb.SQLST5.Connection = DLdb.RS5;
                            SqlDataReader theSqlDataReader5 = DLdb.SQLST5.ExecuteReader();
                            if (theSqlDataReader5.HasRows)
                            {
                                theSqlDataReader5.Read();
                                AuditorIDUSerID = theSqlDataReader5["UserID"].ToString();
                                NumberTo = theSqlDataReader5["phoneMobile"].ToString();
                                EmailAddress = theSqlDataReader5["email"].ToString();
                                FullName = theSqlDataReader5["fname"].ToString() + " " + theSqlDataReader5["lname"].ToString();
                            }
                            if (theSqlDataReader5.IsClosed) theSqlDataReader5.Close();
                            DLdb.SQLST5.Parameters.RemoveAt(0);
                            DLdb.RS5.Close();
                        }
                        if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                        DLdb.SQLST4.Parameters.RemoveAt(0);
                        DLdb.RS4.Close();
                    }
                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();
                }
                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // UPDATE THE COC
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatements Set Status = 'Allocated',AuditorID=@AuditorID,isAudit = '1' where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.Parameters.AddWithValue("AuditorID", AuditorID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCInspectors where COCStatementID=@COCStatementID";
            //DLdb.SQLST.Parameters.AddWithValue("UserID", AuditorIDUSerID);
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "update COCInspectors set UserID=@UserID,isActive='1' where COCStatementID=@COCStatementID";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", AuditorIDUSerID);
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            else
            {
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "insert into COCInspectors (UserID, COCStatementID) values (@UserID, @COCStatementID)";
                DLdb.SQLST2.Parameters.AddWithValue("UserID", AuditorIDUSerID);
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string WorkCompletedby = "";

            // UPDATE THE COC DETAILS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatementDetails Set DescriptionofWork = @DescriptionofWork, WorkCompleteby = @WorkCompletedby where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("DescriptionofWork", Request.QueryString["descwork"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("WorkCompletedby", WorkCompletedby);
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));

            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            if (NumberTo.ToString() != "")
            {
                DLdb.sendSMS(AuditorIDUSerID, NumberTo, "Inspect-It: You have been assigned to audit a C.O.C, please login to Inspect-It to view.");
            }

            // EMAIL
            if (EmailAddress.ToString() != "")
            {
                string eHTMLBody = "Dear " + FullName + "<br /><br />You have neen assigned to a C.O.C Statement on the Inspect-It system, please <a href='https://197.242.82.242/inspectit/'>login</a> to view.<br /><br />Regards<br />Inspect-It Administrator";
                string eSubject = "Inspect-IT New C.O.C Statement Audit";
                DLdb.sendEmail(eHTMLBody, eSubject, "mathewpayne@gmail.com", EmailAddress, "");
            }

            DLdb.DB_Close();
        }
    }
}