<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewPlumbers.aspx.cs" Inherits="InspectIT.ViewPlumbers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/css/icons/icomoon/styles.css" rel="stylesheet" type="text/css">
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/bootstrap_limitless.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/layout.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/components.min.css" rel="stylesheet" type="text/css">
    <link href="assets/css/colors.min.css" rel="stylesheet" type="text/css">
    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/lodash.min.js"></script>
    <script src="assets/js/main/jquery.min.js"></script>
    <script src="assets/js/main/bootstrap.bundle.min.js"></script>
    <script src="assets/js/plugins/loaders/blockui.min.js"></script>
    <!-- /core JS files -->

    <!-- Theme JS files -->
    <script src="assets/js/plugins/forms/styling/uniform.min.js"></script>
    <script src="assets/js/plugins/forms/styling/switchery.min.js"></script>
    <script src="assets/js/plugins/forms/inputs/touchspin.min.js"></script>

    <script src="assets/js/app.js"></script>
    <script src="assets/js/demo_pages/form_input_groups.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- START CONTAINER FLUID -->
    <div class="container-fluid container-fixed-lg bg-white">
        <!-- START PANEL -->
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div class="panel-title">
                    Plumber Details
                </div>
                <div class="pull-right">
                    <div class="col-xs-12">
                        <div class="form-group row">
                            <div class="col-xs-10">
                                <div class="input-group">
                                    <input type="text" class="form-control" id="searchs" placeholder="Search Plumbers">
                                    <span class="input-group-append"></span>
                                </div>
                            </div>
                            <div class="col-xs-2">
                                <button class="btn btn-sm btn-primary" type="button" onclick="searchP()"><i class="fa fa-search"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>
                <table class="table table-striped demo-table-search" id="stripedTable">
                    <thead>
                        <tr>
                            <th>Reg No</th>
                            <th>Name</th>
                            <th>Surname</th>
                            <th>Designation</th>
                            <th>Email Address</th>
                            <th>Password(PIN)</th>
                            <th>Status</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>

                <table class="table table-striped demo-table-search" id="stripedTabletwo">
                    <thead>
                        <tr>
                            <th>Reg No</th>
                            <th>Name</th>
                            <th>Surname</th>
                            <th>Designation</th>
                            <th>Email Address</th>
                            <th>Password(PIN)</th>
                            <th>Status</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody id="itemsDisp">
                    </tbody>
                </table>


                <!-- START Form Control-->
                <div class="row hide">
                    <div class="auto-style1">
                        <a href="AddUser.aspx">
                            <input type="button" value="Add New User" class="btn btn-primary" style="float: right;" />
                        </a>
                    </div>
                </div>
                <!-- END Form Control-->
            </div>
        </div>
        <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->
    
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script>
        function deleteconf(url) {
            var result = confirm("Are you sure?");
            if (result) {
                document.location.href = url;
            }
        }

        //$('#stripedTable').DataTable();



        $(document).ready(function () {
            $("#stripedTabletwo").hide();
            $.ajaxSetup({
                cache: false
            });

            function showDetails() {
                alert("showing some details");
            }

            var table = $('#stripedTable').DataTable({
                "filter": false,
                //"pagingType": "simple_numbers",
                "orderClasses": false,
                "order": [[0, "asc"]],
                "info": false,
                "scrollY": "500px",
                "scrollCollapse": true,
                "bProcessing": true,
                "bServerSide": true,
                "sAjaxSource": "API/WebService1.asmx/GetTableDataUsers",
                "fnServerData": function (sSource, aoData, fnCallback) {
                    aoData.push({ "name": "roleId", "value": "admin" });
                    $.ajax({
                        "dataType": 'json',
                        "contentType": "application/json; charset=utf-8",
                        "type": "GET",
                        "url": sSource,
                        "data": aoData,
                        "success": function (msg) {
                            var json = jQuery.parseJSON(msg.d);
                            fnCallback(json);
                            $("#stripedTable").show();
                            console.log("stuff :: " + json);
                        },
                        error: function (xhr, textStatus, error) {
                            if (typeof console == "object") {
                                console.log(xhr.status + "," + xhr.responseText + "," + textStatus + "," + error);
                            }
                        }
                    });
                },
                fnDrawCallback: function () {
                    $('.image-details').bind("click", showDetails);
                }
            });
        })

        function searchP() {
            $("#itemsDisp").html("");
            $("#stripedTable_wrapper").hide();
            $("#stripedTable").hide();
            $("#stripedTabletwo").show();
            console.log("Handler for .change() called.");

            $.ajax({
                type: "POST",
                url: 'API/WebService1.asmx/GetTableDataUsersSearched',
                data: { roleId: $("#searchs").val() },
                success: function (data) {
                    console.log("Handler for .change() called. :::: " + data);
                    $("#itemsDisp").append(data);

                },
            });
        }



    </script>

</asp:Content>
