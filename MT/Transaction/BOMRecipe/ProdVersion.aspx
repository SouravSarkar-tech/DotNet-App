<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/BOMRecipe/BOMRecipeMasterPage.master"
    AutoEventWireup="true" CodeFile="ProdVersion.aspx.cs" Inherits="Transaction_BOMRecipe_ProdVersion" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function IsNumberNoDecimal() {
            if (!(event.keyCode == 13) && !(event.keyCode == 32) && !(event.keyCode == 35) && !(event.keyCode >= 48 && event.keyCode <= 57))
                return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdProdVer" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlProdVer" runat="server" Width="100%">
                <table border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="5">Production Version
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td colspan="5">
                            <asp:Label ID="lblProdVersionID" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD">Plant
                        </td>
                        <td class="rigthTD" colspan="4">
                            <asp:Label ID="lblPlant" runat="server">                                        
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD">Material
                            <asp:Label ID="labeltxtMaterialNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtMaterialNo" runat="server" CssClass="textbox" MaxLength="18"
                                Enabled="false" Width="180" onkeypress="return IsNumberNoDecimal();" />
                            <asp:RequiredFieldValidator ID="reqtxtMaterialNo" runat="server" ControlToValidate="txtMaterialNo"
                                ValidationGroup="prod" ErrorMessage="Material Number cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />
                        </td>
                        <td class="rigthTD" colspan="3">
                            <asp:Label ID="lblMatDesc" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD">Production Version
                            <%--<asp:Label ID="labeltxtProdVersion" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtProdVersion" runat="server" CssClass="textbox" Width="50px" MaxLength="4" Enabled="false" />
                            <%-- <asp:TextBox ID="txtProdVersion" runat="server" CssClass="textbox" Width="50px" MaxLength="4" OnTextChanged="txtProdVersion_TextChanged" AutoPostBack="true" onkeypress="return IsNumberNoDecimal();"
                                Enabled ="false" />--%>
                            <%--<asp:RequiredFieldValidator ID="reqtxtProdVersion" runat="server" ControlToValidate="txtProdVersion"
                                ValidationGroup="prod" ErrorMessage="Production version cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Production version cannot be blank' />" />--%>
                        </td>
                        <td class="leftTD">Prod. Version Description
                        </td>
                        <td class="rigthTD" colspan="2">
                            <asp:TextBox ID="txtProdVerDesc" runat="server" CssClass="textbox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtxtProdVerDesc" runat="server" ControlToValidate="txtProdVerDesc"
                                ValidationGroup="prod" ErrorMessage="Task List description cannot be blank."
                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Task List description cannot be blank' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rigthTD" colspan="5"></td>
                    </tr>
                    <tr>
                        <td align="right" class="leftTD" colspan="5" valign="middle">
                            <asp:Label ID="Label1" runat="server" Text="Production Version"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD">Lock
                             <%--<asp:Label ID="lblddlLock" runat="server"  Text="Lock" Visible="false" ></asp:Label>--%>
                                
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlLock" runat="server" AppendDataBoundItems="True" Width="150px"
                                CssClass="dropdownlist" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <%-- <td colspan="3" class="leftTD">
                        </td>--%>
                        <td align="right" class="leftTD" style="width: 20%">
                             <asp:Label ID="lblddlRStatus" runat="server"  Text="Status" Visible="false" ></asp:Label>
                                        <%--<asp:Label ID="lableddlRStatus" runat="server" ForeColor="Red" Text="*" Visible="false" ></asp:Label>--%>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlRStatus" runat="server" CssClass="dropdownlist" 
                                Width="150px" Visible="false">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlRStatus" runat="server" ControlToValidate="ddlRStatus" Visible="false"
                                ValidationGroup="prod" ErrorMessage="Status cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Status cannot be blank.' />" />

                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="leftTD">From
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtProdFrom" runat="server" Width="70px" CssClass="textbox" onkeypress="return IsNumber();" Enabled="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regtxtProdFrom" ControlToValidate="txtProdFrom"
                                runat="server" ErrorMessage="From should have numeric value up to 3 decimal place only."
                                Display="Dynamic" ValidationGroup="prod" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                Text="<img src='../../images/Error.png' title='From should have numeric value up to 3 decimal place only.' />" />
                            <asp:CompareValidator ID="cmptxtProdFrom" runat="server" ControlToValidate="txtProdFrom"
                                ControlToCompare="txtProdTo" Operator="LessThanEqual" ValidationGroup="prod" Type="Double"
                                ErrorMessage="Charge quantity range From should be less than Charge quantity range To."
                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Charge quantity range From should be less than Charge quantity range To.' />" />
                        </td>
                        <td align="right" class="leftTD">To
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtProdTo" runat="server" Width="70px" CssClass="textbox" onkeypress="return IsNumber();" Enabled="false"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regtxtProdTo" ControlToValidate="txtProdTo" runat="server"
                                ErrorMessage="From should have numeric value up to 3 decimal place only." Display="Dynamic"
                                ValidationGroup="prod" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                Text="<img src='../../images/Error.png' title='From should have numeric value up to 3 decimal place only.' />" />
                            <asp:CompareValidator ID="cmptxtProdTo" runat="server" ControlToValidate="txtProdTo"
                                ControlToCompare="txtProdFrom" Operator="GreaterThanEqual" ValidationGroup="prod"
                                Type="Double" ErrorMessage="Charge quantity range To should be greater than Charge quantity range From."
                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Charge quantity range To should be greater than Charge quantity range From.' />" />
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtProdUnit" runat="server" Width="50px" CssClass="textbox" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="leftTD">Valid From
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtProdVFrom" runat="server" Width="70px" CssClass="textbox" Enabled="false"></asp:TextBox>
                            <act:CalendarExtender ID="caltxtValidFrom" runat="server" TargetControlID="txtProdVFrom"
                                PopupPosition="BottomLeft" Format="dd/MM/yyyy" Animated="true">
                            </act:CalendarExtender>
                            <asp:RegularExpressionValidator ID="regtxtProdVFrom" runat="server" ControlToValidate="txtProdVFrom"
                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                ValidationGroup="prod" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1}))|([0-9]{2}))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                        </td>
                        <td align="right" class="leftTD">Valid To
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtProdVTo" runat="server" Width="70px" CssClass="textbox" Enabled="false"></asp:TextBox>
                            <act:CalendarExtender ID="calExder1" runat="server" TargetControlID="txtProdVTo"
                                PopupPosition="BottomLeft" Format="dd/MM/yyyy" Animated="true">
                            </act:CalendarExtender>
                            <asp:RegularExpressionValidator ID="regtxtProdVTo" runat="server" ControlToValidate="txtProdVTo"
                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                ValidationGroup="prod" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1}))|([0-9]{2}))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                        </td>
                        <td class="rigthTD"></td>
                    </tr>
                    <tr>
                        <td class="rigthTD" colspan="5"></td>
                    </tr>
                    <tr>
                        <td align="right" class="leftTD" colspan="5" valign="middle">
                            <asp:Label ID="Label3" runat="server" Text="Plan Data"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD"></td>
                        <td valign="middle" align="center" class="leftTD">Task List Type
                        </td>
                        <td valign="middle" align="center" class="leftTD">Group
                        </td>
                        <td valign="middle" align="center" class="leftTD">Group Counter
                        </td>
                        <td valign="middle" align="center" class="leftTD">Check Start
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD">Detailed Planning
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlDPTaskList" runat="server" AppendDataBoundItems="True" Width="150px"
                                Enabled="false" CssClass="dropdownlist">
                            </asp:DropDownList>
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtDPGroup" runat="server" CssClass="textbox" Width="161px" MaxLength="8"
                                onkeypress="return IsNumberNoDecimal();" OnTextChanged="txtDPGroup_TextChanged"
                                AutoPostBack="true" />
                            <asp:RequiredFieldValidator ID="reqtxtDPGroup" runat="server" ControlToValidate="txtDPGroup"
                                ValidationGroup="prod" ErrorMessage="Recipe Group cannot be blank." Enabled="false"
                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Recipe Group cannot be blank' />" />
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtDPGroupCntr" runat="server" CssClass="textbox" Width="26px" onkeypress="return IsNumberNoDecimal();" />
                            <asp:RequiredFieldValidator ID="reqtxtDPGroupCntr" runat="server" ControlToValidate="txtDPGroupCntr"
                                ValidationGroup="prod" ErrorMessage="Recipe Group counter cannot be blank." Enabled="false"
                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Recipe Group counter cannot be blank' />" />
                        </td>
                        <td class="rigthTD"></td>
                    </tr>
                    <tr>
                        <td class="rigthTD" colspan="5"></td>
                    </tr>
                    <tr>
                        <td align="right" class="leftTD" colspan="5" valign="middle">
                            <asp:Label ID="Label4" runat="server" Text="BOM Data"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="leftTD">Alternative BOM
                        </td>
                        <td class="rigthTD"> 
                            <asp:TextBox ID="txtProdAltBOM" runat="server" Width="70px" CssClass="textbox" onkeypress="return IsNumberNoDecimal();"
                                ></asp:TextBox>
                            <%--Enabled="false"--%>
                            <%--<asp:RequiredFieldValidator ID="reqtxtProdAltBOM" runat="server" ControlToValidate="txtProdAltBOM"
                                ValidationGroup="prod" ErrorMessage="Alternative BOM cannot be blank." Enabled="false"
                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alternative BOM cannot be blank.' />" />--%>
                        </td>
                        <td align="right" class="leftTD">BOM Usage
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlProdBOMUsage" runat="server" CssClass="dropdownlist" Enabled="false">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlProdBOMUsage" runat="server" ControlToValidate="ddlProdBOMUsage"
                                ValidationGroup="prod" ErrorMessage="BOM Usage cannot be blank." Enabled="false"
                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='BOM Usage cannot be blank.' />" />
                        </td>
                        <td class="rigthTD"></td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="6" id="trButton" runat="server" visible="false">
                            <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="prod" TabIndex="26"
                                CssClass="button" OnClick="btnPrevious_Click" />
                            <asp:Button ID="btnProdSave" runat="server" Text="Save" CssClass="button" OnClick="btnProdSave_Click"
                                ValidationGroup="prod" />
                            <asp:Button ID="btnNext" runat="server" ValidationGroup="prod" Text="Save & Next"
                                TabIndex="28" UseSubmitBehavior="true" CssClass="button" OnClick="btnNext_Click"
                                Width="120px" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblUserId" runat="server" Visible="false" />
                <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
                <asp:Label ID="lblRecipeId" runat="server" Visible="false" />
                <asp:Label ID="lblMode" runat="server" Visible="false" />
                <asp:Label ID="lblModuleId" runat="server" Visible="false" />
                <asp:Label ID="lblActionType" runat="server" Style="display: none" />
                <asp:Label ID="lblSectionId" runat="server" Text="72" Visible="false" />
                <asp:ValidationSummary ID="valProdVer" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="prod" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
