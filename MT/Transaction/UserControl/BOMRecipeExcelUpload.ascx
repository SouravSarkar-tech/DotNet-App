<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BOMRecipeExcelUpload.ascx.cs" Inherits="Transaction_UserControl_BOMRecipeExcelUpload" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<div style="width: 100%" class="leftTD">
    Import Data</div>
<div align="right">
    <asp:HyperLink ID="hlImportFormat" runat="server" Text="Excel Format" Target="_blank"
        NavigateUrl="~/Transaction/BOMRecipe/UploadFormat/BOMRecipe blocking.xlsx"></asp:HyperLink>
</div>
<asp:FileUpload ID="fileUpload" runat="server" />
<asp:Button ID="Process" runat="server" OnClick="Process_Click" Text="Upload" />
<asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
<asp:Panel ID="pnlMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" />
</asp:Panel>
<act:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="hiddenTargetControlForModalPopup"
    BehaviorID="programmaticModalPopupBehavior" CancelControlID="btnCancel" PopupControlID="pnlAddData"
    BackgroundCssClass="modalBackground" DropShadow="true" PopupDragHandleControlID="pnlTitle" />
<asp:Panel ID="pnlAddData" runat="server" Width="100%">
    <div style="background-color: White; padding: 2px 2px 2px 2px; overflow: scroll;
        width: 1100px; height: 550px">
        <asp:Panel ID="pnlTitle" runat="server" Style="cursor: move; background-color: Black;
            border: solid 1px Gray; color: Black">
            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                <span class="ui-dialog-title">Import Block/Unblock Data</span>
            </div>
        </asp:Panel>
        <table border="0" cellpadding="0" cellspacing="1" width="100%">
            <tr>
                <td class="tdSpace">
                    <asp:GridView ID="grvData" runat="server" OnDataBound="grvData_DataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Material code" HeaderText="Material Code" />                            
                            <asp:TemplateField HeaderText="Plant Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblPlant" runat="server" Text='<%# Eval("Plant") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Recipe Group">
                                <ItemTemplate>
                                    <asp:Label ID="lblRecipeGrp" runat="server" Text='<%# Eval("Recipe Group") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Recipe Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblRStatus" runat="server" Text='<%# Eval("Recipe Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Alt. BOM">
                                <ItemTemplate>
                                    <asp:Label ID="lblAltBOM" runat="server" Text='<%# Eval("Alternative BOM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BOM Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblBOMStatus" runat="server" Text='<%# Eval("BOM Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prod. Version">
                                <ItemTemplate>
                                    <asp:Label ID="lblProdVer" runat="server" Text='<%# Eval("Prod Version") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lock">
                                <ItemTemplate>
                                    <asp:Label ID="lblProdLock" runat="server" Text='<%# Eval("Lock") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Panel ID="pnlMsg" runat="server">
                                        <asp:Label ID="lblMsg" runat="server" />
                                    </asp:Panel>
                                    <asp:TextBox ID="txtMaterialCode" runat="server" CssClass="textbox" MaxLength="10"
                                        Visible="false" Text='<%# Eval("Material code") %>' AutoPostBack="true" TabIndex="1"
                                        Width="180" />                                    
                                    <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                        Visible="false" TabIndex="1">
                                        <asp:ListItem Text="Select" Value="" />
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtRecipeGrp" runat="server" CssClass="textbox" MaxLength="10"
                                        Visible="false" Text='<%# Eval("Recipe Group") %>' AutoPostBack="true" TabIndex="1"
                                        Width="180" />  
                                    <asp:DropDownList ID="ddlRStatus" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                        Visible="false" TabIndex="1">
                                        <asp:ListItem Text="Select" Value="" />
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtAltBOM" runat="server" CssClass="textbox" MaxLength="10"
                                        Visible="false" Text='<%# Eval("Alternative BOM") %>' TabIndex="1" Width="180" />
                                    <asp:DropDownList ID="ddlBOMStatus" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                        Visible="false" TabIndex="1">
                                            <asp:ListItem Text="Select" Value="" />
                                            <asp:ListItem Text="01-Active" Value="01" Selected="True" />
                                            <asp:ListItem Text="02-Inactive" Value="02" />
                                    </asp:DropDownList>
                                     <asp:TextBox ID="txtProdVer" runat="server" CssClass="textbox" MaxLength="10"
                                        Visible="false" Text='<%# Eval("Prod Version") %>' AutoPostBack="true" TabIndex="1"
                                        Width="180" />
                                    <asp:DropDownList ID="ddlLock" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                        Visible="false" TabIndex="1">
                                        <asp:ListItem Text="Select" Value="" />
                                    </asp:DropDownList>                              
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textarea" TextMode="MultiLine"
                                        Visible="false" Text='<%# Eval("Remarks") %>' TabIndex="26" Columns="100" Rows="3" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="centerTD">
                    <asp:Button ID="btnAdd" runat="server" Text="Import Data" CssClass="button" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<br />
<br />
