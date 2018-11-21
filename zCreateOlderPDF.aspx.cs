using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InspectIT
{
    public partial class zCreateOlderPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string AuditorID = "";
            string AuditorIDUSerID = "";
            string NumberTo = "";
            string EmailAddress = "";
            string FullName = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCStatements where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", Request.QueryString["cocid"]);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "select * from Customers where CustomerID = @CustomerID";
                DLdb.SQLST2.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                if (theSqlDataReader2.HasRows)
                {
                    theSqlDataReader2.Read();
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "select * from Area where Name = @Name";
                    DLdb.SQLST3.Parameters.AddWithValue("Name", theSqlDataReader2["AddressSuburb"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();
                    if (theSqlDataReader3.HasRows)
                    {
                        theSqlDataReader3.Read();
                        DLdb.RS4.Open();
                        DLdb.SQLST4.CommandText = "select * from AuditorAreas where AreaID = @AreaID";
                        DLdb.SQLST4.Parameters.AddWithValue("AreaID", theSqlDataReader3["ID"].ToString());
                        DLdb.SQLST4.CommandType = CommandType.Text;
                        DLdb.SQLST4.Connection = DLdb.RS4;
                        SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();
                        if (theSqlDataReader4.HasRows)
                        {
                            theSqlDataReader4.Read();
                            AuditorID = theSqlDataReader4["AuditorID"].ToString();
                            DLdb.RS5.Open();
                            DLdb.SQLST5.CommandText = "select * from Auditor where AuditorID = @AuditorID";
                            DLdb.SQLST5.Parameters.AddWithValue("AuditorID", theSqlDataReader4["AuditorID"].ToString());
                            DLdb.SQLST5.CommandType = CommandType.Text;
                            DLdb.SQLST5.Connection = DLdb.RS5;
                            SqlDataReader theSqlDataReader5 = DLdb.SQLST5.ExecuteReader();
                            if (theSqlDataReader5.HasRows)
                            {
                                theSqlDataReader5.Read();
                                AuditorIDUSerID = theSqlDataReader5["UserID"].ToString();
                                NumberTo = theSqlDataReader5["phoneMobile"].ToString();
                                EmailAddress = theSqlDataReader5["email"].ToString();
                                FullName = theSqlDataReader5["fname"].ToString() + " " + theSqlDataReader5["lname"].ToString();
                            }
                            if (theSqlDataReader5.IsClosed) theSqlDataReader5.Close();
                            DLdb.SQLST5.Parameters.RemoveAt(0);
                            DLdb.RS5.Close();
                        }
                        if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                        DLdb.SQLST4.Parameters.RemoveAt(0);
                        DLdb.RS4.Close();
                    }
                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();
                }
                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            // UPDATE THE COC

           
            string WorkCompletedby = "";
            
            // ****************************************************************************************
            // BUILD THE PDF COC
            // ****************************************************************************************

            // CUSTOMERID
            string CustomerID = "";
            string gUID = "";
            string emailaddress = "";
            string name = "";
            string aOrBs = "";
            string descOfWorkg = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from COCStatements where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", Request.QueryString["cocid"]);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    CustomerID = theSqlDataReader["CustomerID"].ToString();
                    gUID = theSqlDataReader["UserID"].ToString();
                    aOrBs = theSqlDataReader["AorB"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from COCStatementDetails where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", Request.QueryString["cocid"]);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    descOfWorkg = theSqlDataReader["DescriptionofWork"].ToString();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // GET USER DETAILS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", gUID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                name = theSqlDataReader["fname"].ToString() + ' ' + theSqlDataReader["lname"].ToString();
                emailaddress = theSqlDataReader["email"].ToString();

            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // GET CLIENT DETAILS
            string clientname = "";
            string clientaddress = "";
            string Clientemail = "";
            string ClientTel = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Customers where CustomerID = @CustomerID";
            DLdb.SQLST.Parameters.AddWithValue("CustomerID", CustomerID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                clientname = theSqlDataReader["CustomerName"].ToString() + ' ' + theSqlDataReader["CustomerSurname"].ToString();
                clientaddress = theSqlDataReader["AddressStreet"].ToString() + "<br />" + theSqlDataReader["AddressSuburb"].ToString() + "<br />" + theSqlDataReader["AddressCity"].ToString() + "<br />" + theSqlDataReader["Province"].ToString();
                Clientemail = theSqlDataReader["CustomerEmail"].ToString();
                ClientTel = theSqlDataReader["CustomerCellNo"].ToString();

            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // GET USER DETAILS
            string uname = "";
            string uemail = "";
            string ucompany = "";
            string usignature = "";
            string uaddress = "";
            string ucontact = "";
            string uregno = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select top 1 * from Users where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", gUID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                uname = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                uemail = theSqlDataReader["email"].ToString();
                ucompany = theSqlDataReader["company"].ToString();
                usignature = "<img src=\"http://197.242.82.242/pirbreg/signatures/" + theSqlDataReader["signature"].ToString() + "\" />";
                uaddress = theSqlDataReader["ResidentialStreet"].ToString() + " " + theSqlDataReader["ResidentialSuburb"].ToString() + " " + theSqlDataReader["ResidentialCity"].ToString() + "  " + theSqlDataReader["Province"].ToString() + " " + theSqlDataReader["ResidentialCode"].ToString();
                ucontact = theSqlDataReader["fname"].ToString();
                uregno = theSqlDataReader["regno"].ToString();
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // GET ALL ELEMENTS
            string EmailHTML = "";
            string EmailSub = "";
            var html_FIELD_Content = "";

            // BUILD CLIENT DETAILS
            string clientdetails = "<div style='border:1px solid #EFEFEF;padding:10px;width:100%;'><b>Customer Name: </b>" + clientname + "<br />";
            clientdetails += "<b>Email Address: </b>" + Clientemail + "<br />";
            clientdetails += "<b>Tel No.: </b>" + ClientTel + "<br />";
            clientdetails += "<b>Address: </b>" + clientaddress + "<br /></div>";

            // BUILD THE PDF FILENAME
            DateTime cDate = DateTime.Now;
            string filename = cDate.ToString("ddMMyyyy") + "_" + gUID + "_COC_" + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + ".pdf";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCInstallations where COCStatementID = @COCStatementID and isActive = '1'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", Request.QueryString["cocid"]);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    // JSON //
                    string json = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select TOP 1 * from FormUserData where DataID = @DataID order by createdate desc";
                    DLdb.SQLST2.Parameters.AddWithValue("DataID", theSqlDataReader["DataID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            json = theSqlDataReader2["json"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    // GET FORM ID
                    string frmid = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from [dbo].[FormLinks] where TypeID = @TypeID and FormType = 'COC'";
                    DLdb.SQLST2.Parameters.AddWithValue("TypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            frmid = theSqlDataReader2["FormID"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    //FORM DETAILS
                    string frmName = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from Forms where FormID = @FormID";
                    DLdb.SQLST2.Parameters.AddWithValue("FormID", frmid.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            frmName = theSqlDataReader2["Name"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    // PREP FIELD BUILDER
                    //html_FIELD_Content += "<table border='0' cellpadding='10px' cellspacing='0' width='80%'><tr>" +
                    //                         "  <td align='left' colspan='2' style=\"border: 1px solid #E5E5E5;\">" +
                    //                         "      <h3 style='padding-top:10px;margin;0px;'><b>" + frmName + "</b></h3>" +
                    //                         "  </td>" +
                    //                         "</tr>";

                    //// Convert DATA to TABLE
                    //DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                    //string json_HTML = DLdb.ConvertDataTableToHTML(dt);
                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    // GET THE FORM AND BUILD THE HTML FOR PDF
                    //    DLdb.RS2.Open();
                    //    DLdb.SQLST2.CommandText = "select * from formfields where FormID = '" + frmid + "' and name = '" + row["name"].ToString() + "' and isactive = '1' order by orderby";
                    //    DLdb.SQLST2.CommandType = CommandType.Text;
                    //    DLdb.SQLST2.Connection = DLdb.RS2;
                    //    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    //    if (theSqlDataReader2.HasRows)
                    //    {
                    //        while (theSqlDataReader2.Read())
                    //        {
                    //            string typ = theSqlDataReader2["type"].ToString();

                    //            try
                    //            {
                    //                // check type
                    //                if (theSqlDataReader2["type"].ToString() == "head")
                    //                {
                    //                    html_FIELD_Content += "<tr>" +
                    //                                          "  <td align='left' colspan='2' style=\"border: 1px solid #E5E5E5;\">" +
                    //                                          "      <h3 style='padding-top:10px;margin;0px;'><b>" + theSqlDataReader2["label"].ToString() + "</b></h3>" +
                    //                                          "  </td>" +
                    //                                          "</tr>";
                    //                }
                    //                else if (theSqlDataReader2["type"].ToString() == "select")
                    //                {

                    //                    // split the options
                    //                    string fieldvalues = "";
                    //                    string str_options = theSqlDataReader2["options"].ToString();
                    //                    string[] options = str_options.Split(',');
                    //                    foreach (string option in options)
                    //                    {
                    //                        try
                    //                        {
                    //                            string row_field_id = theSqlDataReader2["name"].ToString();// + "-" + DLdb.replaceitemname(option.ToString()).ToString();
                    //                            string nn = row["name"].ToString();
                    //                            string nnval = row["value"].ToString();
                    //                            string row_value = option;
                    //                            if (row_value == nnval)
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
                    //                                          "      <b>" + theSqlDataReader2["label"].ToString() + "</b>" +
                    //                                          "  </td>" +
                    //                                          "  <td align='right' width='30%'>" +
                    //                                          "      " + fieldvalues + "" +
                    //                                          "  </td>" +
                    //                                          "</tr>";
                    //                }
                    //                else if (theSqlDataReader2["type"].ToString() == "radio")
                    //                {

                    //                    // split the options
                    //                    string fieldvalues = "";
                    //                    string str_options = theSqlDataReader2["options"].ToString();
                    //                    string[] options = str_options.Split(',');
                    //                    foreach (string option in options)
                    //                    {
                    //                        try
                    //                        {
                    //                            string row_field_id = theSqlDataReader2["name"].ToString();// + "-" + DLdb.replaceitemname(option.ToString()).ToString();
                    //                            string nn = row["name"].ToString();
                    //                            string nnval = row["value"].ToString();
                    //                            string row_value = option;
                    //                            if (row_value == nnval)
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
                    //                                          "      <b>" + theSqlDataReader2["label"].ToString() + "</b>" +
                    //                                          "  </td>" +
                    //                                          "  <td align='right' width='30%'>" +
                    //                                          "      " + fieldvalues + "" +
                    //                                          "  </td>" +
                    //                                          "</tr>";
                    //                }
                    //                else if (theSqlDataReader2["type"].ToString() == "checkbox")
                    //                {

                    //                    // split the options
                    //                    string fieldvalues = "";
                    //                    string str_options = theSqlDataReader2["options"].ToString();
                    //                    string[] options = str_options.Split(',');
                    //                    foreach (string option in options)
                    //                    {
                    //                        try
                    //                        {
                    //                            string row_field_id = theSqlDataReader2["name"].ToString();// + "-" + DLdb.replaceitemname(option.ToString()).ToString();
                    //                            string nn = row["name"].ToString();
                    //                            string nnval = row["value"].ToString();
                    //                            string row_value = option;
                    //                            if (row_value == nnval)
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
                    //                                          "      <b>" + theSqlDataReader2["label"].ToString() + "</b>" +
                    //                                          "  </td>" +
                    //                                          "  <td align='right' width='30%'>" +
                    //                                          "      " + fieldvalues + "" +
                    //                                          "  </td>" +
                    //                                          "</tr>";
                    //                }
                    //                else if (theSqlDataReader2["type"].ToString() == "img")
                    //                {
                    //                    // GET IMG
                    //                    string imgsrc = "";
                    //                    DLdb.RS3.Open();
                    //                    DLdb.SQLST3.CommandText = "select * from FormImg where FieldID = @photoid and COCID = @COCID and FormID = @FormID";
                    //                    DLdb.SQLST3.Parameters.AddWithValue("COCID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                    //                    DLdb.SQLST3.Parameters.AddWithValue("FormID", frmid);
                    //                    DLdb.SQLST3.Parameters.AddWithValue("photoid", "img_div_" + theSqlDataReader2["name"].ToString());
                    //                    DLdb.SQLST3.CommandType = CommandType.Text;
                    //                    DLdb.SQLST3.Connection = DLdb.RS3;
                    //                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    //                    if (theSqlDataReader3.HasRows)
                    //                    {
                    //                        theSqlDataReader3.Read();
                    //                        imgsrc = "https://197.242.82.242/inspectit/AuditorImgs/" + theSqlDataReader3["imgsrc"].ToString();

                    //                        html_FIELD_Content += "<tr>" +
                    //                                          "  <td align='left' colspan='2'>" +
                    //                                          "      <b>" + theSqlDataReader2["label"].ToString() + "</b><br />" +
                    //                                          "      <img src=\"" + imgsrc + "\" style='width:50%;height:50%;' />" +
                    //                                          "  </td>" +
                    //                                          "</tr>";
                    //                    }

                    //                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    //                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    //                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    //                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    //                    DLdb.RS3.Close();
                    //                }
                    //                else // text and textarea
                    //                {
                    //                    // check blanks and does not exist
                    //                    string nn = row["name"].ToString();
                    //                    string nnval = row["value"].ToString();
                    //                    if (theSqlDataReader2["name"].ToString() == nn)
                    //                    {
                    //                        html_FIELD_Content += "<tr>" +
                    //                                          "  <td align='left' width='70%'>" +
                    //                                          "      <b>" + theSqlDataReader2["label"].ToString() + "</b>" +
                    //                                          "  </td>" +
                    //                                          "  <td align='right' width='30%'>" +
                    //                                          "      " + nnval + "" +
                    //                                          "  </td>" +
                    //                                          "</tr>";
                    //                    }
                    //                }
                    //            }
                    //            catch (Exception err)
                    //            {
                    //                Response.Write(err + "<br /");
                    //                Response.End();
                    //            }
                    //        }

                    //    }

                    //    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    //    DLdb.RS2.Close();

                    //}

                    //html_FIELD_Content += "</table>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string seltype = "";

            if (aOrBs == "A")
            {
                seltype = "                       <tr>" +
                            "                           <td width='10%'>" +
                            "                           <b>A</b>" +
                            "                          </td>" +
                            "                           <td width='90%'>" +
                            "                           <p>The above plumbing work was carried out by me or under my supervision, and that it complies in all respects to the plumbing regulations, laws, National Compulsory Standards and Local by laws.</p>" +
                            "                          </td></tr>" +
                            "                       <tr>";
            }
            else
            {
                seltype = "                       <tr>" +
                            "                           <td width='10%'>" +
                            "                           <b>B</b>" +
                            "                          </td>" +
                            "                           <td width='90%'>" +
                            "                           <p>I have fully inspected and tested the work started but not completed by another Licensed plumber. I further certify that the inspected and tested work and the necessary completion work was carried out by me or under my supervision - complies in all respects to the plumbing regulations, laws, National Compulsory Standards and Local by laws.</p>" +
                            "                          </td></tr>";
            }

            string hotwatersystemstick = "";
            string coldwatersystemstick = "";
            string sanitaryware = "";
            string belowground = "";
            string aboveground = "";
            string rainwater = "";

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCInstallations where COCStatementID = @COCStatementID and isActive = '1'";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", Request.QueryString["cocid"]);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string typeid = theSqlDataReader["TypeID"].ToString();
                    if (typeid.ToString() == "1")
                    {
                        hotwatersystemstick = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }

                    if (typeid.ToString() == "2")
                    {
                        belowground = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }

                    if (typeid.ToString() == "3")
                    {
                        coldwatersystemstick = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }
                    if (typeid.ToString() == "4")
                    {
                        aboveground = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }
                    if (typeid.ToString() == "7")
                    {
                        sanitaryware = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }
                    if (typeid.ToString() == "8")
                    {
                        rainwater = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            //foreach (ListItem item in TypeOfInstallation.Items)
            //{
            //    if (item.Selected)
            //    {
            //        // item.Value.ToString()
            //        if (item.Value.ToString() == "1")
            //        {
            //            hotwatersystemstick = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
            //        }

            //        if (item.Value.ToString() == "2")
            //        {
            //            belowground = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
            //        }

            //        if (item.Value.ToString() == "3")
            //        {
            //            coldwatersystemstick = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"25px;\"/>";
            //        }
            //        if (item.Value.ToString() == "4")
            //        {
            //            aboveground = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
            //        }
            //        if (item.Value.ToString() == "7")
            //        {
            //            sanitaryware = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
            //        }
            //        if (item.Value.ToString() == "8")
            //        {
            //            rainwater = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
            //        }
            //    }
            //}

            var htmlContent = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
                                                "<h2 style='text-align:center;font-size:50px;color:#735b41;'>PLUMBING CERTIFICATE OF COMPLIANCE</h2>" +
                                                "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                "        <tr>" +
                                                "            <td>" +
                                                "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'><tr>" +
                                                "                           <td>" +
                                                "                               <img src='https://197.242.82.242/inspectit/assets/img/cardlogo.jpg' />" +
                                                "                           </td><td>" +
                                                "                               <div style='width:100%;'><div style='width:80%;background-color:#ccc;top:10;float:right;border: 1px solid #E5E5E5;padding:10px'> COC Number: " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " </div ></div ><br /><br /><br />" +
                                                "                               <div style='width:100%;'><div style='width:80%;background-color:pink;top:10;float:right;border: 1px solid #E5E5E5;padding:10px'> ONLY PIRB REGISTERED LICENSED PLUMBERS ARE AUTHORISED TO ISSUE THIS PLUMBING CERTIFICATE OF COMPLIANCE </div></div><br/><br /><br /><br /> " +
                                                "                               <div style='width:100%;'><div style='width:80%;background-color:red;color:white;top:10;float:right;float:right;border: 1px solid #E5E5E5;padding:10px'> TO VERIFY AND AUTHENTICATE THIS CERTIFICATE OF COMPLIANCE VISIT PIRB.CO.ZA AND CLICK ON VERIFY / AUTHENTICATE LINK </div></div> " +
                                                "                           </td></tr></table>" +
                                                "                            <br /><br />" +
                                                //"                        <div>" + clientdetails + "</div>" +
                                                // "                        <div style='width:100%;'><div style='background-color:#ccc;position:absolute;top:10;float:right;border: 1px solid #E5E5E5;padding:10px'>COC Number: " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + "</div></div></td>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                    <td bgcolor='lightgreen'>" +
                                                "                       <h4 style='background-color:lightgreen;text-align:center;'>Physical Address Details of Installation</h4>" +
                                                //"                        <div>" + clientdetails + "</div>" +
                                                "                    </td>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                    <td>" +
                                                "                        " + clientdetails + "" +
                                                "                    </td>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'><tr>" +
                                                "                           <td bgcolor='lightgreen' width='80%'>" +
                                                "                               <h4 style='background-color:lightgreen;text-align:center;'>Type of Installation Carried Out by Licensed Plumber <br /><span>(Clearly tick the appropriate Installation Category Code and complete the installation details below)</span></h4>" +
                                                "                               " +
                                                "                           </td><td width='10%'>" +
                                                "                               <h4 style='text-align:center;'>Code</h4>" +
                                                "                           </td><td width='10%'>" +
                                                "                               <h4 style='text-align:center;'>Tick</h4>" +
                                                "                           </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:red;'> Hot Water System </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" + hotwatersystemstick + "" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:lightblue;'> Cold Water System </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" + coldwatersystemstick + "" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:blue;'> Sanitary-ware and Sanitary-fittings </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" + sanitaryware + "" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:brown;'> Below-ground Drainage System </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" + belowground + "" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:green;'> Above-ground Drainage System </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" + aboveground + "" +
                                                "                          </td></tr>" +
                                                 "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:darkblue;'> Rain Water Disposal System </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" + rainwater + "" +
                                                "                          </td></tr>" +
                                                "                       </table>" +

                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'><tr>" +
                                                "                           <td bgcolor='lightgreen' width='80%'>" +
                                                "                               <h4 style='background-color:lightgreen;text-align:center;'>Specialisations: To be Carried Out by Licensed Plumber Only Registered to do the Specialised Word <br /><span>(To Verify and authenticate Licensed Plumbers specialisations visit pirb.co.za)</span></h4>" +
                                                "                               " +
                                                "                           </td><td width='10%'>" +
                                                "                               <h4 style='text-align:center;'>Code</h4>" +
                                                "                           </td><td width='10%'>" +
                                                "                               <h4 style='text-align:center;'>Tick</h4>" +
                                                "                           </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:orange;'> Solar Water Heating System </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td style='border-bottom: solid 1px black;'>" +
                                                "                               Installation, Replacement and / or Repair of a<span style='color:maroon;'> Heat Pump </span>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                           </td><td style='border-bottom: solid 1px black;'>" +
                                                "                          </td></tr>" +
                                                "                       </table>" +
                                                "                    </tr>" +
                                                "                       <tr><i>See explanations of the above on the reverse of this certificate</i></tr>" +

                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'><tr>" +
                                                "                           <td bgcolor='lightgreen'>" +
                                                "                               <h4 style='background-color:lightgreen;text-align:center;'>Installation Details<br /><span>(Details of the work undertaken or scope of work for which the COC is being issued for)</span></h4>" +
                                                "                           </td></tr>" +
                                                 "                    <tr>" +
                                                "                        <td align='middle'>" +
                                                                           //"                               " + html_FIELD_Content + " " +
                                                                           descOfWorkg.ToString() +
                                                "                        </td>" +
                                                "                    </tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                          </td></tr>" +
                                                "                       </table>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'><tr>" +
                                                "                           <td bgcolor='lightgreen'>" +
                                                "                               <h4 style='background-color:lightgreen;text-align:center;'>Pre-Existing Non Compliance* Conditions<br /><span>(Details of any non-compliance of the pre-existing plumbing installation on which work was done that needs to be brought to the attention of owner/user)</span></h4>" +
                                                "                           </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                          </td></tr>" +
                                                "                       </table>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'>" +
                                                "                       <tr> " +
                                                "                           <td width='10%'>" +
                                                "                          </td>" +
                                                "                           <td width='90%'>" +
                                                "                           <p>I " + uname + " (Licensed Plumber's Name and Surname), Licensed registration number " + uregno + ", certify that, " +
                                                "                                   the above compliance certificate details are true and correct and will be logged in accordance with the prescribed requirements as defined by the PIRB." +
                                                "                                   I further certify that; " +
                                                "                                   <br />Delete either <b>A</b> or <b>B</b> as appropriate</p>" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                       " + seltype + "    " +
                                                "                       <tr>" +
                                                "                           <td width='10%'></td> <td width='90%'>" +
                                                "                               Signed (Licensed Plumber): " + usignature + "" +
                                                "                          </td></tr>" +
                                                "                       </table>" +
                                                "                    </tr><br/><br/><br/><br/><br/><br/><br/><br/><br/>" +
                                                "                    <tr>" +
                                                "                        <table border='1' width='100%'><tr>" +
                                                "                           <td bgcolor='#FF0000'>" +
                                                "                               <h4 style='background-color:red;text-align:center;color:white;'>IMPORTANT NOTICE</h4>" +
                                                "                           </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                               <ul>" +
                                                "                                   <li>An incorrect statement of fact, including an omission, is an offence in terms of the PIRB Code of conduct, and will be subjected to PIRB disciplinary procedures.</li>" +
                                                "                                   <li>A completed Certificate of Compliance must be provided to the owner/consumer within 5 days of the completion of the plumbing works and the details of the Certificate of Compliance must be logged electronically with the PIRB within that period.</li>" +
                                                "                                   <li>The relevant plumbing work that was certified as complaint through the issuing of this certificate may be possibly be audited by a PIRB Auditor for compliance to the regulations, workmanship and health and safety of the plumbing.</li>" +
                                                "                                   <li>If this Certificate of Compliance has been chosen for an audit you must cooperated fully with the PIRB Auditor in allowing them to carry out the relevant audit.</li>" +
                                                "                                   <li>See reverse side of this Certificate of Compliance for further details</li>" +
                                                "                               </ul>" +
                                                "                          </td></tr>" +
                                                "                       </table>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'><tr>" +
                                                "                           <td bgcolor='lightblue'>" +
                                                "                               <h4 style='background-color:lightblue;text-align:center;color:white;'>OWNERS COPY</h4>" +
                                                "                           </td></tr>" +
                                                "                       </table>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'><tr>" +
                                                "                           <td bgcolor='lightgreen'>" +
                                                "                               <h4 style='background-color:lightgreen;text-align:center;'>TERMS & CONDITIONS</h4>" +
                                                "                           </td></tr>" +
                                                "                       </table>" +
                                                "                    </tr>" +
                                                "                    <tr>" +
                                                "                        <table border='0' width='100%'><tr>" +
                                                "                           <td width='50%'>" +
                                                "                           <h4>WHAT IS A PlUMBING CERTIFICATE Of COMPLIANCE (COC)?</h4>" +
                                                "                               <p>A Plumbing COC is a means by which the Plumbing Industry Registration Board (PIRB)" +
                                                "                                    licensed plumber self certifies that their work complies with all the current plumbing regulations" +
                                                "                                    and laws as define by the National Compulsory Standards and Local Bylaws.COC's may" +
                                                "                                    only be purchased, and used by a registered and approved PIRB licensed persons and at" +
                                                "                                    the time of purchase, the COC is captured against the PIRB licensed plumber, and becomes" +
                                                "                                    their responsibility.Upon issuing of a COC the PIRB licensed plumber has to log the relevant" +
                                                "                                    COC into the PIRB's Plumbing audit / data management system within five days.Each day, a" +
                                                "                                    computer random selection of jobs for which a COC has be logged with the PIRB, is selected for" +
                                                "                                    an audit. Upon which a PIRB auditor will be sent out to carry out the audit.If the installation is" +
                                                "                                    found to be incorrect or not up to standard the PIRB licensed plumber will be sent a rectification" +
                                                "                                    notice on which the licensed plumber will have to react within the specified period as by the" +
                                                "                                    auditor.This is usually 5 days.</p>" +
                                                "                          </td>" +
                                                "                           <td width='50%'>" +
                                                "                           <h4>How compliance certificates may be purchased</h4>" +
                                                "                           <p>Compliance Certificates may be purchased by licensed persons or authorized persons through any of the following methods:</p>" +
                                                "                               <ul>" +
                                                "                                   <li><b>Over the counter at the Plumbing Industry Registration Board offices.</b> Purchasers will need to present their current license card.Compliance certificates may only be given on-the spot where payment is by cash, credit card, bank transfer(confirmation required) and or bank cheque.</li>" +
                                                "                                   <li><b>Online: </b>Purchasers should log on to www.pirb.co.za, click on Order COC and follow the prompts.</li>" +
                                                "                                   <li><b>Resellers (merchant): </b>The PIRB Licensed Plumber will need to present his/her current licensed card upon purchasing a compliance certificate from a participating reseller(merchant) outlet. No third parties may purchase from a reseller unless preapproved and verified by the PIRB first.</li>" +
                                                "                               </ul>" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td width='50%'>" +
                                                "                           <h4>JOBS WHICH REQUIRE A COC</h4>" +
                                                "                               <p>COC must be provided to the consumer for all plumbing jobs which fall into one or more of the following categories:</p>" +
                                                "                               <ul>" +
                                                "                                   <li>Where the total value of the work, including materials, labour and VAT, is more than the prescribe value as defined by the PIRB(material costs must be included, regardless of whether the materials were supplied by another person) a certificate must be issued for the following:</li>" +
                                                "                                   <ul>" +
                                                "                                       <li>When an Installation, Replacement and/or Repair of Hot Water Systems and/ or Components is carried out</li>" +
                                                "                                       <li>When an Installation, Replacement and/or Repair of Cold Water Systems and/ or Components is carried out</li>" +
                                                "                                       <li>Installation, Replacement and/or Repair of Sanitary-ware and Sanitary-fittings is carried out.</li>" +
                                                "                                       <li>Installation, Replacement and/or Repair of a Solar Water Heating System</li>" +
                                                "                                       <li>Installation, Replacement and/or Repair of a Below-ground Drainage System</li>" +
                                                "                                       <li>Installation, Replacement and/or Repair of an Above-ground Drainage System</li>" +
                                                "                                       <li>Installation, Replacement and/or Repair of a Rain Water Disposal System</li>" +
                                                "                                       <li>Installation, Replacement and/or Repair of a Heat Pump Water Heating System</li>" +
                                                "                                   </ul>" +
                                                "                                   <li>Any work that requires the installation, replacement and/or repair of any of an electrical / solar hot water cylinder valves or components must have a COC issued to the consumer regardless of the cost.</li>" +
                                                "                               </ul>" +
                                                "                          </td>" +
                                                "                           <td width='50%'>" +
                                                "                           <h4>DISPOSAL OF COMPLIANCE CERTIFICATES</h4>" +
                                                "                           <p>If for any reason, a licensed person does not intend to use a compliance certificate for its intended purpose they should return it to the PIRB office and, if all is found to be in order, a refund could be arranged.If a licensed person has a compliance certificate stolen or loses a compliance certificate, he should report it immediately to the PIRB in the form of a statutory declaration.</p>" +
                                                "                           <h4>THE PURPOSE OF AN AUDIT</h4>" +
                                                "                               <p>Audits are conducted to provide a measure of the standard of the plumbing work being performed across the country.The aim is to ensure a correct and consistent application of the standards is reflected in the work done.</p>" +
                                                "                           <h4>AUDIT PROCESS</h4>" +
                                                "                               <p>A computer random selection of COC for which a compliance certificate has be lodged with the PIRB, is selected for an audit. Audits are conducted by qualified experienced trained plumbers and experts authorized by the PIRB to perform the function.PIRB Plumbing Auditors are registered with the PIRB and carry identification cards.When one of your COC has been selected for an audit you will be contacted by the PIRB Auditor.You will be asked for details of where the work was performed and arrangements will be made by the Auditor with the relevant consumer.You will be requested by the Auditor to attend the audit.</p>" +
                                                "                          </td></tr>" +
                                                "                       <tr>" +
                                                "                           <td width='50%'>" +
                                                "                           <h4>STAGE AT WHICH COC MUST BE COMPLTED</h4>" +
                                                "                               <p>A completed COC must be provided to the consumer within <b>5 days</b> of the completion of the plumbing work and the details of the COC must be logged electronically with the PIRB within that" +
                                                "                                   period. A job is considered to be completed when the plumbing work is practically completed or when plumbing work is capable of being used within an existing system - whichever comes first.</p>" +
                                                "                           <h4>LOGGING COC THROUGH THE SMS SERVICE</h4>" +
                                                "                               <p>Details are to be SMS to 082 934 9334 in the following format:<br /><br /> Your License Registration Number; Compliance Certificate Number; Numeric Code(s) of what work was undertaken; Area code where the work was carried out/ installed Example: 00001 / 75; 123456; 01; 0149.<br /><br /> Incorrectly formatted sms's will be rejected and you will be required to resubmit the details.</p>" +
                                                "                          </td>" +
                                                "                           <td width='50%'>" +
                                                "                           <h4>WHAT HAPPENS IF MY WORK DOES NOT PASS AN AUDIT?</h4>" +
                                                "                           <p>If the audited work is found not to comply, you will be advised of the work requiring attention in the form of a Rectification Notice.You are required to rectify the work in the time period specified by the auditor.This is usually 5 days.The work may then be re-audited.Failure to respond, act or co-operate will result in disciplinary procedures.</p>" +
                                                "                           <h4>IF YOU DISAGREE WITH AN AUDIT RESULT</h4>" +
                                                "                           <p>If you believe that the rectification notice is incorrect, you may contact the PIRB and your objection will be reviewed.Objections must be submitted in writing on the relevant PIRB form, obtainable from PIRB office.</p>" +
                                                "                          </td></tr>" +
                                                //  "                       <tr>" +
                                                "                       </table>" +
                                                "                    </tr><br/><br/><br/><br/><br/><br/><br/><br/><br/>" +

                                                "                    <tr>" +
                                                "                        <table border='1' width='100%'>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                               <h4 style='text-align:center;'>Code</h4>" +
                                                "                           </td>" +
                                                "                           <td bgcolor='lightgreen'>" +
                                                "                               <h4 style='text-align:center;'>Type of Installation Carried Out:</h4>" +
                                                "                           </td>" +
                                                "                       </tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                               <h4 style='text-align:center;color:red;'>01</h4>" +
                                                "                           </td>" +
                                                "                           <td bgcolor='rgba(255,0,0,0.2)'>" +
                                                "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:red;'>Hot Water System and /or Components </span></b><br />(A Certificate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing work carried out on the hot water reticulation system upstream of the pressure regulating valve, which shall include but not be limited to: the pressure regulating valve; an electrical hot water cylinder; all relevant valves and components and all hot water pipe and fittings, and shall end at any of the hot water terminal fittings; but shall exclude any sanitary fittings, solar and heat pump installations. The scope of work and non-compliance on pre-existing installations by others must be clearly noted in the installation details provided overleaf.)</p>" +
                                                "                           </td>" +
                                                "                       </tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                               <h4 style='text-align:center;color:lightblue;'>02</h4>" +
                                                "                           </td>" +
                                                "                           <td bgcolor='rgba(177,226,243,0.2)'>" +
                                                "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:lightblue;'>Cold Water System and /or Components </span></b><br />(A Certificate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing works where work has been carried out on the cold water reticulation system upstream of the municipal metering valve, which shall include but not be limited to: all relevant valves and components relating to the cold water system and all cold water pipe and fittings, and shall end at any of the relevant cold water terminal fittings; but shall exclude any sanitary fittings. The scope of work and any non-compliance pre-existing installations by others must be clearly noted in the installation details provided overleaf.)</p>" +
                                                "                           </td>" +
                                                "                       </tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                               <h4 style='text-align:center;color:blue;'>03</h4>" +
                                                "                           </td>" +
                                                "                           <td bgcolor='rgba(0,0,255,0.2)'>" +
                                                "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:blue;'>Sanitary-ware and Sanitary-fittings </span></b><br />(A Certificate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing works r where work has been carried out on the Sanitary-ware and Sanitary-fittings.The scope of work and any non-compliance pre-existing installations by others must be clearly noted in the installation details provided overleaf.) </p>" +
                                                "                           </td>" +
                                                "                       </tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                               <h4 style='text-align:center;color:orange;'>04</h4>" +
                                                "                           </td>" +
                                                "                           <td bgcolor='rgba(255,164,0,0.2)'>" +
                                                "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:orange;'>Solar Water Heating System</span></b><br />(A Certificate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing works where work has been carried out on the Solar Water Heating System which shall include but not be limited to: the hot water reticulation system upstream of the pressure regulating valve; the pressure regulating valve; if applicable the electrical hot water cylinder; a solar (electrical)hot water cylinder; all relevant valves and components and all hot water pipe and fittings, and shall end at any of the relevant hot water terminal fittings; but shall exclude any sanitary fittings. The scope of work and non-compliance pre-existing installations by others must be clearly noted in the installation details provided overleaf.) <span style='color:red;'>Work can only be undertaken by a Licensed Plumber registered to do this specialised work.</span></p>" +
                                                "                           </td>" +
                                                "                       </tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                               <h4 style='text-align:center;color:brown;'>05</h4>" +
                                                "                           </td>" +
                                                "                           <td bgcolor='rgba(183,41,41,0.2)'>" +
                                                "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:brown;'>Below-ground Drainage System</span></b><br />(A Certificate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing works where work has been carried out on a below- ground drainage system, which shall include but not be limited to: septic tank and French drain installations.The scope of work and any non-compliance pre-existing installations by others must be clearly noted in the installation details provided overleaf.) </p>" +
                                                "                           </td>" +
                                                "                       </tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                               <h4 style='text-align:center;color:green;'>06</h4>" +
                                                "                           </td>" +
                                                "                           <td bgcolor='rgba(0,127,0,0.2)'>" +
                                                "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:green;'>Above-ground Drainage System</span></b><br />(A Certificate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing works where work has been carried out on an above-ground drainage system, which shall include but not be limited to: all internal and external waste water and soil drainage but shall excluded any sanitary ware fixtures.The scope of work and any non-compliance pre-existing installations by others must be clearly noted in the installation details provided overleaf.)</p>" +
                                                "                           </td>" +
                                                "                       </tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                               <h4 style='text-align:center;color:darkblue;'>07</h4>" +
                                                "                           </td>" +
                                                "                           <td bgcolor='rgba(0,0,165,0.2)'>" +
                                                "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:darkblue;'>Rain Water Disposal System</span></b><br />(A Certificate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing works i or where work has been carried out on a rain water disposal system, which shall include but not be limited to: storm water drainage, guttering and flashing.The scope of work and any non-compliance pre-existing installations by others must be clearly noted in the installation details provided overleaf.) </p>" +
                                                "                           </td>" +
                                                "                       </tr>" +
                                                "                       <tr>" +
                                                "                           <td>" +
                                                "                               <h4 style='text-align:center;color:maroon;'>08</h4>" +
                                                "                           </td>" +
                                                "                           <td bgcolor='rgba(130,0,0,0.2)'>" +
                                                "                               <p><b>Installation, Replacement and/or Repair of a <span style='color:maroon;'>Heat Pump</span></b><br />(A Certifcate of Compliance is to be issued for the installation, replacement and/or repair of any plumbing works where work has been carried out on the Heat Pump Water Heating System which shall include but not be limited to: the hot water reticulation system upstream of the pressure regulating valve; the pressure regulating valve; if applicable the electrical hot water cylinder; a heat pump unit; all relevant valves and components and all hot water pipe and fttings, and shall end at any of the relevant hot water terminal fittings; but shall exclude any sanitary fttings. The scope of work and non-compliance pre-existing installations by others must be clearly noted in the installation details provided overleaf.) <span style='color:red;'>Work can only be undertaken by a Licensed Plumber registered to do this specialised work.</span></p>" +
                                                "                           </td>" +
                                                "                       </tr>" +
                                                "                       </table>" +
                                                "                    </tr>" +
                                                //"                    <tr>" +
                                                //"                    <tr>" +
                                                //"                        <td align='bottom'>" +
                                                //"                             <hr><table border='0' width='100%'><tr><td width='50%'><b>Logged by: </b>" + uname + "<br />" +
                                                //"                             <b>Email Address: </b>" + uemail + "<br />" +
                                                //"                             <b>Company: </b>" + ucompany + "<br />" +
                                                //"                             <b>Address: </b>" + uaddress + "<br />" +
                                                //"                             <b>Contact Number: </b>" + ucontact + "<br />" +
                                                //"                             <b>PIRB Reg No.: </b>" + uregno + "</td><td width='50%' valign='top'><b>Signature</b><br />" + usignature + "<br /></td></tr></table>" +
                                                //"                        </td>" +
                                                //"                    </tr>" +
                                                "                </table>" +
                                                "            </td>" +
                                                "        </tr>" +
                                                "    </table>" +
                                                "</body>");

            var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(htmlContent);
            string path = Server.MapPath("~/pdf/") + filename;
            File.WriteAllBytes(path, pdfBytes);

            // EMAIL THE NEW USERS

            //if (isPaper == true)
            //{
            //    string HTMLBody = "Dear " + clientname + "<br /><br />Thank you for completing your C.O.C.<br /><br /><br />Regards<br />System Administrator";
            //    string HTMLSubject = "C.O.C Certificate";
            //    string targetAddress = Clientemail;
            //    DLdb.sendEmail(HTMLBody, HTMLSubject, "mathew@slugg.co.za", targetAddress, "");
            //}
            //else
            //{
            //    string HTMLBody = "Dear " + clientname + "<br /><br />Thank you for completing your C.O.C.<br /><br />Please see the attached certificate<br /><br />Regards<br />System Administrator";
            //    string HTMLSubject = "C.O.C Certificate";
            //    string targetAddress = Clientemail;
            //    DLdb.sendEmail(HTMLBody, HTMLSubject, "mathew@slugg.co.za", targetAddress, path);
            //}


            // ADD THE COC TO THE TABLE
            DLdb.RS.Open();
            DLdb.SQLST.Parameters.Clear();
            DLdb.SQLST.CommandText = "update COCStatements Set COCFilename = @COCFilename where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCFilename", filename);
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", Request.QueryString["cocid"]);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            

            Response.Redirect("pdf/" + filename + "");


            DLdb.DB_Close();
        }
    }
}