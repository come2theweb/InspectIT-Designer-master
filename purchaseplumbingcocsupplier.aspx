<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PurchasePlumbingCOCSupplier.aspx.cs" Inherits="InspectIT.PurchasePlumbingCOCSupplier" %>

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
                    Allocate COC
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                <div class="alert alert-danger" id="err" runat="server" visible="false"></div>
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false">You cannot be allocated more COC’s than what you are permitted to have.  Please either log your Non Logged COC’s or contact the PIRB offices.</div>

                <h5>Search Details</h5>
                <div class="form-group form-group-default required">
                    <label>PIRB LICENSED REG/NO :</label>
                    <div class="controls">
                        <asp:TextBox ID="RegNo" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Button ID="SearchPlumber" runat="server" Text="Search" cssclass="btn btn-primary" style="float: right;" OnClick="SearchPlumber_Click" formnovalidate />
                    </div>
                </div>
                <div id="displayfrm" runat="server" visible="false">
                    <asp:HiddenField ID="PID" runat="server" Value="11"  />
                    <h5>Details of Licensed Plumber: <span id="NameofPlumber" runat="server"></span> - Cell: <span id="NumberTo" runat="server"></span></h5>
                    
                    <div class="row">

                    <div class="col-md-5">
                        <div class="form-group form-group-default hide">
                            <label>Registration Number :</label>
                            <div class="controls">
                                <asp:TextBox ID="PlumberRegNo" runat="server"  CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Mobile Contact :</label>
                            <div class="controls">
                                <asp:TextBox ID="PlumberContact" runat="server"  CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Email :</label>
                            <div class="controls">
                                <asp:TextBox ID="PlumberEmail" runat="server"  CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="form-group form-group-default">
                            <label>Plumbers Name :</label>
                            <div class="controls">
                                <asp:TextBox ID="PlumberFullName" runat="server"  CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Business Contact :</label>
                            <div class="controls">
                                <asp:TextBox ID="PlumberBusContact" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <%--<div class="form-group form-group-default text-center">
                            <div class="controls" id="PlumberImage"><img src="assets\img\profiles\avatar_small2x.jpg" /></div>
                        </div>--%>
                    </div>
                        </div>
                    <div class="row">
                        <div class="col-md-12">
                             <div class="form-group form-group-default">
                                <label>Global Message:</label>
                                <div class="controls">
                                    <asp:TextBox ID="notice" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6" ReadOnly="true" ></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div style="box-shadow: 10px 10px 5px #888888;width:450px;border:1px solid grey;" id="CardFront" runat="server"></div>

                        </div>
                        <div class="col-md-6">
                                <div style="box-shadow: 10px 10px 5px #888888;width:450px;border:1px solid grey;" id="CardBack" runat="server"></div>

                        </div>
                    </div>
                


                    <div class="form-group form-group-default hide">
                        <label>You have userd:</label>
                        <div class="controls">
                            <asp:TextBox ID="NoUsed" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group form-group-default hide">
                        <label>Paper stock you have left:</label>
                        <div class="controls">
                            <asp:TextBox ID="NoStock" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                
                    <div class="form-group form-group-default hide">
                        <label>Number of Non Logged COC's Purchased :</label>
                        <div class="controls">
                            <asp:TextBox ID="NonLoggedCOCsPurchased" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <!-- END Form Control-->

                    <!-- START Form Control-->
                    <div class="form-group form-group-default hide">
                        <label>Number of Permitted COC's :</label>
                        <div class="controls">
                            <asp:TextBox ID="NumberOfPermittedCOCs" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group form-group-default">
                        <label>Number of COC's Licsended Plumber is permitted to purchase :</label>
                        <div class="controls">
                            <asp:TextBox ID="COCsAbleToPurchase" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-default hide">
                        <label>Select type of COC you wish to purchase :</label>
                        <div class="">
                            <asp:RadioButton CssClass="radio" ID="PaperBasedCOC" Text=" Paper Based" GroupName="RadioGroup1" runat="server" value="Paper" Checked="true" />
                        </div>
                    </div>
                    <div id="paperdel">
                        <div class="form-group form-group-default hide">
                            <label>Select Delivery Method :</label>
                            <div class="">
                                <asp:RadioButton ID="CollectPurchaseCOC" CssClass="radio" Text=" Collect at Supplier" Checked="True" GroupName="RadioGroup2" runat="server" />
                            </div>
                        </div>
                        <div class="form-group form-group-default hide">
                            <label>Delivery Cost :</label>
                            <div class="controls">
                                <asp:TextBox ID="DeliveryCostCOCPurchase" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                    <h5>Assign Certificate Stock</h5>

                    <div class="form-group form-group-default">
                        <label>Available Certificate Stock :</label>
                        <div class="controls">
                            <asp:TextBox ID="countCertis" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-default">
                        <label>Select the COC Start Range :</label>
                        <div class="controls">
                            <asp:DropDownList ID="StartRange" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group form-group-default">
                        <label>Select the COC End Range :</label>
                        <div class="controls">
                            <asp:DropDownList ID="EndRange" runat="server" CssClass="form-control" OnSelectedIndexChanged="EndRange_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group form-group-default">
                        <label>Number of COC's to be Allocated to Licensed Plumber :</label>
                        <div class="controls">
                            <asp:TextBox ID="NoCOCsPurchase" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group form-group-default" style="display:none;">
                        <label>Plumbing COC's :</label>
                        <div class="controls">
                            <asp:TextBox ID="PlumbingCOCs" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-default hide">
                        <label>Current Cost of Certificate :</label>
                        <div class="controls">
                            <asp:TextBox ID="CertificateCost" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-default hide">
                        <label>VAT @15% :</label>
                        <div class="controls">
                            <asp:TextBox ID="VATCOCPurchase" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-default hide">
                        <label>Total Due :</label>
                        <div class="controls">
                            <asp:TextBox ID="TotalDueCOCPurchase" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-default">
                        <label>Disclaimer :</label>
                        <div class="controls">
                            <asp:CheckBox  CssClass="checkbox check-success"  ID="DisclaimerAgreementPurchaseCOC"  TextAlign="Right" runat="server" Text="I Declare &amp; understand:" />
                            <p>I acknowledge this allocation of the COC's are done in terms of the PIRB Reseller policy and procedures; and if I fail to comply with the PIRB Reseller policy and procedures it may result in disciplinary action being taken against me, which could result in my removal as a PIRB Reseller. </p>
                        </div>
                    </div>
                   
                    
                    <!-- END Form Control-->

                    <div id="createPDF" runat="server">

                    </div>

                    <!-- START Form Control-->
                    <div class="row">
                        <div class="auto-style1">
                            <a href="ViewCOCStatementSupplier.aspx">
                                <input type="button" value="Cancel" class="btn btn-primary" style="float: right; margin-left: 10px;" />
                            </a>
                            <asp:Button ID="btn_buy" runat="server" Text="Allocate COC's" cssclass="btn btn-primary" disabled style="float: right;" OnClick="btn_buy_Click" />
                        </div>
                    </div>
                     </div>
                    <!-- END Form Control-->
                </div>
            </div>
        </div>
        <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->

    <asp:TextBox ID="totalAmountdue" runat="server" cssclass="hidden"></asp:TextBox>

    <script>

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

        $("#<%=NoCOCsPurchase.ClientID%>").change(function () {

            var NoAv = $("#<%=COCsAbleToPurchase.ClientID%>").val();
            var CurNo = $("#<%=NoCOCsPurchase.ClientID%>").val();

            if (parseInt(CurNo) > parseInt(NoAv)) {
                alert("Can't purchase more than 'Number of COC's I am able to Purchase'");
            } else {
                workoutcoc();
            }
        });

        function workoutcoc() {

            // Work out the costs
            //Paper COC: R124 incl
            //Courier Fee: R235 incl
            //Elec Coc: R117 incl

            if ($('#<%=PaperBasedCOC.ClientID %>').is(':checked')) {
                console.log("paper");

                $("#paperdel").show();

                var nococ = $("#<%=NoCOCsPurchase.ClientID%>").val();
                if (nococ != 0) {

                    var totcost = (nococ * 108.77);
                                       
                    console.log("CourierPurchaseCOC");

                    var cofee1 = 205;
                    $("#<%=DeliveryCostCOCPurchase.ClientID%>").val(cofee1);
                    $("#<%=CertificateCost.ClientID%>").val(totcost);
                    totcost = (totcost + cofee1);
                    var vat = (totcost * 0.15);
                    $("#<%=VATCOCPurchase.ClientID%>").val(Number(vat).toFixed(2));
                    var amm = (totcost + vat);
                    $("#<%=TotalDueCOCPurchase.ClientID%>").val(Number(amm).toFixed(2));
                    $("#<%=totalAmountdue.ClientID%>").val(Number(amm).toFixed(2));

                }


            }

        }


    </script>

</asp:Content>
