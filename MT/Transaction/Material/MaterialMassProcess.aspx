<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/MasterPage.master"
    AutoEventWireup="true" CodeFile="MaterialMassProcess.aspx.cs" Inherits="Transaction_Material_MaterialMassProcess" %>

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
    <asp:UpdatePanel ID="UpdMassMaterialProcess" runat="server" RenderMode="Inline">
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
                                    &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp; <b>Material Type:</b>&nbsp;
                                    <asp:Label ID="lblSelectedModule" runat="server" />&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
                                    <b>Material Code:</b>&nbsp;
                                    <asp:Label ID="lblMaterialNo" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="trHeading" align="center" colspan="4">
                                    Mass Material Process
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
                                                    <asp:Label ID="lblStorageLocation" runat="server" Text='<%# Eval("Storage_Location") %>'></asp:Label>
                                                    <asp:Label ID="lblPlantGroupId" runat="server" Text='<%# Eval("Plant_Group_Id") %>'></asp:Label>
                                                    <asp:Label ID="lblMaterialShortDescription" runat="server" Text='<%# Eval("Material_Short_Description") %>'></asp:Label>
                                                    <asp:Label ID="lblStorageLocationName" runat="server" Text='<%# Eval("Storage_Location_Name") %>'></asp:Label>
                                                    <asp:Label ID="lblSalesOrg" runat="server" Text='<%# Eval("SalesOrgID") %>'></asp:Label>
                                                    <asp:Label ID="lblDistChnl" runat="server" Text='<%# Eval("DistChnl") %>'></asp:Label>
                                                    <asp:Label ID="lblRequestType" runat="server" Text='<%# Eval("RequestType") %>'></asp:Label>
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
                                                    <asp:CheckBoxList ID="ChkReject_To" runat="server" RepeatDirection="Vertical">
                                                    </asp:CheckBoxList>
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
                                    <asp:Button ID="btnSAPUpload" runat="server" Text="SAP Integration" CssClass="button"
                                        OnClientClick="return ShowSAPUploadPopup();" Visible="false" />
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

                             <tr>
                                <td colspan="4" class="tdSpace">
                                      <%-- 8400000359--%>
                <asp:GridView ID="rptCommon" runat="server" AutoGenerateColumns="false"
                    Width="100%" BorderColor="#9D9D9D" GridLines="Both">
                    <RowStyle CssClass="light-gray" />
                    <HeaderStyle CssClass="gridHeader" />
                    <AlternatingRowStyle CssClass="gridRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Department" DataField="Department_Name" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="Actioned By" DataField="Actioned_By" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="Actioned On" DataField="Actioned_On" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="Remarks" DataField="Remark" HeaderStyle-HorizontalAlign="Left" />
                    </Columns>
                </asp:GridView>
                 <%-- 8400000359--%>
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
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="MMP" ShowMessageBox="true"
                ShowSummary="false" />
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
    <%--<asp:UpdatePanel ID="UpdMassRequestData" runat="server">
    <ContentTemplate>--%>
    <br />
    <br />
    <br />
    <div align="right">
        <asp:LinkButton ID="lnkExcelDwld" runat="server" Text="Download Excel" OnClick="lnkExcelDwld_Click" />
        <asp:ImageButton ID="imgExcelDwld" runat="server" ImageUrl="~/images/Excel.png" Height="20px"
            Width="20px" OnClick="lnkExcelDwld_Click" />
    </div>
    <br />
    <br />
    <div style="overflow: scroll; width: 1100px; height: 270px">
        <asp:GridView ID="grvMassRequestData" runat="server">
        </asp:GridView>
    </div>
    <br />
    <br />
    <%--Srinidhi--%>
    <div style="width: 100%" class="leftTD" id="divHeader" runat="server">
        Import Data</div>
    <%@ register namespace="AjaxControlToolkit" assembly="AjaxControlToolkit" tagprefix="act" %>
    <asp:FileUpload ID="fileUpload" runat="server" />
    <asp:Button ID="Process" runat="server" OnClick="Process_Click" Text="Upload" />
    <asp:Button runat="server" ID="hiddenTargetControlForModalPopupI" Style="display: none" />
    <asp:Panel ID="pnlMassUpdate" runat="server">
        <asp:Label ID="lblMassMsg" runat="server" />
    </asp:Panel>
    <act:ModalPopupExtender ID="ModalPopupExtenderI" runat="server" TargetControlID="hiddenTargetControlForModalPopupI"
        BehaviorID="programmaticModalPopupBehaviorI" CancelControlID="btnCancelImport"
        PopupControlID="pnlAddDataI" BackgroundCssClass="modalBackground" DropShadow="true"
        PopupDragHandleControlID="pnlTitleI" />
    <asp:Panel ID="pnlAddDataI" runat="server" Width="100%">
        <div style="background-color: White; padding: 2px 2px 2px 2px; overflow: scroll;
            width: 1100px; height: 550px">
            <asp:Panel ID="pnlTitleI" runat="server" Style="cursor: move; background-color: Black;
                border: solid 1px Gray; color: Black">
                <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                    <span class="ui-dialog-title">Import Mass Material Update Data</span>
                </div>
            </asp:Panel>
            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                <tr>
                    <td class="tdSpace">
                        <asp:GridView ID="grvMassData" runat="server" AutoGenerateColumns="false" OnDataBound="grvMassData_DataBound1">
                            <Columns>
                                <asp:TemplateField runat="server">
                                    <ItemTemplate>
                                        <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                                            <asp:Label ID="lblMsg" runat="server" />
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModuleId" runat="server" Text='<%# Eval("ModuleID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMHId" runat="server" Text='<%# Eval("MHID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Request Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqNo" runat="server" Text='<%# Eval("Sub_Request_No") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plant Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlant" runat="server" Text='<%# Eval("Plant") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Storage Location">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStorageLocation" runat="server" Text='<%# Eval("Storage_Location") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialNo" runat="server" Text='<%# Eval("Material_Number") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialType" runat="server" Text='<%# Eval("Material_Type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Base Unit of Measure">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBUOM" runat="server" Text='<%# Eval("Base_Unit_Of_Measure") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Material_Short_Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material_Group">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialGrp" runat="server" Text='<%# Eval("Material_Group") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Division">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDivision" runat="server" Text='<%# Eval("Division") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acc Assignment Grp">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAcc_Assignment_Grp" runat="server" Text='<%# Eval("Acc_Assignment_Grp") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Hierarchy">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProduct_Hierarchy" runat="server" Text='<%# Eval("Product_Hierarchy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pur Order Unit Measure">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPur_Order_Unit_Measure" runat="server" Text='<%# Eval("Pur_Order_Unit_Measure") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchasing Value Key">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPurchasing_Value_Key" runat="server" Text='<%# Eval("Purchasing_Value_Key") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchasing Group">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPurchasing_Group" runat="server" Text='<%# Eval("Purchasing_Group") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No Of Manufacturer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo_Of_Mftr" runat="server" Text='<%# Eval("No_Of_Manufacturer") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manufacturer Part No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMftr_Part_No" runat="server" Text='<%# Eval("Manufacturer_Part_No") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manufacturer Part Profile">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMftr_Part_Profile" runat="server" Text='<%# Eval("Manufacturer_Part_Profile") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GR Processing Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGR_Processing_Time" runat="server" Text='<%# Eval("GR_Processing_Time") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchase Order Text">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPur_Order_Text" runat="server" Text='<%# Eval("Purchase_Order_Text") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Control_Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblControl_Code" runat="server" Text='<%# Eval("Control_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRP Base Unit Of Measure">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRP_BUOM" runat="server" Text='<%# Eval("MRP_Base_Unit_Of_Measure") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRP Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRP_Type" runat="server" Text='<%# Eval("MRP_Type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRP Controller">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRP_Controller" runat="server" Text='<%# Eval("MRP_Controller") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reorder Point">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReorder_Point" runat="server" Text='<%# Eval("Reorder_Point") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lot Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLot_Size" runat="server" Text='<%# Eval("Lot_Size") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Min Lot Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMin_Lot_Size" runat="server" Text='<%# Eval("Min_Lot_Size") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Max Lot Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMax_Lot_Size" runat="server" Text='<%# Eval("Max_Lot_Size") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fixed Lot Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFixed_Lot_Size" runat="server" Text='<%# Eval("Fixed_Lot_Size") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rounding Value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRounding_Value" runat="server" Text='<%# Eval("Rounding_Value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Max Stock Level">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMax_Stock_Level" runat="server" Text='<%# Eval("Max_Stock_Level") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Planning Time Fence">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlanning_Time_Fence" runat="server" Text='<%# Eval("Planning_Time_Fence") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Production Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProduction_Unit" runat="server" Text='<%# Eval("Production_Unit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Procurement Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProcurement_Type" runat="server" Text='<%# Eval("Procurement_Type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Planned Delivery Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlanned_Delivery_Time" runat="server" Text='<%# Eval("Planned_Delivery_Time_Days") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="InHouse Prod Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInHouse_Production_Time" runat="server" Text='<%# Eval("InHouse_Production_Time") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Min Safety Stock">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMin_Safety_Stock" runat="server" Text='<%# Eval("Min_Safety_Stock") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fxd Lot Size Storage Loc">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFxd_Lot_Size_Storage_Loc" runat="server" Text='<%# Eval("Fxd_Lot_Size_Storage_Loc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Storage bin">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStorage_bin" runat="server" Text='<%# Eval("Storage_bin") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Min Remaining Shelf Life">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMin_Remaining_Shelf_Life" runat="server" Text='<%# Eval("Min_Remaining_Shelf_Life") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Shelf Life">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal_Shelf_Life_Days" runat="server" Text='<%# Eval("Total_Shelf_Life_Days") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Profit Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProfit_Center" runat="server" Text='<%# Eval("Profit_Center") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit of Issue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit_Issue" runat="server" Text='<%# Eval("Unit_Issue") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is QM in Procurement">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIs_QM_in_Procurement" runat="server" Text='<%# Eval("Is_QM_in_Procurement") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Certificate Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCertificate_Type" runat="server" Text='<%# Eval("Certificate_Type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ctrl Key QM Procurement">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCtrl_Key_QM_Procurement" runat="server" Text='<%# Eval("Ctrl_Key_QM_Procurement") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Interval Nxt Inspection">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInterval_Nxt_Inspection" runat="server" Text='<%# Eval("Interval_Nxt_Inspection") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inspection Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInspection_Type" runat="server" Text='<%# Eval("Inspection_Type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valuation_Class">
                                    <ItemTemplate>
                                        <asp:Label ID="lblValuation_Class" runat="server" Text='<%# Eval("Valuation_Class") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price Ctrl Indicator">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice_Ctrl_Indicator" runat="server" Text='<%# Eval("Price_Ctrl_Indicator") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lot Size Prd Cost Est">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLot_Size_Prd_Cost_Est" runat="server" Text='<%# Eval("Lot_Size_Prd_Cost_Est") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlMaterialGrp" runat="server" AppendDataBoundItems="false"
                                            Visible="false" Enabled="false" TabIndex="1">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlDivision" runat="server" AppendDataBoundItems="false" 
                                            Visible="false" TabIndex="2">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlProfitCenter" runat="server" AppendDataBoundItems="false" 
                                            Visible="false" TabIndex="3">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlPlantMass" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            Visible="false" TabIndex="4">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlValuationClass" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            Visible="false" TabIndex="5">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlMRPTypeMass" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            Visible="false" TabIndex="6">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                         <asp:DropDownList ID="ddlMRPControllerMass" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            Visible="false" TabIndex="7">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                         <asp:DropDownList ID="ddlLotSizeMass" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            Visible="false" TabIndex="8">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlControlCodeMass" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            Visible="false" TabIndex="9">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <%--<asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            Visible="false" TabIndex="1">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlStorageLocation" runat="server" TabIndex="5" AutoPostBack="true"
                                            Visible="false">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSalesOrginization" runat="server" TabIndex="5" AutoPostBack="true"
                                            Visible="false">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlDistributionChannel" runat="server" TabIndex="6" Visible="false">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            Visible="false" TabIndex="8">
                                            <asp:ListItem Text="Select" Value="0" />
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlField" runat="server" AppendDataBoundItems="false" TabIndex="9"
                                            Visible="false">
                                            <asp:ListItem Text="Select" Value="0" />
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtOldValue" runat="server" CssClass="textbox" Width="210" TabIndex="10"
                                            Visible="false" Text='<%# Eval("Old Value") %>' />
                                        <asp:TextBox ID="txtNewValue" runat="server" CssClass="textbox" MaxLength="70" Width="210"
                                            Visible="false" Text='<%# Eval("New Value") %>' TabIndex="11" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="leftTD">
                        <asp:Button ID="btnAdd" runat="server" Text="Import Data" CssClass="button" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnCancelImport" Text="Cancel" runat="server" CssClass="button" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <%--Srinidhi--%>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
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
            window.location.assign("Materialmaster.aspx")

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
       
    </script>
</asp:Content>
