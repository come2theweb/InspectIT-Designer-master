<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddSupplier.aspx.cs" Inherits="InspectIT.AddSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/lodash.min.js"></script>
    <style>
        h4 {
            font-weight: bold;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid container-fixed-lg bg-white">
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div class="panel-title">
                    Add Reseller
       
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

                <div class="row">
                    
                    <div class="col-md-6">
                        <h4>Reseller Details</h4>
                        <div class="form-group form-group-default required">
                            <label>Reseller Name :</label>
                            <div class="controls">
                                <asp:TextBox ID="SupplierName" runat="server" placeholder="Enter Reseller Name" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Reseller Reg No :</label>
                            <div class="controls">
                                <asp:TextBox ID="SupplierRegNo" runat="server" placeholder="Enter Your Reseller Registration Number" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default hide">
                            <label>Website :</label>
                            <div class="controls">
                                <asp:TextBox ID="SupplierWebsite" runat="server" placeholder="Enter Reseller Website" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Reseller Email :</label>
                            <div class="controls">
                                <asp:TextBox ID="SupplierEmail" runat="server" placeholder="Enter Reseller Email Address" TextMode="Email" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Reseller Contact Number :</label>
                            <div class="controls">
                                <asp:TextBox ID="SupplierContactNo" runat="server" placeholder="Enter Reseller Contact Number" CssClass="form-control phone" required></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <h4>Reseller Contact Details</h4>
                        <div class="form-group form-group-default required">
                            <label>Name :</label>
                            <div class="controls">
                                <asp:TextBox ID="fName" runat="server" placeholder="Enter Reseller Name" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Surname :</label>
                            <div class="controls">
                                <asp:TextBox ID="lName" runat="server" placeholder="Enter Reseller Name" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Email :</label>
                            <div class="controls">
                                <asp:TextBox ID="email" runat="server" placeholder="Enter Reseller Email Address" TextMode="Email" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Password:</label>
                            <div class="controls">
                                <asp:TextBox ID="Password" runat="server" placeholder="Password" CssClass="form-control" TextMode="Password" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Password Confirm:</label>
                            <div class="controls">
                                <asp:TextBox ID="PasswordConfirm" runat="server" placeholder="password" CssClass="form-control" TextMode="Password" required></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                              <h5>COC Overview</h5>

                            <div class="form-group form-group-default">
                                <label>Number of COC's able to purchase:</label>
                                <div class="controls">
                                    <asp:TextBox ID="nonloggedcocallocated" runat="server" CssClass="form-control"  ></asp:TextBox>
                                </div>
                            </div>
 </div>

                    <div class="col-md-6">
                        <h5>Physical Address</h5>
                        <div class="form-group form-group-default required">
                            <label>Address Line 1 :</label>
                            <div class="controls">
                                <asp:TextBox ID="AddressLine1" runat="server" placeholder="Enter Reseller Address" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Address Line 2 :</label>
                            <div class="controls">
                                <asp:TextBox ID="AddressLine2" runat="server" placeholder="Enter Reseller Address" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            
                              <label>Area Code :</label>
                            <div class="controls">
                                <asp:TextBox ID="AreaCode" runat="server" placeholder="Enter Reseller Area Code" CssClass="form-control postCode" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>City/Suburb :</label>
                            <div class="controls">
                                <asp:TextBox ID="CitySuburb" runat="server" placeholder="Enter Reseller City / Suburb" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Province :</label>
                            <div class="controls">
                                <asp:DropDownList ID="Province" runat="server" CssClass="form-control">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="Eastern Cape">Eastern Cape</asp:ListItem>
                                    <asp:ListItem Value="Free State">Free State</asp:ListItem>
                                    <asp:ListItem Value="Gauteng">Gauteng</asp:ListItem>
                                    <asp:ListItem Value="Kwazulu Natal">Kwazulu Natal</asp:ListItem>
                                    <asp:ListItem Value="Limpopo">Limpopo</asp:ListItem>
                                    <asp:ListItem Value="Mpumalanga">Mpumalanga</asp:ListItem>
                                    <asp:ListItem Value="North West Province">North West Province</asp:ListItem>
                                    <asp:ListItem Value="Northern Cape">Northern Cape</asp:ListItem>
                                    <asp:ListItem Value="Western Cape">Western Cape</asp:ListItem>
                                </asp:DropDownList>
                              
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <h5>Postal</h5>
                        <div class="form-group form-group-default">
                            <label>Postal Address :</label>
                            <div class="controls">
                                <asp:TextBox ID="PostalAddress" runat="server" placeholder="Enter Reseller Postal Address" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>City :</label>
                            <div class="controls">
                                <asp:TextBox ID="PostalCity" runat="server" placeholder="Enter Reseller City" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Postal Code :</label>
                            <div class="controls">
                                <asp:TextBox ID="PostalCode" runat="server" placeholder="Enter Reseller Postal Code" CssClass="form-control postCode"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Province :</label>
                            <div class="controls">
                                <asp:DropDownList ID="postalprovince" runat="server" CssClass="form-control">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="Eastern Cape">Eastern Cape</asp:ListItem>
                                    <asp:ListItem Value="Free State">Free State</asp:ListItem>
                                    <asp:ListItem Value="Gauteng">Gauteng</asp:ListItem>
                                    <asp:ListItem Value="Kwazulu Natal">Kwazulu Natal</asp:ListItem>
                                    <asp:ListItem Value="Limpopo">Limpopo</asp:ListItem>
                                    <asp:ListItem Value="Mpumalanga">Mpumalanga</asp:ListItem>
                                    <asp:ListItem Value="North West Province">North West Province</asp:ListItem>
                                    <asp:ListItem Value="Northern Cape">Northern Cape</asp:ListItem>
                                    <asp:ListItem Value="Western Cape">Western Cape</asp:ListItem>
                                </asp:DropDownList>
                              
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asp:Button ID="btn_add" CssClass="btn btn-primary" runat="server" Text="Add New Reseller" OnClick="btn_add_Click" Style="float: right;" />
                    </div>
                </div>
                <div class="row hide">
                    <div class="col-md-12">
                        <div class="form-group form-group-default required">
                            <label>Certificate Start Range :</label>
                            <div class="controls">
                                <asp:DropDownList ID="StartRange" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Certificate End Range :</label>
                            <div class="controls">
                                <asp:DropDownList ID="EndRange" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Invoice Number :</label>
                            <div class="controls">
                                <asp:TextBox ID="InvoiceNumber" runat="server" TextMode="Number" placeholder="Enter the invoice number" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default hide">
                            <label>Number or Certificates to assign :</label>
                            <div class="controls">
                                <asp:TextBox ID="NoCertificates" runat="server" TextMode="Number" placeholder="Enter the number of certificates allowed to produce" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                </div>

            </div>
        </div>
    </div>

    <script>

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
