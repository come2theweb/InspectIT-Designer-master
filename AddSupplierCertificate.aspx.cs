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
    public partial class AddSupplierCertificate : System.Web.UI.Page
    {
        public int MaxNoCert = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

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

            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                btn_add.Visible = false;
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {

            }
            if (!IsPostBack)
            {
                int NoStockCertificates = 0;

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select count(*) as Total from COCStatements where isstock = '1' and userid = '0' and SupplierID = '0'";
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
                            //btn_add.Enabled = false;
                        }
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                StartRange.Items.Clear();
                StartRange.Items.Add(new ListItem("", ""));

                EndRange.Items.Clear();
                EndRange.Items.Add(new ListItem("", ""));

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCStatements where isstock = '1' and userid = '0' and SupplierID = '0'";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        StartRange.Items.Add(new ListItem(theSqlDataReader["COCNumber"].ToString(), theSqlDataReader["COCStatementID"].ToString()));
                        EndRange.Items.Add(new ListItem(theSqlDataReader["COCNumber"].ToString(), theSqlDataReader["COCStatementID"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                // LOAD SUPPLIERS
                SupplierID.Items.Clear();
                SupplierID.Items.Add(new ListItem("", ""));

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Suppliers where isActive = '1' order by SupplierName";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        SupplierID.Items.Add(new ListItem(theSqlDataReader["SupplierName"].ToString(), theSqlDataReader["SupplierID"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                DLdb.DB_Close();
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            
            Global DLdb = new Global();
            DLdb.DB_Connect();
            errormsg.Visible = false;

            //string SupplierID = DLdb.Decrypt(Request.QueryString["sid"]);
            int sRange = Convert.ToInt32(StartRange.SelectedValue);
            int eRange = Convert.ToInt32(EndRange.SelectedValue);
            int curAmount = 1;

            for (int i = sRange; i < eRange; i++)
            {
                curAmount++;
            }

            NoCertificates.Text = curAmount.ToString();
            string UserID = "";
            string AddressLine2 = "";
            string AddressLine1 = "";
            string Province = "";
            string CitySuburb = "";
            string AreaCode = "";
            string SupplierName = "";
            string SupplierContactNo = "";
            string SupplierEmail = "";
            string fName = "";
            string email = "";
            int supplierStart =0;
            int supplierEnd = 0;
            int supplierTotal = 0;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Suppliers where SupplierID=@SupplierID";
            DLdb.SQLST.Parameters.AddWithValue("SupplierID", SupplierID.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                UserID = theSqlDataReader["UserID"].ToString();
                AddressLine1 = theSqlDataReader["AddressLine1"].ToString();
                AddressLine2 = theSqlDataReader["AddressLine2"].ToString();
                Province = theSqlDataReader["Province"].ToString();
                CitySuburb = theSqlDataReader["CitySuburb"].ToString();
                AreaCode = theSqlDataReader["AreaCode"].ToString();
                SupplierName = theSqlDataReader["SupplierName"].ToString();
                SupplierContactNo = theSqlDataReader["SupplierContactNo"].ToString();
                SupplierEmail = theSqlDataReader["SupplierEmail"].ToString();
                supplierTotal = Convert.ToInt32(theSqlDataReader["TotalNumber"].ToString());
                supplierEnd = Convert.ToInt32(theSqlDataReader["StartRange"].ToString());
                supplierStart = Convert.ToInt32(theSqlDataReader["EndRange"].ToString());
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            supplierTotal = supplierTotal + Convert.ToInt32(NoCertificates.Text.ToString());
            supplierStart = supplierStart + Convert.ToInt32(StartRange.Text.ToString());
            supplierEnd = supplierEnd + Convert.ToInt32(EndRange.Text.ToString());

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update Suppliers set TotalNumber=@TotalNumber,StartRange=@StartRange,EndRange=@EndRange,InvoiceNumber=@InvoiceNumber where SupplierID=@SupplierID";
            //DLdb.SQLST.Parameters.AddWithValue("TotalNumber", supplierTotal.ToString());
            //DLdb.SQLST.Parameters.AddWithValue("StartRange", supplierStart.ToString());
            //DLdb.SQLST.Parameters.AddWithValue("EndRange", supplierEnd.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TotalNumber", NoCertificates.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("StartRange", StartRange.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("EndRange", EndRange.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("InvoiceNumber", InvoiceNumber.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("SupplierID", SupplierID.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
           theSqlDataReader = DLdb.SQLST.ExecuteReader();
            
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                fName = theSqlDataReader["fname"].ToString();
                email = theSqlDataReader["email"].ToString();
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            // ASSIGN CERTIFICATES
           
            for (int i = sRange; i < eRange; i++)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Update COCStatements set SupplierID = @SupplierID,DatePurchased=getdate(), isStock = '1' where COCStatementID = @COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("SupplierID", SupplierID.SelectedValue);
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", i);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
                DLdb.SQLST.Parameters.AddWithValue("Message", "Certificate assigned to supplier: " + SupplierName);
                DLdb.SQLST.Parameters.AddWithValue("Username", Session["IIT_UName"].ToString());
                DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
                DLdb.SQLST.Parameters.AddWithValue("CertificateID", i.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }

            // ADD LAST
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Update COCStatements set SupplierID = @SupplierID,DatePurchased=getdate(), isStock = '1' where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("SupplierID", SupplierID.SelectedValue);
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", EndRange.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
            DLdb.SQLST.Parameters.AddWithValue("Message", "Certificate assigned to supplier: " + SupplierName);
            DLdb.SQLST.Parameters.AddWithValue("Username", Session["IIT_UName"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
            DLdb.SQLST.Parameters.AddWithValue("CertificateID", EndRange.SelectedValue.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            decimal cocCost = 0;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Rates where ID='44'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    cocCost = Convert.ToDecimal(theSqlDataReader["Amount"]);
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            decimal vats = 0;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from settings where ID='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                vats = Convert.ToDecimal(theSqlDataReader["VatPercentage"].ToString());
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            decimal vat = Convert.ToDecimal(vats);
            decimal totalCost = curAmount * cocCost;
            decimal vatCost = totalCost * vat;
            // CREATE ORDER
            var OrderID = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "INSERT INTO Orders (SupplierID,UserID,Description,TotalNoItems,SubTotal,Vat,Delivery,Total,COCType,Method,isPaid,StartRange,EndRange) values (@SupplierID,@UserID,@Description,@TotalNoItems,@SubTotal,@Vat,@Delivery,@Total,@COCType,@Method,'1',@StartRange,@EndRange); Select Scope_Identity() as OrderID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
            DLdb.SQLST.Parameters.AddWithValue("SupplierID", SupplierID.SelectedValue);
            DLdb.SQLST.Parameters.AddWithValue("Description", "COC Purchase");
            DLdb.SQLST.Parameters.AddWithValue("TotalNoItems", curAmount);
            DLdb.SQLST.Parameters.AddWithValue("SubTotal", totalCost.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Vat", vatCost.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Delivery", "");
            DLdb.SQLST.Parameters.AddWithValue("Total", (totalCost + vatCost));
            DLdb.SQLST.Parameters.AddWithValue("COCType", "Paper");
            DLdb.SQLST.Parameters.AddWithValue("Method", "Delivery at Supplier");
            DLdb.SQLST.Parameters.AddWithValue("StartRange", StartRange.SelectedValue);
            DLdb.SQLST.Parameters.AddWithValue("EndRange", EndRange.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                OrderID = theSqlDataReader["OrderID"].ToString();
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
            DLdb.RS.Close();

            var createPDF = "";
            // GET THE PERIOD YY AND MM
            string srtMM = DateTime.Now.Month.ToString();
            string srtYY = DateTime.Now.Year.ToString();

            string SupAddress = AddressLine1.ToString() + ", " + AddressLine2.ToString() + ", " + Province.ToString() + ", " + CitySuburb.ToString() + ", " + AreaCode.ToString();

            // CREATE THE PDF INVOICE
            createPDF = String.Format("<body style='font-family:Calibri;font-size:11pt;color:black;'>" +
                                        "    <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                        "        <tr>" +
                                        "            <td>" +
                                        "                <table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                                        "                    <tr>" +
                                        "                    <td align='center' colspan='2' width='60%'><img src='https://197.242.82.242/inspectit/assets/img/cardlogo.jpg'/></td>" +
                                        "                    <td align='left' width='40%'><font style='font-size:26px;'><b>INVOICE No. : " + InvoiceNumber.Text.ToString() + "</b><br />Period: </b>" + srtMM + "-" + srtYY + "</font></td></tr>" +
                                        "                    </tr>" +
                                        "                    <tr>" +
                                        "                        <td align='left' width='70%'><br /><h4></h4>" + SupplierName.ToString() + "<br/>" + SupplierContactNo.ToString() + "<br/>" + SupplierEmail.ToString() + "</td>" +
                                        "                        <td align='left' width='30%' colspan='2'><br /><h4>Address :</h4>" + SupAddress.ToString() + "</td>" +
                                        "                    </tr>" +
                                        "                    <tr>" +
                                        "                        <td align='left' width='70%'><br /><h4>Paper bases COC Statements</h4></td>" +
                                        "                    </tr>" +
                                        "                    <tr>" +
                                        "                        <td align='left' colspan='3' valign='top'>" +
                                        "                            <table border='0' cellpadding='5px' cellspacing='0' width='100%'>" +
                                        "                               <tr>" +
                                        "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>COC Type</b></td>" +
                                        "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>Collection Method</b></td>" +
                                        "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'><b>Number of COC's</b></td>" +
                                        "                                </tr>" +
                                        "                               <tr>" +
                                        "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>Paper</td>" +
                                        "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>Delivery</td>" +
                                        "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle'>" + curAmount + "</td>" +
                                        "                                </tr>" +
                                        "                               <tr>" +
                                        "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
                                        "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>VAT @15%<b></td>" +
                                        "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + vatCost.ToString("0.##") + "</b></td>" +
                                        "                                </tr>" +
                                        "                               <tr>" +
                                        "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" valign='middle'></td>" +
                                        "                                   <td width='70%' style=\"border: 1px solid #E5E5E5;\" valign='middle' align='right'><b>Total Amount<b></td>" +
                                        "                                   <td width='15%' style=\"border: 1px solid #E5E5E5;\" align='right' valign='middle'><b>R" + (totalCost + vatCost).ToString("0.##") + "</b></td>" +
                                        "                                </tr>" +
                                        "                            </table>" +
                                        "                        </td>" +
                                        "                    </tr>" +
                                        "                    <tr>" +
                                        "                       <td align='middle'><b>Powered By InspectIT</b></td>" +
                                        "                        <td><td align='left'><img src='https://197.242.82.242/inspectit/assets/img/logo.png'/></td>" +
                                        "                    </tr>" +
                                        "                </table>" +
                                        "            </td>" +
                                        "        </tr>" +
                                        "    </table>" +
                                        "</body>");

            string filename = "invoice_" + OrderID + "_" + srtMM + "-" + srtYY + ".pdf";
            var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(createPDF);
            string path = Server.MapPath("~/invoices/") + filename;
            File.WriteAllBytes(path, pdfBytes);

            DLdb.RS3.Open();
            DLdb.SQLST3.CommandText = "update Orders set PDFName=@PDFName where OrderID=@OrderID";
            DLdb.SQLST3.Parameters.AddWithValue("OrderID", OrderID);
            DLdb.SQLST3.Parameters.AddWithValue("PDFName", filename);
            DLdb.SQLST3.CommandType = CommandType.Text;
            DLdb.SQLST3.Connection = DLdb.RS3;
            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            DLdb.SQLST3.Parameters.RemoveAt(0);
            DLdb.SQLST3.Parameters.RemoveAt(0);
            DLdb.RS3.Close();
            
                // EMAIL THE USER DETAILS
                string HTMLSubject = "Inspect IT Certificates.";
                string HTMLBody = "Dear " + fName.ToString() + "<br /><br />Certificates have been allocated to you" +
                "<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Administrator";
                DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", email.ToString(), path);


            DLdb.DB_Close();
            Response.Redirect("ViewSupplier.aspx?msg=" + DLdb.Encrypt("Certificate has been added successfully"));



        }
        
    }
}