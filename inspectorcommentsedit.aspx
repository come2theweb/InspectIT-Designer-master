<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="InspectorCommentsEdit.aspx.cs" Inherits="InspectIT.InspectorCommentsEdit" %>

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
                    Edit Statement
       
                </div>
                <div class="pull-right">
                    <div class="col-xs-12">
                        
                    </div>
                </div>
                <div class="clearfix"></div>
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
                        <asp:DropDownList ID="subTypes" runat="server" CssClass="form-control" OnSelectedIndexChanged="subTypes_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <asp:DropDownList ID="FormFields" runat="server" CssClass="form-control hide"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Review Name :</label>
                    <div class="controls">
                        <asp:TextBox ID="Name" runat="server" placeholder="Enter Your Comment Name" CssClass="form-control" required></asp:TextBox>
                    </div>
                </div>
                
                <div class="form-group form-group-default">
                    <label>Statement :</label>
                    <div class="controls">
                        <asp:TextBox ID="question" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Comment :</label>
                    <div class="controls">
                        <asp:TextBox ID="Comment" runat="server" placeholder="Enter Your comment" TextMode="MultiLine" Height="120px" CssClass="form-control" required></asp:TextBox>

                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Comment Files/Pictures :</label>
                    <div class="controls">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <div class="pull-right"><asp:Button ID="btnUpload" CssClass="btn btn-primary" runat="server" Text="Upload" OnClick="btnUpload_Click" formnovalidate /></div>
                       <div id="Div1" runat="server"></div>
                    </div>
                </div>
                
                  <div class="form-group form-group-default">
                    <label>Reference :</label>
                    <div class="controls">
                        <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Your Reference" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Reference Files/Pictures :</label>
                    <div class="controls">
                        <asp:FileUpload ID="FileUpload2" runat="server" />
                        <div class="pull-right"><asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Upload" formnovalidate OnClick="Button1_Click" /></div>
                        
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


                <div class="row">
                    <div class="auto-style1">
                        <asp:Button ID="btn_add" CssClass="btn btn-primary" runat="server" Text="Update Statement" OnClick="btn_add_Click" Style="float: right;" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>

        function deleteImage(id, pid) {
            var result = confirm("Are you sure?");
            if (result) {
                document.location.href = "DeleteItems.aspx?op=delCommentImgins&id=" + id + "&pid=" + pid;
            }
        }

        function deleteImagea(id, pid) {
            var result = confirm("Are you sure?");
            if (result) {
                document.location.href = "DeleteItems.aspx?op=delReferenceImgins&id=" + id + "&pid=" + pid;
            }
        }

        
    </script>

</asp:Content>
