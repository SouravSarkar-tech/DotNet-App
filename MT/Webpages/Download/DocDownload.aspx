<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/MainMaster.master"
    AutoEventWireup="true" CodeFile="DocDownload.aspx.cs" Inherits="Webpages_DocDownload" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <div style="width: 100%; text-align: center; color: Black; background: #FAB3A5; font-weight: bold;
            border: solid 1px red;">
            <asp:Label ID="lblMsg" runat="server" />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlContainer" runat="server" Visible="true">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td valign="top" class="trHeading" colspan="4">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="trHeading" align="center">
                                Download File
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <br />
                    <table border="0" cellpadding="2" cellspacing="1" width="50%">
                        <tr>
                            <td style="width: 10%" class="leftTD">
                                Type
                            </td>
                            <td colspan="3" class="rigthTD">
                                <asp:DropDownList ID="ddlModuleType" runat="server" ToolTip="Select Type">
                                    <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Vendor" Value="V"></asp:ListItem>
                                    <asp:ListItem Text="Customer" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="Material" Value="M"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%" class="leftTD">
                                Date
                            </td>
                            <td style="width: 30%" class="rigthTD">
                                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox" Width="100px" />
                                <ajax:CalendarExtender ID="caltxtDateFrom" runat="server" TargetControlID="txtDateFrom"
                                    Format="dd/MM/yyyy" />
                                <asp:RegularExpressionValidator ID="revDTTest" runat="server" ControlToValidate="txtDateFrom"
                                    ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                    Text="<img src='../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                    ValidationGroup="search" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="centerTD">
                                <asp:Button ID="btnDownload" runat="server" Text="DownLoad" CssClass="button" ValidationGroup="DownLoad"
                                    OnClick="btnDownload_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%-- <asp:UpdateProgress ID="updateProgressMaster" runat="server" DisplayAfter="5">
        <ProgressTemplate>
            <div id="light" class="loading">
                <img src="../images/ajax-loader-proccessing.gif" alt="Loading" height="30px" />
            </div>
            <div class="transparent_overlay">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
