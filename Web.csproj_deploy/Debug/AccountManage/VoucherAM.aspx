<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountManage_VoucherAM" Codebehind="VoucherAM.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function ShowAddons(v)
{
    window.showModalDialog("AppendixShow.aspx?id="+v+"&g="+(new Date()).getTime(),"","dialogWidth=720px;dialogHeight=508px;center=yes;");
}
function ShowEntryData(v,s,r,m)
{
    window.showModalDialog("AddEntryData.aspx?id="+v+"&no="+s+"&row="+r+"&money="+m+"&g="+(new Date()).getTime(),"","dialogWidth=600px;dialogHeight=452px;center=yes;");
}
</script>
</head>
<body style="font-size: 10pt">
    <form id="form1" runat="server">
    <asp:HiddenField ID="aDay" runat="server" Value="2" />
    <asp:HiddenField ID="aMonth" runat="server" Value="1" />
    <asp:HiddenField ID="aYear" runat="server" Value="2008" />
    <asp:HiddenField ID="tDay" runat="server" Value="20" />
    <asp:HiddenField ID="tMonth" runat="server" Value="5" />
    <asp:HiddenField ID="tYear" runat="server" Value="2008" />
    <asp:HiddenField ID="tWeek" runat="server" Value="一" />
    <script type="text/javascript" src="../Images/SelDate/popcalendar2.js"></script>
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">凭证审核 -- 单张</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 10%; text-align: right; height: 32px">
                    凭证日期：</td>
                <td class="t1" style="width: 20%">
                    <asp:TextBox ID="QVDate" runat="server"></asp:TextBox></td>
                <td class="t1" style="width: 10%; text-align: right">
                    凭证编号：</td>
                <td class="t1" style="width: 27%; text-align: center">
                    <asp:TextBox ID="VoucherNoS" runat="server" Width="71px"></asp:TextBox>&nbsp;^^^^^&nbsp;
                    <asp:TextBox ID="VoucherNoE" runat="server" Width="71px"></asp:TextBox></td>
                <td class="t2" style="width: 33%; text-align: center;">
                    <asp:Button ID="Button3" runat="server" Height="25px" OnClick="Button3_Click" Text="查询凭证" Width="90px" />&nbsp;&nbsp;
                    <asp:Button ID="AuditQVoucher" runat="server" Height="25px" OnClick="AuditQVoucher_Click" Text="按此条件审核凭证" Width="137px" /></td>
            </tr>
            <tr>
                <td class="t4" style="height: 32px; text-align: center" colspan="5">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="首张凭证" Height="25px" Width="70px" />&nbsp;&nbsp;
                    <asp:Button ID="PreVoucher" runat="server" OnClick="PreVoucher_Click" Text="上张凭证" Height="25px" Width="70px" />&nbsp;&nbsp;
                    <asp:Button ID="NextVoucher" runat="server" OnClick="NextVoucher_Click" Text="下张凭证" Height="25px" Width="70px" />&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="末张凭证" Height="25px" Width="70px" />&nbsp;&nbsp;
                    <input id="Button4" type="button" value="查看凭证列表" style="height:25px; width:97px" onclick="location.href='VoucherAL.aspx';" />&nbsp;&nbsp;
                    <asp:Button ID="BackControl" runat="server" Text="反审核" Height="25px" Width="72px" OnClick="BackControl_Click" />&nbsp;&nbsp;
                    <asp:Button ID="AuditVoucher" runat="server" OnClick="AuditVoucher_Click" Text="审核当前凭证" Height="25px" Width="90px" />&nbsp;&nbsp;
                    <asp:Button ID="AuditAllVoucher" runat="server" OnClick="AuditAllVoucher_Click" Text="批量审核所有凭证" Height="25px" Width="137px" /></td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="background-position: center center; background-repeat: no-repeat; height: 349px; text-align: center; vertical-align:top">
                    <asp:PlaceHolder ID="ShowPageContent" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="RecordRowIndex" runat="server" Value="0" />
    <asp:HiddenField ID="Alarms" runat="server" Value="1=2" />
    <asp:HiddenField ID="QuotField" runat="server" />
    </form>
</body>
</html>
