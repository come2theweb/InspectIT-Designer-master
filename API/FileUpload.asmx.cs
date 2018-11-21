using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

namespace InspectIT
{
    /// <summary>
    /// Summary description for FileUpload
    /// </summary>
    
    public class FileUpload : System.Web.Services.WebService
    {

        [WebMethod]
        public string SaveImage()
        {
            try {
                
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];

                string pathToSave_100 = Server.MapPath("~/sitenotemedia/") + file.FileName;
                file.SaveAs(pathToSave_100);
                
                Global DLdb = new Global();
                DLdb.DB_Connect();

                // OPEN FORM DETAILS
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "delete FormImg where userid = @uid and qid = @qid and sid = @sid and sqid = @sqid";
                DLdb.SQLST.Parameters.AddWithValue("uid", HttpContext.Current.Request.Params["uid"]);
                DLdb.SQLST.Parameters.AddWithValue("qid", HttpContext.Current.Request.Params["qid"]);
                DLdb.SQLST.Parameters.AddWithValue("sid", HttpContext.Current.Request.Params["sid"]);
                DLdb.SQLST.Parameters.AddWithValue("sqid", HttpContext.Current.Request.Params["sqid"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();


                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "insert into FormImg (imgsrc,userid,sid,qid,sqid) values (@imgsrc,@uid,@sid,@qid,@sqid)";
                DLdb.SQLST.Parameters.AddWithValue("imgsrc", file.FileName);
                DLdb.SQLST.Parameters.AddWithValue("uid", HttpContext.Current.Request.Params["uid"]);
                DLdb.SQLST.Parameters.AddWithValue("sid", HttpContext.Current.Request.Params["sid"]);
                DLdb.SQLST.Parameters.AddWithValue("qid", HttpContext.Current.Request.Params["qid"]);
                DLdb.SQLST.Parameters.AddWithValue("sqid", HttpContext.Current.Request.Params["sqid"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                return file.FileName;

            }
            catch (Exception err)
            {
                return err.Message.ToString();
            }


            
        }

    }
}
