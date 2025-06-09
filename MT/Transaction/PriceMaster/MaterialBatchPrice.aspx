<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/PriceMaster/PriceMaster.master" AutoEventWireup="true" CodeFile="MaterialBatchPrice.aspx.cs" Inherits="Transaction_PriceMaster_MaterialBatchPrice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        <div style="overflow-y: auto; width: 1100px">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="trHeading" align="center" colspan="2">PRICE MASTER CREATION FORM
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" colspan="2">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="tdSpace" colspan="2"></td>
                            </tr>


                            <tr>

                                <td align="left" valign="top" colspan="2">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true">

                                        <Columns>
                                            <asp:BoundField DataField="RowNumber" HeaderText="Row Number" Visible="false" />
                                            <asp:TemplateField HeaderText="ID" ItemStyle-Width="0" Visible="false">

                                                <ItemTemplate>

                                                    <%--<asp:TextBox ID="txtID" runat="server" Text='<%# Eval("ID") %>' Enabled="false" />--%>
                                                    <asp:Literal ID="txtID" runat="server" Text='<%# Eval("ID") %>'></asp:Literal>

                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material Code" ItemStyle-Width="150">

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtMaterial_Code" runat="server" Text='<%# Eval("Material_Code") %>' OnTextChanged="txtMaterial_Code_TextChanged" AutoPostBack="true" />
                                                    <asp:RequiredFieldValidator ID="reqtxtMaterial_Code" runat="server" ControlToValidate="txtMaterial_Code"
                                                        ValidationGroup="change" ErrorMessage="Material Code cannot be blank." SetFocusOnError="true"
                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code cannot be blank.' />" />
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />

                                                <FooterTemplate>

                                                    <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click" />

                                                </FooterTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material Desc" ItemStyle-Width="150">

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtMaterial_Desc" runat="server" Text='<%# Eval("Material_Desc") %>' Enabled="false" />

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Group" ItemStyle-Width="30">

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtMaterial_Group" runat="server" Text='<%# Eval("Material_Group") %>' Enabled="false" />

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch" ItemStyle-Width="30">

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtBatch" runat="server" Text='<%# Eval("Batch") %>' MaxLength="10" />

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ZMRP" ItemStyle-Width="30">

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtZMRP" runat="server" Text='<%# Eval("ZMRP") %>' />
                                                    <asp:RequiredFieldValidator ID="reqtxtZMRP" runat="server" ControlToValidate="txtZMRP"
                                                        ValidationGroup="change" ErrorMessage="Material Code cannot be blank." SetFocusOnError="true"
                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='ZMRP cannot be blank.' />" />

                                                    <asp:RegularExpressionValidator ID="regtxtZMRP" runat="server" ControlToValidate="txtZMRP" Display="Dynamic" ErrorMessage="Only Decimals With Precision Less Than 2."
                                                        ValidationExpression="^\d+(\.\d{1,2})?$" ValidationGroup="save" Text="<img src='../../images/Error.png' title='Only Decimals With Precision Less Than 2.' />">
                                                    </asp:RegularExpressionValidator>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ZTRP" ItemStyle-Width="30">

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtZTRP" runat="server" Text='<%# Eval("ZTRP") %>' />
                                                    <asp:RegularExpressionValidator ID="reqtxtZTRP" runat="server" ControlToValidate="txtZTRP" Display="Dynamic" ErrorMessage="Only Decimals With Precision Less Than 2."
                                                        ValidationExpression="^\d+(\.\d{1,2})?$" ValidationGroup="save" Text="<img src='../../images/Error.png' title='Only Decimals With Precision Less Than 2.' />">
                                                    </asp:RegularExpressionValidator>

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ZSPL" ItemStyle-Width="30">

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtZSPL" runat="server" Text='<%# Eval("ZSPL") %>' />
                                                    <asp:RegularExpressionValidator ID="reqtxtZSPL" runat="server" ControlToValidate="txtZSPL" Display="Dynamic" ErrorMessage="Only Decimals With Precision Less Than 2."
                                                        ValidationExpression="^\d+(\.\d{1,2})?$" ValidationGroup="save" Text="<img src='../../images/Error.png' title='Only Decimals With Precision Less Than 2.' />">
                                                    </asp:RegularExpressionValidator>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit" ItemStyle-Width="30">

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtUnit" runat="server" Text='<%# Eval("Unit") %>' Enabled="false" />

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Division" ItemStyle-Width="30">

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtDivision" runat="server" Text='<%# Eval("Division") %>' Enabled="false" />

                                                </ItemTemplate>




                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Effective Date" ItemStyle-Width="30">

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtdEffectivedate" runat="server" Text='<%# Eval("dEffectivedate") %>' />
                                                    <%-- <asp:RegularExpressionValidator ID="reqdefectdate" runat="server" ControlToValidate="defectdate" Display="Dynamic" ErrorMessage="Enter valid date."
                                                        ValidationExpression="^\d+(\.\d{1,2})?$" ValidationGroup="save" Text="<img src='../../images/Error.png' title='Enter valid date.' />">
                                                    </asp:RegularExpressionValidator>--%>
                                                    <asp:RegularExpressionValidator ID="regdefectdate" runat="server" ControlToValidate="txtdEffectivedate"
                                                        ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                        Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                        ToolTip="Enter Valid Date in dd/mm/yyyy format."
                                                        AlternateText="Enter Valid Date in dd/mm/yyyy format."
                                                        placeholder="mm/dd/yyyy"
                                                        ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1}))|([0-9]{2}))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Save Data">
                                                <ItemTemplate>
                                                    <%--<asp:Button ID="btnViewmore"
                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>
            "
                                                        CommandName="More" runat="server" Text="View Detail" />--%>
                                                    <asp:Button ID="btnSaveRow" runat="server" OnClick="btnSaveRow_Click" Text="Save Row" ValidationGroup="save" />
                                                </ItemTemplate>

                                                <%--<FooterStyle HorizontalAlign="Right" />

                                                <FooterTemplate>

                                                    <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click" />

                                                </FooterTemplate>--%>

                                            </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>
                                </td>


                            </tr>

                            <%-- <tr>
                <td class="tdSpace" colspan="4"></td>
            </tr>
            <tr id="trButton" runat="server" visible="false">
                <td class="centerTD" colspan="4">
                    <asp:Button ID="btnPrevious" runat="server" ValidationGroup="save" Text="Back" UseSubmitBehavior="false"
                        TabIndex="10" CssClass="button" />
                    <asp:Button ID="btnSave" runat="server" ValidationGroup="save" Text="Save" CssClass="button"
                        UseSubmitBehavior="true" TabIndex="11" OnClick="btnSave_Click" />
                    <asp:Button ID="btnNext" runat="server" ValidationGroup="save" Text="Save & Next"
                        UseSubmitBehavior="true" TabIndex="12" CssClass="button"
                        Width="120px" />
                </td>
            </tr>--%>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="tdSpace" colspan="4"></td>
            </tr>
            <tr id="trButton" runat="server" visible="false">
                <td class="centerTD" colspan="4">
                    <asp:Button ID="btnPrevious" runat="server" ValidationGroup="save" Text="Back" UseSubmitBehavior="false"
                        TabIndex="10" CssClass="button" />
                    <%--OnClick="btnPrevious_Click"--%>
                    <asp:Button ID="btnSave" runat="server" ValidationGroup="save" Text="Save" CssClass="button"
                        UseSubmitBehavior="true" TabIndex="11" OnClick="btnSave_Click" />
                    <%--OnClick="btnSave_Click"--%>
                    <asp:Button ID="btnNext" runat="server" ValidationGroup="save" Text="Save & Next"
                        UseSubmitBehavior="true" TabIndex="12" CssClass="button"
                        Width="120px" />
                    <%--OnClick="btnNext_Click"--%>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="save" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="97" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblCustomerGeneralId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />

</asp:Content>

