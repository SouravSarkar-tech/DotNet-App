<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Vendor/VendorMasterPage.master"
    AutoEventWireup="true" CodeFile="VendorGeneral1.aspx.cs" Inherits="Transaction_Vendor_VendorGeneral1" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery.MultiFile.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetToolTip(control) {
            ddrivetip('29', control);
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
                    General Data
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="1" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" width="20%">
                                Vendor Code
                                <asp:Label ID="labletxtCustomerCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="textbox" MaxLength="10"
                                    TabIndex="1" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtCustomerCode" runat="server" ControlToValidate="txtCustomerCode"
                                    ValidationGroup="save" ErrorMessage="Vendor Code cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Code cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtCustomerCode" runat="server" ControlToValidate="txtCustomerCode"
                                    ValidationGroup="save" ErrorMessage="Vendor Code Invalid." SetFocusOnError="true"
                                    ValidationExpression="^[\S]{4,10}" Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Code Invalid.' />" />
                            </td>
                            <td class="leftTD" colspan="2">
                                <asp:Label ID="lblVendorName" runat="server" Font-Bold="true" Font-Italic="true"></asp:Label>
                            </td>
                        </tr>
                        <%--GST changes Start--%>
                        <tr runat="server" id="trSupplyPlace" visible="false">
                            <td class="leftTD" width="20%">
                                Principal place of Supply
                                <asp:Label ID="labeltxtSupplyPlace" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtSupplyPlace" runat="server" CssClass="textbox" MaxLength="10"
                                    TabIndex="1" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtSupplyPlace" runat="server" ControlToValidate="txtSupplyPlace"
                                    ValidationGroup="save" ErrorMessage="Principal place of Supply cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Principal place of Supply cannot be blank.' />"
                                    Enabled="false" />
                                <asp:RegularExpressionValidator ID="regtxtSupplyPlace" runat="server" ControlToValidate="txtSupplyPlace"
                                    ValidationGroup="save" ErrorMessage="Principal place of Supply Invalid." SetFocusOnError="true"
                                    Enabled="false" ValidationExpression="^[\d]{4,10}" Display="Dynamic" Text="<img src='../../images/Error.png' title='Principal place of Supply Invalid.' />" />
                            </td>
                            <td class="leftTD" colspan="2">
                            </td>
                        </tr>
                        <tr id="trImpVendor" runat="server" visible="false">
                            <td class="leftTD" width="20%">
                                Imp Vendor Type
                                <asp:Label ID="labelImpVendor" runat="server" ForeColor="Red" Text="*" Visible = "false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:RadioButtonList ID="rdbImpVendor" RepeatColumns="2" RepeatDirection="Horizontal"
                                    RepeatLayout="Table" runat="server" ValidationGroup="save">
                                    <asp:ListItem Value = "S">Service Vendor</asp:ListItem>
                                    <asp:ListItem Value = "G">Goods Vendor</asp:ListItem>
                                </asp:RadioButtonList>                               
                                <asp:RequiredFieldValidator ID="reqImpVendor" runat="server" ControlToValidate="rdbImpVendor"
                                    ValidationGroup="save" ErrorMessage="Select the type of Import vendor." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Select the type of Import vendor.' />"
                                    Enabled="false" />
                            </td>
                            <td class="leftTD" colspan="2">
                            </td>
                        </tr>
                        <%--GST changes End--%>
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
                                    ValidationGroup="save" ErrorMessage="Select Company Code." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company Code.' />" />
                            </td>
                            <td class="leftTD" width="20%">
                                Vendor account group
                                <asp:Label ID="lableddlVendorAccGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlVendorAccGrp" runat="server" AppendDataBoundItems="false"
                                    TabIndex="3">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlVendorAccGrp" runat="server" ControlToValidate="ddlVendorAccGrp"
                                    ValidationGroup="save" ErrorMessage="Select  Vendor account group." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select  Vendor account group.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" width="20%">
                                Purchasing Organization
                                <asp:Label ID="lableddlPurchaseOrg" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlPurchaseOrg" runat="server" AppendDataBoundItems="false"
                                    TabIndex="4">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPurchaseOrg" runat="server" ControlToValidate="ddlPurchaseOrg"
                                    ValidationGroup="save" ErrorMessage="Select Purchasing Organization." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Purchasing Organization.' />" />
                            </td>
                            <td class="leftTD" width="20%">
                                Vendor Type
                                <asp:Label ID="lableddlVendorCategory" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlVendorCategory" runat="server" AppendDataBoundItems="false"
                                    TabIndex="4">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlVendorCategory" runat="server" ControlToValidate="ddlVendorCategory"
                                    ValidationGroup="save" ErrorMessage="Select Vendor Type." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Vendor Type.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Title
                                <asp:Label ID="labletxtTitle" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="txtTitle" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                    TabIndex="4" OnSelectedIndexChanged="txtTitle_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtTitle" runat="server" CssClass="textboxAutocomplete" MaxLength="15" AutoPostBack="true"
                                    TabIndex="5" onfocus="return txtTitleOnFocus();" onchange="return txtTitleTextChangeEvent();" CausesValidation="false"
                                    Width="180" OnTextChanged="txtTitle_TextChanged" />--%>
                                <asp:RequiredFieldValidator ID="reqtxtTitle" runat="server" ControlToValidate="txtTitle"
                                    ValidationGroup="save" ErrorMessage="Title cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Title cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                <%--Meno--%>
                                Name of Proprietor
                                <asp:Label ID="labletxtMemo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtMemo" runat="server" CssClass="textbox" MaxLength="30" Width="180"
                                    TabIndex="6" />
                                <asp:RequiredFieldValidator ID="reqtxtMemo" runat="server" ControlToValidate="txtMemo"
                                    ValidationGroup="save" ErrorMessage="Name of Proprietor cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Name of Proprietor cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Vendor Name
                                <asp:Label ID="labletxtName1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtName1" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                    onchange="return ActivateName();" TabIndex="7" />
                                <asp:RequiredFieldValidator ID="reqtxtName1" runat="server" ControlToValidate="txtName1"
                                    ValidationGroup="save" ErrorMessage="Vendor Name cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Name cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Vendor Name(conti..)
                                <asp:Label ID="labletxtName2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtName2" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                    TabIndex="8" />
                                <asp:RequiredFieldValidator ID="reqtxtName2" runat="server" ControlToValidate="txtName2"
                                    ValidationGroup="save" ErrorMessage="Vendor Name(conti..) cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Name(conti..) cannot be blank.' />" />
                                <asp:CompareValidator ID="ComptxtName2" runat="server" Type="String" Display="Dynamic"
                                    ValidationGroup="save" ControlToCompare="txtName1" ControlToValidate="txtName2"
                                    ErrorMessage="Vendor Name (Conti..) cannot be same as Vendor Name." Operator="NotEqual"
                                    Text="<img src='../../images/Error.png' title='Vendor Name (Conti..) cannot be same as Vendor Name.' />"></asp:CompareValidator>
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
                                <asp:TextBox ID="txtName3" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                    TabIndex="9" />
                                <asp:RequiredFieldValidator ID="reqtxtName3" runat="server" ControlToValidate="txtName3"
                                    ValidationGroup="save" ErrorMessage="Name 3 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 3 cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                <%--Name 4--%>
                                Name 4
                                <asp:Label ID="labletxtName4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtName4" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                    TabIndex="10" />
                                <asp:RequiredFieldValidator ID="reqtxtName4" runat="server" ControlToValidate="txtName4"
                                    ValidationGroup="save" ErrorMessage="Name 4 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 4 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                <%--House number and street--%>
                                Address
                                <asp:Label ID="labletxtHouseNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtHouseNo" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                    onchange="return ActivateAddress();" TabIndex="11" />
                                <asp:RequiredFieldValidator ID="reqtxtHouseNo" runat="server" ControlToValidate="txtHouseNo"
                                    ValidationGroup="save" ErrorMessage="Address cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Address cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                <%--Street 4--%>
                                Address 1 (Conti..)
                                <asp:Label ID="labletxtStreet4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtStreet4" runat="server" CssClass="textbox" MaxLength="40" Width="210"
                                    onchange="return ActivateAddress();" TabIndex="12" />
                                <asp:RequiredFieldValidator ID="reqtxtStreet4" runat="server" ControlToValidate="txtStreet4"
                                    ValidationGroup="save" ErrorMessage="Address 1 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Address 1 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                <%--Street 5--%>
                                Address 2 (Conti..)
                                <asp:Label ID="labletxtStreet5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtStreet5" runat="server" CssClass="textbox" MaxLength="40" Width="210"
                                    TabIndex="13" />
                                <asp:RequiredFieldValidator ID="reqtxtStreet5" runat="server" ControlToValidate="txtStreet5"
                                    ValidationGroup="save" ErrorMessage="Street 5 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Street 5 cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                <%--Sort field--%>
                                Search Term
                                <asp:Label ID="labletxtSortfield" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtSortfield" runat="server" CssClass="textbox" MaxLength="20" Width="210"
                                    TabIndex="14" />
                                <asp:RequiredFieldValidator ID="reqtxtSortfield" runat="server" ControlToValidate="txtSortfield"
                                    ValidationGroup="save" ErrorMessage="Search Term cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Search Term cannot be blank.' />" />
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
                                    ValidationGroup="save" ErrorMessage="City cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='City cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                District
                                <asp:Label ID="labletxtDistrict" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtDistrict" runat="server" CssClass="textbox" MaxLength="35" Width="180"
                                    TabIndex="16" />
                                <asp:RequiredFieldValidator ID="reqtxtDistrict" runat="server" ControlToValidate="txtDistrict"
                                    ValidationGroup="save" ErrorMessage="District cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='District cannot be blank.' />" />
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
                                    TabIndex="17" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtPostalCode" runat="server" ControlToValidate="txtPostalCode"
                                    ValidationGroup="save" ErrorMessage="Postal Code cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Postal Code cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtPostalCode" runat="server" ControlToValidate="txtPostalCode"
                                    ValidationExpression="^[0-9]{6}$" ValidationGroup="save" ErrorMessage="Invalid Postal Code"
                                    Enabled="true" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Postal Code.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Country Key
                                <asp:Label ID="lableddlCountry" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" TabIndex="18"
                                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCountry" runat="server" ControlToValidate="ddlCountry"
                                    ValidationGroup="save" ErrorMessage="Country of origin cannot be blank." InitialValue="0"
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Country of origin cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Region (State, Province, County)
                                <asp:Label ID="lableddlRegion" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true" TabIndex="19"
                                    OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlRegion" runat="server" ControlToValidate="ddlRegion"
                                    ValidationGroup="save" ErrorMessage="Region cannot be blank." InitialValue="0"
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Region cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Language
                                <asp:Label ID="labletxtLanguage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtLanguage" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    TabIndex="20" onfocus="return txtLanguageOnFocus();" onchange="return txtLanguageTextChangeEvent();"
                                    Width="130px" />
                                <asp:RequiredFieldValidator ID="reqtxtLanguage" runat="server" ControlToValidate="txtLanguage"
                                    ValidationGroup="save" ErrorMessage="  Language cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title=' Language cannot be blank.' />" />
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
                                    TabIndex="21" />
                                <asp:RequiredFieldValidator ID="reqtxtPOBox" runat="server" ControlToValidate="txtPOBox"
                                    ValidationGroup="save" ErrorMessage="PO Box cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                P.O. Box Postal Code
                                <asp:Label ID="labletxtPOBoxPostal" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtPOBoxPostal" runat="server" CssClass="textbox" MaxLength="10"
                                    TabIndex="22" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtPOBoxPostal" runat="server" ControlToValidate="txtPOBoxPostal"
                                    ValidationGroup="save" ErrorMessage="PO Box cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='P.O. Box Postal Code cannot be blank.' />" />
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
                                    onchange="return ActivateMobile();" TabIndex="23" />
                                <asp:RequiredFieldValidator ID="reqtxtMobileNum" runat="server" ControlToValidate="txtMobileNum"
                                    ValidationGroup="save" ErrorMessage=" First Mobile number cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title=' First Mobile number cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtMobileNum" runat="server" ControlToValidate="txtMobileNum"
                                    ValidationExpression="^[0-9]{10}$" ValidationGroup="save" ErrorMessage="Invalid First Mobile Number"
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid First Mobile Number.' />" />
                            </td>
                            <td class="leftTD">
                                Second Mobile Number
                                <asp:Label ID="labletxtMobileNum2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtMobileNum2" runat="server" CssClass="textbox" MaxLength="30"
                                    TabIndex="24" Width="100" />
                                <asp:RequiredFieldValidator ID="reqtxtMobileNum2" runat="server" ControlToValidate="txtMobileNum2"
                                    ValidationGroup="save" ErrorMessage="Second Mobile Number cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Second Mobile Number cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtMobileNum2" runat="server" ControlToValidate="txtMobileNum2"
                                    ValidationExpression="^[0-9]{10}$" ValidationGroup="save" ErrorMessage="Invalid Second Mobile Number"
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Second Mobile Number.' />" />
                                <asp:CompareValidator ID="ComptxtMobileNum2" runat="server" Type="String" Display="Dynamic"
                                    ValidationGroup="save" ControlToCompare="txtMobileNum" ControlToValidate="txtMobileNum2"
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
                                    onchange="return ActivateTel();" TabIndex="25" Width="100" />
                                <asp:RequiredFieldValidator ID="reqtxtFirsttelephone" runat="server" ControlToValidate="txtFirsttelephone"
                                    ValidationGroup="save" ErrorMessage=" First telephone number cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' First telephone number cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Second Telephone Number
                                <asp:Label ID="labletxtSecondTelephoneNumber" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtSecondTelephoneNumber" runat="server" CssClass="textbox" MaxLength="16"
                                    TabIndex="26" Width="100" />
                                <asp:RequiredFieldValidator ID="reqtxtSecondTelephoneNumber" runat="server" ControlToValidate="txtSecondTelephoneNumber"
                                    ValidationGroup="save" ErrorMessage="Second Telephone Number cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Second Telephone Number cannot be blank.' />" />
                                <asp:CompareValidator ID="ComptxtSecondTelephoneNumber" runat="server" Type="String"
                                    Display="Dynamic" ValidationGroup="save" ControlToCompare="txtFirsttelephone"
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
                                    TabIndex="27" />
                                <asp:RequiredFieldValidator ID="reqtxtFaxNumber" runat="server" ControlToValidate="txtFaxNumber"
                                    ValidationGroup="save" ErrorMessage="Fax Number cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Fax Number cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Telex number
                                <asp:Label ID="labletxtTelex_number" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtTelex_number" runat="server" CssClass="textbox" MaxLength="10"
                                    TabIndex="28" Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtTelex_number" runat="server" ControlToValidate="txtTelex_number"
                                    ValidationGroup="save" ErrorMessage="Telex number cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Telex number cannot be blank.' />" />
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
                                    onchange="return ActivateEmail();" TabIndex="29" MaxLength="241" />
                                <asp:RequiredFieldValidator ID="reqtxtEmailAddress" runat="server" ControlToValidate="txtEmailAddress"
                                    ValidationGroup="save" ErrorMessage="E-Mail Address cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='E-Mail Address cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtEmailAddress" runat="server" ControlToValidate="txtEmailAddress"
                                    ValidationExpression="^([\w-\.]+)[\w]{1}@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                    ValidationGroup="save" ErrorMessage="Invalid E-Mail Address" SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid E-Mail Address.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                E-Mail Address 2
                                <asp:Label ID="labletxtEmailAddress2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtEmailAddress2" runat="server" CssClass="textbox" Width="180px"
                                    onchange="return ActivateEmail();" TabIndex="30" MaxLength="241" />
                                <asp:RequiredFieldValidator ID="reqtxtEmailAddress2" runat="server" ControlToValidate="txtEmailAddress2"
                                    ValidationGroup="save" ErrorMessage="E-Mail Address 2 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='E-Mail Address 2 cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtEmailAddress2" runat="server" ControlToValidate="txtEmailAddress2"
                                    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                    ValidationGroup="save" ErrorMessage="Invalid E-Mail Address 2" SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid E-Mail Address 2.' />" />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" Type="String" Display="Dynamic"
                                    ValidationGroup="save" ControlToCompare="txtEmailAddress" ControlToValidate="txtEmailAddress2"
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
                                    TabIndex="31" MaxLength="241" />
                                <asp:RequiredFieldValidator ID="reqtxtEmailAddress3" runat="server" ControlToValidate="txtEmailAddress3"
                                    ValidationGroup="save" ErrorMessage="E-Mail Address 3 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='E-Mail Address 3 cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtEmailAddress3" runat="server" ControlToValidate="txtEmailAddress3"
                                    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                    ValidationGroup="save" ErrorMessage="Invalid E-Mail Address 3" SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid E-Mail Address 3.' />" />
                                <asp:CompareValidator ID="CmptxtEmailAddress" runat="server" Type="String" Display="Dynamic"
                                    ValidationGroup="save" ControlToCompare="txtEmailAddress2" ControlToValidate="txtEmailAddress3"
                                    ErrorMessage="E-Mail Address 3 cannot be same as E-Mail Address 2" Operator="NotEqual"
                                    Text="<img src='../../images/Error.png' title='E-Mail Address 3 cannot be same as E-Mail Address 2.' />"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" Type="String" Display="Dynamic"
                                    ValidationGroup="save" ControlToCompare="txtEmailAddress" ControlToValidate="txtEmailAddress3"
                                    ErrorMessage="E-Mail Address 3 cannot be same as E-Mail Address" Operator="NotEqual"
                                    Text="<img src='../../images/Error.png' title='E-Mail Address 3 cannot be same as E-Mail Address.' />"></asp:CompareValidator>
                            </td>
                            <td class="leftTD">
                                Customer Number
                                <asp:Label ID="labletxtCustomer_Number" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtCustomer_Number')"
                                    onmouseout="hideddrivetip()" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtCustomer_Number" runat="server" CssClass="textbox" MaxLength="6"
                                    TabIndex="32" Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtCustomer_Number" runat="server" ControlToValidate="txtCustomer_Number"
                                    ValidationGroup="save" ErrorMessage="Customer Number cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Number cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Teletex Number
                                <asp:Label ID="labletxtTeletex_Number" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtTeletex_Number" runat="server" CssClass="textbox" MaxLength="2"
                                    TabIndex="33" Width="100" />
                                <asp:RequiredFieldValidator ID="reqtxtTeletex_Number" runat="server" ControlToValidate="txtTeletex_Number"
                                    ValidationGroup="save" ErrorMessage="Teletex Number cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title=' Teletex Number cannot be blank.' />" />
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
                                Authorization Group
                                <asp:Label ID="labletxtAuthorizationGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtAuthorizationGroup" runat="server" CssClass="textbox" MaxLength="2"
                                    TabIndex="34" Width="100" />
                                <asp:RequiredFieldValidator ID="reqtxtAuthorizationGroup" runat="server" ControlToValidate="txtAuthorizationGroup"
                                    ValidationGroup="save" ErrorMessage=" Authorization Group cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title=' Authorization Group cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Company ID of Trading Partner
                                <asp:Label ID="labletxtCompanyIDTrading" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtCompanyIDTrading" runat="server" CssClass="textboxAutocomplete"
                                    TabIndex="35" MaxLength="6" onfocus="return txtCompanyIDTradingOnFocus();" onchange="return txtCompanyIDTradingTextChangeEvent();"
                                    Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtCompanyIDTrading" runat="server" ControlToValidate="txtCompanyIDTrading"
                                    ValidationGroup="save" ErrorMessage="Company ID of Trading Partner cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Company ID of Trading Partner cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <%--<asp:Panel ID="pnlRemarks" runat="server" Visible="false">--%>
                        <tr>
                            <td class="leftTD">
                                Remarks
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtRemarks" runat="server" MaxLength="250" TextMode="MultiLine"
                                    Width="90%" TabIndex="37" Rows="3" />
                            </td>
                            <td class="tdSpace" colspan="2">
                            </td>
                        </tr>
                        <%--</asp:Panel>--%>
                        <tr>
                            <td colspan="4" class="tdSpace">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" align="left" colspan="4">
                                <b>Attach Documents (Image/PDF Files Only)</b>
                            </td>
                        </tr>
                        <tr runat="server" id="trMandatoryDocs">
                            <%--Vendor workflow modification Start--%>
                            <%--<td colspan="2">
                                <asp:GridView ID="grdMandDocs" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>  
                                                <asp:RadioButton ID="rdoSelection" runat="server" GroupName="selection" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />                                              
                                                
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMandDocId" runat="server" Text='<%# Eval("DocListId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Mandatory Document Name" DataField="DocName" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </td>--%>
                            <%--Vendor workflow modification End--%>
                            <td class="rigthTD" align="left" colspan="2">
                                <asp:GridView ID="grdAttachedDocs" runat="server" AutoGenerateColumns="False" DataKeyNames="Document_Upload_Id"
                                    Visible="False" OnRowCommand="grdAttachedDocs_RowCommand">
                                    <Columns>
                                        <%--Vendor workflow modification start--%>
                                        <%--<asp:TemplateField HeaderText="Document Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttachedDocType" runat="server" Text='<%# Eval("Doc_Type") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--Vendor workflow modification end--%>
                                        <asp:TemplateField HeaderText="Attached Documents">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttachedDocName" runat="server" Text='<%# Eval("Document_Name") %>'
                                                    Visible="false" />
                                                <asp:Label ID="lblUploadedFileName" runat="server" Text='<%# Eval("Document_Path") %>'
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
                                                <%-- onclick="lnkDelete_Click" --%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="rigthTD" align="left" colspan="2" valign="top">
                                <div>
                                    <%--<asp:FileUpload ID="file_upload" class="multi" runat="server" TabIndex="36" OnClientClick="return Validate();" />--%>
                                    <asp:FileUpload ID="file_upload" runat="server" TabIndex="36" />
                                    <asp:Button ID="btnUploadDoc" runat="server" Text="Upload" 
                                        OnClick="btnUploadDoc_Click" />
                                    <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />
                                </div>
                            </td>
                            <td class="tdSpace" colspan="2">
                            </td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="save" UseSubmitBehavior="false"
                                    TabIndex="38" CssClass="button" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="save" Text="Save" CssClass="button"
                                    UseSubmitBehavior="true" TabIndex="39" OnClick="btnSave_Click" />
                                <asp:Button ID="btnNext" runat="server" ValidationGroup="save" Text="Save & Next"
                                    UseSubmitBehavior="true" TabIndex="40" CssClass="button" OnClick="btnNext_Click"
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
    <asp:Label ID="lblSectionId" runat="server" Text="29" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblVendorGeneralId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <script type="text/javascript">
        function ActivateAddress() {

            if ($('#<%= txtHouseNo.ClientID%>').val().length > 0)
                $('#<%= txtStreet4.ClientID%>').attr('disabled', false);

            if ($('#<%= txtStreet4.ClientID%>').val().length > 0)
                $('#<%= txtStreet5.ClientID%>').attr('disabled', false);


        }

        function ActivateName() {

            if ($('#<%= txtName1.ClientID%>').val().length > 0)
                $('#<%= txtName2.ClientID%>').attr('disabled', false);
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
        var ActionType = $('#<%= lblActionType.ClientID%>').html();


        //        function txtTitleOnFocus() {
        //            textboxId = $('#<%= txtTitle.ClientID%>').attr('ID');
        //            textboxRealId = "txtTitle";
        //            AutoCompleteLookUpVendor();
        //        }

        //        function txtTitleTextChangeEvent() {
        //            CheckLookupVendor($('#<%= txtTitle.ClientID%>').attr('ID'), "txtTitle", $('#<%= btnNext.ClientID%>').attr('ID'));
        //        }

        function txtLanguageOnFocus() {
            textboxId = $('#<%= txtLanguage.ClientID%>').attr('ID');
            textboxRealId = "txtLanguage";
            AutoCompleteLookUpVendor();
        }

        function txtLanguageTextChangeEvent() {
            CheckLookupVendor($('#<%= txtLanguage.ClientID%>').attr('ID'), "txtLanguage", $('#<%= btnNext.ClientID%>').attr('ID'));
        }



        function txtCompanyIDTradingOnFocus() {
            textboxId = $('#<%= txtCompanyIDTrading.ClientID%>').attr('ID');
            textboxRealId = "txtCompanyIDTrading";
            AutoCompleteLookUpVendor();
        }

        function txtCompanyIDTradingTextChangeEvent() {
            CheckLookupVendor($('#<%= txtCompanyIDTrading.ClientID%>').attr('ID'), "txtCompanyIDTrading", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        

    </script>
</asp:Content>
