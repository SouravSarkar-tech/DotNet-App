<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Customer/CustomerMasterPage.master"
    AutoEventWireup="true" CodeFile="CustomerExtension.aspx.cs" Inherits="Transaction_CustomerExtension" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Transaction/UserControl/ucCustExtView.ascx" TagPrefix="uc" TagName="ucCustExtView" %><%--//CS_8200049196--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">
                    Customer Extension
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2" align="left">
                    <strong>Customer Type :</strong>
                    <asp:Label ID="lblCustomerType" runat="server" Font-Underline="true" />
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <asp:UpdatePanel ID="UpdCustomerExtension" runat="server">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                        <asp:Panel ID="pnlGrid" runat="server">
                                            <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                                DataKeyNames="Cust_Extension_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
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
                                                    <asp:TemplateField HeaderText="Customer Data" ItemStyle-Width="19%">
                                                        <ItemTemplate>
                                                            <strong>Customer Code&nbsp;:</strong>
                                                            <asp:Label ID="lblCustomerCode" runat="server" Text='<%# Eval("Customer_Code") %>'></asp:Label>
                                                            <br />
                                                            <strong>Customer Name&nbsp;:</strong><br />
                                                            <asp:Label ID="lblCustomerDesc" runat="server" Text='<%# Eval("Customer_Desc") %>'></asp:Label>
                                                            <br />
                                                            <%--<strong>Company Name&nbsp;:</strong>
                                                    <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Company_Name") %>'></asp:Label>
                                                    <br />--%>
                                                            <strong>Customer Acc. Grp.&nbsp;:</strong>
                                                            <asp:Label ID="lblCustomerAccGrp" runat="server" Text='<%# Eval("Customer_Acc_Grp_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="19%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Area" ItemStyle-Width="25%">
                                                        <ItemTemplate>
                                                            <strong>Sales Org.&nbsp;:</strong>
                                                            <asp:Label ID="lblSalesOrganization" runat="server" Text='<%# Eval("SalesOrganization") %>'></asp:Label>
                                                            <br />
                                                            <strong>Dist. Chnl.&nbsp;:</strong>
                                                            <asp:Label ID="lblDistributionChannel" runat="server" Text='<%# Eval("DistributionChannel") %>'></asp:Label>
                                                            <br />
                                                            <strong>Division&nbsp;:</strong>
                                                            <asp:Label ID="lblDivision" runat="server" Text='<%# Eval("Division") %>'></asp:Label>
                                                            <br />
                                                            <strong>Delivery Plant&nbsp;:</strong>
                                                            <asp:Label ID="lblDeliveryPlant" runat="server" Text='<%# Eval("Delivery_Plant_Name") %>'></asp:Label>
                                                            <br />
                                                            <strong>Price Grp.&nbsp;:</strong>
                                                            <asp:Label ID="lblPriceGrp" runat="server" Text='<%# Eval("PriceGroupName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="25%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Area Data" ItemStyle-Width="24%">
                                                        <ItemTemplate>
                                                            <strong>Sales Dist.&nbsp;:</strong>
                                                            <asp:Label ID="lblSalesDistrict" runat="server" Text='<%# Eval("SalesDistrictName") %>'></asp:Label>
                                                            <br />
                                                            <strong>Sales Off.&nbsp;:</strong>
                                                            <asp:Label ID="lblSalesOffice" runat="server" Text='<%# Eval("SalesOfficeName") %>'></asp:Label>
                                                            <br />
                                                            <strong>Sales Grp.&nbsp;:</strong>
                                                            <asp:Label ID="lblSalesGroup" runat="server" Text='<%# Eval("SalesGroupName") %>'></asp:Label>
                                                            <br />
                                                            <strong>Invoice Dt.&nbsp;:</strong>
                                                            <asp:Label ID="lblInvoiceDatesName" runat="server" Text='<%# Eval("InvoiceDatesName") %>'></asp:Label>
                                                            <br />
                                                            <strong>Invoice Dt. Schl.&nbsp;:</strong>
                                                            <asp:Label ID="lblInvoiceListScheduleName" runat="server" Text='<%# Eval("InvoiceListScheduleName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="24%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Credit Mgmt. Data" ItemStyle-Width="21%">
                                                        <ItemTemplate>
                                                            <strong>Crdt. Ctrl Area&nbsp;:</strong>
                                                            <asp:Label ID="lblCreditControlArea" runat="server" Text='<%# Eval("CreditControlAreaName") %>'></asp:Label>
                                                            <br />
                                                            <strong>Credit Limit&nbsp;:</strong>
                                                            <asp:Label ID="lblCustomercreditlimit" runat="server" Text='<%# Eval("Customer_credit_limit") %>'></asp:Label>
                                                            <br />
                                                            <strong>Risk Category&nbsp;:</strong>
                                                            <asp:Label ID="lblRiskcategory" runat="server" Text='<%# Eval("RiskCategoryName") %>'></asp:Label>
                                                            <br />
                                                            <strong>Currency&nbsp;:</strong>
                                                            <asp:Label ID="lblCurrencyId" runat="server" Text='<%# Eval("Currency_Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="21%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
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
                                            <%--<hr />
                                <hr />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                            &nbsp;&nbsp;Customer Details :
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">
                                            Customer Code
                                            <asp:Label ID="labletxtCustomerCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="textbox" MaxLength="10"
                                                AutoPostBack="true" TabIndex="1" Width="180" OnTextChanged="txtCustomerCode_TextChanged" />
                                            <asp:RequiredFieldValidator ID="reqtxtCustomerCode" runat="server" ControlToValidate="txtCustomerCode"
                                                ValidationGroup="CustExtension" ErrorMessage="Customer Code cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Code cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="regtxtCustomerCode" runat="server" ErrorMessage="Please check the SAP Customer Code."
                                                Text="<img src='../../images/Error.png' title='Please check the SAP Customer Code.' />"
                                                Display="Dynamic" ControlToValidate="txtCustomerCode" ValidationExpression="^[\S]{4,10}"
                                                ValidationGroup="CustExtension"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Customer Name
                                            <asp:Label ID="labletxtName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="textbox" Width="180" TabIndex="2" />
                                            <asp:RequiredFieldValidator ID="reqtxtName" runat="server" ControlToValidate="txtName"
                                                ValidationGroup="CustExtension" ErrorMessage="Customer Name cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Name cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" width="20%">
                                            Company Code
                                            <asp:Label ID="lableddlCompanyCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlCompanyCode" runat="server" AppendDataBoundItems="false"
                                                TabIndex="3">
                                                <asp:ListItem Text="Select" Value="0" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlCompanyCode" runat="server" ControlToValidate="ddlCompanyCode"
                                                ValidationGroup="CustExtension" ErrorMessage="Select Company Code." SetFocusOnError="true"
                                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company Code.' />" />
                                        </td>
                                        <td class="leftTD" width="20%">
                                            Customer account group
                                            <asp:Label ID="lableddlCustomerAccGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlCustomerAccGrp" runat="server" AppendDataBoundItems="false"
                                                Enabled="false" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerAccGrp_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="0" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlCustomerAccGrp" runat="server" ControlToValidate="ddlCustomerAccGrp"
                                                ValidationGroup="CustExtension" ErrorMessage="Select Customer account group."
                                                SetFocusOnError="true" InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Customer account group.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                            &nbsp;&nbsp;Sales Area :
                                        </td>
                                    </tr>
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
                                            <asp:DropDownList ID="ddlSalesOrginization" runat="server" TabIndex="5" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlSalesOrginization_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSalesOrginization" runat="server" ControlToValidate="ddlSalesOrginization"
                                                ValidationGroup="CustExtension" ErrorMessage="Sales Organization cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD">
                                            Distribution Channel
                                            <asp:Label ID="lableddlDistributionChannel" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD">
                                            <asp:DropDownList ID="ddlDistributionChannel" runat="server" TabIndex="6" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlDistributionChannel_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlDistributionChannel" runat="server" ControlToValidate="ddlDistributionChannel"
                                                ValidationGroup="CustExtension" ErrorMessage="Distribution Channel cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />
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
                                            <asp:DropDownList ID="ddlDivision" runat="server" TabIndex="7" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlDivision" runat="server" ControlToValidate="ddlDivision"
                                                ValidationGroup="CustExtension" ErrorMessage="Division cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Division cannot be blank.' />" />
                                        </td>
                                        <td class="tdSpace" colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                            &nbsp;&nbsp;Sales Area Details :
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
                                            <asp:DropDownList ID="ddlSalesDistrict" runat="server" TabIndex="8">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSalesDistrict" runat="server" ControlToValidate="ddlSalesDistrict"
                                                ValidationGroup="CustExtension" ErrorMessage="Sales district cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Title cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Sales Office
                                            <asp:Label ID="lableddlSalesOffice" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlSalesOffice" runat="server" TabIndex="9" OnSelectedIndexChanged="ddlSalesOffice_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSalesOffice" runat="server" ControlToValidate="ddlSalesOffice"
                                                ValidationGroup="CustExtension" ErrorMessage="Sales Office cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 2 cannot be blank.' />" />
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
                                            <asp:DropDownList ID="ddlSalesGroup" runat="server" TabIndex="10">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlSalesGroup" runat="server" ControlToValidate="ddlSalesGroup"
                                                ValidationGroup="CustExtension" ErrorMessage="Sales Group cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 4 cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Currency
                                            <asp:Label ID="lableddlCurrency" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <ajax:ComboBox ID="ddlCurrency" runat="server" AutoPostBack="false" TabIndex="11"
                                                DropDownStyle="DropDownList" AutoCompleteMode="Suggest" CaseSensitive="False"
                                                CssClass="AjaxToolkitStyle">
                                                <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                            </ajax:ComboBox>
                                            <asp:RequiredFieldValidator ID="reqddlCurrency" runat="server" ControlToValidate="ddlCurrency"
                                                ValidationGroup="CustExtension" ErrorMessage="Currency cannot be blank." InitialValue="---Select---"
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
                                            <asp:DropDownList ID="ddlDeliveringPlant" runat="server" TabIndex="12">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlDeliveringPlant" runat="server" ControlToValidate="ddlDeliveringPlant"
                                                ValidationGroup="CustExtension" ErrorMessage="Delivering Plant cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Delivering Plant cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Price group
                                            <asp:Label ID="lableddlPriceGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlPriceGroup" runat="server" TabIndex="13">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPriceGroup" runat="server" ControlToValidate="ddlPriceGroup"
                                                ValidationGroup="CustExtension" ErrorMessage="Price group (customer) cannot be blank."
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
                                            <ajax:ComboBox ID="ddlInvoiceDates" runat="server" AutoPostBack="false" TabIndex="14"
                                                DropDownStyle="DropDownList" AutoCompleteMode="Suggest" CaseSensitive="False"
                                                CssClass="AjaxToolkitStyle">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </ajax:ComboBox>
                                            <asp:RequiredFieldValidator ID="reqddlInvoiceDates" runat="server" ControlToValidate="ddlInvoiceDates"
                                                ValidationGroup="CustExtension" ErrorMessage="Invoice dates (calendar identification) cannot be blank."
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
                                            <ajax:ComboBox ID="ddlInvoiceListSchedule" runat="server" AutoPostBack="false" TabIndex="15"
                                                DropDownStyle="DropDownList" AutoCompleteMode="Suggest" CaseSensitive="False"
                                                CssClass="AjaxToolkitStyle">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </ajax:ComboBox>
                                            <asp:RequiredFieldValidator ID="reqddlInvoiceListSchedule" runat="server" ControlToValidate="ddlInvoiceListSchedule"
                                                ValidationGroup="CustExtension" ErrorMessage="Invoice list schedule (calendar identification) cannot be blank."
                                                InitialValue="---Select---" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Code cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                            &nbsp;&nbsp;Credit Management Data :
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
                                            <asp:DropDownList ID="ddlCreditControlArea" runat="server" TabIndex="16" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlCreditControlArea_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlCreditControlArea" runat="server" ControlToValidate="ddlCreditControlArea"
                                                ValidationGroup="CustExtension" ErrorMessage="Credit Control Area cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Credit Control Area cannot be blank.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Credit Currency
                                            <asp:Label ID="lableddlCreditCurrency" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <ajax:ComboBox ID="ddlCreditCurrency" runat="server" AutoPostBack="false" TabIndex="17"
                                                DropDownStyle="DropDownList" AutoCompleteMode="Suggest" CaseSensitive="False"
                                                CssClass="AjaxToolkitStyle">
                                                <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                            </ajax:ComboBox>
                                            <asp:RequiredFieldValidator ID="reqddlCreditCurrency" runat="server" ControlToValidate="ddlCreditCurrency"
                                                ValidationGroup="CustExtension" ErrorMessage="Credit Currency cannot be blank."
                                                InitialValue="---Select---" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Credit Currency cannot be blank.' />" />
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
                                            <asp:TextBox ID="txtCustomer_credit_limit" runat="server" CssClass="textbox" MaxLength="18"
                                                TabIndex="18" Width="180" />
                                            <asp:RequiredFieldValidator ID="reqtxtCustomer_credit_limit" runat="server" ControlToValidate="txtCustomer_credit_limit"
                                                ValidationGroup="CustExtension" ErrorMessage="Customer's credit limit cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer's credit limit cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="regtxtCustomer_credit_limit" runat="server" ControlToValidate="txtCustomer_credit_limit"
                                                ValidationExpression="\d{0,20}(\.\d{1,2})?" ValidationGroup="CustExtension" ErrorMessage="Invalid Customer's credit limit"
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Customer's credit limit.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">
                                            Risk category
                                            <asp:Label ID="lableddlRiskcategory" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:DropDownList ID="ddlRiskcategory" runat="server" TabIndex="19">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlRiskcategory" runat="server" ControlToValidate="ddlRiskcategory"
                                                ValidationGroup="CustExtension" ErrorMessage="Risk category cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Risk category cannot be blank.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">
                                            Remarks
                                        </td>
                                        <td class="rigthTD" colspan="3">
                                            <asp:TextBox ID="txtRemarks" runat="server" MaxLength="250" TextMode="MultiLine"
                                                Width="90%" TabIndex="20" Rows="3" />
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
                                                TabIndex="21" MaxLength="3" Width="100" onfocus="return txtcountryKeyExportOnFocus();"
                                                onchange="return txtcountryKeyExportTextChangeEvent();" />
                                        </td>
                                        <td class="tdSpace" colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr id="trButton" runat="server" visible="false">
                                        <td class="centerTD" colspan="4">
                                            <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="CustExtension"
                                                TabIndex="22" CssClass="button" OnClick="btnPrevious_Click" />
                                            <asp:Button ID="btnSave" runat="server" ValidationGroup="CustExtension" Text="Save"
                                                CssClass="button" TabIndex="23" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnNext" runat="server" ValidationGroup="CustExtension" Text="Save & Next"
                                                TabIndex="24" CssClass="button" OnClick="btnNext_Click" Width="120px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="CustExtension" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblCustomerExtensionId" runat="server" Visible="false" Text="0" />
    <asp:Label ID="lblSectionId" runat="server" Text="50" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
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
    
    
    
    <%--CS_8200049196 Start--%>
     <div align="left" style="width: 98%">
        <uc:ucCustExtView ID="ucCustExtView" runat="server" Visible="false"/>
    </div>
    <%--CS_8200049196 End--%>
</asp:Content>
