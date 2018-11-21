<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="InspectIT.Controls.menu" %>

<div class="sidebar-menu" id="supplierMenu" runat="server">
    <ul class="menu-items">
        <li class="m-t-30">
        <a href="ViewCOCStatementSupplier.aspx" class="detailed"><span class="title">Reseller COC Statement</span></a>
        <span class="icon-thumbnail "><i class="pg-home"></i></span>
    </li>
    <li class="">
        <a href="PurchasePlumbingCOCSupplier.aspx"><span class="title">Allocate COC</span></a>
        <span class="icon-thumbnail "><i class="pg-social"></i></span>
    </li>
    <li class="">
        <a href="PurchasePlumbingCOCSupplierSide.aspx"><span class="title">Purchase COC</span></a>
        <span class="icon-thumbnail "><i class="pg-social"></i></span>
    </li>
         <li class="">
            <a href="supplierOrders.aspx"><span class="title">My Orders/Invoices</span></a> 
            <span class="icon-thumbnail "><i class="pg-social"></i></span>
        </li>
    </ul>
    <div class="clearfix"></div>
</div>

<div class="sidebar-menu" id="staffMenu" runat="server">
    <ul class="menu-items">
        <li class="m-t-30">
        <a href="userDashboard.aspx" class="detailed"><span class="title">Dash Board</span></a>
        <span class="icon-thumbnail "><i class="pg-home"></i></span>
        </li>
        <li class="" runat="server" id="hideItemCocStat">
        <a href="ViewCOCStatement.aspx" class="detailed"><span class="title">COC Statement</span></a>
        <span class="icon-thumbnail "><i class="pg-home"></i></span>
        </li>
        <li class="" runat="server" id="hideItemCocPurch">
            <a href="PurchasePlumbingCOCs.aspx"><span class="title">Purchase COC</span></a>
            <span class="icon-thumbnail "><i class="pg-social"></i></span>
        </li>
        <li class="" runat="server" id="hideItemaAduit">
            <a href="ViewRefixandAuditStatement.aspx"><span class="title">Audit Review <span class="text-danger" id="countRefixes" runat="server"></span></span></a>
            <span class="icon-thumbnail "><i class="pg-social"></i></span>
        </li>
        <li class="">
            <a href="UserOrders.aspx"><span class="title">My Orders/Invoices</span></a> 
            <span class="icon-thumbnail "><i class="pg-social"></i></span>
        </li>
        <li class="">
            <a href="UserAssessments.aspx"><span class="title">My CPD</span></a> 
            <span class="icon-thumbnail "><i class="pg-social"></i></span>
        </li>
        <li class="">
            <a href="UserPerformanceStatus.aspx"><span class="title">Performance Status</span></a> 
            <span class="icon-thumbnail "><i class="pg-social"></i></span>
        </li>
        <li class="">
            <a href="myDetails.aspx"><span class="title">My Details</span></a> 
            <span class="icon-thumbnail "><i class="pg-social"></i></span>
        </li>
        <li class="">
            <a href="userReports.aspx"><span class="title">My Reports</span></a> 
            <span class="icon-thumbnail "><i class="pg-social"></i></span>
        </li>
        <li class="navigation-header"><span  style="margin-left:10px;color:#fff;">System Messages</span> <i class="icon-menu" title="" data-original-title="Messages"></i></li>
        <div id="dispStaffMsg" runat="server"></div>
    </ul>
     
    <div class="clearfix"></div>
   
</div>

