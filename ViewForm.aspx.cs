using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace InspectIT
{
    public partial class ViewFormNew : System.Web.UI.Page
    {
        public string gDID = "0";
        public string gCOCID = "";
        public string gTypeID = "";
        public string gFormID = "";

        protected override void OnInit(EventArgs e)
        {
            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("default");
            }

            // CHECK THE FORM ID HAS BEEN PASSED TO THE PAGE
            if (Request.QueryString["tid"] == null)
            {
                Response.Write("Error");
                Response.End();
            }

            Global DLdb = new Global();

            // GET THE FORM ID
            DLdb.DB_Connect();

            COCNumber.InnerHtml = "COC Number: " + DLdb.Decrypt(Request.QueryString["cocid"].ToString());
            COCType.InnerHtml = "Type: Normal";

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

            // GET THE DATA ID IF EXISTS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCInstallations where COCStatementID = @COCStatementID and TypeID = @TypeID and DataID is not Null";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("TypeID", DLdb.Decrypt(Request.QueryString["tid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                gDID = theSqlDataReader["DataID"].ToString();
                gFormID = gDID;
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            gCOCID = DLdb.Decrypt(Request.QueryString["cocid"].ToString());
            gTypeID = DLdb.Decrypt(Request.QueryString["tid"].ToString());
            

            DLdb.DB_Close();

            if (FormID != "")
            {
                // BUILD THE FORM
                viewformdisplay.InnerHtml = DLdb.buildForm(FormID, false, gCOCID, gTypeID);
            }

            if (Request.QueryString["refix"] != null)
            {
                // REFIX
                //DisplayRefixNotice.Visible = true;
            }

            
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("default");
            }

            // CHECK THE FORM ID HAS BEEN PASSED TO THE PAGE
            if (Request.QueryString["tid"] == null)
            {
                Response.Write("Error");
                Response.End();
            }

        }

        protected void ViewPDF_Click(object sender, EventArgs e)
        {
            // CREATE THE PDF
            //Global DLdb = new Global();
            //DLdb.DB_Connect();
            
            //// JSON //
            //string json = jdata.Text.ToString();
            //json = "[" + json + "]";


            //// GET ALL ELEMENTS
            //string FormName = "";
            //string EmailHTML = "";
            //string EmailSub = "";

            //string fid = DLdb.Decrypt(Request.QueryString["fid"].ToString());

            //// GET FORM DETAILS
            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "select * from forms where FormID = '" + fid + "' and isactive = '1'";
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
            //var html_FIELD_Content = "";

            //// Convert DATA to TABLE
            //DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            //string json_HTML = DLdb.ConvertDataTableToHTML(dt);
            //foreach (DataRow row in dt.Rows)
            //{
            //    // GET THE FORM AND BUILD THE HTML FOR PDF
            //    DLdb.RS.Open();
            //    DLdb.SQLST.CommandText = "select * from formfields where FormID = '" + fid + "' and isactive = '1' order by orderby";
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
            //                                          "  <td align='left' colspan='2' style=\"background-image: none;box-shadow: none;text-shadow: none;padding: 9px 19px 9px 15px;border-radius: 3px;font-size: 13px;border-width: 0;-webkit-transition: all 0.2s linear 0s;transition: all 0.2s linear 0s;background-color: #daeffd;color: #2b6a94;border-color: #2b6a94;padding-left:5px;\">" +
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
            //                            //Response.Write("Checkbox - error: " + err + "<br />");
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
            //                    DLdb.SQLST2.Parameters.AddWithValue("cid", "");
            //                    DLdb.SQLST2.Parameters.AddWithValue("frmid", fid);
            //                    DLdb.SQLST2.Parameters.AddWithValue("photoid", theSqlDataReader["name"].ToString());
            //                    DLdb.SQLST2.CommandType = CommandType.Text;
            //                    DLdb.SQLST2.Connection = DLdb.RS2;
            //                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            //                    if (theSqlDataReader2.HasRows)
            //                    {
            //                        theSqlDataReader2.Read();
            //                        imgsrc = "http://www.plumbertools.co.za/MediaUploader/" + theSqlDataReader2["imgsrc"].ToString();

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
            
            //string frmname = FormName.Replace(" ", "").ToString();
            //frmname = frmname.Replace("/", "").ToString();
            //frmname = frmname.Replace("\\", "").ToString();
            //frmname = frmname.Replace("-", "").ToString();
            //frmname = frmname.Replace("?", "").ToString();
            //frmname = frmname.Replace("&", "").ToString();

            //// BUILD THE PDF {JOB CARD NO}, {CLIENT}, {FROM TYPE}
            //string filename = frmname + "_template.pdf";
            
            //var htmlContent = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
            //                                    "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
            //                                    "        <tr>" +
            //                                    "            <td>" +
            //                                    "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
            //                                    "                    <tr>" +
            //                                    "                        <td align='middle'><br /><br /><img src='http://www.chsitapp.co.za/assets/img/logo.png' />" +
            //                                    "                        <div style='width:100%;'><div style='position:absolute;top:10;float:right;border: 1px solid #E5E5E5;padding:10px'>" + FormName + "</div></div></td>" +
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
            //string path = Server.MapPath("~/pdfs/") + filename;
            //File.WriteAllBytes(path, pdfBytes);

            //string url = "pdfs/" + filename;
            //string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,left=100,top=100,resizable=yes');";
            //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        }


                   

    }
}