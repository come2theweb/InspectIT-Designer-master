<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="NewPlumberRegistrationAdmin.aspx.cs" Inherits="InspectIT.NewPlumberRegistrationAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="https://fonts.googleapis.com/css?family=Roboto:400,300,100,500,700,900" rel="stylesheet" type="text/css" />
    <link href="assets/global_assets/css/icons/icomoon/styles.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/bootstrap_limitless.min.css" rel="stylesheet" />
    <link href="assets/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/components.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/colors.min.css" rel="stylesheet" type="text/css" />
    <!-- /global stylesheets -->

    <!-- Core JS files -->
    <script src="assets/global_assets/js/main/jquery.min.js"></script>
    <script src="assets/global_assets/js/main/bootstrap.bundle.min.js"></script>
    <script src="assets/global_assets/js/plugins/loaders/blockui.min.js"></script>
    <!-- /core JS files -->


    <style>
        
        .card{
            width:100%;
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
                    New Applications
                </div>

                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>

                <div class="panel">
                    <div class="col-md-6">
                        <div class="form-group form-group-default ">
                        <label>Application Status :</label>
                        <div class="controls">
                            <asp:CheckBox ID="IDAttached" runat="server" Text="ID Attached" />
                            <asp:CheckBox ID="QualificationVerified" runat="server" Text="Qualification Verified" /> 
                            <asp:CheckBox ID="ProofExperience" runat="server" Text="Proof of Experience" />
                            <asp:CheckBox ID="Declaration" runat="server" Text="Declaration Signed" />
                            <asp:CheckBox ID="InitialEachPage" runat="server" Text="Initial each page" />
                            <asp:CheckBox ID="PhotoCorrect" runat="server" Text="Photo Correct" />
                            <asp:CheckBox ID="CompanyDetailsCorrect" runat="server" Text="Company Details Correct" />
                            <asp:CheckBox ID="InductionDone" runat="server" Text="Induction Completed" />
                            <asp:CheckBox ID="PaymentRecieved" runat="server" Text="Payment Recieved" />
                        </div>
                    </div>
                    <div class="form-group form-group-default ">
                        <label>Approval Status :</label>
                        <div class="controls">
                                <asp:RadioButton ID="isApproved" runat="server" GroupName="approvedStat" Text="Active" />
                                <asp:RadioButton ID="isRejected" runat="server" GroupName="approvedStat" Text="Rejected" />
                        </div>
                    </div>
                         <div class="form-group form-group-default ">
                            <label>Add Attachments :</label>
                             <div id="attachs" runat="server"></div>
                            <div class="controls">
                                <asp:FileUpload ID="FileUpload4" CssClass="form-control" runat="server" />
                                <asp:Button ID="Button3" runat="server" Text="Add Attachment" CssClass="btn btn-primary" OnClick="Button3_Click" />
                            </div>
                        </div>

                         <div class="form-group form-group-default ">
                            <label>Comment History :</label>
                             <div id="commsHis" runat="server"></div>
                            <div class="controls">
                                <label>Comment :</label>
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                <asp:Button ID="Button2" runat="server" Text="Add Comment" CssClass="btn btn-primary" OnClick="Button2_Click" />
                            </div>
                        </div>

                    </div>
                    <div class="col-md-6">
                         <div class="form-group form-group-default ">
                            <label>Designated award :</label>
                            <div class="controls">
                                <asp:RadioButtonList ID="RadioButtonList2" runat="server">
                                                <asp:ListItem Value="Licensed Plumber">Licensed Plumber</asp:ListItem>
                                                <asp:ListItem Value="Technical Operating Practitioner">Technical Operating Practitioner</asp:ListItem>
                                                <asp:ListItem Value="Technical Assistance Practitioner">Technical Assistance Practitioner</asp:ListItem>
                                                <asp:ListItem Value="Learner">Learner</asp:ListItem>
                                            </asp:RadioButtonList>
                            </div>
                        </div>

                        <h4>Qualification Details</h4>

                        <table class="table table-striped demo-table-search" id="stripedTable">
                            <thead>
                            <tr>
                                <th>Trade Test/Certification Number</th> 
                                <th>Year</th> 
                                <th>Route</th> 
                                <th>Training Provider</th> 
                                <th></th> 
                            </tr>
                            </thead>
                            <tbody  id="userQualifications" runat="server">
            
                            </tbody>
                        </table>

                        <div class="form-group form-group-default ">
                            <label>Trade Test/Certification Number :</label>
                            <div class="controls">
                                <asp:TextBox ID="CertificationNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                         <div class="form-group form-group-default ">
                            <label>Year Certificate was obtained :</label>
                            <div class="controls">
                                <asp:TextBox ID="certificateYear" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                         <div class="form-group form-group-default ">
                            <label>Route you achieved your qualification through :</label>
                            <div class="controls">
                                <asp:DropDownList ID="routeQualification" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                         <div class="form-group form-group-default ">
                            <label>Training Provider where your training was done :</label>
                            <div class="controls">
                                <asp:TextBox ID="TrainingProvider" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        
                                <asp:Button ID="btnQualificationAdd" runat="server" Text="Add Qualification" CssClass="btn btn-primary" OnClick="btnQualificationAdd_Click" />

                    </div>
                     
                                <asp:Button ID="Button4" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="saveAppDetails_Click" />
                                <asp:Button ID="Button5" runat="server" Text="New Registration Email" CssClass="btn btn-primary" OnClick="Button5_Click" />

                                        <div class="alert alert-danger" runat="server" id="ERRMSGsub"></div>

                     <div class="card-group-control card-group-control-left" id="accordion-control">
                            <div class="card">
                                <div class="card-header">
                                    <h6 class="card-title">
                                        <a data-toggle="collapse" class="text-default" href="#accordion-control-group1">Procedure of Registration</a>
                                    </h6>
                                </div>

                                <div id="accordion-control-group1" class="collapse show" data-parent="#accordion-control">
                                    <div class="card-body">

                                        <ul>
                                            <li>All qualifications of any individual applying for registration will be vetted and verified with the various authenticating bodies.</li>
                                            <li>The applicant will be notified via email/sms/telephone of any discrepancies that are found and the applicants application will be put on hold. The process of the application/registration will only continue once it has been addressed.
                                            </li>
                                            <li>Once the application has been approved a pro-forma invoice for the yearly registration fee will be sent (current yearly registration fees can be found at www.pirb.co.za). The pro-forma invoice will be sent to the contact details that appear on the application/registration form.
                                            </li>
                                            <li>Only once payment has been received, the PIRB will continue with the application and the application will be registered on the PIRB database. 
                                            </li>
                                            <li>It the applicant requested a card, the PIRB registration card registration will be sent via registered mail to the postal address that appears on the application form, or alternatively the PIRB Registration Card can also be collected from the PIRB registration office or collection points.
                                            </li>
                                            <li>If the registration card is sent via registered mail the relevant tracking number will be sms’d to the applicant and it will be the applicants responsibility to keep track of the registered mail. Any registered mail returned to PIRB office due to non-collection by the applicant will only be resent if an additional administration fee is paid. Alternatively it can be collected at the PIRB registration office.
                                            </li>
                                            <li>If the application is found to be in order and payment of the invoice has been within a reasonable time, the PIRB registration process should not take longer than 20 working days from receipt of application.
                                            </li>
                                            <li>Further information can be obtained from www.pirb.co.za or you may email registration@pirb.co.za
                                            </li>
                                        </ul>

                                        <div class="form-group form-group-default">
                                            <label>I declare that I have fully read and understood the Procedure of Registration</label>
                                            <div class="controls">
                                                <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" />
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>

                            <div class="card">
                                <div class="card-header">
                                    <h6 class="card-title">
                                        <a class="collapsed text-default" data-toggle="collapse" href="#accordion-control-group2">Registration Card</a>
                                    </h6>
                                </div>

                                <div id="accordion-control-group2" class="collapse" data-parent="#accordion-control">
                                    <div class="card-body">
                                        <div class="row">

                                            <div class="col-md-12">
                                                Due to the high number of card returns and cost incurred the registration fees do not include a registration card.  Registration cards are available but must be requested separately.   
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group form-group-default">
                                                    <label>Registration Card Required :</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" Enabled="false">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group form-group-default">
                                                    <label>Method of Delivery :</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server" Enabled="false">
                                                            <asp:ListItem Value="Collection">Collection</asp:ListItem>
                                                            <asp:ListItem Value="Registration Mail">Registration Mail</asp:ListItem>
                                                            <asp:ListItem Value="Courier">Courier</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="card">
                                <div class="card-header">
                                    <h6 class="card-title">
                                        <a class="collapsed text-default" data-toggle="collapse" href="#accordion-control-group3">Register Personal Details</a>
                                    </h6>
                                </div>

                                <div id="accordion-control-group3" class="collapse" data-parent="#accordion-control">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12" style="color:#F5F5F5;">
                                                <%--veronike hide text, only here to keep the page full width--%>
										Due to the high number of card returns and cost incurred the registration fees do not include a registration card.  Registration cards are available but must be requested separately.   
                                            </div>
                                            <div class="col-md-12 col-lg-12">
                                                <div class="col-md-6">
                                                    <div class="form-group form-group-default ">
                                                        <label>Title :</label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="title" CssClass="form-control" runat="server" >
                                                                <asp:ListItem Value="Mr">Mr</asp:ListItem>
                                                                <asp:ListItem Value="Mrs">Mrs</asp:ListItem>
                                                                <asp:ListItem Value="Miss">Miss</asp:ListItem>
                                                                <asp:ListItem Value="Other">Other</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group form-group-default ">
                                                        <label>Surname :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="Surname" CssClass="form-control" runat="server" ></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group form-group-default">
                                                        <label>Alternate ID :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="AlternateID" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group form-group-default ">
                                                        <label>Gender :</label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="Gender" CssClass="form-control" runat="server" >
                                                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group form-group-default required ">
                                                        <label>Racial Status</label>
                                                        <asp:DropDownList ID="RacialStatus" runat="server" CssClass="form-control" >
                                                            <asp:ListItem Value=""></asp:ListItem>
                                                            <asp:ListItem Value="Black: African">Black: African</asp:ListItem>
                                                            <asp:ListItem Value="Black: Coloured">Black: Coloured</asp:ListItem>
                                                            <asp:ListItem Value="Black: Indian">Black: Indian</asp:ListItem>
                                                            <asp:ListItem Value="White">White</asp:ListItem>
                                                            <asp:ListItem Value="Unknown">Unknown</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group form-group-default  required">
                                                        <label>South Africa National</label>
                                                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" >
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group form-group-default  required">
                                                        <label>Citizen Resident Status</label>
                                                        <asp:DropDownList ID="ResidentStatus" runat="server" CssClass="form-control" >
                                                            <asp:ListItem Value=""></asp:ListItem>
                                                            <asp:ListItem Value="Dual (SA plus other)">Dual (SA plus other)</asp:ListItem>
                                                            <asp:ListItem Value="Other">Other</asp:ListItem>
                                                            <asp:ListItem Value="Permanent Resident">Permanent Resident</asp:ListItem>
                                                            <asp:ListItem Value="South Africa">South Africa</asp:ListItem>
                                                            <asp:ListItem Value="Unknown">Unknown</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group form-group-default required">
                                                        <label>First Name :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="Name" CssClass="form-control" runat="server" ></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group form-group-default">
                                                        <label>SA ID Number :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="IDNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group form-group-default">
                                                        <label>Attached ID :</label>
                                                        <div class="controls">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                            <asp:Image ID="Image1" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group form-group-default">
                                                        <label>Date of Birth :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="dob" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                    <div class="form-group form-group-default  required">
                                                        <label>Home Language</label>
                                                        <asp:DropDownList ID="HomeLanguage" runat="server" CssClass="form-control" >
                                                            <asp:ListItem Value=""></asp:ListItem>
                                                            <asp:ListItem Value="Afrikaans">Afrikaans</asp:ListItem>
                                                            <asp:ListItem Value="English">English</asp:ListItem>
                                                            <asp:ListItem Value="isiNdebele">isiNdebele</asp:ListItem>
                                                            <asp:ListItem Value="isiXhosa">isiXhosa</asp:ListItem>
                                                            <asp:ListItem Value="isiZulu">isiZulu</asp:ListItem>
                                                            <asp:ListItem Value="Other">Other</asp:ListItem>
                                                            <asp:ListItem Value="sePedi">sePedi</asp:ListItem>
                                                            <asp:ListItem Value="seSotho">seSotho</asp:ListItem>
                                                            <asp:ListItem Value="seTswana">seTswana</asp:ListItem>
                                                            <asp:ListItem Value="siSwati">siSwati</asp:ListItem>
                                                            <asp:ListItem Value="South African Sign Language">South African Sign Language</asp:ListItem>
                                                            <asp:ListItem Value="tshiVenda">tshiVenda</asp:ListItem>
                                                            <asp:ListItem Value="Unknown">Unknown</asp:ListItem>
                                                            <asp:ListItem Value="xiTsonga">xiTsonga</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Nationality</label>
                                                        <asp:DropDownList ID="Nationality" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value=""></asp:ListItem>
                                                            <asp:ListItem Value="Angola">Angola</asp:ListItem>
                                                            <asp:ListItem Value="Asian countries">Asian countries</asp:ListItem>
                                                            <asp:ListItem Value="Australia Oceania countries">Australia Oceania countries</asp:ListItem>
                                                            <asp:ListItem Value="Botswana">Botswana</asp:ListItem>
                                                            <asp:ListItem Value="European countries">European countries</asp:ListItem>
                                                            <asp:ListItem Value="Lesotho">Lesotho</asp:ListItem>
                                                            <asp:ListItem Value="Malawi">Malawi</asp:ListItem>
                                                            <asp:ListItem Value="Mauritius">Mauritius</asp:ListItem>
                                                            <asp:ListItem Value="Mozambique">Mozambique</asp:ListItem>
                                                            <asp:ListItem Value="N/A: Institution">N/A: Institution</asp:ListItem>
                                                            <asp:ListItem Value="Namibia">Namibia</asp:ListItem>
                                                            <asp:ListItem Value="North American countries">North American countries</asp:ListItem>
                                                            <asp:ListItem Value="Other & rest of Oceania">Other & rest of Oceania</asp:ListItem>
                                                            <asp:ListItem Value="Rest of Africa">Rest of Africa</asp:ListItem>
                                                            <asp:ListItem Value="SADC except SA">SADC except SA</asp:ListItem>
                                                            <asp:ListItem Value="Seychelles">Seychelles</asp:ListItem>
                                                            <asp:ListItem Value="South / Central American countries">South / Central American countries</asp:ListItem>
                                                            <asp:ListItem Value="South Africa">South Africa</asp:ListItem>
                                                            <asp:ListItem Value="Swaziland">Swaziland</asp:ListItem>
                                                            <asp:ListItem Value="Tanzania">Tanzania</asp:ListItem>
                                                            <asp:ListItem Value="Unspecified">Unspecified</asp:ListItem>
                                                            <asp:ListItem Value="Zaire">Zaire</asp:ListItem>
                                                            <asp:ListItem Value="Zambia">Zambia</asp:ListItem>
                                                            <asp:ListItem Value="Zimbabwe">Zimbabwe</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group form-group-default required ">
                                                        <label>Disability</label>
                                                        <asp:DropDownList ID="Disability" runat="server" CssClass="form-control" >
                                                            <asp:ListItem Value=""></asp:ListItem>
                                                            <asp:ListItem Value="Communication(talk/listen)">Communication(talk/listen)</asp:ListItem>
                                                            <asp:ListItem Value="Disabled but unspecified">Disabled but unspecified</asp:ListItem>
                                                            <asp:ListItem Value="Emotional (behav/psych)">Emotional (behav/psych)</asp:ListItem>
                                                            <asp:ListItem Value="Hearing (even with h. aid)">Hearing (even with h. aid)</asp:ListItem>
                                                            <asp:ListItem Value="Intellectual (learn etc)">Intellectual (learn etc)</asp:ListItem>
                                                            <asp:ListItem Value="Multiple">Multiple</asp:ListItem>
                                                            <asp:ListItem Value="None">None</asp:ListItem>
                                                            <asp:ListItem Value="None now - was Communic">None now - was Communic</asp:ListItem>
                                                            <asp:ListItem Value="None now - was Disabled but unspecified">None now - was Disabled but unspecified</asp:ListItem>
                                                            <asp:ListItem Value="None now - was Emotional">None now - was Emotional</asp:ListItem>
                                                            <asp:ListItem Value="None now - was Hearing">None now - was Hearing</asp:ListItem>
                                                            <asp:ListItem Value="None now - was Intellect">None now - was Intellect</asp:ListItem>
                                                            <asp:ListItem Value="None now - was Multiple">None now - was Multiple</asp:ListItem>
                                                            <asp:ListItem Value="None now - was Physical">None now - was Physical</asp:ListItem>
                                                            <asp:ListItem Value="None now - was Sight">None now - was Sight</asp:ListItem>
                                                            <asp:ListItem Value="Physical (move/stand etc)">Physical (move/stand etc)</asp:ListItem>
                                                            <asp:ListItem Value="Sight (even with glasses)">Sight (even with glasses)</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-md-12 col-lg-12">
                                                <div class="col-md-6">
                                                    <h4>Postal Address</h4>
                                                    <div class="form-group form-group-default required">
                                                        <label>Address :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="postalAddress" CssClass="form-control" runat="server" ></asp:TextBox>
                                                        </div>
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

                                                    <div class="form-group form-group-default required">
                                                        <label>Postal Code :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="PostalCode" CssClass="form-control" runat="server" ></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <h4>Physical Address</h4>
                                                    <div class="form-group form-group-default required">
                                                        <label>Address :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="PhysicalAddress" CssClass="form-control" runat="server" ></asp:TextBox>
                                                        </div>
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
                                                </div>

                                            </div>
                                            <div class="col-md-12 col-lg-12">
                                                <div class="col-md-6">
                                                    <div class="form-group form-group-default required">
                                                        <label>Home Phone :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="homePhone" CssClass="form-control" runat="server" ></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group form-group-default required">
                                                        <label>Mobile :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="Mobile" CssClass="form-control" runat="server" ></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group form-group-default required">
                                                        <label>Email Address :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="emailAddress" CssClass="form-control" runat="server" ></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group form-group-default required">
                                                        <label>Work Phone :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="WorkPhone" CssClass="form-control" runat="server" ></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group form-group-default required">
                                                        <label>Secondary Email Address :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="secondEmailAddress" CssClass="form-control" runat="server" ></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="card">
                                <div class="card-header">
                                    <h6 class="card-title">
                                        <a class="collapsed text-default" data-toggle="collapse" href="#accordion-control-group4">Employment Details</a>
                                    </h6>
                                </div>

                                <div id="accordion-control-group4" class="collapse" data-parent="#accordion-control">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12" style="color:#F5F5F5;">
                                                <%--veronike hide text, only here to keep the page full width--%>
										Due to the high number of card retur to the high number of card returns and cost  to the high number of card returns and cost  to the high number of card returns and cost  to the high number of card returns and cost ns and cost incurred the registration fees do not include a registration card.  Registration cards are available but must be requested separately.   
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group form-group-default required ">
                                                        <label>Employment Status</label>
                                                        <asp:DropDownList ID="EmploymentStatus" runat="server" CssClass="form-control" >
                                                            <asp:ListItem Value=""></asp:ListItem>
                                                            <asp:ListItem Value="Employed">Employed</asp:ListItem>
                                                            <asp:ListItem Value="Unemployed">Unemployed</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Select Company</label>
                                                        <asp:DropDownList ID="CompanyID" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group form-group-default ">
                                                        <label>Primary Contact Person</label>
                                                        <asp:TextBox ID="primaryContact" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Company VAT nr</label>
                                                        <asp:TextBox ID="CompanyVAt" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <h4>Employer Employment Postal Address</h4>
                                                    <div class="form-group form-group-default ">
                                                        <label>Address</label>
                                                        <asp:TextBox ID="empPostalAddress" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Province</label>
                                                        <asp:TextBox ID="empPostalProvince" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>City</label>
                                                        <asp:TextBox ID="empPostalCity" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Suburb</label>
                                                        <asp:TextBox ID="empPostalSuburb" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Code</label>
                                                        <asp:TextBox ID="empPostalCode" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <h4>Employer Physical Address</h4>
                                                    <div class="form-group form-group-default ">
                                                        <label>Address</label>
                                                        <asp:TextBox ID="empAddress" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Province</label>
                                                        <asp:TextBox ID="empProvince" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>City</label>
                                                        <asp:TextBox ID="empCity" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Suburb</label>
                                                        <asp:TextBox ID="empSuburb" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                    <h4>Contact Details</h4>
                                                <div class="col-md-6">
                                                    <div class="form-group form-group-default ">
                                                        <label>Work Phone</label>
                                                        <asp:TextBox ID="empWorkPhone" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Email Address</label>
                                                        <asp:TextBox ID="empEmailaddress" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group form-group-default ">
                                                        <label>Mobile</label>
                                                        <asp:TextBox ID="empMobile" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="card">
                                <div class="card-header">
                                    <h6 class="card-title">
                                        <a class="collapsed text-default" data-toggle="collapse" href="#accordion-control-group5">Photo Identification</a>
                                    </h6>
                                </div>

                                <div id="accordion-control-group5" class="collapse" data-parent="#accordion-control">
                                    <div class="card-body">
                                        <div class="col-md-12" style="color:#F5F5F5;">
                                            <%--veronike hide text, only here to keep the page full width--%>
										Due to the high number of card returns aDue to the high number of card returns and cost iDue to the high number of card returns and cost iDue to the high number of card returns and cost ind cost incurred the registration fees do not include a registration card.  Registration cards are available but must be requested separately.   
                                        </div>
                                        <ul>
                                            <li>Photos must be no more than 6 months old </li>
                                            <li>Photos must be high quality </li>
                                            <li>Photos must be in colour </li>
                                            <li>Photos must have clear preferably white background </li>
                                            <li>Photos must be in sharp focus and clear </li>
                                            <li>Photo must be only of your head and shoulders </li>
                                            <li>You must be looking directly at the camera </li>
                                            <li>No sunglasses or hats </li>
                                            <li>Attachment should be no larger than 2MB</li>
                                            <li>File name is your NAME and SURNAME.</li>
                                        </ul>
                                        <div class="form-group form-group-default ">
                                            <label>Attached Photo</label>
                                            <asp:FileUpload ID="FileUpload2" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="card">
                                <div class="card-header">
                                    <h6 class="card-title">
                                        <a class="collapsed text-default" data-toggle="collapse" href="#accordion-control-group6">Designation</a>
                                    </h6>
                                </div>

                                <div id="accordion-control-group6" class="collapse" data-parent="#accordion-control">
                                    <div class="card-body">
                                        <div class="col-md-12" style="color:#F5F5F5;">
                                                <%--veronike hide text, only here to keep the page full width--%>
										Due to the high number of card returns and cost incurred the registration fees do not include a registration card.  Registration cards are available but must be requested separately.   
                                            </div>
                                        <div class="col-md-12">
                                            Please select the relevant designation being applied for. To view the designation requirements <a href="http://pirb.co.za/wp-content/uploads/2015/07/A0-Designation-Poster_2017-10-09_Digital-1.pdf" target="_blank">Click here</a>
                                        </div>

                                        <div class="form-group form-group-default ">
                                            <label>Select One</label>
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                                <asp:ListItem Value="Licensed Plumber">Licensed Plumber</asp:ListItem>
                                                <asp:ListItem Value="Technical Operating Practitioner">Technical Operating Practitioner</asp:ListItem>
                                                <asp:ListItem Value="Technical Assistance Practitioner">Technical Assistance Practitioner</asp:ListItem>
                                                <asp:ListItem Value="Learner">Learner</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="card">
                                <div class="card-header">
                                    <h6 class="card-title">
                                        <a class="collapsed text-default" data-toggle="collapse" href="#accordion-control-group7">Attach Certificates</a>
                                    </h6>
                                </div>

                                <div id="accordion-control-group7" class="collapse" data-parent="#accordion-control">
                                    <div class="card-body">
                                        <div class="col-md-12">
                                            NOTE: Applications to Director and Master Plumber and or specialisations can only be done once your registration has been verified and approved.  See Application for further designations/specializations
                                        </div>

                                        <div class="form-group form-group-default ">
                                            <label>Please Attach ALL your relevant trade certificates.</label>
                                            <asp:FileUpload ID="FileUpload3" runat="server" />
                                            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Upload" OnClick="Button1_Click" formnovalidate />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h6 class="card-title">
                                        <a class="collapsed text-default" data-toggle="collapse" href="#accordion-control-group8">PIRB’s Code of Conduct  </a>
                                    </h6>
                                </div>

                                <div id="accordion-control-group8" class="collapse" data-parent="#accordion-control">
                                    <div class="card-body">
                                        <div class="col-md-12">
                                            <ul>
                                                <li>PIRB registered plumbers agree to conduct themselves and their business in a professional manner which shall be seen by those they serve as being honourable, transparent and fair. </li>
                                                <li>PIRB registered plumbers agree to proactively perform, work and act to promote plumbing practices that protect the health and safety of the community and the integrity of the water supply and wastewater systems. 
                                                </li>
                                                <li>PIRB registered plumbers agree to promote, protect and encourage the upliftment and advancement of the skills development and training in terms of the National Skills ACT., for themselves and individuals in the plumbing sector or wishing to join the plumbing industry. 
                                                </li>
                                                <li>PIRB registered plumbers agree to monitor and enforce compliance with technical standards of plumbing work that comply with all requirements of the relevant SANS codes of practice and regulations set out in the compulsory National Standards of the Water Service Act 1997 Amended (8th June2001) as well as relevant local municipal bylaws.
                                                </li>
                                                <li>PIRB registered plumbers agree to actively promote and support a consistent and effective regulatory plumbing environment throughout South Africa. 
                                                </li>
                                                <li>PIRB registered plumbers agree to regularly consult and liaise with the plumbing industry in an open forum free of any political or commercial agenda for the discussion of matters affecting the plumbing industry and the role of plumbing for the well-being of the community and the integrity of the water supply and wastewater systems. 
                                                </li>
                                                <li>PIRB registered plumbers agree to promote, monitor and maintain expertise and competencies among our registered and non-registered plumbing professionals. 
                                                </li>
                                                <li>PIRB registered Licensed plumbers agree to issue a PIRB Plumbing Certificate of Compliance (COC) on all plumbing works undertaken as a PIRB Licensed Plumber and shall further issue the COC in terms of the prescribe requirements for issuing of a PIRB Plumbing COC.
                                                </li>
                                            </ul>
                                        </div>


                                        <div class="form-group form-group-default">
                                            <label>
                                                I declare that I have fully read and understood the PIRB’s Code of Conduct
 :</label>
                                            <div class="controls">
                                                <asp:CheckBox ID="CheckBox2" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h6 class="card-title">
                                        <a class="collapsed text-default" data-toggle="collapse" href="#accordion-control-group9">Acknowledgement</a>
                                    </h6>
                                </div>

                                <div id="accordion-control-group9" class="collapse" data-parent="#accordion-control">
                                    <div class="card-body">
                                        <div class="row">
                                        <div class="col-md-12">

                                            <ul>
                                                <li>I acknowledge that part of the PIRB registration process, all qualifications of any individual applying for registration is vetted and  verified with various authenticating bodies. If it is found that the relevant authenticating bodies have no knowledge or records of the relevant individuals qualification it will be forwarded to the PIRB Steering Committee to be reviewed. Only once the PIRB steering committee have reviewed your trade test result and gave authorization will be the PIRB register the relevant individual. Further to this if the verification bodies at any stage communicate to the PIRB that the relevant individuals qualification is no longer valid for reason beyond the PIRB’s control, the PIRB reserve the right to remove the PIRB status of the registered individual with immediate effect and the PIRB will not be held liable for any possible damages that may arise from this. It will further not be the responsibility of the PIRB to address or follow this up with the authenticating body.
                                                </li>
                                                <li>I acknowledge that plumber registration is an annual registration and that a registration fee is charged for plumber registration and this fee which is subject to change is to be paid into the PIRB bank account before I am to be registered.
                                                </li>
                                                <li>I acknowledge that I must reapply for re-registration, one calendar month before the renewal date that appears on my registration card and that the PIRB reserves the right to level a penalty fee for late registration.
                                                </li>
                                                <li>I acknowledge that the PIRB has the authority to suspend or terminate my registration if I act against the best interest of the PIRB, its aims and objectives and the PIRB’s Plumbers Code of Conduct. I further acknowledge that in the event of a suspension and or termination the PIRB’s reserves the right to notify this fact publically and the reason for the suspension and/or termination.
                                                </li>
                                                <li>I acknowledge that if I register for the designation of a Licensed, I shall agree to issue a PIRB Plumbing Certificate of Compliance (COC) on all plumbing works undertaken as a PIRB Licensed Plumber.
                                                </li>
                                                <li>I acknowledged that the issuing of the PIRB Plumbing COC shall be done in the strict defined terms of the prescribe requirements for issuing of a PIRB Plumbing COC and acknowledge that random audits will take place on the COC’s and that I will give full cooperation in this regard.
                                                </li>
                                                <li>I acknowledge that as a Licensed Plumber if I fail to issue a PIRB Plumbing Certificate of Compliance (COC) for work undertaken, carried out and or work adequately supervised; and a complaint is raised against the said plumbing works and or my actions; and the said plumbing works and or my actions are found to be contra to the PIRB’s Code of Conduct, I may and can be held accountable for all cost incurred in resolving the said complaint.
                                                </li>
                                            </ul>
                                            <div class="form-group form-group-default">
                                                <label>
                                                    I declare that I have fully read and understood the Acknowledgement
 :</label>
                                                <div class="controls">
                                                    <asp:CheckBox ID="CheckBox3" runat="server" />
                                                </div>
                                            </div>
                                        </div>
</div>

                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header">
                                    <h6 class="card-title">
                                        <a class="collapsed text-default" data-toggle="collapse" href="#accordion-control-group10">Declaration</a>
                                    </h6>
                                </div>

                                <div id="accordion-control-group10" class="collapse" data-parent="#accordion-control">
                                    <div class="card-body">
                                        <div class="col-md-12">
                                            I
                                            <asp:TextBox ID="declareName" CssClass="form-control" runat="server"></asp:TextBox>
                                            identification number
                                            <asp:TextBox ID="declareIDnum" CssClass="form-control" runat="server"></asp:TextBox>
                                            Declare that the information contained in this application, or attached by me to this application, is complete, accurate and true to the best of my knowledge. I further declare that by forwarding this completed application form to the PIRB I am acknowledging that I have read and fully understood what is required of me as a PIRB registered and professional plumber and that I adhere to all aims and objectives of the PIRB and the PIRB’s Plumber Code of Conduct. I give consent for enquiries for verification purposes to be made into any information I have given on this application.

                                             
                                          <div class="form-group form-group-default">
                                              <label>
                                                  I declare that I have fully read and understood the declaration
 :</label>
                                              <div class="controls">
                                                  <asp:CheckBox ID="CheckBox4" runat="server" />
                                              </div>
                                          </div>
                                        </div>
 <asp:Button ID="submitNewApplication" runat="server" Text="Submit" OnClick="submitNewApplication_Click" CssClass="btn btn-primary"/>


                                    </div>
                                </div>
                            </div>
                        </div>

                </div>
            </div>
        </div>
    </div>



    <script>
     
        var rblSelectedValue = $("#<%= RadioButtonList2.ClientID %> input:checked").val();
        console.log("vaaal :: "+rblSelectedValue);

        if (<%=disabledEnabledRadios.ToString()%> == true) {
            $('#<%=isApproved.ClientID%>').attr("disabled", "disabled");
            $('#<%=isRejected.ClientID%>').attr("disabled", "disabled");
        }
        else {
            $('#<%=isApproved.ClientID%>').removeAttr("disabled");
            $('#<%=isRejected.ClientID%>').removeAttr("disabled");
        }

        $("input").change(function () {
            if ($('#<%=IDAttached.ClientID%>').is(':checked') && $('#<%=QualificationVerified.ClientID%>').is(':checked') && $('#<%=ProofExperience.ClientID%>').is(':checked') && $('#<%=Declaration.ClientID%>').is(':checked') && $('#<%=InitialEachPage.ClientID%>').is(':checked') && $('#<%=PhotoCorrect.ClientID%>').is(':checked') && $('#<%=CompanyDetailsCorrect.ClientID%>').is(':checked') && $('#<%=InductionDone.ClientID%>').is(':checked') && $('#<%=PaymentRecieved.ClientID%>').is(':checked')) {
                if ($("#<%= RadioButtonList2.ClientID %> input:checked").val()) {
                    $('#<%=isApproved.ClientID%>').removeAttr("disabled");
                    $('#<%=isRejected.ClientID%>').removeAttr("disabled");
                }
                else {
                    $('#<%=isApproved.ClientID%>').attr("disabled", "disabled");
                    $('#<%=isRejected.ClientID%>').attr("disabled", "disabled");
                }
            }
            else {
                $('#<%=isApproved.ClientID%>').attr("disabled", "disabled");
                $('#<%=isRejected.ClientID%>').attr("disabled", "disabled");
            }
        });
    </script>

</asp:Content>
