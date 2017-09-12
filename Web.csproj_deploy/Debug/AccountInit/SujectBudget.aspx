<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountInit_SujectBudget" Codebehind="SujectBudget.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<script type="text/javascript" id="HideExeScript" src=""></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function SubmitForm()
{
    var patrn=/^\d{1,16}(\.\d{1,4})?$/;
    if(!patrn.test($("BudgetBalance").value))
    {
       $("BudgetBalance").focus();
       alert("金额输入格式有误！");
       return false;
    }
    return true;
}
function FormatFloat(v)
{
    var i=v.lastIndexOf(".");
    if(i==-1){return v+".00";}
    if(i==0){return v+"0";}
    return v;
}
function SelectSubject(n,v,t)
{
    if($("CurSelSubject").value==n&&t==0){return;}
    $("CurSelSubject").value=n;
    $("CurSubjectNo").value=v;
    $("HideExeScript").src="GetBudget.aspx?no="+v+"&year="+$("BudgetYear").value+"&g="+(new Date()).getTime();
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">全年预算设置</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px;">
            <tr>
                <td class="t1" colspan="1" style="height: 22px; background-color: #f6f6f6; text-align: center; width: 35%;">科目选择
                </td>
                <td class="t2" colspan="2" style="height: 22px; background-color: #f6f6f6; text-align: center">
                    科目全年预算数额录入</td>
            </tr>
            <tr>
                <td class="t3" rowspan="7" valign="top" style="height:380px">
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
                <td class="t1" style="width: 15%; text-align: right">
                    预算年份：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="BudgetYear" runat="server" Width="255px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    当前操作科目：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="CurSelSubject" runat="server" Width="255px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    本年预算数额：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="BudgetBalance" runat="server" Width="254px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="text-align: center; background-color: #f6f6f6; height: 28px;">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="height: 32px">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="height: 26px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="保存录入" Width="200px" OnClick="Button1_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" style="width: 184px" type="button" value="返回科目表维护" onclick="location.href='AccountSubject.aspx';" /></td>
            </tr>
            <tr>
                <td class="t4" colspan="2" style="height: 164px; text-align: center">&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="CurSubjectNo" runat="server" />
    <asp:HiddenField ID="BudgetBalanceOld" runat="server" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
    <script type="text/javascript">
        if($("CurSubjectNo").value!=""){SelectSubject($("CurSelSubject").value,$("CurSubjectNo").value,1);}
    </script>
</body>
</html>
