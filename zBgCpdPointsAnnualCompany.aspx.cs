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
    public partial class zBgCpdPointsAnnualCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            // run annually
            decimal CompanyPoints = 0;
            string CompanyID = "";
            string itemSelect = "";


            // national badges run yearly
            string placement = "";
            string UserIDs = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select top 50 ROW_NUMBER() OVER(ORDER BY sum(cast(TotalPoints as decimal)) desc) AS Row,sum(cast(TotalPoints as decimal)) as tot,u.CompanyID,u.CompanyName from Companies u group by u.CompanyID,u.CompanyName";
            // DLdb.SQLST.Parameters.AddWithValue("UserID", theSqlDataReader["UserID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    decimal rowNumber = Convert.ToDecimal(theSqlDataReader["Row"].ToString());
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
                        placement += ",3rd Place";
                    }
                    if (rowNumber == 2)
                    {
                        placement += ",2nd Place";
                    }
                    if (rowNumber == 1)
                    {
                        placement += ",Top Current CPD";
                    }

                    List<string> ParamsListNew = placement.Split(',').ToList<string>();
                    foreach (string badgeItem in ParamsListNew)
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "SELECT * FROM badges where Item=@Item and Section='Company National CPD'";
                        DLdb.SQLST2.Parameters.AddWithValue("Item", badgeItem.ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                DLdb.RS3.Open();
                                DLdb.SQLST3.CommandText = "SELECT * FROM AssignedBadges where BadgeID=@BadgeID and CompanyID=@CompanyID";
                                DLdb.SQLST3.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                DLdb.SQLST3.Parameters.AddWithValue("CompanyID", CompanyID.ToString());
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
                                    DLdb.SQLST4.CommandText = "insert into AssignedBadges (CompanyID,BadgeID) values (@CompanyID,@BadgeID)";
                                    DLdb.SQLST4.Parameters.AddWithValue("CompanyID", CompanyID.ToString());
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

            //regional badges
            DLdb.RS5.Open();
            DLdb.SQLST5.CommandText = "select * from Province";
            DLdb.SQLST5.CommandType = CommandType.Text;
            DLdb.SQLST5.Connection = DLdb.RS5;
            SqlDataReader theSqlDataReader5 = DLdb.SQLST5.ExecuteReader();
            if (theSqlDataReader5.HasRows)
            {
                while (theSqlDataReader5.Read())
                {
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select top 50 ROW_NUMBER() OVER(ORDER BY sum(cast(TotalPoints as decimal)) desc) AS Row,sum(cast(TotalPoints as decimal)) as tot,u.CompanyID,u.CompanyName,u.Province from Companies u where u.Province=@Province group by u.CompanyID,u.CompanyName,u.Province";
                    DLdb.SQLST.Parameters.AddWithValue("Province", theSqlDataReader5["Name"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            decimal rowNumber = Convert.ToDecimal(theSqlDataReader["Row"].ToString());
                            if (rowNumber <= 20)
                            {
                                placement += ",Top 50";
                            }
                            if (rowNumber <= 10)
                            {
                                placement += ",Top 10";
                            }
                            if (rowNumber == 3)
                            {
                                placement += ",3rd Place";
                            }
                            if (rowNumber == 2)
                            {
                                placement += ",2nd Place";
                            }
                            if (rowNumber == 1)
                            {
                                placement += ",Top Current";
                            }


                            List<string> ParamsListNew = placement.Split(',').ToList<string>();
                            foreach (string badgeItem in ParamsListNew)
                            {
                                DLdb.RS2.Open();
                                DLdb.SQLST2.CommandText = "SELECT * FROM badges where Item=@Item and Section='Company Regional CPD'";
                                DLdb.SQLST2.Parameters.AddWithValue("Item", badgeItem.ToString());
                                DLdb.SQLST2.CommandType = CommandType.Text;
                                DLdb.SQLST2.Connection = DLdb.RS2;
                                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                                if (theSqlDataReader2.HasRows)
                                {
                                    while (theSqlDataReader2.Read())
                                    {
                                        DLdb.RS3.Open();
                                        DLdb.SQLST3.CommandText = "SELECT * FROM AssignedBadges where BadgeID=@BadgeID and CompanyID=@CompanyID";
                                        DLdb.SQLST3.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                        DLdb.SQLST3.Parameters.AddWithValue("CompanyID", CompanyID.ToString());
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
                                            DLdb.SQLST4.CommandText = "insert into AssignedBadges (CompanyID,BadgeID) values (@CompanyID,@BadgeID)";
                                            DLdb.SQLST4.Parameters.AddWithValue("CompanyID", CompanyID.ToString());
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
                    DLdb.SQLST.CommandText = "select top 50 ROW_NUMBER() OVER(ORDER BY sum(cast(TotalPoints as decimal)) desc) AS Row,sum(cast(TotalPoints as decimal)) as tot,u.CompanyID,u.CompanyName,u.City from Companies u where u.City=@City group by u.CompanyID,u.CompanyName,u.City";
                    DLdb.SQLST.Parameters.AddWithValue("City", theSqlDataReader5["Name"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            decimal rowNumber = Convert.ToDecimal(theSqlDataReader["Row"].ToString());
                            if (rowNumber <= 20)
                            {
                                placement += ",Top 50";
                            }
                            if (rowNumber <= 10)
                            {
                                placement += ",Top 10";
                            }
                            if (rowNumber == 3)
                            {
                                placement += ",3rd Place";
                            }
                            if (rowNumber == 2)
                            {
                                placement += ",2nd Place";
                            }
                            if (rowNumber == 1)
                            {
                                placement += ",Top Current";
                            }


                            List<string> ParamsListNew = placement.Split(',').ToList<string>();
                            foreach (string badgeItem in ParamsListNew)
                            {
                                DLdb.RS2.Open();
                                DLdb.SQLST2.CommandText = "SELECT * FROM badges where Item=@Item and Section='Company City CPD'";
                                DLdb.SQLST2.Parameters.AddWithValue("Item", badgeItem.ToString());
                                DLdb.SQLST2.CommandType = CommandType.Text;
                                DLdb.SQLST2.Connection = DLdb.RS2;
                                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                                if (theSqlDataReader2.HasRows)
                                {
                                    while (theSqlDataReader2.Read())
                                    {
                                        DLdb.RS3.Open();
                                        DLdb.SQLST3.CommandText = "SELECT * FROM AssignedBadges where BadgeID=@BadgeID and CompanyID=@CompanyID";
                                        DLdb.SQLST3.Parameters.AddWithValue("BadgeID", theSqlDataReader2["BadgeID"].ToString());
                                        DLdb.SQLST3.Parameters.AddWithValue("CompanyID", CompanyID.ToString());
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
                                            DLdb.SQLST4.CommandText = "insert into AssignedBadges (CompanyID,BadgeID) values (@CompanyID,@BadgeID)";
                                            DLdb.SQLST4.Parameters.AddWithValue("CompanyID", CompanyID.ToString());
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