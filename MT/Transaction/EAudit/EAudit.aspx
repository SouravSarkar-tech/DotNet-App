<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/EAudit/EAuditMasterPage.master"
    AutoEventWireup="true" CodeFile="EAudit.aspx.cs" Inherits="Transaction_EAudit_EAudit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetToolTip(control) {
            //ddrivetipm('61', control);
            //SDT17052019
            ddrivetipm('<%=ConfigurationManager.AppSettings["Tool6163"]%>', control);
            //SDT17052019
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlAddNew" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">Manufacturer Approval Request Form
                </td>
            </tr>
            <tr>
                <td class="tdHeading" align="left" colspan="2">Corporate Vendor Group
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">From
                                <asp:Label ID="labelFrom" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:Label ID="lblFrom" runat="server" Enabled="false" Width="180" />
                            </td>
                            <td class="leftTD" style="width: 20%">To
                                <asp:Label ID="labelTo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:Label ID="lblTo" runat="server" Enabled="false" Width="180" Text="VQ Group" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <%-- <td class="leftTD" style="width: 20%">
                                Location
                                <asp:Label ID="labletxtMaterialDescription" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="false" TabIndex="1">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlLocation" runat="server" ControlToValidate="ddlLocation"
                                    ValidationGroup="EAudit" ErrorMessage="Location cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Location cannot be blank.' />" />
                            </td>--%>
                            <td class="leftTD" style="width: 20%">Date of Request
                                <asp:Label ID="lblDateOfRequest" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" colspan="3" style="width: 30%">
                                <asp:TextBox ID="txtDateOfRequest" runat="server" CssClass="textbox" MaxLength="11"
                                    Width="210px" Enabled="false" />
                                <ajax:CalendarExtender ID="caltxtDateOfReq" runat="server" TargetControlID="txtDateOfRequest"
                                    Format="dd/MM/yyyy">
                                </ajax:CalendarExtender>
                                <asp:RegularExpressionValidator ID="regtxtDateOfReq" runat="server" ControlToValidate="txtDateOfRequest"
                                    ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                    Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                    ValidationGroup="EAudit" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="reqtxtDateOfReq" runat="server" ControlToValidate="txtDateOfRequest"
                                    ValidationGroup="EAudit" ErrorMessage="Date of Request cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Date of Request cannot be blank.' />" />
                            </td>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Priority
                                <asp:Label ID="lblddlPriority" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('ddlPriority');"
                                    onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlPriority" runat="server" AppendDataBoundItems="false" TabIndex="2"
                                    OnSelectedIndexChanged="ddlPriority_SelectedIndexChanged" AutoPostBack="true" Width="210">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPriority" runat="server" ControlToValidate="ddlPriority"
                                    ValidationGroup="EAudit" ErrorMessage="Priority cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Priority cannot be blank.' />" />
                            </td>
                            <td class="tdSpace" colspan="2">
                                <asp:Label ID="lblPriorityDate" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <%--<tr>
                            <td class="leftTD" style="width: 20%">
                                Dept
                                <asp:Label ID="lblddlDept" runat="server" ForeColor="Red" Text="*" Visible= "false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="false" TabIndex="6">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Specify
                                <asp:Label ID="lblDeptSpecify" runat="server" ForeColor="Red" Text="*" Visible= "false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtDeptSpecify" runat="server" CssClass="textbox" MaxLength="20"
                                    TabIndex="7" Width="180" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="leftTD" style="width: 20%">Proposed Market
                                <asp:Label ID="labelddlMarket" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <cc1:DropDownCheckBoxes ID="ddlMarket" runat="server" AddJQueryReference="false"
                                    UseButtons="false" UseSelectAllNode="true" Style="top: 0px; left: 0px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlMarket_SelectedIndexChanged" TabIndex="3">
                                    <Style2 SelectBoxWidth="210" DropDownBoxBoxWidth="350" DropDownBoxBoxHeight="80" />
                                    <Texts SelectBoxCaption="--Select--" />
                                </cc1:DropDownCheckBoxes>
                            </td>
                            <td class="leftTD" colspan="2">
                                <asp:Label ID="lableddlMarket" runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkRefreshddlMarket" runat="server" Text="[ Refresh ]" Font-Bold="false"
                                    OnClick="lnkRefreshddlMarket_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Specify(Others)(If Not applicable mention NA)
                                <asp:Label ID="labelMarketSpec" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtMarketSpec" runat="server" CssClass="textbox" MaxLength="20"
                                    Width="210" />
                                <asp:RequiredFieldValidator ID="reqtxtMarketSpec" runat="server" ControlToValidate="txtMarketSpec"
                                    ValidationGroup="EAudit" ErrorMessage="Please specify the Proposed market." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Please specify the Proposed market.' />" />
                            </td>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="tdHeading" align="left" colspan="4">Manufacturer Name
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" width="20%">Manufacturer Code
                                <asp:Label ID="labletxtCustomerCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <%--onfocus="return VendorCodeOnFocus();"--%>
                                <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="textboxAutocomplete" MaxLength="10"
                                    AutoPostBack="true" TabIndex="4" Width="210" 
                                    OnTextChanged="txtCustomerCode_TextChanged" />
                                <asp:RequiredFieldValidator ID="reqtxtCustomerCode" runat="server" ControlToValidate="txtCustomerCode"
                                    ValidationGroup="EAudit" ErrorMessage="Manufacturer Code cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Code cannot be blank.' />" />
                            </td>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Manufacturer Name
                                <asp:Label ID="labletxtName1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtName1" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                    Enabled="false" />
                                <asp:RequiredFieldValidator ID="reqtxtName1" runat="server" ControlToValidate="txtName1"
                                    ValidationGroup="EAudit" ErrorMessage="Manufacturer Name cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Manufacturer Name cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">Manufacturer Name(conti..)
                                <asp:Label ID="labletxtName2" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtName2" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                    Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Name 3
                                <asp:Label ID="labletxtName3" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtName3" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                    Enabled="false" />
                                <%-- <asp:RequiredFieldValidator ID="reqtxtName3" runat="server" ControlToValidate="txtName3"
                                    ValidationGroup="save" ErrorMessage="Name 3 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 3 cannot be blank.' />" />--%>
                            </td>
                            <td class="leftTD" style="width: 20%">
                                <%--Name 4--%>
                                Name 4
                                <asp:Label ID="labletxtName4" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtName4" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                    Enabled="false" />
                                <%-- <asp:RequiredFieldValidator ID="reqtxtName4" runat="server" ControlToValidate="txtName4"
                                    ValidationGroup="save" ErrorMessage="Name 4 cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Name 4 cannot be blank.' />" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="tdHeading" align="left" colspan="4">Manufacturer Address
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                <%--House number and street--%>
                                Address
                                <%--<asp:Label ID="labletxtHouseNo" runat="server" ForeColor="Red" Text="*" Visible= "false"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtHouseNo" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                    Enabled="false" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                <%--Street 4--%>
                                Address 1 (Conti..)
                                <%--<asp:Label ID="labletxtStreet4" runat="server" ForeColor="Red" Text="*" Visible= "false"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtStreet4" runat="server" CssClass="textbox" MaxLength="40" Width="210"
                                    Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="6"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                <%--Street 5--%>
                                Address 2 (Conti..)
                                <%--<asp:Label ID="labletxtStreet5" runat="server" ForeColor="Red" Text="*" Visible= "false"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtStreet5" runat="server" CssClass="textbox" MaxLength="40" Width="210"
                                    Enabled="false" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                <%--Sort field--%>
                                Search Term
                                <%--<asp:Label ID="labletxtSortfield" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtSortfield" runat="server" CssClass="textbox" MaxLength="20" Width="210"
                                    Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">City
                                <%--<asp:Label ID="labletxtCity" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                    Enabled="false" />
                            </td>
                            <td class="leftTD" style="width: 20%">District
                                <%--<asp:Label ID="labletxtDistrict" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtDistrict" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                    Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Postal Code
                                <%--<asp:Label ID="labletxtPostalCode" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtPostalCode" runat="server" CssClass="textbox" MaxLength="10"
                                    Width="210" Enabled="false" />
                                <%--<asp:RequiredFieldValidator ID="reqtxtPostalCode" runat="server" ControlToValidate="txtPostalCode"
                                    ValidationGroup="save" ErrorMessage="Postal Code cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Postal Code cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtPostalCode" runat="server" ControlToValidate="txtPostalCode"
                                    ValidationExpression="^[0-9]{6}$" ValidationGroup="save" ErrorMessage="Invalid Postal Code"
                                    Enabled="true" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Postal Code.' />" />--%>
                            </td>
                            <td class="leftTD" style="width: 20%">Country Key
                                <%--<asp:Label ID="lableddlCountry" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCountry" runat="server" CssClass="textbox" MaxLength="10" Width="210"
                                    Enabled="false" />
                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" TabIndex="18"
                                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Visible="false">
                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <%-- <asp:RequiredFieldValidator ID="reqddlCountry" runat="server" ControlToValidate="ddlCountry"
                                    ValidationGroup="save" ErrorMessage="Country of origin cannot be blank."
                                    InitialValue="0" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Country of origin cannot be blank.' />" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Region (State, Province, County)
                                <%--<asp:Label ID="lableddlRegion" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtRegion" runat="server" CssClass="textbox" MaxLength="10" Width="210"
                                    Enabled="false" />
                                <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true" TabIndex="19"
                                    Visible="false">
                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <%--  <asp:RequiredFieldValidator ID="reqddlRegion" runat="server" ControlToValidate="ddlRegion"
                                    ValidationGroup="save" ErrorMessage="Region cannot be blank." InitialValue="0"
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Region cannot be blank.' />" />--%>
                            </td>
                            <td class="leftTD">Language
                                <%--<asp:Label ID="labletxtLanguage" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtLanguage" runat="server" CssClass="textboxAutocomplete" MaxLength="2"
                                    Width="210px" Enabled="false" />
                                <%-- <asp:RequiredFieldValidator ID="reqtxtLanguage" runat="server" ControlToValidate="txtLanguage"
                                    ValidationGroup="save" ErrorMessage="  Language cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title=' Language cannot be blank.' />" /> onfocus="return txtLanguageOnFocus();" onchange="return txtLanguageTextChangeEvent();"--%>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr style="display: none">
                            <td class="leftTD" style="width: 20%">PO Box
                                <%--<asp:Label ID="labletxtPOBox" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtPOBox" runat="server" CssClass="textbox" MaxLength="10" Width="210"
                                    Enabled="false" />
                                <%--<asp:RequiredFieldValidator ID="reqtxtPOBox" runat="server" ControlToValidate="txtPOBox"
                                    ValidationGroup="save" ErrorMessage="PO Box cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Number cannot be blank.' />" />--%>
                            </td>
                            <td class="leftTD" style="width: 20%">P.O. Box Postal Code
                                <%--<asp:Label ID="labletxtPOBoxPostal" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtPOBoxPostal" runat="server" CssClass="textbox" MaxLength="10"
                                    Width="210" Enabled="false" />
                                <%--  <asp:RequiredFieldValidator ID="reqtxtPOBoxPostal" runat="server" ControlToValidate="txtPOBoxPostal"
                                    ValidationGroup="save" ErrorMessage="PO Box cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='P.O. Box Postal Code cannot be blank.' />" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="tdHeading" align="left" colspan="4">List of material supplied
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div style="width: 860px; height: 200px; overflow-x: auto; border-style: ridge">
                                    <asp:GridView ID="grdMaterials" runat="server" AutoGenerateColumns="false" DataKeyNames="SerialNo,Pharmacopical_Status,AnalysisMethod,MaterialCategory,LupinLoc"
                                        CssClass="GridClass" ShowHeaderWhenEmpty="true" Visible="true" ShowFooter="false"
                                        AllowSorting="true" OnRowDataBound="grdMaterials_RowDataBound" OnRowCommand="grdMaterials_RowCommand">
                                        <%--OnRowCommand="grdBOMDetailAdd_RowCommand"
                                    OnRowDataBound="grdBOMDetailAdd_RowDataBound"--%>
                                        <FooterStyle CssClass="gridFooter" />
                                        <RowStyle CssClass="light-gray" />
                                        <HeaderStyle CssClass="gridHeader" />
                                        <AlternatingRowStyle CssClass="gridRowStyle" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="D" CausesValidation="false">  
                                                <img src="../../images/delete.png" alt="Delete" title='Delete'/></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblE_Audit_Material_Id" runat="server" Text='<%# Eval("E_Audit_Material_Id") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSNo" runat="server" Text='<%# Bind("SerialNo") %>' Enabled="false"
                                                        Width="10"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMatName" runat="server" Text='<%# Bind("Material_Name") %>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqtxtMatName" runat="server" ControlToValidate="txtMatName"
                                                        ValidationGroup="EAudit" ErrorMessage="Material Name cannot be blank." SetFocusOnError="true"
                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Name cannot be blank.' />" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lupin's Product Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtProdName" runat="server" Text='<%# Bind("Product_Name") %>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqtxtProdName" runat="server" ControlToValidate="txtProdName"
                                                        ValidationGroup="EAudit" ErrorMessage="Product Name cannot be blank." SetFocusOnError="true"
                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Product Name cannot be blank.' />" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lupin's Location" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%--<asp:DropDownList ID="ddlLupinLoc" runat="server" DataTextField="LookUp_Desc" DataValueField="LookUp_Code">
                                                </asp:DropDownList>--%>
                                                    <cc1:DropDownCheckBoxes ID="ddlLupinLoc" runat="server" AddJQueryReference="false"
                                                        UseButtons="false" UseSelectAllNode="true" Style="top: 0px; left: 0px" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlLupinLoc_SelectedIndexChanged" TabIndex="3" DataTextField="LookUp_Desc" DataValueField="LookUp_Code">
                                                        <Style2 SelectBoxWidth="300" DropDownBoxBoxWidth="300" DropDownBoxBoxHeight="80" />
                                                        <Texts SelectBoxCaption="--Select--" />
                                                    </cc1:DropDownCheckBoxes>
                                                    <cc1:ExtendedRequiredFieldValidator ID="reqddlLupinLoc" runat="server" ControlToValidate="ddlLupinLoc"
                                                        ValidationGroup="EAudit" ErrorMessage="Lupin location cannot be blank." SetFocusOnError="true"
                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Lupin location cannot be blank.' />"></cc1:ExtendedRequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other Location" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOtherLC" runat="server" Text='<%# Bind("Other_LupinLoc") %>' Enabled="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqtxtOtherLC" runat="server" ControlToValidate="txtOtherLC"
                                                        ValidationGroup="EAudit" ErrorMessage="Product Name cannot be blank." SetFocusOnError="true"
                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Product Name cannot be blank.' />" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pharmacopical Status" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <cc1:DropDownCheckBoxes ID="ddlPharmaStatus" runat="server" AddJQueryReference="false" AutoPostBack="true"
                                                        UseButtons="false" UseSelectAllNode="true" Style="top: 0px; left: 0px" OnSelectedIndexChanged="ddlPharmaStatus_SelectedIndexChanged">
                                                        <Style2 SelectBoxWidth="300" DropDownBoxBoxWidth="300" DropDownBoxBoxHeight="80" />
                                                        <Texts SelectBoxCaption="--Select--" />
                                                    </cc1:DropDownCheckBoxes>
                                                    <cc1:ExtendedRequiredFieldValidator ID="reqddlPharmaStatus" runat="server" ControlToValidate="ddlPharmaStatus"
                                                        ValidationGroup="EAudit" ErrorMessage="Pharmacopical Status cannot be blank." SetFocusOnError="true"
                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Pharmacopical Status cannot be blank.' />"></cc1:ExtendedRequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other Pharmacopical Status" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOtherPharmaStatus" runat="server" Text='<%# Bind("Other_Pharmacopical_Status") %>' Enabled="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqOtherPharmaStatus" runat="server" ControlToValidate="txtOtherPharmaStatus"
                                                        ValidationGroup="EAudit" ErrorMessage="Product Name cannot be blank." SetFocusOnError="true" Enabled="false"
                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Product Name cannot be blank.' />" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pharmacopical Status" HeaderStyle-HorizontalAlign="Left"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPharmacopical_Status" runat="server" Text='<%# Eval("Pharmacopical_Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Spec & MOA" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%--<asp:DropDownList ID="ddlMthdAnalysis" runat="server" DataTextField="LookUp_Desc"
                                                    DataValueField="LookUp_Code">
                                                </asp:DropDownList>--%>
                                                    <cc1:DropDownCheckBoxes ID="ddlMthdAnalysis" runat="server" AddJQueryReference="false" AutoPostBack="true"
                                                        UseButtons="false" UseSelectAllNode="true" Style="top: 0px; left: 0px" OnSelectedIndexChanged="ddlMthdAnalysis_SelectedIndexChanged">
                                                        <Style2 SelectBoxWidth="300" DropDownBoxBoxWidth="300" DropDownBoxBoxHeight="80" />
                                                        <Texts SelectBoxCaption="--Select--" />
                                                    </cc1:DropDownCheckBoxes>
                                                    <cc1:ExtendedRequiredFieldValidator ID="reqddlMthdAnalysis" runat="server" ControlToValidate="ddlMthdAnalysis"
                                                        ValidationGroup="EAudit" ErrorMessage="Spec & MOA cannot be blank." SetFocusOnError="true"
                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Spec & MOA cannot be blank.' />"></cc1:ExtendedRequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other Spec & MOA" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOtherMthdAnalysis" runat="server" Text='<%# Bind("Other_AnalysisMethod") %>' Enabled="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqtxtOtherMthdAnalysis" runat="server" ControlToValidate="txtOtherMthdAnalysis"
                                                        ValidationGroup="EAudit" ErrorMessage="Product Name cannot be blank." SetFocusOnError="true" Enabled="false"
                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Product Name cannot be blank.' />" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Category" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlMatCategory" runat="server" DataTextField="LookUp_Desc"
                                                        DataValueField="LookUp_Code" AutoPostBack="true" OnSelectedIndexChanged="ddlMatCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqddlMatCategory" runat="server" ControlToValidate="ddlMatCategory"
                                                        ValidationGroup="EAudit" ErrorMessage="Material Category cannot be blank." SetFocusOnError="true"
                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Category cannot be blank.' />"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other Material Category" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOtherMatCategory" runat="server" Text='<%# Bind("Other_MaterialCategory") %>' Enabled="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqtxtOtherMatCategory" runat="server" ControlToValidate="txtOtherMatCategory"
                                                        ValidationGroup="EAudit" ErrorMessage="Product Name cannot be blank." SetFocusOnError="true" Enabled="false"
                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Product Name cannot be blank.' />" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUpd_Flag" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr id="trAddNewMatRow" runat="server" visible="false" align="left">
                            <td class="centerTD" colspan="2">
                                <asp:TextBox ID="txtNewRow" runat="server" Text="1" MaxLength="3" Width="20px" CssClass="textbox" />
                                <asp:RangeValidator ID="rangePositionNumber" runat="server" ValidationGroup="addRowValidation"
                                    ControlToValidate="txtNewRow" MaximumValue="100" MinimumValue="1" Type="Integer"
                                    ErrorMessage="Enter Numeric Value only (Maximum limit 100)." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Enter Numeric Value only (Maximum limit 100).' />"></asp:RangeValidator>
                                <asp:Button ID="btnAdd" runat="server" Text="New Row" ValidationGroup="addRowValidation"
                                    OnClick="btnAdd_Click" CssClass="button" UseSubmitBehavior="false" />
                                <asp:Label ID="lblgridmsg" runat="server" ForeColor="Red" Visible="False" />
                                <%--OnClick="btnAdd_Click"--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <%-- <tr>
                            <td class="tdHeading" align="left" colspan="4">
                                Quality Agreement
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Is the manufacturer ready to sign-off quality agreement
                                <asp:Label ID="lblQAgreement" runat="server" ForeColor="Red" Text="*" Visible="true"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlQAgreement" runat="server" AutoPostBack="true" 
                                    TabIndex="18" OnSelectedIndexChanged="ddlQAgreement_SelectedIndexChanged">
                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="NA" Value="NA"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlQAgreement" runat="server" ControlToValidate="ddlQAgreement"
                                    ValidationGroup="EAudit" ErrorMessage="Please mention whether manufacturer ready to sign off Quality Agreement." SetFocusOnError="true"
                                    Enabled="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please mention whether manufacturer ready to sign off Quality Agreement.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                If No, specify justification
                                <asp:Label ID="lblJustification" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtJustification" runat="server" CssClass="textarea" TextMode="MultiLine"
                                    TabIndex="26" Columns="100" Rows="3" Enabled="false" />
                                <asp:RequiredFieldValidator ID="reqtxtJustification" runat="server" ControlToValidate="txtJustification"
                                    ValidationGroup="EAudit" ErrorMessage="Please specify justification." SetFocusOnError="true"
                                    Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please specify justification.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr id ="trRnD" runat="server">
                            <td colspan="4">
                                <table>
                                    <tr>
                                        <td class="tdHeading" align="left" colspan="4">To be filled by R&D Team
                                        </td>
                                    </tr>
                                    <tr>


                                        <td class="leftTD" style="width: 20%">Requirement of DMF
                                <asp:Label ID="labelRNDDMF" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtRNDDMF" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                                Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtRNDDMF" runat="server" ControlToValidate="txtRNDDMF"
                                                Enabled="false" ValidationGroup="EAudit" ErrorMessage="Please mention Requirement of DMF."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please mention Requirement of DMF.' />" />
                                        </td>
                                        <td class="tdSpace" colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">Category of redefined material
                                <asp:Label ID="labelRNDRedefinedCat" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtRNDRedefinedCat" runat="server" CssClass="textbox" Width="210" MaxLength="35"
                                                Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtRNDRedefinedCat" runat="server" ControlToValidate="txtRNDRedefinedCat"
                                                ValidationGroup="EAudit" ErrorMessage="Please mention category of redefined material."
                                                SetFocusOnError="true" Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please mention category of redefined material.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">Whether audit is needed
                                <asp:Label ID="labelRNDAuditReq" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtRNDAuditReq" runat="server" CssClass="textbox" Width="210" MaxLength="35"
                                                Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqRNDtxtAuditReq" runat="server" ControlToValidate="txtRNDAuditReq"
                                                ValidationGroup="EAudit" ErrorMessage="Please whether audit is needed." SetFocusOnError="true"
                                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please whether audit is needed.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">Do R&D will join in audit
                                <asp:Label ID="labelRND" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtRND" runat="server" CssClass="textbox" Width="210" MaxLength="35"
                                                Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtRND" runat="server" ControlToValidate="txtRND"
                                                ValidationGroup="EAudit" ErrorMessage="Please mention if RA will join in audit."
                                                SetFocusOnError="true" Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please mention if RA will join in audit.' />" />
                                        </td>


                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">R&D Comments (If Not applicable mention NA)
                                <asp:Label ID="lblRNDComments" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtRNDComments" runat="server" CssClass="textarea" TextMode="MultiLine"
                                                TabIndex="26" Columns="100" Rows="3" Enabled="false" Width="210px" />
                                            <asp:RequiredFieldValidator ID="reqtxtRNDComments" runat="server" ControlToValidate="txtRNDComments"
                                                ValidationGroup="EAudit" ErrorMessage="Please mention RND Comments." SetFocusOnError="true"
                                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please mention RND Comments.' />" />
                                        </td>

                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id ="trRA" runat="server">
                            <td colspan="4">
                                <table>


                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="tdHeading" align="left" colspan="4">To be filled by RA Team
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">Requirement of DMF
                                <asp:Label ID="labelDMF" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtDMF" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                                Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtDMF" runat="server" ControlToValidate="txtDMF"
                                                Enabled="false" ValidationGroup="EAudit" ErrorMessage="Please mention Requirement of DMF."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please mention Requirement of DMF.' />" />
                                        </td>
                                        <%--<td class="leftTD" style="width: 20%; display: none">Material Type
                                <asp:Label ID="labelMatType" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%; display: none">
                                <asp:TextBox ID="txtMatType" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                    Enabled="false" />
                                <asp:RequiredFieldValidator ID="reqtxtMatType" runat="server" ControlToValidate="txtMatType"
                                    Enabled="false" ValidationGroup="EAudit" ErrorMessage="Please mention Material Type."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please mention Material Type.' />" />
                            </td>--%>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <%--<td class="leftTD" style="width: 20%">Is the materials category modification which defined earlier need to be updated
                                <asp:Label ID="labelModReq" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtModReq" runat="server" CssClass="textbox" MaxLength="35" Width="210"
                                    Enabled="false" />
                                <asp:RequiredFieldValidator ID="reqtxtModReq" runat="server" ControlToValidate="txtModReq"
                                    Enabled="false" ValidationGroup="EAudit" ErrorMessage="Please whether Material category modification required."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please whether Material category modification required.' />" />
                            </td>--%>
                                        <td class="leftTD" style="width: 20%">Category of redefined material
                                <asp:Label ID="labelRedefinedCat" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtRedefinedCat" runat="server" CssClass="textbox" Width="210" MaxLength="35"
                                                Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtRedefinedCat" runat="server" ControlToValidate="txtRedefinedCat"
                                                ValidationGroup="EAudit" ErrorMessage="Please mention category of redefined material."
                                                SetFocusOnError="true" Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please mention category of redefined material.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdSpace" colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">Whether audit is needed
                                <asp:Label ID="labelAuditReq" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtAuditReq" runat="server" CssClass="textbox" Width="210" MaxLength="35"
                                                Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtAuditReq" runat="server" ControlToValidate="txtAuditReq"
                                                ValidationGroup="EAudit" ErrorMessage="Please whether audit is needed." SetFocusOnError="true"
                                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please whether audit is needed.' />" />
                                        </td>
                                        <td class="leftTD" style="width: 20%">Do RA will join in audit
                                <asp:Label ID="labelRA" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtRA" runat="server" CssClass="textbox" Width="210" MaxLength="35"
                                                Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtRA" runat="server" ControlToValidate="txtRA"
                                                ValidationGroup="EAudit" ErrorMessage="Please mention if RA will join in audit."
                                                SetFocusOnError="true" Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please mention if RA will join in audit.' />" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD" style="width: 20%">RA Comments (If Not applicable mention NA)
                                <asp:Label ID="lblRAComments" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                                        </td>
                                        <td class="rigthTD" style="width: 30%">
                                            <asp:TextBox ID="txtRAComments" runat="server" CssClass="textarea" TextMode="MultiLine"
                                                TabIndex="26" Columns="100" Rows="3" Enabled="false" Width="210px" />
                                            <asp:RequiredFieldValidator ID="reqtxtRAComments" runat="server" ControlToValidate="txtRAComments"
                                                ValidationGroup="EAudit" ErrorMessage="Please mention RND Comments." SetFocusOnError="true"
                                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please mention RND Comments.' />" />
                                        </td>

                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="tdHeading" align="left" colspan="4">Vendor Contact Details
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Contact Person Name
                                <asp:Label ID="labeltxtContactName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtContactName" runat="server" CssClass="textbox" MaxLength="35"
                                    Width="210" TabIndex="5" />
                                <asp:RequiredFieldValidator ID="reqtxtContactName" runat="server" ControlToValidate="txtContactName"
                                    ValidationGroup="EAudit" ErrorMessage="Contact Person Name cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Contact Person Name cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">Mobile Number
                                <asp:Label ID="labletxtMobileNum" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtcountrymcode" runat="server" CssClass="textbox" MaxLength="3" Width="30"
                                    TabIndex="13" onkeypress="return isNumberKey(event,this)" />
                                <asp:RequiredFieldValidator ID="reqcountrymcode" runat="server" ControlToValidate="txtcountrymcode"
                                    Visible="false" ValidationGroup="EAudit" ErrorMessage=" Extension cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Mobile number cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regcountrymcode" runat="server" ControlToValidate="txtcountrymcode"
                                    ValidationExpression="^[0-9]{2,3}$" ValidationGroup="EAudit" ErrorMessage="Invalid Mobile Number"
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Extension.' />" />


                                <asp:TextBox ID="txtMobileNum" runat="server" CssClass="textbox" MaxLength="10" Width="100"
                                    TabIndex="6" onkeypress="return isNumberKey(event,this)" />
                                <asp:RequiredFieldValidator ID="reqtxtMobileNum" runat="server" ControlToValidate="txtMobileNum"
                                    Visible="false" ValidationGroup="EAudit" ErrorMessage=" Mobile number cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Mobile number cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtMobileNum" runat="server" ControlToValidate="txtMobileNum"
                                    ValidationExpression="^[0-9]{10}$" ValidationGroup="EAudit" ErrorMessage="Invalid Mobile Number"
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Mobile Number.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Landline Number
                                <asp:Label ID="labletxttelephone" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txttelephone" runat="server" CssClass="textbox" MaxLength="16" TabIndex="7"
                                    Width="209px" onkeypress="return isNumberKey(event,this)" />
                                <asp:RequiredFieldValidator ID="reqtxttelephone" runat="server" ControlToValidate="txttelephone"
                                    ValidationGroup="EAudit" ErrorMessage=" Landline number cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title=' Landline number cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">E-Mail Address
                                <asp:Label ID="labletxtEmailAddress" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="textbox" Width="210px"
                                    TabIndex="8" MaxLength="241" />
                                <asp:RequiredFieldValidator ID="reqtxtEmailAddress" runat="server" ControlToValidate="txtEmailAddress"
                                    ValidationGroup="EAudit" ErrorMessage="E-Mail Address cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='E-Mail Address cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtEmailAddress" runat="server" ControlToValidate="txtEmailAddress"
                                    ValidationExpression="^([\w-\.]+)[\w]{1}@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                    ValidationGroup="EAudit" ErrorMessage="Invalid E-Mail Address" SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid E-Mail Address.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Reason for Audit
                                <asp:Label ID="labelddlReasonAudit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlReasonAudit" runat="server" AppendDataBoundItems="false"
                                    TabIndex="9" OnSelectedIndexChanged="ddlReasonAudit_SelectedIndexChanged" AutoPostBack="true" Width="210">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlReasonAudit" runat="server" ControlToValidate="ddlReasonAudit"
                                    ValidationGroup="EAudit" ErrorMessage="Please Provide Reason for Audit." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Please Provide Reason for Audit.' />"></asp:RequiredFieldValidator>
                            </td>
                            <td class="leftTD" style="width: 20%;">Specify(Others)
                                <asp:Label ID="labeltxtAuditSpec" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtAuditSpec" runat="server" CssClass="textarea" TextMode="MultiLine" TabIndex="10"
                                    Width="210px" Visible="false" Columns="100" Rows="3" />

                                <asp:RequiredFieldValidator ID="reqtxtAuditSpec" runat="server" ControlToValidate="txtAuditSpec"
                                    ValidationGroup="EAudit" ErrorMessage="Please specify the reason for audit."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please specify the reason for audit.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%; display: none">Previous Approval status (if applicable)
                                <asp:Label ID="label1" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtApprovalStatus" runat="server" CssClass="textbox" MaxLength="20"
                                    TabIndex="11" Width="210px" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Remarks(If Not applicable mention NA)
                                <asp:Label ID="labletxtRemarks" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="textarea" TextMode="MultiLine"
                                    TabIndex="12" Columns="100" Rows="3" Width="210px" />
                                <asp:RequiredFieldValidator ID="RequiredtxtRemarks" runat="server" ControlToValidate="txtRemarks"
                                    ValidationGroup="EAudit" ErrorMessage="Please specify Remark."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please specify Remark.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="rigthTD">
                                <asp:Label ID="lblCQAComm" runat="server" Text="CQA Rremark" Font-Bold="true" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:Label ID="lblCQAComm_Desc" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" align="left" colspan="4">
                                <b>Attach Documents (Image/PDF Files Only)</b>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td class="rigthTD" align="left" colspan="2" valign="top">
                                <div>
                                    <asp:FileUpload ID="file_upload" class="multi" runat="server" />
                                    <asp:Button ID="btnupload" runat="server" Text="Upload" Height="23px" OnClick="btnupload_Click" Visible="false" />

                                    <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />
                                    <asp:RequiredFieldValidator ID="Requiredfile_upload" runat="server" ControlToValidate="file_upload" ValidationGroup="EAudit" ErrorMessage="Please Upload Document"
                                        SetFocusOnError="true" Display="Dynamic" Enabled="false" Text="<img src='../../images/Error.png' title='Please specify Remark.' />" />
                                </div>
                            </td>
                            <td class="rigthTD" align="left" colspan="2">
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
                        <tr>
                            <td colspan="4" class="tdSpace"></td>
                        </tr>
                        <tr>
                            <%--id="trButton" runat="server" visible="false"--%>
                            <td class="centerTD" colspan="4">
                                <%--<asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="BasicData"
                                    TabIndex="26" CssClass="button" OnClick="btnPrevious_Click" />--%>
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="EAudit" UseSubmitBehavior="true"
                                    Text="Save" CssClass="button" TabIndex="13" OnClick="btnSave_Click" Visible="false" />
                                <asp:Button ID="btnNext" runat="server" Text="Submit" TabIndex="14" CssClass="button"
                                    OnClick="btnNext_Click" UseSubmitBehavior="true" Visible="false" Enabled="false"
                                    ValidationGroup="EAudit" />
                                <%-- <asp:Button ID="btnNext" runat="server" ValidationGroup="BasicData" Text="Save & Next"
                                    TabIndex="28" UseSubmitBehavior="true" CssClass="button" OnClick="btnNext_Click"
                                    Width="120px" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="save" runat="server" ValidationGroup="EAudit" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:ValidationSummary ID="VaSu" runat="server" ShowMessageBox="true" ShowSummary="false"
        ValidationGroup="addRowValidation" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblEAuditId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <%--<asp:Label ID="lblSectionId" runat="server" Text="61" Visible="false" />--%>
    <asp:Label ID="lblSectionId" runat="server" Text="61" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <script type="text/javascript">
        function ActivateName() {

            if ($('#<%= txtName1.ClientID%>').val().length > 0)
                $('#<%= txtName2.ClientID%>').attr('disabled', false);
        }
        function ActivateAddress() {

            if ($('#<%= txtHouseNo.ClientID%>').val().length > 0)
                $('#<%= txtStreet4.ClientID%>').attr('disabled', false);

            if ($('#<%= txtStreet4.ClientID%>').val().length > 0)
                $('#<%= txtStreet5.ClientID%>').attr('disabled', false);


        }
        function VendorCodeOnFocus() {
            textboxId = $('#<%= txtCustomerCode.ClientID%>').attr('ID');
            AutoCompleteVendorName();
        }

        function isNumberKey(evt, ctrlName) {
            var str = ctrlName.value;
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 && charCode != 46) || charCode > 57) {
                return false;
            }
            else if (charCode == 46 && (str.indexOf(".") != -1)) {
                return false;
            }
            else {
                return true;
            }
        }

        function RequestPage() {
            window.location.href = "EAuditMaster.aspx";
        }

    </script>
</asp:Content>
