<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChaKanZhaoBiaoGongGao.aspx.cs" Inherits="SanZi.Web.zhaobiao.ChaKanZhaoBiaoGongGao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>查看招标公告</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
     <!--NoPrintStart--><center><input type="button" value="打印" style="width:120px" onclick="window.open('../../PrintWeb.html')"/></center><!--NoPrintEnd-->
<table style="width:760px;text-align:center;font-weight:bold;font-size:25px;font-family:黑体;margin-top:15px;">
	<tr>
		<td>招标公告</td>
	</tr>
</table>
<table style="width:760px;">
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;经民主议事六步工作法，<asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label>村委会决定通过公开投标方式对<asp:Label ID="lblProjectName" runat="server" Text=""></asp:Label>进行招标，受其委托，乡镇竞价招投标办公室将有关事项公告如下：</td>
	</tr>
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;一、招标内容及项目情况：</td>
	</tr>
	<tr style="height:200px;">
		<td style="text-align:left;">&nbsp;<asp:Label ID="lblZhaoBiaoNeiRong" runat="server" Text=""></asp:Label></td>
	</tr>
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;二、报名条件：</td>
	</tr>
	<tr style="height:100px;">
		<td style="text-align:left;">&nbsp;<asp:Label ID="lblBaoMingTiaoJian" runat="server" Text=""></asp:Label></td>
	</tr>
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;三、招标程序：</td>
	</tr>
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;1、本公告发布之日起7日后，由招投标办公室根据报名情况确定3家以上投标单位。</td>
	</tr>
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;2、招投标办公室确定招标时间、地点。</td>
	</tr>
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;3、召开招标会，并根据<asp:Label ID="lblYuanZe" runat="server" Text=""></asp:Label>原则，产生中标人（单位）。</td>
	</tr>
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;4、与中标人（单位）签订合同。</td>
	</tr>
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;四、报名事项：</td>
	</tr>
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;1、时间：<asp:Label ID="lblStartDate" runat="server" Text=""></asp:Label>----<asp:Label ID="lblEndDate" runat="server" Text=""></asp:Label>。</td>
	</tr>
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;2、地点：<asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></td>
	</tr>
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;3、联系人及电话：<asp:Label ID="lblTel" runat="server" Text=""></asp:Label></td>
	</tr>
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;4、报名时持本人（法人）有效证明原件及复印件。</td>
	</tr>
	<tr>
		<td style="text-align:right;"><asp:Label ID="Label1" runat="server" Text=""></asp:Label>竞价招投标办公室<td>
	</tr>
	<tr>
		<td style="text-align:right;"><asp:Label ID="lblPubDate" runat="server" Text=""></asp:Label><td>
	</tr>
</table>

    </form>
</body>
</html>
