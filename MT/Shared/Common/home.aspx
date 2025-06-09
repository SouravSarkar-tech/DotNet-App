<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/HomeMasterPage.master"
    AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Shared_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="left">
        <asp:Panel ID="pnlVendor" runat="server">
            <fieldset>
                <legend class="DashLegend">
                    <asp:Label ID="Label2" runat="server" Text="Vendor DashBoard"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlVenderModule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVenderModule_SelectedIndexChanged">
                    </asp:DropDownList>
                </legend>
                <table width="100%">
                    <tr>
                        <td colspan="2" align="center">
                            <table align="center">
                                <tr>
                                    <asp:Repeater ID="rptVendorDashboard" runat="server">
                                        <ItemTemplate>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <a id="lnkRedirect" runat="server" href="../../Transaction/Vendor/VendorMaster.aspx"
                                                            style="text-decoration: none">
                                                            <td id="tdimg" runat="server">
                                                                <asp:Label ID="lblWorkflow" Font-Italic="true" runat="server"></asp:Label>
                                                            </td>
                                                        </a>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTotalPendings" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlDashBoard" runat="server">
            <fieldset>
                <legend class="DashLegend">
                    <asp:Label ID="lblModuleName" runat="server" Text="Material DashBoard"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlMaterialModule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialModule_SelectedIndexChanged"
                        ToolTip="Select Material Type">
                    </asp:DropDownList>
                </legend>
                <table width="100%">
                    <tr>
                        <td colspan="2" align="center">
                            <table align="center">
                                <tr>
                                    <asp:Repeater ID="rptMaterialDashBoard" runat="server">
                                        <ItemTemplate>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <a id="lnkRedirect" runat="server" href="../../Transaction/Material/MaterialMaster.aspx?pg=6"
                                                            style="text-decoration: none">
                                                            <td id="tdimg" runat="server">
                                                                <asp:Label ID="lblWorkflow" Font-Italic="true" runat="server"></asp:Label>
                                                            </td>
                                                        </a>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTotalPendings" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlCustomer" runat="server">
            <fieldset>
                <legend class="DashLegend">
                    <asp:Label ID="Label1" runat="server" Text="Customer DashBoard"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                    </asp:DropDownList>
                </legend>
                <table width="100%">
                    <tr>
                        <td colspan="2" align="center">
                            <table align="center">
                                <tr>
                                    <asp:Repeater ID="rptCustomerashBoard" runat="server">
                                        <ItemTemplate>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <a id="lnkRedirect" runat="server" href="../../Transaction/Customer/CustomerMaster.aspx"
                                                            style="text-decoration: none">
                                                            <td id="tdimg" runat="server">
                                                                <asp:Label ID="lblWorkflow" Font-Italic="true" runat="server"></asp:Label>
                                                            </td>
                                                        </a>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTotalPendings" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlBomDashboard" runat="server">
            <fieldset>
                <legend class="DashLegend">
                <%--<legend style="font-size: 0.9em; font-weight: 600; padding: 2px 4px 8px 10px; background-color: #bddbfa;
                    border: 1px solid Gray;">--%>
                    <asp:Label ID="Label4" runat="server" Text="BOM/Recipe DashBoard"></asp:Label>&nbsp;
                    <asp:DropDownList ID="ddlBomModule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBomModule_SelectedIndexChanged">
                    </asp:DropDownList>
                </legend>
                <table width="100%">
                    <tr>
                        <td colspan="2" align="center">
                            <table align="center">
                                <tr>
                                    <asp:Repeater ID="rptBomDashboard" runat="server">
                                        <ItemTemplate>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <a id="lnkRedirect" runat="server" href="../../transaction/BOM/BOMMaster.aspx" style="text-decoration: none">
                                                            <td id="tdimg" runat="server">
                                                                <asp:Label ID="lblWorkflow" Font-Italic="true" runat="server"></asp:Label>
                                                            </td>
                                                        </a>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTotalPendings" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlRecipe" runat="server">
            <fieldset>
                <legend style="font-size: 0.9em; font-weight: 600; padding: 2px 4px 8px 10px; background-color: #bddbfa;
                    border: 1px solid Gray;">
                    <asp:Label ID="Label3" runat="server" Text="Recipe DashBoard"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddlReceipeModule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReceipeModule_SelectedIndexChanged">
                    </asp:DropDownList>
                </legend>
                <table width="100%">
                    <tr>
                        <td colspan="2" align="center">
                            <table align="center">
                                <tr>
                                    <asp:Repeater ID="rptRecipeDashboard" runat="server">
                                        <ItemTemplate>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <a id="lnkRedirect" runat="server" href="../../transaction/Receipe/ReceipeMaster.aspx"
                                                            style="text-decoration: none">
                                                            <td id="tdimg" runat="server">
                                                                <asp:Label ID="lblWorkflow" Font-Italic="true" runat="server"></asp:Label>
                                                            </td>
                                                        </a>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTotalPendings" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
</asp:Content>
