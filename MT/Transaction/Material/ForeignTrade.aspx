<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="ForeignTrade.aspx.cs" Inherits="Transaction_ForeignTrade" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlAddNew" runat="server">
        <%--  <asp:UpdatePanel ID="UpdMaterialForeignTrade" runat="server">
            <ContentTemplate>--%>
        <asp:Panel ID="pnlMsg" runat="server" Visible="false">
            <asp:Label ID="lblMsg" runat="server" />
        </asp:Panel>
        <table border="0" cellpadding="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">
                    Foreign Trade
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
                    <asp:Panel ID="pnlGrid" runat="server">
                        <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                            DataKeyNames="Mat_Foreign_Trade_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                            GridLines="Both">
                            <RowStyle CssClass="light-gray" />
                            <HeaderStyle CssClass="gridHeader" />
                            <AlternatingRowStyle CssClass="gridRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <Columns>
                                <asp:BoundField HeaderText="Plants" DataField="Plant" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" />
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
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Plant
                                <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                    ValidationGroup="FT" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                            </td>
                            <td class="tdSpace" colspan="2" align="right">
                                <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">
                                Sales Organization
                                <asp:Label ID="lableddlSalesOrginization" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlSalesOrginization" runat="server" AppendDataBoundItems="false"
                                    TabIndex="2">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlSalesOrginization" runat="server" ControlToValidate="ddlSalesOrginization"
                                    ValidationGroup="FT" ErrorMessage="Sales Organization cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />
                            </td>
                            <td class="leftTD">
                                Distribution Channel
                                <asp:Label ID="lableddlDistributionChannel" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlDistributionChannel" runat="server" AppendDataBoundItems="false"
                                    TabIndex="3">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlDistributionChannel" runat="server" ControlToValidate="ddlDistributionChannel"
                                    ValidationGroup="FT" ErrorMessage="Distribution Channel cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Country of origin of the material
                                <asp:Label ID="lableddlCountry" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" TabIndex="4"
                                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCountry" runat="server" ControlToValidate="ddlCountry"
                                    ValidationGroup="FT" ErrorMessage="Country of origin of the material cannot be blank."
                                    InitialValue="0" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Country of origin of the material cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Region of origin of material
                                <asp:Label ID="lableddlRegion" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlRegion" runat="server" TabIndex="5">
                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlRegion" runat="server" ControlToValidate="ddlRegion"
                                    ValidationGroup="FT" ErrorMessage="Region cannot be blank." InitialValue="0"
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Region cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Commodity Code/Import Code Number for Foreign Trade
                                <asp:Label ID="labletxtCommodityCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCommodityCode" runat="server" CssClass="textbox" Width="100px"
                                    TabIndex="6" MaxLength="17" />
                                <asp:RequiredFieldValidator ID="reqtxtCommodityCode" runat="server" ControlToValidate="txtCommodityCode"
                                    ValidationGroup="FT" ErrorMessage="Commodity Code/Import Code Number for Foreign Trade cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Commodity Code/Import Code Number for Foreign Trade cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Export/import material group
                                <asp:Label ID="lableddlExport" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlExport" runat="server" AppendDataBoundItems="false" TabIndex="7">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlExport" runat="server" ControlToValidate="ddlExport"
                                    ValidationGroup="FT" ErrorMessage="Export/import material group cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Export/import material group cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Preference indicator in export/import
                                <asp:Label ID="lableddlPreference" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlPreference" runat="server" AppendDataBoundItems="false"
                                    TabIndex="8">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPreference" runat="server" ControlToValidate="ddlPreference"
                                    ValidationGroup="FT" ErrorMessage="Preference indicator in export/import cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Preference indicator in export/import cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Exemption certificate: Indicator for legal control
                                <asp:Label ID="lableddlExemption" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlExemption" runat="server" AppendDataBoundItems="false" TabIndex="9">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlExemption" runat="server" ControlToValidate="ddlExemption"
                                    ValidationGroup="FT" ErrorMessage="Exemption certificate: Indicator for legal control cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Exemption certificate: Indicator for legal control cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr id="trGSTReq" runat="server" visible="false">
                            <td class="leftTD">
                                Whether HSN and GST rate is required or not
                                <asp:Label ID="LabelGSTReq" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlGSTReq" runat="server" AppendDataBoundItems="false" TabIndex="10">
                                    <asp:ListItem Text="Select" Value="" />
                                    <asp:ListItem Text="Yes" Value="Yes" />
                                    <asp:ListItem Text="No" Value="No" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlGSTReq" runat="server" ControlToValidate="ddlGSTReq"
                                    ValidationGroup="FT" ErrorMessage="Kindly select whether HSN and GST rate is required or not."
                                    SetFocusOnError="true" Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kindly select whether HSN and GST rate is required or not.' />" />
                            </td>
                            <td class="tdSpace" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                <%--GST Changes--%>
                                <%--Control code --%>
                                Control code / HSN
                                <%--GST Changes--%>
                                <asp:Label ID="lableddlControlCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" colspan="3">
                                <asp:DropDownList ID="ddlControlCode" runat="server" AppendDataBoundItems="false"
                                    AutoPostBack="true" TabIndex="10" OnSelectedIndexChanged="ddlControlCode_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlControlCode" runat="server" ControlToValidate="ddlControlCode"  
                                    ValidationGroup="FT" ErrorMessage="Control code cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Control code cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Chapter ID
                                <asp:Label ID="lableddlChapter_ID" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" colspan="3">
                                <asp:DropDownList ID="ddlChapter_ID" runat="server" AppendDataBoundItems="false"
                                    TabIndex="10">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlChapter_ID" runat="server" ControlToValidate="ddlChapter_ID"
                                    ValidationGroup="FT" ErrorMessage="Chapter ID cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Chapter ID cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Subcontractors
                                <asp:Label ID="lablechkSubcontractors" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkSubcontractors" runat="server" Text=" Check if applicable."
                                    TabIndex="5" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Material Type
                                <asp:Label ID="lableddlMaterial_Type" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlMaterial_Type" runat="server" AppendDataBoundItems="false"
                                    TabIndex="9">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlMaterial_Type" runat="server" ControlToValidate="ddlMaterial_Type"
                                    ValidationGroup="FT" InitialValue="0" ErrorMessage="Material Type cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Type cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                No of Goods Receipts per Excise Invoice
                                <asp:Label ID="lableddlNo_of_Goods_Receipts_per_Excise_Invoice" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlNo_of_Goods_Receipts_per_Excise_Invoice" runat="server"
                                    AppendDataBoundItems="false" TabIndex="9">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlNo_of_Goods_Receipts_per_Excise_Invoice" runat="server"
                                    ControlToValidate="ddlNo_of_Goods_Receipts_per_Excise_Invoice" ValidationGroup="FT"
                                    InitialValue="0" ErrorMessage="No of Goods Receipts per Excise Invoice cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='No of Goods Receipts per Excise Invoice cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Output Material For ModVat
                                <asp:Label ID="labletxtOutput_Material_For_ModVat" runat="server" ForeColor="Red"
                                    Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtOutput_Material_For_ModVat" runat="server" CssClass="textbox"
                                    Width="100px" TabIndex="6" MaxLength="17" />
                                <asp:RequiredFieldValidator ID="reqtxtOutput_Material_For_ModVat" runat="server"
                                    ControlToValidate="txtOutput_Material_For_ModVat" ValidationGroup="FT" ErrorMessage="Output Material For ModVat cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Output Material For ModVat cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtOutput_Material_For_ModVat" runat="server"
                                    ControlToValidate="txtOutput_Material_For_ModVat" ValidationGroup="FT" ErrorMessage="Output Material For ModVat Invalid."
                                    SetFocusOnError="true" ValidationExpression="^[\d]{6,10}$" Display="Dynamic"
                                    Text="<img src='../../images/Error.png' title='Output Material For ModVat Invalid.' />" />
                            </td>
                        </tr>
                        <%--GST Changes--%>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr id="trGSTRate" runat="server" visible="false">
                            <td class="leftTD">
                                GST rate
                                <asp:Label ID="lableGSTRate" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtGSTRate" runat="server" CssClass="textbox" Width="100px" TabIndex="6"
                                    MaxLength="17" />
                                <asp:RequiredFieldValidator ID="reqtxtGSTRate" runat="server" ControlToValidate="txtGSTRate"
                                    ValidationGroup="FT" ErrorMessage="GST Rate cannot be blank." SetFocusOnError="true"
                                    Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='GST Rate cannot be blank.' />" />
                            </td>
                            <td class="tdSpace" colspan="2">
                            </td>
                        </tr>
                        <%--GST Changes--%>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr id="trRemarks" runat="server" visible="false">
                            <td class="leftTD">
                                Info. relevant to Chapter ID
                                <asp:Label ID="labletxtRemarks" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="textarea" TextMode="MultiLine"
                                    TabIndex="26" Columns="100" Rows="3" />
                                <asp:RequiredFieldValidator ID="reqtxtRemarks" runat="server" ControlToValidate="txtRemarks"
                                    ValidationGroup="FT" ErrorMessage="Remarks cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Remarks cannot be blank.' />" />
                            </td>
                            <td class="tdSpace" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr id="trDocsText" runat="server" visible="false">
                            <td class="leftTD" align="left" colspan="4">
                                <b>Attach Documents (Image/PDF Files Only)</b>
                            </td>
                        </tr>
                        <tr id="trDocs" runat="server" visible="false">
                            <td class="rigthTD" align="left" colspan="2" valign="top">
                                <div>
                                    <asp:FileUpload ID="file_upload" class="multi" runat="server" TabIndex="27" />
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
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnPrevious" runat="server" ValidationGroup="FT" Text="Back" CssClass="button"
                                    TabIndex="11" UseSubmitBehavior="false" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="FT" Text="Save" CssClass="button"
                                    TabIndex="12" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                <asp:Button ID="btnNext" runat="server" ValidationGroup="FT" Text="Save & Next" CssClass="button"
                                    TabIndex="13" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <%--  </ContentTemplate>
        </asp:UpdatePanel>--%>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="FT" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblForeignTradeId" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="7" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
</asp:Content>
