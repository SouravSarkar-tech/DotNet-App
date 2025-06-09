<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="Accounting2.aspx.cs" Inherits="Transaction_Accounting2" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlAddNew" runat="server">
        <asp:UpdatePanel ID="UpdMaterialAccounting" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Accounting 2
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <asp:Panel ID="pnlGrid" runat="server">
                                <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                    DataKeyNames="Mat_Accounting2_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                    GridLines="Both">
                                    <RowStyle CssClass="light-gray" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <AlternatingRowStyle CssClass="gridRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Plants" DataField="Plant" />
                                        <asp:TemplateField HeaderText="Commercial Price">
                                            <ItemTemplate>
                                                <strong>Commercial Price 1&nbsp;:</strong>
                                                <asp:Label ID="lblCommercialPrice1" runat="server" Text='<%# Eval("Commercial_Price1") %>'></asp:Label>
                                                <br />
                                                <strong>Commercial Price 2&nbsp;:</strong>
                                                <asp:Label ID="lblCommercialPrice2" runat="server" Text='<%# Eval("Commercial_Price2") %>'></asp:Label>
                                                <br />
                                                <strong>Commercial Price 3&nbsp;:</strong>
                                                <asp:Label ID="lblCommercialPrice3" runat="server" Text='<%# Eval("Commercial_Price3") %>'></asp:Label>
                                                <br />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
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
                                                ValidationGroup="Accounting2" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                        </td>
                                        <td class="tdSpace" colspan="2" align="right">
                                            <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            LIFO/FIFO-Relevant
                                            <asp:Label ID="lablechkLifoFifo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:CheckBox ID="chkLifoFifo" runat="server" Text="Check if Relevant" TabIndex="2" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Pool number for LIFO valuation
                                            <asp:Label ID="lableddlPoolNumberLifo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlPoolNumberLifo" runat="server" AppendDataBoundItems="false"
                                                TabIndex="3">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPoolNumberLifo" runat="server" ControlToValidate="ddlPoolNumberLifo"
                                                ValidationGroup="Accounting2" ErrorMessage="Pool number for LIFO valuation cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Pool number for LIFO valuation cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Tax Price 1
                                            <asp:Label ID="labletxtTaxPrice1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtTaxPrice1" runat="server" CssClass="textbox" Width="100px" TabIndex="4"
                                                onkeypress="return IsNumber();" MaxLength="11" />
                                            <asp:RequiredFieldValidator ID="reqtxtTaxPrice1" runat="server" ControlToValidate="txtTaxPrice1"
                                                ValidationGroup="Accounting2" ErrorMessage="Tax Price 1 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax Price 1 cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Commercial Price 1
                                            <asp:Label ID="labletxtCommercialPrice1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtCommercialPrice1" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="4" onkeypress="return IsNumber();" MaxLength="11" />
                                            <asp:RequiredFieldValidator ID="reqtxtCommercialPrice1" runat="server" ControlToValidate="txtCommercialPrice1"
                                                ValidationGroup="Accounting2" ErrorMessage="Commercial Price 1 cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Commercial Price 1 cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Tax Price 2
                                            <asp:Label ID="labletxtTaxPrice2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtTaxPrice2" runat="server" CssClass="textbox" Width="100px" TabIndex="5"
                                                onkeypress="return IsNumber();" MaxLength="11" />
                                            <asp:RequiredFieldValidator ID="reqtxtTaxPrice2" runat="server" ControlToValidate="txtTaxPrice2"
                                                ValidationGroup="Accounting2" ErrorMessage="Tax Price 2 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax Price 2 cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Commercial Price 2
                                            <asp:Label ID="labletxtCommercialPrice2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtCommercialPrice2" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="5" onkeypress="return IsNumber();" MaxLength="11" />
                                            <asp:RequiredFieldValidator ID="reqtxtCommercialPrice2" runat="server" ControlToValidate="txtCommercialPrice2"
                                                ValidationGroup="Accounting2" ErrorMessage="Commercial Price 2 cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Commercial Price 2 cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Tax Price 3
                                            <asp:Label ID="labletxtTaxPrice3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtTaxPrice3" runat="server" CssClass="textbox" Width="100px" TabIndex="6"
                                                onkeypress="return IsNumber();" MaxLength="11" />
                                            <asp:RequiredFieldValidator ID="reqtxtTaxPrice3" runat="server" ControlToValidate="txtTaxPrice3"
                                                ValidationGroup="Accounting2" ErrorMessage="Tax Price 3 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax Price 3 cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Commercial Price 3
                                            <asp:Label ID="labletxtCommercialPrice3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtCommercialPrice3" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="6" onkeypress="return IsNumber();" MaxLength="11" />
                                            <asp:RequiredFieldValidator ID="reqtxtCommercialPrice3" runat="server" ControlToValidate="txtCommercialPrice3"
                                                ValidationGroup="Accounting2" ErrorMessage="Commercial Price 3 cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Commercial Price 3 cannot be blank.' />" />
                                        </td>
                                        <td class="tdSpace" colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr id="trButton" runat="server" visible="false">
                                        <td class="centerTD" colspan="4">
                                            <asp:Button ID="btnPrevious" runat="server" ValidationGroup="Accounting2" Text="Back"
                                                TabIndex="7" UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                            <asp:Button ID="btnSave" runat="server" ValidationGroup="Accounting2" Text="Save"
                                                TabIndex="8" UseSubmitBehavior="true" CssClass="button" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnNext" runat="server" ValidationGroup="Accounting2" Text="Save & Next"
                                                TabIndex="9" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Accounting2" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblAccountingId" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="2" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
</asp:Content>
