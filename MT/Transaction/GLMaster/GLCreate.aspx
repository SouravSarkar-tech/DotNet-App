<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GLMaster/GLMasterPage.master" AutoEventWireup="true" CodeFile="GLCreate.aspx.cs" Inherits="Transaction_GLMaster_GLCreate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetToolTip(control) {
            ddrivetip('32', control);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">GL CREATION FORM
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">G/L Code
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtGLCode" runat="server" MaxLength="8" CssClass="textbox" Width="150px" Enabled="false" TabIndex="1"></asp:TextBox>
                            </td>

                            <td class="leftTD" style="width: 20%">Reference G/L Code
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtRefGLCode" runat="server" MaxLength="8" CssClass="textbox" Width="150px" TabIndex="2"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>

                            <td class="leftTD" style="width: 20%">Company Code
                                <asp:Label ID="LabelddlCompanyCode" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlCompanyCode" runat="server" TabIndex="3">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCompanyCode" runat="server" ControlToValidate="ddlCompanyCode"
                                   InitialValue="0"  ValidationGroup="save" ErrorMessage="Company Code cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Company Code cannot be blank.' />" />
                            </td>

                            <td class="leftTD" style="width: 20%">Reference Company Code
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlRefCompanyCode" runat="server" TabIndex="4">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td class="leftTD" style="width: 20%">Account Group
                                <asp:Label ID="lableddlAccGroup" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlAccGroup" runat="server" TabIndex="5" OnSelectedIndexChanged="ddlAccGroup_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlAccGroup" runat="server" ControlToValidate="ddlAccGroup"
                                    ValidationGroup="save" ErrorMessage="Account Group cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Account Group cannot be blank.' />" />
                            </td>

                            <%--S4HanaGLDT07122021--%>

                                                    <%--    <td class="leftTD" style="width: 20%">P&L Statement/Balance Sheet Account
                                <asp:Label ID="lableddlAccType" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlAccType" runat="server" TabIndex="6" OnSelectedIndexChanged="ddlAccType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="P&L Statement Account" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Balance Sheet Account" Value="2"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlAccType" runat="server" ControlToValidate="ddlAccType"
                                    ValidationGroup="save" ErrorMessage="Please select P&L / Balance Sheet A/C" SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Please select P&L / Balance Sheet A/C' />" />
                            </td>--%>
                            <%--S4HanaGLDT07122021--%>

                            <%--S4HanaGLDT07122021--%>
                            <td class="leftTD" style="width: 20%">G/L Account Type
                                <asp:Label ID="lableddlGLAccType" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlGLAccType" runat="server" TabIndex="6" OnSelectedIndexChanged="ddlGLAccType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Balance Sheet Account" Value="X"></asp:ListItem>
                                    <asp:ListItem Text="Nonoperating Expense or Income" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="Primary Costs or Revenue" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="Secondary Costs" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="Cash Account" Value="C"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlGLAccType" runat="server" ControlToValidate="ddlGLAccType"
                                    ValidationGroup="save" ErrorMessage="Please select G/L Account Type" SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Please select G/L Account Type' />" />
                            </td>
                            <%--S4HanaGLDT07122021--%>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td class="leftTD" style="width: 20%">Cost Element Category
                                <asp:Label ID="labelddlCostElementCategory" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlCostElementCategory" runat="server" TabIndex="7" Enabled="false">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCostElementCategory" runat="server" ControlToValidate="ddlCostElementCategory"
                                    ValidationGroup="save" ErrorMessage="Cost Element Category cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Cost Element Category cannot be blank.' />" />
                            </td>

                            <%--S4HanaGLDT07122021--%>
                            <td class="leftTD" style="width: 20%">G/L Account Sub Type
                                <asp:Label ID="lableddlGLAccSubType" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlGLAccSubType" runat="server" TabIndex="6" Enabled="false">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Bank Reconciliation Account" Value="B"></asp:ListItem>
                                    <asp:ListItem Text="Pertty Cash" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="Bank Sub Account" Value="S"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlGLAccSubType" runat="server" ControlToValidate="ddlGLAccSubType"
                                    ValidationGroup="save" ErrorMessage="Please select G/L Account Sub Type" SetFocusOnError="true"
                                    Display="Dynamic" Visible="false" Text="<img src='../../images/Error.png' title='Please select G/L Account Sub Type' />" />
                            </td>
                            <%--S4HanaGLDT07122021--%>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td colspan="2"><b>Description</b></td>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Short Text
                                <asp:Label ID="labletxtShortText" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtShortText" runat="server" MaxLength="20" CssClass="textbox" Width="150px" TabIndex="8"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtShortText" runat="server"
                                    ControlToValidate="txtShortText" ValidationGroup="save"
                                    ErrorMessage="Short Text cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Short Text cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Language
                                <asp:Label ID="lableddlLang1" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlLang1" runat="server" AppendDataBoundItems="false" TabIndex="9" Enabled="false">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlLang1" runat="server" ControlToValidate="ddlLang1"
                                    ValidationGroup="save" ErrorMessage="Language1 cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Language1 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">G/L Account Long Text
                                <asp:Label ID="labletxtLongText" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtLongText" runat="server" MaxLength="50" CssClass="textbox" Width="150px" TabIndex="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtLongText" runat="server"
                                    ControlToValidate="txtLongText" ValidationGroup="save"
                                    ErrorMessage="G/L Account Long Text cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='G/L Account Long Text cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Language
                                <asp:Label ID="lableddlLang2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlLang2" runat="server" AppendDataBoundItems="false" TabIndex="11" Enabled="false">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlLang2" runat="server" ControlToValidate="ddlLang2"
                                    ValidationGroup="save" ErrorMessage="Language2 cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Language2 cannot be blank.' />" />
                            </td>
                        </tr>

                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td colspan="2"><b>Control Data</b></td>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Reconciliation account for acct type
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlRecAcc" runat="server" TabIndex="12">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="A - Assets" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="D - Customers" Value="D"></asp:ListItem>
                                    <asp:ListItem Text="K - Vendors" Value="K"></asp:ListItem>
                                    <asp:ListItem Text="V - Contract accounts receivable" Value="V"></asp:ListItem>

                                </asp:DropDownList>
                            </td>
                            <td class="leftTD" style="width: 20%">Open Item Management
                                <asp:Label ID="lableddlOpenItemMgmt" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlOpenItemMgmt" runat="server" TabIndex="13">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlOpenItemMgmt" runat="server" ControlToValidate="ddlOpenItemMgmt"
                                    ValidationGroup="save" ErrorMessage="Open Item Management Office cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Open Item Management cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <%--S4HanaGLDT07122021--%>
                      <%--      <td class="leftTD" style="width: 20%">Line Item Display
                                <asp:Label ID="lableddlLineItemDisplay" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlLineItemDisplay" runat="server" TabIndex="14">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlLineItemDisplay" runat="server" ControlToValidate="ddlLineItemDisplay"
                                    ValidationGroup="save" ErrorMessage="Line Item Display cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Line Item Display cannot be blank.' />" />
                            </td>--%>
                            <%--S4HanaGLDT07122021--%>

                             <%--S4HanaGLDT07122021--%>
                            <td class="leftTD" style="width: 20%">Clearing Spec. to Ledger GPS
                                <asp:Label ID="lableddlClearSpectoLedgerGPS" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlClearSpectoLedgerGPS" runat="server" TabIndex="14">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlClearSpectoLedgerGPS" runat="server" ControlToValidate="ddlClearSpectoLedgerGPS"
                                    ValidationGroup="save" ErrorMessage="Clearing Spec. to Ledger GPS cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Clearing Spec. to Ledger GPS cannot be blank.' />" />
                            </td>
                            <%--S4HanaGLDT07122021--%>

                            <td class="leftTD" style="width: 20%">Reason For Creation
                                <asp:Label ID="lableddlReason" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <%--<asp:DropDownList ID="ddlReason" runat="server" AppendDataBoundItems="false" TabIndex="15">
                                    <asp:ListItem Text="Select" Value="" />
                                    <asp:ListItem Text="Process Change" Value="PC" />
                                    <asp:ListItem Text="MOC/Specification/Dimension change" Value="MOC" />
                                    <asp:ListItem Text="One time requirement code" Value="OTR" />
                                    <asp:ListItem Text="No harmonization of code" Value="NHC" />
                                    <asp:ListItem Text="First time purchasing of item" Value="FTP" />
                                    <asp:ListItem Text="Common item Revenue,Consumable" Value="CRC" />
                                    <asp:ListItem Text="For availing excise benefit" Value="AEB" />
                                    <asp:ListItem Text="New Creation" Value="NC" />
                                </asp:DropDownList>--%>

                                <asp:TextBox ID="txtReason" runat="server" CssClass="textbox" />

                                <asp:RequiredFieldValidator ID="reqtxtReason" runat="server" ControlToValidate="txtReason"
                                    ValidationGroup="save" ErrorMessage="Reason cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Reason cannot be blank.' />" />
                            </td>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Remarks
                                <asp:Label ID="labletxtRemarks" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="textarea" TextMode="MultiLine" TabIndex="16" Columns="100" Rows="3" />
                                <asp:RequiredFieldValidator ID="reqtxtRemarks" runat="server" ControlToValidate="txtRemarks"
                                    ValidationGroup="save" ErrorMessage="Remarks cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Remarks cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td class="leftTD" align="left" colspan="4">
                                <b>Attach Documents (Image/PDF Files Only)</b>
                            </td>
                        </tr>
                        <tr>
                            <td class="rigthTD" align="left" colspan="2" valign="top">
                                <div>
                                    <asp:FileUpload ID="file_upload" class="multi" runat="server" TabIndex="17" />
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

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnPrevious" runat="server" ValidationGroup="save" Text="Back" UseSubmitBehavior="false"
                                    TabIndex="18" CssClass="button" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="save" Text="Save" CssClass="button"
                                    UseSubmitBehavior="true" TabIndex="19" OnClick="btnSave_Click" />
                                <asp:Button ID="btnNext" runat="server" ValidationGroup="save" Text="Save & Next"
                                    UseSubmitBehavior="true" TabIndex="20" CssClass="button"
                                    Width="120px" OnClick="btnNext_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="save" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <%--<asp:Label ID="lblSectionId" runat="server" Text="92" Visible="false" />--%>
    <asp:Label ID="lblSectionId" runat="server" Text="<%$appSettings:SECGLN %>" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblCustomerGeneralId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />

</asp:Content>

