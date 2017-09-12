<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountManage_VoucherPage" Codebehind="VoucherPage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>凭证查询 -- 单张</title>
<base target="_self" />
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function Number.prototype.str(s){var a=""+this;return s.substring(0,s.length-a.length)+a;}
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function ShowAddons(v)
{
    window.showModalDialog("../AccountManage/AppendixShow.aspx?id="+v+"&g="+(new Date()).getTime(),"","dialogWidth=720px;dialogHeight=508px;center=yes;");
}
function ShowEntryData(v,s,r,m)
{
    window.showModalDialog("../AccountManage/AddEntryData.aspx?id="+v+"&no="+s+"&row="+r+"&money="+m+"&g="+(new Date()).getTime(),"","dialogWidth=600px;dialogHeight=452px;center=yes;");
}
function SelSubject()
{
    var returnV=window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=0&g="+(new Date()).getTime(),"","dialogWidth=402px;dialogHeight:387px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        $("QSubjectNo").value=returnV[0]+"["+returnV[1]+"]";
        $("SubjectNo").value=returnV[1];
    }
}
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
<body style="font-size: 10pt">
    <form id="form1" runat="server">
    <div>
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="font-size: 18pt; font-family: 隶书">凭证查询 -- 单张</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr style="text-align: center">
                <td class="t1" style="width: 11%;">
                    查询日期：</td>
                <td class="t1" style="width: 29%">
                    <asp:DropDownList ID="QYear" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="QSMonth" runat="server">
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
                    </asp:DropDownList>&nbsp;^^^^&nbsp;
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
                <td class="t1" style="width: 10%;">
                    凭证编号：</td>
                <td class="t1" style="width: 25%">
                <asp:TextBox ID="VoucherNoS" runat="server" Width="71px"></asp:TextBox>&nbsp;至&nbsp;<asp:TextBox ID="VoucherNoE" runat="server" Width="71px"></asp:TextBox></td>
                <td class="t1" style="width: 10%;">附单张数：</td>
                <td class="t2" style="width: 25%; height: 28px">
                    <asp:TextBox ID="TextBox1" runat="server" BorderWidth="1px" ReadOnly="True" Width="60px"></asp:TextBox>&nbsp;张</td>
            </tr>
            <tr>
                <td class="t4" colspan="6" style="height: 36px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="首张凭证" Height="25px" Width="90px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="PreVoucher" runat="server" OnClick="PreVoucher_Click" Text="上张凭证" Height="25px" Width="90px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="NextVoucher" runat="server" OnClick="NextVoucher_Click" Text="下张凭证" Height="25px" Width="90px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="末张凭证" Height="25px" Width="90px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button5" runat="server" Text="查询凭证" OnClick="Button3_Click" Width="120px" />
                    &nbsp;&nbsp;
                    <input id="Button6" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印凭证" style="width: 90px" />
                    &nbsp;&nbsp;
                    <input id="Button4" type="button" value="查看凭证列表" style="height:25px; width:100px" onclick="location.href='VoucherList.aspx';" /></td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 349px; text-align: center; vertical-align:top">
                    <div style="width: 748px; height: 100%; overflow-y:scroll; overflow-x:hidden">
                    <!--NoPrintEnd-->
                    <asp:PlaceHolder ID="ShowPageContent" runat="server"></asp:PlaceHolder>
                    <!--NoPrintStart-->
                    </div>
                </td>
            </tr>
        </table>
        <!--NoPrintEnd-->
    </div>
    <asp:Label ID="ExecuteScript" runat="server"></asp:Label>
    <asp:HiddenField ID="RecordRowIndex" runat="server" Value="0" />
    <asp:HiddenField ID="VoucherID" runat="server" />
    </form>
</body>
</html>
