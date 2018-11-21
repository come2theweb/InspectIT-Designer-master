using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace InspectIT.Controls
{
    public partial class notifications : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string dispMsgStaff = "";
            string dispMsgAduit = "";
            if (Session["IIT_Role"].ToString() == "Staff")
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from COCStatements where isRefix='1' and UserID=@UserID";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    noti.InnerHtml = "<span class=\"bubble\"></span>";
                    NotifyText.InnerHtml = "<a href=\"ViewRefixandAuditStatement.aspx\" class=\"\">View Refixes</a>";
                }
                else
                {
                    NotifyText.InnerHtml = "<a href=\"#\" class=\"\">No New Notifications</a>";
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                string msgDisp = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from MessagLists where isActive='1' and Users='Plumber'";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "select * from MessageListsItems where MessageListID=@MessageListID";
                        DLdb.SQLST3.Parameters.AddWithValue("MessageListID", theSqlDataReader["ID"].ToString());
                        DLdb.SQLST3.CommandType = CommandType.Text;
                        DLdb.SQLST3.Connection = DLdb.RS3;
                        SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                        if (theSqlDataReader3.HasRows)
                        {
                            while (theSqlDataReader3.Read())
                            {
                                

                                DLdb.RS2.Open();
                                DLdb.SQLST2.CommandText = "select * from MessageListsRead where MessageID=@MessageID and UserID=@UserID";
                                DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"]);
                                DLdb.SQLST2.Parameters.AddWithValue("MessageID", theSqlDataReader3["ID"].ToString());
                                DLdb.SQLST2.CommandType = CommandType.Text;
                                DLdb.SQLST2.Connection = DLdb.RS2;
                                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                                if (!theSqlDataReader2.HasRows)
                                {
                                    noti.InnerHtml = "<span class=\"bubble\"></span>";
                                    msgDisp = theSqlDataReader3["Message"].ToString();
                                    dispMsgStaff +=
                                    "<div class=\"notification-item unread clearfix\" id=\"msgtop" + theSqlDataReader3["ID"].ToString() + "\">" +
                                    "    <div class=\"heading open\">" +
                                    "        <div class=\"more-details\">" +
                                    "           <div class=\"more-details-inner\">" +
                                    "                <h5 class=\"semi-bold fs-16\">" + msgDisp + "</h5>" +
                                    "            </div>" +
                                    "        </div>" +
                                    "    </div>" +
                                    "    <div class=\"option\" data-toggle=\"tooltip\" data-placement=\"left\" title=\"mark as read\">" +
                                    "        <a onclick=\"markMsgReadsys('" + theSqlDataReader3["ID"].ToString() + "','" + Session["IIT_UID"].ToString() + "')\" style=\"cursor:pointer;\"><i class=\"fs-14 fa fa-check\"></i></a>" +
                                    "    </div>" +
                                    "</div>";
                                }
                                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.RS2.Close();
                            }
                        }
                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.RS3.Close();

                       
                    }
                }
                else
                {
                    dispMsgStaff = "<a href=\"#\" class=\"\">No New Notifications</a>";
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                messageListDisp.InnerHtml = dispMsgStaff;
            }
            else if (Session["IIT_Role"].ToString() == "Inspector")
            {
                string msgDisp = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from MessagLists where isActive='1' and Users='Auditor'";
                DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
               SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "select * from MessageListsItems where MessageListID=@MessageListID";
                        DLdb.SQLST3.Parameters.AddWithValue("MessageListID", theSqlDataReader["ID"].ToString());
                        DLdb.SQLST3.CommandType = CommandType.Text;
                        DLdb.SQLST3.Connection = DLdb.RS3;
                        SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                        if (theSqlDataReader3.HasRows)
                        {
                            while (theSqlDataReader3.Read())
                            {
                                msgDisp = theSqlDataReader3["Message"].ToString();

                                DLdb.RS2.Open();
                                DLdb.SQLST2.CommandText = "select * from MessageListsRead where MessageID=@MessageID and UserID=@UserID";
                                DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"]);
                                DLdb.SQLST2.Parameters.AddWithValue("MessageID", theSqlDataReader3["ID"].ToString());
                                DLdb.SQLST2.CommandType = CommandType.Text;
                                DLdb.SQLST2.Connection = DLdb.RS2;
                                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                                if (theSqlDataReader2.HasRows)
                                {
                                    while (theSqlDataReader2.Read())
                                    {

                                    }
                                }
                                else
                                {
                                    noti.InnerHtml = "<span class=\"bubble\"></span>";
                                    dispMsgAduit +=
                                    "<div class=\"notification-item unread clearfix\">" +
                                    "    <div class=\"heading open\">" +
                                    "        <div class=\"more-details\">" +
                                    "           <div class=\"more-details-inner\">" +
                                    "                <h5 class=\"semi-bold fs-16\">" + msgDisp + "</h5>" +
                                    "            </div>" +
                                    "        </div>" +
                                    "    </div>" +
                                    "    <div class=\"option\" data-toggle=\"tooltip\" data-placement=\"left\" title=\"mark as read\">" +
                                    "        <a onclick=\"markMsgRead('" + theSqlDataReader3["ID"].ToString() + "','" + Session["IIT_UID"].ToString() + "')\"><i class=\"fs-14 fa fa-check\"></i></a>" +
                                    "    </div>" +
                                    "</div>";
                                }
                                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.SQLST2.Parameters.RemoveAt(0);
                                DLdb.RS2.Close();
                            }
                        }
                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.RS3.Close();

                        
                    }
                }
                else
                {
                    dispMsgStaff = "<a href=\"#\" class=\"\">No New Notifications</a>";
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                messageListDisp.InnerHtml = dispMsgAduit;
            }
            DLdb.DB_Close();
        }
    }
}