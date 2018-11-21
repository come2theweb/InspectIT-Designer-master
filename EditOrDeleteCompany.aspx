<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditOrDeleteCompany.aspx.cs" Inherits="InspectIT.EditOrDeleteCompany" %>
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

    <div class="container-fluid container-fixed-lg bg-white">
    <div class="panel panel-transparent">
        <div class="panel-heading">
        <div class="panel-title">Edit Or Delete Companies
        </div>
        <div class="pull-right">
            <div class="col-xs-12">
                <input type="text" id="search-table" class="form-control pull-right" placeholder="Search">
            </div>
        </div>
        <div class="clearfix"></div>
        </div>
        <div class="panel-body">
      
     <div class="row">
           <h4>Company Details</h4>
           <div class="col-md-6">
                    <div class="form-group form-group-default">
                        <label>Company Name :</label>
                            <div class="controls">
                                <asp:TextBox ID="CompanyName" runat="server" placeholder="Enter Company Name" CssClass="form-control" required></asp:TextBox>
                            </div>
                    </div>
                    <div class="form-group form-group-default required">
                        <label>Company Reg No :</label>
                            <div class="controls">
                                <asp:TextBox ID="CompanyRegNo" runat="server" placeholder="Enter Your Company Registration Number" CssClass="form-control" required></asp:TextBox>
                            </div>
                    </div>
                    <div class="form-group form-group-default required">
                        <label>Website :</label>
                            <div class="controls">
                                <asp:TextBox ID="CompanyWebsite" runat="server" placeholder="Enter Company Website" CssClass="form-control" required></asp:TextBox>
                            </div>
                    </div>
                    <div class="form-group form-group-default">
                        <label>Company Email :</label>
                            <div class="controls">
                                <asp:TextBox ID="CompanyEmail" runat="server" placeholder="Enter Company Email Address" CssClass="form-control" required></asp:TextBox>
                            </div>
                    </div>
                    <div class="form-group form-group-default">
                        <label>Company Contact Number :</label>
                            <div class="controls">
                                <asp:TextBox ID="CompanyContactNo" runat="server" placeholder="Enter Company Contact Number" CssClass="form-control" required></asp:TextBox>
                            </div>
                    </div>
            </div>
       </div>
<div class="row">
    <h4>Address</h4>

    <div class="col-md-6">
    <h5>Physical</h5>

            <div class="form-group form-group-default">
                <label>Address Line 1 :</label>
                    <div class="controls">
                        <asp:TextBox ID="AddressLine1" runat="server" placeholder="Enter Company Address" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
            <div class="form-group form-group-default">
                <label>Address Line 2 :</label>
                    <div class="controls">
                        <asp:TextBox ID="AddressLine2" runat="server" placeholder="Enter Company Address" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
            <div class="form-group form-group-default">
                <label>Province :</label>
                    <div class="controls">
                        <asp:TextBox ID="Province" runat="server" placeholder="Enter Company Province" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
            <div class="form-group form-group-default">
                <label>City/Suburb :</label>
                    <div class="controls">
                        <asp:TextBox ID="CitySuburb" runat="server" placeholder="Enter Company City / Suburb" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
            <div class="form-group form-group-default">
                <label>Area Code :</label>
                    <div class="controls">
                        <asp:TextBox ID="AreaCode" runat="server" placeholder="Enter Company Area Code" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
    </div>

<div class="col-md-6">
      <h5>Postal</h5>
            <div class="form-group form-group-default">
                <label>Postal Address :</label>
                    <div class="controls">
                        <asp:TextBox ID="PostalAddress" runat="server" placeholder="Enter Company Postal Address" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
            <div class="form-group form-group-default">
                <label>City :</label>
                    <div class="controls">
                        <asp:TextBox ID="PostalCity" runat="server" placeholder="Enter Company City" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
            <div class="form-group form-group-default">
                <label>Postal Code :</label>
                    <div class="controls">
                        <asp:TextBox ID="PostalCode" runat="server" placeholder="Enter Company Postal Code" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
</div>
</div>
    <div class="row">
        <div class="auto-style1">
            <asp:Button ID="btn_update" CssClass="btn btn-primary" runat="server" Text="Update Company" style="float:right;" OnClick="btn_update_Click"/>
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
