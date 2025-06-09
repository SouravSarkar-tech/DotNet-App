<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="BasicData1.aspx.cs" Inherits="Transaction_BasicData1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Transaction/UserControl/ucMaterialDescl.ascx" TagName="ucMatDesc" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetToolTip(control) {
            ddrivetipm('3', control);
        }
        function basicPopup() {
            popupWindow = window.open("default.aspx", 'popUpWindow', 'height=500,width=850,left=100,top=30,resizable=Yes,scrollbars=Yes,toolbar=no,menubar=no,location=no,directories=no, status=No');
        }
    </script>

    <style type="text/css">
        .style1 {
            height: 5px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:UpdatePanel ID="UpdMaterialBasic" runat="server">    
   <contenttemplate>--%>
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlAddNew" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">Basic Data 1
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Material Number
                                <asp:Label ID="labletxtMaterialNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtMaterialNo" runat="server" CssClass="textbox" MaxLength="18"
                                    TabIndex="1" Enabled="false" Width="180" />
                            </td>
                            <td class="leftTD">Material Description
                                <asp:Label ID="labletxtMaterialDescription" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <%--Style="text-transform: uppercase"--%>
                                <asp:TextBox ID="txtMaterialDescription" runat="server" CssClass="textbox"
                                    MaxLength="40" size="41"
                                    TabIndex="2" Width="281px" />
                                <asp:RequiredFieldValidator ID="reqtxtMaterialDescription" runat="server" ControlToValidate="txtMaterialDescription"
                                    ValidationGroup="BasicData" ErrorMessage="Material Description cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Description cannot be blank.' />" />
                            </td>
                        </tr>
                        <%--PROSOL_SDT16092019--%>
                        <tr id="trProsolId" runat="server" clientidmode="Static" visible="false">

                            <td class="tdSpace" colspan="4" align="right">
                                <asp:Button ID="btnSPOCProsol" runat="server" Text="Edit Material Description"
                                    CssClass="button" OnClick="btnSPOCProsol_Click" />
                            </td>
                        </tr>
                        <%--PROSOL_SDT16092019--%>
                        <%-- <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>--%>
                        <tr id="testlnk">
                            <td class="tdSpace" colspan="4" align="right">
                                <asp:Image runat="server" src="../../images/graphics_arrows.gif" Style="width: 16px; height: 16px; cursor: pointer;" ID="testImg" Visible="false" />
                                <asp:LinkButton ID="lnkAddMatDesc" runat="server" Font-Bold="false" Text="(Show/Hide Material Description)"
                                    Visible="false" OnClick="lnkAddMatDesc_Click"></asp:LinkButton>
                            </td>
                        </tr>

                        <%--Srinidhi--%>
                        <tr>
                            <td class="tdSpace" colspan="4">
                                <asp:Panel ID="pnlMatDesc" runat="server" Visible="false" BorderWidth="2px" BorderColor="Black"
                                    BorderStyle="Solid">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="updDescMat" runat="server">
                                                    <ContentTemplate>
                                                        <uc:ucMatDesc ID="ucMatDescription" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr id="trMatDesc" runat="server" visible="false">
                                            <td class="centerTD">
                                                <asp:Button ID="btnDescSave" runat="server" ValidationGroup="MaterialDesc" Text="Generate Description"
                                                    CssClass="button" OnClick="btnDescSave_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <%--Srinidhi--%>

                        <tr>
                            <td class="leftTD" style="width: 20%">Material Type
                                <asp:Label ID="lableddlMaterialType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlMaterialType" runat="server" AppendDataBoundItems="false"
                                    TabIndex="3">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlMaterialType" runat="server" ControlToValidate="ddlMaterialType"
                                    ValidationGroup="BasicData" ErrorMessage="Material Type cannot be blank." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Type cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Industry Sector
                                <asp:Label ID="lableddlIndustrySector" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlIndustrySector" runat="server" AppendDataBoundItems="false"
                                    TabIndex="4">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlIndustrySector" runat="server" ControlToValidate="ddlMaterialType"
                                    ValidationGroup="BasicData" ErrorMessage="Industry Sector cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Industry Sector cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Base Unit of Measure
                                <asp:Label ID="lableddlBaseUnit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlBaseUnit" runat="server" AppendDataBoundItems="false" TabIndex="5"
                                    OnSelectedIndexChanged="ddlBaseUnit_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlBaseUnit" runat="server" ControlToValidate="ddlBaseUnit"
                                    ValidationGroup="BasicData" ErrorMessage="Base Unit of Measure cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Base Unit of Measure cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Material Group
                                <asp:Label ID="lableddlMaterialGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlMaterialGroup');"
                                    onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlMaterialGroup" runat="server" AppendDataBoundItems="false"
                                    TabIndex="6">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlMaterialGroup" runat="server" ControlToValidate="ddlMaterialGroup"
                                    ValidationGroup="BasicData" ErrorMessage="Material Group cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Group cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Old Material Number
                                <asp:Label ID="labletxtOldMaterialNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtOldMaterialNo" runat="server" CssClass="textbox" MaxLength="18"
                                    TabIndex="7" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtOldMaterialNo" runat="server" ControlToValidate="txtOldMaterialNo"
                                    ValidationGroup="BasicData" ErrorMessage="Old Material Number cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Old Material Number cannot be blank.' />" />
                            </td>
                            <td class="leftTD">External Material Grp.
                                <asp:Label ID="lableddlExternalMaterialGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlExternalMaterialGroup" runat="server" AppendDataBoundItems="false"
                                    TabIndex="8">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlExternalMaterialGroup" runat="server" ControlToValidate="ddlExternalMaterialGroup"
                                    ValidationGroup="BasicData" ErrorMessage="External Material Grp. cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='External Material Grp. cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Division 
                                <asp:Label ID="lableddlDivision" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlDivision" runat="server" AppendDataBoundItems="false"
                                    TabIndex="9" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlDivision" runat="server" ControlToValidate="ddlDivision"
                                    ValidationGroup="BasicData" ErrorMessage="Division cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Division cannot be blank.' />" />
                            </td>
                            <%--CTRL_SUB_SDT06062019, Desc : Controll Substance , Change By : Nitin R--%>
                            <td class="leftTD">Is Contains Chemical 
                                <asp:Label ID="lableddlIsContainsChemical" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlIsContainsChemical" runat="server" AppendDataBoundItems="false"
                                    OnSelectedIndexChanged="ddlIsContainsChemical_SelectedIndexChanged" AutoPostBack="true" TabIndex="9">
                                    <asp:ListItem Value="0" Text="Select" />
                                    <asp:ListItem Value="1" Text="Yes" />
                                    <asp:ListItem Value="2" Text="No" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlIsContainsChemical" runat="server" ControlToValidate="ddlIsContainsChemical"
                                    ValidationGroup="BasicData" ErrorMessage="Is Contains Chemical cannot be blank." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Is Contains Chemical cannot be blank.' />" />
                            </td>
                            <%--CTRL_SUB_EDT06062019, Desc : Controll Substance , Change By : Nitin R--%>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none" id="tridProdHr1" runat="server">
                            <td class="leftTD">Product Hierarchy 1
                                <asp:Label ID="lableddlProductHierarchy1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlProductHierarchy1" runat="server" AppendDataBoundItems="false"
                                    AutoPostBack="true" TabIndex="10" OnSelectedIndexChanged="ddlProductHierarchy1_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlProductHierarchy1" runat="server" ControlToValidate="ddlProductHierarchy1"
                                    ValidationGroup="BasicData" ErrorMessage="Product Hierarchy 1 cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Product Hierarchy 1 cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Product Hierarchy 2
                                <asp:Label ID="lableddlProductHierarchy2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlProductHierarchy2" runat="server" AppendDataBoundItems="false"
                                    AutoPostBack="true" TabIndex="11" OnSelectedIndexChanged="ddlProductHierarchy2_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlProductHierarchy2" runat="server" ControlToValidate="ddlProductHierarchy2"
                                    ValidationGroup="BasicData" ErrorMessage="Product Hierarchy 2 cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Product Hierarchy 2 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none" id="tridProdHr3" runat="server">
                            <td class="leftTD">Product Hierarchy 3
                                <asp:Label ID="lableddlProductHierarchy3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" colspan="3">
                                <asp:DropDownList ID="ddlProductHierarchy3" runat="server" AppendDataBoundItems="false"
                                    TabIndex="12">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlProductHierarchy3" runat="server" ControlToValidate="ddlProductHierarchy3"
                                    ValidationGroup="BasicData" ErrorMessage="Product Hierarchy 3 cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Product Hierarchy 3 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Laboratory/Design Office
                                <asp:Label ID="lableddlLaboratory" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlLaboratory" runat="server" AppendDataBoundItems="false"
                                    TabIndex="13">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlLaboratory" runat="server" ControlToValidate="ddlLaboratory"
                                    ValidationGroup="BasicData" ErrorMessage="Laboratory/Design Office cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Laboratory/Design Office cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Gen Item Category Grp
                                <asp:Label ID="lableddlGenItemCategoryGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlGenItemCategoryGrp" runat="server" AppendDataBoundItems="false"
                                    TabIndex="14">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlGenItemCategoryGrp" runat="server" ControlToValidate="ddlGenItemCategoryGrp"
                                    ValidationGroup="BasicData" ErrorMessage="Gen Item Category Grp cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Gen Item Category Grp cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Cross Plant Material Status
                                <asp:Label ID="lableddlCrossPlantMaterialStatus" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlCrossPlantMaterialStatus" runat="server" AppendDataBoundItems="false"
                                    TabIndex="15">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCrossPlantMaterialStatus" runat="server" ControlToValidate="ddlCrossPlantMaterialStatus"
                                    ValidationGroup="BasicData" ErrorMessage="Division cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Division cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">Valid From
                                <asp:Label ID="labletxtValidFrom" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtValidFrom" runat="server" CssClass="textbox" MaxLength="11" TabIndex="16"
                                    Width="180px" />
                                <ajax:CalendarExtender ID="caltxtValidFrom" runat="server" TargetControlID="txtValidFrom"
                                    Format="dd/MM/yyyy">
                                </ajax:CalendarExtender>
                                <asp:RegularExpressionValidator ID="regtxtValidFrom" runat="server" ControlToValidate="txtValidFrom"
                                    ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                    Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                    ValidationGroup="BasicData" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="reqtxtValidFrom" runat="server" ControlToValidate="txtValidFrom"
                                    ValidationGroup="BasicData" ErrorMessage="CST Date cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='CST Date cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Gross weight
                                <asp:Label ID="labletxtGrossWeight" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtGrossWeight" runat="server" CssClass="textbox" MaxLength="13"
                                    TabIndex="17" Width="130px" onkeypress="return IsNumber();" />
                                <asp:RequiredFieldValidator ID="reqtxtGrossWeight" runat="server" ControlToValidate="txtGrossWeight"
                                    ValidationGroup="BasicData" ErrorMessage="Production/Inspection Memo cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Production/Inspection Memo cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Weight Unit
                                <asp:Label ID="lableddlWeightUnit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlWeightUnit" runat="server" AppendDataBoundItems="false"
                                    TabIndex="18">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlWeightUnit" runat="server" ControlToValidate="ddlWeightUnit"
                                    ValidationGroup="BasicData" ErrorMessage="Weight Unit cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Weight Unit cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Net Weight
                                <asp:Label ID="labletxtNetWeight" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtNetWeight" runat="server" CssClass="textbox" MaxLength="13" Width="130px"
                                    TabIndex="19" onkeypress="return IsNumber();" />
                                <asp:RequiredFieldValidator ID="reqtxtNetWeight" runat="server" ControlToValidate="txtNetWeight"
                                    ValidationGroup="BasicData" ErrorMessage="Net Weight cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Net Weight cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Production/Inspection Memo
                                <asp:Label ID="labletxtProduction" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtProduction" runat="server" CssClass="textbox" MaxLength="18"
                                    TabIndex="20" Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtProduction" runat="server" ControlToValidate="txtProduction"
                                    ValidationGroup="BasicData" ErrorMessage="Production/Inspection Memo cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Production/Inspection Memo cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none" id="tridVolume1" runat="server">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none" id="tridVolume" runat="server">
                            <td class="leftTD">Volume
                                <asp:Label ID="labletxtVolume" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtVolume" runat="server" CssClass="textbox" MaxLength="13" Width="130px"
                                    TabIndex="21" />
                                <asp:RequiredFieldValidator ID="reqtxtVolume" runat="server" ControlToValidate="txtVolume"
                                    ValidationGroup="BasicData" ErrorMessage="Volume cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Volume cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Volume Unit
                                <asp:Label ID="lableddlVolumeUnit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlVolumeUnit" runat="server" AppendDataBoundItems="false"
                                    TabIndex="22">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlVolumeUnit" runat="server" ControlToValidate="ddlVolumeUnit"
                                    ValidationGroup="BasicData" ErrorMessage="Volume Unit cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Volume Unit cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">International Article Number (EAN/UPC)
                                <asp:Label ID="labletxtInternationalANo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtInternationalANo" runat="server" CssClass="textbox" MaxLength="18"
                                    TabIndex="23" Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtInternationalANo" runat="server" ControlToValidate="txtInternationalANo"
                                    ValidationGroup="BasicData" ErrorMessage="International Article Number (EAN/UPC) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='International Article Number (EAN/UPC) cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Category of International Article Number (EAN)
                                <asp:Label ID="lableddlCategoryIA" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" colspan="3">
                                <asp:DropDownList ID="ddlCategoryIA" runat="server" AppendDataBoundItems="false"
                                    TabIndex="24">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCategoryIA" runat="server" ControlToValidate="ddlCategoryIA"
                                    ValidationGroup="BasicData" ErrorMessage="Category of International Article Number (EAN) cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Category of International Article Number (EAN) cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <%--PROV-CCP-MM-941-23-0045 in QAMS--%>
                        <tr>

                            <td class="leftTD">
                                <asp:Label ID="lblddlIsMatComm" runat="server" Text="Is material going to used for commercial?" Visible="false"></asp:Label>

                                <asp:Label ID="lableddlIsMatComm" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlIsMatComm" runat="server" AppendDataBoundItems="false" TabIndex="9" Visible="false">
                                   <asp:ListItem Value="0" Text="Select" />
                                    <asp:ListItem Value="1" Text="Yes" />
                                    <asp:ListItem Value="2" Text="No" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlIsMatComm" runat="server" Visible="false" Enabled="false" ControlToValidate="ddlIsMatComm"
                                    ValidationGroup="BasicData" ErrorMessage="Is material going to used for commercial cannot be blank." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Is material going to used for commercial cannot be blank.' />" />
                            </td>

                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <%--PROV-CCP-MM-941-23-0045 in QAMS--%>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Matl. Grp. Pack. Matl.
                                <asp:Label ID="lableddlMatlGrpPackMatl" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlMatlGrpPackMatl" runat="server" AppendDataBoundItems="false"
                                    AutoPostBack="true" TabIndex="25" OnSelectedIndexChanged="ddlProductHierarchy2_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlMatlGrpPackMatl" runat="server" ControlToValidate="ddlMatlGrpPackMatl"
                                    ValidationGroup="BasicData" ErrorMessage="Matl. Grp. Pack. Matl. cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Matl. Grp. Pack. Matl. cannot be blank.' />" />
                            </td>
                            <%--DT05072023_BG_Type--%>
                            <td class="leftTD">
                                <%--Branded/Generic--%>
                                <asp:Label ID="labelddlBGWCF" runat="server" Text="Material exten to all SKUs (CFA & CWH)"></asp:Label>
                                <asp:Label ID="lblddlBGWCF" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlBGWCF" runat="server" AppendDataBoundItems="false" TabIndex="25" Enabled="false">
                                    <asp:ListItem Value="" Text="Select" />
                                    <asp:ListItem Value="B" Text="Branded SKUs" />
                                    <asp:ListItem Value="G" Text="Generic SKUs" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlBGWCF" runat="server" ControlToValidate="ddlBGWCF" Visible="false"
                                    ValidationGroup="BasicData" ErrorMessage="Please select Material exten to all SKUs (CFA & CWH)."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please select Material exten to all SKUs (CFA & CWH).' />" />
                            </td>
                            <%--DT05072023_BG_Type--%>
                            <%-- <td class="tdSpace" colspan="2"></td>--%>
                        </tr>
                        <tr>
                            <td class="leftTD">Reason For Creation
                                <asp:Label ID="lableddlReason" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlReason" runat="server" AppendDataBoundItems="false" TabIndex="25">
                                    <asp:ListItem Text="Select" Value="" />
                                    <asp:ListItem Text="Process Change" Value="PC" />
                                    <asp:ListItem Text="MOC/Specification/Dimension change" Value="MOC" />
                                    <asp:ListItem Text="One time requirement code" Value="OTR" />
                                    <asp:ListItem Text="No harmonization of code" Value="NHC" />
                                    <asp:ListItem Text="First time purchasing of item" Value="FTP" />
                                    <asp:ListItem Text="Common item Revenue,Consumable" Value="CRC" />
                                    <asp:ListItem Text="For availing excise benefit" Value="AEB" />
                                    <asp:ListItem Text="New Creation" Value="NC" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlReason" runat="server" ControlToValidate="ddlReason"
                                    ValidationGroup="BasicData" ErrorMessage="Reason cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Reason cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Remarks
                                <asp:Label ID="labletxtRemarks" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="textarea" TextMode="MultiLine"
                                    TabIndex="26" Columns="100" Rows="3" />
                                <asp:RequiredFieldValidator ID="reqtxtRemarks" runat="server" ControlToValidate="txtRemarks"
                                    ValidationGroup="BasicData" ErrorMessage="Remarks cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Remarks cannot be blank.' />" />
                            </td>
                            <%--  <td class="tdSpace" colspan="2">
                            </td>--%>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdSpace"></td>
                        </tr>
                        <tr id="trlnkExtnsn">
                            <td class="tdSpace" colspan="4" align="left">
                                <asp:LinkButton ID="lnkExtnsn" runat="server" Font-Bold="false" Text="(Depot Extension MRP data)"
                                    Visible="false" OnClientClick="basicPopup();return false;"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdSpace"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" align="left" colspan="4">
                                <b>Attach Documents (Image/PDF Files Only)</b>
                            </td>
                        </tr>
                        <tr>
                            <td class="rigthTD" align="left" colspan="2" valign="top">
                                <div>
                                    <asp:FileUpload ID="file_upload" class="multi" runat="server" TabIndex="36" />
                                    <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />
                                </div>
                            </td>
                            <td class="rigthTD" align="left" colspan="2">
                                <asp:GridView ID="grdAttachedDocs" runat="server" AutoGenerateColumns="False" DataKeyNames="Document_Upload_Id"
                                    Visible="False" OnRowCommand="grdAttachedDocs_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Attached Documents">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttachedDocName" runat="server" Text='<%# Eval("Document_Name") %>'
                                                    Visible="false" />
                                                <asp:Label ID="lblUploadedFileName" runat="server" Text='<%# Eval("Document_Name") %>'
                                                    Visible="false" />
                                                <asp:HyperLink ID="aDocPath" runat="server" Text='<%# Eval("Document_Name") %>' NavigateUrl='<%# Eval("Document_Path") %>'
                                                    Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="X" ForeColor="Red" Font-Size="15px"
                                                    CommandName="DEL" Font-Bold="true" OnClientClick="return confirm('Are you certain you want to delete this document?');" />&nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <%--CTRL_SUB_SDT18112019--%>
                        <tr id="CtrlsubValmsg" runat="server" style="display: none;">
                            <td class="leftTD" align="left" colspan="4" style="color: red;">
                                <b>Note : </b>* Kindly attach Chemical Structure & Chemical Name of Controlled Substance Material to be created.
                            </td>
                        </tr>
                        <%--CTRL_SUB_SDT18112019--%>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="BasicData"
                                    TabIndex="26" CssClass="button" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="BasicData" UseSubmitBehavior="true"
                                    Text="Save" CssClass="button" TabIndex="27" OnClick="btnSave_Click" />
                                <asp:Button ID="btnNext" runat="server" ValidationGroup="BasicData" Text="Save & Next"
                                    TabIndex="28" UseSubmitBehavior="true" CssClass="button" OnClick="btnNext_Click"
                                    Width="120px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="BasicData" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblBasicDataId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="3" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
