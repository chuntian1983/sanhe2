<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountQuery_DefineReport" Codebehind="DefineReport.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function Number.prototype.str(s){var a=""+this;return s.substring(0,s.length-a.length)+a;}
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function OnRowClick(row,rowid)
{
    if(row==0){return;}
    if($("curRowID").value!="")
    {
        $($("curRowID").value).style.backgroundColor="";
    }
    $(rowid).style.backgroundColor="#f6f6f6";
    $("curRowID").value=rowid;
    $("curRowIndex").value=row;
}
function OnCell0Click(cellid)
{
    $("ControlDiv").style.display="";
    var v=$(cellid).innerHTML.replace(/&nbsp;&nbsp;/g," ");
    if(v=="&nbsp;"||v==" "){v="";}
    $("CellText").value=v;
    $("CellText").select();
    $("TempC").value=cellid;
    SetObjectPos(cellid,"ControlDiv");
}
function wCellText()
{
    $("ControlDiv").style.display="none";
    $($("TempC").value).innerHTML=$("CellText").value.replace(/ /g,"&nbsp;&nbsp;");
}
function OnCell1Click(cellid)
{
    $("ControlDiv").style.display="none";
    var returnV=window.showModalDialog("../AccountInit/DefineExpr.aspx?f=0&g="+(new Date()).getTime(),$(cellid).ItemExpr,"dialogWidth=402px;dialogHeight:359px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        $(cellid).ItemExpr=returnV;
    }
}
function OnCell2Click(cellid)
{
    if($(cellid).innerHTML.indexOf("TempInput")!=-1){return;}
    var v=$(cellid).innerText;
    if(v==" "){v="";}
    $(cellid).innerHTML="<input type=text id=TempInput onblur=\"$('"+cellid+"').innerHTML=this.value;\" value=\""+v+"\" class=TempInput2>";
    $("TempInput").focus();
    $("TempInput").select();
}
function CheckSelRow(v)
{
    $("ControlDiv").style.display="none";
    if($("curRowIndex").value=="0")
    {
        alert("请选择需要执行操作的行，当行背景颜色发生变化时为选定。");
        return false;
    }
    return v==0?confirm('您确定在此行后插入数据吗？'):confirm('您确定删除此行数据吗？');
}
function SetObjectPos(OParent,offsetObj)
{
    var aTag = document.getElementById(OParent);
    var leftpos=aTag.offsetLeft;
    var toppos=aTag.offsetTop;
    while(aTag = aTag.offsetParent)
    {
        leftpos += aTag.offsetLeft;
        toppos += aTag.offsetTop;
    }
    document.getElementById(offsetObj).style.left=leftpos;
    document.getElementById(offsetObj).style.top=toppos;
}
function _OnSubmit()
{
    $("RowItemText").value="";
    switch($("ReportList").value)
    {
        case "000005":
            //收益分配表
            for(var i=1;i<eval($("RowsCount").value);i++)
            {
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(0).innerHTML+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(1).innerHTML+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(2).ItemExpr+"!_1_!!_1_!!_1_!!_1_!!_1_!!_0_!";
            }
            break;
        default:
            //默认标准报表
            for(var i=1;i<eval($("RowsCount").value);i++)
            {
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(0).innerHTML+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(1).innerHTML+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(2).ItemExpr+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(3).ItemExpr+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(4).innerHTML+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(5).innerHTML+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(6).ItemExpr+"!_1_!";
                $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(7).ItemExpr+"!_0_!";
            }
            break;
    }
    $("RowItemText").value=$("RowItemText").value.replace(/&nbsp;&nbsp;/g," ");
    $("RowItemText").value=$("RowItemText").value.replace(/&nbsp;/g,"");
}
function OnSelChange(v,t)
{
    if(t=="0")
    {
        $("ReportDate").value=$("ReportDate").value.replace(/\d{4}年/,v+"年");
    }
    else
    {
        $("ReportDate").value=$("ReportDate").value.replace(/\d{2}月/,v+"月");
    }
}
document.onclick = function onClick(ev)
{
     ev = ev || window.event;   
     var target = ev.target || ev.srcElement;
     var oid=target.getAttribute("id");
     if(("ControlDiv,CellText,"+$("TempC").value).indexOf(oid)==-1||oid=="")
     {
         if($("ControlDiv").style.display==""){wCellText();}
         $("ControlDiv").style.display="none";
     }
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    报表年月：</td>
                <td class="t1" style="width: 35%;">&nbsp;
                    <asp:DropDownList ID="SelYear" runat="server">
                    </asp:DropDownList><asp:DropDownList ID="SelMonth" runat="server">
                        <asp:ListItem Value="01">01月</asp:ListItem>
                        <asp:ListItem Value="02">02月</asp:ListItem>
                        <asp:ListItem Value="03">03月</asp:ListItem>
                        <asp:ListItem Value="04">04月</asp:ListItem>
                        <asp:ListItem Value="05">05月</asp:ListItem>
                        <asp:ListItem Value="06">06月</asp:ListItem>
                        <asp:ListItem Value="07">07月</asp:ListItem>
                        <asp:ListItem Value="08">08月</asp:ListItem>
                        <asp:ListItem Value="09">09月</asp:ListItem>
                        <asp:ListItem Value="10">10月</asp:ListItem>
                        <asp:ListItem Value="11">11月</asp:ListItem>
                        <asp:ListItem Value="12">12月</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 15%; text-align: right">
                    选择报表：</td>
                <td class="t2" style="width: 35%;">&nbsp;
                    <asp:DropDownList ID="ReportList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ReportList_SelectedIndexChanged">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t3" colspan="2" style="text-align:center; height: 32px;">
                    <asp:Button ID="InsertRow" runat="server" Text="插入行" OnClick="InsertRow_Click" Width="80px" />
                    <asp:Button ID="DeleteRow" runat="server" Text="删除行" OnClick="DeleteRow_Click" Width="80px" />
                    <asp:Button ID="ClearReportData" runat="server" Text="清空报表行" Width="100px" OnClick="ClearReportData_Click" />
                    <asp:Button ID="SaveSheet" runat="server" Text="保存报表" OnClick="SaveSheet_Click" Width="90px" /></td>
                <td class="t4" colspan="2" style="text-align:center; height: 32px;">
                    <asp:Button ID="QDataClick" runat="server" OnClick="QDataClick_Click" Text="统计查询" Width="80px" />
                    <input id="Button4" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印报表" style="width:80px" />
                    <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="115px" />
                    <input id="Button1" onclick="location.href='DefineReportList.aspx';" type="button" value="报表管理" style="width:80px" /></td>
            </tr>
        </table>
        <hr style="width: 750px; color:Red; height:2px; text-align:left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span id="ReportTitle" style="color:Blue; font-size: 20pt;" runat="server"></span></td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt;">
            <tr>
                <td style="width:41%; text-align: left">
                    &nbsp; <span style="color: red">编制单位：<asp:Label ID="AName" runat="server"></asp:Label></span></td>
                <td style="width:29%; text-align: left">
                    <span style="color: red">报表年月：</span><span style="color: blue"><asp:TextBox ID="ReportDate"
                        runat="server" BorderWidth="0px" ForeColor="Red" Width="70px"></asp:TextBox></span></td>
                <td style="width:30%; text-align: right">
                    <span style="color: red">单位：元&nbsp;&nbsp;</span></td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CaptionAlign="Left" CssClass="onlyborder" Width="750px" ShowHeader="False">
            <PagerSettings Visible="False" />
            <RowStyle Font-Size="10pt" Height="21px" />
        </asp:GridView>
        <!--#include file="ReportBottom.aspx"-->
    </div>
    <asp:HiddenField ID="RowsCount" runat="server" Value="0" />
    <asp:HiddenField ID="curRowID" runat="server" />
    <asp:HiddenField ID="curRowIndex" runat="server" Value="0" />
    <asp:HiddenField ID="RowItemText" runat="server" />
    <asp:HiddenField ID="AllDesignID" runat="server" />
    <asp:HiddenField ID="TempC" runat="server" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    <div id="ControlDiv" style="border-right: red 1px solid; border-top: red 1px solid;
        display: none; z-index: 1; left: 194px; border-left: red 1px solid; width: 156px;
        border-bottom: red 1px solid; position: absolute; top: 298px; height: 23px; background-color: #f6f6f6;
        text-align: center">
        <asp:TextBox ID="CellText" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="152px"></asp:TextBox>&nbsp;
    </div>
    </form>
</body>
</html>
