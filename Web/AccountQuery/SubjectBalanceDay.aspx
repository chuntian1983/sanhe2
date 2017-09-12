<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountQuery_SubjectBalanceDay" Codebehind="SubjectBalanceDay.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 10%; text-align: right">
                    查询日期：</td>
                <td class="t1" style="width: 18%; text-align: center;">
                    <asp:TextBox ID="QDate" runat="server" BorderWidth="1px" ForeColor="black" Width="110px"></asp:TextBox></td>
                <td class="t1" style="width: 10%; text-align: right">
                    科目范围：</td>
                <td class="t2" style="width: 30%; text-align: center;">
                    <asp:TextBox ID="SubjectNoS" runat="server" Width="79px"></asp:TextBox>&nbsp; ^^^^^
                    &nbsp;
                    <asp:TextBox ID="SubjectNoE" runat="server" Width="79px"></asp:TextBox>
                </td>
                <td class="t2" style="width: 6%; text-align: right;">条件：</td>
                <td class="t2" style="width: 26%; text-align: center;">
                    <asp:CheckBox ID="isShowDetail" runat="server" Text="显示下级" />
                    <asp:CheckBox ID="isShowZeroSubject" runat="server" Text="显示零余额科目" Checked="True" /></td>
            </tr>
            <tr>
                <td class="t4" colspan="6" style="height: 26px; text-align: center">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="200px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="Button3" type="button" value="打印报表" style="width:200px" onclick="window.open('../PrintWeb.html','','');" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="120px" /></td>
            </tr>
        </table>
        <hr style="width: 750px; color: red; height: 2px; text-align: left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="vertical-align:top; height: 235px;">
                    <asp:PlaceHolder ID="ShowPageContent" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
