<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/BOMRecipe/BOMRecipeMasterPage.master"
    AutoEventWireup="true" CodeFile="BOMRecipeBlock.aspx.cs" Inherits="Transaction_BOMRecipe_BOMRecipeBlock" %>

<%@ Register Src="~/Transaction/UserControl/BOMRecipeExcelUpload.ascx" TagPrefix="uc" TagName="ExcelUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showModalPopupViaClient() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
        function IsNumberNoDecimal() {
            if (!(event.keyCode == 13) && !(event.keyCode == 32) && !(event.keyCode == 35) && !(event.keyCode >= 48 && event.keyCode <= 57))
                return false;
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
                        BOM Recipe Block/Unblock
                    </td>
                </tr>
                <tr>
                    <td class="tdSpace" colspan="2">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                DataKeyNames="BOMRecipe_Block_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                GridLines="Both">
                                <RowStyle CssClass="light-gray" HorizontalAlign="Left" />
                                <HeaderStyle CssClass="gridHeader" />
                                <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Left" />
                                <PagerStyle CssClass="PagerStyle" />
                                <Columns>                                    
                                    <asp:TemplateField HeaderText="Material Data">
                                        <ItemTemplate>
                                            <strong>Material&nbsp;:</strong>
                                            <asp:Label ID="lblMaterialNumber" runat="server" Text='<%# Eval("Material_Number") %>'></asp:Label>
                                            <br />
                                            <strong>Plant&nbsp;:</strong>
                                            <asp:Label ID="lblPlant" runat="server" Text='<%# Eval("Plant") %>'></asp:Label>
                                            <br />
                                            <strong>Remarks&nbsp;:</strong>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Recipe Data">
                                        <ItemTemplate>
                                            <strong>Recipe Group&nbsp;:</strong>
                                            <asp:Label ID="lblRecipeGrp" runat="server" Text='<%# Eval("Recipe_Group") %>'></asp:Label>
                                            <br />
                                            <strong>Status&nbsp;:</strong>
                                            <asp:Label ID="lblRStatus" runat="server" Text='<%# Eval("StatusDesc") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BOM Data">
                                        <ItemTemplate>
                                            <strong>Alternative BOM&nbsp;:</strong>
                                            <asp:Label ID="lblAltBOM" runat="server" Text='<%# Eval("AlternativeBOM") %>'></asp:Label>
                                            <br />
                                            <strong>BOM Status&nbsp;:</strong>
                                            <asp:Label ID="lblBOMStatus" runat="server" Text='<%# Eval("BOMStatusDesc") %>'></asp:Label>
                                            <br />                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prod. Version Data">
                                        <ItemTemplate>
                                            <strong>Prod. Version&nbsp;:</strong>
                                            <asp:Label ID="lblProdVer" runat="server" Text='<%# Eval("ProdVersionNo") %>'></asp:Label>
                                            <br />
                                            <strong>Lock&nbsp;:</strong>
                                            <asp:Label ID="lblLock" runat="server" Text='<%# Eval("LockDesc") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCopy" runat="server" Text="Copy" OnClick="lnkCopy_Click"/><br />  <%----%>
                                            <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click"/><br />   <%----%>
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="lnkDelete_Click"/> <%-- --%>
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
                                        <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>   <%--OnClick="lnlAddDetails_Click"--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" width="20%">
                                        Material&nbsp; Code
                                        <asp:Label ID="labletxtMaterialCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtMaterialCode" runat="server" CssClass="textbox" MaxLength="10"
                                            AutoPostBack="true" TabIndex="1" Width="180" onkeypress="return IsNumberNoDecimal();" onkeydown="return (event.keyCode!=13);" />
                                            <%--OnTextChanged="txtMaterialCode_TextChanged"--%>
                                        <asp:RequiredFieldValidator ID="reqtxtMaterialCode" runat="server" ControlToValidate="txtMaterialCode"
                                            ValidationGroup="Block" ErrorMessage="Material Code cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtMaterialCode" runat="server" ControlToValidate="txtMaterialCode"
                                            ValidationGroup="Block" ErrorMessage="Material Code Invalid." SetFocusOnError="true"
                                            ValidationExpression="^[\d]{6}$" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code Invalid.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Plant
                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" TabIndex="4" Enabled ="false">
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
                                    <td align="right" class="leftTD" style="width: 20%">
                                        Recipe&nbsp; Group
                                        <asp:Label ID="labelRecipeGroup" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtRecipeGroup" runat="server" CssClass="textbox" Width="161px" AutoPostBack = "true"
                                            MaxLength="8" onkeypress="return IsNumberNoDecimal();" OnTextChanged = "txtRecipeGroup_TextChanged"/>
                                        <asp:RegularExpressionValidator ID="regtxtRecipeGroup" runat="server" ControlToValidate="txtRecipeGroup"
                                            ValidationGroup="Block" ErrorMessage="Invalid Recipe group." SetFocusOnError="true"
                                            ValidationExpression="^[\d]{8}$" Display="Dynamic" Text="<img src='../../images/Error.png' title=Invalid Recipe group.' />" />
                                    </td>
                                    <td align="right" class="leftTD" style="width: 20%">
                                        Status
                                        <asp:Label ID="lableddlRStatus" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlRStatus" runat="server" CssClass="dropdownlist" Width="150px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlRStatus" runat="server" ControlToValidate="ddlRStatus"
                                            ValidationGroup="Block" ErrorMessage="Status cannot be blank." SetFocusOnError="true"
                                            Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Status cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="leftTD">
                                        Alternative BOM
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtAltBOM" runat="server" Width="70px" CssClass="textbox" onkeypress="return IsNumberNoDecimal();"
                                            OnTextChanged = "txtAltBOM_TextChanged" AutoPostBack = "true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqtxtAltBOM" runat="server" ControlToValidate="txtAltBOM"
                                            ValidationGroup="Block" ErrorMessage="Alternative BOM cannot be blank." Enabled="false"
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alternative BOM cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtAltBOM" runat="server" ControlToValidate="txtAltBOM"
                                            ValidationGroup="Block" ErrorMessage="Invalid Alternative BOM." SetFocusOnError="true"
                                            ValidationExpression="^[\d]{1,2}$" Display="Dynamic" Text="<img src='../../images/Error.png' title=Invalid Alternative BOM.' />" />
                                    </td>
                                    <td align="right" class="leftTD">
                                        BOM Status
                                        <asp:Label ID="labelddlBOMStatus" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlBOMStatus" runat="server" CssClass="dropdownlist">
                                            <asp:ListItem Text="Select" Value="" />
                                            <asp:ListItem Text="01-Active" Value="01" Selected="True" />
                                            <asp:ListItem Text="02-Inactive" Value="02" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlBOMStatus" runat="server" ControlToValidate="ddlBOMStatus"
                                            ValidationGroup="Block" ErrorMessage="BOM Status cannot be blank." Enabled="false"
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='BOM Status cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Production Version
                                        <%--<asp:Label ID="labeltxtProdVersion" runat="server" ForeColor="Red" Text="*" Visible = "false"></asp:Label>--%>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtProdVersion" runat="server" CssClass="textbox" Width="50px" MaxLength="4" Enable = "false" />
                                        <%--<asp:TextBox ID="txtProdVersion" runat="server" CssClass="textbox" Width="50px" MaxLength="4"
                                            onkeypress="return IsNumberNoDecimal();" OnTextChanged = "txtProdVersion_TextChanged" AutoPostBack = "true"/>
                                        <asp:RequiredFieldValidator ID="reqtxtProdVersion" runat="server" ControlToValidate="txtProdVersion"
                                            ValidationGroup="Block" ErrorMessage="Production version cannot be blank." SetFocusOnError="true"
                                            Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Production version cannot be blank' />" />
                                        <asp:RegularExpressionValidator ID="regtxtProdVersion" runat="server" ControlToValidate="txtProdVersion"
                                            ValidationGroup="Block" ErrorMessage="Invalid Prod version." SetFocusOnError="true"
                                            ValidationExpression="^[\d]{1,4}$" Display="Dynamic" Text="<img src='../../images/Error.png' title=Invalid Prod version.' />" />--%>
                                    </td>
                                    <td class="leftTD">
                                        Lock
                                        <asp:Label ID="labelddlLock" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlLock" runat="server" AppendDataBoundItems="True" Width="150px"
                                            CssClass="dropdownlist">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="reqddlLock" runat="server" ControlToValidate="ddlLock"
                                            ValidationGroup="Block" ErrorMessage="Lock Status cannot be blank." SetFocusOnError="true" InitialValue = ""
                                            Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Lock Status cannot be blank' />" />  --%>                                      
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
                                            UseSubmitBehavior="false" CssClass="button"  OnClick="btnPrevious_Click"/>
                                            <%----%>
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="Block" Text="Save" UseSubmitBehavior="true"
                                            TabIndex="10" CssClass="button" OnClick="btnSave_Click"/>
                                             <%-- --%>
                                        <asp:Button ID="btnNext" runat="server" Text="Proceed to Submit" TabIndex="11" CssClass="button"
                                           Width="120px" UseSubmitBehavior="true" OnClick="btnNext_Click"/>
                                            <%-- --%>
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
    <asp:Label ID="lblBOMRecipeBlockId" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="81" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <br />
    <br />
    <div align="left" style="width: 98%">
        <uc:ExcelUpload ID="ExcelUpload1" runat="server" />
    </div>
</asp:Content>
