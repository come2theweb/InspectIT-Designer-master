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
    public partial class srv_frmSubmit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // SET THE SCORES FROM THE QUESTIONS FORM.
            if (Request.QueryString["fid"] != null && Request.QueryString["sid"] != null && Request.QueryString["json"] != null && Request.QueryString["uid"] != null)
            {
                string fid = Request.QueryString["fid"].ToString();
                string uid = Request.QueryString["uid"].ToString();
                string sid = Request.QueryString["sid"].ToString();
                string json = Request.QueryString["json"].ToString();

                Global DLdb = new Global();
                DLdb.DB_Connect();

                //SAVE IT
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into FormUserDataSubmit (fid,uid,sid,json) values (@fid,@uid,@sid,@json)";
                DLdb.SQLST.Parameters.AddWithValue("fid", fid.ToString());
                DLdb.SQLST.Parameters.AddWithValue("uid", uid.ToString());
                DLdb.SQLST.Parameters.AddWithValue("json", json.ToString());
                DLdb.SQLST.Parameters.AddWithValue("sid", sid.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                //// GET USER DETAILS
                //string gUID = "";
                //string emailaddress = "";
                //string name = "";

                //DLdb.RS.Open();
                //DLdb.SQLST.CommandText = "Select top 1 * from AppUsers where userid = '" + uid.ToString() + "'";
                //DLdb.SQLST.CommandType = CommandType.Text;
                //DLdb.SQLST.Connection = DLdb.RS;
                //theSqlDataReader = DLdb.SQLST.ExecuteReader();

                //if (theSqlDataReader.HasRows)
                //{
                //    theSqlDataReader.Read();
                //    gUID = theSqlDataReader["UserID"].ToString();
                //    emailaddress = theSqlDataReader["emailaddress"].ToString();
                //    name = theSqlDataReader["name"].ToString();
                //}

                //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.RS.Close();

                //// GET CLIENT DETAILS
                //string clientname = "";
                //string clientjobcard = "";
                //string clientcomments = "";
                //string clientteam = "";

                //DLdb.RS.Open();
                //DLdb.SQLST.CommandText = "select top 1 * from appchecklists where checklistid = '" + cid.ToString() + "' order by createdate desc";
                //DLdb.SQLST.CommandType = CommandType.Text;
                //DLdb.SQLST.Connection = DLdb.RS;
                //theSqlDataReader = DLdb.SQLST.ExecuteReader();

                //if (theSqlDataReader.HasRows)
                //{
                //    theSqlDataReader.Read();
                //    clientname = theSqlDataReader["clientname"].ToString();
                //    clientteam = theSqlDataReader["clientteam"].ToString();
                //    clientcomments = theSqlDataReader["clientcomments"].ToString();
                //    clientjobcard = theSqlDataReader["clientjobcard"].ToString();
                //}

                //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.RS.Close();

                //// JSON //
                //json = "[" + json + "]";


                //// GET ALL ELEMENTS
                //string FormName = "";
                //string EmailHTML = "";
                //string EmailSub = "";

                //// GET FORM DETAILS
                //DLdb.RS.Open();
                //DLdb.SQLST.CommandText = "select * from forms where FormID = '" + frmid.ToString() + "' and isactive = '1'";
                //DLdb.SQLST.CommandType = CommandType.Text;
                //DLdb.SQLST.Connection = DLdb.RS;
                //theSqlDataReader = DLdb.SQLST.ExecuteReader();

                //if (theSqlDataReader.HasRows)
                //{
                //    while (theSqlDataReader.Read())
                //    {
                //        FormName = theSqlDataReader["name"].ToString();
                //        EmailHTML = theSqlDataReader["emailbody"].ToString();
                //        EmailSub = theSqlDataReader["emailsubject"].ToString();
                //    }
                //}

                //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.RS.Close();

                //// PREP FIELD BUILDER
                //var html_FIELD_Content = "<table border='0' cellpadding='10px' cellspacing='0' width='80%'><tr>" +
                //                         "  <td align='left' colspan='2' style=\"border: 1px solid #E5E5E5;\">" +
                //                         "      <h3 style='padding-top:10px;margin;0px;'><b>" + FormName + "</b></h3>" +
                //                         "  </td>" +
                //                         "</tr>";

                //// Convert DATA to TABLE
                //DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                //string json_HTML = ConvertDataTableToHTML(dt);
                //foreach (DataRow row in dt.Rows)
                //{
                //    // GET THE FORM AND BUILD THE HTML FOR PDF
                //    DLdb.RS.Open();
                //    DLdb.SQLST.CommandText = "select * from formfields where FormID = '" + frmid.ToString() + "' and isactive = '1' order by orderby";
                //    DLdb.SQLST.CommandType = CommandType.Text;
                //    DLdb.SQLST.Connection = DLdb.RS;
                //    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                //    if (theSqlDataReader.HasRows)
                //    {
                //        while (theSqlDataReader.Read())
                //        {
                //            // check blanks and does not exist

                //            //Response.Write(theSqlDataReader["name"].ToString() + "<br />");

                //            try
                //            {
                //                // check type
                //                if (theSqlDataReader["type"].ToString() == "head")
                //                {
                //                    html_FIELD_Content += "<tr>" +
                //                                          "  <td align='left' colspan='2' style=\"border: 1px solid #E5E5E5;\">" +
                //                                          "      <h3 style='padding-top:10px;margin;0px;'><b>" + theSqlDataReader["label"].ToString() + "</b></h3>" +
                //                                          "  </td>" +
                //                                          "</tr>";
                //                }
                //                else if (theSqlDataReader["type"].ToString() == "checkbox")
                //                {

                //                    //Response.Write("Checkbox" + theSqlDataReader["name"].ToString() + "<br />");

                //                    // split the options
                //                    string fieldvalues = "";
                //                    string str_options = theSqlDataReader["options"].ToString();
                //                    string[] options = str_options.Split(',');
                //                    foreach (string option in options)
                //                    {
                //                        try
                //                        {
                //                            string row_field_id = theSqlDataReader["name"].ToString() + "-" + DLdb.replaceitemname(option.ToString()).ToString();
                //                            string row_value = row[row_field_id].ToString();
                //                            if (row_value == "true")
                //                            {
                //                                if (fieldvalues == "")
                //                                {
                //                                    fieldvalues = option;
                //                                }
                //                                else
                //                                {
                //                                    fieldvalues += "<br />" + option;
                //                                }
                //                            }
                //                        }
                //                        catch (Exception err)
                //                        {
                //                            Response.Write("Checkbox - error: " + err + "<br />");
                //                        }

                //                    }

                //                    html_FIELD_Content += "<tr>" +
                //                                          "  <td align='left' width='70%'>" +
                //                                          "      <b>" + theSqlDataReader["label"].ToString() + "</b>" +
                //                                          "  </td>" +
                //                                          "  <td align='right' width='30%'>" +
                //                                          "      " + fieldvalues + "" +
                //                                          "  </td>" +
                //                                          "</tr>";
                //                }
                //                else if (theSqlDataReader["type"].ToString() == "img")
                //                {
                //                    // GET IMG
                //                    string imgsrc = "";
                //                    DLdb.RS2.Open();
                //                    DLdb.SQLST2.CommandText = "select * from FormImg where cid = @cid and frmid = @frmid and photoid = @photoid";
                //                    DLdb.SQLST2.Parameters.AddWithValue("cid", cid.ToString());
                //                    DLdb.SQLST2.Parameters.AddWithValue("frmid", frmid);
                //                    DLdb.SQLST2.Parameters.AddWithValue("photoid", theSqlDataReader["name"].ToString());
                //                    DLdb.SQLST2.CommandType = CommandType.Text;
                //                    DLdb.SQLST2.Connection = DLdb.RS2;
                //                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                //                    if (theSqlDataReader2.HasRows)
                //                    {
                //                        theSqlDataReader2.Read();
                //                        imgsrc = "http://www.chsitapp.co.za/MediaUploader/" + theSqlDataReader2["imgsrc"].ToString();

                //                        html_FIELD_Content += "<tr>" +
                //                                          "  <td align='left' colspan='2'>" +
                //                                          "      <b>" + theSqlDataReader["label"].ToString() + "</b><br />" +
                //                                          "      <img src=\"" + imgsrc + "\" style='width:50%;height:50%;' />" +
                //                                          "  </td>" +
                //                                          "</tr>";
                //                    }

                //                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                //                    DLdb.SQLST2.Parameters.RemoveAt(0);
                //                    DLdb.SQLST2.Parameters.RemoveAt(0);
                //                    DLdb.SQLST2.Parameters.RemoveAt(0);
                //                    DLdb.RS2.Close();
                //                }
                //                else // text and textarea
                //                {
                //                    // check blanks and does not exist
                //                    if (row[theSqlDataReader["name"].ToString()].ToString() != null)
                //                    {
                //                        html_FIELD_Content += "<tr>" +
                //                                          "  <td align='left' width='70%'>" +
                //                                          "      <b>" + theSqlDataReader["label"].ToString() + "</b>" +
                //                                          "  </td>" +
                //                                          "  <td align='right' width='30%'>" +
                //                                          "      " + row[theSqlDataReader["name"].ToString()].ToString() + "" +
                //                                          "  </td>" +
                //                                          "</tr>";
                //                    }
                //                }
                //            }
                //            catch (Exception err)
                //            {
                //                //Response.Write(err + "<br /");
                //                //Response.End();
                //            }
                //        }
                //        //clientname = theSqlDataReader["clientname"].ToString();
                //        //clientjobcard = theSqlDataReader["clientjobcard"].ToString();
                //    }

                //    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //    DLdb.RS.Close();


                //}

                //html_FIELD_Content += "</table>";


                ////Response.End();

                //// BUILD CLIENT DETAILS
                //string clientdetails = "<div style='border:1px solid #EFEFEF;padding:10px;width:400px;'><b>Client Name: </b>" + clientname + "<br />";
                //clientdetails += "<b>Team: </b>" + clientteam + "<br />";
                //clientdetails += "<b>Comments: </b>" + clientcomments + "<br /></div>";

                //string frmname = FormName.Replace(" ", "").ToString();
                //frmname = frmname.Replace("/", "").ToString();
                //frmname = frmname.Replace("\\", "").ToString();
                //frmname = frmname.Replace("-", "").ToString();
                //frmname = frmname.Replace("?", "").ToString();

                // BUILD THE PDF {JOB CARD NO}, {CLIENT}, {FROM TYPE}
                //string filename = frmname + "_" + clientjobcard.Replace(" ", "").ToString() + "_" + clientname.Replace(" ", "").Replace(" ", "").ToString() + "_" + FormName.Replace(" ", "").ToString() + ".pdf";


                //var htmlContent = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
                //                                    "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                //                                    "        <tr>" +
                //                                    "            <td>" +
                //                                    "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                //                                    "                    <tr>" +
                //                                    "                        <td align='middle'><br /><br /><img src='http://www.plumbertools.co.za/img/logo.png' />" +
                //                                    "                        <div>" + clientdetails + "</div>" +
                //                                    "                        <div style='width:100%;'><div style='position:absolute;top:10;float:right;border: 1px solid #E5E5E5;padding:10px'>" + clientjobcard + "</div></div></td>" +
                //                                    "                    </tr>" +
                //                                    "                    <tr>" +
                //                                    "                        <td align='middle'>" +
                //                                    "                               " + html_FIELD_Content + " " +
                //                                    "                        </td>" +
                //                                    "                    </tr>" +
                //                                    "                </table>" +
                //                                    "            </td>" +
                //                                    "        </tr>" +
                //                                    "    </table>" +
                //                                    "</body>");

                //var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(htmlContent);
                //string path = Server.MapPath("~/pdf_checklists/") + filename;
                //File.WriteAllBytes(path, pdfBytes);

                // EMAIL THE NEW USERS
                //string HTMLBody = "Dear " + name.ToString() + "<br /><br />Plumbers App checklist data for " + clientname + " (" + clientjobcard + "), see details below.<br /><br />" + json_HTML + "<br /><br />Images: " + frmimages + "<br /><br /><Regards<br />System Administrator";
                //string HTMLSubject = "Plumbers App Checklist for " + clientname + " (" + clientjobcard + ")";

                //string HTMLBody = EmailHTML.Replace("[CLIENTNAME]", clientname.ToString()).Replace("[TYPE]", FormName.ToString()).Replace("[JOBCARDNO]", clientjobcard.ToString()).Replace("[TEAM]", clientteam.ToString()).Replace("[COMMENTS]", clientcomments.ToString());
                //string HTMLSubject = EmailSub.Replace("[CLIENTNAME]", clientname.ToString()).Replace("[TYPE]", FormName.ToString()).Replace("[JOBCARDNO]", clientjobcard.ToString()).Replace("[TEAM]", clientteam.ToString()).Replace("[COMMENTS]", clientcomments.ToString());

                //string targetAddress = emailaddress.ToString();
                //DLdb.sendEmailATT(HTMLBody, HTMLSubject, "mathew@slugg.co.za", "mathew@slugg.co.za", targetAddress, filename);

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