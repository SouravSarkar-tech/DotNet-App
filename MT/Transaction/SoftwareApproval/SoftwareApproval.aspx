<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SoftwareApproval/SoftwareApprovalMasterPage.master"
    AutoEventWireup="true" CodeFile="SoftwareApproval.aspx.cs" Inherits="Transaction_SoftwareApproval_SoftwareApproval" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        .PlaceHolder
        {
            color: #D3D3D3;
        }
    </style>
    <script type="text/javascript">
        function showModalPopupViaClient() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }

        function showModalPopupViaClient() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior1');
            modalPopupBehavior.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlAddNew" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="2">
                    Manufacturing & Quality Software Approval
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                User Name
                                <asp:Label ID="labletxtUserName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox" MaxLength="18" TabIndex="1"
                                    Enabled="false" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtUserName" runat="server" ControlToValidate="txtUserName"
                                    ValidationGroup="save" ErrorMessage="User Name cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='User Name cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Requestor's Dept
                                <asp:Label ID="labletxtDept" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtDept" runat="server" CssClass="textbox" MaxLength="18" TabIndex="2"
                                    Enabled="false" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtDept" runat="server" ControlToValidate="txtDept"
                                    ValidationGroup="save" ErrorMessage="Department cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Department cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Locations for Software Installation
                                <asp:Label ID="lableddlInstallLocation" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <cc1:DropDownCheckBoxes ID="ddlInstallLocation" runat="server" AddJQueryReference="false"
                                    TabIndex="3" UseButtons="false" UseSelectAllNode="true" OnSelectedIndexChanged="ddlInstallLocation_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="350" DropDownBoxBoxHeight="80" />
                                    <Texts SelectBoxCaption="--Select--" />
                                </cc1:DropDownCheckBoxes>
                                <cc1:ExtendedRequiredFieldValidator ID="reqddlInstallLocation" runat="server" ControlToValidate="ddlInstallLocation"
                                    ValidationGroup="save" ErrorMessage="Location cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Location cannot be blank.' />"></cc1:ExtendedRequiredFieldValidator>
                            </td>
                            <td class="leftTD" colspan="2">
                                <asp:Label ID="lableRddlInstallLocation" runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkRefreshddlInstallLocation" runat="server" Text="[ Refresh ]"
                                    Font-Bold="false" OnClick="lnkRefreshddlInstallLocation_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr runat="server" id="trSpecifyLoc">
                            <td class="leftTD" style="width: 20%">
                                Specify Location(if Others)
                                <asp:Label ID="labeltxtOtherLoc" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtOtherLoc" runat="server" CssClass="textbox" MaxLength="18" TabIndex="2"
                                    Width="180" Enabled="false" />
                                <asp:RequiredFieldValidator ID="reqtxtOtherLoc" runat="server" ControlToValidate="txtOtherLoc"
                                    Enabled="false" ValidationGroup="save" ErrorMessage="Specify Location in case of others."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Specify Location in case of others.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Departments for Software Installation
                                <asp:Label ID="labelddlInstallDept" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <cc1:DropDownCheckBoxes ID="ddlInstallDept" runat="server" AddJQueryReference="false"
                                    TabIndex="4" UseButtons="false" UseSelectAllNode="true" OnSelectedIndexChanged="ddlInstallDept_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="350" DropDownBoxBoxHeight="80" />
                                    <Texts SelectBoxCaption="--Select--" />
                                </cc1:DropDownCheckBoxes>
                                <cc1:ExtendedRequiredFieldValidator ID="reqddlInstallDept" runat="server" ControlToValidate="ddlInstallDept"
                                    ValidationGroup="save" ErrorMessage="Department cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Department cannot be blank.' />"></cc1:ExtendedRequiredFieldValidator>
                            </td>
                            <td class="leftTD" colspan="2">
                                <asp:Label ID="labelRddlInstallDept" runat="server"></asp:Label>
                                <asp:LinkButton ID="lnkddlInstallDept" runat="server" Text="[ Refresh ]" Font-Bold="false"
                                    OnClick="lnkddlInstallDept_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr runat="server" id="trSpecifyDept">
                            <td class="leftTD" style="width: 20%">
                                Specify Department(if Others)
                                <asp:Label ID="labeltxtOtherDept" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtOtherDept" runat="server" CssClass="textbox" MaxLength="18" TabIndex="2"
                                    Width="180" Enabled="false" />
                                <asp:RequiredFieldValidator ID="reqtxtOtherDept" runat="server" ControlToValidate="txtOtherDept"
                                    Enabled="false" ValidationGroup="save" ErrorMessage="Specify Department in case of others."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Specify Department in case of others.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Software Name
                                <asp:Label ID="labletxtSWName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtSWName" runat="server" CssClass="textbox" TabIndex="5" Width="180"
                                    PlaceHolder="Enter Software Name" />
                                <asp:RequiredFieldValidator ID="reqtxtSWName" runat="server" ControlToValidate="txtSWName"
                                    ValidationGroup="save" ErrorMessage="Software Name cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Software Name cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Manufacturer
                                <asp:Label ID="labletxtMnfr" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtMnfr" runat="server" CssClass="textbox" TabIndex="6" Width="180"
                                    PlaceHolder="Enter Software Manufacturer Name" />
                                <asp:RequiredFieldValidator ID="reqtxtMnfr" runat="server" ControlToValidate="txtMnfr"
                                    ValidationGroup="save" ErrorMessage="Manufacturer cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Manufacturer cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Manufacturer Website
                                <asp:Label ID="labletxtMnfrSite" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtMnfrSite" runat="server" CssClass="textbox" TabIndex="7" Width="180"
                                    PlaceHolder="Enter Manufacturer Website" />
                                <asp:RequiredFieldValidator ID="reqtxtMnfrSite" runat="server" ControlToValidate="txtMnfrSite"
                                    ValidationGroup="save" ErrorMessage="Manufacturer website cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Manufacturer website cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Manufacturer Contact Person Name
                                <asp:Label ID="labeltxtMnfrPersonName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtMnfrPersonName" runat="server" CssClass="textbox" TabIndex="8"
                                    Width="180" PlaceHolder="Enter Manufacturer Contact Person Name" />
                                <asp:RequiredFieldValidator ID="reqtxtMnfrPersonName" runat="server" ControlToValidate="txtMnfrPersonName"
                                    ValidationGroup="save" ErrorMessage="Manufacturer contact person name cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Manufacturer contact person name cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Manufacturer Email Address
                                <asp:Label ID="labeltxtEmailAdd" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtEmailAdd" runat="server" CssClass="textbox" TabIndex="9" Width="180"
                                    PlaceHolder="Enter Manufacturer Email ID" />
                                <asp:RequiredFieldValidator ID="reqtxtEmailAdd" runat="server" ControlToValidate="txtEmailAdd"
                                    ValidationGroup="save" ErrorMessage="Manufacturer Email Address cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Manufacturer Email Address cannot be blank.' />" />
                                <asp:RegularExpressionValidator ID="regtxtEmailAdd" runat="server" ControlToValidate="txtEmailAdd"
                                    ValidationExpression="^([\w-\.]+)[\w]{1}@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                    ValidationGroup="save" ErrorMessage="Invalid E-Mail Address" SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid E-Mail Address.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Manufacturer Contact Number
                                <asp:Label ID="labeltxtPhneNo" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtPhneNo" runat="server" CssClass="textbox" TabIndex="10" Width="180"
                                    PlaceHolder="Enter Manufacturer Contact Number" />
                                <asp:RequiredFieldValidator ID="reqtxtPhneNo" runat="server" ControlToValidate="txtPhneNo"
                                    ValidationGroup="save" ErrorMessage="Manufacturer contact number cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Manufacturer contact number cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Software Cost (approx) in Rs
                                <asp:Label ID="labletxtSWCost" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtSWCost" runat="server" CssClass="textbox" TabIndex="11" Width="180"
                                    onkeypress="return IsNumber();" PlaceHolder="Enter Software Amount" AutoPostBack="true"
                                    OnTextChanged="txtSWCost_TextChanged" />
                                <asp:RequiredFieldValidator ID="reqtxtSWCost" runat="server" ControlToValidate="txtSWCost"
                                    ValidationGroup="save" ErrorMessage="Software Cost cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Software Cost cannot be blank.' />" />
                            </td>
                            <td colspan="2" class="tdSpace">
                            </td>
                            <%--<td class="leftTD" style="width: 20%">
                                        What software would do?
                                        <asp:Label ID="labletxtSWUse" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtSWUse" runat="server" CssClass="textbox" TabIndex="7" Width="180" />
                                        <asp:RequiredFieldValidator ID="reqtxtSWUse" runat="server" ControlToValidate="txtSWUse"
                                            ValidationGroup="save" ErrorMessage="What software would do? cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='What software would do? cannot be blank.' />" />
                                    </td>--%>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Business justification
                                <asp:Label ID="labletxtBJustification" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" colspan="3">
                                <asp:TextBox ID="txtBJustification" runat="server" CssClass="textarea" TextMode="MultiLine"
                                    TabIndex="12" Columns="100" Rows="3" Width="268px" PlaceHolder="Mention key deliverables & benefits of this software in detail" />
                                <asp:RequiredFieldValidator ID="reqtxtBJustification" runat="server" ControlToValidate="txtBJustification"
                                    ValidationGroup="save" ErrorMessage="Business justification cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Business justification cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Software functionality
                                <asp:Label ID="labletxtSWUse" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" colspan="3">
                                <%--<asp:TextBox ID="txtSWUse" runat="server" CssClass="textbox" TabIndex="8" Width="180" />--%>
                                <asp:TextBox ID="txtSWUse" runat="server" CssClass="textarea" TextMode="MultiLine"
                                    TabIndex="13" Columns="100" Rows="3" Width="268px" PlaceHolder="Mention the exact function or operation of the software" />
                                <asp:RequiredFieldValidator ID="reqtxtSWUse" runat="server" ControlToValidate="txtSWUse"
                                    ValidationGroup="save" ErrorMessage="Software functionality cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Software functionality cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdHeading" align="left" colspan="4">
                                IT Requirements
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Server Required
                                <asp:Label ID="labletxtInstalledServer" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlInstalledServer" runat="server" TabIndex="14" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlInstalledServer_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="" />
                                    <asp:ListItem Text="Yes" Value="Yes" />
                                    <asp:ListItem Text="No" Value="No" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlInstalledServer" runat="server" ControlToValidate="ddlInstalledServer"
                                    ValidationGroup="save" ErrorMessage="Please specify should it be installed on a Server?"
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please specify should it be installed on a Server?' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Quantity
                                <asp:Label ID="labletxtServerQty" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtServerQty" runat="server" CssClass="textbox" TabIndex="15" onkeypress="return IsNumber();"
                                    Width="180" Enabled="false" PlaceHolder="Enter Server Quantity" />
                                <asp:RequiredFieldValidator ID="reqtxtServerQty" runat="server" ControlToValidate="txtServerQty"
                                    Enabled="false" ValidationGroup="save" ErrorMessage="Please specify number of servers required."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please specify number of servers required.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                PC/Laptop Required
                                <asp:Label ID="labelddlPCLapReq" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlPCLapReq" runat="server" TabIndex="16" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPCLapReq_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="" />
                                    <asp:ListItem Text="Yes" Value="Yes" />
                                    <asp:ListItem Text="No" Value="No" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlPCLapReq" runat="server" ControlToValidate="ddlPCLapReq"
                                    ValidationGroup="save" ErrorMessage="Please specify if PC or Laptop is required."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please specify if PC or Laptop is required.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Quantity
                                <asp:Label ID="labeltxtPCLapQty" runat="server" ForeColor="Red" Text="*" Visible="false"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtPCLapQty" runat="server" CssClass="textbox" TabIndex="17" onkeypress="return IsNumber();"
                                    Width="180" Enabled="false" PlaceHolder="Enter PC/Laptop Quantity" />
                                <asp:RequiredFieldValidator ID="reqtxtPCLapQty" runat="server" ControlToValidate="txtPCLapQty"
                                    ValidationGroup="save" ErrorMessage="Please specify number of PC/laptop required."
                                    Enabled="false" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please specify number of PC/laptop required.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Number of users expected
                                <asp:Label ID="labletxtExpectedUsers" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtExpectedUsers" runat="server" CssClass="textbox" TabIndex="18"
                                    onkeypress="return IsNumber();" Width="180" PlaceHolder="Enter no.of users expected" />
                                <asp:RequiredFieldValidator ID="reqtxtExpectedUsers" runat="server" ControlToValidate="txtExpectedUsers"
                                    ValidationGroup="save" ErrorMessage="Please specify number of expected users."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Please specify number of expected users.' />" />
                            </td>
                            <td colspan="2" class="tdSpace">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                Approx. data size in GB per year
                                <asp:Label ID="labletxtDataSize" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtDataSize" runat="server" CssClass="textbox" TabIndex="19" Width="180"
                                    onkeypress="return IsNumber();" PlaceHolder="Enter data size" />
                                <asp:RequiredFieldValidator ID="reqtxtDataSize" runat="server" ControlToValidate="txtDataSize"
                                    ValidationGroup="save" ErrorMessage="Approx. data size cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Approx. data size cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">
                                Printing requirement,no of pages per day
                                <asp:Label ID="labletxtPages" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtPages" runat="server" CssClass="textbox" TabIndex="20" Width="180"
                                    onkeypress="return IsNumber();" PlaceHolder="Enter no.of pages required" />
                                <asp:RequiredFieldValidator ID="reqtxtPages" runat="server" ControlToValidate="txtPages"
                                    ValidationGroup="save" ErrorMessage="Please specify Printing requirement." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Please specify Printing requirement.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Other Software/ Hardware requirement to support requested software installation
                                <asp:Label ID="labletxtRequirements" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" colspan="3">
                                <asp:TextBox ID="txtRequirements" runat="server" CssClass="textarea" TextMode="MultiLine"
                                    TabIndex="21" Columns="100" Rows="3" Width="268px" PlaceHolder="eg: Network or third party software, internet access, etc." />
                                <asp:RequiredFieldValidator ID="reqtxtRequirements" runat="server" ControlToValidate="txtRequirements"
                                    ValidationGroup="save" ErrorMessage="Please specify other requirements." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Please specify other requirements.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Information Security concern
                                <asp:Label ID="labletxtSecurityIssues" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" colspan="3">
                                <asp:TextBox ID="txtSecurityIssues" runat="server" CssClass="textarea" TextMode="MultiLine"
                                    TabIndex="22" Columns="100" Rows="3" Width="268px" PlaceHolder="Describe Information Security concern" />
                                <asp:RequiredFieldValidator ID="reqtxtSecurityIssues" runat="server" ControlToValidate="txtSecurityIssues"
                                    ValidationGroup="save" ErrorMessage="Information Security concern cannot be blank."
                                    SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Information Security concern cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                IT requirement cost (approx) in Rs
                                <asp:Label ID="labeltxtITCost" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtITCost" runat="server" TabIndex="23" CssClass="textbox" PlaceHolder="Enter IT req. cost"
                                    onkeypress="return IsNumber();" AutoPostBack="true" OnTextChanged="txtITCost_TextChanged" />
                                <asp:RequiredFieldValidator ID="reqtxtITCost" runat="server" ControlToValidate="txtITCost"
                                    ValidationGroup="save" ErrorMessage="IT Requirement cost cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='IT Requirement cost cannot be blank.' />" />
                            </td>
                            <td colspan="2" class="tdSpace">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Total Cost
                                <br />
                                (Software Cost + IT requirement Cost) in Rs
                                <asp:Label ID="labeltxtTotalCost" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" colspan="3">
                                <asp:TextBox ID="txtTotalCost" runat="server" TabIndex="24" CssClass="textbox" onkeypress="return IsNumber();"
                                    Enabled="false" />
                                <asp:RequiredFieldValidator ID="reqtxtTotalCost" runat="server" ControlToValidate="txtTotalCost"
                                    ValidationGroup="save" ErrorMessage="Total cost cannot be blank." SetFocusOnError="true"
                                    Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Total cost cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD">
                                Remarks
                                <asp:Label ID="labletxtITRemarks" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" colspan="3">
                                <asp:TextBox ID="txtITRemarks" runat="server" CssClass="textarea" TextMode="MultiLine"
                                    TabIndex="25" Columns="100" Rows="3" Width="294px" PlaceHolder="Enter Remarks" />
                                <asp:RequiredFieldValidator ID="reqtxtITRemarks" runat="server" ControlToValidate="txtITRemarks"
                                    ValidationGroup="save" ErrorMessage="Remarks cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Remarks cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" align="left" colspan="4">
                                <b>Attach Documents (Image/PDF Files Only)</b>
                            </td>
                        </tr>
                        <tr>
                            <td class="rigthTD" align="left" colspan="2" valign="top">
                                <div>
                                    <asp:FileUpload ID="file_upload" runat="server" TabIndex="36" />
                                    <asp:Button ID="btnUploadDoc" runat="server" Text="Upload" OnClick="btnUploadDoc_Click" />
                                    <asp:Label ID="lblFileMessage" runat="server" ForeColor="Red" Visible="False" />
                                </div>
                            </td>
                            <td class="rigthTD" align="left" colspan="2">
                                <asp:GridView ID="grdAttachedDocs" runat="server" AutoGenerateColumns="False" DataKeyNames="Document_Upload_Id"
                                    Visible="False" OnRowCommand="grdAttachedDocs_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Attached Documents">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttachedDocName" runat="server" Text='<%# Eval("Document_Name") %>'
                                                    Visible="false" />
                                                <asp:Label ID="lblUploadedFileName" runat="server" Text='<%# Eval("Document_Name") %>'
                                                    Visible="false" />
                                                <asp:HyperLink ID="aDocPath" runat="server" Text='<%# Eval("Document_Name") %>' NavigateUrl='<%# Eval("Document_Path") %>'
                                                    Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                &nbsp;
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="X" ForeColor="Red" Font-Size="15px"
                                                    CommandName="DEL" Font-Bold="true" OnClientClick="return confirm('Are you certain you want to delete this document?');" />&nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr runat="server" id="trMISC" visible="false">
                            <td colspan="2">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="tdHeading" align="left" colspan="2">
                                            MISC Requirements
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftTD">
                                            Panel1 Decision
                                        </td>
                                        <td class="rigthTD">
                                            <asp:Label ID="txtMISCPanel1" runat="server" TabIndex="16" />
                                        </td>
                                       
                                    </tr>
                                    <tr>
                                        <td class="leftTD">
                                            Panel2 Decision
                                        </td>
                                        <td class="rigthTD">
                                            <asp:Label ID="txtMISCPanel2" runat="server" TabIndex="16" />
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td class="leftTD">
                                            Panel3 Decision
                                        </td>
                                        <td class="rigthTD">
                                            <asp:Label ID="txtMISCPanel3" runat="server" TabIndex="16" />
                                        </td>
                                       
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="save" UseSubmitBehavior="true"
                                    Text="Save" CssClass="button" TabIndex="26" OnClick="btnSave_Click" Width="75px" />
                                <asp:Button ID="btnNext" runat="server" Text="Submit" TabIndex="18" CssClass="button"
                                    Width="76px" UseSubmitBehavior="true" ValidationGroup="save" OnClick="btnNext_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%--     </ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:ValidationSummary ID="save" runat="server" ValidationGroup="save" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblSWFormId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="83" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
</asp:Content>
