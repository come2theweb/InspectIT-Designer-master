<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="VerifyPurchasePlumber.aspx.cs" Inherits="InspectIT.VerifyPurchasePlumber" %>

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
                    Verify Purchase of Plumbing COC
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                
                <h3>Details</h3>
                <div>
                    <div class="alert alert-danger" id="errormsg" runat="server" visible="false">You cannot be allocated more COC’s than what you are permitted to have.  Please either log your Non Logged COC’s or contact the PIRB offices.</div>
                    <h3>COC Details for <span id="NameofPlumber" runat="server"></span> - Cell: <span id="NumberTo" runat="server"></span></h3>
                    
                    <div class="form-group form-group-default">
                        <label>OTP Code :</label>
                        <div class="controls">
                            <asp:TextBox ID="OTPCode" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group form-group-default">
                        <div class="controls">
                            <div class="btn-group pull-right">
                                <asp:Button ID="resendOTP" runat="server" Text="Resend OTP" cssclass="btn btn-danger" style="float: right;" OnClick="resendOTP_Click" />   
                            <asp:Button ID="btn_buy" runat="server" Text="Verify" cssclass="btn btn-success" style="float: right;" OnClick="btn_buy_Click" Enabled="false" />
                            </div>
                            
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->

</asp:Content>
