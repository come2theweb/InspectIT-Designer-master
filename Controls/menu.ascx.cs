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
    public partial class menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            
            if (Session["IIT_UID"] != null)
            {
                // CHECK SESSION AND SETUP MENUS
                if (Session["IIT_Role"].ToString() == "Administrator")
                {
                    adminMenu.Visible = true;
                    staffMenu.Visible = false;
                    inspectorMenu.Visible = false;
                    supplierMenu.Visible = false;
                }
                else if (Session["IIT_Role"].ToString() == "Inspector")
                {
                    adminMenu.Visible = false;
                    staffMenu.Visible = false;
                    inspectorMenu.Visible = true;
                    supplierMenu.Visible = false;
                }
                else if (Session["IIT_Role"].ToString() == "Staff")
                {
                    adminMenu.Visible = false;
                    staffMenu.Visible = true;
                    inspectorMenu.Visible = false;
                    supplierMenu.Visible = false;

                }
                else if (Session["IIT_Role"].ToString() == "Supplier")
                {
                    adminMenu.Visible = false;
                    staffMenu.Visible = false;
                    inspectorMenu.Visible = false;
                    supplierMenu.Visible = true;
                }
                else
                {
                    // NO TYPE LOG USER OUT
                    Response.Redirect("default");
                }
            }

            string uid = Session["IIT_UID"].ToString();

            // ADD COC REFIX COUNTER TO MENUBAR
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select count(*) as countRefixes from COCStatements where isRefix='1' and UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                countRefixes.InnerHtml = theSqlDataReader["countRefixes"].ToString();
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            string dispMsgStaff = "<ul class=\"menu-items\">";
            string dispMsgAduit = "<ul class=\"menu-items\">";
            if (Session["IIT_Role"].ToString() == "Staff")
            {
                
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

                                if (theSqlDataReader2.HasRows)
                                {
                                    while (theSqlDataReader2.Read())
                                    {

                                    }
                                }
                                else
                                {
                                    msgDisp = theSqlDataReader3["Message"].ToString();
                                    dispMsgStaff +=

                                        "<li class=\"\" id=\"msg" + theSqlDataReader3["ID"].ToString() + "\">" +
                                        "<a style=\"display:inline;cursor:pointer\" onclick=\"markMsgReadsys('" + theSqlDataReader3["ID"].ToString() + "','" + Session["IIT_UID"].ToString() + "')\"><span class=\"icon-thumbnail \"><i class=\"fs-14 fa fa-check\" title=\"Mark as read\"></i></span></a>" +
                                        "<span style=\"color:#ffffff;\">" + msgDisp + "</span>" +
                                        "</li>";
                                        
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
                    dispMsgStaff = "<li><a href=\"#\" class=\"\">No New Notifications</a></li>";
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from PlumberDesignations where isActive='1' and plumberid=@plumberid";
                DLdb.SQLST.Parameters.AddWithValue("plumberid", Session["IIT_UID"]);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["Designation"].ToString() != "Licensed Plumber" && theSqlDataReader["Designation"].ToString() != "Director Plumber" & theSqlDataReader["Designation"].ToString() != "Master Plumber")
                        {
                            hideItemCocStat.Visible = false;
                            hideItemaAduit.Visible = false;
                            hideItemCocPurch.Visible = false;
                        }
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                dispStaffMsg.InnerHtml = dispMsgStaff + "</ul>"; 
            }
            else if (Session["IIT_Role"].ToString() == "Inspector")
            {
                string msgDisp = "";
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from MessagLists where isActive='1' and Users='Auditor'";
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
                                    dispMsgAduit +=
                                         "<li class=\"\" id=\"msg" + theSqlDataReader3["ID"].ToString() + "\">" +
                                        "<a style=\"display:inline;\" onclick=\"markMsgReadsys('" + theSqlDataReader3["ID"].ToString() + "','" + Session["IIT_UID"].ToString() + "')\"><span class=\"icon-thumbnail \"><i class=\"fs-14 fa fa-check\" title=\"Mark as read\"></i></span></a>" +
                                        "<a href=\"#\" style=\"color:#ffffff;\">" + msgDisp + "</a>" +
                                        "</li>";
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

                dispMsgInspec.InnerHtml = dispMsgAduit + "</ul>";
            }

            Administration.Visible = false;
            COCStatement.Visible = false;
            //Plumbers.Visible = false;
            Resellers.Visible = false;
            Audits.Visible = false;

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from UserRights where UserID=@UserID and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("UserID", uid);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["Menu"].ToString() == "COCStatement")
                    {
                        COCStatement.Visible = true;
                    }
                    else if (theSqlDataReader["Menu"].ToString() == "Plumbers")
                    {
                        //Plumbers.Visible = true;
                    }
                    else if (theSqlDataReader["Menu"].ToString() == "Resellers")
                    {
                        Resellers.Visible = true;
                    }
                    else if (theSqlDataReader["Menu"].ToString() == "Audits")
                    {
                        Audits.Visible = true;
                    }
                    else if (theSqlDataReader["Menu"].ToString() == "Administration")
                    {
                        Administration.Visible = true;
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }
    }
}