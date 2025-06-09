<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="ApproveFromBackend.aspx.cs" Inherits="Administration_ApproveFromBackend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upMsg" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlAddNew" runat="server" Visible="false">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">Approve Request from Backend
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Enter Request No.
                                        <asp:Label ID="lblRequestNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtRequestNo" runat="server" CssClass="textbox"
                                            MaxLength="20" Width="25%" />
                                        &nbsp; &nbsp;
                                        <asp:Button ID="btnLoadDept" runat="server" Text="Load Departments" CssClass="button" ValidationGroup="check"
                                            OnClick="btnLoadDept_Click" />
                                    </td>
                                    <asp:RequiredFieldValidator ID="reqtxtRequestNo" runat="server" ControlToValidate="txtRequestNo"
                                        ValidationGroup="save" ErrorMessage="Request No cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../images/Error.png' title='Request No cannot be blank.' />" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRequestNo"
                                        ValidationGroup="check" ErrorMessage="Request No cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../images/Error.png' title='Request No cannot be blank.' />" />
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Enter Department
                                        <asp:Label ID="lblDepartment" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="false">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="reqddlDepartment" runat="server" ControlToValidate="ddlDepartment"
                                            ValidationGroup="save" ErrorMessage="Department Name cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Department Name cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Approve By 
                                        <asp:Label ID="lblApproveBy" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlApproveBy" runat="server"
                                            AppendDataBoundItems="true" AutoPostBack="true">
                                            <asp:ListItem Text="Select" Value="" />
                                            <asp:ListItem Text="User" Value="User" />
                                            <asp:ListItem Text="Admin" Value="Admin" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlApproveBy" runat="server" ControlToValidate="ddlApproveBy"
                                            ValidationGroup="save" ErrorMessage="Please select Approve By." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Approve By cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="centerTD" colspan="2">
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="button" ValidationGroup="save"
                                            OnClick="btnApprove_Click" />
                                        &nbsp;
                                        <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="button"
                                            OnClick="BtnClear_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </asp:Panel>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

