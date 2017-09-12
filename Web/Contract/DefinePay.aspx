<%@ Page Language="C#" AutoEventWireup="true" Inherits="Contract_DefinePay" Codebehind="DefinePay.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>自定义付款过程</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("PeriodName").value=="")
    {
      $("PeriodName").focus();
      alert("名称不能为空！");
      return false;
    }
    var patrn1=/^\d{1,15}(\.\d{1,3})?$/;
    if(!patrn1.test($("PayMoney").value))
    {
      $("PayMoney").focus();
      alert("金额格式不正确！");
      return false;
    }
    if($("PayMoney").value=="0")
    {
      $("PayMoney").focus();
      alert("金额不能为零！");
      return false;
    }
    if($("SPay").value.length==0)
    {
        $("SPay").focus();
        alert("请输入开始日期！");
        return false;
    }
    if($("EPay").value.length==0)
    {
        $("EPay").focus();
        alert("请输入结束日期！");
        return false;
    }
    var syear=eval($("SPay").value.substring(0,4));
    var cyear=eval($("EPay").value.substring(0,4));
    if(syear>cyear)
    {
        alert("开始日期不能大于结束日期！");
        return false;
    }
    else
    {
        if(syear==cyear)
        {
            var smonth=eval($("SPay").value.substring(5,7));
            var cmonth=eval($("EPay").value.substring(5,7));
            if(smonth>cmonth)
            {
                alert("开始日期不能大于结束日期！");
                return false;
            }
            else
            {
                if(smonth==cmonth)
                {
                    var sday=eval($("SPay").value.substring(8,10));
                    var cday=eval($("EPay").value.substring(8,10));
                    if(sday>cday)
                    {
                        alert("开始日期不能大于结束日期！");
                        return false;
                    }
                }
            }
        }
    }
    return true;
}
function EditRow(id,PeriodName,PayMoney,SPay,EPay)
{
    $("EditID").value=id;
    $("PeriodName").value=PeriodName;
    $("PayMoney").value=PayMoney;
    $("SPay").value=SPay;
    $("EPay").value=EPay;
    $("hidEPay").value=EPay;
    $("Button1").value="更新过程";
    $("PeriodName").focus();
    return false;
}
function ClearData()
{
    $("EditID").value="";
    $("PeriodName").value="";
    $("PayMoney").value="";
    $("SPay").value="";
    $("EPay").value="";
    $("hidEPay").value="";
    $("Button1").value="添加过程";
    $("PeriodName").focus();
}
function LeasePay(lid,pid)
{
    window.showModalDialog("LeasePay.aspx?lid="+lid+"&pid="+pid+"&g="+(new Date()).getTime(),"","dialogWidth=400px;dialogHeight:271px;center=yes;");
    __doPostBack('HidePostBack','');
    return false;
}
function OnWinClose()
{
    window.returnValue=new Array($("NextPayDate").value,$("NextPayMoney").value);
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
    $("PeriodName").focus();
    resetDialogSize();
}
</script>
</head>
<body onunload="OnWinClose();">
    <form id="form1" runat="server">
        <asp:HiddenField ID="EditID" runat="server" />
        <div>
            <table cellpadding="0" cellspacing="0" style="width: 650px">
                <tr>
                    <td style="height: 25px; text-align: center">
                        <span style="font-size: 16pt; font-family: 隶书">自定义付款过程管理</span>&nbsp;</td>
                </tr>
            </table>
        </div>
        <table id="EditorTable" cellpadding="0" cellspacing="0" style="width: 650px" runat="server">
            <tr>
                <td style="height:15px" colspan="4"></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    开始日期：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="SPay" runat="server" BackColor="#F6F6F6" BorderWidth="1px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    名称：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="PeriodName" runat="server" BorderWidth="1px" Width="192px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right">
                    结束日期：</td>
                <td class="t1">
                    <asp:TextBox ID="EPay" runat="server" BackColor="#F6F6F6" BorderWidth="1px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    金额：</td>
                <td class="t2">
                    <asp:TextBox ID="PayMoney" runat="server" BorderWidth="1px" Width="192px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 60px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加过程" Width="180px" Height="30px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button3" type="button" style="width:180px; height:30px" value="清除数据" onclick="ClearData();" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" style="width:180px; height:30px" value="关闭" onclick="window.close();" />
                    <asp:LinkButton ID="HidePostBack" runat="server" OnClick="HidePostBack_Click"></asp:LinkButton></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" Style="color: navy"
            Width="650px" PageSize="6" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="PeriodName" HeaderText="名称">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="金额" DataField="PayMoney" DataFormatString="{0:f}" HtmlEncode="False">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="StartPay" HeaderText="开始日期">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="EndPay" HeaderText="结束日期">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PayState" HeaderText="状态">
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PayDate" HeaderText="收款日期">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="PayUser" HeaderText="收款人">
                    <ItemStyle Width="60px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="编辑">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server">编辑</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="收款">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnPay" runat="server" CommandArgument='<%# Bind("CardID") %>'>收款</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="btnDelete" CommandArgument='<%# Bind("id") %>'>删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
                &nbsp;<asp:LinkButton ID="FirstPage" runat="server" Font-Size="10pt" OnClick="FirstPage_Click">首页</asp:LinkButton>&nbsp;
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
        <asp:HiddenField ID="hidEPay" runat="server" />
        <asp:HiddenField ID="NextPayDate" runat="server" />
        <asp:HiddenField ID="NextPayMoney" runat="server" />
    </form>
</body>
</html>
