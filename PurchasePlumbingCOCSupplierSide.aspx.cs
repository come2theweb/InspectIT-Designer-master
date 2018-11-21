using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace InspectIT
{
    public partial class PurchasePlumbingCOCSupplierSide : System.Web.UI.Page
    {
        //public decimal COCElectronic = 0;
        //public decimal COCPaper = 0;
        //public decimal CourierDelivery = 0;
        //public decimal RegisteredPostDelivery = 0;
        //public decimal Collection = 0;
        public Boolean signatureImg = false;
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
            //if (Session["IIT_Role"].ToString() != "Administrator")
            //{
            //    Response.Redirect("Default");
            //}

            //if (Request.QueryString["msg"] != null)
            //{
            //    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
            //    successmsg.InnerHtml = msg;
            //    successmsg.Visible = true;
            //}

            if (Request.QueryString["err"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
                Div1.InnerHtml = msg;
                Div1.Visible = true;
            }
            if (!IsPostBack)
            {
               
            }
            string supID = "";
            Boolean isblockked = false;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Suppliers where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    supID = theSqlDataReader["SupplierID"].ToString();
                    if (theSqlDataReader["isBlocked"].ToString()=="True")
                    {
                        isblockked = true;
                    }
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            dispErr.Visible = false;
            if (isblockked==true)
            {
                errormsg.InnerHtml = "You can't purchase any COCs";
                errormsg.Visible = true;
                btn_buy.Visible = false;
            }
            else
            {
                // GET AVALIABLE
                int NoBought = 0;
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select count(*) as Total from [dbo].[COCStatements] where SupplierID = @SupplierID";
                DLdb.SQLST.Parameters.AddWithValue("SupplierID", supID);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        NoBought = Convert.ToInt32(theSqlDataReader["Total"]);
                        NonLoggedCOCsPurchased.Text = NoBought.ToString();
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();


                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID = @UserID and NoCOCpurchases <> '0'";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                       
                        NumberOfPermittedCOCs.Text = theSqlDataReader["NoCOCpurchases"].ToString();
                        COCsAbleToPurchase.Text = (Convert.ToInt32(theSqlDataReader["NoCOCpurchases"]) - NoBought).ToString();
                    }
                }
                else
                {
                    errormsg.Visible = true;
                    btn_buy.Visible = false;
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }
            
            DLdb.DB_Close();
        }

        protected void btn_buy_Click(object sender, EventArgs e)
        {
            if (NoCOCsPurchase.Text != "")
            {
                // check number avaliable and current
                int NoAv = Convert.ToInt32(COCsAbleToPurchase.Text);
                int CurNo = Convert.ToInt32(NoCOCsPurchase.Text);

                if (CurNo > NoAv)
                {
                    errormsg.InnerHtml = "Can't purchase more than 'Number of COC's I am able to Purchase'";
                    errormsg.Visible = true;
                }
                else
                {
                    // check disclaimer
                    if (DisclaimerAgreementPurchaseCOC.Checked == false)
                    {
                        errormsg.InnerHtml = "You need to accept the disclaimer to continue.";
                        errormsg.Visible = true;
                    }
                    else
                    {
                        errormsg.Visible = false;
                        string OrderID = "";
                        string Type = "Paper";
                        
                        string Method = "Collect at PIRB Offices";
                        if (RegisteredPostPurchaseCOC.Checked == true)
                        {
                            Method = "Registered Post";
                        }
                        if (CourierPurchaseCOC.Checked == true)
                        {
                            Method = "Courier";
                        }

                        Global DLdb = new Global();
                        DLdb.DB_Connect();

                        // CREATE ORDER
                        decimal StartRange = 0;
                        DLdb.RS.Open();
                        DLdb.SQLST.CommandText = "Select top 1 (COCStatementID + 1) as nCOCStatementID from COCStatements order by COCStatementID desc";
                        DLdb.SQLST.CommandType = CommandType.Text;
                        DLdb.SQLST.Connection = DLdb.RS;
                        SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
                        if (theSqlDataReader.HasRows)
                        {
                            while (theSqlDataReader.Read())
                            {
                                StartRange = Convert.ToDecimal(theSqlDataReader["nCOCStatementID"].ToString());
                            }
                        }
                        if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                        DLdb.RS.Close();

                        decimal eRange = StartRange + Convert.ToDecimal(NoCOCsPurchase.Text);

                        DLdb.RS.Open();
                        DLdb.SQLST.CommandText = "INSERT INTO Orders (StartRange,EndRange,UserID,Description,TotalNoItems,SubTotal,Vat,Delivery,Total,COCType,Method,isPaid) values (@StartRange,@EndRange,@UserID,@Description,@TotalNoItems,@SubTotal,@Vat,@Delivery,@Total,@COCType,@Method,@isPaid); Select Scope_Identity() as OrderID";
                        DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                        DLdb.SQLST.Parameters.AddWithValue("Description", "COC Purchase");
                        DLdb.SQLST.Parameters.AddWithValue("TotalNoItems", NoCOCsPurchase.Text.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("SubTotal", CertificateCosts.Text.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("Vat", VATCOCPurchases.Text.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("Delivery", DeliveryCostCOCPurchase.Text.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("Total", totalAmountdue.Text.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("COCType", Type);
                        DLdb.SQLST.Parameters.AddWithValue("Method", Method);
                        DLdb.SQLST.Parameters.AddWithValue("StartRange", StartRange.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("EndRange", eRange.ToString());
                        DLdb.SQLST.Parameters.AddWithValue("isPaid", "0");
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
                        
                        string UserName = "";
                        string UserEmail = "";
                        string NumberTo = "";
                        //string useridss = Session["IIT_UID"].ToString();

                        //GET THE USERS DETAILS
                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "select * from users where userid = @UserID";
                        DLdb.SQLST3.Parameters.AddWithValue("UserID", Session["IIT_UID"]);
                        DLdb.SQLST3.CommandType = CommandType.Text;
                        DLdb.SQLST3.Connection = DLdb.RS3;
                        SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                        if (theSqlDataReader3.HasRows)
                        {
                            theSqlDataReader3.Read();
                            UserName = theSqlDataReader3["fname"].ToString() + " " + theSqlDataReader3["lname"].ToString();
                            UserEmail = theSqlDataReader3["email"].ToString();
                            if (theSqlDataReader3["contact"].ToString() != "" && theSqlDataReader3["contact"] != DBNull.Value)
                            {
                                NumberTo = theSqlDataReader3["contact"].ToString();
                            }
                        }

                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.RS3.Close();
                        

                        string OTPCode = DLdb.CreateNumber(5);
                        Session["IIT_OTPCodePlumber"] = OTPCode;
                        // Response.Redirect("ViewCOCStatement?msg=" + DLdb.Encrypt("Your purchase was complete, thank you."));
                        DLdb.sendSMS(Session["IIT_UID"].ToString(), NumberTo.ToString(), "Inspect-It: You would like to purchase " + NoCOCsPurchase.Text.ToString() + " " + Type + " C.O.C for " + Method + ". Amount: R" + totalAmountdue.Text.ToString() + ". OTP Code: " + OTPCode);
                        Response.Redirect("VerifyPurchaseSupplier?oid=" + DLdb.Encrypt(OrderID) + "&cost=" + totalAmountdue.Text.ToString());
                    }
                }
            }
            else
            {
                errormsg.InnerHtml = "Please enter an ammount into 'Number of COC's You wish to Purchase'";
                errormsg.Visible = true;
            }
            
        }

        protected void NoCOCsPurchase_TextChanged(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (Convert.ToInt32(NoCOCsPurchase.Text.ToString()) < 1)
            {
                NoCOCsPurchase.Text = "1";
            }
            else
            {
                decimal COCPaper = 0;
                decimal CourierDelivery = 0;
                decimal RegisteredPostDelivery = 0;
                decimal Collection = 0;

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Rates";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["ID"].ToString() == "36")
                        {
                            COCPaper = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                        }
                        else if (theSqlDataReader["ID"].ToString() == "24")
                        {
                            CourierDelivery = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                        }
                        else if (theSqlDataReader["ID"].ToString() == "25")
                        {
                            RegisteredPostDelivery = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                        }
                        else if (theSqlDataReader["ID"].ToString() == "26")
                        {
                            Collection = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                        }
                    }
                }
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                int NoCocsPurchase = Convert.ToInt32(NoCOCsPurchase.Text.ToString());
                int COCsAbleToPurchases = Convert.ToInt32(COCsAbleToPurchase.Text.ToString());

                if (NoCocsPurchase > COCsAbleToPurchases)
                {
                    dispErr.InnerHtml = "Can't purchase more than 'Number of COC's I am able to Purchase";
                    dispErr.Visible = true;
                }

                decimal totalCost = 0;
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
                string a = "0.##";

                // if amount coc > 50 make delivery cost 0
                if (NoCocsPurchase > 50)
                {
                    RegisteredPostDelivery = 0;
                    CourierDelivery = 0;
                }

                // paperdel.Attributes.Remove("class");
                totalCost = NoCocsPurchase * COCPaper;
                CertificateCost.Text = totalCost.ToString("0.##");
                CertificateCosts.Text = totalCost.ToString("0.##");

                decimal vatPaper = totalCost * vat;
                VATCOCPurchase.Text = vatPaper.ToString("0.##");
                VATCOCPurchases.Text = vatPaper.ToString("0.##");
                decimal totalAmount = totalCost + vatPaper;

                if (CourierPurchaseCOC.Checked == true)
                {
                    DeliveryCostCOCPurchase.Text = CourierDelivery.ToString();
                    totalAmount = totalAmount + CourierDelivery;
                }
                else if (RegisteredPostPurchaseCOC.Checked == true)
                {
                    DeliveryCostCOCPurchase.Text = RegisteredPostDelivery.ToString();
                    totalAmount = totalAmount + RegisteredPostDelivery;
                }
                else if (CollectPurchaseCOC.Checked == true)
                {
                    DeliveryCostCOCPurchase.Text = Collection.ToString();
                    totalAmount = totalAmount + Collection;
                }
                TotalDueCOCPurchase.Text = totalAmount.ToString("0.##");
                totalAmountdue.Text = totalAmount.ToString("0.##");

                DLdb.DB_Close();
            }
            
        }

        protected void Group1_CheckedChanged(Object sender, EventArgs e)
        {
            //if (CollectPurchaseCOC.Checked)
            //{
                
            //}

            //if (RegisteredPostPurchaseCOC.Checked)
            //{
                
            //}
            
            //if (CourierPurchaseCOC.Checked)
            //{
                
            //}

            Global DLdb = new Global();
            DLdb.DB_Connect();

            decimal COCPaper = 0;
            decimal CourierDelivery = 0;
            decimal RegisteredPostDelivery = 0;
            decimal Collection = 0;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Rates";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["ID"].ToString() == "36")
                    {
                        COCPaper = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                    else if (theSqlDataReader["ID"].ToString() == "24")
                    {
                        CourierDelivery = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                    else if (theSqlDataReader["ID"].ToString() == "25")
                    {
                        RegisteredPostDelivery = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                    else if (theSqlDataReader["ID"].ToString() == "26")
                    {
                        Collection = Convert.ToDecimal(theSqlDataReader["Amount"].ToString());
                    }
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            int NoCocsPurchase = Convert.ToInt32(NoCOCsPurchase.Text.ToString());
            int COCsAbleToPurchases = Convert.ToInt32(COCsAbleToPurchase.Text.ToString());

            if (NoCocsPurchase > COCsAbleToPurchases)
            {
                dispErr.InnerHtml = "Can't purchase more than 'Number of COC's I am able to Purchase";
                dispErr.Visible = true;
            }

            decimal totalCost = 0;
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
            string a = "0.##";

            // if amount coc > 50 make delivery cost 0
            if (NoCocsPurchase > 50)
            {
                RegisteredPostDelivery = 0;
                CourierDelivery = 0;
            }

            // paperdel.Attributes.Remove("class");
            totalCost = NoCocsPurchase * COCPaper;
            CertificateCost.Text = totalCost.ToString("0.##");
            CertificateCosts.Text = totalCost.ToString("0.##");

            decimal vatPaper = totalCost * vat;
            VATCOCPurchase.Text = vatPaper.ToString("0.##");
            VATCOCPurchases.Text = vatPaper.ToString("0.##");
            decimal totalAmount = totalCost + vatPaper;

            if (CourierPurchaseCOC.Checked == true)
            {
                DeliveryCostCOCPurchase.Text = CourierDelivery.ToString();
                totalAmount = totalAmount + CourierDelivery;
            }
            else if (RegisteredPostPurchaseCOC.Checked == true)
            {
                DeliveryCostCOCPurchase.Text = RegisteredPostDelivery.ToString();
                totalAmount = totalAmount + RegisteredPostDelivery;
            }
            else if (CollectPurchaseCOC.Checked == true)
            {
                DeliveryCostCOCPurchase.Text = Collection.ToString();
                totalAmount = totalAmount + Collection;
            }
            TotalDueCOCPurchase.Text = totalAmount.ToString("0.##");
            totalAmountdue.Text = totalAmount.ToString("0.##");

            DLdb.DB_Close();
        }
    }
}