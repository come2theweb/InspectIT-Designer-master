using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoogleMaps.LocationServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace InspectIT.API
{
    public partial class srv_getForms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string getForms = "";
            string showbtns = "";
            // LOAD TYPE OF INSTALLATIONS
            string LinksHTML = "";
            decimal cnt = 0;
            decimal totcnt = 0;
            string btnNoticeShow = "";
            string COCNumber = Request.QueryString["ccid"].ToString();
            string a = "";
            string results = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCInstallations where COCStatementID = @COCStatementID and isActive = '1'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", COCNumber);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM InstallationTypes where isActive = '1' and InstallationTypeID=@InstallationTypeID and isActive='1'";
                    DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID",theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            if (theSqlDataReader["DataID"] == DBNull.Value)
                            {
                                //LinksHTML += "<label class=\"btn btn-tag btn-tag-light btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["ccid"] + "&did=" + theSqlDataReader["DataID"].ToString() + "'\">" + theSqlDataReader2["InstallationType"].ToString() + "</label><br />";

                                results = results + "<input type=\"checkbox\" onclick=\"changeURL('viewForm.html?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["ccid"] + "&did=" + theSqlDataReader["DataID"].ToString() + "')\" name=\"checkbox-mini-" + theSqlDataReader["TypeID"] + "\" id=\"checkbox-mini-" + theSqlDataReader["TypeID"] + "\" checked />";
                                results = results + "<label for=\"checkbox-mini-" + theSqlDataReader["TypeID"] + "\">" + theSqlDataReader2["InstallationType"].ToString() + "</label>";
                            }
                            else
                            {
                                cnt++;
                                //LinksHTML += "<label class=\"btn btn-tag btn-success btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["ccid"] + "'\">" + theSqlDataReader2["InstallationType"].ToString() + "</label><br />";

                                results = results + "<input type=\"checkbox\" onclick=\"changeURL('viewForm.html?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["ccid"] + "&did=" + theSqlDataReader["DataID"].ToString() + "')\" name=\"checkbox-mini-" + theSqlDataReader["TypeID"] + "\" id=\"checkbox-mini-" + theSqlDataReader["TypeID"] + "\" checked />";
                                results = results + "<label for=\"checkbox-mini-" + theSqlDataReader["TypeID"] + "\">" + theSqlDataReader2["InstallationType"].ToString() + "</label>";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    //a += "<a href=\"#\" class=\"btn btn-success\">" + theSqlDataReader["TypeID"].ToString() + "</a>";
                    //showbtns += "<label class=\"btn btn-tag btn-tag-light btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["ccid"] + "'\">" + theSqlDataReader["TypeID"].ToString() + "</label><br />";

                   
                
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            DLdb.DB_Close();

            Response.Write(results);
            Response.End();
        }
    }
}