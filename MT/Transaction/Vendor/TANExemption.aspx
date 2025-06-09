<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Vendor/VendorMasterPage.master" AutoEventWireup="true" CodeFile="TANExemption.aspx.cs" Inherits="Transaction_Vendor_TANExemption" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlAddNew" runat="server">

        <div id="divmainPopUp" runat="server" clientidmode="Static">
            <table border="0" cellpadding="0" cellspacing="1" width="100%">

                <tr>
                    <td class="trHeading" align="center" colspan="4">TAN – Based Exemption
                    </td>
                </tr>

                <tr>
                    <td class="tdSpace" colspan="4"></td>
                </tr>

                <tr>
                    <td class="leftTD" width="100%" colspan="4">

                        <asp:TextBox ID="txtNewRow" runat="server" Text="2" MaxLength="3" Width="20px"
                            Enabled="false" CssClass="textbox" Style="display: none;" />
                        <asp:RangeValidator ID="ranvtxtNewRow" runat="server" ValidationGroup="addRowValidation"
                            ControlToValidate="txtNewRow" MaximumValue="20" MinimumValue="1" Type="Integer"
                            ErrorMessage="Enter Numeric Value only (Maximum limit 20)." SetFocusOnError="true"
                            Display="Dynamic"
                            Text="<img src='../../images/Error.png' title='Enter Numeric Value only (Maximum limit 20).' />"></asp:RangeValidator>

                        <asp:Button ID="btnaddRow" runat="server" Text="Add New Row" ValidationGroup="addRowValidation"
                            OnClick="btnaddRow_Click" CssClass="button" UseSubmitBehavior="false" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td class="tdSpace" colspan="4"></td>
                </tr>

                <tr>
                    <td colspan="4">

                        <asp:GridView ID="grdDetailAdd" runat="server" AutoGenerateColumns="false"
                            DataKeyNames="Pk_TANId,sSectionCode,sExemptNum,sExemptReason,sWHTType,sWtaxCode"
                            Width="1000px" CssClass="GridClass" ShowHeaderWhenEmpty="true" Visible="true"
                            ShowFooter="false" AllowSorting="true"
                            OnRowCommand="grdDetailAdd_RowCommand"
                            OnRowDataBound="grdDetailAdd_RowDataBound">
                            <FooterStyle CssClass="gridFooter" />
                            <RowStyle CssClass="light-gray" />
                            <HeaderStyle CssClass="gridHeader" />
                            <AlternatingRowStyle CssClass="gridRowStyle" />
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Remove">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Pk_TANId") %>' CommandName="D">  
                                                 <img src="../../images/delete.png" alt="Delete" title='Delete' Width="20px"
                                                     OnClientClick="return confirm('Are you certain you want to delete this record?');" /></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPk_TANId" runat="server" Text='<%#Eval("Pk_TANId") %>'
                                            Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderText="Section Code">
                                    <ItemTemplate>

                                        <asp:DropDownList ID="ddlsSectionCode" runat="server" AppendDataBoundItems="false" Width="80px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlsSectionCode" runat="server" ControlToValidate="ddlsSectionCode"
                                            ValidationGroup="save" ErrorMessage="Select the Section Code." SetFocusOnError="true"
                                            InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select the Section Code.' />" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderText="Exemption Certificate No">
                                    <ItemTemplate>

                                        <asp:DropDownList ID="ddlsExemptNum" runat="server" AppendDataBoundItems="false" Width="110px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlsExemptNum" runat="server" ControlToValidate="ddlsExemptNum"
                                            ValidationGroup="save" ErrorMessage="Select the exemption certificate number." SetFocusOnError="true"
                                            InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select the exemption certificate number.' />" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderText="Exemption Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtsExemptRate" runat="server" CssClass="textbox" Text='<%#Eval("sExemptRate") %>'
                                            Width="70px" MaxLength="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqtxtsExemptRate" runat="server" ControlToValidate="txtsExemptRate"
                                            ValidationGroup="save" ErrorMessage="Exemption Ratecannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Exemption Rate cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="regtxtsRece_plant" runat="server" ControlToValidate="txtsExemptRate"
                                            ValidationGroup="save" ErrorMessage="Invalid Exemption Rate." SetFocusOnError="true"
                                            ValidationExpression="^[0-9]*(\.[0-9]{0,2})?$" Display="Dynamic"
                                            Text="<img src='../../images/Error.png' title='Invalid Exemption Rate.' />" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderText="Exemption From">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtdExemptFrom" runat="server" Text='<%#Eval("dExemptFrom") %>' CssClass="textbox"
                                            MaxLength="10" Width="65px" />
                                        <act:CalendarExtender ID="caltxtdExemptFrom" runat="server" TargetControlID="txtdExemptFrom"
                                            PopupPosition="BottomLeft" Format="dd/MM/yyyy" Animated="true">
                                        </act:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="regtxtdExemptFrom" runat="server" ControlToValidate="txtdExemptFrom"
                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                            ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1}))|([0-9]{2}))([0-9]{2}))))$)"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderText="Exemption To">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtdExemptTo" runat="server" Text='<%#Eval("dExemptTo") %>' CssClass="textbox"
                                            MaxLength="10" Width="65px" />
                                        <act:CalendarExtender ID="caltxtdExemptTo" runat="server" TargetControlID="txtdExemptTo"
                                            PopupPosition="BottomLeft" Format="dd/MM/yyyy" Animated="true">
                                        </act:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="regtxtdExemptTo" runat="server" ControlToValidate="txtdExemptTo"
                                            ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                            Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                            ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1}))|([0-9]{2}))([0-9]{2}))))$)"></asp:RegularExpressionValidator>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderText="Exemption Reason">
                                    <ItemTemplate>

                                        <asp:DropDownList ID="ddlsExemptReason" runat="server" AppendDataBoundItems="false" Width="110px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqsExemptReason" runat="server" ControlToValidate="ddlsExemptReason"
                                            ValidationGroup="save" ErrorMessage="Select the exemption reason." SetFocusOnError="true"
                                            InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select the exemption reason.' />" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderText="Withhld Tax Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlsWHTType" runat="server" AppendDataBoundItems="false" Width="100px" OnSelectedIndexChanged="ddlsWHTType_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlsWHTType" runat="server" ControlToValidate="ddlsWHTType"
                                            ValidationGroup="save" ErrorMessage="Select the Withhld Tax Type." SetFocusOnError="true"
                                            InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select the Withhld Tax Type.' />" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderText="W/Tax Code">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlsWtaxCode" runat="server" AppendDataBoundItems="false" Width="100px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlsWtaxCode" runat="server" ControlToValidate="ddlsWtaxCode"
                                            ValidationGroup="save" ErrorMessage="Select the W/Tax Code." SetFocusOnError="true"
                                            InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select the W/Tax Code.' />" />


                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderText="Exem Threshold">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtsExemThreshold" runat="server" CssClass="textbox" Text='<%#Eval("sExemThreshold") %>'
                                            Width="80px" MaxLength="10" onkeypress="return IsNumber();" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqtxtsExemThreshold" runat="server" ControlToValidate="txtsExemThreshold"
                                            ValidationGroup="save" ErrorMessage="Exemption Threshold cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Exemption Threshold cannot be blank.' />" />

                                        <asp:RegularExpressionValidator runat="server" ID="txtregpre1"
                                            ValidationGroup="save" Display="Dynamic" ErrorMessage="Invalid Exemption Threshold."
                                            ControlToValidate="txtsExemThreshold"
                                            Text="<img src='../../images/Error.png' title='Invalid Exemption Threshold.' />"
                                            ValidationExpression="^[\d]{1,10}$"></asp:RegularExpressionValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderText="Currency " >
                                    <ItemTemplate>
                                       <asp:TextBox ID="txtsCurrency" runat="server" CssClass="textbox" Text='<%#Eval("sCurrency") %>'
                                            Width="40px" Enabled="false"></asp:TextBox>
                                        
                                        <%-- <asp:RegularExpressionValidator ID="regtxtsCurrency" runat="server" ControlToValidate="txtsCurrency"
                                                    ValidationGroup="save" ErrorMessage="Invalid Material Code." SetFocusOnError="true"
                                                    ValidationExpression="^[\d]{6,10}$" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Material Code.' />" />--%>

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
                    <td class="centerTD" colspan="2">
                        <asp:Button ID="btnPrevious" runat="server" Text="Back" TabIndex="26"
                            CssClass="button" OnClick="btnPrevious_Click" />
                        <asp:Button ID="btnSave" runat="server" UseSubmitBehavior="true" ValidationGroup="save"
                            Text="Save" CssClass="button" TabIndex="27" OnClick="btnSave_Click" />
                        <asp:Button ID="btnNext" runat="server" Text="Save & Next"
                            TabIndex="28" UseSubmitBehavior="true" CssClass="button" OnClick="btnNext_Click"
                            Width="120px" />
                    </td>
                </tr>

                <tr>
                    <td colspan="4" class="tdSpace"></td>
                </tr>

            </table>
        </div>
        <asp:ValidationSummary ID="VaSu" runat="server" ShowMessageBox="true" ShowSummary="false"
            ValidationGroup="addRowValidation" />
        <asp:ValidationSummary ID="VaSu1" runat="server" ShowMessageBox="true" ShowSummary="false"
            ValidationGroup="save" />

        <asp:Label ID="lblUserId" runat="server" Visible="false" />
        <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
        <asp:Label ID="lblMode" runat="server" Visible="false" />
        <asp:Label ID="lblModuleId" runat="server" Visible="false" />
        <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        <asp:Label ID="lblSectionId" runat="server" Text="108" Visible="false" />
        <asp:Label ID="lblFlag" runat="server" Text="" Visible="false" />
        <asp:Label ID="lblReqStatus" runat="server" Text="" Visible="false" />
    </asp:Panel>

</asp:Content>

