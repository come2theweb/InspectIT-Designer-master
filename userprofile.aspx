<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="userprofile.aspx.cs" Inherits="InspectIT.userprofile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="assets/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <link href="pages/css/pages.css" rel="stylesheet" />
    <link href="pages/css/ie9.css" rel="stylesheet" />
    <link href="pages/css/pages.min.css" rel="stylesheet" />
    <link href="pages/css/themes/abstract.css" rel="stylesheet" />
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
                     Update Details
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>

                <div id="Admin" runat="server">
                    <div class="form-group form-group-default">
                        <label>Firstname :</label>
                        <div class="controls">
                            <asp:TextBox ID="adminName" runat="server" placeholder="Enter Your First Name" CssClass="form-control" required></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-default">
                        <label>Password :</label>
                        <div class="controls">
                            <asp:TextBox ID="password" runat="server" placeholder="Enter A Password" CssClass="form-control" required TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                      
                    <div class="form-group form-group-default">
                        <label>Role :</label>
                        <div class="controls">
                            <asp:DropDownList ID="role" runat="server" CssClass="form-control">
                                <asp:ListItem Value="Administrator">Administrator</asp:ListItem>
                                <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                <asp:ListItem Value="Inspector">Inspector</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group form-group-default">
                        <label>Email :</label>
                        <div class="controls">
                            <asp:TextBox ID="adminEmail" runat="server" placeholder="Enter Your Email Address" CssClass="form-control" TextMode="Email" required></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-default">
                        <div class="controls">
                            <div class="checkbox">
                                <asp:CheckBox ID="isActive" runat="server" Text="Active" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="auto-style1">
                            <asp:Button ID="btnUpdateAdmin" CssClass="btn btn-primary" runat="server" Text="Update Profile"  style="float:right;" OnClick="btnUpdateAdmin_Click"/>
                        </div>
                    </div> 
                </div>

                <div id="Supplier" runat="server">
                    <div class="row">
                        <div class="col-md-6">
                            <h4>Supplier Details</h4>
                            <div class="form-group form-group-default required">
                                <label>Supplier Name :</label>
                                <div class="controls">
                                    <asp:TextBox ID="SupplierName" runat="server" placeholder="Enter Supplier Name" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default required">
                                <label>Supplier Reg No :</label>
                                <div class="controls">
                                    <asp:TextBox ID="SupplierRegNo" runat="server" placeholder="Enter Your Supplier Registration Number" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default required">
                                <label>Website :</label>
                                <div class="controls">
                                    <asp:TextBox ID="SupplierWebsite" runat="server" placeholder="Enter Supplier Website" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default required">
                                <label>Supplier Email :</label>
                                <div class="controls">
                                    <asp:TextBox ID="SupplierEmail" runat="server" placeholder="Enter Supplier Email Address" TextMode="Email" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default required">
                                <label>Supplier Contact Number :</label>
                                <div class="controls">
                                    <asp:TextBox ID="SupplierContactNo" runat="server" placeholder="Enter Supplier Contact Number" CssClass="form-control phone" required></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <h4>Supplier Login</h4>
                            <div class="form-group form-group-default required">
                                <label>Password:</label>
                                <div class="controls">
                                    <asp:TextBox ID="supPassword" runat="server" placeholder="Enter password to change" CssClass="form-control" TextMode="Password" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default required">
                                <label>Password Confirm:</label>
                                <div class="controls">
                                    <asp:TextBox ID="supPasswordConfirm" runat="server" placeholder="password confirm" CssClass="form-control" TextMode="Password" required></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <h4>Address</h4>

                        <div class="col-md-6">
                            <h5>Physical</h5>

                            <div class="form-group form-group-default required">
                                <label>Address Line 1 :</label>
                                <div class="controls">
                                    <asp:TextBox ID="SupplierAddressLine1" runat="server" placeholder="Enter Supplier Address" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default required">
                                <label>Address Line 2 :</label>
                                <div class="controls">
                                    <asp:TextBox ID="SupplierAddressLine2" runat="server" placeholder="Enter Supplier Address" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default required">
                                <label>Province :</label>
                                <div class="controls">
                                    <asp:TextBox ID="SupplierProvince" runat="server" placeholder="Enter Supplier Province" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default required">
                                <label>City/Suburb :</label>
                                <div class="controls">
                                    <asp:TextBox ID="SupplierCitySuburb" runat="server" placeholder="Enter Supplier City / Suburb" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default required">
                                <label>Area Code :</label>
                                <div class="controls">
                                    <asp:TextBox ID="SupplierAreaCode" runat="server" placeholder="Enter Supplier Area Code" CssClass="form-control postCode" required></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <h5>Postal</h5>
                            <div class="form-group form-group-default">
                                <label>Postal Address :</label>
                                <div class="controls">
                                    <asp:TextBox ID="SupplierPostalAddress" runat="server" placeholder="Enter Supplier Postal Address" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>City :</label>
                                <div class="controls">
                                    <asp:TextBox ID="SupplierCity" runat="server" placeholder="Enter Supplier City" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Postal Code :</label>
                                <div class="controls">
                                    <asp:TextBox ID="SupplierPostalCode" runat="server" placeholder="Enter Supplier Postal Code" CssClass="form-control postCode"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="auto-style1">
                            <asp:Button ID="btnUpdateSupplier" CssClass="btn btn-primary" runat="server" Text="Update Profile" Style="float: right;" OnClick="btnUpdateSupplier_Click" />
                        </div>
                    </div>
                </div>

                <div id="Inspector" runat="server">
                    <div class="row">
                        <h4>General Details</h4>
                        <div class="col-md-6">

                            <div class="form-group form-group-default required">
                                <label>Reg No :</label>
                                <div class="controls">
                                    <asp:TextBox ID="regNo" runat="server" placeholder="Enter Your Registration Number" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Firstname :</label>
                                <div class="controls">
                                    <asp:TextBox ID="InspecName" runat="server" placeholder="Enter Your First Name" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Lastname :</label>
                                <div class="controls">
                                    <asp:TextBox ID="InspecSurname" runat="server" placeholder="Enter Your Last Name" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>ID No :</label>
                                <div class="controls">
                                    <asp:TextBox ID="idNo" runat="server" placeholder="Enter Your ID Number" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <div class="controls">
                                    <div class="checkbox">
                                        <asp:CheckBox ID="InspecActive" runat="server" Text="Active" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group form-group-default">
                                <label>Start Inactive Date :</label>
                                <div class="controls">
                                    <asp:TextBox ID="startInactiveDate" runat="server" placeholder="Start Inactive Date" CssClass="form-control datepicker-range" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>End Inactive Date :</label>
                                <div class="controls">
                                    <asp:TextBox ID="endInactiveDate" runat="server" placeholder="End Inactive Date" CssClass="form-control datepicker-range" required></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">

                            <div class="form-group form-group-default">
                                <label>Phone (Work) :</label>
                                <div class="controls">
                                    <asp:TextBox ID="phoneWork" runat="server" placeholder="Enter Your Work Contact Number" CssClass="form-control phone" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Phone (Home) :</label>
                                <div class="controls">
                                    <asp:TextBox ID="phoneHome" runat="server" placeholder="Enter Your Home Contact Number" CssClass="form-control phone" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Phone (Mobile) :</label>
                                <div class="controls">
                                    <asp:TextBox ID="phoneMobile" runat="server" placeholder="Enter Your Mobile Contact Number" CssClass="form-control phone" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Fax :</label>
                                <div class="controls">
                                    <asp:TextBox ID="fax" runat="server" placeholder="Enter Your Fax Number" CssClass="form-control phone" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Email :</label>
                                <div class="controls">
                                    <asp:TextBox ID="InspecEmail" runat="server" placeholder="Enter Your Email Address" TextMode="Email" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>PIN No :</label>
                                <div class="controls">
                                    <asp:TextBox ID="pin" runat="server" placeholder="Enter Your PIN Number" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Change Photo :</label>
                                <div class="controls">
                                    <asp:FileUpload ID="photoFile" runat="server" />
                                </div>
                                <div>
                                    <asp:TextBox ID="currentImg" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Image ID="Image1" runat="server" Style="max-width: 150px;" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <h4>Address</h4>
                        <div class="col-md-6">
                            <h5>Residential</h5>
                            <div class="form-group form-group-default">
                                <label>Address Line 1 :</label>
                                <div class="controls">
                                    <asp:TextBox ID="addressLine1" runat="server" placeholder="Enter Your Address" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Address Line 2 :</label>
                                <div class="controls">
                                    <asp:TextBox ID="addressLine2" runat="server" placeholder="Enter Your Address" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Province :</label>
                                <div class="controls">
                                    <asp:TextBox ID="province" runat="server" placeholder="Enter Your Province" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>City :</label>
                                <div class="controls">
                                    <asp:TextBox ID="auditCity" runat="server" placeholder="Enter Your City / Suburb" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Suburb :</label>
                                <div class="controls">
                                    <asp:TextBox ID="auditSuburb" runat="server" placeholder="Enter Your City / Suburb" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Area Code :</label>
                                <div class="controls">
                                    <asp:TextBox ID="areaCode" runat="server" placeholder="Enter Your Area Code" CssClass="form-control postCode" required></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <h5>Postal</h5>

                            <div class="form-group form-group-default">
                                <label>Postal Address :</label>
                                <div class="controls">
                                    <asp:TextBox ID="postalAddress" runat="server" placeholder="Enter Your Postal Address" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>City :</label>
                                <div class="controls">
                                    <asp:TextBox ID="postalCity" runat="server" placeholder="Enter Your City" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Postal Code :</label>
                                <div class="controls">
                                    <asp:TextBox ID="postalCode" runat="server" placeholder="Enter Your Postal Code" CssClass="form-control postCode" required></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <h4>Business</h4>
                        <div class="col-md-6">
                            <h5>Company Details</h5>

                            <div class="form-group form-group-default">
                                <label>Company Reg No :</label>
                                <div class="controls">
                                    <asp:TextBox ID="companyRegNo" runat="server" placeholder="Enter Your Company Registration Number" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>VAT Reg No :</label>
                                <div class="controls">
                                    <asp:TextBox ID="vatRegNo" runat="server" placeholder="Enter Your VAT Registration Number" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group form-group-default">
                                <label>Company Logo :</label>
                                    <div class="controls">
                                        <asp:FileUpload ID="FileUpload1" runat="server"/>
                                     
                                    <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control"></asp:TextBox>
                               
                                <asp:Image ID="Image2" runat="server" Style="max-width: 150px;" />
                                    </div>
                            </div>


                            <div class="form-group form-group-default hide">
                                <label>Pastel Account :</label>
                                <div class="controls">
                                    <asp:TextBox ID="pastelAccount" runat="server" placeholder="Enter Your Pastel Account" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default hide">
                                <label>Invoice Email :</label>
                                <div class="controls">
                                    <asp:TextBox ID="invoiceEmail" runat="server" placeholder="Enter Your Invoice Email" TextMode="Email" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default hide">
                                <div class="controls">
                                    <div class="checkbox ">
                                        <asp:CheckBox ID="stopPayments" runat="server" Text="Stop Payments" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <h5>Banking Details</h5>
                            <div class="form-group form-group-default">
                                <label>Bank Name :</label>
                                <div class="controls">
                                    <asp:TextBox ID="bankName" runat="server" placeholder="Enter Your Bank" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Account Name :</label>
                                <div class="controls">
                                    <asp:TextBox ID="accName" runat="server" placeholder="Enter Your Account Name" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Account Number :</label>
                                <div class="controls">
                                    <asp:TextBox ID="accNumber" runat="server" placeholder="Enter Your Account Number" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Branch Code :</label>
                                <div class="controls">
                                    <asp:TextBox ID="branchCode" runat="server" placeholder="Enter Your Branch Code" CssClass="form-control" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Account Type :</label>
                                <div class="form-control controls">
                                    <asp:DropDownList ID="accType" runat="server">
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <asp:ListItem>Savings</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
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
                            <asp:Button ID="btnUpdateInspector" CssClass="btn btn-primary" runat="server" Text="Update Profile"  Style="float: right;" OnClick="btnUpdateInspector_Click" />
                        </div>
                    </div>
                </div>

                <div id="plumber" runat="server">
                    
                    <div class="row">
                        <h3>Registered Plumber Details</h3>
                        <div class="col-md-6">
                            <div class="form-group form-group-default">
                                <label>Registration Number:</label>
                                <div class="controls">
                                    <asp:TextBox ID="regnoplumber" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default hide">
                                <label>Title:</label>
                                <div class="controls">
                                    <asp:TextBox ID="title" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Name:</label>
                                <div class="controls">
                                    <asp:TextBox ID="Name" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Surname:</label>
                                <div class="controls">
                                    <asp:TextBox ID="Surname" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>ID Number:</label>
                                <div class="controls">
                                    <asp:TextBox ID="IDNum" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Registration Renewal Date:</label>
                                <div class="controls">
                                    <asp:TextBox ID="regrenewaldate" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            
                            
                        </div>
                        <div class="col-md-6">
                            <div class="form-group form-group-default">
                                <label>Password:</label>
                                <div class="controls">
                                    <asp:TextBox ID="plumberPassword" runat="server" CssClass="form-control" ReadOnly="true"  ></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                                <label>Registration Details:</label>
                                <div class="controls">
                                    <asp:CheckBoxList ID="regDetails" runat="server" ReadOnly="true">
                                           <asp:ListItem Value="Licensed Plumber">Licensed Plumber</asp:ListItem>
                                        <asp:ListItem Value="Solar">Solar</asp:ListItem>
                                        <asp:ListItem Value="Heat Pump">Heat Pump</asp:ListItem>
                                        <asp:ListItem Value="Plumbing Training Assesor">Assessor</asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        <div class="form-group form-group-default">
                                <label>Signature:</label>
                                <div class="controls">
                                    <asp:Image ID="plumberSignature" runat="server" />
                                </div>
                            </div>
                        <div class="col-md-12">
                           <div class="form-group form-group-default">
                                <label>Plumber Message:</label>
                                <div class="controls">
                                    <asp:TextBox ID="notice" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6" ReadOnly="true" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="row">
                        <h3>Company Details</h3>
                        <asp:CheckBox ID="CheckBox1" runat="server" />Make this company your Billing details
                        <div class="col-md-12">
                            <div class="form-group form-group-default">
                                <label>Company Name:</label>
                                <div class="controls">
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default hide">
                                <label>Company Reg Number:</label>
                                <div class="controls">
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Company Email:</label>
                                <div class="controls">
                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Company Contact Number:</label>
                                <div class="controls">
                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Company Address:</label>
                                <div class="controls">
                                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Company VAT:</label>
                                <div class="controls">
                                    <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <h3>COC Overview</h3>
                            <div class="form-group form-group-default">
                                <label>Number of Non Logged COC's:</label>
                                <div class="controls">
                                    <asp:TextBox ID="nonloggedcoc" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Number of Non Logged COC's - Allocated:</label>
                                <div class="controls">
                                    <asp:TextBox ID="nonloggedcocallocated" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Number of COC's Logged:</label>
                                <div class="controls">
                                    <asp:TextBox ID="loggedcoc" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Number of COC’s Audited to Date:</label>
                                <div class="controls">
                                    <asp:TextBox ID="numcocaudited" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Refix Notices:</label>
                                <div class="controls">
                                    <asp:TextBox ID="refixNotices" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                        <div class="col-md-6">
                             <h3>Postal Address <small style="color:red;">Note:All mail will be sent to this address</small></h3>
                        <div class="form-group form-group-default">
                                <label>Address:</label>
                                <div class="controls">
                                    <asp:TextBox ID="postalAddressPlumber" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                        <div class="form-group form-group-default">
                                <label>City:</label>
                                <div class="controls">
                                    <asp:TextBox ID="postalCityPlumber" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                        <div class="form-group form-group-default">
                                <label>Postal Code:</label>
                                <div class="controls">
                                    <asp:TextBox ID="postalCodePlumber" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                         <div class="col-md-6">
                             <h3>Residential Address</h3>
                        <div class="form-group form-group-default">
                                <label>Street Address:</label>
                                <div class="controls">
                                    <asp:TextBox ID="resstreetaddyplumber" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                        <div class="form-group form-group-default">
                                <label>Suburb:</label>
                                <div class="controls">
                                    <asp:TextBox ID="ressuburbplumber" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                         <div class="form-group form-group-default">
                                <label>City:</label>
                                <div class="controls">
                                    <asp:TextBox ID="rescityplumber" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                        
                        <div class="form-group form-group-default">
                                <label>Postal Code:</label>
                                <div class="controls">
                                    <asp:TextBox ID="respostalcodeplumber" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                         <div class="form-group form-group-default">
                                <label>Province:</label>
                                <div class="controls">
                                    <asp:TextBox ID="resprovinceplumber" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                        </div>

                         <div class="col-md-6">
                             <h3>Contact Details <br /><small style="color:red;">Note: All notifications will be sent to these numbers and email addresses</small></h3>
                        <div class="form-group form-group-default">
                                <label>Home Phone Number:</label>
                                <div class="controls">
                                    <asp:TextBox ID="plumberhomephone" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                        <div class="form-group form-group-default">
                                <label>Mobile Phone Number:</label>
                                <div class="controls">
                                    <asp:TextBox ID="plumbermodilenum" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                        <div class="form-group form-group-default">
                                <label>Email:</label>
                                <div class="controls">
                                    <asp:TextBox ID="plumberemail" runat="server" TextMode="Email" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group form-group-default">
                                <label>Fax:</label>
                                <div class="controls">
                                    <asp:TextBox ID="plumberfax" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                         <div class="col-md-6">
                             <h3>Employer Details <br /><small style="color:red;">Note: To change your company details contact the PIRB Offices</small></h3>
                        <div class="form-group form-group-default">
                                <label>Company:</label>
                                <div class="controls">
                                    <asp:TextBox ID="plumberempcompany" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        <div class="form-group form-group-default">
                                <label>Contact Number:</label>
                                <div class="controls">
                                    <asp:TextBox ID="plumberempcontact" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                         <div class="form-group form-group-default">
                                <label>Employment Date:</label>
                                <div class="controls">
                                    <asp:TextBox ID="plumberemploymentdate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        
                        <div class="form-group form-group-default">
                                <label>Email:</label>
                                <div class="controls">
                                    <asp:TextBox ID="plumberempemail" TextMode="Email" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                       
                        <h3>Insurance Details </h3>
                        <div class="form-group form-group-default">
                                <label>Insurance Company:</label>
                                <div class="controls">
                                    <asp:TextBox ID="plumberinscompany" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                        <div class="form-group form-group-default">
                                <label>Policy Holder:</label>
                                <div class="controls">
                                    <asp:TextBox ID="plumberpolicyholder" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                        <div class="form-group form-group-default">
                                <label>Policy Number:</label>
                                <div class="controls">
                                    <asp:TextBox ID="plumberpolicynumber" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group form-group-default">
                                <label>Period of Insurance from:</label>
                                <div class="controls">
                                    <asp:TextBox ID="plumberperiodinsfrom" runat="server" CssClass="form-control datepicker-range" ></asp:TextBox>
                                </div>
                            </div>
                        <div class="form-group form-group-default">
                                <label>Period of Insurance to:</label>
                                <div class="controls">
                                    <asp:TextBox ID="plumberperiodinsto" runat="server" CssClass="form-control datepicker-range" ></asp:TextBox>
                                </div>
                            </div>
                        
                    </div>

                    <div class="row">
                        <div class="auto-style1">
                            <asp:Button ID="btnUpdatePlumber" CssClass="btn btn-primary" runat="server" Text="Update Profile"  style="float:right;" OnClick="btnUpdatePlumber_Click"/>
                        </div>
                    </div> 
                </div>

                <div id="otpChangePass" runat="server">
                    <div class="row">
                        <h3>Enter OTP to change your password</h3>
                        <div class="form-group form-group-default">
                                <label>OTP:</label>
                                <div class="controls">
                                    <asp:TextBox ID="otpchangePasseword" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                    </div>
                     <div class="row">
                        <div class="auto-style1">
                            <asp:Button ID="SubCoc" CssClass="btn btn-primary" runat="server" Text="Send"  style="float:right;" OnClick="SubCoc_Click"/>
                        </div>
                    </div> 
                </div>
            </div>
        </div>
    </div>

    <script>
        $('.datepicker-range').datepicker();
    </script>

</asp:Content>
