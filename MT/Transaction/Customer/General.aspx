<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Customer/CustomerMasterPage.master"
    AutoEventWireup="true" CodeFile="General.aspx.cs" Inherits="Transaction_Cutomer_General" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery.MultiFile.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" cellpadding="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">
                    General Data
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Customer Code
                                <asp:Label ID="labletxtCustomerCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="textbox" MaxLength="10"
                                    TabIndex="1" Enabled="false" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtCustomerCode" runat="server" ControlToValidate="txtCustomerCode"
                                    ValidationGroup="CustGeneral" ErrorMessage="Customer Code cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Code cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtCustomerCode" runat="server" ControlToValidate="txtCustomerCode"
                                    ValidationGroup="CustGeneral" ErrorMessage="Customer Code Invalid." SetFocusOnError="true"
                                    ValidationExpression="^[\S]{4,10}" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Code Invalid.' />" />
                            </td>
                            <td class="tdSpace" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" width="20%">
                                Company Code
                                <asp:Label ID="lableddlCompanyCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlCompanyCode" runat="server" AppendDataBoundItems="false"
                                    TabIndex="2">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCompanyCode" runat="server" ControlToValidate="ddlCompanyCode"
                                    ValidationGroup="CustGeneral" ErrorMessage="Select Company Code." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company Code.' />" />
                            </td>
                            <td class="leftTD" width="20%">
                                Customer account group
                                <asp:Label ID="lableddlCustomerAccGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlCustomerAccGrp" runat="server" AppendDataBoundItems="false"
                                    TabIndex="3">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCustomerAccGrp" runat="server" ControlToValidate="ddlCustomerAccGrp"
                                    ValidationGroup="CustGeneral" ErrorMessage="Select Customer account group." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Customer account group.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Title
                                <asp:Label ID="lableddlTitle" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTitle" runat="server" AppendDataBoundItems="false" TabIndex="4">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTitle" runat="server" ControlToValidate="ddlTitle"
                                    ValidationGroup="CustGeneral" ErrorMessage="Title cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Title cannot be blank.' />" />
                            </td>
                            <td class="leftTD" width="20%">
                                Customer Type
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlCustomerType" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Text="---Select---" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCustomerType" runat="server" ControlToValidate="ddlCustomerType"
                                    ValidationGroup="next" ErrorMessage="Select Customer Type." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Customer Type.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Customer Name
                                <asp:Label ID="labletxtName1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtName1" runat="server" CssClass="textbox" MaxLength="35" Width="180"
                                    TabIndex="5" onchange="return ActivateName();" />
                                <asp:RequiredFieldValidator ID="reqtxtName1" runat="server" ControlToValidate="txtName1"
                                    ValidationGroup="CustGeneral" ErrorMessage="Customer Name cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Name cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Customer Name (Conti..)
                                <asp:Label ID="labletxtName2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtName2" runat="server" CssClass="textbox" MaxLength="35" Width="180"
                                    TabIndex="6" />
                                <asp:RequiredFieldValidator ID="reqtxtName2" runat="server" ControlToValidate="txtName2"
                                    ValidationGroup="CustGeneral" ErrorMessage="Customer Name (Conti..) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Name (Conti..) cannot be blank.' />" />
                                <asp:CompareValidator ID="ComptxtName2" runat="server" Type="String" Display="Dynamic"
                                    ValidationGroup="CustGeneral" ControlToCompare="txtName1" ControlToValidate="txtName2"
                                    ErrorMessage="Customer Name (Conti..) cannot be same as Customer Name." Operator="NotEqual"
                                    Text="<img src='../../images/Error.png' title='Customer Name (Conti..) cannot be same as Customer Name.' />"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Name 3
                                <asp:Label ID="labletxtName3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtName3" runat="server" CssClass="textbox" MaxLength="35" Width="180"
                                    TabIndex="7" />
                                <asp:RequiredFieldValidator ID="reqtxtName3" runat="server" ControlToValidate="txtName3"
                                    ValidationGroup="CustGeneral" ErrorMessage="Name 3 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 3 cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Name 4
                                <asp:Label ID="labletxtName4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtName4" runat="server" CssClass="textbox" MaxLength="35" Width="180"
                                    TabIndex="8" />
                                <asp:RequiredFieldValidator ID="reqtxtName4" runat="server" ControlToValidate="txtName4"
                                    ValidationGroup="CustGeneral" ErrorMessage="Name 4 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 4 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Search Term
                                <asp:Label ID="labletxtSortfield" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtSortfield" runat="server" CssClass="textbox" MaxLength="30" Width="180"
                                    TabIndex="9" />
                                <asp:RequiredFieldValidator ID="reqtxtSortfield" runat="server" ControlToValidate="txtSortfield"
                                    ValidationGroup="CustGeneral" ErrorMessage="Search Term cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Search Term cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                House number and street
                                <asp:Label ID="labletxtHouseNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtHouseNo" runat="server" CssClass="textbox" MaxLength="35" Width="180"
                                    TabIndex="10" onchange="return ActivateAddress();" />
                                <asp:RequiredFieldValidator ID="reqtxtHouseNo" runat="server" ControlToValidate="txtHouseNo"
                                    ValidationGroup="CustGeneral" ErrorMessage="House number and street cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='House number and street cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                street 2
                                <asp:Label ID="labletxtstreet2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtstreet2" runat="server" CssClass="textbox" MaxLength="40" Width="180"
                                    TabIndex="11" onchange="return ActivateAddress();" />
                                <asp:RequiredFieldValidator ID="reqtxtstreet2" runat="server" ControlToValidate="txtstreet2"
                                    ValidationGroup="CustGeneral" ErrorMessage="Street 2 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Street 2 cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                street 3
                                <asp:Label ID="labletxtstreet3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtstreet3" runat="server" CssClass="textbox" MaxLength="40" Width="180"
                                    TabIndex="12" />
                                <asp:RequiredFieldValidator ID="reqtxtstreet3" runat="server" ControlToValidate="txtstreet3"
                                    ValidationGroup="CustGeneral" ErrorMessage="Street 3 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Street 3 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                street 4 / D.L. No. 1
                                <asp:Label ID="labletxtstreet4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtstreet4" runat="server" CssClass="textbox" MaxLength="40" Width="180"
                                    TabIndex="13" />
                                <asp:RequiredFieldValidator ID="reqtxtstreet4" runat="server" ControlToValidate="txtstreet4"
                                    ValidationGroup="CustGeneral" ErrorMessage="Street 4 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Street 4 cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                street 5 / D.L. No. 2
                                <asp:Label ID="labletxtstreet5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtstreet5" runat="server" CssClass="textbox" MaxLength="40" Width="180"
                                    TabIndex="14" />
                                <asp:RequiredFieldValidator ID="reqtxtstreet5" runat="server" ControlToValidate="txtstreet5"
                                    ValidationGroup="CustGeneral" ErrorMessage="Street 5 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Street 5 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                City
                                <asp:Label ID="labletxtCity" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="textbox" MaxLength="35" Width="180"
                                    TabIndex="15" />
                                <asp:RequiredFieldValidator ID="reqtxtCity" runat="server" ControlToValidate="txtCity"
                                    ValidationGroup="CustGeneral" ErrorMessage="City cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='City cannot be blank.' />" />
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
                                C/O
                                <asp:Label ID="labletxtNameCO" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtNameCO" runat="server" CssClass="textbox" MaxLength="30" Width="180"
                                    TabIndex="16" />
                                <asp:RequiredFieldValidator ID="reqtxtNameCO" runat="server" ControlToValidate="txtNameCO"
                                    ValidationGroup="CustGeneral" ErrorMessage="C/O cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='C/O cannot be blank.' />" />
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
                                Different City
                                <asp:Label ID="labletxtDifferentCity" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtDifferentCity" runat="server" CssClass="textbox" MaxLength="35"
                                    TabIndex="17" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtDifferentCity" runat="server" ControlToValidate="txtDifferentCity"
                                    ValidationGroup="CustGeneral" ErrorMessage="Different City cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Different City cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                District
                                <asp:Label ID="labletxtDistrict" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtDistrict" runat="server" CssClass="textbox" MaxLength="35" Width="180"
                                    TabIndex="18" />
                                <asp:RequiredFieldValidator ID="reqtxtDistrict" runat="server" ControlToValidate="txtDistrict"
                                    ValidationGroup="CustGeneral" ErrorMessage="District cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='District cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                PO Box
                                <asp:Label ID="labletxtPOBox" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtPOBox" runat="server" CssClass="textbox" MaxLength="10" Width="180"
                                    TabIndex="19" />
                                <asp:RequiredFieldValidator ID="reqtxtPOBox" runat="server" ControlToValidate="txtPOBox"
                                    ValidationGroup="CustGeneral" ErrorMessage="PO Box cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                P.O. Box Postal Code
                                <asp:Label ID="labletxtPOBoxPostal" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtPOBoxPostal" runat="server" CssClass="textbox" MaxLength="10"
                                    TabIndex="20" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtPOBoxPostal" runat="server" ControlToValidate="txtPOBoxPostal"
                                    ValidationGroup="CustGeneral" ErrorMessage="PO Box cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Postal Code
                                <asp:Label ID="labletxtPostalCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtPostalCode" runat="server" CssClass="textbox" MaxLength="10"
                                    TabIndex="21" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtPostalCode" runat="server" ControlToValidate="txtPostalCode"
                                    ValidationGroup="CustGeneral" ErrorMessage="Postal Code cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Postal Code cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtPostalCode" runat="server" ControlToValidate="txtPostalCode"
                                    ValidationExpression="^[0-9]{6}$" ValidationGroup="CustGeneral" ErrorMessage="Invalid Postal Code"
                                    Enabled="true" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Postal Code.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Country
                                <asp:Label ID="lableddlCountry" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" TabIndex="22"
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
                                Region
                                <asp:Label ID="lableddlRegion" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="false" TabIndex="23"
                                    OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlRegion" runat="server" ControlToValidate="ddlRegion"
                                    ValidationGroup="CustGeneral" ErrorMessage="Region cannot be blank." InitialValue="0"
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Region cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Language
                                <asp:Label ID="labletxtLanguage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtLanguage" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    TabIndex="24" onfocus="return txtLanguageOnFocus();" onchange="return txtLanguageTextChangeEvent();"
                                    Width="130px" />
                                <asp:RequiredFieldValidator ID="reqtxtLanguage" runat="server" ControlToValidate="txtLanguage"
                                    ValidationGroup="CustGeneral" ErrorMessage="  Language cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title=' Language cannot be blank.' />" />
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
                                <asp:TextBox ID="txtMobileNum" runat="server" CssClass="textbox" MaxLength="30" Width="100"
                                    onchange="return ActivateMobile();" TabIndex="25" />
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
                                    TabIndex="26" Width="100" />
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
                                    onchange="return ActivateTel();" TabIndex="27" Width="100" />
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
                                    TabIndex="28" Width="100" />
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
                            <td class="leftTD">
                                Fax Number
                                <asp:Label ID="labletxtFaxNumber" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtFaxNumber" runat="server" CssClass="textbox" MaxLength="31" Width="180px"
                                    TabIndex="29" />
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
                            <td class="leftTD" style="width: 20%">
                                E-Mail Address
                                <asp:Label ID="labletxtEmailAddress" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="textbox" Width="180px"
                                    onchange="return ActivateEmail();" TabIndex="30" MaxLength="241" />
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
                                    onchange="return ActivateEmail();" TabIndex="31" MaxLength="241" />
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
                            <td class="leftTD" style="width: 20%">
                                E-Mail Address 3
                                <asp:Label ID="labletxtEmailAddress3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtEmailAddress3" runat="server" CssClass="textbox" Width="180px"
                                    TabIndex="32" MaxLength="241" />
                                <asp:RequiredFieldValidator ID="reqtxtEmailAddress3" runat="server" ControlToValidate="txtEmailAddress3"
                                    ValidationGroup="CustGeneral" ErrorMessage="E-Mail Address 3 cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='E-Mail Address 3 cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtEmailAddress3" runat="server" ControlToValidate="txtEmailAddress3"
                                    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                    ValidationGroup="CustGeneral" ErrorMessage="Invalid E-Mail Address 3" SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid E-Mail Address 3.' />" />
                                <asp:CompareValidator ID="CmptxtEmailAddress" runat="server" Type="String" Display="Dynamic"
                                    ValidationGroup="CustGeneral" ControlToCompare="txtEmailAddress2" ControlToValidate="txtEmailAddress3"
                                    ErrorMessage="E-Mail Address 3 cannot be same as E-Mail Address 2" Operator="NotEqual"
                                    Text="<img src='../../images/Error.png' title='E-Mail Address 3 cannot be same as E-Mail Address 2.' />"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" Type="String" Display="Dynamic"
                                    ValidationGroup="CustGeneral" ControlToCompare="txtEmailAddress" ControlToValidate="txtEmailAddress3"
                                    ErrorMessage="E-Mail Address 3 cannot be same as E-Mail Address" Operator="NotEqual"
                                    Text="<img src='../../images/Error.png' title='E-Mail Address 3 cannot be same as E-Mail Address.' />"></asp:CompareValidator>
                            </td>
                            <td class="leftTD">
                                Account Number of Vendor or Creditor
                                <asp:Label ID="labletxtAccountNumberVendor" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtAccountNumberVendor" runat="server" CssClass="textbox" MaxLength="10"
                                    TabIndex="33" Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtAccountNumberVendor" runat="server" ControlToValidate="txtAccountNumberVendor"
                                    ValidationGroup="CustGeneral" ErrorMessage="Account Number of Vendor or Creditor cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Account Number of Vendor or Creditor cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Company ID of Trading Partner
                                <asp:Label ID="labletxtCompanyIDTrading" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtCompanyIDTrading" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="34" MaxLength="6" onfocus="return txtCompanyIDTradingOnFocus();" onchange="return txtCompanyIDTradingTextChangeEvent();"
                                    Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtCompanyIDTrading" runat="server" ControlToValidate="txtCompanyIDTrading"
                                    ValidationGroup="CustGeneral" ErrorMessage="Company ID of Trading Partner cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Company ID of Trading Partner cannot be blank.' />" />
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
                                Transportation Zone
                                <asp:Label ID="lableddlTransportationZone" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlTransportationZone" runat="server" TabIndex="35">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTransportationZone" runat="server" ControlToValidate="ddlTransportationZone"
                                    ValidationGroup="CustGeneral" ErrorMessage="Transportation Zone cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Transportation Zone cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Tax Jurisdiction
                                <asp:Label ID="labletxtTaxJurisdiction" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtTaxJurisdiction" runat="server" CssClass="textbox" MaxLength="15"
                                    TabIndex="36" Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtTaxJurisdiction" runat="server" ControlToValidate="txtTaxJurisdiction"
                                    ValidationGroup="CustGeneral" ErrorMessage="Tax Jurisdiction cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax Jurisdiction cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Group key
                                <asp:Label ID="labletxtGroupkey" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtGroupkey" runat="server" CssClass="textbox" MaxLength="2" Width="100"
                                    TabIndex="37" />
                                <asp:RequiredFieldValidator ID="reqtxtGroupkey" runat="server" ControlToValidate="txtGroupkey"
                                    ValidationGroup="CustGeneral" ErrorMessage="Group key cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Group key cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Liable for VAT
                                <asp:Label ID="lablerdlLiableforVAT" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:RadioButtonList runat="server" ID="rdlLiableforVAT" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="reqrdlLiableforVAT" runat="server" ControlToValidate="rdlLiableforVAT"
                                    ValidationGroup="CustGeneral" ErrorMessage="Liable for VAT cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Liable for VAT cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Country Code
                                <asp:Label ID="labletxtCountryCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtCountryCode" runat="server" CssClass="textboxAutocomplete" MaxLength="3"
                                    Width="100" onfocus="return txtCountryCodeOnFocus();" onchange="return txtCountryCodeTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtCountryCode" runat="server" ControlToValidate="txtCountryCode"
                                    ValidationGroup="CustGeneral" ErrorMessage=" Country Code cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title=' Country Code cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                City Code
                                <asp:Label ID="labletxtCityCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtCityCode" runat="server" CssClass="textboxAutocomplete" MaxLength="4"
                                    Width="100" onfocus="return txtCityCodeOnFocus();" onchange="return txtCityCodeTextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtCityCode" runat="server" ControlToValidate="txtCityCode"
                                    ValidationGroup="CustGeneral" ErrorMessage="City Code cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='City Code cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
						<%-- Added by Swati for on 08.10.2018 --%>
                        <tr>
                            <td class="leftTD">
                                Remarks
                                <asp:Label ID="labletxtRemarks" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="textarea" TextMode="MultiLine"
                                    TabIndex="26" Columns="100" Rows="3" />
                                <asp:RequiredFieldValidator ID="reqtxtRemarks" runat="server" ControlToValidate="txtRemarks"
                                    ValidationGroup="CustGeneral" ErrorMessage="Remarks cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Remarks cannot be blank.' />" />
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
						<%-- End Change --%>
                        <tr>
                            <td class="leftTD" align="left" colspan="4">
                                <b>Attach Documents (Image/PDF Files Only)</b>
                            </td>
                        </tr>
                        <tr>
                            <td class="rigthTD" align="left" colspan="2" valign="top">
                                <div>
                                    <asp:FileUpload ID="file_upload" class="multi" runat="server" />
                                    <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />
                                </div>
                            </td>
                            <td class="rigthTD" align="left" colspan="2">
                                <asp:GridView ID="grdAttachedDocs" runat="server" AutoGenerateColumns="False" DataKeyNames="Document_Upload_Id"
                                    Visible="False" OnRowCommand="grdAttachedDocs_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Attached Documents">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttachedDocName" runat="server" Text='<%# Eval("Document_Name") %>'
                                                    Visible="false" />
                                                <asp:Label ID="lblUploadedFileName" runat="server" Text='<%# Eval("Document_Name") %>'
                                                    Visible="false" />
                                                <asp:HyperLink ID="aDocPath" runat="server" Text='<%# Eval("Document_Name") %>' NavigateUrl='<%# Eval("Document_Path") %>'
                                                    Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="X" ForeColor="Red" Font-Size="15px"
                                                    CommandName="DEL" Font-Bold="true" OnClientClick="return confirm('Are you certain you want to delete this document?');" />&nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnPrevious" runat="server" ValidationGroup="CustGeneral" Text="Back"
                                    CssClass="button" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="CustGeneral" Text="Save"
                                    CssClass="button" OnClick="btnSave_Click" />
                                <asp:Button ID="btnNext" runat="server" ValidationGroup="CustGeneral" Text="Save & Next"
                                    CssClass="button" OnClick="btnNext_Click" Width="120px" />
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
    <asp:Label ID="lblCustomerGeneralId" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="22" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <script type="text/javascript">
        function ActivateAddress() {

            if ($('#<%= txtHouseNo.ClientID%>').val().length > 0)
                $('#<%= txtstreet2.ClientID%>').attr('disabled', false);

            if ($('#<%= txtstreet2.ClientID%>').val().length > 0)
                $('#<%= txtstreet3.ClientID%>').attr('disabled', false);

            //            if ($('#<%= txtstreet3.ClientID%>').val().length > 0)
            //                $('#<%= txtstreet4.ClientID%>').attr('disabled', false);

            //            if ($('#<%= txtstreet4.ClientID%>').val().length > 0)
            //                $('#<%= txtstreet5.ClientID%>').attr('disabled', false);
        }

        function ActivateName() {

            if ($('#<%= txtName1.ClientID %>').val().length > 0)
                $('#<%= txtName2.ClientID %>').attr('disabled', false);
        }

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

            if ($('#<%= txtEmailAddress2.ClientID%>').val().length > 0)
                $('#<%= txtEmailAddress3.ClientID%>').attr('disabled', false);
        }


        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {

        });

        $(function () {

        });

        var textboxId = "";
        var textboxRealId = "";

        function txtCustomerCodeOnFocus() {
            textboxId = $('#<%= txtCustomerCode.ClientID%>').attr('ID');
            textboxRealId = "txtCustomerCode";
            AutoCompleteLookUpHeaderC();
        }

        function txtCustomerCodeTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCustomerCode.ClientID%>').attr('ID'), "txtCustomerCode", $('#<%= btnNext.ClientID%>').attr('ID'));
        }



        function txtLanguageOnFocus() {
            textboxId = $('#<%= txtLanguage.ClientID%>').attr('ID');
            textboxRealId = "txtLanguage";
            AutoCompleteLookUpHeaderC();
        }

        function txtLanguageTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtLanguage.ClientID%>').attr('ID'), "txtLanguage", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtAccountNumberVendorOnFocus() {
            textboxId = $('#<%= txtAccountNumberVendor.ClientID%>').attr('ID');
            textboxRealId = "txtAccountNumberVendor";
            AutoCompleteLookUpHeaderC();
        }

        function txtAccountNumberVendorTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtAccountNumberVendor.ClientID%>').attr('ID'), "txtAccountNumberVendor", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCompanyIDTradingOnFocus() {
            textboxId = $('#<%= txtCompanyIDTrading.ClientID%>').attr('ID');
            textboxRealId = "txtCompanyIDTrading";
            AutoCompleteLookUpHeaderC();
        }

        function txtCompanyIDTradingTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCompanyIDTrading.ClientID%>').attr('ID'), "txtCompanyIDTrading", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCountryCodeOnFocus() {
            textboxId = $('#<%= txtCountryCode.ClientID%>').attr('ID');
            textboxRealId = "txtCountryCode";
            AutoCompleteLookUpHeaderC();
        }

        function txtCountryCodeTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCountryCode.ClientID%>').attr('ID'), "txtCountryCode", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCityCodeOnFocus() {
            textboxId = $('#<%= txtCityCode.ClientID%>').attr('ID');
            textboxRealId = "txtCityCode";
            AutoCompleteLookUpHeaderC();
        }

        function txtCityCodeTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCityCode.ClientID%>').attr('ID'), "txtCityCode", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
    </script>
</asp:Content>
