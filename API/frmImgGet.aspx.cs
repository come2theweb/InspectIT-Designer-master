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
    public partial class frmImgGet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FieldID"] != null)
            {
                try
                {
                    // SAVE IMAGE WITH TEMPID
                    Global DLdb = new Global();
                    DLdb.DB_Connect();

                    string returnHTML = "";

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "Select * from FormImg where FieldID = @FieldID and COCID = @COCID and FormID = @FormID";
                    //DLdb.SQLST.Parameters.AddWithValue("UserID", Request.QueryString["UserID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("FieldID", Request.QueryString["FieldID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("COCID", Request.QueryString["COCID"].ToString());
                    DLdb.SQLST.Parameters.AddWithValue("FormID", Request.QueryString["FormID"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            if (Request.QueryString["UserID"].ToString() == theSqlDataReader["UserID"].ToString())
                            {
                                returnHTML += "<div class=\"col-md-3\" id=\"show_img_" + theSqlDataReader["imgid"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader["imgid"].ToString() + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"https://197.242.82.242/inspectit/AuditorImgs/" + theSqlDataReader["imgsrc"].ToString() + "\" target=\"_blank\"><img src=\"https://197.242.82.242/inspectit/AuditorImgs/" + theSqlDataReader["imgsrc"].ToString() + "\" class=\"img img-responsive\" /></a></div>";
                            }
                            else
                            {
                                returnHTML += "<div class=\"col-md-3\"><a href=\"https://197.242.82.242/inspectit/AuditorImgs/" + theSqlDataReader["imgsrc"].ToString() + "\" target=\"_blank\"><img src=\"https://197.242.82.242/inspectit/AuditorImgs/" + theSqlDataReader["imgsrc"].ToString() + "\" class=\"img img-responsive\" /></a></div>";
                            }

                        }


                    } else { returnHTML += "No Image Selected"; }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    //DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.SQLST.Parameters.RemoveAt(0);
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