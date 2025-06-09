<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Receipe/ReciepeMasterPage.master"
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
                            Reciepe Header & Operations
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
                                        <asp:TextBox ID="txtRecipe" runat="server" MaxLength="4" Enabled="false" CssClass="textbox" />
                                    </td>
                                    <td class="rightTD" colspan="2">
                                        <asp:TextBox ID="txtReciepeDesc" runat="server" CssClass="textbox" />
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
                                        <asp:Button ID="btnProdVer" runat="server" UseSubmitBehavior="true" Text="Prod. Version"
                                            CssClass="button" OnClick="btnProdVer_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="PnlTab" runat="server" Visible="true">
                                <act:TabContainer ID="tabcontainerRecipe" runat="server" ActiveTabIndex="0" ScrollBars="Both"
                                    Width="1100px" AutoPostBack="false">
                                    <act:TabPanel ID="tbpanelRecipeHeader" runat="server" HeaderText="Recipe Header"
                                        TabIndex="1">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlHeaderTab" runat="server">
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
                                                            <asp:DropDownList ID="ddlRStatus" runat="server" CssClass="dropdownlist" Enabled="false">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqddlRStatus" runat="server" ControlToValidate="ddlRStatus"
                                                                ValidationGroup="save" ErrorMessage="Status cannot be blank." SetFocusOnError="true"
                                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Status cannot be blank.' />" />
                                                            &nbsp;
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
                                                            <asp:TextBox ID="txtFrom" runat="server" Width="90px" CssClass="textbox" onkeypress="return IsNumber();"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="regtxtFrom" ControlToValidate="txtFrom" runat="server"
                                                                ErrorMessage="Charge Quantity Range from should have numeric value up to 3 decimal place only."
                                                                Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                                Text="<img src='../../images/Error.png' title='Charge Quantity Range from should have numeric value up to 3 decimal place only.' />" />
                                                        </td>
                                                        <td align="right" class="leftTD">
                                                            To
                                                        </td>
                                                        <td class="rigthTD">
                                                            <asp:TextBox ID="txtTo" runat="server" Width="90px" CssClass="textbox" onkeypress="return IsNumber();"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="regtxtTo" ControlToValidate="txtTo" runat="server"
                                                                ErrorMessage="Charge Quantity Range To should have numeric value up to 3 decimal place only."
                                                                Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                                Text="<img src='../../images/Error.png' title='Charge Quantity Range To should have numeric value up to 3 decimal place only.' />" />
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
                                                            <asp:TextBox ID="txtNWPlant" runat="server" CssClass="textbox" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </act:TabPanel>
                                    <act:TabPanel ID="tbpnlOperation" runat="server" HeaderText="Operations" TabIndex="2">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlOperationtab" runat="server">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="tdSpace">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="rigthTD">
                                                            <div style="height: auto">
                                                                <asp:GridView ID="GvOperation" runat="server" AutoGenerateColumns="False" DataKeyNames="Recipe_Operation_Id,Control_key,StdText_Key,Destinatn,Relevancy_To_Costing,Plant,Act_Operation_UoM,ChargeUnit,OperUnit,Resource"
                                                                    CssClass="GridClass" ShowHeaderWhenEmpty="True" OnRowDataBound="GvOperation_RowDataBound"
                                                                    EmptyDataText="No Data Found">
                                                                    <HeaderStyle BackColor="#EDF5FF" />
                                                                    <FooterStyle CssClass="gridFooter" />
                                                                    <RowStyle CssClass="grdViewRow" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Delete Flag" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" ID="chkDeletionFlag" />
                                                                                <asp:Label ID="lblDeletionFlag" runat="server" Visible="false" Text='<%#Eval("DeletionFlag") %>' />
                                                                                <asp:HiddenField ID="hdnDeletionFlag" runat="server" Value='<%#Eval("DeletionFlag") %>' />
                                                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="" CommandName="D" Visible="false"
                                                                                    CausesValidation="false">
                                                                                        <img src="../../images/delete.png" alt="Delete" title='Delete'/>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Choose Operation" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelect" runat="server" Enabled="false" />
                                                                                <asp:HiddenField ID="hdnSelect" runat="server" Value='<%#Eval("Select") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRecipe_Operation_Id" runat="server" Text='<%#Eval("Recipe_Operation_Id") %>'
                                                                                    Visible="false" />
                                                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'
                                                                                    Visible="false" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Oper Phase">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtOperation_Phase" runat="server" MaxLength="18" Enabled="false"
                                                                                    Width="30px" CssClass="textbox" Text='<%#Eval("Operation_Phase") %>' />
                                                                                <asp:RequiredFieldValidator ID="reqtxtOperation_Phase" runat="server" ControlToValidate="txtOperation_Phase"
                                                                                    ValidationGroup="save" ErrorMessage="Operation phase cannot be blank." SetFocusOnError="true"
                                                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Operation phase cannot be blank.' />" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Phase Indicator">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkPI" runat="server" CssClass="chkPI" OnCheckedChanged="chkPI_CheckedChanged"
                                                                                    AutoPostBack="true" />
                                                                                <asp:HiddenField ID="hdnPI" runat="server" Value='<%#Eval("Phase_Indicator") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sup Operation">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtSup_Operation" runat="server" Width="30px" CssClass="textbox"
                                                                                    Text='<%#Eval("Sup_Operation") %>' OnTextChanged="txtSup_Operation_TextChanged"
                                                                                    AutoPostBack="true" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Destination">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDestination" runat="server" Text='<%#Eval("Destinatn") %>' Enabled="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Resource">
                                                                            <ItemTemplate>
                                                                                <%--<asp:TextBox ID="txtResource" runat="server" CssClass="textbox" Width="50px" Text='<%#Eval("Resource") %>'/>--%>
                                                                                <asp:DropDownList ID="ddlResource" runat="server" CssClass="dropdownlist" OnSelectedIndexChanged="ddlResource_SelectedIndexChanged"
                                                                                    AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="reqddlResource" runat="server" ControlToValidate="ddlResource"
                                                                                    ValidationGroup="save" ErrorMessage="Resource cannot be blank." SetFocusOnError="true"
                                                                                    Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Resource cannot be blank.' />" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Control key">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlControlKey" CssClass="dropdownlist" runat="server" OnSelectedIndexChanged="ddlControlKey_SelectedIndexChanged"
                                                                                    AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="reqddlControlKey" runat="server" ControlToValidate="ddlControlKey" 
                                                                                    ValidationGroup="save" ErrorMessage="Control Key cannot be blank." SetFocusOnError="true"
                                                                                    Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Control Key cannot be blank.' />" />
                                                                                <%--<asp:Label ID="lblControlKey" runat="server" Text='<%#Eval("Control_key") %>' Visible="false" />--%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Standard text Key">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlStdTextKey" CssClass="dropdownlist" runat="server">
                                                                                </asp:DropDownList>
                                                                                <asp:Label ID="lblFieldKey" runat="server" Text='<%#Eval("StdText_Key") %>' Visible="false" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Operation Text">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox" MaxLength="40"
                                                                                    Width="270px" Text='<%#Eval("Description") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                            HeaderText="Relevancy to Costing Indicator" HeaderStyle-Width="100px" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlIndicatorRelavancyToCosting" runat="server" AppendDataBoundItems="false">
                                                                                    <asp:ListItem Text="Select" Value="0" />
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Base Qty">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtBase_Quantity" runat="server" CssClass="textbox" onkeypress="return IsNumber();"
                                                                                    Width="70px" Text='<%#Eval("Base_Quantity") %>' />
                                                                                <asp:RequiredFieldValidator ID="reqtxtBase_Quantity" runat="server" ControlToValidate="txtBase_Quantity"
                                                                                    Enabled="false" ValidationGroup="save" ErrorMessage="Operation Base Quantity cannot be blank."
                                                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Operation Base Quantity cannot be blank.' />" />
                                                                                <asp:RegularExpressionValidator ID="regtxtBase_Quantity" ControlToValidate="txtBase_Quantity"
                                                                                    runat="server" ErrorMessage="Operation Base Quantity should have numeric value up to 3 decimal place only."
                                                                                    Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                                                    Text="<img src='../../images/Error.png' title='Operation Base Quantity should have numeric value up to 3 decimal place only.' />" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Act/Operation UOM">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlAct_Operation_UoM" runat="server" CssClass="dropdownlist"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlAct_Operation_UoM_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="reqddlAct_Operation_UoM" runat="server" ControlToValidate="ddlAct_Operation_UoM"
                                                                                    ValidationGroup="save" ErrorMessage="Operation UOM cannot be blank." SetFocusOnError="true"
                                                                                    Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Operation UOM cannot be blank.' />" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="1st Std Value">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtFirst_Std_Value" runat="server" CssClass="textbox " onkeypress="return IsNumber();"
                                                                                    Width="55px" Text='<%#Eval("First_Std_Value") %>' Enabled="false" />
                                                                                <asp:RegularExpressionValidator ID="regtxtFirst_Std_Value" ControlToValidate="txtFirst_Std_Value"
                                                                                    runat="server" ErrorMessage="First Std Value should have numeric value up to 3 decimal place only."
                                                                                    Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                                                    Text="<img src='../../images/Error.png' title='First Std Value should have numeric value up to 3 decimal place only.' />" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Standard Value Unit">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtFirst_Std_Value_Unit" runat="server" CssClass="textbox" MaxLength="3"
                                                                                    Width="70px" Text='<%#Eval("First_Std_Value_Unit") %>' Enabled="false" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="2nd Std Value">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtSec_Std_Value" runat="server" CssClass="textbox" onkeypress="return IsNumber();"
                                                                                    Width="55px" Text='<%#Eval("Sec_Std_Value") %>' Enabled="false" />
                                                                                <asp:RegularExpressionValidator ID="regtxtSec_Std_Value" ControlToValidate="txtSec_Std_Value"
                                                                                    runat="server" ErrorMessage="Second Std Value should have numeric value up to 3 decimal place only."
                                                                                    Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                                                    Text="<img src='../../images/Error.png' title='Second Std Value should have numeric value up to 3 decimal place only.' />" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Standard Value Unit">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtSec_Std_Value_Unit" runat="server" CssClass="textbox" MaxLength="3"
                                                                                    Width="70px" Text='<%#Eval("Sec_Std_Value_Unit") %>' Enabled="false" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="3rd Std Value">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtThird_Std_Value" runat="server" CssClass="textbox" onkeypress="return IsNumber();"
                                                                                    Width="55px" Text='<%#Eval("Third_Std_Value") %>' Enabled="false" />
                                                                                <asp:RegularExpressionValidator ID="regtxtThird_Std_Value" ControlToValidate="txtThird_Std_Value"
                                                                                    runat="server" ErrorMessage="Third Std Value should have numeric value up to 3 decimal place only."
                                                                                    Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                                                    Text="<img src='../../images/Error.png' title='Third Std Value should have numeric value up to 3 decimal place only.' />" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Standard Value Unit">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtThird_Std_Value_Unit" runat="server" CssClass="textbox" MaxLength="3"
                                                                                    Width="70px" Text='<%#Eval("Third_Std_Value_Unit") %>' Enabled="false" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Plant" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false">
                                                                                    <asp:ListItem Text="Select" Value="" />
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Charge Quantity">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtChargeQty" runat="server" CssClass="textbox" Width="70px" Text='<%#Eval("ChargeQty") %>'
                                                                                    Enabled="false" />
                                                                                <asp:RequiredFieldValidator ID="reqtxtChargeQty" runat="server" ControlToValidate="txtChargeQty"
                                                                                    ValidationGroup="save" ErrorMessage="Operation Charge Quantity cannot be blank."
                                                                                    Enabled="false" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Operation Charge Quantity cannot be blank.' />" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Charge Unit">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlChngeCUnit" runat="server" Enabled="false">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Operation Quantity">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtOperQty" runat="server" CssClass="textbox" Width="70px" Text='<%#Eval("OperQty") %>'
                                                                                    Enabled="false" />
                                                                                <asp:RequiredFieldValidator ID="reqtxtOperQty" runat="server" ControlToValidate="txtOperQty"
                                                                                    ValidationGroup="save" ErrorMessage="Operation Quantity cannot be blank." Enabled="false"
                                                                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Operation Quantity cannot be blank.' />" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Operation Unit">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlChngeOUnit" runat="server" Enabled="false">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDeleteFlagUDI" Text='<%#Eval("DFlagValue") %>' />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="middle" align="left">
                                                            <asp:Button ID="btnInsertRecord" runat="server" Width="90px" CssClass="button" Text="New Row"
                                                                ValidationGroup="ValgrpCust" CommandName="Insert" OnClick="btnInsertRecord_Click"
                                                                Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </act:TabPanel>
                                    <act:TabPanel ID="tbpnlInspChara" runat="server" HeaderText="Inspection Characteristic"
                                        TabIndex="3">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlInspChara" Visible="False" runat="server">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="tdSpace" colspan="2">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" class="leftTD" colspan="2" valign="middle">
                                                            <asp:Label ID="lblQualityMgmt" runat="server" Text="Quality Management"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" class="leftTD" style="width: 20%">
                                                            Insp. Points
                                                            <asp:Label ID="labelddlInspPoints" runat="server" ForeColor="Red" Text="*" Visible = "false"></asp:Label>
                                                        </td>
                                                        <td class="rigthTD" style="width: 30%">
                                                            <asp:DropDownList ID="ddlInspPoints" runat="server" AppendDataBoundItems="True" Width="250px"
                                                                CssClass="dropdownlist">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqddlInspPoints" runat="server" ControlToValidate="ddlInspPoints"
                                                                ValidationGroup="save" ErrorMessage="Insp. Points cannot be blank." SetFocusOnError="true" Enabled = "false"
                                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Insp. Points cannot be blank.' />" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" class="leftTD" style="width: 20%">
                                                            Partial-lot assign.
                                                            <asp:Label ID="labelddlPartialLot" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                                        </td>
                                                        <td class="rigthTD" style="width: 30%">
                                                            <asp:DropDownList ID="ddlPartialLot" runat="server" AppendDataBoundItems="True" Width="250px"
                                                                CssClass="dropdownlist">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqddlPartialLot" runat="server" ControlToValidate="ddlPartialLot" Enabled="false"
                                                                ValidationGroup="save" ErrorMessage="Partial-lot assign. cannot be blank." SetFocusOnError="true"
                                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Partial-lot assign. cannot be blank.' />" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="rigthTD" colspan="2">
                                                            <div style="height: auto;">
                                                                <asp:GridView ID="gvInspChara" runat="server" AutoGenerateColumns="False" DataKeyNames="Recipe_InspChara_Id,Sampling_Procedure,MIC"
                                                                    CssClass="GridClass" ShowHeaderWhenEmpty="True" OnRowDataBound="gvInspChara_RowDataBound"
                                                                    OnRowCommand="gvInspChara_RowCommand">
                                                                    <HeaderStyle BackColor="#EDF5FF" />
                                                                    <FooterStyle CssClass="gridFooter" />
                                                                    <RowStyle CssClass="grdViewRow" />
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="D" CausesValidation="false">  
                                                                                <img src="../../images/delete.png" alt="Delete" title='Delete'/></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Operation Phase">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtOperationPhase" Text='<%#Eval("Operation_Phase") %>'
                                                                                    CssClass="textbox" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRecipe_InspChara_Id" runat="server" Text='<%#Eval("Recipe_InspChara_Id") %>'
                                                                                    Visible="false" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Chara No">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtCharacteristicNo" Width="30px" runat="server" Text='<%#Eval("Characteristic_No") %>'
                                                                                    CssClass="textbox" onkeypress="return IsNumber();"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="reqtxtCharacteristicNo" runat="server" ControlToValidate="txtCharacteristicNo"
                                                                                    ValidationGroup="save" ErrorMessage="Characteristic No. cannot be blank." SetFocusOnError="true"
                                                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Characteristic No. cannot be blank.' />" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Master Insp Chara Code">
                                                                            <ItemTemplate>
                                                                                <%--<asp:TextBox ID="txtMIC" MaxLength="8" runat="server" Text='<%#Eval("MIC") %>' CssClass="textbox"></asp:TextBox>--%>
                                                                                <asp:DropDownList ID="ddlMIC" CssClass="dropdownlist" runat="server">
                                                                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="reqddlMIC" runat="server" ControlToValidate="ddlMIC"
                                                                                    ValidationGroup="save" ErrorMessage="MIC cannot be blank." SetFocusOnError="true"
                                                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='MIC cannot be blank.' />" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sampling Procedure">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlSamplingProcedure" CssClass="dropdownlist" runat="server">
                                                                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="reqddlSamplingProcedure" runat="server" ControlToValidate="ddlSamplingProcedure"
                                                                                    ValidationGroup="save" ErrorMessage="Sampling procedure cannot be blank." SetFocusOnError="true"
                                                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Sampling procedure cannot be blank.' />" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code Group">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtCodeGrp" runat="server" Text='<%#Eval("CodeGrp") %>' CssClass="textbox"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="reqtxtCodeGrp" runat="server" ControlToValidate="txtCodeGrp" Enabled = "false"
                                                                                    ValidationGroup="save" ErrorMessage="Code Group cannot be blank." SetFocusOnError="true"
                                                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Code Group cannot be blank.' />" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr id="trInspPhase" runat="server" visible="false" colspan="2">
                                                        <td align="left" class="leftTD">
                                                            Operation Phase
                                                            <asp:DropDownList ID="ddlOperationPhase" runat="server" CssClass="dropdownlist">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtAddRowInsp" runat="server" Text="1" CssClass="textbox" Width="50px"
                                                                onkeypress="return IsNumber();"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="reqtxtAddRowInsp" runat="server" ControlToValidate="txtAddRowInsp"
                                                                ValidationGroup="valInspChar" ErrorMessage="Please enter the number of rows to be added."
                                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please enter the number of rows to be added.' />" />
                                                            <asp:Button ID="btnAddInspChara" runat="server" Text="New Row" CssClass="button"
                                                                ValidationGroup="valInspChar" OnClick="btnAddInspChara_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </act:TabPanel>
                                    <act:TabPanel ID="tbpnlSecRes" runat="server" HeaderText="Secondary Resources" TabIndex="4">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlSecRes" Visible="false" runat="server">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="tdSpace">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="rigthTD">
                                                            <div style="height: auto">
                                                                <asp:GridView ID="grdSecResources" runat="server" AutoGenerateColumns="False" CssClass="GridClass"
                                                                    DataKeyNames="Recipe_SecResource_Id,SecResource" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found"
                                                                    OnRowDataBound="grdSecResources_RowDataBound" OnRowCommand="grdSecResources_RowCommand">
                                                                    <HeaderStyle BackColor="#EDF5FF" />
                                                                    <FooterStyle CssClass="gridFooter" />
                                                                    <RowStyle CssClass="grdViewRow" />
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDeleteSec" runat="server" CommandName="D" CausesValidation="false">  
                                                                                <img src="../../images/delete.png" alt="Delete" title='Delete'/></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Operation Phase">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtOperationPhase" Text='<%#Eval("Operation_Phase") %>'
                                                                                    CssClass="textbox" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRecipe_SecResource_Id" runat="server" Text='<%#Eval("Recipe_SecResource_Id") %>'
                                                                                    Visible="false" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sec. Resource Item">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtSecRecItem" Text='<%#Eval("SecResource_Item") %>'
                                                                                    CssClass="textbox" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Secondary Resource">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ddlSecResource" runat="server" OnSelectedIndexChanged="ddlSecResource_SelectedIndexChanged"
                                                                                    AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="reqddlSecResource" runat="server" ControlToValidate="ddlSecResource"
                                                                                    ValidationGroup="save" ErrorMessage="Secondary resource cannot be blank." SetFocusOnError="true"
                                                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Secondary resource cannot be blank.' />" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Duration">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtDuration" Text='<%#Eval("Duration") %>' CssClass="textbox"
                                                                                    Width="100px" onkeypress="return IsNumber();" Enabled = "false"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="reqtxtDuration" runat="server" ControlToValidate="txtDuration"
                                                                                    ValidationGroup="save" ErrorMessage="Duration cannot be blank." SetFocusOnError="true"
                                                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Duration cannot be blank.' />" Enabled = "false"/>
                                                                                <asp:RegularExpressionValidator ID="regtxtDuration" ControlToValidate="txtDuration"
                                                                                    runat="server" ErrorMessage="Sec Resource Duration should have numeric value up to 3 decimal place only."
                                                                                    Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                                                    Text="<img src='../../images/Error.png' title='Sec Resource Duration should have numeric value up to 3 decimal place only.' />" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Unit">
                                                                            <ItemTemplate>
                                                                                <%--<asp:DropDownList ID = "ddlUnit" runat = "server" Enabled = "false" Visible = "false"></asp:DropDownList>--%>
                                                                                <asp:TextBox runat="server" ID="txtUnit1" Text='<%#Eval("Unit1") %>' CssClass="textbox"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Activity Type">
                                                                            <ItemTemplate>
                                                                                <%--<asp:DropDownList ID = "ddlActivityType" runat="server" Visible = "false"></asp:DropDownList>--%>
                                                                                <asp:TextBox runat="server" ID="txtActivityType1" Text='<%#Eval("ActivityType1") %>'
                                                                                    CssClass="textbox" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Process">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtProcess" Text='<%#Eval("Process") %>' CssClass="textbox"
                                                                                    Width="100px" onkeypress="return IsNumber();" Enabled = "false"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="reqtxtProcess" runat="server" ControlToValidate="txtProcess"
                                                                                    ValidationGroup="save" ErrorMessage="Process cannot be blank." SetFocusOnError="true" Enabled = "false"
                                                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Process cannot be blank.' />" />
                                                                                <asp:RegularExpressionValidator ID="regtxtProcess" ControlToValidate="txtProcess"
                                                                                    runat="server" ErrorMessage="Sec Resource Process should have numeric value up to 3 decimal place only."
                                                                                    Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                                                    Text="<img src='../../images/Error.png' title='Sec Resource Process should have numeric value up to 3 decimal place only.' />" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Unit">
                                                                            <ItemTemplate>
                                                                                <%--<asp:DropDownList ID = "ddlUnit" runat = "server" Enabled = "false" Visible = "false"></asp:DropDownList>--%>
                                                                                <asp:TextBox runat="server" ID="txtUnit2" Text='<%#Eval("Unit2") %>' CssClass="textbox"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Activity Type">
                                                                            <ItemTemplate>
                                                                                <%--<asp:DropDownList ID = "ddlActivityType" runat="server" Visible = "false"></asp:DropDownList>--%>
                                                                                <asp:TextBox runat="server" ID="txtActivityType2" Text='<%#Eval("ActivityType2") %>'
                                                                                    CssClass="textbox" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Labor">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtLabor" Text='<%#Eval("Labor") %>' CssClass="textbox"
                                                                                    Width="100px" onkeypress="return IsNumber();" Enabled = "false"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="reqtxtLabor" runat="server" ControlToValidate="txtLabor"
                                                                                    ValidationGroup="save" ErrorMessage="Labor cannot be blank." SetFocusOnError="true" Enabled = "false"
                                                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Labor cannot be blank.' />" />
                                                                                <asp:RegularExpressionValidator ID="regtxtLabor" ControlToValidate="txtLabor" runat="server"
                                                                                    ErrorMessage="Sec Resource Labor should have numeric value up to 3 decimal place only."
                                                                                    Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                                                    Text="<img src='../../images/Error.png' title='Sec Resource Labor should have numeric value up to 3 decimal place only.' />" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Unit">
                                                                            <ItemTemplate>
                                                                                <%--<asp:DropDownList ID = "ddlUnit" runat = "server" Enabled = "false" Visible = "false"></asp:DropDownList>--%>
                                                                                <asp:TextBox runat="server" ID="txtUnit3" Text='<%#Eval("Unit3") %>' CssClass="textbox"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Activity Type">
                                                                            <ItemTemplate>
                                                                                <%--<asp:DropDownList ID = "ddlActivityType" runat="server" Visible = "false"></asp:DropDownList>--%>
                                                                                <asp:TextBox runat="server" ID="txtActivityType3" Text='<%#Eval("ActivityType3") %>'
                                                                                    CssClass="textbox" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr id="trSecRes" runat="server" visible="false" colspan="2">
                                                        <td align="left" class="leftTD">
                                                            Operation Phase
                                                            <asp:DropDownList ID="ddlSecRes" runat="server" CssClass="dropdownlist">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtNoSecRes" runat="server" Text="1" CssClass="textbox" Width="50px"
                                                                onkeypress="return IsNumber();"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="reqtxtNoSecRes" runat="server" ControlToValidate="txtNoSecRes"
                                                                ValidationGroup="valSecRes" ErrorMessage="Please enter the number of rows to be added."
                                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please enter the number of rows to be added.' />" />
                                                            <asp:Button ID="btnAddSecRes" runat="server" Text="New Row" CssClass="button" OnClick="btnAddSecRes_Click"
                                                                ValidationGroup="valSecRes" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </act:TabPanel>
                                </act:TabContainer>
                            </asp:Panel>
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
                <asp:Label ID="lblSectionId" runat="server" Text="35" Visible="false" />
            </asp:Panel>
        </ContentTemplate>
        <%-- <Triggers>
        <asp:AsyncPostBackTrigger ControlID="GvOperation" />
     </Triggers>--%>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdProdVer" runat="server">
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
                                <%--<asp:Label ID="labeltxtProdVersion" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtProdVersion" runat="server" CssClass="textbox" Width="50px" onkeypress="return IsNumber();" ReadOnly="true" />
                                <%--<asp:RequiredFieldValidator ID="reqtxtProdVersion" runat="server" ControlToValidate="txtProdVersion"
                                    ValidationGroup="prod" ErrorMessage="Production version cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Production version cannot be blank' />" />--%>
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
                                <asp:Button ID="btnProdSave" runat="server" Text="Save" CssClass="button" OnClick="btnProdSave_Click"
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
    </asp:UpdatePanel>
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
