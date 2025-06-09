<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Vendor/VendorMasterPage.master"
    AutoEventWireup="true" CodeFile="CompanyCodeData1.aspx.cs" Inherits="Transaction_Vendor_CompanyCodeData1" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetToolTip(control) {
            ddrivetip('30', control);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlAddNew" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">
                    Company Code Data
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
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
                                <td class="tdSpace" colspan="4" align="center" style="color: Red">
                                    ( Please do not enter 'NA' in case of not applicable for Non-Mandatory fields. )
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Reconciliation Account
                                    <asp:Label ID="labletxtReconAcc" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                                    in General Ledger
                                    <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtReconAcc')"
                                        onmouseout="hideddrivetip()" style="width: 16px; height: 16px; cursor: pointer;" />
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtReconAcc" runat="server" CssClass="textboxAutocomplete" Width="100px"
                                        TabIndex="1" MaxLength="10" onfocus="return ReconAccOnFocus();" onchange="return ReconAccTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtReconAcc" runat="server" ControlToValidate="txtReconAcc"
                                        ValidationGroup="CompanyCode" ErrorMessage="Reconciliation Account in General Ledger cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Reconciliation Account in General Ledger cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Vendor HO account number
                                    <asp:Label ID="labletxtHOAccNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtHOAccNo" runat="server" CssClass="textbox" Width="100px" TabIndex="2"
                                        MaxLength="10" />
                                    <asp:RequiredFieldValidator ID="reqtxtHOAccNo" runat="server" ControlToValidate="txtHOAccNo"
                                        ValidationGroup="CompanyCode" ErrorMessage="Vendor HO account number cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor HO account number cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    List of the Payment
                                    <asp:Label ID="lableddlPaymentMethod" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                                    Methods to be Considered
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:Label ID="lblListPaymentMethod" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblListPaymentMethod1" runat="server"></asp:Label>
                                    <%--<cc1:DropDownCheckBoxes ID="ddlPaymentMethod" runat="server" AddJQueryReference="false"
                                        RepeatLayout="Table" AutoPostBack="true" TabIndex="4" UseButtons="false" UseSelectAllNode="true"
                                        OnSelectedIndexChanged="ddlPaymentMethod_SelectedIndexChanged">
                                        <Style2 SelectBoxWidth="180" DropDownBoxBoxWidth="180" DropDownBoxBoxHeight="80" />
                                        <Texts SelectBoxCaption="--Select--" />
                                    </cc1:DropDownCheckBoxes>
                                    <cc1:ExtendedRequiredFieldValidator ID="reqddlPaymentMethod" runat="server" ControlToValidate="ddlPaymentMethod"
                                        ValidationGroup="CompanyCode" ErrorMessage="List of the Payment Methods to be Considered cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='List of the Payment Methods to be Considered cannot be blank.' />" />--%>
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    <%--Planning Group--%>
                                    Cash Mgmt. Group
                                    <asp:Label ID="lableddlPlanningGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:DropDownList ID="ddlPlanningGroup" runat="server" TabIndex="5">
                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqddlPlanningGroup" runat="server" ControlToValidate="ddlPlanningGroup"
                                        ValidationGroup="CompanyCode" ErrorMessage="Cash Mgmt. Group cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Cash Mgmt. Group cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Terms of Payment Key
                                    <asp:Label ID="lableddlTermPaymentKey" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtTermPaymentKey')"
                                        onmouseout="hideddrivetip()" style="width: 16px; height: 16px; cursor: pointer;" />
                                </td>
                                <td class="rigthTD" colspan="3">
                                    <asp:DropDownList ID="ddlTermPaymentKey" runat="server" TabIndex="3">
                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqddlTermPaymentKey" runat="server" ControlToValidate="ddlTermPaymentKey"
                                        ValidationGroup="CompanyCode" ErrorMessage="Terms of Payment Key cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Terms of Payment Key cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Personnel Number
                                    <asp:Label ID="labletxtPersonnelNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtPersonnelNo" runat="server" CssClass="textbox" Width="100px"
                                        TabIndex="6" MaxLength="8" />
                                    <asp:RequiredFieldValidator ID="reqtxtPersonnelNo" runat="server" ControlToValidate="txtPersonnelNo"
                                        ValidationGroup="CompanyCode" ErrorMessage="Personnel Number cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Personnel Number cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Previous Master Record Number
                                    <asp:Label ID="labletxtPreviousMasterNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtPreviousMasterNo')"
                                        onmouseout="hideddrivetip()" style="width: 16px; height: 16px; cursor: pointer;" />
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtPreviousMasterNo" runat="server" CssClass="textbox" Width="100px"
                                        TabIndex="7" MaxLength="10" />
                                    <asp:RequiredFieldValidator ID="reqtxtPreviousMasterNo" runat="server" ControlToValidate="txtPreviousMasterNo"
                                        ValidationGroup="CompanyCode" ErrorMessage="Previous Master Record Number cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Previous Master Record Number cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Account number of the alternative payee
                                    <asp:Label ID="labletxtAccNoAltPayee" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtAccNoAltPayee')"
                                        onmouseout="hideddrivetip()" style="width: 16px; height: 16px; cursor: pointer;" />
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtAccNoAltPayee" runat="server" CssClass="textbox" Width="100px"
                                        TabIndex="8" MaxLength="10" />
                                    <asp:RequiredFieldValidator ID="reqtxtAccNoAltPayee" runat="server" ControlToValidate="txtAccNoAltPayee"
                                        ValidationGroup="CompanyCode" ErrorMessage="Account number of the alternative payee cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Account number of the alternative payee cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Short Key for a House Bank
                                    <asp:Label ID="labletxtShortKeyHouseBank" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtShortKeyHouseBank" runat="server" CssClass="textbox" TabIndex="9"
                                        Width="100px" MaxLength="5" />
                                    <asp:RequiredFieldValidator ID="reqtxtShortKeyHouseBank" runat="server" ControlToValidate="txtShortKeyHouseBank"
                                        ValidationGroup="CompanyCode" ErrorMessage="Short Key for a House Bank cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Short Key for a House Bank cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Probable Time until Check is Paid
                                    <asp:Label ID="labletxtProbableTimeChequePaid" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtProbableTimeChequePaid" runat="server" CssClass="textbox" Width="100px"
                                        TabIndex="10" MaxLength="3" />
                                    <asp:RequiredFieldValidator ID="reqtxtProbableTimeChequePaid" runat="server" ControlToValidate="txtProbableTimeChequePaid"
                                        ValidationGroup="CompanyCode" ErrorMessage="Probable Time until Check is Paid cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Probable Time until Check is Paid cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Check Flag for Double Invoices or Credit Memos
                                    <asp:Label ID="lablechkIsDoubleInvoice" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:CheckBox ID="chkIsDoubleInvoice" runat="server" Text="Check if Relevant" TabIndex="11" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="leftTD" style="width: 20%">
                                    Key for sorting according to assignment numbers
                                    <asp:Label ID="labletxtKeySortAssignNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtKeySortAssignNo" runat="server" CssClass="textboxAutocomplete"
                                        TabIndex="12" Width="100px" MaxLength="3" onfocus="return KeySortAssignNoOnFocus();"
                                        onchange="return KeySortAssignNoTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtKeySortAssignNo" runat="server" ControlToValidate="txtKeySortAssignNo"
                                        ValidationGroup="CompanyCode" ErrorMessage="Key for sorting according to assignment numbers cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Key for sorting according to assignment numbers cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Authorization Group
                                    <asp:Label ID="labletxtAuthorizationgrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtAuthorizationgrp" runat="server" CssClass="textbox" Width="100px"
                                        TabIndex="13" MaxLength="4" />
                                    <asp:RequiredFieldValidator ID="reqtxtAuthorizationgrp" runat="server" ControlToValidate="txtAuthorizationgrp"
                                        ValidationGroup="CompanyCode" ErrorMessage="Authorization Group cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Authorization Group cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="leftTD" style="width: 20%">
                                    Tolerance group for the business partner/G/L account
                                    <asp:Label ID="labletxtToleranceGrpGL" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtToleranceGrpGL" runat="server" CssClass="textboxAutocomplete"
                                        TabIndex="14" Width="100px" MaxLength="4" onfocus="return ToleranceGrpGLOnFocus();"
                                        onchange="return ToleranceGrpGLTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtToleranceGrpGL" runat="server" ControlToValidate="txtToleranceGrpGL"
                                        ValidationGroup="CompanyCode" ErrorMessage="Tolerance group for the business partner/G/L account cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tolerance group for the business partner/G/L account cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Block Key for Payment
                                    <asp:Label ID="labletxtBlockKeyPayment" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtBlockKeyPayment" runat="server" CssClass="textboxAutocomplete"
                                        TabIndex="15" Width="100px" MaxLength="1" onfocus="return BlockKeyPaymentOnFocus();"
                                        onchange="return BlockKeyPaymentTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtBlockKeyPayment" runat="server" ControlToValidate="txtBlockKeyPayment"
                                        ValidationGroup="CompanyCode" ErrorMessage="Block Key for Payment cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Block Key for Payment cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="leftTD" style="width: 20%">
                                    Indicator: Pay all items separately ?
                                    <asp:Label ID="lablechkIsPayAllItemSeperately" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:CheckBox ID="chkIsPayAllItemSeperately" runat="server" Text="Check if Relevant"
                                        TabIndex="16" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Bill of exchange limit (in local curr.)
                                    <asp:Label ID="labletxtBillExchangeLimit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtBillExchangeLimit" runat="server" CssClass="textbox" Width="100px"
                                        TabIndex="17" MaxLength="3" />
                                    <asp:RequiredFieldValidator ID="reqtxtBillExchangeLimit" runat="server" ControlToValidate="txtBillExchangeLimit"
                                        ValidationGroup="CompanyCode" ErrorMessage="Bill of exchange limit (in local curr.) cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Bill of exchange limit (in local curr.) cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="leftTD" style="width: 20%">
                                    Clearing between customer and vendor?
                                    <asp:Label ID="lablechkIsClearingCustVend" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:CheckBox ID="chkIsClearingCustVend" runat="server" Text="Check if Relevant"
                                        TabIndex="18" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Deletion Flag for Master Record (Company Code Level)
                                    <asp:Label ID="lablechkIsMasterRecordDeleted" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:CheckBox ID="chkIsMasterRecordDeleted" runat="server" Text="Check if Relevant"
                                        TabIndex="19" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="leftTD" style="width: 20%">
                                    Key for Payment Grouping
                                    <asp:Label ID="labletxtKeyPaymentGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtKeyPaymentGrp" runat="server" CssClass="textboxAutocomplete"
                                        TabIndex="20" Width="100px" MaxLength="2" onfocus="return KeyPaymentGrpOnFocus();"
                                        onchange="return KeyPaymentGrpTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtKeyPaymentGrp" runat="server" ControlToValidate="txtKeyPaymentGrp"
                                        ValidationGroup="CompanyCode" ErrorMessage="Key for Payment Grouping cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Key for Payment Grouping cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Payment Method Supplement
                                    <asp:Label ID="labletxtPaymentMethodSupp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtPaymentMethodSupp" runat="server" CssClass="textboxAutocomplete"
                                        TabIndex="21" Width="100px" MaxLength="2" onfocus="return KeySortAssignNoOnFocus();"
                                        onchange="return KeySortAssignNoTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtPaymentMethodSupp" runat="server" ControlToValidate="txtPaymentMethodSupp"
                                        ValidationGroup="CompanyCode" ErrorMessage="Payment Method Supplement cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Payment Method Supplement cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="leftTD" style="width: 20%">
                                    Send Payment Advices by EDI
                                    <asp:Label ID="lablechkIsSendPaymentAdvicesEDI" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:CheckBox ID="chkIsSendPaymentAdvicesEDI" runat="server" Text="Check if Relevant"
                                        TabIndex="22" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Release Approval Group
                                    <asp:Label ID="labletxtReleaseApprovalGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtReleaseApprovalGrp" runat="server" CssClass="textboxAutocomplete"
                                        TabIndex="23" Width="100px" MaxLength="4" onfocus="return ReleaseApprovalGrpOnFocus();"
                                        onchange="return ReleaseApprovalGrpTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtReleaseApprovalGrp" runat="server" ControlToValidate="txtReleaseApprovalGrp"
                                        ValidationGroup="CompanyCode" ErrorMessage="Release Approval Group cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Release Approval Group cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="leftTD" style="width: 20%">
                                    Posting block for company code
                                    <asp:Label ID="lablechkIsPostingBlockCompanyCode" runat="server" ForeColor="Red"
                                        Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:CheckBox ID="chkIsPostingBlockCompanyCode" runat="server" Text="Check if Relevant"
                                        TabIndex="24" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Tolerance group; Invoice Verification
                                    <asp:Label ID="labletxtToleranceGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtToleranceGrp" runat="server" CssClass="textboxAutocomplete" Width="100px"
                                        TabIndex="25" MaxLength="4" onfocus="return ToleranceGrpOnFocus();" onchange="return ToleranceGrpTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtToleranceGrp" runat="server" ControlToValidate="txtToleranceGrp"
                                        ValidationGroup="CompanyCode" ErrorMessage="Tolerance group; Invoice Verification cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tolerance group; Invoice Verification cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="leftTD" style="width: 20%">
                                    Internet address of partner company clerk
                                    <asp:Label ID="labletxtInternetAddpartner" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtInternetAddpartner" runat="server" CssClass="textbox" Width="100px"
                                        TabIndex="26" MaxLength="130" />
                                    <asp:RequiredFieldValidator ID="reqtxtInternetAddpartner" runat="server" ControlToValidate="txtInternetAddpartner"
                                        ValidationGroup="CompanyCode" ErrorMessage="Internet address of partner company clerk cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Internet address of partner company clerk cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Payment Terms Key for Credit Memos
                                    <asp:Label ID="labletxtPaymentTermKeyCreditMeno" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtPaymentTermKeyCreditMeno" runat="server" CssClass="textboxAutocomplete"
                                        TabIndex="27" Width="100px" MaxLength="4" onfocus="return PaymentTermKeyCreditMenoOnFocus();"
                                        onchange="return PaymentTermKeyCreditMenoTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtPaymentTermKeyCreditMeno" runat="server" ControlToValidate="txtPaymentTermKeyCreditMeno"
                                        ValidationGroup="CompanyCode" ErrorMessage="Payment Terms Key for Credit Memos cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Payment Terms Key for Credit Memos cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="leftTD" style="width: 20%">
                                    Indicator for periodic account statements
                                    <asp:Label ID="lablechkIsPeriodicAccStmt" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:CheckBox ID="chkIsPeriodicAccStmt" runat="server" Text="Check if Relevant" TabIndex="28" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Date 1
                                    <asp:Label ID="labletxtDate1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtDate1" runat="server" CssClass="textbox" Width="100px" MaxLength="10"
                                        TabIndex="29" />
                                    <act:CalendarExtender ID="CaltxtDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate1" />
                                    <asp:RequiredFieldValidator ID="reqtxtDate1" runat="server" ControlToValidate="txtDate1"
                                        ValidationGroup="CompanyCode" ErrorMessage="Date 1 cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Reconciliation Account in General Ledger cannot be blank.' />" />
                                    <asp:RegularExpressionValidator ID="revDTTest" runat="server" ControlToValidate="txtDate1"
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
                                    Date 2
                                    <asp:Label ID="labletxtDate2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtDate2" runat="server" CssClass="textbox" Width="100px" MaxLength="10"
                                        TabIndex="30" />
                                    <act:CalendarExtender ID="CaltxtDate2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate2" />
                                    <asp:RequiredFieldValidator ID="reqtxtDate2" runat="server" ControlToValidate="txtDate2"
                                        ValidationGroup="CompanyCode" ErrorMessage="Date 2 cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Date 2 cannot be blank.' />" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDate2"
                                        ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                        Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                        ValidationGroup="CompanyCode" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Date 3
                                    <asp:Label ID="labletxtCertiDate" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtCertiDate" runat="server" CssClass="textbox" Width="100px" MaxLength="10"
                                        TabIndex="31" />
                                    <act:CalendarExtender ID="CaltxtCertiDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtCertiDate" />
                                    <asp:RequiredFieldValidator ID="reqtxtCertiDate" runat="server" ControlToValidate="txtCertiDate"
                                        ValidationGroup="CompanyCode" ErrorMessage="Date 3 cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Date 3 cannot be blank.' />" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCertiDate"
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
                                    Deletion bock for master record
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:CheckBox ID="chkIsBlockMasterRecordDeletion" runat="server" Text="Check if Relevant"
                                        TabIndex="32" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Prepayment Relevance (Vendor Master)
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:CheckBox ID="chkIsPrepaymentRelevant" runat="server" Text="Check if Relevant"
                                        TabIndex="33" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr id="trButton" runat="server" visible="false">
                                <td class="centerTD" colspan="4">
                                    <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="CompanyCode"
                                        UseSubmitBehavior="false" TabIndex="34" CssClass="button" OnClick="btnPrevious_Click" />
                                    <asp:Button ID="btnSave" runat="server" ValidationGroup="CompanyCode" Text="Save"
                                        UseSubmitBehavior="true" TabIndex="35" CssClass="button" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnNext" runat="server" ValidationGroup="CompanyCode" Text="Save & Next"
                                        UseSubmitBehavior="true" TabIndex="36" CssClass="button" OnClick="btnNext_Click"
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
    <asp:Label ID="lblSectionId" runat="server" Text="30" Visible="false" />
    <asp:Label ID="lblCompanyCodeId" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <script type="text/javascript">
        var textboxId = "";
        var textboxRealId = "";
        var ActionType = $('#<%= lblActionType.ClientID%>').html(); //  attr('Text');

        function ReconAccOnFocus() {
            textboxId = $('#<%= txtReconAcc.ClientID%>').attr('ID');
            textboxRealId = "txtReconAcc";
            AutoCompleteLookUpVendor();
        }

        function ReconAccTextChangeEvent() {
            CheckLookupVendor($('#<%= txtReconAcc.ClientID%>').attr('ID'), "txtReconAcc", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function KeySortAssignNoOnFocus() {
            textboxId = $('#<%= txtKeySortAssignNo.ClientID%>').attr('ID');
            textboxRealId = "txtKeySortAssignNo";
            AutoCompleteLookUpVendor();
        }

        function KeySortAssignNoTextChangeEvent() {
            CheckLookupVendor($('#<%= txtKeySortAssignNo.ClientID%>').attr('ID'), "txtKeySortAssignNo", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function ToleranceGrpGLOnFocus() {
            textboxId = $('#<%= txtToleranceGrpGL.ClientID%>').attr('ID');
            textboxRealId = "txtToleranceGrpGL";
            AutoCompleteLookUpVendor();
        }

        function ToleranceGrpGLTextChangeEvent() {
            CheckLookupVendor($('#<%= txtToleranceGrpGL.ClientID%>').attr('ID'), "txtToleranceGrpGL", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function ProbableTimeChequePaidOnFocus() {
            textboxId = $('#<%= txtProbableTimeChequePaid.ClientID%>').attr('ID');
            textboxRealId = "txtProbableTimeChequePaid";
            AutoCompleteLookUpVendor();
        }

        function ProbableTimeChequePaidTextChangeEvent() {
            CheckLookupVendor($('#<%= txtProbableTimeChequePaid.ClientID%>').attr('ID'), "txtProbableTimeChequePaid", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function BlockKeyPaymentOnFocus() {
            textboxId = $('#<%= txtBlockKeyPayment.ClientID%>').attr('ID');
            textboxRealId = "txtBlockKeyPayment";
            AutoCompleteLookUpVendor();
        }

        function BlockKeyPaymentTextChangeEvent() {
            CheckLookupVendor($('#<%= txtBlockKeyPayment.ClientID%>').attr('ID'), "txtBlockKeyPayment", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function AccNoAltPayeeOnFocus() {
            textboxId = $('#<%= txtAccNoAltPayee.ClientID%>').attr('ID');
            textboxRealId = "txtAccNoAltPayee";
            AutoCompleteLookUpVendor();
        }

        function AccNoAltPayeeTextChangeEvent() {
            CheckLookupVendor($('#<%= txtAccNoAltPayee.ClientID%>').attr('ID'), "txtAccNoAltPayee", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function ShortKeyHouseBankOnFocus() {
            textboxId = $('#<%= txtShortKeyHouseBank.ClientID%>').attr('ID');
            textboxRealId = "txtShortKeyHouseBank";
            AutoCompleteLookUpVendor();
        }

        function ShortKeyHouseBankTextChangeEvent() {
            CheckLookupVendor($('#<%= txtShortKeyHouseBank.ClientID%>').attr('ID'), "txtShortKeyHouseBank", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function KeyPaymentGrpOnFocus() {
            textboxId = $('#<%= txtKeyPaymentGrp.ClientID%>').attr('ID');
            textboxRealId = "txtKeyPaymentGrp";
            AutoCompleteLookUpVendor();
        }

        function KeyPaymentGrpTextChangeEvent() {
            CheckLookupVendor($('#<%= txtKeyPaymentGrp.ClientID%>').attr('ID'), "txtKeyPaymentGrp", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function PaymentMethodSuppOnFocus() {
            textboxId = $('#<%= txtPaymentMethodSupp.ClientID%>').attr('ID');
            textboxRealId = "txtPaymentMethodSupp";
            AutoCompleteLookUpVendor();
        }

        function PaymentMethodSuppTextChangeEvent() {
            CheckLookupVendor($('#<%= txtPaymentMethodSupp.ClientID%>').attr('ID'), "txtPaymentMethodSupp", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function ReleaseApprovalGrpOnFocus() {
            textboxId = $('#<%= txtReleaseApprovalGrp.ClientID%>').attr('ID');
            textboxRealId = "txtReleaseApprovalGrp";
            AutoCompleteLookUpVendor();
        }

        function ReleaseApprovalGrpTextChangeEvent() {
            CheckLookupVendor($('#<%= txtReleaseApprovalGrp.ClientID%>').attr('ID'), "txtReleaseApprovalGrp", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function ToleranceGrpOnFocus() {
            textboxId = $('#<%= txtToleranceGrp.ClientID%>').attr('ID');
            textboxRealId = "txtToleranceGrp";
            AutoCompleteLookUpVendor();
        }

        function ToleranceGrpTextChangeEvent() {
            CheckLookupVendor($('#<%= txtToleranceGrp.ClientID%>').attr('ID'), "txtToleranceGrp", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function PaymentTermKeyCreditMenoOnFocus() {
            textboxId = $('#<%= txtPaymentTermKeyCreditMeno.ClientID%>').attr('ID');
            textboxRealId = "txtPaymentTermKeyCreditMeno";
            AutoCompleteLookUpVendor();
        }

        function PaymentTermKeyCreditMenoTextChangeEvent() {
            CheckLookupVendor($('#<%= txtPaymentTermKeyCreditMeno.ClientID%>').attr('ID'), "txtPaymentTermKeyCreditMeno", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        


    </script>
</asp:Content>
