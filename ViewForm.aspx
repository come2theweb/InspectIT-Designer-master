<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewForm.aspx.cs" Inherits="InspectIT.ViewFormNew" %>

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
                    
                    

                <div class="col-md-12" id="viewformdisplay" runat="server"></div>
                
                <div class="col-md-12 text-right">

                </div>

            </div>
        </div>
    </div>

    <script>

        // Read a page's GET URL variables and return them as an associative array.
        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        $('.datepicker-range').datepicker();
        
        var did = "<%=gDID%>";
        var cocid = "<%=gCOCID%>";
        var typeid = "<%=gTypeID%>";
        var userid = "<%=Session["IIT_UID"].ToString()%>";
        var frmid = "<%=gFormID%>";

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

                        var $el = $('[name="' + val.name + '"]'),
                            type = $el.attr('type');

                        //console.log(val.name, type, $el);
                        
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

                        console.log($el.attr('type'));

                    });

                    //$("#loaddiv").css("display", "none");

                }
            }).done(function () {
                $(".loadimg_api").each(function () {
                    loadIMG(this.id);
                });
            });
        }
        
        function loadIMG(fid) {
            $.post('https://197.242.82.242/inspectit/api/frmImgGet?FormID=' + frmid + '&FieldID=' + fid + '&TypeID=' + typeid + '&COCID=' + cocid + '&UserID=' + userid, {}, function (data) {
                $("#" + fid).html(data);
            });
        }

        function saveFRM() { // intercepts the submit event
            
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
                    document.location.href = "EditCOCStatement?cocid=" + getUrlVars()["cocid"];
                    console.log("Saved");
                }
           });

        }

        if (did != "0") {
            getFrmData();
        }
        

        function uploadFile(id) {
            var data = new FormData();
            var files = $("#uploadFileName" + id).get(0).files;
            if (files.length > 0) {
                data.append("UploadedImage", files[0]);
                data.append("UserID", userid);
                data.append("COCID", cocid);
                data.append("FieldID", id);
                data.append("FormID", did);
                
                var ajaxRequest = $.ajax({
                    type: "POST",
                    url: "/inspectit/api/frmImgSave",
                    contentType: false,
                    processData: false,
                    data: data
                });

                ajaxRequest.done(function (xhr, textStatus) {
                    $("#img_div_" + id).html(xhr);
                });
            }
            
        }

        function deleteImage(id) {
            if (confirm('Are you sure?')) {
                $.post('https://197.242.82.242/inspectit/api/frmImgDel?imgid=' + id, { }, function (data) {
                    $("#show_img_" + id).addClass("hide");
                })
            }
        }

    </script>




</asp:Content>
