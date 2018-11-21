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
using System.Drawing;

namespace InspectIT
{
    public partial class HomeCOCAdmin : System.Web.UI.Page
    {
        public string COCDailySales = "";
        public string COCDailySalesMonth = "";
        public string COCDailyLogged = "";
        public string COCSupplierStock = "";
        public string LicensedPlumbers = "";
        public string InstallTypes = "";
        public string COCStatus = "";
        public string COCStatusYear = "";

        protected void Page_init(object sender, EventArgs e)
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

            if (Request.QueryString["msg"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
                successmsg.InnerHtml = msg;
                successmsg.Visible = true;
            }

            if (Request.QueryString["err"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
                errormsg.InnerHtml = msg;
                errormsg.Visible = true;
            }
            
            DLdb.DB_Connect();

            COCDailySales = "[\"Day\", \"COC\", { role: \"style\" }]";

            COCDailySalesMonth = "[\"Month\", \"COC\", { role: \"style\" }]";

            COCDailyLogged = "[\"Day\", \"COC\", { role: \"style\" }]";

            COCSupplierStock = "[\"Supplier\", \"Stock\", { role: \"style\" }]";

            LicensedPlumbers = "[\"Months\", \"Users\", { role: \"style\" }]";

            InstallTypes = "[\"Types\", \"COC\", { role: \"style\" }]";

            COCStatus = "[\"Types\", \"COC\", { role: \"style\" }]";

            COCStatusYear = "[\"Types\", \"COC\", { role: \"style\" }]";

            DateTime sDate = DLdb.FirstDayOfMonth(DateTime.Now);
            DateTime eDate = DLdb.LastDayOfMonth(DateTime.Now);

            foreach (DateTime day in DLdb.EachDay(sDate, eDate))
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select count(*) as total from COCStatements where isactive='1' and isStock = '0' and userid <> '0' and DatePurchased between '" + day.ToString().Replace(" 12:00:00 AM", "") + " 00:00:01' and '" + day.ToString().Replace(" 12:00:00 AM", "") + " 23:59:59'";
                DLdb.SQLST.Parameters.AddWithValue("Param", "val");
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        Random rand = new Random();
                        int r = rand.Next(256);
                        int g = rand.Next(256);
                        int b = rand.Next(256);
                        Color col = Color.FromArgb(r, g, b);

                        COCDailySales += ",[\"" + day.ToString() + "\", " + theSqlDataReader["total"].ToString() + ", \"blue\"]";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select count(*) as total from COCStatements where isactive='1' and Status = 'Logged' and isStock = '0' and userid <> '0' and DateLogged between '" + day.ToString().Replace(" 12:00:00 AM", "") + " 00:00:01' and '" + day.ToString().Replace(" 12:00:00 AM", "") + " 23:59:59'";
                DLdb.SQLST.Parameters.AddWithValue("Param", "val");
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        Random rand = new Random();
                        int r = rand.Next(256);
                        int g = rand.Next(256);
                        int b = rand.Next(256);
                        Color col = Color.FromArgb(r, g, b);

                        COCDailyLogged += ",[\"" + day.ToString() + "\", " + theSqlDataReader["total"].ToString() + ", \"green\"]";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();
            }

            // SUPPLIER STOCK
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "Select * from Suppliers where isactive = '1'";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {

                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "Select count(*) as Total from COCStatements where SupplierID = @SupplierID and isActive = '1' and isStock = '1'";
                    DLdb.SQLST3.Parameters.AddWithValue("SupplierID", theSqlDataReader2["SupplierID"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        while (theSqlDataReader3.Read())
                        {
                            if (theSqlDataReader3["total"] != DBNull.Value && theSqlDataReader3["total"].ToString() != "0")
                            {
                                COCSupplierStock += ",[\"" + theSqlDataReader2["SupplierName"].ToString() + "\", " + theSqlDataReader3["total"].ToString() + ", \"green\"]";
                            }
                        }
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();

                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            // GET TOTALS
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "Select count(*) as Total from COCStatements where isactive = '1' and isstock = '0' and userid <> '0' and DatePurchased between '" + sDate.ToString().Replace(" 12:00:00 AM", "") + " 00:00:01' and '" + eDate.ToString().Replace(" 12:00:00 AM", "") + " 23:59:59'";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    TotalCOC.InnerHtml = theSqlDataReader2["total"].ToString();
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "Select count(*) as Total from COCStatements where isactive = '1' and Type = 'Electronic' and isStock = '0' and userid <> '0' and DatePurchased between '" + sDate.ToString().Replace(" 12:00:00 AM", "") + " 00:00:01' and '" + eDate.ToString().Replace(" 12:00:00 AM", "") + " 23:59:59'";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    ElectronicCOC.InnerHtml = theSqlDataReader2["total"].ToString();
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "Select count(*) as Total from COCStatements where isactive = '1' and Type = 'Paper' and isStock = '0' and userid <> '0' and DatePurchased between '" + sDate.ToString().Replace(" 12:00:00 AM", "") + " 00:00:01' and '" + eDate.ToString().Replace(" 12:00:00 AM", "") + " 23:59:59'";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    PaperCOC.InnerHtml = theSqlDataReader2["total"].ToString();
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            // COC STATUS
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select [status], count(*) as Total from COCStatements where [status] <> '' and isStock = '0' and userid <> '0' and CreateDate between '" + sDate.ToString().Replace(" 12:00:00 AM", "") + " 00:00:01' and '" + eDate.ToString().Replace(" 12:00:00 AM", "") + " 23:59:59' group by [status]";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    COCStatus += ",[\"" + theSqlDataReader2["status"].ToString() + "\", " + theSqlDataReader2["total"].ToString() + ", \"green\"]";
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select count(*) as Total from COCStatements c inner join COCInspectors i on c.COCStatementID = i.COCStatementID where i.[Status] = 'Auditing' and c.isActive = '1' and c.isRenege = '0' and c.DateAudited between '" + sDate.ToString().Replace(" 12:00:00 AM", "") + " 00:00:01' and '" + eDate.ToString().Replace(" 12:00:00 AM", "") + " 23:59:59'";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    if (theSqlDataReader2["Total"] != DBNull.Value)
                    {
                        Audits.InnerHtml = theSqlDataReader2["Total"].ToString();
                    }
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select count(*) as Total from (select c.COCStatementID from COCStatements c inner join COCReviews r on c.COCStatementID = r.COCStatementID where c.isactive = '1' and c.[Status] = 'Auditing' and c.isRenege = '0' and r.[status] = 'Failed' and c.DateAudited between '" + sDate.ToString().Replace(" 12:00:00 AM", "") + " 00:00:01' and '" + eDate.ToString().Replace(" 12:00:00 AM", "") + " 23:59:59' group by c.COCStatementID) as p";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    if (theSqlDataReader2["Total"] != DBNull.Value)
                    {
                        Refix.InnerHtml = theSqlDataReader2["Total"].ToString();
                    }
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            // GET YEAR STATS
            DateTime target = DateTime.Now;
            DateTime sDateY = target.AddYears(-1);
            DateTime eDateY = target;

            foreach (DateTime month in DLdb.EachMonth(sDateY, eDateY))
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select count(*) as total from COCStatements where isactive='1' and isStock = '0' and userid <> '0' and month(DatePurchased) = '" + month.ToString("MM") + "' and year(DatePurchased) = '" + month.ToString("yyyy") + "'";
                DLdb.SQLST.Parameters.AddWithValue("Param", "val");
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        Random rand = new Random();
                        int r = rand.Next(256);
                        int g = rand.Next(256);
                        int b = rand.Next(256);
                        Color col = Color.FromArgb(r, g, b);

                        COCDailySalesMonth += ",[\"" + month.ToString("MMM") + "\", " + theSqlDataReader["total"].ToString() + ", \"blue\"]";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "Select count(*) as total from Users where isactive='1' and isSuspended = '0' and role = 'Staff' and month(CreateDate) = '" + month.ToString("MM") + "' and year(createdate) = '" + month.ToString("yyyy") + "'";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        Random rand = new Random();
                        int r = rand.Next(256);
                        int g = rand.Next(256);
                        int b = rand.Next(256);
                        Color col = Color.FromArgb(r, g, b);

                        LicensedPlumbers += ",[\"" + month.ToString("MMM") + "\", " + theSqlDataReader["total"].ToString() + ", \"green\"]";
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

            }

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select * from InstallationTypes where isactive = '1'";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {

                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "select count(*) as total from COCStatements c inner join COCInstallations t on c.COCStatementID = t.COCStatementID where c.isactive = '1' and t.isactive = '1' and t.TypeID = @TypeID and DateLogged between '" + sDateY.Year.ToString() + "/" + sDateY.Month.ToString() + "/" + sDateY.Day.ToString() + " 00:00:01' and '" + eDateY.Year.ToString() + "/" + eDateY.Month.ToString() + "/" + eDateY.Day.ToString() + " 23:59:59'";
                    DLdb.SQLST3.Parameters.AddWithValue("TypeID", theSqlDataReader2["InstallationTypeID"].ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        while (theSqlDataReader3.Read())
                        {
                            if (theSqlDataReader3["total"] != DBNull.Value && theSqlDataReader3["total"].ToString() != "0")
                            {
                                InstallTypes += ",[\"" + theSqlDataReader2["InstallationType"].ToString() + "\", " + theSqlDataReader3["total"].ToString() + ", \"green\"]";
                            }
                            else
                            {
                                InstallTypes += ",[\"" + theSqlDataReader2["InstallationType"].ToString() + "\", 0, \"green\"]";
                            }
                        }
                    }
                    else
                    {
                        InstallTypes += ",[\"" + theSqlDataReader2["InstallationType"].ToString() + "\", 0, \"green\"]";
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();

                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            // GET TOTALS
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "Select count(*) as Total from COCStatements where isactive = '1' and isStock = '0' and userid <> '0' and DatePurchased between '" + sDateY.Year.ToString() + "/" + sDateY.Month.ToString() + "/" + sDateY.Day.ToString() + " 00:00:01' and '" + eDateY.Year.ToString() + "/" + eDateY.Month.ToString() + "/" + eDateY.Day.ToString() + " 23:59:59'";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    TotalCOCYear.InnerHtml = theSqlDataReader2["total"].ToString();
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "Select count(*) as Total from COCStatements where isactive = '1' and Type = 'Electronic' and isStock = '0' and userid <> '0' and DatePurchased between '" + sDateY.Year.ToString() + "/" + sDateY.Month.ToString() + "/" + sDateY.Day.ToString() + " 00:00:01' and '" + eDateY.Year.ToString() + "/" + eDateY.Month.ToString() + "/" + eDateY.Day.ToString() + " 23:59:59'";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    ElectronicCOCYear.InnerHtml = theSqlDataReader2["total"].ToString();
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "Select count(*) as Total from COCStatements where isactive = '1' and Type = 'Paper' and isStock = '0' and userid <> '0' and DatePurchased between '" + sDateY.Year.ToString() + "/" + sDateY.Month.ToString() + "/" + sDateY.Day.ToString() + " 00:00:01' and '" + eDateY.Year.ToString() + "/" + eDateY.Month.ToString() + "/" + eDateY.Day.ToString() + " 23:59:59'";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    PaperCOCYear.InnerHtml = theSqlDataReader2["total"].ToString();
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            // COC STATUS
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select [status], count(*) as Total from COCStatements where [status] <> '' and isStock = '0' and userid <> '0' and CreateDate between '" + sDateY.Year.ToString() + "/" + sDateY.Month.ToString() + "/" + sDateY.Day.ToString() + " 00:00:01' and '" + eDateY.Year.ToString() + "/" + eDateY.Month.ToString() + "/" + eDateY.Day.ToString() + " 23:59:59' group by [status]";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    COCStatusYear += ",[\"" + theSqlDataReader2["status"].ToString() + "\", " + theSqlDataReader2["total"].ToString() + ", \"green\"]";
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select count(*) as Total from COCStatements c inner join COCInspectors i on c.COCStatementID = i.COCStatementID where i.[Status] = 'Auditing' and c.isRenege = '0' and c.isActive = '1' and c.DateAudited between '" + sDateY.Year.ToString() + "/" + sDateY.Month.ToString() + "/" + sDateY.Day.ToString() + " 00:00:01' and '" + eDateY.Year.ToString() + "/" + eDateY.Month.ToString() + "/" + eDateY.Day.ToString() + " 23:59:59'";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    if (theSqlDataReader2["Total"] != DBNull.Value)
                    {
                        AuditsYear.InnerHtml = theSqlDataReader2["Total"].ToString();
                    }
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select count(*) as Total from (select c.COCStatementID from COCStatements c inner join COCReviews r on c.COCStatementID = r.COCStatementID where c.isactive = '1' and c.[Status] = 'Auditing' and c.isRenege = '0' and r.[status] = 'Failed' and c.DateAudited between '" + sDateY.Year.ToString() + "/" + sDateY.Month.ToString() + "/" + sDateY.Day.ToString() + " 00:00:01' and '" + eDateY.Year.ToString() + "/" + eDateY.Month.ToString() + "/" + eDateY.Day.ToString() + " 23:59:59' group by c.COCStatementID) as p";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    if (theSqlDataReader2["Total"] != DBNull.Value)
                    {
                        RefixYear.InnerHtml = theSqlDataReader2["Total"].ToString();
                    }
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();



            DLdb.DB_Close();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

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

        }
    }
}