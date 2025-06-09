<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/ProfitCenter/ProfitCenterMaster.master" AutoEventWireup="true" CodeFile="ProfitCenterBlock.aspx.cs" Inherits="Transaction_ProfitCenter_ProfitCenterBlock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">Profit Center Master
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" width="20%">Profit Center
                                <asp:Label ID="labletxtProfitCenter" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtProfitCenter" runat="server" Enabled="false" TabIndex="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtProfitCenter" runat="server" ControlToValidate="txtProfitCenter"
                                    ValidationGroup="block" ErrorMessage="Profit Center cannot be blank" SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Profit Center cannot be blank.' />" />
                            </td>
                        </tr>

                        <tr>
                            <td class="leftTD" width="20%">Profit Center Name as per SAP
                                <asp:Label ID="LabeltxtPCName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtPCName" runat="server" Enabled="false" TabIndex="2" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtCCName" runat="server" ControlToValidate="txtPCName"
                                    ValidationGroup="block" ErrorMessage="Profit Center name cannot be blank." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Profit Center name cannot be blank.' />" />
                            </td>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                                <%--Block/Unblock--%>
                                <asp:Label ID="lblBlock_Unblock" runat="server" Text="Block/Unblock"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Status 
                                        <asp:Label ID="lablerdlBlockValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" colspan="3">
                                <asp:DropDownList ID="ddlBlockValue" runat="server" Enabled="false">
                                    <asp:ListItem Text="Active" Value="1" />
                                    <asp:ListItem Text="In-Active" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlBlockValue" runat="server" ControlToValidate="ddlBlockValue"
                                    ValidationGroup="block" ErrorMessage="Block/Unblock cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Block/Unblock cannot be blank.' />" />
                            </td>
                        </tr>


                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Remarks
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtRemarks" runat="server" MaxLength="250" TextMode="MultiLine"
                                    Width="90%" TabIndex="9" Rows="3" />
                            </td>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdSpace"></td>
                        </tr>

                        <tr>
                            <td class="leftTD" align="left" colspan="4">
                                <b>Attach Documents (Image/PDF Files Only)</b>
                            </td>
                        </tr>
                        <tr>
                            <td class="rigthTD" align="left" colspan="2" valign="top">
                                <div>
                                    <asp:FileUpload ID="file_upload" class="multi" runat="server" TabIndex="10" />
                                    <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />
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

                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="block" Text="Save" CssClass="button"
                                    TabIndex="11" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="block" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <%--<asp:Label ID="lblSectionId" runat="server" Text="91" Visible="false" />--%>
    <asp:Label ID="lblSectionId" runat="server" Text="<%$appSettings:SECPCB %>" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblProfitCenterBlockId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />

    <script type="text/javascript">
        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {

        });

    </script>
</asp:Content>

