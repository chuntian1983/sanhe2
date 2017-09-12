<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountInit_SetAccountPara" Codebehind="SetAccountPara.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
input{margin-left:1px}
</style>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("AccountName").value=="")
    {
      $("AccountName").focus();
      alert("账套名称不能为空！");
      return false;
    }
    if($("AccountName").value.length>12)
    {
      $("AccountName").focus();
      alert("账套名称长度不能超过12个汉字或字符！");
      return false;
    }
    if($("Director").value.length>6)
    {
      $("Director").focus();
      alert("主管会计长度不能超过6个汉字或字符！");
      return false;
    }
    if($("PageRowCount").value.length==0)
    {
      $("PageRowCount").focus();
      alert("默认报表打印行数不能为空！");
      return false;
    }
    if(!(/\d/.test($("PageRowCount").value)))
    {
      $("PageRowCount").focus();
      alert("默认报表打印行数必须为数字！");
      return false;
    }
    return confirm("您确定保存账套信息设置吗？");
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">账套信息设置</span></td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    乡镇编号：</td>
                <td class="t1" style="height: 25px">
                    <asp:Label ID="UnitID" runat="server" Text="Label"></asp:Label>&nbsp;</td>
                <td class="t1" style="height: 25px; text-align: right">
                    乡镇名称：</td>
                <td class="t2" style="height: 25px">
                    <asp:Label ID="UnitName" runat="server" Text="Label"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    账套编号：</td>
                <td class="t1" style="height: 25px">
                    <asp:Label ID="AccountID" runat="server" Text="Label"></asp:Label>&nbsp;</td>
                <td class="t1" style="height: 25px; text-align: right">
                    建账日期：</td>
                <td class="t2" style="height: 25px">
                    <asp:Label ID="StartAccountDate" runat="server" Text="Label"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    财务日期：</td>
                <td class="t1" style="height: 25px">
                    <asp:Label ID="AccountDate" runat="server" Text="Label"></asp:Label>&nbsp;</td>
                <td class="t1" style="height: 25px; text-align: right">
                    最后结转：</td>
                <td class="t2" style="height: 25px">
                    <asp:Label ID="LastCarryDate" runat="server" Text="Label"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t2" colspan="4" style="height: 25px; background:#f6f6f6; text-align: center;">&nbsp;基本信息参数设置</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    账套名称：</td>
                <td class="t1" style="width: 35%; height: 25px">
                    <asp:TextBox ID="AccountName" runat="server" Width="240px"></asp:TextBox></td>
                <td class="t1" style="width: 112px; height: 25px; text-align: right">
                    赤字提示：</td>
                <td class="t2" style="width: 35%; height: 25px">
                    <asp:RadioButtonList ID="SetRedFigure" runat="server" RepeatDirection="Horizontal" Width="141px">
                        <asp:ListItem Selected="True" Value="0">关闭</asp:ListItem>
                        <asp:ListItem Value="1">开启</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    主管会计：</td>
                <td class="t1" style="width: 35%; height: 25px">
                    <asp:TextBox ID="Director" runat="server" Width="240px"></asp:TextBox></td>
                <td class="t1" style="width: 112px; height: 25px; text-align: right">
                    年末收益分配：</td>
                <td class="t2" style="width: 35%; height: 25px">
                    <asp:RadioButtonList ID="YearCarryFlag" runat="server" RepeatDirection="Horizontal" Width="160px">
                        <asp:ListItem Selected="True" Value="1">执行</asp:ListItem>
                        <asp:ListItem Value="0">不执行</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    默认报表行数：</td>
                <td class="t1" style="width: 35%; height: 25px">
                    <asp:TextBox ID="PageRowCount" runat="server" Width="111px">36</asp:TextBox>（每页显示行数）</td>
                <td class="t1" style="width: 112px; height: 25px; text-align: right">
                    自动年结凭证：</td>
                <td class="t2" style="width: 35%; height: 25px">
                    <asp:TextBox ID="YearCarryVoucher" runat="server" Width="240px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    科目分组设置：</td>
                <td class="t2" style="width: 85%; height: 25px" colspan="3">
                    <asp:TextBox ID="SubjectGroup" runat="server" Width="435px"></asp:TextBox>[每个科目以英文逗号（,）分隔]</td>
            </tr>
            <tr>
                <td class="t2" colspan="4" style="height: 25px; text-align: center">
                    报表打印行数设置</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    科目余额表：</td>
                <td class="t1" style="width: 35%; height: 25px">
                    <asp:TextBox ID="PageRowCount000000" runat="server" Width="111px">0</asp:TextBox>（每页显示行数）</td>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    收支逐笔公开：</td>
                <td class="t2" style="width: 35%; height: 25px">
                    <asp:TextBox ID="PageRowCount100002" runat="server" Width="111px">0</asp:TextBox>（每页显示行数）</td>
            </tr>
            <tr>
                <td class="t1" style="width: 112px; height: 25px; text-align: right">
                    总账：</td>
                <td class="t1" style="width: 35%; height: 25px">
                    <asp:TextBox ID="PageRowCount100008" runat="server" Width="111px">0</asp:TextBox>（每页显示行数）</td>
                <td class="t1" style="width: 112px; height: 25px; text-align: right">
                    报表分析：</td>
                <td class="t2" style="width: 35%; height: 25px">
                    <asp:TextBox ID="PageRowCount100001" runat="server" Width="111px">0</asp:TextBox>（每页显示行数）</td>
            </tr>
            <tr>
                <td class="t1" style="width: 112px; height: 25px; text-align: right">
                    明细分类账：</td>
                <td class="t1" style="width: 35%; height: 25px">
                    <asp:TextBox ID="PageRowCount100004" runat="server" Width="111px">0</asp:TextBox>（每页显示行数）</td>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    固定资产明细账：</td>
                <td class="t2" style="width: 35%; height: 25px">
                    <asp:TextBox ID="PageRowCount100007" runat="server" Width="111px">0</asp:TextBox>（每页显示行数）</td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 52px; text-align: center">
                    <asp:Button ID="SaveAccountPara" runat="server" Height="32px" OnClick="SaveAccountPara_Click" Text="保存设置" Width="125px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="RepairAlarmVoucher" runat="server" Height="32px" OnClick="RepairAlarmVoucher_Click" Text="修复超限凭证状态" Width="125px" /></td>
            </tr>
        </table>
        <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
