<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountInit_MDatabase" Codebehind="MDatabase.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
.Lightbox{BORDER:#fff 1px solid;DISPLAY:block;Z-INDEX:9999;TEXT-ALIGN:center;POSITION:absolute;BACKGROUND-COLOR:#f6f6f6;COLOR:Blue; FONT-SIZE:18px;height:60px;width:600px}
.Overlay{DISPLAY:block;Z-INDEX:9998;FILTER:alpha(opacity=3);POSITION:absolute;BACKGROUND-COLOR:#000;moz-opacity:0.8;opacity:.80;}
</style>
<script type="text/javascript" id="UpdateCtlTime" src=""></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function ShowConfirm(tip,msg)
{
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
   ShowProsess("<br />备份编号："+v+"&nbsp;&nbsp;<a href='../BackupDB/"+v+".zip' target=_blank>下载</a>&nbsp;&nbsp;<a href='###' onclick='CancelProsess()'>取消</a>");
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
function CheckUpload()
{
    var pattern=/^.*\.zip$/i;
    var file=$("FileUpload1").value;
    if(file.length==0||!pattern.test(file))
    {
        alert("请选择正确的数据库文件，必须后缀名为.zip！");
        return false;
    }
    return true;
}
function RefreshD()
{
    $("UpdateCtlTime").src="../AccountManage/UpdateCTLTime.aspx?g="+(new Date()).getTime();
    setTimeout("RefreshD()",10000);
}
</script>
</head>
<body id="HBody">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">数据备份恢复</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr style="background:#f8f8f8">
                <td class="t1" colspan="2" style="text-align: center">
                数据完全备份</td>
                <td class="t2" colspan="2" style="text-align: center">
                选择性备份</td>
            </tr>
            <tr>
                <td class="t1" style="width: 12%; text-align: right; height: 70px">
                备份日志：</td>
                <td class="t1" style="width: 30%; vertical-align: top">
                <asp:TextBox ID="Notes" runat="server" Height="67px" TextMode="MultiLine" Width="260px" BorderWidth="1px" BorderColor="gray"></asp:TextBox>
                </td>
                <td class="t1" style="width: 12%; text-align: right">
                数据库表：</td>
                <td class="t2" style="width: 46%; vertical-align: top">
                <asp:CheckBoxList ID="TableList" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" Width="328px">
                </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td class="t1" style="width: 12%;">&nbsp;</td>
                <td class="t1" style="text-align: left; height: 33px;">
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="数据完全备份" Width="263px" Height="25px" /></td>
                <td class="t1" style="width: 12%;">&nbsp;</td>
                <td class="t2" style="text-align: left; height: 33px;">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="备份选择数据表" Width="329px" Height="25px" /></td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t3" style="height: 33px; text-align: center; width:10%">
                上传备份：</td>
                <td class="t4" colspan="3" style="height: 33px; width:88%">
                <asp:FileUpload ID="FileUpload1" runat="server" Width="565px" unselectable="on" />&nbsp;&nbsp;
                <asp:Button ID="Button3" runat="server" Text="上传" Width="97px" OnClick="Button3_Click" /></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
            PageSize="10" Style="color: navy" Width="750px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="备份编号" ReadOnly="True">
                    <ItemStyle Width="160px" HorizontalAlign="Center" />
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
                <asp:BoundField DataField="backupdate" HeaderText="备份时间" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="还原" ShowHeader="False">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="RestoreDB"
                            OnClick="LinkButton1_Click" Text="还原" CommandArgument='<%# Bind("id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CommandArgument='<%# Bind("id") %>'>删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="下载">
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CommandArgument='<%# Bind("id") %>'>下载</asp:LinkButton>
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
