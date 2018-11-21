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
    public partial class frmImgDel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Imgid"] != null)
            {
                try
                {
                    // SAVE IMAGE WITH TEMPID
                    Global DLdb = new Global();
                    DLdb.DB_Connect();

                    string returnHTML = "true";

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Delete from FormImg where Imgid = @Imgid";
                    DLdb.SQLST.Parameters.AddWithValue("Imgid", Request.QueryString["Imgid"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
                    
                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    

                    DLdb.DB_Close();

                    Response.Write(returnHTML);

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