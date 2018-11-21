<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TickerEdit.aspx.cs" Inherits="InspectIT.TickerEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen" />
      <!-- Global stylesheets -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,300,100,500,700,900" rel="stylesheet" type="text/css" />
    <link href="assets/global_assets/css/icons/icomoon/styles.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/bootstrap_limitless.min.css" rel="stylesheet" />
    <link href="assets/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/components.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/colors.min.css" rel="stylesheet" type="text/css" />
    <!-- /global stylesheets -->


    
	<!-- Core JS files -->
	<script src="assets/js/main/jquery.min.js"></script>
	<script src="assets/js/main/bootstrap.bundle.min.js"></script>
	<script src="assets/js/plugins/loaders/blockui.min.js"></script>
	<!-- /core JS files -->

	<!-- Theme JS files -->
	<script src="assets/js/plugins/ui/moment/moment.min.js"></script>
	<script src="assets/js/plugins/pickers/daterangepicker.js"></script>
	<script src="assets/js/plugins/pickers/anytime.min.js"></script>
	<script src="assets/js/plugins/pickers/pickadate/picker.js"></script>
	<script src="assets/js/plugins/pickers/pickadate/picker.date.js"></script>
	<script src="assets/js/plugins/pickers/pickadate/picker.time.js"></script>
	<script src="assets/js/plugins/pickers/pickadate/legacy.js"></script>
	<script src="assets/js/plugins/notifications/jgrowl.min.js"></script>
    
    <%--<script type="text/javascript" src="assets/js/pages/picker_date.js"></script>--%>

	<script src="assets/js/app.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid container-fixed-lg bg-white">
    <div class="panel panel-transparent">
        <div class="panel-heading">
        <div class="panel-title">Ticker Edit
        </div>
        <div class="pull-right">
            <div class="col-xs-12">
                <input type="text" id="search-table" class="form-control pull-right" placeholder="Search" />
            </div>
        </div>
        <div class="clearfix"></div>
        </div>
        <div class="panel-body">
       <div class="panel-body">
                <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>


                <div id="plumber">

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group form-group-default">
                                <label>Notice:</label>
                                <div class="controls">
                                    <asp:TextBox ID="Notice" runat="server" CssClass="form-control"  ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Start Date:</label>
                                <div class="controls">
                                    <asp:TextBox ID="StartDate" runat="server" CssClass="form-control anytime-month-numeric"  ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>End Date:</label>
                                <div class="controls">
                                    <asp:TextBox ID="EndDate" runat="server" CssClass="form-control anytime-month-numeric"  ></asp:TextBox>
                                </div>
                            </div>
                            
                        </div>
                         
                    </div>
                    

                    <div class="row">
                        <div class="auto-style1">
                            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Add"  style="float:right;" OnClick="Button1_Click"/>
                            <asp:Button ID="btnUpdatePlumber" CssClass="btn btn-primary" runat="server" Text="Update"  style="float:right;" OnClick="btnUpdatePlumber_Click"/>
                        </div>
                    </div> 
                </div>
            </div>
        </div>
    </div>
    </div>
    <script>
        $('.anytime-month-numeric').AnyTime_picker({
            format: '%Z/%m/%d'
        });
        var responsiveHelper = undefined;
        var breakpointDefinition = {
            tablet: 1024,
            phone: 480
        };

        var table = $('#stripedTable');

        var settings = {
            "sDom": "<'table-responsive't><'row'<p i>>",
            "sPaginationType": "bootstrap",
            "destroy": true,
            "scrollCollapse": true,
            "oLanguage": {
                "sLengthMenu": "_MENU_ ",
                "sInfo": "Showing <b>_START_ to _END_</b> of _TOTAL_ entries"
            },
            "iDisplayLength": 50
        };

        table.dataTable(settings);

        // search box for table
        $('#search-table').keyup(function () {
            table.fnFilter($(this).val());
        });

        
        $('.datepicker-range').datepicker();
    </script>

</asp:Content>
