<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditOrDeleteSupplier.aspx.cs" Inherits="InspectIT.EditOrDeleteSupplier" %>

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
                    Edit Reseller
       
                </div>
                <div class="pull-right">
                    <div class="col-xs-12">
                        
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">

                <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>

                
                <ul class="nav nav-tabs nav-tabs-simple" role="tablist">
                    <li class="active"><a href="#supdetails" data-toggle="tab" role="tab">Reseller Details</a></li>
                    <li class=""><a href="#cocdetails" data-toggle="tab" role="tab">COC Range</a></li>
                </ul>

                 <div class="tab-content">
                    <div class="tab-pane active" id="supdetails">
                         <div class="row">
                             <div class="col-md-12">
                                 <asp:CheckBox ID="CheckBox1" runat="server" /> Block Reseller from purchasing more COC's
                             </div>
                    <div class="col-md-6">
                        <h4>Supplier Details</h4>
                        <div class="form-group form-group-default required">
                            <label>Reseller Name :</label>
                            <div class="controls">
                                <asp:TextBox ID="SupplierName" runat="server" placeholder="Enter Supplier Name" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Reseller Reg No :</label>
                            <div class="controls">
                                <asp:TextBox ID="SupplierRegNo" runat="server" placeholder="Enter Your Supplier Registration Number" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default hide">
                            <label>Website :</label>
                            <div class="controls">
                                <asp:TextBox ID="SupplierWebsite" runat="server" placeholder="Enter Supplier Website" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Reseller Email :</label>
                            <div class="controls">
                                <asp:TextBox ID="SupplierEmail" runat="server" placeholder="Enter Supplier Email Address" TextMode="Email" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Reseller Contact Number :</label>
                            <div class="controls">
                                <asp:TextBox ID="SupplierContactNo" runat="server" placeholder="Enter Supplier Contact Number" CssClass="form-control phone" required></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <h4>Supplier Contact Details</h4>
                        <div class="form-group form-group-default required">
                            <label>Name :</label>
                            <div class="controls">
                                <asp:TextBox ID="fName" runat="server" placeholder="Enter Supplier Name" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Surname :</label>
                            <div class="controls">
                                <asp:TextBox ID="lName" runat="server" placeholder="Enter Supplier Name" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Email :</label>
                            <div class="controls">
                                <asp:TextBox ID="email" runat="server" placeholder="Enter Supplier Name" TextMode="Email" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Password:</label>
                            <div class="controls">
                                <asp:TextBox ID="supPassword" runat="server" placeholder="enter password to change" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Password Confirm:</label>
                            <div class="controls">
                                <asp:TextBox ID="supPasswordConfirm" runat="server" placeholder="password confim" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>

                         <div class="row">
                              <h5>COC Overview</h5>

                            <div class="form-group form-group-default required">
                                <label>Number of COC's able to purchase:</label>
                                <div class="controls">
                                    <asp:TextBox ID="nonloggedcocallocated" runat="server" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Number of COC's purchased:</label>
                                <div class="controls">
                                    <asp:TextBox ID="loggedcoc" runat="server" CssClass="form-control"  ></asp:TextBox>
                                </div>
                            </div>
 </div>

                <div class="row">
                    <div class="col-md-6">
                        <h5>Physical Address</h5>

                        <div class="form-group form-group-default required">
                            <label>Address Line 1 :</label>
                            <div class="controls">
                                <asp:TextBox ID="AddressLine1" runat="server" placeholder="Enter Supplier Address" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Address Line 2 :</label>
                            <div class="controls">
                                <asp:TextBox ID="AddressLine2" runat="server" placeholder="Enter Supplier Address" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Area Code :</label>
                            <div class="controls">
                                <asp:TextBox ID="AreaCode" runat="server" placeholder="Enter Supplier Area Code" CssClass="form-control postCode" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>City/Suburb :</label>
                            <div class="controls">
                                <asp:TextBox ID="CitySuburb" runat="server" placeholder="Enter Supplier City / Suburb" CssClass="form-control" required></asp:TextBox>
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
                                <asp:TextBox ID="PostalAddress" runat="server" placeholder="Enter Supplier Postal Address" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>City :</label>
                            <div class="controls">
                                <asp:TextBox ID="PostalCity" runat="server" placeholder="Enter Supplier City" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Postal Code :</label>
                            <div class="controls">
                                <asp:TextBox ID="PostalCode" runat="server" placeholder="Enter Supplier Postal Code" CssClass="form-control postCode"></asp:TextBox>
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
                </div>
                    </div>
                      <div class="tab-pane " id="cocdetails">
                         <table class="table table-striped demo-table-search" id="stripedTable">
                            <thead>
                            <tr>
                        
                                <th>OrderID</th>
                                <th>Description</th>
                                <th>COC Number</th>
                                <th>COC Start Range</th>
                                <th>COC End Range</th>
                                <th>Date & Time Purchase</th>
                                <th>Plumber</th>
                                <th>RegNo</th>
                            </tr>
                            </thead>
                            <tbody  id="COCStatement" runat="server"></tbody>
                        </table>
                    </div>
                </div>

               

                <div class="row hide">
                    <div class="col-md-12">
                        <div class="form-group form-group-default">
                            <label>Certificate Start Range :</label>
                            <div class="controls">
                                <asp:DropDownList ID="StartRange" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Certificate End Range :</label>
                            <div class="controls">
                                <asp:DropDownList ID="EndRange" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group form-group-default hide">
                            <label>Number or Certificates to assign :</label>
                            <div class="controls">
                                <asp:TextBox ID="NoCertificates" runat="server" TextMode="Number" placeholder="Enter the number of certificates allowed to produce" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Invoice Number :</label>
                            <div class="controls">
                                <asp:TextBox ID="InvoiceNumber" runat="server" TextMode="Number" placeholder="Enter the invoice number" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                </div>
                <div class="auto-style1">
                        <asp:Button ID="btn_update" CssClass="btn btn-primary" runat="server" Text="Update Supplier" Style="float: right;" OnClick="btn_update_Click" />
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