<div class="sidebar-menu" id="adminMenu" runat="server">

        <ul class="menu-items">
            <li class="">
            <a href="HomeCOCAdmin.aspx">Dashboard</a>
            <span class="icon-thumbnail" style="padding-top:10px;"><i class="pg-home"></i></span>
            </li>

             <li class="" id="Administration" runat="server">
                <a href="javascript:;"><span class="title">Administration</span><span class="arrow"></span></a>
                <span class="icon-thumbnail" style="padding-top:10px;"><i class="icon-pencil5"></i></span>
                <ul class="sub-menu">
                  <li class="hide">
                      <a href="InvoiceList.aspx">Transactions</a>
                      <span class="icon-thumbnail">tr</span>
                  </li>
                  <li class="hide">
                    <a href="ViewUser.aspx">System Users</a>
                    <span class="icon-thumbnail">us</span>
                  </li>
                  <li class="hide">
                    <a href="settings.aspx">Settings</a>
                    <span class="icon-thumbnail">s</span>
                  </li>
                   
                  <li>
                    <a href="reportQuestions.aspx">Report Statement List</a>
                    <span class="icon-thumbnail">rq</span>
                     <ul>
						<li><a href="installationTypes.aspx">Installation Types</a></li>
						<li><a href="installationTypesSub.aspx">Sub Types</a></li>
						<li><a href="reportQuestions.aspx">Report Statements</a></li>
					</ul>
                  </li>
                  <li class="hide">
                    <a href="Rates.aspx">Rates</a>
                    <span class="icon-thumbnail">r</span>
                  </li>
                  <li class="hide">
                    <a href="MessageList.aspx">Messages</a>
                    <span class="icon-thumbnail">m</span>
                  </li>
                  <li class="">
                    <a href="ViewCertificate.aspx">Certificate Stock</a>
                    <span class="icon-thumbnail">ct</span>
                  </li>
                    <li class="">
                    <a href="ViewReporting.aspx">Report</a>
                    <span class="icon-thumbnail">r</span>
                  </li>
                    <li class="">
                    <a href="ViewAreas.aspx">Manage Areas</a>
                    <span class="icon-thumbnail">ma</span>
                  </li>

                </ul>
            </li>

            <li class="" id="Li1" runat="server">
                <a href="javascript:;"><span class="title">System Setup</span><span class="arrow"></span></a>
                <span class="icon-thumbnail" style="padding-top:10px;"><i class="icon-gear"></i></span>
                <ul class="sub-menu">
                  <li class="">
                    <a href="ViewUser.aspx">System Users</a>
                    <span class="icon-thumbnail">su</span>
                  </li>
                  <li class="">
                    <a href="settings.aspx">Settings</a>
                    <span class="icon-thumbnail">s</span>
                  </li>
                    <li class="">
                    <a href="Rates.aspx">Rates</a>
                    <span class="icon-thumbnail">r</span>
                  </li>
                  <li class="">
                    <a href="MessageList.aspx">Messages</a>
                    <span class="icon-thumbnail">m</span>
                  </li>
                  <li class="">
                    <a href="AssessmentTypes.aspx">Assessment Types</a>
                    <span class="icon-thumbnail">pt</span>
                  </li>
                  <li class="">
                    <a href="PerformanceTypes.aspx">Performance Types</a>
                    <span class="icon-thumbnail">pt</span>
                  </li>
                  <li class="">
                    <a href="LocationTypes.aspx">Location Types</a>
                    <span class="icon-thumbnail">pt</span>
                  </li>
                  <li class="">
                    <a href="CpdActivities.aspx">CPD Activities</a>
                    <span class="icon-thumbnail">ca</span>
                  </li>
                  <li class="">
                    <a href="QualificationRoute.aspx">Quallification Route List</a>
                    <span class="icon-thumbnail">qr</span>
                  </li>
                 
                </ul>
            </li>

             <li class="" id="Li3" runat="server">
                <a href="javascript:;"><span class="title">Registrar</span><span class="arrow"></span></a>
                <span class="icon-thumbnail" style="padding-top:10px;"><i class="icon-clipboard3"></i></span>
                <ul class="sub-menu">
                  <li class="">
                    <a href="ViewPlumbers.aspx">Plumber Registrar</a>
                    <span class="icon-thumbnail">na</span>
                  </li>
                  <li class="">
                    <a href="companies.aspx">Company Registrar</a>
                    <span class="icon-thumbnail">cl</span>
                  </li>
                </ul>
            </li>
             <li class="" id="Li7" runat="server">
                <a href="javascript:;"><span class="title">Activity Queue</span><span class="arrow"></span></a>
                <span class="icon-thumbnail" style="padding-top:10px;"><i class="icon-clipboard3"></i></span>
                <ul class="sub-menu">
                  <li class="">
                    <a href="newApplications.aspx">New Applications</a>
                    <span class="icon-thumbnail">na</span>
                  </li>
                  <li class="">
                    <a href="newApplicationsUpdate.aspx">Update Applications</a>
                    <span class="icon-thumbnail">e</span>
                  </li>
                  <li class="">
                    <a href="AssessmentsQueue.aspx">Assessments Q</a>
                    <span class="icon-thumbnail">aq</span>
                  </li>
                  <li class="">
                    <a href="CpdActivitiesQueue.aspx">CPD Activity Q</a>
                    <span class="icon-thumbnail">cq</span>
                  </li>
                </ul>
            </li>


             <li class="" id="Li4" runat="server">
                <a href="javascript:;"><span class="title">Accounts</span><span class="arrow"></span></a>
                <span class="icon-thumbnail" style="padding-top:10px;"><i class="icon-stats-growth2"></i></span>
                <ul class="sub-menu">
                  <li class="">
                     <a href="InvoiceList.aspx">Transactions</a>
                      <span class="icon-thumbnail">tr</span>
                  </li>
                </ul>
            </li>

           

            <li class="" id="Audits" runat="server">
                <a href="javascript:;"><span class="title">Audits</span><span class="arrow"></span></a>
                <span class="icon-thumbnail" style="padding-top:10px;"><i class="icon-shutter"></i></span>
                <ul class="sub-menu">
                  <li class="">
                    <a href="#">Administration</a>
                    <span class="icon-thumbnail">ad</span>
                  </li>
                  <li class="">
                    <a href="ViewAuditor.aspx">Manage Auditors</a>
                    <span class="icon-thumbnail">ma</span>
                  </li>
                  <li class="">
                    <a href="ViewCOCStatementAdmin">COC Log Statement</a>
                    <span class="icon-thumbnail">cs</span>
                  </li>
                  <li class="">
                    <a href="ViewRefixandAuditStatementAdmin.aspx">Manage Audits/Refixes</a>
                    <span class="icon-thumbnail">rs</span>
                  </li>
                    <li class="">
                    <a href="AddPlumberCertificate.aspx">Allocate COC to Plumbers</a>
                    <span class="icon-thumbnail">ap</span>
                  </li>
                  <li class="">
                    <a href="ViewSupplier.aspx">Reseller Management</a>
                    <span class="icon-thumbnail">rm</span>
                  </li>
                </ul>
            </li>

            <li class="" id="Li5" runat="server">
                <a href="javascript:;"><span class="title">Reports</span><span class="arrow"></span></a>
                <span class="icon-thumbnail" style="padding-top:10px;"><i class="icon-chart"></i></span>
                <ul class="sub-menu">
                  <li class="">
                    <a href="reportprofbody35">Pro Body 35</a>
                    <span class="icon-thumbnail">ps</span>
                  </li>
                  <li class="">
                    <a href="reportprofbody36">Pro Body 36</a>
                    <span class="icon-thumbnail">ps</span>
                  </li>


                  <li class="">
                    <a href="#">Register CPD</a>
                    <span class="icon-thumbnail">rc</span>
                  </li>
                  <li class="">
                    <a href="#">Performance Status</a>
                    <span class="icon-thumbnail">ps</span>
                  </li>
                    

                </ul>
            </li>

            <li class="" id="COCStatement" runat="server">
                <a href="javascript:;"><span class="title">COC Statement</span><span class="arrow"></span></a>
                <span class="icon-thumbnail" style="padding-top:10px;"><i class="icon-images3"></i></span>
                <ul class="sub-menu">
                  <li class="">
                    <a href="ViewCOCStatementAdminLink">COC Statement</a>
                    <span class="icon-thumbnail">cu</span>
                  </li>
                    

                </ul>
            </li>


            <li class="" id="Resellers" runat="server">
                <a href="javascript:;"><span class="title">Resellers</span><span class="arrow"></span></a>
                <span class="icon-thumbnail" style="padding-top:10px;"><i class="icon-person"></i></span>
                <ul class="sub-menu">
                  <li class="hide">
                    <a href="ViewSupplier.aspx">Reseller Management</a>
                    <span class="icon-thumbnail">as</span>
                  </li>
                  <li class="">
                    <a href="AddSupplierCertificate.aspx">Allocate Certificates</a>
                    <span class="icon-thumbnail">ac</span>
                  </li>

                </ul>
            </li>
            
            <li class="" id="Li6" runat="server">
                <a href="javascript:;"><span class="title">Gamification</span><span class="arrow"></span></a>
                <span class="icon-thumbnail" style="padding-top:10px;"><i class="icon-person"></i></span>
                <ul class="sub-menu">
                  <li class="">
                    <a href="Weighting.aspx">Weighting Settings</a>
                    <span class="icon-thumbnail">ws</span>
                  </li>
                  <li class="">
                    <a href="DesignationSpecialisationPoints.aspx">Designations/Specialisations</a>
                    <span class="icon-thumbnail">ds</span>
                  </li>
                  <li class="">
                    <a href="BadgesView.aspx">Badges</a>
                    <span class="icon-thumbnail">b</span>
                  </li>
                  <li class="">
                    <a href="CompanyPoints">Company</a>
                    <span class="icon-thumbnail">c</span>
                  </li>
                  <li class="">
                    <a href="GlobalSettings">Global Settings</a>
                    <span class="icon-thumbnail">gs</span>
                  </li>

                </ul>
            </li>
            
            <%--<li class="">
            <a href="ViewPlumberReviews">Plumber Renege</a>
            <span class="icon-thumbnail">ac</span>
            </li>--%>
            
            
            
             
             
    </ul>
    <div class="clearfix"></div>
