<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="js/jquery.min.js" type="text/javascript"></script>
<%--    <script src="js/jquery.xdomainrequest.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        function AuthenticationCall() {
            //alert('hi');

            jQuery.support.cors = true;
            var urln = 'http://myworkflow.lupinworld.com:8080/Authenticate/authenticate';
            //var urln = 'http://myworkflow.lupin.com:8080/Authenticate/authenticate';
            var tkt = 'AjExMDAgABhwb3J0YWw6cHJhc2FkY2hlbmR3YW5rYXKIABNiYXNpY2F1dGhlbnRpY2F0aW9uAQAAAgADMDAwAwADRVBEBAAMMjAxMzEwMjEwOTQ4BQAEAAAACAoAAP8BTzCCAUsGCSqGSIb3DQEHAqCCATwwggE4AgEBMQswCQYFKw4DAhoFADALBgkqhkiG9w0BBwExggEXMIIBEwIBATBoMF8xCzAJBgNVBAYTAklOMRQwEgYDVQQIEwtNYWhhcmFzaHRyYTEPMA0GA1UEBxMGTXVtYmFpMQ4wDAYDVQQKEwVMdXBpbjELMAkGA1UECxMCSE8xDDAKBgNVBAMTA0VQRAIFAM!ngr0wCQYFKw4DAhoFAKBdMBgGCSqGSIb3DQEJAzELBgkqhkiG9w0BBwEwHAYJKoZIhvcNAQkFMQ8XDTEzMTAyMTA5NDgyOVowIwYJKoZIhvcNAQkEMRYEFNMEZJ50lSpKHxnhewg2K6A%2F9Ad5MAkGByqGSM44BAMELzAtAhUAi%2FVvu!u9SvxSOFXFcnJUWML8rYgCFCFoQI1lHInZlU!w9h2UXEM6eVjB';

//            if ($.browser.msie && parseInt($.browser.version, 10) >= 8 && window.XDomainRequest) {
//                // Use Microsoft XDR
//                alert('xdr');
//                var xdr = new XDomainRequest();
//                xdr.open("get", urln + '?ticket=' + tkt);
//                xdr.onload = function () {
//                    alert('xdr');
//                    alert(xdr.responseText)

//                    for (var i in data) {
//                        alert(i + ':' + data[i]);
//                    }
//                    //alert(data[1].toString());
//                    //$('#txtUser').val(data['portalUsr'].toString());
//                    //$('#txtError').val(data['stdError'].toString());
//                };
//                xdr.error = function () {
//                    alert('xdr-error');
//                    alert(xdr.responseText)

//                    
//                };
//                xdr.send();
//            }
//            else {




                $.ajax({


                    //url: 'http://l2jqas.lupinworld.com:8080/Authenticate/authenticate',
                    url: urln,

                    //data: '{ "ticket": "' + $('#<%= txtticket.ClientID%>').val().toString() + '"}',
                    cache: false,
                    dataType: 'json',
                    type: 'GET',
                    contentType: "application/json;",
                    crossDomain: true,

                    //data: { ticket: $('#<%= txtticket.ClientID%>').val().toString() },
                    data: { ticket: tkt },
                    success: function (data) {

                        for (var i in data) {
                            alert(i + ':' + data[i]);
                        }
                        //alert(data[1].toString());
                        $('#<%= txtUser.ClientID%>').val(data['portalUsr'].toString());
                        $('#<%= txtError.ClientID%>').val(data['stdError'].toString());
                    },
                    error: function (error) {
                        for (var i in error) {
                            alert('Error:' + error[i]);
                        }
                        //error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //alert(errorThrown + '::' + textStatus + '::' + XMLHttpRequest);
                    }
                });
           // }
        }
    </script>
    <title></title>
</head>
<body onload="AuthenticationCall()">
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtticket" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtError" runat="server"></asp:TextBox>
        <asp:Button ID="btnClick" runat="server" OnClientClick="AuthenticationCall();" />
    </div>
    </form>
</body>
</html>
