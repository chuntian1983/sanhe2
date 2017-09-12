<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zbgg.aspx.cs" Inherits="SanZi.Web.zhaobiao.zbgg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>招标公告录入</title>
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
    <div align="center">
    <table cellspacing="0" cellpadding="0" width="100%" style="width:600px;border:1px solid #000800;">
    <tr>
    <td width="30px"></td>
    <td height="60px" class="style1">招标公告录入</td>
    <td width="30px"></td>
    </tr>
    <tr>
    <td></td>
    <td align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;经民主议事六步工作法，<asp:TextBox 
            ID="tbcwh" runat="server" Width="150px"></asp:TextBox>村委会决定通过公开投标
    <br /><br />
    方式对<asp:DropDownList ID="tbzbgc" runat="server">
        </asp:DropDownList>进行招
    <br /><br />
    标，受其委托，乡镇竞价招投标办公室将有关事项公告如下：
    <br /></td>
    <td></td></tr>
    <tr>
    <td></td>
    <td align="left" height="40px">一、招标内容及项目情况：</td>
    <td></td>
    </tr>
    
        <tr>
    <td></td>
    <td align="left">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="tbnrqk" runat="server" 
            Height="80px" TextMode="MultiLine" Width="430px"></asp:TextBox></td>
    <td></td>
    </tr>
    
    <tr>
    <td></td>
    <td align="left" height="40px">二、报名条件：</td>
    <td></td>
    </tr>
    
        <tr>
    <td></td>
    <td align="left">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="tbbmtj" runat="server" 
            Height="80px" TextMode="MultiLine" Width="430px"></asp:TextBox></td>
    <td></td>
    </tr>
    <tr>
    <td></td>
    <td align="left" height="40px">三、招标程序：</td>
    <td></td>
    </tr>
        <tr>
    <td></td>
    <td align="left">
    <table  cellspacing="3" cellpadding="3" width="100%" style=" width:540px">
    <tr>
    <td>1、本公告发布之日起7日后，由招投标办公室根据报名情况确定3家以上投标单位。</td>
   
    </tr>
    <tr><td>2、招投标办公室确定招标时间、地点。</td></tr>
    <tr><td>3、召开招标会，并根据<asp:TextBox ID="tbyz" runat="server" Width="130px"></asp:TextBox>原则，产生中标人（单位）。</td></tr>
    <tr><td>4、与中标人（单位）签订合同。</td></tr>
    </table></td>
    <td></td>
    </tr>
    <tr>
    <td></td>
    <td align="left" height="40px">四、报名事项：</td>
    <td></td>
    </tr>
    <tr>
    <td></td>
    <td align="left">
    <table  cellspacing="3" cellpadding="3" width="100%" style=" width:540px">
    <tr>
    <td>1、时间：<asp:TextBox ID="tbstarttime" runat="server" width="110px"></asp:TextBox>
        —<asp:TextBox ID="tbfinishtime" runat="server" width="110px"></asp:TextBox>
        </td>
   
    </tr>
    <tr><td>2、地点：<asp:TextBox ID="tbbmdd" runat="server" Width="380px"></asp:TextBox>
        </td></tr>
    <tr><td>3、联系人及电话：<asp:TextBox ID="tblxfs" runat="server" Width="315px"></asp:TextBox>
        </td></tr>
    <tr><td>4、报名时持本人（法人）有效证明原件及复印件。</td></tr>
    </table>
    </td>
    <td></td>
    </tr>
    <tr><td height="50px"></td><td  valign="bottom" align="right"><asp:TextBox ID="TextBox1" runat="server" Width="380px"></asp:TextBox>竞价招投标办公室</td><td></td></tr>
    <tr><td height="50px"></td><td  align="right"><asp:TextBox ID="tbsubtime" runat="server" width="110px"></asp:TextBox>
        </td><td></td></tr>
    <tr><td></td><td height="40px">
        <asp:Button ID="btn_sure" runat="server" Text="确定" onclick="btn_sure_Click" /></td><td></td></tr>
    </table>
    </div>
    </form>
</body>
</html>
