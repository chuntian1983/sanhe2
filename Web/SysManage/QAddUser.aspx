<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_QAddUser" Codebehind="QAddUser.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("RealName").value=="")
    {
        $("RealName").focus();
        alert("查询站点名称不能为空！");
        return false;
    }
    if($("UserName").value=="")
    {
        $("UserName").focus();
        alert("登录名称不能为空！");
        return false;
    }
    if($("Password").value=="")
    {
        $("Password").focus();
        alert("登录密码不能为空！");
        return false;
    }
    if($("AccountList").value=="000000")
    {
        $("AccountList").focus();
        alert("请选择查询账套！");
        return false;
    }
    if(!isChecked("Powers"))
    {
        if(!confirm("您没有选择任何权限，是否继续添加操作员！"))
        {
            return false;
        }
    }
    return true;
}
function isChecked(id)
{
    var selFlag=false;
    var gridView1 = document.getElementById(id);
    var items = gridView1.getElementsByTagName("input");
    for(var i = 0; i < items.length; i++)
    {
        if(items[i].type=="checkbox"&&items[i].checked)
        {
           selFlag=true;
           break;
        }
    }
    return selFlag;
}
function OnSelAccount()
{
    if($("AccountList").value!="000000")
    {
        var o=$("AccountList");
        var a=o.options[o.selectedIndex].text;
        $("RealName").value=a;
        $("UserName").value=a;
    }
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">添加村级查询站点</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    站点名称：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="RealName" runat="server" Width="252px" Height="16px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    查询账套：</td>
                <td class="t2" style="width: 35%;">
                    <asp:DropDownList ID="AccountList" runat="server">
                        <asp:ListItem Value="000000">选择账套</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    登录名称：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="UserName" runat="server" Width="252px" Height="16px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    登录密码：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="Password" runat="server" Width="252px" Height="16px">888888</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" rowspan="1" style="width: 15%; text-align: right">
                    权限列表：</td>
                <td class="t2" colspan="3" style="text-align: left">
                    <asp:CheckBoxList ID="Powers" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="634px">
                        <asp:ListItem Value="100001">总账查询</asp:ListItem>
                        <asp:ListItem Value="100002">明细分类账查询</asp:ListItem>
                        <asp:ListItem Value="100003">应收、付账款</asp:ListItem>
                        <asp:ListItem Value="100004">凭证列表查询</asp:ListItem>
                        <asp:ListItem Value="100005">凭证打印输出</asp:ListItem>
                        <asp:ListItem Value="100006">科目余额表</asp:ListItem>
                        <asp:ListItem Value="100008">收支明细表</asp:ListItem>
                        <asp:ListItem Value="100009">资产负债表</asp:ListItem>
                        <asp:ListItem Value="100010">内部往来余额表</asp:ListItem>
                        <asp:ListItem Value="100011">收益分配表</asp:ListItem>
                        <asp:ListItem Value="100012">财务公开榜(月表)</asp:ListItem>
                        <asp:ListItem Value="100013">财务公开榜(季表)</asp:ListItem>
                        <asp:ListItem Value="100014">收支逐笔公开榜</asp:ListItem>
                        <asp:ListItem Value="100015">村级收入情况</asp:ListItem>
                        <asp:ListItem Value="100016">村级支出情况</asp:ListItem>
                        <asp:ListItem Value="100017">村级福利费收入表</asp:ListItem>
                        <asp:ListItem Value="100018">村级福利费支出表</asp:ListItem>
                        <asp:ListItem Value="100019">财务收支分析表</asp:ListItem>
                        <asp:ListItem Value="100020">福利费收支分析表</asp:ListItem>
                        <asp:ListItem Value="100007">资产负债分析表</asp:ListItem>
                        <asp:ListItem Value="100021">资产明细表</asp:ListItem>
                        <asp:ListItem Value="100022">资产明细账</asp:ListItem>
                        <asp:ListItem Value="100023">资源明细表</asp:ListItem>
                        <asp:ListItem Value="999999">全部选择</asp:ListItem>
                    </asp:CheckBoxList></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 37px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" Width="151px" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" onclick="document.location.href='QUserManage.aspx';" type="button" value="显示查询站点列表" /></td>
            </tr>
        </table>
    </div>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