</div>

<div class="sidebar-menu" id="inspectorMenu" runat="server">
    <ul class="menu-items">
         <li class="m-t-30">
            <a href="ViewCOCStatementInspector.aspx" class="detailed"><span class="title">Audit Statement</span></a>
            <span class="icon-thumbnail "><i class="pg-home"></i></span>
        </li>
        <li class="">
            <a href="InspectorOrders.aspx"><span class="title">Invoice</span></a> <!--//REQUIRED: NEEDS PAGE AND INVOICE PDF-->
            <span class="icon-thumbnail "><i class="pg-social"></i></span>
        </li>
        <li class="">
            <a href="InspectorComments.aspx"><span class="title">My Report Statements</span></a> 
            <span class="icon-thumbnail "><i class="pg-social"></i></span>
        </li>
        <li class="navigation-header"><span  style="margin-left:10px;color:#fff;">System Messages</span> <i class="icon-menu" title="" data-original-title="Messages"></i></li>
        <div id="dispMsgInspec" runat="server"></div>
    </ul>
    <div class="clearfix"></div>
    
</div>

<script>
    function markMsgReadsys(id, uid) {
        $.get("https://197.242.82.242/inspectit/API/markMsgRead?uid=" + uid + "&id=" + id, function (data) {
            $("#msg" + id).addClass("hide");
            $("#msgtop" + id).addClass("hide");
        });
    }
