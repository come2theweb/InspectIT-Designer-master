<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PurchasePlumbingCOCs.aspx.cs" Inherits="InspectIT.PurchasePlumbingCOCs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="assets/plugins/bootstrap-select2/select2.min.js"></script>
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
                    Purchase Plumbing COC
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false">You cannot be allocated more COC’s than what you are permitted to have.  Please either log your Non Logged COC’s or contact the PIRB offices.</div>
                 <div class="alert alert-danger" id="Div1" runat="server" visible="false"></div>
                <div class="form-group form-group-default">
                    <label>Number of My Non Logged COC's :</label>
                    <div class="controls">
                        <asp:TextBox ID="NonLoggedCOCsPurchased" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <!-- END Form Control-->

                <!-- START Form Control-->
                <div class="form-group form-group-default">
                    <label>Total Number COC's You are Permitted :</label>
                    <div class="controls">
                        <asp:TextBox ID="NumberOfPermittedCOCs" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Number of Permitted COC's that you are able to purchase :</label>
                    <div class="controls">
                        <asp:TextBox ID="COCsAbleToPurchase" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-group-default">
                    <label>Select type of COC you wish to purchase :</label>
                    <div class="">
                        <asp:RadioButton CssClass="radio" ID="ElectronicCOC" Text=" Electronic" Checked="True" GroupName="RadioGroup1" value="Electronic" runat="server" />
                        <asp:RadioButton CssClass="radio" ID="PaperBasedCOC" Text=" Paper Based" GroupName="RadioGroup1" runat="server" value="Paper" />
                    </div>
                </div>
                <div class=" alert alert-danger" runat="server" id="dispErr">
                </div>
                <div class="form-group form-group-default">
                    <label>Number of COC's You wish to Purchase :</label>
                    <div class="controls">
                        <asp:TextBox ID="NoCOCsPurchase" runat="server" CssClass="form-control" TextMode="Number" OnTextChanged="NoCOCsPurchase_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                </div>
                <div id="paperdel" class="hide" runat="server">
                    <div class="form-group form-group-default">
                        <label>Select Delivery Method :</label>
                        <div class="">
                            <asp:RadioButton ID="CollectPurchaseCOC" CssClass="radio" Text=" Collect at PIRB Offices" Checked="True" GroupName="RadioGroup2" runat="server" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />
                            <asp:RadioButton ID="RegisteredPostPurchaseCOC" CssClass="radio" Text=" Registered Post" GroupName="RadioGroup2" runat="server" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />
                            <asp:RadioButton ID="CourierPurchaseCOC" CssClass="radio" Text=" Courier" GroupName="RadioGroup2" runat="server" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />
                        </div>
                    </div>
                    <div class="form-group form-group-default">
                        <label>Delivery Cost :</label>
                        <div class="controls">
                            <asp:TextBox ID="DeliveryCostCOCPurchase" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                
                <div class="form-group form-group-default" style="display:none;">
                    <label>Plumbing COC's :</label>
                    <div class="controls">
                        <asp:TextBox ID="PlumbingCOCs" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-group-default">
                    <label>Cost of COC :</label>
                    <div class="controls">
                        <asp:TextBox ID="CertificateCost" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-group-default">
                    <label>VAT @15% :</label>
                    <div class="controls">
                        <asp:TextBox ID="VATCOCPurchase" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-group-default">
                    <label>Total Due :</label>
                    <div class="controls">
                        <asp:TextBox ID="TotalDueCOCPurchase" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-group-default">
                    <label>Disclaimer :</label>
                    <div class="controls">
                        <asp:CheckBox  CssClass="checkbox check-success" ID="DisclaimerAgreementPurchaseCOC" TextAlign="Right" runat="server" Text="I declare and understand:" />
                        <ul>
                            <li>That all the plumbing works comply in all respect to the plumbing regulations and laws as defined by the National Compulsory Standards and Local By-Laws</li>
                            <li>The PIRB's auditing, rectification and disciplinary policy and procedures and that i fully comply to them</li>
                            <li>If I fail to comply with the policy and procedures it may result in disciplinary action  being taken against me, which could result in my suspension from the PIRB</li>
                            <li>As a professional plumber I abide by the PIRB Code of Conduct as a professional Plumber</li>
                        </ul>

                    </div>
                </div>
                <!-- END Form Control-->

                <div id="createPDF" runat="server">

                </div>

                <!-- START Form Control-->
                <div class="row">
                    <div class="auto-style1">
                        <a href="userDashboard.aspx">
                            <input type="button" value="Cancel" class="btn btn-primary" style="float: right; margin-left: 10px;" />
                        </a>
                        <a href="#">
                            <asp:Button ID="btn_buy" runat="server" Text="Purchase" cssclass="btn btn-primary" disabled  style="float: right;" OnClick="btn_buy_Click" />
                        </a>
                    </div>
                </div>
                <!-- END Form Control-->

            </div>
        </div>
        <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->

    <asp:TextBox ID="totalAmountdue" runat="server" cssclass="hidden"></asp:TextBox>
     <asp:TextBox ID="CertificateCosts" runat="server" cssclass="hidden"></asp:TextBox>
     <asp:TextBox ID="VATCOCPurchases" runat="server" cssclass="hidden"></asp:TextBox>

    <script>

