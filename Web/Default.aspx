<%@ Page Language="C#" AutoEventWireup="true" Inherits="DefaultSanZi" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>登录</title>
<meta http-equiv="X-UA-Compatible" content="IE=7" />
<link type="text/css" href="Images/css.css" rel="Stylesheet" />
<style type="text/css">
.Lightbox{BORDER:#fff 1px solid;DISPLAY:block;Z-INDEX:9999;TEXT-ALIGN:center;POSITION:absolute;BACKGROUND-COLOR:#f6f6f6;COLOR:Blue; FONT-SIZE:18px;height:60px;width:300px}
.Overlay{DISPLAY:block;Z-INDEX:9998;FILTER:alpha(opacity=3);POSITION:absolute;BACKGROUND-COLOR:#fffccc;moz-opacity:0.8;opacity:.80;}
</style>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckLogin()
{
    if($("UserList").value=="000000")
    {
        $("UserList").focus();
        alert("请选择登录用户！");
        return false;
    }
    if($("Password").value=="")
    {
        $("Password").focus();
        alert("登录口令不能为空！");
        return false;
    }
    ShowProsess("初始化数据");
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
    var aTag=$("DivDefault");
    var leftpos=aTag.offsetLeft;
    var toppos=aTag.offsetTop;
    var objHeight=aTag.offsetHeight;
    var objWidth=aTag.offsetWidth;
    $("Overlay").style.left=leftpos;
    $("Overlay").style.top=toppos;
    $("Overlay").style.height=objHeight;
    $("Overlay").style.width=objWidth;
    $("Lightbox").style.left=leftpos+(objWidth-$("Lightbox").offsetWidth)/2;
    $("Lightbox").style.top=toppos+(objHeight-$("Lightbox").offsetHeight)/2;
}
function SetObjectPos(OParent)
{
    $("DivShowState").value="1";
    $("UnitList").style.display="";
    $("CloseButton").style.display="";
    $('UserList').style.display='none';
    var aTag = document.getElementById(OParent);
    var leftpos=aTag.offsetLeft+1-3;
    var toppos=aTag.offsetTop-13;
    while(aTag = aTag.offsetParent)
    {
        leftpos += aTag.offsetLeft;
        toppos += aTag.offsetTop;
    }
    $("UnitList").style.left=leftpos;
    $("UnitList").style.top=toppos;
    $("CloseButton").style.left=leftpos+$("UnitList").offsetWidth-$("CloseButton").offsetWidth-20;
    $("CloseButton").style.top=toppos+4;
}
function CloseShow()
{
    $('UserList').style.display='';
    $('UnitList').style.display='none';
    $('CloseButton').style.display='none';
    $('DivShowState').value='0';
    $('Password').focus();
}
window.onload = function () {
    if ($("DivShowState").value == "1") {
        SetObjectPos("DoOverLay");
    }
    else {
        CloseShow();
    }
    if ($("DivShowState").value == "0") {
        $('Password').focus();
    }
//    var ulist = $("UserList");
//    for (var k = 0; k < ulist.options.length; k++) {
//        if (ulist.options[k].value == "市级管理员") {
//            ulist.options[k].innerText = ulist.options[k].innerText.replace("领导查询", "市级查询");
//        }
//        else {
//            ulist.options[k].innerText = ulist.options[k].innerText.replace("领导查询", "镇街查询");
//        }
//    }
}
</script>
</head>
<body style="text-align: center;">
    <form id="form1" runat="server">
    <div id="DivDefault" style="margin-top: 40px; width: 932px; height: 500px; background-image:url(Images/szIndex.jpg)">
        <table id="DoOverLay" cellpadding="0" cellspacing="0" style="width: 350px; text-align:left; margin-left: 450px; margin-top: 205px">
            <tr>
                <td style="height: 32px; text-align: center; width: 30%">单位编号：</td>
                <td style="width: 70%">
                    &nbsp;<asp:TextBox ID="AccountID" runat="server" Width="208px" Height="16px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 32px; text-align: center">单位名称：</td>
                <td>
                    &nbsp;<asp:TextBox ID="AccountName" runat="server" Width="208px" Height="16px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 32px; text-align: center">用户名称：</td>
                <td>
                    &nbsp;<asp:DropDownList ID="UserList" runat="server">
                        <asp:ListItem Value="000000">选择登录用户</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="height: 32px; text-align: center">登录口令：</td>
                <td>
                    &nbsp;<asp:TextBox ID="Password" runat="server" Width="208px" Height="16px" TextMode="Password" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 32px; text-align: center">登录类型：</td>
                <td>
                    <asp:RadioButtonList ID="LoginType" runat="server" RepeatDirection="Horizontal" 
                        AutoPostBack="True" OnSelectedIndexChanged="LoginType_SelectedIndexChanged" RepeatColumns="3">
                        <asp:ListItem Value="0" Enabled="False">监督管理</asp:ListItem>
                        <asp:ListItem Value="1" Enabled="False">综合管理</asp:ListItem>
                        <asp:ListItem Value="2" Enabled="False">村级查询</asp:ListItem>
                        <%--<asp:ListItem Value="3" Enabled="False">三资查询</asp:ListItem>--%>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 80px; text-align: center;">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/loginbtn.jpg" OnClick="ImageButton1_Click" />
                    &nbsp;&nbsp;</td>
            </tr>
        </table>
        <div id="UnitList" style="z-index:9991;left:48px;position:absolute;top:87px;height:240px;width:350px;border:1px solid #FF0000;background:#f6f6f6;display:none;overflow-y:scroll;text-align:left">
            <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ImageSet="XPFileExplorer"
                NodeIndent="15" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                <ParentNodeStyle Font-Bold="False" />
                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                <SelectedNodeStyle BackColor="gray" Font-Underline="False" HorizontalPadding="0px"
                    VerticalPadding="0px" BorderStyle="Solid" BorderWidth="1px" BorderColor="blue" />
                <Nodes>
                    <asp:TreeNode SelectAction="None" Text="一级科目" Value="000"></asp:TreeNode>
                </Nodes>
                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                    NodeSpacing="0px" VerticalPadding="2px" />
            </asp:TreeView>
        </div>
        <div id="CloseButton" style="z-index:9992;left:425px;position:absolute;top:351px;height:16px;width:35px;display:none;">
            <a href="javascript:void(0)" onclick="CloseShow();">关闭</a>
        </div>
        <div id="Overlay" runat="server" class="Overlay"></div>
        <div id="Lightbox" runat="server" class="Lightbox"></div>
        <asp:HiddenField ID="IsDoubleClick" runat="server" Value="0" />
        <asp:HiddenField ID="DivShowState" runat="server" Value="0" />
        <asp:HiddenField ID="TotalLevel" runat="server" Value="0" />
        <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
