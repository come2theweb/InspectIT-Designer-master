<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DesignationSpecialisationPointsEdit.aspx.cs" Inherits="InspectIT.DesignationSpecialisationPointsEdit" %>
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
        <div class="panel-title">Edit Weighting
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
                                <label>Name:</label>
                                <div class="controls">
                                    <asp:TextBox ID="Item" runat="server" CssClass="form-control"  ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Points:</label>
                                <div class="controls">
                                    <asp:TextBox ID="Points" runat="server" CssClass="form-control"  ></asp:TextBox>
                                </div>
                            </div>
                            
                        </div>
                         
                    </div>
                    

                    <div class="row">
                        <div class="auto-style1">
                            <asp:Button ID="btnUpdatePlumber" CssClass="btn btn-primary" runat="server" Text="Update"  style="float:right;" OnClick="btnUpdatePlumber_Click"/>
                        </div>
                    </div> 
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

        
        $('.datepicker-range').datepicker();
    </script>

</asp:Content>
