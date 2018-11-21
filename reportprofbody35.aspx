<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="reportprofbody35.aspx.cs" Inherits="InspectIT.reportprofbody35" %>
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
        <div class="panel-title">Report Body 35</div>
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

                        <table class="table table-striped demo-table-search" id="stripedTable">
                            <thead>
                            <tr>
                               <th>National ID</th>
                        <th>Person Alternate ID</th>
                        <%--<th>Alternative ID Type</th>--%>
                        <th>Equity Code</th>
                        <th>Nationality Code</th>
                        <th>Home Language Code</th>
                        <th>Gender Code</th>
                        <th>Citizen Resident Status Code</th>
                        <th>Socioeconomic Status Code</th>
                        <th>Disability Status Code</th>
                        <th>Last Name</th>
                        <th>First Name</th>
                        <%--<th>Middle Name</th>--%>
                        <th>Person Title</th>
                        <th>Birth Date</th>
                        <th>Home Address 1</th>
                        <th>Home Address 2</th>
                        <th>Home Address 3</th>
                        <th>Postal Address 1</th>
                        <th>Postal Address 2</th>
                        <th>Postal Address 3</th>
                        <th>Home Address Postal Code</th>
                        <th>Postal Address Postal Code</th>
                        <th>Home Phone</th>
                        <th>Phone Number</th>
                        <%--<th>Fax Number</th>--%>
                        <th>Email Address</th>   
                        <th>Province Code</th>
                        <th>Filler 01</th>
                        <th>Filler 02</th>
                        <%--<th>Previous Last Name</th>
                        <th>Previous Alternate ID</th>
                        <th>Previous Alternative ID Type</th>--%>
                        <th>Filler 03</th>
                        <th>Filler 04</th>
                        <th>Seeing Rating ID</th>
                        <th>Hearing Rating ID</th>
                        <th>Communicating Rating ID</th>
                        <th>Walking Rating ID</th>
                        <th>Remembering Rating ID</th>
                        <th>Selfcare Rating ID</th>
                        <th>Date Stamp</th>
                            </tr>
                            </thead>
                            <tbody id="displayauditors" runat="server">
                            </tbody>
                        </table>
                      

            <div class="row" runat="server" id="addBtn">
                <div class="auto-style1">
                    <a href="AddAuditor.aspx">
                        <input type="button" value="Add New Auditor" Class="btn btn-primary" style="float:right;" />
                    </a>
                </div>
            </div> 
        </div>
    </div>
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
