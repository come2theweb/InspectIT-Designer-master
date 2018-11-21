<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="userstats.ascx.cs" Inherits="InspectIT.Controls.userstats" %>
<div class="visible-lg visible-md m-t-10">
<div class="pull-left p-r-10 p-t-10 fs-16 font-heading">
    <span class="semi-bold"><%=Session["IIT_UName"].ToString()%></span>
</div>
<div class="dropdown pull-right">
    <button class="profile-dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
    <span class="thumbnail-wrapper d32 circular inline m-t-5">
    <img src="assets/img/profiles/avatar.jpg" alt="" data-src="assets/img/profiles/avatar.jpg" data-src-retina="assets/img/profiles/avatar_small2x.jpg" width="32" height="32">
</span>
    </button>
    <ul class="dropdown-menu profile-dropdown" role="menu">
    <li><a href="userprofile"><i class="pg-settings_small"></i> Profile</a>
    </li>
    <li class="bg-master-lighter">
        <a href="logout" class="clearfix">
        <span class="pull-left"><a href="logout.aspx" style="">Logout</a></span>
        <span class="pull-right"><i class="pg-power"></i></span>
        </a>
    </li>
    </ul>
</div>
</div>