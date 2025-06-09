<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="MRP1.aspx.cs" Inherits="Transaction_MRP1" %>

<%--<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetToolTip(control) {
            ddrivetipm('8', control);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialMRP" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlAddNew" runat="server">
                <table border="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            MRP
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <asp:Panel ID="pnlGrid" runat="server">
                                <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                    DataKeyNames="Mat_MRP1_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                    GridLines="Both">
                                    <RowStyle CssClass="light-gray" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <AlternatingRowStyle CssClass="gridRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Plants" DataField="Plant" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <asp:Panel ID="pnlData" runat="server">
                                <table border="0" cellpadding="0" width="100%">
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
                                                TabIndex="1" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                                ValidationGroup="MRP1" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Storage Location
                                            <asp:Label ID="lableddlStorageLocation" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlStorageLocation" runat="server" AppendDataBoundItems="false"
                                                TabIndex="2">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlStorageLocation" runat="server" ControlToValidate="ddlStorageLocation"
                                                ValidationGroup="MRP1" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            MRP Group
                                            <asp:Label ID="lableddlMrpGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlMrpGroup');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlMrpGroup" runat="server" AppendDataBoundItems="false" TabIndex="3">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMrpGroup" runat="server" ControlToValidate="ddlMrpGroup"
                                                ValidationGroup="MRP1" ErrorMessage="MRP Group cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='MRP Group cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            ABC Indicator
                                            <asp:Label ID="lableddlAbcIndicator" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlAbcIndicator" runat="server" AppendDataBoundItems="false"
                                                TabIndex="4">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlAbcIndicator" runat="server" ControlToValidate="ddlAbcIndicator"
                                                ValidationGroup="MRP1" ErrorMessage="ABC Indicator cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='ABC Indicator cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            MRP Type
                                            <asp:Label ID="lableddlMrpType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlMrpType');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlMrpType" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                TabIndex="5" OnSelectedIndexChanged="ddlMrpType_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMrpType" runat="server" ControlToValidate="ddlMrpType"
                                                ValidationGroup="MRP1" ErrorMessage="MRP Type cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='MRP Type cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Reorder point
                                            <asp:Label ID="labletxtReorder" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtReorder" runat="server" CssClass="textbox" Width="100px" MaxLength="13"
                                                TabIndex="6" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtReorder" runat="server" ControlToValidate="txtReorder"
                                                ValidationGroup="MRP1" ErrorMessage="Reorder point  cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Reorder point  cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Planning cycle
                                            <asp:Label ID="lableddlPlanningCycle" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlPlanningCycle" runat="server" AppendDataBoundItems="false"
                                                TabIndex="7">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPlanningCycle" runat="server" ControlToValidate="ddlPlanningCycle"
                                                ValidationGroup="MRP1" ErrorMessage="Planning cycle cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Planning cycle cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Planning time fence
                                            <asp:Label ID="labletxtPlanningTimeFence" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtPlanningTimeFence" runat="server" CssClass="textbox" Width="100px"
                                                onkeypress="return IsNumber();" TabIndex="8" MaxLength="3" />
                                            <asp:RequiredFieldValidator ID="reqtxtPlanningTimeFence" runat="server" ControlToValidate="txtPlanningTimeFence"
                                                ValidationGroup="MRP1" ErrorMessage="Planning time fence  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planning time fence  cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            MRP Controller (Materials Planner)
                                            <asp:Label ID="lableddlMrpController" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlMrpController');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlMrpController" runat="server" AppendDataBoundItems="false"
                                                TabIndex="9">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMrpController" runat="server" ControlToValidate="ddlMrpController"
                                                ValidationGroup="MRP1" ErrorMessage="MRP Controller (Materials Planner) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='MRP Controller (Materials Planner) cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Lot size (materials planning)
                                            <asp:Label ID="lableddlLotSize" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlLotSize');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlLotSize" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                TabIndex="10" OnSelectedIndexChanged="ddlLotSize_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlLotSize" runat="server" ControlToValidate="ddlLotSize"
                                                ValidationGroup="MRP1" ErrorMessage="Lot size (materials planning) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Lot size (materials planning) cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Minimum lot size
                                            <asp:Label ID="labletxtMinLotSize" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtMinLotSize" runat="server" CssClass="textbox" Width="100px" MaxLength="13"
                                                TabIndex="11" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtMinLotSize" runat="server" ControlToValidate="txtMinLotSize"
                                                ValidationGroup="MRP1" ErrorMessage="Minimum lot size cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Minimum lot size cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Maximum lot size
                                            <asp:Label ID="labletxtMaxLotSize" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtMaxLotSize" runat="server" CssClass="textbox" Width="100px" MaxLength="13"
                                                TabIndex="12" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtMaxLotSize" runat="server" ControlToValidate="txtMaxLotSize"
                                                ValidationGroup="MRP1" ErrorMessage="Maximum lot size cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Maximum lot size cannot be blank.' />" />
                                            <asp:CompareValidator ID="ComtxtMaxLotSize" runat="server" ControlToValidate="txtMaxLotSize"
                                                ControlToCompare="txtMinLotSize" Operator="GreaterThanEqual" ValidationGroup="MRP1"
                                                Type="Double" ErrorMessage="Maximum lot size cannot be less than Minimum lot size."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Maximum lot size cannot be less than Minimum lot size.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Fixed lot size
                                            <asp:Label ID="labletxtFixedLotSize" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtFixedLotSize" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="13" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtFixedLotSize" runat="server" ControlToValidate="txtFixedLotSize"
                                                ValidationGroup="MRP1" ErrorMessage="Fixed lot size  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Fixed lot size  cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Maximum stock level
                                            <asp:Label ID="labletxtMaxStockLevel" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtMaxStockLevel" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="14" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtMaxStockLevel" runat="server" ControlToValidate="txtMaxStockLevel"
                                                ValidationGroup="MRP1" ErrorMessage="Maximum stock level cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Maximum stock level cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Rounding Profile
                                            <asp:Label ID="lableddlRoundingProfile" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlRoundingProfile" runat="server" AppendDataBoundItems="false"
                                                TabIndex="15">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlRoundingProfile" runat="server" ControlToValidate="ddlRoundingProfile"
                                                ValidationGroup="MRP1" ErrorMessage="Rounding Profile cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Rounding Profile cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Rounding value for purchase order quantity
                                            <asp:Label ID="labletxtRoundingValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtRoundingValue" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="16" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtRoundingValue" runat="server" ControlToValidate="txtRoundingValue"
                                                ValidationGroup="MRP1" ErrorMessage="Rounding value for purchase order quantity cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Rounding value for purchase order quantity cannot be blank.' />" />
                                            <asp:CompareValidator ID="ComtxtRoundingValue" runat="server" ControlToValidate="txtRoundingValue"
                                                Type="Integer" ControlToCompare="txtMaxLotSize" Operator="LessThanEqual" ValidationGroup="MRP1"
                                                ErrorMessage="Rounding value cannot be greater than Maximum lot size." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Rounding value cannot be greater than Maximum lot size.' />" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Scrap
                                            <asp:Label ID="labletxtScrap" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtScrap" runat="server" CssClass="textbox" Width="100px" MaxLength="5"
                                                TabIndex="17" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtScrap" runat="server" ControlToValidate="txtScrap"
                                                ValidationGroup="MRP1" ErrorMessage="Scrap cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Scrap cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Unit of Measure Group
                                            <asp:Label ID="lableddlUnitMeasurGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlUnitMeasurGroup" runat="server" AppendDataBoundItems="false"
                                                TabIndex="18">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlUnitMeasurGroup" runat="server" ControlToValidate="ddlUnitMeasurGroup"
                                                ValidationGroup="MRP1" ErrorMessage="Unit of Measure Group cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Unit of Measure Group cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Production unit
                                            <asp:Label ID="lableddlProductionUnit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlProductionUnit" runat="server" AppendDataBoundItems="false"
                                                TabIndex="19">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlProductionUnit" runat="server" ControlToValidate="ddlProductionUnit"
                                                ValidationGroup="MRP1" ErrorMessage="Production unit cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Production unit cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Takt time
                                            <asp:Label ID="labletxtTaktTime" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtTaktTime" runat="server" CssClass="textbox" Width="35px" MaxLength="3"
                                                TabIndex="20" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtTaktTime" runat="server" ControlToValidate="txtTaktTime"
                                                ValidationGroup="MRP1" ErrorMessage="Takt time  cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Takt time  cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr id="trButton" runat="server" visible="false">
                                        <td class="centerTD" colspan="4">
                                            <asp:Button ID="btnPrevious" runat="server" ValidationGroup="MRP1" Text="Back" CssClass="button"
                                                UseSubmitBehavior="false" TabIndex="21" OnClick="btnPrevious_Click" />
                                            <asp:Button ID="btnSave" runat="server" ValidationGroup="MRP1" Text="Save" CssClass="button"
                                                UseSubmitBehavior="true" TabIndex="22" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnNext" runat="server" ValidationGroup="MRP1" Text="Save & Next"
                                                TabIndex="23" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="MRP1" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblMRPId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="8" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
