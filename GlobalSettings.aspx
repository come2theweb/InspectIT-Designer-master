<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GlobalSettings.aspx.cs" Inherits="InspectIT.GlobalSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/lodash.min.js"></script>

    <style>
        .form-control{
            width:200px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- START CONTAINER FLUID -->
    <div class="container-fluid container-fixed-lg bg-white">
    <!-- START PANEL -->
    <div class="panel panel-transparent">
        <div class="panel-heading">
        <div class="panel-title">Global Settings
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
        <table class="table table-striped demo-table-search" id="stripedTable">
            <thead>
            <tr>
                <th>Notice</th>
                <th>Start Date</th>
                <th>End Date</th>
                <th></th>
            </tr>
            </thead>
            <tbody  id="displayrates" runat="server">
                
            </tbody>
        </table>
            <a href="TickerEdit.aspx"><label class="btn btn-primary" style="float:right;">Add</label></a>
        </div>
            <hr />
            <div class="row" style="margin-top:30px;">
                 

                <p style="display:flex;">Award points only if <asp:TextBox ID="one" runat="server" CssClass="form-control"></asp:TextBox> nomination likes are received per post</p>
                
                <p style="display:flex;">Top <asp:TextBox ID="two" runat="server" CssClass="form-control"></asp:TextBox> nomination likes for the day, award <asp:TextBox ID="three" runat="server" CssClass="form-control"></asp:TextBox> bonus points</p>
                
                <p style="display:flex;"><asp:TextBox ID="four" runat="server" CssClass="form-control"></asp:TextBox> performance points, award <asp:TextBox ID="five" runat="server" CssClass="form-control"></asp:TextBox> bonus points</p>

                <p style="display:flex;">Top <asp:TextBox ID="six" runat="server" CssClass="form-control"></asp:TextBox> plumbers overall, for past 30 days, award <asp:TextBox ID="seven" runat="server" CssClass="form-control"></asp:TextBox> points</p>

                <p style="display:flex;">Most COC issued per month award <asp:TextBox ID="eight" runat="server" CssClass="form-control"></asp:TextBox> points</p>

                <p style="display:flex;">If <asp:TextBox ID="nine" runat="server" CssClass="form-control"></asp:TextBox> CPD aquired per month award <asp:TextBox ID="ten" runat="server" CssClass="form-control"></asp:TextBox> points</p>

                <p style="display:flex;">If <asp:TextBox ID="eleven" runat="server" CssClass="form-control"></asp:TextBox> SARS Certificate <asp:TextBox ID="twelve" runat="server" CssClass="form-control"></asp:TextBox> points</p>

                <p style="display:flex;">Bonus points per the <asp:TextBox ID="thirteen" runat="server" CssClass="form-control"></asp:TextBox> of licensed/master/Director plumbers employed <asp:TextBox ID="fourteen" runat="server" CssClass="form-control"></asp:TextBox> points</p>

                <p style="display:flex;">Opening your account every day, award <asp:TextBox ID="fifteen" runat="server" CssClass="form-control"></asp:TextBox> points</p>
            </div>
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Save Global Settings" OnClick="Button1_Click" style="float:right;" />
        </div>
    </div>
    <!-- END PANEL -->
    </div>

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
