<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/BOMRecipe/BOMRecipeMasterPage.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="Reciepe.aspx.cs"
    Inherits="Transaction_Receipe_Reciepe" %>

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

        function showModalPopupViaClient() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior1');
            modalPopupBehavior.show();
            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlAddNew" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Recipe Header
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
                                    <td class="leftTD">
                                        Recipe Group
                                        <asp:Label ID="labletxtRGroup" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                    </td>
                                    <td class="rightTD" style="width: 30%">
                                        <asp:TextBox ID="txtRGroup" runat="server" MaxLength="10" CssClass="textbox" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Material Number
                                        <asp:Label ID="labletxtMaterialNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtMaterialNmbr" runat="server" CssClass="textbox" onkeypress="return IsNumber();"
                                            Width="180" onkeydown="return (event.keyCode!=13);" OnTextChanged="txtMaterialNmbr_TextChanged"
                                            AutoPostBack="true" />
                                        <asp:ImageButton ID="imgHelpSearchMaterial" runat="server" ImageUrl="~/images/search_icon.png"
                                            Height="15px" Width="15px" Style="padding-top: 1px;" TabIndex="100" OnClick="imgHelpSearchMaterial_Click" />
                                        <asp:RequiredFieldValidator ID="reqtxtMaterialNmbr" runat="server" ControlToValidate="txtMaterialNmbr"
                                            ValidationGroup="save" ErrorMessage="Material Number cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Material Description
                                        <asp:Label ID="labletxtMaterialDescription" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtMaterialDescription" runat="server" CssClass="textbox" MaxLength="40"
                                            size="41" Width="281px" Enabled="false" />
                                        <asp:RequiredFieldValidator ID="reqtxtMaterialDescription" runat="server" ControlToValidate="txtMaterialDescription"
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
                                        Recipe Counter
                                    </td>
                                    <td class="rightTD" style="width: 30%">
                                        <asp:TextBox ID="txtRecipe" runat="server" MaxLength="2" Enabled="false" CssClass="textbox"
                                            Width="20px" />
                                    </td>
                                    <td class="leftTD">
                                        Recipe Description
                                    </td>
                                    <td class="rightTD">
                                        <asp:TextBox ID="txtReciepeDesc" runat="server" CssClass="textbox" MaxLength="40"
                                            size="41" Width="281px" />
                                        <asp:RequiredFieldValidator ID="reqtxtReciepeDesc" runat="server" ControlToValidate="txtReciepeDesc"
                                            ValidationGroup="save" ErrorMessage="Recipe description cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Recipe description cannot be blank.' />" />
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
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" TabIndex="1"
                                            Enabled="false">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                            ValidationGroup="save" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                    </td>
                                    <td colspan="2">
                                        <%--<asp:Button ID="btnProdVer" runat="server" UseSubmitBehavior="true" Text="Prod. Version"
                                            CssClass="button" OnClick="btnProdVer_Click" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="display: none">
                                        <asp:Label ID="lblRecipe_HeaderDetail_Id" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="leftTD" colspan="6" valign="middle">
                                        <asp:Label ID="labelAssgn" runat="server" Text="Assignment"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="leftTD" style="width: 20%" colspan="2">
                                        Status
                                        <asp:Label ID="lableddlRStatus" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%" colspan="4">
                                        <asp:DropDownList ID="ddlRStatus" runat="server" CssClass="dropdownlist" Enabled="false"
                                            Width="150px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlRStatus" runat="server" ControlToValidate="ddlRStatus"
                                            ValidationGroup="save" ErrorMessage="Status cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Status cannot be blank.' />" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkRStatus" runat="server" Text="For Planning/Costing only" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="leftTD" style="width: 20%" colspan="2">
                                        Usage
                                        <asp:Label ID="labelddlUsages" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%" colspan="4">
                                        <asp:DropDownList ID="ddlUsages" runat="server" AppendDataBoundItems="True" Width="150px"
                                            CssClass="dropdownlist" Enabled="false">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlUsages" runat="server" ControlToValidate="ddlUsages"
                                            ValidationGroup="save" ErrorMessage="Usage cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Usage cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="6">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="leftTD" colspan="6" valign="middle">
                                        <asp:Label ID="lblcqtyrange" runat="server" Text="Charge Quantity Range"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="leftTD">
                                        From
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFrom" runat="server" Width="90px" CssClass="textbox" onkeypress="return IsNumber();"
                                            Enabled="false"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regtxtFrom" ControlToValidate="txtFrom" runat="server"
                                            ErrorMessage="Charge Quantity Range from should have numeric value up to 3 decimal place only."
                                            Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                            Text="<img src='../../images/Error.png' title='Charge Quantity Range from should have numeric value up to 3 decimal place only.' />" />
                                        <asp:CompareValidator ID="cmptxtFrom" runat="server" ControlToValidate="txtFrom"
                                            ControlToCompare="txtTo" Operator="LessThanEqual" ValidationGroup="save" Type="Double"
                                            ErrorMessage="Charge quantity range From should be less than Charge quantity range To."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Charge quantity range From should be less than Charge quantity range To.' />" />
                                    </td>
                                    <td align="right" class="leftTD">
                                        To
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtTo" runat="server" Width="90px" CssClass="textbox" onkeypress="return IsNumber();"
                                            Enabled="false"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regtxtTo" ControlToValidate="txtTo" runat="server"
                                            ErrorMessage="Charge Quantity Range To should have numeric value up to 3 decimal place only."
                                            Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                            Text="<img src='../../images/Error.png' title='Charge Quantity Range To should have numeric value up to 3 decimal place only.' />" />
                                        <asp:CompareValidator ID="cmptxtTo" runat="server" ControlToValidate="txtTo" ControlToCompare="txtFrom"
                                            Operator="GreaterThanEqual" ValidationGroup="save" Type="Double" ErrorMessage="Charge quantity range To should be greater than Charge quantity range From."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Charge quantity range To should be greater than Charge quantity range From.' />" />
                                    </td>
                                    <td align="right" class="leftTD">
                                        Unit
                                        <asp:Label ID="labelRheaderUnit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" colspan="3">
                                        <asp:DropDownList ID="ddlRheaderUnit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRheaderUnit_SelectedIndexChanged"
                                            Enabled="false">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlRheaderUnit" runat="server" ControlToValidate="ddlRheaderUnit"
                                            ValidationGroup="save" ErrorMessage="Recipe Header Unit cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Recipe Header Unit cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="6">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="leftTD" colspan="6" valign="middle">
                                        <asp:Label ID="Lbldefault" runat="server" Text="Default Value For Operations"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="leftTD" colspan="2">
                                        Base Quantity
                                        <asp:Label ID="labeltxtBQty" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" colspan="4">
                                        <asp:TextBox ID="txtBQty" runat="server" Width="70px" CssClass="textbox" onkeypress="return IsNumber();"
                                            OnTextChanged="txtBQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqtxtBQty" runat="server" ControlToValidate="txtBQty"
                                            ValidationGroup="save" ErrorMessage="Recipe Header base Quantity cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Recipe Header base Quantity cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtBQty" ControlToValidate="txtBQty" runat="server"
                                            ErrorMessage="Base Quantity should have numeric value up to 3 decimal place only."
                                            Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                            Text="<img src='../../images/Error.png' title='Base Quantity should have numeric value up to 3 decimal place only.' />" />
                                        <asp:CompareValidator ID="cmptxtBQty" runat="server" ValueToCompare="0" ControlToValidate="txtBQty"
                                            ErrorMessage="Base Quantity should be greater than zero." Operator="GreaterThan"
                                            Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" Type="Double"
                                            Text="<img src='../../images/Error.png' title='Base Quantity should be greater than zero.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <%--Started to Add Remark and Reason textbox. Ticket number 8200064571--%>
                                    <td align="right" class="leftTD">
                                        Remark
                                    </td>
                                    <td class="rigthTD" colspan="3">
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="textarea" Enabled="true" TextMode="MultiLine" Columns="100" Rows="3"></asp:TextBox>
                                    </td>
                                    <td align="right" class="leftTD">
                                        Reason
                                    </td>
                                    <td class="rigthTD" colspan="3">
                                        <asp:TextBox ID="txtReason" runat="server" CssClass="textarea" Enabled="true" TextMode="MultiLine" Columns="100" Rows="3" ></asp:TextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="6">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td align="right" class="leftTD">
                                        Charge Quantity
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtchargeqty" runat="server" Width="70px" CssClass="textbox" MaxLength="5"></asp:TextBox>
                                        <asp:Label ID="txtEqualTo" runat="server">Equal To</asp:Label>
                                    </td>
                                    <td align="right" class="leftTD">
                                        Operation Quantity
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtOperationQty" runat="server" Width="70px" CssClass="textbox"
                                            MaxLength="5" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td align="right" class="leftTD">
                                        Planner Group
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtplannergp" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                                    </td>
                                    <td align="center" class="leftTD">
                                        Resource Network
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtResourcenw" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td align="right" class="leftTD">
                                        Network Plant
                                    </td>
                                    <td class="rigthTD" colspan="3">
                                        <asp:TextBox ID="txtNWPlant" runat="server" CssClass="textbox" Enabled="false" ></asp:TextBox>
                                    </td>
                                    
                                </tr>
                                <%--End of Add Remark and Reason textbox. Ticket number 8200064571--%>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr id="trButton" runat="server" visible="false">
                        <td class="centerTD" colspan="2">
                            <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="save" TabIndex="26"
                                CssClass="button" OnClick="btnPrevious_Click" />
                            <asp:Button ID="btnSave" runat="server" ValidationGroup="save" UseSubmitBehavior="true"
                                Text="Save" CssClass="button" TabIndex="27" OnClick="btnSave_Click" />
                            <asp:Button ID="btnNext" runat="server" ValidationGroup="save" Text="Save & Next"
                                TabIndex="28" UseSubmitBehavior="true" CssClass="button" OnClick="btnNext_Click"
                                Width="120px" />
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="sm" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="save" />
                <asp:ValidationSummary ID="insp" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="valInspChar" />
                <asp:ValidationSummary ID="secRes" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="valSecRes" />
                <asp:Label ID="lblUserId" runat="server" Visible="false" />
                <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
                <asp:Label ID="lblRecipeId" runat="server" Visible="false" />
                <asp:Label ID="lblMode" runat="server" Visible="false" />
                <asp:Label ID="lblModuleId" runat="server" Visible="false" />
                <asp:Label ID="lblActionType" runat="server" Style="display: none" />
                <asp:Label ID="lblSectionId" runat="server" Text="71" Visible="false" />
            </asp:Panel>
        </ContentTemplate>
        <%-- <Triggers>
        <asp:AsyncPostBackTrigger ControlID="GvOperation" />
     </Triggers>--%>
    </asp:UpdatePanel>
    <%--<asp:UpdatePanel ID="UpdProdVer" runat="server">
        <ContentTemplate>
            <asp:Button runat="server" ID="hdnTrgtCntrlForProdVer" Style="display: none" />
            <act:ModalPopupExtender ID="modProdVer" runat="server" TargetControlID="hdnTrgtCntrlForProdVer"
                BehaviorID="programmaticModalPopupBehavior" CancelControlID="btnProdCancel" PopupControlID="pnlProdVer"
                BackgroundCssClass="modalBackground" DropShadow="true" PopupDragHandleControlID="pnlProdTitle" />
            <asp:Panel ID="pnlProdVer" runat="server" Width="100%">
                <div style="background-color: White; padding: 2px 2px 2px 2px; overflow: scroll;
                    width: 1000px; height: 500px">
                    <asp:Panel ID="pnlProdTitle" runat="server" Style="cursor: move; background-color: Black;
                        border: solid 1px Gray; color: Black">
                        <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                            <span class="ui-dialog-title">Production Version</span>
                        </div>
                    </asp:Panel>
                    <table border="0" cellpadding="0" cellspacing="1" width="100%">
                        <tr style="display: none">
                            <td colspan="5">
                                <asp:Label ID="lblProdVersionID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Plant
                            </td>
                            <td class="rigthTD" colspan="4">
                                <asp:Label ID="lblPlant" runat="server">                                        
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Material
                                <asp:Label ID="labeltxtMaterialNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtMaterialNo" runat="server" CssClass="textbox" MaxLength="18"
                                    Enabled="false" Width="180" onkeypress="return IsNumber();" />
                                <asp:RequiredFieldValidator ID="reqtxtMaterialNo" runat="server" ControlToValidate="txtMaterialNo"
                                    ValidationGroup="prod" ErrorMessage="Material Number cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />
                            </td>
                            <td class="rigthTD" colspan="3">
                                <asp:Label ID="lblMatDesc" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Production Version
                                <asp:Label ID="labeltxtProdVersion" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtProdVersion" runat="server" CssClass="textbox" Width="50px" onkeypress="return IsNumber();" />
                                <asp:RequiredFieldValidator ID="reqtxtProdVersion" runat="server" ControlToValidate="txtProdVersion"
                                    ValidationGroup="prod" ErrorMessage="Production version cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Production version cannot be blank' />" />
                            </td>
                            <td class="rigthTD" colspan="3">
                                <asp:TextBox ID="txtProdVerDesc" runat="server" CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtProdVerDesc" runat="server" ControlToValidate="txtProdVerDesc"
                                    ValidationGroup="prod" ErrorMessage="Task List description cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Task List description cannot be blank' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="rigthTD" colspan="5">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="leftTD" colspan="5" valign="middle">
                                <asp:Label ID="Label1" runat="server" Text="Production Version"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Lock
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlLock" runat="server" AppendDataBoundItems="True" Width="150px"
                                    CssClass="dropdownlist">
                                </asp:DropDownList>
                            </td>
                            <td colspan="3" class="leftTD">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="leftTD">
                                From
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtProdFrom" runat="server" Width="70px" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td align="right" class="leftTD">
                                To
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtProdTo" runat="server" Width="70px" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtProdUnit" runat="server" Width="50px" CssClass="textbox" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="leftTD">
                                Valid From
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtProdVFrom" runat="server" Width="70px" CssClass="textbox" Enabled="false"></asp:TextBox>
                            </td>
                            <td align="right" class="leftTD">
                                Valid To
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtProdVTo" runat="server" Width="70px" CssClass="textbox" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="rigthTD">
                            </td>
                        </tr>
                        <tr>
                            <td class="rigthTD" colspan="5">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="leftTD" colspan="5" valign="middle">
                                <asp:Label ID="Label3" runat="server" Text="Plan Data"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                            </td>
                            <td valign="middle" align="center" class="leftTD">
                                Task List Type
                            </td>
                            <td valign="middle" align="center" class="leftTD">
                                Group
                            </td>
                            <td valign="middle" align="center" class="leftTD">
                                Group Counter
                            </td>
                            <td valign="middle" align="center" class="leftTD">
                                Check Start
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Detailed Planning
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlDPTaskList" runat="server" AppendDataBoundItems="True" Width="150px" Enabled = "false"
                                    CssClass="dropdownlist">
                                </asp:DropDownList>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtDPGroup" runat="server" CssClass="textbox" Width="50px" />
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtDPGroupCntr" runat="server" CssClass="textbox" Width="20px" />
                            </td>
                            <td class="rigthTD">
                            </td>
                        </tr>
                        <tr>
                            <td class="rigthTD" colspan="5">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="leftTD" colspan="5" valign="middle">
                                <asp:Label ID="Label4" runat="server" Text="BOM Data"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="leftTD">
                                Alternative BOM
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtProdAltBOM" runat="server" Width="70px" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td align="right" class="leftTD">
                                BOM Usage
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtProdBOMUsage" runat="server" Width="70px" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="rigthTD">
                            </td>
                        </tr>
                        <tr>
                            <td class="centerTD" colspan="6">
                                <asp:Button ID="btnProdSave" runat="server" Text="Save" CssClass="button"
                                    ValidationGroup="prod" />
                                <asp:Button ID="btnProdCancel" Text="Cancel" runat="server" CssClass="button" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:ValidationSummary ID="valProdVer" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="prod" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:UpdatePanel ID="UpdPnlSearchMatHELP" runat="server">
        <ContentTemplate>
            <asp:Button runat="server" ID="hdnTrgtCntrlForMatHELP" Style="display: none" />
            <act:ModalPopupExtender ID="modMatSearch" runat="server" TargetControlID="hdnTrgtCntrlForMatHELP"
                BehaviorID="programmaticModalPopupBehavior1" CancelControlID="btnCancelMaterialHelp"
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
                            <td colspan="4" style="display: none">
                                <asp:Label ID="lblType" runat="server"></asp:Label>
                                <asp:Label ID="lblRow" runat="server"></asp:Label>
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
                                <div style="max-height: 350px; overflow: auto; margin-bottom: 5px">
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
