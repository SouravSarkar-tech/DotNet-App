<%@ Page Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="UpdateSAP.aspx.cs" Inherits="Administration_UpdateSAP" %>


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
            <asp:Panel ID="pnlAddNew" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">Update Or Create SAP User ID & Password
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">SAP User ID
                                        <asp:Label ID="lbltxtSAPUserID" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtSAPUserID" runat="server" CssClass="textbox"
                                            MaxLength="20" Width="25%" />

                                    </td>
                                    <asp:RequiredFieldValidator ID="reqtxtSAPUserID" runat="server" ControlToValidate="txtSAPUserID"
                                        ValidationGroup="save" ErrorMessage="SAP User ID cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../images/Error.png' title='SAP User ID cannot be blank.' />" />
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Password
                                       <asp:Label ID="labletxtPassword" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox"
                                            MaxLength="50" Width="25%"  TextMode="Password"/>
                                        <asp:RequiredFieldValidator ID="reqtxtPassword" runat="server" ControlToValidate="txtPassword"
                                            ValidationGroup="save" ErrorMessage="Password cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Password cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Confirm Password
                                       <asp:Label ID="labletxtConfirmPassword" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="textbox"  TextMode="Password"
                                            MaxLength="50" Width="25%" />
                                        <asp:RequiredFieldValidator ID="reqtxtConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                                            ValidationGroup="save" ErrorMessage="Confirm Password cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Confirm Password cannot be blank.' />" />
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPassword"
                                            ControlToCompare="txtPassword" ErrorMessage="The passwords you entered do not match. Please try again."
                                            Display="None" SetFocusOnError="true" ValidationGroup="save" CssClass="form_msg"
                                            Text="<img src='../images/Error.png' title='The passwords you entered do not match. Please try again.' />" />


                                    </td>
                                </tr>


                                <tr>
                                    <td class="centerTD" colspan="2">
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button" ValidationGroup="save"
                                            OnClick="btnUpdate_Click" />
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
    <asp:Label ID="lblUserId" runat="server" Visible="false" />

     <asp:ValidationSummary ID="sm" runat="server" ShowMessageBox="true" ShowSummary="false"
        ValidationGroup="save" />
</asp:Content>
