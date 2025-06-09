<%@ Page Title="" Language="C#" MasterPageFile="~/shared/Site.master" AutoEventWireup="true"
    CodeFile="ProfileMenuMapping.aspx.cs" Inherits="Pages_ProfileMenuMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <fieldset>
        <legend>Profile Menu</legend>
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
                            Profile
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlprofile" AutoPostBack="true" runat="server" CssClass="DropDown"
                                OnSelectedIndexChanged="ddlprofile_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvMenu" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records"
                                Width="100%" ShowFooter="true" HeaderStyle-CssClass="grid" 
                                GridLines="Both" AllowPaging="false" PageSize="20" 
                                onrowdatabound="gvMenu_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActive" runat="server" />
                                            <asp:HiddenField ID="hdnMenuId" runat="server" Value='<%# Eval("Menu_ID") %>' />
                                            <asp:HiddenField ID="hdnMappingId" runat="server" Value="0" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Menu_Name" DataField="Menu_Name" NullDisplayText="N\A" />
                                    <asp:TemplateField HeaderText="View Rights">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkView" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Add Rights">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAdd" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Update Rights">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkUpdate" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete Rights">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Menu Sequence">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSequence" runat="server"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="btnSubmit" Visible="false" runat="server" Text="Submit" 
                                onclick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>
