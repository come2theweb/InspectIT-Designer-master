<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddCustomer.aspx.cs" Inherits="InspectIT.AddCustomer" %>
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
        <div class="panel-title">Add Customers
        </div>
        <div class="pull-right">
            <div class="col-xs-12">
                <input type="text" id="search-table" class="form-control pull-right" placeholder="Search">
            </div>
        </div>
        <div class="clearfix"></div>
        </div>
        <div class="panel-body">
        <%--<table class="table table-striped demo-table-search" id="stripedTable">
            <thead>
            <tr>
                <th>COC Number</th>
                <th>Plumber</th>
                <th>Date</th>
                <th>Audit</th>
                <th>Refix</th>
            </tr>
            </thead>
            <tbody>
            <!--<tr>
                <td class="v-align-middle semi-bold">
                <p>First Tour</p>
                </td>
                <td class="v-align-middle"><a href="#" class="btn btn-tag">United States</a><a href="#" class="btn btn-tag">India</a> <a href="#" class="btn btn-tag">China</a><a href="#" class="btn btn-tag">Africa</a>
                </td>
                <td class="v-align-middle">
                <p>it is more then ONE nation/nationality as its fall name is The United Kingdom of Great Britain and North Ireland..</p>
                </td>
                <td class="v-align-middle">
                <p>Public</td>
                <td class="v-align-middle">
                <p>April 13,2014 10:13</td>
            </tr>-->
            </tbody>
        </table>--%>

<!-- BGINNING OF FORM TO ADD A NEW USER -->

<!-- //REQUIRED: ADD COMPANY DROP DOWN AND INDIVIDUAL -->

<!-- START Form Control-->
    <div class="form-group form-group-default">
        <label>Firstname</label>
            <div class="controls">
                <asp:TextBox ID="CustomerName" runat="server" placeholder="Enter Your First Name" CssClass="form-control" required></asp:TextBox>
            </div>
    </div>
<!-- END Form Control-->

<!-- START Form Control-->
    <div class="form-group form-group-default">
        <label>Lastame</label>
            <div class="controls">
                <asp:TextBox ID="CustomerSurname" runat="server" placeholder="Enter Your Last Name" CssClass="form-control" required></asp:TextBox>
            </div>
    </div>
<!-- END Form Control-->

<!-- START Form Control-->
    <div class="form-group form-group-default">
        <label>Contact Number</label>
            <div class="controls">
                <asp:TextBox ID="CustomerCellNo" runat="server" placeholder="Enter Your Contact Number" CssClass="form-control phone" required></asp:TextBox>
            </div>
    </div>
<!-- END Form Control-->

<!-- START Form Control-->
    <div class="form-group form-group-default">
        <label>Email</label>
            <div class="controls">
                <asp:TextBox ID="CustomerEmail" runat="server" placeholder="Enter Your Email Address" TextMode="Email" CssClass="form-control" required></asp:TextBox>
            </div>
    </div>
<!-- END Form Control-->

<!-- START Form Control-->
    <div class="form-group form-group-default">
        <label>Password</label>
            <div class="controls">
                <asp:TextBox ID="CustomerPassword" runat="server" placeholder="Enter Your Password" CssClass="form-control" required TextMode="Password"></asp:TextBox>
            </div>
    </div>
<!-- END Form Control-->

<!-- START Form Control-->
    <div class="row">
        <div class="auto-style1">
            <asp:Button ID="btn_add" CssClass="btn btn-primary" runat="server" Text="Add New User" OnClick="btn_add_Click"/>
        </div>
    </div> 
<!-- END Form Control-->

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
       

    </script>

</asp:Content>
