<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="MaterialExtension.aspx.cs" Inherits="Transaction_Material_MaterialExtension" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<%--MME_8300002156--%>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Src="~/Transaction/UserControl/ucExcelDownloadl.ascx" TagPrefix="uc" TagName="ExcelDownload" %>

<%--MME_8300002156--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../js/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../../stylesheet/gridviewScroll.css" rel="stylesheet" type="text/css" />

    <%--MME_8300002156--%>
    <script src="../../js/jquery.MultiFile.js" type="text/javascript"></script>
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
    <script type="text/javascript">
        function showModalPopupViaClient() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
    </script>
    <%--MME_8300002156--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialExtension" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlSExt" runat="server" Visible="false">
                <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                    <asp:Label ID="lblMsg" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlAddNew" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="trHeading" align="center" colspan="2">Extension Data
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="2">
                                <asp:Panel ID="pnlGrid" runat="server">
                                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                        DataKeyNames="Material_Extension_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                        GridLines="Both">
                                        <RowStyle CssClass="light-gray" HorizontalAlign="Left" />
                                        <HeaderStyle CssClass="gridHeader" />
                                        <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Left" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <Columns>
                                            <%--<asp:BoundField HeaderText="Plants" DataField="Plant" />--%>
                                            <asp:TemplateField HeaderText="Material Data">
                                                <ItemTemplate>
                                                    <strong>Material&nbsp;:</strong>
                                                    <asp:Label ID="lblMaterialNumber" runat="server" Text='<%# Eval("Material_Number") %>'></asp:Label>
                                                    <br />
                                                    <strong>Material Type&nbsp;:</strong>
                                                    <asp:Label ID="lblMaterialType" runat="server" Text='<%# Eval("Material_Type_Name") %>'></asp:Label>
                                                    <br />
                                                    <strong>Material Description&nbsp;:</strong>
                                                    <asp:Label ID="lblMaterialShortDescription" runat="server" Text='<%# Eval("Material_Short_Description") %>'></asp:Label>
                                                    <br />
                                                    <strong>Plant&nbsp;:</strong>
                                                    <asp:Label ID="lblPlant" runat="server" Text='<%# Eval("Plant_Name") %>'></asp:Label>
                                                    <asp:Label ID="lblPlantID" runat="server" Text='<%# Eval("Plant_Id") %>' Visible="false"></asp:Label>
                                                    <br />
                                                    <strong>Storage Location&nbsp;:</strong>
                                                    <asp:Label ID="lblStorageLocation" runat="server" Text='<%# Eval("Storage_Location") %>'></asp:Label>
                                                    <br />
                                                    <strong>Purc. Grp.&nbsp;:</strong>
                                                    <asp:Label ID="lblPurchasingGroup" runat="server" Text='<%# Eval("Purchasing_Group") %>'></asp:Label>
                                                    <br />
                                                    <strong>Profit Center&nbsp;:</strong>
                                                    <asp:Label ID="lblProfitCenter" runat="server" Text='<%# Eval("Profit_Center") %>'></asp:Label>
                                                    <br />
                                                    <strong>GR Proc. Time&nbsp;:</strong>
                                                    <asp:Label ID="lblGRProcessingTime" runat="server" Text='<%# Eval("GR_Processing_Time") %>'></asp:Label>
                                                    <br />
                                                    <%--<strong>Reason For Creation&nbsp;:</strong>
                                                <asp:Label ID="lblReasonForCreation" runat="server" Text='<%# Eval("Reason_For_Creation") %>'></asp:Label>
                                                <br />--%>
                                                    <strong>Reason for Extension&nbsp;:</strong>
                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                                    <br />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sales & Other Data">
                                                <ItemTemplate>
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
                                                    <br />
                                                    <strong>Valuation Class&nbsp;:</strong>
                                                    <asp:Label ID="lblValuationClass" runat="server" Text='<%# Eval("Valuation_Class") %>'></asp:Label>
                                                    <br />
                                                    <strong>Price Ctrl Ind.&nbsp;:</strong>
                                                    <asp:Label ID="lblPriceCtrlIndicator" runat="server" Text='<%# Eval("Price_Ctrl_Indicator") %>'></asp:Label>
                                                    <br />
                                                    <strong>Special Procurement&nbsp;:</strong>
                                                    <asp:Label ID="lblSpeclProcType" runat="server" Text='<%# Eval("Spl_Procurement_Type") %>'></asp:Label>
                                                    <br />
                                                    <strong>Inspection Type&nbsp;:</strong>
                                                    <asp:Label ID="lblInspectionType" runat="server" Text='<%# Eval("Inspection_Type") %>'></asp:Label>
                                                    <br />
                                                    <strong>Interval to Next Periodic Inspection&nbsp;:</strong>
                                                    <asp:Label ID="lblIntervalNPInspection" runat="server" Text='<%# Eval("Interval_Nxt_Inspection") %>'></asp:Label>
                                                    <br />
                                                    <%--<strong>QM in Procurement is Active &nbsp;:</strong>
                                                <asp:Label ID="lblIsQMInProcurement" runat="server" Text='<%# Eval("Is_QM_in_Procurement") %>'></asp:Label>
                                                <br />
                                                 <strong>Control Key for Quality Management in Procurement&nbsp;:</strong>
                                                <asp:Label ID="lblCtrlKeyQMProcurement" runat="server" Text='<%# Eval("Ctrl_Key_QM_Procurement") %>'></asp:Label>
                                                <br />
                                                 <strong>Certificate Type&nbsp;:</strong>
                                                <asp:Label ID="lblCertificateType" runat="server" Text='<%# Eval("Certificate_Type") %>'></asp:Label>
                                                <br />--%>
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
                                                    <strong>Fixed Lot Size&nbsp;:</strong>
                                                    <asp:Label ID="lblFixedLotSize" runat="server" Text='<%# Eval("Fixed_Lot_Size") %>'></asp:Label>
                                                    <br />
                                                    <strong>Rounding Value&nbsp;:</strong>
                                                    <asp:Label ID="lblRoundingValue" runat="server" Text='<%# Eval("Rounding_Value") %>'></asp:Label>
                                                    <br />
                                                    <strong>Prod. Str. Loc.&nbsp;:</strong>
                                                    <asp:Label ID="lblIssue_Storage_Location" runat="server" Text='<%# Eval("Issue_Storage_Location") %>'></asp:Label>
                                                    <br />
                                                    <strong>Delivery Time&nbsp;:</strong>
                                                    <asp:Label ID="lblPlannedDeliveryTimeDays" runat="server" Text='<%# Eval("Planned_Delivery_Time_Days") %>'></asp:Label>
                                                    <br />
                                                    <%--  <strong>Range of coverage profile&nbsp;:</strong>
                                                <asp:Label ID="lblRangeCoverageProfile" runat="server" Text='<%# Eval("Range_Coverage_Profile") %>'></asp:Label>
                                                <br />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Warehouse Data">
                                                <ItemTemplate>
                                                    <strong>Warehouse Number&nbsp;:</strong>
                                                    <asp:Label ID="lblWarehouse_ID" runat="server" Text='<%# Eval("Warehouse_ID") %>'></asp:Label>
                                                    <br />
                                                    <strong>Storage Type&nbsp;:</strong>
                                                    <asp:Label ID="lblStorage_Type_ID" runat="server" Text='<%# Eval("Storage_Type_ID") %>'></asp:Label>
                                                    <br />
                                                    <strong>Capacity Usage&nbsp;:</strong>
                                                    <asp:Label ID="lblCapacity_Usage" runat="server" Text='<%# Eval("Capacity_Usage") %>'></asp:Label>
                                                    <br />
                                                    <strong>Unit of Measure&nbsp;:</strong>
                                                    <asp:Label ID="lblWMUnitMeasure" runat="server" Text='<%# Eval("WM_Unit_Measure") %>'></asp:Label>
                                                    <br />
                                                    <strong>Stock Placement&nbsp;:</strong>
                                                    <asp:Label ID="lblStorTypeIndStockPlacement" runat="server" Text='<%# Eval("Stor_Type_Ind_Stock_Placement") %>'></asp:Label>
                                                    <br />
                                                    <strong>Stock Removal&nbsp;:</strong>
                                                    <asp:Label ID="lblStorTypeIndStockRemoval" runat="server" Text='<%# Eval("Stor_Type_Ind_Stock_Removal") %>'></asp:Label>
                                                    <br />
                                                    <strong>Storage Section Indicator&nbsp;:</strong>
                                                    <asp:Label ID="lblStorageSectionInd" runat="server" Text='<%# Eval("Storage_Section_Ind") %>'></asp:Label>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Warehouse Data(Conti..)">
                                                <ItemTemplate>
                                                    <strong>Loading Equip Qty 1&nbsp;:</strong>
                                                    <asp:Label ID="lblLoadingEquipmentQuantity" runat="server" Text='<%# Eval("Loading_Equipment_Quantity") %>'></asp:Label>
                                                    <br />
                                                    <strong>Loading Equip Qty 2&nbsp;:</strong>
                                                    <asp:Label ID="lblLoadingEquipmentQuantity1" runat="server" Text='<%# Eval("Loading_Equipment_Quantity1") %>'></asp:Label>
                                                    <br />
                                                    <strong>Loading Equip Qty 3&nbsp;:</strong>
                                                    <asp:Label ID="lblLoadingEquipmentQuantity2" runat="server" Text='<%# Eval("Loading_Equipment_Quantity2") %>'></asp:Label>
                                                    <br />
                                                    <strong>Unit Loading Equip Qty&nbsp;:</strong>
                                                    <asp:Label ID="lblUnitLoadingEquipQuan" runat="server" Text='<%# Eval("Unit_Loading_Equip_Quan") %>'></asp:Label>
                                                    <br />
                                                    <strong>Unit Loading Equip Qty 2&nbsp;:</strong>
                                                    <asp:Label ID="lblUnitLoadingEquipQuan1" runat="server" Text='<%# Eval("Unit_Loading_Equip_Quan1") %>'></asp:Label>
                                                    <br />
                                                    <strong>Unit Loading Equip Qty 3&nbsp;:</strong>
                                                    <asp:Label ID="lblUnitLoadingEquipQuan2" runat="server" Text='<%# Eval("Unit_Loading_Equip_Quan2") %>'></asp:Label>
                                                    <br />
                                                    <strong>Storage Unit Type&nbsp;:</strong>
                                                    <asp:Label ID="lblStorageUnitType" runat="server" Text='<%# Eval("Storage_Unit_Type") %>'></asp:Label>
                                                    <br />
                                                    <strong>Storage Unit Type 2&nbsp;:</strong>
                                                    <asp:Label ID="lblStorageUnitType1" runat="server" Text='<%# Eval("Storage_Unit_Type1") %>'></asp:Label>
                                                    <br />
                                                    <strong>Storage Unit Type 3&nbsp;:</strong>
                                                    <asp:Label ID="lblStorageUnitType2" runat="server" Text='<%# Eval("Storage_Unit_Type2") %>'></asp:Label>
                                                    <br />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkCopy" runat="server" Text="Copy" OnClick="lnkCopy_Click" Visible="false" /><br />
                                                    <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" CausesValidation="false" /><br />
                                                    <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="lnkDelete_Click" OnClientClick="return !(confirm('This will delete the record. Abort?'));" Visible="false" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="2" runat="server" id="trData">
                                <asp:Panel ID="pnlData" runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4" align="right" style="display: none !important">
                                                <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click" Visible="false"></asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" width="20%">Material Code
                                            <asp:Label ID="labletxtMaterialCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:TextBox ID="txtMaterialCode" runat="server" CssClass="textbox" MaxLength="10"
                                                    AutoPostBack="true" TabIndex="1" Width="180" OnTextChanged="txtMaterialCode_TextChanged" Enabled="false" />
                                                <asp:RequiredFieldValidator ID="reqtxtMaterialCode" runat="server" ControlToValidate="txtMaterialCode"
                                                    ValidationGroup="Extn" ErrorMessage="Material Code cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code cannot be blank.' />" />
                                                <asp:RegularExpressionValidator ID="regtxtMaterialCode" runat="server" ControlToValidate="txtMaterialCode"
                                                    ValidationGroup="Extn" ErrorMessage="Material Code Invalid." SetFocusOnError="true"
                                                    ValidationExpression="^[\d]{6,10}$" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code Invalid.' />" />
                                            </td>
                                            <td class="leftTD" style="width: 20%">Material Description
                                            <asp:Label ID="labletxtMaterialDescription" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                <br />
                                                <span style="color: Red; font-size: x-small; font-weight: normal">(As mentioned in SAP)</span>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:TextBox ID="txtMaterialDescription" runat="server" CssClass="textbox" MaxLength="70"
                                                    Width="210" TabIndex="2" Enabled="false" />
                                                <asp:RequiredFieldValidator ID="reqtxtMaterialDescription" runat="server" ControlToValidate="txtMaterialDescription"
                                                    ValidationGroup="Extn" ErrorMessage="Material Description cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Description cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" width="20%">Material Type
                                            <asp:Label ID="lableddlMaterialAccGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlMaterialAccGrp" runat="server" AppendDataBoundItems="false"
                                                    Enabled="false" TabIndex="4">
                                                    <asp:ListItem Text="Select" Value="0" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlMaterialAccGrp" runat="server" ControlToValidate="ddlMaterialAccGrp"
                                                    ValidationGroup="Extn" ErrorMessage="Select Material Type." SetFocusOnError="true"
                                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Material Type.' />" />
                                            </td>
                                            <td class="leftTD" style="width: 20%">Plant
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
                                        </tr>
                                        <tr>
                                            <td class="leftTD" width="20%">Storage Location
                                            <asp:Label ID="lableddlStorageLocation" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlStorageLocation" runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlStorageLocation" runat="server" ControlToValidate="ddlStorageLocation"
                                                    ValidationGroup="Extn" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                                            </td>
                                            <td class="tdSpace" colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" width="20%">Sales Org.
                                            <asp:Label ID="lableddlSalesOrginization" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlSalesOrginization" runat="server" AutoPostBack="true" AppendDataBoundItems="false"
                                                    OnSelectedIndexChanged="ddlSalesOrginization_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlSalesOrginization" runat="server" ControlToValidate="ddlSalesOrginization"
                                                    ValidationGroup="Extn" ErrorMessage="Sales Organization cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />
                                            </td>
                                            <td class="leftTD" width="20%">Distribution Channel
                                            <asp:Label ID="lableddlDistributionChannel" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlDistributionChannel" runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlDistributionChannel" runat="server" ControlToValidate="ddlDistributionChannel"
                                                    ValidationGroup="Extn" ErrorMessage="Distribution Channel cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" width="20%">Material Pricing
                                            <asp:Label ID="lableddlMaterialPGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlMaterialPGroup" runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlMaterialPGroup" runat="server" ControlToValidate="ddlMaterialPGroup"
                                                    ValidationGroup="Extn" ErrorMessage="Material Pricing Group cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Pricing Group cannot be blank.' />" />
                                            </td>
                                            <td class="leftTD" width="20%">Acc. Assignment Grp.
                                            <asp:Label ID="lableddlAccountAssignment" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlAccountAssignment" runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlAccountAssignment" runat="server" ControlToValidate="ddlAccountAssignment"
                                                    ValidationGroup="Extn" ErrorMessage="Account assignment group for this material cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Account assignment group for this material cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" width="20%">Purchasing Group
                                            <asp:Label ID="lableddlPurchasingGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlPurchasingGroup" runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlPurchasingGroup" runat="server" ControlToValidate="ddlPurchasingGroup"
                                                    ValidationGroup="Extn" ErrorMessage="Purchasing Group cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Group cannot be blank.' />" />
                                            </td>
                                            <td class="leftTD" width="20%">MRP Type
                                            <asp:Label ID="lableddlMrpType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlMrpType" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlMrpType_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlMrpType" runat="server" ControlToValidate="ddlMrpType"
                                                    ValidationGroup="Extn" ErrorMessage="MRP Type cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='MRP Type cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" width="20%">MRP Controller
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
                                            <td class="leftTD" width="20%">Reorder point
                                            <asp:Label ID="labletxtReorder" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:TextBox ID="txtReorder" runat="server" CssClass="textbox" Width="100px" MaxLength="13"
                                                    TabIndex="6" onkeypress="return IsNumber();" />
                                                <asp:RequiredFieldValidator ID="reqtxtReorder" runat="server" ControlToValidate="txtReorder"
                                                    ValidationGroup="Extn" ErrorMessage="Reorder point  cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Reorder point  cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" width="20%">Lot size
                                            <asp:Label ID="lableddlLotSize" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlLotSize" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                    TabIndex="10" OnSelectedIndexChanged="ddlLotSize_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlLotSize" runat="server" ControlToValidate="ddlLotSize"
                                                    ValidationGroup="Extn" ErrorMessage="Lot size (materials planning) cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Lot size (materials planning) cannot be blank.' />" />
                                            </td>
                                            <td class="leftTD" width="20%">Minimum lot size
                                            <asp:Label ID="labletxtMinLotSize" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:TextBox ID="txtMinLotSize" runat="server" CssClass="textbox" Width="100px" MaxLength="13"
                                                    TabIndex="11" onkeypress="return IsNumber();" />
                                                <asp:RequiredFieldValidator ID="reqtxtMinLotSize" runat="server" ControlToValidate="txtMinLotSize"
                                                    ValidationGroup="Extn" ErrorMessage="Minimum lot size cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Minimum lot size cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" width="20%">Maximum lot size
                                            <asp:Label ID="labletxtMaxLotSize" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:TextBox ID="txtMaxLotSize" runat="server" CssClass="textbox" Width="100px" MaxLength="13"
                                                    TabIndex="12" onkeypress="return IsNumber();" />
                                                <asp:RequiredFieldValidator ID="reqtxtMaxLotSize" runat="server" ControlToValidate="txtMaxLotSize"
                                                    ValidationGroup="Extn" ErrorMessage="Maximum lot size cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Maximum lot size cannot be blank.' />" />
                                                <asp:CompareValidator ID="ComtxtMaxLotSize" runat="server" ControlToValidate="txtMaxLotSize"
                                                    ControlToCompare="txtMinLotSize" Operator="GreaterThanEqual" ValidationGroup="Extn"
                                                    Type="Integer" ErrorMessage="Maximum lot size cannot be less than Minimum lot size."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Maximum lot size cannot be less than Minimum lot size.' />" />
                                            </td>
                                            <td class="leftTD" width="20%">Fixed lot size
                                            <asp:Label ID="labletxtFixedLotSize" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:TextBox ID="txtFixedLotSize" runat="server" CssClass="textbox" Width="100px"
                                                    TabIndex="13" onkeypress="return IsNumber();" MaxLength="13" />
                                                <asp:RequiredFieldValidator ID="reqtxtFixedLotSize" runat="server" ControlToValidate="txtFixedLotSize"
                                                    ValidationGroup="Extn" ErrorMessage="Fixed lot size  cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Fixed lot size  cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" width="20%">Rounding value
                                            <asp:Label ID="labletxtRoundingValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:TextBox ID="txtRoundingValue" runat="server" CssClass="textbox" Width="100px"
                                                    TabIndex="16" onkeypress="return IsNumber();" MaxLength="13" />
                                                <asp:RequiredFieldValidator ID="reqtxtRoundingValue" runat="server" ControlToValidate="txtRoundingValue"
                                                    ValidationGroup="Extn" ErrorMessage="Rounding value for purchase order quantity cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Rounding value for purchase order quantity cannot be blank.' />" />
                                                <asp:CompareValidator ID="ComtxtRoundingValue" runat="server" ControlToValidate="txtRoundingValue"
                                                    Type="Integer" ControlToCompare="txtMaxLotSize" Operator="LessThanEqual" ValidationGroup="MRP1"
                                                    ErrorMessage="Rounding value cannot be greater than Maximum lot size." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Rounding value cannot be greater than Maximum lot size.' />" />
                                            </td>
                                            <td class="leftTD" width="20%">Prod. Storage Location
                                            <asp:Label ID="lableddlIssueStorageLocation" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlIssueStorageLocation" runat="server" AppendDataBoundItems="false"
                                                    TabIndex="5">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlIssueStorageLocation" runat="server" ControlToValidate="ddlIssueStorageLocation"
                                                    ValidationGroup="Extn" ErrorMessage="Issue Storage Location cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Issue Storage Location cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" width="20%">GR Processing Time
                                            <%--<asp:Label ID="labletxtGRProcessingTime" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:TextBox ID="txtGRProcessingTime" runat="server" CssClass="textbox" Width="100px"
                                                    TabIndex="12" onkeypress="return IsNumber();" MaxLength="3" />
                                                <%-- <asp:RequiredFieldValidator ID="reqtxtGRProcessingTime" runat="server" ControlToValidate="txtGRProcessingTime"
                                                ValidationGroup="Extn" ErrorMessage="GR Processing Time cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='GR Processing Time cannot be blank.' />" />--%>
                                                <asp:RangeValidator ID="rantxtGRProcessingTime" runat="server" ControlToValidate="txtGRProcessingTime"
                                                    Type="Integer" ValidationGroup="Extn" ErrorMessage="Invalid GR Processing Time."
                                                    SetFocusOnError="true" MinimumValue="1" MaximumValue="999" Display="Dynamic"
                                                    Text="<img src='../../images/Error.png' title='Invalid GR Processing Time.' />" />
                                            </td>
                                            <td class="leftTD" width="20%">Planned Delivery Time in Days
                                            <asp:Label ID="labletxtPlannedDeleveryTime" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:TextBox ID="txtPlannedDeleveryTime" runat="server" CssClass="textbox" Width="100px"
                                                    TabIndex="11" onkeypress="return IsNumber();" MaxLength="3" />
                                                <asp:RequiredFieldValidator ID="reqtxtPlannedDeleveryTime" runat="server" ControlToValidate="txtPlannedDeleveryTime"
                                                    ValidationGroup="Extn" ErrorMessage="Planned Delivery Time in Days  cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planned Delivery Time in Days  cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" width="20%">Profit Center
                                            <asp:Label ID="lableddlProfitCenter" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlProfitCenter" runat="server" AppendDataBoundItems="false"
                                                    TabIndex="9">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlProfitCenter" runat="server" ControlToValidate="ddlProfitCenter"
                                                    ValidationGroup="Extn" ErrorMessage="Profit Center cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Profit Center cannot be blank.' />" />
                                            </td>
                                            <td class="leftTD" width="20%">Valuation Class
                                            <asp:Label ID="lableddlValuationClass" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlValuationClass" runat="server" AppendDataBoundItems="false"
                                                    TabIndex="4">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlValuationClass" runat="server" ControlToValidate="ddlValuationClass"
                                                    ValidationGroup="Extn" ErrorMessage="Valuation Class cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Class cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" width="20%">Price Control
                                            <asp:Label ID="lableddlPriceControlIndicator" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlPriceControlIndicator" runat="server" AppendDataBoundItems="false"
                                                    TabIndex="5">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlPriceControlIndicator" runat="server" ControlToValidate="ddlPriceControlIndicator"
                                                    ValidationGroup="Extn" ErrorMessage="Price Control Indicator cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                                            </td>
                                            <td class="leftTD" style="width: 20%">Special procurement type
                                            <asp:Label ID="lableddlSpecialProcType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlSpecialProcType" runat="server" AppendDataBoundItems="false"
                                                    TabIndex="4">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlSpecialProcType" runat="server" ControlToValidate="ddlSpecialProcType"
                                                    ValidationGroup="Extn" ErrorMessage="Special procurement type cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Special procurement type cannot be blank.' />" />
                                            </td>
                                        </tr>

                                        <%--MAT_DT26102020--%>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" style="width: 20%">

                                                <asp:Label ID="lblddlMInspectionType" runat="server" Text="Is Inspection Type Required ?"></asp:Label>
                                                <asp:Label ID="lableddlMInspectionType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD">
                                                <asp:DropDownList ID="ddlMInspectionType" runat="server" AppendDataBoundItems="false"
                                                    OnSelectedIndexChanged="ddlMInspectionType_SelectedIndexChanged" AutoPostBack="true" TabIndex="9">
                                                    <asp:ListItem Value="0" Text="Select" />
                                                    <asp:ListItem Value="1" Text="Yes" />
                                                    <asp:ListItem Value="2" Text="No" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlMInspectionType" runat="server" ControlToValidate="ddlMInspectionType"
                                                    ValidationGroup="Extn" ErrorMessage="Select Is Inspection Type Required (YES/NO)." SetFocusOnError="true"
                                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Is Inspection Type Required (YES/NO).' />" />

                                            </td>
                                            <%--<td class="leftTD" colspan="2"></td>--%>
                                             <%--NRDT02032023 Start--%>
                                            <td class="leftTD" style="width: 20%">Material Authorization Group for Activities in QM
                                        <%--<asp:Label ID="lableddlMaterialAuthGroupActQM" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                            </td>
                                            <td class="rigthTD">
                                                <asp:DropDownList ID="ddlMaterialAuthGroupActQM" runat="server" AppendDataBoundItems="false"
                                                    TabIndex="2">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                               <%-- <asp:RequiredFieldValidator ID="reqddlMaterialAuthGroupActQM" runat="server" ControlToValidate="ddlMaterialAuthGroupActQM"
                                                    ValidationGroup="Quality" ErrorMessage="Material Authorization Group for Activities in QM cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Authorization Group for Activities in QM cannot be blank.' />" />
                                           --%> 

                                            </td>
                                            <%--NRDT02032023 End--%>
                                        </tr>

                                        <%--MAT_DT26102020--%>


                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" style="width: 20%">Inspection Type
                                            <asp:Label ID="lableddlInspectionType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">

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
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" style="width: 20%">
                                                <%----%><asp:Label ID="lblchkQMProcurmentActive" runat="server" Text="QM in Procurement is Active"></asp:Label>
                                                <asp:Label ID="lablechkQMProcurmentActive" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:CheckBox ID="chkQMProcurmentActive" runat="server" Text=""
                                                    TabIndex="5" />
                                            </td>
                                            <td class="leftTD" style="width: 20%">
                                                <asp:Label ID="lblddlControlQualityMang" runat="server" Text="Control Key for Quality Management in Procurement"></asp:Label>
                                                <asp:Label ID="lableddlControlQualityMang" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlControlQualityMang" runat="server" AppendDataBoundItems="false"
                                                    TabIndex="6" OnSelectedIndexChanged="ddlControlQualityMang_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlControlQualityMang" runat="server" ControlToValidate="ddlControlQualityMang"
                                                    ValidationGroup="Extn" ErrorMessage="Control Key for Quality Management in Procurement cannot be blank." Visible="false"
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Control Key for Quality Management in Procurement cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" style="width: 20%">
                                                <%--<asp:Label ID="lableddlCertificateType" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                                <asp:Label ID="lblddlCertificateType" runat="server" Text="Certificate Type"></asp:Label>
                                                <asp:Label ID="lableddlCertificateType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlCertificateType" runat="server" AppendDataBoundItems="false"
                                                    TabIndex="8">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlCertificateType" runat="server" ControlToValidate="ddlCertificateType"
                                                    ValidationGroup="Extn" ErrorMessage="Certificate Type cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Certificate Type cannot be blank.' />" />

                                                <%--<asp:RequiredFieldValidator ID="reqddlCertificateType" runat="server" ControlToValidate="ddlCertificateType"
                                                ValidationGroup="Extn" ErrorMessage="Certificate Type cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Certificate Type cannot be blank.' />" />--%>
                                            </td>
                                            <td class="leftTD" style="width: 20%">Range of coverage profile
                                            <%--<asp:Label ID="lableddlRangeCoverage" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlRangeCoverage');"
                                                    onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:DropDownList ID="ddlRangeCoverage" runat="server" AppendDataBoundItems="false"
                                                    TabIndex="17">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator ID="reqddlRangeCoverage" runat="server" ControlToValidate="ddlRangeCoverage"
                                                ValidationGroup="Extn" ErrorMessage="Range of coverage profile cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Range of coverage profile cannot be blank.' />" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD">Warehouse Number / Warehouse Complex
                                            <asp:Label ID="lableddlWarehouse" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD">
                                                <asp:DropDownList ID="ddlWarehouse" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                    TabIndex="2" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlWarehouse" runat="server" ControlToValidate="ddlWarehouse"
                                                    ValidationGroup="Extn" ErrorMessage="Warehouse Number / Warehouse Complex cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Warehouse Number / Warehouse Complex cannot be blank.' />" />
                                            </td>
                                            <td class="leftTD">Storage Type
                                            <asp:Label ID="lableddlStorageType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD">
                                                <asp:DropDownList ID="ddlStorageType" runat="server" AppendDataBoundItems="false"
                                                    TabIndex="3">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlStorageType" runat="server" ControlToValidate="ddlStorageType"
                                                    ValidationGroup="Extn" ErrorMessage=" Storage Type cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title=' Storage Type cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD">Capacity Usage
                                            <asp:Label ID="labletxtCapacityUsage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD">
                                                <asp:TextBox ID="txtCapacityUsage" runat="server" CssClass="textbox" Width="100px"
                                                    TabIndex="4" onkeypress="return IsNumber();" />
                                                <asp:RequiredFieldValidator ID="reqtxtCapacityUsage" runat="server" ControlToValidate="txtCapacityUsage"
                                                    ValidationGroup="Extn" ErrorMessage="Capacity Usage cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Capacity Usage cannot be blank.' />" />
                                            </td>
                                            <td class="leftTD">Warehouse Management Unit of Measure
                                            <asp:Label ID="lableddlWareHouseMangUnit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD">
                                                <asp:DropDownList ID="ddlWareHouseMangUnit" runat="server" AppendDataBoundItems="false"
                                                    TabIndex="10">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlWareHouseMangUnit" runat="server" ControlToValidate="ddlWareHouseMangUnit"
                                                    ValidationGroup="Extn" ErrorMessage="Warehouse Management Unit of Measure cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Warehouse Management Unit of Measure cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD">Storage type indicator for stock removal
                                            <asp:Label ID="lableddlStorageTypeIndiSPlaceRemoval" runat="server" ForeColor="Red"
                                                Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD">
                                                <asp:DropDownList ID="ddlStorageTypeIndiSPlaceRemoval" runat="server" AppendDataBoundItems="false"
                                                    TabIndex="6">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlStorageTypeIndiSPlaceRemoval" runat="server"
                                                    ControlToValidate="ddlStorageTypeIndiSPlaceRemoval" ValidationGroup="Extn" ErrorMessage="Storage type indicator for stock removal cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage type indicator for stock removal cannot be blank.' />" />
                                            </td>
                                            <td class="leftTD">Storage type indicator for stock placement
                                            <asp:Label ID="lableddlStorageTypeIndiSPlacement" runat="server" ForeColor="Red"
                                                Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD">
                                                <asp:DropDownList ID="ddlStorageTypeIndiSPlacement" runat="server" AppendDataBoundItems="false"
                                                    TabIndex="7">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlStorageTypeIndiSPlacement" runat="server" ControlToValidate="ddlStorageTypeIndiSPlacement"
                                                    ValidationGroup="Extn" ErrorMessage="Storage type indicator for stock placement cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage type indicator for stock placement cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD">Storage Section Indicators
                                            <asp:Label ID="lableddlStorageSectIndi" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD">
                                                <asp:DropDownList ID="ddlStorageSectIndi" runat="server" AppendDataBoundItems="false"
                                                    TabIndex="8">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlStorageSectIndi" runat="server" ControlToValidate="ddlStorageSectIndi"
                                                    ValidationGroup="Extn" ErrorMessage="Storage Section Indicators cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Section Indicators cannot be blank.' />" />
                                            </td>
                                            <td class="leftTD" style="width: 20%">Interval to next periodic inspection
                                            <asp:Label ID="labletxtIntervalNPInspector" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD" style="width: 30%">
                                                <asp:TextBox ID="txtIntervalNPInspector" runat="server" CssClass="textbox" Width="100px"
                                                    onkeypress="return IsNumber();" MaxLength="5" TabIndex="7" />
                                                <asp:RequiredFieldValidator ID="reqtxtIntervalNPInspector" runat="server" ControlToValidate="txtIntervalNPInspector"
                                                    ValidationGroup="Extn" ErrorMessage="Interval to next periodic inspection  cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Interval to next periodic inspection  cannot be blank.' />" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <%-- Comment --%>
                                        <tr>
                                            <td align="left" valign="top" colspan="4">
                                                <table border="0" cellpadding="0" width="100%">
                                                    <tr>
                                                        <td class="leftTD">Loading equipment quantity
                                                        </td>
                                                        <td class="leftTD">Unit of measure for loading equipment quantity
                                                        </td>
                                                        <td class="leftTD">Storage Unit Type
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdSpace" colspan="4"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="rigthTD">
                                                            <asp:Label ID="labletxtLoadingEquipQuantity" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtLoadingEquipQuantity" runat="server" CssClass="textbox" Width="100px"
                                                                TabIndex="4" onkeypress="return IsNumber();" OnTextChanged="txtLoadingEquipQuantity_TextChanged"
                                                                AutoPostBack="true" />
                                                            <asp:RequiredFieldValidator ID="reqtxtLoadingEquipQuantity" runat="server" ControlToValidate="txtLoadingEquipQuantity"
                                                                ValidationGroup="Extn" ErrorMessage="Loading equipment quantity cannot be blank."
                                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Loading equipment quantity cannot be blank.' />" />
                                                        </td>
                                                        <td class="rigthTD">
                                                            <asp:Label ID="lableddlUnitMeasureLoadingEquip" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                            <asp:DropDownList ID="ddlUnitMeasureLoadingEquip" runat="server" AppendDataBoundItems="false"
                                                                TabIndex="5">
                                                                <asp:ListItem Text="Select" Value="" />
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqddlUnitMeasureLoadingEquip" runat="server" ControlToValidate="ddlUnitMeasureLoadingEquip"
                                                                ValidationGroup="Extn" ErrorMessage="Unit of measure for loading equipment quantity be blank."
                                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Unit of measure for loading equipment quantity cannot be blank.' />" />
                                                        </td>
                                                        <td class="rigthTD">
                                                            <asp:Label ID="lableddlStorageUnitType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                            <asp:DropDownList ID="ddlStorageUnitType" runat="server" AppendDataBoundItems="false"
                                                                TabIndex="6">
                                                                <asp:ListItem Text="Select" Value="" />
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqddlStorageUnitType" runat="server" ControlToValidate="ddlStorageUnitType"
                                                                ValidationGroup="Extn" ErrorMessage="Storage Unit Type cannot be blank." SetFocusOnError="true"
                                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Unit Type cannot be blank.' />" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdSpace" colspan="4"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="rigthTD">
                                                            <asp:Label ID="labletxtLoadingEquipQuantity2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtLoadingEquipQuantity2" runat="server" CssClass="textbox" Width="100px"
                                                                TabIndex="7" onkeypress="return IsNumber();" />
                                                            <asp:RequiredFieldValidator ID="reqtxtLoadingEquipQuantity2" runat="server" ControlToValidate="txtLoadingEquipQuantity2"
                                                                ValidationGroup="Extn" ErrorMessage="Loading equipment quantity  quantity be blank."
                                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Loading equipment quantity equipment quantity cannot be blank.' />" />
                                                        </td>
                                                        <td class="rigthTD">
                                                            <asp:Label ID="lableddlUnitMeasureLoadingEquip2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                            <asp:DropDownList ID="ddlUnitMeasureLoadingEquip2" runat="server" AppendDataBoundItems="false"
                                                                TabIndex="8">
                                                                <asp:ListItem Text="Select" Value="" />
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqddlUnitMeasureLoadingEquip2" runat="server" ControlToValidate="ddlUnitMeasureLoadingEquip2"
                                                                ValidationGroup="Extn" ErrorMessage="Unit of measure for loading equipment cannot be blank."
                                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Unit of measure for loading equipment cannot be blank.' />" />
                                                        </td>
                                                        <td class="rigthTD">
                                                            <asp:Label ID="lableddlStorageUnitType2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                            <asp:DropDownList ID="ddlStorageUnitType2" runat="server" AppendDataBoundItems="false"
                                                                TabIndex="9">
                                                                <asp:ListItem Text="Select" Value="" />
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqddlStorageUnitType2" runat="server" ControlToValidate="ddlStorageUnitType2"
                                                                ValidationGroup="Extn" ErrorMessage="Storage Unit Type cannot be blank." SetFocusOnError="true"
                                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Unit Type cannot be blank.' />" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdSpace" colspan="4"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="rigthTD">
                                                            <asp:Label ID="labletxtloadingEquipQuantity3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtloadingEquipQuantity3" runat="server" CssClass="textbox" Width="100px"
                                                                TabIndex="10" onkeypress="return IsNumber();" />
                                                            <asp:RequiredFieldValidator ID="reqtxtloadingEquipQuantity3" runat="server" ControlToValidate="txtloadingEquipQuantity3"
                                                                ValidationGroup="Extn" ErrorMessage="Loading equipment quantity cannot be blank."
                                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Loading equipment quantity cannot be blank.' />" />
                                                        </td>
                                                        <td class="rigthTD">
                                                            <asp:Label ID="lableddlUnitMeasureLoadingEquip3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                            <asp:DropDownList ID="ddlUnitMeasureLoadingEquip3" runat="server" AppendDataBoundItems="false"
                                                                TabIndex="11">
                                                                <asp:ListItem Text="Select" Value="" />
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqddlUnitMeasureLoadingEquip3" runat="server" ControlToValidate="ddlUnitMeasureLoadingEquip3"
                                                                ValidationGroup="Extn" ErrorMessage="Unit of measure for loading equipment quantity be blank."
                                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Unit of measure for loading equipment quantity cannot be blank.' />" />
                                                        </td>
                                                        <td class="rigthTD">
                                                            <asp:Label ID="lableddlStorageUnitType3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                            <asp:DropDownList ID="ddlStorageUnitType3" runat="server" AppendDataBoundItems="false"
                                                                TabIndex="12">
                                                                <asp:ListItem Text="Select" Value="" />
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqddlStorageUnitType3" runat="server" ControlToValidate="ddlStorageUnitType3"
                                                                ValidationGroup="Extn" ErrorMessage="Storage Unit Type cannot be blank." SetFocusOnError="true"
                                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Unit Type cannot be blank.' />" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdSpace" colspan="4"></td>
                                                    </tr>
                                                    <%-- <tr id="tr1" runat="server" visible="false">
                                    <td class="centerTD" colspan="4">
                                        <asp:Button ID="Button1" runat="server" ValidationGroup="WHMgmt" Text="Back" TabIndex="13"
                                            UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="Button2" runat="server" ValidationGroup="WHMgmt" Text="Save" CssClass="button"
                                            TabIndex="14" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                        <asp:Button ID="Button3" runat="server" ValidationGroup="WHMgmt" Text="Save & Next"
                                            TabIndex="15" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                    </td>
                                </tr>--%>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <%--<td class="leftTD">
                                            Reason For Creation
                                            <asp:Label ID="lableddlReason" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlReason" runat="server" AppendDataBoundItems="false" TabIndex="25">
                                                <asp:ListItem Text="Select" Value="" />
                                                <asp:ListItem Text="Process Change" Value="PC" />
                                                <asp:ListItem Text="MOC/Specification/Dimension change" Value="MOC" />
                                                <asp:ListItem Text="One time requirement code" Value="OTR" />
                                                <asp:ListItem Text="No harmonization of code" Value="NHC" />
                                                <asp:ListItem Text="First time purchasing of item" Value="FTP" />
                                                <asp:ListItem Text="Common item Revenue,Consumable" Value="CRC" />
                                                <asp:ListItem Text="For availing excise benefit" Value="AEB" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlReason" runat="server" ControlToValidate="ddlReason"
                                                ValidationGroup="Extn" ErrorMessage="Reason cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Reason cannot be blank.' />" />
                                        </td>--%>
                                            <td class="leftTD">Reason for Extension
                                            <asp:Label ID="labletxtRemarks" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            </td>
                                            <td class="rigthTD">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="textarea" TextMode="MultiLine"
                                                    TabIndex="26" Columns="100" Rows="3" />
                                                <asp:RequiredFieldValidator ID="reqtxtRemarks" runat="server" ControlToValidate="txtRemarks"
                                                    ValidationGroup="Extn" ErrorMessage="Reason for Extension cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Reason for Extension cannot be blank.' />" />
                                            </td>
                                            <td class="tdSpace" colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="Extn" TabIndex="16"
                                    UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="Extn" Text="Save" CssClass="button"
                                    TabIndex="17" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                <asp:Button ID="btnNext" runat="server" Text="Proceed to Submit" TabIndex="18" CssClass="button"
                                    OnClick="btnNext_Click" Width="160px" UseSubmitBehavior="true" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
            <%--MME_8300002156--%>
            <asp:Panel ID="pnlMassExt" runat="server" Visible="false">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlMassExtmsg" runat="server" Visible="false">
                                <asp:Label ID="lblMassExtmsg" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="trHeading" align="center" colspan="2">Material Mass Extension Data
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                                <tr>
                                    <td class="tdSpace"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">Type of Mass Updation
                            <asp:Label ID="lblddlTypeOfMassUpdm" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" align="left">
                                        <asp:DropDownList ID="ddlTypeOfMassUpdm" runat="server" AppendDataBoundItems="false"
                                            AutoPostBack="true" TabIndex="4" OnSelectedIndexChanged="ddlTypeOfMassUpdm_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlTypeOfMassUpdm" runat="server" ControlToValidate="ddlTypeOfMassUpdm"
                                            ValidationGroup="massUpload" ErrorMessage="Type of Mass Updation cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Type of Mass Updation cannot be blank.' />" />
                                    </td>

                                </tr>
                                <tr>
                                    <td class="tdSpace"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        <asp:Label ID="lblselectcap" runat="server" Text="Select File"></asp:Label>
                                        <asp:Label ID="lblSelectFile" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" align="left">
                                        <div>
                                            <asp:FileUpload ID="fileUploadMS" class="multi" runat="server" TabIndex="36" />
                                            <asp:Button ID="btnMSProcess" runat="server" OnClick="btnMSProcess_Click" Text="Upload" ValidationGroup="massUpload" />
                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td class="rigthTD" align="left" colspan="2">
                                        <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />
                                    </td>

                                </tr>
                                <tr>
                                    <td class="tdSpace"></td>
                                </tr>
                                <tr>
                                    <td class="rigthTD" align="left" colspan="2">
                                        <asp:GridView ID="grdAttachedDocs" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="Document_Upload_Id"
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
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="tdSpace"></td>
                                </tr>
                                <tr>
                                    <td class="tdSpace"></td>
                                    <td class="tdSpace">
                                        <asp:HyperLink ID="hlMSImportFormat" runat="server" Text="Excel Format" Target="_blank"></asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr id="trMassBtn" runat="server" visible="false">
                        <td class="centerTD" colspan="2">
                            <asp:Button ID="btnMassSave" runat="server" Text="Save" CssClass="button" UseSubmitBehavior="true"
                                TabIndex="39" OnClick="btnMassSave_Click" Visible="false" />
                        </td>
                    </tr>
                    <tr id="tr1" runat="server" visible="false">
                        <td class="centerTD" colspan="2">
                            <asp:Button ID="btnMassNext" runat="server" Text="Save & Proceed to Submit" UseSubmitBehavior="true"
                                TabIndex="40" CssClass="button" OnClick="btnMassNext_Click" Width="160px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="4"></td>
                    </tr>
                </table>
            </asp:Panel>

            <div id="divValidationModulePopUp" style="display: none;" title="Warning Message">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">

                            <tr>
                                <td class="leftTD" colspan="2">
                                    <span style="color: brown; font-size: 11px">

                                        <i>Dear User,</i>
                                        <br />
                                        <br />
                                        <i>Your request will be submitt after validation completed.</i>
                                        <br />
                                        <br />

                                        <i>If validation is failed then will send mail to your register email.</i>
                                        <br />
                                        <br />
                                        <i>We appreciate your actions in trying to embrace the new, Thanks!</i>

                                    </span>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="2">
                            <asp:Button ID="btnbackMsg" runat="server" Text="Ok" CssClass="button"
                                OnClick="btnbackMsg_Click" />
                        </td>
                    </tr>
                </table>
            </div>

            <asp:ValidationSummary ID="vsSM" runat="server" ValidationGroup="massUpload" ShowMessageBox="true"
                ShowSummary="false" />

            <%--MME_8300002156--%>


            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Extn" ShowMessageBox="true"
                ShowSummary="false" />







            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblMatExtensionId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="58" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
            <%--//CTRL_SUB_SDT18112019 Added by NR--%>
            <asp:Label ID="lblRefModuleId" runat="server" Visible="false" />
            <%--//CTRL_SUB_SDT18112019 Added by NR--%>


            <%--MME_8300002156--%>
            <div align="left" style="width: 98%">
                <uc:ExcelDownload ID="ExcelDownloadEXTDATA" runat="server" ActionType="MEF" Visible="false" />
            </div>

            <div align="left" style="width: 98%">
                <uc:ExcelDownload ID="ExcelDownloadError" runat="server" ActionType="MEE" Visible="false" />
            </div>

            <script type="text/javascript" language="javascript">
                function ShowValidationNewDialog() {
                    $("#divValidationModulePopUp").dialog({
                        height: 350,
                        width: 600,
                        modal: true,
                        closeOnEscape: true,
                        draggable: true,
                        resizable: false,
                        position: 'center',
                        dialogClass: 'alert',
                        open: function (type, data) {
                            $(this).parent().appendTo("form");
                            $(this).show('clip');
                        }
                    });


                }

            </script>

            <%--MME_8300002156--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
