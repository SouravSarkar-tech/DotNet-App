<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Vendor/VendorMasterPage.master"
    AutoEventWireup="true" CodeFile="VendorChange.aspx.cs" Inherits="Transaction_Vendor_VendorChange" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Src="~/Transaction/UserControl/ucBankMaster.ascx" TagPrefix="uc" TagName="ucBankMaster" %>
<%-- Added by Swati for Vendor Excel Upload on 12.12.2018 --%>
<%@ Register Src="~/Transaction/UserControl/ucExcelDownload2.ascx" TagPrefix="uc" TagName="ExcelDownload" %>
<%@ Register Src="~/Transaction/UserControl/VendorChangeExcelUpload.ascx" TagPrefix="uc" TagName="VendorChangeExcelUpload" %>

<%-- End Change --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/jquery.MultiFile.js" type="text/javascript"></script>
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
    <script type="text/javascript">
        function showModalPopupViaClient() {
            //ev.preventDefault();
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <%-- Added by Swati on 15.03.2019 for Ariba Migration Downtime Notification --%>
            <tr>
                <td colspan="4">
                    <asp:Panel ID="pnlWarning" runat="server" Visible="false">
                        <asp:Label ID="lblWarning" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
            <%-- End --%>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                        <asp:Label ID="lblMsg" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="trHeading" align="center" colspan="2">Change Data
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <asp:UpdatePanel ID="UpdChange" runat="server">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                                <asp:UpdatePanel ID="updpnlAddData" runat="server">
                                    <ContentTemplate>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                                                <act:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="hiddenTargetControlForModalPopup"
                                                    BehaviorID="programmaticModalPopupBehavior" CancelControlID="btnCancel" PopupControlID="pnlAddData"
                                                    BackgroundCssClass="modalBackground" DropShadow="true" PopupDragHandleControlID="pnlTitle" />
                                                <asp:Panel ID="pnlAddData" runat="server" Width="100%" DefaultButton="btnAdd">

                                                    <div style="background-color: White; padding: 2px 2px 2px 2px;">
                                                        <asp:Panel ID="pnlTitle" runat="server" Style="cursor: move; background-color: Black; border: solid 1px Gray; color: Black">
                                                            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                                                                <asp:Button ID="btnclose" runat="server" Text="X" OnClick="btnclose_Click" align="right" />
                                                                <span class="ui-dialog-title">Change :: Add Details</span>
                                                                <%--<a class="ui-dialog-titlebar-close ui-corner-all" href="#" align="right">
                                                        <asp:Label ID="lblClose" align="right" runat="server" CssClass="ui-icon ui-icon-closethick">Close</asp:Label></a>--%>
                                                            </div>
                                                        </asp:Panel>

                                                        <div style="display: block" id="divmainPopUp" runat="server" clientidmode="Static">
                                                            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                                                                <tr>
                                                                    <td class="tdSpace" colspan="4">
                                                                        <asp:Panel ID="pnlMsg1" runat="server" Visible="false">
                                                                            <asp:Label ID="lblMsg1" runat="server" />
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdSpace" colspan="4"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="leftTD" width="20%">Vendor Code
                                                                    <asp:Label ID="labletxtVendorCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:TextBox ID="txtVendorCode" runat="server" CssClass="textbox" MaxLength="10"
                                                                            AutoPostBack="true" TabIndex="1" Width="180" OnTextChanged="txtVendorCode_TextChanged" onkeydown="return (event.keyCode!=13);" />
                                                                        <asp:RequiredFieldValidator ID="reqtxtVendorCode" runat="server" ControlToValidate="txtVendorCode"
                                                                            ValidationGroup="save" ErrorMessage="Vendor Code cannot be blank." SetFocusOnError="true"
                                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Code cannot be blank.' />" />
                                                                        <asp:RegularExpressionValidator ID="regtxtVendorCode" runat="server" ControlToValidate="txtVendorCode"
                                                                            ValidationGroup="save" ErrorMessage="Vendor Code Invalid." SetFocusOnError="true"
                                                                            ValidationExpression="^[\S]{4,10}" Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Code Invalid.' />" />
                                                                    </td>
                                                                    <td class="leftTD" style="width: 20%">Vendor Name
                                                                    <asp:Label ID="labletxtVendorName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                        <br />
                                                                        <span style="color: Red; font-size: x-small; font-weight: normal">(As mentioned in SAP)</span>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:TextBox ID="txtVendorName" runat="server" CssClass="textbox" MaxLength="70"
                                                                            Width="210" TabIndex="2" />
                                                                        <asp:RequiredFieldValidator ID="reqtxtVendorName" runat="server" ControlToValidate="txtVendorName"
                                                                            ValidationGroup="save" ErrorMessage="Vendor Name cannot be blank." SetFocusOnError="true"
                                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Name cannot be blank.' />" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdSpace" colspan="4"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="leftTD" width="20%">Company Code
                                                                    <asp:Label ID="lableddlCompanyCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:DropDownList ID="ddlCompanyCode" runat="server" AppendDataBoundItems="false"
                                                                            Enabled="true" TabIndex="3">
                                                                            <asp:ListItem Text="Select" Value="0" />
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="reqddlCompanyCode" runat="server" ControlToValidate="ddlCompanyCode"
                                                                            ValidationGroup="save" ErrorMessage="Select Company Code." SetFocusOnError="true"
                                                                            InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company Code.' />" />
                                                                    </td>
                                                                    <td class="leftTD" width="20%">Vendor account group
                                                                    <asp:Label ID="lableddlVendorAccGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:DropDownList ID="ddlVendorAccGrp" runat="server" AppendDataBoundItems="false"
                                                                            Enabled="false" AutoPostBack="true" TabIndex="3" OnSelectedIndexChanged="ddlVendorAccGrp_SelectedIndexChanged">
                                                                            <asp:ListItem Text="Select" Value="0" />
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="reqddlVendorAccGrp" runat="server" ControlToValidate="ddlVendorAccGrp"
                                                                            ValidationGroup="save" ErrorMessage="Select  Vendor account group." SetFocusOnError="true"
                                                                            InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select  Vendor account group.' />" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdSpace" colspan="4"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="leftTD" width="20%">Purchasing Organization
                                                                    <%--<asp:Label ID="lableddlPurchaseOrg" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:DropDownList ID="ddlPurchaseOrg" runat="server" AppendDataBoundItems="false"
                                                                            TabIndex="4">
                                                                            <asp:ListItem Text="Select" Value="0" />
                                                                        </asp:DropDownList>
                                                                        <%--<asp:RequiredFieldValidator ID="reqddlPurchaseOrg" runat="server" ControlToValidate="ddlPurchaseOrg"
                                                                        ValidationGroup="save" ErrorMessage="Select Purchasing Organization." SetFocusOnError="true"
                                                                        InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Purchasing Organization.' />" />--%>
                                                                    </td>
                                                                    <td class="leftTD" colspan="2">
                                                                        <asp:Label ID="lblVendorChange" runat="server" Text="0" Visible="false" />
                                                                        <asp:Label ID="lblVendorChangeDetailId" runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblVendorChangeAction" runat="server" Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdSpace" colspan="4"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="leftTD" style="width: 20%">Section
                                                                    <asp:Label ID="labletxtSection" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                                            TabIndex="4" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                                                            <asp:ListItem Text="Select" Value="0" />
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="reqddlSection" runat="server" ControlToValidate="ddlSection"
                                                                            ValidationGroup="save" ErrorMessage="Section cannot be blank." SetFocusOnError="true"
                                                                            InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Section cannot be blank.' />" />
                                                                    </td>
                                                                    <td class="leftTD" style="width: 20%">Field
                                                                    <asp:Label ID="lableddlField" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:DropDownList ID="ddlField" runat="server" AppendDataBoundItems="false" TabIndex="4"
                                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlField_SelectedIndexChanged">
                                                                            <asp:ListItem Text="Select" Value="0" />
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="reqddlField" runat="server" ControlToValidate="ddlField"
                                                                            ValidationGroup="save" ErrorMessage="Field cannot be blank." SetFocusOnError="true"
                                                                            InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Field cannot be blank.' />" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="leftTD" style="width: 20%">Old Value
                                                                    <asp:Label ID="labletxtOldValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                        <br />
                                                                        <span style="color: Red; font-size: x-small; font-weight: normal">(As mentioned in SAP)</span>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:TextBox ID="txtOldValue" runat="server" CssClass="textbox" Width="210" TabIndex="7" />
                                                                        <asp:RequiredFieldValidator ID="reqtxtOldValue" runat="server" ControlToValidate="txtOldValue"
                                                                            ValidationGroup="save" ErrorMessage="Old Value cannot be blank." SetFocusOnError="true"
                                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Old Value cannot be blank.' />" />
                                                                    </td>
                                                                    <td class="leftTD" style="width: 20%" runat="server" id="tdNew">New Value
                                                                    <asp:Label ID="labletxtNewValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%" runat="server" id="tdNew1">
                                                                        <asp:TextBox ID="txtNewValue" runat="server" CssClass="textbox" MaxLength="70" Width="210"
                                                                            TabIndex="7" />
                                                                        <asp:RequiredFieldValidator ID="reqtxtNewValue" runat="server" ControlToValidate="txtNewValue"
                                                                            ValidationGroup="save" ErrorMessage="New Value cannot be blank." SetFocusOnError="true"
                                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='New Value cannot be blank.' />" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="leftTD">Remarks
                                                                    </td>
                                                                    <td class="rigthTD">
                                                                        <asp:TextBox ID="txtRemarks" runat="server" MaxLength="250" TextMode="MultiLine"
                                                                            Width="90%" TabIndex="37" Rows="3" />
                                                                    </td>
                                                                    <td class="tdSpace" colspan="2"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdSpace" colspan="4"></td>
                                                                </tr>
                                                                <tr runat="server" id="tdbank" visible="false">
                                                                    <td class="leftTD" style="width: 20%">Bank country key
                                                                    <asp:Label ID="lableddlBankCountrykey" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <%--OnSelectedIndexChanged="ddlBankCountrykey_SelectedIndexChanged"--%>
                                                                        <asp:DropDownList ID="ddlBankCountrykey" runat="server" AutoPostBack="false" TabIndex="1"
                                                                            Width="200px">
                                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="reqddlBankCountrykey" runat="server" ControlToValidate="ddlBankCountrykey"
                                                                            ValidationGroup="save" ErrorMessage="Bank country key cannot be blank." InitialValue="0"
                                                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Bank country key cannot be blank.' />" />
                                                                    </td>
                                                                    <td class="leftTD" style="width: 20%">IFSC Code
                                                                    <asp:Label ID="labletxtBankKey" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                        <asp:LinkButton ID="lnkAddBank" runat="server" Font-Bold="false" Text="(Add New Bank)"
                                                                            OnClick="lnkAddBank_Click" Visible="false"></asp:LinkButton>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:TextBox ID="txtBankKey" runat="server" CssClass="textboxAutocomplete" Width="180px"
                                                                            AutoPostBack="true" TabIndex="1" MaxLength="11" onfocus="return BankKeyOnFocus();"
                                                                            onblur="return BankKeyTextChangeEvent();" OnTextChanged="txtBankKey_TextChanged" />
                                                                        <asp:RequiredFieldValidator ID="reqtxtBankKey" runat="server" ControlToValidate="txtBankKey"
                                                                            ValidationGroup="save" ErrorMessage="Bank Keys cannot be blank." SetFocusOnError="true"
                                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Bank Keys cannot be blank.' />" />
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="tdbank1" visible="false">
                                                                    <td class="tdSpace" colspan="4"></td>
                                                                </tr>
                                                                <tr runat="server" id="tdbank2" visible="false">
                                                                    <td class="leftTD" style="width: 20%">Region
                                                                    <asp:Label ID="lableddlRegion" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="False" TabIndex="3"
                                                                            Enabled="false">
                                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%--<asp:RequiredFieldValidator ID="reqddlRegion" runat="server" ControlToValidate="ddlRegion"
                                                                        ValidationGroup="save" ErrorMessage="Region cannot be blank." InitialValue="0"
                                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Region cannot be blank.' />" />--%>
                                                                    </td>
                                                                    <td class="leftTD" style="width: 20%">Name of bank
                                                                    <asp:Label ID="labletxtBankName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox" MaxLength="60" TabIndex="4"
                                                                            Enabled="false" />
                                                                        <%--<asp:RequiredFieldValidator ID="reqtxtBankName" runat="server" ControlToValidate="txtBankName"
                                                                        ValidationGroup="CompanyCode" ErrorMessage="Name of bank cannot be blank." SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Name of bank cannot be blank.' />" />--%>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="tdbank3" visible="false">
                                                                    <td class="tdSpace" colspan="4"></td>
                                                                </tr>
                                                                <tr runat="server" id="tdbank4" visible="false">
                                                                    <td class="tdSpace" colspan="4">
                                                                        <asp:Panel ID="pnlNewBank" runat="server" Visible="false" BorderWidth="2px" BorderColor="Black"
                                                                            BorderStyle="Solid">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:UpdatePanel ID="updBankNew" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <uc:ucBankMaster ID="ucBankMaster1" runat="server" />
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trBank" runat="server" visible="false">
                                                                                    <td class="centerTD">
                                                                                        <asp:Button ID="BtnBankSave" runat="server" ValidationGroup="BankMaster" Text="Save"
                                                                                            CssClass="button" OnClick="BtnBankSave_Click" />
                                                                                        <asp:Button ID="btnBankCancel" runat="server" CausesValidation="false" Text="Cancel"
                                                                                            CssClass="button" OnClick="btnBankCancel_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server" id="tdbank5" visible="false">
                                                                    <td class="tdSpace" colspan="4"></td>
                                                                </tr>
                                                                <tr runat="server" id="tdbank6" visible="false">
                                                                    <td class="leftTD" style="width: 20%">House number and street
                                                                    <asp:Label ID="labletxtHouseNoStreet" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:TextBox ID="txtHouseNoStreet" runat="server" CssClass="textbox" MaxLength="35"
                                                                            Enabled="false" TabIndex="5" />
                                                                        <%--<asp:RequiredFieldValidator ID="reqtxtHouseNoStreet" runat="server" ControlToValidate="txtHouseNoStreet"
                                                                        ValidationGroup="CompanyCode" ErrorMessage="House number and street	be blank."
                                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='House number and street be blank.' />" />--%>
                                                                    </td>
                                                                    <td class="leftTD" style="width: 20%">Bank number
                                                                    <asp:Label ID="labletxtBankNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:TextBox ID="txtBankNo" runat="server" CssClass="textbox" MaxLength="15" TabIndex="6"
                                                                            Enabled="false" />
                                                                        <%--<asp:RequiredFieldValidator ID="reqtxtBankNo" runat="server" ControlToValidate="txtBankNo"
                                                                        ValidationGroup="CompanyCode" ErrorMessage="Bank number cannot be blank." SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Bank number cannot be blank.' />" />--%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdSpace" colspan="4"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="centerTD" colspan="4">
                                                                        <asp:Button ID="btnAdd" runat="server" ValidationGroup="save" Text="Save" CssClass="button"
                                                                            UseSubmitBehavior="true" TabIndex="39" OnClick="btnAdd_Click" />
                                                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>


                                                        <div id="divValidationModulePopUp" runat="server" style="display: none;" clientidmode="Static">

                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">

                                                                <tr>
                                                                    <td class="leftTD" colspan="2">
                                                                        <span style="color: brown; font-size: 11px">

                                                                            <i>Dear User,</i>
                                                                            <br />
                                                                            <br />
                                                                            <i>Lupin has started vendor onboarding process on the new tool ‘SAP Ariba SLP’. Kindly create the onboarding request for a new supplier using the new SAP Ariba SLP tool.</i>
                                                                            <br />
                                                                            <br />

                                                                            <i>To Log-In to SAP Ariba: Click on SPARK (ARIBA) applications link from Lupin home page or use direct link</i>
                                                                            <br />
                                                                            <br />
                                                                            <i>http://lupin.procurement.ariba.com/</i>
                                                                            <br />
                                                                            <br />
                                                                            <i>Please refer the user training manual for help on how to onboard vendors in SAP Ariba - Attachment Optional.</i>
                                                                            <br />
                                                                            <br />
                                                                            <i>If the link does not open Ariba Home page, kindly fill and submit the attached form to saparibasupport@lupin.com  for SAP Ariba SLP access --- Attach Template.</i>
                                                                            <br />
                                                                            <br />
                                                                            <i>In case of any assistance/queries for vendor creation in SAP Ariba, kindly reach out to the MDM Team.</i>
                                                                            <br />
                                                                            <br />
                                                                            <i>We appreciate your actions in trying to embrace the new, Thanks!</i>

                                                                        </span>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td class="tdSpace" colspan="2"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="centerTD" colspan="2">
                                                                        <%--<asp:LinkButton ID="btnRedirect" runat="server" CssClass="button" OnClick="btnRedirect_Click" ClientIDMode="Static" CausesValidation="false" Text="Go To ARIBA Site"></asp:LinkButton>--%>
                                                                        <%--<a href="http://lupin.procurement.ariba.com/" target="_blank"  Class="button">Go To ARIBA Site</a>--%>
                                                                        <a href="VendorChange.aspx" onclick="window.open('http://lupin.procurement.ariba.com/','window name','width=900,height=600,screenX=25,screenY=25,resizable=yes,scrollbars=yes')" class="button">Go To ARIBA Site</a>
                                                                        <%--<asp:Button ID="btnbackMsg" runat="server" Text="Go To ARIBA Site" CssClass="button" OnClick="btnRedirect_Click" OnClientClick="target ='_blank';" />--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>


                                                        <%--425143--%>
                                                        <div id="divEmpValidationModulePopUp" runat="server" style="display: none;" clientidmode="Static">

                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">

                                                                <tr>
                                                                    <td class="leftTD" colspan="2">
                                                                        <span style="color: brown; font-size: 11px">

                                                                            <i>Dear User,</i>
                                                                            <br />
                                                                            <br />
                                                                            <i>Lupin has started Emploeey vendor onboarding process on the new tool ‘MSG’. Kindly Connect your HR Team.</i>
                                                                            <br />
                                                                            <br />
                                                                            <i>Thanks!</i>

                                                                        </span>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td class="tdSpace" colspan="2"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="centerTD" colspan="2">
                                                                        <%--<asp:LinkButton ID="btnRedirect" runat="server" CssClass="button" OnClick="btnRedirect_Click" ClientIDMode="Static" CausesValidation="false" Text="Go To ARIBA Site"></asp:LinkButton>--%>
                                                                        <%--<a href="http://lupin.procurement.ariba.com/" target="_blank"  Class="button">Go To ARIBA Site</a>--%>
                                                                        <%--<a href="VendorChange.aspx" onclick="window.open('http://lupin.procurement.ariba.com/','window name','width=900,height=600,screenX=25,screenY=25,resizable=yes,scrollbars=yes')" class="button">Go To ARIBA Site</a>--%>
                                                                        <asp:Button ID="btnHRRedirect" runat="server" Text="Close" CssClass="button" OnClick="btnHRRedirect_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                        <%--425143--%>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4" align="right">
                                                <asp:LinkButton ID="lnkAddNew" runat="server" Visible="false" OnClick="lnkAddNew_Click">Add New Vendor<image src="../../images/Add.jpg" border="0px"></image></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <%--<asp:UpdatePanel ID="updPnlAddValue" runat="server">
                                    <ContentTemplate>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup1" Style="display: none" />
                                                <act:ModalPopupExtender ID="ModalPopupExtenderAddValue" runat="server" TargetControlID="hiddenTargetControlForModalPopup1"
                                                    CancelControlID="btnCancelValue" PopupControlID="pnlAddValue" BackgroundCssClass="modalBackground"
                                                    DropShadow="true" PopupDragHandleControlID="pnlTitle2" />
                                                <asp:Panel ID="pnlAddValue" runat="server">
                                                    <div style="background-color: White; padding: 2px 2px 2px 2px;">
                                                        <asp:Panel ID="pnlTitle2" runat="server" Style="cursor: move; background-color: Black;
                                                            border: solid 1px Gray; color: Black">
                                                            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                                                                <span class="ui-dialog-title">Add New Value</span>
                                                            </div>
                                                        </asp:Panel>
                                                        <table border="0" cellpadding="0" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td class="tdSpace" colspan="4">
                                                                    <asp:Panel ID="pnlMsg2" runat="server" Visible="false">
                                                                        <asp:Label ID="lblMsg2" runat="server" />
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4">
                                                                    <asp:Label ID="lblVendorChangeDetailId" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblVendorChangeIddtl" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblModuleIdValue" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblCompanyId" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="leftTD" width="20%">
                                                                    Vendor Code
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:Label ID="lblVendorCode" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="leftTD" style="width: 20%">
                                                                    Vendor Name
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:Label ID="lblVendorName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="leftTD" style="width: 20%">
                                                                    Section
                                                                    <asp:Label ID="lableddlSectionValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:DropDownList ID="ddlSectionValue" runat="server" AppendDataBoundItems="false"
                                                                        AutoPostBack="true" TabIndex="4" OnSelectedIndexChanged="ddlSectionValue_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Select" Value="0" />
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="reqddlSectionValue" runat="server" ControlToValidate="ddlSectionValue"
                                                                        ValidationGroup="SaveValue" ErrorMessage="Section cannot be blank." SetFocusOnError="true"
                                                                        InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Section cannot be blank.' />" />
                                                                </td>
                                                                <td class="leftTD" style="width: 20%">
                                                                    Field
                                                                    <asp:Label ID="lableddlFieldValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:DropDownList ID="ddlFieldValue" runat="server" AppendDataBoundItems="false"
                                                                        TabIndex="4">
                                                                        <asp:ListItem Text="Select" Value="0" />
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="reqddlFieldValue" runat="server" ControlToValidate="ddlFieldValue"
                                                                        ValidationGroup="SaveValue" ErrorMessage="Feild cannot be blank." SetFocusOnError="true"
                                                                        InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Feild cannot be blank.' />" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="leftTD" style="width: 20%">
                                                                    Old Value
                                                                    <asp:Label ID="labletxtOldValueValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:TextBox ID="txtOldValueValue" runat="server" CssClass="textbox" Width="210"
                                                                        TabIndex="7" />
                                                                    <asp:RequiredFieldValidator ID="reqtxtOldValueValue" runat="server" ControlToValidate="txtOldValueValue"
                                                                        ValidationGroup="SaveValue" ErrorMessage="Old Value cannot be blank." SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Old Value cannot be blank.' />" />
                                                                </td>
                                                                <td class="leftTD" style="width: 20%">
                                                                    New Value
                                                                    <asp:Label ID="labletxtNewValueValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:TextBox ID="txtNewValueValue" runat="server" CssClass="textbox" MaxLength="70"
                                                                        Width="210" TabIndex="7" />
                                                                    <asp:RequiredFieldValidator ID="reqtxtNewValueValue" runat="server" ControlToValidate="txtNewValueValue"
                                                                        ValidationGroup="SaveValue" ErrorMessage="New Value cannot be blank." SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='New Value cannot be blank.' />" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="centerTD" colspan="4">
                                                                    <asp:Button ID="btnAddValue" runat="server" ValidationGroup="SaveValue" Text="Save"
                                                                        CssClass="button" UseSubmitBehavior="true" TabIndex="39" OnClick="btnAddValue_Click" />
                                                                    <asp:Button ID="btnCancelValue" Text="Cancel" runat="server" CssClass="button" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                <tr>
                                    <td class="tdSpace"></td>
                                </tr>
                                <%--<asp:Panel ID="pnlRemarks" runat="server" Visible="false">--%>
                                <%--<tr>
                                        <td class="leftTD">
                                            Remarks
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtRemarks" runat="server" MaxLength="250" TextMode="MultiLine"
                                                Width="90%" TabIndex="37" Rows="3" />
                                        </td>
                                        <td class="tdSpace" colspan="2">
                                        </td>
                                    </tr>--%>
                                <%--</asp:Panel>--%>
                                <tr>
                                    <td class="tdSpace"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grvVendorChange" runat="server" AutoGenerateColumns="false" Width="100%"
                                            BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both" OnRowDataBound="grvVendorChange_RowDataBound">
                                            <RowStyle CssClass="light-gray" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle CssClass="gridHeader" />
                                            <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="left" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVendorChangeId" runat="server" Text='<%# Eval("Vendor_Change_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Customer_Code" HeaderText="SAP Code" ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="Vendor_Desc" HeaderText="Name" ItemStyle-Width="15%" />
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company" Visible="false" />
                                                <asp:BoundField DataField="VendorAccGrpName" HeaderText="Acc. Grp." Visible="false" />
                                                <asp:BoundField DataField="Purchase_OrgName" HeaderText="Purch. Org." ItemStyle-Width="10%" />
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        &nbsp;
                                                        <asp:ImageButton ID="lnkAddValue" ImageUrl="~/images/Add.jpg" runat="server" OnClick="lnkAddValue_Click"
                                                            ToolTip="Add Field" Font-Bold="true" CommandArgument='<%# Eval("Vendor_Change_Id") %>' />&nbsp;
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Changes">
                                                    <ItemTemplate>
                                                        <asp:GridView ID="grvVendorChangeDtl" runat="server" AutoGenerateColumns="false"
                                                            Width="100%" BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both">
                                                            <RowStyle CssClass="light-gray" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle CssClass="gridHeader" />
                                                            <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVendorChangeDtl" runat="server" Text='<%# Eval("Vendor_Change_Detail_Id") %>'></asp:Label>
                                                                        <asp:Label ID="lblSectionFeildMasterId" runat="server" Text='<%# Eval("Section_Feild_Master_Id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Decsription" HeaderText="Section" ItemStyle-Width="22%" />
                                                                <asp:BoundField DataField="FeildDisplayName" HeaderText="Field" ItemStyle-Width="22%" />
                                                                <asp:BoundField DataField="Old_Value" HeaderText="Old Value" ItemStyle-Width="22%" />
                                                                <asp:BoundField DataField="New_Value" HeaderText="New Value" ItemStyle-Width="22%" />
                                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="22%" />
                                                                <asp:TemplateField Visible="false" ItemStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/Edit.jpg" OnClick="btnEdit_Click"
                                                                            ToolTip="Edit Field" Font-Bold="true" CommandArgument='<%# Eval("Vendor_Change_Detail_Id") %>' />&nbsp;
                                                                        <asp:ImageButton ID="btnDelete" runat="server" Text="X" ImageUrl="~/images/Delete.bmp"
                                                                            ToolTip="Delete Field" ForeColor="Red" Font-Size="15px" OnClick="btnDelete_Click"
                                                                            Font-Bold="true" CommandArgument='<%# Eval("Vendor_Change_Detail_Id") %>' OnClientClick="return confirm('Are you certain you want to delete this entry?');" />&nbsp;
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        &nbsp;
                                                                        <asp:Button ID="btnDelete" runat="server" Text="X" ForeColor="Red" Font-Size="15px" OnClick="btnDelete_Click"
                                                                            Font-Bold="true" OnClientClick="return confirm('Are you certain you want to delete this entry?');" />&nbsp;
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace"></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="leftTD" align="left" colspan="2">
                    <b>Attach Documents (Image/PDF/Excel Files Only)</b>
                </td>
            </tr>
            <tr>
                <td class="rigthTD" align="left" valign="top">
                    <div>
                        <asp:FileUpload ID="file_upload" class="multi" runat="server" TabIndex="36" />
                        <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />
                    </div>
                </td>
                <td class="rigthTD" align="left">
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
            <tr id="trButton" runat="server" visible="false">
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnPrevious" runat="server" Text="Back" UseSubmitBehavior="false"
                        TabIndex="38" CssClass="button" OnClick="btnPrevious_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" UseSubmitBehavior="true"
                        TabIndex="39" OnClick="btnSave_Click" />
                    <asp:Button ID="btnNext" runat="server" Text="Save & Next" UseSubmitBehavior="true"
                        TabIndex="40" CssClass="button" OnClick="btnNext_Click" Width="120px" />
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="4"></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="save" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SaveValue"
        ShowMessageBox="true" ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="47" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblVendorChangeId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <script type="text/javascript">
        var textboxId = "";
        var textboxRealId = "";
        var ActionType = $('#<%= lblActionType.ClientID%>').html();
        var CountryId = "";

        function BankKeyOnFocus() {

            CountryId = $('#<%= ddlBankCountrykey.ClientID%>').val();

            textboxId = $('#<%= txtBankKey.ClientID%>').attr('ID');
            textboxRealId = CountryId;
            AutoCompleteBankIFSC();
        }



        function BankKeyTextChangeEvent() {
            __doPostBack($('#<%= txtBankKey.ClientID%>').attr('ID'), 'TextChanged');

        }

<%--       function ShowValidationNewDialog() {
            debugger;
            $("#divValidationModulePopUp").dialog({
                height: 250,
                width: 500,
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


            $('#<%= btnbackMsg.ClientID%>')._show();
        }--%>


        //function SetTarget() {
        //    document.getElementById["btnbackMsg"].target = "_blank";
        //}

        //})

    </script>
    <%-- Added by Swati for Vendor Excel Upload on 12.12.2018 --%>
    <div align="left" style="width: 98%">
        <uc:ExcelDownload ID="ExcelDownload1" runat="server" ActionType="C" Visible="false" />
    </div>
    <div align="left" style="width: 98%">
        <uc:VendorChangeExcelUpload ID="VendorChangeExcelUpload1" runat="server" />
    </div>
    <%-- End Change --%>
</asp:Content>
