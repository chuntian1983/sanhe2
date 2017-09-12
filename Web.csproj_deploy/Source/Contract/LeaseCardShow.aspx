<%@ Page Language="C#" AutoEventWireup="true" Inherits="Contract_LeaseCardShow" Codebehind="LeaseCardShow.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>合同卡片</title>
<base target="_self" />
<style type="text/css">html{overflow-x:hidden;}</style>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
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
function imgresize(id) {
    var resizeWidth, resizeHeight, newwidth, newheight;
    newwidth = document.all(id).width;
    newheight = document.all(id).height;
    resizeWidth = 600;
    resizeHeight = 500;
    if (newheight > resizeHeight) {
        newwidth = newwidth * (resizeHeight / newheight);
        newheight = newheight * (resizeHeight / newheight);
    }
    if (newwidth > resizeWidth) {
        newheight = newheight * (resizeWidth / newwidth);
        newwidth = newwidth * (resizeWidth / newwidth);
    }
    document.all(id).width = newwidth;
    document.all(id).height = newheight;
}
window.onload = function () {
    resetDialogSize();
    if (document.all("APicture")) {
        imgresize("APicture");
    }
}
</script>
</head>
<body style="margin-left:25px">
    <form id="form1" runat="server">
    <div style="width:700px; text-align:left;">
        <table cellpadding="0" cellspacing="0" style="width: 700px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">合同卡片</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table id="CardInfo" cellpadding="0" cellspacing="0" style="width: 700px">
            <tr>
                <td class="b" style="width: 15%; height: 25px; text-align: right">
                    卡片编号：</td>
                <td class="b" style="width: 35%;">
                    <asp:Label ID="CardNo" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="width: 15%; text-align: right">
                    变更日期：</td>
                <td class="b" style="width: 35%;">
                    <asp:Label ID="BookDate" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    承&nbsp;&nbsp;包&nbsp;&nbsp;人：</td>
                <td class="b">
                    <asp:Label ID="LeaseHolder" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    联系电话：</td>
                <td class="b">
                    <asp:Label ID="LinkTel" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="text-align: right; height: 25px;" id="TD_Name" runat="server">
                    租赁资源：</td>
                <td class="b">
                    <asp:Label ID="ResourceName" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="height: 22px; text-align: right">
                    交款方式：</td>
                <td class="b" style="height: 22px">
                    <asp:Label ID="PayType" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    每次收款金额：</td>
                <td class="b" style="height: 22px">
                    <asp:Label ID="YearRental" runat="server"></asp:Label>&nbsp;（元/年）</td>
                <td class="b" style="height: 25px; text-align: right">
                    已收款总额：</td>
                <td class="b" style="height: 22px">
                    <asp:Label ID="SumRental" runat="server">0</asp:Label>&nbsp;（元）</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    下次交款时间：</td>
                <td class="b" style="height: 22px">
                    <asp:Label ID="NextPayDate" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="height: 25px; text-align: right">
                    下次交款金额：</td>
                <td class="b" style="height: 22px">
                    <asp:Label ID="NextPayMoney" runat="server">0</asp:Label>&nbsp;（元）</td>
            </tr>
            <tr>
                <td class="b" style="text-align: right; height: 25px;">
                    开始日期：</td>
                <td class="b">
                    <asp:Label ID="SLease" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    结束日期：</td>
                <td class="b">
                    <asp:Label ID="ELease" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr id="TdAmount" runat="server">
                <td class="b" style="text-align: right; height: 22px;">
                    数量或面积：</td>
                <td class="b" style="height: 22px">
                    <asp:Label ID="ResAmount" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right; height: 25px;">
                    计量单位：</td>
                <td class="b" style="height: 22px">
                    <asp:Label ID="ResUnit" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    备注：</td>
                <td class="b" colspan="3">
                    <asp:Label ID="Notes" runat="server" Width="554px" Height="115px"></asp:Label>&nbsp;</td>
            </tr>
        </table>
        <table id="ContractInfo" cellpadding="0" cellspacing="0" style="width: 700px;">
            <tr>
                <td class="b" style="width: 15%; height: 25px; text-align: right">
                    单位名称：</td>
                <td class="b" style="width: 35%;">
                    <asp:Label ID="ContractCo" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="width: 15%; text-align: right">
                    所属组织：</td>
                <td class="b" style="width: 35%;">
                    <asp:Label ID="ResUnitName" runat="server"  BackColor="#F6F6F6"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    合同编号：</td>
                <td class="b">
                    <asp:Label ID="ContractNo" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    签订日期：</td>
                <td class="b">
                    <asp:Label ID="ContractDate" runat="server"  BackColor="#F6F6F6"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    合同名称：</td>
                <td class="b">
                    <asp:Label ID="ContractName" runat="server"></asp:Label>&nbsp;</td>
                <td class="b" style="text-align: right">
                    合同类别：</td>
                <td class="b">
                    <asp:Label ID="ContractType" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="text-align: right; height: 25px;">
                    合同总金额：</td>
                <td class="b">
                    <asp:Label ID="ContractMoney" runat="server">0</asp:Label>&nbsp;（元）</td>
                <td class="b" style="text-align: right;">
                    合同年限：</td>
                <td class="b">
                    <asp:Label ID="ContractYears" runat="server">0</asp:Label>&nbsp;（年）</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    合同内容摘要：</td>
                <td class="b" colspan="3">
                    <asp:Label ID="ContractContent" runat="server" Width="554px" Height="120px"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="b" style="height: 25px; text-align: right">
                    合同到期：<br />履行情况：</td>
                <td class="b" colspan="3">
                    <asp:Label ID="ContractNote" runat="server" Width="554px" Height="100px"></asp:Label>&nbsp;</td>
            </tr>
            <tr id="DivAPicture" runat="server">
                <td style="text-align: right; height: 22px;">
                    附件：</td>
                <td colspan="5" style="height: 22px; text-align:left;">
                    <asp:Image ID="APicture" runat="server" /></td>
            </tr>
        </table>
        <!--NoPrintStart-->        
        <table cellpadding="0" cellspacing="0" style="width: 700px">
            <tr>
                <td colspan="4" style="height: 52px; text-align: center">
                    <input id="Button4" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印卡片" style="width: 180px; height: 30px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" onclick="window.close();" type="button" value="关闭" style="width: 191px; height: 33px" /></td>
            </tr>
        </table>
        <!--NoPrintEnd-->
    </div>
    </form>
</body>
</html>
