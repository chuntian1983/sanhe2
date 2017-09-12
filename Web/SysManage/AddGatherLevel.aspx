<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_AddGatherLevel" Codebehind="AddGatherLevel.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
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
            url: "../Images/JqueryDir/DoJson.ashx?t=u&topid="+$("#TopUnitID").val()+"&l="+$("#TotalLevel").val()+"&g="+(new Date()).getTime(),
            cicos: ["/Images/JqueryDir/Images/checkbox_0.gif","/Images/JqueryDir/Images/checkbox_1.gif","/Images/JqueryDir/Images/checkbox_2.gif"],
            persist: "cookie",
            showcheck: true,
            cookieId: "TreeViewChooseSubject" });
    });
</script>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
html{ overflow-x:hidden; }
a{text-decoration:none;}
a:link{color:#009;}
a:visited{color:#800080;}
a:hover,a:active,a:focus{color:#f00;text-decoration:none;}
</style>
<script type="text/javascript">
function $$(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($$("LevelName").value=="")
    {
      $$("LevelName").focus();
      alert("二级汇总单位不能为空！");
      return false;
    }
    GetCheckedNodes();
    return true;
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
function GetCheckedNodes()
{
    var cltNodes="";
    var allNodes=$("#SelectNodes").val();
    var nodes=document.getElementsByName("commCheckBox");
    for(var i=0;i<nodes.length;i++)
    {
        var n=nodes[i];
        if(n.src.indexOf("/Images/JqueryDir/Images/checkbox_1.gif")==-1)
        {
            allNodes=allNodes.replace(n.id,"");
        }
        else
        {
            var p=document.getElementById(n.pid);
            if(p&&p.src.indexOf("/Images/JqueryDir/Images/checkbox_1.gif")!=-1)
            {
                allNodes=allNodes.replace(p.id,"");
                if(cltNodes.indexOf(p.id)==-1)
                {
                    cltNodes+=p.id;
                }
            }
        }
    }
    $("#SelectNodes").val(allNodes);
    $("#CollectParent").val(cltNodes);
}
window.onload = function()
{
    ShowMaskLay();
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">添加二级汇总单位</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    二级汇总名称：</td>
                <td class="t2" colspan="3">
                    <asp:TextBox ID="LevelName" runat="server" Width="628px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    选择汇总单位：</td>
                <td id="treeContainerTD" class="t2" colspan="3" style="vertical-align: top; height: 400px">
                <div id="treeContainer" style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
	                <div style="text-align:left; margin-top:5px; font-size:10pt">
	                <ul class="filetree" style="margin-left:5px">
	                <span id="rootNode" class="folder" runat="server">科目列表</span>
	                <ul id="ajaxTree"></ul>
	                </ul>
	                </div>
                </div>
                </td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 37px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" Width="100px" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" onclick="document.location.href='GatherLevel.aspx';" type="button" value="显示二级汇总单位列表" />
                </td>
            </tr>
        </table>
    </div>
    <div id="Overlay" runat="server" class="Overlay"></div>
    <div id="Lightbox" runat="server" class="Lightbox">正在加载，请等待... ...</div>
    <asp:HiddenField ID="SelectNodes" runat="server" />
    <asp:HiddenField ID="CollectParent" runat="server" />
    <asp:HiddenField ID="TopUnitID" runat="server" />
    <asp:HiddenField ID="TotalLevel" runat="server" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
