<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HomeCOCAdmin.aspx.cs" Inherits="InspectIT.HomeCOCAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/lodash.min.js"></script>
     <link href="assets/css/components.css" rel="stylesheet" />
    <link href="assets/css/core.css" rel="stylesheet" />
    <link href="pages/css/icons/icomoon/styles.css" rel="stylesheet" type="text/css">
    <style>
        .chart {
          width: 100%; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- START CONTAINER FLUID -->
    <div class="container-fluid container-fixed-lg bg-white">
    <!-- START PANEL -->
    <div class="panel panel-transparent">
        <div class="panel-heading">
        <div class="panel-title">Dashboard
        </div>
       
        </div>
        <div class="panel-transparent">

            <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
            <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>

            <div class="row">
                <div class="col-md-8" style="padding-left:3px;">
                    
                    <div class="row">
                        <div class="col-md-12"><h3><span class="semi-bold">Current</span> Month</h3></div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6 col-md-4">
							<div class="panel panel-body panel-body-accent">
								<div class="media no-margin">
									<div class="media-left media-middle">
										<i class="icon-files-empty icon-3x text-success"></i>
									</div>

									<div class="media-body text-right">
										<h3 class="no-margin text-semibold" id="TotalCOC" runat="server">0</h3>
										<span class="text-uppercase text-size-mini text-muted">Total COC's</span>
									</div>
								</div>
							</div>
						</div>
                        <div class="col-sm-6 col-md-4">
							<div class="panel panel-body panel-body-accent">
								<div class="media no-margin">
									<div class="media-left media-middle">
										<i class=" icon-file-empty icon-3x text-complete"></i>
									</div>

									<div class="media-body text-right">
										<h3 class="no-margin text-semibold" id="ElectronicCOC" runat="server">0</h3>
										<span class="text-uppercase text-size-mini text-muted">Electronic</span>
									</div>
								</div>
							</div>
						</div>
                        <div class="col-sm-6 col-md-4">
							<div class="panel panel-body panel-body-accent">
								<div class="media no-margin">
									<div class="media-left media-middle">
										<i class=" icon-file-text icon-3x text-primary"></i>
									</div>

									<div class="media-body text-right">
										<h3 class="no-margin text-semibold" id="PaperCOC" runat="server">0</h3>
										<span class="text-uppercase text-size-mini text-muted">Paper</span>
									</div>
								</div>
							</div>
						</div>

                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div id="columnchart_values"></div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div id="columnchart_values1"></div>
                        </div>
                    </div>
                    
                </div>
                <div class="col-md-4">
                    <div class="row">
                        
                        <div class="col-md-12">
                            <div id="barchart_values2"></div>
                        </div>

                        <div class="col-md-12 hide">
                            <div id="barchart_values"></div>
                        </div>

                        <div class="col-md-12">
							<div class="widget-9 panel no-border bg-primary no-margin widget-loader-bar">
                              <div class="container-xs-height full-height">
                                <div class="row-xs-height">
                                  <div class="col-xs-height col-top">
                                    <div class="panel-heading  top-left top-right">
                                      <div class="panel-title text-black">
                                        <span class="font-montserrat fs-11 all-caps">Current Audits <i class="fa fa-chevron-right"></i>
                                                            </span>
                                      </div>
                                    </div>
                                  </div>
                                </div>
                                <div class="row-xs-height">
                                  <div class="col-xs-height col-top">
                                    <div class="p-l-20 p-t-15">
                                      <h3 class="no-margin p-b-5 text-white" id="Audits" runat="server">0</h3>
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
						</div>
                        <br /> <br />
                        <div class="col-md-12">
							<div class="widget-9 panel no-border bg-danger no-margin widget-loader-bar">
                              <div class="container-xs-height full-height">
                                <div class="row-xs-height">
                                  <div class="col-xs-height col-top">
                                    <div class="panel-heading  top-left top-right">
                                      <div class="panel-title text-black">
                                        <span class="font-montserrat fs-11 all-caps">Current Refixes <i class="fa fa-chevron-right"></i>
                                                            </span>
                                      </div>
                                    </div>
                                  </div>
                                </div>
                                <div class="row-xs-height">
                                  <div class="col-xs-height col-top">
                                    <div class="p-l-20 p-t-15">
                                      <h3 class="no-margin p-b-5 text-white" id="Refix" runat="server">0</h3>
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
						</div>
                        <br /> <br />
                        <div class="col-md-12">
							<div class="widget-9 panel no-border bg-success no-margin widget-loader-bar">
                              <div class="container-xs-height full-height">
                                <div class="row-xs-height">
                                  <div class="col-xs-height col-top">
                                    <div class="panel-heading  top-left top-right">
                                      <div class="panel-title text-black">
                                        <span class="font-montserrat fs-11 all-caps">Current Audit Ratio <i class="fa fa-chevron-right"></i>
                                                            </span>
                                      </div>
                                    </div>
                                  </div>
                                </div>
                                <div class="row-xs-height">
                                  <div class="col-xs-height col-top">
                                    <div class="p-l-20 p-t-15">
                                      <h3 class="no-margin p-b-5 text-white" id="Ratio" runat="server">0</h3>
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
						</div>

                    </div>

                </div>
            </div>

            <div class="row">
                <div class="col-md-12" style="padding-left:3px;">
                    <hr />
                </div>
            </div>

            <div class="row">
                <div class="col-md-8" style="padding-left:3px;">
                    
                    <div class="row">
                        <div class="col-md-12"><h3><span class="semi-bold">Year</span> Rolling Average</h3></div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6 col-md-4">
							<div class="panel panel-body panel-body-accent">
								<div class="media no-margin">
									<div class="media-left media-middle">
										<i class="icon-files-empty icon-3x text-success"></i>
									</div>

									<div class="media-body text-right">
										<h3 class="no-margin text-semibold" id="TotalCOCYear" runat="server">0</h3>
										<span class="text-uppercase text-size-mini text-muted">Total COC's</span>
									</div>
								</div>
							</div>
						</div>
                        <div class="col-sm-6 col-md-4">
							<div class="panel panel-body panel-body-accent">
								<div class="media no-margin">
									<div class="media-left media-middle">
										<i class=" icon-file-empty icon-3x text-complete"></i>
									</div>

									<div class="media-body text-right">
										<h3 class="no-margin text-semibold" id="ElectronicCOCYear" runat="server">0</h3>
										<span class="text-uppercase text-size-mini text-muted">Electronic</span>
									</div>
								</div>
							</div>
						</div>
                        <div class="col-sm-6 col-md-4">
							<div class="panel panel-body panel-body-accent">
								<div class="media no-margin">
									<div class="media-left media-middle">
										<i class=" icon-file-text icon-3x text-primary"></i>
									</div>

									<div class="media-body text-right">
										<h3 class="no-margin text-semibold" id="PaperCOCYear" runat="server">0</h3>
										<span class="text-uppercase text-size-mini text-muted">Paper</span>
									</div>
								</div>
							</div>
						</div>

                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div id="columnchart_values2"></div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div id="columnchart_values3"></div>
                        </div>
                    </div>

                </div>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-md-12">
                            <div id="barchart_values1"></div>
                        </div>

                        <div class="col-md-12">
							<div class="widget-9 panel no-border bg-primary no-margin widget-loader-bar">
                              <div class="container-xs-height full-height">
                                <div class="row-xs-height">
                                  <div class="col-xs-height col-top">
                                    <div class="panel-heading  top-left top-right">
                                      <div class="panel-title text-black">
                                        <span class="font-montserrat fs-11 all-caps">Current Audits <i class="fa fa-chevron-right"></i>
                                                            </span>
                                      </div>
                                    </div>
                                  </div>
                                </div>
                                <div class="row-xs-height">
                                  <div class="col-xs-height col-top">
                                    <div class="p-l-20 p-t-15">
                                      <h3 class="no-margin p-b-5 text-white" id="AuditsYear" runat="server">0</h3>
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
						</div>
                        <br /> <br />
                        <div class="col-md-12">
							<div class="widget-9 panel no-border bg-danger no-margin widget-loader-bar">
                              <div class="container-xs-height full-height">
                                <div class="row-xs-height">
                                  <div class="col-xs-height col-top">
                                    <div class="panel-heading  top-left top-right">
                                      <div class="panel-title text-black">
                                        <span class="font-montserrat fs-11 all-caps">Current Refixes <i class="fa fa-chevron-right"></i>
                                                            </span>
                                      </div>
                                    </div>
                                  </div>
                                </div>
                                <div class="row-xs-height">
                                  <div class="col-xs-height col-top">
                                    <div class="p-l-20 p-t-15">
                                      <h3 class="no-margin p-b-5 text-white" id="RefixYear" runat="server">0</h3>
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
						</div>
                        <br /> <br />
                        <div class="col-md-12">
							<div class="widget-9 panel no-border bg-success no-margin widget-loader-bar">
                              <div class="container-xs-height full-height">
                                <div class="row-xs-height">
                                  <div class="col-xs-height col-top">
                                    <div class="panel-heading  top-left top-right">
                                      <div class="panel-title text-black">
                                        <span class="font-montserrat fs-11 all-caps">Current Audit Ratio <i class="fa fa-chevron-right"></i>
                                                            </span>
                                      </div>
                                    </div>
                                  </div>
                                </div>
                                <div class="row-xs-height">
                                  <div class="col-xs-height col-top">
                                    <div class="p-l-20 p-t-15">
                                      <h3 class="no-margin p-b-5 text-white" id="RatioYear" runat="server">0</h3>
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
						</div>

                    </div>

                </div>

                <div class="col-md-12">
                    <div id="columnchart_values4"></div>
                </div>

            </div>
       
        </div>
    </div>
    <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">

      google.charts.load("current", {packages:["corechart"]});
      google.charts.setOnLoadCallback(drawChart);
      google.charts.setOnLoadCallback(drawChart1);

      function drawChart() {

            var data = google.visualization.arrayToDataTable([
                <%=COCDailySales%>
            ]);

            var data1 = google.visualization.arrayToDataTable([
                <%=COCDailyLogged%>
            ]);

            var data2 = google.visualization.arrayToDataTable([
                <%=LicensedPlumbers%>
            ]);

          var data3 = google.visualization.arrayToDataTable([
                <%=COCDailySalesMonth%>
          ]);

          var data4 = google.visualization.arrayToDataTable([
                <%=InstallTypes%>
            ]);

            var view = new google.visualization.DataView(data);
            view.setColumns([0, 1,
                                {
                                    calc: "stringify",
                                    sourceColumn: 1,
                                    type: "string",
                                    role: "annotation"
                                },
                                2]);

            var view1 = new google.visualization.DataView(data1);
            view1.setColumns([0, 1,
                                {
                                    calc: "stringify",
                                    sourceColumn: 1,
                                    type: "string",
                                    role: "annotation"
                                },
                                2]);

            var view2 = new google.visualization.DataView(data2);
            view2.setColumns([0, 1,
                                {
                                    calc: "stringify",
                                    sourceColumn: 1,
                                    type: "string",
                                    role: "annotation"
                                },
                                2]);

            var view3 = new google.visualization.DataView(data3);
            view3.setColumns([0, 1,
                                {
                                    calc: "stringify",
                                    sourceColumn: 1,
                                    type: "string",
                                    role: "annotation"
                                },
                                2]);

            var view4 = new google.visualization.DataView(data4);
            view4.setColumns([0, 1,
                                {
                                    calc: "stringify",
                                    sourceColumn: 1,
                                    type: "string",
                                    role: "annotation"
                                },
                                2]);

            var options = {
                title: "COC Sales Daily",
                width: "100%",
                height: 300,
                bar: { groupWidth: "90%" },
                legend: { position: "none" }
            };

            var options1 = {
                title: "COC's Logged Daily",
                width: "100%",
                height: 300,
                bar: { groupWidth: "90%" },
                legend: { position: "none" }
            };

            var options2 = {
                title: "Licensed Active Plumbers",
                width: "100%",
                height: 300,
                bar: { groupWidth: "90%" },
                legend: { position: "none" }
            };

            var options3 = {
                title: "COC Sales",
                width: "100%",
                height: 300,
                bar: { groupWidth: "90%" },
                legend: { position: "none" }
            };

            var options4 = {
                title: "Type of Installations",
                width: "100%",
                height: 300,
                bar: { groupWidth: "90%" },
                legend: { position: "none" }
            };
          
            var chart = new google.visualization.ColumnChart(document.getElementById("columnchart_values"));
            chart.draw(view, options);

            var chart1 = new google.visualization.ColumnChart(document.getElementById("columnchart_values1"));
            chart1.draw(view1, options1);

            var chart2 = new google.visualization.ColumnChart(document.getElementById("columnchart_values2"));
            chart2.draw(view2, options2);

            var chart3 = new google.visualization.ColumnChart(document.getElementById("columnchart_values3"));
            chart3.draw(view3, options3);

            var chart4 = new google.visualization.ColumnChart(document.getElementById("columnchart_values4"));
            chart4.draw(view4, options4);

      }


      function drawChart1() {

          var data = google.visualization.arrayToDataTable([
            <%=COCSupplierStock%>
          ]);

          var data1 = google.visualization.arrayToDataTable([
            <%=COCStatusYear%>
          ]);

          var data2 = google.visualization.arrayToDataTable([
            <%=COCStatus%>
          ]);
          
          var view = new google.visualization.DataView(data);
          view.setColumns([0, 1,
                           {
                               calc: "stringify",
                               sourceColumn: 1,
                               type: "string",
                               role: "annotation"
                           },
                           2]);

          var view1 = new google.visualization.DataView(data1);
          view1.setColumns([0, 1,
                           {
                               calc: "stringify",
                               sourceColumn: 1,
                               type: "string",
                               role: "annotation"
                           },
                           2]);

          var view2 = new google.visualization.DataView(data2);
          view2.setColumns([0, 1,
                           {
                               calc: "stringify",
                               sourceColumn: 1,
                               type: "string",
                               role: "annotation"
                           },
                           2]);
          
          var options = {
              title: "Reseller Stock",
              width: "100%",
              height: 1000,
              bar: { groupWidth: "95%" },
              legend: { position: "none" },
          };

          var options1 = {
              title: "COC Status",
              width: "100%",
              height: 300,
              bar: { groupWidth: "95%" },
              legend: { position: "none" },
          };

          var options2 = {
              title: "COC Status",
              width: "100%",
              height: 300,
              bar: { groupWidth: "95%" },
              legend: { position: "none" },
          };
          
          var chart = new google.visualization.BarChart(document.getElementById("barchart_values"));
          chart.draw(view, options);

          var chart1 = new google.visualization.BarChart(document.getElementById("barchart_values1"));
          chart1.draw(view1, options1);

          var chart2 = new google.visualization.BarChart(document.getElementById("barchart_values2"));
          chart2.draw(view2, options2);

          


      }

    </script>


</asp:Content>
