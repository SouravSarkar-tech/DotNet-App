<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true"
    CodeFile="UPReportingManager.aspx.cs" Inherits="Administration_UPReportingManager" %>
    <%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upMsg" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg1" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlAddNew" runat="server" Visible="false">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Add/Update Reporting Manager
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">
                                        Login Id<span class="mandatory">*</span>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtUserName" runat="server"  CssClass="textbox"
                                            MaxLength="20" Width="100px" onkeypress="return blockSpecialChar(event)" />
                                            <%--onchange="return CheckUserName();"--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtUserName"
                                            ValidationGroup="save" ErrorMessage="Login Id cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='User Name cannot be blank.' />" />
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" 
                                            OnClick="btnSearch_Click" Text="Search" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Profile<%--<span class="mandatory">*</span>--%></td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlProfile" runat="server" AppendDataBoundItems="true" Enabled="false">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProfile"
                                            ValidationGroup="save" ErrorMessage="Profile cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Profile cannot be blank.' />" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Full Name<%--<span class="mandatory">*</span>--%></td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtFullName" runat="server" CssClass="textbox" MaxLength="100" Enabled="false"/>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFullName"
                                            ValidationGroup="save" ErrorMessage="Full Name cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Full Name cannot be blank.' />" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Email<%--<span class="mandatory">*</span>--%></td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="100" Enabled="false"/>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail"
                                            ValidationGroup="save" ErrorMessage="Email cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Email cannot be blank.' />" />--%>
                                        <asp:RegularExpressionValidator ID="rev" ControlToValidate="txtEmail" runat="server"
                                            ErrorMessage="Please Enter a valid Email." Display="Dynamic" ValidationGroup="save"
                                            SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            Text="<img src='../images/Error.png' title='Please Enter a valid Email.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Department<%--<span class="mandatory">*</span>--%></td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" Enabled="false">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDepartment"
                                            ValidationGroup="save" ErrorMessage="Department cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Department cannot be blank.' />" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Reporting To
                                        <span class="mandatory">*</span>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtReportingTOName" runat="server" CssClass="textbox" MaxLength="100" onkeypress="return blockSpecialChar(event)" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtReportingTOName"
                                            ValidationGroup="save" ErrorMessage="Reporting To Name cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Reporting To Name cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td class="leftTD">
                                        Email
                                        <span class="mandatory">*</span>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtReprotingToEmail" runat="server" CssClass="textbox" MaxLength="100" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtReprotingToEmail"
                                            ValidationGroup="save" ErrorMessage="Reporting To Email cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Reporting To Email cannot be blank.' />" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtReprotingToEmail"
                                            runat="server" ErrorMessage="Please Enter a valid Email." Display="Dynamic" ValidationGroup="save"
                                            SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            Text="<img src='../images/Error.png' title='Please Enter a valid Email.' />" />
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD">
                                        Country<%--<span class="mandatory">*</span>--%></td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" Enabled="false">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCountry"
                                            ValidationGroup="save" ErrorMessage="Country cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Country cannot be blank.' />" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <%--<tr id="trPassword1" runat="server">
                                    <td class="leftTD">
                                        Password<span class="mandatory">*</span>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" MaxLength="50" TextMode="Password" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPassword"
                                            ValidationGroup="save" ErrorMessage="Password cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Password cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr id="trPassword2" runat="server">
                                    <td class="leftTD">
                                        Re-type Password<span class="mandatory">*</span>
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
                                </tr>--%>
                                <tr>
                                    <td class="leftTD">
                                        Module<span class="mandatory">*</span>
                                    </td>
                                    <td class="rigthTD">
                                        <cc1:DropDownCheckBoxes ID="ddlModule" runat="server" AddJQueryReference="false"
                                            TabIndex="3" UseButtons="false" UseSelectAllNode="true" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="350" DropDownBoxBoxHeight="80" />
                                            <Texts SelectBoxCaption="--Select--" />
                                        </cc1:DropDownCheckBoxes>
                                        <cc1:ExtendedRequiredFieldValidator ID="reqddlModule" runat="server" ControlToValidate="ddlModule"
                                            ValidationGroup="save" ErrorMessage="Module cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Module cannot be blank.' />"></cc1:ExtendedRequiredFieldValidator>
                                    </td>
                                    
                                </tr>
                                <tr>
                                  <td class="rigthTD" colspan="2">
                                        <asp:Label ID="lableRddlModule" runat="server"></asp:Label>
                                        <%--<asp:LinkButton ID="lnkRefreshddlModule" runat="server" Text="[ Refresh ]"
                                    Font-Bold="false" OnClick="lnkRefreshddlModule_Click"></asp:LinkButton>--%>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="centerTD" colspan="2">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="save"
                                            OnClick="btnSave_Click" />
                                        <%--&nbsp;
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="button" OnClick="btnBack_Click" />--%>
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
           <%-- <asp:AsyncPostBackTrigger ControlID="btnBack" />--%>
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


        function blockSpecialChar(e) {
            var k = e.keyCode == 0 ? e.charCode : e.keyCode; 
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57));
        }

</script>

</asp:Content>
