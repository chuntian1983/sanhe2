<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountQuery_PrintGeneral" Codebehind="PrintGeneral.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function Number.prototype.str(s){var a=""+this;return s.substring(0,s.length-a.length)+a;}
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function SelAMonth(v)
{
    var a=eval($("QEMonth").value);
    for(var i=$("QEMonth").options.length-1;i>=0;i--)
    {
        $("QEMonth").options.remove(i);
    }
    for(var i=eval(v);i<=12;i++)
    {
        $("QEMonth").options.add(new Option(i.str("00")+"月",i.str("00")));
        if(a==i){$("QEMonth").selectedIndex=$("QEMonth").options.length-1;}
    }
}
</script>
</head>
<body style="font-size: 10pt" onpaste="return false">
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="font-size: 18pt; font-family: 隶书">总账连续打印输出</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    查询日期：</td>
                <td class="t1" style="width: 35%">
                    <asp:DropDownList ID="QYear" runat="server">
                    </asp:DropDownList><asp:DropDownList ID="QSMonth" runat="server">
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
                    </asp:DropDownList>&nbsp; ^^^^&nbsp;
                    <asp:DropDownList ID="QEMonth" runat="server">
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
                <td class="t1" style="width: 15%; text-align: right">
                    科目范围：</td>
                <td class="t2" style="width: 35%" colspan="2">
                    <asp:TextBox ID="SubjectNoS" runat="server" Width="103px"></asp:TextBox>&nbsp; ^^^^&nbsp;
                    <asp:TextBox ID="SubjectNoE" runat="server" Width="102px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    分页类型：</td>
                <td class="t1" style="width: 35%">
                    <asp:RadioButtonList ID="PageType" runat="server" RepeatDirection="Horizontal" Width="205px">
                        <asp:ListItem Value="0">自动分页</asp:ListItem>
                        <asp:ListItem Value="1" Selected="True">按科目分页</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="t1" style="width: 15%; text-align: right">
                    输出选项：</td>
                <td class="t1" style="width: 26%;">
                    <asp:CheckBox ID="isShowFlag" runat="server" Text="显示余额或发生额为零的科目" /></td>
                <td class="t2" style="width: 9%; text-align: center">
                    <asp:DropDownList ID="ReportType" runat="server">
                        <asp:ListItem Value="0">纵向</asp:ListItem>
                        <asp:ListItem Value="1">横向</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t4" colspan="5" style="height: 31px; text-align: center">
                 <asp:Button ID="QButton" runat="server" Text="查询" OnClick="QButton_Click" Width="180px" />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <input id="Button1" onclick="location.href='GeneralLedger.aspx';" style="width:180px" type="button" value="普通打印" />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="Button4" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印报表" style="width: 120px" /></td>
            </tr>
        </table>
        <hr style="width: 750px; color:Red; height:2px; text-align:left" />
        <!--NoPrintEnd-->
        <asp:HiddenField ID="GridViewWidth" runat="server" Value="750" />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="vertical-align:top; height: 235px;">
                    <asp:PlaceHolder ID="ShowPageContent" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">SelAMonth($("QSMonth").value);</script>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
