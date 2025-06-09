<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/CostCenter/CostCenterMasterPage.master" AutoEventWireup="true" CodeFile="CostCenterBlock.aspx.cs" Inherits="Transaction_CostCenter_CostCenterBlock" %>

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
                <td class="trHeading" align="center" colspan="2">Cost Center Master
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr> 
                        <tr>
                            <td class="leftTD" style="width: 20%">Company Code
                                   </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlCompanyCode" runat="server" TabIndex="13" Enabled="false">
                                </asp:DropDownList>
                                 </td>
                            <td class="leftTD" style="width: 20%">Business Area
                                  </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlBusinessArea" runat="server" AppendDataBoundItems="false" TabIndex="14" Enabled="false">
                                    <asp:ListItem Text="---Select---" Value="" />
                                </asp:DropDownList>
                                </td>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>


                        <tr>
                            <td class="leftTD" width="20%">Cost Center
                                <asp:Label ID="labletxtCostCenter" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCostCenter" runat="server" Enabled="false" TabIndex="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtCostCenter" runat="server" ControlToValidate="txtCostCenter"
                                    ValidationGroup="block" ErrorMessage="Cost Center cannot be blank" SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Cost Center cannot be blank.' />" />
                            </td>
                        </tr>

                        <tr>
                            <td class="leftTD" width="20%">Cost Center Name as per SAP
                                <asp:Label ID="LabeltxtCCName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCCName" runat="server" Enabled="false" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtCCName" runat="server" ControlToValidate="txtCCName"
                                    ValidationGroup="block" ErrorMessage="Cost Center name cannot be blank." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Cost Center name cannot be blank.' />" />
                            </td>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                                <b><asp:Label ID="lblSubHeading" runat="server" Text=""></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Lock Indicator for Actual Primary Costs
                                <asp:Label ID="lablechkActPrimaryCost" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkActPrimaryCost" runat="server" Text="Block" TabIndex="3" />
                            </td>

                            <td class="leftTD">Lock Indicator for Plan Primary Costs
                                <asp:Label ID="lablechkPlnPrimaryCost" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkPlnPrimaryCost" runat="server" Text="Block" TabIndex="4"/>
                            </td>

                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td class="leftTD">Lock Indicator for Actual Secondary Costs
                                <asp:Label ID="lablechkActSecCost" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkActSecCost" runat="server" Text="Block" TabIndex="5"/>
                            </td>

                            <td class="leftTD">Lock Indicator for Plan Secondary Costs
                                <asp:Label ID="lablechkPlnSecCost" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkPlnSecCost" runat="server" Text="Block" TabIndex="6"/>
                            </td>

                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD"><asp:Label ID="lblchkActRevPostings" runat="server" Text="Lock Indicator for Actual Revenue Postings"></asp:Label>
                                <asp:Label ID="labelchkActRevPostings" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkActRevPostings" runat="server" Text="Block" TabIndex="7"/>
                            </td>

                            <td class="leftTD"><asp:Label ID="lblchkPlnRevPostings" runat="server" Text="Lock Indicator for Plan Revenue Postings"></asp:Label>
                                <asp:Label ID="labelchkPlnRevPostings" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkPlnRevPostings" runat="server" Text="Block" TabIndex="8"/>
                            </td>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <asp:Panel ID="pnlRemarks" runat="server">
                            <tr>
                                <td class="leftTD">Remarks
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtRemarks" runat="server" MaxLength="250" TextMode="MultiLine"
                                        Width="90%" TabIndex="9" Rows="3" />
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
    <asp:Label ID="lblSectionId" runat="server" Text="<%$appSettings:SECCCB %>" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblCostCenterBlockId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <asp:Label ID="lblTinNo" runat="server" Visible="false" />
    <asp:Label ID="lblCostCenterCategory" runat="server" Visible="false" />
    <script type="text/javascript">
        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {

        });

    </script>
</asp:Content>

