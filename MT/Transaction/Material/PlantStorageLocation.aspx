<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="PlantStorageLocation.aspx.cs" Inherits="Transaction_PlantStorageLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialMRP" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlData" runat="server">
                <table border="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">Plant Storage Location
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                            DataKeyNames="Mat_Plant_Storage_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                            GridLines="Both">
                                            <RowStyle CssClass="light-gray" />
                                            <HeaderStyle CssClass="gridHeader" />
                                            <AlternatingRowStyle CssClass="gridRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Plant" DataField="Plant" />
                                                <%--<asp:BoundField HeaderText="Storage Location" DataField="StorageLocation" />--%>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%--<asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("Mat_Plant_Storage_Id") %>' />--%>
                                                        <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                    <td class="tdSpace" colspan="2" align="right">
                                        <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>
                                        <asp:HiddenField ID="hdnID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Plant
                                        <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                            ValidationGroup="PlantStrLoc" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">Storage Location
                                        <asp:Label ID="lableddlStorageLoc" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" colspan="2">
                                        <asp:DropDownList ID="ddlStorageLoc" runat="server" AppendDataBoundItems="false"
                                            TabIndex="2">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlStorageLoc" runat="server" ControlToValidate="ddlStorageLoc"
                                            ValidationGroup="PlantStrLoc" ErrorMessage="Storage Location cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">CC indicator is fixed
                                        <asp:Label ID="lablechkCCIndicatorFixed" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkCCIndicatorFixed" runat="server" TabIndex="3" />
                                    </td>
                                    <td class="leftTD">Negative stocks allowed in plant
                                        <asp:Label ID="lablechkNegativeStockAPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkNegativeStockAPlant" runat="server" TabIndex="4" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">Source list requirement
                                        <asp:Label ID="labletxtHazardousMNumber" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtHazardousMNumber" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="5" />
                                    </td>
                                    <td class="leftTD">Maximum storage period
                                        <asp:Label ID="labletxtMaxStoragePeriod" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMaxStoragePeriod" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="6" onkeypress="return IsNumber();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Period Indicator
                                        <asp:Label ID="lableddlPeriodIndiShelfLifeExpDate" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPeriodIndiShelfLifeExpDate" runat="server" AppendDataBoundItems="false"
                                            TabIndex="7">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPeriodIndiShelfLifeExpDate" runat="server"
                                            ControlToValidate="ddlPeriodIndiShelfLifeExpDate" ValidationGroup="PlantStorage"
                                            ErrorMessage="Cross-Plant Material Status cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Cross-Plant Material Status cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">Rounding rule for calculation of SLED
                                        <asp:Label ID="lableddlRoundingRuleCalcSled" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlRoundingRuleCalcSled" runat="server" AppendDataBoundItems="false"
                                            TabIndex="8">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlRoundingRuleCalcSled" runat="server" ControlToValidate="ddlRoundingRuleCalcSled"
                                            ValidationGroup="PlantStorage" ErrorMessage="Rounding rule for calculation of SLED cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Rounding rule for calculation of SLED cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Storage Bin
                                        <asp:Label ID="labletxtStorageBin" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtStorageBin" runat="server" CssClass="textbox" Width="100px" TabIndex="9" MaxLength="10" />
                                        <asp:RequiredFieldValidator ID="reqtxtStorageBin" runat="server" ControlToValidate="txtStorageBin"
                                            ValidationGroup="PlantStorage" ErrorMessage="Storage Bin cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Bin cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">Profit Center
                                        <asp:Label ID="lableddlProfitCenter" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlProfitCenter" runat="server" AppendDataBoundItems="false"
                                            TabIndex="9">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlProfitCenter" runat="server" ControlToValidate="ddlProfitCenter"
                                            ValidationGroup="PlantStorage" ErrorMessage="Profit Center cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Profit Center cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Label Type
                                        <asp:Label ID="lableddlLabelType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlLabelType" runat="server" AppendDataBoundItems="false" TabIndex="10">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlLabelType" runat="server" ControlToValidate="ddlLabelType"
                                            ValidationGroup="PlantStorage" ErrorMessage="Label Type cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Label Type cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">Label Form
                                        <asp:Label ID="lableddlLabelForm" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlLabelForm" runat="server" AppendDataBoundItems="false" TabIndex="11">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlLabelForm" runat="server" ControlToValidate="ddlLabelForm"
                                            ValidationGroup="PlantStorage" ErrorMessage="Label Form cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Label Form cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">Storage conditions
                                        <asp:Label ID="lableddlStorageCondition" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlStorageCondition" runat="server" AppendDataBoundItems="false"
                                            TabIndex="12">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlStorageCondition" runat="server" ControlToValidate="ddlStorageCondition"
                                            ValidationGroup="PlantStorage" ErrorMessage="Storage conditions cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage conditions cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">Storage percentage
                                        <asp:Label ID="labletxtStoragePercentage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtStoragePercentage" runat="server" CssClass="textbox" Width="100px" MaxLength="11"
                                            TabIndex="13" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtStoragePercentage" runat="server" ControlToValidate="txtStoragePercentage"
                                            ValidationGroup="PlantStorage" ErrorMessage="Storage percentage cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage percentage cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                         <asp:Label ID="lblmrsl" runat="server"  Text="Minimum Remaining Shelf Life"></asp:Label>
                                        <asp:Label ID="labletxtMinRemainingShelfLife" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMinRemainingShelfLife" runat="server" onkeypress="return IsNumber();"
                                            MaxLength="11" TabIndex="14" CssClass="textbox" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtMinRemainingShelfLife" runat="server" ControlToValidate="txtMinRemainingShelfLife"
                                            ValidationGroup="PlantStorage" ErrorMessage="Batch management requirement indicator cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Batch management requirement indicator cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                         <asp:Label ID="lbltsl" runat="server"  Text="Total shelf life"></asp:Label>
                                        <asp:Label ID="labletxtTotalShelfLifeDays" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtTotalShelfLifeDays" runat="server" CssClass="textbox" Width="100px"
                                            MaxLength="11" TabIndex="15" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtTotalShelfLifeDays" runat="server" ControlToValidate="txtTotalShelfLifeDays"
                                            ValidationGroup="PlantStorage" ErrorMessage="Total shelf life cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Total shelf life cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">Unit for maximum storage period
                                        <asp:Label ID="lableddlUnitMaxStoragePeriod" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlUnitMaxStoragePeriod" runat="server" AppendDataBoundItems="false"
                                            TabIndex="16">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlUnitMaxStoragePeriod" runat="server" ControlToValidate="ddlUnitMaxStoragePeriod"
                                            ValidationGroup="PlantStorage" ErrorMessage="Unit for maximum storage period cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Unit for maximum storage period cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">Temperature conditions indicator
                                        <asp:Label ID="labletxtTemperatureCondIndi" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtTemperatureCondIndi" runat="server" CssClass="textbox" Width="100px" MaxLength="2"
                                            TabIndex="17" />
                                        <asp:RequiredFieldValidator ID="reqtxtTemperatureCondIndi" runat="server" ControlToValidate="txtTemperatureCondIndi"
                                            ValidationGroup="PlantStorage" ErrorMessage="Temperature conditions indicator cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Temperature conditions indicator cannot be blank.' />" />
                                    </td>
                                </tr>

                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <%--added By : NR, ddlsTypeofChemical CTRL_SUB_SDT06062019--%>
                                <tr>

                                   <td class="leftTD">
                                    <asp:Label ID="lblddlIsMatCtrlSub" runat="server" Text="Is the Material Controlled Substance" ></asp:Label>
                                <asp:Label ID="lableddlIsMatCtrlSub" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD"> 
                                <asp:DropDownList ID="ddlIsMatCtrlSub" runat="server" AppendDataBoundItems="false" Enabled="false" 
                                  OnSelectedIndexChanged="ddlIsMatCtrlSub_SelectedIndexChanged" AutoPostBack="true"  >
                                    <asp:ListItem Value="0" Text="Select" />
                                    <asp:ListItem Value="1" Text="Yes" />
                                    <asp:ListItem Value="2" Text="No" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlIsMatCtrlSub" runat="server" ControlToValidate="ddlIsMatCtrlSub"
                                    ValidationGroup="PlantStorage" ErrorMessage="Is the Material Controlled Substancel cannot be blank." SetFocusOnError="true"
                                 Visible="false" InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Is the Material Controlled Substance cannot be blank.' />" />
                            </td>

                                    <td class="leftTD">
                                   
                                    <asp:Label ID="lblddlsTypeofChemical" runat="server" Text="Controlled Substance Classification" ></asp:Label>
                                        <asp:Label ID="labelddlsTypeofChemical" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlsTypeofChemical" runat="server" AppendDataBoundItems="false" TabIndex="10"
                                            Enabled="false">
                                            <asp:ListItem Value="" Text="Select"/> 
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlsTypeofChemical" runat="server" ControlToValidate="ddlsTypeofChemical"
                                            ValidationGroup="PlantStorage" ErrorMessage="Controlled Substance Classification cannot be blank." SetFocusOnError="true"
                                          Visible="false"  Display="Dynamic" Text="<img src='../../images/Error.png' title='Controlled Substance Classification cannot be blank.' />" />
                                    </td>
                                    
                                </tr>
                                <%--added By : NR, ddlsTypeofChemical CTRL_SUB_EDT06062019--%>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr id="trButton" runat="server" visible="false">
                                    <td class="centerTD" colspan="4">
                                        <asp:Button ID="btnPrevious" runat="server" TabIndex="18" Text="Back" UseSubmitBehavior="false"
                                            ValidationGroup="PlantStorage" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="PlantStorage" Text="Save"
                                            CssClass="button" TabIndex="19" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnNext" runat="server" ValidationGroup="PlantStorage" Text="Save & Next"
                                            TabIndex="20" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="PlantStorage" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblPlantStorageLocId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="13" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
