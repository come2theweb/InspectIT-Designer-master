<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="quickview.ascx.cs" Inherits="InspectIT.Controls.quickview" %>
<!-- Nav tabs -->
<ul class="nav nav-tabs">
<li class="active">
    <a href="#quickview-alerts" data-toggle="tab">Alerts</a>
</li>
</ul>
<a class="btn-link quickview-toggle" data-toggle-element="#quickview" data-toggle="quickview"><i class="pg-close"></i></a>
<!-- Tab panes -->
<div class="tab-content">
<!-- BEGIN Notes !-->

<!-- BEGIN Alerts !-->
<div class="tab-pane fade active no-padding" id="quickview-alerts">
    <div class="view-port clearfix" id="alerts">
    <!-- BEGIN Alerts View !-->
    <div class="view bg-white">
        <!-- BEGIN View Header !-->
        <div class="navbar navbar-default navbar-sm">
        <div class="navbar-inner">
            <!-- BEGIN Header Controler !-->
            <a href="javascript:;" class="inline action p-l-10 link text-master" data-navigate="view" data-view-port="#chat" data-view-animation="push-parrallax">
            <i class="pg-more"></i>
            </a>
            <!-- END Header Controler !-->
            <div class="view-heading">
            Notications
            </div>
            <!-- BEGIN Header Controler !-->
            <a href="#" class="inline action p-r-10 pull-right link text-master">
            <i class="pg-search"></i>
            </a>
            <!-- END Header Controler !-->
        </div>
        </div>
        <!-- END View Header !-->
        <!-- BEGIN Alert List !-->
        <div data-init-list-view="ioslist" class="list-view boreded no-top-border">
        <!-- BEGIN List Group !-->
        <div class="list-view-group-container">
            <!-- BEGIN List Group Header!-->
            <div class="list-view-group-header text-uppercase">
            Calendar
            </div>
            <!-- END List Group Header!-->
            <ul>
            <!-- BEGIN List Group Item!-->
            <li class="alert-list">
                <!-- BEGIN Alert Item Set Animation using data-view-animation !-->
                <a href="javascript:;" class="" data-navigate="view" data-view-port="#chat" data-view-animation="push-parrallax">
                <p class="col-xs-height col-middle">
                    <span class="text-warning fs-10"><i class="fa fa-circle"></i></span>
                </p>
                <p class="p-l-10 col-xs-height col-middle col-xs-9 overflow-ellipsis fs-12">
                    <span class="text-master">Mathew Payne's Birthday</span>
                </p>
                <p class="p-r-10 col-xs-height col-middle fs-12 text-right">
                    <span class="text-warning">Today <br></span>
                    <span class="text-master">All Day</span>
                </p>
                </a>
                <!-- END Alert Item!-->
                <!-- BEGIN List Group Item!-->
            </li>
            <!-- END List Group Item!-->
            </ul>
        </div>
        <!-- END List Group !-->
        </div>
        <!-- END Alert List !-->
    </div>
    <!-- EEND Alerts View !-->
    </div>
</div>
<!-- END Alerts !-->

    </div>
</div>
</div>