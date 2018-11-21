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
    public partial class RatesMaintainAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            // ADMIN CHECK
            if (Session["IIT_Role"].ToString() != "Administrator")
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



            if (!IsPostBack)
            {
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from SupplyItems where isActive = '1' order by Description";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        supplyItemsDrop.Items.Add(new ListItem(theSqlDataReader["Description"].ToString(), theSqlDataReader["ID"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();

                if (Request.QueryString["rid"] != null)
                {
                    updateRates.Visible = true;
                    saveRates.Visible = false;

                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "SELECT * FROM rates where isActive='1' and ID=@ID";
                    DLdb.SQLST.Parameters.AddWithValue("ID", Request.QueryString["rid"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            supplyItemsDrop.SelectedValue = theSqlDataReader["SupplyItemID"].ToString();
                            amount.Text = theSqlDataReader["Amount"].ToString();
                            dates.Text = theSqlDataReader["ValidFrom"].ToString();

                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                    DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();
                }
                else
                {
                    updateRates.Visible = false;
                    saveRates.Visible = true;
                }
            }
            
            

            DLdb.DB_Close();
        }

        protected void saveRates_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into rates (SupplyItemID,Amount,ValidFrom) values (@SupplyItemID,@Amount,@ValidFrom)";
            DLdb.SQLST.Parameters.AddWithValue("SupplyItemID", supplyItemsDrop.SelectedValue);
            DLdb.SQLST.Parameters.AddWithValue("Amount", amount.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ValidFrom", dates.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();
            
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("RatesMaintain.aspx?msg=Rate has been added to the system");
        }

        protected void updateRates_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update rates set SupplyItemID=@SupplyItemID,Amount=@Amount,ValidFrom=@ValidFrom where ID=@ID";
            DLdb.SQLST.Parameters.AddWithValue("SupplyItemID", supplyItemsDrop.SelectedValue);
            DLdb.SQLST.Parameters.AddWithValue("Amount", amount.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ValidFrom", dates.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("ID", Request.QueryString["rid"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("RatesMaintain.aspx?msg=Rate has been updated in the system");
        }
    }
}