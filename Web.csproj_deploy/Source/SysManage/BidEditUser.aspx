<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_BidEditUser" Codebehind="BidEditUser.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("UserName").value=="")
    {
        $("UserName").focus();
        alert("登录名称不能为空！");
        return false;
    }
    if ($("TownList").value == "-") {
        $("TownList").focus();
        alert("请选择从属单位！");
        return false;
    }
    return true;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">编辑招投标管理用户信息</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    从属单位：</td>
                <td class="t1" style="width: 35%">
                    <asp:DropDownList ID="TownList" runat="server">
                    </asp:DropDownList></td>
                <td class="t1" style="width: 15%; text-align: right">
                    &nbsp;</td>
                <td class="t2" style="width: 35%">
                    &nbsp;</td>
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
                <td class="t4" colspan="4" style="height: 37px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="保存编辑" Width="100px" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" value="显示用户列表" onclick="document.location.href='BidUserManage.aspx';" /></td>
            </tr>
        </table>
    
    </div>
        <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
