<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/BOMRecipe/BOMRecipeMasterPage.master" AutoEventWireup="true" CodeFile="ReciepeSecRec.aspx.cs" Inherits="Transaction_BOMRecipe_ReciepeSecRec" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlSecRes" Visible="false" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">Secondary Resources
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace"></td>
                    </tr>
                    <tr>
                        <td class="rigthTD">
                            <div style="height: auto; overflow-x: auto; width: 950px;">
                                <asp:GridView ID="grdSecResources" runat="server" AutoGenerateColumns="False" CssClass="GridClass"
                                    DataKeyNames="Recipe_SecResource_Id,SecResource" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found"
                                    OnRowDataBound="grdSecResources_RowDataBound" OnRowCommand="grdSecResources_RowCommand">
                                    <HeaderStyle BackColor="#EDF5FF" />
                                    <FooterStyle CssClass="gridFooter" />
                                    <RowStyle CssClass="grdViewRow" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDeleteSec" runat="server" CommandName="D" CausesValidation="false">  
                                                                                <img src="../../images/delete.png" alt="Delete" title='Delete'/></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Operation Phase">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtOperationPhase" Text='<%#Eval("Operation_Phase") %>'
                                                    CssClass="textbox" Width="50px" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecipe_SecResource_Id" runat="server" Text='<%#Eval("Recipe_SecResource_Id") %>'
                                                    Visible="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sec. Resource Item">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSecRecItem" Text='<%#Eval("SecResource_Item") %>'
                                                    CssClass="textbox" Width="50px" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Secondary Resource">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlSecResource" runat="server" OnSelectedIndexChanged="ddlSecResource_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqddlSecResource" runat="server" ControlToValidate="ddlSecResource"
                                                    ValidationGroup="save" ErrorMessage="Secondary resource cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Secondary resource cannot be blank.' />" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Duration">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtDuration" Text='<%#Eval("Duration") %>' CssClass="textbox"
                                                    Width="100px" onkeypress="return IsNumber();" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtDuration" runat="server" ControlToValidate="txtDuration"
                                                    ValidationGroup="save" ErrorMessage="Duration cannot be blank." SetFocusOnError="true"
                                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Duration cannot be blank.' />"
                                                    Enabled="false" />
                                                <asp:RegularExpressionValidator ID="regtxtDuration" ControlToValidate="txtDuration"
                                                    runat="server" ErrorMessage="Sec Resource Duration should have numeric value up to 3 decimal place only."
                                                    Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                    Text="<img src='../../images/Error.png' title='Sec Resource Duration should have numeric value up to 3 decimal place only.' />" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <%--<asp:DropDownList ID = "ddlUnit" runat = "server" Enabled = "false" Visible = "false"></asp:DropDownList>--%>
                                                <asp:TextBox runat="server" ID="txtUnit1" Text='<%#Eval("Unit1") %>' CssClass="textbox"
                                                    Width="100px" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Activity Type">
                                            <ItemTemplate>
                                                <%--<asp:DropDownList ID = "ddlActivityType" runat="server" Visible = "false"></asp:DropDownList>--%>
                                                <asp:TextBox runat="server" ID="txtActivityType1" Text='<%#Eval("ActivityType1") %>'
                                                    CssClass="textbox" Width="100px" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Process">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtProcess" Text='<%#Eval("Process") %>' CssClass="textbox"
                                                    Width="100px" onkeypress="return IsNumber();" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtProcess" runat="server" ControlToValidate="txtProcess"
                                                    ValidationGroup="save" ErrorMessage="Process cannot be blank." SetFocusOnError="true"
                                                    Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Process cannot be blank.' />" />
                                                <asp:RegularExpressionValidator ID="regtxtProcess" ControlToValidate="txtProcess"
                                                    runat="server" ErrorMessage="Sec Resource Process should have numeric value up to 3 decimal place only."
                                                    Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                    Text="<img src='../../images/Error.png' title='Sec Resource Process should have numeric value up to 3 decimal place only.' />" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <%--<asp:DropDownList ID = "ddlUnit" runat = "server" Enabled = "false" Visible = "false"></asp:DropDownList>--%>
                                                <asp:TextBox runat="server" ID="txtUnit2" Text='<%#Eval("Unit2") %>' CssClass="textbox"
                                                    Width="100px" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Activity Type">
                                            <ItemTemplate>
                                                <%--<asp:DropDownList ID = "ddlActivityType" runat="server" Visible = "false"></asp:DropDownList>--%>
                                                <asp:TextBox runat="server" ID="txtActivityType2" Text='<%#Eval("ActivityType2") %>'
                                                    CssClass="textbox" Width="100px" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Labor">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtLabor" Text='<%#Eval("Labor") %>' CssClass="textbox"
                                                    Width="100px" onkeypress="return IsNumber();" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtLabor" runat="server" ControlToValidate="txtLabor"
                                                    ValidationGroup="save" ErrorMessage="Labor cannot be blank." SetFocusOnError="true"
                                                    Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Labor cannot be blank.' />" />
                                                <asp:RegularExpressionValidator ID="regtxtLabor" ControlToValidate="txtLabor" runat="server"
                                                    ErrorMessage="Sec Resource Labor should have numeric value up to 3 decimal place only."
                                                    Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                    Text="<img src='../../images/Error.png' title='Sec Resource Labor should have numeric value up to 3 decimal place only.' />" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <%--<asp:DropDownList ID = "ddlUnit" runat = "server" Enabled = "false" Visible = "false"></asp:DropDownList>--%>
                                                <asp:TextBox runat="server" ID="txtUnit3" Text='<%#Eval("Unit3") %>' CssClass="textbox"
                                                    Width="100px" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Activity Type">
                                            <ItemTemplate>
                                                <%--<asp:DropDownList ID = "ddlActivityType" runat="server" Visible = "false"></asp:DropDownList>--%>
                                                <asp:TextBox runat="server" ID="txtActivityType3" Text='<%#Eval("ActivityType3") %>'
                                                    CssClass="textbox" Width="100px" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr id="trSecRes" runat="server" visible="false" colspan="2">
                        <td align="left" class="leftTD">Operation Phase
                                                            <asp:DropDownList ID="ddlSecRes" runat="server" CssClass="dropdownlist">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                            </asp:DropDownList>
                            <asp:TextBox ID="txtNoSecRes" runat="server" Text="5" CssClass="textbox" Width="50px"
                                onkeypress="return IsNumber();"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtxtNoSecRes" runat="server" ControlToValidate="txtNoSecRes"
                                ValidationGroup="valSecRes" ErrorMessage="Please enter the number of rows to be added."
                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please enter the number of rows to be added.' />" />
                            <asp:Button ID="btnAddSecRes" runat="server" Text="New Row" CssClass="button" OnClick="btnAddSecRes_Click"
                                ValidationGroup="valSecRes" />
                        </td>
                    </tr>

                    <%--ITSM413605--%>
                    <tr runat="server" colspan="2" id="trSecResfile" visible="false">
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
                <asp:Label ID="lblSectionId" runat="server" Text="88" Visible="false" />

                <%--                <asp:DropDownList ID="ddlSecResource" runat="server" Visible="false">
                </asp:DropDownList>--%>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

