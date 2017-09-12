<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cpjg.aspx.cs" Inherits="SanZi.Web.CePing.cpjg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>民主测评结果录入</title>
        <link href="../Style.css" type="text/css" rel="stylesheet"/>
        <script src="../js/calendar1.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            font-family: 黑体;
            font-size: 18pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellspacing="0" cellpadding="0" width="100%" style="width:600px;border:1px solid #000800;">
    <tr>
    <td height="60px">
        <asp:TextBox ID="tbcun" runat="server" Width="100px" Font-Size="18pt" 
            Font-Names="黑体" Height="25px"></asp:TextBox>
        <span class="style1">村三资管理民主测评结果</span></td>
    </tr>
    <tr>
    <td align="left" height="100px">&nbsp;&nbsp;&nbsp;&nbsp;根据《<asp:TextBox ID="tbxian" 
            runat="server" Width="100px"></asp:TextBox>
        市三资管理方法》公示测评监督制有关规定，
        <asp:TextBox ID="tbcunm" runat="server" Width="100px"></asp:TextBox>村
        <br /><br />
        于<asp:Label ID="year1" runat="server"></asp:Label>年<asp:Label ID="month1" runat="server"></asp:Label>月<asp:Label ID="day1" runat="server"></asp:Label>日对村<asp:DropDownList
            ID="DropDownList1" runat="server" Width="81px">
            <asp:ListItem>上半年</asp:ListItem>
            <asp:ListItem>全年</asp:ListItem>
        </asp:DropDownList>三资管理情况进行测评，
        共<asp:TextBox ID="tbzrs" runat="server" Width="50px"></asp:TextBox>
        人参加（超过村民总数
        <br />
        <br />
        5%），其中满意<asp:TextBox ID="tbmyrs" runat="server" Width="50px"></asp:TextBox>
        人，
        基本满意<asp:TextBox ID="tbjbmy" runat="server" Width="50px"></asp:TextBox>
        人，不满意<asp:TextBox ID="tbbmyrs" runat="server" Width="50px"></asp:TextBox>
        人。</td></tr>
    <tr><td align="right" height="50px">
        <asp:TextBox ID="tbzhen" runat="server" Width="100px"></asp:TextBox>（镇、街道）三资托管服务中心</td>
        </tr>
        <tr><td align="right" height="50px">
            <asp:Label ID="year2" runat="server"></asp:Label>年<asp:Label ID="month2"
                runat="server"></asp:Label>月<asp:Label ID="day2" runat="server"></asp:Label>日</td></tr>
        <tr><td align="center">
            <asp:Button ID="btn_sure" runat="server" Text="确定" onclick="btn_sure_Click" /></td></tr>
    </table>
    </div>
    </form>
</body>
</html>
