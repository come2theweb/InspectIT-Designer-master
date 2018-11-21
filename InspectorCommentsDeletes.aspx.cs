﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;

namespace InspectIT
{
    public partial class InspectorCommentsDeletes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            Global DLdb = new Global();
            DLdb.DB_Connect();
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "update InspectorCommentTemplate set isactive='1' where commentid=@commentid";
            DLdb.SQLST.Parameters.AddWithValue("CommentID", DLdb.Decrypt(Request.QueryString["cid"].ToString()));

            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();

            Response.Redirect("InspectorComments.aspx?msg=" + DLdb.Encrypt("Comment added"));


        }
        
    }
}