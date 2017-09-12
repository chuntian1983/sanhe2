<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="SanZi.Web.pibanka.Show" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查看现金支出议定审核批办卡</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
   <form id="formAddUser" runat="server">
    <div align="center">
         <table style="width:600px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td colspan="2" class="tableTitle2">现金支出议定审核批办卡</td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">单位名称：</td>
                <td class="tableContent">
                    &nbsp;
                    <asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">议定事由：</td>
                <td class="tableContent">
                    &nbsp;<asp:Label ID="lblOutReason" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">支出金额：</td>
                <td class="tableContent">
                    &nbsp;<asp:Label ID="lblOutMoney" runat="server" Text=""></asp:Label>元
                </td>
            </tr>
              <tr style="background: #ffffff;">
                <td class="tableTitle">
                    党员代表意见：
                </td>
                <td class="tableContent">
                     于 <asp:Label ID="time1" runat="server"></asp:Label>就议案征求党员意见，（<asp:Label ID="value1" runat="server"></asp:Label>）通过。
                </td>
                  
            </tr>
             <tr style="background: #ffffff;">
                <td class="tableTitle">
                    议案公开：
                </td>
                <td class="tableContent">
                     于 <asp:Label ID="time2" runat="server"></asp:Label>至<asp:Label ID="time3" runat="server"></asp:Label>议案公开（不少于三天）。
                </td>
            </tr>
             <tr style="background: #ffffff;">
                <td class="tableTitle">
                    村民代表会议决议：
                </td>
                <td class="tableContent">
                     于<asp:Label ID="time4" runat="server"></asp:Label>召开村民代表会议决议,共 <asp:Label ID="value2" runat="server"></asp:Label>人参加，<asp:Label ID="value3" runat="server"></asp:Label>人通过。
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td class="tableTitle">
                    结果公开：
                </td>
                <td class="tableContent">
                     于 <asp:Label ID="time5" runat="server"></asp:Label>至<asp:Label ID="time6" runat="server"></asp:Label>结果公开（不少于三天）。
                   
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">审批人信息：</td>
                <td class="tableContent">
                    <asp:Label ID="lblShenPiRen" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">审批时间：</td>
                <td class="tableContent">
                    &nbsp;<asp:Label ID="lblPassTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td colspan="2">
                    <asp:Button ID="btnClose" runat="server" Text=" 关闭 " onclick="btnClose_Click" />
                    &nbsp;
                </td>
            </tr>
         </table>
    </div>
    </form>
</body>
</html>
