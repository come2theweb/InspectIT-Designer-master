using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Net.Mime;
using Newtonsoft.Json;

namespace InspectIT
{
    public class Global : System.Web.HttpApplication
    {
        public SqlConnection RS = new SqlConnection();
        public SqlConnection RS2 = new SqlConnection();
        public SqlConnection RS3 = new SqlConnection();
        public SqlConnection RS4 = new SqlConnection();
        public SqlConnection RS5 = new SqlConnection();

        public SqlConnection classRS = new SqlConnection();
        public SqlConnection classRS2 = new SqlConnection();
        public SqlConnection classRS3 = new SqlConnection();

        internal void Encrypt(string v1, string v2)
        {
            throw new NotImplementedException();
        }

        public SqlConnection classRS4 = new SqlConnection();
        public SqlConnection classRS5 = new SqlConnection();

        public SqlConnection pirbRS = new SqlConnection();
        public SqlConnection pirbRS2 = new SqlConnection();
        public SqlConnection pirbRS3 = new SqlConnection();
        public SqlConnection pirbRS4 = new SqlConnection();
        public SqlConnection pirbRS5 = new SqlConnection();

        public SqlConnection iitRS = new SqlConnection();
        public SqlConnection iitRS2 = new SqlConnection();
        public SqlConnection iitRS3 = new SqlConnection();
        public SqlConnection iitRS4 = new SqlConnection();
        public SqlConnection iitRS5 = new SqlConnection();

        public SqlCommand SQLST = new SqlCommand();
        public SqlCommand SQLST2 = new SqlCommand();
        public SqlCommand SQLST3 = new SqlCommand();
        public SqlCommand SQLST4 = new SqlCommand();
        public SqlCommand SQLST5 = new SqlCommand();

        public SqlCommand classSQLST = new SqlCommand();
        public SqlCommand classSQLST2 = new SqlCommand();
        public SqlCommand classSQLST3 = new SqlCommand();
        public SqlCommand classSQLST4 = new SqlCommand();
        public SqlCommand classSQLST5 = new SqlCommand();

        public SqlCommand pirbSQLST = new SqlCommand();
        public SqlCommand pirbSQLST2 = new SqlCommand();
        public SqlCommand pirbSQLST3 = new SqlCommand();
        public SqlCommand pirbSQLST4 = new SqlCommand();
        public SqlCommand pirbSQLST5 = new SqlCommand();

        public SqlCommand iitSQLST = new SqlCommand();
        public SqlCommand iitSQLST2 = new SqlCommand();
        public SqlCommand iitSQLST3 = new SqlCommand();
        public SqlCommand iitSQLST4 = new SqlCommand();
        public SqlCommand iitSQLST5 = new SqlCommand();

        static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["IIT_GUID"] = System.Guid.NewGuid();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Session["IIT_GUID"] = null;
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        // CUSTOM CLASSES
        protected void Application_Error(object sender, EventArgs e)
        {
            //Exception objErr = Server.GetLastError().GetBaseException();
            //string err = "Error Caught in Application_Error event\n" +
            //        "Error in: " + Request.Url.ToString() +
            //        "\nError Message:" + objErr.Message.ToString() +
            //        "\nStack Trace:" + objErr.StackTrace.ToString();

            //if (Session["IIT_UID"] != null)
            //{
            //    ERR_log(Session["IIT_GUID"].ToString(), Session["IIT_UID"].ToString(), Session["IIT_UName"].ToString(), err.ToString());
            //}
            //else
            //{
            //    ERR_log(Session["IIT_GUID"].ToString(), "0", "No User", err.ToString());
            //}

        }

