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

namespace InspectIT
{
    public partial class EditOrDeleteSupplier : System.Web.UI.Page
    {
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

            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                btn_update.Visible = false;
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {
                
            }

            if (!IsPostBack)
            {
                DLdb.DB_Connect();
                string UserID = "";

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Suppliers where SupplierID=@SupplierID";
                DLdb.SQLST.Parameters.AddWithValue("SupplierID", DLdb.Decrypt(Request.QueryString["SupplierID"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        // General Details
                        SupplierName.Text = theSqlDataReader["SupplierName"].ToString();
                        SupplierRegNo.Text = theSqlDataReader["SupplierRegNo"].ToString();
                        SupplierWebsite.Text = theSqlDataReader["SupplierWebsite"].ToString();
                        SupplierEmail.Text = theSqlDataReader["SupplierEmail"].ToString();
                        SupplierContactNo.Text = theSqlDataReader["SupplierContactNo"].ToString();
                        AddressLine1.Text = theSqlDataReader["AddressLine1"].ToString();
                        AddressLine2.Text = theSqlDataReader["AddressLine2"].ToString();
                        Province.Text = theSqlDataReader["Province"].ToString();
                        postalprovince.Text = theSqlDataReader["PostalProvince"].ToString();
                        CitySuburb.Text = theSqlDataReader["CitySuburb"].ToString();
                        AreaCode.Text = theSqlDataReader["AreaCode"].ToString();
                        PostalAddress.Text = theSqlDataReader["PostalAddress"].ToString();
                        PostalCity.Text = theSqlDataReader["PostalCity"].ToString();
                        PostalCode.Text = theSqlDataReader["PostalCode"].ToString();
                        NoCertificates.Text = theSqlDataReader["TotalNumber"].ToString();
                        UserID = theSqlDataReader["UserID"].ToString();
                        InvoiceNumber.Text = theSqlDataReader["InvoiceNumber"].ToString();
                        if (theSqlDataReader["isBlocked"].ToString() == "True")
                        {
                            CheckBox1.Checked = true;

                        }
                    }
                }
                
                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from users where userid = @userid";
                DLdb.SQLST.Parameters.AddWithValue("userid", UserID);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();
                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        // General Details
                        fName.Text = theSqlDataReader["fName"].ToString();
                        lName.Text = theSqlDataReader["lname"].ToString();
                        email.Text = theSqlDataReader["email"].ToString();
                        nonloggedcocallocated.Text = theSqlDataReader["NoCOCpurchases"].ToString();
                        supPassword.Text = DLdb.Decrypt(theSqlDataReader["password"].ToString());
                        supPasswordConfirm.Text = DLdb.Decrypt(theSqlDataReader["password"].ToString());
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                int NoBought = 0;
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select count(*) as Total from [dbo].[COCStatements] where SupplierID = @SupplierID";
                DLdb.SQLST.Parameters.AddWithValue("SupplierID", DLdb.Decrypt(Request.QueryString["SupplierID"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        NoBought = Convert.ToInt32(theSqlDataReader["Total"]);
                        loggedcoc.Text = NoBought.ToString();
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
                DLdb.SQLST.Parameters.AddWithValue("Param", "val");
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
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();


                COCStatement.InnerHtml = "";

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "SELECT * FROM Orders where SupplierID = @SupplierID and isactive = '1'";
                DLdb.SQLST.Parameters.AddWithValue("SupplierID", DLdb.Decrypt(Request.QueryString["SupplierID"]));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        string username = "";
                        string regno = "";
                        DateTime cDate = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());

                        // GET CUSTOMER NAME AND ADDRESS
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM Users where UserID = @UserID";
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                username = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                                regno = theSqlDataReader2["regno"].ToString();
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                        COCStatement.InnerHtml += "<tr>" +
                                                    "<td>" + theSqlDataReader["OrderID"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["Description"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["TotalNoItems"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["StartRange"].ToString() + "</td>" +
                                                    "<td>" + theSqlDataReader["EndRange"].ToString() + "</td>" +
                                                    "<td>" + cDate.ToString("MM/dd/yyyy") + "</td>" +
                                                    //"<td>" + theSqlDataReader["COCType"].ToString() + "</td>" +
                                                    "<td>" + username + "</td>" +
                                                    "<td>" + regno + "</td>" +
                                                //"<td width=\"100px\"></td>" +
                                                "</tr>";

                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
                

                DLdb.DB_Close();
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (supPassword.Text.ToString() == "")
            {
                Global DLdb = new Global();
                DLdb.DB_Connect();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "UPDATE Suppliers SET SupplierName=@SupplierName,isBlocked=@isBlocked, SupplierRegNo=@SupplierRegNo, SupplierWebsite=@SupplierWebsite," +
                    "SupplierEmail=@SupplierEmail, SupplierContactNo=@SupplierContactNo, AddressLine1=@AddressLine1, AddressLine2=@AddressLine2," +
                    "Province=@Province, CitySuburb=@CitySuburb, AreaCode=@AreaCode,PostalProvince=@PostalProvince, PostalAddress=@PostalAddress, PostalCity=@PostalCity, PostalCode=@PostalCode WHERE SupplierID = @SupplierID";
                DLdb.SQLST.Parameters.AddWithValue("SupplierID", DLdb.Decrypt(Request.QueryString["SupplierID"]));
                DLdb.SQLST.Parameters.AddWithValue("SupplierName", SupplierName.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SupplierRegNo", SupplierRegNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SupplierWebsite", SupplierWebsite.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SupplierEmail", SupplierEmail.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("SupplierContactNo", SupplierContactNo.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressLine1", AddressLine1.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AddressLine2", AddressLine2.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("Province", Province.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PostalProvince", postalprovince.SelectedValue.ToString());
                DLdb.SQLST.Parameters.AddWithValue("CitySuburb", CitySuburb.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("AreaCode", AreaCode.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PostalAddress", PostalAddress.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PostalCity", PostalCity.Text.ToString());
                DLdb.SQLST.Parameters.AddWithValue("PostalCode", PostalCode.Text.ToString());
                if (CheckBox1.Checked)
                {
                    DLdb.SQLST.Parameters.AddWithValue("isBlocked", "1");
                }
                else
                {

                    DLdb.SQLST.Parameters.AddWithValue("isBlocked", "0");
                }
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
                DLdb.RS.Close();

                

                string UserID = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select * from suppliers where supplierid = @supplierID";
                DLdb.SQLST.Parameters.AddWithValue("supplierID", DLdb.Decrypt(Request.QueryString["SupplierID"]));
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
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "UPDATE Users SET NoCOCpurchases=@NoCOCpurchases WHERE UserID = @UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                DLdb.SQLST.Parameters.AddWithValue("NoCOCpurchases", nonloggedcocallocated.Text.ToString());
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("ViewSupplier.aspx?msg=" + DLdb.Encrypt("Supplier details updated"));
            }
            else
            {
                if (supPassword.Text.ToString() == supPasswordConfirm.Text.ToString())
                {
                    Global DLdb = new Global();
                    DLdb.DB_Connect();
                    
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "UPDATE Suppliers SET SupplierName=@SupplierName, SupplierRegNo=@SupplierRegNo, SupplierWebsite=@SupplierWebsite," +
                        "SupplierEmail=@SupplierEmail, SupplierContactNo=@SupplierContactNo, AddressLine1=@AddressLine1, AddressLine2=@AddressLine2," +
                        "Province=@Province, CitySuburb=@CitySuburb, AreaCode=@AreaCode,isBlocked=@isBlocked, PostalAddress=@PostalAddress, PostalCity=@PostalCity, PostalCode=@PostalCode WHERE SupplierID = @SupplierID";
                    DLdb.SQLST.Parameters.AddWithValue("SupplierID", DLdb.Decrypt(Request.QueryString["SupplierID"]));

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
                    if (CheckBox1.Checked)
                    {
                        DLdb.SQLST.Parameters.AddWithValue("isBlocked", "1");
                    }
                    else
                    {

                        DLdb.SQLST.Parameters.AddWithValue("isBlocked", "0");
                    }
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

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
                    DLdb.RS.Close();
                    

                    string UserID = "";
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from suppliers where supplierid = @supplierID";
                    DLdb.SQLST.Parameters.AddWithValue("supplierID", DLdb.Decrypt(Request.QueryString["SupplierID"]));
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
                    DLdb.RS.Close();
                    
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "update users set password = @password, lname = @lname,NoCOCpurchases=@NoCOCpurchases, fname = @fname, email = @email where UserID = @UserID";
                    DLdb.SQLST.Parameters.AddWithValue("UserID", UserID);
                    DLdb.SQLST.Parameters.AddWithValue("password", DLdb.Encrypt(supPassword.Text.ToString()));
                    DLdb.SQLST.Parameters.AddWithValue("fName", fName.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("lname", lName.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("email", email.Text.ToString());
                    DLdb.SQLST.Parameters.AddWithValue("NoCOCpurchases", nonloggedcocallocated.Text.ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.DB_Close();

                    Response.Redirect("ViewSupplier.aspx?msg=" + DLdb.Encrypt("Supplier details updated"));
                }
                else
                {
                    errormsg.InnerHtml = "Your password does not match";
                    errormsg.Visible = true;
                }
            }
        }
    }
}