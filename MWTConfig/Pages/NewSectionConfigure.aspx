<%@ Page Title="Section Configure" Language="C#" MasterPageFile="~/shared/Site.master"
    AutoEventWireup="true" CodeFile="NewSectionConfigure.aspx.cs" Inherits="Pages_NewSectionConfigure" %>

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
                            Section
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSection" AutoPostBack="true" runat="server" CssClass="DropDown"
                                OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            Company
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCompany" AutoPostBack="true" runat="server" CssClass="DropDown"
                                OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
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
                                            <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("Active") %>' />
                                            <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("ID") %>' />
                                            <asp:HiddenField ID="hdnMappingId" runat="server" Value="0" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Field Name">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFieldNameI" Text='<%#Eval("FieldName") %>' runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFieldNameF" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rqvFieldName" runat="server" ControlToValidate="txtFieldNameF"
                                                ForeColor="Red" ErrorMessage="Please enter the Field Name" Text="*" ToolTip="Please enter the Field Name"
                                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Field Description">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFieldDescriptionI" TextMode="MultiLine" Rows="3" Columns="4"
                                                runat="server" Text='<%# Eval("FieldDescription") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFieldDescriptionF" runat="server" TextMode="MultiLine" Rows="3"
                                                Columns="4"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rqvFieldDescription" runat="server" ControlToValidate="txtFieldDescriptionF"
                                                ForeColor="Red" ErrorMessage="Please enter the Field Description" Text="*" ToolTip="Please enter the Field Description"
                                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FieldStatus">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlFieldStatusI" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlFieldStatusF" runat="server">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approval Code">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtApprovalDept" runat="server"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="New Field">
                                        <FooterTemplate>
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="Add" OnClick="btnAdd_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>--%>
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
