<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Vendor/VendorMasterPage.master"
    AutoEventWireup="true" CodeFile="CompanyCodeData2.aspx.cs" Inherits="Transaction_Vendor_CompanyCodeData2" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Src="~/Transaction/UserControl/ucBankMaster.ascx" TagPrefix="uc" TagName="ucBankMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetToolTip(control) {
            ddrivetip('31', control);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdWHT" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlAddNew" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Company Code Data 2
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <asp:Panel ID="pnlData" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="leftTD" style="width: 20%">									
										<%--Start Change by Swati on 03.01.2019--%>
                                            Bank Details Required or not?
                                            <asp:Label ID="labelddlBankDetailsReq" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlBankDetailsReq" runat="server" AutoPostBack="true" TabIndex="3" OnSelectedIndexChanged="ddlBankDetailsReq_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlBankDetailsReq" runat="server" ControlToValidate="ddlBankDetailsReq"
                                                ValidationGroup="CompanyCode" ErrorMessage="Please select if Bank Details are required."
                                                 
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please select if Bank Details are required.' />" />
                                            <%--InitialValue="0"--%>
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Reasons for non-maintaining Bank Details 
                                            <asp:Label ID="labeltxtReasonNonBankDet" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtReasonNonBankDet" runat="server" CssClass="textbox" MaxLength="60" TabIndex="4" />
                                            <asp:RequiredFieldValidator ID="reqtxtReasonNonBankDet" runat="server" ControlToValidate="txtReasonNonBankDet"
                                                ValidationGroup="CompanyCode" ErrorMessage="Reason for not maintaining Bank Details cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Reason for not maintaining Bank Details cannot be blank.' />" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class ="tdspace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
										<%--End Change--%>
                                            Bank country key
                                            <asp:Label ID="lableddlBankCountrykey" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <%--OnSelectedIndexChanged="ddlBankCountrykey_SelectedIndexChanged"--%>
                                            <asp:DropDownList ID="ddlBankCountrykey" runat="server" AutoPostBack="false" TabIndex="1"
                                                Width="200px">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlBankCountrykey" runat="server" ControlToValidate="ddlBankCountrykey"
                                                ValidationGroup="CompanyCode" ErrorMessage="Bank country key cannot be blank."
                                                InitialValue="0" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Bank country key cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            IFSC Code
                                            <asp:Label ID="labletxtBankKey" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <asp:LinkButton ID="lnkAddBank" runat="server" Font-Bold="false" Text="(Add New Bank)"
                                                OnClick="lnkAddBank_Click" Visible="false"></asp:LinkButton>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtBankKey" runat="server" CssClass="textboxAutocomplete" Width="180px"
                                                AutoPostBack="true" TabIndex="1" MaxLength="11" onfocus="return BankKeyOnFocus();"
                                                onblur="return BankKeyTextChangeEvent();" OnTextChanged="txtBankKey_TextChanged" />
                                            <asp:RequiredFieldValidator ID="reqtxtBankKey" runat="server" ControlToValidate="txtBankKey"
                                                ValidationGroup="CompanyCode" ErrorMessage="Bank Keys cannot be blank." SetFocusOnError="true"
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
                                            <asp:Label ID="lableddlRegion" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="False" TabIndex="3">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlRegion" runat="server" ControlToValidate="ddlRegion"
                                                ValidationGroup="CompanyCode" ErrorMessage="Region cannot be blank." InitialValue="0"
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Region cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Name of bank
                                            <asp:Label ID="labletxtBankName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox" MaxLength="60" TabIndex="4" />
                                            <asp:RequiredFieldValidator ID="reqtxtBankName" runat="server" ControlToValidate="txtBankName"
                                                ValidationGroup="CompanyCode" ErrorMessage="Name of bank cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Name of bank cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                            <asp:Panel ID="pnlNewBank" runat="server" Visible="false" BorderWidth="2px" BorderColor="Black"
                                                BorderStyle="Solid">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:UpdatePanel ID="updBankNew" runat="server">
                                                                <ContentTemplate>
                                                                    <uc:ucBankMaster ID="ucBankMaster1" runat="server" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr id="trBank" runat="server" visible="false">
                                                        <td class="centerTD">
                                                            <asp:Button ID="BtnBankSave" runat="server" ValidationGroup="BankMaster" Text="Save"
                                                                CssClass="button" OnClick="BtnBankSave_Click" />
                                                            <asp:Button ID="btnBankCancel" runat="server" CausesValidation="false" Text="Cancel"
                                                                CssClass="button" OnClick="btnBankCancel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            House number and street
                                            <asp:Label ID="labletxtHouseNoStreet" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtHouseNoStreet" runat="server" CssClass="textbox" MaxLength="35"
                                                TabIndex="5" />
                                            <asp:RequiredFieldValidator ID="reqtxtHouseNoStreet" runat="server" ControlToValidate="txtHouseNoStreet"
                                                ValidationGroup="CompanyCode" ErrorMessage="House number and street	be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='House number and street be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Bank number
                                            <asp:Label ID="labletxtBankNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtBankNo" runat="server" CssClass="textbox" MaxLength="15" TabIndex="6" />
                                            <asp:RequiredFieldValidator ID="reqtxtBankNo" runat="server" ControlToValidate="txtBankNo"
                                                ValidationGroup="CompanyCode" ErrorMessage="Bank number cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Bank number cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Account Holder Name
                                            <asp:Label ID="labletxtAccountHolderName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtAccountHolderName" runat="server" CssClass="textbox" MaxLength="60"
                                                TabIndex="7" />
                                            <asp:RequiredFieldValidator ID="reqtxtAccountHolderName" runat="server" ControlToValidate="txtAccountHolderName"
                                                ValidationGroup="CompanyCode" ErrorMessage="Account Holder Name cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Account Holder Name cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Bank account number
                                            <asp:Label ID="labletxtBankAccNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtBankAccNo" runat="server" CssClass="textbox" MaxLength="18" TabIndex="8" />
                                            <asp:RequiredFieldValidator ID="reqtxtBankAccNo" runat="server" ControlToValidate="txtBankAccNo"
                                                ValidationGroup="CompanyCode" ErrorMessage="Bank account number cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Bank account number cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Bank Control Key
                                            <asp:Label ID="labletxtBankControlKey" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtBankControlKey')"
                                                onmouseout="hideddrivetip()" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtBankControlKey" runat="server" CssClass="textbox" Width="25px"
                                                TabIndex="9" MaxLength="2" />
                                            <asp:RequiredFieldValidator ID="reqtxtBankControlKey" runat="server" ControlToValidate="txtBankControlKey"
                                                ValidationGroup="CompanyCode" ErrorMessage="Bank Control Key cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Bank Control Key cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Account Number of the Alternative Payee
                                            <asp:Label ID="labletxtAccount_Number_Alternative_Payee" runat="server" ForeColor="Red"
                                                Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtAccount_Number_Alternative_Payee')"
                                                onmouseout="hideddrivetip()" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtAccount_Number_Alternative_Payee" runat="server" CssClass="textbox"
                                                TabIndex="10" MaxLength="10" Width="180" />
                                            <asp:RequiredFieldValidator ID="reqtxtAccount_Number_Alternative_Payee" runat="server"
                                                ControlToValidate="txtAccount_Number_Alternative_Payee" ValidationGroup="save"
                                                ErrorMessage=" Account Number of the Alternative Payee cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title=' Name 1 cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            IBAN (International Bank Account Number)
                                            <asp:Label ID="labletxtInterNBankAccNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtInterNBankAccNo" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="11" MaxLength="34" />
                                            <asp:RequiredFieldValidator ID="reqtxtInterNBankAccNo" runat="server" ControlToValidate="txtInterNBankAccNo"
                                                ValidationGroup="CompanyCode" ErrorMessage="IBAN (International Bank Account Number) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='IBAN (International Bank Account Number) cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Partner Bank Type
                                            <asp:Label ID="labletxtPartnerBankType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtPartnerBankType" runat="server" CssClass="textboxAutocomplete"
                                                TabIndex="12" Width="50px" MaxLength="4" onfocus="return PartnerBankTypeOnFocus();"
                                                onchange="return PartnerBankTypeTextChangeEvent();" />
                                            <asp:RequiredFieldValidator ID="reqtxtPartnerBankType" runat="server" ControlToValidate="txtPartnerBankType"
                                                ValidationGroup="CompanyCode" ErrorMessage="Partner Bank Type cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner Bank Type cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Subject to withholding tax?
                                            <asp:Label ID="lablerdlIsSubjectWHT" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <%--<asp:CheckBox ID="chkIsSubjectWHT" runat="server" Text="Check if Relevant" SkinID="Disable"
                                        TabIndex="13" />--%>
                                            <asp:RadioButtonList runat="server" ID="rdlIsSubjectWHT" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="reqrdlIsSubjectWHT" runat="server" ControlToValidate="rdlIsSubjectWHT"
                                                ValidationGroup="CompanyCode" ErrorMessage="Subject to withholding tax cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Subject to withholding tax cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Withholding Tax Country Key
                                            <asp:Label ID="lableddlWHTCountryKey" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <act:ComboBox ID="ddlWHTCountryKey" runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                                                TabIndex="14" ValidationGroup="CompanyCode" AutoCompleteMode="Suggest" CaseSensitive="False"
                                                CssClass="AjaxToolkitStyle" OnSelectedIndexChanged="ddlWHTCountryKey_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </act:ComboBox>
                                            <asp:RequiredFieldValidator ID="reqddlWHTCountryKey" runat="server" ControlToValidate="ddlWHTCountryKey"
                                                ValidationGroup="CompanyCode" ErrorMessage="Withholding Tax Country Key cannot be blank."
                                                InitialValue="---Select---" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Withholding Tax Country Key cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table width="100%">
                                                <tr>
                                                    <td class="leftTD" style="width: 15%">
                                                        WHT type<asp:Label ID="lableddlIndicatorWHTType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    </td>
                                                    <td class="leftTD" style="width: 15%">
                                                        WHT Code
                                                        <asp:Label ID="lableddlWithHoldingTaxCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    </td>
                                                    <td class="leftTD" style="width: 15%">
                                                        Type of recipient
                                                        <asp:Label ID="lableddlTypeRecipient" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    </td>
                                                    <td class="leftTD" style="width: 15%">
                                                        Certificate Number
                                                        <asp:Label ID="labletxtWHTExemptCertNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    </td>
                                                    <td class="leftTD" style="width: 15%">
                                                        Exemption rate
                                                        <asp:Label ID="labletxtExemptionRateBatchInp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    </td>
                                                    <td class="leftTD" style="width: 12%">
                                                        WHT Exempt From Date
                                                        <asp:Label ID="labletxtWTExemptFromDate" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    </td>
                                                    <td class="leftTD" style="width: 12%">
                                                        WHT Exempt To Date
                                                        <asp:Label ID="labletxtWTExemptToDate" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlIndicatorWHTType" runat="server" AutoPostBack="true" TabIndex="15"
                                                            Width="100%" OnSelectedIndexChanged="ddlIndicatorWHTType_SelectedIndexChanged">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlIndicatorWHTType" runat="server" ControlToValidate="ddlIndicatorWHTType"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT type cannot be blank." SetFocusOnError="true" InitialValue="---Select---"
                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT type cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlWithHoldingTaxCode" runat="server" Width="100%" TabIndex="16">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlWithHoldingTaxCode" runat="server" ControlToValidate="ddlWithHoldingTaxCode"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Code cannot be blank." SetFocusOnError="true" InitialValue="---Select---"
                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Code cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlTypeRecipient" runat="server" TabIndex="17" Width="100%">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlTypeRecipient" runat="server" ControlToValidate="ddlTypeRecipient"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Type of recipient cannot be blank." InitialValue="---Select---"
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Type of recipient cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWHTExemptCertNo" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="18" MaxLength="10" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWHTExemptCertNo" runat="server" ControlToValidate="txtWHTExemptCertNo"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Certificate Number cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Certificate Number cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtExemptionRateBatchInp" runat="server" CssClass="textbox" Width="55px"
                                                            TabIndex="19" MaxLength="5" />
                                                        <asp:RequiredFieldValidator ID="reqtxtExemptionRateBatchInp" runat="server" ControlToValidate="txtExemptionRateBatchInp"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Exemption rate cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Exemption rate cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWTExemptFromDate" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="20" MaxLength="10" />
                                                        <act:CalendarExtender ID="CaltxtWTExemptFromDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtWTExemptFromDate" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWTExemptFromDate" runat="server" ControlToValidate="txtWTExemptFromDate"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Exempt From Date cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Exempt From Date cannot be blank.' />" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtWTExemptFromDate"
                                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                            ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWTExemptToDate" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="21" MaxLength="10" />
                                                        <act:CalendarExtender ID="CaltxtWTExemptToDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtWTExemptToDate" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWTExemptToDate" runat="server" ControlToValidate="txtWTExemptToDate"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Exempt To Date cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Exempt To Date cannot be blank.' />" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtWTExemptToDate"
                                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                            ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlIndicatorWHTType2" runat="server" AutoPostBack="true" TabIndex="22"
                                                            Width="100%" OnSelectedIndexChanged="ddlIndicatorWHTType2_SelectedIndexChanged">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlIndicatorWHTType2" runat="server" ControlToValidate="ddlIndicatorWHTType2"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT type cannot be blank." SetFocusOnError="true" InitialValue="---Select---"
                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT type cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlWithHoldingTaxCode2" runat="server" AutoPostBack="False"
                                                            Width="100%" TabIndex="23">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlWithHoldingTaxCode2" runat="server" ControlToValidate="ddlWithHoldingTaxCode2"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Code cannot be blank." SetFocusOnError="true" InitialValue="---Select---"
                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Code cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlTypeRecipient2" runat="server" AutoPostBack="False" TabIndex="24"
                                                            Width="100%">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlTypeRecipient2" runat="server" ControlToValidate="ddlTypeRecipient2"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Type of recipient cannot be blank." InitialValue="---Select---"
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Type of recipient cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWHTExemptCertNo2" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="25" MaxLength="10" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWHTExemptCertNo2" runat="server" ControlToValidate="txtWHTExemptCertNo2"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Certificate Number cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Certificate Number cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtExemptionRateBatchInp2" runat="server" CssClass="textbox" Width="55px"
                                                            TabIndex="26" MaxLength="5" />
                                                        <asp:RequiredFieldValidator ID="reqtxtExemptionRateBatchInp2" runat="server" ControlToValidate="txtExemptionRateBatchInp2"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Exemption rate cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Exemption rate cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWTExemptFromDate2" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="27" MaxLength="10" />
                                                        <act:CalendarExtender ID="CaltxtWTExemptFromDate2" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtWTExemptFromDate2" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWTExemptFromDate2" runat="server" ControlToValidate="txtWTExemptFromDate2"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Exempt From Date cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Exempt From Date cannot be blank.' />" />
                                                        <asp:RegularExpressionValidator ID="RegtxtWTExemptFromDate2" runat="server" ControlToValidate="txtWTExemptFromDate2"
                                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                            ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWTExemptToDate2" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="28" MaxLength="10" />
                                                        <act:CalendarExtender ID="CaltxtWTExemptToDate2" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtWTExemptToDate2" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWTExemptToDate2" runat="server" ControlToValidate="txtWTExemptToDate2"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Exempt To Date cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Exempt To Date cannot be blank.' />" />
                                                        <asp:RegularExpressionValidator ID="RegtxtWTExemptToDate2" runat="server" ControlToValidate="txtWTExemptToDate2"
                                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                            ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlIndicatorWHTType3" runat="server" AutoPostBack="true" TabIndex="29"
                                                            Width="100%" OnSelectedIndexChanged="ddlIndicatorWHTType3_SelectedIndexChanged">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlIndicatorWHTType3" runat="server" ControlToValidate="ddlIndicatorWHTType3"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT type cannot be blank." SetFocusOnError="true" InitialValue="---Select---"
                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT type cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlWithHoldingTaxCode3" runat="server" AutoPostBack="False"
                                                            Width="100%" TabIndex="30">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlWithHoldingTaxCode3" runat="server" ControlToValidate="ddlWithHoldingTaxCode3"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Code cannot be blank." SetFocusOnError="true" InitialValue="---Select---"
                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Code cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlTypeRecipient3" runat="server" AutoPostBack="False" TabIndex="31"
                                                            Width="100%">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlTypeRecipient3" runat="server" ControlToValidate="ddlTypeRecipient3"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Type of recipient cannot be blank." InitialValue="---Select---"
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Type of recipient cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWHTExemptCertNo3" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="32" MaxLength="10" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWHTExemptCertNo3" runat="server" ControlToValidate="txtWHTExemptCertNo3"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Certificate Number cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Certificate Number cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtExemptionRateBatchInp3" runat="server" CssClass="textbox" Width="55px"
                                                            TabIndex="33" MaxLength="5" />
                                                        <asp:RequiredFieldValidator ID="reqtxtExemptionRateBatchInp3" runat="server" ControlToValidate="txtExemptionRateBatchInp3"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Exemption rate cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Exemption rate cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWTExemptFromDate3" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="34" MaxLength="10" />
                                                        <act:CalendarExtender ID="CaltxtWTExemptFromDate3" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtWTExemptFromDate3" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWTExemptFromDate3" runat="server" ControlToValidate="txtWTExemptFromDate3"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Exempt From Date cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Exempt From Date cannot be blank.' />" />
                                                        <asp:RegularExpressionValidator ID="regtxtWTExemptFromDate3" runat="server" ControlToValidate="txtWTExemptFromDate3"
                                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                            ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWTExemptToDate3" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="35" MaxLength="10" />
                                                        <act:CalendarExtender ID="CaltxtWTExemptToDate3" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtWTExemptToDate3" />
                                                        <asp:RequiredFieldValidator ID="ReqtxtWTExemptToDate3" runat="server" ControlToValidate="txtWTExemptToDate3"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Exempt To Date cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Exempt To Date cannot be blank.' />" />
                                                        <asp:RegularExpressionValidator ID="regtxtWTExemptToDate3" runat="server" ControlToValidate="txtWTExemptToDate3"
                                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                            ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlIndicatorWHTType4" runat="server" AutoPostBack="true" TabIndex="36"
                                                            Width="100%" OnSelectedIndexChanged="ddlIndicatorWHTType4_SelectedIndexChanged">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlIndicatorWHTType4" runat="server" ControlToValidate="ddlIndicatorWHTType4"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT type cannot be blank." SetFocusOnError="true" InitialValue="---Select---"
                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT type cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlWithHoldingTaxCode4" runat="server" AutoPostBack="False"
                                                            Width="100%" TabIndex="37">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlWithHoldingTaxCode4" runat="server" ControlToValidate="ddlWithHoldingTaxCode4"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Code cannot be blank." SetFocusOnError="true" InitialValue="---Select---"
                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Code cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlTypeRecipient4" runat="server" AutoPostBack="False" TabIndex="38"
                                                            Width="100%">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlTypeRecipient4" runat="server" ControlToValidate="ddlTypeRecipient4"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Type of recipient cannot be blank." InitialValue="---Select---"
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Type of recipient cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWHTExemptCertNo4" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="39" MaxLength="10" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWHTExemptCertNo4" runat="server" ControlToValidate="txtWHTExemptCertNo4"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Certificate Number cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Certificate Number cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtExemptionRateBatchInp4" runat="server" CssClass="textbox" Width="55px"
                                                            TabIndex="40" MaxLength="5" />
                                                        <asp:RequiredFieldValidator ID="reqtxtExemptionRateBatchInp4" runat="server" ControlToValidate="txtExemptionRateBatchInp4"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Exemption rate cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Exemption rate cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWTExemptFromDate4" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="41" MaxLength="10" />
                                                        <act:CalendarExtender ID="CaltxtWTExemptFromDate4" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtWTExemptFromDate4" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWTExemptFromDate4" runat="server" ControlToValidate="txtWTExemptFromDate4"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Exempt From Date cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Exempt From Date cannot be blank.' />" />
                                                        <asp:RegularExpressionValidator ID="regtxtWTExemptFromDate4" runat="server" ControlToValidate="txtWTExemptFromDate4"
                                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                            ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWTExemptToDate4" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="42" MaxLength="10" />
                                                        <act:CalendarExtender ID="CaltxtWTExemptToDate4" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtWTExemptToDate4" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWTExemptToDate4" runat="server" ControlToValidate="txtWTExemptToDate4"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Exempt To Date cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Exempt To Date cannot be blank.' />" />
                                                        <asp:RegularExpressionValidator ID="regtxtWTExemptToDate4" runat="server" ControlToValidate="txtWTExemptToDate4"
                                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                            ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlIndicatorWHTType5" runat="server" AutoPostBack="true" TabIndex="43"
                                                            Width="100%" OnSelectedIndexChanged="ddlIndicatorWHTType5_SelectedIndexChanged">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlIndicatorWHTType5" runat="server" ControlToValidate="ddlIndicatorWHTType5"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT type cannot be blank." SetFocusOnError="true" InitialValue="---Select---"
                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT type cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlWithHoldingTaxCode5" runat="server" AutoPostBack="False"
                                                            Width="100%" TabIndex="44">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlWithHoldingTaxCode5" runat="server" ControlToValidate="ddlWithHoldingTaxCode5"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Code cannot be blank." SetFocusOnError="true" InitialValue="---Select---"
                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Code cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlTypeRecipient5" runat="server" AutoPostBack="False" TabIndex="45"
                                                            Width="100%">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlTypeRecipient5" runat="server" ControlToValidate="ddlTypeRecipient5"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Type of recipient cannot be blank." InitialValue="---Select---"
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Type of recipient cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWHTExemptCertNo5" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="46" MaxLength="10" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWHTExemptCertNo5" runat="server" ControlToValidate="txtWHTExemptCertNo5"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Certificate Number cannot be blank." InitialValue="---Select---"
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Certificate Number cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtExemptionRateBatchInp5" runat="server" CssClass="textbox" Width="55px"
                                                            TabIndex="47" MaxLength="5" />
                                                        <asp:RequiredFieldValidator ID="reqtxtExemptionRateBatchInp5" runat="server" ControlToValidate="txtExemptionRateBatchInp5"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Exemption rate cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Exemption rate cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWTExemptFromDate5" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="48" MaxLength="10" />
                                                        <act:CalendarExtender ID="CaltxtWTExemptFromDate5" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtWTExemptFromDate5" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWTExemptFromDate5" runat="server" ControlToValidate="txtWTExemptFromDate5"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Exempt From Date cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Exempt From Date cannot be blank.' />" />
                                                        <asp:RegularExpressionValidator ID="regtxtWTExemptFromDate5" runat="server" ControlToValidate="txtWTExemptFromDate5"
                                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                            ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWTExemptToDate5" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="49" MaxLength="10" />
                                                        <act:CalendarExtender ID="CaltxtWTExemptToDate5" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtWTExemptToDate5" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWTExemptToDate5" runat="server" ControlToValidate="txtWTExemptToDate5"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Exempt To Date cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Exempt To Date cannot be blank.' />" />
                                                        <asp:RegularExpressionValidator ID="regtxtWTExemptToDate5" runat="server" ControlToValidate="txtWTExemptToDate5"
                                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                            ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlIndicatorWHTType6" runat="server" AutoPostBack="true" TabIndex="50"
                                                            Width="100%" OnSelectedIndexChanged="ddlIndicatorWHTType6_SelectedIndexChanged">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlIndicatorWHTType6" runat="server" ControlToValidate="ddlIndicatorWHTType6"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT type cannot be blank." SetFocusOnError="true" InitialValue="---Select---"
                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT type cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlWithHoldingTaxCode6" runat="server" AutoPostBack="False"
                                                            Width="100%" TabIndex="51">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlWithHoldingTaxCode6" runat="server" ControlToValidate="ddlWithHoldingTaxCode6"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Code cannot be blank." SetFocusOnError="true" InitialValue="---Select---"
                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Code cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <act:ComboBox ID="ddlTypeRecipient6" runat="server" AutoPostBack="False" TabIndex="52"
                                                            Width="100%">
                                                            <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                                        </act:ComboBox>
                                                        <asp:RequiredFieldValidator ID="reqddlTypeRecipient6" runat="server" ControlToValidate="ddlTypeRecipient6"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Type of recipient cannot be blank." InitialValue="---Select---"
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Type of recipient cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWHTExemptCertNo6" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="53" MaxLength="10" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWHTExemptCertNo6" runat="server" ControlToValidate="txtWHTExemptCertNo6"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Certificate Number cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Certificate Number cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtExemptionRateBatchInp6" runat="server" CssClass="textbox" Width="55px"
                                                            TabIndex="54" MaxLength="5" />
                                                        <asp:RequiredFieldValidator ID="reqtxtExemptionRateBatchInp6" runat="server" ControlToValidate="txtExemptionRateBatchInp6"
                                                            ValidationGroup="CompanyCode" ErrorMessage="Exemption rate cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Exemption rate cannot be blank.' />" />
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWTExemptFromDate6" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="55" MaxLength="10" />
                                                        <act:CalendarExtender ID="CaltxtWTExemptFromDate6" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtWTExemptFromDate6" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWTExemptFromDate6" runat="server" ControlToValidate="txtWTExemptFromDate6"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Exempt From Date cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Exempt From Date cannot be blank.' />" />
                                                        <asp:RegularExpressionValidator ID="regtxtWTExemptFromDate6" runat="server" ControlToValidate="txtWTExemptFromDate6"
                                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                            ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td class="rigthTD">
                                                        <asp:TextBox ID="txtWTExemptToDate6" runat="server" CssClass="textbox" Width="100px"
                                                            TabIndex="56" MaxLength="10" />
                                                        <act:CalendarExtender ID="CaltxtWTExemptToDate6" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtWTExemptToDate6" />
                                                        <asp:RequiredFieldValidator ID="reqtxtWTExemptToDate6" runat="server" ControlToValidate="txtWTExemptToDate6"
                                                            ValidationGroup="CompanyCode" ErrorMessage="WHT Exempt To Date cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='WHT Exempt To Date cannot be blank.' />" />
                                                        <asp:RegularExpressionValidator ID="regtxtWTExemptToDate6" runat="server" ControlToValidate="txtWTExemptToDate6"
                                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                            ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Reason for exemption
                                            <asp:Label ID="lableddlExemptionReason" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlExemptionReason" runat="server" TabIndex="57">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlExemptionReason" runat="server" ControlToValidate="ddlExemptionReason"
                                                ValidationGroup="CompanyCode" ErrorMessage="Reason for exemption cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Reason for exemption cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Withholding tax identification number
                                            <asp:Label ID="labletxtWHTIdentificationNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtWHTIdentificationNo" runat="server" CssClass="textbox" Width="160px"
                                                TabIndex="58" MaxLength="16" />
                                            <asp:RequiredFieldValidator ID="reqtxtWHTIdentificationNo" runat="server" ControlToValidate="txtWHTIdentificationNo"
                                                ValidationGroup="CompanyCode" ErrorMessage="Withholding tax identification number cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Withholding tax identification number cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Reason for exemption
                                            <asp:Label ID="lableddlExemptionReason2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlExemptionReason2" runat="server" TabIndex="59">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlExemptionReason2" runat="server" ControlToValidate="ddlExemptionReason2"
                                                ValidationGroup="CompanyCode" ErrorMessage="Reason for exemption cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Reason for exemption cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Withholding tax identification number
                                            <asp:Label ID="labletxtWHTIdentificationNo2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtWHTIdentificationNo2" runat="server" CssClass="textbox" Width="160px"
                                                TabIndex="60" MaxLength="16" />
                                            <asp:RequiredFieldValidator ID="reqtxtWHTIdentificationNo2" runat="server" ControlToValidate="txtWHTIdentificationNo2"
                                                ValidationGroup="CompanyCode" ErrorMessage="Withholding tax identification number cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Withholding tax identification number cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Date 1
                                            <asp:Label ID="labletxtKOVDate" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtKOVDate" runat="server" CssClass="textbox" Width="100px" MaxLength="10"
                                                TabIndex="61" />
                                            <act:CalendarExtender ID="CaltxtKOVDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtKOVDate" />
                                            <asp:RequiredFieldValidator ID="reqtxtKOVDate" runat="server" ControlToValidate="txtKOVDate"
                                                ValidationGroup="CompanyCode" ErrorMessage="Date 1 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Date 1 cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="revDTTest" runat="server" ControlToValidate="txtKOVDate"
                                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Date 2
                                            <asp:Label ID="labletxtKOBIssueDate" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtKOBIssueDate" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="62" MaxLength="10" />
                                            <act:CalendarExtender ID="CaltxtKOBIssueDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtKOBIssueDate" />
                                            <asp:RequiredFieldValidator ID="reqtxtKOBIssueDate" runat="server" ControlToValidate="txtKOBIssueDate"
                                                ValidationGroup="CompanyCode" ErrorMessage="Date 2 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Date 2 cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtKOBIssueDate"
                                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Date 3
                                            <asp:Label ID="labletxtValidFromDate" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtValidFromDate" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="63" MaxLength="10" />
                                            <act:CalendarExtender ID="CaltxtValidFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtValidFromDate" />
                                            <asp:RequiredFieldValidator ID="reqtxtValidFromDate" runat="server" ControlToValidate="txtValidFromDate"
                                                ValidationGroup="CompanyCode" ErrorMessage="Date 3 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Date 3 cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtValidFromDate"
                                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Date 4
                                            <asp:Label ID="labletxtGMValidDate" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtGMValidDate" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="64" MaxLength="10" />
                                            <act:CalendarExtender ID="CaltxtGMValidDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtGMValidDate" />
                                            <asp:RequiredFieldValidator ID="reqtxtGMValidDate" runat="server" ControlToValidate="txtGMValidDate"
                                                ValidationGroup="CompanyCode" ErrorMessage="Date 4 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Date 4 cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtGMValidDate"
                                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Date 5
                                            <asp:Label ID="labletxtMDate" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtMDate" runat="server" CssClass="textbox" Width="100px" MaxLength="10"
                                                TabIndex="66" />
                                            <act:CalendarExtender ID="CaltxtMDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtMDate" />
                                            <asp:RequiredFieldValidator ID="reqtxtMDate" runat="server" ControlToValidate="txtMDate"
                                                ValidationGroup="CompanyCode" ErrorMessage="Date 5 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Date 5 cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtMDate"
                                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Date 6
                                            <asp:Label ID="labletxtDate3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtDate3" runat="server" CssClass="textbox" Width="100px" MaxLength="10"
                                                TabIndex="67" />
                                            <act:CalendarExtender ID="CaltxtDate3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate3" />
                                            <asp:RequiredFieldValidator ID="reqtxtDate3" runat="server" ControlToValidate="txtDate3"
                                                ValidationGroup="CompanyCode" ErrorMessage="Date 6 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Date 6 cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtDate3"
                                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Authority for Exemption from Withholding Tax
                                            <asp:Label ID="labletxtAuthExemptionWHT" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtAuthExemptionWHT" runat="server" CssClass="textboxAutocomplete"
                                                TabIndex="68" Width="10px" MaxLength="1" onfocus="return AuthExemptionWHTOnFocus();"
                                                onchange="return AuthExemptionWHTTextChangeEvent();" />
                                            <asp:RequiredFieldValidator ID="reqtxtAuthExemptionWHT" runat="server" ControlToValidate="txtAuthExemptionWHT"
                                                ValidationGroup="CompanyCode" ErrorMessage="Authority for Exemption from Withholding Tax cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Authority for Exemption from Withholding Tax cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Vendor Recipient Type
                                            <asp:Label ID="labletxtVendReceiptType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtVendReceiptType" runat="server" CssClass="textboxAutocomplete"
                                                TabIndex="69" Width="25px" SkinID="Disable" MaxLength="2" onfocus="return VendReceiptTypeOnFocus();"
                                                onchange="return VendReceiptTypeTextChangeEvent();" />
                                            <asp:RequiredFieldValidator ID="reqtxtVendReceiptType" runat="server" ControlToValidate="txtVendReceiptType"
                                                ValidationGroup="CompanyCode" ErrorMessage="Vendor Recipient Type cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Recipient Type cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr id="trButton" runat="server" visible="false">
                                        <td class="centerTD" colspan="4">
                                            <asp:Button ID="btnPrevious" runat="server" ValidationGroup="save" Text="Back" TabIndex="70"
                                                UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                            <asp:Button ID="btnSave" runat="server" ValidationGroup="CompanyCode" Text="Save"
                                                TabIndex="71" UseSubmitBehavior="true" CssClass="button" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnNext" runat="server" ValidationGroup="CompanyCode" Text="Save & Next"
                                                TabIndex="72" UseSubmitBehavior="true" CssClass="button" OnClick="btnNext_Click"
                                                Width="120px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="CompanyCode" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="31" Visible="false" />
            <asp:Label ID="lblCompanyCodeId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        var textboxId = "";
        var textboxRealId = "";
        var ActionType = $('#<%= lblActionType.ClientID%>').html();
        var CountryId = "";

        function BankKeyOnFocus() {

            CountryId = $('#<%= ddlBankCountrykey.ClientID%>').val();

            textboxId = $('#<%= txtBankKey.ClientID%>').attr('ID');
            textboxRealId = CountryId;
            AutoCompleteBankIFSC();
        }

        function BankKeyTextChangeEvent() {
            //CountryId = $('#<%= ddlBankCountrykey.ClientID%>').val();
            //CheckBankValue($('#<%= txtBankKey.ClientID%>').attr('ID'), CountryId, $('#<%= btnNext.ClientID%>').attr('ID'));
            __doPostBack($('#<%= txtBankKey.ClientID%>').attr('ID'), 'TextChanged');
        }

        function PartnerBankTypeOnFocus() {
            textboxId = $('#<%= txtPartnerBankType.ClientID%>').attr('ID');
            textboxRealId = "txtPartnerBankType";
            AutoCompleteLookUpVendor();
        }

        function PartnerBankTypeTextChangeEvent() {
            CheckLookupVendor($('#<%= txtPartnerBankType.ClientID%>').attr('ID'), "txtPartnerBankType", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function VendReceiptTypeOnFocus() {
            textboxId = $('#<%= txtVendReceiptType.ClientID%>').attr('ID');
            textboxRealId = "txtVendReceiptType";
            AutoCompleteLookUpVendor();
        }

        function VendReceiptTypeTextChangeEvent() {
            CheckLookupVendor($('#<%= txtVendReceiptType.ClientID%>').attr('ID'), "txtVendReceiptType", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function AuthExemptionWHTOnFocus() {
            textboxId = $('#<%= txtAuthExemptionWHT.ClientID%>').attr('ID');
            textboxRealId = "txtAuthExemptionWHT";
            AutoCompleteLookUpVendor();
        }

        function AuthExemptionWHTTextChangeEvent() {
            CheckLookupVendor($('#<%= txtAuthExemptionWHT.ClientID%>').attr('ID'), "txtAuthExemptionWHT", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function WHTExemptCertNoOnFocus() {
            textboxId = $('#<%= txtWHTExemptCertNo.ClientID%>').attr('ID');
            textboxRealId = "txtWHTExemptCertNo";
            AutoCompleteLookUpVendor();
        }

        function WHTExemptCertNoTextChangeEvent() {
            CheckLookupVendor($('#<%= txtWHTExemptCertNo.ClientID%>').attr('ID'), "txtWHTExemptCertNo", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function WHTIdentificationNoOnFocus() {
            textboxId = $('#<%= txtWHTIdentificationNo.ClientID%>').attr('ID');
            textboxRealId = "txtWHTIdentificationNo";
            AutoCompleteLookUpVendor();
        }

        function WHTIdentificationNoTextChangeEvent() {
            CheckLookupVendor($('#<%= txtWHTIdentificationNo.ClientID%>').attr('ID'), "txtWHTIdentificationNo", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

    </script>
</asp:Content>
