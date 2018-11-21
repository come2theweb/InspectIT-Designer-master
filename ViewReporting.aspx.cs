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
    public partial class ViewReporting : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            Global DLdb = new Global();
            DLdb.DB_Connect();

            filterCheckbxs.Visible = false;
            selFieldsView.Visible = false;
            selStat.Visible = false;
            filterheading.Visible = false;
            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            if (Session["IIT_Rights"].ToString() == "View Only")
            {

            }
            else if (Session["IIT_Rights"].ToString() == "View and Edit")
            {

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
                DLdb.SQLST.CommandText = "select [Status] from COCStatements group by [Status]";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        selStat.Items.Add(new ListItem(theSqlDataReader["Status"].ToString(), theSqlDataReader["Status"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();
                

                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Reports where isActive = '1' order by Name";
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        SelDataset.Items.Add(new ListItem(theSqlDataReader["Name"].ToString(), theSqlDataReader["ID"].ToString()));
                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.RS.Close();
            }

            DLdb.DB_Close();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string SQLST = "";
            string Params = "";
            string columnNames = "";
            string columnNamesView = "";
            string tblHeadHTML = "<tr>";
            filterColumnsDisp.Items.Clear();

            // GET STATEMENT AND PARAMS
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Reports where ID=@ID";
            DLdb.SQLST.Parameters.AddWithValue("ID", SelDataset.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["Params"] != DBNull.Value)
                    {
                        List<string> ParamsListNew = theSqlDataReader["Params"].ToString().Split(',').ToList<string>();
                        foreach (string DropDownShow in ParamsListNew)
                        {
                            DropDownList txtName = (DropDownList)stuff.FindControl(DropDownShow);
                            txtName.Visible = true;
                            filterheading.Visible = true;
                        }
                    }
                    //SQLST = "Select * from users u inner join MentorsStudents s on u.UserID = s.StudentID where s.MentorID = @MentorID and s.StudentID = @StudentID";
                    //Params = "StudentID,MentorID";
                    SQLST = theSqlDataReader["SQLST"].ToString();
                    Params = theSqlDataReader["Params"].ToString();
                    //columnNames = theSqlDataReader["ColumnNames"].ToString();
                    columnNames = theSqlDataReader["ColName"].ToString();
                    columnNamesView = theSqlDataReader["ColNameUserSee"].ToString();

                    string[] split = columnNamesView.Split(',');

                    List<string> colNameList = columnNames.Split(',').ToList<string>();

                    int i = 0;
                    foreach (string colParm in colNameList)
                    {

                        tblHeadHTML += "<th>" + colParm.Substring(colParm.IndexOf('.') + 1) + "</th>";
                        filterColumnsDisp.Items.Add(new ListItem(split[i], colParm.Substring(colParm.IndexOf('.') + 1)));
                        i++;
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            // GET DATA
            DLdb.RS2.Open();
            DLdb.SQLST2.CommandText = SQLST;

            // ****************************************************************************************
            // 
            // ****************************************************************************************


            //// BUILD HEADER
            //DLdb.RS3.Open();
            ////DLdb.SQLST3.CommandText = "SELECT COLUMN_NAME,* FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'YourTableName'";
            //DLdb.SQLST3.CommandText = columnNames;
            ////DLdb.SQLST3.Parameters.AddWithValue("Param", "val");
            //DLdb.SQLST3.CommandType = CommandType.Text;
            //DLdb.SQLST3.Connection = DLdb.RS3;
            //SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

            //if (theSqlDataReader3.HasRows)
            //{
            //    while (theSqlDataReader3.Read())
            //    {
            //        string fName = theSqlDataReader3["COLUMN_NAME"].ToString();
            //        tblHeadHTML += "<th>" + fName + "</th>";


            //        filterColumnsDisp.Items.Add(new ListItem(theSqlDataReader3["COLUMN_NAME"].ToString(), theSqlDataReader3["COLUMN_NAME"].ToString()));


            //    }
            //}

            //if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
            ////DLdb.SQLST3.Parameters.RemoveAt(0);
            //DLdb.RS3.Close();

            tblHeadHTML += "</tr>";

            // ****************************************************************************************
            // 
            // ****************************************************************************************
            // CHECK PARAMS
            List<string> ParamsList = Params.Split(',').ToList<string>();
            if (Params != "")
            {

                foreach (string Parm in ParamsList)
                {
                    //Parm
                    // DropDownList txtName = FindControl(Parm) as DropDownList;
                    //DropDownList txtName = (DropDownList)Page.FindControl(Parm);
                    DropDownList txtName = (DropDownList)stuff.FindControl(Parm);
                    if (txtName.SelectedValue.ToString() != "" && txtName.SelectedValue.ToString() != null)
                    {
                        // ADD PARAMETERS TO SQL
                        DLdb.SQLST2.Parameters.AddWithValue(Parm, txtName.SelectedValue.ToString());
                    }
                    else
                    {
                        // show error
                        txtName.Attributes.Add("style", "border: 1px solid red");
                    }

                }
            }


            // ****************************************************************************************
            // 
            // ****************************************************************************************

            string tblDisplayHTML = "";

            DLdb.SQLST2.CommandType = CommandType.Text;
            DLdb.SQLST2.Connection = DLdb.RS2;
            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            if (theSqlDataReader2.HasRows)
            {
                while (theSqlDataReader2.Read())
                {
                    tblDisplayHTML += "<tr>";

                    // GET FIELD NAMES and VALUES
                    DLdb.RS3.Open();
                    //DLdb.SQLST3.CommandText = columnNames;
                    DLdb.SQLST3.CommandText = SQLST.Replace("*", columnNames);
                    List<string> ParamsListNew = Params.Split(',').ToList<string>();
                    if (Params != "")
                    {

                        foreach (string Parm in ParamsListNew)
                        {
                            //Parm
                            // DropDownList txtName = FindControl(Parm) as DropDownList;
                            //DropDownList txtName = (DropDownList)Page.FindControl(Parm);
                            DropDownList txtName = (DropDownList)stuff.FindControl(Parm);
                            if (txtName.SelectedValue.ToString() != "" && txtName.SelectedValue.ToString() != null)
                            {
                                // ADD PARAMETERS TO SQL
                                DLdb.SQLST3.Parameters.AddWithValue(Parm, txtName.SelectedValue.ToString());
                            }
                            else
                            {
                                // show error
                                txtName.Attributes.Add("style", "border: 1px solid red");
                            }

                        }
                    }
                    DLdb.SQLST3.CommandType = CommandType.Text;
                    DLdb.SQLST3.Connection = DLdb.RS3;
                    SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                    if (theSqlDataReader3.HasRows)
                    {
                        theSqlDataReader3.Read();
                        //while (theSqlDataReader3.Read())
                        //{
                        List<string> colNameList = columnNames.Split(',').ToList<string>();

                        foreach (string colParm in colNameList)
                        {
                            string fName = colParm;
                            tblDisplayHTML += "<td>" + theSqlDataReader2[fName.Substring(colParm.IndexOf('.') + 1)].ToString() + "</td>";
                        }

                        //string fName = columnNames;
                        //tblDisplayHTML += "<td>" + theSqlDataReader2[fName.Substring(2)].ToString() + "</td>";
                        //}
                    }

                    if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                    if (Params != "")
                    {
                        foreach (string Parm in ParamsListNew)
                        {
                            //Parm
                            //DropDownList txtName = FindControl(Parm) as DropDownList;
                            DropDownList txtName = (DropDownList)stuff.FindControl(Parm);
                            if (txtName.SelectedValue.ToString() != "")
                            {
                                // ADD PARAMETERS TO SQL
                                DLdb.SQLST3.Parameters.RemoveAt(0);
                            }
                        }
                    }
                    DLdb.RS3.Close();

                    tblDisplayHTML += "</tr>";
                }
            }

            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();


            // ****************************************************************************************
            // 
            // ****************************************************************************************
            if (Params != "")
            {
                foreach (string Parm in ParamsList)
                {
                    //Parm
                    //DropDownList txtName = FindControl(Parm) as DropDownList;
                    DropDownList txtName = (DropDownList)stuff.FindControl(Parm);
                    if (txtName.SelectedValue.ToString() != "")
                    {
                        // ADD PARAMETERS TO SQL
                        DLdb.SQLST2.Parameters.RemoveAt(0);
                    }
                }
            }

            DLdb.RS2.Close();

            DLdb.DB_Close();

            tblHead.InnerHtml = tblHeadHTML;
            tblDisplay.InnerHtml = tblDisplayHTML;
            filterCheckbxs.Visible = true;
            selFieldsView.Visible = true;
        }


        protected void filterCheckbxs_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            string commalist = "";
            string tblHeadHTML = "<tr>";

            foreach (ListItem listItem in filterColumnsDisp.Items)
            {
                if (listItem.Selected)
                {
                    if (commalist == "")
                    {
                        commalist = listItem.Value.ToString();
                        tblHeadHTML += "<th>" + listItem.Value.ToString() + "</th>";
                    }
                    else
                    {
                        commalist = commalist + "," + listItem.Value.ToString();
                        tblHeadHTML += "<th>" + listItem.Value.ToString() + "</th>";
                    }
                }
            }

            tblHeadHTML += "</tr>";
            if (commalist == "")
            {
                errormsg.Visible = true;
                errormsg.InnerHtml = "Please select columns from the checkbox list";
            }
            else
            {
                string SQLST = "";
                string Params = "";
                string columnNames = "";
                string columnNamesView = "";

                // GET STATEMENT AND PARAMS
                DLdb.RS.Open();
                DLdb.SQLST.CommandText = "select * from Reports where ID=@ID";
                DLdb.SQLST.Parameters.AddWithValue("ID", SelDataset.SelectedValue);
                DLdb.SQLST.CommandType = CommandType.Text;
                DLdb.SQLST.Connection = DLdb.RS;
                SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                if (theSqlDataReader.HasRows)
                {
                    while (theSqlDataReader.Read())
                    {
                        if (theSqlDataReader["Params"] != DBNull.Value)
                        {
                            List<string> ParamsListNew = theSqlDataReader["Params"].ToString().Split(',').ToList<string>();
                            foreach (string DropDownShow in ParamsListNew)
                            {
                                DropDownList txtName = (DropDownList)stuff.FindControl(DropDownShow);
                                txtName.Visible = true;
                                filterheading.Visible = true;
                            }
                        }
                        //SQLST = "Select * from users u inner join MentorsStudents s on u.UserID = s.StudentID where s.MentorID = @MentorID and s.StudentID = @StudentID";
                        //Params = "StudentID,MentorID";
                        SQLST = theSqlDataReader["SQLST"].ToString();
                        Params = theSqlDataReader["Params"].ToString();
                        //columnNames = theSqlDataReader["ColumnNames"].ToString();
                        columnNames = theSqlDataReader["ColName"].ToString();
                        columnNamesView = theSqlDataReader["ColNameUserSee"].ToString();

                    }
                }

                if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                DLdb.SQLST.Parameters.RemoveAt(0);
                DLdb.RS.Close();

                // GET DATA
                DLdb.RS2.Open();
                DLdb.SQLST2.CommandText = SQLST.Replace("*", commalist);


                // ****************************************************************************************
                // 
                // ****************************************************************************************
                // CHECK PARAMS
                List<string> ParamsList = Params.Split(',').ToList<string>();
                if (Params != "")
                {

                    foreach (string Parm in ParamsList)
                    {
                        //Parm
                        // DropDownList txtName = FindControl(Parm) as DropDownList;
                        //DropDownList txtName = (DropDownList)Page.FindControl(Parm);
                        DropDownList txtName = (DropDownList)stuff.FindControl(Parm);
                        if (txtName.SelectedValue.ToString() != "" && txtName.SelectedValue.ToString() != null)
                        {
                            // ADD PARAMETERS TO SQL
                            DLdb.SQLST2.Parameters.AddWithValue(Parm, txtName.SelectedValue.ToString());
                        }
                        else
                        {
                            // show error
                            txtName.Attributes.Add("style", "border: 1px solid red");
                        }

                    }
                }


                // ****************************************************************************************
                // 
                // ****************************************************************************************

                string tblDisplayHTML = "";

                DLdb.SQLST2.CommandType = CommandType.Text;
                DLdb.SQLST2.Connection = DLdb.RS2;
                SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                if (theSqlDataReader2.HasRows)
                {
                    while (theSqlDataReader2.Read())
                    {
                        tblDisplayHTML += "<tr>";

                        foreach (ListItem slistItem in filterColumnsDisp.Items)
                        {
                            if (slistItem.Selected)
                            {
                                tblDisplayHTML += "<td>" + theSqlDataReader2[slistItem.Value].ToString() + "</td>";
                            }
                        }

                        tblDisplayHTML += "</tr>";
                    }
                }

                if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();


                // ****************************************************************************************
                // 
                // ****************************************************************************************
                if (Params != "")
                {
                    foreach (string Parm in ParamsList)
                    {
                        //Parm
                        //DropDownList txtName = FindControl(Parm) as DropDownList;
                        DropDownList txtName = (DropDownList)stuff.FindControl(Parm);
                        if (txtName.SelectedValue.ToString() != "")
                        {
                            // ADD PARAMETERS TO SQL
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                        }
                    }
                }

                DLdb.RS2.Close();

                DLdb.DB_Close();

                tblHead.InnerHtml = tblHeadHTML;
                tblDisplay.InnerHtml = tblDisplayHTML;
                filterCheckbxs.Visible = true;
            }
            filterCheckbxs.Visible = true;

        }

        protected void SelDataset_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select * from Reports where ID=@ID";
            DLdb.SQLST.Parameters.AddWithValue("ID", SelDataset.SelectedValue);
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["Params"] != DBNull.Value)
                    {
                        List<string> ParamsList = theSqlDataReader["Params"].ToString().Split(',').ToList<string>();
                        foreach (string Parm in ParamsList)
                        {
                            DropDownList txtName = (DropDownList)stuff.FindControl(Parm);
                            txtName.Visible = true;
                            filterheading.Visible = true;
                        }
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