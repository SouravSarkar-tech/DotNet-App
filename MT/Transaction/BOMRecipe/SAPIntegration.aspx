<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAPIntegration.aspx.cs" Inherits="Transaction_BOMRecipe_SAPIntegration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../js/jquery.min.js" type="text/javascript"></script>
    <link href="../../stylesheet/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HyperLink ID="hyper" runat="server" CssClass="button"></asp:HyperLink>
    </div>
    <asp:GridView ID="grdSearch" runat="server" AutoGenerateColumns="false" Width="100%"
        BorderColor="#9D9D9D" EmptyDataText="No Data Found" GridLines="Both">
        <RowStyle CssClass="light-gray" />
        <HeaderStyle CssClass="gridHeader" />
        <AlternatingRowStyle CssClass="gridRowStyle" />
        <PagerStyle CssClass="PagerStyle" />
        <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Uploaded on" DataField="Created_Date" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField HeaderText="Uploaded By" DataField="Created_By" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField HeaderText="INtegration Details" DataField="msg" HeaderStyle-HorizontalAlign="Left"
                ItemStyle-Width="70%" ItemStyle-HorizontalAlign="Left" />
        </Columns>
    </asp:GridView>
    </form>

    <script type="text/javascript">
        $("a.button").one("click", function () {
            $(this).click(function () { return false; });
        });
    </script>
</body>
</html>
