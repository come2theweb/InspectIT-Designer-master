<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewFormInspector.aspx.cs" Inherits="InspectIT.ViewFormInspector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
    <script type="text/javascript" src="assets/plugins/datatables-responsive/js/lodash.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid container-fixed-lg bg-white">
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div class="panel-title">
                    COC Statement
                </div>
                <div class="pull-right alert alert-info">
                    <div id="COCNumber" runat="server"></div>
                    <div id="COCType" runat="server"></div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="panel-body">

                <div class="alert alert-success" id="successmsg" runat="server" visible="false"></div>
                <div class="alert alert-danger" id="errormsg" runat="server" visible="false"></div>

                <div class="row">
                    
                    <div class="col-md-12 hide" id="DisplayRefixNotice" runat="server" visible="false">
                        <div class="panel panel-default" data-pages="portlet">
                          <div class="panel-heading bg-danger">
                            <div class="panel-title">Refix Notice</div>
                          </div>
                          <div class="panel-body">
                            

                              <div class="panel panel-default bg-danger" data-pages="portlet">
                              <div class="panel-heading">
                                <div class="panel-title">Auditors Details</div>
                              </div>
                              <div class="panel-body">

                                <div class="form-group form-group-default">
                                    <label>Full Name: </label>
                                    <div class="controls">
                                        newuser
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Email Address: </label>
                                    <div class="controls">
                                        new@gmail.com
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Comments: </label>
                                    <div class="controls">
                                        The installation was incorrectly done, kljhgljkd fkjhf gdfkjh gsdfjhsdfksdjfh gskdf skhj ksdf hj
                                    </div>
                                </div>

                                <div class="form-group form-group-default">
                                    <label>Images: </label>
                                    <div class="controls">
                                        <div class="row">
                                            <div class="col-md-6"><img src="assets/img/blog1.jpg" /></div>
                                            <div class="col-md-6"><img src="assets/img/blog1.jpg" /></div>
                                        </div>
                                    </div>
                                </div>


                            </div>
                        
                          </div>
                        </div>
                    </div>

                    


                </div>

                <div class="col-md-12" id="viewformdisplay" runat="server"></div>
                
                <div class="col-md-12 text-right">

                </div>

            </div>
        </div>
    </div>

    <script>
        
        $('.datepicker-range').datepicker();
        
        var did = "<%=gDID%>";
        var cocid = "<%=gCOCID%>";
        var typeid = "<%=gTypeID%>";

        $.fn.serializeObject = function () {
            var o = {};
            var a = this.serializeArray();
            $.each(a, function () {
                if (o[this.name]) {
                    if (!o[this.name].push) {
                        o[this.name] = [o[this.name]];
                    }
                    o[this.name].push(this.value || '');
                } else {
                    o[this.name] = this.value || '';
                }
            });
            return o;
        };

        function getFrmData() {
            // LOAD VALUES SELECTED IF EXISTS
            $.ajax({
                type: 'GET',
                url: 'https://197.242.82.242/inspectit/api/getFormData',
                data: 'did=' + did,
                dataType: "json",
                success: function (data) {

                    $.each(data, function (name, val) {

                        console.log(val.name);

                        var $el = $('[name="' + val.name + '"]'),
                            type = $el.attr('type');

                        switch (type) {
                            case 'checkbox':
                                $el.prop('checked', true); //.checkboxradio('refresh');
                                $el.attr('checked', 'checked');
                                break;
                            case 'radio':
                                $el.filter('[value="' + val.value + '"]').attr('checked', 'checked');
                                $el.filter('[value="' + val.value + '"]').prop('checked', true);//.checkboxradio('refresh');
                                break;
                            case 'textarea':
                                $el.val(val.value);
                                break;
                            default:
                                $el.val(val.value);
                        }

                    });

                    //$("#loaddiv").css("display", "none");

                }
            });
        }
        
        function saveFRM() { // intercepts the submit event
            //$("#loaddiv").addClass("show");
            //console.log("ford details: " + $("#viewformdisplay").serializeObject());
            var jData = $("#<%=viewformdisplay.ClientID%>").find("select, textarea, input").serializeArray();
            jData = JSON.stringify(jData);
            $.post('https://197.242.82.242/inspectit/api/srv_frmSave?did=' + did + '&typeid=' + typeid + '&cocid=' + cocid + '&json=' + jData, {}, function (data) {
                $("#resultbox").append(data);
            }).done(function () {
                var gData = $("#resultbox").html();
                if (gData == 'Error') {
                    $("#resultbox").show();
                    $("#resultbox").html('Please select something.')
                } else {
                    //document.location.href = "sitesforms.html?sid=" + sid;
                    console.log("Saved");
                }

            });
        }

        if (did != "0") {
            getFrmData();
        }
        

    </script>

<div class="modal fade fill-in" id="modalRefixInspector" tabindex="-1" role="dialog" aria-hidden="true">
<button type="button" class="close" data-dismiss="modal" aria-hidden="true">
<i class="pg-close"></i>
</button>
<div class="modal-dialog">
<div class="modal-content">
    <div class="modal-header">
    <h5 class="text-left p-b-5"><span class="semi-bold">Refix</span> Notice</h5>
    </div>
    <div class="modal-body bg-white" style="overflow-y:scroll;padding:10px;">
            
        <div class="form-group form-group-default">
            <div class="controls">
                <h5>Inspector Report</h5>
                
                <div class="form-group form-group-default">
                    <label>Audit Status: </label>
                    <div class="controls">
                        <asp:RadioButton ID="chkFailure" runat="server" GroupName="InspectionStatus" CssClass="radio radio-danger" Text="Failure" Checked="true" />
                        <asp:RadioButton ID="chkCautionary" runat="server" GroupName="InspectionStatus" CssClass="radio radio-warning" Text="Cautionary" />
                        <asp:RadioButton ID="chkComplement" runat="server" GroupName="InspectionStatus" CssClass="radio radio-success" Text="Complement" />
                    </div>
                </div>

                <div class="form-group form-group-default">
                    <label>Templated Comments</label>
                    <asp:DropDownList ID="TemplateSelection" CssClass="form-control" runat="server">

                    </asp:DropDownList>
                </div>

                <div class="form-group form-group-default">
                    <label>Comments</label>
                    <asp:TextBox ID="RefixComments" runat="server" TextMode="MultiLine" CssClass="form-control" Height="120" required></asp:TextBox>
                </div>

                <div class="form-group form-group-default">
                    <label>Image</label>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </div>
                                                
                <div class="form-group form-group-default text-right">
                    <div class="controls">
                        <div style="display:none;"><asp:TextBox ID="FieldID" runat="server"></asp:TextBox></div>
                        <asp:Button ID="savenotice" CssClass="btn btn-primary" runat="server" Text="Save Notice" OnClick="savenotice_Click"  />
                    </div>
                </div>
            </div>
        </div>

    </div>
</div>
<!-- /.modal-content -->
</div>
<!-- /.modal-dialog -->
</div>

        <script>

            $("#<%=TemplateSelection.ClientID%>").change(function () {
                $("#<%=RefixComments.ClientID%>").val($("#<%=TemplateSelection.ClientID%>").val());
            });


            function insertFieldID(fieldname) {
                console.log(fieldname);
                $("#<%=FieldID.ClientID%>").val(fieldname);
            }

        </script>

    </div>
</asp:Content>
