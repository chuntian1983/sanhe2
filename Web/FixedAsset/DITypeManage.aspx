<%@ Page Language="C#" AutoEventWireup="true" Inherits="FixedAsset_DITypeManage" Codebehind="DITypeManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckMod()
{
    if($("TName").value.length==0)
    {
        $("TName").focus();
        alert("增加方式名称不能为空！");
        return false;
    }
    if($("TName").value.length>18)
    {
        $("TName").focus();
        alert("增加方式名称长度不能超过18个汉字或字符！");
        return false;
    }
    if($("LinkSubject").value.length==0)
    {
        alert("对应入账科目不能为空！");
        return false;
    }
    return true;
}
function CheckAdd()
{
    if($("NewDName").value.length==0)
    {
        $("NewDName").focus();
        alert("增加方式名称不能为空！");
        return false;
    }
    if($("NLinkSubject").value.length==0)
    {
        alert("对应入账科目不能为空！");
        return false;
    }
    return true;
}
//卡片项目选择
function SelectItem(t)
{
    var returnV=window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=1&g="+(new Date()).getTime(),"","dialogWidth=360px;dialogHeight:400px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        switch(t)
        {
            case 1:
                $("LinkSubject").value=returnV[1]+"."+returnV[0];
                break;
            case 2:
                $("NLinkSubject").value=returnV[1]+"."+returnV[0];
                break;
            case 3:
                $("ValueAddSubject").value = returnV[1] + "." + returnV[0];
                break;
            case 4:
                $("NValueAddSubject").value = returnV[1] + "." + returnV[0];
                break;
        }
    }
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">增减方式管理</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px;">
            <tr>
                <td class="t1" colspan="1" style="height: 22px; background-color: #f6f6f6; text-align: center; width: 35%;">
                    增减方式列表&nbsp;</td>
                <td class="t2" colspan="2" style="height: 22px; background-color: #f6f6f6; text-align: center">
                    增减方式名称编辑</td>
            </tr>
            <tr>
                <td class="t3" rowspan="13" valign="top" style="height:380px">
                <div style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
                    <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer"
                        NodeIndent="15" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>
                            <asp:TreeNode SelectAction="None" Text="增减方式目录表" Value="000000">
                                <asp:TreeNode SelectAction="Expand" Text="增加方式" Value="1"></asp:TreeNode>
                                <asp:TreeNode SelectAction="Expand" Text="减少方式" Value="2"></asp:TreeNode>
                            </asp:TreeNode>
                        </Nodes>
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                            NodeSpacing="0px" VerticalPadding="2px" />
                    </asp:TreeView>
                 </div>
                </td>
                <td class="t1" style="width: 15%; text-align: right">
                    增减方式编号：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="DID" runat="server" Width="257px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    增减方式名称：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="TName" runat="server" Width="255px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    对应入账科目：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="LinkSubject" runat="server" Width="255px" BackColor="#E0E0E0" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    增值入账科目：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="ValueAddSubject" runat="server" Width="255px" BackColor="#E0E0E0" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="height: 28px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="保存编辑" Width="150px" OnClick="Button1_Click" Enabled="False" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" Text="删除当前增减方式" Width="150px" OnClick="Button3_Click" Enabled="False" /></td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="height: 20px; text-align: center">&nbsp;</td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="text-align: center; background-color: #f6f6f6; height: 26px;">
                    增加增减方式</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    增减方式类别：</td>
                <td class="t2" style="width: 35%">
                    <asp:DropDownList ID="DIType" runat="server">
                        <asp:ListItem Value="1">增加方式</asp:ListItem>
                        <asp:ListItem Value="2">减少方式</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    增减方式名称：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="NewDName" runat="server" Width="255px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    对应入账科目：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="NLinkSubject" runat="server" Width="255px" BackColor="#E0E0E0" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    增值入账科目：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="NValueAddSubject" runat="server" Width="255px" BackColor="#E0E0E0" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="2" style="height: 37px; text-align: center">
                    <asp:Button ID="Button2" runat="server" Text="增加增减方式" Width="200px" OnClick="Button2_Click" /></td>
            </tr>
            <tr>
                <td class="t4" colspan="2" style="height: 20px; text-align: center">&nbsp;</td>
            </tr>
        </table>
    </div>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
