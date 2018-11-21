<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="LocationTypes.aspx.cs" Inherits="InspectIT.LocationTypes" %>
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
            
        <div class="panel-title">Location Types
        </div>
       
        <div class="clearfix"></div>
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
                 <div class="row">
                    <div class="col-xs-6"> 
                        
                    </div>
                     <div class="col-xs-6">
                        <input type="text" id="search-table" class="form-control pull-right" placeholder="Search table">
                    </div>
                </div>
                <div class="clearfix"></div>
                <table class="table table-striped demo-table-search" id="stripedTable">
                    <thead>
                    <tr>
                        <th>Type</th> 
                        <th>Points</th> 
                        <th>isActive</th> 
                        <th></th> 
                    </tr>
                    </thead>
                    <tbody  id="displayusers" runat="server">
            
                    </tbody>
                </table>
            </div>
            <div class="tab-pane" id="TabArchived">
                <div class="row">
                    <div class="col-xs-12">
                        <input type="text" id="search-table-del" class="form-control pull-right" placeholder="Search table">
                    </div>
                </div>
                <div class="clearfix"></div>
                <table class="table table-striped demo-table-search" id="stripedTable_del">
                    <thead>
                    <tr>
                        <th>Type</th> 
                        <th>Points</th> 
                        <th>isActive</th> 
                        <th></th> 
                    </tr>
                    </thead>
                    <tbody  id="displayusers_del" runat="server"></tbody>
                </table>
            </div>
                <asp:Button ID="addBtn" runat="server" CssClass="btn btn-primary" Text="Add" style="float:right;" OnClick="Button1_Click" />
        </div>

        </div>
            
            
            
        </div>
    </div>
    <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->

    

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
        var table1 = $('#stripedTable_del');

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
        table1.dataTable(settings);

        // search box for table
        $('#search-table').keyup(function () {
            table.fnFilter($(this).val());
        });

        $('#search-table-del').keyup(function () {
            table1.fnFilter($(this).val());
        });
       

    </script>

</asp:Content>
