<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cyssEdit.aspx.cs" Inherits="SanZi.Web.zhaobiao.cyssEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>村预算书</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table cellspacing="0" cellpadding="0" width="100%" style="width:600px;border:1px solid #000800;">
        <tr><td width="40px"></td><td height="60px">
            <asp:TextBox ID="tbyss"
                runat="server" Width="150px"></asp:TextBox>预算书</td>
                <td width="40px"></td></tr>
        <tr><td width="40px"></td><td align="left">一、项目名称：</td>
        <td width="40px"></td></tr>
        <tr><td width="40px"></td><td align="left">
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="tbxmmc" runat="server">
            </asp:DropDownList>
            </td>
            <td width="40px"></td></tr>
        <tr><td width="40px"></td><td align="left">二、项目实施时间：</td>
        <td width="40px"></td></tr>
        <tr><td width="40px"></td><td align="left">
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="tbxmsssj" runat="server" Width="300px"></asp:TextBox>
            </td><td width="40px"></td></tr>
        <tr><td width="40px"></td><td align="left">三、项目明细：</td><td width="40px"></td></tr>
        <tr><td width="40px"></td><td align="left">
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="tbxmmx" runat="server" Width="490px" 
                Height="200px" TextMode="MultiLine"></asp:TextBox>
            </td><td width="40px"></td></tr>
        <tr><td width="40px"></td>
        <td align="right" height="50px">
            <asp:TextBox ID="tbcwh" runat="server" Width="90px"></asp:TextBox>村委会</td><td width="40px"></td></tr>
        <tr><td width="40px"></td>
        <td align="right" height="50px">
            <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
            </td></tr>
        <tr><td width="40px"></td><td height="40px">
            <asp:Button ID="btn_sure" runat="server" Text="确定" onclick="btn_sure_Click" /></td><td width="40px"></td></tr>
        </table>
    </div>
    </form>
</body>
</html>
