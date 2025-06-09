<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/Material/MaterialMasterPage.master"
    AutoEventWireup="true" CodeFile="BasicData2.aspx.cs" Inherits="Transaction_BasicData2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdMaterialBasic" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                <asp:Label ID="lblMsg" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlAddNew" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="trHeading" align="center" colspan="2">Basic Data 2
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="2"></td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD" style="width: 20%">Material Number
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:Label ID="lblMaterialNumber" runat="server" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">Material Type
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:Label ID="lblMaterialType" runat="server" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">Industry Sector
                                    </td>
                                    <td class="rigthTD" colspan="3">
                                        <asp:Label ID="lblIndstrySector" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">&nbsp;&nbsp;Unit Of Measure : (Conversion Factor)
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td class="leftTD" style="width: 28%">Alternative Unit Value X
                                                </td>
                                                <td class="leftTD" style="width: 36%">Alternative Unit Of Measurement
                                                </td>
                                                <td class="leftTD">Alternative Unit Value Y
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rigthTD" style="width: 28%">
                                                    <b>Example -></b> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1
                                                </td>
                                                <td class="rigthTD" style="width: 36%">TS-Thousand
                                                </td>
                                                <td class="rigthTD">1000 EA - Each
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rigthTD">
                                                    <asp:Label ID="labletxtAltUnitValueX" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:TextBox ID="txtAltUnitValueX" runat="server" CssClass="textbox" Width="150"
                                                        TabIndex="1" MaxLength="5" onkeypress="return IsNumber();" />
                                                    <asp:RequiredFieldValidator ID="reqtxtAltUnitValueX" runat="server" ControlToValidate="txtAltUnitValueX"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Value X cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Value X cannot be blank.' />" />
                                                    <asp:RangeValidator ID="rngtxtAltUnitValueX" runat="server" ControlToValidate="txtAltUnitValueX"
                                                        Type="Integer" ValidationGroup="BasicData" ErrorMessage="Invalid Alt. Unit Value X."
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="99999" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Invalid Alt. Unit Value X.' />" />
                                                </td>
                                                <td class="leftTD">
                                                    <asp:Label ID="lableddlAltUnitOfMeasure" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="ddlAltUnitOfMeasure" runat="server" AppendDataBoundItems="false"
                                                        AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlAltUnitOfMeasure_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="" />
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqddlAltUnitOfMeasure" runat="server" ControlToValidate="ddlAltUnitOfMeasure"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Of Measure cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Of Measure cannot be blank.' />" />
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure" runat="server" Type="String" Display="Dynamic"
                                                        ValidationGroup="BasicData" ValueToCompare="0" ControlToValidate="ddlAltUnitOfMeasure"
                                                        ErrorMessage="Alternate Units cannot be same as Base UOM." Operator="NotEqual"
                                                        Text="<img src='../../images/Error.png' title='Alternate Units cannot be same as Base UOM.' />"></asp:CompareValidator>
                                                </td>
                                                <td class="rigthTD">
                                                    <asp:Label ID="labletxtAltUnitValueY" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:TextBox ID="txtAltUnitValueY" runat="server" CssClass="textbox" Width="150"
                                                        TabIndex="3" MaxLength="5" onkeypress="return IsNumber();" />
                                                    <asp:RequiredFieldValidator ID="reqtxtAltUnitValueY" runat="server" ControlToValidate="txtAltUnitValueY"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Value Y cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Value Y cannot be blank.' />" />
                                                    <asp:RangeValidator ID="rngtxtAltUnitValueY" runat="server" ControlToValidate="txtAltUnitValueY"
                                                        Type="Integer" ValidationGroup="BasicData" ErrorMessage="Invalid Alt. Unit Value Y."
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="99999" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Invalid Alt. Unit Value Y.' />" />
                                                    <asp:Label ID="lblBaseUnit1" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rigthTD">
                                                    <asp:Label ID="labletxtAltUnitValueX1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:TextBox ID="txtAltUnitValueX1" runat="server" CssClass="textbox" Width="150"
                                                        TabIndex="4" MaxLength="5" onkeypress="return IsNumber();" />
                                                    <asp:RequiredFieldValidator ID="reqtxtAltUnitValueX1" runat="server" ControlToValidate="txtAltUnitValueX1"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Value X cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Value X cannot be blank.' />" />
                                                    <asp:RangeValidator ID="rngtxtAltUnitValueX1" runat="server" ControlToValidate="txtAltUnitValueX1"
                                                        Type="Integer" ValidationGroup="BasicData" ErrorMessage="Invalid Alt. Unit Value X."
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="99999" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Invalid Alt. Unit Value X.' />" />
                                                </td>
                                                <td class="leftTD">
                                                    <asp:Label ID="lableddlAltUnitOfMeasure1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="ddlAltUnitOfMeasure1" runat="server" AppendDataBoundItems="false"
                                                        AutoPostBack="true" TabIndex="5" OnSelectedIndexChanged="ddlAltUnitOfMeasure1_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="" />
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqddlAltUnitOfMeasure1" runat="server" ControlToValidate="ddlAltUnitOfMeasure1"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Of Measure cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Of Measure cannot be blank.' />" />
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure11" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure"
                                                        ControlToValidate="ddlAltUnitOfMeasure1" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure1" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ValueToCompare="0" ControlToValidate="ddlAltUnitOfMeasure1"
                                                        ErrorMessage="Alternate Units cannot be same as Base UOM." Operator="NotEqual"
                                                        Text="<img src='../../images/Error.png' title='Alternate Units cannot be same as Base UOM.' />"></asp:CompareValidator>
                                                </td>
                                                <td class="rigthTD">
                                                    <asp:Label ID="labletxtAltUnitValueY1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:TextBox ID="txtAltUnitValueY1" runat="server" CssClass="textbox" Width="150"
                                                        TabIndex="6" MaxLength="5" onkeypress="return IsNumber();" />
                                                    <asp:RequiredFieldValidator ID="reqtxtAltUnitValueY1" runat="server" ControlToValidate="txtAltUnitValueY1"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Value Y cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Value Y cannot be blank.' />" />
                                                    <asp:RangeValidator ID="rngtxtAltUnitValueY1" runat="server" ControlToValidate="txtAltUnitValueY1"
                                                        Type="Integer" ValidationGroup="BasicData" ErrorMessage="Invalid Alt. Unit Value Y."
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="99999" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Invalid Alt. Unit Value Y.' />" />
                                                    <asp:Label ID="lblBaseUnit2" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rigthTD">
                                                    <asp:Label ID="labletxtAltUnitValueX2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:TextBox ID="txtAltUnitValueX2" runat="server" CssClass="textbox" Width="150"
                                                        TabIndex="7" MaxLength="5" onkeypress="return IsNumber();" />
                                                    <asp:RequiredFieldValidator ID="reqtxtAltUnitValueX2" runat="server" ControlToValidate="txtAltUnitValueX2"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Value X cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Value X cannot be blank.' />" />
                                                    <asp:RangeValidator ID="rngtxtAltUnitValueX2" runat="server" ControlToValidate="txtAltUnitValueX2"
                                                        Type="Integer" ValidationGroup="BasicData" ErrorMessage="Invalid Alt. Unit Value X."
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="99999" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Invalid Alt. Unit Value X.' />" />
                                                </td>
                                                <td class="leftTD">
                                                    <asp:Label ID="lableddlAltUnitOfMeasure2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="ddlAltUnitOfMeasure2" runat="server" AppendDataBoundItems="false"
                                                        AutoPostBack="true" TabIndex="8" OnSelectedIndexChanged="ddlAltUnitOfMeasure2_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="" />
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqddlAltUnitOfMeasure2" runat="server" ControlToValidate="ddlAltUnitOfMeasure2"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Of Measure cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Of Measure cannot be blank.' />" />
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure2" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ValueToCompare="0" ControlToValidate="ddlAltUnitOfMeasure2"
                                                        ErrorMessage="Alternate Units cannot be same as Base UOM." Operator="NotEqual"
                                                        Text="<img src='../../images/Error.png' title='Alternate Units cannot be same as Base UOM.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure21" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure1"
                                                        ControlToValidate="ddlAltUnitOfMeasure2" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure22" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure"
                                                        ControlToValidate="ddlAltUnitOfMeasure2" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                </td>
                                                <td class="rigthTD">
                                                    <asp:Label ID="labletxtAltUnitValueY2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:TextBox ID="txtAltUnitValueY2" runat="server" CssClass="textbox" Width="150"
                                                        TabIndex="9" MaxLength="5" onkeypress="return IsNumber();" />
                                                    <asp:RequiredFieldValidator ID="reqtxtAltUnitValueY2" runat="server" ControlToValidate="txtAltUnitValueY2"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Value Y cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Value Y cannot be blank.' />" />
                                                    <asp:RangeValidator ID="rngtxtAltUnitValueY2" runat="server" ControlToValidate="txtAltUnitValueY2"
                                                        Type="Integer" ValidationGroup="BasicData" ErrorMessage="Invalid Alt. Unit Value Y."
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="99999" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Invalid Alt. Unit Value Y.' />" />
                                                    <asp:Label ID="lblBaseUnit3" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="rigthTD">
                                                    <asp:Label ID="labletxtAltUnitValueX3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:TextBox ID="txtAltUnitValueX3" runat="server" CssClass="textbox" Width="150"
                                                        TabIndex="7" MaxLength="5" onkeypress="return IsNumber();" />
                                                    <asp:RequiredFieldValidator ID="reqtxtAltUnitValueX3" runat="server" ControlToValidate="txtAltUnitValueX3" Visible="false"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Value X cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Value X cannot be blank.' />" />
                                                    <asp:RangeValidator ID="rngtxtAltUnitValueX3" runat="server" ControlToValidate="txtAltUnitValueX3"
                                                        Type="Integer" ValidationGroup="BasicData" ErrorMessage="Invalid Alt. Unit Value X."
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="99999" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Invalid Alt. Unit Value X.' />" />
                                                </td>
                                                <td class="leftTD">
                                                    <asp:Label ID="lableddlAltUnitOfMeasure3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="ddlAltUnitOfMeasure3" runat="server" AppendDataBoundItems="false"
                                                        AutoPostBack="true" TabIndex="8" OnSelectedIndexChanged="ddlAltUnitOfMeasure3_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="" />
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqddlAltUnitOfMeasure3" runat="server" ControlToValidate="ddlAltUnitOfMeasure3"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Of Measure cannot be blank."
                                                        Visible="false" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Of Measure cannot be blank.' />" />
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure3" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ValueToCompare="0" ControlToValidate="ddlAltUnitOfMeasure3"
                                                        ErrorMessage="Alternate Units cannot be same as Base UOM." Operator="NotEqual"
                                                        Text="<img src='../../images/Error.png' title='Alternate Units cannot be same as Base UOM.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure31" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure1"
                                                        ControlToValidate="ddlAltUnitOfMeasure3" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure32" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure"
                                                        ControlToValidate="ddlAltUnitOfMeasure3" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure33" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure2"
                                                        ControlToValidate="ddlAltUnitOfMeasure3" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                </td>
                                                <td class="rigthTD">
                                                    <asp:Label ID="labletxtAltUnitValueY3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:TextBox ID="txtAltUnitValueY3" runat="server" CssClass="textbox" Width="150"
                                                        TabIndex="9" MaxLength="5" onkeypress="return IsNumber();" />
                                                    <asp:RequiredFieldValidator ID="reqtxtAltUnitValueY3" runat="server" ControlToValidate="txtAltUnitValueY3"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Value Y cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Value Y cannot be blank.' />" />
                                                    <asp:RangeValidator ID="rngtxtAltUnitValueY3" runat="server" ControlToValidate="txtAltUnitValueY3"
                                                        Type="Integer" ValidationGroup="BasicData" ErrorMessage="Invalid Alt. Unit Value Y."
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="99999" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Invalid Alt. Unit Value Y.' />" />
                                                    <asp:Label ID="lblBaseUnit33" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="rigthTD">
                                                    <asp:Label ID="labletxtAltUnitValueX4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:TextBox ID="txtAltUnitValueX4" runat="server" CssClass="textbox" Width="150"
                                                        TabIndex="7" MaxLength="5" onkeypress="return IsNumber();" />
                                                    <asp:RequiredFieldValidator ID="reqtxtAltUnitValueX4" runat="server" ControlToValidate="txtAltUnitValueX4"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Value X cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Value X cannot be blank.' />" />
                                                    <asp:RangeValidator ID="rngtxtAltUnitValueX4" runat="server" ControlToValidate="txtAltUnitValueX4"
                                                        Type="Integer" ValidationGroup="BasicData" ErrorMessage="Invalid Alt. Unit Value X."
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="99999" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Invalid Alt. Unit Value X.' />" />
                                                </td>
                                                <td class="leftTD">
                                                    <asp:Label ID="lableddlAltUnitOfMeasure4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="ddlAltUnitOfMeasure4" runat="server" AppendDataBoundItems="false"
                                                        AutoPostBack="true" TabIndex="8" OnSelectedIndexChanged="ddlAltUnitOfMeasure4_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="" />
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqddlAltUnitOfMeasure4" runat="server" ControlToValidate="ddlAltUnitOfMeasure4"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Of Measure cannot be blank."
                                                       Visible="false" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Of Measure cannot be blank.' />" />
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure4" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ValueToCompare="0" ControlToValidate="ddlAltUnitOfMeasure4"
                                                        ErrorMessage="Alternate Units cannot be same as Base UOM." Operator="NotEqual"
                                                        Text="<img src='../../images/Error.png' title='Alternate Units cannot be same as Base UOM.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure41" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure1"
                                                        ControlToValidate="ddlAltUnitOfMeasure4" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure42" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure"
                                                        ControlToValidate="ddlAltUnitOfMeasure4" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure43" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure2"
                                                        ControlToValidate="ddlAltUnitOfMeasure4" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure44" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure3"
                                                        ControlToValidate="ddlAltUnitOfMeasure4" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                </td>
                                                <td class="rigthTD">
                                                    <asp:Label ID="labletxtAltUnitValueY4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:TextBox ID="txtAltUnitValueY4" runat="server" CssClass="textbox" Width="150"
                                                        TabIndex="9" MaxLength="5" onkeypress="return IsNumber();" />
                                                    <asp:RequiredFieldValidator ID="reqtxtAltUnitValueY4" runat="server" ControlToValidate="txtAltUnitValueY4"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Value Y cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Value Y cannot be blank.' />" />
                                                    <asp:RangeValidator ID="rngtxtAltUnitValueY4" runat="server" ControlToValidate="txtAltUnitValueY4"
                                                        Type="Integer" ValidationGroup="BasicData" ErrorMessage="Invalid Alt. Unit Value Y."
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="99999" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Invalid Alt. Unit Value Y.' />" />
                                                    <asp:Label ID="lblBaseUnit44" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="rigthTD">
                                                    <asp:Label ID="labletxtAltUnitValueX5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:TextBox ID="txtAltUnitValueX5" runat="server" CssClass="textbox" Width="150"
                                                        TabIndex="7" MaxLength="5" onkeypress="return IsNumber();" />
                                                    <asp:RequiredFieldValidator ID="reqtxtAltUnitValueX5" runat="server" ControlToValidate="txtAltUnitValueX5"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Value X cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Value X cannot be blank.' />" />
                                                    <asp:RangeValidator ID="rngtxtAltUnitValueX5" runat="server" ControlToValidate="txtAltUnitValueX5"
                                                        Type="Integer" ValidationGroup="BasicData" ErrorMessage="Invalid Alt. Unit Value X."
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="99999" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Invalid Alt. Unit Value X.' />" />
                                                </td>
                                                <td class="leftTD">
                                                    <asp:Label ID="lableddlAltUnitOfMeasure5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="ddlAltUnitOfMeasure5" runat="server" AppendDataBoundItems="false"
                                                        AutoPostBack="true" TabIndex="8" OnSelectedIndexChanged="ddlAltUnitOfMeasure5_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="" />
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqddlAltUnitOfMeasure5" runat="server" ControlToValidate="ddlAltUnitOfMeasure5"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Of Measure cannot be blank."
                                                       Visible="false" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Of Measure cannot be blank.' />" />
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure5" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ValueToCompare="0" ControlToValidate="ddlAltUnitOfMeasure5"
                                                        ErrorMessage="Alternate Units cannot be same as Base UOM." Operator="NotEqual"
                                                        Text="<img src='../../images/Error.png' title='Alternate Units cannot be same as Base UOM.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure51" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure1"
                                                        ControlToValidate="ddlAltUnitOfMeasure4" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure52" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure"
                                                        ControlToValidate="ddlAltUnitOfMeasure5" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure53" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure2"
                                                        ControlToValidate="ddlAltUnitOfMeasure5" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure54" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure3"
                                                        ControlToValidate="ddlAltUnitOfMeasure5" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure55" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure4"
                                                        ControlToValidate="ddlAltUnitOfMeasure5" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                </td>
                                                <td class="rigthTD">
                                                    <asp:Label ID="labletxtAltUnitValueY5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:TextBox ID="txtAltUnitValueY5" runat="server" CssClass="textbox" Width="150"
                                                        TabIndex="9" MaxLength="5" onkeypress="return IsNumber();" />
                                                    <asp:RequiredFieldValidator ID="reqtxtAltUnitValueY5" runat="server" ControlToValidate="txtAltUnitValueY5"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Value Y cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Value Y cannot be blank.' />" />
                                                    <asp:RangeValidator ID="rngtxtAltUnitValueY5" runat="server" ControlToValidate="txtAltUnitValueY5"
                                                        Type="Integer" ValidationGroup="BasicData" ErrorMessage="Invalid Alt. Unit Value Y."
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="99999" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Invalid Alt. Unit Value Y.' />" />
                                                    <asp:Label ID="lblBaseUnit55" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="rigthTD">
                                                    <asp:Label ID="labletxtAltUnitValueX6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:TextBox ID="txtAltUnitValueX6" runat="server" CssClass="textbox" Width="150"
                                                        TabIndex="7" MaxLength="5" onkeypress="return IsNumber();" />
                                                    <asp:RequiredFieldValidator ID="reqtxtAltUnitValueX6" runat="server" ControlToValidate="txtAltUnitValueX6"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Value X cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Value X cannot be blank.' />" />
                                                    <asp:RangeValidator ID="rngtxtAltUnitValueX6" runat="server" ControlToValidate="txtAltUnitValueX6"
                                                        Type="Integer" ValidationGroup="BasicData" ErrorMessage="Invalid Alt. Unit Value X."
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="99999" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Invalid Alt. Unit Value X.' />" />
                                                </td>
                                                <td class="leftTD">
                                                    <asp:Label ID="lableddlAltUnitOfMeasure6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="ddlAltUnitOfMeasure6" runat="server" AppendDataBoundItems="false"
                                                        AutoPostBack="true" TabIndex="8" OnSelectedIndexChanged="ddlAltUnitOfMeasure6_SelectedIndexChanged">
                                                        <asp:ListItem Text="Select" Value="" />
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqddlAltUnitOfMeasure6" runat="server" ControlToValidate="ddlAltUnitOfMeasure6"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Of Measure cannot be blank."
                                                       Visible="false" SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Of Measure cannot be blank.' />" />
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure6" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ValueToCompare="0" ControlToValidate="ddlAltUnitOfMeasure6"
                                                        ErrorMessage="Alternate Units cannot be same as Base UOM." Operator="NotEqual"
                                                        Text="<img src='../../images/Error.png' title='Alternate Units cannot be same as Base UOM.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure61" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure1"
                                                        ControlToValidate="ddlAltUnitOfMeasure4" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure62" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure"
                                                        ControlToValidate="ddlAltUnitOfMeasure6" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure63" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure2"
                                                        ControlToValidate="ddlAltUnitOfMeasure6" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure64" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure3"
                                                        ControlToValidate="ddlAltUnitOfMeasure6" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure4"
                                                        ControlToValidate="ddlAltUnitOfMeasure6" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompddlAltUnitOfMeasure66" runat="server" Type="String"
                                                        Display="Dynamic" ValidationGroup="BasicData" ControlToCompare="ddlAltUnitOfMeasure5"
                                                        ControlToValidate="ddlAltUnitOfMeasure6" ErrorMessage="Alternate Units cannot be same."
                                                        Operator="NotEqual" Text="<img src='../../images/Error.png' title='Alternate Units cannot be same.' />"></asp:CompareValidator>
                                                </td>
                                                <td class="rigthTD">
                                                    <asp:Label ID="labletxtAltUnitValueY6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                    <asp:TextBox ID="txtAltUnitValueY6" runat="server" CssClass="textbox" Width="150"
                                                        TabIndex="9" MaxLength="5" onkeypress="return IsNumber();" />
                                                    <asp:RequiredFieldValidator ID="reqtxtAltUnitValueY6" runat="server" ControlToValidate="txtAltUnitValueY6"
                                                        ValidationGroup="BasicData" ErrorMessage="Alt. Unit Value Y cannot be blank."
                                                        SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Value Y cannot be blank.' />" />
                                                    <asp:RangeValidator ID="rngtxtAltUnitValueY6" runat="server" ControlToValidate="txtAltUnitValueY6"
                                                        Type="Integer" ValidationGroup="BasicData" ErrorMessage="Invalid Alt. Unit Value Y."
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="99999" Display="Dynamic"
                                                        Text="<img src='../../images/Error.png' title='Invalid Alt. Unit Value Y.' />" />
                                                    <asp:Label ID="lblBaseUnit66" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>


                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD" style="width: 20%">Length (BTCI)
                                        <asp:Label ID="labletxtLength" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtLength" runat="server" CssClass="textbox" MaxLength="13" Width="140px"
                                            TabIndex="10" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtLength" runat="server" ControlToValidate="txtLength"
                                            ValidationGroup="BasicData" ErrorMessage="Length (BTCI) cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Length (BTCI) cannot be blank.' />" />
                                        <asp:RangeValidator ID="rngtxtLength" runat="server" ControlToValidate="txtLength"
                                            Type="Double" ValidationGroup="BasicData" ErrorMessage="Invalid Length." SetFocusOnError="true"
                                            MinimumValue="0" MaximumValue="999999999.99" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Length.' />" />
                                    </td>
                                    <td class="leftTD" style="width: 20%">Width (BTCI)
                                        <asp:Label ID="labletxtWidth" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD" style="width: 30%">
                                        <asp:TextBox ID="txtWidth" runat="server" CssClass="textbox" MaxLength="13" Width="140"
                                            TabIndex="11" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtWidth" runat="server" ControlToValidate="txtWidth"
                                            ValidationGroup="BasicData" ErrorMessage="Width (BTCI) cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Width (BTCI) cannot be blank.' />" />
                                        <asp:RangeValidator ID="rngtxtWidth" runat="server" ControlToValidate="txtWidth"
                                            Type="Double" ValidationGroup="BasicData" ErrorMessage="Invalid Width." SetFocusOnError="true"
                                            MinimumValue="0" MaximumValue="999999999.99" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Width.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Height (BTCI)
                                        <asp:Label ID="labletxtHeight" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtHeight" runat="server" CssClass="textbox" MaxLength="13" Width="140"
                                            TabIndex="12" onkeypress="return IsNumber();" />
                                        <asp:RequiredFieldValidator ID="reqtxtHeight" runat="server" ControlToValidate="txtHeight"
                                            ValidationGroup="BasicData" ErrorMessage=" Height (BTCI) cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title=' Height (BTCI) cannot be blank.' />" />
                                        <asp:RangeValidator ID="rngtxtHeight" runat="server" ControlToValidate="txtHeight"
                                            Type="Double" ValidationGroup="BasicData" ErrorMessage="Invalid Height." SetFocusOnError="true"
                                            MinimumValue="0" MaximumValue="999999999.99" Display="Dynamic" Text="<img src='../../images/Error.png' title='Invalid Height.' />" />
                                    </td>
                                    <td class="leftTD">Unit of Dimension<br />
                                        (Length/Width/Height)
                                        <asp:Label ID="lableddlUnitDimension" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlUnitDimension" runat="server" AppendDataBoundItems="false"
                                            TabIndex="13">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlUnitDimension" runat="server" ControlToValidate="ddlUnitDimension"
                                            ValidationGroup="BasicData" ErrorMessage=" Unit of Dimension for Length/Width/Height cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title=' Unit of Dimension for Length/Width/Height cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr runat="server" id="trShipper">
                                    <td class="leftTD">Shipper Gross Weight
                                        <asp:Label ID="labletxtShipperGrossWeight" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtShipperGrossWeight" runat="server" CssClass="textbox" Width="200"
                                            TabIndex="14" MaxLength="15" />
                                        <asp:RequiredFieldValidator ID="reqtxtShipperGrossWeight" runat="server" ControlToValidate="txtShipperGrossWeight"
                                            ValidationGroup="BasicData" ErrorMessage="Shipper Gross Weight cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Shipper Gross Weight cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">Shipper Weight Unit
                                        <asp:Label ID="lableddlShipperWeightUnit" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlShipperWeightUnit" runat="server" AppendDataBoundItems="false"
                                            TabIndex="15">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlShipperWeightUnit" runat="server" ControlToValidate="ddlShipperWeightUnit"
                                            ValidationGroup="BasicData" ErrorMessage="Shipper Weight Unit cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Shipper Weight Unit cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">&nbsp;&nbsp;Descriptions :
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">Desc. Language
                                        <asp:Label ID="lableddlDescLanguage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlDescLanguage" runat="server" AppendDataBoundItems="false"
                                            TabIndex="16">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlDescLanguage" runat="server" ControlToValidate="ddlDescLanguage"
                                            ValidationGroup="BasicData" ErrorMessage="Desc. Language cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Desc. Language cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">Desc. Language 2
                                        <asp:Label ID="lableddlDescLanguage1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlDescLanguage1" runat="server" AppendDataBoundItems="false"
                                            TabIndex="18">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlDescLanguage1" runat="server" ControlToValidate="ddlDescLanguage1"
                                            ValidationGroup="BasicData" ErrorMessage="Desc. Language 2 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Desc. Language 2 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">Desc. Text
                                        <asp:Label ID="labletxtDescText" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtDescText" runat="server" CssClass="textarea" Width="200" TextMode="MultiLine"
                                            TabIndex="17" Rows="3" />
                                        <asp:RequiredFieldValidator ID="reqtxtDescText" runat="server" ControlToValidate="txtDescText"
                                            ValidationGroup="BasicData" ErrorMessage="Desc. Text cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Desc. Text cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">Desc. Text 2
                                        <asp:Label ID="labletxtDescText1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtDescText1" runat="server" CssClass="textarea" Width="200" TextMode="MultiLine"
                                            TabIndex="19" Rows="3" />
                                        <asp:RequiredFieldValidator ID="reqtxtDescText1" runat="server" ControlToValidate="txtDescText1"
                                            ValidationGroup="BasicData" ErrorMessage="Desc. Text 2 cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Desc. Text 2 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">&nbsp;&nbsp;Basic Data Text :
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Basic Data Language
                                        <asp:Label ID="lableddlBasicDataLanguage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlBasicDataLanguage" runat="server" AppendDataBoundItems="false"
                                            TabIndex="20">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlBasicDataLanguage" runat="server" ControlToValidate="ddlBasicDataLanguage"
                                            ValidationGroup="BasicData" ErrorMessage="Basic Data Language cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Basic Data Language cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">Basic Data Text
                                        <asp:Label ID="labletxtBasicDataText" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtBasicDataText" runat="server" CssClass="textarea" Width="200"
                                            TabIndex="21" TextMode="MultiLine" Rows="3" />
                                        <asp:RequiredFieldValidator ID="reqtxtBasicDataText" runat="server" ControlToValidate="txtBasicDataText"
                                            ValidationGroup="BasicData" ErrorMessage="Basic Data Text cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Basic Data Text cannot be blank.' />" />
                                    </td>

                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr style="display: none">

                                    <td class="leftTD">Basic Data Language 2
                                        <asp:Label ID="lableddlBasicDataLanguage1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlBasicDataLanguage1" runat="server" AppendDataBoundItems="false"
                                            TabIndex="22">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlBasicDataLanguage1" runat="server" ControlToValidate="ddlBasicDataLanguage1"
                                            ValidationGroup="BasicData" ErrorMessage="Basic Data Language 2 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Basic Data Language 2 cannot be blank.' />" />
                                    </td>

                                    <td class="leftTD">Basic Data Text 2
                                        <asp:Label ID="labletxtBasicDataText1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtBasicDataText1" runat="server" CssClass="textarea" Width="200"
                                            TabIndex="23" TextMode="MultiLine" Rows="3" />
                                        <asp:RequiredFieldValidator ID="reqtxtBasicDataText1" runat="server" ControlToValidate="txtBasicDataText1"
                                            ValidationGroup="BasicData" ErrorMessage="Basic Data Text 2 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Basic Data Text 2 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4">&nbsp;&nbsp;Inspection Text :
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Inspection Language
                                        <asp:Label ID="lableddlInspectionLanguage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlInspectionLanguage" runat="server" AppendDataBoundItems="false"
                                            TabIndex="24">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlInspectionLanguage" runat="server" ControlToValidate="ddlInspectionLanguage"
                                            ValidationGroup="BasicData" ErrorMessage="Inspection Language cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Inspection Language cannot be blank.' />" />
                                    </td>

                                    <td class="leftTD">Inspection Text
                                        <asp:Label ID="labletxtInspectionText" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtInspectionText" runat="server" CssClass="textarea" Width="200"
                                            TabIndex="25" TextMode="MultiLine" Rows="3" />
                                        <asp:RequiredFieldValidator ID="reqtxtInspectionText" runat="server" ControlToValidate="txtInspectionText"
                                            ValidationGroup="BasicData" ErrorMessage="Inspection Text cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Inspection Text cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">Inspection Language 2
                                        <asp:Label ID="lableddlInspectionLanguage1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlInspectionLanguage1" runat="server" AppendDataBoundItems="false"
                                            TabIndex="26">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlInspectionLanguage1" runat="server" ControlToValidate="ddlInspectionLanguage1"
                                            ValidationGroup="BasicData" ErrorMessage="Inspection Language 2 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Inspection Language 2 cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">Inspection Text 2
                                        <asp:Label ID="labletxtInspectionText1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtInspectionText1" runat="server" CssClass="textarea" Width="200"
                                            TabIndex="27" TextMode="MultiLine" Rows="3" />
                                        <asp:RequiredFieldValidator ID="reqtxtInspectionText1" runat="server" ControlToValidate="txtInspectionText1"
                                            ValidationGroup="BasicData" ErrorMessage="Inspection Text 2 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Inspection Text 2 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4">&nbsp;&nbsp;Internal Comment :
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">Internal Comment Language
                                        <asp:Label ID="lableddlInternalCommentLanguage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlInternalCommentLanguage" runat="server" AppendDataBoundItems="false"
                                            TabIndex="28">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlInternalCommentLanguage" runat="server" ControlToValidate="ddlInternalCommentLanguage"
                                            ValidationGroup="BasicData" ErrorMessage="Internal Comment Language cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Internal Comment Language cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">Internal Comment Language 2
                                        <asp:Label ID="lableddlInternalCommentLanguage1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlInternalCommentLanguage1" runat="server" AppendDataBoundItems="false"
                                            TabIndex="30">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlInternalCommentLanguage1" runat="server" ControlToValidate="ddlInternalCommentLanguage1"
                                            ValidationGroup="BasicData" ErrorMessage="Internal Comment Language 2 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Internal Comment Language 2 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr style="display: none">
                                    <td class="leftTD">Internal Comment Text
                                        <asp:Label ID="labletxtInternalCommentText" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtInternalCommentText" runat="server" CssClass="textarea" Width="200"
                                            TabIndex="31" TextMode="MultiLine" Rows="3" />
                                        <asp:RequiredFieldValidator ID="reqtxtInternalCommentText" runat="server" ControlToValidate="txtInternalCommentText"
                                            ValidationGroup="BasicData" ErrorMessage="Internal Comment Text cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Internal Comment Text cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">Internal Comment Text 2
                                        <asp:Label ID="labletxtInternalCommentText1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtInternalCommentText1" runat="server" CssClass="textarea" Width="200"
                                            TabIndex="33" TextMode="MultiLine" Rows="3" />
                                        <asp:RequiredFieldValidator ID="reqtxtInternalCommentText1" runat="server" ControlToValidate="txtInternalCommentText1"
                                            ValidationGroup="BasicData" ErrorMessage="Internal Comment Text 2 cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Internal Comment Text 2 cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Unit Of Measure Usage
                                        <asp:Label ID="lableddlUnitOfMeasureUsage" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlUnitOfMeasureUsage" runat="server" AppendDataBoundItems="false"
                                            TabIndex="32" AutoPostBack="true" OnSelectedIndexChanged="ddlUnitOfMeasureUsage_SelectedIndexChanged">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlUnitOfMeasureUsage" runat="server" ControlToValidate="ddlUnitOfMeasureUsage"
                                            ValidationGroup="BasicData" ErrorMessage="Alt. Unit Of Measure cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Alt. Unit Of Measure cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">Characteristic Name
                                        <asp:Label ID="lableddlCharacteristicName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlCharacteristicName" runat="server" AppendDataBoundItems="false"
                                            TabIndex="34">
                                            <asp:ListItem Text="--Select--" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlCharacteristicName" runat="server" ControlToValidate="ddlCharacteristicName"
                                            ValidationGroup="BasicDataX" ErrorMessage="Characteristic Name cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Characteristic Name cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr>
                                    <td class="leftTD">Planned Value Unit Measure
                                        <asp:Label ID="labletxtPlannedValueForUnitMeasure" runat="server" ForeColor="Red"
                                            Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:TextBox ID="txtPlannedValueForUnitMeasure" runat="server" CssClass="textarea"
                                            Width="200" MaxLength="30" TabIndex="35" />
                                        <asp:RequiredFieldValidator ID="reqtxtPlannedValueForUnitMeasure" runat="server"
                                            ControlToValidate="txtPlannedValueForUnitMeasure" ValidationGroup="BasicData"
                                            ErrorMessage="Planned Value Unit Measure cannot be blank." SetFocusOnError="true"
                                            Display="Dynamic" Text="<img src='../../images/Error.png' title='Planned Value Unit Measure cannot be blank.' />" />
                                    </td>
                                    <td class="leftTD">Batch Spcf Matl Unit Measure
                                        <asp:Label ID="lableddlBatchSpcfMatlUnitMeasure" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                    </td>
                                    <td class="rigthTD">
                                        <asp:DropDownList ID="ddlBatchSpcfMatlUnitMeasure" runat="server" AppendDataBoundItems="false"
                                            TabIndex="36">
                                            <asp:ListItem Text="Select" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlBatchSpcfMatlUnitMeasure" runat="server" ControlToValidate="ddlBatchSpcfMatlUnitMeasure"
                                            ValidationGroup="BasicDataX" ErrorMessage="Batch Spcf Matl Unit Measure cannot be blank."
                                            SetFocusOnError="true" Display="Dynamic" Text="<img src='../../images/Error.png' title='Batch Spcf Matl Unit Measure cannot be blank.' />" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdSpace" colspan="4"></td>
                                </tr>
                                <tr id="trButton" runat="server" visible="false">
                                    <td class="centerTD" colspan="4">
                                        <asp:Button ID="btnPrevious" runat="server" ValidationGroup="BasicData" Text="Back"
                                            UseSubmitBehavior="false" TabIndex="37" CssClass="button" OnClick="btnPrevious_Click" />
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="BasicData" UseSubmitBehavior="true"
                                            Text="Save" CssClass="button" TabIndex="38" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnNext" runat="server" ValidationGroup="BasicData" Text="Save & Next"
                                            TabIndex="39" UseSubmitBehavior="true" CssClass="button" OnClick="btnNext_Click"
                                            Width="120px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:ValidationSummary ID="sm" runat="server" ValidationGroup="BasicData" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:Label ID="lblUserId" runat="server" Visible="false" />
            <asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
            <asp:Label ID="lblBasicData2Id" runat="server" Visible="false" />
            <asp:Label ID="lblMode" runat="server" Visible="false" />
            <asp:Label ID="lblModuleId" runat="server" Visible="false" />
            <asp:Label ID="lblSectionId" runat="server" Text="4" Visible="false" />
            <asp:Label ID="lblActionType" runat="server" Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
