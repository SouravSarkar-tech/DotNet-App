<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="js/jquery-ui.min.js" type="text/javascript"></script>
    <script src="js/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="stylesheet/gridviewScroll.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false"
            GridLines="None">
            <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdoSelection" runat="server" GroupName="selection" onclick="javascript:CheckOtherIsCheckedByGVID(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                <asp:BoundField HeaderText="Department" DataField="Department_Name" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Status" DataField="Approval" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Remarks" DataField="Remarks" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Actioned By" DataField="Actioned_By" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Actioned On" DataField="Actioned_On" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Department" DataField="Department_Name" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Status" DataField="Approval" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Remarks" DataField="Remarks" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Actioned By" DataField="Actioned_By" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Actioned On" DataField="Actioned_On" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Department" DataField="Department_Name" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Status" DataField="Approval" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Remarks" DataField="Remarks" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Actioned By" DataField="Actioned_By" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Actioned On" DataField="Actioned_On" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Department" DataField="Department_Name" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Status" DataField="Approval" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Remarks" DataField="Remarks" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Actioned By" DataField="Actioned_By" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField HeaderText="Actioned On" DataField="Actioned_On" HeaderStyle-HorizontalAlign="Left" />
            </Columns>
            <HeaderStyle CssClass="GridviewScrollHeader" />
            <RowStyle CssClass="GridviewScrollItem" />
            <PagerStyle CssClass="GridviewScrollPager" />
        </asp:GridView>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=GridView1.ClientID%>').gridviewScroll({
                width: 660,
                height: 200,
                freezesize: 2
            });
        } 
</script> 
    </form>
</body>
</html>
