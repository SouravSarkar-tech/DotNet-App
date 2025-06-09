<%@ Page Title="Field Status Master" Language="C#" MasterPageFile="~/shared/Site.master" AutoEventWireup="true" CodeFile="FieldStatusMaster.aspx.cs" Inherits="Pages_FieldStatusMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
    Field status
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <fieldset>
        <legend>Field Status Master</legend>
        <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvFieldStatus" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="grid" Width="100%" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("Active") %>' />
                                            <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNameI" runat="server" Text='<%# Eval ("Name") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqNameI" ControlToValidate="txtNameI" runat="server" Text="*" ErrorMessage="Please enter the Name" ToolTip="Please enter the name"
                                                ValidationGroup="submit" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNameF" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqNameF" ControlToValidate="txtNameF" runat="server" Text="*" ErrorMessage="Please enter the Name" ToolTip="Please enter the name"
                                                ValidationGroup="Add" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDescriptionI" runat="server" Text='<%# Eval ("Description") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDescriptionF" runat="server"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Add">
                                        <FooterTemplate>
                                            <asp:Button ID="btnAdd" Text="Add" runat="server" ValidationGroup="Add" OnClick="btnAdd_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnSubmit" ValidationGroup="submit" runat="server" Text="Submit" Visible="false" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>

                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>

