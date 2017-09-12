<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChaKanZhongBiaoGongGao.aspx.cs" Inherits="SanZi.Web.zhaobiao.ChaKanZhongBiaoGongGao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>查看中标公告</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
     <!--NoPrintStart--><center><input type="button" value="打印" style="width:120px" onclick="window.open('../../PrintWeb.html')"/></center><!--NoPrintEnd-->
<table style="width:760px;text-align:center;font-weight:bold;font-size:25px;font-family:黑体;margin-top:15px;">
	<tr>
		<td>中标公告</td>
	</tr>
</table>
<table style="width:760px;">
	<tr>
		<td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;按照《<asp:Label ID="lblNCMC" 
                runat="server" Text=""></asp:Label>农村集体资产、资源竞价处置和工程建设招投标管理办法》规定，于<asp:Label ID="lblStartDate" 
                runat="server" Text=""></asp:Label>到<asp:Label ID="lblEndDate" 
                runat="server" Text=""></asp:Label>对<asp:Label ID="lblProjectName" 
                runat="server" Text=""></asp:Label>进行了公开招标，共有<asp:Label ID="lblDanWeiA" 
                runat="server" Text=""></asp:Label>;、<asp:Label ID="lblDanWeiB" 
                runat="server" Text=""></asp:Label>、<asp:Label ID="lblDanWeiC" 
                runat="server" Text=""></asp:Label>等<asp:Label ID="lblNum" 
                runat="server" Text=""></asp:Label>家单位（个人）参加了投标。经招投标，确定<asp:Label ID="lblZhongBiao" 
                runat="server" Text=""></asp:Label>为中标单位，请于30内持有效证明前来签署合同。如对本次招标投标活动的真实性和合法性有异议，请于5天内向乡镇招投标办公室提出书面意见，举报人应提供真实姓名和电话，否则将视为无效。</td>
	</tr>
	<tr>
		<td style="text-align:right;"><asp:Label ID="lblOffice" 
                runat="server" Text=""></asp:Label>竞价招投标办公室<td>
	</tr>
	<tr>
		<td style="text-align:right;"><asp:Label ID="lblPubDate" 
                runat="server" Text=""></asp:Label><td>
            
	</tr>
	<tr>
	    <td align="center">
	        <asp:Button ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click" />
	    </td>
	</tr>
</table>
    </form>
</body>
</html>
