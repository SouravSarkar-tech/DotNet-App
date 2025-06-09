<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucExcelDownload2.ascx.cs" Inherits="Transaction_UserControl_ucExcelDownload2" %>

<div>
    <asp:LinkButton ID="lnkExcelDwld" runat="server" onclick="lnkExcelDwld_Click" Text="DownLoad Excel"></asp:LinkButton>
    <asp:HiddenField ID="hdnFieldActionType" runat="server" />
</div>