<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountManage_MonthCarryForward" Codebehind="MonthCarryForward.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
.Lightbox{BORDER:#fff 1px solid;DISPLAY:block;Z-INDEX:9999;TEXT-ALIGN:center;POSITION:absolute;BACKGROUND-COLOR:#f6f6f6;COLOR:Red; FONT-SIZE:18px;}
.Overlay{DISPLAY:block;Z-INDEX:9998;FILTER:alpha(opacity=3);POSITION:absolute;BACKGROUND-COLOR:#000;moz-opacity:0.8;opacity:.80;}
</style>
<script type="text/javascript" id="UpdateCtlTime" src=""></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function AutoSetDate(LastDay)
{
    if(confirm("当前财务日期："+$("AccountDate").value+"，必须在当月最后一天方可结转！\n\n是否自动设置财务日期为月末？"))
    {
        $("AccountDate").value=LastDay;
        $("AccountCarry").disabled="";
    }
}
function ShowWaitBox(msg)
{
    if(confirm(msg))
    {
        $('Lightbox').innerHTML="正在执行操作，请稍候...";
        LimitControl('DoOverLay','Overlay','Lightbox');
        return true;
    }
    else
    {
        return false;
    }
}
function LimitControl(DoOverLay,Overlay,Lightbox)
{
    var aTag=$(DoOverLay);
    var leftpos=aTag.offsetLeft;
    var toppos=aTag.offsetTop;
    var objHeight=aTag.offsetHeight;
    var objWidth=aTag.offsetWidth;
    do{
       aTag = aTag.offsetParent;
       leftpos += aTag.offsetLeft;
       toppos += aTag.offsetTop;
    }while(aTag.tagName!="BODY");
    $(Overlay).style.display="";
    $(Overlay).style.left=leftpos;
    $(Overlay).style.top=toppos;
    $(Overlay).style.height=objHeight;
    $(Overlay).style.width=objWidth;
    $(Lightbox).style.display="";
    $(Lightbox).style.left=leftpos+(objWidth-$(Lightbox).offsetWidth)/2;
    $(Lightbox).style.top=toppos+(objHeight-$(Lightbox).offsetHeight)/2-18;
}
function RefreshD()
{
    $("UpdateCtlTime").src="UpdateCTLTime.aspx?g="+(new Date()).getTime();
    setTimeout("RefreshD()",10000);
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center><br />
        <table id="DoOverLay" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td class="t2" colspan="5" style="height: 25px; text-align: center">
                    月末结转信息统计</td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    财务日期：</td>
                <td class="t1" style="text-align:center" colspan="2">
                    <asp:TextBox ID="AccountDate" runat="server" BorderWidth="1px" CssClass="pcenter" ForeColor="Blue" Width="220px"></asp:TextBox></td>
                <td class="t1" style="text-align: center">
                    空结转至：</td>
                <td class="t2" style="text-align: center">
                    <asp:DropDownList ID="EndCarryDate" runat="server" Width="119px">
                        <asp:ListItem Value="000000">--选择终止年月--</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    当前日期：</td>
                <td class="t2" colspan="4" style="text-align:left">
                    <asp:Label ID="CurrentDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    凭证总数：
                </td>
                <td class="t2" colspan="4" style="text-align:left">
                    <asp:Label ID="VoucherCount" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    凭证编号：</td>
                <td class="t2" colspan="4" style="text-align:left">
                    <asp:Label ID="VoucherNoList" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    未审核凭证：</td>
                <td class="t2" colspan="4" style="text-align:left">
                    <asp:Label ID="Label5" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    未记账凭证：</td>
                <td class="t2" colspan="4" style="text-align:left">
                    <asp:Label ID="Label6" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" rowspan="2" style="width: 20%; text-align: right">
                    模拟汇总：</td>
                <td class="t1" style="text-align: center">
                    期初余额</td>
                <td class="t1" style="text-align: center">
                    借方金额</td>
                <td class="t1" style="text-align: center">
                    贷方金额</td>
                <td class="t2" style="text-align: center">
                    期末余额</td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right">
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="pright" BorderWidth="0px" ReadOnly="True" Width="100px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="pright" BorderWidth="0px" ReadOnly="True" Width="100px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="pright" BorderWidth="0px" ReadOnly="True" Width="100px"></asp:TextBox>
                  </td>
                <td class="t2" style="text-align: right">
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="pright" BorderWidth="0px" ReadOnly="True" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="5" style="text-align: center">*&nbsp; 请核对以上信息是否正确再执行结转操作 *</td>
            </tr>
            <tr>
                <td class="t4" colspan="5" style="height: 43px; text-align: center">
                    <asp:Button ID="AccountCarry" runat="server" OnClick="AccountCarry_Click" Text="确认月末结转" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BackCarry" runat="server" OnClick="BackCarry_Click" Text="反结转至上月" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BackupDate" runat="server" OnClick="BackupDate_Click" Text="数据完全备份" /></td>
            </tr>
        </table>
        <div id="Overlay" runat="server" class="Overlay"></div>
        <div id="Lightbox" runat="server" class="Lightbox"></div>
        </center>
        <asp:HiddenField ID="AccountCarryDate" runat="server" />
        <asp:HiddenField ID="CarrySqlString" runat="server" />
        <asp:Label ID="ExeScript" runat="server"></asp:Label></div>
    </form>
</body>
</html>
