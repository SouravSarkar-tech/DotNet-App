<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="Accounting1.aspx.cs" Inherits="Transaction_Accounting1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
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
                        Accounting
                    </td>
                </tr>
                <tr>
                    <td class="tdSpace" colspan="2">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                DataKeyNames="Mat_Accounting1_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                GridLines="Both">
                                <RowStyle CssClass="light-gray" HorizontalAlign="Left" />
                                <HeaderStyle CssClass="gridHeader" />
                                <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Left" />
                                <PagerStyle CssClass="PagerStyle" />
                                <Columns>
                                    <%--<asp:BoundField HeaderText="Plants" DataField="Plant" />--%>
                                    <asp:TemplateField HeaderText="Org Data">
                                        <ItemTemplate>
                                            <strong>Plant&nbsp;:</strong>
                                            <asp:Label ID="lblPlant" runat="server" Text='<%# Eval("Plant") %>'></asp:Label>
                                            <br />
                                            <strong>Valuation Type&nbsp;:</strong>
                                            <asp:Label ID="lblValuationType" runat="server" Text='<%# Eval("Valuation_Type") %>'></asp:Label>
                                            <br />
                                            <strong>Val. Category&nbsp;:</strong>
                                            <asp:Label ID="lblValuationCategory" runat="server" Text='<%# Eval("Valuation_Category") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Accounting Data">
                                        <ItemTemplate>
                                            <strong>Val. Class&nbsp;:</strong>
                                            <asp:Label ID="lblValuationClass" runat="server" Text='<%# Eval("Valuation_Class") %>'></asp:Label>
                                            <br />
                                            <strong>Period Ctrl. Indi.&nbsp;:</strong>
                                            <asp:Label ID="lblPeriodCtrlIndi" runat="server" Text='<%# Eval("Price_Ctrl_Indicator") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Accounting Data">
                                        <ItemTemplate>
                                            <strong>Price Unit&nbsp;:</strong>
                                            <asp:Label ID="lblPriceUnit" runat="server" Text='<%# Eval("Price_Unit") %>'></asp:Label>
                                            <br />
                                            <strong>Moving Avg. Price&nbsp;:</strong>
                                            <asp:Label ID="lblMovingAvgPrice" runat="server" Text='<%# Eval("Moving_Avg_Price") %>'></asp:Label>
                                            <br />
                                            <strong>Standard Price&nbsp;:</strong>
                                            <asp:Label ID="lblStandardPrice" runat="server" Text='<%# Eval("Standard_Price") %>'></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" />
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="lnkDelete_Click" />
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
                                        <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Plant
                                        <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                            ValidationGroup="Accounting" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Valuation Type
                                        <asp:Label ID="lableddlValuationType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlValuationType" runat="server" AppendDataBoundItems="false"
                                            AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlValuationType_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlValuationType" runat="server" ControlToValidate="ddlValuationType"
                                            ValidationGroup="Accounting" ErrorMessage="Valuation Type cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Type cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Valuation Category
                                        <asp:Label ID="lableddlValuationCategory" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlValuationCategory" runat="server" AppendDataBoundItems="false"
                                            TabIndex="3">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlValuationCategory" runat="server" ControlToValidate="ddlValuationCategory"
                                            ValidationGroup="Accounting" ErrorMessage="Valuation Category cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Category cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Valuation Class
                                        <asp:Label ID="lableddlValuationClass" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlValuationClass" runat="server" AppendDataBoundItems="false"
                                            TabIndex="4">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlValuationClass" runat="server" ControlToValidate="ddlValuationClass"
                                            ValidationGroup="Accounting" ErrorMessage="Valuation Class cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Valuation Class cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Price Control Indicator
                                        <asp:Label ID="lableddlPriceControlIndicator" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlPriceControlIndicator" runat="server" AppendDataBoundItems="false"
                                            TabIndex="5">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPriceControlIndicator" runat="server" ControlToValidate="ddlPriceControlIndicator"
                                            ValidationGroup="Accounting" ErrorMessage="Price Control Indicator cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price Control Indicator cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Price unit (BTCI)
                                        <asp:Label ID="labletxtPriceUnit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtPriceUnit" runat="server" CssClass="textbox" MaxLength="5" Width="50px"
                                            TabIndex="6" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtPriceUnit" runat="server" ControlToValidate="txtPriceUnit"
                                            ValidationGroup="Accounting" ErrorMessage="Price unit (BTCI) cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Price unit (BTCI) cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Moving average price (BTCI)
                                        <asp:Label ID="labletxtMovingAvgPrice" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMovingAvgPrice" runat="server" CssClass="textbox" MaxLength="11"
                                            TabIndex="7" onkeypress="return IsNumber();" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtMovingAvgPrice" runat="server" ControlToValidate="txtMovingAvgPrice"
                                            ValidationGroup="Accounting" ErrorMessage="Moving Avg. Price cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Moving Avg. Price cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Standard Price
                                        <asp:Label ID="labletxtStandardPrice" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtStandardPrice" runat="server" CssClass="textbox" MaxLength="11"
                                            TabIndex="8" onkeypress="return IsNumber();" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtStandardPrice" runat="server" ControlToValidate="txtStandardPrice"
                                            ValidationGroup="Accounting" ErrorMessage="Standard Price cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Standard Price cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr id="trButton" runat="server" visible="false">
                                    <td class="centerTD" colspan="4">
                                        <asp:Button ID="btnPrevious" runat="server" ValidationGroup="Accounting" Text="Back"
                                            TabIndex="9" UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="Accounting" Text="Save"
                                            UseSubmitBehavior="true" TabIndex="10" CssClass="button" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnNext" runat="server" ValidationGroup="Accounting" Text="Save & Next"
                                            TabIndex="11" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Accounting" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblAccountingId" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="1" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
</asp:Content>
