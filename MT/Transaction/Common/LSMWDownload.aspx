<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/MainMaster.master"
    AutoEventWireup="true" CodeFile="LSMWDownload.aspx.cs" Inherits="Shared_Common_LSMWDownload" %>

<%@ Register Src="~/Transaction/UserControl/ucExcelDownloadl.ascx" TagPrefix="uc"
    TagName="ExcelDownload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="left" style="width: 98%">
        <uc:ExcelDownload ID="ExcelDownload1" runat="server" ActionType="" />
    </div>
</asp:Content>
