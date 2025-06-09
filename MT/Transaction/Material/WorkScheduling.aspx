<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="WorkScheduling.aspx.cs" Inherits="Transaction_WorkScheduling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialWorkSch" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <table border="0" cellpadding="0" width="100%">
                <tr>
                    <td class="trHeading" align="center" colspan="2">
                        Work Scheduling
                    </td>
                </tr>
                <tr>
                    <td class="tdSpace" colspan="2">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" Width="100%"
                                DataKeyNames="Mat_Work_Scheduling_Id" BorderColor="#9D9D9D" EmptyDataText="No Data Found"
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
                        <asp:Panel ID="pnlData" runat="server">
                            <table border="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Plant
                                        <asp:Label ID="lableddlPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlPlant" runat="server" ControlToValidate="ddlPlant"
                                            ValidationGroup="WS" ErrorMessage="Plant cannot be blank." SetFocusOnError="true"
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
                                        Production Unit
                                        <asp:Label ID="lableddlProductionUnit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlProductionUnit" runat="server" AppendDataBoundItems="false"
                                            TabIndex="2">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlProductionUnit" runat="server" ControlToValidate="ddlProductionUnit"
                                            ValidationGroup="WS" ErrorMessage="Production Unit cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Production Unit cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Unit Of Issue
                                        <asp:Label ID="lableddlUnitOfIssue" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlUnitOfIssue" runat="server" AppendDataBoundItems="false"
                                            TabIndex="3">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlUnitOfIssue" runat="server" ControlToValidate="ddlUnitOfIssue"
                                            ValidationGroup="WS" ErrorMessage="Unit Of Issue cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Unit Of Issue cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Production Supervisor
                                        <asp:Label ID="lableddlProductionSupervisor" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlProductionSupervisor" runat="server" AppendDataBoundItems="false"
                                            TabIndex="4">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlProductionSupervisor" runat="server" ControlToValidate="ddlProductionSupervisor"
                                            ValidationGroup="WS" ErrorMessage="Production Supervisor cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Production Supervisor cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Prod Sched Profile
                                        <asp:Label ID="lableddlProdSchedProfile" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlProdSchedProfile" runat="server" AppendDataBoundItems="false"
                                            TabIndex="5">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlProdSchedProfile" runat="server" ControlToValidate="ddlProdSchedProfile"
                                            ValidationGroup="WS" ErrorMessage="Prod Sched Profile cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Prod Sched Profile cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">
                                        Underdelivered Tolerance Lmt
                                        <asp:Label ID="labletxtUnderdeliveredToleranceLmt" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtUnderdeliveredToleranceLmt" runat="server" MaxLength="5" CssClass="textbox"
                                            TabIndex="6" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtUnderdeliveredToleranceLmt" runat="server"
                                            ControlToValidate="txtUnderdeliveredToleranceLmt" ValidationGroup="WS" ErrorMessage="Underdelivered Tolerance Lmt cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Underdelivered Tolerance Lmt cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Overdelivered_Tolerance_Lmt
                                        <asp:Label ID="labletxtOverdeliveredToleranceLmt" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtOverdeliveredToleranceLmt" runat="server" MaxLength="5" CssClass="textbox"
                                            TabIndex="7" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtOverdeliveredToleranceLmt" runat="server" ControlToValidate="txtOverdeliveredToleranceLmt"
                                            ValidationGroup="WS" ErrorMessage="Repetitive manufacturing profile cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Repetitive manufacturing profile cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Unlimited
                                        <asp:Label ID="lablechkUnlimited" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:CheckBox ID="chkUnlimited" runat="server" TabIndex="8" />
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
                                        Serial Number Profile
                                        <asp:Label ID="lableddlSerialNumberProfile" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlSerialNumberProfile" runat="server" AppendDataBoundItems="false"
                                            TabIndex="9">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlSerialNumberProfile" runat="server" ControlToValidate="ddlSerialNumberProfile"
                                            ValidationGroup="WS" ErrorMessage="Serial Number Profile cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Serial Number Profile cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">
                                        Repetitive manufacturing profile
                                        <asp:Label ID="lableddlRepetitiveManProfile" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:DropDownList ID="ddlRepetitiveManProfile" runat="server" AppendDataBoundItems="false"
                                            TabIndex="10">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlRepetitiveManProfile" runat="server" ControlToValidate="ddlRepetitiveManProfile"
                                            ValidationGroup="WS" ErrorMessage="Repetitive manufacturing profile cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Repetitive manufacturing profile cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr id="trButton" runat="server" visible="false">
                                    <td class="centerTD" colspan="4">
                                        <asp:Button ID="btnPrevious" runat="server" ValidationGroup="WS" Text="Back" UseSubmitBehavior="false"
                                            TabIndex="11" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="WS" Text="Save" CssClass="button"
                                            TabIndex="12" UseSubmitBehavior="true" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnNext" runat="server" ValidationGroup="WS" Text="Save & Next" CssClass="button"
                                            TabIndex="13" OnClick="btnNext_Click" Width="120px" UseSubmitBehavior="true" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="WS" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblWSId" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="21" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
