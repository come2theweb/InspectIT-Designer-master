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
    public partial class Forms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // CHECK SESSION
            if (Session["OHS_UID"] == null)
            {
                Response.Redirect("default");
            }

            Global DLdb = new Global();

            if (Request.QueryString["msg"] != null)
            {
                string msg = DLdb.Decrypt(Request.QueryString["msg"].ToString());
                successbox.InnerHtml = msg;
                successbox.Visible = true;
            }
                        
            DLdb.DB_Connect();
                        
            // LOAD FORMS
            ManagersList.InnerHtml = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from Forms where userid = @userid and isactive = '1'";
            DLdb.SQLST.Parameters.AddWithValue("userid", Session["OHS_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    string gStatus = "Active";
                    if (theSqlDataReader["isActive"].ToString() == "False")
                    {
                        gStatus = "Inactive";
                    }

                    // GET THE TOTAL SUBMITITONS
                    var v_nosubmitions = "";

                    // NO USERS CONNECTED TO FORM
                    var v_nousers = "";

                    // GET COMPLETED
                    var v_nocompleted = "";
                    
                    ManagersList.InnerHtml = ManagersList.InnerHtml + "<tr>" +
                                             "   <td class=\"v-align-middle\"><a href=\"ViewForm?fid=" + DLdb.Encrypt(theSqlDataReader["FormID"].ToString()) + "\">" + theSqlDataReader["name"] + "</a></td>" +
                                             "   <td class=\"v-align-middle\">" + v_nousers + "</td>" +
                                             "   <td class=\"v-align-middle\">" + theSqlDataReader["type"] + "</td>" +
                                             "   <td class=\"v-align-middle\"><div class=\"btn-group\"><button title=\"Archive\" class=\"btn btn-success\" type=\"button\" onclick=\"deleteconf('DeleteForm?fid=" + DLdb.Encrypt(theSqlDataReader["FormID"].ToString()) + "')\"><i class=\"fa fa-trash\"></i></button><button title=\"Preview\" class=\"btn btn-success\" type=\"button\" onclick=\"document.location.href='ViewForm?fid=" + DLdb.Encrypt(theSqlDataReader["FormID"].ToString()) + "'\"><i class=\"fa fa-sign-out\"></i></button><button title=\"Link to NFC TAG\" class=\"btn btn-success\" type=\"button\" onclick=\"document.location.href='LinkForm?fid=" + DLdb.Encrypt(theSqlDataReader["FormID"].ToString()) + "'\"><i class=\"fa fa-rss-square\"></i></button></div></td>" +
                                             "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            


            DLdb.DB_Close();

        }
    }
}