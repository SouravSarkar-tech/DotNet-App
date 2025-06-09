<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/BOMRecipe/BOMRecipeMasterPage.master"
    AutoEventWireup="true" CodeFile="BOMRecipeChange.aspx.cs" Inherits="Transaction_BOMRecipe_BOMRecipeChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="4">
                    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                        <asp:Label ID="lblMsg" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="trHeading" align="center" colspan="4">
                    BOM Recipe Change Data
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="leftTD" align="left" colspan="4">
                    <b>Attach Documents (Excel Files Only)</b>
                </td>
            </tr>
            <tr>
                <td class="rigthTD" align="left" colspan="2" valign="top">
                    <div>
                        <asp:FileUpload ID="file_upload" class="multi" runat="server" TabIndex="36" />
                        <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />
                        <asp:Button ID="btnAttach" runat="server" Text="Attach" CssClass = "button" 
                            onclick="btnAttach_Click"/>
                    </div>
                </td>
                <td class="rigthTD" align="left" colspan="2">
                    <asp:GridView ID="grdAttachedDocs" runat="server" AutoGenerateColumns="False" DataKeyNames="Document_Upload_Id"
                        Visible="False" OnRowCommand="grdAttachedDocs_RowCommand">                         
                        <Columns>
                            <asp:TemplateField HeaderText="Attached Documents">
                                <ItemTemplate>
                                    <asp:Label ID="lblAttachedDocName" runat="server" Text='<%# Eval("Document_Name") %>'
                                        Visible="false" />
                                    <asp:Label ID="lblUploadedFileName" runat="server" Text='<%# Eval("Document_Name") %>'
                                        Visible="false" />
                                    <asp:HyperLink ID="aDocPath" runat="server" Text='<%# Eval("Document_Name") %>' NavigateUrl='<%# Eval("Document_Path") %>'
                                        Target="_blank"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    &nbsp;
                                    <asp:LinkButton ID="lnkDelete" runat="server" Text="X" ForeColor="Red" Font-Size="15px"
                                        CommandName="DEL" Font-Bold="true" OnClientClick="return confirm('Are you certain you want to delete this document?');" />&nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="leftTD">
                    Remarks
                    <asp:Label ID="labletxtRemarks" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td class="rigthTD" colspan = "2">
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textarea" TextMode="MultiLine"
                        TabIndex="26" Columns="100" Rows="3" />
                    <asp:RequiredFieldValidator ID="reqtxtRemarks" runat="server" ControlToValidate="txtRemarks"
                        ValidationGroup="BRChange" ErrorMessage="Remarks cannot be blank." SetFocusOnError="true"
                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Remarks cannot be blank.' />" />
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="4">
                </td>
            </tr>
            <tr id="trButton" runat="server" visible="false">
                <td class="centerTD" colspan="4">
                    <asp:Button ID="btnPrevious" runat="server" Text="Back" UseSubmitBehavior="false"
                        TabIndex="38" CssClass="button" ValidationGroup = "BRChange" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" UseSubmitBehavior="true"
                        TabIndex="39" ValidationGroup = "BRChange" OnClick="btnSave_Click" />
                    <asp:Button ID="btnNext" runat="server" Text="Save & Proceed to Submit" UseSubmitBehavior="true"
                        TabIndex="40" CssClass="button" Width="186px" ValidationGroup = "BRChange" 
                        OnClick="btnNext_Click"/>
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="4">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="change" runat="server" ValidationGroup="BRChange" ShowMessageBox="true" ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="78" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblBRChangeId" runat="server" Visible="false" />
    <asp:Label ID="lblBRPlantGrpId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
</asp:Content>
