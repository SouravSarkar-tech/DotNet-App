<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucExcelDownloadl.ascx.cs" Inherits="Transaction_UserControl_ucExcelDownloadl" %>

<div>
    <asp:LinkButton ID="lnkExcelDwld" runat="server" onclick="lnkExcelDwld_Click" Text="DownLoad Excel"></asp:LinkButton>
    <asp:HiddenField ID="hdnFieldActionType" runat="server" />
</div>