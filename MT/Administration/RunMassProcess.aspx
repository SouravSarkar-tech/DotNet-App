<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/MasterPage.master"
    CodeFile="RunMassProcess.aspx.cs" Inherits="Administration_RunMassProcess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upMsg" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="trHeading" align="center" colspan="2">Run Mass-Process
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" colspan="2">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="tdSpace" colspan="2"></td>
                            </tr>
                            <tr>
                                <td class="centerTD" colspan="2">
                                    <asp:Button ID="btnStartProgram" runat="server" Text="Start" CssClass="button"
                                        OnClick="btnStartProgram_Click" />
                                    &nbsp;
                                        <asp:Button ID="btnStopProgram" runat="server" Text="Stop" CssClass="button"
                                            OnClick="btnStopProgram_Click" Visible="false"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
</asp:Content>

