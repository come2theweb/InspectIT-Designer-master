using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;

namespace InspectIT
{
    public partial class DeleteItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string id = Request.QueryString["id"].ToString();
            string op = Request.QueryString["op"].ToString();
            string pid = Request.QueryString["pid"].ToString();

            if (op == "delsubtype")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update InstallationTypessub set isActive='0' WHERE subID=@subID";
                DLdb.SQLST.Parameters.AddWithValue("subID", id);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("installationTypesSub.aspx?msg=" + DLdb.Encrypt("Installation Sub Type has been archived"));
            }
            else if (op == "undelsubtype")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update InstallationTypessub set isActive='1' WHERE subID=@subID";
                DLdb.SQLST.Parameters.AddWithValue("subID", id);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("installationTypesSub.aspx?msg=" + DLdb.Encrypt("Installation Sub Type has been added"));
            }
            else if (op == "delInstallType")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update InstallationTypes set isActive='0' WHERE InstallationTypeID=@InstallationTypeID";
                DLdb.SQLST.Parameters.AddWithValue("InstallationTypeID", id);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("installationTypes.aspx?msg=" + DLdb.Encrypt("Installation Type has been archived"));
            }
            else if (op == "undelInstallType")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update InstallationTypes set isActive='1' WHERE InstallationTypeID=@InstallationTypeID";
                DLdb.SQLST.Parameters.AddWithValue("InstallationTypeID", id);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("installationTypes.aspx?msg=" + DLdb.Encrypt("Installation Type has been added"));
            }
            else if (op == "delCommentImg")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update formimg set isActive='0' WHERE imgid=@imgid";
                DLdb.SQLST.Parameters.AddWithValue("imgid", id);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("reportQuestionsEdit.aspx?msg=" + DLdb.Encrypt("Image removed") + "&id=" + pid);
            }
            else if (op == "delReferenceImg")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update formimg set isActive='0' WHERE imgid=@imgid";
                DLdb.SQLST.Parameters.AddWithValue("imgid", id);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("reportQuestionsEdit.aspx?msg=" + DLdb.Encrypt("Image removed") + "&id=" + pid);
            }
            else if (op == "delCommentImgins")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update formimg set isActive='0' WHERE imgid=@imgid";
                DLdb.SQLST.Parameters.AddWithValue("imgid", id);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("InspectorCommentsEdit.aspx?msg=" + DLdb.Encrypt("Image removed") + "&cid=" + pid);
            }
            else if (op == "delReferenceImgins")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update formimg set isActive='0' WHERE imgid=@imgid";
                DLdb.SQLST.Parameters.AddWithValue("imgid", id);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("InspectorCommentsEdit.aspx?msg=" + DLdb.Encrypt("Image removed") + "&cid=" + pid);
            }
            else if (op == "delImgCOC")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update formimg set isActive='0' WHERE imgid=@imgid";
                DLdb.SQLST.Parameters.AddWithValue("imgid", id);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + pid);
            }
            else if (op == "delImgAddReview")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update formimg set isActive='0' WHERE imgid=@imgid";
                DLdb.SQLST.Parameters.AddWithValue("imgid", id);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + pid + "&v=" + Request.QueryString["v"].ToString());
            }
            else if (op == "delReferenceImginsaa")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update formimg set isActive='0' WHERE imgid=@imgid";
                DLdb.SQLST.Parameters.AddWithValue("imgid", id);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("EditReview.aspx?cocid=" + pid + "&rid=" + Request.QueryString["rid"]);
            }
            else if (op == "delRessferenceImgins")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update formimg set isActive='0' WHERE imgid=@imgid";
                DLdb.SQLST.Parameters.AddWithValue("imgid", id);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("AddReview.aspx?cocid=" + pid + "&v=" + Request.QueryString["v"].ToString());
            }
            else if (op == "delImgecocPlumber")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update formimg set isActive='0' WHERE imgid=@imgid";
                DLdb.SQLST.Parameters.AddWithValue("imgid", id);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("EditCOCStatement.aspx?cocid=" + pid);
            }
            else if (op == "adminCocArch")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update Cocstatements set isActive='0' WHERE cocstatementid=@cocstatementid";
                DLdb.SQLST.Parameters.AddWithValue("cocstatementid", DLdb.Decrypt(id));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("ViewRefixandAuditStatementAdmin.aspx");
            }
            else if (op == "delaudArea")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update Auditorareas set isActive='0' WHERE ID=@ID";
                DLdb.SQLST.Parameters.AddWithValue("ID", DLdb.Decrypt(id));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("EditOrDeleteAuditor.aspx?AuditorID=" + pid);
            }
            else if (op == "delPdfImgcoc")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCStatements set PaperBasedCOC='' WHERE COCStatementID=@COCStatementID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(id));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("EditCOCStatement.aspx?cocid=" + id);
            }
            else if (op == "delReviewFromCOC")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "update COCReviews set isActive='0' WHERE COCStatementID=@COCStatementID and ReviewID=@ReviewID";
                DLdb.SQLST.Parameters.AddWithValue("COCStatementID", DLdb.Decrypt(pid));
                DLdb.SQLST.Parameters.AddWithValue("ReviewID", DLdb.Decrypt(id));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                Response.Redirect("EditCOCStatementInspector.aspx?cocid=" + pid);
            }

            DLdb.DB_Close();
            //Response.Redirect("ViewUser.aspx?msg=" + DLdb.Encrypt("User has been deleted"));
        }
    }
}