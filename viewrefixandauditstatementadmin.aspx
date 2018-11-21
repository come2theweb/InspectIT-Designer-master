<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewRefixandAuditStatementAdmin.aspx.cs" Inherits="InspectIT.ViewRefixandAuditStatementAdmin" %>

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
                <div class="panel-title">
                    Manage Audits/Refixes       
                </div>
                <div class="pull-right">
                    <div class="col-xs-12">
                        <input type="text" id="search-table" class="form-control pull-right" placeholder="Search">
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                <%--<ul class="nav nav-tabs nav-tabs-simple" role="tablist">
                    <li class="active"><a href="#TabActive" data-toggle="tab" role="tab">Active</a></li>
                    <li class=""><a href="#TabArchived" data-toggle="tab" role="tab">Archived</a></li>
                </ul>--%>
                 <table class="table table-striped demo-table-search" id="stripedTable">
                            <thead>
                                <tr>
                                    <th>COC Number</th>
                                    <th>Status</th>
                                    <th>Installation Type</th>
                                    <th>Assigned Date</th>
                                    <th>Auditted Date</th>
                                    <th>Auditors Name</th>
                                    <th>Refix Date</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody id="COCStatement" runat="server"></tbody>
                        </table>
               <%-- <div class="tab-content">
                    <div class="tab-pane active" id="TabActive">
                       
                    </div>
                    <div class="tab-pane" id="TabArchived">
                        <table class="table table-striped demo-table-search" id="stripedTablea">
                            <thead>
                                <tr>
                                    <th>COC Number</th>
                                    <th>Status</th>
                                    <th>Installation Type</th>
                                    <th>Assigned Date</th>
                                    <th>Auditted Date</th>
                                    <th>Auditors Name</th>
                                    <th>Refix Date</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody id="COCStatementarchived" runat="server"></tbody>
                        </table>
                    </div>
                </div>--%>


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
        var tablea = $('#stripedTablea');

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
        tablea.dataTable(settings);

        // search box for table
        $('#search-table').keyup(function () {
            table.fnFilter($(this).val());
        });

        $('#search-table').keyup(function () {
            tablea.fnFilter($(this).val());
        });


    </script>

</asp:Content>
