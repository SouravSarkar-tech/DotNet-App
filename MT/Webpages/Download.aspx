<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="Download.aspx.cs" Inherits="Webpages_Download" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function RefreshPage() {
            setTimeout("location.reload(true);", 1000);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <div style="width: 100%; text-align: center; color: Black; background: #FAB3A5; font-weight: bold;
                    border: solid 1px red;">
                    <asp:Label ID="lblMsg" runat="server" />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlContainer" runat="server" Visible="true">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td valign="top" class="trHeading" colspan="4">
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="trHeading" align="center">
                                        Download File
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br />
                            <table border="0" cellpadding="2" cellspacing="1" width="50%">
                                <tr>
                                    <td style="width: 10%" class="leftTD">
                                        Type
                                    </td>
                                    <td colspan="3" class="rigthTD">
                                        <asp:DropDownList ID="ddlMaterialModule" runat="server" ToolTip="Select Type">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        File Status
                                    </td>
                                    <td colspan="3" class="rigthTD">
                                        <asp:DropDownList ID="ddlFileStatus" runat="server">
                                            <asp:ListItem Text="New Files" Value="A" />
                                            <%--<asp:ListItem Text="Downloaded Files" Value="D" />--%>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%" class="leftTD">
                                        From Date
                                    </td>
                                    <td style="width: 30%" class="rigthTD">
                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="textbox" Width="100px" />
                                        <ajax:CalendarExtender ID="caltxtMaterialSSaleValid" runat="server" TargetControlID="txtDateFrom"
                                            Format="dd/MM/yyyy" />
                                        <asp:RegularExpressionValidator ID="revDTTest" runat="server" ControlToValidate="txtDateFrom"
                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                            Text="<img src='../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                            ValidationGroup="search" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 20%" class="leftTD">
                                        To Date
                                    </td>
                                    <td style="width: 30%" class="rigthTD">
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="100px" />
                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                            Format="dd/MM/yyyy" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtToDate"
                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                            Text="<img src='../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                            ValidationGroup="search" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="centerTD">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" ValidationGroup="search"
                                            OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br />
                            <asp:Panel ID="pnlNewFiles" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" width="50%">
                                    <tr>
                                        <td class="blueStrip" align="left">
                                            <b>&nbsp;New Files</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdNewFiles" runat="server" AutoGenerateColumns="false" Width="100%"
                                                BorderColor="#F1F1F1" EmptyDataText="No New File Available" ShowFooter="false"
                                                ShowHeader="true" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdNewFiles_PageIndexChanging">
                                                <RowStyle CssClass="light-gray" />
                                                <HeaderStyle BackColor="#EDF5FF" />
                                                <AlternatingRowStyle CssClass="wht-clr-1" />
                                                <PagerStyle CssClass="PagerStyle" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequestID" runat="server" Text='<%# Eval("Master_Header_Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkCheckHeader" runat="server" onclick="return CheckAllCheckBox(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkCheck" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderText="Request No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("Request_No") %>' Font-Bold="true"
                                                                Font-Size="12px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Code") %>' Font-Bold="true"
                                                                Font-Size="12px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <asp:Button ID="btnDownloadMultipleFiles" runat="server" Text="Download Selected Files"
                                                OnClick="btnDownloadMultipleFiles_OnClick" OnClientClick="return RefreshPage();"
                                                CssClass="button" Width="200px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlDownloadedFile" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" width="50%">
                                    <tr>
                                        <td class="blueStrip" align="left">
                                            <b>&nbsp;Downlaoded Files</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdDownloadedFiles" runat="server" AutoGenerateColumns="false"
                                                Width="100%" BorderColor="#F1F1F1" EmptyDataText="No Downloaded File Available"
                                                ShowFooter="false" ShowHeader="true" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdDownloadedFiles_PageIndexChanging">
                                                <RowStyle CssClass="light-gray" />
                                                <HeaderStyle BackColor="#EDF5FF" />
                                                <AlternatingRowStyle CssClass="wht-clr-1" />
                                                <PagerStyle CssClass="PagerStyle" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequestID" runat="server" Text='<%# Eval("Master_Header_Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkCheckHeader" runat="server" onclick="return CheckAllCheckBox2(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkCheck" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderText="Request No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("Request_No") %>' Font-Bold="true"
                                                                Font-Size="12px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Code") %>' Font-Bold="true"
                                                                Font-Size="12px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <asp:Button ID="btnDownloadMultipleFiles2" runat="server" Text="Download Selected Files"
                                                OnClick="btnDownloadMultipleFiles2_OnClick" CssClass="button" Width="200px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnDownloadMultipleFiles" />
            <asp:PostBackTrigger ControlID="btnDownloadMultipleFiles2" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
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
    <script type="text/javascript">
        function CheckAllCheckBox(chk) {
            var chkboxId = $(chk).attr('Id');
            $('#<%=grdNewFiles.ClientID %>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });
        }
        function CheckAllCheckBox2(chk) {
            var chkboxId = $(chk).attr('Id');
            $('#<%=grdDownloadedFiles.ClientID %>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });
        }
    </script>
</asp:Content>
