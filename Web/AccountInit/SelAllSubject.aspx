<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountInit_SelAllSubject" Codebehind="SelAllSubject.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择科目</title>
<style type="text/css">
.Lightbox{BORDER:#fff 1px solid;DISPLAY:block;Z-INDEX:9999;TEXT-ALIGN:center;POSITION:absolute;BACKGROUND-COLOR:#f8f8f8;COLOR:Blue; FONT-SIZE:18px;height:25px;width:200px}
.Overlay{DISPLAY:block;Z-INDEX:9998;FILTER:alpha(opacity=3);POSITION:absolute;BACKGROUND-COLOR:#000;moz-opacity:0.8;opacity:.80;}
</style>
<link rel="stylesheet" href="../Images/JqueryDir/screen.css" />
<link rel="stylesheet" href="../Images/JqueryDir/jquery.treeview.css" />
<script type="text/javascript" src="../Images/JqueryDir/jquery.js"></script>
<script type="text/javascript" src="../Images/JqueryDir/jquery.cookie.js"></script>
<script type="text/javascript" src="../Images/JqueryDir/jquery.treeview.js"></script>
<script type="text/javascript" src="../Images/JqueryDir/jquery.treeview.async.js"></script>
<script type="text/javascript" language="javascript">
    $(document).ready(function() {
        $("#ajaxTree").treeview({
            url: "../Images/JqueryDir/DoJson.ashx?t=<%=Request.QueryString["t"] %>"
                +"&filter=<%=Request.QueryString["filter"] %>"
                +"&hidelock=<%=Request.QueryString["hidelock"] %>"
                +"&g="+(new Date()).getTime(),
            persist: "cookie",
            cookieId: "TreeViewChooseSubject" });
    });
</script>
<style type="text/css">html{overflow-x:hidden;}</style>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
html{ overflow-x:hidden; }
a{text-decoration:none;}
a:link{color:#009;}
a:visited{color:#800080;}
a:hover,a:active,a:focus{color:#f00;text-decoration:none;}
</style>
<base target="_self" />
<script type="text/javascript">
function $$(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function OnTreeClick(n,v)
{
    window.returnValue=new Array(n,v);
    window.close();
}
function ShowMaskLay()
{
    var aTag=$$("treeContainer");
    var oLay=$$("Overlay");
    var lLay=$$("Lightbox");
    var leftpos=aTag.offsetLeft;
    var toppos=aTag.offsetTop;
    var objHeight=aTag.offsetHeight;
    var objWidth=aTag.offsetWidth;
    do{
       aTag = aTag.offsetParent;
       leftpos += aTag.offsetLeft;
       toppos += aTag.offsetTop;
    }while(aTag.tagName!="BODY");
    oLay.style.display="";
    oLay.style.left=leftpos;
    oLay.style.top=toppos;
    oLay.style.height=objHeight;
    oLay.style.width=objWidth;
    lLay.style.display="";
    lLay.style.left=leftpos+(objWidth-lLay.offsetWidth)/2;
    lLay.style.top=toppos+(objHeight-lLay.offsetHeight)/2-18;
}
function CloseMaskLay()
{
    $$("Overlay").style.display="none";
    $$("Lightbox").style.display="none";
}
function resetDialogSize()
{
    var ua = navigator.userAgent;
    if(ua.lastIndexOf("MSIE 7.0") == -1)
    {
        var height = document.body.offsetHeight;
        var width = document.body.offsetWidth;
        if(ua.lastIndexOf("Windows NT 5.2") == -1)
        {
            window.dialogHeight=(height+53)+"px";
            window.dialogWidth=(width+6)+"px";
        }
        else
        {
            window.dialogHeight=(height+46)+"px";
            window.dialogWidth=(width+6)+"px";
        }
    }
}
window.onload = function()
{
    ShowMaskLay();
    resetDialogSize();
}
</script>
</head>
<body class="margin0">
    <form id="form1" runat="server">
    <div>
        <div id="treeContainer" style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
            <div style="text-align:left; margin-top:5px; font-size:10pt">
            <ul class="filetree" style="margin-left:5px">
            <span class="folder">科目列表</span>
            <ul id="ajaxTree"></ul>
            </ul>
            </div>
        </div>
        <div id="Overlay" runat="server" class="Overlay"></div>
        <div id="Lightbox" runat="server" class="Lightbox">正在加载，请等待... ...</div>
    </div>
    </form>
</body>
</html>
