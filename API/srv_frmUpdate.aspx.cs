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
    public partial class srv_frmUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // SET THE SCORES FROM THE QUESTIONS FORM.
            if (Request.QueryString["did"] != null && Request.QueryString["json"] != null)
            {
                string DataID = Request.QueryString["did"].ToString();
                string json = Request.QueryString["json"].ToString();

                Global DLdb = new Global();
                DLdb.DB_Connect();

                // CHECK IF EXISTS 

                //SAVE IT
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update FormUserData set json = @json where DataID = @DataID";
                DLdb.SQLST.Parameters.AddWithValue("DataID", DataID.ToString());
                DLdb.SQLST.Parameters.AddWithValue("json", json.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.DB_Close();

                string saveddata = "True";

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