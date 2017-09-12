<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_CheckBook" Codebehind="CheckBook.aspx.cs" %>

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
    if ($("BillNoPre").value == "") {
        $("BillNoPre").focus();
        alert("支票编码前缀不能为空！");
        return false;
    }
    if ($("BillNoStart").value == "0") {
        $("BillNoStart").focus();
        alert("支票编码起始码不能为空！");
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
                        <span style="font-size: 16pt; font-family: 隶书">支票登记</span>&nbsp;</td>
                </tr>
            </table>
        </div>
        <table id="EditorTable" cellpadding="0" cellspacing="0" style="width: 550px" runat="server">
            <tr>
                <td class="t1" style="height:29px; text-align: right;">
                    开户行及银行账号：</td>
                <td class="t2">
                    <asp:DropDownList ID="BillBank" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; width: 25%; text-align: right">
                    币种：</td>
                <td class="t2" style="width: 75%">
                    <asp:TextBox ID="BillCurrency" runat="server" BorderWidth="1px" Width="60px" Text="人民币"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    支票类型：</td>
                <td class="t2">
                    <asp:DropDownList ID="BillType" runat="server">
                        <asp:ListItem Value="1">现金支票</asp:ListItem>
                        <asp:ListItem Value="2">转账支票</asp:ListItem>
                        <asp:ListItem Value="3">电汇凭证</asp:ListItem>
                        <asp:ListItem Value="4">贷记凭证</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    登记日期：</td>
                <td class="t2">
                    <asp:TextBox ID="BillDate" runat="server" BorderWidth="1px" Width="80px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    有效期：</td>
                <td class="t2">
                    <asp:TextBox ID="BillPeriod" runat="server" BorderWidth="1px" Width="50px"></asp:TextBox>&nbsp;天</td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    支票号码：</td>
                <td class="t2">
                    <asp:TextBox ID="BillNoPre" runat="server" BorderWidth="1px" Width="80px"></asp:TextBox>&nbsp;—
                    <asp:TextBox ID="BillNoStart" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox>（格式：[前缀]—[起始码]）</td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    支票张数：</td>
                <td class="t2">
                    <asp:TextBox ID="BillCount" runat="server" BorderWidth="1px" Width="50px"></asp:TextBox></td>
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
