<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Customer/CustomerMasterPage.master" AutoEventWireup="true" CodeFile="DasSystemInfo.aspx.cs"
    Inherits="Transaction_Customer_DasSystemInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/LookUp.js" type="text/javascript"></script>
    <script src="../../js/jquery.MultiFile.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMsg" runat="server" Visible="false">
        <asp:Label ID="lblMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlData" runat="server">
        <table border="0" cellpadding="0" width="100%">
            <tr>
                <td class="trHeading" align="center" colspan="4">Das System Information
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" colspan="4">
                    <table border="0" cellpadding="0" width="100%">
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Depot
                                <asp:Label ID="lableddlDeliveringPlant" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <%--<asp:TextBox ID="txtDepot" runat="server" CssClass="textbox" TabIndex="1" Width="180" />--%>
                                <asp:DropDownList ID="ddlDeliveringPlant" runat="server" TabIndex="8">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlDeliveringPlant" runat="server" ControlToValidate="ddlDeliveringPlant"
                                    ValidationGroup="save" ErrorMessage="Depot cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Depot cannot be blank.' />" />
                            </td>

                            <td class="leftTD" width="20%">Division
                                <asp:Label ID="labeltxtDivision" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <%--<asp:TextBox ID="txtDivision" runat="server" CssClass="textbox" TabIndex="1" Width="180" disabled="disabled"/>--%>
                                <asp:DropDownList ID="ddlDivision" runat="server" TabIndex="3" Enabled="false">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="reqddlDivision" runat="server" ControlToValidate="ddlDivision"
                                    ValidationGroup="save" ErrorMessage="Division cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Division cannot be blank.' />" />

                            </td>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>

                            <td class="leftTD" width="20%">Territory
                                <asp:Label ID="labeltxtTerritory" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <%--<asp:TextBox ID="txtTerritory" runat="server" CssClass="textbox" TabIndex="1" Width="180" />--%>

                                <asp:DropDownList ID="ddlTerritory" runat="server" TabIndex="3">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="reqddlTerritory" runat="server" ControlToValidate="ddlTerritory"
                                    ValidationGroup="save" ErrorMessage="Territory cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Territory cannot be blank.' />" />
                            </td>

                            <td class="leftTD" style="width: 20%">Structure of the Firm (Partnership/pop)
                                <asp:Label ID="labeltxtStructure_Of_Firm" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <%--<asp:TextBox ID="txtStructure_Of_Firm" runat="server" CssClass="textbox" TabIndex="1" Width="180" />--%>

                                <asp:DropDownList ID="ddlStructure_Of_Firm" runat="server" TabIndex="3">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="reqddlStructure_Of_Firm" runat="server" ControlToValidate="ddlStructure_Of_Firm"
                                    ValidationGroup="save" ErrorMessage="Structure of the Firm cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Structure of the Firm cannot be blank.' />" />
                            </td>

                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>

                            <td class="leftTD" width="20%">Name of the Proprietor/Partner/Directors (Partnership deed copy in case of partnership firm)
                                <asp:Label ID="labeltxtNameOfProprietor" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtNameOfProprietor" runat="server" CssClass="textbox" TabIndex="1" Width="180" />

                                <asp:RequiredFieldValidator ID="reqtxtNameOfProprietor" runat="server" ControlToValidate="txtNameOfProprietor"
                                    ValidationGroup="save" ErrorMessage="Name of the Proprietor/Partner/Directors cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Name of the Proprietor/Partner/Directors cannot be blank.' />" />
                            </td>

                            <td class="leftTD" style="width: 20%">Bank Name
                                <asp:Label ID="labeltxtBank_Name" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtBank_Name" runat="server" CssClass="textbox" TabIndex="1" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtBank_Name" runat="server" ControlToValidate="txtBank_Name" 
                                    ValidationGroup="CustGeneral" ErrorMessage="Bank Name cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Bank Name cannot be blank.' />" />
                            </td>

                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>

                            <td class="leftTD" style="width: 20%">Bank Address
                                <asp:Label ID="labeltxtBank_Addr" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtBank_Addr" runat="server" CssClass="textbox" TabIndex="1" Width="180" />
                                <asp:RequiredFieldValidator ID="reqtxtBank_Addr" runat="server" ControlToValidate="txtBank_Addr"
                                    ValidationGroup="CustGeneral" ErrorMessage="Bank Address cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Bank Address cannot be blank.' />" />
                            </td>

                            <td class="leftTD" style="width: 20%">Want to avail cheque facility
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <%--  --%>
                                <asp:DropDownList ID="ddlAvail_Cheque" runat="server" TabIndex="23">
                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>

                            <td class="leftTD" style="width: 20%">Name of the Transporter
                                <asp:Label ID="labeltxtTransporter_Name" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtTransporter_Name" runat="server" CssClass="textbox" TabIndex="1" Width="180" />

                                <asp:RequiredFieldValidator ID="reqtxtTransporter_Name" runat="server" ControlToValidate="txtTransporter_Name"
                                    ValidationGroup="save" ErrorMessage="Name of the Transporter cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Name of the Transporter cannot be blank.' />" />
                            </td>

                            <td class="leftTD" style="width: 20%">Whether additional party or replacement
                                 <asp:Label ID="labeltxtAdd_Or_Replacement" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <%--<asp:TextBox ID="txtAdd_Or_Replacement" runat="server" CssClass="textbox" TabIndex="1" Width="180" />--%>

                                <asp:DropDownList ID="ddlAdd_Or_Replacement" runat="server" TabIndex="3">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="New" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Replacement" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Line Extension" Value="2"></asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="reqddlAdd_Or_Replacement" runat="server" ControlToValidate="ddlAdd_Or_Replacement"
                                    ValidationGroup="save" ErrorMessage="Whether additional party or replacement cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Whether additional party or replacement cannot be blank.' />" />
                            </td>

                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>

                            <td class="leftTD" style="width: 20%">No of years in Pharma Distribution Business
                                 <asp:Label ID="labeltxtYears_Pharma_Distr" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtYears_Pharma_Distr" runat="server" CssClass="textbox" TabIndex="1" Width="180" />

                                <asp:RequiredFieldValidator ID="reqtxtYears_Pharma_Distr" runat="server" ControlToValidate="txtYears_Pharma_Distr"
                                    ValidationGroup="save" ErrorMessage="No of years in Pharma Distribution Business cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='No of years in Pharma Distribution Business cannot be blank.' />" />
                            </td>

                            <td class="leftTD" style="width: 20%">Channel of Distribution
                                <asp:Label ID="labeltxtChannel_Of_Distr" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtChannel_Of_Distr" runat="server" CssClass="textbox" TabIndex="1" Width="180" />

                                <asp:RequiredFieldValidator ID="reqtxtChannel_Of_Distr" runat="server" ControlToValidate="txtChannel_Of_Distr"
                                    ValidationGroup="save" ErrorMessage="Channel of Distribution cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Channel of Distribution cannot be blank.' />" />
                            </td>

                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>

                        <tr>
                            <td colspan="3"><b>Details of Major Pharma Companies for which party is a Distributor</b></td>
                        </tr>
                        <tr style="height: auto; overflow-x: auto; width: 950px;">
                            <td colspan="4">

                                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="panelTable" runat="server">
                                        </asp:Panel>
                                        <span class="style1">
                                            <asp:Button ID="btnInsertRecord" runat="server" Text="Add New Row.." OnClick="btnInsertRecord_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>--%>



                                <asp:GridView runat="server" ID="gvDetails" ShowFooter="true" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowDeleting="gvDetails_RowDeleting">

                                    <HeaderStyle CssClass="headerstyle" />

                                    <Columns>

                                        <asp:BoundField DataField="rowid" HeaderText="Sr. No." ReadOnly="true" Visible="false" />

                                        <asp:TemplateField HeaderText="Name of Company">

                                            <ItemTemplate>

                                                <asp:TextBox ID="txtName" runat="server" Width="500px" />

                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Monthly Turnover(Rs. in Lacs)">

                                            <ItemTemplate>

                                                <asp:TextBox ID="txtPrice" runat="server" Width="250px" />

                                            </ItemTemplate>

                                            <FooterTemplate>

                                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />

                                            </FooterTemplate>

                                        </asp:TemplateField>

                                        <asp:CommandField ShowDeleteButton="true" />

                                    </Columns>

                                </asp:GridView>






                            </td>
                        </tr>

                        <tr>
                        </tr>

                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Last three years Total Turnover of the Party (Rs. in Lacs)
                                <asp:Label ID="labeltxtTurnover_Three_Years" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtTurnover_Three_Years" runat="server" CssClass="textbox" TabIndex="1" Width="180" />

                                <asp:RequiredFieldValidator ID="reqtxtTurnover_Three_Years" runat="server" ControlToValidate="txtTurnover_Three_Years"
                                    ValidationGroup="save" ErrorMessage="Last three years Total Turnover cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Last three years Total Turnover cannot be blank.' />" />
                            </td>
                            <td class="leftTD" style="width: 20%">Submitted bank statement for at least last three months : 
                                
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:DropDownList ID="ddlBank_Stmt_Submitted" runat="server" TabIndex="23">
                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                </asp:DropDownList>


                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Expected Monthly sales in Lupin (Rs. in Lacs)
                                <asp:Label ID="labeltxtExpected_Monthly_Sales" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtExpected_Monthly_Sales" runat="server" CssClass="textbox" TabIndex="1" Width="180" />

                                <asp:RequiredFieldValidator ID="reqtxtExpected_Monthly_Sales" runat="server" ControlToValidate="txtExpected_Monthly_Sales"
                                    ValidationGroup="save" ErrorMessage="Expected Monthly sales in Lupin cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Expected Monthly sales in Lupin cannot be blank.' />" />
                            </td>

                            <td class="leftTD" style="width: 20%">Justification for Appointment by RSM
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtJustification_For_Appt" runat="server" CssClass="textbox" TabIndex="1" Width="180" />
                            </td>
                            <td class="tdSpace" colspan="2"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Stock value of Distributor to be replaced
                                <%--<asp:Label ID="lblddlStockValue" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                            </td>

                            <td class="rigthTD" style="width: 30%">
                                <%--<asp:TextBox ID="txtStockValue" runat="server" CssClass="textbox" TabIndex="1" Width="180" />--%>

                                <asp:DropDownList ID="ddlStockValue" runat="server" TabIndex="1">
                                    <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                </asp:DropDownList>

                                <%--<asp:RequiredFieldValidator ID="reqddlStockValue" runat="server" ControlToValidate="ddlStockValue"
                                    ValidationGroup="save" ErrorMessage="Stock value of Distributor to be replaced cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Stock value of Distributor to be replaced cannot be blank.' />" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>

                            <td class="tdSpace" colspan="2"></td>
                        </tr>

                        <tr>
                            <td colspan="2"><b>Comments from DE/RCM/RDM - table</b></td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Current Sales of the Territory (Rs. in Lacs)
                                <asp:Label ID="lbltxtCur_Territory_Sales" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtCur_Territory_Sales" runat="server" CssClass="textbox" TabIndex="1" Width="180" />

                                <asp:RequiredFieldValidator ID="reqtxtCur_Territory_Sales" runat="server" ControlToValidate="txtCur_Territory_Sales"
                                    ValidationGroup="save" ErrorMessage="Current Sales of the Territory (Rs. in Lacs) cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Current Sales of the Territory (Rs. in Lacs) cannot be blank.' />" />
                            </td>

                            <td class="leftTD" style="width: 20%">Existing No. of distributors in the Territory
                                <asp:Label ID="lbltxtDist_In_Territory" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtDist_In_Territory" runat="server" CssClass="textbox" TabIndex="1" Width="180" />

                                <asp:RequiredFieldValidator ID="reqtxtDist_In_Territory" runat="server" ControlToValidate="txtDist_In_Territory"
                                    ValidationGroup="save" ErrorMessage="Existing No. of distributors in the Territory cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Existing No. of distributors in the Territory cannot be blank.' />" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">Ratio of Sales per Distributor (Rs. in Lacs)
                                <asp:Label ID="lbltxtSales_Ratio_Per_Dist" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD" style="width: 30%">
                                <asp:TextBox ID="txtSales_Ratio_Per_Dist" runat="server" CssClass="textbox" TabIndex="1" Width="180" />

                                <asp:RequiredFieldValidator ID="reqtxtSales_Ratio_Per_Dist" runat="server" ControlToValidate="txtSales_Ratio_Per_Dist"
                                    ValidationGroup="save" ErrorMessage="Ratio of Sales per Distributor (Rs. in Lacs) cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='Ratio of Sales per Distributor (Rs. in Lacs) cannot be blank.' />" />
                            </td>



                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td colspan="2"><b>Age wise outstanding of Headquarter</b></td>
                            <%--<asp:Label ID="lbltxtOutstanding_0_30" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>

                            <td class="tdSpace" colspan="4">
                                <table border="1">
                                    <tr>
                                        <td><b>Age</b></td>
                                        <td><b>0-30</b></td>
                                        <td><b>31-60</b></td>
                                        <td><b>61-90</b></td>
                                        <td><b>91-180</b></td>
                                        <td><b>Age > 180</b></td>
                                    </tr>
                                    <tr>
                                        <td><b>Outstanding (Rs. in Lacs)</b></td>
                                        <td>
                                            <%--<asp:Label ID="lbltxtOutstanding_0_30" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                            <asp:TextBox ID="txtOutstanding_0_30" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="reqtxtOutstanding_0_30" runat="server" ControlToValidate="txtOutstanding_0_30"
                                                ValidationGroup="save" ErrorMessage="Age 0-30 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Age 0-30 cannot be blank.' />" />--%>
                                        </td>
                                        <td>
                                            <%--<asp:Label ID="lbltxtOutstanding_31_60" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                            <asp:TextBox ID="txtOutstanding_31_60" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="reqtxtOutstanding_31_60" runat="server" ControlToValidate="txtOutstanding_31_60"
                                                ValidationGroup="save" ErrorMessage="Age 31-60 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Age 31-60 cannot be blank.' />" />--%>
                                        </td>
                                        <td>
                                            <%--<asp:Label ID="lbltxtOutstanding_61_90" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                            <asp:TextBox ID="txtOutstanding_61_90" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="reqtxtOutstanding_61_90" runat="server" ControlToValidate="txtOutstanding_61_90"
                                                ValidationGroup="save" ErrorMessage="Age 61-90 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Age 61-90 cannot be blank.' />" />--%>
                                        </td>
                                        <td>
                                            <%--<asp:Label ID="lbltxtOutstanding_91_180" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                            <asp:TextBox ID="txtOutstanding_91_180" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="reqtxtOutstanding_91_180" runat="server" ControlToValidate="txtOutstanding_91_180"
                                                ValidationGroup="save" ErrorMessage="Age 91-180 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Age 91-180 cannot be blank.' />" />--%>
                                        </td>
                                        <td>
                                            <%--<asp:Label ID="lbltxtOutstanding_Age_180" runat="server" ForeColor="Red" Text="*"></asp:Label>--%>
                                            <asp:TextBox ID="txtOutstanding_Age_180" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="reqtxtOutstanding_Age_180" runat="server" ControlToValidate="txtOutstanding_Age_180"
                                                ValidationGroup="save" ErrorMessage="Age > 180 cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Age > 180 cannot be blank.' />" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD">In case of Replacement, if any outstanding: 
                                <asp:Label ID="labelddlOutstanding_Replacement" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="rigthTD">
                                <asp:DropDownList ID="ddlOutstanding_Replacement" runat="server" TabIndex="23">
                                    <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlOutstanding_Replacement" runat="server" ControlToValidate="ddlOutstanding_Replacement"
                                    ValidationGroup="save" InitialValue="0" ErrorMessage="In case of Replacement, if any outstanding cannot be blank." SetFocusOnError="true"
                                    Display="Dynamic" Text="<img src='../../images/Error.png' title='In case of Replacement, if any outstanding cannot be blank.' />" />
                            </td>
                            <td class="leftTD">Any other feedback
                            </td>
                            <td class="rigthTD">
                                <asp:TextBox ID="txtFeeback" runat="server" CssClass="textarea" TextMode="MultiLine" Columns="100" Rows="3" TabIndex="1" Width="180" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr>
                            <td class="leftTD" align="left" colspan="4">
                                <b>Attach Documents (Image/PDF Files Only)</b>
                            </td>
                        </tr>
                        <tr>
                            <td class="rigthTD" align="left" colspan="2" valign="top">
                                <div>
                                    <asp:FileUpload ID="file_upload" class="multi" runat="server" />
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
                                        <asp:TemplateField Visible="false">
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
                            <td class="tdSpace" colspan="4"></td>
                        </tr>
                        <tr id="trButton" runat="server" visible="false">
                            <td class="centerTD" colspan="4">
                                <asp:Button ID="btnPrevious" runat="server" ValidationGroup="save" Text="Back"
                                    CssClass="button" OnClick="btnPrevious_Click" />
                                <asp:Button ID="btnSave" runat="server" ValidationGroup="save" Text="Save"
                                    CssClass="button" OnClick="btnSave_Click" />
                                <asp:Button ID="btnNext" runat="server" ValidationGroup="save" Text="Save & Next"
                                    CssClass="button" OnClick="btnNext_Click" Width="120px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="CustGeneral" ShowMessageBox="true"
        ShowSummary="false" />
    <asp:Label ID="lblUserId" runat="server" Visible="false" />
    <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
    <asp:Label ID="lblMode" runat="server" Visible="false" />
    <asp:Label ID="lblModuleId" runat="server" Visible="false" />
    <asp:Label ID="lblCustomerGeneralId" runat="server" Visible="false" />
    <asp:Label ID="lblSectionId" runat="server" Text="96" Visible="false" />
    <asp:Label ID="lblActionType" runat="server" Style="display: none" />
</asp:Content>

