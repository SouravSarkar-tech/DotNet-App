<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="ExtensionData.aspx.cs" Inherits="Transaction_Material_ExtensionData" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
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
                            <asp:Panel ID="pnlGrid" runat="server">
                                <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                    DataKeyNames="Mat_Create_Extension_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                    GridLines="Both">
                                    <RowStyle CssClass="light-gray" HorizontalAlign="Left" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Left" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Material Data">
                                            <ItemTemplate>
                                                <strong>Plant&nbsp;:</strong>
                                                <asp:Label ID="lblPlant" runat="server" Text='<%# Eval("Plant_Name") %>'></asp:Label>
                                                <asp:Label ID="lblPlantID" runat="server" Text='<%# Eval("Plant_Id") %>' Visible="false"></asp:Label>
                                                <br />
                                                <strong>Storage Location&nbsp;:</strong>
                                                <asp:Label ID="lblStorageLocation" runat="server" Text='<%# Eval("Storage_Location") %>'></asp:Label>
                                                <br />
                                                <strong>Sales Org.&nbsp;:</strong>
                                                <asp:Label ID="lblSalesOrganizationId" runat="server" Text='<%# Eval("Sales_Organization_Name") %>'></asp:Label>
                                                <br />
                                                <strong>Dist. Chnl.&nbsp;:</strong>
                                                <asp:Label ID="lblDistributionChannelID" runat="server" Text='<%# Eval("Distribution_Channel_Name") %>'></asp:Label>
                                                <br />
                                                <strong>Pricing Grp.&nbsp;:</strong>
                                                <asp:Label ID="lblMatPricingGroup" runat="server" Text='<%# Eval("Mat_Pricing_Group") %>'></asp:Label>
                                                <br />
                                                <strong>Assignment Grp.&nbsp;:</strong>
                                                <asp:Label ID="lblAccAssignmentGrp" runat="server" Text='<%# Eval("Acc_Assignment_Grp") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRP Data">
                                            <ItemTemplate>
                                                <strong>MRP Type&nbsp;:</strong>
                                                <asp:Label ID="lblMRPType" runat="server" Text='<%# Eval("MRP_Type") %>'></asp:Label>
                                                <br />
                                                <strong>MRP Controller.&nbsp;:</strong>
                                                <asp:Label ID="lblMRPController" runat="server" Text='<%# Eval("MRP_Controller") %>'></asp:Label>
                                                <br />
                                                <strong>Reorder Pt.&nbsp;:</strong>
                                                <asp:Label ID="lblReorderPoint" runat="server" Text='<%# Eval("Reorder_Point") %>'></asp:Label>
                                                <br />
                                                <strong>Lot Size&nbsp;:</strong>
                                                <asp:Label ID="lblLotSize" runat="server" Text='<%# Eval("Lot_Size") %>'></asp:Label>
                                                <br />
                                                <strong>Min. Lot Size&nbsp;:</strong>
                                                <asp:Label ID="lblMinLotSize" runat="server" Text='<%# Eval("Min_Lot_Size") %>'></asp:Label>
                                                <br />
                                                <strong>Max. Lot Size&nbsp;:</strong>
                                                <asp:Label ID="lblMaxLotSize" runat="server" Text='<%# Eval("Max_Lot_Size") %>'></asp:Label>
                                                <br />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRP Data(Conti..)">
                                            <ItemTemplate>
                                                <strong>Fixed Lot Size&nbsp;:</strong>
                                                <asp:Label ID="lblFixedLotSize" runat="server" Text='<%# Eval("Fixed_Lot_Size") %>'></asp:Label>
                                                <br />
                                                <strong>Rounding Value&nbsp;:</strong>
                                                <asp:Label ID="lblRoundingValue" runat="server" Text='<%# Eval("Rounding_Value") %>'></asp:Label>
                                                <br />
                                                <strong>Prod. Str. Loc.&nbsp;:</strong>
                                                <asp:Label ID="lblIssue_Storage_Location" runat="server" Text='<%# Eval("Issue_Storage_Location") %>'></asp:Label>
                                                <br />
                                                <strong>GR Proc. Time&nbsp;:</strong>
                                                <asp:Label ID="lblGRProcessingTime" runat="server" Text='<%# Eval("GR_Processing_Time") %>'></asp:Label>
                                                <br />
                                                <strong>Delivery Time&nbsp;:</strong>
                                                <asp:Label ID="lblPlannedDeliveryTimeDays" runat="server" Text='<%# Eval("Planned_Delivery_Time_Days") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Other Data">
                                            <ItemTemplate>
                                                <strong>Purc. Grp.&nbsp;:</strong>
                                                <asp:Label ID="lblPurchasingGroup" runat="server" Text='<%# Eval("Purchasing_Group") %>'></asp:Label>
                                                <br />
                                                <strong>Profit Center&nbsp;:</strong>
                                                <asp:Label ID="lblProfitCenter" runat="server" Text='<%# Eval("Profit_Center") %>'></asp:Label>
                                                <br />
                                                <strong>Valuation Class&nbsp;:</strong>
                                                <asp:Label ID="lblValuationClass" runat="server" Text='<%# Eval("Valuation_Class") %>'></asp:Label>
                                                <br />
                                                <strong>Price Ctrl Ind.&nbsp;:</strong>
                                                <asp:Label ID="lblPriceCtrlIndicator" runat="server" Text='<%# Eval("Price_Ctrl_Indicator") %>'></asp:Label>
                                                <br />
                                                <strong>Special Procurement&nbsp;:</strong>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Spl_Procurement_Type") %>'></asp:Label>
                                                <br />
                                                <strong>Inspection Type&nbsp;:</strong>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("InspectionType") %>'></asp:Label>
                                                <br />
                                                <strong>Interval To Next Periodic Inspection&nbsp;:</strong>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Interval_Nxt_Inspection") %>'></asp:Label>
                                                <br />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkCopy" runat="server" Text="Copy" OnClick="lnkCopy_Click" /><br />
                                                <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" /><br />
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="lnkDelete_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <asp:Panel ID="pnlData" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4" align="right">
                                            <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Plant
                                            <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                TabIndex="1" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                                ValidationGroup="Extn" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">
                                            Storage Location
                                            <asp:Label ID="lableddlStorageLocation" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlStorageLocation" runat="server" AppendDataBoundItems="false"
                                                TabIndex="2">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlStorageLocation" runat="server" ControlToValidate="ddlStorageLocation"
                                                ValidationGroup="Extn" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">
                                            Sales Org.
                                            <asp:Label ID="lableddlSalesOrginization" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlSalesOrginization" runat="server" AutoPostBack="true" AppendDataBoundItems="false"
                                                OnSelectedIndexChanged="ddlSalesOrginization_SelectedIndexChanged" TabIndex="3">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSalesOrginization" runat="server" ControlToValidate="ddlSalesOrginization"
                                                ValidationGroup="Extn" ErrorMessage="Sales Organization cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">
                                            Distribution Channel
                                            <asp:Label ID="lableddlDistributionChannel" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlDistributionChannel" runat="server" AppendDataBoundItems="false"
                                                TabIndex="4">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlDistributionChannel" runat="server" ControlToValidate="ddlDistributionChannel"
                                                ValidationGroup="Extn" ErrorMessage="Distribution Channel cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">
                                            Material Pricing
                                            <asp:Label ID="lableddlMaterialPGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlMaterialPGroup" runat="server" AppendDataBoundItems="false"
                                                TabIndex="5">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMaterialPGroup" runat="server" ControlToValidate="ddlMaterialPGroup"
                                                ValidationGroup="Extn" ErrorMessage="Material Pricing Group cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Pricing Group cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">
                                            Acc. Assignment Grp.
                                            <asp:Label ID="lableddlAccountAssignment" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlAccountAssignment" runat="server" AppendDataBoundItems="false"
                                                TabIndex="6">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlAccountAssignment" runat="server" ControlToValidate="ddlAccountAssignment"
                                                ValidationGroup="Extn" ErrorMessage="Account assignment group for this material cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Account assignment group for this material cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">
                                            Purchasing Group
                                            <asp:Label ID="lableddlPurchasingGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlPurchasingGroup" runat="server" AppendDataBoundItems="false"
                                                TabIndex="7">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPurchasingGroup" runat="server" ControlToValidate="ddlPurchasingGroup"
                                                ValidationGroup="Extn" ErrorMessage="Purchasing Group cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Group cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">
                                            MRP Type
                                            <asp:Label ID="lableddlMrpType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlMrpType" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlMrpType_SelectedIndexChanged" TabIndex="8">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMrpType" runat="server" ControlToValidate="ddlMrpType"
                                                ValidationGroup="Extn" ErrorMessage="MRP Type cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='MRP Type cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">
                                            MRP Controller
                                            <asp:Label ID="lableddlMrpController" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlMrpController" runat="server" AppendDataBoundItems="false"
                                                TabIndex="9">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMrpController" runat="server" ControlToValidate="ddlMrpController"
                                                ValidationGroup="Extn" ErrorMessage="MRP Controller (Materials Planner) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='MRP Controller (Materials Planner) cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">
                                            Reorder point
                                            <asp:Label ID="labletxtReorder" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtReorder" runat="server" CssClass="textbox" Width="100px" MaxLength="13"
                                                TabIndex="10" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtReorder" runat="server" ControlToValidate="txtReorder"
                                                ValidationGroup="Extn" ErrorMessage="Reorder point  cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Reorder point  cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">
                                            Lot size
                                            <asp:Label ID="lableddlLotSize" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlLotSize" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                TabIndex="11" OnSelectedIndexChanged="ddlLotSize_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlLotSize" runat="server" ControlToValidate="ddlLotSize"
                                                ValidationGroup="Extn" ErrorMessage="Lot size (materials planning) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Lot size (materials planning) cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">
                                            Minimum lot size
                                            <asp:Label ID="labletxtMinLotSize" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtMinLotSize" runat="server" CssClass="textbox" Width="100px" MaxLength="13"
                                                TabIndex="12" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtMinLotSize" runat="server" ControlToValidate="txtMinLotSize"
                                                ValidationGroup="Extn" ErrorMessage="Minimum lot size cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Minimum lot size cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">
                                            Maximum lot size
                                            <asp:Label ID="labletxtMaxLotSize" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtMaxLotSize" runat="server" CssClass="textbox" Width="100px" MaxLength="13"
                                                TabIndex="13" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtMaxLotSize" runat="server" ControlToValidate="txtMaxLotSize"
                                                ValidationGroup="Extn" ErrorMessage="Maximum lot size cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Maximum lot size cannot be blank.' />" />
                                            <asp:CompareValidator ID="ComtxtMaxLotSize" runat="server" ControlToValidate="txtMaxLotSize"
                                                ControlToCompare="txtMinLotSize" Operator="GreaterThanEqual" ValidationGroup="Extn"
                                                Type="Integer" ErrorMessage="Maximum lot size cannot be less than Minimum lot size."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Maximum lot size cannot be less than Minimum lot size.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">
                                            Fixed lot size
                                            <asp:Label ID="labletxtFixedLotSize" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtFixedLotSize" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="14" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtFixedLotSize" runat="server" ControlToValidate="txtFixedLotSize"
                                                ValidationGroup="Extn" ErrorMessage="Fixed lot size  cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Fixed lot size  cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">
                                            Rounding value
                                            <asp:Label ID="labletxtRoundingValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtRoundingValue" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="15" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtRoundingValue" runat="server" ControlToValidate="txtRoundingValue"
                                                ValidationGroup="Extn" ErrorMessage="Rounding value for purchase order quantity cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Rounding value for purchase order quantity cannot be blank.' />" />
                                            <asp:CompareValidator ID="ComtxtRoundingValue" runat="server" ControlToValidate="txtRoundingValue"
                                                Type="Integer" ControlToCompare="txtMaxLotSize" Operator="LessThanEqual" ValidationGroup="MRP1"
                                                ErrorMessage="Rounding value cannot be greater than Maximum lot size." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Rounding value cannot be greater than Maximum lot size.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">
                                            Prod. Storage Location
                                            <asp:Label ID="lableddlIssueStorageLocation" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlIssueStorageLocation" runat="server" AppendDataBoundItems="false"
                                                TabIndex="16">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlIssueStorageLocation" runat="server" ControlToValidate="ddlIssueStorageLocation"
                                                ValidationGroup="Extn" ErrorMessage="Issue Storage Location cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Issue Storage Location cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">
                                            GR Processing Time
                                            <asp:Label ID="labletxtGRProcessingTime" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtGRProcessingTime" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="17" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtGRProcessingTime" runat="server" ControlToValidate="txtGRProcessingTime"
                                                ValidationGroup="Extn" ErrorMessage="GR Processing Time cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='GR Processing Time cannot be blank.' />" />
                                            <asp:RangeValidator ID="rantxtGRProcessingTime" runat="server" ControlToValidate="txtGRProcessingTime"
                                                Type="Integer" ValidationGroup="Extn" ErrorMessage="Invalid GR Processing Time."
                                                SetFocusOnError="true" MinimumValue="1" MaximumValue="999" Display="Dynamic"
                                                Text="<img src='../../images/Error.png' title='Invalid GR Processing Time.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">
                                            Planned Delivery Time in Days
                                            <asp:Label ID="labletxtPlannedDeleveryTime" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtPlannedDeleveryTime" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="18" onkeypress="return IsNumber();" MaxLength="3" />
                                            <asp:RequiredFieldValidator ID="reqtxtPlannedDeleveryTime" runat="server" ControlToValidate="txtPlannedDeleveryTime"
                                                ValidationGroup="Extn" ErrorMessage="Planned Delivery Time in Days  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planned Delivery Time in Days  cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">
                                            Profit Center
                                            <asp:Label ID="lableddlProfitCenter" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlProfitCenter" runat="server" AppendDataBoundItems="false"
                                                TabIndex="19">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlProfitCenter" runat="server" ControlToValidate="ddlProfitCenter"
                                                ValidationGroup="Extn" ErrorMessage="Profit Center cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Profit Center cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">
                                            Valuation Class
                                            <asp:Label ID="lableddlValuationClass" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlValuationClass" runat="server" AppendDataBoundItems="false"
                                                TabIndex="20">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlValuationClass" runat="server" ControlToValidate="ddlValuationClass"
                                                ValidationGroup="Extn" ErrorMessage="Valuation Class cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Class cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">
                                            Price Control
                                            <asp:Label ID="lableddlPriceControlIndicator" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlPriceControlIndicator" runat="server" AppendDataBoundItems="false"
                                                TabIndex="21">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPriceControlIndicator" runat="server" ControlToValidate="ddlPriceControlIndicator"
                                                ValidationGroup="Extn" ErrorMessage="Price Control Indicator cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Special procurement type
                                            <asp:Label ID="lableddlSpecialProcType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlSpecialProcType" runat="server" AppendDataBoundItems="false"
                                                TabIndex="22">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSpecialProcType" runat="server" ControlToValidate="ddlSpecialProcType"
                                                ValidationGroup="Extn" ErrorMessage="Special procurement type cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Special procurement type cannot be blank.' />" />
                                        </td>
                                        <%-- <td class="tdSpace" colspan="2">
                                        </td>--%>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Inspection Type
                                            <asp:Label ID="lableddlInspectionType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <%--  <asp:DropDownList ID="ddlInspectionType" runat="server" AppendDataBoundItems="false"
                                            TabIndex="3">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlInspectionType" runat="server" ControlToValidate="ddlInspectionType"
                                            ValidationGroup="Quality" ErrorMessage="Inspection Type cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Inspection Type cannot be blank.' />" />--%>
                                            <cc1:DropDownCheckBoxes ID="ddlInspectionType" runat="server" AddJQueryReference="false"
                                                UseButtons="false" UseSelectAllNode="true">
                                                <%-- AutoPostBack="true" OnSelectedIndexChanged="ddlInspectionType_SelectedIndexChanged">--%>
                                                <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="350" DropDownBoxBoxHeight="80" />
                                                <Texts SelectBoxCaption="--Select--" />
                                            </cc1:DropDownCheckBoxes>
                                            <%--<cc1:ExtendedRequiredFieldValidator ID="reqddlInspectionType" runat="server" ControlToValidate="ddlInspectionType" 
                                            ValidationGroup="Quality" ErrorMessage="Inspection Type cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Inspection Type cannot be blank.' />"></cc1:ExtendedRequiredFieldValidator>--%>
                                        </td>
                                        <td class="leftTD" colspan="2">
                                            <asp:Label ID="lableddlInspectionType1" runat="server"></asp:Label>
                                            <asp:LinkButton ID="lnkRefreshddlInspectionType" runat="server" Text="[ Refresh ]"
                                                Font-Bold="false" OnClick="lnkRefreshddlInspectionType_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Interval to next periodic inspection
                                            <asp:Label ID="labletxtIntervalNPInspector" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtIntervalNPInspector" runat="server" CssClass="textbox" Width="100px"
                                                onkeypress="return IsNumber();" MaxLength="5" TabIndex="23" />
                                            <asp:RequiredFieldValidator ID="reqtxtIntervalNPInspector" runat="server" ControlToValidate="txtIntervalNPInspector"
                                                ValidationGroup="Extn" ErrorMessage="Interval to next periodic inspection  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Interval to next periodic inspection  cannot be blank.' />" />
                                        </td>
                                        <td class="tdSpace" colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr id="trButton" runat="server" visible="false">
                        <td class="centerTD" colspan="4">
                            <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="Extn" TabIndex="22"
                                UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                            <asp:Button ID="btnSave" runat="server" ValidationGroup="Extn" Text="Save" CssClass="button"
                                TabIndex="23" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                           <%-- <asp:Button ID="btnNext" runat="server" Text="Proceed to Submit" TabIndex="24" CssClass="button"
                                OnClick="btnNext_Click" Width="120px" />--%>
                             <asp:Button ID="btnNext" runat="server" Text="Save & Next" TabIndex="24" CssClass="button"
                                OnClick="btnNext_Click" Width="120px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Extn" ShowMessageBox="true"
                ShowSummary="false" />
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
