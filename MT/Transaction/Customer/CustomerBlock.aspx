<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Customer/CustomerMasterPage.master"
    AutoEventWireup="true" CodeFile="CustomerBlock.aspx.cs" Inherits="Transaction_Customer_CustomerBlock" %>

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
            <tr>
                <td class="trHeading" align="center" colspan="2">
                    Customer Master
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
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCompanyCode" runat="server" ControlToValidate="ddlCompanyCode"
                                    ValidationGroup="block" ErrorMessage="Select Company Code." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company Code.' />" />
                            </td>
                            <td class="tdSpace" colspan="2">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>Customer Description </strong>:
                                <asp:Label ID="lblCustomerDesc" runat="server" Font-Italic="true" Font-Underline="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Sales Organization
                                <asp:Label ID="lableddlSalesOrginization" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlSalesOrginization" runat="server" TabIndex="2" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSalesOrginization_SelectedIndexChanged">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlSalesOrginization" runat="server" ControlToValidate="ddlSalesOrginization"
                                    ValidationGroup="block" ErrorMessage="Sales Organization cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />
                            </td>
                            <td class="tdSpace" colspan="2">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>Customer Type </strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:
                                <asp:Label ID="lblCustomerType" runat="server" Font-Italic="true" Font-Underline="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Distribution Channel
                                <asp:Label ID="lableddlDistributionChannel" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlDistributionChannel" runat="server" TabIndex="3" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlDistributionChannel_SelectedIndexChanged">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlDistributionChannel" runat="server" ControlToValidate="ddlDistributionChannel"
                                    ValidationGroup="block" ErrorMessage="Distribution Channel cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Division
                                <asp:Label ID="lableddlDivision" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlDivision" runat="server" TabIndex="4">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlDivision" runat="server" ControlToValidate="ddlDivision"
                                    ValidationGroup="block" ErrorMessage="Division cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Division cannot be blank.' />" />
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
                                <asp:CheckBox ID="chkAllCompanies" runat="server" Text="Block" onclick="return checkAllCompanies();"
                                    TabIndex="5" OnCheckedChanged="chkAllCompanies_CheckedChanged" />
                            </td>
                            <td class="leftTD">
                                Selected Company Code
                                <asp:Label ID="lablechkSelectedCompany" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkSelectedCompany" runat="server" Text="Block" onclick="return checkSelectedCompany();"
                                    TabIndex="6" OnCheckedChanged="chkSelectedCompany_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                                Order Block
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                All Sales Areas
                                <asp:Label ID="lablechkAllSalesAreaOrderBlock" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkAllSalesAreaOrderBlock" runat="server" Text="Block" AutoPostBack="true"
                                    TabIndex="7" OnCheckedChanged="chkAllSalesAreaOrderBlock_CheckedChanged" />
                            </td>
                            <td class="leftTD">
                                Selected Sales Areas
                                <asp:Label ID="lablechkSelectedSalesAreaOrderBlock" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkSelectedSalesAreaOrderBlock" runat="server" Text="Block" AutoPostBack="true"
                                    TabIndex="8" OnCheckedChanged="chkSelectedSalesAreaOrderBlock_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                                Delivery Block
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                All Sales Areas
                                <asp:Label ID="lablechkAllSalesAreaDeliveryBlock" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkAllSalesAreaDeliveryBlock" runat="server" Text="Block" AutoPostBack="true"
                                    TabIndex="9" OnCheckedChanged="chkAllSalesAreaDeliveryBlock_CheckedChanged" />
                            </td>
                            <td class="leftTD">
                                Selected Sales Areas
                                <asp:Label ID="lablechkSelectedSalesAreaDeliveryBlock" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkSelectedSalesAreaDeliveryBlock" runat="server" Text="Block"
                                    TabIndex="10" AutoPostBack="true" OnCheckedChanged="chkSelectedSalesAreaDeliveryBlock_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                                Billing Block
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                All Sales Areas
                                <asp:Label ID="lablechkAllSalesAreaBillingBlock" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkAllSalesAreaBillingBlock" runat="server" Text="Block" AutoPostBack="true"
                                    TabIndex="11" OnCheckedChanged="chkAllSalesAreaBillingBlock_CheckedChanged" />
                            </td>
                            <td class="leftTD">
                                Selected Sales Areas
                                <asp:Label ID="lablechkSelectedSalesAreaBillingBlock" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkSelectedSalesAreaBillingBlock" runat="server" Text="Block" AutoPostBack="true"
                                    TabIndex="12" OnCheckedChanged="chkSelectedSalesAreaBillingBlock_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                                Block Sales Support
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                All Sales Areas
                                <asp:Label ID="lablechkAllSalesAreaBlockSalesSupport" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkAllSalesAreaBlockSalesSupport" runat="server" Text="Block" AutoPostBack="true"
                                    TabIndex="13" OnCheckedChanged="chkAllSalesAreaBlockSalesSupport_CheckedChanged" />
                            </td>
                            <td class="leftTD">
                                Selected Sales Areas
                                <asp:Label ID="lablechkSelectedSalesAreaBlockSalesSupport" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkSelectedSalesAreaBlockSalesSupport" runat="server" Text="Block"
                                    TabIndex="14" AutoPostBack="true" OnCheckedChanged="chkSelectedSalesAreaBlockSalesSupport_CheckedChanged" />
                            </td>
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
                                        Width="90%" TabIndex="15" Rows="3" />
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
                        <tr>
                            <td class="rigthTD" align="left" colspan="2" valign="top">
                                <div>
                                    <asp:FileUpload ID="file_upload" class="multi" runat="server" TabIndex="16" />
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
                                    TabIndex="17" UseSubmitBehavior="true" OnClick="btnSave_Click" />
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
    <asp:Label ID="lblSectionId" runat="server" Text="49" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblCustomerBlockId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <asp:Label ID="lblTinNo" runat="server" Visible="false" />
    <asp:Label ID="lblCustomerCategory" runat="server" Visible="false" />
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
