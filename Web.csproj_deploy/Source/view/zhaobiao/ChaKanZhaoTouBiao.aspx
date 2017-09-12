<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChaKanZhaoTouBiao.aspx.cs" Inherits="SanZi.Web.zhaobiao.ChaKanZhaoTouBiao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>查看竞价招投标记录</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
     <!--NoPrintStart--><center><input type="button" value="打印" style="width:120px" onclick="window.open('../../PrintWeb.html')"/></center><!--NoPrintEnd-->
<table style="width:760px;text-align:center;font-weight:bold;font-size:25px;font-family:黑体;margin-top:15px;">
	<tr>
		<td>查看竞价招投标记录</td>
	</tr>
</table>
<table style="width:760px;">
	<tr>
		<td style="font-weight:bold;text-align:left;">项目名称：<asp:Label ID="lblProjectName" runat="server" Text=""></asp:Label></td>
	</tr>
</table>
<table style="width:760px;border:1px solid #000800;text-align:center;margin-top:5px;background:#000800;" cellspacing="1" cellPadding="3"  >
	<tr style="background:#ffffff;">
		<td class="tableTitle">时间</td>
		<td class="tableContent">&nbsp;<asp:Label ID="lblShiJian" 
                runat="server" Text=""></asp:Label></td>
		<td class="tableTitle">地点</td>
		<td class="tableContent">&nbsp;<asp:Label ID="lblDiDian" 
                runat="server" Text=""></asp:Label></td>
	</tr>
	<tr style="background:#ffffff;">
		<td class="tableTitle">参加人员</td>
		<td class="tableContent">&nbsp;<asp:Label ID="lblCanJiaRenYuan" 
                runat="server" Text=""></asp:Label></td>
		<td class="tableTitle">主持人</td>
		<td class="tableContent">&nbsp;<asp:Label ID="lblZhuChiRen" 
                runat="server" Text=""></asp:Label></td>
	</tr>
	<tr style="background:#ffffff;">
		<td class="tableTitle">唱标人</td>
		<td class="tableContent">&nbsp;<asp:Label ID="lblChangBiaoRen" 
                runat="server" Text=""></asp:Label></td>
		<td class="tableTitle">记录人</td>
		<td class="tableContent">&nbsp;<asp:Label ID="lblJiLuRen" 
                runat="server" Text=""></asp:Label></td>
	</tr>
	<tr style="background:#ffffff;">
		<td class="tableTitle">主要内容</td>
		<td class="tableContent" colspan="3">&nbsp;
		<asp:Label ID="lblZhuYaoNeiRong" runat="server" Text=""></asp:Label>
		</td>

	</tr>
	<tr style="background:#ffffff;">
	    <td align="center" colspan="4">
	        <asp:Button ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click" />
	    </td>
	</tr>
</table>
    </form>
</body>
</html>
