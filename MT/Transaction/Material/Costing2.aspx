<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="Costing2.aspx.cs" Inherits="Transaction_Costing2" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlAddNew" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">
                    Costing 2
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
                    <asp:Panel ID="pnlGrid" runat="server">
                        <asp:GridView ID="grvCosting2" runat="server" AutoGenerateColumns="false" Width="100%"
                            DataKeyNames="Mat_Costing2_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                            GridLines="Both">
                            <RowStyle CssClass="light-gray" />
                            <HeaderStyle CssClass="gridHeader" />
                            <AlternatingRowStyle CssClass="gridRowStyle" />
                            <PagerStyle CssClass="PagerStyle" />
                            <Columns>
                                <asp:BoundField HeaderText="Plants" DataField="Plant" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" />
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
                                    <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" TabIndex="1">
                                        <asp:ListItem Text="Select" Value="" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                        ValidationGroup="Costing2" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                </td>
                                <td class="tdSpace" colspan="2" align="right">
                                    <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Planned price 1 (BTCI)
                                    <asp:Label ID="labletxtPlannedPrice" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 25%">
                                    <asp:TextBox ID="txtPlannedPrice" runat="server" CssClass="textbox" Width="100px"
                                        TabIndex="2" onkeypress="return IsNumber();" MaxLength="11" />
                                    <asp:RequiredFieldValidator ID="reqtxtPlannedPrice" runat="server" ControlToValidate="txtPlannedPrice"
                                        ValidationGroup="Costing2" ErrorMessage="Planned price 1 (BTCI) cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planned price 1 (BTCI) cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 25%">
                                    Planned Price Date 1 (BTCI)
                                    <asp:Label ID="labletxtPlannedPriceDate" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtPlannedPriceDate" runat="server" CssClass="textbox" Width="100px"
                                        TabIndex="3" />
                                    <act:CalendarExtender ID="CaltxtPlannedPriceDate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtPlannedPriceDate" />
                                    <asp:RequiredFieldValidator ID="reqtxtPlannedPriceDate" runat="server" ControlToValidate="txtPlannedPriceDate"
                                        ValidationGroup="Costing2" ErrorMessage="Planned Price Date 1 (BTCI) cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planned Price Date 1 (BTCI) cannot be blank.' />" />
                                    <asp:RegularExpressionValidator ID="revDTTest" runat="server" ControlToValidate="txtPlannedPriceDate"
                                        ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                        ValidationGroup="Costing2" Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                        Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Planned price 2 (BTCI)
                                    <asp:Label ID="labletxtPlannedPrice2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 25%">
                                    <asp:TextBox ID="txtPlannedPrice2" runat="server" CssClass="textbox" Width="100px"
                                        TabIndex="4" onkeypress="return IsNumber();" MaxLength="11" />
                                    <asp:RequiredFieldValidator ID="reqtxtPlannedPrice2" runat="server" ControlToValidate="txtPlannedPrice2"
                                        ValidationGroup="Costing2" ErrorMessage="Planned price 2 (BTCI) cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planned price 2 (BTCI) cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 25%">
                                    Planned Price Date 2 (BTCI)
                                    <asp:Label ID="labletxtPlannedPriceDate2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtPlannedPriceDate2" runat="server" CssClass="textbox" Width="100px"
                                        TabIndex="5" />
                                    <act:CalendarExtender ID="CaltxtPlannedPriceDate2" runat="server" TargetControlID="txtPlannedPriceDate2"
                                        Format="dd/MM/yyyy" />
                                    <asp:RequiredFieldValidator ID="reqtxtPlannedPriceDate2" runat="server" ControlToValidate="txtPlannedPriceDate2"
                                        ValidationGroup="Costing2" ErrorMessage="Planned Price Date 2 (BTCI) cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planned Price Date 2 (BTCI) cannot be blank.' />" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPlannedPriceDate2"
                                        ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                        ValidationGroup="Costing2" Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                        Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" style="width: 20%">
                                    Planned price 3 (BTCI)
                                    <asp:Label ID="labletxtPlannedPrice3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 25%">
                                    <asp:TextBox ID="txtPlannedPrice3" runat="server" CssClass="textbox" Width="100px"
                                        TabIndex="6" onkeypress="return IsNumber();" MaxLength="11" />
                                    <asp:RequiredFieldValidator ID="reqtxtPlannedPrice3" runat="server" ControlToValidate="txtPlannedPrice3"
                                        ValidationGroup="Costing2" ErrorMessage="Planned price 3 (BTCI) cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planned price 3 (BTCI) cannot be blank.' />" />
                                </td>
                                <td class="leftTD" style="width: 25%">
                                    Planned Price Date 3 (BTCI)
                                    <asp:Label ID="labletxtPlannedPriceDate3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtPlannedPriceDate3" runat="server" CssClass="textbox" Width="100px"
                                        TabIndex="7" />
                                    <act:CalendarExtender ID="CaltxtPlannedPriceDate3" runat="server" TargetControlID="txtPlannedPriceDate3"
                                        Format="dd/MM/yyyy" />
                                    <asp:RequiredFieldValidator ID="reqtxtPlannedPriceDate3" runat="server" ControlToValidate="txtPlannedPriceDate3"
                                        ValidationGroup="Costing2" ErrorMessage="Planned Price Date 3 (BTCI) cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Planned Price Date 3 (BTCI) cannot be blank.' />" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPlannedPriceDate3"
                                        ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                        ValidationGroup="Costing2" Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                        Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" colspan="4">
                                </td>
                            </tr>
                            <tr id="trButton" runat="server" visible="false">
                                <td class="centerTD" colspan="4">
                                    <asp:Button ID="btnPrevious" runat="server" ValidationGroup="Costing2" Text="Back"
                                        TabIndex="8" UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                    <asp:Button ID="btnSave" runat="server" ValidationGroup="Costing2" Text="Save" CssClass="button"
                                        TabIndex="9" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnNext" runat="server" ValidationGroup="Costing2" Text="Save & Next"
                                        TabIndex="10" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Costing2" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblCostingId" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="6" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
</asp:Content>
