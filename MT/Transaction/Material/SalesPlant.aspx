<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="SalesPlant.aspx.cs" Inherits="Transaction_SalesPlant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialSales" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlData" runat="server">
                <table border="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Sales Plant
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
                                            DataKeyNames="Mat_Sales3_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                            GridLines="Both">
                                            <RowStyle CssClass="light-gray" />
                                            <HeaderStyle CssClass="gridHeader" />
                                            <AlternatingRowStyle CssClass="gridRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Plant" DataField="Plant" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("Mat_Sales3_Id") %>' />
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
                                            ValidationGroup="SalesPlant" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                    </td>
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
                                            ValidationGroup="SalesPlant" ErrorMessage="Sales Organization cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
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
                                            ValidationGroup="SalesPlant" ErrorMessage="Distribution Channel cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Availability Check
                                        <asp:Label ID="lableddlAvailabilityCheck" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlAvailabilityCheck" runat="server" AppendDataBoundItems="false"
                                            TabIndex="4">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlAvailabilityCheck" runat="server" ControlToValidate="ddlAvailabilityCheck"
                                            ValidationGroup="SalesPlant" ErrorMessage="Availability Check cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Availability Check cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Batch Mgmt
                                        <asp:Label ID="lablechkBatchMgmt" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkBatchMgmt" runat="server" Text=" Batch Mgmt" TabIndex="5" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Transportation Group
                                        <asp:Label ID="lableddlTransportationGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlTransportationGroup" runat="server" AppendDataBoundItems="false"
                                            TabIndex="6">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlTransportationGroup" runat="server" ControlToValidate="ddlTransportationGroup"
                                            ValidationGroup="SalesPlant" ErrorMessage="Transportation Group cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Transportation Group cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Loading Group
                                        <asp:Label ID="lableddlLoadingGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlLoadingGroup" runat="server" AppendDataBoundItems="false"
                                            TabIndex="7">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlLoadingGroup" runat="server" ControlToValidate="ddlLoadingGroup"
                                            ValidationGroup="SalesPlant" ErrorMessage="Loading Group cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Loading Group cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Base quantity for capacity planning in shipping
                                        <asp:Label ID="labletxtBaseQuantCapacity" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtBaseQuantCapacity" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="8" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtBaseQuantCapacity" runat="server" ControlToValidate="txtBaseQuantCapacity"
                                            ValidationGroup="SalesPlant" ErrorMessage="Base quantity for capacity planning in shipping cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Base quantity for capacity planning in shipping cannot be blank.' />" />
                                    </td>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Shipping processing time
                                        <asp:Label ID="labletxtShippingProcessingTime" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtShippingProcessingTime" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="9" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtShippingProcessingTime" runat="server" ControlToValidate="txtShippingProcessingTime"
                                            ValidationGroup="SalesPlant" ErrorMessage="Shipping processing time cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Shipping processing time cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Shipping setup time
                                        <asp:Label ID="labletxtShippingSTime" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtShippingSTime" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="10" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtShippingSTime" runat="server" ControlToValidate="txtShippingSTime"
                                            ValidationGroup="SalesPlant" ErrorMessage="Shipping setup time cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Shipping setup time cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Profit Center
                                        <asp:Label ID="lableddlProfitCenter" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlProfitCenter" runat="server" AppendDataBoundItems="false"
                                            TabIndex="11">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlProfitCenter" runat="server" ControlToValidate="ddlProfitCenter"
                                            ValidationGroup="SalesPlant" ErrorMessage="Profit Center cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Profit Center cannot be blank.' />" />
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
                                        <asp:Button ID="btnPrevious" runat="server" ValidationGroup="SalesPlant" Text="Back"
                                            TabIndex="12" UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="SalesPlant" Text="Save"
                                            TabIndex="13" UseSubmitBehavior="true" CssClass="button" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnNext" runat="server" ValidationGroup="SalesPlant" Text="Save & Next"
                                            TabIndex="14" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="SalesPlant" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblSalesPlantId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="17" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
