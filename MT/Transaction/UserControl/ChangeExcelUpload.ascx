<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChangeExcelUpload.ascx.cs"
    Inherits="Transaction_UserControl_ChangeExcelUpload" %>
<div style="width: 100%" class="leftTD">
    Import Data</div>
<div align="right">
    <asp:HyperLink ID="hlImportFormat" runat="server" Text="Excel Format" Target="_blank"
        NavigateUrl="~/Transaction/Material/UploadFormat/Material change.xlsx"></asp:HyperLink>
</div>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:FileUpload ID="fileUpload" runat="server" />
<asp:Button ID="Process" runat="server" OnClick="Process_Click" Text="Upload" />
<asp:Button runat="server" ID="hiddenTargetControlForModalPopupI" Style="display: none" />
<asp:Panel ID="pnlMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" />
</asp:Panel>
<act:ModalPopupExtender ID="ModalPopupExtenderI" runat="server" TargetControlID="hiddenTargetControlForModalPopupI"
    BehaviorID="programmaticModalPopupBehaviorI" CancelControlID="btnCancel" PopupControlID="pnlAddDataI"
    BackgroundCssClass="modalBackground" DropShadow="true" PopupDragHandleControlID="pnlTitleI" />
<asp:Panel ID="pnlAddDataI" runat="server" Width="100%">
    <div style="background-color: White; padding: 2px 2px 2px 2px; overflow: scroll;
        width: 1100px; height: 550px">
        <asp:Panel ID="pnlTitleI" runat="server" Style="cursor: move; background-color: Black;
            border: solid 1px Gray; color: Black">
            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                <span class="ui-dialog-title">Import Material Change Data</span>
            </div>
        </asp:Panel>
        <table border="0" cellpadding="0" cellspacing="1" width="100%">
            <tr>
                <td class="tdSpace">
                    <%--<asp:GridView ID="grvData1" runat="server" ></asp:GridView>--%>
                    <asp:GridView ID="grvData1" runat="server" OnDataBound="grvData1_DataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Material code" HeaderText="Material Code" />
                            <asp:BoundField DataField="Material Description" HeaderText="Material Description" />
                            <asp:TemplateField HeaderText="Plant Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblPlant" runat="server" Text='<%# Eval("Plant") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Storage Location">
                                <ItemTemplate>
                                    <asp:Label ID="lblStorageLocation" runat="server" Text='<%# Eval("Storage Loc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Org.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSalesOrg" runat="server" Text='<%# Eval("Sales Org") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Distribution Chnl.">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistributionChannel" runat="server" Text='<%# Eval("Distribution Channel") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section">
                                <ItemTemplate>
                                    <asp:Label ID="lblSection" runat="server" Text='<%# Eval("Section") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Field">
                                <ItemTemplate>
                                    <asp:Label ID="lblField" runat="server" Text='<%# Eval("Field") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Old Value" HeaderText="Old Value" />
                            <asp:BoundField DataField="New Value" HeaderText="New Value" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Panel ID="pnlMsg" runat="server">
                                        <asp:Label ID="lblMsg" runat="server" />
                                    </asp:Panel>
                                    <asp:TextBox ID="txtMaterialCode" runat="server" CssClass="textbox" MaxLength="10" Visible="false"
                                        Text='<%# Eval("Material code") %>' AutoPostBack="true" TabIndex="1" Width="180" />
                                    <asp:TextBox ID="txtMaterialName" runat="server" CssClass="textbox" MaxLength="70" visible="false"
                                        Text='<%# Eval("Material Description") %>' Width="210" TabIndex="2" />
                                    <asp:DropDownList ID="ddlMaterialAccGrp" runat="server" AppendDataBoundItems="false" visible="false"
                                        Enabled="false" TabIndex="4">
                                        <asp:ListItem Text="Select" Value="0" />
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true" visible="false"
                                        TabIndex="1">
                                        <asp:ListItem Text="Select" Value="" />
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlStorageLocation" runat="server" TabIndex="5" AutoPostBack="true"
                                        Visible="false">
                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSalesOrginization" runat="server" TabIndex="5" AutoPostBack="true" visible="false">
                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlDistributionChannel" runat="server" TabIndex="6" Visible="false">
                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="false" AutoPostBack="true" visible="false"
                                        TabIndex="8">
                                        <asp:ListItem Text="Select" Value="0" />
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlField" runat="server" AppendDataBoundItems="false" TabIndex="9" visible="false">
                                        <asp:ListItem Text="Select" Value="0" />
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtOldValue" runat="server" CssClass="textbox" Width="210" TabIndex="10" visible="false"
                                        Text='<%# Eval("Old Value") %>' />
                                    <asp:TextBox ID="txtNewValue" runat="server" CssClass="textbox" MaxLength="70" Width="210" visible="false"
                                        Text='<%# Eval("New Value") %>' TabIndex="11" />
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
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMatPlantGrpId" runat="server" Visible="false" />
</asp:Panel>
