<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="map.aspx.cs" Inherits="InspectIT.map" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src='http://maps.google.com/maps/api/js?key=AIzaSyCCaRQ0kP8fuCaU5d0lmBSXMSGr7NzaU3A&&sensor=false&amp;libraries=places'></script>

    <style type="text/css">
        #map_canvas {
            height: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="container-fluid container-fixed-lg bg-white">
    <!-- START PANEL -->
    <div class="panel panel-transparent">
        <div class="panel-heading">
        <div class="panel-title">Map
        </div>
        <div class="clearfix"></div>
        </div>
        <div class="panel-body">
            <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
        <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>
       

            <div id="DisplayDiv" runat="server"></div>
                        <div id="mapdisplay" runat="server">
                            <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />

                            <%--<script type="text/javascript"
                          src="http://maps.googleapis.com/maps/api/js?key=AIzaSyCCaRQ0kP8fuCaU5d0lmBSXMSGr7NzaU3A&sensor=false">
                        </script>--%>
                            <div id="map_canvas" style="width: 100%; height: 700px"></div>
                            <%=str_mapdisplay.ToString()%>
                            <%=audstr_mapdisplay.ToString()%>
                        </div>

        </div>
    </div>
    <!-- END PANEL -->
    </div>
</asp:Content>
