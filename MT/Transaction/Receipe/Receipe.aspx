<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Receipe/ReceipeMasterPage.master"
    AutoEventWireup="true" CodeFile="Receipe.aspx.cs" Inherits="Transaction_Receipe_Receipe" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
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
                Receipe Header
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" colspan="2">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Material
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtMaterial" runat="server" CssClass="textbox" MaxLength="18" Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMaterial"
                                ValidationGroup="save" ErrorMessage="Material cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Plant
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
                        <td class="leftTD" width="20%">
                            Prod Version
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtProd_Version" runat="server" CssClass="textbox" MaxLength="4"
                                Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtProd_Version"
                                ValidationGroup="save" ErrorMessage="Prod Version cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Profile
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtProfile" runat="server" CssClass="textboxAutocomplete" MaxLength="7"
                                Width="180" onfocus="return ProfileOnFocus();" onchange="return ProfileChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtProfile"
                                ValidationGroup="save" ErrorMessage="Profile cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            TaskList_Desc
                        </td>
                        <td class="rigthTD" style="width: 30%" colspan="3">
                            <asp:TextBox ID="txtTaskList_Desc" runat="server" CssClass="textbox" MaxLength="20"
                                Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTaskList_Desc"
                                ValidationGroup="save" ErrorMessage="Task List Desc cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Recipe
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtRecipe" runat="server" CssClass="textbox" MaxLength="2" Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRecipe"
                                ValidationGroup="save" ErrorMessage="Recipe cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Status
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtStatus" runat="server" CssClass="textboxAutocomplete" MaxLength="10"
                                Width="180" onfocus="return StatusOnFocus();" onchange="return StatusChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtStatus"
                                ValidationGroup="save" ErrorMessage="Status cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Usage
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtUsage" runat="server" CssClass="textboxAutocomplete" MaxLength="3"
                                Width="180" onfocus="return UsageOnFocus();" onchange="return UsageChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtUsage"
                                ValidationGroup="save" ErrorMessage="Usage cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Base Quantity
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtBase_Quantity" runat="server" CssClass="textbox" MaxLength="13"
                                Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtBase_Quantity"
                                ValidationGroup="save" ErrorMessage="Base Quantity cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Charge Quantity
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtCharge_Quantity" runat="server" CssClass="textbox" MaxLength="5"
                                Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCharge_Quantity"
                                ValidationGroup="save" ErrorMessage="Charge Quantity cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Operation Quantity
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtOperation_Quantity" runat="server" CssClass="textbox" MaxLength="5"
                                Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtOperation_Quantity"
                                ValidationGroup="save" ErrorMessage="Operation Quantity cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            UOM
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtUOM" runat="server" CssClass="textboxAutocomplete" MaxLength="3"
                                Width="180" onfocus="return UOMOnFocus();" onchange="return UOMChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtUOM"
                                ValidationGroup="save" ErrorMessage="UOM cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Super Ordinate Operation
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtSuperOrdinate_Operation" runat="server" CssClass="textboxAutocomplete"
                                MaxLength="4" Width="180" onfocus="return SuperOrdinate_OperationOnFocus();"
                                onchange="return SuperOrdinate_OperationChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtSuperOrdinate_Operation"
                                ValidationGroup="save" ErrorMessage="Super Ordinate Operation cannot be blank."
                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Control Recipe Destination
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtControl_Recipe_Destination" runat="server" CssClass="textboxAutocomplete"
                                MaxLength="2" Width="180" onfocus="return Control_Recipe_DestinationOnFocus();"
                                onchange="return Control_Recipe_DestinationTextChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtControl_Recipe_Destination"
                                ValidationGroup="save" ErrorMessage="Control Recipe Destination Operation cannot be blank."
                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Resource
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtResource" runat="server" CssClass="textbox" MaxLength="8" Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtResource"
                                ValidationGroup="save" ErrorMessage="ResourceOperation cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Control Key
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtControl_Key" runat="server" CssClass="textboxAutocomplete" MaxLength="4"
                                Width="180" onfocus="return Control_KeyOnFocus();" onchange="return Control_KeyTextChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtControl_Key"
                                ValidationGroup="save" ErrorMessage="Control Key Operation cannot be blank."
                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Standard Text Key
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtStandardTextKey" runat="server" CssClass="textbox" MaxLength="7"
                                Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtStandardTextKey"
                                ValidationGroup="save" ErrorMessage="Standard Text Key cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Description
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox" MaxLength="40"
                                Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtDescription"
                                ValidationGroup="save" ErrorMessage="Description cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Relevancy To Costing
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:CheckBox ID="chkRelevancy_To_Costing" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            First Std Value
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtFirst_Std_Value" runat="server" CssClass="textbox" MaxLength="6"
                                Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtFirst_Std_Value"
                                ValidationGroup="save" ErrorMessage="First Std Value cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            First Std Value Unit
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtFirst_Std_Value_Unit" runat="server" CssClass="textboxAutocomplete"
                                MaxLength="3" Width="180" onfocus="return First_Std_Value_UnitOnFocus();" onchange="return First_Std_Value_UnitTextChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtFirst_Std_Value_Unit"
                                ValidationGroup="save" ErrorMessage="First Std Value Unit cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            First Activity Type
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtFirst_Activity_Type" runat="server" CssClass="textboxAutocomplete"
                                MaxLength="6" Width="180" onfocus="return First_Activity_TypeOnFocus();" onchange="return First_Activity_TypeTextChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtFirst_Activity_Type"
                                ValidationGroup="save" ErrorMessage="First Activity Type Unit cannot be blank."
                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Sec Std Value
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtSec_Std_Value" runat="server" CssClass="textbox" MaxLength="6"
                                Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtSec_Std_Value"
                                ValidationGroup="save" ErrorMessage="Sec Std Value Unit cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Sec Std Value Unit
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtSec_Std_Value_Unit" runat="server" CssClass="textboxAutocomplete"
                                MaxLength="3" Width="180" onfocus="return Sec_Std_Value_UnitOnFocus();" onchange="return Sec_Std_Value_UnitTextChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtSec_Std_Value_Unit"
                                ValidationGroup="save" ErrorMessage=" Sec Std Value Unit scannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Sec Activity Type
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtSec_Activity_Type" runat="server" CssClass="textboxAutocomplete"
                                MaxLength="6" Width="180" onfocus="return Sec_Activity_TypeOnFocus();" onchange="return Sec_Activity_TypeTextChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtSec_Activity_Type"
                                ValidationGroup="save" ErrorMessage="Sec Activity Type cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Third Std Value
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtThird_Std_Value" runat="server" CssClass="textbox" MaxLength="6"
                                Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtThird_Std_Value"
                                ValidationGroup="save" ErrorMessage="Third Std Value cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Third Std Value Unit
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtThird_Std_Value_Unit" runat="server" CssClass="textboxAutocomplete"
                                MaxLength="3" Width="180" onfocus="return Third_Std_Value_UnitOnFocus();" onchange="return Third_Std_Value_UnitTextChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtThird_Std_Value_Unit"
                                ValidationGroup="save" ErrorMessage="Third Std Value Unit cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Third Activity Type
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtThird_Activity_Type" runat="server" CssClass="textboxAutocomplete"
                                MaxLength="6" Width="180" onfocus="return Third_Activity_TypeOnFocus();" onchange="return Third_Activity_TypeTextChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtThird_Activity_Type"
                                ValidationGroup="save" ErrorMessage="Third Activity Type Unit cannot be blank."
                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Fourth Std Value
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtFourth_Std_Value" runat="server" CssClass="textbox" MaxLength="6"
                                Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtFourth_Std_Value"
                                ValidationGroup="save" ErrorMessage="Fourth Std Value cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Fourth Std Value Unit
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtFourth_Std_Value_Unit" runat="server" CssClass="textboxAutocomplete"
                                MaxLength="3" Width="180" onfocus="return Fourth_Std_Value_UnitOnFocus();" onchange="return Fourth_Std_Value_UnitTextChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtFourth_Std_Value_Unit"
                                ValidationGroup="save" ErrorMessage="Fourth Std Value Unit cannot be blank."
                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Fourth Activity Type
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtFourth_Activity_Type" runat="server" CssClass="textboxAutocomplete"
                                MaxLength="6" Width="180" onfocus="return Fourth_Activity_TypeOnFocus();" onchange="return Fourth_Activity_TypeTextChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtFourth_Activity_Type"
                                ValidationGroup="save" ErrorMessage="Fourth Activity Type cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Fifth Std Value
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtFifth_Std_Value" runat="server" CssClass="textbox" MaxLength="6"
                                Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtFifth_Std_Value"
                                ValidationGroup="save" ErrorMessage="Fifth Std Value cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Fifth Std Value Unit
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtFifth_Std_Value_Unit" runat="server" CssClass="textboxAutocomplete"
                                MaxLength="3" Width="180" onfocus="return Fifth_Std_Value_UnitOnFocus();" onchange="return Fifth_Std_Value_UnitTextChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txtFifth_Std_Value_Unit"
                                ValidationGroup="save" ErrorMessage="Fifth Std Value Unit cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Fifth Activity Type
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtFifth_Activity_Type" runat="server" CssClass="textboxAutocomplete"
                                MaxLength="6" Width="180" onfocus="return Fifth_Activity_TypeOnFocus();" onchange="return Fifth_Activity_TypeTextChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtFifth_Activity_Type"
                                ValidationGroup="save" ErrorMessage="Fifth Activity Type cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Sixth Std Value
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtSixth_Std_Value" runat="server" CssClass="textbox" MaxLength="6"
                                Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtSixth_Std_Value"
                                ValidationGroup="save" ErrorMessage="Sixth Std Value cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Sixth Std Value Unit
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtSixth_Std_Value_Unit" runat="server" CssClass="textboxAutocomplete"
                                MaxLength="3" Width="180" onfocus="return Sixth_Std_Value_UnitOnFocus();" onchange="return Sixth_Std_Value_UnitTextChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txtSixth_Std_Value_Unit"
                                ValidationGroup="save" ErrorMessage="Sixth Std Value Unit cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                        <td class="leftTD" width="20%">
                            Sixth Activity Type
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtSixth_Activity_Type" runat="server" CssClass="textboxAutocomplete"
                                MaxLength="6" Width="180" onfocus="return Sixth_Activity_TypeOnFocus();" onchange="return Sixth_Activity_TypeTextChangeEvent();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtSixth_Activity_Type"
                                ValidationGroup="save" ErrorMessage="Sixth Activity Type cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="20%">
                            Base Qty
                        </td>
                        <td class="rigthTD" style="width: 30%" colspan="3">
                            <asp:TextBox ID="txtBase_Qty" runat="server" CssClass="textbox" MaxLength="13" Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtBase_Qty"
                                ValidationGroup="save" ErrorMessage="Base Qty cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" style="width: 20%">
                            Valid From
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtValidFrom" runat="server" CssClass="textbox" MaxLength="10" Width="180" />
                            <act:CalendarExtender ID="txtValidFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtValidFrom">
                            </act:CalendarExtender>
                            <asp:RequiredFieldValidator ID="reqtxtTitle" runat="server" ControlToValidate="txtValidFrom"
                                ValidationGroup="save" ErrorMessage="Title cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Valid From cannot be blank.' />" />
                        </td>
                        <td class="leftTD" style="width: 20%">
                            Valid To
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:TextBox ID="txtValidTo" runat="server" CssClass="textbox" MaxLength="10" Width="180" />
                            <act:CalendarExtender ID="txtValidTo_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtValidTo">
                            </act:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtValidTo"
                                ValidationGroup="save" ErrorMessage="Title cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Valid To cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="4">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" OnClick="btnSubmit_Click"
                                Visible="false" ValidationGroup="save" />

                            <asp:Button id="btnBack" runat="server" Text="Back" CausesValidation="false" CssClass="button" OnClick="btnBack_Click" />

                            <asp:Button ID="btnRejectTo" runat="server" Text="Reject" CssClass="button" OnClientClick="return ShowRollbackPopup();" />
                            <asp:Panel ID="Panel1" runat="server" Visible="false">
                                <asp:Label ID="Label1" runat="server" />
                            </asp:Panel>
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
                <td class="leftTD" style="width: 25%">
                    Reject To
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
                <td colspan="2" class="tdSpace">
                </td>
            </tr>
            <tr>
                <td class="leftTD" valign="top">
                    Remark
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
                <td colspan="2" class="tdSpace">
                </td>
            </tr>
            <tr>
                <td class="leftTD">
                    &nbsp;
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
    <asp:Label ID="lblReceipeId" runat="server" Visible="false" />
    <asp:Label ID="lblCostingId" runat="server" Visible="false" />
    <asp:Label ID="lblIsUserApprover" runat="server" Visible="false" />
    <script type="text/javascript">
        var textboxId = "";
        var textboxRealId = "";

        function ProfileOnFocus() {
            textboxId = $('#<%= txtProfile.ClientID%>').attr('ID');
            textboxRealId = "txtProfile";
            AutoCompleteLookUpReceipe();
        }

        function ProfileTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtProfile.ClientID%>').attr('ID'), "txtProfile");
        }

        function StatusOnFocus() {
            textboxId = $('#<%= txtStatus.ClientID%>').attr('ID');
            textboxRealId = "txtStatus";
            AutoCompleteLookUpReceipe();
        }

        function StatusTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtStatus.ClientID%>').attr('ID'), "txtStatus");
        }

        function UsageOnFocus() {
            textboxId = $('#<%= txtUsage.ClientID%>').attr('ID');
            textboxRealId = "txtUsage";
            AutoCompleteLookUpReceipe();
        }

        function UsageTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtUsage.ClientID%>').attr('ID'), "txtUsage");
        }

        function UOMOnFocus() {
            textboxId = $('#<%= txtUOM.ClientID%>').attr('ID');
            textboxRealId = "txtUOM";
            AutoCompleteLookUpReceipe();
        }

        function UOMTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtUOM.ClientID%>').attr('ID'), "txtUOM");
        }

        function SuperOrdinate_OperationOnFocus() {
            textboxId = $('#<%= txtSuperOrdinate_Operation.ClientID%>').attr('ID');
            textboxRealId = "txtSuperOrdinate_Operation";
            AutoCompleteLookUpReceipe();
        }

        function SuperOrdinate_OperationTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtSuperOrdinate_Operation.ClientID%>').attr('ID'), "txtSuperOrdinate_Operation");
        }

        function Control_Recipe_DestinationOnFocus() {
            textboxId = $('#<%= txtControl_Recipe_Destination.ClientID%>').attr('ID');
            textboxRealId = "txtControl_Recipe_Destination";
            AutoCompleteLookUpReceipe();
        }

        function Control_Recipe_DestinationTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtControl_Recipe_Destination.ClientID%>').attr('ID'), "txtControl_Recipe_Destination");
        }


        function Control_KeyOnFocus() {
            textboxId = $('#<%= txtControl_Key.ClientID%>').attr('ID');
            textboxRealId = "txtControl_Key";
            AutoCompleteLookUpReceipe();
        }

        function Control_KeyTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtControl_Key.ClientID%>').attr('ID'), "txtControl_Key");
        }

        function First_Std_Value_UnitOnFocus(obj) {
            textboxId = $('#<%= txtFirst_Std_Value_Unit.ClientID%>').attr('ID');
            textboxRealId = "txtFirst_Std_Value_Unit";
            AutoCompleteLookUpReceipe();
        }

        function First_Std_Value_UnitTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtFirst_Std_Value_Unit.ClientID%>').attr('ID'), "txtFirst_Std_Value_Unit");
        }


        function First_Activity_TypeOnFocus() {
            textboxId = $('#<%= txtFirst_Activity_Type.ClientID%>').attr('ID');
            textboxRealId = "txtFirst_Activity_Type";
            AutoCompleteLookUpReceipe();
        }

        function First_Activity_TypeTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtFirst_Activity_Type.ClientID%>').attr('ID'), "txtFirst_Activity_Type");
        }

        function Sec_Std_Value_UnitOnFocus(obj) {
            textboxId = $('#<%= txtSec_Std_Value_Unit.ClientID%>').attr('ID');
            textboxRealId = "txtSec_Std_Value_Unit";
            AutoCompleteLookUpReceipe();
        }

        function Sec_Std_Value_UnitTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtSec_Std_Value_Unit.ClientID%>').attr('ID'), "txtSec_Std_Value_Unit");
        }


        function Sec_Activity_TypeOnFocus() {
            textboxId = $('#<%= txtSec_Activity_Type.ClientID%>').attr('ID');
            textboxRealId = "txtSec_Activity_Type";
            AutoCompleteLookUpReceipe();
        }

        function Sec_Activity_TypeTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtSec_Activity_Type.ClientID%>').attr('ID'), "txtSec_Activity_Type");
        }

        function Third_Std_Value_UnitOnFocus(obj) {
            textboxId = $('#<%= txtThird_Std_Value_Unit.ClientID%>').attr('ID');
            textboxRealId = "txtThird_Std_Value_Unit";
            AutoCompleteLookUpReceipe();
        }

        function Third_Std_Value_UnitTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtThird_Std_Value_Unit.ClientID%>').attr('ID'), "txtThird_Std_Value_Unit");
        }


        function Third_Activity_TypeOnFocus() {
            textboxId = $('#<%= txtThird_Activity_Type.ClientID%>').attr('ID');
            textboxRealId = "txtThird_Activity_Type";
            AutoCompleteLookUpReceipe();
        }

        function Third_Activity_TypeTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtThird_Activity_Type.ClientID%>').attr('ID'), "txtThird_Activity_Type");
        }

        function Fourth_Std_Value_UnitOnFocus(obj) {
            textboxId = $('#<%= txtFourth_Std_Value_Unit.ClientID%>').attr('ID');
            textboxRealId = "txtFourth_Std_Value_Unit";
            AutoCompleteLookUpReceipe();
        }

        function Fourth_Std_Value_UnitTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtFourth_Std_Value_Unit.ClientID%>').attr('ID'), "txtFourth_Std_Value_Unit");
        }

        function Fourth_Activity_TypeOnFocus() {
            textboxId = $('#<%= txtFourth_Activity_Type.ClientID%>').attr('ID');
            textboxRealId = "txtFourth_Activity_Type";
            AutoCompleteLookUpReceipe();
        }

        function Fourth_Activity_TypeTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtFourth_Activity_Type.ClientID%>').attr('ID'), "txtFourth_Activity_Type");
        }


        function Fifth_Std_Value_UnitOnFocus(obj) {
            textboxId = $('#<%= txtFifth_Std_Value_Unit.ClientID%>').attr('ID');
            textboxRealId = "txtFifth_Std_Value_Unit";
            AutoCompleteLookUpReceipe();
        }

        function Fifth_Std_Value_UnitTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtFifth_Std_Value_Unit.ClientID%>').attr('ID'), "txtFifth_Std_Value_Unit");
        }

        function Fifth_Activity_TypeOnFocus() {
            textboxId = $('#<%= txtFifth_Activity_Type.ClientID%>').attr('ID');
            textboxRealId = "txtFifth_Activity_Type";
            AutoCompleteLookUpReceipe();
        }

        function Fifth_Activity_TypeTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtFifth_Activity_Type.ClientID%>').attr('ID'), "txtFifth_Activity_Type");
        }


        function Sixth_Std_Value_UnitOnFocus(obj) {
            textboxId = $('#<%= txtSixth_Std_Value_Unit.ClientID%>').attr('ID');
            textboxRealId = "txtSixth_Std_Value_Unit";
            AutoCompleteLookUpReceipe();
        }

        function Sixth_Std_Value_UnitTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtSixth_Std_Value_Unit.ClientID%>').attr('ID'), "txtSixth_Std_Value_Unit");
        }

        function Sixth_Activity_TypeOnFocus() {
            textboxId = $('#<%= txtSixth_Activity_Type.ClientID%>').attr('ID');
            textboxRealId = "txtSixth_Activity_Type";
            AutoCompleteLookUpReceipe();
        }

        function Sixth_Activity_TypeTextChangeEvent() {
            CheckLookupReceipe($('#<%= txtSixth_Activity_Type.ClientID%>').attr('ID'), "txtSixth_Activity_Type");
        }

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
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                    $(this).show('clip');
                }
            });
            return false;
        }

    </script>
</asp:Content>
