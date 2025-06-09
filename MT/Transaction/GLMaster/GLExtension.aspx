<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/GLMaster/GLMasterPage.master" AutoEventWireup="true" CodeFile="GLExtension.aspx.cs" Inherits="Transaction_GLMaster_GLExtension" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../js/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../../stylesheet/gridviewScroll.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlAddNew" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Extension Data
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <asp:Panel ID="pnlGrid" runat="server">
                                <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                    DataKeyNames="GL_Extension_Id,GLGroup" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                    GridLines="Both">
                                    <RowStyle CssClass="light-gray" HorizontalAlign="Left" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Left" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="GL Data">
                                            <ItemTemplate>
                                                <strong>GL Code&nbsp;:</strong>
                                                <asp:Label ID="lblGLCode" runat="server" Text='<%# Eval("GL_Code") %>'></asp:Label>
                                                <br />
                                                <strong>Company Code&nbsp;:</strong>
                                                <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Eval("Company_Name") %>'></asp:Label>
                                                <br />
                                                <strong>Reference Company Code&nbsp;:</strong>
                                                <asp:Label ID="lblRefCompanyCode" runat="server" Text='<%# Eval("Ref_Company_Code") %>'></asp:Label>
                                                <br />
                                                <strong>Reason for Extension&nbsp;:</strong>
                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                                <br />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkCopy" runat="server" Text="Edit" OnClick="lnkCopy_Click" /><br />
                                                <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" CausesValidation="false" /><br />
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="lnkDelete_Click"/>
                                                <%--  OnClientClick="return !(confirm('This will delete the record. Abort?'));" --%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2" runat = "server" id = "trData">
                            <asp:Panel ID="pnlData" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4" align="right">
                                            <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click" Visible = "false"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">
                                            GL Code
                                            <asp:Label ID="labletxtMaterialCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtGLCode" runat="server" CssClass="textbox" MaxLength="8"
                                                AutoPostBack="true" TabIndex="1" Width="180" OnTextChanged="txtGLCode_TextChanged"/>
                                            <asp:RequiredFieldValidator ID="reqtxtGLCode" runat="server" ControlToValidate="txtGLCode"
                                                ValidationGroup="Extn" ErrorMessage="GL Code cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='GL Code cannot be blank.' />" />
                                            <%--<asp:RegularExpressionValidator ID="regtxtMaterialCode" runat="server" ControlToValidate="txtMaterialCode"
                                                ValidationGroup="Extn" ErrorMessage="Material Code Invalid." SetFocusOnError="true"
                                                ValidationExpression="^[\d]{6,10}$" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code Invalid.' />" />--%>
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Company Code
                                            <asp:Label ID="labletxtMaterialDescription" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <br />
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="false" TabIndex="2">
                                                <asp:ListItem Text="Select" Value="0" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlCompany" runat="server" ControlToValidate="ddlCompany"
                                                ValidationGroup="Extn" ErrorMessage="Company Code cannot be blank." SetFocusOnError="true"
                                               InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Company Code cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%; display:none">
                                            Acc Group
                                            <asp:Label ID="lableddlGLAccGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <br />
                                        </td>
                                        <td class="rigthTD" style="width: 30%; display:none">
                                            <asp:DropDownList ID="ddlGLAccGrp" runat="server" AppendDataBoundItems="false"
                                                Enabled="false" TabIndex="3">
                                                <asp:ListItem Text="Select" Value="0" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlGLAccGrp" runat="server" ControlToValidate="ddlGLAccGrp"
                                                ValidationGroup="Extn" ErrorMessage="Material Description cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Description cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">
                                            Reference Company Code
                                            <asp:Label ID="lableddlMaterialAccGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtRefCompanyCode" runat="server" CssClass="textbox" MaxLength="70" Width="210" TabIndex="4" />
                                            <asp:RequiredFieldValidator ID="reqtxtRefCompanyCode" runat="server" ControlToValidate="txtRefCompanyCode"
                                                ValidationGroup="Extn" ErrorMessage="Reference Company Code cannot be blank." SetFocusOnError="true"
                                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Reference Company Code cannot be blank.' />" />
                                        </td>

                                        <td class="leftTD">
                                            Reason for Extension
                                            <asp:Label ID="labletxtRemarks" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="textarea" TextMode="MultiLine"
                                                TabIndex="5" Columns="100" Rows="3" />
                                            <asp:RequiredFieldValidator ID="reqtxtRemarks" runat="server" ControlToValidate="txtRemarks"
                                                ValidationGroup="Extn" ErrorMessage="Reason for Extension cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Reason for Extension cannot be blank.' />" />
                                        </td>
                                        
                                    </tr>
                                    
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr id="trButton" runat="server" visible="false">
                        <td class="centerTD" colspan="4">
                            <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="Extn" TabIndex="6"
                                UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                            <asp:Button ID="btnSave" runat="server" ValidationGroup="Extn" Text="Save" CssClass="button"
                                TabIndex="7" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                            <asp:Button ID="btnNext" runat="server" Text="Proceed to Submit" TabIndex="8" CssClass="button"
                                OnClick="btnNext_Click" Width="160px" UseSubmitBehavior="true" Visible="false"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Extn" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblMatExtensionId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
    <%--<asp:Label ID="lblSectionId" runat="server" Text="95" Visible="false" />--%>
    <asp:Label ID="lblSectionId" runat="server" Text="<%$appSettings:SECGEXT %>" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
</asp:Content>

