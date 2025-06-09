<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="PendingReqList.aspx.cs" Inherits="Webpages_PendingReqList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upmaterial" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="trHeading" align="center" colspan="2">
                         <asp:Label ID="lblHeader" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="tdSpace" colspan="2">
                    </td>
                </tr>
                <tr id="trDDL" runat="server">
                    <td style="width: 20%" class="leftTD">
                        <asp:Label ID="lblType" runat="server" />
                    </td>
                    <td class="rigthTD">
                        <asp:DropDownList ID="ddlMaterialModule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialModule_SelectedIndexChanged"
                            ToolTip="Select Material Type">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdSpace" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="grdMaterial" runat="server" AutoGenerateColumns="false" Width="100%"
                            BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                            GridLines="Both" Visible="true">
                            <RowStyle CssClass="light-gray" />
                            <HeaderStyle CssClass="gridHeader" />
                            <AlternatingRowStyle CssClass="gridRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <Columns>
                                <asp:BoundField HeaderText="Request No." DataField="Request_No" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Material Number" DataField="Material_Number" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Material Type" DataField="Material_Type" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Created On" DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Pending For" DataField="Pending_For" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="grdCust" runat="server" AutoGenerateColumns="false" Width="100%"
                            BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                            GridLines="Both" Visible="false">
                            <RowStyle CssClass="light-gray" />
                            <HeaderStyle CssClass="gridHeader" />
                            <AlternatingRowStyle CssClass="gridRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <Columns>
                                <asp:BoundField HeaderText="Request No." DataField="Request_No" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Account Group" DataField="Account_Group" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Name1" DataField="Name1" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Customer Code" DataField="Customer_Code" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Created On" DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Pending For" DataField="Pending_For" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="grdVendor" runat="server" AutoGenerateColumns="false" Width="100%"
                            BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                            GridLines="Both" Visible="false">
                            <RowStyle CssClass="light-gray" />
                            <HeaderStyle CssClass="gridHeader" />
                            <AlternatingRowStyle CssClass="gridRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <Columns>
                                <asp:BoundField HeaderText="Request No." DataField="Request_No" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Name" DataField="Name1" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Customer Name" DataField="Customer_Number" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Created On" DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Pending For" DataField="Pending_For" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="grdBom" runat="server" AutoGenerateColumns="false" Width="100%"
                            BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                            GridLines="Both" Visible="false">
                            <RowStyle CssClass="light-gray" />
                            <HeaderStyle CssClass="gridHeader" />
                            <AlternatingRowStyle CssClass="gridRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <Columns>
                                <asp:BoundField HeaderText="Request No." DataField="Request_No" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Material" DataField="Material" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Created On" DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Pending For" DataField="Pending_For" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="grdRecipe" runat="server" AutoGenerateColumns="false" Width="100%"
                            BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                            GridLines="Both" Visible="false">
                            <RowStyle CssClass="light-gray" />
                            <HeaderStyle CssClass="gridHeader" />
                            <AlternatingRowStyle CssClass="gridRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <Columns>
                                <asp:BoundField HeaderText="Request No." DataField="Request_No" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Material" DataField="Material" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Created On" DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Pending For" DataField="Pending_For" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <br />
                        <asp:Button id="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="button" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlMaterialModule" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="updateProgressMaster" runat="server" DisplayAfter="5">
        <ProgressTemplate>
            <div id="light" class="loading">
                <img src="../images/ajax-loader-proccessing.gif" alt="Loading" height="30px" />
            </div>
            <div class="transparent_overlay">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
