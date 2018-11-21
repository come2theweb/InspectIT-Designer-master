<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddRegistrationOrUpdateDetails.aspx.cs" Inherits="InspectIT.EditRegistrationOrUpdateDetails" %>

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
                    Add Registration Or Update Details
                </div>
                <div class="pull-right">
                    <div class="col-xs-12">
                        <input type="text" id="search-table" class="form-control pull-right" placeholder="Search">
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <h4>Registered Plumber Details</h4>
                    <div class="col-md-6">

                        <div class="form-group form-group-default required">
                            <label>Registration No :</label>
                            <div class="controls">
                                <asp:TextBox ID="RegistrationNumber" runat="server" placeholder="Enter Your Registration Number" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Title :</label>
                            <div class="controls">
                                <asp:TextBox ID="TitleStatus" runat="server" placeholder="Enter Your Title" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Firstname :</label>
                            <div class="controls">
                                <asp:TextBox ID="FirstName" runat="server" placeholder="Enter Your First Name" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Lastname :</label>
                            <div class="controls">
                                <asp:TextBox ID="LastName" runat="server" placeholder="Enter Your Last Name" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>ID No :</label>
                            <div class="controls">
                                <asp:TextBox ID="IDNumber" runat="server" placeholder="Enter Your ID Number" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">

                        <div class="form-group form-group-default">
                            <label>Registration Renewal Date :</label>
                            <div class="controls">
                                <asp:TextBox ID="RegistrationRenewalDate" runat="server" placeholder="Enter Your Registration Renewal Date" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <div class="controls">
                                <label>Registration Details :</label>
                                <div class="checkbox">
                                    <asp:CheckBox ID="LicensedPlumber" runat="server" Text="Licensed Plumber" />
                                    <asp:CheckBox ID="Solar" runat="server" Text="Solar" />
                                    <asp:CheckBox ID="HeatPump" runat="server" Text="Heat Pump" />
                                    <asp:CheckBox ID="Assessor" runat="server" Text="Assessor" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-8">
                        <div class="form-group form-group-default">
                            <label>Notice :</label>
                            <div class="controls">
                                <asp:TextBox ID="Notice" TextMode="MultiLine" runat="server" Style="height: 80px;" placeholder="Enter Your Registration Renewal Date" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <img src="assets/img/profiles/3x.jpg" alt="" id="ProfileImage" cssclass="img-responsive" style="width: 100%; max-width: 200px; margin: auto; display: block;" />
                        <p style="text-align: center;">Note: To change your photo contact the PIRB offices.</p>
                    </div>


                </div>
                
                <div class="row">
                    <h4>COC Overview</h4>
                    <div class="col-md-6">
                        <div class="form-group form-group-default">
                            <label>Number of Non Logged COC's :</label>
                            <div class="controls">
                                <asp:TextBox ID="NonLoggedCOCs" runat="server" placeholder="Number of Non Logged COC’s" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Number of Non Logged COC's - Allocated :</label>
                            <div class="controls">
                                <asp:TextBox ID="NonLoggedCOCsAllocated" runat="server" placeholder="Number of Non Logged COC’s - Allocated" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Number of COC's Logged :</label>
                            <div class="controls">
                                <asp:TextBox ID="NumberCOCsLogged" runat="server" placeholder="Number of COC's Logged" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Number of COC's Audited to Date :</label>
                            <div class="controls">
                                <asp:TextBox ID="NumberCOCsAudited" runat="server" placeholder="Number of COC's Audited to Date" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Refix Notices :</label>
                            <div class="controls">
                                <asp:TextBox ID="RefixNotices" runat="server" placeholder="Refix Notices" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="auto-style1">
                            <a href="ViewRefixandAuditStatement.aspx">
                                <input type="button" value="View" class="btn btn-primary" style="float: right;" />
                            </a>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <h4>Address</h4>

                    <div class="col-md-6">
                        <h5>Residential</h5>

                        <div class="form-group form-group-default">
                            <label>Street :</label>
                            <div class="controls">
                                <asp:TextBox ID="AddressStreet" runat="server" placeholder="Enter Your Street" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Suburb :</label>
                            <div class="controls">
                                <asp:TextBox ID="AddressSuburb" runat="server" placeholder="Enter Your Suburb" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>City :</label>
                            <div class="controls">
                                <asp:TextBox ID="AddressCity" runat="server" placeholder="Enter Your City" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Province :</label>
                            <div class="controls">
                                <asp:TextBox ID="AddressProvince" runat="server" placeholder="Enter Your Province" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Area Code :</label>
                            <div class="controls">
                                <asp:TextBox ID="AddressAreaCode" runat="server" placeholder="Enter Your Area Code" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <h5>Postal</h5>

                        <div class="form-group form-group-default">
                            <label>Postal Address :</label>
                            <div class="controls">
                                <asp:TextBox ID="PostalAddress" runat="server" placeholder="Enter Your Postal Address" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>City :</label>
                            <div class="controls">
                                <asp:TextBox ID="PostalCity" runat="server" placeholder="Enter Your City" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Postal Code :</label>
                            <div class="controls">
                                <asp:TextBox ID="PostalCode" runat="server" placeholder="Enter Your Postal Code" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="row">
                    <h4>Contact Details</h4>
                    <div class="col-md-6">
                        <div class="form-group form-group-default">
                            <label>Home Number :</label>
                            <div class="controls">
                                <asp:TextBox ID="HomeNo" runat="server" placeholder="Enter Your Home Number" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Mobile Number :</label>
                            <div class="controls">
                                <asp:TextBox ID="MobileNo" runat="server" placeholder="Enter Your Mobile Number" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Employment Date :</label>
                            <div class="controls">
                                <asp:TextBox ID="EmploymentDate" runat="server" placeholder="Enter Your Employment Date" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Email :</label>
                            <div class="controls">
                                <asp:TextBox ID="Email" runat="server" placeholder="Enter Your Email" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <h4>Insurance Details</h4>
                    <div class="col-md-6">
                        <div class="form-group form-group-default">
                            <label>Insurance Company :</label>
                            <div class="controls">
                                <asp:TextBox ID="InsuranceCompany" runat="server" placeholder="Enter Your Insurance Company" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Policy Holder :</label>
                            <div class="controls">
                                <asp:TextBox ID="PolicyHolder" runat="server" placeholder="Enter Your Policy Holder" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Policy Number :</label>
                            <div class="controls">
                                <asp:TextBox ID="PolicyNumber" runat="server" placeholder="Enter Your Policy Number" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Period of Insurance :</label>
                            <div class="controls">
                                <label>From :</label>
                                <asp:TextBox ID="PeriodOfInsuranceFrom" runat="server" placeholder="Enter Your Period of Insurance" CssClass="form-control" required></asp:TextBox>
                                <label>To :</label>
                                <asp:TextBox ID="PeriodOfInsuranceTo" runat="server" placeholder="Enter Your Period of Insurance" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="auto-style1">
                        <asp:Button ID="btn_updateDetails" CssClass="btn btn-primary" Style="float: right;" runat="server" Text="Update Details" OnClick="btn_updateDetails_Click" />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <script>
        $('.datepicker-range').datepicker();
    </script>

</asp:Content>
