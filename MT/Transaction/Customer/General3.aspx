<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Customer/CustomerMasterPage.master"
    AutoEventWireup="true" CodeFile="General3.aspx.cs" Inherits="Transaction_Customer_General3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnlAddData" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlData" runat="server">
                <table border="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="4">
                            Contact Person
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
                                    <td class="leftTD" style="width: 20%">
                                        Fiscal Year Variant
                                        <asp:Label ID="labletxtFiscal_Year_Variant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtFiscal_Year_Variant" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="1" MaxLength="2" onfocus="return Fiscal_Year_VariantOnFocus();" onchange="return Fiscal_Year_VariantTextChangeEvent();"
                                            Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtFiscal_Year_Variant" runat="server" ControlToValidate="txtFiscal_Year_Variant"
                                            ValidationGroup="CustGeneral" ErrorMessage="Fiscal Year Variant cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Fiscal Year Variant cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Reference Account Group for One-Time Account (Customer)
                                        <asp:Label ID="labletxtReference_Account" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtReference_Account" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="2" MaxLength="4" onfocus="return txtReference_AccountOnFocus();" onchange="return txtReference_AccountTextChangeEvent();"
                                            Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtReference_Account" runat="server" ControlToValidate="txtReference_Account"
                                            ValidationGroup="CustGeneral" ErrorMessage="Reference Account Group for One-Time Account (Customer) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Reference Account Group for One-Time Account (Customer) cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        PO Box city
                                        <asp:Label ID="labletxtPO_Box_city" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtPO_Box_city" runat="server" CssClass="textbox" MaxLength="35"
                                            TabIndex="3" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtPO_Box_city" runat="server" ControlToValidate="txtPO_Box_city"
                                            ValidationGroup="CustGeneral" ErrorMessage="PO Box city cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='PO Box city cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Hierarchy assignment (batch input)
                                        <asp:Label ID="labletxtHierarchy_assignment" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtHierarchy_assignment" onKeyPress="return IsNumber();" runat="server"
                                            TabIndex="4" CssClass="textbox" MaxLength="2" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtHierarchy_assignment" runat="server" ControlToValidate="txtHierarchy_assignment"
                                            ValidationGroup="CustGeneral" ErrorMessage="Hierarchy assignment (batch input) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Hierarchy assignment (batch input) cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Central sales block for customer
                                        <asp:Label ID="labletxtCentral_sales" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCentral_sales" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="5" MaxLength="2" onfocus="return txtCentral_salesOnFocus();" onchange="return txtCentral_salesTextChangeEvent();"
                                            Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtCentral_sales" runat="server" ControlToValidate="txtCentral_sales"
                                            ValidationGroup="CustGeneral" ErrorMessage=" Central sales block for customer cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Central sales block for customer cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Customer condition group 1
                                        <asp:Label ID="labletxtCustomer_condition1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCustomer_condition1" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="6" MaxLength="2" onfocus="return txtCustomer_condition1OnFocus();"
                                            onchange="return txtCustomer_condition1TextChangeEvent();" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustomer_condition1" runat="server" ControlToValidate="txtCustomer_condition1"
                                            ValidationGroup="CustGeneral" ErrorMessage="Customer condition group 1 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer condition group 1 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Customer condition group 2
                                        <asp:Label ID="labletxtCustomer_condition2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCustomer_condition2" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="7" MaxLength="2" onfocus="return txtCustomer_condition2OnFocus();"
                                            onchange="return txtCustomer_condition2TextChangeEvent();" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustomer_condition2" runat="server" ControlToValidate="txtCustomer_condition2"
                                            ValidationGroup="CustGeneral" ErrorMessage="Customer condition group 2 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer condition group 2 cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Customer condition group 3
                                        <asp:Label ID="labletxtCustomer_condition3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCustomer_condition3" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="8" MaxLength="2" onfocus="return txtCustomer_condition3OnFocus();"
                                            onchange="return txtCustomer_condition3TextChangeEvent();" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustomer_condition3" runat="server" ControlToValidate="txtCustomer_condition3"
                                            ValidationGroup="CustGeneral" ErrorMessage="Customer condition group 3 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer condition group 3 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Customer condition group 4
                                        <asp:Label ID="labletxtCustomer_condition4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCustomer_condition4" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="9" MaxLength="2" onfocus="return txtCustomer_condition4OnFocus();"
                                            onchange="return txtCustomer_condition4TextChangeEvent();" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustomer_condition4" runat="server" ControlToValidate="txtCustomer_condition4"
                                            ValidationGroup="CustGeneral" ErrorMessage="Customer condition group 4 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer condition group 4 cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Customer condition group 5
                                        <asp:Label ID="labletxtCustomer_condition5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtCustomer_condition5" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="10" MaxLength="2" onfocus="return txtCustomer_condition5OnFocus();"
                                            onchange="return txtCustomer_condition5TextChangeEvent();" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustomer_condition5" runat="server" ControlToValidate="txtCustomer_condition5"
                                            ValidationGroup="CustGeneral" ErrorMessage="Customer condition group 5 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer condition group 5 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Uniform Resource Locator
                                        <asp:Label ID="labletxtUniform_Resource" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtUniform_Resource" runat="server" CssClass="textbox" MaxLength="132"
                                            TabIndex="11" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtUniform_Resource" runat="server" ControlToValidate="txtUniform_Resource"
                                            ValidationGroup="CustGeneral" ErrorMessage="Uniform Resource Locator cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Uniform Resource Locator cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Central deletion block for master record
                                        <asp:Label ID="lablechkCentralDeletionBlock" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkCentralDeletionBlock" runat="server" TabIndex="12" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Contact Person Name
                                        <asp:Label ID="labletxtFirst_name" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFirst_name" runat="server" CssClass="textbox" MaxLength="35"
                                            AutoPostBack="true" TabIndex="13" Width="180px" OnTextChanged="txtFirst_name_TextChanged" />
                                        <asp:RequiredFieldValidator ID="reqtxtFirst_name" runat="server" ControlToValidate="txtFirst_name"
                                            ValidationGroup="CustGeneral" ErrorMessage="Contact Person Name cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Contact Person Name cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Country
                                        <asp:Label ID="lableddlCountry" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" TabIndex="14"
                                            OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlCountry" runat="server" ControlToValidate="ddlCountry"
                                            ValidationGroup="CustGeneral" ErrorMessage="Country cannot be blank." InitialValue="0"
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Country cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        First Mobile Number
                                        <asp:Label ID="labletxtMobileNum" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMobileNum" runat="server" CssClass="textbox" MaxLength="30" Width="180px"
                                            onchange="return ActivateMobile();" TabIndex="15" />
                                        <asp:RequiredFieldValidator ID="reqtxtMobileNum" runat="server" ControlToValidate="txtMobileNum"
                                            ValidationGroup="CustGeneral" ErrorMessage=" First Mobile number cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' First Mobile number cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtMobileNum" runat="server" ControlToValidate="txtMobileNum"
                                            ValidationExpression="^[0-9]{10}$" ValidationGroup="CustGeneral" ErrorMessage="Invalid First Mobile Number"
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid First Mobile Number.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Second Mobile Number
                                        <asp:Label ID="labletxtMobileNum2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMobileNum2" runat="server" CssClass="textbox" MaxLength="30"
                                            TabIndex="16" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtMobileNum2" runat="server" ControlToValidate="txtMobileNum2"
                                            ValidationGroup="CustGeneral" ErrorMessage="Second Mobile Number cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Second Mobile Number cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtMobileNum2" runat="server" ControlToValidate="txtMobileNum2"
                                            ValidationExpression="^[0-9]{10}$" ValidationGroup="CustGeneral" ErrorMessage="Invalid Second Mobile Number"
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Second Mobile Number.' />" />
                                        <asp:CompareValidator ID="ComptxtMobileNum2" runat="server" Type="String" Display="Dynamic"
                                            ValidationGroup="CustGeneral" ControlToCompare="txtMobileNum" ControlToValidate="txtMobileNum2"
                                            ErrorMessage="Mobile Numbers cannot be same." Operator="NotEqual" Text="<img src='../../images/Error.png' title='Mobile Numbers cannot be same.' />"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        First Telephone Number
                                        <asp:Label ID="labletxtFirsttelephone" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFirsttelephone" runat="server" CssClass="textbox" MaxLength="16"
                                            onchange="return ActivateTel();" TabIndex="17" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtFirsttelephone" runat="server" ControlToValidate="txtFirsttelephone"
                                            ValidationGroup="CustGeneral" ErrorMessage=" First telephone number cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' First telephone number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Second Telephone Number
                                        <asp:Label ID="labletxtSecondTelephoneNumber" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtSecondTelephoneNumber" runat="server" CssClass="textbox" MaxLength="16"
                                            TabIndex="18" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtSecondTelephoneNumber" runat="server" ControlToValidate="txtSecondTelephoneNumber"
                                            ValidationGroup="CustGeneral" ErrorMessage="Second Telephone Number cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Second Telephone Number cannot be blank.' />" />
                                        <asp:CompareValidator ID="ComptxtSecondTelephoneNumber" runat="server" Type="String"
                                            Display="Dynamic" ValidationGroup="CustGeneral" ControlToCompare="txtFirsttelephone"
                                            ControlToValidate="txtSecondTelephoneNumber" ErrorMessage="Telephone Number cannot be same."
                                            Operator="NotEqual" Text="<img src='../../images/Error.png' title='Telephone Number cannot be same.' />"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        E-Mail Address
                                        <asp:Label ID="labletxtEmailAddress" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="textbox" Width="180px"
                                            onchange="return ActivateEmail();" TabIndex="19" MaxLength="241" />
                                        <asp:RequiredFieldValidator ID="reqtxtEmailAddress" runat="server" ControlToValidate="txtEmailAddress"
                                            ValidationGroup="CustGeneral" ErrorMessage="E-Mail Address cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='E-Mail Address cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtEmailAddress" runat="server" ControlToValidate="txtEmailAddress"
                                            ValidationExpression="^([\w-\.]+)[\w]{1}@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            ValidationGroup="CustGeneral" ErrorMessage="Invalid E-Mail Address" SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid E-Mail Address.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        E-Mail Address 2
                                        <asp:Label ID="labletxtEmailAddress2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtEmailAddress2" runat="server" CssClass="textbox" Width="180px"
                                            TabIndex="20" MaxLength="241" />
                                        <asp:RequiredFieldValidator ID="reqtxtEmailAddress2" runat="server" ControlToValidate="txtEmailAddress2"
                                            ValidationGroup="CustGeneral" ErrorMessage="E-Mail Address 2 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='E-Mail Address 2 cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtEmailAddress2" runat="server" ControlToValidate="txtEmailAddress2"
                                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            ValidationGroup="CustGeneral" ErrorMessage="Invalid E-Mail Address 2" SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid E-Mail Address 2.' />" />
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" Type="String" Display="Dynamic"
                                            ValidationGroup="CustGeneral" ControlToCompare="txtEmailAddress" ControlToValidate="txtEmailAddress2"
                                            ErrorMessage="E-Mail Address 2 cannot be same as E-Mail Address." Operator="NotEqual"
                                            Text="<img src='../../images/Error.png' title='E-Mail Address 2 cannot be same as E-Mail Address.' />"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Fax Number
                                        <asp:Label ID="labletxtFaxNumber" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFaxNumber" runat="server" CssClass="textbox" MaxLength="31" Width="180px"
                                            TabIndex="21" />
                                        <asp:RequiredFieldValidator ID="reqtxtFaxNumber" runat="server" ControlToValidate="txtFaxNumber"
                                            ValidationGroup="CustGeneral" ErrorMessage="Fax Number cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Fax Number cannot be blank.' />" />
                                    </td>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Contact Person Name
                                        <asp:Label ID="labletxtFirst_name2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFirst_name2" runat="server" CssClass="textbox" MaxLength="35"
                                            AutoPostBack="true" TabIndex="22" Width="180px" OnTextChanged="txtFirst_name2_TextChanged" />
                                        <asp:RequiredFieldValidator ID="reqtxtFirst_name2" runat="server" ControlToValidate="txtFirst_name2"
                                            ValidationGroup="CustGeneral" ErrorMessage="Contact Person Name cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Contact Person Name cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Country
                                        <asp:Label ID="lableddlCountry2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlCountry2" runat="server" AutoPostBack="true" TabIndex="23"
                                            OnSelectedIndexChanged="ddlCountry2_SelectedIndexChanged">
                                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlCountry2" runat="server" ControlToValidate="ddlCountry2"
                                            ValidationGroup="CustGeneral" ErrorMessage="Country cannot be blank." InitialValue="0"
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Country cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        First Mobile Number
                                        <asp:Label ID="labletxtMobileNum_2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMobileNum_2" runat="server" CssClass="textbox" MaxLength="30"
                                            Width="180px" onchange="return ActivateMobile2();" TabIndex="24" />
                                        <asp:RequiredFieldValidator ID="reqtxtMobileNum_2" runat="server" ControlToValidate="txtMobileNum_2"
                                            ValidationGroup="CustGeneral" ErrorMessage=" First Mobile number cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' First Mobile number cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtMobileNum_2" runat="server" ControlToValidate="txtMobileNum_2"
                                            ValidationExpression="^[0-9]{10}$" ValidationGroup="CustGeneral" ErrorMessage="Invalid First Mobile Number"
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid First Mobile Number.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Second Mobile Number
                                        <asp:Label ID="labletxtMobileNum22" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMobileNum22" runat="server" CssClass="textbox" MaxLength="30"
                                            TabIndex="25" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtMobileNum22" runat="server" ControlToValidate="txtMobileNum22"
                                            ValidationGroup="CustGeneral" ErrorMessage="Second Mobile Number cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Second Mobile Number cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtMobileNum22" runat="server" ControlToValidate="txtMobileNum22"
                                            ValidationExpression="^[0-9]{10}$" ValidationGroup="CustGeneral" ErrorMessage="Invalid Second Mobile Number"
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Second Mobile Number.' />" />
                                        <asp:CompareValidator ID="ComptxtMobileNum22" runat="server" Type="String" Display="Dynamic"
                                            ValidationGroup="CustGeneral" ControlToCompare="txtMobileNum_2" ControlToValidate="txtMobileNum22"
                                            ErrorMessage="Mobile Numbers cannot be same." Operator="NotEqual" Text="<img src='../../images/Error.png' title='Mobile Numbers cannot be same.' />"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        First Telephone Number
                                        <asp:Label ID="labletxtFirsttelephone2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFirsttelephone2" runat="server" CssClass="textbox" MaxLength="16"
                                            onchange="return ActivateTel2();" TabIndex="26" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtFirsttelephone2" runat="server" ControlToValidate="txtFirsttelephone2"
                                            ValidationGroup="CustGeneral" ErrorMessage=" First telephone number cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' First telephone number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Second Telephone Number
                                        <asp:Label ID="labletxtSecondTelephoneNumber2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtSecondTelephoneNumber2" runat="server" CssClass="textbox" MaxLength="16"
                                            TabIndex="27" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtSecondTelephoneNumber2" runat="server" ControlToValidate="txtSecondTelephoneNumber2"
                                            ValidationGroup="CustGeneral" ErrorMessage="Second Telephone Number cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Second Telephone Number cannot be blank.' />" />
                                        <asp:CompareValidator ID="ComptxtSecondTelephoneNumber2" runat="server" Type="String"
                                            Display="Dynamic" ValidationGroup="CustGeneral" ControlToCompare="txtFirsttelephone2"
                                            ControlToValidate="txtSecondTelephoneNumber2" ErrorMessage="Telephone Number cannot be same."
                                            Operator="NotEqual" Text="<img src='../../images/Error.png' title='Telephone Number cannot be same.' />"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        E-Mail Address
                                        <asp:Label ID="labletxtEmailAddress_2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtEmailAddress_2" runat="server" CssClass="textbox" Width="180px"
                                            onchange="return ActivateEmail2();" TabIndex="28" MaxLength="241" />
                                        <asp:RequiredFieldValidator ID="reqtxtEmailAddress_2" runat="server" ControlToValidate="txtEmailAddress_2"
                                            ValidationGroup="CustGeneral" ErrorMessage="E-Mail Address cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='E-Mail Address cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtEmailAddress_2" runat="server" ControlToValidate="txtEmailAddress_2"
                                            ValidationExpression="^([\w-\.]+)[\w]{1}@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            ValidationGroup="CustGeneral" ErrorMessage="Invalid E-Mail Address" SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid E-Mail Address.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        E-Mail Address 2
                                        <asp:Label ID="labletxtEmailAddress22" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtEmailAddress22" runat="server" CssClass="textbox" Width="180px"
                                            TabIndex="29" MaxLength="241" />
                                        <asp:RequiredFieldValidator ID="reqtxtEmailAddress22" runat="server" ControlToValidate="txtEmailAddress22"
                                            ValidationGroup="CustGeneral" ErrorMessage="E-Mail Address 2 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='E-Mail Address 2 cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtEmailAddress22" runat="server" ControlToValidate="txtEmailAddress22"
                                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            ValidationGroup="CustGeneral" ErrorMessage="Invalid E-Mail Address 2" SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid E-Mail Address 2.' />" />
                                        <asp:CompareValidator ID="CompareValidator22" runat="server" Type="String" Display="Dynamic"
                                            ValidationGroup="CustGeneral" ControlToCompare="txtEmailAddress_2" ControlToValidate="txtEmailAddress22"
                                            ErrorMessage="E-Mail Address 2 cannot be same as E-Mail Address." Operator="NotEqual"
                                            Text="<img src='../../images/Error.png' title='E-Mail Address 2 cannot be same as E-Mail Address.' />"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Fax Number
                                        <asp:Label ID="labletxtFaxNumber2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFaxNumber2" runat="server" CssClass="textbox" MaxLength="31"
                                            Width="180px" TabIndex="30" />
                                        <asp:RequiredFieldValidator ID="reqtxtFaxNumber2" runat="server" ControlToValidate="txtFaxNumber2"
                                            ValidationGroup="CustGeneral" ErrorMessage="Fax Number cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Fax Number cannot be blank.' />" />
                                    </td>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Contact Person Name
                                        <asp:Label ID="labletxtFirst_name3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFirst_name3" runat="server" CssClass="textbox" MaxLength="35"
                                            AutoPostBack="true" Width="180px" TabIndex="32" OnTextChanged="txtFirst_name3_TextChanged" />
                                        <asp:RequiredFieldValidator ID="reqtxtFirst_name3" runat="server" ControlToValidate="txtFirst_name3"
                                            ValidationGroup="CustGeneral" ErrorMessage="Contact Person Name cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Contact Person Name cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Country
                                        <asp:Label ID="lableddlCountry3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlCountry3" runat="server" AutoPostBack="true" TabIndex="33"
                                            OnSelectedIndexChanged="ddlCountry3_SelectedIndexChanged">
                                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlCountry3" runat="server" ControlToValidate="ddlCountry3"
                                            ValidationGroup="CustGeneral" ErrorMessage="Country cannot be blank." InitialValue="0"
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Country cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        First Mobile Number
                                        <asp:Label ID="labletxtMobileNum3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMobileNum3" runat="server" CssClass="textbox" MaxLength="30"
                                            Width="180px" onchange="return ActivateMobile3();" TabIndex="34" />
                                        <asp:RequiredFieldValidator ID="reqtxtMobileNum3" runat="server" ControlToValidate="txtMobileNum3"
                                            ValidationGroup="CustGeneral" ErrorMessage=" First Mobile number cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' First Mobile number cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtMobileNum3" runat="server" ControlToValidate="txtMobileNum3"
                                            ValidationExpression="^[0-9]{10}$" ValidationGroup="CustGeneral" ErrorMessage="Invalid First Mobile Number"
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid First Mobile Number.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Second Mobile Number
                                        <asp:Label ID="labletxtMobileNum23" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMobileNum23" runat="server" CssClass="textbox" MaxLength="30"
                                            TabIndex="35" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtMobileNum23" runat="server" ControlToValidate="txtMobileNum23"
                                            ValidationGroup="CustGeneral" ErrorMessage="Second Mobile Number cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Second Mobile Number cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtMobileNum23" runat="server" ControlToValidate="txtMobileNum23"
                                            ValidationExpression="^[0-9]{10}$" ValidationGroup="CustGeneral" ErrorMessage="Invalid Second Mobile Number"
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Second Mobile Number.' />" />
                                        <asp:CompareValidator ID="ComptxtMobileNum23" runat="server" Type="String" Display="Dynamic"
                                            ValidationGroup="CustGeneral" ControlToCompare="txtMobileNum3" ControlToValidate="txtMobileNum23"
                                            ErrorMessage="Mobile Numbers cannot be same." Operator="NotEqual" Text="<img src='../../images/Error.png' title='Mobile Numbers cannot be same.' />"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        First Telephone Number
                                        <asp:Label ID="labletxtFirsttelephone3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFirsttelephone3" runat="server" CssClass="textbox" MaxLength="16"
                                            onchange="return ActivateTel3();" TabIndex="36" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtFirsttelephone3" runat="server" ControlToValidate="txtFirsttelephone3"
                                            ValidationGroup="CustGeneral" ErrorMessage=" First telephone number cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' First telephone number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Second Telephone Number
                                        <asp:Label ID="labletxtSecondTelephoneNumber3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtSecondTelephoneNumber3" runat="server" CssClass="textbox" MaxLength="16"
                                            TabIndex="37" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtSecondTelephoneNumber3" runat="server" ControlToValidate="txtSecondTelephoneNumber3"
                                            ValidationGroup="CustGeneral" ErrorMessage="Second Telephone Number cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Second Telephone Number cannot be blank.' />" />
                                        <asp:CompareValidator ID="ComptxtSecondTelephoneNumber3" runat="server" Type="String"
                                            Display="Dynamic" ValidationGroup="CustGeneral" ControlToCompare="txtFirsttelephone3"
                                            ControlToValidate="txtSecondTelephoneNumber3" ErrorMessage="Telephone Number cannot be same."
                                            Operator="NotEqual" Text="<img src='../../images/Error.png' title='Telephone Number cannot be same.' />"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        E-Mail Address
                                        <asp:Label ID="labletxtEmailAddress3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtEmailAddress3" runat="server" CssClass="textbox" Width="180px"
                                            onchange="return ActivateEmail3();" TabIndex="38" MaxLength="241" />
                                        <asp:RequiredFieldValidator ID="reqtxtEmailAddress3" runat="server" ControlToValidate="txtEmailAddress3"
                                            ValidationGroup="CustGeneral" ErrorMessage="E-Mail Address cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='E-Mail Address cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtEmailAddress3" runat="server" ControlToValidate="txtEmailAddress3"
                                            ValidationExpression="^([\w-\.]+)[\w]{1}@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            ValidationGroup="CustGeneral" ErrorMessage="Invalid E-Mail Address" SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid E-Mail Address.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        E-Mail Address 2
                                        <asp:Label ID="labletxtEmailAddress23" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtEmailAddress23" runat="server" CssClass="textbox" Width="180px"
                                            TabIndex="39" MaxLength="241" />
                                        <asp:RequiredFieldValidator ID="reqtxtEmailAddress23" runat="server" ControlToValidate="txtEmailAddress23"
                                            ValidationGroup="CustGeneral" ErrorMessage="E-Mail Address 2 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='E-Mail Address 2 cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtEmailAddress23" runat="server" ControlToValidate="txtEmailAddress23"
                                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            ValidationGroup="CustGeneral" ErrorMessage="Invalid E-Mail Address 2" SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid E-Mail Address 2.' />" />
                                        <asp:CompareValidator ID="CompareValidator23" runat="server" Type="String" Display="Dynamic"
                                            ValidationGroup="CustGeneral" ControlToCompare="txtEmailAddress3" ControlToValidate="txtEmailAddress23"
                                            ErrorMessage="E-Mail Address 2 cannot be same as E-Mail Address." Operator="NotEqual"
                                            Text="<img src='../../images/Error.png' title='E-Mail Address 2 cannot be same as E-Mail Address.' />"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Fax Number
                                        <asp:Label ID="labletxtFaxNumber3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFaxNumber3" runat="server" CssClass="textbox" MaxLength="31"
                                            Width="180px" TabIndex="40" />
                                        <asp:RequiredFieldValidator ID="reqtxtFaxNumber3" runat="server" ControlToValidate="txtFaxNumber3"
                                            ValidationGroup="CustGeneral" ErrorMessage="Fax Number cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Fax Number cannot be blank.' />" />
                                    </td>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Customer factory calendar
                                        <asp:Label ID="labletxtCustomer_factory" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtCustomer_factory" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="41" MaxLength="2" Width="100" onfocus="return txtCustomer_factoryOnFocus();"
                                            onchange="return txtCustomer_factoryTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtCustomer_factory" runat="server" ControlToValidate="txtCustomer_factory"
                                            ValidationGroup="CustGeneral" ErrorMessage="Customer factory calendar cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer factory calendar cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Contact person department
                                        <asp:Label ID="labletxtContact_person_department" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtContact_person_department" runat="server" CssClass="textbox"
                                            TabIndex="42" MaxLength="4" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtContact_person_department" runat="server" ControlToValidate="txtContact_person_department"
                                            ValidationGroup="CustGeneral" ErrorMessage="Contact person department cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Contact person department cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Unloading Point
                                        <asp:Label ID="labletxtUnloading_Point" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtUnloading_Point" runat="server" CssClass="textbox" MaxLength="25"
                                            TabIndex="43" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtUnloading_Point" runat="server" ControlToValidate="txtUnloading_Point"
                                            ValidationGroup="CustGeneral" ErrorMessage=" Unloading Point cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Unloading Point cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Form of address for contact person (Mr, Mrs...etc)
                                        <asp:Label ID="labletxtForm_address" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtForm_address" runat="server" CssClass="textbox" MaxLength="30"
                                            TabIndex="44" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtForm_address" runat="server" ControlToValidate="txtForm_address"
                                            ValidationGroup="CustGeneral" ErrorMessage="Form of address for contact person (Mr, Mrs...etc) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Form of address for contact person (Mr, Mrs...etc) cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Contact person function
                                        <asp:Label ID="labletxtContact_person_function" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtContact_person_function" runat="server" CssClass="textbox" MaxLength="2"
                                            TabIndex="45" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtContact_person_function" runat="server" ControlToValidate="txtContact_person_function"
                                            ValidationGroup="CustGeneral" ErrorMessage=" Contact person function cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Contact person function cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Partner language
                                        <asp:Label ID="labletxtPartner_language" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtPartner_language" runat="server" CssClass="textbox" MaxLength="2"
                                            TabIndex="46" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtPartner_language" runat="server" ControlToValidate="txtPartner_language"
                                            ValidationGroup="CustGeneral" ErrorMessage="Partner language cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner language cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Partner's gender
                                        <asp:Label ID="labletxtPartner_gender" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtPartner_gender" runat="server" CssClass="textbox" MaxLength="1"
                                            TabIndex="47" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtPartner_gender" runat="server" ControlToValidate="txtPartner_gender"
                                            ValidationGroup="CustGeneral" ErrorMessage="Partner's gender cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner's gender cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Marital Status Key
                                        <asp:Label ID="labletxtMarital_Status" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMarital_Status" runat="server" CssClass="textbox" MaxLength="1"
                                            TabIndex="48" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtMarital_Status" runat="server" ControlToValidate="txtMarital_Status"
                                            ValidationGroup="CustGeneral" ErrorMessage="Marital Status Key cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Marital Status Key cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Date (batch input)
                                        <asp:Label ID="labletxtDate_Batch" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtDate_Batch" runat="server" CssClass="textbox" MaxLength="10"
                                            TabIndex="49" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtDate_Batch" runat="server" ControlToValidate="txtDate_Batch"
                                            ValidationGroup="CustGeneral" ErrorMessage="Date (batch input) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Date (batch input) cannot be blank.' />" />
                                        <%--<ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate_Batch"
                                            Format="dd/MM/yyyy">
                                        </ajax:CalendarExtender>--%>
                                        <asp:RegularExpressionValidator ID="revDTTest" runat="server" ControlToValidate="txtDate_Batch"
                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                            ValidationGroup="CustGeneral" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="leftTD">
                                        Contact person's department at customer
                                        <asp:Label ID="labletxtContact_person_department_Cust" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtContact_person_department_Cust" runat="server" CssClass="textbox"
                                            TabIndex="50" MaxLength="12" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtContact_person_department_Cust" runat="server"
                                            ControlToValidate="txtContact_person_department_Cust" ValidationGroup="CustGeneral"
                                            ErrorMessage="Contact person's department at customer cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Contact person's department at customer cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        VIP Partner
                                        <asp:Label ID="labletxtVIP_Partner" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtVIP_Partner" runat="server" CssClass="textboxAutocomplete" MaxLength="1"
                                            TabIndex="51" Width="100" onfocus="return txtVIP_PartnerOnFocus();" onchange="return txtVIP_PartnerChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtVIP_Partner" runat="server" ControlToValidate="txtVIP_Partner"
                                            ValidationGroup="CustGeneral" ErrorMessage="VIP Partner cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='VIP Partner cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Partner's Authority
                                        <asp:Label ID="labletxtPartner_Authority" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtPartner_Authority" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="52" MaxLength="1" Width="100" onfocus="return txtPartner_AuthorityOnFocus();"
                                            onchange="return txtPartner_AuthorityChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtPartner_Authority" runat="server" ControlToValidate="txtPartner_Authority"
                                            ValidationGroup="CustGeneral" ErrorMessage="Partner's Authority cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Partner's Authority cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Notes about contact person
                                        <asp:Label ID="labletxtNotes" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtNotes" runat="server" CssClass="textbox" MaxLength="40" Width="100"
                                            TabIndex="53" />
                                        <asp:RequiredFieldValidator ID="reqtxtNotes" runat="server" ControlToValidate="txtNotes"
                                            ValidationGroup="CustGeneral" ErrorMessage="Notes about contact person cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Notes about contact person cannot be blank.' />" />
                                    </td>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr id="trButton" runat="server" visible="false">
                                    <td class="centerTD" colspan="4">
                                        <asp:Button ID="btnPrevious" runat="server" ValidationGroup="CustGeneral" Text="Back"
                                            TabIndex="54" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="CustGeneral" Text="Save"
                                            TabIndex="55" CssClass="button" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnNext" runat="server" ValidationGroup="CustGeneral" Text="Save & Next"
                                            TabIndex="56" CssClass="button" OnClick="btnNext_Click" Width="120px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="CustGeneral" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblCustomerGeneral3Id" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="27" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
            <script type="text/javascript">

                function ActivateTel() {

                    if ($('#<%= txtFirsttelephone.ClientID%>').val().length > 0)
                        $('#<%= txtSecondTelephoneNumber.ClientID%>').attr('disabled', false);
                }

                function ActivateMobile() {

                    if ($('#<%= txtMobileNum.ClientID%>').val().length > 0)
                        $('#<%= txtMobileNum2.ClientID%>').attr('disabled', false);
                }

                function ActivateEmail() {

                    if ($('#<%= txtEmailAddress.ClientID%>').val().length > 0)
                        $('#<%= txtEmailAddress2.ClientID%>').attr('disabled', false);
                }

                function ActivateTel2() {

                    if ($('#<%= txtFirsttelephone2.ClientID%>').val().length > 0)
                        $('#<%= txtSecondTelephoneNumber2.ClientID%>').attr('disabled', false);
                }

                function ActivateMobile2() {

                    if ($('#<%= txtMobileNum_2.ClientID%>').val().length > 0)
                        $('#<%= txtMobileNum22.ClientID%>').attr('disabled', false);
                }

                function ActivateEmail2() {

                    if ($('#<%= txtEmailAddress_2.ClientID%>').val().length > 0)
                        $('#<%= txtEmailAddress22.ClientID%>').attr('disabled', false);
                }

                function ActivateTel3() {

                    if ($('#<%= txtFirsttelephone3.ClientID%>').val().length > 0)
                        $('#<%= txtSecondTelephoneNumber3.ClientID%>').attr('disabled', false);
                }

                function ActivateMobile3() {

                    if ($('#<%= txtMobileNum3.ClientID%>').val().length > 0)
                        $('#<%= txtMobileNum23.ClientID%>').attr('disabled', false);
                }

                function ActivateEmail3() {

                    if ($('#<%= txtEmailAddress3.ClientID%>').val().length > 0)
                        $('#<%= txtEmailAddress23.ClientID%>').attr('disabled', false);
                }

            </script>
            <script type="text/javascript">

                var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
                prmInstance.add_endRequest(function () {

                });

                $(function () {

                });

                function Fiscal_Year_VariantOnFocus() {
                    textboxId = $('#<%= txtFiscal_Year_Variant.ClientID%>').attr('ID');
                    textboxRealId = "txtFiscal_Year_Variant";
                    AutoCompleteLookUpHeaderC();
                }

                function Fiscal_Year_VariantTextChangeEvent() {
                    CheckLookupHeaderC($('#<%= txtFiscal_Year_Variant.ClientID%>').attr('ID'), "txtFiscal_Year_Variant", $('#<%= btnNext.ClientID%>').attr('ID'));
                }

                function txtReference_AccountOnFocus() {
                    textboxId = $('#<%= txtReference_Account.ClientID%>').attr('ID');
                    textboxRealId = "txtReference_Account";
                    AutoCompleteLookUpHeaderC();
                }

                function txtReference_AccountTextChangeEvent() {
                    CheckLookupHeaderC($('#<%= txtReference_Account.ClientID%>').attr('ID'), "txtReference_Account", $('#<%= btnNext.ClientID%>').attr('ID'));
                }

                function txtCentral_salesOnFocus() {
                    textboxId = $('#<%= txtCentral_sales.ClientID%>').attr('ID');
                    textboxRealId = "txtCentral_sales";
                    AutoCompleteLookUpHeaderC();
                }

                function txtCentral_salesTextChangeEvent() {
                    CheckLookupHeaderC($('#<%= txtCentral_sales.ClientID%>').attr('ID'), "txtCentral_sales", $('#<%= btnNext.ClientID%>').attr('ID'));
                }


                function txtCustomer_condition1OnFocus() {
                    textboxId = $('#<%= txtCustomer_condition1.ClientID%>').attr('ID');
                    textboxRealId = "txtCustomer_condition1";
                    AutoCompleteLookUpHeaderC();
                }

                function txtCustomer_condition1TextChangeEvent() {
                    CheckLookupHeaderC($('#<%= txtCustomer_condition1.ClientID%>').attr('ID'), "txtCustomer_condition1", $('#<%= btnNext.ClientID%>').attr('ID'));
                }

                function txtCustomer_condition2OnFocus() {
                    textboxId = $('#<%= txtCustomer_condition2.ClientID%>').attr('ID');
                    textboxRealId = "txtCustomer_condition2";
                    AutoCompleteLookUpHeaderC();
                }

                function txtCustomer_condition2TextChangeEvent() {
                    CheckLookupHeaderC($('#<%= txtCustomer_condition2.ClientID%>').attr('ID'), "txtCustomer_condition2", $('#<%= btnNext.ClientID%>').attr('ID'));
                }

                function txtCustomer_condition3OnFocus() {
                    textboxId = $('#<%= txtCustomer_condition3.ClientID%>').attr('ID');
                    textboxRealId = "txtCustomer_condition3";
                    AutoCompleteLookUpHeaderC();
                }

                function txtCustomer_condition3TextChangeEvent() {
                    CheckLookupHeaderC($('#<%= txtCustomer_condition3.ClientID%>').attr('ID'), "txtCustomer_condition3", $('#<%= btnNext.ClientID%>').attr('ID'));
                }

                function txtCustomer_condition4OnFocus() {
                    textboxId = $('#<%= txtCustomer_condition4.ClientID%>').attr('ID');
                    textboxRealId = "txtCustomer_condition4";
                    AutoCompleteLookUpHeaderC();
                }

                function txtCustomer_condition4TextChangeEvent() {
                    CheckLookupHeaderC($('#<%= txtCustomer_condition4.ClientID%>').attr('ID'), "txtCustomer_condition4", $('#<%= btnNext.ClientID%>').attr('ID'));
                }

                function txtCustomer_condition5OnFocus() {
                    textboxId = $('#<%= txtCustomer_condition5.ClientID%>').attr('ID');
                    textboxRealId = "txtCustomer_condition5";
                    AutoCompleteLookUpHeaderC();
                }

                function txtCustomer_condition5TextChangeEvent() {
                    CheckLookupHeaderC($('#<%= txtCustomer_condition5.ClientID%>').attr('ID'), "txtCustomer_condition5", $('#<%= btnNext.ClientID%>').attr('ID'));
                }

                function txtCustomer_factoryOnFocus() {
                    textboxId = $('#<%= txtCustomer_factory.ClientID%>').attr('ID');
                    textboxRealId = "txtCustomer_factory";
                    AutoCompleteLookUpHeaderC();
                }

                function txtCustomer_factoryTextChangeEvent() {
                    CheckLookupHeaderC($('#<%= txtCustomer_factory.ClientID%>').attr('ID'), "txtCustomer_factory", $('#<%= btnNext.ClientID%>').attr('ID'));
                }


                function txtVIP_PartnerOnFocus() {
                    textboxId = $('#<%= txtVIP_Partner.ClientID%>').attr('ID');
                    textboxRealId = "txtVIP_Partner";
                    AutoCompleteLookUpHeaderC();
                }

                function txtVIP_PartnerChangeEvent() {
                    CheckLookupHeaderC($('#<%= txtVIP_Partner.ClientID%>').attr('ID'), "txtVIP_Partner", $('#<%= btnNext.ClientID%>').attr('ID'));
                }

                function txtPartner_AuthorityOnFocus() {
                    textboxId = $('#<%= txtPartner_Authority.ClientID%>').attr('ID');
                    textboxRealId = "txtPartner_Authority";
                    AutoCompleteLookUpHeaderC();
                }

                function txtPartner_AuthorityChangeEvent() {
                    CheckLookupHeaderC($('#<%= txtPartner_Authority.ClientID%>').attr('ID'), "txtPartner_Authority", $('#<%= btnNext.ClientID%>').attr('ID'));
                }

                var textboxId = "";
                var textboxRealId = "";
                function IsNumber() {
                    if ((event.keyCode < 48) || (event.keyCode > 57))
                        return false;
                }
                
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
