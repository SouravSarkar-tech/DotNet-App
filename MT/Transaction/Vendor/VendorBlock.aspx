<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Vendor/VendorMasterPage.master"
    AutoEventWireup="true" CodeFile="VendorBlock.aspx.cs" Inherits="Transaction_Vendor_VendorBlock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery.MultiFile.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <%-- Added by Swati on 15.03.2019 for Ariba Migration Downtime Notification --%>
            <tr>
                <td colspan="4">
                    <asp:Panel ID="pnlWarning" runat="server" Visible = "false">
                        <asp:Label ID="lblWarning" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
            <%-- End --%>
            <tr>
                <td class="trHeading" align="center" colspan="2">
                    Vendor Master
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" width="20%">
                                Company Code
                                <asp:Label ID="lableddlCompanyCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlCompanyCode" runat="server" AppendDataBoundItems="false"
                                    TabIndex="2">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCompanyCode" runat="server" ControlToValidate="ddlCompanyCode"
                                    ValidationGroup="block" ErrorMessage="Select Company Code." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company Code.' />" />
                            </td>
                            <td class="leftTD" width="20%">
                                Purchasing
                                <asp:Label ID="lableddlPurchaseOrg" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                                Organization
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlPurchaseOrg" runat="server" AppendDataBoundItems="false"
                                    TabIndex="4">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPurchaseOrg" runat="server" ControlToValidate="ddlPurchaseOrg"
                                    ValidationGroup="block" ErrorMessage="Select Purchasing Organization." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Purchasing Organization.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                                Posting Block
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                All Companies
                                <asp:Label ID="lablechkAllCompanies" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkAllCompanies" runat="server" Text="Block" onclick="return checkAllCompanies();" />
                            </td>
                            <td class="leftTD">
                                Selected Company Code
                                <asp:Label ID="lablechkSelectedCompany" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkSelectedCompany" runat="server" Text="Block" onclick="return checkSelectedCompany();" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                                Purchasing Block
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                All Purchasing Org.
                                <asp:Label ID="lablechkAllPurchasingOrg" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkAllPurchasingOrg" runat="server" Text="Block" AutoPostBack="true"
                                    OnCheckedChanged="chkAllPurchasingOrg_CheckedChanged" />
                            </td>
                            <td class="leftTD">
                                Selected Purchasing Org.
                                <asp:Label ID="lablechkSelectedPurchasingOrg" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkSelectedPurchasingOrg" runat="server" Text="Block" AutoPostBack="true"
                                    OnCheckedChanged="chkSelectedPurchasingOrg_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                                Block For Quality Reasons
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Block Function
                                <asp:Label ID="lableddlBlockFunction" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlBlockFunction" runat="server" TabIndex="18">
                                    <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlBlockFunction" runat="server" ControlToValidate="ddlBlockFunction"
                                    ValidationGroup="block" ErrorMessage="Block Function cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Block Function cannot be blank.' />" />
                            </td>
                         <%--   <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>--%>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                                Payment Block
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Payment Block
                                <asp:Label ID="lableddlPaymentBlock" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlPaymentBlock" runat="server" TabIndex="18">
                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPaymentBlock" runat="server" ControlToValidate="ddlPaymentBlock"
                                    ValidationGroup="block" ErrorMessage="Payment Block  cannot be blank." InitialValue="0"
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Payment Block  cannot be blank.' />" />
                            </td>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <asp:Panel ID="pnlRemarks" runat="server">
                            <tr>
                                <td class="leftTD">
                                    Remarks
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtRemarks" runat="server" MaxLength="250" TextMode="MultiLine"
                                        Width="90%" TabIndex="37" Rows="3" />
                                </td>
                                <td class="tdSpace" colspan="2">
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td colspan="4" class="tdSpace">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" align="left" colspan="4">
                                <b>Attach Documents (Image/PDF Files Only)</b>
                            </td>
                        </tr>
                        <tr runat = "server" id = "trMandatoryDocs">
                            <%--Vendor workflow modification start--%>
                            <%--<td colspan="2">
                                <asp:GridView ID="grdMandDocs" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>  
                                                <asp:RadioButton ID="rdoSelection" runat="server" GroupName="selection" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />                                              
                                               
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMandDocId" runat="server" Text='<%# Eval("DocListId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Mandatory Document Name" DataField="DocName" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                       
                                    </Columns>
                                </asp:GridView>
                            </td>--%>
                            <%--Vendor workflow modification end--%>
                            <td class="rigthTD" align="left" colspan="2">
                                <asp:GridView ID="grdAttachedDocs" runat="server" AutoGenerateColumns="False" DataKeyNames="Document_Upload_Id"
                                    Visible="False" OnRowCommand="grdAttachedDocs_RowCommand">
                                    <Columns>
                                        <%--//Vendor workflow modification start--%>
                                        <%--<asp:TemplateField HeaderText="Document Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttachedDocType" runat="server" Text='<%# Eval("Doc_Type") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--//Vendor workflow modification end--%>
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
                            <td class="rigthTD" align="left" colspan="2" valign="top">
                                <div>
                                    <%--<asp:FileUpload ID="file_upload" class="multi" runat="server" TabIndex="36" />--%>
                                    <asp:FileUpload ID="file_upload" runat="server" TabIndex="36" />
                                    <asp:Button ID= "btnUploadDoc" runat = "server" Text = "Upload" OnClientClick="return Validate();" OnClick="btnUploadDoc_Click"/>
                                    <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />      
                                </div>
                            </td>
                             <td class = "tdSpace" colspan = "2">
                            </td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="block" Text="Save" CssClass="button"
                                    TabIndex="41" UseSubmitBehavior="true" OnClick="btnSave_Click" />
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
    <asp:Label ID="lblSectionId" runat="server" Text="46" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblVendorBlockId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <asp:Label ID="lblTinNo" runat="server" Visible="false" />
    <asp:Label ID="lblVendorCategory" runat="server" Visible="false" />
    <script type="text/javascript">
        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {

        });

        function checkAllCompanies() {
            if ($('#<%= chkAllCompanies.ClientID%>').is(':checked')) {
                $('#<%= chkSelectedCompany.ClientID%>').attr('checked', false);
            }
        }

        function checkSelectedCompany() {
            if ($('#<%= chkSelectedCompany.ClientID%>').is(':checked')) {
                $('#<%= chkAllCompanies.ClientID%>').attr('checked', false);
            }
        }
        
    </script>
</asp:Content>
