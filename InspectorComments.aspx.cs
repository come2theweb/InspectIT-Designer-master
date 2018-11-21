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
    public partial class InspectorComments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            // ADMIN CHECK
            if (Session["IIT_Role"].ToString() != "Inspector")
            {
                Response.Redirect("Default");
            }

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

            displayusers.InnerHtml = "";

            DLdb.DB_Connect();
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "SELECT * FROM InspectorCommentTemplate where UserID=@UserID and isactive='1'";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    // GET THE TYPE
                    string typename = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from InstallationTypes where InstallationTypeID = @InstallationTypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            typename = theSqlDataReader2["installationtype"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string fieldname = "";
                    if (theSqlDataReader["FieldID"].ToString() == "" || theSqlDataReader["FieldID"] == DBNull.Value)
                    {
                        fieldname = theSqlDataReader["Question"].ToString();
                    }
                    else
                    {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from FormFields where FieldID = @FieldID";
                    DLdb.SQLST2.Parameters.AddWithValue("FieldID", theSqlDataReader["FieldID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            fieldname = theSqlDataReader2["label"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    }

                    displayusers.InnerHtml += "<tr>" +
                                                       "<td>" + typename + "</td>" +
                                                       "<td>" + fieldname + "</td>" +
                                                       "<td>" + theSqlDataReader["Name"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["Comment"].ToString() + "</td>" +
                                                       "<td align=\"right\"><div class=\"btn-group text-right\"><button type=\"button\" class=\"btn btn-danger\" onclick=\"deleteconf('InspectorCommentsDelete.aspx?cid=" + theSqlDataReader["CommentID"].ToString() + "')\" ><i class=\"fa fa-trash\"></i></button><a href=\"InspectorCommentsEdit.aspx?cid=" + DLdb.Encrypt(theSqlDataReader["CommentID"].ToString()) + "\"><button type=\"button\" class=\"btn btn-primary\"><i class=\"fa fa-pencil\"></i></button></a></div></td>" +
                                                   "</tr>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "SELECT * FROM InspectorCommentTemplate where UserID=@UserID and isactive='0'";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    // GET THE TYPE
                    string typename = "";
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from InstallationTypes where InstallationTypeID = @InstallationTypeID";
                    DLdb.SQLST2.Parameters.AddWithValue("InstallationTypeID", theSqlDataReader["TypeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            typename = theSqlDataReader2["installationtype"].ToString();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();

                    string fieldname = "";
                    if (theSqlDataReader["FieldID"].ToString() == "" || theSqlDataReader["FieldID"] == DBNull.Value)
                    {
                        fieldname = theSqlDataReader["Question"].ToString();
                    }
                    else
                    {
                        DLdb.RS2.Open();
                        DLdb.SQLST2.CommandText = "select * from FormFields where FieldID = @FieldID";
                        DLdb.SQLST2.Parameters.AddWithValue("FieldID", theSqlDataReader["FieldID"].ToString());
                        DLdb.SQLST2.CommandType = CommandType.Text;
                        DLdb.SQLST2.Connection = DLdb.RS2;
                        theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                        if (theSqlDataReader2.HasRows)
                        {
                            while (theSqlDataReader2.Read())
                            {
                                fieldname = theSqlDataReader2["label"].ToString();
                            }
                        }

                        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                        DLdb.RS2.Close();
                    }

                    Tbody1.InnerHtml += "<tr>" +
                                                       "<td>" + typename + "</td>" +
                                                       "<td>" + fieldname + "</td>" +
                                                       "<td>" + theSqlDataReader["Name"].ToString() + "</td>" +
                                                       "<td>" + theSqlDataReader["Comment"].ToString() + "</td>" +
                                                       "<td align=\"right\"><div class=\"btn-group text-right\"><button type=\"button\" class=\"btn btn-danger\" onclick=\"deleteconf('InspectorCommentsDeletes.aspx?cid=" + theSqlDataReader["CommentID"].ToString() + "')\" ><i class=\"fa fa-trash\"></i></button><a href=\"InspectorCommentsEdit.aspx?cid=" + DLdb.Encrypt(theSqlDataReader["CommentID"].ToString()) + "\"><button type=\"button\" class=\"btn btn-primary\"><i class=\"fa fa-pencil\"></i></button></a></div></td>" +
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