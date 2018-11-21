using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;

namespace InspectIT
{
    public partial class zImportSignatures : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();

            string idName = "";

            DLdb.DB_Connect();

            // LOAD PLUMBER INFORMATION
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Users";
            //DLdb.SQLST.Parameters.AddWithValue("ID", theSqlDataReader["ID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.DB_PIRB_Connect();
                    // IMAGE
                    string maxLogged = "";
                    string photo = "";
                    string profilephoto = "";
                    // below table is qa system
                    DLdb.pirbRS2.Open();
                    DLdb.pirbSQLST2.CommandText = "select * from Plumbers where PlumberID=@PlumberID and Signature is not null";
                    DLdb.pirbSQLST2.Parameters.AddWithValue("PlumberID", theSqlDataReader["PIRBID"].ToString());
                    DLdb.pirbSQLST2.CommandType = CommandType.Text;
                    DLdb.pirbSQLST2.Connection = DLdb.pirbRS2;
                    SqlDataReader theSqlDataReader2 = DLdb.pirbSQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                           // maxLogged = theSqlDataReader2["MaxNonLoggedCertificates"].ToString();
                            //idName = theSqlDataReader2["PlumberID"].ToString();
                            photo = theSqlDataReader2["Signature"].ToString();
                            profilephoto = theSqlDataReader2["Photo"].ToString();

                            //var imageBytes = (byte[])theSqlDataReader2["Signature"];
                            //if (imageBytes.Length > 0)
                            //{
                            //    using (var convertedImage = new Bitmap(new MemoryStream(imageBytes)))
                            //    {
                            //        var fileName = Server.MapPath("~/signatures/") + theSqlDataReader["PIRBID"].ToString() + ".bmp";
                            //        if (File.Exists(fileName))
                            //        {
                            //            File.Delete(fileName);
                            //        }
                            //        convertedImage.Save(fileName);
                            //    }
                            //}

                            //var imageBytesa = (byte[])theSqlDataReader2["Photo"];
                            //if (imageBytesa.Length > 0)
                            //{
                            //    using (var convertedImagea = new Bitmap(new MemoryStream(imageBytesa)))
                            //    {
                            //        var fileNamea = Server.MapPath("~/Photos/") + theSqlDataReader["PIRBID"].ToString() + ".bmp";
                            //        if (File.Exists(fileNamea))
                            //        {
                            //            File.Delete(fileNamea);
                            //        }
                            //        convertedImagea.Save(fileNamea);
                            //    }
                            //}
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.pirbSQLST2.Parameters.RemoveAt(0);
                    DLdb.pirbRS2.Close();

                    DLdb.DB_PIRB_Close();

                    if (maxLogged.ToString() == "")
                    {
                        maxLogged = "10";
                    }

                    DLdb.RS3.Open();
                   // DLdb.SQLST3.CommandText = "update Users set NoCOCpurchases=@NoCOCpurchases where PIRBID=@PIRBID";
                    DLdb.SQLST3.CommandText = "update Users set Signature=@Signature,Photo=@Photo where PIRBID=@PIRBID";
                    DLdb.SQLST3.Parameters.AddWithValue("Signature", photo.ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("Photo", profilephoto.ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("PIRBID", theSqlDataReader["PIRBID"].ToString());
                   // DLdb.SQLST3.Parameters.AddWithValue("NoCOCpurchases", maxLogged.ToString());
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();


                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();
                    Response.Flush();
                    Server.ScriptTimeout = 10000000;

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        } 
    }
}