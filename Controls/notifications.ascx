<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="notifications.ascx.cs" Inherits="InspectIT.Controls.notifications" %>
<ul class="notification-list no-margin hidden-sm hidden-xs b-grey b-l b-r no-style p-l-30 p-r-20">
    <li class="p-r-15 inline">
        <div class="dropdown">
            <a href="javascript:;" id="noti" class="icon-set globe-fill" data-toggle="dropdown" runat="server">
                <%--<span class="bubble"></span>--%>
            </a>
            <div class="dropdown-menu notification-toggle" role="menu" aria-labelledby="notification-center">
                <div class="notification-panel">

                    <div class="notification-body scrollable" id="messageListDisp" runat="server">
                       <%-- <div class="notification-item unread clearfix">
                            <div class="heading open">
                                <div class="more-details">
                                    <div class="more-details-inner">
                                        <h5 class="semi-bold fs-16"></h5>
                                    </div>
                                </div>
                            </div>
                            <div class="option" data-toggle="tooltip" data-placement="left" title="mark as read">
                                <a href="#" class="mark"></a>
                            </div>
                        </div>--%>
                    </div>

                    <div class="notification-footer text-center" id="NotifyText" runat="server">
                        <%--<a href="#" class="">No New Notifications</a>--%>
                        <a data-toggle="refresh" class="portlet-refresh text-black pull-right" href="#">
                            <i class="pg-refresh_new"></i>
                        </a>
                    </div>

                </div>
            </div>
        </div>
    </li>
    <!--<li class="p-r-15 inline">
    <a href="#" class="icon-set clip "></a>
    </li>
    <li class="p-r-15 inline">
    <a href="#" class="icon-set grid-box"></a>
    </li>-->
</ul>

<script>
    function markMsgRead(id, uid) {
        $.get("../API/markMsgRead.aspx?uid=" + uid + "&id=" + id, function (data) {
            
        });
    }
</script>