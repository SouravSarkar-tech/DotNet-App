<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/MasterPage.master" AutoEventWireup="true" CodeFile="ResourceMaster.aspx.cs" Inherits="Transaction_Resource_ResourceMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <link href="../../stylesheet/main.css" rel="stylesheet" type="text/css" />
    <link href="../../stylesheet/autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="../../stylesheet/Paging.css" rel="stylesheet" type="text/css" />
    <script src="../../js/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.15.custom.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlSearch" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="4">
                            Resource Master
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left" style="width: 25%">
                            Request No
                        </td>
                        <td class="rigthTD" align="left" style="width: 25%">
                            <asp:TextBox ID="txtRequestNo" runat="server" CssClass="textbox" />
                        </td>
                        <td class="leftTD" align="left" style="width: 25%">
                            SAP Code
                        </td>
                        <td class="rigthTD" align="left" style="width: 25%">
                            <asp:TextBox ID="txtSAPCode" runat="server" CssClass="textbox" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left">
                            Module
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:DropDownList ID="ddlModuleSearch" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="All" Value="0" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModuleSearch"
                                ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Module.' />" />
                        </td>
                        <td class="leftTD" align="left">
                            Status
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Pending For My Approval" Value="P" />
                                <asp:ListItem Text="Rollbacked To Me" Value="R" />
                                <asp:ListItem Text="Created By Me" Value="C" />
                                <asp:ListItem Text="Incomplete Request" Value="I" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="4">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
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
                                BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                                OnPageIndexChanging="grdSearch_PageIndexChanging" GridLines="Both">
                                <RowStyle CssClass="light-gray" />
                                <HeaderStyle CssClass="gridHeader" />
                                <AlternatingRowStyle CssClass="gridRowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdoSelection" runat="server" GroupName="selection" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrimaryID" runat="server" Text='<%# Eval("Master_Header_Id") %>'></asp:Label>
                                            <asp:Label ID="lblModuleId" runat="server" Text='<%# Eval("Module_Id") %>'></asp:Label>
                                            <asp:Label ID="lblModuleName" runat="server" Text='<%# Eval("Module_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Request No." DataField="Request_No" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Resource" DataField="Resource" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Object_Name" DataField="Object_Name" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Object_Name" DataField="Object_Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Created On" DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Pending For" DataField="Pending_For" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" Visible="false" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" Visible="false">
                                        <ItemTemplate>
                                            <a href="#" onclick="return ShowRejectionNote('<%#Eval("Remarks") %>')" style="color: #1540c2">
                                                Rejection Note</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" HeaderText="Status">
                                        <ItemTemplate>
                                            <%#Eval("Status") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div id="divRejectionNoteContainer" style="display: none;" title="Rejection Note">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="background-color: White;">
                                    <tr>
                                        <td align="left">
                                            <div id="divRejectionNote">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <br />
                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="button" OnClientClick="return Validate();"
                                OnClick="btnView_Click" Visible="false" />
                            <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="button" OnClientClick="return Validate();"
                                OnClick="btnModify_Click" Visible="false" />
                            <input type="button" name="btnCreateNew" id="btnCreateNew" class="button" value="Create New"
                                style="width: 120px" onclick="return ShowCreateNewDialog();" />
                            <asp:Button ID="btnChangeRequest" runat="server" Text="Change Request" CssClass="button"
                                OnClientClick="return Validate();" Width="150px" Visible="false" OnClick="btnChangeRequest_Click" />
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
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divModulePopUp" style="display: none;" title="Customer Master">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="leftTD" width="30%">
                    Resource Type
                </td>
                <td class="rigthTD">
                    <asp:DropDownList ID="ddlModule" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select" Value="" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlModule"
                        ValidationGroup="next" ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic"
                        Text="<img src='../../images/Error.png' title='Select Module.' />" />
                </td>
            </tr>
            <tr>
                <td class="tdSpace" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="centerTD" colspan="2">
                    <asp:Button ID="btnNext" runat="server" ValidationGroup="next" Text="Next" CssClass="button"
                        OnClick="btnNext_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdateProgress ID="updateProgressMaster" runat="server" DisplayAfter="5">
        <ProgressTemplate>
            <div id="light" class="loading">
                <img src="../../images/ajax-loader-proccessing.gif" alt="Loading" height="30px" />
            </div>
            <div class="transparent_overlay">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="next" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblPk" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <script type="text/javascript" language="javascript">

        $(function () {

        });

        function ShowCreateNewDialog() {
            $("#divModulePopUp").dialog({
                height: 140,
                width: 400,
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
        }

        function ShowRejectionNote(obj) {

            $("#divRejectionNoteContainer").dialog({
                height: 140,
                width: 500,
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
                    $("#divRejectionNote").html('');
                    $("#divRejectionNote").html(obj);
                }
            });
        }
    </script>
</asp:Content>

