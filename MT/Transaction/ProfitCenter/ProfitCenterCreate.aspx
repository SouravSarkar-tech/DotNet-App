<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/ProfitCenter/ProfitCenterMaster.master" AutoEventWireup="true" CodeFile="ProfitCenterCreate.aspx.cs" Inherits="Transaction_ProfitCenter_ProfitCenterCreate" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetToolTip(control) {
            ddrivetip('236', control);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">PROFIT CENTER CREATION FORM
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Profit Center
                                <asp:Label ID="LabeltxtProfitCenter" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtProfitCenter" runat="server" CssClass="textbox" Width="150px"
                                    MaxLength="8" TabIndex="1" onkeypress="return IsNumber();" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtProfitCenter" runat="server" ControlToValidate="txtProfitCenter"
                                    ValidationGroup="save" ErrorMessage="Profit Center Code cannot be blank" SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Profit Center Code cannot be blank' />" />
                           
                                <asp:RegularExpressionValidator ID="regtxtProfitCenter" runat="server" ControlToValidate="txtProfitCenter"
                                    ValidationGroup="save" ErrorMessage="Invalid Profit Center Code." SetFocusOnError="true"
                                    ValidationExpression="^[\S]{4,8}$" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Profit Center Code.' />" />

                            </td>

                            <td class="leftTD" style="width: 20%">Reference Profit Center
                                <asp:Label ID="LabeltxtRefProfitCenter" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtRefProfitCenter" runat="server" CssClass="textbox" Width="150px" TabIndex="2"
                                    MaxLength="8" onkeypress="return IsNumber();" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="rfvtxtRefProfitCenter" runat="server" ControlToValidate="txtRefProfitCenter"
                                    ValidationGroup="save" ErrorMessage="Ref. Profit Center Code cannot be blank" SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Ref. Profit Center Code cannot be blank' />" />
                                <asp:RegularExpressionValidator ID="regtxtRefProfitCenter" runat="server" ControlToValidate="txtRefProfitCenter"
                                    ValidationGroup="save" ErrorMessage="Invalid Ref. Profit Center Code." SetFocusOnError="true"
                                    ValidationExpression="^[\S]{4,8}$" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Ref. Profit Center Code.' />" />
                                <asp:CompareValidator ID="cvtxtRefProfitCenter" runat="server" Type="String" Display="Dynamic"
                                    ValidationGroup="save" ControlToCompare="txtProfitCenter" ControlToValidate="txtRefProfitCenter"
                                    ErrorMessage="Profit Center and Ref. Profit Center cannot be same." Operator="NotEqual"
                                    Text="<img src='../../images/Error.png' title='Profit Center and Ref. Profit Center cannot be same.' />"></asp:CompareValidator>

                            </td>

                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Anaylisis Period
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtValidFrom" runat="server" CssClass="textbox" TabIndex="3" />
                                <act:CalendarExtender ID="CaltxtValidFrom" runat="server" Format="dd/MM/yyyy" TargetControlID="txtValidFrom" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtValidFrom"
                                    ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                    Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                    ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                            </td>


                            <td class="leftTD" style="width: 20%">To
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtValidTo" runat="server" CssClass="textbox" TabIndex="4" />
                                <act:CalendarExtender ID="CaltxtValidTo" runat="server" Format="dd/MM/yyyy" TargetControlID="txtValidTo" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtValidTo"
                                    ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                    Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                    ValidationGroup="save" Display="Dynamic" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"></asp:RegularExpressionValidator>

                            </td>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td class="leftTD" style="width: 20%">Controlling Area
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlControllingArea" runat="server" TabIndex="5" AppendDataBoundItems="false">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td class="leftTD" style="width: 20%">Name  
                                <asp:Label ID="labletxtName" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtName" runat="server" CssClass="textbox" TabIndex="6" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtName" runat="server" ControlToValidate="txtName"
                                    ValidationGroup="save" ErrorMessage="Name cannot be blank" SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Name cannot be blank' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">Company Code
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlCompanyCode" runat="server" TabIndex="13">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlCompanyCode" runat="server" ControlToValidate="ddlCompanyCode"
                                   InitialValue="0"  ValidationGroup="save" ErrorMessage="Company Code cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Company Code cannot be blank.' />" />
                         
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Long Text
                                <asp:Label ID="labeltxtLongText" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%" colspan="3">
                                <asp:TextBox ID="txtLongText" runat="server" CssClass="textbox" TabIndex="7" MaxLength="40"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtLongText" runat="server" ControlToValidate="txtLongText"
                                    ValidationGroup="save" ErrorMessage="Long Text cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Long Text cannot be blank.' />" />
                            </td>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td class="leftTD" style="width: 20%">User Responsible
                                
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtUserResp" runat="server" CssClass="textbox" Width="150px" TabIndex="8"></asp:TextBox>
                            </td>

                            <td class="leftTD">Person Responsible
                                <asp:Label ID="labeltxtPersonResp" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtPersonResp" runat="server" CssClass="textbox" TabIndex="9"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtPersonResp" runat="server" ControlToValidate="txtPersonResp"
                                    ValidationGroup="save" ErrorMessage="Person Responsible cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Person Responsible cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">Department
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtDepartment" runat="server" CssClass="textbox" Width="150px" TabIndex="10"></asp:TextBox>
                            </td>

                            <td class="leftTD" style="width: 20%">Profit Center Group
                                <asp:Label ID="LabelddlpcGroup" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlpcGroup" runat="server" TabIndex="11" AppendDataBoundItems="false">
                                    <asp:ListItem Text="---Select---" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlpcGroup" runat="server" ControlToValidate="ddlpcGroup"
                                    ValidationGroup="save" ErrorMessage="Please select Profit Center Group." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please Select Profit Center Group.' />" />
                            </td>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Segment   
                                <asp:Label ID="LabelddlSegment" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlSegment" runat="server" TabIndex="11" AppendDataBoundItems="false">
                                    <asp:ListItem Text="---Select---" Value="0" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlSegment" runat="server" ControlToValidate="ddlSegment"
                                    ValidationGroup="save" ErrorMessage="Please select Segment." SetFocusOnError="true"
                                    InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please Select Segment.' />" />
                            </td>
                            <td class="leftTD">Remarks
                                  <asp:Label ID="LabeltxtRemarks" runat="server" ForeColor="Red" Text="*"></asp:Label><br />
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox" Width="150px" TabIndex="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtRemarks" runat="server" ControlToValidate="txtRemarks"
                                    ValidationGroup="save" ErrorMessage="Remarks cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Remarks cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>


                        <tr>
                            <td class="leftTD" align="left" colspan="4">
                                <b>Attach Documents (Image/PDF Files Only)</b>
                            </td>
                        </tr>
                        <tr>
                            <td class="rigthTD" align="left" colspan="2" valign="top">
                                <div>
                                    <asp:FileUpload ID="file_upload" class="multi" runat="server" TabIndex="16" />
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
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnPrevious" runat="server" ValidationGroup="save" Text="Back" UseSubmitBehavior="false"
                                    TabIndex="17" CssClass="button" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="save" Text="Save" CssClass="button"
                                    UseSubmitBehavior="true" TabIndex="18" OnClick="btnSave_Click" />
                                <asp:Button ID="btnNext" runat="server" ValidationGroup="save" Text="Save & Next"
                                    UseSubmitBehavior="true" TabIndex="19" CssClass="button"
                                    Width="120px" OnClick="btnNext_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="save" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <%--<asp:Label ID="lblSectionId" runat="server" Text="236" Visible="false" />--%>
    <asp:Label ID="lblSectionId" runat="server" Text="<%$appSettings:SECPCN %>" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblCustomerGeneralId" runat="server" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
</asp:Content>

