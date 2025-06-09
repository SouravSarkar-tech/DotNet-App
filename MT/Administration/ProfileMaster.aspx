<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="ProfileMaster.aspx.cs" Inherits="Administration_ProfileMaster" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
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
    <asp:UpdatePanel ID="upSearch" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlSearch" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Profile Creation
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left" style="width: 25%">
                            Profile Name
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:TextBox ID="txtUserProfileSearch" runat="server" CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="2">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            <script type="text/javascript">
                                function CheckOtherIsCheckedByGVID(spanChk) {

                                    var CurrentRdbID = spanChk.id;
                                    var Chk = spanChk;
                                    Parent = document.getElementById("<%=grdSearch.ClientID%>");
                                    var items = Parent.getElementsByTagName('input');
                                    for (i = 0; i < items.length; i++) {
                                        if (items[i].id != CurrentRdbID && items[i].type == "radio") {
                                            if (items[i].checked) {
                                                items[i].checked = false;
                                            }
                                        }
                                    }
                                }
                            </script>
                            <asp:GridView ID="grdSearch" runat="server" AutoGenerateColumns="false" Width="100%"
                                BorderColor="#9D9D9D" EmptyDataText="No Record Found" AllowPaging="true" PageSize="10"
                                OnPageIndexChanging="grdSearch_PageIndexChanging" GridLines="Both">
                                <RowStyle CssClass="light-gray" />
                                <HeaderStyle CssClass="gridHeader" />
                                <AlternatingRowStyle CssClass="gridRowStyoe" />
                                <PagerStyle CssClass="PagerStyle" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdoSelection" runat="server" GroupName="selection" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrimaryID" runat="server" Text='<%# Eval("Profile_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Profile Name" DataField="Profile_Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <br />
                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="button" OnClientClick="return Validate();"
                                OnClick="btnView_Click" />
                            <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="button" OnClientClick="return Validate();"
                                OnClick="btnModify_Click" />
                            <asp:Button ID="btnCreateNew" runat="server" Text="Create New" CssClass="button"
                                Width="120px" OnClick="btnCreateNew_Click" />
                            <script type="text/javascript">
                                function Validate() {
                                    var gv = document.getElementById("<%=grdSearch.ClientID%>");
                                    var rbs = gv.getElementsByTagName("input");
                                    var flag = 0;
                                    for (var i = 0; i < rbs.length; i++) {

                                        if (rbs[i].type == "radio") {
                                            if (rbs[i].checked) {
                                                flag = 1;
                                                break;
                                            }
                                        }
                                    }
                                    if (flag == 0) {
                                        alert("Kindly Select A Record");
                                        return false;
                                    }
                                }
                                              
                            </script>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCreateNew" />
            <asp:AsyncPostBackTrigger ControlID="btnView" />
            <asp:AsyncPostBackTrigger ControlID="btnModify" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnBack" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upAddNew" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlAddNew" runat="server" Visible="false">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Profile Creation
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" style="width: 25%">
                            Profile Name <span class="mandatory">*</span>
                        </td>
                        <td class="rigthTD">
                            <asp:TextBox ID="txtUserProfileName" runat="server" CssClass="textbox" MaxLength="50"
                                onchange="return CheckProfileName();" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtUserProfileName"
                                ValidationGroup="save" ErrorMessage="User Profile cannot be blank." SetFocusOnError="true"
                                Display="Dynamic" Text="<img src='../images/Error.png' title='User Profile cannot be blank.' />" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="grdMenus" runat="server" AutoGenerateColumns="false" Width="100%"
                                BorderColor="#9D9D9D" EmptyDataText="No Data Found" OnRowDataBound="grdMenus_OnRowDataBound">
                                <RowStyle CssClass="light-gray" />
                                <HeaderStyle BackColor="#EDF5FF" />
                                <AlternatingRowStyle CssClass="wht-clr-1" />
                                <PagerStyle CssClass="PagerStyle" />
                                <Columns>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdParentId" runat="server" Value='<%#Eval("Parent_Id") %>' />
                                            <asp:HiddenField ID="hdUrl" runat="server" Value='<%#Eval("Page_URL") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        HeaderText="Menu">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdMenuId" runat="server" Value='<%#Eval("Menu_Id") %>' />
                                            <asp:Label ID="lblMenuName" runat="server" Text='<%#Eval("Menu_Name") %>' Visible="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-CssClass="AlignCenter">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkView" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Add" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-CssClass="AlignCenter">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAdd" runat="server" onclick="return AddCheckBoxSelection(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Modify" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-CssClass="AlignCenter">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkModify" runat="server" onclick="return ModifyCheckBoxSelection(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-CssClass="AlignCenter">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDelete" runat="server" onclick="return DeleteCheckBoxSelection(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdSpace" colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="2">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="save"
                                OnClick="btnSave_Click" />
                            &nbsp;
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="button" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCreateNew" />
            <asp:AsyncPostBackTrigger ControlID="btnView" />
            <asp:AsyncPostBackTrigger ControlID="btnModify" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="btnBack" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upLBL" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblPk" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
        </ContentTemplate>
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
    <script type="text/javascript">
        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {

        });
        $(function () {

        });

        function CheckProfileName() {
            var profileName = $('#<%=txtUserProfileName.ClientID %>').val();
            $('#<%= txtUserProfileName.ClientID %>').attr('class', 'textboxbussy')
            $('#<%= btnSave.ClientID %>').attr('disabled', 'disabled');
            $.ajax({
                type: "POST",
                url: "../Service.svc/IsProfileNameExists",
                data: '{"profileName":"' + profileName + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        alert('Profile Name already exists.');
                        $('#<%=txtUserProfileName.ClientID %>').val('');
                    }
                    $('#<%=txtUserProfileName.ClientID %>').attr('class', 'textbox');
                    $('#<%=btnSave.ClientID %>').removeAttr('disabled');

                },
                error: function (a) {
                    alert("Error Occurred");
                    $('#<%=txtUserProfileName.ClientID %>').attr('class', 'textbox');
                }
            });
        }

        function AddCheckBoxSelection(obj) {
            var addChkId = $(obj).attr("ID");
            if ($('#' + addChkId).is(':checked')) {
                $('#' + addChkId).parent().prev().find("input:checkbox").attr('checked', 'checked');
                $('#' + addChkId).parent().prev().find("input:checkbox").attr('disabled', 'disabled');
            }
            else {

                if (!($('#' + addChkId).parent().next().find("input:checkbox").is(':checked')) && !($('#' + addChkId).parent().next().next().find("input:checkbox").is(':checked'))) {
                    $('#' + addChkId).parent().prev().find("input:checkbox").removeAttr('disabled');
                }
            }
        }

        function ModifyCheckBoxSelection(obj) {
            var addChkId = $(obj).attr("ID");
            if ($('#' + addChkId).is(':checked')) {
                $('#' + addChkId).parent().prev().prev().find("input:checkbox").attr('checked', 'checked');
                $('#' + addChkId).parent().prev().prev().find("input:checkbox").attr('disabled', 'disabled');
            }
            else {

                if (!($('#' + addChkId).parent().prev().find("input:checkbox").is(':checked')) && !($('#' + addChkId).parent().next().find("input:checkbox").is(':checked'))) {
                    $('#' + addChkId).parent().prev().prev().find("input:checkbox").removeAttr('disabled');
                }
            }
        }
        function DeleteCheckBoxSelection(obj) {
            var addChkId = $(obj).attr("ID");
            if ($('#' + addChkId).is(':checked')) {
                $('#' + addChkId).parent().prev().prev().prev().find("input:checkbox").attr('checked', 'checked');
                $('#' + addChkId).parent().prev().prev().prev().find("input:checkbox").attr('disabled', 'disabled');
            }
            else {

                if (!($('#' + addChkId).parent().prev().prev().find("input:checkbox").is(':checked')) && !($('#' + addChkId).parent().prev().find("input:checkbox").is(':checked'))) {
                    $('#' + addChkId).parent().prev().prev().prev().find("input:checkbox").removeAttr('disabled');
                }
            }
        }
    </script>
</asp:Content>
