<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAccounting2.ascx.cs" Inherits="ucUserControl_Accounting2" %>
<script type="text/javascript">
    function checkLifoFifo(sender, args) {
        
     
            args.IsValid = false;

    }
</script>
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
                                <td>LIFO/FIFO-Relevant</td>
                                <td>
                                    <asp:CheckBox ID="chkLifoFifo" runat="server" />
                                    <asp:CustomValidator ID="reqchkLifoFifo" runat="server" CssClass="RequiredField"
                                        ErrorMessage="Please enter the Valuation Category" ClientValidationFunction="checkLifoFifo" Text="*" ValidationGroup="submitGroup"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Pool number for LIFO valuation</td>
                                <td>
                                    <asp:DropDownList ID="ddlPoolNumberLifo" runat="server" CssClass="dropDown" />
                                    <asp:RequiredFieldValidator ID="reqddlPoolNumberLifo" runat="server" CssClass="RequiredField" ControlToValidate="ddlPoolNumberLifo"
                                        ErrorMessage="Please enter the Pool number for LIFO valuation" Text="*" ValidationGroup="submitGroup"></asp:RequiredFieldValidator>
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
