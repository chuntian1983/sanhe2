<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_ReceiptShow" Codebehind="ReceiptShow.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>收据管理</title>
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
                    <span style="font-size: 16pt; font-family: 隶书">收据详情</span>&nbsp;</td>
            </tr>
        </table>
        <table id="EditorTable" cellpadding="0" cellspacing="0" style="width: 550px" runat="server">
            <tr>
                <td class="t1" style="height:29px; width: 22%; text-align: right;">
                    收款日期：</td>
                <td class="t1" style="width: 35%">
                    <asp:Label ID="ReceiveDate" runat="server"></asp:Label>&nbsp;
                </td>
                <td class="t1" style="height:29px; width: 13%; text-align: right">
                    收款序号：</td>
                <td class="t2" style="width: 30%">
                    <asp:Label ID="ReceiveNo" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    交款原因：</td>
                <td class="t1" colspan="3">
                    <asp:Label ID="PayReason" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    交款单位：</td>
                <td class="t1">
                    <asp:Label ID="PayUnit" runat="server"></asp:Label>&nbsp;
                </td>
                <td class="t1" style="height:29px; text-align: right">
                    交款人：</td>
                <td class="t2">
                    <asp:Label ID="PayMan" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    交款方式：</td>
                <td class="t1">
                    <asp:Label ID="PayType" runat="server"></asp:Label>&nbsp;
                </td>
                <td class="t1" style="height:29px; text-align: right">
                    票据号码：</td>
                <td class="t2">
                    <asp:Label ID="InvoiceNo" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    收款额：</td>
                <td class="t1">
                    <asp:Label ID="ReceiveMoney" runat="server"></asp:Label>&nbsp;
                </td>
                <td class="t1" style="height:29px; text-align: right">
                    收款人：</td>
                <td class="t2">
                    <asp:Label ID="ReceiveMan" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    制单人：</td>
                <td class="t1">
                    <asp:Label ID="DoBillMan" runat="server"></asp:Label>&nbsp;
                </td>
                <td class="t1" style="height:29px; text-align: right">
                    当前状态：</td>
                <td class="t2">
                    <asp:Label ID="ReveiveState" runat="server"></asp:Label>&nbsp;
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
