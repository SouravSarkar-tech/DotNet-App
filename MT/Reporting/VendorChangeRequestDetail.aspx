<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="VendorChangeRequestDetail.aspx.cs" Inherits="Reports_VendorChangeRequestDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlSearch" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="4">
                    Vendor Master
                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdSpace">
                </td>
            </tr>
            <tr>
                <td class="leftTD" align="left" style="width: 25%">
                    Request No
                </td>
                <td class="rigthTD" align="left" colspan="3">
                    <asp:TextBox ID="txtRequestNo" runat="server" CssClass="textbox" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdSpace">
                </td>
            </tr>
            <tr>
                <td class="centerTD" colspan="4">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdSpace">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    <script type="text/javascript">
                        function CheckOtherIsCheckedByGVID(spanChk) {

                            var CurrentRdbID = spanChk.id;
                            var Chk = spanChk;
                            Parent = document.getElementById("<%=grdSearch.ClientID%>");
                            var items = Parent.getElementsByTagName('input');
                            for (i = 0; i < items.length; i++) {
                                if (items[i].id != CurrentRdbID && items[i].type == "radio") {
                                    if (items[i].checked) {
                                        items[i].checked = false;
                                    }
                                }
                            }
                        }
                    </script>
                    <asp:GridView ID="grdSearch" runat="server" AutoGenerateColumns="false" Width="100%"
                        BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                        GridLines="Both">
                        <RowStyle CssClass="light-gray" />
                        <HeaderStyle CssClass="gridHeader" />
                        <AlternatingRowStyle CssClass="gridRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdoSelection" runat="server" GroupName="selection" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrimaryID" runat="server" Text='<%# Eval("Master_Header_Id") %>'></asp:Label>
                                    <asp:Label ID="lblModuleId" runat="server" Text='<%# Eval("Module_Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Request No." DataField="Request_No" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField HeaderText="Customer Code" DataField="Customer_Code" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField HeaderText="Account Group" DataField="Vendor_Acc_Grp_Code" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="4">
                    <br />
                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="button" OnClientClick="return Validate();"
                        OnClick="btnView_Click" />
                    <script type="text/javascript">
                        function Validate() {
                            var gv = document.getElementById("<%=grdSearch.ClientID%>");
                            var rbs = gv.getElementsByTagName("input");
                            var flag = 0;
                            for (var i = 0; i < rbs.length; i++) {

                                if (rbs[i].type == "radio") {
                                    if (rbs[i].checked) {
                                        flag = 1;
                                        break;
                                    }
                                }
                            }
                            if (flag == 0) {
                                alert("Kindly Select A Record");
                                return false;
                            }
                        }
                                              
                    </script>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlView" runat="server" Visible="false">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="4">
                    Vendor Master
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div style="width: 1100px; overflow: auto">
                        <asp:GridView ID="grdRequest" runat="server" AutoGenerateColumns="true" Width="100%"
                            CellPadding="10" BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both"
                            HeaderStyle-Wrap="false" RowStyle-Wrap="false">
                            <RowStyle CssClass="light-gray" />
                            <HeaderStyle CssClass="gridHeader" />
                            <AlternatingRowStyle CssClass="gridRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <br />
                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="button" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:UpdateProgress ID="updateProgressMaster" runat="server" DisplayAfter="5">
        <ProgressTemplate>
            <div id="light" class="loading">
                <img src="../images/ajax-loader-proccessing.gif" alt="Loading" height="30px" />
            </div>
            <div class="transparent_overlay">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="next" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblPk" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
</asp:Content>
