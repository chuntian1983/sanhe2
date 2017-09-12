<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChaKanCePingJieGuo.aspx.cs" Inherits="SanZi.Web.CePing.ChaKanCePingJieGuo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>查看民主测评结果</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
<table style="width:760px;text-align:center;font-weight:bold;font-size:25px;font-family:黑体;margin-top:15px;">
	<tr>
		<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                ID="lblTitle" runat="server" Text=""></asp:Label></td>
	</tr>
</table>
<table style="width:760px;">
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblContent" runat="server" Text=""></asp:Label></td>
	</tr>

	<tr>
		<td style="text-align:right;">
		<asp:Label ID="lblTime" runat="server" Text=""></asp:Label>
		<td>
	</tr>
	<tr>
	    <td><asp:Button ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click" /></td>
	</tr>
</table>

    

    </form>
</body>
</html>
