<%@ Page Language="C#" AutoEventWireup="true" Inherits="_MainStart" Codebehind="MainStart.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
</script>
</head>
<body style="text-align: center; background: #fffbfd">
    <form id="form1" runat="server">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:ImageMap ID="ImageMap1" runat="server" ImageUrl="~/Images/liucheng.jpg">
            <asp:RectangleHotSpot AlternateText="填制凭证" Left="40" NavigateUrl="~/AccountManage/DoVoucher.aspx" Top="100" Bottom="170" Right="100" />
            <asp:RectangleHotSpot AlternateText="凭证审核" Bottom="170" Left="195" NavigateUrl="~/AccountManage/VoucherAM.aspx" Right="255" Top="100" />
            <asp:RectangleHotSpot AlternateText="凭证记账" Bottom="170" Left="330" NavigateUrl="~/AccountManage/VoucherRM.aspx" Right="390" Top="100" />
            <asp:RectangleHotSpot AlternateText="月末结转" Bottom="170" Left="470" NavigateUrl="~/AccountManage/MonthCarryForward.aspx" Right="530" Top="100" />
        </asp:ImageMap>
    </form>
</body>
</html>
