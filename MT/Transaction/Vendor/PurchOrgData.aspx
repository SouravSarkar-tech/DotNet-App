<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Vendor/VendorMasterPage.master"
    AutoEventWireup="true" CodeFile="PurchOrgData.aspx.cs" Inherits="Transaction_Customer_PurchOrgData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetToolTip(control) {
            ddrivetip('32', control);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">
                    Purch. Org data
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Purchase order
                                <asp:Label ID="lableddlPurchaseOrder_Currency" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                                currency
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <ajax:ComboBox ID="ddlPurchaseOrder_Currency" runat="server" AutoPostBack="false"
                                    TabIndex="1" DropDownStyle="DropDownList" AutoCompleteMode="Suggest" CaseSensitive="False"
                                    CssClass="AjaxToolkitStyle">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </ajax:ComboBox>
                                <asp:RequiredFieldValidator ID="reqddlPurchaseOrder_Currency" runat="server" ControlToValidate="ddlPurchaseOrder_Currency"
                                    ValidationGroup="save" ErrorMessage="Purchase order currency cannot be blank."
                                    InitialValue="--Select--" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchase order currency cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Terms of
                                <asp:Label ID="lableddlTermsPayment_Key" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                                Payment Key
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtTermsPayment_Key')"
                                    onmouseout="hideddrivetip()" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTermsPayment_Key" runat="server" TabIndex="2">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTermsPayment_Key" runat="server" ControlToValidate="ddlTermsPayment_Key"
                                    ValidationGroup="save" ErrorMessage="Terms of Payment Key cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Terms of Payment Key cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Incoterms
                                <asp:Label ID="lableddlIncotermsPart1" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                                (Part 1)
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtIncotermsPart1')"
                                    onmouseout="hideddrivetip()" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlIncotermsPart1" runat="server" TabIndex="3">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlIncotermsPart1" runat="server" ControlToValidate="ddlIncotermsPart1"
                                    ValidationGroup="save" ErrorMessage="Incoterms (Part 1) cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Incoterms (Part 1) cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Incoterms
                                <asp:Label ID="labletxtIncotermsPart2" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                                (Part 2)
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtIncotermsPart2')"
                                    onmouseout="hideddrivetip()" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtIncotermsPart2" runat="server" CssClass="textbox" MaxLength="28"
                                    TabIndex="4" Width="150" />
                                <asp:RequiredFieldValidator ID="reqtxtIncotermsPart2" runat="server" ControlToValidate="txtIncotermsPart2"
                                    ValidationGroup="save" ErrorMessage="Incoterms (Part 2) cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Incoterms (Part 2) cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Group for Calculation Schema (Vendor)
                                <asp:Label ID="lableddlGroupCalculation_SchemaVendor" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtGroupCalculation_SchemaVendor')"
                                    onmouseout="hideddrivetip()" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlGroupCalculation_SchemaVendor" runat="server" TabIndex="5">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlGroupCalculation_SchemaVendor" runat="server"
                                    InitialValue="0" ControlToValidate="ddlGroupCalculation_SchemaVendor" ValidationGroup="save"
                                    ErrorMessage="Group for Calculation Schema (Vendor) cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Group for Calculation Schema (Vendor) cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Indicator: GR-Based Invoice Verification
                                <asp:Label ID="lablechkIndicatorInvoice_Verification" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:CheckBox ID="chkIndicatorInvoice_Verification" runat="server" Text="" TabIndex="6" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Indicator for Service-Based Invoice Verification
                                <asp:Label ID="lablechkIndicator_ServiceBased_Verification" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:CheckBox ID="chkIndicator_ServiceBased_Verification" runat="server" Text=""
                                    TabIndex="7" />
                            </td>
                            <td class="leftTD">
                                Staging Time in Days (Batch Input)
                                <asp:Label ID="labletxtStagingTime_Days_BatchInput" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtStagingTime_Days_BatchInput" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="8" MaxLength="3" Width="50px" onfocus="return txtStagingTime_Days_BatchInputOnFocus();"
                                    onchange="return txtStagingTime_Days_BatchInputTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtStagingTime_Days_BatchInput" runat="server"
                                    ControlToValidate="txtStagingTime_Days_BatchInput" ValidationGroup="save" ErrorMessage="Staging Time in Days (Batch Input) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Staging Time in Days (Batch Input) cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Minimum order value (batch input field)
                                <asp:Label ID="labletxtMinimumOrder_batchInput" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtMinimumOrder_batchInput" runat="server" CssClass="textbox" MaxLength="16"
                                    TabIndex="9" onkeypress="return IsNumber();" Width="150" />
                                <asp:RequiredFieldValidator ID="reqtxtMinimumOrder_batchInput" runat="server" ControlToValidate="txtMinimumOrder_batchInput"
                                    ValidationGroup="save" ErrorMessage="Minimum order value (batch input field) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Minimum order value (batch input field) cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Responsible Salesperson at Vendor's Office
                                <asp:Label ID="labletxtResponsible_SalesPerson" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtResponsible_SalesPerson" runat="server" CssClass="textbox" MaxLength="30"
                                    TabIndex="10" Width="150" />
                                <asp:RequiredFieldValidator ID="reqtxtResponsible_SalesPerson" runat="server" ControlToValidate="txtResponsible_SalesPerson"
                                    ValidationGroup="save" ErrorMessage="Responsible Salesperson at Vendor's Office cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Responsible Salesperson at Vendor's Office cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Vendor's Telephone Number
                                <asp:Label ID="labletxtVendor_TelephoneNumber" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtVendor_TelephoneNumber" runat="server" CssClass="textbox" MaxLength="16"
                                    TabIndex="11" Width="150" />
                                <asp:RequiredFieldValidator ID="reqtxtVendor_TelephoneNumber" runat="server" ControlToValidate="txtVendor_TelephoneNumber"
                                    ValidationGroup="save" ErrorMessage="Vendor's Telephone Number cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor's Telephone Number cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                ABC indicator
                                <asp:Label ID="lablechkABC_Indicator" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkABC_Indicator" runat="server" Text="" TabIndex="12" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Purchasing block at purchasing organization level
                                <asp:Label ID="lablechkPurchasingBlock_Purchasing" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkPurchasingBlock_Purchasing" runat="server" Text="" TabIndex="13" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Delete flag for vendor at purchasing level
                                <asp:Label ID="lablechkDeleteflag_purchasinglevel" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkDeleteflag_purchasinglevel" runat="server" Text="" TabIndex="14" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Automatic Generation of Purchase Order Allowed
                                <asp:Label ID="lablechkAutomatic_Generation" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkAutomatic_Generation" runat="server" Text="" TabIndex="15" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Order Acknowledgment Requirement
                                <asp:Label ID="lablechkOrderAcknowledgment_Requirement" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkOrderAcknowledgment_Requirement" runat="server" Text="" TabIndex="16" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Mode of Transport for Foreign Trade
                                <asp:Label ID="labletxtModeTransport_ForeignTrade" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtModeTransport_ForeignTrade" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="17" MaxLength="1" Width="50px" onfocus="return txtModeTransport_ForeignTradeOnFocus();"
                                    onchange="return txtModeTransport_ForeignTradeTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtModeTransport_ForeignTrade" runat="server"
                                    ControlToValidate="txtModeTransport_ForeignTrade" ValidationGroup="save" ErrorMessage="Mode of Transport for Foreign Trade customer cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Mode of Transport for Foreign Trade cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Customs Office: Office of Exit/Entry for Foreign Trade
                                <asp:Label ID="labletxtCustomsOffice_ForeignTrade" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtCustomsOffice_ForeignTrade" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="18" MaxLength="6" Width="50" onfocus="return txtCustomsOffice_ForeignTradeOnFocus();"
                                    onchange="return txtCustomsOffice_ForeignTradeTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtCustomsOffice_ForeignTrade" runat="server"
                                    ControlToValidate="txtCustomsOffice_ForeignTrade" ValidationGroup="save" ErrorMessage="Customs Office: Office of Exit/Entry for Foreign Trade cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customs Office: Office of Exit/Entry for Foreign Trade cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Purchasing Group
                                <asp:Label ID="labletxtPurchasing_Group" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtPurchasing_Group" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="19" MaxLength="16" Width="150" onfocus="return txtPurchasing_GroupOnFocus();"
                                    onchange="return txtPurchasing_GroupTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtPurchasing_Group" runat="server" ControlToValidate="txtPurchasing_Group"
                                    ValidationGroup="save" ErrorMessage="Purchasing Group cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Group cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Indicator: vendor subject to subseq. settlement accounti
                                <asp:Label ID="lablechkIndicator_vendor_accountimng" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:CheckBox ID="chkIndicator_vendor_accountimng" runat="server" Text="" TabIndex="20" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Planned Delivery Time in Days (BTCI)
                                <asp:Label ID="labletxtPlannedTime_Days_BTCI" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtPlannedTime_Days_BTCI" runat="server" CssClass="textbox" MaxLength="3"
                                    TabIndex="21" onkeypress="return IsNumber();" Width="50" />
                                <asp:RequiredFieldValidator ID="reqtxtPlannedTime_Days_BTCI" runat="server" ControlToValidate="txtPlannedTime_Days_BTCI"
                                    ValidationGroup="save" ErrorMessage="Planned Delivery Time in Days (BTCI) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planned Delivery Time in Days (BTCI) cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Shipping Conditions
                                <asp:Label ID="labletxtShipping_Conditions" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtShipping_Conditions" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="22" MaxLength="2" Width="50px" onfocus="return txtShipping_ConditionsOnFocus();"
                                    onchange="return txtShipping_ConditionsTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtShipping_Conditions" runat="server" ControlToValidate="txtShipping_Conditions"
                                    ValidationGroup="save" ErrorMessage="Shipping Conditions cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Shipping Conditions cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Category indicator for tax codes
                                <asp:Label ID="labletxtCategory_tax_codes" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtCategory_tax_codes" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="23" MaxLength="3" Width="150" onfocus="return txtCategory_tax_codesOnFocus();"
                                    onchange="return txtCategory_tax_codesTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtCategory_tax_codes" runat="server" ControlToValidate="txtCategory_tax_codes"
                                    ValidationGroup="save" ErrorMessage="Category indicator for tax codes cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Category indicator for tax codes cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Vendor Subrange
                                <asp:Label ID="labletxtVendor_Subrange" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtVendor_Subrange" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="24" MaxLength="6" Width="50px" onfocus="return txtVendor_SubrangeOnFocus();"
                                    onchange="return txtVendor_SubrangeTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtVendor_Subrange" runat="server" ControlToValidate="txtVendor_Subrange"
                                    ValidationGroup="save" ErrorMessage="Vendor Subrange cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Subrange cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Language Acc. to ISO 639 (Batch Input Field)
                                <asp:Label ID="labletxtLanguage_BatchInputField" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtLanguage_BatchInputField" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="25" MaxLength="2" Width="50" onfocus="return txtLanguage_BatchInputFieldOnFocus();"
                                    onchange="return txtLanguage_BatchInputFieldTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtLanguage_BatchInputField" runat="server" ControlToValidate="txtLanguage_BatchInputField"
                                    ValidationGroup="save" ErrorMessage="Language Acc. to ISO 639 (Batch Input Field) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Language Acc. to ISO 639 (Batch Input Field) cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Purchasing Organization
                                <asp:Label ID="labletxtPurchasing_Organization" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtPurchasing_Organization" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="26" MaxLength="4" Width="50px" onfocus="return txtPurchasing_OrganizationOnFocus();"
                                    onchange="return txtPurchasing_OrganizationTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtPurchasing_Organization" runat="server" ControlToValidate="txtPurchasing_Organization"
                                    ValidationGroup="save" ErrorMessage="Purchasing Organization cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Organization cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Plant
                                <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlPlant" runat="server" TabIndex="27">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                    ValidationGroup="save" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Partner counter
                                <asp:Label ID="labletxtPartner_counter" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtPartner_counter" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="28" MaxLength="2" Width="100" onfocus="return txtPartner_counterOnFocus();"
                                    onchange="return txtPartner_counterTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtPartner_counter" runat="server" ControlToValidate="txtPartner_counter"
                                    ValidationGroup="save" ErrorMessage="Partner counter cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner counter cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Partner Function
                                <asp:Label ID="lableddlPartner_Function" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlPartner_Function" runat="server" TabIndex="29">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPartner_Function" runat="server" ControlToValidate="ddlPartner_Function"
                                    ValidationGroup="save" ErrorMessage="Partner Function cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner Function cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Name of Business Partner in Vendor Master
                                <asp:Label ID="labletxtNameBusinessPartnerVendorMaster" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtNameBusinessPartnerVendorMaster" runat="server" CssClass="textbox"
                                    TabIndex="30" MaxLength="6" Width="50px" />
                                <asp:RequiredFieldValidator ID="reqtxtNameBusinessPartnerVendorMaster" runat="server"
                                    ControlToValidate="txtNameBusinessPartnerVendorMaster" ValidationGroup="save" ErrorMessage="Name of Business Partner in Vendor Master cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Name of Business Partner in Vendor Master cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Partner Function
                                <asp:Label ID="lableddlPartner_Function2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlPartner_Function2" runat="server" TabIndex="31">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPartner_Function2" runat="server" ControlToValidate="ddlPartner_Function2"
                                    ValidationGroup="save" ErrorMessage="Partner Function cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner Function cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Name of Person who Created the Object
                                <asp:Label ID="labletxtNameBusinessPartnerVendorMaster2" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                 <asp:TextBox ID="txtNameBusinessPartnerVendorMaster2" runat="server" CssClass="textbox"
                                    TabIndex="32" MaxLength="6" Width="50px" />
                                <asp:RequiredFieldValidator ID="reqtxtNameBusinessPartnerVendorMaster2" runat="server"
                                    ControlToValidate="txtNameBusinessPartnerVendorMaster2" ValidationGroup="save" ErrorMessage="Name of Business Partner in Vendor Master cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Name of Business Partner in Vendor Master cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Partner Function
                                <asp:Label ID="lableddlPartner_Function3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlPartner_Function3" runat="server" TabIndex="33">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPartner_Function3" runat="server" ControlToValidate="ddlPartner_Function3"
                                    ValidationGroup="save" ErrorMessage="Partner Function cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner Function cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Name of Business Partner in Vendor Master
                                <asp:Label ID="labletxtNameBusinessPartnerVendorMaster3" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtNameBusinessPartnerVendorMaster3" runat="server" CssClass="textbox"
                                    TabIndex="34" MaxLength="6" Width="50px" />
                                <asp:RequiredFieldValidator ID="reqtxtNameBusinessPartnerVendorMaster3" runat="server"
                                    ControlToValidate="txtNameBusinessPartnerVendorMaster3" ValidationGroup="save" ErrorMessage="Name of Business Partner in Vendor Master cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Name of Business Partner in Vendor Master cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Partner Function
                                <asp:Label ID="lableddlPartner_Function4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlPartner_Function4" runat="server" TabIndex="35">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPartner_Function4" runat="server" ControlToValidate="ddlPartner_Function4"
                                    ValidationGroup="save" ErrorMessage="Partner Function cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner Function cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Name of Business Partner in Vendor Master
                                <asp:Label ID="labletxtNameBusinessPartnerVendorMaster4" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtNameBusinessPartnerVendorMaster4" runat="server" CssClass="textbox"
                                    TabIndex="36" MaxLength="6" Width="50px" />
                                <asp:RequiredFieldValidator ID="reqtxtNameBusinessPartnerVendorMaster4" runat="server"
                                    ControlToValidate="txtNameBusinessPartnerVendorMaster4" ValidationGroup="save" ErrorMessage="Name of Business Partner in Vendor Master cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Name of Business Partner in Vendor Master cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Partner Function
                                <asp:Label ID="lableddlPartner_Function5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlPartner_Function5" runat="server" TabIndex="37">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPartner_Function5" runat="server" ControlToValidate="ddlPartner_Function5"
                                    ValidationGroup="save" ErrorMessage="Partner Function cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner Function cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Name of Business Partner in Vendor Master
                                <asp:Label ID="labletxtNameBusinessPartnerVendorMaster5" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtNameBusinessPartnerVendorMaster5" runat="server" CssClass="textbox"
                                    TabIndex="38" MaxLength="6" Width="50px" />
                                <asp:RequiredFieldValidator ID="reqtxtNameBusinessPartnerVendorMaster5" runat="server"
                                    ControlToValidate="txtNameBusinessPartnerVendorMaster5" ValidationGroup="save" ErrorMessage="Name of Business Partner in Vendor Master cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Name of Business Partner in Vendor Master cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Partner Function
                                <asp:Label ID="lableddlPartner_Function6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlPartner_Function6" runat="server" TabIndex="39">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPartner_Function6" runat="server" ControlToValidate="ddlPartner_Function6"
                                    ValidationGroup="save" ErrorMessage="Partner Function cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner Function cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Name of Business Partner in Vendor Master
                                <asp:Label ID="labletxtNameBusinessPartnerVendorMaster6" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtNameBusinessPartnerVendorMaster6" runat="server" CssClass="textbox"
                                    TabIndex="40" MaxLength="6" Width="50px" />
                                <asp:RequiredFieldValidator ID="reqtxtNameBusinessPartnerVendorMaster6" runat="server"
                                    ControlToValidate="txtNameBusinessPartnerVendorMaster6" ValidationGroup="save" ErrorMessage="Name of Business Partner in Vendor Master cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Name of Business Partner in Vendor Master cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Date on Which Record Was Created
                                <asp:Label ID="labletxtDate_Which_Record_Created" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtDate_Which_Record_Created" runat="server" CssClass="textbox"
                                    TabIndex="41" MaxLength="12" Width="100" />
                                <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate_Which_Record_Created"
                                    Format="dd/MM/yyyy">
                                </ajax:CalendarExtender>
                                <asp:RequiredFieldValidator ID="reqtxtDate_Which_Record_Created" runat="server" ControlToValidate="txtDate_Which_Record_Created"
                                    ValidationGroup="save" ErrorMessage="Date on Which Record Was Created cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Date on Which Record Was Created cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="revDTTest" runat="server" ControlToValidate="txtDate_Which_Record_Created"
                                    ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                    Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                    ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                            </td>
                            <td class="leftTD">
                                Reference to other vendor
                                <asp:Label ID="labletxtReference_vendor" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtReference_vendor" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="42" MaxLength="10" Width="50px" onfocus="return txtReference_vendorOnFocus();"
                                    onchange="return txtReference_vendorTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtReference_vendor" runat="server" ControlToValidate="txtReference_vendor"
                                    ValidationGroup="save" ErrorMessage="Reference to other vendor cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Reference to other vendor cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Personnel Number (Batch Input Field)
                                <asp:Label ID="lablechkPersonnel_Number_BatchInputField" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:CheckBox ID="chkPersonnel_Number_BatchInputField" runat="server" Text="" TabIndex="43" />
                            </td>
                            <td class="leftTD">
                                Country Key
                                <asp:Label ID="lableddlCountry" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <ajax:ComboBox ID="ddlCountry" runat="server" AutoPostBack="false" DropDownStyle="DropDownList"
                                    TabIndex="44" AutoCompleteMode="Suggest" CaseSensitive="False" CssClass="AjaxToolkitStyle">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </ajax:ComboBox>
                                <asp:RequiredFieldValidator ID="reqddlCountry" runat="server" ControlToValidate="ddlCountry"
                                    ValidationGroup="FT" ErrorMessage="Country of origin of the material cannot be blank."
                                    InitialValue="0" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Country of origin of the material cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Supply region (region supplied)
                                <asp:Label ID="lablechkSupplyRegion_RegionSupplied" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:CheckBox ID="chkSupplyRegion_RegionSupplied" runat="server" Text="" TabIndex="45" />
                            </td>
                            <td class="leftTD">
                                Account Number of Vendor or Creditor
                                <asp:Label ID="lablechkAccountNumber_VendorCreditor" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:CheckBox ID="chkAccountNumber_VendorCreditor" runat="server" Text="" TabIndex="46" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Material Number
                                <asp:Label ID="lablechkMaterial_Number" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:CheckBox ID="chkMaterial_Number" runat="server" Text="" TabIndex="47" />
                            </td>
                            <td class="leftTD">
                                Preference zone
                                <asp:Label ID="labletxtPreference_Zone" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtPreference_Zone" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="48" MaxLength="5" Width="100" onfocus="return txtPreference_ZoneOnFocus();"
                                    onchange="return txtPreference_ZoneTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtPreference_Zone" runat="server" ControlToValidate="txtPreference_Zone"
                                    ValidationGroup="save" ErrorMessage="Preference zone cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Preference zone cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnPrevious" runat="server" ValidationGroup="save" Text="Back" UseSubmitBehavior="false"
                                    TabIndex="49" CssClass="button" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="save" Text="Save" CssClass="button"
                                    UseSubmitBehavior="true" TabIndex="50" OnClick="btnSave_Click" />
                                <asp:Button ID="btnNext" runat="server" ValidationGroup="save" Text="Save & Next"
                                    UseSubmitBehavior="true" TabIndex="51" CssClass="button" OnClick="btnNext_Click"
                                    Width="120px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="save" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="32" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblCustomerGeneralId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <script type="text/javascript">









        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {

        });

        $(function () {

        });


        var ActionType = $('#<%= lblActionType.ClientID%>').html();


        function txtMinimumOrder_batchInputOnFocus() {
            textboxId = $('#<%= txtMinimumOrder_batchInput.ClientID%>').attr('ID');
            textboxRealId = "txtMinimumOrder_batchInput";
            AutoCompleteLookUpVendor();
        }

        function txtMinimumOrder_batchInputTextChangeEvent() {
            CheckLookupVendor($('#<%= txtMinimumOrder_batchInput.ClientID%>').attr('ID'), "txtMinimumOrder_batchInput", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtResponsible_SalesPersonOnFocus() {
            textboxId = $('#<%= txtResponsible_SalesPerson.ClientID%>').attr('ID');
            textboxRealId = "txtResponsible_SalesPerson";
            AutoCompleteLookUpVendor();
        }

        function txtResponsible_SalesPersonTextChangeEvent() {
            CheckLookupVendor($('#<%= txtResponsible_SalesPerson.ClientID%>').attr('ID'), "txtResponsible_SalesPerson", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtVendor_TelephoneNumberOnFocus() {
            textboxId = $('#<%= txtVendor_TelephoneNumber.ClientID%>').attr('ID');
            textboxRealId = "txtVendor_TelephoneNumber";
            AutoCompleteLookUpVendor();
        }

        function txtVendor_TelephoneNumberTextChangeEvent() {
            CheckLookupVendor($('#<%= txtVendor_TelephoneNumber.ClientID%>').attr('ID'), "txtVendor_TelephoneNumber", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtModeTransport_ForeignTradeOnFocus() {
            textboxId = $('#<%= txtModeTransport_ForeignTrade.ClientID%>').attr('ID');
            textboxRealId = "txtModeTransport_ForeignTrade";
            AutoCompleteLookUpVendor();
        }

        function txtModeTransport_ForeignTradeTextChangeEvent() {
            CheckLookupVendor($('#<%= txtModeTransport_ForeignTrade.ClientID%>').attr('ID'), "txtModeTransport_ForeignTrade", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtCustomsOffice_ForeignTradeOnFocus() {
            textboxId = $('#<%= txtCustomsOffice_ForeignTrade.ClientID%>').attr('ID');
            textboxRealId = "txtCustomsOffice_ForeignTrade";
            AutoCompleteLookUpVendor();
        }

        function txtCustomsOffice_ForeignTradeTextChangeEvent() {
            CheckLookupVendor($('#<%= txtCustomsOffice_ForeignTrade.ClientID%>').attr('ID'), "txtCustomsOffice_ForeignTrade", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtPurchasing_GroupOnFocus() {
            textboxId = $('#<%= txtPurchasing_Group.ClientID%>').attr('ID');
            textboxRealId = "txtPurchasing_Group";
            AutoCompleteLookUpVendor();
        }

        function txtPurchasing_GroupTextChangeEvent() {
            CheckLookupVendor($('#<%= txtPurchasing_Group.ClientID%>').attr('ID'), "txtPurchasing_Group", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtPlannedTime_Days_BTCIOnFocus() {
            textboxId = $('#<%= txtPlannedTime_Days_BTCI.ClientID%>').attr('ID');
            textboxRealId = "txtPlannedTime_Days_BTCI";
            AutoCompleteLookUpVendor();
        }

        function txtPlannedTime_Days_BTCITextChangeEvent() {
            CheckLookupVendor($('#<%= txtPlannedTime_Days_BTCI.ClientID%>').attr('ID'), "txtPlannedTime_Days_BTCI", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtShipping_ConditionsOnFocus() {
            textboxId = $('#<%= txtShipping_Conditions.ClientID%>').attr('ID');
            textboxRealId = "txtShipping_Conditions";
            AutoCompleteLookUpVendor();
        }

        function txtShipping_ConditionsTextChangeEvent() {
            CheckLookupVendor($('#<%= txtShipping_Conditions.ClientID%>').attr('ID'), "txtShipping_Conditions", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtStagingTime_Days_BatchInputOnFocus() {
            textboxId = $('#<%= txtStagingTime_Days_BatchInput.ClientID%>').attr('ID');
            textboxRealId = "txtStagingTime_Days_BatchInput";
            AutoCompleteLookUpVendor();
        }

        function txtStagingTime_Days_BatchInputTextChangeEvent() {
            CheckLookupVendor($('#<%= txtStagingTime_Days_BatchInput.ClientID%>').attr('ID'), "txtStagingTime_Days_BatchInput", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtCategory_tax_codesOnFocus() {
            textboxId = $('#<%= txtCategory_tax_codes.ClientID%>').attr('ID');
            textboxRealId = "txtCategory_tax_codes";
            AutoCompleteLookUpVendor();
        }

        function txtCategory_tax_codesTextChangeEvent() {
            CheckLookupVendor($('#<%= txtCategory_tax_codes.ClientID%>').attr('ID'), "txtCategory_tax_codes", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCategory_tax_codesOnFocus() {
            textboxId = $('#<%= txtCategory_tax_codes.ClientID%>').attr('ID');
            textboxRealId = "txtCategory_tax_codes";
            AutoCompleteLookUpVendor();
        }

        function txtCategory_tax_codesTextChangeEvent() {
            CheckLookupVendor($('#<%= txtCategory_tax_codes.ClientID%>').attr('ID'), "txtCategory_tax_codes", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtVendor_SubrangeOnFocus() {
            textboxId = $('#<%= txtVendor_Subrange.ClientID%>').attr('ID');
            textboxRealId = "txtVendor_Subrange";
            AutoCompleteLookUpVendor();
        }

        function txtVendor_SubrangeTextChangeEvent() {
            CheckLookupVendor($('#<%= txtVendor_Subrange.ClientID%>').attr('ID'), "txtVendor_Subrange", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtLanguage_BatchInputFieldOnFocus() {
            textboxId = $('#<%= txtLanguage_BatchInputField.ClientID%>').attr('ID');
            textboxRealId = "txtLanguage_BatchInputField";
            AutoCompleteLookUpVendor();
        }

        function txtLanguage_BatchInputFieldTextChangeEvent() {
            CheckLookupVendor($('#<%= txtLanguage_BatchInputField.ClientID%>').attr('ID'), "txtLanguage_BatchInputField", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtPurchasing_OrganizationOnFocus() {
            textboxId = $('#<%= txtPurchasing_Organization.ClientID%>').attr('ID');
            textboxRealId = "txtPurchasing_Organization";
            AutoCompleteLookUpVendor();
        }

        function txtPurchasing_OrganizationTextChangeEvent() {
            CheckLookupVendor($('#<%= txtPurchasing_Organization.ClientID%>').attr('ID'), "txtPurchasing_Organization", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtPartner_counterOnFocus() {
            textboxId = $('#<%= txtPartner_counter.ClientID%>').attr('ID');
            textboxRealId = "txtPartner_counter";
            AutoCompleteLookUpVendor();
        }

        function txtPartner_counterTextChangeEvent() {
            CheckLookupVendor($('#<%= txtPartner_counter.ClientID%>').attr('ID'), "txtPartner_counter", $('#<%= btnNext.ClientID%>').attr('ID'));
        }



        function txtDate_Which_Record_CreatedOnFocus() {
            textboxId = $('#<%= txtDate_Which_Record_Created.ClientID%>').attr('ID');
            textboxRealId = "txtDate_Which_Record_Created";
            AutoCompleteLookUpVendor();
        }

        function txtDate_Which_Record_CreatedTextChangeEvent() {
            CheckLookupVendor($('#<%= txtDate_Which_Record_Created.ClientID%>').attr('ID'), "txtDate_Which_Record_Created", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtReference_vendorOnFocus() {
            textboxId = $('#<%= txtReference_vendor.ClientID%>').attr('ID');
            textboxRealId = "txtReference_vendor";
            AutoCompleteLookUpVendor();
        }

        function txtReference_vendorTextChangeEvent() {
            CheckLookupVendor($('#<%= txtReference_vendor.ClientID%>').attr('ID'), "txtReference_vendor", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtPreference_ZoneOnFocus() {
            textboxId = $('#<%= txtPreference_Zone.ClientID%>').attr('ID');
            textboxRealId = "txtPreference_Zone";
            AutoCompleteLookUpVendor();
        }

        function txtPreference_ZoneTextChangeEvent() {
            CheckLookupVendor($('#<%= txtPreference_Zone.ClientID%>').attr('ID'), "txtPreference_Zone", $('#<%= btnNext.ClientID%>').attr('ID'));

        }


        // onfocus="return txtPartner_AuthorityOnFocus();" onchange="return txtPartner_AuthorityChangeEvent();"
        var textboxId = "";
        var textboxRealId = "";
        function IsNumber() {
            if ((event.keyCode < 48) || (event.keyCode > 57))
                return false;
        }

    </script>
</asp:Content>