</script>

<%--<div class="sidebar-menu">
<ul class="menu-items">
    <li class="m-t-30">
        <a href="ViewCOCStatement.aspx" class="detailed"><span class="title">COC Statement</span></a>
        <span class="icon-thumbnail "><i class="pg-home"></i></span>
    </li>
    <li class="">
        <a href="PurchasePlumbingCOCs.aspx"><span class="title">Purchase COC</span></a>
        <span class="icon-thumbnail "><i class="pg-social"></i></span>
    </li>
    <li class="">
        <a href="ViewRefixandAuditStatement.aspx"><span class="title">COC Refix <span class="text-danger" id="countRefixes" runat="server"></span></span></a>
        <span class="icon-thumbnail "><i class="pg-social"></i></span>
    </li>
    <li class="">
        <a href="UserOrders.aspx"><span class="title">Orders</span></a> <!--//REQUIRED: NEEDS PAGE AND INVOICE PDF-->
        <span class="icon-thumbnail "><i class="pg-social"></i></span>
    </li>
   <!-- <li class="">
        <a href="RequestNewIDCard.aspx"><span class="title">New ID Card</span></a>
        <span class="icon-thumbnail "><i class="pg-social"></i></span>
    </li>-->
    <li class="">
    <a href="javascript:;">
        <span class="title">Administration</span>
        <span class=" arrow"></span>
    </a>
    <span class="icon-thumbnail"><i class="pg-grid"></i></span>
    <ul class="sub-menu">
        <li class="">
        <a href="ViewUser.aspx">Users</a>
        <span class="icon-thumbnail">sp</span>
        </li>
        <li class="">
        <a href="ViewAuditor.aspx">Auditors</a>
        <span class="icon-thumbnail">sp</span>
        </li>
        <li class="">
        <a href="ViewCustomer.aspx">Customers</a>
        <span class="icon-thumbnail">sp</span>
        </li>
        <li class="">
        <a href="ViewCompany.aspx">Companies</a>
        <span class="icon-thumbnail">sp</span>
        </li>
        <li class="">
        <a href="#">COC Statement</a>
        <span class="icon-thumbnail">sp</span>
        </li>
        <li class="">
        <a href="#">Refix Statement</a>
        <span class="icon-thumbnail">sp</span>
        </li>
        <li class="">
        <a href="#">Rates</a>
        <span class="icon-thumbnail">sp</span>
        </li>
        <li class="">
        <a href="#">Types</a>
        <span class="icon-thumbnail">sp</span>
        </li>
        <li class="">
        <a href="#">Forms</a>
        <span class="icon-thumbnail">sp</span>
        </li>
        <li class="">
        <a href="#">Transactions</a>
        <span class="icon-thumbnail">sp</span>
        </li>
        <li class="">
        <a href="ViewSupplier.aspx">Suppliers</a>
        <span class="icon-thumbnail">sp</span>
        </li>
    </ul>
    </li>



    <!-- INSPECTOR -->
     <li class="m-t-30">
        <a href="ViewCOCStatement.aspx" class="detailed"><span class="title">COC Statement</span></a>
        <span class="icon-thumbnail "><i class="pg-home"></i></span>
    </li>
    <li class="">
        <a href="InspectorInvoices.aspx"><span class="title">Invoices</span></a> <!--//REQUIRED: NEEDS PAGE AND INVOICE PDF-->
        <span class="icon-thumbnail "><i class="pg-social"></i></span>
    </li>






</ul>
<div class="clearfix"></div>
</div>--%>