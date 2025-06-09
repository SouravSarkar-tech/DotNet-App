<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="Sales2.aspx.cs" Inherits="Transaction_Sales2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialSales" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlData" runat="server">
                <table border="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Sales 2
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                            DataKeyNames="Mat_Sales2_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                            GridLines="Both">
                                            <RowStyle CssClass="light-gray" />
                                            <HeaderStyle CssClass="gridHeader" />
                                            <AlternatingRowStyle CssClass="gridRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Plant" DataField="Plant" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("Mat_Sales2_Id") %>' />
                                                        <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2">
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
                                    <td class="leftTD">
                                        Plant
                                        <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" TabIndex="1"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                            ValidationGroup="Sales" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Sales Organization
                                        <asp:Label ID="lableddlSalesOrginization" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlSalesOrginization" runat="server" AppendDataBoundItems="false"
                                            AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlSalesOrginization_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlSalesOrginization" runat="server" ControlToValidate="ddlSalesOrginization"
                                            ValidationGroup="Sales" ErrorMessage="Sales Organization cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Distribution Channel
                                        <asp:Label ID="lableddlDistributionChannel" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlDistributionChannel" runat="server" AppendDataBoundItems="false"
                                            TabIndex="3">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlDistributionChannel" runat="server" ControlToValidate="ddlDistributionChannel"
                                            ValidationGroup="Sales" ErrorMessage="Distribution Channel cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />
                                    </td>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Material statistics group
                                        <asp:Label ID="lableddlMaterialStatisticsG" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlMaterialStatisticsG" runat="server" AppendDataBoundItems="false"
                                            TabIndex="4">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlMaterialStatisticsG" runat="server" ControlToValidate="ddlMaterialStatisticsG"
                                            ValidationGroup="Sales" ErrorMessage="Material statistics group cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material statistics group cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Material Pricing Group
                                        <asp:Label ID="lableddlMaterialPGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlMaterialPGroup" runat="server" AppendDataBoundItems="false"
                                            TabIndex="5">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlMaterialPGroup" runat="server" ControlToValidate="ddlMaterialPGroup"
                                            ValidationGroup="Sales" ErrorMessage="Material Pricing Group cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Pricing Group cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Account assignment group for this material
                                        <asp:Label ID="lableddlAccountAssignment" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlAccountAssignment" runat="server" AppendDataBoundItems="false"
                                            TabIndex="6">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlAccountAssignment" runat="server" ControlToValidate="ddlAccountAssignment"
                                            ValidationGroup="Sales" ErrorMessage="Account assignment group for this material cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Account assignment group for this material cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Gen Item Category Grp
                                        <asp:Label ID="lableddlGenItemCategoryGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlGenItemCategoryGrp" runat="server" AppendDataBoundItems="false"
                                            TabIndex="7">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlGenItemCategoryGrp" runat="server" ControlToValidate="ddlGenItemCategoryGrp"
                                            ValidationGroup="Sales" ErrorMessage="Gen Item Category Grp cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Gen Item Category Grp cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                    <td class="leftTD">
                                        CAS number for pharmaceutical products in foreign trade
                                        <asp:Label ID="lableddlCasNumPharmaceutical" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlCasNumPharmaceutical" runat="server" AppendDataBoundItems="false"
                                            TabIndex="8">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlCasNumPharmaceutical" runat="server" ControlToValidate="ddlCasNumPharmaceutical"
                                            ValidationGroup="Salesch" ErrorMessage="CAS number for pharmaceutical products in foreign trade cannot be blank."
                                            SetFocusOnError="true" Visible="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='CAS number for pharmaceutical products in foreign trade cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Commission group
                                        <asp:Label ID="lableddlCommisionGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlCommisionGroup" runat="server" AppendDataBoundItems="false"
                                            TabIndex="9">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                     <%--   <asp:RequiredFieldValidator ID="reqddlCommisionGroup" runat="server" ControlToValidate="ddlCommisionGroup"
                                            ValidationGroup="Sales" ErrorMessage="Commission group indicator cannot be blank."
                                            SetFocusOnError="true" Visible="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Commission group cannot be blank.' />" />--%>
                                    </td>
                                    <td class="leftTD">
                                        Cross-distribution-chain material status
                                        <asp:Label ID="lableddlCrossDistribution" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlCrossDistribution" runat="server" AppendDataBoundItems="false"
                                            TabIndex="10">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlCrossDistribution" runat="server" ControlToValidate="ddlCrossDistribution"
                                            ValidationGroup="Salesch" ErrorMessage="Cross-distribution-chain material status cannot be blank."
                                            SetFocusOnError="true" Visible="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Cross-distribution-chain material status cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Product Hierarchy 1
                                        <asp:Label ID="lableddlProductHierarchy1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlProductHierarchy1" runat="server" AppendDataBoundItems="false"
                                            AutoPostBack="true" TabIndex="10" OnSelectedIndexChanged="ddlProductHierarchy1_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlProductHierarchy1" runat="server" ControlToValidate="ddlProductHierarchy1"
                                            ValidationGroup="Sales" ErrorMessage="Product Hierarchy 1 cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Product Hierarchy 1 cannot be blank.' />" />
                                    </td>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Product Hierarchy 2
                                        <asp:Label ID="lableddlProductHierarchy2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" colspan="3">
                                        <asp:DropDownList ID="ddlProductHierarchy2" runat="server" AppendDataBoundItems="false"
                                            AutoPostBack="true" TabIndex="11" OnSelectedIndexChanged="ddlProductHierarchy2_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlProductHierarchy2" runat="server" ControlToValidate="ddlProductHierarchy2"
                                            ValidationGroup="Sales" ErrorMessage="Product Hierarchy 2 cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Product Hierarchy 2 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Product Hierarchy 3
                                        <asp:Label ID="lableddlProductHierarchy3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" colspan="3">
                                        <asp:DropDownList ID="ddlProductHierarchy3" runat="server" AppendDataBoundItems="false"
                                            TabIndex="12">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlProductHierarchy3" runat="server" ControlToValidate="ddlProductHierarchy3"
                                            ValidationGroup="Sales" ErrorMessage="Product Hierarchy 3 cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Product Hierarchy 3 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Item category group from material master
                                        <asp:Label ID="lableddlItemCategoryGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlItemCategoryGroup" runat="server" AppendDataBoundItems="false"
                                            TabIndex="11">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlItemCategoryGroup" runat="server" ControlToValidate="ddlItemCategoryGroup"
                                            ValidationGroup="Sales" ErrorMessage="Item category group from material master cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Item category group from material master cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Material group 1
                                        <asp:Label ID="lableddlMaterialGroup1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" colspan="3">
                                        <asp:DropDownList ID="ddlMaterialGroup1" runat="server" AppendDataBoundItems="false"
                                            TabIndex="13">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlMaterialGroup1" runat="server" ControlToValidate="ddlMaterialGroup1"
                                            ValidationGroup="Sales" ErrorMessage="Material group 1 cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Material group 1 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Material group 2
                                        <asp:Label ID="lableddlMaterialGroup2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlMaterialGroup2" runat="server" AppendDataBoundItems="false"
                                            TabIndex="14">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlMaterialGroup2" runat="server" ControlToValidate="ddlMaterialGroup2"
                                            ValidationGroup="Sales" ErrorMessage="Material group 2 cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Material group 2 cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Material group 3
                                        <asp:Label ID="lableddlMaterialGroup3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlMaterialGroup3" runat="server" AppendDataBoundItems="false"
                                            TabIndex="15">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlMaterialGroup3" runat="server" ControlToValidate="ddlMaterialGroup3"
                                            ValidationGroup="Sales" ErrorMessage="Material group 3 cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Material group 3 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Material group 4
                                        <asp:Label ID="lableddlMaterialGroup4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlMaterialGroup4" runat="server" AppendDataBoundItems="false"
                                            TabIndex="16">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlMaterialGroup4" runat="server" ControlToValidate="ddlMaterialGroup4"
                                            ValidationGroup="Sales" ErrorMessage="Material group 4 cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Material group 4 cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Material group 5
                                        <asp:Label ID="lableddlMaterialGroup5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlMaterialGroup5" runat="server" AppendDataBoundItems="false"
                                            TabIndex="17">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlMaterialGroup5" runat="server" ControlToValidate="ddlMaterialGroup5"
                                            ValidationGroup="Sales" ErrorMessage="Material group 5 cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Material group 5 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Volume rebate group
                                        <asp:Label ID="lableddlValumeRGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlValumeRGroup" runat="server" AppendDataBoundItems="false"
                                            TabIndex="18">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                      <%--  <asp:RequiredFieldValidator ID="reqddlValumeRGroup" runat="server" ControlToValidate="ddlValumeRGroup"
                                            ValidationGroup="Sales" ErrorMessage="Volume rebate group cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Visible="false"  Text="<img src='../../images/Error.png' title='Volume rebate group cannot be blank.' />" />--%>
                                    </td>
                                    <td class="leftTD">
                                        Pricing Reference Material
                                        <asp:Label ID="lableddlPricingRefMaterial" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPricingRefMaterial" runat="server" AppendDataBoundItems="false"
                                            TabIndex="19">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPricingRefMaterial" runat="server" ControlToValidate="ddlPricingRefMaterial"
                                            ValidationGroup="Salesch" ErrorMessage="Pricing Reference Material cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Pricing Reference Material cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr id="trButton" runat="server" visible="false">
                                    <td class="centerTD" colspan="4">
                                        <asp:Button ID="btnPrevious" runat="server" ValidationGroup="Sales" Text="Back" UseSubmitBehavior="false"
                                            TabIndex="19" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="Sales" Text="Save" CssClass="button"
                                            UseSubmitBehavior="true" TabIndex="20" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnNext" runat="server" ValidationGroup="Sales" Text="Save & Next"
                                            TabIndex="21" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Sales" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblSalesId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="16" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
