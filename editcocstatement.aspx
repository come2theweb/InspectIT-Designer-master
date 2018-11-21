<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditCOCStatement.aspx.cs" Inherits="InspectIT.EditCOCStatement" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="assets/plugins/bootstrap-select2/select2.css" rel="stylesheet" />
    <style>
        h4 {
            font-weight: bold;
        }

        .borderTable {
            border-bottom: thin solid black;
        }

        .gm-style-iw {
            width: 300px;
            min-height: 150px;
        }

        .gm-style img {
            max-width: 330px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid container-fixed-lg bg-white">
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div class="panel-title">
                    COC Statement
                </div>
                <div class="pull-right alert alert-info">
                    <div id="COCNumber" runat="server"></div>
                    <div id="COCType" runat="server"></div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">

                <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>


                <ul class="nav nav-tabs nav-tabs-simple" role="tablist">
                    <li class="active"><a href="#cocdetails" data-toggle="tab" role="tab">COC Details</a></li>
                    <li class=""><a href="#auditreport" data-toggle="tab" role="tab">Audit Report</a></li>
                </ul>

                <div class="tab-content">
                    <div class="tab-pane active" id="cocdetails">
                        <div class="row" id="inspectorDetails" runat="server">

                            <h4>Inspector Details</h4>
                            <div class="col-md-5">

                                <div class="form-group form-group-default">
                                    <label>Name :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="InspectorFullName" runat="server" placeholder="Estimated date work will be completed" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Mobile Contact :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="InspectorContact" runat="server" placeholder="Estimated date work will be completed" CssClass="form-control phone" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group form-group-default">
                                    <label>Email :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="InspectorEmail" runat="server" placeholder="Estimated date work will be completed" CssClass="form-control" TextMode="Email" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group form-group-default hide">
                                    <label>Registration Number :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="InspectorRegNo" runat="server" placeholder="Estimated date work will be completed" CssClass="form-control datepicker-range" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group form-group-default hide">
                                    <label>Business Contact :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="InspectorBusContact" runat="server" placeholder="Estimated date work will be completed" CssClass="form-control datepicker-range" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-7">
                                <div class="form-group form-group-default text-center">
                                    <div class="controls" id="InspectorImage">
                                        <asp:Image ID="Image1" runat="server" CssClass="img-circle" Style="height: 100px; width: 100px;" /></div>
                                </div>
                            </div>

                        </div>

                        <div class="row">

                            <h4>Certificate of Compliance Details</h4>
                            <div class="alert alert-warning" id="BlankFormWarning" runat="server">You need to complete this section and update details before you can continue.</div>

                            <div class="col-md-12 hide" id="DisplayRefixNotice" runat="server">
                                <div class="panel panel-default" data-pages="portlet">
                                    <div class="panel-heading bg-default">
                                        <div class="panel-title">Refix Notice</div>
                                    </div>
                                    <div class="panel-body">


                                        <div class="panel panel-default bg-default" data-pages="portlet">
                                            <div class="panel-heading">
                                                <div class="panel-title">Action is required on the following Installation Type Form</div>
                                            </div>
                                            <div class="panel-body">
                                                <div class="form-group form-group-default">
                                                    <div class="controls" id="completeForms" runat="server">
                                                    </div>
                                                </div>

                                                <div class="form-group form-group-default">
                                                    <label>Estimated Completion Date: </label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="RefixDate" runat="server" placeholder="Estimated date work will be completed" CssClass="form-control datepicker-range"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group form-group-default">
                                                    <label>Mark As Fixed: </label>
                                                    <div class="controls">
                                                        <asp:CheckBox ID="RefixCompleted" runat="server" CssClass="checkbox" Text="This job has been refixed" />
                                                    </div>
                                                </div>

                                                <div class="form-group form-group-default text-right">
                                                    <div class="controls">
                                                        <asp:Button ID="updateRefix" CssClass="btn btn-primary" runat="server" Text="Update Refix" OnClick="updateRefix_Click" />
                                                    </div>
                                                </div>

                                                <div class="form-group form-group-default">
                                                    <label>Comments</label>
                                                    <asp:TextBox ID="jkhg" runat="server" TextMode="MultiLine" CssClass="form-control" Height="120"></asp:TextBox>
                                                    <asp:Button ID="adkjhgdComments" CssClass="btn btn-default" runat="server" Text="Add Comment" OnClick="addComments_Click" />

                                                </div>

                                                <hr />
                                                <h5>Refix Comments</h5>
                                                <b>Latest comment:</b>
                                                <p class="text-white" id="lkjhg" runat="server"></p>
                                                <label class="btn btn-green btn-lg pull-right" data-target="#modalRefixComments" data-toggle="modal">View Comments</label>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>


                            <div class="col-md-6">

                                <div class="form-group form-group-default">
                                    <label>Completion Date : <span style="color: red;">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="CompletedDate" runat="server" placeholder="Estimated date work will be completed" CssClass="form-control datepicker-range" required></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group form-group-default" style="display: none;">
                                    <div class="controls">
                                        <label>Type Of COC :</label>
                                        <asp:RadioButton ID="NormalCOC" GroupName="TypeofCOC" runat="server" CssClass="radio" Text="Normal C.O.C" />
                                        <asp:RadioButton ID="SalesCOC" GroupName="TypeofCOC" runat="server" CssClass="radio" Text="Sales C.O.C" />
                                        <asp:RadioButton ID="PreInstallCOC" GroupName="TypeofCOC" runat="server" CssClass="radio" Text="Pre-Install C.O.C" />
                                    </div>
                                </div>


                                <div style="display: none;">
                                    <h5>Insurance Details</h5>
                                    <div class="form-group form-group-default">
                                        <label>Insurance Company :</label>
                                        <div class="controls">
                                            <asp:TextBox ID="InsuranceCompany" runat="server" placeholder="Enter Your Insurance Company" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group form-group-default">
                                        <label>Policy Holder :</label>
                                        <div class="controls">
                                            <asp:TextBox ID="PolicyHolder" runat="server" placeholder="Enter Your Policy Holder" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group form-group-default">
                                        <label>Policy Number :</label>
                                        <div class="controls">
                                            <asp:TextBox ID="PolicyNumber" runat="server" placeholder="Enter Your Policy Number" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group form-group-default">
                                        <label>Insurer is Bank :</label>
                                        <div class="controls">
                                            <asp:DropDownList ID="isBank" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="">Not a Bank</asp:ListItem>
                                                <asp:ListItem Value="ABSA">ABSA</asp:ListItem>
                                                <asp:ListItem Value="Standard Bank">Standard Bank</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Tel Number : <span style="color: red;">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="CustomerCellNo" runat="server" placeholder="Enter Customers Cell Number" CssClass="form-control phone" required></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Email Address : <span style="color: red;">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="CustomerEmail" runat="server" placeholder="Enter Customers Email Address" TextMode="Email" CssClass="form-control" required></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Street : <span style="color: red;">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="AddressStreet" runat="server" placeholder="Enter Your Street" CssClass="form-control" required></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Suburb : <span style="color: red;">*</span></label>
                                    <div class="controls">
                                        <%--<asp:TextBox ID="selSuburb" runat="server" placeholder="Enter Your Suburb" CssClass="form-control"></asp:TextBox>--%>
                                        <asp:DropDownList ID="selSuburb" runat="server" CssClass="form-control select2" required OnSelectedIndexChanged="selSuburb_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>City : <span style="color: red;">*</span></label>
                                    <div class="controls">
                                        <%--<asp:TextBox ID="AddressCity" runat="server" placeholder="Enter Your City" CssClass="form-control" required></asp:TextBox>--%>
                                        <asp:DropDownList ID="AddressCity" runat="server" CssClass="form-control select2" required></asp:DropDownList>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-6">

                                <div class="form-group form-group-default">
                                    <label>Customer Name : <span style="color: red;">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="CustomerName" runat="server" placeholder="Enter Customers Name" CssClass="form-control" required></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Customer Surname : <span style="color: red;">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="CustomerSurname" runat="server" placeholder="Enter Customers Surname" CssClass="form-control" required></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Alternate Tel Number : <span style="color: red;">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="CustomerCellNoAlt" runat="server" placeholder="Enter Customers Alternate Tel Number" CssClass="form-control phone" required></asp:TextBox>
                                    </div>
                                </div>


                                <div>


                                    <div class="form-group form-group-default">
                                        <label>Province : <span style="color: red;">*</span></label>
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

                                    <div class="form-group form-group-default hide">
                                        <label>Area Code :</label>
                                        <div class="controls">
                                            <asp:TextBox ID="AddressAreaCode" runat="server" placeholder="Enter Your Area Code" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group form-group-default hide">
                                        <label>Map Location :</label>
                                        <div class="controls">
                                            <asp:TextBox ID="PhysicalAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">
                                <div class="form-group form-group-default" style="display: none;">
                                    <label>Address</label>
                                    <div class="row">
                                        <div class="col-md-11">
                                            <asp:TextBox ID="CustomerFullAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1 text-right" id="CustomerMap" runat="server">
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group form-group-default" style="display: none;">
                                    <label>Period of Insurance</label>
                                    <div class="controls">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label>From :</label>
                                                <asp:TextBox ID="PeriodOfInsuranceFrom" runat="server" placeholder="Enter Your Period of Insurance" CssClass="form-control datepicker-range"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label>To :</label>
                                                <asp:TextBox ID="PeriodOfInsuranceTo" runat="server" placeholder="Enter Your Period of Insurance" CssClass="form-control datepicker-range"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12 hide" id="Mapvisuals">
                                <h3>Please drag the pin to the specific address</h3>
                                <div class="form-group" style="display: none;">
                                    <label>Latitude:</label>
                                    <input type="text" class="form-control" id="us3-lat">
                                </div>

                                <div class="form-group" style="display: none;">
                                    <label>Longitude:</label>
                                    <input type="text" class="form-control" id="us3-lon">
                                </div>
                                <div class="form-group" style="display: none;">
                                    <label>Location:</label>
                                    <input type="text" class="form-control" id="us3-address">
                                </div>
                                <div class="form-group">
                                    <div id="us3" class="map-wrapper" style="height: 300px;"></div>
                                </div>
                            </div>

                            <div class="col-md-6">

                                <div class="form-group form-group-default hide">
                                    <div class="controls">
                                        <label>Completion of Work :</label>
                                        <div id="CompletionofWork" runat="server"></div>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <div class="controls">
                                        <label>Description of Work : <span style="color: red;">*</span></label>
                                        <asp:TextBox ID="DescriptionofWork" runat="server" TextMode="MultiLine" CssClass="form-control" Height="120" required></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-6">

                                <div class="form-group form-group-default">
                                    <div class="controls">
                                        <label>Type Of Installation : <span style="color: red;">*</span></label>
                                        <asp:CheckBoxList ID="TypeOfInstallation" CssClass="checkbox" runat="server" required>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>



                            </div>



                            <div class="col-md-12 text-right hide">

                                <asp:Button ID="btn_updateDetails" CssClass="btn btn-primary" runat="server" Text="Update Details" OnClick="btn_updateDetails_Click" />
                                <hr />

                            </div>


                            <div class="col-md-12 hide">
                                <h4>C.O.C Forms</h4>

                                <div class="form-group form-group-default">
                                    <label>Current Progress: </label>
                                    <div class="controls">
                                        <div class="progress " id="progressBarStatus" runat="server"></div>
                                    </div>
                                </div>
                                <div class="form-group form-group-default">
                                    <label>Complete all forms below before Submitting</label>
                                    <div class="controls" id="FormLinks" runat="server" style="padding: 15px;"></div>
                                </div>
                            </div>



                            <div class="form-group form-group-default">
                                <label>Non Compliance Details (details of non-compliance of the plumbing installation that were not carried out by you:</label>
                                <div class="controls">
                                    <asp:TextBox ID="nonCompTxtbx" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Height="120px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="form-group form-group-default" id="paperBase" runat="server">
                                    <div class="controls">
                                        <div class="alert alert-primary" role="alert">
                                            Take a photo of your paperbased COC denoting all details and upload.
                                        </div>
                                        <label>Paper Based COC :</label>
                                        <div class="col-md-2">
                                            <asp:FileUpload ID="paperBasedCOC" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-md-10">
                                            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Upload COC" OnClick="Button1_Click" formnovalidate />
                                            <div runat="server" id="PaperCOCDisp" style="float: right;"></div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="form-group form-group-default">
                                <label>Add images of the completed job:</label>
                                <div runat="server" id="dispCompltImgs"></div>
                                <div class="controls">
                                    <asp:FileUpload ID="FileUpload3" CssClass="form-control" runat="server" />
                                    <asp:Button ID="Button3" runat="server" Text="Upload Image" CssClass="btn btn-primary" OnClick="Button3_Click1" formnovalidate />
                                </div>
                            </div>

                            <div class="form-group form-group-default">
                                <p><b>I <%=Session["IIT_UName"].ToString()%>, Licensed registration number <%=Session["IIT_RegNo"].ToString()%>, certify that, the above compliance certifcate details are true and correct and will be logged in accordance with the prescribed requirements as defned by the PIRB.</b></p>
                                <p><b>I further certify that;</b></p>
                                <label>Tick A / B: <span style="color: red;">*</span></label>
                                <div class="controls">
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                        <asp:ListItem Value="A">A: The above plumbing work was carried out by me or under my supervision, and that it complies in all respects to the plumbing regulations, laws, National Compulsory Standards and Local bylaws.</asp:ListItem>
                                        <asp:ListItem Value="B">B: I have fully inspected and tested the work started but not completed by another Licensed plumber. I further certify that the inspected and tested work and the necessary completion work was carried out by me or under my supervision- complies in all respects to the plumbing regulations, laws, National Compulsory Standards and Local bylaws.</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                            <asp:TextBox ID="submitAudPin" runat="server" Style="display: none;"></asp:TextBox>
                        </div>
                    </div>
                    <div class="tab-pane" id="auditreport">

                        <div id="hideAudit" runat="server">
                            <div class="col-md-6">
                                <h4>Audit Report</h4>
                                <div class="form-group form-group-default">
                                    <label>Audit Date: </label>
                                    <div class="controls">
                                        <asp:TextBox ID="InspectionDate" runat="server" placeholder="Date you inspected" CssClass="form-control datepicker-range" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group form-group-default">
                                    <label>Overall Workmanship Rating: </label>
                                    <div class="controls">
                                        <asp:DropDownList ID="Quality" runat="server" CssClass="form-control" ReadOnly="true">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="Excellent">Excellent</asp:ListItem>
                                            <asp:ListItem Value="Good">Good</asp:ListItem>
                                            <asp:ListItem Value="Poor">Poor</asp:ListItem>
                                            <asp:ListItem Value="Bad">Bad</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <h5>&nbsp;</h5>
                                <div class="form-group form-group-default">
                                    <label>Refix Details</label>
                                    <div id="refixdetails" runat="server"></div>
                                </div>
                                <div class="form-group form-group-default">
                                    <label>Files/Pictures :</label>
                                    <div class="controls">
                                        <div style="display: none;">
                                            <asp:FileUpload ID="FileUpload1" runat="server" /></div>
                                        <div id="AuditPicture" runat="server"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="hideAudita" runat="server">
                            <div class="form-group form-group-default">
                                <label>Comments</label>
                                <asp:TextBox ID="RefixComments" runat="server" TextMode="MultiLine" CssClass="form-control" Height="120"></asp:TextBox>
                                <asp:Button ID="addComments" CssClass="btn btn-default" runat="server" Text="Add Comment" OnClick="addComments_Click" />
                            </div>

                            <div class="form-group form-group-default">
                                <div class="controls">
                                    <h5>Comments</h5>
                                    <b>Latest Comment: </b>
                                    <p id="latestCommentPosted" runat="server"></p>
                                    <label class="btn btn-green btn-lg pull-right" data-target="#modalRefixComments" data-toggle="modal" id="btnFillSizeToggler2">View Comments</label>
                                </div>
                            </div>

                            <h4>Audit Review</h4>
                            <div class="form-group form-group-default">
                                <label>Current Reviews: </label>
                                <div class="controls">
                                    <div id="CurrentReview" runat="server"></div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="col-md-12">


                    <div class="alert alert-danger" id="Div1" runat="server"></div>

                    <div class="form-group form-group-default text-right">
                        <div class="controls">
                            <span id="showreportbtn" runat="server"></span>
                            <asp:Button ID="btnSubmitCompleteRifxes" CssClass="btn btn-primary" runat="server" Text="Submit Refixes" OnClick="btnSubRefix_Click" />
                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save COC" OnClick="btnSave_Click" UseSubmitBehavior="false" formnovalidate />
                            <span id="subbtnhideorshow" runat="server" visible="false">
                                <label style="min-width: 100px; float: right; margin-left: 5px;" onclick="submitAuditSendOtp()" disabled class="btn btn-success undisablethisbutton" id="subButton">Log COC</label>
                                <%-- <button style="min-width:100px;float:right;margin-left:5px;" onclick="submitAuditSendOtp()" class="btn btn-success" id="subButton">Log COC New</button>--%></span>
                            <%--<asp:Button ID="btnSubmit" CssClass="btn btn-success" runat="server" Text="Submit Audit" OnClick="btnSubmit_Click" />--%>
                        </div>
                    </div>

                </div>

            </div>
        </div>
        <asp:TextBox ID="latitudeHidden" CssClass="form-control hide" runat="server"></asp:TextBox>
        <asp:TextBox ID="longitudeHidden" CssClass="form-control hide" runat="server"></asp:TextBox>
        <asp:TextBox ID="populateLatitudeValue" CssClass="form-control hide" runat="server"></asp:TextBox>
        <asp:TextBox ID="populateLongitudeValue" CssClass="form-control hide" runat="server"></asp:TextBox>
    </div>

    <!-- Modal -->
    <div class="modal fade fill-in" id="modalRefixComments" tabindex="-1" role="dialog" aria-hidden="true">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
            <i class="pg-close"></i>
        </button>
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="text-left p-b-5"><span class="semi-bold">Refix</span> comments</h5>
                </div>
                <div class="modal-body bg-white" style="overflow-y: scroll; padding: 10px;">

                    <div class="timeline-container top-circle">
                        <section class="timeline">


                            <div class="timeline-block">
                                <div class="timeline-point success">
                                    <i class="pg-map"></i>
                                </div>
                                <!-- timeline-point -->
                                <div class="timeline-content">
                                    <div class="card share full-width">
                                        <div class="card-header clearfix">
                                            <h5 id="plumberName" runat="server"></h5>
                                        </div>
                                        <div class="card-description">
                                            <div id="plumberComments" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="event-date">
                                        <small class="fs-12 hint-text" id="plumberDatePosted" runat="server"></small>
                                    </div>
                                </div>
                                <!-- timeline-content -->
                            </div>

                            <!-- timeline-block -->
                            <div class="timeline-block">
                                <div class="timeline-point small">
                                </div>
                                <!-- timeline-point -->
                                <div class="timeline-content">
                                    <div class="card share full-width">
                                        <div class="card-header clearfix">
                                            <h5 id="inspectorName" runat="server"></h5>
                                        </div>
                                        <div class="card-description">
                                            <div id="inspectorComments" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="event-date">
                                        <small class="fs-12 hint-text" id="inspectorDatePosted" runat="server"></small>
                                    </div>
                                </div>
                                <!-- timeline-content -->
                            </div>

                        </section>
                        <!-- timeline -->
                    </div>

                </div>
                <div class="modal-footer">
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <div class="modal fade fill-in" id="modalOTPpin" tabindex="-1" role="dialog" aria-hidden="true">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
            <i class="pg-close"></i>
        </button>
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="text-left p-b-5"><span class="semi-bold">OTP</span> Pin</h5>
                </div>
                <div class="modal-body bg-white" style="overflow-y: scroll; padding: 10px;">
                    <p>By entering the OTP below, I <%=Session["IIT_UName"].ToString()%><small id="plumberNameSurname" runat="server"></small> declare that I understand:<br />
                    </p>
                    <ul>
                        <li>That all the information provided on the COC is true and correct;</li>
                        <li>That one the COC has been log the information on the COC cannot be change or altered</li>
                        <li>That all the plumbing works comply in all respect to the plumbing regulations and laws as defined by the National Compulsory Standards and Local By-Laws;</li>
                        <li>That I will fully comply to the PIRB’s auditing, rectification and disciplinary, policies and procedures; and if I fail to comply with these policy procedures it may result in disciplinary action being taken against me, which could result in my suspension from the PIRB;  </li>
                        <li>That as a Licensed plumber and registered I shall abide by the PIRB Code of Conduct. </li>
                    </ul>

                    <asp:TextBox ID="submitOTPpin" CssClass="form-control" runat="server" placeholder="Enter your OTP here"></asp:TextBox><br />
                    <asp:Button ID="btnSubmit" CssClass="btn btn-success pull-right" runat="server" Text="Log COC" OnClick="btnSubmit_Click" />
                </div>
                <div class="modal-footer">
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <div class="modal fade fill-in" id="uploadImages" tabindex="-1" role="dialog" aria-hidden="true">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
            <i class="pg-close"></i>
        </button>
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="text-left p-b-5"><span class="semi-bold">Upload</span> Image</h5>
                </div>
                <div class="modal-body bg-white" style="overflow-y: scroll; padding: 10px;">
                    <asp:TextBox ID="TextBox1" runat="server" CssClass=""></asp:TextBox>
                    <asp:FileUpload ID="FileUpload2" CssClass="form-control" runat="server" /><br />
                    <asp:Button ID="Button2" CssClass="btn btn-success pull-right" runat="server" Text="Upload" OnClick="Button3_Click" />
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>


    <%--<script src="assets/js/jquery.min.js"></script>--%>
    <%--<script src="assets/js/select2.min.js"></script>--%>
    <script src="assets/plugins/bootstrap-select2/select2.js"></script>
    <script src="assets/js/bootstrap_select.min.js"></script>

    <script type="text/javascript" src="assets/js/plugins/forms/inputs/typeahead/typeahead.bundle.min.js"></script>
    <script type="text/javascript" src="assets/js/plugins/forms/inputs/typeahead/handlebars.min.js"></script>

    <script type="text/javascript" src='https://maps.google.com/maps/api/js?key=AIzaSyDTm83EWH3_x8ttFP_D9k3NH_H3OYWGBsI&amp;libraries=places'></script>
    <script type="text/javascript" src="assets/js/location/typeahead_addresspicker.js"></script>
    <script type="text/javascript" src="assets/js/location/autocomplete_addresspicker.js"></script>

    <script>
        $('#<%=RadioButtonList1.ClientID%>').click(function () {
            $('.undisablethisbutton').removeAttr("disabled")

        });

        $('.select2').select2({
            // selectOnClose: true
        });

        var AorB = '<%=radioSelected%>';
        console.log("AorB   : " + AorB);

        if (AorB != '' && AorB != null) {
            $('.undisablethisbutton').removeAttr("disabled");
        }

        var defaultLatitudes = "";
        var defaultlongitudes = "";

        var addressExist = "<%=addressExists.ToString()%>";
        <%--var latfromdb = '<%=latitudeFromDB.ToString()%>';
        var lngfromdb = '<%=longitudeFromDB.ToString()%>';--%>
        var latfromdb = $("#<%=populateLatitudeValue.Text%>").val();
        var lngfromdb = $("#<%=populateLongitudeValue.Text%>").val();

        console.log("addressExist : " + addressExist);
        console.log("latfrmdb : " + latfromdb);
        console.log("lngfrmdb : " + lngfromdb);




        if (addressExist == "true") {

            defaultLatitudes = latfromdb;
            defaultlongitudes = lngfromdb;
            $("#Mapvisuals").addClass("hide");

        } else {

            //navigator.geolocation.getCurrentPosition(onSuccessMark, onErrorMark);

        }

        function onSuccessMark(position) {

            console.log('Latitude: ' + position.coords.latitude + '\n' +
                'Longitude: ' + position.coords.longitude + '\n' +
                'Altitude: ' + position.coords.altitude + '\n' +
                'Accuracy: ' + position.coords.accuracy + '\n' +
                'Altitude Accuracy: ' + position.coords.altitudeAccuracy + '\n' +
                'Heading: ' + position.coords.heading + '\n' +
                'Speed: ' + position.coords.speed + '\n' +
                'Timestamp: ' + new Date(position.timestamp) + '\n');

            defaultLatitudes = position.coords.latitude;
            defaultlongitudes = position.coords.longitude;

            console.log("lats : " + defaultLatitudes);
            console.log("long : " + defaultlongitudes);

            (function ($) {

                /**
                 * Holds google map object and related utility entities.
                 * @constructor
                 */
                function GMapContext(domElement, options) {
                    var _map = new google.maps.Map(domElement, options);
                    var _marker = new google.maps.Marker({
                        position: new google.maps.LatLng(54.19335, -3.92695),
                        map: _map,
                        title: "Drag Me",
                        draggable: options.draggable
                    });
                    return {
                        map: _map,
                        marker: _marker,
                        circle: null,
                        location: _marker.position,
                        radius: options.radius,
                        locationName: options.locationName,
                        settings: options.settings,
                        domContainer: domElement,
                        geodecoder: new google.maps.Geocoder()
                    }
                }

                // Utility functions for Google Map Manipulations
                var GmUtility = {
                    /**
                     * Draw a circle over the the map. Returns circle object.
                     * Also writes new circle object in gmapContext.
                     *
                     * @param center - LatLng of the center of the circle
                     * @param radius - radius in meters
                     * @param gmapContext - context
                     * @param options
                     */
                    drawCircle: function (gmapContext, center, radius, options) {
                        if (gmapContext.circle != null) {
                            gmapContext.circle.setMap(null);
                        }
                        if (radius > 0) {
                            radius *= 1;
                            options = $.extend({
                                strokeColor: "#0000FF",
                                strokeOpacity: 0.35,
                                strokeWeight: 2,
                                fillColor: "#0000FF",
                                fillOpacity: 0.20
                            }, options);
                            options.map = gmapContext.map;
                            options.radius = radius;
                            options.center = center;
                            gmapContext.circle = new google.maps.Circle(options);
                            return gmapContext.circle;
                        }
                        return null;
                    },
                    /**
                     *
                     * @param gMapContext
                     * @param location
                     * @param callback
                     */

                    setPosition: function (gMapContext, location, callback) {
                        gMapContext.location = location;
                        gMapContext.marker.setPosition(location);
                        gMapContext.map.panTo(location);
                        this.drawCircle(gMapContext, location, gMapContext.radius, {});
                        if (gMapContext.settings.enableReverseGeocode) {
                            gMapContext.geodecoder.geocode({ latLng: gMapContext.location }, function (results, status) {
                                if (status == google.maps.GeocoderStatus.OK && results.length > 0) {
                                    gMapContext.locationName = results[0].formatted_address;
                                    console.log(results[0]);
                                    if ($("#<%=CustomerName.ClientID%>").val() == '') {
                                        console.log("empty");
                                        $("#<%=AddressStreet.ClientID%>").val("");
                                        $("#<%=AddressCity.ClientID%>").val("");
                                        $("#<%=selSuburb.ClientID%>").val("");//.toUpperCase() .trigger("change")
                                        $("#<%=Province.ClientID%>").val("");
                                    }
                                    else {
                                        console.log("not nempty");
                                        $("#<%=AddressStreet.ClientID%>").val(results[0].address_components[0].long_name + ' ' + results[0].address_components[1].long_name);
                                        $("#<%=AddressCity.ClientID%>").val(results[0].address_components[3].long_name).trigger("change");
                                        $("#<%=selSuburb.ClientID%>").val(results[0].address_components[2].long_name).trigger("change");//.toUpperCase() 
                                        $("#<%=Province.ClientID%>").val(results[0].address_components[5].long_name);
                                    }

                                    ////
                                }
                                if (callback) {
                                    callback.call(this, gMapContext);
                                }
                            });
                        } else {
                            if (callback) {
                                callback.call(this, gMapContext);
                            }
                        }

                    },
                    locationFromLatLng: function (lnlg) {
                        console.log("latudie new : " + lnlg.lat());
                        console.log("longtude new : " + lnlg.lng());
                        $("#<%=latitudeHidden.ClientID%>").val(lnlg.lat());
                        $("#<%=longitudeHidden.ClientID%>").val(lnlg.lng());
                        return { latitude: lnlg.lat(), longitude: lnlg.lng() }
                    }
                }

                function isPluginApplied(domObj) {
                    return getContextForElement(domObj) != undefined;
                }

                function getContextForElement(domObj) {
                    return $(domObj).data("locationpicker");
                }

                function updateInputValues(inputBinding, gmapContext) {
                    if (!inputBinding) return;
                    var currentLocation = GmUtility.locationFromLatLng(gmapContext.location);
                    if (inputBinding.latitudeInput) {
                        inputBinding.latitudeInput.val(currentLocation.latitude);
                    }
                    if (inputBinding.longitudeInput) {
                        inputBinding.longitudeInput.val(currentLocation.longitude);
                    }
                    if (inputBinding.radiusInput) {
                        inputBinding.radiusInput.val(gmapContext.radius);
                    }
                    if (inputBinding.locationNameInput) {
                        inputBinding.locationNameInput.val(gmapContext.locationName);
                    }
                }

                function setupInputListenersInput(inputBinding, gmapContext) {
                    if (inputBinding) {
                        if (inputBinding.radiusInput) {
                            inputBinding.radiusInput.on("change", function () {
                                gmapContext.radius = $(this).val();
                                GmUtility.setPosition(gmapContext, gmapContext.location, function (context) {
                                    context.settings.onchanged(GmUtility.locationFromLatLng(context.location), context.radius, false);
                                });
                            });
                        }
                        if (inputBinding.locationNameInput && gmapContext.settings.enableAutocomplete) {
                            gmapContext.autocomplete = new google.maps.places.Autocomplete(inputBinding.locationNameInput.get(0));
                            google.maps.event.addListener(gmapContext.autocomplete, 'place_changed', function () {
                                var place = gmapContext.autocomplete.getPlace();
                                if (!place.geometry) {
                                    gmapContext.settings.onlocationnotfound(place.name);
                                    return;
                                }
                                GmUtility.setPosition(gmapContext, place.geometry.location, function (context) {
                                    updateInputValues(inputBinding, context);
                                    context.settings.onchanged(GmUtility.locationFromLatLng(context.location), context.radius, false);
                                });
                            });
                        }
                        if (inputBinding.latitudeInput) {
                            inputBinding.latitudeInput.on("change", function () {
                                GmUtility.setPosition(gmapContext, new google.maps.LatLng($(this).val(), gmapContext.location.lng()), function (context) {
                                    context.settings.onchanged(GmUtility.locationFromLatLng(context.location), context.radius, false);
                                });
                            });
                        }
                        if (inputBinding.longitudeInput) {
                            inputBinding.longitudeInput.on("change", function () {
                                GmUtility.setPosition(gmapContext, new google.maps.LatLng(gmapContext.location.lat(), $(this).val()), function (context) {
                                    context.settings.onchanged(GmUtility.locationFromLatLng(context.location), context.radius, false);
                                });
                            });
                        }
                    }
                }

                /**
                 * Initialization:
                 *  $("#myMap").locationpicker(options);
                 * @param options
                 * @param params
                 * @returns {*}
                 */
                $.fn.locationpicker = function (options, params) {
                    if (typeof options == 'string') { // Command provided
                        var _targetDomElement = this.get(0);
                        // Plug-in is not applied - nothing to do.
                        if (!isPluginApplied(_targetDomElement)) return;
                        var gmapContext = getContextForElement(_targetDomElement);
                        switch (options) {
                            case "location":
                                if (params == undefined) { // Getter
                                    var location = GmUtility.locationFromLatLng(gmapContext.location);
                                    location.radius = gmapContext.radius;
                                    location.name = gmapContext.locationName;
                                    return location;
                                } else { // Setter
                                    if (params.radius) {
                                        gmapContext.radius = params.radius;
                                    }
                                    GmUtility.setPosition(gmapContext, new google.maps.LatLng(params.latitude, params.longitude), function (gmapContext) {
                                        updateInputValues(gmapContext.settings.inputBinding, gmapContext);
                                    });
                                }
                                break;
                            case "subscribe":
                                /**
                                 * Provides interface for subscribing for GoogleMap events.
                                 * See Google API documentation for details.
                                 * Parameters:
                                 * - event: string, name of the event
                                 * - callback: function, callback function to be invoked
                                 */
                                if (options == undefined) { // Getter is not available
                                    return null;
                                } else {
                                    var event = params.event;
                                    var callback = params.callback;
                                    if (!event || !callback) {
                                        console.error("LocationPicker: Invalid arguments for method \"subscribe\"")
                                        return null;
                                    }
                                    google.maps.event.addListener(gmapContext.map, event, callback);
                                }

                                break;
                        }
                        return null;
                    }
                    return this.each(function () {
                        var $target = $(this);
                        // If plug-in hasn't been applied before - initialize, otherwise - skip
                        if (isPluginApplied(this)) return;
                        // Plug-in initialization is required
                        // Defaults
                        var settings = $.extend({}, $.fn.locationpicker.defaults, options);
                        // Initialize
                        var gmapContext = new GMapContext(this, {
                            zoom: settings.zoom,
                            center: new google.maps.LatLng(settings.location.latitude, settings.location.longitude),
                            mapTypeId: google.maps.MapTypeId.ROADMAP,
                            mapTypeControl: false,
                            disableDoubleClickZoom: false,
                            scrollwheel: settings.scrollwheel,
                            streetViewControl: false,
                            radius: settings.radius,
                            locationName: settings.locationName,
                            settings: settings,
                            draggable: settings.draggable
                        });
                        $target.data("locationpicker", gmapContext);
                        // Subscribe GMap events
                        google.maps.event.addListener(gmapContext.marker, "dragend", function (event) {
                            GmUtility.setPosition(gmapContext, gmapContext.marker.position, function (context) {
                                var currentLocation = GmUtility.locationFromLatLng(gmapContext.location);
                                context.settings.onchanged(currentLocation, context.radius, true);
                                updateInputValues(gmapContext.settings.inputBinding, gmapContext);
                            });
                        });
                        GmUtility.setPosition(gmapContext, new google.maps.LatLng(settings.location.latitude, settings.location.longitude), function (context) {
                            updateInputValues(settings.inputBinding, gmapContext);
                            context.settings.oninitialized($target);
                        });
                        // Set up input bindings if needed
                        setupInputListenersInput(settings.inputBinding, gmapContext);
                    });
                };
                $.fn.locationpicker.defaults = {
                    location: { latitude: 40.7324319, longitude: -73.82480799999996 },
                    locationName: "",
                    radius: 500,
                    zoom: 15,
                    scrollwheel: true,
                    inputBinding: {
                        latitudeInput: null,
                        longitudeInput: null,
                        radiusInput: null,
                        locationNameInput: null
                    },
                    enableAutocomplete: false,
                    enableReverseGeocode: true,
                    draggable: true,
                    onchanged: function (currentLocation, radius, isMarkerDropped) { },
                    onlocationnotfound: function (locationName) { },
                    oninitialized: function (component) { }

                }

            }(jQuery));

            var markers = [];

            function setMapOnAll(map) {
                for (var i = 0; i < markers.length; i++) {
                    markers[i].setMap(map);
                }
                markers = [];
            }

            $('#us3').locationpicker({
                location: { latitude: defaultLatitudes, longitude: defaultlongitudes },
                radius: 1,
                scrollwheel: false,
                zoom: 14,
                inputBinding: {
                    latitudeInput: $('#us3-lat'),
                    longitudeInput: $('#us3-lon'),
                    locationNameInput: $('#us3-address')
                },
                enableReverseGeocode: true,
                enableAutocomplete: true,
                onchanged: function (currentLocation, radius, isMarkerDropped) {
                    //console.log(gMapContext.locationName, currentLocation, radius, isMarkerDropped);
                    $("#<%=PhysicalAddress.ClientID%>").val($('#us3-address').val());
                }
            });
        }

        // onError Callback receives a PositionError object
        //
        function onErrorMark(error) {
            console.log('code: ' + error.code + '\n' +
                'message: ' + error.message + '\n');
        }

        if ($("#<%=submitAudPin.ClientID%>").val() == "true") {
            //$("#subButton").removeAttr('disabled','disabled');
        }
        else {
            //$("#subButton").attr('disabled','disabled');
        }

        var today = new Date();
        $('.datepicker-range').datepicker({
            format: 'dd-mm-yyyy',
            endDate: "today",
            maxDate: today
        });

        // Only for fillin modals so that the backdrop action is still there
        $('#modalFillIn').on('show.bs.modal', function (e) {
            $('body').addClass('fill-in-modal');
        })
        $('#modalFillIn').on('hidden.bs.modal', function (e) {
            $('body').removeClass('fill-in-modal');
        })

        //function deleteImage(id) {
        //    if (confirm('Are you sure?')) {
        //        $.post('https://197.242.82.242/inspectit/api/frmImgDel?imgid=' + id, { }, function (data) {
        //            $("#show_img_" + id).addClass("hide");
        //        })
        //    }
        //}
        function deleteImage(id, pid) {
            var result = confirm("Are you sure?");
            if (result) {
                document.location.href = "DeleteItems.aspx?op=delImgecocPlumber&id=" + id + "&pid=" + pid;
            }
        }

        function deleteImagePDF(id) {
            var result = confirm("Are you sure?");
            if (result) {
                document.location.href = "DeleteItems.aspx?op=delPdfImgcoc&pid=0&id=" + id;
            }
        }

        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        var cocid = getUrlVars()["cocid"];

        $("#<%=Div1.ClientID%>").hide();
        function submitAuditSendOtp() {
            $.ajax({
                type: "POST",
                url: 'https://197.242.82.242/inspectit/API/WebService1.asmx/paperCocExists',
                data: { uid:  <%=Session["IIT_UID"].ToString()%> , cocid: cocid },
                success: function (data) {
                    if (data == "false") {
                        $("#<%=Div1.ClientID%>").show();
                        $("#<%=Div1.ClientID%>").html("You can't log the COC until you upload your paper based COC");
                    }
                    else {
                        if ($("#<%=TypeOfInstallation.ClientID%> input:checkbox:checked").length > 0) {
                            //alert("okay!");
                            if ($("#<%=CompletedDate.ClientID%>").val() == "" || $("#<%=CustomerName.ClientID%>").val() == "" || $("#<%=CustomerSurname.ClientID%>").val() == "" || $("#<%=CustomerCellNo.ClientID%>").val() == "" || $("#<%=CustomerEmail.ClientID%>").val() == "" || $("#<%=CustomerCellNoAlt.ClientID%>").val() == "" || $("#<%=Province.ClientID%>").val() == "" || $("#<%=AddressStreet.ClientID%>").val() == "" || $("#<%=selSuburb.ClientID%>").val() == "" || $("#<%=AddressCity.ClientID%>").val() == "" || $("#<%=DescriptionofWork.ClientID%>").val() == "") {
                                $("#<%=Div1.ClientID%>").html("Please complete all missing fields");
                                $("#<%=Div1.ClientID%>").show();
                            }
                            else {
                                // veronike change localhost to live
                                $.get('https://197.242.82.242/inspectit/api/sendOTP?uid=' +  <%=Session["IIT_UID"].ToString()%> + "&cocid=" + cocid, {}, function (data) {
                                    $('#modalOTPpin').modal('toggle');
                                });
                            }
                        }
                        else {
                            //alert("failr");
                            $("#<%=Div1.ClientID%>").html("Please select an Installation Type");
                            $("#<%=Div1.ClientID%>").show();
                        }
                    }
                },
            });




        }

        function markasFixed(id) {
            $.get('https://197.242.82.242/inspectit/markRefixFixed?id=' + id + "&op=plumbFix", {}, function (data) {
                location.reload();
            });
        }

        function uploadImg(revID, coc) {
            $('#uploadImages').modal('toggle');
            $('#<%=TextBox1.ClientID%>').val(revID);

        }

<%--        $("#<%=CustomerCellNo.ClientID%>").focus(function () {
            console.log("Handler for .focus() called.");
            $("#<%=CustomerCellNo.ClientID%>").setCursorPosition(0);
        });--%>


</script>

</asp:Content>
