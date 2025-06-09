<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Customer/CustomerMasterPage.master"
    AutoEventWireup="true" CodeFile="General2.aspx.cs" Inherits="Transaction_Customer_General2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="4">Tax / Excise Data
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" width="100%">
                        <%--<tr>
                            <td class="tdSpace" colspan="4" align="center" style="color: Red">
                                ( Please enter 'NA' in case of not applicable for Mandatory fields. )
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="leftTD">

                                <asp:Label ID="lbltxtTaxNumber1" runat="server" Text="CST No."></asp:Label>
                                <asp:Label ID="labletxtTaxNumber1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtTaxNumber1" runat="server" CssClass="textbox" MaxLength="16"
                                    TabIndex="1" Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtTaxNumber1" runat="server" ControlToValidate="txtTaxNumber1"
                                    ValidationGroup="CustGeneral" ErrorMessage="CST No. cannot be blank.Enter 'NA',if not applicable."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='CST No. cannot be blank.Enter 'NA',if not applicable.' />" />
                            </td>
                            <td class="leftTD">

                                <asp:Label ID="lbltxtTaxNumber2" runat="server" Text="CST Date"></asp:Label>
                                <asp:Label ID="labletxtTaxNumber2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtTaxNumber2" runat="server" CssClass="textbox" MaxLength="11"
                                    TabIndex="2" Width="180px" />
                                <ajax:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtTaxNumber2"
                                    Format="dd/MM/yyyy">
                                </ajax:CalendarExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTaxNumber2"
                                    ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                    Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                    ValidationGroup="CustGeneral" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="reqtxtTaxNumber2" runat="server" ControlToValidate="txtTaxNumber2"
                                    ValidationGroup="CustGeneral" ErrorMessage="CST Date cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='CST Date cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">

                                <asp:Label ID="lbltxtTax_Numbe_3" runat="server" Text="LST/ VAT No."></asp:Label>
                                <asp:Label ID="labletxtTax_Numbe_3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtTax_Numbe_3" runat="server" CssClass="textbox" MaxLength="18"
                                    TabIndex="3" Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtTax_Numbe_3" runat="server" ControlToValidate="txtTax_Numbe_3"
                                    ValidationGroup="CustGeneral" ErrorMessage="LST/VAT No. cannot be blank.Enter 'NA',if not applicable."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='LST/VAT No. cannot be blank.Enter 'NA',if not applicable.' />" />
                            </td>
                            <td class="leftTD">

                                <asp:Label ID="lbltxtTax_Numbe_4" runat="server" Text="LST Date"></asp:Label>
                                <asp:Label ID="labletxtTax_Numbe_4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtTax_Numbe_4" runat="server" CssClass="textbox" MaxLength="18"
                                    TabIndex="4" onkeypress="return IsNumber();" Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtTax_Numbe_4" runat="server" ControlToValidate="txtTax_Numbe_4"
                                    ValidationGroup="CustGeneral" ErrorMessage="LST Date cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='LST Date be blank.' />" />
                                <ajax:CalendarExtender ID="CaltxtTax_Numbe_4" runat="server" TargetControlID="txtTax_Numbe_4"
                                    Format="dd/MM/yyyy">
                                </ajax:CalendarExtender>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtTax_Numbe_4"
                                    ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                    Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                    ValidationGroup="CustGeneral" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                            </td>
                        </tr>




                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">

                                <asp:Label ID="lblddlRegisterPAN" runat="server" Text="PAN Number exist"></asp:Label>
                                <asp:Label ID="lableddlRegisterPAN" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlRegisterPAN" runat="server" AppendDataBoundItems="false"
                                    OnSelectedIndexChanged="ddlRegisterPAN_SelectedIndexChanged" AutoPostBack="true" TabIndex="9">
                                    <%--<asp:ListItem Value="0" Text="Select" />--%>
                                    <asp:ListItem Value="0" Text="Select" />
                                    <asp:ListItem Value="1" Text="Yes" />
                                    <asp:ListItem Value="2" Text="No" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlRegisterPAN" runat="server" ControlToValidate="ddlRegisterPAN"
                                    ValidationGroup="CustGeneral" ErrorMessage="Select PAN Number exist (YES/NO)." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select PAN Number exist (YES/NO).' />" />

                            </td>
                            <td class="leftTD">
                                <asp:Label ID="lbltxtPanReason" runat="server" Text="Reasons for not providing PAN Number"></asp:Label>
                                <asp:Label ID="labletxtPanReason" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtPanReason" runat="server" CssClass="textbox" MaxLength="50"
                                    TabIndex="5" Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtPanReason" runat="server" ControlToValidate="txtPanReason"
                                    ValidationGroup="CustGeneral" ErrorMessage="Reasons for not providing PAN Number cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Reasons for not providing PAN Number cannot be blank.' />" />


                            </td>
                        </tr>



                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">PAN
                                <asp:Label ID="labletxtTax_Number_5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtTax_Number_5" runat="server" CssClass="textbox" MaxLength="10"
                                    TabIndex="5" Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtTax_Number_5" runat="server" ControlToValidate="txtTax_Number_5"
                                    ValidationGroup="CustGeneral" ErrorMessage="PAN cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='PAN cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="revtxtTax_Number_5" runat="server" ControlToValidate="txtTax_Number_5"
                                    Enabled="true" ValidationGroup="CustGeneral" Display="Dynamic" ErrorMessage="Invalid PAN"
                                    Text="<img src='../../images/Error.png' title='Invalid PAN!' />" ValidationExpression="[A-Za-z]{3}(p|P|c|C|h|H|f|F|a|A|t|T|b|B|l|L|j|J|g|G)[A-Za-z]{1}[\d]{4}[A-Za-z]{1}"></asp:RegularExpressionValidator>


                            </td>
                            <td class="leftTD" style="width: 20%">

                                <asp:Label ID="lbltxtTypeOfBusiness" runat="server" Text="Service Tax"></asp:Label>
                                <asp:Label ID="labletxtTypeOfBusiness" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtTypeOfBusiness" runat="server" CssClass="textbox" MaxLength="30"
                                    TabIndex="7" Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtTypeOfBusiness" runat="server" ControlToValidate="txtTypeOfBusiness"
                                    ValidationGroup="CustGeneral" ErrorMessage="Service Tax cannot be blank.Enter 'NA',if not applicable."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Service Tax cannot be blank.Enter 'NA',if not applicable.' />" />
                            </td>
                        </tr>

                        <%--GST Changes--%>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">

                                <asp:Label ID="lblddlRegisterUnderGST" runat="server" Text="Whether customer is Registerd Under GST"></asp:Label>
                                <asp:Label ID="lableddlRegisterUnderGST" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlRegisterUnderGST" runat="server" AppendDataBoundItems="false"
                                    OnSelectedIndexChanged="ddlRegisterUnderGST_SelectedIndexChanged" AutoPostBack="true" TabIndex="9">
                                    <asp:ListItem Value="0" Text="Select" />
                                    <asp:ListItem Value="1" Text="Yes" />
                                    <asp:ListItem Value="2" Text="No" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlRegisterUnderGST" runat="server" ControlToValidate="ddlRegisterUnderGST"
                                    ValidationGroup="CustGeneral" ErrorMessage="Select Whether customer is Registerd Under GST." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Whether customer is Registerd Under GST.' />" />

                            </td>

                            <td class="leftTD" style="width: 20%">GST No.
                                <asp:Label ID="labletxtGSTNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <%-- Start Change By Swati --%>
                                <asp:TextBox ID="txtGSTNo" runat="server" CssClass="textbox" MaxLength="15" MinLength="15" TabIndex="9"
                                    Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtGSTNo" runat="server" ControlToValidate="txtGSTNo"
                                    ValidationGroup="CustGeneral" ErrorMessage="GST No. cannot be blank.Enter 'NA',if not applicable."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='GST No. cannot be blank.Enter 'NA',if not applicable.' />" />
                                <asp:RegularExpressionValidator ID="regtxtGSTNo" runat="server" ControlToValidate="txtGSTNo"
                                    Enabled="true" ValidationGroup="CustGeneral" Display="Dynamic" ErrorMessage="Invalid GST no"
                                    Text="<img src='../../images/Error.png' title='Invalid GST no' />" ValidationExpression="[0-9 a-z A-Z]{15}"></asp:RegularExpressionValidator>
                                <%-- End Change --%>
                            </td>
                        </tr>
                        <%--GST Changes--%>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">VAT Registration Number
                                <asp:Label ID="labletxtVATRegistration" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtVATRegistration" runat="server" CssClass="textbox" MaxLength="20"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtVATRegistration" runat="server" ControlToValidate="txtVATRegistration"
                                    ValidationGroup="CustGeneral" ErrorMessage="VAT Registration Number cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='VAT Registration Number cannot be blank.' />" />
                            </td>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">Tax Number Type
                                <asp:Label ID="labletxtTax_Number_Type" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtTax_Number_Type" runat="server" CssClass="textbox" TabIndex="9"
                                    MaxLength="2" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtTax_Number_Type" runat="server" ControlToValidate="txtTax_Number_Type"
                                    ValidationGroup="CustGeneral" ErrorMessage=" Tax Number Type cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='P.O. Box Postal Code cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">Tax type
                                <asp:Label ID="labletxtTax_Type" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtTax_Type" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    TabIndex="10" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtTax_Type" runat="server" ControlToValidate="txtTax_Type"
                                    ValidationGroup="CustGeneral" ErrorMessage="Tax type cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='District cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">

                                <asp:Label ID="lbltxtECCNumber" runat="server" Text="ECC Number"></asp:Label>
                                <asp:Label ID="labletxtECCNumber" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtECCNumber" runat="server" CssClass="textbox" TabIndex="35" MaxLength="40"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtECCNumber" runat="server" ControlToValidate="txtECCNumber"
                                    ValidationGroup="CustGeneral" ErrorMessage="ECC Number cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='ECC Number cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">

                                <asp:Label ID="lbltxtExciseRegistrationNo" runat="server" Text="Excise Registration No"></asp:Label>
                                <asp:Label ID="labletxtExciseRegistrationNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtExciseRegistrationNo" runat="server" CssClass="textbox" TabIndex="36"
                                    MaxLength="40" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtExciseRegistrationNo" runat="server" ControlToValidate="txtExciseRegistrationNo"
                                    ValidationGroup="CustGeneral" ErrorMessage="Excise Registration No cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Excise Registration No cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">

                                <asp:Label ID="lbltxtExciseRange" runat="server" Text="Excise Range"></asp:Label>
                                <asp:Label ID="labletxtExciseRange" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtExciseRange" runat="server" CssClass="textbox" TabIndex="37"
                                    MaxLength="60" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtExciseRange" runat="server" ControlToValidate="txtExciseRange"
                                    ValidationGroup="CustGeneral" ErrorMessage="Excise Range cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Excise Range cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">

                                <asp:Label ID="lbltxtExciseDivision" runat="server" Text="Excise Division"></asp:Label>
                                <asp:Label ID="labletxtExciseDivision" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtExciseDivision" runat="server" CssClass="textbox" TabIndex="38"
                                    MaxLength="60" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtExciseDivision" runat="server" ControlToValidate="txtExciseDivision"
                                    ValidationGroup="CustGeneral" ErrorMessage="Excise Division cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Excise Division cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">

                                <asp:Label ID="lbltxtExciseCommissionerate" runat="server" Text="Excise Commissionerate"></asp:Label>
                                <asp:Label ID="labletxtExciseCommissionerate" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtExciseCommissionerate" runat="server" CssClass="textbox" TabIndex="39"
                                    MaxLength="60" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtExciseCommissionerate" runat="server" ControlToValidate="txtExciseCommissionerate"
                                    ValidationGroup="CustGeneral" ErrorMessage="Excise Commissionerate cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Excise Commissionerate cannot be blank.' />" />
                            </td>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">Industry key
                                <asp:Label ID="labletxtIndustrykey" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtIndustrykey" runat="server" CssClass="textboxAutocomplete" MaxLength="4"
                                    onfocus="return txtIndustrykeyOnFocus();" onchange="return txtIndustrykeyTextChangeEvent();"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtIndustrykey" runat="server" ControlToValidate="txtIndustrykey"
                                    ValidationGroup="CustGeneral" ErrorMessage="Industry key cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Industry key cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">Customer classification
                                <asp:Label ID="labletxtCustomerclassification" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCustomerclassification" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="2" onfocus="return txtCustomerclassificationOnFocus();" onchange="return txtCustomerclassificationTextChangeEvent();"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtCustomerclassification" runat="server" ControlToValidate="txtCustomerclassification"
                                    ValidationGroup="CustGeneral" ErrorMessage="Customer classification cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer classification cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">Nielsen ID
                                <asp:Label ID="labletxtNielsenID" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtNielsenID" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    onfocus="return txtNielsenIDOnFocus();" onchange="return txtNielsenIDTextChangeEvent();"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtNielsenID" runat="server" ControlToValidate="txtNielsenID"
                                    ValidationGroup="CustGeneral" ErrorMessage="Nielsen ID cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Nielsen ID cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">Account number of an alternative payer
                                <asp:Label ID="labletxtAccountNumberPayer" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtAccountNumberPayer" runat="server" CssClass="textbox" MaxLength="10"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtAccountNumberPayer" runat="server" ControlToValidate="txtAccountNumberPayer"
                                    ValidationGroup="CustGeneral" ErrorMessage="Account number of an alternative payer cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Account number of an alternative payer cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">Regional Market
                                <asp:Label ID="labletxtRegionalMarket" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtRegionalMarket" runat="server" CssClass="textbox" MaxLength="5"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtRegionalMarket" runat="server" ControlToValidate="txtRegionalMarket"
                                    ValidationGroup="CustGeneral" ErrorMessage=" Regional Market cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Regional Market cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">Legal status
                                <asp:Label ID="labletxtLegalstatus" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtLegalstatus" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    onfocus="return txtLegalstatusOnFocus();" onchange="return txtLegalstatusTextChangeEvent();"
                                    Width="30" />
                                <asp:RequiredFieldValidator ID="reqtxtLegalstatus" runat="server" ControlToValidate="txtLegalstatus"
                                    ValidationGroup="CustGeneral" ErrorMessage="Legal status cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Legal status cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">Indicator: Is an alternative payer allowed in document?
                                <asp:Label ID="lablechkPayerAllowDocs" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkPayerAllowDocs" runat="server" />
                            </td>
                            <td class="leftTD" style="width: 20%">Central Deletion Flag for Master Record
                                <asp:Label ID="lablechkCentralDeletion" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkCentralDeletion" runat="server" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">Central posting block
                                <asp:Label ID="lablechkCentralPosting" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:CheckBox ID="chkCentralPosting" runat="server" />
                            </td>
                            <td class="leftTD" style="width: 20%">Central order block for customer
                                <asp:Label ID="labletxtCentralorderblock" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCentralorderblock" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="2" onfocus="return txtCentralorderblockOnFocus();" onchange="return txtCentralorderblockTextChangeEvent();"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtCentralorderblock" runat="server" ControlToValidate="txtCentralorderblock"
                                    ValidationGroup="CustGeneral" ErrorMessage="Central order block for customer cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Central order block for customer cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">Central delivery block for the customer
                                <asp:Label ID="labletxtCentraldeliveryblock" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCentraldeliveryblock" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="2" onfocus="return txtCentraldeliveryblockOnFocus();" onchange="return txtCentraldeliveryblockTextChangeEvent();"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtCentraldeliveryblock" runat="server" ControlToValidate="txtCentraldeliveryblock"
                                    ValidationGroup="CustGeneral" ErrorMessage=" Central delivery block for the customer cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Central delivery block for the customer cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">Central billing block for customer
                                <asp:Label ID="labletxtCentralbillingblock" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCentralbillingblock" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="2" onfocus="return txtCentralbillingblockOnFocus();" onchange="return txtCentralbillingblockTextChangeEvent();"
                                    Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtCentralbillingblock" runat="server" ControlToValidate="txtCentralbillingblock"
                                    ValidationGroup="CustGeneral" ErrorMessage=" Central billing block for customer cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Central billing block for customer cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">Industry Code 1
                                <asp:Label ID="labletxtIndustryCode1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtIndustryCode1" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="10" Width="100" onfocus="return txtIndustryCode1OnFocus();" onchange="return txtIndustryCode1TextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtIndustryCode1" runat="server" ControlToValidate="txtIndustryCode1"
                                    ValidationGroup="CustGeneral" ErrorMessage="Industry Code 1 cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Industry Code 1 cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Industry Code 2
                                <asp:Label ID="labletxtIndustryCode2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtIndustryCode2" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="10" onfocus="return txtIndustryCode2OnFocus();" onchange="return txtIndustryCode2TextChangeEvent();"
                                    Width="130px" />
                                <asp:RequiredFieldValidator ID="reqtxtIndustryCode2" runat="server" ControlToValidate="txtIndustryCode2"
                                    ValidationGroup="CustGeneral" ErrorMessage="Industry Code 2 cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Industry Code 2 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">Industry Code 3
                                <asp:Label ID="labletxtIndustryCode3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtIndustryCode3" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="10" Width="100" onfocus="return txtIndustryCode3OnFocus();" onchange="return txtIndustryCode3TextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtIndustryCode3" runat="server" ControlToValidate="txtIndustryCode3"
                                    ValidationGroup="CustGeneral" ErrorMessage="Industry Code 3 cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Industry Code 3 cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Industry Code 4
                                <asp:Label ID="labletxtIndustryCode4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtIndustryCode4" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="10" onfocus="return txtIndustryCode4OnFocus();" onchange="return txtIndustryCode4TextChangeEvent();"
                                    Width="180px" />
                                <asp:RequiredFieldValidator ID="reqtxtIndustryCode4" runat="server" ControlToValidate="txtIndustryCode4"
                                    ValidationGroup="CustGeneral" ErrorMessage="Industry Code 4 cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Industry Code 4 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">Industry Code 5
                                <asp:Label ID="labletxtIndustryCode5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtIndustryCode5" runat="server" CssClass="textboxAutocomplete"
                                    MaxLength="10" Width="100" onfocus="return txtIndustryCode5OnFocus();" onchange="return txtIndustryCode5TextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtIndustryCode5" runat="server" ControlToValidate="txtIndustryCode5"
                                    ValidationGroup="CustGeneral" ErrorMessage="Industry Code 5 cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Industry Code 5 cannot be blank.' />" />
                            </td>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">Attribute 1
                                <asp:Label ID="labletxtAttribute1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtAttribute1" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    Width="100" onfocus="return txtAttribute1OnFocus();" onchange="return txtAttribute1TextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtAttribute1" runat="server" ControlToValidate="txtAttribute1"
                                    ValidationGroup="CustGeneral" ErrorMessage="Attribute 1 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Attribute 1 cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Attribute 2
                                <asp:Label ID="labletxtAttribute2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtAttribute2" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    Width="100" onfocus="return txtAttribute2OnFocus();" onchange="return txtAttribute2TextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtAttribute2" runat="server" ControlToValidate="txtAttribute2"
                                    ValidationGroup="CustGeneral" ErrorMessage="Attribute 2 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Attribute 2 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">Attribute 3
                                <asp:Label ID="labletxtAttribute3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtAttribute3" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    Width="100" onfocus="return txtAttribute3OnFocus();" onchange="return txtAttribute3TextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtAttribute3" runat="server" ControlToValidate="txtAttribute3"
                                    ValidationGroup="CustGeneral" ErrorMessage="Attribute 3 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Attribute 3 cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Attribute 4
                                <asp:Label ID="labletxtAttribute4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtAttribute4" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    Width="100" onfocus="return txtAttribute4OnFocus();" onchange="return txtAttribute4TextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtAttribute4" runat="server" ControlToValidate="txtAttribute4"
                                    ValidationGroup="CustGeneral" ErrorMessage="Attribute 4 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Attribute 4 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">Attribute 5
                                <asp:Label ID="labletxtAttribute5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtAttribute5" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    Width="100" onfocus="return txtAttribute5OnFocus();" onchange="return txtAttribute5TextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtAttribute5" runat="server" ControlToValidate="txtAttribute5"
                                    ValidationGroup="CustGeneral" ErrorMessage="Attribute 5 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Attribute 5 cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Attribute 6
                                <asp:Label ID="labletxtAttribute6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtAttribute6" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    Width="100" onfocus="return txtAttribute6OnFocus();" onchange="return txtAttribute6TextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtAttribute6" runat="server" ControlToValidate="txtAttribute6"
                                    ValidationGroup="CustGeneral" ErrorMessage="Attribute 6 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Attribute 6 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">Attribute 7
                                <asp:Label ID="labletxtAttribute7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtAttribute7" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    Width="100" onfocus="return txtAttribute7OnFocus();" onchange="return txtAttribute7TextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtAttribute7" runat="server" ControlToValidate="txtAttribute7"
                                    ValidationGroup="CustGeneral" ErrorMessage="Attribute 7 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Attribute 7 cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Attribute 8
                                <asp:Label ID="labletxtAttribute8" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtAttribute8" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    Width="100" onfocus="return txtAttribute8OnFocus();" onchange="return txtAttribute8TextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtAttribute8" runat="server" ControlToValidate="txtAttribute8"
                                    ValidationGroup="CustGeneral" ErrorMessage="Attribute 8 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Attribute 8 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD">Attribute 9
                                <asp:Label ID="labletxtAttribute9" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtAttribute9" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    Width="100" onfocus="return txtAttribute9OnFocus();" onchange="return txtAttribute9TextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtAttribute9" runat="server" ControlToValidate="txtAttribute9"
                                    ValidationGroup="CustGeneral" ErrorMessage="Attribute 9 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Attribute 9 cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Attribute 10
                                <asp:Label ID="labletxtAttribute10" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtAttribute10" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    Width="100" onfocus="return txtAttribute10OnFocus();" onchange="return txtAttribute10TextChangeEvent();" />
                                <asp:RequiredFieldValidator ID="reqtxtAttribute10" runat="server" ControlToValidate="txtAttribute10"
                                    ValidationGroup="CustGeneral" ErrorMessage="Attribute 10 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Attribute 10 cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnPrevious" runat="server" ValidationGroup="CustGeneral" Text="Back"
                                    CssClass="button" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="CustGeneral" Text="Save"
                                    CssClass="button" OnClick="btnSave_Click" />
                                <asp:Button ID="btnNext" runat="server" ValidationGroup="CustGeneral" Text="Save & Next"
                                    CssClass="button" OnClick="btnNext_Click" Width="120px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="CustGeneral" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblCustomerGeneral2Id" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="26" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Visible="false" />
    <%--GST Validation changes  , SDT27052019 Added By : NR--%>
    <asp:Label ID="lblGstNo" runat="server" Visible="false" />
    <%--GST Validation changes  , EDT27052019 Added By : NR--%>

    <script type="text/javascript">

        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {

        });

        $(function () {

        });

        function txtIndustrykeyOnFocus() {
            textboxId = $('#<%= txtIndustrykey.ClientID%>').attr('ID');
            textboxRealId = "txtIndustrykey";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndustrykeyTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndustrykey.ClientID%>').attr('ID'), "txtIndustrykey", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCustomerclassificationOnFocus() {
            textboxId = $('#<%= txtCustomerclassification.ClientID%>').attr('ID');
            textboxRealId = "txtCustomerclassification";
            AutoCompleteLookUpHeaderC();
        }

        function txtCustomerclassificationTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCustomerclassification.ClientID%>').attr('ID'), "txtCustomerclassification", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtNielsenIDOnFocus() {
            textboxId = $('#<%= txtNielsenID.ClientID%>').attr('ID');
            textboxRealId = "txtNielsenID";
            AutoCompleteLookUpHeaderC();
        }

        function txtNielsenIDTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtNielsenID.ClientID%>').attr('ID'), "txtNielsenID", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCentralorderblockOnFocus() {
            textboxId = $('#<%= txtCentralorderblock.ClientID%>').attr('ID');
            textboxRealId = "txtCentralorderblock";
            AutoCompleteLookUpHeaderC();
        }

        function txtCentralorderblockTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCentralorderblock.ClientID%>').attr('ID'), "txtCentralorderblock", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCentraldeliveryblockOnFocus() {
            textboxId = $('#<%= txtCentraldeliveryblock.ClientID%>').attr('ID');
            textboxRealId = "txtCentraldeliveryblock";
            AutoCompleteLookUpHeaderC();
        }

        function txtCentraldeliveryblockTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCentraldeliveryblock.ClientID%>').attr('ID'), "txtCentraldeliveryblock", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtCentralbillingblockOnFocus() {
            textboxId = $('#<%= txtCentralbillingblock.ClientID%>').attr('ID');
            textboxRealId = "txtCentralbillingblock";
            AutoCompleteLookUpHeaderC();
        }

        function txtCentralbillingblockTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtCentralbillingblock.ClientID%>').attr('ID'), "txtCentralbillingblock", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtLegalstatusOnFocus() {
            textboxId = $('#<%= txtLegalstatus.ClientID%>').attr('ID');
            textboxRealId = "txtLegalstatus";
            AutoCompleteLookUpHeaderC();
        }

        function txtLegalstatusTextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtLegalstatus.ClientID%>').attr('ID'), "txtLegalstatus", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtIndustryCode1OnFocus() {
            textboxId = $('#<%= txtIndustryCode1.ClientID%>').attr('ID');
            textboxRealId = "txtIndustryCode1";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndustryCode1TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndustryCode1.ClientID%>').attr('ID'), "txtIndustryCode1", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtIndustryCode2OnFocus() {
            textboxId = $('#<%= txtIndustryCode2.ClientID%>').attr('ID');
            textboxRealId = "txtIndustryCode2";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndustryCode2TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndustryCode2.ClientID%>').attr('ID'), "txtIndustryCode2", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtIndustryCode3OnFocus() {
            textboxId = $('#<%= txtIndustryCode3.ClientID%>').attr('ID');
            textboxRealId = "txtIndustryCode3";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndustryCode3TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndustryCode3.ClientID%>').attr('ID'), "txtIndustryCode3", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        function txtIndustryCode4OnFocus() {
            textboxId = $('#<%= txtIndustryCode4.ClientID%>').attr('ID');
            textboxRealId = "txtIndustryCode4";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndustryCode4TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndustryCode4.ClientID%>').attr('ID'), "txtIndustryCode4", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtIndustryCode5OnFocus() {
            textboxId = $('#<%= txtIndustryCode5.ClientID%>').attr('ID');
            textboxRealId = "txtIndustryCode5";
            AutoCompleteLookUpHeaderC();
        }

        function txtIndustryCode5TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtIndustryCode5.ClientID%>').attr('ID'), "txtIndustryCode5", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtAttribute1OnFocus() {
            textboxId = $('#<%= txtAttribute1.ClientID%>').attr('ID');
            textboxRealId = "txtAttribute1";
            AutoCompleteLookUpHeaderC();
        }

        function txtAttribute1TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtAttribute1.ClientID%>').attr('ID'), "txtAttribute1", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtAttribute2OnFocus() {
            textboxId = $('#<%= txtAttribute2.ClientID%>').attr('ID');
            textboxRealId = "txtAttribute2";
            AutoCompleteLookUpHeaderC();
        }

        function txtAttribute2TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtAttribute2.ClientID%>').attr('ID'), "txtAttribute2", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtAttribute3OnFocus() {
            textboxId = $('#<%= txtAttribute3.ClientID%>').attr('ID');
            textboxRealId = "txtAttribute3";
            AutoCompleteLookUpHeaderC();
        }

        function txtAttribute3TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtAttribute3.ClientID%>').attr('ID'), "txtAttribute3", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtAttribute4OnFocus() {
            textboxId = $('#<%= txtAttribute4.ClientID%>').attr('ID');
            textboxRealId = "txtAttribute4";
            AutoCompleteLookUpHeaderC();
        }

        function txtAttribute4TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtAttribute4.ClientID%>').attr('ID'), "txtAttribute4", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtAttribute5OnFocus() {
            textboxId = $('#<%= txtAttribute5.ClientID%>').attr('ID');
            textboxRealId = "txtAttribute5";
            AutoCompleteLookUpHeaderC();
        }

        function txtAttribute5TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtAttribute5.ClientID%>').attr('ID'), "txtAttribute5", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtAttribute6OnFocus() {
            textboxId = $('#<%= txtAttribute6.ClientID%>').attr('ID');
            textboxRealId = "txtAttribute6";
            AutoCompleteLookUpHeaderC();
        }

        function txtAttribute6TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtAttribute6.ClientID%>').attr('ID'), "txtAttribute6", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtAttribute7OnFocus() {
            textboxId = $('#<%= txtAttribute7.ClientID%>').attr('ID');
            textboxRealId = "txtAttribute7";
            AutoCompleteLookUpHeaderC();
        }

        function txtAttribute7TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtAttribute7.ClientID%>').attr('ID'), "txtAttribute7", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtAttribute8OnFocus() {
            textboxId = $('#<%= txtAttribute8.ClientID%>').attr('ID');
            textboxRealId = "txtAttribute8";
            AutoCompleteLookUpHeaderC();
        }

        function txtAttribute8TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtAttribute8.ClientID%>').attr('ID'), "txtAttribute8", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtAttribute9OnFocus() {
            textboxId = $('#<%= txtAttribute9.ClientID%>').attr('ID');
            textboxRealId = "txtAttribute9";
            AutoCompleteLookUpHeaderC();
        }

        function txtAttribute9TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtAttribute9.ClientID%>').attr('ID'), "txtAttribute9", $('#<%= btnNext.ClientID%>').attr('ID'));
        }

        function txtAttribute10OnFocus() {
            textboxId = $('#<%= txtAttribute10.ClientID%>').attr('ID');
            textboxRealId = "txtAttribute10";
            AutoCompleteLookUpHeaderC();
        }

        function txtAttribute10TextChangeEvent() {
            CheckLookupHeaderC($('#<%= txtAttribute10.ClientID%>').attr('ID'), "txtAttribute10", $('#<%= btnNext.ClientID%>').attr('ID'));
        }


        // onfocus="return txtAttribute10OnFocus();" onchange="return txtAttribute10TextChangeEvent();"
        var textboxId = "";
        var textboxRealId = "";

        //function MaterialTypeOnFocus() {
        ///        textboxId = $('#).attr('ID');
        //      textboxRealId = "txtMaterialType";
        //      AutoCompleteLookUpHeader();
        //   }

        //  function MaterialTypeTextChangeEvent() {
        //        CheckLookupHeader($(').attr('ID'), "txtMaterialType");
        //    }
    </script>
</asp:Content>
