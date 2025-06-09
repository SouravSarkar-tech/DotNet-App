<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="MaterialBlock.aspx.cs" Inherits="Transaction_MaterialBlock" %>

<%@ Register Src="~/Transaction/UserControl/ExcelUpload.ascx" TagPrefix="uc" TagName="ExcelUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showModalPopupViaClient() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialAccounting" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="trHeading" align="center" colspan="2">
                        Material Block/Unblock
                    </td>
                </tr>
                <tr>
                    <td class="tdSpace" colspan="2">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                DataKeyNames="Material_Block_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                GridLines="Both">
                                <RowStyle CssClass="light-gray" HorizontalAlign="Left" />
                                <HeaderStyle CssClass="gridHeader" />
                                <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Left" />
                                <PagerStyle CssClass="PagerStyle" />
                                <Columns>
                                    <%--<asp:BoundField HeaderText="Plants" DataField="Plant" />--%>
                                    <asp:TemplateField HeaderText="Material Data">
                                        <ItemTemplate>
                                            <strong>Material&nbsp;:</strong>
                                            <asp:Label ID="lblMaterialNumber" runat="server" Text='<%# Eval("Material_Number") %>'></asp:Label>
                                            <br />
                                            <strong>Material Type&nbsp;:</strong>
                                            <asp:Label ID="lblMaterialType" runat="server" Text='<%# Eval("Material_Type_Name") %>'></asp:Label>
                                            <br />
                                            <strong>Material Description&nbsp;:</strong>
                                            <asp:Label ID="lblMaterialShortDescription" runat="server" Text='<%# Eval("Material_Short_Description") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Blocking Level">
                                        <ItemTemplate>
                                            <strong>Blocking Level&nbsp;:</strong>
                                            <asp:Label ID="lblBlockingLevel" runat="server" Text='<%# Eval("Blocking_Level_Name") %>'></asp:Label>
                                            <br />
                                            <strong>Remarks&nbsp;:</strong>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Org. Data">
                                        <ItemTemplate>
                                            <strong>Plant&nbsp;:</strong>
                                            <asp:Label ID="lblPlant" runat="server" Text='<%# Eval("Plant") %>'></asp:Label>
                                            <br />
                                            <strong>Sales Organization&nbsp;:</strong>
                                            <asp:Label ID="lblSalesOrganization" runat="server" Text='<%# Eval("Sales_Organization") %>'></asp:Label>
                                            <br />
                                            <strong>Distribution Channel&nbsp;:</strong>
                                            <asp:Label ID="lblDistribution_Channel_ID" runat="server" Text='<%# Eval("Distribution_Channel") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCopy" runat="server" Text="Copy" OnClick="lnkCopy_Click" /><br />
                                            <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" /><br />
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="lnkDelete_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="tdSpace" colspan="2">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" colspan="2">
                        <asp:Panel ID="pnlData" runat="server">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4" align="right">
                                        <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" width="20%">
                                        Material Code
                                        <asp:Label ID="labletxtMaterialCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtMaterialCode" runat="server" CssClass="textbox" MaxLength="10"
                                            AutoPostBack="true" TabIndex="1" Width="180" OnTextChanged="txtMaterialCode_TextChanged" />
                                        <asp:RequiredFieldValidator ID="reqtxtMaterialCode" runat="server" ControlToValidate="txtMaterialCode"
                                            ValidationGroup="Block" ErrorMessage="Material Code cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtMaterialCode" runat="server" ControlToValidate="txtMaterialCode"
                                            ValidationGroup="Block" ErrorMessage="Material Code Invalid." SetFocusOnError="true"
                                            ValidationExpression="^[\d]{6,10}$" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code Invalid.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Material Name
                                        <asp:Label ID="labletxtMaterialName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <br />
                                        <span style="color: Red; font-size: x-small; font-weight: normal">(As mentioned in SAP)</span>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtMaterialName" runat="server" CssClass="textbox" MaxLength="70"
                                            Width="210" TabIndex="2" />
                                        <asp:RequiredFieldValidator ID="reqtxtMaterialName" runat="server" ControlToValidate="txtMaterialName"
                                            ValidationGroup="Block" ErrorMessage="Material Name cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Name cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" width="20%">
                                        Material Type
                                        <asp:Label ID="lableddlMaterialAccGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlMaterialAccGrp" runat="server" AppendDataBoundItems="false"
                                            Enabled="false" TabIndex="3">
                                            <asp:ListItem Text="Select" Value="0" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlMaterialAccGrp" runat="server" ControlToValidate="ddlMaterialAccGrp"
                                            ValidationGroup="Block" ErrorMessage="Select Material Type." SetFocusOnError="true"
                                            InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Material Type.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Plant
                                        <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            TabIndex="4" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                            ValidationGroup="Block" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Block/Unblock Level
                                        <asp:Label ID="lablerdlBlockValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" colspan="3">
                                        <asp:RadioButtonList ID="rdlBlockValue" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" TabIndex="5"
                                            OnSelectedIndexChanged="rdlBlockValue_SelectedIndexChanged">
                                            <asp:ListItem Text="Complete Block" Value="M" />
                                            <asp:ListItem Text="Plant Block" Value="P" />
                                            <asp:ListItem Text="Purchase Block" Value="U" />
                                            <asp:ListItem Text="Sales Block" Value="S" />
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="reqrdlBlockValue" runat="server" ControlToValidate="rdlBlockValue"
                                            ValidationGroup="Block" ErrorMessage="Valuation Type cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Type cannot be blank.' />" />
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
                                        <asp:DropDownList ID="ddlSalesOrginization" runat="server" TabIndex="6" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSalesOrginization_SelectedIndexChanged">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlSalesOrginization" runat="server" ControlToValidate="ddlSalesOrginization"
                                            ValidationGroup="Block" ErrorMessage="Sales Organization cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Distribution Channel
                                        <asp:Label ID="lableddlDistributionChannel" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlDistributionChannel" runat="server" TabIndex="7">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlDistributionChannel" runat="server" ControlToValidate="ddlDistributionChannel"
                                            ValidationGroup="Block" ErrorMessage="Distribution Channel cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Remarks
                                        <asp:Label ID="labletxtRemarks" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" colspan="3">
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="textarea" TextMode="MultiLine"
                                            TabIndex="8" Columns="100" Rows="3" />
                                        <asp:RequiredFieldValidator ID="reqtxtRemarks" runat="server" ControlToValidate="txtRemarks"
                                            ValidationGroup="Block" ErrorMessage="Remarks cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Remarks cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr id="trButton" runat="server" visible="false">
                                    <td class="centerTD" colspan="4">
                                        <asp:Button ID="btnPrevious" runat="server" ValidationGroup="Block" Text="Back" TabIndex="9"
                                            UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="Block" Text="Save" UseSubmitBehavior="true"
                                            TabIndex="10" CssClass="button" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnNext" runat="server" Text="Proceed to Submit" TabIndex="11" CssClass="button"
                                            OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Block" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMaterialBlockId" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="59" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <br />
    <br />
    <div align="left" style="width: 98%">
        <uc:ExcelUpload ID="ExcelUpload1" runat="server" />
    </div>
</asp:Content>
