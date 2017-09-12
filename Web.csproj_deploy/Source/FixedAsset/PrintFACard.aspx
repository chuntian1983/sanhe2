<%@ Page Language="C#" AutoEventWireup="true" Inherits="FixedAsset_PrintFACard" Codebehind="PrintFACard.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<base target="_self" />
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function imgresize(id)
{
    var resizeWidth,resizeHeight,newwidth,newheight;
    newwidth=document.all(id).width;
    newheight=document.all(id).height;
    resizeWidth=650;
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
    if(document.all("APicture"))
    {
        imgresize("APicture");
    }
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">固定资产卡片</span>
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt; text-align:right">
            <tr>
                <td style="width: 15%; text-align: right">
                    卡片编号</td>
                <td style="width: 20%">
                    <asp:Label ID="CardID" runat="server" Width="120px"></asp:Label></td>
                <td style="width: 18%; text-align: right">&nbsp;</td>
                <td style="width: 17%">&nbsp;</td>
                <td style="width: 12%; text-align: right;">
                    日期</td>
                <td style="width: 17%"><asp:Label ID="PrintDate" runat="server" Width="100px"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="6"><hr style="height:2px;" /></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    资产编号</td>
                <td>
                    <asp:TextBox ID="AssetNo" runat="server" Width="138px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right">
                    资产名称</td>
                <td colspan="3">
                    <asp:TextBox ID="AssetName" runat="server" Width="390px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    类别编号</td>
                <td>
                    <asp:TextBox ID="ClassID" runat="server" Width="138px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right;">
                    类别名称</td>
                <td colspan="3">
                    <asp:TextBox ID="CName" runat="server" Width="390px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    规格型号</td>
                <td>
                    <asp:TextBox ID="AssetModel" runat="server" Width="138px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right;">
                    部门名称</td>
                <td colspan="3">
                    <asp:TextBox ID="DeptName" runat="server" Width="390px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    增加方式</td>
                <td>
                    <asp:TextBox ID="AddType" runat="server" Width="138px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right">
                    存放地点</td>
                <td colspan="3">
                    <asp:TextBox ID="Depositary" runat="server" Width="390px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    使用状况
                </td>
                <td>
                    <asp:TextBox ID="UseState" runat="server" Width="138px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right">
                    使用年限</td>
                <td>
                    <asp:TextBox ID="UseLife" runat="server" Width="142px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right;">
                    折旧方法</td>
                <td>
                    <asp:TextBox ID="DeprMethod" runat="server" Width="142px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    开始使用日期</td>
                <td>
                    <asp:TextBox ID="SUseDate" runat="server" Width="138px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right">
                    已计提月份</td>
                <td>
                    <asp:TextBox ID="UsedMonths" runat="server" Width="142px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right;">
                    币种</td>
                <td>
                    <asp:TextBox ID="CurrencyType" runat="server" Width="142px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    原值</td>
                <td>
                    <asp:TextBox ID="OldPrice" runat="server" Width="138px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right;">
                    净残值率</td>
                <td>
                    <asp:TextBox ID="JingCZL" runat="server" Width="142px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right;">
                    净残值</td>
                <td>
                    <asp:TextBox ID="JingCZ" runat="server" Width="142px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    累计折旧</td>
                <td>
                    <asp:TextBox ID="ZheJiu" runat="server" Width="138px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right">
                    月折旧率</td>
                <td>
                    <asp:TextBox ID="MonthZJL" runat="server" Width="142px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right;">
                    月折旧额</td>
                <td>
                    <asp:TextBox ID="MonthZJE" runat="server" Width="142px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    净值</td>
                <td>
                    <asp:TextBox ID="NewPrice" runat="server" Width="138px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right">
                    对应折旧科目</td>
                <td>
                    <asp:TextBox ID="DeprSubject" runat="server" Width="142px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right;">
                    项目</td>
                <td>
                    <asp:TextBox ID="AssetItem" runat="server" Width="142px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    计量单位</td>
                <td>
                    <asp:TextBox ID="AUnit" runat="server" Width="138px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right">
                    数量或面积</td>
                <td>
                    <asp:TextBox ID="AAmount" runat="server" Width="142px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
                <td style="text-align: right;">
                    单价</td>
                <td>
                    <asp:TextBox ID="APrice" runat="server" Width="142px" CssClass="CardCSS" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr id="DivAPicture" runat="server">
                <td style="text-align: right; height: 22px;">
                    资产图片</td>
                <td colspan="5" style="height: 22px; text-align:left;">
                    <asp:Image ID="APicture" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="6" style="text-align: center"><hr style="height:2px;" /></td>
            </tr>
            <tr>
                <td style="text-align: right; height: 15px;">
                    录入人</td>
                <td style="height: 15px;">
                    <asp:Label ID="BookMan" runat="server"></asp:Label></td>
                <td style="text-align: right; height: 15px;">&nbsp;</td>
                <td style="height: 15px;">&nbsp;</td>
                <td style="text-align: right; height: 15px;">
                    录入日期</td>
                <td style="height: 15px;">
                    <asp:Label ID="BookTime" runat="server"></asp:Label></td>
            </tr>
            <!--NoPrintStart-->
            <tr>
                <td colspan="6" style="height: 52px; text-align: center;">
                <input id="Button4" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印卡片" style="width: 180px; height: 33px" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="Button2" onclick="window.close();" type="button" value="关闭" style="width: 120px; height: 30px" /></td>
            </tr>
            <!--NoPrintEnd-->
        </table>
    </div>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
