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

namespace InspectIT.API
{
    public partial class inspectorSubmitAudit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            

            string inspecDate = Request.QueryString["inspecDate"].ToString();
            string refixDate = Request.QueryString["refixDate"].ToString();
            string audEmail= Request.QueryString["audEmail"].ToString();
            string quality = Request.QueryString["quality"].ToString();
            string cocid = Request.QueryString["cocid"].ToString();
            string uid = Request.QueryString["uid"].ToString();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCInspectors set InspectionDate = @InspectionDate, Status = @Status, Quality = @Quality where COCStatementID = @COCStatementID and UserID = @UserID and isactive='1'";
            DLdb.SQLST.Parameters.AddWithValue("InspectionDate", inspecDate.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Status", "");
            DLdb.SQLST.Parameters.AddWithValue("Quality", quality.ToString());
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID",uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatements set DateRefix = @DateRefix,DateAudited = getdate(),isInspectorSubmitted='1',isPlumberSubmitted='0' where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID",cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("DateRefix", refixDate.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            

            string CustomerID = "";
            string Reviews = "";

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "SELECT * FROM COCReviews where COCStatementID = @COCStatementID and isActive = '1' order by createdate desc";
            DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    string StatusCol = "";
                    string backCol = "";
                    string btnFix = "";
                    var html_FIELD_Content = "";

                    if (theSqlDataReader2["status"].ToString() == "Failure")
                    {
                        backCol = "#F8D7DA";
                        StatusCol = "danger";
                    }
                    else if (theSqlDataReader2["status"].ToString() == "Cautionary")
                    {
                        backCol = "#FFF3CD";
                        StatusCol = "warning";
                    }
                    else if (theSqlDataReader2["status"].ToString() == "Compliment")
                    {
                        backCol = "#D4EDDA";
                        StatusCol = "success";
                    }

                    string InstallationType = "";

                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "Select * from InstallationTypes where InstallationTypeID = @InstallationTypeID";
                    DLdb.SQLST3.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader2["TypeID"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        while (theSqlDataReader3.Read())
                        {
                            InstallationType = theSqlDataReader3["InstallationType"].ToString();
                        }
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();

                    int countImga = 0;
                    string Media = "";

                    string HTMLContentImgs = "<table border='0' cellpadding='10px' cellspacing='0' width='100%'><tr>";

                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "Select * from FormImg where ReviewID = @ReviewID and UserID=@UserID and isReference='0'";
                    DLdb.SQLST3.Parameters.AddWithValue("ReviewID", theSqlDataReader2["ReviewID"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("UserID", uid.ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        while (theSqlDataReader3.Read())
                        {

                            if (theSqlDataReader3["imgsrc"].ToString() != "" && theSqlDataReader3["imgsrc"] != DBNull.Value)
                            {
                                string filename = theSqlDataReader3["imgsrc"].ToString();

                                if (countImga == 3)
                                {
                                    HTMLContentImgs += "</tr><tr>";
                                    countImga = 0;
                                }

                                HTMLContentImgs += "<td>" +
                                   "<img src=\"https://197.242.82.242/inspectit/AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img-thumbnail img img-responsive\" style=\"height:100px;\" />" +
                                   "</td>";

                                countImga++;

                            }
                            else
                            {
                                Media += "";
                            }

                        }
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();

                    string refImgsw = "";
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "Select * from FormImg where ReviewID = @ReviewID and UserID=@UserID and isReference='1'";
                    DLdb.SQLST3.Parameters.AddWithValue("ReviewID", theSqlDataReader2["ReviewID"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("UserID", uid.ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        while (theSqlDataReader3.Read())
                        {

                            if (theSqlDataReader3["imgsrc"].ToString() != "" && theSqlDataReader3["imgsrc"] != DBNull.Value)
                            {
                                string filename = theSqlDataReader3["imgsrc"].ToString();

                                if (countImga == 3)
                                {
                                    HTMLContentImgs += "</tr><tr>";
                                    countImga = 0;
                                }

                                refImgsw += "<td>" +
                                   "<img src=\"https://197.242.82.242/inspectit/AuditorImgs/" + filename + "\" style=\"height:130px;\" class=\"img-thumbnail img img-responsive\" style=\"height:100px;\" />" +
                                   "</td>";

                                countImga++;

                            }
                            else
                            {
                                Media += "";
                            }

                        }
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();

                    html_FIELD_Content = "<table border='0' cellpadding='10px' cellspacing='0' width='100%'>" +
                        "<tr>" +
                        "   <td colspan='2' style=\"text-align:center;\"><b>" + InstallationType + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                        "   <td><b>Audit Status:</b></td>" +
                        "   <td>" + theSqlDataReader2["status"].ToString() + "</td>" +
                        "</tr>" +

                        "<tr>" +
                        "   <td><b>Reference:</b></td>" +
                        "   <td>" + theSqlDataReader2["Reference"].ToString() + "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "   <td colspan='2'>" + refImgsw + "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "   <td><b>Comments:</b></td>" +
                        "   <td>" + theSqlDataReader2["comment"].ToString() + "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "   <td colspan='2'>" + HTMLContentImgs + "</tr></table></td>" +
                        "</tr>" +
                        "</table>";

                    Reviews += "<div class=\"row alert-" + StatusCol + "\" style=\"padding: 5px;background-color:" + backCol + "; \">" +
                                               html_FIELD_Content +
                                               " </div><hr />";

                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            // GET USER DETAILS
            string uname = "";
            string uemail = "";
            string ucompany = "";
            string usignature = "";
            string uaddress = "";
            string ucontact = "";
            string uregno = "";

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select top 1 * from Users where UserID = @UserID";
            DLdb.SQLST2.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                theSqlDataReader2.Read();
                uname = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                uemail = theSqlDataReader2["email"].ToString();
                ucompany = theSqlDataReader2["company"].ToString();
                string sigImg = "";
                if (theSqlDataReader2["signature"].ToString() != "" || theSqlDataReader2["signature"] != DBNull.Value)
                {
                    string filename = theSqlDataReader2["signature"].ToString();
                    sigImg += "<img src=\"http://197.242.82.242/pirbreg/signatures/" + sigImg + "\" class=\"img img-responsive\" style=\"max-height:100px;\" />";

                }
                else
                {
                    sigImg = "";
                }
                // usignature = "<img src=\"https://197.242.82.242/inspectit/signatures/" + theSqlDataReader2["signature"].ToString() + "\" />";
                usignature = sigImg;
                uaddress = theSqlDataReader2["ResidentialStreet"].ToString() + " " + theSqlDataReader2["ResidentialSuburb"].ToString() + " " + theSqlDataReader2["ResidentialCity"].ToString() + "  " + theSqlDataReader2["Province"].ToString() + " " + theSqlDataReader2["ResidentialCode"].ToString();
                ucontact = theSqlDataReader2["fname"].ToString();
                uregno = theSqlDataReader2["regno"].ToString();
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            string plumberid = "";
            string refixTrue = "false";
            string tickab = "";
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "Select * from COCStatements where COCStatementID = @COCStatementID";
            DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    if (theSqlDataReader2["isRefix"].ToString() == "True")
                    {
                        refixTrue = "true";
                    }
                    plumberid = theSqlDataReader2["UserID"].ToString();
                    CustomerID = theSqlDataReader2["CustomerID"].ToString();
                    tickab = theSqlDataReader2["AorB"].ToString();
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            string plumberemail = "";
            string plumbername = "";
            string plumbercontact = "";
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select * from Users where UserID = @UserID";
            DLdb.SQLST2.Parameters.AddWithValue("UserID", plumberid);
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                theSqlDataReader2.Read();
                plumbername = theSqlDataReader2["fname"].ToString() + " " + theSqlDataReader2["lname"].ToString();
                plumberemail = theSqlDataReader2["email"].ToString();
                plumbercontact = theSqlDataReader2["contact"].ToString();
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            string installTypes = "";
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select * from COCInstallations where COCstatementID = @COCstatementID";
            DLdb.SQLST2.Parameters.AddWithValue("COCstatementID", cocid.ToString());
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    if (installTypes == "")
                    {
                        installTypes = theSqlDataReader2["TypeID"].ToString();
                    }
                    else
                    {
                        installTypes += "," + theSqlDataReader2["TypeID"].ToString();
                    }
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close(); 

            string clientname = "";
            string clientaddress = "";
            string Clientemail = "";
            string ClientTel = "";

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select * from Customers where CustomerID = @CustomerID";
            DLdb.SQLST2.Parameters.AddWithValue("CustomerID", CustomerID);
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                theSqlDataReader2.Read();
                clientname = theSqlDataReader2["CustomerName"].ToString() + ' ' + theSqlDataReader2["CustomerSurname"].ToString();
                clientaddress = theSqlDataReader2["AddressStreet"].ToString() + "<br />" + theSqlDataReader2["AddressSuburb"].ToString() + "<br />" + theSqlDataReader2["AddressCity"].ToString() + "<br />" + theSqlDataReader2["Province"].ToString();
                Clientemail = theSqlDataReader2["CustomerEmail"].ToString();
                ClientTel = theSqlDataReader2["CustomerCellNo"].ToString();

            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.SQLST2.Parameters.RemoveAt(0);
            DLdb.RS2.Close();

            string clientdetails = "<div style='border:1px solid #EFEFEF;padding:10px;width:100%;'><b>Customer Name: </b>" + clientname + "<br />";
            clientdetails += "<b>Email Address: </b>" + Clientemail + "<br />";
            clientdetails += "<b>Tel No.: </b>" + ClientTel + "<br />";
            clientdetails += "<b>Address: </b>" + clientaddress + "<br /></div>";

            // BUILD THE PDF FILENAME
            DateTime cDate = DateTime.Now;
            string filenames = cDate.ToString("ddMMyyyy") + "_" +uid.ToString() + "_InspectorCOC_" + cocid.ToString() + ".pdf";

            string seltype = "";

            if (tickab.ToString() == "A")
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
            string heatpump = "";
            string solarwaterHeating = "";
            List<string> ParamsListNewNew = installTypes.Split(',').ToList<string>();
            foreach (string types in ParamsListNewNew)
            {

                // item.Value.ToString()
                if (types.ToString() == "1")
                {
                    hotwatersystemstick = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                }

                if (types.ToString() == "5")
                {
                    belowground = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                }

                if (types.ToString() == "2")
                {
                    coldwatersystemstick = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                }
                if (types.ToString() == "6")
                {
                    aboveground = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                }
                if (types.ToString() == "3")
                {
                    sanitaryware = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                }
                if (types.ToString() == "7")
                {
                    rainwater = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                }
                if (types.ToString() == "4")
                {
                    solarwaterHeating = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                }
                if (types.ToString() == "8")
                {
                    heatpump = "<img src=\"https://197.242.82.242/inspectit/assets/tick.png\" style=\"height:25px;\"/>";
                }

            }


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
                                                "                           <td bgcolor='lightgreen' colspan='2'>" +
                                                "                               <h4 style='background-color:lightgreen;text-align:center;'>Installation Details<br /><span>(Details of the work undertaken or scope of work for which the COC is being issued for)</span></h4>" +
                                                "                           </td></tr>" +
                                                 "                    <tr>" +
                                                "                        <td align='middle'>" +
                                                                            //"                               " + html_FIELD_Content + " " +
                                                                            Reviews +


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
                                                "                       </table>" +
                                                "                    </tr><br/><br/><br/><br/><br/><br/><br/><br/><br/>" +
                                                "                </table>" +
                                                "            </td>" +
                                                "        </tr>" +
                                                "    </table>" +
                                                "</body>");

            //Response.Write(htmlContent);
            //Response.End();

            var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(htmlContent);
            string path = Server.MapPath("~/Inspectorinvoices/") + filenames;
            File.WriteAllBytes(path, pdfBytes);

            DLdb.RS5.Open();
            DLdb.SQLST5.CommandText = "update COCInspectors set Invoice=@Invoice where COCStatementID=@COCStatementID";
            DLdb.SQLST5.Parameters.AddWithValue("Invoice", filenames);
            DLdb.SQLST5.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST5.CommandType = CommandType.Text;
            DLdb.SQLST5.Connection = DLdb.RS5;
            SqlDataReader theSqlDataReader5 = DLdb.SQLST5.ExecuteReader();

            if (theSqlDataReader5.IsClosed) theSqlDataReader5.Close();
            DLdb.SQLST5.Parameters.RemoveAt(0);
            DLdb.SQLST5.Parameters.RemoveAt(0);
            DLdb.RS5.Close();

            DLdb.sendSMS(plumberid, plumbercontact, "Inspect-It: COC Statement Number: " + DLdb.Decrypt(Request.QueryString["cocid"]) + " Has been audited and requires your attention");

            string eHTMLBody = "Dear Auditor<br /><br />COC Statement Number: " + DLdb.Decrypt(Request.QueryString["cocid"]) + "<br/> Please see attached your report <br/><br /><br />Regards<br />Inspect-It Administrator";
            string eSubject = "Inspect-IT - C.O.C Statement Report";
            DLdb.sendEmail(eHTMLBody, eSubject, "mathewpayne@gmail.com", audEmail.ToString(), path);

            string aHTMLBody = "Dear " + plumbername + "<br /><br />COC Statement Number: " + DLdb.Decrypt(Request.QueryString["cocid"]) + "<br/> has been audited. See attached.<br/><br /><br />Regards<br />Inspect-It Administrator";
            string aSubject = "Inspect-IT - C.O.C Statement Report";
            DLdb.sendEmail(aHTMLBody, aSubject, "mathewpayne@gmail.com", plumberemail.ToString(), path);

            if (refixTrue == "true")
            {
                string HTMLSubject = "Inspect IT - C.O.C Refix Required";
                string HTMLBody = "Dear " + plumbername.ToString() + "<br /><br />COC Number " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " has been audited and a refix is required.<br /><br />If you have any problems, please contact us on <a href=\"mailto:support@inspectit.co.za\">support@inspectit.co.za</a><br /><br />Kind Regards<br />Inspect IT Team";
                DLdb.sendEmail(HTMLBody, HTMLSubject, "mathewpayne27@gmail.com", plumberemail.ToString(), "");

                DLdb.sendSMS(plumberid.ToString(), plumbercontact.ToString(), "COC Number " + DLdb.Decrypt(Request.QueryString["cocid"].ToString()) + " has been audited and a refix is required.");

            }


            DLdb.DB_Close();

        }
    }
}