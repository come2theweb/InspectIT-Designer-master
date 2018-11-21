<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminGenerateInvoice.aspx.cs" Inherits="InspectIT.adminGenerateInvoice" %>
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
        <div class="panel-title">COC Statements
        </div>
       
        </div>
        <div class="panel-body">

        <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
        <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>

        <div class="panel">
            <ul class="nav nav-tabs nav-tabs-simple" role="tablist">
            <li class="active"><a href="#TabActive" data-toggle="tab" role="tab">Active</a></li>
            <li class=""><a href="#TabArchived" data-toggle="tab" role="tab">Archived</a></li>
            </ul>
            <div class="tab-content">
            <div class="tab-pane active" id="TabActive">
                 <div class="pull-right">
                    <div class="col-xs-12">
                        <input type="text" id="search-table" class="form-control pull-right" placeholder="Search">
                    </div>
                </div>
                <div class="clearfix"></div>
                <table class="table table-striped demo-table-search" id="stripedTable">
                    <thead>
                    <tr>
                        
                        <th>COC Number</th>
                        <th>Type</th>
                        <th>Date Purchase</th>
                        <th>Status</th>
                        <th>Consumer</th>
                        <th>Address</th>
                        <th></th>
                    </tr>
                    </thead>
                    <tbody  id="COCStatement" runat="server"></tbody>
                </table>
            </div>
            <div class="tab-pane" id="TabArchived">
                <table class="table table-striped demo-table-search" id="stripedTable_del">
                    <thead>
                    <tr>
                        <th>COC Number</th>
                        <th>Type</th>
                        <th>Date Purchase</th>
                        <th>Status</th>
                        <th>Consumer</th>
                        <th>Address</th>
                        <th></th>
                    </tr>
                    </thead>
                    <tbody  id="COCStatement_del" runat="server"></tbody>
                </table>
            </div>
            
        </div>

        </div>
       
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
            "iDisplayLength": 50
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

    </script>

</asp:Content>
