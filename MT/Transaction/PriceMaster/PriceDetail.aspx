<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/PriceMaster/PriceMasterPage.master"
    AutoEventWireup="true" CodeFile="PriceDetail.aspx.cs" Inherits="Transaction_PriceMaster_PriceDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                                <td class="tdSpace" colspan="4">
                                    <asp:Panel ID="pnlGrid" runat="server">
                                        <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                            BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both">
                                            <RowStyle CssClass="light-gray" />
                                            <HeaderStyle CssClass="gridHeader" />
                                            <AlternatingRowStyle CssClass="gridRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPriceDetailId" runat="server" Text='<%# Eval("Price_Detail_Id") %>'></asp:Label>
                                                        <asp:Label ID="lblPriceHeaderId" runat="server" Text='<%# Eval("Price_Header_Id") %>'></asp:Label>
                                                        <asp:Label ID="lblPlantRegionId" runat="server" Text='<%# Eval("Plant_Region_Id") %>'></asp:Label>
                                                        <asp:Label ID="lblRegionId" runat="server" Text='<%# Eval("Region_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Delivery Plant Region" DataField="Plant_Region" />
                                                <asp:BoundField HeaderText="Region" DataField="Region" />
                                                <asp:BoundField HeaderText="MRP" DataField="MRP" />
                                                
                                                <asp:TemplateField>
                                                <HeaderTemplate>Trade Price</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTradePrice" runat="server" Text='<%# Eval("Trade_Price") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqtxtTradePrice" runat="server" ControlToValidate="txtTradePrice"
                                                            ValidationGroup="PriceHeader" ErrorMessage="Trade price cannot be blank."
                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Trade price cannot be blank.' />" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Excise Duty" DataField="Excise_Duty" />
                                                <asp:BoundField HeaderText="Education Cess" DataField="Education_Cess" />
                                                <asp:BoundField HeaderText="Sec Higher Edu Cess" DataField="Sec_High_Edu_Cess" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
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
</asp:Content>
