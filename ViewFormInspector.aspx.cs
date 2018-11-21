using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace InspectIT
{
    public partial class ViewFormInspector : System.Web.UI.Page
    {
        public string gDID = "0";
        public string gCOCID = "";
        public string gTypeID = "";

        protected override void OnInit(EventArgs e)
        {
            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("default");
            }

            // CHECK THE FORM ID HAS BEEN PASSED TO THE PAGE
            if (Request.QueryString["tid"] == null)
            {
                Response.Write("Error");
                Response.End();
            }

            Global DLdb = new Global();

            if (Request.QueryString["msg"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["msg"].ToString());
                successmsg.InnerHtml = msg;
                successmsg.Visible = true;
            }

            if (Request.QueryString["err"] != null)
            {
                string msg = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span>&times;</span><span class=\"sr-only\">Close</span></button>" + DLdb.Decrypt(Request.QueryString["err"].ToString());
                errormsg.InnerHtml = msg;
                errormsg.Visible = true;
            }
            
            // GET THE FORM ID
            DLdb.DB_Connect();

            COCNumber.InnerHtml = "COC Number: " + DLdb.Decrypt(Request.QueryString["cocid"].ToString());
            COCType.InnerHtml = "Type: Normal";

            string FormID = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "Select * from FormLinks where FormType = @FormType and TypeID = @TypeID";
            DLdb.SQLST.Parameters.AddWithValue("FormType", Request.QueryString["typ"].ToString());
            DLdb.SQLST.Parameters.AddWithValue("TypeID", DLdb.Decrypt(Request.QueryString["tid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                FormID = theSqlDataReader["FormID"].ToString();
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // GET THE DATA ID IF EXISTS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from COCInstallations where COCStatementID = @COCStatementID and TypeID = @TypeID and DataID is not Null";
            DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("TypeID", DLdb.Decrypt(Request.QueryString["tid"].ToString()));
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                gDID = theSqlDataReader["DataID"].ToString();
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            gCOCID = DLdb.Decrypt(Request.QueryString["cocid"].ToString());
            gTypeID = DLdb.Decrypt(Request.QueryString["tid"].ToString());

            // GET THE COMMENT TEMPLATES
            TemplateSelection.Items.Clear();
            TemplateSelection.Items.Add(new ListItem("", ""));

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM InspectorCommentTemplate where UserID = @UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    TemplateSelection.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["Comment"].ToString()));
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            if (FormID != "")
            {
                // BUILD THE FORM
                viewformdisplay.InnerHtml = DLdb.buildFormInspector(FormID, false, DLdb.Decrypt(Request.QueryString["cocid"].ToString()), DLdb.Decrypt(Request.QueryString["tid"].ToString()));
            }

            if (Request.QueryString["refix"] != null)
            {
                // REFIX
                DisplayRefixNotice.Visible = true;
            }

            
        }

        protected void TemplateSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TemplateSelection.SelectedValue.ToString() != "")
            {
                RefixComments.Text = TemplateSelection.SelectedValue.ToString();

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("default");
            }

            // CHECK THE FORM ID HAS BEEN PASSED TO THE PAGE
            if (Request.QueryString["tid"] == null)
            {
                Response.Write("Error");
                Response.End();
            }

        }

        protected void savenotice_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();

            // GET THE FORM ID
            DLdb.DB_Connect();

            string NoticeType = "";
            if (chkFailure.Checked == true)
            {
                NoticeType = "Failure";
            }
            else if (chkCautionary.Checked == true)
            {
                NoticeType = "Cautionary";
            }
            else if (chkComplement.Checked == true)
            {
                NoticeType = "Complement";
            }

            // CHECK THE PICTURE
            if (FileUpload1.HasFile)
            {
                try
                {
                    
                    // UPLOAD THE PICTURE
                    string filename = Path.GetFileName(FileUpload1.FileName);
                    filename = DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + filename;
                    FileUpload1.SaveAs(Server.MapPath("~/noticeimages/") + filename);

                    // ADD REFIX NOTICE
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "insert into COCRefixFields (COCStatementID,TypeID,FieldID,NoticeType,Comments,Picture) values (@COCStatementID,@TypeID,@FieldID,@NoticeType,@Comments,@Picture)";
                    DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                    DLdb.SQLST2.Parameters.AddWithValue("FieldID", FieldID.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("NoticeType", NoticeType);
                    DLdb.SQLST2.Parameters.AddWithValue("Comments", RefixComments.Text.ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("Picture", filename);
                    DLdb.SQLST2.Parameters.AddWithValue("TypeID", DLdb.Decrypt(Request.QueryString["tid"].ToString()));
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    //REQUIRED: EMAIL COC OWNER

                    DLdb.DB_Close();
                    Response.Redirect("ViewFormInspector?cocid=" + Request.QueryString["cocid"].ToString() + "&typ=COC&tid=" + Request.QueryString["tid"].ToString() + "&msg=" + DLdb.Encrypt("Notice Added Successfully") + "&did=" + Request.QueryString["did"].ToString());

                }
                catch (Exception err)
                {
                    DLdb.DB_Close();
                    Response.Redirect("ViewFormInspector?cocid=" + Request.QueryString["cocid"].ToString() + "&typ=COC&tid=" + Request.QueryString["tid"].ToString() + "&err=" + DLdb.Encrypt(err.Message.ToString()) + "&did=" + Request.QueryString["did"].ToString());
                }

            }
            else
            {

                // ADD REFIX NOTICE
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = "insert into COCRefixFields (COCStatementID,TypeID,FieldID,NoticeType,Comments) values (@COCStatementID,@TypeID,@FieldID,@NoticeType,@Comments)";
                DLdb.SQLST2.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(Request.QueryString["cocid"].ToString()));
                DLdb.SQLST2.Parameters.AddWithValue("FieldID", FieldID.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("NoticeType", NoticeType);
                DLdb.SQLST2.Parameters.AddWithValue("Comments", RefixComments.Text.ToString());
                DLdb.SQLST2.Parameters.AddWithValue("TypeID", DLdb.Decrypt(Request.QueryString["tid"].ToString()));
                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.SQLST2.Parameters.RemoveAt(0);
                DLdb.RS2.Close();

                //REQUIRED: EMAIL COC OWNER

                DLdb.DB_Close();
                Response.Redirect("ViewFormInspector?cocid=" + Request.QueryString["cocid"].ToString() + "&typ=COC&tid=" + Request.QueryString["tid"].ToString() + "&msg=" + DLdb.Encrypt("Notice Added Successfully") + "&did=" + Request.QueryString["did"].ToString());

            }

            DLdb.DB_Close();

        }

        
    }
}