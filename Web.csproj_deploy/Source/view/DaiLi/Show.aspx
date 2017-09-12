<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="SanZi.Web.daili.Show" Title="显示页" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查看代理申请</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="formChaKanDaiLi" runat="server">
    <div align="center">
          <table style="width:600px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
            <tr style="background:#ffffff;">
                <td colspan="4" class="tableTitle2">查看代理申请</td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">申请村：</td>
                <td class="tableContent">
                    <asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label>
                </td>
                <td class="tableTitle">日期：</td>
                <td class="tableContent">
                    <asp:Label ID="lblApplyDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">项目背景：</td>
                <td class="tableContent" colspan="3">
                    <asp:Label ID="lblBackGround" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">项目名称：</td>
                <td class="tableContent">
                    <asp:Label ID="lblProjectName" runat="server" Text=""></asp:Label>
                </td>
                <td class="tableTitle">估算价：</td>
                <td class="tableContent">
                    <asp:Label ID="lblMoney" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td class="tableTitle">项目类型：</td>
                <td class="tableContent" colspan="3">
                    <asp:Label ID="lblProjectType" runat="server" Text=""></asp:Label>
                </td>
            </tr>
             <tr style="background: #ffffff;">
                <td class="tableTitle">
                   支部提议：
                </td>
                <td class="tableContent"  colspan="3">
                    因 <asp:Label ID="time8" runat="server" ></asp:Label> 于 <asp:Label ID="time9" runat="server" ></asp:Label>村党支部提出拟对<asp:Label ID="value5" runat="server" ></asp:Label>事项实行招标。
                </td>
            </tr>
             <tr style="background: #ffffff;">
                <td class="tableTitle">
                   两委商议：
                </td>
                <td class="tableContent"  colspan="3">
                     于 <asp:Label ID="time1" runat="server" ></asp:Label>召开村两委联席会商议。
                </td>
                 
            </tr>
             <tr style="background: #ffffff;" >
                <td class="tableTitle">
                    党员代表意见：
                </td>
                <td class="tableContent"  colspan="3">
                     于 <asp:Label ID="time2" runat="server" ></asp:Label>就议案征求党员意见，（<asp:Label ID="value1" runat="server" ></asp:Label>）通过。
                </td>
            </tr>
             <tr style="background: #ffffff;">
                <td class="tableTitle">
                    议案公开：
                </td>
                <td class="tableContent"  colspan="3">
                     于 <asp:Label ID="time3" runat="server" ></asp:Label>至<asp:Label ID="time4" runat="server" ></asp:Label>议案公开（不少于三天）。
                </td>
            </tr>
             <tr style="background: #ffffff;">
                <td class="tableTitle">
                    村民代表会议决议：
                </td>
                <td class="tableContent"  colspan="3">
                     于<asp:Label ID="time5" runat="server" ></asp:Label>召开村民代表会议决议,共 <asp:Label ID="value2" runat="server" ></asp:Label>人参加，<asp:Label ID="value3" runat="server" ></asp:Label>人通过。通过率达<asp:Label ID="value4" runat="server" ></asp:Label>%。
                </td>
            </tr>
            <tr style="background: #ffffff;">
                <td class="tableTitle">
                    结果公开：
                </td>
                <td class="tableContent"  colspan="3">
                     于 <asp:Label ID="time6" runat="server" ></asp:Label>至<asp:Label ID="time7" runat="server" ></asp:Label>结果公开（不少于三天）。
                   
                </td>
            </tr>
            <tr style="background:#ffffff;" style="display:block;" id="barcodeInfoArea">
                <td class="tableTitle">村民代表信息：</td>
                <td class="tableContent" colspan="3">
                    <asp:Label ID="lblShenPiRen" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="background:#ffffff;">
                <td colspan="4">
                    <asp:Button ID="btnClose" runat="server" Text=" 关闭 " onclick="btnClose_Click" />
                </td>
            </tr>
         </table>
    </div>
    </form>
</body>
</html>
