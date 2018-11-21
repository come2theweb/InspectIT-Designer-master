<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddSupplierCertificate.aspx.cs" Inherits="InspectIT.AddSupplierCertificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   

    <link href="assets/plugins/bootstrap-select2/select2.css" rel="stylesheet" />
    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen" />
   
    <style>
        h4 {
            font-weight: bold;
        }
        .auto-style1 {
            display: block;
            width: 100%;
            height: 35px;
            font-size: 14px;
            line-height: normal;
            color: #272727;
            border-radius: 2px;
            -webkit-box-shadow: none;
            box-shadow: none;
            -webkit-transition: background 0.2s linear 0s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: background 0.2s linear 0s;
            font-family: Arial, sans-serif;
            -webkit-appearance: none;
            outline-width: 0;
            outline-style: none;
            outline-color: invert;
            font-weight: normal;
            vertical-align: middle;
            min-height: 35px;
            -webkit-border-radius: 2px;
            -moz-border-radius: 2px;
            border: 1px solid #ccc;
            padding: 9px 12px;
            background-color: #ffffff;
            background-image: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid container-fixed-lg bg-white">
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div class="panel-title">
                    Add Certificate
                </div>
                <div class="pull-right">
                    <div class="col-xs-12">
                        <input type="text" id="search-table" class="form-control pull-right" placeholder="Search">
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">

                <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group form-group-default required">
                            <label>Supplier :</label>
                            <div class="controls">
                                <asp:DropDownList ID="SupplierID" runat="server" CssClass="form-control select2" required></asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="form-group form-group-default required">
                            <label>Certificate Start Range :</label>
                            <div class="controls">
                                <asp:DropDownList ID="StartRange" runat="server" CssClass="form-control" required></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Certificate End Range :</label>
                            <div class="controls">
                                <asp:DropDownList ID="EndRange" runat="server" CssClass="form-control" required></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Invoice Number :</label>
                            <div class="controls">
                                <asp:TextBox ID="InvoiceNumber" runat="server" TextMode="Number" placeholder="Enter the invoice number" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default hide">
                            <label>Number or Certificates to assign :</label>
                            <div class="controls">
                                <asp:TextBox ID="NoCertificates" runat="server" TextMode="Number" placeholder="Enter the number of certificates allowed to produce" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asp:Button ID="btn_add" CssClass="btn btn-primary" runat="server" Text="Add Certificates" OnClick="btn_add_Click" Style="float: right;" />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <%--<script src="assets/js/jquery.min.js"></script>--%>
     <script src="assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/lodash.min.js"></script>

    <script src="assets/plugins/bootstrap-select2/select2.js"></script>
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

        $('.select2').select2({
           // selectOnClose: true
        });

    </script>

</asp:Content>
