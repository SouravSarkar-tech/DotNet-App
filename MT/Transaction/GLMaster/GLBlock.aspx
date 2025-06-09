<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GLMaster/GLMasterPage.master" AutoEventWireup="true" CodeFile="GLBlock.aspx.cs" Inherits="Transaction_GLMaster_GLBlock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery.MultiFile.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">GL Master
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                                <td class="leftTD">GL Code
                                <%--<asp:Label ID="labletxtGLCode" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtGLCode" runat="server" CssClass="textbox" TabIndex="6"/>
                                </td>
                                <td class="tdSpace" colspan="2"></td>
                            </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" width="20%">Company Code
                                <asp:Label ID="lableddlCompanyCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlCompanyCode" runat="server" AppendDataBoundItems="false" Enabled="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCompanyCode" runat="server" ControlToValidate="ddlCompanyCode"
                                    ValidationGroup="block" ErrorMessage="Select Company Code." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company Code.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">Block in Chart Of Accounts
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Blocked for Creation
                                <asp:Label ID="lablechkBlockedforCreation" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkBlockedforCreation" runat="server" Text="Block" TabIndex="2"/>
                                <%--  onclick="return checkAllCompanies();" --%>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">Blocked for Posting
                                <asp:Label ID="lablechkBlockedforPosting" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkBlockedforPosting" runat="server" Text="Block" TabIndex="3" />
                                <%--  onclick="return checkSelectedCompany();" --%>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">Blocked for Planning
                                <asp:Label ID="lablechkBlockedforPlanning" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkBlockedforPlanning" runat="server" Text="Block" TabIndex="4" />
                                <%--  onclick="return checkSelectedCompanyChanged();" --%>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">Block in Company Code
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>

                            <td class="leftTD">Blocked for Posting
                                <asp:Label ID="lablechkSelectedPurchasingOrg" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkSelectedPurchasingOrg" runat="server" Text="Block" TabIndex="5" />
                            </td>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <asp:Panel ID="pnlRemarks" runat="server">
                            <tr>
                                <td class="leftTD">Remarks
                                <asp:Label ID="labletxtRemarks" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtRemarks" runat="server" MaxLength="250" TextMode="MultiLine" Width="90%" TabIndex="6" Rows="3" />
                                    <asp:RequiredFieldValidator ID="reqtxtRemarks" runat="server" ControlToValidate="txtRemarks"
                                    ValidationGroup="block" ErrorMessage="Remarks cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Remarks cannot be blank.' />" />
                                </td>
                                <td class="tdSpace" colspan="2"></td>
                            </tr>
                        </asp:Panel>
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
                                    <asp:FileUpload ID="file_upload" class="multi" runat="server" TabIndex="7" />
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
                                    TabIndex="8" UseSubmitBehavior="true" OnClick="btnSave_Click" />
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
    <%--<asp:Label ID="lblSectionId" runat="server" Text="94" Visible="false" />--%>
    <asp:Label ID="lblSectionId" runat="server" Text="<%$appSettings:SECGLB %>" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblGLBlockId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <asp:Label ID="lblTinNo" runat="server" Visible="false" />
    <asp:Label ID="lblGLCategory" runat="server" Visible="false" />
    <script type="text/javascript">
        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {

        });

        function checkAllCompanies() {
            if ($('#<%= chkBlockedforCreation.ClientID%>').is(':checked')) {
                $('#<%= chkBlockedforPosting.ClientID%>').attr('checked', false);
                $('#<%= chkBlockedforPlanning.ClientID%>').attr('checked', false);
            }
        }

        function checkSelectedCompany() {
            if ($('#<%= chkBlockedforPosting.ClientID%>').is(':checked')) {
                $('#<%= chkBlockedforCreation.ClientID%>').attr('checked', false);
                $('#<%= chkBlockedforPlanning.ClientID%>').attr('checked', false);
            }
        }

        function checkSelectedCompanyChanged() {
            if ($('#<%= chkBlockedforPlanning.ClientID%>').is(':checked')) {
                $('#<%= chkBlockedforCreation.ClientID%>').attr('checked', false);
                $('#<%= chkBlockedforPosting.ClientID%>').attr('checked', false);
            }
        }

    </script>
</asp:Content>

