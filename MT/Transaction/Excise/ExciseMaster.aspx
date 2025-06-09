<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Receipe/ReceipeMasterPage.master"
    AutoEventWireup="true" CodeFile="ExciseMaster.aspx.cs" Inherits="Transaction_Excise_ExciseMaster" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:UpdatePanel ID="upSearch" runat="server" >
        <ContentTemplate>
            <asp:Panel ID="pnlSearch" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="4">
                            Excise Master
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left" style="width: 25%">
                            Request No
                        </td>
                        <td class="rigthTD" align="left" style="width: 25%">
                            <asp:TextBox ID="txtRequestNo" runat="server" CssClass="textbox" />
                        </td>
                        <td class="leftTD" align="left">
                            Status
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Pending For My Approval" Value="P" />
                                <asp:ListItem Text="Rollbacked To Me" Value="R" />
                                <asp:ListItem Text="Created By Me" Value="C" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD">
                            Excise Type
                        </td>
                        <td class="rigthTD" colspan="3">
                            <asp:DropDownList ID="ddlExciseTypeSearch" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="All" Value="0" />
                                <asp:ListItem Text="Chapter Id" Value="21" />
                                <asp:ListItem Text="Material and Chapter-Id Combination" Value="22" />
                                <asp:ListItem Text="CENVAT  Determination" Value="23" />
                                <asp:ListItem Text="Vendor Excise Details" Value="24" />
                                <asp:ListItem Text="Customer Excise Details" Value="25" />
                                <asp:ListItem Text="Excise Tax Rate" Value="26" />
                                <asp:ListItem Text="Exception Material Excise Rate" Value="27" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="4">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                            <script type="text/javascript">
                                function CheckOtherIsCheckedByGVID(spanChk) {

                                    var CurrentRdbID = spanChk.id;
                                    var Chk = spanChk;
                                    Parent = document.getElementById("<%=grdSearch.ClientID%>");
                                    var items = Parent.getElementsByTagName('input');
                                    for (i = 0; i < items.length; i++) {
                                        if (items[i].id != CurrentRdbID && items[i].type == "radio") {
                                            if (items[i].checked) {
                                                items[i].checked = false;
                                            }
                                        }
                                    }
                                }
                            </script>
                            <asp:GridView ID="grdSearch" runat="server" AutoGenerateColumns="false" Width="100%"
                                BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                                GridLines="Both">
                                <RowStyle CssClass="light-gray" />
                                <HeaderStyle CssClass="gridHeader" />
                                <AlternatingRowStyle CssClass="gridRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdoSelection" runat="server" GroupName="selection" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrimaryID" runat="server" Text='<%# Eval("Master_Header_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Request No." DataField="Request_No" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Created On" DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Pending For" DataField="Pending_For" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" Visible="false" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" Visible="false">
                                        <ItemTemplate>
                                            <a href="#" onclick="return ShowRejectionNote('<%#Eval("Remarks") %>')" style="color: #1540c2">
                                                Rejection Note</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" HeaderText="Status">
                                        <ItemTemplate>
                                            <%#Eval("Status") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div id="divRejectionNoteContainer" style="display: none;" title="Rejection Note">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="background-color: White;">
                                    <tr>
                                        <td align="left">
                                            <div id="divRejectionNote">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <br />
                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="button" OnClientClick="return Validate();"
                                OnClick="btnView_Click" Visible="false" />
                            <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="button" OnClientClick="return Validate();"
                                OnClick="btnModify_Click" Visible="false" />
                            <input type="button" name="btnCreateNew" id="btnCreateNew" class="button" value="Create New"
                                style="width: 120px" onclick="return ShowCreateNewDialog();" />
                            <script type="text/javascript">
                                function Validate() {
                                    var gv = document.getElementById("<%=grdSearch.ClientID%>");
                                    var rbs = gv.getElementsByTagName("input");
                                    var flag = 0;
                                    for (var i = 0; i < rbs.length; i++) {

                                        if (rbs[i].type == "radio") {
                                            if (rbs[i].checked) {
                                                flag = 1;
                                                break;
                                            }
                                        }
                                    }
                                    if (flag == 0) {
                                        alert("Kindly Select A Record");
                                        return false;
                                    }
                                }

                            </script>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlNew" runat="server" Visible="false">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">
                    Excise Master -
                    <asp:Label ID="lblExciseType" runat="server" Text="Chapter ID" />
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <asp:Panel ID="pnlChapterId" runat="server" Visible="false">
                        <table border="0" cellpadding="0" cellspacing="1" width="100%">
                            <tr>
                                <td class="tdSpace" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" width="20%">
                                    Chapter ID
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtChapterId" runat="server" CssClass="textbox" MaxLength="12" Width="110" />
                                    <asp:RequiredFieldValidator ID="reqtxtChapterId" runat="server" ControlToValidate="txtChapterId"
                                        ValidationGroup="save" ErrorMessage="Chapter Id cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD" width="20%">
                                    Unit of Measure for Excise
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtUOMExcise" runat="server" CssClass="textboxAutocomplete" MaxLength="3"
                                        Width="30" onfocus="return UOMExciseOnFocus();" onchange="return UOMExciseTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtUOMExcise" runat="server" ControlToValidate="txtUOMExcise"
                                        ValidationGroup="save" ErrorMessage="Unit of Measure for Excise Id cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Description as per Law
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtDescAsPerLaw1" runat="server" CssClass="textbox" MaxLength="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtDescAsPerLaw1" runat="server" ControlToValidate="txtDescAsPerLaw1"
                                        ValidationGroup="save" ErrorMessage="Description as per Law cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Description as per Law
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtDescAsPerLaw2" runat="server" CssClass="textbox" MaxLength="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtDescAsPerLaw2" runat="server" ControlToValidate="txtDescAsPerLaw2"
                                        ValidationGroup="save" ErrorMessage="Description as per Law cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Description as per Law
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtDescAsPerLaw3" runat="server" CssClass="textbox" MaxLength="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtDescAsPerLaw3" runat="server" ControlToValidate="txtDescAsPerLaw3"
                                        ValidationGroup="save" ErrorMessage="Description as per Law cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Description as per Law
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtDescAsPerLaw4" runat="server" CssClass="textbox" MaxLength="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtDescAsPerLaw4" runat="server" ControlToValidate="txtDescAsPerLaw4"
                                        ValidationGroup="save" ErrorMessage="Description as per Law cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Description as per Law
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtDescAsPerLaw5" runat="server" CssClass="textbox" MaxLength="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtDescAsPerLaw5" runat="server" ControlToValidate="txtDescAsPerLaw5"
                                        ValidationGroup="save" ErrorMessage="Description as per Law cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Description as per Law
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtDescAsPerLaw6" runat="server" CssClass="textbox" MaxLength="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtDescAsPerLaw6" runat="server" ControlToValidate="txtDescAsPerLaw6"
                                        ValidationGroup="save" ErrorMessage="Description as per Law cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Description as per Law
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtDescAsPerLaw7" runat="server" CssClass="textbox" MaxLength="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtDescAsPerLaw7" runat="server" ControlToValidate="txtDescAsPerLaw7"
                                        ValidationGroup="save" ErrorMessage="Description as per Law cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Description as per Law
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtDescAsPerLaw8" runat="server" CssClass="textbox" MaxLength="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtDescAsPerLaw8" runat="server" ControlToValidate="txtDescAsPerLaw8"
                                        ValidationGroup="save" ErrorMessage="Description as per Law cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlmcCombination" runat="server" Visible="false">
                        <table border="0" cellpadding="0" cellspacing="1" width="100%">
                            <tr>
                                <td class="tdSpace" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" width="20%">
                                    Material Number
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtMaterialNumber" runat="server" CssClass="textboxAutocomplete"
                                        MaxLength="18" Width="180" onfocus="return MaterialNumberOnFocus();" onchange="return MaterialNumberTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtMaterialNumber" runat="server" ControlToValidate="txtMaterialNumber"
                                        ValidationGroup="save" ErrorMessage="Material Number cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD" width="20%">
                                    Plant
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtPlant" runat="server" CssClass="textboxAutocomplete" MaxLength="4"
                                        Width="40" onfocus="return PlantOnFocus();" onchange="return PlantTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtPlant" runat="server" ControlToValidate="txtPlant"
                                        ValidationGroup="save" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Chapter ID
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtChapterId_mcCombination" runat="server" CssClass="textboxAutocomplete"
                                        MaxLength="12" Width="120"  onfocus="return ChapterId_mcCombinationOnFocus();" onchange="return ChapterId_mcCombinationTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtChapterId_mcCombination" runat="server" ControlToValidate="txtChapterId_mcCombination"
                                        ValidationGroup="save" ErrorMessage="Chapter Id cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Material Can Be Sent to Subcontractors
                                </td>
                                <td class="rigthTD">
                                    <asp:CheckBox ID="chkSubcontractors" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Material Type
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtMaterialType" runat="server" CssClass="textboxAutocomplete" MaxLength="1"
                                        Width="10"  onfocus="return MaterialTypeOnFocus();" onchange="return MaterialTypeTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtMaterialType" runat="server" ControlToValidate="txtMaterialType"
                                        ValidationGroup="save" ErrorMessage="Material Type cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Number of Goods Receipts per Excise Invoice
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtNoOfGoodsReceipts" runat="server" CssClass="textbox" MaxLength="1"
                                        Width="10" />
                                    <asp:RequiredFieldValidator ID="reqtxtNoOfGoodsReceipts" runat="server" ControlToValidate="txtNoOfGoodsReceipts"
                                        ValidationGroup="save" ErrorMessage="Number of Goods Receipts per Excise Invoice cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Indicator whether the material is declared to Excise
                                </td>
                                <td class="rigthTD">
                                    <asp:CheckBox ID="chkIndicatorMaterialDeclared" runat="server" />
                                </td>
                                <td class="leftTD">
                                    Declaration date of the material to Excise
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtDeclarationDate" runat="server" CssClass="textbox" MaxLength="8"
                                        Width="80" />
                                    <asp:RequiredFieldValidator ID="reqtxtDeclarationDate" runat="server" ControlToValidate="txtDeclarationDate"
                                        ValidationGroup="save" ErrorMessage="Declaration date of the material to Excise cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlCENVATDetermination" runat="server" Visible="false">
                        <table border="0" cellpadding="0" cellspacing="1" width="100%">
                            <tr>
                                <td class="tdSpace" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" width="20%">
                                    Plant
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtPlantCenvat" runat="server" CssClass="textboxAutocomplete" MaxLength="4"
                                        Width="40"  onfocus="return PlantCenvatOnFocus();" onchange="return PlantCenvatTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtPlantCenvat" runat="server" ControlToValidate="txtPlantCenvat"
                                        ValidationGroup="save" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD" width="20%">
                                    Input material for Modvat
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtInputMaterialModvat" runat="server" CssClass="textboxAutocomplete"
                                        MaxLength="18" Width="180"  onfocus="return InputMaterialModvatOnFocus();" onchange="return InputMaterialModvatTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtInputMaterialModvat" runat="server" ControlToValidate="txtInputMaterialModvat"
                                        ValidationGroup="save" ErrorMessage="Input material for Modvat cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Output material for Modvat
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtOutputMaterialModvat" runat="server" CssClass="textboxAutocomplete"
                                        MaxLength="18" Width="180"  onfocus="return OutputMaterialModvatOnFocus();" onchange="return OutputMaterialModvatTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtOutputMaterialModvat" runat="server" ControlToValidate="txtOutputMaterialModvat"
                                        ValidationGroup="save" ErrorMessage="Output material for Modvat cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Default Indicator for Modvat
                                </td>
                                <td class="rigthTD">
                                    <asp:CheckBox ID="chkDefaultIndicatorModvat" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Excise Intimation Date
                                </td>
                                <td class="rigthTD" colspan="3">
                                    <asp:TextBox ID="txtExciseIntimationDate" runat="server" CssClass="textbox" MaxLength="10"
                                        Width="100" />
                                    <asp:RequiredFieldValidator ID="reqtxtExciseIntimationDate" runat="server" ControlToValidate="txtExciseIntimationDate"
                                        ValidationGroup="save" ErrorMessage="Excise Intimation Date cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlVendorExciseDetails" runat="server" Visible="false">
                        <table border="0" cellpadding="0" cellspacing="1" width="100%">
                            <tr>
                                <td class="tdSpace" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" width="20%">
                                    Account Number of Vendor or Creditor
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtVendorAccountNo" runat="server" CssClass="textboxAutocomplete"
                                        MaxLength="10" Width="100"  onfocus="return VendorAccountNoOnFocus();" onchange="return VendorAccountNoTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtVendorAccountNo" runat="server" ControlToValidate="txtVendorAccountNo"
                                        ValidationGroup="save" ErrorMessage="Account Number of Vendor or Creditor cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD" width="20%">
                                    ECC Number
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtECCNumber" runat="server" CssClass="textbox" MaxLength="40"  />
                                    <asp:RequiredFieldValidator ID="reqtxtECCNumber" runat="server" ControlToValidate="txtECCNumber"
                                        ValidationGroup="save" ErrorMessage="ECC Number cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Excise Registration Number
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtExciseRegNo" runat="server" CssClass="textbox" MaxLength="40"  />
                                    <asp:RequiredFieldValidator ID="reqtxtExciseRegNo" runat="server" ControlToValidate="txtExciseRegNo"
                                        ValidationGroup="save" ErrorMessage="Excise Registration Number cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Excise Range
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtExciseRange" runat="server" CssClass="textbox" MaxLength="60" />
                                    <asp:RequiredFieldValidator ID="reqtxtExciseRange" runat="server" ControlToValidate="txtExciseRange"
                                        ValidationGroup="save" ErrorMessage="Excise Range cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Excise Division
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtExciseDivision" runat="server" CssClass="textbox" MaxLength="60" />
                                    <asp:RequiredFieldValidator ID="reqtxtExciseDivision" runat="server" ControlToValidate="txtExciseDivision"
                                        ValidationGroup="save" ErrorMessage="Excise Division cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Excise Commissionerate
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtExciseCommissionerate" runat="server" CssClass="textbox" MaxLength="60" />
                                    <asp:RequiredFieldValidator ID="reqtxtExciseCommissionerate" runat="server" ControlToValidate="txtExciseCommissionerate"
                                        ValidationGroup="save" ErrorMessage="Excise Commissionerate cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Central Sales Tax Number
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtCentralSalesTaxNo" runat="server" CssClass="textbox" MaxLength="40" />
                                    <asp:RequiredFieldValidator ID="reqtxtCentralSalesTaxNo" runat="server" ControlToValidate="txtCentralSalesTaxNo"
                                        ValidationGroup="save" ErrorMessage="Central Sales Tax Number cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Local Sales Tax Number
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtLocalSalesTaxNo" runat="server" CssClass="textbox" MaxLength="40" />
                                    <asp:RequiredFieldValidator ID="reqtxtLocalSalesTaxNo" runat="server" ControlToValidate="txtLocalSalesTaxNo"
                                        ValidationGroup="save" ErrorMessage="Local Sales Tax Number cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Permanent Account Number
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtPermanentAccountNo" runat="server" CssClass="textbox" MaxLength="40" />
                                    <asp:RequiredFieldValidator ID="reqtxtPermanentAccountNo" runat="server" ControlToValidate="txtPermanentAccountNo"
                                        ValidationGroup="save" ErrorMessage="Permanent Account Number cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Excise tax indicator for vendor
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtExciseTaxndicator" runat="server" CssClass="textboxAutocomplete"
                                        MaxLength="1" Width="10"  onfocus="return ExciseTaxndicatorOnFocus();" onchange="return ExciseTaxndicatorTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtExciseTaxndicator" runat="server" ControlToValidate="txtExciseTaxndicator"
                                        ValidationGroup="save" ErrorMessage="Excise tax indicator for vendor cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    SSI Status
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtSSIStatus" runat="server" CssClass="textboxAutocomplete" MaxLength="1"
                                        Width="10"  onfocus="return SSIStatusOnFocus();" onchange="return SSIStatusTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtSSIStatus" runat="server" ControlToValidate="txtSSIStatus"
                                        ValidationGroup="save" ErrorMessage="SSI Status cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Type of Vendor
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtTypeOfVendor" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                        Width="20"  onfocus="return TypeOfVendorOnFocus();" onchange="return TypeOfVendorTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtTypeOfVendor" runat="server" ControlToValidate="txtTypeOfVendor"
                                        ValidationGroup="save" ErrorMessage="Type of Vendor cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    CENVAT Scheme Participant
                                </td>
                                <td class="rigthTD">
                                    <asp:CheckBox ID="chkCenvatSchemeParticipant" runat="server" />
                                </td>
                                <td class="leftTD">
                                    Service Tax Registration Number
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtServiceTaxRegNo" runat="server" CssClass="textbox" MaxLength="40" />
                                    <asp:RequiredFieldValidator ID="reqtxtServiceTaxRegNo" runat="server" ControlToValidate="txtServiceTaxRegNo"
                                        ValidationGroup="save" ErrorMessage="Service Tax Registration Number cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    PAN Reference Number
                                </td>
                                <td class="rigthTD" colspan="3">
                                    <asp:TextBox ID="txtPANReferenceNumber" runat="server" CssClass="textbox" MaxLength="40" />
                                    <asp:RequiredFieldValidator ID="reqtxtPANReferenceNumber" runat="server" ControlToValidate="txtPANReferenceNumber"
                                        ValidationGroup="save" ErrorMessage="PAN Reference Number cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlCustomerExciseDetails" runat="server" Visible="false">
                        <table border="0" cellpadding="0" cellspacing="1" width="100%">
                            <tr>
                                <td class="tdSpace" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" width="20%">
                                    Customer Number
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtCustomerNo" runat="server" CssClass="textboxAutocomplete" MaxLength="10"
                                        Width="100"  onfocus="return CustomerNoOnFocus();" onchange="return CustomerNoTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtCustomerNo" runat="server" ControlToValidate="txtCustomerNo"
                                        ValidationGroup="save" ErrorMessage="Customer Number cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD" width="20%">
                                    ECC Number
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtECCNumberCust" runat="server" CssClass="textbox"
                                        MaxLength="40" />
                                    <asp:RequiredFieldValidator ID="reqtxtECCNumberCust" runat="server" ControlToValidate="txtECCNumberCust"
                                        ValidationGroup="save" ErrorMessage="ECC Number cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Excise Registration Number
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtExciseRegNoCust" runat="server" CssClass="textbox"
                                        MaxLength="40"/>
                                    <asp:RequiredFieldValidator ID="reqtxtExciseRegNoCust" runat="server" ControlToValidate="txtExciseRegNoCust"
                                        ValidationGroup="save" ErrorMessage="Excise Registration Number cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Excise Range
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtExciseRangeCust" runat="server" CssClass="textbox"
                                        MaxLength="60"/>
                                    <asp:RequiredFieldValidator ID="reqtxtExciseRangeCust" runat="server" ControlToValidate="txtExciseRangeCust"
                                        ValidationGroup="save" ErrorMessage="Excise Range cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Excise Division
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtExciseDivisionCust" runat="server" CssClass="textbox" MaxLength="60" />
                                    <asp:RequiredFieldValidator ID="reqtxtExciseDivisionCust" runat="server" ControlToValidate="txtExciseDivisionCust"
                                        ValidationGroup="save" ErrorMessage="Excise Division cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Excise Commissionerate
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtExciseCommissionerateCust" runat="server" CssClass="textbox"
                                        MaxLength="60" />
                                    <asp:RequiredFieldValidator ID="reqtxtExciseCommissionerateCust" runat="server" ControlToValidate="txtExciseCommissionerateCust"
                                        ValidationGroup="save" ErrorMessage="Excise Commissionerate cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Central Sales Tax Number
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtCentralSalesTaxNoCust" runat="server" CssClass="textbox" MaxLength="40" />
                                    <asp:RequiredFieldValidator ID="reqtxtCentralSalesTaxNoCust" runat="server" ControlToValidate="txtCentralSalesTaxNoCust"
                                        ValidationGroup="save" ErrorMessage="Central Sales Tax Number cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Local Sales Tax Number
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtLocalSalesTaxNoCust" runat="server" CssClass="textbox" MaxLength="40" />
                                    <asp:RequiredFieldValidator ID="reqtxtLocalSalesTaxNoCust" runat="server" ControlToValidate="txtLocalSalesTaxNoCust"
                                        ValidationGroup="save" ErrorMessage="Local Sales Tax Number cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Permanent Account Number
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtPermanentAccountNoCust" runat="server" CssClass="textbox" MaxLength="40" />
                                    <asp:RequiredFieldValidator ID="reqtxtPermanentAccountNoCust" runat="server" ControlToValidate="txtPermanentAccountNoCust"
                                        ValidationGroup="save" ErrorMessage="Permanent Account Number cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Excise tax indicator for vendor
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtExciseTaxndicatorCust" runat="server" CssClass="textboxAutocomplete"
                                        MaxLength="1" Width="10"  onfocus="return ExciseTaxndicatorCustOnFocus();" onchange="return ExciseTaxndicatorCustTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtExciseTaxndicatorCust" runat="server" ControlToValidate="txtExciseTaxndicatorCust"
                                        ValidationGroup="save" ErrorMessage="Excise tax indicator for vendor cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Service Tax Registration Number
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtServiceTaxRegNoCust" runat="server" CssClass="textbox" MaxLength="40" />
                                    <asp:RequiredFieldValidator ID="reqtxtServiceTaxRegNoCust" runat="server" ControlToValidate="txtServiceTaxRegNoCust"
                                        ValidationGroup="save" ErrorMessage="Service Tax Registration Number cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    PAN Reference Number
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtPANReferenceNumberCust" runat="server" CssClass="textbox" MaxLength="40" />
                                    <asp:RequiredFieldValidator ID="reqtxtPANReferenceNumberCust" runat="server" ControlToValidate="txtPANReferenceNumberCust"
                                        ValidationGroup="save" ErrorMessage="PAN Reference Number cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlExciseTaxRate" runat="server" Visible="false">
                        <table border="0" cellpadding="0" cellspacing="1" width="100%">
                            <tr>
                                <td class="tdSpace" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" width="20%">
                                    Chapter ID
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtChapterIdETR" runat="server" CssClass="textboxAutocomplete" MaxLength="12"
                                        Width="120"  onfocus="return ChapterIdETROnFocus();" onchange="return ChapterIdETRTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtChapterIdETR" runat="server" ControlToValidate="txtChapterIdETR"
                                        ValidationGroup="save" ErrorMessage="Chapter ID cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD" width="20%">
                                    Excise tax indicator
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtExciseTaxIndicator" runat="server" CssClass="textbox" MaxLength="1"
                                        Width="10" />
                                    <asp:RequiredFieldValidator ID="reqtxtExciseTaxIndicator" runat="server" ControlToValidate="txtExciseTaxIndicator"
                                        ValidationGroup="save" ErrorMessage="Excise tax indicator cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Date from which the tax rule is valid
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtDateFromRuleValid" runat="server" CssClass="textbox" MaxLength="8"
                                        Width="80" />
                                    <asp:RequiredFieldValidator ID="reqtxtDateFromRuleValid" runat="server" ControlToValidate="txtDateFromRuleValid"
                                        ValidationGroup="save" ErrorMessage="Date from which the tax rule is valid cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Date to which the tax rule is valid
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtDateToRuleValid" runat="server" CssClass="textbox" MaxLength="8"
                                        Width="80" />
                                    <asp:RequiredFieldValidator ID="reqtxtDateToRuleValid" runat="server" ControlToValidate="txtDateToRuleValid"
                                        ValidationGroup="save" ErrorMessage="   Date to which the tax rule is valid cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Rate of Excise Duty
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtRateExciseDuty" runat="server" CssClass="textbox" MaxLength="7"
                                        Width="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtRateExciseDuty" runat="server" ControlToValidate="txtRateExciseDuty"
                                        ValidationGroup="save" ErrorMessage="Rate of Excise Duty cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Excise Duty Rate
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtExciseDuteRate" runat="server" CssClass="textbox" MaxLength="13"
                                        Width="130" />
                                    <asp:RequiredFieldValidator ID="reqtxtExciseDuteRate" runat="server" ControlToValidate="txtExciseDuteRate"
                                        ValidationGroup="save" ErrorMessage="Excise Duty Rate cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Rate unit (currency or percentage)
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtRateUnit" runat="server" CssClass="textboxAutocomplete" MaxLength="5"
                                        Width="50"  onfocus="return RateUnitOnFocus();" onchange="return RateUnitTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtRateUnit" runat="server" ControlToValidate="txtRateUnit"
                                        ValidationGroup="save" ErrorMessage="Rate unit (currency or percentage) cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Condition pricing unit
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtConditionPricingUnit" runat="server" CssClass="textbox" MaxLength="5"
                                        Width="50" />
                                    <asp:RequiredFieldValidator ID="reqtxtConditionPricingUnit" runat="server" ControlToValidate="txtConditionPricingUnit"
                                        ValidationGroup="save" ErrorMessage="Condition pricing unit cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Condition unit
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtConditionUnit" runat="server" CssClass="textboxAutocomplete"
                                        MaxLength="3" Width="30" onfocus="return ConditionUnitOnFocus();" onchange="return ConditionUnitTextChangeEvent();" />
                                    <asp:RequiredFieldValidator ID="reqtxtConditionUnit" runat="server" ControlToValidate="txtConditionUnit"
                                        ValidationGroup="save" ErrorMessage="Condition unit cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Additional Excise Duty %
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtAdditionExciseDuty" runat="server" CssClass="textbox" MaxLength="7"
                                        Width="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtAdditionExciseDuty" runat="server" ControlToValidate="txtAdditionExciseDuty"
                                        ValidationGroup="save" ErrorMessage="Additional Excise Duty % cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Special Excise Duty %
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtSpecialExciseDuty" runat="server" CssClass="textbox" MaxLength="7"
                                        Width="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtSpecialExciseDuty" runat="server" ControlToValidate="txtSpecialExciseDuty"
                                        ValidationGroup="save" ErrorMessage="Special Excise Duty % cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    NCCD Rate in %
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtMCCDRate" runat="server" CssClass="textbox" MaxLength="7" Width="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtMCCDRate" runat="server" ControlToValidate="txtMCCDRate"
                                        ValidationGroup="save" ErrorMessage="NCCD Rate in % cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    ECS rate in %
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtECSRate" runat="server" CssClass="textbox" MaxLength="7" Width="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtECSRate" runat="server" ControlToValidate="txtECSRate"
                                        ValidationGroup="save" ErrorMessage="ECS rate  in % cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    AT1 rate in %
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtAT1Rate" runat="server" CssClass="textbox" MaxLength="7" Width="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtAT1Rate" runat="server" ControlToValidate="txtAT1Rate"
                                        ValidationGroup="save" ErrorMessage="AT1 rate in % cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    AT2 rate in %
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtAT2Rate" runat="server" CssClass="textbox" MaxLength="7" Width="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtAT2Rate" runat="server" ControlToValidate="txtAT2Rate"
                                        ValidationGroup="save" ErrorMessage="AT2 rate in % cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    AT3 rate in %
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtAT3Rate" runat="server" CssClass="textbox" MaxLength="7" Width="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtAT3Rate" runat="server" ControlToValidate="txtAT3Rate"
                                        ValidationGroup="save" ErrorMessage="AT3 rate in % cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlExceptionMaterialExciseRate" runat="server" Visible="false">
                        <table border="0" cellpadding="0" cellspacing="1" width="100%">
                            <tr>
                                <td class="tdSpace" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD" width="20%">
                                    Plant
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtPlantException" runat="server" CssClass="textboxAutocomplete"
                                        MaxLength="4" Width="40"  onfocus="return PlantExceptionOnFocus();" onchange="return PlantExceptionTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtPlantException" runat="server" ControlToValidate="txtPlantException"
                                        ValidationGroup="save" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD" width="20%">
                                    Material Number
                                </td>
                                <td class="rigthTD" style="width: 30%">
                                    <asp:TextBox ID="txtMaterialNumberEX" runat="server" CssClass="textboxAutocomplete"
                                        MaxLength="18"  onfocus="return MaterialNumberEXOnFocus();" onchange="return MaterialNumberEXTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtMaterialNumberEX" runat="server" ControlToValidate="txtMaterialNumberEX"
                                        ValidationGroup="save" ErrorMessage="Material Number cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Account Number of Vendor or Creditor
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtAccountNoEX" runat="server" CssClass="textboxAutocomplete" MaxLength="10"
                                        Width="100" onfocus="return AccountNoEXOnFocus();" onchange="return AccountNoEXTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtAccountNoEX" runat="server" ControlToValidate="txtAccountNoEX"
                                        ValidationGroup="save" ErrorMessage="Account Number of Vendor or Creditor is valid cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Date from which the tax rule is valid
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtDateFromTaxRuleValidEX" runat="server" CssClass="textbox" MaxLength="8"
                                        Width="80" />
                                    <asp:RequiredFieldValidator ID="reqtxtDateFromTaxRuleValidEX" runat="server" ControlToValidate="txtDateFromTaxRuleValidEX"
                                        ValidationGroup="save" ErrorMessage="Date from which the tax rule is valid cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Type of Excise duty
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtTypeExciseDutyEX" runat="server" CssClass="textboxAutocomplete"
                                        MaxLength="3" Width="30"  onfocus="return TypeExciseDutyEXOnFocus();" onchange="return TypeExciseDutyEXTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtTypeExciseDutyEX" runat="server" ControlToValidate="txtTypeExciseDutyEX"
                                        ValidationGroup="save" ErrorMessage="Type of Excise duty cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Date to which the tax rule is valid
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtDateToTaxRuleValidEX" runat="server" CssClass="textbox" MaxLength="8"
                                        Width="80" />
                                    <asp:RequiredFieldValidator ID="reqtxtDateToTaxRuleValidEX" runat="server" ControlToValidate="txtDateToTaxRuleValidEX"
                                        ValidationGroup="save" ErrorMessage="Date to which the tax rule is valid cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Chapter ID
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtChapterIDEX" runat="server" CssClass="textboxAutocomplete" MaxLength="12"
                                        Width="120"  onfocus="return ChapterIDEXOnFocus();" onchange="return ChapterIDEXTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtChapterIDEX" runat="server" ControlToValidate="txtChapterIDEX"
                                        ValidationGroup="save" ErrorMessage="Rate unit (currency or percentage) cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Rate of Excise Duty
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtRateExciseDutyEX" runat="server" CssClass="textbox" MaxLength="7"
                                        Width="70" />
                                    <asp:RequiredFieldValidator ID="reqtxtRateExciseDutyEX" runat="server" ControlToValidate="txtRateExciseDutyEX"
                                        ValidationGroup="save" ErrorMessage="Rate of Excise Duty cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Excise Duty Rate
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtExciseDutyRateEX" runat="server" CssClass="textbox"
                                        MaxLength="13" Width="130"/>
                                    <asp:RequiredFieldValidator ID="reqtxtExciseDutyRateEX" runat="server" ControlToValidate="txtExciseDutyRateEX"
                                        ValidationGroup="save" ErrorMessage="Excise Duty Rate cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Rate unit (currency or percentage)
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtRateUnitEX" runat="server" CssClass="textboxAutocomplete" MaxLength="5" Width="50"  onfocus="return RateUnitEXOnFocus();" onchange="return RateUnitEXTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtRateUnitEX" runat="server" ControlToValidate="txtRateUnitEX"
                                        ValidationGroup="save" ErrorMessage="Rate unit (currency or percentage) cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftTD">
                                    Condition pricing unit
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtConditionPricingUnitEX" runat="server" CssClass="textbox" MaxLength="5"
                                        Width="50" />
                                    <asp:RequiredFieldValidator ID="reqtxtConditionPricingUnitEX" runat="server" ControlToValidate="txtConditionPricingUnitEX"
                                        ValidationGroup="save" ErrorMessage="Condition pricing unit cannot be blank."
                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                                <td class="leftTD">
                                    Condition unit
                                </td>
                                <td class="rigthTD">
                                    <asp:TextBox ID="txtConditionunitEX" runat="server" CssClass="textboxAutocomplete" MaxLength="3"
                                        Width="30"  onfocus="return ConditionunitEXOnFocus();" onchange="return ConditionunitEXTextChangeEvent();"/>
                                    <asp:RequiredFieldValidator ID="reqtxtConditionunitEX" runat="server" ControlToValidate="txtConditionunitEX"
                                        ValidationGroup="save" ErrorMessage="Condition unit cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' />" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" OnClick="btnSubmit_Click"
                        ValidationGroup="save" />
                    <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" CssClass="button"
                        OnClick="btnBack_Click" />
                    <asp:Button ID="btnRejectTo" runat="server" Text="Reject" CssClass="button" OnClientClick="return ShowRollbackPopup();" />
                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                        <asp:Label ID="Label1" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
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
    <div id="divModulePopUp" style="display: none;" title="Material Master">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="leftTD" width="25%">
                    Excise Type
                </td>
                <td class="rigthTD">
                    <asp:DropDownList ID="ddlExciseType" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select" Value="" />
                        <asp:ListItem Text="Chapter Id" Value="21" />
                        <asp:ListItem Text="Material and Chapter-Id Combination" Value="22" />
                        <asp:ListItem Text="CENVAT  Determination" Value="23" />
                        <asp:ListItem Text="Vendor Excise Details" Value="24" />
                        <asp:ListItem Text="Customer Excise Details" Value="25" />
                        <asp:ListItem Text="Excise Tax Rate" Value="26" />
                        <asp:ListItem Text="Exception Material Excise Rate" Value="27" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlExciseType"
                        ValidationGroup="next" ErrorMessage="Select Excise Type." SetFocusOnError="true"
                        Display="Dynamic" Text="<img src='../images/Error.png' title='Select Excise Type.' />" />
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnNext" runat="server" ValidationGroup="next" Text="Next" CssClass="button"
                        OnClick="btnNext_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblExciseId" runat="server" Visible="false" />
    <asp:Label ID="lblCostingId" runat="server" Visible="false" />
    <asp:Label ID="lblIsUserApprover" runat="server" Visible="false" />
    <script type="text/javascript">
        var textboxId = "";
        var textboxRealId = "";

        function UOMExciseOnFocus() {
            textboxId = $('#<%= txtUOMExcise.ClientID%>').attr('ID');
            textboxRealId = "txtUOMExcise";
            AutoCompleteLookUpExcise();
        }

        function UOMExciseTextChangeEvent() {
            CheckLookupExcise($('#<%= txtUOMExcise.ClientID%>').attr('ID'), "txtUOMExcise", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function MaterialNumberOnFocus() {
            textboxId = $('#<%= txtMaterialNumber.ClientID%>').attr('ID');
            textboxRealId = "txtMaterialNumber";
            AutoCompleteLookUpExcise();
        }

        function MaterialNumberTextChangeEvent() {
            CheckLookupExcise($('#<%= txtMaterialNumber.ClientID%>').attr('ID'), "txtMaterialNumber", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function PlantOnFocus() {
            textboxId = $('#<%= txtPlant.ClientID%>').attr('ID');
            textboxRealId = "txtPlant";
            AutoCompleteLookUpExcise();
        }

        function PlantTextChangeEvent() {
            CheckLookupExcise($('#<%= txtPlant.ClientID%>').attr('ID'), "txtPlant", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function ChapterId_mcCombinationOnFocus() {
            textboxId = $('#<%= txtChapterId_mcCombination.ClientID%>').attr('ID');
            textboxRealId = "txtChapterId_mcCombination";
            AutoCompleteLookUpExcise();
        }

        function ChapterId_mcCombinationTextChangeEvent() {
            CheckLookupExcise($('#<%= txtChapterId_mcCombination.ClientID%>').attr('ID'), "txtChapterId_mcCombination", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function MaterialTypeOnFocus() {
            textboxId = $('#<%= txtMaterialType.ClientID%>').attr('ID');
            textboxRealId = "txtMaterialType";
            AutoCompleteLookUpExcise();
        }

        function MaterialTypeTextChangeEvent() {
            CheckLookupExcise($('#<%= txtMaterialType.ClientID%>').attr('ID'), "txtMaterialType", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function PlantCenvatOnFocus() {
            textboxId = $('#<%= txtPlantCenvat.ClientID%>').attr('ID');
            textboxRealId = "txtPlantCenvat";
            AutoCompleteLookUpExcise();
        }

        function PlantCenvatTextChangeEvent() {
            CheckLookupExcise($('#<%= txtPlantCenvat.ClientID%>').attr('ID'), "txtPlantCenvat", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function InputMaterialModvatOnFocus() {
            textboxId = $('#<%= txtInputMaterialModvat.ClientID%>').attr('ID');
            textboxRealId = "txtInputMaterialModvat";
            AutoCompleteLookUpExcise();
        }

        function InputMaterialModvatTextChangeEvent() {
            CheckLookupExcise($('#<%= txtInputMaterialModvat.ClientID%>').attr('ID'), "txtInputMaterialModvat", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function OutputMaterialModvatOnFocus() {
            textboxId = $('#<%= txtOutputMaterialModvat.ClientID%>').attr('ID');
            textboxRealId = "txtOutputMaterialModvat";
            AutoCompleteLookUpExcise();
        }

        function OutputMaterialModvatTextChangeEvent() {
            CheckLookupExcise($('#<%= txtOutputMaterialModvat.ClientID%>').attr('ID'), "txtOutputMaterialModvat", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function VendorAccountNoOnFocus() {
            textboxId = $('#<%= txtVendorAccountNo.ClientID%>').attr('ID');
            textboxRealId = "txtVendorAccountNo";
            AutoCompleteLookUpExcise();
        }

        function VendorAccountNoTextChangeEvent() {
            CheckLookupExcise($('#<%= txtVendorAccountNo.ClientID%>').attr('ID'), "txtVendorAccountNo", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function ExciseTaxndicatorOnFocus() {
            textboxId = $('#<%= txtExciseTaxndicator.ClientID%>').attr('ID');
            textboxRealId = "txtExciseTaxndicator";
            AutoCompleteLookUpExcise();
        }

        function ExciseTaxndicatorTextChangeEvent() {
            CheckLookupExcise($('#<%= txtExciseTaxndicator.ClientID%>').attr('ID'), "txtExciseTaxndicator", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function SSIStatusOnFocus() {
            textboxId = $('#<%= txtSSIStatus.ClientID%>').attr('ID');
            textboxRealId = "txtSSIStatus";
            AutoCompleteLookUpExcise();
        }

        function SSIStatusTextChangeEvent() {
            CheckLookupExcise($('#<%= txtSSIStatus.ClientID%>').attr('ID'), "txtSSIStatus", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function TypeOfVendorOnFocus() {
            textboxId = $('#<%= txtTypeOfVendor.ClientID%>').attr('ID');
            textboxRealId = "txtTypeOfVendor";
            AutoCompleteLookUpExcise();
        }

        function TypeOfVendorTextChangeEvent() {
            CheckLookupExcise($('#<%= txtTypeOfVendor.ClientID%>').attr('ID'), "txtTypeOfVendor", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function CustomerNoOnFocus() {
            textboxId = $('#<%= txtCustomerNo.ClientID%>').attr('ID');
            textboxRealId = "txtCustomerNo";
            AutoCompleteLookUpExcise();
        }

        function CustomerNoTextChangeEvent() {
            CheckLookupExcise($('#<%= txtCustomerNo.ClientID%>').attr('ID'), "txtCustomerNo", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function ExciseTaxndicatorCustOnFocus() {
            textboxId = $('#<%= txtExciseTaxndicatorCust.ClientID%>').attr('ID');
            textboxRealId = "txtExciseTaxndicatorCust";
            AutoCompleteLookUpExcise();
        }

        function ExciseTaxndicatorCustTextChangeEvent() {
            CheckLookupExcise($('#<%= txtExciseTaxndicatorCust.ClientID%>').attr('ID'), "txtExciseTaxndicatorCust", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function ChapterIdETROnFocus() {
            textboxId = $('#<%= txtChapterIdETR.ClientID%>').attr('ID');
            textboxRealId = "txtChapterIdETR";
            AutoCompleteLookUpExcise();
        }

        function ChapterIdETRTextChangeEvent() {
            CheckLookupExcise($('#<%= txtChapterIdETR.ClientID%>').attr('ID'), "txtChapterIdETR", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function RateUnitOnFocus() {
            textboxId = $('#<%= txtRateUnit.ClientID%>').attr('ID');
            textboxRealId = "txtRateUnit";
            AutoCompleteLookUpExcise();
        }

        function RateUnitTextChangeEvent() {
            CheckLookupExcise($('#<%= txtRateUnit.ClientID%>').attr('ID'), "txtRateUnit", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function ConditionUnitOnFocus() {
            textboxId = $('#<%= txtConditionUnit.ClientID%>').attr('ID');
            textboxRealId = "txtConditionUnit";
            AutoCompleteLookUpExcise();
        }

        function ConditionUnitTextChangeEvent() {
            CheckLookupExcise($('#<%= txtConditionUnit.ClientID%>').attr('ID'), "txtConditionUnit", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function PlantExceptionOnFocus() {
            textboxId = $('#<%= txtPlantException.ClientID%>').attr('ID');
            textboxRealId = "txtPlantException";
            AutoCompleteLookUpExcise();
        }

        function PlantExceptionTextChangeEvent() {
            CheckLookupExcise($('#<%= txtPlantException.ClientID%>').attr('ID'), "txtPlantException", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function MaterialNumberEXOnFocus() {
            textboxId = $('#<%= txtMaterialNumberEX.ClientID%>').attr('ID');
            textboxRealId = "txtMaterialNumberEX";
            AutoCompleteLookUpExcise();
        }

        function MaterialNumberEXTextChangeEvent() {
            CheckLookupExcise($('#<%= txtMaterialNumberEX.ClientID%>').attr('ID'), "txtMaterialNumberEX", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function AccountNoEXOnFocus() {
            textboxId = $('#<%= txtAccountNoEX.ClientID%>').attr('ID');
            textboxRealId = "txtAccountNoEX";
            AutoCompleteLookUpExcise();
        }

        function AccountNoEXTextChangeEvent() {
            CheckLookupExcise($('#<%= txtAccountNoEX.ClientID%>').attr('ID'), "txtAccountNoEX", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function TypeExciseDutyEXOnFocus() {
            textboxId = $('#<%= txtTypeExciseDutyEX.ClientID%>').attr('ID');
            textboxRealId = "txtTypeExciseDutyEX";
            AutoCompleteLookUpExcise();
        }

        function TypeExciseDutyEXTextChangeEvent() {
            CheckLookupExcise($('#<%= txtTypeExciseDutyEX.ClientID%>').attr('ID'), "txtTypeExciseDutyEX", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function ChapterIDEXOnFocus() {
            textboxId = $('#<%= txtChapterIDEX.ClientID%>').attr('ID');
            textboxRealId = "txtChapterIDEX";
            AutoCompleteLookUpExcise();
        }

        function ChapterIDEXTextChangeEvent() {
            CheckLookupExcise($('#<%= txtChapterIDEX.ClientID%>').attr('ID'), "txtChapterIDEX", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function RateUnitEXOnFocus() {
            textboxId = $('#<%= txtRateUnitEX.ClientID%>').attr('ID');
            textboxRealId = "txtRateUnitEX";
            AutoCompleteLookUpExcise();
        }

        function RateUnitEXTextChangeEvent() {
            CheckLookupExcise($('#<%= txtRateUnitEX.ClientID%>').attr('ID'), "txtRateUnitEX", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function ConditionunitEXOnFocus() {
            textboxId = $('#<%= txtConditionunitEX.ClientID%>').attr('ID');
            textboxRealId = "txtConditionunitEX";
            AutoCompleteLookUpExcise();
        }

        function ConditionunitEXTextChangeEvent() {
            CheckLookupExcise($('#<%= txtConditionunitEX.ClientID%>').attr('ID'), "txtConditionunitEX", $('#<%= btnNext.ClientID%>').attr('ID'));
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

        function ShowCreateNewDialog() {
            $("#divModulePopUp").dialog({
                height: 140,
                width: 400,
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
        }

    </script>
</asp:Content>
