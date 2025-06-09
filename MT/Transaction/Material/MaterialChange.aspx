<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="MaterialChange.aspx.cs" Inherits="Transaction_Material_MaterialChange" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Src="~/Transaction/UserControl/ChangeExcelUpload.ascx" TagPrefix="uc" TagName="ChangeExcelUpload" %>
<%@ Register Src="~/Transaction/UserControl/ucExcelDownloadl.ascx" TagPrefix="uc" TagName="ExcelDownload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/jquery.MultiFile.js" type="text/javascript"></script>
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
    <script type="text/javascript">
        function showModalPopupViaClient() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="pnlData" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                        <asp:Label ID="lblMsg" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="trHeading" align="center" colspan="2">Material Change Data
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2"></td>
            </tr>
            <%--<tr>
                <td class="tdSpace" colspan="2" align="left">
                    <strong>Material Type :</strong>
                    <asp:Label ID="lblMaterialType" runat="server" Font-Underline="true" />
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
                </td>
            </tr>--%>
            <%--//MSC_8300001775 End--%>
            <tr id="newtrMC" runat="server" style="display: none !important">
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="1" width="100%">
                        <tr>
                            <td class="tdSpace"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Type of Mass Updation
                            <asp:Label ID="lblddlTypeOfMassUpdm" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" align="left">
                                <%--valign="top"--%>
                                <asp:DropDownList ID="ddlTypeOfMassUpdm" runat="server" AppendDataBoundItems="false"
                                     AutoPostBack="true" TabIndex="4" OnSelectedIndexChanged="ddlTypeOfMassUpdm_SelectedIndexChanged" >
                                    <asp:ListItem Text="Select" Value="" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlTypeOfMassUpdm" runat="server" ControlToValidate="ddlTypeOfMassUpdm"
                                    ValidationGroup="massUpload" ErrorMessage="Type of Mass Updation cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Type of Mass Updation cannot be blank.' />" />
                            </td>

                        </tr>
                        <tr>
                            <td class="tdSpace"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                 <asp:Label ID="lblselectcap" runat="server" Text="Select File"></asp:Label>
                            <asp:Label ID="lblSelectFile" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" align="left">
                                <div>
                                    <asp:FileUpload ID="fileUploadMS"   runat="server" TabIndex="36" />
                                    <%-- <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />--%>
                                    <asp:Button ID="btnMSProcess" runat="server" OnClick="btnMSProcess_Click" Text="Upload" ValidationGroup="massUpload" />
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td class="rigthTD" align="left" colspan="2">
                                <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />
                            </td>

                        </tr>
                        <tr>
                            <td class="tdSpace"></td>
                        </tr>
                        <tr>
                            <td class="rigthTD" align="left" colspan="2">
                                <asp:GridView ID="grdAttachedDocs" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="Document_Upload_Id"
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
                            <td></td>
                        </tr>
                        <tr>
                            <td class="tdSpace"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace"></td>
                            <td class="tdSpace">
                                <asp:HyperLink ID="hlMSImportFormat" runat="server" Text="Excel Format" Target="_blank"></asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--//MSC_8300001775 End--%>

            <tr id="oldtrMC" runat="server" style="display: none !important">
                <td align="left" valign="top" colspan="2">
                    <asp:UpdatePanel ID="UpdChange" runat="server">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                                <asp:UpdatePanel ID="updpnlAddData" runat="server">
                                    <ContentTemplate>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                                                <act:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="hiddenTargetControlForModalPopup"
                                                    BehaviorID="programmaticModalPopupBehavior" CancelControlID="btnCancel" PopupControlID="pnlAddData"
                                                    BackgroundCssClass="modalBackground" DropShadow="true" PopupDragHandleControlID="pnlTitle" />
                                                <asp:Panel ID="pnlAddData" runat="server" Width="100%">
                                                    <div style="background-color: White; padding: 2px 2px 2px 2px;">
                                                        <asp:Panel ID="pnlTitle" runat="server" Style="cursor: move; background-color: Black; border: solid 1px Gray; color: Black">
                                                            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                                                                <span class="ui-dialog-title">Change :: Add Details</span>
                                                            </div>
                                                        </asp:Panel>
                                                        <table border="0" cellpadding="0" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td class="tdSpace" colspan="4">
                                                                    <asp:Panel ID="pnlMsg1" runat="server" Visible="false">
                                                                        <asp:Label ID="lblMsg1" runat="server" />
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4"></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="leftTD" width="20%">Material Code
                                                                    <asp:Label ID="labletxtMaterialCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:TextBox ID="txtMaterialCode" runat="server" CssClass="textbox" MaxLength="10"
                                                                        AutoPostBack="true" TabIndex="1" Width="180" OnTextChanged="txtMaterialCode_TextChanged" />
                                                                    <asp:RequiredFieldValidator ID="reqtxtMaterialCode" runat="server" ControlToValidate="txtMaterialCode"
                                                                        ValidationGroup="MaterialChange" ErrorMessage="Material Code cannot be blank."
                                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code cannot be blank.' />" />
                                                                    <asp:RegularExpressionValidator ID="regtxtMaterialCode" runat="server" ControlToValidate="txtMaterialCode"
                                                                        ValidationGroup="MaterialChange" ErrorMessage="Material Code Invalid." SetFocusOnError="true"
                                                                        ValidationExpression="^[\d]{6,10}$" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code Invalid.' />" />
                                                                </td>
                                                                <td class="leftTD" style="width: 20%">Material Name
                                                                    <asp:Label ID="labletxtMaterialName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    <br />
                                                                    <span style="color: Red; font-size: x-small; font-weight: normal">(As mentioned in SAP)</span>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:TextBox ID="txtMaterialName" runat="server" CssClass="textbox" MaxLength="70"
                                                                        Width="210" TabIndex="2" />
                                                                    <asp:RequiredFieldValidator ID="reqtxtMaterialName" runat="server" ControlToValidate="txtMaterialName"
                                                                        ValidationGroup="MaterialChange" ErrorMessage="Material Name cannot be blank."
                                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Name cannot be blank.' />" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4"></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="leftTD" width="20%">Material Type
                                                                    <asp:Label ID="lableddlMaterialAccGrp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:DropDownList ID="ddlMaterialAccGrp" runat="server" AppendDataBoundItems="false"
                                                                        Enabled="false" AutoPostBack="true" TabIndex="4" OnSelectedIndexChanged="ddlMaterialAccGrp_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Select" Value="0" />
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="reqddlMaterialAccGrp" runat="server" ControlToValidate="ddlMaterialAccGrp"
                                                                        ValidationGroup="MaterialChange" ErrorMessage="Select Material Type."
                                                                        SetFocusOnError="true" InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Material Type.' />" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4"></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="leftTD" style="width: 20%">Plant
                                                                    <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                                        TabIndex="1" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Select" Value="" />
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                                                        ValidationGroup="MaterialChange" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Plant cannot be blank.' />" />
                                                                </td>
                                                                <td class="leftTD" style="width: 20%">Storage Location
                                                                    <asp:Label ID="lableddlStorageLocation" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:DropDownList ID="ddlStorageLocation" runat="server" AppendDataBoundItems="false"
                                                                        TabIndex="2">
                                                                        <asp:ListItem Text="Select" Value="" />
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="reqddlStorageLocation" runat="server" ControlToValidate="ddlStorageLocation"
                                                                        ValidationGroup="MaterialChange" ErrorMessage="Storage Location cannot be blank."
                                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Storage Location cannot be blank.' />" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4"></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="leftTD">Sales Organization
                                                                    <%--<asp:Label ID="lableddlSalesOrginization" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                                                </td>
                                                                <td class="rigthTD">
                                                                    <asp:DropDownList ID="ddlSalesOrginization" runat="server" TabIndex="5" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlSalesOrginization_SelectedIndexChanged">
                                                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="reqddlSalesOrginization" runat="server" ControlToValidate="ddlSalesOrginization"
                                                                        ValidationGroup="MaterialChange" ErrorMessage="Sales Organization cannot be blank."
                                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Sales Organization cannot be blank.' />" />--%>
                                                                </td>
                                                                <td class="leftTD">Distribution Channel
                                                                    <%--<asp:Label ID="lableddlDistributionChannel" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                                                </td>
                                                                <td class="rigthTD">
                                                                    <asp:DropDownList ID="ddlDistributionChannel" runat="server" TabIndex="6">
                                                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="reqddlDistributionChannel" runat="server" ControlToValidate="ddlDistributionChannel"
                                                                        ValidationGroup="MaterialChange" ErrorMessage="Distribution Channel cannot be blank."
                                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Distribution Channel cannot be blank.' />" />--%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4">
                                                                    <asp:Label ID="lblMaterialChange" runat="server" Text="0" Visible="false" />
                                                                    <asp:Label ID="lblMaterialChangeDetailId" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblMaterialChangeAction" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="leftTD" style="width: 20%">Section
                                                                    <asp:Label ID="labletxtSection" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                                                        TabIndex="8" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Select" Value="0" />
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="reqddlSection" runat="server" ControlToValidate="ddlSection"
                                                                        ValidationGroup="MaterialChange" ErrorMessage="Section cannot be blank." SetFocusOnError="true"
                                                                        InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Section cannot be blank.' />" />
                                                                </td>
                                                                <td class="leftTD" style="width: 20%">Field
                                                                    <asp:Label ID="lableddlField" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:DropDownList ID="ddlField" runat="server" AppendDataBoundItems="false" TabIndex="9">
                                                                        <asp:ListItem Text="Select" Value="0" />
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="reqddlField" runat="server" ControlToValidate="ddlField"
                                                                        ValidationGroup="MaterialChange" ErrorMessage="Field cannot be blank." SetFocusOnError="true"
                                                                        InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Field cannot be blank.' />" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="leftTD" style="width: 20%">Old Value
                                                                    <asp:Label ID="labletxtOldValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    <br />
                                                                    <span style="color: Red; font-size: x-small; font-weight: normal">(As mentioned in SAP)</span>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:TextBox ID="txtOldValue" runat="server" CssClass="textbox" Width="210" TabIndex="10" />
                                                                    <asp:RequiredFieldValidator ID="reqtxtOldValue" runat="server" ControlToValidate="txtOldValue"
                                                                        ValidationGroup="MaterialChange" ErrorMessage="Old Value cannot be blank." SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Old Value cannot be blank.' />" />
                                                                </td>
                                                                <td class="leftTD" style="width: 20%">New Value
                                                                    <asp:Label ID="labletxtNewValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <%-- <asp:TextBox ID="txtNewValue" runat="server" CssClass="textbox" MaxLength="2000" Width="210"
                                                                        TabIndex="11" />  Rows="3" --%>
                                                                    <asp:TextBox ID="txtNewValue" runat="server" CssClass="textarea" TextMode="MultiLine"
                                                                        TabIndex="11" Columns="100" />
                                                                    <asp:RequiredFieldValidator ID="reqtxtNewValue" runat="server" ControlToValidate="txtNewValue"
                                                                        ValidationGroup="MaterialChange" ErrorMessage="New Value cannot be blank." SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='New Value cannot be blank.' />" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4"></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="centerTD" colspan="4">
                                                                    <asp:Button ID="btnAdd" runat="server" ValidationGroup="MaterialChange" Text="Save"
                                                                        CssClass="button" UseSubmitBehavior="true" TabIndex="12" OnClick="btnAdd_Click" />
                                                                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdSpace" colspan="4" align="right">
                                                <asp:LinkButton ID="lnkAddNew" runat="server" Visible="false" OnClick="lnkAddNew_Click">Add New Material<image src="../../images/Add.jpg" border="0px" ></image></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <tr>
                                    <td class="tdSpace"></td>
                                </tr>
                                <asp:Panel ID="pnlRemarks" runat="server" Visible="false">
                                    <tr>
                                        <td class="leftTD">Remarks
                                        </td>
                                        <td class="rigthTD">
                                            <asp:TextBox ID="txtRemarks" runat="server" MaxLength="250" TextMode="MultiLine"
                                                Width="90%" TabIndex="37" Rows="3" />
                                        </td>
                                        <td class="tdSpace" colspan="2"></td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td class="tdSpace"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grvMaterialChange" runat="server" AutoGenerateColumns="false" Width="100%"
                                            BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both" OnRowDataBound="grvMaterialChange_RowDataBound">
                                            <RowStyle CssClass="light-gray" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle CssClass="gridHeader" />
                                            <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="left" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaterialChangeId" runat="server" Text='<%# Eval("Material_Change_Id") %>'></asp:Label>
                                                        <asp:Label ID="lblSalesOrgId" runat="server" Text='<%# Eval("Sales_Organisation_Id") %>'></asp:Label>
                                                        <asp:Label ID="lblDistChnlId" runat="server" Text='<%# Eval("Distribution_Channel_Id") %>'></asp:Label>
                                                        <asp:Label ID="lblPlantId" runat="server" Text='<%# Eval("Plant_Id") %>'></asp:Label>
                                                        <asp:Label ID="lblStorageLocationId" runat="server" Text='<%# Eval("Storage_Location") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Material_Code" HeaderText="SAP Code" ItemStyle-Width="8%" />
                                                <asp:BoundField DataField="Material_Desc" HeaderText="Name" ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company" Visible="false" />
                                                <asp:BoundField DataField="MaterialAccGrpName" HeaderText="Acc. Grp." Visible="false" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-Width="25%" HeaderText="Org. Level Data">
                                                    <ItemTemplate>
                                                        Plant.:
                                                        <asp:Label ID="lblPlant" runat="server" Text='<%# Eval("PlantName") %>'></asp:Label>
                                                        <br />
                                                        Storage Loc.:
                                                        <asp:Label ID="lblStorageLocation" runat="server" Text='<%# Eval("Storage_Loc_Name") %>'></asp:Label>
                                                        <br />
                                                        Sales Org.:
                                                        <asp:Label ID="lblSalesOrg" runat="server" Text='<%# Eval("SalesOrgName") %>'></asp:Label>
                                                        <br />
                                                        Dist. Chnl.:
                                                        <asp:Label ID="lblDistChnl" runat="server" Text='<%# Eval("DistributionChnlName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkAddValue" ImageUrl="~/images/Add.jpg" runat="server" OnClick="lnkAddValue_Click"
                                                            ToolTip="Add Field" Font-Bold="true" CommandArgument='<%# Eval("Material_Change_Id") %>' />&nbsp;
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Changes">
                                                    <ItemTemplate>
                                                        <asp:GridView ID="grvMaterialChangeDtl" runat="server" AutoGenerateColumns="false"
                                                            Width="100%" BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both">
                                                            <RowStyle CssClass="light-gray" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle CssClass="gridHeader" />
                                                            <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialChangeDtl" runat="server" Text='<%# Eval("Material_Change_Detail_Id") %>'></asp:Label>
                                                                        <asp:Label ID="lblSectionFeildMasterId" runat="server" Text='<%# Eval("Section_Feild_Master_Id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Decsription" HeaderText="Section" ItemStyle-Width="22%" />
                                                                <asp:BoundField DataField="FeildDisplayName" HeaderText="Field" ItemStyle-Width="22%" />
                                                                <asp:BoundField DataField="Old_Value" HeaderText="Old Value" ItemStyle-Width="22%" />
                                                                <asp:BoundField DataField="New_Value" HeaderText="New Value" ItemStyle-Width="22%" />
                                                                <asp:TemplateField Visible="false" ItemStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/Edit.jpg" OnClick="btnEdit_Click"
                                                                            ToolTip="Edit Field" Font-Bold="true" CommandArgument='<%# Eval("Material_Change_Detail_Id") %>' />&nbsp;
                                                                        <asp:ImageButton ID="btnDelete" runat="server" Text="X" ImageUrl="~/images/Delete.bmp"
                                                                            ToolTip="Delete Field" ForeColor="Red" Font-Size="15px" OnClick="btnDelete_Click"
                                                                            Font-Bold="true" CommandArgument='<%# Eval("Material_Change_Detail_Id") %>' OnClientClick="return confirm('Are you certain you want to delete this entry?');" />&nbsp;
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace"></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <%--<tr>
                <td class="leftTD" align="left" colspan="2">
                    <b>Attach Documents (Image/PDF Files Only)</b>
                </td>
            </tr>
            <tr>
                <td class="rigthTD" align="left" valign="top">
                    <div>
                        <asp:FileUpload ID="file_upload" class="multi" runat="server" TabIndex="36" />
                        <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />
                    </div>
                </td>
                <td class="rigthTD" align="left">
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
            </tr>--%>
            <%--MSC_8300001775--%>
            <tr id="trMassBtn" runat="server" visible="false">
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnMassSave" runat="server" Text="Save" CssClass="button" UseSubmitBehavior="true"
                        TabIndex="39" OnClick="btnMassSave_Click" Visible="false"/>
                </td>
            </tr>
            <%--MSC_8300001775--%>
            <tr id="trButton" runat="server" visible="false">
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnPrevious" runat="server" Text="Back" UseSubmitBehavior="false"
                        TabIndex="38" CssClass="button" OnClick="btnPrevious_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" UseSubmitBehavior="true"
                        TabIndex="39" OnClick="btnSave_Click" Visible="false" />
                    <asp:Button ID="btnNext" runat="server" Text="Save & Proceed to Submit" UseSubmitBehavior="true"
                        TabIndex="40" CssClass="button" OnClick="btnNext_Click" Width="160px" />
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="4"></td>
            </tr>
        </table>
    </asp:Panel>

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
                                <i>Your request will be submitt after validation completed.</i>
                                <br />
                                <br />

                                <i>If validation is failed then will send mail to your register email.</i>
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
                    <asp:Button ID="btnbackMsg" runat="server" Text="Ok" CssClass="button"
                        OnClick="btnbackMsg_Click" />
                </td>
            </tr>
        </table>
    </div>

    <%--MSC_8300001775 end--%>
    <asp:ValidationSummary ID="vsSM" runat="server" ValidationGroup="massUpload" ShowMessageBox="true"
        ShowSummary="false" />
    <%--MSC_8300001775 end--%>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="save" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SaveValue"
        ShowMessageBox="true" ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="56" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblMaterialChangeId" runat="server" Visible="false" />
    <asp:Label ID="lblMatPlantGrpId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
    <%--//CTRL_SUB_SDT18112019 Added by NR--%>
    <asp:Label ID="lblRefModuleId" runat="server" Visible="false" />
    <%--//CTRL_SUB_SDT18112019 Added by NR--%>
    <div align="left" style="width: 98%">
        <uc:ChangeExcelUpload ID="ChangeExcelUpload1" runat="server" />
    </div>
    <div align="left" style="width: 98%">
        <uc:ExcelDownload ID="ExcelDownload1" runat="server" ActionType="C" Visible="false" />
    </div>

    <div align="left" style="width: 98%">
        <uc:ExcelDownload ID="ExcelDownload2" runat="server" ActionType="MMC" Visible="false" />
    </div>


    <script type="text/javascript" language="javascript">
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
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                    $(this).show('clip');
                }
            });


        }

    </script>

</asp:Content>
