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
    public partial class Weighting : System.Web.UI.Page
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

            //check performance types and add them to weighting if needed
            
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "SELECT * FROM performancetypes where isActive='1' and PerformanceID not in (select PerformanceID from Weighting where isActive='1' and PerformanceID is not null)";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    // INSERT
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "Insert into Weighting (PerformanceID,Section,Item,Qualified,Licensed,Master,Director) values (@PerformanceID,@Section,@Item,'0','0','0','0')";
                    DLdb.SQLST3.Parameters.AddWithValue("PerformanceID", theSqlDataReader2["PerformanceID"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("Section", "Performance Type");
                    DLdb.SQLST3.Parameters.AddWithValue("Item", theSqlDataReader2["Type"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();
                    
                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();
                    
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();



            displayrates.InnerHtml = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT section FROM Weighting where isActive='1' group by section";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    displayrates.InnerHtml += "<tr>" +
                                                "<td><b>" + theSqlDataReader["Section"].ToString() + "</b></td><td></td><td></td><td></td><td></td><td></td>" +
                                              "</tr>";

                    DLdb.RS4.Open();
                    DLdb.SQLST4.CommandText = "select * from Weighting where isactive = '1' and Section = @Section";
                    DLdb.SQLST4.Parameters.AddWithValue("Section", theSqlDataReader["Section"].ToString());
                    DLdb.SQLST4.CommandType = CommandType.Text;
                    DLdb.SQLST4.Connection = DLdb.RS4;
                    SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                    if (theSqlDataReader4.HasRows)
                    {
                        while (theSqlDataReader4.Read())
                        {
                            displayrates.InnerHtml += "<tr>" +
                                                       "<td>" + theSqlDataReader4["Item"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader4["Qualified"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader4["Licensed"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader4["Master"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader4["Director"].ToString() + "</td>" +
                                                       "<td><a href=\"WeightingEdit.aspx?id=" + DLdb.Encrypt(theSqlDataReader4["WeightingID"].ToString()) + "\"><div class=\"btn btn-sm btn-primary\"><i class=\"fa fa-pencil\"></i></div></a>" +
                                                   "</tr>";
                        }
                    }

                    if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                    DLdb.SQLST4.Parameters.RemoveAt(0);
                    DLdb.RS4.Close();
                    
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();
            
            DLdb.DB_Close();
        }
    }
}