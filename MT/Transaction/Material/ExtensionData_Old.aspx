<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="ExtensionData_Old.aspx.cs" Inherits="Transaction_Material_ExtensionData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../js/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../../stylesheet/gridviewScroll.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialExtension" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlAddNew" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Plant Extension
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <asp:GridView ID="Grd_Extension" runat="server" Width="100%" AutoGenerateColumns="false"
                                HorizontalAlign="Left" ShowFooter="true" ShowHeaderWhenEmpty="false" GridLines="None"
                                EmptyDataText="No Record Found" OnDataBound="Grd_Extension_DataBound">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCopy" runat="server" Text="Copy" OnClick="lnkCopy_Click"></asp:LinkButton>
                                            /
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Del" OnClick="lnkDelete_Click"></asp:LinkButton>
                                            <asp:Label ID="lblMatCreateExtensionId" runat="server" Text='<%# Eval("Mat_Create_Extension_Id") %>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkSave" runat="server" Text="Add New" OnClick="lnkSave_Click"
                                                ValidationGroup="ExtnAdd"></asp:LinkButton>
                                            <asp:Label ID="lblMatCreateExtensionIdf" runat="server" Text="0" Visible="false"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Plant">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                                ValidationGroup="Extn" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlPlantf" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlPlantf_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPlantf" runat="server" ControlToValidate="ddlPlantf"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Storage Location">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlStorageLocation" runat="server" AppendDataBoundItems="false">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlStorageLocation" runat="server" ControlToValidate="ddlStorageLocation"
                                                ValidationGroup="Extn" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlStorageLocationf" runat="server" AppendDataBoundItems="false">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlStorageLocationf" runat="server" ControlToValidate="ddlStorageLocationf"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Org.">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlSalesOrginization" runat="server" AutoPostBack="true" AppendDataBoundItems="false"
                                                OnSelectedIndexChanged="ddlSalesOrginization_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSalesOrginization" runat="server" ControlToValidate="ddlSalesOrginization"
                                                ValidationGroup="Extn" ErrorMessage="Sales Organization cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlSalesOrginizationf" runat="server" AutoPostBack="true" AppendDataBoundItems="false"
                                                OnSelectedIndexChanged="ddlSalesOrginizationf_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSalesOrginizationf" runat="server" ControlToValidate="ddlSalesOrginizationf"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Sales Organization cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distribution Channel">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlDistributionChannel" runat="server" AppendDataBoundItems="false">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlDistributionChannel" runat="server" ControlToValidate="ddlDistributionChannel"
                                                ValidationGroup="Extn" ErrorMessage="Distribution Channel cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlDistributionChannelf" runat="server" AppendDataBoundItems="false">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlDistributionChannelf" runat="server" ControlToValidate="ddlDistributionChannelf"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Distribution Channel cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Pricing">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlMaterialPGroup" runat="server" AppendDataBoundItems="false">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMaterialPGroup" runat="server" ControlToValidate="ddlMaterialPGroup"
                                                ValidationGroup="Extn" ErrorMessage="Material Pricing Group cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Pricing Group cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlMaterialPGroupf" runat="server" AppendDataBoundItems="false">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMaterialPGroupf" runat="server" ControlToValidate="ddlMaterialPGroupf"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Material Pricing Group cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Pricing Group cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Acc. Assignment Grp.">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlAccountAssignment" runat="server" AppendDataBoundItems="false">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlAccountAssignment" runat="server" ControlToValidate="ddlAccountAssignment"
                                                ValidationGroup="Extn" ErrorMessage="Account assignment group for this material cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Account assignment group for this material cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlAccountAssignmentf" runat="server" AppendDataBoundItems="false">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlAccountAssignmentf" runat="server" ControlToValidate="ddlAccountAssignmentf"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Account assignment group for this material cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Account assignment group for this material cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purchasing Group">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlPurchasingGroup" runat="server" AppendDataBoundItems="false">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPurchasingGroup" runat="server" ControlToValidate="ddlPurchasingGroup"
                                                ValidationGroup="Extn" ErrorMessage="Purchasing Group cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Group cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlPurchasingGroupf" runat="server" AppendDataBoundItems="false">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPurchasingGroupf" runat="server" ControlToValidate="ddlPurchasingGroupf"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Purchasing Group cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Group cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRP Type">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlMrpType" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlMrpType_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMrpType" runat="server" ControlToValidate="ddlMrpType"
                                                ValidationGroup="Extn" ErrorMessage="MRP Type cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='MRP Type cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlMrpTypef" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                TabIndex="5" OnSelectedIndexChanged="ddlMrpTypef_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMrpTypef" runat="server" ControlToValidate="ddlMrpTypef"
                                                ValidationGroup="ExtnAdd" ErrorMessage="MRP Type cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='MRP Type cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRP Controller">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlMrpController" runat="server" AppendDataBoundItems="false"
                                                TabIndex="9">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMrpController" runat="server" ControlToValidate="ddlMrpController"
                                                ValidationGroup="Extn" ErrorMessage="MRP Controller (Materials Planner) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='MRP Controller (Materials Planner) cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlMrpControllerf" runat="server" AppendDataBoundItems="false"
                                                TabIndex="9">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMrpControllerf" runat="server" ControlToValidate="ddlMrpControllerf"
                                                ValidationGroup="ExtnAdd" ErrorMessage="MRP Controller (Materials Planner) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='MRP Controller (Materials Planner) cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reorder point">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtReorder" runat="server" CssClass="textbox" Width="100px" MaxLength="13"
                                                Text='<%# Eval("Reorder_Point") %>' TabIndex="6" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtReorder" runat="server" ControlToValidate="txtReorder"
                                                ValidationGroup="Extn" ErrorMessage="Reorder point  cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Reorder point  cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtReorderf" runat="server" CssClass="textbox" Width="100px" MaxLength="13"
                                                TabIndex="6" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtReorderf" runat="server" ControlToValidate="txtReorderf"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Reorder point  cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Reorder point  cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lot size">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlLotSize" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                TabIndex="10" OnSelectedIndexChanged="ddlLotSize_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlLotSize" runat="server" ControlToValidate="ddlLotSize"
                                                ValidationGroup="Extn" ErrorMessage="Lot size (materials planning) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Lot size (materials planning) cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlLotSizef" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                TabIndex="10" OnSelectedIndexChanged="ddlLotSizef_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlLotSizef" runat="server" ControlToValidate="ddlLotSizef"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Lot size (materials planning) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Lot size (materials planning) cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Minimum lot size">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMinLotSize" runat="server" CssClass="textbox" Width="100px" MaxLength="13"
                                                Text='<%# Eval("Min_Lot_Size") %>' TabIndex="11" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtMinLotSize" runat="server" ControlToValidate="txtMinLotSize"
                                                ValidationGroup="Extn" ErrorMessage="Minimum lot size cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Minimum lot size cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtMinLotSizef" runat="server" CssClass="textbox" Width="100px"
                                                MaxLength="13" TabIndex="11" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtMinLotSizef" runat="server" ControlToValidate="txtMinLotSizef"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Minimum lot size cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Minimum lot size cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maximum lot size">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMaxLotSize" runat="server" CssClass="textbox" Width="100px" MaxLength="13"
                                                Text='<%# Eval("Max_Lot_Size") %>' TabIndex="12" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtMaxLotSize" runat="server" ControlToValidate="txtMaxLotSize"
                                                ValidationGroup="Extn" ErrorMessage="Maximum lot size cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Maximum lot size cannot be blank.' />" />
                                            <asp:CompareValidator ID="ComtxtMaxLotSize" runat="server" ControlToValidate="txtMaxLotSize"
                                                ControlToCompare="txtMinLotSize" Operator="GreaterThanEqual" ValidationGroup="Extn"
                                                Type="Integer" ErrorMessage="Maximum lot size cannot be less than Minimum lot size."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Maximum lot size cannot be less than Minimum lot size.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtMaxLotSizef" runat="server" CssClass="textbox" Width="100px"
                                                MaxLength="13" TabIndex="12" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtMaxLotSizef" runat="server" ControlToValidate="txtMaxLotSizef"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Maximum lot size cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Maximum lot size cannot be blank.' />" />
                                            <asp:CompareValidator ID="ComtxtMaxLotSizef" runat="server" ControlToValidate="txtMaxLotSizef"
                                                ControlToCompare="txtMinLotSizef" Operator="GreaterThanEqual" ValidationGroup="ExtnAdd"
                                                Type="Integer" ErrorMessage="Maximum lot size cannot be less than Minimum lot size."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Maximum lot size cannot be less than Minimum lot size.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fixed lot size">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFixedLotSize" runat="server" CssClass="textbox" Width="100px"
                                                Text='<%# Eval("Fixed_Lot_Size") %>' TabIndex="13" onkeypress="return IsNumber();"
                                                MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtFixedLotSize" runat="server" ControlToValidate="txtFixedLotSize"
                                                ValidationGroup="Extn" ErrorMessage="Fixed lot size  cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Fixed lot size  cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFixedLotSizef" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="13" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtFixedLotSizef" runat="server" ControlToValidate="txtFixedLotSizef"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Fixed lot size  cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Fixed lot size  cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rounding value">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRoundingValue" runat="server" CssClass="textbox" Width="100px"
                                                Text='<%# Eval("Rounding_Value") %>' TabIndex="16" onkeypress="return IsNumber();"
                                                MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtRoundingValue" runat="server" ControlToValidate="txtRoundingValue"
                                                ValidationGroup="Extn" ErrorMessage="Rounding value for purchase order quantity cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Rounding value for purchase order quantity cannot be blank.' />" />
                                            <asp:CompareValidator ID="ComtxtRoundingValue" runat="server" ControlToValidate="txtRoundingValue"
                                                Type="Integer" ControlToCompare="txtMaxLotSize" Operator="LessThanEqual" ValidationGroup="MRP1"
                                                ErrorMessage="Rounding value cannot be greater than Maximum lot size." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Rounding value cannot be greater than Maximum lot size.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRoundingValuef" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="16" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtRoundingValuef" runat="server" ControlToValidate="txtRoundingValuef"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Rounding value for purchase order quantity cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Rounding value for purchase order quantity cannot be blank.' />" />
                                            <asp:CompareValidator ID="ComtxtRoundingValuef" runat="server" ControlToValidate="txtRoundingValuef"
                                                Type="Integer" ControlToCompare="txtMaxLotSizef" Operator="LessThanEqual" ValidationGroup="ExtnAdd"
                                                ErrorMessage="Rounding value cannot be greater than Maximum lot size." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Rounding value cannot be greater than Maximum lot size.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prod. Storage Location">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlIssueStorageLocation" runat="server" AppendDataBoundItems="false"
                                                TabIndex="5">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlIssueStorageLocation" runat="server" ControlToValidate="ddlIssueStorageLocation"
                                                ValidationGroup="Extn" ErrorMessage="Issue Storage Location cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Issue Storage Location cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlIssueStorageLocationf" runat="server" AppendDataBoundItems="false"
                                                TabIndex="5">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlIssueStorageLocationf" runat="server" ControlToValidate="ddlIssueStorageLocationf"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Issue Storage Location cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Issue Storage Location cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GR Processing Time">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGRProcessingTime" runat="server" CssClass="textbox" Width="100px"
                                                Text='<%# Eval("GR_Processing_Time") %>' TabIndex="12" onkeypress="return IsNumber();"
                                                MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtGRProcessingTime" runat="server" ControlToValidate="txtGRProcessingTime"
                                                ValidationGroup="Extn" ErrorMessage="GR Processing Time cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='GR Processing Time cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtGRProcessingTimef" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="12" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtGRProcessingTimef" runat="server" ControlToValidate="txtGRProcessingTimef"
                                                ValidationGroup="ExtnAdd" ErrorMessage="GR Processing Time cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='GR Processing Time cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Planned Delivery Time in Days">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPlannedDeleveryTime" runat="server" CssClass="textbox" Width="100px"
                                                Text='<%# Eval("Planned_Delivery_Time_Days") %>' TabIndex="11" onkeypress="return IsNumber();"
                                                MaxLength="3" />
                                            <asp:RequiredFieldValidator ID="reqtxtPlannedDeleveryTime" runat="server" ControlToValidate="txtPlannedDeleveryTime"
                                                ValidationGroup="Extn" ErrorMessage="Planned Delivery Time in Days  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planned Delivery Time in Days  cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtPlannedDeleveryTimef" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="11" onkeypress="return IsNumber();" MaxLength="3" />
                                            <asp:RequiredFieldValidator ID="reqtxtPlannedDeleveryTimef" runat="server" ControlToValidate="txtPlannedDeleveryTimef"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Planned Delivery Time in Days  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planned Delivery Time in Days  cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Profit Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlProfitCenter" runat="server" AppendDataBoundItems="false"
                                                TabIndex="9">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlProfitCenter" runat="server" ControlToValidate="ddlProfitCenter"
                                                ValidationGroup="Extn" ErrorMessage="Profit Center cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Profit Center cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlProfitCenterf" runat="server" AppendDataBoundItems="false"
                                                TabIndex="9">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlProfitCenterf" runat="server" ControlToValidate="ddlProfitCenterf"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Profit Center cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Profit Center cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valuation Class">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlValuationClass" runat="server" AppendDataBoundItems="false"
                                                TabIndex="4">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlValuationClass" runat="server" ControlToValidate="ddlValuationClass"
                                                ValidationGroup="Extn" ErrorMessage="Valuation Class cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Class cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlValuationClassf" runat="server" AppendDataBoundItems="false"
                                                TabIndex="4">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlValuationClassf" runat="server" ControlToValidate="ddlValuationClassf"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Valuation Class cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Class cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price Control">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlPriceControlIndicator" runat="server" AppendDataBoundItems="false"
                                                TabIndex="5">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPriceControlIndicator" runat="server" ControlToValidate="ddlPriceControlIndicator"
                                                ValidationGroup="Extn" ErrorMessage="Price Control Indicator cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlPriceControlIndicatorf" runat="server" AppendDataBoundItems="false"
                                                TabIndex="5">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPriceControlIndicatorf" runat="server" ControlToValidate="ddlPriceControlIndicatorf"
                                                ValidationGroup="ExtnAdd" ErrorMessage="Price Control Indicator cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="GridviewScrollHeader" />
                                <RowStyle CssClass="GridviewScrollItem" />
                                <FooterStyle CssClass="GridviewScrollItem" />
                                <PagerStyle CssClass="GridviewScrollPager" />
                            </asp:GridView>
                            <asp:HiddenField ID="hfGrd_ExtensionSV" runat="server" />
                            <asp:HiddenField ID="hfGrd_ExtensionSH" runat="server" />
                            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="ExtnAdd" ShowMessageBox="true"
                                ShowSummary="false" />
                            <asp:ValidationSummary ID="sm1" runat="server" ValidationGroup="Extn" ShowMessageBox="true"
                                ShowSummary="false" />
                            <script type="text/javascript">
                                function isIE() {
                                    var myNav = navigator.userAgent.toLowerCase();
                                    return (myNav.indexOf('msie') != -1) ? parseInt(myNav.split('msie')[1]) : false;
                                }

                                $(document).ready(function () {
                                    //alert(isIE());

                                    //function load() {
                                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(gridviewScroll);
                                    //}


                                    gridviewScroll();
                                });

                                //                                function fnConfirmNext() {
                                //                                    if ($("#<=ddlPlantf.ClientID>").val() != '') {
                                //                                        if (confirm('Want to discard the changes in last row?\n Click Ok to discard.\n Please click Cancel and then Add New to save the data in last row.'))
                                //                                            return true;
                                //                                        else
                                //                                            return false;
                                //                                    }
                                //                                }

                                function gridviewScroll() {
                                    $('#<%=Grd_Extension.ClientID%>').gridviewScroll({
                                        width: 850,
                                        height: 800,
                                        freezesize: isIE() > 8 ? 2 : 0,
                                        startVertical: $("#<%=hfGrd_ExtensionSV.ClientID%>").val(),
                                        startHorizontal: $("#<%=hfGrd_ExtensionSH.ClientID%>").val(),
                                        onScrollVertical: function (delta) {
                                            $("#<%=hfGrd_ExtensionSV.ClientID%>").val(delta);
                                        },
                                        onScrollHorizontal: function (delta) {
                                            $("#<%=hfGrd_ExtensionSH.ClientID%>").val(delta);
                                        }

                                    });
                                }

                                function ExtValidation() {
                                    var isValid = false;
                                    isValid = Page_ClientValidate('Extn');
                                    if (isValid) {
                                        isValid = Page_ClientValidate('ExtnAdd');
                                    }
                                    return isValid;
                                }

                            </script>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr id="trButton" runat="server" visible="false">
                        <td class="centerTD" colspan="4">
                            <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="Extn" TabIndex="16"
                                UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                            <asp:Button ID="btnSaveAll" runat="server" ValidationGroup="Extn" Text="Save & Add"
                                CssClass="button" OnClientClick="return ExtValidation();" TabIndex="17" UseSubmitBehavior="true"
                                OnClick="btnSaveAll_Click" />
                            <asp:Button ID="btnSave" runat="server" ValidationGroup="Extn" Text="Save without Add"
                                CssClass="button" TabIndex="17" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                            <asp:Button ID="btnNext" runat="server" Text="Proceed to Submit" TabIndex="18" CssClass="button"
                                OnClick="btnNext_Click" Width="120px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblCreateExtensionId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="57" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
