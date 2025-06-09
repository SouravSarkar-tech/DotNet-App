<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserMaster.aspx.cs" Inherits="Administration_UserMaster" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %> 

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
    <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlSearch" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="4">User Maintenance
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left" style="width: 25%">AD User ID/Name
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:TextBox ID="txtUserNameSearch" runat="server" CssClass="textbox" />
                        </td>
                        <td colspan="2" class="tdSpace"></td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace"></td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left" style="width: 25%">From Date
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" />
                            <act:CalendarExtender ID="CaltxtFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFromDate"
                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>

                        </td>
                        <td class="leftTD" align="left" style="width: 25%">To Date
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" />
                            <act:CalendarExtender ID="CaltxtToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtToDate"
                                ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                                Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                                ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace"></td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="4">
                            <asp:Button ID="btnSearch" runat="server" Text="Search By User" CssClass="button" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnSearchDate" runat="server" Text="Search By Date" CssClass="button" OnClick="btnSearchDate_Click" />
                           
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace"></td>
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
                                            <asp:Label ID="lblPrimaryID" runat="server" Text='<%# Eval("User_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Login Id" DataField="UserName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Full Name" DataField="FullName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Email" DataField="EmailId" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Role" DataField="Role" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <br />
                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="button" OnClientClick="return Validate();"
                                OnClick="btnView_Click" />
                            <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="button" OnClientClick="return Validate();"
                                OnClick="btnModify_Click" />
                            <asp:Button ID="btnCreateNew" runat="server" Text="Create New" CssClass="button"
                                Width="120px" OnClick="btnCreateNew_Click" />
                            <asp:Button ID="btnCreateADUser" runat="server" Text="Create AD User" CssClass="button"
                                Width="120px" OnClick="btnCreateADUser_Click" />
                            <asp:Button ID="btnCopy" runat="server" Text="Assign Dual Role"
                                CssClass="button" OnClientClick="return Validate();"
                                Width="120px" OnClick="btnCopy_Click" />
                            <%--8400000241 Start Add button for DeactiveUser fun--%>
                             <asp:Button ID="btnDeactiveUser" runat="server" Text="Deactive User"
                                CssClass="button" 
                                Width="120px" OnClick="btnDeactiveUser_Click" />
                             <%--8400000241 End Add button for DeactiveUser fun--%>
                             



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
            <asp:AsyncPostBackTrigger ControlID="btnCreateADUser" />
            <asp:AsyncPostBackTrigger ControlID="btnCreateNew" />
            <asp:AsyncPostBackTrigger ControlID="btnView" />
            <asp:AsyncPostBackTrigger ControlID="btnModify" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnBack" />
             
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlAddNew" runat="server" Visible="false">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">User Maintenance
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">Login Id<span class="mandatory">*</span>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox"
                                            MaxLength="20" Width="100px" />
                                        <%--onchange="return CheckUserName();"--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtUserName"
                                            ValidationGroup="save" ErrorMessage="Login Id cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='User Name cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Profile<span class="mandatory">*</span>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlProfile" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProfile"
                                            ValidationGroup="save" ErrorMessage="Profile cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Profile cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Full Name<span class="mandatory">*</span>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFullName" runat="server" CssClass="textbox" MaxLength="100" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFullName"
                                            ValidationGroup="save" ErrorMessage="Full Name cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Full Name cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Email<span class="mandatory">*</span>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="100" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail"
                                            ValidationGroup="save" ErrorMessage="Email cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Email cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="rev" ControlToValidate="txtEmail" runat="server"
                                            ErrorMessage="Please Enter a valid Email." Display="Dynamic" ValidationGroup="save"
                                            SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            Text="<img src='../images/Error.png' title='Please Enter a valid Email.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Department<span class="mandatory">*</span>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDepartment"
                                            ValidationGroup="save" ErrorMessage="Department cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Department cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Reporting To
                                        <%--<span class="mandatory">*</span>--%>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtReportingTOName" runat="server" CssClass="textbox" MaxLength="100" />
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtReportingTOName"
                                            ValidationGroup="save" ErrorMessage="Reporting To Name cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Reporting To Name cannot be blank.' />" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Email
                                        <%--<span class="mandatory">*</span>--%>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtReprotingToEmail" runat="server" CssClass="textbox" MaxLength="100" />
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtReprotingToEmail"
                                            ValidationGroup="save" ErrorMessage="Reporting To Email cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Reporting To Email cannot be blank.' />" />--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtReprotingToEmail"
                                            runat="server" ErrorMessage="Please Enter a valid Email." Display="Dynamic" ValidationGroup="save"
                                            SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            Text="<img src='../images/Error.png' title='Please Enter a valid Email.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Country<span class="mandatory">*</span>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCountry"
                                            ValidationGroup="save" ErrorMessage="Country cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Country cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr id="trPassword1" runat="server">
                                    <td class="leftTD">Password<span class="mandatory">*</span>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" MaxLength="50" TextMode="Password" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPassword"
                                            ValidationGroup="save" ErrorMessage="Password cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Password cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr id="trPassword2" runat="server">
                                    <td class="leftTD">Re-type Password<span class="mandatory">*</span>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtReTypePassword" runat="server" CssClass="textbox" MaxLength="50"
                                            TextMode="Password" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtReTypePassword"
                                            ValidationGroup="save" ErrorMessage="Re-type Password cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Re-type Password cannot be blank.' />" />
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtReTypePassword"
                                            ControlToCompare="txtPassword" ErrorMessage="The passwords you entered do not match. Please try again."
                                            Display="None" SetFocusOnError="true" ValidationGroup="save" CssClass="form_msg"
                                            Text="<img src='../images/Error.png' title='The passwords you entered do not match. Please try again.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2"></td>
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
    <asp:ValidationSummary ID="sm" runat="server" ShowMessageBox="true" ShowSummary="false"
        ValidationGroup="save" />
    <script type="text/javascript">
        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {
        });
        $(function () {

        });


        function CheckUserName() {
            var userName = $('#<%=txtUserName.ClientID %>').val();
            $('#<%= txtUserName.ClientID %>').attr('class', 'textboxbussy')
            $('#<%= btnSave.ClientID %>').attr('disabled', 'disabled');

            $.ajax({
                type: "POST",
                url: "../Service.svc/IsUserNameExists",
                data: '{"userName":"' + userName + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        alert('User Name already exists.');
                        $('#<%=txtUserName.ClientID %>').val('');
                    }
                    $('#<%=txtUserName.ClientID %>').attr('class', 'textbox');
                    $('#<%=btnSave.ClientID %>').removeAttr('disabled');

                },
                error: function (a) {
                    alert("Error Occurred");
                    $('#<%=txtUserName.ClientID %>').attr('class', 'textbox');
                }
            });
        }


    </script>
</asp:Content>
