<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/MasterPage.master"
    AutoEventWireup="true" CodeFile="VendorMaster.aspx.cs" Inherits="Transaction_Vendor_VendorMaster" %>

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
            <asp:Panel ID="pnlSearch" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <%-- Added by Swati on 15.03.2019 for Ariba Migration Downtime Notification --%>
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                                <asp:Label ID="lblMsg" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <%-- End --%>
                    <tr>
                        <td class="trHeading" align="center" colspan="4">Vendor Master
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
                        <td class="leftTD" align="left">Vendor account group
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:DropDownList ID="ddlVendorAccGrp" runat="server" AppendDataBoundItems="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlVendorAccGrp" runat="server" ControlToValidate="ddlVendorAccGrp"
                                ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic" Text="<img src='../images/Error.png' title='Select Module.' />" />
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
                    <tr style="display: none">
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
                        <td colspan="4" class="tdSpace">
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
                                PagerSettings-Mode="Numeric" OnPageIndexChanging="grdSearch_PageIndexChanging" OnRowDataBound="grdSearch_RowDataBound"
                                GridLines="Both">
                                <%----%>
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
                                            <asp:Label ID="lblPendingFor" runat="server" Text='<%# Eval("Pending_For") %>'></asp:Label>
                                            <asp:Label ID="lblSelectedVendorAccGrp" runat="server" Text='<%# Eval("SelectedVendorAccGrp") %>'></asp:Label>
                                            <asp:Label ID="lblForward" runat="server" Text='<%# Eval("ForwardRemarks") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Request No.">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkWorkflow" runat="server" Text='<%# Eval("Request_No") %>'
                                                OnClientClick='<%# string.Format( "OpenRequestHistory(\"{0}\");", Eval("Master_Header_Id")) %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="Request No." DataField="Request_No" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />--%>
                                    <asp:BoundField HeaderText="SAP Code" DataField="Customer_Code" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Vendor Acc. Grp" DataField="Module_Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Vendor" DataField="Name1" HeaderStyle-HorizontalAlign="Left"
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
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" Visible="false"
                                        HeaderText="Forward Remarks">
                                        <ItemTemplate>
                                            <a href="#" onclick='<%# string.Format( "ShowForwardNote(\"{0}\");", Eval("ForwardRemarks")) %>' style="color: #1540c2"
                                                runat="server" id="lnkFrwrdNote">Forward Note</a>
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
                            <div id="divForwardRemarkNoteContainer" style="display: none;" title="Forward Note">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="background-color: White;">
                                    <tr>
                                        <td align="left">
                                            <div id="divForwardNote">
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
                                style="width: 120px" onclick="return ShowCreateNewDialog();" runat="server" clientidmode="Static" />

                            <%--<asp:Button ID="btnNext" runat="server" Text="Create New" CssClass="button" OnClick="btnNext_Click"
                                Width="100px" />--%>

                            <%--PFun_DT06032020 Start Commented by NR--%>
                            <%-- <asp:Button ID="btnChangeBulkRequest" runat="server" Text="Change Request" CssClass="button"
                                OnClick="btnChangeBulkRequest_Click" Width="112px" />--%>
                            <%--PFun_DT06032020 End Commented by NR--%>

                            <%--PFun_DT06032020 Start Commented by NR--%>
                            <input type="button" name="btnChangeBulkRequest" id="btnChangeBulkRequest" class="button" value="Change Request"
                                style="width: 112px" onclick="return ShowChangeNewDialog();" runat="server" clientidmode="Static" />
                            <%--PFun_DT06032020 End Commented by NR--%>

                            <asp:Button ID="btnChangeRequest" runat="server" Text="Change Request" CssClass="button"
                                Width="150px" Visible="false" OnClientClick="return ShowChangeDialog();" />
                            <asp:Button ID="btnBlockRequest" runat="server" Text="Vendor Block / Unblock" CssClass="button"
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
        </Triggers>
    </asp:UpdatePanel>
    <div id="divModulePopUp" style="display: none;" title="Vendor Master">
        <asp:UpdatePanel ID="UpdModulePopUp" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">Company
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlCompany"
                                ValidationGroup="next" ErrorMessage="Select Company." SetFocusOnError="true"
                               InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Vendor Account Group
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlVendorAccGroup" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlVendorAccGroup_SelectedIndexChanged">
                                <%--Vendor Workflow Modification changes Start--%>
                                <%--AutoPostBack="true" OnSelectedIndexChanged="ddlVendorAccGroup_SelectedIndexChanged"--%>
                                <%--Vendor Workflow Modification changes End--%>
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlVendorAccGroup"
                                ValidationGroup="next" ErrorMessage="Select Vendor Account Group." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Vendor Account Group.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <%--Vendor Workflow Modification changes Start--%>
                    <tr>
                        <td class="leftTD" width="25%">
                            <asp:Label ID="lblManager" runat="server" Text="Reporting Manger"></asp:Label>
                            
                        </td>
                        <td class="rigthTD">

                            <asp:TextBox ID="txtManager" runat="server" CssClass="textbox" MaxLength="10" Width="180" Enabled="false" />
                            <asp:RequiredFieldValidator ID="reqtxtManager" runat="server" ControlToValidate="txtManager"
                                ValidationGroup="next" ErrorMessage="Select Reporting Manager." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Reporting Manager.' />" />
                        </td>
                    </tr>
                    <%--Vendor Workflow Modification changes End--%>
                    <tr>
                        <td class="leftTD" colspan="2">
                            <span style="color: Orange; font-size: x-small"><i>Note: Please select the correct Vendor
                                Account Group.</i><br />
                                <i>Once selected cannot be changed at later stage.</i></span>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="tdSpace" colspan="2"></td>
            </tr>
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
    <%--425143--%>

    <div id="divHrModulePopUp" style="display: none;" title="Warning Message">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="tdSpace" colspan="2"></td>
            </tr>
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnbackHRMsg" runat="server" Text="Go to back" CssClass="button"
                        OnClick="btnbackHRMsg_Click" />
                </td>
            </tr>
        </table>
    </div>
    <%--425143--%>


    <div id="divValidationModulePopUp" style="display: none;" title="Warning Message">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="tdSpace" colspan="2"></td>
            </tr>
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnbackMsg" runat="server" Text="Go to ARIBA site" CssClass="button"
                        OnClick="btnbackMsg_Click" />
                </td>
            </tr>
        </table>
    </div>


    <div id="divChangeModulePopUp" style="display: none;" title="Vendor Master :: Change Request">
        <asp:UpdatePanel ID="UpdChangePopup" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">SAP Vendor Code
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtVendorCode" runat="server" CssClass="textbox" MaxLength="10"
                                AutoPostBack="true" TabIndex="1" Width="180" OnTextChanged="txtVendorCode_TextChanged" />
                            <asp:RequiredFieldValidator ID="reqtxtVendorCode" runat="server" ControlToValidate="txtVendorCode"
                                ValidationGroup="change" ErrorMessage="Vendor Code cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Code cannot be blank.' />" />
                            <asp:RegularExpressionValidator ID="regtxtVendorCode" runat="server" ErrorMessage="Please check the SAP Vendor Code."
                                Text="<img src='../../images/Error.png' title='Please check the SAP Vendor Code.' />"
                                Display="Dynamic" ControlToValidate="txtVendorCode" ValidationExpression="^[\S]{4,10}"
                                ValidationGroup="change"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Vendor Name
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtVendorName" runat="server" CssClass="textbox" MaxLength="60"
                                TabIndex="2" Width="180" />
                            <asp:RequiredFieldValidator ID="reqtxtVendorName" runat="server" ControlToValidate="txtVendorName"
                                ValidationGroup="change" ErrorMessage="Vendor Name cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Vendor Name cannot be blank.' />" />
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
                                <asp:ListItem Text="Select" Value="" />
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
                        <td class="leftTD" width="25%">Vendor Account Group
                        </td>
                        <td class="rigthTD">
                            <%--Added OnSelectedIndexChanged by Swati on 15.03.2019 for Ariba Migration Downtime Notification--%>
                            <asp:DropDownList ID="ddlVendorAccGroupC" runat="server" AppendDataBoundItems="true">
                                <%--OnSelectedIndexChanged="ddlVendorAccGroupC_SelectedIndexChanged" AutoPostBack="true"--%>
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <%-- End --%>
                            <asp:RequiredFieldValidator ID="reqddlVendorAccGroupC" runat="server" ControlToValidate="ddlVendorAccGroupC"
                                ValidationGroup="change" ErrorMessage="Select Vendor Account Group." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Vendor Account Group.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" colspan="2">
                            <span style="color: Orange; font-size: x-small"><i>Note: Please select the correct Vendor
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
                    <asp:Button ID="btnChange" runat="server" ValidationGroup="change" Text="Change"
                        CssClass="button" OnClick="btnChangeRequest_Click" />
                    <asp:Button ID="btnBlock" runat="server" ValidationGroup="change" Text="Block" CssClass="button"
                        OnClick="btnBlockRequest_Click" />
                    <asp:Button ID="btnUnBlock" runat="server" ValidationGroup="change" Text="UnBlock"
                        CssClass="button" OnClick="btnUnBlockRequest_Click" />
                </td>
            </tr>
        </table>
    </div>


    <%--PFun_DT06032020 Added by NR--%>
    <div id="divChangePFPopUp" style="display: none; height: 100px !important" title="Vendor Master :: Change Request">
        <asp:UpdatePanel ID="upPartnerFun" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">Select Request
                        </td>
                        <td class="rigthTD">
                            <%--OnSelectedIndexChanged="ddlSelectRequest_SelectedIndexChanged"--%>
                            <asp:DropDownList ID="ddlSelectRequest" runat="server" AppendDataBoundItems="false"
                                TabIndex="9" onchange="GetSelectedValue(this)"
                                AutoPostBack="true">
                                <asp:ListItem Text="--Select--" Value="-1" />
                                <asp:ListItem Text="Create Change Request" Value="1" />
                                <asp:ListItem Text="Link Partner Function Request" Value="2" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSelectRequest" runat="server" ControlToValidate="ddlSelectRequest"
                                ValidationGroup="changepf" ErrorMessage="Select Request." SetFocusOnError="true" InitialValue="-1"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Request.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 50px !important">
            <tr>
                <td class="tdSpace" colspan="2"></td>
            </tr>
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnChangeNext" runat="server" ValidationGroup="changepf" Text="Next" CssClass="button"
                        OnClick="btnChangeBulkRequest_Click" ClientIDMode="Static" Style="display: none !important" />
                    <asp:Button ID="btnChangeAdd" runat="server" ValidationGroup="changepf" Text="Add Link Vendor" CssClass="button"
                        OnClick="btnChangeAdd_Click" ClientIDMode="Static" Style="display: none !important" />
                    <asp:Button ID="btnChangeRemove" runat="server" ValidationGroup="changepf" Text="Remove Link Vendor" CssClass="button"
                        OnClick="btnChangeRemove_Click" ClientIDMode="Static" Style="display: none !important" />
                </td>
            </tr>
        </table>
    </div>

    <%--PFun_DT06032020 Added by NR--%>

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
    <asp:ValidationSummary ID="valChangeRequest" runat="server" ValidationGroup="change"
        ShowMessageBox="true" ShowSummary="false" />

    <asp:ValidationSummary ID="valPFChange" runat="server" ValidationGroup="changepf" ShowMessageBox="true"
        ShowSummary="false" />

    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblPk" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <script type="text/javascript" language="javascript">

        $(function () {

        });

        function ShowChangeDialog() {

            $("#divChangeModulePopUp").dialog({
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

            $('#<%= btnBlock.ClientID%>')._hide();
            $('#<%= btnUnBlock.ClientID%>')._hide();
            $('#<%= btnChange.ClientID%>')._show();
        }

        function ShowBlockDialog() {

            $("#divChangeModulePopUp").dialog({
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

            $('#<%= btnBlock.ClientID%>')._show();
            $('#<%= btnUnBlock.ClientID%>')._show();
            $('#<%= btnChange.ClientID%>')._hide();
        }

        function ShowCreateNewDialog() {
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

            $('#<%= btnNext.ClientID%>')._show();
            $('#<%= btnCopy.ClientID%>')._hide();

        }

        function ShowValidationNewDialog() {
            $("#divValidationModulePopUp").dialog({
                height: 350,
                width: 600,
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
        }

        function ShowCopyDialog() {
            if (Validate()) {
                $("#divModulePopUp").dialog({
                    height: 200,
                    width: 400,
                    modal: true,
                    closeOnEscape: true,
                    draggable: true,
                    resizable: false,
                    position: 'center',
                    dialogClass: 'alert',
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

        function ShowForwardNote(obj) {

            $("#divForwardRemarkNoteContainer").dialog({
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
                    $("#divForwardNote").html('');
                    $("#divForwardNote").html(obj);
                }
            });
        }

        function ShowValidationNewHRDialog() {
            $("#divHrModulePopUp").dialog({
                height: 350,
                width: 600,
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
        }

        //PFun_DT06032020 Start Added by NR
        function ShowChangeNewDialog() {
            $("#divChangePFPopUp").dialog({
                height: 250,
                width: 500,
                modal: true,
                closeOnEscape: true,
                draggable: true,
                resizable: false,
                position: 'center',
                dialogClass: 'alert',
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                    $(this).show('clip');
                }
            });

         <%--   $('#<%= btnChangeNext.ClientID%>')._hide();
            $('#<%= btnChangeAdd.ClientID%>')._hide();
            $('#<%= btnChangeRemove.ClientID%>')._hide();--%>

           <%-- $('#<%= btnChangeRemove.ClientID%>')._show();--%>
        }

        function GetSelectedValue(ddlSelectRequest) {
            //var selectedText = ddlSelectRequest.options[ddlSelectRequest.selectedIndex].innerHTML;
            var selectedValue = ddlSelectRequest.value;
            if (selectedValue == 1) {
                $('#<%= btnChangeNext.ClientID%>')._show();
                $('#<%= btnChangeAdd.ClientID%>')._hide();
                $('#<%= btnChangeRemove.ClientID%>')._hide();
            }
            else if (selectedValue == 2) {
                $('#<%= btnChangeNext.ClientID%>')._hide();
                $('#<%= btnChangeAdd.ClientID%>')._show();
                $('#<%= btnChangeRemove.ClientID%>')._show();
            }
            else {
                $('#<%= btnChangeNext.ClientID%>')._hide();
                $('#<%= btnChangeAdd.ClientID%>')._hide();
                $('#<%= btnChangeRemove.ClientID%>')._hide();
            }
            //alert("Selected Text: " + selectedText + " Value: " + selectedValue);
        }

                              //PFun_DT06032020 end Added by NR

    </script>
</asp:Content>
