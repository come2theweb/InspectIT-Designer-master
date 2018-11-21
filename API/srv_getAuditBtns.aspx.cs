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
    public partial class srv_getAuditBtns : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            Boolean isLogged = false;
            decimal cnt = 0;
            decimal totcnt = 0;
            Boolean btnSave = false;
            Boolean btnSubmit = false;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCInstallations where COCStatementID = @COCStatementID and isActive = '1'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM InstallationTypes where isActive = '1' and InstallationTypeID=@InstallationTypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            if (theSqlDataReader["DataID"] == DBNull.Value)
                            {
                               // LinksHTML += "<label class=\"btn btn-tag btn-tag-light btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["cocid"] + "&did=" + theSqlDataReader["DataID"].ToString() + "'\">" + li.Text.ToString() + "</label><br />";
                            }
                            else
                            {
                                cnt++;
                                //LinksHTML += "<label class=\"btn btn-tag btn-success btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["cocid"] + "'\">" + li.Text.ToString() + "</label><br />";
                            }
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    //foreach (ListItem li in TypeOfInstallation.Items)
                    //{
                    //    if (li.Value.ToString() == theSqlDataReader["TypeID"].ToString())
                    //    {
                    //        li.Selected = true;
                            

                    //        if (theSqlDataReader["DataID"] == DBNull.Value)
                    //        {
                    //            LinksHTML += "<label class=\"btn btn-tag btn-tag-light btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["cocid"] + "&did=" + theSqlDataReader["DataID"].ToString() + "'\">" + li.Text.ToString() + "</label><br />";
                    //        }
                    //        else
                    //        {
                    //            cnt++;
                    //            LinksHTML += "<label class=\"btn btn-tag btn-success btn-tag-rounded m-r-20\" onclick=\"document.location.href='ViewForm.aspx?typ=COC&tid=" + DLdb.Encrypt(theSqlDataReader["TypeID"].ToString()) + "&cocid=" + Request.QueryString["cocid"] + "'\">" + li.Text.ToString() + "</label><br />";
                    //        }

                    //    }
                    //}
                    totcnt++;
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCStatements where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"]));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    // CHECK IF LOGGED
                    if (theSqlDataReader["DateLogged"] != DBNull.Value && theSqlDataReader["status"].ToString() == "Logged")
                    {
                        btnSubmit = false;
                        isLogged = true;
                    }

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            if (totcnt == cnt && cnt != 0)
            {
                if (isLogged == false)
                {
                    btnSave = true;
                    btnSubmit = true;
                }
              //  progressBarStatus.InnerHtml = "<div class=\"progress-bar progress-bar-primary\" data-percentage=\"100%\"></div>";

            }
            else
            {
                if (cnt != 0)
                {
                    decimal gPercent = (cnt / totcnt) * 100m;
                    if (isLogged == false)
                    {
                        btnSave = false;
                        btnSubmit = false;
                    }
                    //progressBarStatus.InnerHtml = "<div class=\"progress-bar progress-bar-primary\" data-percentage=\"" + gPercent.ToString().Replace(",0", "") + "%\"></div>";
                }
                else
                {
                    if (isLogged == false)
                    {
                        btnSave = false;
                        btnSubmit = false;
                    }
                    //progressBarStatus.InnerHtml = "<div class=\"progress-bar progress-bar-primary\" data-percentage=\"0%\"></div>";
                }

            }

            DLdb.DB_Close();
            string saveSubBtnsDisp = "";
            if (btnSave == true && btnSubmit == true)
            {
                saveSubBtnsDisp = "<div class=\"row\">" +
                                    "<div class=\"col-md-12\" style=\"text-align:center;margin-top:10px;\">" +
                                        "<input type=\"button\" id=\"btnSave\" class=\"btn btn-primary\" Value=\"Save Audit\" onclick=\"saveAudit("+ Request.QueryString["cocid"].ToString()+ ")\" />" +
                                        "<input type=\"button\" id=\"btnSave\" class=\"btn btn-primary\" Value=\"Submit Audit\" onclick=\"submitAudit(" + Request.QueryString["cocid"].ToString() + ")\" style=\"margin-right:10px;\"/>" +
                                    "</div>" +
                                  "</div>";
            }
            else
            {
                saveSubBtnsDisp = "<div class=\"row\">" +
                                    "<div class=\"col-md-12\" style=\"text-align:center;margin-top:10px;\">" +
                                        "<input type=\"button\" id=\"btnSave\" class=\"btn btn-primary\" Value=\"Save Audit\" disabled style=\"margin-right:10px;\" />" +
                                        "<input type=\"button\" id=\"btnSave\" class=\"btn btn-primary\" Value=\"Submit Audit\" disabled />" +
                                    "</div>" +
                                  "</div>";
            }

            //Response.Write(totcnt + "," + cnt);
            Response.Write(saveSubBtnsDisp);
            Response.End();
        }
    }
}