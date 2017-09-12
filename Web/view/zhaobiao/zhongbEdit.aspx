<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zhongbEdit.aspx.cs" Inherits="SanZi.Web.zhaobiao.zhongbEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>中标公告录入</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
    <script src="../js/calendar2.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            font-size: x-large; line-height:60px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table  cellspacing="0" cellpadding="0" width="100%"  style="width:640px">
    <tr>
    <td style="width:30px"></td>
    <td style=" height:60px" class="style1">中标公告</td>
    <td style="width:30px"></td>
    </tr>
    <tr>
    <td style="width:30px;height:200px"></td>
    <td align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;按照《<asp:TextBox ID="tbncmc" 
            runat="server" Width="140px"></asp:TextBox>农村集体资产、资源竞价处置和工程建设招投标管理
    <br /><br />
    方法》，规定于 
        <asp:TextBox ID="tbstarttime" runat="server" width="105px"></asp:TextBox>到<asp:TextBox ID="tbfinishtime" runat="server" width="105px"></asp:TextBox>对<asp:DropDownList ID="tbzbmc" runat="server">
        </asp:DropDownList>
  <br /><br />
  进行了公开招标，共有<asp:TextBox ID="tbdwa" runat="server" Width="118px"></asp:TextBox>、<asp:TextBox ID="tbdwb"
      runat="server" Width="118px"></asp:TextBox>、<asp:TextBox ID="tbdwc" 
            runat="server" Width="118px"></asp:TextBox>
            <br /><br />
            等<asp:TextBox ID="tbdws" runat="server" Width="30px"></asp:TextBox>家单位（个人）参加了投标。经招投标，确定<asp:TextBox
                ID="tbzbdw" runat="server" Width="125px"></asp:TextBox>为中标单位，<br /><br />
               请于30日内持单位有效证明前来签署合同。如对本次招标活动的真实性和合法性有异议，<br />
        <br />
        请于5天内向乡镇招投标办公室提出书面意见，举报人应提供真实姓名和电话，否则将视<br />
        <br />
                为无效。</td>
    <td style="width:30px"></td>
    </tr>
    <tr><td style="height:100px" ></td>
    <td valign="bottom" align="right">
        <asp:TextBox ID="tbztbdw" runat="server"></asp:TextBox>竞价招投标办公室&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
    <td></td></tr>
    <tr><td></td><td style="height:80px" align="right">
        <asp:TextBox ID="tbsubtime" 
            runat="server" width="119px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td><td></td></tr>
        <tr><td></td><td>
            <asp:Button ID="Button1" runat="server" Text="确定" onclick="Button1_Click" /></td><td></td></tr>
    
    </table>
    </div>
    </form>
</body>
</html>

