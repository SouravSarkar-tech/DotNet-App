<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/MasterPage.master"
    AutoEventWireup="true" CodeFile="ProfitCenterMaster.aspx.cs"
    Inherits="Transaction_ProfitCenter_ProfitCenterMaster" %>

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
                        <td class="trHeading" align="center" colspan="4">Profit Center
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left">Request No
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:TextBox ID="txtRequestNo" runat="server" CssClass="textbox" />
                        </td>
                        <td class="leftTD" align="left">Profit Center Code</td>
                        <td class="rigthTD" align="left">
                            <asp:TextBox ID="txtSAPCode" runat="server" CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left">Module Name
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:DropDownList ID="ddlModuleName" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="All" Value="0" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlModuleName" runat="server" ControlToValidate="ddlModuleName"
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
                                PagerSettings-Mode="Numeric" OnPageIndexChanging="grdSearch_PageIndexChanging"
                                GridLines="Both">
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
                                            <%--<asp:Label ID="lblMasterCode" runat="server" Text='<%# Eval("Customer_Code") %>'></asp:Label>--%>
                                            <asp:Label ID="lblActionType" runat="server" Text='<%# Eval("Action_Type") %>'></asp:Label>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("Created_By") %>'></asp:Label>
                                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                            <asp:Label ID="lblContactNo" runat="server" Text='<%# Eval("ContactNo") %>'></asp:Label>
                                            <asp:Label ID="lblPendingFor" runat="server" Text='<%# Eval("Pending_For") %>'></asp:Label>
                                            <asp:Label ID="lblSelectedPlant" runat="server" Text='<%# Eval("MaterialPlant") %>'></asp:Label>
                                            <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("Company") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Request No.">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkWorkflow" runat="server" Text='<%# Eval("Request_No") %>'
                                                OnClientClick='<%# string.Format( "OpenRequestHistory(\"{0}\");", Eval("Master_Header_Id")) %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Module Name" DataField="Module_Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Plant Name" DataField="MaterialPlant" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Created By" DataField="Created_By" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Req. Dt." DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Location" DataField="Location" HeaderStyle-HorizontalAlign="Left"
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
                            <input type="button" name="btnCreateNew" id="btnCreateNew" class="button" value="Create New" runat="server"
                                style="width: 120px" onclick="return ShowCreateNewDialog();" />
                            <%--  <asp:Button ID="btnChangeRequest" runat="server" Text="Change Request" CssClass="button"
                                Width="150px" Visible="false" OnClientClick="return ShowChangeDialog();" />--%>
                            <%--<asp:Button ID="btnChangeBulkRequest" runat="server" Text="Change Request" CssClass="button"
                                Width="112px" OnClick="btnChangeBulkRequest_Click" />--%>
                            <%--Carve_LC17&LC23_8400000406--%>
                            <asp:Button ID="btnChangeBulkRequest" runat="server" Text="Change Request" CssClass="button"
                                Width="112px" Visible="true" OnClientClick="return ShowChangeDialog();" />
                           <%--Carve_LC17&LC23_8400000406--%>
                            <asp:Button ID="btnBlockRequest" runat="server" Text="Block / Unblock" CssClass="button"
                                Width="150px" Visible="true" OnClientClick="return ShowBlockDialog();" />
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
    <div id="divModulePopUp" style="display: none;" title="Profit Center Master">
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
                            <asp:RequiredFieldValidator ID="rfvddlCompany" runat="server" ControlToValidate="ddlCompany"
                                ValidationGroup="next" ErrorMessage="Select Company." SetFocusOnError="true"
                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Module Name
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlCreateModule" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlCreateModule" runat="server" ControlToValidate="ddlCreateModule"
                                ValidationGroup="next" ErrorMessage="Select Module name." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Module name.' />" />
                        </td>
                    </tr>

                    <%-- Carve_LC17&LC23_8400000406 start--%>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                    <tr>
                        <td class="leftTD" width="25%">Plant
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlantm" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                               <asp:RequiredFieldValidator ID="rfvddlPlantm" runat="server" ControlToValidate="ddlPlantm"
                                ValidationGroup="next" ErrorMessage="Select Plant." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Plant.' />" />
                        </td> 
                    </tr>
                    <%-- Carve_LC17&LC23_8400000406 end--%>
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
                </td>
            </tr>
        </table>
    </div>

    <div id="divblockModulePopUp" style="display: none;" title="Block/Un-block Request">
        <asp:UpdatePanel ID="UpdblockPopup" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">Profit Center Code
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtProfitCenterCode" runat="server" CssClass="textbox" MaxLength="8"
                                AutoPostBack="true" TabIndex="1" />
                            <asp:RequiredFieldValidator ID="reqtxtProfitCenterCode" runat="server" ControlToValidate="txtProfitCenterCode"
                                ValidationGroup="block" ErrorMessage="Profit Center cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Profit Center cannot be blank.' />" />
                            <asp:RegularExpressionValidator ID="regtxtProfitCenterCode" runat="server" ErrorMessage="Please check the SAP Profit Center Code."
                                Text="<img src='../../images/Error.png' title='Please check the SAP Profit Center Code.' />"
                                Display="Dynamic" ControlToValidate="txtProfitCenterCode" ValidationExpression="^[\S]{4,10}"
                                ValidationGroup="block"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    
                    <tr>
                        <td class="leftTD" width="25%">Profit Center Name (as per SAP)
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtProfitCenterName" runat="server" Text="" CssClass="textbox" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtxtProfitCenterName" runat="server" ControlToValidate="txtProfitCenterName"
                                ValidationGroup="block" ErrorMessage="Enter Profit Center Name" SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Enter Profit Center Name' />" />
                        </td>
                    </tr>
                    <%-- Carve_LC17&LC23_8400000406 start--%>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr  >
                        <td class="leftTD" width="25%">Company
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlCompanyCode" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlCompanyCode" runat="server" ControlToValidate="ddlCompanyCode"
                                ValidationGroup="block" ErrorMessage="Select Company." SetFocusOnError="true"
                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Plant
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlantb" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                               <asp:RequiredFieldValidator ID="rfvddlPlantb" runat="server" ControlToValidate="ddlPlantb"
                                ValidationGroup="block" ErrorMessage="Select Plant." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Plant.' />" />
                        </td> 
                    </tr>
                    <%-- Carve_LC17&LC23_8400000406 end--%>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnBlock" runat="server" ValidationGroup="block" Text="Block" CssClass="button"
                        OnClick="btnBlockRequest_Click" />
                    <asp:Button ID="btnUnBlock" runat="server" ValidationGroup="block" Text="UnBlock"
                        CssClass="button" OnClick="btnUnBlockRequest_Click" />
                </td>
            </tr>
        </table>
    </div>
     <%-- Carve_LC17&LC23_8400000406 end--%>
    <div id="divModulePopUpc" style="display: none;" title="Profit Center Change">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">Company
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlCompanyCodec" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlCompanyCodec" runat="server" ControlToValidate="ddlCompanyCodec"
                                ValidationGroup="cnext" ErrorMessage="Select Company." SetFocusOnError="true"
                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Company.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr> 
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                    <tr>
                        <td class="leftTD" width="25%">Plant
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlantc" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                               <asp:RequiredFieldValidator ID="rfvddlPlantc" runat="server" ControlToValidate="ddlPlantc"
                                ValidationGroup="cnext" ErrorMessage="Select Plant." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Plant.' />" />
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
                    <asp:Button ID="btnNextC" runat="server" ValidationGroup="cnext" Text="Next" CssClass="button"
                        OnClick="btnNextC_Click" />
                </td>
            </tr>
        </table>
    </div>
     <%-- Carve_LC17&LC23_8400000406 end--%>
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

    <asp:ValidationSummary ID="valCR" runat="server" ValidationGroup="change"
        ShowMessageBox="true" ShowSummary="false" />

    <asp:ValidationSummary ID="valBR" runat="server" ValidationGroup="block"
        ShowMessageBox="true" ShowSummary="false" />

    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblPk" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <script type="text/javascript" language="javascript">

        $(function () {

        });


        function ShowBlockDialog() {

            $("#divblockModulePopUp").dialog({
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

            $('#<%= btnBlock.ClientID%>')._show();
            $('#<%= btnUnBlock.ClientID%>')._show();
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
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                    $(this).show('clip');
                }
            });

            $('#<%= btnNext.ClientID%>')._show();
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
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                    $(this).show('clip');
                    $("#divRejectionNote").html('');
                    $("#divRejectionNote").html(obj);
                }
            });
        }
        /*Carve_LC17 & LC23_8400000406*/
        function ShowChangeDialog() {

            $("#divModulePopUpc").dialog({
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

            $('#<%= btnNextC.ClientID%>')._show(); 
        }
        /*Carve_LC17 & LC23_8400000406*/
    </script>
</asp:Content>


