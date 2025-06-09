<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="MRP3.aspx.cs" Inherits="Transaction_MRP3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetToolTip(control) {
            ddrivetipm('10', control);
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
                            MRP 3
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <asp:Panel ID="pnlGrid" runat="server">
                                <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                    DataKeyNames="Mat_MRP3_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
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
                                                ValidationGroup="MRP3" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
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
                                                ValidationGroup="MRP3" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Period Indicator
                                            <asp:Label ID="lableddlPerioIndicator" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlPerioIndicator" runat="server" AppendDataBoundItems="false"
                                                TabIndex="3">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPerioIndicator" runat="server" ControlToValidate="ddlPerioIndicator"
                                                ValidationGroup="MRP3" ErrorMessage="Period Indicator cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Period Indicator cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Checking Group for Availability Check
                                            <asp:Label ID="lableddlAvailabilityCheck" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlAvailabilityCheck" runat="server" AppendDataBoundItems="false"
                                                TabIndex="4">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlAvailabilityCheck" runat="server" ControlToValidate="ddlAvailabilityCheck"
                                                ValidationGroup="MRP3" ErrorMessage="Checking Group for Availability Check cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Checking Group for Availability Check cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Planning strategy group
                                            <asp:Label ID="lableddlPlanningSGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlPlanningSGroup" runat="server" AppendDataBoundItems="false"
                                                TabIndex="5">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPlanningSGroup" runat="server" ControlToValidate="ddlPlanningSGroup"
                                                ValidationGroup="MRP3" ErrorMessage="Planning strategy group cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planning strategy group cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Consumption mode
                                            <asp:Label ID="lableddlConsumptionMode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlConsumptionMode" runat="server" AppendDataBoundItems="false"
                                                TabIndex="6">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlConsumptionMode" runat="server" ControlToValidate="ddlConsumptionMode"
                                                ValidationGroup="MRP3" ErrorMessage="Consumption mode cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Consumption mode cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Splitting Indicator
                                            <asp:Label ID="lableddlSplitingIndicator" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlSplitingIndicator" runat="server" AppendDataBoundItems="false"
                                                TabIndex="7">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSplitingIndicator" runat="server" ControlToValidate="ddlSplitingIndicator"
                                                ValidationGroup="MRP3" ErrorMessage="Splitting Indicator cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Splitting Indicator cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Fiscal Year Variant
                                            <asp:Label ID="lableddlFiscalYearVariant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlFiscalYearVariant" runat="server" AppendDataBoundItems="false"
                                                TabIndex="8">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlFiscalYearVariant" runat="server" ControlToValidate="ddlFiscalYearVariant"
                                                ValidationGroup="MRP3" ErrorMessage="Fiscal Year Variant cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Fiscal Year Variant cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                        <td class="leftTD" style="width: 20%">
                                            Backward consumption period 
                                            <asp:Label ID="labletxtBackwardCPeriod" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtBackwardCPeriod" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="9" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtBackwardCPeriod" runat="server" ControlToValidate="txtBackwardCPeriod"
                                                ValidationGroup="MRP3" ErrorMessage="Backward consumption period  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Backward consumption period  cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Forward consumption period 
                                            <asp:Label ID="labletxtForwardCPeriod" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtForwardCPeriod" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="10" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtForwardCPeriod" runat="server" ControlToValidate="txtForwardCPeriod"
                                                ValidationGroup="MRP3" ErrorMessage="Forward consumption period  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Forward consumption period  cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                        <td class="leftTD" style="width: 20%">
                                            Mixed MRP indicator
                                            <asp:Label ID="lableddlMixedMrpIndicator" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlMixedMrpIndicator" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMixedMrpIndicator" runat="server" ControlToValidate="ddlMixedMrpIndicator"
                                                ValidationGroup="MRP3" ErrorMessage="Mixed MRP indicator cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Mixed MRP indicator cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Total replenishment lead time 
                                            <asp:Label ID="labletxtTotalReplenishment" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtTotalReplenishment" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="12" onkeypress="return IsNumber();" MaxLength="3" />
                                            <asp:RequiredFieldValidator ID="reqtxtTotalReplenishment" runat="server" ControlToValidate="txtTotalReplenishment"
                                                ValidationGroup="MRP3" ErrorMessage="Total replenishment lead time  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Total replenishment lead time  cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        
                                        <td class="leftTD" style="width: 20%">
                                            Planning material
                                            <asp:Label ID="lableddlPlanningMaterial" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlPlanningMaterial" runat="server" AppendDataBoundItems="false"
                                                TabIndex="13">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPlanningMaterial" runat="server" ControlToValidate="ddlPlanningMaterial"
                                                ValidationGroup="MRP3" ErrorMessage="Planning material cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Planning material cannot be blank.' />" />
                                        </td>
                                        <td class="tdSpace" colspan="2">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr c>
                                        <td class="leftTD" style="width: 20%">
                                            Planning plant
                                            <asp:Label ID="lableddlPlanningPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlPlanningPlant" runat="server" AppendDataBoundItems="false"
                                                TabIndex="14">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPlanningPlant" runat="server" ControlToValidate="ddlPlanningPlant"
                                                ValidationGroup="MRP3" ErrorMessage="Planning plant cannot be blank." SetFocusOnError="true"
                                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planning plant cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Conv. factor f. plng material
                                            <asp:Label ID="labletxtConvFacyor" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtConvFacyor" runat="server" CssClass="textbox" Width="100px" MaxLength="10"
                                                TabIndex="15" />
                                            <asp:RequiredFieldValidator ID="reqtxtConvFacyor" runat="server" ControlToValidate="txtConvFacyor"
                                                ValidationGroup="MRP3" ErrorMessage="Conv. factor f. plng material cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Conv. factor f. plng material cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr id="trButton" runat="server" visible="false">
                                        <td class="centerTD" colspan="4">
                                            <asp:Button ID="btnPrevious" runat="server" ValidationGroup="MRP3" Text="Back" TabIndex="16"
                                                UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                            <asp:Button ID="btnSave" runat="server" ValidationGroup="MRP3" Text="Save" CssClass="button"
                                                TabIndex="17" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnNext" runat="server" ValidationGroup="MRP3" Text="Save & Next"
                                                TabIndex="18" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="MRP3" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblMRPId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="10" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
