<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/MasterPage.master"
    AutoEventWireup="true" CodeFile="VMStatusReport.aspx.cs" Inherits="Reports_VMTransReport" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblUserId" runat="server" Text="0" Visible="false"></asp:Label>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="trHeading" align="center" colspan="4">
                MWT Summary Report
            </td>
        </tr>
        <tr>
            <td colspan="4" class="tdSpace">
            </td>
        </tr>
        <tr>
            <td class="leftTD" align="left" style="width: 25%">
                Master Type
            </td>
            <td class="rigthTD" align="left" style="width: 25%">
                <asp:DropDownList runat="server" ID="ddlMasterType">
                    <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Bank Master" Value="A"></asp:ListItem>
                    <asp:ListItem Text="Vendor Master" Value="V"></asp:ListItem>
                    <asp:ListItem Text="Customer Master" Value="C"></asp:ListItem>
                    <asp:ListItem Text="Material Master" Value="M"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tdSpace">
            </td>
        </tr>
        <tr>
            <td colspan="4" class="tdSpace">
            </td>
        </tr>
        <tr>
            <td class="leftTD" align="left" style="width: 25%">
                From Date
            </td>
            <td class="rigthTD" align="left" style="width: 25%">
                <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" />
                <act:CalendarExtender ID="CaltxtFromDate" runat="server" Format="yyyy-MM-dd" TargetControlID="txtFromDate" />
            </td>
            <td class="leftTD" align="left" style="width: 25%">
                To Date
            </td>
            <td class="rigthTD" align="left" style="width: 25%">
                <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" />
                <act:CalendarExtender ID="CaltxtToDate" runat="server" Format="yyyy-MM-dd" TargetControlID="txtToDate" />
            </td>
        </tr>
        <tr>
            <td class="centerTD" colspan="4">
                &nbsp;
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                    InteractiveDeviceInfos="(Collection)" Width="100%" Height="100%" WaitMessageFont-Names="Verdana"
                    WaitMessageFont-Size="14pt">
                    <LocalReport ReportPath="Reporting\VendorMasterStatusRpt.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="MWTVendorMaster" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetData" TypeName="MWTVendorMasterDSTableAdapters.pr_rpt_VMStatusReportTableAdapter">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtFromDate" Name="StartDate" PropertyName="Text"
                            Type="DateTime" />
                        <asp:ControlParameter ControlID="txtToDate" Name="EndDate" PropertyName="Text" Type="DateTime" />
                        <asp:ControlParameter ControlID="lblUserId" DefaultValue="0" Name="UserId" PropertyName="Text"
                            Type="Decimal" />
                        <asp:ControlParameter ControlID="ddlMasterType" Name="ModuleType" PropertyName="SelectedValue"
                            Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
