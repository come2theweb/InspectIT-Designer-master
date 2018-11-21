<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddAssessment.aspx.cs" Inherits="InspectIT.AddAssessment" %>
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
        <div class="panel-title">Submit a CPD Activity
        </div>
        <div class="pull-right">
           
        </div>
        <div class="clearfix"></div>
        </div>
        <div class="panel-body">
       

<!-- BGINNING OF FORM TO ADD A NEW USER -->

<!-- First Row -->
<div class="row">
    <!-- Second Column -->
    <div class="col-md-12">
        <div class="alert alert-danger" runat="server" id="checkBxErr"></div>
         <div class="form-group form-group-default required">
                <label>Product Code :</label>
                    <div class="controls">
                        <div class="col-md-6">
                            
                        <asp:TextBox ID="CPDActivityID" CssClass="form-control hide" runat="server" ></asp:TextBox>
                        <asp:TextBox ID="productCode" CssClass="form-control" runat="server"  required></asp:TextBox>
                        </div>
                        <div class="col-md-6">

                        <asp:Button ID="searchProdCode" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="searchProdCode_Click" formnovalidate="formnovalidate" />
                        </div>
                        <span class="label label-danger" runat="server" id="errMsgProdCode"></span>
                    </div>
            </div>
            <div class="form-group form-group-default required">
                <label>Date of Activity :</label>
                    <div class="controls">
                        <asp:TextBox ID="activityDate" runat="server" CssClass="form-control datepicker-range" required></asp:TextBox>
                    </div>
            </div>
         
            <div class="form-group form-group-default required">
                <label>Reg No :</label>
                    <div class="controls">
                        <asp:TextBox ID="regno" runat="server" CssClass="form-control" required></asp:TextBox>
                    </div>
            </div>
       
            <div class="form-group form-group-default">
                <label>Name :</label>
                    <div class="controls">
                        <asp:TextBox ID="name" CssClass="form-control" runat="server" ></asp:TextBox>
                    </div>
            </div>

        <div class="form-group form-group-default">
                <label>Surname :</label>
                    <div class="controls">
                        <asp:TextBox ID="surname" CssClass="form-control" runat="server"></asp:TextBox>
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
                        <asp:TextBox ID="Activity" CssClass="form-control" runat="server" ></asp:TextBox>
                    </div>
            </div>
       
            <div class="form-group form-group-default">
                <label>CPD points :</label>
                    <div class="controls">
                        <asp:TextBox ID="Points" runat="server" CssClass="form-control" required></asp:TextBox>
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
                            <asp:TextBox ID="CommentsActivity" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control" ></asp:TextBox>
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
    
    <div class="col-md-12">
        
       

    </div>
</div>

            <div class="row">
                <div class="auto-style1">
                    <asp:Button ID="btn_add" CssClass="btn btn-primary" runat="server" style="float:right;" Text="Submit" OnClick="btn_add_Click" />
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
