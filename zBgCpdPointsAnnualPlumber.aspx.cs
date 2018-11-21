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
    public partial class zBgCpdPointsAnnualPlumber : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            // run annually
            decimal totalCPDpoints = 0;
            string UserID = "";
            string itemSelect = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users where isactive='1' and Status='Active'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    UserID = theSqlDataReader["UserID"].ToString();
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT sum(NoPoints) as totPoinnt FROM Assessments where isApproved='1' and UserID=@UserID";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();
                        totalCPDpoints = Convert.ToDecimal(theSqlDataReader2["totPoinnt"].ToString());
                    }
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    if (totalCPDpoints == 25)
                    {
                        itemSelect = "Minimal Value Achieved";
                    }
                    else if (totalCPDpoints == 50)
                    {
                        itemSelect = "CPD x2 Minimal Value";
                    }
                    else if (totalCPDpoints == 75)
                    {
                        itemSelect = "CPD x3 Minimal Value";
                    }
                    else if (totalCPDpoints == 100)
                    {
                        itemSelect = "CPD x4 Minimal Value";
                    }
                    else if (totalCPDpoints == 125)
                    {
                        itemSelect = "CPD x5 Minimal Value";
                    }

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM badges where Item=@Item";
                    DLdb.SQLST2.Parameters.AddWithValue("Item", itemSelect.ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "SELECT * FROM AssignedBadges where BadgeID=@BadgeID and UserID=@UserID";
                            DLdb.SQLST3.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                            DLdb.SQLST3.Parameters.AddWithValue("UserID", UserID.ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {

                                }
                            }
                            else
                            {
                                DLdb.RS4.Open();
                                DLdb.SQLST4.CommandText = "insert into AssignedBadges (UserID,BadgeID) values (@UserID,@BadgeID)";
                                DLdb.SQLST4.Parameters.AddWithValue("UserID", UserID.ToString());
                                DLdb.SQLST4.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                DLdb.SQLST4.CommandType = CommandType.Text;
                                DLdb.SQLST4.Connection = DLdb.RS4;
                                SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                                if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                                DLdb.SQLST4.Parameters.RemoveAt(0);
                                DLdb.SQLST4.Parameters.RemoveAt(0);
                                DLdb.RS4.Close();

                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();



                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            // national badges run yearly
            string placement = "";
            string UserIDs = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select top 50 ROW_NUMBER() OVER(ORDER BY sum(NoPoints) desc) AS Row,sum(NoPoints) as tot,u.UserID,u.fname,u.lname from users u inner join Assessments a on u.UserID=a.UserID group by u.UserID,u.fname,u.lname";
            // DLdb.SQLST.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    decimal rowNumber = Convert.ToDecimal(theSqlDataReader["Row"].ToString());
                    UserIDs = theSqlDataReader["UserID"].ToString();
                    if (rowNumber <= 50)
                    {
                        placement += ",Top 50";
                    }
                    if (rowNumber <= 10)
                    {
                        placement += ",Top 10";
                    }
                    if (rowNumber == 3)
                    {
                        placement += ",3rd Place CPD Earner";
                    }
                    if (rowNumber == 2)
                    {
                        placement += ",2nd Place CPD Earner";
                    }
                    if (rowNumber == 1)
                    {
                        placement += ",Top Current CPD";
                    }

                    List<string> ParamsListNew = placement.Split(',').ToList<string>();
                    foreach (string badgeItem in ParamsListNew)
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM badges where Item=@Item and Section='National CPD'";
                        DLdb.SQLST2.Parameters.AddWithValue("Item", badgeItem.ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                DLdb.RS3.Open();
                                DLdb.SQLST3.CommandText = "SELECT * FROM AssignedBadges where BadgeID=@BadgeID and UserID=@UserID";
                                DLdb.SQLST3.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("UserID", UserIDs.ToString());
                                DLdb.SQLST3.CommandType = CommandType.Text;
                                DLdb.SQLST3.Connection = DLdb.RS3;
                                SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                                if (theSqlDataReader3.HasRows)
                                {
                                    while (theSqlDataReader3.Read())
                                    {

                                    }
                                }
                                else
                                {
                                    DLdb.RS4.Open();
                                    DLdb.SQLST4.CommandText = "insert into AssignedBadges (UserID,BadgeID) values (@UserID,@BadgeID)";
                                    DLdb.SQLST4.Parameters.AddWithValue("UserID", UserIDs.ToString());
                                    DLdb.SQLST4.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                    DLdb.SQLST4.CommandType = CommandType.Text;
                                    DLdb.SQLST4.Connection = DLdb.RS4;
                                    SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                                    if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                                    DLdb.SQLST4.Parameters.RemoveAt(0);
                                    DLdb.SQLST4.Parameters.RemoveAt(0);
                                    DLdb.RS4.Close();

                                }

                                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                                DLdb.SQLST3.Parameters.RemoveAt(0);
                                DLdb.SQLST3.Parameters.RemoveAt(0);
                                DLdb.RS3.Close();
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                    }
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            // DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            DLdb.RS5.Open();
            DLdb.SQLST5.CommandText = "select * from Province";
            DLdb.SQLST5.CommandType = CommandType.Text;
            DLdb.SQLST5.Connection = DLdb.RS5;
            SqlDataReader theSqlDataReader5 = DLdb.SQLST5.ExecuteReader();
            if (theSqlDataReader5.HasRows)
            {
                while (theSqlDataReader5.Read())
                {
                    //regional badges
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select top 20 ROW_NUMBER() OVER(ORDER BY sum(NoPoints) desc) AS Row,sum(NoPoints) as tot,u.UserID,u.fname,u.lname,u.Province from users u inner join Assessments a on u.UserID=a.UserID where u.Province=@Province group by u.UserID,u.fname,u.lname,u.Province";
                    DLdb.SQLST.Parameters.AddWithValue("Province", theSqlDataReader5["Name"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            decimal rowNumber = Convert.ToDecimal(theSqlDataReader["Row"].ToString());
                            UserIDs = theSqlDataReader["UserID"].ToString();
                            if (rowNumber <= 20)
                            {
                                placement += ",Top 20";
                            }
                            if (rowNumber <= 10)
                            {
                                placement += ",Top 10";
                            }
                            if (rowNumber == 3)
                            {
                                placement += ",3rd Place CPD Earner";
                            }
                            if (rowNumber == 2)
                            {
                                placement += ",2nd Place CPD Earner";
                            }
                            if (rowNumber == 1)
                            {
                                placement += ",Top Current CPD";
                            }


                            List<string> ParamsListNew = placement.Split(',').ToList<string>();
                            foreach (string badgeItem in ParamsListNew)
                            {
                                DLdb.RS2.Open();
                                DLdb.SQLST2.CommandText = "SELECT * FROM badges where Item=@Item and Section='Regional CPD'";
                                DLdb.SQLST2.Parameters.AddWithValue("Item", badgeItem.ToString());
                                DLdb.SQLST2.CommandType = CommandType.Text;
                                DLdb.SQLST2.Connection = DLdb.RS2;
                                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                                if (theSqlDataReader2.HasRows)
                                {
                                    while (theSqlDataReader2.Read())
                                    {
                                        DLdb.RS3.Open();
                                        DLdb.SQLST3.CommandText = "SELECT * FROM AssignedBadges where BadgeID=@BadgeID and UserID=@UserID";
                                        DLdb.SQLST3.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                        DLdb.SQLST3.Parameters.AddWithValue("UserID", UserIDs.ToString());
                                        DLdb.SQLST3.CommandType = CommandType.Text;
                                        DLdb.SQLST3.Connection = DLdb.RS3;
                                        SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                                        if (theSqlDataReader3.HasRows)
                                        {
                                            while (theSqlDataReader3.Read())
                                            {

                                            }
                                        }
                                        else
                                        {
                                            DLdb.RS4.Open();
                                            DLdb.SQLST4.CommandText = "insert into AssignedBadges (UserID,BadgeID) values (@UserID,@BadgeID)";
                                            DLdb.SQLST4.Parameters.AddWithValue("UserID", UserIDs.ToString());
                                            DLdb.SQLST4.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                            DLdb.SQLST4.CommandType = CommandType.Text;
                                            DLdb.SQLST4.Connection = DLdb.RS4;
                                            SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                                            if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                                            DLdb.SQLST4.Parameters.RemoveAt(0);
                                            DLdb.SQLST4.Parameters.RemoveAt(0);
                                            DLdb.RS4.Close();

                                        }

                                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                                        DLdb.SQLST3.Parameters.RemoveAt(0);
                                        DLdb.SQLST3.Parameters.RemoveAt(0);
                                        DLdb.RS3.Close();
                                    }
                                }

                                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.RS2.Close();
                            }
                        }
                    }
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }
            }
            if (theSqlDataReader5.IsClosed) theSqlDataReader5.Close();
            DLdb.RS5.Close();
            //city badges
            DLdb.RS.Close();
            DLdb.RS5.Open();
            DLdb.SQLST5.CommandText = "select * from Area";
            DLdb.SQLST5.CommandType = CommandType.Text;
            DLdb.SQLST5.Connection = DLdb.RS5;
            theSqlDataReader5 = DLdb.SQLST5.ExecuteReader();
            if (theSqlDataReader5.HasRows)
            {
                while (theSqlDataReader5.Read())
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select top 20 ROW_NUMBER() OVER(ORDER BY sum(NoPoints) desc) AS Row,sum(NoPoints) as tot,u.UserID,u.fname,u.lname,u.ResidentialCity from users u inner join Assessments a on u.UserID=a.UserID where u.ResidentialCity=@ResidentialCity group by u.UserID,u.fname,u.lname,u.ResidentialCity";
                    DLdb.SQLST.Parameters.AddWithValue("ResidentialCity", theSqlDataReader["Name"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            decimal rowNumber = Convert.ToDecimal(theSqlDataReader["Row"].ToString());
                            UserIDs = theSqlDataReader["UserID"].ToString();
                            if (rowNumber <= 20)
                            {
                                placement += ",Top 20";
                            }
                            if (rowNumber <= 10)
                            {
                                placement += ",Top 10";
                            }
                            if (rowNumber == 3)
                            {
                                placement += ",3rd Place CPD Earner";
                            }
                            if (rowNumber == 2)
                            {
                                placement += ",2nd Place CPD Earner";
                            }
                            if (rowNumber == 1)
                            {
                                placement += ",Top Current CPD";
                            }

                            List<string> ParamsListNew = placement.Split(',').ToList<string>();
                            foreach (string badgeItem in ParamsListNew)
                            {
                                DLdb.RS2.Open();
                                DLdb.SQLST2.CommandText = "SELECT * FROM badges where Item=@Item and Section='City CPD'";
                                DLdb.SQLST2.Parameters.AddWithValue("Item", badgeItem.ToString());
                                DLdb.SQLST2.CommandType = CommandType.Text;
                                DLdb.SQLST2.Connection = DLdb.RS2;
                                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                                if (theSqlDataReader2.HasRows)
                                {
                                    while (theSqlDataReader2.Read())
                                    {
                                        DLdb.RS3.Open();
                                        DLdb.SQLST3.CommandText = "SELECT * FROM AssignedBadges where BadgeID=@BadgeID and UserID=@UserID";
                                        DLdb.SQLST3.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                        DLdb.SQLST3.Parameters.AddWithValue("UserID", UserIDs.ToString());
                                        DLdb.SQLST3.CommandType = CommandType.Text;
                                        DLdb.SQLST3.Connection = DLdb.RS3;
                                        SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                                        if (theSqlDataReader3.HasRows)
                                        {
                                            while (theSqlDataReader3.Read())
                                            {

                                            }
                                        }
                                        else
                                        {
                                            DLdb.RS4.Open();
                                            DLdb.SQLST4.CommandText = "insert into AssignedBadges (UserID,BadgeID) values (@UserID,@BadgeID)";
                                            DLdb.SQLST4.Parameters.AddWithValue("UserID", UserIDs.ToString());
                                            DLdb.SQLST4.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                            DLdb.SQLST4.CommandType = CommandType.Text;
                                            DLdb.SQLST4.Connection = DLdb.RS4;
                                            SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                                            if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                                            DLdb.SQLST4.Parameters.RemoveAt(0);
                                            DLdb.SQLST4.Parameters.RemoveAt(0);
                                            DLdb.RS4.Close();

                                        }

                                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                                        DLdb.SQLST3.Parameters.RemoveAt(0);
                                        DLdb.SQLST3.Parameters.RemoveAt(0);
                                        DLdb.RS3.Close();
                                    }
                                }

                                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.RS2.Close();
                            }
                        }
                    }
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }
            }
            if (theSqlDataReader5.IsClosed) theSqlDataReader5.Close();
            DLdb.RS5.Close();
            DLdb.DB_Close();
        }
    }
}