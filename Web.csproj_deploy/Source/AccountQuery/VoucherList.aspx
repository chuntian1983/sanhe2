<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountManage_VoucherList" Codebehind="VoucherList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function Number.prototype.str(s){var a=""+this;return s.substring(0,s.length-a.length)+a;}
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
String.prototype.Trim=function(){return this.replace(/(^\s*)|(\s*$)/g,"");}
String.prototype.LTrim=function(){return this.replace(/(^\s*)/g,"");}
String.prototype.RTrim=function(){return this.replace(/(\s*$)/g,"");}
function SetObjectPos(OParent)
{
    $("CloseButton").style.display="";
    $("SubjectList").style.display="";
    var aTag = document.getElementById(OParent);
    var leftpos=aTag.offsetLeft+1;
    var toppos=aTag.offsetTop+aTag.offsetHeight;
    while(aTag = aTag.offsetParent)
    {
        leftpos += aTag.offsetLeft;
        toppos += aTag.offsetTop;
    }
    $("SubjectList").style.left=leftpos;
    $("SubjectList").style.top=toppos;
    $("CloseButton").style.left=leftpos+$("SubjectList").offsetWidth-$("CloseButton").offsetWidth-20;
    $("CloseButton").style.top=toppos+4;
}
function OnTreeClick(v)
{
    $("QSubjectNo").value=v;
    $("SubjectList").style.display="none";
    $("CloseButton").style.display="none";
}
function OnSelectNode(node)
{
    var text=TreeView_GetNodeText(node).split(".");
    if(text.length>=2)
    {
        $("QSubjectNo").value=text[1].substring(1)+"["+text[0].substring(0,text[0].length-1)+"]";
    }
}
function SelAMonth(v)
{
    var a=eval($("QEMonth").value);
    for(var i=$("QEMonth").options.length-1;i>=0;i--)
    {
        $("QEMonth").options.remove(i);
    }
    for(var i=eval(v);i<=12;i++)
    {
        $("QEMonth").options.add(new Option(i.str("00")+"月",i.str("00")));
        if(a==i){$("QEMonth").selectedIndex=$("QEMonth").options.length-1;}
    }
    OnSelChange(v,1);
}
function OnSelChange(v,t)
{
    if($("QSMonth").value==$("QEMonth").value)
    {
         $("ReportDate").value=$("QYear").value+"年"+$("QSMonth").value+"月";
    }
    else
    {
         $("ReportDate").value=$("QYear").value+"年"+$("QSMonth").value+"-"+$("QEMonth").value+"月";
    }
}
function ShowVoucher(vid)
{
   window.showModalDialog("../AccountManage/LookVoucher.aspx?id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=767px;dialogHeight=385px;center=yes;");
}
function SetSuperQuery()
{
   var returnV=window.showModalDialog("SuperQuery.aspx?g="+(new Date()).getTime(),$("QGroup").value,"dialogWidth=600px;dialogHeight=287px;center=yes;");
   if(typeof(returnV)!="undefined")
   {
       $("QGroup").value=returnV;
   }
}
window.onload=function()
{
    SelAMonth($("QSMonth").value);
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="font-size: 18pt; font-family: 隶书">凭证查询 -- 列表</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 12%; text-align: right">
                    查询日期：</td>
                <td class="t1" style="width: 30%" colspan="2">
                    <asp:DropDownList ID="QYear" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="QSMonth" runat="server">
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
                    </asp:DropDownList>&nbsp; ^^^^&nbsp;
                    <asp:DropDownList ID="QEMonth" runat="server">
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
                <td class="t1" style="width: 12%; text-align: right">
                    选择科目：</td>
                <td class="t1" style="width: 36%">
                    <asp:TextBox ID="QSubjectNo" runat="server" Width="255px"></asp:TextBox></td>
                <td class="t2" style="width: 10%">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="高级选项" /></td>
            </tr>
            <tr>
                <td class="t1" style="width: 12%; height: 34px; text-align: right">
                    附单张数：</td>
                <td class="t1" style="width: 16%; height: 34px">
                    <asp:TextBox ID="TextBox1" runat="server" BorderWidth="1px" ReadOnly="True" Width="60px"></asp:TextBox>&nbsp;张</td>
                <td class="t1" style="width: 14%; text-align: center;">
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="分页显示" Checked="True" /></td>
                <td class="t2" colspan="3" style="height: 34px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="查询凭证" OnClick="Button1_Click" Width="120px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button4" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印列表" style="width: 137px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="OutputDataToExcel" runat="server" Text="导出数据至Excel" Width="120px" OnClick="OutputDataToExcel_Click" /></td>
            </tr>
            <tr>
                <td class="t4" colspan="6" style="height: 30px; text-align: center">注：选定“高级选项”，上面的“查询日期”和“选择科目”将无效！</td>
            </tr>
        </table>
        <br />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt;">
            <tr>
                <td style="width:45%; text-align: left">
                    &nbsp;核算单位：<asp:Label ID="AName" runat="server"></asp:Label></td>
                <td style="width:25%; text-align: left">
                    <asp:TextBox ID="ReportDate" runat="server" BorderWidth="0px" Width="147px" ReadOnly="True"></asp:TextBox></td>
                <td style="width:30%; text-align: right">打印日期：<%=DateTime.Now.ToString("yyyy年MM月dd日") %>&nbsp;&nbsp;</td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" PageSize="12" Style="color: navy"
            Width="750px" OnRowDataBound="GridView1_RowDataBound">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="voucherno" HeaderText="凭证编号">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="voucherdate" HeaderText="凭证日期">
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="vsummary" HeaderText="摘要">
                    <ItemStyle Width="240px" />
                </asp:BoundField>
                <asp:BoundField DataField="subjectno" HeaderText="总账科目">
                    <ItemStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="subjectname" HeaderText="明细科目">
                    <ItemStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="方向">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="summoney" DataFormatString="{0:f}" HeaderText="金额" HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="voucherid" HeaderText="凭证表ID" Visible="False" />
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
                <asp:Label ID="Label1" runat="server" Font-Size="10pt" ForeColor="Navy" Text="跳转到："></asp:Label>
                <asp:DropDownList ID="JumpPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="JumpPage_SelectedIndexChanged">
                </asp:DropDownList>
            </PagerTemplate>
            <RowStyle Font-Size="10pt" />
            <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
            <PagerStyle BackColor="White" ForeColor="Olive" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
        </asp:GridView>
        <!--#include file="ReportBottom.aspx"-->
        <div id="SubjectList" style="z-index: 1; left: 237px; position: absolute; top: 222px; height: 200px; width: 258px; border: 1px solid #FF0000; background:#ffffff; display:none; overflow-y:scroll">
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
        <div id="CloseButton" style="z-index: 1; left: 392px; position: absolute; top: 351px; height: 16px; width: 70px; display:none;">
         <a href="javascript:void(0)" onclick="$('QSubjectNo').value=''">清除</a>&nbsp;&nbsp;&nbsp;
         <a href="javascript:void(0)" onclick="$('SubjectList').style.display='none';$('CloseButton').style.display='none'">关闭</a>
        </div>
        <asp:HiddenField ID="QGroup" runat="server" />
        <asp:Label ID="ExeScript" runat="server"></asp:Label></div>
    </form>
</body>
</html>
<script type="text/javascript" src="../Images/TreeViewEvent.js"></script>
