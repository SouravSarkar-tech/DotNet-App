<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Customer/CustomerMasterPage.master"
    AutoEventWireup="true" CodeFile="SalesArea2.aspx.cs" Inherits="Transaction_Customer_SalesArea2" %>

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
        <asp:UpdatePanel ID="UpdSalesData2" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Sales Area Data 2
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Sales Organization
                                    </td>
                                    <td class="rigthTD">
                                        <cc1:DropDownCheckBoxes ID="ddlSalesOrginization" runat="server" AddJQueryReference="false"
                                            TabIndex="1" UseButtons="false" UseSelectAllNode="true" AutoPostBack="false">
                                            <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="250" DropDownBoxBoxHeight="80" />
                                            <Texts SelectBoxCaption="--Select--" />
                                        </cc1:DropDownCheckBoxes>
                                    </td>
                                    <td class="leftTD">
                                        Distribution Channel
                                    </td>
                                    <td class="rigthTD">
                                        <cc1:DropDownCheckBoxes ID="ddlDistributionChannel" runat="server" AddJQueryReference="false"
                                            TabIndex="2" UseButtons="false" UseSelectAllNode="true" AutoPostBack="false">
                                            <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="250" DropDownBoxBoxHeight="80" />
                                            <Texts SelectBoxCaption="--Select--" />
                                        </cc1:DropDownCheckBoxes>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Division
                                    </td>
                                    <td class="rigthTD">
                                        <cc1:DropDownCheckBoxes ID="ddlDivision" runat="server" AddJQueryReference="false"
                                            TabIndex="3" UseButtons="false" UseSelectAllNode="true" AutoPostBack="false">
                                            <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="250" DropDownBoxBoxHeight="80" />
                                            <Texts SelectBoxCaption="--Select--" />
                                        </cc1:DropDownCheckBoxes>
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
                                        Billing block for customer (sales and distribution)
                                        <asp:Label ID="labletxtBilingBlockCust" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtBilingBlockCust" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="4" MaxLength="2" Width="180" onfocus="return txtBilingBlockCustOnFocus();"
                                            onchange="return txtBilingBlockCustTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtBilingBlockCust" runat="server" ControlToValidate="txtBilingBlockCust"
                                            ValidationGroup="salesarea" ErrorMessage="Billing block for customer (sales and distribution) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Title cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Partial delivery at item level
                                        <asp:Label ID="lablechkPartialItemLevel" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkPartialItemLevel" runat="server" Text="" TabIndex="5" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Delivery priority
                                        <asp:Label ID="labletxtDeliveryPriority" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtDeliveryPriority" runat="server" CssClass="textbox" MaxLength="2"
                                            TabIndex="6" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtDeliveryPriority" runat="server" ControlToValidate="txtDeliveryPriority"
                                            ValidationGroup="salesarea" ErrorMessage="Delivery priority (batch input) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='District cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtDeliveryPriority" ControlToValidate="txtDeliveryPriority"
                                            ValidationExpression="\d+" ValidationGroup="salesarea" Display="Static" EnableClientScript="true"
                                            ErrorMessage="numbers only" runat="server" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Exchange Rate Type
                                        <asp:Label ID="labletxtEXchangeRateTYpe" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtEXchangeRateTYpe" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="7" MaxLength="4" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtEXchangeRateTYpe" runat="server" ControlToValidate="txtEXchangeRateTYpe"
                                            ValidationGroup="salesarea" ErrorMessage="Exchange Rate Type cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 2 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Customer group 1
                                        <asp:Label ID="labletxtCustomerGroup1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCustomerGroup1" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="8" MaxLength="3" Width="180" onfocus="return txtCustomerGroup1OnFocus();"
                                            onchange="return txtCustomerGroup1TextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustomerGroup1" runat="server" ControlToValidate="txtCustomerGroup1"
                                            ValidationGroup="salesarea" ErrorMessage="Customer group cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 3 cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Customer group 2
                                        <asp:Label ID="labletxtCustomerGroup2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCustomerGroup2" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="9" MaxLength="3" Width="180" onfocus="return txtCustomerGroup2OnFocus();"
                                            onchange="return txtCustomerGroup2TextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustomerGroup2" runat="server" ControlToValidate="txtCustomerGroup2"
                                            ValidationGroup="salesarea" ErrorMessage="Customer group cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 4 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Customer group 3
                                        <asp:Label ID="labletxtCustomerGroup3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCustomerGroup3" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="10" MaxLength="3" Width="180" onfocus="return txtCustomerGroup3OnFocus();"
                                            onchange="return txtCustomerGroup3TextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustomerGroup3" runat="server" ControlToValidate="txtCustomerGroup3"
                                            ValidationGroup="salesarea" ErrorMessage="Customer group cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Sort field cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Customer group 4
                                        <asp:Label ID="labletxtCustomerGroup4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCustomerGroup4" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="11" MaxLength="3" Width="180" onfocus="return txtCustomerGroup4OnFocus();"
                                            onchange="return txtCustomerGroup4TextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustomerGroup4" runat="server" ControlToValidate="txtCustomerGroup4"
                                            ValidationGroup="salesarea" ErrorMessage="Customer group cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='House number and street cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Customer group 5
                                        <asp:Label ID="labletxtCustomerGroup5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCustomerGroup5" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="12" MaxLength="3" Width="180" onfocus="return txtCustomerGroup5OnFocus();"
                                            onchange="return txtCustomerGroup5TextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustomerGroup5" runat="server" ControlToValidate="txtCustomerGroup5"
                                            ValidationGroup="salesarea" ErrorMessage="Customer group 5 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Customer payment guarantee procedure
                                        <asp:Label ID="labletxtCustPayGuarantProc" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCustPayGuarantProc" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="13" MaxLength="4" Width="180" onfocus="return txtCustPayGuarantProcOnFocus();"
                                            onchange="return txtCustPayGuarantProcTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustPayGuarantProc" runat="server" ControlToValidate="txtCustPayGuarantProc"
                                            ValidationGroup="salesarea" ErrorMessage="Customer payment guarantee procedure cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='City cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Credit Control Area
                                        <asp:Label ID="labletxtCreditControlArea" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCreditControlArea" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="14" MaxLength="4" Width="180" onfocus="return txtCreditControlAreaOnFocus();"
                                            onchange="return txtCreditControlAreaTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtCreditControlArea" runat="server" ControlToValidate="txtCreditControlArea"
                                            ValidationGroup="salesarea" ErrorMessage="Credit Control Area cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Sales block for customer (sales area)
                                        <asp:Label ID="labletxtSalesBlockCust" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtSalesBlockCust" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="15" MaxLength="2" Width="180" onfocus="return txtSalesBlockCustOnFocus();"
                                            onchange="return txtSalesBlockCustTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtSalesBlockCust" runat="server" ControlToValidate="txtSalesBlockCust"
                                            ValidationGroup="salesarea" ErrorMessage="Sales block for customer (sales area) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='District cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Switch off rounding?
                                        <asp:Label ID="labletxtSwitchOffRound" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtSwitchOffRound" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="16" MaxLength="1" Width="180" onfocus="return txtSwitchOffRoundOnFocus();"
                                            onchange="return txtSwitchOffRoundTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtSwitchOffRound" runat="server" ControlToValidate="txtSwitchOffRound"
                                            ValidationGroup="salesarea" ErrorMessage="Switch off rounding? cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Customer classification (ABC analysis)
                                        <asp:Label ID="labletxtCustClassABC" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCustClassABC" runat="server" CssClass="textbox" MaxLength="2"
                                            TabIndex="17" Width="30" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustClassABC" runat="server" ControlToValidate="txtCustClassABC"
                                            ValidationGroup="salesarea" ErrorMessage="Customer classification (ABC analysis) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Country Key cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Tax category (sales tax, federal sales tax,...)
                                        <asp:Label ID="labletxtTaxCategory" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtTaxCategory" runat="server" CssClass="textboxAutocomplete" MaxLength="4"
                                            TabIndex="18" Width="100" onfocus="return txtTaxCategoryOnFocus();" onchange="return txtTaxCategoryTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtTaxCategory" runat="server" ControlToValidate="txtTaxCategory"
                                            ValidationGroup="salesarea" ErrorMessage="Tax category (sales tax, federal sales tax,...) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Region cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Tax classification for customer
                                        <asp:Label ID="labletxtTaxClassificationCust" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtTaxClassificationCust" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="19" MaxLength="1" Width="130px" onfocus="return txtTaxClassificationCustOnFocus();"
                                            onchange="return txtTaxClassificationCustTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtTaxClassificationCust" runat="server" ControlToValidate="txtTaxClassificationCust"
                                            ValidationGroup="salesarea" ErrorMessage="  Tax classification for customer cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Language Acc. to ISO 639 (Batch Input Field) cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        License number
                                        <asp:Label ID="labletxtLicenceNumber" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtLicenceNumber" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="20" MaxLength="15" Width="100" onfocus="return txtLicenceNumberOnFocus();"
                                            onchange="return txtLicenceNumberTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtLicenceNumber" runat="server" ControlToValidate="txtLicenceNumber"
                                            ValidationGroup="salesarea" ErrorMessage=" License number cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title=' First telephone number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Date (batch input)
                                        <asp:Label ID="labletxtDateBatchInput" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtDateBatchInput" runat="server" CssClass="textbox" MaxLength="8"
                                            TabIndex="21" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtDateBatchInput" runat="server" ControlToValidate="txtDateBatchInput"
                                            ValidationGroup="salesarea" ErrorMessage="Date (batch input) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Fax Number cannot be blank.' />" />
                                        <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateBatchInput"
                                            Format="dd/MM/yyyy">
                                        </ajax:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="revDTTest" runat="server" ControlToValidate="txtDateBatchInput"
                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                            ValidationGroup="salesarea" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Date (batch input)
                                        <asp:Label ID="labletxtDateBatchin2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtDateBatchin2" runat="server" CssClass="textbox" MaxLength="8"
                                            TabIndex="22" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtDateBatchin2" runat="server" ControlToValidate="txtDateBatchin2"
                                            ValidationGroup="salesarea" ErrorMessage="Date (batch input) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Second Telephone Number cannot be blank.' />" />
                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateBatchin2"
                                            Format="dd/MM/yyyy">
                                        </ajax:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDateBatchin2"
                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                            ValidationGroup="salesarea" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="leftTD">
                                        Confirmation for licenses
                                        <asp:Label ID="labletxtConfirmationLicenses" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtConfirmationLicenses" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="23" MaxLength="1" Width="180px" onfocus="return txtConfirmationLicensesOnFocus();"
                                            onchange="return txtConfirmationLicensesTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtConfirmationLicenses" runat="server" ControlToValidate="txtConfirmationLicenses"
                                            ValidationGroup="salesarea" ErrorMessage="Confirmation for licenses cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Account Number of Vendor or Creditor cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Order probability of the item (batch input)
                                        <asp:Label ID="labletxtOrderProbabilityitem" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtOrderProbabilityitem" runat="server" CssClass="textbox" MaxLength="3"
                                            TabIndex="24" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtOrderProbabilityitem" runat="server" ControlToValidate="txtOrderProbabilityitem"
                                            ValidationGroup="salesarea" ErrorMessage=" Order probability of the item (batch input) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Name 1 cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtOrderProbabilityitem"
                                            ValidationExpression="\d+" ValidationGroup="salesarea" Display="Static" EnableClientScript="true"
                                            ErrorMessage="numbers only" runat="server" />
                                    </td>
                                    <td class="leftTD">
                                        Maximum no.of permitted part.deliveries/item
                                        <asp:Label ID="labletxtMaxPermittedDeliveries" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMaxPermittedDeliveries" runat="server" CssClass="textbox" MaxLength="1"
                                            TabIndex="25" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtMaxPermittedDeliveries" runat="server" ControlToValidate="txtMaxPermittedDeliveries"
                                            ValidationGroup="salesarea" ErrorMessage="Maximum no.of permitted part.deliveries/item cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Maximum no.of permitted part.deliveries/item cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtDeliveryPriority"
                                            ValidationExpression="\d+" ValidationGroup="salesarea" Display="Static" SetFocusOnError="true"
                                            ForeColor="Red" EnableClientScript="true" ErrorMessage="numbers only" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Incoterms 1
                                        <asp:Label ID="lableddlIncotermsPart1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlIncotermsPart1" runat="server" TabIndex="26">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlIncotermsPart1" runat="server" ControlToValidate="ddlIncotermsPart1"
                                            ValidationGroup="salesarea" ErrorMessage="Incoterms (Part 1) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Incoterms (Part 1) cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Incoterms 2
                                        <asp:Label ID="labletxtIncotermsPart2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtIncotermsPart2" runat="server" CssClass="textbox" MaxLength="28"
                                            TabIndex="27" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtIncotermsPart2" runat="server" ControlToValidate="txtIncotermsPart2"
                                            ValidationGroup="salesarea" ErrorMessage="Incoterms (Part 2) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Group key cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Terms of Payment Key
                                        <asp:Label ID="lableddlTermPaymentKey" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <%--<asp:DropDownList ID="ddlTermPaymentKey" runat="server" TabIndex="2">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>--%>
                                        <ajax:ComboBox ID="ddlTermPaymentKey" runat="server" AutoPostBack="false" TabIndex="28"
                                            DropDownStyle="DropDownList" AutoCompleteMode="Suggest" CaseSensitive="False"
                                            CssClass="AjaxToolkitStyle">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </ajax:ComboBox>
                                        <asp:RequiredFieldValidator ID="reqddlTermPaymentKey" runat="server" ControlToValidate="ddlTermPaymentKey"
                                            ValidationGroup="salesarea" ErrorMessage="Terms of Payment Key cannot be blank."
                                            InitialValue="---Select---" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Terms of Payment Key cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Account assignment group for this customer
                                        <asp:Label ID="lableddlAccAssignmentCust" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlAccAssignmentCust" runat="server" TabIndex="29" OnSelectedIndexChanged="ddlAccAssignmentCust_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlAccAssignmentCust" runat="server" ControlToValidate="ddlAccAssignmentCust"
                                            ValidationGroup="salesarea" ErrorMessage="Account assignment group for this customer cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax Number 2 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Pricing procedure assigned to this customer
                                        <asp:Label ID="labletxtPricingProcuderAssCust" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtPricingProcuderAssCust" runat="server" CssClass="textbox" MaxLength="1"
                                            TabIndex="30" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtPricingProcuderAssCust" runat="server" ControlToValidate="txtPricingProcuderAssCust"
                                            ValidationGroup="salesarea" ErrorMessage="Pricing procedure assigned to this customer cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Customer Statistics Group
                                        <asp:Label ID="labletxtCustStatisticsGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCustStatisticsGroup" runat="server" CssClass="textbox" MaxLength="1"
                                            TabIndex="31" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqTxtCustStatisticsGroup" runat="server" ControlToValidate="txtCustStatisticsGroup"
                                            ValidationGroup="salesarea" ErrorMessage="Customer Statistics Group cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Country Key cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <%--TCSDT20072021 Start--%>
                                <tr>
                                     <td class="leftTD" style="width: 20%">
                                        Whether customer PY turnover is more than 10cr?
                                        <asp:Label ID="lblTCS" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlTCSYesNo" runat="server" TabIndex="29">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            <asp:ListItem Text="No - TCS Applicable" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Yes - TCS Not Applicable" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlTCSYesNo" runat="server" ControlToValidate="ddlTCSYesNo"
                                            ValidationGroup="salesarea" ErrorMessage=" Whether customer PY turnover is more than 10cr? cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Whether customer PY turnover is more than 10cr? cannot be blank.' />" />
                                     </td>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                              <%--TCSDT20072021 End--%> 
                                


                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr
                                <tr>
                                    <td class="leftTD">
                                        Order Combination Indicator
                                        <asp:Label ID="lablechkOrderCombinationIndi" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkOrderCombinationIndi" runat="server" Text="Check if Relevant"
                                            TabIndex="32" />
                                    </td>
                                    <td class="leftTD">
                                        Shipping Conditions
                                        <asp:Label ID="labletxtShippingCondition" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtShippingCondition" runat="server" CssClass="textbox" MaxLength="2"
                                            TabIndex="33" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtShippingCondition" runat="server" ControlToValidate="txtShippingCondition"
                                            ValidationGroup="salesarea" ErrorMessage="  Language Acc. to ISO 639 (Batch Input Field) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Language Acc. to ISO 639 (Batch Input Field) cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Complete delivery defined for each sales order?
                                        <asp:Label ID="labletxtCompleteDeliverySalesOrder" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtCompleteDeliverySalesOrder" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="34" MaxLength="1" Width="100px" onfocus="return txtCompleteDeliverySalesOrderOnFocus()"
                                            onchange="return txtCompleteDeliverySalesOrderTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtCompleteDeliverySalesOrder" runat="server"
                                            ControlToValidate="txtCompleteDeliverySalesOrder" ValidationGroup="salesarea"
                                            ErrorMessage="Complete delivery defined for each sales order?t cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Fax Number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Indicator: Customer Is Rebate-Relevant
                                        <asp:Label ID="labletxtIndiCustRebate" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtIndiCustRebate" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="35" MaxLength="1" Width="180" onfocus="return txtIndiCustRebateOnFocus();"
                                            onchange="return txtIndiCustRebateTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtIndiCustRebate" runat="server" ControlToValidate="txtIndiCustRebate"
                                            ValidationGroup="salesarea" ErrorMessage=" Indicator: Customer Is Rebate-Relevant cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Name 1 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Item proposal
                                        <asp:Label ID="labletxtItemProposal" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtItemProposal" runat="server" CssClass="textboxAutocomplete" MaxLength="10"
                                            TabIndex="36" Width="100" onfocus="return txtItemProposalOnFocus();" onchange="return txtItemProposalTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtItemProposal" runat="server" ControlToValidate="txtItemProposal"
                                            ValidationGroup="salesarea" ErrorMessage="Item proposal cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Sort field cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Customer group
                                        <asp:Label ID="labletxtCustomerGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCustomerGroup" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="37" MaxLength="2" Width="100" onfocus="return txtCustomerGroupOnFocus();"
                                            onchange="return txtCustomerGroupTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustomerGroup" runat="server" ControlToValidate="txtCustomerGroup"
                                            ValidationGroup="salesarea" ErrorMessage="Customer group cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='House number and street cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Price list type
                                        <asp:Label ID="labletxtPriceListType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtPriceListType" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="38" MaxLength="2" Width="100" onfocus="return txtPriceListTypeOnFocus();"
                                            onchange="return txtPriceListTypeTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtPriceListType" runat="server" ControlToValidate="txtPriceListType"
                                            ValidationGroup="salesarea" ErrorMessage="Price list type cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Shipper's (Our) Account Number at the Customer or Vendor
                                        <asp:Label ID="labletxtShipperAccountCustVendor" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtShipperAccountCustVendor" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="39" MaxLength="12" Width="100" onfocus="return txtShipperAccountCustVendorOnFocus();"
                                            onchange="return txtShipperAccountCustVendorTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtShipperAccountCustVendor" runat="server" ControlToValidate="txtShipperAccountCustVendor"
                                            ValidationGroup="salesarea" ErrorMessage="Shipper's (Our) Account Number at the Customer or Vendor cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Customer order block (sales area)
                                        <asp:Label ID="labletxtCustOrderBlock" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtCustOrderBlock" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="40" MaxLength="2" Width="100" onfocus="return txtCustOrderBlockOnFocus();"
                                            onchange="return txtCustOrderBlockTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustOrderBlock" runat="server" ControlToValidate="txtCustOrderBlock"
                                            ValidationGroup="salesarea" ErrorMessage=" Customer order block (sales area) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Country Code cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Customer delivery block (sales area)
                                        <asp:Label ID="labletxtCustDeliveryBlock" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtCustDeliveryBlock" runat="server" CssClass="textbox" MaxLength="2"
                                            TabIndex="41" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustDeliveryBlock" runat="server" ControlToValidate="txtCustDeliveryBlock"
                                            ValidationGroup="salesarea" ErrorMessage="Customer delivery block (sales area) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='City Code cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Authorization Group
                                        <asp:Label ID="labletxtAuthorizationGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtAuthorizationGroup" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="42" MaxLength="4" Width="100" onfocus="return txtAuthorizationGroupOnFocus();"
                                            onchange="return txtAuthorizationGroupTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtAuthorizationGroup" runat="server" ControlToValidate="txtAuthorizationGroup"
                                            ValidationGroup="salesarea" ErrorMessage=" Authorization Group cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Country Code cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Deletion flag for customer (sales level)
                                        <asp:Label ID="labletxtDeletionFlagCust" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtDeletionFlagCust" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="43" MaxLength="1" Width="100" onfocus="return txtDeletionFlagCustOnFocus();"
                                            onchange="return txtDeletionFlagCustTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtDeletionFlagCust" runat="server" ControlToValidate="txtDeletionFlagCust"
                                            ValidationGroup="salesarea" ErrorMessage="Deletion flag for customer (sales level) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax Number 2 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr  style="display: none">
                                    <td class="leftTD">
                                        Partner Function
                                        <asp:Label ID="lableddlPartnerFunction" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPartnerFunction" runat="server" TabIndex="44" AutoPostBack="true" 
                                            OnSelectedIndexChanged="ddlPartnerFunction_SelectedIndexChanged">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPartnerFunction" runat="server" ControlToValidate="ddlPartnerFunction"
                                            ValidationGroup="salesarea" ErrorMessage="Partner Function cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner Function cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Number of an SD business partner
                                        <asp:Label ID="labletxtNumberSDBusinPartner" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtNumberSDBusinPartner" runat="server" CssClass="textbox" MaxLength="10"
                                            TabIndex="45" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtNumberSDBusinPartner" runat="server" ControlToValidate="txtNumberSDBusinPartner"
                                            ValidationGroup="salesarea" ErrorMessage="Number of an SD business partner cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Company ID of Trading Partner cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr  style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr  style="display: none">
                                    <td class="leftTD">
                                        Partner Function
                                        <asp:Label ID="lableddlPartnerFunction2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPartnerFunction2" runat="server" TabIndex="46" OnSelectedIndexChanged="ddlPartnerFunction2_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPartnerFunction2" runat="server" ControlToValidate="ddlPartnerFunction2"
                                            ValidationGroup="salesarea" ErrorMessage="Partner Function cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner Function cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Number of an SD business partner
                                        <asp:Label ID="labletxtNumberSDBusinPartner2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtNumberSDBusinPartner2" runat="server" CssClass="textbox" MaxLength="10"
                                            TabIndex="47" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtNumberSDBusinPartner2" runat="server" ControlToValidate="txtNumberSDBusinPartner2"
                                            ValidationGroup="salesarea" ErrorMessage="Number of an SD business partner cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Company ID of Trading Partner cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr  style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr  style="display: none">
                                    <td class="leftTD">
                                        Partner Function
                                        <asp:Label ID="lableddlPartnerFunction3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPartnerFunction3" runat="server" TabIndex="48" OnSelectedIndexChanged="ddlPartnerFunction3_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPartnerFunction3" runat="server" ControlToValidate="ddlPartnerFunction3"
                                            ValidationGroup="salesarea" ErrorMessage="Partner Function cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner Function cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Number of an SD business partner
                                        <asp:Label ID="labletxtNumberSDBusinPartner3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtNumberSDBusinPartner3" runat="server" CssClass="textbox" MaxLength="10"
                                            TabIndex="49" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtNumberSDBusinPartner3" runat="server" ControlToValidate="txtNumberSDBusinPartner3"
                                            ValidationGroup="salesarea" ErrorMessage="Number of an SD business partner cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Company ID of Trading Partner cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr  style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr  style="display: none">
                                    <td class="leftTD">
                                        Partner Function
                                        <asp:Label ID="lableddlPartnerFunction4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPartnerFunction4" runat="server" TabIndex="50" OnSelectedIndexChanged="ddlPartnerFunction4_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPartnerFunction4" runat="server" ControlToValidate="ddlPartnerFunction4"
                                            ValidationGroup="salesarea" ErrorMessage="Partner Function cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner Function cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Number of an SD business partner
                                        <asp:Label ID="labletxtNumberSDBusinPartner4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtNumberSDBusinPartner4" runat="server" CssClass="textbox" MaxLength="10"
                                            TabIndex="51" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtNumberSDBusinPartner4" runat="server" ControlToValidate="txtNumberSDBusinPartner4"
                                            ValidationGroup="salesarea" ErrorMessage="Number of an SD business partner cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Company ID of Trading Partner cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr  style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr  style="display: none">
                                    <td class="leftTD">
                                        Partner Function
                                        <asp:Label ID="lableddlPartnerFunction5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPartnerFunction5" runat="server" TabIndex="52" OnSelectedIndexChanged="ddlPartnerFunction5_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPartnerFunction5" runat="server" ControlToValidate="ddlPartnerFunction5"
                                            ValidationGroup="salesarea" ErrorMessage="Partner Function cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner Function cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Number of an SD business partner
                                        <asp:Label ID="labletxtNumberSDBusinPartner5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtNumberSDBusinPartner5" runat="server" CssClass="textbox" MaxLength="10"
                                            TabIndex="53" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtNumberSDBusinPartner5" runat="server" ControlToValidate="txtNumberSDBusinPartner5"
                                            ValidationGroup="salesarea" ErrorMessage="Number of an SD business partner cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Company ID of Trading Partner cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr  style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr  style="display: none">
                                    <td class="leftTD">
                                        Partner Function
                                        <asp:Label ID="lableddlPartnerFunction6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPartnerFunction6" runat="server" TabIndex="54" OnSelectedIndexChanged="ddlPartnerFunction6_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPartnerFunction6" runat="server" ControlToValidate="ddlPartnerFunction6"
                                            ValidationGroup="salesarea" ErrorMessage="Partner Function cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner Function cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Number of an SD business partner
                                        <asp:Label ID="labletxtNumberSDBusinPartner6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtNumberSDBusinPartner6" runat="server" CssClass="textbox" MaxLength="10"
                                            TabIndex="55" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtNumberSDBusinPartner6" runat="server" ControlToValidate="txtNumberSDBusinPartner6"
                                            ValidationGroup="salesarea" ErrorMessage="Number of an SD business partner cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Company ID of Trading Partner cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Default Partner
                                        <asp:Label ID="labletxtDefaultPartner" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtDefaultPartner" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="56" MaxLength="1" Width="100" onfocus="return txtDefaultPartnerOnFocus();"
                                            onchange="return txtDefaultPartnerTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtDefaultPartner" runat="server" ControlToValidate="txtDefaultPartner"
                                            ValidationGroup="salesarea" ErrorMessage="Default Partner cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Group key cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Category indicator for tax codes
                                        <asp:Label ID="labletxtCateIndiTaxCodes" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtCateIndiTaxCodes" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="57" MaxLength="3" Width="180px" onfocus="return txtCateIndiTaxCodesOnFocus();"
                                            onchange="return txtCateIndiTaxCodesTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtCateIndiTaxCodes" runat="server" ControlToValidate="txtCateIndiTaxCodes"
                                            ValidationGroup="salesarea" ErrorMessage="Category indicator for tax codes cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax Number 1 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr id="trButton" runat="server" visible="false">
                                    <td class="centerTD" colspan="4">
                                        <asp:Button ID="btnPrevious" runat="server" ValidationGroup="salesarea" Text="Back"
                                            TabIndex="58" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="salesarea" Text="Save" CssClass="button"
                                            TabIndex="59" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnNext" runat="server" ValidationGroup="salesarea" Text="Save & Next"
                                            TabIndex="60" CssClass="button" OnClick="btnNext_Click" Width="120px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="salesarea" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblCustomerGeneralId" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="28" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <script type="text/javascript">

        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {

        });

        $(function () {

        });

        function BilingBlockCustOnFocus() {
            textboxId = $('#<%= txtBilingBlockCust.ClientID%>').attr('ID');
            textboxRealId = "txtBilingBlockCust";
            AutoCompleteLookUpHeaderC();
        }

        function BilingBlockCustTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtBilingBlockCust.ClientID%>').attr('ID'), "txtBilingBlockCust", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtEXchangeRateTYpeOnFocus() {
            textboxId = $('#<%= txtEXchangeRateTYpe.ClientID%>').attr('ID');
            textboxRealId = "txtEXchangeRateTYpe";
            AutoCompleteLookUpHeaderC();
        }

        function txtEXchangeRateTYpeTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtEXchangeRateTYpe.ClientID%>').attr('ID'), "txtEXchangeRateTYpe", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtIndiCustRebateOnFocus() {
            textboxId = $('#<%= txtIndiCustRebate.ClientID%>').attr('ID');
            textboxRealId = "txtIndiCustRebate";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndiCustRebateTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndiCustRebate.ClientID%>').attr('ID'), "txtIndiCustRebate", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtBilingBlockCustOnFocus() {
            textboxId = $('#<%= txtBilingBlockCust.ClientID%>').attr('ID');
            textboxRealId = "txtBilingBlockCust";
            AutoCompleteLookUpHeaderC();
        }

        function txtBilingBlockCustTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtBilingBlockCust.ClientID%>').attr('ID'), "txtBilingBlockCust", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtCustomerGroup1OnFocus() {
            textboxId = $('#<%= txtCustomerGroup1.ClientID%>').attr('ID');
            textboxRealId = "txtCustomerGroup1";
            AutoCompleteLookUpHeaderC();
        }

        function txtCustomerGroup1TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCustomerGroup1.ClientID%>').attr('ID'), "txtCustomerGroup1", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtCustomerGroup2OnFocus() {
            textboxId = $('#<%= txtCustomerGroup2.ClientID%>').attr('ID');
            textboxRealId = "txtCustomerGroup2";
            AutoCompleteLookUpHeaderC();
        }

        function txtCustomerGroup2TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCustomerGroup2.ClientID%>').attr('ID'), "txtCustomerGroup2", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCustomerGroup3OnFocus() {
            textboxId = $('#<%= txtCustomerGroup3.ClientID%>').attr('ID');
            textboxRealId = "txtCustomerGroup3";
            AutoCompleteLookUpHeaderC();
        }

        function txtCustomerGroup3TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCustomerGroup3.ClientID%>').attr('ID'), "txtCustomerGroup3", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCustomerGroup4OnFocus() {
            textboxId = $('#<%= txtCustomerGroup4.ClientID%>').attr('ID');
            textboxRealId = "txtCustomerGroup4";
            AutoCompleteLookUpHeaderC();
        }

        function txtCustomerGroup4TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCustomerGroup4.ClientID%>').attr('ID'), "txtCustomerGroup4", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCustomerGroup5OnFocus() {
            textboxId = $('#<%= txtCustomerGroup5.ClientID%>').attr('ID');
            textboxRealId = "txtCustomerGroup5";
            AutoCompleteLookUpHeaderC();
        }

        function txtCustomerGroup5TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCustomerGroup5.ClientID%>').attr('ID'), "txtCustomerGroup5", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCustPayGuarantProcOnFocus() {
            textboxId = $('#<%= txtCustPayGuarantProc.ClientID%>').attr('ID');
            textboxRealId = "txtCustPayGuarantProc";
            AutoCompleteLookUpHeaderC();
        }

        function txtCustPayGuarantProcTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCustPayGuarantProc.ClientID%>').attr('ID'), "txtCustPayGuarantProc", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCreditControlAreaOnFocus() {
            textboxId = $('#<%= txtCreditControlArea.ClientID%>').attr('ID');
            textboxRealId = "txtCreditControlArea";
            AutoCompleteLookUpHeaderC();
        }

        function txtCreditControlAreaTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCreditControlArea.ClientID%>').attr('ID'), "txtCreditControlArea", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtSalesBlockCustOnFocus() {
            textboxId = $('#<%= txtSalesBlockCust.ClientID%>').attr('ID');
            textboxRealId = "txtSalesBlockCust";
            AutoCompleteLookUpHeaderC();
        }

        function txtSalesBlockCustTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtSalesBlockCust.ClientID%>').attr('ID'), "txtSalesBlockCust", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtSwitchOffRoundOnFocus() {
            textboxId = $('#<%= txtSwitchOffRound.ClientID%>').attr('ID');
            textboxRealId = "txtSwitchOffRound";
            AutoCompleteLookUpHeaderC();
        }

        function txtSwitchOffRoundTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtSwitchOffRound.ClientID%>').attr('ID'), "txtSwitchOffRound", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtTaxCategoryOnFocus() {
            textboxId = $('#<%= txtTaxCategory.ClientID%>').attr('ID');
            textboxRealId = "txtTaxCategory";
            AutoCompleteLookUpHeaderC();
        }

        function txtTaxCategoryTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtTaxCategory.ClientID%>').attr('ID'), "txtSwitchOffRound", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtTaxClassificationCustOnFocus() {
            textboxId = $('#<%= txtTaxClassificationCust.ClientID%>').attr('ID');
            textboxRealId = "txtTaxClassificationCust";
            AutoCompleteLookUpHeaderC();
        }

        function txtTaxClassificationCustTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtTaxClassificationCust.ClientID%>').attr('ID'), "txtTaxClassificationCust", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtLicenceNumberOnFocus() {
            textboxId = $('#<%= txtLicenceNumber.ClientID%>').attr('ID');
            textboxRealId = "txtLicenceNumber";
            AutoCompleteLookUpHeaderC();
        }

        function txtLicenceNumberTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtLicenceNumber.ClientID%>').attr('ID'), "txtLicenceNumber", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtConfirmationLicensesOnFocus() {
            textboxId = $('#<%= txtConfirmationLicenses.ClientID%>').attr('ID');
            textboxRealId = "txtConfirmationLicenses";
            AutoCompleteLookUpHeaderC();
        }

        function txtConfirmationLicensesTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtConfirmationLicenses.ClientID%>').attr('ID'), "txtConfirmationLicenses", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtDefaultPartnerOnFocus() {
            textboxId = $('#<%= txtDefaultPartner.ClientID%>').attr('ID');
            textboxRealId = "txtDefaultPartner";
            AutoCompleteLookUpHeaderC();
        }

        function txtDefaultPartnerTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtDefaultPartner.ClientID%>').attr('ID'), "txtDefaultPartner", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
        function txtCateIndiTaxCodesOnFocus() {
            textboxId = $('#<%= txtCateIndiTaxCodes.ClientID%>').attr('ID');
            textboxRealId = "txtCateIndiTaxCodes";
            AutoCompleteLookUpHeaderC();
        }

        function txtCateIndiTaxCodesTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCateIndiTaxCodes.ClientID%>').attr('ID'), "txtCateIndiTaxCodes", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtItemProposalOnFocus() {
            textboxId = $('#<%= txtItemProposal.ClientID%>').attr('ID');
            textboxRealId = "txtItemProposal";
            AutoCompleteLookUpHeaderC();
        }

        function txtItemProposalTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtItemProposal.ClientID%>').attr('ID'), "txtItemProposal", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCustomerGroupOnFocus() {
            textboxId = $('#<%= txtCustomerGroup.ClientID%>').attr('ID');
            textboxRealId = "txtCustomerGroup";
            AutoCompleteLookUpHeaderC();
        }

        function txtCustomerGroupTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCustomerGroup.ClientID%>').attr('ID'), "txtCustomerGroup", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtShipperAccountCustVendorOnFocus() {
            textboxId = $('#<%= txtShipperAccountCustVendor.ClientID%>').attr('ID');
            textboxRealId = "txtShipperAccountCustVendor";
            AutoCompleteLookUpHeaderC();
        }

        function txtShipperAccountCustVendorTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtShipperAccountCustVendor.ClientID%>').attr('ID'), "txtShipperAccountCustVendor", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtPricingProcuderAssCustOnFocus() {
            textboxId = $('#<%= txtPricingProcuderAssCust.ClientID%>').attr('ID');
            textboxRealId = "txtPricingProcuderAssCust";
            AutoCompleteLookUpHeaderC();
        }

        function txtPricingProcuderAssCustTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtPricingProcuderAssCust.ClientID%>').attr('ID'), "txtPricingProcuderAssCust", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtPriceListTypeOnFocus() {
            textboxId = $('#<%= txtPriceListType.ClientID%>').attr('ID');
            textboxRealId = "txtPriceListType";
            AutoCompleteLookUpHeaderC();
        }

        function txtPriceListTypeTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtPriceListType.ClientID%>').attr('ID'), "txtPriceListType", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCompleteDeliverySalesOrderOnFocus() {
            textboxId = $('#<%= txtCompleteDeliverySalesOrder.ClientID%>').attr('ID');
            textboxRealId = "txtCompleteDeliverySalesOrder";
            AutoCompleteLookUpHeaderC();
        }

        function txtCompleteDeliverySalesOrderTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCompleteDeliverySalesOrder.ClientID%>').attr('ID'), "txtCompleteDeliverySalesOrder", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCustOrderBlockOnFocus() {
            textboxId = $('#<%= txtCustOrderBlock.ClientID%>').attr('ID');
            textboxRealId = "txtCustOrderBlock";
            AutoCompleteLookUpHeaderC();
        }

        function txtCustOrderBlockTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCustOrderBlock.ClientID%>').attr('ID'), "txtCustOrderBlock", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtAuthorizationGroupOnFocus() {
            textboxId = $('#<%= txtAuthorizationGroup.ClientID%>').attr('ID');
            textboxRealId = "txtAuthorizationGroup";
            AutoCompleteLookUpHeaderC();
        }

        function txtAuthorizationGroupTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtAuthorizationGroup.ClientID%>').attr('ID'), "txtAuthorizationGroup", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtDeletionFlagCustOnFocus() {
            textboxId = $('#<%= txtDeletionFlagCust.ClientID%>').attr('ID');
            textboxRealId = "txtDeletionFlagCust";
            AutoCompleteLookUpHeaderC();
        }

        function txtDeletionFlagCustTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtDeletionFlagCust.ClientID%>').attr('ID'), "txtDeletionFlagCust", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        // onfocus="return txtPartner_AuthorityOnFocus();" onchange="return txtPartner_AuthorityChangeEvent();"
        var textboxId = "";
        var textboxRealId = "";
        function IsNumber() {
            if ((event.keyCode < 48) || (event.keyCode > 57))
                return false;
        }
        //function MaterialTypeOnFocus() {
        ///        textboxId = $('#).attr('ID');
        //      textboxRealId = "txtMaterialType";
        //      AutoCompleteLookUpHeader();
        //   }

        //  function MaterialTypeTextChangeEvent() {
        //        CheckLookupHeader($(').attr('ID'), "txtMaterialType");
        //    }
    </script>
</asp:Content>
