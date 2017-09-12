<%@ Page Language="C#" AutoEventWireup="true" Inherits="FixedAsset_ClassManage" Codebehind="ClassManage.aspx.cs" %>

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
    if($("CName").value.length>18)
    {
        $("CName").focus();
        alert("新类别名称长度不能超过18个汉字或字符！");
        return false;
    }
    if($("FASubjectNo").value==$("ClassID").value)
    {
        $("ClassID").focus();
        alert("资产类别编号不能为："+$("FASubjectNo").value+"！");
        return false;
    }
    if($("LinkSubject").value.length==0)
    {
        alert("对应入账科目不能为空！");
        return false;
    }
    var patrn1=/\d/;
    var sl=$("UseLife0").value.length;
    if(sl==0)
    {
      $("UseLife0").focus();
      alert("使用年限不能为空！");
      return false;
    }
    if(!patrn1.test($("UseLife0").value))
    {
      $("UseLife0").focus();
      alert("使用年限必须为数字！");
      return false;
    }
    sl=$("SVRate").value.length;
    if(sl==0)
    {
      $("SVRate").focus();
      alert("净残值率不能为空！");
      return false;
    }
    if(!patrn1.test($("SVRate").value))
    {
      $("SVRate").focus();
      alert("净残值率必须为数字！");
      return false;
    }
    if($("CEditState").value=="1")
    {
        return confirm("注意：如果更改了类别编号和类别名称，将自动更新所有关联卡片！\n\n您确定保存类别编辑吗？");
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
                $("DeprSubject").value=returnV[1]+"."+returnV[0];
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
                    <span style="font-size: 16pt; font-family: 隶书">资产类别管理</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px;">
            <tr>
                <td class="t1" colspan="1" style="height: 22px; background-color: #f6f6f6; text-align: center; width: 35%;">
                    类别列表（<asp:LinkButton ID="UpdateAssetClass" runat="server" OnClick="UpdateAssetClass_Click">同步科目表</asp:LinkButton>）</td>
                <td class="t2" colspan="2" style="height: 22px; background-color: #f6f6f6; text-align: center">
                    类别视图</td>
            </tr>
            <tr>
                <td class="t3" rowspan="12" valign="top" style="height:380px">
                <div style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
                    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ImageSet="XPFileExplorer"
                        NodeIndent="15" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>
                            <asp:TreeNode SelectAction="None" Text="固定资产"></asp:TreeNode>
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
                    <asp:TextBox ID="ClassIDP" runat="server" Width="120px" CssClass="d4" MaxLength="16"></asp:TextBox>
                    <asp:TextBox ID="ClassID" runat="server" Width="130px" CssClass="d4" MaxLength="16"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    类别名称：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="CName" runat="server" Width="256px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    使用年限：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="UseLife0" runat="server" Width="63px" BorderWidth="1px"></asp:TextBox>
                    年&nbsp;&nbsp;<asp:DropDownList ID="UseLife1" runat="server">
                        <asp:ListItem Value="0">0月</asp:ListItem>
                        <asp:ListItem Value="1">1月</asp:ListItem>
                        <asp:ListItem Value="2">2月</asp:ListItem>
                        <asp:ListItem Value="3">3月</asp:ListItem>
                        <asp:ListItem Value="4">4月</asp:ListItem>
                        <asp:ListItem Value="5">5月</asp:ListItem>
                        <asp:ListItem Value="6">6月</asp:ListItem>
                        <asp:ListItem Value="7">7月</asp:ListItem>
                        <asp:ListItem Value="8">8月</asp:ListItem>
                        <asp:ListItem Value="9">9月</asp:ListItem>
                        <asp:ListItem Value="10">10月</asp:ListItem>
                        <asp:ListItem Value="11">11月</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    净残值率：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="SVRate" runat="server" Width="63px" BorderWidth="1px"></asp:TextBox>%</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    计量单位：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="MUnit" runat="server" Width="63px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    折旧方法：</td>
                <td class="t2" style="width: 35%">
                    <asp:DropDownList ID="DeprMethod" runat="server">
                        <asp:ListItem Value="0">不提折旧</asp:ListItem>
                        <asp:ListItem Selected="True" Value="1">平均年限法</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    入账科目：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="LinkSubject" runat="server" BackColor="#E0E0E0" BorderWidth="1px" Width="255px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    对应折旧科目：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="DeprSubject" runat="server" Width="255px" BackColor="#E0E0E0" BorderWidth="1px"></asp:TextBox></td>
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
                <td class="t4" colspan="2" style="height: 60px; text-align: center">&nbsp;</td>
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
