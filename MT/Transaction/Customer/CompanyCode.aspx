<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Customer/CustomerMasterPage.master"
    AutoEventWireup="true" CodeFile="CompanyCode.aspx.cs" Inherits="Transaction_Customer_CompanyCode" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" cellpadding="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">
                    Company Code Data
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
                    <asp:Panel ID="pnlGrid" runat="server">
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" width="100%">
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Company Code
                                <asp:Label ID="lableddlCompany" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <cc1:DropDownCheckBoxes ID="ddlCompany" runat="server" AddJQueryReference="false"
                                    TabIndex="1" UseButtons="false" UseSelectAllNode="true" AutoPostBack="false">
                                    <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="250" DropDownBoxBoxHeight="80" />
                                    <Texts SelectBoxCaption="--Select--" />
                                </cc1:DropDownCheckBoxes>
                            </td>
                            <td class="tdSpace" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Reconciliation Account in General Ledger
                                <asp:Label ID="lableddlReconAcc" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlReconAcc" runat="server" TabIndex="2">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlReconAcc" runat="server" ControlToValidate="ddlReconAcc"
                                    ValidationGroup="CompanyCode" ErrorMessage="Reconciliation Account in General Ledger cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Reconciliation Account in General Ledger cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Cash Mgmt. Group
                                <asp:Label ID="lableddlPlanningGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlPlanningGroup" runat="server" TabIndex="3">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPlanningGroup" runat="server" ControlToValidate="ddlPlanningGroup"
                                    ValidationGroup="CompanyCode" ErrorMessage="Cash Mgmt. Group cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Cash Mgmt. Group cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Head office account number (in branch accounts)
                                <asp:Label ID="labletxtHeadOfficeAccNumber" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtHeadOfficeAccNumber" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="4" Width="100px" onfocus="return txtHeadOfficeAccNumberOnFocus();"
                                    onchange="return txtHeadOfficeAccNumberTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtHeadOfficeAccNumber" runat="server" ControlToValidate="txtHeadOfficeAccNumber"
                                    ValidationGroup="CompanyCode" ErrorMessage=" Head office account number (in branch accounts) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Key for sorting according to assignment numbers
                                <asp:Label ID="labletxtkeySortingAssignment" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtkeySortingAssignment" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="5" Width="100px" onfocus="return txtkeySortingAssignmentOnFocus();"
                                    onchange="return txtkeySortingAssignmentTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtkeySortingAssignment" runat="server" ControlToValidate="txtkeySortingAssignment"
                                    ValidationGroup="CompanyCode" ErrorMessage=" Key for sorting according to assignment numbers cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Indicator: Record Payment History ?
                                <asp:Label ID="lablechkIndicaterRecordPayHis" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkIndicaterRecordPayHis" runat="server" Text="Check if Relevant"
                                    TabIndex="6" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Terms of Payment Key
                                <asp:Label ID="lableddlTermPaymentKey" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <ajax:ComboBox ID="ddlTermPaymentKey" runat="server" AutoPostBack="false" TabIndex="7"
                                    DropDownStyle="DropDownList" AutoCompleteMode="Suggest" CaseSensitive="False"
                                    CssClass="AjaxToolkitStyle">
                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                </ajax:ComboBox>
                                <asp:RequiredFieldValidator ID="reqddlTermPaymentKey" runat="server" ControlToValidate="ddlTermPaymentKey"
                                    ValidationGroup="CompanyCode" ErrorMessage="Terms of Payment Key cannot be blank."
                                    InitialValue="---Select---" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Terms of Payment Key cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Authorization Group
                                <asp:Label ID="labletxtAuthorizationGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtAuthorizationGroup" runat="server" CssClass="textbox" MaxLength="4"
                                    TabIndex="8" Width="100" />
                                <asp:RequiredFieldValidator ID="reqtxtAuthorizationGroup" runat="server" ControlToValidate="txtAuthorizationGroup"
                                    ValidationGroup="save" ErrorMessage=" Authorization Group cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title=' Authorization Group cannot be blank.' />" />
                            </td>
                            <td class="tdSpace" colspan="2">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                List of the Payment Methods to be Considered
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
                                Block Key for Payment
                                <asp:Label ID="labletxtBlockKeyPayment" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtBlockKeyPayment" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="9" Width="100px" onfocus="return txtBlockKeyPaymentOnFocus();" onchange="return txtBlockKeyPaymentTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtBlockKeyPayment" runat="server" ControlToValidate="txtBlockKeyPayment"
                                    ValidationGroup="CompanyCode" ErrorMessage="Block Key for Payment cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Account number of an alternative payer
                                <asp:Label ID="labletxtAccNumberALterPlayer" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtAccNumberALterPlayer" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="10" Width="100px" onfocus="return txtAccNumberALterPlayerOnFocus();"
                                    onchange="return txtAccNumberALterPlayerTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtAccNumberALterPlayer" runat="server" ControlToValidate="txtAccNumberALterPlayer"
                                    ValidationGroup="CompanyCode" ErrorMessage="Account number of an alternative payer cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Short Key for a House Bank
                                <asp:Label ID="lableddlShortKeyBank" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlShortKeyBank" runat="server" TabIndex="11">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlShortKeyBank" runat="server" ControlToValidate="ddlShortKeyBank"
                                    ValidationGroup="CompanyCode" ErrorMessage="Short Key for a House Bank cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Short Key for a House Bank cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Indicator: Pay all items separately ?
                                <asp:Label ID="lablechkIndicatorPayAll" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkIndicatorPayAll" runat="server" Text="" TabIndex="12" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Indicator: Clearing between customer and vendor ?
                                <asp:Label ID="labletxtIndiClearingBetwCust" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtIndiClearingBetwCust" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="13" Width="100px" onfocus="return txtIndiClearingBetwCustOnFocus();"
                                    onchange="return txtIndiClearingBetwCustTextChangeEvent();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtIndiClearingBetwCust" runat="server" ControlToValidate="txtIndiClearingBetwCust"
                                    ValidationGroup="CompanyCode" ErrorMessage="Indicator: Clearing between customer and vendor ? cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Next payee
                                <asp:Label ID="labletxtNextPaye" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtNextPaye" runat="server" CssClass="textboxAutocomplete" Width="100px"
                                    TabIndex="14" onfocus="return txtNextPayeOnFocus();" onchange="return txtNextPayeTextChangeEvent();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtNextPaye" runat="server" ControlToValidate="txtNextPaye"
                                    ValidationGroup="CompanyCode" ErrorMessage="Next payee cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Accounting clerk
                                <asp:Label ID="labletxtAccountionClerk" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtAccountionClerk" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="15" Width="100px" onfocus="return txtAccountionClerkOnFocus();" onchange="return txtAccountionClerkTextChangeEvent();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtAccountionClerk" runat="server" ControlToValidate="txtAccountionClerk"
                                    ValidationGroup="CompanyCode" ErrorMessage=" Accounting clerk cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Indicator for periodic account statements
                                <asp:Label ID="lablechkindicatorPeriodicAccount" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkindicatorPeriodicAccount" runat="server" Text="" TabIndex="16" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Our account number at customer
                                <asp:Label ID="labletxtOurAccoCust" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtOurAccoCust" runat="server" CssClass="textboxAutocomplete" Width="100px"
                                    TabIndex="17" onfocus="return txtOurAccoCustOnFocus();" onchange="return txtOurAccoCustTextChangeEvent();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtOurAccoCust" runat="server" ControlToValidate="txtOurAccoCust"
                                    ValidationGroup="CompanyCode" ErrorMessage=" Our account number at customer cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Memo
                                <asp:Label ID="labletxtMemo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtMemo" runat="server" CssClass="textboxAutocomplete" Width="100px"
                                    TabIndex="18" onfocus="return txtMemoOnFocus();" onchange="return txtMemoTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtMemo" runat="server" ControlToValidate="txtMemo"
                                    ValidationGroup="CompanyCode" ErrorMessage=" Memo cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Indicator: Payment notice to customer (with cleared item)
                                <asp:Label ID="labletxtIndiPaymentNotice" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtIndiPaymentNotice" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="19" Width="100px" onfocus="return txtIndiPaymentNoticeOnFocus();" onchange="return txtIndiPaymentNoticeTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtIndiPaymentNotice" runat="server" ControlToValidate="txtIndiPaymentNotice"
                                    ValidationGroup="CompanyCode" ErrorMessage=" Indicator: Payment notice to customer (with cleared item) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Indicator: payment notice to sales department?
                                <asp:Label ID="labletxtIndiPayment" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtIndiPayment" runat="server" CssClass="textboxAutocomplete" Width="100px"
                                    TabIndex="20" onfocus="return txtIndiPaymentOnFocus();" onchange="return txtIndiPaymentTextChangeEvent();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtIndiPayment" runat="server" ControlToValidate="txtIndiPayment"
                                    ValidationGroup="CompanyCode" ErrorMessage="Indicator: payment notice to sales department? cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Indicator: payment notice to customer (w/o cleared items)
                                <asp:Label ID="labletxtIndipaymentWoCleared" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtIndipaymentWoCleared" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="21" Width="100px" onfocus="return txtIndipaymentWoClearedOnFocus();"
                                    onchange="return txtIndipaymentWoClearedTextChangeEvent();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtIndipaymentWoCleared" runat="server" ControlToValidate="txtIndipaymentWoCleared"
                                    ValidationGroup="CompanyCode" ErrorMessage="Indicator: payment notice to customer (w/o cleared items) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Indicator: Payment notice to the accounting department ?
                                <asp:Label ID="labletxtIndiPaymentAccountingDepart" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtIndiPaymentAccountingDepart" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="22" Width="100px" onfocus="return txtIndiPaymentAccountingDepartOnFocus();"
                                    onchange="return txtIndiPaymentAccountingDepartTextChangeEvent();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtIndiPaymentAccountingDepart" runat="server"
                                    ControlToValidate="txtIndiPaymentAccountingDepart" ValidationGroup="CompanyCode"
                                    ErrorMessage="Indicator: Payment notice to the accounting department ? cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Indicator: payment notice to legal department?
                                <asp:Label ID="labletxtIndiPaymentlegalDepartment" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtIndiPaymentlegalDepartment" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="23" Width="100px" onfocus="return txtIndiPaymentlegalDepartmentOnFocus();"
                                    onchange="return txtIndiPaymentlegalDepartmentTextChangeEvent();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtIndiPaymentlegalDepartment" runat="server"
                                    ControlToValidate="txtIndiPaymentlegalDepartment" ValidationGroup="CompanyCode"
                                    ErrorMessage=" Indicator: payment notice to legal department? cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Deletion Flag for Master Record (Company Code Level)
                                <asp:Label ID="labletxtDeletionFlagMasterRecord" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtDeletionFlagMasterRecord" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="24" Width="100px" onfocus="return txtDeletionFlagMasterRecordOnFocus();"
                                    onchange="return txtDeletionFlagMasterRecordTextChangeEvent();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtDeletionFlagMasterRecord" runat="server" ControlToValidate="txtDeletionFlagMasterRecord"
                                    ValidationGroup="CompanyCode" ErrorMessage=" Deletion Flag for Master Record (Company Code Level) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Posting block for company code
                                <asp:Label ID="labletxtPostingBlockCompanyCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtPostingBlockCompanyCode" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="25" Width="100px" onfocus="return txtPostingBlockCompanyCodeOnFocus();"
                                    onchange="return txtPostingBlockCompanyCodeTextChangeEvent();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtPostingBlockCompanyCode" runat="server" ControlToValidate="txtPostingBlockCompanyCode"
                                    ValidationGroup="CompanyCode" ErrorMessage="Posting block for company code cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Previous Master Record Number
                                <asp:Label ID="labletxtPreviousRecordNumber" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtPreviousRecordNumber" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="26" Width="100px" onfocus="return txtPreviousRecordNumberOnFocus();"
                                    onchange="return txtPreviousRecordNumberTextChangeEvent();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtPreviousRecordNumber" runat="server" ControlToValidate="txtPreviousRecordNumber"
                                    ValidationGroup="CompanyCode" ErrorMessage=" Previous Master Record Number cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Key for Payment Grouping
                                <asp:Label ID="labletxtKeyPaymentGrouping" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtKeyPaymentGrouping" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="27" Width="100px" onfocus="return txtKeyPaymentGroupingOnFocus();"
                                    onchange="return txtKeyPaymentGroupingTextChangeEvent();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtKeyPaymentGrouping" runat="server" ControlToValidate="txtKeyPaymentGrouping"
                                    ValidationGroup="CompanyCode" ErrorMessage="Key for Payment Grouping cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Payment Terms Key for Credit Memos
                                <asp:Label ID="labletxtPaymentTermCreditMemos" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtPaymentTermCreditMemos" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="28" Width="100px" onfocus="return txtPaymentTermCreditMemosOnFocus();"
                                    onchange="return txtPaymentTermCreditMemosTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtPaymentTermCreditMemos" runat="server" ControlToValidate="txtPaymentTermCreditMemos"
                                    ValidationGroup="CompanyCode" ErrorMessage="Payment Terms Key for Credit Memos cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Withholding Tax Country Key
                                <asp:Label ID="labletxtWithholdingTaxCountry" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtWithholdingTaxCountry" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="29" Width="100px" onfocus="return txtWithholdingTaxCountryOnFocus();"
                                    onchange="return txtWithholdingTaxCountryTextChangeEvent();"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtWithholdingTaxCountry" runat="server" ControlToValidate="txtWithholdingTaxCountry"
                                    ValidationGroup="CompanyCode" ErrorMessage=" Withholding Tax Country Key cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Indicator for withholding tax type
                                <asp:Label ID="labletxtIndiForWithHoldingTaxType" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtIndiForWithHoldingTaxType" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="30" Width="100px" onfocus="return txtIndiForWithHoldingTaxTypeOnFocus();"
                                    onchange="return txtIndiForWithHoldingTaxTypeTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtIndiForWithHoldingTaxType" runat="server" ControlToValidate="txtIndiForWithHoldingTaxType"
                                    ValidationGroup="CompanyCode" ErrorMessage=" Indicator for withholding tax type cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Withholding tax code
                                <asp:Label ID="labletxtWithholdingTaxCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtWithholdingTaxCode" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="31" Width="100px" onfocus="return txtWithholdingTaxCodeOnFocus();"
                                    onchange="return txtWithholdingTaxCodeTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtWithholdingTaxCode" runat="server" ControlToValidate="txtWithholdingTaxCode"
                                    ValidationGroup="CompanyCode" ErrorMessage=" Withholding tax code cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Withholding tax identification number
                                <asp:Label ID="labletxtWitHoldingTaxIdenNumb" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtWitHoldingTaxIdenNumb" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="32" Width="100px" onfocus="return txtWitHoldingTaxIdenNumbOnFocus();"
                                    onchange="return txtWitHoldingTaxIdenNumbTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtWitHoldingTaxIdenNumb" runat="server" ControlToValidate="txtWitHoldingTaxIdenNumb"
                                    ValidationGroup="CompanyCode" ErrorMessage="Withholding tax identification number cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Tolerance group for the business partner/G/L account
                                <asp:Label ID="labletxtToleranceGroupBussAcc" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtToleranceGroupBussAcc" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="33" Width="100px" onfocus="return txtToleranceGroupBussAccOnFocus();"
                                    onchange="return txtToleranceGroupBussAccTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtToleranceGroupBussAcc" runat="server" ControlToValidate="txtToleranceGroupBussAcc"
                                    ValidationGroup="CompanyCode" ErrorMessage=" Tolerance group for the business partner/G/L account cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Category cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnPrevious" runat="server" ValidationGroup="CompanyCode" Text="Back"
                                    TabIndex="34" CssClass="button" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="CompanyCode" Text="Save"
                                    TabIndex="35" CssClass="button" OnClick="btnSave_Click" />
                                <asp:Button ID="btnNext" runat="server" ValidationGroup="CompanyCode" Text="Save & Next"
                                    TabIndex="36" CssClass="button" OnClick="btnNext_Click" Width="120px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="CompanyCode" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblAccountingId" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblCustomerGeneralId" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="23" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <script type="text/javascript">

        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {

        });

        $(function () {

        });

        function WithholdingTaxCodeOnFocus() {
            textboxId = $('#<%= txtWithholdingTaxCode.ClientID%>').attr('ID');
            textboxRealId = "txtWithholdingTaxCode";
            AutoCompleteLookUpHeaderC();
        }

        function WithholdingTaxCodeTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtWithholdingTaxCode.ClientID%>').attr('ID'), "txtWithholdingTaxCode", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtWitHoldingTaxIdenNumbOnFocus() {
            textboxId = $('#<%= txtWitHoldingTaxIdenNumb.ClientID%>').attr('ID');
            textboxRealId = "txtWitHoldingTaxIdenNumb";
            AutoCompleteLookUpHeaderC();
        }

        function txtWitHoldingTaxIdenNumbTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtWitHoldingTaxIdenNumb.ClientID%>').attr('ID'), "txtWitHoldingTaxIdenNumb", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtIndiForWithHoldingTaxTypeOnFocus() {
            textboxId = $('#<%= txtIndiForWithHoldingTaxType.ClientID%>').attr('ID');
            textboxRealId = "txtIndiForWithHoldingTaxType";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndiForWithHoldingTaxTypeTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndiForWithHoldingTaxType.ClientID%>').attr('ID'), "txtIndiForWithHoldingTaxType", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtWithholdingTaxCodeOnFocus() {
            textboxId = $('#<%= txtWithholdingTaxCode.ClientID%>').attr('ID');
            textboxRealId = "txtWithholdingTaxCode";
            AutoCompleteLookUpHeaderC();
        }

        function txtWithholdingTaxCodeTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtWithholdingTaxCode.ClientID%>').attr('ID'), "txtWithholdingTaxCode", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtPaymentTermCreditMemosOnFocus() {
            textboxId = $('#<%= txtPaymentTermCreditMemos.ClientID%>').attr('ID');
            textboxRealId = "txtPaymentTermCreditMemos";
            AutoCompleteLookUpHeaderC();
        }

        function txtPaymentTermCreditMemosTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtPaymentTermCreditMemos.ClientID%>').attr('ID'), "txtPaymentTermCreditMemos", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtWithholdingTaxCountryOnFocus() {
            textboxId = $('#<%= txtWithholdingTaxCountry.ClientID%>').attr('ID');
            textboxRealId = "txtWithholdingTaxCountry";
            AutoCompleteLookUpHeaderC();
        }

        function txtWithholdingTaxCountryTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtWithholdingTaxCountry.ClientID%>').attr('ID'), "txtWithholdingTaxCountry", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtIndiPaymentNoticeOnFocus() {
            textboxId = $('#<%= txtIndiPaymentNotice.ClientID%>').attr('ID');
            textboxRealId = "txtIndiPaymentNotice";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndiPaymentNoticeTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndiPaymentNotice.ClientID%>').attr('ID'), "txtIndiPaymentNotice", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtIndiPaymentOnFocus() {
            textboxId = $('#<%= txtIndiPayment.ClientID%>').attr('ID');
            textboxRealId = "txtIndiPayment";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndiPaymentTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndiPayment.ClientID%>').attr('ID'), "txtIndiPayment", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtIndipaymentWoClearedOnFocus() {
            textboxId = $('#<%= txtIndipaymentWoCleared.ClientID%>').attr('ID');
            textboxRealId = "txtIndipaymentWoCleared";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndipaymentWoClearedTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndipaymentWoCleared.ClientID%>').attr('ID'), "txtIndipaymentWoCleared", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtIndiPaymentAccountingDepartOnFocus() {
            textboxId = $('#<%= txtIndiPaymentAccountingDepart.ClientID%>').attr('ID');
            textboxRealId = "txtIndiPaymentAccountingDepart";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndiPaymentAccountingDepartTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndiPaymentAccountingDepart.ClientID%>').attr('ID'), "txtIndiPaymentAccountingDepart", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtIndiPaymentlegalDepartmentOnFocus() {
            textboxId = $('#<%= txtIndiPaymentlegalDepartment.ClientID%>').attr('ID');
            textboxRealId = "txtIndiPaymentlegalDepartment";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndiPaymentlegalDepartmentTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndiPaymentlegalDepartment.ClientID%>').attr('ID'), "txtIndiPaymentlegalDepartment", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtDeletionFlagMasterRecordOnFocus() {
            textboxId = $('#<%= txtDeletionFlagMasterRecord.ClientID%>').attr('ID');
            textboxRealId = "txtDeletionFlagMasterRecord";
            AutoCompleteLookUpHeaderC();
        }

        function txtDeletionFlagMasterRecordTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtDeletionFlagMasterRecord.ClientID%>').attr('ID'), "txtDeletionFlagMasterRecord", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtPostingBlockCompanyCodeOnFocus() {
            textboxId = $('#<%= txtPostingBlockCompanyCode.ClientID%>').attr('ID');
            textboxRealId = "txtPostingBlockCompanyCode";
            AutoCompleteLookUpHeaderC();
        }

        function txtPostingBlockCompanyCodeTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtPostingBlockCompanyCode.ClientID%>').attr('ID'), "txtPostingBlockCompanyCode", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtPreviousRecordNumberOnFocus() {
            textboxId = $('#<%= txtPreviousRecordNumber.ClientID%>').attr('ID');
            textboxRealId = "txtPreviousRecordNumber";
            AutoCompleteLookUpHeaderC();
        }

        function txtPreviousRecordNumberTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtPreviousRecordNumber.ClientID%>').attr('ID'), "txtPreviousRecordNumber", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtKeyPaymentGroupingOnFocus() {
            textboxId = $('#<%= txtKeyPaymentGrouping.ClientID%>').attr('ID');
            textboxRealId = "txtKeyPaymentGrouping";
            AutoCompleteLookUpHeaderC();
        }

        function txtKeyPaymentGroupingTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtKeyPaymentGrouping.ClientID%>').attr('ID'), "txtKeyPaymentGrouping", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtIndiClearingBetwCustOnFocus() {
            textboxId = $('#<%= txtIndiClearingBetwCust.ClientID%>').attr('ID');
            textboxRealId = "txtIndiClearingBetwCust";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndiClearingBetwCustTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndiClearingBetwCust.ClientID%>').attr('ID'), "txtIndiClearingBetwCust", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtNextPayeOnFocus() {
            textboxId = $('#<%= txtNextPaye.ClientID%>').attr('ID');
            textboxRealId = "txtNextPaye";
            AutoCompleteLookUpHeaderC();
        }

        function txtNextPayeTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtNextPaye.ClientID%>').attr('ID'), "txtNextPaye", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtAccountionClerkOnFocus() {
            textboxId = $('#<%= txtAccountionClerk.ClientID%>').attr('ID');
            textboxRealId = "txtAccountionClerk";
            AutoCompleteLookUpHeaderC();
        }

        function txtAccountionClerkTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtAccountionClerk.ClientID%>').attr('ID'), "txtAccountionClerk", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtOurAccoCustOnFocus() {
            textboxId = $('#<%= txtOurAccoCust.ClientID%>').attr('ID');
            textboxRealId = "txtOurAccoCust";
            AutoCompleteLookUpHeaderC();
        }

        function txtOurAccoCustTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtOurAccoCust.ClientID%>').attr('ID'), "txtOurAccoCust", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtMemoOnFocus() {
            textboxId = $('#<%= txtMemo.ClientID%>').attr('ID');
            textboxRealId = "txtMemo";
            AutoCompleteLookUpHeaderC();
        }

        function txtMemoTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtMemo.ClientID%>').attr('ID'), "txtMemo", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtBlockKeyPaymentOnFocus() {
            textboxId = $('#<%= txtBlockKeyPayment.ClientID%>').attr('ID');
            textboxRealId = "txtBlockKeyPayment";
            AutoCompleteLookUpHeaderC();
        }

        function txtBlockKeyPaymentTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtBlockKeyPayment.ClientID%>').attr('ID'), "txtBlockKeyPayment", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtAccNumberALterPlayerOnFocus() {
            textboxId = $('#<%= txtAccNumberALterPlayer.ClientID%>').attr('ID');
            textboxRealId = "txtAccNumberALterPlayer";
            AutoCompleteLookUpHeaderC();
        }

        function txtAccNumberALterPlayerTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtAccNumberALterPlayer.ClientID%>').attr('ID'), "txtAccNumberALterPlayer", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtToleranceGroupBussAccOnFocus() {
            textboxId = $('#<%= txtToleranceGroupBussAcc.ClientID%>').attr('ID');
            textboxRealId = "txtToleranceGroupBussAcc";
            AutoCompleteLookUpHeaderC();
        }

        function txtToleranceGroupBussAccTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtToleranceGroupBussAcc.ClientID%>').attr('ID'), "txtToleranceGroupBussAcc", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtkeySortingAssignmentOnFocus() {
            textboxId = $('#<%= txtkeySortingAssignment.ClientID%>').attr('ID');
            textboxRealId = "txtkeySortingAssignment";
            AutoCompleteLookUpHeaderC();
        }

        function txtkeySortingAssignmentTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtkeySortingAssignment.ClientID%>').attr('ID'), "txtkeySortingAssignment", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtHeadOfficeAccNumberOnFocus() {
            textboxId = $('#<%= txtHeadOfficeAccNumber.ClientID%>').attr('ID');
            textboxRealId = "txtHeadOfficeAccNumber";
            AutoCompleteLookUpHeaderC();
        }

        function txtHeadOfficeAccNumberTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtHeadOfficeAccNumber.ClientID%>').attr('ID'), "txtHeadOfficeAccNumber", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        var textboxId = "";
        var textboxRealId = "";
        
    </script>
</asp:Content>
