<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountCollect_Statistics" Codebehind="Statistics.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
.Lightbox{BORDER:#fff 1px solid;DISPLAY:block;Z-INDEX:9999;TEXT-ALIGN:center;POSITION:absolute;BACKGROUND-COLOR:#f6f6f6;COLOR:Blue; FONT-SIZE:18px;height:60px;width:300px}
.Overlay{DISPLAY:block;Z-INDEX:9998;FILTER:alpha(opacity=3);POSITION:absolute;BACKGROUND-COLOR:#fffccc;moz-opacity:0.8;opacity:.80;}
</style>
<base target="_self" />
<script type="text/javascript" id="HideExeScript" src=""></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function SubmitForm()
{
    if($("StatisticUnit").value=="")
    {
      alert("请选择统计单位！");
      return false;
    }
    if($("StatisticExpr").value=="")
    {
      alert("请定义统计公式！");
      return false;
    }
    ShowProsess("数据统计");
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
    var aTag=$("DoOverLay");
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
function SelectExpr(oid)
{
    var returnV=window.showModalDialog("../AccountInit/DefineExpr.aspx?f=0&g="+(new Date()).getTime(),$(oid).value,"dialogWidth=402px;dialogHeight:359px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        $(oid).value=returnV;
    }
}
function setYear(o,v)
{
    var m=eval($(o).value)+v;
    $(o).value=m;
    return false;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">单项统计</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <table id="DoOverLay" cellpadding="0" cellspacing="0" style="width: 750px;">
            <tr>
                <td class="t1" colspan="1" style="height: 22px; background-color: #f6f6f6; text-align: center; width: 35%;">
                    单位选择</td>
                <td class="t2" colspan="2" style="height: 22px; background-color: #f6f6f6; text-align: center">
                    统计结果</td>
            </tr>
            <tr>
                <td class="t3" rowspan="7" valign="top" style="width: 35%; height:410px">
                <div style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
                    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ImageSet="XPFileExplorer" NodeIndent="15" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <SelectedNodeStyle BackColor="#F6F6F6" Font-Underline="False" HorizontalPadding="0px"
                            VerticalPadding="0px" BorderStyle="Solid" BorderWidth="1px" />
                        <Nodes>
                            <asp:TreeNode SelectAction="None" Text="一级科目" Value="000"></asp:TreeNode>
                        </Nodes>
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                    </asp:TreeView>
                 </div>
                </td>
                <td class="t1" style="text-align: right; height: 35px">
                    统计单位：</td>
                <td class="t2">
                    &nbsp;<asp:TextBox ID="StatisticUnit" runat="server" BorderWidth="1px" ReadOnly="True" Width="221px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right; height: 35px">
                    统计年月：</td>
                <td class="t2">
                    &nbsp;<asp:ImageButton ID="SMinus" runat="server" ImageUrl="~/Images/jian.gif" />
                    <asp:TextBox ID="SelYear" runat="server" BorderWidth="0px" Width="27px" Height="18px">2009</asp:TextBox>&nbsp;
                    <asp:ImageButton ID="SPlus" runat="server" ImageUrl="~/Images/jia.gif" />
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
            </tr>
            <tr>
                <td class="t1" style="text-align: right">
                    统计项目：</td>
                <td class="t2">
                    &nbsp;<asp:TextBox ID="StatisticExpr" runat="server" Width="321px" BorderWidth="1px" Height="116px" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr style="background-color:#f6f6f6; color:Red">
                <td class="t1" style="text-align: right; height: 35px">
                    统计结果：</td>
                <td class="t2">
                    &nbsp;<asp:Label ID="StatisticValue" runat="server" Text="0.00"></asp:Label></td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="text-align: center; height:35px">
                    <asp:Button ID="Button1" runat="server" Text="开始统计" Width="200px" Height="30px" OnClick="Button1_Click" /></td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="text-align: center; background-color: #f6f6f6; height: 28px;">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t4" colspan="2" style="height: 100px; text-align: center">&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="Overlay" runat="server" class="Overlay"></div>
    <div id="Lightbox" runat="server" class="Lightbox"></div>
    <asp:HiddenField ID="GAccountList" runat="server" />
    <asp:HiddenField ID="TotalLevel" runat="server" Value="0" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
