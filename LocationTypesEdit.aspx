<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="LocationTypesEdit.aspx.cs" Inherits="InspectIT.LocationTypesEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/lodash.min.js"></script>
    <link href="http://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.9/summernote.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid container-fixed-lg bg-white">
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div class="panel-title">
                    Edit Location Type
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">

                <div class="form-group form-group-default required">
                    <label>Location Type :</label>
                    <div class="controls">
                                <asp:TextBox ID="location" CssClass="form-control" runat="server" placeholder="Location Type" required></asp:TextBox>
                    </div>
                </div>

                 <div class="form-group form-group-default">
                    <label>Address :</label>
                    <div class="controls">
                                <asp:TextBox ID="Address" CssClass="form-control" runat="server" placeholder="Address"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="auto-style1">
                        
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnsave_Click1" style="float:right;" />
                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="btn_update_Click1" style="float:right;"/>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<script src="assets/js/summernote.js"></script>--%>
    <script src="assets/js/summernote.min.js"></script>

    <script>
        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        var id = getUrlVars()["id"];
        var responsiveHelper = undefined;
        var breakpointDefinition = {
            tablet: 1024,
            phone: 480
        };
        


    </script>

</asp:Content>
