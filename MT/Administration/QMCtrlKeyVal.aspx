<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master"
    AutoEventWireup="true" CodeFile="QMCtrlKeyVal.aspx.cs" Inherits="Administration_QMCtrlKeyVal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upMsg" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="upAppSearch" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="trHeading" align="center" colspan="24">QM Ctrl Key Validation 
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="tdSpace"></td>
                </tr>
                <tr>

                    <td class="leftTD" align="left" style="width: 25%">Module
                    </td>
                    <td class="rigthTD" align="left">
                        <asp:DropDownList ID="ddlModuleSearch" runat="server"
                            AppendDataBoundItems="true">
                            <asp:ListItem Text="All" Value="0" />
                        </asp:DropDownList>

                    </td>
                    <td class="leftTD" align="left" style="width: 25%">Plant
                    </td>
                    <td class="rigthTD" align="left">
                        <asp:DropDownList ID="ddlPlantCode" runat="server"
                            AppendDataBoundItems="true">
                        </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="tdSpace"></td>
                </tr>
                <tr>
                    <td class="centerTD" colspan="4">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button"
                            OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="tdSpace"></td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlData" runat="server">
                <div style="overflow-y: auto; width: 1100px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="trHeading" align="center" colspan="2">PRICE MASTER CREATION FORM
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="2">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="tdSpace" colspan="2"></td>
                                    </tr>


                                    <tr>

                                        <td align="left" valign="top" colspan="2">
                                            <asp:GridView ID="gvQMCtrlKeyVal" runat="server" AutoGenerateColumns="false" ShowFooter="true">

                                                <Columns>
                                                    <asp:BoundField DataField="RowNumber" HeaderText="Row Number" Visible="false" />
                                                    <asp:TemplateField HeaderText="ID" ItemStyle-Width="0" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="txtID" runat="server" Text='<%# Eval("ID") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Module Code" ItemStyle-Width="150">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlModule" runat="server" AppendDataBoundItems="false" Width="125px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqddlModule" runat="server" ControlToValidate="ddlModule"
                                                                ValidationGroup="save" ErrorMessage="Select the Module type." SetFocusOnError="true"
                                                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select the Module type.' />" />
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnAdd" runat="server" Text="Add New Row" OnClick="btnAdd_Click" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Plant Code" ItemStyle-Width="150">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlPlantCode" runat="server" AppendDataBoundItems="false" Width="125px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqddlPlantCode" runat="server" ControlToValidate="ddlPlantCode"
                                                                ValidationGroup="save" ErrorMessage="Select the Plant Code." SetFocusOnError="true"
                                                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select the Plant Code.' />" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Mandatory" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlMandatory" runat="server" AppendDataBoundItems="false" Width="125px">
                                                                <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqddlMandatory" runat="server" ControlToValidate="ddlMandatory"
                                                                ValidationGroup="save" ErrorMessage="Select the Mandatory type." SetFocusOnError="true"
                                                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select the Mandatory type.' />" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Show" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlsCondintion_type" runat="server" AppendDataBoundItems="false" Width="125px">
                                                                <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqddlsCondintion_type" runat="server" ControlToValidate="ddlsCondintion_type"
                                                                ValidationGroup="save" ErrorMessage="Select the condintion type." SetFocusOnError="true"
                                                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select the condintion type.' />" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Enable" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlEnable" runat="server" AppendDataBoundItems="false" Width="125px">
                                                                <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="reqddlEnable" runat="server" ControlToValidate="ddlEnable"
                                                                ValidationGroup="save" ErrorMessage="Select the Enable type." SetFocusOnError="true"
                                                                InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select the Enable type.' />" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Default Value" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDefaultValue" runat="server" Text='<%# Eval("sDefualtValue") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Deletion Flag" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtDeletionFlag" runat="server" Text='<%# Eval("bIsActive") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Save Data">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnSaveRow" runat="server" OnClick="btnSaveRow_Click" Text="Save Row" ValidationGroup="save" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>
                                        </td>


                                    </tr>

                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>


        </ContentTemplate>
        <%--<Triggers> 
            <asp:AsyncPostBackTrigger ControlID="btnSave" /> 
            <asp:AsyncPostBackTrigger ControlID="btnCreateNew" />
        </Triggers>--%>
    </asp:UpdatePanel>


    <asp:UpdateProgress ID="updateProgressMaster" runat="server" DisplayAfter="5">
        <ProgressTemplate>
            <div id="light" class="loading">
                <img src="../images/ajax-loader-proccessing.gif" alt="Loading" height="30px" />
            </div>
            <div class="transparent_overlay">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="save" ShowMessageBox="true"
        ShowSummary="false" />

</asp:Content>

