using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.ComponentModel;
using System.IO;

namespace InspectIT
{
    public partial class frmImgSave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["UserID"] != null)
            {
                try
                {
                    if (HttpContext.Current.Request.Files.AllKeys.Any())
                    {
                        // Get the uploaded image from the Files collection
                        var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

                        if (httpPostedFile != null)
                        {
                            string returnHTML = "";
                            string ImgID = "";
                            var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/AuditorImgs"), httpPostedFile.FileName);
                            httpPostedFile.SaveAs(fileSavePath);
                            
                            // SAVE IMAGE WITH TEMPID
                            Global DLdb = new Global();
                            DLdb.DB_Connect();

                            DLdb.RS.Open();
                            DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,COCID,isFile,FormID) values (@imgsrc,@UserID,@FieldID,@tempID,'1',@FormID); Select Scope_Identity() as ImgID";
                            DLdb.SQLST.Parameters.AddWithValue("imgsrc", httpPostedFile.FileName);
                            DLdb.SQLST.Parameters.AddWithValue("UserID", Request.Form["UserID"].ToString());
                            DLdb.SQLST.Parameters.AddWithValue("FieldID", "img_div_" + Request.Form["FieldID"].ToString());
                            DLdb.SQLST.Parameters.AddWithValue("tempID", Request.Form["COCID"].ToString());
                            DLdb.SQLST.Parameters.AddWithValue("FormID", Request.Form["FormID"].ToString());
                            DLdb.SQLST.CommandType = CommandType.Text;
                            DLdb.SQLST.Connection = DLdb.RS;
                            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                            if (theSqlDataReader.HasRows)
                            {
                                theSqlDataReader.Read();
                                ImgID = theSqlDataReader["ImgID"].ToString();
                            }

                            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.SQLST.Parameters.RemoveAt(0);
                            DLdb.RS.Close();
                            
                            returnHTML += "<div class=\"col-md-3\" id=\"show_img_" + ImgID + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + ImgID + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + httpPostedFile.FileName + "\" target=\"_blank\"><img src=\"AuditorImgs/" + httpPostedFile.FileName + "\" class=\"img img-responsive\" /></a></div>";

                            DLdb.DB_Close();

                            Response.Write(returnHTML);

                        }
                    }
                    
                }
                catch (Exception err)
                {
                    Response.Write(err);
                }

            }
            else
            {
                Response.Write("error");
            }

            Response.End();
        }

    }


        
}