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
    public partial class ViewCOCStatementisStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }
            
            if (Request.QueryString["msg"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
                successmsg.InnerHtml = msg;
                successmsg.Visible = true;
            }

            if (Request.QueryString["err"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
                errormsg.InnerHtml = msg;
                errormsg.Visible = true;
            }


            COCStatement.InnerHtml = "";
            
            DLdb.DB_Connect();
            DLdb.RS.Open();

            //DLdb.SQLST.CommandText = "SELECT * FROM COCStatements c inner join COCInspectors i on c.COCStatementID = i.COCStatementID where c.isactive = '1' and c.userid = @userid";
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where isStock='1'";
            //DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DateTime created = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                    COCStatement.InnerHtml += "<tr>" +
                                                "<td>" + theSqlDataReader["COCNumber"].ToString() + "</td>" +
                                                "<td>" + theSqlDataReader["Type"].ToString() + "</td>" +
                                                "<td>" + created.ToString("dd MMM yyyy") + "</td>" +
                                            "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            

            DLdb.DB_Close();
        }
    }
}