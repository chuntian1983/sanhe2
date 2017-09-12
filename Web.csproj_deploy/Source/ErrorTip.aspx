<%@ Page Language="C#" AutoEventWireup="true" Inherits="_ErrorTip" Codebehind="ErrorTip.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>信息</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <br /><br /><br /><br /><br /><br />
        <table style="background:url(Images/xinxi.jpg)">
            <tr>
                <td style="width: 566px; height: 19px; vertical-align:bottom">
                <div id="ShowMachineCode" style="padding-left: 55px; text-align:left; font-size:9pt" runat="server"></div>
                </td>
            </tr>
            <tr>
                <td id="ShowMsg" style="width: 566px; height: 100px;" runat="server">&nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
