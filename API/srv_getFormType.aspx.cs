using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace InspectIT.API
{
    public partial class srv_getFormType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string getFormType = "";
            string getOptions = "";
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = "select * from formlinks fl inner join forms fs on fl.formid=fs.FormID where fl.FormType='COC' and fl.isActive='1'";
            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    // GET COC STATEMENT TYPE
                    DLdb.RS3.Open();
                    DLdb.SQLST3.CommandText = "select * from COCInstallations where TypeID = @TypeID and COCStatementID = @COCStatementID";
                    DLdb.SQLST3.Parameters.AddWithValue("TypeID", theSqlDataReader2["TypeID"].ToString());
                    DLdb.SQLST3.Parameters.AddWithValue("COCStatementID", Request.QueryString["ccid"]);
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        theSqlDataReader3.Read();
                        getOptions += "<input type=\"checkbox\" class=\"chk\" name=\"isCode\" id=\"checkbox-mini-" + theSqlDataReader2["FormID"].ToString() + "\" value=\"" + theSqlDataReader2["FormID"].ToString() + "\" checked=\"checked\" />" +
                                        "<label onclick=\"logInstallationTypes(this.id,'" + theSqlDataReader2["TypeID"].ToString() + "')\" for=\"checkbox-mini-" + theSqlDataReader2["FormID"].ToString() + "\">" + theSqlDataReader2["name"].ToString() + "</label>";
                        
                    } else
                    {
                        getOptions += "<input type=\"checkbox\" class=\"chk\" name=\"isCode\" id=\"checkbox-mini-" + theSqlDataReader2["FormID"].ToString() + "\" value=\"" + theSqlDataReader2["FormID"].ToString() + "\" />" +
                                      "<label onclick=\"logInstallationTypes(this.checked," + theSqlDataReader2["TypeID"].ToString() + ")\"  for=\"checkbox-mini-" + theSqlDataReader2["FormID"].ToString() + "\">" + theSqlDataReader2["name"].ToString() + "</label>";
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.SQLST3.Parameters.RemoveAt(0);
                    DLdb.RS3.Close();
                      
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            DLdb.RS2.Close();

            getFormType = "<div data-role=\"fieldcontain\">" +
                                           "     <legend>Type of Installation:</legend><fieldset data-role=\"controlgroup\" data-mini=\"true\">" +
                                                getOptions +
                                           "      </fieldset>" +
                                           " </div>";

            DLdb.DB_Close();

            Response.Write(getFormType);
            Response.End();
        }
    }
}