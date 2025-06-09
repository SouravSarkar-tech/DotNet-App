<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/HomeMasterPage.master"
    AutoEventWireup="true" CodeFile="WorkFlowSearch.aspx.cs" Inherits="Transaction_Common_WorkFlowSearch" %>

<%@ Register Src="~/Transaction/UserControl/ucWorkFlow.ascx" TagPrefix="uc" TagName="ucWorkFlow" %>
<%@ Register Src="~/Transaction/UserControl/ucRequestDtl.ascx" TagPrefix="uc" TagName="ucRequestDtl" %>
<%@ Register Src="~/Transaction/UserControl/ucSAPIntegrationLog.ascx" TagPrefix="uc" TagName="ucSAPIntegrationLog" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">
                    Request Search
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="1" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" width="20%">
                                Request No. :
                                
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtRequestNo" runat="server" CssClass="textbox" MaxLength="20"
                                    TabIndex="1" Width="220" />
                            </td>
                            <td class="tdspace" colspan="2">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" 
                                    onclick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div align = "center" style="border-bottom:1px solid Black;">
    :: Request Details ::
    </div>
    <div>
        <uc:ucRequestDtl ID="ucRequestDtl1" runat="server" />
    </div>
    <div align="left">
        <act:TabContainer ID="tabContainer" runat="server" ActiveTabIndex="0" OnDemand="false">
            <act:TabPanel ID="TabWorkFlow" runat="server" HeaderText="WorkFlow Details" OnDemandMode="Once">
                <ContentTemplate>
                    <asp:UpdatePanel ID="updWorkFlow" runat="server">
                        <ContentTemplate>
                            <uc:ucWorkFlow ID="WorkFlow" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </act:TabPanel>
            <act:TabPanel ID="TabSAPIntegration" runat="server" HeaderText="SAP Integration History"
                OnDemandMode="Once">
                <ContentTemplate>
                    <asp:UpdatePanel ID="updSAPIntegrationLog" runat="server">
                        <ContentTemplate>
                            <uc:ucSAPIntegrationLog ID="SAPIntegrationLog" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </act:TabPanel>
        </act:TabContainer>
    </div>
</asp:Content>
