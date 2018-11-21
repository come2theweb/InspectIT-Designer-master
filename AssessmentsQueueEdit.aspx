<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AssessmentsQueueEdit.aspx.cs" Inherits="InspectIT.AssessmentsQueueEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="https://fonts.googleapis.com/css?family=Roboto:400,300,100,500,700,900" rel="stylesheet" type="text/css">
    <link href="assets/css/icons/icomoon/styles.css" rel="stylesheet" type="text/css">
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/bootstrap_limitless.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/layout.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/components.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/colors.min.css" rel="stylesheet" type="text/css">


    <script src="assets/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js" type="text/javascript"></script>



    <script src="assets/js/plugins/ui/moment/moment.min.js"></script>
    <script src="assets/js/plugins/pickers/daterangepicker.js"></script>
    <script src="assets/js/plugins/pickers/anytime.min.js"></script>
    <script src="assets/js/plugins/pickers/pickadate/picker.js"></script>
    <script src="assets/js/plugins/pickers/pickadate/picker.date.js"></script>
    <script src="assets/js/plugins/pickers/pickadate/picker.time.js"></script>
    <script src="assets/js/plugins/pickers/pickadate/legacy.js"></script>
    <script src="assets/js/plugins/notifications/jgrowl.min.js"></script>

    <link href="pages/css/pages.css" rel="stylesheet" />
    <link href="pages/css/ie9.css" rel="stylesheet" />
    <link href="pages/css/pages.min.css" rel="stylesheet" />
    <link href="pages/css/themes/abstract.css" rel="stylesheet" />
    <style>
       .pickatime{
           top:;
       }
        h4 {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- START CONTAINER FLUID -->
    <div class="container-fluid container-fixed-lg bg-white">
        <!-- START PANEL -->
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div class="panel-title">
                    Assessment Edit
                </div>
                <div class="pull-right">
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="alert alert-danger" runat="server" id="checkBxErr"></div>
                        <div class="form-group  required">
                            <label>Reg No :</label>
                            <div class="controls">
                                <div class="col-md-6">
                                    <asp:TextBox ID="regno" runat="server" CssClass="form-control" required></asp:TextBox>
                                    <asp:TextBox ID="uids" runat="server" CssClass="form-control hide"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="searchuid_Click" formnovalidate="formnovalidate" />
                                </div>
                                <span class="label label-danger" runat="server" id="Span1"></span>
                            </div>
                        </div>

                        <div class="form-group ">
                            <label>Name :</label>
                            <div class="controls">
                                <asp:TextBox ID="name" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group ">
                            <label>Surname :</label>
                            <div class="controls">
                                <asp:TextBox ID="surname" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group ">
                            <label>Cell No :</label>
                            <div class="controls">
                                <asp:TextBox ID="cell" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group ">
                            <label>Email Address :</label>
                            <div class="controls">
                                <asp:TextBox ID="email" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group ">
                            <label>Assessment Type :</label>
                            <div class="controls">
                                <asp:DropDownList ID="AssessType" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <h3>Assessment Booking Details</h3>
                        <div class="form-group ">
                            <label>Date :</label>
                            <div class="controls">
                                <asp:TextBox ID="Date" runat="server" CssClass="form-control datepicker-range"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group ">
                            <label>Time :</label>
                            <div class="controls">
                                <asp:TextBox ID="Time" runat="server" CssClass="form-control pickatime"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group ">
                            <label>Location :</label>
                            <div class="controls">
                                <asp:DropDownList ID="locations" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="form-group ">
                            <label>OTP :</label>
                            <div class="controls">
                                <asp:TextBox ID="OTP" runat="server" CssClass="form-control "></asp:TextBox>
                            </div>
                        </div>

                        <h3>Assessment Results</h3>

                        <div class="form-group ">
                            <label>Result :</label>
                            <div class="controls">
                                <asp:DropDownList ID="Result" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group ">
                            <label>Result % :</label>
                            <div class="controls">
                                <asp:TextBox ID="ResultPercent" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group ">
                            <label>Attachment :</label>
                            <div class="controls">
                                <asp:FileUpload ID="img" CssClass="form-control" runat="server" />
                                <asp:TextBox ID="imgName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Image ID="Image1" runat="server" />
                            </div>
                        </div>

                        <div class="form-group ">
                            <label>Comments :</label>
                            <div id="dispCommsAssess" runat="server"></div>
                            <div class="controls">
                                <asp:TextBox ID="CommentsActivity" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control"></asp:TextBox>

                                <asp:Button ID="add_comm" CssClass="btn btn-primary" runat="server" Text="Add Comment" OnClick="add_comm_Click" formnovalidate />
                            </div>
                        </div>
                        <div class="form-group ">
                            <div class="form-check form-check-inline">
                                <label class="form-check-label">
                                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="form-check-input-styled" />
                                    New card Required
                                </label>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="auto-style1">
                            <asp:Button ID="btn_add" CssClass="btn btn-primary" runat="server" Style="float: right;" Text="Save" OnClick="btn_add_Click" />
                            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Style="float: right;" Text="Save" OnClick="btn2_add_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <script>

        $('.datepicker-range').datepicker();

        $('.pickatime').pickatime();
        
        $(".pickatime").css("top","");
    </script>

</asp:Content>
