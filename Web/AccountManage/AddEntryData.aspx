<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountManage_AddEntryData" Codebehind="AddEntryData.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>分录数据编辑框</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function WinClose()
{
    if($("EntryMoney").value=="")
    {
      $("EntryMoney").focus();
      alert("录入金额格式不正确！最多8位整数和2位小数！");
      return;
    }
    $("EntryMoney").value=parseFloat($("EntryMoney").value);
    var patrn2=/^\d{1,8}(\.\d{1,2})?$/;
    if(!patrn2.test($("EntryMoney").value))
    {
      $("EntryMoney").focus();
      alert("录入金额格式不正确！最多8位整数和2位小数！");
      return;
    }
    window.returnValue=new Array($("BalanceType").value+$("EntryMoney").value,$("VSummary").value,$("AccountType").value);
    window.close();
}
function CancelClose()
{
    window.close();
}
function CheckMoney()
{
    var patrn2=/^\d{1,8}(\.\d{1,2})?$/;
    if(!patrn2.test($("EntryMoney").value)&&$("EntryMoney").value!="")
    {
      $("EntryMoney").focus();
      alert("录入金额格式不正确！最多8位整数和2位小数！");
      return false;
    }
    if(!patrn2.test($("Balance").value))
    {
      $("Balance").focus();
      alert("录入金额格式不正确！最多8位整数和2位小数！");
      return false;
    }
    if($("Balance").value=="0")
    {
      $("Balance").focus();
      alert("金额不能为零！");
      return false;
    }
    var patrn1=/^\d{1,8}(\.\d{1,2})?$/;
    if(!patrn1.test($("Amount").value))
    {
      $("Amount").focus();
      alert("录入数量格式不正确！最多8位整数和2位小数！");
      return false;
    }
    if(!$("CanUse").checked&&$("Amount").value=="0")
    {
      $("Amount").focus();
      alert("数量不能为零！");
      return false;
    }
    return true;
}
function EditRow(id,esummary,balance,amount)
{
    $("EditID").value=id;
    $("ESummary").value=esummary;
    $("Balance").value=parseFloat(balance);
    $("Amount").value=parseFloat(amount);
    $("CanUse").checked=($("Amount").value=="0");
    $("Button1").value="更新原始凭证";
    setTextFocus();
    return false;
}
function ClearData()
{
    if($("AccountType").value=="0"){return;}
    $("EditID").value="";
    $("ESummary").value="";
    $("Balance").value="0";
    $("Amount").value="0";
    $("CanUse").checked=false;
    $("Button1").value="添加原始凭证";
    setTextFocus();
}
function B_OnKeyDown()
{
    if(event.keyCode==13){WinClose();}
    if(event.keyCode==37||event.keyCode==38||event.keyCode==39||event.keyCode==40)
    {
        if($("BalanceType").value=="+")
        {
            $("BalanceType").value="-";
        }
        else
        {
            $("BalanceType").value="+";
        }
        setTextFocus();
    }
}
function setTextFocus()
{
    var focusText=($("AccountType").value=="0")?"EntryMoney":"Balance";
    $(focusText).focus();
    if($(focusText).value!="0")
    {
        $(focusText).select();
    }
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
    if("<%=Request.QueryString["row"] %>".indexOf("Q")!=-1)
    {
        $("EditorTable").style.display="none";
        document.title="原始凭证查看";
    }
    else
    {
        setTextFocus();
    }
    resetDialogSize();
}
</script>
</head>
<body onkeydown="B_OnKeyDown();">
    <form id="form1" runat="server">
        <div style="height:600px; height:400px">
            <table cellpadding="0" cellspacing="0" style="width: 600px">
                <tr>
                    <td class="t4" style="height: 28px; text-align: center">
                        <span style="font-size: 16pt; font-family: 隶书">分录发生额或原始凭证管理</span>&nbsp;</td>
                </tr>
            </table>
            <br />
            <table id="EditorTable" cellpadding="0" cellspacing="0" style="width: 600px">
                <tr>
                    <td class="t1" style="width: 15%; text-align: right">
                        科目代码：</td>
                    <td class="t1" style="width: 35%">
                        <asp:TextBox ID="SubjectNo" runat="server" BorderWidth="0px" ReadOnly="True" Width="170px"></asp:TextBox></td>
                    <td class="t1" style="width: 15%; text-align: right">
                        科目名称：</td>
                    <td class="t2" style="width: 35%">
                        <asp:TextBox ID="SubjectName" runat="server" BorderWidth="0px" ReadOnly="True" Width="170px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="t1" style="width: 15%; text-align: right; height: 29px;">
                        科目发生额：</td>
                    <td class="t1" style="width: 35%; height: 29px;">
                        <asp:DropDownList ID="BalanceType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="BalanceType_SelectedIndexChanged">
                            <asp:ListItem Value="+">借</asp:ListItem>
                            <asp:ListItem Value="-">贷</asp:ListItem>
                        </asp:DropDownList><asp:TextBox ID="EntryMoney" runat="server" MaxLength="16" Width="162px">0</asp:TextBox></td>
                    <td class="t1" style="width: 15%; text-align: right; height: 29px;">
                        &nbsp;</td>
                    <td class="t2" style="height: 29px; text-align: center">
                        <input id="Button4" type="button" value="录入并关闭" onclick="WinClose();" style="width: 100px" />
                        &nbsp;
                        <input id="Button2" type="button" value="取消并关闭" onclick="CancelClose();" style="width: 90px" /></td>
                </tr>
                <tr>
                    <td class="t2" colspan="4" style="height: 22px; background-color: #f6f6f6; text-align: center">
                        原始凭证管理</td>
                </tr>
                <tr>
                    <td class="t1" style="width: 15%; text-align: right">
                        摘要：</td>
                    <td class="t1">
                        <asp:TextBox ID="ESummary" runat="server" Width="200px"></asp:TextBox></td>
                    <td class="t1" style="text-align:right">
                        计量单位：</td>
                    <td class="t2">
                        <asp:TextBox ID="SUnit" runat="server" Width="200px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="t1" style="width: 15%; text-align: right">
                        金额：</td>
                    <td class="t1" style="width: 35%">
                        <asp:TextBox ID="Balance" runat="server" MaxLength="16" Width="200px">0</asp:TextBox></td>
                    <td class="t1" style="width: 15%; text-align: right">
                        数量：</td>
                    <td class="t2" style="width: 35%">
                        <asp:TextBox ID="Amount" runat="server" Width="200px">0</asp:TextBox></td>
                </tr>
                <tr>
                    <td class="t1" style="width: 15%; text-align: right; height: 29px;">
                        其它：</td>
                    <td class="t1" style="width: 35%; height: 29px;">
                        <asp:CheckBox ID="CanUse" runat="server" Text="启用增值、减值" /></td>
                    <td class="t1" style="width: 15%; text-align: right; height: 29px;">
                        &nbsp;</td>
                    <td class="t2" style="height: 29px; text-align: center">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加原始凭证" Width="100px" Enabled="False" />
                        &nbsp;
                        <input id="Button3" type="button" value="清除数据" onclick="ClearData();" style="width: 90px" /></td>
                </tr>
                <tr>
                    <td class="t4" colspan="4" style="background-color: #f6f6f6; height: 22px;">&nbsp;</td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" CaptionAlign="Left" Style="color: navy"
                Width="600px" PageSize="6" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
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
                <Columns>
                    <asp:BoundField DataField="esummary" HeaderText="摘要">
                        <ItemStyle Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="单价" DataFormatString="{0:f}" HtmlEncode="False">
                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="数量" DataField="amount" DataFormatString="{0:f}" HtmlEncode="False">
                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="方向" ReadOnly="True">
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="金额" DataField="balance" DataFormatString="{0:f}" HtmlEncode="False">
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="编辑">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                        <ItemTemplate>
                            <asp:LinkButton ID="edtDelete" runat="server" CommandName="Edit" Text="编辑"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="删除">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="btnDelete">删除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:HiddenField ID="AccountType" runat="server" />
            <asp:HiddenField ID="VSummary" runat="server" />
            <asp:HiddenField ID="EditID" runat="server" />
            <asp:HiddenField ID="hidSUnit" runat="server" />
    </div>
    </form>
</body>
</html>
