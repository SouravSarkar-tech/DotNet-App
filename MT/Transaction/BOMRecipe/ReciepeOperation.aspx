<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/BOMRecipe/BOMRecipeMasterPage.master"
    AutoEventWireup="true" CodeFile="ReciepeOperation.aspx.cs" Inherits="Transaction_BOMRecipe_ReciepeOperation" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="trHeading" align="center" colspan="2">Operations
                    </td>
                </tr>
                <tr>
                    <td class="tdSpace"></td>
                </tr>
                <tr>
                    <td class="rigthTD">
                        <div style="height: auto; overflow-x: auto; width: 950px;">
                            <asp:GridView ID="GvOperation" runat="server" AutoGenerateColumns="False" DataKeyNames="Recipe_Operation_Id,Control_key,StdText_Key,Destinatn,Relevancy_To_Costing,Plant,Act_Operation_UoM,ChargeUnit,OperUnit,Resource,AltResource1,AltResource2,AltResource3,AltResource4,IsKX_Sche"
                                CssClass="GridClass" ShowHeaderWhenEmpty="True" OnRowDataBound="GvOperation_RowDataBound"
                                EmptyDataText="No Data Found" OnRowCommand="GvOperation_RowCommand">
                                <HeaderStyle BackColor="#EDF5FF" />
                                <FooterStyle CssClass="gridFooter" />
                                <RowStyle CssClass="grdViewRow" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Delete Flag">
                                        <ItemTemplate>
                                            <%-- <asp:CheckBox runat="server" ID="chkDeletionFlag" />
                                            <asp:Label ID="lblDeletionFlag" runat="server" Visible="false" Text='<%#Eval("DeletionFlag") %>' />
                                            <asp:HiddenField ID="hdnDeletionFlag" runat="server" Value='<%#Eval("DeletionFlag") %>' />
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="" CommandName="D" Visible="false"
                                                CausesValidation="false">
                                               <img src="../../images/delete.png" alt="Delete" title='Delete'/>
                                            </asp:LinkButton>--%>
                                            <%--Started to Add Remark and Reason textbox. Ticket number 8200064571--%>
                                            <asp:LinkButton ID="lnkdelRecOp" runat="server" CommandName="D" CausesValidation="false">  
                                                                                <img src="../../images/delete.png" alt="Delete" title='Delete'/>
                                            </asp:LinkButton>
                                            <%--ENded to Add Remark and Reason textbox. Ticket number 8200064571--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item" Visible="False">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Enabled="false" />
                                            <asp:HiddenField ID="hdnSelect" runat="server" Value='<%#Eval("Select") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecipe_Operation_Id" runat="server" Text='<%#Eval("Recipe_Operation_Id") %>'
                                                Visible="false" />
                                            <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'
                                                Visible="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOperation_Phase" runat="server" MaxLength="18" Enabled="false"
                                                Width="30px" CssClass="textbox" Text='<%#Eval("Operation_Phase") %>' />
                                            <asp:RequiredFieldValidator ID="reqtxtOperation_Phase" runat="server" ControlToValidate="txtOperation_Phase"
                                                ValidationGroup="save" ErrorMessage="Operation phase cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Operation phase cannot be blank.' />" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phase Indicator">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPI" runat="server" CssClass="chkPI" />
                                            <asp:HiddenField ID="hdnPI" runat="server" Value='<%#Eval("Phase_Indicator") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sup Operation">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSup_Operation" runat="server" Width="30px" CssClass="textbox"
                                                Text='<%#Eval("Sup_Operation") %>' OnTextChanged="txtSup_Operation_TextChanged"
                                                AutoPostBack="true" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="KX_Scheduling">
                                        <ItemTemplate>
                                             <asp:DropDownList ID="ddlchkPIKX" runat="server" CssClass="dropdownlist"
                                                 AutoPostBack="true" OnSelectedIndexChanged="ddlchkPIKX_SelectedIndexChanged">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="reqddlchkPIKX" runat="server" ControlToValidate="ddlchkPIKX"
                                                ValidationGroup="save" ErrorMessage="KX_Scheduling cannot be blank." SetFocusOnError="true"
                                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='KX_Scheduling cannot be blank.' />" />

                                           <%-- <asp:CheckBox ID="chkPIKX" runat="server" CssClass="chkPIKX"
                                                 AutoPostBack="true" OnCheckedChanged="chkPIKX_CheckedChanged" />
                                            <asp:HiddenField ID="hdnPIKX" runat="server" Value='<%#Eval("IsKX_Sche") %>' />--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Destination">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDestination" runat="server" Text='<%#Eval("Destinatn") %>' Enabled="false"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resource">
                                        <ItemTemplate>
                                            <%--<asp:TextBox ID="txtResource" runat="server" CssClass="textbox" Width="50px" Text='<%#Eval("Resource") %>'/>
                                                                                <asp:DropDownList ID="ddlResource" runat="server" CssClass="dropdownlist" OnSelectedIndexChanged="ddlResource_SelectedIndexChanged"
                                                                                    AutoPostBack="true">--%>
                                            <asp:DropDownList ID="ddlResource" runat="server" CssClass="dropdownlist">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlResource" runat="server" ControlToValidate="ddlResource"
                                                ValidationGroup="save" ErrorMessage="Resource cannot be blank." SetFocusOnError="true"
                                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Resource cannot be blank.' />" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <%--PROV-CCP-MM-941-23-0076 --%>
                                    <asp:TemplateField HeaderText="Alt Resource1">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlAltResource1" runat="server" CssClass="dropdownlist">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="cvddlAltResource1" runat="server" Type="String" Display="Dynamic"
                                                ValidationGroup="save" ControlToCompare="ddlResource" ControlToValidate="ddlAltResource1"
                                                ErrorMessage="Resource and alt. resource cannot be blank." Operator="NotEqual"
                                                Text="<img src='../../images/Error.png' />" ToolTip="Resource and alt. resource cannot be blank."></asp:CompareValidator>

                                            <%-- <asp:RequiredFieldValidator ID="reqddlAltResource1" runat="server" ControlToValidate="ddlAltResource1"
                                                    ValidationGroup="save" ErrorMessage="Alt Resource1 cannot be blank." SetFocusOnError="true"
                                                    Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt Resource1 cannot be blank.' />" />
                                            --%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Alt Resource2">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlAltResource2" runat="server" CssClass="dropdownlist">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="cvddlAltResource2" runat="server" Type="String" Display="Dynamic"
                                                ValidationGroup="save" ControlToCompare="ddlResource" ControlToValidate="ddlAltResource2"
                                                ErrorMessage="Resource and alt. resource cannot be blank." Operator="NotEqual"
                                                Text="<img src='../../images/Error.png' />" ToolTip="Resource and alt. resource cannot be blank."></asp:CompareValidator>
                                            <asp:CompareValidator ID="cvddlAltResource22" runat="server" Type="String" Display="Dynamic"
                                                ValidationGroup="save" ControlToCompare="ddlAltResource1" ControlToValidate="ddlAltResource2"
                                                ErrorMessage="Resource and alt. resource cannot be blank." Operator="NotEqual"
                                                Text="<img src='../../images/Error.png' />" ToolTip="Resource and alt. resource cannot be blank."></asp:CompareValidator>

                                            <%-- <asp:RequiredFieldValidator ID="reqddlAltResource2" runat="server" ControlToValidate="ddlAltResource2"
                                                    ValidationGroup="save" ErrorMessage="Alt Resource2 cannot be blank." SetFocusOnError="true"
                                                    Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt Resource2 cannot be blank.' />" />
                                            --%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Alt Resource3">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlAltResource3" runat="server" CssClass="dropdownlist">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="cvddlAltResource3" runat="server" Type="String" Display="Dynamic"
                                                ValidationGroup="save" ControlToCompare="ddlResource" ControlToValidate="ddlAltResource3"
                                                ErrorMessage="Resource and alt. resource cannot be blank." Operator="NotEqual"
                                                Text="<img src='../../images/Error.png' />" ToolTip="Resource and alt. resource cannot be blank."></asp:CompareValidator>
                                            <asp:CompareValidator ID="cvddlAltResource33" runat="server" Type="String" Display="Dynamic"
                                                ValidationGroup="save" ControlToCompare="ddlAltResource1" ControlToValidate="ddlAltResource3"
                                                ErrorMessage="Resource and alt. resource cannot be blank." Operator="NotEqual"
                                                Text="<img src='../../images/Error.png' />" ToolTip="Resource and alt. resource cannot be blank."></asp:CompareValidator>
                                            <asp:CompareValidator ID="cvddlAltResource333" runat="server" Type="String" Display="Dynamic"
                                                ValidationGroup="save" ControlToCompare="ddlAltResource2" ControlToValidate="ddlAltResource3"
                                                ErrorMessage="Resource and alt. resource cannot be blank." Operator="NotEqual"
                                                Text="<img src='../../images/Error.png' />" ToolTip="Resource and alt. resource cannot be blank."></asp:CompareValidator>

                                            <%-- <asp:RequiredFieldValidator ID="reqddlAltResource3" runat="server" ControlToValidate="ddlAltResource3"
                                                    ValidationGroup="save" ErrorMessage="Alt Resource3 cannot be blank." SetFocusOnError="true"
                                                    Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt Resource3 cannot be blank.' />" />
                                            --%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Alt Resource4">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlAltResource4" runat="server" CssClass="dropdownlist">
                                            </asp:DropDownList>
                                             <asp:CompareValidator ID="cvddlAltResource4" runat="server" Type="String" Display="Dynamic"
                                                ValidationGroup="save" ControlToCompare="ddlResource" ControlToValidate="ddlAltResource4"
                                                ErrorMessage="Resource and alt. resource cannot be blank." Operator="NotEqual"
                                                Text="<img src='../../images/Error.png' />" ToolTip="Resource and alt. resource cannot be blank."></asp:CompareValidator>
                                            <asp:CompareValidator ID="cvddlAltResource44" runat="server" Type="String" Display="Dynamic"
                                                ValidationGroup="save" ControlToCompare="ddlAltResource1" ControlToValidate="ddlAltResource4"
                                                ErrorMessage="Resource and alt. resource cannot be blank." Operator="NotEqual"
                                                Text="<img src='../../images/Error.png' />" ToolTip="Resource and alt. resource cannot be blank."></asp:CompareValidator>
                                            <asp:CompareValidator ID="cvddlAltResource444" runat="server" Type="String" Display="Dynamic"
                                                ValidationGroup="save" ControlToCompare="ddlAltResource2" ControlToValidate="ddlAltResource4"
                                                ErrorMessage="Resource and alt. resource cannot be blank." Operator="NotEqual"
                                                Text="<img src='../../images/Error.png' />" ToolTip="Resource and alt. resource cannot be blank."></asp:CompareValidator>
                                             <asp:CompareValidator ID="cvddlAltResource4444" runat="server" Type="String" Display="Dynamic"
                                                ValidationGroup="save" ControlToCompare="ddlAltResource3" ControlToValidate="ddlAltResource4"
                                                ErrorMessage="Resource and alt. resource cannot be blank." Operator="NotEqual"
                                                Text="<img src='../../images/Error.png' />" ToolTip="Resource and alt. resource cannot be blank."></asp:CompareValidator>

                                            <%-- <asp:RequiredFieldValidator ID="reqddlAltResource4" runat="server" ControlToValidate="ddlAltResource4"
                                                    ValidationGroup="save" ErrorMessage="Alt Resource4 cannot be blank." SetFocusOnError="true"
                                                    Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt Resource4 cannot be blank.' />" />
                                            --%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <%--PROV-CCP-MM-941-23-0076 --%>


                                    <asp:TemplateField HeaderText="Control key">
                                        <ItemTemplate>
                                            <%--<asp:DropDownList ID="ddlControlKey" CssClass="dropdownlist" runat="server" OnSelectedIndexChanged="ddlControlKey_SelectedIndexChanged"
                                                                                    AutoPostBack="true">--%>
                                            <asp:DropDownList ID="ddlControlKey" CssClass="dropdownlist" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlControlKey" runat="server" ControlToValidate="ddlControlKey"
                                                ValidationGroup="save" ErrorMessage="Control Key cannot be blank." SetFocusOnError="true"
                                                 Enabled="false" InitialValue="0" Display="Dynamic" Text="<img src='../../images/Error.png' title='Control Key cannot be blank.' />" />
                                            <%--<asp:Label ID="lblControlKey" runat="server" Text='<%#Eval("Control_key") %>' Visible="false" />--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Standard text Key">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlStdTextKey" CssClass="dropdownlist" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlStdTextKey" runat="server" ControlToValidate="ddlStdTextKey"
                                                ValidationGroup="save" ErrorMessage="Resource cannot be blank." SetFocusOnError="true"
                                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Resource cannot be blank.' />" />
                                            <asp:Label ID="lblFieldKey" runat="server" Text='<%#Eval("StdText_Key") %>' Visible="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Operation Text">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox" MaxLength="40"
                                                Width="270px" Text='<%#Eval("Description") %>' />

                                            <asp:RequiredFieldValidator ID="reqtxtDescription" runat="server" ControlToValidate="txtDescription"
                                                ValidationGroup="save" ErrorMessage="Operation Text cannot be blank." SetFocusOnError="true"
                                                Display="Dynamic" Text="<img src='../../images/Error.png' title='Operation Text cannot be blank.' />" />

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Relevancy to Costing Indicator" Visible="False">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlIndicatorRelavancyToCosting" runat="server" AppendDataBoundItems="false">
                                                <asp:ListItem Text="Select" Value="0" />
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Base Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBase_Quantity" runat="server" CssClass="textbox" onkeypress="return IsNumber();"
                                                Width="70px" Text='<%#Eval("Base_Quantity") %>' />
                                            <asp:RequiredFieldValidator ID="reqtxtBase_Quantity" runat="server" ControlToValidate="txtBase_Quantity"
                                                Enabled="false" ValidationGroup="save" ErrorMessage="Operation Base Quantity cannot be blank."
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Operation Base Quantity cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="regtxtBase_Quantity" ControlToValidate="txtBase_Quantity"
                                                runat="server" ErrorMessage="Operation Base Quantity should have numeric value up to 3 decimal place only."
                                                Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                Text="<img src='../../images/Error.png' title='Operation Base Quantity should have numeric value up to 3 decimal place only.' />" />
                                            <asp:CompareValidator ID="cmptxtBase_Quantity" runat="server" ValueToCompare="0"
                                                ControlToValidate="txtBase_Quantity" ErrorMessage="Operation base Quantity should be greater than zero."
                                                Operator="GreaterThan" Display="Dynamic" ValidationGroup="save" SetFocusOnError="true"
                                                Type="Double" Text="<img src='../../images/Error.png' title='Operation Base Quantity should be greater than zero.' />" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Act/Operation UOM">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlAct_Operation_UoM" runat="server" CssClass="dropdownlist"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlAct_Operation_UoM_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqddlAct_Operation_UoM" runat="server" ControlToValidate="ddlAct_Operation_UoM"
                                                ValidationGroup="save" ErrorMessage="Operation UOM cannot be blank." SetFocusOnError="true"
                                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Operation UOM cannot be blank.' />" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="1st Std Value">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFirst_Std_Value" runat="server" CssClass="textbox " onkeypress="return IsNumber();"
                                                Width="55px" Text='<%#Eval("First_Std_Value") %>' Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtFirst_Std_Value" runat="server" ControlToValidate="txtFirst_Std_Value"
                                                ValidationGroup="save" ErrorMessage="First Std Value cannot be blank." SetFocusOnError="true"
                                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='First Std Value cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="regtxtFirst_Std_Value" ControlToValidate="txtFirst_Std_Value"
                                                runat="server" ErrorMessage="First Std Value should have numeric value up to 3 decimal place only."
                                                Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                Text="<img src='../../images/Error.png' title='First Std Value should have numeric value up to 3 decimal place only.' />" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Standard Value Unit">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFirst_Std_Value_Unit" runat="server" CssClass="textbox" MaxLength="3"
                                                Width="70px" Text='<%#Eval("First_Std_Value_Unit") %>' Enabled="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="2nd Std Value">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSec_Std_Value" runat="server" CssClass="textbox" onkeypress="return IsNumber();"
                                                Width="55px" Text='<%#Eval("Sec_Std_Value") %>' Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtSec_Std_Value" runat="server" ControlToValidate="txtSec_Std_Value"
                                                ValidationGroup="save" ErrorMessage="Second Std Value cannot be blank." SetFocusOnError="true"
                                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Second Std Value cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="regtxtSec_Std_Value" ControlToValidate="txtSec_Std_Value"
                                                runat="server" ErrorMessage="Second Std Value should have numeric value up to 3 decimal place only."
                                                Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                Text="<img src='../../images/Error.png' title='Second Std Value should have numeric value up to 3 decimal place only.' />" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Standard Value Unit">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSec_Std_Value_Unit" runat="server" CssClass="textbox" MaxLength="3"
                                                Width="70px" Text='<%#Eval("Sec_Std_Value_Unit") %>' Enabled="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="3rd Std Value">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtThird_Std_Value" runat="server" CssClass="textbox" onkeypress="return IsNumber();"
                                                Width="55px" Text='<%#Eval("Third_Std_Value") %>' Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtThird_Std_Value" runat="server" ControlToValidate="txtThird_Std_Value"
                                                ValidationGroup="save" ErrorMessage="Third Std Value cannot be blank." SetFocusOnError="true"
                                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='Third Std Value cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="regtxtThird_Std_Value" ControlToValidate="txtThird_Std_Value"
                                                runat="server" ErrorMessage="Third Std Value should have numeric value up to 3 decimal place only."
                                                Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                Text="<img src='../../images/Error.png' title='Third Std Value should have numeric value up to 3 decimal place only.' />" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Standard Value Unit">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtThird_Std_Value_Unit" runat="server" CssClass="textbox" MaxLength="3"
                                                Width="70px" Text='<%#Eval("Third_Std_Value_Unit") %>' Enabled="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <%--PROV-CCP-MM-941-23-0076 --%>
                                    <asp:TemplateField HeaderText="4th Std Value">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtStd_Value_4" runat="server" CssClass="textbox" onkeypress="return IsNumber();"
                                                Width="55px" Text='<%#Eval("Std_Value_4") %>' Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtStd_Value_4" runat="server" ControlToValidate="txtStd_Value_4"
                                                ValidationGroup="save" ErrorMessage="4th Std Value cannot be blank." SetFocusOnError="true"
                                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='4th Std Value cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="regtxtStd_Value_4" ControlToValidate="txtStd_Value_4"
                                                runat="server" ErrorMessage="4th Std Value should have numeric value up to 3 decimal place only."
                                                Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                Text="<img src='../../images/Error.png' title='4th Std Value should have numeric value up to 3 decimal place only.' />" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Standard Value Unit">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtStd_Value_Unit_4" runat="server" CssClass="textbox" MaxLength="3"
                                                Width="70px" Text='<%#Eval("Std_Value_Unit_4") %>' Enabled="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="5th Std Value">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtStd_Value_5" runat="server" CssClass="textbox" onkeypress="return IsNumber();"
                                                Width="55px" Text='<%#Eval("Std_Value_5") %>' Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtStd_Value_5" runat="server" ControlToValidate="txtStd_Value_5"
                                                ValidationGroup="save" ErrorMessage="5th Std Value cannot be blank." SetFocusOnError="true"
                                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='5th Std Value cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="regtxtStd_Value_5" ControlToValidate="txtStd_Value_5"
                                                runat="server" ErrorMessage="5th Std Value should have numeric value up to 3 decimal place only."
                                                Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                Text="<img src='../../images/Error.png' title='5th Std Value should have numeric value up to 3 decimal place only.' />" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Standard Value Unit">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtStd_Value_Unit_5" runat="server" CssClass="textbox" MaxLength="3"
                                                Width="70px" Text='<%#Eval("Std_Value_Unit_4") %>' Enabled="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="6th Std Value">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtStd_Value_6" runat="server" CssClass="textbox" onkeypress="return IsNumber();"
                                                Width="55px" Text='<%#Eval("Std_Value_6") %>' Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtStd_Value_6" runat="server" ControlToValidate="txtStd_Value_6"
                                                ValidationGroup="save" ErrorMessage="6th Std Value cannot be blank." SetFocusOnError="true"
                                                Enabled="false" Display="Dynamic" Text="<img src='../../images/Error.png' title='6th Std Value cannot be blank.' />" />
                                            <asp:RegularExpressionValidator ID="regtxtStd_Value_6" ControlToValidate="txtStd_Value_6"
                                                runat="server" ErrorMessage="6th Std Value should have numeric value up to 3 decimal place only."
                                                Display="Dynamic" ValidationGroup="save" SetFocusOnError="true" ValidationExpression="^[0-9]*(\.[0-9]{1,3})?$"
                                                Text="<img src='../../images/Error.png' title='6th Std Value should have numeric value up to 3 decimal place only.' />" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Standard Value Unit">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtStd_Value_Unit_6" runat="server" CssClass="textbox" MaxLength="3"
                                                Width="70px" Text='<%#Eval("Std_Value_Unit_6") %>' Enabled="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <%--PROV-CCP-MM-941-23-0076 --%>


                                    <asp:TemplateField HeaderText="Plant" Visible="False">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="false">
                                                <asp:ListItem Text="Select" Value="" />
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Charge Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtChargeQty" runat="server" CssClass="textbox" Width="70px" Text='<%#Eval("ChargeQty") %>'
                                                Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtChargeQty" runat="server" ControlToValidate="txtChargeQty"
                                                ValidationGroup="save" ErrorMessage="Operation Charge Quantity cannot be blank."
                                                Enabled="false" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Operation Charge Quantity cannot be blank.' />" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Charge Unit">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlChngeCUnit" runat="server" Enabled="false">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Operation Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtOperQty" runat="server" CssClass="textbox" Width="70px" Text='<%#Eval("OperQty") %>'
                                                Enabled="false" />
                                            <asp:RequiredFieldValidator ID="reqtxtOperQty" runat="server" ControlToValidate="txtOperQty"
                                                ValidationGroup="save" ErrorMessage="Operation Quantity cannot be blank." Enabled="false"
                                                SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Operation Quantity cannot be blank.' />" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Operation Unit">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlChngeOUnit" runat="server" Enabled="false">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDeleteFlagUDI" Text='<%#Eval("DFlagValue") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <%--PROV-CCP-MM-941-23-0076 --%>
                                    <asp:TemplateField HeaderText="Class Type">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtClass_type" runat="server" CssClass="textbox" MaxLength="3"
                                                Width="150px" Text='<%#Eval("Class_type") %>' Enabled="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WC Area">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtWC_Area" runat="server" CssClass="textbox" MaxLength="3"
                                                Width="70px" Text='<%#Eval("WC_Area") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WC Area Grp">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtWC_Area_grp" runat="server" CssClass="textbox" MaxLength="3"
                                                Width="70px" Text='<%#Eval("WC_Area_grp") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <%--PROV-CCP-MM-941-23-0076 --%>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td valign="middle" align="left">
                                    <asp:Button ID="btnInsertRecord" runat="server" Width="90px" CssClass="button" Text="New Row"
                                        ValidationGroup="ValgrpCust" CommandName="Insert" OnClick="btnInsertRecord_Click"
                                        Visible="False" />
                                </td>
                                <td valign="middle" align="left">
                                    <asp:Button ID="btnOpSave" runat="server" Width="90px" CssClass="button" Text="Apply"
                                        ValidationGroup="ValgrpCust" CommandName="Save" OnClick="btnOpSave_Click" />
                                </td>
                                <td valign="middle" align="left">
                                    <asp:Button ID="btnMissingResource" runat="server" Width="90px" CssClass="button" Text="Get Missing Resource"
                                        ValidationGroup="getmissres" CommandName="GetMResource"
                                        OnClick="btnMissingResource_Click" />
                                </td>
                                <td valign="middle" align="left" id="idtxtResource" visible="false" runat="server">
                                    <asp:TextBox ID="txtResource" runat="server" CssClass="textbox" MaxLength="18"
                                        Width="180" />
                                    <%--    <asp:RequiredFieldValidator ID="reqtxtResource" runat="server" ControlToValidate="txtResource"
                                        ValidationGroup="getmissres" ErrorMessage="Resource cannot be blank." SetFocusOnError="true"
                                        Display="Dynamic" Text="<img src='../../images/Error.png' title='Resource cannot be blank.' />" />--%>


                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdSpace" colspan="2"></td>
                </tr>

                <%--ITSM413605--%>
                <tr runat="server" colspan="2" id="trSecResfile" visible="false">
                    <td align="left" class="leftTD">Browse file 
                            <asp:HyperLink ID="hlMSImportFormat" runat="server" Text="click here" Target="_blank"></asp:HyperLink>

                        to download format

                              <asp:UpdatePanel ID="UupdtUpload" runat="server">
                                  <ContentTemplate>
                                      <asp:FileUpload ID="fileUpdsecResorce" runat="server" TabIndex="1" />
                                  </ContentTemplate>
                                  <Triggers>
                                      <asp:PostBackTrigger ControlID="btnMSProcess" />
                                  </Triggers>
                              </asp:UpdatePanel>
                    </td>
                </tr>

                <tr id="trgrdAtt" runat="server" visible="false">
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

                <tr id="trButtonuf" runat="server" colspan="2" visible="false">
                    <td class="centerTD" colspan="2">
                        <asp:Button ID="btnMSProcess" runat="server" OnClick="btnMSProcess_Click" Text="Upload" CssClass="button" />
                        <%--<asp:Button ID="btnSRValidation" runat="server" OnClick="btnSRValidation_Click" Text="Validation" CssClass="button" />--%>
                        <asp:Button ID="btnSRSubmit" runat="server" OnClick="btnSRSubmit_Click" Text="Submit" CssClass="button" />
                    </td>
                </tr>

                <%--ITSM413605--%>
                <tr>
                    <td class="tdSpace" colspan="2"></td>
                </tr>

                <tr id="trButton" runat="server" visible="false">
                    <td class="centerTD" colspan="2">
                        <asp:Button ID="btnPrevious" runat="server" Text="Back" ValidationGroup="save" TabIndex="26"
                            CssClass="button" OnClick="btnPrevious_Click" />
                        <asp:Button ID="btnSave" runat="server" ValidationGroup="save" UseSubmitBehavior="true"
                            Text="Save" CssClass="button" TabIndex="27" OnClick="btnSave_Click" />
                        <asp:Button ID="btnNext" runat="server" ValidationGroup="save" Text="Save & Next"
                            TabIndex="28" UseSubmitBehavior="true" CssClass="button" OnClick="btnNext_Click"
                            Width="120px" />
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblRecipeId" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
            <asp:Label ID="lblSectionId" runat="server" Text="85" Visible="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
