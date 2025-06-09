<%@ Page Language="C#" AutoEventWireup="true" CodeFile="homets.aspx.cs" Inherits="Shared_Common_homets" %>

<!DOCTYPE html>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    <link href="stylesheet/main.css" rel="stylesheet" type="text/css" />
    <link href="stylesheet/autocomplete.css" rel="stylesheet" type="text/css" />
    <%--<link href="stylesheet/Paging.css" rel="stylesheet" type="text/css" />--%>
    <script src="js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <%--<link href="stylesheet/TabStyle.css" rel="stylesheet" type="text/css" />--%>
    <script src="js/Common.js" type="text/javascript"></script> 
    <style type="text/css">
        .ui-dialog-titlebar-close {
            display: none;
        }
    </style>
</head>
<%--<body onload="ShowChangeDialog();">--%>
    <body>
    <form id="form1" runat="server">
        <div>
            <div id="outer_main">
                <ajax:ToolkitScriptManager ID="stmm" runat="server">
                </ajax:ToolkitScriptManager>

                <div id="divChangeModulePopUp" style="display: none;" title="Confirmation">
                    <asp:UpdatePanel ID="UpdChangePopup" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                                <asp:Label ID="lblMsg" runat="server" />
                            </asp:Panel>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">

                                <tr>
                                    <td class="leftTD" width="25%">Select Role
                            <asp:Label ID="lblDepartmentName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true">
                                            <%--<asp:ListItem Text="Select" Value="" />--%>
                                            <asp:ListItem Text="Requestor" Value="0" />
                                            <asp:ListItem Text="Manager" Value="28" />
                                            <asp:ListItem Text="Master Cell" Value="13" />
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="reqddlDepartment" runat="server" ControlToValidate="ddlDepartment"
                                            ValidationGroup="update" ErrorMessage="Please Select Role." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Please Select Role.' />" />
                                    </td>
                                </tr>

                               <%-- <tr>
                                    <td class="leftTD">Select Language
                            <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPlant" runat="server" TabIndex="6"> 
                                            <asp:ListItem Text="EN" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                            ValidationGroup="update" ErrorMessage="Language cannot be blank." SetFocusOnError="true"
                                            InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Language cannot be blank.' />" />
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" colspan="2">
                                        <span style="color: Orange; font-size: x-small">

                                            <i>*Please Ensure that you raise request only for those</i><br />
                                            <i>items which are not in the “NEW MASTER REQUEST” </i>
                                            <i>menu and Not applicable for you.</i>

                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="centerTD" colspan="2">
                                        <asp:Button ID="btnYES" runat="server" ValidationGroup="update" Text="OK"
                                            TabIndex="7" CssClass="button" OnClick="btnYES_Click" />
                                        <asp:Button ID="btnNO" runat="server" Text="CANCEL"
                                            TabIndex="7" CssClass="button" OnClick="btnNO_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="update" ShowMessageBox="true"
                        ShowSummary="false" />
                </div>

            </div>
        </div>
    </form>

    <script type="text/javascript" language="javascript">

        $(function () {

        });

        function ShowChangeDialog() {

            $("#divChangeModulePopUp").dialog({
                height: 200,
                width: 400,
                modal: true,
                closeOnEscape: false,
                draggable: true,
                resizable: false,
                position: 'center',
                dialogClass: 'alert',
                //show: { effect: 'drop', duration: 500 },
                //hide: { effect: 'explode', duration: 100 },
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                    $(this).show('clip');
                }
            });
        }

    </script>
</body>
</html>
