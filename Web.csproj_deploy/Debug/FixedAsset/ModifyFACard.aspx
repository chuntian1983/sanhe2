<%@ Page Language="C#" AutoEventWireup="true" Inherits="FixedAsset_ModifyFACard" Codebehind="ModifyFACard.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function Number.prototype.Fixed(n){with(Math){var tmp=pow(10,n);return round(this*tmp)/tmp;}}
//选择开始使用日期
function AfterSelDate()
{
    if($("SUseDate").value.length>0)
    {
        var syear=eval($("SUseDate").value.substring(0,4));
        var smonth=eval($("SUseDate").value.substring(5,7));
        var cyear=eval($("AccountDate").value.substring(0,4));
        var cmonth=eval($("AccountDate").value.substring(5,7));
        var m=(cyear-syear)*12+cmonth-smonth-1;
        if(m>=-1)
        {
            if(m>0)
            {
                var useDate=parseFloat($("UseLife1").value);
                if($("UseLife0").value!="0")
                {
                   useDate+=parseFloat($("UseLife0").value)*12;
                }
                $("UsedMonths").value=m>useDate?useDate:m;
            }
            else
            {
                $("UsedMonths").value="0";
            }
        }
        else
        {
            $("SUseDate").value="";
            $("UsedMonths").value="0";
            alert("开始使用日期不能在财务日期之后！");
        }
    }
    else
    {
        $("UsedMonths").value="0";
    }
}
//修改使用年限
function ChangeUseLife()
{
    if($("UseLife0").value.length==0)
    {
        $("UseLife0").value="0";
    }
    AfterSelDate();
    ChangeJingCZL();
}
//修改已计提月数
function ChangeUsedMonths()
{
    if($("UsedMonths").value.length==0)
    {
        $("UsedMonths").value="0";
    }
}
//修改原值
function ChangeOldPrice()
{
    if($("OldPrice").value.length==0)
    {
        $("OldPrice").value="0";
    }
    if($("OldPrice").value!="0")
    {
        ChangeZheJiu();
        ChangeJingCZL();
    }
    else
    {
        $("NewPrice").value="0";
        $("JingCZ").value="0";
        $("MonthZJE").value="0";
    }
    ChangeAAmount();
}
//修改累计折旧
function ChangeZheJiu()
{
    if($("ZheJiu").value.length==0)
    {
        $("ZheJiu").value="0";
    }
    if($("OldPrice").value!="0"&&$("ZheJiu").value!="0")
    {
        var ce=formatFloat(parseFloat($("OldPrice").value)-parseFloat($("ZheJiu").value));
        if(ce>=0)
        {
            $("NewPrice").value=ce;
        }
        else
        {
            $("NewPrice").value=0;
            $("ZheJiu").value=$("OldPrice").value;
            alert("累计折旧不能大于原值！");
        }
    }
    else
    {
        $("NewPrice").value=$("OldPrice").value;
    }
    CheckNewP2CZ();
}
//修改净值
function ChangeNewPrice()
{
    if($("NewPrice").value.length==0)
    {
        $("NewPrice").value="0";
    }
    if($("OldPrice").value!="0"&&$("NewPrice").value!="0")
    {
        var ce=formatFloat(parseFloat($("OldPrice").value)-parseFloat($("NewPrice").value));
        if(ce>=0)
        {
            $("ZheJiu").value=ce;
        }
        else
        {
            $("ZheJiu").value=0;
            $("NewPrice").value=$("OldPrice").value;
            alert("净值不能大于原值！");
        }
    }
    else
    {
        $("ZheJiu").value=$("OldPrice").value;
    }
    CheckNewP2CZ();
}
//修改净残值率
function ChangeJingCZL()
{
    if($("JingCZL").value.length==0)
    {
        $("JingCZL").value="0";
    }
    if($("OldPrice").value!="0")
    {
        $("JingCZ").value=formatFloat(eval($("OldPrice").value)*eval($("JingCZL").value)/100);
    }
    else
    {
        $("JingCZ").value="0";
        $("MonthZJL").value="0";
        $("MonthZJE").value="0";
    }
    CheckNewP2CZ();
}
//修改净残值
function ChangeJingCZ()
{
    if($("JingCZ").value.length==0)
    {
        $("JingCZ").value="0";
    }
    if($("OldPrice").value!="0")
    {
        $("JingCZL").value=formatFloat(parseFloat($("JingCZ").value)/parseFloat($("OldPrice").value)*100);
    }
    else
    {
        $("JingCZL").value="0";
        $("MonthZJL").value="0";
        $("MonthZJE").value="0";
    }
    CheckNewP2CZ();
}
//比较净值与净残值
function CheckNewP2CZ()
{
    var a=parseFloat($("NewPrice").value)-parseFloat($("JingCZ").value);
    if(a<0)
    {
        $("JingCZ").value="0";
        $("JingCZL").value="0";
        alert("注意：净残值不能大于净值！");
    }
    ChangeMonthZJ();
}
//修改月折旧率、月折旧额
function ChangeMonthZJ()
{
    if($("UseState").value=="101"&&$("DeprMethod").value=="1")
    {
        var useDate=parseFloat($("UseLife1").value);
        if($("UseLife0").value!="0")
        {
           useDate+=parseFloat($("UseLife0").value)*12-parseFloat($("UsedMonths").value);
        }
        var ce=parseFloat($("NewPrice").value)-parseFloat($("JingCZ").value);
        if(useDate>0&&$("NewPrice").value!="0"&&ce>0)
        {
            $("MonthZJE").value=formatFloat(ce/useDate);
            $("MonthZJL").value=(parseFloat($("MonthZJE").value)/parseFloat($("NewPrice").value)).Fixed(4);
        }
        else
        {
            $("MonthZJL").value="0";
            $("MonthZJE").value="0";
        }
    }
    else
    {
        $("MonthZJL").value="0";
        $("MonthZJE").value="0";
    }
}
function ChangeAAmount()
{
    if($("OldPrice").value!="0"&&$("AAmount").value!="0")
    {
        $("APrice").value=(parseFloat($("OldPrice").value)/parseFloat($("AAmount").value)).Fixed(2);
    }
    else
    {
        $("APrice").value=0;
    }
}
//精确数字
function formatFloat(src)
{
    return Math.round(src*Math.pow(10, 2))/Math.pow(10, 2);
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
    if(t==2)
    {
        var returnV=window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=1&g="+(new Date()).getTime(),"","dialogWidth=360px;dialogHeight:400px;center=yes;");
        if(typeof(returnV)!="undefined")
        {
            $("DeprSubject").value=returnV[1]+"."+returnV[0];
        }
        return;
    }
    var returnV=window.showModalDialog("SelectItem.aspx?t="+t+"&g="+(new Date()).getTime(),"","dialogWidth=360px;dialogHeight:400px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        switch(t)
        {
            case 0:
                $("ClassID").value=returnV[0];
                $("CName").value=returnV[1];
                if($("AssetName").value.length==0)
                {
                    $("AssetName").value=returnV[1];
                }
                break;
            case 1:
                $("DeptName").value=returnV[0]+"."+returnV[1];
                break;
            case 2:
                $("DeprSubject").value=returnV[0]+"."+returnV[1];
                break;
        }
    }
}
//提交卡片录入
function CheckSubmit()
{
    if($("AssetName").value.length==0)
    {
        $("AssetName").focus();
        alert("固定资产名称不能为空！");
        return false;
    }
    if($("ClassID").value.length==0)
    {
        alert("请选择资产类别和类别名称！");
        return false;
    }
    if($("DeptName").value.length==0)
    {
        alert("请选择资产所属部门！");
        return false;
    }
    if ($("OldPrice").value == "0") {
        alert("资产原值不能为零！");
        return false;
    }
    if (parseFloat($("ZheJiu").value) < 0) {
        alert("累计折旧必须大于等于零！");
        return false;
    }
    var nprice = parseFloat($("ZheJiu").value) + parseFloat($("NewPrice").value);
    if (parseFloat($("OldPrice").value) != nprice.Fixed(2)) {
    if (parseFloat($("NewPrice").value) < parseFloat($("JingCZ").value)) {
        alert("净残值不能大于净值！");
        return false;
    }
    if($("UseState").value == "101" && $("DeprMethod").value == "1")
    {
        if ($("AddType").value.length == 0) {
            alert("请选择增加方式！");
            return false;
        }
        if($("UseLife0").value == "0" && $("UseLife1").value == "0")
        {
            alert("使用年限不能为零！");
            return false;
        }
        if($("SUseDate").value.length == 0)
        {
            alert("请输入开始使用日期！");
            return false;
        }
        if ($("DeprSubject").value.length == 0) {
            alert("请选择资产对应折旧科目！");
            return false;
        }
        if (parseFloat($("ZheJiu").value) > 0 && $("UsedMonths").value == "0") {
            alert("已计提月数为零的固定资产，累计折旧必须为零！");
            return false;
        }
    }
    var chkstr="UseLife0,UsedMonths,OldPrice,ZheJiu,NewPrice,JingCZL,JingCZ,AAmount,APrice".split(",");
    var chknam="使用年限,已计提月份,原值,累计折旧,净值,净残值率,净残值,数量或面积,单价".split(",");
    var patrn=/^\d{1,16}(\.\d{1,4})?$/;
    for(var i=0;i<chkstr.length;i++)
    {
        if(!patrn.test($(chkstr[i]).value))
        {
            alert("["+chknam[i]+"]含有非法数据或小数大于4位！");
            return false;
        }
    }
    var pic=$("FileUpload1").value.substring($("FileUpload1").value.length-4).toLowerCase();
    if(pic.length>0&&".gif,.jpg,.bmp".indexOf(pic)==-1)
    {
        alert("上传图片文件格式必须为：.Gif|.Jpg|.Bmp");
        return false;
    }
    return confirm("您确定需要保存该卡片吗");
}
//window.onload=function(){CheckNewP2CZ();}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t2" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">固定资产卡片变更</span>&nbsp;
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr style="background:#f6f6f6">
                <td class="t1" style="width: 15%; text-align: right;">
                    卡片编号：</td>
                <td class="t2" colspan="5">
                    <asp:TextBox ID="CardID" runat="server" BorderWidth="1px" Width="138px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    资产编号：</td>
                <td class="t1" style="width: 20%">
                    <asp:TextBox ID="AssetNo" runat="server" Width="138px" BorderWidth="1px"></asp:TextBox></td>
                <td class="t1" style="width: 18%; text-align: right">
                    资产名称：</td>
                <td class="t2" colspan="3" style="width: 46%">
                    <asp:TextBox ID="AssetName" runat="server" Width="390px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right; height: 22px;">
                    类别编号：</td>
                <td class="t1" style="width: 20%; height: 22px;">
                    <asp:TextBox ID="ClassID" runat="server" Width="138px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
                <td class="t1" style="width: 18%; text-align: right; height: 22px;">
                    类别名称：</td>
                <td class="t2" colspan="3" style="width: 46%; height: 22px;">
                    <asp:TextBox ID="CName" runat="server" Width="390px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right;">
                    规格型号：</td>
                <td class="t1" style="width: 20%;">
                    <asp:TextBox ID="AssetModel" runat="server" Width="138px" BorderWidth="1px"></asp:TextBox></td>
                <td class="t1" style="width: 18%; text-align: right;">
                    部门名称：</td>
                <td class="t2" colspan="3" style="width: 46%">
                    <asp:TextBox ID="DeptName" runat="server" Width="390px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    增加方式：</td>
                <td class="t1" style="width: 20%">
                    <asp:DropDownList ID="AddType" runat="server"></asp:DropDownList></td>
                <td class="t1" style="width: 18%; text-align: right">
                    存放地点：</td>
                <td class="t2" colspan="3" style="width: 46%">
                    <asp:TextBox ID="Depositary" runat="server" Width="390px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    使用状况：
                </td>
                <td class="t1" style="width: 20%">
                    <asp:DropDownList ID="UseState" runat="server">
                        <asp:ListItem Value="101">使用中</asp:ListItem>
                        <asp:ListItem Value="102">未使用</asp:ListItem>
                        <asp:ListItem Value="103">不需要</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 18%; text-align: right">
                    使用年限：</td>
                <td class="t2" style="width: 17%">
                    <asp:TextBox ID="UseLife0" runat="server" Width="63px" BorderWidth="1px">20</asp:TextBox>
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
                <td class="t2" style="width: 12%; text-align: right;">
                    折旧方法：</td>
                <td class="t2" style="width: 17%">
                    <asp:DropDownList ID="DeprMethod" runat="server">
                        <asp:ListItem Value="0">不提折旧</asp:ListItem>
                        <asp:ListItem Selected="True" Value="1">平均年限法</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    开始使用日期：</td>
                <td class="t1" style="width: 20%">
                    <asp:TextBox ID="SUseDate" runat="server" Width="138px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
                <td class="t1" style="width: 18%; text-align: right">
                    已计提月份：</td>
                <td class="t2" style="width: 17%">
                    <asp:TextBox ID="UsedMonths" runat="server" Width="125px" BorderWidth="0px">0</asp:TextBox>月</td>
                <td class="t2" style="width: 12%; text-align: right;">
                    币种：</td>
                <td class="t2" style="width: 17%;">
                    <asp:TextBox ID="CurrencyType" runat="server" Width="140px" BorderWidth="0px">人民币</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right;">
                    原值：</td>
                <td class="t1" style="width: 20%;">
                    <asp:TextBox ID="OldPrice" runat="server" Width="138px" BorderWidth="1px">0</asp:TextBox></td>
                <td class="t1" style="width: 18%; text-align: right;">
                    净残值率：</td>
                <td class="t2" style="width: 17%;">
                    <asp:TextBox ID="JingCZL" runat="server" Width="125px" BorderWidth="1px">4</asp:TextBox>%</td>
                <td class="t2" style="width: 12%; text-align: right;">
                    净残值：</td>
                <td class="t2" style="width: 17%;">
                    <asp:TextBox ID="JingCZ" runat="server" Width="140px" BorderWidth="1px">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    累计折旧：</td>
                <td class="t1" style="width: 20%">
                    <asp:TextBox ID="ZheJiu" runat="server" Width="138px" BorderWidth="1px">0</asp:TextBox></td>
                <td class="t1" style="width: 18%; text-align: right">
                    月折旧率：</td>
                <td class="t2" style="width: 17%">
                    <asp:TextBox ID="MonthZJL" runat="server" Width="140px" BorderWidth="0px">0</asp:TextBox></td>
                <td class="t2" style="width: 12%; text-align: right;">
                    月折旧额：</td>
                <td class="t2" style="width: 17%">
                    <asp:TextBox ID="MonthZJE" runat="server" Width="140px" BorderWidth="0px">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    净值：</td>
                <td class="t1" style="width: 20%">
                    <asp:TextBox ID="NewPrice" runat="server" Width="138px" BorderWidth="1px">0</asp:TextBox></td>
                <td class="t1" style="width: 18%; text-align: right">
                    对应折旧科目：</td>
                <td class="t2" style="width: 17%">
                    <asp:TextBox ID="DeprSubject" runat="server" Width="140px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
                <td class="t2" style="width: 12%; text-align: right;">
                    项目：</td>
                <td class="t2" style="width: 17%">
                    <asp:TextBox ID="AssetItem" runat="server" Width="140px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    计量单位：</td>
                <td class="t1" style="width: 20%">
                    <asp:TextBox ID="AUnit" runat="server" BorderWidth="1px" Width="138px"></asp:TextBox></td>
                <td class="t1" style="width: 18%; text-align: right">
                    数量或面积：</td>
                <td class="t2" style="width: 17%">
                    <asp:TextBox ID="AAmount" runat="server" BorderWidth="1px" Width="140px">0</asp:TextBox></td>
                <td class="t2" style="width: 12%; text-align: right">
                    单价：</td>
                <td class="t2" style="width: 17%">
                    <asp:TextBox ID="APrice" runat="server" BorderWidth="1px" Width="140px">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    责任人：</td>
                <td class="t1" style="width: 20%">
                    <asp:TextBox ID="AssetAdmin" runat="server" BorderWidth="1px" Width="138px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    资产图片：</td>
                <td class="t2" colspan="3" style="text-align: left">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="315px" unselectable="on" />
                    &nbsp;&nbsp;<asp:HyperLink ID="ShowFile" runat="server" Target="_blank">查看</asp:HyperLink>
                    &nbsp;&nbsp;<asp:LinkButton ID="DelFile" runat="server" OnClick="DelFile_Click">删除</asp:LinkButton></td>
            </tr>
            <tr>
                <td class="t2" colspan="6" style="text-align: center; background-color:#f8f8f8; color:Blue">
                    变更记录</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    变更项目：</td>
                <td class="t1" style="width: 20%">
                    <asp:DropDownList ID="ChangeType" runat="server">
                        <asp:ListItem>选择项目</asp:ListItem>
                        <asp:ListItem>卡片编号</asp:ListItem>
                        <asp:ListItem>资产编号</asp:ListItem>
                        <asp:ListItem>资产名称</asp:ListItem>
                        <asp:ListItem>资产类别</asp:ListItem>
                        <asp:ListItem>规格型号</asp:ListItem>
                        <asp:ListItem>部门名称</asp:ListItem>
                        <asp:ListItem>增加方式</asp:ListItem>
                        <asp:ListItem>存放地点</asp:ListItem>
                        <asp:ListItem>使用状况</asp:ListItem>
                        <asp:ListItem>使用年限</asp:ListItem>
                        <asp:ListItem>折旧方法</asp:ListItem>
                        <asp:ListItem>开始使用日期</asp:ListItem>
                        <asp:ListItem>资产原值</asp:ListItem>
                        <asp:ListItem>累计折旧</asp:ListItem>
                        <asp:ListItem>资产净值</asp:ListItem>
                        <asp:ListItem>净残值</asp:ListItem>
                        <asp:ListItem>对应折旧科目</asp:ListItem>
                        <asp:ListItem>计量单位</asp:ListItem>
                        <asp:ListItem>数量或面积</asp:ListItem>
                        <asp:ListItem>单价</asp:ListItem>
                        <asp:ListItem>责任人</asp:ListItem>
                        <asp:ListItem>资产图片</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 18%; text-align: right">
                    变更内容：</td>
                <td class="t2" colspan="3" style="width: 46%">
                    <asp:TextBox ID="ChangeNotes" runat="server" Width="390px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="6" style="height: 52px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="保存资产卡片" Width="240px" Height="33px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" onclick="location.href='FixedAssetList.aspx';" type="button" value="显示资产卡片列表" style="width: 180px; height: 33px" /></td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="text-align:center">
                    <div style="margin:5px; text-align:left; width:735px">
                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" CaptionAlign="Left" Height="1px" Style="color: navy" Width="735px">
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
                        <Columns>
                            <asp:BoundField DataField="logdefine3" HeaderText="变更项目">
                                <ItemStyle Width="130px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="logcontent" HeaderText="变更内容" ReadOnly="True">
                                <ItemStyle Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="logname" HeaderText="变更人">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="logtime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="变更时间"
                                HtmlEncode="False" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="logdefine2" HeaderText="操作IP">
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle Font-Size="10pt" />
                        <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
                        <PagerStyle BackColor="White" ForeColor="Olive" />
                        <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="#EBF0F6" />
                    </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="IsNewCard" runat="server" />
    <asp:HiddenField ID="AccountDate" runat="server" />
    <asp:HiddenField ID="DeprState" runat="server" />
    <asp:HiddenField ID="AssetState" runat="server" />
    <asp:HiddenField ID="CVoucher" runat="server" />
    <asp:HiddenField ID="BookDate" runat="server" />
    <asp:HiddenField ID="OldCardID" runat="server" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
    <script type="text/javascript" src="../Images/SelDate/getcalendar3.js"></script>
</body>
</html>
