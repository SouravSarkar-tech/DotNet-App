<%@ Page Title="Test Page" Language="C#" MasterPageFile="~/shared/Site.master" AutoEventWireup="true" CodeFile="TestPage.aspx.cs" Inherits="Pages_TestPage" %>

<%@ Register Src="../UserControl/ucAccounting1.ascx" TagName="ucAccounting1" TagPrefix="uc1" %>

<%@ Register Src="../UserControl/ucAccounting2.ascx" TagName="ucAccounting2" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <fieldset>
        <legend>Test Page</legend>
        <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">

                            <uc1:ucAccounting1 ID="ucAccounting11" runat="server" Visible="false" />

                            <uc2:ucAccounting2 ID="ucAccounting21" runat="server" Visible="false" />

                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>



</asp:Content>

