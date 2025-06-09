<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/PriceMaster/PriceMasterPage.master"
    AutoEventWireup="true" CodeFile="PriceHeader.aspx.cs" Inherits="Transaction_PriceMaster_PriceHeader" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
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
                    Price Master
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
                                    Material
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:DropDownList ID="ddlMaterial" runat="server" />
                                    <asp:RequiredFieldValidator ID="reqddlMaterial" runat="server" ControlToValidate="ddlMaterial"
                                        ValidationGroup="PriceHeader" ErrorMessage="Material cannot be blank." InitialValue="0"
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Batch
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:DropDownList ID="ddlBatch" runat="server">
                                        <asp:ListItem Text="--Select Batch--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqddlBatch" runat="server" ControlToValidate="ddlBatch"
                                        ValidationGroup="PriceHeader" ErrorMessage="Batch cannot be blank." InitialValue="0"
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Batch cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Valid From
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtValidityDateFrom" runat="server" CssClass="textbox" Width="100px" />
                                    <act:CalendarExtender ID="CaltxtValidityDateFrom" runat="server" Format="dd/MM/yyyy" TargetControlID="txtValidityDateFrom" />
                                    <asp:RequiredFieldValidator ID="reqtxtValidityDateFrom" runat="server" ControlToValidate="txtValidityDateFrom"
                                        ValidationGroup="PriceHeader" ErrorMessage="Valid From cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../images/Error.png' title='Valid From cannot be blank.' />" />
                                    <asp:RegularExpressionValidator ID="regtxtStartDate" runat="server" ControlToValidate="txtValidityDateFrom"
                                        ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                        ValidationGroup="PriceHeader" Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                        Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Plant
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:DropDownList ID="ddlPlant" runat="server">
                                        <asp:ListItem Text="--Select Plant--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                        ValidationGroup="PriceHeader" ErrorMessage="Plant cannot be blank." InitialValue="0"
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Customer
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:DropDownList ID="ddlCustomer" runat="server" ><asp:ListItem Text="--Select Vendor--" Value="0"></asp:ListItem></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqddlCustomer" runat="server" ControlToValidate="ddlCustomer"
                                        ValidationGroup="PriceHeader" ErrorMessage="Customer cannot be blank." InitialValue="0"
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Vendor
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:DropDownList ID="ddlVendor" runat="server">
                                        <asp:ListItem Text="--Select Vendor--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqddlVendor" runat="server" ControlToValidate="ddlVendor"
                                        ValidationGroup="PriceHeader" ErrorMessage="Vendor cannot be blank." InitialValue="0"
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Price Group
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtPriceGroup" runat="server" CssClass="textboxAutocomplete" Width="100px" MaxLength="2"
                                        onfocus="return PriceGroupOnFocus();" onchange="return PriceGroupTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtPriceGroup" runat="server" ControlToValidate="txtPriceGroup"
                                        ValidationGroup="PriceHeader" ErrorMessage="Price Group cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Group cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Processing Status
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtProcessingStatus" runat="server" CssClass="textboxAutocomplete" Width="100px" MaxLength="2"
                                        onfocus="return ProcessingStatusOnFocus();" onchange="return ProcessingStatusTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtProcessingStatus" runat="server" ControlToValidate="txtProcessingStatus"
                                        ValidationGroup="PriceHeader" ErrorMessage="Processing Status cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Processing Status cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Base Unit of Measure
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtBaseUnitofMeasure" runat="server" CssClass="textboxAutocomplete" Width="100px" MaxLength="3"
                                        onfocus="return BaseUnitofMeasureOnFocus();" onchange="return BaseUnitofMeasureTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtBaseUnitofMeasure" runat="server" ControlToValidate="txtBaseUnitofMeasure"
                                        ValidationGroup="PriceHeader" ErrorMessage="Base Unit of Measure cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Base Unit of Measure cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Rate Unit
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtRateUnit" runat="server" CssClass="textboxAutocomplete" Width="100px" MaxLength="5"
                                        onfocus="return RateUnitOnFocus();" onchange="return RateUnitTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtRateUnit" runat="server" ControlToValidate="txtRateUnit"
                                        ValidationGroup="PriceHeader" ErrorMessage="Rate Unit cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Rate Unit cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Trade Price
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtTradePrice" runat="server" CssClass="textbox" Width="100px" MaxLength="13" />
                                    <asp:RequiredFieldValidator ID="reqtxtTradePrice" runat="server" ControlToValidate="txtTradePrice"
                                        ValidationGroup="PriceHeader" ErrorMessage="Trade Price cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Trade Price cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Excise  Duty 
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtExciseDuty" runat="server" CssClass="textbox" Width="100px" MaxLength="13" />
                                    <asp:RequiredFieldValidator ID="reqtxtExciseDuty" runat="server" ControlToValidate="txtExciseDuty"
                                        ValidationGroup="PriceHeader" ErrorMessage="Excise  Duty cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Excise  Duty cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Education Cess 0.02
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtEducationCess" runat="server" CssClass="textbox" Width="100px" MaxLength="13" />
                                    <asp:RequiredFieldValidator ID="reqtxtEducationCess" runat="server" ControlToValidate="txtEducationCess"
                                        ValidationGroup="PriceHeader" ErrorMessage="Education Cess 0.02 cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Education Cess 0.02 cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 20%">
                                    Secondary & Higher E cess 1 %
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtSecHighEduCess" runat="server" CssClass="textbox" Width="100px" MaxLength="13"/>
                                    <asp:RequiredFieldValidator ID="reqtxtSecHighEduCess" runat="server" ControlToValidate="txtSecHighEduCess"
                                        ValidationGroup="PriceHeader" ErrorMessage="Processing Status cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Processing Status cannot be blank.' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    MRP Incl.of (All Taxes)
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtMRP" runat="server" CssClass="textbox" Width="100px" MaxLength="13" />
                                    <asp:RequiredFieldValidator ID="reqtxtMRP" runat="server" ControlToValidate="txtMRP"
                                        ValidationGroup="PriceHeader" ErrorMessage="MRP Incl.of (All Taxes) cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='MRP Incl.of (All Taxes) cannot be blank.' />" />
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
                                    <asp:Button ID="btnPrevious" runat="server" CausesValidation="false" Text="Back"
                                        CssClass="button" OnClick="btnPrevious_Click" />
                                    <asp:Button ID="btnSave" runat="server" ValidationGroup="PriceHeader" Text="Save"
                                        CssClass="button" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnNext" runat="server" ValidationGroup="PriceHeader" Text="Save & Next"
                                        CssClass="button" OnClick="btnNext_Click" Width="120px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="PriceHeader" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblPriceHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <script type="text/javascript">
        var textboxId = "";
        var textboxRealId = "";

        function PriceGroupOnFocus() {
            textboxId = $('#<%= txtPriceGroup.ClientID%>').attr('ID');
            textboxRealId = "txtPriceGroup";
            AutoCompleteLookUpPrice();
        }

        function PriceGroupTextChangeEvent() {
            CheckLookupPrice($('#<%= txtPriceGroup.ClientID%>').attr('ID'), "txtPriceGroup", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function ProcessingStatusOnFocus() {
            textboxId = $('#<%= txtProcessingStatus.ClientID%>').attr('ID');
            textboxRealId = "txtProcessingStatus";
            AutoCompleteLookUpPrice();
        }

        function ProcessingStatusTextChangeEvent() {
            CheckLookupPrice($('#<%= txtProcessingStatus.ClientID%>').attr('ID'), "txtProcessingStatus", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function BaseUnitofMeasureOnFocus() {
            textboxId = $('#<%= txtBaseUnitofMeasure.ClientID%>').attr('ID');
            textboxRealId = "txtBaseUnitofMeasure";
            AutoCompleteLookUpPrice();
        }

        function BaseUnitofMeasureTextChangeEvent() {
            CheckLookupPrice($('#<%= txtBaseUnitofMeasure.ClientID%>').attr('ID'), "txtBaseUnitofMeasure", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function RateUnitOnFocus() {
            textboxId = $('#<%= txtRateUnit.ClientID%>').attr('ID');
            textboxRealId = "txtRateUnit";
            AutoCompleteLookUpPrice();
        }

        function RateUnitTextChangeEvent() {
            CheckLookupPrice($('#<%= txtRateUnit.ClientID%>').attr('ID'), "txtRateUnit", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

    </script>
</asp:Content>
