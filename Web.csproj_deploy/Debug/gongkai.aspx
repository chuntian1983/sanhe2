<%@ Page Language="C#" AutoEventWireup="true" Inherits="_GongKai" Codebehind="gongkai.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>三资查询</title>
</head>
<body style="text-align: center;">
    <form id="form1" runat="server">
    <div style="margin-top: 60px; width: 932px; height: 430px; background-image:url(Images/blistgk.jpg); text-align:left">
        <table id="DoOverLay" cellpadding="0" cellspacing="0" style="width: 720px; margin-top: 150px; margin-left: 50px; text-align: center">
            <tr>
                <td style="height: 200px; width: 30%">
                    <a href="QueryFrame2.aspx?t=2"><img alt="资金查询" src="Images/finance2.jpg" style="border-width:0px" /></a></td>
                <td style="width: 30%">
                    <a href="QueryFrame2.aspx?t=4"><img alt="资产查询" src="Images/asset2.jpg" style="border-width:0px" /></a></td>
                <td style="width: 30%">
                    <a href="QueryFrame2.aspx?t=5"><img alt="资源查询" src="Images/resource2.jpg" style="border-width:0px" /></a></td>
            </tr>
        </table>
        <div style="text-align:right"><a href="LoginOut.aspx"><img alt="安全退出" src="Images/loginout.jpg" style="border-width:0px" /></a></div>
    </div>
    </form>
</body>
</html>
