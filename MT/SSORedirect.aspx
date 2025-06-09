<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SSORedirect.aspx.cs" Inherits="SSORedirect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>:: MWT ::</title>
    <link href="stylesheet/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="outer_main">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="52">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="52">
                                <a href="#">
                                    <img src="images/Lupin.jpeg" alt="Lupin Logo" border="0" style="height: 36px; width: 37px" /></a>
                            </td>
                            <td width="262" valign="bottom" align="right">
                            &nbsp;
                                <%--<img src="images/accenture_logo.gif" alt="Accenture Logo" border="0" style="margin-bottom: 15px;" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="menu" class="menu" width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" valign="middle">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="5">
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <table border="0" cellspacing="0" cellpadding="0" width="99%">
                        <tr>
                            <td height="30" colspan="2" align="left" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left" valign="top">
                                <table width="99%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="537" valign="top" style="border: 1px #e4e3e3 solid">
                                            <img src="images/login_img1.jpg" alt="" width="556" height="284" />
                                        </td>
                                        <td width="26">
                                            &nbsp;
                                        </td>
                                        <td width="417" style="border: 1px #e4e3e3 solid">
                                            <div>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                                    <tr>
                                                        <td height="24" colspan="2" class="txt_normal txt_bold" style="border-bottom: 1px #e4e3e3 solid">
                                                            &nbsp; Login
                                                        </td>
                                                        <td align="right" valign="top" style="border-bottom: 1px #e4e3e3 solid">
                                                            <img src="images/lock_admin.jpg" alt="" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" class="mainclass666666small">
                                                            &nbsp;
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="35%" align="right" class="txt_normal">
                                                            User Name
                                                        </td>
                                                        <td width="40%" align="right">
                                                            <asp:TextBox ID="txtUserName" CssClass="textboxSimple" runat="server" size="30"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                                                                ErrorMessage="User Name is required." ToolTip="User Name is required." SetFocusOnError="true"
                                                                ValidationGroup="ctlLogin" Display="None"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" class="txt_normal">
                                                            Password
                                                        </td>
                                                        <td align="right">
                                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="textboxSimple" TextMode="Password"
                                                                size="30"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                                                Display="None" ErrorMessage="Password is required." ToolTip="Password is required."
                                                                ValidationGroup="ctlLogin" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imgBtnLogin" TabIndex="3" runat="server" ImageUrl="~/images/go-btn.jpg"
                                                                ValidationGroup="ctlLogin" CommandName="Login" OnClick="imgBtnLogin_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="40" align="right" valign="bottom" colspan="3">
                                                            <asp:CheckBox ID="chkRememberMe" Text="Use browser cookies to save login information."
                                                                runat="server" Style="font-size: 11px; font-family: Verdana" />
                                                            &nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="40" align="center" valign="bottom">
                                                            <%--<a href="Forgot_pwd.aspx" class="txt_normal">Forgot password?</a>--%>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="40" valign="bottom">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                                <span style="color: Red;">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                </span>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="150" colspan="2" align="left" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="1" colspan="2" align="left" valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="9" align="left" valign="top">
                                            <img src="images/btm_left.jpg" alt="" border="0" />
                                        </td>
                                        <td align="right" valign="middle" background="images/btm_middle_bg.jpg" class="txt_normal copyright_txt_normal">
                                            <%--Copyright @ 2013, All rights reserved by Accenture.--%>
                                        </td>
                                        <td width="9" align="right" valign="top">
                                            <img src="images/btm_right.jpg" alt="" border="0" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="712" height="1" align="left" valign="top">
                                &nbsp;
                            </td>
                            <td align="center" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="ctlLogin" HeaderText="There are some problem with the form fields"
        runat="server" />
    </form>
</body>
</html>
