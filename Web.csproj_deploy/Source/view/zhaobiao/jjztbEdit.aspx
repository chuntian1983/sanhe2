<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jjztbEdit.aspx.cs" Inherits="SanZi.Web.zhaobiao.jjztbEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>竞价招投标记录</title>
        <link href="../Style.css" type="text/css" rel="stylesheet"/>
        <script src="../js/calendar2.js" type="text/javascript"></script>
    <style type="text/css">
        .title
        {
            font-size: x-large; line-height:60px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table  cellspacing="0" cellpadding="0" width="100%"  style="width:600px">
    <tr>
    <td colspan="3" style=" height:40px" class="title">竞价招投标记录</td>
    </tr>
    <tr> 
    <td style=" width:30px;height:40px"></td>
    <td align="left" style="height:30px;width:540px" class="tableTitle">项目名称：<asp:DropDownList ID="tbxmmc" runat="server">
            </asp:DropDownList></td>
    <td style=" width:30px"></td>
    </tr>
    <tr><td></td>
    <td>
    <table cellspacing="0" cellpadding="0" width="100%" style="width:540px;border:1px solid #000800;">
    <tr>
    <td style="width:15%;height:40px; border-bottom:solid 1px #000800" class="tableTitle">时&nbsp;&nbsp;&nbsp;间</td>
    <td align="left" style="width:35%; border-bottom:solid 1px #000800; border-left:solid 1px #000800">
        <asp:TextBox ID="tbsubtime" 
            runat="server"></asp:TextBox>
        </td> 
    <td style="width:15%; border-bottom:solid 1px #000800; border-left:solid 1px #000800" class="tableTitle">地&nbsp;&nbsp;&nbsp;点</td>
    <td align="left" style="width:35%; border-bottom:solid 1px #000800; border-left:solid 1px #000800">
        <asp:TextBox ID="tbadress" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td style="width:15%;height:40px; border-bottom:solid 1px #000800" class="tableTitle">参加人员</td>
    <td align="left" style="width:35%; border-bottom:solid 1px #000800; border-left:solid 1px #000800">
        <asp:TextBox ID="tbcyry" runat="server"></asp:TextBox>
        </td> 
    <td style="width:15%; border-bottom:solid 1px #000800; border-left:solid 1px #000800" class="tableTitle">主持人</td>
    <td align="left" style="width:35%; border-bottom:solid 1px #000800; border-left:solid 1px #000800">
        <asp:TextBox ID="tbzcr" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td style="width:15%;height:40px; border-bottom:solid 1px #000800" class="tableTitle">唱标人</td>
    <td align="left" style="width:35%; border-bottom:solid 1px #000800; border-left:solid 1px #000800">
        <asp:TextBox ID="tbcbr" runat="server"></asp:TextBox>
        </td> 
    <td style="width:15%; border-bottom:solid 1px #000800; border-left:solid 1px #000800" class="tableTitle">记录人</td>
    <td align="left" style="width:35%; border-bottom:solid 1px #000800; border-left:solid 1px #000800">
        <asp:TextBox ID="tbjlr" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td style="width:15%; border-bottom" class="tableTitle">
    主<br />
    要<br />
    内<br />
    容</td>
    <td style="width:75%; border-bottom:solid 1px #000800; border-left:solid 1px #000800" colspan="3" align="left">
        <asp:TextBox ID="tbzynr" runat="server" TextMode="MultiLine" Height="340px" 
            Width="456px"></asp:TextBox></td> 

    </tr>
    </table>
    </td><td></td></tr>
    <tr><td colspan="4" style="height:30px">
        <asp:Button ID="btn_sure" runat="server" Text="确定" onclick="btn_sure_Click" /></td></tr></table>
    </div>
    </form>
</body>
</html>

