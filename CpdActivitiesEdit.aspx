<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CpdActivitiesEdit.aspx.cs" Inherits="InspectIT.CpdActivitiesEdit" %>

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
                   Add / Edit CPD Activities
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                
                            <div class="alert alert-danger alert-styled-left alert-bordered" id="errormsg" runat="server" visible="false"></div>
                <div class="form-group form-group-default">
                    <label>Category of CPD Registration : <span style="color:red;">*</span></label>
                    <div class="controls">
                        <asp:DropDownList ID="Category" CssClass="form-control" runat="server" required>
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="1">Category 1: Developmental Activities</asp:ListItem>
                            <asp:ListItem Value="2">Category 2: Work-based Activities</asp:ListItem>
                            <asp:ListItem Value="3">Category 3: Individual Activities</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                 <div class="form-group form-group-default">
                    <label>Activity : <span style="color:red;">*</span></label>
                    <div class="controls">
                            <asp:TextBox ID="Activity" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="5" MaxLength="160" required></asp:TextBox>
                    </div>
                </div>

                 <div class="form-group form-group-default">
                    <label>Product Code : <span style="color:red;">*</span></label>
                    <div class="controls">
                            <asp:TextBox ID="ProductCode" CssClass="form-control" runat="server" required></asp:TextBox>
                    </div>
                      <asp:Button ID="genQR" runat="server" CssClass="btn btn-primary" style="margin-left:10px;" Text="Generate QR" OnClick="genQR_Click" formnovalidate />
                                                <asp:Image ID="Image1" runat="server" style="height:100px;" />
                </div>

                 <div class="form-group form-group-default">
                    <label>CPD Points : <span style="color:red;">*</span></label>
                    <div class="controls">
                            <asp:TextBox ID="Points" TextMode="Number" CssClass="form-control" runat="server" required></asp:TextBox>
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Start Date : <span style="color:red;">*</span></label>
                    <div class="controls">
                            <asp:TextBox ID="StartDate" CssClass="form-control  datepicker-range" runat="server" required></asp:TextBox>
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>End Date : <span style="color:red;">*</span></label>
                    <div class="controls">
                            <asp:TextBox ID="EndDate" CssClass="form-control  datepicker-range" runat="server" required></asp:TextBox>
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
        

        $('.datepicker-range').datepicker();

    </script>

</asp:Content>
