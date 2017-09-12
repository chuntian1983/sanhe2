<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountQuery_PrintVoucher" Codebehind="PrintVoucher.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function Number.prototype.str(s){var a=""+this;return s.substring(0,s.length-a.length)+a;}
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function SelAMonth(v)
{
    var a=eval($("QEMonth").value);
    for(var i=$("QEMonth").options.length-1;i>=0;i--)
    {
        $("QEMonth").options.remove(i);
    }
    for(var i=eval(v);i<=12;i++)
    {
        $("QEMonth").options.add(new Option(i.str("00")+"月",i.str("00")));
        if(a==i){$("QEMonth").selectedIndex=$("QEMonth").options.length-1;}
    }
}
function CheckQ()
{
    var patrn=/[^0-9]/;
    if(patrn.test($("VCount").value))
    {
        $("VCount").focus();
        alert("每页凭证数量参数格式不正确！");
        return false;
    }
    if(patrn.test($("EntryCount").value))
    {
        $("EntryCount").focus();
        alert("每凭证分录数参数格式不正确！");
        return false;
    }
    if(patrn.test($("TopSpace").value))
    {
        $("TopSpace").focus();
        alert("距页顶部间距参数格式不正确！");
        return false;
    }
    if(patrn.test($("VSpace").value))
    {
        $("VSpace").focus();
        alert("凭证之间距离参数格式不正确！");
        return false;
    }
    return true;
}
function ShowAddons(v)
{
    window.showModalDialog("../AccountManage/AppendixShow.aspx?id="+v+"&g="+(new Date()).getTime(),"","dialogWidth=720px;dialogHeight=508px;center=yes;");
}
function ShowEntryData(v,s,r,m)
{
    window.showModalDialog("../AccountManage/AddEntryData.aspx?id="+v+"&no="+s+"&row="+r+"&money="+m+"&g="+(new Date()).getTime(),"","dialogWidth=600px;dialogHeight=452px;center=yes;");
}
function startPrint(v)
{
    window.open('../PrintWeb.html', '', '');
    return;
    var printUrl="VoucherPrint1.aspx?year="+$("QYear").value+"&sm="+$('QSMonth').value+"&em="+$('QEMonth').value+"&sno="+$('VoucherNoS').value+"&eno="+$('VoucherNoE').value+"&g="+(new Date()).getTime();
    switch(v)
    {
        case 0:
            PrintOneURL0(printUrl);
            break;
        case 1:
            PrintOneURL1(printUrl);
            break;
    }
}
</script>
</head>
<body style="font-size: 10pt" onpaste="return false">
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="font-size: 18pt; font-family: 隶书">凭证打印输出</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    查询日期：</td>
                <td class="t1" style="width: 35%">
                    <asp:DropDownList ID="QYear" runat="server">
                    </asp:DropDownList><asp:DropDownList ID="QSMonth" runat="server">
                        <asp:ListItem Value="01">01月</asp:ListItem>
                        <asp:ListItem Value="02">02月</asp:ListItem>
                        <asp:ListItem Value="03">03月</asp:ListItem>
                        <asp:ListItem Value="04">04月</asp:ListItem>
                        <asp:ListItem Value="05">05月</asp:ListItem>
                        <asp:ListItem Value="06">06月</asp:ListItem>
                        <asp:ListItem Value="07">07月</asp:ListItem>
                        <asp:ListItem Value="08">08月</asp:ListItem>
                        <asp:ListItem Value="09">09月</asp:ListItem>
                        <asp:ListItem Value="10">10月</asp:ListItem>
                        <asp:ListItem Value="11">11月</asp:ListItem>
                        <asp:ListItem Value="12">12月</asp:ListItem>
                    </asp:DropDownList>&nbsp; ^^^^&nbsp;
                    <asp:DropDownList ID="QEMonth" runat="server">
                        <asp:ListItem Value="01">01月</asp:ListItem>
                        <asp:ListItem Value="02">02月</asp:ListItem>
                        <asp:ListItem Value="03">03月</asp:ListItem>
                        <asp:ListItem Value="04">04月</asp:ListItem>
                        <asp:ListItem Value="05">05月</asp:ListItem>
                        <asp:ListItem Value="06">06月</asp:ListItem>
                        <asp:ListItem Value="07">07月</asp:ListItem>
                        <asp:ListItem Value="08">08月</asp:ListItem>
                        <asp:ListItem Value="09">09月</asp:ListItem>
                        <asp:ListItem Value="10">10月</asp:ListItem>
                        <asp:ListItem Value="11">11月</asp:ListItem>
                        <asp:ListItem Value="12">12月</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 15%; text-align: right">
                    凭证编号：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="VoucherNoS" runat="server" Width="71px"></asp:TextBox>&nbsp; ^^^^&nbsp;
                    <asp:TextBox ID="VoucherNoE" runat="server" Width="71px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    超额凭证：</td>
                <td class="t1" style="width: 35%">
                    <asp:CheckBox ID="cbxShowVoucher" runat="server" Checked="True" Text="输出凭证" />
                    &nbsp;
                    <asp:CheckBox ID="cbxShowTipImg" runat="server" Checked="True" Text="显示超额提示警灯" /></td>
                <td class="t1" style="width: 15%; text-align: right">
                    冲红凭证：</td>
                <td class="t2" style="width: 35%">
                    <asp:CheckBox ID="cbxShowReverseState" runat="server" Checked="True" Text="显示冲红状态" /></td>
            </tr>
            <tr>
                <td class="t2" colspan="4" style="text-align: center; background:#f6f6f6">打印参数设置</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">每页凭证数量：</td>
                <td class="t1" style="width: 35%">
                <asp:TextBox ID="VCount" runat="server" Width="71px">2</asp:TextBox>（单位：个，0为不限制）</td>
                <td class="t1" style="width: 15%; text-align: right">距页顶部间距：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="TopSpace" runat="server" Width="71px">10</asp:TextBox>（单位：px）</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">每凭证分录数：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="EntryCount" runat="server" Width="71px">7</asp:TextBox>（单位：条，0为不限制）</td>
                <td class="t1" style="width: 15%; text-align: right">凭证之间距离：</td>
                <td class="t2" style="width: 35%">
                <asp:TextBox ID="VSpace" runat="server" Width="71px">35</asp:TextBox>（单位：px）</td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 31px; text-align: center">
                 <asp:Button ID="QButton" runat="server" Text="查询凭证" OnClick="QButton_Click" Width="120px" />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <input id="Button1" onclick="startPrint(0);" type="button" value="--  打印预览 --" style="width: 120px" />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <%--<input id="Button3" onclick="startPrint(1);" type="button" value="--  直接打印 --" style="width: 120px" />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                 <asp:Button ID="SavePrintPara" runat="server" Text="保存打印参数" Width="120px" OnClick="SavePrintPara_Click" /></td>
            </tr>
        </table>
        <hr style="width: 750px; color:Red; height:2px; text-align:left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="vertical-align:top; height: 235px;">
                    <asp:PlaceHolder ID="ShowPageContent" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">SelAMonth($("QSMonth").value);</script>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
