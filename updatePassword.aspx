<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="updatePassword.aspx.cs" Inherits="InspectIT.updatePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
    <style>
        .modal-backdrop{
            display:none;
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
                    Update Password
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">
                
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>
                 <div class="alert alert-danger" id="Div1" runat="server" visible="false"></div>
                <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                
                <div class="form-group form-group-default">
                    <label>Old Password :</label>
                    <div class="controls">
                        <asp:TextBox ID="oldPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
              
                <div class="form-group form-group-default">
                    <label>New Password :</label>
                    <div class="controls">
                        <asp:TextBox ID="newPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
              <div class="form-group form-group-default">
                    <label>Confirm New Password :</label>
                    <div class="controls">
                        <asp:TextBox ID="confirmNewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
              <div class="alert alert-danger" id="Div2" style="display:none;"></div>
              <div class="alert alert-success" id="Div3" style="display:none;"></div>
                <div class="row">
                    <div class="auto-style1">
                        <a href="userDashboard.aspx">
                            <input type="button" value="Cancel" class="btn btn-primary" style="float: right; margin-left: 10px;" />
                        </a>
                      <button type="button" class="btn btn-primary" onclick="clickSendOtp()" style="float: right; margin-left: 10px;">Change</button>
                    </div>
                </div>

            </div>
        </div>
        <!-- END PANEL -->
    </div>

    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Enter OTP</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
       <div class="form-group form-group-default">
                    <label>Enter OTP :</label>
                    <div class="controls">
                        <asp:TextBox ID="otpCode" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            <asp:Button ID="btn_buy" runat="server" Text="Update" cssclass="btn btn-primary hide"  style="float: right;" OnClick="btn_buy_Click" />
          <label onclick="updatePass()" class="btn btn-primary">Update</label>

      </div>
    </div>
  </div>
</div>

    <script>
        function clickSendOtp() {
            $.ajax({
                type: "POST",
                url: 'https://197.242.82.242/inspectit/API/WebService1.asmx/sendOTPChangePasswords',
                data: { uid: <%=Session["IIT_UID"].ToString()%>},
                success: function (data) {
                    localStorage.setItem("OTPCode", data);
                    $('#exampleModal').modal('toggle');
                },
            });
        }

        function updatePass() {
            $.ajax({
                type: "POST",
                url: 'https://197.242.82.242/inspectit/API/WebService1.asmx/updatePassword',
                data: { uid: <%=Session["IIT_UID"].ToString()%>,newPass: $("#<%=newPassword.ClientID%>").val(), oldPass: $("#<%=oldPassword.ClientID%>").val()},
                success: function (data) {
                    if (data == "done") {
                        document.location.href = "myDetails";
                    }
                    else {
                        $("#Div3").hide();
                        $("#Div2").show();
                        $("#Div2").html(data);
                    }
                    
                },
             });
        }

        //Div2Div3
        $("#<%=confirmNewPassword.ClientID%>").keyup(function () {
            if ($("#<%=confirmNewPassword.ClientID%>").val() != $("#<%=newPassword.ClientID%>").val()) {
                $("#Div3").hide();
                $("#Div2").show();
                $("#Div2").html("Passwords do not match");
            }
            else {
                $("#Div2").hide();
                $("#Div3").show();
                $("#Div3").html("Passwords match!");
            }
        });
        
    </script>
</asp:Content>
