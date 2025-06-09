<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAccounting1.ascx.cs" Inherits="UserControl_usAccounting1" %>
 <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlAccounting1" runat="server" GroupingText="Accounting 1">
                                <table>
                                    <tr>
                                        <td>Valuation Category
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlValuationCategory" runat="server" CssClass="dropDown" ></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlValuationCategory" runat="server" CssClass="RequiredField" ControlToValidate="ddlValuationCategory"
                                                ErrorMessage="Please enter the Valuation Category" Text="*" ValidationGroup="submitGroup"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Price Control Indicator</td>
                                        <td>
                                            <asp:DropDownList ID="ddlPriceControlIndicator" runat="server" CssClass="dropDown" ></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlPriceControlIndicator" runat="server" CssClass="RequiredField" ControlToValidate="ddlPriceControlIndicator"
                                                ErrorMessage="Please enter the Price Control Indicator" Text="*" ValidationGroup="submitGroup"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Moving average price (BTCI)</td>
                                        <td>
                                            <asp:TextBox ID="txtMovingAvgPrice" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqtxtMovingAvgPrice" runat="server" CssClass="RequiredField" ControlToValidate="txtMovingAvgPrice"
                                                ErrorMessage="Please enter the Moving average price" Text="*" ValidationGroup="submitGroup"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Standard price</td>
                                        <td>
                                            <asp:TextBox ID="txtStandardPrice" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqtxtStandardPrice" runat="server" CssClass="RequiredField" ControlToValidate="txtStandardPrice"
                                                ErrorMessage="Please enter the Standard price" Text="*" ValidationGroup="submitGroup"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Price unit (BTCI)</td>
                                        <td>
                                            <asp:TextBox ID="txtPriceUnit" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqtxtPriceUnit" runat="server" CssClass="RequiredField" ControlToValidate="txtPriceUnit"
                                                ErrorMessage="Please enter the Price unit" Text="*" ValidationGroup="submitGroup"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Valuation Class</td>
                                        <td>
                                            <asp:DropDownList ID="ddlValuationClass" runat="server" CssClass="dropDown"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlValuationClass" runat="server" CssClass="RequiredField" ControlToValidate="ddlValuationClass"
                                                ErrorMessage="Please enter the Valuation Class" Text="*" ValidationGroup="submitGroup"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td style="text-align: center">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submitGroup" />


                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                </table>

                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>