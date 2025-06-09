<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="WM2.aspx.cs" Inherits="Transaction_WM2" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialWM" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlData" runat="server">
                <table border="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            WM2
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                            DataKeyNames="Mat_WareHouse_Mgmt2_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                            GridLines="Both">
                                            <RowStyle CssClass="light-gray" />
                                            <HeaderStyle CssClass="gridHeader" />
                                            <AlternatingRowStyle CssClass="gridRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Plant" DataField="Plant" />
                                                <%--<asp:BoundField HeaderText="Warehouse" DataField="Warehouse" />
                                                <asp:BoundField HeaderText="Storage Type" DataField="StorageType" />--%>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("Mat_WareHouse_Mgmt2_Id") %>' />
                                                        <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                    <td class="tdSpace" colspan="2" align="right">
                                        <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>
                                        <asp:HiddenField ID="hdnID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Plant
                                        <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            TabIndex="1">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                            ValidationGroup="WHMgmt" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
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
                                        Warehouse Number / Warehouse Complex
                                        <asp:Label ID="lableddlWarehouse" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlWarehouse" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            TabIndex="2" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlWarehouse" runat="server" ControlToValidate="ddlWarehouse"
                                            ValidationGroup="WHMgmt" ErrorMessage="Warehouse Number / Warehouse Complex cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Warehouse Number / Warehouse Complex cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Storage Type
                                        <asp:Label ID="lableddlStorageType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlStorageType" runat="server" AppendDataBoundItems="false"
                                            TabIndex="3">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlStorageType" runat="server" ControlToValidate="ddlStorageType"
                                            ValidationGroup="WHMgmt" ErrorMessage=" Storage Type cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title=' Storage Type cannot be blank.' />" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="leftTD">
                                        Loading equipment quantity
                                    </td>
                                    <td class="leftTD">
                                        Unit of measure for loading equipment quantity
                                    </td>
                                    <td class="leftTD">
                                        Storage Unit Type
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rigthTD">
                                        <asp:Label ID="labletxtLoadingEquipQuantity" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:TextBox ID="txtLoadingEquipQuantity" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="4" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtLoadingEquipQuantity" runat="server" ControlToValidate="txtLoadingEquipQuantity"
                                            ValidationGroup="WHMgmt" ErrorMessage="Loading equipment quantity cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Loading equipment quantity cannot be blank.' />" />
                                    </td>
                                    <td class="rigthTD">
                                        <asp:Label ID="lableddlUnitMeasureLoadingEquip" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:DropDownList ID="ddlUnitMeasureLoadingEquip" runat="server" AppendDataBoundItems="false"
                                            TabIndex="5">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlUnitMeasureLoadingEquip" runat="server" ControlToValidate="ddlUnitMeasureLoadingEquip"
                                            ValidationGroup="WHMgmt" ErrorMessage="Unit of measure for loading equipment quantity be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Unit of measure for loading equipment quantity cannot be blank.' />" />
                                    </td>
                                    <td class="rigthTD">
                                        <asp:Label ID="lableddlStorageUnitType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:DropDownList ID="ddlStorageUnitType" runat="server" AppendDataBoundItems="false"
                                            TabIndex="6">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlStorageUnitType" runat="server" ControlToValidate="ddlStorageUnitType"
                                            ValidationGroup="WHMgmt" ErrorMessage="Storage Unit Type cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Unit Type cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rigthTD">
                                        <asp:Label ID="labletxtLoadingEquipQuantity2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:TextBox ID="txtLoadingEquipQuantity2" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="7" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtLoadingEquipQuantity2" runat="server" ControlToValidate="txtLoadingEquipQuantity2"
                                            ValidationGroup="WHMgmt" ErrorMessage="Loading equipment quantity  quantity be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Loading equipment quantity equipment quantity cannot be blank.' />" />
                                    </td>
                                    <td class="rigthTD">
                                        <asp:Label ID="lableddlUnitMeasureLoadingEquip2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:DropDownList ID="ddlUnitMeasureLoadingEquip2" runat="server" AppendDataBoundItems="false"
                                            TabIndex="8">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlUnitMeasureLoadingEquip2" runat="server" ControlToValidate="ddlUnitMeasureLoadingEquip2"
                                            ValidationGroup="WHMgmt" ErrorMessage="Unit of measure for loading equipment cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Unit of measure for loading equipment cannot be blank.' />" />
                                    </td>
                                    <td class="rigthTD">
                                        <asp:Label ID="lableddlStorageUnitType2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:DropDownList ID="ddlStorageUnitType2" runat="server" AppendDataBoundItems="false"
                                            TabIndex="9">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlStorageUnitType2" runat="server" ControlToValidate="ddlStorageUnitType2"
                                            ValidationGroup="WHMgmt" ErrorMessage="Storage Unit Type cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Unit Type cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rigthTD">
                                        <asp:Label ID="labletxtloadingEquipQuantity3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:TextBox ID="txtloadingEquipQuantity3" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="10" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtloadingEquipQuantity3" runat="server" ControlToValidate="txtloadingEquipQuantity3"
                                            ValidationGroup="WHMgmt" ErrorMessage="Loading equipment quantity cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Loading equipment quantity cannot be blank.' />" />
                                    </td>
                                    <td class="rigthTD">
                                        <asp:Label ID="lableddlUnitMeasureLoadingEquip3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:DropDownList ID="ddlUnitMeasureLoadingEquip3" runat="server" AppendDataBoundItems="false"
                                            TabIndex="11">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlUnitMeasureLoadingEquip3" runat="server" ControlToValidate="ddlUnitMeasureLoadingEquip3"
                                            ValidationGroup="WHMgmt" ErrorMessage="Unit of measure for loading equipment quantity be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Unit of measure for loading equipment quantity cannot be blank.' />" />
                                    </td>
                                    <td class="rigthTD">
                                        <asp:Label ID="lableddlStorageUnitType3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <asp:DropDownList ID="ddlStorageUnitType3" runat="server" AppendDataBoundItems="false"
                                            TabIndex="12">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlStorageUnitType3" runat="server" ControlToValidate="ddlStorageUnitType3"
                                            ValidationGroup="WHMgmt" ErrorMessage="Storage Unit Type cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Unit Type cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr id="trButton" runat="server" visible="false">
                                    <td class="centerTD" colspan="4">
                                        <asp:Button ID="btnPrevious" runat="server" ValidationGroup="WHMgmt" Text="Back"
                                            TabIndex="13" UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="WHMgmt" Text="Save" CssClass="button"
                                            TabIndex="14" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnNext" runat="server" ValidationGroup="WHMgmt" Text="Save & Next"
                                            TabIndex="15" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="WHMgmt" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblWMId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="20" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
