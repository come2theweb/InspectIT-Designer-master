<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="settings.aspx.cs" Inherits="InspectIT.settings" %>

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
                <div class="panel-title">
                    Settings
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>

                <div class="form-group form-group-default">
                    <label>Audit Percentage:</label>
                    <asp:TextBox ID="AuditPercentage" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group form-group-default">
                    <label>Audit Refix Number:</label>
                    <asp:TextBox ID="AditRefixNumber" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group form-group-default">
                    <label>Company Name:</label>
                    <asp:TextBox ID="CompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group form-group-default">
                    <label>Company Telephone Number:</label>
                    <asp:TextBox ID="CompanyTelephoneNumber" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group form-group-default">
                    <label>Company Vat Number:</label>
                    <asp:TextBox ID="CompanyVatNumber" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group form-group-default">
                    <label>Eskom Rebate Audit Percentage:</label>
                    <asp:TextBox ID="EskomRebateAuditPercentage" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group form-group-default">
                    <label>Next Random Audit Run Date:</label>
                    <asp:TextBox ID="NextRandomAuditRunDate" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group form-group-default">
                    <label>Office Hours:</label>
                    <asp:TextBox ID="OfficeHours" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group form-group-default">
                    <label>Pirb Order Email:</label>
                    <asp:TextBox ID="PirbOrderEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group form-group-default">
                    <label> Pirb Order Fax:</label>
                    <asp:TextBox ID="PirbOrderFax" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group form-group-default">
                    <label>Plumber Max Non Logged Certificates:</label>
                    <asp:TextBox ID="PlumberMaxNonLoggedCertificates" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group form-group-default">
                    <label> Refix Period:</label>
                    <asp:TextBox ID="RefixPeriod" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group form-group-default">
                    <label>System Email Address:</label>
                    <asp:TextBox ID="SystemEmailAddress" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group form-group-default">
                    <label>Vat Percentage: <span style="color:red;">(0.15)</span></label>
                    <asp:TextBox ID="VatPercentage" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="row">
                    <div class="auto-style1">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="Button1_Click" />
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
