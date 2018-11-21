<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserSettings.aspx.cs" Inherits="InspectIT.UserSettings" %>
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
        <div class="panel-title">Edit User
        </div>
        <div class="pull-right">
            <div class="col-xs-12">
                <input type="text" id="search-table" class="form-control pull-right" placeholder="Search">
            </div>
        </div>
        <div class="clearfix"></div>
        </div>
        <div class="panel-body">
       
    <div class="form-group form-group-default">
        <label>Firstname :</label>
            <div class="controls">
                <asp:TextBox ID="UserFirstName" runat="server" placeholder="Enter Your First Name" CssClass="form-control" required></asp:TextBox>
            </div>
    </div>
    <div class="form-group form-group-default">
        <label>Password :</label>
            <div class="controls">
                <asp:TextBox ID="UserPassword" runat="server" placeholder="Enter A Password" CssClass="form-control" required TextMode="Password"></asp:TextBox>
            </div>
    </div>

            <div class="form-group form-group-default">
                <label>Email :</label>
                    <div class="controls">
                        <asp:TextBox ID="UserEmail" runat="server" placeholder="Enter Your Email Address" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
           
    <div class="row">
        <div class="auto-style1">
            <asp:Button ID="btn_update" CssClass="btn btn-primary" runat="server" Text="Update User" OnClick="btn_update_Click1" style="float:right;"/>
        </div>
    </div> 

        </div>
    </div>
    </div>

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
       

    </script>

</asp:Content>
