<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/HomeMasterPage.master" AutoEventWireup="true" CodeFile="ProsolReport.aspx.cs" Inherits="Transaction_Common_ProsolReport" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlSearch" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="4">Prosol Request Report
                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>
            <tr>
                <td class="leftTD" align="left" style="width: 25%">
                    <asp:Label ID="lblProsolid" runat="server" Text="Prosol ID :"></asp:Label>
                </td>
                <td class="rigthTD" align="left" style="width: 25%">
                    <asp:TextBox ID="txtProsolid" runat="server" CssClass="textbox" onkeypress="return IsNumber();"></asp:TextBox>
                </td>

                <td class="leftTD" align="left" style="width: 25%">
                    <asp:Label ID="lblMwtReqNum" runat="server" Text="MWT Request Number :"></asp:Label>
                </td>
                <td class="rigthTD" align="left" style="width: 25%">
                    <asp:TextBox ID="txtMwtReqNum" runat="server" CssClass="textbox" onkeypress="return IsNumber();"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>
            
            <tr>
                 <td class="leftTD" align="left" style="width: 25%">
                    <asp:Label ID="lbltxtFromdate" runat="server" Text="From Date :"></asp:Label>
                </td>
                <td class="leftTD" align="left" style="width: 25%">
                    <asp:TextBox ID="txtFromdate" runat="server" CssClass="textbox" />
                    <cc1:CalendarExtender ID="CaltxtFromdate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromdate" />
                    <asp:RegularExpressionValidator ID="regtxtFromdate" runat="server" ControlToValidate="txtFromdate"
                        ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                        Text="<img src='../../images/Help/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                        ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="reqtxtFromdate" runat="server" ForeColor="Red" ErrorMessage="From Date cannot be blank"
                        SetFocusOnError="true" Text="<img src='../../images/Help/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                        ControlToValidate="txtFromdate" ValidationGroup="prequest" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftTD" align="left" style="width: 25%">
                    <asp:Label ID="lbltxtToDate" runat="server" Text="To Date :"></asp:Label>
                </td>
                <td class="leftTD" align="left" style="width: 25%">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" />
                    <cc1:CalendarExtender ID="CaltxtToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate" />
                    <asp:RegularExpressionValidator ID="regtxtToDate" runat="server" ControlToValidate="txtToDate"
                        ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                        Text="<img src='../../images/Help/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                        ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="reqtxtToDate" runat="server" ForeColor="Red" ErrorMessage="To Date cannot be blank"
                        SetFocusOnError="true" Text="<img src='../../images/Help/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                        ControlToValidate="txtToDate" ValidationGroup="prequest" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </td>
               
            </tr>
            <tr>
                <td class="centerTD" colspan="4">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" 
                        ValidationGroup="prequest"  CssClass="button"/>
                </td>
            </tr>

            <tr>
                <td class="centerTD" colspan="4">
                   <%-- <div id="dvGrid" runat="server" style="overflow-x: auto; height: 400px;">--%>
                        <asp:GridView ID="grdReport" runat="server"  AutoGenerateColumns="false" Width="100%"
                        BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="20"
                        OnPageIndexChanging="grdReport_PageIndexChanging" GridLines="Both">
                        <RowStyle CssClass="light-gray" />
                        <HeaderStyle CssClass="gridHeader" />
                        <AlternatingRowStyle CssClass="gridRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                            <Columns>
                                  <asp:BoundField HeaderText="Prosol ID" DataField="sProsolID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                                  <asp:BoundField HeaderText="Request Created In MWT" DataField="dCreatedOn" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                                  <asp:BoundField HeaderText="MWT Request No." DataField="sMWTRequestNo" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                                  <asp:BoundField HeaderText="Material Code" DataField="sSAPMatNumber" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                                  <asp:BoundField HeaderText="Material Created In SAP" DataField="dMatCreatedOn" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                                  <asp:BoundField HeaderText="Material Updated In Prosol" DataField="dModifiedOn" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                                 <asp:BoundField HeaderText="Remarks" DataField="sProsolRemarks" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                               <%-- <asp:TemplateField HeaderText="Prosol ID" ItemStyle-CssClass="column_style_left"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                    <ItemStyle CssClass="column_style_left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblUniqProID" runat="server" Text='<%# Eval("sProsolID")%>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SAP DateTime" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="column_style_right"
                                    ItemStyle-Width="150px">
                                    <ItemStyle CssClass="column_style_right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSapDateTime" runat="server" Width="150px" Text='<%# Eval("dCreatedOn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MWT Request Number" ItemStyle-CssClass="column_style_right" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="130px">
                                    <ItemStyle CssClass="column_style_right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMwtRq" runat="server" Width="130px" Text='<%# Eval("sMWTRequestNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Number" ItemStyle-CssClass="column_style_right" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="130px">
                                    <ItemStyle CssClass="column_style_right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMatnr" runat="server" Width="130px" Text='<%# Eval("sSAPMatNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SAP MWT Timestamp" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="column_style_right"
                                    ItemStyle-Width="90px">
                                    <ItemStyle CssClass="column_style_right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblmwtdate" runat="server" Text='<%# Eval("dMatCreatedOn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coda DateTime" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="column_style_right"
                                    ItemStyle-Width="150px">
                                    <ItemStyle CssClass="column_style_right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodadate" runat="server" Width="150px" Text='<%# Eval("dModifiedOn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks" ItemStyle-CssClass="column_style_right"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px">
                                    <ItemStyle CssClass="column_style_right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("sProsolRemarks")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                           <%-- <EmptyDataRowStyle CssClass="empty-row" />
                            <HeaderStyle CssClass="GridviewScrollHeader" />
                            <RowStyle CssClass="GridviewScrollItem" />
                            <PagerStyle CssClass="GridviewScrollPager" />--%>
                        </asp:GridView>
                 <%--   </div>--%>
                </td>
            </tr>
        </table>

    </asp:Panel>


    <%-- <table>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div id="dvGrid" runat="server" style="overflow-x: auto; width: 1250px; height: 400px;">
                        
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>--%>
</asp:Content>

