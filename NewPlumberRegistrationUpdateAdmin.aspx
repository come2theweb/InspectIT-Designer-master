<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="NewPlumberRegistrationUpdateAdmin.aspx.cs" Inherits="InspectIT.NewPlumberRegistrationUpdateAdmin" %>

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
                    New Applications Update
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
                            <asp:CheckBox ID="QualificationVerified" runat="server" Text="Qualification Verified" />
                            <asp:CheckBox ID="ProofExperience" runat="server" Text="Proof of Experience" />
                            <asp:CheckBox ID="Declaration" runat="server" Text="Declaration Signed" />
                            <asp:CheckBox ID="PaymentRecieved" runat="server" Text="Payment Recieved" />
                            <asp:CheckBox ID="AssessmentComplete" runat="server" Text="Assessment Completed" />
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
                            <label>Update Designated award :</label>
                            <div class="controls">
                                  <div class="form-group form-group-default ">
                                            <asp:CheckBox ID="DirectorPlumber" CssClass="checkbox check-info" runat="server" Text="Director Plumber" />
                                            <asp:CheckBox ID="MasterPlumber" CssClass="checkbox check-warning" runat="server" Text="Master Plumber" />
                                            <asp:CheckBox ID="MasterPlumberTrainingAssesor" CssClass="checkbox check-warning" runat="server" Text="- Plumbing Training Assessor" />
                                            <asp:CheckBox ID="MasterPlumberEstimator" CssClass="checkbox check-warning" runat="server" Text="- Plumbing estimator" />
                                            <asp:CheckBox ID="MasterPlumberArbitrator" CssClass="checkbox check-warning" runat="server" Text="- Plumbing Arbitrator" />
                                            <asp:CheckBox ID="LicensedPlumber" CssClass="checkbox check-danger" runat="server" Text="Licensed Plumber" />
                                            <asp:CheckBox ID="LicensedPlumberSolar" CssClass="checkbox check-danger" runat="server" Text="- Solar" />
                                            <asp:CheckBox ID="LicensedPlumberHeatPump" CssClass="checkbox check-danger" runat="server" Text="- Heat Pump" />
                                            <asp:CheckBox ID="LicensedPlumberGas" CssClass="checkbox check-danger" runat="server" Text="- Gas" />
                                            <asp:CheckBox ID="TechnicalOperatorPractitioner" CssClass="checkbox check-primary" runat="server" Text="Technical Operator Practitioner" />
                                            <asp:CheckBox ID="TechnicalOperatorPractitionerDrainage" CssClass="checkbox check-primary" runat="server" Text="- Drainage" />
                                            <asp:CheckBox ID="TechnicalOperatorPractitionerColdWater" CssClass="checkbox check-primary" runat="server" Text="- Cold Water" />
                                            <asp:CheckBox ID="TechnicalOperatorPractitionerHotWater" CssClass="checkbox check-primary" runat="server" Text="- Hot Water" />
                                            <asp:CheckBox ID="TechnicalOperatorPractitionerWaterEnergyEfficiency" CssClass="checkbox check-primary" runat="server" Text="- Water Energy Efficiency" />
                                            <asp:CheckBox ID="TechnicalAssistantPractitioner" CssClass="checkbox check-info" runat="server" Text="Technical Assistant Practitioner" />
                                            <asp:CheckBox ID="LearnerPlumber" CssClass="checkbox check-success hide" runat="server" Text="Learner" />
                                            <asp:CheckBox ID="QualifiedPlumber" CssClass="checkbox check-info hide" runat="server" Text="Qualified Plumber" />
                                        </div>
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
                                        <a data-toggle="collapse" class="text-default" href="#accordion-control-group1">Procedure for update of Designations</a>
                                    </h6>
                                </div>

                                <div id="accordion-control-group1" class="collapse show" data-parent="#accordion-control">
                                    <div class="card-body">

                                        <ul>
                                            <li>All qualifications of any individual applying for update of designations will be vetted and verified with the various authenticating bodies.
                                            </li>
                                            <li>The applicant will be notified via email/sms/telephone of any discrepancies that are found and the applicants application will be put on hold. The process of the application/registration will only continue once it has been addressed.
                                            </li>
                                            <li>Once the application has been approved a pro-forma invoice for the designation fee will be sent (current designation fees can be found at www.pirb.co.za). The pro-forma invoice will be sent to the contact details that appear on the application/registration form.
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
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
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
                                                        <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
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
                                                        <asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server">
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
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group form-group-default required">
                                                            <label>Registration Number :</label>
                                                            <div class="controls">
                                                                    <asp:TextBox ID="regno" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    <asp:TextBox ID="TxtUdi" CssClass="form-control hide" runat="server"></asp:TextBox>
                                                                    <asp:TextBox ID="pirbid" CssClass="form-control hide" runat="server"></asp:TextBox>

                                                               
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group form-group-default required">
                                                            <label>Title :</label>
                                                            <div class="controls">
                                                                <asp:DropDownList ID="title" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Value="Mr">Mr</asp:ListItem>
                                                                    <asp:ListItem Value="Mrs">Mrs</asp:ListItem>
                                                                    <asp:ListItem Value="Miss">Miss</asp:ListItem>
                                                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group form-group-default required">
                                                            <label>First Name :</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="Name" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group form-group-default required">
                                                            <label>Surname :</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="Surname" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
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

                                        <div class="form-group form-group-default " style="margin-left: 50px;">
                                            <label>Select One</label>
                                            <asp:CheckBox ID="CheckBox5" CssClass="checkbox check-info" runat="server" Text="Director Plumber" />
                                            <asp:CheckBox ID="CheckBox6" CssClass="checkbox check-warning" runat="server" Text="Master Plumber" />
                                            <asp:CheckBox ID="CheckBox7" CssClass="checkbox check-warning" runat="server" Text="- Plumbing Training Assessor" />
                                            <asp:CheckBox ID="CheckBox8" CssClass="checkbox check-warning" runat="server" Text="- Plumbing estimator" />
                                            <asp:CheckBox ID="CheckBox9" CssClass="checkbox check-warning" runat="server" Text="- Plumbing Arbitrator" />
                                            <asp:CheckBox ID="CheckBox10" CssClass="checkbox check-danger" runat="server" Text="Licensed Plumber" />
                                            <asp:CheckBox ID="CheckBox11" CssClass="checkbox check-danger" runat="server" Text="- Solar" />
                                            <asp:CheckBox ID="CheckBox12" CssClass="checkbox check-danger" runat="server" Text="- Heat Pump" />
                                            <asp:CheckBox ID="CheckBox13" CssClass="checkbox check-danger" runat="server" Text="- Gas" />
                                            <asp:CheckBox ID="CheckBox14" CssClass="checkbox check-primary" runat="server" Text="Technical Operator Practitioner" />
                                            <asp:CheckBox ID="CheckBox15" CssClass="checkbox check-primary" runat="server" Text="- Drainage" />
                                            <asp:CheckBox ID="CheckBox16" CssClass="checkbox check-primary" runat="server" Text="- Cold Water" />
                                            <asp:CheckBox ID="CheckBox17" CssClass="checkbox check-primary" runat="server" Text="- Hot Water" />
                                            <asp:CheckBox ID="CheckBox18" CssClass="checkbox check-primary" runat="server" Text="- Water Energy Efficiency" />
                                            <asp:CheckBox ID="CheckBox19" CssClass="checkbox check-info" runat="server" Text="Technical Assistant Practitioner" />
                                            <asp:CheckBox ID="CheckBox20" CssClass="checkbox check-success hide" runat="server" Text="Learner" />
                                            <asp:CheckBox ID="CheckBox21" CssClass="checkbox check-info hide" runat="server" Text="Qualified Plumber" />
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


                                    </div>
                                </div>
                            </div>
                        </div>




                </div>
            </div>
        </div>
        <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->



    <script>
       
        $("#<%=DirectorPlumber.ClientID%>").on('click', function () {
            var $cb = $(this);
            if ($cb.is(':checked')) {

                $("#<%=MasterPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=LicensedPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalOperatorPractitioner.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalAssistantPractitioner.ClientID%>").attr("disabled", "disabled");
                    $("#<%=LearnerPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=QualifiedPlumber.ClientID%>").attr("disabled", "disabled");
                } else {
                    $("#<%=MasterPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=LicensedPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalOperatorPractitioner.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalAssistantPractitioner.ClientID%>").removeAttr("disabled");
                    $("#<%=LearnerPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=QualifiedPlumber.ClientID%>").removeAttr("disabled");
                }
        });
        $("#<%=LearnerPlumber.ClientID%>").on('click', function () {
            var $cb = $(this);
            if ($cb.is(':checked')) {
                showLearnerDetails();
                $("#<%=MasterPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=LicensedPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalOperatorPractitioner.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalAssistantPractitioner.ClientID%>").attr("disabled", "disabled");
                    $("#<%=DirectorPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=QualifiedPlumber.ClientID%>").attr("disabled", "disabled");
                } else {
                    $("#<%=MasterPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=LicensedPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalOperatorPractitioner.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalAssistantPractitioner.ClientID%>").removeAttr("disabled");
                    $("#<%=DirectorPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=QualifiedPlumber.ClientID%>").removeAttr("disabled");
                }
        });
        $("#<%=LicensedPlumber.ClientID%>").on('click', function () {
                var $cb = $(this);
                if ($cb.is(':checked')) {
                    $("#<%=MasterPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=LearnerPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalOperatorPractitioner.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalAssistantPractitioner.ClientID%>").attr("disabled", "disabled");
                    $("#<%=DirectorPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=LicensedPlumberSolar.ClientID%>").removeAttr("disabled");
                    $("#<%=LicensedPlumberHeatPump.ClientID%>").removeAttr("disabled");
                    $("#<%=LicensedPlumberGas.ClientID%>").removeAttr("disabled");
                    $("#<%=QualifiedPlumber.ClientID%>").attr("disabled", "disabled");
                } else {
                    $("#<%=MasterPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=LearnerPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalOperatorPractitioner.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalAssistantPractitioner.ClientID%>").removeAttr("disabled");
                    $("#<%=DirectorPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=LicensedPlumberSolar.ClientID%>").attr("disabled", "disabled");
                    $("#<%=LicensedPlumberHeatPump.ClientID%>").attr("disabled", "disabled");
                    $("#<%=LicensedPlumberGas.ClientID%>").attr("disabled", "disabled");
                    $("#<%=QualifiedPlumber.ClientID%>").removeAttr("disabled");
                }
            });
            $("#<%=MasterPlumber.ClientID%>").on('click', function () {
                var $cb = $(this);
                if ($cb.is(':checked')) {
                    $("#<%=LicensedPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=LearnerPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalOperatorPractitioner.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalAssistantPractitioner.ClientID%>").attr("disabled", "disabled");
                    $("#<%=DirectorPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=MasterPlumberArbitrator.ClientID%>").removeAttr("disabled");
                    $("#<%=MasterPlumberEstimator.ClientID%>").removeAttr("disabled");
                    $("#<%=MasterPlumberTrainingAssesor.ClientID%>").removeAttr("disabled");
                    $("#<%=QualifiedPlumber.ClientID%>").attr("disabled", "disabled");
                } else {
                    $("#<%=LicensedPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=LearnerPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalOperatorPractitioner.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalAssistantPractitioner.ClientID%>").removeAttr("disabled");
                    $("#<%=DirectorPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=MasterPlumberArbitrator.ClientID%>").attr("disabled", "disabled");
                    $("#<%=MasterPlumberEstimator.ClientID%>").attr("disabled", "disabled");
                    $("#<%=MasterPlumberTrainingAssesor.ClientID%>").attr("disabled", "disabled");
                    $("#<%=QualifiedPlumber.ClientID%>").removeAttr("disabled");
                }
            });
            $("#<%=TechnicalOperatorPractitioner.ClientID%>").on('click', function () {
                var $cb = $(this);
                if ($cb.is(':checked')) {
                    showLearnerDetails();
                    $("#<%=LicensedPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=LearnerPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=MasterPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalAssistantPractitioner.ClientID%>").attr("disabled", "disabled");
                    $("#<%=DirectorPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalOperatorPractitionerDrainage.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalOperatorPractitionerColdWater.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalOperatorPractitionerHotWater.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalOperatorPractitionerWaterEnergyEfficiency.ClientID%>").removeAttr("disabled");
                    $("#<%=QualifiedPlumber.ClientID%>").attr("disabled", "disabled");
                } else {
                    $("#<%=LicensedPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=LearnerPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=MasterPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalAssistantPractitioner.ClientID%>").removeAttr("disabled");
                    $("#<%=DirectorPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalOperatorPractitionerDrainage.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalOperatorPractitionerColdWater.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalOperatorPractitionerHotWater.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalOperatorPractitionerWaterEnergyEfficiency.ClientID%>").attr("disabled", "disabled");
                    $("#<%=QualifiedPlumber.ClientID%>").removeAttr("disabled");
                }
            });
            $("#<%=TechnicalAssistantPractitioner.ClientID%>").on('click', function () {
                var $cb = $(this);
                if ($cb.is(':checked')) {
                    showLearnerDetails();
                    $("#<%=MasterPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=LicensedPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalOperatorPractitioner.ClientID%>").attr("disabled", "disabled");
                    $("#<%=DirectorPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=LearnerPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=QualifiedPlumber.ClientID%>").attr("disabled", "disabled");
                } else {
                    $("#<%=MasterPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=LicensedPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalOperatorPractitioner.ClientID%>").removeAttr("disabled");
                    $("#<%=DirectorPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=LearnerPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=QualifiedPlumber.ClientID%>").removeAttr("disabled");
                }
            });

            $("#<%=QualifiedPlumber.ClientID%>").on('click', function () {
                var $cb = $(this);
                if ($cb.is(':checked')) {
                    showLearnerDetails();
                    $("#<%=MasterPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=LicensedPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalOperatorPractitioner.ClientID%>").attr("disabled", "disabled");
                    $("#<%=TechnicalAssistantPractitioner.ClientID%>").attr("disabled", "disabled");
                    $("#<%=DirectorPlumber.ClientID%>").attr("disabled", "disabled");
                    $("#<%=LearnerPlumber.ClientID%>").attr("disabled", "disabled");
                } else {
                    $("#<%=MasterPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=LicensedPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalOperatorPractitioner.ClientID%>").removeAttr("disabled");
                    $("#<%=TechnicalAssistantPractitioner.ClientID%>").removeAttr("disabled");
                    $("#<%=DirectorPlumber.ClientID%>").removeAttr("disabled");
                    $("#<%=LearnerPlumber.ClientID%>").removeAttr("disabled");
                }
            });
    </script>

</asp:Content>
