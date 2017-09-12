<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountInit_StartAccount" Codebehind="StartAccount.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>启用账套</title>
<base target="_self" />
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("AccountDate").value=="")
    {
        $("AccountDate").focus();
        alert("请设置财务日期！");
        return false;
    }
    if(eval($("AccountDate").value.substr(8,2))>20)
    {
        return confirm("建账时财务日期建议不超过本月20日。\n\n您确定需要继续启用该账套？");
    }
    return confirm("建议您启用账套前，请对数据进行完全备份！\n\n您确定需要继续启用该账套吗？");
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
window.onload = resetDialogSize;
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td class="t2" colspan="3" style="height: 33px; text-align: center">
                <span id="PageTitle" style="font-size: 18pt; font-family: 隶书" runat="server">启用账套</span>
                </td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 40px; text-align: right">
                    财务日期：</td>
                <td class="t1" style="text-align: center; width: 30%;">
                    <asp:TextBox ID="AccountDate" runat="server" BorderWidth="1px" Width="150px" ForeColor="Blue"></asp:TextBox></td>
                <td class="t2" rowspan="4" style="width: 55%; text-align: center; background: #f6f6f6; color: blue;">
                    <strong><span style="color: green">【数据统计】<br />
                    </span></strong><br />
                    &nbsp;―――――借方期初余额汇总――――：<asp:Label ID="Label4" runat="server" Width="70px"  CssClass="pright"></asp:Label><br />
                    &nbsp;―――――贷方期初余额汇总――――：<asp:Label ID="Label5" runat="server" Width="70px"  CssClass="pright"></asp:Label><br />
                    <br />
                    &nbsp;―――――资产类＋成本类―――――：<asp:Label ID="Label1" runat="server" Width="70px"  CssClass="pright"></asp:Label><br />
                    &nbsp;――负债类＋所有者权益＋损益类――：<asp:Label ID="Label2" runat="server" Width="70px"  CssClass="pright"></asp:Label><br />
                    &nbsp;――其中损益类（收入－费用）―――：<asp:Label ID="Label3" runat="server" Width="70px" CssClass="pright"></asp:Label><br />
                 </td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right; height: 40px">
                    当前日期：</td>
                <td class="t1" style="text-align: center;">
                    <asp:TextBox ID="CurrentDate" runat="server" BorderWidth="1px" Width="150px" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right; height: 40px;">
                    账套名称：</td>
                <td class="t1" style="text-align: center;">
                    <asp:TextBox ID="AccountName" runat="server" BorderWidth="1px" Width="150px" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right; height: 40px;">
                    乡镇名称：</td>
                <td class="t1" style="text-align: center;">
                    <asp:TextBox ID="UnitName" runat="server" BorderWidth="1px" Width="150px" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="3" style="height: 66px; text-align: center">
                    【借方―贷方＝<asp:Label ID="Label6" runat="server" Width="100px" Font-Names="Arial"></asp:Label>】
                    【资产类―负债类―所有者权益＝<asp:Label ID="Label7" runat="server" Width="100px" Font-Names="Arial"></asp:Label>】<br />
                    <br /><span style="color: #008000">上述数据统计中资产类应等于负债、所有者权益、成本、损益之和；如果数据正确，请启用账套。</span></td>
            </tr>
            <tr>
                <td class="t4" colspan="3" style="height: 50px; text-align: center; background: #f6f6f6">
                    <asp:Button ID="Button1" runat="server" Text="启用账套" OnClick="Button1_Click" Height="30px" Width="150px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BackupDate" runat="server" Text="数据完全备份" OnClick="BackupDate_Click" Height="30px" Width="150px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="btnWinCLose" type="button" value="关闭窗口" onclick="window.close();" style="width:150px; height:30px" /></td>
            </tr>
        </table>
    </div>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    <asp:HiddenField ID="UseDate" runat="server" />
    </form>
</body>
</html>
