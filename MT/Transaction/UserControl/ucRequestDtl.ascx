<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRequestDtl.ascx.cs"
    Inherits="Transaction_UserControl_ucRequestDtl" %>
<table border="0" cellpadding="0" cellspacing="1" width="100%">
    <tr>
        <td class="tdSpace" colspan="4">
        </td>
    </tr>
    <tr>
        <td class="leftTD" width="20%">
            Request No.
        </td>
        <td class="rigthTD" style="width: 30%">
            <asp:Label ID="lblRequestNo" runat="server"></asp:Label>
        </td>
        <td class="leftTD" width="20%">
            Master
        </td>
        <td class="rigthTD" style="width: 30%">
            <asp:Label ID="lblMasterType" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="tdSpace" colspan="4">
        </td>
    </tr>
    <tr>
        <td class="leftTD" width="20%">
            Module
        </td>
        <td class="rigthTD" style="width: 30%">
            <asp:Label ID="lblModule" runat="server"></asp:Label>
        </td>
        <td class="leftTD" width="20%">
            Request Status
        </td>
        <td class="rigthTD" style="width: 30%">
            <asp:Label ID="lblRequestStatus" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="tdSpace" colspan="4">
        </td>
    </tr>
    <tr>
        <td class="leftTD" width="20%">
            Requestor
        </td>
        <td class="rigthTD" style="width: 30%">
            <asp:Label ID="lblRequestor" runat="server"></asp:Label>
        </td>
        <td class="leftTD" width="20%">
            Request Date.
        </td>
        <td class="rigthTD" style="width: 30%">
            <asp:Label ID="lblRequestDate" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="tdSpace" colspan="4">
        </td>
    </tr>
</table>
