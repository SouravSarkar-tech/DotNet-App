<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="Purchasing.aspx.cs" Inherits="Transaction_Purchasing" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetToolTip(control) {
            ddrivetipm('12', control);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialCosting" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlData" runat="server">
                <table border="0" cellpadding="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Purchasing
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                            DataKeyNames="Mat_Purchasing_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                            GridLines="Both">
                                            <RowStyle CssClass="light-gray" />
                                            <HeaderStyle CssClass="gridHeader" />
                                            <AlternatingRowStyle CssClass="gridRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Plant" DataField="Plant" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("Mat_Purchasing_Id") %>' />
                                                        <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                    <td class="tdSpace" colspan="2" align="right">
                                        <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>
                                        <asp:HiddenField ID="hdnID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Plant
                                        <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" colspan="2">
                                        <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" TabIndex="1"
                                            OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                            ValidationGroup="Purchasing" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Purchase Order Unit of Measure
                                        <asp:Label ID="lableddlPurchaseOrder" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlPurchaseOrder');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPurchaseOrder" runat="server" AppendDataBoundItems="false"
                                            TabIndex="2">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPurchaseOrder" runat="server" ControlToValidate="ddlPurchaseOrder"
                                            ValidationGroup="Purchasing" ErrorMessage="Purchase Order Unit of Measure cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchase Order Unit of Measure cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Variable Purchase Order Unit Active
                                        <asp:Label ID="lableddlVariablePurchase" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlVariablePurchase" runat="server" AppendDataBoundItems="false"
                                            TabIndex="3">
                                            <asp:ListItem Text="Select" Value="0" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlVariablePurchase" runat="server" ControlToValidate="ddlVariablePurchase"
                                            ValidationGroup="Purchasing" ErrorMessage="Variable Purchase Order Unit Active cannot be blank."
                                            InitialValue="0" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Variable Purchase Order Unit Active cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Purchasing Group
                                        <asp:Label ID="lableddlPurchasingGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlPurchasingGroup');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPurchasingGroup" runat="server" AppendDataBoundItems="false"
                                            TabIndex="4">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPurchasingGroup" runat="server" ControlToValidate="ddlPurchasingGroup"
                                            ValidationGroup="Purchasing" ErrorMessage="Purchasing Group cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Group cannot be blank.' />" />
                                    </td>
                                    <%--GST Changes--%>
                                    <%--<td class="tdSpace" colspan="2">
                                    </td>--%>
                                    <td class="leftTD">
                                        Tax indicator for material (Purchasing)
                                        <asp:Label ID="lableddlTaxIndicatorMPurchasing" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlTaxIndicatorMPurchasing" runat="server" AppendDataBoundItems="false"
                                            TabIndex="20" Enabled = "false">
                                            <asp:ListItem Text="Select" Value="" />
                                            <asp:ListItem Text="0 - Taxable under GST" Value="0" />
                                            <asp:ListItem Text="1 - GST exempted" Value="1" />
                                            <asp:ListItem Text="2 - Nontaxable GST" Value="2" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlTaxIndicatorMPurchasing" runat="server" ControlToValidate="ddlTaxIndicatorMPurchasing"
                                            ValidationGroup="Purchasing" ErrorMessage="Tax indicator for material cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax indicator for material cannot be blank.' />" />
                                    </td>                                      
                                    <%--GST Changes--%>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Batch management requirement indicator
                                        <asp:Label ID="lablechkBatchManagReqIndicator" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkBatchManagReqIndicator" runat="server" TabIndex="5" />
                                    </td>
                                    <td class="leftTD">
                                        automatic purchase order allowed
                                        <asp:Label ID="lablechkIndicatorAutomatic" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkIndicatorAutomatic" runat="server" TabIndex="6" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Purchasing Value Key
                                        <asp:Label ID="lableddlPurchasingValueKey" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlPurchasingValueKey');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPurchasingValueKey" runat="server" AppendDataBoundItems="false"
                                            TabIndex="7">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPurchasingValueKey" runat="server" ControlToValidate="ddlPurchasingValueKey"
                                            ValidationGroup="Purchasing" ErrorMessage="Purchasing Value Key cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Value Key cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        GR Processing Time
                                        <asp:Label ID="labletxtGRProcessingTime" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtGRProcessingTime" runat="server" CssClass="textbox" MaxLength="3"
                                            onkeypress="return IsNumber();" TabIndex="8" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtGRProcessingTime" runat="server" ControlToValidate="txtGRProcessingTime"
                                            ValidationGroup="Purchasing" ErrorMessage="GR Processing Time cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='GR Processing Time cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Quota arrangement usage
                                        <asp:Label ID="lableddlQuotaArrangement" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlQuotaArrangement');"
                                            onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlQuotaArrangement" runat="server" AppendDataBoundItems="false"
                                            TabIndex="9">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlQuotaArrangement" runat="server" ControlToValidate="ddlQuotaArrangement"
                                            ValidationGroup="Purchasing" ErrorMessage="Quota arrangement usage cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Quota arrangement usage cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Source list requirement
                                        <asp:Label ID="lablechkIndicatorSource" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkIndicatorSource" runat="server" TabIndex="10" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Critical part
                                        <asp:Label ID="lablechkIndicatorCritical" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkIndicatorCritical" runat="server" TabIndex="11" />
                                    </td>
                                    <td class="leftTD">
                                        Unlimited Overdelivery Allowed
                                        <asp:Label ID="lablechkIndicatorUnlimited" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkIndicatorUnlimited" runat="server" TabIndex="12" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Material qualifies for discount in kind
                                        <asp:Label ID="lablechkMQualifiesDiscount" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkMQualifiesDiscount" runat="server" TabIndex="12" />
                                    </td>
                                    <td class="leftTD">
                                        Post to Inspection Stock
                                        <asp:Label ID="lablechkPostInspectionStock" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkPostInspectionStock" runat="server" TabIndex="13" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Material freight group
                                        <asp:Label ID="lableddlMaterialFreightG" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlMaterialFreightG" runat="server" AppendDataBoundItems="false"
                                            TabIndex="14">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlMaterialFreightG" runat="server" ControlToValidate="ddlMaterialFreightG"
                                            ValidationGroup="Purchasing" ErrorMessage="Material freight group cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material freight group cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Cross-Plant Material Status
                                        <asp:Label ID="lableddlCrossPlantMStatus" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlCrossPlantMStatus" runat="server" AppendDataBoundItems="false"
                                            TabIndex="15">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlCrossPlantMStatus" runat="server" ControlToValidate="ddlCrossPlantMStatus"
                                            ValidationGroup="Purchasing" ErrorMessage="Cross-Plant Material Status cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Cross-Plant Material Status cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        From-date of gen. material status for sales
                                        <asp:Label ID="labletxtFromDateGenMStatus" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFromDateGenMStatus" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="16" MaxLength="10" />
                                        <act:CalendarExtender ID="caltxtFromDateGenMStatus" runat="server" TargetControlID="txtFromDateGenMStatus"
                                            Format="dd/MM/yyyy" />
                                        <asp:RequiredFieldValidator ID="reqtxtFromDateGenMStatus" runat="server" ControlToValidate="txtFromDateGenMStatus"
                                            ValidationGroup="Purchasing" ErrorMessage="From-date of gen cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='From-date of gen cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="revDTTest" runat="server" ControlToValidate="txtFromDateGenMStatus"
                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                            ValidationGroup="Purchasing" Text="<img src='../../images/Error.png' title='From-date of gen cannot be blank.' />"
                                            Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td class="leftTD">
                                        From-date of material status for purchasing
                                        <asp:Label ID="labletxtFromDateMStatus" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFromDateMStatus" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="17" MaxLength="10" />
                                        <act:CalendarExtender ID="caltxtFromDateMStatus" runat="server" TargetControlID="txtFromDateMStatus"
                                            Format="dd/MM/yyyy" />
                                        <asp:RequiredFieldValidator ID="reqtxtFromDateMStatus" runat="server" ControlToValidate="txtFromDateMStatus"
                                            ValidationGroup="Purchasing" ErrorMessage="From-date of material status cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='From-date of material status cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFromDateMStatus"
                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                            ValidationGroup="Purchasing" Text="<img src='../../images/Error.png' title='From-date of gen cannot be blank.' />"
                                            Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">
                                        Plant-Specific Material Status
                                        <asp:Label ID="labletxtPlantSpecificMStatus" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtPlantSpecificMStatus" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="18" MaxLength="2" />
                                        <asp:RequiredFieldValidator ID="reqtxtPlantSpecificMStatus" runat="server" ControlToValidate="txtPlantSpecificMStatus"
                                            ValidationGroup="Purchasing" ErrorMessage="Plant-Specific Material Status cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant-Specific Material Status cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Processing time for goods receipt in days
                                        <asp:Label ID="labletxtProcessingTime" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtProcessingTime" runat="server" CssClass="textbox" Width="100px"
                                            onkeypress="return IsNumber();" TabIndex="19" MaxLength="3" />
                                        <asp:RequiredFieldValidator ID="reqtxtProcessingTime" runat="server" ControlToValidate="txtProcessingTime"
                                            ValidationGroup="Purchasing" ErrorMessage="Processing time for goods receipt in days cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Processing time for goods receipt in days cannot be blank.' />" />
                                    </td>
                                </tr>
                                <%-- GST Changes
                                <tr style="display: none">                                    
                                    <td class="leftTD">
                                        Tax indicator for material (Purchasing)
                                        <asp:Label ID="lableddlTaxIndicatorMPurchasing" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlTaxIndicatorMPurchasing" runat="server" AppendDataBoundItems="false"
                                            TabIndex="20">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlTaxIndicatorMPurchasing" runat="server" ControlToValidate="ddlTaxIndicatorMPurchasing"
                                            ValidationGroup="Purchasing" ErrorMessage="Tax indicator for material cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tax indicator for material cannot be blank.' />" />
                                    </td>                                   
                                    
                                </tr>--%>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">                                    
                                    <td class="leftTD">
                                        Tolerance limit for underdelivery
                                        <asp:Label ID="labletxtToleranceLimiteUnderdelivery" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtToleranceLimiteUnderdelivery" runat="server" CssClass="textbox"
                                            TabIndex="22" MaxLength="3" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtToleranceLimiteUnderdelivery" runat="server"
                                            ControlToValidate="txtToleranceLimiteUnderdelivery" ValidationGroup="Purchasing"
                                            ErrorMessage="Tolerance limit for underdelivery cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Tolerance limit for underdelivery cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Tolerance limit for overdelivery
                                        <asp:Label ID="labletxtToleranceLimit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtToleranceLimit" runat="server" CssClass="textbox" Width="100px"
                                            TabIndex="21" MaxLength="3" />
                                        <asp:RequiredFieldValidator ID="reqtxtToleranceLimit" runat="server" ControlToValidate="txtToleranceLimit"
                                            ValidationGroup="Purchasing" ErrorMessage="Tolerance limit for overdelivery cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Tolerance limit for overdelivery cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Manufacturer Part Profile
                                        <asp:Label ID="lableddlMPartProfile" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlMPartProfile" runat="server" AppendDataBoundItems="false"
                                            TabIndex="23">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlMPartProfile" runat="server" ControlToValidate="ddlMPartProfile"
                                            ValidationGroup="Purchasing" ErrorMessage="Manufacturer Part Profile cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Manufacturer Part Profile cannot be blank.' />" />
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
                                        Number of a Manufacturer
                                        <asp:Label ID="labletxtNManufacturer" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtNManufacturer" runat="server" CssClass="textbox" Width="70px"
                                            TabIndex="24" MaxLength="6" />
                                        <asp:RequiredFieldValidator ID="reqtxtNManufacturer" runat="server" ControlToValidate="txtNManufacturer"
                                            ValidationGroup="Purchasing" ErrorMessage="Number of a Manufacturer cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Number of a Manufacturer cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtNManufacturer" runat="server" ControlToValidate="txtNManufacturer"
                                            ValidationGroup="Purchasing" ErrorMessage="Invalid Number of a Manufacturer."
                                            ValidationExpression="^(35[0-9]{4})$" SetFocusOnError="true" Display="Dynamic"
                                            Text="<img src='../../images/Error.png' title='Invalid Number of a Manufacturer.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Name of Manufacturer
                                        <asp:Label ID="labletxtNameOfManufacturer" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtNameOfManufacturer" runat="server" CssClass="textbox" Width="180px"
                                            TabIndex="24" MaxLength="50" />
                                        <asp:RequiredFieldValidator ID="reqtxtNameOfManufacturer" runat="server" ControlToValidate="txtNameOfManufacturer"
                                            ValidationGroup="Purchasing" ErrorMessage="Name of Manufacturer cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Name of Manufacturer cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Manufacturer Part Number
                                        <asp:Label ID="labletxtMPartNumber" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMPartNumber" runat="server" CssClass="textbox" Width="200px"
                                            TabIndex="25" MaxLength="40" Style="text-transform: uppercase"/>
                                        <asp:RequiredFieldValidator ID="reqtxtMPartNumber" runat="server" ControlToValidate="txtMPartNumber"
                                            ValidationGroup="Purchasing" ErrorMessage="Manufacturer Part Number cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Manufacturer Part Number cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Purchase Order Text
                                        <asp:Label ID="labletxtPurchaseOrderText" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtPurchaseOrderText" runat="server" CssClass="textarea" TextMode="MultiLine"
                                            TabIndex="26" Columns="100" Rows="3" />
                                        <asp:RequiredFieldValidator ID="reqtxtPurchaseOrderText" runat="server" ControlToValidate="txtPurchaseOrderText"
                                            ValidationGroup="Purchasing" ErrorMessage="Purchase Order Text cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchase Order Text cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr id="trButton" runat="server" visible="false">
                                    <td class="centerTD" colspan="4">
                                        <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="Purchasing"
                                            TabIndex="27" UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="Purchasing" Text="Save"
                                            TabIndex="28" UseSubmitBehavior="true" CssClass="button" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnNext" runat="server" ValidationGroup="Purchasing" Text="Save & Next"
                                            TabIndex="29" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
  
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Purchasing" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblPurchasingId" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="12" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
</asp:Content>
