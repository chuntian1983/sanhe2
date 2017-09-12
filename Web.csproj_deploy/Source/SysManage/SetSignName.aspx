<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_SetSignName" Codebehind="SetSignName.aspx.cs" %>

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
                <span style="font-size: 16pt; font-family: 隶书">设置报表底部签字</span></td>
        </tr>
    </table>
    <br />
    <table cellpadding="0" cellspacing="0" style="width: 750px">
        <tr>
            <td class="t1" style="height: 25px; width: 32%"><asp:TextBox ID="SignName0" runat="server" BorderWidth="1px" Width="200px"></asp:TextBox></td>
            <td class="t1" style="height: 25px; width: 32%"><asp:TextBox ID="SignName1" runat="server" BorderWidth="1px" Width="200px"></asp:TextBox></td>
            <td class="t2" style="height: 25px; width: 35%"><asp:CheckBox ID="IsShowDate" runat="server" Text="显示打印日期" Checked="True" /></td>
        </tr>
        <tr>
            <td class="t3" style="height: 25px"><asp:TextBox ID="SignName2" runat="server" BorderWidth="1px" Width="200px"></asp:TextBox></td>
            <td class="t3" style="height: 25px"><asp:TextBox ID="SignName3" runat="server" BorderWidth="1px" Width="200px"></asp:TextBox></td>
            <td class="t4" style="height: 25px"><asp:TextBox ID="SignName4" runat="server" BorderWidth="1px" Width="200px"></asp:TextBox></td>
        </tr>
    </table>
    <br />
    <table cellpadding="0" cellspacing="0" style="width: 750px; height: 44px;">
        <tr>
            <td class="t4" style="height: 28px; text-align: center">
                <asp:Button ID="Button1" runat="server" Height="32px" Text="保存设置" Width="180px" OnClick="Button1_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" Height="32px" Text="恢复默认值" Width="180px" OnClick="Button2_Click" />
            </td>
        </tr>
    </table>
    </div>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
