<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBankMaster.ascx.cs"
    Inherits="Transaction_UserControl_ucBankMaster" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Panel ID="pnlMsg" runat="server" Visible="false">
    <asp:Label ID="lblMsg" runat="server" />
</asp:Panel>
<asp:Panel ID="pnlAddNew" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="trHeading" align="center">
                Bank Master
            </td>
        </tr>
        <tr>
            <td class="tdSpace">
                <br />
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <asp:Panel ID="pnlData" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Bank country key
                                <asp:Label ID="lableddlBankCountrykeyBNK" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <%--<act:ComboBox ID="ddlBankCountrykeyBNK" runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                                    AutoCompleteMode="Suggest" CaseSensitive="False" CssClass="AjaxToolkitStyle"
                                    OnSelectedIndexChanged="ddlBankCountrykeyBNK_SelectedIndexChanged">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </act:ComboBox>
                                <asp:RequiredFieldValidator ID="reqddlBankCountrykeyBNK" runat="server" ControlToValidate="ddlBankCountrykeyBNK"
                                    ValidationGroup="BankMaster" ErrorMessage="Bank country key cannot be blank."
                                    InitialValue="---Select---" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Bank country key cannot be blank.' />" />--%>
                                <asp:DropDownList ID="ddlBankCountrykeyBNK" runat="server" AutoPostBack="true" CaseSensitive="False"
                                    CssClass="AjaxToolkitStyle" OnSelectedIndexChanged="ddlBankCountrykeyBNK_SelectedIndexChanged">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlBankCountrykeyBNK" runat="server" ControlToValidate="ddlBankCountrykeyBNK"
                                    ValidationGroup="BankMaster" ErrorMessage="Bank country key cannot be blank."
                                    InitialValue="0" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Bank country key cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Bank Keys
                                <asp:Label ID="labletxtBankkeyBNK" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtBankKeyBNK" runat="server" CssClass="textbox" AutoPostBack="true"  MaxLength="11"
                                    OnTextChanged="txtBankKeyBNK_TextChanged" />
                                    <%-- MaxLength="60"--%>
                                <asp:RequiredFieldValidator ID="reqtxtBankKey" runat="server" ControlToValidate="txtBankKeyBNK"
                                    ValidationGroup="BankMaster" ErrorMessage="Bank Keys cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Bank Keys cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Region
                                <%--<asp:Label ID="lableddlRegionBNK" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <%--<act:ComboBox ID="ddlRegionBNK" runat="server" AutoPostBack="False" DropDownStyle="DropDownList"
                                    AutoCompleteMode="Suggest" CaseSensitive="False" CssClass="AjaxToolkitStyle">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </act:ComboBox>--%>
                                <asp:DropDownList ID="ddlRegionBNK" runat="server" AutoPostBack="False">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="reqddlRegionBNK" runat="server" ControlToValidate="ddlRegionBNK"
                                    ValidationGroup="BankMaster" ErrorMessage="Region cannot be blank." InitialValue="---Select---"
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Region cannot be blank.' />" />--%>
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Name of bank
                                <asp:Label ID="labletxtBankNameBNK" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtBankNameBNK" runat="server" CssClass="textbox" MaxLength="60" />
                                <asp:RequiredFieldValidator ID="reqtxtBankNameBNK" runat="server" ControlToValidate="txtBankNameBNK"
                                    ValidationGroup="BankMaster" ErrorMessage="Name of bank cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Name of bank cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Bank Branch
                                <asp:Label ID="labletxtBankBranchBNK" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtBankBranchBNK" runat="server" CssClass="textbox" Width="100px"
                                    MaxLength="40" />
                                <asp:RequiredFieldValidator ID="reqtxtBankBranchBNK" runat="server" ControlToValidate="txtBankBranchBNK"
                                    ValidationGroup="BankMaster" ErrorMessage="Partner Bank Type cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner Bank Type cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                House number and street
                                <%--<asp:Label ID="labletxtHouseNoStreetBNK" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtHouseNoStreetBNK" runat="server" CssClass="textbox" MaxLength="35" />
                                <%--<asp:RequiredFieldValidator ID="reqtxtHouseNoStreetBNK" runat="server" ControlToValidate="txtHouseNoStreetBNK"
                                    ValidationGroup="BankMaster" ErrorMessage="House number and street	be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='House number and street be blank.' />" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                City
                                <%--<asp:Label ID="labletxtCityBNK" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCityBNK" runat="server" CssClass="textbox" Width="100px" MaxLength="35" />
                                <%--<asp:RequiredFieldValidator ID="reqtxtCityBNK" runat="server" ControlToValidate="txtCityBNK"
                                    ValidationGroup="BankMaster" ErrorMessage="City cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='City cannot be blank.' />" />--%>
                            </td>
                            <td class="leftTD" style="width: 20%">
                                IFSC Code
                                <asp:Label ID="labletxtSwiftBNK" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtSwiftBNK" runat="server" CssClass="textbox" MaxLength="11" />
                                <asp:RequiredFieldValidator ID="reqtxtSwiftBNK" runat="server" ControlToValidate="txtSwiftBNK"
                                    ValidationGroup="BankMaster" ErrorMessage="IFSC Code cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='IFSC Code cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Bank number
                                <%--<asp:Label ID="labletxtBankNumberBNK" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtBankNumberBNK" runat="server" CssClass="textbox" MaxLength="15" />
                                <%--<asp:RequiredFieldValidator ID="reqtxtBankNo" runat="server" ControlToValidate="txtBankNumberBNK"
                                    ValidationGroup="BankMaster" ErrorMessage="Bank number cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Bank number cannot be blank.' />" />--%>
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Bank Group
                                <%--<asp:Label ID="labletxtBankGroupBNK" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtBankGroupBNK" runat="server" CssClass="textbox" Width="50px"
                                    MaxLength="2" />
                                <%--<asp:RequiredFieldValidator ID="reqtxtBankGroupBNK" runat="server" ControlToValidate="txtBankGroupBNK"
                                    ValidationGroup="BankMaster" ErrorMessage="Bank Group cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Bank Group cannot be blank.' />" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:ValidationSummary ID="smBank" runat="server" ValidationGroup="BankMaster" ShowMessageBox="true"
    ShowSummary="false" />
<asp:Label ID="lblUserId" runat="server" Visible="false" />
<asp:Label ID="lblBankId" runat="server" Visible="false" />
<asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
<asp:Label ID="lblRefMasterHeaderId" runat="server" Visible="false" />
<asp:Label ID="lblModuleId" runat="server" Visible="false" Text="60" />