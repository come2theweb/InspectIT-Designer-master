<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="InspectIT.test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/lodash.min.js"></script>
    <style type="text/css">
        .auto-style1 {
            left: 1px;
            top: -268px;
        }
        .auto-style2 {
            position: absolute;
            top: 248px;
            left: 93px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- START CONTAINER FLUID -->
    <div class="container-fluid container-fixed-lg bg-white">
    <!-- START PANEL -->
    <div class="panel panel-transparent">
        <div class="panel-heading">
        <div class="panel-title">COC Statement
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

            <div class="col-md-6">
            

            <!-- START Form Control-->
            <div class="form-group form-group-default">
              <label>Company Name</label>
              <div class="controls">
                    <asp:TextBox ID="companyName" runat="server" placeholder="Enter the Company Name" CssClass="form-control" required></asp:TextBox>
              </div>
            </div>
            <!-- END Form Control-->
            
            <!-- START Form Control-->
            <div class="form-group form-group-default">
              <label>Company Address</label>
              <div class="controls">
                    <asp:TextBox ID="companyAddress" runat="server" placeholder="Enter the Company Address" CssClass="form-control" required></asp:TextBox>
              </div>
            </div>
            <!-- END Form Control-->

            <!-- START Form Control-->
            <div class="form-group form-group-default">
              <label>Company Contact Number</label>
              <div class="controls">
                    <asp:TextBox ID="companyContact" runat="server" placeholder="Enter the Company Contact Number" CssClass="form-control" required></asp:TextBox>
              </div>
            </div>
            <!-- END Form Control-->

            <!-- START Form Control-->
            <div class="form-group form-group-default" style="left: -1px; top: 5px">
              <label>Company Product</label>
              <div class="controls">
                    <asp:TextBox ID="companyProduct" runat="server" placeholder="Enter the Company Product" CssClass="form-control" required></asp:TextBox>
              </div>
            </div>
            <!-- END Form Control-->


            
            <!-- START Form Control-->
            <div class="row">
              <div class="auto-style1">
                  <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Add" OnClick="Button1_Click" />
<%--                  <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="View" OnClick="Button2_Click" formnovalidate />--%>
                </div>
            </div> 
            <!-- END Form Control-->
            <!-- START Form Control-->
            <div class="row">
              <div class="col-md-12 no-padding">
                <div id="dispError" runat="server" class="alert alert-danger" visible="false"></div>
                <div id="dispSuccess" runat="server" class="alert alert-success" visible="false"></div>
              </div>
            </div>
            <!-- END Form Control-->
            <br />
           
            <br />
             <div class="row">
              <div class="col-sm-12">
                  
              </div>
            </div>
        </div>

        <div class="col-md-6">
            <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>--%>

            <div id="displayData" runat="server"></div>

            <table class="table table-hover">
                <tbody>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Address</th>
                            <th>Contact</th>
                            <th>Product</th>
                        </tr>
                    </thead>
                        
                </tbody>
            </table>
            
            
        </div>

        </div>


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
