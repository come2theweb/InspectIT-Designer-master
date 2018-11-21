<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewAssessmentCategories.aspx.cs" Inherits="InspectIT.ViewAssessmentCategories" %>
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

    <!-- START CONTAINER FLUID -->
    <div class="container-fluid container-fixed-lg bg-white">
    <!-- START PANEL -->
    <div class="panel panel-transparent">
        <div class="panel-heading">
        <div class="panel-title">View Users
        </div>
        <div class="pull-right">
            <div class="col-xs-12">
                <input type="text" id="search-table" class="form-control pull-right" placeholder="Search">
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
                <th>Category ID</th>
                <th>Name</th>
                <th></th>
            </tr>
            </thead>
            <tbody  id="displayusers" runat="server">
            
            </tbody>
        </table>
        <!-- START Form Control-->
            <div class="row">
                <div class="auto-style1">
                    <a href="AddCategory.aspx">
                        <<%--input type="button" value="Add New Category" Class="btn btn-primary"/>--%>
                        
                    </a>
                </div>
            </div> <label class="btn btn-primary" data-target="#modalSlideUp" data-toggle="modal" style="float:right;" >Add New Category</label>
            <%--<label class="btn btn-success" data-target="#modalSlideUpSub" data-toggle="modal" style="float:right;" >Add SubCategory</label>--%>
        <!-- END Form Control-->

            <!-- Modal -->
            <div class="modal fade slide-up disable-scroll" id="modalSlideUp" tabindex="-1" role="dialog" aria-hidden="false">
              <div class="modal-dialog ">
                <div class="modal-content-wrapper">
                  <div class="modal-content">
                    <div class="modal-header clearfix text-left">
                      <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><i class="pg-close fs-14"></i>
                      </button>
                      <h5>Main <span class="semi-bold">Category</span></h5>
                    </div>
                    <div class="modal-body">
                      <form role="form">
                        <div class="form-group-attached">
                          <div class="row">
                            <div class="col-sm-12">
                              <div class="form-group form-group-default">
                                <label>Category Name</label>
                                    <asp:TextBox ID="CategoryName" CssClass="form-control" runat="server"></asp:TextBox>
                              </div>
                            </div>
                          </div>
                        </div>
                      </form>
                      <div class="row">
                        
                        <div class="col-sm-4 m-t-10 sm-m-t-10" style="float:right;">
                            <asp:Button ID="addCategory" CssClass="btn btn-primary btn-block m-t-5" runat="server" Text="Add" OnClick="addCategory_Click" />
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- /.modal-content -->
              </div>
            </div>
            <!-- /.modal-dialog -->

            <!-- Modal -->
            <div class="modal fade slide-up disable-scroll" id="modalSlideUpEdit" tabindex="-1" role="dialog" aria-hidden="false">
              <div class="modal-dialog ">
                <div class="modal-content-wrapper">
                  <div class="modal-content">
                    <div class="modal-header clearfix text-left">
                      <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><i class="pg-close fs-14"></i>
                      </button>
                      <h5>Edit <span class="semi-bold">Category</span></h5>
                    </div>
                    <div class="modal-body">
                      <form role="form">
                        <div class="form-group-attached">
                          <div class="row">
                            <div class="col-sm-12">
                              <div class="form-group form-group-default">
                                <label>Edit Category Name</label>
                                  <div style="display:none;"><asp:TextBox ID="EditCategoryID" runat="server" CssClass="form-control"></asp:TextBox ></div>
                                    <asp:TextBox ID="EditCategoryName" CssClass="form-control" runat="server"></asp:TextBox>
                              </div>
                            </div>
                          </div>
                        </div>
                      </form>
                      <div class="row">
                        
                        <div class="col-sm-4 m-t-10 sm-m-t-10" style="float:right;">
                            <asp:Button ID="ditCategory" CssClass="btn btn-primary btn-block m-t-5" runat="server" Text="Update" OnClick="editCategory_Click" />
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- /.modal-content -->
              </div>
            </div>
            <!-- /.modal-dialog -->


        </div>
    </div>
    <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->

    <script>

        function editrole(catID, catName) {
            $("#<%=EditCategoryID.ClientID%>").val(catID);
            $("#<%=EditCategoryName.ClientID%>").val(catName);
            $("#modalSlideUpEdit").modal();
        }

        function deleteconf(url) {
            var result = confirm("Are you sure?");
            if (result) {
                document.location.href = url;
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
