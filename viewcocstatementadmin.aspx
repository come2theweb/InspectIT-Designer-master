<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewCOCStatementAdmin.aspx.cs" Inherits="InspectIT.ViewCOCStatementAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/lodash.min.js"></script>
    <%-- <link href="assets/css/core.css" rel="stylesheet" />--%>
   <link href="assets/css/components.css" rel="stylesheet" />
<style>

    .nav-tabs ~ .tab-content {
        overflow-x: scroll;
        padding: 15px;
    }

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- START CONTAINER FLUID -->
    <div class="container-fluid container-fixed-lg bg-white">
    <!-- START PANEL -->
    <div class="panel panel-transparent">
        <div class="panel-heading">
        <div class="panel-title">COC Log Statements
        </div>
       
        </div>
        <div class="panel-body">

        <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
        <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>

        <div class="panel">
          
                 <div class="row">
                     <div class="col-xs-4">
                         <table border="0" style="width:100%;">
                             <tr>
                                 <td style="padding-right:3px;"><asp:TextBox ID="COCNumber" CssClass="form-control" runat="server" placeholder="COC Number"></asp:TextBox></td>
                                 <td><asp:Button ID="Search" runat="server" CssClass="btn btn-success" Text="Search" OnClick="Search_Click" /></td>
                             </tr>
                         </table>
                     </div>
                     <div class="col-xs-6"> 
                        <h5 class="card-title" style="margin-top:0px;padding-top:0px;">
                            <small>
                            <label>Filter Date</label>
                            <button type="button" class="btn btn-default daterange-predefined">
						        <i class="icon-calendar22 position-left"></i>
						        <span></span>
						        <b class="caret"></b>
					        </button>
                            <asp:Button ID="filtDates" runat="server" CssClass="btn btn-primary" Text="Filter" OnClick="filtDates_Click" />
                             </small>
                        </h5>
                        <div class="form-group row" style="display:none;">
                            <asp:TextBox ID="startDateSend" runat="server"></asp:TextBox>
                            <asp:TextBox ID="endDateSend" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <input type="text" id="search-table" class="form-control pull-right" placeholder="Search table">
                    </div>
                </div>
                <div class="clearfix"></div>
                <table class="table table-striped demo-table-search" id="stripedTable">
                    <thead>
                    <tr>
                        <th>COC Number</th>
                        <th>Date Purchase</th>
                        <th>Date Logged</th>
                        <th>Plumber</th>
                        <th>Plumber Mobile</th>
                        <th>City</th>
                        <th>Audit vs Log</th> <%-- the number coc's the plumbers has log divided by the number of audits that have been done on the plumber--%>
                        <th style="width:320px;"></th>
                    </tr>
                    </thead>
                    <tbody  id="COCStatement" runat="server"></tbody>
                </table>
          

        </div>
       
        </div>
    </div>
    <!-- END PANEL -->
    </div>
    
  <!--   <script src="assets/vendor/jquery.min.js"></script>END CONTAINER FLUID -->
    <%--<script type="text/javascript" src="assets/js/pickers/daterangepicker.js"></script>
	<script type="text/javascript" src="assets/js/pickers/anytime.min.js"></script>
	<script type="text/javascript" src="assets/js/pickers/pickadate/picker.js"></script>
	<script type="text/javascript" src="assets/js/pickers/pickadate/picker.date.js"></script>
	<script type="text/javascript" src="assets/js/pickers/pickadate/picker.time.js"></script>
	<script type="text/javascript" src="assets/js/pickers/pickadate/legacy.js"></script>--%>

    <script src="assets/js/moment.min.js"></script>
    <script type="text/javascript" src="assets/js/pickers/daterangepicker.js"></script>
    <script type="text/javascript" src="assets/js/pickers/anytime.min.js"></script>
    <script type="text/javascript" src="assets/js/pickers/pickadate/picker.js"></script>
    <script type="text/javascript" src="assets/js/pickers/pickadate/picker.date.js"></script>
    <script type="text/javascript" src="assets/js/pickers/pickadate/picker.time.js"></script>
    <script type="text/javascript" src="assets/js/pickers/pickadate/legacy.js"></script>
    
    <script type="text/javascript" src="assets/js/fullcalendar/fullcalendar.min.js"></script>
    <script>
        function deleteconf(url) {
            var result = confirm("Are you sure?");
            if (result) {
                document.location.href = url;
            }
        }

        var responsiveHelper = undefined;
        var breakpointDefinition = {
            tablet: 1024,
            phone: 480
        };

        var table = $('#stripedTable');
        var table_del = $('#stripedTable_del');

        var settings = {
            "sDom": "<'table-responsive't><'row'<p i>>",
            "sPaginationType": "bootstrap",
            "destroy": true,
            "scrollCollapse": true,
            "oLanguage": {
                "sLengthMenu": "_MENU_ ",
                "sInfo": "Showing <b>_START_ to _END_</b> of _TOTAL_ entries"
            },
            "iDisplayLength": 50,
            "order": [[0, "desc"]]
        };

        table.dataTable(settings);
        table_del.dataTable(settings);

        // search box for table
        $('#search-table').keyup(function () {
            table.fnFilter($(this).val());
        });
       
        function deleteconf(url) {
            var result = confirm("Want to archive?");
            if (result) {
                document.location.href = url;
            }
        }

       <%-- $('.daterange-predefined').daterangepicker(
            {
                startDate: moment().subtract('days', 29),
                endDate: moment(),
                minDate: '01/01/2017',
                maxDate: '12/31/2020',
                dateLimit: { days: 60 },
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract('days', 1), moment().subtract('days', 1)],
                    'Last 7 Days': [moment().subtract('days', 6), moment()],
                    'Last 30 Days': [moment().subtract('days', 29), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract('month', 1).startOf('month'), moment().subtract('month', 1).endOf('month')]
                },
                opens: 'left',
                applyClass: 'btn-small bg-slate',
                cancelClass: 'btn-small btn-default'
            }
            ,
            function (start, end) {
                $('.daterange-predefined span').html(start.format('MMMM D, YYYY') + ' &nbsp; - &nbsp; ' + end.format('MMMM D, YYYY'));
                $("#<%=startDateSend.ClientID%>").val($('.daterange-predefined').data('daterangepicker').startDate.format('MMMM D, YYYY'));
                $("#<%=endDateSend.ClientID%>").val($('.daterange-predefined').data('daterangepicker').endDate.format('MMMM D, YYYY'));
                //getSystemLeaderBoard("lbCardDisp", ProgID, $('.daterange-predefined').data('daterangepicker').startDate.format('MMMM D, YYYY'), $('.daterange-predefined').data('daterangepicker').endDate.format('MMMM D, YYYY'), $("#sysDrivers").val(), $("#sysGroups").val(), $("#sysUsers").val());
            }
        );

        // Display date format
        $('.daterange-predefined span').html(moment().subtract('days', 29).format('MMMM D, YYYY') + ' &nbsp; - &nbsp; ' + moment().format('MMMM D, YYYY'));

        $("#<%=startDateSend.ClientID%>").val($('.daterange-predefined').data('daterangepicker').startDate.format('MMMM D, YYYY'));
        $("#<%=endDateSend.ClientID%>").val($('.daterange-predefined').data('daterangepicker').endDate.format('MMMM D, YYYY'));
