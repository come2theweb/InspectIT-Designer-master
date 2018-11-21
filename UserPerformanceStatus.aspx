<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserPerformanceStatus.aspx.cs" Inherits="InspectIT.UserPerformanceStatus" %>
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
        <div class="panel-title">My Performance Status
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
            <h4>Total CPD Points : <span id="cpdpoints" runat="server"></span></h4>

             <ul class="nav nav-tabs nav-tabs-simple" role="tablist">
            <li class="active"><a href="#TabActive" data-toggle="tab" role="tab">Active</a></li>
            <li class=""><a href="#TabArchived" data-toggle="tab" role="tab">Archived</a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane active" id="TabActive">
                      <table class="table table-striped demo-table-search" id="stripedTable">
            <thead>
            <tr>
                <th>Date</th>
                <th>Performance Type</th>
                <th>Details</th>
                <th>Points</th>
                <th></th>
            </tr>
            </thead>
            <tbody  id="displayusers" runat="server">
            
            </tbody>
        </table>
                </div>
                <div class="tab-pane" id="TabArchived">
                      <table class="table table-striped demo-table-search" id="stripedTable">
            <thead>
            <tr>
                <th>Date</th>
                <th>Performance Type</th>
                <th>Details</th>
                <th>Points</th>
                <th></th>
            </tr>
            </thead>
            <tbody  id="Tbody1" runat="server">
            
            </tbody>
        </table>
                </div>
            </div>


        </div>
    </div>
    <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->

     <div id="modal_default" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add / Update Performance Activity</h5>
                </div>
                <div class="modal-body">

                    <div class="form-group">
                        <label>Date</label>
                        <asp:TextBox ID="date" runat="server" CssClass="form-control datepicker-range"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Performance Type</label>
                        <asp:DropDownList ID="performanceType" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Details</label>
                        <asp:TextBox ID="details" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Attachment</label>

                        <asp:TextBox ID="TextBox1aa" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label>Performance Point Allocation</label>
                        <asp:TextBox ID="points" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>This activity has an end date</label>
                        <asp:CheckBox ID="hasEndDate" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label>End Date</label>
                        <asp:TextBox ID="endDate" runat="server" CssClass="form-control  datepicker-range"></asp:TextBox>
                    </div>

                    <asp:TextBox ID="PerformanceStatusID" runat="server" CssClass="form-control hide"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-link" data-dismiss="modal">Close</button>

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

        function openPerformance(idd) {
            $("#modal_default").modal();
            $("#<%=PerformanceStatusID.ClientID%>").val(idd);
            $.ajax({
                type: "POST",
                url: 'API/WebService1.asmx/getPerformanceStatusDetails',
                data: { id: idd },
                success: function (data) {
                    $("#<%=date.ClientID%>").val(data.Date);
                    $("#<%=performanceType.ClientID%>").val(data.PerformanceType);
                    $("#<%=points.ClientID%>").val(data.PerformancePointAllocation);
                    $("#<%=details.ClientID%>").val(data.Details);
                    $("#<%=TextBox1aa.ClientID%>").val(data.Attachment);
                    $("#<%=endDate.ClientID%>").val(data.endDate);
                    if (data.hasEndDate == "True") {
                        $("#<%=hasEndDate.ClientID%>").attr("checked", "checked");
                    }
                },
            });
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