<%--        $("#<%=NoCOCsPurchase.ClientID%>").change(function () {

            var NoAv = $("#<%=COCsAbleToPurchase.ClientID%>").val();
            var CurNo = $("#<%=NoCOCsPurchase.ClientID%>").val()

            if (parseInt(CurNo) > parseInt(NoAv)) {
                alert("Can't purchase more than 'Number of COC's I am able to Purchase'");
            } else {
                workoutcoc()
            }
        });--%>

        $('#<%=DisclaimerAgreementPurchaseCOC.ClientID%>').change(function () {
            if ($(this).is(":checked")) {
                //alert("a");
                //btn_buy
                $('#<%=btn_buy.ClientID%>').prop('disabled', false);
            }
            else {
                $('#<%=btn_buy.ClientID%>').prop('disabled', 'disabled');
             }
        });


        $("#<%=ElectronicCOC.ClientID%>").click(function () {
            //workoutcoc()
            $("#<%=paperdel.ClientID%>").addClass("hide");
        });

        $("#<%=PaperBasedCOC.ClientID%>").click(function () {
            //workoutcoc();
            $("#<%=paperdel.ClientID%>").removeClass("hide");
        });

        <%--//CollectPurchaseCOC RegisteredPostPurchaseCOC CourierPurchaseCOC
        $("#<%=CollectPurchaseCOC.ClientID%>").click(function () {
            $("#<%=CertificateCost.ClientID%>").val("");
            $("#<%=CertificateCosts.ClientID%>").val("");
            $("#<%=TotalDueCOCPurchase.ClientID%>").val("");
            $("#<%=totalAmountdue.ClientID%>").val("");
            $("#<%=VATCOCPurchase.ClientID%>").val("");
            $("#<%=VATCOCPurchases.ClientID%>").val("");
            $("#<%=NoCOCsPurchase.ClientID%>").val("");
            $("#<%=DeliveryCostCOCPurchase.ClientID%>").val("");
        });

        $("#<%=RegisteredPostPurchaseCOC.ClientID%>").click(function () {
            $("#<%=CertificateCost.ClientID%>").val("");
             $("#<%=CertificateCosts.ClientID%>").val("");
            $("#<%=TotalDueCOCPurchase.ClientID%>").val("");
            $("#<%=totalAmountdue.ClientID%>").val("");
            $("#<%=VATCOCPurchase.ClientID%>").val("");
            $("#<%=VATCOCPurchases.ClientID%>").val("");
            $("#<%=NoCOCsPurchase.ClientID%>").val("");
            $("#<%=DeliveryCostCOCPurchase.ClientID%>").val("");
        });

        $("#<%=CourierPurchaseCOC.ClientID%>").click(function () {
            $("#<%=CertificateCost.ClientID%>").val("");
             $("#<%=CertificateCosts.ClientID%>").val("");
            $("#<%=TotalDueCOCPurchase.ClientID%>").val("");
            $("#<%=totalAmountdue.ClientID%>").val("");
            $("#<%=VATCOCPurchase.ClientID%>").val("");
            $("#<%=VATCOCPurchases.ClientID%>").val("");
            $("#<%=NoCOCsPurchase.ClientID%>").val("");
            $("#<%=DeliveryCostCOCPurchase.ClientID%>").val("");
         });--%>

        <%--function workoutcoc() {

            // Work out the costs
            //Paper COC: R124 incl
            //Courier Fee: R235 incl
            //Elec Coc: R117 incl

            if ($('#<%=ElectronicCOC.ClientID %>').is(':checked')) {
                console.log("electric");

                // ONLINE
                $("#paperdel").hide();

                var totcost = 0;

                var nococ = $("#<%=NoCOCsPurchase.ClientID%>").val();
                if (nococ != 0) {
                    totcost = (nococ * 102.63);
                    $("#<%=CertificateCost.ClientID%>").val(totcost);
                    $("#<%=CertificateCosts.ClientID%>").val(totcost);
                }

                // VAT
                var vat = (totcost * 0.15);
                $("#<%=VATCOCPurchase.ClientID%>").val(Number(vat).toFixed(2));
                $("#<%=VATCOCPurchases.ClientID%>").val(Number(vat).toFixed(2));
                var amm = (totcost + vat);
                $("#<%=TotalDueCOCPurchase.ClientID%>").val(Number(amm).toFixed(2));
                $("#<%=totalAmountdue.ClientID%>").val(Number(amm).toFixed(2));

            } else if ($('#<%=PaperBasedCOC.ClientID %>').is(':checked')) {
                console.log("paper");

                $("#paperdel").show();

                var nococ = $("#<%=NoCOCsPurchase.ClientID%>").val();
                if (nococ != 0) {

                    var totcost = (nococ * 108.77);

                    if ($('#<%=CourierPurchaseCOC.ClientID %>').is(':checked')) {
                        console.log("CourierPurchaseCOC");

                        var cofee1 = 205;
                        $("#<%=DeliveryCostCOCPurchase.ClientID%>").val(cofee1);
                        $("#<%=CertificateCost.ClientID%>").val(totcost);
                        $("#<%=CertificateCosts.ClientID%>").val(totcost);
                        totcost = (totcost + cofee1);
                        var vat = (totcost * 0.15);
                        $("#<%=VATCOCPurchase.ClientID%>").val(Number(vat).toFixed(2));
                        $("#<%=VATCOCPurchases.ClientID%>").val(Number(vat).toFixed(2));
                        var amm = (totcost + vat);
                        $("#<%=TotalDueCOCPurchase.ClientID%>").val(Number(amm).toFixed(2));
                        $("#<%=totalAmountdue.ClientID%>").val(Number(amm).toFixed(2));
                    } else if ($('#<%=RegisteredPostPurchaseCOC.ClientID %>').is(':checked')) {
                        console.log("RegisteredPostPurchaseCOC");

                        var cofee = 39.48;
                        $("#<%=DeliveryCostCOCPurchase.ClientID%>").val(cofee);
                        $("#<%=CertificateCost.ClientID%>").val(totcost);
                        $("#<%=CertificateCosts.ClientID%>").val(totcost);
                        totcost = (totcost + cofee);
                        var vat = (totcost * 0.15);
                        $("#<%=VATCOCPurchase.ClientID%>").val(Number(vat).toFixed(2));
                        $("#<%=VATCOCPurchases.ClientID%>").val(Number(vat).toFixed(2));
                        var amm = (totcost + vat);
                        $("#<%=TotalDueCOCPurchase.ClientID%>").val(Number(amm).toFixed(2));
                        $("#<%=totalAmountdue.ClientID%>").val(Number(amm).toFixed(2));
                    } else if ($('#<%=CollectPurchaseCOC.ClientID %>').is(':checked')) {
                        console.log("CollectPurchaseCOC");

                        $("#<%=DeliveryCostCOCPurchase.ClientID%>").val("0.00");
                        $("#<%=CertificateCost.ClientID%>").val(totcost);
                        $("#<%=CertificateCosts.ClientID%>").val(totcost);
                        // VAT
                        var vat = (totcost * 0.15);
                        $("#<%=VATCOCPurchase.ClientID%>").val(vat);
                        $("#<%=VATCOCPurchases.ClientID%>").val(vat);
                        var amm = (totcost + vat);
                        $("#<%=TotalDueCOCPurchase.ClientID%>").val(Number(amm).toFixed(2));
                        $("#<%=totalAmountdue.ClientID%>").val(Number(amm).toFixed(2));
                    }

                }


            }

        }--%>


    </script>

</asp:Content>
