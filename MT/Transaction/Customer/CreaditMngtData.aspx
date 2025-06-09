<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Customer/CustomerMasterPage.master"
    AutoEventWireup="true" CodeFile="CreaditMngtData.aspx.cs" Inherits="Transaction_Customer_CreaditMngtData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="4">
                    Credit Management Data
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
                                Total Amount
                                <asp:Label ID="labletxtTotalAmt" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="textbox" MaxLength="17" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtTotalAmt" runat="server" ControlToValidate="txtTotalAmt"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage="Total Amount cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Total Amount cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Individual Limit
                                <asp:Label ID="labletxtIndividualLimit" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" colspan=" 3">
                                <asp:TextBox ID="txtIndividualLimit" runat="server" CssClass="textbox" MaxLength="17"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtIndividualLimit" runat="server" ControlToValidate="txtIndividualLimit"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage="Individual Limit cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Individual Limit cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Customer's account number with credit limit reference
                                <asp:Label ID="labletxtCustomer_Acc_No" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCustomer_Acc_No" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="10" onfocus="return txtCustomer_Acc_NoOnFocus();" onchange="return txtCustomer_Acc_NoTextChangeEvent();"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtCustomer_Acc_No" runat="server" ControlToValidate="txtCustomer_Acc_No"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage="Customer's account number with credit limit reference cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer's account number with credit limit reference cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Credit Control Data
                                <asp:Label ID="lableddlCredit_Control" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" colspan=" 3">
                                <asp:DropDownList ID="ddlCredit_Control" runat="server">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCreditControl" runat="server" ControlToValidate="ddlCredit_Control"
                                    ValidationGroup="next" ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic"
                                    Text="<img src='../images/Error.png' title='Select Module.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Date (batch input)
                                <asp:Label ID="labletxtDate_Batch" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtDate_Batch" runat="server" CssClass="textbox" MaxLength="10"
                                    Width="100" />
                                <asp:RequiredFieldValidator ID="reqtxtDate_Batch" runat="server" ControlToValidate="txtDate_Batch"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage="Date (batch input) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Date (batch input) cannot be blank.' />" />
                                <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate_Batch"
                                    Format="dd/MM/yyyy">
                                </ajax:CalendarExtender>
                                <asp:RegularExpressionValidator ID="revDTTest" runat="server" ControlToValidate="txtDate_Batch"
                                    ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                    Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                    ValidationGroup="CrdtMgmtData" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Indicator: Blocked by credit management ?
                                <asp:Label ID="labletxtIndicator_Blocked" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtIndicator_Blocked" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="1" onfocus="return txtIndicator_BlockedOnFocus();" onchange="return txtIndicator_BlockedTextChangeEvent();"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtIndicator_Blocked" runat="server" ControlToValidate="txtIndicator_Blocked"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage="Indicator: Blocked by credit management ? cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Indicator: Blocked by credit management ? cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Credit representative group for credit management
                                <asp:Label ID="labletxtCredit_Group" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCredit_Group" runat="server" CssClass="textboxAutocomplete" MaxLength="3"
                                    onfocus="return txtCredit_GroupOnFocus();" onchange="return txtCredit_GroupTextChangeEvent();"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtCredit_Group" runat="server" ControlToValidate="txtCredit_Group"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage=" Credit representative group for credit management cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Credit representative group for credit management cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Date (batch input)
                                <asp:Label ID="labletxtDate_Batch1" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtDate_Batch1" runat="server" CssClass="textbox" MaxLength="10"
                                    Width="100" />
                                <asp:RequiredFieldValidator ID="reqtxtDate_Batch1" runat="server" ControlToValidate="txtDate_Batch1"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage="Date (batch input) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Date (batch input) cannot be blank.' />" />
                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate_Batch1"
                                    Format="dd/MM/yyyy">
                                </ajax:CalendarExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDate_Batch1"
                                    ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                    Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                    ValidationGroup="CrdtMgmtData" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Credit information number
                                <asp:Label ID="labletxtCredit_info_number" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCredit_info_number" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="11" onfocus="return txtCredit_info_numberOnFocus();" onchange="return txtCredit_info_numberTextChangeEvent();"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtCredit_info_number" runat="server" ControlToValidate="txtCredit_info_number"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage="Credit information number cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Credit information number cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Date (batch input)
                                <asp:Label ID="labletxtDate_Batch2" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtDate_Batch2" runat="server" CssClass="textbox" MaxLength="10"
                                    Width="100" />
                                <asp:RequiredFieldValidator ID="reqtxtDate_Batch2" runat="server" ControlToValidate="txtDate_Batch2"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage="Date (batch input) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Date (batch input) cannot be blank.' />" />
                                <ajax:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDate_Batch2"
                                    Format="dd/MM/yyyy">
                                </ajax:CalendarExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDate_Batch2"
                                    ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                    Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                    ValidationGroup="CrdtMgmtData" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Customer Credit Group
                                <asp:Label ID="labletxtCust_Credit_Group" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtCust_Credit_Group" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="4" Width="100" onfocus="return txtCust_Credit_GroupOnFocus();" onchange="return txtCust_Credit_GroupTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtCust_Credit_Group" runat="server" ControlToValidate="txtCust_Credit_Group"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage="Customer Credit Group cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Credit Group cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Date (batch input)
                                <asp:Label ID="labletxtDate_Batch3" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtDate_Batch3" runat="server" CssClass="textbox" MaxLength="10"
                                    Width="100px" />
                                <asp:RequiredFieldValidator ID="reqtxtDate_Batch3" runat="server" ControlToValidate="txtDate_Batch3"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage="Date (batch input) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Date (batch input) cannot be blank.' />" />
                                <ajax:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDate_Batch3"
                                    Format="dd/MM/yyyy">
                                </ajax:CalendarExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDate_Batch3"
                                    ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                    Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                    ValidationGroup="CrdtMgmtData" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Customer Group
                                <asp:Label ID="labletxtCustomer_Group" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtCustomer_Group" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="8" Width="100" onfocus="return txtCustomer_GroupOnFocus();" onchange="return txtCustomer_GroupTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtCustomer_Group" runat="server" ControlToValidate="txtCustomer_Group"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage="Customer Group cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Group cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Recommended credit limit (Batch input field)
                                <asp:Label ID="labletxtReco_credit_limit" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtReco_credit_limit" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="20" onfocus="return txtReco_credit_limitOnFocus();" onchange="return txtReco_credit_limitTextChangeEvent();"
                                    Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtReco_credit_limit" runat="server" ControlToValidate="txtReco_credit_limit"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage="Recommended credit limit (Batch input field) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Recommended credit limit (Batch input field) cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Currency of recommended credit limit
                                <asp:Label ID="labletxtCurrency_recommend" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtCurrency_recommend" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="5" Width="100" onfocus="return txtCurrency_recommendOnFocus();" onchange="return txtCurrency_recommendTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtCurrency_recommend" runat="server" ControlToValidate="txtCurrency_recommend"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage="Currency of recommended credit limit cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Currency of recommended credit limit cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Date (batch input)
                                <asp:Label ID="labletxtDate_Batch4" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtDate_Batch4" runat="server" CssClass="textbox" MaxLength="10"
                                    Width="100px" />
                                <asp:RequiredFieldValidator ID="reqtxtDate_Batch4" runat="server" ControlToValidate="txtDate_Batch4"
                                    ValidationGroup="CrdtMgmtData" ErrorMessage="Date (batch input) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Date (batch input) cannot be blank.' />" />
                                <ajax:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDate_Batch4"
                                    Format="dd/MM/yyyy">
                                </ajax:CalendarExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDate_Batch4"
                                    ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                    Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                    ValidationGroup="CrdtMgmtData" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnPrevious" runat="server" ValidationGroup="CrdtMgmtData" Text="Back"
                                    CssClass="button" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="CrdtMgmtData" Text="Save"
                                    CssClass="button" OnClick="btnSave_Click" />
                                <asp:Button ID="btnNext" runat="server" ValidationGroup="CrdtMgmtData" Text="Save & Next"
                                    CssClass="button" OnClick="btnNext_Click" Width="120px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="CrdtMgmtData" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblCreaditMgntDataId" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="25" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <script type="text/javascript">

        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {

        });

        $(function () {

        });

        function txtCustomer_Acc_NoOnFocus() {
            textboxId = $('#<%= txtCustomer_Acc_No.ClientID%>').attr('ID');
            textboxRealId = "txtCustomer_Acc_No";
            AutoCompleteLookUpHeaderC();
        }

        function txtCustomer_Acc_NoTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCustomer_Acc_No.ClientID%>').attr('ID'), "txtCustomer_Acc_No", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtIndicator_BlockedOnFocus() {
            textboxId = $('#<%= txtIndicator_Blocked.ClientID%>').attr('ID');
            textboxRealId = "txtIndicator_Blocked";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndicator_BlockedTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndicator_Blocked.ClientID%>').attr('ID'), "txtIndicator_Blocked", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCredit_GroupOnFocus() {
            textboxId = $('#<%= txtCredit_Group.ClientID%>').attr('ID');
            textboxRealId = "txtCredit_Group";
            AutoCompleteLookUpHeaderC();
        }

        function txtCredit_GroupTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCredit_Group.ClientID%>').attr('ID'), "txtCredit_Group", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCredit_info_numberOnFocus() {
            textboxId = $('#<%= txtCredit_info_number.ClientID%>').attr('ID');
            textboxRealId = "txtCredit_info_number";
            AutoCompleteLookUpHeaderC();
        }

        function txtCredit_info_numberTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCredit_info_number.ClientID%>').attr('ID'), "txtCredit_info_number", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCust_Credit_GroupOnFocus() {
            textboxId = $('#<%= txtCust_Credit_Group.ClientID%>').attr('ID');
            textboxRealId = "txtCust_Credit_Group";
            AutoCompleteLookUpHeaderC();
        }

        function txtCust_Credit_GroupTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCust_Credit_Group.ClientID%>').attr('ID'), "txtCust_Credit_Group", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCust_Credit_GroupOnFocus() {
            textboxId = $('#<%= txtCust_Credit_Group.ClientID%>').attr('ID');
            textboxRealId = "txtCust_Credit_Group";
            AutoCompleteLookUpHeaderC();
        }

        function txtCust_Credit_GroupTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCust_Credit_Group.ClientID%>').attr('ID'), "txtCust_Credit_Group", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCustomer_GroupOnFocus() {
            textboxId = $('#<%= txtCustomer_Group.ClientID%>').attr('ID');
            textboxRealId = "txtCustomer_Group";
            AutoCompleteLookUpHeaderC();
        }

        function txtCustomer_GroupTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCustomer_Group.ClientID%>').attr('ID'), "txtCustomer_Group", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtReco_credit_limitOnFocus() {
            textboxId = $('#<%= txtReco_credit_limit.ClientID%>').attr('ID');
            textboxRealId = "txtReco_credit_limit";
            AutoCompleteLookUpHeaderC();
        }

        function txtReco_credit_limitTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtReco_credit_limit.ClientID%>').attr('ID'), "txtReco_credit_limit", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCurrency_recommendOnFocus() {
            textboxId = $('#<%= txtCurrency_recommend.ClientID%>').attr('ID');
            textboxRealId = "txtCurrency_recommend";
            AutoCompleteLookUpHeaderC();
        }

        function txtCurrency_recommendTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCurrency_recommend.ClientID%>').attr('ID'), "txtCurrency_recommend", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        var textboxId = "";
        var textboxRealId = "";

    </script>
</asp:Content>
