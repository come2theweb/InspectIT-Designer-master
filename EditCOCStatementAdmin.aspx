<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditCOCStatementAdmin.aspx.cs" Inherits="InspectIT.EditCOCStatementAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/css/components.css" rel="stylesheet" />
    <link href="http://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.9/summernote.css" rel="stylesheet" />
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
                    <li class="active"><a href="#TabAudit" data-toggle="tab" role="tab">Audit Details</a></li>
                    <li class=""><a href="#TabActive" data-toggle="tab" role="tab">COC Details</a></li>
                    <li class=""><a href="#plumberdetails" data-toggle="tab" role="tab">Plumber Details</a></li>
                    <li class=""><a href="#TabAuditReport" data-toggle="tab" role="tab">Audit Report</a></li>
                    <li class=""><a href="#adminComments" data-toggle="tab" role="tab">Admin Comments</a></li>
                    <li class=""><a href="#TabLog" data-toggle="tab" role="tab">Diary</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane" id="adminComments">
                        <div class="col-md-12">

                            <h4>Admin &amp; Inspector Comments</h4>
                            <div class="form-group form-group-default">
                                <label>Comments between Admin & Inspector</label>
                                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" CssClass="form-control" Height="120"></asp:TextBox>
                                <asp:Button ID="Button1" CssClass="btn btn-default" runat="server" Text="Add Comment" OnClick="addPrivateComments_Click" />
                                <br />
                                <b>Latest comment:</b>
                                <p class="text" id="lastprivatecomm" runat="server"></p>
                                <label class="btn btn-green btn-lg pull-right" data-target="#modalPrivateComments" data-toggle="modal">View Comments</label>
                            </div>

                        </div>



                        <div class="col-md-12">
                            <h4>Admin Comments</h4>
                            <div class="form-group form-group-default">
                                <label>Admin Comments (only you as the administrator will see this)</label>
                                <p id="P2" runat="server"></p>
                                <asp:TextBox ID="TextBox9" runat="server" TextMode="MultiLine" CssClass="form-control" Height="120"></asp:TextBox>
                                <asp:Button ID="Button4" CssClass="btn btn-default" runat="server" Text="Add Comment" OnClick="addplumberHis_Click" />
                            </div>
                        </div>


                    </div>
                    <div class="tab-pane" id="TabAuditReport">
                        <div class="col-md-6">
                            <h4>Audit Report</h4>
                            <div class="form-group form-group-default">
                                <label>Audit Date: </label>
                                <div class="controls">
                                    <asp:TextBox ID="InspectionDate" runat="server" placeholder="Date you inspected" CssClass="form-control datepicker-range"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Overall Workmanship Rating: </label>
                                <div class="controls">
                                    <asp:DropDownList ID="Quality" runat="server" CssClass="form-control">
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
                                <div class="controls">
                                    <asp:TextBox ID="NoDaysToComplete" runat="server" placeholder="When must it be complete by" CssClass="form-control datepicker-range"></asp:TextBox>
                                </div>
                                <div id="refixdetails" runat="server" style="display: none;"></div>
                            </div>
                            <div class="form-group form-group-default">
                                <label>Files/Pictures :</label>
                                <div class="controls">
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <div id="AuditPicture" runat="server"></div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">

                            <div class="form-group form-group-default">
                                <label>Comments between Inspector &amp; Plumber</label>
                                <asp:TextBox ID="RefixComments" runat="server" TextMode="MultiLine" CssClass="form-control" Height="120"></asp:TextBox>
                                <asp:Button ID="addComments" CssClass="btn btn-default" runat="server" Text="Add Comment" OnClick="addComments_Click" />
                                <br />
                                <b>Comments</b>
                                <b>Latest Comment: </b>
                                <p id="latestCommentPosted" runat="server"></p>
                                <label class="btn btn-green btn-lg pull-right" data-target="#modalRefixComments" data-toggle="modal" id="btnFillSizeToggler2">View Comments</label>

                            </div>

                            <h4>Audit Review</h4>
                            <div class="form-group form-group-default">
                                <label>Current Reviews: </label>
                                <div class="controls">
                                    <div id="CurrentReview" runat="server"></div>
                                </div>
                            </div>

                            <div class="form-group form-group-default text-right">
                                <div class="controls">
                                    <asp:Button ID="btnAddReview" CssClass="btn btn-primary" runat="server" Text="Add Review" OnClick="btnAddReview_Click" />
                                </div>
                            </div>


                            <div class="form-group form-group-default hide">
                                <label>Audit History (only you as the inspector will see this)</label>
                                <p id="auidtHistoryCommentsPosted" runat="server"></p>
                                <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" CssClass="form-control" Height="120"></asp:TextBox>
                                <asp:Button ID="addAudHis" CssClass="btn btn-default" runat="server" Text="Add Audit History" OnClick="addAudHis_Click" />
                            </div>


                            <div class="form-group form-group-default text-right">
                                <div class="controls">
                                    <span id="showreportbtn" runat="server"></span>
                                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save Audit" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnSubmit" CssClass="btn btn-success" runat="server" Text="Submit Audit" OnClick="btnSubmit_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="tab-pane" id="TabActive">

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
                                    <label>Completion Date :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="CompletedDate" runat="server" placeholder="Estimated date work will be completed" CssClass="form-control datepicker-range" ReadOnly="true"></asp:TextBox>
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
                                            <asp:TextBox ID="InsuranceCompany" runat="server" placeholder="Enter Your Insurance Company" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group form-group-default">
                                        <label>Policy Holder :</label>
                                        <div class="controls">
                                            <asp:TextBox ID="PolicyHolder" runat="server" placeholder="Enter Your Policy Holder" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group form-group-default">
                                        <label>Policy Number :</label>
                                        <div class="controls">
                                            <asp:TextBox ID="PolicyNumber" runat="server" placeholder="Enter Your Policy Number" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group form-group-default">
                                        <label>Insurer is Bank :</label>
                                        <div class="controls">
                                            <asp:DropDownList ID="isBank" runat="server" CssClass="form-control" ReadOnly="true">
                                                <asp:ListItem Value="">Not a Bank</asp:ListItem>
                                                <asp:ListItem Value="ABSA">ABSA</asp:ListItem>
                                                <asp:ListItem Value="Standard Bank">Standard Bank</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Tel Number :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="CustomerCellNo" runat="server" placeholder="Enter Customers Name" CssClass="form-control phone" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Email Address :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="CustomerEmail" runat="server" placeholder="Enter Customers Name" TextMode="Email" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-6">

                                <div class="form-group form-group-default">
                                    <label>Customer Name :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="CustomerName" runat="server" placeholder="Enter Customers Name" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Customer Surname :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="CustomerSurname" runat="server" placeholder="Enter Customers Name" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Alternate Tel Number :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="CustomerCellNoAlt" runat="server" placeholder="Enter Customers Name" CssClass="form-control phone" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>


                                <div style="display: none;">
                                    <div class="form-group form-group-default">
                                        <label>Street :</label>
                                        <div class="controls">
                                            <asp:TextBox ID="AddressStreet" runat="server" placeholder="Enter Your Street" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group form-group-default">
                                        <label>Suburb :</label>
                                        <div class="controls">
                                            <asp:TextBox ID="AddressSuburb" runat="server" placeholder="Enter Your Suburb" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group form-group-default">
                                        <label>City :</label>
                                        <div class="controls">
                                            <asp:TextBox ID="AddressCity" runat="server" placeholder="Enter Your City" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group form-group-default">
                                        <label>Province :</label>
                                        <div class="controls">
                                            <asp:DropDownList ID="Province" runat="server" CssClass="form-control" ReadOnly="true">
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
                                            <asp:TextBox ID="AddressAreaCode" runat="server" placeholder="Enter Your Area Code" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">
                                <div class="form-group form-group-default">
                                    <label>Address</label>
                                    <div class="row">
                                        <div class="col-md-11">
                                            <asp:TextBox ID="CustomerFullAddress" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
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
                                                <asp:TextBox ID="PeriodOfInsuranceFrom" runat="server" placeholder="Enter Your Period of Insurance" CssClass="form-control datepicker-range" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label>To :</label>
                                                <asp:TextBox ID="PeriodOfInsuranceTo" runat="server" placeholder="Enter Your Period of Insurance" CssClass="form-control datepicker-range" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-6">

                                <div class="form-group form-group-default hide">
                                    <div class="controls">
                                        <label>Completion of Work :</label>
                                        <div id="CompletionofWork" runat="server" readonly="true"></div>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <div class="controls">
                                        <label>Description of Work :</label>
                                        <asp:TextBox ID="DescriptionofWork" runat="server" TextMode="MultiLine" CssClass="form-control" Height="120" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-6">

                                <div class="form-group form-group-default">
                                    <div class="controls">
                                        <label>Type Of Installation :</label>
                                        <asp:CheckBoxList ID="TypeOfInstallation" CssClass="checkbox" runat="server" ReadOnly="true">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12 text-right hide">

                                <asp:Button ID="btn_updateDetails" CssClass="btn btn-primary" runat="server" Text="Update Details" OnClick="btn_updateDetails_Click" />

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

                            <div class="form-group form-group-default">
                                <label>Images of the completed job:</label>
                                <div runat="server" id="dispCompltImgs"></div>

                            </div>

                            <div class="form-group form-group-default">
                                <p><b>I <span id="plumname" runat="server"></span>, Licensed registration number <span id="plumregNo" runat="server"></span>, certify that, the above compliance certifcate details are true and correct and will be logged in accordance with the prescribed requirements as defned by the PIRB.</b></p>
                            <p><b>I further certify that;</b></p>
                                <label>Tick A / B:</label>
                                <div class="controls">
                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                                        <asp:ListItem Value="A">A: The above plumbing work was carried out by me or under my supervision, and that it complies in all respects to the plumbing regulations, laws, National Compulsory Standards and Local bylaws.</asp:ListItem>
                                    <asp:ListItem Value="B">B: I have fully inspected and tested the work started but not completed by another Licensed plumber. I further certify that the inspected and tested work and the necessary completion work was carried out by me or under my supervision- complies in all respects to the plumbing regulations, laws, National Compulsory Standards and Local bylaws.</asp:ListItem>
                                 </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="col-md-12 text-center" style="text-align: center;">
                                <div id="pdfDisp" runat="server"></div>
                            </div>



                        </div>
                    </div>


                    <div class="tab-pane" id="plumberdetails">
                        <div class="row form-group-default">

                            <h4>Plumber Details</h4>
                            <div class="col-md-5">
                                <div class="form-group ">
                                    <label>Registration Number :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="PlumberRegNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Mobile Contact :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="PlumberContact" runat="server" CssClass="form-control phone" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group ">
                                    <label>Email :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="PlumberEmail" runat="server" CssClass="form-control" TextMode="Email" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <label>Plumbers Name :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="PlumberFullName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Business Contact :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="PlumberBusContact" runat="server" CssClass="form-control phone" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group text-center">
                                    <div class="controls" id="PlumberImage">
                                        <asp:Image ID="Image1" runat="server" CssClass="img-circle" Style="height: 100px; width: 100px;" />
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>

                    <div class="tab-pane active" id="TabAudit">
                        <div class="row" style="margin-top: 10px;">

                            <h4>Progress Management</h4>
                            <div class="col-md-12">
                                <div class="form-group form-group-default">
                                    <label class="col-lg-2">Plumber</label>
                                    <div class="controls col-lg-10">
                                        <asp:DropDownList ID="plumbercontactedStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="Contacted">Contacted</asp:ListItem>
                                            <asp:ListItem Value="Not Contacted">Not Contacted</asp:ListItem>
                                            <asp:ListItem Value="Left Message">Left Message</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label class="col-lg-2">Client</label>
                                    <div class="controls col-lg-10">
                                        <asp:DropDownList ID="clientContactedStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="Contacted">Contacted</asp:ListItem>
                                            <asp:ListItem Value="Not Contacted">Not Contacted</asp:ListItem>
                                            <asp:ListItem Value="Left Message">Left Message</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label class="col-lg-2">Scheduled Date</label>
                                    <div class="controls col-lg-10">
                                        <div class="col-md-6">
                                            <asp:TextBox ID="TextBox3" runat="server" placeholder="Scheduled Date" CssClass="form-control datepicker-range"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="TextBox8" runat="server" placeholder="Scheduled Time" CssClass="form-control pickatime"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group form-group-default">
                                    <label class="col-lg-2">COC Status</label>
                                    <div class="controls col-lg-10">
                                        <asp:RadioButtonList ID="cocStatusUpdate" runat="server">
                                            <asp:ListItem Value="Non-Logged" style="display: inline-flex;">Non Logged</asp:ListItem>
                                            <asp:ListItem Value="Non Logged - Allocated" style="display: inline-flex;">Non Logged - Allocated</asp:ListItem>
                                            <asp:ListItem Value="Logged" style="display: inline-flex;">Logged</asp:ListItem>
                                            <asp:ListItem Value="Auditing" style="display: inline-flex;">Auditing</asp:ListItem>
                                            <asp:ListItem Value="Completed" style="display: inline-flex;">Completed</asp:ListItem>
                                            <asp:ListItem Value="Refix Required" style="display: inline-flex;">Refix Required</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <asp:Button ID="saveOrUpdate" runat="server" CssClass="btn btn-primary" Text="Save/Update" OnClick="saveOrUpdate_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="row" id="inspectorDetails" runat="server">
                            <h4>Inspector Details</h4>
                            <div class="col-md-5">

                                <div class="form-group form-group-default">
                                    <label>Name :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="InspectorFullName" runat="server" placeholder="Name" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Mobile Contact :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="InspectorContact" runat="server" placeholder="Mobile" CssClass="form-control phone" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group form-group-default">
                                    <label>Email :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="InspectorEmail" runat="server" placeholder="Email" CssClass="form-control" TextMode="Email" ReadOnly="true"></asp:TextBox>
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
                                    <div class="controls" id="InspectorImage" runat="server"></div>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <h3>Re-Assign COC</h3>
                        </div>
                        <div class="row hide">
                            <label class="btn btn-primary" onclick="showreassignaudit()">Re-Assign COC</label>
                        </div>
                        <div class="row" id="dispAuditorDetailsReassign">
                            <%--style="display:none;"--%>
                            <div class="col-md-6">
                                <div class="form-group form-group-default">
                                    <label>Assign Auditor :</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="inspectorList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="myListDropDown_Change"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Name :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="TextBox4" runat="server" placeholder="Name" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">


                                <div class="form-group form-group-default">
                                    <label>Mobile Contact :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="TextBox5" runat="server" placeholder="Mobile" CssClass="form-control phone" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group form-group-default">
                                    <label>Email :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="TextBox6" runat="server" placeholder="Email" CssClass="form-control" TextMode="Email" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <asp:Button ID="Button2" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="Button2_Click" />
                            <div class="col-md-6 hide">
                                <textarea id="summernote"></textarea>
                                <asp:TextBox ID="info" Style="display: none;" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-group-default" style="margin-top: 10px;">
                            <label>Inspector Notes (only you as the Administrator will see this)</label>
                            <p id="P1" runat="server"></p>
                            <asp:TextBox ID="TextBox7" runat="server" TextMode="MultiLine" CssClass="form-control" Height="120"></asp:TextBox>
                            <asp:Button ID="Button3" CssClass="btn btn-default" runat="server" Text="Add Notes" OnClick="addAdminHis_Click" />
                        </div>

                    </div>
                    <div class="tab-pane" id="TabInvoice">
                        <div id="invoiceDisp" runat="server"></div>

                    </div>
                    <div class="tab-pane" id="TabLog">
                        <div id="logdetails" runat="server"></div>
                    </div>
                </div>

            </div>
        </div>
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

    <div class="modal fade fill-in" id="modalPrivateComments" tabindex="-1" role="dialog" aria-hidden="true">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
            <i class="pg-close"></i>
        </button>
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="text-left p-b-5"><span class="semi-bold">Private</span> comments</h5>
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
                                            <h5 id="adnames" runat="server"></h5>
                                        </div>
                                        <div class="card-description">
                                            <div id="adminscomm" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="event-date">
                                        <small class="fs-12 hint-text" id="admindatepost" runat="server"></small>
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
                                            <h5 id="inspecnamepriv" runat="server"></h5>
                                        </div>
                                        <div class="card-description">
                                            <div id="inspectorCommentspriv" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="event-date">
                                        <small class="fs-12 hint-text" id="inspectorDatePostedpriv" runat="server"></small>
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

    <div class="modal fade fill-in" id="modalRefixInspector" tabindex="-1" role="dialog" aria-hidden="true">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
            <i class="pg-close"></i>
        </button>
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="text-left p-b-5"><span class="semi-bold">Refix</span> Notice</h5>
                </div>
                <div class="modal-body bg-white" style="overflow-y: scroll; padding: 10px;">
                </div>
                <div class="modal-footer">
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <script src="assets/Summernote/summernote.js"></script>
    <script src="assets/Summernote/summernote.min.js"></script>

    <script type="text/javascript" src="assets/js/core/libraries/jasny_bootstrap.min.js"></script>
    <script type="text/javascript" src="assets/js/plugins/forms/styling/uniform.min.js"></script>
    <script type="text/javascript" src="assets/js/plugins/forms/inputs/autosize.min.js"></script>
    <script type="text/javascript" src="assets/js/plugins/forms/inputs/formatter.min.js"></script>
    <script type="text/javascript" src="assets/js/plugins/forms/inputs/typeahead/typeahead.bundle.min.js"></script>
    <script type="text/javascript" src="assets/js/plugins/forms/inputs/typeahead/handlebars.min.js"></script>
    <script type="text/javascript" src="assets/js/plugins/forms/inputs/passy.js"></script>
    <script type="text/javascript" src="assets/js/plugins/forms/inputs/maxlength.min.js"></script>
    <script type="text/javascript" src="assets/js/plugins/forms/validation/validate.min.js"></script>
    <script type="text/javascript" src="assets/js/plugins/pickers/anytime.min.js"></script>
    <script type="text/javascript" src="assets/js/plugins/pickers/pickadate/picker.js"></script>
    <script type="text/javascript" src="assets/js/plugins/pickers/pickadate/picker.date.js"></script>
    <script type="text/javascript" src="assets/js/plugins/pickers/pickadate/picker.time.js"></script>
    <script type="text/javascript" src="assets/js/plugins/pickers/pickadate/legacy.js"></script>
    <script type="text/javascript" src="assets/js/plugins/forms/styling/switchery.min.js"></script>
    <%--<script type="text/javascript" src="assets/js/plugins/loaders/pace.min.js"></script>
	<script type="text/javascript" src="assets/js/core/libraries/jquery.min.js"></script>
	<script type="text/javascript" src="assets/js/core/libraries/bootstrap.min.js"></script>
	<script type="text/javascript" src="assets/js/plugins/loaders/blockui.min.js"></script>--%>

    <script type="text/javascript" src="assets/js/plugins/ui/moment/moment.min.js"></script>
    <script type="text/javascript" src="assets/js/plugins/pickers/anytime.min.js"></script>
    <script type="text/javascript" src="assets/js/plugins/pickers/pickadate/picker.js"></script>
    <script type="text/javascript" src="assets/js/plugins/pickers/pickadate/picker.time.js"></script>
    <script type="text/javascript" src="assets/js/pages/picker_date.js"></script>
    <!-- Theme JS files -->

    <script src="assets/js/bootstrap-dropdown-filter.js"></script>
    <script src="assets/js/diacritics.js"></script>
    <script>
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
        
        $('.datepicker-range').datepicker();

        $('.pickatime-ediut').pickatime({
            editable: true
        });

        $('.datepicker-rangeed').datepicker({

        });
        
        $('.pickatime').pickatime();
        if ($("#<%=clientContactedStatus.ClientID%>").val() == "Contacted") {
            $("#<%=clientContactedStatus.ClientID%>").css('background-color', 'lightgreen');
        }
        else {
            $("#<%=clientContactedStatus.ClientID%>").css('background-color', 'red');
        }

        if ($("#<%=plumbercontactedStatus.ClientID%>").val() == "Contacted") {
            $("#<%=plumbercontactedStatus.ClientID%>").css('background-color', 'lightgreen');
        }
        else {
            $("#<%=plumbercontactedStatus.ClientID%>").css('background-color', 'red');
        }

        $("#<%=plumbercontactedStatus.ClientID%>").change(function () {
            if ($(this).val() == "Contacted") {
                $(this).css('background-color', 'lightgreen');
            }
            else {
                $(this).css('background-color', 'red');
            }
        });

        $("#<%=clientContactedStatus.ClientID%>").change(function () {
              if ($(this).val() == "Contacted") {
                  $(this).css('background-color', 'lightgreen');
              }
              else {
                  $(this).css('background-color', 'red');
              }
          });


          var cocid = getUrlVars()["cocid"];

          $('.datepicker-range').datepicker();

          // Only for fillin modals so that the backdrop action is still there
          $('#modalFillIn').on('show.bs.modal', function (e) {
              $('body').addClass('fill-in-modal');
          })
          $('#modalFillIn').on('hidden.bs.modal', function (e) {
              $('body').removeClass('fill-in-modal');
          })

          function deleteImage(id) {
              if (confirm('Are you sure?')) {
                  $.post('https://197.242.82.242/inspectit/api/frmImgDel?imgid=' + id, {}, function (data) {
                      $("#show_img_" + id).addClass("hide");
                  })
              }
          }

          function showreassignaudit() {
              $("#dispAuditorDetailsReassign").show();
          }


          $('#summernote').summernote();

          if (cocid != null) {
              $('#summernote').summernote('editor.insertText', $("#<%=info.ClientID%>").val());
        }

        $('#summernote').on('summernote.keyup', function (we, e) {
            //console.log('Key is released:', e.keyCode);
        });

        $('#summernote').on('summernote.change', function (we, contents, $editable) {
            //console.log('summernote\'s content is changed.', contents);
            $("#<%=info.ClientID%>").val(contents.replace("<p>", "").replace("</p>", "").replace("<br>", ""));
        });

    </script>

</asp:Content>
