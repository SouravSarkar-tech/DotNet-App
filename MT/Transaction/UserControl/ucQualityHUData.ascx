<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucQualityHUData.ascx.cs"
    Inherits="Transaction_UserControl_ucQualityHUData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Panel ID="pnlQualityData" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="trHeading" align="center">
                Detailed information on inspection type
            </td>
        </tr>
        <tr>
            <td class="tdSpace">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlQualityInspectionData" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                         <tr runat = "server" visible = "false">
                            <td class="tdSpace" colspan="2">
                                <asp:Label runat="server" ID="lblInspectionType" />
                            </td>
                        </tr>
                        <tr>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkPostInspStock" runat="server" Text="Post to Insp. stock"
                                    TabIndex="1" />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkInspHU" runat="server" Text="Insp. for HU"
                                    TabIndex="2" />
                            </td>
                        </tr>
                        <tr>
                            <td class = "tdSpace" colspan = "2">
                            </td>
                        </tr>                        
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Label ID="lblUserId" runat="server" Visible="false" />
<asp:Label ID="lblInspDataId" runat="server" Visible="false" />
<asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
<asp:Label ID="lblModuleId" runat="server" Visible="false" />
