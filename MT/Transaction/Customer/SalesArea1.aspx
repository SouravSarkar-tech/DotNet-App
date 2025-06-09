<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Customer/CustomerMasterPage.master"
    AutoEventWireup="true" CodeFile="SalesArea1.aspx.cs" Inherits="Transaction_Customer_SalesArea1" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Transaction/UserControl/ucCustSalseView.ascx" TagPrefix="uc" TagName="ucCustSalseView" %><%--//CS_8200049196--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdSalesData" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlData" runat="server">
                <table border="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Sales Area & Credit Mgmt. Data
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
                                    <td class="tdSpace" colspan="4">
                                        <asp:Panel ID="pnlGrid" runat="server">
                                            <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                                DataKeyNames="Cust_SalesArea1" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                                GridLines="Both">
                                                <RowStyle CssClass="light-gray" />
                                                <HeaderStyle CssClass="gridHeader" />
                                                <AlternatingRowStyle CssClass="gridRowStyle" />
                                                <PagerStyle CssClass="PagerStyle" />
                                                <Columns>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSalesOrgId" runat="server" Text='<%# Eval("Sales_Organization_Id") %>'></asp:Label>
                                                            <asp:Label ID="lblDistributionChnlId" runat="server" Text='<%# Eval("Distribution_Channel_Id") %>'></asp:Label>
                                                            <asp:Label ID="lblDivisionId" runat="server" Text='<%# Eval("Division_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Area" ItemStyle-Width="28%">
                                                        <ItemTemplate>
                                                            <strong>Dist. Chnl.&nbsp;:</strong>
                                                            <asp:Label ID="lblDistributionChannel" runat="server" Text='<%# Eval("DistributionChannel") %>'></asp:Label>
                                                            <br />
                                                            <strong>Sales Org.&nbsp;:</strong>
                                                            <asp:Label ID="lblSalesOrganization" runat="server" Text='<%# Eval("SalesOrganization") %>'></asp:Label>
                                                            <br />
                                                            <strong>Division&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</strong>
                                                            <asp:Label ID="lblDivision" runat="server" Text='<%# Eval("Division") %>'></asp:Label>
                                                            <br />
                                                            <strong>Delivery Plant&nbsp;:</strong>
                                                            <asp:Label ID="lblDeliveryPlant" runat="server" Text='<%# Eval("Delivery_Plant_Name") %>'></asp:Label>
                                                            <br />
                                                            <strong>Price Grp.&nbsp;:</strong>
                                                            <asp:Label ID="lblPriceGrp" runat="server" Text='<%# Eval("PriceGroupName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="28%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Area Data" ItemStyle-Width="33%">
                                                        <ItemTemplate>
                                                            <strong>Sales Dist.&nbsp;:</strong>
                                                            <asp:Label ID="lblSalesDistrict" runat="server" Text='<%# Eval("SalesDistrictName") %>'></asp:Label>
                                                            <br />
                                                            <strong>Sales Off.&nbsp;&nbsp;:</strong>
                                                            <asp:Label ID="lblSalesOffice" runat="server" Text='<%# Eval("SalesOfficeName") %>'></asp:Label>
                                                            <br />
                                                            <strong>Sales Grp.&nbsp;:</strong>
                                                            <asp:Label ID="lblSalesGroup" runat="server" Text='<%# Eval("SalesGroupName") %>'></asp:Label>
                                                            <br />
                                                            <strong>Sales Currency.&nbsp;:</strong>
                                                            <asp:Label ID="lblSalesCurrency" runat="server" Text='<%# Eval("Currency") %>'></asp:Label>
                                                            <br />
                                                            <strong>Invoice Dt.&nbsp;:</strong>
                                                            <asp:Label ID="lblInvoiceDatesName" runat="server" Text='<%# Eval("InvoiceDatesName") %>'></asp:Label>
                                                            <br />
                                                            <strong>Invoice Dt. Schl.&nbsp;:</strong>
                                                            <asp:Label ID="lblInvoiceListScheduleName" runat="server" Text='<%# Eval("InvoiceListScheduleName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="33%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Credit Mgmt. Data" ItemStyle-Width="35%">
                                                        <ItemTemplate>
                                                            <strong>Crdt. Ctrl Area :</strong>
                                                            <asp:Label ID="lblCreditControlArea" runat="server" Text='<%# Eval("CreditControlAreaName") %>'></asp:Label>
                                                            <br />
                                                            <strong>Credit Limit&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</strong>
                                                            <asp:Label ID="lblCustomercreditlimit" runat="server" Text='<%# Eval("Customer_credit_limit") %>'></asp:Label>
                                                            <br />
                                                            <strong>Risk Category&nbsp;&nbsp;:</strong>
                                                            <asp:Label ID="lblRiskcategory" runat="server" Text='<%# Eval("RiskCategoryName") %>'></asp:Label>
                                                            <br />
                                                            <strong>Currency&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</strong>
                                                            <asp:Label ID="lblCurrencyId" runat="server" Text='<%# Eval("Currency_Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="35%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%--<asp:LinkButton ID="lnkCopy" runat="server" Text="Copy" OnClick="lnkCopy_Click" />--%>
                                                            <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" />
                                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="lnkDelete_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4" align="right" style="border-bottom: 1px solid Black">
                                        <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlAddData" runat="server">
                                <table border="0" width="100%">
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">
                                            Sales Organization
                                            <asp:Label ID="lableddlSalesOrginization" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <%--<cc1:DropDownCheckBoxes ID="ddlSalesOrginization" runat="server" AddJQueryReference="false"
                                    UseButtons="false" UseSelectAllNode="true" AutoPostBack="false">
                                    <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="250" DropDownBoxBoxHeight="80" />
                                    <Texts SelectBoxCaption="--Select--" />
                                </cc1:DropDownCheckBoxes>--%>
                                            <asp:DropDownList ID="ddlSalesOrginization" runat="server" TabIndex="1" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlSalesOrginization_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="reqddlSalesOrginization" runat="server" ControlToValidate="ddlSalesOrginization"
                                                ValidationGroup="salesarea" ErrorMessage="Sales Organization cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />--%>
                                        </td>
                                        <td class="leftTD">
                                            Distribution Channel
                                            <asp:Label ID="lableddlDistributionChannel" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <%--<cc1:DropDownCheckBoxes ID="ddlDistributionChannel" runat="server" AddJQueryReference="false"
                                    UseButtons="false" UseSelectAllNode="true" AutoPostBack="false">
                                    <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="250" DropDownBoxBoxHeight="80" />
                                    <Texts SelectBoxCaption="--Select--" />
                                </cc1:DropDownCheckBoxes>--%>
                                            <asp:DropDownList ID="ddlDistributionChannel" runat="server" TabIndex="2" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlDistributionChannel_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="reqddlDistributionChannel" runat="server" ControlToValidate="ddlDistributionChannel"
                                                ValidationGroup="salesarea" ErrorMessage="Distribution Channel cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">
                                            Division
                                            <asp:Label ID="lableddlDivision" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlDivision" runat="server" TabIndex="3" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="reqddlDivision" runat="server" ControlToValidate="ddlDivision"
                                                ValidationGroup="salesarea" ErrorMessage="Division cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Division cannot be blank.' />" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Sales district
                                            <asp:Label ID="lableddlSalesDistrict" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlSalesDistrict" runat="server" TabIndex="4">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSalesDistrict" runat="server" ControlToValidate="ddlSalesDistrict"
                                                ValidationGroup="salesarea" ErrorMessage="Sales district cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Title cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Sales Office
                                            <asp:Label ID="lableddlSalesOffice" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlSalesOffice" runat="server" TabIndex="5" OnSelectedIndexChanged="ddlSalesOffice_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSalesOffice" runat="server" ControlToValidate="ddlSalesOffice"
                                                ValidationGroup="salesarea" ErrorMessage="Sales Office cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 2 cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Sales Group
                                            <asp:Label ID="lableddlSalesGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlSalesGroup" runat="server" TabIndex="6">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSalesGroup" runat="server" ControlToValidate="ddlSalesGroup"
                                                ValidationGroup="salesarea" ErrorMessage="Sales Group cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 4 cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Currency
                                            <asp:Label ID="lableddlCurrency" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <ajax:ComboBox ID="ddlCurrency" runat="server" AutoPostBack="false" TabIndex="7"
                                                DropDownStyle="DropDownList" AutoCompleteMode="Suggest" CaseSensitive="False"
                                                CssClass="AjaxToolkitStyle">
                                                <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                            </ajax:ComboBox>
                                            <asp:RequiredFieldValidator ID="reqddlCurrency" runat="server" ControlToValidate="ddlCurrency"
                                                ValidationGroup="salesarea" ErrorMessage="Currency cannot be blank." InitialValue="---Select---"
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Currency cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">
                                            Delivering Plant
                                            <asp:Label ID="lableddlDeliveringPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlDeliveringPlant" runat="server" TabIndex="8">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlDeliveringPlant" runat="server" ControlToValidate="ddlDeliveringPlant"
                                                ValidationGroup="salesarea" ErrorMessage="Delivering Plant cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Delivering Plant cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Price group
                                            <asp:Label ID="lableddlPriceGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlPriceGroup" runat="server" TabIndex="9">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPriceGroup" runat="server" ControlToValidate="ddlPriceGroup"
                                                ValidationGroup="salesarea" ErrorMessage="Price group (customer) cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='City cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">
                                            Invoice dates
                                            <asp:Label ID="lableddlInvoiceDates" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <%--<asp:DropDownList ID="ddlInvoiceDates" runat="server" TabIndex="3">
                                    <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                </asp:DropDownList>--%>
                                            <ajax:ComboBox ID="ddlInvoiceDates" runat="server" AutoPostBack="false" TabIndex="10"
                                                DropDownStyle="DropDownList" AutoCompleteMode="Suggest" CaseSensitive="False"
                                                CssClass="AjaxToolkitStyle">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </ajax:ComboBox>
                                            <asp:RequiredFieldValidator ID="reqddlInvoiceDates" runat="server" ControlToValidate="ddlInvoiceDates"
                                                ValidationGroup="salesarea" ErrorMessage="Invoice dates (calendar identification) cannot be blank."
                                                InitialValue="---Select---" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invoice dates (calendar identification) cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Invoice list schedule
                                            <asp:Label ID="lableddlInvoiceListSchedule" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <%--<asp:DropDownList ID="ddlInvoiceListSchedule" runat="server" TabIndex="3">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>--%>
                                            <ajax:ComboBox ID="ddlInvoiceListSchedule" runat="server" AutoPostBack="false" TabIndex="11"
                                                DropDownStyle="DropDownList" AutoCompleteMode="Suggest" CaseSensitive="False"
                                                CssClass="AjaxToolkitStyle">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </ajax:ComboBox>
                                            <asp:RequiredFieldValidator ID="reqddlInvoiceListSchedule" runat="server" ControlToValidate="ddlInvoiceListSchedule"
                                                ValidationGroup="salesarea" ErrorMessage="Invoice list schedule (calendar identification) cannot be blank."
                                                InitialValue="---Select---" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Code cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Credit Control Area
                                            <asp:Label ID="lableddlCreditControlArea" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlCreditControlArea" runat="server" TabIndex="12" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlCreditControlArea_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlCreditControlArea" runat="server" ControlToValidate="ddlCreditControlArea"
                                                ValidationGroup="salesarea" ErrorMessage="Credit Control Area cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Credit Control Area cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Credit Currency
                                            <asp:Label ID="lableddlCreditCurrency" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <ajax:ComboBox ID="ddlCreditCurrency" runat="server" AutoPostBack="false" TabIndex="13"
                                                DropDownStyle="DropDownList" AutoCompleteMode="Suggest" CaseSensitive="False"
                                                CssClass="AjaxToolkitStyle">
                                                <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                            </ajax:ComboBox>
                                            <asp:RequiredFieldValidator ID="reqddlCreditCurrency" runat="server" ControlToValidate="ddlCreditCurrency"
                                                ValidationGroup="salesarea" ErrorMessage="Credit Currency cannot be blank." InitialValue="---Select---"
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Credit Currency cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Customer's credit limit
                                            <asp:Label ID="labletxtCustomer_credit_limit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtCustomer_credit_limit" runat="server" CssClass="textbox" MaxLength="20"
                                                TabIndex="14" Width="180" />
                                            <asp:RequiredFieldValidator ID="reqtxtCustomer_credit_limit" runat="server" ControlToValidate="txtCustomer_credit_limit"
                                                ValidationGroup="salesarea" ErrorMessage="Customer's credit limit cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer's credit limit cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="regtxtCustomer_credit_limit" runat="server" ControlToValidate="txtCustomer_credit_limit"
                                                ValidationExpression="\d{0,20}(\.\d{1,2})?" ValidationGroup="salesarea" ErrorMessage="Invalid Customer's credit limit"
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Customer's credit limit.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Risk category
                                            <asp:Label ID="lableddlRiskcategory" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlRiskcategory" runat="server" TabIndex="15">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlRiskcategory" runat="server" ControlToValidate="ddlRiskcategory"
                                                ValidationGroup="salesarea" ErrorMessage="Risk category cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Risk category cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="leftTD" style="width: 20%">
                                            Country key for export control in customer master
                                            <asp:Label ID="labletxtcountryKeyExport" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtcountryKeyExport" runat="server" CssClass="textboxAutocomplete"
                                                TabIndex="16" MaxLength="3" Width="100" onfocus="return txtcountryKeyExportOnFocus();"
                                                onchange="return txtcountryKeyExportTextChangeEvent();" />
                                            <asp:RequiredFieldValidator ID="reqtxtcountryKeyExport" runat="server" ControlToValidate="txtcountryKeyExport"
                                                ValidationGroup="salesarea" ErrorMessage="Country key for export control in customer master cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Code cannot be blank.' />" />
                                        </td>
                                        <td class="tdSpace" colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr id="trButton" runat="server" visible="false">
                                        <td class="centerTD" colspan="4">
                                            <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="salesarea"
                                                TabIndex="17" CssClass="button" OnClick="btnPrevious_Click" />
                                            <asp:Button ID="btnSave" runat="server" ValidationGroup="salesarea" Text="Save" CssClass="button"
                                                TabIndex="18" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnNext" runat="server" ValidationGroup="salesarea" Text="Save & Next"
                                                TabIndex="19" CssClass="button" OnClick="btnNext_Click" Width="120px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="salesarea" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblCustomerGeneralId" runat="server" Visible="false" Text="0" />
            <asp:Label ID="lblSectionId" runat="server" Text="24" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">

        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {

        });

        $(function () {

        });


        var textboxId = "";
        var textboxRealId = "";

        function txtcountryKeyExportOnFocus() {
            textboxId = $('#<%= txtcountryKeyExport.ClientID%>').attr('ID');
            textboxRealId = "txtcountryKeyExport";
            AutoCompleteLookUpHeaderC();
        }

        function txtcountryKeyExportTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtcountryKeyExport.ClientID%>').attr('ID'), "txtcountryKeyExport", $('#<%= btnNext.ClientID%>').attr('ID'));
        }



        var textboxId = "";
        var textboxRealId = "";
        function IsNumber() {
            if ((event.keyCode < 48) || (event.keyCode > 57))
                return false;
        }

    </script>

    <%--CS_8200049196 Start  Visible="true"--%>
     <div align="left" style="width: 98%">
        <uc:ucCustSalseView ID="ucCustSalseView" runat="server"/>
    </div>
    <%--CS_8200049196 End--%>
</asp:Content>
