<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="SanZi.Web.tousu2.Show" Title="显示页" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查看举报</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="frmTousu" runat="server">
    <div align="center">
          <table style="width:600px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td colspan="2" class="tableTitle2">查看举报</td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">单位名称：</td>
                <td class="tableContent">
                    <asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label>
                </td>
            <tr style="background:#ffffff;">
                <td class="tableTitle">举报内容：</td>
                <td class="tableContent">
                    <asp:Label ID="lblContent" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">举报时间：</td>
                <td class="tableContent">
                    <asp:Label ID="lblTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">IP：</td>
                <td class="tableContent">
                    <asp:Label ID="lblIP" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td colspan="2">
                    <asp:Button ID="btnClose" runat="server" Text=" 关闭 " onclick="btnClose_Click" />
                </td>
            </tr>
         </table>
    </div>
    </form>
</body>
</html>
