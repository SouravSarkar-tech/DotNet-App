<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/BankMaster/BankMasterPage.master"
    AutoEventWireup="true" CodeFile="BankData.aspx.cs" Inherits="Transaction_BankMaster_BankData" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Src="~/Transaction/UserControl/ucBankMaster.ascx" TagPrefix="uc" TagName="ucBankMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery.MultiFile.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlData" runat="server" Width="100%">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <%--<tr>
                <td class="trHeading" align="center" colspan="2">
                    Bank Data
                </td>
            </tr>--%>
            <tr>
                <td class="tdSpace">
                </td>
            </tr>
            <tr>
                <td class="centerTD">
                    <asp:UpdatePanel ID="updBankNew" runat="server">
                        <ContentTemplate>
                            <uc:ucBankMaster ID="ucBankMaster1" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="4">
                </td>
            </tr>
            <tr id="trButton" runat="server">
                <td class="centerTD">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" ValidationGroup="BankMaster"
                        OnClick="btnSubmit_Click" Visible="false" />
                    <asp:Button ID="btnSAPUpload" runat="server" Text="Upload SAP" CssClass="button"
                        OnClientClick="return ShowSAPUploadPopup();" Visible="false" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnRejectTo" runat="server" Text="Reject" CssClass="button" OnClientClick="return ShowRollbackPopup();" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div id="divRejectTo" style="display: none;" title="Reject Request">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 10px;">
            <tr>
                <td class="leftTD" style="width: 25%">
                    Reject To
                </td>
                <td class="rigthTD">
                    <asp:DropDownList ID="ddlRejectTo" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select" Value="" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvq" SetFocusOnError="true" Display="Dynamic" Text="<img src='../images/Error.png' title='Select Department.'"
                        ControlToValidate="ddlRejectTo" InitialValue="" runat="server" ForeColor="Red"
                        ValidationGroup="reject" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="tdSpace">
                </td>
            </tr>
            <tr>
                <td class="leftTD" valign="top">
                    Remark
                </td>
                <td class="rigthTD">
                    <asp:TextBox ID="txtRejectNote" runat="server" CssClass="textarea" TextMode="MultiLine"
                        Height="40px" Width="350px" Style="text-transform: none;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" SetFocusOnError="true"
                        Display="Dynamic" Text="<img src='../images/Error.png' title='Enter Remark.'"
                        ControlToValidate="txtRejectNote" runat="server" ForeColor="Red" ValidationGroup="reject" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="tdSpace">
                </td>
            </tr>
            <tr>
                <td class="leftTD">
                    &nbsp;
                </td>
                <td class="rigthTD">
                    <asp:Button ID="btnRollback" runat="server" Text="Reject" CssClass="button" OnClick="btnRollback_Click"
                        ValidationGroup="reject" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divSAPUpload" style="display: none;" title="Upload To SAP">
        <iframe id="Iframe1" runat="server" width="100%" height="100%" src="SAPIntegration.aspx">
        </iframe>
    </div>
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblBankMasterlId" runat="server" Visible="false" />
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="save" ShowMessageBox="true"
        ShowSummary="false" />
    <script type="text/javascript">
        function ShowRollbackPopup() {
            $("#divRejectTo").dialog({
                height: 210,
                width: 550,
                modal: true,
                closeOnEscape: true,
                draggable: true,
                resizable: false,
                position: 'center',
                dialogClass: 'alert',
                //show: { effect: 'drop', duration: 500 },
                //hide: { effect: 'explode', duration: 100 },
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                    $(this).show('clip');
                }
            });
            return false;
        }


        function ShowSAPUploadPopup() {
            $("#divSAPUpload").dialog({
                height: 500,
                width: 850,
                modal: true,
                closeOnEscape: true,
                draggable: true,
                resizable: false,
                position: 'center',
                dialogClass: 'alert',
                //show: { effect: 'drop', duration: 500 },
                //hide: { effect: 'explode', duration: 100 },
                open: function (type, data) {
                    $(this).parent().appendTo("form");
                    $(this).show('clip');
                }
            });
            return false;
        }
    </script>
</asp:Content>
