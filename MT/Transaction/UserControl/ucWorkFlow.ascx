<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucWorkFlow.ascx.cs" Inherits="Transaction_UserControl_ucWorkFlow" %>
<div>
    <table>
        <tr>
            <asp:Repeater ID="rptWorkflow" runat="server">
                <ItemTemplate>
                    <td align="center" >
                        <asp:Label ID="lblWorkflow" Font-Italic="true" runat="server" width="190px"></asp:Label>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;<asp:Image ID="imgArrow" runat="server" src="../../images/Arrow Green.jpg"
                            alt="" Width="20px" Height="20px" />&nbsp;&nbsp;&nbsp;
                    </td>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
    </table>
</div>
<br />
<div>
    <asp:GridView ID="grdWorkFlowHistory" runat="server" AutoGenerateColumns="false"
        Width="100%" BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both">
        <RowStyle CssClass="light-gray" />
        <HeaderStyle CssClass="gridHeader" />
        <AlternatingRowStyle CssClass="gridRowStyle" />
        <PagerStyle CssClass="PagerStyle" />
        <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Department" DataField="Department_Name" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField HeaderText="Status" DataField="Approval" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField HeaderText="All Approvers" DataField="Approvers" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField HeaderText="Remarks" DataField="Remark" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField HeaderText="Actioned By" DataField="Actioned_By" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField HeaderText="Actioned On" DataField="Actioned_On" HeaderStyle-HorizontalAlign="Left" />
        </Columns>
    </asp:GridView>
</div>
