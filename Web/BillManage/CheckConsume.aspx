<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_CheckConsume" Codebehind="CheckConsume.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>支票管理</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if ($("ConsumeMan").value == "") {
        $("ConsumeMan").focus();
        alert("报销人不能为空！");
        return false;
    }
    if ($("ConsumeMoney").value == "0") {
        $("ConsumeMoney").focus();
        alert("报销金额不能为零！");
        return false;
    }
    if ($("ConsumeMoney").value != $("BillMoney").value) {
        return confirm("报销金额与可使用金额不符，是否继续核销？");
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
                        <span style="font-size: 16pt; font-family: 隶书">支票核销</span>&nbsp;</td>
                </tr>
            </table>
        </div>
        <table id="EditorTable" cellpadding="0" cellspacing="0" style="width: 550px" runat="server">
            <tr>
                <td class="t1" style="height:29px; text-align: right;">
                    支票号码：</td>
                <td class="t2">
                    <asp:Label ID="BillNo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right;">
                    开户行及银行账号：</td>
                <td class="t1">
                    <asp:Label ID="BillBank" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right;">
                    申请用途：</td>
                <td class="t1">
                    <asp:Label ID="BillUsage" runat="server"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; width: 25%; text-align: right">
                    报销人：</td>
                <td class="t2" style="width: 75%">
                    <asp:TextBox ID="ConsumeMan" runat="server" BorderWidth="1px" Width="60px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    报销日期：</td>
                <td class="t2">
                    <asp:TextBox ID="ConsumeDate" runat="server" BorderWidth="1px" Width="80px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    报销金额：</td>
                <td class="t2">
                    <asp:TextBox ID="ConsumeMoney" runat="server" BorderWidth="1px" Width="50px"></asp:TextBox></td>
            </tr>
            <tr style="background-color:#f8f8f8;">
                <td class="t1" style="height:29px; text-align: right">
                    操作：</td>
                <td class="t2">
                    <asp:CheckBox ID="SameUpdate" runat="server" Text="记入银行日记账" /></td>
            </tr>
            <tr style="background-color:#f8f8f8;">
                <td class="t1" style="height:29px; text-align: right">
                    摘要：</td>
                <td class="t2">
                    <asp:TextBox ID="VSummary" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
            </tr>
            <tr style="background-color:#f8f8f8;">
                <td class="t1" style="height:29px; text-align: right">
                    附件：</td>
                <td class="t2">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" /></td>
            </tr>
            <tr>
                <td class="t4" colspan="2" style="height: 40px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="核销" Width="180px" Height="30px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" style="width:180px; height:30px" value="关闭" onclick="window.close();" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="RefreshFlag" runat="server" Value="0" />
        <asp:HiddenField ID="BillMoney" runat="server" Value="0" />
    </form>
</body>
</html>
