<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPage.master" AutoEventWireup="true" 
    CodeFile="ApproverMaster.aspx.cs" Inherits="Administration_ApproverMaster" %>
    <%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="upMsg" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server"/>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="upAppSearch" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlAppSearch" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                     <tr>
                        <td class="trHeading" align="center" colspan="24">
                            Approver Mapping 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left" style="width: 25%">
                            Type
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:DropDownList ID="ddlModuleType" runat="server" ToolTip="Select Type" AutoPostBack = "true"
                                OnSelectedIndexChanged="ddlModuleType_SelectedIndexChanged">
                                    <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Vendor" Value="V"></asp:ListItem>
                                    <asp:ListItem Text="Customer" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="Material" Value="M"></asp:ListItem>
									<asp:ListItem Text="BOM/Recipe" Value="B"></asp:ListItem>

                                <asp:ListItem Text="Audit Request Form" Value="E"></asp:ListItem>
                                <asp:ListItem Text="GL Master" Value="G"></asp:ListItem>
                                <asp:ListItem Text="Cost Center" Value="I"></asp:ListItem>
                                <asp:ListItem Text="Software" Value="S"></asp:ListItem>
                                 <asp:ListItem Text="ZCAP/ZPEX/HSN/GST" Value="Z"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="leftTD" align="left" style="width: 25%">
                            Module
                        </td>
                        <td class="rigthTD" align="left">
                           <%--<asp:DropDownList ID="ddlModuleSearch" runat="server" 
                                AppendDataBoundItems="true" AutoPostBack = "true"
                                OnSelectedIndexChanged="ddlModuleSearch_SelectedIndexChanged">
                                <asp:ListItem Text="---Select---" Value="0" />
                            </asp:DropDownList>--%>
                              <cc1:DropDownCheckBoxes ID="ddlModuleSearch" runat="server" AddJQueryReference="false"
                                            TabIndex="3" UseButtons="false" UseSelectAllNode="true"  OnSelectedIndexChanged="ddlModuleSearch_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <Style2 SelectBoxWidth="250" DropDownBoxBoxWidth="350" DropDownBoxBoxHeight="80" />
                                            <Texts SelectBoxCaption="--Select--" />
                                        </cc1:DropDownCheckBoxes>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="leftTD" align="left" style="width: 25%">
                            Department
                        </td>
                        <td class="rigthTD" align="left">
                            <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="" />
                            </asp:DropDownList>
                        </td>
                        <td colspan="2" class="tdSpace">
                        </td
                    </tr>
                    <tr>
                        <td colspan="4" class="tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td class="centerTD" colspan="4">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button"
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan = "4" class = "tdSpace">
                        </td>
                    </tr>
                    <tr>
                        <td colspan = "4">
                         <br />
                            <script type="text/javascript">
                                function CheckOtherIsCheckedByGVID(spanChk) {

                                    var CurrentRdbID = spanChk.id;
                                    var Chk = spanChk;
                                    Parent = document.getElementById("<%=grdApproverSearch.ClientID%>");
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
                            <asp:GridView ID="grdApproverSearch" runat="server" AutoGenerateColumns="false" Width="100%"
                                BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                                OnPageIndexChanging="grdApproverSearch_PageIndexChanging" GridLines="Both">
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
                                            <asp:Label ID="lblAuthID" runat="server" Text='<%# Eval("Auth_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Module Name" DataField="Module_Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Workflow Type" DataField="Workflow_Type" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Dept Name" DataField="Dept_Name" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Plant" DataField="Plant" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="User Name" DataField="UserName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Priority" DataField="Priority" HeaderStyle-HorizontalAlign="Left"
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
                            <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="button"
                                OnClientClick="return Validate();" OnClick="btnModify_Click" />
                            <asp:Button ID="btnCreateNew" runat="server" Text="Create New" CssClass="button"
                                Width="120px" OnClick="btnCreateNew_Click" />
                            <script type="text/javascript">
                                function Validate() {
                                    var gv = document.getElementById("<%=grdApproverSearch.ClientID%>");
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
            <asp:AsyncPostBackTrigger ControlID="btnView" />
            <asp:AsyncPostBackTrigger ControlID="btnModify" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="btnBack" />
            <asp:AsyncPostBackTrigger ControlID="btnCreateNew" />
            
        </Triggers>
    </asp:UpdatePanel> 

    <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <asp:Panel ID="pnlAddNew" runat="server" Visible="false">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">
                            Approver Mapping
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
                            Type
                        </td>
                        <td class="rigthTD" >
                            <asp:DropDownList ID="ddlModuleTypen" runat="server" ToolTip="Select Type" AutoPostBack = "true"
                                OnSelectedIndexChanged="ddlModuleTypen_SelectedIndexChanged">
                                    <asp:ListItem Text="---Select---" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Vendor" Value="V"></asp:ListItem>
                                    <asp:ListItem Text="Customer" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="Material" Value="M"></asp:ListItem>
									<asp:ListItem Text="BOM/Recipe" Value="B"></asp:ListItem> 
                                <asp:ListItem Text="Audit Request Form" Value="E"></asp:ListItem>
                                <asp:ListItem Text="GL Master" Value="G"></asp:ListItem>
                                <asp:ListItem Text="Cost Center" Value="I"></asp:ListItem>
                                <asp:ListItem Text="Software" Value="S"></asp:ListItem>
                                <asp:ListItem Text="ZCAP/ZPEX/HSN/GST" Value="Z"></asp:ListItem>
                            </asp:DropDownList>
                        </td> 
                    </tr>
                    <tr>
                        <td colspan="2" class="tdSpace">
                        </td>
                    </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">
                                        Module
                                        <asp:Label ID="lableddlModule" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                      <%--  <asp:DropDownList ID="ddlModule" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlModule" runat="server" ControlToValidate="ddlModule"
                                            ValidationGroup="save" ErrorMessage="Module Name cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Module Name cannot be blank.' />" />
                                   --%>

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
                                    <td class="tdSpace" colspan="2">
                                          <asp:Label ID="lableRddlModule" runat="server"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">
                                        Workflow Code 
                                        <asp:Label ID="labletxtWorkflowCode" runat="server" ForeColor="Red" Text="*" Visible = "false"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtWorkflowCode" runat="server" CssClass="textbox"
                                            MaxLength="20" Width="100px" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">
                                        Workflow Type 
                                        <asp:Label ID="labletxtWorkflowType" runat="server" ForeColor="Red" Text="*" Visible = "false"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <%--<asp:TextBox ID="txtWorkflowType" runat="server" CssClass="textbox"
                                            MaxLength="20" Width="100px" ontextchanged="txtWorkflowType_TextChanged" AutoPostBack = "true"/>--%>
                                        <asp:DropDownList ID="ddlWorkflowType" runat="server" 
                                            AppendDataBoundItems="true" AutoPostBack = "true" 
                                            onselectedindexchanged="ddlWorkflowType_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                            <asp:ListItem Text="P" Value="P" />
                                            <asp:ListItem Text="PH" Value="PH" />
                                            <asp:ListItem Text="R" Value="R" />
											<asp:ListItem Text="M" Value="M" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>                               
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">
                                        Department 
                                        <asp:Label ID="lableddlDeptName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlDeptName" runat="server" AppendDataBoundItems="true" AutoPostBack = "true"
                                            OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlDeptName" runat="server" ControlToValidate="ddlDeptName"
                                            ValidationGroup="save" ErrorMessage="Department cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Department cannot be blank.' />" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">
                                        Reference ID 
                                        <asp:Label ID="lableddlReferenceID" runat="server" ForeColor="Red" Text="*" Visible = "false"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlReferenceID" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlReferenceID" runat="server" ControlToValidate="ddlReferenceID"
                                            ValidationGroup="save" ErrorMessage="Reference ID cannot be blank." SetFocusOnError="true" Enabled = "false"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Reference ID cannot be blank.' />" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">
                                        Approver Name 
                                        <asp:Label ID="lableddlApprover" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <%--<asp:DropDownList ID="ddlApprover" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>--%>
                                        <asp:TextBox ID="txtApprover" runat="server" CssClass="textboxAutocomplete" 
                                            onfocus="return ApproverOnFocus();"
                                            onblur="return ApproverTextChangeEvent();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqtxtApprover" runat="server" ControlToValidate="txtApprover"
                                            ValidationGroup="save" ErrorMessage="Approver cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Approver cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 25%">
                                        Priority
                                        <asp:Label ID="labletxtPriority" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtPriority" runat="server" CssClass="textbox"
                                            MaxLength="20" Width="100px" />
                                        <asp:RequiredFieldValidator ID="reqtxtPriority" runat="server" ControlToValidate="txtPriority"
                                            ValidationGroup="save" ErrorMessage="Priority cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../images/Error.png' title='Priority cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="centerTD" colspan="2">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                                            ValidationGroup="save" onclick="btnSave_Click"/>
                                        &nbsp;
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="button" 
                                            onclick="btnBack_Click"/>
                                    </td>
                                </tr>
                            </table> 
                        </td>
                    </tr>
                </table> 
            </asp:Panel>
        </ContentTemplate> 
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnView" />
            <asp:AsyncPostBackTrigger ControlID="btnModify" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="btnBack" />
            <asp:AsyncPostBackTrigger ControlID="btnCreateNew" />
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

        function txtApproverOnFocus() {

            textboxId = $('#<%= txtApprover.ClientID%>').attr('ID');
            CheckLookupUser();
        }

        function txtApproverTextChangeEvent() {
            __doPostBack($('#<%= txtApprover.ClientID%>').attr('ID'), 'TextChanged');
        }


        var textboxId = "";
        var textboxRealId = "";


        function ApproverOnFocus() {
            textboxId = $('#<%= txtApprover.ClientID%>').attr('ID');
            AutoCompleteUserName();
        }

        function ApproverTextChangeEvent() {
            __doPostBack($('#<%= txtApprover.ClientID%>').attr('ID'), 'TextChanged');
        }

    </script>
</asp:Content>

