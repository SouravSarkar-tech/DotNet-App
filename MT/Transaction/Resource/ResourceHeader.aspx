<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Resource/ResourceMasterPage.master"
    AutoEventWireup="true" CodeFile="ResourceHeader.aspx.cs" Inherits="Transaction_Resource_ResourceHeader" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlAddNew" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">
                    Resource Master
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
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
                                <td class="leftTD" style="width: 20%">
                                    Plant
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <cc1:DropDownCheckBoxes ID="ddlPlant" runat="server" AddJQueryReference="false" UseButtons="false"
                                        UseSelectAllNode="true" AutoPostBack="false">
                                        <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="250" DropDownBoxBoxHeight="80" />
                                        <Texts SelectBoxCaption="--Select--" />
                                    </cc1:DropDownCheckBoxes>
                                </td>
                                <td class="tdSpace" colspan="2" align="right">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Resourse
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtResource" runat="server" CssClass="textbox" Width="100px" MaxLength="8" />
                                    <asp:RequiredFieldValidator ID="reqtxtResource" runat="server" ControlToValidate="txtResource"
                                        ValidationGroup="Resource" ErrorMessage="Resourse cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Resourse cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Object Name
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtObjectName" runat="server" CssClass="textbox" MaxLength="40"
                                        Width="100px" />
                                    <asp:RequiredFieldValidator ID="reqtxtObjectName" runat="server" ControlToValidate="txtObjectName"
                                        ValidationGroup="Resource" ErrorMessage="Object Name cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Object Name cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Person responsible for the work center
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtPersonRespWorkCenter" runat="server" CssClass="textboxAutocomplete" Width="100px"
                                        MaxLength="3" onfocus="return PersonRespWorkCenterOnFocus();" onchange="return PersonRespWorkCenterTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtPersonRespWorkCenter" runat="server" ControlToValidate="txtPersonRespWorkCenter"
                                        ValidationGroup="Resource" ErrorMessage="Person responsible for the work center cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Person responsible for the work center cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Standard value key
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtStandardValueKey" runat="server" CssClass="textboxAutocomplete" MaxLength="4"
                                        Width="100px" onfocus="return StandardValueKeyOnFocus();" onchange="return StandardValueKeyTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtStandardValueKey" runat="server" ControlToValidate="txtStandardValueKey"
                                        ValidationGroup="Resource" ErrorMessage="Standard value key cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Standard value key cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Unit of measure for the standard value
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtUnitOfMeasureStdValue" runat="server" CssClass="textboxAutocomplete" Width="100px"
                                        MaxLength="3" onfocus="return UnitOfMeasureStdValueOnFocus();" onchange="return UnitOfMeasureStdValueTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtUnitOfMeasureStdValue" runat="server" ControlToValidate="txtUnitOfMeasureStdValue"
                                        ValidationGroup="Resource" ErrorMessage="Unit of measure for the standard value cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Person responsible for the work center cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Unit of measure for the standard value 2
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtUnitOfMeasureStdValue2" runat="server" CssClass="textboxAutocomplete" MaxLength="3"
                                        Width="100px" onfocus="return UnitOfMeasureStdValue2OnFocus();" onchange="return UnitOfMeasureStdValue2TextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtUnitOfMeasureStdValue2" runat="server" ControlToValidate="txtUnitOfMeasureStdValue2"
                                        ValidationGroup="Resource" ErrorMessage="Unit of measure for the standard value 2 cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=Unit of measure for the standard value 2 cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Formula for cap. reqmts. for other types of int. process
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtFormulaCapReqIntProcess" runat="server" CssClass="textboxAutocomplete" Width="100px"
                                        MaxLength="6" onfocus="return FormulaCapReqIntProcessOnFocus();" onchange="return FormulaCapReqIntProcessTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtFormulaCapReqIntProcess" runat="server" ControlToValidate="txtFormulaCapReqIntProcess"
                                        ValidationGroup="Resource" ErrorMessage="Formula for cap. reqmts. for other types of int. process cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Formula for cap. reqmts. for other types of int. process cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Capacity short text
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtCapacityShortText" runat="server" CssClass="textbox" MaxLength="40"
                                        Width="100px" />
                                    <asp:RequiredFieldValidator ID="reqtxtCapacityShortText" runat="server" ControlToValidate="txtCapacityShortText"
                                        ValidationGroup="Resource" ErrorMessage="Capacity short text cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Capacity short text cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Capacity planner group
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtCapacityPlannerGroup" runat="server" CssClass="textboxAutocomplete" Width="100px"
                                        MaxLength="3" onfocus="return CapacityPlannerGroupOnFocus();" onchange="return CapacityPlannerGroupTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtCapacityPlannerGroup" runat="server" ControlToValidate="txtCapacityPlannerGroup"
                                        ValidationGroup="Resource" ErrorMessage="Capacity planner group cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Capacity planner group cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Capacity utilization rate (percent)
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtCapacityUtilizationRate" runat="server" CssClass="textbox" MaxLength="4"
                                        Width="100px" />
                                    <asp:RequiredFieldValidator ID="reqtxtCapacityUtilizationRate" runat="server" ControlToValidate="txtCapacityUtilizationRate"
                                        ValidationGroup="Resource" ErrorMessage="Capacity utilization rate (percent) cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Capacity utilization rate (percent) cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Start time
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtStartTime" runat="server" CssClass="textbox" Width="100px" MaxLength="10" />
                                    <asp:RequiredFieldValidator ID="reqtxtStartTime" runat="server" ControlToValidate="txtStartTime"
                                        ValidationGroup="Resource" ErrorMessage="Start time cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Start time cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Finish time
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtFinishTime" runat="server" CssClass="textbox" MaxLength="10"
                                        Width="100px" />
                                    <asp:RequiredFieldValidator ID="reqtxtFinishTime" runat="server" ControlToValidate="txtFinishTime"
                                        ValidationGroup="Resource" ErrorMessage="Finish time cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Finish time cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Cumulative length of breaks per shift
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtCumulativeLenBreakPerShift" runat="server" CssClass="textbox"
                                        Width="100px" MaxLength="10" />
                                    <asp:RequiredFieldValidator ID="reqtxtCumulativeLenBreakPerShift" runat="server"
                                        ControlToValidate="txtCumulativeLenBreakPerShift" ValidationGroup="Resource"
                                        ErrorMessage="Cumulative length of breaks per shift cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Cumulative length of breaks per shift cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Number of individual capacities
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtNumberOfIndCap" runat="server" CssClass="textbox" MaxLength="5"
                                        Width="100px" />
                                    <asp:RequiredFieldValidator ID="reqtxtNumberOfIndCap" runat="server" ControlToValidate="txtNumberOfIndCap"
                                        ValidationGroup="Resource" ErrorMessage="Number of individual capacities cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Number of individual capacities cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Base Unit of Measurement for Capacity
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtBaseUOMCapacity" runat="server" CssClass="textbox" Width="100px"
                                        MaxLength="3" />
                                    <asp:RequiredFieldValidator ID="reqtxtBaseUOMCapacity" runat="server" ControlToValidate="txtBaseUOMCapacity"
                                        ValidationGroup="Resource" ErrorMessage="Base Unit of Measurement for Capacity cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Base Unit of Measurement for Capacity cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Formula for the duration of other types of int. processing
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtFormulaDurationIntProcess" runat="server" CssClass="textboxAutocomplete"
                                        MaxLength="6" Width="100px" onfocus="return FormulaDurationIntProcessOnFocus();"
                                        onchange="return FormulaDurationIntProcessTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtFormulaDurationIntProcess" runat="server" ControlToValidate="txtFormulaDurationIntProcess"
                                        ValidationGroup="Resource" ErrorMessage="Formula for the duration of other types of int. processing cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Formula for the duration of other types of int. processing cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Start Date
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="textbox" Width="100px" MaxLength="10" />
                                    <act:CalendarExtender ID="CaltxtStartDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtStartDate" />
                                    <asp:RequiredFieldValidator ID="reqtxtStartDate" runat="server" ControlToValidate="txtStartDate"
                                        ValidationGroup="Resource" ErrorMessage="Start Date cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Start Date cannot be blank.' />" />
                                    <asp:RegularExpressionValidator ID="regtxtStartDate" runat="server" ControlToValidate="txtStartDate"
                                        ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                        ValidationGroup="Resource" Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                        Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    End Date
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="textbox" MaxLength="10" Width="100px" />
                                    <act:CalendarExtender ID="CaltxtEndDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEndDate" />
                                    <asp:RequiredFieldValidator ID="reqtxtEndDate" runat="server" ControlToValidate="txtEndDate"
                                        ValidationGroup="Resource" ErrorMessage="End Date cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='End Date cannot be blank.' />" />
                                    <asp:RegularExpressionValidator ID="regtxtEndDate" runat="server" ControlToValidate="txtEndDate"
                                        ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                        ValidationGroup="Resource" Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                        Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Cost Center
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtCostCenter" runat="server" CssClass="textboxAutocomplete" Width="100px" MaxLength="10"
                                        onfocus="return CostCenterOnFocus();" onchange="return CostCenterTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtCostCenter" runat="server" ControlToValidate="txtCostCenter"
                                        ValidationGroup="Resource" ErrorMessage="Cost Center cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Cost Center cannot be blank.' />" />
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
                                    Activity Type
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtActivityType" runat="server" CssClass="textboxAutocomplete" Width="100px" MaxLength="6"
                                        onfocus="return ActivityTypeOnFocus();" onchange="return ActivityTypeTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtActivityType" runat="server" ControlToValidate="txtActivityType"
                                        ValidationGroup="Resource" ErrorMessage="Activity Type cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Activity Type cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Activity Type 2
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtActivityType2" runat="server" CssClass="textboxAutocomplete" MaxLength="6" Width="100px"
                                        onfocus="return ActivityType2OnFocus();" onchange="return ActivityType2TextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtActivityType2" runat="server" ControlToValidate="txtActivityType2"
                                        ValidationGroup="Resource" ErrorMessage="Activity Type 2 cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Activity Type 2 cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Activity unit
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtActivityUnit" runat="server" CssClass="textboxAutocomplete" Width="100px" MaxLength="3"
                                        onfocus="return ActivityUnitOnFocus();" onchange="return ActivityUnitTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtActivityUnit" runat="server" ControlToValidate="txtActivityUnit"
                                        ValidationGroup="Resource" ErrorMessage="Activity unit cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Activity unit cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Activity unit 2
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtActivityUnit2" runat="server" CssClass="textboxAutocomplete" MaxLength="3" Width="100px"
                                        onfocus="return ActivityUnit2OnFocus();" onchange="return ActivityUnit2TextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtActivityUnit2" runat="server" ControlToValidate="txtActivityUnit2"
                                        ValidationGroup="Resource" ErrorMessage="Activity unit 2 cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Activity unit 2 cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Formula key
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtFormulaKey" runat="server" CssClass="textboxAutocomplete" Width="100px" MaxLength="3"
                                        onfocus="return FormulaKeyOnFocus();" onchange="return FormulaKeyTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtFormulaKey" runat="server" ControlToValidate="txtFormulaKey"
                                        ValidationGroup="Resource" ErrorMessage="Formula key cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Formula key cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Formula key 2
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtFormulaKey2" runat="server" CssClass="textboxAutocomplete" MaxLength="4" Width="100px"
                                        onfocus="return FormulaKey2OnFocus();" onchange="return FormulaKey2TextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtFormulaKey2" runat="server" ControlToValidate="txtFormulaKey2"
                                        ValidationGroup="Resource" ErrorMessage="Formula key 2 cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Formula key 2 cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr id="trButton" runat="server" visible="false">
                                <td class="centerTD" colspan="4">
                                    <asp:Button ID="btnPrevious" runat="server" CausesValidation="false" Text="Back"
                                        CssClass="button" OnClick="btnPrevious_Click" />
                                    <asp:Button ID="btnSave" runat="server" ValidationGroup="Resource" Text="Save"
                                        CssClass="button" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnNext" runat="server" ValidationGroup="Resource" Text="Save & Next"
                                        CssClass="button" OnClick="btnNext_Click" Width="120px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Resource" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblResourceMasterId" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <script type="text/javascript">
        var textboxId = "";
        var textboxRealId = "";

        function PersonRespWorkCenterOnFocus() {
            textboxId = $('#<%= txtPersonRespWorkCenter.ClientID%>').attr('ID');
            textboxRealId = "txtPersonRespWorkCenter";
            AutoCompleteLookUpResource();
        }

        function PersonRespWorkCenterTextChangeEvent() {
            CheckLookupResource($('#<%= txtPersonRespWorkCenter.ClientID%>').attr('ID'), "txtPersonRespWorkCenter", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function StandardValueKeyOnFocus() {
            textboxId = $('#<%= txtStandardValueKey.ClientID%>').attr('ID');
            textboxRealId = "txtStandardValueKey";
            AutoCompleteLookUpResource();
        }

        function StandardValueKeyTextChangeEvent() {
            CheckLookupResource($('#<%= txtStandardValueKey.ClientID%>').attr('ID'), "txtStandardValueKey", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function UnitOfMeasureStdValueOnFocus() {
            textboxId = $('#<%= txtUnitOfMeasureStdValue.ClientID%>').attr('ID');
            textboxRealId = "txtUnitOfMeasureStdValue";
            AutoCompleteLookUpResource();
        }

        function UnitOfMeasureStdValueTextChangeEvent() {
            CheckLookupResource($('#<%= txtUnitOfMeasureStdValue.ClientID%>').attr('ID'), "txtUnitOfMeasureStdValue", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function UnitOfMeasureStdValue2OnFocus() {
            textboxId = $('#<%= txtUnitOfMeasureStdValue2.ClientID%>').attr('ID');
            textboxRealId = "txtUnitOfMeasureStdValue";
            AutoCompleteLookUpResource();
        }

        function UnitOfMeasureStdValue2TextChangeEvent() {
            CheckLookupResource($('#<%= txtUnitOfMeasureStdValue2.ClientID%>').attr('ID'), "txtUnitOfMeasureStdValue", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function FormulaCapReqIntProcessOnFocus() {
            textboxId = $('#<%= txtFormulaCapReqIntProcess.ClientID%>').attr('ID');
            textboxRealId = "txtFormulaCapReqIntProcess";
            AutoCompleteLookUpResource();
        }

        function FormulaCapReqIntProcessTextChangeEvent() {
            CheckLookupResource($('#<%= txtFormulaCapReqIntProcess.ClientID%>').attr('ID'), "txtFormulaCapReqIntProcess", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function CapacityPlannerGroupOnFocus() {
            textboxId = $('#<%= txtCapacityPlannerGroup.ClientID%>').attr('ID');
            textboxRealId = "txtCapacityPlannerGroup";
            AutoCompleteLookUpResource();
        }

        function CapacityPlannerGroupTextChangeEvent() {
            CheckLookupResource($('#<%= txtCapacityPlannerGroup.ClientID%>').attr('ID'), "txtCapacityPlannerGroup", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function FormulaDurationIntProcessOnFocus() {
            textboxId = $('#<%= txtFormulaDurationIntProcess.ClientID%>').attr('ID');
            textboxRealId = "txtFormulaDurationIntProcess";
            AutoCompleteLookUpResource();
        }

        function FormulaDurationIntProcessTextChangeEvent() {
            CheckLookupResource($('#<%= txtFormulaDurationIntProcess.ClientID%>').attr('ID'), "txtFormulaDurationIntProcess", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function CostCenterOnFocus() {
            textboxId = $('#<%= txtCostCenter.ClientID%>').attr('ID');
            textboxRealId = "txtCostCenter";
            AutoCompleteLookUpResource();
        }

        function CostCenterTextChangeEvent() {
            CheckLookupResource($('#<%= txtCostCenter.ClientID%>').attr('ID'), "txtCostCenter", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function ActivityTypeOnFocus() {
            textboxId = $('#<%= txtActivityType.ClientID%>').attr('ID');
            textboxRealId = "txtActivityType";
            AutoCompleteLookUpResource();
        }

        function ActivityTypeTextChangeEvent() {
            CheckLookupResource($('#<%= txtActivityType.ClientID%>').attr('ID'), "txtActivityType", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function ActivityType2OnFocus() {
            textboxId = $('#<%= txtActivityType2.ClientID%>').attr('ID');
            textboxRealId = "txtActivityType";
            AutoCompleteLookUpResource();
        }

        function ActivityType2TextChangeEvent() {
            CheckLookupResource($('#<%= txtActivityType2.ClientID%>').attr('ID'), "txtActivityType", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function ActivityUnitOnFocus() {
            textboxId = $('#<%= txtActivityUnit.ClientID%>').attr('ID');
            textboxRealId = "txtActivityUnit";
            AutoCompleteLookUpResource();
        }

        function ActivityUnitTextChangeEvent() {
            CheckLookupResource($('#<%= txtActivityUnit.ClientID%>').attr('ID'), "txtActivityUnit", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function ActivityUnit2OnFocus() {
            textboxId = $('#<%= txtActivityUnit2.ClientID%>').attr('ID');
            textboxRealId = "txtActivityUnit";
            AutoCompleteLookUpResource();
        }

        function ActivityUnit2TextChangeEvent() {
            CheckLookupResource($('#<%= txtActivityUnit2.ClientID%>').attr('ID'), "txtActivityUnit", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function FormulaKeyOnFocus() {
            textboxId = $('#<%= txtFormulaKey.ClientID%>').attr('ID');
            textboxRealId = "txtFormulaKey";
            AutoCompleteLookUpResource();
        }

        function FormulaKeyTextChangeEvent() {
            CheckLookupResource($('#<%= txtFormulaKey.ClientID%>').attr('ID'), "txtFormulaKey", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function FormulaKey2OnFocus() {
            textboxId = $('#<%= txtFormulaKey2.ClientID%>').attr('ID');
            textboxRealId = "txtFormulaKey";
            AutoCompleteLookUpResource();
        }

        function FormulaKey2TextChangeEvent() {
            CheckLookupResource($('#<%= txtFormulaKey2.ClientID%>').attr('ID'), "txtFormulaKey", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
    </script>
</asp:Content>
