<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_DeptManage" Codebehind="DeptManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckMod()
{
    if($("DeptName").value.length==0)
    {
        $("DeptName").focus();
        alert("部门名称不能为空！");
        return false;
    }
    return true;
}
function CheckAdd()
{
    if($("NewDName").value.length==0)
    {
        $("NewDName").focus();
        alert("新增部门名称不能为空！");
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
                    <span style="font-size: 16pt; font-family: 隶书">部门管理</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px;">
            <tr>
                <td class="t1" colspan="1" style="height: 22px; background-color: #f6f6f6; text-align: center; width: 35%;">
                    部门列表&nbsp;</td>
                <td class="t2" colspan="2" style="height: 22px; background-color: #f6f6f6; text-align: center">
                    部门名称编辑</td>
            </tr>
            <tr>
                <td class="t3" rowspan="8" valign="top" style="height:380px">
                <div style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
                    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ImageSet="XPFileExplorer"
                        NodeIndent="15" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>
                            <asp:TreeNode SelectAction="None" Text="单位名称" Value="000000"></asp:TreeNode>
                        </Nodes>
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                            NodeSpacing="0px" VerticalPadding="2px" />
                    </asp:TreeView>
                 </div>
                </td>
                <td class="t1" style="width: 15%; text-align: right">
                    部门编号：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="DeptID" runat="server" Width="257px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    部门名称：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="DeptName" runat="server" Width="255px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="height: 28px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="保存编辑" Width="150px" OnClick="Button1_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" Text="删除部门" Width="150px" OnClick="Button3_Click" /></td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="height: 80px; text-align: center">&nbsp;</td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="text-align: center; background-color: #f6f6f6; height: 26px;">
                    增加新部门</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    部门名称：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="NewDName" runat="server" Width="255px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="height: 37px; text-align: center">
                    <asp:Button ID="Button2" runat="server" Text="增加部门" Width="200px" OnClick="Button2_Click" /></td>
            </tr>
            <tr>
                <td class="t4" colspan="2" style="height: 80px; text-align: center">&nbsp;</td>
            </tr>
        </table>
    </div>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