        //CUSTOM ERROR LOGGING
        public void ERR_log(string UserGUID, string UserID, string UserName, string error)
        {
            SqlConnection RSError = new SqlConnection();
            SqlCommand SQLSTError = new SqlCommand();

            string IPAddress = string.Empty;
            string SearchName = string.Empty;

            String strHostName = HttpContext.Current.Request.UserHostAddress.ToString();
            IPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();

            string UserIPAddress = IPAddress;
            string err = error;

            string Connectionstring = System.Configuration.ConfigurationManager.AppSettings["SQLConnectionString"];
            RSError.ConnectionString = Connectionstring;

            RSError.Open();
            SQLSTError.CommandText = "exec Add_ErrorLog @UserIPAddress,@UserGUID,@UserID,@UserName,@Err";
            SQLSTError.Parameters.AddWithValue("UserIPAddress", UserIPAddress);
            SQLSTError.Parameters.AddWithValue("UserGUID", UserGUID);
            SQLSTError.Parameters.AddWithValue("UserID", UserID);
            SQLSTError.Parameters.AddWithValue("UserName", UserName);
            SQLSTError.Parameters.AddWithValue("Err", err);
            SQLSTError.CommandType = CommandType.Text;
            SQLSTError.Connection = RSError;
            SqlDataReader theSqlDataReader_Error = SQLSTError.ExecuteReader();

            if (theSqlDataReader_Error.IsClosed) theSqlDataReader_Error.Close();
            SQLSTError.Parameters.RemoveAt(0);
            SQLSTError.Parameters.RemoveAt(0);
            SQLSTError.Parameters.RemoveAt(0);
            SQLSTError.Parameters.RemoveAt(0);
            SQLSTError.Parameters.RemoveAt(0);
            RSError.Close();

            Server.ClearError();

            //this.Response.Redirect("default.aspx?msg=" + err);

        }

        // CONNECT TO DB
        public void DB_Connect()
        {

            string Connectionstring = System.Configuration.ConfigurationManager.AppSettings["SQLConnectionString"];

            RS.ConnectionString = Connectionstring;
            RS2.ConnectionString = Connectionstring;
            RS3.ConnectionString = Connectionstring;
            RS4.ConnectionString = Connectionstring;
            RS5.ConnectionString = Connectionstring;

            classRS.ConnectionString = Connectionstring;
            classRS2.ConnectionString = Connectionstring;
            classRS3.ConnectionString = Connectionstring;
            classRS4.ConnectionString = Connectionstring;
            classRS5.ConnectionString = Connectionstring;

        }

        public void DB_PIRB_Connect()
        {

            string Connectionstring = System.Configuration.ConfigurationManager.AppSettings["pirbSQLConnectionString"];

            pirbRS.ConnectionString = Connectionstring;
            pirbRS2.ConnectionString = Connectionstring;
            pirbRS3.ConnectionString = Connectionstring;
            pirbRS4.ConnectionString = Connectionstring;
            pirbRS5.ConnectionString = Connectionstring;

            pirbRS.ConnectionString = Connectionstring;
            pirbRS2.ConnectionString = Connectionstring;
            pirbRS3.ConnectionString = Connectionstring;
            pirbRS4.ConnectionString = Connectionstring;
            pirbRS5.ConnectionString = Connectionstring;

        }

        public void DB_IITOld_Connect()
        {

            string Connectionstring = System.Configuration.ConfigurationManager.AppSettings["IITOldSQLConnectionString"];

            iitRS.ConnectionString = Connectionstring;
            iitRS2.ConnectionString = Connectionstring;
            iitRS3.ConnectionString = Connectionstring;
            iitRS4.ConnectionString = Connectionstring;
            iitRS5.ConnectionString = Connectionstring;

            iitRS.ConnectionString = Connectionstring;
            iitRS2.ConnectionString = Connectionstring;
            iitRS3.ConnectionString = Connectionstring;
            iitRS4.ConnectionString = Connectionstring;
            iitRS5.ConnectionString = Connectionstring;

        }
        
        public void classDB_Connect()
        {

            string Connectionstring = System.Configuration.ConfigurationManager.AppSettings["SQLConnectionString"];

            classRS.ConnectionString = Connectionstring;
            classRS2.ConnectionString = Connectionstring;
            classRS3.ConnectionString = Connectionstring;
            classRS4.ConnectionString = Connectionstring;
            classRS5.ConnectionString = Connectionstring;

        }

