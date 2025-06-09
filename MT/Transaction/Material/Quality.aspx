<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="Quality.aspx.cs" Inherits="Transaction_Quality" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<%--New Addition for HU tick Start--%>
<%--<%@ Register Src= "~/Transaction/UserControl/ucQualityHUData.ascx" TagName="ucInspData" TagPrefix="uc" %>--%>
<%--New Addition for HU tick End--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialQuality" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlData" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Quality
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <asp:Panel ID="pnlGrid" runat="server">
                                <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                    DataKeyNames="Mat_Quality_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
                                    GridLines="Both">
                                    <RowStyle CssClass="light-gray" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <AlternatingRowStyle CssClass="gridRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Plants" DataField="Plant" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="lnkView_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Plant
                                        <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" TabIndex="1">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                            ValidationGroup="Quality" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                    </td>
                                    <td class="tdSpace" colspan="2" align="right">
                                        <asp:LinkButton ID="lnlAddDetails" runat="server" Text="Add New" OnClick="lnlAddDetails_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Material Authorization Group for Activities in QM
                                        <asp:Label ID="lableddlMaterialAuthGroupActQM" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlMaterialAuthGroupActQM" runat="server" AppendDataBoundItems="false"
                                            TabIndex="2">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlMaterialAuthGroupActQM" runat="server" ControlToValidate="ddlMaterialAuthGroupActQM"
                                            ValidationGroup="Quality" ErrorMessage="Material Authorization Group for Activities in QM cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Authorization Group for Activities in QM cannot be blank.' />" />
                                    </td>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Inspection Type
                                        <asp:Label ID="lableddlInspectionType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <cc1:DropDownCheckBoxes ID="ddlInspectionType" runat="server" AddJQueryReference="false"
                                            UseButtons="false" UseSelectAllNode="true">
                                            <%-- AutoPostBack="true" OnSelectedIndexChanged="ddlInspectionType_SelectedIndexChanged">--%>
                                            <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="350" DropDownBoxBoxHeight="80" />
                                            <Texts SelectBoxCaption="--Select--" />
                                        </cc1:DropDownCheckBoxes>
                                        <%--<cc1:ExtendedRequiredFieldValidator ID="reqddlInspectionType" runat="server" ControlToValidate="ddlInspectionType" 
                                            ValidationGroup="Quality" ErrorMessage="Inspection Type cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Inspection Type cannot be blank.' />"></cc1:ExtendedRequiredFieldValidator>--%>
                                    </td>
                                    <td class="leftTD" colspan="2">
                                        <asp:Label ID="lableddlInspectionType1" runat="server"></asp:Label>
                                         <%--New Addition for HU tick Start--%>
                                        <%--<asp:CheckBoxList ID="chklstInspectionType" runat="server" OnSelectedIndexChanged = "chklstInspectionType_SelectedIndexChanged" AutoPostBack = "true">
                                        </asp:CheckBoxList>--%>
                                         <%--New Addition for HU tick End--%>
                                        <asp:LinkButton ID="lnkRefreshddlInspectionType" runat="server" Text="[ Refresh ]"
                                            Font-Bold="false" OnClick="lnkRefreshddlInspectionType_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>

                                <%--New Addition for HU tick Start--%>
                                <%--<tr>
                                    <td class="tdSpace" colspan="4">
                                        <asp:Panel ID="pnlInspData" runat="server" Visible="false" BorderWidth="2px" BorderColor="Black"
                                            BorderStyle="Solid">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="updInspData" runat="server">
                                                            <ContentTemplate>
                                                                <uc:ucInspData id="ucInspdata" runat="server" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr id="trQualityData" runat="server" visible="false">
                                                    <td class="centerTD">
                                                        <asp:Button ID="btnInspData" runat="server" Text="Update" CssClass="button" OnClick="btnInspData_Click"/>
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>--%>
                                <%--New Addition for HU tick End--%>

                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Unit of issue
                                        <asp:Label ID="lableddlUnitIssue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlUnitIssue" runat="server" AppendDataBoundItems="false" TabIndex="4">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlUnitIssue" runat="server" ControlToValidate="ddlUnitIssue"
                                            ValidationGroup="Quality" ErrorMessage="Unit of issue cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Unit of issue cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        <%--QM in Procurement is Active--%>
                                          <asp:Label ID="lblchkQMProcurmentActive" runat="server" Text="QM in Procurement is Active"></asp:Label>
                                   
                                        <asp:Label ID="lablechkQMProcurmentActive" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:CheckBox ID="chkQMProcurmentActive" runat="server" Text="Check if Relevant"
                                            TabIndex="5" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        <%----%>
                                         <asp:Label ID="lblddlControlQualityMang" runat="server" Text="Control Key for Quality Management in Procurement"></asp:Label>
                                   
                                        <asp:Label ID="lableddlControlQualityMang" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlControlQualityMang" runat="server" AppendDataBoundItems="false"
                                            TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddlControlQualityMang_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlControlQualityMang" runat="server" ControlToValidate="ddlControlQualityMang"
                                            ValidationGroup="Quality" ErrorMessage="Control Key for Quality Management in Procurement cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Control Key for Quality Management in Procurement cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Interval to next periodic inspection
                                        <asp:Label ID="labletxtIntervalNPInspector" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtIntervalNPInspector" runat="server" CssClass="textbox" Width="100px"
                                            onkeypress="return IsNumber();" MaxLength="5" TabIndex="7" />
                                        <asp:RequiredFieldValidator ID="reqtxtIntervalNPInspector" runat="server" ControlToValidate="txtIntervalNPInspector"
                                            ValidationGroup="Quality" ErrorMessage="Interval to next periodic inspection  cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Interval to next periodic inspection  cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        <%--Certificate Type --%>
                                        <asp:Label ID="lblddlCertificateType" runat="server" Text="Certificate Type "></asp:Label>
                                  
                                        <asp:Label ID="lableddlCertificateType" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlCertificateType" runat="server" AppendDataBoundItems="false"
                                            TabIndex="8">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlCertificateType" runat="server" ControlToValidate="ddlCertificateType"
                                            ValidationGroup="Quality" ErrorMessage="Certificate Type cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Certificate Type cannot be blank.' />" />
                                    </td>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">
                                        Documentation required indicator
                                        <asp:Label ID="lablechkDocumentationReqIndi" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:CheckBox ID="chkDocumentationReqIndi" runat="server" Text="Check if Relevant"
                                            TabIndex="9" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Catalog Profile
                                        <asp:Label ID="lableddlCatalogProfile" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlCatalogProfile" runat="server" AppendDataBoundItems="false"
                                            TabIndex="10">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlCatalogProfile" runat="server" ControlToValidate="ddlCatalogProfile"
                                            ValidationGroup="Quality1" ErrorMessage="Catalog Profile cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Catalog Profile cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Minimum Remaining Shelf Life
                                        <asp:Label ID="labletxtMinRemainingShelfLife" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtMinRemainingShelfLife" runat="server" onkeypress="return IsNumber();"
                                            MaxLength="11" TabIndex="11" CssClass="textbox" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtMinRemainingShelfLife" runat="server" ControlToValidate="txtMinRemainingShelfLife"
                                            ValidationGroup="Quality" ErrorMessage="Minimum Remaining Shelf life cannot be blank cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Batch management requirement indicator cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">
                                        Total shelf life
                                        <asp:Label ID="labletxtTotalShelfLifeDays" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtTotalShelfLifeDays" runat="server" CssClass="textbox" Width="100px"
                                            MaxLength="11" TabIndex="12" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtTotalShelfLifeDays" runat="server" ControlToValidate="txtTotalShelfLifeDays"
                                            ValidationGroup="Quality" ErrorMessage="Total shelf life cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Total shelf life cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr id="trButton" runat="server" visible="false">
                                    <td class="centerTD" colspan="4">
                                        <asp:Button ID="btnPrevious" runat="server" ValidationGroup="Quality" Text="Back"
                                            TabIndex="13" UseSubmitBehavior="false" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="Quality" Text="Save" CssClass="button"
                                            TabIndex="14" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnNext" runat="server" ValidationGroup="Quality" Text="Save & Next"
                                            TabIndex="15" CssClass="button" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="Quality" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblQualityId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="14" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }
    </script>
</asp:Content>
