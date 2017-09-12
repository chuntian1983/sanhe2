<%@ Page Language="C#" AutoEventWireup="true" Inherits="FixedAsset_AssetCleanup" Codebehind="AssetCleanup.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("LSubject").value=="")
    {
      alert("请选择固定资产清理后净损失入账对应科目！");
      return false;
    }
    if($("CDate").value=="")
    {
      alert("请输入资产清理日期！");
      return false;
    }
    if($("CRMoney").value.length>0&&$("CRSubject").value.length==0)
    {
      alert("请选择清理收入入账科目！");
      return false;
    }
    if($("CCMoney").value.length>0&&$("CCSubject").value.length==0)
    {
      alert("请选择清理费用入账科目！");
      return false;
    }
    return confirm("您确定需要执行资产清理操作吗？");
}
//校验数字录入
function ValidateNumber(v)
{
    var str=document.selection.createRange().text;
    if((v.indexOf(".")==-1||str.indexOf(".")!=-1)&&v.replace(str,"").length>0)
    {
       return (event.keyCode>=48&&event.keyCode<=57)||event.keyCode==46;
    }
    else
    {
       return (event.keyCode>=48&&event.keyCode<=57);
    }
}
//卡片项目选择
function SelectItem(t)
{
    var returnV=window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=1&g="+(new Date()).getTime(),"","dialogWidth=360px;dialogHeight:400px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        switch(t)
        {
            case 0:
                $("LSubject").value=returnV[1]+"."+returnV[0];
                break;
            case 1:
                $("CRSubject").value=returnV[1]+"."+returnV[0];
                break;
            case 2:
                $("CCSubject").value=returnV[1]+"."+returnV[0];
                break;
        }
    }
}
function VSelSubject(t)
{
    if($("CRMoney").value.length==0&&t==1)
    {
      $("CRMoney").focus();
      alert("应先输入清理收入，再选择对应科目！");
      return;
    }
    if($("CCMoney").value.length==0&&t==2)
    {
      $("CCMoney").focus();
      alert("应先输入清理费用，再选择对应科目！");
      return;
    }
    SelectItem(t);
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">固定资产清理</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    卡片编号：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="CardID" runat="server" BorderWidth="0px" Width="257px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    资产编号：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="AssetNo" runat="server" BorderWidth="0px" Width="257px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    资产名称：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="AssetName" runat="server" Width="252px" BorderWidth="0px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    清理日期：</td>
                <td class="t2" style="width: 35%;">
                    <asp:TextBox ID="CDate" runat="server" BackColor="#E0E0E0" BorderWidth="1px" Width="255px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    清理方式：</td>
                <td class="t1" style="width: 35%">
                    <asp:DropDownList ID="CType" runat="server"></asp:DropDownList></td>
                <td class="t1" style="width: 15%; text-align: right">
                    损益入账科目：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="LSubject" runat="server" BackColor="#E0E0E0" BorderWidth="1px" Width="255px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    清理收入：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="CRMoney" runat="server" Width="253px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    清理收入科目：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="CRSubject" runat="server" BackColor="#E0E0E0" BorderWidth="1px" Width="255px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    清理费用：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="CCMoney" runat="server" Width="253px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    清理费用科目：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="CCSubject" runat="server" BackColor="#E0E0E0" BorderWidth="1px" Width="255px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    清理原因：</td>
                <td class="t2" colspan="3">
                    <asp:TextBox ID="CNotes" runat="server" Rows="5" TextMode="MultiLine" Width="629px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 37px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="确定清理" Width="160px" Height="30px" OnClick="Button1_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" Text="直接删除" Width="160px" Height="30px" OnClick="Button3_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" onclick="location.href='FixedAssetList.aspx';" style="height:30px" type="button" value="显示资产卡片列表" /></td>
            </tr>
        </table>
    </div>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
