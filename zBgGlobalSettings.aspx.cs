using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InspectIT
{
    public partial class zBgGlobalSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            // run monthly
            

            decimal totalCPDpoints = 0;
            string UserID = "";

            decimal numberPlumbers = 0;
            decimal topnumberPlumbersPoints = 0;
            decimal mostCOCs = 0;
            decimal minNumberCpdPoints = 0;
            decimal minCpdPoints = 0;
            decimal numberPerformancePoints = 0;
            decimal PerformancePoints = 0;
            decimal sarsPoints = 0;
            decimal numberPlumbersPerCompany = 0;
            decimal pointsPlumbersPerCompany = 0;
            decimal logInDaily = 0;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from GlobalSettings where GlobalID='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                numberPlumbers = Convert.ToDecimal(theSqlDataReader["six"].ToString());
                topnumberPlumbersPoints = Convert.ToDecimal(theSqlDataReader["seven"].ToString());
                mostCOCs = Convert.ToDecimal(theSqlDataReader["eight"].ToString());
                minNumberCpdPoints = Convert.ToDecimal(theSqlDataReader["nine"].ToString());
                minCpdPoints = Convert.ToDecimal(theSqlDataReader["ten"].ToString());
                numberPerformancePoints = Convert.ToDecimal(theSqlDataReader["four"].ToString());
                PerformancePoints = Convert.ToDecimal(theSqlDataReader["five"].ToString());
                sarsPoints = Convert.ToDecimal(theSqlDataReader["twelve"].ToString());
                numberPlumbersPerCompany = Convert.ToDecimal(theSqlDataReader["thirteen"].ToString());
                pointsPlumbersPerCompany = Convert.ToDecimal(theSqlDataReader["fourteen"].ToString());
                logInDaily = Convert.ToDecimal(theSqlDataReader["fifteen"].ToString());
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();
            
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select top " + numberPlumbers + " ROW_NUMBER() OVER(ORDER BY sum(cast(Points as decimal)) desc) AS Row,sum(cast(Points as decimal)) as tot,u.UserID,u.fname,u.lname from users u inner join AssignedWeighting a on u.UserID=a.UserID where a.createdate between dateadd(day, -30, getdate()) and getdate() group by u.UserID,u.fname,u.lname";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    UserID = theSqlDataReader["UserID"].ToString();
                    totalCPDpoints = Convert.ToDecimal(theSqlDataReader["tot"].ToString());
                   
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into  AssignedWeighting (UserID,WeightingID,Points,Type,weightingValue) values (@UserID,@WeightingID,@Points,'Global Setting Top Plumber',@weightingValue)";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", UserID.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("WeightingID", "0");
                    DLdb.SQLST2.Parameters.AddWithValue("Points", topnumberPlumbersPoints.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("weightingValue", topnumberPlumbersPoints.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select top 1 count(cocstatementid) as counted,u.userid from users u inner join COCStatements c on u.UserID=c.UserID where c.Status='Logged' and c.createdate between dateadd(day, -30, getdate()) and getdate() group by u.userid order by counted desc";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    UserID = theSqlDataReader["UserID"].ToString();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into  AssignedWeighting (UserID,WeightingID,Points,Type,weightingValue) values (@UserID,@WeightingID,@Points,'Global Setting Most COCs logged',@weightingValue)";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", UserID.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("WeightingID", "0");
                    DLdb.SQLST2.Parameters.AddWithValue("Points", mostCOCs.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("weightingValue", mostCOCs.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select sum(NoPoints) as sums,UserID from Assessments where createdate between dateadd(day, -30, getdate()) and getdate() and isActive='1' and isApproved='1' group by userid";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    UserID = theSqlDataReader["UserID"].ToString();
                    if (minNumberCpdPoints <= Convert.ToDecimal(theSqlDataReader["sums"]))
                    {

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into  AssignedWeighting (UserID,WeightingID,Points,Type,weightingValue) values (@UserID,@WeightingID,@Points,'Global Setting "+ theSqlDataReader["sums"].ToString() + " CPD points aquired',@weightingValue)";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", UserID.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("WeightingID", "0");
                    DLdb.SQLST2.Parameters.AddWithValue("Points", minCpdPoints.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("weightingValue", minCpdPoints.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    }
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select sum(cast(PerformancePointAllocation as decimal)) as cnt,userid from PerformanceStatus where createdate between dateadd(day, -30, getdate()) and getdate() group by userid";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    UserID = theSqlDataReader["UserID"].ToString();
                    if (numberPerformancePoints <= Convert.ToDecimal(theSqlDataReader["cnt"]))
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "insert into  AssignedWeighting (UserID,WeightingID,Points,Type,weightingValue) values (@UserID,@WeightingID,@Points,'Global Setting " + theSqlDataReader["cnt"].ToString() + " Performance points aquired',@weightingValue)";
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", UserID.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("WeightingID", "0");
                        DLdb.SQLST2.Parameters.AddWithValue("Points", PerformancePoints.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("weightingValue", PerformancePoints.ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();

                    }
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            string CompanyID = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Companies where isActive='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    CompanyID = theSqlDataReader["CompanyID"].ToString();
                    if (theSqlDataReader["SARSCertificate"].ToString() != "" && theSqlDataReader["SARSCertificate"] != DBNull.Value)
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "insert into  AssignedWeighting (CompanyID,WeightingID,Points,Type,weightingValue) values (@CompanyID,@WeightingID,@Points,'Global Setting has SARS Certificate',@weightingValue)";
                        DLdb.SQLST2.Parameters.AddWithValue("CompanyID", CompanyID.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("WeightingID", "0");
                        DLdb.SQLST2.Parameters.AddWithValue("Points", sarsPoints.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("weightingValue", sarsPoints.ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                    }
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Companies where isActive='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    int LicensedCount = 0;
                    CompanyID = theSqlDataReader["CompanyID"].ToString();

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from users where company=@company";
                    DLdb.SQLST2.Parameters.AddWithValue("company", theSqlDataReader["companyid"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "select * from PlumberDesignations where PlumberID=@PlumberID";
                            DLdb.SQLST3.Parameters.AddWithValue("PlumberID", theSqlDataReader2["UserID"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {
                                    if (theSqlDataReader3["Designation"].ToString() == "Licensed Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Director Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Master Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                }
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    decimal divisionPlumbers = numberPlumbersPerCompany / LicensedCount;
                    decimal roundedNumber = Decimal.Round(divisionPlumbers);
                    decimal totalPoints = roundedNumber * pointsPlumbersPerCompany;
                    
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "insert into  AssignedWeighting (CompanyID,WeightingID,Points,Type,weightingValue) values (@CompanyID,@WeightingID,@Points,'Global Setting Bonus Points per "+ numberPlumbersPerCompany + " of plumbers',@weightingValue)";
                        DLdb.SQLST2.Parameters.AddWithValue("CompanyID", CompanyID.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("WeightingID", "0");
                        DLdb.SQLST2.Parameters.AddWithValue("Points", totalPoints.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("weightingValue", pointsPlumbersPerCompany.ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                    
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();


            int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where isActive='1' and Role='Staff'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    UserID = theSqlDataReader["UserID"].ToString();
                    int cntDays = 0;
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select count(*) as cnt from LoggedInRecords where UserID=@UserID group by createdate";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            cntDays++;
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    if (cntDays == days)
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "insert into  AssignedWeighting (UserID,WeightingID,Points,Type,weightingValue) values (@UserID,@WeightingID,@Points,'Global Setting Log in daily',@weightingValue)";
                        DLdb.SQLST2.Parameters.AddWithValue("UserID", UserID.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("WeightingID", "0");
                        DLdb.SQLST2.Parameters.AddWithValue("Points", logInDaily.ToString());
                        DLdb.SQLST2.Parameters.AddWithValue("weightingValue", logInDaily.ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                    }
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();


            DLdb.DB_Close();
        }
    }
}