<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewReporting.aspx.cs" Inherits="InspectIT.ViewReporting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.datatables.net/buttons/1.5.2/css/buttons.dataTables.min.css" rel="stylesheet" type="text/css" />
    <link href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
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
            <div class="panel-title">Report</div>
        
        </div>
        <div class="panel-body">

        <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
        <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>

           
            <h4 class="panel-title">Dataset</h4>
                    <div class="form-group row">
                        <div class="col-sm-12 col-md-9">
                            <asp:DropDownList ID="SelDataset" runat="server" CssClass="form-control" Width="100%" OnSelectedIndexChanged="SelDataset_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>Select a dataset</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-sm-12 col-md-3">
                            <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="btn btn-success" OnClick="btnFilter_Click" />
                        </div>
                    </div>

            <h4 class="panel-title" id="filterheading" runat="server">Filters </h4>
                    <div class="form-group row" runat="server" id="stuff">
                        <div class="col-md-6">
                            <asp:DropDownList ID="selStat" runat="server" CssClass="form-control">
                                <asp:ListItem>Select Status</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                       
                    </div>

             <h4 class="panel-title" id="selFieldsView" runat="server">Select Fields you would like to view</h4>

                    <div class="form-group row" runat="server" id="chkbxsDisp" style="padding-right:10px;margin-left:10px;">
                        <asp:CheckBoxList ID="filterColumnsDisp" RepeatDirection="Horizontal" RepeatColumns="3" RepeatLayout="Table" runat="server"></asp:CheckBoxList>

                        <div class="col-sm-12 col-md-12">
                            <asp:Button ID="filterCheckbxs" Style="float: right;" runat="server" Text="Select Columns" CssClass="btn btn-success " OnClick="filterCheckbxs_Click" />
                        </div>
                    </div>

             <div class="table-responsive">
                    <table class="table datatable-basic">
                        <thead id="tblHead" runat="server"></thead>
                        <tbody id="tblDisplay" runat="server"></tbody>
                    </table>
                </div>


        </div>
    </div>
    <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->
    <script src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js"></script>
        <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
        <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
        <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
        <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script>


        $('.datatable-basic').DataTable({
            dom: 'Bfrtip',
            buttons: [
                'excelHtml5',
                'csvHtml5',
                'pdf',
                'copy'
            ]
        });


    </script>
</asp:Content>
