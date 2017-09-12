<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_CheckReceive" Codebehind="CheckReceive.aspx.cs" %>

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
    if ($("ReceiveMan").value == "") {
        $("ReceiveMan").focus();
        alert("申领人不能为空！");
        return false;
    }
    if ($("BillUsage").value == "") {
        $("BillUsage").focus();
        alert("申领用途不能为空！");
        return false;
    }
    if ($("BillMoney").value == "0") {
        $("BillMoney").focus();
        alert("申领金额不能为零！");
        return false;
    }
    return true;
}
function selSubject() {
    var returnV = window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=1&g=" + (new Date()).getTime(), "", "dialogWidth=380px;dialogHeight=401px;center=yes;");
    if (typeof (returnV) != "undefined" && returnV[0] != "" && returnV[0] != "+" && returnV[0] != "-") {
        $("BillUsage").value = returnV[1] + "." + returnV[0];
    }
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
                        <span style="font-size: 16pt; font-family: 隶书">支票申领</span>&nbsp;</td>
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
                <td class="t1" style="height:29px; width: 25%; text-align: right">
                    申领人：</td>
                <td class="t2" style="width: 75%">
                    <asp:TextBox ID="ReceiveMan" runat="server" BorderWidth="1px" Width="60px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    申领日期：</td>
                <td class="t2">
                    <asp:TextBox ID="ReceiveDate" runat="server" BorderWidth="1px" Width="80px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; width: 25%; text-align: right">
                    申领用途：</td>
                <td class="t2" style="width: 75%">
                    <asp:TextBox ID="BillUsage" runat="server" BorderWidth="1px" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    使用限额：</td>
                <td class="t2">
                    <asp:TextBox ID="BillMoney" runat="server" BorderWidth="1px" Width="50px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t4" colspan="2" style="height: 60px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="申领" Width="180px" Height="30px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" style="width:180px; height:30px" value="关闭" onclick="window.close();" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="RefreshFlag" runat="server" Value="0" />
    </form>
</body>
</html>
