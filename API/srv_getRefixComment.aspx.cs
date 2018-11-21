using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InspectIT.API
{
    public partial class srv_getRefixComment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string review = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM COCReviews where COCStatementID = @COCStatementID  and isFixed = '0' and isActive = '1' order by createdate desc";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string StatusCol = "";
                    string btnFix = "";

                    if (theSqlDataReader["status"].ToString() == "Failure")
                    {
                        StatusCol = "danger";
                        btnFix = "<div class=\"btn btn-primary\" onclick=\"document.location.href='FixReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "'\">Refix Complete</div>";
                    }
                    else if (theSqlDataReader["status"].ToString() == "Cautionary")
                    {
                        StatusCol = "warning";
                        btnFix = "<div class=\"btn btn-primary\" onclick=\"document.location.href='DismissReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "'\">Dismiss</div>";
                    }
                    else if (theSqlDataReader["status"].ToString() == "Compliment")
                    {
                        StatusCol = "success";
                        btnFix = "<div class=\"btn btn-primary\" onclick=\"document.location.href='DismissReview?cocid=" + Request.QueryString["cocid"].ToString() + "&rid=" + DLdb.Encrypt(theSqlDataReader["ReviewID"].ToString()) + "'\">Dismiss</div>";
                    }

                    string InstallationType = "";

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from InstallationTypes where InstallationTypeID = @InstallationTypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            InstallationType = theSqlDataReader2["InstallationType"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string Media = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "Select * from FormImg where CommentID = @CommentID";
                    DLdb.SQLST2.Parameters.AddWithValue("CommentID", theSqlDataReader["ReviewID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            string filename = theSqlDataReader2["imgsrc"].ToString(); ;
                            if (theSqlDataReader2["UserID"].ToString() == Request.QueryString["uid"].ToString())
                            {
                                Media += "<div class=\"col-md-3\" id=\"show_img_" + theSqlDataReader2["ImgID"].ToString() + "\"><span class=\"btn btn-danger\" title=\"Remove Image\" style=\"position:absolute;\" onclick=\"deleteImage('" + theSqlDataReader2["imgid"].ToString() + "')\"><i class=\"fa fa-trash\"></i></span><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" class=\"img img-responsive\" /></a></div>";
                            }
                            else
                            {
                                Media += "<div class=\"col-md-3\"><a href=\"AuditorImgs/" + filename + "\" target=\"_blank\"><img src=\"AuditorImgs/" + filename + "\" class=\"img img-responsive\" /></a></div>";
                            }

                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    review += "<div class=\"row alert-" + StatusCol + "\" style=\"padding: 5px; \">" +
                                               "       <div class=\"col-md-6 text-left\">" +
                                               "         <b>Instalation Type:</b> " + InstallationType + "<br />" +
                                               "         <b>Audit Status:</b> " + theSqlDataReader["status"].ToString() + "" +
                                               "     </div>" +
                                               "     <div class=\"col-md-6 text-left\">" +
                                               "        <b>Reference:</b> " + theSqlDataReader["name"].ToString() + "" +
                                               "     </div>" +
                                               "     <div class=\"col-md-12 text-left\"><b>Comments:</b> " + theSqlDataReader["comment"].ToString() + "</div>" +
                                               "     <div class=\"col-md-12 text-left\"><b>Media:</b><br />" + Media + "</div>" +
                                               "     <div class=\"col-md-12 text-right\">" + btnFix + "</div>" +
                                               " </div><hr />";

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Write(review);
            Response.End();
        }
    }
}