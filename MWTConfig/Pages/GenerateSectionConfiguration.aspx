<%@ Page Title="GSC" Language="C#" MasterPageFile="~/shared/Site.master" AutoEventWireup="true" CodeFile="GenerateSectionConfiguration.aspx.cs" Inherits="Pages_GenerateSectionConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <fieldset>
        <legend>Generate Section Configuration</legend>
        <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>

