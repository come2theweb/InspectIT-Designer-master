<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewRegistrationOrUpdateDetails.aspx.cs" Inherits="InspectIT.ViewRegistrationOrUpdateDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/bootstrap-select2/select2.css" rel="stylesheet" />
    <script src="assets/plugins/bootstrap-select2/select2.min.js"></script>
    <link href="assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
    <script src="assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/lodash.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- START CONTAINER FLUID -->
    <div class="container-fluid container-fixed-lg bg-white">
        <!-- START PANEL -->
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div class="panel-title">
                    View Users
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
                                <p id="RegistrationNumber" runat="server"></p>
                            </div>
                        </div>
                        <div class="form-group form-group-default required">
                            <label>Title :</label>
                            <div class="controls">
                                <p id="TitleStatus" runat="server"></p>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Firstname :</label>
                            <div class="controls">
                                <p id="FirstName" runat="server"></p>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Lastname :</label>
                            <div class="controls">
                                <p id="LastName" runat="server"></p>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>ID No :</label>
                            <div class="controls">
                                <p id="IDNumber" runat="server"></p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">

                        <div class="form-group form-group-default">
                            <label>Registration Renewal Date :</label>
                            <div class="controls">
                                <p id="RegistrationRenewalDate" runat="server"></p>
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
                                <p id="Notice" runat="server"></p>
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
                                <p id="NonLoggedCOCs" runat="server"></p>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Number of Non Logged COC's - Allocated :</label>
                            <div class="controls">
                                <p id="NonLoggedCOCsAllocated" runat="server"></p>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Number of COC's Logged :</label>
                            <div class="controls">
                                <p id="NumberCOCsLogged" runat="server"></p>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Number of COC's Audited to Date :</label>
                            <div class="controls">
                                <p id="NumberCOCsAudited" runat="server"></p>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Refix Notices :</label>
                            <div class="controls">
                                <p id="RefixNotices" runat="server"></p>
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
                                <p id="AddressStreet" runat="server"></p>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Suburb :</label>
                            <div class="controls">
                                <p id="AddressSuburb" runat="server"></p>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>City :</label>
                            <div class="controls">
                                <p id="AddressCity" runat="server"></p>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Province :</label>
                            <div class="controls">
                                <p id="AddressProvince" runat="server"></p>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Area Code :</label>
                            <div class="controls">
                                <p id="AddressAreaCode" runat="server"></p>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <h5>Postal</h5>

                        <div class="form-group form-group-default">
                            <label>Postal Address :</label>
                            <div class="controls">
                                <p id="PostalAddress" runat="server"></p>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>City :</label>
                            <div class="controls">
                                <p id="PostalCity" runat="server"></p>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Postal Code :</label>
                            <div class="controls">
                                <p id="PostalCode" runat="server"></p>
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
                                <p id="HomeNo" runat="server"></p>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Mobile Number :</label>
                            <div class="controls">
                                <p id="MobileNo" runat="server"></p>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Employment Date :</label>
                            <div class="controls">
                                <p id="EmploymentDate" runat="server"></p>
                            </div>
                        </div>

                        <div class="form-group form-group-default">
                            <label>Email :</label>
                            <div class="controls">
                                <p id="Email" runat="server"></p>
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
                                <p id="InsuranceCompany" runat="server"></p>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Policy Holder :</label>
                            <div class="controls">
                                <p id="PolicyHolder" runat="server"></p>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Policy Number :</label>
                            <div class="controls">
                                <p id="PolicyNumber" runat="server"></p>
                            </div>
                        </div>
                        <div class="form-group form-group-default">
                            <label>Period of Insurance :</label>
                            <div class="controls">
                                <label>From :</label>
                                <p id="PeriodOfInsuranceFrom" runat="server"></p>

                                <label>To :</label>
                                <p id="PeriodOfInsuranceTo" runat="server"></p>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="auto-style1">
                        <asp:Button ID="btn_updateDetails" CssClass="btn btn-primary" Style="float: right;" runat="server" Text="Update Details" />
                    </div>
                </div>

                <!-- START Form Control-->
                <div class="row">
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

    <script>
        $(document).ready(function () {
            $.ajax({
                type: 'POST',
                url: "../API/RegistrationDetails?RegistrationID=1",
                dataType: 'json',
                traditional: true,
                success: function (data) {
                    var result = JSON.stringify(data);
                    result = result.replace("[", "");
                    result = result.replace("]", "");
                    result = JSON.parse(result);
                    $("#<%=RegistrationNumber.ClientID%>").html(result.RegistrationNumber);
                    $("#<%=TitleStatus.ClientID%>").html(result.TitleStatus);
                    $("#<%=FirstName.ClientID%>").html(result.FirstName);
                    $("#<%=LastName.ClientID%>").html(result.LastName);
                    $("#<%=IDNumber.ClientID%>").html(result.IDNumber);
                    $("#<%=RegistrationRenewalDate.ClientID%>").html(result.RegistrationRenewalDate);
                    $("#<%=Notice.ClientID%>").html(result.Notice);
                    $("#<%=NonLoggedCOCs.ClientID%>").html(result.NonLoggedCOCs);
                    $("#<%=NonLoggedCOCsAllocated.ClientID%>").html(result.NonLoggedCOCsAllocated);
                    $("#<%=NumberCOCsLogged.ClientID%>").html(result.NumberCOCsLogged);
                    $("#<%=NumberCOCsAudited.ClientID%>").html(result.NumberCOCsAudited);
                    $("#<%=RefixNotices.ClientID%>").html(result.RefixNotices);
                    $("#<%=AddressStreet.ClientID%>").html(result.AddressStreet);
                    $("#<%=AddressSuburb.ClientID%>").html(result.AddressSuburb);
                    $("#<%=AddressCity.ClientID%>").html(result.AddressCity);
                    $("#<%=AddressProvince.ClientID%>").html(result.AddressProvince);
                    $("#<%=AddressAreaCode.ClientID%>").html(result.NonLoggedCOCsAllocated);
                    $("#<%=PostalAddress.ClientID%>").html(result.PostalAddress);
                    $("#<%=PostalCity.ClientID%>").html(result.PostalCity);
                    $("#<%=PostalCode.ClientID%>").html(result.PostalCode);
                    $("#<%=HomeNo.ClientID%>").html(result.HomeNo);
                    $("#<%=MobileNo.ClientID%>").html(result.MobileNo);
                    $("#<%=EmploymentDate.ClientID%>").html(result.EmploymentDate);
                    $("#<%=Email.ClientID%>").html(result.Email);
                    $("#<%=InsuranceCompany.ClientID%>").html(result.InsuranceCompany);
                    $("#<%=PolicyHolder.ClientID%>").html(result.PolicyHolder);
                    $("#<%=PolicyNumber.ClientID%>").html(result.PolicyNumber);
                    $("#<%=PeriodOfInsuranceFrom.ClientID%>").html(result.PeriodOfInsuranceFrom);
                    $("#<%=PeriodOfInsuranceTo.ClientID%>").html(result.PeriodOfInsuranceTo);


                }
            });
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


    </script>

</asp:Content>
