<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMaterialDescl.ascx.cs"
    Inherits="Transaction_UserControl_ucMaterialDescl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<script language="javascript" type="text/javascript">

    function GetToolTip(control) {
        ddrivetipm('3', control);
    }

    function generateDescription() {
        var str = "";
        //var len = 1;
        //        if (event.keyCode == 8 || event.keyCode == 46)
        //            len = -1;

        if ($('#<%= txtItemDesc.ClientID%>').val() != "")
            str = $('#<%= txtItemDesc.ClientID%>').val();

        if ($('#<%= txtItemType.ClientID%>').val() != "")
            str += ' ' + $('#<%= txtItemType.ClientID%>').val();

        if ($('#<%= ddlMOC.ClientID%>').val() != "")
            str += ' ' + $('#<%= ddlMOC.ClientID%>').val();

        if ($('#<%= ddlSize.ClientID%>').val() != "")
            str += ' ' + $('#<%= ddlSize.ClientID%>').val();

        if ($('#<%= txtSize.ClientID%>').val() != "")
            str += ' ' + $('#<%= txtSize.ClientID%>').val();

        if ($('#<%= txtProcessConSize.ClientID%>').val() != "")
            str += ' ' + $('#<%= txtProcessConSize.ClientID%>').val();

        if ($('#<%= ddlProcessConSize.ClientID%>').val() != "")
            str += ' ' + $('#<%= ddlProcessConSize.ClientID%>').val();

        if ($('#<%= ddlClassRatingGrade.ClientID%>').val() != "")
            str += ' ' + $('#<%= ddlClassRatingGrade.ClientID%>').val();

        if ($('#<%= txtClassRatingGrade.ClientID%>').val() != "")
            str += ' ' + $('#<%= txtClassRatingGrade.ClientID%>').val();

        if ($('#<%= ddlMFGStd.ClientID%>').val() != "")
            str += ' ' + $('#<%= ddlMFGStd.ClientID%>').val();

        if ($('#<%= txtMFGStd.ClientID%>').val() != "")
            str += ' ' + $('#<%= txtMFGStd.ClientID%>').val();

        if ($('#<%= ddlRangeCapacity.ClientID%>').val() != "")
            str += ' ' + $('#<%= ddlRangeCapacity.ClientID%>').val();

        if ($('#<%= txtRangeCapacity.ClientID%>').val() != "")
            str += ' ' + $('#<%= txtRangeCapacity.ClientID%>').val();

        if ($('#<%= txtAvgLeastCnt.ClientID%>').val() != "")
            str += ' ' + $('#<%= txtAvgLeastCnt.ClientID%>').val();

        if ($('#<%= ddlSupplyVolt.ClientID%>').val() != "")
            str += ' ' + $('#<%= ddlSupplyVolt.ClientID%>').val();

        if ($('#<%= txtSupplyVolt.ClientID%>').val() != "")
            str += ' ' + $('#<%= txtSupplyVolt.ClientID%>').val();

        if ($('#<%= ddlFlameProof.ClientID%>').val() != "")
            str += ' ' + $('#<%= ddlFlameProof.ClientID%>').val();

        if ($('#<%= txtFlameProof.ClientID%>').val() != "")
            str += ' ' + $('#<%= txtFlameProof.ClientID%>').val();

        if ($('#<%= txtProtectionClass.ClientID%>').val() != "")
            str += ' ' + $('#<%= txtProtectionClass.ClientID%>').val();

        if ($('#<%= ddlProtectionClass.ClientID%>').val() != "")
            str += ' ' + $('#<%= ddlProtectionClass.ClientID%>').val();

        if ($('#<%= txtIO.ClientID%>').val() != "")
            str += ' ' + $('#<%= txtIO.ClientID%>').val();

        if ($('#<%= txtManufacturerPartNo.ClientID%>').val() != "")
            str += ' ' + $('#<%= txtManufacturerPartNo.ClientID%>').val();

        if ($('#<%= txtMakeMachModelNo.ClientID%>').val() != "")
            str += ' ' + $('#<%= txtMakeMachModelNo.ClientID%>').val();

        //        if (str.length + len > 40)
        if (str.length > 40) {
            return false;
            //alert(str);
            //PageMethods.functionname(str);
        }
        else {
            $('#<%= txtMatDesc.ClientID%>').val(str);
            $('#<%= txtCharacters.ClientID%>').val(str.length);
            $('#<%= hdnMatDesc.ClientID%>').val(str);

        }
    }

