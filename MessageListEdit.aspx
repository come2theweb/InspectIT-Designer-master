<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MessageListEdit.aspx.cs" Inherits="InspectIT.MessageListEdit" %>

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
                    Edit Message
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">

                <div class="form-group form-group-default">
                    <label>Message Name :</label>
                    <p id="messagename" runat="server">test</p>
                </div>
                <div class="form-group form-group-default">
                    <label>Message :</label>
                    <div class="controls">
                        <textarea id="summernote"></textarea>
                        <asp:TextBox ID="info" style="display:none;" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>


                <div class="row">
                    <div class="auto-style1">
                        <asp:Button ID="btn_update" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btn_update_Click1" Style="float: right;" />

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
        

        if (id != null) {
            $('#summernote').summernote('editor.insertText', $("#<%=info.ClientID%>").val());
        }

        $('#summernote').on('summernote.keyup', function (we, e) {
            console.log('Key is released:', e.keyCode);
        });

        $('#summernote').on('summernote.change', function (we, contents, $editable) {
            //console.log('summernote\'s content is changed.', contents);
            $("#<%=info.ClientID%>").val(contents.replace("<p>", "").replace("</p>", "").replace("<br>", ""));
        });


    </script>

</asp:Content>
