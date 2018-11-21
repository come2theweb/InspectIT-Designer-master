<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="companies.aspx.cs" Inherits="InspectIT.companies" %>

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
                    Companies
                </div>
                <div class="pull-right">
                    <div class="col-xs-12">
                        <div class="form-group row">
                            <div class="col-xs-10">
                                <div class="input-group">
                                    <input type="text" class="form-control" id="searchs" placeholder="Search Companies">
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

                <div class="panel">


                    <table id="tblData" class="hover">
                        <thead>
                            <tr class="gridStyle">
                                <th>Company ID</th>
                                <th>Company Name</th>
                                <th>Number of Employees Licensed</th>
                                <th>Number of Employees Non-Licensed</th>
                                <th>.</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>

                       <table class="table table-striped demo-table-search" id="stripedTabletwo">
                    <thead>
                        <tr>
                           <th>Company ID</th>
                                <th>Company Name</th>
                                <th>Number of Employees Licensed</th>
                                <th>Number of Employees Non-Licensed</th>
                                <th>.</th>
                        </tr>
                    </thead>
                    <tbody id="itemsDisp">
                    </tbody>
                </table>

                    <ul class="nav nav-tabs nav-tabs-simple hide" role="tablist">
                        <li class="active"><a href="#TabActive" data-toggle="tab" role="tab">Active</a></li>
                        <li class=""><a href="#TabArchived" data-toggle="tab" role="tab">Archived</a></li>
                    </ul>
                    <div class="tab-content hide">
                        <div class="tab-pane active" id="TabActive">
                            <div class="row">
                                <div class="col-xs-6">
                                </div>
                                <div class="col-xs-6">
                                    <input type="text" id="search-table" class="form-control pull-right" placeholder="Search table">
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <table class="table table-striped demo-table-search" id="stripedTable">
                                <thead>
                                    <tr>
                                        <th>Company ID</th>
                                        <th>Company Name</th>
                                        <th>Number of Employees Licensed</th>
                                        <th>Number of Employees Non-Licensed</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody id="displayusers" runat="server">
                                </tbody>
                            </table>
                        </div>
                        <div class="tab-pane" id="TabArchived">
                            <div class="row">
                                <div class="col-xs-12">
                                    <input type="text" id="search-table-del" class="form-control pull-right" placeholder="Search table">
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <table class="table table-striped demo-table-search" id="stripedTable_del">
                                <thead>
                                    <tr>
                                        <th>Company ID</th>
                                        <th>Company Name</th>
                                        <th>Number of Employees Licensed</th>
                                        <th>Number of Employees Non-Licensed</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody id="displayusers_del" runat="server"></tbody>
                            </table>
                        </div>
                        <asp:Button ID="addBtn" runat="server" CssClass="btn btn-primary" Text="Add" Style="float: right;" OnClick="Button1_Click" />
                    </div>

                </div>



            </div>
        </div>
        <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->
    <%--  Code fragment showing an example of invoking Ajax  --%>
    <%-- Notice that the parameters from DataTable must be encapsulated in 
     a Javascript object, and then stringified.
    Notice also that the name of the Javascript object ("paerameters" in this example
    has to match the parameter name in the WebMethod, or dotnet won't find the correct
    WebMethod and the Ajax call will kick back a 500 error.
    
    Permission to use this code for any purpose and without fee is hereby granted.
    No warrantles.

--%><link href="assets/DataTables-1.10.4/jquery.dataTables.css" rel="stylesheet" />
    <script src="assets/DataTables-1.10.4/jquery.dataTables.js"></script>


    <script>

        function searchP() {
            $("#itemsDisp").html("");
            $("#tblData_wrapper").hide();
            $("#tblData").hide();
            $("#stripedTabletwo").show();
            console.log("Handler for .change() called.");

            $.ajax({
                type: "POST",
                url: 'API/WebService1.asmx/GetTableDataCompaniesSearched',
                data: { roleId: $("#searchs").val() },
                success: function (data) {
                    console.log("Handler for .change() called. :::: " + data);
                    $("#itemsDisp").append(data);

                },
            });
        }

        $(document).ready(function () {
            $.ajaxSetup({
                cache: false
            });

            function showDetails() {
                alert("showing some details");
            }

            var table = $('#tblData').DataTable({
                "filter": false,
                //"pagingType": "simple_numbers",
                "orderClasses": false,
                "order": [[0, "asc"]],
                "info": false,
                "scrollY": "500px",
                "scrollCollapse": true,
                "bProcessing": true,
                "bServerSide": true,
                "sAjaxSource": "API/WebService1.asmx/GetTableData",
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
                            $("#tblData").show();
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

        function showDetails() {
            //so something funky with the data
        }

        function deleteconf(url) {
            var result = confirm("Are you sure?");
            if (result) {
                document.location.href = url;
            }
        }
        //var responsiveHelper = undefined;
        //var breakpointDefinition = {
        //    tablet: 1024,
        //    phone: 480
        //};

        //var table = $('#stripedTable');
        //var table1 = $('#stripedTable_del');

        //var settings = {
        //    "sDom": "<'table-responsive't><'row'<p i>>",
        //    "sPaginationType": "bootstrap",
        //    "destroy": true,
        //    "scrollCollapse": true,
        //    "oLanguage": {
        //        "sLengthMenu": "_MENU_ ",
        //        "sInfo": "Showing <b>_START_ to _END_</b> of _TOTAL_ entries"
        //    },
        //    "iDisplayLength": 50
        //};

        //table.dataTable(settings);
        //table1.dataTable(settings);

        //// search box for table
        //$('#search-table').keyup(function () {
        //    table.fnFilter($(this).val());
        //});

        //$('#search-table-del').keyup(function () {
        //    table1.fnFilter($(this).val());
        //});

        //api/WebService1.asmx/getCompanyDataTableLoading

    </script>

</asp:Content>
