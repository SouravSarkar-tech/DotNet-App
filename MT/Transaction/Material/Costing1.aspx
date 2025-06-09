<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="Costing1.aspx.cs" Inherits="Transaction_Costing1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialCosting" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlAddNew" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Costing
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <asp:Panel ID="pnlGrid" runat="server">
                                <asp:GridView ID="grvCosting1" runat="server" AutoGenerateColumns="false" Width="100%"
                                    DataKeyNames="Mat_Costing1_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                    GridLines="Both">
                                    <RowStyle CssClass="light-gray" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <AlternatingRowStyle CssClass="gridRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Plants" DataField="Plant" />
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
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Plant
                                            <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                TabIndex="1" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                                ValidationGroup="Costing" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
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
                                            Do Not Cost
                                            <asp:Label ID="lablechkDoNotCost" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:CheckBox ID="chkDoNotCost" runat="server" Text=" Do Not Cost" TabIndex="2" />
                                        </td>
                                        <td class="leftTD">
                                            Material Is Costed with Quantity Structure
                                            <asp:Label ID="lablechkMaterialCosted" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:CheckBox ID="chkMaterialCosted" runat="server" Text=" Is Costed with Quantity Structure"
                                                TabIndex="3" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Origin Group as Subdivision of Cost Element
                                            <asp:Label ID="lableddlOriginGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlOriginGroup" runat="server" AppendDataBoundItems="false"
                                                TabIndex="4">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlOriginGroup" runat="server" ControlToValidate="ddlOriginGroup"
                                                ValidationGroup="Costing" ErrorMessage="Origin Group as Subdivision of Cost Element cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Origin Group as Subdivision of Cost Element cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">
                                            Material Related Origin
                                            <asp:Label ID="lablechkMaterialRelatedOrigin" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:CheckBox ID="chkMaterialRelatedOrigin" runat="server" Text=" Is Material Related Origin"
                                                TabIndex="5" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">
                                            Costing Overhead Group
                                            <asp:Label ID="lableddlCosting" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlCosting" runat="server" AppendDataBoundItems="false" TabIndex="6">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlCosting" runat="server" ControlToValidate="ddlCosting"
                                                ValidationGroup="Costing" ErrorMessage="Costing Overhead Group cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Costing Overhead Group cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">
                                            Variance Key
                                            <asp:Label ID="lableddlVarianceKey" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlVarianceKey" runat="server" AppendDataBoundItems="false"
                                                TabIndex="7">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlVarianceKey" runat="server" ControlToValidate="ddlVarianceKey"
                                                ValidationGroup="Costing" ErrorMessage="Variance Key cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Variance Key cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">
                                            Alternative BOM
                                            <asp:Label ID="labletxtBom" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtBom" runat="server" CssClass="textbox" MaxLength="2" Width="100px"
                                                onkeypress="return IsNumber();" TabIndex="8" />
                                            <asp:RequiredFieldValidator ID="reqtxtBom" runat="server" ControlToValidate="txtBom"
                                                ValidationGroup="Costing" ErrorMessage="Alternative BOM cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Alternative BOM cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">
                                            BOM Usage
                                            <asp:Label ID="lableddlMBomUsage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlMBomUsage" runat="server" AppendDataBoundItems="false" TabIndex="9">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMBomUsage" runat="server" ControlToValidate="ddlMBomUsage"
                                                ValidationGroup="Costing" ErrorMessage="BOM Usage cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='BOM Usage cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                        <td class="leftTD">
                                            Key for Task List Group
                                            <asp:Label ID="labletxtKeyTaskListGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                        <asp:TextBox ID="txtKeyTaskListGroup" runat="server" CssClass="textbox" Width="100px" MaxLength="8"
                                                 TabIndex="10" />
                                            <%--<asp:DropDownList ID="ddlKeyTaskListGroup" runat="server" AppendDataBoundItems="false"
                                                TabIndex="10">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>--%>
                                            <asp:RequiredFieldValidator ID="reqtxtKeyTaskListGroup" runat="server" ControlToValidate="txtKeyTaskListGroup"
                                                ValidationGroup="Costing" ErrorMessage="Key for Task List Group cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Key for Task List Group cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">
                                            Group Counter
                                            <asp:Label ID="labletxtGroupCenter" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtGroupCenter" runat="server" CssClass="textbox" Width="30px" MaxLength="2"
                                                onkeypress="return IsNumber();" TabIndex="11" />
                                            <asp:RequiredFieldValidator ID="reqtxtGroupCenter" runat="server" ControlToValidate="txtGroupCenter"
                                                ValidationGroup="Costing" ErrorMessage="Group Counter cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Group Counter cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                    <td class="leftTD">
                                            Task List Type
                                            <asp:Label ID="lableddlTaskListType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlTaskListType" runat="server" AppendDataBoundItems="false"
                                                TabIndex="12">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlTaskListType" runat="server" ControlToValidate="ddlTaskListType"
                                                ValidationGroup="Costing" ErrorMessage="Task List Type cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Task List Type cannot be blank.' />" />
                                        </td>
                                        <td class="tdSpace" colspan="2">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Profit Center
                                            <asp:Label ID="lableddlProfitCenter" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlProfitCenter" runat="server" AppendDataBoundItems="false"
                                                TabIndex="13">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlProfitCenter" runat="server" ControlToValidate="ddlProfitCenter"
                                                ValidationGroup="Costing" ErrorMessage="Profit Center cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Profit Center cannot be blank.' />" />
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
                                            Special Procurement Type for Costing
                                            <asp:Label ID="lableddlSpecialProcurement" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlSpecialProcurement" runat="server" AppendDataBoundItems="false"
                                                TabIndex="14">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSpecialProcurement" runat="server" ControlToValidate="ddlSpecialProcurement"
                                                ValidationGroup="Costing" ErrorMessage="Special Procurement Type for Costing cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Special Procurement Type for Costing cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">
                                            Lot Size of Product Cost Estimate (BTCI)
                                            <asp:Label ID="labletxtBTCI" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtBTCI" runat="server" CssClass="textbox" Width="100px" MaxLength="13"
                                                TabIndex="15" onkeypress="return IsNumber();" />
                                            <asp:RequiredFieldValidator ID="reqtxtBTCI" runat="server" ControlToValidate="txtBTCI"
                                                ValidationGroup="Costing" ErrorMessage="Lot Size of Product Cost Estimate (BTCI) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Lot Size of Product Cost Estimate (BTCI) cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr id="trButton" runat="server" visible="false">
                                        <td class="centerTD" colspan="4">
                                            <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="Costing"
                                                TabIndex="16" UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                            <asp:Button ID="btnSave" runat="server" ValidationGroup="Costing" Text="Save" CssClass="button"
                                                TabIndex="17" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnNext" runat="server" ValidationGroup="Costing" Text="Save & Next"
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
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Costing" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblCostingId" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="5" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
</asp:Content>
