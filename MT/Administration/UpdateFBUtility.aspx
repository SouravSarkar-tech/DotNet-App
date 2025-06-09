<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="UpdateFBUtility.aspx.cs" Inherits="Administration_UpdateFBUtility" %>

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

    <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlAddNew" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">Update BOM Data from Backend
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Enter Request No.
                                        <asp:Label ID="lblRequestNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtRequestNo" runat="server" CssClass="textbox"
                                            MaxLength="20" Width="25%" />

                                    </td>
                                    <asp:RequiredFieldValidator ID="reqtxtRequestNo" runat="server" ControlToValidate="txtRequestNo"
                                        ValidationGroup="save" ErrorMessage="Request No cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../images/Error.png' title='Request No cannot be blank.' />" />
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Alternative BOM
                                       <asp:Label ID="labletxtAlternativeBOM" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtAlternativeBOM" runat="server" CssClass="textbox"
                                            MaxLength="3" Width="25%" />
                                        <asp:RequiredFieldValidator ID="reqtxtAlternativeBOM" runat="server" ControlToValidate="txtAlternativeBOM"
                                            ValidationGroup="save" ErrorMessage="Alternative BOM cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Alternative BOM cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">SAP BOM No
                                       <asp:Label ID="labletxtSAP_BOM_No" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtSAP_BOM_No" runat="server" CssClass="textbox"
                                            MaxLength="50" Width="25%" />
                                        <asp:RequiredFieldValidator ID="reqtxtSAP_BOM_No" runat="server" ControlToValidate="txtSAP_BOM_No"
                                            ValidationGroup="save" ErrorMessage="SAP BOM No cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='SAP BOM No cannot be blank.' />" />
                                    </td>
                                </tr>

                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Recipe Group
                                       <asp:Label ID="labletxtRecipe_Group" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtRecipe_Group" runat="server" CssClass="textbox"
                                            MaxLength="50" Width="25%" />
                                        <asp:RequiredFieldValidator ID="reqtxtRecipe_Group" runat="server" ControlToValidate="txtRecipe_Group"
                                            ValidationGroup="save" ErrorMessage="Recipe Group cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Recipe Group cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Prod Version No
                                       <asp:Label ID="labletxtProdVersionNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtProdVersionNo" runat="server" CssClass="textbox"
                                            MaxLength="50" Width="25%" />
                                        <asp:RequiredFieldValidator ID="reqtxtProdVersionNo" runat="server" ControlToValidate="txtProdVersionNo"
                                            ValidationGroup="save" ErrorMessage="Prod Version No cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Prod Version No cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Recipe Group Cntr
                                       <asp:Label ID="labletxtRecipeGroupCntr" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtRecipeGroupCntr" runat="server" CssClass="textbox"
                                            MaxLength="50" Width="25%" />
                                        <asp:RequiredFieldValidator ID="reqtxtRecipeGroupCntr" runat="server" ControlToValidate="txtRecipeGroupCntr"
                                            ValidationGroup="save" ErrorMessage="Recipe Group Cntr cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Recipe Group Cntr cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Remarks
                                        <asp:Label ID="lblddlRemarks" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlRemarks" runat="server"
                                            AppendDataBoundItems="true" AutoPostBack="true">
                                            <asp:ListItem Text="Select" Value="" />
                                            <asp:ListItem Text="Remarks 1" Value="Remarks1" />
                                            <asp:ListItem Text="Remarks 2" Value="Remarks2" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlRemarks" runat="server" ControlToValidate="ddlRemarks"
                                            ValidationGroup="save" ErrorMessage="Please select Remarks." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Remarks cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Remarks 2
                                       <asp:Label ID="labletxtRemarks" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="textarea" TextMode="MultiLine"
                                            TabIndex="8" Columns="100" Rows="3" />
                                        <asp:RequiredFieldValidator ID="reqtxtRemarks" runat="server" ControlToValidate="txtRemarks"
                                            ValidationGroup="save" ErrorMessage="Remarks cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Remarks cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="centerTD" colspan="2">
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button" ValidationGroup="save"
                                            OnClick="btnUpdate_Click" />
                                        &nbsp;
                                        <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="button"
                                            OnClick="BtnClear_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </asp:Panel>
        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
</asp:Content>

