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
    public partial class DesignationSpecialisationPointsEdit : System.Web.UI.Page
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

            if (Session["IIT_Rights"].ToString() == "View Only")
            {
                btnUpdatePlumber.Visible = false;
            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {

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
                DLdb.SQLST.CommandText = "SELECT * FROM DesignationSpecialisationPoints where PointID=@PointID";
                DLdb.SQLST.Parameters.AddWithValue("PointID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    theSqlDataReader.Read();
                    Item.Text = theSqlDataReader["Item"].ToString();
                    Points.Text = theSqlDataReader["Points"].ToString();
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                DLdb.DB_Close();
            }
        }

        protected void btnUpdatePlumber_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();

            DLdb.SQLST.CommandText = "UPDATE DesignationSpecialisationPoints SET Item=@Item,Points=@Points WHERE PointID=@PointID";
            DLdb.SQLST.Parameters.AddWithValue("PointID", DLdb.Decrypt(Request.QueryString["id"].ToString()));
            DLdb.SQLST.Parameters.AddWithValue("Item", Item.Text.ToString());
            DLdb.SQLST.Parameters.AddWithValue("Points", Points.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
            Response.Redirect("DesignationSpecialisationPoints.aspx?msg=" + DLdb.Encrypt("Designation / Specialisation Points has been updated"));
        }
    }
}