<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddAuditor.aspx.cs" Inherits="InspectIT.AddAuditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="assets/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <link href="pages/css/pages.css" rel="stylesheet" />
    <link href="pages/css/ie9.css" rel="stylesheet" />
    <link href="pages/css/pages.min.css" rel="stylesheet" />
    <link href="pages/css/themes/abstract.css" rel="stylesheet" />

    <link href="assets/plugins/bootstrap-select2/select2.css" rel="stylesheet" />
    <style>
        h4{font-weight:bold; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- START CONTAINER FLUID -->
    <div class="container-fluid container-fixed-lg bg-white">
    <!-- START PANEL -->
    <div class="panel panel-transparent">
        <div class="panel-heading">
        <div class="panel-title">Add Auditors
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
<!-- BGINNING OF FORM TO ADD A NEW USER -->

<!-- First Row -->
<div class="row">
    <h4>General Details</h4>
    <!-- Second Column -->
    <div class="col-md-6">

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Reg No :</label>
                    <div class="controls">
                        <asp:TextBox ID="regNo" runat="server" placeholder="Enter Your Registration Number" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Firstname :</label>
                    <div class="controls">
                        <asp:TextBox ID="fName" runat="server" placeholder="Enter Your First Name" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Lastname :</label>
                    <div class="controls">
                        <asp:TextBox ID="lName" runat="server" placeholder="Enter Your Last Name" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>ID No :</label>
                    <div class="controls">
                        <asp:TextBox ID="idNo" runat="server" placeholder="Enter Your ID Number" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- Active / Not Active Checkbox -->
           <div class="form-group form-group-default">
                    <div class="controls">
                        <div class="checkbox">
                            <asp:CheckBox ID="active" Checked runat="server" Text="Active"/>
                        </div>
                    </div>
            </div>

       <!-- START Form Control-->
            <div class="form-group form-group-default hide">
                <label>Start Inactive Date :</label>
                    <div class="controls">
                        <asp:TextBox ID="startInactiveDate" runat="server" placeholder="Start Inactive Date" CssClass="form-control datepicker-range"></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->
        
         <!-- START Form Control-->
            <div class="form-group form-group-default hide">
                <label>End Inactive Date :</label>
                    <div class="controls">
                        <asp:TextBox ID="endInactiveDate" runat="server" placeholder="End Inactive Date" CssClass="form-control datepicker-range"></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->  
           
    </div>
    <!-- Second Column -->
    <div class="col-md-6">

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Phone (Work) :</label>
                    <div class="controls">
                        <asp:TextBox ID="phoneWork" runat="server" placeholder="Enter Your Work Contact Number" CssClass="form-control phone" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Phone (Home) :</label>
                    <div class="controls">
                        <asp:TextBox ID="phoneHome" runat="server" placeholder="Enter Your Home Contact Number" CssClass="form-control phone" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Phone (Mobile) :</label>
                    <div class="controls">
                        <asp:TextBox ID="phoneMobile" runat="server" placeholder="Enter Your Mobile Contact Number" CssClass="form-control phone" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Fax :</label>
                    <div class="controls">
                        <asp:TextBox ID="fax" runat="server" placeholder="Enter Your Fax Number" CssClass="form-control phone" required></asp:TextBox>
                    </div>
            </div>
            <div class="form-group form-group-default required">
                <label>Email :</label>
                    <div class="controls">
                        <asp:TextBox ID="email" runat="server" placeholder="Enter Your Email Address" CssClass="form-control" TextMode="Email" required></asp:TextBox>
                    </div>
            </div>
            <div class="form-group form-group-default hide">
                <label>PIN No :</label>
                    <div class="controls">
                        <asp:TextBox ID="pin" runat="server" placeholder="Enter Your PIN Number" CssClass="form-control"></asp:TextBox>
                    </div>
            </div>
        <div class="form-group form-group-default required">
                            <label>Password:</label>
                            <div class="controls">
                                <asp:TextBox ID="supPassword" runat="server" placeholder="enter password to change" TextMode="Password" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Password Confirm:</label>
                            <div class="controls">
                                <asp:TextBox ID="supPasswordConfirm" runat="server" placeholder="password confim" TextMode="Password" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
            <div class="form-group form-group-default required">
                <label>Change Photo :</label>
                    <div class="controls">
                        <asp:FileUpload ID="photoFile" runat="server"/>
                    </div>
            </div>
        <!-- END Form Control-->

    </div>
</div>
<!-- / First Row -->

<!-- Second Row -->
<div class="row">
    <h4>Address</h4>
    <!-- First COlumn -->
    <div class="col-md-6">
        <h5>Residential</h5>
        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Address Line 1 :</label>
                    <div class="controls">
                        <asp:TextBox ID="addressLine1" runat="server" placeholder="Enter Your Address" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            <div class="form-group form-group-default">
                <label>Address Line 2 :</label>
                    <div class="controls">
                        <asp:TextBox ID="addressLine2" runat="server" placeholder="Enter Your Address" CssClass="form-control" ></asp:TextBox>
                    </div>
            </div>

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Suburb :</label>
                    <div class="controls">
                        <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control select2"></asp:DropDownList>
                        <asp:TextBox ID="Suburb" runat="server" placeholder="Enter Your Suburb" CssClass="form-control hide" ></asp:TextBox>
                    </div>
            </div>
        <div class="form-group form-group-default required">
                <label>City :</label>
                    <div class="controls">
                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control select2"></asp:DropDownList>
                        <asp:TextBox ID="city" runat="server" placeholder="Enter Your City " CssClass="form-control hide" ></asp:TextBox>
                    </div>
            </div>
        <div class="form-group form-group-default required">
                            <label>Province :</label>
                            <div class="controls">
                                <asp:DropDownList ID="Province" runat="server" CssClass="form-control" required>
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
            <div class="form-group form-group-default  required">
                <label>Area Code :</label>
                    <div class="controls">
                        <asp:TextBox ID="areaCode" runat="server" placeholder="Enter Your Area Code" CssClass="form-control postCode"></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->


    </div>

    <!-- Second Column -->
    <div class="col-md-6">
        <h5>Postal</h5>

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Postal Address :</label>
                    <div class="controls">
                        <asp:TextBox ID="postalAddress" runat="server" placeholder="Enter Your Postal Address" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>City :</label>
                    <div class="controls">
                        <asp:TextBox ID="postalCity" runat="server" placeholder="Enter Your City" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            


        <div class="form-group form-group-default required">
                <label>Postal Code :</label>
                    <div class="controls">
                        <asp:TextBox ID="postalCode" runat="server" placeholder="Enter Your Postal Code" CssClass="form-control postCode" required></asp:TextBox>
                    </div>
            </div>
        
        <div class="form-group form-group-default required">
                            <label>Province :</label>
                            <div class="controls">
                                <asp:DropDownList ID="postalprovince" runat="server" CssClass="form-control" required>
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
<!-- / Second Row -->

<!-- Third Row -->
<div class="row">
    <h4>Business</h4>
    <!-- First Column -->
    <div class="col-md-6">
        <h5>Company Details</h5>

        <!-- START Form Control-->
        <div class="form-group form-group-default required">
                <label>Company Name :</label>
                    <div class="controls">
                        <asp:TextBox ID="companyName" runat="server" placeholder="Enter Your Company Name" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>

            <div class="form-group form-group-default required">
                <label>Company Reg No :</label>
                    <div class="controls">
                        <asp:TextBox ID="companyRegNo" runat="server" placeholder="Enter Your Company Registration Number" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            <div class="form-group form-group-default">
                <label>VAT Reg No :</label>
                    <div class="controls">
                        <asp:TextBox ID="vatRegNo" runat="server" placeholder="Enter Your VAT Registration Number" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->   

        <div class="form-group form-group-default">
                <label>Company Logo :</label>
                    <div class="controls">
                        <asp:FileUpload ID="FileUpload1" runat="server"/>
                    </div>
            </div>

        <!-- START Form Control-->
            <div class="form-group form-group-default hide">
                <label>Pastel Account :</label>
                    <div class="controls">
                        <asp:TextBox ID="pastelAccount" runat="server" placeholder="Enter Your Pastel Account" CssClass="form-control"></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            <div class="form-group form-group-default hide">
                <label>Invoice Email :</label>
                    <div class="controls">
                        <asp:TextBox ID="invoiceEmail" runat="server" placeholder="Enter Your Invoice Email" CssClass="form-control" TextMode="Email"></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->            
        <div class="form-group form-group-default hide">
                <%--<label>Active :</label>--%>
                    <div class="controls">
                        <div class="checkbox ">
                          <%--<input type="checkbox" value="1" id="stopPayments">
                          <label for="stopPayments">Stop Payments</label>--%>
                            <asp:CheckBox ID="stopPayments" runat="server" Text="Stop Payments" />
                        </div>
                    </div>
            </div>
        <!-- END Form Control-->
        
    </div>

    <!-- Second Column -->
    <div class="col-md-6">
        <h5>Banking Details</h5>

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Bank Name :</label>
                    <div class="controls">
                        <asp:TextBox ID="bankName" runat="server" placeholder="Enter Your Bank" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Account Name :</label>
                    <div class="controls">
                        <asp:TextBox ID="accName" runat="server" placeholder="Enter Your Account Name" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Account Number :</label>
                    <div class="controls">
                        <asp:TextBox ID="accNumber" runat="server" placeholder="Enter Your Account Number" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Branch Code :</label>
                    <div class="controls">
                        <asp:TextBox ID="branchCode" runat="server" placeholder="Enter Your Branch Code" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
        <!-- END Form Control-->

        <!-- START Form Control-->
            <div class="form-group form-group-default required">
                <label>Account Type :</label>
                    <div class="form-control controls">                        
                        <asp:DropDownList ID="accType" runat="server" CssClass="form-control">
                            <asp:ListItem>Cheque</asp:ListItem>
                            <asp:ListItem>Savings</asp:ListItem>
                        </asp:DropDownList>
                    </div>
            </div>
        <!-- END Form Control-->
    </div>
</div>
<!-- / Third Row -->

            <!-- Third Row -->
<div class="row">
    <h4>Manage Areas</h4>
    
    <table class="table table-striped demo-table-search" id="stripedTable">
        <thead>
        <tr>
            <th>Province</th>
            <th>City</th>
            <th>Suburb</th>
        </tr>
        </thead>
        <tbody id="displayauditorsareas" runat="server">
        </tbody>
    </table>

    
        <div class="form-group form-group-default required">
                            <label>Province :</label>
                            <div class="controls">
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" OnSelectedIndexChanged="myListDropDown_Change" AutoPostBack="true">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="1">Eastern Cape</asp:ListItem>
                                    <asp:ListItem Value="2">Free State</asp:ListItem>
                                    <asp:ListItem Value="3">Gauteng</asp:ListItem>
                                    <asp:ListItem Value="4">Kwazulu Natal</asp:ListItem>
                                    <asp:ListItem Value="5">Limpopo</asp:ListItem>
                                    <asp:ListItem Value="6">Mpumalanga</asp:ListItem>
                                    <asp:ListItem Value="8">North West Province</asp:ListItem>
                                    <asp:ListItem Value="7">Northern Cape</asp:ListItem>
                                    <asp:ListItem Value="9">Western Cape</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

     <div class="form-group form-group-default required">
                            <label>City :</label>
                            <div class="controls">
                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" OnSelectedIndexChanged="myListDropDownCity_Change" AutoPostBack="true">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

    <div class="form-group form-group-default required">
                            <label>Suburb :</label>
                            <div class="controls">
                                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

     

    <div class="row hide">
        <div class="auto-style1">
            <asp:Button ID="manageAreas" CssClass="btn btn-primary" runat="server" style="float:right;" Text="Manage Areas" OnClick="manageAreas_Click" formnovalidate/>
        </div>
    </div> 
</div>

<div class="row " id="showSearchAreas" runat="server">
    <h4>Select Areas</h4>

    <div class="col-md-6">
        <label>Search by Area Name: </label>
        <asp:TextBox ID="searchAreas" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="col-md-6">
        <asp:Button ID="searchareasclick" CssClass="btn btn-primary" runat="server" style="float:right;" Text="Search" OnClick="searchareasclick_Click" formnovalidate/>
    </div>

    <div class="col-md-12">

    <asp:CheckBoxList ID="filterAreasDisp" RepeatDirection="Horizontal" RepeatColumns="6" RepeatLayout="Table" runat="server"></asp:CheckBoxList>
    </div>
   

</div>

<div class="row hide">
    <h4>Inspector Range</h4>
        <div class="form-group form-group-default">
            <label>Insert your Range :</label>
                <div class="controls">
                    <asp:TextBox ID="Range" runat="server" placeholder="Enter Your Range" CssClass="form-control" Text="100"></asp:TextBox>
                </div>
        </div>
</div>
            <div class="row">
                <div class="auto-style1">
                    <asp:Button ID="btn_add" CssClass="btn btn-primary" runat="server" style="float:right;" Text="Add Auditor" OnClick="btn_add_Click"/>
                </div>
            </div> 
       

        </div>
    </div>
    <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->
    
    
    <script src="assets/plugins/bootstrap-select2/select2.js"></script>
    <script>
        $('.datepicker-range').datepicker();

        $('.select2').select2({
            // selectOnClose: true
        });

    </script>

</asp:Content>
