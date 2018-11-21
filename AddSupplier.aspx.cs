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
using System.IO;

namespace InspectIT
{
    public partial class AddSupplier : System.Web.UI.Page
    {
        public int MaxNoCert = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();

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
            if (!IsPostBack)
            {


                DLdb.DB_Connect();

                int NoStockCertificates = 0;

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select count(*) as Total from COCStatements where isstock = '1'";
                DLdb.SQLST.Parameters.AddWithValue("Param", "val");
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["Total"] != DBNull.Value && theSqlDataReader["Total"].ToString() != "0")
                        {
                            NoStockCertificates = Convert.ToInt32(theSqlDataReader["Total"]);
                            MaxNoCert = NoStockCertificates;
                            successmsg.InnerHtml = "There are " + MaxNoCert.ToString() + " available stock certificates to assign.";
                            successmsg.Visible = true;
                        }
                        else
                        {
                            NoStockCertificates = 0;
                            errormsg.InnerHtml = "There is no stock avaliable to supply paper certificates. please create stock first.";
                            errormsg.Visible = true;
                            btn_add.Enabled = false;
                        }
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                StartRange.Items.Clear();
                StartRange.Items.Add(new ListItem("", ""));

                EndRange.Items.Clear();
                EndRange.Items.Add(new ListItem("", ""));

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCStatements where isstock = '1'";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        StartRange.Items.Add(new ListItem(theSqlDataReader["COCStatementID"].ToString(), theSqlDataReader["COCStatementID"].ToString()));
                        EndRange.Items.Add(new ListItem(theSqlDataReader["COCStatementID"].ToString(), theSqlDataReader["COCStatementID"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                DLdb.DB_Close();
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (Password.Text.ToString() == PasswordConfirm.Text.ToString())
            {
                Global DLdb = new Global();
                errormsg.Visible = false;

                string SupplierID = "";
                //int sRange = Convert.ToInt32(StartRange.SelectedValue);
                //int eRange = Convert.ToInt32(EndRange.SelectedValue);
                //int curAmount = 0;

                //for (int i = sRange; i < eRange; i++)
                //{
                //    curAmount++;
                //}

                //NoCertificates.Text = curAmount.ToString();
                
                DLdb.DB_Connect();
                
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "INSERT INTO Suppliers (SupplierName, SupplierRegNo, SupplierWebsite, SupplierEmail, SupplierContactNo," +
                    "AddressLine1, AddressLine2, Province, CitySuburb, AreaCode, PostalAddress, PostalCity, PostalCode,TotalNumber,StartRange,EndRange,InvoiceNumber,PostalProvince)" +

                    "VALUES (@SupplierName, @SupplierRegNo, @SupplierWebsite, @SupplierEmail, @SupplierContactNo," +
                    "@AddressLine1, @AddressLine2, @Province, @CitySuburb, @AreaCode, @PostalAddress, @PostalCity, @PostalCode,@TotalNumber,@StartRange,@EndRange,@InvoiceNumber,@PostalProvince) Select Scope_Identity() as SupplierID";

                DLdb.SQLST.Parameters.AddWithValue("SupplierName", SupplierName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SupplierRegNo", SupplierRegNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SupplierWebsite", SupplierWebsite.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SupplierEmail", SupplierEmail.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SupplierContactNo", SupplierContactNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressLine1", AddressLine1.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressLine2", AddressLine2.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Province", Province.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CitySuburb", CitySuburb.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AreaCode", AreaCode.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PostalAddress", PostalAddress.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PostalCity", PostalCity.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PostalCode", PostalCode.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("StartRange",  "0");
                DLdb.SQLST.Parameters.AddWithValue("EndRange", "0");
                DLdb.SQLST.Parameters.AddWithValue("TotalNumber", "0");
                DLdb.SQLST.Parameters.AddWithValue("InvoiceNumber", InvoiceNumber.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PostalProvince", postalprovince.SelectedValue.ToString());

                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    SupplierID = theSqlDataReader["SupplierID"].ToString();
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);

                DLdb.RS.Close();
                
                string UserID = "";

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "INSERT INTO Users (fname,lname, password, role, email,NoCOCpurchases,contact) VALUES (@fname, @lname, @password, @role, @email,@NoCOCpurchases,@contact); Select Scope_Identity() as UserID";

                string pass = Password.Text.ToString();

                // General Details
                DLdb.SQLST.Parameters.AddWithValue("fname", fName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("lname", lName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(pass));
                DLdb.SQLST.Parameters.AddWithValue("role", "Supplier");
                DLdb.SQLST.Parameters.AddWithValue("email", email.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("contact", SupplierContactNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("NoCOCpurchases", nonloggedcocallocated.Text.ToString());

                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    UserID = theSqlDataReader["UserID"].ToString();
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                //Update userid in auditors table
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update Suppliers set UserID = @UserID where SupplierID = @SupplierID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                DLdb.SQLST.Parameters.AddWithValue("SupplierID", SupplierID);

                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update users set password = @password where UserID = @UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(Password.Text.ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                // CREATE ORDER
                //var OrderID = "";
                //DLdb.RS.Open();
                //DLdb.SQLST.CommandText = "INSERT INTO Orders (SupplierID,UserID,Description,TotalNoItems,SubTotal,Vat,Delivery,Total,COCType,Method,isPaid,StartRange,EndRange) values (@SupplierID,@UserID,@Description,@TotalNoItems,@SubTotal,@Vat,@Delivery,@Total,@COCType,@Method,'1',@StartRange,@EndRange); Select Scope_Identity() as OrderID";

                //DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                //DLdb.SQLST.Parameters.AddWithValue("SupplierID", SupplierID);
                //DLdb.SQLST.Parameters.AddWithValue("Description", "COC Purchase");
                //DLdb.SQLST.Parameters.AddWithValue("TotalNoItems", curAmount);
                //DLdb.SQLST.Parameters.AddWithValue("SubTotal", (curAmount * 120));
                //DLdb.SQLST.Parameters.AddWithValue("Vat", "");
                //DLdb.SQLST.Parameters.AddWithValue("Delivery", "");
                //DLdb.SQLST.Parameters.AddWithValue("Total", (curAmount * 120));
                //DLdb.SQLST.Parameters.AddWithValue("COCType", "Paper");
                //DLdb.SQLST.Parameters.AddWithValue("Method", "Delivery at Supplier");
                //DLdb.SQLST.Parameters.AddWithValue("StartRange", StartRange.SelectedValue);
                //DLdb.SQLST.Parameters.AddWithValue("EndRange", EndRange.SelectedValue);

                //DLdb.SQLST.CommandType = CommandType.Text;
                //DLdb.SQLST.Connection = DLdb.RS;
                //theSqlDataReader = DLdb.SQLST.ExecuteReader();

                //if (theSqlDataReader.HasRows)
                //{
                //    theSqlDataReader.Read();
                //    OrderID = theSqlDataReader["OrderID"].ToString();
                //}

                //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.SQLST.Parameters.RemoveAt(0);
                //DLdb.RS.Close();

                //var createPDF = "";
                //// GET THE PERIOD YY AND MM
                //string srtMM = DateTime.Now.Month.ToString();
                //string srtYY = DateTime.Now.Year.ToString();

                //string SupAddress = AddressLine1.Text.ToString() + ", " + AddressLine2.Text.ToString() + ", " + Province.SelectedValue.ToString() + ", " + CitySuburb.Text.ToString() + ", " + AreaCode.Text.ToString();

                //// CREATE THE PDF INVOICE
                //createPDF = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
                //                            "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                //                            "        <tr>" +
                //                            "            <td>" +
                //                            "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                //                            "                    <tr>" +
                //                            "                    <td align='center' colspan='2' width='60%'><img src='https://197.242.82.242/inspectit/assets/img/cardlogo.jpg'/></td>" +
                //                            "                    <td align='left' width='40%'><font style='font-size:26px;'><b>INVOICE No. : " + InvoiceNumber.Text.ToString()  + "</b><br />Period: </b>" + srtMM + "-" + srtYY + "</font></td></tr>" +
                //                            "                    </tr>" +
                //                            "                    <tr>" +
                //                            "                        <td align='left' width='70%'><br /><h4></h4>" + SupplierName.Text.ToString() + "<br/>" + SupplierContactNo.Text.ToString() + "<br/>" + SupplierEmail.Text.ToString() + "</td>" +
                //                            "                        <td align='left' width='30%' colspan='2'><br /><h4>Address :</h4>" + SupAddress.ToString() + "</td>" +
                //                            "                    </tr>" +
                //                            "                    <tr>" +
                //                            "                        <td align='left' width='70%'><br /><h4>Paper bases COC Statements</h4></td>" +
                //                            "                    </tr>" +
                //                            "                    <tr>" +
                //                            "                        <td align='left' colspan='3' valign='top'>" +
                //                            "                            <table border='0' cellpadding='5px' cellspacing='0' width='100%'>" +
                //                            "                               <tr>" +
                //                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>COC Type</b></td>" +
                //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>Collection Method</b></td>" +
                //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>Number of COC's</b></td>" +
                //                            "                                </tr>" +
                //                            "                               <tr>" +
                //                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>Paper</td>" +
                //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>Delivary</td>" +
                //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + curAmount + "</td>" +
                //                            "                                </tr>" +

                //                            "                               <tr>" +
                //                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
                //                            "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>Total Amount<b></td>" +
                //                            "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + (curAmount * 120).ToString() + "</b></td>" +
                //                            "                                </tr>" +
                //                            "                            </table>" +
                //                            "                        </td>" +
                //                            "                    </tr>" +
                //                            "                    <tr>" +
                //                            "                       <td align='middle'><b>Powered By InspectIT</b></td>" +
                //                            "                        <td><td align='left'><img src='https://197.242.82.242/inspectit/assets/img/logo.png'/></td>" +
                //                            "                    </tr>" +
                //                            "                </table>" +
                //                            "            </td>" +
                //                            "        </tr>" +
                //                            "    </table>" +
                //                            "</body>");

                //string filename = "invoice_" + OrderID + "_" + srtMM + "-" + srtYY + ".pdf";
                //var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(createPDF);
                //string path = Server.MapPath("~/invoices/") + filename;
                //File.WriteAllBytes(path, pdfBytes);

                //DLdb.RS3.Open();
                //DLdb.SQLST3.CommandText = "update Orders set PDFName=@PDFName where OrderID=@OrderID";
                //DLdb.SQLST3.Parameters.AddWithValue("OrderID", OrderID);
                //DLdb.SQLST3.Parameters.AddWithValue("PDFName", filename);

                //DLdb.SQLST3.CommandType = CommandType.Text;
                //DLdb.SQLST3.Connection = DLdb.RS3;
                //SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                //if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                //DLdb.SQLST3.Parameters.RemoveAt(0);
                //DLdb.SQLST3.Parameters.RemoveAt(0);
                //DLdb.RS3.Close();

                //if (curAmount > 0)
                //{
                //    // EMAIL THE USER DETAILS
                //    string HTMLSubject = "Welcome to Inspect IT.";
                //    string HTMLBody = "Dear " + fName.Text.ToString() + "<br /><br />Welcome to Inspect IT<br /><br />Your login details are;<br />Email Address: " + email.Text.ToString() + "<br />Password: " + pass.ToString() + "<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Administrator";
                //    DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", email.Text.ToString(), filename);

                //}
                //else
                //{
                    // EMAIL THE USER DETAILS
                    string HTMLSubject = "Welcome to Inspect IT.";
                    string HTMLBody = "Dear " + fName.Text.ToString() + "<br /><br />Welcome to Inspect IT<br /><br />Your login details are;<br />Email Address: " + email.Text.ToString() + "<br />Password: " + pass.ToString() + "<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Administrator";
                    DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", email.Text.ToString(), "");

               // }


                DLdb.DB_Close();
                Response.Redirect("ViewSupplier.aspx?msg=" + DLdb.Encrypt("Supplier has been added successfully"));
            }
            else
            {
                errormsg.InnerHtml = "Password does not match";
                errormsg.Visible = true;
            }


        }
    }
}