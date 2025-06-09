<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/ZcapHsnMaster/ZcapHsnMaster.master" AutoEventWireup="true" CodeFile="ZcapHsnCreate.aspx.cs" Inherits="Transaction_ZcapHsnMaster_ZcapHsnCreate" %>

<%@ Register Src="~/Transaction/UserControl/ucExcelDownload2.ascx" TagPrefix="uc" TagName="ExcelDownload" %>
<%@ Register Src="~/Transaction/UserControl/ucZcapHsnExcelUpload.ascx" TagPrefix="uc" TagName="ZcapHsnExcelUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
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
   <%-- <asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlAddNew" runat="server">

                <div id="divmainPopUp" runat="server" clientidmode="Static">
                    <table border="0" cellpadding="0" cellspacing="1" width="100%">

                        <tr>
                            <td class="trHeading" align="center" colspan="4">ZCAP/ZPEX+HSN/GST%
                            </td>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td class="leftTD" width="100%" colspan="4">

                                <asp:TextBox ID="txtNewRow" runat="server" Text="1" MaxLength="3" Width="20px"
                                    Enabled="false" CssClass="textbox" Style="display: none;" />
                                <asp:RangeValidator ID="ranvtxtNewRow" runat="server" ValidationGroup="addRowValidation"
                                    ControlToValidate="txtNewRow" MaximumValue="20" MinimumValue="1" Type="Integer"
                                    ErrorMessage="Enter Numeric Value only (Maximum limit 20)." SetFocusOnError="true"
                                    Display="Dynamic"
                                    Text="<img src='../../images/Error.png' title='Enter Numeric Value only (Maximum limit 20).' />"></asp:RangeValidator>

                                <asp:Button ID="btnaddRow" runat="server" Text="Add New Row" ValidationGroup="addRowValidation"
                                    OnClick="btnaddRow_Click" CssClass="button" UseSubmitBehavior="false" Visible="false" />
                       </td> </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td colspan="4">

                                <asp:GridView ID="grdDetailAdd" runat="server" AutoGenerateColumns="false"
                                    DataKeyNames="HSN_ZCAP_Detaiils_Id,sCondintion_type,sIsLUTCond"
                                    Width="1000px" CssClass="GridClass" ShowHeaderWhenEmpty="true" Visible="true"
                                    ShowFooter="false" AllowSorting="true"
                                    OnRowCommand="grdDetailAdd_RowCommand"
                                    OnRowDataBound="grdDetailAdd_RowDataBound">
                                    <FooterStyle CssClass="gridFooter" />
                                    <RowStyle CssClass="light-gray" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <AlternatingRowStyle CssClass="gridRowStyle" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Remove">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("HSN_ZCAP_Detaiils_Id") %>' CommandName="D">  
                                                 <img src="../../images/delete.png" alt="Delete" title='Delete' Width="20px"
                                                     OnClientClick="return confirm('Are you certain you want to delete this record?');" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHSN_ZCAP_Detaiils_Id" runat="server" Text='<%#Eval("HSN_ZCAP_Detaiils_Id") %>'
                                                    Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Material Code">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsMaterial_Code" runat="server" CssClass="textbox" Text='<%#Eval("sMaterial_Code") %>'
                                                    Width="80px" MaxLength="7" onkeypress="return IsNumber();" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtsMaterial_Code" runat="server" ControlToValidate="txtsMaterial_Code"
                                                    ValidationGroup="save" ErrorMessage="Material Code cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Material Code cannot be blank.' />" />

                                                <asp:RegularExpressionValidator ID="regtxtsMaterial_Code" runat="server" ControlToValidate="txtsMaterial_Code"
                                                    ValidationGroup="save" ErrorMessage="Invalid Material Code." SetFocusOnError="true"
                                                    ValidationExpression="^[\d]{6,10}$" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Material Code.' />" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsMaterial_Name" runat="server" CssClass="textbox" Text='<%#Eval("sMaterial_Name") %>'
                                                    Width="70px" MaxLength="100"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtsMaterial_Name" runat="server" ControlToValidate="txtsMaterial_Name"
                                                    ValidationGroup="save" ErrorMessage="Material name cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Material name cannot be blank.' />" />
                                               </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Supplying Plant">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsSupp_plant" runat="server" CssClass="textbox" Text='<%#Eval("sSupp_plant") %>'
                                                    Width="70px" MaxLength="4"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtsSupp_plant" runat="server" ControlToValidate="txtsSupp_plant"
                                                    ValidationGroup="save" ErrorMessage="Supp. plant cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Supp. plant cannot be blank.' />" />
                                                <asp:RegularExpressionValidator ID="regtxtsSupp_plant" runat="server" ControlToValidate="txtsSupp_plant"
                                                    ValidationGroup="save" ErrorMessage="Invalid Supp. plant." SetFocusOnError="true"
                                                    ValidationExpression="^[a-zA-Z0-9]{4,4}$" Display="Dynamic"
                                                    Text="<img src='../../images/Error.png' title='Invalid Supp. plant.' />" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Receiving Plant">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsRece_plant" runat="server" CssClass="textbox" Text='<%#Eval("sRece_plant") %>'
                                                    Width="70px" MaxLength="4"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtsRece_plant" runat="server" ControlToValidate="txtsRece_plant"
                                                    ValidationGroup="save" ErrorMessage="Rec. plant cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Rec. plant cannot be blank.' />" />
                                                <asp:RegularExpressionValidator ID="regtxtsRece_plant" runat="server" ControlToValidate="txtsRece_plant"
                                                    ValidationGroup="save" ErrorMessage="Invalid Rec. plant." SetFocusOnError="true"
                                                    ValidationExpression="^[a-zA-Z0-9]{4,4}$" Display="Dynamic"
                                                    Text="<img src='../../images/Error.png' title='Invalid Rec. plant.' />" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Condintion type">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlsCondintion_type" runat="server" AppendDataBoundItems="false" Width="125px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlsCondintion_type" runat="server" ControlToValidate="ddlsCondintion_type"
                                                    ValidationGroup="save" ErrorMessage="Select the condintion type." SetFocusOnError="true"
                                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select the condintion type.' />" />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Zcap/Zpex Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsZcapRate" runat="server" CssClass="textbox" Text='<%#Eval("sZcapRate") %>'
                                                    Width="60px" MaxLength="16"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtsZcapRate" runat="server" ControlToValidate="txtsZcapRate"
                                                    ValidationGroup="save" ErrorMessage="Zcap rate cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Zcap rate cannot be blank.' />" />
                                                <asp:RegularExpressionValidator runat="server" ID="txtregpre"
                                                    ValidationGroup="save" Display="Dynamic" ErrorMessage="Invalid Zcap rate."
                                                    ControlToValidate="txtsZcapRate"
                                                    Text="<img src='../../images/Error.png' title='Invalid Zcap rate.' />"
                                                    ValidationExpression="^[0-9]*(\.[0-9]{0,2})?$"></asp:RegularExpressionValidator>
                                                <%--ValidationExpression="^\d+\.\d{1,2}$"></asp:RegularExpressionValidator>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="UOM">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsUOM" runat="server" CssClass="textbox" Text='<%#Eval("sUOM") %>'
                                                    Width="60px" MaxLength="3"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtsUOM" runat="server" ControlToValidate="txtsUOM"
                                                    ValidationGroup="save" ErrorMessage="UOM cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='UOM cannot be blank.' />" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="STO/PO No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsSTONum" runat="server" CssClass="textbox" Text='<%#Eval("sSTONum") %>'
                                                    Width="60px" MaxLength="50"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="HSN Code">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsHSN_Code" runat="server" CssClass="textbox" Text='<%#Eval("sHSN_Code") %>'
                                                    Width="100px" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtsHSN_Code" runat="server" ControlToValidate="txtsHSN_Code"
                                                    ValidationGroup="save" ErrorMessage="HSN code cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='HSN code cannot be blank.' />" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="GST Code (%)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsGST_Code" runat="server" CssClass="textbox" Text='<%#Eval("sGST_Code") %>'
                                                    Width="40px" MaxLength="6" onkeypress="return IsNumber();" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtsGST_Code" runat="server" ControlToValidate="txtsGST_Code"
                                                    ValidationGroup="save" ErrorMessage="GST code cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='GST code cannot be blank.' />" />

                                                <asp:RegularExpressionValidator runat="server" ID="txtregpre1"
                                                    ValidationGroup="save" Display="Dynamic" ErrorMessage="Invalid GST code."
                                                    ControlToValidate="txtsGST_Code"
                                                    Text="<img src='../../images/Error.png' title='Invalid GST code.' />"
                                                    ValidationExpression="^[0-9]*(\.[0-9]{0,3})?$"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Is LUT Cond">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlsIsLUTCond" runat="server" AppendDataBoundItems="false" Width="125px">
                                                    <asp:ListItem Text="Select" Value="" />
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsRemarks" runat="server" CssClass="textbox" Text='<%#Eval("sRemarks") %>'
                                                    Width="120px" MaxLength="50" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtsRemarks" runat="server" ControlToValidate="txtsRemarks"
                                                    ValidationGroup="save" ErrorMessage="Remarks cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Remarks cannot be blank.' />" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdSpace"></td>
                        </tr> <tr>
                            <td class="leftTD" align="left" colspan="4">
                                <b>Attach Documents (Files Only)</b>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" align="left" colspan="1" valign="top">
                                <div>
                                    <asp:FileUpload ID="file_upload" class="multi" runat="server"   />
                                    <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />
                                </div>
                            </td>
                            <td class="rigthTD" align="left" colspan="2">
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
                        </tr>

                        <tr>
                            <td colspan="4" class="tdSpace"></td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="2">
                                <asp:Button ID="btnPrevious" runat="server" Text="Back" TabIndex="26"
                                    CssClass="button" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" UseSubmitBehavior="true" ValidationGroup="save"
                                    Text="Save" CssClass="button" TabIndex="27" OnClick="btnSave_Click" />
                                <asp:Button ID="btnNext" runat="server" Text="Save & Next"
                                    TabIndex="28" UseSubmitBehavior="true" CssClass="button" OnClick="btnNext_Click"
                                    Width="120px" />
                            </td>
                        </tr>

                        <tr>
                            <td colspan="4" class="tdSpace"></td>
                        </tr>
                        <tr>
                            <td colspan="4" class="tdSpace">
                                <asp:GridView ID="rptZcapHsn" runat="server" AutoGenerateColumns="false"
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

                                <%-- <asp:Repeater ID="rptZcapHsn" runat="server">
                                <ItemTemplate>
                                    <td colspan="4" class="tdSpace">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>Comments By : 
                                                    <asp:Label ID="lblUserdetails" runat="server" Text='<%#Eval("CreatedBy") %>'/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Comments : 
                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks") %>'/>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>--%>
                            </td>
                        </tr>

                    </table>
                </div>
                <asp:ValidationSummary ID="VaSu" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="addRowValidation" />
                <asp:ValidationSummary ID="VaSu1" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="save" />

                <asp:Label ID="lblUserId" runat="server" Visible="false" />
                <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
                <asp:Label ID="lblMode" runat="server" Visible="false" />
                <asp:Label ID="lblModuleId" runat="server" Visible="false" />
                <asp:Label ID="lblActionType" runat="server" Style="display: none" />
                <%--<asp:Label ID="lblSectionId" runat="server" Text="103" Visible="false" />--%>
                <asp:Label ID="lblSectionId" runat="server" Text="<%$appSettings:SECHSN %>" Visible="false" />
                <asp:Label ID="lblFlag" runat="server" Text="" Visible="false" />
                 <asp:Label ID="lblReqStatus" runat="server" Text="" Visible="false" />
            </asp:Panel>
      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>



    <div align="left" style="width: 98%">
        <%--<uc:ExcelDownload ID="ExcelDownload1" runat="server" ActionType="ZH" Visible="false" />--%>
        <uc:ExcelDownload ID="ExcelDownload1" runat="server" ActionType="ZH" Visible="false" />
    </div>
    <div align="left" style="width: 98%">
        <%--<uc:ZcapHsnExcelUpload ID="ZcapHsnExcelUpload" runat="server" Visible="false" />--%>
        <uc:ZcapHsnExcelUpload ID="ZcapHsnExcelUpload" runat="server" Visible="false" />
    </div>

</asp:Content>

