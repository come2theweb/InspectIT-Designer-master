<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="myDetails.aspx.cs" Inherits="InspectIT.myDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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

    <style>
        .datepicker-dropdown {
            z-index: 100000 !important;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid container-fixed-lg bg-white">
        <div class="panel panel-transparent">
            <div class="panel-heading">
               
            </div>
            <div class="panel-body">
                <div class="panel-body">
                    <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                    <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>


                    <ul class="nav nav-tabs nav-tabs-simple" role="tablist">
                        <li class="active"><a href="#plumberdetails" data-toggle="tab" role="tab">Plumber Details</a></li>
                        <li class="hide"><a href="#performance" data-toggle="tab" role="tab">Performance Status</a></li>
                        <li class="hide"><a href="#cpd" data-toggle="tab" role="tab">CPD</a></li>
                    </ul>

                    <div class="tab-content">
                        <div class="tab-pane active" id="plumberdetails">

                            <div id="plumber">

                                <div class="row">
                                    <h3>Registration Details</h3>
                                    <div class="row">
                                        <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group form-group-default">
                                                <label>First Registration Date:</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="RegistrationStart" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group form-group-default">
                                                <label>Next Renewal Date:</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="renewalDateTxt" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group form-group-default">
                                                <label>Next Renewal year:</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="Year" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                            </div>
                                        
                                        <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group form-group-default">
                                                <label>Registration Number:</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="regnoplumber" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group form-group-default">
                                                <label>Password:</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="Password" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-md-2">
                                            <a href="updatePassword"><label class="btn btn-primary">Change Password</label></a>
                                        </div>
                                            </div>
                                        <div class="form-group form-group-default">
                                            <label>Status:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="statusTxt" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <h3>PIRB Identification Card/Designations/Specialisations</h3>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div style="box-shadow: 10px 10px 5px #888888; width: 450px; border: 1px solid grey;" id="CardFront" runat="server"></div>

                                        </div>
                                        <div class="col-md-6">
                                            <div style="box-shadow: 10px 10px 5px #888888; width: 450px; border: 1px solid grey;" id="CardBack" runat="server"></div>

                                        </div>
                                        <br />
                                        <a href="OrderNewCard.aspx">
                                            <label style="margin: 5px;" class="btn btn-primary">Order A New Card</label></a>
                                        <div class="form-group form-group-default">
                                            <label>Change Photo:</label>
                                            <div class="controls">
                                                <asp:FileUpload ID="changePlumberPhoto" CssClass="form-control" runat="server" />
                                                <asp:Image ID="Image2" runat="server" />
                                            </div>
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
                                        <p style="color: red;">Note: Registration cards are optional and will be charged at date of renewal.</p>
                                    </div>
                                    <h3>Plumbers Message</h3>
                                    <div class="row col-md-12">
                                        <div class="form-group form-group-default">
                                            <label>Plumber Message:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="notice" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <h3>Registered Plumber Details</h3>

                                    <div class="col-md-6">


                                        <div class="form-group form-group-default">
                                            <label>Title:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="title" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group form-group-default">
                                            <label>Surname:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="Surname" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group form-group-default">
                                            <label>Alternate ID :</label>
                                            <div class="controls">
                                                <asp:TextBox ID="AlternateID" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group form-group-default required">
                                            <label>Gender :</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="Gender" CssClass="form-control" runat="server"  disabled>
                                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group form-group-default required ">
                                            <label>Racial Status</label>
                                            <asp:DropDownList ID="RacialStatus" runat="server" CssClass="form-control"  disabled>
                                                            <asp:ListItem Value="African">African</asp:ListItem>
                                                            <asp:ListItem Value="Coloured">Coloured</asp:ListItem>
                                                            <asp:ListItem Value="Indian">Indian</asp:ListItem>
                                                            <asp:ListItem Value="White">White</asp:ListItem>
                                                            <asp:ListItem Value="Other">Other</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group form-group-default  required">
                                            <label>South Africa National</label>
                                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control"  disabled>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group form-group-default  required">
                                            <label>Citizen Resident Status</label>
                                            <asp:DropDownList ID="ResidentStatus" runat="server" CssClass="form-control"  disabled>
                                                <asp:ListItem Value=""></asp:ListItem>
                                                <asp:ListItem Value="Dual (SA plus other)">Dual (SA plus other)</asp:ListItem>
                                                <asp:ListItem Value="Other">Other</asp:ListItem>
                                                <asp:ListItem Value="Permanent Resident">Permanent Resident</asp:ListItem>
                                                <asp:ListItem Value="South Africa">South Africa</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>


                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group form-group-default">
                                            <label>Name:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="Name" runat="server" CssClass="form-control"  disabled></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group form-group-default">
                                            <label>ID Number:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="IDNum" runat="server" CssClass="form-control"  disabled></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group form-group-default">
                                            <label>Attached ID :</label>
                                            <div class="controls">
                                                <asp:FileUpload ID="FileUpload2" runat="server"  disabled/>
                                                <asp:Image ID="Image1" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group form-group-default">
                                            <label>Date of Birth :</label>
                                            <div class="controls">
                                                <asp:TextBox ID="dob" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="form-group form-group-default  required">
                                            <label>Home Language</label>
                                            <asp:DropDownList ID="HomeLanguage" runat="server" CssClass="form-control"  disabled>
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
                                            <asp:DropDownList ID="Nationality" runat="server" CssClass="form-control" disabled>
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
                                            <asp:DropDownList ID="Disability" runat="server" CssClass="form-control"  disabled>
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

                                        <div class="form-group form-group-default hide">
                                            <label>Registration Details:</label>
                                            <div class="controls">
                                                <asp:CheckBoxList ID="regDetails" runat="server">
                                                    <asp:ListItem Value="Licensed Plumber">Licensed Plumber</asp:ListItem>
                                                    <asp:ListItem Value="Solar">Solar</asp:ListItem>
                                                    <asp:ListItem Value="Heat Pump">Heat Pump</asp:ListItem>
                                                    <asp:ListItem Value="Plumbing Training Assesor">Assessor</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="form-group form-group-default hide">
                                            <label>Signature:</label>
                                            <div class="controls">
                                                <asp:Image ID="plumberSignature" runat="server" />
                                            </div>
                                        </div>
                                    </div>


                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                         <h4>Postal Address</h4>
                                        <p style="color: red;">Note:All mail will be sent to this address</p>
                                                    <div class="form-group form-group-default required">
                                                        <label>Address :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="postalAddressPlumber" CssClass="form-control" runat="server"></asp:TextBox>
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
                                                            <asp:TextBox ID="adminPostProv" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                            <asp:TextBox ID="PostalCode" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                    </div>
                                    <div class="col-md-6">
                                         <h4>Physical Address</h4>
                                        <p style="color: red;">Note:All mail will be sent to this address</p>
                                                    <div class="form-group form-group-default required">
                                                        <label>Address :</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="PhysicalAddress" CssClass="form-control" runat="server"></asp:TextBox>
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
                                                            <asp:TextBox ID="adminPysProv" runat="server" CssClass="form-control"></asp:TextBox>
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

                                <div class="row">
                                         <h3>Contact Details</h3>
                                            <br />
                                            <p style="color: red;">Note: All notifications will be sent to these numbers and email addresses</p>
                                    <div class="col-md-6">
                                        <div class="form-group form-group-default">
                                            <label>Home Phone:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="homePhone" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group form-group-default">
                                            <label>Mobile Phone:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="Mobile" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group form-group-default">
                                            <label>Secondary Email Address:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="SecondEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                           <div class="form-group form-group-default">
                                            <label>Work Phone:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="workphone" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group form-group-default">
                                            <label>Email Address:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="plumberemail" runat="server" TextMode="Email" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                   
                                    <div class="row">
                                        <h3>Employment Details</h3>
                                        <div class="col-md-6">
                                                    <div class="form-group form-group-default required ">
                                                        <label>Employment Status</label>
                                                        <asp:DropDownList ID="EmploymentStatus" runat="server" CssClass="form-control">
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
                                                        <asp:TextBox ID="primaryContact" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Company VAT nr</label>
                                                        <asp:TextBox ID="CompanyVAt" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                                    </div>
                                                </div>
                                        <p style="color:red;">Note: If company name does not appear please contact PIRB.</p>
                                    </div>
                                    
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <h4>Employer Employment Postal Address</h4>
                                                    <div class="form-group form-group-default ">
                                                        <label>Address</label>
                                                        <asp:TextBox ID="empPostalAddress" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Province</label>
                                                        <asp:TextBox ID="empPostalProvince" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>City</label>
                                                        <asp:TextBox ID="empPostalCity" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Suburb</label>
                                                        <asp:TextBox ID="empPostalSuburb" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Code</label>
                                                        <asp:TextBox ID="empPostalCode" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <h4>Employer Physical Address</h4>
                                                    <div class="form-group form-group-default ">
                                                        <label>Address</label>
                                                        <asp:TextBox ID="empAddress" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Province</label>
                                                        <asp:TextBox ID="empProvince" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>City</label>
                                                        <asp:TextBox ID="empCity" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Suburb</label>
                                                        <asp:TextBox ID="empSuburb" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                    
                                            <div class="row">
                                                    <h4>Contact Details</h4>
                                                <div class="col-md-6">
                                                    <div class="form-group form-group-default ">
                                                        <label>Work Phone</label>
                                                        <asp:TextBox ID="empWorkPhone" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                                    </div>
                                                    <div class="form-group form-group-default ">
                                                        <label>Email Address</label>
                                                        <asp:TextBox ID="empEmailaddress" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group form-group-default ">
                                                        <label>Mobile</label>
                                                        <asp:TextBox ID="empMobile" CssClass="form-control" runat="server" disabled></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                <div class="row" id="hideCoCOverview" runat="server">
                                     <h3>COC Overview</h3>
                                    <div class="form-group form-group-default">
                                        <label>Number of Non Logged COC's:</label>
                                        <div class="controls">
                                            <asp:TextBox ID="nonloggedcoc" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group form-group-default">
                                        <label>Number of Non Logged COC's - Allocated:</label>
                                        <div class="controls">
                                            <asp:TextBox ID="nonloggedcocallocated" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group form-group-default">
                                        <label>Number of COC's Logged:</label>
                                        <div class="controls">
                                            <asp:TextBox ID="loggedcoc" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group form-group-default">
                                        <label>Number of COC’s Audited to Date:</label>
                                        <div class="controls">
                                            <asp:TextBox ID="numcocaudited" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group form-group-default">
                                        <label>Refix Notices:</label>
                                        <div class="controls">
                                            <asp:TextBox ID="refixNotices" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="auto-style1">
                                        <asp:Button ID="btnUpdatePlumber" CssClass="btn btn-primary" runat="server" Text="Update Profile" Style="float: right;" OnClick="btnUpdatePlumber_Click" />
                                    </div>
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

                                <label style="float: right;" class="btn btn-primary hide" onclick="openRolesAdd()">Add</label>
                            </div>

                        </div>
                        <div class="tab-pane" id="cpd">
                            <ul class="nav nav-tabs nav-tabs-simple" role="tablist">
                                <li class="active"><a href="#TabActiveActivity" data-toggle="tab" role="tab">Active</a></li>
                                <li class=""><a href="#TabArchivedActivity" data-toggle="tab" role="tab">Archived</a></li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane active" id="TabActiveActivity">
                                    <table class="table table-striped demo-table-search" id="stripedTable">
                                        <thead>
                                            <tr>
                                                <th>Date</th>
                                                <th>Category</th>
                                                <th>Activity</th>
                                                <th>CPD Points</th>
                                                <th>Status</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="Tbody1" runat="server">
                                        </tbody>
                                    </table>
                                </div>
                                <div class="tab-pane" id="TabArchivedActivity">
                                    <table class="table table-striped demo-table-search" id="stripedTable">
                                        <thead>
                                            <tr>
                                                <th>Date</th>
                                                <th>Category</th>
                                                <th>Activity</th>
                                                <th>CPD Points</th>
                                                <th>Status</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="Tbody2" runat="server">
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <div class="row">
                                <div class="auto-style1">
                                    <input type="button" value="Add Activity" class="btn btn-primary hide" style="float: right;" onclick="openActivity()" />
                                </div>
                            </div>

                        </div>
                        <div class="tab-pane" id="designationStat">
                            <h2>Designation</h2>
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
                            <asp:CheckBox ID="LearnerPlumber" CssClass="checkbox check-success" runat="server" Text="Learner" />
                            <asp:CheckBox ID="QualifiedPlumber" CssClass="checkbox check-info" runat="server" Text="Qualified Plumber" />

                            <h2>Status</h2>

                            <asp:CheckBox ID="Suspended" CssClass="checkbox check-warning" runat="server" Text="Suspended" />
                            <asp:CheckBox ID="CpdSuspension" CssClass="checkbox check-warning" runat="server" Text="CPD Restriction" />
                            <asp:CheckBox ID="Expired" CssClass="checkbox check-warning" runat="server" Text="Expired" />
                            <asp:CheckBox ID="Deceased" CssClass="checkbox check-warning" runat="server" Text="Deceased" />
                            <asp:CheckBox ID="Resigned" CssClass="checkbox check-warning" runat="server" Text="Resigned" />

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

                        <asp:TextBox ID="TextBox1aa" runat="server" CssClass="form-control"></asp:TextBox>
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


    <div id="modalActivity" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add / Update CPD Activity</h5>
                </div>
                <div class="modal-body">
                    <div class="form-group form-group-default required">
                        <label>Product Code :</label>
                        <div class="controls">
                            <div class="col-md-6">
                                <asp:TextBox ID="CPDActivityID" CssClass="form-control hide" runat="server"></asp:TextBox>
                                <asp:TextBox ID="productCode" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="btn btn-primary" onclick="searchProdID()">Search</label>
                            </div>
                            <span class="label label-danger" runat="server" id="errMsgProdCode"></span>
                        </div>
                    </div>
                    <div class="form-group form-group-default required">
                        <label>Date of Activity :</label>
                        <div class="controls">
                            <asp:TextBox ID="activityDate" runat="server" CssClass="form-control datepicker-range"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group form-group-default required">
                        <label>Reg No :</label>
                        <div class="controls">
                            <asp:TextBox ID="regno" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group form-group-default">
                        <label>Name :</label>
                        <div class="controls">
                            <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group form-group-default">
                        <label>Surname :</label>
                        <div class="controls">
                            <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group form-group-default">
                        <label>Category of Registration :</label>
                        <div class="controls">
                            <asp:DropDownList ID="Category" CssClass="form-control" runat="server">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="1">Category 1: Developmental Activities</asp:ListItem>
                                <asp:ListItem Value="2">Category 2: Work-based Activities</asp:ListItem>
                                <asp:ListItem Value="3">Category 3: Individual Activities</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group form-group-default">
                        <label>Activity :</label>
                        <div class="controls">
                            <asp:TextBox ID="Activity" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group form-group-default">
                        <label>CPD points :</label>
                        <div class="controls">
                            <asp:TextBox ID="pointsActivity" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-group-default">
                        <label>Attachments :</label>
                        <div class="controls">
                            <asp:FileUpload ID="img" CssClass="form-control" runat="server" />
                            <asp:Image ID="imageDisp" runat="server" />
                        </div>
                    </div>

                    <div class="form-group form-group-default">
                        <label>Comments :</label>
                        <div class="controls">
                            <asp:TextBox ID="CommentsActivity" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group form-group-default">
                        <div class="form-check form-check-inline">
                            <label class="form-check-label">
                                <asp:CheckBox ID="CheckBox1" runat="server" CssClass="form-check-input-styled" />
                                I declare that the information contained in this CPD Activity Register form, is complete, accurate and true to the best of my knowledge. I further declare that I understand that I must keep verifiable evidence of all the CPD activities for at least 2 years and PIRB may conduct a random audit of my activity(s) which would require me to submit the evidence to the PIRB.
                            </label>
                        </div>
                    </div>
                    <span class="alert alert-danger" id="Span1" style="display: none;"></span>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-link" data-dismiss="modal">Close</button>
                        <label class="btn btn-primary" onclick="saveAct()">Save</label>
                        <asp:Button ID="saveBtnActivity" runat="server" Text="Save" CssClass="btn btn-primary hide" OnClick="btn_add_Click" />
                        <asp:Button ID="updateBtnActivity" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="updateBtnActivity_Click" />

                    </div>
                </div>
            </div>
        </div>
    </div>



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

        var user = getUrlVars()["UserID"];

        $("#<%=CompanyID.ClientID%>").change(function () {
            console.log("Handler for .change() called.");
            console.log($(this).val());
            $.ajax({
                type: "POST",
                url: 'API/WebService1.asmx/getCompanyInfo',
                data: { id: $(this).val() },
                success: function (data) {
                    $("#<%=primaryContact.ClientID%>").val(data.primaryContact);
                        $("#<%=CompanyVAt.ClientID%>").val(data.CompanyVAt);
                        $("#<%=empPostalAddress.ClientID%>").val(data.empPostalAddress);
                        $("#<%=empPostalProvince.ClientID%>").val(data.empPostalProvince);
                        $("#<%=empPostalCity.ClientID%>").val(data.empPostalCity);
                        $("#<%=empPostalSuburb.ClientID%>").val(data.empPostalSuburb);
                        $("#<%=empPostalCode.ClientID%>").val(data.empPostalCode);
                        $("#<%=empAddress.ClientID%>").val(data.empAddress);
                        $("#<%=empProvince.ClientID%>").val(data.empProvince);
                        $("#<%=empCity.ClientID%>").val(data.empCity);
                        $("#<%=empSuburb.ClientID%>").val(data.empSuburb);
                        $("#<%=empWorkPhone.ClientID%>").val(data.empWorkPhone);
                        $("#<%=empMobile.ClientID%>").val(data.empMobile);
                        $("#<%=empEmailaddress.ClientID%>").val(data.empEmailaddress);

                },
            });
         });

        function openRolesAdd() {
            $("#modal_default").modal();
            $("#<%=saveBtn.ClientID%>").show();
                $("#<%=updateBtn.ClientID%>").hide();
            }

            function openActivity() {
                $("#modalActivity").modal();
                $("#<%=saveBtnActivity.ClientID%>").show();
                $("#<%=updateBtnActivity.ClientID%>").hide();
            }


            function searchProdID() {
                $.ajax({
                    type: "POST",
                    url: 'API/WebService1.asmx/searchProductCodePerformanceActivity',
                    data: { prodCode: $("#<%=productCode.ClientID%>").val(), uid: user },
                    success: function (data) {
                        $("#<%=activityDate.ClientID%>").val(data.startDate);
                        $("#<%=regno.ClientID%>").val(data.regno);
                        $("#<%=TextBox2.ClientID%>").val(data.name);
                        $("#<%=TextBox3.ClientID%>").val(data.surname);
                        $("#<%=Category.ClientID%>").val(data.Category);
                        $("#<%=Activity.ClientID%>").val(data.Activity);
                        $("#<%=pointsActivity.ClientID%>").val(data.Points);
                        $("#<%=CPDActivityID.ClientID%>").val(data.CPDActivityID);

                    },
                });
            }

            function saveAct() {
                if ($("#<%=CheckBox1.ClientID%>").is(':checked')) {
                    $.ajax({
                        type: "POST",
                        url: 'API/WebService1.asmx/savePerformanceActivity',
                        data: { prodCode: $("#<%=productCode.ClientID%>").val(), comm: $("#<%=CommentsActivity.ClientID%>").val(), uid: user, cat: $("#<%=Category.ClientID%>").val(), CpdActId: $("#<%=CPDActivityID.ClientID%>").val(), act: $("#<%=Activity.ClientID%>").val(), point: $("#<%=pointsActivity.ClientID%>").val(), date: $("#<%=activityDate.ClientID%>").val(), dec: $("#<%=CheckBox1.ClientID%>").val() },
                        success: function (data) {
                            location.reload();
                        },
                    });
                }
                else {
                    $("#Span1").show();
                    $("#Span1").html("Please check declaration before adding activity");
                }
            }

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
                    $("#<%=TextBox1aa.ClientID%>").val(data.Attachment);
                    $("#<%=endDate.ClientID%>").val(data.endDate);
                    if (data.hasEndDate == "True") {
                        $("#<%=hasEndDate.ClientID%>").attr("checked", "checked");
                    }
                },
                });
            }

            function editCpdActivity(idd) {
                $("#modalActivity").modal();
                $("#<%=saveBtnActivity.ClientID%>").hide();
                $("#<%=updateBtnActivity.ClientID%>").hide();

                $("#<%=PerformanceStatusID.ClientID%>").val(idd);
                $.ajax({
                    type: "POST",
                    url: 'API/WebService1.asmx/getCPDActivityDetails',
                    data: { id: idd },
                    success: function (data) {


                        $("#<%=img.ClientID%>").hide();
                        $("#<%=imageDisp.ClientID%>").val(data.Attachment);
                        $("#<%=productCode.ClientID%>").val(data.productCode);
                        $("#<%=CommentsActivity.ClientID%>").val(data.comment);
                    $("#<%=Category.ClientID%>").val(data.Category);
                $("#<%=Activity.ClientID%>").val(data.Activity);
                $("#<%=pointsActivity.ClientID%>").val(data.NoPoints);
                $("#<%=activityDate.ClientID%>").val(data.CertificateDate);

                $("#<%=CheckBox1.ClientID%>").attr("checked", "checked");

                    },
                });
            }

            $('.datepicker-range').datepicker();


            $("#<%=performanceType.ClientID%>").change(function () {
                //alert("Handler for .change() called.");
                $.ajax({
                    type: "POST",
                    url: 'API/WebService1.asmx/getPerformancePoints',
                    data: { pt: $("#<%=performanceType.ClientID%>").val() },
                    success: function (data) {
                        $("#<%=points.ClientID%>").val(data);
                    },
                });
            });

            function deleteconf(url) {
                var result = confirm("Are you sure?");
                if (result) {
                    document.location.href = url;
                }
            }

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


            //$('.datepicker-range').datepicker();
            $("#<%=MasterPlumberTrainingAssesor.ClientID%>").attr("disabled", "disabled");
            $("#<%=MasterPlumberEstimator.ClientID%>").attr("disabled", "disabled");
            $("#<%=MasterPlumberArbitrator.ClientID%>").attr("disabled", "disabled");
            $("#<%=LicensedPlumberSolar.ClientID%>").attr("disabled", "disabled");
             $("#<%=LicensedPlumberHeatPump.ClientID%>").attr("disabled", "disabled");
            $("#<%=LicensedPlumberGas.ClientID%>").attr("disabled", "disabled");
            $("#<%=TechnicalOperatorPractitionerDrainage.ClientID%>").attr("disabled", "disabled");
            $("#<%=TechnicalOperatorPractitionerColdWater.ClientID%>").attr("disabled", "disabled");
            $("#<%=TechnicalOperatorPractitionerHotWater.ClientID%>").attr("disabled", "disabled");
            $("#<%=TechnicalOperatorPractitionerWaterEnergyEfficiency.ClientID%>").attr("disabled", "disabled");

            $("#<%=DirectorPlumber.ClientID%>").attr("disabled", "disabled");
            $("#<%=MasterPlumber.ClientID%>").attr("disabled", "disabled");
            $("#<%=TechnicalOperatorPractitioner.ClientID%>").attr("disabled", "disabled");
            $("#<%=TechnicalAssistantPractitioner.ClientID%>").attr("disabled", "disabled");
            $("#<%=LearnerPlumber.ClientID%>").attr("disabled", "disabled");
            $("#<%=LicensedPlumber.ClientID%>").attr("disabled", "disabled");
            $("#<%=QualifiedPlumber.ClientID%>").attr("disabled", "disabled");



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

            $("#<%=QualifiedPlumber.ClientID%>").on('click', function () {
                var $cb = $(this);
                if ($cb.is(':checked')) {
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
            //DirectorPlumber MasterPlumber MasterPlumberTrainingAssesor MasterPlumberEstimator MasterPlumberArbitrator LicensedPlumber LicensedPlumberSolar
            //LicensedPlumberHeatPump LicensedPlumberGas TechnicalOperatorPractitioner TechnicalOperatorPractitionerDrainage TechnicalOperatorPractitionerColdWater
            //TechnicalOperatorPractitionerHotWater TechnicalOperatorPractitionerWaterEnergyEfficiency TechnicalAssistantPractitioner LearnerPlumber 
            $("#<%=TechnicalOperatorPractitioner.ClientID%>").on('click', function () {
                var $cb = $(this);
                if ($cb.is(':checked')) {
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


            if ($("#<%=LicensedPlumber.ClientID%>").is(':checked')) {
                console.log("yes");
                $("#<%=LicensedPlumberSolar.ClientID%>").removeAttr("disabled");
                $("#<%=LicensedPlumberHeatPump.ClientID%>").removeAttr("disabled");
                $("#<%=LicensedPlumberGas.ClientID%>").removeAttr("disabled");
                $("#<%=LicensedPlumber.ClientID%>").removeAttr("disabled");
            }
            else if ($("#<%=DirectorPlumber.ClientID%>").is(':checked')) {
                $("#<%=DirectorPlumber.ClientID%>").removeAttr("disabled");
            }
            else if ($("#<%=LearnerPlumber.ClientID%>").is(':checked')) {
                $("#<%=LearnerPlumber.ClientID%>").removeAttr("disabled");
            }
            else if ($("#<%=TechnicalAssistantPractitioner.ClientID%>").is(':checked')) {
                $("#<%=TechnicalAssistantPractitioner.ClientID%>").removeAttr("disabled");
            }
            else if ($("#<%=QualifiedPlumber.ClientID%>").is(':checked')) {
                $("#<%=QualifiedPlumber.ClientID%>").removeAttr("disabled");
            }
            else if ($("#<%=MasterPlumber.ClientID%>").is(':checked')) {
                $("#<%=MasterPlumber.ClientID%>").removeAttr("disabled");
                $("#<%=MasterPlumberTrainingAssesor.ClientID%>").removeAttr("disabled");
                $("#<%=MasterPlumberEstimator.ClientID%>").removeAttr("disabled");
                $("#<%=MasterPlumberArbitrator.ClientID%>").removeAttr("disabled");
            }
            else if ($("#<%=TechnicalOperatorPractitioner.ClientID%>").is(':checked')) {
                $("#<%=TechnicalOperatorPractitioner.ClientID%>").removeAttr("disabled");
                $("#<%=TechnicalOperatorPractitionerDrainage.ClientID%>").removeAttr("disabled");
                $("#<%=TechnicalOperatorPractitionerColdWater.ClientID%>").removeAttr("disabled");
                $("#<%=TechnicalOperatorPractitionerHotWater.ClientID%>").removeAttr("disabled");
                $("#<%=TechnicalOperatorPractitionerWaterEnergyEfficiency.ClientID%>").removeAttr("disabled");
            }
    </script>

</asp:Content>
