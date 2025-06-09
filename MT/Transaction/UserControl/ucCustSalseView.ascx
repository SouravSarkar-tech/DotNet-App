
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCustSalseView.ascx.cs" Inherits="Transaction_UserControl_ucCustSalseView" %>
<div style="width: 100%" class="leftTD">
    Import Data
</div>
<div align="right">
    <asp:HyperLink ID="hlImportFormat" runat="server" Text="Excel Format" Target="_blank"
        NavigateUrl="~/Transaction/Customer/UploadFormat/Customer Sales Master Format1.xlsx"></asp:HyperLink>
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
                <span class="ui-dialog-title">Import Customer Sales View Data</span>
            </div>
        </asp:Panel>
        <table border="0" cellpadding="0" cellspacing="1" width="100%">
            <tr>
                <td class="tdSpace">
                    <asp:GridView ID="grvData1" runat="server" OnDataBound="grvData1_DataBound" AutoGenerateColumns="false">
                        <Columns>
                           <asp:TemplateField HeaderText="Sales Org">
                                <ItemTemplate>
                                    <asp:Label ID="lblSalesOrg" runat="server" Text='<%# Eval("Sales_Org") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Dist. Channel">
                                <ItemTemplate>
                                    <asp:Label ID="lblDistChannel" runat="server" Text='<%# Eval("Dist_Channel") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Division">
                                <ItemTemplate>
                                    <asp:Label ID="lblDivision" runat="server" Text='<%# Eval("Division") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales District">
                                <ItemTemplate>
                                    <asp:Label ID="lblSalesDistrict" runat="server" Text='<%# Eval("Sales_District") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Office">
                                <ItemTemplate>
                                    <asp:Label ID="lblSalesOffice" runat="server" Text='<%# Eval("Sales_Office") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Grp">
                                <ItemTemplate>
                                    <asp:Label ID="lblSalesGrp" runat="server" Text='<%# Eval("Sales_Grp") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Currency">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("Currency") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delivering Plant">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeliveringPlant" runat="server" Text='<%# Eval("Delivering_Plant") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price Group">
                                <ItemTemplate>
                                    <asp:Label ID="lblPriceGroup" runat="server" Text='<%# Eval("Price_Group") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Invoice Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("Invoice_Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice List">
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceList" runat="server" Text='<%# Eval("Invoice_List") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit control Area">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreditcontrolArea" runat="server" Text='<%# Eval("Credit_control_Area") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit Currency">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreditCurrency" runat="server" Text='<%# Eval("Credit_Currency") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Risk Category">
                                <ItemTemplate>
                                    <asp:Label ID="lblRiskCategory" runat="server" Text='<%# Eval("Risk_Category") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             
                            <asp:BoundField DataField="Customer_Credit_Limit" HeaderText="Customer Credit Limit" /> 

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Panel ID="pnlMsg" runat="server">
                                        <asp:Label ID="lblMsg" runat="server" />
                                    </asp:Panel>
                                    <asp:DropDownList ID="ddlSalesOrg" runat="server" TabIndex="1" AppendDataBoundItems="false" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlDistChannel" runat="server" TabIndex="2" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlDivision" runat="server" TabIndex="3" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlSalesDistrict" runat="server" TabIndex="4" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlSalesOffice" runat="server" TabIndex="5" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlSalesGrp" runat="server" TabIndex="6" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlCurrency" runat="server" TabIndex="7" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlDeliveringPlant" runat="server" TabIndex="8" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlPriceGroup" runat="server" TabIndex="9" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlInvoiceDate" runat="server" TabIndex="10" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlInvoiceList" runat="server" TabIndex="11" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlCreditcontrolArea" runat="server" TabIndex="12" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlCreditCurrency" runat="server" TabIndex="13" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    </asp:DropDownList> 
                                    <asp:DropDownList ID="ddlRiskCategory" runat="server" TabIndex="14" AutoPostBack="true" Visible="false">
                                        <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    </asp:DropDownList>  
                                    <asp:TextBox ID="txtCustomerCreditLimit" runat="server" CssClass="textbox" MaxLength="70" Width="210" Visible="false"
                                        Text='<%# Eval("Customer_Credit_Limit") %>' TabIndex="15" /> 
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
</asp:Panel>

