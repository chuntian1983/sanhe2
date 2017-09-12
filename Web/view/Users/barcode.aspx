<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="barcode.aspx.cs" Inherits="SanZi.Web.view.Users.barcode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link type="text/css" href="../../Images/css.css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" style="width: 750px">
        <tr>
            <td class="t4" style="height: 28px; text-align: center">
                <span style="font-size: 16pt; font-family: 隶书">条形码导出</span></td>
        </tr>
    </table>
    <br />
    <table cellpadding="0" cellspacing="0" style="width: 750px;">
        <tr>
            <td class="t1" style="height: 25px; width: 15%; text-align: center;">导出来源：</td>
            <td class="t1" style="height: 25px; width: 35%">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                    RepeatDirection="Horizontal" Width="200px">
                    <asp:ListItem Selected="True">新增</asp:ListItem>
                    <asp:ListItem>未使用</asp:ListItem>
                </asp:RadioButtonList>
                
            </td>
            <td class="t1" style="height: 25px; width: 15%; text-align: center;">条形码数量：</td>
            <td class="t2" style="height: 25px; width: 35%">
                <asp:TextBox ID="TextBox1" runat="server">10</asp:TextBox></td>
        </tr>
        <tr>
            <td class="t4" style="height: 325px; vertical-align:top" colspan="4">
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="10" 
                    RepeatDirection="Horizontal" Width="99%">
                </asp:CheckBoxList>&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <table cellpadding="0" cellspacing="0" style="width: 750px; height: 44px;">
        <tr>
            <td class="t4" style="height: 28px; text-align: center">
                <asp:Button ID="Button1" runat="server" Height="32px" Text="导出" Width="180px" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
