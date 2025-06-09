<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Customer/CustomerMasterPage.master"
    AutoEventWireup="true" CodeFile="CustomerChange.aspx.cs" Inherits="Transaction_Customer_CustomerChange" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Src="~/Transaction/UserControl/ucBankMaster.ascx" TagPrefix="uc" TagName="ucBankMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/jquery.MultiFile.js" type="text/javascript"></script>
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <style type="text/css">
        .modalBackground
        {
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
                <td class="trHeading" align="center" colspan="2">
                    Customer Change Data
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
                                                <asp:Panel ID="pnlAddData" runat="server" Width="100%">
                                                    <div style="background-color: White; padding: 2px 2px 2px 2px;">
                                                        <asp:Panel ID="pnlTitle" runat="server" Style="cursor: move; background-color: Black;
                                                            border: solid 1px Gray; color: Black">
                                                            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                                                                <span class="ui-dialog-title">Change :: Add Details</span>
                                                            </div>
                                                        </asp:Panel>
                                                        <table border="0" cellpadding="0" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td class="tdSpace" colspan="4">
                                                                    <asp:Panel ID="pnlMsg1" runat="server" Visible="false">
                                                                        <asp:Label ID="lblMsg1" runat="server" />
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="leftTD" width="20%">
                                                                    Customer Code
                                                                    <asp:Label ID="labletxtCustomerCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="textbox" MaxLength="10"
                                                                        AutoPostBack="true" TabIndex="1" Width="180" OnTextChanged="txtCustomerCode_TextChanged" />
                                                                    <asp:RequiredFieldValidator ID="reqtxtCustomerCode" runat="server" ControlToValidate="txtCustomerCode"
                                                                        ValidationGroup="CustChange" ErrorMessage="Customer Code cannot be blank." SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Code cannot be blank.' />" />
                                                                    <asp:RegularExpressionValidator ID="regtxtCustomerCode" runat="server" ControlToValidate="txtCustomerCode"
                                                                        ValidationGroup="CustChange" ErrorMessage="Customer Code Invalid." SetFocusOnError="true"
                                                                        ValidationExpression="^[\S]{4,10}" Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Code Invalid.' />" />
                                                                </td>
                                                                <td class="leftTD" style="width: 20%">
                                                                    Customer Name
                                                                    <asp:Label ID="labletxtCustomerName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    <br />
                                                                    <span style="color: Red; font-size: x-small; font-weight: normal">(As mentioned in SAP)</span>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="textbox" MaxLength="70"
                                                                        Width="210" TabIndex="2" />
                                                                    <asp:RequiredFieldValidator ID="reqtxtCustomerName" runat="server" ControlToValidate="txtCustomerName"
                                                                        ValidationGroup="CustChange" ErrorMessage="Customer Name cannot be blank." SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Name cannot be blank.' />" />
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
                                                                        Enabled="false" TabIndex="3">
                                                                        <asp:ListItem Text="Select" Value="0" />
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="reqddlCompanyCode" runat="server" ControlToValidate="ddlCompanyCode"
                                                                        ValidationGroup="CustChange" ErrorMessage="Select Company Code." SetFocusOnError="true"
                                                                        InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company Code.' />" />
                                                                </td>
                                                                <td class="leftTD" width="20%">
                                                                    Customer account group
                                                                    <asp:Label ID="lableddlCustAccGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:DropDownList ID="ddlCustAccGrp" runat="server" AppendDataBoundItems="false"
                                                                        Enabled="false" AutoPostBack="true" TabIndex="4" OnSelectedIndexChanged="ddlCustAccGrp_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Select" Value="0" />
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="reqddlCustAccGrp" runat="server" ControlToValidate="ddlCustAccGrp"
                                                                        ValidationGroup="CustChange" ErrorMessage="Select  Vendor account group." SetFocusOnError="true"
                                                                        InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select  Vendor account group.' />" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="leftTD">
                                                                    Sales Organization
                                                                    <%--<asp:Label ID="lableddlSalesOrginization" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                                                </td>
                                                                <td class="rigthTD">
                                                                    <asp:DropDownList ID="ddlSalesOrginization" runat="server" TabIndex="5" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlSalesOrginization_SelectedIndexChanged">
                                                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="reqddlSalesOrginization" runat="server" ControlToValidate="ddlSalesOrginization"
                                                                        ValidationGroup="CustChange" ErrorMessage="Sales Organization cannot be blank."
                                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />--%>
                                                                </td>
                                                                <td class="leftTD">
                                                                    Distribution Channel
                                                                    <%--<asp:Label ID="lableddlDistributionChannel" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                                                </td>
                                                                <td class="rigthTD">
                                                                    <asp:DropDownList ID="ddlDistributionChannel" runat="server" TabIndex="6" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlDistributionChannel_SelectedIndexChanged">
                                                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="reqddlDistributionChannel" runat="server" ControlToValidate="ddlDistributionChannel"
                                                                        ValidationGroup="CustChange" ErrorMessage="Distribution Channel cannot be blank."
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
                                                                    <%--<asp:Label ID="lableddlDivision" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                                                </td>
                                                                <td class="rigthTD">
                                                                    <asp:DropDownList ID="ddlDivision" runat="server" TabIndex="7">
                                                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="reqddlDivision" runat="server" ControlToValidate="ddlDivision"
                                                                        ValidationGroup="CustChange" ErrorMessage="Division cannot be blank." SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Division cannot be blank.' />" />--%>
                                                                </td>
                                                                <td class="leftTD" colspan="2">
                                                                    <asp:Label ID="lblCustomerChange" runat="server" Text="0" Visible="false" />
                                                                    <asp:Label ID="lblCustomerChangeDetailId" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblCustomerChangeAction" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="leftTD" style="width: 20%">
                                                                    Section
                                                                    <asp:Label ID="labletxtSection" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                                        TabIndex="8" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Select" Value="0" />
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="reqddlSection" runat="server" ControlToValidate="ddlSection"
                                                                        ValidationGroup="CustChange" ErrorMessage="Section cannot be blank." SetFocusOnError="true"
                                                                        InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Section cannot be blank.' />" />
                                                                </td>
                                                                <td class="leftTD" style="width: 20%">
                                                                    Field
                                                                    <asp:Label ID="lableddlField" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:DropDownList ID="ddlField" runat="server" AppendDataBoundItems="false" TabIndex="9">
                                                                        <asp:ListItem Text="Select" Value="0" />
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="reqddlField" runat="server" ControlToValidate="ddlField"
                                                                        ValidationGroup="CustChange" ErrorMessage="Field cannot be blank." SetFocusOnError="true"
                                                                        InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Field cannot be blank.' />" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="leftTD" style="width: 20%">
                                                                    Old Value
                                                                    <asp:Label ID="labletxtOldValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    <br />
                                                                    <span style="color: Red; font-size: x-small; font-weight: normal">(As mentioned in SAP)</span>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:TextBox ID="txtOldValue" runat="server" CssClass="textbox" Width="210" TabIndex="10" />
                                                                    <asp:RequiredFieldValidator ID="reqtxtOldValue" runat="server" ControlToValidate="txtOldValue"
                                                                        ValidationGroup="CustChange" ErrorMessage="Old Value cannot be blank." SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Old Value cannot be blank.' />" />
                                                                </td>
                                                                <td class="leftTD" style="width: 20%">
                                                                    New Value
                                                                    <asp:Label ID="labletxtNewValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:TextBox ID="txtNewValue" runat="server" CssClass="textbox" MaxLength="70" Width="210"
                                                                        TabIndex="11" />
                                                                    <asp:RequiredFieldValidator ID="reqtxtNewValue" runat="server" ControlToValidate="txtNewValue"
                                                                        ValidationGroup="CustChange" ErrorMessage="New Value cannot be blank." SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='New Value cannot be blank.' />" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="centerTD" colspan="4">
                                                                    <asp:Button ID="btnAdd" runat="server" ValidationGroup="CustChange" Text="Save" CssClass="button"
                                                                        UseSubmitBehavior="true" TabIndex="12" OnClick="btnAdd_Click" />
                                                                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4" align="right">
                                                <asp:LinkButton ID="lnkAddNew" runat="server" Visible="false" OnClick="lnkAddNew_Click">Add New Customer<image src="../../images/Add.jpg" border="0px" ></image></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <tr>
                                    <td class="tdSpace">
                                    </td>
                                </tr>
                                <asp:Panel ID="pnlRemarks" runat="server" Visible="false">
                                    <tr>
                                        <td class="leftTD">
                                            Remarks
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtRemarks" runat="server" MaxLength="250" TextMode="MultiLine"
                                                Width="90%" TabIndex="37" Rows="3" />
                                        </td>
                                        <td class="tdSpace" colspan="2">
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td class="tdSpace">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grvCustomerChange" runat="server" AutoGenerateColumns="false" Width="100%"
                                            BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both" OnRowDataBound="grvCustomerChange_RowDataBound">
                                            <RowStyle CssClass="light-gray" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle CssClass="gridHeader" />
                                            <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="left" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerChangeId" runat="server" Text='<%# Eval("Customer_Change_Id") %>'></asp:Label>
                                                        <asp:Label ID="lblSalesOrgId" runat="server" Text='<%# Eval("Sales_Organisation_Id") %>'></asp:Label>
                                                        <asp:Label ID="lblDistChnlId" runat="server" Text='<%# Eval("Distribution_Channel_Id") %>'></asp:Label>
                                                        <asp:Label ID="lblDivisionId" runat="server" Text='<%# Eval("Division_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Customer_Code" HeaderText="SAP Code" ItemStyle-Width="8%" />
                                                <asp:BoundField DataField="Customer_Desc" HeaderText="Name" ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company" Visible="false" />
                                                <asp:BoundField DataField="CustomerAccGrpName" HeaderText="Acc. Grp." Visible="false" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-Width="25%" HeaderText="Sales Area">
                                                    <ItemTemplate>
                                                        Sales Org.:
                                                        <asp:Label ID="lblSalesOrg" runat="server" Text='<%# Eval("SalesOrgName") %>'></asp:Label>
                                                        <br />
                                                        Dist. Chnl.:
                                                        <asp:Label ID="lblDistChnl" runat="server" Text='<%# Eval("DistributionChnlName") %>'></asp:Label>
                                                        <br />
                                                        Division.:
                                                        <asp:Label ID="lblDivision" runat="server" Text='<%# Eval("DivisionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkAddValue" ImageUrl="~/images/Add.jpg" runat="server" OnClick="lnkAddValue_Click"
                                                            ToolTip="Add Field" Font-Bold="true" CommandArgument='<%# Eval("Customer_Change_Id") %>' />&nbsp;
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Changes">
                                                    <ItemTemplate>
                                                        <asp:GridView ID="grvCustomerChangeDtl" runat="server" AutoGenerateColumns="false"
                                                            Width="100%" BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both">
                                                            <RowStyle CssClass="light-gray" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle CssClass="gridHeader" />
                                                            <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCustomerChangeDtl" runat="server" Text='<%# Eval("Customer_Change_Detail_Id") %>'></asp:Label>
                                                                        <asp:Label ID="lblSectionFeildMasterId" runat="server" Text='<%# Eval("Section_Feild_Master_Id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Decsription" HeaderText="Section" ItemStyle-Width="22%" />
                                                                <asp:BoundField DataField="FeildDisplayName" HeaderText="Field" ItemStyle-Width="22%" />
                                                                <asp:BoundField DataField="Old_Value" HeaderText="Old Value" ItemStyle-Width="22%" />
                                                                <asp:BoundField DataField="New_Value" HeaderText="New Value" ItemStyle-Width="22%" />
                                                                <asp:TemplateField Visible="false" ItemStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/Edit.jpg" OnClick="btnEdit_Click"
                                                                            ToolTip="Edit Field" Font-Bold="true" CommandArgument='<%# Eval("Customer_Change_Detail_Id") %>' />&nbsp;
                                                                        <asp:ImageButton ID="btnDelete" runat="server" Text="X" ImageUrl="~/images/Delete.bmp"
                                                                            ToolTip="Delete Field" ForeColor="Red" Font-Size="15px" OnClick="btnDelete_Click"
                                                                            Font-Bold="true" CommandArgument='<%# Eval("Customer_Change_Detail_Id") %>' OnClientClick="return confirm('Are you certain you want to delete this entry?');" />&nbsp;
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace">
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="leftTD" align="left" colspan="2">
                    <b>Attach Documents (Image/PDF Files Only)</b>
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
                <td class="tdSpace" colspan="4">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="save" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SaveValue"
        ShowMessageBox="true" ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="48" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblCustomerChangeId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
</asp:Content>
