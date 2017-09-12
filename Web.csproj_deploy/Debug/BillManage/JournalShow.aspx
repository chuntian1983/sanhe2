<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_JournalShow" Codebehind="JournalShow.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>日记账</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
input{margin-left:3px}
select{margin-left:3px}
</style>
<base target="_self" />
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
function imgresize(img)
{
    var resizeWidth,resizeHeight,newwidth,newheight;
    newwidth=img.width;
    newheight=img.height;
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
    img.width=newwidth;
    img.height=newheight;
}
window.onload = function()
{
    resetDialogSize();
    var imgs=document.getElementsByTagName("img");
    for(var i=0;i<imgs.length;i++)
    {
        imgresize(imgs[i]);
    }
}
</script>
</head>
<body style="text-align:center">
    <form id="form1" runat="server">
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width:600px">
            <tr>
                <td style="height: 35px; text-align: center">
                    <input id="Button4" onclick="window.open('../PrintWeb.html','','')" type="button" value="打印报表" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button1" onclick="window.close()" type="button" value="关闭" /></td>
            </tr>
        </table>
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width:600px">
            <tr>
                <td style="height: 35px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书" id="PageTitle" runat="server">现金日记账凭证</span>&nbsp;</td>
            </tr>
        </table>
        <table id="EditorTable" cellpadding="0" cellspacing="0" style="width:600px;text-align:left" runat="server">
            <tr>
                <td class="t1" style="text-align: right">
                    现金科目：</td>
                <td class="t2">
                    <asp:Label ID="AccSubjectNo" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right">
                    币种：</td>
                <td class="t2">
                    <asp:Label ID="AccCurrency" runat="server">人民币</asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right">
                    凭证日期：</td>
                <td class="t2">
                    <asp:Label ID="VoucherDate" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="width: 20%; text-align: right">
                    当日序号：</td>
                <td class="t2" style="width: 80%">
                    <asp:Label ID="DayNo" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right">
                    凭证字：</td>
                <td class="t2">
                    <asp:Label ID="VoucherType" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right; height: 29px;">
                    凭证号：</td>
                <td class="t2">
                    <asp:Label ID="VoucherNo" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right; height: 29px;">
                    分录号：</td>
                <td class="t2">
                    <asp:Label ID="EntryNo" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr id="DivSettle0" runat="server">
                <td class="t1" style="text-align: right">
                    结算方式：</td>
                <td class="t2">
                    <asp:Label ID="SettleType" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr id="DivSettle1" runat="server">
                <td class="t1" style="text-align: right">
                    结算号：</td>
                <td class="t2">
                    <asp:Label ID="SettleNo" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr id="DivSettle2" runat="server">
                <td class="t1" style="text-align: right">
                    结算日期：</td>
                <td class="t2">
                    <asp:Label ID="SettleDate" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right">
                    借方金额：</td>
                <td class="t2">
                    <asp:Label ID="DebitMoney" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right">
                    贷方金额：</td>
                <td class="t2">
                    <asp:Label ID="CreditMoney" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right">
                    对方科目：</td>
                <td class="t2">
                    <asp:Label ID="LinkSubjectNo" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    经手人：</td>
                <td class="t2">
                    <asp:Label ID="Handler" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right">
                    摘要：</td>
                <td class="t2">
                    <asp:Label ID="Notes" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t3" style="text-align: right">
                    制单人：</td>
                <td class="t4">
                    <asp:Label ID="DoBill" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr id="DivAPicture" runat="server">
                <td style="text-align: right; height: 22px;">
                    附单：</td>
                <td colspan="5" style="height: 22px; text-align:left;">
                    <asp:Image ID="APicture" runat="server" /></td>
            </tr>
        </table>
        <asp:HiddenField ID="RefreshFlag" runat="server" Value="0" />
        <asp:HiddenField ID="TableID" runat="server" />
        <asp:HiddenField ID="hidBalance" runat="server" Value="0" />
    </form>
</body>
</html>
