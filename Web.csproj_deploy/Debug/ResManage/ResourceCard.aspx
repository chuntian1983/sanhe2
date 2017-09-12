<%@ Page Language="C#" AutoEventWireup="true" Inherits="ResManage_ResourceCard" Codebehind="ResourceCard.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
//卡片项目选择
function SelectItem(t)
{
    var returnV=window.showModalDialog("SelectItem.aspx?t="+t+"&g="+(new Date()).getTime(),"","dialogWidth=360px;dialogHeight:400px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        switch(t)
        {
            case 0:
                $("ClassID").value=returnV[0];
                $("ClassName").value=returnV[1];
                $("ResUnit").value=returnV[2];
                break;
            case 1:
                $("DeptName").value=returnV[0]+"."+returnV[1];
                break;
        }
    }
}
//提交卡片录入
function CheckSubmit()
{
    if($("ResName").value.length==0)
    {
        $("ResName").focus();
        alert("资源名称不能为空！");
        return false;
    }
    if($("ClassID").value.length==0)
    {
        alert("请选择资源类别和类别名称！");
        return false;
    }
    if($("DeptName").value.length==0)
    {
        alert("请选择资源所属部门！");
        return false;
    }
    if($("BookDate").value.length==0)
    {
        alert("请输入填制日期！");
        return false;
    }
    if($("ResAmount").value=="0")
    {
        $("ResAmount").focus();
        alert("数量或面积不能为零！");
        return false;
    }
    var chkstr="RelateFarmers,ResAmount".split(",");
    var chknam="涉及农民数,资源数量或面积".split(",");
    var patrn=/^\d{1,15}(\.\d{1,3})?$/;
    for(var i=0;i<chkstr.length;i++)
    {
        if(!patrn.test($(chkstr[i]).value))
        {
            $(chkstr[i]).focus();
            alert("["+chknam[i]+"]含有非法数据！");
            return false;
        }
    }
    var pic=$("FileUpload1").value.substring($("FileUpload1").value.length-4).toLowerCase();
    if(pic.length>0&&".gif,.jpg,.bmp".indexOf(pic)==-1)
    {
        alert("上传图片文件格式必须为：.Gif|.Jpg|.Bmp");
        return false;
    }
    return confirm("您确定需要录入该卡片吗");
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">资源卡片录入</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    卡片编号：</td>
                <td class="t1">
                    <asp:TextBox ID="CardNo" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    填制日期：</td>
                <td class="t2">
                    <asp:TextBox ID="BookDate" runat="server" Width="180px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right; height: 25px;">
                    类别编号：</td>
                <td class="t1">
                    <asp:TextBox ID="ClassID" runat="server" Width="180px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
                <td class="t1" style="text-align: right;">
                    类别名称：</td>
                <td class="t2">
                    <asp:TextBox ID="ClassName" runat="server" Width="180px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right; height: 25px;">
                    经营方式：</td>
                <td class="t1">
                    <asp:DropDownList ID="BookType" runat="server">
                        <asp:ListItem>发包</asp:ListItem>
                        <asp:ListItem>转包</asp:ListItem>
                        <asp:ListItem>互换</asp:ListItem>
                        <asp:ListItem>入股</asp:ListItem>
                        <asp:ListItem>合作</asp:ListItem>
                        <asp:ListItem>出租</asp:ListItem>
                        <asp:ListItem>借用</asp:ListItem>
                        <asp:ListItem>闲置</asp:ListItem>
                        <asp:ListItem>集体经营</asp:ListItem>
                        <asp:ListItem>其他</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="text-align: right">
                    使用状况：</td>
                <td class="t2">
                    <asp:DropDownList ID="UsedState" runat="server">
                        <asp:ListItem Value="0">使用中</asp:ListItem>
                        <asp:ListItem Value="1">未使用</asp:ListItem>
                        <asp:ListItem Value="2">已荒废</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right; height: 25px;">
                    资源编号：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="ResNo" runat="server" Width="180px" BorderWidth="1px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    资源名称：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="ResName" runat="server" Width="180px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right; height: 25px;">
                    部门名称：</td>
                <td class="t1">
                    <asp:TextBox ID="DeptName" runat="server" Width="180px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
                <td class="t1" style="height: 25px; text-align: right">
                    涉及农民数：</td>
                <td class="t2">
                    <asp:TextBox ID="RelateFarmers" runat="server" BorderWidth="1px" Width="180px">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right; height: 22px;">
                    数量或面积：</td>
                <td class="t1" style="height: 22px">
                    <asp:TextBox ID="ResAmount" runat="server" BorderWidth="1px" Width="180px">0</asp:TextBox></td>
                <td class="t1" style="text-align: right; height: 25px;">
                    计量单位：</td>
                <td class="t2" style="height: 22px">
                    <asp:TextBox ID="ResUnit" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right; height: 25px;">
                    座落：</td>
                <td class="t2" colspan="3">
                    <asp:TextBox ID="Locality" runat="server" Width="554px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    东至：</td>
                <td class="t1" style="height: 25px">
                    <asp:TextBox ID="BorderE" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="height: 25px; text-align: right">
                    西至：</td>
                <td class="t2" style="height: 25px">
                    <asp:TextBox ID="BorderW" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    南至：</td>
                <td class="t1">
                    <asp:TextBox ID="BorderS" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    北至：</td>
                <td class="t2">
                    <asp:TextBox ID="BorderN" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" style="height: 25px; text-align: center; background-color:#f0f0f0" colspan="4">
                    依法承包</td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    面积：</td>
                <td class="t1">
                    <asp:TextBox ID="name4" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    &nbsp;</td>
                <td class="t2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="t2" style="height: 25px; text-align: center; background-color:#f0f0f0" colspan="4">
                    集体经营</td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    面积：</td>
                <td class="t1">
                    <asp:TextBox ID="name5" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    年收益：</td>
                <td class="t2">
                    <asp:TextBox ID="name6" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" style="height: 25px; text-align: center; background-color:#f0f0f0" colspan="4">
                    承包经营</td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    面积：</td>
                <td class="t1">
                    <asp:TextBox ID="name7" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    承包人：</td>
                <td class="t2">
                    <asp:TextBox ID="name8" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    承包起止日期：</td>
                <td class="t1">
                    <asp:TextBox ID="name9" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    年承包金：</td>
                <td class="t2">
                    <asp:TextBox ID="name10" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    支付方式：</td>
                <td class="t1">
                    <asp:TextBox ID="name11" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    &nbsp;</td>
                <td class="t2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="t2" style="height: 25px; text-align: center; background-color:#f0f0f0" colspan="4">
                    对外投资</td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    面积：</td>
                <td class="t1">
                    <asp:TextBox ID="name12" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    年投资收益：</td>
                <td class="t2">
                    <asp:TextBox ID="name13" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    支付方式：</td>
                <td class="t1">
                    <asp:TextBox ID="name14" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    投资对象：</td>
                <td class="t2">
                    <asp:TextBox ID="name15" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" style="height: 25px; text-align: center; background-color:#f0f0f0" colspan="4">
                    其他占用方式</td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    已利用面积：</td>
                <td class="t1">
                    <asp:TextBox ID="name16" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    闲置面积：</td>
                <td class="t2">
                    <asp:TextBox ID="name17" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    评估年收益：</td>
                <td class="t1">
                    <asp:TextBox ID="name18" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    &nbsp;</td>
                <td class="t2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    资源用途：</td>
                <td class="t2" colspan="3">
                    <asp:TextBox ID="ResUsage" runat="server" TextMode="MultiLine" Width="554px" Height="20px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    资源备注：</td>
                <td class="t2" colspan="3">
                    <asp:TextBox ID="Notes" runat="server" TextMode="MultiLine" Width="554px" Height="45px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right">
                    资源图片：</td>
                <td class="t2" colspan="3" style="text-align: left">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="559px" unselectable="on" /></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 52px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="录入资源卡片" Width="277px" Height="33px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" onclick="location.href='ResourceList.aspx';" type="button" value="显示资源卡片列表" style="width: 191px; height: 33px" /></td>
            </tr>
        </table>
    </div>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
