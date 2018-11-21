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
    public partial class zImportQualification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_PIRB_Connect();

            // LOAD PLUMBER INFORMATION
            DLdb.pirbRS.Open();
            DLdb.pirbSQLST.CommandText = "select * from plumber";
            //DLdb.SQLST.Parameters.AddWithValue("ID", theSqlDataReader["ID"].ToString());
            DLdb.pirbSQLST.CommandType = CommandType.Text;
            DLdb.pirbSQLST.Connection = DLdb.pirbRS;
            SqlDataReader theSqlDataReader = DLdb.pirbSQLST.ExecuteReader();

            string QualificationTypeID = "";

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {

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
                            DLdb.pirbSQLST3.CommandText = "select * from QualificationType where ID=@ID";
                            DLdb.pirbSQLST3.Parameters.AddWithValue("ID", theSqlDataReader2["QualificationTypeID"].ToString());
                            DLdb.pirbSQLST3.CommandType = CommandType.Text;
                            DLdb.pirbSQLST3.Connection = DLdb.pirbRS3;
                            SqlDataReader theSqlDataReader3 = DLdb.pirbSQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                theSqlDataReader3.Read();
                                QualificationTypeID = theSqlDataReader3["Description"].ToString();
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.pirbSQLST3.Parameters.RemoveAt(0);
                            DLdb.pirbRS3.Close();

                            //PlumbersList.InnerHtml += "<tr>" +
                            //                                "<td>" + theSqlDataReader2["PlumberID"].ToString() + "</td>" +
                            //                                "<td>" + theSqlDataReader2["CourseDateStart"].ToString() + "</td>" +
                            //                                "<td>" + theSqlDataReader2["CourseDateCompleted"].ToString() + "</td>" +
                            //                                "<td>" + theSqlDataReader2["TrainingProvider"].ToString() + "</td>" +
                            //                                "<td>" + theSqlDataReader2["CertificationNo"].ToString() + "</td>" +
                            //                                "<td>" + QualificationTypeID + "</td>" +
                            //                          "</tr>";
                            DLdb.DB_Connect();

                            DLdb.RS4.Open(); 
                            DLdb.SQLST4.CommandText = "insert into PlumberQualifications (PlumberID,CourseDateStart,CourseYear,TrainingProvider,CertificationNo)" +
                                "values (@PlumberID,@CourseDateStart,@CourseYear,@TrainingProvider,@CertificationNo)";
                            DLdb.SQLST4.Parameters.AddWithValue("PlumberID", theSqlDataReader2["PlumberID"].ToString());
                            DLdb.SQLST4.Parameters.AddWithValue("CourseDateStart", theSqlDataReader2["CourseDateStart"].ToString());
                            DLdb.SQLST4.Parameters.AddWithValue("CourseYear", theSqlDataReader2["CourseDateCompleted"].ToString());
                            DLdb.SQLST4.Parameters.AddWithValue("TrainingProvider", theSqlDataReader2["TrainingProvider"].ToString());
                            DLdb.SQLST4.Parameters.AddWithValue("CertificationNo", theSqlDataReader2["CertificationNo"].ToString());
                            DLdb.SQLST4.CommandType = CommandType.Text;
                            DLdb.SQLST4.Connection = DLdb.RS4;
                            SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();


                            if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                            DLdb.SQLST4.Parameters.RemoveAt(0);
                            DLdb.SQLST4.Parameters.RemoveAt(0);
                            DLdb.SQLST4.Parameters.RemoveAt(0);
                            DLdb.SQLST4.Parameters.RemoveAt(0);
                            DLdb.SQLST4.Parameters.RemoveAt(0);
                            DLdb.RS4.Close();
                            Response.Flush();

                            DLdb.DB_Close();
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

            DLdb.DB_PIRB_Close();
        }
    }
}