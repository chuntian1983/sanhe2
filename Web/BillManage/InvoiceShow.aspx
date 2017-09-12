<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_InvoiceShow" Codebehind="InvoiceShow.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>发票管理</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
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
window.onload = function()
{
    resetDialogSize();
}
</script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 550px">
            <tr>
                <td style="height: 35px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">发票详情</span>&nbsp;</td>
            </tr>
        </table>
        <table id="EditorTable" cellpadding="0" cellspacing="0" style="width: 550px" runat="server">
            <tr>
                <td class="t1" style="height:29px; width: 22%; text-align: right;">
                    领购日期：</td>
                <td class="t1" style="width: 35%">
                    <asp:Label ID="BuyDate" runat="server"></asp:Label>&nbsp;
                </td>
                <td class="t1" style="height:29px; width: 13%; text-align: right">
                    验旧提醒：</td>
                <td class="t2" style="width: 30%">
                    <asp:Label ID="OldTestDate" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    发票种类：</td>
                <td class="t1">
                    <asp:Label ID="InvoiceType" runat="server"></asp:Label>&nbsp;
                </td>
                <td class="t1" style="height:29px; text-align: right;">
                    发票代码：</td>
                <td class="t2">
                    <asp:Label ID="InvoiceCode" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    发票号码：</td>
                <td class="t1">
                    <asp:Label ID="InvoiceNo" runat="server"></asp:Label>&nbsp;
                </td>
                <td class="t1" style="height:29px; text-align: right">
                    发票金额：</td>
                <td class="t2">
                    <asp:Label ID="InvoiceMoney" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    客户名称：</td>
                <td class="t1">
                    <asp:Label ID="CustomName" runat="server"></asp:Label>&nbsp;
                </td>
                <td class="t1" style="height:29px; text-align: right">
                    客户税号：</td>
                <td class="t2">
                    <asp:Label ID="CustomTaxNo" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    开票人：</td>
                <td class="t1">
                    <asp:Label ID="DoBillMan" runat="server"></asp:Label>&nbsp;
                </td>
                <td class="t1" style="height:29px; text-align: right">
                    开票日期：</td>
                <td class="t2">
                    <asp:Label ID="DoBillDate" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    开票金额：</td>
                <td class="t1">
                    <asp:Label ID="DoBillMoney" runat="server"></asp:Label>&nbsp;
                </td>
                <td class="t1" style="height:29px; text-align: right">
                    税率：</td>
                <td class="t1">
                    <asp:Label ID="TaxRate" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    税额：</td>
                <td class="t1">
                    <asp:Label ID="TaxMoney" runat="server"></asp:Label>&nbsp;
                </td>
                <td class="t1" style="height:29px; text-align: right">
                    价税合计：</td>
                <td class="t1">
                    <asp:Label ID="SumMoney" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 50px; text-align: center">
                    <input id="Button2" type="button" style="width:80px; height:28px" value="关闭" onclick="window.close();" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
