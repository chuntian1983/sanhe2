<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountQuery_EachAccPublish" Codebehind="EachAccPublish.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function ShowVoucher(vid)
{
   window.showModalDialog("../AccountManage/LookVoucher.aspx?id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=767px;dialogHeight=385px;center=yes;");
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t3" style="width: 10%; text-align: center; height: 27px;">查询日期：</td>
                <td class="t3" style="width: 20%; height: 27px;">
                    <asp:DropDownList ID="SelYear" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="SelMonth" runat="server">
                    <asp:ListItem Value="01">01月</asp:ListItem>
                    <asp:ListItem Value="02">02月</asp:ListItem>
                    <asp:ListItem Value="03">03月</asp:ListItem>
                    <asp:ListItem Value="04">04月</asp:ListItem>
                    <asp:ListItem Value="05">05月</asp:ListItem>
                    <asp:ListItem Value="06">06月</asp:ListItem>
                    <asp:ListItem Value="07">07月</asp:ListItem>
                    <asp:ListItem Value="08">08月</asp:ListItem>
                    <asp:ListItem Value="09">09月</asp:ListItem>
                    <asp:ListItem Value="10">10月</asp:ListItem>
                    <asp:ListItem Value="11">11月</asp:ListItem>
                    <asp:ListItem Value="12">12月</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t3" style="width: 10%; text-align: center; height: 27px;">查询类型：</td>
                <td class="t3" style="width: 20%; height: 27px;">
                    <asp:DropDownList ID="QType" runat="server">
                        <asp:ListItem Value="0">现金逐笔公开榜</asp:ListItem>
                        <asp:ListItem Value="1">银行存款逐笔公开榜</asp:ListItem>
                        <asp:ListItem Value="2">收支逐笔公开榜</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t4" style="width: 40%; text-align: center; height: 27px;">
                <asp:Button ID="QData" runat="server" Text="查询数据" OnClick="QData_Click" />
                <input id="Button4" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印报表" />
                <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="120px" /></td>
            </tr>
        </table>
        <hr style="width: 750px; color:Red; height:2px; text-align:left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="vertical-align:top; height: 235px;">
                    <asp:PlaceHolder ID="ShowPageContent" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
        </table>
     </div>
    </form>
</body>
</html>
