<%@ Page Language="C#"  AutoEventWireup="true" Codebehind="Add.aspx.cs" Inherits="SanZi.Web.ceping.Add" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>村务工作综合测评</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
<form id="form1" runat="server">
<table cellspacing="0" cellpadding="0" width="100%" style="width:400px">
<tr>
<td align="left">我要测评：</td></tr>
<tr>
    <td>
<asp:TextBox id="txtDeptName" runat="server" width="150px" value="点击选择所属单位" onfocus="if(this.value=='点击选择所属单位'){this.value='';}"  onblur="if(this.value==''){this.value='点击选择所属单位';}" ></asp:TextBox>
村务工作综合测评
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="year" runat="server"></asp:Label>年<asp:Label ID="month" runat="server"></asp:Label>月<asp:Label
            ID="day" runat="server"></asp:Label>日
    </td>
</tr>
</table>
<table cellspacing="0" cellpadding="0" width="100%" style="width:400px;border:1px solid #000800;margin-top:5px;" >
    <tr><td align="left" style="border-bottom: 1px solid #000800;">欢迎参与
                    <input type="hidden" name="hidDeptID" id="hidDeptID" value="1"/>测评</td></tr>
    <tr><td align="left" style="border-bottom: 1px solid #000800;">你对本村村务工作的综合评价</td></tr>
	<tr>
	<td height="25" align="left" style="border-bottom: 1px solid #000800;">
        <asp:RadioButtonList ID="txtOptionChecked" runat="server" Width="400px">
            <asp:ListItem Value="1">满意</asp:ListItem>
            <asp:ListItem Value="2">基本满意</asp:ListItem>
            <asp:ListItem Value="3">不满意</asp:ListItem>
        </asp:RadioButtonList>
	</td></tr>
	<tr>
	<td height="25"><div align="center">
		<asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click" ></asp:Button>
	</div></td></tr>
</table>
</form>
</body>
</html>
