<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="InspectIT.Controls.header" %>
<div class="sidebar-header">
<img src="assets/img/logo_white.png" alt="logo" class="brand" data-src="assets/img/logo_white.png" data-src-retina="assets/img/logo_white_2x.png" width="78" height="22">
<div class="sidebar-header-controls">
    <button data-toggle-pin="sidebar" class="btn btn-link visible-lg-inline" type="button" onclick="SetMenu()"><i class="fa fs-12"></i>
    </button>
</div>
</div>
<script>

    function SetMenu() {

        if (localStorage.PinMenu == 'undefined') {
            localStorage.PinMenu = true;
        } else {
            if (localStorage.PinMenu == 'true') {
                localStorage.PinMenu = 'false';
            } else {
                localStorage.PinMenu = 'true';
            }
        }

    }

</script>