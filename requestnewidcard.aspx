<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RequestNewIDCard.aspx.cs" Inherits="InspectIT.RequestNewIDCard" %>

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
                    Request a New ID Card
       
                </div>
                <div class="pull-right">
                    <div class="col-xs-12">
                        <input type="text" id="search-table" class="form-control pull-right" placeholder="Search">
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                <br />
                <div class="row">
                    <div class="col-md-4">
                        <img src="assets/img/blog1.jpg" alt="" id="IDCardFront" cssclass="img-responsive" style="width: 100%; max-width: 200px; float: right;" />

                    </div>
                    <div class="col-md-4">
                        <img src="assets/img/blog1.jpg" alt="" id="IDCardBack" cssclass="img-responsive" style="width: 100%; max-width: 200px; float: left;" />

                    </div>
                    <div class="col-md-4">
                        <form>
                            <label><strong>Select Delivery Method :</strong></label><br />
                            <asp:RadioButton ID="CollectIDCard" Text=" Collect at PIRB Offices" Checked="True" GroupName="RadioGroup1" runat="server" /><br />
                            <asp:RadioButton ID="RegisteredPostIDCard" Text=" Registered Post" GroupName="RadioGroup1" runat="server"/><br />
                            <asp:RadioButton ID="CourierIDCard" Text=" Courier" GroupName="RadioGroup1" runat="server"/><br />

                            <asp:CheckBox ID="DisclaimerAgreementIDCard" TextAlign="Right" runat="server" Text="Disclaimer: When applicable I will not hold the PIRB accountable for the non performance of the courier and/or postal service used by the PIRB once my card has left the PIRB’s offices." />
                        </form>
                    </div>
                </div>

                <br />
                <br />

                <div class="row">
                    <div class="col-md-8">
                    </div>
                    <div class="col-md-2">
                        <label>Cost of New Card</label><br />
                        <label>Delivery Cost</label><br />
                        <label>VAT @15%</label><br />
                        <label><strong>Total Due</strong></label>
                    </div>
                    <div class="col-md-2">
                        <label id="CostOfNewCard" runat="server">R 100.00</label><br />
                        <%--<asp:TextBox ID="CostOfNewCard" runat="server" Text="100"></asp:TextBox>
                        <asp:TextBox ID="DeliveryCost" runat="server"></asp:TextBox>
                        <asp:TextBox ID="VAT" runat="server" Text="15"></asp:TextBox>
                        <asp:TextBox ID="TotalDue" runat="server"></asp:TextBox>--%>
                        <label id="DeliveryCost" runat="server">R 0.00</label><br />
                        <label id="VAT" runat="server">R 15.00</label><br />
                        <label id="TotalDue" runat="server"><strong>R 115.00</strong></label>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <a href="EditRegistrationOrUpdateDetails.aspx">
                            <input type="button" value="Update Details" class="btn btn-primary" style="float: right;" />
                        </a>
                    </div>
                    <div class="col-md-6">
                        <a href="EditRegistrationOrUpdateDetails.aspx">
                            <input type="button" value="Cancel" class="btn btn-primary" style="float: right; margin-left: 10px;" />
                        </a>
                        <a href="#">
                            <asp:Button ID="btn_buy" runat="server" CssClass="btn btn-primary" Text="Buy" OnClick="btn_buy_click" style="float: right;"/>
                        </a>
                    </div>
                </div>
                <br />

            </div>
        </div>
        <!-- END PANEL -->
    </div>

    <script>
        $(document).ready(function () {
            $.ajax({
                type: 'POST',
                url: "../API/IDCard?NewCardID=1",
                dataType: 'json',
                traditional: true,
                success: function (data) {
                    var result = JSON.stringify(data);
                    result = result.replace("[", "");
                    result = result.replace("]", "");
                    result = JSON.parse(result);
                    $("#<%=DisclaimerAgreementIDCard.ClientID%>").html(result.DisclaimerAgreementIDCard);
                    $("#<%=CostOfNewCard.ClientID%>").html("R " + result.CostOfNewCard);
                    $("#<%=DeliveryCost.ClientID%>").html("R " + result.DeliveryCost);
                    $("#<%=VAT.ClientID%>").html(result.VAT + " %");
                    $("#<%=TotalDue.ClientID%>").html("R " + result.TotalDue);
                }
            });
        });


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
