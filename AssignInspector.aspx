<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AssignInspector.aspx.cs" Inherits="InspectIT.AssignInspector" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/lodash.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- START CONTAINER FLUID -->
    <div class="container-fluid container-fixed-lg bg-white">
    <!-- START PANEL -->
    <div class="panel panel-transparent">
        <div class="panel-heading">
        <div class="panel-title">Assign Auditor
        </div>
        <div class="clearfix"></div>
        </div>
        <div class="panel-body">
       
            <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
            <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>

            <h3>Assigned Auditor</h3>
            <table class="table table-striped demo-table-search" id="stripedTableAssigned">
                <thead>
                    <tr>
                        <th>Name &amp; Surname</th>
                        <th>Suburb</th>
                        <th>City</th>
                        <th>Province</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody id="AssignedInspector" runat="server"></tbody>
            </table>

            <h3>Avaliable Auditors</h3>
            <div class="pull-right">
                <div class="col-xs-12">
                    <input type="text" id="search-table" class="form-control pull-right" placeholder="Search Auditors">
                </div>
            </div>
            <table class="table table-striped demo-table-search" id="stripedTable">
                <thead>
                    <tr>
                        <th>Name &amp; Surname</th>
                        <th>Suburb</th>
                        <th>City</th>
                        <th>Province</th>
                        <th>Audits</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody id="displayauditors" runat="server"></tbody>
            </table>

        </div>
    </div>
    <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->
    
    <script>

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

        function deleteconf(url) {
            var result = confirm("Want to archive?");
            if (result) {
                document.location.href = url;
            }
        }
    </script>
</asp:Content>
