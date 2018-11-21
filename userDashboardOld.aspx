<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="userDashboardOld.aspx.cs" Inherits="InspectIT.userDashboardOld" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/lodash.min.js"></script>
    <link href="assets/css/components.css" rel="stylesheet" />
    <link href="assets/css/core.css" rel="stylesheet" />
    <link href="pages/css/icons/icomoon/styles.css" rel="stylesheet" type="text/css">



    <link href="https://fonts.googleapis.com/css?family=Roboto:400,300,100,500,700,900" rel="stylesheet" type="text/css">
    <link href="assets/css/icons/icomoon/styles.css" rel="stylesheet" type="text/css">
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/bootstrap_limitless.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/layout.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/components.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/colors.min.css" rel="stylesheet" type="text/css">
    <!-- /global stylesheets -->

    <!-- Core JS files -->
    <script src="assets/js/main/jquery.min.js"></script>
    <script src="assets/js/main/bootstrap.bundle.min.js"></script>
    <script src="assets/js/plugins/loaders/blockui.min.js"></script>
    <!-- /core JS files -->

    <!-- Theme JS files -->
    <script src="assets/js/plugins/visualization/d3/d3.min.js"></script>
    <script src="assets/js/plugins/visualization/d3/d3_tooltip.js"></script>
    <script src="assets/js/plugins/forms/styling/switchery.min.js"></script>
    <script src="assets/js/plugins/forms/selects/bootstrap_multiselect.js"></script>
    <script src="assets/js/plugins/ui/moment/moment.min.js"></script>
    <script src="assets/js/plugins/pickers/daterangepicker.js"></script>

    <script src="assets/js/app.js"></script>
    <script src="assets/js/demo_pages/dashboard.js"></script>

    <!-- Theme JS files -->
    <%--<script src="assets/js/plugins/visualization/d3/d3.min.js"></script>--%>


    <script src="assets/js/app.js"></script>

    <!-- /theme JS files -->

    <style>
        .chart {
            width: 100%;
        }

        .timeline {
            --uiTimelineMainColor: var(--timelineMainColor, #222);
            --uiTimelineSecondaryColor: var(--timelineSecondaryColor, #fff);
            position: relative;
            padding-top: 3rem;
            padding-bottom: 3rem;
        }

            .timeline:before {
                content: "";
                width: 4px;
                height: 100%;
                background-color: var(--uiTimelineMainColor);
                position: absolute;
                top: 0;
            }

        .timeline__group {
            position: relative;
        }

            .timeline__group:not(:first-of-type) {
                margin-top: 4rem;
            }

        .timeline__year {
            padding: .5rem 1.5rem;
            color: var(--uiTimelineSecondaryColor);
            background-color: var(--uiTimelineMainColor);
            position: absolute;
            left: 0;
            top: 0;
        }

        .timeline__box {
            position: relative;
        }

            .timeline__box:not(:last-of-type) {
                margin-bottom: 30px;
            }

            .timeline__box:before {
                content: "";
                width: 100%;
                height: 2px;
                background-color: var(--uiTimelineMainColor);
                position: absolute;
                left: 0;
                z-index: -1;
            }

        .timeline__date {
            min-width: 65px;
            position: absolute;
            left: 0;
            box-sizing: border-box;
            padding: .5rem 1.5rem;
            text-align: center;
            background-color: var(--uiTimelineMainColor);
            color: var(--uiTimelineSecondaryColor);
        }

        .timeline__day {
            font-size: 2rem;
            font-weight: 700;
            display: block;
        }

        .timeline__month {
            display: block;
            font-size: .8em;
            text-transform: uppercase;
        }

        .timeline__post {
            padding: 1.5rem 2rem;
            border-radius: 2px;
            border-left: 3px solid var(--uiTimelineMainColor);
            box-shadow: 0 1px 3px 0 rgba(0, 0, 0, .12), 0 1px 2px 0 rgba(0, 0, 0, .24);
            background-color: var(--uiTimelineSecondaryColor);
        }

        @media screen and (min-width: 641px) {

            .timeline:before {
                left: 30px;
            }

            .timeline__group {
                padding-top: 55px;
            }

            .timeline__box {
                padding-left: 80px;
            }

                .timeline__box:before {
                    top: 50%;
                    transform: translateY(-50%);
                }

            .timeline__date {
                top: 50%;
                margin-top: -27px;
            }
        }

        @media screen and (max-width: 640px) {

            .timeline:before {
                left: 0;
            }

            .timeline__group {
                padding-top: 40px;
            }

            .timeline__box {
                padding-left: 20px;
                padding-top: 70px;
            }

                .timeline__box:before {
                    top: 90px;
                }

            .timeline__date {
                top: 0;
            }
        }

        .timeline {
            --timelineMainColor: #4557bb;
            font-size: 16px;
        }

        @media screen and (min-width: 768px) {

            html {
                font-size: 62.5%;
            }
        }

        @media screen and (max-width: 767px) {

            html {
                font-size: 55%;
            }
        }

        /*
* demo page
*/

        @media screen and (min-width: 768px) {

            html {
                font-size: 62.5%;
            }
        }

        @media screen and (max-width: 767px) {

            html {
                font-size: 50%;
            }
        }

        body {
            font-family: -apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Open Sans, Ubuntu, Fira Sans, Helvetica Neue, sans-serif;
            font-size: 1.6rem;
            color: #222;
            background-color: #f0f0f0;
            margin: 0;
            -webkit-overflow-scrolling: touch;
            overflow-y: scroll;
        }

        p {
            margin-top: 0;
            margin-bottom: 1.5rem;
            line-height: 1.5;
        }

            p:last-child {
                margin-bottom: 0;
            }

        .page {
            min-height: 100vh;
            display: flex;
            flex-direction: column;
            justify-content: space-around;
        }

        .page__demo {
            flex-grow: 1;
        }

        .main-container {
            max-width: 960px;
            padding-left: 2rem;
            padding-right: 2rem;
            margin-left: auto;
            margin-right: auto;
        }

        .page__container {
            padding-top: 30px;
            padding-bottom: 30px;
            max-width: 800px;
        }

        .footer {
            padding-top: 1rem;
            padding-bottom: 1rem;
            text-align: center;
            font-size: 1.4rem;
        }

        .footer__link {
            text-decoration: none;
            color: inherit;
        }

        @media screen and (min-width: 361px) {

            .footer__container {
                display: flex;
                justify-content: space-between;
            }
        }

        @media screen and (max-width: 360px) {

            .melnik909 {
                display: none;
            }
        }
        /*.tooltip .tooltiptext {
    visibility: hidden;
    width: 120px;
    background-color: black;
    color: #fff;
    text-align: center;
    border-radius: 6px;
    padding: 5px 0;
    position: absolute;
    z-index: 1;
    top: 150%;
    left: 50%;
    margin-left: -60px;
}

.tooltip .tooltiptext::after {
    content: "";
    position: absolute;
    bottom: 100%;
    left: 50%;
    margin-left: -5px;
    border-width: 5px;
    border-style: solid;
    border-color: transparent transparent black transparent;
}

.tooltip:hover .tooltiptext {
    visibility: visible;
}*/

        .axis {
        font: 10px sans-serif;
    }

    .axis path,
    .axis line {
      fill: none;
      stroke: lightgrey;
      stroke-width: 0px;
      shape-rendering: crispEdges;
    }

    .line {
      fill: none;
      stroke: #0074D9;
      stroke-width: 3px;
    }

    .label {
      background-color: #fff;
      font-family: sans-serif;
      font-size: 12px;
    }

    .zero {
      stroke: lightgrey;
      stroke-width: 1px;
    }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- START CONTAINER FLUID -->
    <div class="container-fluid container-fixed-lg bg-white">
        <!-- START PANEL -->
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div class="panel-title">
                    Dashboard
                </div>

            </div>
            <div class="panel-transparent">
                <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>

                <div class="row">
                    <div class="col-md-8" style="padding-left: 3px;">

                        <div class="row">
                            <div class="card" style="width: 100%;">
                                <div class="card-body">
                                    <div class="panel panel-body panel-body-accent">
                                        <div class="media no-margin">
                                            <div class="col-md-2">
                                                <div class="media-left media-middle">
                                                    <asp:Image ID="Image1" runat="server" CssClass="img-circle" Style="height: 100px; width: 100px;" />
                                                    <p style="text-align: center;"><%=Session["IIT_UName"].ToString()%></p>
                                                </div>

                                            </div>
                                            <div class="col-md-6">
                                                <div class="media-body text-center">
                                                    <h2 id="totPointRed" runat="server" style="color:red;font-weight:bold;font-size:38px;"></h2>
                                                    <div class="col-sm-4 col-xs-4">
                                                        <div class="media no-margin">
                                                            <div class="media-left media-middle">
                                                                <i class="icon-files-empty icon-2x text-success"></i>
                                                            </div>

                                                            <div class="media-body text-left">
                                                                <h3 class="no-margin text-semibold" id="totalCPD" runat="server">0</h3>
                                                                <span class="text-uppercase text-size-mini text-muted">CPD</span>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-sm-4 col-xs-4">
                                                        <div class="media no-margin">
                                                            <div class="media-left media-middle">
                                                                <i class="icon-files-empty icon-2x text-success"></i>
                                                            </div>

                                                            <div class="media-body text-left">
                                                                <h3 class="no-margin text-semibold" id="PerformancePoints" runat="server">0</h3>
                                                                <span class="text-uppercase text-size-mini text-muted">Performance</span>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-sm-4 col-xs-4">
                                                        <div class="media no-margin">
                                                            <div class="media-left media-middle">
                                                                <i class="icon-files-empty icon-2x text-success"></i>
                                                            </div>

                                                            <div class="media-body text-left">
                                                                <h3 class="no-margin text-semibold" id="cocsCount" runat="server">0</h3>
                                                                <span class="text-uppercase text-size-mini text-muted">COC's</span>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="media-body text-right">
                                                    <span class="text-uppercase text-size-mini text-muted">Your Current Rank:</span><br />
                                                    <span style="color: red;" id="NationalPlacement" runat="server">0</span> National<br />
                                                    <span style="color: red;" id="RegionalPlacement" runat="server">0</span> Regional<br />
                                                    <span style="color: red;" id="CityPlacement" runat="server">0</span> City
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <label style="padding: 10px;">Latest News</label>
                                                <div class="alert alert-danger" id="tickerNotice" runat="server"></div>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="col-md-4">

                        <div class="card" style="width: 100%;">
                            <div class="card-body">
                                <div class="row">
                                    <div class="panel panel-body panel-body-accent">
                                        <div class="media no-margin">
                                            <div class="col-md-6">
                                                <div style="font-weight: bold;" class="media-left media-middle">
                                                    My CPD Status
                                                </div>
                                                <h2><i class="icon-user"></i><span id="ytdCpd" runat="server"></span></h2>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="media-body text-right">
                                                    <span class="text-uppercase text-size-mini text-muted">Required Points</span><br />
                                                    <h2>25</h2>
                                                </div>
                                            </div>
                                            <div style="font-weight: bold;" class="media-left media-middle">
                                                My Badges
                                            </div>
                                            <div class="col-md-12" id="userBadges" runat="server"></div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-8" style="padding-left: 3px;">
                        <div class="card" style="width: 100% !important;">
                            
                            <div class="card-body">

                                <div class="row">
                                    <div class="col-md-8">
                                        <input id="Button5" type="button" value="Personal" class="btn btn-primary" onclick="showPersonal()" />
                                        <input id="Button2" type="button" value="Immediate" class="btn btn-primary" onclick="showIntermediate()" />
                                        <input id="Button3" type="button" value="leaderboard" class="btn btn-primary" onclick="showLeaderboard()" />
                                        <input id="Button4" type="button" value="Regional" class="btn btn-primary" onclick="showRegional()" />
                                    </div>
                                    <div class="col-md-4">
                                        <div id="statusTypeRegional">
                                            <select class="form-control" id="selectStatus" onchange="changeStatRegional(this);">
                                                <option value="">Status type</option>
                                                <option value="Total Average">Total Average</option>
                                                <option value="Total Average CPD">Total Average CPD</option>
                                                <option value="Total Average Performance Status">Total Average Performance Status</option>
                                            </select>
                                        </div>

                                        <div id="leaderBoardFilter">
                                            <select class="form-control" id="selectSregioncity" >
                                                <option value="">Select one</option>
                                                <option value="City">City</option>
                                                <option value="Regional">Regional</option>
                                                <option value="Nationl">Nationl</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>

                                <div class="chart-container">
                                    <div class="col-md-12" id="PersonalChart" runat="server"></div>
                                </div>

                            </div>
                        </div>

                        <div class="card" style="width: 100% !important;">
                            <div class="card-header header-elements-inline">
                                <h5 class="card-title"></h5>
                                <div class="header-elements">
                                </div>
                            </div>

                            <div class="card-body">
                                <div class="chart-container">
                                    <svg width="600" height="500"></svg>
                                    <script src="https://d3js.org/d3.v4.min.js"></script>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="col-md-4">
                        <div class="card" style="width: 100% !important;">
                            <div class="card-body">
                                    <span class="alert alert-danger" id="errorFriend" runat="server"></span>
                                <div class="row" id="addFriend">
                                    <label style="padding: 10px;">Add a friend to your list</label>
                                    <div class="col-sm-8 col-xs-4">
                                        <asp:TextBox ID="friendRegNO" runat="server" CssClass="form-control" placeholder="PIRB Registration Number"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4 col-xs-4">
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-danger" Text="Add" OnClick="Button1_Click" />
                                    </div>
                                </div>
                                


                                  


                                <div class="row">
                                    <div class="panel panel-body panel-body-accent">
                                        <table class="table table-striped table-responsive">
                                            <tr>
                                                <th>Name</th>
                                                <th>Points</th>
                                                <th style="text-align: center;">Badges</th>
                                                <th></th>
                                            </tr>
                                            <tbody id="listofusers" runat="server"></tbody>
                                        </table>
                                    </div>

                                    <div class="panel-body">
                                        <ul class="media-list chat-list content-group" id="timeLineDispItems" runat="server">
                                        </ul>

                                    </div>
                                </div>
                            </div>



                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-8">
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>



            </div>
        </div>
        <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->
    <script>
        function deleteconf(url) {
            var result = confirm("Remove Friend?");
            if (result) {
                document.location.href = url;
            }
        }

        $("#addFriend").show();
        $("#statusTypeRegional").hide();
        $("#leaderBoardFilter").hide();
        
        $.ajax({
            type: "POST",
            url: 'API/WebService1.asmx/getGamificationPersonalChart',
            data: { UserID: <%=UserID%> },
            success: function (data) {
                $("#<%=PersonalChart.ClientID%>").html(data);
            },
        });

        $.ajax({
            type: "POST",
            url: 'API/WebService1.asmx/getGamificationPersonalboard',
            data: { UserID: <%=UserID%> },
            success: function (data) {
                $("#<%=listofusers.ClientID%>").html(data);
            },
        });

        function showPersonal() {

            $.ajax({
                type: "POST",
                url: 'API/WebService1.asmx/getGamificationPersonalChart',
                data: { UserID: <%=UserID%> },
                success: function (data) {
                    $("#<%=PersonalChart.ClientID%>").html(data);
                },
                });

            $.ajax({
                type: "POST",
                url: 'API/WebService1.asmx/getGamificationPersonalboard',
                data: { UserID: <%=UserID%> },
                success: function (data) {
                    $("#<%=listofusers.ClientID%>").html(data);
                },
            });

        }

        function showRegional() {
            $("#leaderBoardFilter").hide();
            $("#statusTypeRegional").show();
            $.ajax({
                type: "POST",
                url: 'API/WebService1.asmx/getGamificationRegionalChart',
                data: { UserID: <%=UserID%>,sel: "Total Average" },
                    success: function (data) {
                        $("#<%=PersonalChart.ClientID%>").html(data);
                    },
            });

            $.ajax({
                type: "POST",
                url: 'API/WebService1.asmx/getGamificationRegionalboard',
                data: { UserID: <%=UserID%>,sel: "Total Average" },
                    success: function (data) {
                        $("#<%=listofusers.ClientID%>").html(data);
                    },
                });
        }

        function showLeaderboard() {
            $("#statusTypeRegional").hide();
            $("#leaderBoardFilter").show();
            $.ajax({
                type: "POST",
                url: 'API/WebService1.asmx/getGamificationleaderBoardChart',
                data: { UserID: <%=UserID%> },
                    success: function (data) {
                        $("#<%=PersonalChart.ClientID%>").html(data);
                    },
             });

             $.ajax({
                 type: "POST",
                 url: 'API/WebService1.asmx/getGamificationLeaderboard',
                 data: { UserID: <%=UserID%> },
                    success: function (data) {
                        $("#<%=listofusers.ClientID%>").html(data);
                    },
                });
        }
        
        function changeStatRegional(sel) {
            console.log(sel.value);
            $.ajax({
                type: "POST",
                url: 'API/WebService1.asmx/getGamificationRegionalChart',
                data: { UserID: <%=UserID%>,sel: sel.value },
                    success: function (data) {
                        $("#<%=PersonalChart.ClientID%>").html(data);
                    },
            });

            $.ajax({
            type: "POST",
            url: 'API/WebService1.asmx/getGamificationRegionalboard',
            data: { UserID: <%=UserID%>,sel: sel.value },
                success: function (data) {
                    $("#<%=listofusers.ClientID%>").html(data);
                },
            });
        }
        

        function showIntermediate() {
            $.ajax({
                type: "POST",
                url: 'API/WebService1.asmx/getGamificationIntermediateChart',
                data: { UserID: <%=UserID%> },
                success: function (data) {
                    $("#<%=PersonalChart.ClientID%>").html(data);
                },
            });
        }

        var data = [
            { "year": "10", "value": -26, "name": "Veronike Kitchen" },
            { "year": "11", "value": 48, "name": "Steve Brown" },
            { "year": "12", "value": 12, "name": "Mat Payne" }
        ]
        
        var svg = d3.select("svg"),
            margin = { top: 20, right: 60, bottom: 30, left: 40 },
            width = +svg.attr("width") - margin.left - margin.right,
            height = +svg.attr("height") - margin.top - margin.bottom;

        //var parseTime = d3.timeParse("%Y")
        //bisectDate = d3.bisector(function (d) { return d.year; }).left;

        var x = d3.scaleLinear().range([0, width]);
        var y = d3.scaleTime().range([height, 0]);

        var line = d3.line()
            .x(function (d) { return x(d.value); })
            .y(function (d) { return y(d.year); })
            .curve(d3.curveStepAfter);

        var g = svg.append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

       
        data.forEach(function (d) {
            d.year = d.year;
            d.value = +d.value;
        });

        y.domain(d3.extent(data, function (d) { return d.year; }).reverse());
        x.domain([d3.min(data, function (d) { return d.value; }), d3.max(data, function (d) { return d.value; }) * 1.005]);

        g.append("g")
            .attr("class", "axis axis--x")
            .attr("transform", "translate(0," + height + ")");

        g.append("g")
            .attr("class", "axis axis--y")
            .call(d3.axisLeft(y))
            .append("text")
            .attr("class", "axis-title")
            .attr("transform", "rotate(-90)")
            .attr("y", 6)
            .attr("dy", ".71em")
            .style("text-anchor", "end")
            .attr("fill", "#5D6971")
            .text("lol");

        g.append("line")
            .attr("class", "zero")
            .attr("stroke-dasharray", "5,5")
            .attr("x1", x(0))
            .attr("y1", 0)
            .attr("x2", x(0))
            .attr("y2", height)
            .attr("transform", "translate(30,0)");

        g.append("path")
            .datum(data)
            .attr("class", "line")
            .attr("transform", "translate(30,0)")
            .attr("d", line);

        g.append("g")
            .attr("class", "rects")
            .selectAll("text")
            .data(data)
            .enter()
            .append("rect")
            .attr("x", function (d) { return x(d.value); })
            .attr("y", function (d) { return y(d.year); })
            .attr("width", 35)
            .attr("height", 20)
            .attr("transform", "translate(15,-15)")
            .style("fill", "#fff");

        g.append("g")
            .attr("class", "labels")
            .selectAll("text")
            .data(data)
            .enter()
            .append("text")
            .attr("class", "label")
            .attr("x", function (d) { return x(d.value); })
            .attr("y", function (d) { return y(d.year); })
            .attr("text-anchor", "middle")
            .attr("transform", "translate(30,0)")
            .text(function (d) {
                return d.value + ' - ' + d.name;
            });
    </script>


</asp:Content>
