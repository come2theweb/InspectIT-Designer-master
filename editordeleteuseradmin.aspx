<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditOrDeleteUserAdmin.aspx.cs" Inherits="InspectIT.EditOrDeleteUserAdmin" %>
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
        <div class="panel-title">Edit User
        </div>
        <div class="pull-right">
            <div class="col-xs-12">
                <input type="text" id="search-table" class="form-control pull-right" placeholder="Search" />
            </div>
        </div>
        <div class="clearfix"></div>
        </div>
        <div class="panel-body">
       <div class="panel-body">
                <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>


                <div id="plumber">

                    <div class="row">
                        <h3>User Details</h3>
                        <div class="col-md-12">
                            <div class="form-group form-group-default">
                                <label>Name:</label>
                                <div class="controls">
                                    <asp:TextBox ID="Name" runat="server" CssClass="form-control"  ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Surname:</label>
                                <div class="controls">
                                    <asp:TextBox ID="Surname" runat="server" CssClass="form-control"  ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Email Address:</label>
                                <div class="controls">
                                    <asp:TextBox ID="Email" runat="server" CssClass="form-control"  ></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group form-group-default required">
                                <label>Password:</label>
                                <div class="controls">
                                    <asp:TextBox ID="Password" runat="server" placeholder="enter password to change" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default required">
                                <label>Password Confirm:</label>
                                <div class="controls">
                                    <asp:TextBox ID="PasswordConfirm" runat="server" placeholder="password confirm" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group form-group-default">
                    <label>Role :</label>
                    <div class="controls">
                        <asp:DropDownList ID="role" runat="server" CssClass="form-control changeRightsdd">
                            <asp:ListItem Value="Administrator">Administrator</asp:ListItem>
                            <%--<asp:ListItem Value="AdminRights">Staff</asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>
                </div>

 <div class="form-group form-group-default showHideRights" runat="server" id="Div1">
                    <label>Menu Items :</label>
                    <div class="controls">
                         <asp:CheckBoxList ID="CheckBoxList1" runat="server" >
                            <asp:ListItem Value="Administration">Administration</asp:ListItem>
                            <asp:ListItem Value="Audits">Audits</asp:ListItem>
                            <asp:ListItem Value="COCStatement">COC Statement</asp:ListItem>
                            <asp:ListItem Value="Plumbers">Plumbers</asp:ListItem>
                            <asp:ListItem Value="Resellers">Resellers</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div>
                            
                <div class="form-group form-group-default showHideRights required" runat="server" id="rightss">
                    <label>Rights :</label>
                    <div class="controls">
                         <asp:CheckBoxList ID="rights" runat="server">
                            <asp:ListItem Value="View Only">View Only</asp:ListItem>
                            <asp:ListItem Value="View and Edit">View & Edit</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div>
                             <div class="form-group form-group-default">
                    <div class="controls">
                        <div class="checkbox">
                            <asp:CheckBox ID="isActive" runat="server" Text="Active" />
                        </div>
                    </div>
                </div>
                            
                        </div>
                         
                    </div>
                    
                    <div class="row">
                        <div class="auto-style1">
                            <asp:Button ID="btnUpdatePlumber" CssClass="btn btn-primary" runat="server" Text="Update User"  style="float:right;" OnClick="btnUpdatePlumber_Click"/>
                        </div>
                    </div> 
                </div>
            </div>
        </div>
    </div>
    </div>
    <script>

        //$(".showHideRights").hide();
        $(".changeRightsdd").change(function () {
            var thisVal = $(this).val();
            //console.log($(this).val());
            if (thisVal == "AdminRights") {
                $(".showHideRights").show();
            }
            else {
                $(".showHideRights").hide();
            }
        });

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

        
        $('.datepicker-range').datepicker();
    </script>

</asp:Content>
