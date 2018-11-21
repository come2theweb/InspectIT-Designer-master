<%@ Page Title="Forms" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Forms.aspx.cs" Inherits="InspectIT.Forms" %>

<%@ Register Src="~/breadcrumbs.ascx" TagPrefix="uc1" TagName="breadcrumbs" %>
<%@ Register Src="~/quickview.ascx" TagPrefix="uc1" TagName="quickview" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">  
    <!-- START PAGE CONTENT WRAPPER -->
      <div class="page-content-wrapper">
        <!-- START PAGE CONTENT -->
        <div class="content">
          <!-- START JUMBOTRON -->
          <div class="jumbotron" data-pages="parallax">
            <div class="container-fluid container-fixed-lg">
              <div class="inner">
                <uc1:breadcrumbs runat="server" ID="breadcrumbs" />
              </div>
            </div>
          </div>
          <!-- END JUMBOTRON -->
          <!-- START CONTAINER FLUID -->
          <div class="container-fluid container-fixed-lg">
            <!-- BEGIN PlACE PAGE CONTENT HERE -->
              <div id="successbox" runat="server" class="alert alert-success" visible="false"></div>
              <div class="panel panel-transparent ">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs nav-tabs-fillup">
                    <li class="active">
                        <a data-toggle="tab" href="#tab-fillup1"><span class="titlenew">Active</span></a>
                    </li>
                    <li>
                        <a data-toggle="tab" href="#tab-fillup2"><span class="titlenew">Archived</span></a>
                    </li>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">
                    <div class="tab-pane active" id="tab-fillup1">
                        <div class="row">
                        <div class="col-md-12">

                            <!-- START PANEL -->
                            <div class="panel panel-default">
                                <div class="panel-heading">Your Available forms
                                <div class="pull-right">
                                    <div class="col-xs-12">
                                    <input type="text" id="search-table-managers" class="form-control pull-right" placeholder="Search">
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                </div>
                                <div class="panel-body">
                                <table class="table table-striped" id="tableManagers">
                                    <thead>
                                    <tr>
                                        <th>Form Name</th>
                                        <th>Sites</th>
                                        <th>Type</th>
                                        <th></th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                        <div ID="ManagersList" runat="server"></div>
                                    </tbody>
                                </table>
                                </div>
                                <div class="pull-right">
                                    <button type="button" class="btn btn-primary m-t-10" onclick="document.location.href='AddForm'">Add Generic Form</button>
                                    <button type="button" class="btn btn-primary m-t-10" onclick="document.location.href='AddFormCustom'">Add Custom Form</button>
                                    <button type="button" class="btn btn-primary m-t-10" onclick="document.location.href='OrderForm'">Order Forms</button>
                                </div>
                            </div>
                            <!-- END PANEL -->

                        </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="tab-fillup2">
                        <div class="row">
                        <div class="col-md-12">

                            <!-- START PANEL -->
                <div class="panel panel-default">
                    <div class="panel-heading">Forms
                    <div class="pull-right">
                        <div class="col-xs-12">
                        <input type="text" id="search-table-managers1" class="form-control pull-right" placeholder="Search">
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    </div>
                    <div class="panel-body">
                    <table class="table table-striped" id="tableManagers1">
                        <thead>
                        <tr>
                            <th>Form Name</th>
                            <th>Sites</th>
                            <th>Type</th>
                            <th></th>
                        </tr>
                        </thead>
                        <tbody>
                            <div ID="ManagersList_del" runat="server"></div>
                        </tbody>
                    </table>
                    </div>
                </div>
                <!-- END PANEL -->

                        </div>
                        </div>
                    </div>
                    </div>
                </div>
                              
            <!-- END PLACE PAGE CONTENT HERE -->
          </div>
          <!-- END CONTAINER FLUID -->
        </div>
        <!-- END PAGE CONTENT -->
    
    <!--START QUICKVIEW -->
    <uc1:quickview runat="server" ID="quickview" />
    <!-- END QUICKVIEW-->

    <!-- BEGIN VENDOR JS -->
    <script src="assets/plugins/pace/pace.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="assets/plugins/modernizr.custom.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="assets/plugins/boostrapv3/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery/jquery-easy.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-unveil/jquery.unveil.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-bez/jquery.bez.min.js"></script>
    <script src="assets/plugins/jquery-ios-list/jquery.ioslist.min.js" type="text/javascript"></script>
    <script src="assets/plugins/imagesloaded/imagesloaded.pkgd.min.js"></script>
    <script src="assets/plugins/jquery-actual/jquery.actual.min.js"></script>
    <script src="assets/plugins/jquery-scrollbar/jquery.scrollbar.min.js"></script>
    <script src="assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
    <!-- END VENDOR JS -->
    <!-- BEGIN CORE TEMPLATE JS -->
    <script src="pages/js/pages.js" type="text/javascript"></script>
    <!-- END CORE TEMPLATE JS -->
    <!-- BEGIN PAGE LEVEL JS -->
    <script src="assets/js/scripts.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL JS -->     
    <script> 

        (function ($) {

            'use strict';

            var responsiveHelper = undefined;
            var breakpointDefinition = {
                tablet: 1024,
                phone: 480
            };

            // Initialize datatable showing a search box at the top right corner
            var initTableWithSearch = function () {
                var table = $('#tableManagers1');

                var settings = {
                    "sDom": "<'table-responsive't><'row'<p i>>",
                    "sPaginationType": "bootstrap",
                    "destroy": true,
                    "scrollCollapse": true,
                    "oLanguage": {
                        "sLengthMenu": "_MENU_ ",
                        "sInfo": "Showing <b>_START_ to _END_</b> of _TOTAL_ entries"
                    },
                    "iDisplayLength": 10
                };

                table.dataTable(settings);

                // search box for table
                $('#search-table-managers1').keyup(function () {
                    table.fnFilter($(this).val());
                });
            }

            var initTableManagers = function () {
                var table = $('#tableManagers');

                var settings = {
                    "sDom": "<'table-responsive't><'row'<p i>>",
                    "sPaginationType": "bootstrap",
                    "destroy": true,
                    "scrollCollapse": true,
                    "oLanguage": {
                        "sLengthMenu": "_MENU_ ",
                        "sInfo": "Showing <b>_START_ to _END_</b> of _TOTAL_ entries"
                    },
                    "iDisplayLength": 10
                };

                table.dataTable(settings);

                // search box for table
                $('#search-table-managers').keyup(function () {
                    table.fnFilter($(this).val());
                });
            }

            
            initTableManagers();
            initTableWithSearch();

        })(window.jQuery);

        function deleteconf(url) {
            var result = confirm("Want to archive?");
            if (result) {
                document.location.href = url;
            }
        }

    </script>
</asp:Content>