        // CLOSE DB CONNECTIONS
        public void DB_Close()
        {
            RS.Close();
            RS2.Close();
            RS3.Close();
            RS4.Close();
            RS5.Close();

        }

        public void DB_PIRB_Close()
        {
            pirbRS.Close();
            pirbRS2.Close();
            pirbRS3.Close();
            pirbRS4.Close();
            pirbRS5.Close();

        }

        public void DB_IITOld_Close()
        {
            iitRS.Close();
            iitRS2.Close();
            iitRS3.Close();
            iitRS4.Close();
            iitRS5.Close();

        }

        public void classDB_Close()
        {

            classRS.Close();
            classRS2.Close();
            classRS3.Close();
            classRS4.Close();
            classRS5.Close();

        }

        public string Encrypt(string originalString)
        {
            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes),
                CryptoStreamMode.Write);
            var writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            //return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            var results = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length).Replace("+", "|");
            results = results.Replace("=", "~");
            return results;
        }

        public string Decrypt(string encryptedString)
        {
            try
            {
                string eS = encryptedString.Replace("|", "+");
                eS = eS.Replace("|", "+");
                eS = eS.Replace("~", "=");
                var cryptoProvider = new DESCryptoServiceProvider();
                var memoryStream = new MemoryStream(Convert.FromBase64String(eS));
                var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes),
                    CryptoStreamMode.Read);
                var reader = new StreamReader(cryptoStream);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return encryptedString;
            }

        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public string CreateNumber(int length)
        {
            const string valid = "1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public void sendEmail(string HTMLBody, string HTMLSubject, string BCC, string targetAddress, string file)
        {
            try
            {
                // get Settings
                string smtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
                string smtpUser = System.Configuration.ConfigurationManager.AppSettings["smtpUser"];
                string smtpPass = System.Configuration.ConfigurationManager.AppSettings["smtpPass"];
                string FromAddress = System.Configuration.ConfigurationManager.AppSettings["FromAddress"];

                SmtpClient mailClient = new System.Net.Mail.SmtpClient();
                MailMessage mailMessage = new System.Net.Mail.MailMessage(FromAddress, targetAddress.ToString().Replace(";", ",").Trim());
                NetworkCredential basicCredential = new NetworkCredential(smtpUser, smtpPass);

                string messageHtml = HTMLBody;

                // check File Attachement
                if (file != "")
                {
                    //var filePath = Server.MapPath("~/pdf");
                    //file = filePath + file;
                    Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                    ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(file);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                    mailMessage.Attachments.Add(data);
                }

                mailClient.Host = smtpServer;
                mailClient.UseDefaultCredentials = false;
                mailClient.Credentials = basicCredential;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = HTMLSubject;
                mailMessage.Bcc.Add(BCC);
                mailMessage.To.Add(new MailAddress(targetAddress.ToString().Replace(";", ",").Trim()));
                mailMessage.Body = messageHtml;

                // send Email
                mailClient.Send(mailMessage);
            }
            catch (Exception err)
            {
                ERR_log("000-000-0001", "0", "Email", err.ToString());
            }


        }

        public string buildForm(string FID, Boolean showDetails, string cocid, string typeid)
        {
            // RESULTS OF THE FORMS
            string frmHTML = "";

            DB_Connect();

            //OPEN FORM DETAILS
            RS.Open();
            SQLST.CommandText = "Select * from Forms where FormID = @FormID";
            SQLST.Parameters.AddWithValue("FormID", FID.ToString());
            SQLST.CommandType = CommandType.Text;
            SQLST.Connection = RS;
            SqlDataReader theSqlDataReader = SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();

                // BUILD FORM
                if (showDetails == true)
                {
                    frmHTML += "<div class=\"row\"><div class=\"col-sm-12 col-md-12 col-lg-12 h4\">" + theSqlDataReader["name"].ToString() + "</div></div><div class=\"row\"><div class=\"col-sm-12 col-md-12 col-lg-12\">" + theSqlDataReader["body"].ToString() + "</div></div>";
                }
                
                // LOAD THE FIELDS
                RS2.Open();
                SQLST2.CommandText = "Select * from FormFields where FormID = @FormID and isActive = '1' order by orderby,formid";
                SQLST2.Parameters.AddWithValue("FormID", theSqlDataReader["FormID"].ToString());
                SQLST2.CommandType = CommandType.Text;
                SQLST2.Connection = RS2;
                SqlDataReader theSqlDataReader2 = SQLST2.ExecuteReader();

                if (theSqlDataReader2.HasRows)
                {
                    while (theSqlDataReader2.Read())
                    {
                        // CHECK IF THE FIELD HAS A REFIX NOTICE
                        string btnNoticeShow = "";
                        RS3.Open();
                        SQLST3.CommandText = "select * from COCRefixFields where COCStatementID = @COCStatementID and TypeID = @TypeID and FieldID = @FieldID";
                        SQLST3.Parameters.AddWithValue("COCStatementID", cocid);
                        SQLST3.Parameters.AddWithValue("TypeID", typeid);
                        SQLST3.Parameters.AddWithValue("FieldID", theSqlDataReader2["FieldID"].ToString());
                        SQLST3.CommandType = CommandType.Text;
                        SQLST3.Connection = RS3;
                        SqlDataReader theSqlDataReader3 = SQLST3.ExecuteReader();

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
                        SQLST3.Parameters.RemoveAt(0);
                        SQLST3.Parameters.RemoveAt(0);
                        SQLST3.Parameters.RemoveAt(0);
                        RS3.Close();


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
                                        radiooptions += "<input type=\"radio\" value=\"" + item + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\" id=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\">" +
                                                        "  <label for=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\">" + item + "</label>";

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
                                       "         <input id=\"" + theSqlDataReader2["name"].ToString() + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\" class=\"" + theSqlDataReader2["class"].ToString() + " " + required + "\" type=\"text\" placeholder=\"\" />" +
                                       "     </div>" +
                                       " </div></div>" + btnNoticeShow + "</div>";

                        }
                        else if (theSqlDataReader2["type"].ToString() == "img") // IMG
                        {
                            // BUILD THE HTML
                            frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\">" +
                                       " <label class=\"col-sm-12 control-label\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                       " <div class=\"col-sm-12\">" +
                                       "         <input type=\"file\" id=\"uploadFileName" + theSqlDataReader2["name"].ToString() + "\" />" +
                                        "        <div onclick=\"uploadFile('" + theSqlDataReader2["name"].ToString() + "')\" class=\"btn btn-primary\">Upload</div>" +
                                        "        <div class=\"alert alert-default loadimg_api\" id=\"img_div_" + theSqlDataReader2["name"].ToString() + "\">No image selected</div>" +
                                       " </div>" + btnNoticeShow + "</div>";
                            

                        }
                        else if (theSqlDataReader2["type"].ToString() == "area") // TEXTAREA
                        {
                            // BUILD THE HTML
                            frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\">" +
                                       " <label for=\"name\" class=\"col-sm-12 control-label\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                       " <div class=\"col-sm-12 p-b-10\">" +
                                       "     <textarea class=\"" + theSqlDataReader2["class"].ToString() + "\" id=\"" + theSqlDataReader2["name"].ToString() + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\" placeholder=\"\"></textarea>" +
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
                                        radiooptions += "<input type=\"checkbox\" name=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\" id=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\" value=\"true\" class=\"checkbox check-success\" ><label for=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\">" + item + "</label>";
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
                SQLST2.Parameters.RemoveAt(0);
                RS2.Close();

            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            SQLST.Parameters.RemoveAt(0);
            RS.Close();

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

            return frmHTML;
        }



        // ****************************************************************************************
        // APP FORMS BUILDER
        // ****************************************************************************************
        public string buildFormAPP(string FID, Boolean showDetails, string cocid, string typeid)
        {
            // RESULTS OF THE FORMS
            string frmHTML = "";

            DB_Connect();

            //OPEN FORM DETAILS
            RS.Open();
            SQLST.CommandText = "Select * from Forms where FormID = @FormID";
            SQLST.Parameters.AddWithValue("FormID", FID.ToString());
            SQLST.CommandType = CommandType.Text;
            SQLST.Connection = RS;
            SqlDataReader theSqlDataReader = SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();

                // BUILD FORM
                if (showDetails == true)
                {
                    frmHTML += "<div class=\"row\"><div class=\"col-sm-12 col-md-12 col-lg-12 h4\">" + theSqlDataReader["name"].ToString() + "</div></div><div class=\"row\"><div class=\"col-sm-12 col-md-12 col-lg-12\">" + theSqlDataReader["body"].ToString() + "</div></div>";
                }

                // LOAD THE FIELDS
                RS2.Open();
                SQLST2.CommandText = "Select * from FormFields where FormID = @FormID and isActive = '1' order by orderby,formid";
                SQLST2.Parameters.AddWithValue("FormID", theSqlDataReader["FormID"].ToString());
                SQLST2.CommandType = CommandType.Text;
                SQLST2.Connection = RS2;
                SqlDataReader theSqlDataReader2 = SQLST2.ExecuteReader();

                if (theSqlDataReader2.HasRows)
                {
                    while (theSqlDataReader2.Read())
                    {
                        // CHECK IF THE FIELD HAS A REFIX NOTICE
                        string btnNoticeShow = "";
                        RS3.Open();
                        SQLST3.CommandText = "select * from COCRefixFields where COCStatementID = @COCStatementID and TypeID = @TypeID and FieldID = @FieldID";
                        SQLST3.Parameters.AddWithValue("COCStatementID", cocid);
                        SQLST3.Parameters.AddWithValue("TypeID", typeid);
                        SQLST3.Parameters.AddWithValue("FieldID", theSqlDataReader2["FieldID"].ToString());
                        SQLST3.CommandType = CommandType.Text;
                        SQLST3.Connection = RS3;
                        SqlDataReader theSqlDataReader3 = SQLST3.ExecuteReader();

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
                        SQLST3.Parameters.RemoveAt(0);
                        SQLST3.Parameters.RemoveAt(0);
                        SQLST3.Parameters.RemoveAt(0);
                        RS3.Close();


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
                                        radiooptions += "<input type=\"radio\" value=\"" + item + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\" id=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\">" +
                                                        "  <label for=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\">" + item + "</label>";

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
                                       "         <input id=\"" + theSqlDataReader2["name"].ToString() + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\" class=\"" + theSqlDataReader2["class"].ToString() + " " + required + "\" type=\"text\" placeholder=\"\" />" +
                                       "     </div>" +
                                       " </div></div>" + btnNoticeShow + "</div>";

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
                                       " </div>" + btnNoticeShow + "</div>";

                            // MOBILE CONTROL
                            //frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\">" +
                            //           " <label class=\"col-sm-12 control-label\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                            //           " <div class=\"col-sm-12\">" +
                            //           "         <button onclick=\"capturePhoto('" + theSqlDataReader2["name"].ToString() + "');return false;\" class=\"btn btn-success\">Camera</button>" +
                            //            "        <button onclick=\"getPhoto(pictureSource.PHOTOLIBRARY);document.getElementById('photoid').value='" + theSqlDataReader2["name"].ToString() + "';return false;\" class=\"btn btn-success\">Gallery</button>" +
                            //            "        <div class=\"alert alert-default loadimg_api\" id=\"img_div_" + theSqlDataReader2["name"].ToString() + "\">No image selected</div>" +
                            //           " </div></div>";

                        }
                        else if (theSqlDataReader2["type"].ToString() == "area") // TEXTAREA
                        {
                            // BUILD THE HTML
                            frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\">" +
                                       " <label for=\"name\" class=\"col-sm-12 control-label\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                       " <div class=\"col-sm-12 p-b-10\">" +
                                       "     <textarea class=\"" + theSqlDataReader2["class"].ToString() + "\" id=\"" + theSqlDataReader2["name"].ToString() + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\" placeholder=\"\"></textarea>" +
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
                                        radiooptions += "<input type=\"checkbox\" name=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\" id=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\" value=\"true\" class=\"checkbox check-success\" ><label for=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\">" + item + "</label>";
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
                SQLST2.Parameters.RemoveAt(0);
                RS2.Close();

            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            SQLST.Parameters.RemoveAt(0);
            RS.Close();

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

            return frmHTML;
        }


        // ****************************************************************************************
        // INSPECTOR FORM BUILDER
        // ****************************************************************************************


        public string buildFormInspector(string FID, Boolean showDetails, string cocid, string typeid)
        {
            // RESULTS OF THE FORMS
            string frmHTML = "";

            DB_Connect();

            //OPEN FORM DETAILS
            RS.Open();
            SQLST.CommandText = "Select * from Forms where FormID = @FormID";
            SQLST.Parameters.AddWithValue("FormID", FID.ToString());
            SQLST.CommandType = CommandType.Text;
            SQLST.Connection = RS;
            SqlDataReader theSqlDataReader = SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();

                // BUILD FORM
                if (showDetails == true)
                {
                    frmHTML += "<div class=\"row\"><div class=\"col-sm-12 col-md-12 col-lg-12 h4\">" + theSqlDataReader["name"].ToString() + "</div></div><div class=\"row\"><div class=\"col-sm-12 col-md-12 col-lg-12\">" + theSqlDataReader["body"].ToString() + "</div></div>";
                }

                // LOAD THE FIELDS
                RS2.Open();
                SQLST2.CommandText = "Select * from FormFields where FormID = @FormID and isActive = '1' order by orderby,formid";
                SQLST2.Parameters.AddWithValue("FormID", theSqlDataReader["FormID"].ToString());
                SQLST2.CommandType = CommandType.Text;
                SQLST2.Connection = RS2;
                SqlDataReader theSqlDataReader2 = SQLST2.ExecuteReader();

                if (theSqlDataReader2.HasRows)
                {
                    while (theSqlDataReader2.Read())
                    {
                        // CHECK IF THE FIELD HAS A REFIX NOTICE
                        string btnNoticeShow = "";
                        RS3.Open();
                        SQLST3.CommandText = "select * from COCRefixFields where COCStatementID = @COCStatementID and TypeID = @TypeID and FieldID = @FieldID";
                        SQLST3.Parameters.AddWithValue("COCStatementID", cocid);
                        SQLST3.Parameters.AddWithValue("TypeID", typeid);
                        SQLST3.Parameters.AddWithValue("FieldID", theSqlDataReader2["FieldID"].ToString());
                        SQLST3.CommandType = CommandType.Text;
                        SQLST3.Connection = RS3;
                        SqlDataReader theSqlDataReader3 = SQLST3.ExecuteReader();

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
                        SQLST3.Parameters.RemoveAt(0);
                        SQLST3.Parameters.RemoveAt(0);
                        SQLST3.Parameters.RemoveAt(0);
                        RS3.Close();
                        
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
                                        radiooptions += "<input type=\"radio\" value=\"" + item + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\" id=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\">" +
                                                        "  <label for=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\">" + item + "</label>";

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
                                       "         <input id=\"" + theSqlDataReader2["name"].ToString() + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\" class=\"" + theSqlDataReader2["class"].ToString() + " " + required + "\" type=\"text\" placeholder=\"\" />" +
                                       "     </div>" +
                                       " </div></div>" + btnNoticeShow + "</div>";

                        }
                        else if (theSqlDataReader2["type"].ToString() == "img") // IMG
                        {
                            // BUILD THE HTML
                            frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\">" +
                                       " <label class=\"col-sm-12 control-label\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                       " <div class=\"col-sm-12\">" +
                                       "         <input type=\"file\" id=\"uploadFileName" + theSqlDataReader2["name"].ToString() + "\" />" +
                                        "        <div onclick=\"uploadFile('" + theSqlDataReader2["name"].ToString() + "')\" class=\"btn btn-primary\">Upload</div>" +
                                        "        <div class=\"alert alert-default loadimg_api\" id=\"img_div_" + theSqlDataReader2["name"].ToString() + "\">No image selected</div>" +
                                       " </div>" + btnNoticeShow + "</div>";

                        }
                        else if (theSqlDataReader2["type"].ToString() == "area") // TEXTAREA
                        {
                            // BUILD THE HTML
                            frmHTML += "<div class=\"form-group\" id=\"d_" + theSqlDataReader2["name"].ToString() + "\">" +
                                       " <label for=\"name\" class=\"col-sm-12 control-label\">" + theSqlDataReader2["label"].ToString() + "</label>" +
                                       " <div class=\"col-sm-12 p-b-10\">" +
                                       "     <textarea class=\"" + theSqlDataReader2["class"].ToString() + "\" id=\"" + theSqlDataReader2["name"].ToString() + "\" name=\"" + theSqlDataReader2["name"].ToString() + "\" placeholder=\"\"></textarea>" +
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
                                        radiooptions += "<input type=\"checkbox\" name=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\" id=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\" value=\"true\" class=\"checkbox check-success\" ><label for=\"" + theSqlDataReader2["name"].ToString() + "-" + replaceitemname(item.ToString()) + "\">" + item + "</label>";
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
                SQLST2.Parameters.RemoveAt(0);
                RS2.Close();

            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            SQLST.Parameters.RemoveAt(0);
            RS.Close();

            // BUTTONS
            //frmHTML += "</div>" +
            //               "<div class=\"form-group\">" +
            //               "     <div class=\"col-sm-12 col-md-12 col-lg-12 text-center\">" +
            //               "         <div id=\"resultbox\" class=\"alert-danger hide\"></div>" +
            //               "         <label class=\"btn btn-success\" onclick=\"saveFRM()\">Save Form</label>" +
            //               "         <label class=\"btn btn-default\" onclick=\"javascript:history.go(-1);\">Cancel</label>" +
            //               "     </div>" +
            //               "</div>" +
            //               "</div></div>";

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

            return frmHTML;
        }

        public DataSet ConvertJsonToObject(string json)
        {
            return JsonConvert.DeserializeObject<DataSet>(json);
        }

        public string ConvertDataTableToHTML(DataTable dt)
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
        
        public string replaceitemname(string itemname)
        {
            return itemname;
        }
        
        // ****************************************************************************************
        // SMS -- SMS PORTAL HTTP/API
        // ****************************************************************************************
        public string sendSMS(string UserID, string NumberTo, string smsMessage)
        {
            // INSERT INTO SMS LOG
            DB_Connect();

            string html = string.Empty;
            string url = @"http://www.mymobileapi.com/api5/http5.aspx?Type=sendparam&username=PIRB%20Registration&password=Plumber&numto=" + NumberTo + "&data1=" + smsMessage;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            RS3.Open();
            SQLST3.CommandText = "insert into SMSLog (UserID, SMSMessage, NumberTo,APIResponse) values (@UserID, @SMSMessage, @NumberTo,@APIResponse)";
            SQLST3.Parameters.AddWithValue("UserID", UserID);
            SQLST3.Parameters.AddWithValue("SMSMessage", smsMessage);
            SQLST3.Parameters.AddWithValue("NumberTo", NumberTo);
            SQLST3.Parameters.AddWithValue("APIResponse", html);
            SQLST3.CommandType = CommandType.Text;
            SQLST3.Connection = RS3;
            SqlDataReader theSqlDataReader3 = SQLST3.ExecuteReader();
            
            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            SQLST3.Parameters.RemoveAt(0);
            SQLST3.Parameters.RemoveAt(0);
            SQLST3.Parameters.RemoveAt(0);
            SQLST3.Parameters.RemoveAt(0);
            RS3.Close();

            DB_Close();
            
            return html;

        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public IEnumerable<DateTime> EachMonth(DateTime from, DateTime thru)
        {
            for (var month = from.Date; month.Date <= thru.Date; month = month.AddMonths(1))
                yield return month;
        }

        public DateTime FirstDayOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public DateTime LastDayOfMonth(DateTime dateTime)
        {
            DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

    }
}