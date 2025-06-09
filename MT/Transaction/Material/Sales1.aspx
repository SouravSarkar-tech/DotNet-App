<%@ Page Title="Sales1" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="Sales1.aspx.cs" Inherits="Transaction_Sales1" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialSales" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="true ">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </asp:Panel>
            <asp:Panel ID="pnlData" runat="server">
                <table border="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Sales 1
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                            DataKeyNames="Mat_Sales1_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                            GridLines="Both">
                                            <RowStyle CssClass="light-gray" />
                                            <HeaderStyle CssClass="gridHeader" />
                                            <AlternatingRowStyle CssClass="gridRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Plant" DataField="Plant" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("Mat_Sales1_Id") %>' />
                                                        <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="3">
                                    </td>
                                    <td class="tdSpace" colspan="2" align="right">
                                        <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Plant
                                        <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                            ValidationGroup="Sales" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
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
                                    <td class="leftTD">
                                        Sales Organization
                                        <asp:Label ID="lableddlSalesOrginization" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlSalesOrginization" runat="server" AutoPostBack="true" AppendDataBoundItems="false"
                                            TabIndex="2" OnSelectedIndexChanged="ddlSalesOrginization_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlSalesOrginization" runat="server" ControlToValidate="ddlSalesOrginization"
                                            ValidationGroup="Sales" ErrorMessage="Sales Organization cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Distribution Channel
                                        <asp:Label ID="lableddlDistributionChannel" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlDistributionChannel" runat="server" AppendDataBoundItems="false"
                                            TabIndex="3">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlDistributionChannel" runat="server" ControlToValidate="ddlDistributionChannel"
                                            ValidationGroup="Sales" ErrorMessage="Distribution Channel cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Sales unit
                                        <asp:Label ID="lableddlSalesUnit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlSalesUnit" runat="server" AppendDataBoundItems="false" 
                                            TabIndex="4" OnSelectedIndexChanged="ddlSalesUnit_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlSalesUnit" runat="server" ControlToValidate="ddlSalesUnit"
                                            ValidationGroup="Sales" ErrorMessage="Sales unit cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales unit cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Vari Sales Unit Not Allowed
                                        <asp:Label ID="lablechkVariSalesUnitNotAllowed" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkVariSalesUnitNotAllowed" runat="server" TabIndex="5" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Cash discount indicator
                                        <asp:Label ID="lablechkCashDiscountIndi" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkCashDiscountIndi" runat="server" TabIndex="6" />
                                    </td>
                                    <td class="leftTD">
                                        Delivering Plant
                                        <asp:Label ID="lableddlDeliveringPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlDeliveringPlant" runat="server" AppendDataBoundItems="false"
                                            TabIndex="7">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlDeliveringPlant" runat="server" ControlToValidate="ddlDeliveringPlant"
                                            ValidationGroup="Salesch" ErrorMessage="Delivering Plant cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Delivering Plant cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Distribution-chain-specific material status
                                        <asp:Label ID="labletxtDistributionCSpecMStatus" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtDistributionCSpecMStatus" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="8" MaxLength="2" />
                                        <asp:RequiredFieldValidator ID="reqtxtDistributionCSpecMStatus" runat="server" ControlToValidate="txtDistributionCSpecMStatus"
                                            ValidationGroup="Salesch" ErrorMessage="Distribution-chain-specific material status cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution-chain-specific material status cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Material status for sales valid from this date
                                        <asp:Label ID="labletxtMaterialSSaleValid" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMaterialSSaleValid" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="9" />
                                        <act:CalendarExtender ID="caltxtMaterialSSaleValid" runat="server" TargetControlID="txtMaterialSSaleValid"
                                            Format="dd/MM/yyyy" />
                                        <asp:RequiredFieldValidator ID="reqtxtMaterialSSaleValid" runat="server" ControlToValidate="txtMaterialSSaleValid"
                                            ValidationGroup="Salesch" ErrorMessage="Material status for sales valid from this date cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material status for sales valid from this date cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="revDTTest" runat="server" ControlToValidate="txtMaterialSSaleValid"
                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                            ValidationGroup="Sales" Display="none" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="leftTD">
                                        Min. make-to-order qty.
                                        <asp:Label ID="labletxtMinMOrderQuantity" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMinMOrderQuantity" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="10" MaxLength="13" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtMinMOrderQuantity" runat="server" ControlToValidate="txtMinMOrderQuantity"
                                            ValidationGroup="Salesch" ErrorMessage="Min. make-to-order qty. cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Min. make-to-order qty. cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Min. delivery quantity for delivery note processing
                                        <asp:Label ID="labletxtMinDeliveryQuantity" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMinDeliveryQuantity" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="11" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtMinDeliveryQuantity" runat="server" ControlToValidate="txtMinDeliveryQuantity"
                                            ValidationGroup="Sales" ErrorMessage="Min. delivery quantity for delivery note processing cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Min. delivery quantity for delivery note processing cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Min. order qty. in base unit of measure
                                        <asp:Label ID="labletxtMinOrderQBaseUnitM" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMinOrderQBaseUnitM" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="12" MaxLength="13" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtMinOrderQBaseUnitM" runat="server" ControlToValidate="txtMinOrderQBaseUnitM"
                                            ValidationGroup="Sales" ErrorMessage="Min. order qty. in base unit of measure cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Min. order qty. in base unit of measure cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Delivery unit
                                        <asp:Label ID="labletxtDeliveryUnit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtDeliveryUnit" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="13" MaxLength="13" onkeypress="return IsNumber();"/>
                                        <asp:RequiredFieldValidator ID="reqtxtDeliveryUnit" runat="server" ControlToValidate="txtDeliveryUnit"
                                            ValidationGroup="Sales" ErrorMessage="Delivering Plant cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Delivering Plant cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Unit of measure of delivery unit
                                        <asp:Label ID="lableddlUnitMeasureDelivery" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlUnitMeasureDelivery" runat="server" AppendDataBoundItems="false"
                                            TabIndex="14">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlUnitMeasureDelivery" runat="server" ControlToValidate="ddlUnitMeasureDelivery"
                                            ValidationGroup="Sales" ErrorMessage="Unit of measure of delivery unit cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Unit of measure of delivery unit cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Sales Text
                                        <asp:Label ID="labletxtSalesText" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <%--onfocus="return DeliveringPlantOnFocus();" onchange="return DeliveringPlantTextChangeEvent();"--%>
                                        <asp:TextBox ID="txtSalesText" runat="server" CssClass="textarea" Columns="120" Rows="3"
                                            TabIndex="15" TextMode="MultiLine" />
                                        <asp:RequiredFieldValidator ID="reqtxtSalesText" runat="server" ControlToValidate="txtSalesText"
                                            ValidationGroup="Salesch" ErrorMessage="Sales Text cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Text cannot be blank.' />" />
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
                                        <asp:Button ID="btnPrevious" runat="server" ValidationGroup="Sales" Text="Back" TabIndex="16"
                                            UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="Sales" Text="Save" CssClass="button"
                                            TabIndex="17" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnNext" runat="server" ValidationGroup="Sales" Text="Save & Next"
                                            TabIndex="18" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Sales" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblSalesId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="15" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
