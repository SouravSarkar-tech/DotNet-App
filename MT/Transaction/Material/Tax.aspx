<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="Tax.aspx.cs" Inherits="Transaction_Tax" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="trHeading" align="center" colspan="2">
                Tax
            </td>
        </tr>
        <tr>
            <td class="tdSpace" colspan="2">
                <asp:Panel ID="pnlGrid" runat="server">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                        DataKeyNames="Mat_Tax_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                        GridLines="Both">
                        <RowStyle CssClass="light-gray" />
                        <HeaderStyle CssClass="gridHeader" />
                        <AlternatingRowStyle CssClass="gridRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="Plants" DataField="Plant" />
                            <asp:BoundField HeaderText="Sales_Organizations" DataField="Sales_Organization" />
                            <asp:BoundField HeaderText="Distribution_Channels" DataField="Distribution_Channel" />
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
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Plant
                                <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                    ValidationGroup="Tax" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />--%>
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
                                Sales Org.
                                <asp:Label ID="lableddlSalesOrg" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlSalesOrg" runat="server" AutoPostBack="true" AppendDataBoundItems="false"
                                    TabIndex="2" OnSelectedIndexChanged="ddlSalesOrginization_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="reqddlSalesOrg" runat="server" ControlToValidate="ddlSalesOrg"
                                    ValidationGroup="Tax" ErrorMessage="Sales Organization cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />--%>
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Distribution Channel
                                <asp:Label ID="lableddlDistributionChannel" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlDistributionChannel" runat="server" AppendDataBoundItems="false"
                                    TabIndex="3">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="reqddlDistributionChannel" runat="server" ControlToValidate="ddlDistributionChannel"
                                    ValidationGroup="Tax" ErrorMessage="Distribution Channel cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Tax category (sales tax, federal sales tax,...)
                                <asp:Label ID="lableddlTaxCategory" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxCategory" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                    <asp:ListItem Text="JCST" Value="JCST" />
                                    <asp:ListItem Text="JIVP" Value="JIVP" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxCategory" runat="server" ControlToValidate="ddlTaxCategory"
                                    ValidationGroup="Tax" ErrorMessage="Tax category cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Tax classification material
                                <asp:Label ID="lableddlTaxClassificationM" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxClassificationM" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxClassificationM" runat="server" ControlToValidate="ddlTaxClassificationM"
                                    ValidationGroup="Tax" ErrorMessage="Tax classification material cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax classification material cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Tax category (sales tax, federal sales tax,...)
                                <asp:Label ID="lableddlTaxCategory2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxCategory2" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                    <asp:ListItem Text="JCST" Value="JCST" />
                                    <asp:ListItem Text="JIVP" Value="JIVP" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxCategory2" runat="server" ControlToValidate="ddlTaxCategory2"
                                    ValidationGroup="Tax" ErrorMessage="Tax category cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Tax classification material
                                <asp:Label ID="lableddlTaxClassificationM2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxClassificationM2" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxClassificationM2" runat="server" ControlToValidate="ddlTaxClassificationM2"
                                    ValidationGroup="Tax" ErrorMessage="Tax classification material cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax classification material cannot be blank.' />" />
                            </td>
                        </tr>
                        <%--GST Changes--%>
                        <%-- <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <%--<tr style="display: none">--%>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Tax category (sales tax, federal sales tax,...)
                                <asp:Label ID="lableddlTaxCategory3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxCategory3" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                    <asp:ListItem Text="JCST" Value="JCST" />
                                    <asp:ListItem Text="JIVP" Value="JIVP" />
                                    <asp:ListItem Text="JOIG" Value="JOIG" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxCategory3" runat="server" ControlToValidate="ddlTaxCategory3"
                                    ValidationGroup="Tax" ErrorMessage="Tax category cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Tax classification material
                                <asp:Label ID="lableddlTaxClassificationM3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxClassificationM3" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxClassificationM3" runat="server" ControlToValidate="ddlTaxClassificationM3"
                                    ValidationGroup="Tax" ErrorMessage="Tax classification material cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax classification material cannot be blank.' />" />
                            </td>
                        </tr>
                        <%--GST Changes--%>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Tax category (sales tax, federal sales tax,...)
                                <asp:Label ID="lableddlTaxCategory4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxCategory4" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxCategory4" runat="server" ControlToValidate="ddlTaxCategory4"
                                    ValidationGroup="Taxch" ErrorMessage="Tax category cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Tax classification material
                                <asp:Label ID="lableddlTaxClassificationM4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxClassificationM4" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxClassificationM4" runat="server" ControlToValidate="ddlTaxClassificationM4"
                                    ValidationGroup="Taxch" ErrorMessage="Tax classification material cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax classification material cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Tax category (sales tax, federal sales tax,...)
                                <asp:Label ID="lableddlTaxCategory5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxCategory5" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxCategory5" runat="server" ControlToValidate="ddlTaxCategory5"
                                    ValidationGroup="Taxch" ErrorMessage="Tax category cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Tax classification material
                                <asp:Label ID="lableddlTaxClassificationM5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxClassificationM5" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxClassificationM5" runat="server" ControlToValidate="ddlTaxClassificationM5"
                                    ValidationGroup="Taxch" ErrorMessage="Tax classification material cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax classification material cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Tax category (sales tax, federal sales tax,...)
                                <asp:Label ID="lableddlTaxCategory6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxCategory6" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxCategory6" runat="server" ControlToValidate="ddlTaxCategory6"
                                    ValidationGroup="Taxch" ErrorMessage="Tax category cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Tax classification material
                                <asp:Label ID="lableddlTaxClassificationM6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxClassificationM6" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxClassificationM6" runat="server" ControlToValidate="ddlTaxClassificationM6"
                                    ValidationGroup="Taxch" ErrorMessage="Tax classification material cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax classification material cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Tax category (sales tax, federal sales tax,...)
                                <asp:Label ID="lableddlTaxCategory7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxCategory7" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxCategory7" runat="server" ControlToValidate="ddlTaxCategory7"
                                    ValidationGroup="Taxch" ErrorMessage="Tax category cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Tax classification material
                                <asp:Label ID="lableddlTaxClassificationM7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxClassificationM7" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxClassificationM7" runat="server" ControlToValidate="ddlTaxClassificationM7"
                                    ValidationGroup="Taxch" ErrorMessage="Tax classification material cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax classification material cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Tax category (sales tax, federal sales tax,...)
                                <asp:Label ID="lableddlTaxCategory8" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxCategory8" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxCategory8" runat="server" ControlToValidate="ddlTaxCategory8"
                                    ValidationGroup="Taxch" ErrorMessage="Tax category cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Tax classification material
                                <asp:Label ID="lableddlTaxClassificationM8" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxClassificationM8" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxClassificationM8" runat="server" ControlToValidate="ddlTaxClassificationM8"
                                    ValidationGroup="Taxch" ErrorMessage="Tax classification material cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax classification material cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">
                                Tax category (sales tax, federal sales tax,...)
                                <asp:Label ID="lableddlTaxCategory9" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxCategory9" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxCategory9" runat="server" ControlToValidate="ddlTaxCategory9"
                                    ValidationGroup="Taxch" ErrorMessage="Tax category cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax category cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Tax classification material
                                <asp:Label ID="lableddlTaxClassificationM9" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlTaxClassificationM9" runat="server" AppendDataBoundItems="false"
                                    TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlTaxClassificationM9" runat="server" ControlToValidate="ddlTaxClassificationM9"
                                    ValidationGroup="Taxch" ErrorMessage="Tax classification material cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax classification material cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnPrevious" runat="server" CausesValidation="false" Text="Back"
                                    CssClass="button" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="Tax" Text="Save" CssClass="button"
                                    OnClick="btnSave_Click" />
                                <asp:Button ID="btnNext" runat="server" ValidationGroup="Tax" Text="Save & Next" CssClass="button"
                                    OnClick="btnNext_Click" Width="120px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Tax" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblTaxId" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="18" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
</asp:Content>
