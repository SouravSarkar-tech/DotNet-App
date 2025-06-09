<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="Classification.aspx.cs" Inherits="Transaction_Classification" %>

<%--validateRequest="false" enableEventValidation="false" --%>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<%--IND_DT14012020 Added by NR--%>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<%--IND_DT14012020 Added by NR--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialClassification" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlAddNew" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">Classification
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="4"></td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="4" align="center" style="color: Red">( Please enter 'NA' in case of not applicable for Mandatory fields of Kinaxis. )
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <asp:Panel ID="pnlGrid" runat="server">
                                <asp:GridView ID="grvClassification" runat="server" AutoGenerateColumns="false" Width="100%"
                                    DataKeyNames="Mat_Classification_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                    GridLines="Both">
                                    <RowStyle CssClass="light-gray" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <AlternatingRowStyle CssClass="gridRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Plants" DataField="Mat_Classification_Id" />
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
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="2"></td>
                                        <td class="tdSpace" colspan="2" align="right">
                                            <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>

                                    <%--IND_DT14012020 Commented by NR--%>
                                    <%-- <tr>
                                        <td class="leftTD">
                                            Class Type
                                            <asp:Label ID="lableddlClass_Type" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlClass_Type" runat="server" AppendDataBoundItems="false"
                                                AutoPostBack="true" TabIndex="11" OnSelectedIndexChanged="ddlClass_Type_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlClass_Type" runat="server" ControlToValidate="ddlClass_Type"
                                                ValidationGroup="Classification" ErrorMessage="Class Type cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Class Type cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">
                                            Class
                                            <asp:Label ID="lableddlClass" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlClass" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                TabIndex="11" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlClass" runat="server" ControlToValidate="ddlClass"
                                                ValidationGroup="Classification" ErrorMessage="Class cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Class cannot be blank.' />" />
                                      
                                            </td>
                                    </tr>--%>
                                    <%--IND_DT14012020 Commented by NR--%>

                                    <%--IND_DT14012020 New Added by NR--%>

                                    <tr>
                                        <td class="leftTD">Class Type
                                            <asp:Label ID="lableddlClass_Type" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <cc1:DropDownCheckBoxes ID="ddlClass_Type" runat="server" AddJQueryReference="false"
                                                UseButtons="false" UseSelectAllNode="true" ClientIDMode="Static"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlClass_Type_SelectedIndexChanged">
                                                <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="350" DropDownBoxBoxHeight="80" />
                                                <Texts SelectBoxCaption="--Select--" />
                                            </cc1:DropDownCheckBoxes>
                                            <cc1:ExtendedRequiredFieldValidator ID="reqddlClass_Type" runat="server"
                                                ControlToValidate="ddlClass_Type"
                                                ValidationGroup="Classification" ErrorMessage="Class Type cannot be blank."
                                                SetFocusOnError="true"
                                                Display="Dynamic"
                                                ToolTip="Class Type cannot be blank."
                                                Text="<img src='../../images/Error.png' />"></cc1:ExtendedRequiredFieldValidator>

                                        </td>


                                        <td colspan="2" class="leftTD">
                                            <asp:Label ID="lblc1ddlClass_Type" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">Class
                                            <asp:Label ID="lableddlClass" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <cc1:DropDownCheckBoxes ID="ddlClass" runat="server" AddJQueryReference="false"
                                                UseButtons="false" UseSelectAllNode="true" ClientIDMode="Static"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                                <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="350" DropDownBoxBoxHeight="80" />
                                                <Texts SelectBoxCaption="--Select--" />
                                            </cc1:DropDownCheckBoxes>
                                            <cc1:ExtendedRequiredFieldValidator ID="reqddlClass" runat="server"
                                                ControlToValidate="ddlClass"
                                                ValidationGroup="Classification"
                                                ErrorMessage="Class cannot be blank."
                                                SetFocusOnError="true"
                                                Display="Dynamic"
                                                ToolTip="Class cannot be blank."
                                                Text="<img src='../../images/Error.png' />"></cc1:ExtendedRequiredFieldValidator>


                                        </td>
                                        <td colspan="2" class="leftTD">
                                            <asp:Label ID="lblc1ddlClass" runat="server"></asp:Label>
                                        </td>
                                    </tr>

                                    <%--IND_DT14012020 New Added by NR--%>

                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4" style="color: Purple; font-weight: bolder;">Class Type : 001 | Class : MCLASS
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">Strength of mat. / Pack type
                                            <asp:Label ID="labletxtStrengthofmatPacktype" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtStrengthofmatPacktype" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtStrengthofmatPacktype" runat="server" ControlToValidate="txtStrengthofmatPacktype"
                                                ValidationGroup="Classification" ErrorMessage="Strength of mat. / Pack type cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Strength of mat. / Pack type cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">Market
                                            <asp:Label ID="labletxtMarket" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtMarket" runat="server" CssClass="textbox" MaxLength="30" Width="180px"
                                                TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtMarket" runat="server" ControlToValidate="txtMarket"
                                                ValidationGroup="Classification" ErrorMessage="Market cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Market cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">NDC No. (LPI)
                                            <asp:Label ID="labletxtNDCNoLPI" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtNDCNoLPI" runat="server" CssClass="textbox" MaxLength="30" Width="180px"
                                                TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtNDCNoLPI" runat="server" ControlToValidate="txtNDCNoLPI"
                                                ValidationGroup="Classification" ErrorMessage="NDC No. (LPI) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='NDC No. (LPI) cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">NDC No. (LL)
                                            <asp:Label ID="labletxtNDCNoLL" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtNDCNoLL" runat="server" CssClass="textbox" MaxLength="30" Width="180px"
                                                TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtNDCNoLL" runat="server" ControlToValidate="txtNDCNoLL"
                                                ValidationGroup="Classification" ErrorMessage="NDC No. (LL) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='NDC No. (LL) cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">HTS
                                            <asp:Label ID="labletxtHTS" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtHTS" runat="server" CssClass="textbox" MaxLength="30" Width="180px"
                                                TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtHTS" runat="server" ControlToValidate="txtHTS"
                                                ValidationGroup="Classification" ErrorMessage="HTS cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='HTS cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">ANDA
                                            <asp:Label ID="labletxtANDA" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtANDA" runat="server" CssClass="textbox" MaxLength="30" Width="180px"
                                                TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtANDA" runat="server" ControlToValidate="txtANDA"
                                                ValidationGroup="Classification" ErrorMessage="ANDA cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='ANDA cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">FDA No.
                                            <asp:Label ID="labletxtFDANo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtFDANo" runat="server" CssClass="textbox" MaxLength="30" Width="180px"
                                                TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtFDANo" runat="server" ControlToValidate="txtFDANo"
                                                ValidationGroup="Classification" ErrorMessage="FDA No. cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='FDA No. cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">LPI Material Identifier
                                            <asp:Label ID="lableddlLPIMaterialIdentifier" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlLPIMaterialIdentifier" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlLPIMaterialIdentifier" runat="server" ControlToValidate="ddlLPIMaterialIdentifier"
                                                ValidationGroup="Classification" ErrorMessage="LPI Material Identifier cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='LPI Material Identifier cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">Material Grouping for MES
                                            <asp:Label ID="lableddlMaterialGroupingforMES" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlMaterialGroupingforMES" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMaterialGroupingforMES" runat="server" ControlToValidate="ddlMaterialGroupingforMES"
                                                ValidationGroup="Classification" ErrorMessage="Material Grouping for MES cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Grouping for MES cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">Short description for 3PL
                                            <asp:Label ID="labletxtShortdescriptionfor3PL" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtShortdescriptionfor3PL" runat="server" CssClass="textbox" MaxLength="21"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtShortdescriptionfor3PL" runat="server" ControlToValidate="txtShortdescriptionfor3PL"
                                                ValidationGroup="Classification" ErrorMessage="Short description for 3PL cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Short description for 3PL cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">Package presentation 3PL
                                            <asp:Label ID="labletxtPackagepresentation3PL" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtPackagepresentation3PL" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtPackagepresentation3PL" runat="server" ControlToValidate="txtPackagepresentation3PL"
                                                ValidationGroup="Classification" ErrorMessage="Package presentation 3PL cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Package presentation 3PL cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">Number of Tablet 3PL
                                            <asp:Label ID="labletxtNumberofTablet3PL" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtNumberofTablet3PL" runat="server" CssClass="textbox" MaxLength="30"
                                                onkeypress="return IsNumber();" Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtNumberofTablet3PL" runat="server" ControlToValidate="txtNumberofTablet3PL"
                                                ValidationGroup="Classification" ErrorMessage="Number of Tablet 3PL cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Number of Tablet 3PL cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">Material Category A 3PL
                                            <asp:Label ID="lableddlMaterialCategoryA3PL" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlMaterialCategoryA3PL" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMaterialCategoryA3PL" runat="server" ControlToValidate="ddlMaterialCategoryA3PL"
                                                ValidationGroup="Classification" ErrorMessage="Material Category A 3PL cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Category A 3PL cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">Material Category B 3PL
                                            <asp:Label ID="lableddlMaterialCategoryB3PL" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlMaterialCategoryB3PL" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMaterialCategoryB3PL" runat="server" ControlToValidate="ddlMaterialCategoryB3PL"
                                                ValidationGroup="Classification" ErrorMessage="Material Category B 3PL cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Category B 3PL cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">Sorting for inventory report
                                            <asp:Label ID="labletxtSortingforinventoryreport" runat="server" ForeColor="Red"
                                                Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtSortingforinventoryreport" runat="server" CssClass="textbox"
                                                MaxLength="30" Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtSortingforinventoryreport" runat="server" ControlToValidate="txtSortingforinventoryreport"
                                                ValidationGroup="Classification" ErrorMessage="Sorting for inventory report cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Sorting for inventory report cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">Pack size
                                            <asp:Label ID="labletxtPacksize" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtPacksize" runat="server" CssClass="textbox" MaxLength="30" Width="180px"
                                                TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtPacksize" runat="server" ControlToValidate="txtPacksize"
                                                ValidationGroup="Classification" ErrorMessage="Pack size cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Pack size cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">Product Group
                                            <asp:Label ID="labletxtProductGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtProductGroup" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtProductGroup" runat="server" ControlToValidate="txtProductGroup"
                                                ValidationGroup="Classification" ErrorMessage="Product Group cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Product Group cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">Drug Category
                                            <asp:Label ID="lableddlDrugCategory" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlDrugCategory" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlDrugCategory" runat="server" ControlToValidate="ddlDrugCategory"
                                                ValidationGroup="Classification" ErrorMessage="Drug Category cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Drug Category cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">Market Entry Date
                                            <asp:Label ID="labletxtMarketEntryDate" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtMarketEntryDate" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <act:CalendarExtender ID="CaltxtMarketEntryDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtMarketEntryDate" />
                                            <asp:RequiredFieldValidator ID="reqtxtMarketEntryDate" runat="server" ControlToValidate="txtMarketEntryDate"
                                                ValidationGroup="Classification" ErrorMessage="Market Entry Date cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Market Entry Date cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="revtxtMarketEntryDate" runat="server" ControlToValidate="txtMarketEntryDate"
                                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                ValidationGroup="Classification" Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="leftTD">PZN ( HORMOSAN)
                                            <asp:Label ID="labletxtPZNHORMOSAN" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtPZNHORMOSAN" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtPZNHORMOSAN" runat="server" ControlToValidate="txtPZNHORMOSAN"
                                                ValidationGroup="Classification" ErrorMessage="PZN ( HORMOSAN) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='PZN ( HORMOSAN) cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">Storage Condition
                                            <asp:Label ID="lableddlStorageCond" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlStorageCond" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlStorageCond" runat="server" ControlToValidate="ddlStorageCond"
                                                ValidationGroup="Classification" ErrorMessage="Storage Condition cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Condition cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <%-- PROV-CCP-MM-941-23-0045 Start--%>

                                    <tr>
                                        <td class="leftTD">Kinaxis-SBU
                                              <asp:Label ID="labeltxtKXSBU" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtKXSBU" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="rfvtxtKXSBU" runat="server" ControlToValidate="txtKXSBU"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-SBU cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-SBU cannot be blank.' />" />

                                            <%--<asp:Label ID="labelddlKXSBU" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlKXSBU" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlKXSBU" runat="server" ControlToValidate="ddlKXSBU"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-SBU cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-SBU cannot be blank.' />" />
                                            --%>
                                        </td>
                                        <td class="leftTD">Kinaxis-Therapy
                                            <%--<asp:Label ID="labelddlKXTHER" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                            <asp:Label ID="labeltxtKXTHER" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtKXTHER" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="rfvtxtKXTHER" runat="server" ControlToValidate="txtKXTHER"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Therapy cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Therapy cannot be blank.' />" />

                                            <%-- <asp:DropDownList ID="ddlKXTHER" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlKXTHER" runat="server" ControlToValidate="ddlKXTHER"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Therapy cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Therapy cannot be blank.' />" />
                                            --%>
                                            
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>

                                    <tr>
                                        <td class="leftTD">Kinaxis-Market
                                            
                                              <asp:Label ID="labeltxtKXMARKT" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtKXMARKT" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="rfvtxtKXMARKT" runat="server" ControlToValidate="txtKXMARKT"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Market cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Market cannot be blank.' />" />

                                            <%--    <asp:Label ID="labelddlKXMARKT" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlKXMARKT" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlKXMARKT" runat="server" ControlToValidate="ddlKXMARKT"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Market cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Market cannot be blank.' />" />
                                            --%>
                                        </td>
                                        <td class="leftTD">Kinaxis-Dosage Form
                                           <%-- <asp:Label ID="labelddlKXDOSFRM" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                            <asp:Label ID="labeltxtKXDOSFRM" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">

                                            <asp:TextBox ID="txtKXDOSFRM" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="rfvtxtKXDOSFRM" runat="server" ControlToValidate="txtKXDOSFRM"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Dosage Form cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Dosage Form cannot be blank.' />" />

                                            <%-- <asp:DropDownList ID="ddlKXDOSFRM" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlKXDOSFRM" runat="server" ControlToValidate="ddlKXDOSFRM"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Dosage Form cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Dosage Form cannot be blank.' />" />
                                            --%>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>

                                    <tr>
                                        <td class="leftTD">Kinaxis-Selling Country
                                            
                                              <asp:Label ID="labeltxtKXSELLCTRY" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtKXSELLCTRY" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="rfvtxtKXSELLCTRY" runat="server" ControlToValidate="txtKXSELLCTRY"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Selling Country cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Selling Country cannot be blank.' />" />


                                            <%--  <asp:Label ID="labelddlKXSELLCTRY" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlKXSELLCTRY" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlKXSELLCTRY" runat="server" ControlToValidate="ddlKXSELLCTRY"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Selling Country cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Selling Country cannot be blank.' />" />
                                            --%>
                                        </td>
                                        <td class="leftTD">Kinaxis-Minimum Shelf Life
                                            <asp:Label ID="labeltxtKXMINSL" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtKXMINSL" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="rfvtxtKXMINSL" runat="server" ControlToValidate="txtKXMINSL"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Minimum Shelf Life cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Minimum Shelf Life cannot be blank.' />" />

                                            <%--<asp:DropDownList ID="ddlKXMINSL" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlKXMINSL" runat="server" ControlToValidate="ddlKXMINSL"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Minimum Shelf Life cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Minimum Shelf Life cannot be blank.' />" />
                                            --%>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>

                                    <tr>
                                        <td class="leftTD">Kinaxis-Business
                                            
                                              <asp:Label ID="labeltxtKXBUSI" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtKXBUSI" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="rfvtxtKXBUSI" runat="server" ControlToValidate="txtKXBUSI"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Business cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Business cannot be blank.' />" />

                                            <%--    <asp:Label ID="labelddlKXBUSI" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlKXBUSI" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlKXBUSI" runat="server" ControlToValidate="ddlKXBUSI"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Business cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Business cannot be blank.' />" />
                                            --%>
                                        </td>
                                        <td class="leftTD">Kinaxis-Marketing Manager
                                          <%--  <asp:Label ID="labelddlMKTMNGER" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                            <asp:Label ID="labeltxtMKTMNGER" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtMKTMNGER" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="rfvtxtMKTMNGER" runat="server" ControlToValidate="txtMKTMNGER"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Marketing Manager cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Marketing Manager cannot be blank.' />" />

                                            <%--<asp:DropDownList ID="ddlMKTMNGER" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlMKTMNGER" runat="server" ControlToValidate="ddlMKTMNGER"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Marketing Manager cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Marketing Manager cannot be blank.' />" />--%>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>

                                    <tr>
                                        <td class="leftTD">Kinaxis-Division
                                            
                                              <asp:Label ID="labeltxtKXDIV" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtKXDIV" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="rfvtxtKXDIV" runat="server" ControlToValidate="txtKXDIV"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Division cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Division cannot be blank.' />" />


                                            <%-- <asp:Label ID="labelddlKXDIV" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlKXDIV" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlKXDIV" runat="server" ControlToValidate="ddlKXDIV"
                                                ValidationGroup="Classification" ErrorMessage="Kinaxis-Division cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Kinaxis-Division cannot be blank.' />" />
                                            --%>
                                        </td>
                                        <td class="leftTD">Molecule Details
                                            <asp:Label ID="labelddlCS_MOLECULE" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlCS_MOLECULE" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlCS_MOLECULE" runat="server" ControlToValidate="ddlCS_MOLECULE"
                                                ValidationGroup="Classification" ErrorMessage="Molecule Details cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Molecule Details cannot be blank.' />" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">Material Grouping for PASX
                                            <%--<asp:Label ID="labelddlMGFPASX" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlMGRPPX" runat="server" AppendDataBoundItems="false"
                                                TabIndex="11">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <%--   <asp:RequiredFieldValidator ID="rfvddlMGFPASX" runat="server" ControlToValidate="ddlMGFPASX"
                                                ValidationGroup="Classification" ErrorMessage="Material Grouping for PASX cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Grouping for PASX cannot be blank.' />" />
                                            --%>
                                        </td>
                                        <td class="leftTD" colspan="2"></td>
                                    </tr>

                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <%-- PROV-CCP-MM-941-23-0045 End--%>
                                    <tr>
                                        <td class="tdSpace" colspan="4" style="color: Purple; font-weight: bolder;">Class Type : 023 | Class : SLED
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">Allowed Manufacturers
                                            <asp:Label ID="labletxtAllowed_Manufacturers" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtAllowed_Manufacturers" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtAllowed_Manufacturers" runat="server" ControlToValidate="txtAllowed_Manufacturers"
                                                ValidationGroup="Classification" ErrorMessage="Allowed Manufacturers cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Allowed Manufacturers cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">HSAN MATERIAL IDENTIFIER
                                            <asp:Label ID="labletxtHSAN_MATERIAL_IDENTIFIER" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtHSAN_MATERIAL_IDENTIFIER" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtHSAN_MATERIAL_IDENTIFIER" runat="server" ControlToValidate="txtHSAN_MATERIAL_IDENTIFIER"
                                                ValidationGroup="Classification" ErrorMessage="HSAN MATERIAL IDENTIFIER cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='HSAN MATERIAL IDENTIFIER cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">Expiration date shelf life
                                            <asp:Label ID="labletxtExpiration_date_shelf_life" runat="server" ForeColor="Red"
                                                Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtExpiration_date_shelf_life" runat="server" CssClass="textbox"
                                                MaxLength="30" Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtExpiration_date_shelf_life" runat="server"
                                                ControlToValidate="txtExpiration_date_shelf_life" ValidationGroup="Classification"
                                                ErrorMessage="Expiration date shelf life cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Expiration date shelf life cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">Next Insp Date for Batch
                                            <asp:Label ID="labletxtNext_Insp_Date_for_Batch" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtNext_Insp_Date_for_Batch" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtNext_Insp_Date_for_Batch" runat="server" ControlToValidate="txtNext_Insp_Date_for_Batch"
                                                ValidationGroup="Classification" ErrorMessage="Next Insp Date for Batch cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Next Insp Date for Batch cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">Batch number
                                            <asp:Label ID="labletxtBatch_number" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtBatch_number" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtBatch_number" runat="server" ControlToValidate="txtBatch_number"
                                                ValidationGroup="Classification" ErrorMessage="Batch number cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Batch number cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">ASSAY ASIS
                                            <asp:Label ID="labletxtASSAY_ASIS" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtASSAY_ASIS" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtASSAY_ASIS" runat="server" ControlToValidate="txtASSAY_ASIS"
                                                ValidationGroup="Classification" ErrorMessage="ASSAY ASIS cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='ASSAY ASIS cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">MANUFACTURER
                                            <asp:Label ID="labletxtMANUFACTURER" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtMANUFACTURER" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtMANUFACTURER" runat="server" ControlToValidate="txtMANUFACTURER"
                                                ValidationGroup="Classification" ErrorMessage="MANUFACTURER cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='MANUFACTURER cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">Potency as is basis
                                            <asp:Label ID="labletxtPotency_as_is_basis" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtPotency_as_is_basis" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtPotency_as_is_basis" runat="server" ControlToValidate="txtPotency_as_is_basis"
                                                ValidationGroup="Classification" ErrorMessage="Potency as is basis cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Potency as is basis cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">Loss on Drying
                                            <asp:Label ID="labletxtLoss_on_Drying" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtLoss_on_Drying" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtLoss_on_Drying" runat="server" ControlToValidate="txtLoss_on_Drying"
                                                ValidationGroup="Classification" ErrorMessage="Loss on Drying cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Loss on Drying cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">Potency as is basis1
                                            <asp:Label ID="labletxtPotency_as_is_basis1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtPotency_as_is_basis1" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtPotency_as_is_basis1" runat="server" ControlToValidate="txtPotency_as_is_basis1"
                                                ValidationGroup="Classification" ErrorMessage="Potency as is basis1 cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Potency as is basis1 cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">RM402217
                                            <asp:Label ID="labletxtRM402217" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtRM402217" runat="server" CssClass="textbox" MaxLength="30" Width="180px"
                                                TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtRM402217" runat="server" ControlToValidate="txtRM402217"
                                                ValidationGroup="Classification" ErrorMessage="RM402217 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='RM402217 cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">RM323350
                                            <asp:Label ID="labletxtRM323350" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtRM323350" runat="server" CssClass="textbox" MaxLength="30" Width="180px"
                                                TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtRM323350" runat="server" ControlToValidate="txtRM323350"
                                                ValidationGroup="Classification" ErrorMessage="RM323350 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='RM323350 cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">SF110063
                                            <asp:Label ID="labletxtSF110063" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtSF110063" runat="server" CssClass="textbox" MaxLength="30" Width="180px"
                                                TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtSF110063" runat="server" ControlToValidate="txtSF110063"
                                                ValidationGroup="Classification" ErrorMessage="SF110063 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='SF110063 cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">SF900052
                                            <asp:Label ID="labletxtSF900052" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtSF900052" runat="server" CssClass="textbox" MaxLength="30" Width="180px"
                                                TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtSF900052" runat="server" ControlToValidate="txtSF900052"
                                                ValidationGroup="Classification" ErrorMessage="SF900052 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='SF900052 cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">IP4A0047
                                            <asp:Label ID="labletxtIP4A0047" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtIP4A0047" runat="server" CssClass="textbox" MaxLength="30" Width="180px"
                                                TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtIP4A0047" runat="server" ControlToValidate="txtIP4A0047"
                                                ValidationGroup="Classification" ErrorMessage="IP4A0047 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='IP4A0047 cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">Assay by GC
                                            <asp:Label ID="labletxtAssay_by_GC" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtAssay_by_GC" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtAssay_by_GC" runat="server" ControlToValidate="txtAssay_by_GC"
                                                ValidationGroup="Classification" ErrorMessage="Assay by GC cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Assay by GC cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">External Material Group
                                            <asp:Label ID="labletxtExternal_Material_Group" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtExternal_Material_Group" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtExternal_Material_Group" runat="server" ControlToValidate="txtExternal_Material_Group"
                                                ValidationGroup="Classification" ErrorMessage="External Material Group cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='External Material Group cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">Version Number
                                            <asp:Label ID="labletxtVersion_Number" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtVersion_Number" runat="server" CssClass="textbox" MaxLength="30"
                                                Width="180px" TabIndex="10" />
                                            <asp:RequiredFieldValidator ID="reqtxtVersion_Number" runat="server" ControlToValidate="txtVersion_Number"
                                                ValidationGroup="Classification" ErrorMessage="Version Number cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Version Number cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr id="trButton" runat="server" visible="false">
                                        <td class="centerTD" colspan="4">
                                            <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="Classification"
                                                TabIndex="16" UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                            <asp:Button ID="btnSave" runat="server" ValidationGroup="Classification" Text="Save"
                                                CssClass="button" TabIndex="17" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnNext" runat="server" ValidationGroup="Classification" Text="Save & Next"
                                                TabIndex="18" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Classification" ShowMessageBox="true" ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblClassificationId" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="51" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <asp:Label ID="lblspclasstype" runat="server" Style="display: none" />
</asp:Content>
