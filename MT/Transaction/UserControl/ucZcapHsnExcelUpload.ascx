<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucZcapHsnExcelUpload.ascx.cs" Inherits="Transaction_UserControl_ucZcapHsnExcelUpload" %>
<div style="width: 100%" class="leftTD">
    Import Mass Data
</div>
<div align="right">
    <%--  <asp:HyperLink ID="hlImportFormat" runat="server" Text="Excel Format" Target="_blank"
        NavigateUrl="~/Transaction/ZcapHsnMaster/UploadFormat/ZcapHsnMaster.xlsx"></asp:HyperLink>--%>
    <asp:HyperLink ID="hlImportFormat" runat="server" Text="Excel Format" Target="_blank"></asp:HyperLink>
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
                <span class="ui-dialog-title">Import ZCAP/ZPEX+HSN/GST% Data</span>
            </div>
        </asp:Panel>
        <table border="0" cellpadding="0" cellspacing="1" width="100%">
            <tr>
                <td class="tdSpace">
                    <asp:GridView ID="grvData1" runat="server" OnDataBound="grvData1_DataBound"
                        AutoGenerateColumns="false">

                        <Columns>
                            <asp:BoundField DataField="Material Code" HeaderText="Material Code" />
                            <asp:BoundField DataField="Material Name" HeaderText="Material Name" />
                            <asp:BoundField DataField="Supp plant" HeaderText="Supp plant" />
                            <asp:BoundField DataField="Rece plant" HeaderText="Rece plant" />
                            <asp:TemplateField HeaderText="Condintion type">
                                <ItemTemplate>
                                    <asp:Label ID="lblCondintiontype" runat="server" Text='<%# Eval("Condintion type") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Zcap Rate" HeaderText="Zcap Rate" />
                            <asp:BoundField DataField="UOM" HeaderText="UOM" />
                            <asp:BoundField DataField="STO Num" HeaderText="STO/PO No" />
                            <asp:BoundField DataField="HSN Code" HeaderText="HSN Code" />
                            <asp:BoundField DataField="GST Code" HeaderText="GST Code" />
                            <%--<asp:BoundField DataField="GST Code" HeaderText="GST Code" />--%>

                            <asp:TemplateField HeaderText="Is LUT Cond">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsLUTCond" runat="server" Text='<%# Eval("Is LUT Cond") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />


                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Panel ID="pnlMsg" runat="server">
                                        <asp:Label ID="lblMsg" runat="server" />
                                    </asp:Panel>
                                    <asp:TextBox ID="txtsMaterial_Code" runat="server" CssClass="textbox" Text='<%#Eval("Material Code") %>' Visible="false"
                                        Width="80px" MaxLength="7" onkeypress="return IsNumber();" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                    <asp:TextBox ID="txtsMaterial_Name" runat="server" CssClass="textbox" Text='<%#Eval("Material Name") %>' Visible="false"
                                        Width="100px" MaxLength="4"></asp:TextBox>
                                    <asp:TextBox ID="txtsSupp_plant" runat="server" CssClass="textbox" Text='<%#Eval("Supp plant") %>' Visible="false"
                                        Width="70px" MaxLength="4"></asp:TextBox>

                                    <asp:TextBox ID="txtsRece_plant" runat="server" CssClass="textbox" Text='<%#Eval("Rece plant") %>' Visible="false"
                                        Width="70px" MaxLength="4"></asp:TextBox>

                                    <asp:DropDownList ID="ddlsCondintion_type" runat="server" AppendDataBoundItems="false" AutoPostBack="true" Visible="false"
                                        Enabled="false">
                                        <asp:ListItem Text="Select" Value="" />
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtsZcapRate" runat="server" CssClass="textbox" Text='<%#Eval("Zcap Rate") %>'
                                        Width="60px" MaxLength="16" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtsUOM" runat="server" CssClass="textbox" Text='<%#Eval("UOM") %>'
                                        Width="60px" MaxLength="3" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtsSTONum" runat="server" CssClass="textbox" Text='<%#Eval("STO Num") %>'
                                        Width="60px" MaxLength="50" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtsHSN_Code" runat="server" CssClass="textbox" Text='<%#Eval("HSN Code") %>'
                                        Width="100px" MaxLength="50" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtsGST_Code" runat="server" CssClass="textbox" Text='<%#Eval("GST Code") %>'
                                        Width="60px" MaxLength="5" Visible="false"></asp:TextBox>
                                    <%-- <asp:TextBox ID="txtsGST_Code" runat="server" CssClass="textbox" Text='<%#Eval("GST Code") %>'
                                        Width="20px" MaxLength="5"></asp:TextBox>--%>

                                    <asp:DropDownList ID="ddlsIsLUTCond" runat="server" AppendDataBoundItems="false" Visible="false"
                                        Enabled="false">
                                        <asp:ListItem Text="Select" Value="" />
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtsRemarks" runat="server" CssClass="textbox" Text='<%#Eval("Remarks") %>'
                                        Width="120px" MaxLength="50" TextMode="MultiLine" Visible="false"></asp:TextBox>

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
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
</asp:Panel>
