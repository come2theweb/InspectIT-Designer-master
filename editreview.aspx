<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditReview.aspx.cs" Inherits="InspectIT.EditReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/lodash.min.js"></script>

    <%--<script type="text/javascript" language="javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
 </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid container-fixed-lg bg-white">
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div class="panel-title">
                    Edit Review
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">

                <div class="form-group form-group-default">
                    <asp:RadioButton ID="rdFail" runat="server" Text="Failure" GroupName="radiosel" CssClass="form-control radio radio-danger" />
                    <asp:RadioButton ID="rdWarning" runat="server" Text="Cautionary" GroupName="radiosel" CssClass="form-control radio radio-warning" />
                    <asp:RadioButton ID="rdSuccess" runat="server" Text="Compliment" GroupName="radiosel" CssClass="form-control radio radio-success" />
                </div>

                <div class="form-group form-group-default hide">
                    <label>Review Template :</label>
                    <div class="controls">
                        <asp:DropDownList ID="ReviewTemplate" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ReviewTemplate_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Installation Type :</label>
                    <div class="controls">
                        <asp:DropDownList ID="TypeOfInstallation" runat="server" CssClass="form-control" OnSelectedIndexChanged="TypeOfInstallation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>

               <div class="form-group form-group-default">
                    <label>Sub Type :</label>
                    <div class="controls">
                        <asp:DropDownList ID="subTypes" runat="server" CssClass="form-control" ></asp:DropDownList>
                    </div>
                </div>

                  <div class="form-group form-group-default">
                    <label>Question :</label>
                    <div class="controls">
                        <asp:TextBox ID="question" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:DropDownList ID="FormFields" runat="server" CssClass="form-control hide"></asp:DropDownList>
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
                        <asp:TextBox ID="Comment" runat="server" placeholder="Enter Your comment" TextMode="MultiLine" Height="120px" CssClass="form-control" required></asp:TextBox>

                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Comment Files/Pictures :</label>
                    <div class="controls">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <div class="pull-right"><asp:Button ID="btnUpload" CssClass="btn btn-primary" runat="server" Text="Upload" OnClick="btnUpload_Click" formnovalidate /></div>
                        <div id="CurrentMedia" runat="server"></div>
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Reference :</label>
                    <div class="controls">
                        <asp:TextBox ID="Reference" runat="server" onkeydown="return false;" placeholder="Enter Your comment" TextMode="MultiLine" Height="120px" CssClass="form-control" ></asp:TextBox>

                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label> Reference Files/Pictures :</label>
                    <div class="controls">
                        <asp:FileUpload ID="img" runat="server" CssClass="hide" />
                        <div class="pull-right hide"><asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Upload" OnClick="btnUpload_Click" formnovalidate /></div>
                        <div id="Div1" runat="server"></div>
                    </div>
                </div>
                
                <div class="row">
                    <div class="text-right">
                        <%--<asp:Button ID="btnRefixComplete" CssClass="btn btn-success" runat="server" Text="Refix Completed" OnClick="btnRefixComplete_Click" />--%>
                         <asp:Button ID="btn_cancel" CssClass="btn btn-danger" runat="server" Text="Cancel Review" OnClick="btn_cancel_Click" />
                        <asp:Button ID="btn_add" CssClass="btn btn-primary" runat="server" Text="Save Review" OnClick="btn_add_Click" />
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>

        function deleteImage(id,pid,rid) {
            var result = confirm("Are you sure?");
            if (result) {
                document.location.href = "DeleteItems.aspx?op=delReferenceImginsaa&id=" + id + "&pid=" + pid + "&rid=" + rid;
            }
        }

        $(document).ready(function () {
            window.history.pushState(null, "", window.location.href);
            window.onpopstate = function () {
                window.history.pushState(null, "", window.location.href);
                alert("Please save or cancel the review with the below buttons before leaving the page!");
            };
        });
    </script>

</asp:Content>
