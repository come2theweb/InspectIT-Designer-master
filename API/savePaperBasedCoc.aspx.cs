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
    public partial class savePaperBasedCoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            string imgsrc = Request.Form["imgsrc"].ToString();
            string uid = Request.Form["userid"].ToString();
            string cocid = Request.Form["cocid"].ToString();
            //string random = Request.Form["random"].ToString();

            string data = "";
            string randomNum = DLdb.CreatePassword(6).ToString();

            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(imgsrc);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            image.Save(Server.MapPath("~/pdf/" + "_" + randomNum + ".png"), System.Drawing.Imaging.ImageFormat.Png);

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update COCStatements Set paperBasedCOC=@paperBasedCOC where COCStatementID = @COCStatementID";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", cocid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("paperBasedCOC", "_" + randomNum + ".png");
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into CertificateTracking (Message,Username,TrackingTypeID,CertificateID) values (@Message,@Username,@TrackingTypeID,@CertificateID)";
            DLdb.SQLST.Parameters.AddWithValue("Message", "New image of paper COC uploaded");
            DLdb.SQLST.Parameters.AddWithValue("Username", uid.ToString());
            DLdb.SQLST.Parameters.AddWithValue("TrackingTypeID", "0");
            DLdb.SQLST.Parameters.AddWithValue("CertificateID", cocid.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Context.Response.Write(data);
        }
    }
}