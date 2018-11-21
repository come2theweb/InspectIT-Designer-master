<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddReview.aspx.cs" Inherits="InspectIT.AddReview" %>

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
                    Add Review
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                <div class="alert alert-success">Please select the status to continue filling in a review</div>
                <div class="form-group form-group-default">
                    <asp:RadioButton ID="rdFail" runat="server" Text="Failure" GroupName="radiosel" CssClass="form-control radio radio-danger" OnCheckedChanged="rdFail_CheckedChanged1" AutoPostBack="true" />
                    <asp:RadioButton ID="rdWarning" runat="server" Text="Cautionary" GroupName="radiosel" CssClass="form-control radio radio-warning" OnCheckedChanged="rdFail_CheckedChanged1" AutoPostBack="true"  />
                    <asp:RadioButton ID="rdSuccess" runat="server" Text="Compliment" GroupName="radiosel" CssClass="form-control radio radio-success" OnCheckedChanged="rdFail_CheckedChanged1" AutoPostBack="true"  />
                </div>

                <div class="row" runat="server" id="commentPanel" visible="false">
                    <div class="form-group form-group-default">
                    <label>Comment :</label>
                    <div class="controls">
                        <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Your comment" TextMode="MultiLine" Height="120px" CssClass="form-control" ></asp:TextBox>

                    </div>
                </div>
                     <div class="row">
                    <div class="text-right">
                        
                        <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="Add Review" OnClick="btn_add_Click" />
                        
                    </div>
                </div>
                </div>

                <div class="row" runat="server" id="hidePanel" visible="false">
                <div class="form-group form-group-default">
                    <label>Review Template : (Choose your own questions or leave empty if you want to work with an Administartor template)</label>
                    <div class="controls">
                        <asp:DropDownList ID="ReviewTemplate" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ReviewTemplate_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Installation Type :</label>
                    <div class="controls">
                        <asp:DropDownList ID="TypeOfInstallation" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="TypeOfInstallation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Sub Type :</label>
                    <div class="controls">
                        <asp:DropDownList ID="subTypes" runat="server" CssClass="form-control" OnSelectedIndexChanged="subTypes_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>

                  <div class="form-group form-group-default">
                    <label>Question :</label>
                    <div class="controls">
                        <asp:TextBox ID="question" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                        <asp:DropDownList ID="FormFields" runat="server" CssClass="form-control" Visible="false" OnSelectedIndexChanged="FormFields_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group form-group-default hide">
                    <label>Review Name :</label>
                    <div class="controls">
                        <asp:TextBox ID="Name" runat="server" placeholder="Enter Your Comment Name" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
                
                <div class="form-group form-group-default">
                    <label>Comment :</label>
                    <div class="controls">
                        <asp:TextBox ID="Comment" runat="server" placeholder="Enter Your comment" TextMode="MultiLine" Height="120px" CssClass="form-control" ></asp:TextBox>

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
                        <asp:TextBox ID="Reference" runat="server" onkeydown="return false;" placeholder="Enter Your comment" TextMode="MultiLine" Height="120px" CssClass="form-control keypressPrevent"  ></asp:TextBox>

                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label> Reference Files/Pictures :</label>
                    <div class="controls">
                        <asp:FileUpload ID="img" runat="server" CssClass="hide" />
                        <div class="pull-right hide"><asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Upload" OnClick="btnUploads_Click" formnovalidate /></div>
                        <div id="Div1" runat="server"></div>
                    </div>
                </div>
                
                    <div class="form-group form-group-default">
                    <label> Point Allocation :</label>
                    <div class="controls">
                        <asp:TextBox ID="pointAllocation" runat="server" placeholder="" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="text-right">
                        
                        <asp:Button ID="btn_add" CssClass="btn btn-primary" runat="server" Text="Add Review" OnClick="btn_add_Click" />
                        
                    </div>
                </div>
                    </div>
            </div>
        </div>
    </div>
    <script>

        //function deleteImage(id) {
        //    if (confirm('Are you sure?')) {
        //        $.post('https://197.242.82.242/inspectit/api/frmImgDel?imgid=' + id, { }, function (data) {
        //            $("#show_img_" + id).addClass("hide");
        //        })
        //    }
        //}
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
        
        var revVal = $("#<%=ReviewTemplate.ClientID%>").val();

        function deleteImagea(id,pid) {
            var result = confirm("Are you sure?");
            if (result) {
                document.location.href = "DeleteItems.aspx?op=delRessferenceImgins&id=" + id + "&pid=" + pid + "&v=" + revVal;
            }
        }

        function deleteImage(id, pid) {
            var result = confirm("Are you sure?");
            if (result) {
                document.location.href = "DeleteItems.aspx?op=delImgAddReview&id=" + id + "&pid=" + pid + "&v=" + revVal;
            }
        }

        $(".select2").select2();

     <%--   $('#<%=Reference%>').keydown(function (event) {
            if (key.charCode >= 0 && key.charCode <= 222) return false;
            //carry on...

             console.log("keypress");
        });--%>
    </script>

</asp:Content>
