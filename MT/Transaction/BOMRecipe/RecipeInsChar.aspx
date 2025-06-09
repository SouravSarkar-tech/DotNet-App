<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/BOMRecipe/BOMRecipeMasterPage.master" AutoEventWireup="true" CodeFile="RecipeInsChar.aspx.cs" Inherits="Transaction_BOMRecipe_RecipeInsChar" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlInspChara" Visible="False" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">Inspection characteristics
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2"></td>
                    </tr>
                    <tr>
                        <td align="right" class="leftTD" colspan="2" valign="middle">
                            <asp:Label ID="lblQualityMgmt" runat="server" Text="Quality Management"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="leftTD" style="width: 20%">Insp. Points
                                                            <asp:Label ID="labelddlInspPoints" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 70%">
                            <table>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlInspPoints" runat="server" AppendDataBoundItems="True" Width="250px"
                                            CssClass="dropdownlist" Enabled="False">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlInspPoints" runat="server" ControlToValidate="ddlInspPoints"
                                            ValidationGroup="save" ErrorMessage="Insp. Points cannot be blank." SetFocusOnError="True"
                                            Enabled="False" Display="Dynamic" Text="<img src='../../images/Error.png' title='Insp. Points cannot be blank.' />" />
                                    </td>
                                    <td align="right" class="leftTD" style="width: 30%">Sampling Procedure
                                    </td>
                                    <td align="right" class="leftTD" style="width: 40%">
                                        <asp:DropDownList ID="ddlSamplingProceduremain" CssClass="dropdownlist" Width="250px"
                                            runat="server" OnSelectedIndexChanged="ddlSamplingProceduremain_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="leftTD" style="width: 20%">Partial-lot assign.
                                                            <asp:Label ID="labelddlPartialLot" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                        </td>
                        <td class="rigthTD" style="width: 70%">
                            <table>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlPartialLot" runat="server" AppendDataBoundItems="True" Width="250px"
                                            CssClass="dropdownlist">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPartialLot" runat="server" ControlToValidate="ddlPartialLot"
                                            Enabled="False" ValidationGroup="save" ErrorMessage="Partial-lot assign. cannot be blank."
                                            SetFocusOnError="True" Display="Dynamic" Text="<img src='../../images/Error.png' title='Partial-lot assign. cannot be blank.' />" />
                                    </td>
                                    <td align="right" class="leftTD" style="width: 30%">Insp.Point Completion
                                    </td>
                                    <td align="right" class="leftTD" style="width: 40%">
                                        <asp:DropDownList ID="ddlInspPtCmptmain" CssClass="dropdownlist" Width="250px" runat="server">
                                            <asp:ListItem Text="Select"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlInspPtCmptmain" runat="server" ControlToValidate="ddlInspPtCmptmain"
                                            ValidationGroup="save" ErrorMessage="Insp. point completion cannot be blank."
                                            Enabled="False" SetFocusOnError="True" Display="Dynamic" Text="<img src='../../images/Error.png' title='Insp. point completion cannot be blank.' />" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rigthTD" colspan="2">
                            <div style="height: auto;">
                                <asp:GridView ID="gvInspChara" runat="server" AutoGenerateColumns="False" CssClass="GridClass"
                                    DataKeyNames="Recipe_InspChara_Id,Sampling_Procedure,MIC" OnRowCommand="gvInspChara_RowCommand"
                                    OnRowDataBound="gvInspChara_RowDataBound" ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="D">  
                                                                                <img src="../../images/delete.png" alt="Delete" title='Delete'/></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Operation Phase">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOperationPhase" runat="server" CssClass="textbox" Enabled="false"
                                                    Text='<%#Eval("Operation_Phase") %>' Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecipe_InspChara_Id" runat="server" Text='<%#Eval("Recipe_InspChara_Id") %>'
                                                    Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chara No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCharacteristicNo" runat="server" CssClass="textbox" onkeypress="return IsNumber();"
                                                    Text='<%#Eval("Characteristic_No") %>' Width="30px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtCharacteristicNo" runat="server" ControlToValidate="txtCharacteristicNo"
                                                    Display="Dynamic" ErrorMessage="Characteristic No. cannot be blank." SetFocusOnError="true"
                                                    Text="&lt;img src='../../images/Error.png' title='Characteristic No. cannot be blank.' /&gt;"
                                                    ValidationGroup="save" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Master Insp Chara Code">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMIC" runat="server" AutoPostBack="true" CssClass="textbox" MaxLength="10"
                                                    OnTextChanged="txtMIC_TextChanged" Text='<%#Eval("MIC") %>'></asp:TextBox>
                                                <%--<asp:DropDownList ID="ddlMIC" CssClass="dropdownlist" runat="server" OnSelectedIndexChanged="ddlMIC_SelectedIndexChanged"
                                                                                    AutoPostBack="true">
                                                                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                                                </asp:DropDownList>--%>
                                                <asp:RequiredFieldValidator ID="reqtxtMIC" runat="server" ControlToValidate="txtMIC"
                                                    Display="Dynamic" ErrorMessage="MIC cannot be blank." SetFocusOnError="true"
                                                    Text="&lt;img src='../../images/Error.png' title='MIC cannot be blank.' /&gt;"
                                                    ValidationGroup="save" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sampling Procedure">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlSamplingProcedure" runat="server" CssClass="dropdownlist">
                                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlSamplingProcedure" runat="server" ControlToValidate="ddlSamplingProcedure"
                                                    Display="Dynamic" ErrorMessage="Sampling procedure cannot be blank." SetFocusOnError="true"
                                                    Text="&lt;img src='../../images/Error.png' title='Sampling procedure cannot be blank.' /&gt;"
                                                    ValidationGroup="save" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code Group">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCodeGrp" runat="server" CssClass="textbox" Text='<%#Eval("CodeGrp") %>'
                                                    Width="100px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtCodeGrp" runat="server" ControlToValidate="txtCodeGrp"
                                                    Display="Dynamic" Enabled="false" ErrorMessage="Code Group cannot be blank."
                                                    SetFocusOnError="true" Text="&lt;img src='../../images/Error.png' title='Code Group cannot be blank.' /&gt;"
                                                    ValidationGroup="save" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No Relation">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkNoRelation" runat="server" Enabled="false" ToolTip="No Relation" />
                                                <asp:HiddenField ID="hdnNoRelation" runat="server" Value='<%#Eval("NoRelation") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="gridFooter" />
                                    <HeaderStyle BackColor="#EDF5FF" />
                                    <RowStyle CssClass="grdViewRow" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr id="trInspPhase" runat="server" colspan="2" visible="False">
                        <td runat="server" align="left" class="leftTD">Operation Phase
                                                            <asp:DropDownList ID="ddlOperationPhase" runat="server" CssClass="dropdownlist">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                            </asp:DropDownList>
                            <asp:TextBox ID="txtAddRowInsp" runat="server" CssClass="textbox" onkeypress="return IsNumber();"
                                Text="10" Width="50px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtxtAddRowInsp" runat="server" ControlToValidate="txtAddRowInsp"
                                Display="Dynamic" ErrorMessage="Please enter the number of rows to be added."
                                SetFocusOnError="True" Text="&lt;img src='../../images/Error.png' title='Please enter the number of rows to be added.' /&gt;"
                                ValidationGroup="valInspChar"></asp:RequiredFieldValidator>
                            <asp:Button ID="btnAddInspChara" runat="server" CssClass="button" OnClick="btnAddInspChara_Click"
                                Text="New Row" ValidationGroup="valInspChar" />
                        </td>
                    </tr>


                    <%--ITSM413605--%>
                    <tr runat="server" colspan="3" id="trSecResfile" visible="false">
                        <td align="left" class="leftTD">Browse file 
                            <asp:HyperLink ID="hlMSImportFormat" runat="server" Text="click here" Target="_blank"></asp:HyperLink>

                            to download format

                              <asp:UpdatePanel ID="UupdtUpload" runat="server">
                                  <ContentTemplate>
                                      <asp:FileUpload ID="fileUpdsecResorce" runat="server" TabIndex="1" />
                                  </ContentTemplate>
                                  <Triggers>
                                      <asp:PostBackTrigger ControlID="btnMSProcess" />
                                  </Triggers>
                              </asp:UpdatePanel>
                        </td>
                    </tr>


                    <tr id="trgrdAtt" runat="server" visible="false">
                        <td class="rigthTD" align="left" colspan="3">
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

                    <tr id="trButtonuf" runat="server" colspan="2" visible="false">
                        <td class="centerTD" colspan="2">
                            <asp:Button ID="btnMSProcess" runat="server" OnClick="btnMSProcess_Click" Text="Upload" CssClass="button" />
                            <%--<asp:Button ID="btnSRValidation" runat="server" OnClick="btnSRValidation_Click" Text="Validation" CssClass="button" />--%>
                            <asp:Button ID="btnSRSubmit" runat="server" OnClick="btnSRSubmit_Click" Text="Submit" CssClass="button" />
                        </td>
                    </tr>

                    <%--ITSM413605--%>

                    <tr id="trButton" runat="server" visible="false">
                        <td class="centerTD" colspan="2">
                            <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="save" TabIndex="26"
                                CssClass="button" OnClick="btnPrevious_Click" />
                            <asp:Button ID="btnSave" runat="server" ValidationGroup="save" UseSubmitBehavior="true"
                                Text="Save" CssClass="button" TabIndex="27" OnClick="btnSave_Click" />
                            <asp:Button ID="btnNext" runat="server" ValidationGroup="save" Text="Save & Next"
                                TabIndex="28" UseSubmitBehavior="true" CssClass="button" OnClick="btnNext_Click"
                                Width="120px" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblUserId" runat="server" Visible="false" />
                <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
                <asp:Label ID="lblRecipeId" runat="server" Visible="false" />
                <asp:Label ID="lblMode" runat="server" Visible="false" />
                <asp:Label ID="lblModuleId" runat="server" Visible="false" />
                <asp:Label ID="lblActionType" runat="server" Style="display: none" />
                <asp:Label ID="lblSectionId" runat="server" Text="87" Visible="false" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

