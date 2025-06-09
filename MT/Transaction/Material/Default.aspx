<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="Transaction_Material_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:: Depot Extension MRP Data :: </title>
    <link href="../../stylesheet/main.css" rel="stylesheet" type="text/css" />
    <link href="../../stylesheet/autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="../../stylesheet/Paging.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <link href="../../stylesheet/TabStyle.css" rel="stylesheet" type="text/css" />
    <script src="../../js/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <act:ToolkitScriptManager ID="sm" runat="server">
    </act:ToolkitScriptManager>
    <div>
        <asp:Panel ID="pnlData" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                            <asp:Label ID="lblMsg" runat="server" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="trHeading" align="center">
                        Depot Extension Data
                    </td>
                </tr>
                <tr>
                    <td class="tdSpace">
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <%--<asp:UpdatePanel ID="UpdDepot" runat="server">
                        <ContentTemplate>--%>
                        <table border="0" cellpadding="0" cellspacing="1" width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="grvDepotExtnsn" runat="server" AutoGenerateColumns="false" Width="100%"
                                        BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both">
                                        <%--OnRowDataBound="grvMaterialChange_RowDataBound"--%>
                                        <RowStyle CssClass="light-gray" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle CssClass="gridHeader" />
                                        <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundField DataField="Mat_Extnsn_Data_Id" Visible="false" />
                                            <asp:BoundField DataField="Plant_Group" HeaderText="Plant Group" ItemStyle-Width="8%" />
                                            <asp:BoundField DataField="Plant_Id" HeaderText="Plant ID" ItemStyle-Width="8%" Visible = "false" />
                                            <asp:BoundField DataField="Plant_Code" HeaderText="Plant" ItemStyle-Width="8%" />
                                            <asp:BoundField DataField="Purchasing_Group" HeaderText="Purchasing Group" ItemStyle-Width="8%" />
                                            <asp:BoundField DataField="MRP_Type" HeaderText="MRP Type" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="MRP_Controller" HeaderText="MRP Controller" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Reorder_Point" HeaderText="Re-order point" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Lot_Size" HeaderText="Lot Size" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Fixed_Lot_Size" HeaderText="Fixed Lot Size" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Rounding_Value" HeaderText="Rounding Value" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Old_Material_Number" HeaderText="Old Material Code" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Range_Coverage_Profile" HeaderText="Coverage Profile"
                                                ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Procurement_Type" HeaderText="Procurement Type" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Safety_Time_WorkDays" HeaderText="Safety Time" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Planned_Delivery_Time_Days" HeaderText="PDT" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="GR_Processing_Time" HeaderText="GRT" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Spl_Procurement_Type" HeaderText="Special Procurement Type"
                                                ItemStyle-Width="8%" />
                                            <asp:BoundField DataField="Fair_Share_Rule" HeaderText="Fair share rule" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Indi_Push_Distribution" HeaderText="Push distribution"
                                                ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Loading_Group" HeaderText="Loading Group" ItemStyle-Width="10%" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel> --%>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Label ID="lblUserId" runat="server" Visible="false" />
        <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
        <asp:Label ID="lblModuleId" runat="server" Visible="false" />
        <asp:Label ID="lblMode" runat="server" Visible="false" />
        <br />
        <div align="left">
            <asp:LinkButton ID="lnkExcelDwld" runat="server" Text="Download Excel" OnClick="lnkExcelDwld_Click" />
            <asp:ImageButton ID="imgExcelDwld" runat="server" ImageUrl="~/images/Excel.png" Height="20px"
                Width="20px" OnClick="lnkExcelDwld_Click" />
        </div>
        <br />
        <br />
        <div style="width: 100%" class="leftTD" id="divHeader" runat="server">
            Import Data</div>
        <%@ register namespace="AjaxControlToolkit" assembly="AjaxControlToolkit" tagprefix="act" %>
        <asp:FileUpload ID="fileUpload" runat="server" />
        <asp:Button ID="Process" runat="server" OnClick="Process_Click" Text="Upload" />
        <asp:Button runat="server" ID="hiddenTargetControlForModalPopupI" Style="display: none" />
        <asp:Panel ID="pnlMassUpdate" runat="server">
            <asp:Label ID="lblMassMsg" runat="server" />
        </asp:Panel>
        <act:ModalPopupExtender ID="ModalPopupExtenderI" runat="server" TargetControlID="hiddenTargetControlForModalPopupI"
            BehaviorID="programmaticModalPopupBehaviorI" CancelControlID="btnCancelImport"
            PopupControlID="pnlAddDataI" BackgroundCssClass="modalBackground" DropShadow="true"
            PopupDragHandleControlID="pnlTitleI" />
        <asp:Panel ID="pnlAddDataI" runat="server" Width="100%">
            <div style="background-color: White; padding: 2px 2px 2px 2px; overflow: scroll;
                width: 1100px; height: 550px">
                <asp:Panel ID="pnlTitleI" runat="server" Style="cursor: move; background-color: Black;
                    border: solid 1px Gray; color: Black">
                    <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                        <span class="ui-dialog-title">Import Depot Extension Update Data</span>
                    </div>
                </asp:Panel>
                <table border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr>
                        <td class="tdSpace">
                            <asp:GridView ID="grvExtnsnData" runat="server" AutoGenerateColumns="false" OnDataBound="grvExtnsnData_DataBound">
                                <Columns>
                                    <asp:TemplateField runat="server">
                                        <ItemTemplate>
                                            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                                                <asp:Label ID="lblMsg" runat="server" />
                                            </asp:Panel>
                                            <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                Visible="false" TabIndex="1">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlLotSize" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                Visible="false" TabIndex="1">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlMrpType" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                Visible="false" TabIndex="1">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlMrpController" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                Visible="false" TabIndex="1">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlPurchasingGroup" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                Visible="false" TabIndex="1">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlProcurmentType" runat="server" AppendDataBoundItems="false" AutoPostBack = "true"
                                                Visible="false" TabIndex="1">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlLoadingGroup" runat="server" AppendDataBoundItems="false" AutoPostBack = "true"
                                                Visible="false" TabIndex="1">
                                                <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMat_Extnsn_Data_Id" runat="server" Text='<%# Eval("Mat_Extnsn_Data_Id") %>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaster_Header_Id" runat="server" Text='<%# Eval("Master_Header_Id") %>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlant_Group" runat="server" Text='<%# Eval("Plant_Group") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible = "false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlant" runat="server" Text='<%# Eval("Plant_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Plant Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlant_Id" runat="server" Text='<%# Eval("Plant_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Purchasing Group">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchasing_Group" runat="server" Text='<%# Eval("Purchasing_Group") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRP Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMRP_Type" runat="server" Text='<%# Eval("MRP_Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRP Controller">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMRP_Controller" runat="server" Text='<%# Eval("MRP_Controller") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reorder Point">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReorder_Point" runat="server" Text='<%# Eval("Reorder_Point") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lot Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLot_Size" runat="server" Text='<%# Eval("Lot_Size") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fixed Lot Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFixed_Lot_Size" runat="server" Text='<%# Eval("Fixed_Lot_Size") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rounding Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRounding_Value" runat="server" Text='<%# Eval("Rounding_Value") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Old Material Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOld_Material_Number" runat="server" Text='<%# Eval("Old_Material_Number") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Range Coverage Profile">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRange_Coverage_Profile" runat="server" Text='<%# Eval("Range_Coverage_Profile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Procurement Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcurement_Type" runat="server" Text='<%# Eval("Procurement_Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Safety Time WorkDays">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSafety_Time_WorkDays" runat="server" Text='<%# Eval("Safety_Time_WorkDays") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Planned Delivery Time Days">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlanned_Delivery_Time_Days" runat="server" Text='<%# Eval("Planned_Delivery_Time_Days") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GR Processing Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGR_Processing_Time" runat="server" Text='<%# Eval("GR_Processing_Time") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Spl Procurement Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpl_Procurement_Type" runat="server" Text='<%# Eval("Spl_Procurement_Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fair Share Rule">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFair_Share_Rule" runat="server" Text='<%# Eval("Fair_Share_Rule") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Indi Push Distribution">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndi_Push_Distribution" runat="server" Text='<%# Eval("Indi_Push_Distribution") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Loading Group">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoading_Group" runat="server" Text='<%# Eval("Loading_Group") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD">
                            <asp:Button ID="btnAdd" runat="server" Text="Import Data" CssClass="button" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnCancelImport" Text="Cancel" runat="server" CssClass="button" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
