<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RatesMaintainAdd.aspx.cs" Inherits="InspectIT.RatesMaintainAdd" %>
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
        <div class="panel-title">View Users
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
       
             <div class="form-group form-group-default">
                    <label>Supply Item :</label>
                    <div class="controls">
                        <asp:DropDownList ID="supplyItemsDrop" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>

            <div class="form-group form-group-default">
                    <label>Amount :</label>
                    <div class="controls">
                        <asp:TextBox ID="amount" runat="server" placeholder="Amount" CssClass="form-control" required></asp:TextBox>
                    </div>
                </div>

            <div class="form-group form-group-default">
                    <label>Valid From Date :</label>
                    <div class="controls">
                        <asp:TextBox ID="dates" runat="server" placeholder="Amount" CssClass="form-control datepicker-range" required></asp:TextBox>
                    </div>
                </div>
             
        <!-- START Form Control-->
            <div class="row">
                <div class="auto-style1">
                    <asp:Button ID="saveRates" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="saveRates_Click" />
                    <asp:Button ID="updateRates" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="updateRates_Click" />
                </div>
            </div> 
        <!-- END Form Control-->
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

        $('.datepicker-range').datepicker();

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
