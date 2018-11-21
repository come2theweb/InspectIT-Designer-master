<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zRunTimer.aspx.cs" Inherits="InspectIT.zRunTimer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
<script src="assets/js/jquery.min.js"></script>
    <script>
      
        function myTimer() {
            console.log(' each 1 second...');
            $.ajax({
                type: 'POST',
                url: "https://197.242.82.242/inspectit/zImportCOCs.aspx",
                traditional: true,
                success: function (data) {
                    console.log("added");
                }
            });
        }

        var myVar = setInterval(myTimer, 60000);
    </script>


</html>
