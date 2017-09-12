<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_EditUser" Codebehind="EditUser.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>编辑操作员信息</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("RealName").value=="")
    {
        $("RealName").focus();
        alert("真实姓名不能为空！");
        return false;
    }
    if($("UserName").value=="")
    {
        $("UserName").focus();
        alert("登录名称不能为空！");
        return false;
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
function SelPower(v)
{
    var noCheck="Powers_999,";
    var cbContainer = document.getElementById("Powers");
    var items = cbContainer.getElementsByTagName("input");
    switch(v)
    {
        case "0":
            noCheck="Powers_3,";
            break;
        case "1":
            noCheck="Powers_2,Powers_4,";
            break;
        case "2":
            //noCheck="Powers_2,";
            break;
    }
    for(var i = 0; i < items.length; i++)
    {
        if(items[i].type=="checkbox")
        {
           items[i].checked=(noCheck.indexOf(items[i].id+",")==-1);
        }
    }
}
</script>
</head>
<body style="text-align:center">
    <form id="form1" runat="server">
    <div style="text-align:left;width: 750px;">
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">编辑操作员信息</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    真实姓名：</td>
                <td class="t1" style="width: 35%;">
                    <asp:TextBox ID="RealName" runat="server" Width="252px" Height="16px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">&nbsp;</td>
                <td class="t2" style="width: 35%;">&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    登录名称：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="UserName" runat="server" Width="252px" Height="16px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    登录密码：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="Password" runat="server" Width="252px" Height="16px" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    账套列表：</td>
                <td class="t2" colspan="3">
                    <asp:CheckBoxList ID="Accounts" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                        Width="634px">
                    </asp:CheckBoxList></td>
            </tr>
            <tr>
                <td class="t1" rowspan="2" style="width: 15%; text-align: right">
                    权限列表：</td>
                <td class="t2" colspan="3" style="text-align: left; background:#f6f6f6">
                    <asp:RadioButtonList ID="PowerType" runat="server" RepeatDirection="Horizontal" Width="351px">
                        <asp:ListItem Value="0">录入记账员</asp:ListItem>
                        <asp:ListItem Value="1">审核员</asp:ListItem>
                        <asp:ListItem Value="2">记账员兼审核员</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="t2" colspan="3" style="text-align: left">
                    <asp:CheckBoxList ID="Powers" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="634px">
                        <asp:ListItem Value="000001" Selected="True">启用账套</asp:ListItem>
                        <asp:ListItem Value="000002" Selected="True">科目表维护</asp:ListItem>
                        <asp:ListItem Value="000003" Selected="True">填制凭证</asp:ListItem>
                        <asp:ListItem Value="000004">审核凭证</asp:ListItem>
                        <asp:ListItem Value="000005" Selected="True">记账凭证</asp:ListItem>
                        <asp:ListItem Value="000006" Selected="True">月末结转</asp:ListItem>
                        <asp:ListItem Value="000007" Selected="True">年末结转</asp:ListItem>
                        <asp:ListItem Value="000008" Selected="True">账簿查询</asp:ListItem>
                        <asp:ListItem Value="000014" Selected="True">凭证查询</asp:ListItem>
                        <asp:ListItem Value="000009" Selected="True">报表查询</asp:ListItem>
                        <asp:ListItem Value="000012" Selected="True">报表分析</asp:ListItem>
                        <asp:ListItem Value="000010" Selected="True">摘要维护</asp:ListItem>
                        <asp:ListItem Value="000011" Selected="True">账套设置</asp:ListItem>
                        <asp:ListItem Value="000015" Selected="True">报表编辑</asp:ListItem>
                        <asp:ListItem Value="000013" Selected="True">资产管理</asp:ListItem>
                        <asp:ListItem Value="000016" Selected="True">资源管理</asp:ListItem>
                        <asp:ListItem Value="000081" Selected="True">代管资金管理</asp:ListItem>
                    </asp:CheckBoxList></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 37px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="保存编辑" Width="100px" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" value="显示操作员列表" onclick="document.location.href='UserManage.aspx';" /></td>
            </tr>
        </table>
    
    </div>
        <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
