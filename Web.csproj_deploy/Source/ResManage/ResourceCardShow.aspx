<%@ Page Language="C#" AutoEventWireup="true" Inherits="ResManage_ResourceCardShow" Codebehind="ResourceCardShow.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>资源卡片</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function resetDialogSize()
{
    var ua = navigator.userAgent;
    if(ua.lastIndexOf("MSIE 7.0") == -1)
    {
        var height = document.body.offsetHeight;
        var width = document.body.offsetWidth;
        if(ua.lastIndexOf("Windows NT 5.2") == -1)
        {
            window.dialogHeight=(height+53)+"px";
            window.dialogWidth=(width+6)+"px";
        }
        else
        {
            window.dialogHeight=(height+46)+"px";
            window.dialogWidth=(width+6)+"px";
        }
    }
}
function imgresize(id)
{
    var resizeWidth,resizeHeight,newwidth,newheight;
    newwidth=document.all(id).width;
    newheight=document.all(id).height;
    resizeWidth=520;
    resizeHeight=500;
    if (newheight>resizeHeight)
    {
        newwidth=newwidth*(resizeHeight/newheight);
        newheight=newheight*(resizeHeight/newheight);
    }
    if (newwidth>resizeWidth)	
    {
        newheight=newheight*(resizeWidth/newwidth);
        newwidth=newwidth*(resizeWidth/newwidth);
    }
    document.all(id).width=newwidth;
    document.all(id).height=newheight;
}
window.onload=function()
{
    resetDialogSize();
    if(document.all("ResPicture"))
    {
        imgresize("ResPicture");
    }
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
        <table cellpadding="0" cellspacing="0" style="width: 700px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">资源卡片</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 700px">
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    卡片编号：</td>
                <td class="b" style="text-align: left">
                    <asp:Label ID="CardNo" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    填制日期：</td>
                <td class="b" style="text-align: left">
                    <asp:Label ID="BookDate" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="text-align: right; height: 25px;">
                    类别编号：</td>
                <td class="b" style="text-align: left">
                    <asp:Label ID="ClassID" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right;">
                    类别名称：</td>
                <td class="b" style="text-align: left">
                    <asp:Label ID="ClassName" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="text-align: right; height: 25px;">
                    经营方式：</td>
                <td class="b" style="text-align: left">
                    <asp:Label ID="BookType" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    使用状况：</td>
                <td class="b" style="text-align: left">
                    <asp:Label ID="UsedState" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="width: 15%; text-align: right; height: 25px;">
                    资源编号：</td>
                <td class="b" style="width: 35%; text-align: left;">
                    <asp:Label ID="ResNo" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="width: 15%; text-align: right">
                    资源名称：</td>
                <td class="b" style="width: 35%; text-align: left;">
                    <asp:Label ID="ResName" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="text-align: right; height: 25px;">
                    部门名称：</td>
                <td class="b" style="text-align: left">
                    <asp:Label ID="DeptName" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right;">
                    存放地点：</td>
                <td class="b" style="text-align: left">
                    <asp:Label ID="Locality" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    涉及农民数：</td>
                <td class="b" style="text-align: left">
                    <asp:Label ID="RelateFarmers" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    规格型号：</td>
                <td class="b" style="text-align: left">
                    <asp:Label ID="ResModel" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="text-align: right; height: 22px;">
                    数量或面积：</td>
                <td class="b" style="text-align: left;">
                    <asp:Label ID="ResAmount" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right; height: 25px;">
                    计量单位：</td>
                <td class="b" style="text-align: left;">
                    <asp:Label ID="ResUnit" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 22px; text-align: right">
                    东至：</td>
                <td class="b" style="text-align: left">
                    <asp:Label ID="BorderE" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="height: 25px; text-align: right">
                    西至：</td>
                <td class="b" style="text-align: left">
                    <asp:Label ID="BorderW" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="text-align: right; height: 25px;">
                    南至：</td>
                <td class="b" style="text-align: left">
                    <asp:Label ID="BorderS" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    北至：</td>
                <td class="b" style="text-align: left">
                    <asp:Label ID="BorderN" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: center; background-color:#f0f0f0" colspan="4">
                    依法承包</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    面积：</td>
                <td class="b">
                    <asp:Label ID="name4" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    &nbsp;</td>
                <td class="b">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: center; background-color:#f0f0f0" colspan="4">
                    集体经营</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    面积：</td>
                <td class="b">
                    <asp:Label ID="name5" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    年收益：</td>
                <td class="b">
                    <asp:Label ID="name6" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: center; background-color:#f0f0f0" colspan="4">
                    承包经营</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    面积：</td>
                <td class="b">
                    <asp:Label ID="name7" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    承包人：</td>
                <td class="b">
                    <asp:Label ID="name8" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    承包起止日期：</td>
                <td class="b">
                    <asp:Label ID="name9" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    年承包金：</td>
                <td class="b">
                    <asp:Label ID="name10" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    支付方式：</td>
                <td class="b">
                    <asp:Label ID="name11" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    &nbsp;</td>
                <td class="b">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: center; background-color:#f0f0f0" colspan="4">
                    对外投资</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    面积：</td>
                <td class="b">
                    <asp:Label ID="name12" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    年投资收益：</td>
                <td class="b">
                    <asp:Label ID="name13" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    支付方式：</td>
                <td class="b">
                    <asp:Label ID="name14" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    投资对象：</td>
                <td class="b">
                    <asp:Label ID="name15" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: center; background-color:#f0f0f0" colspan="4">
                    其他占用方式</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    已利用面积：</td>
                <td class="b">
                    <asp:Label ID="name16" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    闲置面积：</td>
                <td class="b">
                    <asp:Label ID="name17" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    评估年收益：</td>
                <td class="b">
                    <asp:Label ID="name18" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    &nbsp;</td>
                <td class="b">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    资源用途：</td>
                <td class="b" colspan="3" style="text-align: left">
                    <asp:Label ID="ResUsage" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    资源备注：</td>
                <td class="b" colspan="3" style="text-align: left;">
                    <asp:Label ID="Notes" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr id="DivAPicture" runat="server">
                <td class="b" style="text-align: right; height: 22px;">
                    资源图片：</td>
                <td class="b" colspan="3" style="height: 22px; text-align: center">
                    <asp:Image ID="ResPicture" runat="server" /></td>
            </tr>
            <!--NoPrintStart-->
            <tr>
                <td colspan="4" style="height: 52px; text-align: center">
                    <input id="Button4" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印资源卡片" style="width: 180px; height: 30px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" onclick="window.close();" type="button" value="关闭" style="width: 120px; height: 30px" /></td>
            </tr>
            <!--NoPrintEnd-->
        </table>
    </div>
    </form>
</body>
</html>
