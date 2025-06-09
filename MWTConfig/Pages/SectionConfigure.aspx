<%@ Page Title="Section Configure" Language="C#" MasterPageFile="~/shared/Site.master"
    AutoEventWireup="true" CodeFile="SectionConfigure.aspx.cs" Inherits="Pages_SectionConfigure" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="HTMLEditor" %>
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
                        <td colspan="2">
                            <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
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
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvSection" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records"
                                Width="100%" ShowFooter="true" HeaderStyle-CssClass="grid" GridLines="Both" AllowPaging="false"
                                PageSize="20">
                                <Columns>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("Active") %>' />
                                            <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("ID") %>' />
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
                                    <asp:TemplateField HeaderText="Display Name">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFieldDisplayNameI" Text='<%#Eval("FeildDisplayName") %>' runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFieldDisplayNameF" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rqvFieldDisplayName" runat="server" ControlToValidate="txtFieldDisplayNameF"
                                                ForeColor="Red" ErrorMessage="Please enter the Display Name" Text="*" ToolTip="Please enter the Display Name"
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
                                    <asp:TemplateField HeaderText="SAP Name">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSAPNameI" Text='<%#Eval("SAP_Feild") %>' runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtSAPNameF" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rqvSAPName" runat="server" ControlToValidate="txtSAPNameF"
                                                ForeColor="Red" ErrorMessage="Please enter the SAP Name" Text="*" ToolTip="Please enter the SAP Name"
                                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Field Length">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFieldLengthI" Text='<%#Eval("Feild_Length") %>' runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFieldLengthF" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rqvFieldLength" runat="server" ControlToValidate="txtFieldLengthF"
                                                ForeColor="Red" ErrorMessage="Please enter the Field Length" Text="*" ToolTip="Please enter the Field Length"
                                                ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Field Help">
                                        <ItemTemplate>
                                            <htmleditor:editor runat="server" id="HtmlEFieldHelpTextI" height="200px" content='<%# Eval("Field_Help_Text") %>'
                                                autofocus="true" width="100%" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <htmleditor:editor runat="server" id="HtmlEFieldHelpTextI" height="200px"
                                                autofocus="true" width="100%" />
                                        </FooterTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="New Field">
                                        <FooterTemplate>
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="Add" OnClick="btnAdd_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnSubmit" Visible="false" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>
