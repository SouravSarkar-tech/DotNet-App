<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/MasterPage.master" AutoEventWireup="true" CodeFile="BOMRecipeMassProcess.aspx.cs" 
    Inherits="Transaction_BOMRecipe_BOMRecipeMassProcess" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <link href="../../stylesheet/main.css" rel="stylesheet" type="text/css" />
    <link href="../../stylesheet/autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="../../stylesheet/Paging.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdMassBOMRecipeProcess" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                <ContentTemplate>
                    <asp:Panel ID="pnlSearch" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" width="99%" class="gridHeader">
                            <tr>
                                <td>
                                    <b>Requestor Name.:</b>&nbsp;
                                    <asp:Label ID="lblRequestor" runat="server" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp; <b>Location:</b>&nbsp;
                                    <asp:Label ID="lblLocation" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
                                    <b>Contact No:</b>&nbsp;
                                    <asp:Label ID="lblContactNo" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdSpace" style="border-bottom: 1px solid Black;">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Request No.:</b>&nbsp;
                                    <asp:Label ID="lblRequestNo" runat="server" />
                                    &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp; <b>Module Type:</b>&nbsp;
                                    <asp:Label ID="lblSelectedModule" runat="server" />&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
                                   <%-- <b>Material Code:</b>&nbsp;
                                    <asp:Label ID="lblMaterialNo" runat="server" />--%>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="trHeading" align="center" colspan="4">
                                    Mass BOM Recipe Process
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="tdSpace">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
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
                                        BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both" OnRowDataBound="grdSearch_RowDataBound">
                                        <RowStyle CssClass="light-gray" />
                                        <HeaderStyle CssClass="gridHeader" />
                                        <AlternatingRowStyle CssClass="gridRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="rdoSelection" runat="server" GroupName="selection" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMassRequestProcessDetailId" runat="server" Text='<%# Eval("Mass_Request_Process_Detail_Id") %>'></asp:Label>
                                                    <asp:Label ID="lblPrimaryID" runat="server" Text='<%# Eval("Master_Header_Id") %>'></asp:Label>
                                                    <asp:Label ID="lblModuleId" runat="server" Text='<%# Eval("Module_Id") %>'></asp:Label>
                                                    <asp:Label ID="lblModuleName" runat="server" Text='<%# Eval("Module_Name") %>'></asp:Label>
                                                    <asp:Label ID="lblRequestNo" runat="server" Text='<%# Eval("Request_No") %>'></asp:Label>
                                                    <asp:Label ID="lblRequestStatus" runat="server" Text='<%# Eval("Request_Status") %>'></asp:Label>
                                                    <asp:Label ID="lblMasterCode" runat="server" Text='<%# Eval("Material_Number") %>'></asp:Label>
                                                    <asp:Label ID="lblActionType" runat="server" Text='<%# Eval("Action_Type") %>'></asp:Label>
                                                    <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("Created_By") %>'></asp:Label>
                                                    <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                                    <asp:Label ID="lblContactNo" runat="server" Text='<%# Eval("ContactNo") %>'></asp:Label>
                                                    <asp:Label ID="lblPlantId" runat="server" Text='<%# Eval("Mat_Plant_Id") %>'></asp:Label>                                                    
                                                    <asp:Label ID="lblPlantGroupId" runat="server" Text='<%# Eval("Plant_Group_Id") %>'></asp:Label>                                                    
                                                    <asp:Label ID="lblRequestType" runat="server" Text='<%# Eval("RequestType") %>'></asp:Label>   
                                                    <asp:Label ID="lblPlantType" runat="server" Text='<%# Eval("Plant_Type") %>'></asp:Label>                                               
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub - Request No." ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    &nbsp;&nbsp;<asp:LinkButton ID="lnkWorkflow" runat="server" Text='<%# Eval("Request_No") %>'
                                                        OnClientClick='<%# string.Format( "OpenRequestHistory(\"{0}\");", Eval("Master_Header_Id")) %>'></asp:LinkButton>
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
                                            <%--<asp:BoundField HeaderText="Material" DataField="Material_Short_Description" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Manufacturer Part No" DataField="ManPartNo" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField HeaderText="Req. Dt." DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-Width="100px" Visible="false" HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <%-- <a href="#" onclick="return ShowRejectionNote('<%#Eval("Remarks") %>')" style="color: #1540c2">
                                                        Rejection Note</a>--%>
                                                    <asp:Label ID="lblRejectTo" runat="server" Text="Reject To :" Font-Bold="true" Font-Underline="true" />
                                                    <asp:DropDownList ID="ddlReject_To" runat="server" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="Select" Value="" />
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqddlReject_To" SetFocusOnError="true" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Select Department.'>" ControlToValidate="ddlReject_To"
                                                        InitialValue="" runat="server" ForeColor="Red" ValidationGroup='<%# Eval("Request_No") %>' />
                                                   <%-- <asp:CheckBoxList ID="ChkReject_To" runat="server" RepeatDirection="Vertical">
                                                    </asp:CheckBoxList>--%>
                                                    <hr />
                                                    <asp:Label ID="lblRejectionRemark" runat="server" Text="Rejection Remarks :" Font-Bold="true"
                                                        Font-Underline="true" />
                                                    <br />
                                                    <asp:TextBox ID="txtRejectionNote" runat="server" TextMode="MultiLine" Rows="3" Columns="130"
                                                        CssClass="textarea" Text=''></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqtxtRejectionNote" SetFocusOnError="true" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Enter Remarks.'>" ControlToValidate="txtRejectionNote"
                                                        runat="server" ForeColor="Red" ValidationGroup='<%# Eval("Request_No") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" Visible="false"
                                                HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnReject_Ind" runat="server" Text="Reject" CssClass="button" ValidationGroup='<%# Eval("Request_No") %>'
                                                        OnClick="btnReject_Ind_Click" />
                                                    <asp:Button ID="btnComplete" runat="server" Text="Complete" CssClass="button" OnClick="btnComplete_Click" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <script type="text/javascript">
                                        function Validate() {
                                            var gv = document.getElementById("<%=grdSearch.ClientID%>");
                                            var rbs = gv.getElementsByTagName("input");
                                            var flag = 0;
                                            for (var i = 0; i < rbs.length; i++) {

                                                if (rbs[i].type == "radio") {
                                                    if (rbs[i].checked) {
                                                        flag = 1;
                                                        break;
                                                    }
                                                }
                                            }
                                            if (flag == 0) {
                                                alert("Kindly Select A Record");
                                                return false;
                                            }
                                        }</script>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="tdSpace">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <br />
                                    <br />
                                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="button" OnClientClick="return Validate();"
                                        Style="width: 100px" OnClick="btnView_Click" Visible="false" />
                                    <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="button" OnClientClick="return Validate();"
                                        OnClick="btnModify_Click" Visible="false" Style="width: 100px" />
                                    <asp:Button ID="btnSAPValidate" runat="server" Text="Validate" CssClass="button"
                                        OnClientClick="return ShowValidateBOMPopup();" Visible="false" />
                                    <asp:Button ID="btnSAPUpload" runat="server" Text="SAP Integration" CssClass="button"
                                        OnClientClick="return ShowSAPUploadPopup();" Visible="false" />
                                    <asp:Button ID="btnSAPQAUpload" runat="server" Text="Release Recipe" CssClass="button"
                                        OnClientClick="return ShowSAPQAUploadPopup();" Visible="false" />
                                    <asp:Button ID="btnSubmit" runat="server" Text="Approve All" CssClass="button" OnClick="btnSubmit_Click"
                                        OnClientClick="return confirm('Are you sure you want to approve all?');" Visible="false"
                                        Style="width: 100px" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click"
                                        Style="width: 100px" />
                                    <asp:Button ID="btnRejectTo" runat="server" Text="Reject All" CssClass="button" OnClick="btnRejectTo_Click"
                                        Style="width: 100px" />
                                    <asp:Button ID="btnUnGroup" runat="server" Text="Withdraw from Group" CssClass="button"
                                        Style="width: 180px" OnClick="btnUnGroup_Click" />
                                </td>
                            </tr>
                        </table>
                        <div id="divRejectTo" title="Reject Request" runat="server" visible="false">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
                                <tr>
                                    <td class="leftTD" style="width: 25%">
                                        Reject To
                                    </td>
                                    <td class="rigthTD" runat="server" id="tdDdlReject">
                                        <asp:DropDownList ID="ddlRejectTo" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlRejectTo" SetFocusOnError="true" Display="Dynamic"
                                            Text="<img src='../../images/Error.png' title='Select Department.'" ControlToValidate="ddlRejectTo"
                                            InitialValue="" runat="server" ForeColor="Red" ValidationGroup="reject" />
                                    </td>
                                    <td class="rigthTD" runat="server" id="tdChkReject" visible="false">
                                        <asp:CheckBoxList ID="ChkRejectTo" runat="server" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                        <asp:CustomValidator ID="reqChkRejectTo" ErrorMessage="Please select at least one item."
                                            ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server"
                                            ValidationGroup="reject" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="tdSpace">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" valign="top">
                                        Remark
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtRejectNote" runat="server" CssClass="textarea" TextMode="MultiLine"
                                            Height="40px" Width="350px" Style="text-transform: none;" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Enter Remark.'"
                                            ControlToValidate="txtRejectNote" runat="server" ForeColor="Red" ValidationGroup="reject" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="tdSpace">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        &nbsp;
                                    </td>
                                    <td class="rigthTD">
                                        <asp:Button ID="btnRollback" runat="server" Text="Reject" CssClass="button" OnClick="btnRollback_Click"
                                            ValidationGroup="reject" />
                                        <asp:Button ID="btnRejCanel" runat="server" Text="Cancel Reject" CssClass="button"
                                            OnClick="btnRejCanel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divSAPUpload" style="display: none;" title="Upload To SAP">
                            <iframe id="Iframe1" runat="server" width="100%" height="100%" src="MassSAPIntegration.aspx">
                            </iframe>
                        </div>
                         <div id="divBOMValidate" style="display: none;" title="Validate BOM Data">
                            <iframe id="Iframe2" runat="server" width="100%" height="100%" src="MassSAPValidation.aspx">
                            </iframe>
                        </div>
                        <%--<div id="divModuleQA" title="User Entry" style="display: none;">
                            <asp:UpdatePanel ID="UpdatePnlQA" runat="server">
                                <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="leftTD" width="25%">
                                                User
                                            </td>
                                            <td class="rigthTD">
                                                <asp:TextBox ID="txtUserName" CssClass="textboxSimple" runat="server" size="30"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                                                    ErrorMessage="User Name is required." ToolTip="User Name is required." SetFocusOnError="true"
                                                    ValidationGroup="ctlLogin" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftTD" width="25%">
                                                Password
                                            </td>
                                            <td class="rigthTD">
                                                <asp:TextBox ID="txtPassword" runat="server" CssClass="textboxSimple" TextMode="Password"
                                                    size="30"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                                    Display="None" ErrorMessage="Password is required." ToolTip="Password is required."
                                                    ValidationGroup="ctlLogin" SetFocusOnError="true"></asp:RequiredFieldValidator>
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
                                    <td class="centerTD" colspan = "2" >
                                        <asp:Button ID="btnDone" runat="server" ValidationGroup="ctlLogin" Text="Done"
                                            CssClass="button" OnClick="btnDone_Click"/>                                      
                                    </td>                
                                </tr>
                            </table>
                        </div>--%>

                        <div id="divSAPQAUpload" style="display: none;" title="Release">        
                            <iframe id="Iframe3" runat="server" width="100%" height="100%" src="MassSAPQAIntegration.aspx">        
                            </iframe>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMassRequestProcessId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblMRPId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="9" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
            <asp:Label ID="lblRejectionType" runat="server" Visible="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ShowSAPUploadPopup() {
            $("#divSAPUpload").dialog({
                height: 500,
                width: 850,
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
                },
                close: function (type, data) {
                    window.location.href = window.location.pathname;
                }
            });
            return false;
        }

        function RequestSubmitPage() {
            window.location.assign("BOMRecipeMaster.aspx")

        }

        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=ChkRejectTo.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }

        function ShowValidateBOMPopup() {
            $("#divBOMValidate").dialog({
                height: 500,
                width: 850,
                modal: true,
                closeOnEscape: true,
                draggable: true,
                resizable: false,
                position: 'center',
                dialogClass: 'alert',
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                    $(this).show('clip');
                },
                close: function (type, data) {
                    window.location.href = window.location.pathname;
                }
            });
            return false;
        }

//        function ShowQADialog() {

//            $("#divModuleQA").dialog({
//                height: 500,
//                width: 850,
//                modal: true,
//                closeOnEscape: true,
//                draggable: true,
//                resizable: false,
//                position: 'center',
//                dialogClass: 'alert',
//                open: function (type, data) {
//                    $(this).parent().appendTo("form");
//                    $(this).show('clip');
//                },
//                close: function (type, data) {
//                    window.location.href = window.location.pathname;
//                }
//            });
//            return false;

//        }

        function ShowSAPQAUploadPopup() {
            $("#divSAPQAUpload").dialog({
                height: 500,
                width: 850,
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
                },
                close: function (type, data) {
                    window.location.href = window.location.pathname;
                }
            });
            return false;
        }
       
    </script>
</asp:Content>

