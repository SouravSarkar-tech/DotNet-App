<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Vendor/VendorMasterPage.master"
    AutoEventWireup="true" CodeFile="VendorGeneral2.aspx.cs" Inherits="Transaction_Vendor_VendorGeneral2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetToolTip(control) {
            ddrivetip('33', control);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updVG2" runat="server">
        <ContentTemplate>
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
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4" align="center" style="color: Red">
                                        ( Please enter 'NA' in case of not applicable for Mandatory fields. )
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        CST No.
                                        <asp:Label ID="labletxtTaxNumber1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtTaxNumber1" runat="server" CssClass="textbox" MaxLength="16"
                                            TabIndex="1" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtTaxNumber1" runat="server" ControlToValidate="txtTaxNumber1"
                                            ValidationGroup="save" ErrorMessage="CST No. cannot be blank.Enter 'NA',if not applicable."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='CST No. cannot be blank.Enter 'NA',if not applicable.' />" />
                                    </td>
                                    <td class="leftTD">
                                        CST Date
                                        <asp:Label ID="labletxtTaxNumber2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtTaxNumber2" runat="server" CssClass="textbox" MaxLength="11"
                                            onkeypress="return IsNumber();" TabIndex="2" Width="180px" />
                                        <ajax:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtTaxNumber2"
                                            Format="dd/MM/yyyy">
                                        </ajax:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTaxNumber2"
                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                            ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="reqtxtTaxNumber2" runat="server" ControlToValidate="txtTaxNumber2"
                                            ValidationGroup="save" ErrorMessage="CST Date cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='CST Date cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        LST/ VAT No.
                                        <asp:Label ID="labletxtTax_Numbe_3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtTax_Numbe_3" runat="server" CssClass="textbox" MaxLength="18"
                                            TabIndex="3" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtTax_Numbe_3" runat="server" ControlToValidate="txtTax_Numbe_3"
                                            ValidationGroup="save" ErrorMessage="LST/VAT No. cannot be blank.Enter 'NA',if not applicable."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='LST/VAT No. cannot be blank.Enter 'NA',if not applicable.' />" />
                                    </td>
                                    <td class="leftTD">
                                        LST Date
                                        <asp:Label ID="labletxtTax_Numbe_4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtTax_Numbe_4" runat="server" CssClass="textbox" MaxLength="18"
                                            TabIndex="4" onkeypress="return IsNumber();" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtTax_Numbe_4" runat="server" ControlToValidate="txtTax_Numbe_4"
                                            ValidationGroup="save" ErrorMessage="LST Date cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='LST Date be blank.' />" />
                                        <ajax:CalendarExtender ID="CaltxtTax_Numbe_4" runat="server" TargetControlID="txtTax_Numbe_4"
                                            Format="dd/MM/yyyy">
                                        </ajax:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtTax_Numbe_4"
                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                            ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        PAN
                                        <asp:Label ID="labletxtTax_Number_5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtTax_Number_5" runat="server" CssClass="textbox" MaxLength="10"
                                            TabIndex="5" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtTax_Number_5" runat="server" ControlToValidate="txtTax_Number_5"
                                            ValidationGroup="save" ErrorMessage="PAN cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='PAN cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="revtxtTax_Number_5" runat="server" ControlToValidate="txtTax_Number_5"
                                            Enabled="false" ValidationGroup="save" Display="Dynamic" ErrorMessage="Invalid PAN"
                                            Text="<img src='../../images/Error.png' title='Invalid PAN!' />" ValidationExpression="[A-Za-z]{3}(p|P|c|C|h|H|f|F|a|A|t|T|b|B|l|L|j|J|g|G)[A-Za-z]{1}[\d]{4}[A-Za-z]{1}"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="leftTD">
                                        Type of Industry
                                        <asp:Label ID="labletxtType_Industry" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtType_Industry')"
                                            onmouseout="hideddrivetip()" style="width: 16px; height: 16px; cursor: pointer;" />
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtType_Industry" runat="server" CssClass="textbox" MaxLength="16"
                                            TabIndex="6" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtType_Industry" runat="server" ControlToValidate="txtType_Industry"
                                            ValidationGroup="save" ErrorMessage="Type of Industry cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Second Telephone Number cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        VAT Registration
                                        <asp:Label ID="labletxtVAT_Registration_Number" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                                        Number
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtVAT_Registration_Number" runat="server" CssClass="textbox" MaxLength="20"
                                            TabIndex="7" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtVAT_Registration_Number" runat="server" ControlToValidate="txtVAT_Registration_Number"
                                            ValidationGroup="save" ErrorMessage="VAT Registration Number cannot be blank.Enter 'NA',if not applicable."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='VAT Registration Number cannot be blank.Enter 'NA',if not applicable.' />" />
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
                                        Service Tax
                                        <asp:Label ID="labletxtTypeOfBusiness" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtTypeOfBusiness" runat="server" CssClass="textbox" MaxLength="30"
                                            TabIndex="8" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtTypeOfBusiness" runat="server" ControlToValidate="txtTypeOfBusiness"
                                            ValidationGroup="save" ErrorMessage="Service Tax cannot be blank.Enter 'NA',if not applicable."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Service Tax cannot be blank.Enter 'NA',if not applicable.' />" />
                                    </td>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <%--GST Changes--%>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" width="20%">
                                        Vendor Tax Classification
                                        <asp:Label ID="lableddlVendorClass" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlVendorClass" runat="server" AppendDataBoundItems="false"
                                            TabIndex="9" OnSelectedIndexChanged="ddlVendorClass_SelectedIndexChanged" AutoPostBack = "true">                                             
                                            <asp:ListItem Text="--Select--" Value="-1" />
                                            <asp:ListItem Text=" - Registered" Value="" />
                                            <asp:ListItem Text="0 – Not Registered" Value="0" />
                                            <asp:ListItem Text="1 – Compounding Scheme" Value="1" />
                                            <asp:ListItem Text="2 – Special Economic Zone" Value="2" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlVendorClass" runat="server" ControlToValidate="ddlVendorClass" InitialValue = "-1"
                                            ValidationGroup="save" ErrorMessage="Select Vendor Tax Classification." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Vendor Tax Classification.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        GST No.
                                        <asp:Label ID="labletxtGSTNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtGSTNo" runat="server" CssClass="textbox" MaxLength="18" TabIndex="10"
                                            Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtGSTNo" runat="server" ControlToValidate="txtGSTNo"
                                            ValidationGroup="save" ErrorMessage="GST No. cannot be blank if vendor is Registered/Compounding Scheme/SEZ."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='GST No. cannot be blank if vendor is Registered/Compounding Scheme/SEZ.' />" />
                                    </td>                                    
                                    
                                </tr>
                                <%--GST Changes--%>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Tax Number Type
                                        <asp:Label ID="labletxtTax_Number_Type" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtTax_Number_Type" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="11" MaxLength="2" Width="180" onfocus="return txtTax_Number_TypeOnFocus();"
                                            onchange="return txtTax_Number_TypeTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtTax_Number_Type" runat="server" ControlToValidate="txtTax_Number_Type"
                                            ValidationGroup="save" ErrorMessage=" Tax Number Type cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='P.O. Box Postal Code cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Tax type
                                        <asp:Label ID="labletxtTax_Type" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtTax_Type" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                            TabIndex="12" Width="180" onfocus="return txtTax_TypeOnFocus();" onchange="return txtTax_TypeTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtTax_Type" runat="server" ControlToValidate="txtTax_Type"
                                            ValidationGroup="save" ErrorMessage="Tax type cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='District cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Tax Split
                                        <asp:Label ID="lablechkTax_Split" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkTax_Split" runat="server" Text="" TabIndex="11" />
                                    </td>
                                    <td class="leftTD">
                                        Vendor indicator relevant for proof of delivery
                                        <asp:Label ID="labletxtVendorIndicator_Relevant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtVendorIndicator_Relevant" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="13" MaxLength="8" Width="180px" onfocus="return txtVendorIndicator_RelevantOnFocus();"
                                            onchange="return txtVendorIndicator_RelevantTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtVendorIndicator_Relevant" runat="server" ControlToValidate="txtVendorIndicator_Relevant"
                                            ValidationGroup="save" ErrorMessage=" Vendor indicator relevant for proof of delivery cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Number cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Transportation zone to or from which the goods are deliv
                                        <asp:Label ID="labletxtTransportation_Zone_Goods" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtTransportation_Zone_Goods" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="14" MaxLength="10" Width="180" onfocus="return txtTransportation_Zone_GoodsOnFocus();"
                                            onchange="return txtTransportation_Zone_GoodsTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtTransportation_Zone_Goods" runat="server" ControlToValidate="txtTransportation_Zone_Goods"
                                            ValidationGroup="save" ErrorMessage="Transportation zone to or from which the goods are deliv cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='City cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Service agent procedure group
                                        <asp:Label ID="labletxtService_AgentProcedure_Group" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtService_AgentProcedure_Group" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="15" MaxLength="4" Width="180" onfocus="return txtService_AgentProcedure_GroupOnFocus();"
                                            onchange="return txtService_AgentProcedure_GroupTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtService_AgentProcedure_Group" runat="server"
                                            ControlToValidate="txtService_AgentProcedure_Group" ValidationGroup="save" ErrorMessage="Service agent procedure group cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Postal Code cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Industry key
                                        <asp:Label ID="labletxtIndustry_key" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtIndustry_key" runat="server" CssClass="textboxAutocomplete" MaxLength="4"
                                            TabIndex="16" Width="180" onfocus="return txtIndustry_keyOnFocus();" onchange="return txtIndustry_keyTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtIndustry_key" runat="server" ControlToValidate="txtIndustry_key"
                                            ValidationGroup="save" ErrorMessage=" Industry key cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Title cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        External manufacturer code name or number
                                        <asp:Label ID="labletxtExternal_Manufacturer_CodeNumber" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtExternal_Manufacturer_CodeNumber" runat="server" CssClass="textbox"
                                            TabIndex="17" MaxLength="16" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtExternal_Manufacturer_CodeNumber" runat="server"
                                            ControlToValidate="txtExternal_Manufacturer_CodeNumber" ValidationGroup="save"
                                            ErrorMessage="  External manufacturer code name or number cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title=' First telephone number cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Name of Representative
                                        <asp:Label ID="labletxtName_Representative" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtName_Representative" runat="server" CssClass="textbox" MaxLength="10"
                                            TabIndex="18" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtName_Representative" runat="server" ControlToValidate="txtName_Representative"
                                            ValidationGroup="save" ErrorMessage="Name of Representative cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Fax Number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Central deletion block for master record
                                        <asp:Label ID="lablechkCentral_Deletion_MasterRecord" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkCentral_Deletion_MasterRecord" runat="server" Text="" TabIndex="18" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Plant
                                        <asp:Label ID="labletxtPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtPlant" runat="server" CssClass="textboxAutocomplete" MaxLength="4"
                                            TabIndex="19" Width="180" onfocus="return txtPlantOnFocus();" onchange="return txtPlantTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtPlant" runat="server" ControlToValidate="txtPlant"
                                            ValidationGroup="save" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Date (batch input)
                                        <asp:Label ID="labletxtDateBatch_Input2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtDateBatch_Input2" runat="server" MaxLength="10" Width="100" CssClass="textbox"
                                            TabIndex="20" />
                                        <asp:RequiredFieldValidator ID="reqtxtDateBatch_Input2" runat="server" ControlToValidate="txtDateBatch_Input2"
                                            ValidationGroup="save" ErrorMessage=" Date (batch input) cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title=' Teletex Number cannot be blank.' />" />
                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateBatch_Input2"
                                            Format="dd/MM/yyyy">
                                        </ajax:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDateBatch_Input2"
                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                            ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Title
                                        <asp:Label ID="labletxtTitle" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="textboxAutocomplete" MaxLength="35"
                                            TabIndex="21" Width="180px" onfocus="return txtTitleOnFocus();" onchange="return txtTitleTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtTitle" runat="server" ControlToValidate="txtTitle"
                                            ValidationGroup="save" ErrorMessage="Title cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax Number 1 cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Factory calendar key
                                        <asp:Label ID="labletxtFactoryCalendar_key" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFactoryCalendar_key" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="22" MaxLength="20" Width="100" onfocus="return txtFactoryCalendar_keyOnFocus();"
                                            onchange="return txtFactoryCalendar_keyTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtFactoryCalendar_key" runat="server" ControlToValidate="txtFactoryCalendar_key"
                                            ValidationGroup="save" ErrorMessage="Factory calendar key cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax Number 2 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Name 1
                                        <asp:Label ID="labletxtName_1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtName_1" runat="server" CssClass="textbox" MaxLength="35" Width="100"
                                            TabIndex="23" />
                                        <asp:RequiredFieldValidator ID="reqtxtName_1" runat="server" ControlToValidate="txtName_1"
                                            ValidationGroup="save" ErrorMessage=" Name 1 cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title=' Authorization Group cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Name 2
                                        <asp:Label ID="labletxtName_2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtName_2" runat="server" CssClass="textbox" MaxLength="35" Width="180px"
                                            TabIndex="24" />
                                        <asp:RequiredFieldValidator ID="reqtxtName_2" runat="server" ControlToValidate="txtName_2"
                                            ValidationGroup="save" ErrorMessage="Name 2 cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Company ID of Trading Partner cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Name 3
                                        <asp:Label ID="labletxtName_3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtName_3" runat="server" CssClass="textbox" MaxLength="35" Width="180px"
                                            TabIndex="25" />
                                        <asp:RequiredFieldValidator ID="reqtxtName_3" runat="server" ControlToValidate="txtName_3"
                                            ValidationGroup="save" ErrorMessage="Name 3 cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax Number 1 cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        First Name
                                        <asp:Label ID="labletxtFirst_Name" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFirst_Name" runat="server" CssClass="textbox" MaxLength="35"
                                            TabIndex="26" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtFirst_Name" runat="server" ControlToValidate="txtFirst_Name"
                                            ValidationGroup="save" ErrorMessage=" First Name cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax Number 2 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Transportation Chain
                                        <asp:Label ID="labletxtTransportation_Chain" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtTransportation_Chain" runat="server" CssClass="textbox" MaxLength="2"
                                            TabIndex="27" Width="180px" />
                                        <asp:RequiredFieldValidator ID="reqtxtTransportation_Chain" runat="server" ControlToValidate="txtTransportation_Chain"
                                            ValidationGroup="save" ErrorMessage="Transportation Chain cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax Number 1 cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Staging Time in Days (Batch Input)
                                        <asp:Label ID="labletxtStagingTime_Days_BatchInput" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtStagingTime_Days_BatchInput" runat="server" CssClass="textbox"
                                            TabIndex="28" MaxLength="10" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtStagingTime_Days_BatchInput" runat="server"
                                            ControlToValidate="txtStagingTime_Days_BatchInput" ValidationGroup="save" ErrorMessage=" Staging Time in Days (Batch Input) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax Number 2 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Cross Docking: Relevant for Collective Numbering
                                        <asp:Label ID="lablechkCrossDocking_Relevant_CollectiveNumbering" runat="server"
                                            ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkCrossDocking_Relevant_CollectiveNumbering" runat="server" Text=""
                                            TabIndex="29" />
                                    </td>
                                    <td class="leftTD">
                                        Scheduling Procedure
                                        <asp:Label ID="lablechkScheduling_Procedure" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkScheduling_Procedure" runat="server" Text="" TabIndex="30" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Tax Jurisdiction
                                        <asp:Label ID="labletxtTax_Jurisdiction" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtTax_Jurisdiction" runat="server" CssClass="textbox" MaxLength="15"
                                            TabIndex="31" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtTax_Jurisdiction" runat="server" ControlToValidate="txtTax_Jurisdiction"
                                            ValidationGroup="save" ErrorMessage="Tax Jurisdiction cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='House number and street cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Date (batch input)
                                        <asp:Label ID="labletxtDateBatch_Input" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtDateBatch_Input" runat="server" CssClass="textbox" MaxLength="10"
                                            TabIndex="32" Width="100" />
                                        <asp:RequiredFieldValidator ID="reqtxtDateBatch_Input" runat="server" ControlToValidate="txtDateBatch_Input"
                                            ValidationGroup="save" ErrorMessage="Date (batch input) cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 4 cannot be blank.' />" />
                                        <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateBatch_Input"
                                            Format="dd/MM/yyyy">
                                        </ajax:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="revDTTest" runat="server" ControlToValidate="txtDateBatch_Input"
                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                            ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Place of birth of the person subject to withholding tax
                                        <asp:Label ID="labletxtPlaceBirth_WithholdingTax" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtPlaceBirth_WithholdingTax" runat="server" CssClass="textbox"
                                            TabIndex="33" MaxLength="25" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtPlaceBirth_WithholdingTax" runat="server" ControlToValidate="txtPlaceBirth_WithholdingTax"
                                            ValidationGroup="save" ErrorMessage=" Place of birth of the person subject to withholding tax cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 3 cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Key for the Sex of the Person Subject to Withholding Tax
                                        <asp:Label ID="labletxtKeySex_PersonWithholding_Tax" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtKeySex_PersonWithholding_Tax" runat="server" CssClass="textboxAutocomplete"
                                            TabIndex="34" MaxLength="1" Width="180" onfocus="return txtKeySex_PersonWithholding_TaxOnFocus();"
                                            onchange="return txtKeySex_PersonWithholding_TaxTextChangeEvent();" />
                                        <asp:RequiredFieldValidator ID="reqtxtKeySex_PersonWithholding_Tax" runat="server"
                                            ControlToValidate="txtKeySex_PersonWithholding_Tax" ValidationGroup="save" ErrorMessage="Key for the Sex of the Person Subject to Withholding Tax cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Sort field cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        ECC Number
                                        <asp:Label ID="labletxtECCNumber" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtECCNumber" runat="server" CssClass="textbox" TabIndex="35" MaxLength="40"
                                            Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtECCNumber" runat="server" ControlToValidate="txtECCNumber"
                                            ValidationGroup="save" ErrorMessage="ECC Number cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='ECC Number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Excise Registration No
                                        <asp:Label ID="labletxtExciseRegistrationNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtExciseRegistrationNo" runat="server" CssClass="textbox" TabIndex="36"
                                            MaxLength="40" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtExciseRegistrationNo" runat="server" ControlToValidate="txtExciseRegistrationNo"
                                            ValidationGroup="save" ErrorMessage="Excise Registration No cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Excise Registration No cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Excise Range
                                        <asp:Label ID="labletxtExciseRange" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtExciseRange" runat="server" CssClass="textbox" TabIndex="37"
                                            MaxLength="60" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtExciseRange" runat="server" ControlToValidate="txtExciseRange"
                                            ValidationGroup="save" ErrorMessage="Excise Range cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Excise Range cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Excise Division
                                        <asp:Label ID="labletxtExciseDivision" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtExciseDivision" runat="server" CssClass="textbox" TabIndex="38"
                                            MaxLength="60" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtExciseDivision" runat="server" ControlToValidate="txtExciseDivision"
                                            ValidationGroup="save" ErrorMessage="Excise Division cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Excise Division cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Excise Commissionerate
                                        <asp:Label ID="labletxtExciseCommissionerate" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtExciseCommissionerate" runat="server" CssClass="textbox" TabIndex="39"
                                            MaxLength="60" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtExciseCommissionerate" runat="server" ControlToValidate="txtExciseCommissionerate"
                                            ValidationGroup="save" ErrorMessage="Excise Commissionerate cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Excise Commissionerate cannot be blank.' />" />
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
                                        <asp:Button ID="btnPrevious" runat="server" ValidationGroup="save" Text="Back" TabIndex="40"
                                            UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="save" Text="Save" CssClass="button"
                                            TabIndex="41" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnNext" runat="server" ValidationGroup="save" Text="Save & Next"
                                            TabIndex="42" UseSubmitBehavior="true" CssClass="button" OnClick="btnNext_Click"
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
            <asp:Label ID="lblSectionId" runat="server" Text="33" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblVendorGeneralId" runat="server" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
            <asp:Label ID="lblTinNo" runat="server" Visible="false" />
            <asp:Label ID="lblVendorCategory" runat="server" Visible="false" />
            <%--GST changes--%>
            <asp:Label ID="lblGstNo" runat="server" Visible="false" />
            <%--GST changes--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">









        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {

        });

        $(function () {

        });

        var ActionType = $('#<%= lblActionType.ClientID%>').html();

        function txtIndustry_keyOnFocus() {
            textboxId = $('#<%= txtIndustry_key.ClientID%>').attr('ID');
            textboxRealId = "txtIndustry_key";
            AutoCompleteLookUpVendor();
        }

        function txtIndustry_keyTextChangeEvent() {
            CheckLookupVendor($('#<%= txtIndustry_key.ClientID%>').attr('ID'), "txtIndustry_key", $('#<%= btnNext.ClientID%>').attr('ID'));
        }



        function txtVAT_Registration_NumberOnFocus() {
            textboxId = $('#<%= txtVAT_Registration_Number.ClientID%>').attr('ID');
            textboxRealId = "txtVAT_Registration_Number";
            AutoCompleteLookUpVendor();
        }

        function txtVAT_Registration_NumberTextChangeEvent() {
            CheckLookupVendor($('#<%= txtVAT_Registration_Number.ClientID%>').attr('ID'), "txtVAT_Registration_Number", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtPlaceBirth_WithholdingTaxOnFocus() {
            textboxId = $('#<%= txtPlaceBirth_WithholdingTax.ClientID%>').attr('ID');
            textboxRealId = "txtPlaceBirth_WithholdingTax";
            AutoCompleteLookUpVendor();
        }

        function txtPlaceBirth_WithholdingTaxTextChangeEvent() {
            CheckLookupVendor($('#<%= txtPlaceBirth_WithholdingTax.ClientID%>').attr('ID'), "txtPlaceBirth_WithholdingTax", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtKeySex_PersonWithholding_TaxOnFocus() {
            textboxId = $('#<%= txtKeySex_PersonWithholding_Tax.ClientID%>').attr('ID');
            textboxRealId = "txtKeySex_PersonWithholding_Tax";
            AutoCompleteLookUpVendor();
        }

        function txtKeySex_PersonWithholding_TaxTextChangeEvent() {
            CheckLookupVendor($('#<%= txtKeySex_PersonWithholding_Tax.ClientID%>').attr('ID'), "txtKeySex_PersonWithholding_Tax", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtTax_JurisdictionOnFocus() {
            textboxId = $('#<%= txtTax_Jurisdiction.ClientID%>').attr('ID');
            textboxRealId = "txtTax_Jurisdiction";
            AutoCompleteLookUpVendor();
        }

        function txtTax_JurisdictionTextChangeEvent() {
            CheckLookupVendor($('#<%= txtTax_Jurisdiction.ClientID%>').attr('ID'), "txtTax_Jurisdiction", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtPlantOnFocus() {
            textboxId = $('#<%= txtPlant.ClientID%>').attr('ID');
            textboxRealId = "txtPlant";
            AutoCompleteLookUpVendor();
        }

        function txtPlantTextChangeEvent() {
            CheckLookupVendor($('#<%= txtPlant.ClientID%>').attr('ID'), "txtPlant", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtTransportation_Zone_GoodsOnFocus() {
            textboxId = $('#<%= txtTransportation_Zone_Goods.ClientID%>').attr('ID');
            textboxRealId = "txtTransportation_Zone_Goods";
            AutoCompleteLookUpVendor();
        }

        function txtTransportation_Zone_GoodsTextChangeEvent() {
            CheckLookupVendor($('#<%= txtTransportation_Zone_Goods.ClientID%>').attr('ID'), "txtTransportation_Zone_Goods", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtService_AgentProcedure_GroupOnFocus() {
            textboxId = $('#<%= txtService_AgentProcedure_Group.ClientID%>').attr('ID');
            textboxRealId = "txtService_AgentProcedure_Group";
            AutoCompleteLookUpVendor();
        }

        function txtService_AgentProcedure_GroupTextChangeEvent() {
            CheckLookupVendor($('#<%= txtService_AgentProcedure_Group.ClientID%>').attr('ID'), "txtService_AgentProcedure_Group", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtTax_TypeOnFocus() {
            textboxId = $('#<%= txtTax_Type.ClientID%>').attr('ID');
            textboxRealId = "txtTax_Type";
            AutoCompleteLookUpVendor();
        }

        function txtTax_TypeTextChangeEvent() {
            CheckLookupVendor($('#<%= txtTax_Type.ClientID%>').attr('ID'), "txtTax_Type", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtTax_Number_TypeOnFocus() {
            textboxId = $('#<%= txtTax_Number_Type.ClientID%>').attr('ID');
            textboxRealId = "txtTax_Number_Type";
            AutoCompleteLookUpVendor();
        }

        function txtTax_Number_TypeTextChangeEvent() {
            CheckLookupVendor($('#<%= txtTax_Number_Type.ClientID%>').attr('ID'), "txtTax_Number_Type", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtTax_Numbe_3OnFocus() {
            textboxId = $('#<%= txtTax_Numbe_3.ClientID%>').attr('ID');
            textboxRealId = "txtTax_Numbe_3";
            AutoCompleteLookUpVendor();
        }

        function txtTax_Numbe_3TextChangeEvent() {
            CheckLookupVendor($('#<%= txtTax_Numbe_3.ClientID%>').attr('ID'), "txtTax_Numbe_3", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtTax_Numbe_4OnFocus() {
            textboxId = $('#<%= txtTax_Numbe_4.ClientID%>').attr('ID');
            textboxRealId = "txtTax_Numbe_4";
            AutoCompleteLookUpVendor();
        }

        function txtTax_Numbe_4TextChangeEvent() {
            CheckLookupVendor($('#<%= txtTax_Numbe_4.ClientID%>').attr('ID'), "txtTax_Numbe_4", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtExternal_Manufacturer_CodeNumberOnFocus() {
            textboxId = $('#<%= txtExternal_Manufacturer_CodeNumber.ClientID%>').attr('ID');
            textboxRealId = "txtExternal_Manufacturer_CodeNumber";
            AutoCompleteLookUpVendor();
        }

        function txtExternal_Manufacturer_CodeNumberTextChangeEvent() {
            CheckLookupVendor($('#<%= txtExternal_Manufacturer_CodeNumber.ClientID%>').attr('ID'), "txtExternal_Manufacturer_CodeNumber", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtName_RepresentativeOnFocus() {
            textboxId = $('#<%= txtName_Representative.ClientID%>').attr('ID');
            textboxRealId = "txtName_Representative";
            AutoCompleteLookUpVendor();
        }

        function txtName_RepresentativeTextChangeEvent() {
            CheckLookupVendor($('#<%= txtName_Representative.ClientID%>').attr('ID'), "txtName_Representative", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtVendorIndicator_RelevantOnFocus() {
            textboxId = $('#<%= txtVendorIndicator_Relevant.ClientID%>').attr('ID');
            textboxRealId = "txtVendorIndicator_Relevant";
            AutoCompleteLookUpVendor();
        }

        function txtVendorIndicator_RelevantTextChangeEvent() {
            CheckLookupVendor($('#<%= txtVendorIndicator_Relevant.ClientID%>').attr('ID'), "txtVendorIndicator_Relevant", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtName_1OnFocus() {
            textboxId = $('#<%= txtName_1.ClientID%>').attr('ID');
            textboxRealId = "txtName_1";
            AutoCompleteLookUpVendor();
        }

        function txtName_1TextChangeEvent() {
            CheckLookupVendor($('#<%= txtName_1.ClientID%>').attr('ID'), "txtName_1", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtName_2OnFocus() {
            textboxId = $('#<%= txtName_2.ClientID%>').attr('ID');
            textboxRealId = "txtName_2";
            AutoCompleteLookUpVendor();
        }

        function txtName_2TextChangeEvent() {
            CheckLookupVendor($('#<%= txtName_2.ClientID%>').attr('ID'), "txtName_2", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtName_3OnFocus() {
            textboxId = $('#<%= txtName_3.ClientID%>').attr('ID');
            textboxRealId = "txtName_3";
            AutoCompleteLookUpVendor();
        }

        function txtName_3TextChangeEvent() {
            CheckLookupVendor($('#<%= txtName_3.ClientID%>').attr('ID'), "txtName_3", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtFirst_NameOnFocus() {
            textboxId = $('#<%= txtFirst_Name.ClientID%>').attr('ID');
            textboxRealId = "txtFirst_Name";
            AutoCompleteLookUpVendor();
        }

        function txtFirst_NameTextChangeEvent() {
            CheckLookupVendor($('#<%= txtFirst_Name.ClientID%>').attr('ID'), "txtFirst_Name", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtTitleOnFocus() {
            textboxId = $('#<%= txtTitle.ClientID%>').attr('ID');
            textboxRealId = "txtTitle";
            AutoCompleteLookUpVendor();
        }

        function txtTitleTextChangeEvent() {
            CheckLookupVendor($('#<%= txtTitle.ClientID%>').attr('ID'), "txtTitle", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtFactoryCalendar_keyOnFocus() {
            textboxId = $('#<%= txtFactoryCalendar_key.ClientID%>').attr('ID');
            textboxRealId = "txtFactoryCalendar_key";
            AutoCompleteLookUpVendor();
        }

        function txtFactoryCalendar_keyTextChangeEvent() {
            CheckLookupVendor($('#<%= txtFactoryCalendar_key.ClientID%>').attr('ID'), "txtFactoryCalendar_key", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtTransportation_ChainOnFocus() {
            textboxId = $('#<%= txtTransportation_Chain.ClientID%>').attr('ID');
            textboxRealId = "txtTransportation_Chain";
            AutoCompleteLookUpVendor();
        }

        function txtTransportation_ChainTextChangeEvent() {
            CheckLookupVendor($('#<%= txtTransportation_Chain.ClientID%>').attr('ID'), "txtTransportation_Chain", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtStagingTime_Days_BatchInputOnFocus() {
            textboxId = $('#<%= txtStagingTime_Days_BatchInput.ClientID%>').attr('ID');
            textboxRealId = "txtStagingTime_Days_BatchInput";
            AutoCompleteLookUpVendor();
        }

        function txtStagingTime_Days_BatchInputTextChangeEvent() {
            CheckLookupVendor($('#<%= txtStagingTime_Days_BatchInput.ClientID%>').attr('ID'), "txtStagingTime_Days_BatchInput", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtTax_Number_5OnFocus() {
            textboxId = $('#<%= txtTax_Number_5.ClientID%>').attr('ID');
            textboxRealId = "txtTax_Number_5";
            AutoCompleteLookUpVendor();
        }

        function txtTax_Number_5TextChangeEvent() {
            CheckLookupVendor($('#<%= txtTax_Number_5.ClientID%>').attr('ID'), "txtTax_Number_5", $('#<%= btnNext.ClientID%>').attr('ID'));
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
