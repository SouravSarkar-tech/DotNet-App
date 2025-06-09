<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAPLogin.aspx.cs" Inherits="Transaction_BOMRecipe_SAPLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <link href="../../stylesheet/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <asp:UpdatePanel ID="UpdatePnlQA" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">
                            User
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtUserName" CssClass="textboxSimple" runat="server" size="30"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                                ErrorMessage="User Name is required." ToolTip="User Name is required." SetFocusOnError="true"
                                ValidationGroup="ctlLogin" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">
                            Password
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="textboxSimple" TextMode="Password"
                                size="30"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                Display="None" ErrorMessage="Password is required." ToolTip="Password is required."
                                ValidationGroup="ctlLogin" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="centerTD">
                    <asp:Button ID="btnDone" runat="server" ValidationGroup="ctlLogin" Text="Login"
                        CssClass="button" OnClick="btnDone_Click"/>                    
                </td>
                <td class="centerTD">                   
                    <asp:HyperLink ID="hyper" runat="server" CssClass="button" Enabled = "false"></asp:HyperLink>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        $("a.button").one("click", function () {
            $(this).click(function () { return false; });
        });
    </script>
</body>
</html>
