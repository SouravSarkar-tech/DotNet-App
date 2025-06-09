<%@ Page Title="Section Configure" Language="C#" MasterPageFile="~/shared/Site.master"
    AutoEventWireup="true" CodeFile="ModuleSectionConfigure.aspx.cs" Inherits="Pages_ModuleSectionConfigure" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <fieldset>
        <legend>Finance Section</legend>
        <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Module
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlModule" AutoPostBack="true" runat="server" CssClass="DropDown"
                                OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            Plant Group
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlantGrp" AutoPostBack="true" runat="server" CssClass="DropDown"
                                OnSelectedIndexChanged="ddlPlantGrp_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Company
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCompany" AutoPostBack="true" runat="server" CssClass="DropDown"
                                OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            Department
                        </td>
                        <td>
                            <asp:DropDownList ID="ddldepartment" AutoPostBack="true" runat="server" CssClass="DropDown"
                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvSection" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records"
                                Width="100%" ShowFooter="true" HeaderStyle-CssClass="grid" OnRowDataBound="gvSection_RowDataBound"
                                GridLines="Both" AllowPaging="false" PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActive" runat="server"/>
                                            <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("ID") %>' />
                                            <asp:HiddenField ID="hdnMappingId" runat="server" Value="0" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Section Name" DataField="Name" />
                                    <asp:TemplateField HeaderText="View Rights">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkView" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Input Rights">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkInput" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Requestor">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRequestor" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sequence">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSequence" runat="server" Text='<%# Eval("Sequence") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approval Code">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtApprovalDept" runat="server"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="btnSubmit" Visible="false" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>
