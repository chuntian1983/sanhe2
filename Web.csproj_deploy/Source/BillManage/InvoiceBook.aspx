﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_InvoiceBook" Codebehind="InvoiceBook.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>发票管理</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if ($("InvoiceCode").value == "") {
        $("InvoiceCode").focus();
        alert("发票代码不能为空！");
        return false;
    }
    if ($("InvoiceNo").value == "0") {
        $("InvoiceNo").focus();
        alert("起始号码不能为空！");
        return false;
    }
    return true;
}
function OnWinClose()
{
    window.returnValue = $("RefreshFlag").value;
}
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
window.onload = function()
{
    resetDialogSize();
}
</script>
</head>
<body onunload="OnWinClose();">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" style="width: 550px">
                <tr>
                    <td style="height: 35px; text-align: center">
                        <span style="font-size: 16pt; font-family: 隶书">登记新购发票</span>&nbsp;</td>
                </tr>
            </table>
        </div>
        <table id="EditorTable" cellpadding="0" cellspacing="0" style="width: 550px" runat="server">
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    领购日期：</td>
                <td class="t2">
                    <asp:TextBox ID="BuyDate" runat="server" BorderWidth="1px" Width="80px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    验旧提醒：</td>
                <td class="t2">
                    <asp:TextBox ID="OldTestDate" runat="server" BorderWidth="1px" Width="80px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    发票种类：</td>
                <td class="t2">
                    <asp:DropDownList ID="InvoiceType" runat="server">
                        <asp:ListItem Value="0">增值税专用发票</asp:ListItem>
                        <asp:ListItem Value="1">普通发票</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    发票代码：</td>
                <td class="t2">
                    <asp:TextBox ID="InvoiceCode" runat="server" BorderWidth="1px" Width="120px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    起始号码：</td>
                <td class="t2">
                    <asp:TextBox ID="InvoiceNo" runat="server" BorderWidth="1px" Width="120px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    发票张数：</td>
                <td class="t2">
                    <asp:TextBox ID="InvoiceCount" runat="server" BorderWidth="1px" Width="50px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    发票金额：</td>
                <td class="t2">
                    <asp:TextBox ID="InvoiceMoney" runat="server" BorderWidth="1px" Width="50px"></asp:TextBox>（若设置为零，则为非定额发票）
                </td>
            </tr>
            <tr>
                <td class="t4" colspan="2" style="height: 60px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="登记" Width="180px" Height="30px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" style="width:180px; height:30px" value="关闭" onclick="window.close();" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="RefreshFlag" runat="server" Value="0" />
    </form>
</body>
</html>
