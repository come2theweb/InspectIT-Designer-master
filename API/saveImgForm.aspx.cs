using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InspectIT.API
{
    public partial class saveImgForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string imgsrc = Request.Form["imgsrc"].ToString();
            string photoid = Request.Form["FieldID"].ToString();

            string ImgID = "";
            string returnHTML = "";

            string data = "";
            string randomNum = DLdb.CreatePassword(6).ToString();

            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(imgsrc);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            image.Save(Server.MapPath("~/AuditorImgs/" + photoid + "_" + randomNum + ".png"), System.Drawing.Imaging.ImageFormat.Png);

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,UserID,FieldID,COCID,isFile,FormID) values (@imgsrc,@UserID,@FieldID,@tempID,'1',@FormID); Select Scope_Identity() as ImgID";
            DLdb.SQLST.Parameters.AddWithValue("imgsrc", photoid + "_" + randomNum + ".png");
            DLdb.SQLST.Parameters.AddWithValue("UserID", Request.Form["UserID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("FieldID", "img_div_" + Request.Form["FieldID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("tempID", Request.Form["COCID"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("FormID", DLdb.Decrypt(Request.Form["FormID"].ToString()));
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

            returnHTML += "<div class=\"col-md-3\" id=\"show_img_" + ImgID + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + ImgID + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + photoid + "_" + randomNum + ".png" + "\" target=\"_blank\"><img src=\"AuditorImgs/" + photoid + "_" + randomNum + ".png" + "\" class=\"img img-responsive\" /></a></div>";

            DLdb.DB_Close();

            Context.Response.Write(returnHTML);
        }
    }
}