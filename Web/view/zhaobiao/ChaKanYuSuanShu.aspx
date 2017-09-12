<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChaKanYuSuanShu.aspx.cs" Inherits="SanZi.Web.zhaobiao.ChaKanYuSuanShu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>查看村预算书</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
     <!--NoPrintStart--><center><input type="button" value="打印" style="width:120px" onclick="window.open('../../PrintWeb.html')"/></center><!--NoPrintEnd-->
    <table style="width:760px;text-align:center;font-weight:bold;font-size:25px;font-family:黑体;margin-top:15px;" >
	    <tr>
		    <td><asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></td>
	    </tr>
    </table>
    <table style="width:760px;border:1px solid #000800;margin-top:5px;">
	    <tr>
		    <td class="tableTitle">&nbsp;&nbsp;&nbsp;&nbsp;一、项目名称：</td>
	    </tr>
	    <tr style="text-align:left;">
		    <td>&nbsp;<asp:Label ID="lblProjectName2" runat="server" Text=""></asp:Label></td>
	    </tr>
	    <tr>
		    <td class="tableTitle">&nbsp;&nbsp;&nbsp;&nbsp;二、项目实施时间：</td>
	    </tr>
	    <tr style="text-align:left;">
		    <td>&nbsp;<asp:Label ID="lblPuttingTime" runat="server" Text=""></asp:Label></td>
	    </tr>
	    <tr>
		    <td class="tableTitle">&nbsp;&nbsp;&nbsp;&nbsp;三、项目明细：</td>
	    </tr>
	    <tr style="text-align:left;">
		    <td>&nbsp;<asp:Label ID="lblDetail" runat="server" Text=""></asp:Label></td>
	    </tr>

	    <tr style="text-align:right;">
		    <td><asp:Label ID="lblVillage" runat="server" Text=""></asp:Label><td>
	    </tr>
	    <tr style="text-align:right;">
		    <td><asp:Label ID="lblDate" runat="server" Text=""></asp:Label><td>
	    </tr>
    </table>

    </form>
</body>
</html>
