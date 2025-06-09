<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Vendor/VendorMasterPage.master"
    AutoEventWireup="true" CodeFile="PartnerFunction.aspx.cs" Inherits="Transaction_Vendor_PartnerFunction" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Src="~/Transaction/UserControl/ucExcelDownload2.ascx" TagPrefix="uc" TagName="ExcelDownload" %>
<%@ Register Src="~/Transaction/UserControl/PartnerFunExcelUpload.ascx" TagPrefix="uc" TagName="PartnerFunExcelUpload" %>

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
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">

            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                        <asp:Label ID="lblMsg" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="trHeading" align="center" colspan="2">Partner Function Data
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
                                                                <span class="ui-dialog-title">Partner Function Details</span>
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
                                                                    <td class="tdSpace" colspan="4"></td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="leftTD" width="20%">Vendor Code
                                                                    <asp:Label ID="labletxtVendorCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:TextBox ID="txtVendorCode" runat="server" CssClass="textbox" MaxLength="16"
                                                                            AutoPostBack="true" TabIndex="1" Width="180" OnTextChanged="txtVendorCode_TextChanged"
                                                                            onkeydown="return (event.keyCode!=13);" />
                                                                        <asp:RequiredFieldValidator ID="reqtxtVendorCode" runat="server" ControlToValidate="txtVendorCode"
                                                                            ValidationGroup="PFunc" ErrorMessage="Vendor Code cannot be blank." SetFocusOnError="true"
                                                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Code cannot be blank.' />" />
                                                                        <asp:RegularExpressionValidator ID="regtxtVendorCode" runat="server" ControlToValidate="txtVendorCode"
                                                                            ValidationGroup="PFunc" ErrorMessage="Vendor Code Invalid." SetFocusOnError="true"
                                                                            ValidationExpression="^[\S]{4,10}" Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Code Invalid.' />" />
                                                                    </td>
                                                                    <td class="leftTD" style="width: 20%">Vendor Name
                                                                    <asp:Label ID="labletxtVendorName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                        <br />
                                                                        <span style="color: Red; font-size: x-small; font-weight: normal">(As mentioned in SAP)</span>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:TextBox ID="txtVendorName" runat="server" CssClass="textbox" MaxLength="40"
                                                                            Width="210" TabIndex="2" />
                                                                        <asp:RequiredFieldValidator ID="reqtxtVendorName" runat="server" ControlToValidate="txtVendorName"
                                                                            ValidationGroup="PFunc" ErrorMessage="Vendor Name cannot be blank." SetFocusOnError="true"
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
                                                                            TabIndex="3" Enabled="true" AutoPostBack="true" 
                                                                            OnSelectedIndexChanged="ddlCompanyCode_SelectedIndexChanged">
                                                                            <asp:ListItem Text="Select" Value="0" />
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="reqddlCompanyCode" runat="server" ControlToValidate="ddlCompanyCode"
                                                                            ValidationGroup="PFunc" ErrorMessage="Select Company Code." SetFocusOnError="true"
                                                                            InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company Code.' />" />
                                                                    </td>
                                                                    <td class="leftTD" width="20%">Vendor account group
                             <asp:Label ID="lableddlVendorAccGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:DropDownList ID="ddlVendorAccGrp" runat="server" AppendDataBoundItems="false"
                                                                            Enabled="false" TabIndex="4">
                                                                            <asp:ListItem Text="Select" Value="0" />
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="reqddlVendorAccGrp" runat="server" ControlToValidate="ddlVendorAccGrp"
                                                                            ValidationGroup="PFunc" ErrorMessage="Select  Vendor account group." SetFocusOnError="true"
                                                                            InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select  Vendor account group.' />" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdSpace" colspan="4"></td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="leftTD" width="20%">Purchasing
                                <asp:Label ID="lableddlPurchaseOrg" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                                                                        Organization
                                                                    </td>
                                                                    <td class="rigthTD" style="width: 30%">
                                                                        <asp:DropDownList ID="ddlPurchaseOrg" runat="server" AppendDataBoundItems="false"
                                                                            TabIndex="5" Enabled="false">
                                                                            <asp:ListItem Text="Select" Value="0" />
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="reqddlPurchaseOrg" runat="server" ControlToValidate="ddlPurchaseOrg"
                                                                            ValidationGroup="PFunc" ErrorMessage="Select Purchasing Organization." SetFocusOnError="true"
                                                                            InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Purchasing Organization.' />" />
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdSpace" colspan="4"></td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="leftTD" width="20%" colspan="2">Partner Function Details
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtNewRow" runat="server" Text="1" MaxLength="3" Width="20px" Enabled="false" CssClass="textbox" Style="display: none;" />
                                                                        <asp:RangeValidator ID="rangePositionNumber" runat="server" ValidationGroup="addRowValidation"
                                                                            ControlToValidate="txtNewRow" MaximumValue="20" MinimumValue="1" Type="Integer"
                                                                            ErrorMessage="Enter Numeric Value only (Maximum limit 20)." SetFocusOnError="true"
                                                                            Display="Dynamic"
                                                                            Text="<img src='../../images/Error.png' title='Enter Numeric Value only (Maximum limit 20).' />"></asp:RangeValidator>
                                                                        <asp:Button ID="Button1" runat="server" Text="Add New Row" ValidationGroup="addRowValidation"
                                                                            OnClick="btnAddRow_Click" CssClass="button" UseSubmitBehavior="false" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdSpace" colspan="4"></td>
                                                                </tr>

                                                                <tr>
                                                                    <td colspan="4">

                                                                        <asp:GridView ID="grdPFunDetailAdd" runat="server" AutoGenerateColumns="false"
                                                                            DataKeyNames="Vendor_PFun_Detail_Id,sPfun_Lookup_Code"
                                                                            Width="1000px" CssClass="GridClass" ShowHeaderWhenEmpty="true" Visible="true"
                                                                            ShowFooter="false" AllowSorting="true"
                                                                            OnRowCommand="grdPFunDetailAdd_RowCommand"
                                                                            OnRowDataBound="grdPFunDetailAdd_RowDataBound">
                                                                            <FooterStyle CssClass="gridFooter" />
                                                                            <RowStyle CssClass="light-gray" />
                                                                            <HeaderStyle CssClass="gridHeader" />
                                                                            <AlternatingRowStyle CssClass="gridRowStyle" />
                                                                            <Columns>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Remove">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Vendor_PFun_Detail_Id") %>' CommandName="D" CausesValidation="false">  
                                                                                                 <img src="../../images/delete.png" alt="Delete" title='Delete' Width="20px"/></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                    Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblVendor_PFun_Detail_Id" runat="server" Text='<%#Eval("Vendor_PFun_Detail_Id") %>'
                                                                                            Visible="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>


                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Partner Function">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlPfun_Lookup_Id" runat="server" AppendDataBoundItems="false" Width="150px">
                                                                                            <asp:ListItem Text="Select" Value="" />
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Vendor Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtsVendor_Code" runat="server" CssClass="textbox" Text='<%#Eval("sVendor_Code") %>'
                                                                                            Width="150px" MaxLength="16" OnChange="return GetSelectedRow(this)"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                                    HeaderText="Vendor Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtsVendor_Desc" runat="server" CssClass="textbox" Text='<%#Eval("sVendor_Desc") %>'
                                                                                            Width="600px" MaxLength="40"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" class="tdSpace"></td>
                                                                </tr>


                                                                <tr>
                                                                    <td class="centerTD" colspan="4">
                                                                        <asp:Button ID="btnAdd" runat="server" ValidationGroup="PFunc" Text="Save" CssClass="button"
                                                                            UseSubmitBehavior="true" TabIndex="39" OnClick="btnAdd_Click" />
                                                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4" align="right">
                                                <asp:LinkButton ID="lnkAddNew" runat="server" Visible="false" OnClick="lnkAddNew_Click">Add New
                                                    <image src="../../images/Add.jpg" border="0px"></image></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <tr>
                                    <td class="tdSpace"></td>
                                </tr>
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
                                                <asp:BoundField DataField="Customer_Code" HeaderText="SAP Code" ItemStyle-Width="8%" />
                                                <asp:BoundField DataField="Vendor_Desc" HeaderText="Name" ItemStyle-Width="20%" />
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company" Visible="false" />
                                                <asp:BoundField DataField="VendorAccGrpName" HeaderText="Acc. Grp." Visible="false" />
                                                <asp:BoundField DataField="Purchase_OrgName" HeaderText="Purch. Org." ItemStyle-Width="10%" />
                                                <asp:TemplateField Visible="false" HeaderText="Edit" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        &nbsp;
                                                        <asp:ImageButton ID="btnEdit" ImageUrl="~/images/Edit.jpg" runat="server" OnClick="btnEdit_Click"
                                                            ToolTip="Edit" Font-Bold="true" CommandArgument='<%# Eval("Vendor_Change_Id") %>' />&nbsp;

                                                        <asp:ImageButton ID="btnDelete" runat="server" Text="X" ImageUrl="~/images/Delete.bmp"
                                                            ToolTip="Delete" ForeColor="Red" Font-Size="15px" OnClick="btnDelete_Click"
                                                            Font-Bold="true" CommandArgument='<%# Eval("Vendor_Change_Id") %>' OnClientClick="return confirm('Are you certain you want to delete this entry?');" />&nbsp;

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Partner Function Details">
                                                    <ItemTemplate>
                                                        <asp:GridView ID="grvVendorChangeDtl" runat="server" AutoGenerateColumns="false"
                                                            Width="100%" BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both">
                                                            <RowStyle CssClass="light-gray" HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <HeaderStyle CssClass="gridHeader" />
                                                            <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVendorChangeDtl" runat="server" Text='<%# Eval("Vendor_PFun_Detail_Id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="sPfun_Lookup_Code" HeaderText="Partner Function" ItemStyle-Width="10%" />
                                                                <asp:BoundField DataField="sVendor_Code" HeaderText="Vendor Code" ItemStyle-Width="8%" />
                                                                <asp:BoundField DataField="sVendor_Desc" HeaderText="Vendor Name" ItemStyle-Width="22%" />

                                                            </Columns>
                                                        </asp:GridView>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
    <%-- <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="save" ShowMessageBox="true"
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

    --%>

    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="PFunc" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:ValidationSummary ID="VaSu" runat="server" ShowMessageBox="true" ShowSummary="false"
        ValidationGroup="addRowValidation" />

    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="99" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblPartnerFunctionId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <asp:Label ID="lblVendorChange" runat="server" Text="0" Visible="false" />


    <script type="text/javascript" language="javascript">
        var textboxId = "";
        var textboxRealId = "";
        var ActionType = $('#<%= lblActionType.ClientID%>').html();
        var CountryId = "";

        function GetSelectedRow(lnk) {
            var row = lnk.parentNode.parentNode;

            var pddlPfun_Lookup_Id = row.cells[1].getElementsByTagName("select")[0].value;
            var ptxtsVendor_Code = row.cells[2].getElementsByTagName("input")[0].value;

            var grid = document.getElementById("<%= grdPFunDetailAdd.ClientID %>");
            var cellNM;
            var flag = 0;
            if (grid.rows.length > 0) {
                for (i = 1; i <= grid.rows.length - 1; i++) {
                    cellNM = grid.rows[i].cells[1].getElementsByTagName("select")[0].value;
                    cellVCode = grid.rows[i].cells[2].getElementsByTagName("input")[0].value;
                    if (cellNM != '0') {
                        if (cellNM == pddlPfun_Lookup_Id && cellVCode == ptxtsVendor_Code) {
                            flag += 1
                            if (flag > 1) {
                                grid.rows[i].cells[2].getElementsByTagName("input")[0].value = '';
                                alert("Selected Partner function and Vendor code already exists.")
                                return false;
                            }
                        }
                    }
                    else {
                        alert("Please select Partner function.")
                        return false;
                    }

                }
            }

        }


    </script>
    <div align="left" style="width: 98%">
        <uc:ExcelDownload ID="ExcelDownload1" runat="server" ActionType="MN" Visible="false" />
    </div>
    <div align="left" style="width: 98%">
        <uc:PartnerFunExcelUpload ID="PartnerFunExcelUpload" runat="server" Visible="false" />
    </div>

    <%--  <asp:panel id="pnlMsg" runat="server" visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:panel>
    <asp:panel id="pnlData" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="4">
                    <asp:Panel ID="pnlWarning" runat="server" Visible="false">
                        <asp:Label ID="lblWarning" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="trHeading" align="center" colspan="2">Vendor Partner Function
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td class="leftTD" width="20%">Vendor Code
                            <asp:Label ID="labletxtVendorCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtVendorCode" runat="server" CssClass="textbox" MaxLength="16"
                                    AutoPostBack="true" TabIndex="1" Width="180" OnTextChanged="txtVendorCode_TextChanged"
                                    onkeydown="return (event.keyCode!=13);" />
                                <asp:RequiredFieldValidator ID="reqtxtVendorCode" runat="server" ControlToValidate="txtVendorCode"
                                    ValidationGroup="PFunc" ErrorMessage="Vendor Code cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Code cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtVendorCode" runat="server" ControlToValidate="txtVendorCode"
                                    ValidationGroup="PFunc" ErrorMessage="Vendor Code Invalid." SetFocusOnError="true"
                                    ValidationExpression="^[\S]{4,10}" Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Code Invalid.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">Vendor Name
                                                                    <asp:Label ID="labletxtVendorName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                <br />
                                <span style="color: Red; font-size: x-small; font-weight: normal">(As mentioned in SAP)</span>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtVendorName" runat="server" CssClass="textbox" MaxLength="40"
                                    Width="210" TabIndex="2" />
                                <asp:RequiredFieldValidator ID="reqtxtVendorName" runat="server" ControlToValidate="txtVendorName"
                                    ValidationGroup="PFunc" ErrorMessage="Vendor Name cannot be blank." SetFocusOnError="true"
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
                                    TabIndex="3" Enabled="false">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCompanyCode" runat="server" ControlToValidate="ddlCompanyCode"
                                    ValidationGroup="PFunc" ErrorMessage="Select Company Code." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company Code.' />" />
                            </td>
                            <td class="leftTD" width="20%">Vendor account group
                             <asp:Label ID="lableddlVendorAccGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlVendorAccGrp" runat="server" AppendDataBoundItems="false"
                                    Enabled="false" TabIndex="4">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlVendorAccGrp" runat="server" ControlToValidate="ddlVendorAccGrp"
                                    ValidationGroup="PFunc" ErrorMessage="Select  Vendor account group." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select  Vendor account group.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td class="leftTD" width="20%">Purchasing
                                <asp:Label ID="lableddlPurchaseOrg" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                                Organization
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlPurchaseOrg" runat="server" AppendDataBoundItems="false"
                                    TabIndex="5" Enabled="false">
                                    <asp:ListItem Text="Select" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPurchaseOrg" runat="server" ControlToValidate="ddlPurchaseOrg"
                                    ValidationGroup="PFunc" ErrorMessage="Select Purchasing Organization." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Purchasing Organization.' />" />
                            </td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td class="leftTD" width="20%" colspan="2">Partner Function Details
                            </td>
                            <td>
                                <asp:TextBox ID="txtNewRow" runat="server" Text="1" MaxLength="3" Width="20px" Enabled="false" CssClass="textbox" Style="display: none;" />
                                <asp:RangeValidator ID="rangePositionNumber" runat="server" ValidationGroup="addRowValidation"
                                    ControlToValidate="txtNewRow" MaximumValue="20" MinimumValue="1" Type="Integer"
                                    ErrorMessage="Enter Numeric Value only (Maximum limit 20)." SetFocusOnError="true"
                                    Display="Dynamic"
                                    Text="<img src='../../images/Error.png' title='Enter Numeric Value only (Maximum limit 20).' />"></asp:RangeValidator>
                                <asp:Button ID="btnAdd" runat="server" Text="Add New Row" ValidationGroup="addRowValidation"
                                    OnClick="btnAdd_Click" CssClass="button" UseSubmitBehavior="false" /></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td colspan="4">

                                <asp:GridView ID="grdPFunDetailAdd" runat="server" AutoGenerateColumns="false"
                                    DataKeyNames="Vendor_PFun_Detail_Id,sPfun_Lookup_Code"
                                    Width="1000px" CssClass="GridClass" ShowHeaderWhenEmpty="true" Visible="true"
                                    ShowFooter="false" AllowSorting="true"
                                    OnRowCommand="grdPFunDetailAdd_RowCommand"
                                    OnRowDataBound="grdPFunDetailAdd_RowDataBound">
                                    <FooterStyle CssClass="gridFooter" />
                                    <RowStyle CssClass="light-gray" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <AlternatingRowStyle CssClass="gridRowStyle" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Remove">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Vendor_PFun_Detail_Id") %>' CommandName="D" CausesValidation="false">  
                                        <img src="../../images/delete.png" alt="Delete" title='Delete' Width="20px"/></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVendor_PFun_Detail_Id" runat="server" Text='<%#Eval("Vendor_PFun_Detail_Id") %>'
                                                    Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Partner Function">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlPfun_Lookup_Id" runat="server" AppendDataBoundItems="false" Width="150px">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Vendor Code">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsVendor_Code" runat="server" CssClass="textbox" Text='<%#Eval("sVendor_Code") %>'
                                                    Width="150px" MaxLength="16" OnChange="return GetSelectedRow(this)"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Vendor Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsVendor_Desc" runat="server" CssClass="textbox" Text='<%#Eval("sVendor_Desc") %>'
                                                    Width="600px" MaxLength="40"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView> 
                            </td>
                        </tr> 
                        <tr>
                            <td colspan="4" class="tdSpace"></td>
                        </tr>



                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="PFunc" Text="Save" CssClass="button"
                                    TabIndex="7" OnClick="btnSave_Click" UseSubmitBehavior="true" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:panel>
    <asp:validationsummary id="sm" runat="server" validationgroup="PFunc" showmessagebox="true"
        showsummary="false" />
    <asp:validationsummary id="VaSu" runat="server" showmessagebox="true" showsummary="false"
        validationgroup="addRowValidation" />

    <asp:label id="lblUserId" runat="server" visible="false" />
    <asp:label id="lblMasterHeaderId" runat="server" visible="false" />
    <asp:label id="lblSectionId" runat="server" text="101" visible="false" />
    <asp:label id="lblMode" runat="server" visible="false" />
    <asp:label id="lblModuleId" runat="server" visible="false" />
    <asp:label id="lblPartnerFunctionId" runat="server" visible="false" />
    <asp:label id="lblActionType" runat="server" style="display: none" />
    <asp:label id="lblVendorChange" runat="server" text="0" visible="false" />

    <script type="text/javascript" language="javascript"> 
        function GetSelectedRow(lnk) { 
            var row = lnk.parentNode.parentNode; 

            var pddlPfun_Lookup_Id = row.cells[1].getElementsByTagName("select")[0].value;
            var ptxtsVendor_Code = row.cells[2].getElementsByTagName("input")[0].value;

            var grid = document.getElementById("<%= grdPFunDetailAdd.ClientID %>");
            var cellNM;
            var flag = 0;
            if (grid.rows.length > 0) {
                for (i = 1; i <= grid.rows.length - 1; i++) {
                    cellNM = grid.rows[i].cells[1].getElementsByTagName("select")[0].value;
                    cellVCode = grid.rows[i].cells[2].getElementsByTagName("input")[0].value;
                    if (cellNM != '0') {
                        if (cellNM == pddlPfun_Lookup_Id && cellVCode == ptxtsVendor_Code) {
                            flag += 1
                            if (flag > 1) {
                                grid.rows[i].cells[2].getElementsByTagName("input")[0].value = '';
                                alert("Selected Partner function and Vendor code already exists.")
                                return false;
                            }
                        }
                    }
                    else {
                        alert("Please select Partner function.")
                        return false;
                    }

                }
            }

        }


    </script>--%>
</asp:Content>

