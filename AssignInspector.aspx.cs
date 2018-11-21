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
using System.IO;

namespace InspectIT
{
    public partial class AssignInspector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

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

            string city = "";
            string auditorArea = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID and isactive = '1'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from customers where CustomerID = @CustomerID";
                    DLdb.SQLST2.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            city = theSqlDataReader2["AddressCity"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from Area where Name = @Name";
                    DLdb.SQLST2.Parameters.AddWithValue("Name", city.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            auditorArea = theSqlDataReader2["ID"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS5.Open();
            DLdb.SQLST5.CommandText = "select * from AuditorAreas where AreaID = @AreaID";
            DLdb.SQLST5.Parameters.AddWithValue("AreaID", auditorArea.ToString());
            DLdb.SQLST5.CommandType = CommandType.Text;
            DLdb.SQLST5.Connection = DLdb.RS5;
            SqlDataReader theSqlDataReader5 = DLdb.SQLST5.ExecuteReader();

            if (theSqlDataReader5.HasRows)
            {
                while (theSqlDataReader5.Read())
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM Auditor where isactive = '1' and AuditorID=@AuditorID";
                    DLdb.SQLST.Parameters.AddWithValue("AuditorID", theSqlDataReader5["AuditorID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            string NoAudits = "0";

                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "select count(*) as Total from COCInspectors where status = 'Auditing' and isactive = '1' and UserID = @UserID";
                            DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                while (theSqlDataReader2.Read())
                                {
                                    if (theSqlDataReader2["total"] != DBNull.Value)
                                    {
                                        NoAudits = theSqlDataReader2["total"].ToString();
                                    }
                                }
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();


                            displayauditors.InnerHtml += "<tr>" +
                                                               "<td>" + theSqlDataReader["fName"].ToString() + " " + theSqlDataReader["lName"].ToString() + "</td>" +
                                                               "<td>" + theSqlDataReader["suburb"].ToString() + "</td>" +
                                                               "<td>" + theSqlDataReader["city"].ToString() + "</td>" +
                                                               "<td>" + theSqlDataReader["province"].ToString() + "</td>" +
                                                               "<td>" + NoAudits + "</td>" +
                                                               "<td><a href=\"AssignInspectorProcess.aspx?iid=" + DLdb.Encrypt(theSqlDataReader["UserID"].ToString()) + "&cocid=" + Request.QueryString["cocid"].ToString() + "\"><input type=\"button\" value=\"Assign\" class=\"btn btn-primary\"/></a></td>" +
                                                           "</tr>";
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }
            }

            if (theSqlDataReader5.IsClosed) theSqlDataReader5.Close();
            DLdb.SQLST5.Parameters.RemoveAt(0);
            DLdb.RS5.Close();

            

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select a.*, i.cocstatementid from COCInspectors i inner join Auditor a on a.userid = i.userid where COCStatementID = @COCStatementID and i.isactive = '1'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    AssignedInspector.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader["fName"].ToString() + " " + theSqlDataReader["lName"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["suburb"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["city"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["province"].ToString() + "</td>" +
                                                       "<td><div class=\"btn btn-sm btn-danger\" onclick=\"deleteconf('UnassignInspector.aspx?&cocid=" + theSqlDataReader["COCStatementID"].ToString() + "&iid=" + theSqlDataReader["UserID"].ToString() + "')\"><i class=\"fa fa-trash\"></i></div></td>" +
                                                   "</tr>";
                }
            }
            else
            {
                // Display any errors
                AssignedInspector.InnerHtml = "<tr><td colspan='8'>No Auditor Assigned</td><tr>";
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            

            DLdb.DB_Close();
        }
    }
}