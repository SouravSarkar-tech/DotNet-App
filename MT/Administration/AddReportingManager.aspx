<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true" CodeFile="AddReportingManager.aspx.cs" Inherits="Administration_AddReportingManager" %>

<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
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
                        <td class="trHeading" align="center" colspan="2">Approve Request from Backend
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Enter Requestor Name.
                                        <asp:Label ID="lblRequester" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtRequestor" runat="server" CssClass="textboxAutocomplete"
                                            onfocus="return RequestorOnFocus();"
                                            onblur="return RequestorTextChangeEvent();" EnableViewState="false" AutoComplete="off"></asp:TextBox>

                                    </td>
                                    <asp:RequiredFieldValidator ID="reqtxtRequestor" runat="server" ControlToValidate="txtRequestor"
                                        ValidationGroup="save" ErrorMessage="Requestor Name cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../images/Error.png' title='Requestor Name cannot be blank.' />" />
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Enter Approver Name
                                        <asp:Label ID="lblApprover" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtApprover" runat="server" CssClass="textboxAutocomplete" 
                                            onfocus="return ApproverOnFocus();"
                                            onblur="return ApproverTextChangeEvent();" EnableViewState="false" AutoComplete="off"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="reqtxtApprover" runat="server" ControlToValidate="txtApprover"
                                            ValidationGroup="save" ErrorMessage="Approver Name cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Approver Name cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>

                                <tr>
                                    <td class="centerTD" colspan="2">
                                        <asp:Button ID="btnApprove" runat="server" Text="Add Approver" CssClass="button" ValidationGroup="save" OnClick="btnApprove_Click"/>
                                        &nbsp;
                                        <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="button" OnClick="BtnClear_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </asp:Panel>
        </ContentTemplate>

    </asp:UpdatePanel>


    <script type="text/javascript">

        function txtApproverOnFocus() {

            textboxId = $('#<%= txtApprover.ClientID%>').attr('ID');
            //textboxRealId = "txtApprover";
            CheckLookupUser();
        }

        function txtApproverTextChangeEvent() {
            //CheckLookupUser($('#<%= txtApprover.ClientID%>').attr('ID'), "txtApprover", $('#<%= btnApprove.ClientID%>').attr('ID'));
            __doPostBack($('#<%= txtApprover.ClientID%>').attr('ID'), 'TextChanged');
        }


        var textboxId = "";
        var textboxRealId = "";


        function ApproverOnFocus() {

            <%--CountryId = $('#<%= ddlBankCountrykey.ClientID%>').val();--%>

            textboxId = $('#<%= txtApprover.ClientID%>').attr('ID');
            //textboxRealId = "";
            AutoCompleteUserName();
        }

        function ApproverTextChangeEvent() {
            __doPostBack($('#<%= txtApprover.ClientID%>').attr('ID'), 'TextChanged');
        }


        function txtRequestorOnFocus() {

            textboxId = $('#<%= txtRequestor.ClientID%>').attr('ID');
            //textboxRealId = "txtRequestor";
            CheckLookupUser();
        }

        function txtRequestorTextChangeEvent() {
            //CheckLookupUser($('#<%= txtRequestor.ClientID%>').attr('ID'), "txtRequestor", $('#<%= btnApprove.ClientID%>').attr('ID'));
            __doPostBack($('#<%= txtRequestor.ClientID%>').attr('ID'), 'TextChanged');
        }


        var textboxId = "";
        var textboxRealId = "";


        function RequestorOnFocus() {

            <%--CountryId = $('#<%= ddlBankCountrykey.ClientID%>').val();--%>

            textboxId = $('#<%= txtRequestor.ClientID%>').attr('ID');
            //textboxRealId = "";
            AutoCompleteUserName();
        }

        function RequestorTextChangeEvent() {
            __doPostBack($('#<%= txtRequestor.ClientID%>').attr('ID'), 'TextChanged');
        }

    </script>

</asp:Content>

