<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditCOCStatementType.aspx.cs" Inherits="InspectIT.EditCOCStatementType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="assets/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <link href="pages/css/pages.css" rel="stylesheet" />
    <link href="pages/css/ie9.css" rel="stylesheet" />
    <link href="pages/css/pages.min.css" rel="stylesheet" />
    <link href="pages/css/themes/abstract.css" rel="stylesheet" />
    <style>
        h4 {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid container-fixed-lg bg-white">
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div class="panel-title">
                    COC Statement Type
                </div>
                <div class="pull-right alert alert-info">
                    COC Number: <asp:Label ID="COCNumber" runat="server" Text="1"></asp:Label>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <h4>What Type of COC is this?</h4>
                    <div class="col-md-6">

                        <div class="form-group form-group-default">
                            <div class="controls">
                                <asp:Button ID="btn_updateDetails" CssClass="btn btn-primary" Style="float: right;" runat="server" Text="Normal C.O.C" OnClick="btn_updateDetails_Click" />
                                <asp:Button ID="Button1" CssClass="btn btn-primary" Style="float: right;" runat="server" Text="Sales" />
                                <asp:Button ID="Button2" CssClass="btn btn-primary" Style="float: right;" runat="server" Text="Pre-Inspection" />
                            </div>
                        </div>

                    </div>
                    
                </div>

            </div>
        </div>
    </div>

    <script>
        $('.datepicker-range').datepicker();
    </script>

</asp:Content>
