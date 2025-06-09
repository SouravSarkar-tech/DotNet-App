<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/MasterPage.master"
    AutoEventWireup="true" CodeFile="CustomerMaster.aspx.cs" Inherits="Transaction_Customer_CustomerMaster" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <link href="../../stylesheet/main.css" rel="stylesheet" type="text/css" />
    <link href="../../stylesheet/autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="../../stylesheet/Paging.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlSearch" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="4">Customer Master
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left" style="width: 25%">Request No
                        </td>
                        <td class="rigthTD" align="left" style="width: 25%">
                            <asp:TextBox ID="txtRequestNo" runat="server" CssClass="textbox" />
                        </td>
                        <td class="leftTD" align="left" style="width: 25%">SAP Code
                        </td>
                        <td class="rigthTD" align="left" style="width: 25%">
                            <asp:TextBox ID="txtSAPCode" runat="server" CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left">Module
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:DropDownList ID="ddlModuleSearch" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="All" Value="0" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModuleSearch"
                                ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Module.' />" />
                        </td>
                        <td class="leftTD" align="left">Status
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Pending For My Approval" Value="P" />
                                <asp:ListItem Text="Rollbacked To Me" Value="R" />
                                <asp:ListItem Text="Created By Me" Value="C" />
                                <asp:ListItem Text="Incomplete Request" Value="I" />
                                <asp:ListItem Text="Rejected By Me" Value="REJ" />
                                <asp:ListItem Text="Approved" Value="ALL" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left">From Date
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" />
                            <act:CalendarExtender ID="CaltxtFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFromDate"
                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                        </td>
                        <td class="leftTD" align="left">To Date
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" />
                            <act:CalendarExtender ID="CaltxtToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtToDate"
                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace"></td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="4">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                            <script type="text/javascript">
                                function CheckOtherIsCheckedByGVID(spanChk) {

                                    var CurrentRdbID = spanChk.id;
                                    var Chk = spanChk;
                                    Parent = document.getElementById("<%=grdSearch.ClientID%>");
                                    var items = Parent.getElementsByTagName('input');
                                    for (i = 0; i < items.length; i++) {
                                        if (items[i].id != CurrentRdbID && items[i].type == "radio") {
                                            if (items[i].checked) {
                                                items[i].checked = false;
                                            }
                                        }
                                    }
                                }
                            </script>
                            <asp:GridView ID="grdSearch" runat="server" AutoGenerateColumns="false" Width="100%"
                                BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                                OnPageIndexChanging="grdSearch_PageIndexChanging" GridLines="Both">
                                <RowStyle CssClass="light-gray" />
                                <HeaderStyle CssClass="gridHeader" />
                                <AlternatingRowStyle CssClass="gridRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdoSelection" runat="server" GroupName="selection" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrimaryID" runat="server" Text='<%# Eval("Master_Header_Id") %>'></asp:Label>
                                            <asp:Label ID="lblModuleId" runat="server" Text='<%# Eval("Module_Id") %>'></asp:Label>
                                            <asp:Label ID="lblModuleName" runat="server" Text='<%# Eval("Module_Name") %>'></asp:Label>
                                            <asp:Label ID="lblRequestNo" runat="server" Text='<%# Eval("Request_No") %>'></asp:Label>
                                            <asp:Label ID="lblMasterCode" runat="server" Text='<%# Eval("Customer_Code") %>'></asp:Label>
                                            <asp:Label ID="lblActionType" runat="server" Text='<%# Eval("Action_Type") %>'></asp:Label>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("Created_By") %>'></asp:Label>
                                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                            <asp:Label ID="lblContactNo" runat="server" Text='<%# Eval("ContactNo") %>'></asp:Label>
                                            <%--SDT29052019--%>
                                            <asp:Label ID="lblDivisionType" runat="server" Text='<%# Eval("DivisionType") %>'></asp:Label>
                                            <%--EDT29052019--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Request No." ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            &nbsp;&nbsp;<asp:LinkButton ID="lnkWorkflow" runat="server" Text='<%# Eval("Request_No") %>'
                                                OnClientClick='<%# string.Format( "OpenRequestHistory(\"{0}\");", Eval("Master_Header_Id")) %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Customer Code" DataField="Customer_Code" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Customer Acc. Grp" DataField="Module_Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Customer" DataField="Name1" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Created By" DataField="Created_By" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Req. Dt." DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Location" DataField="Location" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Cost Center" DataField="Cost_Centre" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" Visible="false"
                                        HeaderText="Remarks">
                                        <ItemTemplate>
                                            <a href="#" onclick="return ShowRejectionNote('<%#Eval("Remarks") %>')" style="color: #1540c2">Rejection Note</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" HeaderText="Status">
                                        <ItemTemplate>
                                            <%#Eval("Status") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Actioned By" DataField="Actioned_By" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Date" DataField="Actioned_On" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </asp:GridView>
                            <div id="divRejectionNoteContainer" style="display: none;" title="Rejection Note">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="background-color: White;">
                                    <tr>
                                        <td align="left">
                                            <div id="divRejectionNote">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <br />
                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="button" OnClientClick="return Validate();"
                                OnClick="btnView_Click" Visible="false" />
                            <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="button" OnClientClick="return Validate();"
                                OnClick="btnModify_Click" Visible="false" />
                            <%--<input type="button" name="btnCreateNew" id="btnCreateNew" class="button" value="Create New"
                                style="width: 120px" onclick="return ShowCreateNewDialog();" />--%>
                            <input type="button" name="btnCreateNew" id="btnCreateNew" class="button" value="Create New"
                                style="width: 120px" onclick="return ShowCreateNewDialog();" runat="server" clientidmode="Static"/>
                            <asp:Button ID="btnChangeBulkRequestC" runat="server" Text="Change Request" CssClass="button"
                                OnClientClick="return ShowChangeBulkRequestDialog();" Width="112px" />
                            <asp:Button ID="btnChangeExtensionC" runat="server" Text="Customer Ext." CssClass="button"
                                OnClientClick="return ShowChangeExtensionDialog();" Width="112px" />
                            <asp:Button ID="btnChangeRequest" runat="server" Text="Change Request" CssClass="button"
                                OnClientClick="return Validate();" Width="150px" Visible="false" OnClick="btnChangeRequest_Click" />
                            <asp:Button ID="btnBlockRequest" runat="server" Text="Block / Unblock" CssClass="button"
                                Width="150px" Visible="true" OnClientClick="return ShowBlockDialog();" />
                            <asp:Button ID="btnCopyRequest" runat="server" Text="Copy Request" CssClass="button"
                                Visible="false" OnClientClick="return ShowCopyDialog();" Width="150px" />
                            <script type="text/javascript">
                                function Validate() {
                                    var gv = document.getElementById("<%=grdSearch.ClientID%>");
                                    var rbs = gv.getElementsByTagName("input");
                                    var flag = 0;
                                    for (var i = 0; i < rbs.length; i++) {

                                        if (rbs[i].type == "radio") {
                                            if (rbs[i].checked) {
                                                flag = 1;
                                                return true;
                                            }
                                        }
                                    }
                                    if (flag == 0) {
                                        alert("Kindly Select A Record");
                                        return false;
                                    }
                                }

                            </script>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divModulePopUp" title="Customer Master" style="display: none;">
        <asp:UpdatePanel ID="UpdModulePopUp" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">Company
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlCompany" runat="server" ControlToValidate="ddlCompany"
                                ValidationGroup="next" ErrorMessage="Select Company." SetFocusOnError="true"
                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Customer Type
                            <br />
                            (For Workflow)
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlCustomerType" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlCustomerType" runat="server" ControlToValidate="ddlCustomerType"
                                ValidationGroup="next" ErrorMessage="Select Customer Type." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Customer Type.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr id="trSalesRegion" runat="server" visible="false">
                        <td class="leftTD" width="25%">Sales Region
                            <br />
                            (For Workflow)
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlSalesRegion" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlSalesRegion_SelectedIndexChanged">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlSalesRegion" runat="server" ControlToValidate="ddlSalesRegion"
                                ValidationGroup="next" ErrorMessage="Select Sales Region." SetFocusOnError="true"
                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Sales Region.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="30%">Account Group
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlModule" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlModule"
                                ValidationGroup="next" ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Module.' />" />
                        </td>
                    </tr>

                    <%--Start Addition By Swati M Date: 08.10.2018--%>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr id="trDivisionType" runat="server" visible="false">
                        <td class="leftTD" width="30%">Division Type
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlDivisionType" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlDivisionType_SelectedIndexChanged">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlDivisionType" runat="server" ControlToValidate="ddlDivisionType"
                                ValidationGroup="next" ErrorMessage="Select Division Type." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Division Type.' />" />
                        </td>
                    </tr>

                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr id="trDivision" runat="server" visible="false">
                        <td class="leftTD" width="30%">Division
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlDivision" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlDivision" runat="server" ControlToValidate="ddlDivision"
                                ValidationGroup="next" ErrorMessage="Select Division." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Division.' />" />
                        </td>
                    </tr>

                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr id="trZone" runat="server" visible="false">
                        <td class="leftTD" width="30%">Zone
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlZone" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlZone" runat="server" ControlToValidate="ddlZone"
                                ValidationGroup="next" ErrorMessage="Select Zone." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Zone.' />" />
                        </td>
                    </tr>

                    <%--End Addition By Swati M Date: 08.10.2018--%>

                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <asp:Label ID="lblFirstApprover" runat="server" Font-Size="Small" ForeColor="#008080"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnNext" runat="server" ValidationGroup="next" Text="Next" CssClass="button"
                        OnClick="btnNext_Click" />
                    <asp:Button ID="btnCopy" runat="server" ValidationGroup="next" Text="Copy" CssClass="button"
                        OnClick="btnCopy_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divChangeModulePopUp" title="Customer Master :: Block Request" style="display: none;">
        <asp:UpdatePanel ID="UpdChangePopup" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">SAP Customer Code
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="textbox" MaxLength="10"
                                AutoPostBack="true" TabIndex="1" Width="180" OnTextChanged="txtCustomerCode_TextChanged" />
                            <asp:RequiredFieldValidator ID="reqtxtCustomerCode" runat="server" ControlToValidate="txtCustomerCode"
                                ValidationGroup="change" ErrorMessage="Vendor Code cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Code cannot be blank.' />" />
                            <asp:RegularExpressionValidator ID="regtxtCustomerCode" runat="server" ErrorMessage="Please check the SAP Customer Code."
                                Text="<img src='../../images/Error.png' title='Please check the SAP Customer Code.' />"
                                Display="Dynamic" ControlToValidate="txtCustomerCode" ValidationExpression="^[\S]{4,10}"
                                ValidationGroup="change"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Customer Name
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="textbox" MaxLength="60"
                                TabIndex="2" Width="180" />
                            <asp:RequiredFieldValidator ID="reqtxtCustomerName" runat="server" ControlToValidate="txtCustomerName"
                                ValidationGroup="change" ErrorMessage="Customer Name cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Customer Name cannot be blank.' />" />
                            <span style="color: Red; font-size: x-small; font-weight: normal">(As mentioned in SAP)</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Company
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlCompanyCode" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlCompanyCode" runat="server" ControlToValidate="ddlCompanyCode"
                                ValidationGroup="change" ErrorMessage="Select Company." SetFocusOnError="true"
                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Customer Type
                            <br />
                            (For Workflow)
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlCustomerTypeC" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerTypeC_SelectedIndexChanged">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlCustomerTypeC" runat="server" ControlToValidate="ddlCustomerTypeC"
                                ValidationGroup="change" ErrorMessage="Select Customer Type." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Customer Type.' />" />
                        </td>
                    </tr>
                    <%--Start Addition By Swati M Date: 08.10.2018--%>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr id="trZoneB" runat="server" visible="false">
                        <td class="leftTD" width="30%">Zone
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlZoneB" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlZoneB" runat="server" ControlToValidate="ddlZoneB"
                                ValidationGroup="change" ErrorMessage="Select Zone." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Zone.' />" />
                        </td>
                    </tr>
                    <%--End Addition By Swati M Date: 08.10.2018--%>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr id="trSalesRegionC" runat="server" visible="false">
                        <td class="leftTD" width="25%">Sales Region
                            <br />
                            (For Workflow)
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlSalesRegionC" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlSalesRegionC_SelectedIndexChanged">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlSalesRegionC" runat="server" ControlToValidate="ddlSalesRegionC"
                                ValidationGroup="change" ErrorMessage="Select Sales Region." SetFocusOnError="true"
                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Sales Region.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Customer Account Group
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlCustomerAccGroupC" runat="server" AppendDataBoundItems="true"
                                Enabled="false" AutoPostBack="true">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlCustomerAccGroupC" runat="server" ControlToValidate="ddlCustomerAccGroupC"
                                ValidationGroup="change" ErrorMessage="Select Customer Account Group." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Customer Account Group.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <asp:Label ID="lblFirstApproverC" runat="server" Font-Size="Small" ForeColor="#008080"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" colspan="2">
                            <span style="color: Orange; font-size: x-small"><i>Note: Please select the correct Customer
                                Account Group.</i><br />
                                <i>Once selected cannot be changed at later stage.</i></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnBlock" runat="server" ValidationGroup="change" Text="Block" CssClass="button"
                        OnClick="btnBlockRequest_Click" />
                    <asp:Button ID="btnUnBlock" runat="server" ValidationGroup="change" Text="UnBlock"
                        CssClass="button" OnClick="btnUnBlockRequest_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divCustomerExtPopup" title="Customer Master :: Change / Extension" style="display: none;">
        <asp:UpdatePanel ID="UpdCustomerPopup" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">Customer Type
                            <br />
                            (For Workflow)
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlCustomerTypeE" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerTypeE_SelectedIndexChanged">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlCustomerTypeE" runat="server" ControlToValidate="ddlCustomerTypeE"
                                ValidationGroup="Extension" ErrorMessage="Select Customer Type." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Customer Type.' />" />
                        </td>
                    </tr>
                    <%--Start Addition By Swati M Date: 08.10.2018--%>
                    <tr id="trZoneE" runat="server" visible="false">
                        <td class="leftTD" width="30%">Zone
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlZoneE" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlZoneE" runat="server" ControlToValidate="ddlZoneE"
                                ValidationGroup="Extension" ErrorMessage="Select Zone." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Zone.' />" />
                        </td>
                    </tr>
                    <%--End Addition By Swati M Date: 08.10.2018--%>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr id="trSalesRegionE" runat="server" visible="false">
                        <td class="leftTD" width="25%">Sales Region
                            <br />
                            (For Workflow)
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlSalesRegionE" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlSalesRegionE_SelectedIndexChanged">
                                <asp:ListItem Text="---Select---" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlSalesRegionE" runat="server" ControlToValidate="ddlSalesRegionE"
                                ValidationGroup="Extension" ErrorMessage="Select Sales Region." SetFocusOnError="true"
                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Sales Region.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <asp:Label ID="lblFirstApproverE" runat="server" Font-Size="Small" ForeColor="#008080"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" colspan="2">
                            <span style="color: Orange; font-size: x-small"><i>Note: Please select the correct Customer
                                Type.</i><br />
                                <i>Once selected cannot be changed at later stage.</i></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnChangeBulkRequest" runat="server" ValidationGroup="Extension"
                        Text="Change Request" CssClass="button" OnClick="btnChangeBulkRequest_Click" />
                    <asp:Button ID="btnChangeExtension" runat="server" Text="Customer Ext." CssClass="button"
                        ValidationGroup="Extension" OnClick="btnChangeExtension_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdateProgress ID="updateProgressMaster" runat="server" DisplayAfter="5">
        <ProgressTemplate>
            <div id="light" class="loading">
                <img src="../../images/ajax-loader-proccessing.gif" alt="Loading" height="30px" />
            </div>
            <div class="transparent_overlay">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="next" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblPk" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <script type="text/javascript" language="javascript">

                                $(function () {

                                });

                                function ShowBlockDialog() {

                                    $("#divChangeModulePopUp").dialog({
                                        height: 350,
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

                                    $('#<%= btnBlock.ClientID%>')._show();
                                    $('#<%= btnUnBlock.ClientID%>')._show();
                                }


                                function ShowCreateNewDialog() {
                                    $("#divModulePopUp").dialog({
                                        height: 250,
                                        width: 450,
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

                                    $('#<%= btnNext.ClientID%>')._show();
            $('#<%= btnCopy.ClientID%>')._hide();
                                }

                                function ShowChangeBulkRequestDialog() {
                                    $("#divCustomerExtPopup").dialog({
                                        height: 240,
                                        width: 450,
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

                                    $('#<%= btnChangeBulkRequest.ClientID%>')._show();
            $('#<%= btnChangeExtension.ClientID%>')._hide();
        }

        function ShowChangeExtensionDialog() {
            $("#divCustomerExtPopup").dialog({
                height: 240,
                width: 450,
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

            $('#<%= btnChangeBulkRequest.ClientID%>')._hide();
            $('#<%= btnChangeExtension.ClientID%>')._show();
        }

        function ShowCopyDialog() {
            if (Validate()) {
                $("#divModulePopUp").dialog({
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
                $('#<%= btnNext.ClientID%>')._hide();
                $('#<%= btnCopy.ClientID%>')._show();
            }
        }

        function ShowRejectionNote(obj) {

            $("#divRejectionNoteContainer").dialog({
                height: 140,
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
                    $("#divRejectionNote").html('');
                    $("#divRejectionNote").html(obj);
                }
            });
        }
    </script>
</asp:Content>
