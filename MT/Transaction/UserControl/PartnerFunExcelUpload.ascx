<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PartnerFunExcelUpload.ascx.cs" Inherits="Transaction_UserControl_PartnerFunExcelUpload" %>
<div style="width: 100%" class="leftTD">
    Import Data
</div>
<div align="right">
    <asp:HyperLink ID="hlImportFormat" runat="server" Text="Partner Function Excel Format" Target="_blank"
        NavigateUrl="~/Transaction/Vendor/UploadFormat/Partner Function Change.xlsx"></asp:HyperLink>
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
    <div style="background-color: White; padding: 2px 2px 2px 2px; overflow: scroll; width: 1100px; height: 550px">
        <asp:Panel ID="pnlTitleI" runat="server" Style="cursor: move; background-color: Black; border: solid 1px Gray; color: Black">
            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                <span class="ui-dialog-title">Import Vendor Partner Function Data</span>
            </div>
        </asp:Panel>
        <table border="0" cellpadding="0" cellspacing="1" width="100%">
            <tr>
                <td class="tdSpace">
                    <asp:GridView ID="grvData1" runat="server" OnDataBound="grvData1_DataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Vendor code" HeaderText="Vendor Code" />
                            <asp:BoundField DataField="Vendor Description" HeaderText="Vendor Description" />
                            <asp:TemplateField HeaderText="Company Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Eval("Company") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Purchase Org.">
                                <ItemTemplate>
                                    <asp:Label ID="lblPurchaseOrg" runat="server" Text='<%# Eval("Purch Org") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Partner Fun.">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartnerFun" runat="server" Text='<%# Eval("Partner Function") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Vendor code to be link" HeaderText="Vendor code to be link" />
                            <asp:BoundField DataField="Vendor Name" HeaderText="Vendor Name" /> 
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Panel ID="pnlMsg" runat="server">
                                        <asp:Label ID="lblMsg" runat="server" />
                                    </asp:Panel>
                                    <asp:TextBox ID="txtVendorCode" runat="server" CssClass="textbox" MaxLength="10" Visible="false"
                                        Text='<%# Eval("Vendor code") %>' AutoPostBack="true" TabIndex="1" Width="180" />
                                    <asp:TextBox ID="txtVendorName" runat="server" CssClass="textbox" MaxLength="70" Visible="false"
                                        Text='<%# Eval("Vendor Description") %>' Width="210" TabIndex="2" />
                                    <asp:DropDownList ID="ddlVendorAccGrp" runat="server" AppendDataBoundItems="false" Visible="false"
                                        Enabled="false" TabIndex="4">
                                        <asp:ListItem Text="Select" Value="0" />
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlCompanyCode" runat="server" AppendDataBoundItems="false" AutoPostBack="true" Visible="false"
                                        TabIndex="1">
                                        <asp:ListItem Text="Select" Value="" />
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlPurchaseOrg" runat="server" TabIndex="5" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlPartnerFun" runat="server" AppendDataBoundItems="false" TabIndex="6" Visible="false">
                                        <asp:ListItem Text="Select" Value="0" />
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtLinkVendorCode" runat="server" CssClass="textbox" Width="210" TabIndex="7" Visible="false"
                                        Text='<%# Eval("Vendor code to be link") %>' />
                                    <asp:TextBox ID="txtLinkVendorName" runat="server" CssClass="textbox" MaxLength="70" Width="210" Visible="false"
                                        Text='<%# Eval("Vendor Name") %>' TabIndex="8" /> 
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
