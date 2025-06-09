<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSAPIntegrationLog.ascx.cs"
    Inherits="Transaction_UserControl_ucSAPIntegrationLog" %>
<asp:Repeater ID="rptSAPIntegration" runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <fieldset>
            <legend class="DashLegend">
                <asp:Label ID="lblMasterHeaderId" runat="server" Text='<%# Eval("Master_Header_Id") %>'
                    Visible="false"></asp:Label>
                <asp:Label ID="lblRequest_No" runat="server" Text='<%# Eval("Request_No") %>'></asp:Label>
            </legend>
            <asp:GridView ID="grdSearch" runat="server" AutoGenerateColumns="false" Width="100%"
                BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both">
                <RowStyle CssClass="light-gray" />
                <HeaderStyle CssClass="gridHeader" />
                <AlternatingRowStyle CssClass="gridRowStyle" />
                <PagerStyle CssClass="PagerStyle" />
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Uploaded on" DataField="Created_Date" HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Uploaded By" DataField="Created_By" HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Message" DataField="msg" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-Width="70%" ItemStyle-HorizontalAlign="Left" />
                </Columns>
            </asp:GridView>
        </fieldset>
    </ItemTemplate>
</asp:Repeater>
