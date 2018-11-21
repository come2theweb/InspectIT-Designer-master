using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace InspectIT
{
    public partial class zImportSpecialisations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_PIRB_Connect();

            string uid = "";
            // LOAD PLUMBER INFORMATION
            DLdb.pirbRS.Open();
            DLdb.pirbSQLST.CommandText = "select * from plumber";
            //DLdb.SQLST.Parameters.AddWithValue("ID", theSqlDataReader["ID"].ToString());
            DLdb.pirbSQLST.CommandType = CommandType.Text;
            DLdb.pirbSQLST.Connection = DLdb.pirbRS;
            SqlDataReader theSqlDataReader = DLdb.pirbSQLST.ExecuteReader();

            string Specialisation = "";

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {

                    uid = theSqlDataReader["ID"].ToString();
                    // PlumberQualification
                    DLdb.pirbRS2.Open();
                    DLdb.pirbSQLST2.CommandText = "select * from PlumberQualification where PlumberID=@PlumberID";
                    DLdb.pirbSQLST2.Parameters.AddWithValue("PlumberID", theSqlDataReader["ID"].ToString());
                    DLdb.pirbSQLST2.CommandType = CommandType.Text;
                    DLdb.pirbSQLST2.Connection = DLdb.pirbRS2;
                    SqlDataReader theSqlDataReader2 = DLdb.pirbSQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            // QualificationType

                            DLdb.pirbRS3.Open();
                            DLdb.pirbSQLST3.CommandText = "select * from QualificationType where ID=@ID and isDesignations='0' and isSpeciliasations='1'";
                            DLdb.pirbSQLST3.Parameters.AddWithValue("ID", theSqlDataReader2["QualificationTypeID"].ToString());
                            DLdb.pirbSQLST3.CommandType = CommandType.Text;
                            DLdb.pirbSQLST3.Connection = DLdb.pirbRS3;
                            SqlDataReader theSqlDataReader3 = DLdb.pirbSQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                theSqlDataReader3.Read();
                                Specialisation = theSqlDataReader3["stuff"].ToString();
                                //PlumbersList.InnerHtml += "<tr>" +
                                //                            "<td>" + theSqlDataReader2["PlumberID"].ToString() + "</td>" +
                                //                            "<td>" + Specialisation + "</td>" +
                                //                      "</tr>";

                                DLdb.DB_Connect();
                                // INSERT INTO PlumberSpecialisations
                                DLdb.RS4.Open();
                                DLdb.SQLST4.CommandText = "insert into PlumberSpecialisations (PlumberID,Specialisation)" +
                                    "values (@PlumberID,@Specialisation)";
                                DLdb.SQLST4.Parameters.AddWithValue("PlumberID", theSqlDataReader["ID"].ToString());
                                DLdb.SQLST4.Parameters.AddWithValue("Specialisation", Specialisation);
                                DLdb.SQLST4.CommandType = CommandType.Text;
                                DLdb.SQLST4.Connection = DLdb.RS4;
                                SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();


                                if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                                DLdb.SQLST4.Parameters.RemoveAt(0);
                                DLdb.SQLST4.Parameters.RemoveAt(0);
                                DLdb.RS4.Close();

                                DLdb.DB_Close();
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.pirbSQLST3.Parameters.RemoveAt(0);
                            DLdb.pirbRS3.Close();



                        }

                    }

                    // INSERT



                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.pirbSQLST2.Parameters.RemoveAt(0);
                    DLdb.pirbRS2.Close();




                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.pirbRS.Close();


            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from PlumberSpecialisations where PlumberID=@PlumberID";
            DLdb.SQLST.Parameters.AddWithValue("PlumberID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM badges where Item=@Item";
                    DLdb.SQLST2.Parameters.AddWithValue("Item", theSqlDataReader["Specialisations"].ToString());
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
                            DLdb.SQLST3.Parameters.AddWithValue("UserID", uid.ToString());
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
                                DLdb.SQLST4.CommandText = "insert into AssignedBadges (@UserID,@BadgeID) values (@UserID,@BadgeID)";
                                DLdb.SQLST4.Parameters.AddWithValue("UserID", uid.ToString());
                                DLdb.SQLST4.Parameters.AddWithValue("BadgeID", theSqlDataReader["BadgeID"].ToString());
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

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from DesignationSpecialisationPoints where Item@Item";
                    DLdb.SQLST2.Parameters.AddWithValue("Item", theSqlDataReader["Specialisations"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();
                    if (theSqlDataReader2.HasRows)
                    {
                        theSqlDataReader2.Read();

                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "SELECT * FROM AssignedDesignationSpecialisationPoints where PointID=@PointID and UserID=@UserID";
                        DLdb.SQLST3.Parameters.AddWithValue("PointID", theSqlDataReader2["PointID"].ToString());
                        DLdb.SQLST3.Parameters.AddWithValue("UserID", uid.ToString());
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
                            DLdb.SQLST4.CommandText = "insert into AssignedDesignationSpecialisationPoints (UserID,PointID,Points) values (@UserID,@PointID,@Points)";
                            DLdb.SQLST4.Parameters.AddWithValue("UserID", uid.ToString());
                            DLdb.SQLST4.Parameters.AddWithValue("PointID", theSqlDataReader["PointID"].ToString());
                            DLdb.SQLST4.Parameters.AddWithValue("Points", theSqlDataReader["Points"].ToString());
                            DLdb.SQLST4.CommandType = CommandType.Text;
                            DLdb.SQLST4.Connection = DLdb.RS4;
                            SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                            if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                            DLdb.SQLST4.Parameters.RemoveAt(0);
                            DLdb.SQLST4.Parameters.RemoveAt(0);
                            DLdb.SQLST4.Parameters.RemoveAt(0);
                            DLdb.RS4.Close();

                        }

                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.RS3.Close();
                    }
                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_PIRB_Close();
        }
    }
}