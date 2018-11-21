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
    public partial class saveAuditDateImg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string imgsrc = Request.Form["imgsrc"].ToString();
            string uid = Request.Form["userid"].ToString();
            string cocid = Request.Form["cocid"].ToString();
           // string random = Request.Form["random"].ToString();

            string data = "";
            string randomNum = DLdb.CreatePassword(6).ToString();

            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(imgsrc);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            image.Save(Server.MapPath("~/noticeimages/" + "_" + randomNum + ".png"), System.Drawing.Imaging.ImageFormat.Png);
            

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCInspectors set Picture = @Picture where COCStatementID = @COCStatementID and UserID = @UserID and isactive='1'";
            DLdb.SQLST.Parameters.AddWithValue("Picture", "_" + randomNum + ".png");
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Context.Response.Write(data);
        }
    }
}