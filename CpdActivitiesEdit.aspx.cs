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
using ZXing.QrCode;
using System.IO;
using GoogleQRGenerator;
using System.Drawing;
using System.Net;

namespace InspectIT
{
    public partial class CpdActivitiesEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                btnUpdate.Visible = false;
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {

            }

            // ADMIN CHECK
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }

            //if (Request.QueryString["msg"] != null)
            //{
            //    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
            //    successmsg.InnerHtml = msg;
            //    successmsg.Visible = true;
            //}

            //if (Request.QueryString["err"] != null)
            //{
            //    string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
            //    errormsg.InnerHtml = msg;
            //    errormsg.Visible = true;
            //}
            
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM CPDActivities where CPDActivityID=@CPDActivityID";
                    DLdb.SQLST.Parameters.AddWithValue("CPDActivityID", DLdb.Decrypt(Request.QueryString["id"]));
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            Category.SelectedValue = theSqlDataReader["Category"].ToString();
                            Activity.Text = theSqlDataReader["Activity"].ToString();
                            ProductCode.Text = theSqlDataReader["ProductCode"].ToString();
                            Points.Text = theSqlDataReader["Points"].ToString();
                            StartDate.Text = theSqlDataReader["StartDate"].ToString();
                            EndDate.Text = theSqlDataReader["EndDate"].ToString();
                            Image1.ImageUrl = "QRCode/" + theSqlDataReader["QRCode"].ToString();

                            if (theSqlDataReader["QRCode"].ToString() != "" && theSqlDataReader["QRCode"] != DBNull.Value)
                            {
                                genQR.Visible = false;
                            }
                        }
                    }
               
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                
                    DLdb.DB_Close();
                    btnSave.Visible = false;
                }
                else
                {
                    btnUpdate.Visible = false;

                }
            }
        }
        protected void btn_update_Click1(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update CPDActivities set Activity=@Activity,ProductCode=@ProductCode,Points=@Points,EndDate=@EndDate,StartDate=@StartDate,Category=@Category where CPDActivityID=@CPDActivityID";
            DLdb.SQLST.Parameters.AddWithValue("CPDActivityID", DLdb.Decrypt(Request.QueryString["id"]));
            DLdb.SQLST.Parameters.AddWithValue("Activity", Activity.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Category", Category.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ProductCode", ProductCode.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("EndDate", EndDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("StartDate", StartDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Points", Points.Text.ToString());
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
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("CpdActivities");
        }


        protected void genQR_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            
            if (ProductCode.Text.ToString() == "")
            {
                errormsg.Visible = true;
                errormsg.InnerHtml = "Please enter a product code before generating a QR Code";
            }
            else
            {
                string qrName = DLdb.CreatePassword(10);
                Session["IIT_qrname"] = qrName;
                //ProductCode.Text = qrName.ToString();
                var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chs={1}x{2}&chl={0}", ProductCode.Text.ToString(), "500", "500");
                WebResponse response = default(WebResponse);
                Stream remoteStream = default(Stream);
                StreamReader readStream = default(StreamReader);
                WebRequest request = WebRequest.Create(url);
                response = request.GetResponse();
                remoteStream = response.GetResponseStream();
                readStream = new StreamReader(remoteStream);
                System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);

                img.Save(System.IO.Path.Combine(Server.MapPath("~/QRCode/" + qrName + ".png")));
                response.Close();
                remoteStream.Close();
                readStream.Close();

                Image1.ImageUrl = url;
            }

            DLdb.DB_Close();
        }

        protected void btnsave_Click1(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            if (Session["IIT_qrname"] == null || Session["IIT_qrname"].ToString() == "")
            {
                errormsg.Visible = true;
                errormsg.InnerHtml = "Please generate a QR Code before submitting a CPD Activity";
            }
            else
            {

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into CPDActivities (Activity,Category,EndDate,StartDate,Points,ProductCode,QRCode) values (@Activity,@Category,@EndDate,@StartDate,@Points,@ProductCode,@QRCode)";
            DLdb.SQLST.Parameters.AddWithValue("Activity", Activity.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Category", Category.SelectedValue.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ProductCode", ProductCode.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("EndDate", EndDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("StartDate", StartDate.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Points", Points.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("QRCode", Session["IIT_qrname"].ToString() + ".png");
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
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("CpdActivities");

            }
        }
    }
}