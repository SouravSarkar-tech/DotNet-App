<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/BOMRecipe/BOMRecipeMasterPage.master"
    AutoEventWireup="true" CodeFile="BOMUserFields.aspx.cs" Inherits="Transaction_BOMRecipe_BOMUserFields" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
    <script type="text/javascript">
        function showModalPopupViaClient() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlAddNew" runat="server">

        <div id="divmainPopUp" runat="server" clientidmode="Static">
            <table border="0" cellpadding="0" cellspacing="1" width="100%">

                <tr>
                    <td class="trHeading" align="center" colspan="4">User Fields
                    </td>
                </tr>

                <tr>
                    <td class="tdSpace" colspan="4"></td>
                </tr>

                <tr>
                    <td class="leftTD" width="100%" colspan="4">

                        <asp:TextBox ID="txtNewRow" runat="server" Text="1" MaxLength="3" Width="20px"
                            Enabled="false" CssClass="textbox" Style="display: none;" />
                        <asp:RangeValidator ID="ranvtxtNewRow" runat="server" ValidationGroup="addRowValidation"
                            ControlToValidate="txtNewRow" MaximumValue="20" MinimumValue="1" Type="Integer"
                            ErrorMessage="Enter Numeric Value only (Maximum limit 20)." SetFocusOnError="true"
                            Display="Dynamic"
                            Text="<img src='../../images/Error.png' title='Enter Numeric Value only (Maximum limit 20).' />"></asp:RangeValidator>

                        <asp:Button ID="btnaddRow" runat="server" Text="Add New Row" ValidationGroup="addRowValidation"
                            OnClick="btnaddRow_Click" CssClass="button" UseSubmitBehavior="false" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td class="tdSpace" colspan="4"></td>
                </tr>

                <tr>
                    <td colspan="4">
                        <div style="height: auto; overflow-x: auto; width: 950px;">
                            <asp:GridView ID="grdDetailAdd" runat="server" AutoGenerateColumns="false"
                                DataKeyNames="Pk_BOM_UserFieldsId,sActivity,sFieldkey"
                                Width="1000px" CssClass="GridClass" ShowHeaderWhenEmpty="true" Visible="true"
                                ShowFooter="false" AllowSorting="true"
                                OnRowCommand="grdDetailAdd_RowCommand"
                                OnRowDataBound="grdDetailAdd_RowDataBound">
                                <FooterStyle CssClass="gridFooter" />
                                <RowStyle CssClass="light-gray" />
                                <HeaderStyle CssClass="gridHeader" />
                                <AlternatingRowStyle CssClass="gridRowStyle" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Remove">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Pk_BOM_UserFieldsId") %>' CommandName="D">  
                                                 <img src="../../images/delete.png" alt="Delete" title='Delete' Width="20px"
                                                     OnClientClick="return confirm('Are you certain you want to delete this record?');" /></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPk_BOM_UserFieldsId" runat="server" Text='<%#Eval("Pk_BOM_UserFieldsId") %>'
                                                Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Activity">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlsActivity" runat="server" AppendDataBoundItems="false" Width="70px"
                                                 AutoPostBack="true" OnSelectedIndexChanged="ddlsActivity_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlsActivity" runat="server" ControlToValidate="ddlsActivity"
                                                ValidationGroup="save" ErrorMessage="Select the Activity." SetFocusOnError="true"
                                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select the Activity.' />" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Field key">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlsFieldkey" runat="server" AppendDataBoundItems="false" Width="180px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlsFieldkey" runat="server" ControlToValidate="ddlsFieldkey"
                                                ValidationGroup="save" ErrorMessage="Select the Field key." SetFocusOnError="true"
                                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select the Field key.' />" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Text 1">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsGFText1" runat="server" CssClass="textbox" Text='<%#Eval("sGFText1") %>'
                                                Width="80px" MaxLength="50"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Text 2">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsGFText2" runat="server" CssClass="textbox" Text='<%#Eval("sGFText2") %>'
                                                Width="80px" MaxLength="50"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Text 3">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsGFText3" runat="server" CssClass="textbox" Text='<%#Eval("sGFText3") %>'
                                                Width="80px" MaxLength="50"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Text 4">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsGFText4" runat="server" CssClass="textbox" Text='<%#Eval("sGFText4") %>'
                                                Width="80px" MaxLength="50"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Quantity 1">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsNFQty1" runat="server" CssClass="textbox" Text='<%#Eval("sNFQty1") %>'
                                                Width="80px" MaxLength="7" onkeypress="return IsNumber();" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsQUNIT1" runat="server" CssClass="textbox" Text='<%#Eval("sQUNIT1") %>'
                                                Width="80px" MaxLength="7"  ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Quantity 2">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsNFQty2" runat="server" CssClass="textbox" Text='<%#Eval("sNFQty2") %>'
                                                Width="80px" MaxLength="7" onkeypress="return IsNumber();" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsQUNIT2" runat="server" CssClass="textbox" Text='<%#Eval("sQUNIT2") %>'
                                                Width="80px" MaxLength="7"  ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Value 3">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsNFValue1" runat="server" CssClass="textbox" Text='<%#Eval("sNFValue1") %>'
                                                Width="80px" MaxLength="7" onkeypress="return IsNumber();" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsVUNIT1" runat="server" CssClass="textbox" Text='<%#Eval("sVUNIT1") %>'
                                                Width="80px" MaxLength="7"  ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Value 4">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsNFValue2" runat="server" CssClass="textbox" Text='<%#Eval("sNFValue2") %>'
                                                Width="80px" MaxLength="7" onkeypress="return IsNumber();" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsVUNIT2" runat="server" CssClass="textbox" Text='<%#Eval("sVUNIT2") %>'
                                                Width="80px" MaxLength="7"  ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Date 1">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtdDTdate1" runat="server" CssClass="textbox" Text='<%#Eval("dDTdate1") %>'
                                                Width="80px" MaxLength="20"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Date 2">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtdDTdate2" runat="server" CssClass="textbox" Text='<%#Eval("dDTdate2") %>'
                                                Width="80px" MaxLength="20"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="KX_Scheduling">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPIKX" runat="server" CssClass="chkPIKX" />
                                            <asp:HiddenField ID="hdnPIKX" runat="server" Value='<%#Eval("bCBKX_Sche") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Indicator2">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPIInd" runat="server" CssClass="chkPIInd" />
                                            <asp:HiddenField ID="hdnPIInd" runat="server" Value='<%#Eval("bCBIndicator") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="tdSpace"></td>
                </tr>

                <tr id="trButton" runat="server" visible="false">
                    <td class="centerTD" colspan="2">
                        <asp:Button ID="btnPrevious" runat="server" Text="Back" TabIndex="26"
                            CssClass="button" OnClick="btnPrevious_Click" />
                        <asp:Button ID="btnSave" runat="server" UseSubmitBehavior="true" ValidationGroup="save"
                            Text="Save" CssClass="button" TabIndex="27" OnClick="btnSave_Click" />
                        <asp:Button ID="btnNext" runat="server" Text="Save & Next"
                            TabIndex="28" UseSubmitBehavior="true" CssClass="button" OnClick="btnNext_Click"
                            Width="120px" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:ValidationSummary ID="VaSu" runat="server" ShowMessageBox="true" ShowSummary="false"
            ValidationGroup="addRowValidation" />
        <asp:ValidationSummary ID="VaSu1" runat="server" ShowMessageBox="true" ShowSummary="false"
            ValidationGroup="save" />

        <asp:Label ID="lblUserId" runat="server" Visible="false" />
        <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
        <asp:Label ID="lblMode" runat="server" Visible="false" />
        <asp:Label ID="lblModuleId" runat="server" Visible="false" />
        <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        <asp:Label ID="lblSectionId" runat="server" Text="<%$appSettings:SECUF %>" Visible="false" />

        <asp:Label ID="lblRecipeId" runat="server" Visible="false" />
        <asp:Label ID="lblFlag" runat="server" Text="" Visible="false" />
        <asp:Label ID="lblReqStatus" runat="server" Text="" Visible="false" />
    </asp:Panel>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>



