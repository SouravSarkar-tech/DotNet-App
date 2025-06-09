<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/MasterPage.master"
    AutoEventWireup="true" CodeFile="MaterialMaster.aspx.cs" Inherits="Transaction_MaterialMaster" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--    <link href="../../stylesheet/main.css" rel="stylesheet" type="text/css" />
    <link href="../../stylesheet/Paging.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="upMsg" runat="server" RenderMode="Inline">
         <contenttemplate></ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlSearch" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="4">Material Master
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
                            <asp:DropDownList ID="ddlModuleSearch" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlModuleSearch_SelectedIndexChanged">
                                <asp:ListItem Text="All" Value="0" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModuleSearch"
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
                                            <asp:RadioButton ID="rdoSelection" runat="server" GroupName="selection" AutoPostBack="true"
                                                onclick="javascript:CheckOtherIsCheckedByGVID(this);" OnCheckedChanged="rdoSelection_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMassRequestProcessId" runat="server" Text='<%# Eval("Mass_Request_Process_Id") %>'></asp:Label>
                                            <asp:Label ID="lblPrimaryID" runat="server" Text='<%# Eval("Master_Header_Id") %>'></asp:Label>
                                            <asp:Label ID="lblModuleId" runat="server" Text='<%# Eval("Module_Id") %>'></asp:Label>
                                            <asp:Label ID="lblModuleName" runat="server" Text='<%# Eval("Module_Name") %>'></asp:Label>
                                            <asp:Label ID="lblRequestNo" runat="server" Text='<%# Eval("Request_No") %>'></asp:Label>
                                            <asp:Label ID="lblMasterCode" runat="server" Text='<%# Eval("Material_Number") %>'></asp:Label>
                                            <asp:Label ID="lblActionType" runat="server" Text='<%# Eval("Action_Type") %>'></asp:Label>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("Created_By") %>'></asp:Label>
                                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                            <asp:Label ID="lblContactNo" runat="server" Text='<%# Eval("ContactNo") %>'></asp:Label>
                                            <asp:Label ID="lblPlantId" runat="server" Text='<%# Eval("Mat_Plant_Id") %>'></asp:Label>
                                            <asp:Label ID="lblStorageLocation" runat="server" Text='<%# Eval("Storage_Location") %>'></asp:Label>
                                            <asp:Label ID="lblPlantGroupId" runat="server" Text='<%# Eval("Plant_Group_Id") %>'></asp:Label>
                                            <asp:Label ID="lblMaterialShortDescription" runat="server" Text='<%# Eval("Material_Short_Description") %>'></asp:Label>
                                            <asp:Label ID="lblPlantName" runat="server" Text='<%# Eval("Plant_Name") %>'></asp:Label>
                                            <asp:Label ID="lblStorageLocationName" runat="server" Text='<%# Eval("Storage_Location_Name") %>'></asp:Label>
                                            <asp:Label ID="lblPurchasingGroup" runat="server" Text='<%# Eval("Purchasing_Group") %>'></asp:Label>
                                            <asp:Label ID="lblMaterialProcessModuleId" runat="server" Text='<%# Eval("Ref_Module_Id") %>'></asp:Label>
                                            <asp:Label ID="lblSalesOrgID" runat="server" Text='<%# Eval("SalesOrg") %>'></asp:Label>
                                            <asp:Label ID="lblDistChnl" runat="server" Text='<%# Eval("DistChnl") %>'></asp:Label>
                                            <asp:Label ID="lblRequestType" runat="server" Text='<%# Eval("RequestType") %>'></asp:Label>
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
                                    <asp:BoundField HeaderText="Material Code" DataField="Material_Number" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Material Type" DataField="Material_Type" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Material" DataField="Material_Short_Description" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Manufacturer Part No" DataField="ManPartNo" HeaderStyle-HorizontalAlign="Left"
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
                                    <asp:BoundField HeaderText="Location" DataField="Location" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cost Center" DataField="Cost_Centre" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" Visible="false"
                                        HeaderText="Remarks">
                                        <ItemTemplate>
                                            <a href="#" onclick="return ShowRejectionNote('<%#Eval("Remarks") %>')" style="color: #1540c2">Rejection Note</a>
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
                        <td colspan="4" class="tdSpace"></td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace" align="right">
                            <asp:Button ID="btnDelete" runat="server" Text="Delete Request" CssClass="button"
                                OnClientClick="return ValidateChk();" Style="width: 120px" Visible="false" OnClick="btnDelete_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnMassSubmit" runat="server" Text="Mass Submit" CssClass="button"
                                OnClientClick="return ValidateChk();" Style="width: 120px" Visible="false" OnClick="btnMassSubmit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace"></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <br />
                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="button" OnClientClick="return Validate();"
                                Style="width: 100px" OnClick="btnView_Click" Visible="false" />
                            <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="button" OnClientClick="return Validate();"
                                OnClick="btnModify_Click" Visible="false" Style="width: 120px" />
                            <%--<input type="button" name="btnCreateNew" id="btnCreateNew" class="button" value="Create New"
                                style="width: 120px" onclick="return ShowCreateNewDialog();" />--%>
                            <input type="button" name="btnCreateNew" id="btnCreateNew" class="button" value="Create New"
                                style="width: 120px" onclick="return ShowCreateNewDialog();" runat="server" clientidmode="Static"/>
                            <%--  <asp:Button ID="btnChangeBulkRequestC" runat="server" Text="Change Request" CssClass="button"
                                OnClientClick="return ShowChangeBulkRequestDialog();" Width="112px" />--%>
                            <%--MSC_8300001775 commented  start--%>
                            <%--<asp:Button ID="btnChangeBulkRequestC" runat="server" Text="Change Request" CssClass="button"
                                OnClientClick="return ShowChangeBulkRequestDialog();" Width="112px" />--%>
                            <%--MSC_8300001775 commented  end--%>
                            <%--MSC_8300001775 add  start--%>
                            <asp:Button ID="btnChangeBulkRequestC" runat="server" Text="Change Request" CssClass="button"
                                OnClientClick="return ShowSMChangeDialog();" Width="112px" />
                            <%--MSC_8300001775 add  end--%>

                            


                            <%--MSE_8300002156 commented  start--%>
                            <%--<asp:Button ID="btnChangeExtensionC" runat="server" Text="Material Ext." CssClass="button"
                                OnClientClick="return ShowChangeExtensionDialog();" Width="112px" />--%>
                            <%--MSE_8300002156 commented  end--%>
                            <%--MSE_8300002156 add  start--%>
                            <asp:Button ID="btnChangeExtensionC" runat="server" Text="Material Ext." CssClass="button"
                                OnClientClick="return ShowSMExtDialog();" Width="112px" />
                            <%--MSE_8300002156 add  end--%>
                            <asp:Button ID="btnChangeRequest" runat="server" Text="Change Request" CssClass="button"
                                OnClientClick="return Validate();" Width="150px" Visible="false" OnClick="btnChangeRequest_Click" />
                            <asp:Button ID="btnBlockRequest" runat="server" Text="Block / Unblock" CssClass="button"
                                Width="150px" Visible="true" OnClientClick="return ShowBlockDialog();" />
                            <asp:Button ID="btnCopyRequest" runat="server" Text="Copy Request" CssClass="button"
                                Visible="false" OnClientClick="return ShowCopyDialog();" Width="150px" />
                            <%--DEP_05102023 add  start--%>
                            <asp:Button ID="btnCreateDepo" runat="server" Text="Child Code Request" CssClass="button"
                                OnClientClick="return ShowSingleChangeDialogdp();" Width="112px"/>
                            <%--DEP_05102023 add  end--%>
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
    <%----%>
    <div id="divModulePopUp" title="Material Master" style="display: none;">
        <asp:UpdatePanel ID="UpdModulePopUp" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">Material Type
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlModule" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlModule" runat="server" ControlToValidate="ddlModule"
                                ValidationGroup="next" ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Module.' />" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr style="display: none">
                        <td class="leftTD" width="25%">Plant Group
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
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr runat="server" id="trddlPlant" visible="true">
                        <td class="leftTD" width="25%">Plant
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                ValidationGroup="next" ErrorMessage="Select Plant." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Plant.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr runat="server" id="trddlStorageLocation" visible="true">
                        <td class="leftTD" style="width: 20%">Storage Location
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlStorageLocation" runat="server" AppendDataBoundItems="false"
                                TabIndex="2">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlStorageLocation" runat="server" ControlToValidate="ddlStorageLocation"
                                ValidationGroup="next" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr runat="server" id="trPurchasingGroup" visible="false">
                        <td class="leftTD" style="width: 20%">Purchasing Group
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlPurchasingGroup" runat="server" AppendDataBoundItems="false"
                                TabIndex="2">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlPurchasingGroup" runat="server" ControlToValidate="ddlPurchasingGroup"
                                ValidationGroup="next" ErrorMessage="Purchasing Group cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Group cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <%--Srinidhi--%>
                    <tr runat="server" id="trMarketType" visible="false">
                        <td class="leftTD" style="width: 20%">Market Type
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlMarketType" runat="server" AppendDataBoundItems="false"
                                TabIndex="2">
                                <asp:ListItem Text="Select" Value="" />
                                <asp:ListItem Text="IRF" Value="I" />
                                <asp:ListItem Text="Non Regulated" Value="R" />
                                <asp:ListItem Text="Regulated" Value="A" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlMarketType" runat="server" ControlToValidate="ddlMarketType"
                                ValidationGroup="next" ErrorMessage="Market Type cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Market Type cannot be blank.' />" />
                        </td>
                    </tr>
                    <%--Srinidhi--%>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr runat="server" id="trEmergency" visible="false">
                        <td class="leftTD" style="width: 20%">Emergency
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:CheckBox ID="chkEmergency" runat="server" Text=" Select if Emergency Request" />
                            <br />
                            <span style="color: Maroon; font-size: x-small"><i>Note: When marked as Emergency.</i>
                                <br />
                                <i>Request will be sent to approval for Procurement team (for RM/PM requests) or BFG
                                    team (for FG/SFG/ZNBW requests).</i>
                                <br />
                                <i>On approval will create only Basic data and rest will be</i>
                                <br />
                                <i>updated once other approvals are done.</i></span>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr runat="server" id="trExcise" visible="false">
                        <td class="leftTD" style="width: 20%">
                            Excise
                        </td>
                        <td class="rigthTD" style="width: 30%">
                             <asp:DropDownList ID="ddlExcise" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                                <asp:ListItem Text="Applicable" Value="1" />
                                <asp:ListItem Text="Not Applicable" Value="0" />
                            </asp:DropDownList>
                            <br />
                            <span style="color: Maroon; font-size: x-small"><i>Note: When Excise is Applicable.</i>
                                <br /><i>Request will be sent to Excise team for approval.</i>
                                <br /><i>On approval will create only Basic data and rest will be</i>
                                <br /><i>updated once other approvals are done.</i></span>
                            
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <span style="color: Orange; font-size: x-small"><i>Note: Please select the options carefully.</i><br />
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
                    <asp:Button ID="btnNext" runat="server" ValidationGroup="next" Text="Next" CssClass="button"
                        OnClick="btnNext_Click" />

                </td>
            </tr>
        </table>
    </div>
    <%----%>
    <div id="divModulePopUpCopy" title="Material Master - Copy" style="display: none;">
        <asp:UpdatePanel ID="UpdatePnlMatCopy" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">Material Type
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlModuleCopy" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlModuleCopy_SelectedIndexChanged"
                                Enabled="false" AutoPostBack="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlModuleCopy" runat="server" ControlToValidate="ddlModuleCopy"
                                ValidationGroup="Copy" ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Module.' />" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr style="display: none">
                        <td class="leftTD" width="25%">Plant Group
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlantGroupCopy" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlPlantGroupCopy" runat="server" ControlToValidate="ddlPlantGroupCopy"
                                ValidationGroup="Copy" ErrorMessage="Select Plant Group." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Plant Group.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Plant
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlantCopy" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPlantCopy_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlPlantCopy" runat="server" ControlToValidate="ddlPlantCopy"
                                ValidationGroup="Copy" ErrorMessage="Select Plant." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Plant.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" style="width: 20%">Storage Location
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlStorageLocationCopy" runat="server" AppendDataBoundItems="false"
                                TabIndex="2">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlStorageLocationCopy" runat="server" ControlToValidate="ddlStorageLocationCopy"
                                ValidationGroup="Copy" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr runat="server" id="trPurchasingGroupCopy" visible="false">
                        <td class="leftTD" style="width: 20%">Purchasing Group
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlPurchasingGroupCopy" runat="server" AppendDataBoundItems="false"
                                TabIndex="2">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlPurchasingGroupCopy" runat="server" ControlToValidate="ddlPurchasingGroupCopy"
                                ValidationGroup="Copy" ErrorMessage="Purchasing Group cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Group cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <%--Srinidhi--%>
                    <tr runat="server" id="trMarketTypeCopy" visible="false">
                        <td class="leftTD" style="width: 20%">Market Type
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlMarketTypeCopy" runat="server" AppendDataBoundItems="false"
                                TabIndex="2">
                                <asp:ListItem Text="Select" Value="" />
                                <asp:ListItem Text="IRF" Value="I" />
                                <asp:ListItem Text="Non Regulated" Value="R" />
                                <asp:ListItem Text="Regulated" Value="A" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlMarketTypeCopy" runat="server" ControlToValidate="ddlMarketTypeCopy"
                                ValidationGroup="Copy" ErrorMessage="Market Type cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Market Type cannot be blank.' />" />
                        </td>
                    </tr>
                    <%--Srinidhi--%>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr runat="server" id="trCopyEmergency" visible="false">
                        <td class="leftTD" style="width: 20%">Emergency
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:CheckBox ID="chkCopyEmergency" runat="server" Text=" Select if Emergency Request" />
                            <br />
                            <span style="color: Maroon; font-size: x-small"><i>Note: When marked as Emergency.</i>
                                <br />
                                <i>Request will be sent to approval for Procurement team (for RM/PM requests) or BFG
                                    team (for FG/SFG/ZNBW requests).</i>
                                <br />
                                <i>On approval will create only Basic data and rest will be</i>
                                <br />
                                <i>updated once other approvals are done.</i></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <span style="color: Orange; font-size: x-small"><i>Note: Please select the options carefully.</i><br />
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
                    <asp:Button ID="btnCopy" runat="server" ValidationGroup="Copy" Text="Copy" CssClass="button"
                        OnClick="btnCopy_Click" />
                </td>
            </tr>
        </table>
    </div>
    <%----%>
    <div id="divMaterialExtPopup" title="Material Master :: Change / Extension" style="display: none;">
        <asp:UpdatePanel ID="UpdMaterialPopup" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr style="display: none">
                        <td class="leftTD" width="25%">Plant Group
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlantGroupC" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlPlantGroupC" runat="server" ControlToValidate="ddlPlantGroupC"
                                Display="Dynamic" ErrorMessage="Select Plant Group." SetFocusOnError="true" Text="&lt;img src='../../images/Error.png' title='Select Plant Group.' /&gt;"
                                ValidationGroup="Extension" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                        <tr>
                            <td class="leftTD" width="25%">Plant
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlPlantC" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPlantC_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPlantC" runat="server" ControlToValidate="ddlPlantC"
                                    Display="Dynamic" ErrorMessage="Select Plant." SetFocusOnError="true" Text="&lt;img src='../../images/Error.png' title='Select Plant.' /&gt;"
                                    ValidationGroup="Extension" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" width="25%">Material Type
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlModuleC" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlModuleC_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlModuleC" runat="server" ControlToValidate="ddlModuleC"
                                    ValidationGroup="Extension" ErrorMessage="Select Module." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Module.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr runat="server" id="trPurchasingGroupC" visible="false">
                            <td class="leftTD" style="width: 20%">Purchasing Group
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlPurchasingGroupC" runat="server" AppendDataBoundItems="false"
                                    TabIndex="2">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPurchasingGroupC" runat="server" ControlToValidate="ddlPurchasingGroupC"
                                    ValidationGroup="Extension" ErrorMessage="Purchasing Group cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Group cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <%--Srinidhi--%>
                        <tr runat="server" id="trMarketTypeExt" visible="false">
                            <td class="leftTD" style="width: 20%">Market Type
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlMarketTypeExt" runat="server" AppendDataBoundItems="false"
                                    TabIndex="2">
                                    <asp:ListItem Text="Select" Value="" />
                                    <asp:ListItem Text="IRF" Value="I" />
                                    <asp:ListItem Text="Non Regulated" Value="R" />
                                    <asp:ListItem Text="Regulated" Value="A" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlMarketTypeExt" runat="server" ControlToValidate="ddlMarketTypeExt"
                                    ValidationGroup="next" ErrorMessage="Market Type cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Market Type cannot be blank.' />" />
                            </td>
                        </tr>
                        <%--Srinidhi--%>
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
                                <span style="color: Orange; font-size: x-small"><i>Note: Please select the correct Plant.</i><br />
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
                    <asp:Button ID="btnChangeExtension" runat="server" Text="Material Ext." CssClass="button"
                        ValidationGroup="Extension" OnClick="btnChangeExtension_Click" />
                </td>
            </tr>
        </table>
    </div>

    <%--/ MSE_8300002156 Start--%>

    <div id="divMatSMExtPopup" title="Material Master :: Extention Request" style="display: none;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnSingleExt" runat="server" Text="Single Material Ext." CssClass="button"
                        OnClientClick="return ShowSingleExtDialog();" />
                    <asp:Button ID="btnMassExt" runat="server" Text="Mass Material Ext." CssClass="button"
                        OnClientClick="return ShowMassExtDialog();" />
                </td>
            </tr>
        </table>
    </div>

    <div id="divMatSingleExtPopup" title="Material Master :: Single Extention Request" style="display: none;">
        <asp:UpdatePanel ID="upMatSingleExtPopup" runat="server">
            <ContentTemplate>

           <%--     <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="2">
                            <b>Material Master :: Single Extention Request will not be available from <b>11th Feb till 16th Feb</b> till S4 Hana Go live .
                                <br />
                            <br />
                        </td>

                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>  
                    <tr>

                        <td colspan="2">Click <b>"ESC"</b> button for proceed further.
                        </td>
                    </tr>
                </table>--%>

                <%--<table border="0" cellpadding="0" cellspacing="0" width="100%" style="display: none !important">--%>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">Material Code
                            <asp:Label ID="lblvExtMatCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtExtMatCode" runat="server" CssClass="textbox" MaxLength="10"
                                AutoPostBack="true" TabIndex="1" Width="180" OnTextChanged="txtExtMatCode_TextChanged" />

                            <asp:RequiredFieldValidator ID="rfvtxtExtMatCode" runat="server" ControlToValidate="txtExtMatCode"
                                ValidationGroup="fetchMatExt" ErrorMessage="Material Code cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code cannot be blank.' />" />
                            <asp:RegularExpressionValidator ID="revtxtExtMatCode" runat="server" ControlToValidate="txtExtMatCode"
                                ValidationGroup="fetchMatExt" ErrorMessage="Material Code Invalid." SetFocusOnError="true"
                                ValidationExpression="^[\d]{6,10}$" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code Invalid.' />" />

                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Material Type
                             <asp:Label ID="lblvExtMaterialType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlExtMaterialType" runat="server" AppendDataBoundItems="true" TabIndex="2" Enabled="false">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvExtMaterialType" runat="server" ControlToValidate="ddlExtMaterialType"
                                ValidationGroup="fetchMatExt" ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Module.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Plant
                             <asp:Label ID="lblvExtPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlExtPlant" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlExtPlant_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlExtPlant" runat="server" ControlToValidate="ddlExtPlant"
                                ValidationGroup="fetchExtMat" ErrorMessage="Select Plant." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Plant.' />" />
                        </td>
                    </tr>

                     <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" style="width: 20%">Storage Location
                            <asp:Label ID="lblvExtStorageLoc" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlExtStorageLoc" runat="server" AppendDataBoundItems="false"
                                TabIndex="4">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlExtStorageLoc" runat="server" ControlToValidate="ddlExtStorageLoc"
                                ValidationGroup="fetchExtMat" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                        </td>
                    </tr>

                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Reference Plant 
                            <asp:Label ID="lblvExtRefPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlExtRefPlant" runat="server" AppendDataBoundItems="true" TabIndex="3"  AutoPostBack="true"
                                OnSelectedIndexChanged="ddlExtRefPlant_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlExtRefPlant" runat="server" ControlToValidate="ddlExtRefPlant"
                                ValidationGroup="fetchExtMat" ErrorMessage="Select Reference Plant." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Reference Plant.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" style="width: 20%">Ref. Storage Location
                            <asp:Label ID="lblvExtStorageLocRef" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlExtStorageLocRef" runat="server" AppendDataBoundItems="false"
                                TabIndex="4">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlExtStorageLocRef" runat="server" ControlToValidate="ddlExtStorageLocRef"
                                ValidationGroup="fetchExtMat" ErrorMessage="Reference Storage Location cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Reference Storage Location cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                    <tr runat="server" id="trextPogrp" visible="false">
                        <td class="leftTD" style="width: 20%">Purchasing Group
                            <asp:Label ID="lblvExtPurchasingGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlExtPurchasingGroup" runat="server" AppendDataBoundItems="false"
                                TabIndex="4">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvExtPurchasingGroup" runat="server" ControlToValidate="ddlExtPurchasingGroup"
                                ValidationGroup="fetchExtMat" ErrorMessage="Purchasing Group cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Group cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                    <tr>
                        <td class="leftTD" style="width: 20%">Ref. Sales Organization
                              <asp:Label ID="lblvExtSalesOrg" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlExtSalesOrg" runat="server" AppendDataBoundItems="false"
                                AutoPostBack="true"
                                TabIndex="5" OnSelectedIndexChanged="ddlExtSalesOrg_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="0" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlExtSalesOrg" runat="server" ControlToValidate="ddlExtSalesOrg"
                                ValidationGroup="fetchExtMat" ErrorMessage="Sales Organization cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                    <tr>
                        <td class="leftTD" style="width: 20%">Ref. Distribution Channel
                            <asp:Label ID="lblvExtDistrChannel" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlExtDistrChannel" runat="server" AppendDataBoundItems="false"
                                TabIndex="6">
                                <asp:ListItem Text="Select" Value="0" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlExtDistrChannel" runat="server" ControlToValidate="ddlExtDistrChannel"
                                ValidationGroup="fetchExtMat" ErrorMessage="Distribution Channel cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />
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
                    <asp:Button ID="btnExtMatGet" runat="server" ValidationGroup="fetchExtMat" Text="Get Material" CssClass="button"
                        TabIndex="7" OnClick="btnExtMatGet_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div id="divMatMassExtPopup" title="Material Master :: Mass Extention Request" style="display: none;">
        <asp:UpdatePanel ID="upMatMassExtPopup" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                     <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                     <tr>
                        <td class="leftTD" width="25%">Material Type
                             <asp:Label ID="lblddlMaterialTypesMassExt" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlMaterialTypesMassExt" runat="server" AppendDataBoundItems="true" TabIndex="2"
                                 AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialTypesMassExt_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rvddlMaterialTypesMassExt" runat="server" ControlToValidate="ddlMaterialTypesMassExt"
                                ValidationGroup="extmassnext" ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Module.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Plant
                            <asp:Label ID="lblvExMassPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlExMassPlant" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlExMassPlant_SelectedIndexChanged" TabIndex="3">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlExMassPlant" runat="server" ControlToValidate="ddlExMassPlant"
                                ValidationGroup="extmassnext" ErrorMessage="Select Plant." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Plant.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" style="width: 20%">Storage Location
                            <asp:Label ID="lblvExtMassStorageLoc" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlExtMassStorageLoc" runat="server" AppendDataBoundItems="false"
                                TabIndex="4">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvExtMassStorageLoc" runat="server" ControlToValidate="ddlExtMassStorageLoc"
                                ValidationGroup="extmassnext" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                     <tr runat="server" id="tr10" visible="false">
                        <td class="leftTD" style="width: 20%">Purchasing Group
                            <asp:Label ID="lblvExtMassPoGroup" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlExtMassPoGroup" runat="server" AppendDataBoundItems="false"
                                TabIndex="4">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlExtMassPoGroup" runat="server" ControlToValidate="ddlExtMassPoGroup"
                                ValidationGroup="extmassnext" ErrorMessage="Purchasing Group cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Group cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" style="width: 20%">Type of Mass Extension
                            <asp:Label ID="lblvExtMassType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlExtMassType" runat="server" AppendDataBoundItems="false"
                                TabIndex="4">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlExtMassType" runat="server" ControlToValidate="ddlExtMassType"
                                ValidationGroup="extmassnext" ErrorMessage="Type of Mass Updation cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Type of Mass Updation cannot be blank.' />" />
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
                    <asp:Button ID="btnMassExtNext" runat="server" ValidationGroup="extmassnext" Text="Next" CssClass="button"
                        TabIndex="7" OnClick="btnMassExtNext_Click" />
                </td>
            </tr>
        </table>
    </div>

    <%--/ MSE_8300002156 Start--%>

    <%--MSC_8300001775 Start--%>

    <div id="divMatSMChangePopup" title="Material Master :: Change Request" style="display: none;">

        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnSinglechange" runat="server" Text="Single Change" CssClass="button"
                        OnClientClick="return ShowSingleChangeDialog();" />
                    <asp:Button ID="btnMasschange" runat="server" Text="Mass Change" CssClass="button"
                        OnClientClick="return ShowMassChangeDialog();" />

                </td>
            </tr>
        </table>
    </div>

    <div id="divMatSingleChangePopup" title="Material Master :: Single Change Request" style="display: none;">
        <asp:UpdatePanel ID="upMatSingleChang" runat="server">
            <ContentTemplate>
               <%--<table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="2">
                            <b>Material Master :: Single Change Request will not be available from <b>11th Feb till 16th Feb</b> till S4 Hana Go live .
                                <br />
                            <br />
                        </td>

                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>  
                    <tr>

                        <td colspan="2">Click <b>"ESC"</b> button for proceed further.
                        </td>
                    </tr>
                </table>--%>

                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">Material Code
                            <asp:Label ID="labletxtMaterialCodes" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtMaterialCodes" runat="server" CssClass="textbox" MaxLength="10"
                                AutoPostBack="true" TabIndex="1" Width="180" OnTextChanged="txtMaterialCode_TextChanged" />
                            <asp:RequiredFieldValidator ID="reqtxtMaterialCode" runat="server" ControlToValidate="txtMaterialCodes"
                                ValidationGroup="fetchMat" ErrorMessage="Material Code cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code cannot be blank.' />" />
                            <asp:RegularExpressionValidator ID="regtxtMaterialCode" runat="server" ControlToValidate="txtMaterialCodes"
                                ValidationGroup="fetchMat" ErrorMessage="Material Code Invalid." SetFocusOnError="true"
                                ValidationExpression="^[\d]{6,10}$" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code Invalid.' />" />

                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Material Type
                             <asp:Label ID="lblddlMaterialTypes" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <%--  AutoPostBack="true"
                                OnSelectedIndexChanged="ddlMaterialTypes_SelectedIndexChanged"--%>
                            <asp:DropDownList ID="ddlMaterialTypes" runat="server" AppendDataBoundItems="true" TabIndex="2" Enabled="false">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rvddlMaterialTypes" runat="server" ControlToValidate="ddlMaterialTypes"
                                ValidationGroup="fetchMat" ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Module.' />" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr style="display: none">
                        <td class="leftTD" width="25%">Plant Group
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlantGroups" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlPlantGroups" runat="server" ControlToValidate="ddlPlantGroups"
                                ValidationGroup="fetchMat" ErrorMessage="Select Plant Group." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Plant Group.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr runat="server" id="tr1" visible="true">
                        <td class="leftTD" width="25%">Plant
                            <asp:Label ID="lblddlPlants" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlants" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPlants_SelectedIndexChanged" TabIndex="3">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlPlants" runat="server" ControlToValidate="ddlPlants"
                                ValidationGroup="fetchMat" ErrorMessage="Select Plant." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Plant.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr runat="server" id="tr2" visible="true">
                        <td class="leftTD" style="width: 20%">Storage Location
                            <asp:Label ID="lblddlStorageLocs" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlStorageLocs" runat="server" AppendDataBoundItems="false"
                                TabIndex="4">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlStorageLocs" runat="server" ControlToValidate="ddlStorageLocs"
                                ValidationGroup="fetchMat" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                    <tr runat="server" id="tr5" visible="false">
                        <td class="leftTD" style="width: 20%">Purchasing Group
                            <asp:Label ID="lblddlPurchasingGroups" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlPurchasingGroups" runat="server" AppendDataBoundItems="false"
                                TabIndex="4">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlPurchasingGroups" runat="server" ControlToValidate="ddlPurchasingGroups"
                                ValidationGroup="fetchMat" ErrorMessage="Purchasing Group cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Group cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                    <tr runat="server" id="tr3" visible="true">
                        <td class="leftTD" style="width: 20%">Sales Organization
                              <asp:Label ID="lblddlSalesOrgs" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlSalesOrgs" runat="server" AppendDataBoundItems="false"
                                AutoPostBack="true"
                                TabIndex="5" OnSelectedIndexChanged="ddlSalesOrgs_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="0" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlSalesOrgs" runat="server" ControlToValidate="ddlSalesOrgs"
                                Visible="false" ValidationGroup="fetchMat" ErrorMessage="Sales Organization cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                    <tr runat="server" id="tr4" visible="true">
                        <td class="leftTD" style="width: 20%">Distribution Channel
                            <asp:Label ID="lblddlDistributionChannels" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlDistributionChannels" runat="server" AppendDataBoundItems="false"
                                TabIndex="6">
                                <asp:ListItem Text="Select" Value="0" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlDistributionChannels" runat="server" ControlToValidate="ddlDistributionChannels"
                                Visible="false" ValidationGroup="fetchMat" ErrorMessage="Distribution Channel cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" >
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnSelectSec" runat="server" ValidationGroup="fetchMat" Text="Next" CssClass="button"
                        TabIndex="7" OnClick="btnSelectSec_Click" />
                    <%-- <asp:Button ID="Button1" runat="server" Text="Next" CssClass="button" ValidationGroup="fetchMat"
                        OnClientClick="return ShowSectionSMDialog();" />--%>
                </td>
            </tr>
        </table>
    </div>

    <div id="divMatMassChangePopup" title="Material Master :: Mass Change Request" style="display: none;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">

                    <%--  <tr>
                        <td class="leftTD" width="25%">Material Type
                            <asp:Label ID="lblddlMaterialTypem" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                             AutoPostBack="true"
                                OnSelectedIndexChanged="ddlMaterialTypes_SelectedIndexChanged"
                            <asp:DropDownList ID="ddlMaterialTypem" runat="server" AppendDataBoundItems="true" TabIndex="2">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            
                        </td>
                    </tr>--%>

                    <tr>
                        <td class="leftTD" width="25%">Material Type
                             <asp:Label ID="lblddlMaterialTypesMass" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlMaterialTypesMass" runat="server" AppendDataBoundItems="true" TabIndex="2" AutoPostBack="true"
                                 OnSelectedIndexChanged="ddlMaterialTypesMass_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rvddlMaterialTypesMass" runat="server" ControlToValidate="ddlMaterialTypesMass"
                                ValidationGroup="massnext" ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Module.' />" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr style="display: none">
                        <td class="leftTD" width="25%">Plant Group
                            <asp:Label ID="lblddlPlantGroupm" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlantGroupm" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlPlantGroupm" runat="server" ControlToValidate="ddlPlantGroupm"
                                ValidationGroup="massnext" ErrorMessage="Select Plant Group." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Plant Group.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr runat="server" id="tr6" visible="true">
                        <td class="leftTD" width="25%">Plant
                            <asp:Label ID="lblddlPlantm" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlantm" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPlantm_SelectedIndexChanged" TabIndex="3">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlPlantm" runat="server" ControlToValidate="ddlPlantm"
                                ValidationGroup="massnext" ErrorMessage="Select Plant." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Plant.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr runat="server" id="tr7" visible="true">
                        <td class="leftTD" style="width: 20%">Storage Location
                            <asp:Label ID="lblddlStorageLocm" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlStorageLocm" runat="server" AppendDataBoundItems="false"
                                TabIndex="4">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlStorageLocm" runat="server" ControlToValidate="ddlStorageLocm"
                                ValidationGroup="massnext" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                    <tr runat="server" id="tr8" visible="false">
                        <td class="leftTD" style="width: 20%">Purchasing Group
                            <asp:Label ID="lblddlPurchasingGroupm" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlPurchasingGroupm" runat="server" AppendDataBoundItems="false"
                                TabIndex="4">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlPurchasingGroupm" runat="server" ControlToValidate="ddlPurchasingGroupm"
                                ValidationGroup="massnext" ErrorMessage="Purchasing Group cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Group cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr runat="server" id="tr9">
                        <td class="leftTD" style="width: 20%">Type of Mass Updation
                            <asp:Label ID="lblddlTypeOfMassUpdm" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlTypeOfMassUpdm" runat="server" AppendDataBoundItems="false"
                                TabIndex="4">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlTypeOfMassUpdm" runat="server" ControlToValidate="ddlTypeOfMassUpdm"
                                ValidationGroup="massnext" ErrorMessage="Type of Mass Updation cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Type of Mass Updation cannot be blank.' />" />
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
                    <asp:Button ID="btnMassNext" runat="server" ValidationGroup="massnext" Text="Next" CssClass="button"
                        TabIndex="7" OnClick="btnMassNext_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div id="divSectionSMPopup" title="Section Selection" style="display: none;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="2">
                    <asp:Panel ID="panelscm" runat="server" Visible="false">
                        <asp:Label ID="lblscm" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnGoback" runat="server" OnClientClick="return ShowSingleChangeDialog();" Text="Go back" CssClass="button"
                        TabIndex="7" Visible="false" />
                    <asp:Button ID="btnGetFromSAP" runat="server" OnClientClick="return ValidateChkSC();" Text="Fetch Material" CssClass="button"
                        TabIndex="7" OnClick="btnGetFromSAP_Click" />
                    <asp:Label ID="lblGetfromSAP" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">

                    <tr>
                        <td colspan="2">
                            <br />
                            <asp:GridView ID="gvSelectSec" runat="server" AutoGenerateColumns="false" Width="100%"
                                BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both">
                                <RowStyle CssClass="light-gray" />
                                <HeaderStyle CssClass="gridHeader" />
                                <AlternatingRowStyle CssClass="gridRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSection_Id" runat="server" Text='<%# Eval("Section_Id") %>'></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <input type="checkbox" id="chkSelectSectionAll" name="chkSelectSectionAll" title="Select All"
                                                onclick="return fnChkSelectSectionAll();" />
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectionSec" runat="server" GroupName="selection" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Section Name" DataField="Section_Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                </Columns>
                            </asp:GridView>

                            <script type="text/javascript">

                                function ValidateChkSC() {

                                    var gv = document.getElementById("<%=gvSelectSec.ClientID%>");
                                    var rbs = gv.getElementsByTagName("input");
                                    var flag = 0;
                                    for (var i = 0; i < rbs.length; i++) {

                                        if (rbs[i].type == "checkbox") {
                                            if (rbs[i].checked) {
                                                flag = flag + 1;
                                                if (flag > 1) {
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if (flag < 1) {
                                        alert("Please Select a section to next");
                                        return false;
                                    }
                                }
                                function GetChildControl(element, id) {
                                    var child_elements = element.getElementsByTagName("*");
                                    for (var i = 0; i < child_elements.length; i++) {
                                        if (child_elements[i].id.indexOf(id) != -1) {
                                            return child_elements[i];
                                        }
                                    }
                                };

                                function fnChkSelectSectionAll() {
                                    var gvall = document.getElementById("chkSelectSectionAll");
                                    var gv = document.getElementById("<%=gvSelectSec.ClientID%>");
                                    var rows = document.getElementById("<%=gvSelectSec.ClientID %>").getElementsByTagName("tr");
                                    var rbs = gv.getElementsByTagName("input");
                                    //debugger;
                                    //var rbsspan = gv.getElementsByTagName("span");

                                    var flag = 0;
                                    for (var i = 0; i < rbs.length; i++) {
                                        if (rbs[i].type == "checkbox" && rbs[i].name != "chkSelectSectionAll") {
                                            //var labels = GetChildControl(rows[i], "lblSection_Id");
                                            //var labels = gv.rows[i].cells[0].getElementsByTagName("span");
                                            //debugger;
                                            //if (labels.innerHTML == '3') {
                                            //    debugger;
                                            //    rbs[i].checked = true;
                                            //}
                                            //else {
                                            //var test = rows[i].cells[0].innerHTML;
                                            //    rbs[i].checked = gvall.checked
                                            //}

                                            //var labels = rows[i].cells[1].innerHTML

                                            //if (labels == 'Basic Data') {
                                            //    //rbs[i].checked = true;
                                            //    //rbs[i].readOnly = true;
                                            //}
                                            //else {
                                            rbs[i].checked = gvall.checked
                                            //}

                                        }
                                    }
                                }


                              <%--  function fnChkSelectSectionAll() { 
                                    var gvall = document.getElementById("chkSelectSectionAll");
                                    var gv = document.getElementById("<%=gvSelectSec.ClientID%>");
                                    var rbs = gv.getElementsByTagName("input");
                                    var flag = 0;
                                    for (var i = 0; i < rbs.length; i++) {
                                        if (rbs[i].type == "checkbox" && rbs[i].name != "chkSelectSectionAll") {

                                            //alert(rbs[i].name);
                                            rbs[i].checked = gvall.checked//flag;
                                        }
                                    }
                                }--%>


</script>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

    <div id="divConfirmSMPopup" title="Material Master :: Confirmation" style="display: none;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="2">
                    <asp:Panel ID="panelSMConf" runat="server" Visible="false">
                        <asp:Label ID="lblSMConf" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="centerTD" colspan="2">
                    <%-- <asp:HyperLink ID="btnSMConfOk" runat="server" CssClass="button" Text="Ok" 
                        OnClientClick="return ShowConfirmSMDialog();"></asp:HyperLink>--%>
                    <asp:Button ID="btnSMConfCancel" runat="server" Text="Confirm"
                        CssClass="button" />
                </td>
            </tr>
        </table>
        <%--<iframe id="Iframe1" runat="server" width="100%" height="100%" src="GetMatFromSAP.aspx"></iframe>--%>
    </div>

    <%--MSC_8300001775 End--%>





    <%--DEP_05102023 End--%>
    <div id="divMatSingleChangePopupdp" title="Material Master :: Child Code Request" style="display: none;">
        <asp:UpdatePanel ID="upMatSingleChangdp" runat="server">
            <ContentTemplate> 
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="leftTD" width="25%">Ref. Material Code
                            <asp:Label ID="labletxtMaterialCodesdp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtMaterialCodesdp" runat="server" CssClass="textbox" MaxLength="10"
                                AutoPostBack="true" TabIndex="1" Width="180" OnTextChanged="txtMaterialCodedp_TextChanged" />
                            <asp:RequiredFieldValidator ID="reqtxtMaterialCodedp" runat="server" ControlToValidate="txtMaterialCodesdp"
                                ValidationGroup="fetchMatdp" ErrorMessage="Material Code cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code cannot be blank.' />" />
                            <asp:RegularExpressionValidator ID="regtxtMaterialCodedp" runat="server" ControlToValidate="txtMaterialCodesdp"
                                ValidationGroup="fetchMatdp" ErrorMessage="Material Code Invalid." SetFocusOnError="true"
                                ValidationExpression="^[\d]{6,10}$" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code Invalid.' />" />

                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" width="25%">Material Type
                             <asp:Label ID="lblddlMaterialTypesdp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD"> 
                            <asp:DropDownList ID="ddlMaterialTypesdp" runat="server" AppendDataBoundItems="true" TabIndex="2" Enabled="false">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rvddlMaterialTypesdp" runat="server" ControlToValidate="ddlMaterialTypesdp"
                                ValidationGroup="fetchMatdp" ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Module.' />" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr style="display: none">
                        <td class="leftTD" width="25%">Ref. Plant Group
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlantGroupsdp" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlPlantGroupsdp" runat="server" ControlToValidate="ddlPlantGroupsdp"
                                ValidationGroup="fetchMatdp" ErrorMessage="Select Plant Group." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Plant Group.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr runat="server" id="tr11" visible="true">
                        <td class="leftTD" width="25%">Ref. Plant
                            <asp:Label ID="lblddlPlantsdp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlantsdp" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPlantsdp_SelectedIndexChanged" TabIndex="3">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlPlantsdp" runat="server" ControlToValidate="ddlPlantsdp"
                                ValidationGroup="fetchMatdp" ErrorMessage="Select Plant." SetFocusOnError="true" Display="Dynamic"
                                Text="<img src='../../images/Error.png' title='Select Plant.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr runat="server" id="tr12" visible="true">
                        <td class="leftTD" style="width: 20%">Ref. Storage Location
                            <asp:Label ID="lblddlStorageLocsdp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlStorageLocsdp" runat="server" AppendDataBoundItems="false"
                                TabIndex="4">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlStorageLocsdp" runat="server" ControlToValidate="ddlStorageLocsdp"
                                ValidationGroup="fetchMatdp" ErrorMessage="Storage Location cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                    <tr runat="server" id="tr13" visible="false">
                        <td class="leftTD" style="width: 20%">Ref. Purchasing Group
                            <asp:Label ID="lblddlPurchasingGroupsdp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlPurchasingGroupsdp" runat="server" AppendDataBoundItems="false"
                                TabIndex="4">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlPurchasingGroupsdp" runat="server" ControlToValidate="ddlPurchasingGroupsdp"
                                ValidationGroup="fetchMatdp" ErrorMessage="Purchasing Group cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Purchasing Group cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                    <tr runat="server" id="tr14" visible="true">
                        <td class="leftTD" style="width: 20%">Ref. Sales Organization
                              <asp:Label ID="lblddlSalesOrgsdp" runat="server" ForeColor="Red" Text="*" ></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlSalesOrgsdp" runat="server" AppendDataBoundItems="false"
                                AutoPostBack="true"
                                TabIndex="5" OnSelectedIndexChanged="ddlSalesOrgsdp_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlSalesOrgsdp" runat="server" ControlToValidate="ddlSalesOrgsdp"
                                ValidationGroup="fetchMatdp" ErrorMessage="Sales Organization cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                    <tr runat="server" id="tr15" visible="true">
                        <td class="leftTD" style="width: 20%">Ref. Distribution Channel
                            <asp:Label ID="lblddlDistributionChannelsdp" runat="server" ForeColor="Red" Text="*" ></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 30%">
                            <asp:DropDownList ID="ddlDistributionChannelsdp" runat="server" AppendDataBoundItems="false"
                                TabIndex="6">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlDistributionChannelsdp" runat="server" ControlToValidate="ddlDistributionChannelsdp"
                                  ValidationGroup="fetchMatdp" ErrorMessage="Distribution Channel cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>

                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" >
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnGetFromSAPdp" runat="server" ValidationGroup="fetchMatdp" Text="Get Material" CssClass="button"
                        TabIndex="7" OnClick="btnGetFromSAPdp_Click" /> 
                </td>
            </tr>
        </table>
    </div>
    <%--DEP_05102023 End--%>

    <div id="divMaterialBlockPopup" title="Material Master :: Block / Unblock" style="display: none;">
        <asp:UpdatePanel ID="UpdMaterialBlock" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr style="display: none">
                        <td class="leftTD" width="25%">Plant Group
                        </td>
                        <td class="rigthTD">
                            <asp:DropDownList ID="ddlPlantGroupB" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlPlantGroupB" runat="server" ControlToValidate="ddlPlantGroupB"
                                Display="Dynamic" ErrorMessage="Select Plant Group." SetFocusOnError="true" Text="&lt;img src='../../images/Error.png' title='Select Plant Group.' /&gt;"
                                ValidationGroup="MaterialBlock" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                        <tr>
                            <td class="leftTD" width="25%">Plant
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlPlantB" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPlantB" runat="server" ControlToValidate="ddlPlantB"
                                    Display="Dynamic" ErrorMessage="Select Plant." SetFocusOnError="true" Text="&lt;img src='../../images/Error.png' title='Select Plant.' /&gt;"
                                    ValidationGroup="MaterialBlock" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" width="25%">Material Type
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlModuleB" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlModuleB" runat="server" ControlToValidate="ddlModuleB"
                                    ValidationGroup="MaterialBlock" ErrorMessage="Select Module." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Module.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="2">
                                <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#008080"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" colspan="2">
                                <span style="color: Orange; font-size: x-small"><i>Note: Please select the correct Plant.</i><br />
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
                    <asp:Button ID="btnBlock" runat="server" ValidationGroup="MaterialBlock" Text="Block"
                        CssClass="button" OnClick="btnBlockRequest_Click" />
                    <asp:Button ID="btnUnBlock" runat="server" ValidationGroup="MaterialBlock" Text="UnBlock"
                        CssClass="button" OnClick="btnUnBlockRequest_Click" />
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

    <%--MSC_8300001775 Start--%>
    <asp:ValidationSummary ID="vsSC" runat="server" ValidationGroup="fetchMat" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:ValidationSummary ID="vsSM" runat="server" ValidationGroup="massnext" ShowMessageBox="true"
        ShowSummary="false" />
    <%--MSC_8300001775 end--%>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="next" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="MaterialBlock"
        ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Extension"
        ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Copy"
        ShowMessageBox="true" ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblPk" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <div id="dhtmltooltip" class="dhtmltooltip">
    </div>
    <script src="../../Tooltip/toolTip.js" type="text/javascript"></script>
    <script type="text/javascript">

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


        function ShowChangeBulkRequestDialog() {
            $("#divMaterialExtPopup").dialog({
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

            $('#<%= btnChangeBulkRequest.ClientID%>')._show();
            $('#<%= btnChangeExtension.ClientID%>')._hide();
        }

        function ShowChangeExtensionDialog() {
            $("#divMaterialExtPopup").dialog({
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

            $('#<%= btnChangeBulkRequest.ClientID%>')._hide();
            $('#<%= btnChangeExtension.ClientID%>')._show();
        }

        //MSC_8300001775 Start

        function ShowSMChangeDialog() {
            $("#divMatSMChangePopup").dialog({
                height: 100,
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
        }

        function ShowMassChangeDialog() {
            $("#divMatMassChangePopup").dialog({
                height: 300,
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
        }

        function ShowSingleChangeDialog() {
            $("#divMatSingleChangePopup").dialog({
                height: 300,
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

        }

        /*DEP_05102023 Start*/
        function ShowSingleChangeDialogdp() {
            $("#divMatSingleChangePopupdp").dialog({
                height: 300,
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

        }
        /*DEP_05102023 End*/
        function ShowSectionSMDialog() {
            $("#divSectionSMPopup").dialog({
                height: 500,
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
        }

        function ShowConfirmSMDialog() {
            $("#divConfirmSMPopup").dialog({
                height: 120,
                width: 500,
                modal: true,
                closeOnEscape: false,
                draggable: true,
                resizable: false,
                position: 'center',
                dialogClass: 'alert',
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                    $(this).show('clip');
                }
            });
            //return false;
        }


        //MSC_8300001775 End

        //MSE_8300002156 Start

        function ShowSMExtDialog() {
            $("#divMatSMExtPopup").dialog({
                height: 100,
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
        }


        function ShowSingleExtDialog() {
            $("#divMatSingleExtPopup").dialog({
                height: 400,
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
        }

        function ShowMassExtDialog() {
            $("#divMatMassExtPopup").dialog({
                height: 300,
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
        }

        //MSE_8300002156 End


        function ShowBlockDialog() {
            $("#divMaterialBlockPopup").dialog({
                height: 240,
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
        }

        function ShowCopyDialog() {
            if (Validate()) {
                $("#divModulePopUpCopy").dialog({
                    height: 240,
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
            }
        }


    </script>
</asp:Content>
