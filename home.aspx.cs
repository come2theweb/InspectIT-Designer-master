using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace InspectIT
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            // ADMIN CHECK
            if (Session["IIT_Role"].ToString() != "Administrator")
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

            //    //Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements";
            // DLdb.SQLST.Parameters.AddWithValue("field1", TextBox1.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DateTime cDate = Convert.ToDateTime(theSqlDataReader["DatePurchased"].ToString());

                    COCStatement.InnerHtml += "<tr>" +
                                                "<td>" + theSqlDataReader["COCStatementID"].ToString() + "</td>" +
                                                "<td>" + theSqlDataReader["Type"].ToString() + "</td>" +
                                                "<td>" + cDate.ToString("MM/dd/yyyy") + "</td>" +
                                                "<td>" + theSqlDataReader["Status"].ToString() + "</td>" +
                                                "<td>" + theSqlDataReader["Consumer"].ToString() + "</td>" +
                                                "<td>" + theSqlDataReader["Address"].ToString() + "</td>" +
                                                "<td width=\"100px\"><div class=\"btn-group\"><a href=\"EditCOCStatement.aspx?cocid=" + theSqlDataReader["COCStatementID"].ToString() + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a><a href=\"Archive.aspx?t=coc&id=" + theSqlDataReader["COCStatementID"].ToString() + "\"><div class=\"btn btn-sm btn-danger\"><i class=\"fa fa-trash\"></i></div></a></div></td>" +
                                            "</tr>";
                }
            }
            else
            {
                // Display any errors
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            DLdb.DB_Close();
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("logout.aspx");
        }
    }
}