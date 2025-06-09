<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/ProfitCenter/ProfitCenterMaster.master" AutoEventWireup="true" CodeFile="ProfitCenterChange.aspx.cs" Inherits="Transaction_ProfitCenter_ProfitCenterChange" %>


<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
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
            //ev.preventDefault();
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
                <td class="trHeading" align="center" colspan="2">Change Data
                </td>
            </tr>
            <tr>
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
                                                                <td class="leftTD" width="20%">Profit Center Code
                                                                    <asp:Label ID="labletxtProfitCenterCode" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:TextBox ID="txtProfitCenterCode" runat="server" CssClass="textbox" MaxLength="10"
                                                                        TabIndex="1" Width="180" />
                                                                    <asp:RequiredFieldValidator ID="reqtxtProfitCenterCode" runat="server" ControlToValidate="txtProfitCenterCode"
                                                                        ValidationGroup="save" ErrorMessage="Profit Center Code cannot be blank." SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Profit Center Code cannot be blank.' />" />
                                                                    <asp:RegularExpressionValidator ID="regtxtProfitCenterCode" runat="server" ControlToValidate="txtProfitCenterCode"
                                                                        ValidationGroup="save" ErrorMessage="Profit Center Code Invalid." SetFocusOnError="true"
                                                                        ValidationExpression="^[\S]{4,10}" Display="Dynamic" Text="<img src='../../images/Error.png' title='Profit Center Code Invalid.' />" />
                                                                </td>
                                                                <td class="leftTD" style="width: 20%">Profit Center Name
                                                                    <asp:Label ID="labletxtProfitCenterName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    <br />
                                                                    <span style="color: Red; font-size: x-small; font-weight: normal">(As mentioned in SAP)</span>
                                                                </td>
                                                                <td class="rigthTD" style="width: 30%">
                                                                    <asp:TextBox ID="txtProfitCenterName" runat="server" CssClass="textbox" MaxLength="70"
                                                                        Width="210" TabIndex="2" />
                                                                    <asp:RequiredFieldValidator ID="reqtxtProfitCenterName" runat="server" ControlToValidate="txtProfitCenterName"
                                                                        ValidationGroup="save" ErrorMessage="Profit Center Name cannot be blank." SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Profit Center Name cannot be blank.' />" />
                                                                </td>
                                                            </tr>

                                                            <tr>

                                                                <td class="leftTD" colspan="2">
                                                                    <asp:Label ID="lblProfitCenterChange" runat="server" Text="0" Visible="false" />
                                                                    <asp:Label ID="lblProfitCenterChangeDetailId" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblProfitCenterChangeAction" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdSpace" colspan="4"></td>
                                                            </tr>

                                                            <tr>
                                                                <td class="leftTD" style="width: 10%"></td>

                                                                <td class="leftTD" style="width: 10%">
                                                                    <asp:Label ID="lblcddlField" runat="server" Text="Field"></asp:Label>
                                                                    <asp:Label ID="lableddlField" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                                <td class="leftTD" style="width: 10%">
                                                                    <asp:Label ID="lblctxtOldValue" runat="server" Text="Old Value"></asp:Label>
                                                                    <asp:Label ID="labletxtOldValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    (As mentioned in SAP) 
                                                                </td>
                                                                <td class="leftTD" style="width: 10%">
                                                                    <asp:Label ID="lblctxtNewValue" runat="server" Text="New Value"></asp:Label>
                                                                    <asp:Label ID="labletxtNewValue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                </td>
                                                            </tr>


                                                            <tr id="ddlf1" runat="server">

                                                                <td class="rigthTD" width="10%"></td>
                                                                <td class="rigthTD" width="10%">

                                                                    <asp:DropDownList ID="ddlField" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlField_SelectedIndexChanged" TabIndex="3">
                                                                        <asp:ListItem Text="----Select----" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>

                                                                    <asp:RequiredFieldValidator ID="reqddlField" runat="server" ControlToValidate="ddlField"
                                                                        ValidationGroup="save" ErrorMessage="Field cannot be blank" SetFocusOnError="true"
                                                                        InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Field cannot be blank' />" />

                                                                </td>
                                                                <td class="rigthTD" width="10%">
                                                                    <asp:TextBox ID="txtOldValue" runat="server" CssClass="textbox" TabIndex="4" />

                                                                    <asp:RequiredFieldValidator ID="reqtxtOldValue" runat="server" ControlToValidate="txtOldValue"
                                                                        ValidationGroup="save" ErrorMessage="Old Value Cannot be blank" SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Old Value Cannot be blank' />" />

                                                                </td>
                                                                <td class="rigthTD" width="10%">
                                                                    <asp:TextBox ID="txtNewValue" runat="server" CssClass="textbox" TabIndex="5" />
                                                                    <asp:RequiredFieldValidator ID="reqtxtNewValue" runat="server" ControlToValidate="txtNewValue"
                                                                        ValidationGroup="save" ErrorMessage="New Value cannot be blank" SetFocusOnError="true"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='New Value cannot be blank' />" />


                                                                    <asp:CompareValidator ID="cvtxtNewValue" runat="server" Type="String" Display="Dynamic"
                                                                        ValidationGroup="save" ControlToCompare="txtOldValue" ControlToValidate="txtNewValue"
                                                                        ErrorMessage="Old Value and New Value cannot be same" Operator="NotEqual"
                                                                        Text="<img src='../../images/Error.png' title='Old Value and New Value cannot be same' />"></asp:CompareValidator>

                                                                </td>
                                                            </tr>
                                                            <tr id="ddlf2" runat="server">

                                                                <td class="rigthTD" width="10%"></td>

                                                                <td class="rigthTD" width="10%">
                                                                    <asp:DropDownList ID="ddlField2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlField2_SelectedIndexChanged" TabIndex="6">
                                                                        <asp:ListItem Text="----Select----" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>

                                                                </td>
                                                                <td class="rigthTD" width="10%">
                                                                    <asp:TextBox ID="txtOldValue2" runat="server" CssClass="textbox" TabIndex="7" />
                                                                    <asp:RequiredFieldValidator ID="rfvtxtOldValue2" runat="server" ControlToValidate="txtOldValue2"
                                                                        ValidationGroup="save" ErrorMessage="Old Value Cannot be blank" SetFocusOnError="true" Enabled="false"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Old Value Cannot be blank' />" />

                                                                </td>
                                                                <td class="rigthTD" width="10%">
                                                                    <asp:TextBox ID="txtNewValue2" runat="server" CssClass="textbox" TabIndex="8" />
                                                                    <asp:RequiredFieldValidator ID="rfvtxtNewValue2" runat="server" ControlToValidate="txtNewValue2"
                                                                        ValidationGroup="save" ErrorMessage="New Value Cannot be blank" SetFocusOnError="true" Enabled="false"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='New Value Cannot be blank' />" />

                                                                    <asp:CompareValidator ID="cvtxtNewValue2" runat="server" Type="String" Display="Dynamic"
                                                                        ValidationGroup="save" ControlToCompare="txtOldValue2" ControlToValidate="txtNewValue2"
                                                                        ErrorMessage="Old Value cannot be blank" Operator="NotEqual"
                                                                        Text="<img src='../../images/Error.png' title='Old Value cannot be blank' />"></asp:CompareValidator>
                                                                </td>
                                                            </tr>

                                                            <tr id="ddlf3" runat="server">

                                                                <td class="rigthTD" width="10%"></td>

                                                                <td class="rigthTD" width="10%">
                                                                    <asp:DropDownList ID="ddlField3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlField3_SelectedIndexChanged" TabIndex="9">
                                                                        <asp:ListItem Text="----Select----" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>

                                                                </td>
                                                                <td class="rigthTD" width="10%">
                                                                    <asp:TextBox ID="txtOldValue3" runat="server" CssClass="textbox" TabIndex="10" />
                                                                    <asp:RequiredFieldValidator ID="rfvtxtOldValue3" runat="server" ControlToValidate="txtOldValue3"
                                                                        ValidationGroup="save" ErrorMessage="Old Value Cannot be blank" SetFocusOnError="true" Enabled="false"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Old Value Cannot be blank' />" />

                                                                </td>
                                                                <td class="rigthTD" width="10%">
                                                                    <asp:TextBox ID="txtNewValue3" runat="server" CssClass="textbox" TabIndex="11" />
                                                                    <asp:RequiredFieldValidator ID="rfvtxtNewValue3" runat="server" ControlToValidate="txtNewValue3"
                                                                        ValidationGroup="save" ErrorMessage="New Value Cannot be blank" SetFocusOnError="true" Enabled="false"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='New Value Cannot be blank' />" />


                                                                    <asp:CompareValidator ID="cvtxtNewValue3" runat="server" Type="String" Display="Dynamic"
                                                                        ValidationGroup="save" ControlToCompare="txtOldValue3" ControlToValidate="txtNewValue3"
                                                                        ErrorMessage="Old Value and New Value cannot be same" Operator="NotEqual"
                                                                        Text="<img src='../../images/Error.png' title='Old Value and New Value cannot be same' />"></asp:CompareValidator>
                                                                </td>
                                                            </tr>

                                                            <tr id="ddlf4" runat="server">

                                                                <td class="rigthTD" width="10%"></td>

                                                                <td class="rigthTD" width="10%">
                                                                    <asp:DropDownList ID="ddlField4" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlField4_SelectedIndexChanged" TabIndex="12">
                                                                        <asp:ListItem Text="----Select----" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>

                                                                </td>
                                                                <td class="rigthTD" width="10%">
                                                                    <asp:TextBox ID="txtOldValue4" runat="server" CssClass="textbox" TabIndex="13" />
                                                                    <asp:RequiredFieldValidator ID="rfvtxtOldValue4" runat="server" ControlToValidate="txtOldValue4"
                                                                        ValidationGroup="save" ErrorMessage="Old Value Cannot be blank" SetFocusOnError="true" Enabled="false"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Old Value Cannot be blank' />" />

                                                                </td>
                                                                <td class="rigthTD" width="10%">
                                                                    <asp:TextBox ID="txtNewValue4" runat="server" CssClass="textbox" TabIndex="14" />
                                                                    <asp:RequiredFieldValidator ID="rfvtxtNewValue4" runat="server" ControlToValidate="txtNewValue4"
                                                                        ValidationGroup="save" ErrorMessage="New Value Cannot be blank" SetFocusOnError="true" Enabled="false"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='New Value Cannot be blank' />" />
                                                                    <asp:CompareValidator ID="cvtxtNewValue4" runat="server" Type="String" Display="Dynamic"
                                                                        ValidationGroup="save" ControlToCompare="txtOldValue4" ControlToValidate="txtNewValue4"
                                                                        ErrorMessage="Old Value and New Value cannot be same" Operator="NotEqual"
                                                                        Text="<img src='../../images/Error.png' title='Old Value and New Value cannot be same' />"></asp:CompareValidator>

                                                                </td>
                                                            </tr>

                                                            <tr id="ddlf5" runat="server">

                                                                <td class="rigthTD" width="10%"></td>

                                                                <td class="rigthTD" width="15%">
                                                                    <asp:DropDownList ID="ddlField5" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlField5_SelectedIndexChanged" TabIndex="15">
                                                                        <asp:ListItem Text="----Select----" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>

                                                                </td>
                                                                <td class="rigthTD" width="15%">
                                                                    <asp:TextBox ID="txtOldValue5" runat="server" CssClass="textbox" TabIndex="16" />
                                                                    <asp:RequiredFieldValidator ID="rfvtxtOldValue5" runat="server" ControlToValidate="txtOldValue5"
                                                                        ValidationGroup="save" ErrorMessage="Old Value Cannot be blank" SetFocusOnError="true" Enabled="false"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Old Value Cannot be blank' />" />

                                                                </td>
                                                                <td class="rigthTD" width="15%">
                                                                    <asp:TextBox ID="txtNewValue5" runat="server" CssClass="textbox" TabIndex="17" />
                                                                    <asp:RequiredFieldValidator ID="rfvtxtNewValue5" runat="server" ControlToValidate="txtNewValue5"
                                                                        ValidationGroup="save" ErrorMessage="New Value Cannot be blank" SetFocusOnError="true" Enabled="false"
                                                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='New Value Cannot be blank' />" />

                                                                    <asp:CompareValidator ID="cvtxtNewValue5" runat="server" Type="String" Display="Dynamic"
                                                                        ValidationGroup="save" ControlToCompare="txtOldValue5" ControlToValidate="txtNewValue5"
                                                                        ErrorMessage="Old Value and New Value cannot be same" Operator="NotEqual"
                                                                        Text="<img src='../../images/Error.png' title='Old Value and New Value cannot be same' />"></asp:CompareValidator>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td class="tdSpace" colspan="4"></td>
                                                            </tr>

                                                            <tr>
                                                                <td class="centerTD" colspan="4">
                                                                    <asp:Button ID="btnAdd" runat="server" ValidationGroup="save" Text="Save" CssClass="button"
                                                                        UseSubmitBehavior="true" TabIndex="18" OnClick="btnAdd_Click" />
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
                                                <asp:LinkButton ID="lnkAddNew" runat="server" Visible="false" OnClick="lnkAddNew_Click">Add New Profit Center<image src="../../images/Add.jpg" border="0px"></image></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <tr>
                                    <td class="tdSpace"></td>
                                </tr>

                                <tr>
                                    <td class="tdSpace"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grvProfitCenterChange" runat="server" AutoGenerateColumns="false" Width="100%"
                                            BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both" OnRowDataBound="grvProfitCenterChange_RowDataBound">
                                            <RowStyle CssClass="light-gray" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle CssClass="gridHeader" />
                                            <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="left" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPCMaster_Change_Id" runat="server" Text='<%# Eval("PCMaster_Change_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="sProfitCenter" HeaderText="Profit Center Code" ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="sProfitCenterName" HeaderText="Name" ItemStyle-Width="15%" />
                                                <asp:BoundField DataField="Company_Code" HeaderText="Company" Visible="false" />
                                                <asp:BoundField DataField="Module_Id" HeaderText="Acc. Grp." />
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        &nbsp;
                                                        <asp:ImageButton ID="lnkAddValue" ImageUrl="~/images/Add.jpg" runat="server" OnClick="lnkAddValue_Click"
                                                            ToolTip="Add Field" Font-Bold="true" CommandArgument='<%# Eval("PCMaster_Change_Id") %>' />&nbsp;
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Changes">
                                                    <ItemTemplate>
                                                        <asp:GridView ID="grvProfitCenterChangeDtl" runat="server" AutoGenerateColumns="false"
                                                            Width="100%" BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both">
                                                            <RowStyle CssClass="light-gray" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle CssClass="gridHeader" />
                                                            <AlternatingRowStyle CssClass="gridRowStyle" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPCMaster_Change_Details_Id" runat="server" Text='<%# Eval("PCMaster_Change_Details_Id") %>'></asp:Label>
                                                                        <asp:Label ID="lblSectionFieldMasterId" runat="server" Text='<%# Eval("Section_Field_Master_Id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Decsription" HeaderText="Section" ItemStyle-Width="22%" />
                                                                <asp:BoundField DataField="FeildDisplayName" HeaderText="Field" ItemStyle-Width="22%" />
                                                                <asp:BoundField DataField="sOld_Value" HeaderText="Old Value" ItemStyle-Width="22%" />
                                                                <asp:BoundField DataField="sNew_Value" HeaderText="New Value" ItemStyle-Width="22%" />
                                                                <asp:TemplateField Visible="false" ItemStyle-Width="12%">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/Edit.jpg" OnClick="btnEdit_Click"
                                                                            ToolTip="Edit Field" Font-Bold="true" CommandArgument='<%# Eval("PCMaster_Change_Details_Id") %>' />&nbsp;
                                                                        <asp:ImageButton ID="btnDelete" runat="server" Text="X" ImageUrl="~/images/Delete.bmp"
                                                                            ToolTip="Delete Field" ForeColor="Red" Font-Size="15px" OnClick="btnDelete_Click"
                                                                            Font-Bold="true" CommandArgument='<%# Eval("PCMaster_Change_Details_Id") %>' OnClientClick="return confirm('Are you certain you want to delete this entry?');" />&nbsp;
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
            <tr>
                <td class="leftTD" align="left" colspan="2">
                    <b>Attach Documents (Image/PDF Files Only)</b>
                </td>
            </tr>
            <tr>
                <td class="rigthTD" align="left" valign="top">
                    <div>
                        <asp:FileUpload ID="file_upload" class="multi" runat="server" TabIndex="19" />
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
            </tr>
            <tr id="trButton" runat="server" visible="false">
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnPrevious" runat="server" Text="Back" UseSubmitBehavior="false"
                        TabIndex="20" CssClass="button" OnClick="btnPrevious_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" UseSubmitBehavior="true"
                        TabIndex="21" OnClick="btnSave_Click" />
                    <asp:Button ID="btnNext" runat="server" Text="Save & Next" UseSubmitBehavior="true"
                        TabIndex="22" CssClass="button" OnClick="btnNext_Click" Width="120px" />
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="4"></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="save" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SaveValue"
        ShowMessageBox="true" ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <%--<asp:Label ID="lblSectionId" runat="server" Text="90" Visible="false" />--%>
    <asp:Label ID="lblSectionId" runat="server" Text="<%$appSettings:SECPCC %>" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblProfitCenterChangeId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />

</asp:Content>


