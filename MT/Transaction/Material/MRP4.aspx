<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="MRP4.aspx.cs" Inherits="Transaction_MRP4" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetToolTip(control) {
            ddrivetipm('11', control);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialMRP" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlAddNew" runat="server">
                <table border="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            MRP 4
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <asp:Panel ID="pnlGrid" runat="server">
                                <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                    DataKeyNames="Mat_MRP4_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
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
                                <table border="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4" align="right">
                                            <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
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
                                                ValidationGroup="MRP4" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Storage Location
                                            <asp:Label ID="lableddlStorageLocation" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlStorageLocation" runat="server" AppendDataBoundItems="false"
                                                TabIndex="2">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlStorageLocation" runat="server" ControlToValidate="ddlStorageLocation"
                                                ValidationGroup="MRP4" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Selection Method
                                            <asp:Label ID="lableddlSelectionMethod" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlSelectionMethod" runat="server" AppendDataBoundItems="false"
                                                TabIndex="3">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSelectionMethod" runat="server" ControlToValidate="ddlSelectionMethod"
                                                ValidationGroup="MRP4" ErrorMessage="Selection Method cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Selection Method cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Dependent requirements ind. for individual and coll. req
                                            <asp:Label ID="lableddlDependentReq" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlDependentReq');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlDependentReq" runat="server" AppendDataBoundItems="false"
                                                TabIndex="4">
                                                <asp:ListItem Text="Select" Value="0" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlDependentReq" runat="server" ControlToValidate="ddlDependentReq" InitialValue="0"
                                                ValidationGroup="MRP4" ErrorMessage="Dependent requirements ind. for individual and coll. req cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Dependent requirements ind. for individual and coll. req cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            MRP Relevance Dep Req
                                            <asp:Label ID="lableddlMRPRelevanceDepReq" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlMRPRelevanceDepReq');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD" colspan="3">
                                            <asp:DropDownList ID="ddlMRPRelevanceDepReq" runat="server" AppendDataBoundItems="false"
                                                TabIndex="5">
                                                <asp:ListItem Text="Select" Value="0" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlMRPRelevanceDepReq" runat="server" ControlToValidate="ddlMRPRelevanceDepReq" InitialValue="0"
                                                ValidationGroup="MRP4" ErrorMessage=" MRP Relevance Dep Req cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' MRP Relevance Dep Req cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Fair Share Rule
                                            <asp:Label ID="lableddlFairShareRule" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlFairShareRule');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlFairShareRule" runat="server" AppendDataBoundItems="false"
                                                TabIndex="6">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlFairShareRule" runat="server" ControlToValidate="ddlFairShareRule"
                                                ValidationGroup="MRP4" ErrorMessage="Fair Share Rule cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Fair Share Rule cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Indi Push Distribution
                                            <asp:Label ID="lableddlIndiPushDistribution" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlIndiPushDistribution');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD" colspan="3">
                                            <asp:DropDownList ID="ddlIndiPushDistribution" runat="server" AppendDataBoundItems="false"
                                                TabIndex="7">
                                                <asp:ListItem Text="Select" Value="0" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlIndiPushDistribution" runat="server" ControlToValidate="ddlIndiPushDistribution" InitialValue="0"
                                                ValidationGroup="MRP4" ErrorMessage="Indi Push Distribution cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Indi Push Distribution cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Storage location MRP indicator
                                            <asp:Label ID="lableddlStorageLocMrpIndi" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlStorageLocMrpIndi');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD" colspan="3">
                                            <asp:DropDownList ID="ddlStorageLocMrpIndi" runat="server" AppendDataBoundItems="false"
                                                TabIndex="8">
                                                <asp:ListItem Text="Select" Value="0" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlStorageLocMrpIndi" runat="server" ControlToValidate="ddlStorageLocMrpIndi" InitialValue="0"
                                                ValidationGroup="MRP4" ErrorMessage="Storage location MRP indicator cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage location MRP indicator cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Special procurement type at storage location level
                                            <asp:Label ID="lableddlSpecialProcTypeSloc" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                            <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlSpecialProcTypeSloc');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlSpecialProcTypeSloc" runat="server" AppendDataBoundItems="false"
                                                TabIndex="9">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSpecialProcTypeSloc" runat="server" ControlToValidate="ddlSpecialProcTypeSloc"
                                                ValidationGroup="MRP4" ErrorMessage="Special procurement type at storage location level cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Special procurement type at storage location level cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Reorder point for storage location MRP 
                                            <asp:Label ID="labletxtReorderPointSLocMrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtReorderPointSLocMrp" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="10" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtReorderPointSLocMrp" runat="server" ControlToValidate="txtReorderPointSLocMrp"
                                                ValidationGroup="MRP4" ErrorMessage="Reorder point for storage location MRP  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Reorder point for storage location MRP  cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Fixed lot size for storage location MRP 
                                            <asp:Label ID="labletxtFixedLSizeStorage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtFixedLSizeStorage" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="11" onkeypress="return IsNumber();" MaxLength="13" />
                                            <asp:RequiredFieldValidator ID="reqtxtFixedLSizeStorage" runat="server" ControlToValidate="txtFixedLSizeStorage"
                                                ValidationGroup="MRP4" ErrorMessage="Fixed lot size for storage location MRP  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Fixed lot size for storage location MRP  cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Repetitive mfg allowed
                                            <asp:Label ID="lablechkIndRepetitiveMfg" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:CheckBox ID="chkIndRepetitiveMfg" runat="server" Text="Check if Relevant" TabIndex="12" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Component scrap in percent 
                                            <asp:Label ID="labletxtComponentScrap" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtComponentScrap" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="13" onkeypress="return IsNumber();" MaxLength="5" />
                                            <asp:RequiredFieldValidator ID="reqtxtComponentScrap" runat="server" ControlToValidate="txtComponentScrap"
                                                ValidationGroup="MRP4" ErrorMessage="Component scrap in percent  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Component scrap in percent  cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Discontinuation indicator
                                            <asp:Label ID="lableddlDiscontinuationIndi" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlDiscontinuationIndi" runat="server" AppendDataBoundItems="false"
                                                TabIndex="14">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlDiscontinuationIndi" runat="server" ControlToValidate="ddlDiscontinuationIndi"
                                                ValidationGroup="MRP4" ErrorMessage="Discontinuation indicator cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Discontinuation indicator cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Effective-Out Date 
                                            <asp:Label ID="labletxtEffectiveOutDate" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtEffectiveOutDate" runat="server" CssClass="textbox" Width="100px"
                                                TabIndex="15" MaxLength="10" />
                                            <act:CalendarExtender ID="CaltxtEffectiveOutDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtEffectiveOutDate" />
                                            <asp:RequiredFieldValidator ID="reqtxtEffectiveOutDate" runat="server" ControlToValidate="txtEffectiveOutDate"
                                                ValidationGroup="MRP4" ErrorMessage="Effective-Out Date  cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Effective-Out Date  cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="revDTTest" runat="server" ControlToValidate="txtEffectiveOutDate"
                                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                                ValidationGroup="MRP4" Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                                Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Follow-Up Material
                                            <asp:Label ID="lableddlFollowUpMaterial" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlFollowUpMaterial" runat="server" AppendDataBoundItems="false"
                                                TabIndex="16">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlFollowUpMaterial" runat="server" ControlToValidate="ddlFollowUpMaterial"
                                                ValidationGroup="MRP4" ErrorMessage="Follow-Up Material cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Follow-Up Material cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Indicator for Requirements Grouping
                                            <asp:Label ID="lableddlIndiReqGrouping" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlIndiReqGrouping" runat="server" AppendDataBoundItems="false"
                                                TabIndex="17">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlIndiReqGrouping" runat="server" ControlToValidate="ddlIndiReqGrouping"
                                                ValidationGroup="MRP4" ErrorMessage="Indicator for Requirements Grouping cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Indicator for Requirements Grouping cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr id="trButton" runat="server" visible="false">
                                        <td class="centerTD" colspan="4">
                                            <asp:Button ID="btnPrevious" runat="server" ValidationGroup="MRP4" Text="Back" UseSubmitBehavior="false"
                                                TabIndex="18" CssClass="button" OnClick="btnPrevious_Click" />
                                            <asp:Button ID="btnSave" runat="server" ValidationGroup="MRP4" Text="Save" CssClass="button"
                                                UseSubmitBehavior="true" TabIndex="19" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnNext" runat="server" ValidationGroup="MRP4" Text="Save & Next"
                                                TabIndex="20" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="MRP4" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblMRPId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="11" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
