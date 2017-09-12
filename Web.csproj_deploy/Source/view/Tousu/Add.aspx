<%@ Page Language="C#"  AutoEventWireup="true" Codebehind="Add.aspx.cs" Inherits="SanZi.Web.tousu.Add"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>我要举报</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
<form id="form1" runat="server">
    <div align="center">
         <table style="width:500px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td colspan="2" class="tableTitle2">我要投诉
                <br /><asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">单位名称：</td>
                <td class="tableContent">
                    <asp:TextBox id="txtDeptName" runat="server" width="150px" ></asp:TextBox>
                    <input type="hidden" name="hidDeptID" id="hidDeptID" value="1"/>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">投诉内容：</td>
                <td class="tableContent">
                    <asp:TextBox id="txtContent" runat="server" Width="400px" TextMode="MultiLine" 
            Height="110px"></asp:TextBox>
                    <font color="red"><strong>*</strong>您提交的内容只有相关部门可以查看，不对外进行公开。</font>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td colspan="2"><asp:Button ID="btnAdd" runat="server" Text="· 提交 ·" OnClick="btnAdd_Click" ></asp:Button></td>
            </tr>
         </table>
    </div>
</form>
</body>
</html>

