<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CopyVoucher.aspx.cs" Inherits="SanZi.Web.AccountInit.CopyVoucher" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<script type="text/javascript">
    document.onkeydown = function () {
        if (event.keyCode == 13) {
            document.getElementById("Button1").click();
            return false;
        }
    }
</script>
</head>
<body style="text-align:center">
    <form id="form1" runat="server">
    <div style="width: 600px">
    <table cellpadding="0" cellspacing="0" style="width: 600px">
        <tr>
            <td class="t4" style="height: 28px; text-align: center">
                操作密码：<asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="t4" style="height: 50px; text-align: center">
                <asp:Button ID="Button1" runat="server" Height="28px" Text="进入操作" Width="72px" onclick="Button1_Click" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
