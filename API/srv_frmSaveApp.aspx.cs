using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace InspectIT.srvAPI
{
    public partial class srv_frmSaveApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // SET THE SCORES FROM THE QUESTIONS FORM.
            if (Request.QueryString["json"] != null && Request.QueryString["cocid"] != null)
            {
                Global DLdb = new Global();
                DLdb.DB_Connect();

                string json = Request.QueryString["json"].ToString();
                string COCStatementID = DLdb.Decrypt(Request.QueryString["cocid"].ToString());
                string TypeID = DLdb.Decrypt(Request.QueryString["typeid"].ToString());
                string DataID = "";
                string saveddata = "False";

                if (Request.QueryString["did"] != "0")
                {
                    DataID = Request.QueryString["did"].ToString();
                    // UPDATE IT
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update FormUserData set json = @json where DataID = @DataID";
                    DLdb.SQLST.Parameters.AddWithValue("json", json.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("DataID", DataID);
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    saveddata = "True";
                }
                else
                {
                    //SAVE IT
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "insert into FormUserData (json) values (@json); Select Scope_Identity() as DataID";
                    DLdb.SQLST.Parameters.AddWithValue("json", json.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        theSqlDataReader.Read();

                        DataID = theSqlDataReader["DataID"].ToString();

                        // ADD THE NEW DATAID
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "update COCInstallations set DataID = @DataID where COCStatementID = @COCStatementID and TypeID = @TypeID";
                        DLdb.SQLST2.Parameters.AddWithValue("DataID", DataID);
                        DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", COCStatementID);
                        DLdb.SQLST2.Parameters.AddWithValue("TypeID", TypeID);
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                        
                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    saveddata = "True";
                }
                
                
                DLdb.DB_Close();

                Response.Write(saveddata);
                Response.End();

            }
            else
            {
                Response.Write("Error");
                Response.End();
            }
            
        }

        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table border='0' cellpadding='5px' cellspacing='0px'><tr><td><table border='0' cellpadding='5px' cellspacing='0px'>";
            //add header row
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                html += "<tr>";
                html += "<td nowrap><b>" + dt.Columns[i].ColumnName + "</b></td>";
                html += "</tr>";
            }
            html += "</table></td>";
            html += "<td><table border='0' cellpadding='5px' cellspacing='0px'>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    html += "<tr>";
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                    html += "</tr>";
                }
            }
            html += "</table></td></tr></table>";
            return html;
        }   
    }
}