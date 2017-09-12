<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_MDatabase" Codebehind="MDatabase.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
.Lightbox{BORDER:#fff 1px solid;DISPLAY:block;Z-INDEX:9999;TEXT-ALIGN:center;POSITION:absolute;BACKGROUND-COLOR:#f6f6f6;COLOR:Blue; FONT-SIZE:18px;height:60px;width:350px}
.Overlay{DISPLAY:block;Z-INDEX:9998;FILTER:alpha(opacity=3);POSITION:absolute;BACKGROUND-COLOR:#000;moz-opacity:0.8;opacity:.80;}
</style>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function BackupConfirm(tip,msg)
{
   if(confirm(tip))
   {
       ShowProsess(msg)
       return true;
   }
   else
   {
       return false;
   }
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
                    <span style="font-size: 16pt; font-family: 隶书">系统数据管理</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t2" colspan="2" style="height: 28px; text-align: center; background:#f6f6f6">
                    数据完全备份</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 39px; text-align: right">
                    备份日志：</td>
                <td class="t2" style="height: 39px; text-align: center;">
                    <asp:TextBox ID="Notes" runat="server" Height="30px" TextMode="MultiLine" Width="610px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t3" style="width: 15%; text-align: right; height: 28px;">
                    执行操作选项：</td>
                <td class="t4" style="height: 28px; text-align: center">
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="数据完全备份" Width="150px" />
                    &nbsp;注：本备份仅对系统数据库有效</td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
            PageSize="5" Style="color: navy" Width="750px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="备份编号" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                    <ItemStyle Width="160px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="数据备份日志">
                    <EditItemTemplate>
                        <asp:TextBox ID="Notes" runat="server" Height="50px" Text='<%# Eval("notes") %>' TextMode="MultiLine" Width="300px"></asp:TextBox>&nbsp;
                    </EditItemTemplate>
                    <ItemStyle Width="330px" />
                    <ItemTemplate>
                        <asp:Label ID="ShowNotes" runat="server" Text='<%# Bind("notes") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="backupdate" HeaderText="备份时间" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                </asp:BoundField>
                <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="还原" ShowHeader="False">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="RestoreDB"
                            OnClick="LinkButton1_Click" Text="还原" CommandArgument='<%# Bind("id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CommandArgument='<%# Bind("id") %>'>删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
                &nbsp;<asp:LinkButton ID="FirstPage" runat="server" Font-Size="10pt" OnClick="FirstPage_Click">首页</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="PreviousPage" runat="server" Font-Size="10pt" OnClick="PreviousPage_Click">上一页</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="NextPage" runat="server" Font-Size="10pt" OnClick="NextPage_Click">下一页</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="LastPage" runat="server" Font-Size="10pt" OnClick="LastPage_Click">尾页</asp:LinkButton>
                &nbsp;
                <asp:Label ID="ShowPageInfo" runat="server" Font-Size="10pt" Text="总页数："></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Text="跳转到：" ForeColor="Navy" Font-Size="10pt"></asp:Label>
                <asp:DropDownList ID="JumpPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="JumpPage_SelectedIndexChanged">
                </asp:DropDownList>
            </PagerTemplate>
            <RowStyle Font-Size="10pt" />
            <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
            <PagerStyle BackColor="White" ForeColor="Olive" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
        </asp:GridView>
        <div id="Overlay" runat="server" class="Overlay"></div>
        <div id="Lightbox" runat="server" class="Lightbox"></div>
        <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
