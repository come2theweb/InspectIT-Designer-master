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
    public partial class srv_viewfrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string FormID = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormLinks where FormType = @FormType and TypeID = @TypeID";
            DLdb.SQLST.Parameters.AddWithValue("FormType", Request.QueryString["typ"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("TypeID", DLdb.Decrypt(Request.QueryString["tid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                FormID = theSqlDataReader["FormID"].ToString();

            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // SET THE SCORES FROM THE QUESTIONS FORM.
            if (FormID != null)
            {
                string cocid = DLdb.Decrypt(Request.QueryString["cocid"].ToString());
                string tid = Request.QueryString["tid"].ToString();
                Boolean showDetails = false;

                //Response.Write(DLdb.buildForm(FormID, false, cocid, tid));

                // RESULTS OF THE FORMS
                string frmHTML = "";

                //OPEN FORM DETAILS
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from Forms where FormID = @FormID";
                DLdb.SQLST.Parameters.AddWithValue("FormID", FormID.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();

                    // BUILD FORM
                    if (showDetails == true)
                    {
                        frmHTML += "<div class=\"row\"><div class=\"col-sm-12 col-md-12 col-lg-12 h4\">" + theSqlDataReader["name"].ToString() + "</div></div><div class=\"row\"><div class=\"col-sm-12 col-md-12 col-lg-12\">" + theSqlDataReader["body"].ToString() + "</div></div>";
                    }

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
                            // CHECK IF THE FIELD HAS A REFIX NOTICE
                            string btnNoticeShow = "";
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "select * from COCRefixFields where COCStatementID = @COCStatementID and TypeID = @TypeID and FieldID = @FieldID";
                            DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", cocid);
                            DLdb.SQLST3.Parameters.AddWithValue("TypeID", tid);
                            DLdb.SQLST3.Parameters.AddWithValue("FieldID", theSqlDataReader2["FieldID"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {
                                    string alertcol = "info";
                                    if (theSqlDataReader3["NoticeType"].ToString() == "Failure")
                                    {
                                        alertcol = "danger";
                                    }
                                    else if (theSqlDataReader3["NoticeType"].ToString() == "Cautionary")
                                    {
                                        alertcol = "warning";
                                    }
                                    else if (theSqlDataReader3["NoticeType"].ToString() == "Complement")
                                    {
                                        alertcol = "success";
                                    }

                                    if (theSqlDataReader3["picture"] != DBNull.Value)
                                    {
                                        btnNoticeShow += "<div class=\"row\"><div class=\"col-md-12\"><div class=\"alert alert-" + alertcol + "\"><h5><b>" + theSqlDataReader3["NoticeType"].ToString() + "</b></h5>" + theSqlDataReader3["Comments"].ToString() + "<br /><br /><img src=\"noticeimages/" + theSqlDataReader3["picture"].ToString() + "\" class=\"img-responsive\" /><br /><br />" + theSqlDataReader3["Createdate"].ToString() + "</div></div></div>";
                                    }
                                    else
                                    {
                                        btnNoticeShow += "<div class=\"row\"><div class=\"col-md-12\"><div class=\"alert alert-" + alertcol + "\"><h5><b>" + theSqlDataReader3["NoticeType"].ToString() + "</b></h5>" + theSqlDataReader3["Comments"].ToString() + "<br /><br />" + theSqlDataReader3["Createdate"].ToString() + "</div></div></div>";
                                    }

                                }
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();


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
                                frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\"><label class=\"col-sm-12 control-label\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                           " <div class=\"col-sm-9\">" +
                                           "   <div class=\"radio radio-success\">" +
                                           "     " + radiooptions + "" +
                                           "   </div>" +
                                           " </div>" + btnNoticeShow + "</div>";

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
                                           "         <input id=\"" + theSqlDataReader2["name"].ToString() + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\" class=\"form-control " + required + "\" style=\"border:1px solid #d6d6d6\" type=\"text\" />" +
                                           "     </div>" +
                                           " </div></div>" + btnNoticeShow + "</div>";

                            }
                            else if (theSqlDataReader2["type"].ToString() == "img") // IMG
                            {
                                // BUILD THE HTML
                                frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\">" +
                                           " <label class=\"col-sm-12 control-label\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                           " <div class=\"col-sm-12\">" +
                                          // "         <input type=\"file\" id=\"uploadFileName" + theSqlDataReader2["name"].ToString() + "\" />" +
                                           // "        <div onclick=\"uploadFile('" + theSqlDataReader2["name"].ToString() + "')\" class=\"btn btn-primary\">Upload</div>" +
                                            "	    <label onclick=\"capturePhoto('" + theSqlDataReader2["name"].ToString() + "')\" class=\"btn btn-primary\">Camera</label>" +
                                            "       <label onclick=\"getPhoto(navigator.camera.PictureSourceType.PHOTOLIBRARY, '" + theSqlDataReader2["name"].ToString() + "')\" class=\"btn btn-primary\">Gallery</label>" +
                                            "        <div class=\"alert alert-default loadimg_api\" id=\"img_div_" + theSqlDataReader2["name"].ToString() + "\">No image selected</div>" +
                                           " </div>" + btnNoticeShow + "</div>";


                            }
                            else if (theSqlDataReader2["type"].ToString() == "area") // TEXTAREA
                            {
                                // BUILD THE HTML
                                frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\">" +
                                           " <label for=\"name\" class=\"col-sm-12 control-label\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                           " <div class=\"col-sm-12 p-b-10\">" +
                                           "     <textarea class=\"form-control\" style=\"border:1px solid #d6d6d6\" id=\"" + theSqlDataReader2["name"].ToString() + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\"></textarea>" +
                                           " </div>" +
                                           "" + btnNoticeShow + "</div>";

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
                                            radiooptions += "<input type=\"checkbox\" name=\"" + theSqlDataReader2["name"].ToString() + "-" + DLdb.replaceitemname(item.ToString()) + "\" id=\"" + theSqlDataReader2["name"].ToString() + "-" + DLdb.replaceitemname(item.ToString()) + "\" value=\"true\" class=\"checkbox check-success\" ><label for=\"" + theSqlDataReader2["name"].ToString() + "-" + DLdb.replaceitemname(item.ToString()) + "\">" + item + "</label>";
                                        }
                                    }

                                    catch (Exception err)
                                    {
                                        // MOVE ONE
                                        Console.WriteLine("{0} Exception caught.", err.Message);
                                    }
                                }
                                // BUILD THE HTML
                                frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\"><label class=\"col-sm-12 control-label\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                           " <div class=\"col-sm-12\">" +
                                           "   <div class=\"checkbox\">" +
                                           "     " + radiooptions + "" +
                                           "   </div>" +
                                           " </div>" + btnNoticeShow + "</div><br />";
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
                                           " </div></div>" + btnNoticeShow + "</div>";
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

                // BUTTONS
                frmHTML += "</div>" +
                               "<div class=\"form-group\">" +
                               "     <div class=\"col-sm-12 col-md-12 col-lg-12 text-center\">" +
                               "         <div id=\"resultbox\" class=\"alert-danger hide\"></div>" +
                               "         <label class=\"btn btn-success\" onclick=\"saveFRM()\">Save Form</label>" +
                               "         <label class=\"btn btn-default\" onclick=\"javascript:history.go(-1);\">Cancel</label>" +
                               "     </div>" +
                               "</div>" +
                               "</div></div>";

                // SCRIPTS
                //frmHTML += "<script>function saveFRM() { // intercepts the submit event" + 
                //           "     $(\".ui-loader\").addClass(\"show\");" + 
                //           "     var fid = getUrlVars()[\"fid\"];" + 
                //           "     var sid = getUrlVars()[\"sid\"];" + 
                //           "     var jData = JSON.stringify($(\"#myform\").serializeObject())" + 
                //           "     $.post('http://localhost:51191/srvAPI/srv_frmSave.aspx?fid=' + fid + '&sid=' + sid + '&uid=' + localStorage.OHS_UID + '&json=' + jData, {}, function (data) {" + 
                //           "         $(\"#resultbox\").append(data);" + 
                //           "     }).done(function () {" + 
                //           "         var gData = $(\"#resultbox\").html();" + 
                //           "         if (gData == 'Error') {" + 
                //           "             $(\"#resultbox\").show();" + 
                //           "             $(\"#resultbox\").html('Please select something.')" + 
                //           "         } else {" + 
                //           "             document.location.href = \"sitesforms.html?sid=\" + sid;" + 
                //           "         }" + 
                //           "     });" + 
                //           " }</script>";




                DLdb.DB_Close();

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