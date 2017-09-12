<%@ Page Language="C#" AutoEventWireup="true" Inherits="_BusinessList" Codebehind="BusinessList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>三资管理平台功能列表</title>
</head>
<body style="text-align: center;">
    <form id="form1" runat="server">
    <div style="margin-top: 60px; width: 932px; height: 430px; background-image:url(Images/BListBg.jpg); text-align:left">
        <table id="DoOverLay" cellpadding="0" cellspacing="0" style="width: 720px; margin-top: 150px; margin-left: 50px; text-align: center">
            <tr>
                <td style="height: 200px; width: 30%">
                    <a href="MainFrame.aspx"><img alt="资金管理" src="Images/finance.jpg" style="border-width:0px" /></a></td>
                <td style="width: 30%">
                    <a href="AssetFrame.aspx"><img alt="资产管理" src="Images/asset.jpg" style="border-width:0px" /></a></td>
                <td style="width: 30%">
                    <a href="ResourceFrame.aspx"><img alt="资源管理" src="Images/resource.jpg" style="border-width:0px" /></a></td>
            </tr>
        </table>
        <div style="text-align:right"><a href="LoginOut.aspx"><img alt="安全退出" src="Images/loginout.jpg" style="border-width:0px" /></a></div>
    </div>
    </form>
</body>
</html>
