<%@ Page Language="C#" AutoEventWireup="true" Inherits="FixedAsset_CarryMonthDepr" Codebehind="CarryMonthDepr.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<style type="text/css">
.Lightbox{BORDER:#fff 1px solid;DISPLAY:block;Z-INDEX:9999;TEXT-ALIGN:center;POSITION:absolute;BACKGROUND-COLOR:#f6f6f6;COLOR:Blue; FONT-SIZE:18px;height:60px;width:500px}
.Overlay{DISPLAY:block;Z-INDEX:9998;FILTER:alpha(opacity=3);POSITION:absolute;BACKGROUND-COLOR:#000;moz-opacity:0.8;opacity:.80;}
</style>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function _Confirm(msg,tip)
{
   if(!confirm(msg)){return false;}
   ShowProsess(tip);
   return true;
}
function ShowProsess(v)
{
   $("Lightbox").innerHTML="<br />正在执行"+v+"，请稍候......！";
   $("Overlay").style.display="";
   $("Lightbox").style.display="";
   LimitControl();
}
function LimitControl()
{
    var aTag=$("HBody");
    var leftpos=aTag.offsetLeft;
    var toppos=aTag.offsetTop;
    var objHeight=aTag.offsetHeight;
    var objWidth=aTag.offsetWidth;
    $("Overlay").style.left=leftpos;
    $("Overlay").style.top=toppos;
    $("Overlay").style.height=objHeight;
    $("Overlay").style.width="750px";
    $("Lightbox").style.left=leftpos+(objWidth-$("Lightbox").offsetWidth)/2;
    $("Lightbox").style.top=toppos+(objHeight-$("Lightbox").offsetHeight)/2;
}
</script>
</head>
<body id="HBody">
    <form id="form1" runat="server">
    <div>
    <center><br /><br /><br /><br /><br />
        <asp:Button ID="Button1" runat="server" Height="50px" Text="计提本月折旧" Width="200px" OnClick="Button1_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Height="50px" Text="批量制单" Width="200px" OnClick="Button2_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Height="50px" Text="批量反制单" Width="200px" OnClick="Button3_Click" />
        <br /><br />
        <div style="text-align:left; width:660px;">
            注：<br /><br />
            1、必须计提折旧后方可批量制单。<br /><br />
            2、必须批量制单后方可查询明细账。<br /><br />
            3、只要资产卡片已制单，当月该资产卡片不可再变更。<br /><br />
            4、计提折旧后制单前， 如果修改资产卡片，则卡片状态自动更改为计提前。<br /><br />
            5、重要提示：总账反月末结转后固定资产模块部分功能将被锁定。
        </div>
    </center>
    </div>
    <div id="Overlay" runat="server" class="Overlay"></div>
    <div id="Lightbox" runat="server" class="Lightbox"></div>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
