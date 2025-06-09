<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Common/HomeMasterPage.master"
    AutoEventWireup="true" CodeFile="MaterialPendingReqReport.aspx.cs" Inherits="Transaction_Common_MaterialPendingReqReport" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<%@ Register Src="~/Transaction/UserControl/ucExcelDownloadl.ascx" TagPrefix="uc"
    TagName="ExcelDownload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlSearch" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="4">
                    <%--Changed by:Manali , date: 21/05/19 , desc: Change heading to "Pending Request Report"--%> 
                    Pending Request Report
                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>
            <%--Added by manali--%>
            <tr>
                <td class="leftTD" align="left">Master Type
                </td>
                <td class="rigthTD" align="left">
                    <asp:DropDownList ID="ddlMasterTypeSearch" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlMasterTypeSearch_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="M">Material Master</asp:ListItem>
                        <asp:ListItem Value="C">Customer Master</asp:ListItem>
                        <asp:ListItem Value="V">Vendor  Master</asp:ListItem>
                        <asp:ListItem Value="I">Cost Center</asp:ListItem>
                        <asp:ListItem Value="G">GL Master</asp:ListItem>
                        <asp:ListItem Value="B">BOM/Recipe/Prod Ver Master</asp:ListItem>
                        <%--<asp:ListItem Value="P">Price Master</asp:ListItem>--%>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqddlMasterTypeSearch" runat="server" ControlToValidate="ddlMasterTypeSearch" AppendDataBoundItems="true"
                        ErrorMessage="Select Master Type." SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Master Type.'  />"
                        ValidationGroup="PendingReq" InitialValue="0" />
                </td>
                <td class="leftTD" align="left" style="width: 25%">Plant
                </td>
                <td class="rigthTD" align="left" style="width: 25%">
                    <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select" Value="" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>
            <tr>
                <td class="leftTD" align="left" style="width: 25%">
                    <%--Purchasing Group--%>
                    <asp:Label ID="lblPurchasingGroup" runat="server" Visible="false"> Purchasing Group</asp:Label>
                    <asp:Label ID="lblTerritory" runat="server" Visible="false"> Territory</asp:Label>

                    <asp:Label ID="lblBusinessArea" runat="server" Visible="false"> Business Area</asp:Label>

                </td>
                <td class="rigthTD" align="left" style="width: 25%">
                    <asp:DropDownList ID="ddlPurchasingGroup" runat="server" AppendDataBoundItems="true" Visible="false">
                        <asp:ListItem Text="Select" Value="" />
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlTerritory" runat="server" AppendDataBoundItems="true" Visible="false">
                        <asp:ListItem Text="Select" Value="" />
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlBusinessArea" runat="server" AppendDataBoundItems="true" Visible="false">
                        <asp:ListItem Text="Select" Value="" />
                    </asp:DropDownList>

                </td>
                <td class="leftTD" align="left" style="width: 25%"></td>
                <td class="rigthTD" align="left" style="width: 25%"></td>
            </tr>
            <%--Added by manali--%>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>
            <tr>
                <td class="leftTD" align="left">Module
                </td>
                <td class="rigthTD" align="left">
                    <asp:DropDownList ID="ddlModuleSearch" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqddlModuleSearch" runat="server" ControlToValidate="ddlModuleSearch" AppendDataBoundItems="true"
                        ErrorMessage="Select Module." SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Select Module.'  />"
                        ValidationGroup="PendingReq" />
                </td>
                <%--<td colspan="2" class="tdSpace">
                </td>--%>
                <td class="leftTD" align="left">Status
                </td>
                <td class="rigthTD" align="left">
                    <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="ALL" Value="" />
                        <asp:ListItem Text="Pending" Value="P" />
                        <asp:ListItem Text="Rollbacked" Value="R" />
                        <asp:ListItem Text="Approved" Value="ALL" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>
            <%--Added by manali--%>
            <tr>
                <td class="leftTD" align="left">
                    <%--Zone--%>
                    <asp:Label ID="lbliZone" runat="server" Visible="false"> Zone</asp:Label>
                </td>
                <td class="rigthTD" align="left">
                    <asp:DropDownList ID="ddliZone" runat="server" AppendDataBoundItems="true" Visible="false">
                    </asp:DropDownList>
                </td>
                <%--<td colspan="2" class="tdSpace">
                </td>--%>
                <td class="leftTD" align="left">
                    <%--Division--%>
                    <asp:Label ID="lblDivision" runat="server" Visible="false"> Division</asp:Label>
                </td>
                <td class="rigthTD" align="left">
                    <asp:DropDownList ID="ddlDivision" runat="server" AppendDataBoundItems="true" Visible="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>
            <%--Added by manali--%>
            <tr>
                <td class="leftTD" align="left">From Date
                </td>
                <td class="rigthTD" align="left">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" />
                    <act:CalendarExtender ID="CaltxtFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFromDate"
                        ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                        Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                        ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="reqtxtFromDate" runat="server" ForeColor="Red" ErrorMessage="From Date cannot be blank"
                        SetFocusOnError="true" Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                        ControlToValidate="txtFromDate" ValidationGroup="PendingReq" Display="Dynamic">
                         
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftTD" align="left">To Date
                </td>
                <td class="rigthTD" align="left">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" />
                    <act:CalendarExtender ID="CaltxtToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtToDate"
                        ForeColor="Red" ErrorMessage="Enter Valid Date in dd/mm/yyyy format." SetFocusOnError="True"
                        Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                        ValidationGroup="save" Display="Dynamic" ValidationExpression="(^((((0[1-9])|([1-2][0-9])|(3[0-1]))|([1-9]))\x2F(((0[1-9])|(1[0-2]))|([1-9]))\x2F(([0-9]{2})|(((19)|([2]([0]{1})))([0-9]{2}))))$)"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="reqtxtToDate" runat="server" ForeColor="Red" ErrorMessage="To Date cannot be blank"
                        SetFocusOnError="true" Text="<img src='../../images/Error.png' title='Enter Valid Date in dd/mm/yyyy format.' />"
                        ControlToValidate="txtToDate" ValidationGroup="PendingReq" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>
            <tr>
                <td class="leftTD" align="left">Approving Department
                </td>
                <td class="rigthTD" align="left">
                    <asp:DropDownList ID="ddlApprDept" runat="server" AppendDataBoundItems="true">
                    </asp:DropDownList>
                </td>
                <td class="leftTD" align="left">Created By
                </td>
                <td class="rigthTD" align="left">
                    <asp:DropDownList ID="ddlUserName" runat="server" AppendDataBoundItems="true">
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>
            <tr>
                <td class="leftTD" align="left">Delay Days
                </td>
                <td class="rigthTD" align="left">
                    <asp:TextBox ID="txtPendingDays" runat="server" CssClass="textbox" onkeypress="return IsNumber();"
                        MaxLength="5" Text="0" />
                </td>
                <%--<td colspan="2" class="tdSpace"></td>--%>
                <%--BOM_NWF_SDT05072019--%>
                 <td class="leftTD" align="left">
                       <asp:Label ID="lblMaterialcode" runat="server" Visible="false"> Material code</asp:Label>
                     
                </td>
                <td class="rigthTD" align="left">
                    <asp:TextBox ID="txtMaterialcode" runat="server" CssClass="textbox" onkeypress="return IsNumber();"
                        MaxLength="10"  Visible="false"/>
                </td>
                <%--BOM_NWF_SDT05072019--%>
            </tr>
            <tr>
                <td class="centerTD" colspan="4">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click"
                        ValidationGroup="PendingReq" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>
            <tr style="display: none !important;" id="trgrdSearch" runat="server">
                <td colspan="4">
                    <br />
                    <asp:GridView ID="grdSearch" runat="server" AutoGenerateColumns="false" Width="100%"
                        BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                        OnPageIndexChanging="grdSearch_PageIndexChanging" GridLines="Both">
                        <RowStyle CssClass="light-gray" />
                        <HeaderStyle CssClass="gridHeader" />
                        <AlternatingRowStyle CssClass="gridRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="Request No." DataField="RequestNumber" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Mass Request No." DataField="Mass_Request_Number" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Description" DataField="Descriptn" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Created By" DataField="Requested_By" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Created On" DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Plant" DataField="Plant" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Pur.Grp" DataField="Purchasing_Grp" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="RequestStatus" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Appr. Dept" DataField="DepartmentName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Delay" DataField="delayDays" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="First Approver" DataField="First_App" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Backup Approver" DataField="Second_App" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>

            <tr style="display: none !important;" id="trgrdSearchC" runat="server">
                <td colspan="4">
                    <br />
                    <asp:GridView ID="grdSearchC" runat="server" AutoGenerateColumns="false" Width="100%"
                        BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                        OnPageIndexChanging="grdSearchC_PageIndexChanging" GridLines="Both">
                        <RowStyle CssClass="light-gray" />
                        <HeaderStyle CssClass="gridHeader" />
                        <AlternatingRowStyle CssClass="gridRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="Request No." DataField="RequestNumber" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Mass Request No." DataField="Mass_Request_Number" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Description" DataField="Descriptn" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Created By" DataField="Requested_By" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Created On" DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Plant" DataField="Plant" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Division" DataField="Division" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="RequestStatus" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Appr. Dept" DataField="DepartmentName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Delay" DataField="delayDays" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="First Approver" DataField="First_App" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Backup Approver" DataField="Second_App" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Zone" DataField="Zone" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Territory" DataField="Territory" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="CusomerType" DataField="CusomerType" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>


            <tr style="display: none !important;" id="trgrdSearchV" runat="server">
                <td colspan="4">
                    <br />
                    <asp:GridView ID="grdSearchV" runat="server" AutoGenerateColumns="false" Width="100%"
                        BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                        OnPageIndexChanging="grdSearchV_PageIndexChanging" GridLines="Both">
                        <RowStyle CssClass="light-gray" />
                        <HeaderStyle CssClass="gridHeader" />
                        <AlternatingRowStyle CssClass="gridRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="Request No." DataField="RequestNumber" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Mass Request No." DataField="Mass_Request_Number" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Description" DataField="Descriptn" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Created By" DataField="Requested_By" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Created On" DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Plant" DataField="Plant" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="RequestStatus" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Appr. Dept" DataField="DepartmentName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Delay" DataField="delayDays" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="First Approver" DataField="First_App" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Backup Approver" DataField="Second_App" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>


            <tr style="display: none !important;" id="trgrdSearchI" runat="server">
                <td colspan="4">
                    <br />
                    <asp:GridView ID="grdSearchI" runat="server" AutoGenerateColumns="false" Width="100%"
                        BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                        OnPageIndexChanging="grdSearchI_PageIndexChanging" GridLines="Both">
                        <RowStyle CssClass="light-gray" />
                        <HeaderStyle CssClass="gridHeader" />
                        <AlternatingRowStyle CssClass="gridRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="Request No." DataField="RequestNumber" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Mass Request No." DataField="Mass_Request_Number" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Description" DataField="Descriptn" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Created By" DataField="Requested_By" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Created On" DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Plant" DataField="Plant" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Business Area" DataField="BusinessArea" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Status" DataField="RequestStatus" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Appr. Dept" DataField="DepartmentName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Delay" DataField="delayDays" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="First Approver" DataField="First_App" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Backup Approver" DataField="Second_App" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Hierarchy Area" DataField="Hierarchy_Area" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Profit Center" DataField="Profit_Center" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Cost Center Category" DataField="Catergory" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                        </Columns>
                    </asp:GridView>
                </td>
            </tr>


            <tr style="display: none !important;" id="trgrdSearchG" runat="server">
                <td colspan="4">
                    <br />
                    <asp:GridView ID="grdSearchG" runat="server" AutoGenerateColumns="false" Width="100%"
                        BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                        OnPageIndexChanging="grdSearchG_PageIndexChanging" GridLines="Both">
                        <RowStyle CssClass="light-gray" />
                        <HeaderStyle CssClass="gridHeader" />
                        <AlternatingRowStyle CssClass="gridRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="Request No." DataField="RequestNumber" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Mass Request No." DataField="Mass_Request_Number" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Description" DataField="Descriptn" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Created By" DataField="Requested_By" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Created On" DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Plant" DataField="Plant" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="RequestStatus" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Appr. Dept" DataField="DepartmentName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Delay" DataField="delayDays" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="First Approver" DataField="First_App" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Backup Approver" DataField="Second_App" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="P & L Statement / Balance Sheet Account" DataField="PnLStatement" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Cost Element Category" DataField="CostElementCategory" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>

            <%--BOM_NWF_SDT05072019--%>
              <tr style="display: none !important;" id="trgrdSearchB" runat="server">
                <td colspan="4">
                    <br />
                    <asp:GridView ID="grdSearchB" runat="server" AutoGenerateColumns="false" Width="100%"
                        BorderColor="#9D9D9D" EmptyDataText="No Data Found" AllowPaging="true" PageSize="10"
                        OnPageIndexChanging="grdSearchB_PageIndexChanging" GridLines="Both">
                        <RowStyle CssClass="light-gray" />
                        <HeaderStyle CssClass="gridHeader" />
                        <AlternatingRowStyle CssClass="gridRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="Request No." DataField="RequestNumber" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Mass Request No." DataField="Mass_Request_Number" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Material Code" DataField="MaterialCode" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            
                            <asp:BoundField HeaderText="Prod. Version/Recipe header Description" DataField="PVRHDesc" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Recipe Group" DataField="RGroup" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            
                            <asp:BoundField HeaderText="Alternetive BOM" DataField="AltBOM" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Production Version" DataField="PrdVersion" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Created By" DataField="Requested_By" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Created On" DataField="CreatedOn" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Plant" DataField="Plant" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="RequestStatus" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Appr. Dept" DataField="DepartmentName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Delay" DataField="delayDays" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="First Approver" DataField="First_App" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Backup Approver" DataField="Second_App" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <%--BOM_NWF_SDT05072019--%>
            <tr>
                <td colspan="4" class="tdSpace"></td>
            </tr>
        </table>
    </asp:Panel>
    <div align="left" style="width: 98%;">
        <uc:ExcelDownload ID="ExcelDownload1" runat="server" ActionType="M" />
    </div>

    <%-- <div align="left" style="width: 98%; display:none !important;" id="divExcelDownload1M" runat ="server">
        <uc:ExcelDownload ID="ExcelDownload1" runat="server" ActionType="M" />
    </div>

     <div align="left" style="width: 98%; display:none !important;" id="divExcelDownload2CC" runat ="server">
        <uc:ExcelDownload ID="ExcelDownload2" runat="server" ActionType="CC" />
    </div>--%>

    <%--<div align="left">--%>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="PendingReq" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
</asp:Content>
