<%@ Page Language="C#" AutoEventWireup="true" Inherits="_ChooseAccount" Codebehind="ChooseAccount.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择查询账套</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<style type="text/css">
.Lightbox{BORDER:#fff 1px solid;DISPLAY:block;Z-INDEX:9999;TEXT-ALIGN:center;POSITION:absolute;BACKGROUND-COLOR:#f6f6f6;COLOR:Blue; FONT-SIZE:18px;height:60px;width:600px}
.Overlay{DISPLAY:block;Z-INDEX:9998;FILTER:alpha(opacity=3);POSITION:absolute;BACKGROUND-COLOR:#000;moz-opacity:0.8;opacity:.80;}
</style>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function ShowConfirm(tip,msg)
{
   if($("AccountID").value.length==0)
   {
       alert('请选择查询账套！');
       return false;
   }
   if(confirm(tip))
   {
       ShowProsess("<br />正在执行"+msg+"，请稍候......！");
       return true;
   }
   else
   {
       return false;
   }
}
function ShowZipInfo(v)
{
   ShowProsess("<br />账套编号："+$("AccountID").value+"，账套名称："+$("AccountName").value+"&nbsp;&nbsp;<a href='../BackupDB/"+v+".zip' target=_blank>下载</a>&nbsp;&nbsp;<a href='###' onclick='CancelProsess()'>取消</a>");
   return false;
}
function CancelProsess()
{
   $("Overlay").style.display="none";
   $("Lightbox").style.display="none";
}
function ShowProsess(msg)
{
   $("Lightbox").innerHTML=msg;
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
function setYear(o,v)
{
    var m=eval($(o).value)+v;
    $(o).value=m;
    return false;
}
function setAccount(v, t) {
   if("<%=Request.QueryString["t"] %>"=="1")
   {
       window.returnValue=v;
       window.close();
       return;
   }
   $('AccountID').value=v;
   $('AccountName').value=t;
}
</script>
</head>
<body id="HBody">
    <form id="form1" runat="server">
    <div style="text-align:center">
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">设置查询账套</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t2" style="height: 370px; vertical-align:top; text-align:left" colspan="6">
                <div style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
                    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ImageSet="XPFileExplorer"
                        NodeIndent="15" ShowLines="True">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>
                            <asp:TreeNode SelectAction="None" Text="一级科目" Value="000"></asp:TreeNode>
                        </Nodes>
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                            NodeSpacing="0px" VerticalPadding="2px" />
                    </asp:TreeView>
                  </div>
                </td>
            </tr>
            <tr>
                <td class="t2" colspan="6" style="height: 10px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="t3" style="height: 28px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Height="25px" Text="设置查询账套" Width="110px" OnClick="Button1_Click" />
                </td>
                <td class="t3" style="height: 28px; text-align: center">当前选择账套：</td>
                <td class="t3" style="height: 28px; text-align: center;">
                    <asp:TextBox ID="AccountID" runat="server" Width="60px" BorderWidth="1px"></asp:TextBox>
                    <asp:TextBox ID="AccountName" runat="server" Width="150px" BorderWidth="1px"></asp:TextBox></td>
                <td class="t3" style="height: 28px; text-align: center">
                    导出年份：</td>
                <td class="t3" style="height: 28px; text-align: center;">
                    <asp:ImageButton ID="SMinus" runat="server" ImageUrl="~/Images/jian.gif" />
                    <asp:TextBox ID="SelYear" runat="server" BorderWidth="0px" Width="27px" Height="18px">2009</asp:TextBox>&nbsp;
                    <asp:ImageButton ID="SPlus" runat="server" ImageUrl="~/Images/jia.gif" /></td>
                <td class="t4" style="height: 28px; text-align: center">
                    <asp:Button ID="Button2" runat="server" Height="25px" Text="导出审计数据" Width="110px" OnClick="Button2_Click" Enabled="False" /></td>
            </tr>
        </table>
        <div id="Overlay" runat="server" class="Overlay"></div>
        <div id="Lightbox" runat="server" class="Lightbox"></div>
        <asp:HiddenField ID="TotalLevel" runat="server" Value="0" />
    </div>
    </form>
</body>
</html>
