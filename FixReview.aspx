<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FixReview.aspx.cs" Inherits="InspectIT.FixReview" %>

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
                    Fix Review
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">

                <div class="form-group form-group-default">
                    <label>Has the job been fixed :</label>
                    <asp:RadioButton ID="rdYes" runat="server" Text="Yes" GroupName="radiosel" CssClass="form-control radio radio-success" Checked="true" />
                    <asp:RadioButton ID="rdNo" runat="server" Text="No" GroupName="radiosel" CssClass="form-control radio radio-danger" />
                </div>

                <div class="form-group form-group-default">
                    <label>Comments :</label>
                    <div class="controls">
                        <asp:TextBox ID="ReviewComments" runat="server" CssClass="form-control" TextMode="MultiLine" Height="120px"></asp:TextBox>
                    </div>
                </div>
                
                <div class="form-group form-group-default">
                    <label>Files/Pictures :</label>
                    <div class="controls">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <div class="pull-right"><asp:Button ID="btnUpload" CssClass="btn btn-primary" runat="server" Text="Upload" OnClick="btnUpload_Click" formnovalidate /></div>
                        <div id="CurrentMedia" runat="server"></div>
                    </div>
                </div>
                
                <div class="row">
                    <div class="text-right">
                        
                        <asp:Button ID="btn_add" CssClass="btn btn-primary" runat="server" Text="Save Review" OnClick="btn_add_Click" />
                        
                    </div>
                </div>


            </div>
        </div>
    </div>

    <script>

        function deleteImage(id) {
            if (confirm('Are you sure?')) {
                $.post('https://197.242.82.242/inspectit/api/frmImgDel?imgid=' + id, { }, function (data) {
                    $("#show_img_" + id).addClass("hide");
                })
            }
        }

    </script>
</asp:Content>
