<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="companiesEdit.aspx.cs" Inherits="InspectIT.companiesEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="assets/plugins/bootstrap-select2/select2.min.js"></script>
    <link href="assets/css/components.css" rel="stylesheet" />
    <link href="http://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.9/summernote.css" rel="stylesheet" />

    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen" />


    <script src="assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>

    <script src="assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/lodash.min.js"></script>
    
	<script src="assets/js/plugins/pickers/pickadate/picker.js"></script>
	<script src="assets/js/plugins/pickers/pickadate/picker.date.js"></script>
	<script src="assets/js/plugins/pickers/pickadate/picker.time.js"></script>
	<script src="assets/js/plugins/pickers/pickadate/legacy.js"></script>
    <style>
        .datepicker-dropdown {
            z-index: 100000 !important;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- START CONTAINER FLUID -->
    <div class="container-fluid container-fixed-lg bg-white">
        <!-- START PANEL -->
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div class="panel-title">
                    Add New Company
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">

                <div class="alert alert-danger" id="errormsg" runat="server" visible="false">You cannot be allocated more COC’s than what you are permitted to have.  Please either log your Non Logged COC’s or contact the PIRB offices.</div>
                <div class="alert alert-danger" id="Div1" runat="server" visible="false"></div>

                 <ul class="nav nav-tabs nav-tabs-simple" role="tablist">
                        <li class="active"><a href="#compdetails" data-toggle="tab" role="tab">Company Details</a></li>
                        <li class=""><a href="#performance" data-toggle="tab" role="tab">Performance Status</a></li>
                        <li class=""><a href="#list" data-toggle="tab" role="tab">Employee Listing</a></li>
                        <li class=""><a href="#apprentice" data-toggle="tab" role="tab">Apprentice/Mentor Listing</a></li>
                    </ul>

                 <div class="tab-content">
                        <div class="tab-pane active" id="compdetails">
                            <div class="col-md-6">
                    <div class="form-group form-group-default required">
                        <label>Company Name</label>
                        <asp:TextBox ID="CompanyName" runat="server" CssClass="form-control" required></asp:TextBox>
                    </div>
                    <div class="form-group form-group-default hide">
                        <label>Registration Number</label>
                        <asp:TextBox ID="CompanyRegNo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group form-group-default">
                        <label>VAT Number</label>
                        <asp:TextBox ID="VatNo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group form-group-default required">
                        <label>Primary Contact Person</label>
                        <asp:TextBox ID="CompanyContactPerson" runat="server" CssClass="form-control" required></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-6">
                    <h4>Postal Address</h4>
                    <div class="form-group form-group-default">
                        <label>Postal Address</label>
                        <asp:TextBox ID="CompanyPostalAddress" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group form-group-default required">
                        <label>Province</label>
                        <div class="controls">
                            <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control" OnSelectedIndexChanged="myListDropDown_Change" AutoPostBack="true">
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
                            <asp:DropDownList ID="postalCities" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="myListDropDownCity_Change" AutoPostBack="true">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="adminpostalCities" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group form-group-default required">
                        <label>Suburb :</label>
                        <div class="controls">
                            <asp:DropDownList ID="postalSuburb" runat="server" CssClass="form-control select2">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="adminpostalSuburb" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-default">
                        <label>Postal Code</label>
                        <asp:TextBox ID="CompanyPostalCode" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                </div>

                <div class="col-md-6">
                    <h4>Physical Address</h4>
                    <div class="form-group form-group-default">
                        <label>Physical Address</label>
                        <asp:TextBox ID="CompanyPhysicalAddress" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group form-group-default required">
                        <label>Physical Province</label>
                        <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control" OnSelectedIndexChanged="myListDropDown2_Change" AutoPostBack="true">
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

                    <div class="form-group form-group-default required">
                        <label>City :</label>
                        <div class="controls">
                            <asp:DropDownList ID="physicalCities" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="myListDropDownCity2_Change" AutoPostBack="true">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="adminphysicalCities" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group form-group-default required">
                        <label>Suburb :</label>
                        <div class="controls">
                            <asp:DropDownList ID="physicalSuburb" runat="server" CssClass="form-control select2">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="adminphysicalSuburb" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-default">
                        <label>Physical Code</label>
                        <asp:TextBox ID="CompanyPhysicalCode" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <h4>Contact Details</h4>
                <div class="col-md-6">
                    <div class="form-group form-group-default">
                        <label>Mobile Phone</label>
                        <asp:TextBox ID="CompanyMobilePhone" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group form-group-default required">
                        <label>Email Address</label>
                        <asp:TextBox ID="CompanyEmailAddress" runat="server" TextMode="Email" CssClass="form-control" required></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form-group form-group-default">
                        <label>Work Phone</label>
                        <asp:TextBox ID="CompanyWorkPhone" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <h4>Work Operations </h4>

                <asp:CheckBox ID="Maintenance" runat="server" Text="Maintenance" />
                <asp:CheckBox ID="Construction" runat="server" Text="Construction" />


                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group form-group-default">
                                        <label>SARS Clearance Certificate</label>
                                        <asp:TextBox ID="SARSCertificate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-md-6">
                                    <div class="form-group form-group-default">
                                        <label>COID Letter of Good Standing</label>
                                        <asp:TextBox ID="COIDLetter" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-md-6">
                                    <div class="form-group form-group-default">
                                        <label>Public Liability Insurance</label>
                                        <asp:TextBox ID="PublicLiabilityInsurance" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-md-6">
                                    <div class="form-group form-group-default">
                                        <label>Company Liability Insurance</label>
                                        <asp:TextBox ID="CompanyLiabilityInsurance" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-md-6">
                                    <div class="form-group form-group-default">
                                        <label>BBBEE Status</label>
                                        <asp:TextBox ID="BBBEEStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-md-6">
                                    <div class="form-group form-group-default">
                                        <label>IOPSA Membership</label>
                                        <asp:TextBox ID="IOPSAMembership" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-md-6">
                                    <div class="form-group form-group-default">
                                        <label>Employee Medicals</label>
                                        <asp:TextBox ID="EmployeeMedicals" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-md-6">
                                    <div class="form-group form-group-default">
                                        <label>PIRB Company Declaration</label>
                                        <asp:TextBox ID="PIRBDeclaration" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                <div class="auto-style1">

                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" Style="float: right;" OnClick="btnSave_Click"/>
                     <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" Style="float: right;" OnClick="btnUpdate_Click"/>
                    
                </div>
            </div>
                        </div>
                     <div class="tab-pane" id="performance">


                         <ul class="nav nav-tabs nav-tabs-simple" role="tablist">
                                <li class="active"><a href="#TabActive" data-toggle="tab" role="tab">Active</a></li>
                                <li class=""><a href="#TabArchived" data-toggle="tab" role="tab">Archived</a></li>
                            </ul>
                            <div class="tab-content">
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
                                                <th>Date</th>
                                                <th>Performance Type</th>
                                                <th>Details</th>
                                                <th>Point Allocation</th>
                                                <th>Attachments</th>
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
                                                <th>Date</th>
                                                <th>Performance Type</th>
                                                <th>Details</th>
                                                <th>Point Allocation</th>
                                                <th>Attachments</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="displayusers_del" runat="server"></tbody>
                                    </table>
                                </div>

                                <label style="float: right;" class="btn btn-primary" onclick="openRolesAdd()">Add</label>
                            </div>




                        </div>
                     <div class="tab-pane" id="list">
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
                                                <th>Reg No</th>
                                                <th>Name &amp; Surname</th>
                                                <th>Performance Rating</th>
                                                <th>Gamification Incons</th>
                                            </tr>
                                        </thead>
                                        <tbody id="Tbody1" runat="server">
                                        </tbody>
                                    </table>
                        </div>
                     <div class="tab-pane" id="apprentice">
                         
                         <ul class="nav nav-tabs nav-tabs-simple" role="tablist">
                                <li class="active"><a href="#TabActivea" data-toggle="tab" role="tab">Active</a></li>
                                <li class=""><a href="#TabArchiveda" data-toggle="tab" role="tab">Archived</a></li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane active" id="TabActivea">
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
                                                <th>Reg No</th>
                                                <th>Name</th>
                                                <th>Renewal Date</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="Tbody2" runat="server">
                                        </tbody>
                                    </table>
                                </div>
                                <div class="tab-pane" id="TabArchiveda">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <input type="text" id="search-table-del" class="form-control pull-right" placeholder="Search table">
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <table class="table table-striped demo-table-search" id="stripedTable_del">
                                        <thead>
                                            <tr>
                                                <th>Reg No</th>
                                                <th>Name</th>
                                                <th>Renewal Date</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="Tbody3" runat="server"></tbody>
                                    </table>
                                </div>

                                <label style="float: right;" class="btn btn-primary" onclick="openApprenticeAdd()">Add</label>
                            </div>
                     </div>
                </div>
                

            </div>


        </div>
    </div>


    
    <div id="modal_default" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add / Update Performance Status</h5>
                </div>
                <div class="modal-body">

                    <div class="form-group">
                        <label>Date</label>
                        <asp:TextBox ID="date" runat="server" CssClass="form-control datepicker-range"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Performance Type</label>
                        <asp:DropDownList ID="performanceType" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Details</label>
                        <asp:TextBox ID="details" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Attachment</label>

                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label>Performance Point Allocation</label>
                        <asp:TextBox ID="points" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>This activity has an end date</label>
                        <asp:CheckBox ID="hasEndDate" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label>End Date</label>
                        <asp:TextBox ID="endDate" runat="server" CssClass="form-control  datepicker-range"></asp:TextBox>
                    </div>

                    <asp:TextBox ID="PerformanceStatusID" runat="server" CssClass="form-control hide"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-link" data-dismiss="modal">Close</button>
                    <asp:Button ID="saveBtn" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="saveBtn_Click" />
                    <asp:Button ID="updateBtn" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="updateBtn_Click" />

                </div>
            </div>
        </div>
    </div>

    
    <div id="modal_apprentice" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add / Update Apprentice/Mentor</h5>
                </div>
                <div class="modal-body">

                    <div class="form-group">
                        <label>Reg No:</label>
                        <div class="row">
                        <div class="col-md-10">
                            <asp:TextBox ID="regnoappmen" runat="server" CssClass="form-control "></asp:TextBox>
                        </div>
                            <div class="col-md-2">
                                <button class="btn btn-sm btn-primary" type="button" onclick="searchRegno()"><i class="fa fa-search"></i></button>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>Name:</label>
                        <asp:TextBox ID="nameappmen" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Surname:</label>
                        <asp:TextBox ID="surnameappmen" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Guidance Type:</label>
                        <asp:DropDownList ID="selAppMenType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="Apprentice">Apprentice</asp:ListItem>
                            <asp:ListItem Value="Mentor">Mentor</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Start Date of Apprentice/Mentoring</label>
                        <asp:TextBox ID="StartDateAppMen" runat="server" CssClass="form-control  datepicker-range"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Attachments</label>
                        <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control" />
                        <div id="attaappmen"></div>
                    </div>
                    <div class="form-group">
                        <label>End Date of Apprentice/Mentoring</label>
                        <asp:TextBox ID="endDateAppMen" runat="server" CssClass="form-control  pickadate-limits"></asp:TextBox>
                    </div>

                    <asp:TextBox ID="appMenID" runat="server" CssClass="form-control hide"></asp:TextBox>
                    <asp:TextBox ID="ApprenticeID" runat="server" CssClass="form-control hide"></asp:TextBox>
                    <asp:TextBox ID="imgUploaded" runat="server" CssClass="form-control hide"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-link" data-dismiss="modal">Close</button>
                    <asp:Button ID="Button1" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="saveBtnApprentice_Click" />
                    <asp:Button ID="Button2" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="updateBtnApprentice_Click" />

                </div>
            </div>
        </div>
    </div>

    <script>
        function openRolesAdd() {
            $("#modal_default").modal();
            $("#<%=saveBtn.ClientID%>").show();
                $("#<%=updateBtn.ClientID%>").hide();
        }

        function openApprenticeAdd() {
            $("#modal_apprentice").modal();
            $("#<%=Button1.ClientID%>").show();
            $("#<%=Button2.ClientID%>").hide();
        }


        $('.datepicker-range').datepicker();
        function editPerformanceStatus(idd) {
            $("#modal_default").modal();
            $("#<%=saveBtn.ClientID%>").hide();
                $("#<%=updateBtn.ClientID%>").show();

                $("#<%=PerformanceStatusID.ClientID%>").val(idd);
                $.ajax({
                    type: "POST",
                    url: 'API/WebService1.asmx/getPerformanceStatusDetails',
                    data: { id: idd },
                    success: function (data) {
                        $("#<%=date.ClientID%>").val(data.Date);
                    $("#<%=performanceType.ClientID%>").val(data.PerformanceType);
                    $("#<%=points.ClientID%>").val(data.PerformancePointAllocation);
                    $("#<%=details.ClientID%>").val(data.Details);
                    $("#<%=TextBox1.ClientID%>").val(data.Attachment);
                    $("#<%=endDate.ClientID%>").val(data.endDate);
                    if (data.hasEndDate == "True") {
                        $("#<%=hasEndDate.ClientID%>").attr("checked", "checked");
                    }
                },
                });
        }

        function editApprenticeMentorShip(idd) {
            $("#modal_apprentice").modal();
            $("#<%=Button1.ClientID%>").hide();
            $("#<%=Button2.ClientID%>").show();

            $("#<%=PerformanceStatusID.ClientID%>").val(idd);
                $.ajax({
                    type: "POST",
                    url: 'API/WebService1.asmx/getApprenticeMentorshipDetails',
                    data: { id: idd },
                    success: function (data) {
                        $("#<%=regnoappmen.ClientID%>").val(data.regno);
                        $("#<%=surnameappmen.ClientID%>").val(data.surname);
                        $("#<%=nameappmen.ClientID%>").val(data.name);
                        $("#<%=selAppMenType.ClientID%>").val(data.type);
                        $("#<%=StartDateAppMen.ClientID%>").val(data.startDate);
                        $("#<%=endDateAppMen.ClientID%>").val(data.endDate);
                        $("#<%=imgUploaded.ClientID%>").val(data.attachment);
                        $("#attaappmen").html("<img src='performanceImgs/"+data.attachment+"' style='height:50px;'>");
                        

                        $("#<%=ApprenticeID.ClientID%>").val(idd);
                   
                    },
                });
        }

        function searchRegno() {
            $.ajax({
                type: "POST",
                url: 'API/WebService1.asmx/searchUsersRegno',
                data: { id: $("#<%=regnoappmen.ClientID%>").val() },
                success: function (data) {
                    $("#<%=regnoappmen.ClientID%>").val(data.regno);
                        $("#<%=surnameappmen.ClientID%>").val(data.surname);
                        $("#<%=nameappmen.ClientID%>").val(data.name);
                        $("#<%=appMenID.ClientID%>").val(data.userid);
                      
                    },
            });
        }

        var d = new Date();
        var year = d.getFullYear()+1;
        var day = d.getDate();
        var month = d.getMonth();
        console.log("year :: " + d + " ::: " + day + "/" + month + "/"+year);

        $('.pickadate-limits').pickadate({
            min: [2014, 3, 20],
            max: [year, month, day]
        });
</script>

</asp:Content>
