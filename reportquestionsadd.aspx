<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="reportQuestionsAdd.aspx.cs" Inherits="InspectIT.reportQuestionsAdd" %>

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

    <div class="container-fluid container-fixed-lg bg-white">
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div class="panel-title">
                    Report Statement
                </div>
            </div>
            <div class="panel-body">

                <div class="form-group form-group-default">
                    <label>Installation Type :</label>
                    <div class="controls">
                        <asp:DropDownList ID="TypeOfInstallation" runat="server" CssClass="form-control" OnSelectedIndexChanged="TypeOfInstallation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Sub Type :</label>
                    <div class="controls">
                        <asp:DropDownList ID="subTypes" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:TextBox ID="FormFields" runat="server" CssClass="form-control hide"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Statement :</label>
                    <div class="controls">
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control hide"></asp:DropDownList>
                        <asp:TextBox ID="question" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group form-group-default hide">
                    <label>Review Name :</label>
                    <div class="controls">
                        <asp:TextBox ID="Name" runat="server" placeholder="Enter Your Comment Name" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                
                <div class="form-group form-group-default">
                    <label>Comment :</label> 
                    <div class="controls">
                        <asp:TextBox ID="Comment" runat="server" placeholder="Enter Your comment" TextMode="MultiLine" Height="120px" CssClass="form-control"></asp:TextBox>

                    </div>
                </div>

                 <div class="form-group form-group-default">
                    <label>Comment Files/Pictures :</label>
                    <div class="controls">
                        <asp:FileUpload ID="FileUpload2" runat="server" />
                        <div class="pull-right"><asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Upload" OnClick="btnUpload_Click" formnovalidate /></div>
                        <div id="Div1" runat="server"></div>
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Reference :</label>
                    <div class="controls">
                        <asp:TextBox ID="TextBox1" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Reference Files/Pictures :</label>
                    <div class="controls">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <div class="pull-right"><asp:Button ID="btnUpload" CssClass="btn btn-primary" runat="server" Text="Upload" OnClick="btnUploads_Click" formnovalidate /></div>
                        <div id="CurrentMedia" runat="server"></div>
                    </div>
                </div>
                 <div class="form-group form-group-default">
                    <label>Refix Point Allocation not completed :</label>
                    <div class="controls">
                        <asp:TextBox ID="refixPointsNotComplete" runat="server" placeholder="" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-group-default">
                    <label>Refix Point Allocation :</label>
                    <div class="controls">
                        <asp:TextBox ID="refixPoints" runat="server" placeholder="" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-group-default">
                    <label>Cautionary Point Allocation :</label>
                    <div class="controls">
                        <asp:TextBox ID="cautionaryPoints" runat="server" placeholder="" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-group-default">
                    <label>Complimentary Point Allocation :</label>
                    <div class="controls">
                        <asp:TextBox ID="complimentaryPoints" runat="server" placeholder="" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group form-group-default">
                    <label>Active :</label>
                    <div class="controls">
                        <asp:CheckBox ID="isActive" runat="server" CssClass="form-control" Checked />
                    </div>
                </div>

                <div class="row">
                    <div class="text-right">
                        <asp:HiddenField ID="CommentID" runat="server" />
                        <asp:Button ID="btn_add" CssClass="btn btn-primary" runat="server" Text="Add Statement" OnClick="btn_add_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>

     
        function deleteImage(id) {
            var result = confirm("Are you sure?");
            if (result) {
                document.location.href = "DeleteItems.aspx?op=delCommentImg&id=" + id;
            }
        }

        function deleteImagea(id) {
            var result = confirm("Are you sure?");
            if (result) {
                document.location.href = "DeleteItems.aspx?op=delReferenceImg&id=" + id;
            }
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
