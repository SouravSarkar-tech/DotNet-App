<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="MRP2.aspx.cs" Inherits="Transaction_MRP2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetToolTip(control) {
            ddrivetipm('9', control);
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
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            MRP 2
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <asp:Panel ID="pnlGrid" runat="server">
                                <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                    DataKeyNames="Mat_MRP2_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
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
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
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
                                                ValidationGroup="MRP2" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
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
                                                ValidationGroup="MRP2" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Procurement Type
                                            <asp:Label ID="lableddlProcurmentType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlProcurmentType" runat="server" AppendDataBoundItems="false"
                                                TabIndex="3" AutoPostBack = "true" 
                                                OnSelectedIndexChanged="ddlProcurmentType_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlProcurmentType" runat="server" ControlToValidate="ddlProcurmentType"
                                                ValidationGroup="MRP2" ErrorMessage="Procurement Type cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Procurement Type cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Special procurement type
                                            <asp:Label ID="lableddlSpecialProcType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlSpecialProcType" runat="server" AppendDataBoundItems="false"
                                                TabIndex="4">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSpecialProcType" runat="server" ControlToValidate="ddlSpecialProcType"
                                                ValidationGroup="MRP2" ErrorMessage="Special procurement type cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Special procurement type cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Prod. Storage Location
                                            <asp:Label ID="lableddlIssueStorageLocation" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlIssueStorageLocation');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlIssueStorageLocation" runat="server" AppendDataBoundItems="false"
                                                TabIndex="5">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlIssueStorageLocation" runat="server" ControlToValidate="ddlIssueStorageLocation"
                                                ValidationGroup="MRP2" ErrorMessage="Prod. Storage Location cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Prod. Storage Location cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Default storage location for external procurement
                                            <asp:Label ID="lableddlDefaultStorage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlDefaultStorage" runat="server" AppendDataBoundItems="false"
                                                TabIndex="6">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlDefaultStorage" runat="server" ControlToValidate="ddlDefaultStorage"
                                                ValidationGroup="MRP2" ErrorMessage="Default storage location for external procurement cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Default storage location for external procurement cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Proposed Supply Area
                                            <asp:Label ID="lableddlProposedSupplyArea" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlProposedSupplyArea');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlProposedSupplyArea" runat="server" AppendDataBoundItems="false"
                                                TabIndex="7">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlProposedSupplyArea" runat="server" ControlToValidate="ddlProposedSupplyArea"
                                                ValidationGroup="MRP2" ErrorMessage="Proposed Supply Area cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Proposed Supply Area cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Indicator: Backflush
                                            <asp:Label ID="lableddlIndicatorBackflush" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlIndicatorBackflush" runat="server" AppendDataBoundItems="false"
                                                TabIndex="8">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlIndicatorBackflush" runat="server" ControlToValidate="ddlIndicatorBackflush"
                                                ValidationGroup="MRP2" ErrorMessage="Indicator: Backflush cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Indicator: Backflush cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Quota Arrangement
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlQuotaArrangementUsage');"
                                                onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" /><br />
                                            Usage
                                            <asp:Label ID="lableddlQuotaArrangementUsage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlQuotaArrangementUsage" runat="server" AppendDataBoundItems="false"
                                                TabIndex="9">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlQuotaArrangementUsage" runat="server" ControlToValidate="ddlQuotaArrangementUsage"
                                                ValidationGroup="MRP2" ErrorMessage="Quota Arrangement Usage cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Quota Arrangement Usage cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            In-house production time
                                            <asp:Label ID="labletxtInHousePTime" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtInHousePTime" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="10" onkeypress="return IsNumber();" MaxLength="3" />
                                            <asp:RequiredFieldValidator ID="reqtxtInHousePTime" runat="server" ControlToValidate="txtInHousePTime"
                                                ValidationGroup="MRP2" ErrorMessage="In-house production time  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='In-house production time  cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Planned Delivery Time in Days
                                            <asp:Label ID="labletxtPlannedDeleveryTime" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtPlannedDeleveryTime" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="11" onkeypress="return IsNumber();" MaxLength="3" />
                                            <asp:RequiredFieldValidator ID="reqtxtPlannedDeleveryTime" runat="server" ControlToValidate="txtPlannedDeleveryTime"
                                                ValidationGroup="MRP2" ErrorMessage="Planned Delivery Time in Days  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planned Delivery Time in Days  cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Planning Calander
                                            <asp:Label ID="lableddlPlanningCalander" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlPlanningCalander" runat="server" AppendDataBoundItems="false"
                                                TabIndex="13">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPlanningCalander" runat="server" ControlToValidate="ddlPlanningCalander"
                                                ValidationGroup="MRP2" ErrorMessage="Planning Calander cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Planning Calander cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Scheduling Margin
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlSchedulingMKey');"
                                                onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" /><br />
                                            Key for Floats
                                            <asp:Label ID="lableddlSchedulingMKey" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlSchedulingMKey" runat="server" AppendDataBoundItems="false"
                                                TabIndex="14">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSchedulingMKey" runat="server" ControlToValidate="ddlSchedulingMKey"
                                                ValidationGroup="MRP2" ErrorMessage="Scheduling Margin Key for Floats cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Scheduling Margin Key for Floats cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            GR Processing Time
                                            <asp:Label ID="labletxtGRProcessingTime" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtGRProcessingTime" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="12" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtGRProcessingTime" runat="server" ControlToValidate="txtGRProcessingTime"
                                                ValidationGroup="MRP2" ErrorMessage="GR Processing Time cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='GR Processing Time cannot be blank.' />" />
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
                                            Safety stock
                                            <asp:Label ID="labletxtSafetyStock" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtSafetyStock" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="15" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtSafetyStock" runat="server" ControlToValidate="txtSafetyStock"
                                                ValidationGroup="MRP2" ErrorMessage="Safety stock  cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Safety stock  cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Min Safety Stock
                                            <asp:Label ID="labletxtMinSafetyStock" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtMinSafetyStock" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="16" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtMinSafetyStock" runat="server" ControlToValidate="txtMinSafetyStock"
                                                ValidationGroup="MRP2" ErrorMessage="Min Safety Stock cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Min Safety Stock cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Range of coverage profile
                                            <asp:Label ID="lableddlRangeCoverage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlRangeCoverage');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlRangeCoverage" runat="server" AppendDataBoundItems="false"
                                                TabIndex="17">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlRangeCoverage" runat="server" ControlToValidate="ddlRangeCoverage"
                                                ValidationGroup="MRP2" ErrorMessage="Range of coverage profile cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Range of coverage profile cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Safety time in workdays (for data transfer)
                                            <asp:Label ID="labletxtSafetyTimeWorkdays" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtSafetyTimeWorkdays" runat="server" CssClass="textbox" Width="100px"
                                                onkeypress="return IsOnlyNumber();" TabIndex="18" MaxLength="2" />
                                            <asp:RequiredFieldValidator ID="reqtxtSafetyTimeWorkdays" runat="server" ControlToValidate="txtSafetyTimeWorkdays"
                                                ValidationGroup="MRP2" ErrorMessage=" Safety time in workdays cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Safety time in workdays cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Safety time indicator (with or without safety time)
                                            <asp:Label ID="lableddlSafetyTimeIndicator" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlSafetyTimeIndicator" runat="server" AppendDataBoundItems="false"
                                                TabIndex="19">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSafetyTimeIndicator" runat="server" ControlToValidate="ddlSafetyTimeIndicator"
                                                ValidationGroup="MRP2" ErrorMessage="Safety time indicator cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Safety time indicator cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Indicator: Bulk Material
                                            <asp:Label ID="lablechkIndicatorBulkMaterial" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:CheckBox ID="chkIndicatorBulkMaterial" runat="server" Text="Check if Relevant"
                                                TabIndex="20" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Determination of batch entry in the production/process
                                            <asp:Label ID="lableddlDetermination" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlDetermination" runat="server" AppendDataBoundItems="false"
                                                TabIndex="21">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlDetermination" runat="server" ControlToValidate="ddlDetermination"
                                                ValidationGroup="MRP2" ErrorMessage="Determination of batch entry in the production/process cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Determination of batch entry in the production/process cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Indicator: Item Relevant to JIT Delivery Schedules
                                            <asp:Label ID="lableddlIndicatorItemRelevant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlIndicatorItemRelevant" runat="server" AppendDataBoundItems="false"
                                                TabIndex="22">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlIndicatorItemRelevant" runat="server" ControlToValidate="ddlIndicatorItemRelevant"
                                                ValidationGroup="MRP2" ErrorMessage="Item Relevant to JIT Delivery Schedules cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Item Relevant to JIT Delivery Schedules cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Period Profile for Safety Time
                                            <asp:Label ID="lableddlPeriodProfileSafetyTime" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlPeriodProfileSafetyTime" runat="server" AppendDataBoundItems="false"
                                                TabIndex="23">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPeriodProfileSafetyTime" runat="server" ControlToValidate="ddlPeriodProfileSafetyTime"
                                                ValidationGroup="MRP2" ErrorMessage="Period Profile for Safety Time cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Period Profile for Safety Time cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Lower Limit for Safety Stock
                                            <asp:Label ID="labletxtLowerLimitSafetyStock" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtLowerLimitSafetyStock" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="24" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtLowerLimitSafetyStock" runat="server" ControlToValidate="txtLowerLimitSafetyStock"
                                                ValidationGroup="MRP2" ErrorMessage="Lower Limit for Safety Stock (BCTI) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Lower Limit for Safety Stock (BCTI) cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Production Scheduling Profile
                                            <asp:Label ID="lableddlProductionSProfile" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlProductionSProfile" runat="server" AppendDataBoundItems="false"
                                                TabIndex="25">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlProductionSProfile" runat="server" ControlToValidate="ddlProductionSProfile"
                                                ValidationGroup="MRP2" ErrorMessage="Special procurement type cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Special procurement type cannot be blank.' />" />
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
                                            <asp:Button ID="btnPrevious" runat="server" ValidationGroup="MRP2" Text="Back" TabIndex="26"
                                                UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                            <asp:Button ID="btnSave" runat="server" ValidationGroup="MRP2" Text="Save" CssClass="button"
                                                TabIndex="27" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnNext" runat="server" ValidationGroup="MRP2" Text="Save & Next"
                                                TabIndex="28" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="MRP2" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblMRPId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="9" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
