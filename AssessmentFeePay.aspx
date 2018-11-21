﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AssessmentFeePay.aspx.cs" Inherits="InspectIT.AssessmentFeePay" %>

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
                    Pay Assessment Fees
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false">You cannot be allocated more COC’s than what you are permitted to have.  Please either log your Non Logged COC’s or contact the PIRB offices.</div>
                 <div class="alert alert-danger" id="Div1" runat="server" visible="false"></div>
                <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                
                <div class="form-group form-group-default">
                    <label>Assessment Type :</label>
                    <div class="controls">
                        <asp:DropDownList ID="AssessType" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
              <div class="form-group form-group-default">
                    <label>I have read and understood the assessment rules :</label>
                    <div class="controls">
                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="form-control" />
                        <a href="#">Click to view</a>
                    </div>
                </div>
                <div class="form-group form-group-default">
                    <label>Location :</label>
                    <div class="controls">
                        <asp:DropDownList ID="locations" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
              <div class="form-group form-group-default">
                    <label>Updated Registration Card required if passed :</label>
                    <div class="controls">
                        <asp:CheckBox ID="CheckBox2" runat="server" CssClass="form-control" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />
                    </div>
                </div>
                <div class="form-group form-group-default">
                        <label>Select Delivery Method :</label>
                        <div class="">
                            <asp:RadioButton ID="CollectPurchaseCOC" CssClass="radio" Text=" Collect at PIRB Offices" GroupName="RadioGroup2" runat="server" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />
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
                <div class="form-group form-group-default">
                    <label>Cost of Card :</label>
                    <div class="controls">
                        <asp:TextBox ID="costOfCard" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-group-default">
                    <label>Assessment Fee :</label>
                    <div class="controls">
                        <asp:TextBox ID="AssessmentFee" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
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
                

                <div id="createPDF" runat="server">

                </div>

                <!-- START Form Control-->
                <div class="row">
                    <div class="auto-style1">
                        <a href="userDashboard.aspx">
                            <input type="button" value="Cancel" class="btn btn-primary" style="float: right; margin-left: 10px;" />
                        </a>
                            <asp:Button ID="btn_buy" runat="server" Text="Pay" cssclass="btn btn-primary"  style="float: right;" OnClick="btn_buy_Click" />
                      
                    </div>
                </div>
                <!-- END Form Control-->

            </div>
        </div>
        <!-- END PANEL -->
    </div>

    <script>
        
        <%--$('#<%=DisclaimerAgreementPurchaseCOC.ClientID%>').change(function () {
            if ($(this).is(":checked")) {
                //alert("a");
                //btn_buy
                $('#<%=btn_buy.ClientID%>').prop('disabled', false);
            }
            else {
                $('#<%=btn_buy.ClientID%>').prop('disabled', 'disabled');
             }
        });--%>
        
       

    </script>

</asp:Content>
