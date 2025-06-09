<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/BOM/BOMMasterPage.master" AutoEventWireup="true"
    CodeFile="BOMHeader.aspx.cs" Inherits="Transaction_BOM_BOMHeader" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        var autocompleteOpen = false;
        var itemselected = false;
        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {
            //you need to re-bind your jquery events here
            AutoMaterialName();
            AutoComponent();
        });
        $(function () {
            AutoMaterialName();
            AutoComponent();
        })
        function AutoMaterialName() {
            var disable = false;
            var type = 'FG'
            //alert($("#<%=txtMaterial.ClientID%>").val());
            $("#<%=txtMaterial.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../Service.svc/GetMaterial",
                        data: '{ "strMaterial": "' + request.term + '","Flag": "' + type + '"}',
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {

                            response($.map(data.d, function (Name) {
                                return {
                                    value: Name,
                                    result: Name
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                select: function (event, ui) {

                },
                focus: function (event, ui) {

                },
                minLength: 0
            })
        };
        function AutoComponent() {
            var disable = false;
            var type = 'SFG'
            $("#<%=txtComponent.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../../Service.svc/GetMaterial",
                        data: '{ "strMaterial": "' + request.term + '","Flag": "' + type + '"}',
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {

                            response($.map(data.d, function (Name) {
                                return {
                                    value: Name,
                                    result: Name
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                select: function (event, ui) {
                    SetMaterialPara(ui.item.value);
                },
                focus: function (event, ui) {
                    $("#<%=txtMaterial.ClientID%>").val(ui.item.value);
                },
                minLength: 0
            })
            };
            function SetMaterialPara(strMaterial) {

                $.ajax({
                    type: "POST",
                    url: "../../Service.svc/GetBOMDetail",
                    data: '{"strMaterial": "' + strMaterial + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d.length > 0) {
                            //alert((data.d[0].Component_UOM));
                            $("#<%=txtComponentUOM.ClientID%>").val(data.d[0].Component_UOM);
                      //  $("#<%=txtBaseQty.ClientID%>").val(data.d[0].Base_Quantity);
                        $("#<%=txtBaseQtyUOM.ClientID%>").val(data.d[0].Base_Quantity_UOM);
                    }

                },
                error: function (a) {
                    alert("Failed to load data");
                }
            });
        }
    </script>
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="trHeading" align="center" colspan="2">BOM Header
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" colspan="2">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">Material
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtMaterial" runat="server" CssClass="textboxAutocomplete" MaxLength="18" Width="180" />
                            <asp:RequiredFieldValidator ID="reqtxtMaterial" runat="server" ControlToValidate="txtMaterial"
                                ValidationGroup="save" ErrorMessage="Material cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Material cannot be blank.' />" />
                        </td>
                        <td class="leftTD" width="20%">Plant
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <cc1:DropDownCheckBoxes ID="ddlPlant" runat="server" AddJQueryReference="false" UseButtons="false"
                                UseSelectAllNode="true" AutoPostBack="false">
                                <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="250" DropDownBoxBoxHeight="80" />
                                <Texts SelectBoxCaption="--Select--" />
                            </cc1:DropDownCheckBoxes>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">BOM Usage
                        </td>
                        <td class="rigthTD" style="width: 30%" colspan="3">
                            <asp:DropDownList ID="ddlBOMUsage" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                                <asp:ListItem Text="BOM1" Value="1" />
                                <asp:ListItem Text="BOM2" Value="2" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlBOMUsage" runat="server" ControlToValidate="ddlBOMUsage"
                                ValidationGroup="next" ErrorMessage="Select BOM Usage." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../images/Error.png' title='Select BOM Usage.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" style="width: 20%">Valid From
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtValidFrom" runat="server" CssClass="textbox" MaxLength="10" Width="180" />
                            <act:CalendarExtender ID="txtValidFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtValidFrom">
                            </act:CalendarExtender>
                            <asp:RequiredFieldValidator ID="reqtxtValidFrom" runat="server" ControlToValidate="txtValidFrom"
                                ValidationGroup="save" ErrorMessage="Valid From cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Valid From cannot be blank.' />" />
                        </td>
                        <td class="leftTD" style="width: 20%">Valid To
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtValidTo" runat="server" CssClass="textbox" MaxLength="10" Width="180" />
                            <act:CalendarExtender ID="txtValidTo_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtValidTo">
                            </act:CalendarExtender>
                            <asp:RequiredFieldValidator ID="reqtxtValidTo" runat="server" ControlToValidate="txtValidTo"
                                ValidationGroup="save" ErrorMessage="Valid To cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Valid To cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="4">
                            <asp:Button ID="btnNext" runat="server" ValidationGroup="save" Text="Save & Next"
                                CssClass="button"  Width="120px" />
                                <%--OnClick="btnNext_Click"--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="4">
                            <asp:Panel ID="pnldetail" runat="server" Visible="false">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="tdSpace" colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">Component
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtComponent" runat="server" CssClass="textboxAutocomplete" MaxLength="18" Width="180" />
                                            <asp:RequiredFieldValidator ID="reqtxtComponent" runat="server" ControlToValidate="txtComponent"
                                                ValidationGroup="Add" ErrorMessage="Component cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Component cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">Quantity
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="textbox" MaxLength="18" Width="180"  onkeypress="return IsNumber();"/>
                                            <asp:RequiredFieldValidator ID="reqtxtQuantity" runat="server" ControlToValidate="txtQuantity"
                                                ValidationGroup="Add" ErrorMessage="Quantity cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Quantity cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">Component UOM
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtComponentUOM" runat="server" CssClass="textboxAutocomplete" MaxLength="3"
                                                Width="180" onfocus="return ComponentUOMOnFocus();" onchange="return ComponentUOMTextChangeEvent();" />
                                            <asp:RequiredFieldValidator ID="reqtxtComponentUOM" runat="server" ControlToValidate="txtComponentUOM"
                                                ValidationGroup="Add" ErrorMessage="Component UOM cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Component UOM cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">ASM
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:CheckBox ID="chkASM" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">Base Quantity
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtBaseQty" runat="server" CssClass="textbox" MaxLength="13" Width="180"  onkeypress="return IsNumber();"/>
                                            <asp:RequiredFieldValidator ID="reqtxtBaseQty" runat="server" ControlToValidate="txtBaseQty"
                                                ValidationGroup="Add" ErrorMessage="Base Quantity cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Base Quantity cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">Base Quantity UOM
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtBaseQtyUOM" runat="server" CssClass="textboxAutocomplete" MaxLength="3"
                                                Width="180" onfocus="return BaseQtyUOMOnFocus();" onchange="return BaseQtyUOMTextChangeEvent();" />
                                            <asp:RequiredFieldValidator ID="reqtxtBaseQtyUOM" runat="server" ControlToValidate="txtBaseQtyUOM"
                                                ValidationGroup="Add" ErrorMessage="Base Quantity UOM cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Base Quantity UOM cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">BOM Status
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtBOMStatus" runat="server" CssClass="textboxAutocomplete" MaxLength="2" Width="180" 
                                                onfocus="return BOMStatusOnFocus();" onchange="return BOMStatusTextChangeEvent();" />
                                            <asp:RequiredFieldValidator ID="reqtxtBOMStatus" runat="server" ControlToValidate="txtBOMStatus"
                                                ValidationGroup="Add" ErrorMessage="BOM Status cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='BOM Status cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">Component Scrap (%)
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtCompScrap" runat="server" CssClass="textbox" MaxLength="10" Width="180"  onkeypress="return IsNumber();"/>
                                            <asp:RequiredFieldValidator ID="reqtxtCompScrap" runat="server" ControlToValidate="txtCompScrap"
                                                ValidationGroup="Add" ErrorMessage="Component Scrap (%) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Component Scrap (%) cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">Item Category
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <%--<asp:TextBox ID="txtItemCat" runat="server" CssClass="textboxAutocomplete" MaxLength="1" Width="180"
                                                 onfocus="return ItemCatOnFocus();" onchange="return ItemCatTextChangeEvent();" />--%>

                                                 <asp:TextBox ID="txtItemCat" runat="server" CssClass="textbox" MaxLength="1" Width="15" Text="L" ReadOnly="true"
                                                  />
                                            <asp:RequiredFieldValidator ID="reqtxtItemCat" runat="server" ControlToValidate="txtItemCat"
                                                ValidationGroup="Add" ErrorMessage="Item Category cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Item Category cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">Costing Relevancy
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtCostingRel" runat="server" CssClass="textboxAutocomplete" MaxLength="1"
                                                Width="180" onfocus="return CostingRelOnFocus();" onchange="return CostingRelTextChangeEvent();" />
                                            <asp:RequiredFieldValidator ID="reqtxtCostingRel" runat="server" ControlToValidate="txtCostingRel"
                                                ValidationGroup="Add" ErrorMessage="Costing Relevancy cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Costing Relevancy cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="centerTD" colspan="4">
                                            <asp:Button ID="btnAdd" runat="server" ValidationGroup="Add" Text="ADD" CssClass="button" Width="120px" />
                                            <%--OnClick="btnAdd_Click" --%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <asp:GridView ID="grd" runat="server" DataKeyNames="BOM_HeaderDetail_Id" AutoGenerateColumns="false"
                                                Width="100%" BorderColor="#9D9D9D" CellSpacing="1" CellPadding="1" OnRowCommand="grd_RowCommand">
                                                <RowStyle CssClass="light-gray" />
                                                <HeaderStyle CssClass="gridHeader" />
                                                <AlternatingRowStyle CssClass="gridRowStyoe" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBOM_HeaderDetail_Id" runat="server" Text='<%# Eval("BOM_HeaderDetail_Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderStyle-Width="30%" DataField="Component" HeaderText="Component" />
                                                    <asp:BoundField HeaderStyle-Width="14%" DataField="Quantity" HeaderText="Quantity" />
                                                    <asp:BoundField HeaderStyle-Width="14%" DataField="Component_UOM" HeaderText="Component UOM" />
                                                    <asp:BoundField HeaderStyle-Width="13%" DataField="ASM" HeaderText="ASM" />
                                                    <asp:BoundField HeaderStyle-Width="13%" DataField="Base_Quantity" HeaderText="Base Quantity" />
                                                    <asp:BoundField HeaderStyle-Width="30%" DataField="Base_Qunatity_UOM" HeaderText="Base Qunatity UOM" />
                                                    <asp:BoundField HeaderStyle-Width="14%" DataField="BOM_Status" HeaderText="BOM Status" />
                                                    <asp:BoundField HeaderStyle-Width="14%" DataField="Comp_Scrap_Per" HeaderText="Component Scrap (%)" />
                                                    <asp:BoundField HeaderStyle-Width="13%" DataField="Item_Category" HeaderText="Item Category" />
                                                    <asp:BoundField HeaderStyle-Width="13%" DataField="Costing_Relevncy" HeaderText="Costing Relevncy" />
                                                    <asp:TemplateField HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center" HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" BorderStyle="None" BorderColor="white" CommandArgument='<%# Eval("BOM_HeaderDetail_Id") %>'
                                                                runat="server" CommandName="Del">
                                                                    <img src="../../images/delete.png" style="border:none; border-color:white" alt="Delete" title="Delete this row." /> 
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="4">
                            <asp:Panel ID="pnldetailV" runat="server" Visible="false">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="tdSpace" colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <asp:GridView ID="grdV" runat="server" DataKeyNames="BOM_HeaderDetail_Id" AutoGenerateColumns="false"
                                                Width="100%" BorderColor="#9D9D9D" CellSpacing="1" CellPadding="1">
                                                <RowStyle CssClass="light-gray" />
                                                <HeaderStyle CssClass="gridHeader" />
                                                <AlternatingRowStyle CssClass="gridRowStyoe" />
                                                <Columns>
                                                    <asp:BoundField HeaderStyle-Width="30%" DataField="Component" HeaderText="Component" />
                                                    <asp:BoundField HeaderStyle-Width="14%" DataField="Quantity" HeaderText="Quantity" />
                                                    <asp:BoundField HeaderStyle-Width="14%" DataField="Component_UOM" HeaderText="Component UOM" />
                                                    <asp:BoundField HeaderStyle-Width="13%" DataField="ASM" HeaderText="ASM" />
                                                    <asp:BoundField HeaderStyle-Width="13%" DataField="Base_Quantity" HeaderText="Base Quantity" />
                                                    <asp:BoundField HeaderStyle-Width="30%" DataField="Base_Qunatity_UOM" HeaderText="Base Qunatity UOM" />
                                                    <asp:BoundField HeaderStyle-Width="14%" DataField="BOM_Status" HeaderText="BOM Status" />
                                                    <asp:BoundField HeaderStyle-Width="14%" DataField="Comp_Scrap_Per" HeaderText="Component Scrap (%)" />
                                                    <asp:BoundField HeaderStyle-Width="13%" DataField="Item_Category" HeaderText="Item Category" />
                                                    <asp:BoundField HeaderStyle-Width="13%" DataField="Costing_Relevncy" HeaderText="Costing Relevncy" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center" class="centerTD">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" OnClick="btnSubmit_Click"
                                Visible="false" />
                            <asp:Button ID="btnRejectTo" runat="server" Text="Reject" CssClass="button" OnClientClick="return ShowRollbackPopup();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="save" ShowMessageBox="true"
        ShowSummary="false" />
    <div id="divRejectTo" style="display: none;" title="Reject Request">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
            <tr>
                <td class="leftTD" style="width: 25%">Reject To
                </td>
                <td class="rigthTD">
                    <asp:DropDownList ID="ddlRejectTo" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select" Value="" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvq" SetFocusOnError="true" Display="Dynamic" Text="<img src='../images/Error.png' title='Select Department.'"
                        ControlToValidate="ddlRejectTo" InitialValue="" runat="server" ForeColor="Red"
                        ValidationGroup="reject" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="tdSpace"></td>
            </tr>
            <tr>
                <td class="leftTD" valign="top">Remark
                </td>
                <td class="rigthTD">
                    <asp:TextBox ID="txtRejectNote" runat="server" CssClass="textarea" TextMode="MultiLine"
                        Height="40px" Width="350px" Style="text-transform: none;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" SetFocusOnError="true"
                        Display="Dynamic" Text="<img src='../images/Error.png' title='Enter Remark.'"
                        ControlToValidate="txtRejectNote" runat="server" ForeColor="Red" ValidationGroup="reject" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="tdSpace"></td>
            </tr>
            <tr>
                <td class="leftTD">&nbsp;
                </td>
                <td class="rigthTD">
                    <asp:Button ID="btnRollback" runat="server" Text="Reject" CssClass="button" OnClick="btnRollback_Click"
                        ValidationGroup="reject" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblBOMHeaderlId" runat="server" Visible="false" />
    <asp:Label ID="lblCostingId" runat="server" Visible="false" />
    <asp:Label ID="lblIsUserApprover" runat="server" Visible="false" />
    <script type="text/javascript">
        function ShowRollbackPopup() {
            $("#divRejectTo").dialog({
                height: 210,
                width: 550,
                modal: true,
                closeOnEscape: true,
                draggable: true,
                resizable: false,
                position: 'center',
                dialogClass: 'alert',
                //show: { effect: 'drop', duration: 500 },
                //hide: { effect: 'explode', duration: 100 },
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                    $(this).show('clip');
                }
            });
            return false;
        }

    </script>

    <script type="text/javascript">
        var textboxId = "";
        var textboxRealId = "";

        function ComponentUOMOnFocus() {
            textboxId = $('#<%= txtComponentUOM.ClientID%>').attr('ID');
            textboxRealId = "txtComponentUOM";
            AutoCompleteLookUpBOM();
        }

        function ComponentUOMTextChangeEvent() {
            CheckLookupBOM($('#<%= txtComponentUOM.ClientID%>').attr('ID'), "txtComponentUOM", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function BaseQtyUOMOnFocus() {
            textboxId = $('#<%= txtBaseQtyUOM.ClientID%>').attr('ID');
            textboxRealId = "txtBaseQtyUOM";
            AutoCompleteLookUpBOM();
        }

        function BaseQtyUOMTextChangeEvent() {
            CheckLookupBOM($('#<%= txtBaseQtyUOM.ClientID%>').attr('ID'), "txtBaseQtyUOM", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function BOMStatusOnFocus() {
            textboxId = $('#<%= txtBOMStatus.ClientID%>').attr('ID');
            textboxRealId = "txtBOMStatus";
            AutoCompleteLookUpBOM();
        }

        function BOMStatusTextChangeEvent() {
            CheckLookupBOM($('#<%= txtBOMStatus.ClientID%>').attr('ID'), "txtBOMStatus", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function ItemCatOnFocus() {
            textboxId = $('#<%= txtItemCat.ClientID%>').attr('ID');
            textboxRealId = "txtItemCat";
            AutoCompleteLookUpBOM();
        }

        function ItemCatTextChangeEvent() {
            CheckLookupBOM($('#<%= txtItemCat.ClientID%>').attr('ID'), "txtItemCat", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function CostingRelOnFocus() {
            textboxId = $('#<%= txtCostingRel.ClientID%>').attr('ID');
            textboxRealId = "txtCostingRel";
            AutoCompleteLookUpBOM();
        }

        function CostingRelTextChangeEvent() {
            CheckLookupBOM($('#<%= txtCostingRel.ClientID%>').attr('ID'), "txtCostingRel", $('#<%= btnNext.ClientID%>').attr('ID'));
        }
    </script>
</asp:Content>
