<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddCertificate.aspx.cs" Inherits="InspectIT.AddCertificate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="assets/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <link href="pages/css/pages.css" rel="stylesheet" />
    <link href="pages/css/ie9.css" rel="stylesheet" />
    <link href="pages/css/pages.min.css" rel="stylesheet" />
    <link href="pages/css/themes/abstract.css" rel="stylesheet" />
    <style>
        h4{
            font-weight:bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- START CONTAINER FLUID -->
    <div class="container-fluid container-fixed-lg bg-white">
    <!-- START PANEL -->
    <div class="panel panel-transparent">
        <div class="panel-heading">
        <div class="panel-title">Allocate Certificates</div>
        <div class="clearfix"></div>
        </div>
        <div class="panel-body">
       
            <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
            <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>

            <!-- First Row -->
            <div class="row">
                <h4>Certificate Details</h4>
                <!-- Second Column -->
                <div class="col-md-12">

                    <!-- START Form Control-->
                        <div class="form-group form-group-default required">
                            <label>Current Range will start at:</label>
                            <div class="controls">
                                <asp:Label ID="StartRange" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    <!-- END Form Control-->

                    <!-- START Form Control-->
                        <div class="form-group form-group-default required">
                            <label>Amount of Certificates:</label>
                            <div class="controls">
                                <asp:TextBox ID="CetificateNumber" TextMode="Number" runat="server" placeholder="Enter the amount of certificates" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                    <!-- END Form Control-->

                    <!-- START Form Control-->
                        <div class="form-group form-group-default required">
                            <label>Current Range will end at:</label>
                            <div class="controls">
                                <asp:Label ID="EndRange" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    <!-- END Form Control-->

                    <!-- START Form Control-->
                        <div class="form-group form-group-default">
                            <div class="btn-group pull-right">
                                <asp:Button ID="btnGenerate" runat="server" Text="Generate Certificates" CssClass="btn btn-primary" OnClick="btnGenerate_Click" />
                            </div>
                        </div>
                    <!-- END Form Control-->

                </div>
    
            </div>
            <!-- / First Row -->


        </div>
    </div>
    <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->
    <script>

        $("#<%=CetificateNumber.ClientID%>").change(function () {
            var sRange = $("#<%=StartRange.ClientID%>").html();
            var CurNo = $("#<%=CetificateNumber.ClientID%>").val();

            var eRange = (parseInt(sRange) + parseInt(CurNo));
            $("#<%=EndRange.ClientID%>").html(eRange);
            
        });

    </script>

</asp:Content>
