<%@ Page Language="C#" AutoEventWireup="true" Inherits="_ChangePassword" Codebehind="ChangePassword.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("OldPassword").value=="")
    {
      $("OldPassword").focus();
      alert("旧密码不能为空！");
      return false;
    }
    if($("NewPassword").value=="")
    {
      $("NewPassword").focus();
      alert("新密码不能为空！");
      return false;
    }
    if($("ReNewPassword").value=="")
    {
      $("ReNewPassword").focus();
      alert("重复新密码不能为空！");
      return false;
    }
    if($("NewPassword").value!=$("ReNewPassword").value)
    {
      alert("新密码与重复新密码不相同！");
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
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    姓名：</td>
                <td class="t2" colspan="3" style="height: 25px">
                    <asp:TextBox ID="RealName" runat="server" BorderWidth="0px" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    用户名：</td>
                <td class="t2" colspan="3" style="height: 25px">
                    <asp:TextBox ID="UserName" runat="server" BorderWidth="0px" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    旧密码：</td>
                <td class="t2" colspan="3" style="height: 25px">
                    <asp:TextBox ID="OldPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    新密码：</td>
                <td class="t2" colspan="3" style="height: 25px">
                    <asp:TextBox ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    重复新密码：</td>
                <td class="t2" colspan="3" style="height: 25px; text-align: left">
                    <asp:TextBox ID="ReNewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t3" style="width: 15%; height: 34px; text-align: right">
                    &nbsp;
                </td>
                <td class="t4" colspan="3" style="height: 34px">
                    <asp:Button ID="Button1" runat="server" Text="保存" OnClick="Button1_Click" Width="105px" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
