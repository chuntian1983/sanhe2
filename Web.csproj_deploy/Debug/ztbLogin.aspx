<%@ Page Language="C#" AutoEventWireup="true" Inherits="_ztbLogin" Codebehind="ztbLogin.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统登录</title>
<link type="text/css" href="Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function resetText()
{
    document.getElementById("txtUserName").value="";
    document.getElementById("txtPassword").value="";
    document.getElementById("txtUserName").focus();
    return false;
}
</script>
</head>
<body style="margin:0px; background-color:#e5e5e5;">
    <form id="form1" runat="server">
    <div style="background-image:url('images/loginbg.jpg');width:1003px; height:600px;">
    <table cellpadding="0" cellspacing="0" style="width:600px; height:358px; margin-top:125px; margin-left:255px">
        <tr>
            <td style="height: 150px; text-align: center" colspan="5">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center; width:40%">&nbsp;</td>
            <td style="text-align: right; width:17%; height: 30px;">
                </td>
            <td style="text-align: left; width:29%">
            <asp:TextBox ID="txtUserName" runat="server" Width="120px" Height="18px" BorderWidth="1px" BorderColor="gray"></asp:TextBox></td>
            <td style="text-align: center; width:10%" rowspan="3">
            </td>
            <td style="text-align: center; width:4%">&nbsp;</td>
        </tr>
        <tr>
            <td style="height:5px" colspan="5"></td>
        </tr>
        <tr>
            <td style="text-align: center">&nbsp;</td>
            <td style="text-align: right; height: 30px;">
                </td>
            <td style="text-align: left">
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="120px" Height="18px" BorderWidth="1px" BorderColor="gray"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: center">&nbsp;</td>
            <td style="height: 50px;" colspan="2">&nbsp;
                <div style="margin-left:25px">
                <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="Images/loginbtn2.jpg" OnClick="btnLogin_OnClick" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="Images/loginreset.jpg" />
                </div>
            </td>
            <td style="text-align: center" colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center; height:85px" colspan="5">&nbsp;</td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
