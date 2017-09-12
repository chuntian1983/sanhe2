<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_SetSysParas" Codebehind="SetSysParas.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" style="width: 750px">
        <tr>
            <td class="t4" style="height: 28px; text-align: center">
                <span style="font-size: 16pt; font-family: 隶书">通用系统参数设置</span></td>
        </tr>
    </table>
    <br />
    <table cellpadding="0" cellspacing="0" style="width: 750px; text-align: center">
        <tr>
            <td class="t1" style="width:20%;height:25px">操作日志</td>
            <td class="t1" style="width:30%">
                <asp:CheckBoxList ID="rizhi" runat="server" RepeatDirection="Horizontal" Width="210px">
                    <asp:ListItem Value="0">乡镇可查看</asp:ListItem>
                    <asp:ListItem Value="1">乡镇可删除</asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td class="t1" style="width:20%">&nbsp;</td>
            <td class="t2" style="width:30%">&nbsp;</td>
        </tr>
        <tr>
            <td class="t3" style="height:25px">&nbsp;</td>
            <td class="t3">&nbsp;</td>
            <td class="t3">&nbsp;</td>
            <td class="t4">&nbsp;</td>
        </tr>
    </table>
    <br />
    <table cellpadding="0" cellspacing="0" style="width: 750px; height: 44px;">
        <tr>
            <td class="t4" style="height: 28px; text-align: center">
                <asp:Button ID="Button1" runat="server" Height="32px" Text="保存设置" Width="180px" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
