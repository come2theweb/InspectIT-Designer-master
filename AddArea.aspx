<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddArea.aspx.cs" Inherits="InspectIT.AddArea" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="assets/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <link href="pages/css/pages.css" rel="stylesheet" />
    <link href="pages/css/ie9.css" rel="stylesheet" />
    <link href="pages/css/pages.min.css" rel="stylesheet" />
    <link href="pages/css/themes/abstract.css" rel="stylesheet" />
    <style>
        h4{
            font-weight:bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- START CONTAINER FLUID -->
    <div class="container-fluid container-fixed-lg bg-white">
    <!-- START PANEL -->
    <div class="panel panel-transparent">
        <div class="panel-heading">
        <div class="panel-title">Add Area
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

            <!-- Third Row -->
<div class="row">
    <h4>Manage Areas</h4>
    
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
                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

    <div class="form-group form-group-default required">
                            <label>Suburb :</label>
                            <div class="controls">
                                <asp:TextBox ID="suburbname" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>



</div>


            <div class="row">
                <div class="auto-style1">
                    <asp:Button ID="btn_add" CssClass="btn btn-primary" runat="server" style="float:right;" Text="Save" OnClick="btn_add_Click"/>
                    <asp:Button ID="btn_update" CssClass="btn btn-primary" runat="server" style="float:right;" Text="Update" OnClick="btn_update_Click"/>
                </div>
            </div> 
       

        </div>
    </div>
    <!-- END PANEL -->
    </div>
    <!-- END CONTAINER FLUID -->
    

    <script>
        $('.datepicker-range').datepicker();

        

    </script>

</asp:Content>
