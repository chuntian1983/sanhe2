<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_AccountManage" Codebehind="AccountManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
.Lightbox{BORDER:#fff 1px solid;DISPLAY:block;Z-INDEX:9999;TEXT-ALIGN:center;POSITION:absolute;BACKGROUND-COLOR:#f6f6f6;COLOR:Blue; FONT-SIZE:18px;height:60px;width:300px}
.Overlay{DISPLAY:block;Z-INDEX:9998;FILTER:alpha(opacity=3);POSITION:absolute;BACKGROUND-COLOR:#000;moz-opacity:0.8;opacity:.80;}
</style>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("AccountName").value=="")
    {
      $("AccountName").focus();
      alert("账套名称不能为空！");
      return false;
    }
    if($("AccountName").value.length>12)
    {
      $("AccountName").focus();
      alert("账套名称长度不能超过12个汉字或字符！");
      return false;
    }
    if($("Director").value.length>6)
    {
      $("Director").focus();
      alert("主管会计长度不能超过6个汉字或字符！");
      return false;
    }
    ShowProsess("创建账套");
    return true;
}
function DelUnit(msg)
{
   if(!confirm(msg)){return false;}
   ShowProsess("删除账套");
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
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">账套管理</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t3" style="width: 10%; height: 33px; text-align: right; font-size:10pt">账套名称：</td>
                <td class="t3" style="width: 22%; height: 33px; text-align: center">
                    <asp:TextBox ID="AccountName" runat="server" Width="150px" MaxLength="50"></asp:TextBox></td>
                <td class="t3" style="width: 10%; text-align: right; height: 33px; font-size:10pt">主管会计：</td>
                <td class="t3" style="width: 15%; height: 33px; text-align: center;">
                    <asp:TextBox ID="Director" runat="server" Width="90px" MaxLength="50"></asp:TextBox></td>
                <td class="t3" style="width: 10%; text-align: right; height: 33px; font-size:10pt">账套类型：</td>
                <td class="t3" style="width: 16%; height: 33px; text-align: center;">
                    <asp:RadioButtonList ID="AccountType" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">普通</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="t4" colspan="2" style="width: 15%; height: 33px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="创建账套" Width="95px" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
            Style="color: navy" Width="750px" PageSize="15">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="levelid" HeaderText="编号">
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:BoundField DataField="accountname" HeaderText="账套名称">
                    <ItemStyle Width="150px" />
                    <ControlStyle Width="99%" />
                </asp:BoundField>
                <asp:BoundField DataField="director" HeaderText="主管会计">
                    <ItemStyle Width="65px" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:BoundField DataField="accountdate" HeaderText="财务日期" ReadOnly="True">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="startaccountdate" HeaderText="启用日期" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="排序">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnUp" runat="server" CommandName='<%# Bind("id") %>' OnClick="btnMove_Click">上移</asp:LinkButton>
                        <asp:LinkButton ID="btnDn" runat="server" CommandName='<%# Bind("id") %>' OnClick="btnMove_Click">下移</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("id") %>'
                            CommandName="btnDelete" OnClick="btnDelete_Click">删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle Font-Size="10pt" />
            <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
            <PagerStyle BackColor="White" ForeColor="Olive" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
        </asp:GridView>
        <div id="Overlay" runat="server" class="Overlay"></div>
        <div id="Lightbox" runat="server" class="Lightbox"></div>
        <asp:Label ID="ExeScript" runat="server"></asp:Label></div>
    </form>
</body>
</html>
