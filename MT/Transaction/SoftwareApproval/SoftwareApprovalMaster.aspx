<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/MasterPage.master" AutoEventWireup="true" CodeFile="SoftwareApprovalMaster.aspx.cs"
    Inherits="Transaction_SoftwareApproval_SoftwareApprovalMaster" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlSearch" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="4">
                            Manufacturing & Quality Software Approval
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
                    <tr style="display: none">
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="4">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
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
                                OnPageIndexChanging="grdSearch_PageIndexChanging" GridLines="Both">
                                <RowStyle CssClass="light-gray" />
                                <HeaderStyle CssClass="gridHeader" />
                                <AlternatingRowStyle CssClass="gridRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdoSelection" runat="server" GroupName="selection" onclick="javascript:CheckOtherIsCheckedByGVID(this);"
                                                OnCheckedChanged="rdoSelection_CheckedChanged"/>                                                 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMassRequestProcessId" runat="server" Text='<%# Eval("Mass_Request_Process_Id") %>'></asp:Label>
                                            <asp:Label ID="lblPrimaryID" runat="server" Text='<%# Eval("Master_Header_Id") %>'></asp:Label>
                                            <asp:Label ID="lblModuleId" runat="server" Text='<%# Eval("Module_Id") %>'></asp:Label>
                                            <asp:Label ID="lblModuleName" runat="server" Text='<%# Eval("Module_Name") %>'></asp:Label>
                                            <asp:Label ID="lblRequestNo" runat="server" Text='<%# Eval("Request_No") %>'></asp:Label>
                                            <asp:Label ID="lblActionType" runat="server" Text='<%# Eval("Action_Type") %>'></asp:Label>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("Created_By") %>'></asp:Label>
                                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                            <asp:Label ID="lblContactNo" runat="server" Text='<%# Eval("ContactNo") %>'></asp:Label>
                                            <asp:Label ID="lblPlantId" runat="server" Text='<%# Eval("MaterialPlant") %>'></asp:Label>
                                            <asp:Label ID="lblPlantGroupId" runat="server" Text='<%# Eval("Plant_Group_Id") %>'></asp:Label>
                                           <%-- <asp:Label ID="lblPlantName" runat="server" Text='<%# Eval("Plant_Name") %>'></asp:Label>
                                            <asp:Label ID="lblPlantType" runat="server" Text='<%# Eval("Plant_Type") %>'></asp:Label>--%>
                                           <%-- <asp:Label ID="lblReqStatus" runat="server" Text='<%# Eval("Req_Status") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Request No." ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            &nbsp;&nbsp;<asp:LinkButton ID="lnkWorkflow" runat="server" Text='<%# Eval("Request_No") %>'
                                                OnClientClick='<%# string.Format( "OpenRequestHistory(\"{0}\",\"{1}\");", Eval("Master_Header_Id"), Eval("Mass_Request_Process_Id")) %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                   <%-- <asp:BoundField HeaderText="Material Code" DataField="Material_Number" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>--%>
                                    <%--<asp:BoundField HeaderText="Customer Name" DataField="Customer_Number" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />--%>
                                    <asp:BoundField HeaderText="Software Type" DataField="Module_Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Software Name" DataField="SWName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Req. Location" DataField="Location" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Created By" DataField="Created_By" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Req. Dt." DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>                                    
                                    <%--<asp:BoundField HeaderText="Cost Center" DataField="Cost_Centre" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" Visible = "false">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>--%>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" Visible="false"
                                        HeaderText="Remarks">
                                        <ItemTemplate>
                                            <a href="#" onclick="return ShowRejectionNote('<%#Eval("Remarks") %>')" style="color: #1540c2">
                                                Rejection Note</a>
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
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace" align="right">
                            <asp:Button ID="btnDelete" runat="server" Text="Delete Request" CssClass="button"
                                OnClientClick="return ValidateChk();" Style="width: 120px" Visible="false" OnClick="btnDelete_Click"/>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button"
                                OnClientClick="return ValidateChk();" Style="width: 120px" Visible="false" OnClick="btnSubmit_Click" />
                            <%--OnClick="btnDelete_Click" OnClick="btnMassSubmit_Click" OnClick="btnView_Click" OnClick="btnModify_Click" OnClick="btnNext_Click"--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <br />
                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="button" OnClientClick="return Validate();"
                                Style="width: 100px" OnClick="btnView_Click" Visible="false" />
                            <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="button" OnClientClick="return Validate();"
                                OnClick="btnModify_Click" Visible="false" Style="width: 120px" />
                            <input type="button" name="btnCreateNew" id="btnCreateNew" class="button" value="Create New"
                                style="width: 120px" onclick="return ShowCreateNewDialog();" />   
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

                                function ShowCopyDialog() {
                                    if (Validate()) {
                                        $("#divModulePopupCopy").dialog({
                                            height: 350,
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
        </Triggers>
    </asp:UpdatePanel>
    <div id="divModulePopUp" title="Manufacturing & Quality Software Approval" style="display: none;">
        <asp:UpdatePanel ID="UpdModulePopUp" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">
                            Your Location
                            <asp:Label ID="lableddlUserLocation" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlUserLocation" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlUserLocation_SelectedIndexChanged">                                
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlUserLocation" runat="server" ControlToValidate="ddlUserLocation"
                                ValidationGroup="next" ErrorMessage="Select your location." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select your location.' />" />
                        </td>
                    </tr>
                    <tr id = "trOtherLoc" runat = "server" visible = "false">
                        <td class="leftTD" width="25%">
                            Specify Location (if Others)
                            <asp:Label ID="labletxtOtherLoc" runat="server" ForeColor="Red" Text="*" Visible = "false"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtOtherLoc" runat="server" CssClass="textbox" Width="180" />
                            <asp:RequiredFieldValidator ID="reqtxtOtherLoc" runat="server" ControlToValidate="txtOtherLoc" Enabled = "false"
                                ValidationGroup="next" ErrorMessage="Specify Location in case of others." SetFocusOnError="true" 
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Specify Location in case of others.' />" />
                            <asp:RegularExpressionValidator ID = "regtxtOtherLoc" runat = "server" ControlToValidate = "txtOtherLoc" Enabled = "false" 
                                ValidationGroup = "next" ErrorMessage="Location should be minimum 3 characters." SetFocusOnError="true" ValidationExpression ="^[a-zA-Z0-9'@&#.\s]{3,}$"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Location should be minimum 3 characters.' />"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">
                            Type of Software
                            <asp:Label ID="lableddlModule" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlModule" runat="server" AppendDataBoundItems="true" 
                                AutoPostBack="true" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">                                
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlModule" runat="server" ControlToValidate="ddlModule"
                                ValidationGroup="next" ErrorMessage="Select Type of Software." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Type of Software.' />" />
                        </td>
                    </tr>
                    <tr id = "trOtherSW" runat = "server" visible = "false">
                        <td class="leftTD" width="25%">
                            Specify Software Type (if Others)
                            <asp:Label ID="labletxtOtherSW" runat="server" ForeColor="Red" Text="*" Visible = "false"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtOtherSW" runat="server" CssClass="textbox" Width="180"/>
                            <asp:RequiredFieldValidator ID="reqtxtOtherSW" runat="server" ControlToValidate="txtOtherSW" Enabled = "false"
                                ValidationGroup="next" ErrorMessage="Specify Software type in case of others." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Specify Software type in case of others.' />" />
                            <asp:RegularExpressionValidator ID = "regtxtOtherSW" runat = "server" ControlToValidate = "txtOtherSW" Enabled = "false" 
                                ValidationGroup = "next" ErrorMessage="Software type should be minimum 3 characters." SetFocusOnError="true" ValidationExpression ="^[a-zA-Z0-9'@&#.\s]{3,}$"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Software type should be minimum 3 characters.' />"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="leftTD" width="25%">
                            Plant Group
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlantGroup" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlPlantGroup" runat="server" ControlToValidate="ddlPlantGroup"
                                ValidationGroup="next" ErrorMessage="Select Plant Group." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Plant Group.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>                    
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr id = "trCategory" runat = "server" visible = "false">
                        <td class="leftTD" width="25%">
                            Sub Category
                            <asp:Label ID="lableddlCategory" runat="server" ForeColor="Red" Text="*" Visible = "false"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">                                                               
                                <asp:ListItem Text="Select" Value="" />                                
                                <asp:ListItem Text="Manufacturing" Value="M" />
                                <asp:ListItem Text="R&D" Value="R" />
                                <asp:ListItem Text="Quality" Value="Q" />
                                <asp:ListItem Text="Others" Value="O" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlCategory" runat="server" ControlToValidate="ddlCategory" Enabled = "false"
                                ValidationGroup="next" ErrorMessage="Select Sub category." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Sub category.' />" />
                        </td>
                    </tr>
                    <tr id = "trOtherSubCat" runat = "server" visible = "false">
                        <td class="leftTD" width="25%">
                            Specify Sub Category (if Others)
                            <asp:Label ID="labletxtOtherCategory" runat="server" ForeColor="Red" Text="*" Visible = "false"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtOtherCategory" runat="server" CssClass="textbox" Width="180"/>
                            <asp:RequiredFieldValidator ID="reqtxtOtherCategory" runat="server" ControlToValidate="txtOtherCategory" Enabled = "false"
                                ValidationGroup="next" ErrorMessage="Specify Sub Category in case of others." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Specify Sub Category in case of others.' />" />
                            <asp:RegularExpressionValidator ID = "regtxtOtherCategory" runat = "server" ControlToValidate = "txtOtherCategory" Enabled = "false" 
                                ValidationGroup = "next" ErrorMessage="Sub category should be minimum 3 characters." SetFocusOnError="true" ValidationExpression ="^[a-zA-Z0-9'@&#.\s]{3,}$"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Sub category should be minimum 3 characters.' />"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">
                            Reporting Manger
                            <asp:Label ID="labletxtManager" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            
                            <asp:TextBox ID="txtManager" runat="server" CssClass="textbox" MaxLength="10" Width="180" Enabled = "false"/>
                            <asp:RequiredFieldValidator ID="reqtxtManager" runat="server" ControlToValidate="txtManager"
                                ValidationGroup="next" ErrorMessage="Select Reporting Manager." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Reporting Manager.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <span style="color: Orange; font-size: x-small"><i>Note: Please select the options carefully.</i><br />
                                <i>Once selected cannot be changed at later stage.</i></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
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
        <asp:UpdatePanel ID="UpdModulePopUpCopy" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">
                            Your Location
                            <asp:Label ID="lableddlUserLocationCopy" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlUserLocationCopy" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlUserLocationCopy_SelectedIndexChanged">                                
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlUserLocationCopy" runat="server" ControlToValidate="ddlUserLocationCopy"
                                ValidationGroup="copy" ErrorMessage="Select your location." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select your location.' />" />
                        </td>
                    </tr>
                    <tr id = "trOtherLocCopy" runat = "server" visible = "false">
                        <td class="leftTD" width="25%">
                            Specify Location (if Others)
                            <asp:Label ID="labletxtOtherLocCopy" runat="server" ForeColor="Red" Text="*" Visible = "false"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtOtherLocCopy" runat="server" CssClass="textbox" Width="180"/>
                            <asp:RequiredFieldValidator ID="reqtxtOtherLocCopy" runat="server" ControlToValidate="txtOtherLocCopy" Enabled = "false"
                                ValidationGroup="copy" ErrorMessage="Specify Location in case of others." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Specify Location in case of others.' />" />
                            <asp:RegularExpressionValidator ID = "regtxtOtherLocCopy" runat = "server" ControlToValidate = "txtOtherLocCopy" Enabled = "false" 
                                ValidationGroup = "copy" ErrorMessage="Location should be minimum 3 characters." SetFocusOnError="true" ValidationExpression ="^[a-zA-Z0-9'@&#.\s]{3,}$"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Location should be minimum 3 characters.' />"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">
                            Type of Software
                            <asp:Label ID="lableddlModuleCopy" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlModuleCopy" runat="server" AppendDataBoundItems="true" 
                                AutoPostBack="true" OnSelectedIndexChanged="ddlModuleCopy_SelectedIndexChanged">                                
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlModuleCopy" runat="server" ControlToValidate="ddlModuleCopy"
                                ValidationGroup="copy" ErrorMessage="Select Type of Software." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Type of Software.' />" />
                        </td>
                    </tr>
                    <tr id = "trOtherSWCopy" runat = "server" visible = "false">
                        <td class="leftTD" width="25%">
                            Specify Software Type (if Others)
                            <asp:Label ID="labletxtOtherSWCopy" runat="server" ForeColor="Red" Text="*" Visible ="false"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtOtherSWCopy" runat="server" CssClass="textbox" Width="180"/>
                            <asp:RequiredFieldValidator ID="reqtxtOtherSWCopy" runat="server" ControlToValidate="txtOtherSWCopy" Enabled = "false"
                                ValidationGroup="copy" ErrorMessage="Specify Software type in case of others." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Specify Software type in case of others.' />" />
                            <asp:RegularExpressionValidator ID = "regtxtOtherSWCopy" runat = "server" ControlToValidate = "txtOtherSWCopy" Enabled = "false" 
                                ValidationGroup = "copy" ErrorMessage="Software Type should be minimum 3 characters." SetFocusOnError="true" ValidationExpression ="^[a-zA-Z0-9'@&#.\s]{3,}$"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Software Type should be minimum 3 characters.' />"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="leftTD" width="25%">
                            Plant Group
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlantGroupCopy" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlPlantGroupCopy" runat="server" ControlToValidate="ddlPlantGroupCopy"
                                ValidationGroup="copy" ErrorMessage="Select Plant Group." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Plant Group.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>                    
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr id = "trCategoryCopy" runat = "server" visible = "false">
                        <td class="leftTD" width="25%">
                            Sub Category
                            <asp:Label ID="lableddlCategoryCopy" runat="server" ForeColor="Red" Text="*" Visible ="false"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlCategoryCopy" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCategoryCopy_SelectedIndexChanged">                                
                                <asp:ListItem Text="Select" Value="" />                                
                                <asp:ListItem Text="Manufacturing" Value="M" />
                                <asp:ListItem Text="R&D" Value="R" />
                                <asp:ListItem Text="Quality" Value="Q" />
                                <asp:ListItem Text="Others" Value="O" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlCategoryCopy" runat="server" ControlToValidate="ddlCategoryCopy" Enabled = "false"
                                ValidationGroup="copy" ErrorMessage="Select Sub category." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Sub category.' />" />
                        </td>
                    </tr>
                    <tr id = "trOtherCatCopy" runat = "server" visible = "false">
                        <td class="leftTD" width="25%">
                            Specify Sub Category (if Others)
                            <asp:Label ID="labletxtOtherCategoryCopy" runat="server" ForeColor="Red" Text="*" Visible ="false"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtOtherCategoryCopy" runat="server" CssClass="textbox" Width="180"/>
                            <asp:RequiredFieldValidator ID="reqtxtOtherCategoryCopy" runat="server" ControlToValidate="txtOtherCategoryCopy" Enabled = "false"
                                ValidationGroup="copy" ErrorMessage="Specify Sub Category in case of others." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Specify Sub Category in case of others.' />" />
                            <asp:RegularExpressionValidator ID = "regtxtOtherCategoryCopy" runat = "server" ControlToValidate = "txtOtherCategoryCopy" Enabled = "false" 
                                ValidationGroup = "copy" ErrorMessage="Sub category should be minimum 3 characters." SetFocusOnError="true" ValidationExpression ="^[a-zA-Z0-9'@&#.\s]{3,}$"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Sub category should be minimum 3 characters.' />"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">
                            Reporting Manger
                            <asp:Label ID="labletxtManagerCopy" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            
                            <asp:TextBox ID="txtManagerCopy" runat="server" CssClass="textbox" MaxLength="10" Width="180" Enabled = "false"/>
                            <asp:RequiredFieldValidator ID="reqtxtManagerCopy" runat="server" ControlToValidate="txtManagerCopy"
                                ValidationGroup="copy" ErrorMessage="Select Reporting Manager." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Reporting Manager.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <span style="color: Orange; font-size: x-small"><i>Note: Please select the options carefully.</i><br />
                                <i>Once selected cannot be changed at later stage.</i></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnCopy" runat="server" ValidationGroup="copy" Text="Copy" CssClass="button" 
                        OnClick="btnCopy_Click" />
                </td>
            </tr>
        </table>
    </div>

    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="next" ShowMessageBox="true"
        ShowSummary="false" />
     <asp:ValidationSummary ID="smCopy" runat="server" ValidationGroup="copy" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblPk" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />

    <script src="../../Tooltip/toolTip.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        $(function () {

        });

        function ShowCreateNewDialog() {
            $("#divModulePopUp").dialog({
                height: 350,
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
    </script>
</asp:Content>