--%>


       // Initialize with options
        $('.daterange-predefined').daterangepicker(
            {
                startDate: moment().subtract('days', 29),
                endDate: moment(),
                minDate: '01/01/2009',
                maxDate: '12/31/2020',
                dateLimit: { days: 60 },
                ranges: {
                    'Today': [moment(), moment()],
                    // 'Yesterday': [moment().subtract('days', 1), moment().subtract('days', 1)],
                    //'Last 7 Days': [moment().subtract('days', 6), moment()],
                    'Last 30 Days': [moment().subtract('days', 29), moment()],
                    //'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract('month', 1).startOf('month'), moment().subtract('month', 1).endOf('month')]
                },
                opens: 'left',
                applyClass: 'btn-small bg-slate',
                cancelClass: 'btn-small btn-default'
            }
            ,
            function (start, end) {
                $('.daterange-predefined span').html(start.format('MMMM D, YYYY') + ' &nbsp; - &nbsp; ' + end.format('MMMM D, YYYY'));
                $("#<%=startDateSend.ClientID%>").val($('.daterange-predefined').data('daterangepicker').startDate.format('MMMM D, YYYY'));
                $("#<%=endDateSend.ClientID%>").val($('.daterange-predefined').data('daterangepicker').endDate.format('MMMM D, YYYY'));

                console.log($("#<%=startDateSend.ClientID%>").val());
                console.log($("#<%=endDateSend.ClientID%>").val());
                //getSystemLeaderBoard("lbCardDisp", ProgID, $('.daterange-predefined').data('daterangepicker').startDate.format('MMMM D, YYYY'), $('.daterange-predefined').data('daterangepicker').endDate.format('MMMM D, YYYY'), $("#sysDrivers").val(), $("#sysGroups").val(), $("#sysUsers").val());
            }
        );

        // Display date format
        $('.daterange-predefined span').html(moment().subtract('days', 29).format('MMMM D, YYYY') + ' &nbsp; - &nbsp; ' + moment().format('MMMM D, YYYY'));

        $("#<%=startDateSend.ClientID%>").val($('.daterange-predefined').data('daterangepicker').startDate.format('MMMM D, YYYY'));
        $("#<%=endDateSend.ClientID%>").val($('.daterange-predefined').data('daterangepicker').endDate.format('MMMM D, YYYY'));

    </script>

</asp:Content>
