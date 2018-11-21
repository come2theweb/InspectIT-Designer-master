using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;

namespace InspectIT.srvAPI
{
    public partial class srv_getSiteForms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string result = "";
            // SET THE SCORES FROM THE QUESTIONS FORM
            if (Request.Form["key"] != null && Request.Form["sid"] != null && Request.Form["uid"] != null)
            {
                Global DLdb = new Global();
                DLdb.DB_Connect();

                string sid = Request.Form["sid"].ToString();
                string uid = Request.Form["uid"].ToString();

                // LOAD FORMS
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Forms inner join SiteManagerForms on Forms.FormID = SiteManagerForms.FormID where SiteManagerForms.siteid = @SiteID and SiteManagerForms.UserID = @UserID";
                DLdb.SQLST.Parameters.AddWithValue("SiteID", sid);
                DLdb.SQLST.Parameters.AddWithValue("UserID", uid);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        string gStatus = "Active";
                        if (theSqlDataReader["isActive"].ToString() == "False")
                        {
                            gStatus = "Inactive";
                        }

                        // GET THE TOTAL SUBMITITONS
                        var v_nosubmitions = "0";

                        // NO USERS CONNECTED TO FORM
                        var v_nousers = "";
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "select count(*) as Total from SiteForms inner join SiteManagerForms on SiteForms.FormID = SiteManagerForms.FormID and SiteForms.SiteID = SiteManagerForms.SiteID  where SiteForms.siteid = @SiteID and SiteForms.formid = @FormID";
                        DLdb.SQLST2.Parameters.AddWithValue("SiteID", sid.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("FormID", theSqlDataReader["formid"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            theSqlDataReader2.Read();
                            if (theSqlDataReader2["Total"] != DBNull.Value)
                            {
                                v_nousers = theSqlDataReader2["Total"].ToString();
                            }
                            else
                            {
                                v_nousers = "0";
                            }
                        }
                        else
                        {
                            v_nousers = "0";
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        // NO USERS CONNECTED TO FORM
                        var gSavedData = "";
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "select count(*) as Total from FormUserData where sid = @sid and fid = @fid and uid = @uid";
                        DLdb.SQLST2.Parameters.AddWithValue("sid", sid.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("fid", theSqlDataReader["formid"].ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("uid", uid.ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            theSqlDataReader2.Read();
                            if (theSqlDataReader2["Total"] != DBNull.Value)
                            {
                                gSavedData = theSqlDataReader2["Total"].ToString();
                            }
                            else
                            {
                                gSavedData = "0";
                            }
                        }
                        else
                        {
                            gSavedData = "0";
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        var gSubmitData = "";
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "select count(*) as Total from FormUserDataSubmit where sid = @sid and fid = @fid and uid = @uid";
                        DLdb.SQLST2.Parameters.AddWithValue("sid", sid.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("fid", theSqlDataReader["formid"].ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("uid", uid.ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            theSqlDataReader2.Read();
                            if (theSqlDataReader2["Total"] != DBNull.Value)
                            {
                                gSubmitData = theSqlDataReader2["Total"].ToString();
                            }
                            else
                            {
                                gSubmitData = "0";
                            }
                        }
                        else
                        {
                            gSubmitData = "0";
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();


                        // GET COMPLETED
                        var v_nocompleted = "0";

                        //result += "<tr>" +
                        //            "   <td class=\"v-align-middle\"><a href=\"\"></a></td>" +
                        //            "   <td class=\"v-align-middle\"><a href='SiteFormUsers?fid=" + DLdb.Encrypt(theSqlDataReader["FormID"].ToString()) + "&sid=" + Request.QueryString["sid"].ToString() + "'>" + v_nousers + "</a></td>" +
                        //            "   <td class=\"v-align-middle\"><a href='SiteFormSubmissions?fid=" + DLdb.Encrypt(theSqlDataReader["FormID"].ToString()) + "&sid=" + Request.QueryString["sid"].ToString() + "'>" + v_nosubmitions + "</a></td>" +
                        //            "   <td class=\"v-align-middle\"><a href='SiteFormCompleted?fid=" + DLdb.Encrypt(theSqlDataReader["FormID"].ToString()) + "&sid=" + Request.QueryString["sid"].ToString() + "'>" + v_nocompleted + "</a></td>" +
                        //            "   <td class=\"v-align-middle\">" + gStatus + "</td>" +
                        //            "   <td class=\"v-align-middle\">" + theSqlDataReader["type"] + "</td>" +
                        //            "   <td class=\"v-align-middle\"><div class=\"btn-group\"><button title=\"Archive\" class=\"btn btn-success\" type=\"button\" onclick=\"deleteconf('DeleteForm?fid=" + DLdb.Encrypt(theSqlDataReader["FormID"].ToString()) + "')\"><i class=\"fa fa-trash\"></i></button><button title=\"Preview\" class=\"btn btn-success\" type=\"button\" onclick=\"document.location.href='ViewForm?fid=" + DLdb.Encrypt(theSqlDataReader["FormID"].ToString()) + "'\"><i class=\"fa fa-sign-out\"></i></button></div></td>" +
                        //            "</tr>";



                        result += "<div class=\"col-sm-12 col-md-4 col-lg-3\">" +
                                   "<div data-pages=\"portlet\" class=\"panel panel-default bg-white raisedbox\">" +
                                   "    <div class=\"panel-body\">" +
                                   "        <a href=\"ViewForm.html?fid=" + theSqlDataReader["FormID"].ToString() + "&sid=" + sid + "\" style=\"color:#000;\">" +
                                   "            <div class=\"row\">" +
                                   "                <div class=\"col-sm-12 p-l-15 p-t-15 p-b-5 fs-14\" style=\"font-weight:bold;text-align:left;\">" + theSqlDataReader["name"] + "</div>" +
                                   "            </div></a>" +
                                   "          <div class=\"btn-group btn-group-lg text-center col-md-12\">" +
                                   "              <button type=\"button\" class=\"btn btn-success\" onclick=\"document.location.href='ViewForm.html?fid=" + theSqlDataReader["FormID"].ToString() + "&sid=" + sid + "'\"><i class=\"fa fa-eye\"></i></button>" +
                                   "              <button type=\"button\" class=\"btn btn-default\" onclick=\"document.location.href='ViewFormSave.html?fid=" + theSqlDataReader["FormID"].ToString() + "&sid=" + sid + "'\"><i class=\"fa fa-save\"></i> " + gSavedData + "</button>" +
                                   "              <button type=\"button\" class=\"btn btn-default\" onclick=\"document.location.href='ViewFormSubmit.html?fid=" + theSqlDataReader["FormID"].ToString() + "&sid=" + sid + "'\"><i class=\"fa fa-upload\"></i> " + gSubmitData + "</button>" +
                                   "          </div>" +
                                   "    </div>" +
                                   "</div>" +
                               "</div>";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                                
                DLdb.DB_Close();

            }
            else
            {
                result = "error";
            }

            Response.Write(result);
            Response.End();
        }
    }
}