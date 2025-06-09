<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/BOM/BOMMasterPage.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="BOMHeaderComp.aspx.cs"
    Inherits="Transaction_BOM_BOMHeaderComp" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
    <script type="text/javascript">
        function showModalPopupViaClient() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
       
    </script>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlAddNew" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            BOM Header/Component
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 130px;">
                                        Material Number
                                        <asp:Label ID="labletxtMaterialNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="vertical-align: middle;">
                                        <asp:TextBox ID="txtMaterialNo" runat="server" CssClass="textbox" MaxLength="18"
                                            onkeypress="return IsNumber();" Width="180" onkeydown="return (event.keyCode!=13);"
                                            OnTextChanged="txtMaterialNo_TextChanged" AutoPostBack="true" />
                                        <asp:ImageButton ID="imgHelpSearchMaterial" runat="server" ImageUrl="~/images/search_icon.png"
                                            Height="15px" Width="15px" Style="padding-top: 1px;" TabIndex="100" OnClick="imgHelpSearchMaterial_Click" />
                                        <asp:RequiredFieldValidator ID="reqtxtMaterialNo" runat="server" ControlToValidate="txtMaterialNo"
                                            ValidationGroup="save" ErrorMessage="Material Number cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Material Description
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMaterialDesc" runat="server" CssClass="textbox" MaxLength="40"
                                            size="41" Width="281" Enabled="false" />
                                        <asp:RequiredFieldValidator ID="reqtxtMaterialDesc" runat="server" ControlToValidate="txtMaterialDesc"
                                            ValidationGroup="save" ErrorMessage="Material Description cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Description cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Plant
                                        <asp:Label ID="labeltxtPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" TabIndex="1"
                                            Enabled="false">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                            ValidationGroup="save" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        BOM Usage
                                        <asp:Label ID="labelddlBOMUsage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlBOMUsage" runat="server" OnSelectedIndexChanged="ddlBOMUsage_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlBOMUsage" runat="server" ControlToValidate="ddlBOMUsage"
                                            ValidationGroup="save" ErrorMessage="BOM Usage cannot be blank." SetFocusOnError="true"
                                            InitialValue="" Display="Dynamic" Text="<img src='../../images/Error.png' title='BOM Usage cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Alternative BOM
                                        <%--<asp:Label ID="labeltxtAlternativeBOM" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtAlternativeBOM" runat="server" CssClass="textbox" MaxLength="3"
                                            Width="156px" Enabled = "false"/>
                                        <%--<asp:TextBox ID="txtAlternativeBOM" runat="server" CssClass="textbox" MaxLength="3"
                                            Width="156px" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtAlternativeBOM" runat="server" ControlToValidate="txtAlternativeBOM"
                                            ValidationGroup="save" ErrorMessage="Alternative BOM cannot be blank." SetFocusOnError="true"
                                            InitialValue="" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alternative BOM cannot be blank.' />" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        BOM Text
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtBOMText" runat="server" MaxLength="600" CssClass="textbox" TextMode="MultiLine" />
                                    </td>
                                    <td class="leftTD">
                                        Alternative Text
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtAlternativeText" runat="server" MaxLength="40" CssClass="textbox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="6">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Base Quantity
                                        <asp:Label ID="labeltxtBaseQuantity" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtBaseQuantity" runat="server" MaxLength="13" CssClass="textbox"
                                            onkeypress="return IsNumber();" Width="130px" />
                                        <asp:RequiredFieldValidator ID="reqtxtBaseQuantity" runat="server" ControlToValidate="txtBaseQuantity"
                                            ValidationGroup="save" ErrorMessage="Base Quantity cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Base Quantity cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtBaseQuantity" ControlToValidate="txtBaseQuantity"
                                            runat="server" ErrorMessage="Base Quantity should have numeric value up to 3 decimal place only."
                                            Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                            Text="<img src='../../images/Error.png' title='Base Quantity should have numeric value up to 3 decimal place only.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Base UOM
                                        <asp:Label ID="labeltxtBaseUOM" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtBaseUOM" runat="server" MaxLength="3" CssClass="textbox" Width="30px"
                                            Enabled="false" />
                                        <asp:RequiredFieldValidator ID="reqtxtBaseUOM" runat="server" ControlToValidate="txtBaseUOM"
                                            ValidationGroup="save" ErrorMessage="BOM header Base UOM cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='BOM header Base UOM cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        BOM status
                                        <asp:Label ID="labelddlBomStatus" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlBomStatus" runat="server" Enabled="false">
                                            <asp:ListItem Text="Select" Value="" />
                                            <asp:ListItem Text="01-Active" Value="01" Selected="True" />
                                            <asp:ListItem Text="02-Inactive" Value="02" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlBomStatus" runat="server" ControlToValidate="ddlBomStatus"
                                            ValidationGroup="save" ErrorMessage="BOM status cannot be blank." SetFocusOnError="true"
                                            InitialValue="" Display="Dynamic" Text="<img src='../../images/Error.png' title='BOM status cannot be blank.' />" />
                                    </td>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="width: 1089px; overflow: auto; padding-bottom: 10px; max-height: 320px;">
                                <asp:GridView ID="grdBOMDetailAdd" runat="server" AutoGenerateColumns="false" DataKeyNames="BOM_HeaderDetail_Id,Item_Category,Spare_Part_Indicator,StorageLocation,starategy,Relevancy_To_Costing,Comp_SortString1,Alt_Item_Group,Component_UOM,ActiveFiller"
                                    Width="1600px" CssClass="GridClass" ShowHeaderWhenEmpty="true" Visible="true"
                                    ShowFooter="false" AllowSorting="true" OnRowCommand="grdBOMDetailAdd_RowCommand"
                                    OnRowDataBound="grdBOMDetailAdd_RowDataBound">
                                    <FooterStyle CssClass="gridFooter" />
                                    <RowStyle CssClass="light-gray" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <AlternatingRowStyle CssClass="gridRowStyle" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="D" CausesValidation="false">  
                                        <img src="../../images/delete.png" alt="Delete" title='Delete'/></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBomDetailId" runat="server" Text='<%#Eval("BOM_HeaderDetail_Id") %>'
                                                    Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Position No." HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPositionNumber" runat="server" MaxLength="4" CssClass="textbox"
                                                    Text='<%#Eval("Postion_No") %>' Width="40px" />
                                                <asp:RequiredFieldValidator ID="reqtxtPositionNumber" runat="server" ControlToValidate="txtPositionNumber"
                                                    ValidationGroup="save" ErrorMessage="Position Number cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Position Number cannot be blank.' />" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            Visible="false" HeaderText="Comp_Type" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="hdnCompType" runat="server" MaxLength="4" CssClass="textbox" Visible="false"
                                                    Text='<%#Eval("Comtype") %>' Width="40px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="IC" HeaderStyle-Width="100px" Visible="false">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlItemCategory" runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left" 
                                            HeaderText="Component" HeaderStyle-Width="250px" ItemStyle-Width="250px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtComponent" runat="server" MaxLength="8" CssClass="textbox" Width="80px"
                                                    AutoPostBack="true" Text='<%#Eval("Component") %>' onkeydown="return (event.keyCode!=13);"
                                                    OnTextChanged="txtComponent_TextChanged" />
                                                <asp:ImageButton ID="imgHSrchMat" runat="server" ImageUrl="~/images/search_icon.png"
                                                    Height="15px" Width="15px" Style="padding-top: 1px;" OnClick="imgHSrchMat_Click" />
                                                <asp:RequiredFieldValidator ID="reqtxtComponent" runat="server" ControlToValidate="txtComponent"
                                                    ValidationGroup="save" ErrorMessage="Component cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Component cannot be blank.' />" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Component Desc." HeaderStyle-Width="220px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtComponentDesc" runat="server" CssClass="textbox" MaxLength="80"
                                                    Width="250px" Text='<%#Eval("Component_desc") %>' onkeydown="return (event.keyCode!=13);"
                                                    Enabled="false" />
                                                <asp:RequiredFieldValidator ID="reqtxtComponentDesc" runat="server" ControlToValidate="txtComponentDesc"
                                                    ValidationGroup="save" ErrorMessage="Component Desc. cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Component Desc. cannot be blank.' />" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Quantity") %>' MaxLength="13"
                                                    onkeypress="return IsNumber();" CssClass="textbox" Width="130px" ToolTip="Quantity" />
                                                <asp:RequiredFieldValidator ID="reqtxtQuantity" runat="server" ControlToValidate="txtQuantity"
                                                    ValidationGroup="save" ErrorMessage="Component Base Quantity cannot be blank."
                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Component Base Quantity cannot be blank.' />" />
                                                <asp:RegularExpressionValidator ID="regtxtQuantity" ControlToValidate="txtQuantity"
                                                    runat="server" ErrorMessage="Component Base Quantity should have numeric value up to 3 decimal place only."
                                                    Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                    Text="<img src='../../images/Error.png' title='Component Base Quantity should have numeric value up to 3 decimal place only.' />" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="UOM">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlCompenentUnitOfMeasure" runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlCompenentUnitOfMeasure" runat="server" ControlToValidate="ddlCompenentUnitOfMeasure"
                                                    ValidationGroup="save" ErrorMessage="Component UOM cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Component UOM cannot be blank.' />" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Spare part Indi." HeaderStyle-Width="100px" Visible="false">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlSparePartIndicator" runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Storage Loc." HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlStorageLocation" runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Alt. Item Grp" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlAlternativeItemGroup" runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Priority" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPriority" runat="server" Text='<%#Eval("Priority") %>' BorderColor="YellowGreen"
                                                    CssClass="textbox" MaxLength="2" Width="20px" AutoPostBack="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Strategy" Visible="false">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlStrategy" runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Usage Prob." HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtUsageProbability" runat="server" Text='<%#Eval("Usage_Probebilty") %>'
                                                    CssClass="textbox" MaxLength="3" Width="30px" ToolTip="Usage Probability" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Indi. Relevancy Costing" HeaderStyle-Width="100px" Visible="false">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlIndicatorRelavancyToCosting" runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="Select" Value="0" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Remarks" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox" Text='<%#Eval("Remarks") %>'
                                                    MaxLength="250" TextMode="MultiLine" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="ASM" Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkASM" runat="server" />
                                                <asp:HiddenField ID="hdnASM" Visible="false" runat="server" Value='<%#Eval("ASM") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Phantom Indi." Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPhantomIndicator" runat="server" ToolTip="Phantom Indicator" />
                                                <asp:HiddenField ID="hdnPhantomIndicator" runat="server" Value='<%#Eval("Phantom_Indicator") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Comp. Scrap(%)" HeaderStyle-Width="120px" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtComponentScrap" runat="server" Text='<%#Eval("Component_Scrap") %>'
                                                    CssClass="textbox" MaxLength="4" Width="40px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Recursive BOM">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRecursiveBOM" runat="server" ToolTip="Recursive BOM" Enabled="false" />
                                                <asp:HiddenField ID="hdnRecursiveBOM" runat="server" Value='<%#Eval("RecursiveBOM") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Sort String">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlCompSortString" runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Active/Filler">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlActFil" runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="Select" Value="" />
                                                    <asp:ListItem Text="Active" Value="A" />
                                                    <asp:ListItem Text="Filler" Value="F" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Act/Fill Combi.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtComb" runat="server" CssClass="textbox" Text='<%#Eval("Combination") %>'
                                                    Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Qty. Is Fixed" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkQtyIsFixed" runat="server" ToolTip="Quantity is Fixed" />
                                                <asp:HiddenField ID="hdnQtyIsFixed" runat="server" Value='<%#Eval("Qty_Is_Fixed1") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Valid From">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtValidFrom" runat="server" Text='<%#Eval("Valid_From") %>' CssClass="textbox"
                                                    MaxLength="10" Width="65px" />
                                                <act:CalendarExtender ID="caltxtValidFrom" runat="server" TargetControlID="txtValidFrom"
                                                    PopupPosition="BottomLeft" Format="dd/MM/yyyy" Animated="true">
                                                </act:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Valid To">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtValidTo" runat="server" Text='<%#Eval("Valid_to") %>' CssClass="textbox"
                                                    MaxLength="10" Width="65px" />
                                                <act:CalendarExtender ID="calExder1" runat="server" TargetControlID="txtValidTo"
                                                    PopupPosition="BottomLeft" Format="dd/MM/yyyy" Animated="true">
                                                </act:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="BOM Item Text Line 1" HeaderStyle-Width="50px" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBomItemText1" runat="server" CssClass="textbox" Text='<%#Eval("BOM_Item_Text1") %>'
                                                    MaxLength="40" ToolTip="BOM Item Text 1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="BOM Item Text Line 2" HeaderStyle-Width="50px" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBomItemText2" runat="server" CssClass="textbox" Text='<%#Eval("BOM_Item_Text2") %>'
                                                    MaxLength="40" ToolTip="BOM Item Text 2" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            Visible="false" HeaderText="Item_node" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="hdnItemNode" runat="server" MaxLength="8" CssClass="textbox" Visible="true"
                                                    Enabled="false" Text='<%#Eval("Item_node_number") %>' Width="40px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUpd_Flag" runat="server" Text='<%#Eval("Upd_Flag") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div style="width: 1089px; overflow: auto; padding-bottom: 10px; max-height: 320px;">
                                <asp:GridView ID="grdBomDetailsView" runat="server" AutoGenerateColumns="false" DataKeyNames="BOM_HeaderDetail_Id"
                                    Width="1500px" CssClass="GridClass" ShowHeaderWhenEmpty="true" Visible="false"
                                    ShowFooter="false">
                                    <FooterStyle CssClass="gridFooter" />
                                    <RowStyle CssClass="light-gray" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <AlternatingRowStyle CssClass="gridRowStyle" />
                                    <Columns>
                                        <asp:BoundField HeaderText="BOM Detail ID." DataField="BOM_HeaderDetail_Id" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:BoundField HeaderText="Position No." DataField="Postion_No" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="IC" DataField="Item_Category" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Component" DataField="Component" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Comp. Desc." DataField="Component_desc" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Qty" DataField="Quantity" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Comp.UOM" DataField="Component_UOM" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <%--<asp:BoundField HeaderText="Grade" DataField="Comp_Grade" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Molecule" DataField="Comp_Molecule" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center" />--%>
                                        <asp:BoundField HeaderText="Sort String" DataField="Comp_SortString1" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Qty. Is Fixed" DataField="Qty_Is_Fixed1" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Spare part Indi." DataField="Spare_Part_Indicator" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Storage Loc." DataField="StorageLocation" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Alt. Item Grp" DataField="Alt_Item_Group" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Priority" DataField="Priority" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Strategy" DataField="starategy" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Usage Prob." DataField="Usage_Probebilty" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Indi. Relevancy Costing" DataField="Relevancy_To_Costing"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <%-- <asp:BoundField HeaderText="Bulk Indi." DataField="Bulk_Indicator" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderText="Long Text">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLongText" runat="server" Text='<%#Eval("Long_Text") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        <%-- Added by chandrasekar S on 01.11.2014 for Formula of Formulation BOM types Begin --%>
                                        <%--<asp:BoundField HeaderText="Distribute" DataField="Distribute" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Relationship" DataField="Relationship" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Percentage" DataField="Percentage" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center" />--%>
                                        <asp:BoundField HeaderText="Remarks" DataField="Remarks" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <%-- Added by chandrasekar S on 01.11.2014 for Formula of Formulation BOM types End --%>
                                        <asp:BoundField HeaderText="ASM" DataField="ASM" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Phantom Indi." DataField="Phantom_Indicator" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Comp. Scrap(%)" DataField="Component_Scrap" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Valid From" DataField="Valid_From" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="75px" />
                                        <asp:BoundField HeaderText="Valid To" DataField="Valid_to" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="65px" />
                                        <asp:BoundField HeaderText="BOM Item Text 1" DataField="BOM_Item_Text1" Visible="false"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="BOM Item Text 2" DataField="BOM_Item_Text2" Visible="false"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="2">
                            <asp:TextBox ID="txtNewRow" runat="server" Text="2" MaxLength="3" Width="20px" CssClass="textbox" />
                            <asp:RangeValidator ID="rangePositionNumber" runat="server" ValidationGroup="addRowValidation"
                                ControlToValidate="txtNewRow" MaximumValue="100" MinimumValue="1" Type="Integer"
                                ErrorMessage="Enter Numeric Value only (Maximum limit 100)." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Enter Numeric Value only (Maximum limit 100).' />"></asp:RangeValidator>
                            <asp:Button ID="btnAdd" runat="server" Text="New Row" ValidationGroup="addRowValidation"
                                OnClick="btnAdd_Click" CssClass="button" UseSubmitBehavior="false" />
                            <%--OnClick="btnAdd_Click"--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr id="trButton" runat="server" visible="false">
                        <td class="centerTD" colspan="2">
                            <asp:Button ID="btnSave" runat="server" ValidationGroup="save" UseSubmitBehavior="true"
                                Text="Save" CssClass="button" TabIndex="27" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="sm" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="save" />
                <asp:ValidationSummary ID="VaSu" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="addRowValidation" />
                <asp:Label ID="lblUserId" runat="server" Visible="false" />
                <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
                <asp:Label ID="lblBOMHeaderId" runat="server" Visible="false" />
                <asp:Label ID="lblMode" runat="server" Visible="false" />
                <asp:Label ID="lblModuleId" runat="server" Visible="false" />
                <asp:Label ID="lblActionType" runat="server" Style="display: none" />
                <asp:Label ID="lblSectionId" runat="server" Text="34" Visible="false" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdPnlSearchMatHELP" runat="server">
        <ContentTemplate>
            <asp:Button runat="server" ID="hdnTrgtCntrlForMatHELP" Style="display: none" />
            <act:ModalPopupExtender ID="modMatSearch" runat="server" TargetControlID="hdnTrgtCntrlForMatHELP"
                BehaviorID="programmaticModalPopupBehavior" CancelControlID="btnCancelMaterialHelp"
                PopupControlID="pnlHelpSearchMat" BackgroundCssClass="modalBackground" DropShadow="true"
                PopupDragHandleControlID="pnlMatSearchTitle" />
            <asp:Panel ID="pnlHelpSearchMat" runat="server" Width="100%">
                <div style="background-color: White; padding: 2px 2px 2px 2px; overflow: scroll;
                    width: 1000px; height: 500px">
                    <asp:Panel ID="pnlMatSearchTitle" runat="server" Style="cursor: move; background-color: Black;
                        border: solid 1px Gray; color: Black">
                        <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                            <span class="ui-dialog-title">Material Search</span>
                        </div>
                    </asp:Panel>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan ="4" style="display:none">
                                <asp:Label ID = "lblType" runat = "server"></asp:Label>
                                <asp:Label ID ="lblRow" runat = "server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdSpace">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 25%">
                                Material Number
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtMaterialNoHELP" runat="server" CssClass="textbox" />
                            </td>
                            <td class="leftTD" style="width: 25%">
                                Material Desc
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtMaterialDescHELP" runat="server" CssClass="textbox" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdSpace">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 25%">
                                <%--BOM Usage--%>
                            </td>
                            <td class="rigthTD" align="left">
                                <asp:TextBox ID="txtBOMusage" Visible="false" runat="server" CssClass="textbox" />
                            </td>
                            <td class="leftTD" style="width: 25%">
                                <%--Alternative BOM--%>
                            </td>
                            <td class="rigthTD" align="left">
                                <asp:TextBox ID="txtAltBOM" Visible="false" runat="server" CssClass="textbox" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdSpace">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Plant <span class="mandatory">*</span>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlPlantHELP" runat="server" Enabled="false">
                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPlantHELP" runat="server" ControlToValidate="ddlPlantHELP"
                                    ValidationGroup="searchmaterial" ErrorMessage="select Plant Code." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='select Plant Code.' />" />
                            </td>
                            <td class="tdSpace" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdSpace">
                            </td>
                        </tr>
                        <tr>
                            <td class="centerTD" colspan="4" style="padding: 0px;">
                                <asp:Button ID="btnSeachMaterialHELP" runat="server" Text="Search" ValidationGroup="searchmaterial"
                                    CssClass="button" Style="padding: 0px;" OnClick="btnSeachMaterialHELP_Click" />&nbsp;
                                <asp:Button ID="btnCancelMaterialHelp" runat="server" Text="Cancel" CssClass="button"
                                    Style="padding: 0px;" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdSpace">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdSpace">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div style="max-height: 200px; overflow: auto; margin-bottom: 5px">
                                    <asp:GridView ID="grdMaterialHELP" runat="server" AutoGenerateColumns="false" DataKeyNames="Material_No"
                                        CssClass="GridClass" ShowHeaderWhenEmpty="true" ShowFooter="false" EmptyDataText="No Data Found"
                                        Width="100%" OnRowDataBound="grdMaterialHELP_RowDataBound" OnRowCommand="grdMaterialHELP_RowCommand">
                                        <HeaderStyle CssClass="gridHeader" />
                                        <FooterStyle CssClass="gridFooter" />
                                        <RowStyle CssClass="grdViewRow" BackColor="#FBFBFB" Font-Size="11px" Font-Names="verdana, Helvetica, sans-serif" />
                                        <AlternatingRowStyle CssClass="grdViewRow" BackColor="#F9FDFF" Font-Size="11px" Font-Names="verdana, Helvetica, sans-serif" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                HeaderText="Material No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMaterialNumberHELP" runat="server" Text='<%#Eval("Material_No") %>' />
                                                    <asp:Label ID="lblBOMUSAGEHELP" CssClass="lblBOMUSAGEHELP" runat="server" Text='<%#Eval("BOM_Usage") %>'
                                                        Visible="false" />
                                                    <asp:Label ID="lblAltBOM" runat="server" Text='<%#Eval("Alternative_BOM") %>' Visible="false" />
                                                    <asp:Label ID="lblPlantHELP" runat="server" Text='<%#Eval("Plant_Id") %>' Visible="false" />
                                                    <asp:Label ID="lblPlantDescHELP" runat="server" Text='<%#Eval("Plant_Name") %>' Visible="false" />
                                                    <asp:Label ID="lblMatDescHELP" runat="server" Text='<%#Eval("Material_Desc") %>'
                                                        Visible="false" />
                                                    <asp:Label ID="lblBaseUOM" runat="server" Text='<%#Eval("BaseUOM") %>' Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Description" DataField="Material_Desc" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Type" DataField="Material_Type" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Base UOM" DataField="BaseUOM" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="BOM_Usage" DataField="BOM_Usage" Visible="false" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Alternative_BOM" DataField="Alternative_BOM" Visible="false"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSeachMaterialHELP" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelMaterialHelp" />
            <asp:AsyncPostBackTrigger ControlID="imgHelpSearchMaterial" />
            <asp:AsyncPostBackTrigger ControlID="grdMaterialHELP" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
