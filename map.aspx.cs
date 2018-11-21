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
    public partial class map : System.Web.UI.Page
    {
        protected System.Data.SqlClient.SqlConnection sqlConnection1;
        protected System.Data.OleDb.OleDbConnection oleDbConnection1;

        public string str_mapdisplay = "";
        public string audstr_mapdisplay = "";

        public class GeoLocation
        {
            public decimal Lat { get; set; }
            public decimal Lng { get; set; }
        }

        public class GeoGeometry
        {
            public GeoLocation Location { get; set; }
        }

        public class GeoResult
        {
            public GeoGeometry Geometry { get; set; }
        }

        public class GeoResponse
        {
            public string Status { get; set; }
            public GeoResult[] Results { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // CHECK SESSION
            if (Session["IIT_UID"] == null)
            {
                Response.Redirect("Default");
            }
            if (Session["IIT_Role"].ToString() != "Administrator")
            {
                Response.Redirect("Default");
            }

            Global DLdb = new Global();

            DLdb.DB_Connect();

            if (!IsPostBack)
            {
                
                    DisplayDiv.InnerHtml = "";
                    string str_markers = "";
                    string Latitude = "";
                    string Longitude = "";
                    string name = "";
                    string surname = "";
                    string cusID = "";
                    string IconS = "https://197.242.82.242/vwwvirtualworks/demo/pins/grey-dot.png";
                    string supplierIcon = "https://197.242.82.242/vwwvirtualworks/demo/pins/red-dot.png";
                    string inspectorIcon = "https://197.242.82.242/vwwvirtualworks/demo/pins/blue-dot.png";
                    DLdb.RS.Open();
                    DLdb.SQLST.CommandText = "select * from COCStatements where  isactive='1'"; //
                    //DLdb.SQLST.Parameters.AddWithValue("collectionid", Request.QueryString["cid"].ToString());
                    DLdb.SQLST.CommandType = CommandType.Text;
                    DLdb.SQLST.Connection = DLdb.RS;
                    SqlDataReader theSqlDataReader = DLdb.SQLST.ExecuteReader();

                    if (theSqlDataReader.HasRows)
                    {
                        while (theSqlDataReader.Read())
                        {
                            DLdb.RS2.Open();
                            DLdb.SQLST2.CommandText = "select * from Customers where CustomerID = @CustomerID";
                            DLdb.SQLST2.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
                            DLdb.SQLST2.CommandType = CommandType.Text;
                            DLdb.SQLST2.Connection = DLdb.RS2;
                            SqlDataReader theSqlDataReader2 = DLdb.SQLST2.ExecuteReader();

                            if (theSqlDataReader2.HasRows)
                            {
                                while (theSqlDataReader2.Read())
                                {
                                    Latitude = theSqlDataReader2["lat"].ToString();
                                    Longitude = theSqlDataReader2["lng"].ToString();
                                    name = theSqlDataReader2["customername"].ToString();
                                    surname = theSqlDataReader2["customersurname"].ToString();
                                    cusID = theSqlDataReader2["CustomerID"].ToString();

                                    str_markers = str_markers + "   var myLatlng" + cusID + " = new google.maps.LatLng(" + Latitude + ", " + Longitude + ");" +
                                                " var iconFile" + cusID + " = '" + IconS + "';" +
                                                "   var marker" + cusID + " = new google.maps.Marker({" +
                                                "       position: myLatlng" + cusID + "," +
                                                "       icon: iconFile" + cusID + "," +
                                                "       title: \"" + name + "\"" +
                                                "   });" +
                                                "   var contentString" + cusID + " = '<div id=\"content" + cusID + "\">' +" +
                                                "       '<div id=\"siteNotice" + cusID + "\">' +" +
                                                "       '</div>' +" +
                                                "       '<h2 id=\"firstHeading" + cusID + "\" class=\"firstHeading\">" + name + "</h2>' +" +
                                                "       '<div id=\"bodyContent" + cusID + "\">' +" +
                                                "       '" + name + " " + surname + "<br>(" + theSqlDataReader2["CustomerCellNo"].ToString() + ") <br><a ' +" +
                                                "   'href=\"customer.aspx?cid=" + cusID + "\">Open Record</a>' +" +
                                                "       '</div>' +" +
                                                "       '</div>';" +
                                                "   var infowindow" + cusID + " = new google.maps.InfoWindow({" +
                                                "       content: contentString" + cusID +
                                                "   });" +
                                                "   google.maps.event.addListener(marker" + cusID + ", 'click', function () {" +
                                                "       infowindow" + cusID + ".open(map, marker" + cusID + ");" +
                                                "   });" +
                                                "   marker" + cusID + ".setMap(map);";
                                }
                            }

                            if (theSqlDataReader2.IsClosed) theSqlDataReader2.Close();
                            DLdb.SQLST2.Parameters.RemoveAt(0);
                            DLdb.RS2.Close();
                        }
                    }

                    if (theSqlDataReader.IsClosed) theSqlDataReader.Close();
                   // DLdb.SQLST.Parameters.RemoveAt(0);
                    DLdb.RS.Close();

                    DLdb.DB_Close();

                    //str_mapdisplay = "<script type=\"text/javascript\">" +
                    //                                " var iconFile = \"http://maps.google.com/mapfiles/ms/icons/purple-dot.png\";" +
                    //                                "   var mapOptions = {" +
                    //                                "       center: new google.maps.LatLng(" + Latitude + ", " + Longitude + ")," +
                    //                                "       zoom: 10," +
                    //                                "       mapTypeId: google.maps.MapTypeId.ROADMAP" +
                    //                                "   };" +
                    //                                "   var map = new google.maps.Map(document.getElementById(\"map_canvas\")," +
                    //                                "       mapOptions);" +
                    //                                "   var myLatlng = new google.maps.LatLng(" + Latitude + ", " + Longitude + ");" +
                    //                                "   var markerMyLoc = new google.maps.Marker({" +
                    //                                "       position: myLatlng," +
                    //                                "       icon: iconFile," +
                    //                                "       title: \"Your location\"" +
                    //                                "   });" +
                    //                                "   var contentStringMyLoc = '<div id=\"contentMyLoc\">' +" +
                    //                                "       '<div id=\"siteNoticeMyLoc\">' +" +
                    //                                "       '</div>' +" +
                    //                                "       '<h2 id=\"firstHeadingMyLoc\" class=\"firstHeading\">Your location</h2>' +" +
                    //                                "       '<div id=\"bodyContentMyLoc\"></div>' +" +
                    //                                "       '</div>';" +
                    //                                "   var infowindowMyLoc = new google.maps.InfoWindow({" +
                    //                                "       content: contentStringMyLoc" +
                    //                                "   });" +
                    //                                "   google.maps.event.addListener(markerMyLoc, 'click', function () {" +
                    //                                "       infowindowMyLoc.open(map, markerMyLoc);" +
                    //                                "   });" +
                    //                                "   markerMyLoc.setMap(map);" +
                    //                                "   " + str_markers.ToString() + "" +
                    //                                "   </script>";

                 //   DisplayDiv.InnerHtml = str_mapdisplay;
                // mapdisplay.Visible = true;

                string audstr_markers = "";
                string audLatitude = "";
                string audLongitude = "";
                string audname = "";
                string audsurname = "";
                string audcusID = "";

                // auditors
                DLdb.RS3.Open();
                DLdb.SQLST3.CommandText = "select * from Auditor";
                //DLdb.SQLST3.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
                DLdb.SQLST3.CommandType = CommandType.Text;
                DLdb.SQLST3.Connection = DLdb.RS3;
                SqlDataReader theSqlDataReader3 = DLdb.SQLST3.ExecuteReader();

                if (theSqlDataReader3.HasRows)
                {
                    while (theSqlDataReader3.Read())
                    {
                        audLatitude = theSqlDataReader3["lat"].ToString();
                        audLongitude = theSqlDataReader3["lng"].ToString();
                        audname = theSqlDataReader3["fname"].ToString();
                        audsurname = theSqlDataReader3["lname"].ToString();
                        audcusID = theSqlDataReader3["auditorid"].ToString();

                        audstr_markers = audstr_markers + "   var myLatlng" + audcusID + " = new google.maps.LatLng(" + audLatitude + ", " + audLongitude + ");" +
                                    " var iconFile" + audcusID + " = '" + inspectorIcon + "';" +
                                    "   var marker" + audcusID + " = new google.maps.Marker({" +
                                    "       position: myLatlng" + audcusID + "," +
                                    "       icon: iconFile" + audcusID + "," +
                                    "       title: \"" + audname + "\"" +
                                    "   });" +
                                    "   var contentString" + audcusID + " = '<div id=\"content" + audcusID + "\">' +" +
                                    "       '<div id=\"siteNotice" + audcusID + "\">' +" +
                                    "       '</div>' +" +
                                    "       '<h3 id=\"firstHeading" + audcusID + "\" class=\"firstHeading\">" + audname + "</h3>' +" +
                                    "       '<div id=\"bodyContent" + audcusID + "\">' +" +
                                    "       '" + audname + " " + audsurname + "<br>(" + theSqlDataReader3["phonework"].ToString() + ") <br><a ' +" +
                                    "   'href=\"customer.aspx?cid=" + audcusID + "\">Open Record</a>' +" +
                                    "       '</div>' +" +
                                    "       '</div>';" +
                                    "   var infowindow" + audcusID + " = new google.maps.InfoWindow({" +
                                    "       content: contentString" + audcusID +
                                    "   });" +
                                    "   google.maps.event.addListener(marker" + audcusID + ", 'click', function () {" +
                                    "       infowindow" + audcusID + ".open(map, marker" + audcusID + ");" +
                                    "   });" +
                                    "   marker" + audcusID + ".setMap(map);";
                    }
                }

                if (theSqlDataReader3.IsClosed) theSqlDataReader3.Close();
               // DLdb.SQLST3.Parameters.RemoveAt(0);
                DLdb.RS3.Close();

                string supstr_markers = "";
                string supLatitude = "";
                string supLongitude = "";
                string supname = "";
                string supcusID = "";

                // auditors
                DLdb.RS4.Open();
                DLdb.SQLST4.CommandText = "select * from Suppliers";
                //DLdb.SQLST4.Parameters.AddWithValue("CustomerID", theSqlDataReader["CustomerID"].ToString());
                DLdb.SQLST4.CommandType = CommandType.Text;
                DLdb.SQLST4.Connection = DLdb.RS4;
                SqlDataReader theSqlDataReader4 = DLdb.SQLST4.ExecuteReader();

                if (theSqlDataReader4.HasRows)
                {
                    while (theSqlDataReader4.Read())
                    {
                        supLatitude = theSqlDataReader4["lat"].ToString();
                        supLongitude = theSqlDataReader4["lng"].ToString();
                        supname = theSqlDataReader4["suppliername"].ToString();
                        supcusID = theSqlDataReader4["supplierid"].ToString();

                        supstr_markers = supstr_markers + "   var myLatlng" + supcusID + " = new google.maps.LatLng(" + supLatitude + ", " + supLongitude + ");" +
                                    " var iconFile" + supcusID + " = '" + supplierIcon + "';" +
                                    "   var marker" + supcusID + " = new google.maps.Marker({" +
                                    "       position: myLatlng" + supcusID + "," +
                                    "       icon: iconFile" + supcusID + "," +
                                    "       title: \"" + supname + "\"" +
                                    "   });" +
                                    "   var contentString" + supcusID + " = '<div id=\"content" + supcusID + "\">' +" +
                                    "       '<div id=\"siteNotice" + supcusID + "\">' +" +
                                    "       '</div>' +" +
                                    "       '<h4 id=\"firstHeading" + supcusID + "\" class=\"firstHeading\">" + supname + "</h4>' +" +
                                    "       '<div id=\"bodyContent" + supcusID + "\">' +" +
                                    "       '" + supname + " " + supname + "<br>(" + theSqlDataReader4["suppliercontactno"].ToString() + ") <br><a ' +" +
                                   // "   'href=\"customer.aspx?cid=" + supcusID + "\">Open Record</a>' +" +
                                    "       '</div>' +" +
                                    "       '</div>';" +
                                    "   var infowindow" + supcusID + " = new google.maps.InfoWindow({" +
                                    "       content: contentString" + supcusID +
                                    "   });" +
                                    "   google.maps.event.addListener(marker" + supcusID + ", 'click', function () {" +
                                    "       infowindow" + supcusID + ".open(map, marker" + supcusID + ");" +
                                    "   });" +
                                    "   marker" + supcusID + ".setMap(map);";
                    }
                }

                if (theSqlDataReader4.IsClosed) theSqlDataReader4.Close();
                // DLdb.SQLST4.Parameters.RemoveAt(0);
                DLdb.RS4.Close();

                audstr_mapdisplay = "<script type=\"text/javascript\">" +
                                                    " var iconFile = \"http://maps.google.com/mapfiles/ms/icons/purple-dot.png\";" +
                                                    "   var mapOptions = {" +
                                                    "       center: new google.maps.LatLng(" + audLatitude + ", " + audLongitude + ")," +
                                                    "       zoom: 10," +
                                                    "       mapTypeId: google.maps.MapTypeId.ROADMAP" +
                                                    "   };" +
                                                    "   var map = new google.maps.Map(document.getElementById(\"map_canvas\")," +
                                                    "       mapOptions);" +
                                                    "   var myLatlng = new google.maps.LatLng(" + audLatitude + ", " + audLongitude + ");" +
                                                    "   var markerMyLoc = new google.maps.Marker({" +
                                                    "       position: myLatlng," +
                                                    "       icon: iconFile," +
                                                    "       title: \"Your location\"" +
                                                    "   });" +
                                                    "   var contentStringMyLoc = '<div id=\"contentMyLoc\">' +" +
                                                    "       '<div id=\"siteNoticeMyLoc\">' +" +
                                                    "       '</div>' +" +
                                                    "       '<h2 id=\"firstHeadingMyLoc\" class=\"firstHeading\">Your location</h2>' +" +
                                                    "       '<div id=\"bodyContentMyLoc\"></div>' +" +
                                                    "       '</div>';" +
                                                    "   var infowindowMyLoc = new google.maps.InfoWindow({" +
                                                    "       content: contentStringMyLoc" +
                                                    "   });" +
                                                    "   google.maps.event.addListener(markerMyLoc, 'click', function () {" +
                                                    "       infowindowMyLoc.open(map, markerMyLoc);" +
                                                    "   });" +
                                                    "   markerMyLoc.setMap(map);" +
                                                    "   " + str_markers.ToString() + audstr_markers.ToString() + supstr_markers + "" +
                                                    "   </script>";

                DisplayDiv.InnerHtml = audstr_mapdisplay;

            }
        }
    }
}