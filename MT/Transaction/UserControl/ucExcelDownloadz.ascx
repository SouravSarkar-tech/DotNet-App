<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucExcelDownloadz.ascx.cs" Inherits="Transaction_UserControl_ucExcelDownloadz" %>

<div>
    <asp:LinkButton ID="lnkExcelDwld" runat="server" onclick="lnkExcelDwld_Click" Text="DownLoad Excel"></asp:LinkButton>
    <asp:HiddenField ID="hdnFieldActionType" runat="server" />
</div>