</script>
<asp:Panel ID="pnlDescData" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="trHeading" align="center">
                Material Description Data
            </td>
        </tr>
        <tr>
            <td class="tdSpace">
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <asp:Panel ID="pnlMatDescData" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="3">
                                Desc :
                                <%--<asp:Label runat="server" ID="lblMaterialDesc" />--%>
                                <asp:TextBox runat="server" ID="txtMatDesc" Enabled="false" Size="41" 
                                    forecolor = "Black" Width="370px"/>
                                <asp:HiddenField runat="server" ID="hdnMatDesc" />
                            </td>
                            <td align="right">
                                Char :
                                <%--<asp:Label runat="server" ID="lblCharacters" />--%>
                                <asp:TextBox runat="server" ID="txtCharacters" Enabled="false" Size="3" forecolor = "Black"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                1. Item Description
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtItemDesc');"
                                    onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <asp:TextBox ID="txtItemDesc" runat="server" CssClass="textbox" TabIndex="1" MaxLength = "40"
                                 onkeypress="return generateDescription();" onblur="return generateDescription();"></asp:TextBox>
                            </td>
                            <%--  <td class="rightTD" style="width: 20%">
                            </td>--%>
                            <td class="leftTD" style="width: 20%">
                                2. Item Type
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtItemType');"
                                    onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <asp:TextBox ID="txtItemType" runat="server" CssClass="textbox" TabIndex="2" 
                                onkeypress="return generateDescription();" onblur="return generateDescription();"></asp:TextBox>
                            </td>
                            <%--  <td class="rightTD" style="width: 20%">
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                3. MOC
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <asp:DropDownList ID="ddlMOC" runat="server" TabIndex="3" AppendDataBoundItems="false"
                                    AutoPostBack="true" onblur="return generateDescription();">
                                    <%--<asp:ListItem Text="Select" Value="" />--%>
                                    <%--OnSelectedIndexChanged ="return generateDescription();" --%>
                                </asp:DropDownList>
                            </td>
                            <%-- <td class="rightTD" style="width: 20%">
                            </td>--%>
                            <td class="leftTD" style="width: 20%">
                                4. Size
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <asp:DropDownList ID="ddlSize" runat="server" AppendDataBoundItems="false" TabIndex="4"
                                    onblur="return generateDescription();" OnSelectedIndexChanged="ddlSize_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <%-- <asp:ListItem Text="Select" Value="" />--%>
                                    <%--OnSelectedIndexChanged ="return generateDescription();"--%>
                                </asp:DropDownList>
                                <%-- <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox" onkeypress="return generateDescription();" TabIndex = "5" AutoPostBack = "true"
                                    onblur="return generateDescription();" OnTextChanged="txtSize_TextChanged"></asp:TextBox>--%>
                                <asp:TextBox ID="txtSize" runat="server" CssClass="textbox" onkeypress="return generateDescription();" 
                                    TabIndex="5" AutoPostBack="true" onblur="return generateDescription();" OnTextChanged="txtSize_TextChanged"></asp:TextBox>
                            </td>
                            <%--<td class="rightTD" style="width: 20%">                                
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                5. Process Connection Size
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <%--     <asp:DropDownList ID="ddlProcessConSize" runat="server" AppendDataBoundItems="false"
                                    TabIndex="6" onblur="return generateDescription();" AutoPostBack="true">
                                </asp:DropDownList>--%>
                                <%--  <asp:TextBox ID="txtProcessConSize" runat="server" CssClass="textbox" TabIndex = "6" onkeypress="return generateDescription();"
                                    onblur="return generateDescription();"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlProcessConSize" runat="server" TabIndex="3" AppendDataBoundItems="false"
                                    AutoPostBack="true" onblur="return generateDescription();" 
                                    OnSelectedIndexChanged="ddlProcessConSize_SelectedIndexChanged">
                                    <%--<asp:ListItem Text="Select" Value="" />--%>
                                    <%--OnSelectedIndexChanged ="return generateDescription();" --%>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtProcessConSize" runat="server" CssClass="textbox" TabIndex="7" 
                                    onkeypress="return generateDescription();" 
                                    onblur="return generateDescription();" 
                                    OnTextChanged="txtProcessConSize_TextChanged"></asp:TextBox>
                            </td>
                            <%--<td class="rightTD" style="width: 20%">                             
                            </td>--%>
                            <td class="leftTD" style="width: 20%">
                                6. Class/Rating/Grade
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <asp:DropDownList ID="ddlClassRatingGrade" runat="server" AppendDataBoundItems="false"
                                    TabIndex="8" onblur="return generateDescription();" AutoPostBack="true" OnSelectedIndexChanged="ddlClassRatingGrade_SelectedIndexChanged">
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtClassRatingGrade" runat="server" CssClass="textbox" TabIndex="7"
                                    onkeypress="return generateDescription();" onblur="return generateDescription();"></asp:TextBox>--%>
                                <asp:TextBox ID="txtClassRatingGrade" runat="server" CssClass="textbox" TabIndex="9"
                                    onkeypress="return generateDescription();" AutoPostBack="true" onblur="return generateDescription();"
                                    OnTextChanged="txtClassRatingGrade_TextChanged"></asp:TextBox>
                            </td>
                            <%--<td class="rightTD" style="width: 20%">                              
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                7. MFG Standard
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtMFGStd');"
                                    onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <asp:DropDownList ID="ddlMFGStd" runat="server" AppendDataBoundItems="false" TabIndex="10"
                                    onblur="return generateDescription();" AutoPostBack="true" OnSelectedIndexChanged="ddlMFGStd_SelectedIndexChanged">
                                </asp:DropDownList>
                                <%-- <asp:TextBox ID="txtMFGStd" runat="server" CssClass="textbox" TabIndex="8" onkeypress="return generateDescription();"
                                    onblur="return generateDescription();"></asp:TextBox>--%>
                                <asp:TextBox ID="txtMFGStd" runat="server" CssClass="textbox" TabIndex="11" onkeypress="return generateDescription();" 
                                    onblur="return generateDescription();" AutoPostBack="true" OnTextChanged="txtMFGStd_TextChanged"></asp:TextBox>
                            </td>
                            <%--<td class="rightTD" style="width: 20%">                                
                            </td>--%>
                            <td class="leftTD" style="width: 20%">
                                8. Range/Capacity
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtRangeCapacity');"
                                    onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <asp:DropDownList ID="ddlRangeCapacity" runat="server" AppendDataBoundItems="false"
                                    TabIndex="12" onblur="return generateDescription();" AutoPostBack="true" OnSelectedIndexChanged="ddlRangeCapacity_SelectedIndexChanged">
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtRangeCapacity" runat="server" CssClass="textbox" TabIndex="9"
                                    onkeypress="return generateDescription();" onblur="return generateDescription();"></asp:TextBox>--%>
                                <asp:TextBox ID="txtRangeCapacity" runat="server" CssClass="textbox" TabIndex="13"
                                    onkeypress="return generateDescription();" onblur="return generateDescription();"
                                    AutoPostBack="true" OnTextChanged="txtRangeCapacity_TextChanged"></asp:TextBox>
                            </td>
                            <%--<td class="rightTD" style="width: 20%">                                
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                9. Accuracy/Least Count
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtAvgLeastCnt');"
                                    onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <asp:TextBox ID="txtAvgLeastCnt" runat="server" CssClass="textbox" TabIndex="13" 
                                    onkeypress="return generateDescription();" onblur="return generateDescription();"></asp:TextBox>
                            </td>
                            <%--<td class="rightTD" style="width: 20%">
                            </td>--%>
                            <td class="leftTD" style="width: 20%">
                                10. Supply Voltage
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtSupplyVolt');"
                                    onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <asp:DropDownList ID="ddlSupplyVolt" runat="server" AppendDataBoundItems="false"
                                    TabIndex="14" onblur="return generateDescription();" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplyVolt_SelectedIndexChanged">
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtSupplyVolt" runat="server" CssClass="textbox" TabIndex="11" onkeypress="return generateDescription();"
                                    onblur="return generateDescription();"></asp:TextBox>--%>
                                <asp:TextBox ID="txtSupplyVolt" runat="server" CssClass="textbox" TabIndex="15" onkeypress="return generateDescription();"
                                    onblur="return generateDescription();" AutoPostBack="true" OnTextChanged="txtSupplyVolt_TextChanged"></asp:TextBox>
                            </td>
                            <%--<td class="rightTD" style="width: 20%">
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                11. Flame Proof
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <asp:DropDownList ID="ddlFlameProof" runat="server" AppendDataBoundItems="false"
                                    TabIndex="16" onblur="return generateDescription();" AutoPostBack="true" OnSelectedIndexChanged="ddlFlameProof_SelectedIndexChanged">
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtFlameProof" runat="server" CssClass="textbox" TabIndex="17" onkeypress="return generateDescription();"
                                    onblur="return generateDescription();"></asp:TextBox>--%>
                                <asp:TextBox ID="txtFlameProof" runat="server" CssClass="textbox" TabIndex="17" onkeypress="return generateDescription();" 
                                    onblur="return generateDescription();" AutoPostBack="true" OnTextChanged="txtFlameProof_TextChanged"></asp:TextBox>
                            </td>
                            <%--<td class="rightTD" style="width: 20%">                                 
                            </td>--%>
                            <td class="leftTD" style="width: 20%">
                                12. Protection Class
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <asp:DropDownList ID="ddlProtectionClass" runat="server" TabIndex="3" AppendDataBoundItems="false"
                                    AutoPostBack="true" onblur="return generateDescription();" 
                                    OnSelectedIndexChanged="ddlProtectionClass_SelectedIndexChanged">
                                    <%--<asp:ListItem Text="Select" Value="" />--%>
                                    <%--OnSelectedIndexChanged ="return generateDescription();" --%>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtProtectionClass" runat="server" CssClass="textbox" TabIndex="13" 
                                    onkeypress="return generateDescription();" 
                                    onblur="return generateDescription();" 
                                    OnTextChanged="txtProtectionClass_TextChanged"></asp:TextBox>
                            </td>
                            <%-- <td class="rightTD" style="width: 20%">
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                13. Input/Output
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtIO');"
                                    onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <asp:TextBox ID="txtIO" runat="server" CssClass="textbox" TabIndex="14" onkeypress="return generateDescription();" 
                                    onblur="return generateDescription();"></asp:TextBox>
                            </td>
                            <%--  <td class="rightTD" style="width: 20%">
                            </td>--%>
                            <td class="leftTD" style="width: 20%">
                                14. Manufacturer Part No
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtManufacturerPartNo');"
                                    onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <asp:TextBox ID="txtManufacturerPartNo" runat="server" CssClass="textbox" TabIndex="15" 
                                    onkeypress="return generateDescription();" onblur="return generateDescription();"></asp:TextBox>
                            </td>
                            <%--<td class="rightTD" style="width: 20%">
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftTD" style="width: 20%">
                                15. Make,Machine Name and Model Number
                                <img src="../../images/Help-icon.png" alt="?" onmouseover="GetToolTip('txtMakeMachModelNo');"
                                    onmouseout="hideddrivetip();" style="width: 16px; height: 16px; cursor: pointer;" />
                            </td>
                            <td class="rightTD" style="width: 30%">
                                <asp:TextBox ID="txtMakeMachModelNo" runat="server" CssClass="textbox" TabIndex="16" 
                                    onkeypress="return generateDescription();" onblur="return generateDescription();"></asp:TextBox>
                            </td>
                            <%--<td class="rightTD" style="width: 20%">
                            </td>--%>
                            <td class="tdSpace" colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSpace" colspan="4">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:ValidationSummary ID="smMatDesc" runat="server" ValidationGroup="MaterialDesc"
    ShowMessageBox="true" ShowSummary="false" />
<asp:Label ID="lblUserId" runat="server" Visible="false" />
<asp:Label ID="lblMatDescId" runat="server" Visible="false" />
<asp:Label ID="lblMasterHeaderId" runat="server" Visible="false" />
<asp:Label ID="lblModuleId" runat="server" Visible="false" />