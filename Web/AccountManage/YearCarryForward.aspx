<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountManage_YearCarryForward" Codebehind="YearCarryForward.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
.Lightbox{BORDER:#fff 1px solid;DISPLAY:block;Z-INDEX:9999;TEXT-ALIGN:center;POSITION:absolute;BACKGROUND-COLOR:#f6f6f6;COLOR:Blue; FONT-SIZE:18px;height:60px;width:500px}
.Overlay{DISPLAY:block;Z-INDEX:9998;FILTER:alpha(opacity=3);POSITION:absolute;BACKGROUND-COLOR:#000;moz-opacity:0.8;opacity:.80;}
</style>
<script type="text/javascript" id="UpdateCtlTime" src=""></script>
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
   LimitControl();
}
function LimitControl()
{
    var aTag=$("HBody");
    var leftpos=aTag.offsetLeft;
    var toppos=aTag.offsetTop;
    var objHeight=aTag.offsetHeight;
    var objWidth=aTag.offsetWidth;
    $("Overlay").style.display="";
    $("Overlay").style.left=leftpos;
    $("Overlay").style.top=toppos;
    $("Overlay").style.height=objHeight;
    $("Overlay").style.width="750px";
    $("Lightbox").style.display="";
    $("Lightbox").style.left=leftpos+(objWidth-$("Lightbox").offsetWidth)/2;
    $("Lightbox").style.top=toppos+(objHeight-$("Lightbox").offsetHeight)/2;
}
function RefreshD()
{
    $("UpdateCtlTime").src="UpdateCTLTime.aspx?g="+(new Date()).getTime();
    setTimeout("RefreshD()",10000);
}
</script>
</head>
<body id="HBody">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="CarryForward0" runat="server" Height="60px" Text="年末收支自动结转" Width="200px" OnClick="CarryForward0_Click" />
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
        <asp:Button ID="CarryForward1" runat="server" Height="60px" Text="年末结转" Width="200px" OnClick="CarryForward1_Click" />
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
        <asp:Button ID="BackCarryForward" runat="server" Height="60px" Text="反年末结转" Width="200px" OnClick="BackCarryForward_Click" /><br />
    </div>
    <div id="Overlay" runat="server" class="Overlay"></div>
    <div id="Lightbox" runat="server" class="Lightbox"></div>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
