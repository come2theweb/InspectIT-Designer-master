using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InspectIT
{
    public partial class zBgCreateJSONUsersCompanies : System.Web.UI.Page
    {
        public class CustomUser
        {
            //public int UserId { get; set; }
            //public string Name { get; set; }
            //public string Address { get; set; }
            //public int Age { get; set; }
            public string companyID { get; set; }
            public string companyName { get; set; }
            public string licensedCount { get; set; }
            public string nonLicensedCount { get; set; }
            public string Edit { get; set; }
            public string delete { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            IList<CustomUser> compList = new List<CustomUser>();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Companies where isActive='1'";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    int LicensedCount = 0;
                    int NonLicensedCOunt = 0;

                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "select * from users where company=@company";
                    DLdb.SQLST2.Parameters.AddWithValue("company", theSqlDataReader["companyid"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            DLdb.RS3.Open();
                            DLdb.SQLST3.CommandText = "select * from PlumberDesignations where PlumberID=@PlumberID";
                            DLdb.SQLST3.Parameters.AddWithValue("PlumberID", theSqlDataReader2["UserID"].ToString());
                            DLdb.SQLST3.CommandType = CommandType.Text;
                            DLdb.SQLST3.Connection = DLdb.RS3;
                            SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                            if (theSqlDataReader3.HasRows)
                            {
                                while (theSqlDataReader3.Read())
                                {
                                    if (theSqlDataReader3["Designation"].ToString() == "Licensed Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Director Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Master Plumber")
                                    {
                                        LicensedCount++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Qualified Plumber")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Learner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Technical Operator Practitioner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                    else if (theSqlDataReader3["Designation"].ToString() == "Technical Assistant Practitioner")
                                    {
                                        NonLicensedCOunt++;
                                    }
                                }
                            }

                            if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                            DLdb.SQLST3.Parameters.RemoveAt(0);
                            DLdb.RS3.Close();
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                    //Page.companyID = theSqlDataReader["CompanyID"].ToString();
                    //Page.companyName = theSqlDataReader["CompanyName"].ToString();
                    //Page.licensedCount = LicensedCount.ToString();
                    //Page.nonLicensedCount = NonLicensedCOunt.ToString();
                    compList.Add(new CustomUser() { companyID = theSqlDataReader["CompanyID"].ToString(), companyName = theSqlDataReader["CompanyName"].ToString(), licensedCount = LicensedCount.ToString(), nonLicensedCount = NonLicensedCOunt.ToString(), Edit = "ed", delete = "de" });

                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();


            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "insert into json (Companies) values (@Companies)";
            DLdb.SQLST.Parameters.AddWithValue("Companies", compList.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();
            
            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();



            DLdb.DB_Close();
        }
    }
}