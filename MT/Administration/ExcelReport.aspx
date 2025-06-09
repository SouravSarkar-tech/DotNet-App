<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="ExcelReport.aspx.cs" Inherits="Administration_ExcelReport" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<%--<%@ Register Src="~/Transaction/UserControl/ucExcelDownloadlU.ascx" TagPrefix="uc" TagName="ExcelDownload" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlSearch" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="4">Reports
                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>
            <tr>
                <td class="leftTD" align="left" style="width: 25%">Report Type
                </td>
                <td class="rigthTD" align="left">
                    <asp:DropDownList ID="ddlReportType" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="Admin Activites Report" Value="A" />
                        <asp:ListItem Text="User Report" Value="U" />
                        <asp:ListItem Text="Last Tranzaction Report" Value="L" />
                        <asp:ListItem Text="Admin Report" Value="R" />
                        <asp:ListItem Text="SLA Report" Value="S" />
                    </asp:DropDownList>
                </td>

                <td class="leftTD" align="left" style="width: 25%">Name
                </td>
                <td class="rigthTD" align="left">
                    <asp:TextBox ID="txtUserNameSearch" runat="server" CssClass="textbox" />
                </td>
                <%--<td colspan="2" class="tdSpace"></td>--%>
            </tr>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>
            <tr>
                <td class="leftTD" align="left" style="width: 25%">From Date
                </td>
                <td class="rigthTD" align="left">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" />
                    <act:CalendarExtender ID="CaltxtFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFromDate"
                        ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                        Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                        ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>

                </td>
                <td class="leftTD" align="left" style="width: 25%">To Date
                </td>
                <td class="rigthTD" align="left">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" />
                    <act:CalendarExtender ID="CaltxtToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtToDate"
                        ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                        Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                        ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>

                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>

            <tr>
                <td colspan="4" class="tdSpace" align="center">
                     <asp:LinkButton ID="lnkExcelDwld" runat="server" Text="Download Report" OnClick="lnkExcelDwld_Click"  CssClass="button"/>             
                </td>
            </tr>

              <%-- <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>    
            <tr>
                <td class="centerTD" colspan="4">
                    <table>
                        <tr>
                            <td>User Report</td>
                            <td>
                                <uc:ExcelDownload ID="ExcelDownload1" runat="server" ActionType="U" Visible="true" />

                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdSpace"></td>
                        </tr>
                        <tr>
                            <td>Admin Report</td>
                            <td>

                                <uc:ExcelDownload ID="ExcelDownload2" runat="server" ActionType="A" Visible="true" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>--%>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

