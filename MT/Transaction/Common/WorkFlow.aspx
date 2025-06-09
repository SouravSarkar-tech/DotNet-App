<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkFlow.aspx.cs" Inherits="Transaction_Common_WorkFlow" %>

<%@ Register Src="~/Transaction/UserControl/ucWorkFlow.ascx" TagPrefix="uc" TagName="ucWorkFlow" %>
<%@ Register Src="~/Transaction/UserControl/ucRequestDtl.ascx" TagPrefix="uc" TagName="ucRequestDtl" %>
<%@ Register Src="~/Transaction/UserControl/ucSAPIntegrationLog.ascx" TagPrefix="uc"
    TagName="ucSAPIntegrationLog" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WorkFlow History</title>
    <link href="../../stylesheet/main.css" rel="stylesheet" type="text/css" />
    <link href="../../stylesheet/TabStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <act:ToolkitScriptManager ID="sm" runat="server">
    </act:ToolkitScriptManager>
    <div>
        <uc:ucRequestDtl ID="ucRequestDtl1" runat="server" />
    </div>
    <div>
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
                    <div style="overflow:auto;height:500px" >
                        <asp:UpdatePanel ID="updSAPIntegrationLog" runat="server">
                            <ContentTemplate>
                                <uc:ucSAPIntegrationLog ID="SAPIntegrationLog" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </act:TabPanel>
        </act:TabContainer>
    </div>
    </form>
</body>
</html>
