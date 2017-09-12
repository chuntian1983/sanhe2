<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_ResClassManage" Codebehind="ResClassManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckMod()
{
    if($("CName").value.length==0)
    {
        $("CName").focus();
        alert("新类别名称不能为空！");
        return false;
    }
    if($("FASubjectNo").value==$("ClassID").value)
    {
        $("ClassID").focus();
        alert("资源类别编号不能为："+$("FASubjectNo").value+"！");
        return false;
    }
    return true;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">资源类别管理</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px;">
            <tr>
                <td class="t1" colspan="1" style="height: 22px; background-color: #f6f6f6; text-align: center; width: 35%;">
                    类别列表</td>
                <td class="t2" colspan="2" style="height: 22px; background-color: #f6f6f6; text-align: center">
                    类别视图</td>
            </tr>
            <tr>
                <td class="t3" rowspan="10" valign="top" style="height:380px">
                <div style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
                    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ImageSet="XPFileExplorer"
                        NodeIndent="15" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>
                            <asp:TreeNode SelectAction="None" Text="资源"></asp:TreeNode>
                        </Nodes>
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                            NodeSpacing="0px" VerticalPadding="2px" />
                    </asp:TreeView>
                 </div>
                </td>
                <td class="t1" style="width: 15%; text-align: right; background-color: #f6f6f6;">
                    当前状态：</td>
                <td class="t2" style="width: 35%; background-color: #f6f6f6;">
                    <asp:Label ID="EditState" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    上级名称：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="ParentName" runat="server" BorderWidth="0px" Width="257px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    类别编号：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="ClassIDP" runat="server" Width="120px" CssClass="d4"></asp:TextBox>
                    <asp:TextBox ID="ClassID" runat="server" Width="130px" CssClass="d4"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    类别名称：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="CName" runat="server" Width="255px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    计量单位：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="MUnit" runat="server" Width="255px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    类别描述：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="CNotes" runat="server" Height="80px" TextMode="MultiLine" Width="255px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="height: 35px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="保存当前类别" Width="100px" OnClick="Button1_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="增加下级类别" Width="100px" OnClick="Button2_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" Text="删除当前类别" Width="100px" OnClick="Button3_Click" /></td>
            </tr>
            <tr>
                <td class="t4" colspan="2" style="height: 100px; text-align: center">&nbsp;</td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="SelectClass" runat="server" Value="0" />
    <asp:HiddenField ID="CEditState" runat="server" Value="0" />
    <asp:HiddenField ID="FASubjectNo" runat="server" Value="151" />
    <asp:HiddenField ID="OldClassID" runat="server" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
