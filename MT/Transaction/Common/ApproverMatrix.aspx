<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/MainMaster.master"
    AutoEventWireup="true" CodeFile="ApproverMatrix.aspx.cs" Inherits="Transaction_Common_ApproverMatrix" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="left">
        <asp:HyperLink ID="hlApproverList" runat="server" Text="Download Plantwise Departmentwise Approver Master" Target="_blank"
            NavigateUrl="~/Transaction/Material/UploadFormat/Material Approvers.xlsx"></asp:HyperLink> <br />
        <asp:HyperLink ID="hlFieldAuthority" runat="server" Text="Download Departmentwise Field Master" Target="_blank"
            NavigateUrl="~/Transaction/Material/UploadFormat/Approving Authority For Fields.xlsx"></asp:HyperLink>
    </div>
</asp:Content>
