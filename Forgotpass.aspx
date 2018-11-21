<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forgotpass.aspx.cs" Inherits="InspectIT.Forgotpass" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="content-type" content="text/html;charset=UTF-8" />
    <meta charset="utf-8" />
    <title>OHS Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <link rel="apple-touch-icon" href="pages/ico/60.png">
    <link rel="apple-touch-icon" sizes="76x76" href="pages/ico/76.png">
    <link rel="apple-touch-icon" sizes="120x120" href="pages/ico/120.png">
    <link rel="apple-touch-icon" sizes="152x152" href="pages/ico/152.png">
    <link rel="icon" type="image/x-icon" href="favicon.ico" />
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-touch-fullscreen" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="default">
    <meta content="" name="description" />
    <meta content="" name="author" />
    <link href="assets/plugins/pace/pace-theme-flash.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/boostrapv3/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/jquery-scrollbar/jquery.scrollbar.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="assets/plugins/bootstrap-select2/select2.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="assets/plugins/switchery/css/switchery.min.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="pages/css/pages-icons.css" rel="stylesheet" type="text/css">
    <link class="main-stylesheet" href="pages/css/pages.css" rel="stylesheet" type="text/css" />
    <!--[if lte IE 9]>
        <link href="pages/css/ie9.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <script type="text/javascript">
        window.onload = function () {
            // fix for windows 8
            if (navigator.appVersion.indexOf("Windows NT 6.2") != -1)
                document.head.innerHTML += '<link rel="stylesheet" type="text/css" href="pages/css/windows.chrome.fix.css" />'
        }
    </script>
</head>
<body class="fixed-header">
    
    <!-- START PAGE-CONTAINER -->
    <div class="login-wrapper ">
      <!-- START Login Background Pic Wrapper-->
      <div class="bg-pic">
        <!-- START Background Pic-->
        <!-- <img src="assets/img/demo/new-york-city-buildings-sunrise-morning-hd-wallpaper.jpg" data-src="assets/img/demo/new-york-city-buildings-sunrise-morning-hd-wallpaper.jpg" data-src-retina="assets/img/demo/new-york-city-buildings-sunrise-morning-hd-wallpaper.jpg" alt="" class="lazy"> -->
        <!-- END Background Pic-->
        <!-- START Background Caption-->
        <div class="bg-caption pull-bottom sm-pull-bottom text-white p-l-20 m-b-20">
          <h2 class="semi-bold text-white">OHS</h2>
          <p class="small">
            Pay off line here.
          </p>
        </div>
        <!-- END Background Caption-->
      </div>
      <!-- END Login Background Pic Wrapper-->
      <!-- START Login Right Container-->
      <div class="login-container bg-white">
        <div class="p-l-50 m-l-20 p-r-50 m-r-20 p-t-50 m-t-30 sm-p-l-15 sm-p-r-15 sm-p-t-40">
          <div style="text-align:center;width:100%;">
              <img src="assets/img/logo.png" />
          </div>
          <p class="p-t-35">Forgot your password?</p>
          <!-- START Login Form -->
            <form id="form1" runat="server">
            <!-- START Form Control-->
            <div class="form-group form-group-default">
              <label>Email Address</label>
              <div class="controls">
                    <asp:TextBox ID="username" runat="server" placeholder="Enter your email address" CssClass="form-control" required></asp:TextBox>
              </div>
            </div>
            <div id="successbox" runat="server" class="alert alert-success" visible="false"></div>
            <div id="errorbox" runat="server" class="alert alert-danger" visible="false"></div>
            <asp:Button ID="ResetPass" runat="server" Text="Reset Password" CssClass="btn btn-primary btn-cons" OnClick="ResetPass_Click" />
          <!--END Login Form-->
          </form>
          <div class="pull-bottom sm-pull-bottom">
            <div class="m-b-30 p-r-80 sm-m-t-20 sm-p-r-15 sm-p-b-20 clearfix">
              <div class="col-sm-3 col-md-2 no-padding"></div>
              <div class="col-sm-9 no-padding m-t-10">
                <p></p>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- END Login Right Container-->
    </div>
    <!-- END PAGE CONTAINER -->
    <!-- BEGIN VENDOR JS -->
    <script src="assets/plugins/pace/pace.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="assets/plugins/modernizr.custom.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="assets/plugins/boostrapv3/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery/jquery-easy.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-unveil/jquery.unveil.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-bez/jquery.bez.min.js"></script>
    <script src="assets/plugins/jquery-ios-list/jquery.ioslist.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-actual/jquery.actual.min.js"></script>
    <script src="assets/plugins/jquery-scrollbar/jquery.scrollbar.min.js"></script>
    <script type="text/javascript" src="assets/plugins/bootstrap-select2/select2.min.js"></script>
    <script type="text/javascript" src="assets/plugins/classie/classie.js"></script>
    <script src="assets/plugins/switchery/js/switchery.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-validation/js/jquery.validate.js" type="text/javascript"></script>
    <!-- END VENDOR JS -->
    <!-- BEGIN CORE TEMPLATE JS -->
    <script src="pages/js/pages.min.js"></script>
    <!-- END CORE TEMPLATE JS -->
    <!-- BEGIN PAGE LEVEL JS -->
    <script src="assets/js/scripts.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL JS -->
    <script>
        $(function () {
            $('#form1').validate()
        })
    </script>
    
  </body>
</html>
