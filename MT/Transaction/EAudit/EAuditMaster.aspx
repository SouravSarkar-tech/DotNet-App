<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/MasterPage.master"
    AutoEventWireup="true" CodeFile="EAuditMaster.aspx.cs" Inherits="Transaction_EAudit_EAuditMaster" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                        <td class="trHeading" align="center" colspan="4">
                            Manufacturer Approval Master
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left" style="width: 25%">
                            Request No
                        </td>
                        <td class="rigthTD" align="left" style="width: 25%">
                            <asp:TextBox ID="txtRequestNo" runat="server" CssClass="textbox" />
                        </td>
                        <td class="leftTD" align="left" style="width: 25%">
                            SAP Code
                        </td>
                        <td class="rigthTD" align="left" style="width: 25%">
                            <asp:TextBox ID="txtSAPCode" runat="server" CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left">
                            Module
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:DropDownList ID="ddlModuleSearch" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="All" Value="0" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModuleSearch"
                                ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Module.' />" />
                        </td>
                        <td class="leftTD" align="left">
                            Status
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Pending For My Approval" Value="P" />
                                <asp:ListItem Text="Rollbacked To Me" Value="R" />
                                <asp:ListItem Text="Created By Me" Value="C" />
                                <asp:ListItem Text="Incomplete Request" Value="I" />
                                <asp:ListItem Text="Rollbacked By Me" Value="REJ" />
                                <%--Srinidhi--%>
                                <asp:ListItem Text="Rejected To Me" Value="Z" />
                                <asp:ListItem Text="Rejected By Me" Value="ZE" />
                                <%--Srinidhi--%>
                                <asp:ListItem Text="Approved" Value="ALL" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left">
                            From Date
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" />
                            <act:CalendarExtender ID="CaltxtFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFromDate"
                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                        </td>
                        <td class="leftTD" align="left">
                            To Date
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
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="4">
                            <%--OnClick="btnSearch_Click"--%>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
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
                            <%--OnPageIndexChanging="grdSearch_PageIndexChanging"--%>
                            <asp:GridView ID="grdSearch" runat="server" AutoGenerateColumns="false" Width="100%"
                                BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdSearch_PageIndexChanging"
                                GridLines="Both">
                                <RowStyle CssClass="light-gray" />
                                <HeaderStyle CssClass="gridHeader" />
                                <AlternatingRowStyle CssClass="gridRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%--OnCheckedChanged="rdoSelection_CheckedChanged"--%>
                                            <asp:RadioButton ID="rdoSelection" runat="server" GroupName="selection" AutoPostBack="true"
                                                onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrimaryID" runat="server" Text='<%# Eval("Master_Header_Id") %>'></asp:Label>
                                            <asp:Label ID="lblModuleId" runat="server" Text='<%# Eval("Module_Id") %>'></asp:Label>
                                            <asp:Label ID="lblModuleName" runat="server" Text='<%# Eval("Module_Name") %>'></asp:Label>
                                            <asp:Label ID="lblRequestNo" runat="server" Text='<%# Eval("Request_No") %>'></asp:Label>
                                            <asp:Label ID="lblActionType" runat="server" Text='<%# Eval("Action_Type") %>'></asp:Label>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("Created_By") %>'></asp:Label>
                                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                            <asp:Label ID="lblContactNo" runat="server" Text='<%# Eval("ContactNo") %>'></asp:Label>
                                            <asp:Label ID="lblCreatorDept" runat="server" Text='<%# Eval("Creator_Dept") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Request No." ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            &nbsp;&nbsp;<asp:LinkButton ID="lnkWorkflow" runat="server" Text='<%# Eval("Request_No") %>'
                                                OnClientClick='<%# string.Format( "OpenRequestHistory(\"{0}\",\"{1}\");", Eval("Master_Header_Id"), Eval("Mass_Request_Process_Id")) %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Request Type" DataField="Req_Type" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" Visible = "false"/>
                                    <asp:BoundField HeaderText="Module Name" DataField="Module_Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" Visible = "false" />
                                    <asp:BoundField HeaderText="Created By" DataField="Created_By" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Req. Dt." DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Location" DataField="Location" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" Visible = "false"/>
                                    <asp:BoundField HeaderText="Cost Center" DataField="Cost_Centre" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" Visible = "false" />
                                    <asp:BoundField HeaderText="Manufacturer Name" DataField="MnfrName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Material Name" DataField="MaterialName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left"/>
                                    <asp:BoundField HeaderText="Lupin's Product Name" DataField="ProdName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left"/>
                                    <asp:BoundField HeaderText="Attachment List" DataField="Attachments" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left"/>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" Visible="false"
                                        HeaderText="Remarks">
                                        <ItemTemplate>
                                            <a href="#" onclick="return ShowRejectionNote('<%#Eval("Remarks") %>')" style="color: #1540c2">
                                                Rejection Note</a>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" Visible="false"
                                        HeaderText="Remarks">
                                        <ItemTemplate>
                                            <a href="#" onclick="return ShowApprovalNote('<%#Eval("Remarks") %>')" style="color: #1540c2">
                                                Approval Note </a>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" HeaderText="Status">
                                        <ItemTemplate>
                                            <%#Eval("Status") %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Actioned By" DataField="Actioned_By" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Date" DataField="Actioned_On" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <input type="checkbox" id="chkSelectAll" name="chkSelectAll" title="Select All" onclick="return fnChkSelectAll();" />
                                            <%--<asp:CheckBox ID="chkSelection" runat="server" GroupName="selection" />--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelection" runat="server" GroupName="selection" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
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
                            <div id="divApprovalNoteContainer" style="display: none;" title="Approval Note">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="background-color: White;">
                                    <tr>
                                        <td align="left">
                                            <div id="divApprovalNote">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace" align="right">
                            <asp:Button ID="btnDelete" runat="server" Text="Delete Request" CssClass="button"
                                Style="width: 120px" Visible="false" OnClientClick="return ValidateChk();" OnClick="btnDelete_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnMassSubmit" runat="server" Text="Submit" CssClass="button" Style="width: 120px"
                                Visible="false" OnClientClick="return ValidateChk();" OnClick="btnMassSubmit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <br />
                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="button" Style="width: 100px"
                                Visible="false" OnClick="btnView_Click" />
                            <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="button" Visible="false"
                                Style="width: 120px" OnClick="btnModify_Click" />
                            <%--<asp:Button ID="btnModRollBack" runat="server" Text="Modify" CssClass="button" Visible="false"
                                Style="width: 120px" OnClientClick="return ShowModifyDialog();" />--%>
                            <asp:Button ID="btnCreateNew" runat="server" Text="Create New" CssClass="button"
                                Style="width: 120px" OnClientClick="return ShowCreateNewDialog();" />
                            <asp:Button ID="btnCopyRequest" runat="server" Text="Copy Request" CssClass="button"
                                Visible="false" Style="width: 150px" OnClientClick="return ShowCopyDialog();"/>
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

                                function ValidateChk() {
                                    var gv = document.getElementById("<%=grdSearch.ClientID%>");
                                    var rbs = gv.getElementsByTagName("input");
                                    var flag = 0;
                                    for (var i = 0; i < rbs.length; i++) {

                                        if (rbs[i].type == "checkbox") {
                                            if (rbs[i].checked) {
                                                flag = flag + 1;
                                                break;
                                            }
                                        }
                                    }
                                    if (flag == 0) {
                                        alert("Kindly Select a Record to submit");
                                        return false;
                                    }
                                }

                                function ShowCreateNewDialog() {
                                    $("#divModulePopUp").dialog({
                                        height: 200,
                                        width: 550,
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
                                }

                                function fnChkSelectAll() {
                                    var gvall = document.getElementById("chkSelectAll");
                                    var gv = document.getElementById("<%=grdSearch.ClientID%>");
                                    var rbs = gv.getElementsByTagName("input");
                                    var flag = 0;
                                    for (var i = 0; i < rbs.length; i++) {
                                        //                                        if (rbs[i].name == "chkSelectAll") {
                                        //                                            flag = rbs[i].checked;
                                        //                                        }

                                        if (rbs[i].type == "checkbox" && rbs[i].name != "chkSelectAll") {

                                            //alert(rbs[i].name);
                                            rbs[i].checked = gvall.checked//flag;
                                        }
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

                                function ShowApprovalNote(obj) {
                                    $("#divApprovalNoteContainer").dialog({
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
                                            $("#divApprovalNote").html('');
                                            $("#divApprovalNote").html(obj);
                                        }
                                    });
                                }

                                function ShowCopyDialog() {
                                    if (Validate()) {
                                        $("#divModulePopupCopy").dialog({
                                            height: 200,
                                            width: 550,
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
                                    }
                                }

                                function ShowModifyDialog() {
                                    if (Validate()) {
                                        $("#divModulePopupModify").dialog({
                                            height: 200,
                                            width: 550,
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
    <div id="divModulePopUp" title="Manufacturer Approval Master" style="display: none;">
        <%--<asp:UpdatePanel ID="UpdModulePopUp" runat="server">
                                    <ContentTemplate>--%>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
           <tr id="trAppDept" runat="server">
                <td class="leftTD" style="width: 25%">
                    Select Department
                </td>
                <td class="rigthTD" runat="server" >
                    <asp:DropDownList ID="ddlRNDRA" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select" Value="" />
                        <%--<asp:ListItem Text="R&D/PDL" Value="D" />
                        <asp:ListItem Text="RA" Value="A" />--%>
                        <asp:ListItem Text="R&D/PDL and RA" Value="R" />
                        <asp:ListItem Text="None" Value="N" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqddlRNDRA" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Next Level Department.'"
                        ControlToValidate="ddlRNDRA" InitialValue="" runat="server" ForeColor="Red" ErrorMessage = "Select Next Level Department."
                        ValidationGroup="next" />
                </td>
            </tr>
            <tr>
                <td class="leftTD" width="25%">
                    Select Department Head
                </td>
                <td class="rigthTD">
                    <%--OnSelectedIndexChanged="ddlModule_SelectedIndexChanged"--%>
                    <asp:DropDownList ID="ddlDeptHead" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select" Value="" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqddlDeptHead" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Department Head.'"
                        ControlToValidate="ddlDeptHead" InitialValue="" runat="server" ForeColor="Red" ErrorMessage = "Select Department Head."
                        ValidationGroup="next" />
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
                </td>
            </tr>
        </table>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnNext" runat="server" ValidationGroup="next" Text="Next" CssClass="button"
                        OnClick="btnNext_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div id="divModulePopupCopy" title="Manufacturer Approval Master" style="display: none;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
           <tr id="trAppDeptCopy" runat="server">
                <td class="leftTD" style="width: 25%">
                    Select Department
                </td>
                <td class="rigthTD" runat="server" >
                    <asp:DropDownList ID="ddlRNDRACopy" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select" Value="" />
                        <%--<asp:ListItem Text="R&D/PDL" Value="D" />
                        <asp:ListItem Text="RA" Value="A" />--%>
                        <asp:ListItem Text="R&D/PDL and RA" Value="R" />
                        <asp:ListItem Text="None" Value="N" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqddlAppDeptCopy" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Next Level Department.'"
                        ControlToValidate="ddlRNDRACopy" InitialValue="" runat="server" ForeColor="Red" ErrorMessage="Select Next Level Department."
                        ValidationGroup="Copy" />
                </td>
            </tr>
            <tr>
                <td class="leftTD" width="25%">
                    Select Department Head
                </td>
                <td class="rigthTD">
                    <asp:DropDownList ID="ddlDeptHeadCopy" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select" Value="" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqddlDeptHeadCopy" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Department Head.'"
                        ControlToValidate="ddlDeptHeadCopy" InitialValue="" runat="server" ForeColor="Red" ErrorMessage = "Select Department Head."
                        ValidationGroup="Copy" />
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnCopy" runat="server" ValidationGroup="Copy" Text="Copy" CssClass="button" 
                        OnClick="btnCopy_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div id="divModulePopupModify" title="Manufacturer Approval Master" style="display: none;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
           <tr id="trAppDeptModify" runat="server">
                <td class="leftTD" style="width: 25%">
                    Select Department
                </td>
                <td class="rigthTD" runat="server" >
                    <asp:DropDownList ID="ddlRNDRAModify" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select" Value="" />
                        <%--<asp:ListItem Text="R&D/PDL" Value="D" />
                        <asp:ListItem Text="RA" Value="A" />--%>
                        <asp:ListItem Text="R&D/PDL and RA" Value="R" />
                        <asp:ListItem Text="None" Value="N" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqddlAppDeptModify" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Next Level Department.'"
                        ControlToValidate="ddlRNDRAModify" InitialValue="" runat="server" ForeColor="Red" ErrorMessage = "Select Next Level Department."
                        ValidationGroup="Modify" />
                </td>
            </tr>
            <tr>
                <td class="leftTD" width="25%">
                    Select Department Head
                </td>
                <td class="rigthTD">
                    <asp:DropDownList ID="ddlDeptHeadModify" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select" Value="" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqddlDeptHeadModify" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Department Head.'"
                        ControlToValidate="ddlDeptHeadModify" InitialValue="" runat="server" ForeColor="Red" ErrorMessage = "Select Department Head."
                        ValidationGroup="Modify" />
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnNextModify" runat="server" ValidationGroup="Modify" Text="Next" CssClass="button"/>
                    <%--OnClick="btnNextModify_Click" --%>
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
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Copy" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Modify" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblPk" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
</asp:Content>
