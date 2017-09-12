<%@ Page Language="C#" AutoEventWireup="true" Inherits="Contract_LeaseCard" Codebehind="LeaseCard.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>合同卡片录入</title>
<base target="_self" />
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
//卡片项目选择
function SelectItem(t)
{
    var s=(t==1?2:t)
    var returnV=window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=1&g="+(new Date()).getTime(),"","dialogWidth=360px;dialogHeight:400px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        switch(t)
        {
            case 1:
                $("IncomeSubject").value=returnV[1]+"."+returnV[0];
                break;
            case 2:
                $("PaySubject").value=returnV[1]+"."+returnV[0];
                break;
        }
    }
}
//提交卡片录入
function CheckSubmit()
{
    TableSelect(0);
    if($("LeaseHolder").value.length==0)
    {
        $("LeaseHolder").focus();
        alert("请输入承包人！");
        return false;
    }
    if($("SLease").value.length==0)
    {
        $("SLease").focus();
        alert("请输入开始日期！");
        return false;
    }
    if($("ELease").value.length==0)
    {
        $("ELease").focus();
        alert("请输入结束日期！");
        return false;
    }
    var syear=eval($("SLease").value.substring(0,4));
    var cyear=eval($("ELease").value.substring(0,4));
    if(syear>cyear)
    {
        alert("开始日期不能大于结束日期！");
        return false;
    }
    else
    {
        if(syear==cyear)
        {
            var smonth=eval($("SLease").value.substring(5,7));
            var cmonth=eval($("ELease").value.substring(5,7));
            if(smonth>cmonth)
            {
                alert("开始日期不能大于结束日期！");
                return false;
            }
            else
            {
                if(smonth==cmonth)
                {
                    var sday=eval($("SLease").value.substring(8,10));
                    var cday=eval($("ELease").value.substring(8,10));
                    if(sday>cday)
                    {
                        alert("开始日期不能大于结束日期！");
                        return false;
                    }
                }
            }
        }
    }
    if($("BookDate").value.length==0)
    {
        alert("请输入填制日期！");
        return false;
    }
    var chkstr="ResAmount,YearRental,SumRental,NextPayMoney,ContractMoney,ContractYears".split(",");
    var chknam="数量或面积,租赁价格,已收款总额,下次交款金额,合同总金额,合同年限".split(",");
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
    if ($("ResourceID").value != "000000") {
        var resAmount = eval($("ResAmount").value);
        if (resAmount <= 0) {
            alert("数量或面积必须大于零！");
            return false;
        }
        if ((eval($("HasAmount").value) + eval($("OldAmount").value)) < resAmount) {
            alert("可流转数量或面积小于" + $("ResAmount").value + "！");
            return false;
        }
    }
    TableSelect(1);
    if($("ContractCo").value.length==0)
    {
        $("ContractCo").focus();
        alert("请输入单位名称！");
        return false;
    }
    if($("ContractNo").value.length==0)
    {
        $("ContractNo").focus();
        alert("请输入合同编号！");
        return false;
    }
    if($("ContractDate").value.length==0)
    {
        $("ContractDate").focus();
        alert("请输入合同签订日期！");
        return false;
    }
    TableSelect(0);
    return confirm("您确定需要录入该卡片吗");
}
function MPay()
{
    if($("CardID").value=="000000")
    {
        alert("请先保存合同！");
    }
    else
    {
        var returnV=window.showModalDialog("DefinePay.aspx?id="+$("CardID").value+"&g="+(new Date()).getTime(),"","dialogWidth=650px;dialogHeight:325px;center=yes;");
        if(returnV)
        {
            $("NextPayDate").value=returnV[0];
            $("NextPayMoney").value=returnV[1];
        }
    }
}
function SelPayType()
{
    if($("PayType").value=="8")
    {
        MPay();
    }
}
function WinClose()
{
    TableSelect($("ShowFlag").value);
    alert("合同保存成功！");
}
function OnWinClose()
{
    if(eval($("HasAmount").value)<=0&&window.dialogArguments)
    {
        window.dialogArguments.disabled="disabled";
        window.dialogArguments.onclick=function(){};
        window.dialogArguments.removeAttribute("href");
    }
    window.returnValue="000000";
}
function TableSelect(v)
{
    if(v=="0")
    {
        $("ShowCard").style.color="blue";
        $("ShowContract").style.color="";
        $("ShowCard").style.backgroundColor="";
        $("ShowContract").style.backgroundColor="#f6f6f6";
        $("CardInfo").style.display="";
        $("ContractInfo").style.display="none";
    }
    else
    {
        $("ShowCard").style.color="";
        $("ShowContract").style.color="blue";
        $("ShowCard").style.backgroundColor="#f6f6f6";
        $("ShowContract").style.backgroundColor="";
        $("CardInfo").style.display="none";
        $("ContractInfo").style.display="";
    }
    $("ShowFlag").value = v;
}
function additem() {

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
    //TableSelect($("ShowFlag").value);
}
window.onload = resetDialogSize;
</script>
</head>
<body onunload="OnWinClose()">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t2" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">合同卡片录入</span>&nbsp;
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 750px; text-align:center;">
            <tr>
                <td id="ShowCard" class="t1" style="height:22px; width:374px; cursor:hand; color:blue" onclick="TableSelect('0')">卡片基本信息</td>
                <td id="ShowContract" class="t2" style="height:22px; width:375px; cursor:hand; background-color:#f6f6f6" onclick="TableSelect('1')">合同基本信息</td>
            </tr>
            <tr>
                <td colspan="2" class="t2" style="height:10px; font-size:0pt">&nbsp;</td>
            </tr>
        </table>
        <table id="CardInfo" cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    卡片编号：</td>
                <td class="t1" style="width: 35%;">
                    <asp:TextBox ID="CardNo" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    填制日期：</td>
                <td class="t2" style="width: 35%;">
                    <asp:TextBox ID="BookDate" runat="server" Width="180px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    承&nbsp;&nbsp;包&nbsp;&nbsp;人：</td>
                <td class="t1">
                    <asp:TextBox ID="LeaseHolder" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    联系电话：</td>
                <td class="t2">
                    <asp:TextBox ID="LinkTel" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right; height: 25px;" id="TD_Name" runat="server">
                    租赁资源：</td>
                <td class="t1">
                    <asp:TextBox ID="ResourceName" runat="server" Width="180px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox>
                    <input id="Button4" type="button" value="- 合同项管理 -" style="height:22px; border-width:1px; display:none" onclick="additem();" /></td>
                <td class="t1" style="height: 22px; text-align: right">
                    交款方式：</td>
                <td class="t2" style="height: 22px">
                    <asp:DropDownList ID="PayType" runat="server">
                        <asp:ListItem Value="0">一次付清</asp:ListItem>
                        <asp:ListItem Value="1">半年一次</asp:ListItem>
                        <asp:ListItem Value="2">一年一次</asp:ListItem>
                        <asp:ListItem Value="8">自定义</asp:ListItem>
                    </asp:DropDownList>
                    <input id="Button3" type="button" value="- 收款管理 -" style="height:22px; border-width:1px" onclick="MPay();" /></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    收入科目：</td>
                <td class="t1">
                    <asp:TextBox ID="IncomeSubject" runat="server" BorderWidth="1px" Width="180px" BackColor="#F6F6F6"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    收款科目：</td>
                <td class="t2">
                    <asp:TextBox ID="PaySubject" runat="server" Width="180px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    每次收款金额：</td>
                <td class="t1" style="height: 22px">
                    <asp:TextBox ID="YearRental" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox>（元/年）</td>
                <td class="t1" style="height: 25px; text-align: right">
                    已收款总额：</td>
                <td class="t2" style="height: 22px">
                    <asp:TextBox ID="SumRental" runat="server" BorderWidth="1px" Width="180px">0</asp:TextBox>（元）</td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    下次交款时间：</td>
                <td class="t1" style="height: 22px">
                    <asp:TextBox ID="NextPayDate" runat="server" BackColor="#F6F6F6" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="height: 25px; text-align: right">
                    下次交款金额：</td>
                <td class="t2" style="height: 22px">
                    <asp:TextBox ID="NextPayMoney" runat="server" BorderWidth="1px" Width="180px">0</asp:TextBox>（元）</td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right; height: 25px;">
                    开始日期：</td>
                <td class="t1">
                    <asp:TextBox ID="SLease" runat="server" BackColor="#F6F6F6" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    结束日期：</td>
                <td class="t2">
                    <asp:TextBox ID="ELease" runat="server" BackColor="#F6F6F6" BorderWidth="1px" Width="180px"></asp:TextBox></td>
            </tr>
            <tr id="TdAmount" runat="server">
                <td class="t1" style="text-align: right; height: 22px;">
                    数量或面积：</td>
                <td class="t1" style="height: 22px">
                    <asp:TextBox ID="ResAmount" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right; height: 25px;">
                    计量单位：</td>
                <td class="t2" style="height: 22px">
                    <asp:TextBox ID="ResUnit" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    备注：</td>
                <td class="t2" colspan="3">
                    <asp:TextBox ID="Notes" runat="server" TextMode="MultiLine" Width="554px" Height="115px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right">
                    附件：</td>
                <td class="t2" colspan="3" style="text-align: left">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="559px" unselectable="on" />
                    &nbsp;&nbsp;<asp:HyperLink ID="ShowFile" runat="server" Target="_blank">查看</asp:HyperLink>
                    &nbsp;&nbsp;<asp:LinkButton ID="DelFile" runat="server" OnClick="DelFile_Click">删除</asp:LinkButton></td>
            </tr>
        </table>
        <table id="ContractInfo" cellpadding="0" cellspacing="0" style="width: 750px; display:none">
            <tr>
                <td class="t1" style="width: 15%; height: 25px; text-align: right">
                    单位名称：</td>
                <td class="t1" style="width: 35%;">
                    <asp:TextBox ID="ContractCo" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    所属组织：</td>
                <td class="t2" style="width: 35%;">
                    <asp:TextBox ID="ResUnitName" runat="server" Width="180px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    合同编号：</td>
                <td class="t1">
                    <asp:TextBox ID="ContractNo" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    签订日期：</td>
                <td class="t2">
                    <asp:TextBox ID="ContractDate" runat="server" Width="180px" BorderWidth="1px" BackColor="#F6F6F6"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    合同名称：</td>
                <td class="t1">
                    <asp:TextBox ID="ContractName" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    合同类别：</td>
                <td class="t2">
                    <asp:DropDownList ID="ContractType" runat="server">
                        <asp:ListItem>发包</asp:ListItem>
                        <asp:ListItem>转包</asp:ListItem>
                        <asp:ListItem>转让</asp:ListItem>
                        <asp:ListItem>出租</asp:ListItem>
                        <asp:ListItem>互换</asp:ListItem>
                        <asp:ListItem>入股</asp:ListItem>
                        <asp:ListItem>合作</asp:ListItem>
                        <asp:ListItem>借用</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right; height: 25px;">
                    合同总金额：</td>
                <td class="t1">
                    <asp:TextBox ID="ContractMoney" runat="server" BorderWidth="1px" Width="180px">0</asp:TextBox></td>
                <td class="t1" style="text-align: right;">
                    合同年限：</td>
                <td class="t2">
                    <asp:TextBox ID="ContractYears" runat="server" BorderWidth="1px" Width="180px">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    合同内容摘要：</td>
                <td class="t2" colspan="3">
                    <asp:TextBox ID="ContractContent" runat="server" TextMode="MultiLine" Width="554px" Height="120px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: right">
                    合同到期：<br />履行情况：</td>
                <td class="t2" colspan="3">
                    <asp:TextBox ID="ContractNote" runat="server" TextMode="MultiLine" Width="554px" Height="100px"></asp:TextBox></td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t2" style="height:10px; font-size:0pt">&nbsp;</td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 40px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="保存合同" Width="277px" Height="33px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" style="width: 130px; height: 32px" value="关闭" onclick="window.close();" /></td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="CardID" runat="server" Value="000000" />
    <asp:HiddenField ID="ShowFlag" runat="server" Value="0" />
    <asp:HiddenField ID="OldCardNo" runat="server" />
    <asp:HiddenField ID="ResourceID" runat="server" />
    <asp:HiddenField ID="OldAmount" runat="server" Value="0" />
    <asp:HiddenField ID="HasAmount" runat="server" Value="0" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
