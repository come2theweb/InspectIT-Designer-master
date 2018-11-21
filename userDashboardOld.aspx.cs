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
using System.Drawing;

namespace InspectIT
{
    public partial class userDashboardOld : System.Web.UI.Page
    {
        // this is no longer in use because the navigation  bar doesn't work
        public string personalPoints = "";
        public string immediatePoints = "";
        public string leaderPoints = "";
        public string regionalPoints = "";
        public string personalBoard = "";
        public string UserID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }

            UserID = Session["IIT_UID"].ToString();

            errorFriend.Visible = false;
            string userProvince = "";
            string ResidentialCity = "";
            string usersID = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Users where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                theSqlDataReader.Read();
                Image1.ImageUrl = "Photos/" + theSqlDataReader["Photo"].ToString();
                userProvince= theSqlDataReader["Province"].ToString();
                ResidentialCity = theSqlDataReader["ResidentialCity"].ToString();
                usersID = theSqlDataReader["UserID"].ToString();
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Ticker where isActive='1' and GETDATE() between StartDate and EndDate";
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    tickerNotice.InnerHtml += theSqlDataReader["Notice"].ToString() + "<br/>";
                }
            }
            else
            {
                tickerNotice.Visible = false;
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.RS.Close();

            decimal designationPoints = 0;
            decimal cpdPoints = 0;
            string badgesDisp = "";
            decimal performancePoints = 0;
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT sum(NoPoints) as totPoinnt FROM Assessments where isActive='1' and isApproved='1' and UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["totPoinnt"].ToString() != "" && theSqlDataReader["totPoinnt"] != DBNull.Value)
                    {
                        totalCPD.InnerHtml = theSqlDataReader["totPoinnt"].ToString();
                        ytdCpd.InnerHtml = theSqlDataReader["totPoinnt"].ToString();
                        cpdPoints = Convert.ToDecimal(theSqlDataReader["totPoinnt"].ToString());
                    }
                    
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select count(*) as totCocs from COCStatements where UserID=@UserID and Status='Logged'";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    cocsCount.InnerHtml = theSqlDataReader["totCocs"].ToString() + "<br/>";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "select sum(cast(performancePointAllocation as int)) as totPerformancePoints from PerformanceStatus where UserID=@UserID";
            DLdb.SQLST.CommandText = "select sum(cast(Points as decimal)) as totPerformancePoints from AssignedWeighting where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["totPerformancePoints"].ToString() != "" && theSqlDataReader["totPerformancePoints"] != DBNull.Value)
                    {
                        PerformancePoints.InnerHtml = theSqlDataReader["totPerformancePoints"].ToString() + "<br/>";
                        performancePoints = Convert.ToDecimal(theSqlDataReader["totPerformancePoints"].ToString());
                    }
                    
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select sum(Points) as tot from AssignedDesignationSpecialisationPoints where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["tot"] == DBNull.Value)
                    {
                        designationPoints = 0;
                    }
                    else
                    {
                        designationPoints = Convert.ToDecimal(theSqlDataReader["tot"].ToString());
                    }
                    
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            decimal bigRedTotal = designationPoints + performancePoints + cpdPoints;
            totPointRed.InnerHtml = bigRedTotal.ToString();


            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select ROW_NUMBER() OVER(ORDER BY sum(NoPoints) desc) AS Row,sum(NoPoints) as tot,u.UserID from users u inner join Assessments a on u.UserID=a.UserID group by u.UserID";
            //DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["UserID"].ToString() == Session["IIT_UID"].ToString())
                    {
                        NationalPlacement.InnerHtml = theSqlDataReader["Row"].ToString();
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select ROW_NUMBER() OVER(ORDER BY sum(NoPoints) desc) AS Row,sum(NoPoints) as tot,u.UserID from users u inner join Assessments a on u.UserID=a.UserID where u.Province=@Province group by u.UserID";
            DLdb.SQLST.Parameters.AddWithValue("Province", userProvince.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["UserID"].ToString() == Session["IIT_UID"].ToString())
                    {
                        RegionalPlacement.InnerHtml = theSqlDataReader["Row"].ToString();
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select ROW_NUMBER() OVER(ORDER BY sum(NoPoints) desc) AS Row,sum(NoPoints) as tot,u.UserID from users u inner join Assessments a on u.UserID=a.UserID where u.ResidentialCity=@ResidentialCity group by u.UserID";
            DLdb.SQLST.Parameters.AddWithValue("ResidentialCity", ResidentialCity.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (theSqlDataReader["UserID"].ToString() == Session["IIT_UID"].ToString())
                    {
                        CityPlacement.InnerHtml = theSqlDataReader["Row"].ToString();
                    }
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

        //    immediatePoints = "";
        //public string leaderPoints = "";
        //public string regionalPoints = "";
        DLdb.RS.Open();
            DLdb.SQLST.CommandText = "select ROW_NUMBER() OVER(ORDER BY sum(NoPoints) desc) AS Row,sum(NoPoints) as tot,u.UserID,u.fname,u.lname from users u inner join Assessments a on u.UserID=a.UserID group by u.UserID,u.fname,u.lname";
            DLdb.SQLST.Parameters.AddWithValue("ResidentialCity", ResidentialCity.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    if (personalPoints=="")
                    {
                        personalPoints = theSqlDataReader["tot"].ToString();
                    }
                    else
                    {
                        personalPoints += "," + theSqlDataReader["tot"].ToString();
                    }
                    string usersNames = "";
                    if (theSqlDataReader["UserID"].ToString() == Session["IIT_UID"].ToString())
                    {
                        usersNames = "You";
                    }
                    else
                    {
                        usersNames = theSqlDataReader["fname"].ToString() + " " + theSqlDataReader["lname"].ToString();
                    }
                    personalBoard += 
                        
                    "<div class=\"row\">" +
                    "<div class=\"col-md-3\">" +
                    "    <div class=\"media-left media-middle\">" +
                    "        "+ theSqlDataReader["Row"].ToString() + ". " + usersNames +
                    "    </div>" +
                    "</div>" +
                    "<div class=\"col-md-3\">" +
                    "    <div class=\"media-body text-right\">" +
                    "        <span class=\"text-uppercase text-size-mini text-muted\">"+ theSqlDataReader["tot"].ToString() + "</span><br />" +
                    "    </div>" +
                    "</div>" +
                    "<div class=\"col-md-3\">" +
                    "    <div class=\"media-body text-right\">" +
                    "        <span class=\"text-uppercase text-size-mini text-muted\">abc</span><br />" +
                    "    </div>" +
                    "</div>" +
                    "<div class=\"col-md-3\">" +
                    "    <div class=\"media-body text-right\">" +
                    "        <span class=\"text-uppercase text-size-mini text-muted\">edit</span><br />" +
                    "    </div>" +
                    "</div>" +
                    "</div> ";
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            //listofusers.InnerHtml = personalBoard;

            string reversedClass = "";
            int count = 0;
            string timelineDisp = "";
            DateTime weightingCreated;
            string typeAccomplishment = "";
            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM AssignedWeighting where UserID=@UserID and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    weightingCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                    if (theSqlDataReader["CPDActivityID"].ToString() != "" && theSqlDataReader["CPDActivityID"] != DBNull.Value)
                    {
                        typeAccomplishment = theSqlDataReader["Points"].ToString() + " Points gained by completing CPD Assessment";
                    }
                    else if (theSqlDataReader["PerformanceStatusID"].ToString() != "" && theSqlDataReader["PerformanceStatusID"] != DBNull.Value)
                    {
                        typeAccomplishment = theSqlDataReader["Points"].ToString() + " Points gained by completing Performance Status";
                    }
                    if (theSqlDataReader["WeightingID"].ToString()=="0")
                    {
                        typeAccomplishment = theSqlDataReader["Type"].ToString();
                    }
                    if(count%2 == 0){
                        reversedClass = " reversed ";
                    }
                    else{
                        reversedClass = "";
                    }
                    timelineDisp += ""+

                  "<li class=\"media "+ reversedClass + "\">" +
                  "	<div class=\"media-body\">" +
                  "		<div class=\"media-content\">" + typeAccomplishment + "</div>" +
                  "		<span class=\"media-annotation display-block mt-10\">" + weightingCreated.ToString("dd MMM") + "<a href=\"#\"><i class=\"icon-pin-alt position-right text-muted\"></i></a></span>" +
                  "	</div>" +
                  "</li>";
                    count++;
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM assignedbadges where UserID=@UserID and isActive='1'";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    weightingCreated = Convert.ToDateTime(theSqlDataReader["CreateDate"].ToString());
                    if (count % 2 == 0)
                    {
                        reversedClass = " reversed ";
                    }
                    else
                    {
                        reversedClass = "";
                    }
                    timelineDisp += "" +
                    "<li class=\"media" + reversedClass + "\">" +
                    "	<div class=\"media-body\">" +
                    "		<div class=\"media-content\">Earned a badge</div>" +
                    "		<span class=\"media-annotation display-block mt-10\">" + weightingCreated.ToString("dd MMM") + "<a href=\"#\"><i class=\"icon-pin-alt position-right text-muted\"></i></a></span>" +
                    "	</div>" +
                    "</li>";
                    count++;
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            timeLineDispItems.InnerHtml = timelineDisp;

            //DLdb.RS.Open();
            //DLdb.SQLST.CommandText = "SELECT * FROM PlumberSpecialisations where PlumberID=@PlumberID";
            //DLdb.SQLST.Parameters.AddWithValue("PlumberID", Session["IIT_UID"].ToString());
            //DLdb.SQLST.CommandType = CommandType.Text;
            //DLdb.SQLST.Connection = DLdb.RS;
            //theSqlDataReader = DLdb.SQLST.ExecuteReader();

            //if (theSqlDataReader.HasRows)
            //{
            //    while (theSqlDataReader.Read())
            //    {
            //        DLdb.RS2.Open();
            //        DLdb.SQLST2.CommandText = "SELECT * FROM badges where Item=@Item";
            //        DLdb.SQLST2.Parameters.AddWithValue("Item", theSqlDataReader["Specialisation"].ToString());
            //        DLdb.SQLST2.CommandType = CommandType.Text;
            //        DLdb.SQLST2.Connection = DLdb.RS2;
            //        SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

            //        if (theSqlDataReader2.HasRows)
            //        {
            //            while (theSqlDataReader2.Read())
            //            {
            //                badgesDisp += "<img src='Badges/" + theSqlDataReader2["Badge"].ToString() + "' style='height:50px;'  title=\"" + theSqlDataReader2["Item"].ToString() + "\"/>";
            //            }
            //        }

            //        if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
            //        DLdb.SQLST2.Parameters.RemoveAt(0);
            //        DLdb.RS2.Close();
            //    }
            //}

            //if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            //DLdb.SQLST.Parameters.RemoveAt(0);
            //DLdb.RS.Close();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM AssignedBadges where UserID=@UserID";
            DLdb.SQLST.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM badges where BadgeID=@BadgeID";
                    DLdb.SQLST2.Parameters.AddWithValue("BadgeID", theSqlDataReader["BadgeID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            badgesDisp += "<img src='Badges/" + theSqlDataReader2["Badge"].ToString() + "' style='height:50px;width:50px;'  title=\"" + theSqlDataReader2["Item"].ToString() + "\"/>";
                        }
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();
            
            userBadges.InnerHtml = badgesDisp;


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Global DLdb = new Global();
            DLdb.DB_Connect();

            DLdb.RS.Open();
            DLdb.SQLST.CommandText = "SELECT * FROM Users where regno=@regno";
            DLdb.SQLST.Parameters.AddWithValue("regno", friendRegNO.Text.ToString());
            DLdb.SQLST.CommandType = CommandType.Text;
            DLdb.SQLST.Connection = DLdb.RS;
            SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

            if (theSqlDataReader.HasRows)
            {
                while (theSqlDataReader.Read())
                {
                    DLdb.RS2.Open();
                    DLdb.SQLST2.CommandText = "SELECT * FROM Friends where UserID=@UserID and FriendUserID=@FriendUserID";
                    DLdb.SQLST2.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                    DLdb.SQLST2.Parameters.AddWithValue("FriendUserID", theSqlDataReader["UserID"].ToString());
                    DLdb.SQLST2.CommandType = CommandType.Text;
                    DLdb.SQLST2.Connection = DLdb.RS2;
                    SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                    if (theSqlDataReader2.HasRows)
                    {
                        while (theSqlDataReader2.Read())
                        {
                            errorFriend.Visible = true;
                            errorFriend.InnerHtml = "Friend already added to your list";
                        }
                    }
                    else
                    {
                        DLdb.RS3.Open();
                        DLdb.SQLST3.CommandText = "insert into Friends (UserID,FriendUserID) values (@UserID,@FriendUserID)";
                        DLdb.SQLST3.Parameters.AddWithValue("UserID", Session["IIT_UID"].ToString());
                        DLdb.SQLST3.Parameters.AddWithValue("FriendUserID", theSqlDataReader["UserID"].ToString());
                        DLdb.SQLST3.CommandType = CommandType.Text;
                        DLdb.SQLST3.Connection = DLdb.RS3;
                        SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();
                        
                        if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.SQLST3.Parameters.RemoveAt(0);
                        DLdb.RS3.Close();
                    }

                    if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.SQLST2.Parameters.RemoveAt(0);
                    DLdb.RS2.Close();
                }
            }
            else
            {
                errorFriend.Visible = true;
                errorFriend.InnerHtml = "RegNo does not exist";
            }

            if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
            DLdb.SQLST.Parameters.RemoveAt(0);
            DLdb.RS.Close();

            DLdb.DB_Close();
        }
    }
}