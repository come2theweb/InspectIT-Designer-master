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
    public partial class srv_submitfrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // SET THE SCORES FROM THE QUESTIONS FORM.
            if (Request.QueryString["fid"] != null)
            {
                Global DLdb = new Global();
                DLdb.DB_Connect();

                string fid = Request.QueryString["fid"].ToString();

                // RESULTS OF THE FORMS
                string frmHTML = "<div class=\"col-sm-12 col-md-12 col-lg-12\">" +
                                   "<div data-pages=\"portlet\" class=\"panel panel-default bg-white\">" +
                                   "    <div class=\"panel-body\"><form method=\"post\" action=\"\" id=\"myform\">";


                //OPEN FORM DETAILS
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from Forms where FormID = @FormID";
                DLdb.SQLST.Parameters.AddWithValue("FormID", fid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();

                    // BUILD FORM
                    frmHTML += "<div class=\"row\"><div class=\"col-sm-12 col-md-12 col-lg-12 grey\">" + theSqlDataReader["body"].ToString() + "</div></div><div class=\"row\"><div class=\"col-sm-12 col-md-12 col-lg-12\"><h4>" + theSqlDataReader["name"].ToString() + "</h4></div></div>";

                    // LOAD THE FIELDS
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from FormFields where FormID = @FormID and isActive = '1' order by orderby,formid";
                    DLdb.SQLST2.Parameters.AddWithValue("FormID", theSqlDataReader["FormID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            // CHECK FIELD TYPE
                            if (theSqlDataReader2["type"].ToString() == "head") // HEADER TYPE
                            {
                                // BUILD THE HTML
                                frmHTML += "<div class=\"row\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\"><div id=\"" + theSqlDataReader2["name"].ToString() + "\" class=\"col-sm-12 col-md-12 col-lg-12 p-l-5 alert alert-info bold" + theSqlDataReader2["lblclass"].ToString() + "\">" + theSqlDataReader2["label"].ToString() + "</div></div>";
                            }
                            else if (theSqlDataReader2["type"].ToString() == "radio") // RADIO TYPE
                            {
                                // GET THE OPTIONS
                                string radiooptions = "";
                                if (theSqlDataReader2["options"] != DBNull.Value)
                                {
                                    try
                                    {
                                        string s = theSqlDataReader2["options"].ToString();
                                        string[] radiooptionlist = s.Split(',');
                                        foreach (string item in radiooptionlist)
                                        {
                                            //Console.WriteLine(item);
                                            radiooptions += "<input type=\"radio\" value=\"" + item + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\" id=\"" + theSqlDataReader2["name"].ToString() + "-" + DLdb.replaceitemname(item.ToString()) + "\">" +
                                                            "  <label for=\"" + theSqlDataReader2["name"].ToString() + "-" + DLdb.replaceitemname(item.ToString()) + "\">" + item + "</label>";

                                        }
                                    }

                                    catch (Exception err)
                                    {
                                        // MOVE ONE
                                        Console.WriteLine("{0} Exception caught.", err.Message);
                                    }
                                }
                                // BUILD THE HTML
                                frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\"><label class=\"col-sm-3 control-label\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                           " <div class=\"col-sm-9\">" +
                                           "   <div class=\"radio radio-success\">" +
                                           "     " + radiooptions + "" +
                                           "   </div>" +
                                           " </div></div>";

                            }
                            else if (theSqlDataReader2["type"].ToString() == "text") // TEXT
                            {
                                string required = "";
                                string isRequired = "false";
                                if (theSqlDataReader2["required"].ToString() == "True")
                                {
                                    required = "required";
                                    isRequired = "true";
                                }

                                // BUILD THE HTML
                                frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\"><div class=\"col-sm-12 col-md-12 col-lg-12\"><div aria-required=\"" + isRequired + "\" class=\"form-group form-group-default " + required + "\">" +
                                           "     <label class=\"" + theSqlDataReader2["lblclass"].ToString() + "\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                           "     <div class=\"controls\">" +
                                           "         <input id=\"" + theSqlDataReader2["name"].ToString() + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\" class=\"" + theSqlDataReader2["class"].ToString() + " " + required + "\" type=\"text\" placeholder=\"\" />" +
                                           "     </div>" +
                                           " </div></div></div>";

                            }
                            else if (theSqlDataReader2["type"].ToString() == "img") // IMG
                            {
                                // BUILD THE HTML
                                frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\">" +
                                           " <label class=\"col-sm-12 control-label\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                           " <div class=\"col-sm-12\">" +
                                           "         <button onclick=\"capturePhoto('" + theSqlDataReader2["name"].ToString() + "');return false;\" class=\"btn btn-success\">Camera</button>" +
                                            "        <button onclick=\"getPhoto(pictureSource.PHOTOLIBRARY);document.getElementById('photoid').value='" + theSqlDataReader2["name"].ToString() + "';return false;\" class=\"btn btn-success\">Gallery</button>" +
                                            "        <div class=\"alert alert-default loadimg_api\" id=\"img_div_" + theSqlDataReader2["name"].ToString() + "\">No image selected</div>" +
                                           " </div></div>";

                            }
                            else if (theSqlDataReader2["type"].ToString() == "area") // TEXTAREA
                            {
                                // BUILD THE HTML
                                frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\">" +
                                           " <label for=\"name\" class=\"col-sm-3 control-label\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                           " <div class=\"col-sm-9 p-b-10\">" +
                                           "     <textarea class=\"" + theSqlDataReader2["class"].ToString() + "\" id=\"" + theSqlDataReader2["name"].ToString() + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\" placeholder=\"\"></textarea>" +
                                           " </div>" +
                                           "</div>";

                            }
                            else if (theSqlDataReader2["type"].ToString() == "checkbox") // CHECKBOX TYPE
                            {
                                // GET THE OPTIONS
                                string radiooptions = "";
                                if (theSqlDataReader2["options"] != DBNull.Value)
                                {
                                    try
                                    {
                                        string s = theSqlDataReader2["options"].ToString();
                                        string[] radiooptionlist = s.Split(',');
                                        foreach (string item in radiooptionlist)
                                        {
                                            //Console.WriteLine(item);
                                            radiooptions += "<input type=\"checkbox\" name=\"" + theSqlDataReader2["name"].ToString() + "-" + DLdb.replaceitemname(item.ToString()) + "\" id=\"" + theSqlDataReader2["name"].ToString() + "-" + DLdb.replaceitemname(item.ToString()) + "\" value=\"true\" class=\"success\" ><label for=\"" + theSqlDataReader2["name"].ToString() + "-" + DLdb.replaceitemname(item.ToString()) + "\">" + item + "</label>";
                                        }
                                    }

                                    catch (Exception err)
                                    {
                                        // MOVE ONE
                                        Console.WriteLine("{0} Exception caught.", err.Message);
                                    }
                                }
                                // BUILD THE HTML
                                frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\"><label class=\"col-sm-3 control-label\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                           " <div class=\"col-sm-9\">" +
                                           "   <div class=\"checkbox\">" +
                                           "     " + radiooptions + "" +
                                           "   </div>" +
                                           " </div></div>";
                            }
                            else if (theSqlDataReader2["type"].ToString() == "select") // DROPDOWN TYPE
                            {
                                // GET THE OPTIONS
                                string radiooptions = "";
                                if (theSqlDataReader2["options"] != DBNull.Value)
                                {
                                    try
                                    {
                                        string s = theSqlDataReader2["options"].ToString();
                                        string[] radiooptionlist = s.Split(',');
                                        foreach (string item in radiooptionlist)
                                        {
                                            //Console.WriteLine(item);
                                            radiooptions += "<option value=\"" + item + "\">" + item + "</option>";
                                        }
                                    }

                                    catch (Exception err)
                                    {
                                        // MOVE ONE
                                        Console.WriteLine("{0} Exception caught.", err.Message);
                                    }
                                }

                                string required = "";
                                string isRequired = "false";
                                if (theSqlDataReader2["required"].ToString() == "True")
                                {
                                    required = "required";
                                    isRequired = "true";
                                }

                                // BUILD THE HTML
                                frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\"><div class=\"col-sm-12 col-md-12 col-lg-12\"><div aria-required=\"" + isRequired + "\" class=\"form-group form-group-default " + required + "\">" +
                                           "     <label class=\"" + theSqlDataReader2["lblclass"].ToString() + "\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                           "     <div class=\"controls\">" +
                                           "         <select id=\"" + theSqlDataReader2["name"].ToString() + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\" class=\"" + theSqlDataReader2["class"].ToString() + "\">" + radiooptions + "</select>" +
                                           "     </div>" +
                                           " </div></div></div>";
                            }

                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                frmHTML += "</form></div>" +
                           "<div class=\"form-group\">" +
                           "     <div class=\"col-sm-12 col-md-12 col-lg-12 text-center\">" +
                           "         <div id=\"resultbox\" class=\"alert-danger hide\"></div>" +
                           "         <button class=\"btn btn-default\" onclick=\"javascript:history.go(-1);\"><i class=\"fa fa-backward\"></i></button>" +
                           "     </div>" +
                           "</div>" +
                           "</div></div>";

                Response.Write(frmHTML);
                Response.End();

            }
            else
            {
                Response.Write("Error");
                Response.End();
            }
            
        }
    }
}