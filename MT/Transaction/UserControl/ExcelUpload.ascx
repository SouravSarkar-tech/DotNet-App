<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExcelUpload.ascx.cs" Inherits="Transaction_UserControl_ExcelUpload" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<div style="width: 100%" class="leftTD">
    Import Data</div>
<div align="right">
    <asp:HyperLink ID="hlImportFormat" runat="server" Text="Excel Format" Target="_blank"
        NavigateUrl="~/Transaction/Material/UploadFormat/Material blocking.xlsx"></asp:HyperLink>
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
                            <asp:BoundField DataField="Material Description" HeaderText="Material Description" />
                            <asp:TemplateField HeaderText="Plant Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblPlant" runat="server" Text='<%# Eval("Plant") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Block Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblBlockType" runat="server" Text='<%# Eval("Block Type") %>'></asp:Label>
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
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Panel ID="pnlMsg" runat="server">
                                        <asp:Label ID="lblMsg" runat="server" />
                                    </asp:Panel>
                                    <asp:TextBox ID="txtMaterialCode" runat="server" CssClass="textbox" MaxLength="10"
                                        Visible="false" Text='<%# Eval("Material code") %>' AutoPostBack="true" TabIndex="1"
                                        Width="180" />
                                    <asp:TextBox ID="txtMaterialName" runat="server" CssClass="textbox" MaxLength="70"
                                        Visible="false" Text='<%# Eval("Material Description") %>' Width="210" TabIndex="2" />
                                    <asp:DropDownList ID="ddlMaterialAccGrp" runat="server" AppendDataBoundItems="false"
                                        Visible="false" Enabled="false" TabIndex="4">
                                        <asp:ListItem Text="Select" Value="0" />
                                    </asp:DropDownList>
                                    <asp:RadioButtonList ID="rdlBlockValue" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                        Visible="false">
                                        <asp:ListItem Text="Complete Block" Value="M" />
                                        <asp:ListItem Text="Plant Block" Value="P" />
                                        <asp:ListItem Text="Purchase Block" Value="U" />
                                        <asp:ListItem Text="Sales Block" Value="S" />
                                    </asp:RadioButtonList>
                                    <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                        Visible="false" TabIndex="1">
                                        <asp:ListItem Text="Select" Value="" />
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSalesOrginization" runat="server" TabIndex="5" AutoPostBack="true"
                                        Visible="false">
                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlDistributionChannel" runat="server" TabIndex="6" Visible="false">
                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
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
