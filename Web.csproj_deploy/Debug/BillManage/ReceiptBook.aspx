<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_ReceiptBook" Codebehind="ReceiptBook.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>收据管理</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if ($("ReceiveNo").value == "") {
        $("ReceiveNo").focus();
        alert("收据序号不能为空！");
        return false;
    }
    if ($("PayUnit").value == "0") {
        $("PayUnit").focus();
        alert("交款单位不能为空！");
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
                        <span style="font-size: 16pt; font-family: 隶书">收据登记</span>&nbsp;</td>
                </tr>
            </table>
        </div>
        <table id="EditorTable" cellpadding="0" cellspacing="0" style="width: 550px" runat="server">
            <tr>
                <td class="t1" style="height:29px; width: 20%; text-align: right">
                    收款日期：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="ReceiveDate" runat="server" BorderWidth="1px" Width="80px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    收款序号：</td>
                <td class="t2" style="width: 30%">
                    <asp:TextBox ID="ReceiveNo" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    交款原因：</td>
                <td class="t2" colspan="3">
                    <asp:TextBox ID="PayReason" runat="server" BorderWidth="1px" Width="375px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    交款单位：</td>
                <td class="t1">
                    <asp:TextBox ID="PayUnit" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    交款人：</td>
                <td class="t2">
                    <asp:TextBox ID="PayMan" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    交款方式：</td>
                <td class="t1">
                    <asp:DropDownList ID="PayType" runat="server">
                        <asp:ListItem Value="0">现金</asp:ListItem>
                        <asp:ListItem Value="1">现金支票</asp:ListItem>
                        <asp:ListItem Value="2">转账支票</asp:ListItem>
                        <asp:ListItem Value="3">电汇凭证</asp:ListItem>
                        <asp:ListItem Value="4">贷记凭证</asp:ListItem>
                        <asp:ListItem Value="5">商业承兑汇票</asp:ListItem>
                        <asp:ListItem Value="6">银行承兑汇票</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t1" style="height:29px; text-align: right">
                    票据号码：</td>
                <td class="t2">
                    <asp:TextBox ID="InvoiceNo" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    收款额：</td>
                <td class="t1">
                    <asp:TextBox ID="ReceiveMoney" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox>（单位：元）</td>
                <td class="t1" style="text-align: right">
                    收款人：</td>
                <td class="t2">
                    <asp:TextBox ID="ReceiveMan" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    制单人：</td>
                <td class="t1">
                    <asp:TextBox ID="DoBillMan" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    当前状态：</td>
                <td class="t2">
                    <asp:DropDownList ID="ReveiveState" runat="server">
                        <asp:ListItem Value="0">已开</asp:ListItem>
                        <asp:ListItem Value="1">已换开发票</asp:ListItem>
                        <asp:ListItem Value="2">已并开发票</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    备注：</td>
                <td class="t2" colspan="3">
                    <asp:TextBox ID="Notes" runat="server" BorderWidth="1px" Width="375px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 60px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="登记" Width="180px" Height="30px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" style="width:180px; height:30px" value="关闭" onclick="window.close();" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="RefreshFlag" runat="server" Value="0" />
        <asp:HiddenField ID="ReceiptID" runat="server" />
    </form>
</body>
</html>
