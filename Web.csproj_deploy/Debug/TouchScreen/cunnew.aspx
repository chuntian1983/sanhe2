<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cunnew.aspx.cs" Inherits="yuxi.cunnew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<title>-</title>
<style>
    .zi{  margin-top:10px;}
</style>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
</head>
<body bgcolor="#FFFFFF" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="text-align:center;">
<!-- Save for Web Slices (未标题-6.psd) -->
<form id="form1" runat="server">
<div style="text-align:center; background-image:url(images/cg.jpg); width:1024px; height:768px">
<table id="__01" width="724" height="400" border="0" cellpadding="0" cellspacing="0" style="margin-top:200px">
	<tr>
		<td colspan="2" style="height:80px; text-align:center">
            <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="XX-Large" 
                Font-Underline="False">LinkButton</asp:LinkButton>
        </td>
	</tr>
	<tr>
		<td>
			<img src="images/cun_07.jpg" width="300" height="67" style="cursor:hand" onclick="location.href='cin.aspx?case=1&kid=<%=kid %>'" alt=""></td>
		<td>
			<img alt="" height="69" 
                onclick="location.href='cin.aspx?case=4&amp;kid=<%=kid %>'" 
                src="images/cun_14.jpg" style="cursor:hand" width="300" /></td>
	</tr>
	<tr>
		<td>
			<img src="images/cun_12.jpg" width="300" height="68" style="cursor:hand" onclick="location.href='cin.aspx?case=3&kid=<%=kid %>'" alt=""></td>
		<td>
			<img alt="" height="67" 
                onclick="location.href='cin.aspx?case=6&amp;kid=<%=kid %>'" 
                src="images/cun_1866.jpg" style="cursor:hand" width="300" /></td>
	</tr>
	<tr>
		<td>
			<img src="images/cun_16.jpg" width="300" height="68" style="cursor:hand" onclick="location.href='cin.aspx?case=5&kid=<%=kid %>'" alt=""></td>
		<td>
			<img src="images/cun_18.jpg" width="300" height="67" style="cursor:hand" onclick="location.href='index.aspx'" alt=""></td>
	</tr>
	<tr>
		<td colspan="2" height=58>
        <div>
            <asp:ImageButton ID="ImageButton1" runat="server" 
                ImageUrl="~/images/1_03(2).gif" onclick="ImageButton1_Click" Visible=false /></div>
        </td>
	</tr>
	</table>
</div>
</form>
<!-- End Save for Web Slices -->
</body>
</html>
